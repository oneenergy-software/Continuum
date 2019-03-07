using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuumNS
{
    [Serializable()]
    public class Exposure
    {
        public double[] expo; // Exposure calculated in each WD sector
        public double[] expoDist; // 1/dist^exponent for each WD sector (used to calculate exposures at larger radii)
        public int radius; // radius of investigation used in exposure calculation 
        public double exponent; // exponent in inverse distance weighting    
        public int numSectors = 1; // Number of sectors to average exposure over (default = 1)
        public double[] UW_P10CrossGrade; // P10 of UW crosswind slope used to determine sign of UW coeff in flow around hill algorithm
        public double[] UW_ParallelGrade; // Highest slope parallel to wind dir
        public double[] SR; // Surface roughness calculated in each WD sector
        public double[]  dispH; // Displacement height calculated in each WD sector
        public double[]  SR_Dist; // 1/dist^exponent for each WD sector (used to calculate SRDH at larger radii)

        // add function to return overall UW, DW, SR, DH
        
        public double GetOverallValue(double[] windRose, string paramType, string UW_or_DW) // Param_types: 'Expo', 'SR', 'DH', "Cross", "Para"
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
                

    public double GetDW_Param(int WD_Ind, string paramType)
        {
            // Returns DW exposure, surface roughness or displacement for specified WD sector
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

        public double GetWgtAvg(double[] windRose, int WD_sec, string UW_or_DW, string expo_SR_DH)
        {
            double weightedAvg = 0;

            // Returns weighted average UW or DW exposure, surface roughness or displacement height
            // The average may include the two neighboring sectors with weights * windRose for neighbors

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
