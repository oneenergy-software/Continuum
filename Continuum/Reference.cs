using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Research.Science.Data.Imperative;
using Microsoft.Research.Science.Data;
using System.Net;
using Microsoft.VisualBasic;
using System.Windows.Media.Media3D;
using System.Runtime.CompilerServices;

namespace ContinuumNS
{
    /// <summary> Class that contains reanalysis data (ERA5 or MERRA2) nodes and interpolated at specified latitude/longitude (if more than 1 node is used). 
    /// It contains functions that extract data from locally-downloaded datafiles and calculate average wind speed and wind direction from UV data and monthly/yearly energy production statistics.</summary>

    [Serializable()]
    public class Reference
    {
      //  /// <summary> Reference data type: ERA5 or MERRA2. </summary>
      //  public string referenceType; // this is stored in RefDataDownload
        
        /// <summary> Start of reference dataset in local UTC time. </summary>
        public DateTime startDate = new DateTime(1989, 1, 1, 0, 0, 0);
        /// <summary> End of reference dataset in local UTC time. </summary>
        public DateTime endDate = new DateTime(2018, 12, 31, 23, 0, 0);
        /// <summary> Reference data download object (contains folder location, start/end, coords). </summary>
        public ReferenceCollection.RefDataDownload refDataDownload;
   //     /// <summary> Renalysis data provider username (NASA EarthData). This is now stored in ReferenceCollection.RefDataDownload objects </summary>
   //     public string earthdataUser = "";
   //     /// <summary> Renalysis data provider password. This is now stored in ReferenceCollection.RefDataDownload objects </summary>
   //     public string earthdataPwd = "";

        /// <summary> Site location coordinates, interpolated reference time series data (WS, WD, pressure, and temp), and calculated monthly and annual energy production. </summary>
        public Wind_Data_and_Prod_Stats interpData;
        /// <summary> List of reference node data. Each entry contains time series data, node coordinates, and X/Y datafile indices. User selects either 1, 4, or 16 nodes. </summary>
        public Node_Data[] nodes = new Node_Data[0];
        /// <summary> True if not associated with a loaded met site. </summary>
        public bool isUserDefined;
        /// <summary> Power curve used to calculate energy production. </summary>
        public TurbineCollection.PowerCurve powerCurve;
        /// <summary> Scaling Factor applied to WS. Default = 0.85. </summary>
        public double WS_ScaleFactor = 0.85;

        /// <summary> Number of nodes. Options are 1, 4, 16. Default is 1. </summary>
        public int numNodes;
        /// <summary> Height of WS and WD estimates </summary>
        public double wswdH;
        /// <summary> Height of Temperature estimates </summary>
        public double temperatureH;
   //     /// <summary> Latitude resolution in degs </summary> // Moved this to ReferenceCollection.GetLatRes(RefDataDownload)
   //     public double latRes = 0.5;
   //     /// <summary> Longitude resolution in degs </summary>
   //     public double lonRes = 0.625;                

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>   Struct containing date, WS, WD and energy production for each entry. </summary>
        [Serializable()]
        public struct Wind_Data_and_Prod_Stats
        {
            /// <summary> Wind speed, wind direction, and energy production time series data. </summary>
            public Wind_TS_with_Prod[] TS_Data;
            /// <summary> Monthly energy production by year and long-term average monthly energy production. </summary>
            public MonthlyProdByYearAndLTAvg[] monthlyProd;
            /// <summary> Yearly energy production and long-term average yearly energy production. </summary>
            public YearlyProdAndLTAvg annualProd;
            /// <summary> Latitude and longitude of site. </summary>
            public UTM_conversion.Lat_Long Coords;
            /// <summary> UTM coordinates of site. </summary>
            public UTM_conversion.UTM_coords UTM;
            /// <summary> UTM hour offset. </summary>
            public int UTC_offset;

            /// <summary> Data type conversion for structs created in MERRA class. this() lets the base ValueTye class initialize all the fields </summary>            
            public Wind_Data_and_Prod_Stats(MERRA.Wind_Data_and_Prod_Stats merraObj) : this()
            {
                if (TS_Data != null)
                {
                    for (int t = 0; t < this.TS_Data.Length; t++)
                        this.TS_Data[t] = (Wind_TS_with_Prod)merraObj.TS_Data[t];
                }

                if (monthlyProd != null)
                {
                    for (int m = 0; m < this.monthlyProd.Length; m++)
                        this.monthlyProd[m] = (MonthlyProdByYearAndLTAvg)merraObj.Monthly_Prod[m];
                }

                this.annualProd = (YearlyProdAndLTAvg)merraObj.Annual_Prod;
                this.Coords = merraObj.Coords;
                this.UTM = merraObj.UTM;
            }

            public static explicit operator Wind_Data_and_Prod_Stats(MERRA.Wind_Data_and_Prod_Stats merraObj) => new Wind_Data_and_Prod_Stats(merraObj);
        }

  /*      /// <summary> Contains time stamp, wind speed, direction, pressure, and temperature data </summary>
        [Serializable()]
        public struct Wind_TS
        {
            /// <summary> Timestamp </summary>
            public DateTime ThisDate;
            /// <summary> Wind speed at wswdH </summary>
            public double WS;
            /// <summary> Wind direction at wswdH  </summary>
            public double WD;
            /// <summary> Surface level pressure </summary>
            public double surfPress;
            /// <summary> Sea-level pressure </summary>
            public double seaPress;
            /// <summary> Temperature at temperatureH </summary>
            public double temperature;
        }
  */
        /// <summary> Contains time stamp, wind speed, direction, pressure, temperature, and energy production data </summary>
        [Serializable()]
        public struct Wind_TS_with_Prod
        {
            /// <summary> Timestamp </summary>
            public DateTime thisDate;
            /// <summary> Wind speed at wswdH </summary>
            public double WS;
            /// <summary> Wind direction at wswdH  </summary>
            public double WD;
            /// <summary> Energy production </summary>
            public double prod;
            /// <summary> Surface level pressure </summary>
            public double surfPress;
            /// <summary> Sea-level pressure </summary>
            public double seaPress;
            /// <summary> Temperature at temperatureH  </summary>
            public double temperature;

            /// <summary> Data type conversion for structs created in MERRA class. this() lets the base ValueTye class initialize all the fields </summary> 
            public Wind_TS_with_Prod(MERRA.Wind_TS_with_Prod merraObj) : this()
            {
                this.thisDate = merraObj.ThisDate;
                this.WS = merraObj.WS50m;
                this.WD = merraObj.WD50m;
                this.prod = merraObj.Prod;
                this.seaPress = merraObj.SeaPress;
                this.surfPress = merraObj.SurfPress;
                this.temperature = merraObj.Temp10m;
            }

            /// <summary> Data conversion operator from MERRA2 object </summary>            
            public static explicit operator Wind_TS_with_Prod(MERRA.Wind_TS_with_Prod merraObj) => new Wind_TS_with_Prod(merraObj);
        }

        /// <summary> Contains WS, WD, temperature, and pressure time series data and lat/long and corresponding datafile X/Y indices </summary>
        [Serializable()]
        public struct Node_Data
        {
            /// <summary> WS, WD, temperature, and pressure time series data </summary>
            public Wind_TS_with_Prod[] TS_Data;
            /// <summary> Lat/long and corresponding datafile X/Y indices </summary>
            public XYIndices XY_ind;
        }

        /// <summary> Holds coordinates, UTM coordinates, MERRA2 file indices and time series data of MERRA2 node </summary>
        public struct RefData_Pull
        {
            /// <summary> Lat/Long of node </summary>
            public UTM_conversion.Lat_Long Coords;
            /// <summary> UTM coords of node </summary>
            public UTM_conversion.UTM_coords UTM;
            /// <summary> Datafile X and Y indices </summary>
            public XYIndices XY_ind;
            /// <summary> Time series data </summary>
            public Wind_TS_with_Prod[] Data;
        }

        /// <summary>   Contains the Lat/Long and X/Y index (i.e. in reference datafile) of nodes. </summary>
        [Serializable()]
        public struct XYIndices
        {
            /// <summary> Datafile X index </summary>
            public int X_ind;
            /// <summary> Latitude </summary>
            public double Lat;
            /// <summary> Datafile Y index </summary>
            public int Y_ind;
            /// <summary> Longitude </summary>
            public double Lon;
        }

        /// <summary>   Contains the specified month, the long-term average monthly energy production and an array containing the monthly energy production for every year. </summary>        
        [Serializable()]
        public struct MonthlyProdByYearAndLTAvg
        {
            /// <summary> Month. </summary>
            public int Month;
            /// <summary> List of monthly energy production by year (i.e. if month is January, list would contain energy production in January for every year) </summary>
            public YearAndProd[] YearProd;
            /// <summary>   Long-term average monthly energy production. </summary>
            public double LT_Avg;

            /// <summary> Data type conversion for structs created in MERRA class. this() lets the base ValueTye class initialize all the fields </summary> 
            public MonthlyProdByYearAndLTAvg(MERRA.MonthlyProdByYearAndLTAvg merraObj) : this()
            {
                this.Month = merraObj.Month;

                for (int i = 0; i < YearProd.Length; i++)
                    this.YearProd[i] = (YearAndProd)merraObj.YearProd[i];
                
                this.LT_Avg = merraObj.LT_Avg;                
            }

            /// <summary> Data conversion operator from MERRA2 object </summary>            
            public static explicit operator MonthlyProdByYearAndLTAvg(MERRA.MonthlyProdByYearAndLTAvg merraObj) => new MonthlyProdByYearAndLTAvg(merraObj);
        }

        /// <summary> Contains yearly energy production for each year and long-term AEP</summary>
        [Serializable()]
        public struct YearlyProdAndLTAvg
        {
            /// <summary>   Annual energy production by year. </summary>
            public YearAndProd[] Yearly_Prod;
            /// <summary>   Long-term average annual energy production  </summary>
            public double LT_Avg;

            /// <summary> Data type conversion for structs created in MERRA class. this() lets the base ValueTye class initialize all the fields </summary> 
            public YearlyProdAndLTAvg(MERRA.YearlyProdAndLTAvg merraObj) : this()
            {
                if (Yearly_Prod != null)
                {
                    for (int i = 0; i < Yearly_Prod.Length; i++)
                        this.Yearly_Prod[i] = (YearAndProd)merraObj.Yearly_Prod[i];
                }

                this.LT_Avg = merraObj.LT_Avg;
            }

            /// <summary> Data conversion operator from MERRA2 object </summary>            
            public static explicit operator YearlyProdAndLTAvg(MERRA.YearlyProdAndLTAvg merraObj) => new YearlyProdAndLTAvg(merraObj);
        }


        /// <summary>  Contains year and energy production. </summary>        
        [Serializable()]
        public struct YearAndProd
        {
            /// <summary>  Energy Production Year </summary>
            public int year;
            /// <summary>  Energy Production</summary>
            public double prod;

            /// <summary> Data type conversion for structs created in MERRA class. this() lets the base ValueTye class initialize all the fields </summary> 
            public YearAndProd(MERRA.YearAndProd merraObj) : this()
            {
                this.year = merraObj.year;
                this.prod = merraObj.prod;
            }

            /// <summary> Data conversion operator from MERRA2 object </summary>            
            public static explicit operator YearAndProd(MERRA.YearAndProd merraObj) => new YearAndProd(merraObj);
        }

        /// <summary> Holds hourly timestamps and east/north (i.e. U/V) wind speeds for 24 hour period (i.e. 1 day) </summary>
        public struct East_North_WSs
        {
            /// <summary> Hourly timestamps (24 hours) </summary>
            public DateTime[] timeStamp;
            /// <summary> East-West wind speed </summary>
            public double[] U_WS;
            /// <summary> North-South wind speed </summary>
            public double[] V_WS;            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Returns the name of Long-Term reference site (used in dropdown lists) </summary>
        public string GetName(MetCollection metList, UTM_conversion utmConv)
        {
            string refName = "";
            
            if (isUserDefined)
                refName = refDataDownload.refType + " Lat: " + interpData.Coords.latitude.ToString() + ", Long: " + interpData.Coords.longitude.ToString() +
                " , Num Nodes: " + numNodes.ToString();
            else
            {
                Met thisMet = metList.GetMetAtLatLon(interpData.Coords.latitude, interpData.Coords.longitude, utmConv);
                refName = refDataDownload.refType + " interp. at " + thisMet.name + " , Num Nodes: " + numNodes.ToString();
            }                

            return refName;            
        }               

        /// <summary> Clears all calculated data and site location coordinates. </summary>
        public void ClearInterpData()
        {
            interpData.TS_Data = new Wind_TS_with_Prod[0];
            interpData.monthlyProd = null;
            interpData.annualProd.Yearly_Prod = null;
            interpData.annualProd.LT_Avg = 0;
            interpData.Coords.latitude = 0;
            interpData.Coords.longitude = 0;
            interpData.UTM.UTMEasting = 0;
            interpData.UTM.UTMNorthing = 0;
            interpData.UTC_offset = 0;

        }

        /// <summary> Clears all node data </summary>
        public void Clear_Node_Data()
        {
            if (nodes.Length > 0)
                for (int i = 0; i < nodes.Length; i++)
                    nodes[i].TS_Data = new Wind_TS_with_Prod[0];
        }               

        /// <summary> Clears all node data and all calculated data and site locations. </summary>
        public void ClearAll()
        {
            ClearInterpData();
            Clear_Node_Data();
        }

        /// <summary> Resets monthly energy production data. </summary>
        public void Reset_MonthProdStats()
        {
            if (interpData.monthlyProd == null)
                Array.Resize(ref interpData.monthlyProd, 12);

            Array.Clear(interpData.monthlyProd, 0, interpData.monthlyProd.Length);

            for (int i = 0; i < 12; i++)
            {
                Array.Resize(ref interpData.monthlyProd[i].YearProd, 0);
                Array.Clear(interpData.monthlyProd[i].YearProd, 0, interpData.monthlyProd[i].YearProd.Length);
                interpData.monthlyProd[i].Month = i + 1;
            }

        }

        /// <summary> Resets annual energy production data. </summary>
        public void Reset_AnnualProdStats()
        {
            interpData.annualProd.LT_Avg = 0;
            interpData.annualProd.Yearly_Prod = null;
        }

        /// <summary> Returns true if monthly energy production has been calculated. </summary>
        public bool GotMonthlyProd()
        {
            bool gotIt = false;

            if (interpData.monthlyProd != null)
                gotIt = true;

            return gotIt;
        }

        /// <summary> Returns true if have interpolated MERRA2 time series data. </summary>
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

        /// <summary> Resizes the referenced array to new size. </summary>
        public void Size_East_North_WS_Data(ref East_North_WSs[] theseUVs, int New_Size)
        {
            if (theseUVs == null)
                return;

            for (int i = 0; i < theseUVs.Length; i++)
            {
                Array.Resize(ref theseUVs[i].U_WS, New_Size);
                Array.Resize(ref theseUVs[i].V_WS, New_Size);                
            }
        }

        /// <summary> Returns true if MERRA2 datafile parameter is needed (i.e. wind speed, temperature, or pressure). </summary>
        public bool Need_This_Param(string thisString)
        {
            bool Need_it = false;
            int String_Len = thisString.Length;

            if (refDataDownload.refType == "MERRA2")
            {
                if (String_Len >= 11)
                    if (thisString.Substring(0, 11) == "PRECTOTCORR") // Bias-corrected precipitation
                        Need_it = false;

                if (String_Len >= 8)
                    if (thisString.Substring(0, 8) == "PRECTOT[") // Precipitation
                        Need_it = false;

                if (String_Len >= 9)
                    if (thisString.Substring(0, 9) == "MDSOPTHCK") // Optical Thickness Total Mean))
                        Need_it = false; // Optical Thickness Total Mean))

                if (String_Len >= 8)
                    if (thisString.Substring(0, 8) == "ISCCPCLD") // Total Cloud Area Fraction )
                        Need_it = false; // ISCCP Total Cloud Area Fraction

                if (String_Len >= 6)
                    if (thisString.Substring(0, 6) == "MDSCLD") // Cloud Fraction Total mean))
                        Need_it = false; // MODIS Cloud Fraction Total mean))

                if (String_Len >= 3)
                    if (thisString.Substring(0, 3) == "U50" || thisString.Substring(0, 3) == "V50" ||
                    thisString.Substring(0, 3) == "SLP" || thisString.Substring(0, 3) == "T10")
                        Need_it = true;

                if (String_Len >= 2)
                    if (thisString.Substring(0, 2) == "PS")
                        Need_it = true;
            }

            return Need_it;
        }

        /// <summary> Sets site location (lat/long and UTM coordinates) and UTC offset. </summary>
        public void Set_Interp_LatLon_Dates_Offset(double latitude, double longitude, int offset, UTM_conversion utmConv)
        {
            interpData.Coords.latitude = Math.Round(latitude, 3);
            interpData.Coords.longitude = Math.Round(longitude, 3);
            interpData.UTC_offset = offset;

            if (utmConv.savedDatumIndex == 100)
                utmConv.savedDatumIndex = 0;

            if (utmConv.hemisphere == "")
            {
                if (latitude > 0)
                    utmConv.hemisphere = "Northern";
                else
                    utmConv.hemisphere = "Southern";
            }

            interpData.UTM = utmConv.LLtoUTM(interpData.Coords.latitude, interpData.Coords.longitude);

        }

        /// <summary> Returns string containing specified timestamp </summary>
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

        /// <summary> Returns true if dataset contains full year for specified year. </summary>
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
                if (thisTS[i].thisDate == startDate)
                    haveStart = true;

                if (thisTS[i].thisDate == endDate)
                    haveEnd = true;

                if (haveStart == true && haveEnd == true)
                {
                    isFull = true;
                    break;
                }
            }

            return isFull;
        }

        /// <summary> Returns true if dataset contains full month for specified month. </summary>
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
                if (thisTS[i].thisDate == startDate)
                    haveStart = true;

                if (thisTS[i].thisDate == endDate)
                    haveEnd = true;

                if (haveStart == true && haveEnd == true)
                {
                    isFull = true;
                    break;
                }
            }

            return isFull;
        }

        /// <summary> Returns either long-term or specified yearly or monthly energy production. For long-term estimates, thisMonth/thisYear set to 100. </summary>
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

        /// <summary> Calculates and returns either long-term or specified yearly or monthly capacity factor. For long-term estimates, thisMonth/thisYear set to 100. </summary>
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

        /// <summary> Returns index of specified year. </summary>
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

        /// <summary> Returns index of specified year from monthly energy production data. </summary>
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

        /// <summary> Returns indices of specified month and year from monthly energy production data. monthYearInd[0] = Month index; monthYearInd[1] = Year index </summary>
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

        /// <summary> Calculates energy production for referenced time series. </summary>
        public void ApplyPC(ref Wind_TS_with_Prod[] Wind)
        {
            if (Wind == null)
                return;

            for (int i = 0; i < Wind.Length; i++)
            {
                if (Wind[i].WS >= 0)
                {
                    double Scaled_WS = Wind[i].WS * WS_ScaleFactor;
                    TurbineCollection turbList = new TurbineCollection();
                    Wind[i].prod = turbList.GetInterpPowerOrThrust(Scaled_WS, powerCurve, "Power");
                }
                else
                    Wind[i].prod = 0;
            }
        }

        /// <summary> Calculates the annual energy production (AEP) and long-term AEP and saves to referenced YearlyProdAndLTAvg object. </summary>
        public void CalcAnnualProd(ref YearlyProdAndLTAvg thisAnnual, MonthlyProdByYearAndLTAvg[] thisMonthly, UTM_conversion utm)
        {
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

        /// <summary> Calculates and returns the average or long-term monthly or yearly MERRA2 data parameter ("50 m WS", "Energy prod.", "Surface Pressure", "Sea Level Pressure", "10 m Temp") </summary>
        public double Calc_Avg_or_LT(Wind_TS_with_Prod[] thisTS, int thisMonth, int thisYear, string param)
        {
            double avgOrLT = 0;
            int count = 0;

            for (int i = 0; i < thisTS.Length; i++)
            {
                if ((thisMonth == 100 || thisTS[i].thisDate.Month == thisMonth) &&
                    (thisYear == 100 || thisTS[i].thisDate.Year == thisYear))
                {
                    if (param == "50 m WS" || param == "100 m WS")
                        avgOrLT = avgOrLT + thisTS[i].WS;
                    else if (param == "Energy prod.")
                        avgOrLT = avgOrLT + thisTS[i].prod;
                    else if (param == "Surface Pressure")
                        avgOrLT = avgOrLT + thisTS[i].surfPress / 1000;
                    else if (param == "Sea Level Pressure")
                        avgOrLT = avgOrLT + thisTS[i].seaPress / 1000;
                    else if (param == "10 m Temp")
                        avgOrLT = avgOrLT + thisTS[i].temperature - 273.15;

                    count++;
                }

            }

            if (count > 0)
                avgOrLT = avgOrLT / count;

            return avgOrLT;

        }

        /// <summary> Calculates and returns the average wind rose or energy rose for specified month and year. For long-term wind rose, thisMonth and thisYear = 100. </summary>        
        public double[] CalcWindOrEnergyRose(int thisMonth, int thisYear, UTM_conversion utm, int numWD, string windOrEnergy, double airDens, double rotorDiam)
        {            
            double[] windRose = new double[numWD];
            double allCount = 0;

            if (GotWindTS(utm) == false)
                return windRose;

            for (int i = 0; i < interpData.TS_Data.Length; i++)
            {
                if ((thisMonth == 100 || interpData.TS_Data[i].thisDate.Month == thisMonth) &&
                    (thisYear == 100 || interpData.TS_Data[i].thisDate.Year == thisYear))
                {
                    int WD_ind = (int)Math.Round(interpData.TS_Data[i].WD / (360 / (double)numWD), 0, MidpointRounding.AwayFromZero);
                    if (WD_ind == numWD) WD_ind = 0;

                    if (windOrEnergy == "Wind Rose")
                    {
                        windRose[WD_ind]++;
                        allCount++;
                    }
                    else if (windOrEnergy == "Energy Rose")
                    {
                        double thisPower = 0.5 * airDens * Math.PI * Math.Pow(rotorDiam / 2, 2) * Math.Pow(interpData.TS_Data[i].WS, 3) / 1000; // Power in wind
                        windRose[WD_ind] = windRose[WD_ind] + thisPower;
                        allCount = allCount + thisPower;
                    }
                }
            }

            if (allCount > 0)
                for (int i = 0; i < numWD; i++)
                    windRose[i] = windRose[i] / allCount;

            return windRose;
        }

        /// <summary> Calculates the long-term average energy production for referenced YearlyProdAndLTAvg object. </summary>        
        public void Calc_LT_Avg_Prod(ref YearlyProdAndLTAvg thisAnnual)
        {
            double avgLT = 0.0;
            int numYears = 0;
            if (thisAnnual.Yearly_Prod != null) numYears = thisAnnual.Yearly_Prod.Length;

            for (int i = 0; i < numYears; i++)
                avgLT = avgLT + thisAnnual.Yearly_Prod[i].prod;

            if (numYears > 0) thisAnnual.LT_Avg = avgLT / numYears;

        }

        /// <summary> Calculates the wind speed and wind direction based on U and V wind speeds for each node in thisRefData_Pull and for each time stamp. </summary>        
        public void Calc_WS_WD(ref RefData_Pull[] thisRefData_Pull, East_North_WSs[] theseUVs)
        {
            if (thisRefData_Pull == null || theseUVs == null)
                return;

            int numNodes = thisRefData_Pull.Length;
            int numHours = thisRefData_Pull[0].Data.Length;

            for (int i = 0; i < numNodes; i++)
                for (int j = 0; j < numHours; j++)
                {
                    if (theseUVs[i].U_WS[j] != 0 && theseUVs[i].V_WS[j] != 0)
                    {
                        thisRefData_Pull[i].Data[j].WS = Math.Pow((Math.Pow(theseUVs[i].U_WS[j], 2) + Math.Pow(theseUVs[i].V_WS[j], 2)), 0.5);
                        thisRefData_Pull[i].Data[j].WD = Math.Atan2(theseUVs[i].V_WS[j], theseUVs[i].U_WS[j]) * (180 / Math.PI);
                        thisRefData_Pull[i].Data[j].WD = 270 - thisRefData_Pull[i].Data[j].WD; // this moves WD=0 to north and flips WD to be from wind direction
                        if (thisRefData_Pull[i].Data[j].WD > 360) thisRefData_Pull[i].Data[j].WD = thisRefData_Pull[i].Data[j].WD - 360;
                    }
                }
        }

        /// <summary> Calculates the monthly energy production on a yearly basis and the average long-term monthly energy production </summary>
        public void Calc_MonthProdStats(UTM_conversion utm)
        {
            Reset_MonthProdStats();

            if (GotWindTS(utm) == false) return;

            int startYear = interpData.TS_Data[0].thisDate.Year;
            int endYear = interpData.TS_Data[interpData.TS_Data.Length - 1].thisDate.Year;
            int numYears = endYear - startYear + 1;

            // Initialize arrays and populate years
            for (int i = 0; i < 12; i++)
            {
                interpData.monthlyProd[i].YearProd = new YearAndProd[numYears];

                for (int j = 0; j < numYears; j++)
                    interpData.monthlyProd[i].YearProd[j].year = startYear + j;
            }

            // Go through all data and fill in MonthProdStats monthly energy production for every year            
            for (int i = 0; i < interpData.TS_Data.Length; i++)
            {
                int[] monthYearInd = Get_Month_Year_Inds(interpData.monthlyProd, interpData.TS_Data[i].thisDate.Month, interpData.TS_Data[i].thisDate.Year);
                interpData.monthlyProd[monthYearInd[0]].YearProd[monthYearInd[1]].prod += interpData.TS_Data[i].prod;
            }

            // Calculate average monthly energy production           
            for (int i = 0; i < interpData.monthlyProd.Length; i++)
            {
                int yearCount = 0;

                for (int j = 0; j < interpData.monthlyProd[0].YearProd.Length; j++)
                {
                    if (interpData.monthlyProd[i].YearProd[j].prod > 0)
                    {
                        interpData.monthlyProd[i].LT_Avg = interpData.monthlyProd[i].LT_Avg + interpData.monthlyProd[i].YearProd[j].prod;
                        yearCount++;
                    }
                }

                interpData.monthlyProd[i].LT_Avg = interpData.monthlyProd[i].LT_Avg / yearCount;
            }

        }

        /// <summary> Calculates and returns percent differnce of energy production calculated in specified year from long-term average monthly production. </summary>
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

        /// <summary> Calculates and returns percent differnce of energy production calculated in specified year from long-term average yearly production. </summary>
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

        /// <summary> Calculates and returns deviation of specified year/month from long-term value. </summary>
        public double Calc_Dev_from_LT(MonthlyProdByYearAndLTAvg[] thisMonthly, YearlyProdAndLTAvg thisAnnual, int thisYear, int thisMonth)
        {
            double devFromLT = 0;

            if (thisMonth == 100 && thisYear != 100) // deviation from long-term annual
            {
                int yearInd = Get_Year_Ind(thisYear, thisAnnual);
                devFromLT = (thisAnnual.Yearly_Prod[yearInd].prod - thisAnnual.LT_Avg) / thisAnnual.LT_Avg;
            }
            else if (thisMonth != 100 & thisYear != 100) // deviation from long-term monthly
            {
                int[] monthYearInd = Get_Month_Year_Inds(thisMonthly, thisMonth, thisYear);
                devFromLT = (thisMonthly[monthYearInd[0]].YearProd[monthYearInd[1]].prod - thisMonthly[monthYearInd[0]].LT_Avg) / thisMonthly[monthYearInd[0]].LT_Avg;
            }

            return devFromLT;
        }

        /// <summary>  Opens a raw data file, looks at lat/long and assigns Xind and Yind to nodesToPull. If any nodesToPull are out of the range, returns false. </summary>
        public bool GetPullXYIndices(ref RefData_Pull[] nodesToPull)
        {
            bool gotAllIndices = false;
            if (nodesToPull == null)
                return gotAllIndices;

            int numLats = 0;
            int numLongs = 0;
            double[] lats = new double[numLats];
            double[] longs = new double[numLongs];

            if (refDataDownload.refType == "MERRA2")
            {
                string[] refDataFiles = Directory.GetFiles(refDataDownload.folderLocation, "*.ascii");
                string line;

                if (refDataFiles == null)
                {
                    MessageBox.Show("Check specified folderpath and try again.");
                    return gotAllIndices;
                }

                StreamReader file;

                try
                {
                    file = new StreamReader(refDataFiles[0]);
                }
                catch
                {
                    MessageBox.Show("Error opening MERRA data file. Check that it's not open in another program.");
                    return gotAllIndices;
                }
                                                
                while ((line = file.ReadLine()) != null)
                {
                    string[] substrings = line.Split(',');

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
            }
            else if (refDataDownload.refType == "ERA5")
            {
                // Get ERA5 pull index
                string[] refDataFiles = Directory.GetFiles(refDataDownload.folderLocation, "*.nc");

                if (refDataFiles == null)
                {
                    MessageBox.Show("Check specified folderpath and try again.");
                    return gotAllIndices;
                }

                try
                {
                    DataSet thisERA5Data = DataSet.Open(refDataFiles[0]);
                    Variable[] allVars = thisERA5Data.Variables.ToArray();

                    Single[] singlats = thisERA5Data.GetData<Single[]>("latitude");
                    Single[] singlons = thisERA5Data.GetData<Single[]>("longitude");

                    numLats = singlats.Length;
                    numLongs = singlons.Length;

                    lats = new double[singlats.Length];
                    longs = new double[singlons.Length];

                    for (int l = 0; l < singlats.Length; l++)
                        lats[l] = Convert.ToDouble(singlats[l]);

                    for (int l = 0; l < singlons.Length; l++)
                        longs[l] = Convert.ToDouble(singlons[l]);
                }
                catch
                {
                    MessageBox.Show("Error opening ERA5 data file.");
                    return gotAllIndices;
                }   
            }

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
                    MessageBox.Show("The reference data file does not contain all required lat/long nodes. Min Lat: " + minLat.ToString() + ", Max Lat: " + maxLat.ToString() +
                        ", Min Long: " + minLong.ToString() + ", Max Long: " + maxLong.ToString());
                    return gotAllIndices;
                }
            }

            gotAllIndices = true;
            
            return gotAllIndices;
        }

        /// <summary> Finds lat/long and datafile X/Y indices for reference node(s) closest to project site. Returns true if required reference data are available (i.e. locally downloaded). </summary>        
        public bool FindCoords(ReferenceCollection refList)
        {
            bool foundInds = false;
            
            double[] lats = refList.GetAllLatsOrLongs(refDataDownload, "Lats");
            double[] longs = refList.GetAllLatsOrLongs(refDataDownload, "Longs");
            int numPerRC = 1; // Number of nodes per row/column
            numPerRC = (int)Math.Pow(numNodes, 0.5);
                

            if ((lats.Length < 4 || longs.Length < 4) && numPerRC == 4) // 16 MERRA nodes
            {
                MessageBox.Show("Reference data range is too small. A minimum of four latitudes and four longitudes are needed if 16 nodes are used.", "Continuum 3");
                return foundInds;
            }
            else if ((lats.Length < 2 || longs.Length < 2) && numPerRC == 2) // 4 reference nodes
            {
                MessageBox.Show("Reference data range is too small. A minimum of two latitudes and two longitudes are needed if 4 nodes are used.", "Continuum 3");
                return foundInds;
            }

            // Figure out if the Reference files have nodes covering the specified area
            double latReso = refList.GetLatRes(refDataDownload);
            double longReso = refList.GetLongRes(refDataDownload);

            //  double minReqLat = interpData.Coords.Lat - (numPerRC / 2) * latReso;
            double minReqLat = lats[0];
            double minReqLong = longs[0];
            double maxReqLat = lats[lats.Length - 1];
            double maxReqLong = longs[longs.Length - 1];

            if (numNodes > 1)
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
                    MessageBox.Show("Reference data range is not large enough to cover desired number of nodes.");
                    return foundInds;
                }
            }

            if (interpData.Coords.latitude < (minReqLat - latReso / 2) || interpData.Coords.latitude > (maxReqLat + latReso / 2))
            {
                MessageBox.Show("Outside of available Reference data range. With " + numNodes + " nodes selected, the allowed latitude range is " + minReqLat + " to " + maxReqLat);
                return foundInds;
            }

            if (interpData.Coords.longitude < (minReqLong - longReso / 2) || interpData.Coords.longitude > (maxReqLong + longReso / 2))
            {
                MessageBox.Show("Outside of available Reference data range. With " + numNodes + " nodes selected, the allowed longitude range is " + minReqLong + " to " + maxReqLong);
                return foundInds;
            }

            int startLatInd = 0;
            int startLongInd = 0;
            double lastDiff = 1000;

            if (numNodes == 1) // find lat and long closest to project site (as opposed to finding the lat/long that form a box around project site)
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

            if (nodes.Length == 0)
                Array.Resize(ref nodes, numPerRC * numPerRC);

            for (int row = 0; row < numPerRC; row++)
                for (int col = 0; col < numPerRC; col++)
                {
                    int refInd = row * numPerRC + col;
                    nodes[refInd].XY_ind.X_ind = startLatInd + col;
                    nodes[refInd].XY_ind.Lat = lats[0] + (startLatInd + col) * latReso;
                    nodes[refInd].XY_ind.Y_ind = startLongInd + row;
                    nodes[refInd].XY_ind.Lon = longs[0] + (startLongInd + row) * longReso;
                }

            foundInds = true;
            

            return foundInds;
        }


        /// <summary> Returns number of days in month. </summary>
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

        /// <summary> Adds new MERRA2 data to database. Saved in local time. </summary>
        public void AddNewDataToDB(Continuum thisInst, RefData_Pull[] newDataToAdd)
        {
            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            if (newDataToAdd == null)
                return;

            int numNewData = newDataToAdd.Length;

            using (var context = new Continuum_EDMContainer(connString))
            {
                if (refDataDownload.refType == "MERRA2")
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
                }
                else if (refDataDownload.refType == "ERA5")
                {
                    for (int i = 0; i < numNewData; i++)
                    {
                        ERA_Node_table eraNodeTable = new ERA_Node_table();
                        eraNodeTable.latitude = newDataToAdd[i].Coords.latitude;
                        eraNodeTable.longitude = newDataToAdd[i].Coords.longitude;

                        MemoryStream MS1 = new MemoryStream();
                        bin.Serialize(MS1, newDataToAdd[i].Data);
                        eraNodeTable.eraData = MS1.ToArray();

                        try
                        {
                            context.ERA_Node_table.Add(eraNodeTable);
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.InnerException.ToString());
                            return;
                        }

                    }
                }

                context.Database.Connection.Close();
            }
        }

        /// <summary> Checks the database to see if the saved reference data covers to specified start/end times </summary>        
        public bool DoesDB_HaveRequestedDataRange(Continuum thisInst)
        {
            // Returns true if data saved in DB covers the start/end range
            bool gotTheData = true;

            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            using (var context = new Continuum_EDMContainer(connString))
            {
                for (int i = 0; i < numNodes; i++)
                {
                    double thisLat = nodes[i].XY_ind.Lat;
                    double thisLong = nodes[i].XY_ind.Lon;

                    if (refDataDownload.refType == "MERRA2")
                    {
                        var thisMERRAData = from N in context.MERRA_Node_table where N.latitude == thisLat && N.longitude == thisLong select N;

                        foreach (var N in thisMERRAData)
                        {
                            MemoryStream MS = new MemoryStream(N.merraData);

                            // Data may be stored using old MERRA.Wind_TS struct
                            var thisTSData = bin.Deserialize(MS);
                            MS.Close();

                            Wind_TS_with_Prod[] newStructTSData = new Wind_TS_with_Prod[0];
                            MERRA.Wind_TS[] oldStructTSData = new MERRA.Wind_TS[0];

                            try
                            {
                                // Try new struct first
                                newStructTSData = (Wind_TS_with_Prod[])thisTSData;

                                if (newStructTSData[0].thisDate > startDate.AddHours(-interpData.UTC_offset))
                                    gotTheData = false;
                                else if (newStructTSData[newStructTSData.Length - 1].thisDate < endDate.AddHours(-interpData.UTC_offset))
                                    gotTheData = false;
                            }
                            catch
                            {
                                // Threw error so try old one
                                try
                                {
                                    oldStructTSData = (MERRA.Wind_TS[])thisTSData;

                                    if (oldStructTSData[0].ThisDate > startDate.AddHours(-interpData.UTC_offset))
                                        gotTheData = false;
                                    else if (oldStructTSData[newStructTSData.Length - 1].ThisDate < endDate.AddHours(-interpData.UTC_offset))
                                        gotTheData = false;
                                }
                                catch
                                {
                                    MessageBox.Show("Error reading MERRA2 data from database file");
                                }
                            }


                        }
                    }
                    else if (refDataDownload.refType == "ERA5")
                    {
                        Wind_TS_with_Prod[] newStructTSData = new Wind_TS_with_Prod[0];
                        var thisERA5Data = from N in context.ERA_Node_table where N.latitude == thisLat && N.longitude == thisLong select N;

                        foreach (var N in thisERA5Data)
                        {
                            MemoryStream MS = new MemoryStream(N.eraData);
                            newStructTSData = (Wind_TS_with_Prod[])bin.Deserialize(MS);
                            MS.Close();

                            if (newStructTSData[0].thisDate > startDate.AddHours(-interpData.UTC_offset))
                                gotTheData = false;
                            else if (newStructTSData[newStructTSData.Length - 1].thisDate < endDate.AddHours(-interpData.UTC_offset))
                                gotTheData = false;
                        }

                    }
                }

                context.Database.Connection.Close();
            }


                return gotTheData;

        }

        /// <summary> Gets reference data from database. </summary>
        public void GetReferenceDataFromDB(Continuum thisInst)
        {
            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            using (var context = new Continuum_EDMContainer(connString))
            {
                double thisLat = interpData.Coords.latitude;
                double thisLong = interpData.Coords.longitude;
                DateTime startTimeUTC0 = startDate.AddHours(-interpData.UTC_offset);
                DateTime endTimeUTC0 = endDate.AddHours(-interpData.UTC_offset);

                for (int i = 0; i < numNodes; i++)
                {
                    thisLat = nodes[i].XY_ind.Lat;
                    thisLong = nodes[i].XY_ind.Lon;

                    if (refDataDownload.refType == "MERRA2")
                    {
                        var thisMERRAData = from N in context.MERRA_Node_table where N.latitude == thisLat && N.longitude == thisLong select N;

                        foreach (var N in thisMERRAData)
                        {
                            MemoryStream MS = new MemoryStream(N.merraData);

                            // Data may be stored using old MERRA.Wind_TS struct
                            var thisTSData = bin.Deserialize(MS);
                            MS.Close();

                            Wind_TS_with_Prod[] newStructTSData = new Wind_TS_with_Prod[0];
                            MERRA.Wind_TS[] oldStructTSData = new MERRA.Wind_TS[0];

                            try
                            {
                                // Try new struct first
                                newStructTSData = (Wind_TS_with_Prod[])thisTSData;

                                // Find start/end indices
                                int startInd = 0;
                                int endInd = newStructTSData.Length - 1;

                                while (newStructTSData[startInd].thisDate < startTimeUTC0)
                                    startInd++;

                                while (newStructTSData[endInd].thisDate > endTimeUTC0)
                                    endInd--;

                                nodes[i].TS_Data = newStructTSData.Skip(startInd).Take(endInd - startInd + 1).ToArray();

                                // Convert nodes timestamp to local time
                                for (int t = 0; t < nodes[i].TS_Data.Length; t++)
                                    nodes[i].TS_Data[t].thisDate = nodes[i].TS_Data[t].thisDate.AddHours(interpData.UTC_offset);
                            }
                            catch
                            {
                                // Threw error so try old one
                                try
                                {
                                    oldStructTSData = (MERRA.Wind_TS[])thisTSData;

                                    // Find start/end indices
                                    int startInd = 0;
                                    int endInd = oldStructTSData.Length - 1;

                                    while (oldStructTSData[startInd].ThisDate < startTimeUTC0)
                                        startInd++;

                                    while (oldStructTSData[endInd].ThisDate > endTimeUTC0)
                                        endInd--;
                                                                        
                                    // Populate nodes.TS_Data with data
                                    int numRecs = endInd - startInd + 1;
                                    nodes[i].TS_Data = new Wind_TS_with_Prod[numRecs];

                                    for (int r = 0; r < numRecs; r++)
                                    {
                                        nodes[i].TS_Data[r].thisDate = oldStructTSData[r + startInd].ThisDate.AddHours(interpData.UTC_offset);
                                        nodes[i].TS_Data[r].WS = oldStructTSData[r + startInd].WS50m;
                                        nodes[i].TS_Data[r].WD = oldStructTSData[r + startInd].WD50m;
                                        nodes[i].TS_Data[r].temperature = oldStructTSData[r + startInd].Temp10m;
                                        nodes[i].TS_Data[r].surfPress = oldStructTSData[r + startInd].SurfPress;
                                        nodes[i].TS_Data[r].seaPress = oldStructTSData[r + startInd].SeaPress;
                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("Error reading MERRA2 data from database file");
                                }
                            }  
                        }
                    }
                    else if (refDataDownload.refType == "ERA5")
                    {
                        var thisERA5Data = from N in context.ERA_Node_table where N.latitude == thisLat && N.longitude == thisLong select N;

                        foreach (var N in thisERA5Data)
                        {
                            MemoryStream MS = new MemoryStream(N.eraData);
                            Wind_TS_with_Prod[] allERA5Data = (Wind_TS_with_Prod[])bin.Deserialize(MS);
                            MS.Close();

                            // Find start/end indices
                            int startInd = 0;
                            int endInd = allERA5Data.Length - 1;

                            while (allERA5Data[startInd].thisDate < startTimeUTC0)
                                startInd++;

                            while (allERA5Data[endInd].thisDate > endTimeUTC0)
                                endInd--;

                            nodes[i].TS_Data = allERA5Data.Skip(startInd).Take(endInd - startInd + 1).ToArray();

                            // Convert nodes timestamp to local time
                            for (int t = 0; t < nodes[i].TS_Data.Length; t++)
                                nodes[i].TS_Data[t].thisDate = nodes[i].TS_Data[t].thisDate.AddHours(interpData.UTC_offset);
                        }                  
                    }
                }

                context.Database.Connection.Close();
            }

        }

        /// <summary> Generates interpData time series dataset which contains MERRA2 interpolated temperature, pressure, and wind speed data </summary>        
        public void GetInterpData(UTM_conversion utm)
        {
            // interpData not saved in file or DB, only MERRA2 data saved in DB and interpData created as needed)
            if (nodes.Length == 0)
                return;

            if (nodes[0].TS_Data == null)
                return;

            int MERRA_Length = nodes[0].TS_Data.Length;
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

                interpData.TS_Data[i].thisDate = nodes[0].TS_Data[i].thisDate;

                for (int n = 0; n < numNodes; n++)
                {
                    UTM_conversion.UTM_coords theseUTM = utm.LLtoUTM(nodes[n].XY_ind.Lat, nodes[n].XY_ind.Lon);
                    double thisDist = topo.CalcDistanceBetweenPoints(interpData.UTM.UTMEasting, interpData.UTM.UTMNorthing, theseUTM.UTMEasting, theseUTM.UTMNorthing);
                    if (thisDist == 0)
                        thisDist = 0.1; // so it doesn't throw divide by zero error

                    sumDist = sumDist + 1 / thisDist;

                    double thisU = -nodes[n].TS_Data[i].WS * Math.Cos(Math.PI / 180 * (90 - nodes[n].TS_Data[i].WD)); // negative sign is there since WD is the direction 
                    double thisV = -nodes[n].TS_Data[i].WS * Math.Sin(Math.PI / 180 * (90 - nodes[n].TS_Data[i].WD)); // that wind is coming from.

                    avgU = avgU + thisU / thisDist;
                    avgV = avgV + thisV / thisDist;
                    avgTemp = avgTemp + nodes[n].TS_Data[i].temperature / thisDist;
                    avgSurfPress = avgSurfPress + nodes[n].TS_Data[i].surfPress / thisDist;
                    avgSeaLevelPress = avgSeaLevelPress + nodes[n].TS_Data[i].seaPress / thisDist;
                }

                if (sumDist > 0)
                {
                    avgU = avgU / sumDist;
                    avgV = avgV / sumDist;
                    interpData.TS_Data[i].WS = Math.Sqrt(avgU * avgU + avgV * avgV);
                    interpData.TS_Data[i].WD = 180 + 180 / Math.PI * Math.Atan2(avgU, avgV);
                    interpData.TS_Data[i].temperature = avgTemp / sumDist;
                    interpData.TS_Data[i].seaPress = avgSeaLevelPress / sumDist;
                    interpData.TS_Data[i].surfPress = avgSurfPress / sumDist;
                }
            }

            if (powerCurve.name != null)
                ApplyPC(ref interpData.TS_Data);

        }

        /// <summary> Finds and returns maximum hourly wind speed for each year of long-term data (used in extreme WS calcs). </summary>        
        public Met.MaxYearlyWind[] GetMaxHourlyWindSpeeds()
        {
            int refLength = interpData.TS_Data.Length;
            int firstYear = interpData.TS_Data[0].thisDate.Year;
            int lastYear = interpData.TS_Data[refLength - 1].thisDate.Year;
            int numYears = lastYear - firstYear + 1;
            Met.MaxYearlyWind[] maxHourlyRef = new Met.MaxYearlyWind[numYears];

            double thisMax = 0;
            lastYear = firstYear;
            int yearInd = 0;
            int thisYear = 0;

            for (int i = 0; i < refLength; i++)
            {
                thisYear = interpData.TS_Data[i].thisDate.Year;
                if (thisYear == lastYear)
                {
                    if (interpData.TS_Data[i].WS > thisMax)
                        thisMax = interpData.TS_Data[i].WS;
                }
                else
                {
                    maxHourlyRef[yearInd].maxWS = thisMax;
                    maxHourlyRef[yearInd].thisYear = lastYear;
                    thisMax = interpData.TS_Data[i].WS;
                    yearInd++;
                    lastYear = thisYear;
                }
            }

            if (yearInd < numYears && interpData.TS_Data[refLength - 1].thisDate.Month == 12 && interpData.TS_Data[refLength - 1].thisDate.Day == 31)
            {
                maxHourlyRef[yearInd].maxWS = thisMax;
                maxHourlyRef[yearInd].thisYear = lastYear;
            }


            return maxHourlyRef;
        }

        /// <summary> Returns true if data was fully loaded. </summary>  
        public bool WasDataFullyLoaded(RefData_Pull[] refData)
        {
            bool fullyLoaded = true;
            TimeSpan timeSpan;
            DateTime thisTime;
            DateTime lastTime = DateTime.Now;

            for (int i = 0; i < refData.Length; i++)
            {
                if (refData[i].Data.Length > 0)
                    lastTime = refData[i].Data[0].thisDate;

                for (int j = 0; j < refData[i].Data.Length; j++)
                {
                    thisTime = refData[i].Data[j].thisDate;
                    timeSpan = thisTime.Subtract(lastTime);

                    if (timeSpan.TotalMinutes > 60) // one hour
                    {
                        MessageBox.Show("Missing reference data. Start of missing interval: " + thisTime.ToString());
                        return fullyLoaded = false;
                    }

                    if (refData[i].Data[j].WS == 0 && refData[i].Data[j].WD == 0)
                    {
                        MessageBox.Show("Reference data was not fully loaded. Missing data: " + lastTime.ToString());
                        return fullyLoaded = false;
                    }

                    lastTime = thisTime;
                }
            }


            return fullyLoaded;
        }


        /// <summary> Returns array of lat/long coordinates of reference data coordinates for specified lat/long </summary>        
        public UTM_conversion.Lat_Long[] GetRequiredCoords(ReferenceCollection refList)
        {
            UTM_conversion.Lat_Long[] theseReqCoords = new UTM_conversion.Lat_Long[numNodes];

            double latitude = interpData.Coords.latitude;
            double longitude = interpData.Coords.longitude;

            double latRes = refList.GetLatRes(refDataDownload);
            double lonRes = refList.GetLongRes(refDataDownload);

            double minReqLat = Math.Round(latitude / latRes, 0) * latRes;
            double minReqLong = Math.Round(longitude / lonRes) * lonRes;
            double maxReqLat = minReqLat;
            double maxReqLong = minReqLong;

            if (numNodes == 1)
            {
                theseReqCoords[0].latitude = minReqLat;
                theseReqCoords[0].longitude = minReqLong;
            }
            else if (numNodes == 4)
            {
                if (latitude >= minReqLat)
                    maxReqLat = minReqLat + latRes;
                else
                {
                    minReqLat = minReqLat - latRes;
                    maxReqLat = minReqLat + latRes;
                }

                if (longitude >= minReqLong)
                    maxReqLong = minReqLong + lonRes;
                else
                {
                    minReqLong = minReqLong - lonRes;
                    maxReqLong = minReqLong + lonRes;
                }

                theseReqCoords[0].latitude = minReqLat;
                theseReqCoords[0].longitude = minReqLong;

                theseReqCoords[1].latitude = minReqLat;
                theseReqCoords[1].longitude = maxReqLong;

                theseReqCoords[2].latitude = maxReqLat;
                theseReqCoords[2].longitude = minReqLong;

                theseReqCoords[3].latitude = maxReqLat;
                theseReqCoords[3].longitude = maxReqLong;
            }
            else if (numNodes == 16)
            {
                if (latitude > minReqLat)
                {
                    minReqLat = minReqLat - latRes;
                    maxReqLat = minReqLat + 3 * latRes;
                }
                else
                {
                    minReqLat = minReqLat - 1;
                    maxReqLat = minReqLat + 3 * latRes;
                }

                if (longitude > minReqLong)
                {
                    minReqLong = minReqLong - lonRes;
                    maxReqLong = minReqLong + 3 * lonRes;
                }
                else
                {
                    minReqLong = minReqLong - 2 * lonRes;
                    maxReqLong = minReqLong + 3 * lonRes;
                }

                int latInd = 0;
                int longInd = 0;

                for (int i = 0; i < numNodes; i++)
                {
                    theseReqCoords[i].latitude = minReqLat + latRes * latInd;
                    theseReqCoords[i].longitude = minReqLong + lonRes * longInd;

                    latInd++;
                    if (latInd >= 4)
                    {
                        latInd = 0;
                        longInd++;
                    }
                }

            }

            return theseReqCoords;
        }

        /// <summary> Finds the min/max lat/long of nodes needed for specified latitude and longitude. Returns coordinates of additional data needed (if any). </summary>
        public UTM_conversion.Lat_Long[] GetRequiredNewNodeCoords(Continuum thisInst)
        {
            UTM_conversion.Lat_Long[] newRequiredRefNodes = new UTM_conversion.Lat_Long[0];
            int numNewReqNodes = 0;

            UTM_conversion.Lat_Long[] theseRequiredNodes = GetRequiredCoords(thisInst.refList);
            UTM_conversion.Lat_Long[] existingNodes = new UTM_conversion.Lat_Long[0];
            int numExistingNodes = 0;

            // Loop through required nodes and see what additional nodes are needed
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            using (var context = new Continuum_EDMContainer(connString))
            {
                if (refDataDownload.refType == "MERRA2")
                {
                    var theseNodes = from N in context.MERRA_Node_table select N;

                    foreach (var N in theseNodes)
                    {
                        numExistingNodes++;
                        Array.Resize(ref existingNodes, numExistingNodes);
                        existingNodes[numExistingNodes - 1].latitude = N.latitude;
                        existingNodes[numExistingNodes - 1].longitude = N.longitude;
                    }
                }
                else if (refDataDownload.refType == "ERA5")
                {
                    // ERA5 DB
                    var theseNodes = from N in context.ERA_Node_table select N;

                    foreach (var N in theseNodes)
                    {
                        numExistingNodes++;
                        Array.Resize(ref existingNodes, numExistingNodes);
                        existingNodes[numExistingNodes - 1].latitude = N.latitude;
                        existingNodes[numExistingNodes - 1].longitude = N.longitude;
                    }
          
                }
            }

            for (int i = 0; i < numNodes; i++)
            {
                bool gotIt = false;
                for (int j = 0; j < numExistingNodes; j++)
                {
                    if (existingNodes[j].latitude == theseRequiredNodes[i].latitude && existingNodes[j].longitude == theseRequiredNodes[i].longitude)
                    {
                        gotIt = true;
                        break;
                    }
                }

                if (gotIt == false)
                {
                    numNewReqNodes++;
                    Array.Resize(ref newRequiredRefNodes, numNewReqNodes);
                    newRequiredRefNodes[numNewReqNodes - 1].latitude = theseRequiredNodes[i].latitude;
                    newRequiredRefNodes[numNewReqNodes - 1].longitude = theseRequiredNodes[i].longitude;
                }
            }

            return newRequiredRefNodes;
        }


       

        /// <summary>  Return node lat/longs in reference data file </summary>
        public Node_Data[] GetAllNodesInFile()
        {
            // Opens first file in folder and creates MERRA objects for each set of latitude and longitude
            Node_Data[] allRefnodes = new Node_Data[0];

            bool folderExists = Directory.Exists(refDataDownload.folderLocation);
            if (folderExists == false)
                return allRefnodes;

            int numLats = 0;
            int numLongs = 0;
            double[] lats = new double[numLats];
            double[] longs = new double[numLongs];

            if (refDataDownload.refType == "MERRA2")
            {
                string[] MERRAfiles = Directory.GetFiles(refDataDownload.folderLocation, "*.ascii");
                string line;

                if (MERRAfiles.Length > 0)
                {
                    StreamReader file = new StreamReader(MERRAfiles[0]); 

                    while ((line = file.ReadLine()) != null)
                    {
                        string[] substrings = line.Split(',');

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
                }
            }
            else if (refDataDownload.refType == "ERA5")
            {                
                string[] refDataFiles = Directory.GetFiles(refDataDownload.folderLocation, "*.nc");

                if (refDataFiles == null)
                {
                    MessageBox.Show("Check specified folder path and try again.");
                    return allRefnodes;
                }

                try
                {
                    DataSet thisERA5Data = DataSet.Open(refDataFiles[0]);
                    Variable[] allVars = thisERA5Data.Variables.ToArray();

                    Single[] singlats = thisERA5Data.GetData<Single[]>("latitude");
                    Single[] singlons = thisERA5Data.GetData<Single[]>("longitude");

                    numLats = singlats.Length;
                    numLongs = singlons.Length;

                    lats = new double[numLats];
                    longs = new double[numLongs];

                    for (int l = 0; l < singlats.Length; l++) 
                        lats[l] = Convert.ToDouble(singlats[l]);

                    for (int l = 0; l < singlons.Length; l++)
                        longs[l] = Convert.ToDouble(singlons[l]);
                    
                }
                catch
                {
                    MessageBox.Show("Error opening ERA5 data file.");
                    return allRefnodes;
                }
            }

            int nodeInd = 0;
            int numNodes = numLats * numLongs;
            allRefnodes = new Node_Data[numNodes];

            for (int l = 0; l < numLats; l++)
                for (int g = 0; g < numLongs; g++)
                {
                    allRefnodes[nodeInd] = new Node_Data();
                    allRefnodes[nodeInd].XY_ind.Lat = lats[l];
                    allRefnodes[nodeInd].XY_ind.Lon = longs[g];
                    nodeInd++;
                }

            return allRefnodes;
        }

        /// <summary> Sets new object to default settings for selected referency type </summary>
        public void SetDefaultsForReferenceType()
        {
            // Sets settings to default values for reference type
            if (refDataDownload.refType == "MERRA2")
            {
                WS_ScaleFactor = 0.85;
                wswdH = 50;
                temperatureH = 10;                
            }
            else if (refDataDownload.refType == "ERA5")
            {
                WS_ScaleFactor = 1.0;
                wswdH = 100;
                temperatureH = 10;          
            }
        }

    }
}

