using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class MetPairCollection_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;

        public MetPairCollection_Tests()
        {
            testingFolder = globals.testingFolder + "MetPairCollection";
        }

        [TestMethod]
        public void GetRMS_SectorErr_Test()
        {
            Continuum thisInst = new Continuum("", false);
            

            string Filename =  testingFolder + "\\MetPairCollection Panhandle.cfm";
            thisInst.Open(Filename);

            Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);

            // Test 1
            double thisErr = thisInst.metPairList.GetRMS_SectorErr(theseModels[0], 0);
            Assert.AreEqual(thisErr, 0.060588, 0.001, "Wrong RMS error");

            // Test 2
            thisErr = thisInst.metPairList.GetRMS_SectorErr(theseModels[0], 8);
            Assert.AreEqual(thisErr, 0.033429, 0.001, "Wrong RMS error");

            // Test 3
            thisErr = thisInst.metPairList.GetRMS_SectorErr(theseModels[3], 3);
            Assert.AreEqual(thisErr, 0.235717, 0.001, "Wrong RMS error");

            // Test 4
            thisErr = thisInst.metPairList.GetRMS_SectorErr(theseModels[3], 15);
            Assert.AreEqual(thisErr, 0.021076, 0.001, "Wrong RMS error");                       

            thisInst.Close();
        }

        [TestMethod]
        public void DoRR_Calc_Test()
        {
            Continuum thisInst = new Continuum("", false);
            
            string MCP_Method = thisInst.Get_MCP_Method();

            string Filename = testingFolder + "\\MetPairCollection Great Western.cfm";
            thisInst.Open(Filename);
            MetPairCollection.RR_funct_obj[] thisRR_Obj = new MetPairCollection.RR_funct_obj[3];
            string[,] Mets_for_UWDW = new string[3, 3];
            Mets_for_UWDW[0, 0] = "Met_474";
            Mets_for_UWDW[0, 1] = "Met_474";
            Mets_for_UWDW[0, 2] = "Met_475";
            Mets_for_UWDW[1, 0] = "Met_475";
            Mets_for_UWDW[1, 1] = "Met_3350";
            Mets_for_UWDW[1, 2] = "Met_3350";

            Assert.AreEqual(thisInst.metPairList.RoundRobinCount, 0, "Round Robin calcs already done");

            string[] theseMets = new string[2];
            theseMets[0] = "Met_474";
            theseMets[1] = "Met_475";
            thisRR_Obj[0] = thisInst.metPairList.DoRR_Calc(theseMets, thisInst, thisInst.metList.GetMetsUsed(), MCP_Method);

            theseMets = new string[2];
            theseMets[0] = "Met_474";
            theseMets[1] = "Met_3350";
            thisRR_Obj[1] = thisInst.metPairList.DoRR_Calc(theseMets, thisInst, thisInst.metList.GetMetsUsed(), MCP_Method);

            theseMets = new string[2];
            theseMets[0] = "Met_475";
            theseMets[1] = "Met_3350";
            thisRR_Obj[2] = thisInst.metPairList.DoRR_Calc(theseMets, thisInst, thisInst.metList.GetMetsUsed(), MCP_Method);

            thisInst.metPairList.AddRoundRobinEst(thisRR_Obj, thisInst.metList.GetMetsUsed(), 2, Mets_for_UWDW);
            Assert.AreEqual(thisInst.metPairList.roundRobinEsts[0].RMS_All, 0.011393858, 0.0005, "Wrong RMS error");
            Assert.AreEqual(thisInst.metPairList.roundRobinEsts[0].RMS_Err[0], 0.015460321, 0.0005, "Wrong error in first model");
            Assert.AreEqual(thisInst.metPairList.roundRobinEsts[0].RMS_Err[1], 0.007049366, 0.0005, "Wrong error in second model");
            Assert.AreEqual(thisInst.metPairList.roundRobinEsts[0].RMS_Err[2], 0.010037177, 0.0005, "Wrong error in third model");

            thisInst.Close();
        }

        [TestMethod]
        public void SweepGetRMSWithAdjModel_Test()
        {
            // Creates a model with modified values and an Model_Adj with adjustments that should equate to same model as modified values
            // Calls SweepGetRMSWithAdjModel with the Model_Adj and calls Do_Met_Cross_Preds using the model with modified values. 
            // Compares the RMS error found from the two

            Continuum thisInst = new Continuum("", false);
            
            NodeCollection nodeList = new NodeCollection();
                        
            string Filename = testingFolder + "\\MetPairCollection Panhandle.cfm";
            thisInst.Open(Filename);

            // Clear other wind speed cross-predictions
            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
                thisInst.metPairList.metPairs[i].WS_Pred = null;

            int numWD = thisInst.metList.numWD;
            // First create model with modified values and Model_Adj with modifying factors/values
            Model[] Modified_UWDW = new Model[1];
            Modified_UWDW[0] = new Model();
            Modified_UWDW[0].SizeArrays(numWD);
            Modified_UWDW[0].SetDefaultModelCoeffs(numWD);
            Modified_UWDW[0].radius = 4000;
            Modified_UWDW[0].metsUsed = thisInst.metList.GetMetsUsed();
            Modified_UWDW[0].season = Met.Season.All;
            Modified_UWDW[0].timeOfDay = Met.TOD.All;

            Model[] defaultModel = new Model[1];
            defaultModel[0] = new Model();
            defaultModel[0].SizeArrays(numWD);
            defaultModel[0].metsUsed = thisInst.metList.GetMetsUsed();
            defaultModel[0].radius = 4000;
            defaultModel[0].season = Met.Season.All;
            defaultModel[0].timeOfDay = Met.TOD.All;

            // Add WS Pred with Default model
            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
                thisInst.metPairList.metPairs[i].AddWS_Pred(defaultModel);

            MetPairCollection.Model_Adj Adj_UWDW = new MetPairCollection.Model_Adj();
            thisInst.metPairList.SizeAdjModel(ref Adj_UWDW, numWD);

            for (int i = 0; i < numWD; i++)
            {
                Modified_UWDW[0].downhill_A[i] = (0.5 + i / 10.0) * Modified_UWDW[0].downhill_A[i];
                Adj_UWDW.DH_A_Adj[i] = (0.5 + i / 10.0);

                Modified_UWDW[0].downhill_B[i] = (2 - i / 10.0) * Modified_UWDW[0].downhill_B[i];
                Adj_UWDW.DH_B_Adj[i] = (2 - i / 10.0);

                Modified_UWDW[0].uphill_A[i] = (0.5 + i / 10.0) * Modified_UWDW[0].uphill_A[i];
                Adj_UWDW.UH_A_Adj[i] = (0.5 + i / 10.0);

                Modified_UWDW[0].uphill_B[i] = (2 - i / 10.0) * Modified_UWDW[0].uphill_B[i];
                Adj_UWDW.UH_B_Adj[i] = (2 - i / 10.0);

                Modified_UWDW[0].UW_crit[i] = 10 + 2 * i;
                Adj_UWDW.UW_Crit[i] = 10 + 2 * i;

                Modified_UWDW[0].spdUp_A[i] = (0.5 + i / 10.0) * Modified_UWDW[0].spdUp_A[i];
                Adj_UWDW.SU_A_Adj[i] = (0.5 + i / 10.0);

                Modified_UWDW[0].spdUp_B[i] = (2 - i / 10.0) * Modified_UWDW[0].spdUp_B[i];
                Adj_UWDW.SU_B_Adj[i] = (2 - i / 10.0);

                Modified_UWDW[0].DH_Stab_A[i] = (0.5 + i / 10.0);
                Adj_UWDW.DH_Stab_A[i] = (0.5 + i / 10.0);

                Modified_UWDW[0].UH_Stab_A[i] = (3 - i / 10.0);
                Adj_UWDW.UH_Stab_A[i] = (3 - i / 10.0);

                Modified_UWDW[0].SU_Stab_A[i] = (0.2 + i / 5.0);
                Adj_UWDW.SU_Stab_A[i] = (0.2 + i / 5.0);
            }

            double Sweep_get_RMS_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref defaultModel[0], Adj_UWDW, thisInst);

            // Calculate met cross-prediction error using Modified_UWDW and compare to Sweep_get_RMS_Error

            // Clear other wind speed cross-predictions
            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
                thisInst.metPairList.metPairs[i].WS_Pred = null;

            thisInst.modelList.AddModel(Modified_UWDW);

            Pair_Of_Mets[] metPairs = thisInst.metPairList.metPairs;
            double RMS_err = 0;
            int RMS_Count = 0;

            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
            {
                metPairs[i].AddWS_Pred(Modified_UWDW);
                int WS_PredInd = metPairs[i].GetWS_PredIndOneModel(Modified_UWDW[0], thisInst.modelList);

                metPairs[i].DoMetCrossPred(WS_PredInd, 0, thisInst);

                RMS_err = RMS_err + Math.Pow(metPairs[i].WS_Pred[WS_PredInd, 0].percErr[0], 2);
                RMS_Count++;

                RMS_err = RMS_err + Math.Pow(metPairs[i].WS_Pred[WS_PredInd, 0].percErr[1], 2);
                RMS_Count++;
            }

            RMS_err = Math.Pow((RMS_err / (RMS_Count)), 0.5);

            Assert.AreEqual(Sweep_get_RMS_Error, RMS_err, 0.000001);

            thisInst.Close();
        }

        [TestMethod]
        public void Sweep_a_Param_Test()
        {
            Continuum thisInst = new Continuum("", false);
            
            NodeCollection nodeList = new NodeCollection();
                        
            string Filename = testingFolder + "\\MetPairCollection Panhandle.cfm";
            thisInst.Open(Filename);
            int numWD = thisInst.metList.numWD;

            // For each Iter_type, call SweepGetRMSWithAdjModel and GetRMS_SectorErr for each value interval. Find Minimum Error
            // Call Sweep_a_Param with same Iter_type and value intervals.
            // Compare the adjusted model coefficients

            // Used in Sweep_get_RMS_All_WD
            MetPairCollection.Model_Adj This_Model_Adj_1 = new MetPairCollection.Model_Adj();
            thisInst.metPairList.SizeAdjModel(ref This_Model_Adj_1, numWD);

            // Used in Sweep_get_RMS_All_WD
            MetPairCollection.Model_Adj This_Model_Adj_2 = new MetPairCollection.Model_Adj();
            thisInst.metPairList.SizeAdjModel(ref This_Model_Adj_2, numWD);

            MetPairCollection.Init_Params theseInitParams = new MetPairCollection.Init_Params();
            double[] initCoeffs = new double[4]; // 0: Intercept 1: m_uw 2: m_dw 3: Rsq

            MetPairCollection.MinMax_Expos theseMinMax = new MetPairCollection.MinMax_Expos();
            thisInst.metPairList.InitMinMaxExpos(ref theseMinMax, numWD);

            Model[] thisModel = new Model[1];
            thisModel[0] = new Model();
            thisModel[0].radius = 4000;
            thisModel[0].metsUsed = thisInst.metList.GetMetsUsed();
            thisModel[0].SizeArrays(numWD);
            thisModel[0].SetDefaultModelCoeffs(numWD);
            thisModel[0].season = Met.Season.All;
            thisModel[0].timeOfDay = Met.TOD.All;
            thisModel[0].height = 80;

            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
            {
                thisInst.metPairList.metPairs[i].WS_Pred = null;
                thisInst.metPairList.metPairs[i].AddWS_Pred(thisModel);
            }

            Model defaultModel = new Model();
            defaultModel.radius = 4000;
            defaultModel.metsUsed = thisInst.metList.GetMetsUsed();
            defaultModel.SizeArrays(numWD);
            defaultModel.SetDefaultModelCoeffs(numWD);
            defaultModel.season = Met.Season.All;
            defaultModel.timeOfDay = Met.TOD.All;
            defaultModel.height = 80;

            thisInst.metPairList.Calc_MinMax_Expos(ref theseMinMax, thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, Met.TOD.All, Met.Season.All), 0, thisInst.metList, thisModel[0]); // finds min/max UW expo (used to initialize UW crit), min/max sum of UWDW (used in flow separation model), avg P10 expo and min/max WS

            //calculate linear regression to find initial coefficients
            thisInst.metPairList.FindBestInitCoeffs(thisModel[0], ref theseInitParams, ref initCoeffs, thisInst.metList, 0, theseMinMax);
            thisInst.metPairList.InitializeAdjModel(ref This_Model_Adj_1, numWD, theseInitParams, initCoeffs, thisModel[0], theseMinMax);
            thisInst.metPairList.InitializeAdjModel(ref This_Model_Adj_2, numWD, theseInitParams, initCoeffs, thisModel[0], theseMinMax);

            double Mid_Val_1 = 0;
            double Mid_Val_2 = 0;
            int Mid_Int = Convert.ToInt16((((2 - 0.2) / 0.2) + 1) / 2) - 1;
            bool Error_Changed = false;
            double Last_Error = 0;

            // TEST 1
            // Downhill flow A. Sweep params from 0.2 to 2 with interval = 0.2
            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 10; i++)
                {
                    double Val = 0.2 * i + 0.2;
                    This_Model_Adj_1.DH_A_Adj[WD_Ind] = Val * theseInitParams.DH / defaultModel.downhill_A[WD_Ind]; // B = 0

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.DH_A_Adj[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.DH_A_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                This_Model_Adj_1.DH_A_Adj[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0.2f, 2, 0.2f, WD_Ind, "Downhill A", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.DH_A_Adj[WD_Ind], This_Model_Adj_2.DH_A_Adj[WD_Ind], 0.00001, "Wrong DH A coeff. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 2
            // Uphill flow A. Sweep params from 0.2 to 2 with interval = 0.2
            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;
                
                for (int i = 0; i < 10; i++)
                {
                    double Val = 0.2 * i + 0.2;
                    This_Model_Adj_1.UH_A_Adj[WD_Ind] = Val * Math.Abs(theseInitParams.UH) / defaultModel.uphill_A[WD_Ind]; // B = 0

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.UH_A_Adj[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.UH_A_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                This_Model_Adj_1.UH_A_Adj[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0.2f, 2, 0.2f, WD_Ind, "Uphill A", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.UH_A_Adj[WD_Ind], This_Model_Adj_2.UH_A_Adj[WD_Ind], 0.00001, "Wrong UH A coeff. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 3
            // UW Critical Exposure. Sweep params from 1 to 40 with interval = 4.875
            Mid_Int = Convert.ToInt16((((40 - 1) / 4.875) + 1) / 2) - 1;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 9; i++)
                {
                    double Val = 4.875 * i + 1;
                    This_Model_Adj_1.UW_Crit[WD_Ind] = Val;

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.UW_Crit[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.UW_Crit[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                This_Model_Adj_1.UW_Crit[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 1f, 40, 4.875f, WD_Ind, "UW Critical", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.UW_Crit[WD_Ind], This_Model_Adj_2.UW_Crit[WD_Ind], 0.00001, "Wrong UW Critical. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 4
            // Speed-Up flow A. Sweep params from 0.65 to 1.5 with interval = 0.2
            Mid_Int = Convert.ToInt16((((1.5 - 0.65) / 0.2) + 1) / 2) - 1;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 5; i++)
                {
                    double Val = 0.2 * i + 0.65;
                    This_Model_Adj_1.SU_A_Adj[WD_Ind] = Val * Math.Abs(theseInitParams.SU) / defaultModel.spdUp_A[WD_Ind]; // B = 0;

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.SU_A_Adj[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.SU_A_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                This_Model_Adj_1.SU_A_Adj[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0.65f, 1.5, 0.2f, WD_Ind, "SpdUp A", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.SU_A_Adj[WD_Ind], This_Model_Adj_2.SU_A_Adj[WD_Ind], 0.00001, "Wrong Speed-Up A. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 5
            // Downhill Stability Factor. Sweep from 0 to 3 with interval = 0.5
            Mid_Int = Convert.ToInt16((((3 - 0) / 0.5) + 1) / 2) - 1;
            double Max_Int = 0;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;                                

                for (int i = 0; i < 7; i++)
                {
                    double Val = 0.5 * i;
                    This_Model_Adj_1.DH_Stab_A[WD_Ind] = Val;

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.DH_Stab_A[WD_Ind];

                    if (i == 6)
                        Max_Int = This_Model_Adj_1.DH_Stab_A[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.DH_Stab_A[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                if (Val_Min_1 == Max_Int)
                    Val_Min_1 = 5;

                This_Model_Adj_1.DH_Stab_A[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 3, 0.5f, WD_Ind, "DH Stability", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.DH_Stab_A[WD_Ind], This_Model_Adj_2.DH_Stab_A[WD_Ind], 0.00001, "Wrong Downhill Stability. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 6
            // Uphill Stability Factor. Sweep from 0 to 3 with interval = 0.5
            Mid_Int = Convert.ToInt16((((3 - 0) / 0.5) + 1) / 2) - 1;
            Max_Int = 0;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 7; i++)
                {
                    double Val = 0.5 * i;
                    This_Model_Adj_1.UH_Stab_A[WD_Ind] = Val;

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == 6)
                        Max_Int = This_Model_Adj_1.UH_Stab_A[WD_Ind];

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.UH_Stab_A[WD_Ind];

                    if (i == 6)
                        Max_Int = This_Model_Adj_1.UH_Stab_A[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.UH_Stab_A[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                if (Val_Min_1 == Max_Int)
                    Val_Min_1 = 5;

                This_Model_Adj_1.UH_Stab_A[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 3, 0.5f, WD_Ind, "UH Stability", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.UH_Stab_A[WD_Ind], This_Model_Adj_2.UH_Stab_A[WD_Ind], 0.00001, "Wrong Uphill Stability. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 7
            // Speed-Up Stability Factor. Sweep from 0 to 3 with interval = 0.5
            Mid_Int = Convert.ToInt16((((3 - 0) / 0.5) + 1) / 2) - 1;
            Max_Int = 0;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                if (WD_Ind == 2)
                    WD_Ind = WD_Ind;

                for (int i = 0; i < 7; i++)
                {
                    double Val = 0.5 * i;
                    This_Model_Adj_1.SU_Stab_A[WD_Ind] = Val;

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.SU_Stab_A[WD_Ind];

                    if (i == 6)
                        Max_Int = This_Model_Adj_1.SU_Stab_A[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.SU_Stab_A[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                if (Val_Min_1 == Max_Int)
                    Val_Min_1 = 5;

                This_Model_Adj_1.SU_Stab_A[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 3, 0.5f, WD_Ind, "SU Stability", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.SU_Stab_A[WD_Ind], This_Model_Adj_2.SU_Stab_A[WD_Ind], 0.00001, "Wrong Speed-Up Stability. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 8
            // Downhill flow B Factor. Sweep from 0 to 1.4 with interval = 0.2
            Mid_Int = Convert.ToInt16((((1.4 - 0) / 0.2) + 1) / 2) - 1;
            int counter = 0;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                double Val_Min_2 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                if (WD_Ind == 3)
                    WD_Ind = WD_Ind;

                for (int i = 0; i < 8; i++)
                {
                    double Val = 0.2 * i;

                    // Calculates the coefficient at average P10 Expo and then alters B to change the slope but keep magnitude at Avg P10 Expo fixed

                    double Avg_Coeff = This_Model_Adj_1.DH_A_Adj[WD_Ind] * defaultModel.downhill_A[WD_Ind] * Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (This_Model_Adj_1.DH_B_Adj[WD_Ind] * defaultModel.downhill_B[WD_Ind]));
                    This_Model_Adj_1.DH_B_Adj[WD_Ind] = Val;
                    This_Model_Adj_1.DH_A_Adj[WD_Ind] = Avg_Coeff / defaultModel.downhill_A[WD_Ind] / (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (defaultModel.downhill_B[WD_Ind] * This_Model_Adj_1.DH_B_Adj[WD_Ind])));
                    if (counter == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.DH_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.DH_B_Adj[WD_Ind];
                    }

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.DH_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.DH_B_Adj[WD_Ind];
                    }

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.DH_A_Adj[WD_Ind];
                        Val_Min_2 = This_Model_Adj_1.DH_B_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                {
                    Val_Min_1 = Mid_Val_1;
                    Val_Min_2 = Mid_Val_2;
                }

                This_Model_Adj_1.DH_A_Adj[WD_Ind] = Val_Min_1;
                This_Model_Adj_1.DH_B_Adj[WD_Ind] = Val_Min_2;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 1.4, 0.2f, WD_Ind, "Downhill B", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.DH_A_Adj[WD_Ind], This_Model_Adj_2.DH_A_Adj[WD_Ind], 0.00001, "Wrong Downhill A Test 8. WD_Ind = " + WD_Ind.ToString());
                Assert.AreEqual(This_Model_Adj_1.DH_B_Adj[WD_Ind], This_Model_Adj_2.DH_B_Adj[WD_Ind], 0.00001, "Wrong Downhill B Test 8. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 9
            // Uphill flow B Factor. Sweep from 0 to 1.4 with interval = 0.2
            Mid_Int = Convert.ToInt16((((1.4 - 0) / 0.2) + 1) / 2) - 1;
            counter = 0;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                double Val_Min_2 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 8; i++)
                {
                    double Val = 0.2 * i;

                    // Calculates the coefficient at average P10 Expo and then alters B to change the slope but keep magnitude at Avg P10 Expo fixed

                    double Avg_Coeff = This_Model_Adj_1.UH_A_Adj[WD_Ind] * defaultModel.uphill_A[WD_Ind] * Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (This_Model_Adj_1.UH_B_Adj[WD_Ind] * defaultModel.uphill_B[WD_Ind]));
                    This_Model_Adj_1.UH_B_Adj[WD_Ind] = Val;
                    This_Model_Adj_1.UH_A_Adj[WD_Ind] = Avg_Coeff / defaultModel.uphill_A[WD_Ind] / (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (defaultModel.uphill_B[WD_Ind] * This_Model_Adj_1.UH_B_Adj[WD_Ind])));

                    if (counter == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.UH_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.UH_B_Adj[WD_Ind];
                    }

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.UH_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.UH_B_Adj[WD_Ind];
                    }

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.UH_A_Adj[WD_Ind];
                        Val_Min_2 = This_Model_Adj_1.UH_B_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                {
                    Val_Min_1 = Mid_Val_1;
                    Val_Min_2 = Mid_Val_2;
                }

                This_Model_Adj_1.UH_A_Adj[WD_Ind] = Val_Min_1;
                This_Model_Adj_1.UH_B_Adj[WD_Ind] = Val_Min_2;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 1.4, 0.2f, WD_Ind, "Uphill B", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.UH_A_Adj[WD_Ind], This_Model_Adj_2.UH_A_Adj[WD_Ind], 0.00001, "Wrong Uphill A Test 9. WD_Ind = " + WD_Ind.ToString());
                Assert.AreEqual(This_Model_Adj_1.UH_B_Adj[WD_Ind], This_Model_Adj_2.UH_B_Adj[WD_Ind], 0.00001, "Wrong Uphill B Test 9. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 10
            // Speed-Up flow B Factor. Sweep from 0 to 1.4 with interval = 0.2
            Mid_Int = Convert.ToInt16((((1.4 - 0) / 0.2) + 1) / 2) - 1;
            counter = 0;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                double Val_Min_2 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 8; i++)
                {
                    double Val = 0.2 * i;

                    // Calculates the coefficient at average P10 Expo and then alters B to change the slope but keep magnitude at Avg P10 Expo fixed

                    double Avg_Coeff = This_Model_Adj_1.SU_A_Adj[WD_Ind] * defaultModel.uphill_A[WD_Ind] * Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (This_Model_Adj_1.SU_B_Adj[WD_Ind] * defaultModel.spdUp_B[WD_Ind]));
                    This_Model_Adj_1.SU_B_Adj[WD_Ind] = Val;
                    This_Model_Adj_1.SU_A_Adj[WD_Ind] = Avg_Coeff / defaultModel.uphill_A[WD_Ind] / (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (defaultModel.spdUp_B[WD_Ind] * This_Model_Adj_1.SU_B_Adj[WD_Ind])));

                    if (counter == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.SU_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.SU_B_Adj[WD_Ind];
                    }

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.SU_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.SU_B_Adj[WD_Ind];
                    }

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.SU_A_Adj[WD_Ind];
                        Val_Min_2 = This_Model_Adj_1.SU_B_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                {
                    Val_Min_1 = Mid_Val_1;
                    Val_Min_2 = Mid_Val_2;
                }

                This_Model_Adj_1.SU_A_Adj[WD_Ind] = Val_Min_1;
                This_Model_Adj_1.SU_B_Adj[WD_Ind] = Val_Min_2;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 1.4, 0.2f, WD_Ind, "SpdUp B", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.SU_A_Adj[WD_Ind], This_Model_Adj_2.SU_A_Adj[WD_Ind], 0.00001, "Wrong Speed-Up A Test 10. WD_Ind = " + WD_Ind.ToString());
                Assert.AreEqual(This_Model_Adj_1.SU_B_Adj[WD_Ind], This_Model_Adj_2.SU_B_Adj[WD_Ind], 0.00001, "Wrong Speed-Up B Test 10. WD_Ind = " + WD_Ind.ToString());
            }

            thisInst.Close();
        }
    }
}
