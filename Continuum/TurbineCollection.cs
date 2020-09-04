using System;
using System.Windows.Forms;
using MathNet.Numerics;
using System.IO;

namespace ContinuumNS
{
    /// <summary> Class that holds list of Turbine objects, list of PowerCurve objects, and Exceedance object.  Also, contains functions to add, get, and clear estimates at turbine sites. </summary>
    [Serializable()]
    public class TurbineCollection
    {
        /// <summary> List of Turbine objects. </summary>
        public Turbine[] turbineEsts;
        /// <summary> True if calculations at turbine sites have been done. </summary>
        public bool turbineCalcsDone;
        /// <summary> List of turbine power and thrust curves. </summary>
        public PowerCurve[] powerCurves;
        /// <summary> Exceedance (uncertainty) model. </summary>
        public Exceedance exceed;
        /// <summary> True if time series estimates have been generated. </summary>
        public bool genTimeSeries = false;

        /// <summary> Holds power and thrust curve and all other info for a turbine model. </summary>
        [Serializable()] public struct PowerCurve
        {
            /// <summary> Name of turbine model. </summary>
            public string name;
            /// <summary> Cut-in wind speed. </summary>
            public double cutInWS;
            /// <summary> Cut-out wind speed. </summary>
            public double cutOutWS;
            /// <summary> Rated power. </summary>
            public double ratedPower;
            /// <summary> Rated wind speed. </summary>
            public double ratedWS;
            /// <summary> Power production (by WS). </summary>
            public double[] power;
            /// <summary> Rotor diameter. </summary>
            public double RD;
            /// <summary> Thrust coefficient (by WS). </summary>
            public double[] thrustCoeff;
            /// <summary> Rated RPM. </summary>
            public double ratedRPM;
            /// <summary> Wind speed interval. </summary>
            public double wsInt;
            /// <summary> First WS in power and thrust curves. </summary>
            public double firstWS;

            /// <summary> Clears turbine object. </summary>
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

        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary> Returns number of turbines in list. </summary>
        public int TurbineCount {
            get {
                if (turbineEsts == null)
                    return 0;
                else
                    return turbineEsts.Length;
            }
        }

        /// <summary> Returns number of power curves in list. </summary>
        public int PowerCurveCount {
            get {
                if (powerCurves == null)
                    return 0;
                else
                    return powerCurves.Length;
            }
        }

        /// <summary> Returns number of turbine strings. </summary>
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

        /// <summary> Clears all turbine objects in list. </summary>
        public void ClearAllTurbines() {            
            turbineEsts = null;
        }

        /// <summary> Clears all power curves in list. </summary>
        public void ClearAllPowerCurves() {
            
            powerCurves = null;
        }

        /// <summary> Clears all gross AEP estimates formed at turbine sites. </summary>
        public void ClearAllGrossEsts() {
            
            for (int i = 0; i < TurbineCount; i++)
                turbineEsts[i].grossAEP = null;
        }

        /// <summary> Clears all WS_Estimate() and avgWS_Est(). Sets turbineCalcsDone to false. </summary>
        public void ClearAllWSEsts() {
            
            for (int i = 0; i < TurbineCount; i++) {
                turbineEsts[i].avgWS_Est = null;
                turbineEsts[i].WS_Estimate = null;
            }

            turbineCalcsDone = false;
        }

        /// <summary> Clears all net AEP estimates formed at turbine sites. </summary>
        public void ClearAllNetEsts() {
            
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

        

        /// <summary> Clears all calculations at turbine sites. </summary>
        public void ClearAllCalcs()
        {            
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

        /// <summary> Add power curve to list. </summary>
        public void AddPowerCurve(string name, double cutIn, double cutOut, double ratedPower, double[] powerValues, double[] thrustValues, double RD, 
            double ratedRPM, double ratedWS, double WS_IntSize, double firstWS)
        {
            
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

        /// <summary> Imports new power curve and adds to list. </summary>
        public void ReadPowerCurve(string name, double[,] importedPower, double RD, double RPM)
        {                             
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

            // Cut-in WS to first instance of 0 kW
            if (importedPower[1, 0] > 0) // first entry is cut-in
                cutIn = importedPower[0, 0];
            else
            {
                for (int i = 0; i < numWS_Import - 1; i++)
                {
                    if (importedPower[1, i] == 0 && importedPower[1, i + 1] > 0)
                    {
                        cutIn = importedPower[0, i];
                        break;
                    }
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

        /// <summary> Adds new Turbine object to list. </summary>
        public void AddTurbine(string name, double utmx, double utmy, int stringNum)
        {           
            // Check to see that the turbine doesn't already exist in memory
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

        /// <summary> Edits the UTMX, UTMY of turbine site and clears all calculated values. </summary>
        public void EditTurbine(string name, double utmx, double utmy)
        {            
            for (int i = 0; i <= TurbineCount - 1; i++) {
                if (name == turbineEsts[i].name) {
                    turbineEsts[i].UTMX = utmx;
                    turbineEsts[i].UTMY = utmy;
                    turbineEsts[i].ClearAllCalcs();
                }
            }
        }

        /// <summary> Calculates exposure and SRDH (if gotSR is true) for all radii at all turbine sites. </summary>        
        public void CalcTurbineExposures(Continuum thisInst, int radius, double exponent, int numSectors)
        {             
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
                            turbineEsts[i].expo[expoIndex].UW_P10CrossGrade[r] = thisInst.topo.CalcP10_UW_CrosswindGrade(turbineEsts[i].UTMX, turbineEsts[i].UTMY, r, numWD); ;
                            turbineEsts[i].expo[expoIndex].UW_ParallelGrade[r] = thisInst.topo.CalcP10_UW_ParallelGrade(turbineEsts[i].UTMX, turbineEsts[i].UTMY, r, numWD);
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

        /// <summary> Returns the min. and max. distance from the turbines to specified UTMX and UTMY. </summary>   
        public int[] CalcMinMaxDistanceToTurbines(double thisX, double thisY)
        {            
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

        /// <summary> Recalculates upwind and downwind surface roughness and displacement height at turbine sites.  Called after the land cover key is changed. </summary>        
        public void ReCalcTurbine_SRDH(Continuum thisInst)
        {              
            if (thisInst.topo.gotTopo == false || GotEst("Expo", new PowerCurve(), null) == false)
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

        /// <summary> Calculates the gross AEP for every turbine and every power curve. </summary>        
        public void CalcGrossAEPFromTABs(Continuum thisInst)
        {            
            if (thisInst.metList.numWD == 0) return;
            int numWD = thisInst.metList.numWD;
            int numWS = thisInst.metList.numWS;
                      
            string[] metsUsed = thisInst.metList.GetMetsUsed();    

            for (int i = 0; i < PowerCurveCount; i++)
            {                
                for (int j = 0; j < TurbineCount; j++)
                {
                    double[] P50_AEP_Sect = new double[numWD];
                    // Check to see if energy calc already done
                    bool alreadyCalc = false;
                    for (int k = 0; k < turbineEsts[j].GrossAEP_Count; k++) {
                        if (turbineEsts[j].grossAEP[k].powerCurve.name == powerCurves[i].name) 
                        { 
                            alreadyCalc = true;
                            break;
                        }
                    }                    

                    Turbine.Avg_Est avgEst = turbineEsts[j].GetAvgWS_Est(null);

                    if (avgEst.freeStream.WS == 0)
                        break;

                    if (alreadyCalc == false)
                    {                        
                        double[,] sectorDist = new double[numWD, numWS];

                        for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++) {
                            double P90_WS = avgEst.freeStream.sectorWS[WD_Ind] - avgEst.uncert * 1.28f;
                            double[] WS_Dist = thisInst.metList.CalcWS_DistForTurbOrMap(metsUsed, P90_WS, WD_Ind, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
                            for (int WS_ind = 0; WS_ind <= numWS - 1; WS_ind++)
                                sectorDist[WD_Ind, WS_ind] = WS_Dist[WS_ind];
                        }

                        double[] P90_Dist = thisInst.metList.CalcOverallWS_Dist(sectorDist, thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, Met.TOD.All, Met.Season.All));

                        for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++) {
                            double P99_WS = avgEst.freeStream.sectorWS[WD_Ind] - avgEst.uncert * 2.33f;
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
                        turbineEsts[j].AddGrossAEP(powerCurves[i], P50_AEP, This_CF, P90_AEP, P99_AEP, P50_AEP_Sect);
                    }
                }
            }
        }

        /// <summary> Calculates and returns the capacity factor based on AEP and rated power. AEP is in MWh and rated power is in kW. </summary>        
        public double CalcCapacityFactor(double AEP, double ratedPower) 
        {            
            double thisCF = AEP / (8.76 * ratedPower);

            return thisCF;
        }

        /// <summary> Calculates and returns gross AEP (in MWh) calculated from specified wind speed distribution and power curve. </summary>        
        public double CalcAndReturnGrossAEP(double[] WS_Dist, MetCollection metList, string powerCurve)
        {            
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

        /// <summary> Assigns a string number to all turbine based on distance between turbines in layout. </summary>     
        public void AssignStringNumber()
        {            
            if (TurbineCount > 0)
                if (turbineEsts[0].stringNum != 0)
                    return;

            // Clear old string numbers
            for (int i = 0; i < TurbineCount; i++)
                turbineEsts[i].stringNum = 0;                      
                        
            TopoInfo topo = new TopoInfo();

            if (TurbineCount > 0)
            {
                turbineEsts[0].stringNum = 1;
                int lastString = 1;

                for (int i = 1; i < TurbineCount; i++)
                {
                    double closestDistance = 100000;
                    for (int j = 0; j < i; j++)
                    {
                        double thisDistance = topo.CalcDistanceBetweenPoints(turbineEsts[i].UTMX, turbineEsts[i].UTMY, turbineEsts[j].UTMX, turbineEsts[j].UTMY);
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

        /// <summary> If calculations were interrupted, this cleans up the turbine estimate list so that all turbine sites have same calculations. </summary> 
        public void CleanUpEsts()
        {           
            bool inLastTurbine = false;
            WakeCollection wakeList = new WakeCollection();

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
                                if (turbineEsts[i].grossAEP[j].powerCurve.name == turbineEsts[TurbineCount - 1].grossAEP[k].powerCurve.name) { 
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
                                if (wakeList.IsSameWakeModel(turbineEsts[i].netAEP[j].wakeModel, turbineEsts[TurbineCount - 1].netAEP[k].wakeModel)) {
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

        /// <summary> Deletes turbine object with specified name. </summary> 
        public void DeleteTurbine(string name)
        {            
            int newCount = TurbineCount - 1;

            if (newCount > 0)
            {
                Turbine[] tempList = new Turbine[newCount];                   
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

        /// <summary> Deletes power curve with specified name. </summary> 
        public void DeletePowerCurve(string name)
        {            
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

        /// <summary> Calculates the average, st. dev., min and max of turbine wind speed estimates for specified WD sector and updates the textboxes on Gross Turb Ests tab. </summary> 
        public void FindParamStats(Continuum thisInst, Turbine[] turbines, int WD_Ind)
        {
            PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Gross");
            double avg = FindAverage(turbines, WD_Ind, powerCurve);
            double stdev = FindStdev(turbines, WD_Ind, powerCurve);
            double min = FindMin(turbines, WD_Ind, powerCurve);
            double max = FindMax(turbines, WD_Ind, powerCurve);

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
                double avgAEP = FindAverageAEP(turbines, selectedPowerCurve, WD_Ind);
                double stdevAEP = FindStdevAEP(turbines, selectedPowerCurve, WD_Ind);
                double minAEP = FindMinAEP(turbines, selectedPowerCurve, WD_Ind);
                double maxAEP = FindMaxAEP(turbines, selectedPowerCurve, WD_Ind);

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

        /// <summary> Calculates and returns the average free-stream wind speed for specified WD sector. </summary> 
        public double FindAverage(Turbine[] turbines, int WD_Ind, PowerCurve powerCurve)
        {           
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
                Turbine.Avg_Est avgEst = turbines[i].GetAvgWS_Est(null);

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

        /// <summary> Calculates and returns the average gross AEP for specified WD sector and power curve. </summary> 
        public double FindAverageAEP(Turbine[] turbines, string powerCurve, int WD_Ind)
        {            
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
                    if (turbines[i].grossAEP[j].powerCurve.name == powerCurve)
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

        /// <summary> Calculates and returns the minimum gross AEP for specified WD sector and power curve. </summary>
        public double FindMinAEP(Turbine[] turbines, string powerCurve, int WD_Ind)
        {           
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
                    if (turbines[i].grossAEP[j].powerCurve.name == powerCurve) 
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

        /// <summary> Calculates and returns the minimum free-stream wind speed for specified WD sector. </summary>
        public double FindMin(Turbine[] turbines, int WD_Ind, PowerCurve powerCurve)
        {            
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
                Turbine.Avg_Est avgEst = turbines[i].GetAvgWS_Est(null);

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

        /// <summary> Calculates and returns the maximum gross AEP for specified WD sector and power curve. </summary>
        public double FindMaxAEP(Turbine[] turbines, string powerCurve, int WD_Ind)
        {            
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
                    if (turbines[i].grossAEP[j].powerCurve.name == powerCurve) 
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

        /// <summary> Calculates and returns the maximum free-stream wind speed for specified WD sector. </summary>
        public double FindMax(Turbine[] turbines, int WD_Ind, PowerCurve powerCurve)
        {            
            double max = 0;
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

            for (int i = 0; i <= turbines.Length - 1; i++)
            {
                Turbine.Avg_Est avgEst = turbines[i].GetAvgWS_Est(null);

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

        /// <summary> Calculates and returns the st. deviation gross AEP for specified WD sector and power curve. </summary>
        public double FindStdevAEP(Turbine[] turbines, string powerCurve, int WD_Ind)
        {            
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
                    if (turbines[i].grossAEP[j].powerCurve.name == powerCurve) 
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

        /// <summary> Calculates and returns the st. deviation wind speed for specified WD sector. </summary>
        public double FindStdev(Turbine[] turbines, int WD_Ind, PowerCurve powerCurve)
        {            
            double avg = 0;
            double stdev = 0;
            int dataCount = 0;
            
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
                Turbine.Avg_Est avgEst = turbines[i].GetAvgWS_Est(null);

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

        /// <summary>  Returns Turbine object with specified name. </summary>
        public Turbine GetTurbine(string turbineName)
        {            
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

        /// <summary>  Returns power curve object with specified name. </summary>
        public PowerCurve GetPowerCurve(string powerCurveName)
        {            
            PowerCurve thisPowerCurve = new PowerCurve();

            for (int i = 0; i < PowerCurveCount; i++)
                if (powerCurves[i].name == powerCurveName)
                {
                    thisPowerCurve = powerCurves[i];
                    break;
                }

            return thisPowerCurve;
        }

        /// <summary>  Returns power or thrust coefficient of specified power curve interpolated at specified wind speed. </summary>
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

                    if (middleWSInd == 0)
                        powerArray[0] = 0;
                    else
                        powerArray[0] = thisPowerCurve.power[middleWSInd - 1];

                    powerArray[1] = thisPowerCurve.power[middleWSInd];
                    powerArray[2] = thisPowerCurve.power[middleWSInd + 1];

                    Polynomial thisPoly = Polynomial.Fit(WS_Array, powerArray, 2, MathNet.Numerics.LinearRegression.DirectRegressionMethod.NormalEquations);
                    thisVal = thisPoly.Evaluate(thisWS);
                }
                else if (powerOrThrust == "Thrust")
                {
                    double[] thrustArray = new double[3];

                    if (middleWSInd == 0)
                        thrustArray[0] = 0;
                    else
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

        /// <summary> Opens and reads power curve file and adds to list of power curves. </summary>
        public void ImportPowerCurve(Continuum thisInst, double RD, double RPM)
        {  
            int WS_Ind = 0;
            double[,] powerCurve = new double[3, WS_Ind + 1];           

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

            ReadPowerCurve(powerCurveName, powerCurve, RD, RPM);
   
            thisInst.ChangesMade();
            thisInst.turbineList.AreTurbCalcsDone(thisInst);
            thisInst.updateThe.AllTABs(thisInst);
        }

        /// <summary> Deletes any duplicate or unneeded AvgWSEsts. </summary>
        public void ClearDuplicateAvgWS(bool isTimeSeries)
        {          
            
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

        /// <summary> Checks the state of the turbine ests compared to what power curves and wake models have been created. If turbine calcs not complete then sets turbineCalcsDone = false. </summary>
        public void AreTurbCalcsDone(Continuum thisInst)
        {            
            if (TurbineCount == 0)
            {
                turbineCalcsDone = false;
                return;
            }                        

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
                        Turbine.Gross_Energy_Est grossEst = turbineEsts[0].GetGrossEnergyEst(powerCurve);

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
                        Turbine.Net_Energy_Est netEst = turbineEsts[0].GetNetEnergyEst(wakeModel);

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
                        bool haveEst = turbineEsts[0].HaveTS_Estimate("Gross", null, powerCurve);
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
                        bool haveEst = turbineEsts[0].HaveTS_Estimate("Gross", null, powerCurve);
                        if (haveEst == false)
                            turbineCalcsDone = false;
                    }

                    for (int i = 0; i < thisInst.wakeModelList.NumWakeModels; i++)
                    {
                        Wake_Model wakeModel = thisInst.wakeModelList.wakeModels[i];
                        bool haveEst = turbineEsts[0].HaveTS_Estimate("Net", wakeModel, wakeModel.powerCurve);
                        if (haveEst == false)
                            turbineCalcsDone = false;
                    }

                }
            }
        }

        /// <summary> Returns true if calculations have been completed with specified power curve and wake model. estType = "Expo", "WS", "Gross", "Net". </summary>
        public bool GotEst(string estType, PowerCurve powerCurve, Wake_Model wakeModel)
        {            
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

        /// <summary> Clears all exceedance calculations for all turbines. </summary>
        public void ClearAllExceedance()
        {                                          
            if (exceed != null)
                exceed.SizeMonteCarloArrays();            
        }

        /// <summary> Defines default exceedance curves if they don't exist yet. </summary>
        public void SetExceedCurves()
        {            
            if (exceed == null)
            {
                exceed = new Exceedance();
                exceed.compositeLoss.isComplete = false;
                exceed.CreateDefaultCurve();
            }
        }

        /// <summary> Calculates and returns the probability of each turbine creating a wake at specified UTMX and Y. Used in effective TI calculations. </summary>        
        public double[,] CalcProbOfWakeForEffectiveTI(Continuum thisInst, double thisX, double thisY, PowerCurve powerCurve)
        {
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
                
                for (int i = 0; i < TurbineCount; i++)
                {                    
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

        /// <summary> Clears all generated time series. Called after save file. </summary>  
        public void ClearTimeSeries()
        {           
            for (int i = 0; i < TurbineCount; i++)
                for (int j = 0; j < turbineEsts[i].AvgWSEst_Count; j++)
                    turbineEsts[i].avgWS_Est[j].timeSeries = new ModelCollection.TimeSeries[0];
        }
    }
}
    
