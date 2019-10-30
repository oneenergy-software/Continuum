using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class Model
    {
        public Met.TOD timeOfDay; // enum: All / Day / Night
        public Met.Season season; // enum: All / Winter / Spring / Summer / Fall
        public double height;

        public double[] downhill_A;  // 'A' coefficient of Downhill power law of each WD sector model
        public double[] downhill_B;  // 'B' coefficient of Downhill power law of each WD sector model

        public double[] uphill_A;   // 'A' coefficient of Uphill power law of each WD sector model
        public double[] uphill_B;   // 'B' coefficient of Uphill power law of each WD sector model

        public double[] UW_crit;  //for (int positive UW expo only, UW exposure cut-off: if UW expo > cut-off then muw is negative, if UW expo < cut-off then muw is positive (due to speed-up over a small hill!)  
        public double[] spdUp_A;   // 'A' coefficient of UW power law when UW < UWcrit
        public double[] spdUp_B;   // 'B' coefficient of UW power law when UW < UWcrit

        public double[] DH_Stab_A;   // 'A' coefficient of Stability correction (for Downhill flow) power law of each WD sector model
        public double[] UH_Stab_A;   // 'A' coefficient of Stability correction (for Uphill flow) power law of each WD sector model
        public double[] SU_Stab_A;   // 'A' coefficient of Stability correction (for Induced Speed Up flow) power law of each WD sector model
        public double[] stabB;   // 'B' coefficient of Stability correction power law of each WD sector model

        public double[] sep_A_DW;   // //A' coefficient of Flow separation model when flow sep occurs downwind Sum UW+DW > sepCrit & UW>0 & DW>0 
        public double[] sep_B_DW;   // //B' coefficient of Flow separation model when flow sep occurs downwind Sum UW+DW > sepCrit & UW>0 & DW>0
        public double[] turbWS_Fact;   // Turbulent WS factor for each WD sector where WS is modeled as a linear decrease in wind speed as a function of distance * turbWS_Fact
        public double[] sepCrit;   // When UW>0 & DW>0 & UW+DW > sepCrit, flow separation occurs, mdw is negative and is found using Sep_A & Sep_B
        public double[] Sep_crit_WS;   // critical WS for flow separation to occur 

        //  public Block_A()  double // //A' coefficient of Hill Blockage power law(which describes positive muw when UW<0 or negative mdw when DW>0 (i.e.on back side of hill) when UW crosswind slope is greater than Max_CW_Slope)
        //  public Block_B()  double
        //  public Diverge_A()  double // //A' coefficient of Hill Divergence power law(which describes negative muw when UW>0 or positive mdw when DW<0 (i.e.on front side of hill) when UW crosswind slope is greater than Max_CW_Slope)
        //  public Diverge_B()  double
        //  public Max_CW_Slope()  double // TO DO: FLOW AROUND HILLS / FLOW OVER HILLS ALGORITHM
        // public Max_PL_Slope()  double

        public int radius;             
     
        public int maxElevDiff;
        public double maxP10DiffSlope;
        public double maxP10DiffOffset;
      
        public bool isImported = false;

        public string[] metsUsed;
        public double RMS_WS_Est;   // RMS error of met cross-predictions 
        public double[] RMS_Sect_WS_Est;   // RMS error of sectorwise met cross-preds 

        public double minSumUWDW;   // minimum Sum UWDW for flow separation model to kick in                       

        /// <summary>
        /// Calculates and returns DW coefficient based on flow type and terrain complexity (i.e. P10_Expo)
        /// </summary>        
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

        public double CalcOverallDW_Coeff(double[] P10_DW_Expo, double[] P10_UW_Expo, double[] avgDW, double[] avgUW, double[] windRose,
                                          double[] windSpeed, bool useFlowSep, NodeCollection.Sep_Nodes[] flowSepNodes)
        {
            // Calculates and returns overall DW coefficient (for Advacned tab plot/table)            
            double overallDW_Coeff = 0;
            
            for (int WD_sec = 0; WD_sec < windRose.Length; WD_sec++)
            {
                string flowType = GetFlowType(avgUW[WD_sec], avgDW[WD_sec], WD_sec, "DW", flowSepNodes, windSpeed[WD_sec], useFlowSep, 0);
                double DW_Coeff = CalcDW_Coeff(P10_DW_Expo[WD_sec], P10_UW_Expo[WD_sec], WD_sec, flowType);
                overallDW_Coeff = overallDW_Coeff + DW_Coeff * windRose[WD_sec];
            }

            return overallDW_Coeff;
        }

        /// <summary>
        /// Calculates and returns UW coefficient based on flowType and P10_Expo
        /// </summary>        
        public double CalcUW_Coeff(double P10_UW_Expo, double P10_DW_Expo, int WD_sec, string flowType)
        {             
            // if ( UW Expo > 0 ) then use uphill coeff
            // if ( UW Expo < 0 And DW Expo > 0 ) then use downhill coeff
            // if ( UW Expo < 0 And DW Expo < 0 ) then use UPHILL COEFFS (was overpredicting in areas with negative UW and negative DW exposure)

            double UW_Coeff = 0;
            double P10_Expo;

            P10_UW_Expo = Math.Abs(P10_UW_Expo);
            P10_DW_Expo = Math.Abs(P10_DW_Expo);

            if (P10_DW_Expo > P10_UW_Expo)
                P10_Expo = P10_DW_Expo;
            else
                P10_Expo = P10_UW_Expo;

            // flowType = "Downhill"
            // flowType = "Uphill"
            //  flowType = "SpdUp"
            //  flowType = "Valley"
            //  flowType = "Turbulent"

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

        public double CalcOverallUW_Coeff(double[] windRose, double[] P10_UW, double[] P10_DW, double[] UW_CW_Grade, double[] UW_PL_Grade,
                                                          double[] UWExpo, double[] DWExpo, NodeCollection.Sep_Nodes[] flowSepNodes, double[] WS, bool useFlowSep)
        {
            // Calculates and returns overall UW coefficient (for Advanced tab plot/table)
            double UW_Coeff = 0;            
            double overallUW_Coeff = 0;
            string flowType;
            NodeCollection.Sep_Nodes[] Sep_nodes;
            NodeCollection nodeList = new NodeCollection();
            
            for (int WD_sec = 0; WD_sec < windRose.Length; WD_sec++)
            {
                Sep_nodes = nodeList.GetSepNodes1and2(flowSepNodes, flowSepNodes, WD_sec);
                flowType = GetFlowType(UWExpo[WD_sec], DWExpo[WD_sec], WD_sec, "UW", flowSepNodes, WS[WD_sec], useFlowSep, 0);
                UW_Coeff = CalcUW_Coeff(P10_UW[WD_sec], P10_DW[WD_sec], WD_sec, flowType);

                overallUW_Coeff = overallUW_Coeff + UW_Coeff * windRose[WD_sec];
            }

            return overallUW_Coeff;

        }               

        public double CalcOverallUWCrit(double[] windRose)
        {
            //  Calculates and returns overall UW critical
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

        public double CalcOverallSepCrit(double[] windRose)
        {
            //  Calculates and returns overall critical flow separation
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

        public double CalcOverallSepCritWS(double[] windRose)
        {
            //  Calculates and returns overall flow separation critical wind speed
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

        public double GetStabilityCorrection(double UW_Expo, double DW_Expo, int WD_Ind, double SR, bool useFlowSep, string UW_or_DW)
        {
            // Calculates and returns stability correction factor used in surface roughness model
            
            double Stab_corr = 0;
            NodeCollection.Sep_Nodes[] Flow_Sep_Node = null;

            string flowType = GetFlowType(UW_Expo, DW_Expo, WD_Ind, UW_or_DW, Flow_Sep_Node, 0, useFlowSep, 0);

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

        /// <summary>
        /// Determines and returns flow type upwind or downwind of site: Downhill, Uphill, Induced Speed-up, Valley or Turbulent
        /// </summary>        
        public string GetFlowType(double thisUW, double thisDW, int WD_sec, string UW_or_DW, NodeCollection.Sep_Nodes[] flowSepNodes, double thisWS, bool useFlowSep, 
            int radiusInd)
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
                else if (thisUW < 0 && thisDW < 0)
                    // flowType = "Valley"
                    flowType = "Downhill";
            }
            else
                flowType = "Turbulent";

            return flowType;
        }

        public double CalcTurbulentZoneLength(double sumUWDW)
        {
            // Calculates and returns length of upwind turbulent zone
            double Turb_zone_length = 8 * sumUWDW;

            if (Turb_zone_length > 2000)
                Turb_zone_length = 2000;

            return Turb_zone_length;
        }

        public double GetDeltaWS_TurbulentZone(double turbLength, int WD_Ind)
        {
            // Calculates and returns change in wind speed from either upwind high node to site or from upwind high node to upwind end of turbulent zone
            double Delta_WS = -turbWS_Fact[WD_Ind] * turbLength / 1500;

            return Delta_WS;
        }


        public void SetDefaultModelCoeffs(int numWD)
        {
            // Sets the model coefficients to default values
            for (int i = 0; i < numWD; i++)
            {
                downhill_A[i] = 0.1432f; // old DW_A = 0.2551
                downhill_B[i] = -0.6537f; // old DW_B = -0.774

                uphill_A[i] = 0.0819f; // old UW_A = 0.1551
                uphill_B[i] = -0.6242f; // old UW_B = -0.774

                spdUp_A[i] = 0.0165f; // old SU A = 0.03
                spdUp_B[i] = -0.5966f; // old SU B = -0.774
                UW_crit[i] = 21f;

                //   Block_A[i] = 0.001
                //'   Block_B[i] = 0.1
                //  Diverge_A[i] = 0.001
                //  Diverge_B[i] = -0.1
                //  Max_CW_Slope[i] = 0.1
                //  Max_PL_Slope[i] = 0.1

                DH_Stab_A[i] = 2f;
                UH_Stab_A[i] = 2f;
                SU_Stab_A[i] = 2;
                stabB[i] = 0f;

                sep_A_DW[i] = 0.0819f;
                sep_B_DW[i] = -0.6242f;
                turbWS_Fact[i] = 2f;
                sepCrit[i] = 250f;
                Sep_crit_WS[i] = 4f;

          //      downhillBounds[i].min = -999f;
          //      downhillBounds[i].max = -999f;                
         //       uphillBounds[i].min = -999f;
          //      uphillBounds[i].max = -999f;
          //      spdUpBounds[i].min = -999f;
          //      spdUpBounds[i].max = -999f;
                
            }

        }

        public void SetDefaultLimits()
        {
            // Sets default maximum elevation and max exposure difference and minimum flow separation point
            maxElevDiff = 50;
            maxP10DiffSlope = 0.4f;
            maxP10DiffOffset = 10f;
            minSumUWDW = 150f;
        }

        public void SizeArrays(int numWD)
        {
            // Sizes model coefficient arrays
            downhill_A = new double[numWD];
            downhill_B = new double[numWD];

            uphill_A = new double[numWD];
            uphill_B = new double[numWD];

            //  Block_A[numWD];
            //  Block_B[numWD];
            //  Diverge_A[numWD];
            //  Diverge_B[numWD];
            //  Max_CW_Slope[numWD];
            //  Max_PL_Slope[numWD];
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
            //Sep_A_UW[numWD];
            //Sep_B_UW[numWD];
            sepCrit = new double[numWD];
            Sep_crit_WS = new double[numWD];                               

        }

        public double CalcRMS_SectorsEstError(bool isWeighted, double[] windRose)
        {
            // Calculates and returns RMS of sectorwise wind speed estimate error either weighted by wind rose or not
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
