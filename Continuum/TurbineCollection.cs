using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using MathNet.Numerics;
using System.IO;

namespace ContinuumNS
{
    [Serializable()]
    public class TurbineCollection
    {
        public Turbine[] turbineEsts;
        public bool turbineCalcsDone;
        public PowerCurve[] powerCurves;
        public Exceedance exceed; // List of Project Performance curves and composite P-values
        public bool genTimeSeries = false;

        [Serializable()] public struct PowerCurve
        {
            public string name;
            public double cutInWS;
            public double cutOutWS;
            public double ratedPower;
            public double ratedWS;
            public double[] power;
            public double RD;
            public double[] thrustCoeff;
            public double ratedRPM;
            public double wsInt; // Wind speed interval
            public double firstWS; // first WS in power and thrust curves

            public void Clear()
            {
                name = "";
                cutInWS = 0;
                cutOutWS = 0;
                ratedPower = 0;
                ratedWS = 0;
                power = null;
                RD = 0;
                thrustCoeff = null;
                ratedRPM = 0;
            }
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

            turbineCalcsDone = false;
        }

        public void ClearAllNetEsts() {
            //  Clears all net AEP estimates formed at turbine sites
            for (int i = 0; i < TurbineCount; i++)
            {
                turbineEsts[i].netAEP = null;

                // Clear all wake estimates from avg WS estimates
                for (int j = 0; j < turbineEsts[i].AvgWSEst_Count; j++)
                {
                    if (turbineEsts[i].avgWS_Est[j].haveNetTS)
                    {
                        for (int t = 0; t < turbineEsts[i].avgWS_Est[j].timeSeries.Length; t++)
                        {
                            turbineEsts[i].avgWS_Est[j].timeSeries[t].netEnergy = 0;
                            turbineEsts[i].avgWS_Est[j].timeSeries[t].wakedWS = 0;
                        }

                        turbineEsts[i].avgWS_Est[j].haveNetTS = false;
                    }
                }
                
                // Clear all avg WS estimates with same power curve
                Turbine.Avg_Est[] newAvgWS_Ests = null;
                int newNum = 0;

                for (int j = 0; j < turbineEsts[i].AvgWSEst_Count; j++)
                {
                    bool isDuplicate = false;

                    for (int k = 0; k < newNum; k++)
                    {
                        if (turbineEsts[i].avgWS_Est[j].powerCurve.name == newAvgWS_Ests[k].powerCurve.name)
                            isDuplicate = true;
                    }
                    
                    if (isDuplicate == false)
                    {
                        newNum++;
                        Array.Resize(ref newAvgWS_Ests, newNum);
                        newAvgWS_Ests[newNum - 1] = turbineEsts[i].avgWS_Est[j];
                    }                    
                }

                turbineEsts[i].avgWS_Est = newAvgWS_Ests;
            }
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
            }

            turbineCalcsDone = false;

        }

        public void AddPowerCurve(string name, double cutIn, double cutOut, double ratedPower, double[] powerValues, double[] thrustValues, double RD, 
            double ratedRPM, double ratedWS, double WS_IntSize, double firstWS)
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
            powerCurves[newCount].ratedRPM = ratedRPM;
            powerCurves[newCount].ratedWS = ratedWS;
            powerCurves[newCount].wsInt = WS_IntSize;
            powerCurves[newCount].firstWS = firstWS;
        }

        public void ReadPowerCurve(Continuum thisInst, string name, double[,] importedPower, double RD, double RPM)
        {
            //  Imports new power curve and adds to list                    
            double cutIn = importedPower[0, 0]; // First wind
            double cutOut = 0;
            double ratedPower = 0;
            double ratedWS = 0;
            double firstWS = 0;
            double wsInterval = 0;
                        
            int numWS_Import = importedPower.GetUpperBound(1) + 1;

            if (numWS_Import > 0)
                firstWS = importedPower[0, 0];

            if (numWS_Import > 1)
                wsInterval = importedPower[0, 1] - importedPower[0, 0];                                  

            // Fill in blanks between cut-in and cut-out WS
            for (int i = 0; i < numWS_Import; i++) {
                if (importedPower[1, i] == 0 && importedPower[1, i + 1] > 0) {
                    cutIn = importedPower[0, i + 1];
                    break;
                }
            }

            for (int i = numWS_Import - 1; i >= 0; i--)
            {
                if (i > 0)
                {
                    if (importedPower[1, i] == 0 && importedPower[1, i - 1] > 0)
                    {
                        cutOut = importedPower[0, i - 1];
                        break;
                    }
                }
                else
                    cutOut = importedPower[0, numWS_Import - 1];
            }

            for (int i = 0; i < numWS_Import; i++)
                if (importedPower[1, i] > ratedPower)
                {
                    ratedPower = importedPower[1, i];
                    ratedWS = importedPower[0, i];
                }                        

            for (int i = 0; i < numWS_Import; i++)
                if (importedPower[0, i] > cutIn && importedPower[0, i] < cutOut && importedPower[1, i] == 0)
                    importedPower[1, i] = ratedPower;

            double[] powerOnly = new double[numWS_Import];
            double[] thrustOnly = new double[numWS_Import];

            for (int i = 0; i < numWS_Import; i++) {
                powerOnly[i] = importedPower[1, i];
                thrustOnly[i] = importedPower[2, i];
            }
                        
            AddPowerCurve(name, cutIn, cutOut, ratedPower, powerOnly, thrustOnly, RD, RPM, ratedWS, wsInterval, firstWS);                        

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
                    turbineEsts[i].AddExposure(radius, exponent, numSectors, thisInst.metList.numWD);
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
            if (thisInst.topo.gotTopo == false || GotEst("Expo", new TurbineCollection.PowerCurve(), null) == false)
                return;

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
            int smallerRadius = thisInst.topo.GetSmallerRadius(turbineEsts[turbineIndex].expo, turbineEsts[turbineIndex].expo[expoIndex].radius, turbineEsts[turbineIndex].expo[expoIndex].exponent, numSectors);

            if (smallerRadius == 0 || numSectors > 1)  // when sector avg is used, can//t add on to exposure calcs...so gotta do it the long way
                turbineEsts[turbineIndex].expo[expoIndex] = thisInst.topo.CalcExposures(turbineEsts[turbineIndex].UTMX, turbineEsts[turbineIndex].UTMY,
                                turbineEsts[turbineIndex].elev, turbineEsts[turbineIndex].expo[expoIndex].radius, turbineEsts[turbineIndex].expo[expoIndex].exponent, numSectors, thisInst.topo, numWD);
            else {
                Exposure smallerExposure = thisInst.topo.GetSmallerRadiusExpo(turbineEsts[turbineIndex].expo, smallerRadius, turbineEsts[turbineIndex].expo[expoIndex].exponent, numSectors);

                turbineEsts[turbineIndex].expo[expoIndex] = thisInst.topo.CalcExposuresWithSmallerRadius(turbineEsts[turbineIndex].UTMX, turbineEsts[turbineIndex].UTMY,
                                turbineEsts[turbineIndex].elev, turbineEsts[turbineIndex].expo[expoIndex].radius, turbineEsts[turbineIndex].expo[expoIndex].exponent, numSectors, smallerRadius, smallerExposure, numWD);
            }

            if (turbineEsts[turbineIndex].expo[expoIndex].UW_P10CrossGrade == null)
            {
                // Calc P10 UW Crosswind Grade
                double UW_Grade = 0;
                for (expoIndex = 0; expoIndex < thisInst.radiiList.ThisCount; expoIndex++)
                    turbineEsts[turbineIndex].expo[expoIndex].UW_P10CrossGrade = new double[thisInst.metList.numWD];

                for (int r = 0; r < thisInst.metList.numWD; r++) {
                    UW_Grade = thisInst.topo.CalcP10_UW_CrosswindGrade(turbineEsts[turbineIndex].UTMX, turbineEsts[turbineIndex].UTMY, thisInst.radiiList, r, thisInst.metList.numWD);
                    for (expoIndex = 0; expoIndex <= thisInst.radiiList.ThisCount - 1; expoIndex++)
                        turbineEsts[turbineIndex].expo[expoIndex].UW_P10CrossGrade[r] = UW_Grade;

                }
            }
        }

        public void CalcGrossAEPFromTABs(Continuum thisInst, bool isCalibrated)
        {
            // Calculates the gross AEP for every turbine and every power curve for either the site-calibrated or default model
            if (thisInst.metList.numWD == 0) return;
            int numWD = thisInst.metList.numWD;
            int numWS = thisInst.metList.numWS;

          //  if (thisInst.modelList.ModelCount <= 1 && isCalibrated == false)
           //     return;
            
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

                    Turbine.Avg_Est avgEst = turbineEsts[j].GetAvgWS_Est(null, new PowerCurve());

                    if (avgEst.freeStream.WS == 0)
                        break;

                    if (alreadyCalc == false)
                    {
                        double[,] sectorDist = new double[numWD, numWS];

                        for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++) {
                            double P90_WS = avgEst.freeStream.sectorWS[WD_Ind] - avgEst.freeStream.sectorWS[WD_Ind] * 0.0128155f;
                            double[] WS_Dist = thisInst.metList.CalcWS_DistForTurbOrMap(metsUsed, P90_WS, WD_Ind, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
                            for (int WS_ind = 0; WS_ind <= numWS - 1; WS_ind++)
                                sectorDist[WD_Ind, WS_ind] = WS_Dist[WS_ind];
                        }

                        double[] P90_Dist = thisInst.metList.CalcOverallWS_Dist(sectorDist, thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, Met.TOD.All, Met.Season.All));

                        for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++) {
                            double P99_WS = avgEst.freeStream.sectorWS[WD_Ind] - avgEst.freeStream.sectorWS[WD_Ind] * 0.02326f;
                            double[] WS_Dist = thisInst.metList.CalcWS_DistForTurbOrMap(metsUsed, P99_WS, WD_Ind, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
                            for (int WS_ind = 0; WS_ind <= numWS - 1; WS_ind++)
                                sectorDist[WD_Ind, WS_ind] = WS_Dist[WS_ind];
                        }

                        double[] P99_Dist = thisInst.metList.CalcOverallWS_Dist(sectorDist, thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, Met.TOD.All, Met.Season.All));

                        double P90_AEP = CalcAndReturnGrossAEP(P90_Dist, thisInst.metList, powerCurves[i].name);
                        double P99_AEP = CalcAndReturnGrossAEP(P99_Dist, thisInst.metList, powerCurves[i].name);
                        double P50_AEP = CalcAndReturnGrossAEP(avgEst.freeStream.WS_Dist, thisInst.metList, powerCurves[i].name);                                             

                        for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                        {
                            double[] ThisDist = new double[numWS];
                            double[] windRose = thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                            for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
                                ThisDist[WS_ind] = avgEst.freeStream.sectorWS_Dist[WD_Ind, WS_ind];
                            P50_AEP_Sect[WD_Ind] = CalcAndReturnGrossAEP(ThisDist, thisInst.metList, powerCurves[i].name) * windRose[WD_Ind];
                        }                           
                     
                        double This_CF = CalcCapacityFactor(P50_AEP, powerCurves[i].ratedPower);
                        turbineEsts[j].AddGrossAEP(thisInst, powerCurves[i], P50_AEP, This_CF, P90_AEP, P99_AEP, isCalibrated, P50_AEP_Sect);
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

        public double CalcAndReturnGrossAEP(double[] WS_Dist, MetCollection metList, string powerCurve)
        {
            // Calculates and returns gross AEP (in MWh) calculated from specified WS_Dist and power_crv
            double thisAEP = 0;
            int numWS = WS_Dist.Length;
            
            for (int i = 0; i < PowerCurveCount; i++)
            {
                PowerCurve thisPowerCurve = powerCurves[i];
                if (thisPowerCurve.name == powerCurve) {
                    for (int k = 0; k < numWS; k++)
                    {
                        double thisWS = metList.GetWS_atWS_Ind(k);
                        double thisPower = GetInterpPowerOrThrust(thisWS, thisPowerCurve, "Power");
                        thisAEP = thisAEP + WS_Dist[k] * thisPower;
                    }
                        
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
                                if ((turbineEsts[i].avgWS_Est[j].powerCurve.name == turbineEsts[TurbineCount - 1].avgWS_Est[k].powerCurve.name) &&
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
                        if (turbineEsts[i].netAEP[j].wakeModel.powerCurve.name != name)
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
                    numWD = turbines[0].avgWS_Est[0].freeStream.sectorWS.Length;
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
                Turbine.Avg_Est avgEst = turbines[i].GetAvgWS_Est(null, new PowerCurve());

                try {
                    if (WD_Ind == numWD)
                        thisEst = avgEst.freeStream.WS;
                    else
                        thisEst = avgEst.freeStream.sectorWS[WD_Ind];

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
                numWD = turbines[0].avgWS_Est[0].freeStream.sectorWS.Length;
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
                numWD = turbines[0].avgWS_Est[0].freeStream.sectorWS.Length;
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
            // Calculates and returns the minimum wind speed for specified WD sector 
            double min = 1000;
            double thisWS;
            
            int numWD;

            if (turbines == null)
                return 0;

            try {
                numWD = turbines[0].avgWS_Est[0].freeStream.sectorWS.Length;
            }
            catch  {
                return 0;
            }

            for (int i = 0; i < turbines.Length; i++)
            {
                Turbine.Avg_Est avgEst = turbines[i].GetAvgWS_Est(null, new PowerCurve());

                try {
                    if (WD_Ind == numWD)
                        thisWS = avgEst.freeStream.WS;
                    else
                        thisWS = avgEst.freeStream.sectorWS[WD_Ind];

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
                numWD = turbines[0].avgWS_Est[0].freeStream.sectorWS.Length;
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
                numWD = turbines[0].avgWS_Est[0].freeStream.sectorWS.Length;
            }
            catch  {
                return 0;
            }

            for (int i = 0; i <= turbines.Length - 1; i++)
            {
                Turbine.Avg_Est avgEst = turbines[i].GetAvgWS_Est(null, new PowerCurve());

                try {
                    if (WD_Ind == numWD)
                        thisWS = avgEst.freeStream.WS;
                    else
                        thisWS = avgEst.freeStream.sectorWS[WD_Ind];

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
                numWD = turbines[0].avgWS_Est[0].freeStream.sectorWS.Length;
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
                numWD = turbines[0].avgWS_Est[0].freeStream.sectorWS.Length;
            }
            catch  {
                return 0;
            }

            for (int i = 0; i < turbines.Length; i++)
            {
                Turbine.Avg_Est avgEst = turbines[i].GetAvgWS_Est(null, new PowerCurve());

                try {
                    if (WD_Ind == numWD)
                        thisEst = avgEst.freeStream.WS;
                    else
                        thisEst = avgEst.freeStream.sectorWS[WD_Ind];

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

        public double GetInterpPowerOrThrust(double thisWS, PowerCurve thisPowerCurve, string powerOrThrust)
        {
            double thisVal = 0;
            
            if (thisWS > thisPowerCurve.ratedWS && powerOrThrust == "Power")
                thisVal = thisPowerCurve.ratedPower;
            else if (thisWS < thisPowerCurve.cutInWS)
                thisVal = 0;
            else if (thisWS > thisPowerCurve.cutOutWS)
                thisVal = 0;
            else
            {
                double[] WS_Array = new double[3];

                int middleWSInd = (int)Math.Round((thisWS - thisPowerCurve.firstWS) / thisPowerCurve.wsInt, 0);
                double middleWS = thisPowerCurve.firstWS + middleWSInd * thisPowerCurve.wsInt;

                if (powerOrThrust == "Power" && middleWS + thisPowerCurve.wsInt > thisPowerCurve.ratedWS)
                {
                    middleWS = (int)(thisPowerCurve.ratedWS - thisPowerCurve.wsInt);
                    middleWSInd = middleWSInd - 1;
                }

                if (middleWS == thisPowerCurve.cutInWS)
                {
                    middleWS = middleWS + thisPowerCurve.wsInt;
                    middleWSInd = middleWSInd + 1;
                }

                if ((middleWSInd + 1) >= thisPowerCurve.power.Length)
                {
                    middleWSInd = thisPowerCurve.power.Length - 2;
                    middleWS = thisPowerCurve.firstWS + middleWSInd * thisPowerCurve.wsInt;
                }

                WS_Array[0] = middleWS - thisPowerCurve.wsInt;
                WS_Array[1] = middleWS;
                WS_Array[2] = middleWS + thisPowerCurve.wsInt;

                if (powerOrThrust == "Power")
                {
                    double[] powerArray = new double[3];

                    powerArray[0] = thisPowerCurve.power[middleWSInd - 1];
                    powerArray[1] = thisPowerCurve.power[middleWSInd];
                    powerArray[2] = thisPowerCurve.power[middleWSInd + 1];

                    Polynomial thisPoly = Polynomial.Fit(WS_Array, powerArray, 2, MathNet.Numerics.LinearRegression.DirectRegressionMethod.NormalEquations);
                    thisVal = thisPoly.Evaluate(thisWS);
                }
                else if (powerOrThrust == "Thrust")
                {
                    double[] thrustArray = new double[3];
                    thrustArray[0] = thisPowerCurve.thrustCoeff[middleWSInd - 1];
                    thrustArray[1] = thisPowerCurve.thrustCoeff[middleWSInd];
                    thrustArray[2] = thisPowerCurve.thrustCoeff[middleWSInd + 1];

                    if (thrustArray[0] == thrustArray[1])
                        thisVal = thrustArray[0];
                    else
                    {
                        Polynomial thisPoly = Polynomial.Fit(WS_Array, thrustArray, 2, MathNet.Numerics.LinearRegression.DirectRegressionMethod.NormalEquations);
                        thisVal = thisPoly.Evaluate(thisWS);
                    }
                }

            }

            return thisVal;
        }

        public void ImportPowerCurve(Continuum thisInst, double RD, double RPM)
        {
            // Prompts user to open a power curve. thisPowerCurve file and updates turbine calcs if they were done before
            
            int WS_Ind = 0;
            double[,] powerCurve = new double[3, WS_Ind + 1];

            string wholePath = thisInst.ofdPowerCurve.FileName;

            string[] fileRow;
            string fileName = thisInst.ofdPowerCurve.FileName;

            StreamReader sr = new StreamReader(fileName);

            char[] delims = new char[2];
            delims[0] = '\t';
            delims[1] = ',';

            // Read in power curve name               
            string powerCurveName = sr.ReadLine();
            fileRow = powerCurveName.Split(delims);
            bool gotFirstWS = false;

            if (fileRow != null)
                if (fileRow.Length == 3 && fileRow[1] != "" & fileRow[2] != "") // Read in first wind speed of file
                {
                    int lastSlash = fileName.LastIndexOf('\\');
                    powerCurveName = fileName.Substring(lastSlash + 1, fileName.Length - lastSlash - 5);

                    thisInst.ResizeArray(ref powerCurve, 3, WS_Ind + 1);
                    powerCurve[0, WS_Ind] = Convert.ToSingle(fileRow[0]); // Wind speed
                    powerCurve[1, WS_Ind] = Convert.ToSingle(fileRow[1]); // Power 
                    powerCurve[2, WS_Ind] = Convert.ToSingle(fileRow[2]); // Thrust
                    WS_Ind++;

                    gotFirstWS = true;
                }

            // Make sure power curve hasn't already been entered                
            int numPowerCurves = PowerCurveCount;

            for (int i = 0; i < numPowerCurves; i++)
            {
                if (powerCurves[i].name == powerCurveName)
                {
                    MessageBox.Show("A power curve of the same name has already been imported.", "Continuum 3");
                    sr.Close();
                    return;
                }
            }

            while (sr.EndOfStream == false)
            {
                // Read in Met name
                if (gotFirstWS == false || WS_Ind > 0)
                {
                    string dataStr = sr.ReadLine();
                    fileRow = dataStr.Split(delims);
                }

                if (fileRow.Length >= 3)
                {
                    try
                    {                            
                        thisInst.ResizeArray(ref powerCurve, 3, WS_Ind + 1);
                        powerCurve[0, WS_Ind] = Convert.ToSingle(fileRow[0]); // Wind speed
                        powerCurve[1, WS_Ind] = Convert.ToSingle(fileRow[1]); // Power 
                        powerCurve[2, WS_Ind] = Convert.ToSingle(fileRow[2]); // Thrust
                        WS_Ind++;
                            
                    }
                    catch
                    {
                        MessageBox.Show("Error while importing power curve file.  Please check your file.", "Continuum 3");
                        sr.Close();
                        return;
                    }
                }
                else if (fileRow.Length == 2)
                {
                    MessageBox.Show("Power curve files must include wind speed, power and thrust coefficients. Please check your file.", "Continuum 3");
                    sr.Close();
                    return;
                }
            }

            sr.Close();

            ReadPowerCurve(thisInst, powerCurveName, powerCurve, RD, RPM);

    /*           BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

            argsForBW.thisInst = thisInst;
            argsForBW.thisWakeModel = null;

            // Call background worker to run calculations
            thisInst.BW_worker = new BackgroundWork();
            string MCP_Method = thisInst.Get_MCP_Method();
            argsForBW.MCP_Method = MCP_Method;
            if (thisInst.topo.gotTopo)
                thisInst.BW_worker.Call_BW_TurbCalcs(argsForBW);
            else
                thisInst.updateThe.AllTABs(thisInst);
*/
            thisInst.ChangesMade();
            thisInst.turbineList.AreTurbCalcsDone(thisInst);
            thisInst.updateThe.AllTABs(thisInst);
        }        

        public void ClearDuplicateAvgWS(bool isTimeSeries)
        {
            // Deletes any duplicate or unneeded AvgWSEsts
            
            for (int i = 0; i < TurbineCount; i++)
            {
                for (int j = 0; j < turbineEsts[i].AvgWSEst_Count; j++)
                {
                    Turbine.Avg_Est avgEst = turbineEsts[i].avgWS_Est[j];

                    for (int k = j + 1; k < turbineEsts[i].AvgWSEst_Count; k++)
                    {
                        Turbine.Avg_Est nextEst = turbineEsts[i].avgWS_Est[k];
                        if (isTimeSeries)
                        {
                            if (avgEst.haveNetTS == false && nextEst.haveNetTS == false && avgEst.haveGrossTS == false && nextEst.haveGrossTS == false)
                            {
                                turbineEsts[i].RemoveAvgWS(k);
                                break;
                            }
                        }
                        else
                        {
                            if (avgEst.wakeModel != null && nextEst.wakeModel == null)
                            {
                                turbineEsts[i].RemoveAvgWS(k);
                                break;
                            }
                            else if (avgEst.wakeModel == null && nextEst.wakeModel != null)
                            {
                                turbineEsts[i].RemoveAvgWS(j);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void AreTurbCalcsDone(Continuum thisInst)
        {
            // Checks the state of the turbine ests compared to what power curves and wake models have been created
            // If turbine calcs not complete then sets turbineCalcsDone = false
            
            if (TurbineCount == 0)
            {
                turbineCalcsDone = false;
                return;
            }

            bool isCalibrated = false;
            if (thisInst.metList.ThisCount > 1) isCalibrated = true;

            if (thisInst.metList.isTimeSeries == false || thisInst.turbineList.genTimeSeries == false)
            {
                if (thisInst.turbineList.PowerCurveCount == 0)
                {
                    if (turbineEsts[0].AvgWSEst_Count == 0)
                        turbineCalcsDone = false;
                    else if (turbineEsts[0].avgWS_Est[0].freeStream.WS == 0)
                        turbineCalcsDone = false;
                    else
                        turbineCalcsDone = true;
                }
                else if (thisInst.turbineList.PowerCurveCount > 0 && thisInst.wakeModelList.NumWakeModels == 0)
                {
                    turbineCalcsDone = true;

                    for (int i = 0; i < thisInst.turbineList.PowerCurveCount; i++)
                    {
                        TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[i];
                        Turbine.Gross_Energy_Est grossEst = turbineEsts[0].GetGrossEnergyEst(isCalibrated, powerCurve);

                        if (grossEst.AEP == 0)
                            turbineCalcsDone = false;
                    }
                }
                else if (thisInst.turbineList.PowerCurveCount > 0 && thisInst.wakeModelList.NumWakeModels > 0)
                {
                    turbineCalcsDone = true;

                    for (int i = 0; i < thisInst.wakeModelList.NumWakeModels; i++)
                    {
                        Wake_Model wakeModel = thisInst.wakeModelList.wakeModels[i];
                        Turbine.Net_Energy_Est netEst = turbineEsts[0].GetNetEnergyEst(isCalibrated, wakeModel);

                        if (netEst.AEP == 0)
                            turbineCalcsDone = false;
                    }
                }
            }
            else // isTimeSeries == true and genTimeSeries == true
            {
                if (thisInst.turbineList.PowerCurveCount == 0)
                {
                    if (turbineEsts[0].AvgWSEst_Count == 0)
                        turbineCalcsDone = false;                    
                    else if (turbineEsts[0].avgWS_Est[0].timeSeries == null)
                        turbineCalcsDone = false;
                    else if (turbineEsts[0].avgWS_Est[0].timeSeries[0].freeStreamWS == 0)
                        turbineCalcsDone = false;
                    else
                        turbineCalcsDone = true;
                }
                else if (thisInst.turbineList.PowerCurveCount > 0 && thisInst.wakeModelList.NumWakeModels == 0)
                {
                    turbineCalcsDone = true;

                    for (int i = 0; i < thisInst.turbineList.PowerCurveCount; i++)
                    {
                        TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[i];
                        bool haveEst = turbineEsts[0].HaveTS_Estimate("Gross", isCalibrated, null, powerCurve);
                        if (haveEst == false)
                            turbineCalcsDone = false;
                    }
                }
                else if (thisInst.turbineList.PowerCurveCount > 0 && thisInst.wakeModelList.NumWakeModels > 0)
                {
                    turbineCalcsDone = true;

                    for (int i = 0; i < thisInst.turbineList.PowerCurveCount; i++)
                    {
                        TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[i];
                        bool haveEst = turbineEsts[0].HaveTS_Estimate("Gross", isCalibrated, null, powerCurve);
                        if (haveEst == false)
                            turbineCalcsDone = false;
                    }

                    for (int i = 0; i < thisInst.wakeModelList.NumWakeModels; i++)
                    {
                        Wake_Model wakeModel = thisInst.wakeModelList.wakeModels[i];
                        bool haveEst = turbineEsts[0].HaveTS_Estimate("Net", isCalibrated, wakeModel, wakeModel.powerCurve);
                        if (haveEst == false)
                            turbineCalcsDone = false;
                    }

                }
            }
        }

        public bool GotEst(string estType, PowerCurve powerCurve, Wake_Model wakeModel)
        {
            // Returns true if calculations have been completed with specified power curve and wake model. estType = Expo, WS, Gross, Net

            bool haveEst = false;
            
            if (TurbineCount == 0)
                return haveEst;

            Turbine thisTurb = turbineEsts[0];

            if (estType == "Expo")
            {
                if (thisTurb.ExposureCount > 0)
                    haveEst = true;
            }
            else if (estType == "WS")
            {
                for (int i = 0; i < thisTurb.AvgWSEst_Count; i++)
                    if (thisTurb.avgWS_Est[i].freeStream.WS != 0)
                        haveEst = true;
            }
            else if (estType == "Gross")
            {
                for (int i = 0; i < thisTurb.GrossAEP_Count; i++)
                    if (thisTurb.grossAEP[i].AEP != 0 && thisTurb.grossAEP[i].powerCurve.name == powerCurve.name)
                        haveEst = true;
            }
            else if (estType == "Net")
            {
                WakeCollection wakeList = new WakeCollection();

                for (int i = 0; i < thisTurb.NetAEP_Count; i++)
                    if (thisTurb.netAEP[i].AEP != 0 && wakeList.IsSameWakeModel(wakeModel, thisTurb.netAEP[i].wakeModel))
                        haveEst = true;
            }
            
            return haveEst;
        }

        public void ClearAllExceedance()
        {
            // Clears all exceedance calculations from all turbines                                       
            if (exceed != null)
                exceed.SizeMonteCarloArrays();
            
        }

        public void SetExceedCurves()
        {
            // Define exceedance curves if doesn't exist yet
            if (exceed == null)
            {
                exceed = new Exceedance();
                exceed.compositeLoss.isComplete = false;
                exceed.CreateDefaultCurve();
            }
        }

        public double[,] CalcProbOfWakeForEffectiveTI(Continuum thisInst, double thisX, double thisY, TurbineCollection.PowerCurve powerCurve)
        {
            // Calculates and returns the probability of each turbine creating a wake at specified UTMX and Y
            // Used in effective TI calculations

            double[,] probWake = new double[TurbineCount, thisInst.metList.numWD]; // i = Turbine count, j = WD

            // Go through each wind direction
            for (int WD_Ind = 0; WD_Ind < thisInst.metList.numWD; WD_Ind++)
            {
                double minWD = WD_Ind * (360.0 / thisInst.metList.numWD) - (360.0 / thisInst.metList.numWD / 2);                
                double maxWD = minWD + (360.0 / thisInst.metList.numWD);
                
                double[] edgeMin = new double[TurbineCount];
                double[] edgeMax = new double[TurbineCount];

                // Go through each turbine and see if it's upwind
                for (int i = 0; i < TurbineCount; i++)
                {
                    Turbine thisTurb = turbineEsts[i];
                    double distance = thisInst.topo.CalcDistanceBetweenPoints(thisTurb.UTMX, thisTurb.UTMY, thisX, thisY);

                    if (distance > 0 && (distance / powerCurve.RD) < 10)
                    {
                        double turbOrient = 90 - 180 / Math.PI * Math.Atan2(thisTurb.UTMY - thisY, thisTurb.UTMX - thisX);
                        if (turbOrient < 0) turbOrient = turbOrient + 360;

                        double edgeOrient1 = turbOrient - 180 / Math.PI * Math.Atan2(powerCurve.RD / 2, distance);
                        double edgeOrient2 = turbOrient + 180 / Math.PI * Math.Atan2(powerCurve.RD / 2, distance);

                        if (WD_Ind == 0 && edgeOrient1 > 270) edgeOrient1 = edgeOrient1 - 360;
                        if (WD_Ind == 0 && edgeOrient2 > 270) edgeOrient2 = edgeOrient2 - 360;

                        if ((edgeOrient1 >= minWD && edgeOrient1 <= maxWD) || (edgeOrient2 >= minWD && edgeOrient2 <= maxWD)
                            || (turbOrient >= minWD && turbOrient <= maxWD) || (edgeOrient1 <= minWD && edgeOrient2 >= maxWD))                            
                        {
                            if (edgeOrient1 < minWD) edgeOrient1 = minWD;
                            if (edgeOrient2 > maxWD) edgeOrient2 = maxWD;

                            edgeMin[i] = edgeOrient1;
                            edgeMax[i] = edgeOrient2;
                            probWake[i, WD_Ind] = (edgeMax[i] - edgeMin[i]) / (360.0 / thisInst.metList.numWD);
                        }
                        else
                        {
                            edgeMin[i] = -999;
                            edgeMax[i] = -999;
                            probWake[i, WD_Ind] = 0;
                        }

                    }
                    else
                    {
                        edgeMin[i] = -999;
                        edgeMax[i] = -999;
                        probWake[i, WD_Ind] = 0;
                    }
                }

                // Now go through and take out any 'hidden' turbines
                for (int i = 0; i < TurbineCount; i++)
                {
                    Turbine turb1 = turbineEsts[i];
                    double dist1 = thisInst.topo.CalcDistanceBetweenPoints(turb1.UTMX, turb1.UTMY, thisX, thisY);

                    for (int j = 0; j < TurbineCount; j++)
                    {
                        Turbine turb2 = turbineEsts[j];
                        if (i != j)
                        {
                            double dist2 = thisInst.topo.CalcDistanceBetweenPoints(turb2.UTMX, turb2.UTMY, thisX, thisY);

                            if (dist1 < dist2 && edgeMax[i] > edgeMin[i] && edgeMax[i] != -999 && edgeMin[i] != -999 && edgeMax[j] != -999 && edgeMin[j] != -999)
                            {
                                if (edgeMax[i] >= edgeMax[j] && edgeMin[i] <= edgeMin[j])
                                {
                                    edgeMin[j] = -999;
                                    edgeMax[j] = -999;
                                    probWake[j, WD_Ind] = 0;
                                }
                            }
                            else if (dist1 < dist2 && edgeMin[i] > edgeMax[i] && edgeMin[i] != -999 && edgeMax[i] != -999 && edgeMin[j] != -999 && edgeMax[j] != -999)
                            {
                                if (edgeMin[i] <= edgeMin[j] && edgeMax[i] >= edgeMax[j])
                                {
                                    edgeMin[j] = -999;
                                    edgeMax[j] = -999;
                                    probWake[j, WD_Ind] = 0;
                                }
                            }
                        }
                        
                    }
                }

                double totalWake = 0;
                for (int i = 0; i < TurbineCount; i++)
                    totalWake = totalWake + (edgeMax[i] - edgeMin[i]);

                // Find any overlap and subtrac from total waked sector (and turb wake prob) and sum wake probability
                double sumWake = 0;

                for (int i = 0; i < TurbineCount; i++)
                {
                    sumWake = sumWake + probWake[i, WD_Ind];
                    Turbine turb1 = thisInst.turbineList.turbineEsts[i];
                    double dist1 = thisInst.topo.CalcDistanceBetweenPoints(turb1.UTMX, turb1.UTMY, thisX, thisY);

                    for (int j = i + 1; j < TurbineCount; j++)
                    {
                        Turbine turb2 = thisInst.turbineList.turbineEsts[j];
                        double dist2 = thisInst.topo.CalcDistanceBetweenPoints(turb2.UTMX, turb2.UTMY, thisX, thisY);

                        if (edgeMax[i] > edgeMin[j] && edgeMax[i] < edgeMax[j] && edgeMax[i] != -999 && edgeMin[j] != -999)
                        {
                            totalWake = totalWake - (edgeMax[i] - edgeMin[j]);

                            if (dist1 < dist2) // Reduce wake of turbine that is furthest away
                                probWake[j, WD_Ind] = probWake[j, WD_Ind] - (edgeMax[i] - edgeMin[j]) / (360.0 / thisInst.metList.numWD);
                            else
                                probWake[i, WD_Ind] = probWake[i, WD_Ind] - (edgeMax[i] - edgeMin[j]) / (360.0 / thisInst.metList.numWD);
                        }
                        else if (edgeMax[j] > edgeMin[i] && edgeMax[j] < edgeMax[i] && edgeMax[j] != -999 && edgeMin[i] != -999)
                        {
                            totalWake = totalWake - (edgeMax[j] - edgeMin[i]);
                            if (dist1 < dist2)
                                probWake[j, WD_Ind] = probWake[j, WD_Ind] - (edgeMax[j] - edgeMin[i]) / (360.0 / thisInst.metList.numWD);
                            else
                                probWake[i, WD_Ind] = probWake[i, WD_Ind] - (edgeMax[j] - edgeMin[i]) / (360.0 / thisInst.metList.numWD);
                        }
                    }
                }
            }
            
            return probWake;
        }

        public void ClearTimeSeries()
        {
            // Clears all generated time series. Called after save file.
            for (int i = 0; i < TurbineCount; i++)
                for (int j = 0; j < turbineEsts[i].AvgWSEst_Count; j++)
                    turbineEsts[i].avgWS_Est[j].timeSeries = new ModelCollection.TimeSeries[0];
        }




    }
}
    
