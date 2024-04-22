using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Windows.Forms;
using static ContinuumNS.Met_Data_Filter;
using static ContinuumNS.TurbineCollection;

namespace ContinuumNS
{
    /// <summary> Class that holds all information and modeled data for a single turbine. Stored data includes the calculated exposures, SRDH, and P10 Exposure (complexity). Modeled
    /// estimates include free-stream and waked wind speeds and gross/net energy production estimates.</summary>
    [Serializable()]
    public class Turbine
    {
        /// <summary> Name of turbine </summary>
        public string name;
        /// <summary> UTM X coordinate </summary>
        public double UTMX;
        /// <summary> UTM Y coordinate </summary>
        public double UTMY;
        /// <summary> Turbine site elevation </summary>
        public double elev;
        /// <summary> Turbine string number (either imported or found in turbineList.AssignStringNumber) </summary>
        public int stringNum;
        /// <summary> List of exposure and SRDH at turbine site </summary>
        public Exposure[] expo;
        /// <summary> List of terrain complexity at turbine site </summary>
        public Grid_Info gridStats = new Grid_Info();
        /// <summary> Wind speed estimates: One for each predictor met and each Continuum model (4 radii) </summary>
        public WS_Ests[] WS_Estimate;
        /// <summary> Combination of WS_Estimate() to form overall average and sectorwise wind speed estimates </summary>
        public Avg_Est[] avgWS_Est;
        /// <summary> Gross Energy Estimates: One for each power curve entered. </summary>
        public Gross_Energy_Est[] grossAEP;
        /// <summary> Net Energy Estimates: One for each wake loss model. DOES NOT INCLUDE OTHER LOSSES. Other losses are applied to Waked Turb List and Net Exports. </summary>
        public Net_Energy_Est[] netAEP;
        /// <summary> If flow separation model enabled, nodes where flow separation will occur surrounding turbine. </summary>                                 
        public NodeCollection.Sep_Nodes[] flowSepNodes;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Holds waked/unwaked wind speed and energy production estimates at turbine site based on combined WS_Ests. Also contains calculated shear and time series estimates if time series met data imported. </summary>
        [Serializable()]
        public struct Avg_Est
        {
            /// <summary> Free-stream wind speed estimate </summary> 
            public EstWS_Data freeStream;
            /// <summary> Waked wind speed estimate </summary>
            public EstWS_Data waked;
            /// <summary> Wind speed estimate uncertainty </summary>
            public double uncert;
            /// <summary> True if estimate generated from imported wind flow model coefficients. </summary>
            public bool isImported;
            /// <summary> True if gross energy TS has been calculated. </summary>
            public bool haveGrossTS;
            /// <summary> True if net energy TS has been calculated. </summary>
            public bool haveNetTS;
            /// <summary> Power curve used to calculate energy production </summary>
            public TurbineCollection.PowerCurve powerCurve;
            /// <summary> Wake loss model used to calculate wake losses </summary>
            public Wake_Model wakeModel;
            /// <summary> Free-stream average WS and WS distribution by month </summary>
            public MonthlyWS_Vals[] FS_MonthlyVals;
            /// <summary> Waked average WS and waked WS distribution by month </summary>
            public MonthlyWS_Vals[] wakedMonthlyVals;
            /// <summary> Directional average power law shear exponent, alpha </summary>
            public double[] alpha;
            /// <summary> Ambient, representative, and effective turbulence intensity (calculated from met time series data) </summary>
            public TurbIntensity TI;
            /// <summary> Array of time series data (Timestamp, WS, Gross, Net). Clears on save. Is stored in DB. </summary>
            public ModelCollection.TimeSeries[] timeSeries;
        }

        /// <summary> Holds average, representative, and effective sectorwise turbulence intensity. </summary> 
        [Serializable()]
        public struct TurbIntensity
        {
            /// <summary> Sectorwise average turbulence intensity. </summary> 
            public double[] TI;
            /// <summary> Sectorwise representative turbulence intensity. </summary>
            public double[] repTI;
            /// <summary> Sectorwise effective turbulence intensity. </summary>
            public double[] effectiveTI;
        }

        /// <summary> Holds overall and sectorwise wind speed estimate at turbine site based on specified wind flow model and predictor met </summary>
        [Serializable()]
        public struct WS_Ests
        {
            /// <summary> Name of Predictor met site </summary>
            public string predictorMetName;
            /// <summary> Path of nodes between predictor met and turbine (if any) </summary>
            public Nodes[] pathOfNodes;
            /// <summary> Overall wind speed estimated at nodes between predictor met and turbine </summary>
            public double[] WS_at_nodes;
            /// <summary> Sectorwise wind speed estimated at nodes between predictor met and turbine </summary>
            public double[,] sectorWS_at_nodes;   // i = Node num j = WD sector
            /// <summary> Sectorwise WS estimates at turbine </summary>
            public double[] sectorWS;
            /// <summary> Wind flow model used to generate estimate. </summary>
            public Model model;
            /// <summary> Overall wind speed estimate at turbine. </summary>
            public double WS;
            /// <summary> Wind speed estimate weight (calculated from wind flow model cross-prediction error and similarity in terrain complexity between predictor met and turbine). </summary>
            public double WS_weight;
        }

        /// <summary> Contains total and sectorwise gross energy estimate values, capacity factors, and monthly values for given power curve. </summary>
        [Serializable()]
        public struct Gross_Energy_Est
        {
            /// <summary> Power curve used to generate energy estimate. </summary>
            public TurbineCollection.PowerCurve powerCurve;
            /// <summary> Gross annual energy estimate. </summary>
            public double AEP;
            /// <summary> Sectorwise gross annual energy estimate (sectorws dist * power curve * 8760 * wind_rose[i]). </summary>
            public double[] sectorEnergy;
            /// <summary> P90 gross AEP. </summary>
            public double P90;
            /// <summary> P90 gross AEP. </summary>
            public double P99;
            /// <summary> Gross annual capacity factor. </summary>
            public double CF;
            /// <summary> Monthly gross energy estimates. </summary>
            public MonthlyEnergyVals[] monthlyVals;
        }

        /// <summary> Contains total and sectorwise net energy estimate values, capacity factors, and monthly values for given power curve and wake loss model. </summary>
        [Serializable()]
        public struct Net_Energy_Est
        {
            /// <summary> Wake loss model used to calculate wake losses. </summary>
            public Wake_Model wakeModel;
            /// <summary> Net annual energy estimate (includes wake loss; does not include all other losses). </summary>
            public double AEP;
            /// <summary> Sectorwise Net annual energy estimate (includes wake loss; does not include all other losses). </summary>
            public double[] sectorEnergy;
            /// <summary> P90 net AEP. </summary>
            public double P90;
            /// <summary> P99 net AEP. </summary>
            public double P99;
            /// <summary> Net capacity factor. </summary>
            public double CF;
            /// <summary> Overall wake loss. </summary>
            public double wakeLoss;
            /// <summary> Sectorwise wake loss. </summary>
            public double[] sectorWakeLoss;
            /// <summary> Monthly net energy estimates. </summary>
            public MonthlyEnergyVals[] monthlyVals;
        }

        /// <summary> Contains month, year, average wind speed and WS distribution. </summary>
        [Serializable()]
        public struct MonthlyWS_Vals
        {
            /// <summary> Month. </summary>
            public int month;
            /// <summary> Year. </summary>
            public int year;
            /// <summary> Average wind speed. </summary>
            public double avgWS;
            /// <summary> Wind speed / Wind direction distribution. </summary>
            public Met.WSWD_Dist WS_Dist;
        }

        /// <summary> Contains month, year, and monthly energy production. </summary>
        [Serializable()]
        public struct MonthlyEnergyVals
        {
            /// <summary> Month. </summary>
            public int month;
            /// <summary> Year. </summary>
            public int year;
            /// <summary> Energy Production over month, MWh. </summary>
            public double energyProd;
        }

        /// <summary> Contains estimated overall and sectorwise wind speeds and WS distributions. </summary>
        [Serializable()]
        public struct EstWS_Data
        {
            /// <summary> Overall wind speed estimate. </summary>
            public double WS;
            /// <summary> Overall wind speed distribution. </summary>
            public double[] WS_Dist;
            /// <summary> Estimated Weibull parameters. </summary>
            public MetCollection.Weibull_params weibullParams;
            /// <summary> Sectorwise WS estimates. </summary>
            public double[] sectorWS;
            /// <summary> Sectorwise WS distributions i = Sector num, j = WS interval . </summary>
            public double[,] sectorWS_Dist;
            /// <summary> Estimated wind rose </summary>
            public double[] windRose;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Returns number of calculated exposure. </summary>
        public int ExposureCount
        {
            get
            {
                if (expo == null)
                    return 0;
                else
                    return expo.Length;
            }
        }

        /// <summary> Returns number of wind speed estimates with a given predicting met and model. </summary>
        public int WSEst_Count
        {

            get
            {

                if (WS_Estimate == null)
                    return 0;
                else
                    return WS_Estimate.Length;
            }
        }

        /// <summary> Returns number of average wind speed estimates (weighted average of wind speed estimates). </summary>
        public int AvgWSEst_Count
        {

            get
            {
                if (avgWS_Est == null)
                    return 0;
                else
                    return avgWS_Est.Length;
            }
        }

        /// <summary> Returns number of gross energy production estimates. </summary>
        public int GrossAEP_Count
        {
            get
            {
                if (grossAEP == null)
                    return 0;
                else
                    return grossAEP.Length;
            }
        }

        /// <summary> Returns number of net energy production estimates. </summary>
        public int NetAEP_Count
        {
            get
            {
                if (netAEP == null)
                    return 0;
                else
                    return netAEP.Length;
            }
        }

        /// <summary> Returns false if exposure has already been calculated with specified radius and exponent. </summary>
        public bool IsNewExposure(int radius, double exponent, int numSectors)
        {
            int thisCount = ExposureCount;
            bool isNew = false;

            if (thisCount == 0) isNew = true;

            for (int i = 0; i <= thisCount - 1; i++)
            {
                if (expo[i].exponent == exponent && expo[i].radius == radius && expo[i].numSectors == numSectors)
                { // the exposures based on radius and exp combo already calculated
                    //MsgBox("caught it" & exponent & " " & radius)
                    isNew = false;
                    break;
                }
                else
                    isNew = true;

            }

            return isNew;
        }

        /// <summary> Returns false if surface roughness and displacement height have already been calculated. </summary>
        public bool IsNewSRDH(int radius, double exponent, int numSectors)
        {
            bool isNew = true;

            for (int i = 0; i < ExposureCount; i++)
            {
                if (expo[i].exponent == exponent && expo[i].radius == radius && expo[i].numSectors == numSectors && expo[i].SR != null && expo[i].dispH != null)
                { // the exposures based on radius and exp combo and number of sectors to avg already calculated
                    //MsgBox("caught it" & exponent & " " & radius)
                    isNew = false;
                    break;
                }
            }

            return isNew;
        }

        /// <summary> Returns true if estimates have already been formed for specified wake model. </summary>
        public bool EstsExistForWakeModel(Wake_Model thisWakeModel, WakeCollection WakeList)
        {
            bool estsExist = false;

            for (int i = 0; i <= AvgWSEst_Count - 1; i++)
            {
                if (avgWS_Est[i].wakeModel != null)
                {
                    if (WakeList.IsSameWakeModel(avgWS_Est[i].wakeModel, thisWakeModel))
                    {
                        estsExist = true;
                        break;
                    }
                }
            }

            for (int i = 0; i < NetAEP_Count; i++)
            {
                if (WakeList.IsSameWakeModel(netAEP[i].wakeModel, thisWakeModel))
                {
                    estsExist = true;
                    break;
                }
            }

            return estsExist;
        }

        /// <summary> Adds exposure to list with specified radius of investigation and weighting exponent. </summary>
        public void AddExposure(int radius, double exponent, int numSectors, int numWD)
        {
            int insertIndex = 0;
            int thisExpoCount = ExposureCount;

            if (thisExpoCount > 0)
            {
                if (radius > expo[thisExpoCount - 1].radius)  // Larger radius than largest in list
                    insertIndex = thisExpoCount;
                else if (radius < expo[0].radius)  // Smaller than smallest in list
                    insertIndex = 0;
                else
                {
                    for (int i = 0; i <= thisExpoCount - 2; i++)
                    {
                        if (expo[i].radius < radius && expo[i + 1].radius >= radius)
                        {
                            insertIndex = i + 1;
                            break;
                        }
                    }
                }

                Exposure[] existingExpos = new Exposure[thisExpoCount];

                for (int j = 0; j <= thisExpoCount - 1; j++)
                    existingExpos[j] = expo[j];

                expo = new Exposure[thisExpoCount + 1];

                for (int j = 0; j <= insertIndex - 1; j++)
                    expo[j] = existingExpos[j];

                expo[insertIndex] = new Exposure();
                expo[insertIndex].radius = radius;
                expo[insertIndex].exponent = exponent;
                expo[insertIndex].numSectors = numSectors;
                expo[insertIndex].expo = new double[numWD];

                for (int j = insertIndex + 1; j <= thisExpoCount; j++)
                    expo[j] = existingExpos[j - 1];
            }
            else
            {
                expo = new Exposure[1];
                expo[0] = new Exposure();
                expo[0].radius = radius;
                expo[0].exponent = exponent;
                expo[0].numSectors = numSectors;
                expo[0].expo = new double[numWD];
            }

        }

        /// <summary> Add specified wind speed estimate to list. </summary>
        public void AddWS_Estimate(WS_Ests newWS_Est)
        {
            int newCount = WSEst_Count;
            int numWD = newWS_Est.model.downhill_A.Length;

            if (WSEst_Count > 0)
            {
                Array.Resize(ref WS_Estimate, newCount + 1);
                WS_Estimate[newCount] = newWS_Est;
                WS_Estimate[newCount].sectorWS = new double[numWD];
            }
            else
            {
                WS_Estimate = new WS_Ests[1];
                WS_Estimate[0] = newWS_Est;
            }

        }

        /// <summary> Add specified average wind speed estimate to list. </summary>
        public void AddAvgWS_Estimate(Avg_Est newAvgEst)
        {
            int newCount = AvgWSEst_Count;

            if (AvgWSEst_Count > 0)
            {
                Array.Resize(ref avgWS_Est, newCount + 1);
                avgWS_Est[newCount] = newAvgEst;
                avgWS_Est[newCount].freeStream = avgWS_Est[0].freeStream;
            }
            else
            {
                avgWS_Est = new Avg_Est[1];
                avgWS_Est[0] = newAvgEst;
            }
        }

        /// <summary> Add specified gross energy time series estimate to list. </summary>
        public void AddGrossEstTimeSeries(Gross_Energy_Est thisGross)
        {
            int newCount = GrossAEP_Count;
            Array.Resize(ref grossAEP, newCount + 1);
            grossAEP[newCount] = thisGross;
        }

        /// <summary> Add specified net energy time series estimate to list. </summary>
        public void AddNetEstTimeSeries(Net_Energy_Est thisNet)
        {
            int newCount = NetAEP_Count;
            Array.Resize(ref netAEP, newCount + 1);
            netAEP[newCount] = thisNet;
        }

        /// <summary> Adds gross energy estimate to list. </summary>
        public void AddGrossAEP(TurbineCollection.PowerCurve thisPowerCurve, double P50_AEP, double P50_CF, double P90_AEP,
                                 double P99_AEP, double[] P50_Sect_AEP)
        {
            // See if there is an empty Gross AEP
            bool haveEmpty = false;
            int emptyIndex = 0;

            for (int i = 0; i <= GrossAEP_Count - 1; i++)
            {
                if (grossAEP[i].AEP == 0)
                {
                    haveEmpty = true;
                    emptyIndex = i;
                    break;
                }
            }

            if (haveEmpty == true)
            {
                grossAEP[emptyIndex].powerCurve = thisPowerCurve;
                grossAEP[emptyIndex].sectorEnergy = P50_Sect_AEP;
                grossAEP[emptyIndex].AEP = P50_AEP;
                grossAEP[emptyIndex].CF = P50_CF;
                grossAEP[emptyIndex].P99 = P99_AEP;
                grossAEP[emptyIndex].P90 = P90_AEP;
            }
            else
            {
                int newCount = GrossAEP_Count;
                Array.Resize(ref grossAEP, newCount + 1);
                grossAEP[newCount].powerCurve = thisPowerCurve;
                grossAEP[newCount].sectorEnergy = P50_Sect_AEP;
                grossAEP[newCount].AEP = P50_AEP;
                grossAEP[newCount].CF = P50_CF;
                grossAEP[newCount].P99 = P99_AEP;
                grossAEP[newCount].P90 = P90_AEP;
            }

        }

        /// <summary> Adds net energy estimate to list. </summary>
        public void AddNetAEP(Wake_Model thisWakeModel, double P50_AEP, double P50_CF, double P90_AEP, double P99_AEP, double wakeLoss, double[] P50_Sect_AEP)
        {
            // See if there is an empty Net AEP
            bool haveEmpty = false;
            int Empty_Ind = 0;

            for (int i = 0; i <= NetAEP_Count - 1; i++)
            {
                if (netAEP[i].AEP == 0)
                {
                    haveEmpty = true;
                    Empty_Ind = i;
                    break;
                }
            }

            if (haveEmpty == true)
            {
                netAEP[Empty_Ind].wakeModel = thisWakeModel;
                netAEP[Empty_Ind].AEP = P50_AEP;
                netAEP[Empty_Ind].CF = P50_CF;
                netAEP[Empty_Ind].P99 = P99_AEP;
                netAEP[Empty_Ind].P90 = P90_AEP;
                netAEP[Empty_Ind].sectorEnergy = P50_Sect_AEP;
                netAEP[Empty_Ind].wakeLoss = wakeLoss;
            }
            else
            {
                int newCount = NetAEP_Count;
                Array.Resize(ref netAEP, newCount + 1);
                netAEP[newCount].wakeModel = thisWakeModel;
                netAEP[newCount].AEP = P50_AEP;
                netAEP[newCount].CF = P50_CF;
                netAEP[newCount].P99 = P99_AEP;
                netAEP[newCount].P90 = P90_AEP;
                netAEP[newCount].sectorEnergy = P50_Sect_AEP;
                netAEP[newCount].wakeLoss = wakeLoss;
            }

        }

        /// <summary> Returns wind speed estimate at specified radius, using specified met and model. </summary>
        public WS_Ests GetWS_Est(int radius, string predMet, Model This_Model)
        {
            WS_Ests thisWS_Est = new WS_Ests();
            ModelCollection Model_List = new ModelCollection();

            for (int i = 0; i < WSEst_Count; i++)
            {
                if (WS_Estimate[i].predictorMetName == predMet && WS_Estimate[i].model.radius == radius && Model_List.IsSameModel(WS_Estimate[i].model, This_Model))
                {
                    thisWS_Est = WS_Estimate[i];
                    break;
                }
            }

            return thisWS_Est;
        }

        /// <summary> Returns average wind speed estimate found using specified wake model or unwaked (i.e. no wake model specified). </summary>
        public Avg_Est GetAvgWS_Est(Wake_Model thisWakeModel)
        {
            Avg_Est thisAvgWS_Est = new Avg_Est();
            WakeCollection wakeList = new WakeCollection();
            bool blankWake = false;

            if (thisWakeModel == null)
                blankWake = true;
            else if (thisWakeModel.powerCurve.name == null)
                blankWake = true;

            for (int i = 0; i < AvgWSEst_Count; i++)
            {
                if (blankWake == true || wakeList.IsSameWakeModel(thisWakeModel, avgWS_Est[i].wakeModel))
                {
                    thisAvgWS_Est = avgWS_Est[i];
                    break;
                }
            }

            return thisAvgWS_Est;
        }

        /// <summary> Returns gross energy estimate using specified power curve. </summary>
        public Gross_Energy_Est GetGrossEnergyEst(TurbineCollection.PowerCurve powerCurve)
        {
            Gross_Energy_Est thisGrossEst = new Gross_Energy_Est();

            for (int i = 0; i < GrossAEP_Count; i++)
            {
                if (grossAEP[i].powerCurve.name == powerCurve.name)
                {
                    thisGrossEst = grossAEP[i];
                    break;
                }
            }

            return thisGrossEst;
        }

        /// <summary> Returns net energy estimate using specified wake model. </summary>
        public Net_Energy_Est GetNetEnergyEst(Wake_Model thisWakeModel)
        {
            Net_Energy_Est thisNetEst = new Net_Energy_Est();
            WakeCollection wakeList = new WakeCollection();

            for (int i = 0; i < NetAEP_Count; i++)
                if (wakeList.IsSameWakeModel(thisWakeModel, netAEP[i].wakeModel))
                {
                    thisNetEst = netAEP[i];
                    break;
                }

            return thisNetEst;
        }

        /// <summary> Returns average WS, Weibull A, or Weibull K estimate of wake model (if specified) and for specified WD. Specify "WS", "WeibA" or "Weibk" </summary>
        public double GetAvgOrSectorWS_Est(Wake_Model thisWakeModel, int WD_Ind, string WS_WeibA_WeibK, TurbineCollection.PowerCurve powerCurve)
        {
            double thisValue = 0;
            WakeCollection wakeList = new WakeCollection();

            if (AvgWSEst_Count == 0)
                return thisValue;

            int numWD = avgWS_Est[0].freeStream.sectorWS.Length;
            bool isFreestream = false;

            if (thisWakeModel == null)
                isFreestream = true;
            else if (thisWakeModel.powerCurve.name == null)
                isFreestream = true;

            EstWS_Data thisEst = new EstWS_Data();

            for (int i = 0; i < AvgWSEst_Count; i++)
            {
                if ((thisWakeModel == null || wakeList.IsSameWakeModel(thisWakeModel, avgWS_Est[i].wakeModel)) && (powerCurve.name == null || avgWS_Est[i].powerCurve.name == powerCurve.name))
                {
                    if (isFreestream)
                        thisEst = avgWS_Est[i].freeStream;
                    else
                        thisEst = avgWS_Est[i].waked;

                    if (WD_Ind == numWD)
                    {
                        if (WS_WeibA_WeibK == "WS")
                            thisValue = thisEst.WS;
                        else if (WS_WeibA_WeibK == "WeibA")
                            thisValue = thisEst.weibullParams.overall_A;
                        else if (WS_WeibA_WeibK == "WeibK")
                            thisValue = thisEst.weibullParams.overall_k;
                        else
                        {
                            MessageBox.Show("Invalid WS_WeibA_WeibK flag: " + WS_WeibA_WeibK);
                            return thisValue;
                        }
                    }
                    else
                    {
                        if (WS_WeibA_WeibK == "WS")
                            thisValue = thisEst.sectorWS[WD_Ind];
                        else if (WS_WeibA_WeibK == "WeibA")
                            thisValue = thisEst.weibullParams.sector_A[WD_Ind];
                        else if (WS_WeibA_WeibK == "WeibK")
                            thisValue = thisEst.weibullParams.sector_k[WD_Ind];
                        else
                        {
                            MessageBox.Show("Invalid WS_WeibA_WeibK flag: " + WS_WeibA_WeibK);
                            return thisValue;
                        }
                    }

                    break;
                }
            }

            return thisValue;
        }

        /// <summary> Returns gross AEP estimate for specified power curve and WD sector. </summary>
        public double GetGrossAEP(string powerCurve, int WD_Ind)
        {
            double thisAEP = 0;
            int numWD = 0;

            for (int i = 0; i < GrossAEP_Count; i++)
            {
                try
                {
                    numWD = grossAEP[i].sectorEnergy.Length;
                }
                catch
                {
                    return thisAEP;
                }

                if (WD_Ind == numWD)
                { // overall AEP
                    if (grossAEP[i].powerCurve.name == powerCurve)
                    {
                        thisAEP = grossAEP[i].AEP;
                        break;
                    }
                }
                else
                { // sectorwise
                    if (grossAEP[i].powerCurve.name == powerCurve)
                    {
                        thisAEP = grossAEP[i].sectorEnergy[WD_Ind];
                        break;
                    }
                }
            }

            return thisAEP;
        }

        /// <summary> Returns net AEP estimate for specified wake loss model and WD sector. </summary>
        public double GetNetAEP(Wake_Model thisWakeModel, int WD_Ind)
        {
            double thisAEP = 0;
            WakeCollection wakeList = new WakeCollection();
            int numWD = 0;

            for (int i = 0; i < NetAEP_Count; i++)
            {
                try
                {
                    numWD = netAEP[i].sectorEnergy.Length;
                }
                catch
                {
                    return thisAEP;
                }

                if (wakeList.IsSameWakeModel(thisWakeModel, netAEP[i].wakeModel))
                {
                    if (WD_Ind == numWD) // overall AEP
                        thisAEP = netAEP[i].AEP;
                    else
                        thisAEP = netAEP[i].sectorEnergy[WD_Ind];

                    break;
                }
            }

            return thisAEP;
        }

        /// <summary> Returns net capacity factor estimate for specified wake loss model and WD sector. </summary>
        public double GetNetCF(Wake_Model thisWakeModel, int WD_Ind)
        {
            double This_CF = 0;
            int numWD = 0;
            WakeCollection wakeList = new WakeCollection();

            for (int i = 0; i < NetAEP_Count; i++)
            {
                try
                {
                    numWD = netAEP[i].sectorEnergy.Length;
                }
                catch
                {
                    return This_CF;
                }

                if (wakeList.IsSameWakeModel(netAEP[i].wakeModel, thisWakeModel))
                {
                    if (WD_Ind == numWD)
                    { // overall CF
                        This_CF = netAEP[i].CF;
                    }
                    else
                    {
                        TurbineCollection turbineList = new TurbineCollection();
                        This_CF = turbineList.CalcCapacityFactor(netAEP[i].sectorEnergy[WD_Ind], thisWakeModel.powerCurve.ratedPower);
                    }
                    break;
                }
            }

            return This_CF;
        }

        /// <summary> Returns wake loss estimate for specified wake loss model and WD sector. </summary>
        public double GetWakeLoss(Wake_Model thisWakeModel, int WD_Ind)
        {
            double thisWakeLoss = 0;
            WakeCollection wakeList = new WakeCollection();
            int numWD;

            for (int i = 0; i < NetAEP_Count; i++)
            {
                try
                {
                    numWD = netAEP[i].sectorEnergy.Length;
                }
                catch
                {
                    return thisWakeLoss;
                }

                if (wakeList.IsSameWakeModel(netAEP[i].wakeModel, thisWakeModel))
                {
                    if (WD_Ind == numWD)
                        thisWakeLoss = netAEP[i].wakeLoss;
                    else
                        thisWakeLoss = netAEP[i].sectorWakeLoss[WD_Ind];

                    break;
                }
            }

            return thisWakeLoss;
        }

        /// <summary> Finds all flow separation nodes (used in flow sep. model) and populates flowSepNodes. </summary>
        public void GetFlowSepNodes(Continuum thisInst)
        {
            int numWD = thisInst.metList.numWD;
            NodeCollection nodeList = new NodeCollection();

            Nodes thisNode = nodeList.GetTurbNode(this);
            flowSepNodes = nodeList.FindAllFlowSeps(thisNode, thisInst, numWD);

        }

        /// <summary> Removes avgWS with specified avgWS_Ind. </summary>
        public void RemoveAvgWS(int avgWS_index)
        {
            int newCount = AvgWSEst_Count - 1;

            if (newCount > 0)
            {
                Avg_Est[] tempList = new Avg_Est[newCount];
                int tempindex = 0;

                for (int i = 0; i < AvgWSEst_Count; i++)
                {
                    if (i != avgWS_index)
                    {
                        tempList[tempindex] = avgWS_Est[i];
                        tempindex++;
                    }
                }

                avgWS_Est = tempList;
            }
            else
                avgWS_Est = null;

        }

        /// <summary> Clears waked WS and net energy time series ests from avgWS with specified wake loss model and sets haveNetTS to false. </summary>
        public void ClearNetEstsFromAvgWS(Wake_Model thisWakeModel, WakeCollection wakeList)
        {
            for (int i = 0; i < AvgWSEst_Count; i++)
                if (wakeList.IsSameWakeModel(thisWakeModel, avgWS_Est[i].wakeModel))
                {
                    if (avgWS_Est[i].timeSeries != null)
                        for (int n = 0; n < avgWS_Est[i].timeSeries.Length; n++)
                        {
                            avgWS_Est[i].timeSeries[n].wakedWS = 0;
                            avgWS_Est[i].timeSeries[n].netEnergy = 0;
                        }
                    avgWS_Est[i].haveNetTS = false;
                    avgWS_Est[i].wakeModel = null;
                    avgWS_Est[i].waked = new EstWS_Data();
                    avgWS_Est[i].wakedMonthlyVals = null;
                }
        }

        /// <summary> Clears gross energy time series ests from avgWS with specified power curve and sets haveGrossTS to false. </summary>
        public void ClearGrossEstsFromAvgWS(string powerCurveName)
        {
            for (int i = 0; i < AvgWSEst_Count; i++)
                if (avgWS_Est[i].powerCurve.name == powerCurveName)
                {
                    if (avgWS_Est[i].timeSeries != null)
                        for (int n = 0; n < avgWS_Est[i].timeSeries.Length; n++)
                            avgWS_Est[i].timeSeries[n].grossEnergy = 0;

                    avgWS_Est[i].haveGrossTS = false;
                    avgWS_Est[i].powerCurve.Clear();
                }

        }

        /// <summary> Removes net AEP with specified power curve name. </summary>
        public void RemoveNetAEP_byPowerCurve(string powerCurveName)
        {
            int newCount = 0;

            for (int i = 0; i <= NetAEP_Count - 1; i++)
                if (netAEP[i].wakeModel.powerCurve.name != powerCurveName)
                    newCount++;

            if (newCount > 0)
            {
                Net_Energy_Est[] tempList = new Net_Energy_Est[newCount];
                int tempindex = 0;

                for (int i = 0; i <= NetAEP_Count - 1; i++)
                {
                    if (netAEP[i].wakeModel.powerCurve.name != powerCurveName)
                    {
                        tempList[tempindex] = netAEP[i];
                        tempindex++;
                    }
                }

                netAEP = tempList;
            }
            else
                netAEP = null;

        }

        /// <summary> Removes netAEP estimate(s) with specified wake loss model. </summary>
        public void RemoveNetAEP_byWakeModel(Wake_Model thisWakeModel, WakeCollection wakeList)
        {
            int newCount = 0;

            for (int i = 0; i < NetAEP_Count; i++)
                if (wakeList.IsSameWakeModel(thisWakeModel, netAEP[i].wakeModel) == false)
                    newCount++;

            if (newCount > 0)
            {
                Net_Energy_Est[] tempList = new Net_Energy_Est[newCount];
                int tempIndex = 0;

                for (int i = 0; i < NetAEP_Count; i++)
                {
                    if (wakeList.IsSameWakeModel(thisWakeModel, netAEP[i].wakeModel) == false)
                    {
                        tempList[tempIndex] = netAEP[i];
                        tempIndex++;
                    }
                }

                netAEP = tempList;
            }
            else
                netAEP = null;

        }

        /// <summary> Removes grossAEP with specified Gross_ind. </summary>
        public void RemoveGrossAEP(int grossIndex)
        {
            int newCount = GrossAEP_Count - 1;

            if (newCount > 0)
            {
                Gross_Energy_Est[] tempList = new Gross_Energy_Est[newCount];
                int tempIndex = 0;

                for (int i = 0; i < GrossAEP_Count; i++)
                {
                    if (i != grossIndex)
                    {
                        tempList[tempIndex] = grossAEP[i];
                        tempIndex++;
                    }
                }

                grossAEP = tempList;
            }
            else
                grossAEP = null;

        }

        /// <summary> Removes netAEP with specified Net_ind. </summary>
        public void RemoveNetAEP(int netIndex)
        {
            int newCount = NetAEP_Count - 1;

            if (newCount > 0)
            {
                Net_Energy_Est[] tempList = new Net_Energy_Est[newCount];
                int tempIndex = 0;

                for (int i = 0; i <= NetAEP_Count - 1; i++)
                {
                    if (i != netIndex)
                    {
                        tempList[tempIndex] = netAEP[i];
                        tempIndex++;
                    }
                }

                netAEP = tempList;
            }
            else
                netAEP = null;

        }

        /// <summary> Resets all calculated values (including elev, exposure, grid stats, WS_Ests, AEP_ests). </summary>
        public void ClearAllCalcs()
        {
            elev = 0;
            expo = null;
            gridStats.stats = null;
            WS_Estimate = null;
            avgWS_Est = null;
            grossAEP = null;
            netAEP = null;
            flowSepNodes = null;
        }

        /// <summary>
        /// Calculates waked wind speed (overall and sectorwise) and wake losses at turbine site based on specified wake coefficients and wake model.
        /// Adds waked wind speed estimates to Avg_Est object. Adds wake loss estimates and net energy estiamtes to Net_Energy_Estimate object. 
        /// </summary>        
        public void CalcTurbineWakeLosses(Continuum thisInst, WakeCollection.WakeLossCoeffs[] wakeCoeffs, Wake_Model thisWakeModel)
        {
            bool foundAvgEst = false;
            int avgWS_index = 0;
            // Check to see if need to create one or if one exists with only free-stream WS
            for (int j = 0; j < AvgWSEst_Count; j++)
            {
                if (avgWS_Est[j].wakeModel == null || thisInst.wakeModelList.IsSameWakeModel(thisWakeModel, avgWS_Est[j].wakeModel))
                {
                    avgWS_index = j;
                    foundAvgEst = true;
                    break;
                }
            }

            if (foundAvgEst == false)
            { // need to create one
                Avg_Est newAvgEst = new Avg_Est();
                AddAvgWS_Estimate(newAvgEst);
                avgWS_index = AvgWSEst_Count - 1;
            }

            Gross_Energy_Est grossEst = GetGrossEnergyEst(thisWakeModel.powerCurve);

            AddNetAEP(thisWakeModel, 0, 0, 0, 0, 0, null);
            int netEstInd = NetAEP_Count - 1;

            avgWS_Est[avgWS_index].wakeModel = thisWakeModel;

            WakeCollection.WakeCalcResults wakeResults = thisInst.wakeModelList.CalcWakeLosses(wakeCoeffs, UTMX, UTMY, avgWS_Est[avgWS_index].freeStream.sectorWS_Dist, grossEst.AEP, grossEst.sectorEnergy,
                                                     thisInst, avgWS_Est[avgWS_index].wakeModel, avgWS_Est[avgWS_index].freeStream.windRose);

            avgWS_Est[avgWS_index].waked.WS = wakeResults.wakedWS;
            avgWS_Est[avgWS_index].waked.WS_Dist = wakeResults.WS_Dist;
            avgWS_Est[avgWS_index].waked.sectorWS_Dist = wakeResults.sectorDist;
            avgWS_Est[avgWS_index].waked.sectorWS = wakeResults.sectorWakedWS;
            avgWS_Est[avgWS_index].powerCurve = thisWakeModel.powerCurve;

            netAEP[netEstInd].AEP = wakeResults.netEnergy;
            netAEP[netEstInd].sectorEnergy = wakeResults.sectorNetEnergy;

            for (int WD = 0; WD <= netAEP[netEstInd].sectorEnergy.Length - 1; WD++)
                netAEP[netEstInd].sectorEnergy[WD] = netAEP[netEstInd].sectorEnergy[WD];

            netAEP[netEstInd].wakeLoss = wakeResults.wakeLoss;
            netAEP[netEstInd].sectorWakeLoss = wakeResults.sectorWakeLoss;
            netAEP[netEstInd].CF = thisInst.turbineList.CalcCapacityFactor(netAEP[netEstInd].AEP, thisWakeModel.powerCurve.ratedPower);

            // Calculate weibull params          
            MetCollection.Weibull_params thisWeibull = thisInst.metList.CalcWeibullParams(avgWS_Est[avgWS_index].waked.WS_Dist, avgWS_Est[avgWS_index].waked.sectorWS_Dist, avgWS_Est[avgWS_index].waked.WS);

            avgWS_Est[avgWS_index].waked.weibullParams.overall_A = thisWeibull.overall_A;
            avgWS_Est[avgWS_index].waked.weibullParams.overall_k = thisWeibull.overall_k;
            avgWS_Est[avgWS_index].waked.weibullParams.sector_A = thisWeibull.sector_A;
            avgWS_Est[avgWS_index].waked.weibullParams.sector_k = thisWeibull.sector_k;

        }

        /// <summary> For each predictor met and each model (i.e. 4 Radii), finds a path of nodes to turbine site, 
        ///  does wind speed estimate along nodes using specified model and creates WS_Ests() for each met and each model. </summary>        
        public void DoTurbineCalcs(Continuum thisInst, Model[] models)
        {
            NodeCollection nodeList = new NodeCollection();

            if (models == null)
                return;

            int numModels = models.Length;

            Met[] metsToUse = thisInst.metList.GetMets(models[0].metsUsed, null);
            double[] windRose = thisInst.metList.GetInterpolatedWindRose(models[0].metsUsed, UTMX, UTMY, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

            for (int j = 0; j < metsToUse.Length; j++)
            {
                Met thisMet = metsToUse[j];

                for (int m = 0; m < numModels; m++)
                {
                    // Check to see if already have this model estimate calculated
                    bool alreadyCalc = false;
                    for (int n = 0; n < WSEst_Count; n++)
                    {
                        if (WS_Estimate[n].model != null)
                        {
                            if (thisInst.modelList.IsSameModel(WS_Estimate[n].model, models[m]) && thisMet.name == WS_Estimate[n].predictorMetName)
                            {
                                alreadyCalc = true;
                                break;
                            }
                        }
                    }

                    if (alreadyCalc == false)
                    {
                        WS_Ests newWS_Est = new WS_Ests();
                        newWS_Est.predictorMetName = thisMet.name;
                        newWS_Est.model = models[m];

                        AddWS_Estimate(newWS_Est);

                        int WS_Est_ind = WSEst_Count - 1;
                        Nodes targetNode = nodeList.GetTurbNode(this);
                        Nodes startNode = nodeList.GetMetNode(thisMet);

                        WS_Estimate[WS_Est_ind].pathOfNodes = nodeList.FindPathOfNodes(startNode, targetNode, models[m], thisInst);

                        WS_Ests thisEst = DoWS_EstAlongNodes(thisMet, models[m], WS_Estimate[WS_Est_ind].pathOfNodes, thisInst, windRose);
                        WS_Estimate[WS_Est_ind].sectorWS = thisEst.sectorWS;
                        WS_Estimate[WS_Est_ind].sectorWS_at_nodes = thisEst.sectorWS_at_nodes;
                        WS_Estimate[WS_Est_ind].WS = thisEst.WS;
                        WS_Estimate[WS_Est_ind].WS_at_nodes = thisEst.WS_at_nodes;

                    }
                }
            }

        }


        /// <summary> Calculates and returns wind speed estimate along path of nodes (from Met to turbine) using specified model and predictor met. </summary>        
        public WS_Ests DoWS_EstAlongNodes(Met predMet, Model model, Nodes[] pathOfNodes, Continuum thisInst, double[] windRose)
        {
            WS_Ests thisWS_Est = new WS_Ests();

            if (thisInst.metList.ThisCount == 0)
                return thisWS_Est;

            NodeCollection nodeList = new NodeCollection();

            int numWD = thisInst.metList.numWD;
            int numNodes = 0;
            if (pathOfNodes != null) numNodes = pathOfNodes.Length;

            thisWS_Est.sectorWS = new double[numWD];
            Nodes endNode = nodeList.GetTurbNode(this);
            ModelCollection.WS_Est_Struct WS_EstStr = thisInst.modelList.DoWS_Estimate(predMet, endNode, pathOfNodes, model, thisInst);

            if (numNodes > 0)
            {
                thisWS_Est.WS_at_nodes = new double[numNodes];
                thisWS_Est.sectorWS_at_nodes = new double[numNodes, numWD];
            }

            thisWS_Est.sectorWS = WS_EstStr.sectorWS;

            for (int nodeIndex = 0; nodeIndex <= numNodes - 1; nodeIndex++)
                for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                    thisWS_Est.sectorWS_at_nodes[nodeIndex, WD_Ind] = WS_EstStr.sectorWS_AtNodes[nodeIndex, WD_Ind];

            if (numNodes > 0)
            {
                for (int nodeIndex = 0; nodeIndex < numNodes; nodeIndex++)
                {
                    thisWS_Est.WS_at_nodes[nodeIndex] = 0;
                    for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                        thisWS_Est.WS_at_nodes[nodeIndex] = thisWS_Est.WS_at_nodes[nodeIndex] + thisWS_Est.sectorWS_at_nodes[nodeIndex, WD_Ind] * windRose[WD_Ind];
                }
            }

            thisWS_Est.WS = 0;
            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                thisWS_Est.WS = thisWS_Est.WS + thisWS_Est.sectorWS[WD_Ind] * windRose[WD_Ind];

            return thisWS_Est;
        }

        /// <summary> Creates and adds new Avg_Est based on time series data. If forRoundRobin is true then doesn't assign uncertainty. </summary>        
        public void GenerateAvgWSTimeSeries(ModelCollection.TimeSeries[] thisTS, Continuum thisInst, Wake_Model wakeModel, bool isImported,
            string MCP_Method, bool forRoundRobin, TurbineCollection.PowerCurve powerCurve)
        {
            if (thisTS.Length == 0)
                return;

            int numWD = thisInst.metList.numWD;
            int avgWS_index = 0;

            bool isWaked = true;
            if (wakeModel == null)
                isWaked = false;

            if (wakeModel.powerCurve.ratedPower == 0)
                isWaked = false;

            // Calculate free-stream WSWD distribution
            Met.WSWD_Dist freeStreamDist = thisInst.modelList.CalcWSWD_Dist(thisTS, thisInst, "Freestream");

            // Get wind speed uncertainty
            MetPairCollection.RR_WS_Ests uncert = new MetPairCollection.RR_WS_Ests();
            if (thisInst.metList.ThisCount > 1 && forRoundRobin == false)
                uncert = thisInst.metPairList.GetRoundRobinEst(thisInst.metList.ThisCount - 1, thisInst, MCP_Method);
            else if (thisInst.metList.ThisCount == 1 && forRoundRobin == false)
                uncert.RMS_All = GetDefaultUncertainty(freeStreamDist.windRose);

            bool foundAvgEst = false;
            // Check to see if need to create one or if one exists with only WS
            for (int j = 0; j < AvgWSEst_Count; j++)
            {
                if (avgWS_Est[j].isImported == isImported && avgWS_Est[j].haveGrossTS == false && avgWS_Est[j].haveNetTS == false)
                {
                    avgWS_index = j;
                    foundAvgEst = true;
                    break;
                }
            }

            if (foundAvgEst == false)
            { // need to create one
                Avg_Est newAvgEst = new Avg_Est();
                AddAvgWS_Estimate(newAvgEst);
                avgWS_index = AvgWSEst_Count - 1;
            }

            avgWS_Est[avgWS_index].isImported = isImported;
            avgWS_Est[avgWS_index].wakeModel = wakeModel;
            avgWS_Est[avgWS_index].powerCurve = powerCurve;

            avgWS_Est[avgWS_index].freeStream.WS = freeStreamDist.WS;
            avgWS_Est[avgWS_index].freeStream.WS_Dist = freeStreamDist.WS_Dist;
            avgWS_Est[avgWS_index].freeStream.windRose = freeStreamDist.windRose;
            avgWS_Est[avgWS_index].freeStream.sectorWS_Dist = freeStreamDist.sectorWS_Dist;
            avgWS_Est[avgWS_index].freeStream.sectorWS = new double[numWD];

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                avgWS_Est[avgWS_index].freeStream.sectorWS[WD_Ind] = freeStreamDist.sectorWS_Ratio[WD_Ind] * freeStreamDist.WS;

            // Calculate weibull params  
            avgWS_Est[avgWS_index].freeStream.weibullParams = thisInst.metList.CalcWeibullParams(avgWS_Est[avgWS_index].freeStream.WS_Dist, avgWS_Est[avgWS_index].freeStream.sectorWS_Dist,
                                                                            avgWS_Est[avgWS_index].freeStream.WS);

            if (isWaked)
            {
                Met.WSWD_Dist wakedDist = thisInst.modelList.CalcWSWD_Dist(thisTS, thisInst, "Waked");
                avgWS_Est[avgWS_index].waked.WS = wakedDist.WS;
                avgWS_Est[avgWS_index].waked.WS_Dist = wakedDist.WS_Dist;
                avgWS_Est[avgWS_index].waked.windRose = wakedDist.windRose;
                avgWS_Est[avgWS_index].waked.sectorWS_Dist = wakedDist.sectorWS_Dist;
                avgWS_Est[avgWS_index].waked.sectorWS = new double[numWD];

                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                    avgWS_Est[avgWS_index].waked.sectorWS[WD_Ind] = wakedDist.sectorWS_Ratio[WD_Ind] * wakedDist.WS;

                // Calculate waked weibull params  
                avgWS_Est[avgWS_index].waked.weibullParams = thisInst.metList.CalcWeibullParams(avgWS_Est[avgWS_index].waked.WS_Dist, avgWS_Est[avgWS_index].waked.sectorWS_Dist,
                                                                                avgWS_Est[avgWS_index].waked.WS);
            }

            avgWS_Est[avgWS_index].timeSeries = thisTS;
            avgWS_Est[avgWS_index].uncert = uncert.RMS_All;

            avgWS_Est[avgWS_index].FS_MonthlyVals = CalcMonthlyWS_Values(thisTS, thisInst, "Freestream");

            if (isWaked)
                avgWS_Est[avgWS_index].wakedMonthlyVals = CalcMonthlyWS_Values(thisTS, thisInst, "Waked");

            if (avgWS_Est[avgWS_index].timeSeries != null)
            {
                if (avgWS_Est[avgWS_index].timeSeries[0].grossEnergy != 0)
                    avgWS_Est[avgWS_index].haveGrossTS = true;
                else
                    avgWS_Est[avgWS_index].haveGrossTS = false;

                if (avgWS_Est[avgWS_index].timeSeries[0].netEnergy != 0)
                    avgWS_Est[avgWS_index].haveNetTS = true;
                else
                    avgWS_Est[avgWS_index].haveNetTS = false;
            }

        }

        /// <summary>
        /// Combines WS_Ests (i.e. estimates at turbine site for each predicting met and each model) to form Avg_Est.
        /// Calculates average overall and sectorwise WS and WS distributions, uncertainty, and Weibull params
        /// FOR TAB FILES ONLY (not time series)
        /// </summary>        
        public void GenerateAvgWSFromTABs(Continuum thisInst, Model[] models, double[] windRose, bool forRoundRobin)
        {
            if (thisInst.metList.ThisCount == 0) return;

            double avgWS = 0;
            double avgWeight = 0;
            int numWD = thisInst.metList.numWD;
            double[] avgSectorWS = new double[numWD];

            if (models[0] == null)
                return;

            string[] metsUsed = models[0].metsUsed;
            NodeCollection nodeList = new NodeCollection();

            Met[] predictingMets = thisInst.metList.GetMets(metsUsed, null);
            Met predictingMet = null;

            Nodes turbineNode = nodeList.GetTurbNode(this);
            ModelCollection.ModelWeights[] indivMetWeights = thisInst.modelList.GetWS_EstWeights(predictingMets, turbineNode, models,
                thisInst.metList.GetAvgWindRoseMetsUsed(metsUsed, Met.TOD.All, Met.Season.All, thisInst.modeledHeight), thisInst.radiiList);

            for (int j = 0; j < WSEst_Count; j++)
            {
                for (int k = 0; k < thisInst.metList.ThisCount; k++)
                {
                    if (thisInst.metList.metItem[k].name == WS_Estimate[j].predictorMetName)
                    {
                        predictingMet = thisInst.metList.metItem[k];
                        break;
                    }
                }

                double modelWeight = thisInst.modelList.GetWeightForMetAndModel(indivMetWeights, predictingMet, WS_Estimate[j].model);
                avgWS = avgWS + WS_Estimate[j].WS * modelWeight;
                WS_Estimate[j].WS_weight = modelWeight;
                avgWeight = avgWeight + modelWeight;

                for (int WD = 0; WD <= numWD - 1; WD++)
                    avgSectorWS[WD] = avgSectorWS[WD] + WS_Estimate[j].sectorWS[WD] * modelWeight;

            }

            Avg_Est newAvgEst = new Avg_Est();
            newAvgEst.isImported = models[0].isImported;
            AddAvgWS_Estimate(newAvgEst);
            int avgWS_index = AvgWSEst_Count - 1;

            avgWS_Est[avgWS_index].freeStream.windRose = windRose;

            MetPairCollection.RR_WS_Ests uncert = new MetPairCollection.RR_WS_Ests();
            if (thisInst.metList.ThisCount > 1 && forRoundRobin == false)
                uncert = thisInst.metPairList.GetRoundRobinEst(thisInst.metList.ThisCount - 1, thisInst, null);
            else if (thisInst.metList.ThisCount == 1 && forRoundRobin == false)
                uncert.RMS_All = GetDefaultUncertainty(avgWS_Est[avgWS_index].freeStream.windRose);

            avgWS_Est[avgWS_index].freeStream.WS = 0;
            avgWS_Est[avgWS_index].uncert = uncert.RMS_All;
            avgWS_Est[avgWS_index].freeStream.sectorWS = new double[numWD];

            if (avgWeight != 0)
            {
                avgWS_Est[avgWS_index].freeStream.WS = avgWS / avgWeight;

                for (int WD = 0; WD <= numWD - 1; WD++)
                    avgWS_Est[avgWS_index].freeStream.sectorWS[WD] = avgSectorWS[WD] / avgWeight;

            }

            int numWS = thisInst.metList.numWS;
            avgWS_Est[avgWS_index].freeStream.sectorWS_Dist = new double[numWD, numWS];

            for (int WD = 0; WD < numWD; WD++)
            {
                double[] WS_Dist = thisInst.metList.CalcWS_DistForTurbOrMap(metsUsed, avgWS_Est[avgWS_index].freeStream.sectorWS[WD], WD, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

                for (int WS = 0; WS < numWS; WS++)
                    avgWS_Est[avgWS_index].freeStream.sectorWS_Dist[WD, WS] = WS_Dist[WS];

            }

            // instead of forming WS_Dist from overall WS, use sectorWS distributions (as is done in net energy calcs) 10/10/2016
            avgWS_Est[avgWS_index].freeStream.WS_Dist = thisInst.metList.CalcOverallWS_Dist(avgWS_Est[avgWS_index].freeStream.sectorWS_Dist, windRose);

            // Calculate weibull params
            avgWS_Est[avgWS_index].freeStream.weibullParams = thisInst.metList.CalcWeibullParams(avgWS_Est[avgWS_index].freeStream.WS_Dist, avgWS_Est[avgWS_index].freeStream.sectorWS_Dist,
                                                                            avgWS_Est[avgWS_index].freeStream.WS);


        }

        /// <summary> Calculates and returns default estimated uncertainty. Based on results of single met study using Continuum 1. Needs to be revisited. </summary>
        public double GetDefaultUncertainty(double[] windRose)
        {
            double overallUncert = 0;

            double adjFact = 1.2f; // what is this based on?
            // these errors are based on overall WS errors.
            if (gridStats.GetOverallP10(windRose, 0, "DW") < 15)
                overallUncert = 0.0167f * adjFact;
            else if (gridStats.GetOverallP10(windRose, 0, "DW") < 85)
                overallUncert = 0.021f * adjFact;
            else
                overallUncert = 0.03f * adjFact;

            return overallUncert;

        }



        /// <summary> Calculate and returns mean monthly wind speed estimates using time series of either waked or freestream wind speed estimates. </summary>        
        public MonthlyWS_Vals[] CalcMonthlyWS_Values(ModelCollection.TimeSeries[] thisTS, Continuum thisInst, string wakedOrFreestream)
        {
            MonthlyWS_Vals[] monthlyWS_Vals = new MonthlyWS_Vals[0];

            if (thisTS == null) return monthlyWS_Vals;

            int TS_Length = thisTS.Length;
            if (TS_Length <= 1) return monthlyWS_Vals;

            // Figure out time interval (in hours)
            TimeSpan timeSpan = thisTS[1].dateTime - thisTS[0].dateTime;
            double timeInt = timeSpan.TotalMinutes / 60.0;

            // Start on first hour on first of month
            int TS_Ind = 0;
            int thisDay = thisTS[TS_Ind].dateTime.Day;
            int thisHour = thisTS[TS_Ind].dateTime.Hour;
            bool atFirstDay = false;
            if (thisDay == 1 && thisHour == 0)
                atFirstDay = true;

            while (atFirstDay == false && TS_Ind < thisTS.Length - 1)
            {
                TS_Ind++;
                thisDay = thisTS[TS_Ind].dateTime.Day;
                thisHour = thisTS[TS_Ind].dateTime.Hour;

                if (thisDay == 1 && thisHour == 0)
                    atFirstDay = true;
            }

            if (atFirstDay == false)
                return monthlyWS_Vals;

            int lastMonth = thisTS[TS_Ind].dateTime.Month;
            int lastYear = thisTS[TS_Ind].dateTime.Year;

            double avgWS = 0;
            int WS_count = 0;

            MERRA merra = new MERRA(); // Using some functions from this class (i.e. Get_Num_Days_in_Month)
            int numDaysInMonth = merra.Get_Num_Days_in_Month(lastMonth, lastYear);
            int numMonthlyInts = Convert.ToInt32(Math.Round(numDaysInMonth / timeInt * 24, 0));
            ModelCollection.TimeSeries[] monthlyTS = new ModelCollection.TimeSeries[numMonthlyInts];
            int monthlyInd = 0;
            int Ind = 0;

            for (int t = TS_Ind; t < TS_Length; t++)
            {
                int month = thisTS[t].dateTime.Month;
                int year = thisTS[t].dateTime.Year;

                if (month == lastMonth && year == lastYear)
                {
                    double thisWS = 0;

                    if (wakedOrFreestream == "Waked")
                        thisWS = thisTS[t].wakedWS;
                    else if (wakedOrFreestream == "Freestream")
                        thisWS = thisTS[t].freeStreamWS;
                    else
                    {
                        MessageBox.Show("Invalid flag in CalcMonthlyWS_Values");
                        return monthlyWS_Vals;
                    }

                    if (thisWS != -999)
                    {
                        avgWS = avgWS + thisWS;
                        WS_count++;
                    }
                    else
                        avgWS = avgWS;


                    monthlyTS[Ind].dateTime = thisTS[t].dateTime;

                    if (thisWS != -999)
                    {
                        if (wakedOrFreestream == "Waked")
                            monthlyTS[Ind].wakedWS = thisWS;
                        else
                            monthlyTS[Ind].freeStreamWS = thisWS;
                    }

                    monthlyTS[Ind].WD = thisTS[t].WD;
                    Ind++;
                }
                else
                {
                    // Resize Monthly time series in case have some empty  entries
                    Array.Resize(ref monthlyTS, Ind);
                    // Calculate WSWD distribution for month
                    Array.Resize(ref monthlyWS_Vals, monthlyInd + 1);

                    if (WS_count > 0)
                        avgWS = avgWS / WS_count;

                    monthlyWS_Vals[monthlyInd].year = lastYear;
                    monthlyWS_Vals[monthlyInd].month = lastMonth;
                    monthlyWS_Vals[monthlyInd].avgWS = avgWS;
                    monthlyWS_Vals[monthlyInd].WS_Dist = thisInst.modelList.CalcWSWD_Dist(monthlyTS, thisInst, wakedOrFreestream);
                    monthlyInd++;

                    numDaysInMonth = merra.Get_Num_Days_in_Month(month, year);
                    numMonthlyInts = Convert.ToInt32(Math.Round(numDaysInMonth / timeInt * 24, 0));
                    Array.Clear(monthlyTS, 0, monthlyTS.Length);
                    Array.Resize(ref monthlyTS, numMonthlyInts);

                    /////
                    Ind = 0;
                    avgWS = 0;
                    WS_count = 0;

                    double thisWS = 0;

                    if (wakedOrFreestream == "Waked")
                        thisWS = thisTS[t].wakedWS;
                    else if (wakedOrFreestream == "Freestream")
                        thisWS = thisTS[t].freeStreamWS;
                    else
                    {
                        MessageBox.Show("Invalid flag in CalcMonthlyWS_Values");
                        return monthlyWS_Vals;
                    }

                    if (thisWS != -999)
                    {
                        avgWS = avgWS + thisWS;
                        WS_count++;
                    }

                    monthlyTS[Ind].dateTime = thisTS[t].dateTime;

                    if (thisWS != -999)
                    {
                        if (wakedOrFreestream == "Waked")
                            monthlyTS[Ind].wakedWS = thisWS;
                        else
                            monthlyTS[Ind].freeStreamWS = thisWS;
                    }

                    monthlyTS[Ind].WD = thisTS[t].WD;
                    Ind++;

                    lastMonth = month;
                    lastYear = year;
                }

            }

            // Resize Monthly time series in case have some empty  entries
            Array.Resize(ref monthlyTS, Ind);

            // If last month of data is complete (i.e. if next time step is first of month) then resize array and calculate WSWD distribution for month
            DateTime nextTS = thisTS[thisTS.Length - 1].dateTime.AddHours(timeInt);
            if (nextTS.Day == 1 && nextTS.Hour == 0)
            {
                Array.Resize(ref monthlyWS_Vals, monthlyInd + 1);

                if (WS_count > 0)
                    avgWS = avgWS / WS_count;

                monthlyWS_Vals[monthlyInd].year = lastYear;
                monthlyWS_Vals[monthlyInd].month = lastMonth;
                monthlyWS_Vals[monthlyInd].avgWS = avgWS;
                monthlyWS_Vals[monthlyInd].WS_Dist = thisInst.modelList.CalcWSWD_Dist(monthlyTS, thisInst, wakedOrFreestream);
            }

            return monthlyWS_Vals;

        }

        /// <summary> Calculates and adds gross energy estimate based on energy production time series. </summary>
        public void CalcGrossAEPFromTimeSeries(Continuum thisInst, ModelCollection.TimeSeries[] thisTS, TurbineCollection.PowerCurve powerCurve)
        {
            if (thisInst.metList.numWD == 0) return;
            int numWD = thisInst.metList.numWD;
            int numWS = thisInst.metList.numWS;

            // Check to see if energy calc already done
            bool alreadyCalc = false;
            for (int k = 0; k < GrossAEP_Count; k++)
            {
                if (grossAEP[k].powerCurve.name == powerCurve.name)
                {
                    alreadyCalc = true;
                    break;
                }
            }

            Avg_Est avgEst = GetAvgWS_Est(new Wake_Model());
            EstWS_Data estData = avgEst.freeStream;

            if (alreadyCalc == false)
            {
                Gross_Energy_Est thisGross = new Gross_Energy_Est();
                thisGross.powerCurve = powerCurve;
                thisInst.modelList.CalcGrossAEP_AndMonthlyEnergy(ref thisGross, thisTS, thisInst); // calculates long-term P50 AEP, sectorwise energy, and monthly values

                // P90 and P99 estimates
                double[,] sectorDist = new double[numWD, numWS];

                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                {
                    double P90_WS = estData.sectorWS[WD_Ind] - estData.sectorWS[WD_Ind] * 0.0128155f;
                    double[] WS_Dist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), P90_WS, WD_Ind, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
                    for (int WS_ind = 0; WS_ind <= numWS - 1; WS_ind++)
                        sectorDist[WD_Ind, WS_ind] = WS_Dist[WS_ind];
                }

                double[] P90_Dist = thisInst.metList.CalcOverallWS_Dist(sectorDist, thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, Met.TOD.All, Met.Season.All));

                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                {
                    double P99_WS = estData.sectorWS[WD_Ind] - estData.sectorWS[WD_Ind] * 0.02326f;
                    double[] WS_Dist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), P99_WS, WD_Ind, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
                    for (int WS_ind = 0; WS_ind <= numWS - 1; WS_ind++)
                        sectorDist[WD_Ind, WS_ind] = WS_Dist[WS_ind];
                }

                double[] P99_Dist = thisInst.metList.CalcOverallWS_Dist(sectorDist, thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, Met.TOD.All, Met.Season.All));

                double P90_AEP = thisInst.turbineList.CalcAndReturnGrossAEP(P90_Dist, thisInst.metList, powerCurve.name);
                double P99_AEP = thisInst.turbineList.CalcAndReturnGrossAEP(P99_Dist, thisInst.metList, powerCurve.name);

                thisGross.P90 = P90_AEP;
                thisGross.P99 = P99_AEP;

                AddGrossEstTimeSeries(thisGross);
            }
        }

        /// <summary> Calculates and adds net energy estimate based on energy production time series. </summary>
        public void CalcNetAEPFromTimeSeries(Continuum thisInst, ModelCollection.TimeSeries[] thisTS, TurbineCollection.PowerCurve powerCurve, Wake_Model wakeModel)
        {
            if (thisInst.metList.numWD == 0) return;

            // Check to see if energy calc already done
            bool alreadyCalc = false;
            for (int k = 0; k < NetAEP_Count; k++)
            {
                if (thisInst.wakeModelList.IsSameWakeModel(netAEP[k].wakeModel, wakeModel))
                {
                    alreadyCalc = true;
                    break;
                }
            }

            if (alreadyCalc == false)
            {
                Net_Energy_Est thisNet = new Net_Energy_Est();
                thisNet.wakeModel = wakeModel;

                Gross_Energy_Est thisGross = GetGrossEnergyEst(powerCurve);
                thisInst.modelList.CalcNetAEP_AndMonthlyEnergy(ref thisNet, thisTS, thisInst); // calculates long-term AEP, sectorwise energy, and monthly values

                double otherLoss = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
                thisNet.sectorWakeLoss = thisInst.wakeModelList.CalcThisSectWakeLoss(thisNet.sectorEnergy, thisGross.sectorEnergy, otherLoss);
                thisNet.wakeLoss = thisInst.wakeModelList.CalcThisWakeLoss(thisNet.AEP, thisGross.AEP, otherLoss);

                AddNetEstTimeSeries(thisNet);
            }
        }

        /// <summary> Calculates and returns annual value (thisParam = "Avg WS", "Gross AEP", "Net AEP") </summary>        
        public double CalcYearlyValue(int thisYear, string thisParam, Wake_Model thisWakeModel, TurbineCollection.PowerCurve powerCurve)
        {
            double thisAnnual = 0;
            double sumAnnual = 0;
            MERRA thisMERRA = new MERRA(); // For Get_Num_Days_in_Month function

            bool isWaked = true;
            if (thisWakeModel == null)
                isWaked = false;
            else if (thisWakeModel.powerCurve.name == null)
                isWaked = false;

            if (thisParam == "Avg WS")
            {
                if (AvgWSEst_Count == 0)
                    return thisAnnual;
                else
                {
                    Avg_Est thisAvgEst = GetAvgWS_Est(thisWakeModel);
                    MonthlyWS_Vals[] thisMonthly;

                    if (isWaked == false)
                        thisMonthly = thisAvgEst.FS_MonthlyVals;
                    else
                        thisMonthly = thisAvgEst.wakedMonthlyVals;

                    if (thisMonthly == null)
                        return thisAnnual;

                    for (int i = 0; i < thisMonthly.Length; i++)
                    {
                        if (thisMonthly[i].year == thisYear)
                        {
                            int numDays = thisMERRA.Get_Num_Days_in_Month(thisMonthly[i].month, thisMonthly[i].year);
                            thisAnnual = thisAnnual + thisMonthly[i].avgWS * numDays;
                            sumAnnual = sumAnnual + numDays;
                        }
                    }

                    if (sumAnnual > 0)
                        thisAnnual = thisAnnual / sumAnnual;

                }
            }
            else if (thisParam == "Gross AEP")
            {
                if (GrossAEP_Count == 0)
                    return thisAnnual;
                else
                {
                    Gross_Energy_Est thisGrossEst = GetGrossEnergyEst(powerCurve);

                    if (thisGrossEst.monthlyVals == null)
                        return thisAnnual;

                    for (int i = 0; i < thisGrossEst.monthlyVals.Length; i++)
                        if (thisGrossEst.monthlyVals[i].year == thisYear)
                            thisAnnual = thisAnnual + thisGrossEst.monthlyVals[i].energyProd;
                }
            }
            else if (thisParam == "Net AEP")
            {
                if (NetAEP_Count == 0)
                    return thisAnnual;
                else
                {
                    Net_Energy_Est thisNetEst = GetNetEnergyEst(thisWakeModel);

                    if (thisNetEst.monthlyVals == null)
                        return thisAnnual;

                    for (int i = 0; i < thisNetEst.monthlyVals.Length; i++)
                        if (thisNetEst.monthlyVals[i].year == thisYear)
                            thisAnnual = thisAnnual + thisNetEst.monthlyVals[i].energyProd;
                }
            }

            return thisAnnual;
        }

        /// <summary> Calculates and returns long-term monthly value ("Avg WS", "Gross AEP", "Net AEP", or "Wake Loss"). </summary>        
        public double CalcLT_MonthlyValue(string thisParam, int thisMonth, Wake_Model thisWakeModel, TurbineCollection.PowerCurve powerCurve)
        {
            double thisLT_Value = 0;
            int thisCount = 0;

            bool isWaked = true;
            if (thisWakeModel == null)
                isWaked = false;
            else if (thisWakeModel.powerCurve.name == null)
                isWaked = false;

            if (thisParam == "Avg WS")
            {
                Avg_Est thisAvgEst = GetAvgWS_Est(thisWakeModel);
                MonthlyWS_Vals[] thisMonthly = null;

                if (isWaked)
                    thisMonthly = thisAvgEst.wakedMonthlyVals;
                else
                    thisMonthly = thisAvgEst.FS_MonthlyVals;

                if (thisMonthly == null)
                    return thisLT_Value;
                else
                {
                    for (int i = 0; i < thisMonthly.Length; i++)
                    {
                        if (thisMonthly[i].month == thisMonth)
                        {
                            thisLT_Value = thisLT_Value + thisMonthly[i].avgWS;
                            thisCount++;
                        }
                    }
                }
            }
            else if (thisParam == "Gross AEP")
            {
                Gross_Energy_Est thisGrossEst = GetGrossEnergyEst(powerCurve);
                if (thisGrossEst.monthlyVals == null)
                    return thisLT_Value;
                else
                {
                    for (int i = 0; i < thisGrossEst.monthlyVals.Length; i++)
                    {
                        if (thisGrossEst.monthlyVals[i].month == thisMonth)
                        {
                            thisLT_Value = thisLT_Value + thisGrossEst.monthlyVals[i].energyProd;
                            thisCount++;
                        }
                    }
                }
            }
            else if (thisParam == "Net AEP")
            {
                Net_Energy_Est thisNetEst = GetNetEnergyEst(thisWakeModel);
                if (thisNetEst.monthlyVals == null)
                    return thisLT_Value;
                else
                {
                    for (int i = 0; i < thisNetEst.monthlyVals.Length; i++)
                    {
                        if (thisNetEst.monthlyVals[i].month == thisMonth)
                        {
                            thisLT_Value = thisLT_Value + thisNetEst.monthlyVals[i].energyProd;
                            thisCount++;
                        }
                    }
                }
            }
            else if (thisParam == "Wake Loss")
            {
                Gross_Energy_Est thisGrossEst = GetGrossEnergyEst(powerCurve);
                Net_Energy_Est thisNetEst = GetNetEnergyEst(thisWakeModel);

                if (thisNetEst.monthlyVals == null)
                    return thisLT_Value;
                else
                {
                    for (int i = 0; i < thisNetEst.monthlyVals.Length; i++)
                    {
                        if (thisNetEst.monthlyVals[i].month == thisMonth)
                        {
                            thisLT_Value = thisLT_Value + (thisGrossEst.monthlyVals[i].energyProd - thisNetEst.monthlyVals[i].energyProd) / thisGrossEst.monthlyVals[i].energyProd;
                            thisCount++;
                        }
                    }
                }
            }

            if (thisCount > 0)
                thisLT_Value = thisLT_Value / thisCount;

            return thisLT_Value;
        }

        /// <summary> Calculates and returns the waked wind speed standard deviation used in Effective TI calculations. </summary>        
        public double CalcWakedStDev(double thisX, double thisY, TurbineCollection.PowerCurve powerCurve, double ambSD, double avgWS, Continuum thisInst)
        {
            double wakedSD = 0;
            double cT = thisInst.turbineList.GetInterpPowerOrThrust(avgWS, powerCurve, "Thrust");
            double dist = thisInst.topo.CalcDistanceBetweenPoints(thisX, thisY, UTMX, UTMY) / powerCurve.RD;

            if (cT != 0)
                wakedSD = Math.Pow((Math.Pow(avgWS, 2) / (Math.Pow(1.5 + 0.8 * dist / Math.Pow(cT, 0.5), 2)) + Math.Pow(ambSD, 2)), 0.5);

            return wakedSD;
        }

        /// <summary> Calculates and returns effective TI at turbine site for a specified wind direction sector using ambient TI measured at met site. </summary>        
        public double[] CalcEffectiveTI(Met thisMet, double wohler, Continuum thisInst, TurbineCollection.PowerCurve powerCurve, int WD_Ind, double terrComplCorr)
        {
            double[] effectiveTI = new double[thisInst.metList.numWS];
            double[,] probWake = thisInst.turbineList.CalcProbOfWakeForEffectiveTI(thisInst, UTMX, UTMY, powerCurve);
            double[,] p90SD = new double[thisMet.turbulence.p90SD.GetUpperBound(0) + 1, thisMet.turbulence.p90SD.GetUpperBound(1) + 1];

            for (int i = 0; i < thisInst.metList.numWS; i++)
                for (int j = 0; j < thisInst.metList.numWD; j++)
                    p90SD[i, j] = thisMet.turbulence.p90SD[i, j] * terrComplCorr;

            for (int i = 0; i < thisInst.metList.numWS; i++)
            {
                double thisWS = thisInst.metList.WS_FirstInt - thisInst.metList.WS_IntSize / 2 + i * thisInst.metList.WS_IntSize;
                if (WD_Ind != thisInst.metList.numWD) // get TI for specfic WD
                {
                    if (thisWS >= powerCurve.cutInWS && thisWS <= powerCurve.cutOutWS)
                    {
                        double sumWeightedWakeProb = 0;
                        double sumWakeProb = 0;

                        // Sum up probability of wake and waked induced TI
                        for (int j = 0; j < thisInst.turbineList.TurbineCount; j++)
                        {
                            if (thisInst.turbineList.turbineEsts[j].name != name)
                            {
                                double wakedSD = CalcWakedStDev(thisInst.turbineList.turbineEsts[j].UTMX, thisInst.turbineList.turbineEsts[j].UTMY, powerCurve,
                                    p90SD[i, WD_Ind], thisMet.turbulence.avgWS[i, WD_Ind], thisInst);
                                sumWeightedWakeProb = sumWeightedWakeProb + probWake[j, WD_Ind] * Math.Pow(wakedSD, wohler);
                                sumWakeProb = sumWakeProb + probWake[j, WD_Ind];
                            }
                        }

                        effectiveTI[i] = Math.Pow((1 - sumWakeProb) * Math.Pow(p90SD[i, WD_Ind], wohler) + sumWeightedWakeProb, (1 / wohler));

                        if (thisMet.turbulence.avgWS[i, WD_Ind] > 0 && p90SD[i, WD_Ind] > 0)
                            effectiveTI[i] = effectiveTI[i] / thisMet.turbulence.avgWS[i, WD_Ind];
                        else
                            effectiveTI[i] = 0;
                    }
                    else
                    {
                        if (thisMet.turbulence.avgWS[i, WD_Ind] > 0)
                            effectiveTI[i] = p90SD[i, WD_Ind] / thisMet.turbulence.avgWS[i, WD_Ind];
                        else
                            effectiveTI[i] = 0;
                    }
                }
                else // get TI for overall WD
                {
                    int sumCount = 0;

                    if (thisWS >= powerCurve.cutInWS && thisWS <= powerCurve.cutOutWS)
                    {
                        for (int WD = 0; WD < thisInst.metList.numWD; WD++)
                        {
                            double sumWeightedWakeProb = 0;
                            double sumWakeProb = 0;

                            if (thisMet.turbulence.avgWS[i, WD] > 0)
                            {
                                for (int j = 0; j < thisInst.turbineList.TurbineCount; j++)
                                {
                                    if (thisInst.turbineList.turbineEsts[j].name != name)
                                    {
                                        double wakedSD = CalcWakedStDev(thisInst.turbineList.turbineEsts[j].UTMX, thisInst.turbineList.turbineEsts[j].UTMY, powerCurve,
                                            p90SD[i, WD], thisMet.turbulence.avgWS[i, WD], thisInst);
                                        sumWeightedWakeProb = sumWeightedWakeProb + probWake[j, WD] * Math.Pow(wakedSD, wohler);
                                        sumWakeProb = sumWakeProb + probWake[j, WD];
                                    }
                                }

                                if (thisMet.turbulence.count[i, WD] > 2)
                                {
                                    sumCount = sumCount + thisMet.turbulence.count[i, WD];
                                    double sectorEffTI = Math.Pow((1 - sumWakeProb) * Math.Pow(p90SD[i, WD], wohler) + sumWeightedWakeProb, (1 / wohler));

                                    if (thisMet.turbulence.avgWS[i, WD] > 0)
                                        sectorEffTI = sectorEffTI / thisMet.turbulence.avgWS[i, WD];

                                    effectiveTI[i] = effectiveTI[i] + sectorEffTI * thisMet.turbulence.count[i, WD];
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int WD = 0; WD < thisInst.metList.numWD; WD++)
                        {
                            if (thisMet.turbulence.avgWS[i, WD] > 0 && thisMet.turbulence.p90SD[i, WD] > 0)
                            {
                                effectiveTI[i] = effectiveTI[i] + p90SD[i, WD] / thisMet.turbulence.avgWS[i, WD] * thisMet.turbulence.count[i, WD];
                                sumCount = sumCount + thisMet.turbulence.count[i, WD];
                            }
                        }
                    }

                    if (sumCount > 0)
                        effectiveTI[i] = effectiveTI[i] / sumCount;
                }
            }

            return effectiveTI;
        }

        /// <summary>  Checks to see if time series estimates have already been generated ("WS", "Gross", "Net"). Returns true if they have been. </summary> 
        public bool HaveTS_Estimate(string estType, Wake_Model wakeModel, TurbineCollection.PowerCurve powerCurve)
        {
            bool haveEst = false;

            if (estType == "WS")
            {
                if (AvgWSEst_Count > 0)
                    return haveEst = true;
            }
            else if (estType == "Gross")
            {
                for (int i = 0; i < AvgWSEst_Count; i++)
                    if (avgWS_Est[i].powerCurve.name == powerCurve.name)
                        return haveEst = true;
            }
            else if (estType == "Net")
            {
                WakeCollection wakeList = new WakeCollection();

                for (int i = 0; i < AvgWSEst_Count; i++)
                    if (wakeList.IsSameWakeModel(avgWS_Est[i].wakeModel, wakeModel))
                        return haveEst = true;
            }

            return haveEst;
        }

        /// <summary> Updates AvgWS_Est object with calculated time series </summary>        
        public void UpdateAvgWS_EstWithTS(Avg_Est avgEstToUpdate)
        {
            WakeCollection wakeList = new WakeCollection();

            for (int i = 0; i < AvgWSEst_Count; i++)
            {
                if (wakeList.IsSameWakeModel(avgEstToUpdate.wakeModel, avgWS_Est[i].wakeModel))
                {
                    avgWS_Est[i] = avgEstToUpdate;
                    break;
                }
            }
        }

        /// <summary> Saves Turbine Time Series data to local database </summary>
        public void SaveTimeSeriesToDB(Continuum thisInst)
        {
            
            NodeCollection nodeList = new NodeCollection();
            WakeCollection wakeCollection = new WakeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            // Save all time series data 
            for (int i = 0; i < AvgWSEst_Count; i++)
            {
                if (avgWS_Est[i].timeSeries == null)
                    continue;

                string thisWakeModel = "None";

                if (avgWS_Est[i].wakeModel != null)
                    if (avgWS_Est[i].wakeModel.powerCurve.name != null)
                        thisWakeModel = wakeCollection.CreateWakeModelString(avgWS_Est[i].wakeModel);

                string thisPowerCurve = "None";

                if (avgWS_Est[i].powerCurve.name != "")
                    thisPowerCurve = avgWS_Est[i].powerCurve.name;

                // Check to see if it's already in database
                using (var context = new Continuum_EDMContainer(connString))
                {
                    var turbData = from N in context.Turb_TS_table where N.turbName == name && N.wakeModel == thisWakeModel && N.powerCurve == thisPowerCurve select N;

                    if (turbData.Count() > 0)
                        foreach (var turbEst in turbData)
                            context.Turb_TS_table.Remove(turbEst);

                    Turbine_TS_Ests_table turbEstTable = new Turbine_TS_Ests_table();
                    turbEstTable.turbName = name;
                    turbEstTable.wakeModel = thisWakeModel;
                    turbEstTable.powerCurve = thisPowerCurve;

                    MemoryStream MS1 = new MemoryStream();
                    bin.Serialize(MS1, avgWS_Est[i].timeSeries);
                    turbEstTable.tsData = MS1.ToArray();

                    try
                    {
                        context.Turb_TS_table.Add(turbEstTable);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                        return;
                    }

                }

            }
        }

        /// <summary> Gets estimated time series data from database. </summary>        
        public bool GetTimeSeriesDataFomDB(Continuum thisInst)
        {
            NodeCollection nodeList = new NodeCollection();
            WakeCollection wakeCollection = new WakeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);
            bool gotSome = false;

            for (int i = 0; i < AvgWSEst_Count; i++)
            {
                string thisWakeModel = "None";

                if (avgWS_Est[i].wakeModel != null)
                    if (avgWS_Est[i].wakeModel.powerCurve.name != null)
                        thisWakeModel = wakeCollection.CreateWakeModelString(avgWS_Est[i].wakeModel);

                string thisPowerCurve = "None";

                if (avgWS_Est[i].powerCurve.name != "")
                    thisPowerCurve = avgWS_Est[i].powerCurve.name;

                using (var context = new Continuum_EDMContainer(connString))
                {
                    var turbData = from N in context.Turb_TS_table where N.turbName == name && N.wakeModel == thisWakeModel && N.powerCurve == thisPowerCurve select N;

                    foreach (var N in turbData)
                    {
                        MemoryStream MS = new MemoryStream(N.tsData);
                        avgWS_Est[i].timeSeries = (ModelCollection.TimeSeries[])bin.Deserialize(MS);
                        MS.Close();
                        gotSome = true;
                    }
                }
            }

            return gotSome;
        }
    }
}
