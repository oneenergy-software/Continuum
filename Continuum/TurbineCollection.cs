using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ContinuumNS
{
    [Serializable()]
    public class TurbineCollection
    {
        public Turbine[] turbineEsts;
        public bool turbineCalcsDone = false;
        public PowerCurve[] powerCurves;


        [Serializable()] public struct PowerCurve
        {
            public string name;
            public double cutInWS;
            public double cutOutWS;
            public double ratedPower;
            public double[] power;
            public double RD;
            public double[] thrustCoeff;
        }

        public int TurbineCount {
            get {
                if (turbineEsts == null)
                    return 0;
                else
                    return turbineEsts.Length;
            }
        }

        public int PowerCurveCount {
            get {
                if (powerCurves == null)
                    return 0;
                else
                    return powerCurves.Length;
            }
        }

        public int NumStrings {
            get {
                if (TurbineCount > 0)
                {
                    int maxNum = 0;

                    for (int i = 0; i < TurbineCount; i++)
                    {
                        if (turbineEsts[i].stringNum > maxNum)
                            maxNum = turbineEsts[i].stringNum;
                    }
                    return maxNum;
                }
                else
                    return 0;
            }
        }

        public void ClearAllTurbines() {
            // Clears all turbine objects in list
            turbineEsts = null;
        }

        public void ClearAllPowerCurves() {
            // Clears all power curves in list
            powerCurves = null;
        }

        public void ClearAllGrossEsts() {
            // Clears all gross AEP estimates formed at turbine sites
            for (int i = 0; i < TurbineCount; i++)
                turbineEsts[i].grossAEP = null;
        }

        public void ClearAllWSEsts() {
            // Clears all WS_Estimate() and avgWS_Est()
            for (int i = 0; i < TurbineCount; i++) {
                turbineEsts[i].avgWS_Est = null;
                turbineEsts[i].WS_Estimate = null;
            }
        }

        public void ClearAllNetEsts() {
            //  Clears all net AEP estimates formed at turbine sites
            for (int i = 0; i < TurbineCount; i++)
            {
                turbineEsts[i].netAEP = null;

                Turbine.Avg_Est[] unwakedAvgWS_Est = null;
                int numUnwaked = 0;

                // Delete all waked avg WS estimates
                for (int j = 0; j < turbineEsts[i].AvgWSEst_Count; j++)
                {
                    if (turbineEsts[i].avgWS_Est[j].isWaked == false)
                    {
                        numUnwaked++;
                        Array.Resize(ref unwakedAvgWS_Est, numUnwaked);
                        unwakedAvgWS_Est[numUnwaked - 1] = turbineEsts[i].avgWS_Est[j];
                    }
                }

                turbineEsts[i].avgWS_Est = unwakedAvgWS_Est;
            }
        }

        public Nodes[] GetAllNodesInPaths(NodeCollection nodeList, Continuum thisInst)
        {
            // Gets and returns all paths to mets for all turbines in list
                        
            Nodes[] allNodesInPaths = null;
            NodeCollection.Node_UTMs[] allNodesInPathsUTM = null;
            int allNodeCount = 0;

            double thisUTMX;
            double thisUTMY;

            for (int turbineIndex = 0; turbineIndex < TurbineCount; turbineIndex++)
            {
                int numNodes;
                int numWSEsts;

                try {
                    numWSEsts = turbineEsts[turbineIndex].WSEst_Count;
                }
                catch  {
                    numWSEsts = 0;
                }

                if (numWSEsts > 0)
                {
                    for (int WS_ind = 0; WS_ind < numWSEsts; WS_ind++)
                    {
                        try {
                            numNodes = turbineEsts[turbineIndex].WS_Estimate[WS_ind].pathOfNodesUTMs.Length;
                        }
                        catch {
                            numNodes = 0;
                        }

                        if (numNodes > 0)
                        {
                            // Go through path of nodes and add to list if not already there
                            bool alreadyGotIt = false;

                            for (int nodeIndex = 0; nodeIndex <= numNodes - 1; nodeIndex++)
                            {
                                alreadyGotIt = false;
                                thisUTMX = turbineEsts[turbineIndex].WS_Estimate[WS_ind].pathOfNodesUTMs[nodeIndex].UTMX;
                                thisUTMY = turbineEsts[turbineIndex].WS_Estimate[WS_ind].pathOfNodesUTMs[nodeIndex].UTMY;

                                for (int All_Node_ind = 0; All_Node_ind <= allNodeCount - 1; All_Node_ind++)
                                {
                                    if (allNodesInPathsUTM[All_Node_ind].UTMX == thisUTMX && allNodesInPathsUTM[All_Node_ind].UTMY == thisUTMY)
                                    {
                                        alreadyGotIt = true;
                                        break;
                                    }
                                }

                                if (alreadyGotIt == false) {
                                    Array.Resize(ref allNodesInPathsUTM, allNodeCount + 1);
                                    allNodesInPathsUTM[allNodeCount].UTMX = thisUTMX;
                                    allNodesInPathsUTM[allNodeCount].UTMY = thisUTMY;
                                    allNodeCount++;
                                }
                            }
                        }
                    }
                }
            }

            Nodes[] blank = null;
            if (allNodesInPathsUTM != null)
                allNodesInPaths = nodeList.GetPathOfNodes(allNodesInPathsUTM, thisInst, ref blank);

            return allNodesInPaths;
        }

        public void ClearWS_EstCalcs(MetCollection metList)
        {
            // Keeps default WS estimates and path of nodes between turbs and each met but clears the WS calc using calibrated model
            for (int i = 0; i < TurbineCount ; i++)
            {
                Turbine.WS_Ests[] WS_Ests_Default = null;
                int numDefaultWS_Ests = 0;

                for (int j = 0; j < turbineEsts[i].WSEst_Count; j++)
                {
                    if (turbineEsts[i].WS_Estimate[j].model != null)
                    {
                        if (turbineEsts[i].WS_Estimate[j].model.isCalibrated == true)
                        { // clears turb ests that use site-calibrated model but keeps default model estimates
                            numDefaultWS_Ests++;
                            Array.Resize(ref WS_Ests_Default, numDefaultWS_Ests);
                            WS_Ests_Default[numDefaultWS_Ests - 1] = turbineEsts[i].WS_Estimate[j];
                        }
                    }
                }

                turbineEsts[i].WS_Estimate = WS_Ests_Default;
            }
        }

        public void ClearWS_EstsDeletedMets(MetCollection metList)
        {
            // Clears WS estimates based on any mets that were deleted

            int numMets = metList.ThisCount;
            
            Turbine.WS_Ests[] newWS_Ests = null;
            int newWS_EstCount = 0;

            for (int i = 0; i < TurbineCount; i++)
            {
                for (int j = 0; j < turbineEsts[i].WSEst_Count; j++)
                {
                    // Check to see if predictor is in met list
                    bool metInList = false;
                    for (int k = 0; k < numMets; k++)
                    {
                        if (metList.metItem[k].name == turbineEsts[i].WS_Estimate[j].predictorMetName)
                        {
                            metInList = true;
                            break;
                        }
                    }

                    if (metInList == true) {
                        Array.Resize(ref newWS_Ests, newWS_EstCount + 1);
                        newWS_Ests[newWS_EstCount] = turbineEsts[i].WS_Estimate[j];
                        newWS_EstCount++;
                    }
                }

                turbineEsts[i].WS_Estimate = null;
                if (newWS_EstCount > 0)
                {
                    turbineEsts[i].WS_Estimate = new Turbine.WS_Ests[newWS_EstCount];

                    for (int j = 0; j < newWS_EstCount; j++)
                        turbineEsts[i].WS_Estimate[j] = newWS_Ests[j];

                    newWS_Ests = new Turbine.WS_Ests[1];
                    newWS_EstCount = 0;
                }
            }
        }

        public void ClearAllCalcs()
        {
            // Clears all calculations at turbine sites
            for (int i = 0; i < TurbineCount; i++)
            {
                try {
                    turbineEsts[i].gridStats.stats = null;
                }
                catch { }

                turbineEsts[i].expo = null;
                turbineEsts[i].WS_Estimate = null;
                turbineEsts[i].avgWS_Est = null;
                turbineEsts[i].grossAEP = null;
                turbineEsts[i].flowSepNodes = null;
                turbineEsts[i].netAEP = null;
                turbineEsts[i].windRose = null;
            }

            turbineCalcsDone = false;

        }

        public void AddPowerCurve(string name, double cutIn, double cutOut, double ratedPower, double[] powerValues, double[] thrustValues, double RD)
        {
            // Add power curve to list
            int newCount = PowerCurveCount;
            Array.Resize(ref powerCurves, newCount + 1);

            powerCurves[newCount].name = name;
            powerCurves[newCount].cutInWS = cutIn;
            powerCurves[newCount].cutOutWS = cutOut;
            powerCurves[newCount].ratedPower = ratedPower;
            powerCurves[newCount].power = powerValues;
            powerCurves[newCount].thrustCoeff = thrustValues;
            powerCurves[newCount].RD = RD;

        }

        public void ReadPowerCurve(Continuum thisInst, string name, double[,] importedPower)
        {
            //  Imports new power curve and adds to list                    
            double cutIn = 100;
            double cutOut = 0;
            double ratedPower = 0;
            
            int numMets = thisInst.metList.ThisCount;
            int numWS_Import = importedPower.GetUpperBound(1) + 1;

            if (numMets > 0)
            {
                Met thisMet = thisInst.metList.metItem[0];
                int numWS_mets = thisMet.WS_Dist.Length;
                double WS_FirstInt = thisMet.WS_FirstInt;
                double WS_IntSize = thisMet.WS_IntSize;

                double[,] thisPowerCurve = new double[3, numWS_mets];

                for (int i = 0; i < numWS_mets; i++)
                    thisPowerCurve[0, i] = WS_FirstInt + WS_IntSize * i - WS_IntSize / 2;

                for (int i = 0; i < numWS_Import; i++)
                {
                    double thisWS = importedPower[0, i];
                    for (int j = 0; j < numWS_mets; j++)
                    {
                        if (thisWS == thisPowerCurve[0, j]) {
                            thisPowerCurve[1, j] = importedPower[1, i];
                            thisPowerCurve[2, j] = importedPower[2, i];
                            break;
                        }
                    }
                }

                // if ( the WS bins used in the power curve file don//t line up with the met WS dists then the array will be blank so send message and exit sub
                bool gotOne = false;
                for (int i = 0; i < numWS_mets; i++) {
                    if (thisPowerCurve[1, i] != 0) {
                        gotOne = true;
                        break;
                    }
                }

                if (gotOne == false) {
                    MessageBox.Show("Error reading in the power curve.  The WS bins don//t line up with the met WS distributions.  Recall that TAB files use WS bins that represent the max value in that bin while the power curve files use WS bins that represent the mid value of the bin.");
                    return;
                }

                // Fill in blanks between cut-in and cut-out WS
                for (int i = 0; i < numWS_mets; i++) {
                    if (thisPowerCurve[1, i] == 0 && thisPowerCurve[1, i + 1] > 0) {
                        cutIn = thisPowerCurve[0, i + 1];
                        break;
                    }
                }

                for (int i = numWS_mets - 1; i >= 0; i--)
                {
                    if (i > 0)
                    {
                        if (thisPowerCurve[1, i] == 0 && thisPowerCurve[1, i - 1] > 0)
                        {
                            cutOut = thisPowerCurve[0, i - 1];
                            break;
                        }
                    }
                    else
                        cutOut = thisPowerCurve[0, numWS_mets - 1];
                }

                for (int i = 0; i < numWS_mets; i++)
                    if (thisPowerCurve[1, i] > ratedPower)
                        ratedPower = thisPowerCurve[1, i];

                for (int i = 0; i < numWS_mets; i++)
                    if (thisPowerCurve[0, i] > cutIn && thisPowerCurve[0, i] < cutOut && thisPowerCurve[1, i] == 0)
                        thisPowerCurve[1, i] = ratedPower;

                double[] powerOnly = new double[numWS_mets];
                double[] thrustOnly = new double[numWS_mets];

                for (int i = 0; i < numWS_mets; i++) {
                    powerOnly[i] = thisPowerCurve[1, i];
                    thrustOnly[i] = thisPowerCurve[2, i];
                }

                double RD = Convert.ToSingle(Interaction.InputBox("What is the rotor diameter [m] of the turbine?", "Continuum 2.3"));
                AddPowerCurve(name, cutIn, cutOut, ratedPower, powerOnly, thrustOnly, RD);

            }

        }

        public void AddTurbine(string name, double utmx, double utmy, int stringNum)
        {
            // Adds Turbine object to list
            // Check to see that the turbine doesn//t already exist in memory
            for (int i = 0; i < TurbineCount; i++) {
                if (name == turbineEsts[i].name) {
                    MessageBox.Show("A turbine of the same name already exists.");
                    return;
                }
            }

            int newCount = TurbineCount;
            Array.Resize(ref turbineEsts, newCount + 1);

            turbineEsts[newCount] = new Turbine();
            turbineEsts[newCount].name = name;
            turbineEsts[newCount].UTMX = utmx;
            turbineEsts[newCount].UTMY = utmy;
            if (stringNum != 0) turbineEsts[newCount].stringNum = stringNum;

        }

        public void EditTurbine(string name, double utmx, double utmy)
        {
            // Edits the UTMX, UTMY of turbine site and clears all calculated values
            for (int i = 0; i <= TurbineCount - 1; i++) {
                if (name == turbineEsts[i].name) {
                    turbineEsts[i].UTMX = utmx;
                    turbineEsts[i].UTMY = utmy;
                    turbineEsts[i].ClearAllCalcs();
                }
            }
        }

        public void CalcTurbineExposures(Continuum thisInst, int radius, double exponent, int numSectors)
        {
            // Calculates exposure and SRDH (if gotSR) for all radii at turbine sites            
            if (thisInst.metList.ThisCount == 0)
                return; // can't do exposure calculations until TAB files have been imported and the numWD is set
            int numWD = thisInst.metList.numWD;

            for (int i = 0; i < TurbineCount; i++)
            {
                // First find elevation
                if (turbineEsts[i].elev == 0)
                    turbineEsts[i].elev = thisInst.topo.CalcElevs(turbineEsts[i].UTMX, turbineEsts[i].UTMY);

                // Check to see if the exposures have already been calculated
                bool isNewSRDH = turbineEsts[i].IsNewSRDH(radius, exponent, numSectors);
                bool isNew = turbineEsts[i].IsNewExposure(radius, exponent, numSectors);

                if (isNew == true)
                {
                    turbineEsts[i].AddExposure(radius, exponent, numSectors, thisInst.metList.metItem[0].windRose.Length);
                    // Find exposure index
                    int expoIndex = 0;
                    for (expoIndex = 0; expoIndex < turbineEsts[i].ExposureCount; expoIndex++)
                        if (turbineEsts[i].expo[expoIndex].radius == radius && turbineEsts[i].expo[expoIndex].exponent == exponent && turbineEsts[i].expo[expoIndex].numSectors == numSectors) // Found the exposure index
                            break;

                    // Check to see if an exposure with a smaller radii has been calculated
                    int smallerRadius = thisInst.topo.GetSmallerRadius(turbineEsts[i].expo, turbineEsts[i].expo[expoIndex].radius, turbineEsts[i].expo[expoIndex].exponent, numSectors);

                    if (smallerRadius == 0 || numSectors > 1)
                    { // when sector avg is used, can//t add on to exposure calcs...so gotta do it the long way
                        turbineEsts[i].expo[expoIndex] = thisInst.topo.CalcExposures(turbineEsts[i].UTMX, turbineEsts[i].UTMY, turbineEsts[i].elev, radius, exponent, numSectors, thisInst.topo, numWD);
                        if (thisInst.topo.gotSR == true)
                            thisInst.topo.CalcSRDH(ref turbineEsts[i].expo[expoIndex], turbineEsts[i].UTMX, turbineEsts[i].UTMY, radius, exponent, numWD);
                    }
                    else {
                        Exposure smallerExposure = thisInst.topo.GetSmallerRadiusExpo(turbineEsts[i].expo, smallerRadius, turbineEsts[i].expo[expoIndex].exponent, numSectors);

                        turbineEsts[i].expo[expoIndex] = thisInst.topo.CalcExposuresWithSmallerRadius(turbineEsts[i].UTMX, turbineEsts[i].UTMY,
                                        turbineEsts[i].elev, turbineEsts[i].expo[expoIndex].radius, turbineEsts[i].expo[expoIndex].exponent, numSectors, smallerRadius, smallerExposure, thisInst.metList.numWD);
                        if (thisInst.topo.gotSR == true)
                            thisInst.topo.CalcSRDHwithSmallerRadius(ref turbineEsts[i].expo[expoIndex], turbineEsts[i].UTMX, turbineEsts[i].UTMY, radius, exponent, numSectors, smallerRadius, smallerExposure, numWD);

                    }

                    if (turbineEsts[i].expo[expoIndex].UW_P10CrossGrade == null)
                    {
                        // Calc P10 UW Crosswind Grade
                        turbineEsts[i].expo[expoIndex].UW_P10CrossGrade = new double[numWD];
                        turbineEsts[i].expo[expoIndex].UW_ParallelGrade = new double[numWD];
                        
                        for (int r = 0; r < numWD; r++) {                            
                            turbineEsts[i].expo[expoIndex].UW_P10CrossGrade[r] = thisInst.topo.CalcP10_UW_CrosswindGrade(turbineEsts[i].UTMX, turbineEsts[i].UTMY, thisInst.radiiList, r, numWD); ;
                            turbineEsts[i].expo[expoIndex].UW_ParallelGrade[r] = thisInst.topo.CalcP10_UW_ParallelGrade(turbineEsts[i].UTMX, turbineEsts[i].UTMY, thisInst.radiiList, r, numWD);
                        }
                    }
                }

                else if (isNewSRDH == true)
                {
                    int expoIndex = 0;
                    for (expoIndex = 0; expoIndex <= turbineEsts[i].ExposureCount - 1; expoIndex++)
                        if (turbineEsts[i].expo[expoIndex].radius == radius && turbineEsts[i].expo[expoIndex].exponent == exponent && turbineEsts[i].expo[expoIndex].numSectors == numSectors)
                            // Found the exposure index
                            break;

                    if (thisInst.topo.gotSR == true)
                    {
                        // Check to see if an exposure with a smaller radii has been calculated
                        int smallerRadius = thisInst.topo.GetSmallerRadius(turbineEsts[i].expo, radius, exponent, numSectors);

                        if (smallerRadius == 0)
                            thisInst.topo.CalcSRDH(ref turbineEsts[i].expo[expoIndex], turbineEsts[i].UTMX, turbineEsts[i].UTMY, radius, exponent, numWD);
                        else {
                            Exposure smallerExposure = thisInst.topo.GetSmallerRadiusExpo(turbineEsts[i].expo, smallerRadius, turbineEsts[i].expo[expoIndex].exponent, numSectors);
                            thisInst.topo.CalcSRDHwithSmallerRadius(ref turbineEsts[i].expo[expoIndex], turbineEsts[i].UTMX, turbineEsts[i].UTMY, radius, exponent, numSectors, smallerRadius, smallerExposure, numWD);
                        }
                    }
                }
            }
        }

        public int[] CalcMinMaxDistanceToTurbines(double thisX, double thisY)
        {
            // Returns the min and max distance from the turbines to specified UTMX and UTMY
            int[] minMaxDistance = new int[2];
            minMaxDistance[0] = 10000000;
            minMaxDistance[1] = 0;
            double thisDistance = 0;
            TopoInfo topo = new TopoInfo();

            for (int i = 0; i < TurbineCount; i++) {
                thisDistance = topo.CalcDistanceBetweenPoints(thisX, thisY, turbineEsts[i].UTMX, turbineEsts[i].UTMY);
                if (thisDistance != 0 && thisDistance < minMaxDistance[0]) minMaxDistance[0] = (int)thisDistance;
                if (thisDistance > minMaxDistance[1]) minMaxDistance[1] = (int)thisDistance;
            }

            return minMaxDistance;
        }

        public void ReCalcTurbine_SRDH(Continuum thisInst)
        {
            //  Recalculates UW/DW Surface roughness & Displacement height at turbine sites
            int numSectors = 1;
                        
            for (int i = 0; i < TurbineCount; i++)
            {
                // First find elevation
                if (turbineEsts[i].elev == 0)
                    turbineEsts[i].elev = thisInst.topo.CalcElevs(turbineEsts[i].UTMX, turbineEsts[i].UTMY);
                
                int expoIndex = 0;

                for (int radiusIndex = 0; radiusIndex <= thisInst.radiiList.ThisCount - 1; radiusIndex++)
                {
                    int radius = thisInst.radiiList.investItem[radiusIndex].radius;
                    double exponent = thisInst.radiiList.investItem[radiusIndex].exponent;

                    for (expoIndex = 0; expoIndex <= turbineEsts[i].ExposureCount - 1; expoIndex++)
                        if (turbineEsts[i].expo[expoIndex].radius == radius && turbineEsts[i].expo[expoIndex].exponent == exponent && turbineEsts[i].expo[expoIndex].numSectors == numSectors)
                            // Found the exposure index
                            break;

                    if (thisInst.topo.gotSR == true)
                    {
                        // Check to see if an exposure with a smaller radii has been calculated
                        int smallerRadius = thisInst.topo.GetSmallerRadius(turbineEsts[i].expo, radius, exponent, numSectors);

                        if (smallerRadius == 0)
                            thisInst.topo.CalcSRDH(ref turbineEsts[i].expo[expoIndex], turbineEsts[i].UTMX, turbineEsts[i].UTMY, radius, exponent, thisInst.metList.numWD);
                        else {
                            Exposure smallerExposure = thisInst.topo.GetSmallerRadiusExpo(turbineEsts[i].expo, smallerRadius, turbineEsts[i].expo[expoIndex].exponent, numSectors);
                            thisInst.topo.CalcSRDHwithSmallerRadius(ref turbineEsts[i].expo[expoIndex], turbineEsts[i].UTMX, turbineEsts[i].UTMY, radius, exponent, numSectors, smallerRadius, smallerExposure, thisInst.metList.numWD);
                        }
                    }
                }
            }
            
        }

        public void ReCalcNetEnergy(double previousOtherLoss, double newOtherLoss)
        {
            // Recalculate net energy production with updated 'other' losses
            for (int i = 0; i <= TurbineCount - 1; i++)
            {
                int Num_Net_Ests = turbineEsts[i].NetAEP_Count;
                for (int j = 0; j < Num_Net_Ests; j++)
                {
                    turbineEsts[i].netAEP[j].AEP = turbineEsts[i].netAEP[j].AEP * (1 - newOtherLoss) / (1 - previousOtherLoss);

                    for (int WD = 0; WD < turbineEsts[i].netAEP[j].sectorEnergy.Length; WD++)
                        turbineEsts[i].netAEP[j].sectorEnergy[WD] = turbineEsts[i].netAEP[j].sectorEnergy[WD] * (1 - newOtherLoss) / (1 - previousOtherLoss);

                    turbineEsts[i].netAEP[j].CF = CalcCapacityFactor(turbineEsts[i].netAEP[j].AEP, turbineEsts[i].netAEP[j].wakeModel.powerCurve.ratedPower);
                }
            }

        }

        public void CalcEditTurbineExposures(Continuum thisInst, string name, int radius, double exponent, int numSectors)
        {
            // Calculates the exposure at specified turbine using specified radius

            int numMets = thisInst.metList.ThisCount;
            int numWD = thisInst.metList.numWD;
            if (numWD == 0) return;
            int turbineIndex;

            for (turbineIndex = 0; turbineIndex <= TurbineCount - 1; turbineIndex++)
                if (turbineEsts[turbineIndex].name == name)
                    // Found Turbine to perform calculations
                    break;

            // First find elevation
            if (turbineEsts[turbineIndex].elev == 0)
                turbineEsts[turbineIndex].elev = thisInst.topo.CalcElevs(turbineEsts[turbineIndex].UTMX, turbineEsts[turbineIndex].UTMY);

            // Check to see if the exposures have already been calculated
            int smallerRadius;
            Exposure smallerExposure;
            double[] windRose = thisInst.metList.GetAvgWindRose();

            bool isNew = turbineEsts[turbineIndex].IsNewExposure(radius, exponent, numSectors);
            if (isNew == true)
                turbineEsts[turbineIndex].AddExposure(radius, exponent, numSectors, numWD);

            // Find exponsure index
            int expoIndex = 0;
            for (expoIndex = 0; expoIndex <= turbineEsts[turbineIndex].ExposureCount - 1; expoIndex++)
                if (turbineEsts[turbineIndex].expo[expoIndex].radius == radius && turbineEsts[turbineIndex].expo[expoIndex].exponent == exponent && turbineEsts[turbineIndex].expo[expoIndex].numSectors == numSectors)
                    // Found the exposure index
                    break;

            // Check to see if an exposure with a smaller radii has been calculated
            smallerRadius = thisInst.topo.GetSmallerRadius(turbineEsts[turbineIndex].expo, turbineEsts[turbineIndex].expo[expoIndex].radius, turbineEsts[turbineIndex].expo[expoIndex].exponent, numSectors);

            if (smallerRadius == 0 || numSectors > 1)  // when sector avg is used, can//t add on to exposure calcs...so gotta do it the long way
                turbineEsts[turbineIndex].expo[expoIndex] = thisInst.topo.CalcExposures(turbineEsts[turbineIndex].UTMX, turbineEsts[turbineIndex].UTMY,
                                turbineEsts[turbineIndex].elev, turbineEsts[turbineIndex].expo[expoIndex].radius, turbineEsts[turbineIndex].expo[expoIndex].exponent, numSectors, thisInst.topo, numWD);
            else {
                smallerExposure = thisInst.topo.GetSmallerRadiusExpo(turbineEsts[turbineIndex].expo, smallerRadius, turbineEsts[turbineIndex].expo[expoIndex].exponent, numSectors);

                turbineEsts[turbineIndex].expo[expoIndex] = thisInst.topo.CalcExposuresWithSmallerRadius(turbineEsts[turbineIndex].UTMX, turbineEsts[turbineIndex].UTMY,
                                turbineEsts[turbineIndex].elev, turbineEsts[turbineIndex].expo[expoIndex].radius, turbineEsts[turbineIndex].expo[expoIndex].exponent, numSectors, smallerRadius, smallerExposure, numWD);
            }

            if (turbineEsts[turbineIndex].expo[expoIndex].UW_P10CrossGrade == null)
            {
                // Calc P10 UW Crosswind Grade
                double UW_Grade = 0;
                for (expoIndex = 0; expoIndex < thisInst.radiiList.ThisCount; expoIndex++)
                    turbineEsts[turbineIndex].expo[expoIndex].UW_P10CrossGrade = new double[windRose.Length];

                for (int r = 0; r < windRose.Length; r++) {
                    UW_Grade = thisInst.topo.CalcP10_UW_CrosswindGrade(turbineEsts[turbineIndex].UTMX, turbineEsts[turbineIndex].UTMY, thisInst.radiiList, r, windRose.Length);
                    for (expoIndex = 0; expoIndex <= thisInst.radiiList.ThisCount - 1; expoIndex++)
                        turbineEsts[turbineIndex].expo[expoIndex].UW_P10CrossGrade[r] = UW_Grade;

                }
            }
        }

        public void CalcGrossAEP(Continuum thisInst, bool isCalibrated)
        {
            // Calculates the gross AEP for every turbine and every power curve for either the site-calibrated or default model
            if (thisInst.metList.numWD == 0) return;
            int numWD = thisInst.metList.numWD;
            int numWS = thisInst.metList.numWS;

            if (thisInst.modelList.ModelCount <= 1 && isCalibrated == false)
                return;
            
            string[] metsUsed = thisInst.metList.GetMetsUsed();    

            for (int i = 0; i < PowerCurveCount; i++)
            {
                bool alreadyCalc = false;
                for (int j = 0; j < TurbineCount; j++)
                {
                    double[] P50_AEP_Sect = new double[numWD];
                    // Check to see if energy calc already done
                    alreadyCalc = false;
                    for (int k = 0; k < turbineEsts[j].GrossAEP_Count; k++) {
                        if (turbineEsts[j].grossAEP[k].powerCurve.name == powerCurves[i].name && turbineEsts[j].grossAEP[k].isCalibrated == isCalibrated)
                        { 
                            alreadyCalc = true;
                            break;
                        }
                    }

                    int avgWS_Index = 0;
                    for (int m = 0; m < turbineEsts[j].AvgWSEst_Count; m++) {
                        if (turbineEsts[j].avgWS_Est[m].isWaked == false && turbineEsts[j].avgWS_Est[m].isCalibrated == isCalibrated)
                        {
                                avgWS_Index = m;
                                break;
                        }                        
                    }

                    if (turbineEsts[j].avgWS_Est[avgWS_Index].WS == 0)
                        break;

                    if (alreadyCalc == false)
                    {            

                        double[,] sectorDist = new double[numWD, numWS];

                        for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++) {
                            double P90_WS = turbineEsts[j].avgWS_Est[avgWS_Index].sectorWS[WD_Ind] - turbineEsts[j].avgWS_Est[avgWS_Index].sectorWS[WD_Ind] * 0.0128155f;
                            double[] WS_Dist = thisInst.metList.CalcWS_DistForTurbOrMap(metsUsed, P90_WS, WD_Ind);
                            for (int WS_ind = 0; WS_ind <= numWS - 1; WS_ind++)
                                sectorDist[WD_Ind, WS_ind] = WS_Dist[WS_ind] * 1000;
                        }

                        double[] P90_Dist = thisInst.metList.CalcOverallWS_Dist(sectorDist, thisInst.metList.GetAvgWindRose());

                        for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++) {
                            double P99_WS = turbineEsts[j].avgWS_Est[avgWS_Index].sectorWS[WD_Ind] - turbineEsts[j].avgWS_Est[avgWS_Index].sectorWS[WD_Ind] * 0.02326f;
                            double[] WS_Dist = thisInst.metList.CalcWS_DistForTurbOrMap(metsUsed, P99_WS, WD_Ind);
                            for (int WS_ind = 0; WS_ind <= numWS - 1; WS_ind++)
                                sectorDist[WD_Ind, WS_ind] = WS_Dist[WS_ind] * 1000;
                        }

                        double[] P99_Dist = thisInst.metList.CalcOverallWS_Dist(sectorDist, thisInst.metList.GetAvgWindRose());

                        double P90_AEP = CalcAndReturnGrossAEP(P90_Dist, powerCurves[i].name);
                        double P99_AEP = CalcAndReturnGrossAEP(P99_Dist, powerCurves[i].name);
                        double P50_AEP = CalcAndReturnGrossAEP(turbineEsts[j].avgWS_Est[avgWS_Index].WS_Dist, powerCurves[i].name);                                             

                        for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                        {
                            double[] ThisDist = new double[numWS];
                            double[] windRose = thisInst.metList.GetAvgWindRose();
                            for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
                                ThisDist[WS_ind] = turbineEsts[j].avgWS_Est[avgWS_Index].sectorWS_Dist[WD_Ind, WS_ind]/1000;
                            P50_AEP_Sect[WD_Ind] = CalcAndReturnGrossAEP(ThisDist, powerCurves[i].name) * windRose[WD_Ind];
                        }                           
                     
                        double This_CF = CalcCapacityFactor(P50_AEP, powerCurves[i].ratedPower);
                        turbineEsts[j].AddGrossAEP(thisInst, powerCurves[i], P50_AEP, This_CF, P90_AEP, P99_AEP, turbineEsts[j].avgWS_Est[avgWS_Index].isCalibrated, P50_AEP_Sect);
                    }
                }
            }
        }

        public double CalcCapacityFactor(double AEP, double ratedPower) // AEP is in MWh and Rated_P is in kW
        {
            // Calculates and returns capacity factor
            double thisCF = AEP / (8760 * ratedPower / 1000);

            return thisCF;
        }

        public double CalcAndReturnGrossAEP(double[] WS_Dist, string powerCurve)
        {
            // Calculates and returns gross AEP calculated from specified WS_Dist and power_crv
            double thisAEP = 0;
            int numWS = WS_Dist.Length;
            
            for (int i = 0; i < PowerCurveCount; i++)
            {
                PowerCurve thisPowerCurve = powerCurves[i];
                if (thisPowerCurve.name == powerCurve) {
                    for (int k = 0; k < numWS; k++)
                        thisAEP = thisAEP + WS_Dist[k] * thisPowerCurve.power[k];

                    thisAEP = thisAEP * 365 * 24 / 1000;
                    break;
                }
            }

            return thisAEP;
        }

        public double GetOverallWakeLoss(Wake_Model thisWakeModel, int WD_Ind, bool isCalibrated)
        {
            // Returns overall wake loss for specified wake loss model, WD sector and site-calibrated vs. default model
            double overallWakeLoss = 0;
            double totalGrossAEP = 0;
            double totalNetAEP = 0;
            double thisWakeLoss = 0;

            for (int i = 0; i < TurbineCount; i++)
            {
                totalGrossAEP = totalGrossAEP + turbineEsts[i].GetGrossAEP(thisWakeModel.powerCurve.name, isCalibrated, WD_Ind);
                thisWakeLoss = turbineEsts[i].GetWakeLoss(thisWakeModel, isCalibrated, WD_Ind);
                totalNetAEP = totalNetAEP + (1 - thisWakeLoss) * turbineEsts[i].GetGrossAEP(thisWakeModel.powerCurve.name, isCalibrated, WD_Ind);
            }

            if (totalGrossAEP > 0)
                overallWakeLoss = (totalGrossAEP - totalNetAEP) / totalGrossAEP;
            else
                overallWakeLoss = 0;

            return overallWakeLoss;
        }

        public void AssignStringNumber()
        {
            // if ( turbine string numbers were not read in, assigns string numbers to turbines based on distance between sites
            if (TurbineCount > 0)
                if (turbineEsts[0].stringNum != 0)
                    return;

            // Clear old string numbers
            for (int i = 0; i < TurbineCount; i++)
                turbineEsts[i].stringNum = 0;

            double thisDistance = 0;
            double closestDistance = 100000;
            int lastString;
            TopoInfo topo = new TopoInfo();

            if (TurbineCount > 0)
            {
                turbineEsts[0].stringNum = 1;
                lastString = 1;

                for (int i = 1; i < TurbineCount; i++)
                {
                    closestDistance = 100000;
                    for (int j = 0; j < i; j++)
                    {
                        thisDistance = topo.CalcDistanceBetweenPoints(turbineEsts[i].UTMX, turbineEsts[i].UTMY, turbineEsts[j].UTMX, turbineEsts[j].UTMY);
                        if (thisDistance < 1000 && thisDistance < closestDistance) {
                            closestDistance = thisDistance;
                            turbineEsts[i].stringNum = turbineEsts[j].stringNum;
                        }
                    }

                    if (turbineEsts[i].stringNum == 0) {
                        turbineEsts[i].stringNum = lastString + 1;
                        lastString = lastString + 1;
                    }
                }
            }
        }

        public void CleanUpEsts(WakeCollection wakeList)
        {
            // if calculations were interrupted, this cleans up the turbine estimate list so that all turbine sites have same calculations
            bool inLastTurbine = false;

            if (TurbineCount > 0)
            {
                int numAvgEsts = turbineEsts[TurbineCount - 1].AvgWSEst_Count;
                int numGrossEsts = turbineEsts[TurbineCount - 1].GrossAEP_Count;
                int numNetEsts = turbineEsts[TurbineCount - 1].NetAEP_Count;

                // compare avg ws ests to ests in last turbine in list
                for (int i = 0; i < TurbineCount; i++)
                {
                    while (turbineEsts[i].AvgWSEst_Count != numAvgEsts)
                    {
                        for (int j = 0; j < turbineEsts[i].AvgWSEst_Count; j++)
                        {
                            inLastTurbine = false;
                            for (int k = 0; k < turbineEsts[TurbineCount - 1].AvgWSEst_Count; k++)
                            {
                                if (turbineEsts[i].avgWS_Est[j].isWaked == turbineEsts[TurbineCount - 1].avgWS_Est[k].isWaked &&
                                    turbineEsts[i].avgWS_Est[j].isCalibrated == turbineEsts[TurbineCount - 1].avgWS_Est[k].isCalibrated &&
                                    turbineEsts[i].avgWS_Est[j].usesSRDH == turbineEsts[TurbineCount - 1].avgWS_Est[k].usesSRDH &&
                                    turbineEsts[i].avgWS_Est[j].usesFlowSep == turbineEsts[TurbineCount - 1].avgWS_Est[k].usesFlowSep &&
                                    ((turbineEsts[TurbineCount - 1].avgWS_Est[k].wakeModel == null && turbineEsts[TurbineCount - 1].avgWS_Est[k].wakeModel == null) ||
                                     wakeList.IsSameWakeModel(turbineEsts[i].avgWS_Est[j].wakeModel, turbineEsts[TurbineCount - 1].avgWS_Est[k].wakeModel))) {
                                    inLastTurbine = true;
                                    break;
                                }
                            }

                            if (inLastTurbine == false) {
                                turbineEsts[i].RemoveAvgWS(j);
                                break;
                            }
                        }
                    }

                    while (turbineEsts[i].GrossAEP_Count != numGrossEsts)
                    {
                        for (int j = 0; j < turbineEsts[i].GrossAEP_Count; j++)
                        {
                            inLastTurbine = false;
                            for (int k = 0; k < turbineEsts[TurbineCount - 1].GrossAEP_Count; k++)
                            {
                                if (turbineEsts[i].grossAEP[j].powerCurve.name == turbineEsts[TurbineCount - 1].grossAEP[k].powerCurve.name &&
                                    turbineEsts[i].grossAEP[j].isCalibrated == turbineEsts[TurbineCount - 1].grossAEP[k].isCalibrated &&
                                    turbineEsts[i].grossAEP[j].useSRDH == turbineEsts[TurbineCount - 1].grossAEP[k].useSRDH &&
                                    turbineEsts[i].grossAEP[j].usesFlowSep == turbineEsts[TurbineCount - 1].grossAEP[k].usesFlowSep) {
                                    inLastTurbine = true;
                                    break;
                                }
                            }

                            if (inLastTurbine == false) {
                                turbineEsts[i].RemoveGrossAEP(j);
                                break;
                            }
                        }
                    }

                    while (turbineEsts[i].NetAEP_Count != numNetEsts)
                    {
                        for (int j = 0; j < turbineEsts[i].NetAEP_Count; j++)
                        {
                            inLastTurbine = false;
                            for (int k = 0; k < turbineEsts[TurbineCount - 1].NetAEP_Count; k++)
                            {
                                if (turbineEsts[i].netAEP[j].isCalibrated == turbineEsts[TurbineCount - 1].netAEP[k].isCalibrated &&
                                    turbineEsts[i].netAEP[j].useSRDH == turbineEsts[TurbineCount - 1].netAEP[k].useSRDH &&
                                    turbineEsts[i].netAEP[j].usesFlowSep == turbineEsts[TurbineCount - 1].netAEP[k].usesFlowSep &&
                                    wakeList.IsSameWakeModel(turbineEsts[i].netAEP[j].wakeModel, turbineEsts[TurbineCount - 1].netAEP[k].wakeModel)) {
                                    inLastTurbine = true;
                                    break;
                                }
                            }

                            if (inLastTurbine == false) {
                                turbineEsts[i].RemoveNetAEP(j);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void DeleteTurbine(string name)
        {
            // Deletes turbine object with specified name
            int newCount = TurbineCount - 1;

            if (newCount > 0)
            {
                Turbine[] tempList = new Turbine[newCount];   // Create list of radii that you//re keeping(so size one less than before)                
                int tempIndex = 0;

                for (int i = 0; i < TurbineCount; i++)
                {
                    if (turbineEsts[i].name != name) {
                        tempList[tempIndex] = turbineEsts[i];
                        tempIndex++;
                    }
                }

                turbineEsts = tempList;
            }
            else {
                turbineEsts = null;
                turbineCalcsDone = false;
            }

        }

        public void DeletePowerCurve(string name)
        {
            //  Deletes power curve with specified name
            int newCount = PowerCurveCount - 1;
            int newAEP_Count = 0;
            Turbine.Gross_Energy_Est[] grossTempAEP;
            Turbine.Net_Energy_Est[] netTempAEP;
            int tempIndex;
            PowerCurve[] tempList;

            if (newCount > 0) {
                tempList = new PowerCurve[newCount];
                tempIndex = 0;

                for (int i = 0; i < PowerCurveCount; i++) {
                    if (powerCurves[i].name != name) {
                        tempList[tempIndex] = powerCurves[i];
                        tempIndex++;
                    }
                }

                powerCurves = tempList;
            }
            else
                powerCurves = null;

            if (turbineCalcsDone == true)
            {
                // Now delete Gross and Net AEP calcs

                for (int i = 0; i < TurbineCount; i++)
                {
                    newAEP_Count = 0;
                    for (int j = 0; j < turbineEsts[i].GrossAEP_Count; j++)
                        if (turbineEsts[i].grossAEP[j].powerCurve.name != name)
                            newAEP_Count++;

                    if (newAEP_Count > 0)
                    {
                        grossTempAEP = new Turbine.Gross_Energy_Est[newAEP_Count];
                        tempIndex = 0;

                        for (int j = 0; j < turbineEsts[i].GrossAEP_Count; j++) {
                            if (turbineEsts[i].grossAEP[j].powerCurve.name != name) {
                                grossTempAEP[tempIndex] = turbineEsts[i].grossAEP[j];
                                tempIndex++;
                            }
                        }

                        turbineEsts[i].grossAEP = grossTempAEP;
                    }
                    else
                        turbineEsts[i].grossAEP = null;

                    newAEP_Count = 0;
                    for (int j = 0; j < turbineEsts[i].NetAEP_Count; j++)
                        if (turbineEsts[i].netAEP[j].wakeModel.powerCurve.name != name)
                            newAEP_Count++;

                    if (newAEP_Count > 0)
                    {
                        netTempAEP = new Turbine.Net_Energy_Est[newAEP_Count];
                        tempIndex = 0;

                        for (int j = 0; j < turbineEsts[i].NetAEP_Count; j++)
                        {
                            if (turbineEsts[i].grossAEP[j].powerCurve.name != name)
                            {
                                netTempAEP[tempIndex] = turbineEsts[i].netAEP[j];
                                tempIndex++;
                            }
                        }

                        turbineEsts[i].netAEP = netTempAEP;
                    }
                    else
                        turbineEsts[i].netAEP = null;

                }
            }

        }

        public void FindParamStats(Continuum thisInst, Turbine[] turbines, bool isCalibrated, int WD_Ind, int numWD)
        {
            // Calculates the average, st. dev., min and max of turbine wind speed estimates for specified WD sector and model (site-calibrated vs. default) and updates the textboxes on Gross Turb Ests tab

            double avg = FindAverage(turbines, isCalibrated, WD_Ind);
            double stdev = FindStdev(turbines, isCalibrated, WD_Ind);
            double min = FindMin(turbines, isCalibrated, WD_Ind);
            double max = FindMax(turbines, isCalibrated, WD_Ind);

            if (avg != 0)
            {
                thisInst.txtAvg.Text = Math.Round(avg, 3).ToString();
                thisInst.txtStDev.Text = Math.Round(stdev, 3).ToString();
                thisInst.txtMin.Text = Math.Round(min, 3).ToString();
                thisInst.txtMax.Text = Math.Round(max, 3).ToString();
                thisInst.txtCount.Text = TurbineCount.ToString();
            }
            else
            {
                thisInst.txtAvg.Text = "";
                thisInst.txtStDev.Text = "";
                thisInst.txtMin.Text = "";
                thisInst.txtMax.Text = "";
                thisInst.txtCount.Text = "";
            }

            if (thisInst.cboPowerCrvs.SelectedItem.ToString() != "No power Curves Imported")
            {
                string selectedPowerCurve = thisInst.cboPowerCrvs.SelectedItem.ToString();
                double avgAEP = FindAverageAEP(turbines, selectedPowerCurve, isCalibrated, WD_Ind);
                double stdevAEP = FindStdevAEP(turbines, selectedPowerCurve, isCalibrated, WD_Ind);
                double minAEP = FindMinAEP(turbines, selectedPowerCurve, isCalibrated, WD_Ind);
                double maxAEP = FindMaxAEP(turbines, selectedPowerCurve, isCalibrated, WD_Ind);

                if (avgAEP != 0) {
                    thisInst.txtAEPAvg.Text = Math.Round(avgAEP, 0).ToString();
                    thisInst.txtAEPSD.Text = Math.Round(stdevAEP, 0).ToString();
                    thisInst.txtAEPMin.Text = Math.Round(minAEP, 0).ToString();
                    thisInst.txtAEPMax.Text = Math.Round(maxAEP, 0).ToString();
                }
                else {
                    thisInst.txtAEPAvg.Clear();
                    thisInst.txtAEPSD.Clear();
                    thisInst.txtAEPMin.Clear();
                    thisInst.txtAEPMax.Clear();
                }
            }
            else {
                thisInst.txtAEPAvg.Clear();
                thisInst.txtAEPSD.Clear();
                thisInst.txtAEPMin.Clear();
                thisInst.txtAEPMax.Clear();
            }

        }        

        public double FindAverage(Turbine[] turbines, bool isCalibrated, int WD_Ind)
        {
            //  Calculates and returns the average wind speed for specified WD sector and model (site-calibrated vs. default)
            int avgWS_Index = 0;
            double avg = 0;            
            int dataCount = 0;
            int numWD;
            double thisEst;

            if (TurbineCount == 0)
                return 0;
            else {
                try
                {
                    numWD = turbines[0].avgWS_Est[0].sectorWS.Length;
                }
                catch 
                {
                    return 0;
                }
            }

            if (turbines == null)
                return avg;

            for (int i = 0; i < turbines.Length; i++)
            {
                for (int j = 0; j < turbines[i].AvgWSEst_Count; j++)
                {
                    if (turbines[i].avgWS_Est[j].isCalibrated == isCalibrated) {
                        avgWS_Index = j;
                        break;
                    }
                }

                try {
                    if (WD_Ind == numWD)
                        thisEst = turbines[i].avgWS_Est[avgWS_Index].WS;
                    else
                        thisEst = turbines[i].avgWS_Est[avgWS_Index].sectorWS[WD_Ind];

                    avg = avg + thisEst;
                    dataCount++;
                }
                catch  {
                    return avg;
                }

            }

            if (dataCount > 0) 
                avg = avg / dataCount;

            return avg;

        }

        public double FindAverageAEP(Turbine[] turbines, string powerCurve, bool isCalibrated, int WD_Ind)
        {
            // Calculates and returns the average gross AEP for specified WD sector and model (site-calibrated vs. default)
            double avg = 0;            
            int dataCount = 0;
            int numWD;

            double thisEst;

            if (turbines == null)
                return avg;

            try {
                numWD = turbines[0].avgWS_Est[0].sectorWS.Length;
            }
            catch  {
                return avg;
            }

            for (int i = 0; i < turbines.Length; i++)
            {
                thisEst = 0;
                for (int j = 0; j < turbines[i].GrossAEP_Count; j++)
                {
                    if (turbines[i].grossAEP[j].powerCurve.name == powerCurve && turbines[i].grossAEP[j].isCalibrated == isCalibrated)
                    {
                        if (WD_Ind == numWD)
                            thisEst = turbines[i].grossAEP[j].AEP;
                        else
                            thisEst = turbines[i].grossAEP[j].sectorEnergy[WD_Ind];

                        break;
                    }
                }

                if (thisEst > 0) {
                    avg = avg + thisEst;
                    dataCount++;
                }
            }

            if (dataCount > 0)
                avg = avg / dataCount;

            return avg;
        }

        public double FindMinAEP(Turbine[] turbines, string powerCurve, bool isCalibrated, int WD_Ind)
        {
            // Calculates and returns the minimum gross AEP for specified WD sector and model (site-calibrated vs. default)
            double min = 100000;
            double thisEst = 0;
            int numWD;

            if (turbines == null)
                return 0;

            try {
                numWD = turbines[0].avgWS_Est[0].sectorWS.Length;
            }
            catch  {
                return 0;
            }

            for (int i = 0; i < turbines.Length; i++)
            {
                for (int j = 0; j <= turbines[i].GrossAEP_Count - 1; j++)
                {
                    if (turbines[i].grossAEP[j].powerCurve.name == powerCurve && turbines[i].grossAEP[j].isCalibrated == isCalibrated)
                    {
                        if (WD_Ind == numWD)
                            thisEst = turbines[i].grossAEP[j].AEP;
                        else
                            thisEst = turbines[i].grossAEP[j].sectorEnergy[WD_Ind];
                    }
                }
                if (thisEst < min) min = thisEst;
            }

            return min;
        }

        public double FindMin(Turbine[] turbines, bool isCalibrated, int WD_Ind)
        {
            // Calculates and returns the minimum wind speed for specified WD sector and model (site-calibrated vs. default)
            double min = 1000;
            double thisWS;
            int avgWS_Index = 0;
            int numWD;

            if (turbines == null)
                return 0;

            try {
                numWD = turbines[0].avgWS_Est[0].sectorWS.Length;
            }
            catch  {
                return 0;
            }

            for (int i = 0; i < turbines.Length; i++)
            {
                for (int j = 0; j <= turbines[i].AvgWSEst_Count - 1; j++)
                {
                    if (turbines[i].avgWS_Est[j].isCalibrated == isCalibrated) {
                        avgWS_Index = j;
                        break;
                    }
                }

                try {
                    if (WD_Ind == numWD)
                        thisWS = turbines[i].avgWS_Est[avgWS_Index].WS;
                    else
                        thisWS = turbines[i].avgWS_Est[avgWS_Index].sectorWS[WD_Ind];

                    if (thisWS < min) min = thisWS;
                }
                catch  {
                    return 0;
                }

            }

            return min;

        }

        public double FindMaxAEP(Turbine[] turbines, string powerCurve, bool isCalibrated, int WD_Ind)
        {
            // Calculates and returns the maximum gross AEP for specified WD sector and model (site-calibrated vs. default)
            double max = 0;
            double thisEst = 0;
            int numWD;

            if (turbines == null)
                return 0;

            try {
                numWD = turbines[0].avgWS_Est[0].sectorWS.Length;
            }
            catch  {
                return 0;
            }

            for (int i = 0; i <= turbines.Length - 1; i++)
            {
                for (int j = 0; j <= turbines[i].GrossAEP_Count - 1; j++)
                {
                    if (turbines[i].grossAEP[j].powerCurve.name == powerCurve && turbines[i].grossAEP[j].isCalibrated == isCalibrated)
                    {
                        if (WD_Ind == numWD)
                            thisEst = turbines[i].grossAEP[j].AEP;
                        else
                            thisEst = turbines[i].grossAEP[j].sectorEnergy[WD_Ind];

                        break;
                    }
                }

                if (thisEst > max) max = thisEst;
            }

            return max;
        }

        public double FindMax(Turbine[] turbines, bool isCalibrated, int WD_Ind)
        {
            //  Calculates and returns the maximum wind speed for specified WD sector and model (site-calibrated vs. default)
            double max = 0;
            double thisWS;
            int avgWS_Index = 0;
            int numWD;

            if (turbines == null)
                return 0;

            try {
                numWD = turbines[0].avgWS_Est[0].sectorWS.Length;
            }
            catch  {
                return 0;
            }

            for (int i = 0; i <= turbines.Length - 1; i++)
            {
                for (int j = 0; j <= turbines[i].AvgWSEst_Count - 1; j++)
                {
                    if (turbines[i].avgWS_Est[j].isCalibrated == isCalibrated) {
                        avgWS_Index = j;
                        break;
                    }
                }

                try {
                    if (WD_Ind == numWD)
                        thisWS = turbines[i].avgWS_Est[avgWS_Index].WS;
                    else
                        thisWS = turbines[i].avgWS_Est[avgWS_Index].sectorWS[WD_Ind];

                    if (thisWS > max) max = thisWS;
                }
                catch  { }

            }

            return max;
        }

        public double FindStdevAEP(Turbine[] turbines, string powerCurve, bool isCalibrated, int WD_Ind)
        {
            //  Calculates and returns the st. deviation gross AEP for specified WD sector and model (site-calibrated vs. default)
            double avg = 0;
            double stdev = 0;
            int dataCount = 0;
            int numWD;

            if (turbines == null)
                return 0;

            try {
                numWD = turbines[0].avgWS_Est[0].sectorWS.Length;
            }
            catch  {
                return 0;
            }

            double thisEst;

            for (int i = 0; i < turbines.Length; i++)
            {
                thisEst = 0;
                for (int j = 0; j <= turbines[i].GrossAEP_Count - 1; j++)
                {
                    if (turbines[i].grossAEP[j].powerCurve.name == powerCurve && turbines[i].grossAEP[j].isCalibrated == isCalibrated)
                    {
                        if (WD_Ind == numWD)
                            thisEst = turbines[i].grossAEP[j].AEP;
                        else
                            thisEst = turbines[i].grossAEP[j].sectorEnergy[WD_Ind];

                        break;
                    }
                }

                if (thisEst > 0) {
                    avg = avg + thisEst;
                    stdev = stdev + Math.Pow(thisEst, 2);
                    dataCount++;
                }
            }

            if (dataCount > 0) {
                avg = avg / dataCount;
                stdev = Math.Pow((stdev / dataCount - Math.Pow(avg, 2)), 0.5);
            }

            return stdev;

        }

        public double FindStdev(Turbine[] turbines, bool isCalibrated, int WD_Ind)
        {
            //  Calculates and returns the st. deviation wind speed for specified WD sector and model (site-calibrated vs. default)
            double avg = 0;
            double stdev = 0;
            int dataCount = 0;
            int avgWS_Index = 0;
            double thisEst;
            int numWD;

            if (turbines == null)
                return 0;

            try {
                numWD = turbines[0].avgWS_Est[0].sectorWS.Length;
            }
            catch  {
                return 0;
            }

            for (int i = 0; i < turbines.Length; i++)
            {
                for (int j = 0; j <= turbines[i].AvgWSEst_Count - 1; j++)
                {
                    if (turbines[i].avgWS_Est[j].isCalibrated == isCalibrated) {
                        avgWS_Index = j;
                        break;
                    }
                }

                try {
                    if (WD_Ind == numWD)
                        thisEst = turbines[i].avgWS_Est[avgWS_Index].WS;
                    else
                        thisEst = turbines[i].avgWS_Est[avgWS_Index].sectorWS[WD_Ind];

                    avg = avg + thisEst;
                    stdev = stdev + Math.Pow(thisEst, 2);
                    dataCount++;
                }
                catch  {
                }

            }

            if (dataCount > 0) {
                avg = avg / dataCount;
                stdev = Math.Pow((stdev / dataCount - Math.Pow(avg, 2)), 0.5);
            }

            return stdev;
        }


        public double GetMinDistance(Turbine turbine, Model model, MetCollection metList)
        {
            // Calculates and returns the minimum distance between specified turbine and all met sites
            double minDistance = 100000;
            TopoInfo topo = new TopoInfo();
            int numMets = model.metsUsed.Length;

            for (int i = 0; i <= numMets - 1; i++)
                for (int j = 0; j <= metList.ThisCount - 1; j++)
                    if (model.metsUsed[i] == metList.metItem[j].name)
                    {
                        double thisDistance = topo.CalcDistanceBetweenPoints(turbine.UTMX, turbine.UTMY, metList.metItem[j].UTMX, metList.metItem[j].UTMY);

                        if (thisDistance < minDistance)
                            minDistance = thisDistance;

                        break;
                    }


            return minDistance;
        }

        public double GetMinDeltaP10Expo(Turbine turbine, Model[] models, MetCollection metList, int WD_Ind)
        {
            // Calculates and returns the minimum P10 exposure difference from turbine site to each predictor met
            double minP10DW_Diff = 10000;
            double minP10UW_Diff = 100000;
            
            double avgDiff = 0;
            int numRadii = models.Length;

            int numMets = 0;
            if (models[0].isImported == true)
                numMets = metList.ThisCount;
            else
                numMets = models[0].metsUsed.Length;

            Met[] metsUsed = new Met[numMets];

            for (int i = 0; i < numMets; i++)
            {
                for (int j = 0; j < metList.ThisCount; j++)
                {
                    if (models[0].isImported == true) {
                        metsUsed[i] = metList.metItem[j];
                        break;
                    }
                    else {
                        if (models[0].metsUsed[i] == metList.metItem[j].name) {
                            metsUsed[i] = metList.metItem[j];
                            break;
                        }
                    }

                }
            }

            for (int i = 0; i < numMets; i++) {
                for (int j = 0; j < numRadii; j++) {
                    if (Math.Abs(metsUsed[i].gridStats.stats[j].P10_UW[WD_Ind] - turbine.gridStats.stats[j].P10_UW[WD_Ind]) < minP10UW_Diff)
                        minP10UW_Diff = Math.Abs(metsUsed[i].gridStats.stats[j].P10_UW[WD_Ind] - turbine.gridStats.stats[j].P10_UW[WD_Ind]);

                    if (Math.Abs(metsUsed[i].gridStats.stats[j].P10_DW[WD_Ind] - turbine.gridStats.stats[j].P10_DW[WD_Ind]) < minP10DW_Diff)
                        minP10DW_Diff = Math.Abs(metsUsed[i].gridStats.stats[j].P10_DW[WD_Ind] - turbine.gridStats.stats[j].P10_DW[WD_Ind]);

                }
            }

            avgDiff = (minP10UW_Diff + minP10DW_Diff) / 2;

            return avgDiff;

        }

  /*      public double GetErrorEst(Turbine turbine, Continuum thisInst)
        {
            // Calculates and returns estimate of wind speed unceratinty at turbine site for specified model
            // Checks range of UW and Dw exposure in each sector (with a min. freq of 0.05) and compares UW and DW exposure at turbine site. if turbine is outside
            // the range of exposures at met site then it figures out if turbine is less than or greater than UW or DW exposure range and then finds the met site that
            // has the lowest/highest UW/DW exposure and then forms a site-calibrated model excluding that met site and calculates the wind speed est error of excluded met
            // and uses this as the uncertainty for turbine site
            double thisUncert = 0;
            double uncertCount = 0;
            double adjFact = 1.2f;
            
            NodeCollection nodeList = new NodeCollection();
            double[] windRose = thisInst.metList.GetAvgWindRose();

            int numWD = thisInst.metList.numWD;                        
            int numRadii = thisInst.radiiList.ThisCount;
            
            bool isImported = false;
            if (thisInst.modelList.models.GetUpperBound(0) > 0) // has default and at least one other model (i.e. either imported or site-calibrated
                if (thisInst.modelList.models[1, 0].isImported)
                    isImported = true;

            if (thisInst.metList.ThisCount <= 2 || isImported == true)
            {
                if (turbine.gridStats.GetOverallP10(windRose, 0, "DW") < 15)
                    thisUncert = 0.0167f * adjFact;
                else if (turbine.gridStats.GetOverallP10(windRose, 0, "DW") < 85)
                    thisUncert = 0.021f * adjFact;
                else
                    thisUncert = 0.03f * adjFact;
            }
            else
            {
                for (int i = 0; i < turbine.WSEst_Count; i++) // Go through each wind speed estimate, check if Turbine P10 expo is within bounds of model.
                                                                     // If it is, increment uncertainty average (thisUncert) by Model RMS error * WS_weight and uncertainty weights (uncertCount) 
                                                                     // If it's not, find out if exceedance occurred in uphill or downhill and min or max P10 expo and find the met with the lowest/highest uphill/downhill P10 expo
                                                                     // Omit that met and create a model. Then go through met cross-predictions, find the model that omits met, and increment uncertainty average by WS_Estimate_Error * WS_weight               
                {
                    Model thisModel = turbine.WS_Estimate[i].model;
                    int radiusIndex = thisInst.radiiList.GetRadiusInd(thisModel.radius);
                    int WD_exceed_ind = 100;
                    double overallP10DW = turbine.gridStats.GetOverallP10(turbine.windRose, radiusIndex, "DW");
                    string[] isInBounds = new string[2];

                    for (int WD = 0; WD < numWD; WD++)
                    {
                        if (windRose[WD] > 0.05f) // Only consider WD sectors that account for at least 5% of wind rose
                        {
                            double P10_UW = turbine.gridStats.stats[radiusIndex].P10_UW[WD];
                            double P10_DW = turbine.gridStats.stats[radiusIndex].P10_DW[WD];
                            double UW = turbine.expo[radiusIndex].expo[WD];
                            double DW = turbine.expo[radiusIndex].GetDW_Param(WD, "Expo");

                            string[] isThisInBounds = thisModel.IsSiteWithinMetLimitsUsedInModel(P10_UW, P10_DW, UW, DW, WD);

                            if (WD_exceed_ind == 100 && (isThisInBounds[0] != "OK" || isThisInBounds[1] != "OK"))
                            {
                                isInBounds = isThisInBounds;
                                WD_exceed_ind = WD;
                            }
                            else if (windRose[WD] > windRose[WD_exceed_ind] && (isThisInBounds[0] != "OK" || isThisInBounds[1] != "OK"))
                            {
                                isInBounds = isThisInBounds;
                                WD_exceed_ind = WD;
                            }
                        }
                    }

                    if (isInBounds[0] == "OK" && isInBounds[1] == "OK")
                    {
                        if (thisModel.metsUsed.Length == 1)
                        {
                            if (overallP10DW < 15)
                                thisUncert = thisUncert + (0.0167f * adjFact); // Simple terrain. Default uncertainties based on "Single Met Model" study . This should be revisisted.
                            else if (overallP10DW < 85)
                                thisUncert = thisUncert + (0.021f * adjFact); // Moderately complex
                            else
                                thisUncert = thisUncert + (0.03f * adjFact); // Complex                            
                        }
                        else                        
                            thisUncert = thisUncert + thisModel.RMS_WS_Est * turbine.WS_Estimate[i].WS_weight;  
                            
                    }
                    else if (thisModel.metsUsed.Length <= 3)
                    { // if there are three or fewer mets and a turbine exposure exceeds then use default uncertainties
                        if (overallP10DW < 15)
                            thisUncert = thisUncert + (0.0167f * adjFact) * turbine.WS_Estimate[i].WS_weight;
                        else if (overallP10DW < 85)
                            thisUncert = thisUncert + (0.021f * adjFact) * turbine.WS_Estimate[i].WS_weight;
                        else
                            thisUncert = thisUncert + (0.03f * adjFact) * turbine.WS_Estimate[i].WS_weight;
                        
                    }
                    else {  // Turbine is outside bounds, need to figure out what bounds are exceeded and then which met to omit                                              

                        Met metToOmit = null;
                        if (isInBounds[0] != "OK")
                            metToOmit = thisInst.metList.Get_Met_with_Min_or_Max_UH_or_DH_P10(WD_exceed_ind, "Min", isInBounds[0], thisModel);
                        else if (isInBounds[1] != "OK")
                            metToOmit = thisInst.metList.Get_Met_with_Min_or_Max_UH_or_DH_P10(WD_exceed_ind, "Max", isInBounds[1], thisModel);

                        string[] metToOmitStr = new string[1];
                        metToOmitStr[0] = metToOmit.name;
                        string[] metsToUse = thisInst.metList.GetMetsExceptThoseInList(metToOmitStr);
                        Met[] metsInModel = thisInst.metList.GetMets(metsToUse, null);

                        // Create model with met omitted (if not already created)
                        Model[] modelOmitOne = thisInst.modelList.CreateModel(metsToUse, thisInst);
                        Nodes metNode = nodeList.GetMetNode(metToOmit);
                        double[,] theseWgts = thisInst.modelList.GetWS_EstWeights(metsInModel, metNode, modelOmitOne, windRose);
                        int predInd = 0;
                        double thisErr = 0;
                        double errCount = 0;

                        // Search through met pairs for estimates with Met that was omitted from model
                        for (int m = 0; m < thisInst.metPairList.PairCount; m++)
                        {
                            Pair_Of_Mets thisPair = thisInst.metPairList.metPairs[m];
                            if ((thisPair.met1.name == metToOmit.name || thisPair.met2.name == metToOmit.name)) // either met1 or met2 is the met omitted from the model
                            {
                                for (int n = 0; n <= metsToUse.Length - 1; n++) {
                                    if ((thisPair.met1.name == metsToUse[n]) || (thisPair.met2.name == metsToUse[n])) {
                                        predInd = n; // This index corresponds to theseWgts indices
                                        break;
                                    }
                                }

                                for (int n = 0; n < thisPair.WS_PredCount; n++)
                                {
                                    if (thisInst.modelList.IsSameModel(thisPair.WS_Pred[n, radiusIndex].model, modelOmitOne[radiusIndex]))
                                    {
                                        if (thisPair.met1.name == metToOmit.name)
                                        {
                                            thisErr = thisErr + Math.Abs(thisPair.WS_Pred[n, radiusIndex].percErr[1]) * theseWgts[predInd, radiusIndex];
                                            errCount = errCount + theseWgts[predInd, radiusIndex];
                                            break;
                                        }
                                        else {
                                            thisErr = thisErr + Math.Abs(thisPair.WS_Pred[n, radiusIndex].percErr[0]) * theseWgts[predInd, radiusIndex];
                                            errCount = errCount + theseWgts[predInd, radiusIndex];
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        thisErr = thisErr / errCount;
                        thisUncert = thisUncert + thisErr * turbine.WS_Estimate[i].WS_weight;
                    }
                    uncertCount = uncertCount + turbine.WS_Estimate[i].WS_weight;
                }
            } 

            if (uncertCount > 0)
                thisUncert = (thisUncert / uncertCount);        

            return thisUncert;
        }
*/
        

        public Turbine GetTurbine(string turbineName)
        {
            // Returns Turbine objects for names listed
            Turbine thisTurbine = new Turbine();

            if (turbineName == "") return thisTurbine;

            for (int i = 0; i < turbineEsts.Length; i++)
            {
                if (turbineName == turbineEsts[i].name)
                {
                    thisTurbine = turbineEsts[i];
                    break;
                }
            }

            return thisTurbine;
        }

        public PowerCurve GetPowerCurve(string powerCurveName)
        {
            // returns power curve object
            PowerCurve thisPowerCurve = new PowerCurve();

            for (int i = 0; i < PowerCurveCount; i++)
                if (powerCurves[i].name == powerCurveName)
                {
                    thisPowerCurve = powerCurves[i];
                    break;
                }

            return thisPowerCurve;
        }       

    }
}
    
