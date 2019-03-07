using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public double[] windRose; // Interpolated wind rose based on distance from met sites
        public WS_Ests[] WS_Estimate; // Wind speed estimates: One for each predictor met and each UWDW model (4 radii)
        public Avg_Est[] avgWS_Est; // Combination of WS_Estimate() to form overall average and sectorwise wind speed estimates
        public Gross_Energy_Est[] grossAEP; // Gross Energy Estimates: One for each power curve entered and for default and site-calibrated model
        public Net_Energy_Est[] netAEP; // Net Energy Estimates: One for each power curve entered and for default and site-calibrated model
        public NodeCollection.Sep_Nodes[] flowSepNodes; // If flow separation model enabled, nodes where flow separation will occur surrounding turbine

        [Serializable()] public struct Avg_Est
        {
            public double WS;
            
            public double uncert;
            public double[] WS_Dist;
            public double weibull_A;
            public double weibull_k;
            public double[] sectorWS;   // Overall Sectorwise WS estimate at turbine
            public double[,] sectorWS_Dist;   // i = Sector num, j = WS interval
            public double[] sectWeibull_A;   // Sector num
            public double[] sectWeibull_k;   // Sector num
            public bool isWaked;   // false = Free-stream, gross; true = Waked, net
            public Wake_Model wakeModel;            
            public bool usesSRDH;
            public bool usesFlowSep;
            public bool isImported;
            public bool isCalibrated; // true if default model (i.e. not site-calibrated model) was used to create estimate

        }

        [Serializable()] public struct WS_Ests {
            public string predictorMetName;
            public NodeCollection.Node_UTMs[] pathOfNodesUTMs;
            public double[] WS_at_nodes;
            public double[,] sectorWS_at_nodes;   // i = Node num j = WD sector
            public double[] sectorWS;   // Sectorwise WS estimates at turbine
            public Model model;
            public double WS;
            public double WS_weight;
            public int radius;
            public bool elevDiffTooBig;
            public bool expoDiffTooBig;
        }

        [Serializable()] public struct Gross_Energy_Est {
            public TurbineCollection.PowerCurve powerCurve;
            public double AEP;   // Gross annual energy estimate
            public double[] sectorEnergy;   // Sectorwise gross annual energy estimate (sectorws dist * power curve * 8760 * wind_rose[i])
            public double P90;
            public double P99;
            public double CF;   // Gross annual capacity factor
            public bool isCalibrated; // true if default model (i.e. not site-calibrated model) was used to create estimate
            public bool useSRDH;
            public bool usesFlowSep;
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
            public bool isCalibrated; // true if default model (i.e. not site-calibrated model) was used to create estimate
            public bool useSRDH;
            public bool usesFlowSep;
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

        public bool EstsExistForWakeModel(Wake_Model thisWakeModel, bool isCalibrated, WakeCollection WakeList, bool useSR, bool useFlowSep)
        {
            // Returns true if estimates have already been formed for This_wake_mod and UWDW model
            bool estsExist = false;

            for (int i = 0; i <= AvgWSEst_Count - 1; i++)
            {
                if (avgWS_Est[i].wakeModel != null)
                {
                    if (avgWS_Est[i].isWaked && avgWS_Est[i].isCalibrated == isCalibrated && avgWS_Est[i].usesSRDH == useSR && avgWS_Est[i].usesFlowSep == useFlowSep
                        && WakeList.IsSameWakeModel(avgWS_Est[i].wakeModel, thisWakeModel))
                    {
                        estsExist = true;
                        break;
                    }
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

            if (WSEst_Count > 0)
            {
                Array.Resize(ref avgWS_Est, newCount + 1);
                avgWS_Est[newCount] = newAvgEst;
            }
            else {
                avgWS_Est = new Avg_Est[1];
                avgWS_Est[0] = newAvgEst;
            }
        }

        public void AddGrossAEP(Continuum thisInst, TurbineCollection.PowerCurve ThisPowerCurve, double P50_AEP, double P50_CF, double P90_AEP,
                                 double P99_AEP, bool isCalibrated, double[] P50_Sect_AEP)
        {
            // Adds gross energy estimate to list of grossAEP()
            // See if there is an empty Gross AEP
            bool haveEmpty = false;
            int emptyIndex = 0;

            for (int i = 0; i <= GrossAEP_Count - 1; i++)
            {
                if (grossAEP[i].isCalibrated == isCalibrated && grossAEP[i].AEP == 0)
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
                grossAEP[emptyIndex].isCalibrated = isCalibrated;
                grossAEP[emptyIndex].useSRDH = thisInst.topo.useSR;
                grossAEP[emptyIndex].usesFlowSep = thisInst.topo.useSepMod;
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
                grossAEP[newCount].isCalibrated = isCalibrated;
                grossAEP[newCount].useSRDH = thisInst.topo.useSR;
                grossAEP[newCount].usesFlowSep = thisInst.topo.useSepMod;
            }

        }

        public void AddNetAEP(Wake_Model thisWakeModel, double P50_AEP, double P50_CF, double P90_AEP, double P99_AEP,
                           bool isCalibrated, double wakeLoss, double[] P50_Sect_AEP, bool useSR, bool usesFlowSep)
        {
            // Adds net energy estimate to list of netAEP()

            // See if there is an empty Net AEP
            bool haveEmpty = false;
            int Empty_Ind = 0;

            for (int i = 0; i <= NetAEP_Count - 1; i++)
            {
                if (netAEP[i].isCalibrated == isCalibrated && netAEP[i].AEP == 0)
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
                netAEP[Empty_Ind].isCalibrated = isCalibrated;
                netAEP[Empty_Ind].useSRDH = useSR;
                netAEP[Empty_Ind].usesFlowSep = usesFlowSep;
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
                netAEP[newCount].isCalibrated = isCalibrated;
                netAEP[newCount].useSRDH = useSR;
                netAEP[newCount].usesFlowSep = usesFlowSep;
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
                if (WS_Estimate[i].predictorMetName == predMet && WS_Estimate[i].radius == radius && Model_List.IsSameModel(WS_Estimate[i].model, This_Model))
                {
                    thisWS_Est = WS_Estimate[i];
                    break;
                }
            }

            return thisWS_Est;
        }

        public Avg_Est GetAvgWS_Est(bool isCalibrated, Wake_Model thisWakeModel)
        {
            // Returns wind speed estimate at specified radius, using specified met and model
            Avg_Est thisAvgWS_Est = new Avg_Est();
            ModelCollection modelList = new ModelCollection();
            WakeCollection wakeList = new WakeCollection();

            for (int i = 0; i < AvgWSEst_Count; i++)
            {
                if (avgWS_Est[i].isCalibrated == isCalibrated && wakeList.IsSameWakeModel(thisWakeModel, avgWS_Est[i].wakeModel))
                {
                    thisAvgWS_Est = avgWS_Est[i];
                    break;
                }
            }

            return thisAvgWS_Est;
        }

        public Gross_Energy_Est GetGrossEnergyEst(bool isCalibrated)
        {
            // Returns wind speed estimate at specified radius, using specified met and model
            Gross_Energy_Est thisGrossEst = new Gross_Energy_Est();
                        
            for (int i = 0; i < GrossAEP_Count; i++)
            {
                if (grossAEP[i].isCalibrated == isCalibrated)
                {
                    thisGrossEst = grossAEP[i];
                    break;
                }
            }

            return thisGrossEst;
        }

        public Net_Energy_Est GetNetEnergyEst(bool isCalibrated, Wake_Model thisWakeModel)
        {
            // Returns wind speed estimate at specified radius, using specified met and model
            Net_Energy_Est thisNetEst = new Net_Energy_Est();
            ModelCollection modelList = new ModelCollection();
            WakeCollection wakeList = new WakeCollection();

            for (int i = 0; i < NetAEP_Count; i++)
            {
                if (netAEP[i].isCalibrated == isCalibrated && wakeList.IsSameWakeModel(thisWakeModel, netAEP[i].wakeModel))
                {
                    thisNetEst = netAEP[i];
                    break;
                }
            }

            return thisNetEst;
        }

        public double GetAvgOrSectorWS_Est(bool isCalibrated, Wake_Model thisWakeModel, int WD_Ind, string WS_WeibA_WeibK)
        {
            // Gets average WS or weibull A or weibull K estimate of specified model (default or site-calibratied) and for specified WD 
            
            double thisValue = 0;
            WakeCollection wakeList = new WakeCollection();

            if (WSEst_Count == 0)
                return thisValue;

            int numWD = WS_Estimate[0].sectorWS.Length;

            for (int i = 0; i < AvgWSEst_Count; i++)
            {
                if ((avgWS_Est[i].isCalibrated == isCalibrated && wakeList.IsSameWakeModel(thisWakeModel, avgWS_Est[i].wakeModel)) || AvgWSEst_Count == 1)
                {
                    if (WD_Ind == numWD)
                    {
                        if (WS_WeibA_WeibK == "WS")
                            thisValue = avgWS_Est[i].WS;
                        else if (WS_WeibA_WeibK == "WeibA")
                            thisValue = avgWS_Est[i].weibull_A;
                        else if (WS_WeibA_WeibK == "WeibK")
                            thisValue = avgWS_Est[i].weibull_k;
                        else
                        {
                            MessageBox.Show("Invalid WS_WeibA_WeibK flag: " + WS_WeibA_WeibK);
                            return thisValue;
                        }
                    }
                    else
                    {
                        if (WS_WeibA_WeibK == "WS")
                            thisValue = avgWS_Est[i].sectorWS[WD_Ind];
                        else if (WS_WeibA_WeibK == "WeibA")
                            thisValue = avgWS_Est[i].sectWeibull_A[WD_Ind];
                        else if (WS_WeibA_WeibK == "WeibK")
                            thisValue = avgWS_Est[i].sectWeibull_k[WD_Ind];
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
                

        public double GetGrossAEP(string powerCurve, bool isCalibrated, int WD_Ind)
        {
            // Returns Gross AEP estimate for specified power curve, WD sector and model (default vs. site-calibrated)
            double thisAEP = 0;
            int numWD = 0;

            for (int i = 0; i < GrossAEP_Count; i++)
            {
                try {
                    numWD = grossAEP[i].sectorEnergy.Length;
                }
                catch  {
                    return thisAEP;
                }

                if (WD_Ind == numWD)
                { // overall AEP
                    if (grossAEP[i].powerCurve.name == powerCurve && grossAEP[i].isCalibrated == isCalibrated)
                    {
                        thisAEP = grossAEP[i].AEP;
                        break;
                    }
                }
                else
                { // sectorwise
                    if (grossAEP[i].powerCurve.name == powerCurve && grossAEP[i].isCalibrated == isCalibrated)
                    {
                        thisAEP = grossAEP[i].sectorEnergy[WD_Ind];
                        break;
                    }
                }
            }

            return thisAEP;
        }

        public double GetNetAEP(Wake_Model thisWakeModel, bool isCalibrated, int WD_Ind)
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

                if (netAEP[i].isCalibrated == isCalibrated && wakeList.IsSameWakeModel(thisWakeModel, netAEP[i].wakeModel))
                {
                    if (WD_Ind == numWD)
                     // overall AEP
                        thisAEP = netAEP[i].AEP; 
                    else                    
                        thisAEP = netAEP[i].sectorEnergy[WD_Ind];                        

                    break;
                }                
            }

            return thisAEP;
        }

        public double GetNetCF(Wake_Model thisWakeModel, bool isCalibrated, int WD_Ind)
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

                if (wakeList.IsSameWakeModel(netAEP[i].wakeModel, thisWakeModel) && netAEP[i].isCalibrated == isCalibrated)
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

        public double GetWakeLoss(Wake_Model thisWakeModel, bool isCalibrated, int WD_Ind)
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
                catch  {
                    return thisWakeLoss;
                }

                if (wakeList.IsSameWakeModel(netAEP[i].wakeModel, thisWakeModel) && netAEP[i].isCalibrated == isCalibrated)
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
            int numWD;
            NodeCollection nodeList = new NodeCollection();

            try {
                numWD = windRose.Length;
            }
            catch  { 
                return;
            }

            Nodes thisNode = nodeList.GetTurbNode(this);
            Nodes[] blankNodes = null;
            flowSepNodes = nodeList.FindAllFlowSeps(thisNode, thisInst, numWD, ref blankNodes);

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

        public void RemoveAvgWS_byWakeModel(Wake_Model thisWakeModel, WakeCollection wakeList)
        {
            //  Removes avgWS with specified wake loss model
            int newCount = 0;

            for (int i = 0; i <= AvgWSEst_Count - 1; i++)
                if (avgWS_Est[i].isWaked == false || (avgWS_Est[i].isWaked == true && wakeList.IsSameWakeModel(thisWakeModel, avgWS_Est[i].wakeModel) == false))
                    newCount++;

            if (newCount > 0)
            {
                Avg_Est[] tempList = new Avg_Est[newCount];
                int tempindex = 0;

                for (int i = 0; i <= AvgWSEst_Count - 1; i++)
                {
                    if (avgWS_Est[i].isWaked == false || (avgWS_Est[i].isWaked == true && wakeList.IsSameWakeModel(thisWakeModel, avgWS_Est[i].wakeModel) == false))
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

        public void RemoveAvgWS_byPowerCurve(string powerCurveName)
        {
            //  Removes avgWS with specified power curve
            int newCount = 0;

            for (int i = 0; i <= AvgWSEst_Count - 1; i++)
            {
                if (avgWS_Est[i].isWaked == true)
                {
                    if (avgWS_Est[i].wakeModel.powerCurve.name != powerCurveName)
                        newCount++;
                }
                else
                    newCount++;
                    
            }

            if (newCount > 0)
            {
                Avg_Est[] tempList = new Avg_Est[newCount];
                int tempindex = 0;

                for (int i = 0; i <= AvgWSEst_Count - 1; i++)
                {
                    if (avgWS_Est[i].isWaked == true)
                    {
                        if (avgWS_Est[i].wakeModel.powerCurve.name != powerCurveName)
                        {
                            tempList[tempindex] = avgWS_Est[i];
                            tempindex++;
                        }
                    }
                    else
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

        public void CalcTurbineWakeLosses(Continuum thisInst, WakeCollection.WakeLossCoeffs[] wakeCoeffs, Wake_Model thisWakeModel, bool isCalibrated)
        {
            // Calculates wake losses at turbine site and creates net energy estimates
            WakeCollection.WakeCalcResults wakeResults = new WakeCollection.WakeCalcResults();

            int avgWS_index = 0;            
            bool foundAvgEst = false;

            for (int i = 0; i <= AvgWSEst_Count - 1; i++)
            { 
                if (avgWS_Est[i].isWaked == true) {
                    if (thisInst.wakeModelList.IsSameWakeModel(avgWS_Est[i].wakeModel, thisWakeModel) && avgWS_Est[i].isCalibrated == isCalibrated && avgWS_Est[i].usesSRDH == thisInst.topo.useSR 
                        && avgWS_Est[i].usesFlowSep == thisInst.topo.useSepMod) {
                        avgWS_index = i;
                        foundAvgEst = true;
                        break;
                    }
                }
            }

            int freeStreamAvgIndex = 0;
            for (int i = 0; i <= AvgWSEst_Count - 1; i++)
                if (avgWS_Est[i].isWaked == false && avgWS_Est[i].isCalibrated == isCalibrated && avgWS_Est[i].usesSRDH == thisInst.topo.useSR && avgWS_Est[i].usesFlowSep == thisInst.topo.useSepMod)
                    freeStreamAvgIndex = i;               
            
            if (foundAvgEst == false)
            { // need to create one
                Turbine.Avg_Est newAvgEst = new Turbine.Avg_Est();
                newAvgEst.isCalibrated = isCalibrated;
                newAvgEst.isWaked = true;
                newAvgEst.wakeModel = thisWakeModel;
                newAvgEst.usesSRDH = thisInst.topo.useSR;
                newAvgEst.usesFlowSep = thisInst.topo.useSepMod;
                AddAvgWS_Estimate(newAvgEst);
            }

            for (int i = 0; i <= AvgWSEst_Count - 1; i++)
            { 
                if (avgWS_Est[i].isWaked == true) {
                    if (thisInst.wakeModelList.IsSameWakeModel(avgWS_Est[i].wakeModel, thisWakeModel) && avgWS_Est[i].isCalibrated == isCalibrated && avgWS_Est[i].usesSRDH == thisInst.topo.useSR 
                        && avgWS_Est[i].usesFlowSep == thisInst.topo.useSepMod) {
                        avgWS_index = i;
                        foundAvgEst = true;
                        break;
                    }
                }
            }

            int grossIndex = 0;

            for (int i = 0; i <= GrossAEP_Count - 1; i++)
                if (grossAEP[i].powerCurve.name == thisWakeModel.powerCurve.name && grossAEP[i].isCalibrated == isCalibrated && grossAEP[i].useSRDH == thisInst.topo.useSR
                    && grossAEP[i].usesFlowSep == thisInst.topo.useSepMod) {
                    grossIndex = i;
                    break;
                }

            int netIndex = 0;
            bool foundNet = false;

            for (int i = 0; i <= NetAEP_Count - 1; i++)
                if (thisInst.wakeModelList.IsSameWakeModel(netAEP[i].wakeModel, thisWakeModel) && netAEP[i].isCalibrated == isCalibrated && netAEP[i].useSRDH == thisInst.topo.useSR 
                    && netAEP[i].usesFlowSep == thisInst.topo.useSepMod) {
                    netIndex = i;
                    foundNet = true;
                    break;
                }

            if (foundNet == false)
                AddNetAEP(thisWakeModel, 0, 0, 0, 0, isCalibrated, 0, null, thisInst.topo.useSR, thisInst.topo.useSepMod);            

            for (int i = 0; i <= NetAEP_Count - 1; i++)
                if (thisInst.wakeModelList.IsSameWakeModel(netAEP[i].wakeModel, thisWakeModel) && netAEP[i].isCalibrated == isCalibrated && netAEP[i].useSRDH == thisInst.topo.useSR 
                    && netAEP[i].usesFlowSep == thisInst.topo.useSepMod) {
                    netIndex = i;
                    break;
                }

            wakeResults = thisInst.wakeModelList.CalcWakeLosses(wakeCoeffs, UTMX, UTMY, avgWS_Est[freeStreamAvgIndex].sectorWS_Dist, grossAEP[grossIndex].AEP, grossAEP[grossIndex].sectorEnergy,
                                                     thisInst, avgWS_Est[avgWS_index].wakeModel);

            double Total_Other_Loss = thisInst.otherLosses.Get_Total_Losses();

            avgWS_Est[avgWS_index].WS = wakeResults.wakedWS;
            avgWS_Est[avgWS_index].WS_Dist = wakeResults.WS_Dist;
            avgWS_Est[avgWS_index].sectorWS_Dist = wakeResults.sectorDist;
            avgWS_Est[avgWS_index].sectorWS = wakeResults.sectorWakedWS;

            netAEP[netIndex].AEP = wakeResults.netEnergy * (1 - Total_Other_Loss);
            netAEP[netIndex].sectorEnergy = wakeResults.sectorNetEnergy;

            for (int WD = 0; WD <= netAEP[netIndex].sectorEnergy.Length - 1; WD++)
                netAEP[netIndex].sectorEnergy[WD] = netAEP[netIndex].sectorEnergy[WD] * (1 - Total_Other_Loss);

            netAEP[netIndex].wakeLoss = wakeResults.wakeLoss;
            netAEP[netIndex].sectorWakeLoss = wakeResults.sectorWakeLoss;
            netAEP[netIndex].CF = thisInst.turbineList.CalcCapacityFactor(netAEP[netIndex].AEP, thisWakeModel.powerCurve.ratedPower);

            // Calculate weibull params          
            MetCollection.Weibull_params This_weibull = thisInst.metList.CalcWeibullParams(avgWS_Est[avgWS_index].WS_Dist, avgWS_Est[avgWS_index].sectorWS_Dist, avgWS_Est[avgWS_index].WS);

            avgWS_Est[avgWS_index].weibull_A = This_weibull.overall_A;
            avgWS_Est[avgWS_index].weibull_k = This_weibull.overall_k;
            avgWS_Est[avgWS_index].sectWeibull_A = This_weibull.sector_A;
            avgWS_Est[avgWS_index].sectWeibull_k = This_weibull.sector_k;

        }

        public void DoTurbineCalcs(Nodes[] allNodesInPaths, Continuum thisInst, Model[] models)
        {
            // Does grid stat at turbine location then for each predictor met and each model (i.e. 4 Radii) finds a path of nodes to
            //  turbine site, does wind speed estimate along nodes using specified model and creates WS_Estimates()                                   
                      
            // Calculate grid statistics at turbine site
            if (gridStats.stats == null)            
                gridStats.GetGridArrayAndCalcStats(UTMX, UTMY, thisInst);

            // Find path of nodes in between mets and turbines. 
            NodeCollection nodeList = new NodeCollection();
            Nodes[] pathOfNodes = null;
            NodeCollection.Node_UTMs[] pathOfCoords = null;
            int numWD = thisInst.metList.numWD;
            Nodes[] newNodes = null;
            Nodes[] blankNodes = null; 
                                    
            if (models[0] == null)
                return;

            Met[] metsToUse = thisInst.metList.GetMets(models[0].metsUsed, null);

            for (int j = 0; j < metsToUse.Length; j++)
            {
                Met thisMet = metsToUse[j];                                             

                for (int r = 0; r < thisInst.radiiList.ThisCount; r++)
                {
                    // Check to see if already have this UW&DW estimate calculated
                    bool alreadyCalc = false;
                    for (int n = 0; n < WSEst_Count; n++)
                    { 
                        if (WS_Estimate[n].model != null) {
                            if (thisInst.modelList.IsSameModel(WS_Estimate[n].model, models[r]) && thisMet.name == WS_Estimate[n].predictorMetName) {
                                alreadyCalc = true;
                                break;
                            }
                        }
                    }

                    if (alreadyCalc == false)
                    {
                        // Check to see if there is a WS_Estimate for this met and radius that has been cleared // don't think that this can happen...
                        int thisRadius = thisInst.radiiList.investItem[r].radius;
                        
                        Turbine.WS_Ests newWS_Est = new Turbine.WS_Ests();
                        newWS_Est.predictorMetName = thisMet.name;
                        newWS_Est.model = models[r];
                        newWS_Est.radius = thisRadius;

                        // Check to see if elevation difference b/w met and turbine is within limits
                        bool isWithinLimits = thisInst.modelList.IsWithinModelLimit(gridStats, elev, thisMet.gridStats, thisMet.elev, r, thisMet.windRose);
                        AddWS_Estimate(newWS_Est);

                        if (isWithinLimits)
                        {
                            // Search WS_estimates already created to see if a path of nodes to that met has already been found
                            bool gotNodes = false;
                            for (int m = 0; m < WSEst_Count; m++) {
                                if (thisMet.name == WS_Estimate[m].predictorMetName && WS_Estimate[m].radius == thisRadius
                                    && WS_Estimate[m].pathOfNodesUTMs != null) {
                                    pathOfCoords = WS_Estimate[m].pathOfNodesUTMs;
                                    gotNodes = true;
                                    break;
                                }
                            }

                            if (gotNodes == false) {                           

                                Nodes targetNode = nodeList.GetTurbNode(this);
                                Nodes startNode = nodeList.GetMetNode(thisMet);

                                pathOfNodes = nodeList.FindPathOfNodes(startNode, targetNode, models[r], thisInst, ref newNodes, ref blankNodes);
                            }
                            else
                            {
                                // found path of coords
                                if (pathOfCoords.Length > 0)
                                {
                                    pathOfNodes = new Nodes[pathOfCoords.Length];

                                    if (allNodesInPaths != null)
                                        pathOfNodes = GetPathFromList(pathOfCoords, allNodesInPaths, thisInst);
                                    else
                                        pathOfNodes = nodeList.GetPathOfNodes(pathOfCoords, thisInst, ref blankNodes);

                                }
                                else
                                    pathOfNodes = new Nodes[0];

                            }

                            int WS_Est_ind = WSEst_Count - 1;

                            if (pathOfNodes != null)
                            {
                                if (pathOfNodes.Length == 200)
                                    pathOfNodes = null;

                                pathOfCoords = new NodeCollection.Node_UTMs[pathOfNodes.Length];
                                for (int m = 0; m <= pathOfNodes.Length - 1; m++)
                                {
                                    pathOfCoords[m].UTMX = pathOfNodes[m].UTMX;
                                    pathOfCoords[m].UTMY = pathOfNodes[m].UTMY;
                                }

                                WS_Estimate[WS_Est_ind].pathOfNodesUTMs = pathOfCoords;
                                WS_Estimate[WS_Est_ind].radius = models[r].radius;
                                DoWS_EstAlongNodes(WS_Est_ind, r, pathOfNodes, thisInst);
                            }
                        }
                    }                        
                }
            }

            if (newNodes != null)
                nodeList.AddNodes(newNodes, thisInst.savedParams.savedFileName);            

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
                        pathToMet[nodeIndex] = nodeList.GetANode(thisUTMX, thisUTMY, thisInst, ref blank, null);
                        if (thisInst.topo.useSepMod == true) pathToMet[nodeIndex].GetFlowSepNodes(nodeList, thisInst, null);
                    }

                }
            }

            if (numNodes == 0)
                pathToMet = null;

            return pathToMet;
        }

        public void DoWS_EstAlongNodes(int WS_EstIndex, int radiusIndex, Nodes[] pathOfNodes, Continuum thisInst)
        {
            //  Performs wind speed estimate along path of nodes from Met to turbine using specified model and predictor met
            if (thisInst.metList.ThisCount == 0) return;
            Met thisMet = null;
            NodeCollection nodeList = new NodeCollection();

            for (int i = 0; i < thisInst.metList.ThisCount; i++) { 
                if (thisInst.metList.metItem[i].name == WS_Estimate[WS_EstIndex].predictorMetName) {
                    thisMet = thisInst.metList.metItem[i];
                    break;
                }
            }                      
                        
            int numWD = thisInst.metList.numWD;
            int numNodes = 0;
            if (pathOfNodes != null) numNodes = pathOfNodes.Length;

            WS_Estimate[WS_EstIndex].sectorWS = new double[numWD];
            Nodes endNode = nodeList.GetTurbNode(this);
            ModelCollection.WS_Est_Struct WS_EstStr = thisInst.modelList.DoWS_Estimate(thisMet, endNode, pathOfNodes, radiusIndex, WS_Estimate[WS_EstIndex].model, thisInst);

            if (numNodes > 0) {
                WS_Estimate[WS_EstIndex].WS_at_nodes = new double[numNodes];
                WS_Estimate[WS_EstIndex].sectorWS_at_nodes = new double[numNodes, numWD];
            }

            WS_Estimate[WS_EstIndex].sectorWS = WS_EstStr.sectorWS;

            for (int nodeIndex = 0; nodeIndex <= numNodes - 1; nodeIndex++)
                for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                    WS_Estimate[WS_EstIndex].sectorWS_at_nodes[nodeIndex, WD_Ind] = WS_EstStr.sectorWS_AtNodes[nodeIndex, WD_Ind];               
            
            if (numNodes > 0)
            {
                for (int nodeIndex = 0; nodeIndex < numNodes; nodeIndex++)
                {
                    WS_Estimate[WS_EstIndex].WS_at_nodes[nodeIndex] = 0;
                    for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                        WS_Estimate[WS_EstIndex].WS_at_nodes[nodeIndex] = WS_Estimate[WS_EstIndex].WS_at_nodes[nodeIndex] + WS_Estimate[WS_EstIndex].sectorWS_at_nodes[nodeIndex, WD_Ind] * windRose[WD_Ind];                    
                }
            }

            WS_Estimate[WS_EstIndex].WS = 0;
            for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                WS_Estimate[WS_EstIndex].WS = WS_Estimate[WS_EstIndex].WS + WS_Estimate[WS_EstIndex].sectorWS[WD_Ind] * windRose[WD_Ind];            

        }

        public void GenerateAvgWS(Continuum thisInst, Model[] models)
        {
            // Calculates Avg WS, uncertainty AEP, weibull params for each turbine for either site-calibrated or default model
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
            int predictingMetIndex = 0;
              
            Nodes turbineNode = nodeList.GetTurbNode(this);
            double[,] indivMetWeights = thisInst.modelList.GetWS_EstWeights(predictingMets, turbineNode, models, thisInst.metList.GetAvgWindRoseMetsUsed(metsUsed));

            for (int r = 0; r < thisInst.radiiList.ThisCount; r++)
            {
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

                    for (int k = 0; k < metsUsed.Length; k++)
                    {
                        if (metsUsed[k] == predictingMet.name)
                        {
                            predictingMetIndex = k;
                            break;
                        }
                    }

                    if (thisInst.modelList.IsSameModel(WS_Estimate[j].model, models[r]) == true && WS_Estimate[j].elevDiffTooBig == false &&
                        WS_Estimate[j].expoDiffTooBig == false && WS_Estimate[j].WS != 0)
                    {
                        avgWS = avgWS + WS_Estimate[j].WS * indivMetWeights[predictingMetIndex, r];
                        WS_Estimate[j].WS_weight = indivMetWeights[predictingMetIndex, r];
                        avgWeight = avgWeight + indivMetWeights[predictingMetIndex, r];

                        for (int WD = 0; WD <= numWD - 1; WD++)
                            avgSectorWS[WD] = avgSectorWS[WD] + WS_Estimate[j].sectorWS[WD] * indivMetWeights[predictingMetIndex, r];

                    }
                }
            }
          
            int avgWS_index = 0;
            bool foundAvgEst = false;
                     
            foundAvgEst = false;
            // Check to see if need to create one or if one exists
            for (int j = 0; j < AvgWSEst_Count; j++)
            {
                if (avgWS_Est[j].isCalibrated == models[0].isCalibrated && avgWS_Est[j].isImported == models[0].isImported)
                {
                    avgWS_index = j;
                    foundAvgEst = true;
                    break;
                }
            }

            if (foundAvgEst == false)
            { // need to create one
                Avg_Est newAvgEst = new Avg_Est();
                newAvgEst.isCalibrated = models[0].isCalibrated;                        
                AddAvgWS_Estimate(newAvgEst);
            }

            for (int j = 0; j < AvgWSEst_Count; j++)
            {
                if (avgWS_Est[j].isCalibrated == models[0].isCalibrated && avgWS_Est[j].isImported == models[0].isImported)
                {
                    avgWS_index = j;
                    foundAvgEst = true;
                    break;
                }
            }

            avgWS_Est[avgWS_index].WS = 0;
            avgWS_Est[avgWS_index].uncert = GetErrorEst(thisInst);
            avgWS_Est[avgWS_index].sectorWS = new double[numWD];

            if (avgWeight != 0)
            {
                avgWS_Est[avgWS_index].WS = avgWS / avgWeight;

                for (int WD = 0; WD <= numWD - 1; WD++)
                    avgWS_Est[avgWS_index].sectorWS[WD] = avgSectorWS[WD] / avgWeight;

            }

            int numWS = thisInst.metList.metItem[0].WS_Dist.Length;
            avgWS_Est[avgWS_index].sectorWS_Dist = new double[numWD, numWS];

            for (int WD = 0; WD < numWD; WD++)
            {
                double[] WS_Dist = thisInst.metList.CalcWS_DistForTurbOrMap(metsUsed, avgWS_Est[avgWS_index].sectorWS[WD], WD);

                for (int WS = 0; WS < numWS; WS++)
                    avgWS_Est[avgWS_index].sectorWS_Dist[WD, WS] = WS_Dist[WS] * 1000;

            }

            // instead of forming WS_Dist from overall WS, use sectorWS distributions (as is done in net energy calcs) 10/10/2016
            avgWS_Est[avgWS_index].WS_Dist = thisInst.metList.CalcOverallWS_Dist(avgWS_Est[avgWS_index].sectorWS_Dist, windRose);

            // Calculate weibull params
            MetCollection.Weibull_params thisWeibull = thisInst.metList.CalcWeibullParams(avgWS_Est[avgWS_index].WS_Dist, avgWS_Est[avgWS_index].sectorWS_Dist,
                                                                            avgWS_Est[avgWS_index].WS);

            avgWS_Est[avgWS_index].weibull_A = thisWeibull.overall_A;
            avgWS_Est[avgWS_index].weibull_k = thisWeibull.overall_k;
            avgWS_Est[avgWS_index].sectWeibull_A = thisWeibull.sector_A;
            avgWS_Est[avgWS_index].sectWeibull_k = thisWeibull.sector_k;
            avgWS_Est[avgWS_index].usesSRDH = thisInst.topo.useSR;
            avgWS_Est[avgWS_index].usesFlowSep = thisInst.topo.useSepMod;
          
        }

        public double GetErrorEst(Continuum thisInst)
        {
            double overallUncert = 0;
            bool isImported = false;
            if (thisInst.modelList.models.GetUpperBound(0) > 0) // has default and at least one other model (i.e. either imported or site-calibrated
                if (thisInst.modelList.models[1, 0].isImported)
                    isImported = true;

            if (thisInst.metList.ThisCount <= 2 || isImported == true)
            {
                double adjFact = 1.2f; // what is this based on?
                // these errors are based on overall WS errors.
                if (gridStats.GetOverallP10(windRose, 0, "DW") < 15)
                    overallUncert = 0.0167f * adjFact;
                else if (gridStats.GetOverallP10(windRose, 0, "DW") < 85)
                    overallUncert = 0.021f * adjFact;
                else
                    overallUncert = 0.03f * adjFact;
            }
            else
            {
                if (thisInst.modelList.ModelCount > 1)
                {
                    Model[] models = new Model[thisInst.radiiList.ThisCount];

                    for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
                        models[i] = thisInst.modelList.models[1, i];

                    double[] theseWgts = thisInst.modelList.GetModelWeights(models);
                    double sumWgts = 0;
                    int numWD = thisInst.GetNumWD();

                    for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
                    {
                        Model thisModel = models[i];
                        double modelUncert = 0;

                        for (int j = 0; j < numWD; j++)
                        {
                            double thisUncert = thisModel.GetUncertaintyEstimate(thisInst, gridStats.stats[i].P10_UW[j], gridStats.stats[i].P10_DW[j],
                                expo[i].expo[j], expo[i].GetDW_Param(j, "Expo"), j);
                            modelUncert = modelUncert + thisUncert * windRose[j];
                        }

                        overallUncert = overallUncert + modelUncert * theseWgts[i];
                        sumWgts = sumWgts + theseWgts[i];
                    }

                    if (sumWgts > 0)
                        overallUncert = overallUncert / sumWgts;
                }
            }

            return overallUncert;
        }
    }
}
