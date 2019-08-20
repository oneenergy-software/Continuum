using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuumNS
{
    [Serializable()]
    public class Met
    {
        public string name; // Name of met site        
        public WSWD_Dist[] WSWD_Dists; // list of long-term wind speed and wind direction distributions for different heights, time of day, and season       
        
        public double UTMX; // UTM X Coordinate, m
        public double UTMY; // UTM Y Coordinate, m
        public double elev; // Elevation
        public Exposure[] expo; // Calculated exposure and SR/DH
        public Grid_Info gridStats = new Grid_Info(); // Calculated terrain complexity
        public NodeCollection.Sep_Nodes[] flowSepNodes; // If flow separation model enabled, nodes surrounding met where flow separation occurs
        public Met_Data_Filter metData;
        public MCP mcp;
        public Turbulence turbulence;

        [Serializable()]
        public struct WSWD_Dist
        {
            public TOD timeOfDay;
            public Season season;            
            public double WS;
            public double height;
            public double[] sectorWS_Ratio; // Directional wind speed ratios
            public double[] windRose; // Wind direction frequency (wind rose)
            public double[,] sectorWS_Dist; // Sectorwise wind speed distribution i = Sector num, j = WS interval
            public double[] WS_Dist; // Overall Wind speed distribution
        }

        [Serializable()]
        public struct Turbulence
        {
            public DateTime startTime;
            public DateTime endTime;
            public double[,] avgSD; // numWS, numWD
            public double[,] avgWS;
            public double[,] avgPlus1_28SD;
            public double[,] p90SD;
            public int[,] count;
        }

        public enum Season
        {
            Winter,
            Spring,
            Summer,
            Fall,
            All
        }

        public enum TOD
        {
            Day,
            Night,
            All
        }

        public int ExposureCount
        {
            get { if (expo == null)
                    return 0;
                else
                    return expo.Length; }
        }

        public int WSWD_DistCount
        {
            get
            {
                if (WSWD_Dists == null)
                    return 0;
                else
                    return WSWD_Dists.Length;
            }
        }

        public int ExtrapDataCount
        {
            get
            {
                if (metData == null)
                    return 0;
                else if (metData.simData == null)
                    return 0;
                else
                    return metData.simData.Length;
            }
        }

        public void AddExposure(int radius, double exponent, int numSectors)
        {
            // Add exposure to list
            int expCount = 0;
            if (expo != null) expCount = expo.Length;
            int insertIndex = 0;

            if (expCount > 0)
            {
                if (radius > expo[expCount - 1].radius)  // Larger radius than largest in list
                    insertIndex = expCount - 1;
                else {
                    for (int i = 0; i <= expCount - 2; i++)
                    {
                        if (expo[i].radius < radius && expo[i + 1].radius >= radius) {
                            insertIndex = i;
                            break;
                        }
                    }
                }

                Exposure[] existingExpos = new Exposure[expCount];
                for (int j = 0; j < expCount; j++)
                    existingExpos[j] = expo[j];

                expo = new Exposure[expCount + 1];

                for (int j = 0; j <= insertIndex; j++)
                    expo[j] = existingExpos[j];


                expo[insertIndex + 1] = new Exposure();
                expo[insertIndex + 1].radius = radius;
                expo[insertIndex + 1].exponent = exponent;
                expo[insertIndex + 1].numSectors = numSectors;

                for (int j = insertIndex + 2; j <= expCount; j++)
                    expo[j] = existingExpos[j - 1];
            }

            else {
                expo = new Exposure[1];
                expo[0] = new Exposure();
                expo[0].radius = radius;
                expo[0].exponent = exponent;
                expo[0].numSectors = numSectors;

            }

        }

        public void AddWSWD_DistFromTAB(TOD thisTOD, Season thisSeason, double height, double[,] sectorWS_Dist, double[] windRose)
        {
            // Adds wind speed / wind direction distribution to Met's list of WSWD_Dists
            
            if (AlreadyHaveWSWD_Dist(thisTOD, thisSeason, height) == false)
            {
                Array.Resize(ref WSWD_Dists, WSWD_DistCount + 1);
                WSWD_Dists[WSWD_DistCount - 1] = new WSWD_Dist();
                WSWD_Dists[WSWD_DistCount - 1].height = height;
                WSWD_Dists[WSWD_DistCount - 1].season = thisSeason;
                WSWD_Dists[WSWD_DistCount - 1].timeOfDay = thisTOD;
                WSWD_Dists[WSWD_DistCount - 1].sectorWS_Dist = sectorWS_Dist;
                WSWD_Dists[WSWD_DistCount - 1].windRose = windRose;
            }
        }

        public void AddWSWD_Dist(WSWD_Dist thisDist)
        {
            // Adds wind speed / wind direction distribution to Met's list of WSWD_Dists
            if (AlreadyHaveWSWD_Dist(thisDist.timeOfDay, thisDist.season, thisDist.height) == false)
            {
                Array.Resize(ref WSWD_Dists, WSWD_DistCount + 1);
                WSWD_Dists[WSWD_DistCount - 1] = thisDist;                
            }
        }

        public bool AlreadyHaveWSWD_Dist(TOD thisTOD, Season thisSeason, double height)
        {
            bool alreadyGotIt = false;

            for (int i = 0; i < WSWD_DistCount; i++)
                if (WSWD_Dists[i].height == height && WSWD_Dists[i].season == thisSeason && WSWD_Dists[i].timeOfDay == thisTOD)
                    alreadyGotIt = true;
            
            return alreadyGotIt;
        }

        public void ClearExposures()
        {
            // Clear all calculated exposure and SRDH
            expo = null;
        }

        public bool IsNewExposure(int radius, double exponent, int numSectors)
        {
            // Returns true if the exposure with specified radius and exponent has not been calculated

            bool isNew = true;
            if (expo == null)
                isNew = true;
            else
            {
                for (int i = 0; i < expo.Length; i++)
                {
                    if (expo[i].exponent == exponent && expo[i].radius == radius && expo[i].numSectors == numSectors) // the exposures based on radius and exp combo and number of sectors to avg already calculated
                    {
                        isNew = false;
                        break;
                    }                    
                }
            }

            return isNew;

        }

        public bool IsNewSRDH(int radius, double exponent, int numSectors)
        {

            bool isNew = true;
            if (expo == null)
                isNew = true;
            else
            {
                for (int i = 0; i < expo.Length; i++)
                {
                    if (expo[i].exponent == exponent && expo[i].radius == radius && expo[i].numSectors == numSectors && expo[i].SR != null)
                    {
                        isNew = false;
                        break;
                    }
                                       
                }
            }
            return isNew;
        }   
          
        public void CalcAvgWS(Continuum thisInst)
        {
            // Calculates overall average wind speed using sectorwise wind speed distribution and wind rose (i.e. TAB file)
            // ONLY USED FOR TAB FILE CALCS (i.e. all time of day and all seasons)
             
            int WSWDind = GetWS_WD_DistInd(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

            // first calc WS dist over all sectors
            int numWS = WSWD_Dists[WSWDind].sectorWS_Dist.GetUpperBound(1) + 1;
            WSWD_Dists[WSWDind].WS_Dist = new double[numWS];

            double sumRose = 0;

            for (int i = 0; i < numWS; i++)
            {
                sumRose = 0;
                for (int j = 0; j <= WSWD_Dists[WSWDind].sectorWS_Dist.GetUpperBound(0); j++)
                {
                    WSWD_Dists[WSWDind].WS_Dist[i] = WSWD_Dists[WSWDind].WS_Dist[i] + WSWD_Dists[WSWDind].sectorWS_Dist[j, i] * WSWD_Dists[WSWDind].windRose[j];
                    sumRose = sumRose + WSWD_Dists[WSWDind].windRose[j];
                }
                WSWD_Dists[WSWDind].WS_Dist[i] = WSWD_Dists[WSWDind].WS_Dist[i] / sumRose;
            }

            WSWD_Dists[WSWDind].WS = 0;
            double sumDist = 0;

            for (int i = 0; i < numWS; i++)
            {
                WSWD_Dists[WSWDind].WS = WSWD_Dists[WSWDind].WS + WSWD_Dists[WSWDind].WS_Dist[i] * (thisInst.metList.WS_FirstInt + i * thisInst.metList.WS_IntSize - thisInst.metList.WS_IntSize / 2);
                sumDist = sumDist + WSWD_Dists[WSWDind].WS_Dist[i];
            }

            WSWD_Dists[WSWDind].WS = WSWD_Dists[WSWDind].WS / sumDist;

        }

        public void CalcSectorWS_Ratios(Continuum thisInst)
        {
            // Calculate directional wind speed ratios from TAB file
            // ONLY USED FOR TAB FILE CALCS
            int WSWD_ind = GetWS_WD_DistInd(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
            if (WSWD_Dists[WSWD_ind].windRose == null || WSWD_Dists[WSWD_ind].WS == 0) return;

            int numWS = WSWD_Dists[WSWD_ind].WS_Dist.Length;
            int numWD = WSWD_Dists[WSWD_ind].windRose.Length;

            double[] WS_by_dir = new double[numWD];
            WSWD_Dists[WSWD_ind].sectorWS_Ratio = new double[numWD];
            
            double sumWS = 0;
                    
            for (int i = 0; i < numWD; i++)
            {
                sumWS = 0;
                for (int j = 0; j <= numWS - 1; j++)
                {
                    WS_by_dir[i] = WS_by_dir[i] + WSWD_Dists[WSWD_ind].sectorWS_Dist[i, j] * (thisInst.metList.WS_FirstInt + j * thisInst.metList.WS_IntSize - thisInst.metList.WS_IntSize / 2);
                    sumWS = sumWS + WSWD_Dists[WSWD_ind].sectorWS_Dist[i, j];
                }
                WS_by_dir[i] = WS_by_dir[i] / sumWS;
            }

            for (int i = 0; i < numWD; i++)
                WSWD_Dists[WSWD_ind].sectorWS_Ratio[i] = WS_by_dir[i] / WSWD_Dists[WSWD_ind].WS;
        }
            

        public void GetFlowSepNodes(NodeCollection nodeList, Continuum thisInst)
        {

            // if flow separation model is enabled, this function searches for points surrounding met where flow separation will occur             
            int numWD = thisInst.GetNumWD();

            Nodes thisNode = nodeList.GetMetNode(this);           
            flowSepNodes = nodeList.FindAllFlowSeps(thisNode, thisInst, numWD);
            
        }

        public WSWD_Dist GetWS_WD_Dist(double thisHeight, TOD thisTOD, Season thisSeason)
        {
            WSWD_Dist thisDist = new WSWD_Dist();

            for (int i = 0; i < WSWD_DistCount; i++)
                if (WSWD_Dists[i].height == thisHeight && WSWD_Dists[i].timeOfDay == thisTOD && WSWD_Dists[i].season == thisSeason)
                    thisDist = WSWD_Dists[i];
            
            return thisDist;
        }

        public int GetWS_WD_DistInd(double thisHeight, TOD thisTOD, Season thisSeason)
        {
            int thisInd = 0;

            for (int i = 0; i < WSWD_DistCount; i++)
                if (WSWD_Dists[i].height == thisHeight && WSWD_Dists[i].timeOfDay == thisTOD && WSWD_Dists[i].season == thisSeason)
                    thisInd = i;

            return thisInd;
        }

        public int GetSimDataInd(double modeledHeight)
        {
            int simInd = 0;

            for (int i = 0; i < ExtrapDataCount; i++)
                if (modeledHeight == metData.simData[i].height)
                    simInd = i;

            return simInd;
        }
                
        public WSWD_Dist CalcLT_WSWD_Dists(double thisHeight, TOD thisTOD, Season thisSeason, Continuum thisInst, MCP.Site_data[] LT_WS_Ests)
        {            
            WSWD_Dist thisDist = new WSWD_Dist();
            thisDist.height = thisHeight;
            thisDist.timeOfDay = thisTOD;
            thisDist.season = thisSeason;            
            thisDist.WS_Dist = new double[thisInst.metList.numWS];
            thisDist.windRose = new double[thisInst.metList.numWD];
            thisDist.sectorWS_Ratio = new double[thisInst.metList.numWD];
            thisDist.sectorWS_Dist = new double[thisInst.metList.numWD, thisInst.metList.numWS];

            int allCount = 0;
            int[] secCount = new int[thisInst.metList.numWD];

            // starting at This_Start, goes through LT WS Est data, until it reaches This_End,
            // and finds WD and WS/WD distributions  
            double sumDist = 0;

            for (int i = 0; i < LT_WS_Ests.Length; i++)            
            {
                Met.TOD siteDataTOD = thisInst.metList.GetTOD(LT_WS_Ests[i].thisDate);
                Met.Season siteDataSeason = thisInst.metList.GetSeason(LT_WS_Ests[i].thisDate);
                
                if ((thisTOD == Met.TOD.All || thisTOD == siteDataTOD) && (thisSeason == Met.Season.All || thisSeason == siteDataSeason))
                {
                    int WS_ind = mcp.Get_WS_ind(LT_WS_Ests[i].thisWS, 1);
                    int WD_ind = mcp.Get_WD_ind(LT_WS_Ests[i].thisWD);

                    if (WS_ind > 29) WS_ind = 29;

                    thisDist.windRose[WD_ind]++;
                    thisDist.WS_Dist[WS_ind]++;
                    thisDist.sectorWS_Dist[WD_ind, WS_ind]++;
                    sumDist++;

                    thisDist.WS = thisDist.WS + LT_WS_Ests[i].thisWS;
                    allCount++;

                    thisDist.sectorWS_Ratio[WD_ind] = thisDist.sectorWS_Ratio[WD_ind] + LT_WS_Ests[i].thisWS;
                    secCount[WD_ind]++;

                    if (WD_ind == 4)
                        WD_ind = 4;
                                        
                }                
            }

            if (sumDist > 0)
                for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                    thisDist.WS_Dist[WS_ind] = thisDist.WS_Dist[WS_ind] / sumDist;

            // Calculate wind speed overall and sectorwise distributions
            if (sumDist > 0)
                for (int i = 0; i < thisInst.metList.numWD; i++)            
                    thisDist.windRose[i] = thisDist.windRose[i] / sumDist;                        
            
            for (int WD_ind = 0; WD_ind < thisInst.metList.numWD; WD_ind++)
            {
                double sumWS = 0;
                for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                    sumWS = sumWS + thisDist.sectorWS_Dist[WD_ind, WS_ind];

                for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                    thisDist.sectorWS_Dist[WD_ind, WS_ind] = thisDist.sectorWS_Dist[WD_ind, WS_ind] / sumWS;
            }                      
            
            // Calculate sectorwise and overall wind speeds
            
            if (allCount > 0)
                thisDist.WS = thisDist.WS / allCount;

            for (int i = 0; i < thisInst.metList.numWD; i++)
            {
                if (secCount[i] > 0 && thisDist.WS > 0)
                    thisDist.sectorWS_Ratio[i] = thisDist.sectorWS_Ratio[i] / secCount[i] / thisDist.WS;
            }

            return thisDist;
        }

        public WSWD_Dist CalcMeas_WSWD_Dists(double thisHeight, TOD thisTOD, Season thisSeason, Continuum thisInst, Met_Data_Filter.Sim_TS extrapData)
        {           
            WSWD_Dist thisDist = new WSWD_Dist();
            thisDist.height = thisHeight;
            thisDist.timeOfDay = thisTOD;
            thisDist.season = thisSeason;
            thisDist.WS_Dist = new double[thisInst.metList.numWS];
            thisDist.windRose = new double[thisInst.metList.numWD];
            thisDist.sectorWS_Ratio = new double[thisInst.metList.numWD];
            thisDist.sectorWS_Dist = new double[thisInst.metList.numWD, thisInst.metList.numWS];
            MCP mcp = new MCP(); // For Get_WS_ind and Get_WD_ind functions
            mcp.New_MCP(false, false, thisInst);

            int allCount = 0;
            int[] secCount = new int[thisInst.metList.numWD];

            // starting at This_Start, goes through LT WS Est data, until it reaches This_End,
            // and finds WD and WS/WD distributions                 

            for (int i = 0; i < extrapData.WS_WD_data.Length; i++)
            {
                Met.TOD siteDataTOD = thisInst.metList.GetTOD(extrapData.WS_WD_data[i].timeStamp);
                Met.Season siteDataSeason = thisInst.metList.GetSeason(extrapData.WS_WD_data[i].timeStamp);

                if ((thisTOD == Met.TOD.All || thisTOD == siteDataTOD) && (thisSeason == Met.Season.All || thisSeason == siteDataSeason))
                {
                    int WS_ind = mcp.Get_WS_ind(extrapData.WS_WD_data[i].WS, 1);
                    int WD_ind = mcp.Get_WD_ind(extrapData.WS_WD_data[i].WD);

                    if (WS_ind > 29) WS_ind = 29;
                                        
                    if (extrapData.WS_WD_data[i].WS != -999 && extrapData.WS_WD_data[i].WD != -999)
                    {
                        thisDist.windRose[WD_ind]++;
                        thisDist.WS_Dist[WS_ind]++;
                        thisDist.sectorWS_Dist[WD_ind, WS_ind]++;

                        thisDist.WS = thisDist.WS + extrapData.WS_WD_data[i].WS;
                        allCount++;

                        thisDist.sectorWS_Ratio[WD_ind] = thisDist.sectorWS_Ratio[WD_ind] + extrapData.WS_WD_data[i].WS;
                        secCount[WD_ind]++;
                    }
                    
                }
            }

            if (allCount > 0)
                for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                    thisDist.WS_Dist[WS_ind] = thisDist.WS_Dist[WS_ind] / allCount;

            // Calculate wind speed overall and sectorwise distributions
            double sumWD = 0;
            for (int i = 0; i < thisInst.metList.numWD; i++)
                sumWD = sumWD + thisDist.windRose[i];

            for (int i = 0; i < thisInst.metList.numWD; i++)
                thisDist.windRose[i] = thisDist.windRose[i] / sumWD;

            for (int WD_ind = 0; WD_ind < thisInst.metList.numWD; WD_ind++)
            {
                double sumWS = 0;
                for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                    sumWS = sumWS + thisDist.sectorWS_Dist[WD_ind, WS_ind];

                for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                    thisDist.sectorWS_Dist[WD_ind, WS_ind] = thisDist.sectorWS_Dist[WD_ind, WS_ind] / sumWS;
            }
                        
            double[] sumSectorDist = new double[thisInst.metList.numWD];
                        
            // Calculate sectorwise and overall wind speeds

            if (allCount > 0)
                thisDist.WS = thisDist.WS / allCount;

            for (int i = 0; i < thisInst.metList.numWD; i++)
            {
                if (secCount[i] > 0 && thisDist.WS > 0)
                    thisDist.sectorWS_Ratio[i] = thisDist.sectorWS_Ratio[i] / secCount[i] / thisDist.WS;
            }

            return thisDist;
        }

        public void CalcAllLT_WSWD_Dists(Continuum thisInst, MCP.Site_data[] LT_WS_Ests)
        {
            // Calculates all wind speed / wind direction distributions and saves to list
            if (mcp.gotMCP_Est == false)
                return;

            WSWD_Dist thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.All, Season.All, thisInst, LT_WS_Ests);
            AddWSWD_Dist(thisDist);
            
            if (mcp.numTODs > 1 && mcp.numSeasons == 1) // Using diurnal models but not seasonal
            {
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.Day, Season.All, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);
                
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.Night, Season.All, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);                
            }
            else if (mcp.numTODs == 1 && mcp.numSeasons > 1) // Using seasonal models but not diurnal
            {
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.All, Season.Winter, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);
                
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.All, Season.Spring, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);
                
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.All, Season.Summer, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);
                
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.All, Season.Fall, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);                
            }
            else if (mcp.numTODs > 1 && mcp.numSeasons > 1) // Using seasonal and diurnal models
            {
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.Day, Season.Winter, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);
                
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.Day, Season.Spring, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);
                
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.Day, Season.Summer, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);
                
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.Day, Season.Fall, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);
                
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.Night, Season.Winter, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);
                
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.Night, Season.Spring, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);
                
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.Night, Season.Summer, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);
                
                thisDist = CalcLT_WSWD_Dists(thisInst.modeledHeight, TOD.Night, Season.Fall, thisInst, LT_WS_Ests);
                AddWSWD_Dist(thisDist);                
            }            
            
        }

        public void CalcAllMeas_WSWD_Dists(Continuum thisInst, Met_Data_Filter.Sim_TS extrapData)
        {
            // Calculates all wind speed / wind direction distributions and saves to list
            if (extrapData.WS_WD_data == null)
                return;

            WSWD_Dist thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.All, Season.All, thisInst, extrapData);
            AddWSWD_Dist(thisDist);

            if (thisInst.metList.numTOD > 1 && thisInst.metList.numSeason == 1) // Using diurnal models but not seasonal
            {
                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.Day, Season.All, thisInst, extrapData);
                AddWSWD_Dist(thisDist);

                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.Night, Season.All, thisInst, extrapData);
                AddWSWD_Dist(thisDist);
            }
            else if (thisInst.metList.numTOD == 1 && thisInst.metList.numSeason > 1) // Using seasonal models but not diurnal
            {
                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.All, Season.Winter, thisInst, extrapData);
                AddWSWD_Dist(thisDist);

                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.All, Season.Spring, thisInst, extrapData);
                AddWSWD_Dist(thisDist);

                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.All, Season.Summer, thisInst, extrapData);
                AddWSWD_Dist(thisDist);

                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.All, Season.Fall, thisInst, extrapData);
                AddWSWD_Dist(thisDist);
            }
            else if (thisInst.metList.numTOD > 1 && thisInst.metList.numSeason > 1) // Using seasonal and diurnal models
            {
                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.Day, Season.Winter, thisInst, extrapData);
                AddWSWD_Dist(thisDist);

                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.Day, Season.Spring, thisInst, extrapData);
                AddWSWD_Dist(thisDist);

                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.Day, Season.Summer, thisInst, extrapData);
                AddWSWD_Dist(thisDist);

                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.Day, Season.Fall, thisInst, extrapData);
                AddWSWD_Dist(thisDist);

                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.Night, Season.Winter, thisInst, extrapData);
                AddWSWD_Dist(thisDist);

                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.Night, Season.Spring, thisInst, extrapData);
                AddWSWD_Dist(thisDist);

                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.Night, Season.Summer, thisInst, extrapData);
                AddWSWD_Dist(thisDist);

                thisDist = CalcMeas_WSWD_Dists(thisInst.modeledHeight, TOD.Night, Season.Fall, thisInst, extrapData);
                AddWSWD_Dist(thisDist);
            }

        }

        public double CalcWS_inOneSectorFromWSWD_Dist(WSWD_Dist thisDist, int WD_Ind)
        {
            // Calculates and returns the average wind speed in wind direction sector, WD_Ind
            double thisWS = 0;

            if (thisDist.sectorWS_Ratio == null)
                return thisWS;

            if (thisDist.sectorWS_Ratio.Length <= WD_Ind)
                return thisWS;

            thisWS = thisDist.sectorWS_Ratio[WD_Ind] * thisDist.WS;
            
            return thisWS;
        }

        public int GetTOD_Ind(int numTODs, TOD thisTOD)
        {
            int thisInd = 0;

            if (numTODs != 1)
            {
                if (thisTOD == TOD.Night)
                    thisInd = 1;
            }

            return thisInd;
        }

        public int GetSeasonInd(int numSeasons, Season thisSeaon)
        {
            int thisInd = 0;

            if (numSeasons != 1)
            {
                if (thisSeaon == Season.Winter)
                    thisInd = 0;
                else if (thisSeaon == Season.Spring)
                    thisInd = 1;
                else if (thisSeaon == Season.Summer)
                    thisInd = 2;
                else if (thisSeaon == Season.Fall)
                    thisInd = 3;
            }

            return thisInd;
        }

        public struct Array_of_SDs
        {
            public double[] SDs;
        }

        public void CalcTurbulenceIntensity(DateTime startTime, DateTime endTime, double height, Continuum thisInst)
        {
            // Bins the data by WS and WD and calculates the average standard deviation, average WS, avg + 1.28 SD, and P90 SD
            // Uses extrapolated wind speed and standard deviation at height closest to extrapolated height

            turbulence.startTime = startTime;
            turbulence.endTime = endTime;

            if (mcp == null)
                mcp = new MCP();

            if (metData == null)
                return;

            Met_Data_Filter.Sim_TS extrapData = metData.GetSimulatedTimeSeries(height);                 
                      
            int vaneInd = metData.GetVaneClosestToHH(height);
            Met_Data_Filter.Vane_Data vane = metData.vanes[vaneInd]; 

            int timeInd = 0;

            while (extrapData.WS_WD_data[timeInd].timeStamp < startTime)
                timeInd++;

            int numWD = thisInst.metList.numWD;
            int numWS = thisInst.metList.numWS;

            turbulence.avgSD = new double[numWS, numWD];
            turbulence.avgWS = new double[numWS, numWD];
            turbulence.avgPlus1_28SD = new double[numWS, numWD];
            turbulence.p90SD = new double[numWS, numWD];
            turbulence.count = new int[numWS, numWD];
            Array_of_SDs[,] arrayOfSDs = new Array_of_SDs[numWS, numWD];

            while (extrapData.WS_WD_data[timeInd].timeStamp <= endTime)
            {
                if (vane.dirData[timeInd].filterFlag == Met_Data_Filter.Filter_Flags.Valid && extrapData.WS_WD_data[timeInd].WS != -999
                    && extrapData.WS_WD_data[timeInd].SD != -999 && extrapData.WS_WD_data[timeInd].SD < extrapData.WS_WD_data[timeInd].WS / 3)
                {
                    int WD_ind = metData.GetWD_Ind(vane.dirData[timeInd].avg, numWD);  
                    int WS_ind = mcp.Get_WS_ind(extrapData.WS_WD_data[timeInd].WS, thisInst.metList.WS_IntSize);                                      

                    turbulence.avgSD[WS_ind, WD_ind] = turbulence.avgSD[WS_ind, WD_ind] + extrapData.WS_WD_data[timeInd].SD;
                    turbulence.avgWS[WS_ind, WD_ind] = turbulence.avgWS[WS_ind, WD_ind] + extrapData.WS_WD_data[timeInd].WS;
                    turbulence.count[WS_ind, WD_ind]++;
                    int binCount = turbulence.count[WS_ind, WD_ind];

                    Array.Resize(ref arrayOfSDs[WS_ind, WD_ind].SDs, binCount);
                    arrayOfSDs[WS_ind, WD_ind].SDs[binCount - 1] = extrapData.WS_WD_data[timeInd].SD;
                }

                timeInd++;

                if (timeInd >= extrapData.WS_WD_data.Length)
                    break;
            }

            for (int i = 0; i < numWS; i++)
                for (int j = 0; j < numWD; j++)
                {
                    if (turbulence.count[i,j] > 2)
                    {
                        turbulence.avgWS[i, j] = turbulence.avgWS[i, j] / turbulence.count[i, j];
                        turbulence.avgSD[i, j] = turbulence.avgSD[i, j] / turbulence.count[i, j];
                        turbulence.avgPlus1_28SD[i, j] = turbulence.avgWS[i, j] + 1.28 * turbulence.avgSD[i, j];

                        Array.Sort(arrayOfSDs[i, j].SDs);
                        int P90 = (int)Math.Round(arrayOfSDs[i, j].SDs.Length * 0.9) - 1;
                        double thisP90 = arrayOfSDs[i, j].SDs[P90];
                        turbulence.p90SD[i, j] = thisP90;
                    }                    
                }
        }

        public struct TIandCount
        {
            public double overallTI;
            public int count;
        } 

        public TIandCount[] CalcOverallTurbulenceIntensity(string turbType, Continuum thisInst)
        {
            // Calculates and returns overall turbulence intensity. Either average or representative
            TIandCount[] overallTI = new TIandCount[thisInst.metList.numWS];

            if (turbulence.count == null)
            {
                DateTime startTime = thisInst.dateTIStart.Value;
                DateTime endTime = thisInst.dateTIEnd.Value;
                CalcTurbulenceIntensity(startTime, endTime, thisInst.modeledHeight, thisInst);
            }                       

            for (int WS_Ind = 0; WS_Ind < thisInst.metList.numWS; WS_Ind++)
            {                
                double avgWS = 0;

                for (int WD_Ind = 0; WD_Ind < thisInst.metList.numWD; WD_Ind++)
                {
                    if (turbulence.count[WS_Ind, WD_Ind] > 2)
                    {
                        if (turbType == "Average")
                            overallTI[WS_Ind].overallTI = overallTI[WS_Ind].overallTI + turbulence.avgSD[WS_Ind, WD_Ind] * turbulence.count[WS_Ind, WD_Ind];
                        else if (turbType == "Representative")
                            overallTI[WS_Ind].overallTI = overallTI[WS_Ind].overallTI + turbulence.p90SD[WS_Ind, WD_Ind] * turbulence.count[WS_Ind, WD_Ind];

                        avgWS = avgWS + turbulence.avgWS[WS_Ind, WD_Ind] * turbulence.count[WS_Ind, WD_Ind];
                        overallTI[WS_Ind].count = overallTI[WS_Ind].count + turbulence.count[WS_Ind, WD_Ind];
                    }
                }

                if (overallTI[WS_Ind].count > 0)
                {
                    avgWS = avgWS / overallTI[WS_Ind].count;                   
                    overallTI[WS_Ind].overallTI = overallTI[WS_Ind].overallTI / overallTI[WS_Ind].count / avgWS;
                }
            }

            return overallTI;
        }

        public double[] GetAlphaHistogram(string WS_Range, Continuum thisInst, DateTime startTime, DateTime endTime)
        {            
            double minAlpha = -0.5;
            double alphaInt = 0.02;
            double maxAlpha = 1.5;
            int numHisto = (int)Math.Round((maxAlpha - minAlpha) / alphaInt + 1, 0);
            double[] alphaHisto = new double[numHisto];

            if (metData == null)
                return alphaHisto;

            Met_Data_Filter.Sim_TS extrapData = metData.GetSimulatedTimeSeries(thisInst.modeledHeight);

            if (extrapData.WS_WD_data == null)
                return alphaHisto;

            int startInd = 0;
            while (extrapData.WS_WD_data[startInd].timeStamp < startTime && startInd < extrapData.WS_WD_data.Length - 1)
                startInd++;

            int endInd = 0;
            while (extrapData.WS_WD_data[endInd].timeStamp < endTime && endInd < extrapData.WS_WD_data.Length - 1)
                endInd++;
                      
            for (int i = startInd; i < endInd; i++)
            {
                bool useAlpha = false;

                if (extrapData.WS_WD_data[i].alpha != -999)
                {

                    if (WS_Range == "5 - 10 m/s" && extrapData.WS_WD_data[i].WS >= 5 && extrapData.WS_WD_data[i].WS <= 10)
                        useAlpha = true;
                    else if (WS_Range == "10 - 15 m/s" && extrapData.WS_WD_data[i].WS >= 10 && extrapData.WS_WD_data[i].WS <= 15)
                        useAlpha = true;
                    else if (WS_Range == "15+ m/s" && extrapData.WS_WD_data[i].WS >= 15)
                        useAlpha = true;
                    else if (WS_Range == "All > Cut-In" && extrapData.WS_WD_data[i].WS >= 3)
                        useAlpha = true;

                    if (useAlpha == true)
                    {                      

                        int histoInd = (int)Math.Round((extrapData.WS_WD_data[i].alpha - minAlpha) / alphaInt, 0, MidpointRounding.AwayFromZero);
                        if (histoInd < 0) histoInd = 0;
                        if (histoInd >= numHisto) histoInd = numHisto - 1;
                        alphaHisto[histoInd]++;                                             
                                                
                    }
                }
            }                     
            
            return alphaHisto;
        }

        public double GetAlphaPValue(double minWS, double maxWS, int pLevel, Continuum thisInst, DateTime startTime, DateTime endTime)
        {
            // Finds alpha between min and max WS, sorts alpha and then finds and returns P-Value
            double alphaPVal = 0;
            int alphaCount = 0;

            if (metData == null)
                return alphaPVal;

            if (metData.anems[0].windData == null)
                metData.GetSensorDataFromDB(thisInst, name);

            if (metData.alpha.Length == 0)
            {
                metData.EstimateAlpha();
                metData.ExtrapolateData(thisInst.modeledHeight);
            }

            Met_Data_Filter.Sim_TS extrapData = metData.GetSimulatedTimeSeries(thisInst.modeledHeight);

            int startInd = 0;
            // Advance to startTime
            while (extrapData.WS_WD_data[startInd].timeStamp < startTime && startInd < extrapData.WS_WD_data.Length - 1)
                startInd++;

            int endInd = 0;
            // Figure out index of endTime
            while (extrapData.WS_WD_data[endInd].timeStamp < endTime && endInd < extrapData.WS_WD_data.Length - 1)
                endInd++;

            // Figure out how big to make the array
            for (int i = startInd; i <= endInd; i++)
                if (extrapData.WS_WD_data[i].WS >= minWS && extrapData.WS_WD_data[i].WS <= maxWS && extrapData.WS_WD_data[i].WS != -999)
                    alphaCount++;

            double[] alphaArray = new double[alphaCount];
            alphaCount = 0;

            for (int i = startInd; i <= endInd; i++)
                if (extrapData.WS_WD_data[i].WS >= minWS && extrapData.WS_WD_data[i].WS <= maxWS && extrapData.WS_WD_data[i].WS != -999)
                {
                    alphaArray[alphaCount] = extrapData.WS_WD_data[i].alpha;
                    alphaCount++;
                }

            if (alphaCount > 1)
            {
                Array.Sort(alphaArray);
                int pInd = (int)Math.Round(alphaCount * (100 - pLevel) / 100.0, 0);
                if (pInd >= alphaCount)
                    pInd = alphaCount - 1;

                alphaPVal = alphaArray[pInd];
            }

            return alphaPVal;
        }

        public struct Extreme_WindSpeed
        {
            public double tenMin50yr;
            public double tenMin1yr;
            public double gust50yr;
            public double gust1yr;

            public double[] yearsOfOcc;
            public double[] maxTenMin;
            public double[] maxGust;
        }

        public struct MaxYearlyWind
        {
            public double maxWS;
            public int thisYear;
        }

        public MaxYearlyWind[] GetMaxYearlyWinds(string tenMinOrGust, Continuum thisInst)
        {
            MaxYearlyWind[] maxYearlyWinds = new MaxYearlyWind[0];
            int dataInd = 0;

            // Get anems closest to hub height
            int[] anemInds = metData.GetAnemsClosestToHH(thisInst.modeledHeight);
            Met_Data_Filter.Anem_Data anem1 = metData.anems[anemInds[0]];
            Met_Data_Filter.Anem_Data anem2 = new Met_Data_Filter.Anem_Data();
            if (anemInds[1] != -999)
                anem2 = metData.anems[anemInds[1]];

            // Go to first Jan 1
            
            while (anem1.windData[dataInd].timeStamp.Month != 1 || anem1.windData[dataInd].timeStamp.Day != 1)
                dataInd++;

            int lastYear = anem1.windData[dataInd].timeStamp.Year;

            // Check to see that end data is after 12/31 of year
            if (anem1.windData[anem1.windData.Length - 1].timeStamp.Year == lastYear)
                return maxYearlyWinds;
                            
            double maxWS = 0;            
            int yearInd = 0;

            for (int i = dataInd; i < anem1.windData.Length; i++)
            {
                int thisYear = anem1.windData[i].timeStamp.Year;

                if (thisYear == lastYear)
                {
                    if (tenMinOrGust == "10-min")
                    {
                        if (anem1.windData[i].avg > maxWS)
                            maxWS = anem1.windData[i].avg;
                        if (anemInds[1] != -999)
                            if (anem2.windData[i].avg > maxWS)
                                maxWS = anem1.windData[i].avg;
                    }
                    else
                    {
                        if (anem1.windData[i].max > maxWS)
                            maxWS = anem1.windData[i].max;
                        if (anemInds[1] != -999)
                            if (anem2.windData[i].max > maxWS)
                                maxWS = anem1.windData[i].max;
                    }
                }
                else
                {
                    Array.Resize(ref maxYearlyWinds, yearInd + 1);
                    maxYearlyWinds[yearInd].maxWS = maxWS;
                    maxYearlyWinds[yearInd].thisYear = lastYear;

                    if (tenMinOrGust == "10-min")
                    {
                        maxWS = anem1.windData[i].avg;
                        if (anemInds[1] != -999)
                            if (anem2.windData[i].avg > maxWS)
                                maxWS = anem1.windData[i].avg;
                    }
                    else
                    {
                        maxWS = anem1.windData[i].max;
                        if (anemInds[1] != -999)
                            if (anem2.windData[i].max > maxWS)
                                maxWS = anem1.windData[i].max;
                    }

                    lastYear = thisYear;
                    yearInd++;
                }
            }

            // If dataset ends on 12/31, use max wind speed in list (otherwise don't since it's not a full year)
            if (anem1.windData[anem1.windData.Length - 1].timeStamp.Month == 12 && anem1.windData[anem1.windData.Length - 1].timeStamp.Day == 31)
            {
                Array.Resize(ref maxYearlyWinds, yearInd + 1);
                maxYearlyWinds[yearInd].maxWS = maxWS;
                maxYearlyWinds[yearInd].thisYear = lastYear;
            }

            return maxYearlyWinds;
        }

        public Extreme_WindSpeed CalcExtremeWindSpeeds(Continuum thisInst)
        {
            // Calculates and returns extreme wind speeds estimates (1 yr and 50 yr, 10-min and Gust)
            Extreme_WindSpeed extremeWinds = new Extreme_WindSpeed();                                 

            // Get MERRA2 data used for this met
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(UTMX, UTMY);
            int UTC_offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);

            if (thisInst.merraList.GotMERRA(theseLL.latitude, theseLL.longitude) == false)
                return extremeWinds;
            
            MERRA thisMERRA = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
            
            int refLength = thisMERRA.interpData.TS_Data.Length;

            if (thisMERRA.interpData.TS_Data.Length == 0)
            {
                thisMERRA.GetMERRADataFromDB(thisInst);
                thisMERRA.GetInterpData(thisInst.UTM_conversions);
            }

            if (thisMERRA.interpData.TS_Data.Length == 0)
                return extremeWinds;

            if (metData.GetNumAnems() > 0)
            {
                if (metData.anems[0].windData == null)
                    metData.GetSensorDataFromDB(thisInst, name);

                if (metData.anems[0].windData == null)
                    return extremeWinds;
            }
            else
                return extremeWinds;

            // Create array of max hourly wind speed every year (MERRA2 data)
            MaxYearlyWind[] maxHourlyRefWS = thisMERRA.GetMaxHourlyWindSpeeds();

            // Find max 10-min and max gust for every full year of data (Jan - Dec)            
            MaxYearlyWind[] maxMetTenMin = GetMaxYearlyWinds("10-min", thisInst);
            MaxYearlyWind[] maxMetGust = GetMaxYearlyWinds("Gust", thisInst);

            if (maxMetTenMin.Length == 0)
            {
              //  MessageBox.Show("There are no full years of data (i.e. Jan. 1 to Dec. 31) to use in extreme WS calculations.", "Continum 3");
                return extremeWinds;
            }

            // Calculate the average of max 10-mins
            double avgTenMin = 0;
            for (int i = 0; i < maxMetTenMin.Length; i++)
                avgTenMin = avgTenMin + maxMetTenMin[i].maxWS;

            avgTenMin = avgTenMin / maxMetTenMin.Length;

            if (maxMetGust.Length == 0)
            {
              //  MessageBox.Show("There are no full years of data (i.e. Jan. 1 to Dec. 31) to use in extreme WS calculations.", "Continum 3");
                return extremeWinds;
            }

            // Calculate the average of max 10-mins
            double avgGust = 0;
            for (int i = 0; i < maxMetGust.Length; i++)
                avgGust = avgGust + maxMetGust[i].maxWS;

            avgGust = avgGust / maxMetGust.Length;

            // Calculate the average of max hourly values for same years as met data
            double avgHourly = 0;
            int numYears = 0;

            for (int i = 0; i < maxHourlyRefWS.Length; i++)
            {
                bool haveThisYear = false;
                for (int j = 0; j < maxMetTenMin.Length; j++)
                    if (maxHourlyRefWS[i].thisYear == maxMetTenMin[j].thisYear)
                        haveThisYear = true;

                if (haveThisYear == true)
                {
                    avgHourly = avgHourly + maxHourlyRefWS[i].maxWS;
                    numYears++;
                }                    
            }

            if (numYears == 0)
            {
                MessageBox.Show("The reference data years don't coincide with the met data. Cannot calculate extreme wind speeds.", "Continuum 3.0");
                return extremeWinds;
            }

            avgHourly = avgHourly / numYears;

            if (avgHourly == 0)
            {
                MessageBox.Show("Reference wind speeds are zero.", "Continuum 3.0");
                return extremeWinds;
            }

            // Calculate ratio of avg hourly and avg 10-mins
            double hourlyToTenMin = avgTenMin / avgHourly;

            // Convert hourly max WS to 10-min estimated max WS
            MaxYearlyWind[] maxTenMinWS = new MaxYearlyWind[maxHourlyRefWS.Length];

            for (int i = 0; i < maxHourlyRefWS.Length; i++)
            {
                maxTenMinWS[i].maxWS = maxHourlyRefWS[i].maxWS * hourlyToTenMin;
                maxTenMinWS[i].thisYear = maxHourlyRefWS[i].thisYear;
            }

            // Calculate ratio of avg 10-min and avg gust
            double tenMinToGust = avgGust / avgTenMin;

            // Convert hourly max WS to 10-min estimated max WS
            MaxYearlyWind[] maxGustWS = new MaxYearlyWind[maxHourlyRefWS.Length];

            for (int i = 0; i < maxHourlyRefWS.Length; i++)
            {
                maxGustWS[i].maxWS = maxTenMinWS[i].maxWS * tenMinToGust;
                maxGustWS[i].thisYear = maxTenMinWS[i].thisYear;
            }

            // Calculate average and standard deviation of 10-min max WS and average gusts
            double[] tenMin = new double[maxHourlyRefWS.Length];
            for (int i = 0; i < maxHourlyRefWS.Length; i++)
                tenMin[i] = maxTenMinWS[i].maxWS;

            double avgMaxTenMin = thisInst.topo.FindAvg(tenMin);
            double stDevMaxTenMin = thisInst.topo.FindSD(tenMin);                      

            double[] gust = new double[maxHourlyRefWS.Length];
            for (int i = 0; i < maxHourlyRefWS.Length; i++)
                gust[i] = maxGustWS[i].maxWS;

            double avgMaxGust = thisInst.topo.FindAvg(gust);
            double stDevMaxGust = thisInst.topo.FindSD(gust);

            // Calculate beta and mu for extreme WS model
            double alphaTenMin = stDevMaxTenMin * Math.Pow(6, 0.5) / Math.PI;
            double alphaGust = stDevMaxGust * Math.Pow(6, 0.5) / Math.PI;

            double betaTenMin = avgMaxTenMin - 0.577 * alphaTenMin;
            double betaGust = avgMaxGust - 0.577 * alphaGust;

            // Calculate 1yr/50yr max ten-min and max gust WS
            extremeWinds.tenMin1yr = avgMaxTenMin;
            extremeWinds.gust1yr = avgMaxGust;
            extremeWinds.tenMin50yr = betaTenMin - alphaTenMin * Math.Log(-Math.Log(1 - 1 / 50.0));            
            extremeWinds.gust50yr = betaGust - alphaGust * Math.Log(-Math.Log(1 - 1 / 50.0));

            // Calculate max WS vs years of Recurrence arrays
            extremeWinds.yearsOfOcc = new double[98];
            extremeWinds.maxTenMin = new double[98];
            extremeWinds.maxGust = new double[98];

            for (int i = 0; i < 98; i++)
            {                
                extremeWinds.yearsOfOcc[i] = 1.5 + 0.5 * i;
                extremeWinds.maxTenMin[i] = betaTenMin - alphaTenMin * Math.Log(-Math.Log(1 - 1/ extremeWinds.yearsOfOcc[i]));
                extremeWinds.maxGust[i] = betaGust - alphaGust * Math.Log(-Math.Log(1 - 1 / extremeWinds.yearsOfOcc[i]));
            }                       

            return extremeWinds;
        }
        
    }
}
