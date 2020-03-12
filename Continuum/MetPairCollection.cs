using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class MetPairCollection
    {
        public Pair_Of_Mets[] metPairs; // List of met pairs
        public RR_WS_Ests[] roundRobinEsts;   // List of Round Robin estimates

        [Serializable()] public struct RR_WS_Ests
        {
            public string[] metsUsed;   // All mets used in Round Robin calcs
            public int metSubSize;   // Size of subset of mets            
            public Model[,] model;   // List of models [i, j]; i = model #, j = radius ind
            public Avg_Est[,] avgWS_Ests;   // Avg ests. at mets not used in model  i = predictee Met num j = Model #
            public string[,] metsInModel;   // Mets used in every model i = Num mets used j = Model num
            public double[] RMS_Err;   // RMS error of every model (i.e. average of all avgWS_Est(i,j) for each model [j], averaged over all predictee mets
            public double RMS_All;   // RMS error overall (i.e. average over all UW&DW models (or combinations of mets))
       //     public bool isWS_Weighted;            
        }

        [Serializable()] public struct Avg_Est
        {
            public string predictee;  // Met site being predicted in Round Robin
            public double avgWS;   // 
            public double estError;   // 
            public int WS_Count;   // Number of WS estimates that formed average
        }

        /// <summary>
        /// Holds inputs and results of Round Robin analysis 
        /// </summary>
        public struct RR_funct_obj
        {
            /// <summary> Models used in Round Robin /// </summary>
            public Model[] models; 

            /// <summary> Mets omitted from model. Using Turbine class to hold estimates. /// </summary>
            public Turbine[] omittedMets;

            /// <summary> Actual wind speeds at omitted met sites </summary>
            public double[] actualWS_AtMets;

            /// <summary> Wind speed estimate error at omitted met sites </summary>
            public double[] errsAtOmittedMets;            

            /// <summary> RMS error of Round Robin estimates </summary>
            public double errRMS;
        }

        public struct MinMax_Expos
        {
            public double avgP10Expo;
            public double minUW_Expo;
            public double maxUW_Expo;
            public double[] minUW_ExpoWD  ;
            public double[] maxUW_ExpoWD  ;

            public double minSumUWDW_Expo ;
            public double maxSumUWDW_Expo ;
            public double[] minSumUWDW_ExpoWD;
            public double[] maxSumUWDW_ExpoWD;

            public double[] minWS_WD;
            public double[] maxWS_WD;

            public double[] avgP10ExpoWD;
        }

        /// <summary>
        /// Adjusted model coefficients
        /// </summary>
        public struct Model_Adj
        {
            /// <summary> Downhill model parameter, A, multiplier by direction sector </summary>
            public double[] DH_A_Adj;
            /// <summary> Downhill model parameter, B, multiplier by direction sector </summary>
            public double[] DH_B_Adj;

            /// <summary> Uphill model parameter, A, multiplier by direction sector </summary>
            public double[] UH_A_Adj;
            /// <summary> Uphill model parameter, B, multiplier by direction sector </summary>
            public double[] UH_B_Adj;

            /// <summary> Speed-up model parameter, A, multiplier by direction sector </summary>
            public double[] SU_A_Adj;
            /// <summary> Speed-up model parameter, B, multiplier by direction sector </summary>
            public double[] SU_B_Adj;

            /// <summary> Critical UW exposure by direction sector </summary>
            public double[] UW_Crit;

            /// <summary> Flow separation model parameter, A, multiplier by direction sector </summary>
            public double[] sepA_DW_Adj;
            /// <summary> Flow separation model parameter, B, multiplier by direction sector </summary>
            public double[] sepB_DW_Adj;

            /// <summary> Flow separation model turbulent zone wind speed factor </summary>
            public double[] turbWS_Fact;
            /// <summary> Flow separation model critical UWDW exposure sum </summary>
            public double[] sepCrit;
            /// <summary> Flow separation model critical wind speed </summary>
            public double[] sepCritWS;

            /// <summary> Downhill roughness model stability parameter </summary>
            public double[] DH_Stab_A;
            /// <summary> Uphill roughness model stability parameter </summary>
            public double[] UH_Stab_A;
            /// <summary> Speed-up roughness model stability parameter </summary>
            public double[] SU_Stab_A;

          //  public double[] stabB;
        }

        /// <summary>
        /// Best-fit model coefficients found from regression analysis. Used as starting point for site calibration
        /// </summary>
        public struct Init_Params
        { 
            /// <summary> Downhill coefficient /// </summary>
            public double DH;
            /// <summary> Uphill coefficient /// </summary>
            public double UH;
            /// <summary> Speed-up coefficient /// </summary>
            public double SU;
            /// <summary> Flow separation model downhill coefficient /// </summary>
            public double FS_DW;
            /// <summary> Critical upwind exposure /// </summary>
            public double UW_Crit;
            /// <summary> Flow separation critical UWDW exposure sum /// </summary>
            public double sepCrit;
        }

        public int PairCount
        {
            get
            {
                if (metPairs == null)
                    return 0;
                else
                    return metPairs.Length;
            }
        }

        public int RoundRobinCount
        {
            get
            {
                if (roundRobinEsts == null)
                    return 0;
                else
                    return roundRobinEsts.Length;
            }
        }

        public Pair_Of_Mets GetPair_Of_Mets(Met met1, Met met2)
        {
            Pair_Of_Mets thisPair = new Pair_Of_Mets();

            for (int i = 0; i < PairCount; i++)
                if ((metPairs[i].met1.name == met1.name && metPairs[i].met2.name == met2.name) ||
                    (metPairs[i].met1.name == met2.name && metPairs[i].met2.name == met1.name))
                    thisPair = metPairs[i];

            return thisPair;
        }

        public RR_WS_Ests GetRoundRobinEst(int numMetsInModel, Continuum thisInst, string MCP_Method)
        {
            // Finds and returns RR_WS_Ests object with Round Robin analysis results using numMets
            RR_WS_Ests thisRR = new RR_WS_Ests();

            for (int i = 0; i < RoundRobinCount; i++)
            {
                if (roundRobinEsts[i].metSubSize == numMetsInModel)
                {
                    thisRR = roundRobinEsts[i];
                    break;
                }
            }
            
            if (thisRR.RMS_All == 0 && thisInst.metList.ThisCount > numMetsInModel)
            {
                int numModels = GetNumModelsForRoundRobin(thisInst.metList.ThisCount, numMetsInModel);
                string[,] metsForModels = GetAllCombos(thisInst.metList.GetMetsUsed(), thisInst.metList.ThisCount, numMetsInModel, numModels);
                RR_funct_obj[] RR_obj_coll = new RR_funct_obj[0];

                for (int i = 0; i <= numModels - 1; i++)
                {
                    string[] metsForThisModel = new string[numMetsInModel];

                    for (int j = 0; j <= numMetsInModel - 1; j++)
                        metsForThisModel[j] = metsForModels[j, i];                                       

                    RR_funct_obj thisRR_obj = DoRR_Calc(metsForThisModel, thisInst, thisInst.metList.GetMetsUsed(), MCP_Method);
                    Array.Resize(ref RR_obj_coll, i + 1);
                    RR_obj_coll[i] = thisRR_obj;
                                        
                }

                AddRoundRobinEst(RR_obj_coll, thisInst.metList.GetMetsUsed(), numMetsInModel, metsForModels, true, thisInst.metList);
            }

            for (int i = 0; i < RoundRobinCount; i++)
            {
                if (roundRobinEsts[i].metSubSize == numMetsInModel)
                {
                    thisRR = roundRobinEsts[i];
                    break;
                }
            }

            return thisRR;
        }

        public bool RR_DoneAlready(string[] metsUsed, int metSubSize, MetCollection metList)
        {
            // Returns false if Round Robin has already been calculated
            bool alreadyDone = false;
            
            for (int i = 0; i < RoundRobinCount; i++)
            {
                bool sameMets = metList.sameMets(metsUsed, roundRobinEsts[i].metsUsed);
                if (sameMets == true && metSubSize == roundRobinEsts[i].metSubSize)
                    alreadyDone = true;                
            }

            return alreadyDone;
        }

        public void ClearAll()
        {
            // Clears all met pairs and Round Robin estimates
            metPairs = null;
            roundRobinEsts = null;
        }                

        public void RemoveAllWithDeletedMets(MetCollection metList)
        {
            // Deletes met pairs with mets that have been deleted
            int numMets = metList.ThisCount;
            bool haveMet1 = false;
            bool haveMet2 = false;
            Pair_Of_Mets[] newMetPairs = null;
            int keepPairCount = 0;

            for (int i = 0; i < PairCount; i++)
            {
                haveMet1 = false;
                haveMet2 = false;
                for (int j = 0; j < numMets; j++)
                {
                    if (metPairs[i].met1.name == metList.metItem[j].name)
                        haveMet1 = true;

                    if (metPairs[i].met2.name == metList.metItem[j].name)
                        haveMet2 = true;
                    
                }

                if (haveMet1 == true && haveMet2 == true)
                {
                    Array.Resize(ref newMetPairs, keepPairCount + 1);
                    newMetPairs[keepPairCount] = metPairs[i];
                    keepPairCount++;
                }
            }

            metPairs = null;
            metPairs = newMetPairs;                     

        }

        public void RemoveAllExceptDefault()
        {
            // Delete wind speed estimates except for default model
            for (int i = 0; i < PairCount; i++)
                metPairs[i].RemoveWS_PredExceptDefault();
            
        }

        public void ClearRoundRobin()
        {
            // Clears all Round Robin estimates
            roundRobinEsts = null;
        }

        public void CreateMetPairs(Continuum thisInst)
        {
            // Creates a Pair_of_Mets for every combination of mets and adds to list
            int numMets = thisInst.metList.ThisCount;           
            int numRadii = thisInst.radiiList.ThisCount;

            if (numMets == 0 || numRadii == 0)
                return;

            for (int i = 0; i < numMets; i++)
            { 
                for (int j = i + 1; j < numMets; j++)
                {
                    bool havePairAlready = false;
                    Met met1 = thisInst.metList.metItem[i];
                    Met met2 = thisInst.metList.metItem[j];

                    for (int k = 0; k < PairCount; k++)
                    { 
                        if (((metPairs[k].met1.name == met1.name && metPairs[k].met2.name == met2.name)) ||
                           ((metPairs[k].met2.name == met1.name && metPairs[k].met1.name == met2.name)) )
                        {
                            havePairAlready = true;
                            break;
                        }
                    }

                    if (havePairAlready == false)
                        AddMetPair(met1, met2); 
                }
            }
            
        }

        public void AddMetPair(Met met1, Met met2)
        {
            // Adds met pair to list
            Array.Resize(ref metPairs, PairCount + 1);

            metPairs[PairCount - 1] = new Pair_Of_Mets();

            // To ensure that the same path of nodes is used whether it goes met1 to met2 or met2 to met1, always put westernmost Met as met1
            if (met1.UTMX < met2.UTMX)
            {
                metPairs[PairCount - 1].met1 = met1;
                metPairs[PairCount - 1].met2 = met2;
            }
            else {
                metPairs[PairCount - 1].met1 = met2;
                metPairs[PairCount - 1].met2 = met1;
            }
                       
        }

            
        public void Calc_MinMax_Expos(ref MinMax_Expos theseMinMax, double[] windRose, int radiusIndex, MetCollection metList, Model thisModel)
        {
            // Finds min/max UW expo (used to initialize UW crit), min/max sum of UWDW (used in flow separation model), avg P10 expo and min/max WS
                   
            int numWD = windRose.Length;
            double UW_Expo = 0;
            double DW_Expo = 0;
                        
            int numMetsUsed = thisModel.metsUsed.Length;           
            
            for (int i = 0; i < numMetsUsed; i++)
            {
                Met thisMet = metList.GetMet(thisModel.metsUsed[i]);
                Met.WSWD_Dist thisDist = thisMet.GetWS_WD_Dist(thisModel.height, thisModel.timeOfDay, thisModel.season);

                UW_Expo = thisMet.expo[radiusIndex].GetOverallValue(thisDist.windRose, "Expo", "UW");
                DW_Expo = thisMet.expo[radiusIndex].GetOverallValue(thisDist.windRose, "Expo", "DW");

                theseMinMax.avgP10Expo = theseMinMax.avgP10Expo + Math.Max(thisMet.gridStats.GetOverallP10(windRose, radiusIndex, "UW"), thisMet.gridStats.GetOverallP10(windRose, radiusIndex, "DW"));
                if (UW_Expo < theseMinMax.minUW_Expo)
                    theseMinMax.minUW_Expo = UW_Expo;
            
                if (UW_Expo > theseMinMax.maxUW_Expo) 
                    theseMinMax.maxUW_Expo = UW_Expo;                    

                if (UW_Expo + DW_Expo < theseMinMax.minSumUWDW_Expo) 
                    theseMinMax.minSumUWDW_Expo = UW_Expo + DW_Expo;                    

                if (UW_Expo + DW_Expo > theseMinMax.maxSumUWDW_Expo) 
                    theseMinMax.maxSumUWDW_Expo = UW_Expo + DW_Expo;                    

                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                {
                    if (thisDist.WS * thisDist.sectorWS_Ratio[WD_Ind] < theseMinMax.minWS_WD[WD_Ind])
                        theseMinMax.minWS_WD[WD_Ind] = thisDist.WS * thisDist.sectorWS_Ratio[WD_Ind];

                    if (thisDist.WS * thisDist.sectorWS_Ratio[WD_Ind] > theseMinMax.maxWS_WD[WD_Ind])
                        theseMinMax.maxWS_WD[WD_Ind] = thisDist.WS * thisDist.sectorWS_Ratio[WD_Ind];

                    theseMinMax.avgP10ExpoWD[WD_Ind] = theseMinMax.avgP10ExpoWD[WD_Ind] + Math.Max(thisMet.gridStats.stats[radiusIndex].P10_UW[WD_Ind], thisMet.gridStats.stats[radiusIndex].P10_DW[WD_Ind]);

                    if (thisMet.expo[radiusIndex].expo[WD_Ind] < theseMinMax.minUW_ExpoWD[WD_Ind])
                        theseMinMax.minUW_ExpoWD[WD_Ind] = thisMet.expo[radiusIndex].expo[WD_Ind];

                    if (thisMet.expo[radiusIndex].expo[WD_Ind] > theseMinMax.maxUW_ExpoWD[WD_Ind])
                        theseMinMax.maxUW_ExpoWD[WD_Ind] = thisMet.expo[radiusIndex].expo[WD_Ind];

                    if ((thisMet.expo[radiusIndex].expo[WD_Ind] + thisMet.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo")) < theseMinMax.minSumUWDW_ExpoWD[WD_Ind])
                        theseMinMax.minSumUWDW_ExpoWD[WD_Ind] = thisMet.expo[radiusIndex].expo[WD_Ind] + thisMet.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo");

                    if ((thisMet.expo[radiusIndex].expo[WD_Ind] + thisMet.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo")) > theseMinMax.maxSumUWDW_ExpoWD[WD_Ind])
                        theseMinMax.maxSumUWDW_ExpoWD[WD_Ind] = thisMet.expo[radiusIndex].expo[WD_Ind] + thisMet.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo");                        
                }
                
            }            

            theseMinMax.avgP10Expo = theseMinMax.avgP10Expo / numMetsUsed;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                theseMinMax.avgP10ExpoWD[WD_Ind] = theseMinMax.avgP10ExpoWD[WD_Ind] / numMetsUsed;
                if (theseMinMax.avgP10ExpoWD[WD_Ind] < 1) theseMinMax.avgP10ExpoWD[WD_Ind] = 1;
            }
            
            if (theseMinMax.avgP10Expo < 1) theseMinMax.avgP10Expo = 1;

        }

        /// <summary>
        /// Performs all wind speed cross-estimates for all met pairs with modified model
        /// Finds value that generates lowest error (sectorwise)
        /// </summary>
        /// <param name="thisInst"></param>
        /// <param name="minVal">Minimum value to sweep</param>
        /// <param name="maxVal">Maximum value to sweep</param>
        /// <param name="valInt">Interval size to sweep</param>
        /// <param name="WD_Ind">Wind direction index</param>
        /// <param name="iterType">Model parameter to sweep</param>
        /// <param name="model">Referenced model</param>
        /// <param name="adjModel">Adjusted model coefficients</param>
        /// <param name="maxSumUWDW_ExpoWD">Max UWDW exposure sum by wind direction</param>
        /// <param name="avgP10ExpoWD">Average P10 Exposure by wind direction</param>
        /// <param name="theseInit">Initial best-fit model parameters</param>
        public void Sweep_a_Param(Continuum thisInst, double minVal, double maxVal, double valInt, int WD_Ind, string iterType, ref Model model, Model_Adj adjModel, 
                                    double[] maxSumUWDW_ExpoWD, double[] avgP10ExpoWD, Init_Params theseInit)
        {
                        
            int numWD = thisInst.metList.numWD;                   
            double minError = 1000;            
            double lastError = 0;
            bool errorChanged = false;
         
            Model origModel = new Model();
            origModel.SizeArrays(numWD);
            origModel.SetDefaultModelCoeffs(numWD);
                       
            if (valInt == 0) return;
            valInt = Math.Round(valInt, 3);
            minVal = Math.Round(minVal, 3);
            maxVal = Math.Round(maxVal, 3);
                     
            double val1MinRMS = 0; // Parameter value which produced minimum RMS
            double val2MinRMS = 0; // Used when sweeping B parameters. The B param affects the slope of the log-log but it also changes the magnitude. In order to only sweep B, the A parameter
                                   // is modified to keep the magnitude at the average P10 exposure constant and only change the slope
            double midVal1 = 0; // Midpoint value
            double midVal2 = 0; // Used when sweeping B parameters. Midpoint of B parameter (midVal1 holds midpoint of A parameter when sweeping B)
            int midInt = Convert.ToInt16((((maxVal - minVal) / valInt) + 1) / 2) - 1;

            if (midInt < 0) midInt = 0;

            int counter = 0;

            for (double thisVal = minVal; thisVal <= maxVal; thisVal = Math.Round(thisVal + valInt,3))
            {
                if (iterType == "Downhill A")
                {
                    adjModel.DH_A_Adj[WD_Ind] = thisVal * theseInit.DH / origModel.downhill_A[WD_Ind] / (Math.Pow(avgP10ExpoWD[WD_Ind], (origModel.downhill_B[WD_Ind] * adjModel.DH_B_Adj[WD_Ind])));
                    if (counter == midInt)
                        midVal1 = adjModel.DH_A_Adj[WD_Ind];
                }
                else if (iterType == "Downhill B")
                {
                    double avgCoeff = adjModel.DH_A_Adj[WD_Ind] * origModel.downhill_A[WD_Ind] * Math.Pow(avgP10ExpoWD[WD_Ind], (adjModel.DH_B_Adj[WD_Ind] * origModel.downhill_B[WD_Ind])); // Coefficient using previous A & B. Keeps coeff const at avg P10 expo
                    adjModel.DH_B_Adj[WD_Ind] = thisVal;
                    adjModel.DH_A_Adj[WD_Ind] = avgCoeff / origModel.downhill_A[WD_Ind] / (Math.Pow(avgP10ExpoWD[WD_Ind], (origModel.downhill_B[WD_Ind] * adjModel.DH_B_Adj[WD_Ind])));
                    if (counter == midInt)
                    {
                        midVal1 = adjModel.DH_A_Adj[WD_Ind];
                        midVal2 = adjModel.DH_B_Adj[WD_Ind];
                    } 
                }
                else if (iterType == "Uphill A")
                {
                    adjModel.UH_A_Adj[WD_Ind] = thisVal * Math.Abs(theseInit.UH) / origModel.uphill_A[WD_Ind] / (Math.Pow(avgP10ExpoWD[WD_Ind], (origModel.uphill_B[WD_Ind] * adjModel.UH_B_Adj[WD_Ind])));
                    if (counter == midInt)
                        midVal1 = adjModel.UH_A_Adj[WD_Ind];
                }
                else if (iterType == "Uphill B")
                {
                    double avgCoeff = adjModel.UH_A_Adj[WD_Ind] * origModel.uphill_A[WD_Ind] * Math.Pow(avgP10ExpoWD[WD_Ind], (adjModel.UH_B_Adj[WD_Ind] * origModel.uphill_B[WD_Ind]));
                    adjModel.UH_B_Adj[WD_Ind] = thisVal;
                    adjModel.UH_A_Adj[WD_Ind] = avgCoeff / origModel.uphill_A[WD_Ind] / (Math.Pow(avgP10ExpoWD[WD_Ind], (origModel.uphill_B[WD_Ind] * adjModel.UH_B_Adj[WD_Ind])));
                    if (counter == midInt)
                    {
                        midVal1 = adjModel.UH_A_Adj[WD_Ind];
                        midVal2 = adjModel.UH_B_Adj[WD_Ind];
                    } 
                }
                else if (iterType == "UW Critical")
                {
                    adjModel.UW_Crit[WD_Ind] = thisVal;
                    if (counter == midInt)
                        midVal1 = adjModel.UW_Crit[WD_Ind];
                }
                else if (iterType == "SpdUp A")
                {
                    adjModel.SU_A_Adj[WD_Ind] = thisVal * Math.Abs(theseInit.SU) / origModel.spdUp_A[WD_Ind] / (Math.Pow(avgP10ExpoWD[WD_Ind], (origModel.spdUp_B[WD_Ind] * adjModel.SU_B_Adj[WD_Ind])));
                    if (counter == midInt)
                        midVal1 = adjModel.SU_A_Adj[WD_Ind];
                }
                else if (iterType == "SpdUp B")
                {
                    double avgCoeff = adjModel.SU_A_Adj[WD_Ind] * origModel.spdUp_A[WD_Ind] * Math.Pow(avgP10ExpoWD[WD_Ind], (adjModel.SU_B_Adj[WD_Ind] * origModel.spdUp_B[WD_Ind]));
                    adjModel.SU_B_Adj[WD_Ind] = thisVal;
                    adjModel.SU_A_Adj[WD_Ind] = avgCoeff / origModel.spdUp_A[WD_Ind] / (Math.Pow(avgP10ExpoWD[WD_Ind], (origModel.spdUp_B[WD_Ind] * adjModel.SU_B_Adj[WD_Ind])));
                    if (counter == midInt)
                    {
                        midVal1 = adjModel.SU_A_Adj[WD_Ind];
                        midVal2 = adjModel.SU_B_Adj[WD_Ind];
                    } 
                }
                else if (iterType == "DH Stability")
                {
                    adjModel.DH_Stab_A[WD_Ind] = thisVal;
                    if (counter == midInt)
                        midVal1 = adjModel.DH_Stab_A[WD_Ind];
                }
                else if (iterType == "UH Stability")
                {
                    adjModel.UH_Stab_A[WD_Ind] = thisVal;
                    if (counter == midInt)
                        midVal1 = adjModel.UH_Stab_A[WD_Ind];
                }
                else if (iterType == "SU Stability")
                {
                    adjModel.SU_Stab_A[WD_Ind] = thisVal;
                    if (counter == midInt)
                        midVal1 = adjModel.SU_Stab_A[WD_Ind];
                }
             /*   else if (iterType == "Stability B")
                {
                    adjModel.stabB[WD_Ind] = thisVal;
                    if (counter == midInt)
                        midVal1 = adjModel.stabB[WD_Ind];
                } */
                else if (iterType == "Sep DW A")
                {
                    adjModel.sepA_DW_Adj[WD_Ind] = thisVal * Math.Abs(theseInit.FS_DW) / origModel.sep_A_DW[WD_Ind] / (Math.Pow(avgP10ExpoWD[WD_Ind], (origModel.sep_B_DW[WD_Ind] * adjModel.sepB_DW_Adj[WD_Ind])));
                    if (counter == midInt)
                        midVal1 = adjModel.sepA_DW_Adj[WD_Ind];
                }
                else if (iterType == "Sep DW B")
                {
                    double avgCoeff = adjModel.sepA_DW_Adj[WD_Ind] * origModel.sep_A_DW[WD_Ind] * Math.Pow(avgP10ExpoWD[WD_Ind], (adjModel.sepB_DW_Adj[WD_Ind] * origModel.sep_B_DW[WD_Ind]));
                    adjModel.sepB_DW_Adj[WD_Ind] = thisVal;
                    adjModel.sepA_DW_Adj[WD_Ind] = avgCoeff / origModel.sep_A_DW[WD_Ind] / (Math.Pow(avgP10ExpoWD[WD_Ind], (origModel.sep_B_DW[WD_Ind] * adjModel.sepB_DW_Adj[WD_Ind])));
                       if (counter == midInt)
                       {
                           midVal1 = adjModel.sepA_DW_Adj[WD_Ind];
                           midVal2 = adjModel.sepB_DW_Adj[WD_Ind];
                       } 
                }
                else if (iterType == "Turb WS factor")
                {
                    adjModel.turbWS_Fact[WD_Ind] = thisVal;
                    if (counter == midInt)
                        midVal1 = adjModel.turbWS_Fact[WD_Ind];
                }
                else if (iterType == "Flow Sep Critical")
                {
                    adjModel.sepCrit[WD_Ind] = thisVal;
                    if (counter == midInt)
                        midVal1 = adjModel.sepCrit[WD_Ind];
                }
                else if (iterType == "Sep Crit WS")
                {
                    adjModel.sepCritWS[WD_Ind] = thisVal;
                    if (counter == midInt)
                       midVal1 = adjModel.sepCritWS[WD_Ind];
                }

                double overallRMS_err = SweepGetRMSWithAdjModel(ref model, adjModel, thisInst); // The model is modified in this function. Overall RMS error is not used for finding best value
                double RMS_Err = GetRMS_SectorErr(model, WD_Ind);

                if (lastError != 0 && lastError != RMS_Err)
                    errorChanged = true;

                if (RMS_Err < minError)
                {
                    if (iterType == "Downhill A")
                        val1MinRMS = adjModel.DH_A_Adj[WD_Ind];
                    else if (iterType == "Downhill B")
                    {
                        val1MinRMS = adjModel.DH_A_Adj[WD_Ind];
                        val2MinRMS = adjModel.DH_B_Adj[WD_Ind];
                    }
                    else if (iterType == "Uphill A")
                        val1MinRMS = adjModel.UH_A_Adj[WD_Ind];
                    else if (iterType == "Uphill B")
                    {
                        val1MinRMS = adjModel.UH_A_Adj[WD_Ind];
                        val2MinRMS = adjModel.UH_B_Adj[WD_Ind];
                    }
                    else if (iterType == "SpdUp A")
                        val1MinRMS = adjModel.SU_A_Adj[WD_Ind];
                    else if (iterType == "SpdUp B")
                    {
                        val1MinRMS = adjModel.SU_A_Adj[WD_Ind];
                        val2MinRMS = adjModel.SU_B_Adj[WD_Ind];
                    }
                    else if (iterType == "UW Critical")
                        val1MinRMS = adjModel.UW_Crit[WD_Ind];
                    else if (iterType == "DH Stability")
                        val1MinRMS = adjModel.DH_Stab_A[WD_Ind];
                    else if (iterType == "UH Stability")
                        val1MinRMS = adjModel.UH_Stab_A[WD_Ind];
                    else if (iterType == "SU Stability")
                        val1MinRMS = adjModel.SU_Stab_A[WD_Ind];
                 //   else if (iterType == "Stability B")
                 //       val1MinRMS = adjModel.stabB[WD_Ind];
                    else if (iterType == "Sep DW A")
                        val1MinRMS = adjModel.sepA_DW_Adj[WD_Ind];
                    else if (iterType == "Sep DW B")
                    {
                        val1MinRMS = adjModel.sepA_DW_Adj[WD_Ind];
                        val2MinRMS = adjModel.sepB_DW_Adj[WD_Ind];
                    }
                    else if (iterType == "Turb WS factor")
                        val1MinRMS = adjModel.turbWS_Fact[WD_Ind];  
                    else if (iterType == "Flow Sep Critical")
                        val1MinRMS = adjModel.sepCrit[WD_Ind];
                    if (iterType == "Sep Crit WS")
                        val1MinRMS = adjModel.sepCritWS[WD_Ind];

                    minError = RMS_Err;
                }

                lastError = RMS_Err;
                counter++;
            }

            if (errorChanged == false)
            {
                val1MinRMS = midVal1;
                val2MinRMS = midVal2;
            }
            
            if (iterType == "UW Critical")
                adjModel.UW_Crit[WD_Ind] = val1MinRMS;
            else if (iterType == "Flow Sep Critical")
            {
                if (val1MinRMS >= maxSumUWDW_ExpoWD[WD_Ind])
                    adjModel.sepCrit[WD_Ind] = 1000;
                else
                    adjModel.sepCrit[WD_Ind] = val1MinRMS;
            }
            else if (iterType == "Sep Crit WS")
                adjModel.sepCritWS[WD_Ind] = val1MinRMS;
            else if (iterType == "Downhill A")
                adjModel.DH_A_Adj[WD_Ind] = val1MinRMS;
            else if (iterType == "Downhill B")
            {
                adjModel.DH_A_Adj[WD_Ind] = val1MinRMS;
                adjModel.DH_B_Adj[WD_Ind] = val2MinRMS;
            }
            else if (iterType == "Uphill A")
                adjModel.UH_A_Adj[WD_Ind] = val1MinRMS;
            else if (iterType == "Uphill B")
            {
                adjModel.UH_A_Adj[WD_Ind] = val1MinRMS;
                adjModel.UH_B_Adj[WD_Ind] = val2MinRMS;
            }
            else if (iterType == "SpdUp A")
                adjModel.SU_A_Adj[WD_Ind] = val1MinRMS;
            else if (iterType == "SpdUp B")
            {
                adjModel.SU_A_Adj[WD_Ind] = val1MinRMS;
                adjModel.SU_B_Adj[WD_Ind] = val2MinRMS;
            }
            else if (iterType == "Sep DW A")
                adjModel.sepA_DW_Adj[WD_Ind] = val1MinRMS;
            else if (iterType == "Sep DW B")
            {
                adjModel.sepA_DW_Adj[WD_Ind] = val1MinRMS;
                adjModel.sepB_DW_Adj[WD_Ind] = val2MinRMS;
            }
            else if (iterType == "Turb WS factor")
                adjModel.turbWS_Fact[WD_Ind] = val1MinRMS;
            else if (iterType == "DH Stability")
            {
                if (val1MinRMS == maxVal)
                    val1MinRMS = 5;
                adjModel.DH_Stab_A[WD_Ind] = val1MinRMS;
            }
            else if (iterType == "UH Stability")
            {
                if (val1MinRMS == maxVal)
                    val1MinRMS = 5;
                adjModel.UH_Stab_A[WD_Ind] = val1MinRMS;
            }
            else if (iterType == "SU Stability")
            {
                if (val1MinRMS == maxVal)
                    val1MinRMS = 5;
                adjModel.SU_Stab_A[WD_Ind] = val1MinRMS;
            }
            
        }
         
        public void SizeAdjModel(ref Model_Adj thisAdjModel, int numWD)
        {
            // Sizes arrays in modified wind flow model
            thisAdjModel.DH_A_Adj = new double[numWD];
            thisAdjModel.DH_B_Adj = new double[numWD];
            thisAdjModel.UH_A_Adj = new double[numWD];
            thisAdjModel.UH_B_Adj = new double[numWD];
            thisAdjModel.SU_A_Adj = new double[numWD];
            thisAdjModel.SU_B_Adj = new double[numWD];
            thisAdjModel.UW_Crit = new double[numWD];

            thisAdjModel.sepA_DW_Adj = new double[numWD];
            thisAdjModel.sepB_DW_Adj = new double[numWD];
            thisAdjModel.turbWS_Fact = new double[numWD];
            thisAdjModel.sepCrit = new double[numWD];
            thisAdjModel.sepCritWS = new double[numWD];

            thisAdjModel.DH_Stab_A = new double[numWD];
            thisAdjModel.UH_Stab_A = new double[numWD];
            thisAdjModel.SU_Stab_A = new double[numWD];
          //  thisAdjModel.stabB = new double[numWD];
        }

        public void FindBestInitCoeffs(Model thisModel, ref Init_Params theseInitParams, ref double[] initCoeffs, MetCollection metList, int radiusIndex, MinMax_Expos theseMinMax)
        {
            // if more than three mets used in model, finds the overall UW and DW exposure and fits a linear regression. if R^2 < 0.7 or if mdw < 0 and muw > 0 then uses default model.
            int WS_ind = 0;
            int numMetsUsed = thisModel.metsUsed.Length;
            double[,] UW_DW_array = new double[numMetsUsed, 3];
            double[] WS_array = new double[numMetsUsed];

            if (numMetsUsed > 3)
            {
                for (int i = 0; i < metList.ThisCount; i++)
                {
                    for (int j = 0; j < numMetsUsed; j++)
                    {
                        if (metList.metItem[i].name == thisModel.metsUsed[j])
                        {
                            Met.WSWD_Dist thisDist = metList.metItem[i].GetWS_WD_Dist(thisModel.height, thisModel.timeOfDay, thisModel.season);
                            UW_DW_array[WS_ind, 0] = 1;
                            UW_DW_array[WS_ind, 1] = metList.metItem[i].expo[radiusIndex].GetOverallValue(thisDist.windRose, "Expo", "UW");
                            UW_DW_array[WS_ind, 2] = metList.metItem[i].expo[radiusIndex].GetOverallValue(thisDist.windRose, "Expo", "DW");
                            WS_array[WS_ind] = thisDist.WS;
                            WS_ind++;
                            break;
                        }
                    }
                }

                double defaultDH = thisModel.downhill_A[0] * Math.Pow(theseMinMax.avgP10Expo, thisModel.downhill_B[0]);
                double defaultUH = -thisModel.uphill_A[0] * Math.Pow(theseMinMax.avgP10Expo, thisModel.uphill_B[0]);
                double defaultSU = thisModel.spdUp_A[0] * Math.Pow(theseMinMax.avgP10Expo,  thisModel.spdUp_B[0]);
                double defaultFS_DW = thisModel.sep_A_DW[0] * Math.Pow(theseMinMax.avgP10Expo,  thisModel.sep_B_DW[0]);

                initCoeffs = FindInitCoeffs(UW_DW_array, WS_array);

                if ((initCoeffs[1] > 0 && initCoeffs[2] < 0) || initCoeffs[3] < 0.7)
                {
                    theseInitParams.DH = thisModel.downhill_A[0] * Math.Pow(theseMinMax.avgP10Expo, thisModel.downhill_B[0]);
                    theseInitParams.UH = thisModel.uphill_A[0] * Math.Pow(theseMinMax.avgP10Expo, thisModel.uphill_B[0]);
                    theseInitParams.SU = thisModel.spdUp_A[0] * Math.Pow(theseMinMax.avgP10Expo, thisModel.spdUp_B[0]);
                    theseInitParams.FS_DW = thisModel.sep_A_DW[0] * Math.Pow(theseMinMax.avgP10Expo, thisModel.sep_B_DW[0]);
                    theseInitParams.UW_Crit = Convert.ToSingle(Math.Max(1, theseMinMax.minUW_Expo - theseMinMax.minUW_Expo * 0.1));
                    theseInitParams.sepCrit = Convert.ToSingle(Math.Max(150, theseMinMax.maxSumUWDW_Expo));
                }
                else
                {                    
                    if (initCoeffs[1] > 0)
                    {
                        theseInitParams.SU = initCoeffs[1];
                        theseInitParams.UH = defaultUH;
                        theseInitParams.UW_Crit = theseMinMax.maxUW_Expo + theseMinMax.maxUW_Expo * 0.1f;
                    }
                    else
                    {
                        theseInitParams.UH = initCoeffs[1];
                        theseInitParams.SU = defaultSU;
                        theseInitParams.UW_Crit = Convert.ToSingle(Math.Max(1, theseMinMax.minUW_Expo - theseMinMax.minUW_Expo * 0.1));
                    }

                    theseInitParams.DH = initCoeffs[2];

                    if (theseInitParams.DH < 0)
                    { // flow separation
                        theseInitParams.DH = defaultDH;
                        theseInitParams.FS_DW = initCoeffs[2];
                    }
                    else
                    {
                        theseInitParams.DH = initCoeffs[2];
                        theseInitParams.FS_DW = defaultFS_DW;
                    }
                    theseInitParams.sepCrit = Math.Max(150, theseMinMax.maxSumUWDW_Expo);
                }
            }
            else
            {
                theseInitParams.DH = thisModel.downhill_A[0] * Math.Pow(theseMinMax.avgP10Expo, thisModel.downhill_B[0]);
                theseInitParams.UH = thisModel.uphill_A[0] * Math.Pow(theseMinMax.avgP10Expo, thisModel.uphill_B[0]);
                theseInitParams.SU = thisModel.spdUp_A[0] * Math.Pow(theseMinMax.avgP10Expo, thisModel.spdUp_B[0]);
                theseInitParams.FS_DW = thisModel.sep_A_DW[0] * Math.Pow(theseMinMax.avgP10Expo, thisModel.sep_B_DW[0]);
                theseInitParams.UW_Crit = Convert.ToSingle(Math.Max(1, theseMinMax.minUW_Expo - theseMinMax.minUW_Expo * 0.1));
                theseInitParams.sepCrit = Math.Max(150, theseMinMax.maxSumUWDW_Expo);
            }

        }
        

        public void InitMinMaxExpos(ref MinMax_Expos theseMinMax, int numWD)
        {
            // Initializes the Min/Max Exposure values used in site-calibration sweep
            theseMinMax.avgP10Expo = 0;
            theseMinMax.minUW_Expo = 1000;
            theseMinMax.maxUW_Expo = 0;
            theseMinMax.minSumUWDW_Expo = 1000;
            theseMinMax.maxSumUWDW_Expo = 0;

            theseMinMax.minUW_ExpoWD = new double[numWD];
            theseMinMax.maxUW_ExpoWD = new double[numWD];
            theseMinMax.minSumUWDW_ExpoWD = new double[numWD];
            theseMinMax.maxSumUWDW_ExpoWD = new double[numWD];
            theseMinMax.minWS_WD = new double[numWD];
            theseMinMax.maxWS_WD = new double[numWD];
            theseMinMax.avgP10ExpoWD = new double[numWD];

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                theseMinMax.minUW_ExpoWD[WD_Ind] = 1000;
                theseMinMax.maxUW_ExpoWD[WD_Ind] = 0;
                theseMinMax.minSumUWDW_ExpoWD[WD_Ind] = 1000;
                theseMinMax.maxSumUWDW_ExpoWD[WD_Ind] = 0;
                theseMinMax.minWS_WD[WD_Ind] = 1000;
                theseMinMax.maxWS_WD[WD_Ind] = 0;
            }
        }

        public void DoCoarseSweep(Continuum thisInst, Model thisModel, ref Model_Adj thisAdjModel, Init_Params theseInitParams, MinMax_Expos theseMinMax)
        {
            // Using coarse step sizes and broad ranges, this function: 
            // Sweeps downhill, uphill and speed-up //A coeffs// (which modifies the magnitude of the coeffs) and UW_crit
            // if surface roughness model is used, sweeps UH/DH/SU stability exponents
            // if flow separation model is used, sweeps critical UWDW, turbulent //A// coeff and turbulent zone WS factor (which affects the wind speed in the turbulent zone downwind of point of separation)
            // && finds coeffs that yield the lowest cross-prediction error and stores them as Model_Adj object

            double overallRMS_err;
            double RMS_Err;
            int numWD = thisInst.metList.numWD;

            // Coarse sweep
            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {                
                if (thisInst.topo.useSepMod == true) {
                    // 2) Sweep Flow Separation Crit
                    double sepLowLim = Math.Max(thisModel.minSumUWDW, theseMinMax.minSumUWDW_ExpoWD[WD_Ind] - theseMinMax.minSumUWDW_ExpoWD[WD_Ind] * 0.1);
                    double sepHighLim = theseMinMax.maxSumUWDW_ExpoWD[WD_Ind];

                    //   Sep_CritWS_WD[WD_Ind] = minWS_WD[WD_Ind]

                    Sweep_a_Param(thisInst, sepLowLim, sepHighLim * 1.1f, (sepHighLim * 1.1f - sepLowLim) / 8, WD_Ind, "Flow Sep Critical", ref thisModel, thisAdjModel,  
                              theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);
                }

                // 3) Downhill flow
                Sweep_a_Param(thisInst, 0.65f, 1.5, 0.2f, WD_Ind, "Downhill A", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                // 4) Uphill flow
                Sweep_a_Param(thisInst, 0.65f, 1.5, 0.2f, WD_Ind, "Uphill A", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                // 1) Sweep UW Crit
                double UW_LowLim = Convert.ToSingle(Math.Max(1, theseMinMax.minUW_ExpoWD[WD_Ind] - theseMinMax.minUW_ExpoWD[WD_Ind] * 0.1));
                double UW_UpperLim = theseMinMax.maxUW_ExpoWD[WD_Ind] * 1.1f;
                if (UW_UpperLim < 1.5) UW_UpperLim = 1.5;
                Sweep_a_Param(thisInst, UW_LowLim, UW_UpperLim, (UW_UpperLim - UW_LowLim) / 8, WD_Ind, "UW Critical", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                // 5) Sweep UW Spd-up A 
                Sweep_a_Param(thisInst, 0.65f, 1.5f, 0.2f, WD_Ind, "SpdUp A", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                if (thisInst.topo.useSR == true)
                {
                    // 9) Sweep Stability factor for downhill flow 
                    Sweep_a_Param(thisInst, 0, 3, 0.5f, WD_Ind, "DH Stability", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                    // 10) Sweep Stability factor for uphill flow 
                    Sweep_a_Param(thisInst, 0, 3, 0.5f, WD_Ind, "UH Stability", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                    // 9) Sweep Stability factor for speed-up flow 
                    Sweep_a_Param(thisInst, 0, 3, 0.5f, WD_Ind, "SU Stability", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);
                }

                if (thisInst.topo.useSepMod == true && thisAdjModel.sepCrit[WD_Ind] != 1000)
                {
                    // 6) Sweep Flow Separation Crit WS
                    //   Dim SepWS_Low_lim  double
                    //   Dim SepWS_High_lim  double
                    //   SepWS_Low_lim = minWS_WD[WD_Ind]
                    //   SepWS_High_lim = maxWS_WD[WD_Ind]

                    // Sweep_a_Param(SepWS_Low_lim, SepWS_High_lim, (SepWS_High_lim - SepWS_Low_lim) / 8, WD_Ind, "Sep Crit WS", thisModel, DH_A_Adj_WD, DH_B_Adj_WD, UH_A_Adj_WD, UH_B_Adj_WD, modelList, radius, metList,
                    //                                          windRose, UW_Crit_WD, SU_A_Adj_WD, SU_B_Adj_WD, DH_Stab_A_WD, UH_Stab_A_WD, SU_Stab_A_WD, Stab_B_WD, useSR, Sep_A_DW_Adj_WD, Sep_B_DW_Adj_WD,
                    //                                          Turb_WS_Fact_WD, Sep_Crit_WD, Sep_CritWS_WD, nodeList, useSepModel, maxSumUWDW_ExpoWD, avgP10ExpoWD, Init_DH, Init_UH,
                    //                                          Init_SU, Init_FS_DW)

                    // 7) Sweep Separation flow A and B (when flow separation occurs downwind)

                    Sweep_a_Param(thisInst, 0.5f, 2, 0.25f, WD_Ind, "Sep DW A", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                    // 8) Sweep Turbulent length factor (Turbulenth sonze length = Factor * sumUWDW & Delta WS = Turbulent length / 500)
                    Sweep_a_Param(thisInst, 1, 7, 0.5f, WD_Ind, "Turb WS factor", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                    // 8) Sweep Separation flow A and B (when flow separation occurs upwind)
                    //   Sep_B_UW_Adj_WD[WD_Ind] = 0
                    //   Sweep_a_Param(0.5, 2, 0.25, WD_Ind, "Sep UW A", thisModel, DH_A_Adj_WD, DH_B_Adj_WD, UH_A_Adj_WD, UH_B_Adj_WD, modelList, radius, metList,
                    //                                            windRose, UW_Crit_WD, SU_A_Adj_WD, SU_B_Adj_WD, DH_Stab_A_WD, UH_Stab_A_WD, SU_Stab_A_WD, Stab_B_WD, useSR, Sep_A_DW_Adj_WD, Sep_B_DW_Adj_WD,
                    //                                            Turb_Length_WD, Sep_Crit_WD, Sep_CritWS_WD, nodeList, useSepModel, maxSumUWDW_ExpoWD, avgP10ExpoWD, Init_DH, Init_UH,
                    //                                            Init_SU, Init_FS_DW)

                }

                overallRMS_err = SweepGetRMSWithAdjModel(ref thisModel, thisAdjModel, thisInst);
                RMS_Err = GetRMS_SectorErr(thisModel, WD_Ind);
            }

        }

        public void DoFineSweep(Continuum thisInst, Model thisModel, ref Model_Adj thisAdjModel, Init_Params theseInitParams, MinMax_Expos theseMinMax)
        {
            // Using finer step sizes and smaller ranges, this function: 
            // Sweeps downhill, uphill and speed-up //A coeffs// (which modifies the magnitude of the coeffs) and UW_crit
            // if ( surface roughness model is used, sweeps UH/DH/SU stability exponents
            // if ( flow separation model is used, sweeps critical UWDW, turbulent //A// coeff and turbulent zone WS factor (which affects the wind speed in the turbulent zone downwind of point of separation)
            // && finds coeffs that yield the lowest cross-prediction error and stores them as Model_Adj object

            double lowLim;
            double upperLim;
            double overallRMS_err;
            double RMS_Err;
            int numWD = thisInst.metList.numWD;
            Model origModel = new Model();
            origModel.SetDefaultLimits();
            origModel.SizeArrays(numWD);
            origModel.SetDefaultModelCoeffs(numWD);

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                if (WD_Ind == 11)
                    WD_Ind = WD_Ind;

                // 1) Sweep UW Crit

                lowLim = 0.7f * thisAdjModel.UW_Crit[WD_Ind];
                upperLim = 1.3f * thisAdjModel.UW_Crit[WD_Ind];
                
                Sweep_a_Param(thisInst, lowLim, upperLim, (upperLim - lowLim) / 8, WD_Ind, "UW Critical", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, 
                    theseMinMax.avgP10ExpoWD, theseInitParams);

                if (thisInst.topo.useSepMod == true)
                {
                    // 2) Sweep Flow Separation Crit
                    if (thisAdjModel.sepCrit[WD_Ind] != 1000)
                    {
                        lowLim = 0.7f * thisAdjModel.sepCrit[WD_Ind];
                        upperLim = 1.3f * thisAdjModel.sepCrit[WD_Ind];
                    }
                    else {
                        lowLim = Math.Max(thisModel.minSumUWDW, theseMinMax.minSumUWDW_ExpoWD[WD_Ind] - theseMinMax.minSumUWDW_ExpoWD[WD_Ind] * 0.1);
                        upperLim = theseMinMax.maxSumUWDW_ExpoWD[WD_Ind];

                    }

                    Sweep_a_Param(thisInst, lowLim, upperLim, (upperLim - lowLim) / 8, WD_Ind, "Flow Sep Critical", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD,
                                    theseMinMax.avgP10ExpoWD, theseInitParams);
                }

                // 3) Downhill flow
                lowLim = 0.77f * thisAdjModel.DH_A_Adj[WD_Ind] / theseInitParams.DH * origModel.downhill_A[WD_Ind] * (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (origModel.downhill_B[WD_Ind] * thisAdjModel.DH_B_Adj[WD_Ind])));
                upperLim = 1.3f * thisAdjModel.DH_A_Adj[WD_Ind] / theseInitParams.DH * origModel.downhill_A[WD_Ind] * (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (origModel.downhill_B[WD_Ind] * thisAdjModel.DH_B_Adj[WD_Ind])));

                Sweep_a_Param(thisInst, lowLim, upperLim, (upperLim - lowLim) / 8, WD_Ind, "Downhill A", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD,
                                  theseMinMax.avgP10ExpoWD, theseInitParams);

                // 4) Uphill flow
                lowLim = 0.77f * thisAdjModel.UH_A_Adj[WD_Ind] / Math.Abs(theseInitParams.UH) * origModel.uphill_A[WD_Ind] * (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (origModel.uphill_B[WD_Ind] * thisAdjModel.UH_B_Adj[WD_Ind])));
                upperLim = 1.3f * thisAdjModel.UH_A_Adj[WD_Ind] / Math.Abs(theseInitParams.UH) * origModel.uphill_A[WD_Ind] * (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (origModel.uphill_B[WD_Ind] * thisAdjModel.UH_B_Adj[WD_Ind])));
                Sweep_a_Param(thisInst, lowLim, upperLim, (upperLim - lowLim) / 8, WD_Ind, "Uphill A", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD,
                                  theseMinMax.avgP10ExpoWD, theseInitParams);

                // 5) Sweep UW Spd-up A 
                lowLim = 0.77f * thisAdjModel.SU_A_Adj[WD_Ind] / theseInitParams.SU * origModel.spdUp_A[WD_Ind] * (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (origModel.spdUp_B[WD_Ind] * thisAdjModel.SU_B_Adj[WD_Ind])));
                upperLim = 1.3f * thisAdjModel.SU_A_Adj[WD_Ind] / theseInitParams.SU * origModel.spdUp_A[WD_Ind] * (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (origModel.spdUp_B[WD_Ind] * thisAdjModel.SU_B_Adj[WD_Ind])));
                Sweep_a_Param(thisInst, lowLim, upperLim, (upperLim - lowLim) / 8, WD_Ind, "SpdUp A", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                if (thisInst.topo.useSR == true)
                {
                    // 9) Sweep Stability factor for downhill flow
                    lowLim = thisAdjModel.DH_Stab_A[WD_Ind] - 1;
                    if (lowLim < 0) lowLim = 0;

                    upperLim = thisAdjModel.DH_Stab_A[WD_Ind] + 1;
                    Sweep_a_Param(thisInst, lowLim, upperLim, (upperLim - lowLim) / 5, WD_Ind, "DH Stability", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD,
                        theseMinMax.avgP10ExpoWD, theseInitParams);

                    // 10) Sweep Stability factor for uphill flow 
                    lowLim = thisAdjModel.UH_Stab_A[WD_Ind] - 1;
                    if (lowLim < 0) lowLim = 0;
                    upperLim = thisAdjModel.UH_Stab_A[WD_Ind] + 1;
                    Sweep_a_Param(thisInst, lowLim, upperLim, (upperLim - lowLim) / 5, WD_Ind, "UH Stability", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD,
                        theseMinMax.avgP10ExpoWD, theseInitParams);

                    // 11) Sweep Stability factor for speed-up flow 
                    lowLim = thisAdjModel.SU_Stab_A[WD_Ind] - 1;
                    if (lowLim < 0) lowLim = 0;
                    upperLim = thisAdjModel.SU_Stab_A[WD_Ind] + 1;
                    Sweep_a_Param(thisInst, lowLim, upperLim, (upperLim - lowLim) / 5, WD_Ind, "SU Stability", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD,
                        theseMinMax.avgP10ExpoWD, theseInitParams);

                }

                if (thisInst.topo.useSepMod == true && thisAdjModel.sepCrit[WD_Ind] != 1000) {
                    // 6) Sweep Flow Separation Crit WS
                    //   lowLim = Sep_CritWS_WD[WD_Ind] - 0.5
                    //   upperLim = Sep_CritWS_WD[WD_Ind] + 0.5

                    // Sweep_a_Param(lowLim, upperLim, (upperLim - lowLim) / 5, WD_Ind, "Sep Crit WS", thisModel, DH_A_Adj_WD, DH_B_Adj_WD, UH_A_Adj_WD, UH_B_Adj_WD, modelList, radius, metList,
                    //                                          windRose, UW_Crit_WD, SU_A_Adj_WD, SU_B_Adj_WD, DH_Stab_A_WD, UH_Stab_A_WD, SU_Stab_A_WD, Stab_B_WD, useSR, Sep_A_DW_Adj_WD, Sep_B_DW_Adj_WD,
                    //                                          Turb_WS_Fact_WD, Sep_Crit_WD, Sep_CritWS_WD, nodeList, useSepModel, maxSumUWDW_ExpoWD, avgP10ExpoWD, Init_DH, Init_UH,
                    //                                          Init_SU, Init_FS_DW)

                    // 7) Sweep Separation flow A and B (when flow separation occurs downwind)

                    lowLim = 0.4f * thisAdjModel.sepA_DW_Adj[WD_Ind] / Math.Abs(theseInitParams.FS_DW) * origModel.sep_A_DW[WD_Ind] * (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (origModel.sep_B_DW[WD_Ind] * thisAdjModel.sepB_DW_Adj[WD_Ind])));
                    upperLim = 1.6f * thisAdjModel.sepA_DW_Adj[WD_Ind] / Math.Abs(theseInitParams.FS_DW) * origModel.sep_A_DW[WD_Ind] * (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (origModel.sep_B_DW[WD_Ind] * thisAdjModel.sepB_DW_Adj[WD_Ind])));
                    Sweep_a_Param(thisInst, lowLim, upperLim, (upperLim - lowLim) / 8, WD_Ind, "Sep DW A", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD,
                                  theseMinMax.avgP10ExpoWD, theseInitParams);


                    // 8) Sweep Turbulent WS Factor (when flow separation occurs upwind)
                    // lowLim = 0.4 * Sep_A_UW_Adj_WD[WD_Ind] / Math.Abs(Init_FS_UW) * origModel.Sep_A_UW[WD_Ind] * (avgP10ExpoWD[WD_Ind] ^ (origModel.Sep_B_UW[WD_Ind] * Sep_B_UW_Adj_WD[WD_Ind]))
                    //  upperLim = 1.6 * Sep_A_UW_Adj_WD[WD_Ind] / Math.Abs(Init_FS_UW) * origModel.Sep_A_UW[WD_Ind] * (avgP10ExpoWD[WD_Ind] ^ (origModel.Sep_B_UW[WD_Ind] * Sep_B_UW_Adj_WD[WD_Ind]))

                    lowLim = thisAdjModel.turbWS_Fact[WD_Ind] * 0.7f;
                    upperLim = thisAdjModel.turbWS_Fact[WD_Ind] * 1.3f;
                    Sweep_a_Param(thisInst, lowLim, upperLim, (upperLim - lowLim) / 8, WD_Ind, "Turb WS Factor", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD,
                                  theseMinMax.avgP10ExpoWD, theseInitParams);

                }

                overallRMS_err = SweepGetRMSWithAdjModel(ref thisModel, thisAdjModel, thisInst);
                RMS_Err = GetRMS_SectorErr(thisModel, WD_Ind);
            }
        }

        public void InitializeAdjModel(ref Model_Adj thisAdjModel, int numWD, Init_Params theseInitParams, double[] initCoeffs, Model thisModel, MinMax_Expos theseMinMax)
        {
            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                thisAdjModel.DH_A_Adj[WD_Ind] = theseInitParams.DH / thisModel.downhill_A[0];
                thisAdjModel.DH_B_Adj[WD_Ind] = 0;
                thisAdjModel.UH_A_Adj[WD_Ind] = Math.Abs(theseInitParams.UH) / thisModel.uphill_A[0];
                thisAdjModel.UH_B_Adj[WD_Ind] = 0;
                thisAdjModel.SU_A_Adj[WD_Ind] = Math.Abs(theseInitParams.SU) / thisModel.spdUp_A[0];
                thisAdjModel.SU_B_Adj[WD_Ind] = 0;

                if (initCoeffs[1] > 0)
                    thisAdjModel.UW_Crit[WD_Ind] = theseMinMax.maxUW_ExpoWD[WD_Ind] + theseMinMax.maxUW_ExpoWD[WD_Ind] * 0.1f;
                else
                    thisAdjModel.UW_Crit[WD_Ind] = Math.Max(1, theseMinMax.minUW_ExpoWD[WD_Ind] - theseMinMax.minUW_ExpoWD[WD_Ind] * 0.1f);

                thisAdjModel.sepA_DW_Adj[WD_Ind] = Math.Abs(theseInitParams.FS_DW) / thisModel.sep_A_DW[0];
                thisAdjModel.sepB_DW_Adj[WD_Ind] = 0;
                thisAdjModel.turbWS_Fact[WD_Ind] = 4;
                thisAdjModel.sepCrit[WD_Ind] = Math.Max(100, theseMinMax.maxSumUWDW_ExpoWD[WD_Ind] * 1.5f);
                thisAdjModel.sepCritWS[WD_Ind] = theseMinMax.minWS_WD[WD_Ind] * 0.5f;

                thisAdjModel.DH_Stab_A[WD_Ind] = 5;
                thisAdjModel.UH_Stab_A[WD_Ind] = 5;
                thisAdjModel.SU_Stab_A[WD_Ind] = 5;
             //   thisAdjModel.stabB[WD_Ind] = 0;
            }
        }

        public void SweepFindMinError(Model thisModel, Continuum thisInst)
        {
            // Finds site-calibrated model by sweeping parameters and finding model that minimizes cross-prediction error          
                        
            double[] windRose = thisInst.metList.GetAvgWindRoseMetsUsed(thisModel.metsUsed, thisModel.timeOfDay, thisModel.season, thisModel.height);
            MetCollection metList = thisInst.metList;
            ModelCollection modelList = thisInst.modelList;            
            NodeCollection nodeList = new NodeCollection();

            int radiusIndex = thisInst.radiiList.GetRadiusInd(thisModel.radius);      
            int numWD = thisInst.metList.numWD;
            Model_Adj thisAdjModel = new Model_Adj();
            SizeAdjModel(ref thisAdjModel, numWD);

            Init_Params theseInitParams = new Init_Params();
            double[] initCoeffs = new double[4]; // 0: Intercept 1: m_uw 2: m_dw 3: Rsq

            MinMax_Expos theseMinMax = new MinMax_Expos();
            InitMinMaxExpos(ref theseMinMax, numWD);

            thisModel.SizeArrays(numWD);
            thisModel.SetDefaultModelCoeffs(numWD);

            Calc_MinMax_Expos(ref theseMinMax, windRose, radiusIndex, metList, thisModel); // finds min/max UW expo (used to initialize UW crit), min/max sum of UWDW (used in flow separation model), avg P10 expo and min/max WS

            //calculate linear regression to find initial coefficients
            FindBestInitCoeffs(thisModel, ref theseInitParams, ref initCoeffs, metList, radiusIndex, theseMinMax);
            InitializeAdjModel(ref thisAdjModel, numWD, theseInitParams, initCoeffs, thisModel, theseMinMax);
                
            double RMS_Err;

            Model origModel = new Model();
            origModel.SizeArrays(numWD);
            origModel.SetDefaultModelCoeffs(numWD);            

            double overallRMS_err = SweepGetRMSWithAdjModel(ref thisModel, thisAdjModel, thisInst);

            DoCoarseSweep(thisInst, thisModel, ref thisAdjModel, theseInitParams, theseMinMax);

            DoFineSweep(thisInst, thisModel, ref thisAdjModel, theseInitParams, theseMinMax);

            // Now do slope fit by WD
            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                // 1) First modify Downhill flow

                // for each B step, find A_adj so that coeff at Avg P10 Expo = Init_mdw
                Sweep_a_Param(thisInst, 0, 1.4f, 0.2f, WD_Ind, "Downhill B", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                // 2) Now modify Uphill flow
                Sweep_a_Param(thisInst, 0, 1.4f, 0.2f, WD_Ind, "Uphill B", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                // 3) Sweep UW Spd-up A and B
                Sweep_a_Param(thisInst, 0, 1.4f, 0.2f, WD_Ind, "SpdUp B", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                if (thisInst.topo.useSepMod == true)
                {
                    if (thisAdjModel.sepCrit[WD_Ind] != 1000) {
                        // 6) Sweep Separation flow A and B for when flow sep occurs downwind
                        Sweep_a_Param(thisInst, 0, 1.4f, 0.2f, WD_Ind, "Sep B DW", ref thisModel, thisAdjModel, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                        // 6) Sweep Separation flow A and B for when flow sep occurs upwind
                        //   Sweep_a_Param(0, 1.4, 0.2, WD_Ind, "Sep B UW", thisModel, DH_A_Adj_WD, DH_B_Adj_WD, UH_A_Adj_WD, UH_B_Adj_WD, modelList, radius, metList,
                        //                                       windRose, UW_Crit_WD, SU_A_Adj_WD, SU_B_Adj_WD, DH_Stab_A_WD, UH_Stab_A_WD, SU_Stab_A_WD, Stab_B_WD, useSR, Sep_A_DW_Adj_WD, Sep_B_DW_Adj_WD,
                        //                                       Turb_WS_Fact_WD, Sep_Crit_WD, Sep_CritWS_WD, nodeList, useSepModel, maxSumUWDW_ExpoWD, avgP10ExpoWD, Init_DH, Init_UH,
                        //                                       Init_SU, Init_FS_DW)

                    }                    
                }

                overallRMS_err = SweepGetRMSWithAdjModel(ref thisModel, thisAdjModel, thisInst);

                RMS_Err = GetRMS_SectorErr(thisModel, WD_Ind);
            }

            RMS_Err = SweepGetRMSWithAdjModel(ref thisModel, thisAdjModel, thisInst);

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                thisModel.downhill_A[WD_Ind] = origModel.downhill_A[WD_Ind] * thisAdjModel.DH_A_Adj[WD_Ind];
                thisModel.downhill_B[WD_Ind] = origModel.downhill_B[WD_Ind] * thisAdjModel.DH_B_Adj[WD_Ind];

                thisModel.uphill_A[WD_Ind] = origModel.uphill_A[WD_Ind] * thisAdjModel.UH_A_Adj[WD_Ind];
                thisModel.uphill_B[WD_Ind] = origModel.uphill_B[WD_Ind] * thisAdjModel.UH_B_Adj[WD_Ind];

                thisModel.spdUp_A[WD_Ind] = origModel.spdUp_A[WD_Ind] * thisAdjModel.SU_A_Adj[WD_Ind];
                thisModel.spdUp_B[WD_Ind] = origModel.spdUp_B[WD_Ind] * thisAdjModel.SU_B_Adj[WD_Ind];
                thisModel.UW_crit[WD_Ind] = thisAdjModel.UW_Crit[WD_Ind];

                thisModel.sep_A_DW[WD_Ind] = origModel.sep_A_DW[WD_Ind] * thisAdjModel.sepA_DW_Adj[WD_Ind];
                thisModel.sep_B_DW[WD_Ind] = origModel.sep_B_DW[WD_Ind] * thisAdjModel.sepB_DW_Adj[WD_Ind];
                // thisModel.Sep_A_UW[WD_Ind] = origModel.Sep_A_UW[WD_Ind] * Sep_A_UW_Adj_WD[WD_Ind]
                //   thisModel.Sep_B_UW[WD_Ind] = origModel.Sep_B_UW[WD_Ind] * Sep_B_UW_Adj_WD[WD_Ind]
                thisModel.turbWS_Fact[WD_Ind] = thisAdjModel.turbWS_Fact[WD_Ind];
                thisModel.sepCrit[WD_Ind] = thisAdjModel.sepCrit[WD_Ind];
                thisModel.Sep_crit_WS[WD_Ind] = thisAdjModel.sepCritWS[WD_Ind];

                thisModel.DH_Stab_A[WD_Ind] = thisAdjModel.DH_Stab_A[WD_Ind];
                thisModel.UH_Stab_A[WD_Ind] = thisAdjModel.UH_Stab_A[WD_Ind];
                thisModel.SU_Stab_A[WD_Ind] = thisAdjModel.SU_Stab_A[WD_Ind];
             //   thisModel.stabB[WD_Ind] = thisAdjModel.stabB[WD_Ind];
            }


        }

        /// <summary>
        /// Conducts linear regression and calculates the initial UW and DW coefficients (based on overall wind speed and overall UW/DW exposure) before doing site-calibration
        /// </summary>
        /// <param name="UW_DW_array"></param>
        /// <param name="WS_array"></param>
        /// <returns></returns>
        public double[] FindInitCoeffs(double[,] UW_DW_array, double[] WS_array)
        {             
            double[] coeffs = new double[4];
            int numParams = 3;
            int numMets = WS_array.Length;
            double[] errorVector = new double[numMets];

            // Calculate average wind speed
            double Y_bar = 0;
            for (int i = 0; i < WS_array.Length; i++)
                Y_bar = Y_bar + WS_array[i];

            Y_bar = Y_bar / WS_array.Length;

            //coeffs = (X'X)^-1X'y = Inv(X'X)X'y
            // First find X' (transpose)
            double[,] Xprime = new double[numParams, numMets];

            for (int i = 0; i < numMets; i++)
                for (int j = 0; j < numParams; j++)
                    Xprime[j, i] = UW_DW_array[i, j];

            // Now find X'X
            double[,] XprimeX = new double[numParams, numParams];

            for (int i = 0; i < numParams; i++)
                for (int j = 0; j < numParams; j++)
                    for (int k = 0; k < numMets; k++)
                        XprimeX[i, j] = XprimeX[i, j] + Xprime[i, k] * UW_DW_array[k, j];


            // Now find inverse of X'X: Inv(X'X)
            // First check to make sure that it is invertible
            double det = 0;
            double[,] invXprimeX = new double[numParams, numParams];

            det = (XprimeX[0, 0] * XprimeX[1, 1] * XprimeX[2, 2]) + (XprimeX[1, 0] * XprimeX[2, 1] * XprimeX[0, 2]) +
                    (XprimeX[2, 0] * XprimeX[0, 1] * XprimeX[1, 2]) - (XprimeX[0, 0] * XprimeX[2, 1] * XprimeX[1, 2]) -
                    (XprimeX[2, 0] * XprimeX[1, 1] * XprimeX[0, 2]) - (XprimeX[1, 0] * XprimeX[0, 1] * XprimeX[2, 2]);

            if (det != 0) {
                invXprimeX[0, 0] = (XprimeX[1, 1] * XprimeX[2, 2] - XprimeX[1, 2] * XprimeX[2, 1]) / det; // Should this be absolute value?
                invXprimeX[0, 1] = (XprimeX[0, 2] * XprimeX[2, 1] - XprimeX[0, 1] * XprimeX[2, 2]) / det;
                invXprimeX[0, 2] = (XprimeX[0, 1] * XprimeX[1, 2] - XprimeX[0, 2] * XprimeX[1, 1]) / det;
                invXprimeX[1, 0] = (XprimeX[1, 2] * XprimeX[2, 0] - XprimeX[1, 0] * XprimeX[2, 2]) / det;
                invXprimeX[1, 1] = (XprimeX[0, 0] * XprimeX[2, 2] - XprimeX[0, 2] * XprimeX[2, 0]) / det;
                invXprimeX[1, 2] = (XprimeX[0, 2] * XprimeX[1, 0] - XprimeX[0, 0] * XprimeX[1, 2]) / det;
                invXprimeX[2, 0] = (XprimeX[1, 0] * XprimeX[2, 1] - XprimeX[1, 1] * XprimeX[2, 0]) / det;
                invXprimeX[2, 1] = (XprimeX[0, 1] * XprimeX[2, 0] - XprimeX[0, 0] * XprimeX[2, 1]) / det;
                invXprimeX[2, 2] = (XprimeX[0, 0] * XprimeX[1, 1] - XprimeX[0, 1] * XprimeX[1, 0]) / det;
            }

            //coeffs = (X'X)^-1X'y = Inv(X'X)X//y
            // Let A = Inv(X'X)X'
            double[,] A = new double[numParams, numMets];

            for (int i = 0; i < numParams; i++)
                for (int j = 0; j < numMets; j++)
                    for (int k = 0; k < numParams; k++)
                        A[i, j] = A[i, j] + invXprimeX[i, k] * Xprime[k, j];

            // coeffs = A * y
            for (int i = 0; i < numParams; i++)
                for (int j = 0; j < numMets; j++)
                    coeffs[i] = coeffs[i] + (A[i, j] * WS_array[j]);

            // Error = y - y' = y - (Coeff1* x1 + Coeff2* x2 + Coeff3* x3 + Coeff4* x4)
            double[] Y_est = new double[numMets];

            for (int i = 0; i < numMets; i++)
            {
                if (numParams == 2)
                    Y_est[i] = coeffs[0] * UW_DW_array[i, 0] + coeffs[1] * UW_DW_array[i, 1];
                else if (numParams == 3) 
                    Y_est[i] = coeffs[0] * UW_DW_array[i, 0] + coeffs[1] * UW_DW_array[i, 1] + coeffs[2] * UW_DW_array[i, 2];
                else if (numParams == 4) 
                        Y_est[i] = coeffs[0] * UW_DW_array[i, 0] + coeffs[1] * UW_DW_array[i, 1] + coeffs[2] * UW_DW_array[i, 2] + coeffs[3] * UW_DW_array[i, 3];

            }

            for (int i = 0; i < numMets; i++)
                errorVector[i] = WS_array[i] - (Y_est[i]);
            
            double SSerr = 0;
            double SStot = 0;
            
            for (int i = 0; i < numMets; i++)
            {
                SStot = SStot + Math.Pow((WS_array[i] - Y_bar), 2);
                SSerr = SSerr + Math.Pow(errorVector[i], 2);
            }

            double Rsq = 1 - SSerr / SStot;
            coeffs[3] = Rsq;

            // Setting limit on coefficient value = 0.18. This is the maximum coefficient allowed in Model.Calc_DW_Coeff and Model.Calc_UW_Coeff
            if (Math.Abs(coeffs[1]) > 0.18)
            {
                if (coeffs[1] < 0)
                    coeffs[1] = -0.18;
                else
                    coeffs[1] = 0.18;
            }

            if (Math.Abs(coeffs[2]) > 0.18)
            {
                if (coeffs[2] < 0)
                    coeffs[2] = -0.18;
                else
                    coeffs[2] = 0.18;
            }


            return coeffs;

        }

        public double[,] GetMinAndMaxP10Expo(Model model, InvestCollection radiiList, int WD_Ind, int numWD)
        {
            // Finds the min and max P10 expo for Pos and Neg UW and DW exposure of mets and nodes (if any)
            double[,] minMaxP10Expo = new double[4, 2]; // i = 0 Pos DW, 1 Neg DW, 2 Pos UW, 3 Neg UW; j = 0 Min, 1 Max
            double minPosDW = 1000;
            double maxPosDW = 0;
            double minNegDW = 1000;
            double maxNegDW = 0;
            double minPosUW = 1000;
            double maxPosUW = 0;
            double minNegUW = 1000;
            double maxNegUW = 0;
                                    
            int radiusIndex = 0;
            double P10Expo = 0;
            
            for (int i = 0; i < radiiList.ThisCount; i++)
                if (radiiList.investItem[i].radius == model.radius)
                {
                    radiusIndex = i;
                    break;
                }
            
            for (int i = 0; i < PairCount; i++)
            {
                // See if mets in pair are used in UW&DW model
                bool bothMetsUsed = BothInModel(metPairs[i], model);

                if (bothMetsUsed == true)
                {
                    // Met 1 P10 expo
                    if (metPairs[i].met1.gridStats.stats[radiusIndex].P10_DW[WD_Ind] > metPairs[i].met1.gridStats.stats[radiusIndex].P10_UW[WD_Ind])
                        P10Expo = metPairs[i].met1.gridStats.stats[radiusIndex].P10_DW[WD_Ind];
                    else
                        P10Expo = metPairs[i].met1.gridStats.stats[radiusIndex].P10_UW[WD_Ind];                    

                    if (metPairs[i].met1.expo[radiusIndex].expo[WD_Ind] > 0)
                    {
                        if (P10Expo < minPosUW) minPosUW = P10Expo;
                        if (P10Expo > maxPosUW) maxPosUW = P10Expo;
                    }
                    else {
                        if (P10Expo < minNegUW) minNegUW = P10Expo;
                        if (P10Expo > maxNegUW) maxNegUW = P10Expo;
                    }

                    if (metPairs[i].met1.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo") > 0)
                    {
                        if (P10Expo < minPosDW) minPosDW = P10Expo;
                        if (P10Expo > maxPosDW) maxPosDW = P10Expo;
                    }
                    else {
                        if (P10Expo < minNegDW) minNegDW = P10Expo;
                        if (P10Expo > maxNegDW) maxNegDW = P10Expo;
                    }

                    // Met 2 P10 Expo
                    if (metPairs[i].met2.gridStats.stats[radiusIndex].P10_DW[WD_Ind] > metPairs[i].met2.gridStats.stats[radiusIndex].P10_UW[WD_Ind])
                        P10Expo = metPairs[i].met2.gridStats.stats[radiusIndex].P10_DW[WD_Ind];
                    else
                        P10Expo = metPairs[i].met2.gridStats.stats[radiusIndex].P10_UW[WD_Ind];

                    if (metPairs[i].met2.expo[radiusIndex].expo[WD_Ind] > 0) {
                        if (P10Expo < minPosUW) minPosUW = P10Expo;
                        if (P10Expo > maxPosUW) maxPosUW = P10Expo;
                    }
                    else {
                        if (P10Expo < minNegUW) minNegUW = P10Expo;
                        if (P10Expo > maxNegUW) maxNegUW = P10Expo;
                    }

                    if (metPairs[i].met2.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo") > 0)
                    {
                        if (P10Expo < minPosDW) minPosDW = P10Expo;
                        if (P10Expo > maxPosDW) maxPosDW = P10Expo;
                    }
                    else {
                        if (P10Expo < minNegDW) minNegDW = P10Expo;
                        if (P10Expo > maxNegDW) maxNegDW = P10Expo;
                    }

                    int numWSEsts = metPairs[i].WS_PredCount;

                    for (int WSEst = 0; WSEst < numWSEsts; WSEst++)
                    {
                        int numNodes = metPairs[i].WS_Pred[WSEst, radiusIndex].nodePath.Length;

                        for (int nodeInd = 0; nodeInd < numNodes; nodeInd++)
                        {
                            Nodes thisNode = metPairs[i].WS_Pred[WSEst, radiusIndex].nodePath[nodeInd];

                            if (thisNode.gridStats.stats[radiusIndex].P10_DW[WD_Ind] > thisNode.gridStats.stats[radiusIndex].P10_UW[WD_Ind])
                                P10Expo = thisNode.gridStats.stats[radiusIndex].P10_DW[WD_Ind];
                            else
                                P10Expo = thisNode.gridStats.stats[radiusIndex].P10_UW[WD_Ind];

                            if (thisNode.expo[radiusIndex].expo[WD_Ind] > 0)
                            {
                                if (P10Expo < minPosUW) minPosUW = P10Expo;
                                if (P10Expo > maxPosUW) maxPosUW = P10Expo;
                            }
                            else {
                                if (P10Expo < minNegUW) minNegUW = P10Expo;
                                if (P10Expo > maxNegUW) maxNegUW = P10Expo;
                            }

                            if (thisNode.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo") > 0)
                            {
                                if (P10Expo < minPosDW) minPosDW = P10Expo;
                                if (P10Expo > maxPosDW) maxPosDW = P10Expo;
                            }
                            else {
                                if (P10Expo < minNegDW) minNegDW = P10Expo;
                                if (P10Expo > maxNegDW) maxNegDW = P10Expo;
                            }
                        }
                    }
                }
            }
            
            // i = 0 Pos DW, 1 Neg DW, 2 Pos UW, 3 Neg UW; j = 0 Min, 1 Max
            minMaxP10Expo[0, 0] = minPosDW;
            minMaxP10Expo[0, 1] = maxPosDW;
            minMaxP10Expo[1, 0] = minNegDW;
            minMaxP10Expo[1, 1] = maxNegDW;
            minMaxP10Expo[2, 0] = minPosUW;
            minMaxP10Expo[2, 1] = maxPosUW;
            minMaxP10Expo[3, 0] = minNegUW;
            minMaxP10Expo[3, 1] = maxNegUW;

            return minMaxP10Expo;

        }

        public double GetRMS_SectorErr(Model model, int WD_Ind)
        {
            // Calculates and returns the RMS of the cross-prediction estimates at specified wind direction index

            double RMS_Err   = 0;
            int RMS_Count   = 0;            
            int radiusIndex = 0;

            for (int i = 0; i < PairCount; i++)
            {
                // See if mets in pair are used in UW&DW model
                bool bothMetsUsed = BothInModel(metPairs[i], model);

                if (bothMetsUsed == true)
                {
                    for (int m = 0; m <= metPairs[i].WS_Pred.GetUpperBound(1); m++)
                    { 
                        if (metPairs[i].WS_Pred[0, m].model.radius == model.radius)
                        {
                            radiusIndex = m;
                            break;
                        }
                    }
                                        
                    // Find WS prediction using same model 
                    Pair_Of_Mets.WS_CrossPreds thisCrossPred = metPairs[i].GetWS_CrossPred(model);                                      

                    RMS_Err = RMS_Err + Math.Pow(thisCrossPred.percErrSector[0, WD_Ind], 2);
                    RMS_Err = RMS_Err + Math.Pow(thisCrossPred.percErrSector[1, WD_Ind], 2);
                    RMS_Count = RMS_Count + 2;                   
                }
            }

            if (RMS_Count > 0) RMS_Err = Math.Pow((RMS_Err / (RMS_Count)), 0.5);
            return RMS_Err;
        }

        /// <summary>
        /// Conducts met cross-prediction using modified model and returns RMS error of cross-prediction estimates
        /// </summary>
        /// <param name="model">Model used</param>
        /// <param name="adjModel">Adjusted Model</param>
        /// <param name="thisInst"></param>
        /// <returns></returns>
        public double SweepGetRMSWithAdjModel(ref Model model, Model_Adj adjModel, Continuum thisInst)
        {            
            double RMS_Err = 0;            
            double RMS_Count = 0;
            int numWD = thisInst.metList.numWD;
                                            
            Model defaultModel = new Model();
            defaultModel.SizeArrays(numWD);
            defaultModel.SetDefaultModelCoeffs(numWD);

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                model.downhill_A[WD_Ind] = defaultModel.downhill_A[WD_Ind] * adjModel.DH_A_Adj[WD_Ind];
                model.downhill_B[WD_Ind] = defaultModel.downhill_B[WD_Ind] * adjModel.DH_B_Adj[WD_Ind];
                model.uphill_A[WD_Ind] = defaultModel.uphill_A[WD_Ind] * adjModel.UH_A_Adj[WD_Ind];
                model.uphill_B[WD_Ind] = defaultModel.uphill_B[WD_Ind] * adjModel.UH_B_Adj[WD_Ind];

                model.UW_crit[WD_Ind] = adjModel.UW_Crit[WD_Ind];
                model.spdUp_A[WD_Ind] = defaultModel.spdUp_A[WD_Ind] * adjModel.SU_A_Adj[WD_Ind];
                model.spdUp_B[WD_Ind] = defaultModel.spdUp_B[WD_Ind] * adjModel.SU_B_Adj[WD_Ind];

                model.sepCrit[WD_Ind] = adjModel.sepCrit[WD_Ind];
                model.Sep_crit_WS[WD_Ind] = adjModel.sepCritWS[WD_Ind];
                model.sep_A_DW[WD_Ind] = defaultModel.sep_A_DW[WD_Ind] * adjModel.sepA_DW_Adj[WD_Ind];
                model.sep_B_DW[WD_Ind] = defaultModel.sep_B_DW[WD_Ind] * adjModel.sepB_DW_Adj[WD_Ind];
                model.turbWS_Fact[WD_Ind] = adjModel.turbWS_Fact[WD_Ind];

                model.DH_Stab_A[WD_Ind] = adjModel.DH_Stab_A[WD_Ind];
                model.UH_Stab_A[WD_Ind] = adjModel.UH_Stab_A[WD_Ind];
                model.SU_Stab_A[WD_Ind] = adjModel.SU_Stab_A[WD_Ind];
             //   model.stabB[WD_Ind] = adjModel.stabB[WD_Ind];
            }


            for (int i = 0; i < PairCount; i++)
            {   
                bool bothMetsUsed = BothInModel(metPairs[i], model); // See if mets in pair are used in UW&DW model

                if (bothMetsUsed == true)
                {
                    int radiusIndex = 0;
                    for (int m = 0; m <= metPairs[i].WS_Pred.GetUpperBound(1); m++)
                    {
                        if (metPairs[i].WS_Pred[0, m].model.radius == model.radius)
                        {
                            radiusIndex = m;
                            break;
                        }
                    }                                       
                    
                    int WS_PredInd = metPairs[i].GetWS_PredIndOneModel(model, thisInst.modelList);
                    
                    if (WS_PredInd == -1)                                            
                        return RMS_Err;

                    metPairs[i].DoMetCrossPred(WS_PredInd, radiusIndex, thisInst);                                      

                    RMS_Err = RMS_Err + Math.Pow(metPairs[i].WS_Pred[WS_PredInd, radiusIndex].percErr[0], 2);
                    RMS_Count++;                                      
                    
                    RMS_Err = RMS_Err + Math.Pow(metPairs[i].WS_Pred[WS_PredInd, radiusIndex].percErr[1], 2);
                    RMS_Count++;
                }
            }

            RMS_Err = Math.Pow((RMS_Err / (RMS_Count)), 0.5);

            return RMS_Err;
        }

        public double[,] GetCoeffsForLogLogPlot(Model model, Continuum thisInst, NodeCollection nodeList, int WD_sec, string UW_or_DW, string flowType)
        {
            // Returns P10 expo and coefficients for log-log plot on Advanced tab
            double[,] coeffsAndP10Expos = new double[2, 1]; // Col 1: coeff; Col 2: P10 Expo
            int coeffCount = 0;

            
            int numWD = thisInst.GetNumWD();
            if (numWD == 0)
                return coeffsAndP10Expos;                        

            double[] coeffs = null;
            double[] P10_Expo = null;
            
            double WS1 = 0;
            double UW1 = 0;
            double UW2 = 0;
            double DW1 = 0;
            double DW2 = 0;
            
            if (PairCount == 0) 
                return null;            
                        
            Pair_Of_Mets.WS_CrossPreds thisWS_Est = new Pair_Of_Mets.WS_CrossPreds();
            int radiusIndex = 0;
            
            string flow1;
            string flow2;
            
            for (int i = 0; i < PairCount; i++)
            {
                // Check to see if met pair is in UWDW model
                bool bothMetsUsed = BothInModel(metPairs[i], model);

                if (bothMetsUsed == true)
                {
                    bool foundWS_Est = false;
                    
                    for (int j = 0; j < metPairs[i].WS_PredCount; j++)
                    {
                        for (int k = 0; k <= metPairs[i].WS_Pred.GetUpperBound(1); k++)
                            if (thisInst.modelList.IsSameModel(metPairs[i].WS_Pred[j, k].model, model))
                            {
                                thisWS_Est = metPairs[i].WS_Pred[j, k];
                                radiusIndex = k;
                                foundWS_Est = true;
                                break;
                            }

                        if (foundWS_Est == true) break;
                    }

                    double thisP10DW = 0;
                    double thisP10UW = 0;
                    double thisP10 = 0;
                    NodeCollection.Sep_Nodes[] flowSep = new NodeCollection.Sep_Nodes[1];
                    Met.WSWD_Dist met1Dist = metPairs[i].met1.GetWS_WD_Dist(model.height, model.timeOfDay, model.season);
                    Met.WSWD_Dist met2Dist = metPairs[i].met2.GetWS_WD_Dist(model.height, model.timeOfDay, model.season);

                    if (WD_sec < numWD)
                    {
                        if (thisWS_Est.nodePath.Length > 0)
                        {
                            thisP10DW = (Math.Abs(metPairs[i].met1.gridStats.stats[radiusIndex].P10_DW[WD_sec]) + Math.Abs(thisWS_Est.nodePath[0].gridStats.stats[radiusIndex].P10_DW[WD_sec])) / 2;
                            thisP10UW = (Math.Abs(metPairs[i].met1.gridStats.stats[radiusIndex].P10_UW[WD_sec]) + Math.Abs(thisWS_Est.nodePath[0].gridStats.stats[radiusIndex].P10_UW[WD_sec])) / 2;

                            if (thisP10DW > thisP10UW)
                                thisP10 = thisP10DW;
                            else
                                thisP10 = thisP10UW;

                            WS1 = met1Dist.WS * met1Dist.sectorWS_Ratio[WD_sec];
                            UW1 = metPairs[i].met1.expo[radiusIndex].expo[WD_sec];
                            UW2 = thisWS_Est.nodePath[0].expo[radiusIndex].expo[WD_sec];
                            DW1 = metPairs[i].met1.expo[radiusIndex].GetDW_Param(WD_sec, "Expo");
                            DW2 = thisWS_Est.nodePath[0].expo[radiusIndex].GetDW_Param(WD_sec, "Expo");

                            if (thisInst.topo.useSepMod == true) flowSep = nodeList.GetSepNodes1and2(metPairs[i].met1.flowSepNodes, thisWS_Est.nodePath[0].flowSepNodes, WD_sec);

                            if (UW_or_DW == "UW")
                            {
                                flow1 = model.GetFlowType(UW1, DW1, WD_sec, "UW", metPairs[i].met1.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                flow2 = model.GetFlowType(UW2, DW2, WD_sec, "UW", metPairs[i].met2.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                            }
                            else {
                                flow1 = model.GetFlowType(UW1, DW1, WD_sec, "DW", metPairs[i].met1.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                flow2 = model.GetFlowType(UW2, DW2, WD_sec, "DW", metPairs[i].met2.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                            }

                            if ((flow1 == flowType) || (flow2 == flowType))
                            {
                                Array.Resize(ref coeffs, coeffCount + 1);
                                Array.Resize(ref P10_Expo, coeffCount + 1);

                                P10_Expo[coeffCount] = thisP10;

                                if (UW_or_DW == "UW")
                                    coeffs[coeffCount] = model.CalcUW_Coeff(thisP10UW, thisP10DW, WD_sec, flowType);
                                else
                                    coeffs[coeffCount] = model.CalcDW_Coeff(thisP10DW, thisP10UW, WD_sec, flowType);

                                coeffCount++;
                            }
                            
                            for (int n = 1; n <= thisWS_Est.nodePath.Length - 1; n++)
                            {
                                thisP10DW = (Math.Abs(thisWS_Est.nodePath[n - 1].gridStats.stats[radiusIndex].P10_DW[WD_sec]) + Math.Abs(thisWS_Est.nodePath[n].gridStats.stats[radiusIndex].P10_DW[WD_sec])) / 2;
                                thisP10UW = (Math.Abs(thisWS_Est.nodePath[n - 1].gridStats.stats[radiusIndex].P10_UW[WD_sec]) + Math.Abs(thisWS_Est.nodePath[n].gridStats.stats[radiusIndex].P10_UW[WD_sec])) / 2;

                                if (thisP10DW > thisP10UW)
                                    thisP10 = thisP10DW;
                                else
                                    thisP10 = thisP10UW;

                                UW1 = thisWS_Est.nodePath[n - 1].expo[radiusIndex].expo[WD_sec];
                                UW2 = thisWS_Est.nodePath[n].expo[radiusIndex].expo[WD_sec];
                                WS1 = thisWS_Est.nodeSectorWS_Ests1to2[n - 1, WD_sec];
                                DW1 = thisWS_Est.nodePath[n - 1].expo[radiusIndex].GetDW_Param(WD_sec, "Expo");
                                DW2 = thisWS_Est.nodePath[n].expo[radiusIndex].GetDW_Param(WD_sec, "Expo");

                                if (thisInst.topo.useSepMod == true) flowSep = nodeList.GetSepNodes1and2(thisWS_Est.nodePath[n - 1].flowSepNodes, thisWS_Est.nodePath[n].flowSepNodes, WD_sec);

                                if (UW_or_DW == "UW")
                                {
                                    flow1 = model.GetFlowType(UW1, DW1, WD_sec, "UW", thisWS_Est.nodePath[n - 1].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                    flow2 = model.GetFlowType(UW2, DW2, WD_sec, "UW", thisWS_Est.nodePath[n].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                }
                                else {
                                    flow1 = model.GetFlowType(UW1, DW1, WD_sec, "DW", thisWS_Est.nodePath[n - 1].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                    flow2 = model.GetFlowType(UW2, DW2, WD_sec, "DW", thisWS_Est.nodePath[n].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                }

                                if ((flow1 == flowType) || (flow2 == flowType))
                                {
                                    Array.Resize(ref coeffs, coeffCount + 1);
                                    Array.Resize(ref P10_Expo, coeffCount + 1);

                                    P10_Expo[coeffCount] = thisP10;
                                    if (UW_or_DW == "UW")
                                        coeffs[coeffCount] = model.CalcUW_Coeff(thisP10UW, thisP10DW, WD_sec, flowType);
                                    else
                                        coeffs[coeffCount] = model.CalcDW_Coeff(thisP10DW, thisP10UW, WD_sec, flowType);

                                    coeffCount++;
                                }

                            }

                            thisP10DW = (Math.Abs(metPairs[i].met2.gridStats.stats[radiusIndex].P10_DW[WD_sec]) + Math.Abs(thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].gridStats.stats[radiusIndex].P10_DW[WD_sec])) / 2;
                            thisP10UW = (Math.Abs(metPairs[i].met2.gridStats.stats[radiusIndex].P10_UW[WD_sec]) + Math.Abs(thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].gridStats.stats[radiusIndex].P10_UW[WD_sec])) / 2;

                            if (thisP10DW > thisP10UW)
                                thisP10 = thisP10DW;
                            else
                                thisP10 = thisP10UW;

                            UW1 = metPairs[i].met2.expo[radiusIndex].expo[WD_sec];
                            UW2 = thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].expo[radiusIndex].expo[WD_sec];
                            WS1 = thisWS_Est.nodeSectorWS_Ests1to2[thisWS_Est.nodePath.Length - 1, WD_sec];
                            DW1 = metPairs[i].met2.expo[radiusIndex].GetDW_Param(WD_sec, "Expo");
                            DW2 = thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].expo[radiusIndex].GetDW_Param(WD_sec, "Expo");

                            if (thisInst.topo.useSepMod == true) flowSep = nodeList.GetSepNodes1and2(metPairs[i].met2.flowSepNodes, thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].flowSepNodes, WD_sec);
                            if (UW_or_DW == "UW") {
                                flow1 = model.GetFlowType(UW1, DW1, WD_sec, "UW", metPairs[i].met2.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                flow2 = model.GetFlowType(UW2, DW2, WD_sec, "UW", thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                            }
                            else {
                                flow1 = model.GetFlowType(UW1, DW1, WD_sec, "DW", metPairs[i].met2.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                flow2 = model.GetFlowType(UW2, DW2, WD_sec, "DW", thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                            }

                            if ((flow1 == flowType) || (flow2 == flowType))
                            {
                                Array.Resize(ref coeffs, coeffCount + 1);
                                Array.Resize(ref P10_Expo, coeffCount + 1);

                                P10_Expo[coeffCount] = thisP10;
                                if (UW_or_DW == "UW")
                                    coeffs[coeffCount] = model.CalcUW_Coeff(thisP10UW, thisP10DW, WD_sec, flowType);
                                else
                                    coeffs[coeffCount] = model.CalcDW_Coeff(thisP10DW, thisP10UW, WD_sec, flowType);

                                coeffCount++;
                            }
                        }
                        else
                        { // No nodes
                            thisP10DW = (Math.Abs(metPairs[i].met1.gridStats.stats[radiusIndex].P10_DW[WD_sec]) + Math.Abs(metPairs[i].met2.gridStats.stats[radiusIndex].P10_DW[WD_sec])) / 2;
                            thisP10UW = (Math.Abs(metPairs[i].met1.gridStats.stats[radiusIndex].P10_UW[WD_sec]) + Math.Abs(metPairs[i].met2.gridStats.stats[radiusIndex].P10_UW[WD_sec])) / 2;

                            if (thisP10DW > thisP10UW)
                                thisP10 = thisP10DW;
                            else
                                thisP10 = thisP10UW;

                            UW1 = metPairs[i].met1.expo[radiusIndex].expo[WD_sec];
                            UW2 = metPairs[i].met2.expo[radiusIndex].expo[WD_sec];
                            WS1 = met1Dist.WS * met1Dist.sectorWS_Ratio[WD_sec];
                            DW1 = metPairs[i].met1.expo[radiusIndex].GetDW_Param(WD_sec, "Expo");
                            DW2 = metPairs[i].met2.expo[radiusIndex].GetDW_Param(WD_sec, "Expo");

                            if (thisInst.topo.useSepMod == true) flowSep = nodeList.GetSepNodes1and2(metPairs[i].met1.flowSepNodes, metPairs[i].met2.flowSepNodes, WD_sec);
                            if (UW_or_DW == "UW") {
                                flow1 = model.GetFlowType(UW1, DW1, WD_sec, "UW", metPairs[i].met1.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                flow2 = model.GetFlowType(UW2, DW2, WD_sec, "UW", metPairs[i].met2.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                            }
                            else {
                                flow1 = model.GetFlowType(UW1, DW1, WD_sec, "DW", metPairs[i].met1.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                flow2 = model.GetFlowType(UW2, DW2, WD_sec, "DW", metPairs[i].met2.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                            }

                            if ((flow1 == flowType) || (flow2 == flowType))
                            {
                                Array.Resize(ref coeffs, coeffCount + 1);
                                Array.Resize(ref P10_Expo, coeffCount + 1);

                                P10_Expo[coeffCount] = thisP10;
                                if (UW_or_DW == "UW")
                                    coeffs[coeffCount] = model.CalcUW_Coeff(thisP10UW, thisP10DW, WD_sec, flowType);
                                else
                                    coeffs[coeffCount] = model.CalcDW_Coeff(thisP10DW, thisP10UW, WD_sec, flowType);

                                coeffCount++;
                            }
                        }
                    }
                    else {
                        // gets all directions
                        for (int WD = 0; WD < numWD; WD++)
                        {
                            if (thisWS_Est.nodePath.Length > 0)
                            {
                                thisP10DW = (Math.Abs(metPairs[i].met1.gridStats.stats[radiusIndex].P10_DW[WD]) + Math.Abs(thisWS_Est.nodePath[0].gridStats.stats[radiusIndex].P10_DW[WD])) / 2;
                                thisP10UW = (Math.Abs(metPairs[i].met1.gridStats.stats[radiusIndex].P10_UW[WD]) + Math.Abs(thisWS_Est.nodePath[0].gridStats.stats[radiusIndex].P10_UW[WD])) / 2;

                                if (thisP10DW > thisP10UW)
                                    thisP10 = thisP10DW;
                                else
                                    thisP10 = thisP10UW;

                                UW1 = metPairs[i].met1.expo[radiusIndex].expo[WD];
                                UW2 = thisWS_Est.nodePath[0].expo[radiusIndex].expo[WD];
                                WS1 = met1Dist.WS * met1Dist.sectorWS_Ratio[WD];
                                DW1 = metPairs[i].met1.expo[radiusIndex].GetDW_Param(WD, "Expo");
                                DW2 = thisWS_Est.nodePath[0].expo[radiusIndex].GetDW_Param(WD, "Expo");

                                if (thisInst.topo.useSepMod == true) flowSep = nodeList.GetSepNodes1and2(metPairs[i].met1.flowSepNodes, thisWS_Est.nodePath[0].flowSepNodes, WD);
                                if (UW_or_DW == "UW")
                                {
                                    flow1 = model.GetFlowType(UW1, DW1, WD, "UW", metPairs[i].met1.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                    flow2 = model.GetFlowType(UW2, DW2, WD, "UW", thisWS_Est.nodePath[0].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                }
                                else {
                                    flow1 = model.GetFlowType(UW1, DW1, WD, "DW", metPairs[i].met1.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                    flow2 = model.GetFlowType(UW2, DW2, WD, "DW", thisWS_Est.nodePath[0].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                }


                                if ((flow1 == flowType) || (flow2 == flowType))
                                {
                                    Array.Resize(ref coeffs, coeffCount + 1);
                                    Array.Resize(ref P10_Expo, coeffCount + 1);

                                    P10_Expo[coeffCount] = thisP10;

                                    if (UW_or_DW == "UW")
                                        coeffs[coeffCount] = model.CalcUW_Coeff(thisP10UW, thisP10DW, WD, flowType);
                                    else
                                        coeffs[coeffCount] = model.CalcDW_Coeff(thisP10DW, thisP10UW, WD, flowType);

                                    coeffCount++;
                                }

                                for (int n = 1; n <= thisWS_Est.nodePath.Length - 1; n++)
                                {
                                    thisP10DW = (Math.Abs(thisWS_Est.nodePath[n - 1].gridStats.stats[radiusIndex].P10_DW[WD]) + Math.Abs(thisWS_Est.nodePath[n].gridStats.stats[radiusIndex].P10_DW[WD])) / 2;
                                    thisP10UW = (Math.Abs(thisWS_Est.nodePath[n - 1].gridStats.stats[radiusIndex].P10_UW[WD]) + Math.Abs(thisWS_Est.nodePath[n].gridStats.stats[radiusIndex].P10_UW[WD])) / 2;

                                    if (thisP10DW > thisP10UW)
                                        thisP10 = thisP10DW;
                                    else
                                        thisP10 = thisP10UW;

                                    UW1 = thisWS_Est.nodePath[n - 1].expo[radiusIndex].expo[WD];
                                    UW2 = thisWS_Est.nodePath[n].expo[radiusIndex].expo[WD];
                                    DW1 = thisWS_Est.nodePath[n - 1].expo[radiusIndex].GetDW_Param(WD, "Expo");
                                    DW2 = thisWS_Est.nodePath[n].expo[radiusIndex].GetDW_Param(WD, "Expo");

                                    if (thisInst.topo.useSepMod == true) flowSep = nodeList.GetSepNodes1and2(thisWS_Est.nodePath[n - 1].flowSepNodes, thisWS_Est.nodePath[n].flowSepNodes, WD);

                                    if (UW_or_DW == "UW")
                                    {
                                        flow1 = model.GetFlowType(UW1, DW1, WD, "UW", thisWS_Est.nodePath[n - 1].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                        flow2 = model.GetFlowType(UW2, DW2, WD, "UW", thisWS_Est.nodePath[n].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                    }
                                    else {
                                        flow1 = model.GetFlowType(UW1, DW1, WD, "DW", thisWS_Est.nodePath[n - 1].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                        flow2 = model.GetFlowType(UW2, DW2, WD, "DW", thisWS_Est.nodePath[n].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                    }


                                    if ((flow1 == flowType) || (flow2 == flowType))
                                    {
                                        Array.Resize(ref coeffs, coeffCount + 1);
                                        Array.Resize(ref P10_Expo, coeffCount + 1);

                                        P10_Expo[coeffCount] = thisP10;

                                        if (UW_or_DW == "UW")
                                            coeffs[coeffCount] = model.CalcUW_Coeff(thisP10UW, thisP10DW, WD, flowType);
                                        else
                                            coeffs[coeffCount] = model.CalcDW_Coeff(thisP10DW, thisP10UW, WD, flowType);

                                        coeffCount++;
                                    }

                                }


                                thisP10DW = (Math.Abs(metPairs[i].met2.gridStats.stats[radiusIndex].P10_DW[WD]) + Math.Abs(thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].gridStats.stats[radiusIndex].P10_DW[WD])) / 2;
                                thisP10UW = (Math.Abs(metPairs[i].met2.gridStats.stats[radiusIndex].P10_UW[WD]) + Math.Abs(thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].gridStats.stats[radiusIndex].P10_UW[WD])) / 2;

                                if (thisP10DW > thisP10UW)
                                    thisP10 = thisP10DW;
                                else
                                    thisP10 = thisP10UW;

                                UW1 = metPairs[i].met2.expo[radiusIndex].expo[WD];
                                UW2 = thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].expo[radiusIndex].expo[WD];
                                WS1 = met2Dist.WS * met2Dist.sectorWS_Ratio[WD];
                                DW1 = metPairs[i].met2.expo[radiusIndex].GetDW_Param(WD, "Expo");
                                DW2 = thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].expo[radiusIndex].GetDW_Param(WD, "Expo");

                                if (thisInst.topo.useSepMod == true) flowSep = nodeList.GetSepNodes1and2(metPairs[i].met2.flowSepNodes, thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].flowSepNodes, WD);

                                if (UW_or_DW == "UW") {
                                    flow1 = model.GetFlowType(UW1, DW1, WD, "UW", metPairs[i].met2.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                    flow2 = model.GetFlowType(UW2, DW2, WD, "UW", thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                }
                                else {
                                    flow1 = model.GetFlowType(UW1, DW1, WD, "DW", metPairs[i].met2.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                    flow2 = model.GetFlowType(UW2, DW2, WD, "DW", thisWS_Est.nodePath[thisWS_Est.nodePath.Length - 1].flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                }
                                
                                if ((flow1 == flowType) || (flow2 == flowType))
                                {
                                    Array.Resize(ref coeffs, coeffCount + 1);
                                    Array.Resize(ref P10_Expo, coeffCount + 1);

                                    P10_Expo[coeffCount] = thisP10;

                                    if (UW_or_DW == "UW")
                                        coeffs[coeffCount] = model.CalcUW_Coeff(thisP10UW, thisP10DW, WD, flowType);
                                    else
                                        coeffs[coeffCount] = model.CalcDW_Coeff(thisP10DW, thisP10UW, WD, flowType);

                                    coeffCount++;
                                }

                            }

                            else {
                                thisP10DW = (Math.Abs(metPairs[i].met1.gridStats.stats[radiusIndex].P10_DW[WD]) + Math.Abs(metPairs[i].met2.gridStats.stats[radiusIndex].P10_DW[WD])) / 2;
                                thisP10UW = (Math.Abs(metPairs[i].met1.gridStats.stats[radiusIndex].P10_UW[WD]) + Math.Abs(metPairs[i].met2.gridStats.stats[radiusIndex].P10_UW[WD])) / 2;

                                if (thisP10DW > thisP10UW)
                                    thisP10 = thisP10DW;
                                else
                                    thisP10 = thisP10UW;

                                UW1 = metPairs[i].met1.expo[radiusIndex].expo[WD];
                                UW2 = metPairs[i].met2.expo[radiusIndex].expo[WD];
                                WS1 = met1Dist.WS * met1Dist.sectorWS_Ratio[WD];
                                DW1 = metPairs[i].met1.expo[radiusIndex].GetDW_Param(WD, "Expo");
                                DW2 = metPairs[i].met2.expo[radiusIndex].GetDW_Param(WD, "Expo");

                                if (thisInst.topo.useSepMod == true) flowSep = nodeList.GetSepNodes1and2(metPairs[i].met1.flowSepNodes, metPairs[i].met2.flowSepNodes, WD);

                                if (UW_or_DW == "UW")
                                {
                                    flow1 = model.GetFlowType(UW1, DW1, WD, "UW", metPairs[i].met1.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                    flow2 = model.GetFlowType(UW2, DW2, WD, "UW", metPairs[i].met2.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                }
                                else {
                                    flow1 = model.GetFlowType(UW1, DW1, WD, "DW", metPairs[i].met1.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                    flow2 = model.GetFlowType(UW2, DW2, WD, "DW", metPairs[i].met2.flowSepNodes, WS1, thisInst.topo.useSepMod, radiusIndex);
                                }


                                if ((flow1 == flowType) || (flow2 == flowType))
                                {
                                    Array.Resize(ref coeffs, coeffCount + 1);
                                    Array.Resize(ref P10_Expo, coeffCount + 1);

                                    P10_Expo[coeffCount] = thisP10;

                                    if (UW_or_DW == "UW")
                                        coeffs[coeffCount] = model.CalcUW_Coeff(thisP10UW, thisP10DW, WD, flowType);
                                    else
                                        coeffs[coeffCount] = model.CalcDW_Coeff(thisP10DW, thisP10UW, WD, flowType);

                                    coeffCount++;
                                }
                            }
                        }
                    }
                }
            }

            if (coeffs != null)
            {
                coeffsAndP10Expos = new double[2, coeffs.Length];

                for (int i = 0; i < coeffs.Length; i++)
                {
                    coeffsAndP10Expos[0, i] = coeffs[i];
                    coeffsAndP10Expos[1, i] = P10_Expo[i];
                }
            }

            return coeffsAndP10Expos;
        }

        public double[,] GetSRandStabCorrsPlot(Model model, ModelCollection modelList, NodeCollection nodeList, int WD_sec, string flowType, string UW_or_DW)
        {
            // Returns surface roughness and stability correction for specified flow type and WD sector
            double[,] stabCorrsAndSurfaceRough = new double[2, 1];   // Col 1: Surface roughness; Col 2: Stability correction
            int coeffCount = 0;
            // Dim Last_count  int = 0
            int numWD = 0;

            try {
                numWD = model.downhill_A.Length;
            }
            catch 
            {
                return null;
            }
                       

            double[] SR = null;
            double[] Stab_corr = null;                        

            if (PairCount == 0)
                return null;
                        
            Pair_Of_Mets.WS_CrossPreds thisWS_Est = new Pair_Of_Mets.WS_CrossPreds();
            int radiusIndex = 0;
            bool foundWS_Est = false;            
            double thisDW;
            double thisUW;
            
            for (int i = 0; i < PairCount; i++)
            {
                // Check to see if met pair is in UWDW model
                bool bothMetsUsed = BothInModel(metPairs[i], model);

                if (bothMetsUsed == true)
                {
                    foundWS_Est = false;
                    for (int j = 0; j < metPairs[i].WS_PredCount; j++)
                    {
                        for (int k = 0; k <= metPairs[i].WS_Pred.GetUpperBound(1); k++)
                            if (modelList.IsSameModel(metPairs[i].WS_Pred[j, k].model, model))
                            {
                                thisWS_Est = metPairs[i].WS_Pred[j, k];
                                radiusIndex = k;
                                foundWS_Est = true;
                                break;
                            }

                        if (foundWS_Est == true)
                            break;
                    }

                    if (WD_sec < numWD)
                    {
                        thisUW = metPairs[i].met1.expo[radiusIndex].expo[WD_sec];
                        thisDW = metPairs[i].met1.expo[radiusIndex].GetDW_Param(WD_sec, "Expo");

                        Array.Resize(ref SR, coeffCount + 1);
                        Array.Resize(ref Stab_corr, coeffCount + 1);

                        SR[coeffCount] = metPairs[i].met1.expo[radiusIndex].SR[WD_sec];
                        Stab_corr[coeffCount] = model.GetStabilityCorrection(thisUW, thisDW, WD_sec, SR[coeffCount], false, UW_or_DW);

                        coeffCount++;

                        if (thisWS_Est.nodePath.Length > 0)
                        {
                            for (int n = 0; n < thisWS_Est.nodePath.Length; n++)
                            {
                                thisUW = thisWS_Est.nodePath[n].expo[radiusIndex].expo[WD_sec];
                                thisDW = thisWS_Est.nodePath[n].expo[radiusIndex].GetDW_Param(WD_sec, "Expo");

                                Array.Resize(ref SR, coeffCount + 1);
                                Array.Resize(ref Stab_corr, coeffCount + 1);

                                SR[coeffCount] = thisWS_Est.nodePath[n].expo[radiusIndex].SR[WD_sec];
                                Stab_corr[coeffCount] = model.GetStabilityCorrection(thisUW, thisDW, WD_sec, SR[coeffCount], false, UW_or_DW);
                                coeffCount++;
                            }

                        }

                        thisUW = metPairs[i].met2.expo[radiusIndex].expo[WD_sec];
                        thisDW = metPairs[i].met2.expo[radiusIndex].GetDW_Param(WD_sec, "Expo");

                        Array.Resize(ref SR, coeffCount + 1);
                        Array.Resize(ref Stab_corr, coeffCount + 1);

                        SR[coeffCount] = metPairs[i].met2.expo[radiusIndex].SR[WD_sec];
                        Stab_corr[coeffCount] = model.GetStabilityCorrection(thisUW, thisDW, WD_sec, SR[coeffCount], false, UW_or_DW);
                    }
                    else
                    {
                        for (int WD = 0; WD < numWD; WD++)
                        {
                            thisUW = metPairs[i].met2.expo[radiusIndex].expo[WD];
                            thisDW = metPairs[i].met2.expo[radiusIndex].GetDW_Param(WD, "Expo");

                            Array.Resize(ref SR, coeffCount + 1);
                            Array.Resize(ref Stab_corr, coeffCount + 1);

                            SR[coeffCount] = metPairs[i].met1.expo[radiusIndex].SR[WD];
                            Stab_corr[coeffCount] = model.GetStabilityCorrection(thisUW, thisDW, WD, SR[coeffCount], false, UW_or_DW);
                            coeffCount++;

                            if (thisWS_Est.nodePath.Length > 0)
                            { 
                                for (int n = 0; n < thisWS_Est.nodePath.Length; n++)
                                {
                                    thisUW = thisWS_Est.nodePath[n].expo[radiusIndex].expo[WD];
                                    thisDW = thisWS_Est.nodePath[n].expo[radiusIndex].GetDW_Param(WD, "Expo");

                                    Array.Resize(ref SR, coeffCount + 1);
                                    Array.Resize(ref Stab_corr, coeffCount + 1);

                                    SR[coeffCount] = thisWS_Est.nodePath[n].expo[radiusIndex].SR[WD];
                                    Stab_corr[coeffCount] = model.GetStabilityCorrection(thisUW, thisDW, WD, SR[coeffCount], false, UW_or_DW);
                                    coeffCount++;
                                }
                            }

                            thisUW = metPairs[i].met2.expo[radiusIndex].expo[WD];

                            if (WD < numWD / 2)
                                thisDW = metPairs[i].met2.expo[radiusIndex].expo[WD + numWD / 2];
                            else 
                                thisDW = metPairs[i].met2.expo[radiusIndex].expo[WD - numWD / 2];

                            Array.Resize(ref SR, coeffCount + 1);
                            Array.Resize(ref Stab_corr, coeffCount + 1);

                            SR[coeffCount] = metPairs[i].met2.expo[radiusIndex].SR[WD];
                            Stab_corr[coeffCount] = model.GetStabilityCorrection(thisUW, thisDW, WD, SR[coeffCount], false, UW_or_DW);
                            coeffCount++;
                        }
                    }
                }

            }

            if (SR != null)
            {
                stabCorrsAndSurfaceRough = new double[2, SR.Length];

                for (int i = 0; i < SR.Length; i++)
                {
                    stabCorrsAndSurfaceRough[0, i] = Stab_corr[i];
                    stabCorrsAndSurfaceRough[1, i] = SR[i];
                }
            }

            return stabCorrsAndSurfaceRough;
        }

        public bool BothInModel(Pair_Of_Mets thisPair, Model model)
        {
            // Returns true if both mets are used in model
            bool bothInModel = false;
            bool met1InModel = false;
            bool met2InModel = false;

            for (int i = 0; i < model.metsUsed.Length; i++)
            { 
                if (thisPair.met1.name == model.metsUsed[i]) 
                    met1InModel = true;                

                if (thisPair.met2.name == model.metsUsed[i]) 
                    met2InModel = true;                
            }

            if (met1InModel == true && met2InModel == true ) 
                bothInModel = true;

            return bothInModel;
        }

        public string[,] GetAllCombos(string[] listOfMets, int numAllMets, int numMetsInModel, int numModels)
        {
            // Returns all possible combinations of met sites with specified subset size
            string[,] metsForModel = new string[numMetsInModel, numModels];
            int[,] metIndForModel = new int[numMetsInModel, numModels];
            int modelInd = 0;

            // create arrays of combos of mets
            for (int j = 0; j <= numAllMets - numMetsInModel; j++)
            {               
                for (int k = 0; k < numMetsInModel; k++) // this creates the first entry which would be the first N mets (where N is num_mets_in_UWDW)
                    metIndForModel[k, modelInd] = j + k;

                modelInd++;

                for (int k = numMetsInModel - 1; k >= 1; k--) // the alg goes to the end of the met list and, if it's less than possible max value 
                {
                    // (i.e. Num_All - Num_Mets_in_UWDW + k, which is position of met in list) then it scrolls through each new combination and adds to array of met lists
                    while (metIndForModel[k, modelInd - 1] < numAllMets - numMetsInModel + k)
                    {
                        for (int m = 0; m <= k - 1; m++)
                            metIndForModel[m, modelInd] = metIndForModel[m, modelInd - 1];

                        metIndForModel[k, modelInd] = metIndForModel[k, modelInd - 1] + 1;

                        for (int m = k + 1; m < numMetsInModel; m++)
                            metIndForModel[m, modelInd] = metIndForModel[m - 1, modelInd] + 1;

                        modelInd++;

                        // now goes back to the end of the list and works back to last position that was changed and creates new met lists for each possible value
                        for (int l = numMetsInModel - 1; l >= k + 1; l--)
                        {
                            while (metIndForModel[l, modelInd - 1] < numAllMets - numMetsInModel + l)
                            {
                                for (int m = 0; m <= l - 1; m++)
                                    metIndForModel[m, modelInd] = metIndForModel[m, modelInd - 1];

                                metIndForModel[l, modelInd] = metIndForModel[l, modelInd - 1] + 1;

                                for (int m = l + 1; m < numMetsInModel; m++)
                                    metIndForModel[m, modelInd] = metIndForModel[m - 1, modelInd] + 1;

                                modelInd++;

                                if (l < numMetsInModel - 1)
                                    if (metIndForModel[l, modelInd - 1] < numAllMets - numMetsInModel + l && metIndForModel[l + 1, modelInd - 1] < numAllMets - numMetsInModel + (l + 1))
                                        // need to go back to end of list
                                        l = numMetsInModel - 1;

                            }

                        }
                    }
                }

            }

            // Have an array of met indices now need to fill array with names of mets
            for (int i = 0; i < numMetsInModel; i++)
                for (int j = 0; j < numModels; j++)
                    metsForModel[i, j] = listOfMets[metIndForModel[i, j]];               
            
            return metsForModel;
        }

        public void AddRoundRobinEst(RR_funct_obj[] thisRR_Obj, string[] theseMetsUsed, int metSubSize, string[,] theseMetsInModel, bool IsWeighted, MetCollection metList)
        {
            // Adds a Round Robin estimate to list
            int thisCount = RoundRobinCount;
            Array.Resize(ref roundRobinEsts, thisCount + 1);

            int numRR_Objs = thisRR_Obj.Length;
            int numToPredict = thisRR_Obj[0].omittedMets.Length;
            int numRadii = thisRR_Obj[0].models.Length;
            Avg_Est[,] theseAvgWS = new Avg_Est[numToPredict, numRR_Objs];
            double[] theseRMS_Err = new double[numRR_Objs];
            Model[,] theseModels = new Model[numRR_Objs, numRadii];
            
            for (int i = 0; i < numRR_Objs; i++)
            { 
                for (int j = 0; j < numRadii; j++)
                    theseModels[i,j] = thisRR_Obj[i].models[j];

                for (int j = 0; j < numToPredict; j++)
                {
                    theseAvgWS[j, i].predictee = thisRR_Obj[i].omittedMets[j].name;
                    theseAvgWS[j, i].avgWS = thisRR_Obj[i].omittedMets[j].avgWS_Est[0].freeStream.WS;
                    theseAvgWS[j, i].estError = thisRR_Obj[i].errsAtOmittedMets[j];
                    theseAvgWS[j, i].WS_Count = thisRR_Obj[i].omittedMets[j].WSEst_Count;
                }                    

                theseRMS_Err[i] = thisRR_Obj[i].errRMS;
            }

            roundRobinEsts[thisCount] = new RR_WS_Ests();
            roundRobinEsts[thisCount].metsUsed = theseMetsUsed;
            roundRobinEsts[thisCount].metSubSize = metSubSize;            
            roundRobinEsts[thisCount].avgWS_Ests = theseAvgWS;
            roundRobinEsts[thisCount].model = theseModels;
            roundRobinEsts[thisCount].metsInModel = theseMetsInModel;
            roundRobinEsts[thisCount].RMS_Err = theseRMS_Err;
                        
            CalcRR_RMS_All(thisCount);

        }

        /// <summary>
        /// Does one RR calc: One set of mets out of bigger set. Gets (or creates) UWDW models to use then uses them to estimate at all mets not included in set
        ///Conducts Round Robin calculations using sub-set of mets.metsUsed is list of all mets in Continuum model and theseMetsInModel is list of subset of mets which
        /// are used to create a model. A temporary turbine object is created for the predicted met and the wind speed is predicted at temp turbine site
        /// </summary>
        public RR_funct_obj DoRR_Calc(string[] theseMetsInModel, Continuum thisInst, string[] metsUsed, string MCP_Method)
        {           
            RR_funct_obj thisRR_FuncObj = new RR_funct_obj();
         
            int numMetsToPredict = metsUsed.Length - theseMetsInModel.Length;
            thisRR_FuncObj.omittedMets = new Turbine[numMetsToPredict];
            thisRR_FuncObj.errsAtOmittedMets = new double[numMetsToPredict];
            thisRR_FuncObj.actualWS_AtMets = new double[numMetsToPredict];

            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
       
            int predMetInd = 0;
            // Populate omittedMets array (Turbine class) with Met coordinates, elevation, exposure, and grid stats. 
            // Populate actualWS_AtMets array with met average wind speeds
            for (int m = 0; m < thisInst.metList.ThisCount; m++)
            {
                bool isMetInModel = false;
                
                for (int n = 0; n < theseMetsInModel.Length; n++)
                {
                    if (thisInst.metList.metItem[m].name == theseMetsInModel[n])
                    {
                        isMetInModel = true;
                        break;
                    }
                }

                if (isMetInModel == false)
                {
                    thisRR_FuncObj.omittedMets[predMetInd] = new Turbine();
                    thisRR_FuncObj.omittedMets[predMetInd].name = thisInst.metList.metItem[m].name;
                    thisRR_FuncObj.omittedMets[predMetInd].UTMX = thisInst.metList.metItem[m].UTMX;
                    thisRR_FuncObj.omittedMets[predMetInd].UTMY = thisInst.metList.metItem[m].UTMY;
                    thisRR_FuncObj.omittedMets[predMetInd].elev = thisInst.metList.metItem[m].elev;

                    thisRR_FuncObj.omittedMets[predMetInd].expo = thisInst.metList.metItem[m].expo;
                    thisRR_FuncObj.omittedMets[predMetInd].gridStats = thisInst.metList.metItem[m].gridStats;
                    Met.WSWD_Dist thisDist = thisInst.metList.metItem[m].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                    thisRR_FuncObj.actualWS_AtMets[predMetInd] = thisDist.WS;
                                    
                    predMetInd++;
                }
            }

            // Create models using TOD and Season bins
            thisRR_FuncObj.models = thisInst.modelList.GetAllModels(thisInst, theseMetsInModel);

            // For each met to predict, generate wind speed estimate using models and same method used to calculate turbine estimates. Then calculate wind speed estimate error            
            for (int i = 0; i < numMetsToPredict; i++)
            {
                // Average distribution model
                if (thisInst.metList.isTimeSeries == false || thisInst.metList.isMCPd == false || thisInst.turbineList.genTimeSeries == false)
                {
                    thisRR_FuncObj.omittedMets[i].DoTurbineCalcs(thisInst, thisRR_FuncObj.models);
                    double[] windRose = thisInst.metList.GetInterpolatedWindRose(theseMetsInModel, thisRR_FuncObj.omittedMets[i].UTMX, thisRR_FuncObj.omittedMets[i].UTMY, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
                    thisRR_FuncObj.omittedMets[i].GenerateAvgWSFromTABs(thisInst, thisRR_FuncObj.models, windRose, true);
                }
                else
                { // Time Series model
                    NodeCollection nodeList = new NodeCollection();
                    Nodes targetNode = nodeList.GetTurbNode(thisRR_FuncObj.omittedMets[i]);
                    
                    ModelCollection.TimeSeries[] thisTS = thisInst.modelList.GenerateTimeSeries(thisInst, theseMetsInModel, targetNode, 
                        new TurbineCollection.PowerCurve(), null, null, MCP_Method);
                    thisRR_FuncObj.omittedMets[i].GenerateAvgWSTimeSeries(thisTS, thisInst, new Wake_Model(), false, MCP_Method, true, new TurbineCollection.PowerCurve()); // Creates and adds new Avg_Est based on time series data
                }    
                                
                thisRR_FuncObj.errsAtOmittedMets[i] = (thisRR_FuncObj.omittedMets[i].avgWS_Est[0].freeStream.WS - thisRR_FuncObj.actualWS_AtMets[i]) / thisRR_FuncObj.actualWS_AtMets[i];
            }          

            // Calculate average WS estimates and error at all omitted sites
            for (int m = 0; m < numMetsToPredict; m++)
                thisRR_FuncObj.errRMS = thisRR_FuncObj.errRMS + Math.Pow(thisRR_FuncObj.errsAtOmittedMets[m], 2);
            
            thisRR_FuncObj.errRMS = Math.Pow((thisRR_FuncObj.errRMS / numMetsToPredict), 0.5);
            return thisRR_FuncObj;
        }

        public void CalcRR_RMS_All(int RR_ind)
        {
            // Calculates the RMS of Round Robin estimate errors
            int numModels = roundRobinEsts[RR_ind].RMS_Err.Length;
            double RMS_All = 0;

            for (int i = 0; i < numModels; i++)
                RMS_All = RMS_All + Math.Pow(roundRobinEsts[RR_ind].RMS_Err[i], 2);

            RMS_All = Math.Pow((RMS_All / numModels), 0.5);
            roundRobinEsts[RR_ind].RMS_All = RMS_All;

        }

        public int GetNumModelsForRoundRobin(int numMets, int numMetsInModel)
        {
            int numModels = 1;

            double N_fact = 1;

            for (int i = numMets; i >= 1; i--)
                N_fact = N_fact * i;

            double K_fact = 1;
            for (int j = numMetsInModel; j >= 1; j--)
                K_fact = K_fact * j;

            double N_minus_k_fact = 1;
            for (int j = (numMets - numMetsInModel); j >= 1; j--)
                N_minus_k_fact = N_minus_k_fact * j;

            numModels = (int)(N_fact / (K_fact * N_minus_k_fact));
            return numModels;
        }
        
    }

}
