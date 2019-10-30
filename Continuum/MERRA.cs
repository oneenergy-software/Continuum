////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	InsuranceSettlementForm.cs
//
// summary:	Implements the insurance settlement Windows Form
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using OSGeo.GDAL;
using OSGeo.OGR;
using OSGeo.OSR;
using System.Runtime.Serialization.Formatters.Binary;

namespace ContinuumNS
{    
    
    [Serializable()]
    public partial class MERRA
    {        
        /// <summary>   Array of type Wind_Data_and_Prod_Stats to hold interpolated MERRA2 data and calculated monthly and annual energy production. </summary>
        public Wind_Data_and_Prod_Stats interpData;

        /// <summary>   Arrays of type MERRA_Node_Data containing Wind_TS data, UTM and Lat/Long coords and XY indices of closest MERRA nodes and 
        ///             calculated monthly and annual energy production. </summary>
        public MERRA_Node_Data[] MERRA_Nodes = new MERRA_Node_Data[0];
        /// <summary>  Number of surrounding MERRA nodes to pull </summary>
        
        public Export_Params MERRA_Params;
        public bool isUserDefined; // true if not associated with a loaded met site
        
        public TurbineCollection.PowerCurve powerCurve;
        
        public double WS_ScaleFactor = 0.85; // Scaling Factor applied to WS 

        public int numMERRA_Nodes;
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Struct containing date, WS, WD and energy production for each entry. </summary>
        ///
        /// <remarks>   Liz, 1/16/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////               

        [Serializable()]
        public struct Wind_Data_and_Prod_Stats
        {
            public Wind_TS_with_Prod[] TS_Data;            
            public MonthlyProdByYearAndLTAvg[] Monthly_Prod; // one for every month
            public YearlyProdAndLTAvg Annual_Prod;      
            public UTM_conversion.Lat_Long Coords;
            public UTM_conversion.UTM_coords UTM;
            public int UTC_offset;         
        }

        /// <summary>
        /// Contains time stamp, wind speed, direction, pressure, and temperature data
        /// </summary>
        [Serializable()]
        public struct Wind_TS
        {            
            public DateTime ThisDate;            
            public double WS50m;            
            public double WD50m;            
      //      public double Prod;

       //     public double WS10m;
       //     public double WD10m;
            public double SurfPress;
            public double SeaPress;
            public double Temp10m;
      //      public double Mean_Cloud_Fraction; // MODIS cloud area fraction
      //      public double Optical_Thick;  // MODIS cloud optical thickness
      //      public double Total_Cloud_Area_Fraction;  // ISCCP cloud area fraction
      //      public double Precip; // total precipitation from M2T1NXFLX data set
      //      public double Precip_Corr; // bias corrected total precipitation
        }

        [Serializable()]
        public struct Wind_TS_with_Prod
        {
            public DateTime ThisDate;
            public double WS50m;
            public double WD50m;
            public double Prod;

            //     public double WS10m;
            //     public double WD10m;
            public double SurfPress;
            public double SeaPress;
            public double Temp10m;
            //      public double Mean_Cloud_Fraction; // MODIS cloud area fraction
            //      public double Optical_Thick;  // MODIS cloud optical thickness
            //      public double Total_Cloud_Area_Fraction;  // ISCCP cloud area fraction
            //      public double Precip; // total precipitation from M2T1NXFLX data set
            //      public double Precip_Corr; // bias corrected total precipitation
        }

        [Serializable()]
        public struct Conc_Data
        {
            public DateTime ThisDate;
            public double Swiss_WS;
            public double Interp_WS;
        }

        [Serializable()]
        public struct MERRA_Node_Data
        {
            public Wind_TS[] TS_Data;
            public XYIndices XY_ind;            
         //   public MonthlyProdByYearAndLTAvg[] Monthly_Prod; // one for every month
         //   public YearlyProdAndLTAvg Annual_Prod; 
        }

        /// <summary> Holds coordinates, UTM coordinates, MERRA2 file indices and time series data of MERRA2 node </summary>
        public struct MERRA_Pull
        {
            public UTM_conversion.Lat_Long Coords;
            public UTM_conversion.UTM_coords UTM;
            public XYIndices XY_ind;
            public Wind_TS[] Data;
        }
        
        /// <summary>   Contains the Lat/Long and X/Y index (i.e. in MERRA2 datafile) of MERRA2 nodes. </summary>
        [Serializable()]
        public struct XYIndices
        {            
            public int X_ind;            
            public double Lat;            
            public int Y_ind;            
            public double Lon;
        }                         

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Struct containing the specified month, an array of type Year_and_Prod which 
        ///             holds the year and monthly production, and the average overall monthly production. </summary>
        ///
        /// <remarks>   Liz, 1/16/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Serializable()]
        public struct MonthlyProdByYearAndLTAvg
        {
            /// <summary>   Month. </summary>
            public int Month;
            /// <summary>   Array of Monthly energy production by year </summary>
            public YearAndProd[] YearProd;
            /// <summary>   The LT average monthly energy production. </summary>
            public double LT_Avg;
        }
        [Serializable()]
        public struct YearlyProdAndLTAvg
        {
            /// <summary>   Annual energy production by year. </summary>
            public YearAndProd[] Yearly_Prod;
            /// <summary>   Long-term average annual energy production  </summary>
            public double LT_Avg;             
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Information about the project. </summary>
        ///
        /// <remarks>   Liz, 1/16/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [Serializable()]
        public struct YearAndProd
        {
            public int year;
            public double prod;
        }

        [Serializable()]
        public struct Export_Params
        {
            public bool Get_50mWSWD;            
            public bool Get_10mWSWD;            
            public bool Get_SurfPress;
            public bool Get_SeaPress;
            public bool Get_10mTemp;
            public bool Get_FracMean;
            public bool Get_OpticalThick;
            public bool Get_TotalFrac;
            public bool Get_Precip;
            public bool Get_Corr_Precip;
        }

        /// <summary> Holds an array of timestamps and east/north (i.e. U/V) wind speeds at 10 and 50 m </summary>
        public struct East_North_WSs
        {
            public DateTime[] timeStamp;
            public double[] U50; // arrays to hold 24 hours of data (or whatever the length of the MERRA_pull is
            public double[] V50;
            public double[] U10;
            public double[] V10;
        }                    

        /// <summary>
        /// CLEAR and RESET STUFF
        /// </summary>

        public void ClearInterpData()
        {
            interpData.TS_Data = new Wind_TS_with_Prod[0];          
            interpData.Monthly_Prod = null;
            interpData.Annual_Prod.Yearly_Prod = null;
            interpData.Annual_Prod.LT_Avg = 0;
            interpData.Coords.latitude = 0;
            interpData.Coords.longitude = 0;
            interpData.UTM.UTMEasting = 0;
            interpData.UTM.UTMNorthing = 0;
            interpData.UTC_offset = 0;
          
        }

        public void Clear_MERRA2_Node_Data()
        {
            if (MERRA_Nodes.Length > 0)
                for (int i = 0; i < MERRA_Nodes.Length; i++)                
                    MERRA_Nodes[i].TS_Data = new Wind_TS[0];             
        }                    

        public void ClearAll()
        {            
            ClearInterpData();           
            Clear_MERRA2_Node_Data();                                 
        }                 
        
        public void Reset_MERRA_Pull(ref MERRA_Pull[] thisMERRA_Pull)
        {
            if (thisMERRA_Pull == null)
                return;

            for (int i = 0; i < thisMERRA_Pull.Length; i++)
            {
                for (int j = 0; j < thisMERRA_Pull[i].Data.Length; j++)
                {
                    thisMERRA_Pull[i].Data[j].ThisDate = Convert.ToDateTime("1/1/1980");
                    thisMERRA_Pull[i].Data[j].WS50m = 0;
                    thisMERRA_Pull[i].Data[j].WD50m = 0;
                 //   thisMERRA_Pull[i].Data[j].WS10m = 0;
                 //   thisMERRA_Pull[i].Data[j].WD10m = 0;
                 //   thisMERRA_Pull[i].Data[j].Prod = 0;
                    thisMERRA_Pull[i].Data[j].Temp10m = 0;
                    thisMERRA_Pull[i].Data[j].SurfPress = 0;
                    thisMERRA_Pull[i].Data[j].SeaPress = 0;
                 //   thisMERRA_Pull[i].Data[j].Mean_Cloud_Fraction = 0;
                 //   thisMERRA_Pull[i].Data[j].Optical_Thick = 0;
                 //   thisMERRA_Pull[i].Data[j].Total_Cloud_Area_Fraction = 0;
                 //   thisMERRA_Pull[i].Data[j].Precip = 0;
                 //   thisMERRA_Pull[i].Data[j].Precip_Corr = 0;
                }
                
            }

        }

        public void Reset_MonthProdStats()
        {
            if (interpData.Monthly_Prod == null)
                Array.Resize(ref interpData.Monthly_Prod, 12);

            Array.Clear(interpData.Monthly_Prod, 0, interpData.Monthly_Prod.Length);

            for (int i = 0; i < 12; i++)
            {                               
                Array.Resize(ref interpData.Monthly_Prod[i].YearProd, 0);
                Array.Clear(interpData.Monthly_Prod[i].YearProd, 0, interpData.Monthly_Prod[i].YearProd.Length);
                interpData.Monthly_Prod[i].Month = i + 1;                              
            }                       
                     
        }

        public void Reset_AnnualProdStats()
        {
            interpData.Annual_Prod.LT_Avg = 0;
            interpData.Annual_Prod.Yearly_Prod = null;
        }

        /// <summary>
        /// BOOL FLAGS
        /// </summary>

        public bool GotMonthlyProd()
        {
            bool gotIt = false;

            if (interpData.Monthly_Prod != null)
                gotIt = true;

            return gotIt;
        }

        public bool GotWindTS(UTM_conversion utm)
        {
            bool gotIt = false;

            if (interpData.TS_Data == null)
            {
                GetInterpData(utm);
                if (interpData.TS_Data != null)
                    if (interpData.TS_Data.Length > 0)
                        gotIt = true;
            }
            else if (interpData.TS_Data.Length == 0)
            {
                GetInterpData(utm);
                if (interpData.TS_Data != null)
                    if (interpData.TS_Data.Length > 0)
                        gotIt = true;
            }
            else
                gotIt = true;
                        
            return gotIt;
        }               

        public bool Got_Yearly_Prod(YearlyProdAndLTAvg[] thisData)
        {
            bool gotIt = false;

            if (thisData != null)
                gotIt = true;

            return gotIt;
        }            
 
        public void Size_East_North_WS_Data(ref East_North_WSs[] theseUVs, int New_Size)
        {
            if (theseUVs == null)
                return;

            for (int i = 0; i < theseUVs.Length; i++)
            {
                Array.Resize(ref theseUVs[i].U10, New_Size);
                Array.Resize(ref theseUVs[i].V10, New_Size);
                Array.Resize(ref theseUVs[i].U50, New_Size);
                Array.Resize(ref theseUVs[i].V50, New_Size);
            }
        }

        public bool Need_This_Param(string thisString)
        {
            bool Need_it = false;
            int String_Len = thisString.Length;

            if (String_Len >= 11)
                if (thisString.Substring(0, 11) == "PRECTOTCORR" && MERRA_Params.Get_Corr_Precip == true) // Bias-corrected precipitation
                    Need_it = true; 

            if (String_Len >= 8)
                if (thisString.Substring(0, 8) == "PRECTOT[" && MERRA_Params.Get_Precip == true) // Precipitation
                    Need_it = true; 

            if (String_Len >= 9)            
                if (thisString.Substring(0, 9) == "MDSOPTHCK" && MERRA_Params.Get_OpticalThick == true) // Optical Thickness Total Mean))
                    Need_it = true; // Optical Thickness Total Mean))

            if (String_Len >= 8)
                if (thisString.Substring(0, 8) == "ISCCPCLD" && MERRA_Params.Get_TotalFrac == true) // Total Cloud Area Fraction )
                    Need_it = true; // ISCCP Total Cloud Area Fraction

            if (String_Len >= 6)
                if (thisString.Substring(0, 6) == "MDSCLD" && MERRA_Params.Get_FracMean == true) // Cloud Fraction Total mean))
                    Need_it = true; // MODIS Cloud Fraction Total mean))

            if (String_Len >= 3)
                if ((thisString.Substring(0, 3) == "U50" && MERRA_Params.Get_50mWSWD == true) ||
                (thisString.Substring(0, 3) == "V50" && MERRA_Params.Get_50mWSWD == true) ||
                (thisString.Substring(0, 3) == "U10" && MERRA_Params.Get_10mWSWD == true) ||
                (thisString.Substring(0, 3) == "V10" && MERRA_Params.Get_10mWSWD == true) ||
                (thisString.Substring(0, 2) == "PS" && MERRA_Params.Get_SurfPress == true) ||
                (thisString.Substring(0, 3) == "SLP" && MERRA_Params.Get_SeaPress == true) ||
                (thisString.Substring(0, 3) == "T10" && MERRA_Params.Get_10mTemp == true))                                    
                    Need_it = true;
            
            return Need_it;
        }
                
        /// <summary>
        /// SET STUFF
        /// </summary>
        public void Set_Interp_LatLon_Dates_Offset(double latitude, double longitude, int offset, Continuum thisInst)
        {            
            interpData.Coords.latitude = Math.Round(latitude, 3);
            interpData.Coords.longitude = Math.Round(longitude, 3);                
            interpData.UTC_offset = offset;

            if (thisInst.UTM_conversions.savedDatumIndex == 100)
                thisInst.UTM_conversions.savedDatumIndex = 0;

            if (thisInst.UTM_conversions.hemisphere == "")
            {
                if (latitude > 0)
                    thisInst.UTM_conversions.hemisphere = "Northern";
                else
                    thisInst.UTM_conversions.hemisphere = "Southern";
            }
                                    
            interpData.UTM = thisInst.UTM_conversions.LLtoUTM(interpData.Coords.latitude, interpData.Coords.longitude);

        }

        /// <summary>
        /// FORMAT STUFF
        /// </summary>

        public string Make_MERRA2_Date_String(DateTime thisDate)
        {
            string datestring = Convert.ToString(thisDate.Year);

            if (thisDate.Month < 10)
                datestring = datestring + "0" + Convert.ToString(thisDate.Month);
            else
                datestring = datestring + Convert.ToString(thisDate.Month);

            if (thisDate.Day < 10)
                datestring = datestring + "0" + Convert.ToString(thisDate.Day);
            else
                datestring = datestring + Convert.ToString(thisDate.Day);
            
            return datestring;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Export data. </summary>
        ///
        /// <remarks>   OEE, 1/16/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string[] Create_DateString_for_Export(DateTime startTime, DateTime endTime)
        {
            string[] DateStrings = new string[2];

            DateStrings[0] = Convert.ToString(startTime.Year);
            DateStrings[1] = Convert.ToString(endTime.Year);

            if (startTime.Month < 10)
                DateStrings[0] += "0" + Convert.ToString(startTime.Month);
            else
                DateStrings[0] += Convert.ToString(startTime.Month);

            if (startTime.Day < 10)
                DateStrings[0] += "0" + Convert.ToString(startTime.Day);
            else
                DateStrings[0] += Convert.ToString(startTime.Day);

            if (endTime.Month < 10)
                DateStrings[1] += "0" + Convert.ToString(endTime.Month);
            else
                DateStrings[1] += Convert.ToString(endTime.Month);

            if (endTime.Day < 10)
                DateStrings[1] += "0" + Convert.ToString(endTime.Day);
            else
                DateStrings[1] += Convert.ToString(endTime.Day);

            return DateStrings;
        }

        public string Get_Dataset_String(string Code_string, string Full_or_Dropdown)
        {
            string Dataset_String = "";

            if (Full_or_Dropdown == "Full")
            {
                if (Code_string == "Interp" && numMERRA_Nodes > 1)
                    Dataset_String = "MERRA2 data interpolated between " + numMERRA_Nodes + " closest nodes";
                else if (Code_string == "Interp")
                    Dataset_String = "Closest MERRA2 node data";
                else
                    Dataset_String = "Swiss Re MERRA2 data";
            }
            else // Dropdown
            {
                if (Code_string == "Interp")
                    Dataset_String = "Interp MERRA2";
                else
                    Dataset_String = "Swiss Re MERRA2";
            }

            return Dataset_String;
        }

        /// <summary>
        /// GET STUFF
        /// </summary>

        public double Get_Annual_Prod(int thisYear)
        {
            double thisAnnualProd = 0;                    
                            
            for (int j = 0; j < interpData.Annual_Prod.Yearly_Prod.Length; j++)
                if (interpData.Annual_Prod.Yearly_Prod[j].year == thisYear)
                {
                    thisAnnualProd = interpData.Annual_Prod.Yearly_Prod[j].prod;
                    break;
                }
            
            return thisAnnualProd;
        }        
                           
              
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Used in Swiss data import. Gets rid of empty strings after 'splitting' the line 
        ///             into substrings. </summary>
        ///
        /// <remarks>   Liz, 1/17/2018. </remarks>
        ///
        /// <param name="substrings">  Substrings read from splitting line of data. </param>
        ///
        /// <returns>   All non-empty substrings. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string[] Get_Rid_of_Empty_String(string[] substrings)
        {
            int numStrings = 0;
            string[] datastrings = new string[0];

            for (int i = 0; i < substrings.Length; i++)
            {
                if (substrings[i] != "")
                {
                    numStrings++;
                    Array.Resize(ref datastrings, numStrings);
                    datastrings[numStrings - 1] = substrings[i];
                }
            }

            return datastrings;
        }              

        public bool Have_Full_Year(Wind_TS_with_Prod[] thisTS, int thisYear)
        {
            bool isFull = false;
            DateTime startDate, endDate;
            bool haveStart = false;
            bool haveEnd = false;           
            
            startDate = Convert.ToDateTime("1/1/" + thisYear);
            endDate = Convert.ToDateTime("12/31/" + thisYear + " 23:00");                   
            
            for (int i = 0; i < thisTS.Length; i++)
            {
                if (thisTS[i].ThisDate == startDate)
                    haveStart = true;

                if (thisTS[i].ThisDate == endDate)
                    haveEnd = true;

                if (haveStart == true && haveEnd == true)
                {
                    isFull = true;
                    break;
                }
            }
            
            return isFull;
        }
               

        public bool Have_Full_Month(Wind_TS_with_Prod[] thisTS, int thisMonth, int thisYear)
        {
            bool isFull = false;
            DateTime startDate, endDate;
            bool haveStart = false;
            bool haveEnd = false;
                                    
            startDate = Convert.ToDateTime(thisMonth + "/1/" + thisYear);

            if (thisMonth == 1 || thisMonth == 3 || thisMonth == 5 || thisMonth == 7 || thisMonth == 8 || thisMonth == 10 || thisMonth == 12)
                endDate = Convert.ToDateTime(thisMonth + "/31/" + thisYear + " 23:00");
            else if (thisMonth == 2 && thisYear % 4 == 0) // leap year
                endDate = Convert.ToDateTime(thisMonth + "/29/" + thisYear + " 23:00");
            else if (thisMonth == 2) // not a leap year
                endDate = Convert.ToDateTime(thisMonth + "/28/" + thisYear + " 23:00");
            else
                endDate = Convert.ToDateTime(thisMonth + "/30/" + thisYear + " 23:00");

            for (int i = 0; i < thisTS.Length; i++)
            {
                if (thisTS[i].ThisDate == startDate)
                    haveStart = true;

                if (thisTS[i].ThisDate == endDate)
                    haveEnd = true;

                if (haveStart == true && haveEnd == true)
                {
                    isFull = true;
                    break;
                }
            }

            return isFull;
        }
                
        public double Get_Energy_Prod(YearlyProdAndLTAvg thisAnnual, MonthlyProdByYearAndLTAvg[] thisMonthly, int thisMonth, int thisYear) // if thisMonth = 100, gets yearly; if thisYear = 100, gets LT
        {
            double thisProd = 0;

            if (thisYear == 100) // long-term energy production
            {
                if (thisMonth == 100)
                    thisProd = thisAnnual.LT_Avg; // Long-term annual energy production
                else
                {
                    int[] monthYearInd = Get_Month_Year_Inds(thisMonthly, thisMonth, thisYear);
                    thisProd = thisMonthly[monthYearInd[0]].LT_Avg; // Long-term monthly energy production
                }
            }
            else
            {
                if (thisMonth == 100)
                {
                    int yearInd = Get_Year_Ind(thisYear, thisAnnual);
                    thisProd = thisAnnual.Yearly_Prod[yearInd].prod; // Annual energy production for specific year
                }
                else
                {
                    int[] monthYearInd = Get_Month_Year_Inds(thisMonthly, thisMonth, thisYear);
                    thisProd = thisMonthly[monthYearInd[0]].YearProd[monthYearInd[1]].prod; // Monthly energy production for specific year
                }                    

            }                        
            
            return thisProd / 1000;
        }

        public Export_Params Get_Export_Params(Continuum thisInst)
        {
            MERRA_Params = new Export_Params();

            if (thisInst.chkWSWD50m.Checked == true)
                MERRA_Params.Get_50mWSWD = true;
            else
                MERRA_Params.Get_50mWSWD = false;
            
       /*     if (thisInst.chkWSWD10m.Checked == true)
                MERRA_Params.Get_10mWSWD = true;  
            else
                MERRA_Params.Get_10mWSWD = false;
                */
            if (thisInst.chkTemp10m.Checked == true)
                MERRA_Params.Get_10mTemp = true;
            else
                MERRA_Params.Get_10mTemp = false;

            if (thisInst.chkPressure.Checked == true)
                MERRA_Params.Get_SurfPress = true;
            else
                MERRA_Params.Get_SurfPress = false;

            if (thisInst.chkSeaLevel.Checked == true)
                MERRA_Params.Get_SeaPress = true;
            else
                MERRA_Params.Get_SeaPress = false;
            /*
                        if (thisInst.chkCloudFracMean.Checked == true)
                            MERRA_Params.Get_FracMean = true;                
                        else
                            MERRA_Params.Get_FracMean = false;

                        if (thisInst.chkOptThick.Checked == true)
                            MERRA_Params.Get_OpticalThick = true;
                        else
                            MERRA_Params.Get_OpticalThick = false;

                        if (thisInst.chkTotalFrac.Checked == true)
                            MERRA_Params.Get_TotalFrac = true;
                        else
                            MERRA_Params.Get_TotalFrac = false;

                        if (thisInst.chk_Precip.Checked == true)
                            MERRA_Params.Get_Precip = true;
                        else
                            MERRA_Params.Get_Precip = false;

                        if (thisInst.chk_Precip_Corr.Checked == true)
                            MERRA_Params.Get_Corr_Precip = true;
                        else
                            MERRA_Params.Get_Corr_Precip = false;
            */
            return MERRA_Params;
        }

        public double Calc_CF(double thisProd, int thisMonth, int thisYear, TurbineCollection.PowerCurve powerCurve) // if This_Month == 100 then it's a yearly CF calc
        {
            double thisCF = 0;
            int numMonthDays = 0;
            int numYearDays = 365;

            if (thisYear != 100 && thisYear % 4 == 0) numYearDays = 366; // leap year
                        
            if (thisMonth == 100)
                thisCF = thisProd / (powerCurve.ratedPower * numYearDays * 24 / 1000);
            else
            {
                if (thisMonth == 1 || thisMonth == 3 || thisMonth == 5 || thisMonth == 7 || thisMonth == 8 || thisMonth == 10 || thisMonth == 12)
                    numMonthDays = 31;
                else if (thisMonth == 2 && thisYear % 4 != 0)
                    numMonthDays = 28;
                else if (thisMonth == 2 && thisYear % 4 == 0)
                    numMonthDays = 29;
                else
                    numMonthDays = 30;

                thisCF = thisProd / (powerCurve.ratedPower / 1000 * numMonthDays * 24); // This_Prod is in MWh
            }
            
            return thisCF;
        }

        public int Get_Year_Ind(int thisYear, YearlyProdAndLTAvg thisAnnual)
        {
            int yearInd = -1;

            if (thisAnnual.Yearly_Prod == null)
                return yearInd;

            for (int i = 0; i < thisAnnual.Yearly_Prod.Length; i++)
                if (thisAnnual.Yearly_Prod[i].year == thisYear)
                {
                    yearInd = i;
                    break;
                }

            return yearInd;
        }

        public int Get_Year_Ind_from_Monthly_Prod(int thisYear, MonthlyProdByYearAndLTAvg thisMonthly)
        {
            int yearInd = -1;

            for (int i = 0; i < thisMonthly.YearProd.Length; i++)
                if (thisMonthly.YearProd[i].year == thisYear)
                {
                    yearInd = i;
                    break;
                }

            return yearInd;
        }

        public int[] Get_Month_Year_Inds(MonthlyProdByYearAndLTAvg[] monthProd, int thisMonth, int thisYear) // i = Month index, j = Year index
        {
            int[] monthYearInd = new int[2];

            if (monthProd == null)
                return monthYearInd;

            for (int i = 0; i < monthProd.Length; i++)
            {
                if (monthProd[i].Month == thisMonth)
                {
                    monthYearInd[0] = i;
                    for (int j = 0; j < monthProd[i].YearProd.Length; j++)
                        if (monthProd[i].YearProd[j].year == thisYear)
                        {
                            monthYearInd[1] = j;
                            break;
                        }
                }
            }

            return monthYearInd;
        }            

        /// <summary>  
        /// CALCULATE AND FIND STUFF
        /// </summary>

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Applies the PC described by Wind. </summary>
        ///
        /// <remarks>   OEE, 1/16/2018. </remarks>
        ///
        /// <param name="Wind"> [in,out] The wind. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ApplyPC(ref Wind_TS_with_Prod[] Wind)
        {
            // Applies selected power curve to wind speed data and inputs data directly into the Wind_TS variable specified in the funcion, must used "ref" inside function
            if (Wind == null)
                return;
            
            for (int i = 0; i < Wind.Length; i++)
            {
                if (Wind[i].WS50m >= 0)
                {
                    double Scaled_WS = Wind[i].WS50m * WS_ScaleFactor;
                    if (Scaled_WS < powerCurve.cutInWS || Scaled_WS > powerCurve.cutOutWS)
                        Wind[i].Prod = 0;
                    else if ((Scaled_WS >= powerCurve.cutInWS) && (Scaled_WS < (powerCurve.ratedWS - .5)))
                    {
                        TurbineCollection turbList = new TurbineCollection();
                        Wind[i].Prod = turbList.GetInterpPowerOrThrust(Scaled_WS, powerCurve, "Power");

                        if (Wind[i].Prod > powerCurve.ratedPower) Wind[i].Prod = powerCurve.ratedPower;                                                
                    }
                    
                    else
                    {
                        Wind[i].Prod = powerCurve.ratedPower;                        
                    }

                }
                else
                    Wind[i].Prod = 0;
            }
        }

        public void CalcAnnualProd(ref YearlyProdAndLTAvg thisAnnual, MonthlyProdByYearAndLTAvg[] thisMonthly, UTM_conversion utm)
        {
            // Calculates the annual energy production based on either the Interp or Swiss dataset and using either a traditional year or contract year
            thisAnnual.Yearly_Prod = null;
            thisAnnual.LT_Avg = 0;

            if (thisMonthly == null)
                return;

            if (thisMonthly[0].YearProd.Length == 0)
                return;
                        
            int allNumYears = thisMonthly[0].YearProd.Length;
            int startYear = thisMonthly[0].YearProd[0].year;

            int numYears = 0;
            int firstYears = 0;
            
            for (int i = 0; i < allNumYears; i++)
            {
                if (GotWindTS(utm))
                {
                    if (Have_Full_Year(interpData.TS_Data, startYear + i))
                    {
                        numYears++;
                        if (firstYears == 0) firstYears = startYear + i;
                    }                                          
                }                                   
            }
                        
            Array.Resize(ref thisAnnual.Yearly_Prod, numYears);
            
            int yearInds = 0;

            // Calculate annual production from monthly production array
            for (int i = 0; i < allNumYears; i++)
            {                
                double yearProd = 0;
                int startMonth = 1;
                
                if (startYear + i >= firstYears && startYear + i < firstYears + numYears)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        int thisMonth = startMonth + j;
                        int thisYear = startYear + i;

                        if (thisMonth > 12)
                        {
                            thisMonth = thisMonth - 12;
                            thisYear = thisYear + 1;
                        }

                        int[] monthYearInd = Get_Month_Year_Inds(thisMonthly, thisMonth, thisYear);
                        yearProd = yearProd + thisMonthly[monthYearInd[0]].YearProd[monthYearInd[1]].prod;
                    }

                    thisAnnual.Yearly_Prod[yearInds].year = startYear + i;
                    thisAnnual.Yearly_Prod[yearInds].prod = yearProd;
                    yearProd = 0;
                    yearInds++;
                    
                    
                }                
            }
                    
            Calc_LT_Avg_Prod(ref thisAnnual);
            
        }

        public double Calc_Avg_or_LT(Wind_TS_with_Prod[] thisTS, int thisMonth, int thisYear, string param) 
        {
            double avgOrLT = 0;
            int count = 0;
            int cloudCount = 0;

            for (int i = 0; i < thisTS.Length; i++)
            {
                if ((thisMonth == 100 || thisTS[i].ThisDate.Month == thisMonth) && 
                    (thisYear == 100 || thisTS[i].ThisDate.Year == thisYear))                    
                {
                    if (param == "50 m WS")
                        avgOrLT = avgOrLT + thisTS[i].WS50m;
                    else if (param == "Energy Prod.")
                        avgOrLT = avgOrLT + thisTS[i].Prod;
                  //  else if (param == "10 m WS")
                  //      avgOrLT = avgOrLT + thisTS[i].WS10m;
                    else if (param == "Surface Pressure")
                        avgOrLT = avgOrLT + thisTS[i].SurfPress/1000;
                    else if (param == "Sea Level Pressure")
                        avgOrLT = avgOrLT + thisTS[i].SeaPress / 1000;
                    else if (param == "10 m Temp")
                        avgOrLT = avgOrLT + thisTS[i].Temp10m - 273.15;
                  /*  else if (param == "MODIS Cloud Fraction")
                    {
                        if (thisTS[i].Mean_Cloud_Fraction < 10)
                        {
                            avgOrLT = avgOrLT + thisTS[i].Mean_Cloud_Fraction;
                            cloudCount++;
                        }
                    }                                                    
                    else if (param == "MODIS Cloud Thickness")
                    {
                        if (thisTS[i].Optical_Thick < 1e+10)
                        {
                            avgOrLT = avgOrLT + thisTS[i].Optical_Thick;
                            cloudCount++;
                        }
                    }
                    
                    else if (param == "ISCCP Cloud Fraction")
                    {
                        if (thisTS[i].Mean_Cloud_Fraction < 10)
                        {
                            avgOrLT = avgOrLT + thisTS[i].Total_Cloud_Area_Fraction;
                            cloudCount++;
                        }
                    }
                    else if (param == "Precipitation")
                        avgOrLT = avgOrLT + thisTS[i].Precip;
                    else if (param == "Corrected Precipitation")
                        avgOrLT = avgOrLT + thisTS[i].Precip_Corr;
                    */
                    count++;
                }

            }

            bool isCloud = false;

            if ((param == "MODIS Cloud Fraction") || (param == "MODIS Cloud Thickness") || (param == "ISCCP Cloud Fraction"))
                isCloud = true;

            if (count > 0 && isCloud == false)
                avgOrLT = avgOrLT / count;
            else if (cloudCount > 0 && isCloud == true)
                avgOrLT = avgOrLT / cloudCount;
            
            return avgOrLT;
            
        }

        /// <summary>
        /// Calculates and returns the average wind rose for specified month and year
        /// </summary>        
        public double[] Calc_Wind_Rose(int thisMonth, int thisYear, UTM_conversion utm)
        {
            int numWD = 16;
            double[] windRose = new double[numWD];
            int allCount = 0;
            
            if (GotWindTS(utm) == false)
                return windRose;

            for (int i = 0; i < interpData.TS_Data.Length; i++)
            {
                if ((thisMonth == 100 || interpData.TS_Data[i].ThisDate.Month == thisMonth) &&
                    (thisYear == 100 || interpData.TS_Data[i].ThisDate.Year == thisYear))
                {
                    int WD_ind = (int)Math.Round(interpData.TS_Data[i].WD50m / (360 / (double)numWD), 0, MidpointRounding.AwayFromZero);
                    if (WD_ind == numWD) WD_ind = 0;
                    windRose[WD_ind]++;
                    allCount++;
                }                    
            }

            if (allCount > 0)
                for (int i = 0; i < numWD; i++)
                    windRose[i] = windRose[i] / allCount;

            return windRose;
        }
               
        /// <summary>
        /// Calculates the long-term average energy production for referenced YearlyProdAndLTAvg object
        /// </summary>        
        public void Calc_LT_Avg_Prod(ref YearlyProdAndLTAvg thisAnnual)
        {            
            double avgLT = 0.0;                                 
            int numYears = 0;
            if (thisAnnual.Yearly_Prod != null) numYears = thisAnnual.Yearly_Prod.Length;
           
            for (int i = 0; i < numYears; i++)
                avgLT = avgLT + thisAnnual.Yearly_Prod[i].prod;
                                                
            if (numYears > 0) thisAnnual.LT_Avg = avgLT / numYears;            
            
        }

        /// <summary> Calculates the wind speed and wind direction based on MERRA U and V wind speeds for each node in thisMERRA_Pull and for each time stamp </summary>        
        public void Calc_MERRA2_WS_WD(ref MERRA_Pull[] thisMERRA_Pull, East_North_WSs[] theseUVs)
        {
            if (thisMERRA_Pull == null || theseUVs == null)
                return;

            int numNodes = thisMERRA_Pull.Length;
            int numHours = thisMERRA_Pull[0].Data.Length;

            for (int i = 0; i < numNodes; i++)
                for (int j = 0; j < numHours; j++)
                {
                    /*        if (theseUVs[i].U10[j] != 0 && theseUVs[i].V10[j] != 0 && MERRA_Params.Get_10mWSWD)
                            {
                                thisMERRA_Pull[i].Data[j].WS10m = Math.Pow((Math.Pow(theseUVs[i].U10[j], 2) + Math.Pow(theseUVs[i].V10[j], 2)), 0.5);
                                thisMERRA_Pull[i].Data[j].WD10m = Math.Atan2(theseUVs[i].V10[j], theseUVs[i].U10[j]) * (180 / Math.PI);
                                thisMERRA_Pull[i].Data[j].WD10m = 270 - thisMERRA_Pull[i].Data[j].WD10m; // this moves WD=0 to north and flips WD to be from wind direction
                                if (thisMERRA_Pull[i].Data[j].WD10m > 360) thisMERRA_Pull[i].Data[j].WD10m = thisMERRA_Pull[i].Data[j].WD10m - 360;
                            }
                    */

                    if (j == 52627)
                        j = j;

                    if (theseUVs[i].U50[j] != 0 && theseUVs[i].V50[j] != 0 && MERRA_Params.Get_50mWSWD)
                    {
                        thisMERRA_Pull[i].Data[j].WS50m = Math.Pow((Math.Pow(theseUVs[i].U50[j], 2) + Math.Pow(theseUVs[i].V50[j], 2)), 0.5);
                        thisMERRA_Pull[i].Data[j].WD50m = Math.Atan2(theseUVs[i].V50[j], theseUVs[i].U50[j]) * (180 / Math.PI);
                        thisMERRA_Pull[i].Data[j].WD50m = 270 - thisMERRA_Pull[i].Data[j].WD50m; // this moves WD=0 to north and flips WD to be from wind direction
                        if (thisMERRA_Pull[i].Data[j].WD50m > 360) thisMERRA_Pull[i].Data[j].WD50m = thisMERRA_Pull[i].Data[j].WD50m - 360;
                    }                                     
                    
                }                        
        }

        /// <summary> Calculates the monthly energy production on a yearly basis and the average long-term monthly energy production </summary>
        public void Calc_MonthProdStats(UTM_conversion utm)
        {
            Reset_MonthProdStats();

            if (GotWindTS(utm) == false) return;

            int startYear = interpData.TS_Data[0].ThisDate.Year;
            int endYear = interpData.TS_Data[interpData.TS_Data.Length - 1].ThisDate.Year;
            int numYears = endYear - startYear + 1;

            // Initialize arrays and populate years
            for (int i = 0; i < 12; i++)
            {
                interpData.Monthly_Prod[i].YearProd = new YearAndProd[numYears];

                for (int j = 0; j < numYears; j++)
                    interpData.Monthly_Prod[i].YearProd[j].year = startYear + j;
            }

            // Go through all data and fill in MonthProdStats monthly energy production for every year            
            for (int i = 0; i < interpData.TS_Data.Length; i++)
            {
                int[] monthYearInd = Get_Month_Year_Inds(interpData.Monthly_Prod, interpData.TS_Data[i].ThisDate.Month, interpData.TS_Data[i].ThisDate.Year);
                interpData.Monthly_Prod[monthYearInd[0]].YearProd[monthYearInd[1]].prod += interpData.TS_Data[i].Prod;
            }

            // Calculate average monthly energy production           
            for (int i = 0; i < interpData.Monthly_Prod.Length; i++)
            {
                int yearCount = 0;

                for (int j = 0; j < interpData.Monthly_Prod[0].YearProd.Length; j++)
                {
                    if (interpData.Monthly_Prod[i].YearProd[j].prod > 0)
                    {
                        interpData.Monthly_Prod[i].LT_Avg = interpData.Monthly_Prod[i].LT_Avg + interpData.Monthly_Prod[i].YearProd[j].prod;
                        yearCount++;
                    }
                }

                interpData.Monthly_Prod[i].LT_Avg = interpData.Monthly_Prod[i].LT_Avg / yearCount;
            }            

        }                   

        public double Calc_Perc_Diff_from_LT_Monthly(MonthlyProdByYearAndLTAvg thisMonthly, int thisYear)
        {
            double percDiff = 0;

            if (thisMonthly.YearProd.Length > 0)
            {
                int yearInd = Get_Year_Ind_from_Monthly_Prod(thisYear, thisMonthly);
                double thisProd = thisMonthly.YearProd[yearInd].prod;
                percDiff = (thisProd - thisMonthly.LT_Avg) / thisMonthly.LT_Avg;
            }

            return percDiff;
        }

        public double Calc_Perc_Diff_from_LT_Yearly(YearlyProdAndLTAvg thisAnnual, int thisYear)

        {
            double percDiff = 0;

            if (thisAnnual.Yearly_Prod.Length > 0)
            {
                int yearInd = Get_Year_Ind(thisYear, thisAnnual);
                double thisProd = thisAnnual.Yearly_Prod[yearInd].prod;
                percDiff = (thisProd - thisAnnual.LT_Avg) / thisAnnual.LT_Avg;
            }

            return percDiff;

        }

        public double Calc_Dev_from_LT(MonthlyProdByYearAndLTAvg[] thisMonthly, YearlyProdAndLTAvg thisAnnual, int thisYear, int thisMonth)
        {
            double devFromLT = 0;
            int yearInd = 0;

            if (thisMonth == 100 && thisYear != 100) // deviation from long-term annual
            {
                yearInd = Get_Year_Ind(thisYear, thisAnnual);
                devFromLT = (thisAnnual.Yearly_Prod[yearInd].prod - thisAnnual.LT_Avg) / thisAnnual.LT_Avg;
            }
            else if (thisMonth != 100 & thisYear != 100) // deviation from long-term monthly
            {
                int[] monthYearInd = Get_Month_Year_Inds(thisMonthly, thisMonth, thisYear);
                devFromLT = (thisMonthly[monthYearInd[0]].YearProd[monthYearInd[1]].prod - thisMonthly[monthYearInd[0]].LT_Avg) / thisMonthly[monthYearInd[0]].LT_Avg;
            }

            return devFromLT;
        }

            
        public bool GetMERRAPullXYIndices(ref MERRA_Pull[] nodesToPull, string MERRAfolder)
        {
            // Opens a MERRA data file, looks at lat/long and assigns Xind and Yind to nodesToPull
            // If any nodesToPull are out of the range, returns false. 

            bool gotAllIndices = false;
            if (nodesToPull == null)
                return gotAllIndices;

            string[] MERRAfiles = Directory.GetFiles(MERRAfolder, "*.ascii");
            string line;

            if (MERRAfiles == null)
            {
                MessageBox.Show("Check specified folderpath and try again.");
                return gotAllIndices;
            }
                        
            StreamReader file;

            try
            {
                file = new StreamReader(MERRAfiles[0]);
            }
            catch
            {
                MessageBox.Show("Error opening MERRA data file. Check that it's not open in another program.");
                return gotAllIndices;
            }

            char[] delims = { ',' };
            int numLats = 0;
            int numLongs = 0;
            double[] lats = new double[numLats];
            double[] longs = new double[numLongs];
                        
            while ((line = file.ReadLine()) != null)
            {                
                string[] substrings = line.Split(delims);
                
                if (substrings[0] == "lat") // read in all latitudes
                {
                    numLats = substrings.Length - 1;
                    Array.Resize(ref lats, numLats);

                    for (int i = 0; i < numLats; i++)
                        lats[i] = Convert.ToDouble(substrings[i + 1]);
                }

                if (substrings[0] == "lon") // read in all longitudes
                {
                    numLongs = substrings.Length - 1;
                    Array.Resize(ref longs, numLongs);

                    for (int i = 0; i < numLongs; i++)
                        longs[i] = Convert.ToDouble(substrings[i + 1]);
                }
            }
            
            file.Close();

            // Get min/max required lat/long
            double minLat = 1000;
            double minLong = 1000;
            double maxLat = 0;
            double maxLong = -1000;

            for (int i = 0; i < nodesToPull.Length; i++)
            {
                if (nodesToPull[i].Coords.latitude < minLat)
                    minLat = nodesToPull[i].Coords.latitude;
                if (nodesToPull[i].Coords.longitude < minLong)
                    minLong = nodesToPull[i].Coords.longitude;
                if (nodesToPull[i].Coords.latitude > maxLat)
                    maxLat = nodesToPull[i].Coords.latitude;
                if (nodesToPull[i].Coords.longitude > maxLong)
                    maxLong = nodesToPull[i].Coords.longitude;
            }
            
            // Go through nodesToPull and assign Xind and Yind
            for (int i = 0; i < nodesToPull.Length; i++)
            {
                bool gotLat = false;
                bool gotLong = false;

                for (int latInd = 0; latInd < numLats; latInd++)
                {
                    if (lats[latInd] == nodesToPull[i].Coords.latitude)
                    {
                        nodesToPull[i].XY_ind.Lat = lats[latInd];
                        nodesToPull[i].XY_ind.X_ind = latInd;
                        gotLat = true;
                    }
                }

                for (int longInd = 0; longInd < numLongs; longInd++)
                { 
                    if (longs[longInd] == nodesToPull[i].Coords.longitude)
                    {
                        nodesToPull[i].XY_ind.Lon = longs[longInd];
                        nodesToPull[i].XY_ind.Y_ind = longInd;
                        gotLong = true;
                    }
                }

                if (gotLat == false || gotLong == false)
                {
                    MessageBox.Show("The MERRA data file does not contain all required lat/long nodes. Min Lat: " + minLat.ToString() + ", Max Lat: " + maxLat.ToString() +
                        ", Min Long: " + minLong.ToString() + ", Max Long: " + maxLong.ToString());                    
                    return gotAllIndices;
                }
            }

            gotAllIndices = true;

            return gotAllIndices;
        }



        /// <summary> Finds lat/long and MERRA2 datafile X/Y indices for MERRA2 node(s) closest to project site </summary>        
        public bool Find_MERRA_Coords(string MERRAfolder)
        {                        
            bool foundInds = false;

            // Grabbing filepaths for all MERRA data containing files
            string[] MERRAfiles = Directory.GetFiles(MERRAfolder, "*.ascii");
            string line;

            if (MERRAfiles == null)
            {
                MessageBox.Show("Check specified folderpath and try again.");
                return foundInds;
            }

            // Open one of the MERRA .ascii files and find the lat/lon closest TWO lats/lons to that of the input
            StreamReader file;

            try
            {
                file = new StreamReader(MERRAfiles[0]);
            }
            catch
            {
                MessageBox.Show("Error opening MERRA data file. Check that it's not open in another program.");
                return foundInds;
            }

            double[] lats = new double[0];
            double[] longs = new double[0];
            int numPerRC = 1; // Number of nodes per row/column
            numPerRC = (int)Math.Pow(numMERRA_Nodes, 0.5);

            while ((line = file.ReadLine()) != null)
            {
                char[] delims = { ',' };
                string[] substrings = line.Split(delims);
                        

                if (substrings[0] == "lat") // read in all latitudes
                {
                    Array.Resize(ref lats, substrings.Length - 1);

                    for (int i = 1; i < substrings.Length; i++)
                        lats[i - 1] = Convert.ToDouble(substrings[i]);                                            
                }

                if (substrings[0] == "lon") // read in all latitudes
                {
                    Array.Resize(ref longs, substrings.Length - 1);

                    for (int i = 1; i < substrings.Length; i++)
                        longs[i - 1] = Convert.ToDouble(substrings[i]);                                                               
                }
            }                    

            file.Close();

            if ((lats.Length < 4 || longs.Length < 4) & numPerRC == 4) // 16 MERRA nodes
            {                               
                MessageBox.Show("MERRA2 data range is too small. A minimum of four latitudes and four longitudes are needed if 16 MERRA nodes are used.", "Continuum 3");
                return foundInds;
            }
            else if (lats.Length < 2 || longs.Length < 2)
            {
                MessageBox.Show("MERRA2 data range is too small. A minimum of two latitudes and two longitudes are needed.", "Continuum 3");
                return foundInds;
            }

            // Figure out if the MERRA2 files have nodes covering the specified area
            double latReso = lats[1] - lats[0];
            double longReso = longs[1] - longs[0];            
            
          //  double minReqLat = interpData.Coords.Lat - (numPerRC / 2) * latReso;
            double minReqLat = lats[0];
            double minReqLong = longs[0];
            double maxReqLat = lats[lats.Length - 1];
            double maxReqLong = longs[longs.Length - 1];

            if (numMERRA_Nodes > 1)
            {
                if (lats.Length > (numPerRC / 2 - 1) && longs.Length > (numPerRC / 2 - 1))
                {
                    minReqLat = lats[numPerRC / 2 - 1]; 
                    minReqLong = longs[numPerRC / 2 - 1];
                    maxReqLat = lats[lats.Length - numPerRC / 2];
                    maxReqLong = longs[longs.Length - numPerRC / 2];
                }
                else
                {
                    MessageBox.Show("MERRA2 data range is not large enough to cover desired number of MERRA2 nodes.");
                    return foundInds;
                }                
            }                        

            if (interpData.Coords.latitude < minReqLat || interpData.Coords.latitude > maxReqLat)
            {
                MessageBox.Show("Outside of available MERRA2 data range. With " + numMERRA_Nodes + " MERRA2 nodes selected, the allowed latitude range is " + minReqLat + " to " + maxReqLat);
                return foundInds;
            }

            if (interpData.Coords.longitude < minReqLong || interpData.Coords.longitude > maxReqLong)
            {                
                MessageBox.Show("Outside of available MERRA2 data range. With " + numMERRA_Nodes + " MERRA2 nodes selected, the allowed longitude range is " + minReqLong + " to " + maxReqLong);
                return foundInds;
            }

            int startLatInd = 0;
            int startLongInd = 0;
            double lastDiff = 1000;

            if (numMERRA_Nodes == 1) // find lat and long closest to project site (as opposed to finding the lat/long that form a box around project site)
            {
                for (int i = 0; i < lats.Length; i++)
                {
                    double thisDiff = lats[i] - interpData.Coords.latitude;
                    if (Math.Abs(thisDiff) < lastDiff)
                    {
                        startLatInd = i;
                        lastDiff = Math.Abs(thisDiff);
                    }
                }

                lastDiff = 1000;
                for (int i = 0; i < longs.Length; i++)
                {
                    double thisDiff = longs[i] - interpData.Coords.longitude;
                    if (Math.Abs(thisDiff) < lastDiff)
                    {
                        startLongInd = i;
                        lastDiff = Math.Abs(thisDiff);
                    }
                }
            }
            else
            { // Find latitude that is closest but also west of desired latitude
                for (int i = 0; i < lats.Length; i++)
                {
                    double thisDiff = lats[i] - interpData.Coords.latitude;
                    if (thisDiff <= 0 && Math.Abs(thisDiff) < lastDiff)
                    {
                        startLatInd = i;
                        lastDiff = Math.Abs(thisDiff);
                    }
                }

                // Find longitude that is closest but south of desired location
                lastDiff = 1000;
                for (int i = 0; i < longs.Length; i++)
                {
                    double thisDiff = longs[i] - interpData.Coords.longitude;
                    if (thisDiff <= 0 && Math.Abs(thisDiff) < lastDiff)
                    {
                        startLongInd = i;
                        lastDiff = Math.Abs(thisDiff);
                    }
                }

                startLatInd = startLatInd - numPerRC / 2 + 1;
                startLongInd = startLongInd - numPerRC / 2 + 1;
            }
            

            for (int row = 0; row < numPerRC; row++)
                for (int col = 0; col < numPerRC; col++)
                {
                    int MERRA_ind = row * numPerRC + col ;
                    MERRA_Nodes[MERRA_ind].XY_ind.X_ind = startLatInd + col;
                    MERRA_Nodes[MERRA_ind].XY_ind.Lat = lats[0] + (startLatInd + col) * latReso;
                    MERRA_Nodes[MERRA_ind].XY_ind.Y_ind = startLongInd + row;
                    MERRA_Nodes[MERRA_ind].XY_ind.Lon = longs[0] + (startLongInd + row) * longReso;
                }

            foundInds = true;

            return foundInds;
        }
                        
            

        public int Get_Num_Days_in_Month(int Month_1_to_12, int thisYear)
        {
            int numDays = 31;

            if (Month_1_to_12 == 4 || Month_1_to_12 == 6 || Month_1_to_12 == 9 || Month_1_to_12 == 11)
                numDays = 30;
            else if (Month_1_to_12 == 2 && thisYear % 4 == 0) // leap year
                numDays = 29;
            else if (Month_1_to_12 == 2 && thisYear % 4 != 0) // not a leap year
                numDays = 28;
                           
            return numDays;
        }

        public void AddDataToDBAndClear(Continuum thisInst)
        {
            // Adds MERRA2 data to database (if it doesn't already exist) and clears MERRA2.TS_Data
            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);
                        
            using (var context = new Continuum_EDMContainer(connString))
            {               
                for (int i = 0; i < numMERRA_Nodes; i++)
                {
                    double thisLat = MERRA_Nodes[i].XY_ind.Lat;
                    double thisLong = MERRA_Nodes[i].XY_ind.Lon;

                    var thisMERRAData = from N in context.MERRA_Node_table where N.latitude == thisLat && N.longitude == thisLong select N;
                    if (thisMERRAData.Count() == 0)
                    {
                        MERRA_Node_table merraNodeTable = new MERRA_Node_table();
                        merraNodeTable.latitude = thisLat;
                        merraNodeTable.longitude = thisLong;

                        MemoryStream MS1 = new MemoryStream();
                        bin.Serialize(MS1, MERRA_Nodes[i].TS_Data);
                        merraNodeTable.merraData = MS1.ToArray();

                        try
                        {
                            context.MERRA_Node_table.Add(merraNodeTable);
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.InnerException.ToString());                            
                            return;
                        }
                    }

                    MERRA_Nodes[i].TS_Data = null;
                }
                context.Database.Connection.Close();
            }
        }

        public void AddNewDataToDB(Continuum thisInst, MERRA_Pull[] newDataToAdd)
        {
            // Adds new MERRA2 data to database 
            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            if (newDataToAdd == null)
                return;

            int numNewData = newDataToAdd.Length;

            using (var context = new Continuum_EDMContainer(connString))
            {
                for (int i = 0; i < numNewData; i++)
                { 
                    MERRA_Node_table merraNodeTable = new MERRA_Node_table();
                    merraNodeTable.latitude = newDataToAdd[i].Coords.latitude;
                    merraNodeTable.longitude = newDataToAdd[i].Coords.longitude;

                    MemoryStream MS1 = new MemoryStream();
                    bin.Serialize(MS1, newDataToAdd[i].Data);
                    merraNodeTable.merraData = MS1.ToArray();

                    try
                    {
                        context.MERRA_Node_table.Add(merraNodeTable);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());                        
                        return;
                    }                    
                    
                }

                context.Database.Connection.Close();
            }
        }

        public void GetMERRADataFromDB(Continuum thisInst)
        {
            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);
                      
            using (var context = new Continuum_EDMContainer(connString))
            {
                double thisLat = interpData.Coords.latitude;
                double thisLong = interpData.Coords.longitude;                      

                for (int i = 0; i < numMERRA_Nodes; i++)
                {
                    thisLat = MERRA_Nodes[i].XY_ind.Lat;
                    thisLong = MERRA_Nodes[i].XY_ind.Lon;

                    var thisMERRAData = from N in context.MERRA_Node_table where N.latitude == thisLat && N.longitude == thisLong select N;

                    foreach (var N in thisMERRAData)
                    {
                        MemoryStream MS = new MemoryStream(N.merraData);
                        MERRA_Nodes[i].TS_Data = (Wind_TS[])bin.Deserialize(MS);
                        MS.Close();
                    }
                }

                context.Database.Connection.Close();
            }
            
        }

        /// <summary> Generates interpData time series dataset which contains MERRA2 interpolated temperature, pressure, and wind speed data </summary>        
        public void GetInterpData(UTM_conversion utm)
        {
            // interpData not saved in file or DB, only MERRA2 data saved in DB and interpData created as needed)
            if (MERRA_Nodes.Length == 0)
                return;

            if (MERRA_Nodes[0].TS_Data == null)
                return;

            int MERRA_Length = MERRA_Nodes[0].TS_Data.Length;
            Array.Resize(ref interpData.TS_Data, MERRA_Length);

            TopoInfo topo = new TopoInfo(); // created to use "CalcDistanceBetweenTwoPoints" function
            
            for (int i = 0; i < MERRA_Length; i++)
            {
                double sumDist = 0; // sum of inverse distance weights
                double avgU = 0; // average U-component (east-west) of WS. Positive is west-to-east -->
                double avgV = 0; // average V-component (north-south) of WS. Positive is south-to-north ^
                double avgTemp = 0; // average temperature
                double avgSurfPress = 0; // average surface level pressure
                double avgSeaLevelPress = 0; // average sea level pressure

                interpData.TS_Data[i].ThisDate = MERRA_Nodes[0].TS_Data[i].ThisDate;

                for (int n = 0; n < numMERRA_Nodes; n++)
                {
                    UTM_conversion.UTM_coords theseUTM = utm.LLtoUTM(MERRA_Nodes[n].XY_ind.Lat, MERRA_Nodes[n].XY_ind.Lon);
                    double thisDist = topo.CalcDistanceBetweenPoints(interpData.UTM.UTMEasting, interpData.UTM.UTMNorthing, theseUTM.UTMEasting, theseUTM.UTMNorthing);
                    if (thisDist == 0)
                        thisDist = 0.1; // so it doesn't throw divide by zero error

                    sumDist = sumDist + 1 / thisDist;

                    double thisU = -MERRA_Nodes[n].TS_Data[i].WS50m * Math.Cos(Math.PI / 180 * (90 - MERRA_Nodes[n].TS_Data[i].WD50m)); // negative sign is there since WD is the direction 
                    double thisV = -MERRA_Nodes[n].TS_Data[i].WS50m * Math.Sin(Math.PI / 180 * (90 - MERRA_Nodes[n].TS_Data[i].WD50m)); // that wind is coming from.

                    avgU = avgU + thisU / thisDist;
                    avgV = avgV + thisV / thisDist;
                    avgTemp = avgTemp + MERRA_Nodes[n].TS_Data[i].Temp10m / thisDist;
                    avgSurfPress = avgSurfPress + MERRA_Nodes[n].TS_Data[i].SurfPress / thisDist;
                    avgSeaLevelPress = avgSeaLevelPress + MERRA_Nodes[n].TS_Data[i].SeaPress / thisDist;
                }

                if (sumDist > 0)
                {
                    avgU = avgU / sumDist;
                    avgV = avgV / sumDist;
                    interpData.TS_Data[i].WS50m = Math.Sqrt(avgU * avgU + avgV * avgV);
                    interpData.TS_Data[i].WD50m = 180 + 180 / Math.PI * Math.Atan2(avgU, avgV);
                    interpData.TS_Data[i].Temp10m = avgTemp / sumDist;
                    interpData.TS_Data[i].SeaPress = avgSeaLevelPress / sumDist;
                    interpData.TS_Data[i].SurfPress = avgSurfPress / sumDist;
                }
            }

            if (powerCurve.name != null)
                ApplyPC(ref interpData.TS_Data);

        }

        /// <summary>
        /// Finds and returns maximum hourly wind speed for each year of long-term data (used in extreme WS calcs)
        /// </summary>
        /// <returns></returns>
        public Met.MaxYearlyWind[] GetMaxHourlyWindSpeeds()
        {            
            int refLength = interpData.TS_Data.Length;
            int firstYear = interpData.TS_Data[0].ThisDate.Year;
            int lastYear = interpData.TS_Data[refLength - 1].ThisDate.Year;
            int numYears = lastYear - firstYear + 1;
            Met.MaxYearlyWind[] maxHourlyRef = new Met.MaxYearlyWind[numYears];

            double thisMax = 0;
            lastYear = firstYear;
            int yearInd = 0;
            int thisYear = 0;

            for (int i = 0; i < refLength; i++)
            {
                thisYear = interpData.TS_Data[i].ThisDate.Year;
                if (thisYear == lastYear)
                {
                    if (interpData.TS_Data[i].WS50m > thisMax)
                        thisMax = interpData.TS_Data[i].WS50m;
                }
                else
                {
                    maxHourlyRef[yearInd].maxWS = thisMax;
                    maxHourlyRef[yearInd].thisYear = lastYear;
                    thisMax = interpData.TS_Data[i].WS50m;
                    yearInd++;
                    lastYear = thisYear;
                }
            }

            if (yearInd < numYears && interpData.TS_Data[refLength - 1].ThisDate.Month == 12 && interpData.TS_Data[refLength - 1].ThisDate.Day == 31)
            {
                maxHourlyRef[yearInd].maxWS = thisMax;
                maxHourlyRef[yearInd].thisYear = lastYear;
            }
            

            return maxHourlyRef;
        }

        public bool WasDataFullyLoaded(MERRA_Pull[] merraData, string selectedFolder)
        {
            bool fullyLoaded = true;
            TimeSpan timeSpan;
            DateTime thisTime;
            DateTime lastTime = DateTime.Now;

            for (int i = 0; i < merraData.Length; i++)
            {
                if (merraData[i].Data.Length > 0)
                    lastTime = merraData[i].Data[0].ThisDate;

                for (int j = 0; j < merraData[i].Data.Length; j++)
                {
                    thisTime = merraData[i].Data[j].ThisDate;
                    timeSpan = thisTime.Subtract(lastTime);

                    if (timeSpan.TotalMinutes > 60) // one hour
                    {
                        MessageBox.Show("Missing MERRA data. Start of missing interval: " + thisTime.ToString());
                        return fullyLoaded = false;
                    }

                    if (merraData[i].Data[j].WS50m == 0 && merraData[i].Data[j].WD50m == 0)
                    {
                        MessageBox.Show("MERRA2 data was not fully loaded. Missing data: " + lastTime.ToString());
                        return fullyLoaded = false;
                    }

                    lastTime = thisTime;
                }
            }


            return fullyLoaded;
        }
        

    }

}
