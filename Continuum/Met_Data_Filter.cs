using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ContinuumNS
{    
    /// <summary> Met_Data_Filter class contains functions that read in raw MET data (as .csv), applies filters,
    /// calculates power law shear, and extrapolates to hub height. Filters include tower shadow, icing, min/max wind speed standard deviation. </summary>
    
    [Serializable()]
    public partial class Met_Data_Filter
    {
        /// <summary>   One Energy WRA filtering Methodology version used in this tool. </summary>
        public string WRA_Methodology = "2019.0";
        /// <summary>   Filename of the raw input datafile. </summary>
        public string rawFilename = "";
        /// <summary>   List of anemometer datasets. </summary>
        public Anem_Data[] anems = new Anem_Data[0];
        /// <summary>   List of wind vane datasets. </summary>
        public Vane_Data[] vanes = new Vane_Data[0];
        /// <summary>   List of temperature sensor datasets. </summary>
        public Temp_Data[] temps = new Temp_Data[0];
        /// <summary>   List of temperature sensor datasets. </summary>
        public Press_Data[] baros = new Press_Data[0];
        /// <summary>   If filtering has been conducted, this is true. </summary>
        public bool filteringDone = false;
        /// <summary>   Wind speed units, mph (miles/hour) or mps (meters/sec). </summary>
        public string WS_units;
        /// <summary>   All calculated shear alpha exponents and wind direction data from vane closest to hub height. Used to generate alpha table and plot, not used for extrapolating </summary>
        public Est_Alpha[] alpha = new Est_Alpha[0];                
        /// <summary> Shear Power law alpha datasets with WS and WD. One is created for each anem height. Used to extrapolate data. </summary>
        public Shear_Data[] alphaByAnem = new Shear_Data[0];
        /// <summary>   Simulated (estimated) wind speed, wind direction, and WS SD data. </summary>
        public Sim_TS[] simData = new Sim_TS[0];
        /// <summary>   First date of dataset. </summary>
        public DateTime allStartDate;
        /// <summary>   Last date of dataset. </summary>
        public DateTime allEndDate;
        /// <summary>   Start date defining beginning of period to use in analysis. </summary>
        public DateTime startDate;
        /// <summary>   End date defining end of period to use in analysis. </summary>
        public DateTime endDate;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // Met data filtering settings. 
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Minimum wind speed for standard deviation filters. Default = 1 m/s. </summary>
        public double minWS_ForSD_Filts = 1;
        /// <summary>   Minimum standard deviation filter slope. Default = 0.02. </summary>
        public double minSD_FiltSlope = 0.02;
        /// <summary>   Maximum standard deviation filter slope. Default = 0.22. </summary>
        public double maxSD_FiltSlope = 0.22;
        /// <summary>   Maximum SD filter intercept. Default = 1.1. </summary>
        public double maxSD_FiltInt = 1.1;
        /// <summary>   Maximum range between min and max wind speed. Default = 20. </summary>
        public double maxRange = 20;
        /// <summary>   Maximum temperature (deg F) for Icing flag. Default = 34 F. </summary>
        public double maxIcingTemp = 34;
        /// <summary>   Anemometer minimum standard deviation used in Icing filter. Default = 0.01. </summary>
        public double anemIcingMinSD = 0.01;
        /// <summary>   Vane minimum standard deviation used in Icing filter. Default = 1. </summary>
        public double vaneIcingMinSD = 1;
        /// <summary>   Maximum absolute wind speed difference used to define tower shadow. Default = 0.2. </summary>
        public double towerShadowThresh = 0.2;
        /// <summary>   Width of the tower shadow when have single anem at a height. Default = 36. </summary>
        public int towerShadowWidth = 36;
        /// <summary>   Minimum wind speed filter. Default = 0.4. </summary>
        public double minWS_Filt = 0.4;
        /// <summary>   Minimum WS of closest anemometer to flag min WS. Default = 1. </summary>
        public double minClosestWS = 1;                                  
              
        /// <summary> Flag showing whether file is being opened (so certain messages don't appear) </summary>
        public bool Opening_a_file = false;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary> Contains timestamp, 10-min average, 10-min wind speed standard deviation, 10-min minimum, 10-min maximum, and filter flag </summary>        
        [Serializable()]
        public struct data
        {             
            /// <summary>   Time stamp Date/Time. </summary>
            public DateTime timeStamp;
            /// <summary>   10-min minimum. </summary>
            public double min;
            /// <summary>   10-min maximum. </summary>
            public double max;
            /// <summary>   10-min average. </summary>
            public double avg;
            /// <summary>   10-min wind speed standard deviation. </summary>
            public double SD;
            /// <summary>   QC filter flag. </summary>
            public Filter_Flags filterFlag;
        }


        /// <summary> Contains timstamp, wind speed, wind direction, wind speed standard deviation, and estimated shear alpha exponent. </summary>
        [Serializable()]
        public struct Est_Data
        {
            /// <summary>   Time stamp Date/Time. </summary>
            public DateTime timeStamp;
            /// <summary>   Wind speed. </summary>
            public double WS;
            /// <summary>   Wind direction. </summary>
            public double WD;            
            /// <summary> WS standard deviation from anem at height closest to (but not higher than) the extrapolated height. </summary>            
            public double SD;
            /// <summary> Estimated shear alpha exponent </summary>
            public double alpha;
        }               

        
        /// <summary>  Contains timestamp, wind speed, wind direction, and estimated shear alpha exponent. </summary>
        [Serializable()]
        public struct Est_Alpha
        {
            /// <summary>   Time stamp Date/Time. </summary>
            public DateTime timeStamp;
            /// <summary>   Average power law shear exponent alpha. </summary>
            public double alpha;
            /// <summary>   Wind direction of vane closest to modeled height. </summary>
            public double WD;
            /// <summary>   Wind speed at modeled height. </summary>
            public double WS;
        }


        /// <summary> Holds all anemometer information and data including the sensor height, sensor ID, tower shadow sectors, and wind time series. </summary>        
        [Serializable()]
        public struct Anem_Data
        {
            /// <summary>   The height of sensor. </summary>
            public double height;
            /// <summary>   The identifier (A or B) if there is another sensor at same height. </summary>
            public char ID;
            /// <summary> orientation of the boom (degs) </summary>
            public int orientation;
            /// <summary> Tower shadow sector </summary>
            public Tower_Shadow shadow;
            /// <summary>   false if there is another sensor at same height. </summary>
            public bool isOnlyMet;
            /// <summary>   Wind speed time series data. </summary>
            public data[] windData;

            /// <summary> Returns index with specified timestamp </summary>            
            public int GetTS_Index(DateTime thisDate)
            {
                int tsInd = 0;
                int indMid = windData.Length / 2;
                int dataIntMins = Convert.ToInt32(Math.Round(windData[indMid].timeStamp.Subtract(windData[indMid - 1].timeStamp).TotalMinutes, 0));

                tsInd = Convert.ToInt32(Math.Round(thisDate.Subtract(windData[0].timeStamp).TotalMinutes / dataIntMins, 0));

                while (windData[tsInd].timeStamp < thisDate && tsInd < windData.Length - 1)
                    tsInd++;

                while (windData[tsInd].timeStamp > thisDate && tsInd > 0)
                    tsInd--;

                if (windData[tsInd].timeStamp != thisDate)
                    tsInd = -999;

                return tsInd;
            }
        }
       
        /// <summary>   Contains defined tower shadow sector (min/max/center WD). </summary>        
        [Serializable()]
        public struct Tower_Shadow
        {
            /// <summary>   Start of tower shadow. </summary>
            public int minWD;
            /// <summary>   End of tower shadow. </summary>
            public int maxWD;
            /// <summary>   Center of tower shadow. -999 if there is no tower shadow. </summary>
            public int center;            
        }
        
        /// <summary> Holds wind direction time series data and height of vane. </summary>
        [Serializable()]
        public struct Vane_Data
        {
            /// <summary>  Vane height. </summary>
            public double height;
            /// <summary> Vane boom orientation </summary>
            public double orientation;
            /// <summary>  Wind direction time series data. </summary>
            public data[] dirData;
        }
                
        /// <summary> Holds temperature sensor time series data plus the height of the sensor and temperature units. </summary>
        [Serializable()]
        public struct Temp_Data
        {
            /// <summary>   Sensor height. </summary>
            public double height;
            /// <summary>   Celsius 'C' or Fahrenheit 'F'. </summary>
            public char C_or_F;
            /// <summary>   Array of type data holding time series of temperature sensor data. </summary>
            public data[] temp;
        }


        /// <summary> Holds pressure sensor time series data plus the height of the sensor and pressure units. </summary>
        [Serializable()]
        public struct Press_Data
        {
            /// <summary>   Sensor height. </summary>
            public double height;
            /// <summary>   Pressure units. </summary>
            public string units;
            /// <summary>   Array of type data holding time series of pressure sensor data. </summary>
            public data[] pressure;
        }

        /// <summary> Holds anemometer height and time series of shear alpha, wind speed and wind direction data </summary>        
        [Serializable()]
        public struct Shear_Data
        {            
            /// <summary>   Anemometer height. </summary>
            public double anemHeight;
            /// <summary>   Wind speed and wind direction (average of filtered data, if redundant) and estimated alpha data time series data. </summary>
            public Est_Data[] WS_WD_Alpha;
        }
                
        /// <summary> Holds estimated time series data and the height of the estimated data. </summary>
        [Serializable()]
        public struct Sim_TS
        {             
            /// <summary>  Height of estimated data. </summary>
            public double height;
            /// <summary>   Estimated wind speed and wind direction time series data. </summary>
            public Est_Data[] WS_WD_data;
        }              
                
        /// <summary> Holds the concurrent wind speed data collected by two anemomters. </summary>        
        public struct Conc_WS_Data
        {
            /// <summary>   Wind speed at Anem A. </summary>
            public double[] anemA_WS;
            /// <summary>   Wind speed at Anem B. </summary>
            public double[] anemB_WS;
        }
        
        /// <summary> Holds wind speed ratios and wind direction data for scatterplot. </summary>
        public struct WS_Ratio_WD_Data
        {
            /// <summary>   Wind speed ratios. </summary>
            public double[] WS_Ratio;
            /// <summary>   Wind direction data. </summary>
            public double[] WD;
        }

        
        /// <summary>  Holds wind speed ratio and wind speed data for scatterplot. </summary>
        public struct WS_Ratio_WS_Data
        {
            /// <summary>   Wind speed ratios. </summary>
            public double[] WS_Ratio;
            /// <summary>   Wind speed data. </summary>
            public double[] WS;
        }

        
        /// <summary> Holds the maximum ten-minute and maximum gust and their corresponding time stamps for a given year. </summary>        
        public struct Yearly_Maxes
        {
            /// <summary>   Timestamp when max WS was measured. </summary>
            public DateTime timeStampMaxWS;
            /// <summary>   Timestamp when Maximum gust was measured. </summary>
            public DateTime timeStampMaxGust;
            /// <summary>   Max 10-minute wind speed. </summary>
            public double maxWS;
            /// <summary>   Max 3-sec gust. </summary>
            public double maxGust;
        }
                
        /// <summary> Filter_Flags Used to define the different QC filtering flags. </summary>
        
        public enum Filter_Flags
        {
            /// <summary>   Data outside range flag. </summary>
            outsideRange = -2,
            /// <summary>   Data missing flag. </summary>
            missing = -1,
            /// <summary>   Valid data flag. </summary>
            Valid = 0,
            /// <summary>   Minimum anemometer standard deviation flag. </summary>
            minAnemSD = 1,
            /// <summary>   Maximum anemometer standard deviation flag. </summary>
            maxAnemSD = 2,
            /// <summary>   Max anemometer min/max wind speed range flag. </summary>
            maxAnemRange = 3,
            /// <summary>   Icing flag. </summary>
            Icing = 4,
            /// <summary>   Tower shadow flag. </summary>
            towerEffect = 5,
            /// <summary>   Maximum wind speed difference (at same height) flag (not currently implemented). </summary>
            maxDeltaWS = 6,
            /// <summary>   Minimum wind speed flag (not currently implemented). </summary>
            minWS = 7
            
        }   

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>  Gets the number of anemometers loaded. </summary>
        public int GetNumAnems()
        {
            return anems.Length;
        }

        
        /// <summary>   Gets the number of vanes loaded. </summary>
        public int GetNumVanes()
        {
            return vanes.Length;
        }
                
        /// <summary>   Gets the number of temperature sensors. </summary>
        public int GetNumTemps()
        {
            return temps.Length;
        }

        /// <summary>   Gets the number of pressure sensors. </summary>
        public int GetNumBaros()
        {
            return baros.Length;
        }


        /// <summary> Gets the number of simulated (estimated) datasets. </summary>
        public int GetNumSimData()
        {
            return simData.Length;
        }
                
        /// <summary> Gets length of met dataset. </summary>        
        public int GetDataLength()
        {
            int dataCount = 0;

            if (GetNumAnems() > 0) dataCount = anems[0].windData.Length;

            return dataCount;
        }
                
        /// <summary> Gets heights of anemometers. </summary>        
        public double[] GetHeightsOfAnems()
        {
            double[] These_Heights = new double[0];
            int numHeights = 0;
            double Last_Height = 1000;

            foreach(Anem_Data thisAnem in anems)
            {
                if (Math.Abs(thisAnem.height - Last_Height) > 2)
                {
                    numHeights++;
                    Array.Resize(ref These_Heights, numHeights);
                    These_Heights[numHeights-1] = thisAnem.height;
                    Last_Height = thisAnem.height;
                    
                }
            }

            return These_Heights;
        }               

        
        /// <summary> Converts all wind speed data from miles per hour to meters per second. </summary>  
        public void ConvertToMPS()
        {           
            for (int i = 0; i < GetNumAnems(); i++)
            {
                for (int j = 0; j < anems[i].windData.Length; j++)
                {
                    if (anems[i].windData[j].filterFlag != Filter_Flags.missing)
                    {
                        anems[i].windData[j].avg = anems[i].windData[j].avg * 0.44704;
                        anems[i].windData[j].SD = anems[i].windData[j].SD * 0.44704;
                        anems[i].windData[j].max = anems[i].windData[j].max * 0.44704;
                        anems[i].windData[j].min = anems[i].windData[j].min * 0.44704;
                    }                    
                }
            }
        }               
        
        /// <summary>   Calculates and returns average alpha in each WD sector for specified hourly interval. </summary>
        public double[] GetAvgAlpha(int minHour, int maxHour, int numWD)
        {            
            double[] avgAlpha = new double[numWD];
            int[] Count_Alpha = new int[numWD];

            if (alpha.Length == 0)
                EstimateAlpha();

            if (alpha.Length == 0)
                return avgAlpha;

            int thisInd = 0;

            while (alpha[thisInd].timeStamp < startDate)
                thisInd++;
                          
            while (thisInd < alpha.Length)
            {
                if (alpha[thisInd].timeStamp > endDate)
                    break;

                int WD_Ind = GetWD_Ind(alpha[thisInd].WD, numWD);                
                int thisHour = alpha[thisInd].timeStamp.Hour;
                               
                
                if (((alpha[thisInd].alpha != -999) && (WD_Ind != -999)) && (((maxHour >= minHour) && (thisHour >= minHour) && (thisHour <= maxHour)) 
                    || ((minHour > maxHour) && ((thisHour >= minHour) || (thisHour <= maxHour)))))
                {                    
                    avgAlpha[WD_Ind] = avgAlpha[WD_Ind] + alpha[thisInd].alpha;                    
                    Count_Alpha[WD_Ind]++;                    
                }

                thisInd++;
            }
                      
            for (int i = 0; i < numWD; i++)
                if (Count_Alpha[i] > 0) avgAlpha[i] = avgAlpha[i] / Count_Alpha[i];                      

            return avgAlpha;
        }
                
        /// <summary> Calculates and returns the wind direction index of specified wind direction. </summary>        
        public int GetWD_Ind(double thisWD, int numWD)
        {
            int WD_Ind = 0;

            if (thisWD == -999)
                WD_Ind = -999;
            else
            {                
                WD_Ind = (int)Math.Round(thisWD / (360 / (double)numWD), 0, MidpointRounding.AwayFromZero);
                if (WD_Ind == numWD) WD_Ind = 0;
            }            

            return WD_Ind;
        }

        /// <summary> Calculates and returns the wind speed index of specified wind speed. </summary>   
        public int GetWS_ind(double thisWS, double binWidth, int numWS)
        {
            int WS_Ind = 0;
            if (binWidth != 0)
                WS_Ind = (int)Math.Round(thisWS / binWidth, 0, MidpointRounding.AwayFromZero);

            if (WS_Ind >= numWS)
                WS_Ind = numWS - 1;

            return WS_Ind;
        }

        /// <summary> Calculates the average wind speed of specified anem in each wind direction bin (as determined by specified vane) for specified start/end time either filtered or unfiltered data (hard-coded 5 deg WD bin width) used for plotting average WS by WD on Met Data QC tab. </summary>
        public double[] Calc_Avg_WS_by_WD(DateTime startTime, DateTime endTime, Anem_Data anem, Vane_Data vane, string filtOrUnfilt)
        {
            double[] theseWS_byWD = new double[72];
            int[] WD_count = new int[72];

      //      int vaneInd = GetVaneClosestToHH(anems[anemInd].height);
            
            int timeInd = 0;
            while (anem.windData[timeInd].timeStamp < startTime)
                timeInd++;

            while (anem.windData[timeInd].timeStamp < endTime)
            {
                if ((filtOrUnfilt == "Unfiltered" && (anem.windData[timeInd].filterFlag != Filter_Flags.missing ||
                    anem.windData[timeInd].filterFlag != Filter_Flags.outsideRange)) || 
                    (anem.windData[timeInd].filterFlag == Filter_Flags.Valid && vane.dirData[timeInd].filterFlag == Filter_Flags.Valid))
                {
                    int WD_Ind = GetWD_Ind(vane.dirData[timeInd].avg, 72);
                    theseWS_byWD[WD_Ind] = theseWS_byWD[WD_Ind] + anem.windData[timeInd].avg;
                    WD_count[WD_Ind]++;
                    
                }

                timeInd++;
            }

            for (int i = 0; i < 72; i++)
            {
                if (WD_count[i] > 0)
                    theseWS_byWD[i] = theseWS_byWD[i] / WD_count[i];
            }

            return theseWS_byWD;
        }
                
        /// <summary>   Calculates and returns the wind rose between start/end time using either filtered or unfiltered data. </summary>        
        public double[] Calc_Wind_Rose(DateTime startTime, DateTime endTime, Vane_Data vane, string filtOrUnfilt)
        {
            double[] thisWR = new double[16];
            int totalCount = 0;
            
            int timeInd = 0;
            while (vane.dirData[timeInd].timeStamp < startTime)
                timeInd++;

            while (vane.dirData[timeInd].timeStamp < endTime && timeInd < vane.dirData.Length - 1)
            {
                if ((vane.dirData[timeInd].filterFlag != Filter_Flags.missing && vane.dirData[timeInd].filterFlag != Filter_Flags.outsideRange) &&
                        (filtOrUnfilt == "Unfiltered" || vane.dirData[timeInd].filterFlag == Filter_Flags.Valid))
                {
                    int WD_Ind = GetWD_Ind(vane.dirData[timeInd].avg, 16);
                    thisWR[WD_Ind]++;
                    totalCount++;
                }
                timeInd++;
            }

            if (totalCount > 0)
            {
                for (int i = 0; i < 16; i++)
                    thisWR[i] = thisWR[i] / totalCount;
            }
            
            return thisWR;
        }
                
        /// <summary> Calculates and returns WS ratio or WS difference and wind direction time series data. Used for plots on GUI </summary> 
        public WS_Ratio_WD_Data GetWS_RatioOrDiffAndWD(double anemHeight, string filtOrUnfilt, string ratioOrDiff, bool allOrNot)
        {            
            WS_Ratio_WD_Data Ratios_WD = new WS_Ratio_WD_Data();

            if (GetNumAnems() == 0 || GetNumVanes() == 0)
                return Ratios_WD;                      

            int[] indA_andB = GetAnemsClosestToHH(anemHeight);

            if (indA_andB.Length < 2)
                return Ratios_WD;

            if (indA_andB[0] == -999 || indA_andB[1] == -999)
                return Ratios_WD;

            Anem_Data thisAnemA = anems[indA_andB[0]];
            Anem_Data thisAnemB = anems[indA_andB[1]];

            int vaneInd = GetVaneClosestToHH(anemHeight);           
            Vane_Data thisVane = vanes[vaneInd];                       

            // count number of concurrent data intervals
            int concCount = 0;
            int thisInd = 0;
            
            DateTime thisStart;
            DateTime thisEnd;

            if (allOrNot == true)
            {
                thisStart = allStartDate; // Uses all met data that has been imported
                thisEnd = allEndDate;
            }
            else
            {
                thisStart = startDate; // Uses met data analysis interval (specified either by function FindStartEndDatesWithMaxRecovery or by user)
                thisEnd = endDate;
            }

            while (thisAnemA.windData[thisInd].timeStamp < thisStart) // Find index at specified start time
                thisInd++;

            int startInd = thisInd;
            
            while (thisInd < thisAnemA.windData.Length) // Increment until end time is reached and count number of concurrent valid measurements
            {
                if (thisAnemA.windData[thisInd].timeStamp > thisEnd)
                    break;                              

                if ((filtOrUnfilt == "Filtered") && (thisAnemA.windData[thisInd].filterFlag == 0) && (thisAnemB.windData[thisInd].filterFlag == 0) && (thisVane.dirData[thisInd].filterFlag == 0))
                    concCount++;
                else if ((filtOrUnfilt == "Unfiltered") && (thisAnemA.windData[thisInd].filterFlag >= 0) && (thisAnemB.windData[thisInd].filterFlag >= 0) && (thisVane.dirData[thisInd].filterFlag >= 0)) // unfiltered (not missing or out of range)
                    concCount++;

                thisInd++;
            }

            Array.Resize(ref Ratios_WD.WS_Ratio, concCount);
            Array.Resize(ref Ratios_WD.WD, concCount);

            concCount = 0;
            thisInd = startInd;

            while (thisInd < thisAnemA.windData.Length)
            {
                if (thisAnemA.windData[thisInd].timeStamp > thisEnd)
                    break;               

                if ((filtOrUnfilt == "Filtered") && (thisAnemA.windData[thisInd].filterFlag == 0) && (thisAnemB.windData[thisInd].filterFlag == 0) && (thisVane.dirData[thisInd].filterFlag == 0))
                {                 
                    if (ratioOrDiff == "Ratio") Ratios_WD.WS_Ratio[concCount] = thisAnemA.windData[thisInd].avg / thisAnemB.windData[thisInd].avg;
                    if (ratioOrDiff == "Diff") Ratios_WD.WS_Ratio[concCount] = thisAnemA.windData[thisInd].avg - thisAnemB.windData[thisInd].avg;
                    double This_Diff = thisAnemA.windData[thisInd].avg - thisAnemB.windData[thisInd].avg;
                    Ratios_WD.WD[concCount] = thisVane.dirData[thisInd].avg;
                    concCount++;                   
                    
                }
                else if ((filtOrUnfilt == "Unfiltered") && (thisAnemA.windData[thisInd].filterFlag >= 0) && (thisAnemB.windData[thisInd].filterFlag >= 0) && (thisVane.dirData[thisInd].filterFlag >= 0)) // unfiltered (not missing or out of range)
                {
                    if (ratioOrDiff == "Ratio") Ratios_WD.WS_Ratio[concCount] = thisAnemA.windData[thisInd].avg / thisAnemB.windData[thisInd].avg;
                    if (ratioOrDiff == "Diff") Ratios_WD.WS_Ratio[concCount] = thisAnemA.windData[thisInd].avg - thisAnemB.windData[thisInd].avg;
                    Ratios_WD.WD[concCount] = thisVane.dirData[thisInd].avg;
                    concCount++;
                }

                thisInd++;
            }

            return Ratios_WD;
        }

        /// <summary> Returns wind speed ratios calculated between two specified anemometers in WD bins as defined by specified vane </summary>        
        public WS_Ratio_WD_Data GetWS_DiffbyWD_TwoAnems(Anem_Data anemA, Anem_Data anemB, Vane_Data selVane, string filtOrUnfilt, bool allOrNot)
        {
            WS_Ratio_WD_Data Ratios_WD = new WS_Ratio_WD_Data();

            if (anemA.height == 0 || anemB.height == 0 || selVane.height == 0)
                return Ratios_WD;
            
            // count number of concurrent data intervals
            int concCount = 0;
            int thisInd = 0;

            DateTime thisStart;
            DateTime thisEnd;

            if (allOrNot == true)
            {
                thisStart = allStartDate; // Uses all met data that has been imported
                thisEnd = allEndDate;
            }
            else
            {
                thisStart = startDate; // Uses met data analysis interval (specified either by function FindStartEndDatesWithMaxRecovery or by user)
                thisEnd = endDate;
            }

            while (anemA.windData[thisInd].timeStamp < thisStart) // Find index at specified start time
                thisInd++;

            int startInd = thisInd;

            while (thisInd < anemA.windData.Length) // Increment until end time is reached and count number of concurrent valid measurements
            {
                if (anemA.windData[thisInd].timeStamp > thisEnd)
                    break;

                if ((filtOrUnfilt == "Filtered") && (anemA.windData[thisInd].filterFlag == 0) && (anemB.windData[thisInd].filterFlag == 0) && (selVane.dirData[thisInd].filterFlag == 0))
                    concCount++;
                else if ((filtOrUnfilt == "Unfiltered") && (anemA.windData[thisInd].filterFlag >= 0) && (anemB.windData[thisInd].filterFlag >= 0) && (selVane.dirData[thisInd].filterFlag >= 0)) // unfiltered (not missing or out of range)
                    concCount++;

                thisInd++;
            }

            Array.Resize(ref Ratios_WD.WS_Ratio, concCount);
            Array.Resize(ref Ratios_WD.WD, concCount);

            concCount = 0;
            thisInd = startInd;

            while (thisInd < anemA.windData.Length)
            {
                if (anemA.windData[thisInd].timeStamp > thisEnd)
                    break;

                if ((filtOrUnfilt == "Filtered") && (anemA.windData[thisInd].filterFlag == 0) && (anemB.windData[thisInd].filterFlag == 0) && (selVane.dirData[thisInd].filterFlag == 0))
                {                    
                    Ratios_WD.WS_Ratio[concCount] = anemA.windData[thisInd].avg - anemB.windData[thisInd].avg;                    
                    Ratios_WD.WD[concCount] = selVane.dirData[thisInd].avg;
                    concCount++;

                }
                else if ((filtOrUnfilt == "Unfiltered") && (anemA.windData[thisInd].filterFlag >= 0) && (anemB.windData[thisInd].filterFlag >= 0) && (selVane.dirData[thisInd].filterFlag >= 0)) // unfiltered (not missing or out of range)
                {                    
                    Ratios_WD.WS_Ratio[concCount] = anemA.windData[thisInd].avg - anemB.windData[thisInd].avg;
                    Ratios_WD.WD[concCount] = selVane.dirData[thisInd].avg;
                    concCount++;
                }

                thisInd++;
            }

            return Ratios_WD;
        }

        /// <summary> Calculates and returns wind speed ratio or difference and wind speed data between start and end dates. Used in scatterplots on GUI. </summary>
        public WS_Ratio_WS_Data GetWS_RatioOrDiffAndWS(int anemHeight, string filtOrUnfilt, string ratioOrDiff)
        {            
            WS_Ratio_WS_Data ratiosWS = new WS_Ratio_WS_Data();

            Anem_Data thisAnemA = new Anem_Data();
            Anem_Data thisAnemB = new Anem_Data();

            foreach (Anem_Data thisAnem in anems)
            {
                if ((Math.Abs(thisAnem.height - anemHeight) < 2) && (thisAnem.ID == 'A'))
                    thisAnemA = thisAnem;

                if ((Math.Abs(thisAnem.height - anemHeight) < 2) && (thisAnem.ID == 'B'))
                    thisAnemB = thisAnem;
            }

            // count number of concurrent data intervals
            int concCount = 0;
            if ((thisAnemA.height == 0) || (thisAnemB.height == 0))
                return ratiosWS;

            int thisInd = 0;
            
            while (thisAnemA.windData[thisInd].timeStamp < startDate)
                thisInd++;

            int startInd = thisInd;

            while (thisInd < thisAnemA.windData.Length)
            {
                if (thisAnemA.windData[thisInd].timeStamp > endDate)
                    break;

                if ((filtOrUnfilt == "Filtered") && (thisAnemA.windData[thisInd].filterFlag == 0) && (thisAnemB.windData[thisInd].filterFlag == 0))
                    concCount++;
                else if ((filtOrUnfilt == "Unfiltered") && (thisAnemA.windData[thisInd].filterFlag >= 0) && (thisAnemB.windData[thisInd].filterFlag >= 0)) // unfiltered (not missing or out of range)
                    concCount++;

                thisInd++;

            }

            Array.Resize(ref ratiosWS.WS_Ratio, concCount);
            Array.Resize(ref ratiosWS.WS, concCount);

            concCount = 0;

            thisInd = startInd;

            while (thisInd < thisAnemA.windData.Length)
            {
                if (thisAnemA.windData[thisInd].timeStamp > endDate)
                    break;

                if ((filtOrUnfilt == "Filtered") && (thisAnemA.windData[thisInd].filterFlag == 0) && (thisAnemB.windData[thisInd].filterFlag == 0))
                {
                    if (ratioOrDiff == "Ratio") ratiosWS.WS_Ratio[concCount] = thisAnemA.windData[thisInd].avg / thisAnemB.windData[thisInd].avg;
                    if (ratioOrDiff == "Diff") ratiosWS.WS_Ratio[concCount] = thisAnemA.windData[thisInd].avg - thisAnemB.windData[thisInd].avg;
                    ratiosWS.WS[concCount] = GetMaxWS_atHeight(thisAnemA.height, thisInd);
                    concCount++;
                }
                else if ((filtOrUnfilt == "Unfiltered") && (thisAnemA.windData[thisInd].filterFlag >= 0) && (thisAnemB.windData[thisInd].filterFlag >= 0)) // unfiltered (not missing or out of range)
                {
                    if (ratioOrDiff == "Ratio") ratiosWS.WS_Ratio[concCount] = thisAnemA.windData[thisInd].avg / thisAnemB.windData[thisInd].avg;
                    if (ratioOrDiff == "Diff") ratiosWS.WS_Ratio[concCount] = thisAnemA.windData[thisInd].avg - thisAnemB.windData[thisInd].avg;
                    ratiosWS.WS[concCount] = GetMaxWS_atHeight(thisAnemA.height, thisInd);
                    concCount++;
                }

                thisInd++;
            }

            return ratiosWS;
        }

        /// <summary> Calculates and returns wind speed difference and wind speed data between start and end dates for specified anems. Used in scatterplots on GUI. </summary>
        public WS_Ratio_WS_Data GetWS_DiffAndWS_TwoAnems(Anem_Data anemA, Anem_Data anemB, string filtOrUnfilt)
        {
            WS_Ratio_WS_Data ratiosWS = new WS_Ratio_WS_Data();               

            // count number of concurrent data intervals
            int concCount = 0;
            if ((anemA.height == 0) || (anemB.height == 0))
                return ratiosWS;

            int thisInd = 0;

            while (anemA.windData[thisInd].timeStamp < startDate)
                thisInd++;

            int startInd = thisInd;

            while (thisInd < anemA.windData.Length)
            {
                if (anemA.windData[thisInd].timeStamp > endDate)
                    break;

                if ((filtOrUnfilt == "Filtered") && (anemA.windData[thisInd].filterFlag == 0) && (anemB.windData[thisInd].filterFlag == 0))
                    concCount++;
                else if ((filtOrUnfilt == "Unfiltered") && (anemA.windData[thisInd].filterFlag >= 0) && (anemB.windData[thisInd].filterFlag >= 0)) // unfiltered (not missing or out of range)
                    concCount++;

                thisInd++;

            }

            Array.Resize(ref ratiosWS.WS_Ratio, concCount);
            Array.Resize(ref ratiosWS.WS, concCount);

            concCount = 0;

            thisInd = startInd;

            while (thisInd < anemA.windData.Length)
            {
                if (anemA.windData[thisInd].timeStamp > endDate)
                    break;

                if ((filtOrUnfilt == "Filtered") && (anemA.windData[thisInd].filterFlag == 0) && (anemB.windData[thisInd].filterFlag == 0))
                {                    
                    ratiosWS.WS_Ratio[concCount] = anemA.windData[thisInd].avg - anemB.windData[thisInd].avg;
                    ratiosWS.WS[concCount] = Math.Max(anemA.windData[thisInd].avg, anemB.windData[thisInd].avg);
                    //   ratiosWS.WS[concCount] = GetMaxWS_atHeight(anemA.height, thisInd);
                    concCount++;
                }
                else if ((filtOrUnfilt == "Unfiltered") && (anemA.windData[thisInd].filterFlag >= 0) && (anemB.windData[thisInd].filterFlag >= 0)) // unfiltered (not missing or out of range)
                {                    
                    ratiosWS.WS_Ratio[concCount] = anemA.windData[thisInd].avg - anemB.windData[thisInd].avg;
                    ratiosWS.WS[concCount] = Math.Max(anemA.windData[thisInd].avg, anemB.windData[thisInd].avg);
                    //   ratiosWS.WS[concCount] = GetMaxWS_atHeight(anemA.height, thisInd);
                    concCount++;
                }

                thisInd++;
            }

            return ratiosWS;
        }

        /// <summary> Finds and returns concurrent (filtered or unfiltered) wind speeds at redundant sensors at specified height or at two specified anems and between start and end dates. </summary>
        public Conc_WS_Data GetConcWS(int anemHeight, string filtOrUnfilt, Continuum thisInst, string metName, Anem_Data anemA, Anem_Data anemB)
        {             
            Conc_WS_Data concWS = new Conc_WS_Data();                      

            Anem_Data thisAnemA = new Anem_Data();
            Anem_Data thisAnemB = new Anem_Data();

            if (GetNumAnems() == 0)
                return concWS;
            else
            {
                if (anems[0].windData == null)
                    GetSensorDataFromDB(thisInst, metName);
            }

            if (anemA.height == 0 && anemB.height == 0)
            {
                foreach (Anem_Data thisAnem in anems)
                {
                    if ((Math.Abs(thisAnem.height - anemHeight) < 2) && (thisAnem.ID == 'A'))
                        thisAnemA = thisAnem;

                    if ((Math.Abs(thisAnem.height - anemHeight) < 2) && (thisAnem.ID == 'B'))
                        thisAnemB = thisAnem;
                }
            }
            else
            {
                thisAnemA = anemA;
                thisAnemB = anemB;
            }

            if ((thisAnemA.height == 0) || (thisAnemB.height == 0) || thisAnemA.windData == null || thisAnemB.windData == null)
                return concWS;
                       
            // count number of concurrent data intervals
            int concCount = 0;            
            int thisInd = 0;

            while (thisAnemA.windData[thisInd].timeStamp < startDate)
                thisInd++;

            int startInd = thisInd;

            while (thisInd < thisAnemA.windData.Length)
            {
                if (thisAnemA.windData[thisInd].timeStamp > endDate)
                    break;
                
                if ((filtOrUnfilt == "Filtered") && (thisAnemA.windData[thisInd].filterFlag == 0) && (thisAnemB.windData[thisInd].filterFlag == 0))
                    concCount++;
                else if ((filtOrUnfilt == "Unfiltered") && (thisAnemA.windData[thisInd].filterFlag >= 0) && (thisAnemB.windData[thisInd].filterFlag >= 0)) // unfiltered but not missing or out of range flags
                    concCount++;

                thisInd++;

            }

            Array.Resize(ref concWS.anemA_WS, concCount);
            Array.Resize(ref concWS.anemB_WS, concCount);

            concCount = 0;
            thisInd = startInd;

            while (thisInd < thisAnemA.windData.Length)
            {
                if (thisAnemA.windData[thisInd].timeStamp > endDate)
                    break;

                if ((filtOrUnfilt == "Filtered") && (thisAnemA.windData[thisInd].filterFlag == 0) && (thisAnemB.windData[thisInd].filterFlag == 0))
                {
                    concWS.anemA_WS[concCount] = thisAnemA.windData[thisInd].avg;
                    concWS.anemB_WS[concCount] = thisAnemB.windData[thisInd].avg;
                    concCount++;
                }
                else if ((filtOrUnfilt == "Unfiltered") && (thisAnemA.windData[thisInd].filterFlag >= 0) && (thisAnemB.windData[thisInd].filterFlag >= 0))  // unfiltered
                {
                    concWS.anemA_WS[concCount] = thisAnemA.windData[thisInd].avg;
                    concWS.anemB_WS[concCount] = thisAnemB.windData[thisInd].avg;
                    concCount++;
                }

                thisInd++;
            }

            return concWS;
        }
                
        /// <summary> Calculates and returns the average extrapolated wind speed. </summary>        
        public double CalcAvgExtrapolatedWS(Sim_TS thisSim)
        {            
            double avgWS = 0;
            int WS_Count = 0;
            int thisInd = 0;

            while (thisSim.WS_WD_data[thisInd].timeStamp < startDate)
                thisInd++;

            while (thisInd < thisSim.WS_WD_data.Length)
            {
                if (thisSim.WS_WD_data[thisInd].timeStamp > endDate)
                    break;
                
                if (thisSim.WS_WD_data[thisInd].WS != -999 && thisSim.WS_WD_data[thisInd].WD != -999)
                { 
                    avgWS = avgWS + thisSim.WS_WD_data[thisInd].WS;
                    WS_Count++;                                            
                }                

                thisInd++;
            }

            if (WS_Count > 0) avgWS = avgWS / WS_Count;

            return avgWS;
        }
                
        /// <summary> Calculates and returns the average wind speed of filtered or unfiltered data. </summary>        
        public double CalcAvgWS(Anem_Data thisAnem, bool filtered)
        {
            double avgWS = 0;
            int dataCount = 0;
            int thisInd = 0;
            
            while (thisAnem.windData[thisInd].timeStamp < startDate)
                thisInd++;

            while (thisInd < thisAnem.windData.Length)
            {
                if (thisAnem.windData[thisInd].timeStamp > endDate)
                    break;
                
                if ((filtered == false) && (thisAnem.windData[thisInd].filterFlag != Filter_Flags.missing) && (thisAnem.windData[thisInd].filterFlag != Filter_Flags.outsideRange))
                {
                    avgWS = avgWS + thisAnem.windData[thisInd].avg;                    
                    dataCount++;
                }
                else
                {
                    if (thisAnem.windData[thisInd].filterFlag == 0)
                    {
                        avgWS = avgWS + thisAnem.windData[thisInd].avg;
                        dataCount++;
                    }
                }

                thisInd++;
            }

            if (dataCount > 0)
                avgWS = avgWS / dataCount;

            return avgWS;
        }
                
        /// <summary> Calculates and returns the anemometer data recoverybetween start and end dates. </summary>       
        public double CalcAnemDataRecovery(Anem_Data thisAnem, bool filtered)
        {            
            double dataRec = 0;
            int dataCount = 0;
            int thisInd = 0;
            
            while (thisInd < thisAnem.windData.Length && thisAnem.windData[thisInd].timeStamp < startDate)
                thisInd++;
                        
            while (thisInd < thisAnem.windData.Length)
            {
                if (thisAnem.windData[thisInd].timeStamp > endDate)
                    break;

                if ((filtered == false) && (thisAnem.windData[thisInd].filterFlag >= 0))
                {
                    if (thisAnem.windData[thisInd].avg > 0)
                        dataCount++;
                }
                else
                {
                    if (thisAnem.windData[thisInd].filterFlag == 0)
                    {
                        if (thisAnem.windData[thisInd].avg > 0)
                            dataCount++;
                    }
                }

                thisInd++;
            }

            // Check the timestamp interval            
            int firstHourInd = 0;
            int firstHour = thisAnem.windData[firstHourInd].timeStamp.Hour;
            
            while (thisAnem.windData[firstHourInd].timeStamp.Minute != 0)
            {
                firstHourInd++;
                firstHour = thisAnem.windData[firstHourInd].timeStamp.Hour;
            }                

            int numPointsPerHour = 0;

            while (thisAnem.windData[firstHourInd].timeStamp.Hour == firstHour)
            {
                numPointsPerHour++;
                firstHourInd++;
            }                

            if (thisAnem.windData.Length > 0)
            {
                double totesDays = (endDate.Subtract(startDate).TotalDays) * 24 * numPointsPerHour + 1;
                dataRec = dataCount / totesDays; // data recovery over whole period (Start to End)
            }

            return dataRec;
        }
                
        /// <summary> Calculates and returns the filtered/unfiltered vane data recovery between start and end dates. </summary>        
        public double CalcVaneDataRecovery(Vane_Data thisVane, bool filtered)
        {            
            double dataRec = 0;
            int dataCount = 0;
            int thisInd = 0;

            while (thisInd < thisVane.dirData.Length && thisVane.dirData[thisInd].timeStamp < startDate)
                thisInd++;

            while (thisInd < thisVane.dirData.Length)
            {
                if (thisVane.dirData[thisInd].timeStamp > endDate)
                    break;

                if ((filtered == false) && (thisVane.dirData[thisInd].filterFlag >= 0))
                {
                    if (thisVane.dirData[thisInd].avg >= 0)
                        dataCount++;
                }
                else
                {
                    if (thisVane.dirData[thisInd].filterFlag == 0)
                    {
                        if (thisVane.dirData[thisInd].avg >= 0)
                            dataCount++;
                    }
                }
                thisInd++;
            }

            // Check the timestamp interval            
            int firstHourInd = 0;
            int firstHour = thisVane.dirData[firstHourInd].timeStamp.Hour;

            while (thisVane.dirData[firstHourInd].timeStamp.Minute != 0)
            {
                firstHourInd++;
                firstHour = thisVane.dirData[firstHourInd].timeStamp.Hour;
            }

            int numPointsPerHour = 0;

            while (thisVane.dirData[firstHourInd].timeStamp.Hour == firstHour)
            {
                numPointsPerHour++;
                firstHourInd++;
            }
            
            if (thisVane.dirData.Length > 0)
            {
                double totesDays = endDate.Subtract(startDate).TotalDays * 24 * numPointsPerHour + 1;
                dataRec = dataCount / totesDays; // data recovery over whole period (Start to End)
            }

            return dataRec;
        }
                
        /// <summary> Calculates and returns the temperature sensor data recovery between the start and end dates. </summary>        
        public double CalcTempDataRecovery(Temp_Data thisTemp)
        {                        
            int dataCount = 0;
            int thisInd = 0;

            while (thisInd < thisTemp.temp.Length && thisTemp.temp[thisInd].timeStamp < startDate)
                thisInd++;

            while (thisInd < thisTemp.temp.Length)
            {
                if (thisTemp.temp[thisInd].timeStamp > endDate)
                    break;

                if (thisTemp.temp[thisInd].filterFlag == 0)
                    dataCount++;
                thisInd++;
            }                

            double totesDays = endDate.Subtract(startDate).TotalDays * 24 * 6 + 1;
            double dataRec = 1.0;
            
            if (totesDays > 0)
                dataRec = dataCount / totesDays; // data recovery over whole period (Start to End)
                        
            return dataRec;
        }
        
        /// <summary> Calculates the percent of anemometer data filtered due to specified flag between start and end dates. </summary>        
        public double CalcPercentAnemFiltered(Anem_Data thisAnem, Filter_Flags thisFlag)
        {
            double percFilt = 0;
            int thisInd = 0;

            while (thisInd < thisAnem.windData.Length && thisAnem.windData[thisInd].timeStamp < startDate)
                thisInd++;

            while (thisInd < thisAnem.windData.Length )
            {
                if (thisAnem.windData[thisInd].timeStamp > endDate)
                    break;

                if (thisAnem.windData[thisInd].filterFlag == thisFlag)
                    percFilt++;
                thisInd++;
            }     
                         
            if (endDate > startDate)
                percFilt = percFilt / (endDate.Subtract(startDate).TotalDays * 24 * 6);
            
            return percFilt;
        }                
       
        /// <summary> Calculates the percent of vane data filtered due to specified flag between start and end dates. </summary>        
        public double CalcPercentVaneFiltered(Vane_Data thisVane, Filter_Flags thisFlag)
        {
            double percFilt = 0;
            int thisInd = 0;

            while (thisInd < thisVane.dirData.Length && thisVane.dirData[thisInd].timeStamp < startDate)
                thisInd++;

            while (thisInd < thisVane.dirData.Length)
            {
                if (thisVane.dirData[thisInd].timeStamp > endDate)
                    break;

                if (thisVane.dirData[thisInd].filterFlag == thisFlag)
                    percFilt++;

                thisInd++;
            }

            if (endDate > startDate)
                percFilt = percFilt / (endDate.Subtract(startDate).TotalDays * 24 * 6);

            return percFilt;
        }
        
        /// <summary> Gets maximum wind speed at specified height and at a given timestamp. </summary>        
        public double GetMaxWS_atHeight(double anemHeight, int TS_index)
        {            
            double thisMaxWS = 0;

            Anem_Data thisAnemA = new Anem_Data();
            Anem_Data thisAnemB = new Anem_Data();

            foreach (Anem_Data thisAnem in anems)
            {
                if ((Math.Abs(thisAnem.height - anemHeight) < 2) && (thisAnem.ID == 'A'))
                    thisAnemA = thisAnem;

                if ((Math.Abs(thisAnem.height - anemHeight) < 2) && (thisAnem.ID == 'B'))
                    thisAnemB = thisAnem;
            }

            if (thisAnemA.isOnlyMet == false)
                if (thisAnemA.windData[TS_index].avg > thisAnemB.windData[TS_index].avg)
                    thisMaxWS = thisAnemA.windData[TS_index].avg;
                else
                    thisMaxWS = thisAnemB.windData[TS_index].avg;
            
            return thisMaxWS;
        }

        /// <summary> Returns true if specified filter is in list of filters to apply or if "All" filters are applied </summary>        
        public bool IsFilterInList(string[] filterList, string thisFilter)
        {
            bool isInList = false;

            if (filterList != null)
            {
                for (int i = 0; i < filterList.Length; i++)
                    if (filterList[i] == thisFilter)
                        isInList = true;

                if (filterList[0] == "All")
                    isInList = true;
            }
             
            return isInList;
        }

        public bool HaveAnem_SD_Data(Anem_Data thisAnemData)
        {
            // Returns true if dataset has SD data (if all == 0 then return false)
            bool gotSD_Data = false;

            if (thisAnemData.windData != null)
            {
                for (int i = 0; i < thisAnemData.windData.Length; i++)
                    if (thisAnemData.windData[i].SD != 0)
                    {
                        gotSD_Data = true;
                        break;
                    }
            }

            return gotSD_Data;
        }

        /// <summary> Quality check and filter data based on specified filtering flags ("All", "Min WS", "Icing", "Min WS SD", "Max WS SD", "Max WS Range", Tower Shadow") </summary>        
        public void FilterData(string[] filtersToApply)
        { 
            // clear extrapolated data
            alpha = new Est_Alpha[0];
            alphaByAnem = new Shear_Data[0];
            simData = new Sim_TS[0];    
                    
            // Apply all filters except for tower shadow
            for (int i = 0; i < GetDataLength(); i++)
            {                                   
                for (int j = 0; j < GetNumAnems(); j++)
                {
                    bool haveAnemSD = HaveAnem_SD_Data(anems[j]);

                    if ((anems[j].windData[i].filterFlag != Filter_Flags.missing) && (anems[j].windData[i].filterFlag != Filter_Flags.outsideRange))
                    {                        
                        int vaneInd = GetVaneClosestToHH(anems[j].height);                                                
                                              
                        // min WS filter
                        if ((anems[j].windData[i].avg < minWS_Filt && GetClosestWS(j,i) > minClosestWS) && IsFilterInList(filtersToApply, "Min WS"))
                            anems[j].windData[i].filterFlag = Filter_Flags.minWS;
                                               
                        // Icing filter
                        if (haveAnemSD && (anems[j].windData[i].filterFlag == Filter_Flags.Valid) && (GetNumTemps() > 0) && IsFilterInList(filtersToApply, "Icing"))
                        {
                            // find a Valid temperature reading for icing filter
                            double validTemp = -999;
                            for (int k = 0; k < GetNumTemps(); k++)
                                if ((temps[k].temp[i].filterFlag != Filter_Flags.missing) && (temps[k].temp[i].filterFlag != Filter_Flags.outsideRange))
                                    validTemp = temps[k].temp[i].avg;

                            if ((validTemp != -999) && (validTemp <= GetMaxIcingTemp(temps[0])) && (anems[j].windData[i].SD <= anemIcingMinSD) &&
                                (vanes[vaneInd].dirData[i].SD <= vaneIcingMinSD))
                                anems[j].windData[i].filterFlag = Filter_Flags.Icing;
                        }                                               

                        // min SD filter
                        if (haveAnemSD && (anems[j].windData[i].filterFlag == Filter_Flags.Valid) && (anems[j].windData[i].avg > 1) && (anems[j].windData[i].SD <= minSD_FiltSlope * anems[j].windData[i].avg) && IsFilterInList(filtersToApply, "Min WS SD"))
                            anems[j].windData[i].filterFlag = Filter_Flags.minAnemSD;                       
                                                                     
                        // max SD filter
                        if (haveAnemSD && (anems[j].windData[i].filterFlag == Filter_Flags.Valid) && (anems[j].windData[i].SD >= maxSD_FiltSlope * anems[j].windData[i].avg + maxSD_FiltInt) && IsFilterInList(filtersToApply, "Max WS SD"))
                            anems[j].windData[i].filterFlag = Filter_Flags.maxAnemSD;                                       
                        
                        // max - min Range filter
                        if ((anems[j].windData[i].filterFlag == Filter_Flags.Valid) && ((anems[j].windData[i].max - anems[j].windData[i].min) >= maxRange) && IsFilterInList(filtersToApply, "Max WS Range"))
                            anems[j].windData[i].filterFlag = Filter_Flags.maxAnemRange;                                               
                                              
                    }
                }

                for (int j = 0; j < GetNumVanes(); j++)
                {
                    if ((vanes[j].dirData[i].filterFlag != Filter_Flags.outsideRange) && (vanes[j].dirData[i].filterFlag != Filter_Flags.missing))
                    {
                        // Icing filter
                        if ((GetNumTemps() > 0) && IsFilterInList(filtersToApply, "Icing"))
                        {
                            // find a Valid temperature reading
                            double validTemp = -999;
                            for (int k = 0; k < GetNumTemps(); k++)
                                if ((temps[k].temp[i].filterFlag != Filter_Flags.missing) && (temps[k].temp[i].filterFlag != Filter_Flags.outsideRange))
                                    validTemp = temps[k].temp[i].avg;

                            if ((validTemp != -999) && ((vanes[j].dirData[i].filterFlag != Filter_Flags.missing) && (validTemp <= GetMaxIcingTemp(temps[0])) && (vanes[j].dirData[i].SD <= vaneIcingMinSD)))
                                vanes[j].dirData[i].filterFlag = Filter_Flags.Icing;

                        }
                    }
                }
            }

            DefineTowerShadowSectors();

            for (int i = 0; i < GetDataLength(); i++)
            {
                for (int j = 0; j < GetNumAnems(); j++)
                {
                    if (anems[j].windData[i].filterFlag == Filter_Flags.Valid)
                    {
                        int vaneInd = GetVaneClosestToHH(anems[j].height);

                        // Tower shadow filter
                        if (IsAffectedByTower(vanes[vaneInd].dirData[i].avg, j) && IsFilterInList(filtersToApply, "Tower Shadow"))
                            anems[j].windData[i].filterFlag = Filter_Flags.towerEffect;
                    }
                }
            }

            filteringDone = true;
            
        }
        
        /// <summary> Gets wind speed of closest anemometer at specified timestamp. </summary>        
        public double GetClosestWS(int anemInd, int TS_ind)
        {            
            double minDist = 100;
            int closestInd = 0;

            for (int i = 0; i < GetNumAnems(); i++)
            {
                if (i != anemInd)
                {
                    if ((Math.Abs(anems[anemInd].height - anems[i].height)) < minDist)
                    {
                        minDist = Math.Abs(anems[anemInd].height - anems[i].height);
                        closestInd = i;
                    }
                }
            }

            double closestWS = anems[closestInd].windData[TS_ind].avg;
            
            return closestWS;
        }
        
        /// <summary> Checks thisWD to see if it falls within the tower shadow sector defined for Anem[anemInd]. Returns true if thisWD is in tower shadow. </summary>        
        public bool IsAffectedByTower(double thisWD, int anemInd)
        {
            bool isInShadow = false;

            if (anems[anemInd].shadow.center == -999)
            {
                isInShadow = false; // no tower shadow defined
            }
            else if (anems[anemInd].shadow.maxWD > anems[anemInd].shadow.minWD)
            {
                if ((thisWD > anems[anemInd].shadow.minWD) && (thisWD < anems[anemInd].shadow.maxWD))
                    isInShadow = true;
            }
            else // minWD > maxWD so it crosses over 0
            {
                if ((thisWD > anems[anemInd].shadow.minWD) || (thisWD < anems[anemInd].shadow.maxWD))
                    isInShadow = true;
            }
                        
            return isInShadow;
        }
                
        /// <summary> Defines tower shadow sectors at each anemometer level. </summary>        
        public void DefineTowerShadowSectors()
        {
            for (int i = 0; i < GetNumAnems(); i++)
            {
                if (anems[i].isOnlyMet == true)
                {
                    if (anems[i].orientation - 180 > 0)                    
                        anems[i].shadow.center = anems[i].orientation - 180;                    
                    else
                        anems[i].shadow.center = anems[i].orientation + 180;

                    anems[i].shadow.minWD = anems[i].shadow.center - towerShadowWidth / 2;
                    if (anems[i].shadow.minWD < 0) anems[i].shadow.minWD = anems[i].shadow.minWD + 360;

                    anems[i].shadow.maxWD = anems[i].shadow.center + towerShadowWidth / 2;
                    if (anems[i].shadow.maxWD > 359) anems[i].shadow.maxWD = anems[i].shadow.maxWD - 360;
                }

                if ((anems[i].isOnlyMet == false) && (anems[i].shadow.minWD == 0) && (anems[i].shadow.maxWD == 0)) 
                {                  
                    // Get WS difference data
                    WS_Ratio_WD_Data diffsAndWD = GetWS_RatioOrDiffAndWD(anems[i].height, "Filtered", "Diff", true);
                    
                    // Calculate average WS difference
                    double[] avgWS_Diffs = CalcAvgWS_DiffsByWD(diffsAndWD);

                    // De-bias avgWS
                    double[] debiasedWS_Diffs = DebiasAvgWS_Diff(avgWS_Diffs);

                    // Find tower shadow sectors (Anem[i] is Anem_A
                    int[] indA_andB = GetAnemsClosestToHH(anems[i].height);
                    anems[indA_andB[0]].shadow.center = FindMinMaxWS_Diff(debiasedWS_Diffs, "min");
                    anems[indA_andB[1]].shadow.center = FindMinMaxWS_Diff(debiasedWS_Diffs, "max");

                    int[] theseShadowBounds = FindShadowMinMax(debiasedWS_Diffs, anems[indA_andB[0]].shadow.center);

                    if ((theseShadowBounds[0] == 0) && (theseShadowBounds[1] == 0))
                    {
                        //anems[indA_andB[0]].shadow.Found_Sector = false;
                    }
                    else
                    {
                        anems[indA_andB[0]].shadow.minWD = theseShadowBounds[0];
                        anems[indA_andB[0]].shadow.maxWD = theseShadowBounds[1];
                    }

                    theseShadowBounds = FindShadowMinMax(debiasedWS_Diffs, anems[indA_andB[1]].shadow.center);
                    if ((theseShadowBounds[0] == 0) && (theseShadowBounds[1] == 0))
                    {
                        //anems[indA_andB[1]].shadow.Found_Sector = false;
                    }
                    else
                    {
                        anems[indA_andB[1]].shadow.minWD = theseShadowBounds[0];
                        anems[indA_andB[1]].shadow.maxWD = theseShadowBounds[1];
                    }
                }
            }     
            
        }

        
        /// <summary> Reads in array of wind speed differences and normalizes to an average wind speed difference of 0.0. Returns debiased wind speed differences. </summary>        
        public double[] DebiasAvgWS_Diff(double[] avgWS_Diff)
        {
            double[] debiasedDiffs = new double[avgWS_Diff.Length];
            double avgDiff = 0;

            for (int i = 0; i < avgWS_Diff.Length; i++)            
                avgDiff = avgDiff + avgWS_Diff[i];

            avgDiff = avgDiff / avgWS_Diff.Length;

            for (int i = 0; i < avgWS_Diff.Length; i++)
                debiasedDiffs[i] = avgWS_Diff[i] - avgDiff;

            return debiasedDiffs;
        }
        
        /// <summary> Finds the tower shadow wind direction range based on array of average wind speed difference and specified tower shadow center. </summary>        
        public int[] FindShadowMinMax(double[] avgWS_Diffs, double shadowCenter)
        {
            int[] shadowMinMax = new int[2];

            if (shadowCenter == -999) // no tower shadow
                return shadowMinMax;

            // find shadow min WD            
            int centerInd = GetWD_Ind(shadowCenter, avgWS_Diffs.Length);
            bool foundMinWD = false;
            bool foundMaxWD = false;

            for (int i = 0; i < avgWS_Diffs.Length; i++)
            {
                int thisWD_Ind = centerInd - i;
                if (thisWD_Ind < 0) thisWD_Ind = thisWD_Ind + avgWS_Diffs.Length;
                
                if (Math.Abs(avgWS_Diffs[thisWD_Ind]) < towerShadowThresh)
                {
                    shadowMinMax[0] = thisWD_Ind * 360 / avgWS_Diffs.Length;
                    foundMinWD = true;
                    break;
                }
            }

            // Find shadow max WD
            for (int i = 0; i < avgWS_Diffs.Length; i++)
            {
                int thisWD_Ind = centerInd + i;
                if (thisWD_Ind >= avgWS_Diffs.Length) thisWD_Ind = thisWD_Ind - avgWS_Diffs.Length;

                if (Math.Abs(avgWS_Diffs[thisWD_Ind]) < towerShadowThresh)
                {
                    shadowMinMax[1] = thisWD_Ind * 360 / avgWS_Diffs.Length;
                    foundMaxWD = true;
                    break;
                }
            }

            if (foundMinWD == false) shadowMinMax[0] = Convert.ToInt16(shadowCenter - towerShadowThresh / 2);
            if (shadowMinMax[0] < 0) shadowMinMax[0] = shadowMinMax[0] + 360;

            if (foundMaxWD == false) shadowMinMax[1] = Convert.ToInt16(shadowCenter + towerShadowThresh / 2);
            if (shadowMinMax[1] > 359) shadowMinMax[1] = shadowMinMax[1] - 360;

            return shadowMinMax;
        }
       
        /// <summary> Finds the wind direction index with either the min or max average wind speed difference. Used in tower shadow sector definition. </summary>        
        public int FindMinMaxWS_Diff(double[] avgWS_Diffs, string minOrMax)
        {
            int minOrMaxWD = 0;
            double thisMin = 1000;
            double thisMax = 0;

            bool isTowerShadow = true; // min and max WS diff must be greater than threshold for tower shadow to be flagged

            if (minOrMax == "min")
            {                
                for (int i = 0; i < avgWS_Diffs.Length; i++)
                {
                    if (avgWS_Diffs[i] < thisMin)
                    {
                        minOrMaxWD = i * 360 / avgWS_Diffs.Length;
                        thisMin = avgWS_Diffs[i];                        
                    }
                }
                if (Math.Abs(thisMin) < towerShadowThresh)
                    isTowerShadow = false;
            }
            else
            {                
                for (int i = 0; i < avgWS_Diffs.Length; i++)
                {
                    if (avgWS_Diffs[i] > thisMax)
                    {
                        minOrMaxWD = i * 360 / avgWS_Diffs.Length;
                        thisMax = avgWS_Diffs[i];
                    }
                }
                if (thisMax < towerShadowThresh)
                    isTowerShadow = false;
            }

            if (isTowerShadow == false) minOrMaxWD = -999;
            
            return minOrMaxWD;
        }
        
        /// <summary>  Calculates the average wind speed difference by wind direction. </summary>        
        public double[] CalcAvgWS_DiffsByWD(WS_Ratio_WD_Data diffsAndWD)
        {
            // using 2 deg bins
            double[] avgDiffs = new double[180];
            int[] countDiffs = new int[180];

            if (diffsAndWD.WD == null)
                return avgDiffs;

            for (int i = 0; i < diffsAndWD.WD.Length; i++)
            {
                int WD_Ind = GetWD_Ind(diffsAndWD.WD[i], 180);
                avgDiffs[WD_Ind] = avgDiffs[WD_Ind] + diffsAndWD.WS_Ratio[i];
                countDiffs[WD_Ind]++;
            }

            for (int i = 0; i < 180; i++)
            {
                if (countDiffs[i] > 0)
                    avgDiffs[i] = avgDiffs[i] / countDiffs[i];
            }

            return avgDiffs;
        }
                
        /// <summary> Gets index of vane closest to but not higher than specified HH (hub height). </summary>
        public int GetVaneClosestToHH(double thisHH)
        {
            int vaneInd = 0;
            double heightDiff = 1000;

            for (int i = 0; i < GetNumVanes(); i++)
            {
                if ((thisHH - vanes[i].height >= 0) && (Math.Abs(vanes[i].height - thisHH) < heightDiff))
                {
                    vaneInd = i;
                    heightDiff = Math.Abs(vanes[i].height - thisHH);
                }
            }

            if (heightDiff == 1000) // thisHH is lower than lowest measurement
                vaneInd = 0;

            return vaneInd;
        }
       
        /// <summary> Gets indices of anemometers closest to (but not higher than) specified height. </summary>
        public int[] GetAnemsClosestToHH(double thisHH)
        {
            int numAnemsAtH = 1;
            int[] anemInd = new int[numAnemsAtH];
            double heightDiff = 1000;
            
            // find first Anem
            for (int i = 0; i < GetNumAnems(); i++)
            {
                if ((thisHH - anems[i].height >= 0) && (Math.Abs(anems[i].height - thisHH) < heightDiff)) 
                {                    
                    anemInd[0] = i;
                    heightDiff = Math.Abs(anems[i].height - thisHH);
                }
            }

            if (heightDiff == 1000) // thisHH is lower than lowest measurement so find lowest anemometer
            {
                double lowestHeight = 1000;
                for (int i = 0; i < GetNumAnems(); i++)
                {
                    if (anems[i].height < lowestHeight)
                    {
                        lowestHeight = anems[i].height;
                        anemInd[0] = i;
                    } 
                }
            }

            // Find redundant sensor
            for (int i = 0; i < GetNumAnems(); i++)
                if (anemInd[0] != i && Math.Abs(anems[i].height - anems[anemInd[0]].height) <= 2)
                {
                    numAnemsAtH++;
                    Array.Resize(ref anemInd, numAnemsAtH);
                    anemInd[numAnemsAtH - 1] = i;
                }
                    

            return anemInd;
        }

        /// <summary> Gets anemometer height closest to modeled height. </summary>        
        public int GetHeightClosestToHH(double thisHH)
        {
            double heightDiff = 1000;
            int heightInd = 0;
            double[] anemHeights = GetHeightsOfAnems();

            for (int i = 0; i < anemHeights.Length; i++)
            {
                if ((thisHH - anemHeights[i] >= 0) && (Math.Abs(anemHeights[i] - thisHH) < heightDiff))
                {
                    heightInd = i;
                    heightDiff = Math.Abs(anems[i].height - thisHH);
                }
            }

            if (heightDiff == 1000) // thisHH is lower than lowest measurement
            {
                double lowestLevel = 1000;

                for (int i = 0; i < anemHeights.Length; i++)
                {
                    if (anemHeights[i] < lowestLevel)
                    {
                        lowestLevel = anemHeights[i];
                        heightInd = i;
                    }
                }
            }

            return heightInd;
        }


        /// <summary> Gets maximum temperature that Icing occurs (in C or F). </summary>        
        public double GetMaxIcingTemp(Temp_Data thisTemp)
        {          
            double thisMaxTemp = maxIcingTemp; // in F

            if (thisTemp.C_or_F == 'C')
                thisMaxTemp = (maxIcingTemp - 32) * 5 / 9;

            return thisMaxTemp;
        }       
                
        /// <summary> Clears the filter flags and estimated data. </summary>        
        public void ClearFilterFlagsAndEstimatedData()
        {
            for (int i = 0; i < GetDataLength(); i++)
            {
                for (int j = 0; j < GetNumAnems(); j++)
                    if (anems[j].windData[i].filterFlag >= 0) anems[j].windData[i].filterFlag = 0;
                for (int j = 0; j < GetNumVanes(); j++)
                    if (vanes[j].dirData[i].filterFlag >= 0) vanes[j].dirData[i].filterFlag = 0;
            }

            // clear tower shadow
            for (int i = 0; i < GetNumAnems(); i++)
            {
                anems[i].shadow.center = 0;
                anems[i].shadow.minWD = 0;
                anems[i].shadow.maxWD = 0;
            }

            alpha = new Est_Alpha[0];
            alphaByAnem = new Shear_Data[0];
            simData = new Sim_TS[0];

            filteringDone = false;            
        }                
        
        /// <summary> Creates simulated time series of extrapolated data at specified height. </summary>
        public void ExtrapolateData(double thisHeight)
        {
            Array.Resize(ref simData, simData.Length + 1);
            simData[simData.Length - 1].height = thisHeight; 
                        
            int heightInd = GetHeightClosestToHH(thisHeight); 

            // Check to see if thisHeight is same as the closest anemometer.
            bool haveExtrapHeight = false;

            for (int i = 0; i < GetNumAnems(); i++)
                if (anems[i].height == thisHeight)                
                    haveExtrapHeight = true;                   
                    
            if (haveExtrapHeight == true)
            {
                // Count number of data points with both Valid anem and vane
                int validCount = 0;
                int vaneInd = GetVaneClosestToHH(thisHeight);
                int[] anemInds = GetAnemsClosestToHH(thisHeight);
                int numAnems = anemInds.Length;

                for (int i = 0; i < anems[anemInds[0]].windData.Length; i++)
                {
                    bool gotOne = false;

                    for (int a = 0; a < numAnems; a++)
                        if (anems[anemInds[a]].windData[i].filterFlag == Filter_Flags.Valid)
                            gotOne = true;

                    if (gotOne && vanes[vaneInd].dirData[i].filterFlag == Filter_Flags.Valid)
                        validCount++;
                }
                    

                Array.Resize(ref simData[simData.Length - 1].WS_WD_data, validCount);

                try
                {
                    validCount = 0;
                    for (int i = 0; i < anems[anemInds[0]].windData.Length; i++)
                    {
                        bool gotOne = false;

                        for (int a = 0; a < numAnems; a++)
                            if (anems[anemInds[a]].windData[i].filterFlag == Filter_Flags.Valid)
                                gotOne = true;

                        if (gotOne && vanes[vaneInd].dirData[i].filterFlag == Filter_Flags.Valid)
                        {
                            double avgWS = 0;
                            double avgSD = 0;
                            int avgWSCount = 0;

                            for (int a = 0; a < numAnems; a++)
                                if (anems[anemInds[a]].windData[i].filterFlag == Filter_Flags.Valid)
                                {
                                    avgWS = avgWS + anems[anemInds[a]].windData[i].avg;
                                    avgSD = avgSD + anems[anemInds[a]].windData[i].SD;
                                    avgWSCount++;
                                }

                            if (avgWSCount > 0)
                            {
                                avgWS = avgWS / avgWSCount;
                                avgSD = avgSD / avgWSCount;

                                simData[simData.Length - 1].WS_WD_data[validCount].timeStamp = anems[anemInds[0]].windData[i].timeStamp;
                                simData[simData.Length - 1].WS_WD_data[validCount].WS = avgWS;
                                simData[simData.Length - 1].WS_WD_data[validCount].SD = avgSD;
                                simData[simData.Length - 1].WS_WD_data[validCount].WD = vanes[vaneInd].dirData[i].avg;

                                validCount++;
                            }
                        }
                    }
                }
                catch 
                {
                    MessageBox.Show("Error extrapolating data.", "Continuum 3");
                }
                                
            }
            else
            { 
                if (alphaByAnem.Length == 0)
                    EstimateAlpha();

                if (alphaByAnem.Length == 0)
                    return;

                if (alphaByAnem[heightInd].WS_WD_Alpha == null)
                    return;

                Array.Resize(ref simData[simData.Length - 1].WS_WD_data, alphaByAnem[heightInd].WS_WD_Alpha.Length);

                for (int i = 0; i < alphaByAnem[heightInd].WS_WD_Alpha.Length; i++)
                {
                    simData[simData.Length - 1].WS_WD_data[i].timeStamp = alphaByAnem[heightInd].WS_WD_Alpha[i].timeStamp;
                    simData[simData.Length - 1].WS_WD_data[i].WD = alphaByAnem[heightInd].WS_WD_Alpha[i].WD;

                    if (alphaByAnem[heightInd].WS_WD_Alpha[i].alpha != -999 && alphaByAnem[heightInd].WS_WD_Alpha[i].WS != -999)
                    {                        
                        simData[simData.Length - 1].WS_WD_data[i].WS = alphaByAnem[heightInd].WS_WD_Alpha[i].WS *
                            Math.Pow((thisHeight / alphaByAnem[heightInd].anemHeight), alphaByAnem[heightInd].WS_WD_Alpha[i].alpha);

                        // SD filter: SD must be less than WS / 3
                        if (alphaByAnem[heightInd].WS_WD_Alpha[i].SD < alphaByAnem[heightInd].WS_WD_Alpha[i].WS / 3 &&
                            alphaByAnem[heightInd].WS_WD_Alpha[i].SD != 0)
                            simData[simData.Length - 1].WS_WD_data[i].SD = alphaByAnem[heightInd].WS_WD_Alpha[i].SD;
                        else
                            simData[simData.Length - 1].WS_WD_data[i].SD = -999;

                        simData[simData.Length - 1].WS_WD_data[i].alpha = alphaByAnem[heightInd].WS_WD_Alpha[i].alpha;

                    }
                    else
                    { 
                        simData[simData.Length - 1].WS_WD_data[i].WS = -999;
                        simData[simData.Length - 1].WS_WD_data[i].SD = alphaByAnem[heightInd].WS_WD_Alpha[i].SD; // Should this just be -999?
                        simData[simData.Length - 1].WS_WD_data[i].alpha = -999;
                    }

                }
            }

            SortSimDataByH();
        }
        
        /// <summary> Estimates time series of alpha (power law shear exponent). Using average of filtered wind speeds at each height, 
        /// calculates shear between each pair of heights and finds overall average shear exponent. </summary>
        public void EstimateAlpha()
        {
            // find heights of anemometers
            double[] anemHeights = GetHeightsOfAnems();
            int numHeights = anemHeights.Length;

            if (numHeights <= 1)
                return;

            // Get index of vane closest to (but not higher than) HH
            int vaneInd80 = GetVaneClosestToHH(80);
            int thisInd = 0;
            
            if (GetNumAnems() == 0)
                return;
            
            while (anems[0].windData[thisInd].timeStamp < startDate)
                thisInd++;

            int startInd = thisInd;
            int endInd = anems[0].windData.Length - 1;

            while (anems[0].windData[endInd].timeStamp > endDate)
                endInd--;

            Array.Resize(ref alpha, (endInd - startInd + 1));
            Array.Resize(ref alphaByAnem, numHeights);

            for (int m = 0; m < numHeights; m++)
            {
                alphaByAnem[m].WS_WD_Alpha = new Est_Data[endInd - startInd + 1];
                alphaByAnem[m].anemHeight = anemHeights[m];
            }                

            while (thisInd < GetDataLength() && anems[0].windData[thisInd].timeStamp <= endDate)
            {
                // get average WS by height
                double[] avgWS_ByH = GetAvgValidByHeight(thisInd, "avg");
                double[] avgSD_ByH = GetAvgValidByHeight(thisInd, "SD");                               

                // get average alpha
                double thisAlpha = GetAvgAlphaFromValidWS(avgWS_ByH, anemHeights);                               

                alpha[thisInd - startInd].timeStamp = anems[0].windData[thisInd].timeStamp;
                alpha[thisInd - startInd].alpha = thisAlpha;                              

                if (vanes[vaneInd80].dirData[thisInd].filterFlag == Filter_Flags.Valid)
                    alpha[thisInd - startInd].WD = vanes[vaneInd80].dirData[thisInd].avg;
                else
                    alpha[thisInd - startInd].WD = -999;

                for (int m = 0; m < numHeights; m++)
                {
                    alphaByAnem[m].WS_WD_Alpha[thisInd - startInd].timeStamp = anems[0].windData[thisInd].timeStamp;
                    alphaByAnem[m].WS_WD_Alpha[thisInd - startInd].WS = avgWS_ByH[m];
                    alphaByAnem[m].WS_WD_Alpha[thisInd - startInd].SD = avgSD_ByH[m];
                    alphaByAnem[m].WS_WD_Alpha[thisInd - startInd].alpha = thisAlpha;                                      
                    
                    // check to see if there is a Valid wind direction at this timestamp
                    int HH_Vane_ind = GetVaneClosestToHH(anemHeights[m]);

                    if (vanes[HH_Vane_ind].dirData[thisInd].filterFlag == Filter_Flags.Valid)
                        alphaByAnem[m].WS_WD_Alpha[thisInd - startInd].WD = vanes[HH_Vane_ind].dirData[thisInd].avg;
                    else
                        alphaByAnem[m].WS_WD_Alpha[thisInd - startInd].WD = -999;
                }

                thisInd++;
            }
                        
        }
        
        /// <summary> Gets average shear exponent alpha calculated between each pair of valid wind speed measurement heights. </summary>
        public double GetAvgAlphaFromValidWS(double[] avgWS_ByH, double[] anemHeights)
        {
            double avgAlpha = 0;
            int numHeights = anemHeights.Length;
            int numValidPairs = 0;

            for (int i = 0; i < numHeights; i++)
            {
                for (int j = i+1; j < numHeights; j++)
                {
                    double thisAlpha = Math.Log(avgWS_ByH[i] / avgWS_ByH[j]) / Math.Log(anemHeights[i] / anemHeights[j]);
                    if ((Math.Abs(thisAlpha) < 2.5) && avgWS_ByH[i] != -999 && avgWS_ByH[j] != -999)
                    {
                        avgAlpha = avgAlpha + thisAlpha;
                        numValidPairs++;
                    }
                }
            }

            if (numValidPairs > 0)
                avgAlpha = avgAlpha / numValidPairs;
            else
                avgAlpha = -999;

            return avgAlpha;
        }
       
        /// <summary> Calculates and returns average valid wind speed or standard deviation at each wind speed measurement height at specified time series index. </summary>       
        public double[] GetAvgValidByHeight(int dataInd, string avgOrSD)
        {
            double[] anemHeights = GetHeightsOfAnems();
            double[] avgVal = new double[anemHeights.Length];
            int[] avgWS_Count = new int[anemHeights.Length];

            foreach (Anem_Data thisAnem in anems)
            {
                for (int i = 0; i < anemHeights.Length; i++)
                {
                    if ((Math.Abs(anemHeights[i] - thisAnem.height) <= 2) && thisAnem.windData[dataInd].filterFlag == Filter_Flags.Valid)
                    {
                        if (avgOrSD == "avg")
                            avgVal[i] = avgVal[i] + thisAnem.windData[dataInd].avg;
                        else
                            avgVal[i] = avgVal[i] + thisAnem.windData[dataInd].SD;

                        avgWS_Count[i]++;
                    }
                }
            }

            for (int i = 0; i < avgVal.Length; i++)
            {
                if (avgWS_Count[i] > 0)
                    avgVal[i] = avgVal[i] / avgWS_Count[i];
                else
                    avgVal[i] = -999;
            }
                
            return avgVal;
        }    
               
        /// <summary> Gets maximum wind speed and gust for specified year. </summary>        
        public Yearly_Maxes GetMaxWS_AndGust(int thisYear, double HH)
        {
            Yearly_Maxes theseMaxVals = new Yearly_Maxes();
            
            int[] anemInds = GetAnemsClosestToHH(HH);
            int numAnemsAtH = anemInds.Length;
            bool gotAnem2 = false;
            if (numAnemsAtH >= 2)
                gotAnem2 = true;
            
            theseMaxVals.maxWS = 0;
            theseMaxVals.maxGust = 0;

            for (int i = 0; i < GetDataLength(); i++)
            {
                if (anems[anemInds[0]].windData[i].timeStamp.Year == thisYear)
                {
                    if (anems[anemInds[0]].windData[i].avg > theseMaxVals.maxWS)
                    {
                        theseMaxVals.timeStampMaxWS = anems[anemInds[0]].windData[i].timeStamp;
                        theseMaxVals.maxWS = anems[anemInds[0]].windData[i].avg;
                    }

                    if (gotAnem2)
                    {
                        if (anems[anemInds[1]].windData[i].avg > theseMaxVals.maxWS)
                        {
                            theseMaxVals.timeStampMaxWS = anems[anemInds[1]].windData[i].timeStamp;
                            theseMaxVals.maxWS = anems[anemInds[1]].windData[i].avg;
                        }
                    }

                    if (anems[anemInds[0]].windData[i].max > theseMaxVals.maxGust)
                    {
                        theseMaxVals.timeStampMaxGust = anems[anemInds[0]].windData[i].timeStamp;
                        theseMaxVals.maxGust = anems[anemInds[0]].windData[i].max;
                    }

                    if (gotAnem2)
                    {
                        if (anems[anemInds[1]].windData[i].max > theseMaxVals.maxGust)
                        {
                            theseMaxVals.timeStampMaxGust = anems[anemInds[1]].windData[i].timeStamp;
                            theseMaxVals.maxGust = anems[anemInds[1]].windData[i].max;
                        }
                    }
                }
            }                       

            return theseMaxVals;
        }                                   
                
        /// <summary> Calculates and returns the recovery of the extrapolated data set between start and end dates. </summary>        
        public double CalcExtrapRecovery(Sim_TS thisSim)
        {            
            int numValid = 0;
            int thisInd = 0;

            while (thisSim.WS_WD_data[thisInd].timeStamp < startDate)
                thisInd++;

            while (thisSim.WS_WD_data[thisInd].timeStamp < endDate && thisInd < (thisSim.WS_WD_data.Length - 1))
            {
                if (thisSim.WS_WD_data[thisInd].WS != -999 && thisSim.WS_WD_data[thisInd].WD != -999)
                    numValid++;

                thisInd++;
            }
            
            double extrapRec = numValid / (endDate.Subtract(startDate).TotalDays * 24 * 6 + 1);                       

            return extrapRec;
        }
        
        /// <summary> Sorts the extrapolated data by height (low height first) </summary>        
        public void SortSimDataByH()
        {            
            int numSim = GetNumSimData();
            Sim_TS[] tempSims = new Sim_TS[numSim];

            for (int i = 0; i < numSim; i++)
            {
                double minH = 1000.0;
                for (int j = 0; j < numSim; j++)
                {
                    if (i == 0)
                    {
                        if (simData[j].height < minH)
                        {
                            minH = simData[j].height;
                            tempSims[i] = simData[j];
                        }
                    }
                    else
                    {
                        if (simData[j].height < minH && simData[j].height > tempSims[i - 1].height)
                        {
                            minH = simData[j].height;
                            tempSims[i] = simData[j];
                        }
                    }
                }
            }

            simData = tempSims;
        }

        /// <summary> Finds and returns simulated time series dataset at specified height. </summary>     
        public Sim_TS GetSimulatedTimeSeries(double extrapHeight)
        {
            Sim_TS thisSim = new Sim_TS();

            for (int i = 0; i < GetNumSimData(); i++)            
                if (simData[i].height == extrapHeight)                
                    thisSim = simData[i];                               
            
            if (thisSim.WS_WD_data == null)
            {                
                EstimateAlpha();
                ExtrapolateData(extrapHeight);

                for (int i = 0; i < GetNumSimData(); i++)
                    if (simData[i].height == extrapHeight)
                    thisSim = simData[i];
            }

            return thisSim;
        }

        /// <summary> Finds start / end dates with maximum recovery at anems and vanes. Calculates average anem and average vane recovery for each full 
        /// year period (moving window = 1 month) and finds average of the two </summary>     
        public void FindStartEndDatesWithMaxRecovery()
        {
            if (GetNumAnems() == 0 || GetNumVanes() == 0) // if there is no anem or vane data then return
                return;

            double maxRec = 0.0;
            DateTime maxStart = allStartDate;
            startDate = allStartDate;
            endDate = allEndDate;
            TimeSpan dataLength = allEndDate - allStartDate;
            int numIntYears = (int)Math.Floor((double)dataLength.Days / 365);
            DateTime maxEnd = startDate.AddYears(numIntYears);

            double thisAnemRec = 0.0;
            double thisVaneRec = 0.0;
            double avgRec;

            if (numIntYears == 0) // less than one year of data so use all
                return;

            if (maxEnd.AddMonths(1) > endDate) // there isn't more than one year and one month of data so compare first year and last year period.
            {
                // Find average recovery using first year period and compare to last year period
                endDate = startDate.AddYears(1);
                foreach (Anem_Data thisAnem in anems)
                    thisAnemRec = thisAnemRec + CalcAnemDataRecovery(thisAnem, true);

                thisAnemRec = thisAnemRec / GetNumAnems();

                foreach (Vane_Data thisVane in vanes)
                    thisVaneRec = thisVaneRec + CalcVaneDataRecovery(thisVane, true);

                thisVaneRec = thisVaneRec / GetNumVanes();

                avgRec = (thisAnemRec + thisVaneRec) / 2;

                if (avgRec > maxRec)
                {
                    maxRec = avgRec;
                    maxStart = startDate;
                    maxEnd = endDate;
                }

                // Find average recovery over last year period
                endDate = allEndDate;
                startDate = endDate.AddYears(-1);
                thisAnemRec = 0;
                thisVaneRec = 0;

                foreach (Anem_Data thisAnem in anems)
                    thisAnemRec = thisAnemRec + CalcAnemDataRecovery(thisAnem, true);

                thisAnemRec = thisAnemRec / GetNumAnems();

                foreach (Vane_Data thisVane in vanes)
                    thisVaneRec = thisVaneRec + CalcVaneDataRecovery(thisVane, true);

                thisVaneRec = thisVaneRec / GetNumVanes();

                avgRec = (thisAnemRec + thisVaneRec) / 2;

                if (avgRec > maxRec)
                {
                    maxRec = avgRec;
                    maxStart = startDate;
                    maxEnd = endDate;
                }

                startDate = maxStart;
                endDate = maxEnd;

                return;
            }                
            else
                endDate = maxEnd;            
                       
            while (endDate < allEndDate)
            {
                foreach (Anem_Data thisAnem in anems)                
                    thisAnemRec = thisAnemRec + CalcAnemDataRecovery(thisAnem, true);

                thisAnemRec = thisAnemRec / GetNumAnems();

                foreach (Vane_Data thisVane in vanes)
                    thisVaneRec = thisVaneRec + CalcVaneDataRecovery(thisVane, true);

                avgRec = (thisAnemRec + thisVaneRec) / 2;

                if (avgRec > maxRec)
                {
                    maxRec = avgRec;
                    maxStart = startDate;
                    maxEnd = endDate;                    
                }

                startDate = startDate.AddMonths(1);
                endDate = endDate.AddMonths(1);
                thisAnemRec = 0;
                thisVaneRec = 0;
            }

            startDate = maxStart;
            endDate = maxEnd;
            
        }

        /// <summary> Adds anemometer, vane, and temperature data to database and then clears it from Met_Data_Filter object  </summary>  
        public void AddSensorDatatoDBAndClear(Continuum thisInst, string metName)
        {            
            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);
                     
            // Save anemometer data
            for (int i = 0; i < GetNumAnems(); i++)
            {
                double thisHeight = anems[i].height;
                string thisSensorChar = anems[i].ID.ToString();

                // Check to see if it's already in database
                using (var context = new Continuum_EDMContainer(connString))
                {
                    var sensorData = from N in context.Anem_table where N.metName == metName && N.height == thisHeight && N.sensorChar == thisSensorChar select N;
                    if (sensorData.Count() == 0)
                    {
                        Anem_table anemTable = new Anem_table();
                        anemTable.metName = metName;
                        anemTable.height = anems[i].height;
                        anemTable.sensorChar = anems[i].ID.ToString();

                        MemoryStream MS1 = new MemoryStream();
                        bin.Serialize(MS1, anems[i].windData);
                        anemTable.windData = MS1.ToArray();

                        try
                        {
                            context.Anem_table.Add(anemTable);
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.InnerException.ToString());                            
                            return;
                        }
                    }
                    
                }
                // Clear anem data
                anems[i].windData = null;

            }

            // Save vane data
            for (int i = 0; i < GetNumVanes(); i++)
            {
                double thisHeight = vanes[i].height;
                
                // Check to see if it's already in database
                using (var context = new Continuum_EDMContainer(connString))
                {
                    var sensorData = from N in context.Vane_table where N.metName == metName && N.height == thisHeight select N;
                    if (sensorData.Count() == 0)
                    {
                        Vane_table vaneTable = new Vane_table();
                        vaneTable.metName = metName;
                        vaneTable.height = vanes[i].height;
                        
                        MemoryStream MS1 = new MemoryStream();
                        bin.Serialize(MS1, vanes[i].dirData);
                        vaneTable.dirData = MS1.ToArray();

                        try
                        {
                            context.Vane_table.Add(vaneTable);
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.InnerException.ToString());                            
                            return;
                        }
                    }
                    
                }
                // Clear vane data
                vanes[i].dirData = null;

            }

            // Save temperature data
            for (int i = 0; i < GetNumTemps(); i++)
            {
                double thisHeight = temps[i].height;

                // Check to see if it's already in database
                using (var context = new Continuum_EDMContainer(connString))
                {
                    var sensorData = from N in context.Temp_table where N.metName == metName && N.height == thisHeight select N;
                    if (sensorData.Count() == 0)
                    {
                        Temp_table tempTable = new Temp_table();
                        tempTable.metName = metName;
                        tempTable.height = temps[i].height;

                        MemoryStream MS1 = new MemoryStream();
                        bin.Serialize(MS1, temps[i].temp);
                        tempTable.temp = MS1.ToArray();

                        try
                        {
                            context.Temp_table.Add(tempTable);
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.InnerException.ToString());                            
                            return;
                        }
                    }
                    
                }
                // Clear temperature data
                temps[i].temp = null;

            }

        }

        /// <summary> Gets sensor time series data from database. </summary>        
        public void GetSensorDataFromDB(Continuum thisInst, string metName)
        {
            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            // Get anemometer data
            for (int i = 0; i < GetNumAnems(); i++)
            {                
                using (var context = new Continuum_EDMContainer(connString))
                {
                    double thisHeight = anems[i].height;
                    string thisID = anems[i].ID.ToString();

                    var sensorData = from N in context.Anem_table where N.metName == metName && N.height == thisHeight && N.sensorChar == thisID select N;

                    foreach (var N in sensorData)
                    {
                        MemoryStream MS = new MemoryStream(N.windData);
                        anems[i].windData = (data[])bin.Deserialize(MS);
                        MS.Close();
                    }
                }               
            }

            // Get vane data
            for (int i = 0; i < GetNumVanes(); i++)
            {
                using (var context = new Continuum_EDMContainer(connString))
                {
                    double thisHeight = vanes[i].height;
                    
                    var sensorData = from N in context.Vane_table where N.metName == metName && N.height == thisHeight select N;

                    foreach (var N in sensorData)
                    {
                        MemoryStream MS = new MemoryStream(N.dirData);
                        vanes[i].dirData = (data[])bin.Deserialize(MS);
                        MS.Close();
                    }
                }
            }

            // Get temperature data
            for (int i = 0; i < GetNumTemps(); i++)
            {
                using (var context = new Continuum_EDMContainer(connString))
                {
                    double thisHeight = temps[i].height;

                    var sensorData = from N in context.Temp_table where N.metName == metName && N.height == thisHeight select N;

                    foreach (var N in sensorData)
                    {
                        MemoryStream MS = new MemoryStream(N.temp);
                        temps[i].temp = (data[])bin.Deserialize(MS);
                        MS.Close();
                    }
                }
            }
        }

        /// <summary> Clears calculated alpha and extrapolated (i.e. simulated) datasets. These are not saved in the file. They're generated as needed. </summary>  
        public void ClearAlphaAndSimulatedEstimates()
        {             
            alpha = new Est_Alpha[0];
            alphaByAnem = new Shear_Data[0];
            simData = new Sim_TS[0]; ;
        }

        /// <summary> Returns met data filter string. </summary> 
        public string GetFlagString(Filter_Flags filter)
        {             
            string fliterFlag = "";

            if (filter == Filter_Flags.Icing)
                fliterFlag = "Icing";
            else if (filter == Filter_Flags.maxAnemRange)
                fliterFlag = "Max Anem Range";
            else if (filter == Filter_Flags.maxAnemSD)
                fliterFlag = "Max Anem SD";
            else if (filter == Filter_Flags.maxDeltaWS)
                fliterFlag = "Max Delta WS";
            else if (filter == Filter_Flags.minAnemSD)
                fliterFlag = "Min Anem SD";
            else if (filter == Filter_Flags.minWS)
                fliterFlag = "Min WS";
            else if (filter == Filter_Flags.missing)
                fliterFlag = "Missing";
            else if (filter == Filter_Flags.outsideRange)
                fliterFlag = "Outside Range";
            else if (filter == Filter_Flags.towerEffect)
                fliterFlag = "Tower Shadow";
            else if (filter == Filter_Flags.Valid)
                fliterFlag = "Valid";

            return fliterFlag;
        }

        /// <summary> Gets name of anemometer </summary>
        public string GetAnemName(Anem_Data thisAnem, bool inclSensType)
        {
            string anemName = "";

            if (inclSensType)
                anemName = "ANEM ";

            anemName = anemName + thisAnem.height.ToString() + "-" + GetBoomOrientChars(thisAnem.orientation);

            return anemName;
        }

        /// <summary> Gets name of wind vane </summary>
        public string GetVaneName(Vane_Data thisVane, bool inclSensType)
        {
            string vaneName = "";

            if (inclSensType)
                vaneName = "VANE ";

            vaneName = vaneName + thisVane.height.ToString() + "-" + GetBoomOrientChars(thisVane.orientation);

            return vaneName;
        }

        /// <summary> Gets name of temperature sensor </summary>
        public string GetTempName(Temp_Data thisTemp, bool inclSensType)
        {
            string tempName = "";

            if (inclSensType)
                tempName = "TEMP ";

            tempName = tempName + thisTemp.height.ToString();

            return tempName;
        }

        /// <summary> Gets name of pressure sensor </summary>
        public string GetPressName(Press_Data thisVane, bool inclSensType)
        {
            string pressName = "";

            if (inclSensType)
                pressName = "BARO ";

            pressName = pressName + thisVane.height.ToString();

            return pressName;
        }

        /// <summary> Returns boom orientation in N-S </summary>
        public string GetBoomOrientChars(double thisOrient)
        {
            string boomOrient = "n";
            int wdInd = Convert.ToInt32(Math.Round(thisOrient / 22.5, 0));

            if (wdInd == 1)
                boomOrient = "nne";
            else if (wdInd == 2)
                boomOrient = "ne";
            else if (wdInd == 3)
                boomOrient = "ene";
            else if (wdInd == 4)
                boomOrient = "e";
            else if (wdInd == 5)
                boomOrient = "ese";
            else if (wdInd == 6)
                boomOrient = "se";
            else if (wdInd == 7)
                boomOrient = "sse";
            else if (wdInd == 8)
                boomOrient = "s";
            else if (wdInd == 9)
                boomOrient = "ssw";
            else if (wdInd == 10)
                boomOrient = "sw";
            else if (wdInd == 11)
                boomOrient = "wsw";
            else if (wdInd == 12)
                boomOrient = "w";
            else if (wdInd == 13)
                boomOrient = "wnw";
            else if (wdInd == 14)
                boomOrient = "nw";
            else if (wdInd == 15)
                boomOrient = "nnw";
            
            return boomOrient;
        }
    }
         
}

