////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	MCP.cs
//
// summary:	Imports reference and target WS and WD data and performs Measure-Correlate-Predict
// to estimate long-term wind speeds at target site. 
// Authors: Liz Walls, Andrew Rapin
// Reviewer: Liz Walls
// Version Number: 1.0
// Release Date: 10/19/2017
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace ContinuumNS
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Main Form of MCP tool. </summary>
    ///
    /// <remarks>   OEE, 10/19/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Serializable()]
    public partial class MCP
    {
        /// <summary>   Start Date/Time of Reference site data. </summary>
        public DateTime refStart;
        /// <summary>   End Date/Time of Reference site data. </summary>
        public DateTime refEnd;
        /// <summary>   Start Date/Time of Target site data. </summary>
        public DateTime targetStart;
        /// <summary>   End Date/Time of Target site data. </summary>
        public DateTime targetEnd;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start Date/Time of the concurrent data (i.e. overlap between Reference and Target) .
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DateTime concStart;
        /// <summary>   End Date/Time of the concurrent data. </summary>
        public DateTime concEnd;
        /// <summary>   Start Date/Time of export interval. </summary>
        public DateTime exportStart;
        /// <summary>   End Date/Time of the export interval. </summary>
        public DateTime exportEnd;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Array of type Site_data for Reference site. Each Site_data entry contains Date, WS, WD and
        /// Temp.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Site_data[] refData = new Site_data[0]; // Not saved with file. Regenerated as needed.
        /// <summary>   True if reference data has been imported. </summary>
        public bool gotRef = false;
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Array of type Site_data for Target site. Each Site_data entry contains Date/Time, WS, WD and
        /// Temp.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Site_data[] targetData = new Site_data[0]; // Not saved with file. Regenerated as needed.
        /// <summary>   True if target data has been imported. </summary>
        public bool gotTarg = false;
                
        /// <summary> Array of type Concurrent_data for a specified window (i.e. not necessarily all concurrent data) </summary>        
        public Concurrent_data[] concData = new Concurrent_data[0];

        /// <summary>   True if conccurent data is defined. </summary>
        public bool gotConc = false;

        /// <summary>   Array of type Concurrent_data which holds ALL concurrent data. </summary>
        Concurrent_data[] concDataAll = new Concurrent_data[0];

        public Site_data[] LT_WS_Ests = new Site_data[0];

        public bool gotMCP_Est = false;
                
        /// <summary>   Number of Wind Direction sectors to conduct MCP. </summary>
        public int numWD { get; set; }

        public int numTODs { get; set; } // either 1 or 2 (Day / Night) 

        public int numSeasons { get; set; } // either 1 or 4

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Width of the Wind Speed bin used in Method of Bins and Matrix-LastWS methods.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double WS_BinWidth = 1;
                
        // Matrix-LastWS weights

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Weight factor to apply to estimate from defined Reference-Target WS Matrix.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double matrixWgt = 1;
        /// <summary>   Weight factor to apply to estiamte from defined WS-LastWS Matrix. </summary>
        public double lastWS_Wgt = 1;
          
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Array containing standard deviation of wind speed change at target site for each WS interval.
        /// Used to create Last WS CDF to multiply with Matrix WS PDF in Matrix-LastWS method.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double[] SD_WS_Lag = new double[0];

        /// <summary>   Object of type Lin_MCP conatining results of orthogonal regression MCP. </summary>
        public Lin_MCP MCP_Ortho = new Lin_MCP();
        /// <summary>   Object of type Method_of_Bins containing results of Method Of Bin MCP. </summary>
        public Method_of_Bins MCP_Bins = new Method_of_Bins();
        /// <summary>   Object of type Lin_MCP conatining results of variance MCP. </summary>
        public Lin_MCP MCP_Varrat = new Lin_MCP();
        /// <summary>   Object of type Matrix_Obj containing results of Matrix-LastWS MCP. </summary>
        public Matrix_Obj MCP_Matrix = new Matrix_Obj();

        /// <summary>   Size of the window step size (in months) used in uncertainty calculations. </summary>
        public int uncertStepSize = 1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Array of type MCP_Uncert containing results of uncertainty analysis using orthogonal
        /// regression.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public MCP_Uncert[] uncertOrtho = new MCP_Uncert[0];

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Array of type MCP_Uncert containing results of uncertainty analysis using Method of Bins.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public MCP_Uncert[] uncertBins = new MCP_Uncert[0];

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Array of type MCP_Uncert containing results of uncertainty analysis using variance method.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public MCP_Uncert[] uncertVarrat = new MCP_Uncert[0];

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Array of type MCP_Uncert containing results of uncertainty analysis using Matrix-LastWS.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public MCP_Uncert[] uncertMatrix = new MCP_Uncert[0];

        
        /// <summary> Array of SectorCountBin containing the data count in each WD, time of day, and seasonal bin. </summary>        
        SectorCountBin[] sectors = new SectorCountBin[0];

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Object of type Stats used to perform statistics calcs such as variance and co-variance
        /// calculations.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Stats stat = new Stats();
        
        /// <summary>   True if this analysis is a newly opened file. </summary>
        bool Is_Newly_Opened_File = false;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Structure defined for uncertainty calculation where multiple windows of varying sizes are
        /// used to generate LT Ests.
        /// </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Serializable()]
        public struct MCP_Uncert
        {
            /// <summary>   Window size of concurrent WS data interval in months. </summary>
            public int WSize; 
            
            /// <summary>   Number of concurrent WS windows. </summary>
            public int NWindows;
                        
            /// <summary>   Array of long-term WS estimates generated from each concurrent WS window. </summary>
            public double[] LT_Ests;

            /// <summary>   Average LT WS Estimates. </summary>
            public double avg;
            
            /// <summary>   Standard deviation of LT WS Estimates. </summary>
            public double stDev;

            /// <summary>   Start of data period used in uncertainty analysis. </summary>
            public DateTime[] start;

            /// <summary>   End of data period used in uncertainty analysis. </summary>
            public DateTime[] end;

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Clears this MCP_Uncert object to its blank/initial state. </summary>
            ///
            /// <remarks>   OEE, 10/19/2017. </remarks>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public void Clear()
            {
                WSize = 0;
                NWindows = 0;
                LT_Ests = null;
                avg = 0;
                stDev = 0;
                start = null;
                end = null;                
            }

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///  Struct containing time stamp, wind speed, and wind direction.
        /// </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Serializable()]
        public struct Site_data
        {
            /// <summary>   Time stamp. </summary>
            public DateTime thisDate;
            /// <summary>   Wind Speed. </summary>
            public double thisWS;
            /// <summary>   Wind direction (degs). </summary>
            public double thisWD;            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Structure defined to hold concurrent reference and target wind speed and wind direction time series
        /// </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Serializable()]
        public struct Concurrent_data
        {
            /// <summary>   Time stamp. </summary>
            public DateTime thisDate;
            /// <summary>   Reference wind speed. </summary>
            public double refWS;
            /// <summary>   Reference wind direction. </summary>
            public double refWD;
            /// <summary>   Target wind speed. </summary>
            public double targetWS;
            /// <summary>   Target wind direction. </summary>
            public double targetWD;            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Struture defined to hold linear MCP statistics (such as orthogonol or variance methods)
        /// </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Serializable()]
        public struct Lin_MCP
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// slope of linear MCP methods for each WD sector, each time of day interval and each season
            /// interval.
            /// </summary>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public double[,,] slope;

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// intercept of linear MCP methods for each WD sector, each time of day interval and each season
            /// interval.
            /// </summary>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public double[,,] intercept;

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// R^2 of linear MCP methods for WD and each hourly interval, each time of day interval and each season
            /// interval.
            /// </summary>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public double[,,] R_sq;
            /// <summary>   slope of linear MCP methods for all data (WD, TOD, Season) </summary>
            public double allSlope;
            /// <summary>   intercept of linear MCP methods for all data (WD, TOD, Season) </summary>
            public double allIntercept;
            /// <summary>   R^2 of linear MCP methods for all data (WD, TOD, Season) </summary>
            public double allR_Sq;

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// Time series of wind speed estimated at target site based on linear MCP methods and reference
            /// WS and WD (WD is same as Ref site WD)
            /// </summary>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

      //      public Site_data[] LT_WS_Est;

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Clears Lin_MCP object to its blank state. </summary>
            ///
            /// <remarks>   OEE, 10/19/2017. </remarks>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public void Clear()
            {
                slope = null;
                intercept = null;
                R_sq = null;
             //   LT_WS_Est = null;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Structure defined to hold Method of Bins MCP statistics. </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Serializable()]
        public struct Method_of_Bins
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// Array of type Bin_Object containing average ratio, standard deviation of ratio and bin count
            /// for each WS and WD bin (i = WS bin, j = WD bin.)
            /// </summary>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public Bin_Object[,] binAvgSD_Cnt;

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// Array of type Site_data containing time series of wind speed and wind direction estimated at
            /// target site based on method of bins (WD is same as Ref site WD)
            /// </summary>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

       //     public Site_data[] LT_WS_Est;

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Clears this Method_of_Bins object to its blank state. </summary>
            ///
            /// <remarks>   OEE, 10/19/2017. </remarks>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public void Clear()
            {
                binAvgSD_Cnt = null;
              //  LT_WS_Est = null;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Structure defined to contain average and standard deviation of wind speed ratio and data
        /// count used in Method of Bins MCP.
        /// </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Serializable()]
        public struct Bin_Object
        {
            /// <summary>   Average wind speed ratio. </summary>
            public double avgWS_Ratio;
            /// <summary>   Standard deviation of wind speed ratio. </summary>
            public double SD_WS_Ratio;
            /// <summary>   Bin count. </summary>
            public double count;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Structure defined to hold Matrix cumulative distribution functions and long-term estimated
        /// wind speeds.
        /// </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Serializable()]
        public struct Matrix_Obj
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// Array of type CDF_Obj which contain wind speed cumulative distribution functions which define
            /// probability of a WS at target occurring given a WS at the reference site.
            /// </summary>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public CDF_Obj[] WS_CDFs;

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// Array of type Site_data containing time series of wind speed and wind direction estimated at
            /// target site based on Matrix-LastWS method (WD is same as Ref site WD)
            /// </summary>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

       //     public Site_data[] LT_WS_Est;

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Clears this object to its initial state. </summary>
            ///
            /// <remarks>   OEE, 10/19/2017. </remarks>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public void Clear()
            {
                WS_CDFs = null;
          //      LT_WS_Est = null;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Structure defined to hold statistics and variables associated with cumulative distribution
        /// functions.
        /// </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Serializable()]
        public struct CDF_Obj
        {
            /// <summary>   Cumulative Distribution Function. </summary>
            public double[] CDF; 
            /// <summary>   Minimum wind speed. </summary>
            public double minWS;
            /// <summary>   Size of WS interval. </summary>
            public double WS_interval;

            /// <summary>   Weibull shape factor, k. </summary>
            public double weibull_k;
            /// <summary>   Weibull scale factor, A. </summary>
            public double weibull_A;

            /// <summary>   Wind Speed index. </summary>
            public int WS_Ind;
            /// <summary>   Wind direction index. </summary>
            public int WD_Ind;
            /// <summary>   Time of day bin </summary>
            public Met.TOD TOD;
            /// <summary>   Seasonal bin </summary>
            public Met.Season season;
            /// <summary>   Number of data points used to define CDF. </summary>
            public int count;
        }
                
        /// <summary>   Holds sector count for specified wind direction, time of day, and season bin. </summary>        
        [Serializable()]
        public struct SectorCountBin
        {
            /// <summary>   Wind direction bin </summary>
            public int WD;
            /// <summary>   Time of day. </summary>
            public Met.TOD TOD;
            /// <summary>   Season. </summary>
            public Met.Season season;
            /// <summary>   Data count in bin. </summary>
            public int count;
        }  
                  

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the wind speed width entered on form for Method of Bins or Matrix-LastWS MCP.
        /// </summary>
        ///
        /// <remarks>   Liz, 5/26/2017. </remarks>
        ///
        /// <returns>   The wind speed interval to use in Method of Bins or Matrix-LastWS MCP. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Get_WS_width_for_MCP()
        {            
            return WS_BinWidth;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the wind speed interval to use in TAB file export. </summary>
        ///
        /// <remarks>   Liz, 5/26/2017, Not tested since it is a simple textbox to. </remarks>
        ///
        /// <returns>   The wind speed interval used in TAB file export. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Get_TAB_export_WS_width(Continuum thisInst)
        {            
            double TAB_WS_bin = Convert.ToSingle(thisInst.txtTAB_WS_bin.Text);
            return TAB_WS_bin;
        }      
              

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the number of wind direction sectors used in MCP. </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ///
        /// <returns>   The number of wind direction sectors. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Get_Num_WD()
        {                        
            return numWD;
        }              

        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the WS Matrix PDF weight to use in Matrix-LastWS method. </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ///
        /// <returns>   The Matrix WS PDF weight. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Get_WS_PDF_Weight()
        {
            // Returns WS PDF weight to be used in Matrix-lastWS method            
            return matrixWgt;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the LastWS PDF weight to use in Matrix-LastWS method. </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ///
        /// <returns>   The LastWS PDF weight. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Get_Last_WS_Weight()
        {
            // Returns Last WS weight to be used in Matrix-lastWS method            
            return lastWS_Wgt;            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Calculates WD index of entered wind direction . </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ///
        /// <param name="thisWD">  Wind direction in degrees. </param>
        /// <param name="numWD">   Number of wind direction sectors. </param>
        ///
        /// <returns>   The wind direction index corresponding to entered wind direction. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Get_WD_ind(double thisWD)
        {            
            int WD_Ind = (int)Math.Round(thisWD / (360 / (double)numWD),0, MidpointRounding.AwayFromZero);
                        
            if (WD_Ind == numWD) WD_Ind = 0;

            return WD_Ind;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Calculates WS index of entered wind speed. </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ///
        /// <param name="thisWS">      Wind Speed. </param>
        /// <param name="binWidth">    Width of WS bin. </param>
        ///
        /// <returns>   The wind speed index corresponding to entered wind speed. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Get_WS_ind(double thisWS, double binWidth)
        {
            int WS_Ind = 0;
            if (binWidth != 0)
            WS_Ind = (int)Math.Round(thisWS / binWidth,0, MidpointRounding.AwayFromZero);
            
            return WS_Ind;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets uncertainty analysis step size as entered on form. </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ///
        /// <returns>   The step size (in months) of uncertainty analysis. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Get_Uncert_Step_Size()
        {            
            return uncertStepSize;
        }                

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Generates a cumulative distribution function (CDF) of target wind speed distribution for
        /// each specified wind direction, time of day and seasonal bin.
        /// </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ///
        /// <returns>   An array of type CDF_Obj which contains the CDF, bin indices, etc. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public CDF_Obj[] GenerateMatrixCDFs()
        {                        
            double WS_width = Get_WS_width_for_MCP();          
            
            int numWS = (int)(30 / WS_width);
            if (numWS == 0) numWS = 1;

            int numMatrices = numWD * numTODs * numSeasons * numWS;
            int CDF_Count = 0;
            
            CDF_Obj[] theseCDFs = new CDF_Obj[numMatrices];

            for (int i = 0; i < numWD; i++)                      
                for (int j = 0; j < numTODs; j++)                   
                    for (int k = 0; k < numSeasons; k++)
                        for (int l = 0; l < numWS; l++)
                        {
                            if (j == 0 && numTODs == 1)
                                theseCDFs[CDF_Count].TOD = Met.TOD.All;
                            else if (j == 0)
                                theseCDFs[CDF_Count].TOD = Met.TOD.Day;
                            else
                                theseCDFs[CDF_Count].TOD = Met.TOD.Night;
                            
                            if (k == 0 && numSeasons == 1)
                                theseCDFs[CDF_Count].season = Met.Season.All;
                            else if (k == 0)
                                theseCDFs[CDF_Count].season = Met.Season.Winter;
                            else if (k == 1)
                                theseCDFs[CDF_Count].season = Met.Season.Spring;
                            else if (k == 2)
                                theseCDFs[CDF_Count].season = Met.Season.Summer;
                            else if (k == 3)
                                theseCDFs[CDF_Count].season = Met.Season.Fall;
                            
                            theseCDFs[CDF_Count].WD_Ind = i;
                            theseCDFs[CDF_Count].WS_Ind = l;                                                                                 

                          //  double[] minMaxWD = GetMinMaxWD(i);                            
                            double minWS = l * WS_width - WS_width / 2;
                            double maxWS = l * WS_width + WS_width / 2;

                            double[] targetWS = GetConcWS_Array("Target", i, theseCDFs[CDF_Count].TOD, theseCDFs[CDF_Count].season, minWS, maxWS, false);

                            if (targetWS.Length > 1)
                            {
                                Array.Sort(targetWS);
                                double targMinWS = targetWS[0];
                                double targMaxWS = targetWS[targetWS.Length - 1];

                                if (targMinWS == targMaxWS)
                                {
                                    targMinWS = targMinWS - targMinWS * 0.02;
                                    targMaxWS = targMaxWS + targMaxWS * 0.02;
                                }

                                double WS_int = (targMaxWS - targMinWS) / 99;

                                theseCDFs[CDF_Count].count = targetWS.Length;
                                theseCDFs[CDF_Count].minWS = targMinWS;
                                theseCDFs[CDF_Count].WS_interval = WS_int;                                                        
                                                                                                         
                                // count WS in each bin
                                int[] WS_count = new int[100];

                                for (int m = 0; m < targetWS.Length; m++)
                                {
                                    int WS_Ind = Convert.ToInt16((targetWS[m] - targMinWS) / WS_int);
                             //       int Round_ind = (int)Math.Round((targetWS[m] - targMinWS) / WS_int, 0);

                                    WS_count[WS_Ind]++;
                                }

                                theseCDFs[CDF_Count].CDF = new double[100];
                                double[] thisPDF = new double[100];

                                for (int m = 0; m < 100; m++)                                
                                    thisPDF[m] = (double)WS_count[m] / targetWS.Length / WS_int;
                                
                                //  Calculate CDF
                                theseCDFs[CDF_Count].CDF[0] = thisPDF[0] * theseCDFs[CDF_Count].WS_interval;

                                for (int m = 1; m < 100; m++)                                
                                    theseCDFs[CDF_Count].CDF[m] = theseCDFs[CDF_Count].CDF[m - 1] + thisPDF[m] * theseCDFs[CDF_Count].WS_interval;
                                

                                // interpolate between plateaus in CDF
                                //CDF = InterpolateCDF(CDF);

                                // normalize CDF to add to 1.0
                                //   for (int j = 0; j < 1000; j++)
                                //      CDF[j] = CDF[j] / CDF[999];
                            }
                            else if (targetWS.Length > 0)
                            {
                                theseCDFs[CDF_Count].count = 1;
                                theseCDFs[CDF_Count].minWS = targetWS[0];
                                theseCDFs[CDF_Count].WS_interval = 0;
                            }
                            else
                            {
                                theseCDFs[CDF_Count].count = 0;
                                theseCDFs[CDF_Count].minWS = l * WS_width;
                                theseCDFs[CDF_Count].WS_interval = 0;
                            }

                            CDF_Count++;
                        
            }

            return theseCDFs;
        }
          
        /// <summary>
        /// Calculates and returns the average wind speeds at the target site and the reference site, and the concurrent data count 
        /// </summary>
        public double[] GetConcAvgsCount(int WD_Ind, Met.TOD TOD, Met.Season Season)
        {
            double[] avgWS_Count = { 0, 0, 0 }; // 0: Target WS; 1: Reference WS; 2: Data count'
            MetCollection metList = new MetCollection();
            metList.numWD = numWD;
            metList.numTOD = numTODs;
            metList.numSeason = numSeasons;
                                   
            foreach (Concurrent_data Conc in concData)
                if (Conc.thisDate >= concStart && Conc.thisDate <= concEnd)
                {
                    int thisWD_Ind = Get_WD_ind(Conc.refWD);
                    Met.TOD thisTOD = metList.GetTOD(Conc.thisDate);
                    Met.Season thisSeason = metList.GetSeason(Conc.thisDate);
                    
                    if ((TOD == Met.TOD.All && Season == Met.Season.All) || (thisTOD == TOD && thisSeason == Season))
                    {
                        if (WD_Ind == numWD || thisWD_Ind == WD_Ind)
                        {
                            avgWS_Count[0] = avgWS_Count[0] + Conc.targetWS;
                            avgWS_Count[1] = avgWS_Count[1] + Conc.refWS;
                            avgWS_Count[2] = avgWS_Count[2] + 1;
                        }                        
                    }
                }

            if (avgWS_Count[2] > 0)
            {
                avgWS_Count[0] = avgWS_Count[0] / avgWS_Count[2];
                avgWS_Count[1] = avgWS_Count[1] / avgWS_Count[2];
            }

            return avgWS_Count;
        }              

        /// <summary>
        /// Finds and returns array of either reference and target wind speeds during concurrent period for specified wind direction, TOD, season, and wind speed range
        /// </summary>        
        public double[] GetConcWS_Array(string targetOrRef, int WD_Ind, Met.TOD TOD, Met.Season season, double minWS, double maxWS, bool getAll)
        {
            double[] theseWS = null;            
            int thisWD_Ind = numWD;
            Met.TOD thisTOD = Met.TOD.All;
            Met.Season thisSeason = Met.Season.All;
            MetCollection metList = new MetCollection();
            metList.numWD = numWD;
            metList.numTOD = numTODs;
            metList.numSeason = numSeasons;
                        
            if (gotConc)
            {
                if (getAll == true) //|| ((minWS == 0) && (maxWS == 30) && ((WD_Ind == 0) && (Get_Num_WD() == 1)) && ((hourlyInd == 0) && (Get_Num_Hourly_Ints() == 1)) && ((tempInd == 0) && (Get_Num_Temp_Ints() == 1))))            
                {
                    Array.Resize(ref theseWS, concData.Length);

                    for (int i = 0; i < concData.Length; i++)
                        if (targetOrRef == "Target")
                            theseWS[i] = concData[i].targetWS;
                        else
                            theseWS[i] = concData[i].refWS;
                }
                else
                {
                    int WD_count = 0;

                    foreach (Concurrent_data These_Conc in concData)
                    {
                        if (WD_Ind != numWD)
                            thisWD_Ind = Get_WD_ind(These_Conc.refWD);

                        if (TOD != Met.TOD.All)
                            thisTOD = metList.GetTOD(These_Conc.thisDate);

                        if (season != Met.Season.All)
                            thisSeason = metList.GetSeason(These_Conc.thisDate);                                               

                        if ((These_Conc.refWS > minWS) && (These_Conc.refWS <= maxWS) && (thisWD_Ind == WD_Ind) && (thisTOD == TOD) && (thisSeason == season))
                            WD_count++;

                    }

                    Array.Resize(ref theseWS, WD_count);
                    WD_count = 0;

                    foreach (Concurrent_data These_Conc in concData)
                    {
                        if (WD_Ind != numWD)
                            thisWD_Ind = Get_WD_ind(These_Conc.refWD);

                        if (TOD != Met.TOD.All)
                            thisTOD = metList.GetTOD(These_Conc.thisDate);

                        if (season != Met.Season.All)
                            thisSeason = metList.GetSeason(These_Conc.thisDate);

                        if ((These_Conc.refWS > minWS) && (These_Conc.refWS <= maxWS) && (thisWD_Ind == WD_Ind) && (thisTOD == TOD) && (thisSeason == season))
                        {                            
                            if (targetOrRef == "Target")
                                theseWS[WD_count] = These_Conc.targetWS;
                            else
                                theseWS[WD_count] = These_Conc.refWS;

                            WD_count++;
                        }
                    }
                }
            }

            return theseWS;
        }
        
        
        /// <summary> Creates array of Concurrent_data containing WS and WD at reference and target sites. </summary>        
        public void FindConcurrentData(DateTime start, DateTime end)
        {            
            int concCount = 0;
            int refStartInd = 0;
            int targStartInd = 0;                      
                                               
            concStart = start;
            concEnd = end;
            
            if (refData == null || targetData == null)            
                return;   
            
            if (refData.Length == 0 || targetData.Length == 0) return;

            foreach (Site_data RefSite in refData)
            {
                if (RefSite.thisDate < concStart)
                    refStartInd++;
                else
                    break;
            }

            foreach (Site_data TargSite in targetData)
            {
                if (TargSite.thisDate < concStart)
                    targStartInd++;
                else
                    break;
            }

            for (int i = targStartInd; i < targetData.Length; i++)
            {
                for (int j = refStartInd; j < refData.Length; j++)
                {
                    if (targetData[i].thisDate == refData[j].thisDate && targetData[i].thisDate <= concEnd)
                    {                        
                        concCount = concCount + 1;
                        Array.Resize(ref concDataAll, concCount);
                        concDataAll[concCount - 1].thisDate = targetData[i].thisDate;
                        concDataAll[concCount - 1].refWS = refData[j].thisWS;
                        concDataAll[concCount - 1].refWD = refData[j].thisWD;
                        concDataAll[concCount - 1].targetWS = targetData[i].thisWS;
                        concDataAll[concCount - 1].targetWD = targetData[i].thisWD;                        
                        break;
                    }

                }

                if (targetData[i].thisDate >= concEnd)                
                    break;
                
            }

            concData = concDataAll;

            if (concCount == 0)
                MessageBox.Show("There is no concurrent data between the reference and target site for the selected start and end dates.");
            else
                gotConc = true;
            
        }
        
        /// <summary>   Gets and returns minimum and maximum wind direction for specified wind direction bin. </summary>        
        /// <returns>   An array of type double containing min and max wind direction in degs. </returns>        
        public double[] GetMinMaxWD(int WD_Ind)
        {
            double[] minMaxWD = new double[2];
            
            if ((numWD == 1) || (WD_Ind == numWD))
            {
                minMaxWD[0] = 0;
                minMaxWD[1] = 360;
            }
            else
            {
                minMaxWD[0] = (double)WD_Ind * 360 / numWD - (double)360 / numWD / 2;
                if (minMaxWD[0] < 0) minMaxWD[0] = minMaxWD[0] + 360;

                minMaxWD[1] = (double)WD_Ind * 360 / numWD + (double)360 / numWD / 2;
                if (minMaxWD[1] > 360) minMaxWD[1] = minMaxWD[1] - 360;
            }

            return minMaxWD;

        }

        
        /// <summary> Redefines concurrent data (concData) by copying data from concDataAll between specified start and end dates. </summary> 
        public void GetSubsetConcData(DateTime thisConcStart, DateTime thisConcEnd)
        {
            int startInd = 0; 
            int endInd = 0; 
            int subLength = 0;

            for (int i = 0; i < concDataAll.Length; i++)
            {               

                if (concDataAll[i].thisDate.Year == thisConcStart.Year && concDataAll[i].thisDate.Month == thisConcStart.Month
                    && concDataAll[i].thisDate.Day == thisConcStart.Day && concDataAll[i].thisDate.Hour == thisConcStart.Hour)
                    startInd = i;
                else if (concDataAll[i].thisDate.Year == thisConcEnd.Year && concDataAll[i].thisDate.Month == thisConcEnd.Month
                    && concDataAll[i].thisDate.Day == thisConcEnd.Day && concDataAll[i].thisDate.Hour == thisConcEnd.Hour)
                {
                    endInd = i;
                    subLength = endInd - startInd + 1;
                    break;
                }
                else if (i == concDataAll.Length - 1 && endInd == 0)
                {
                    endInd = concDataAll.Length;
                    subLength = endInd - startInd;
                    break;
                }
            }

            Array.Resize(ref concData, subLength);
            Array.ConstrainedCopy(concDataAll, startInd, concData, 0, subLength);
        }

        
        /// <summary>   Gets the sector count of specified wind speed, time of day, and seasonal bin. </summary>
        public int GetSectorCount(int WD_Ind, Met.TOD TOD, Met.Season season, MetCollection metList)
        {            
            if (sectors.Length == 0)
                Find_Sector_Counts(metList);

            int sectorCount = 0;
            for (int l = 0; l < sectors.Length; l++)
            {
                if (sectors[l].WD == WD_Ind && sectors[l].TOD == TOD && sectors[l].season == season)
                {
                    sectorCount = sectors[l].count;
                    break;
                }
            }

            return sectorCount;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Executes the mcp operation. </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ///
        /// <param name="thisConcStart">  Start Date/Time of concurrent data to use in MCP. </param>
        /// <param name="thisConcEnd">    End Date/Time of concurrent data to use in MCP. </param>
        /// <param name="Use_All_Data">     True to use all data. </param>
        /// <param name="MCP_Method">       The mcp method. </param>
        ///
        /// <returns>   A double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double DoMCP(DateTime thisConcStart, DateTime thisConcEnd, bool Use_All_Data, string MCP_Method, Continuum thisInst, Met thisMet)
        {
            // Performs MCP using a linear model (i.e orthogonal regression or variance ratio) or a method of bins or a Matrix method
            // Orth. Reg. minimizes the distance between both the reference and target site wind speeds from the regression line

            // Build array of conccurent data for specified dates
            GetSubsetConcData(thisConcStart, thisConcEnd);

            // Calculate the regression for each WD and overall
            // To calculate the slope and intercept, need the variance of Y and X and the co-variance of X and Y

            // First calculate for all WD sectors and all hours and all temperatures
            double minWD = 0;
            double maxWD = 360;
            double[] refWS = GetConcWS_Array("Reference", 0, 0, 0, 0, 30, true);
            double[] targetWS = GetConcWS_Array("Target", 0, 0, 0, 0, 30, true);
            
            Stats stat = new Stats();
            double var_x = Convert.ToSingle(stat.CalcVariance(refWS));
            double var_y = Convert.ToSingle(stat.CalcVariance(targetWS));
            double covar_xy = Convert.ToSingle(stat.CalcCovariance(refWS, targetWS));

            int WD_Ind;
            double WS_bin = Get_WS_width_for_MCP();
            int numWS = (int)(30 / WS_bin);                     
            
            double[] thisConc = GetConcAvgsCount(numWD, Met.TOD.All, Met.Season.All);
            double avgTarg = thisConc[0];
            double avgRef = thisConc[1];

            double LT_WS_Est = 0; // if Use_All_Data is false then it is an uncertainty analysis and this value is returned
            double thisSlope = 0;
            double This_Int = 0;
            int totalCount = 0;

            double lastWS_Wgt = Get_Last_WS_Weight();
            double matrixWgt = Get_WS_PDF_Weight();

            MetCollection metList = thisInst.metList;

            // find total data count
            totalCount = totalCount + stat.GetDataCount(refData, exportStart, exportEnd, 0, 0, 0, thisInst.metList, true);

            // if this is not an uncertainty analysis, then calculate the slope, intercept and R^2 for all WD (this is not used in LT WS Estimation, just GUI)
            if (Use_All_Data == true && MCP_Method == "Orth. Regression")
            {
                MCP_Ortho.Clear();
                MCP_Ortho.slope = new double[numWD, numTODs, numSeasons]; // slope for each WD and each hour
                MCP_Ortho.intercept = new double[numWD, numTODs, numSeasons];
                MCP_Ortho.R_sq = new double[numWD, numTODs, numSeasons];

                MCP_Ortho.allSlope = CalcOrthoSlope(var_x, var_y, covar_xy);
                MCP_Ortho.allIntercept = stat.CalcIntercept(avgTarg, MCP_Ortho.allSlope, avgRef);
                MCP_Ortho.allR_Sq = stat.CalcR_Sqr(covar_xy, var_x, var_y);
            }
            else if (Use_All_Data == true && MCP_Method == "Variance Ratio")
            {
                MCP_Varrat.Clear();
                MCP_Varrat.slope = new double[numWD, numTODs, numSeasons]; // slope for each WD and each hour
                MCP_Varrat.intercept = new double[numWD, numTODs, numSeasons];
                MCP_Varrat.R_sq = new double[numWD, numTODs, numSeasons];

                MCP_Varrat.allSlope = CalcVarianceRatioSlope(var_x, var_y);
                MCP_Varrat.allIntercept = stat.CalcIntercept(avgTarg, MCP_Varrat.allSlope, avgRef);
                MCP_Varrat.allR_Sq = stat.CalcR_Sqr(covar_xy, var_x, var_y);
            }
            else if (Use_All_Data == true && MCP_Method == "Method of Bins")
            {
                MCP_Bins.Clear();
                MCP_Bins.binAvgSD_Cnt = new Bin_Object[numWS, numWD + 1]; // WD_Ind = numWD is overall ratio
            }
            else if (MCP_Method == "Matrix")
            {
                MCP_Matrix.WS_CDFs = GenerateMatrixCDFs();
                FindSD_ChangeInWS();
            }

            // Now calculate for all WD and all hourly intervals and all temp intervals
            if (MCP_Method == "Orth. Regression" || MCP_Method == "Variance Ratio")
            {
                for (int i = 0; i < numWD; i++)
                    for (int j = 0; j < numTODs; j++)
                        for (int k = 0; k < numSeasons; k++)
                        {
                            double[] minMaxWD = GetMinMaxWD(i);
                            minWD = minMaxWD[0];
                            maxWD = minMaxWD[1];

                            Met.TOD thisTOD = Met.TOD.All;
                            if (numTODs > 1)
                            {
                                if (j == 0)
                                    thisTOD = Met.TOD.Day;
                                else
                                    thisTOD = Met.TOD.Night;
                            }

                            Met.Season thisSeason = Met.Season.All;
                            if (numSeasons > 1)
                            {
                                if (k == 0)
                                    thisSeason = Met.Season.Winter;
                                else if (k == 1)
                                    thisSeason = Met.Season.Spring;
                                else if (k == 2)
                                    thisSeason = Met.Season.Summer;
                                else if (k == 3)
                                    thisSeason = Met.Season.Fall;
                            }

                            refWS = GetConcWS_Array("Reference", i, thisTOD, thisSeason, 0, 30, false);
                            targetWS = GetConcWS_Array("Target", i, thisTOD, thisSeason, 0, 30, false);

                            // Find Sector count for specific WD, hour, and temp bin combination
                            int sectorCount = GetSectorCount(i, thisTOD, thisSeason, metList);

                            var_x = Convert.ToSingle(stat.CalcVariance(refWS));
                            var_y = Convert.ToSingle(stat.CalcVariance(targetWS));
                            covar_xy = Convert.ToSingle(stat.CalcCovariance(refWS, targetWS));

                            thisConc = GetConcAvgsCount(i, thisTOD, thisSeason);
                            avgTarg = thisConc[0];
                            avgRef = thisConc[1];

                            if (MCP_Method == "Orth. Regression")
                            {
                                if (thisConc[2] > 3) // if there are three or fewer concurrent data points in bin then use a slope = Avg Target/Avg Ref WS
                                    thisSlope = CalcOrthoSlope(var_x, var_y, covar_xy);
                                else if (thisConc[2] > 0)
                                    thisSlope = avgTarg / avgRef;
                                else
                                    thisSlope = 1;
                            }
                            else
                            {
                                if (thisConc[2] > 3)
                                    thisSlope = CalcVarianceRatioSlope(var_x, var_y);
                                else if (thisConc[2] > 0)
                                    thisSlope = avgTarg / avgRef;
                                else
                                    thisSlope = 1;
                            }

                            // limit slope to +/- 5 (?)
                            if (Math.Abs(thisSlope) > 5)
                                thisSlope = avgTarg / avgRef;

                            if (thisSlope > 5) thisSlope = 5;
                            This_Int = stat.CalcIntercept(avgTarg, thisSlope, avgRef);

                            if (Use_All_Data == true)
                            {
                                if (MCP_Method == "Orth. Regression")
                                {
                                    MCP_Ortho.slope[i, j, k] = thisSlope;
                                    MCP_Ortho.intercept[i, j, k] = This_Int;
                                    MCP_Ortho.R_sq[i, j, k] = stat.CalcR_Sqr(covar_xy, var_x, var_y);
                                }
                                else // if more linear models are added, will need to add another else if
                                {
                                    MCP_Varrat.slope[i, j, k] = thisSlope;
                                    MCP_Varrat.intercept[i, j, k] = This_Int;
                                    MCP_Varrat.R_sq[i, j, k] = stat.CalcR_Sqr(covar_xy, var_x, var_y);
                                }
                            }

                            avgRef = stat.CalcAvgWS(refData, refStart, refEnd, minWD, maxWD, thisTOD, thisSeason, metList);

                            double thisWS = avgRef * thisSlope + This_Int;
                            if (thisWS < 0)
                                thisWS = 0;

                            if (Double.IsNaN(thisSlope) == false) LT_WS_Est = LT_WS_Est + thisWS * ((double)sectorCount / (double)totalCount);
                        }

            }
            else if (MCP_Method == "Method of Bins") // Method of Bins
            {

                MCP_Bins.binAvgSD_Cnt = new Bin_Object[numWS, numWD + 1];

                foreach (Concurrent_data These_Conc in concData)
                {
                    int WS_Ind = Get_WS_ind(These_Conc.refWS, WS_bin);
                    WD_Ind = Get_WD_ind(These_Conc.refWD);

                    // Directional ratios
                    MCP_Bins.binAvgSD_Cnt[WS_Ind, WD_Ind].avgWS_Ratio = MCP_Bins.binAvgSD_Cnt[WS_Ind, WD_Ind].avgWS_Ratio + These_Conc.targetWS / These_Conc.refWS;
                    MCP_Bins.binAvgSD_Cnt[WS_Ind, WD_Ind].SD_WS_Ratio = MCP_Bins.binAvgSD_Cnt[WS_Ind, WD_Ind].SD_WS_Ratio + Math.Pow(These_Conc.targetWS / These_Conc.refWS, 2);
                    MCP_Bins.binAvgSD_Cnt[WS_Ind, WD_Ind].count++;

                    // Overall ratios (all WD)
                    MCP_Bins.binAvgSD_Cnt[WS_Ind, numWD].avgWS_Ratio = MCP_Bins.binAvgSD_Cnt[WS_Ind, numWD].avgWS_Ratio + These_Conc.targetWS / These_Conc.refWS;
                    MCP_Bins.binAvgSD_Cnt[WS_Ind, numWD].SD_WS_Ratio = MCP_Bins.binAvgSD_Cnt[WS_Ind, numWD].SD_WS_Ratio + Math.Pow(These_Conc.targetWS / These_Conc.refWS, 2);
                    MCP_Bins.binAvgSD_Cnt[WS_Ind, numWD].count++;

                }

                for (int i = 0; i < numWS; i++)
                    for (int j = 0; j <= numWD; j++)
                    {
                        if (MCP_Bins.binAvgSD_Cnt[i, j].count > 0)
                        {
                            MCP_Bins.binAvgSD_Cnt[i, j].avgWS_Ratio = MCP_Bins.binAvgSD_Cnt[i, j].avgWS_Ratio / MCP_Bins.binAvgSD_Cnt[i, j].count;
                            MCP_Bins.binAvgSD_Cnt[i, j].SD_WS_Ratio = MCP_Bins.binAvgSD_Cnt[i, j].SD_WS_Ratio / MCP_Bins.binAvgSD_Cnt[i, j].count -
                                    Math.Pow(MCP_Bins.binAvgSD_Cnt[i, j].avgWS_Ratio, 2);
                        }
                    }

            }                                
                        
            gotMCP_Est = true;
            LT_WS_Ests = GenerateLT_WS_TS(thisInst, thisMet, MCP_Method);

            if (MCP_Method == "Method of Bins" || MCP_Method == "Matrix")
                LT_WS_Est = stat.CalcAvgWS(LT_WS_Ests, refStart, refEnd, 0, 360, Met.TOD.All, Met.Season.All, metList);

            return LT_WS_Est;
        }

        public Site_data[] GenerateLT_WS_TS(Continuum thisInst, Met thisMet, string MCP_Method)
        { 
            // Estimate time series at met site using MERRA2 data and selected MCP type
            
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
            MERRA thisMERRA = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);

            if (gotMCP_Est == false && MCP_Method != "Matrix")
                thisInst.metList.RunMCP(ref thisMet, thisMERRA, thisInst, MCP_Method); // Reads in MERRA data and extrapolated filtered data and runs MCP using settings on form                    

            if (thisMet.mcp.refData.Length == 0)
                thisMet.mcp.GetRefData(thisMERRA, ref thisMet, thisInst);                       

            Site_data[] LT_WS_Est_TS = new Site_data[thisMet.mcp.refData.Length];

            Random thisRandom = GetRandomNumber();
            double lastWS = 0;
            
            double WS_bin = Get_WS_width_for_MCP();
            int numWS = (int)(thisInst.metList.numWS / WS_bin);

            for (int i = 0; i < thisMet.mcp.refData.Length; i++)
            {
                int thisWD_Ind = thisMet.mcp.Get_WD_ind(thisMet.mcp.refData[i].thisWD);
                int WS_Ind = thisMet.mcp.Get_WS_ind(thisMet.mcp.refData[i].thisWS, WS_bin);

                Met.TOD thisTOD = thisInst.metList.GetTOD(thisMet.mcp.refData[i].thisDate);
                Met.Season thisSeason = thisInst.metList.GetSeason(thisMet.mcp.refData[i].thisDate);

                int timeInd = thisMet.GetTOD_Ind(numTODs, thisTOD); 
                int seasonInd = thisMet.GetSeasonInd(numSeasons, thisSeason);

                LT_WS_Est_TS[i].thisDate = thisMet.mcp.refData[i].thisDate;
                LT_WS_Est_TS[i].thisWD = thisMet.mcp.refData[i].thisWD;

                if (MCP_Method == "Orth. Regression")
                {
                    double thisWS = thisMet.mcp.refData[i].thisWS * thisMet.mcp.MCP_Ortho.slope[thisWD_Ind, timeInd, seasonInd] +
                        thisMet.mcp.MCP_Ortho.intercept[thisWD_Ind, timeInd, seasonInd];

                    if (thisWS < 0)
                        thisWS = 0;

                    LT_WS_Est_TS[i].thisWS = thisWS;
                }
                else if (MCP_Method == "Variance Ratio")
                {                   

                    double thisWS = thisMet.mcp.refData[i].thisWS * thisMet.mcp.MCP_Varrat.slope[thisWD_Ind, timeInd, seasonInd] +
                        thisMet.mcp.MCP_Varrat.intercept[thisWD_Ind, timeInd, seasonInd];

                    if (thisWS < 0)
                        thisWS = 0;

                    LT_WS_Est_TS[i].thisWS = thisWS;

                }
                else if (MCP_Method == "Method of Bins")
                {                    
                    if (thisMet.mcp.MCP_Bins.binAvgSD_Cnt[WS_Ind, thisWD_Ind].avgWS_Ratio > 0)
                        LT_WS_Est_TS[i].thisWS = thisMet.mcp.refData[i].thisWS * thisMet.mcp.MCP_Bins.binAvgSD_Cnt[WS_Ind, thisWD_Ind].avgWS_Ratio;
                    else
                    {
                        // there was no data for this bin so find the two closest ratios and use average of two
                        double avgRatio = 0;
                        int avgRatioCount = 0;
                        int minusInd = WS_Ind;
                        int plusInd = WS_Ind;
                        int countWhile = 0;

                        while (avgRatioCount < 2 && (minusInd != 0 || plusInd != numWS))
                        {
                            if (minusInd > 0) minusInd--;
                            if (plusInd < (numWS - 1)) plusInd++;

                            if (thisMet.mcp.MCP_Bins.binAvgSD_Cnt[minusInd, thisWD_Ind].avgWS_Ratio > 0)
                            {
                                avgRatio = avgRatio + thisMet.mcp.MCP_Bins.binAvgSD_Cnt[minusInd, thisWD_Ind].avgWS_Ratio;
                                avgRatioCount++;
                            }

                            if (thisMet.mcp.MCP_Bins.binAvgSD_Cnt[plusInd, thisWD_Ind].avgWS_Ratio > 0)
                            {
                                avgRatio = avgRatio + thisMet.mcp.MCP_Bins.binAvgSD_Cnt[plusInd, thisWD_Ind].avgWS_Ratio;
                                avgRatioCount++;
                            }
                            countWhile++;
                            if (countWhile > 30)
                            {
                                break;
                            }
                        }

                        if (avgRatioCount > 0) avgRatio = avgRatio / avgRatioCount;
                        LT_WS_Est_TS[i].thisWS = thisMet.mcp.refData[i].thisWS * avgRatio;
                    }
                    LT_WS_Est_TS[i].thisWD = thisMet.mcp.refData[i].thisWD;

                }
                else if (MCP_Method == "Matrix" && MCP_Matrix.WS_CDFs != null)
                {                    
                    CDF_Obj WS_CDF = new CDF_Obj();                    
                                        
                    // find PDF defined for this WD, hourly and temp bin
                    foreach (CDF_Obj thisCDF in MCP_Matrix.WS_CDFs)
                    {
                        if ((thisCDF.WD_Ind == thisWD_Ind) && (thisCDF.TOD == thisTOD) && (thisCDF.season == thisSeason) && (thisCDF.WS_Ind == WS_Ind))
                        {
                            WS_CDF = thisCDF;
                            break;
                        }
                    }                                      

                    CDF_Obj Combo_CDF = new CDF_Obj(); // combination of WS PDF and Last WS PDF

                    if ((lastWS != 0) && (WS_CDF.count > 1) && (lastWS_Wgt > 0))
                    {
                        
                        double[] Last_WS_CDF = GetLagWS_CDF(lastWS, WS_CDF.minWS, WS_CDF.WS_interval);

                        Combo_CDF.CDF = new double[100];
                        Combo_CDF.count = 100;
                        Combo_CDF.minWS = WS_CDF.minWS;
                        Combo_CDF.WS_interval = WS_CDF.WS_interval;

                        // combine WS_CDF with Last_WS_CDF
                        for (int j = 0; j < 100; j++)
                            if (WS_CDF.CDF[j] != 0)
                                Combo_CDF.CDF[j] = (matrixWgt * WS_CDF.CDF[j] + (Last_WS_CDF[j] * lastWS_Wgt)) / (lastWS_Wgt + matrixWgt);                                                                          

                    }
                    else
                        Combo_CDF = WS_CDF;
                                                          
                    
                    if (Combo_CDF.count > 1)
                    {                   
                        // Generate random number from 0 to 1 and find index in CDF                        
                        double randomNum = thisRandom.NextDouble();
                        int minInd = FindCDF_Index(Combo_CDF, randomNum);
                        LT_WS_Est_TS[i].thisWS = Combo_CDF.minWS + Combo_CDF.WS_interval * minInd;
                    }                    
                    else
                    {
                        LT_WS_Est_TS[i].thisWS = Combo_CDF.minWS; // no data for this WS bin so use same WS as reference
                        
                    }

                    LT_WS_Est_TS[i].thisWD = thisMet.mcp.refData[i].thisWD;                                    
                    lastWS = LT_WS_Est_TS[i].thisWS;                                        
                }
                
            }

            
            return LT_WS_Est_TS;
        }

        
        /// <summary> Finds and returns the CDF index that corresponds to random number between 0 and 1 </summary>        
        public int FindCDF_Index(CDF_Obj thisCDF, double randomNum)
        {
            double minDiff = 100;
            int minInd = 10000;
            
            // find CDF index that most closely corresponds to random number
            for (int m = 0; m < 100; m++)
            {
                double thisDiff = Math.Abs(thisCDF.CDF[m] - randomNum);
                if (thisDiff < minDiff) 
                {
                    minInd = m;
                    minDiff = thisDiff;
                }                
                else if (thisDiff > minDiff)
                    break;
            }

            if (minInd == 10000)
                minInd = 99;

            return minInd;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// When creating a CDF from discrete data, plateaus are present in CDF, this function
        /// interpolates between points so the estimated WS ramps up with random number instead of being
        /// step-wise.
        /// </summary>
        ///
        /// <remarks>   NOT CURRENTLY USED. NOT TESTED. </remarks>
        ///
        /// <param name="thisCDF"> this cdf. </param>
        ///
        /// <returns>   Interpolated CDF as array of type double. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double[] InterpolateCDF(double[] thisCDF)
        {           
            double[] interpCDF = new double[1000];
            double lastCDF = thisCDF[0];
            int lastCDF_Ind = 0;
            double nextCDF = 0;
            int nextCDF_Ind = 0;
            int thisWS_Ind = 1;

            while (thisWS_Ind < 1000)
            {
                if (thisCDF[thisWS_Ind] == lastCDF)
                {
                    while ((thisCDF[thisWS_Ind] == lastCDF) && (thisWS_Ind < 999))
                        thisWS_Ind++;

                    nextCDF_Ind = thisWS_Ind;
                    nextCDF = thisCDF[thisWS_Ind];

                    for (int j = lastCDF_Ind; j <= nextCDF_Ind; j++)
                        interpCDF[j] = (double)(j - lastCDF_Ind) / (nextCDF_Ind - lastCDF_Ind) * (thisCDF[nextCDF_Ind] - thisCDF[lastCDF_Ind]) + thisCDF[lastCDF_Ind];

                    lastCDF = thisCDF[thisWS_Ind];
                    lastCDF_Ind = thisWS_Ind;
                    thisWS_Ind++;

                }
                else
                {
                    interpCDF[thisWS_Ind] = thisCDF[thisWS_Ind];
                    lastCDF = thisCDF[thisWS_Ind];
                    lastCDF_Ind = thisWS_Ind;
                    thisWS_Ind++;
                }
            }
            
            return interpCDF;
        }

        /// <summary> Calculates and returns a CDF corresponding to the last WS estimate and the standard deviation
        /// of change in WS from one hour to next. </summary>
        public double[] GetLagWS_CDF(double lastWS, double CDF_MinWS, double CDF_WS_Int)
        {             
            double[] lagWS_CDF = new double[100];
            
            int WS_Ind = Get_WS_ind(lastWS, Get_WS_width_for_MCP());

            if (WS_Ind >= SD_WS_Lag.Length)
                WS_Ind = SD_WS_Lag.Length - 1;          
      
            double thisWS = 0;
            double SD_Sqr = Math.Pow(SD_WS_Lag[WS_Ind], 2); // Square of standard deviation of WS change at bin WS_Ind
            double thisPDF; // PDF calculated at thisWS
            double lastPDF = 0; // PDF calculated at last WS
            double midPDF = 0; // Average of thisPDF and lastPDF. Cumulative distribution is an integration, using trapezoidal integration so use avg PDF value                        

            while (thisWS <= CDF_MinWS) // Sum up CDF from thisWS = 0 to CDF_MinWS
            {                
                thisPDF = 1 / Math.Pow(2 * SD_Sqr * Math.PI, 0.5) * Math.Exp(-Math.Pow((thisWS - lastWS), 2) / (2 * SD_Sqr));
                midPDF = (thisPDF + lastPDF) / 2;
                                
                lagWS_CDF[0] = lagWS_CDF[0] + CDF_WS_Int * midPDF;
                lastPDF = thisPDF;
                thisWS = thisWS + CDF_WS_Int;
            }
                                               
            for (int i = 1; i < 100; i++)
            {
                thisWS = CDF_MinWS + i * CDF_WS_Int;                
                thisPDF = 1 / Math.Pow(2 * SD_Sqr * Math.PI, 0.5) * Math.Exp(-Math.Pow((thisWS - lastWS), 2) / (2 * SD_Sqr));
                midPDF = (thisPDF + lastPDF) / 2;
                
                lagWS_CDF[i] = lagWS_CDF[i - 1] + CDF_WS_Int * midPDF;
                lastPDF = thisPDF;
            }

            // normalize to add to 1.0
            for (int i = 0; i < 100; i++)
                lagWS_CDF[i] = lagWS_CDF[i] / lagWS_CDF[99];

            return lagWS_CDF;
        }
                
        /// <summary> Calculates the standard deviation of change in wind speed (from one hour to the next) as a function of 
        /// wind speed at target site for specified concurrent period. </summary>  
        public void FindSD_ChangeInWS()
        {    
            DateTime lastRecord = DateTime.Today;
            DateTime nextRecord = lastRecord.AddHours(1);
                      
            int numWS = (int)(30 / Get_WS_width_for_MCP());            
            SD_WS_Lag = new double[numWS];
            double[] thisAvg = new double[numWS];
            double lastWS = 0;
            int[] thisCount = new int[numWS];

            foreach (Concurrent_data thisConc in concData)
            {           
                int WS_Ind = Get_WS_ind(thisConc.targetWS, Get_WS_width_for_MCP());
                
                if ((lastWS != 0) && (thisConc.targetWS > 0) && (nextRecord == thisConc.thisDate) && (WS_Ind < numWS))
                {
                    double thisDiff = thisConc.targetWS - lastWS;
                    thisAvg[WS_Ind] = thisAvg[WS_Ind] + thisDiff;
                    SD_WS_Lag[WS_Ind] = SD_WS_Lag[WS_Ind] + Math.Pow(thisDiff,2);
                    lastWS = thisConc.targetWS;
                    thisCount[WS_Ind]++;                    
                }
                
                lastWS = thisConc.targetWS;
                lastRecord = thisConc.thisDate;
                nextRecord = lastRecord.AddHours(1);
            }
                        
            for (int i = 0; i < numWS; i++)               
                {
                    if (thisCount[i] > 1)
                    {
                        thisAvg[i] = thisAvg[i] / thisCount[i];
                        SD_WS_Lag[i] = Math.Pow(SD_WS_Lag[i] / thisCount[i] - Math.Pow(thisAvg[i], 2), 0.5);
                    }
                    else
                        SD_WS_Lag[i] = 1; // If no data, assume a SD deviation of 1 m/s
            }                     
           
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a random number between 0 and 1. </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ///
        /// <returns>   Random number. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Random GetRandomNumber()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            return rnd;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Calculates the slope of the orthogonal regression. </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. </remarks>
        ///
        /// <param name="var_x">    Variance of concurrent reference wind speed. </param>
        /// <param name="var_y">    Variance of concurrent target wind speed. </param>
        /// <param name="covar_xy"> Covariance of concurrent reference and target wind speed. </param>
        ///
        /// <returns>   The calculated slope of orthogonal regression. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double CalcOrthoSlope(double var_x, double var_y, double covar_xy)
        {    
            double slope = (var_y - var_x + Math.Pow((Math.Pow((var_y - var_x), 2) + 4 * Math.Pow(covar_xy, 2)), 0.5)) / (2 * covar_xy);
            
            return slope;
        }
                
        /// <summary> Calculates the slope of variance ratio method where slope is defined as ratio of standard
        /// deviation of reference and target concurrent wind speeds. </summary>        
        public double CalcVarianceRatioSlope(double var_x, double var_y)
        {            
            double slope = 0;
            
            if (var_x > 0)            
                slope = Math.Pow(var_y, 0.5) / Math.Pow(var_x, 0.5);
            else            
                slope = 0;
            
            return slope;
        }        

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Resets the export dates with start/end date of reference dataset. </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ResetExportDates(Continuum thisInst)
        {            
            thisInst.dateMCPExportStart.Value = refStart;
            thisInst.dateMCPExportEnd.Value = refEnd;            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Find start and end dates of full concurrent period and updates the form dates.
        /// </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Set_Conc_Dates_On_Form(Continuum thisInst)
        {            
            if (gotTarg != true || gotRef != true)
                return;
                   
            thisInst.dateConcurrentStart.Value = concStart;
            thisInst.dateConcurrentEnd.Value = concEnd;
        }               
                

        

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Resets the MCP analysis and clears all calculated onjects. </summary>
        ///
        /// <remarks>
        /// Liz, 5/16/2017. This is different from New_MCP in that it clears the calculated values but it
        /// keeps the user-specified number of bins, weights.
        /// </remarks>
        ///
        /// <param name="All_or_Matrix_or_Bin"> If 'All', clears entire analysis, 'Matrix_and_Bins' clear
        ///                                     Matrix-LastWS and Method_of_Bins objects only, 'Matrix'
        ///                                     clears only Matrix-LasatWS object. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ResetMCP(string All_or_Matrix_or_Bin, MetCollection metList)
        {
            if (All_or_Matrix_or_Bin == "All")
            {
                concData = new Concurrent_data[0];
                MCP_Ortho.Clear();
                MCP_Bins.Clear();
                MCP_Varrat.Clear();
                MCP_Matrix.Clear();

                uncertOrtho = new MCP_Uncert[0];
                uncertBins = new MCP_Uncert[0];
                uncertVarrat = new MCP_Uncert[0];
                uncertMatrix = new MCP_Uncert[0];
                                
                gotConc = false;
                gotMCP_Est = false;
                
                numWD = metList.numWD;
                numTODs = metList.numTOD;
                numSeasons = metList.numSeason;
                WS_BinWidth = 1;
                                
            }
            else if (All_or_Matrix_or_Bin == "Matrix_and_Bins") // this is called if the WS bin width is changed since it only affects Matrix and Method of Bins
            {
                MCP_Bins.Clear();
                MCP_Matrix.Clear();

                uncertBins = new MCP_Uncert[0];
                uncertMatrix = new MCP_Uncert[0];
            }
            else if (All_or_Matrix_or_Bin == "Matrix") // this is called if the Matrix or LastWS weights are changed
            {
                MCP_Matrix.Clear();
                uncertMatrix = new MCP_Uncert[0];
            }                        
        }
        

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event handler. Called when 'Convert to Hourly' button is clicked.  Reads in 10-minute time
        /// series WS and WD data, converts to hourly data and saves .CSV file.
        /// </summary>
        ///
        /// <remarks>
        /// Liz, 5/16/2017. Tested outside of Visual Studio by comparing to excel VBA tool output.
        /// </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Site_data[] ConvertToHourly(Site_data[] tenMinData)
        {
            // Prompt user to find reference data file            
            Site_data[] hourlyData = new Site_data[0];

            try
            {
                int numTenMin = tenMinData.Length;
                int numHourly = (int)Math.Round(numTenMin / 6.0, 0);
                Array.Resize(ref hourlyData, numHourly);
            }
            catch
            {
                return hourlyData;
            }
                       
            DateTime lastDate = DateTime.Today;
            int hourlyCount = 0;

            double[] WS_Arr = null;
            double[] WD_Arr = null;
            double avgWS = 0;
            double avgWD = 0;
            int avgCount = 0;                     

            if (tenMinData != null)
            {              

                foreach(Site_data thisData in tenMinData)
                {                    

                    if (lastDate == DateTime.Today)
                        lastDate = thisData.thisDate;
                    
                    if (thisData.thisDate.Hour == lastDate.Hour)
                    {
                        if (thisData.thisWS != -999 && thisData.thisWD != -999)
                        {
                            avgCount++;
                            Array.Resize(ref WS_Arr, avgCount);
                            Array.Resize(ref WD_Arr, avgCount);

                            WS_Arr[avgCount - 1] = thisData.thisWS;
                            WD_Arr[avgCount - 1] = thisData.thisWD;
                        }
                        
                        lastDate = thisData.thisDate;
                    }
                    else if (avgCount >= 1) // need at least one record per hour
                    {
                        // calculate avg WS
                        for (int i = 0; i < avgCount; i++)
                            avgWS = avgWS + WS_Arr[i];

                        // first figure out if there is cross-over
                        double max_diff = 0;
                        for (int i = 0; i < avgCount - 1; i++)
                        {
                            double this_diff = Math.Abs(WD_Arr[i + 1] - WD_Arr[i]);
                            if (this_diff > max_diff)
                                max_diff = this_diff;
                        }

                        if (max_diff > 270)
                        {
                            for (int i = 0; i < avgCount; i++)
                                if (WD_Arr[i] > 270) WD_Arr[i] = WD_Arr[i] - 360;
                        }

                        // calculate avg WD
                        for (int i = 0; i < avgCount; i++)
                            avgWD = avgWD + WD_Arr[i];

                        avgWS = avgWS / avgCount;
                        avgWD = avgWD / avgCount;

                        if (avgWD < 0) avgWD = avgWD + 360;

                        DateTime hourDate = lastDate;
                        int thisYear = hourDate.Year;
                        int thisMonth = hourDate.Month;
                        int thisDay = hourDate.Day;
                        int thisNewHour = hourDate.Hour;

                        DateTime newHourDate = new DateTime(thisYear, thisMonth, thisDay);
                        TimeSpan ts = new TimeSpan(thisNewHour, 0, 0);
                        newHourDate = newHourDate.Date + ts;

                        if (hourlyCount >= hourlyData.Length)
                            Array.Resize(ref hourlyData, hourlyCount + 1);

                        hourlyData[hourlyCount].thisDate = newHourDate;
                        hourlyData[hourlyCount].thisWS = avgWS;
                        hourlyData[hourlyCount].thisWD = avgWD;
                        hourlyCount++;                       

                        avgCount = 0;
                        avgWS = 0;
                        avgWD = 0;
                        WS_Arr = null;
                        WD_Arr = null;

                        if (thisData.thisWS != -999 && thisData.thisWD != -999)
                        {
                            avgCount++;
                            Array.Resize(ref WS_Arr, avgCount);
                            Array.Resize(ref WD_Arr, avgCount);

                            WS_Arr[avgCount - 1] = thisData.thisWS;
                            WD_Arr[avgCount - 1] = thisData.thisWD;
                        }
                        
                        lastDate = thisData.thisDate;
                    }
                    else
                    {
                        avgCount = 0;
                        avgWS = 0;
                        avgWD = 0;
                        WS_Arr = null;
                        WD_Arr = null;

                        if (thisData.thisWS != -999 && thisData.thisWD != -999)
                        {
                            avgCount++;
                            Array.Resize(ref WS_Arr, avgCount);
                            Array.Resize(ref WD_Arr, avgCount);

                            WS_Arr[avgCount - 1] = thisData.thisWS;
                            WD_Arr[avgCount - 1] = thisData.thisWD;
                        }
                        lastDate = thisData.thisDate;
                    }
                }      
                
            }

            Array.Resize(ref hourlyData, hourlyCount);
            return hourlyData;
        }            
       

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Executes the MCP uncertainty analysis. </summary>
        ///
        /// <remarks>   Liz, 5/24/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Do_MCP_Uncertainty(Continuum thisInst, MERRA thisMERRA, Met thisMet)
        {
            int uncertStepSize = Get_Uncert_Step_Size(); // Step size (in months) that defines the next start date. 
                                                           // Default is 1 month but for large datasets, increasing this 
                                                           // helps to reduce the number of calculations. Possible choices are 1, 2, 3 or 4 month step size.

            // how many months in Conc -> Number of MCP_Uncert objects to create
            int numObj = ((concEnd.Year - concStart.Year) * 12) + concEnd.Month - concStart.Month;

            string currentMethod = thisInst.Get_MCP_Method();

            DateTime testStart = concStart;            
            DateTime testEnd = concEnd;
            DateTime origStart = concStart;

            // Copy MERRA data as the reference data
            if (refData == null)
                refData = GetRefData(thisMERRA, ref thisMet, thisInst);

            // Get sector count to be used within loops
            Find_Sector_Counts(thisInst.metList);

            // Get target data
            if (targetData == null)
                targetData = GetTargetData(thisInst.modeledHeight, thisMet); 
                      
            thisMet.mcp.FindConcurrentData(thisMet.metData.startDate, thisMet.metData.endDate);
            // Find concurrent data to be referenced in DoMCP function            
            
            // For every MCP_Uncert, for every possible conc window, construct Uncert structures
            if (currentMethod == "Orth. Regression")
            {
                Array.Resize(ref uncertOrtho, numObj);

                for (int m = 0; m < numObj; m++)
                {
                    uncertOrtho[m].WSize = m + 1;
                    uncertOrtho[m].NWindows = (numObj - m) / uncertStepSize;

                    Array.Resize(ref uncertOrtho[m].LT_Ests, uncertOrtho[m].NWindows);
                    Array.Resize(ref uncertOrtho[m].start, uncertOrtho[m].NWindows);
                    Array.Resize(ref uncertOrtho[m].end, uncertOrtho[m].NWindows);

                    testStart = origStart;

                    for (int i = 0; i < uncertOrtho[m].NWindows; i++)
                    {
                        // Initialize First Test Start at Concurrent Start Date at beginning of each iteration                        
                        testEnd = testStart.AddMonths(m + 1);

                        uncertOrtho[m].LT_Ests[i] = DoMCP(testStart, testEnd, false, currentMethod, thisInst, thisMet);
                        uncertOrtho[m].start[i] = testStart;
                        uncertOrtho[m].end[i] = testEnd;

                        // Increment start date by uncertStepSize (default = 1 month but may be up to 4 months)
                        testStart = testStart.AddMonths(uncertStepSize);
                    }
                    // Find Statistics for analysis
                    CalcAvgSD_Uncert(ref uncertOrtho[m]);
                }
                thisInst.btnMCP_Uncert.Enabled = false;
            }
            else if (currentMethod == "Method of Bins")
            {
                Array.Resize(ref uncertBins, numObj);

                for (int m = 0; m < numObj; m++)
                {
                    uncertBins[m].WSize = m + 1;
                    uncertBins[m].NWindows = (numObj - m) / uncertStepSize;

                    Array.Resize(ref uncertBins[m].LT_Ests, uncertBins[m].NWindows);
                    Array.Resize(ref uncertBins[m].start, uncertBins[m].NWindows);
                    Array.Resize(ref uncertBins[m].end, uncertBins[m].NWindows);

                    testStart = origStart;

                    for (int i = 0; i < uncertBins[m].NWindows; i++)
                    {
                        // Initialize First Test Start at Concurrent Start Date at beginning of each iteration
                        testEnd = testStart.AddMonths(m + 1);

                        uncertBins[m].LT_Ests[i] = DoMCP(testStart, testEnd, false, currentMethod, thisInst, thisMet);
                        uncertBins[m].start[i] = testStart;
                        uncertBins[m].end[i] = testEnd;

                        testStart = testStart.AddMonths(uncertStepSize);
                    }
                    // Find Statistics for analysis
                    CalcAvgSD_Uncert(ref uncertBins[m]);
                }
                thisInst.btnMCP_Uncert.Enabled = false;
            }
            else if (currentMethod == "Variance Ratio")
            {
                Array.Resize(ref uncertVarrat, numObj);

                for (int m = 0; m < numObj; m++)
                {
                    uncertVarrat[m].WSize = m + 1;
                    uncertVarrat[m].NWindows = (numObj - m) / uncertStepSize;

                    Array.Resize(ref uncertVarrat[m].LT_Ests, uncertVarrat[m].NWindows);
                    Array.Resize(ref uncertVarrat[m].start, uncertVarrat[m].NWindows);
                    Array.Resize(ref uncertVarrat[m].end, uncertVarrat[m].NWindows);

                    testStart = origStart;

                    for (int i = 0; i < uncertVarrat[m].NWindows; i++)
                    {
                        // Initialize First Test Start at Concurrent Start Date at beginning of each iteration
                        testEnd = testStart.AddMonths(m + 1);

                        uncertVarrat[m].LT_Ests[i] = DoMCP(testStart, testEnd, false, currentMethod, thisInst, thisMet);
                        uncertVarrat[m].start[i] = testStart;
                        uncertVarrat[m].end[i] = testEnd;

                        testStart = testStart.AddMonths(uncertStepSize);
                    }
                    // Find Statistics for analysis
                    CalcAvgSD_Uncert(ref uncertVarrat[m]);
                }
                thisInst.btnMCP_Uncert.Enabled = false;
            }
            else if (currentMethod == "Matrix")
            {
                Array.Resize(ref uncertMatrix, numObj);

                for (int m = 0; m < numObj; m++)
                {
                    uncertMatrix[m].WSize = m + 1;
                    uncertMatrix[m].NWindows = (numObj - m) / uncertStepSize;

                    Array.Resize(ref uncertMatrix[m].LT_Ests, uncertMatrix[m].NWindows);
                    Array.Resize(ref uncertMatrix[m].start, uncertMatrix[m].NWindows);
                    Array.Resize(ref uncertMatrix[m].end, uncertMatrix[m].NWindows);

                    testStart = origStart;

                    for (int i = 0; i < uncertMatrix[m].NWindows; i++)
                    {
                        // Initialize First Test Start at Concurrent Start Date at beginning of each iteration
                        testEnd = testStart.AddMonths(m + 1);

                        uncertMatrix[m].LT_Ests[i] = DoMCP(testStart, testEnd, false, currentMethod, thisInst, thisMet);
                        uncertMatrix[m].start[i] = testStart;
                        uncertMatrix[m].end[i] = testEnd;

                        testStart = testStart.AddMonths(uncertStepSize);
                    }
                    // Find Statistics for analysis
                    CalcAvgSD_Uncert(ref uncertMatrix[m]);
                }
                
            }  
                        
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Calculates the average and standard deviation of long-term estimates generated for an
        /// uncertainty object (i.e. certain window size, start and end dates)
        /// </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. </remarks>
        ///
        /// <param name="thisUncert">  [in,out] MCP Uncertainty object, MCP_Uncert. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void CalcAvgSD_Uncert(ref MCP_Uncert thisUncert)
        {
            double sum_x = 0;
            double var_x = 0;
            int val_length = thisUncert.LT_Ests.Length;

            if (thisUncert.LT_Ests != null)
            {
                foreach (double value in thisUncert.LT_Ests)
                    sum_x = sum_x + value;
                
                thisUncert.avg = Convert.ToSingle(sum_x / val_length);

                foreach (double value in thisUncert.LT_Ests)                
                    var_x = var_x + (Math.Pow(value - thisUncert.avg, 2) / (val_length));
                
                thisUncert.stDev = Math.Pow(var_x, 0.5);

            }
        }
         
          

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new MCP analysis and sets all fields to default values. </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. Tested outside of Visual Studio. </remarks>
        ///
        /// <param name="Clear_Ref">    True to clear reference. </param>
        /// <param name="Clear_Target"> True to clear target. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void New_MCP(bool Clear_Ref, bool Clear_Target, Continuum thisInst)
        {
            // Creates a MCP analysis

            if (Clear_Ref == true)
            {
                gotRef = false;
                refData = new Site_data[0];
            }                
            
            if (Clear_Target == true)
            {
                gotTarg = false;
                targetData = new Site_data[0];
            }               
                        
            concData = new Concurrent_data[0];
            gotConc = false;

            exportStart = thisInst.dateMERRAStart.Value;
            exportEnd = thisInst.dateMERRAEnd.Value;

            numWD = thisInst.GetNumWD();
            numTODs = thisInst.metList.numTOD;
            numSeasons = thisInst.metList.numSeason;

            WS_BinWidth = thisInst.metList.mcpWS_BinWidth;
            matrixWgt = thisInst.metList.mcpMatrixWgt;
            lastWS_Wgt = thisInst.metList.mcpLastWS_Wgt;                              

            MCP_Ortho.Clear();
            MCP_Bins.Clear();
            MCP_Varrat.Clear();
            MCP_Matrix.Clear();

            uncertStepSize = thisInst.metList.mcpUncertStepSize;            
            uncertOrtho = new MCP_Uncert[0];
            uncertBins = new MCP_Uncert[0];
            uncertVarrat = new MCP_Uncert[0];
            uncertMatrix = new MCP_Uncert[0];                  

            Is_Newly_Opened_File = false;   // this is set to true and then set to false to avoid messages that appear to ask the user 
                                            // if they are sure that they want to clear the calculations
            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets data counts for every WD, hourly and temperature bin in reference dataset.
        /// </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Find_Sector_Counts(MetCollection metList)
        {
            int Total_comb = numWD * numTODs * numSeasons;
            int counter = 0;

            Array.Resize(ref sectors,Total_comb);

            for (int i = 0; i < numWD; i++)
                for (int j = 0; j < numTODs; j++)
                    for (int k = 0; k < numSeasons; k++)
                    {
                        sectors[counter].WD = i;

                        if (j == 0 && numTODs == 1)
                            sectors[counter].TOD = Met.TOD.All;
                        else if (j == 0)
                            sectors[counter].TOD = Met.TOD.Day;
                        else if (j == 1)
                            sectors[counter].TOD = Met.TOD.Night;

                        if (k == 0 && numSeasons == 1)
                            sectors[counter].season = Met.Season.All;
                        else if (k == 0)
                            sectors[counter].season = Met.Season.Winter;
                        else if (k == 1)
                            sectors[counter].season = Met.Season.Spring;
                        else if (k == 2)
                            sectors[counter].season = Met.Season.Summer;
                        else if (k == 3)
                            sectors[counter].season = Met.Season.Fall;

                        sectors[counter].count = stat.GetDataCount(refData, exportStart, exportEnd, i, sectors[counter].TOD, sectors[counter].season, metList, false);
                        counter = counter + 1;
                    }
        }  


        public Site_data[] GetTargetData(double height, Met thisMet)
        {
            // Reads in Target data for MCP from Filtered Met Data 
            
            Met_Data_Filter.Sim_TS targetDataTenMin = thisMet.metData.GetSimulatedTimeSeries(height);
            int tenMinCount = targetDataTenMin.WS_WD_data.Length;
            // Convert to hourly data
            MCP.Site_data[] tenMinData = new MCP.Site_data[tenMinCount];

            for (int i = 0; i < tenMinCount; i++)
            {
                tenMinData[i].thisDate = targetDataTenMin.WS_WD_data[i].timeStamp;
                tenMinData[i].thisWS = targetDataTenMin.WS_WD_data[i].WS;
                tenMinData[i].thisWD = targetDataTenMin.WS_WD_data[i].WD;
            }

            MCP.Site_data[] targetData = thisMet.mcp.ConvertToHourly(tenMinData);
            thisMet.mcp.targetStart = targetData[0].thisDate;
            thisMet.mcp.targetEnd = targetData[targetData.Length - 1].thisDate;
            thisMet.mcp.gotTarg = true;

            return targetData;
        }

        public int GetTargetDataLength(DateTime thisStart, DateTime thisEnd)
        {
            int numDays = 0;
            DateTime thisDate = targetStart;
            
            while (thisDate < thisStart)
                thisDate.AddDays(1);

            while (thisDate <= thisEnd)
            {
                numDays++;
                thisDate = thisDate.AddDays(1);
            }


            return numDays;
        }             
              

        public Site_data[] GetRefData(MERRA thisMERRA, ref Met thisMet, Continuum thisInst)
        {
            if (thisMERRA.interpData.TS_Data == null)
            {
                thisMERRA.GetMERRADataFromDB(thisInst);
                thisMERRA.GetInterpData(thisInst.UTM_conversions);
            }

            if (thisMERRA.interpData.TS_Data.Length == 0)
            {
                thisMERRA.GetMERRADataFromDB(thisInst);
                thisMERRA.GetInterpData(thisInst.UTM_conversions);
            }

            int refDataLen = thisMERRA.interpData.TS_Data.Length;
            refData = new MCP.Site_data[refDataLen];
            thisMet.mcp.refStart = thisMERRA.interpData.TS_Data[0].ThisDate;
            thisMet.mcp.refEnd = thisMERRA.interpData.TS_Data[refDataLen - 1].ThisDate;
            thisMet.mcp.gotRef = true;

            for (int i = 0; i < refDataLen; i++)
            {
                refData[i].thisDate = thisMERRA.interpData.TS_Data[i].ThisDate;
                refData[i].thisWS = thisMERRA.interpData.TS_Data[i].WS50m;
                refData[i].thisWD = thisMERRA.interpData.TS_Data[i].WD50m;
            }

            return refData;
        }
        
        
        
    }
}



