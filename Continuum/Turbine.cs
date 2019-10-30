using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;

namespace ContinuumNS
{
    [Serializable()]
    public class Turbine
    {
        public string name; // name of turbine
        public double UTMX; // UTM X coordinate
        public double UTMY; // UTM Y coordinate
        public double elev; // Elevation
        public int stringNum; // Turbine string number (either imported or found in turbineList.AssignStringNumber)
        public Exposure[] expo; // Exposure and SRDH at turbine site
        public Grid_Info gridStats = new Grid_Info(); // Terrain complexity
        public WS_Ests[] WS_Estimate; // Wind speed estimates: One for each predictor met and each UWDW model (4 radii)
        public Avg_Est[] avgWS_Est; // Combination of WS_Estimate() to form overall average and sectorwise wind speed estimates
        public Gross_Energy_Est[] grossAEP; // Gross Energy Estimates: One for each power curve entered and for default and site-calibrated model
        public Net_Energy_Est[] netAEP; // Net Energy Estimates: One for each power curve entered and for default and site-calibrated model DOES NOT INCLUDE OTHER LOSSES
                                            // Other losses are applied to Waked Turb List, Net Exports
        public NodeCollection.Sep_Nodes[] flowSepNodes; // If flow separation model enabled, nodes where flow separation will occur surrounding turbine
        
        [Serializable()] public struct Avg_Est
        {
            public EstWS_Data freeStream;
            public EstWS_Data waked;           
            public double uncert;           
            public bool isImported;            
            public bool haveGrossTS; // true if gross energy TS has been calculated
            public bool haveNetTS; // true if net energy TS has been calculated
            public TurbineCollection.PowerCurve powerCurve;
            public Wake_Model wakeModel;
            public MonthlyWS_Vals[] FS_MonthlyVals; // freestream average and distribution of WS by month 
            public MonthlyWS_Vals[] wakedMonthlyVals; // freestream average and distribution of WS by month 
            public double[] alpha; // directional average power law shear exponent, alpha
            public TurbIntensity TI; // ambient, representative, and effective TI
            public ModelCollection.TimeSeries[] timeSeries; // array of time series data (Timestamp, WS, Gross, Net). Clears on save. Is regenerated when needed.
        }

        [Serializable()] public struct TurbIntensity
        {
            public double[] TI; // directional turbulence intensity
            public double[] repTI; // representative turbulence intensity
            public double[] effectiveTI; // Effective TI
        }

        [Serializable()] public struct WS_Ests {
            public string predictorMetName;
            public Nodes[] pathOfNodes;
            public double[] WS_at_nodes;
            public double[,] sectorWS_at_nodes;   // i = Node num j = WD sector
            public double[] sectorWS;   // Sectorwise WS estimates at turbine
            public Model model;
            public double WS;
            public double WS_weight;
          //  public int radius;
         //   public bool elevDiffTooBig;
         //   public bool expoDiffTooBig;
        }

        /// <summary>
        /// Contains total and sectorwise gross energy estimate values, capacity factors, and monthly values for given power curve.
        /// </summary>
        [Serializable()] public struct Gross_Energy_Est {
            public TurbineCollection.PowerCurve powerCurve;
            public double AEP;   // Gross annual energy estimate
            public double[] sectorEnergy;   // Sectorwise gross annual energy estimate (sectorws dist * power curve * 8760 * wind_rose[i])
            public double P90;
            public double P99;
            public double CF;   // Gross annual capacity factor     
            public MonthlyEnergyVals[] monthlyVals; // Gross energy by month 
        }

        [Serializable()] public struct Net_Energy_Est {
            public Wake_Model wakeModel;
            public double AEP;   // Net annual energy estimate (including both wake and all other losses)
            public double[] sectorEnergy;   // Sectorwise Net annual energy estimate (including sectorwise wake and all other losses)
            public double P90;
            public double P99;
            public double CF;   // Net capacity factor
            public double wakeLoss;
            public double[] sectorWakeLoss;       
            public MonthlyEnergyVals[] monthlyVals; // Net energy by month 
        }

        [Serializable()] public struct MonthlyWS_Vals
        {
            public int month;
            public int year;
            public double avgWS;
            public Met.WSWD_Dist WS_Dist; // Gross wind speed / wind direction distribution            
        }

        [Serializable()] public struct MonthlyEnergyVals
        {
            public int month;
            public int year;
            public double energyProd; // Energy Production over month, MWh           

        }

        [Serializable()] public struct EstWS_Data
        {
            public double WS;     
            public double[] WS_Dist;            
            public MetCollection.Weibull_params weibullParams;
            public double[] sectorWS;   // Overall Sectorwise WS estimate at turbine
            public double[,] sectorWS_Dist;   // i = Sector num, j = WS interval    
            public double[] windRose;
        }

        public int ExposureCount {
            get {
                if (expo == null)
                    return 0;
                else
                    return expo.Length;
            }
        }

        public int WSEst_Count {

            get {

                if (WS_Estimate == null)
                    return 0;
                else
                    return WS_Estimate.Length;
            }
        }

        public int AvgWSEst_Count {

            get {
                if (avgWS_Est == null)
                    return 0;
                else
                    return avgWS_Est.Length;
            }
        }

        public int GrossAEP_Count {
            get {
                if (grossAEP == null)
                    return 0;
                else
                    return grossAEP.Length;
            }
        }

        public int NetAEP_Count {
            get {
                if (netAEP == null)
                    return 0;
                else
                    return netAEP.Length;
            }
        }

        public bool IsNewExposure(int radius, double exponent, int numSectors)
        {
            // Returns false if exposure has already been calculated
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

        public bool IsNewSRDH(int radius, double exponent, int numSectors)
        {
            //  Returns false if surface roughness & displacement height have already been calculated            
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

        public bool EstsExistForWakeModel(Wake_Model thisWakeModel, WakeCollection WakeList)
        {
            // Returns true if estimates have already been formed for This_wake_mod and UWDW model
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

        public void AddExposure(int radius, double exponent, int numSectors, int numWD)
        {
            // Adds exposure to list

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
            else {
                expo = new Exposure[1];
                expo[0] = new Exposure();
                expo[0].radius = radius;
                expo[0].exponent = exponent;
                expo[0].numSectors = numSectors;
                expo[0].expo = new double[numWD];
            }

        }

        public void AddWS_Estimate(WS_Ests newWS_Est)
        {
            // Add WS_Estimate to  list
            int newCount = WSEst_Count;
            int numWD = newWS_Est.model.downhill_A.Length;

            if (WSEst_Count > 0) {
                Array.Resize(ref WS_Estimate, newCount + 1);
                WS_Estimate[newCount] = newWS_Est;
                WS_Estimate[newCount].sectorWS = new double[numWD];
            }
            else {
                WS_Estimate = new WS_Ests[1];
                WS_Estimate[0] = newWS_Est;
            }

        }

        public void AddAvgWS_Estimate(Avg_Est newAvgEst)
        {
            //  Add Avg_Est to  list
            int newCount = AvgWSEst_Count;

            if (AvgWSEst_Count > 0)
            {
                Array.Resize(ref avgWS_Est, newCount + 1);
                avgWS_Est[newCount] = newAvgEst;
                avgWS_Est[newCount].freeStream = avgWS_Est[0].freeStream;                
            }
            else {
                avgWS_Est = new Avg_Est[1];
                avgWS_Est[0] = newAvgEst;
            }
        }

        public void AddGrossEstTimeSeries(Gross_Energy_Est thisGross)
        {
            int newCount = GrossAEP_Count;
            Array.Resize(ref grossAEP, newCount + 1);
            grossAEP[newCount] = thisGross;
        }

        public void AddNetEstTimeSeries(Net_Energy_Est thisNet)
        {
            int newCount = NetAEP_Count;
            Array.Resize(ref netAEP, newCount + 1);
            netAEP[newCount] = thisNet;
        }

        public void AddGrossAEP(Continuum thisInst, TurbineCollection.PowerCurve ThisPowerCurve, double P50_AEP, double P50_CF, double P90_AEP,
                                 double P99_AEP, double[] P50_Sect_AEP)
        {
            // Adds gross energy estimate to list of grossAEP()
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
                grossAEP[emptyIndex].powerCurve = ThisPowerCurve;
                grossAEP[emptyIndex].sectorEnergy = P50_Sect_AEP;
                grossAEP[emptyIndex].AEP = P50_AEP;
                grossAEP[emptyIndex].CF = P50_CF;
                grossAEP[emptyIndex].P99 = P99_AEP;
                grossAEP[emptyIndex].P90 = P90_AEP;          
            }
            else {
                int newCount = GrossAEP_Count;
                Array.Resize(ref grossAEP, newCount + 1);
                grossAEP[newCount].powerCurve = ThisPowerCurve;
                grossAEP[newCount].sectorEnergy = P50_Sect_AEP;
                grossAEP[newCount].AEP = P50_AEP;
                grossAEP[newCount].CF = P50_CF;
                grossAEP[newCount].P99 = P99_AEP;
                grossAEP[newCount].P90 = P90_AEP;          
            }

        }

        public void AddNetAEP(Wake_Model thisWakeModel, double P50_AEP, double P50_CF, double P90_AEP, double P99_AEP,
                           double wakeLoss, double[] P50_Sect_AEP, bool useSR, bool usesFlowSep)
        {
            // Adds net energy estimate to list of netAEP()

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
            else {
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

        public WS_Ests GetWS_Est(int radius, string predMet, Model This_Model)
        {
            // Returns wind speed estimate at specified radius, using specified met and model
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

        public Avg_Est GetAvgWS_Est(Wake_Model thisWakeModel, TurbineCollection.PowerCurve powerCurve)
        {
            // Returns wind speed estimate at specified radius, using specified met and model
            Avg_Est thisAvgWS_Est = new Avg_Est();
            ModelCollection modelList = new ModelCollection();
            WakeCollection wakeList = new WakeCollection();
            bool blankWake = false;

            if (thisWakeModel == null)
                blankWake = true;
            else if (thisWakeModel.powerCurve.name == null)
                blankWake = true;

            for (int i = 0; i < AvgWSEst_Count; i++)
            {
                if ((blankWake == true || wakeList.IsSameWakeModel(thisWakeModel, avgWS_Est[i].wakeModel)) 
                    && (powerCurve.name == null || powerCurve.name == avgWS_Est[i].powerCurve.name))
                {
                    thisAvgWS_Est = avgWS_Est[i];
                    break;
                }
            }

            return thisAvgWS_Est;
        }

        public Gross_Energy_Est GetGrossEnergyEst(TurbineCollection.PowerCurve powerCurve)
        {
            // Returns wind speed estimate at specified radius, using specified met and model
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

        public Net_Energy_Est GetNetEnergyEst(Wake_Model thisWakeModel)
        {
            // Returns wind speed estimate at specified radius, using specified met and model
            Net_Energy_Est thisNetEst = new Net_Energy_Est();
            ModelCollection modelList = new ModelCollection();
            WakeCollection wakeList = new WakeCollection();

            for (int i = 0; i < NetAEP_Count; i++)
            {
                if (wakeList.IsSameWakeModel(thisWakeModel, netAEP[i].wakeModel)) 
                {
                    thisNetEst = netAEP[i];
                    break;
                }
            }

            return thisNetEst;
        }

        public double GetAvgOrSectorWS_Est(Wake_Model thisWakeModel, int WD_Ind, string WS_WeibA_WeibK, TurbineCollection.PowerCurve powerCurve)
        {
            // Gets average WS or weibull A or weibull K estimate of specified model and for specified WD 

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


        public double GetGrossAEP(string powerCurve, int WD_Ind)
        {
            // Returns Gross AEP estimate for specified power curve, WD sector and model (default vs. site-calibrated)
            double thisAEP = 0;
            int numWD = 0;

            for (int i = 0; i < GrossAEP_Count; i++)
            {
                try {
                    numWD = grossAEP[i].sectorEnergy.Length;
                }
                catch {
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

        public double GetNetAEP(Wake_Model thisWakeModel, int WD_Ind)
        {
            // Returns Net AEP estimate for specified wake loss model, WD sector and Continuum model (default vs. site-calibrated)
            double thisAEP = 0;
            WakeCollection wakeList = new WakeCollection();
            int numWD = 0;

            for (int i = 0; i < NetAEP_Count; i++)
            {
                try
                {
                    numWD = netAEP[i].sectorEnergy.Length;
                }
                catch {
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

        public double GetNetCF(Wake_Model thisWakeModel, int WD_Ind)
        {
            //  Returns Net Capacity factor for specified wake loss model, WD sector and Continuum model (default vs. site-calibrated)
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
                        This_CF = turbineList.CalcCapacityFactor(netAEP[i].sectorEnergy[WD_Ind], thisWakeModel.powerCurve.ratedPower) * numWD;
                    }
                    break;
                }
            }

            return This_CF;
        }

        public double GetWakeLoss(Wake_Model thisWakeModel, int WD_Ind)
        {
            //  Returns wake loss for specified wake loss model, WD sector and Continuum model (default vs. site-calibrated)
            double thisWakeLoss = 0;
            WakeCollection wakeList = new WakeCollection();
            int numWD;

            for (int i = 0; i < NetAEP_Count; i++)
            {
                try {
                    numWD = netAEP[i].sectorEnergy.Length;
                }
                catch {
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

        public void GetFlowSepNodes(Continuum thisInst)
        {
            // Finds all flow separation nodes (used in flow sep. model)
            int numWD = thisInst.metList.numWD;
            NodeCollection nodeList = new NodeCollection();
                     
            Nodes thisNode = nodeList.GetTurbNode(this);
            flowSepNodes = nodeList.FindAllFlowSeps(thisNode, thisInst, numWD);

        }

        public void RemoveWS_Estimate(int WS_Est_ind)
        {
            // Removes WS_Estimate with specified WS_Est_ind
            int newCount = WSEst_Count - 1;

            if (newCount > 0)
            {
                WS_Ests[] tempList = new WS_Ests[newCount];
                int tempindex = 0;

                for (int i = 0; i < WSEst_Count; i++)
                {
                    if (i != WS_Est_ind)
                    {
                        tempList[tempindex] = WS_Estimate[i];
                        tempindex++;
                    }
                }

                WS_Estimate = tempList;
            }
            else
                WS_Estimate = null;

        }

        public void RemoveWS_EstimateByMet(string metName)
        {
            //  Removes WS_Estimate that use specified Met site
            if (WSEst_Count > 0)
            {
                WS_Ests[] tempList = new WS_Ests[1];
                int tempindex = 0;

                for (int i = 0; i < WSEst_Count; i++)
                {
                    if (WS_Estimate[i].predictorMetName != metName)
                    {
                        tempList[tempindex] = WS_Estimate[i];
                        tempindex++;
                    }
                }

                WS_Estimate = tempList;
            }
            else
                WS_Estimate = null;

        }

        public void RemoveAvgWS(int avgWS_index)
        {
            // Removes avgWS with specified avgWS_Ind
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

        public void ClearNetEstsFromAvgWS(Wake_Model thisWakeModel, WakeCollection wakeList) // ClearNetEstsFromAvgWS
        {
            //  Clears waked WS and net energy time series ests from avgWS with specified wake loss model and sets haveNetTS to false.

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

        public void ClearGrossEstsFromAvgWS(string powerCurveName)
        {
            //  Clears gross energy time series ests from avgWS with specified power curve and sets haveGrossTS to false.

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

        public void RemoveNetAEP_byPowerCurve(string powerCurveName)
        {
            //  Removes netAEP estimate(s) with specified power curve
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

        public void RemoveNetAEP_byWakeModel(Wake_Model thisWakeModel, WakeCollection wakeList)
        {
            //  Removes netAEP estimate(s) with specified wake loss model
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

        public void RemoveGrossAEP(int grossIndex)
        {
            //  Removes grossAEP with specified Gross_ind
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

        public void RemoveNetAEP(int netIndex)
        {
            //  Removes netAEP with specified Net_ind
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

        public void ClearAllCalcs()
        {
            // Resets all calculated values (including elev, exposure, grid stats, WS_Ests, AEP_ests
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

            AddNetAEP(thisWakeModel, 0, 0, 0, 0, 0, null, thisInst.topo.useSR, thisInst.topo.useSepMod);
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

        /// <summary>
        ///  For each predictor met and each model (i.e. 4 Radii), finds a path of nodes to turbine site, 
        ///  does wind speed estimate along nodes using specified model and creates WS_Ests() for each met and each model 
        /// </summary>        
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
                        if (WS_Estimate[n].model != null) {
                            if (thisInst.modelList.IsSameModel(WS_Estimate[n].model, models[m]) && thisMet.name == WS_Estimate[n].predictorMetName) {
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

        public Nodes[] GetPathFromList(NodeCollection.Node_UTMs[] pathToMetUTM, Nodes[] allNodesInPath, Continuum thisInst)
        {
            // Returns pathToMet()  Nodes based on Path_to_Met_UTM()  NodeCollection.Node_UTMs
            Nodes[] pathToMet = null;
            int numNodes;
            int numAllNodes;
            NodeCollection nodeList = new NodeCollection();

            if (allNodesInPath == null)
                numAllNodes = 0;
            else
                numAllNodes = allNodesInPath.Length;

            if (pathToMetUTM == null)
                numNodes = 0;
            else
                numNodes = pathToMetUTM.Length;

            if (numNodes > 0 && numAllNodes > 0)
            {
                pathToMet = new Nodes[numNodes];
                double thisUTMX;
                double thisUTMY;

                for (int nodeIndex = 0; nodeIndex <= numNodes - 1; nodeIndex++)
                {
                    thisUTMX = pathToMetUTM[nodeIndex].UTMX;
                    thisUTMY = pathToMetUTM[nodeIndex].UTMY;

                    for (int allNodeIndex = 0; allNodeIndex <= numAllNodes - 1; allNodeIndex++)
                    {
                        if (thisUTMX == allNodesInPath[allNodeIndex].UTMX && thisUTMY == allNodesInPath[allNodeIndex].UTMY) {
                            pathToMet[nodeIndex] = allNodesInPath[allNodeIndex];
                            break;
                        }
                    }

                    Nodes[] blank = null;
                    if (pathToMet[nodeIndex] == null) {
                        pathToMet[nodeIndex] = nodeList.GetANode(thisUTMX, thisUTMY, thisInst);
                        if (thisInst.topo.useSepMod == true) pathToMet[nodeIndex].GetFlowSepNodes(nodeList, thisInst, null, thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, Met.TOD.All, Met.Season.All));
                    }

                }
            }

            if (numNodes == 0)
                pathToMet = null;

            return pathToMet;
        }

        /// <summary>
        /// Performs and calculates wind speed estimate along path of nodes from Met to turbine using specified model and predictor met
        /// </summary>        
        public WS_Ests DoWS_EstAlongNodes(Met predMet, Model model, Nodes[] pathOfNodes, Continuum thisInst, double[] windRose)
        {            
            //  Returns WS_Ests
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

            if (numNodes > 0) {
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

        /// <summary>
        /// Creates and adds new Avg_Est based on time series data
        /// If forRoundRobin is true then doesn't assign uncertainty
        /// </summary>        
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

        public double GetDefaultUncertainty(double[] windRose)
        {
            // Calculates and returns default estimated uncertainty. Based on results of single met study using Continuum 1. Needs to be revisited.
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

                

        public double GetWeightForMetAndModel(ModelCollection.ModelWeights[] weights, Met thisMet, Model thisModel)
        {
            double thisWeight = 0;

            if (weights == null)
                return thisWeight;

            int numWeights = weights.Length;
            ModelCollection modelList = new ModelCollection();

            for (int i = 0; i < numWeights; i++)
                if (weights[i].met.name == thisMet.name && modelList.IsSameModel(weights[i].model, thisModel))
                {
                    thisWeight = weights[i].weight;
                    break;
                }

            return thisWeight;
        }

        /// <summary>
        /// Calculate and returns mean monthly wind speed estimates using time series of either waked or freestream wind speed estimates
        /// </summary>        
        public MonthlyWS_Vals[] CalcMonthlyWS_Values(ModelCollection.TimeSeries[] thisTS, Continuum thisInst, string wakedOrFreestream)
        {            
            MonthlyWS_Vals[] monthlyWS_Vals = new MonthlyWS_Vals[0];

            if (thisTS == null) return monthlyWS_Vals;
                        
            int TS_Length = thisTS.Length;
            if (TS_Length <= 1) return monthlyWS_Vals;

            // Figure out time interval (in hours)
            TimeSpan timeSpan = thisTS[1].dateTime - thisTS[0].dateTime;
            int timeInt = timeSpan.Hours;

            // Start on first hour on first of month
            int TS_Ind = 0;
            int thisDay = thisTS[TS_Ind].dateTime.Day;
            int thisHour = thisTS[TS_Ind].dateTime.Hour;
            MERRA merra = new MERRA(); // Using some functions from this class (i.e. Get_Num_Days_in_Month)

            while (thisDay != 1 && thisHour != 0)
            {
                TS_Ind++;
                thisDay = thisTS[TS_Ind].dateTime.Day;
                thisHour = thisTS[TS_Ind].dateTime.Hour;
            }

            int lastMonth = thisTS[TS_Ind].dateTime.Month;
            int lastYear = thisTS[TS_Ind].dateTime.Year;

            double avgWS = 0;
            int WS_count = 0;
            int numDaysInMonth = merra.Get_Num_Days_in_Month(lastMonth, lastYear);
            int numMonthlyInts = numDaysInMonth * timeInt * 24;
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
                    
                    avgWS = avgWS + thisWS;
                    WS_count++;
                    monthlyTS[Ind].dateTime = thisTS[t].dateTime;

                    if (wakedOrFreestream == "Waked")
                        monthlyTS[Ind].wakedWS = thisWS;
                    else
                        monthlyTS[Ind].freeStreamWS = thisWS;

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
                    numMonthlyInts = numDaysInMonth * timeInt * 24;
                    Array.Clear(monthlyTS, 0, monthlyTS.Length);
                    Array.Resize(ref monthlyTS, numMonthlyInts);
                    Ind = 0;
                    avgWS = 0;
                    WS_count = 0;
                    monthlyTS[Ind].dateTime = thisTS[t].dateTime;

                    if (wakedOrFreestream == "Waked")
                        monthlyTS[Ind].wakedWS = thisTS[t].wakedWS;
                    else
                        monthlyTS[Ind].freeStreamWS = thisTS[t].freeStreamWS;

                    monthlyTS[Ind].WD = thisTS[t].WD;
                    lastMonth = month;
                    lastYear = year;
                    Ind++;
                }

            }

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
            
            return monthlyWS_Vals;

        }
           

        public void CalcGrossAEPFromTimeSeries(Continuum thisInst, ModelCollection.TimeSeries[] thisTS, TurbineCollection.PowerCurve powerCurve)
        {
            // Calculates and adds gross energy estimate based on energy production time series
            if (thisInst.metList.numWD == 0) return;
            int numWD = thisInst.metList.numWD;
            int numWS = thisInst.metList.numWS;                                          
                        
            bool alreadyCalc = false;
                
            double[] P50_AEP_Sect = new double[numWD];
            // Check to see if energy calc already done
            alreadyCalc = false;
            for (int k = 0; k < GrossAEP_Count; k++)
            {
                if (grossAEP[k].powerCurve.name == powerCurve.name) 
                {
                    alreadyCalc = true;
                    break;
                }
            }

            Avg_Est avgEst = GetAvgWS_Est(new Wake_Model(), powerCurve);
            EstWS_Data estData = avgEst.freeStream;

            if (alreadyCalc == false)
            {

                Gross_Energy_Est thisGross = new Gross_Energy_Est();
                thisGross.powerCurve = powerCurve;          
                thisInst.modelList.CalcGrossAEP_AndMonthlyEnergy(ref thisGross, thisTS, thisInst); // calculates long-term AEP, sectorwise energy, and monthly values

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

        public void CalcNetAEPFromTimeSeries(Continuum thisInst, ModelCollection.TimeSeries[] thisTS, TurbineCollection.PowerCurve powerCurve, Wake_Model wakeModel)
        {
            // Calculates and adds net energy estimate based on energy production time series
            if (thisInst.metList.numWD == 0) return;
            int numWD = thisInst.metList.numWD;
            
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
                thisNet.sectorWakeLoss = thisInst.wakeModelList.CalcThisSectWakeLoss(wakeModel, thisNet.sectorEnergy, thisGross.sectorEnergy, otherLoss);
                thisNet.wakeLoss = thisInst.wakeModelList.CalcThisWakeLoss(wakeModel, thisNet.AEP, thisGross.AEP, otherLoss);

                AddNetEstTimeSeries(thisNet);
            }
        }

        /// <summary>
        /// Calculates and returns annual value (wind speed, net AEP, wake loss)
        /// </summary>        
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
                    Avg_Est thisAvgEst = GetAvgWS_Est(thisWakeModel, powerCurve);
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

        /// <summary>
        /// Calculates and returns long-term monthly value (Avg WS, Gross AEP, Net AEP, or Wake Loss)
        /// </summary>        
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
                Avg_Est thisAvgEst = GetAvgWS_Est(thisWakeModel, powerCurve);
                MonthlyWS_Vals[] thisMonthly = null;

                if (isWaked)
                    thisMonthly = thisAvgEst.wakedMonthlyVals;
                else
                    thisMonthly = thisAvgEst.FS_MonthlyVals;

                if (thisMonthly == null)
                    return thisLT_Value;
                else
                {
                    for(int i = 0; i < thisMonthly.Length; i++)
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

        /// <summary>
        /// Calculates and returns the waked wind speed standard deviation used in Effective TI calculations
        /// </summary>        
        public double CalcWakedStDev(double thisX, double thisY, TurbineCollection.PowerCurve powerCurve, double ambSD, double avgWS, Continuum thisInst)
        {             
            double wakedSD = 0;
            double cT = thisInst.turbineList.GetInterpPowerOrThrust(avgWS, powerCurve, "Thrust");
            double dist = thisInst.topo.CalcDistanceBetweenPoints(thisX, thisY, UTMX, UTMY) / powerCurve.RD;                       

            if (cT != 0)
                wakedSD = Math.Pow((Math.Pow(avgWS ,2)/ (Math.Pow(1.5 + 0.8 * dist / Math.Pow(cT, 0.5), 2)) + Math.Pow(ambSD, 2)), 0.5);
            
            return wakedSD;
        }

        /// <summary>
        /// Calculates and returns effective TI at turbine site for a specified wind direction sector using ambient TI measured at met site
        /// </summary>        
        public double[] CalcEffectiveTI(Met thisMet, double wohler, Continuum thisInst, TurbineCollection.PowerCurve powerCurve, int WD_Ind)
        {            
            double[] effectiveTI = new double[thisInst.metList.numWS];
            double[,] probWake = thisInst.turbineList.CalcProbOfWakeForEffectiveTI(thisInst, UTMX, UTMY, powerCurve);
                      
            for (int i = 0; i < thisInst.metList.numWS; i++)
            {
                if (WD_Ind != thisInst.metList.numWD) // get TI for specfic WD
                {
                    if (i >= powerCurve.cutInWS && i <= powerCurve.cutOutWS)
                    {
                        double sumWeightedWakeProb = 0;
                        double sumWakeProb = 0;

                        // Sum up probability of wake and waked induced TI
                        for (int j = 0; j < thisInst.turbineList.TurbineCount; j++)
                        {
                            if (thisInst.turbineList.turbineEsts[j].name != name)
                            {
                                double wakedSD = CalcWakedStDev(thisInst.turbineList.turbineEsts[j].UTMX, thisInst.turbineList.turbineEsts[j].UTMY, powerCurve,
                                    thisMet.turbulence.p90SD[i, WD_Ind], thisMet.turbulence.avgWS[i, WD_Ind], thisInst);
                                sumWeightedWakeProb = sumWeightedWakeProb + probWake[j, WD_Ind] * Math.Pow(wakedSD, wohler);
                                sumWakeProb = sumWakeProb + probWake[j, WD_Ind];
                            }
                        }

                        effectiveTI[i] = Math.Pow((1 - sumWakeProb) * Math.Pow(thisMet.turbulence.p90SD[i, WD_Ind], wohler) + sumWeightedWakeProb, (1 / wohler));

                        if (thisMet.turbulence.avgWS[i, WD_Ind] > 0)
                            effectiveTI[i] = effectiveTI[i] / thisMet.turbulence.avgWS[i, WD_Ind];
                        else
                            effectiveTI[i] = 0;
                    }
                    else
                    {
                        if (thisMet.turbulence.avgWS[i, WD_Ind] > 0)
                            effectiveTI[i] = thisMet.turbulence.p90SD[i, WD_Ind] / thisMet.turbulence.avgWS[i, WD_Ind];
                        else
                            effectiveTI[i] = 0;
                    }
                }
                else // get TI for overall WD
                {                    
                    int sumCount = 0;

                    if (i > powerCurve.cutInWS && i < powerCurve.cutOutWS)
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
                                            thisMet.turbulence.p90SD[i, WD], thisMet.turbulence.avgWS[i, WD], thisInst);
                                        sumWeightedWakeProb = sumWeightedWakeProb + probWake[j, WD] * Math.Pow(wakedSD, wohler);
                                        sumWakeProb = sumWakeProb + probWake[j, WD];
                                    }
                                }

                                if (thisMet.turbulence.count[i, WD] > 2)
                                {
                                    sumCount = sumCount + thisMet.turbulence.count[i, WD];
                                    double sectorEffTI = Math.Pow((1 - sumWakeProb) * Math.Pow(thisMet.turbulence.p90SD[i, WD], wohler) + sumWeightedWakeProb, (1 / wohler));

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
                                effectiveTI[i] = effectiveTI[i] + thisMet.turbulence.p90SD[i, WD] / thisMet.turbulence.avgWS[i, WD] * thisMet.turbulence.count[i, WD];
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

        public bool HaveTS_Estimate(string estType, Wake_Model wakeModel, TurbineCollection.PowerCurve powerCurve)
        {
            // Checks to see if time series estimates have already been generated. Returns true if they have been.
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
    }
}
