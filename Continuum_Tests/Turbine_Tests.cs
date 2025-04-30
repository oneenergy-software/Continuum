using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;
using System.Windows.Forms;

namespace Continuum_Tests
{
    [TestClass]
    public class Turbine_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;

        public Turbine_Tests()
        {
            testingFolder = globals.testingFolder + "Turbine";
        }

        [TestMethod]
        public void DoWS_EstAlongNodes_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            double thisUTMX = 823500;
            double thisUTMY = 4500400;
            int numWD = 16;
            NodeCollection nodeList = new NodeCollection();

            string Filename = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(Filename);
            thisInst.turbineList.ClearAllTurbines();
            thisInst.turbineList.AddTurbine("Test", thisUTMX, thisUTMY, 1);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 4000, 1.0f, 1);
            double[] windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), thisUTMX, thisUTMY, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

            thisInst.turbineList.turbineEsts[0].gridStats.GetGridArrayAndCalcStats(thisInst.turbineList.turbineEsts[0].UTMX, thisInst.turbineList.turbineEsts[0].UTMY, thisInst, nodeList);
            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            Met thisMet = thisInst.metList.metItem[0];
            Turbine.WS_Ests New_WS_Est = new Turbine.WS_Ests();
            New_WS_Est.predictorMetName = thisMet.name;
            New_WS_Est.model = models[0];
                        
            thisInst.turbineList.turbineEsts[0].AddWS_Estimate(New_WS_Est);
            Nodes startNode = nodeList.GetMetNode(thisMet);
            Nodes targetNode = nodeList.GetTurbNode(thisInst.turbineList.turbineEsts[0]);
            
            Nodes[] pathOfNodes = nodeList.FindPathOfNodes(startNode, targetNode, models[0], thisInst);            
            Turbine.WS_Ests thisWS_Est = thisInst.turbineList.turbineEsts[0].DoWS_EstAlongNodes(thisMet, models[0], pathOfNodes, thisInst, windRose);
            
            Assert.AreNotEqual(thisWS_Est.sectorWS_at_nodes[0, 0], 0, 0, "Didn't calculate WS at Node 1");
            Assert.AreNotEqual(thisWS_Est.sectorWS_at_nodes[1, 0], 0, 0, "Didn't calculate WS at Node 2");
            Assert.AreNotEqual(thisWS_Est.sectorWS_at_nodes[2, 0], 0, 0, "Didn't calculate WS at Node 3");

            Assert.AreNotEqual(thisWS_Est.sectorWS_at_nodes[0, 8], 0, 0, "Didn't calculate WS at Node 1");
            Assert.AreNotEqual(thisWS_Est.sectorWS_at_nodes[1, 8], 0, 0, "Didn't calculate WS at Node 2");
            Assert.AreNotEqual(thisWS_Est.sectorWS_at_nodes[2, 8], 0, 0, "Didn't calculate WS at Node 3");

            Assert.AreNotEqual(thisWS_Est.sectorWS_at_nodes[0, numWD - 1], 0, 0, "Didn't calculate WS at Node 1");
            Assert.AreNotEqual(thisWS_Est.sectorWS_at_nodes[1, numWD - 1], 0, 0, "Didn't calculate WS at Node 2");
            Assert.AreNotEqual(thisWS_Est.sectorWS_at_nodes[2, numWD - 1], 0, 0, "Didn't calculate WS at Node 3");

            Assert.AreNotEqual(thisWS_Est.WS_at_nodes[0], 0, 0, "Didn't calculate WS at Node 1");
            Assert.AreNotEqual(thisWS_Est.WS_at_nodes[1], 0, 0, "Didn't calculate WS at Node 1");
            Assert.AreNotEqual(thisWS_Est.WS_at_nodes[2], 0, 0, "Didn't calculate WS at Node 1");

            Assert.AreNotEqual(thisWS_Est.sectorWS[0], 0, 0, "Didn't calculate WS at Target site");
            Assert.AreNotEqual(thisWS_Est.sectorWS[numWD - 1], 0, 0, "Didn't calculate WS at Target site");
            Assert.AreNotEqual(thisWS_Est.WS, 0, 0, "Didn't calculate WS at Target site");


            thisInst.Close();
        }

        [TestMethod]
        public void ExportMetWeights()
        {
            // Used in GenerateAvgWSFromTABs
            Continuum thisInst = new Continuum("", false);
            string exportName = testingFolder + "\\Met Weights and WS Ests.csv";
            StreamWriter sw = new StreamWriter(exportName);

            string Filename = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(Filename);

            NodeCollection nodeList = new NodeCollection();
            Turbine thisTurb = thisInst.turbineList.turbineEsts[0]; // W1                        
            Turbine.WS_Ests[] wsEsts = thisTurb.WS_Estimate;
            int numWD = thisInst.metList.numWD;

            for (int i = 0; i < wsEsts.Length; i++)
            {
                sw.WriteLine(wsEsts[i].predictorMetName + "," + wsEsts[i].WS + "," + wsEsts[i].WS_weight);

                for (int j = 0; j < numWD; j++)
                    sw.WriteLine(j.ToString() + "," + wsEsts[i].sectorWS[j].ToString());

                sw.WriteLine();
            }

            sw.Close();
            thisInst.Close();
        }

        [TestMethod]
        public void GenerateAvgWSFromTABs_Test()
        {
            Continuum thisInst = new Continuum("", false);
            
            string Filename = testingFolder + "\\Turbine testing.cfm";
            string WSFile = testingFolder + "\\W1 Avg WS and Sect WS Ests.csv";
            thisInst.Open(Filename);
            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            int numWD = thisInst.metList.numWD;
            StreamReader sr = new StreamReader(WSFile);

            double thisWS = Convert.ToDouble(sr.ReadLine());
            Assert.AreEqual(thisInst.turbineList.turbineEsts[0].avgWS_Est[0].freeStream.WS, thisWS, 0.001, "Wrong average wind speed");

            for (int i = 0; i < numWD; i++)
            {
                thisWS = Convert.ToDouble(sr.ReadLine());
                Assert.AreEqual(thisInst.turbineList.turbineEsts[0].avgWS_Est[0].freeStream.sectorWS[i], thisWS, 0.001, "Wrong average wind speed");
            }

            sr.Close();
            thisInst.Close();


        }                

        [TestMethod]
        public void DoTurbineCalcs_Test()
        {
            Continuum thisInst = new Continuum("", false);
            
            string Filename = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(Filename);

            thisInst.turbineList.ClearAllWSEsts();
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);

            for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)    
                thisInst.turbineList.turbineEsts[i].DoTurbineCalcs(thisInst, models); 

            for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
            {
                Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                Assert.AreNotSame(thisTurb.WS_Estimate, null, "Didn't calculate wind speed estimates");
                Assert.AreNotSame(thisTurb.gridStats, null, "Didn't calculate grid stats");
                Assert.AreEqual(thisTurb.WSEst_Count, 12, 0, "Didn't calculate 12 wind speed estimates (3 mets, 4 radii");
                
                for (int j = 0; j < thisTurb.WSEst_Count; j++)
                {
                    Assert.AreNotSame(thisTurb.WS_Estimate[j].predictorMetName, "", "Didn't save pred met name");
                    Assert.AreNotSame(thisTurb.WS_Estimate[j].sectorWS, null, "Didn't save sectorwise wind speeds");
                    Assert.AreNotSame(thisTurb.WS_Estimate[j].model, null, "Didn't save model");
                    Assert.AreNotEqual(thisTurb.WS_Estimate[j].WS, 0, 0, "Didn't save wind speed");
                }                
            }

            thisInst.Close();
        }

        [TestMethod]
        public void CalcTurbineWakeLosses_Test()
        {
            Continuum thisInst = new Continuum("", false);            

            string Filename = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(Filename);

            thisInst.wakeModelList.AddWakeModel(0, 5, 12, thisInst.turbineList.GetPowerCurve("GW 87/1500 1.205 kg/m^3,,"), 10, 3, 0.05f, "Linear");
                        
            int minDist = 10000000;
            int maxDist = 0;

            for (int i = 0; i <= thisInst.turbineList.TurbineCount - 1; i++)
            {
                int[] Min_Max_Dist = thisInst.turbineList.CalcMinMaxDistanceToTurbines(thisInst.turbineList.turbineEsts[i].UTMX, thisInst.turbineList.turbineEsts[i].UTMY);
                if (Min_Max_Dist[0] < minDist) minDist = Min_Max_Dist[0]; // this is min distance to turbine but when WD is at a different angle (not in line with turbines) the X dist is less than this value so making this always equal to 2*RD
                if (Min_Max_Dist[1] > maxDist) maxDist = Min_Max_Dist[1];
            }

            minDist = (2 * 87);
            if (maxDist == 0) maxDist = 15000; // maxDist will be zero when there is only one turbine. Might be good to make this value constant
            WakeCollection.WakeLossCoeffs[] Wake_coeffs = thisInst.wakeModelList.GetWakeLossesCoeffs(minDist, maxDist, thisInst.wakeModelList.wakeModels[0], thisInst.metList);
                        
            thisInst.turbineList.turbineEsts[0].CalcTurbineWakeLosses(thisInst, Wake_coeffs, thisInst.wakeModelList.wakeModels[0]);

            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].AvgWSEst_Count, 0, "Didn't add average WS est");
            int avgWS_Ind = thisInst.turbineList.turbineEsts[0].AvgWSEst_Count - 1;            
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].avgWS_Est[avgWS_Ind].wakeModel, null, "Didn't save wake model");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].avgWS_Est[avgWS_Ind].waked.sectorWS, null, "Didn't save sectorwise wind speeds");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].avgWS_Est[avgWS_Ind].waked.WS, null, "Didn't save wind speeds");

            int Net_AEP_ind = thisInst.turbineList.turbineEsts[0].NetAEP_Count - 1;
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].wakeLoss, 0, "Didn't calculate wake loss");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].wakeModel, null, "Didn't save wake model");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].CF, null, "Didn't calculate CF");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].AEP, null, "Didn't calculate AEP est");
     //       Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].P90, 0, "Didn't calculate P90"); P90/P99 not currently calculated in net AEP
     //       Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].P99, 0, "Didn't calculate P99");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].sectorEnergy, null, "Didn't calculate sectorwise WS dists");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].sectorWakeLoss, null, "Didn't calculate sectorwise wake loss");

            thisInst.Close();
        }

        [TestMethod]
        public void GenerateAvgWSTimeSeries_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string fileName = testingFolder + "\\Turbine TS testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            NodeCollection nodeList = new NodeCollection();
            Nodes targetNode = nodeList.GetTurbNode(thisTurb);
            string MCP_Method = thisInst.Get_MCP_Method();
            TurbineCollection.PowerCurve powerCurve = new TurbineCollection.PowerCurve();
            Wake_Model wakeModel = new Wake_Model();

            ModelCollection.TimeSeries[] thisTS = thisInst.modelList.GenerateTimeSeries(thisInst, thisInst.metList.GetMetsUsed(), targetNode, powerCurve, wakeModel, null, MCP_Method);
            thisTurb.GenerateAvgWSTimeSeries(thisTS, thisInst, wakeModel, false, MCP_Method, false, powerCurve);

            Assert.AreEqual(thisTurb.avgWS_Est[0].freeStream.WS, 5.81393758, 0.01, "Wrong WS Test 1");
            Assert.AreEqual(thisTurb.avgWS_Est[0].freeStream.sectorWS[8], 4.78742580, 0.02, "Wrong WS Test 2");
            Assert.AreEqual(thisTurb.avgWS_Est[0].freeStream.sectorWS[10], 5.43308824, 0.02, "Wrong WS Test 3");
            Assert.AreEqual(thisTurb.avgWS_Est[0].freeStream.sectorWS[15], 5.9455375, 0.02, "Wrong WS Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcMonthlyWS_Values_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string fileName = testingFolder + "\\Turbine TS testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            NodeCollection nodeList = new NodeCollection();
            Nodes targetNode = nodeList.GetTurbNode(thisTurb);
            string MCP_Method = thisInst.Get_MCP_Method();
            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0]; ;
            Wake_Model wakeModel = new Wake_Model();

            ModelCollection.TimeSeries[] thisTS = thisInst.modelList.GenerateTimeSeries(thisInst, thisInst.metList.GetMetsUsed(), targetNode, powerCurve, wakeModel, null, MCP_Method);

            Turbine.MonthlyWS_Vals[] monthlyWS = thisTurb.CalcMonthlyWS_Values(thisTS, thisInst, "Freestream");

            Assert.AreEqual(monthlyWS[0].avgWS, 6.489662634, 0.01, "Wrong monthly WS Jan 2005 Test 1");
            Assert.AreEqual(monthlyWS[15].avgWS, 6.580793056, 0.01, "Wrong monthly WS Apr 2006 Test 2");
            Assert.AreEqual(monthlyWS[45].avgWS, 6.133737903, 0.01, "Wrong monthly WS Oct 2008 Test 3");
            Assert.AreEqual(monthlyWS[71].avgWS, 6.169447581, 0.01, "Wrong monthly WS Dec 2010 Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcYearlyValue_Test()
        {
            Continuum thisInst = new Continuum("", false);
            
            string fileName = testingFolder + "\\Turbine TS testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            Wake_Model wakeModel = thisInst.wakeModelList.wakeModels[0];

            double thisVal = thisTurb.CalcYearlyValue(2005, "Avg WS", null, new TurbineCollection.PowerCurve());
            Assert.AreEqual(thisVal, 5.73003025, 0.01, "Wrong Avg WS Test 1");

            thisVal = thisTurb.CalcYearlyValue(2007, "Gross AEP", wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 3820.63630, 0.01, "Wrong Gross AEP Test 2");

            thisVal = thisTurb.CalcYearlyValue(2008, "Net AEP", wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 3894.20687, 0.01, "Wrong Net AEP Test 3");

            thisVal = thisTurb.CalcYearlyValue(2010, "Net AEP", wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 3573.25519, 0.01, "Wrong Net AEP Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcLT_MonthlyValue_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            
            string fileName = testingFolder + "\\Turbine TS testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            Wake_Model wakeModel = thisInst.wakeModelList.wakeModels[0];

            // Test 1 Waked WS
            double thisVal = thisTurb.CalcLT_MonthlyValue("Avg WS", 1, wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 6.47765367, 0.01, "Wrong Avg WS Test 1");

            // Test 2 Freestream WS
            thisVal = thisTurb.CalcLT_MonthlyValue("Avg WS", 4, null, new TurbineCollection.PowerCurve());
            Assert.AreEqual(thisVal, 6.8392918, 0.01, "Wrong Avg WS Test 2");

            // Test 3
            thisVal = thisTurb.CalcLT_MonthlyValue("Gross AEP", 3, wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 420.44058, 0.1, "Wrong Gross Energy Test 3");

            // Test 4
            thisVal = thisTurb.CalcLT_MonthlyValue("Net AEP", 12, wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 367.22712, 0.1, "Wrong Net Energy Test 4");


            thisInst.Close();
        }

        [TestMethod]
        public void CalcWakedStDev_Test()
        {
            Continuum thisInst = new Continuum("", false);
            
            string fileName = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(fileName);

            Turbine turbine = new Turbine();
            turbine.UTMX = 704907;
            turbine.UTMY = 4532165;
            double utmX = 705207;
            double utmY = 4531965;

            double thisWS = 6;
            double thisP90SD = 0.2;

            double thisWakeP90SD = turbine.CalcWakedStDev(utmX, utmY, thisInst.turbineList.powerCurves[0], thisP90SD,
                thisWS, thisInst);

            Assert.AreEqual(thisWakeP90SD, 1.169216, 0.001, "Wrong waked SD for effective TI");

            thisInst.Close();

        }

        [TestMethod]
        public void CalcEffectiveTI_Test()
        {
            Continuum thisInst = new Continuum("", false);
            
            string fileName = testingFolder + "\\Turbine TS testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[1];
            Met thisMet = thisInst.metList.metItem[0];

            // Test 1 WD = 4 Wohler = 1
            string tiFile = testingFolder + "\\CalcEffectiveTI\\WD 4 TI Wohler 1.csv";
            StreamReader sr = new StreamReader(tiFile);
            double[] effectiveTI = thisTurb.CalcEffectiveTI(thisMet, 1.0, thisInst, thisInst.turbineList.powerCurves[0], 4, 1.0);

            for (int i = 0; i < effectiveTI.Length; i++)
            {
                double thisTI = 0;

                try
                {
                    thisTI = Convert.ToDouble(sr.ReadLine());
                    double thisDiff = thisTI - effectiveTI[i];
                    Assert.AreEqual(effectiveTI[i], thisTI, 0.001, "Wrong Effective TI Test 1 WD ind:" + i.ToString());
                }
                catch (Exception ex)
                {                    
                    Assert.AreEqual(effectiveTI[i], thisTI, 0.001, "Wrong Effective TI Test 1 WD ind:" + i.ToString());
                }
                
            }

            sr.Close();

            // Test 2 WD = 13 Wohler = 10
            tiFile = testingFolder + "\\CalcEffectiveTI\\WD 13 TI Wohler 10.csv";
            sr = new StreamReader(tiFile);
            effectiveTI = thisTurb.CalcEffectiveTI(thisMet, 10, thisInst, thisInst.turbineList.powerCurves[0], 13, 1.0);
            
            for (int i = 0; i < effectiveTI.Length; i++)
            {
                double thisTI = 0;

                try
                {
                    thisTI = Convert.ToDouble(sr.ReadLine());
                    if (i >= 10) // Reducing required difference. In Continuum, now using GetInterpPowerOrThrust to find Ct for Waked SD calcs. This isn't done in Excel so answers are slightly different in bins with low counts and avg WS that are off-center
                        Assert.AreEqual(effectiveTI[i], thisTI, 0.01, "Wrong Effective TI Test 2 WD ind:" + i.ToString());
                    else
                        Assert.AreEqual(effectiveTI[i], thisTI, 0.001, "Wrong Effective TI Test 2 WD ind:" + i.ToString());
                }
                catch (Exception ex)
                {
                    Assert.AreEqual(effectiveTI[i], thisTI, 0.001, "Wrong Effective TI Test 2 WD ind:" + i.ToString());
                }

            }

            sr.Close();

            thisInst.Close();
        }
    }
}
