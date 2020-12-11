using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary> Class that holds all met site information and calculated/estimated values generated at met site. Class contains the met site location and the terrain
    /// exposure, surface roughness, displacement, and terrain complexity calculated in each wind direction sector and using a range of radius of investigation. Class
    /// contains the time series met data (if any) including the applied QC filters, shear exponents, and extrapolated estimates. Class contains results of MCP (long-term
    /// estimation) analysis and turublence intensity calcuated from time series data.</summary>
    [Serializable()]
    public class Met
    {
        /// <summary> Name of met site </summary>
        public string name;
        /// <summary> List of wind speed and wind direction distributions (for each height, time of day, and season) </summary>
        public WSWD_Dist[] WSWD_Dists;
        /// <summary> UTM X Coordinate, m </summary>
        public double UTMX;
        /// <summary> UTM Y Coordinate, m </summary>
        public double UTMY;
        /// <summary> Elevation, m </summary>
        public double elev;
        /// <summary> Calculated exposure, surface roughness, and displacement height calculated using a range of ROI </summary>
        public Exposure[] expo;
        /// <summary> Calculated terrain complexity (P10 Exposure) using a range of ROI </summary>
        public Grid_Info gridStats = new Grid_Info();
        /// <summary>  If flow separation model enabled, nodes surrounding met where flow separation occurs </summary>
        public NodeCollection.Sep_Nodes[] flowSepNodes;
        /// <summary> Met time series data </summary>
        public Met_Data_Filter metData;
        /// <summary> MCP (Measure-Correlate-Predict) long-term estimate </summary>
        public MCP mcp;
        /// <summary> Measured turbulence intensity (from time series data) </summary>
        public Turbulence turbulence;
                
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary> Contains wind speed and wind direction distributions for each WD sector for specified time of day, season, and extrapolated height </summary>
        [Serializable()]
        public struct WSWD_Dist
        {
            /// <summary> Time of day: Day, Night, or All </summary>
            public TOD timeOfDay;
            /// <summary> Season :Winter, Spring, Summer, Fall, or All </summary>
            public Season season;
            /// <summary> Overall average wind speed </summary>
            public double WS;
            /// <summary> Height of WS/WD </summary>
            public double height;
            /// <summary>  Directional wind speed ratios </summary>
            public double[] sectorWS_Ratio;
            /// <summary>  Wind direction frequency (wind rose) </summary>
            public double[] windRose;
            /// <summary>  Sectorwise wind speed distribution i = Sector num, j = WS interval </summary>
            public double[,] sectorWS_Dist;
            /// <summary>  Overall Wind speed distribution </summary>
            public double[] WS_Dist;
        }

        /// <summary> Holds average and representative turbulence intensity (calculated between start/end times) in each direction sector </summary>
        [Serializable()]
        public struct Turbulence
        {
            /// <summary>  Interval start time </summary>
            public DateTime startTime;
            /// <summary>  Interval end time </summary>
            public DateTime endTime;
            /// <summary>  Wind speed standard deviation in each WS and WD bin (i = WS; j = WD) </summary>
            public double[,] avgSD;
            /// <summary>  Average wind speed in each WS and WD bin (i = WS; j = WD) </summary>
            public double[,] avgWS;
            /// <summary>  Average plus 1.28 * wind speed standard deviation in each WS and WD bin (i = WS; j = WD) </summary>
            public double[,] avgPlus1_28SD;
            /// <summary>  P90 wind speed standard deviation in each WS and WD bin (i = WS; j = WD) </summary>
            public double[,] p90SD;
            /// <summary>  Data count in each WS and WD bin (i = WS; j = WD) </summary>
            public int[,] count;
        }

        /// <summary> Holds array of wind speed standard deviations </summary>
        public struct Array_of_SDs
        {
            /// <summary> Array of wind speed standard deviations </summary>
            public double[] SDs;
        }

        /// <summary> Holds overall TI and data count </summary>
        public struct TIandCount
        {
            /// <summary> Overall turbulence intensity </summary>
            public double overallTI;
            /// <summary> Data count </summary>
            public int count;
        }

        /// <summary> Holds array of measured maximum wind speeds and 1-year/50-year estimated long-term extreme wind speed. </summary>
        public struct Extreme_WindSpeed
        {
            /// <summary> 50-year extreme 10-minute wind speed estimate </summary>
            public double tenMin50yr;
            /// <summary> 1-year extreme 10-minute wind speed estimate </summary>
            public double tenMin1yr;
            /// <summary> 50-year extreme gust estimate </summary>
            public double gust50yr;
            /// <summary> 1-year extreme gust estimate </summary>
            public double gust1yr;
            /// <summary> Full years of met data </summary>
            public double[] yearsOfOcc;
            /// <summary> Maximum ten-minute WS by year </summary>
            public double[] maxTenMin;
            /// <summary> Maximum WS gusts by year </summary>
            public double[] maxGust;
        }

        /// <summary> Holds maximum wind speed and year of measurement. </summary>
        public struct MaxYearlyWind
        {
            /// <summary> Maximum measured wind speed. </summary>
            public double maxWS;
            /// <summary> Year of measurement. </summary>
            public int thisYear; 
        }

        
        /// <summary> Season enumeration: Winter, Spring, Summer, Fall, All </summary>
        public enum Season
        {
            Winter,
            Spring,
            Summer,
            Fall,
            All
        }

        /// <summary> Time of day enumeration: Day, Night, All </summary>
        public enum TOD
        {
            Day, // If hour >= MetCollection.dayStartHour and <= hour < MetCollection.dayEndHour
            Night, // Hours outside daytime hours
            All // All hours
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Returns number of exposures calculated at met site </summary>
        public int ExposureCount
        {
            get { if (expo == null)
                    return 0;
                else
                    return expo.Length; }
        }

        /// <summary> Returns number of wind speed/wind direction distributions calculated at met site </summary>
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

        /// <summary> Adds an exposure with specified radius of investigation and inverse distance weight exponent to list of exposures </summary>
        public void AddExposure(int radius, double exponent, int numSectors)
        {            
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

        /// <summary> Adds wind speed / wind direction distribution to Met's list of WSWD_Dists from TAB file input </summary>
        public void AddWSWD_DistFromTAB(TOD thisTOD, Season thisSeason, double height, double[,] sectorWS_Dist, double[] windRose)
        {            
            
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

        /// <summary> Adds wind speed / wind direction distribution to Met's list of WSWD_Dists from met time series input </summary>
        public void AddWSWD_Dist(WSWD_Dist thisDist)
        {            
            if (AlreadyHaveWSWD_Dist(thisDist.timeOfDay, thisDist.season, thisDist.height) == false)
            {
                Array.Resize(ref WSWD_Dists, WSWD_DistCount + 1);
                WSWD_Dists[WSWD_DistCount - 1] = thisDist;                
            }
        }

        /// <summary> Returns true if WSWD distribution with specified height, time of day, and season has been created already. </summary>
        public bool AlreadyHaveWSWD_Dist(TOD thisTOD, Season thisSeason, double height)
        {
            bool alreadyGotIt = false;

            for (int i = 0; i < WSWD_DistCount; i++)
                if (WSWD_Dists[i].height == height && WSWD_Dists[i].season == thisSeason && WSWD_Dists[i].timeOfDay == thisTOD)
                    alreadyGotIt = true;
            
            return alreadyGotIt;
        }

        /// <summary> Clears all calculated exposures, surface roughness, and displacement height. </summary>
        public void ClearExposures()
        {
            // Clear all calculated exposure and SRDH
            expo = null;
        }

        /// <summary> Returns true if the exposure with specified radius and exponent has not been calculated </summary>
        public bool IsNewExposure(int radius, double exponent, int numSectors)
        {
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

        /// <summary> Returns true if the SRDH with specified radius and exponent has not been calculated </summary>
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

        /// <summary> Calculates overall average wind speed and wind speed distribution using sectorwise wind speed distribution and wind rose (i.e. TAB file). 
        /// ONLY USED FOR TAB FILE CALCS (i.e. all time of day and all seasons) </summary>
        public void CalcAvgWS(Continuum thisInst)
        {                         
            int WSWDind = GetWS_WD_DistInd(thisInst.modeledHeight, TOD.All, Season.All);                      
            WSWD_Dists[WSWDind].WS_Dist = new double[thisInst.metList.numWS];
                       
            // Calculate overall wind speed distribution
            for (int i = 0; i < thisInst.metList.numWS; i++)
            {
                double sumRose = 0;
                for (int j = 0; j < thisInst.metList.numWD; j++)
                {
                    WSWD_Dists[WSWDind].WS_Dist[i] = WSWD_Dists[WSWDind].WS_Dist[i] + WSWD_Dists[WSWDind].sectorWS_Dist[j, i] * WSWD_Dists[WSWDind].windRose[j];
                    sumRose = sumRose + WSWD_Dists[WSWDind].windRose[j];
                }
                WSWD_Dists[WSWDind].WS_Dist[i] = WSWD_Dists[WSWDind].WS_Dist[i] / sumRose;
            }

            // Calculate overall wind speed
            WSWD_Dists[WSWDind].WS = 0;
            double sumDist = 0;

            for (int i = 0; i < thisInst.metList.numWS; i++)
            {
                WSWD_Dists[WSWDind].WS = WSWD_Dists[WSWDind].WS + WSWD_Dists[WSWDind].WS_Dist[i] * (thisInst.metList.WS_FirstInt + i * thisInst.metList.WS_IntSize - thisInst.metList.WS_IntSize / 2);
                sumDist = sumDist + WSWD_Dists[WSWDind].WS_Dist[i];
            }

            WSWD_Dists[WSWDind].WS = WSWD_Dists[WSWDind].WS / sumDist;

        }

        /// <summary> Calculate directional wind speed ratios from TAB file. ONLY USED FOR TAB FILE CALCS (i.e. all time of day and all seasons) </summary>
        public void CalcSectorWS_Ratios(Continuum thisInst)
        {            
            int WSWD_ind = GetWS_WD_DistInd(thisInst.modeledHeight, TOD.All, Season.All);
            if (WSWD_Dists[WSWD_ind].windRose == null || WSWD_Dists[WSWD_ind].WS == 0) return;

            int numWS = thisInst.metList.numWS;
            int numWD = thisInst.metList.numWD;

            double[] WS_by_dir = new double[numWD];
            WSWD_Dists[WSWD_ind].sectorWS_Ratio = new double[numWD];                       
                    
            for (int i = 0; i < numWD; i++)
            {
                double sumWS = 0;
                for (int j = 0; j < numWS; j++)
                {
                    WS_by_dir[i] = WS_by_dir[i] + WSWD_Dists[WSWD_ind].sectorWS_Dist[i, j] * (thisInst.metList.WS_FirstInt + j * thisInst.metList.WS_IntSize - thisInst.metList.WS_IntSize / 2);
                    sumWS = sumWS + WSWD_Dists[WSWD_ind].sectorWS_Dist[i, j];
                }
                WS_by_dir[i] = WS_by_dir[i] / sumWS;
            }

            for (int i = 0; i < numWD; i++)
                WSWD_Dists[WSWD_ind].sectorWS_Ratio[i] = WS_by_dir[i] / WSWD_Dists[WSWD_ind].WS;
        }

        /// <summary> If flow separation model is enabled, this function searches for points surrounding met where flow separation is estimated to occur  </summary>
        public void GetFlowSepNodes(NodeCollection nodeList, Continuum thisInst)
        {                    
            int numWD = thisInst.GetNumWD();

            Nodes thisNode = nodeList.GetMetNode(this);           
            flowSepNodes = nodeList.FindAllFlowSeps(thisNode, thisInst, numWD);
            
        }

        /// <summary> Gets WS/WD distribution for specified height, time of day, and season  </summary>
        public WSWD_Dist GetWS_WD_Dist(double thisHeight, TOD thisTOD, Season thisSeason)
        {
            WSWD_Dist thisDist = new WSWD_Dist();

            for (int i = 0; i < WSWD_DistCount; i++)
                if (WSWD_Dists[i].height == thisHeight && WSWD_Dists[i].timeOfDay == thisTOD && WSWD_Dists[i].season == thisSeason)
                    thisDist = WSWD_Dists[i];
            
            return thisDist;
        }        

        /// <summary> Gets index of WS/WD distribution for specified height, time of day, and season  </summary>
        public int GetWS_WD_DistInd(double thisHeight, TOD thisTOD, Season thisSeason)
        {
            int thisInd = 0;

            for (int i = 0; i < WSWD_DistCount; i++)
                if (WSWD_Dists[i].height == thisHeight && WSWD_Dists[i].timeOfDay == thisTOD && WSWD_Dists[i].season == thisSeason)
                    thisInd = i;

            return thisInd;
        }

        /// <summary> Calculates and returns the wind speed and wind direction distribution based on a long-term time series estimate  </summary>        
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

            int allCount = 0; // Count of all WSWD used for overall WS, wind speed distribution, and wind rose
            int[] secCount = new int[thisInst.metList.numWD]; // Sectorwise count used for sectorwise wind speed ratios and sectorwise wind speed distributions             
            
            for (int i = 0; i < LT_WS_Ests.Length; i++)            
            {
                TOD siteDataTOD = thisInst.metList.GetTOD(LT_WS_Ests[i].thisDate); 
                Season siteDataSeason = thisInst.metList.GetSeason(LT_WS_Ests[i].thisDate);
                
                if ((thisTOD == TOD.All || thisTOD == siteDataTOD) && (thisSeason == Season.All || thisSeason == siteDataSeason))
                {
                    int WS_ind = mcp.Get_WS_ind(LT_WS_Ests[i].thisWS, 1);
                    int WD_ind = mcp.Get_WD_ind(LT_WS_Ests[i].thisWD);

                    if (WS_ind >= thisInst.metList.numWS) WS_ind = thisInst.metList.numWS - 1;

                    thisDist.windRose[WD_ind]++; // Wind direction count
                    thisDist.WS_Dist[WS_ind]++; // Overall wind speed distribution
                    thisDist.sectorWS_Dist[WD_ind, WS_ind]++; // Sectorwise wind speed distribution                   
                    
                    allCount++;
                    secCount[WD_ind]++;

                    thisDist.WS = thisDist.WS + LT_WS_Ests[i].thisWS; // Overall wind speed
                    thisDist.sectorWS_Ratio[WD_ind] = thisDist.sectorWS_Ratio[WD_ind] + LT_WS_Ests[i].thisWS; // Sectorwise wind speed 
                }                
            }

            // Calculate mean wind speed, overall wind speed distribution and wind rose
            if (allCount > 0)
            {
                thisDist.WS = thisDist.WS / allCount;

                for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                    thisDist.WS_Dist[WS_ind] = thisDist.WS_Dist[WS_ind] / allCount;
                
                for (int i = 0; i < thisInst.metList.numWD; i++)
                    thisDist.windRose[i] = thisDist.windRose[i] / allCount;
            }
            
            // Calculate sectorwise wind speed ratios and wind speed distribution
            for (int WD_ind = 0; WD_ind < thisInst.metList.numWD; WD_ind++)
            {
                if (secCount[WD_ind] > 0)
                {
                    for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                        thisDist.sectorWS_Dist[WD_ind, WS_ind] = thisDist.sectorWS_Dist[WD_ind, WS_ind] / secCount[WD_ind];

                    if (thisDist.WS > 0)
                        thisDist.sectorWS_Ratio[WD_ind] = thisDist.sectorWS_Ratio[WD_ind] / secCount[WD_ind] / thisDist.WS;
                }
            }               

            return thisDist;
        }

        /// <summary> Calculates and returns the wind speed and wind direction distribution based on simulated (extrapolated) time series estimate  </summary>        
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

            MCP thisMCP = new MCP(); // Created to use Get_WS_ind and Get_WD_ind 
            thisMCP.numWD = thisInst.metList.numWD;
            thisMCP.numSeasons = thisInst.metList.numSeason;
            thisMCP.numTODs = thisInst.metList.numTOD;

            int allCount = 0; // Count of all WSWD used for overall WS, wind speed distribution, and wind rose
            int[] secCount = new int[thisInst.metList.numWD]; // Sectorwise count used for sectorwise wind speed ratios and sectorwise wind speed distributions                

            for (int i = 0; i < extrapData.WS_WD_data.Length; i++)
            {
                TOD siteDataTOD = thisInst.metList.GetTOD(extrapData.WS_WD_data[i].timeStamp);
                Season siteDataSeason = thisInst.metList.GetSeason(extrapData.WS_WD_data[i].timeStamp);

                if (extrapData.WS_WD_data[i].WS != -999 && extrapData.WS_WD_data[i].WD != -999 && 
                    (thisTOD == TOD.All || thisTOD == siteDataTOD) && (thisSeason == Season.All || thisSeason == siteDataSeason))
                {
                    int WS_ind = thisMCP.Get_WS_ind(extrapData.WS_WD_data[i].WS, 1);
                    int WD_ind = thisMCP.Get_WD_ind(extrapData.WS_WD_data[i].WD);
                                        
                    if (WS_ind >= thisInst.metList.numWS) WS_ind = thisInst.metList.numWS - 1;

                    thisDist.windRose[WD_ind]++; // Wind direction count
                    thisDist.WS_Dist[WS_ind]++; // Overall wind speed distribution
                    thisDist.sectorWS_Dist[WD_ind, WS_ind]++; // Sectorwise wind speed distribution                   

                    allCount++;
                    secCount[WD_ind]++;

                    thisDist.WS = thisDist.WS + extrapData.WS_WD_data[i].WS; // Overall wind speed
                    thisDist.sectorWS_Ratio[WD_ind] = thisDist.sectorWS_Ratio[WD_ind] + extrapData.WS_WD_data[i].WS; // Sectorwise wind speed 
                }
            }

            // Calculate wind speed overall distribution and wind rose
            if (allCount > 0)
            {
                thisDist.WS = thisDist.WS / allCount;

                for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                    thisDist.WS_Dist[WS_ind] = thisDist.WS_Dist[WS_ind] / allCount;

                for (int i = 0; i < thisInst.metList.numWD; i++)
                    thisDist.windRose[i] = thisDist.windRose[i] / allCount;
            }

            // Calculate sectorwise wind speed ratios and wind speed distribution
            for (int WD_ind = 0; WD_ind < thisInst.metList.numWD; WD_ind++)
            {
                if (secCount[WD_ind] > 0)
                {                 
                    for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                        thisDist.sectorWS_Dist[WD_ind, WS_ind] = thisDist.sectorWS_Dist[WD_ind, WS_ind] / secCount[WD_ind];

                    if (thisDist.WS > 0)
                        thisDist.sectorWS_Ratio[WD_ind] = thisDist.sectorWS_Ratio[WD_ind] / secCount[WD_ind] / thisDist.WS;
                }
            }           

            return thisDist;
        }

        /// <summary> Calculates all long-term (i.e. based on MCP'd data) wind speed / wind direction distributions and saves to list  </summary>   
        public void CalcAllLT_WSWD_Dists(Continuum thisInst, MCP.Site_data[] LT_WS_Ests)
        {            
            if (mcp.HaveMCP_Estimate("Any") == false)
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

        /// <summary> Calculates all measured (i.e. based on extrapolated data not adjusted for LT) wind speed / wind direction distributions and saves to list  </summary>
        public void CalcAllMeas_WSWD_Dists(Continuum thisInst, Met_Data_Filter.Sim_TS extrapData)
        {            
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

        /// <summary> Gets time of day index. Day = 0; Night = 1  </summary>
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

        /// <summary> Gets seasonal index (Winter: 0, Spring: 1, Summer: 2, Fall: 3)  </summary>
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

        /// <summary> Calculates average and representative turbulence intensity. Bins the data by WS and WD and calculates the average standard deviation, average WS, avg + 1.28 SD, and P90 SD
        /// Uses extrapolated wind speed and standard deviation at height closest to extrapolated height.  </summary>
        public void CalcTurbulenceIntensity(DateTime startTime, DateTime endTime, double height, Continuum thisInst)
        {            
            turbulence.startTime = startTime;
            turbulence.endTime = endTime;                       

            if (metData == null)
                return;

            Met_Data_Filter.Sim_TS extrapData = metData.GetSimulatedTimeSeries(height);    
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
                if (extrapData.WS_WD_data[timeInd].WD != -999 && extrapData.WS_WD_data[timeInd].WS != -999
                    && extrapData.WS_WD_data[timeInd].SD != -999 && extrapData.WS_WD_data[timeInd].SD < extrapData.WS_WD_data[timeInd].WS / 3)
                {
                    int WD_ind = metData.GetWD_Ind(extrapData.WS_WD_data[timeInd].WD, numWD);                    
                    int WS_ind = metData.GetWS_ind(extrapData.WS_WD_data[timeInd].WS, thisInst.metList.WS_IntSize, thisInst.metList.numWS);                                      

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
                        turbulence.avgPlus1_28SD[i, j] =   1.28 * turbulence.avgSD[i, j];

                        Array.Sort(arrayOfSDs[i, j].SDs);
                        int P90 = (int)Math.Round(arrayOfSDs[i, j].SDs.Length * 0.9) - 1;
                        double thisP90 = arrayOfSDs[i, j].SDs[P90];
                        turbulence.p90SD[i, j] = thisP90;
                    }                    
                }
        }

        /// <summary> Calculates and returns overall (i.e. wind direction sectors) turbulence intensity in each wind speed bin. turbType: "Average" or "Representative"  </summary>
        public TIandCount[] CalcOverallTurbulenceIntensity(string turbType, Continuum thisInst)
        {            
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

        /// <summary> Calculates and returns histogram of power law exponent (alpha) calculated between start/end dates and for specified WS range ("5 - 10 m/s", "10 - 15 m/s", "15+ m/s", "All > Cut-In"). 
        /// Uses an alpha range of -0.5 to 1.5 with an interval size of 0.02.</summary>
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

        /// <summary> Finds all alpha exponents for wind speeds within the specified minimum/maximum range and within specified start/end times and returns the alpha at the specified P-value  </summary>
        public double GetAlphaPValue(double minWS, double maxWS, int pLevel, Continuum thisInst, DateTime startTime, DateTime endTime)
        {            
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

        /// <summary> Finds and returns maximum 10-minute or gust for each full year of data.  </summary>
        public MaxYearlyWind[] GetMaxYearlyWinds(string tenMinOrGust, Continuum thisInst)
        {
            MaxYearlyWind[] maxYearlyWinds = new MaxYearlyWind[0];
            int dataInd = 0;

            // Get anems closest to hub height
            int[] anemInds = metData.GetAnemsClosestToHH(thisInst.modeledHeight);
            Met_Data_Filter.Anem_Data anem1 = metData.anems[anemInds[0]];
            Met_Data_Filter.Anem_Data anem2 = new Met_Data_Filter.Anem_Data();
            bool gotAnem2 = false;
            if (anemInds.Length > 1)
            {
                anem2 = metData.anems[anemInds[1]];
                gotAnem2 = true;
            }                

            // Go to first Jan 1            
            while (dataInd < anem1.windData.Length && (anem1.windData[dataInd].timeStamp.Month != 1 || anem1.windData[dataInd].timeStamp.Day != 1))
                dataInd++;

            if (dataInd >= anem1.windData.Length) // no Jan. 1 in dataset
                return maxYearlyWinds;

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
                        if (gotAnem2) 
                            if (anem2.windData[i].avg > maxWS)
                                maxWS = anem1.windData[i].avg;
                    }
                    else
                    {
                        if (anem1.windData[i].max > maxWS)
                            maxWS = anem1.windData[i].max;
                        if (gotAnem2)
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
                        if (gotAnem2)
                            if (anem2.windData[i].avg > maxWS)
                                maxWS = anem1.windData[i].avg;
                    }
                    else
                    {
                        maxWS = anem1.windData[i].max;
                        if (gotAnem2)
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

        /// <summary> Calculates and returns extreme wind speeds estimates (1 yr and 50 yr, 10-min and Gust).  </summary>
        public Extreme_WindSpeed CalcExtremeWindSpeeds(Continuum thisInst)
        {            
            Extreme_WindSpeed extremeWinds = new Extreme_WindSpeed();                                 

            // Get MERRA2 data used for this met
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(UTMX, UTMY);
       //     int UTC_offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);

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
