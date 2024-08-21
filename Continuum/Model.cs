using System;

namespace ContinuumNS
{
    /// <summary> Class with Continuum wind flow model coefficients and functions to calculate model coefficients. </summary>
    [Serializable()]
    public class Model
    {
        /// <summary> Time of day: All / Day / Night. </summary>
        public Met.TOD timeOfDay;
        /// <summary> Season: All / Winter / Spring / Summer / Fall. </summary>
        public Met.Season season;
        /// <summary> Modeled height. </summary>
        public double height;

        /// <summary> Elevation model coefficient in each WD sector </summary>
        public double[] elevCoeff;

        /// <summary> 'A' model coefficient for Downhill flow in each WD sector (m = A*P10Expo^B) </summary>
        public double[] downhill_A;
        /// <summary> 'B' model coefficient for Downhill flow in each WD sector (m = A*P10Expo^B)</summary>
        public double[] downhill_B;
        /// <summary> 'A' model coefficient for Uphill flow in each WD sector (m = A*P10Expo^B) </summary>
        public double[] uphill_A;
        /// <summary> 'B' model coefficient for Uphill flow in each WD sector (m = A*P10Expo^B) </summary>
        public double[] uphill_B;
        /// <summary> Critical upwind exposure in each WD sector </summary>
        public double[] UW_crit;
        /// <summary> 'A' model coefficient for Induced Speed-up flow in each WD sector (m = A*P10Expo^B) </summary>
        public double[] spdUp_A;
        /// <summary> 'B' model coefficient for Induced Speed-up flow in each WD sector (m = A*P10Expo^B) </summary>
        public double[] spdUp_B;
        /// <summary> Downhill flow stability correction coefficient 'A' in each WD sector (Stab. Corr. = 10^A) </summary>
        public double[] DH_Stab_A;
        /// <summary> Uphill flow stability correction coefficient 'A' in each WD sector (Stab. Corr. = 10^A) </summary>
        public double[] UH_Stab_A;
        /// <summary> Induced speed-up flow stability correction coefficient 'A' in each WD sector (Stab. Corr. = 10^A) </summary>
        public double[] SU_Stab_A;
        /// <summary> 'B' coefficient of Stability correction (not currently used) </summary>
        public double[] stabB;
        /// <summary> 'A' model coefficient for Separated flow in each WD sector (m = A*P10Expo^B)  </summary>
        public double[] sep_A_DW;
        /// <summary> 'B' model coefficient for Separated flow in each WD sector (m = A*P10Expo^B)  </summary>
        public double[] sep_B_DW;
        /// <summary> Turbulent WS factor for each WD sector where WS is modeled as a linear decrease in wind speed as a function of distance * turbWS_Fact  </summary>
        public double[] turbWS_Fact;
        /// <summary> Crtical sum of UW and DW exposure which defines wehn flow separation occurs. </summary>
        public double[] sepCrit;
        /// <summary> Crtical wind speed which defines wehn flow separation occurs. </summary>
        public double[] Sep_crit_WS;
        /// <summary> Model radius of investigation. </summary>
        public int radius;
        /// <summary> Maximum allowed elevation difference between nodes when finding path of nodes. </summary>
        public int maxElevDiff;
        /// <summary> Slope that defines the maximum allowed difference in P10 exposure when finding a path of nodes. </summary>
        public double maxP10DiffSlope;
        /// <summary> Intercept that defines the maximum allowed difference in P10 exposure when finding a path of nodes. </summary>
        public double maxP10DiffOffset;
        /// <summary> True if model coefficeints were imported by user. </summary>
        public bool isImported = false;
        /// <summary> List of met sites used to define model. </summary>
        public string[] metsUsed;
        /// <summary> RMS error of met cross-predictions. </summary>
        public double RMS_WS_Est;
        /// <summary> RMS error of sectorwise met cross-predictions </summary>
        public double[] RMS_Sect_WS_Est;
        /// <summary> Minimum Sum of UW and DW exposure which defines start of flow separation. </summary>
        public double minSumUWDW;                       

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Calculates and returns DW coefficient based on flow type (Downhill, Uphill, or Turbulent) and terrain complexity (i.e. P10_Expo) </summary>        
        public double CalcDW_Coeff(double P10_DW_Expo, double P10_UW_Expo, int WD_sec, string flowType)
        {            
            double DW_Coeff = 0;
            double P10_Expo;

            P10_UW_Expo = Math.Abs(P10_UW_Expo);
            P10_DW_Expo = Math.Abs(P10_DW_Expo);

            if (P10_DW_Expo > P10_UW_Expo)
                P10_Expo = P10_DW_Expo;
            else
                P10_Expo = P10_UW_Expo;

            if (P10_Expo < 0) P10_Expo = Math.Abs(P10_Expo);

            if (flowType == "Turbulent")  // Flow separates
                DW_Coeff = -sep_A_DW[WD_sec] * Math.Pow(P10_Expo, sep_B_DW[WD_sec]);
            else if (flowType == "Downhill")
                DW_Coeff = downhill_A[WD_sec] * Math.Pow(P10_Expo, downhill_B[WD_sec]);
            else if (flowType == "Uphill" || flowType == "Valley")
                DW_Coeff = uphill_A[WD_sec] * Math.Pow(P10_Expo, uphill_B[WD_sec]);

            if (DW_Coeff > 0.18)
                DW_Coeff = 0.18f;

            return DW_Coeff;

        }
                
        /// <summary> Calculates and returns UW coefficient based on flowType (Downhill, Uphill, SpdUp, Valley, or Turbulent) and P10_Expo. </summary>        
        public double CalcUW_Coeff(double P10_UW_Expo, double P10_DW_Expo, int WD_sec, string flowType)
        {              
            double UW_Coeff = 0;
            double P10_Expo;

            P10_UW_Expo = Math.Abs(P10_UW_Expo);
            P10_DW_Expo = Math.Abs(P10_DW_Expo);

            if (P10_DW_Expo > P10_UW_Expo)
                P10_Expo = P10_DW_Expo;
            else
                P10_Expo = P10_UW_Expo;            

            if (flowType == "Turbulent") { } // there's flow separation
                                              //   if there is upwind turbulence, the UW exposure is not used to estimate the change in wind speed
                                              // Instead, the length of the turbulent zone is estimated based on the sumUWDW of the Flow Sep.High Node
                                              // ) { Delta_WS = -turbWS_Fact(WD_Ind) * turbLength / 1500
                                              //  calculated in function: GetDeltaWS_TurbulentZone
            else if (flowType == "Downhill")
            { // Downhill flow: Negative UW and Positive DW exposure
                UW_Coeff = downhill_A[WD_sec] * Math.Pow(P10_Expo, downhill_B[WD_sec]);

                if (UW_Coeff > 0.18)
                    UW_Coeff = 0.18f;

                if (UW_Coeff < 0)
                    UW_Coeff = 0;
                else
                    UW_Coeff = -UW_Coeff;
            }
            else
            {
                if (flowType == "SpdUp")
                { // Positive UW expo, UW < UW critical Induced Speed-Up, positive coeff
                    // Based on //simple approx of speed-up over hill', linear function between change in WS and hill slope until reach //crit' slope
                    UW_Coeff = spdUp_A[WD_sec] * Math.Pow(P10_Expo, spdUp_B[WD_sec]);
                    if (UW_Coeff > 0.18) UW_Coeff = 0.18f;
                }
                else if (flowType == "Uphill")
                { // Positive UW expo, UW > UW critical, Uphill flow, negative coeff
                    UW_Coeff = -uphill_A[WD_sec] * Math.Pow(P10_Expo, uphill_B[WD_sec]);
                    if (UW_Coeff < -0.18) UW_Coeff = -0.18f;
                }
                else if (flowType == "Valley")
                { // Positive coeff:  valley gets steeper, wind speed decreases further, UW expo becomes more negative so delta WS = m_uw (>0) * delta_UW (<0)
                    UW_Coeff = uphill_A[WD_sec] * Math.Pow(P10_Expo, uphill_B[WD_sec]);
                    if (UW_Coeff > 0.18) UW_Coeff = 0.18f;
                }

            }

            return UW_Coeff;
        }

        /// <summary> Calculates and returns overall UW critical </summary>         
        public double CalcOverallUWCrit(double[] windRose)
        {            
            double overallUWCrit = 0;

            if (windRose != null)
            {
                int numWD = windRose.Length;
                double sumRose = 0;

                for (int WD = 0; WD < numWD; WD++)
                {
                    if (UW_crit[WD] != 4 && UW_crit[WD] != 500)
                    {
                        overallUWCrit = overallUWCrit + UW_crit[WD] * windRose[WD];
                        sumRose = sumRose + windRose[WD];
                    }
                }

                if (sumRose > 0)
                    overallUWCrit = overallUWCrit / sumRose;
            }

            return overallUWCrit;

        }

        /// <summary> Calculates and returns overall critical flow separation </summary>  
        public double CalcOverallSepCrit(double[] windRose)
        {             
            double overallSepCrit = 0;

            int numWD = windRose.Length;
            double sumRose = 0;

            for (int WD = 0; WD < numWD; WD++)
            {
                overallSepCrit = overallSepCrit + sepCrit[WD] * windRose[WD];
                sumRose = sumRose + windRose[WD];
            }

            if (sumRose > 0)
                overallSepCrit = overallSepCrit / sumRose;
            
            return overallSepCrit;
        }

        /// <summary> Calculates and returns overall flow separation critical wind speed </summary>  
        public double CalcOverallSepCritWS(double[] windRose)
        {           
            double overallSepCritWS = 0;
            int numWD = windRose.Length;
            double sumRose = 0;

            for (int WD = 0; WD < numWD; WD++)
            {
                overallSepCritWS = overallSepCritWS + Sep_crit_WS[WD] * windRose[WD];
                sumRose = sumRose + windRose[WD];
            }

            if (sumRose > 0)
                overallSepCritWS = overallSepCritWS / sumRose;

            return overallSepCritWS;
        }

        /// <summary> Calculates and returns stability correction factor used in surface roughness model </summary> 
        public double GetStabilityCorrection(double UW_Expo, double DW_Expo, int WD_Ind, double SR, bool useFlowSep, string UW_or_DW, bool useValley)
        {             
            double Stab_corr = 0;
            NodeCollection.Sep_Nodes[] Flow_Sep_Node = null;

            string flowType = GetFlowType(UW_Expo, DW_Expo, WD_Ind, UW_or_DW, Flow_Sep_Node, 0, useFlowSep, 0, useValley);

            if (flowType == "Downhill")  // Downhill
                Stab_corr = Math.Pow(10, DH_Stab_A[WD_Ind]);
            else if (flowType == "Uphill" || flowType == "Valley")  // Uphill
                Stab_corr = Math.Pow(10, UH_Stab_A[WD_Ind]);
            else if (flowType == "SpdUp")  //  Induced Speed-up 
                Stab_corr = Math.Pow(10, SU_Stab_A[WD_Ind]);
            else  // Turbulent
                Stab_corr = 5;

            return Stab_corr;
        }

        /// <summary> Determines and returns flow type upwind or downwind of site: Downhill, Uphill, Induced Speed-up, Valley or Turbulent </summary>        
        public string GetFlowType(double thisUW, double thisDW, int WD_sec, string UW_or_DW, NodeCollection.Sep_Nodes[] flowSepNodes, double thisWS, bool useFlowSep, int radiusInd, bool useValley)
        {             
            string flowType = "";
            bool isTurbulent = false;
            NodeCollection.Sep_Nodes thisFS_Node = new NodeCollection.Sep_Nodes();
            int numFS = 0;

            if (flowSepNodes != null)
                numFS = flowSepNodes.Length;

            for (int FS_ind = 0; FS_ind < numFS; FS_ind++)
            {
                if (flowSepNodes[FS_ind].flowSepWD == WD_sec)
                {
                    thisFS_Node = flowSepNodes[FS_ind];
                    break;
                }
            }

            if (UW_or_DW == "UW" && thisUW < 10) // can only be turbulent in UW direction if UW exposure < 10 (initially, UW exposure had to be negative
                                                  // but found that some cases with turbulent UW conditions had a slightly positive UW exposure)
            {
                if (thisFS_Node.highNode == null && thisFS_Node.turbEndNode == null)
                    isTurbulent = false;
                else if (thisFS_Node.highNode.expo[radiusInd].expo[WD_sec] + thisFS_Node.highNode.expo[radiusInd].GetDW_Param(WD_sec, "Expo") > sepCrit[WD_sec]
                   && thisWS > Sep_crit_WS[WD_sec] && useFlowSep == true)
                    isTurbulent = true;
                else
                    isTurbulent = false;
            }
            else if (UW_or_DW == "DW" && thisDW > 0 && thisUW > 0)
            {
                if (thisUW + thisDW > sepCrit[WD_sec] && thisWS > Sep_crit_WS[WD_sec] && useFlowSep == true)
                    isTurbulent = true;
                else
                    isTurbulent = false;

            }

            if (isTurbulent == false)
            {
                if ((thisDW >= 0 && UW_or_DW == "DW") || (thisUW < 0 && thisDW > 0 && UW_or_DW == "UW"))
                    flowType = "Downhill";
                else if (thisUW > UW_crit[WD_sec] || (thisDW < 0 && UW_or_DW == "DW"))
                    flowType = "Uphill";
                else if (thisUW >= 0 && thisUW <= UW_crit[WD_sec] && UW_or_DW == "UW")
                    flowType = "SpdUp";
                else if (thisUW < 0 && thisDW <= 0 && useValley)
                    flowType = "Valley";
                else if (thisUW < 0 && thisDW <= 0)
                    flowType = "Downhill";
            }
            else
                flowType = "Turbulent";

            return flowType;
        }

        /// <summary> Calculates and returns length of upwind turbulent zone </summary> 
        public double CalcTurbulentZoneLength(double sumUWDW)
        {            
            double Turb_zone_length = 8 * sumUWDW;

            if (Turb_zone_length > 2000)
                Turb_zone_length = 2000;

            return Turb_zone_length;
        }

        /// <summary> Calculates and returns change in wind speed from either upwind high node to site or from upwind high node to upwind end of turbulent zone </summary> 
        public double GetDeltaWS_TurbulentZone(double turbLength, int WD_Ind)
        {            
            double Delta_WS = -turbWS_Fact[WD_Ind] * turbLength / 1500;

            return Delta_WS;
        }

        /// <summary> Sets the model coefficients to default values </summary> 
        public void SetDefaultModelCoeffs(int numWD)
        {            
            for (int i = 0; i < numWD; i++)
            {
                elevCoeff[i] = 0.005;

                downhill_A[i] = 0.1432f; // old DW_A = 0.2551
                downhill_B[i] = -0.6537f; // old DW_B = -0.774

                uphill_A[i] = 0.0819f; // old UW_A = 0.1551
                uphill_B[i] = -0.6242f; // old UW_B = -0.774

                spdUp_A[i] = 0.0165f; // old SU A = 0.03
                spdUp_B[i] = -0.5966f; // old SU B = -0.774
                UW_crit[i] = 21f;                               

                DH_Stab_A[i] = 2f;
                UH_Stab_A[i] = 2f;
                SU_Stab_A[i] = 2;
                stabB[i] = 0f;

                sep_A_DW[i] = 0.0819f;
                sep_B_DW[i] = -0.6242f;
                turbWS_Fact[i] = 2f;
                sepCrit[i] = 250f;
                Sep_crit_WS[i] = 4f;                          
                
            }
        }

        /// <summary> Sets default maximum elevation and max exposure difference and minimum flow separation point </summary> 
        public void SetDefaultLimits()
        {            
            maxElevDiff = 50;
            maxP10DiffSlope = 0.4f;
            maxP10DiffOffset = 10f;
            minSumUWDW = 150f;
        }

        /// <summary> Sizes model coefficient arrays </summary> 
        public void SizeArrays(int numWD)
        {     
            elevCoeff = new double[numWD];
            downhill_A = new double[numWD];
            downhill_B = new double[numWD];
            uphill_A = new double[numWD];
            uphill_B = new double[numWD];                        
            UW_crit = new double[numWD];
            spdUp_A = new double[numWD];
            spdUp_B = new double[numWD];

            DH_Stab_A = new double[numWD];
            UH_Stab_A = new double[numWD];
            SU_Stab_A = new double[numWD];
            stabB = new double[numWD];

            sep_A_DW = new double[numWD];
            sep_B_DW = new double[numWD];
            turbWS_Fact = new double[numWD];            
            sepCrit = new double[numWD];
            Sep_crit_WS = new double[numWD];                               

        }

        /// <summary> Calculates and returns RMS of sectorwise wind speed estimate error either weighted by wind rose or not </summary> 
        public double CalcRMS_SectorsEstError(bool isWeighted, double[] windRose)
        {            
            double RMS_Err = 0;
            double RMS_Err_count = 0;
            int numWD = windRose.Length;

            for (int WD = 0; WD < numWD; WD++)
            {
                if (isWeighted == true)
                {
                    RMS_Err = RMS_Err + RMS_Sect_WS_Est[WD] * windRose[WD];
                    RMS_Err_count = RMS_Err_count + windRose[WD];
                }
                else {
                    RMS_Err = RMS_Err + RMS_Sect_WS_Est[WD];
                    RMS_Err_count = RMS_Err_count + 1;
                }
            }

            RMS_Err = RMS_Err / RMS_Err_count;

            return RMS_Err;

        }         

        /// <summary> Returns true if specified met site was used to generate model. </summary>        
        public bool IsMetUsedInModel(string metName)
        {
            bool isUsed = false;

            for (int i = 0; i < metsUsed.Length; i++)
                if (metsUsed[i] == metName)
                    isUsed = true;
            
            return isUsed;
        }
       
    }
}
