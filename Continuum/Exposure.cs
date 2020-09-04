using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary> Exposure class holds calculated exposure, surface roughness, and displacement height for each wind direction sector for a specific 
    /// radius of investigation and inverse distance exponent </summary>
    [Serializable()]
    public class Exposure
    {
        /// <summary> Exposure calculated in each WD sector </summary>
        public double[] expo;
        /// <summary>  1/dist^exponent for each WD sector (used to calculate exposures at larger radii without recalculating values) </summary>
        public double[] expoDist; 
        /// <summary> Radius of investigation </summary>
        public int radius;
        /// <summary> Exponent in inverse distance weighting  </summary>
        public double exponent;
        /// <summary> Number of sectors to average exposure over (default = 1) </summary>
        public int numSectors = 1; 
        /// <summary> P10 of UW crosswind slope (to be used in flow around hill algorithm) </summary>
        public double[] UW_P10CrossGrade;
        /// <summary> Highest slope parallel to WD (to be used in flow around hill algorithm) </summary>
        public double[] UW_ParallelGrade; 
        /// <summary> Surface roughness calculated in each WD sector </summary>
        public double[] SR;  
        /// <summary> Displacement height calculated in each WD sector </summary>
        public double[]  dispH; 
        /// <summary>  1/dist^exponent for each WD sector (used to calculate SRDH at larger radii) </summary>
        public double[]  SR_Dist;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>  Gets overall UW or DW parameter (paramType = 'Expo', 'SR', 'DH', "Cross", "Para") </summary>
        public double GetOverallValue(double[] windRose, string paramType, string UW_or_DW) // Param_types: 
        {
            double overallValue = 0;
            if (windRose == null)
                return overallValue;

            int numWD = windRose.Length;

            if (paramType != "Expo" && paramType != "SR" && paramType != "DH")
            {
                MessageBox.Show("Error calculating overall value. Specify 'Expo', 'SR', or 'DH'. Value passed = " + paramType);
                return overallValue;
            }
            else if (paramType == "Expo")
            {
                if (UW_or_DW == "UW")
                    for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                        overallValue = overallValue + expo[WD_Ind] * windRose[WD_Ind];
                else
                    for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                        overallValue = overallValue + GetDW_Param(WD_Ind, "Expo") * windRose[WD_Ind];

            }
            else if (paramType == "SR")
            {
                if (UW_or_DW == "UW")
                    for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                        overallValue = overallValue + SR[WD_Ind] * windRose[WD_Ind];
                else
                    for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                        overallValue = overallValue + GetDW_Param(WD_Ind, "SR") * windRose[WD_Ind];
            }
            else if (paramType == "DH")
            {
                if (UW_or_DW == "UW")
                    for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                        overallValue = overallValue + dispH[WD_Ind] * windRose[WD_Ind];
                else
                    for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                        overallValue = overallValue + GetDW_Param(WD_Ind, "DH") * windRose[WD_Ind];
            }
            else if (paramType == "Cross")
            {
                if (UW_or_DW == "UW")
                    for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                        overallValue = overallValue + UW_P10CrossGrade[WD_Ind] * windRose[WD_Ind];
                else
                    for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                        overallValue = overallValue + GetDW_Param(WD_Ind, "Cross") * windRose[WD_Ind];
            }
            else if (paramType == "Para")
            {
                if (UW_or_DW == "UW")
                    for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                        overallValue = overallValue + UW_ParallelGrade[WD_Ind] * windRose[WD_Ind];
                else
                    for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                        overallValue = overallValue + GetDW_Param(WD_Ind, "Para") * windRose[WD_Ind];
            }

            return overallValue;
        }


        /// <summary> Returns DW parameter for specified WD sector (paramType = 'Expo', 'SR', 'DH', "Cross", "Para")  </summary> 
        public double GetDW_Param(int WD_Ind, string paramType)
        {            
            double DW_Param = 0;
            int numWD = expo.Length;

            if (WD_Ind < numWD / 2)
            {
                if (paramType == "Expo")
                    DW_Param = expo[WD_Ind + numWD / 2];
                else if (paramType == "SR") 
                    DW_Param = SR[WD_Ind + numWD / 2];
                else if (paramType == "DH") 
                    DW_Param = dispH[WD_Ind + numWD / 2];
                else if (paramType == "Cross")
                    DW_Param = UW_P10CrossGrade[WD_Ind + numWD / 2];
                else if (paramType == "Para")
                    DW_Param = UW_ParallelGrade[WD_Ind + numWD / 2];
            } 
            else
            {
                if (paramType == "Expo")
                    DW_Param = expo[WD_Ind - numWD / 2];
                else if (paramType == "SR")
                    DW_Param = SR[WD_Ind - numWD / 2];
                else if (paramType == "DH")
                    DW_Param = dispH[WD_Ind - numWD / 2];
                else if (paramType == "Cross")
                    DW_Param = UW_P10CrossGrade[WD_Ind - numWD / 2];
                else if (paramType == "Para")
                    DW_Param = UW_ParallelGrade[WD_Ind - numWD / 2];

            }

            return DW_Param;
    }

        /// <summary> Returns weighted average UW or DW exposure, surface roughness or displacement height ("Expo", "SR", "DH"). The average may include the 
        /// two neighboring sectors with weights * windRose for neighbors (currently average does not include neighboring sectors)  </summary> 
        public double GetWgtAvg(double[] windRose, int WD_sec, string UW_or_DW, string expo_SR_DH)
        {
            double weightedAvg = 0;            

            double sectWgtMinus1 = 0;
            double sectWgtPlus1 = 0;
            double sectWgt = 1;

            int numWD = windRose.Length;

            int minInd = WD_sec - 1;
            if (minInd < 0) minInd = numWD - 1;
            sectWgtMinus1 = sectWgtMinus1 * windRose[minInd];

            int maxInd = WD_sec + 1;
            if (maxInd > numWD - 1) maxInd = 0;
            sectWgtPlus1 = sectWgtPlus1 * windRose[maxInd];

            if (expo_SR_DH == "Expo")
            {
                if (UW_or_DW == "UW")
                    // Avg UW Expo
                    weightedAvg = (sectWgtMinus1 * expo[minInd] + sectWgt * expo[WD_sec] + sectWgtPlus1 * expo[maxInd])
                            / (sectWgtMinus1 + sectWgt + sectWgtPlus1);

                else // Avg DW Expo
                    weightedAvg = (sectWgtMinus1 * GetDW_Param(minInd, "Expo") + sectWgt * GetDW_Param(WD_sec, "Expo") + sectWgtPlus1 * GetDW_Param(maxInd, "Expo"))
                            / (sectWgtMinus1 + sectWgt + sectWgtPlus1);

            }
            else if (expo_SR_DH == "SR")
            {
                if (UW_or_DW == "UW")
                    // Avg UW SR
                    weightedAvg = (sectWgtMinus1 * SR[minInd] + sectWgt * SR[WD_sec] + sectWgtPlus1 * SR[maxInd])
                            / (sectWgtMinus1 + sectWgt + sectWgtPlus1);

                else // Avg DW SR
                    weightedAvg = (sectWgtMinus1 * GetDW_Param(minInd, "SR") + sectWgt * GetDW_Param(WD_sec, "SR") + sectWgtPlus1 * GetDW_Param(maxInd, "SR"))
                            / (sectWgtMinus1 + sectWgt + sectWgtPlus1);
            } 
            else if (expo_SR_DH == "DH" )
            {
                if (UW_or_DW == "UW")
                    // Avg UW DH
                    weightedAvg = (sectWgtMinus1 * dispH[minInd] + sectWgt * dispH[WD_sec] + sectWgtPlus1 * dispH[maxInd])
                            / (sectWgtMinus1 + sectWgt + sectWgtPlus1);

                else // Avg DW DH
                    weightedAvg = (sectWgtMinus1 * GetDW_Param(minInd, "DH") + sectWgt * GetDW_Param(WD_sec, "DH") + sectWgtPlus1 * GetDW_Param(maxInd, "DH"))
                            / (sectWgtMinus1 + sectWgt + sectWgtPlus1);
                
            }

            return weightedAvg;

        }

    }
}
