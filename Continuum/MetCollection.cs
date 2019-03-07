using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class MetCollection
    {
        public Met[] metItem;
        public bool expoIsCalc;
        public bool SRDH_IsCalc;
        public int numWD;
        public int numWS;

        public int ThisCount
        {
            get {
                if (metItem == null)
                    return 0;
                else
                    return metItem.Length; }
        }     
        
        public void MakeAllSameLength()
        {
            // if Met TAB files have different number of WS bins, this void adds zeros to the end to make all mets WS dists the same length
            int maxLength = 0;
                        
            for (int i = 0; i < ThisCount; i++)
            {
                if (metItem[i].WS_Dist.Length > maxLength)
                    maxLength = metItem[i].WS_Dist.Length;                
            }

            for (int i = 0; i < ThisCount; i++)
            {
                int thisLength = metItem[i].WS_Dist.Length;
                if (thisLength < maxLength)
                {
                    Array.Resize(ref metItem[i].WS_Dist, maxLength);

                    double[,] Orig_Sect_Dist = metItem[i].sectorWS_Dist;
                    metItem[i].sectorWS_Dist = new double[numWD, maxLength];

                    for (int WS_ind = 0; WS_ind <= Orig_Sect_Dist.GetUpperBound(0); WS_ind++)
                        for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                            metItem[i].sectorWS_Dist[WD_Ind, WS_ind] = Orig_Sect_Dist[WD_Ind, WS_ind];                                       
                    
                }
            }

        }

        public string MakeUpNameIfBlank()
        {
            // Creates a name for met site if blank in TAB file
            string metName = "Met_" + ThisCount;

            return metName;

        }

        public void AddMetTAB(string metName, double UTMX, double UTMY, int height, double[] metWindRose, double[,] sectorWS_Dist, double WS_FirstInt, double WS_IntSize)
        {
            // Adds met to list and calculates average wind speed and directional WS ratios
            int newCount = ThisCount;
            Array.Resize(ref metItem, newCount + 1);

            metItem[newCount] = new Met();
            metItem[newCount].name = metName;
            metItem[newCount].UTMX = UTMX;
            metItem[newCount].UTMY = UTMY;
            metItem[newCount].height = height;
            metItem[newCount].WS_FirstInt = WS_FirstInt;
            metItem[newCount].WS_IntSize = WS_IntSize;
            metItem[newCount].sectorWS_Dist = sectorWS_Dist;
            metItem[newCount].windRose = metWindRose;

            metItem[newCount].CalcAvgWS();
            metItem[newCount].CalcSectorWS_Ratios();

            numWD = metWindRose.Length;
            numWS = sectorWS_Dist.GetUpperBound(1) + 1;

        }

        public void Remove(string metname)
        {
            // Deletes Met from list
            int newCount = ThisCount - 1;

            if (newCount > 0)
            {
                Met[] tempList = new Met[newCount]; // Create list of met towers that you//re keeping(so size one less than before)
                int tempIndex = 0;
                
                for (int i = 0; i < ThisCount; i++)
                {
                    if (metItem[i].name != metname)
                    {
                        tempList[tempIndex] = metItem[i];
                        tempIndex++;
                    }
                }
                metItem = tempList;
            }
            else {
                metItem = null;

            }

        }

        public void CalcMetExposures(int metInd, int radiusIndex, Continuum thisInst)
        {
            // Calculates exposure and SRDH at met site (if not already calculated)
            int expoInd = 0;
            int numSectors = 1;

            // First find elevation
            if (metItem[metInd].elev == 0) metItem[metInd].elev = thisInst.topo.CalcElevs(metItem[metInd].UTMX, metItem[metInd].UTMY);
            
            int thisRadius = thisInst.radiiList.investItem[radiusIndex].radius;
            double thisExponent = thisInst.radiiList.investItem[radiusIndex].exponent;
            double thisX = metItem[metInd].UTMX;
            double thisY = metItem[metInd].UTMY;

            bool isNew = metItem[metInd].IsNewExposure(thisRadius, thisExponent, numSectors);
            bool IsNewSRDH = metItem[metInd].IsNewSRDH(thisRadius, thisExponent, numSectors);

            if (isNew == true)
            {
                metItem[metInd].AddExposure(thisRadius, thisExponent, numSectors);
                // Now calculate exposures, (have to find where the R and exp was entered in the list)
                for (int k = 0; k < metItem[metInd].ExposureCount; k++)
                {
                    if (metItem[metInd].expo[k].radius == thisRadius && metItem[metInd].expo[k].exponent == thisExponent && metItem[metInd].expo[k].numSectors == numSectors)
                    {
                        expoInd = k;
                        break;
                    }
                }

                // Check to see if an exposure with a smaller radii has been calculated
                int smallerRadius = thisInst.topo.GetSmallerRadius(metItem[metInd].expo, thisRadius, thisExponent, numSectors);

                if (smallerRadius == 0 || numSectors > 1)
                { // when sector avg is used, can//t add on to exposure calcs...so gotta do it the long way
                    metItem[metInd].expo[expoInd] = thisInst.topo.CalcExposures(thisX, thisY, metItem[metInd].elev, thisRadius, thisExponent, numSectors, thisInst.topo, numWD);
                    if (thisInst.topo.gotSR == true)
                        thisInst.topo.CalcSRDH(ref metItem[metInd].expo[expoInd], thisX, thisY, thisRadius, thisExponent, numWD);
                }
                else
                {
                    Exposure smallerExposure = thisInst.topo.GetSmallerRadiusExpo(metItem[metInd].expo, smallerRadius, metItem[metInd].expo[expoInd].exponent, numSectors);

                    metItem[metInd].expo[expoInd] = thisInst.topo.CalcExposuresWithSmallerRadius(thisX, thisY, metItem[metInd].elev, thisRadius, thisExponent, numSectors, smallerRadius, smallerExposure, numWD);

                    if (thisInst.topo.gotSR == true)
                        thisInst.topo.CalcSRDHwithSmallerRadius(ref metItem[metInd].expo[expoInd], thisX, thisY, thisRadius, thisExponent, numSectors, smallerRadius, smallerExposure, numWD);
                    
                }
            }

            if (IsNewSRDH == true)
            {
                if (thisInst.topo.gotSR == true)
                {
                    for (int k = 0; k < metItem[metInd].ExposureCount; k++)
                    {
                        if (metItem[metInd].expo[k].radius == thisRadius && metItem[metInd].expo[k].exponent == thisExponent)
                        {
                            expoInd = k;
                            break;
                        }
                    }

                    // Check to see if an exposure with a smaller radii has been calculated
                    int smallerRadius = thisInst.topo.GetSmallerRadius(metItem[metInd].expo, thisRadius, thisExponent, numSectors);

                    if (smallerRadius == 0)
                        thisInst.topo.CalcSRDH(ref metItem[metInd].expo[expoInd], thisX, thisY, thisRadius, thisExponent, numWD);
                    else {
                        Exposure smallerExposure = thisInst.topo.GetSmallerRadiusExpo(metItem[metInd].expo, smallerRadius, metItem[metInd].expo[expoInd].exponent, numSectors);
                        thisInst.topo.CalcSRDHwithSmallerRadius(ref metItem[metInd].expo[expoInd], thisX, thisY, thisRadius, thisExponent, numSectors, smallerRadius, smallerExposure, numWD);
                    }

                }
                else {
                    // Not a new exposure but still updates the bulk UW and DW with updated WR
                    for (int k = 0; k < metItem[metInd].ExposureCount; k++)
                    {
                        if (metItem[metInd].expo[k].radius == thisInst.radiiList.investItem[radiusIndex].radius && metItem[metInd].expo[k].exponent == thisInst.radiiList.investItem[radiusIndex].exponent
                            && metItem[metInd].expo[k].numSectors == numSectors)
                        {
                            expoInd = k;
                            break;
                        }
                    }
                }
            }

            if (metItem[metInd].expo[expoInd].UW_P10CrossGrade == null)
            {
                // Calc P10 UW Crosswind Grade
                
                metItem[metInd].expo[expoInd].UW_P10CrossGrade = new double[numWD];
                metItem[metInd].expo[expoInd].UW_ParallelGrade = new double[numWD];

                for (int r = 0; r < numWD; r++)
                {
                    double UW_CW_Grade = thisInst.topo.CalcP10_UW_CrosswindGrade(metItem[metInd].UTMX, metItem[metInd].UTMY, thisInst.radiiList, r, numWD);
                    double UW_PL_Grade = thisInst.topo.CalcP10_UW_ParallelGrade(metItem[metInd].UTMX, metItem[metInd].UTMY, thisInst.radiiList, r, numWD);

                    metItem[metInd].expo[expoInd].UW_P10CrossGrade[r] = UW_CW_Grade;
                    metItem[metInd].expo[expoInd].UW_ParallelGrade[r] = UW_PL_Grade;
                }
            }
        }

        public void ReCalcSRDH(TopoInfo topo, InvestCollection radiiList)
        {
            if (expoIsCalc == false)
                return;

            // if Land Cover key is changed, the surface roughness and displacement heights are recalculated in this void
            int expoInd = 0;
            int numSectors = 1;

            for (int metInd = 0; metInd < ThisCount; metInd++)
            {
                // First find elevation
                if (metItem[metInd].elev == 0) metItem[metInd].elev = topo.CalcElevs(metItem[metInd].UTMX, metItem[metInd].UTMY);                              
                         
                double thisX = metItem[metInd].UTMX;
                double thisY = metItem[metInd].UTMY;

                for (int radiusInd = 0; radiusInd < radiiList.ThisCount; radiusInd++)
                {
                    int thisRadius = radiiList.investItem[radiusInd].radius;
                    double thisExponent = radiiList.investItem[radiusInd].exponent;

                    if (topo.gotSR == true)
                    {
                        for (int k = 0; k < metItem[metInd].ExposureCount; k++)
                        {
                            if (metItem[metInd].expo[k].radius == thisRadius && metItem[metInd].expo[k].exponent == thisExponent)
                            {
                                expoInd = k;
                                break;
                            }
                        }

                        // Check to see if an exposure with a smaller radii has been calculated
                        int smallerRadius = topo.GetSmallerRadius(metItem[metInd].expo, thisRadius, thisExponent, numSectors);

                        if (smallerRadius == 0)
                            topo.CalcSRDH(ref metItem[metInd].expo[expoInd], thisX, thisY, thisRadius, thisExponent, numWD);
                        else {
                            Exposure smallerExposure = topo.GetSmallerRadiusExpo(metItem[metInd].expo, smallerRadius, metItem[metInd].expo[expoInd].exponent, numSectors);
                            topo.CalcSRDHwithSmallerRadius(ref metItem[metInd].expo[expoInd], thisX, thisY, thisRadius, thisExponent, numSectors, smallerRadius, smallerExposure, numWD);
                        }
                    }
                }
            }
            SRDH_IsCalc = true;
        }
                

        public double[] GetAvgWindRoseMetsUsed(string[] metsUsed)
        {
            // Returns average wind rose calculated from metsUsed
            double[] avgWindRose = null;
            int numWD;
            bool metIsUsed = false;

            if (ThisCount > 0)
            {
                if (metItem[0].windRose == null) return avgWindRose;
                numWD = metItem[0].windRose.Length;
                avgWindRose = new double[numWD];

                for (int i = 0; i < ThisCount; i++)
                {
                    metIsUsed = false;

                    for (int metInd = 0; metInd < metsUsed.Length; metInd++)
                    {
                        if (metItem[i].name == metsUsed[metInd])
                        {
                            metIsUsed = true;
                            break;
                        }
                    }

                    if (metIsUsed == true)
                        for (int j = 0; j < numWD; j++)
                            avgWindRose[j] = avgWindRose[j] + metItem[i].windRose[j];
                                        
                }

                for (int i = 0; i < numWD; i++)
                    avgWindRose[i] = avgWindRose[i] / metsUsed.Length;
            }

            return avgWindRose;

        }

        public double[] GetAvgWindRose()
        {
            // Returns average wind rose using all mets in list
            double[] avgWindRose = null;
            int numWD;  

            if (ThisCount > 0)
            {
                numWD = metItem[0].windRose.Length;
                avgWindRose = new double[numWD];

                for (int i = 0; i < ThisCount; i++)
                {
                    for (int j = 0; j < numWD; j++)
                        avgWindRose[j] = avgWindRose[j] + metItem[i].windRose[j];
                }

                for (int i = 0; i < numWD; i++)
                    avgWindRose[i] = avgWindRose[i] / ThisCount;
            }

            return avgWindRose;

        }

        public double[] GetInterpolatedWindRose(string[] metsUsed, double UTMX, double UTMY)
        {
            // Returns wind rose interpolated from mets in metsUsed and weighted based on distance to X/Y
            double[] thisWR = null; 
            TopoInfo topo = new TopoInfo();

            if (ThisCount > 0)
            {                
                thisWR = new double[numWD];
                double[] wgts = new double[numWD];
                
                for (int i = 0; i < ThisCount; i++)
                { 
                    for (int j = 0; j < metsUsed.Length; j++)
                    {
                        if (metItem[i].name == metsUsed[j])
                        {
                            double thisDist = topo.CalcDistanceBetweenPoints(UTMX, UTMY, metItem[i].UTMX, metItem[i].UTMY);

                            if (thisDist == 0) thisDist = 1;

                            for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                            {
                                thisWR[WD_Ind] = thisWR[WD_Ind] + metItem[i].windRose[WD_Ind] * 1 / thisDist;
                                wgts[WD_Ind] = wgts[WD_Ind] + 1 / thisDist;
                            }
                            break;
                        }
                    }
                }

                for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                    thisWR[WD_Ind] = thisWR[WD_Ind] / wgts[WD_Ind];
            }

            return thisWR;
        }


        public double[] CalcOverallWS_Dist(double[,] sectorWS_Dist, double[] windRose)
        {
            // Calculate overall WS distribution using sectorwise wind speed distribution and weighted by wind rose                       
            double[] WS_Dist = null;

            if (sectorWS_Dist == null) return WS_Dist;  
            
            WS_Dist = new double[numWS];
            
            for (int i = 0; i < numWS; i++)
            {
                double sumRose = 0;
                double thisFreq = 0;
                for (int j = 0; j < numWD; j++)
                {
                    thisFreq = thisFreq + sectorWS_Dist[j, i] * windRose[j];
                    sumRose = sumRose + windRose[j];
                }
                WS_Dist[i] = thisFreq / sumRose / 1000;
            }

            return WS_Dist;
        }


        public double[] CalcWS_DistForTurbOrMap(string[] metsUsed, double avgWS, int WD_Ind)
        {
            // Calculates and returns sectorwise WS distribution by combining wind speed distributions from metsUsed such that avgWS is reached
            // if ( avgWS is within range of met avg wind speeds, use weighted average of all mets where weights are altered until avgWS is reached
            // if ( avgWS is outside range of met WS then it uses the met with either the highest or lowest WS and adjusts WS dist until avgWS is reached
            
            if (metsUsed == null || avgWS == 0) return null;
            double[] WS_Dist = new double[numWS];

            int numMetsUsed = metsUsed.Length;
            Met[] metsForDist = GetMets(metsUsed, null);
                        
            double calcWS = 0;            
            double avgWeightSum = 0;
            
            Met[] minMaxWS_Mets = GetMetsWithMinMaxWS(metsUsed, WD_Ind);
            double[] minMaxWS = new double[2];
            
            if (WD_Ind == numWD)
            {
                minMaxWS[0] = minMaxWS_Mets[0].WS;
                minMaxWS[1] = minMaxWS_Mets[1].WS;
            }
            else
            {
                minMaxWS[0] = minMaxWS_Mets[0].WS * minMaxWS_Mets[0].sectorWS_Ratio[WD_Ind];
                minMaxWS[1] = minMaxWS_Mets[1].WS * minMaxWS_Mets[1].sectorWS_Ratio[WD_Ind];
            }

            bool avgWS_InRange = false;
            if (avgWS > minMaxWS[0] && avgWS < minMaxWS[1])
                avgWS_InRange = true;                     

            if (avgWS_InRange == true)
            { // use combination of all mets used to form WS distribution

                double WS_diff = calcWS - avgWS;
                double[] weightSum = new double[numWS];
                double[] metWeights = new double[numMetsUsed];

                for (int i = 0; i < numMetsUsed; i++)
                {
                    metWeights[i] = 1;
                    for (int j = 0; j < numWS; j++)
                    {
                        if (WD_Ind == numWD) {
                            WS_Dist[j] = WS_Dist[j] + metsForDist[i].WS_Dist[j];
                            weightSum[j] = weightSum[j] + metWeights[i];
                        }
                        else {
                            WS_Dist[j] = WS_Dist[j] + metsForDist[i].sectorWS_Dist[WD_Ind, j];
                            weightSum[j] = weightSum[j] + metWeights[i];
                        }
                    }
                }

                calcWS = 0;
                avgWeightSum = 0;
                for (int i = 0; i < numWS; i++)
                {
                    WS_Dist[i] = WS_Dist[i] / weightSum[i];
                    calcWS = calcWS + WS_Dist[i] * GetWS_atWS_Ind(i);
                    avgWeightSum = avgWeightSum + WS_Dist[i];
                }

                // Make sure it adds up to 1.0
                if (avgWeightSum < 0.999 || avgWeightSum > 1.001)
                    for (int i = 0; i < numWS; i++)
                        WS_Dist[i] = WS_Dist[i] * Convert.ToSingle(1.0 / avgWeightSum);

                calcWS = calcWS / avgWeightSum;
                WS_diff = calcWS - avgWS;

                double lastWeight;
                int counter = 0;

                while (Math.Abs(WS_diff) > 0.001 && counter < 200)
                {
                    counter++;

                    for (int j = 0; j < numWS; j++)
                    {
                        WS_Dist[j] = 0;
                        weightSum[j] = 0;
                    }

                    for (int i = 0; i < numMetsUsed; i++)
                    {
                        lastWeight = metWeights[i];
                        if (metWeights[i] == 0) metWeights[i] = 0.001f;

                        if (WD_Ind == numWD && ((WS_diff < 0 && metsForDist[i].WS < avgWS) || (WS_diff > 0 && metsForDist[i].WS > avgWS)))
                        { // reduce met weight
                            metWeights[i] = metWeights[i] - metWeights[i] * Math.Abs(WS_diff) * (1 - Math.Abs((avgWS - metsForDist[i].WS) / avgWS));
                            if (metWeights[i] > lastWeight) metWeights[i] = 0;
                        }
                        else if (WD_Ind == numWD && ((WS_diff > 0 && metsForDist[i].WS < avgWS) || (WS_diff < 0 && metsForDist[i].WS > avgWS)))
                        { // increase met weight
                            double This_Part = Math.Abs(WS_diff) * (1 - Math.Abs((avgWS - metsForDist[i].WS) / avgWS));
                            metWeights[i] = metWeights[i] + metWeights[i] * Math.Abs(WS_diff) * (1 - Math.Abs((avgWS - metsForDist[i].WS) / avgWS));
                            if (metWeights[i] < lastWeight) metWeights[i] = 0;
                        }
                        else if ((WD_Ind != numWD) && (WS_diff < 0 && metsForDist[i].WS * metsForDist[i].sectorWS_Ratio[WD_Ind] < avgWS) ||
                       (WS_diff > 0 && metsForDist[i].WS * metsForDist[i].sectorWS_Ratio[WD_Ind] > avgWS))
                        { // reduce met weight
                            metWeights[i] = metWeights[i] - metWeights[i] * Math.Abs(WS_diff) * (1 - Math.Abs((avgWS - (metsForDist[i].WS * metsForDist[i].sectorWS_Ratio[WD_Ind])) / avgWS));
                            if (metWeights[i] > lastWeight) metWeights[i] = 0;
                        }
                        else if ((WD_Ind != numWD) && (WS_diff > 0 && metsForDist[i].WS * metsForDist[i].sectorWS_Ratio[WD_Ind] < avgWS) ||
                        (WS_diff < 0 && metsForDist[i].WS * metsForDist[i].sectorWS_Ratio[WD_Ind] > avgWS))
                        { // increase met weight
                            metWeights[i] = metWeights[i] + metWeights[i] * Math.Abs(WS_diff) * (1 - Math.Abs((avgWS - (metsForDist[i].WS * metsForDist[i].sectorWS_Ratio[WD_Ind])) / avgWS));
                            if (metWeights[i] < lastWeight) metWeights[i] = 0;
                        }

                        if (metWeights[i] < 0) metWeights[i] = 0.001f;

                        for (int j = 0; j < numWS; j++)
                        {
                            if (WD_Ind == numWD)
                                WS_Dist[j] = WS_Dist[j] + metsForDist[i].WS_Dist[j] * metWeights[i];
                            else
                                WS_Dist[j] = WS_Dist[j] + metsForDist[i].sectorWS_Dist[WD_Ind, j] * metWeights[i];

                            weightSum[j] = weightSum[j] + metWeights[i];
                        }
                    }

                    calcWS = 0;
                    avgWeightSum = 0;

                    for (int i = 0; i < numWS; i++)
                    {
                        WS_Dist[i] = WS_Dist[i] / weightSum[i];
                        calcWS = calcWS + WS_Dist[i] * GetWS_atWS_Ind(i);
                        avgWeightSum = avgWeightSum + WS_Dist[i];
                    }

                    if (avgWeightSum < 0.999 || avgWeightSum > 1.001)
                        for (int i = 0; i < numWS; i++)
                            WS_Dist[i] = WS_Dist[i] * Convert.ToSingle(1.0 / avgWeightSum);

                    calcWS = calcWS / avgWeightSum;
                    WS_diff = calcWS - avgWS;
                }
            }

            else { // use either min or max met and adjust WS dist to form WS dist with avgWS
                calcWS = 0;
                avgWeightSum = 0;
                                
                if (avgWS < minMaxWS[0])
                {
                    for (int i = 0; i < numWS; i++)
                    {
                        if (WD_Ind == numWD)
                            WS_Dist = minMaxWS_Mets[0].WS_Dist;
                        else
                            WS_Dist[i] = minMaxWS_Mets[0].sectorWS_Dist[WD_Ind, i];

                        calcWS = calcWS + WS_Dist[i] * GetWS_atWS_Ind(i);
                        avgWeightSum = avgWeightSum + WS_Dist[i];
                    }
                }
                else
                {                        
                    for (int i = 0; i < numWS; i++)
                    {
                        if (WD_Ind == numWD)
                            WS_Dist = minMaxWS_Mets[1].WS_Dist;
                        else
                            WS_Dist[i] = minMaxWS_Mets[1].sectorWS_Dist[WD_Ind, i];
                        calcWS = calcWS + WS_Dist[i] * GetWS_atWS_Ind(i);
                        avgWeightSum = avgWeightSum + WS_Dist[i];
                    }
                }                          

                // Make sure it adds up to 1.0
                if (avgWeightSum < 0.999 || avgWeightSum > 1.001)
                    for (int i = 0; i <= numWS - 1; i++)
                        WS_Dist[i] = WS_Dist[i] * 1.0f / avgWeightSum;


                calcWS = calcWS / avgWeightSum;
                double WS_diff = calcWS - avgWS;

                int avgWS_rnd = Convert.ToInt16(Math.Round(avgWS, 0));
                int counter = 0;

                while (Math.Abs(WS_diff) > 0.001 && counter < 200)
                {
                    counter++;
                    avgWeightSum = 0;                                      

                    for (int i = 0; i < numWS; i++)
                    {
                        double thisWS = GetWS_atWS_Ind(i);
                        if ((thisWS < avgWS_rnd && WS_diff < 0) || (thisWS > avgWS_rnd && WS_diff > 0))
                        { // reduce freq 
                            WS_Dist[i] = WS_Dist[i] - WS_Dist[i] * Math.Abs(WS_diff) * 0.06f;
                            avgWeightSum = avgWeightSum + WS_Dist[i];
                        }
                        else if ((thisWS < avgWS_rnd && WS_diff > 0) || (thisWS > avgWS_rnd && WS_diff < 0))
                        { // increase freq
                            WS_Dist[i] = WS_Dist[i] + WS_Dist[i] * Math.Abs(WS_diff) * 0.06f;
                            avgWeightSum = avgWeightSum + WS_Dist[i];
                        }
                        else if (thisWS == avgWS_rnd)
                        {
                            WS_Dist[i] = WS_Dist[i];
                            avgWeightSum = avgWeightSum + WS_Dist[i];
                        }
                    }

                    // Make sure it adds up to 1.0
                    if (avgWeightSum < 0.999 || avgWeightSum > 1.001)
                        for (int i = 0; i < numWS; i++)
                            WS_Dist[i] = WS_Dist[i] * 1.0f / avgWeightSum;

                    calcWS = 0;
                    avgWeightSum = 0;
                    for (int i = 0; i < numWS; i++)
                    {
                        calcWS = calcWS + WS_Dist[i] * GetWS_atWS_Ind(i);
                        avgWeightSum = avgWeightSum + WS_Dist[i];
                    }

                    calcWS = calcWS / avgWeightSum;
                    WS_diff = calcWS - avgWS;
                }
            }

            return WS_Dist;
        }               

        public Met[] GetMetsWithMinMaxWS(string[] metsUsed, int WD_Ind)
        {
            Met[] minMaxWS = new Met[2];
            if (metsUsed == null) return minMaxWS;
            int numMetsUsed = metsUsed.Length;
            
            for (int i = 0; i < ThisCount; i++)
            {
                Met thisMet = metItem[i];
                
                for (int j = 0; j < numMetsUsed; j++)
                {
                    if (thisMet.name == metsUsed[j])
                    {
                        if (WD_Ind == numWD)
                        {
                            if (minMaxWS[0] == null)
                                minMaxWS[0] = thisMet;
                            else if (thisMet.WS < minMaxWS[0].WS)
                                minMaxWS[0] = thisMet;

                            if (minMaxWS[1] == null)
                                minMaxWS[1] = thisMet;
                            else if (thisMet.WS > minMaxWS[1].WS)
                                minMaxWS[1] = thisMet;
                        }
                        else
                        {
                            if (minMaxWS[0] == null)
                                minMaxWS[0] = thisMet;
                            else if (thisMet.WS * thisMet.sectorWS_Ratio[WD_Ind] < minMaxWS[0].WS * minMaxWS[0].sectorWS_Ratio[WD_Ind])
                                minMaxWS[0] = thisMet;

                            if (minMaxWS[1] == null)
                                minMaxWS[1] = thisMet;
                            else if (thisMet.WS * thisMet.sectorWS_Ratio[WD_Ind] > minMaxWS[1].WS * minMaxWS[1].sectorWS_Ratio[WD_Ind])
                                minMaxWS[1] = thisMet;
                        }

                        break;
                    }
                }
            }

            return minMaxWS;
        }

        public double GetWS_atWS_Ind(int thisInd)
        {
            double thisWS = 0;
            if (ThisCount == 0) return thisWS;

            double WS_first_int = metItem[0].WS_FirstInt;
            double WS_int_size = metItem[0].WS_IntSize;

            thisWS = WS_first_int + WS_int_size * thisInd - WS_int_size / 2;

            return thisWS;
        }

        public Met[] GetMets(string[] metNames, string[] exceptMets)
        {
            // Returns Met objects for names listed
            Met[] theseMets = null;
            int numToExcl;

            if (metNames == null) return theseMets;
            
            if (exceptMets == null) 
                numToExcl = 0;
            else
                numToExcl = exceptMets.Length;
            
            int numMetsToReturn = metNames.Length - numToExcl;
            theseMets = new Met[numMetsToReturn];
            int metInd = 0;
            bool includeMet = true;
            
            for (int i = 0; i < metNames.Length; i++)
            { 
                for (int j = 0; j < metItem.Length; j++)
                {
                    if (metNames[i] == metItem[j].name)
                    {
                        includeMet = true;
                        for (int k = 0; k < numToExcl; k++)
                        {
                            if (metNames[i] == exceptMets[k])
                            {
                                includeMet = false;
                                break;
                            }
                        }

                        if (includeMet == true)
                        {
                            theseMets[metInd] = metItem[j];
                            metInd++;
                        }

                        break;
                    }

                }
            }

            return theseMets;
        }

        public Met GetMet(string metName)
        {
            // Returns Met objects for names listed
            Met thisMet = new Met();
            
            if (metName == "") return thisMet;
                        
            for (int i = 0; i < metItem.Length; i++)
            {
                if (metName == metItem[i].name)
                {
                    thisMet = metItem[i];
                    break;
                }
            }
            
            return thisMet;
        }

        public string[] GetMetsUsed()
            {
                // Returns array of met names from list of mets used in model
                string[] metsUsed = new string[ThisCount];

                for (int i = 0; i < ThisCount; i++)
                    metsUsed[i] = metItem[i].name;
            
                return metsUsed;
        }

        public Weibull_params CalcWeibullParams(double[] WS_Dist, double[,] sectDist, double avgWS)
        {
            // Calculates the RMS difference in weibull Freq dist and WS dist then sweeps k value until the min is found. Returns weibull estimates for overall and sector
            Weibull_params weibull = new Weibull_params();

            if (ThisCount == 0 || WS_Dist == null || sectDist == null)
                return weibull;          
            
            double WS_first = metItem[0].WS_FirstInt;
            double WS_int = metItem[0].WS_IntSize;
                        
            double K_Min_RMS = 0;
            double freqDiffMin = 0;
            
            // Sweep k from 1.5 to 3.5 and find k with min freq diff
            for (double k = 1.5f; k <= 3.5f; k = k + 0.5f)
            {
                double freqDiffSqr = 0;
                weibull.overall_k = k;
                double m = 1 + 1 / weibull.overall_k;                
                weibull.overall_A = CalcWeibullA(avgWS, m);
                double[] dist = new double[numWS];

                for (int j = 0; j < numWS; j++)
                {
                    double thisWS = WS_first + WS_int * j - WS_int / 2;
                    dist[j] = CalcWeibullDist(weibull.overall_k, weibull.overall_A, thisWS);
                    freqDiffSqr = freqDiffSqr + Convert.ToSingle(Math.Pow((WS_Dist[j] - dist[j]), 2));
                }

                freqDiffSqr = Convert.ToSingle(Math.Pow((freqDiffSqr / numWS), 0.5));
                if (k == 1.5)
                {
                    freqDiffMin = freqDiffSqr;
                    K_Min_RMS = 1.5f;
                }
                else if (freqDiffSqr < freqDiffMin)
                {
                    freqDiffMin = freqDiffSqr;
                    K_Min_RMS = k;
                }
            }

            // Sweep k from Last Min k - 0.5 to Last Min k + 0.5 and find k with min freq diff
            for (double k = K_Min_RMS - 0.5f; k <= K_Min_RMS + 0.5f; k = k + 0.1f)
            {
                double freqDiffSqr = 0;
                weibull.overall_k = k;
                double m = 1 + 1 / weibull.overall_k;
                weibull.overall_A = CalcWeibullA(avgWS, m);

                double[] dist = new double[numWS];
                for (int j = 0; j < numWS; j++)
                {
                    double thisWS = WS_first + WS_int * j - WS_int / 2;
                    dist[j] = CalcWeibullDist(weibull.overall_k, weibull.overall_A, thisWS);
                    freqDiffSqr = freqDiffSqr + Convert.ToSingle(Math.Pow((WS_Dist[j] - dist[j]), 2));
                }

                freqDiffSqr = Convert.ToSingle(Math.Pow((freqDiffSqr / numWS), 0.5));
                if ( freqDiffSqr<freqDiffMin )
                {
                    freqDiffMin = freqDiffSqr;
                    K_Min_RMS = k;
                }
            }

            // Sweep k from Last Min k - 0.1 to Last Min k + 0.1 and find k with min freq diff
            for (double k = K_Min_RMS - 0.1f; k <= K_Min_RMS + 0.1f; k = k + 0.02f)
            {
                double freqDiffSqr = 0;
                weibull.overall_k = k;
                double m = 1 + 1 / weibull.overall_k;
                weibull.overall_A = CalcWeibullA(avgWS, m);

                double[] dist = new double[numWS];
                for (int j = 0; j < numWS; j++)
                {
                    double thisWS = WS_first + WS_int * j - WS_int / 2;
                    dist[j] = CalcWeibullDist(weibull.overall_k, weibull.overall_A, thisWS); ;
                    freqDiffSqr = freqDiffSqr + Convert.ToSingle(Math.Pow((WS_Dist[j] - dist[j]), 2));
                }

                freqDiffSqr = Convert.ToSingle(Math.Pow((freqDiffSqr / numWS), 0.5));
                if (freqDiffSqr<freqDiffMin)
                {
                    freqDiffMin = freqDiffSqr;
                    K_Min_RMS = k;
                }
            }

            weibull.overall_k = K_Min_RMS;            
            weibull.overall_A = CalcWeibullA(avgWS, 1 + 1 / weibull.overall_k);

            // Now do sectorwise
            weibull.sector_k = new double[numWD];
            weibull.sector_A = new double[numWD];
            
            for (int i = 0; i < numWD; i++)
            {
                // Need to calc avg WS in this sector
                double avgSectWS = 0;
                double sectSum = 0;              
                freqDiffMin = 0;
                double freqDiffSqr = 0;
                for (int j = 0; j < numWS; j++)
                {
                    avgSectWS = avgSectWS + sectDist[i, j] * (WS_first + WS_int * j - WS_int / 2);
                    sectSum = sectSum + sectDist[i, j];
                }
                avgSectWS = avgSectWS / sectSum;

                // Sweep k from 1.5 to 3.5 and find k with min freq diff
                for (double k = 1.5f; k <= 3.5f; k = k + 0.5f)
                {
                    freqDiffSqr = 0;
                    weibull.sector_k[i] = k;
                    double m = 1 + 1 / weibull.sector_k[i];
                    weibull.sector_A[i] = CalcWeibullA(avgSectWS, m);

                    double[] dist = new double[numWS];
                    for (int j = 0; j < numWS; j++)
                    {
                        double thisWS = WS_first + WS_int * j - WS_int / 2;                        
                        dist[j] = CalcWeibullDist(weibull.sector_k[i], weibull.sector_A[i], thisWS);
                        freqDiffSqr = freqDiffSqr + Convert.ToSingle(Math.Pow((sectDist[i, j] / 1000 - dist[j]), 2));
                    }

                    freqDiffSqr = Convert.ToSingle(Math.Pow((freqDiffSqr / numWS), 0.5));

                    if (k == 1.5)
                    {
                        freqDiffMin = freqDiffSqr;
                        K_Min_RMS = 1.5f;
                    }
                    else if (freqDiffSqr < freqDiffMin)
                    {
                        freqDiffMin = freqDiffSqr;
                        K_Min_RMS = k;
                    }
                }

                // Sweep k from Last Min k - 0.5 to Last Min k + 0.5 and find k with min freq diff
                for (double k = K_Min_RMS - 0.5f; k <= K_Min_RMS + 0.5f; k = k + 0.1f)
                {
                    freqDiffSqr = 0;
                    weibull.sector_k[i] = k;
                    double m = 1 + 1 / weibull.sector_k[i];
                    weibull.sector_A[i] = CalcWeibullA(avgSectWS, m);

                    double[] dist = new double[numWS];
                    for (int j = 0; j < numWS; j++)
                    {
                        double thisWS = WS_first + WS_int * j - WS_int / 2;
                        dist[j] = CalcWeibullDist(weibull.sector_k[i], weibull.sector_A[i], thisWS);
                        freqDiffSqr = freqDiffSqr + Convert.ToSingle(Math.Pow((sectDist[i, j] / 1000 - dist[j]), 2));
                    }

                    freqDiffSqr = Convert.ToSingle(Math.Pow((freqDiffSqr / numWS), 0.5));

                    if (freqDiffSqr < freqDiffMin)
                    {
                        freqDiffMin = freqDiffSqr;
                        K_Min_RMS = k;
                    }
                }

                // Sweep k from Last Min k - 0.1 to Last Min k + 0.1 and find k with min freq diff
                for (double k = K_Min_RMS - 0.1f; k <= K_Min_RMS + 0.1f; k = k + 0.02f)
                {
                    freqDiffSqr = 0;
                    weibull.sector_k[i] = k;
                    double m = 1 + 1 / weibull.sector_k[i];
                    weibull.sector_A[i] = CalcWeibullA(avgSectWS, m);

                    double[] dist = new double[numWS];
                    for (int j = 0; j < numWS; j++)
                    {
                        double thisWS = WS_first + WS_int * j - WS_int / 2;
                        dist[j] = CalcWeibullDist(weibull.sector_k[i], weibull.sector_A[i], thisWS);
                        freqDiffSqr = freqDiffSqr + Convert.ToSingle(Math.Pow((sectDist[i, j] / 1000 - dist[j]), 2));
                    }

                    freqDiffSqr = Convert.ToSingle(Math.Pow((freqDiffSqr / numWS), 0.5));
                    if (freqDiffSqr < freqDiffMin)
                    {
                        freqDiffMin = freqDiffSqr;
                        K_Min_RMS = k;
                    }
                }

                weibull.sector_k[i] = K_Min_RMS;
                double This_m = 1 + 1 / weibull.sector_k[i];
                weibull.sector_A[i] = CalcWeibullA(avgSectWS, This_m);
            }

            return weibull;
        }

        public struct Weibull_params
        {
            public double overall_k;
            public double overall_A;
            public double[] sector_k;
            public double[] sector_A;
        }

        public double CalcWeibullA(double avgWS, double m)
        {
    
            double weibullA = avgWS / Convert.ToSingle((Math.Pow((2 * Math.PI * m), 0.5) * Math.Pow(m, (m - 1)) * Math.Exp(-m) * (1 + 1 / (12 * m) + 1 /
                            (288 * Math.Pow(m, 2)) - 139 / (51840 * Math.Pow(m, 3)))));

            return weibullA;
        }

        public double CalcWeibullDist(double k, double A, double WS)
        {
            double weibullDist = Convert.ToSingle((k / A) * Math.Pow((WS / A), (k - 1)) * Math.Exp(-(Math.Pow((WS / A), k))));
            return weibullDist;
        }

        public bool sameMets(string[] metsUsed1, string[] metsUsed2)
        {
            // Returns true if metsUsed1 and metsUsed2 have same mets            
            bool sameMetSites = false;
            bool foundMet = false;

            if (metsUsed1 != null && metsUsed2 != null)
            {
                if (metsUsed1.Length == metsUsed2.Length)
                {
                    for (int j = 0; j < metsUsed1.Length; j++)
                    {
                        foundMet = false;
                        for (int i = 0; i < metsUsed2.Length; i++)
                        {
                            if (metsUsed1[j] == metsUsed2[i])
                            {
                                foundMet = true;
                                break;
                            }
                        }

                        if (foundMet == false)
                        {
                            sameMetSites = false;
                            break;
                        }
                        else
                            sameMetSites = true;

                    }
                }
                else
                    sameMetSites = false; // different lengths
            }
            else 
                sameMetSites = false;

            return sameMetSites;
        }        

        public string CreateMetString(string[] metsUsed, bool isVerbose)
        {
            // Returns string of all mets in metsUsed
            int numMetsUsed;
            string metString = "";

            if (metsUsed != null)
            {
                numMetsUsed = metsUsed.Length;

                if (numMetsUsed == ThisCount && isVerbose == false)
                    metString = "All Mets";
                else
                {
                    metString = metsUsed[0];
                    if (numMetsUsed > 1)
                    {
                        for (int i = 1; i < numMetsUsed; i++)
                            metString = metString + " " + metsUsed[i];
                    }

                }
            }
            else
                metString = "All Mets";
            
            return metString;

        }

        public void ClearAllMets()
        {
            // Clears list of mets
            metItem = null;
            expoIsCalc = false;
            SRDH_IsCalc = false;
        }

        public void ClearAllExposuresAndGridStats()
        { 
            // Clears all calculated exposures, SRDH, and grid stats
            if (ThisCount > 0)
            {
                for (int i = 0; i < ThisCount; i++)
                {
                    metItem[i].ClearExposures();
                    metItem[i].gridStats.stats = null;
                }
            }
        }
                

        public Met Get_Met_with_Min_or_Max_UH_or_DH_P10(int WD_Ind, string minOrMax, string UH_or_DH, Model This_Model)
        {
            Met metReturn = new Met();
            InvestCollection radiiList = new InvestCollection();
            radiiList.New();
            int radiusIndex = radiiList.GetRadiusInd(This_Model.radius);
            double minOrMaxValue;
            if (minOrMax == "Min")
                minOrMaxValue = 1000;
            else
                minOrMaxValue = -1000;
            
            for (int i = 0; i < ThisCount; i++)
            {
                Met thisMet = metItem[i];
                string UW_Flow = This_Model.GetFlowType(thisMet.expo[radiusIndex].expo[WD_Ind], thisMet.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo"), WD_Ind, "UW", null, 5, false, radiusIndex);
                if (UW_Flow == "SpdUp")
                    UW_Flow = "Uphill"; // doesn't distinguish between induced speed-up and uphill flow for error estimate calculation

                string DW_Flow = This_Model.GetFlowType(thisMet.expo[radiusIndex].expo[WD_Ind], thisMet.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo"), WD_Ind, "DW", null, 5, false, radiusIndex);
                double thisP10 = Math.Max(thisMet.gridStats.stats[radiusIndex].P10_UW[WD_Ind], thisMet.gridStats.stats[radiusIndex].P10_DW[WD_Ind]);

                if (UW_Flow == UH_or_DH || DW_Flow == UH_or_DH)
                {                    
                    if ((minOrMax == "Min" && thisP10 < minOrMaxValue) || (minOrMax == "Max" && thisP10 > minOrMaxValue))
                    {
                        minOrMaxValue = thisP10;
                        metReturn = thisMet;
                    }                    
                }
            }

            return metReturn;
        }

        public string[] GetMetsExceptThoseInList(string[] metsToOmit)
        {
            string[] theseMets = null;
            int Num_to_use = 0;
            int Num_to_omit = 0;
            if (metsToOmit != null)
                Num_to_omit = metsToOmit.Length;

            for (int i = 0; i < ThisCount; i++)
            {
                bool includeThisMet = true;

                for (int j = 0; j < Num_to_omit; j++)
                    if (metItem[i].name == metsToOmit[j])
                        includeThisMet = false;

                if (includeThisMet == true)
                {
                    Num_to_use++;
                    Array.Resize(ref theseMets, Num_to_use);
                    theseMets[Num_to_use - 1] = metItem[i].name;
                }
            }

            return theseMets;
        }
    }
}
