////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Stats.cs
//
// summary:	Implements the statistics class which calculates average, variance, covariance, etc.
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuumNS
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary> Statistics class used in MCP tool. </summary>
    ///
    /// <remarks>   Liz, 5/26/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable()]
    public class Stats
    {
        /// <summary>
        /// Calculates and returns the average wind speed for specified wind speed range, time interval range, 
        /// wind direction range, time of day, and season
        /// </summary>        
        public double CalcAvgWS(MCP.Site_data[] site, DateTime startTime, DateTime endTime, double minWD, double maxWD,
            Met.TOD TOD, Met.Season season, MetCollection metList)
        {
            double avgWS = 0;
            int avgCount = 0;
            
            foreach (MCP.Site_data thisSite in site)
            {
                if (thisSite.thisDate >= startTime && thisSite.thisDate <= endTime)
                {
                    Met.TOD thisTOD = metList.GetTOD(thisSite.thisDate);
                    Met.Season thisSeason = metList.GetSeason(thisSite.thisDate);
                    
                    if (TOD == thisTOD && thisSeason == season)
                    {
                        if (((maxWD > minWD) && (thisSite.thisWD >= minWD && thisSite.thisWD <= maxWD)) ||
                                ((maxWD < minWD) && (thisSite.thisWD >= minWD || thisSite.thisWD <= maxWD)))
                        {
                            avgWS = avgWS + thisSite.thisWS;
                            avgCount = avgCount + 1;
                        }                        
                    }
                }                
            }

            if (avgCount > 0)
                avgWS = avgWS / avgCount;

            return avgWS;
        }

        
        /// <summary>  Returns the count of Site_data for specified start/end time, WD bounds, time of day bin and season bin. </summary>
        public int GetDataCount(MCP.Site_data[] site, DateTime startTime, DateTime endTime, int WD_index, Met.TOD TOD, Met.Season season, 
            MetCollection metList, bool getAll)
        {          
            int avgCount = 0;
                                    
            foreach (MCP.Site_data thisSite in site)
                if (thisSite.thisDate >= startTime && thisSite.thisDate <= endTime )
                {
                    if (getAll == true)                    
                        avgCount++;                    
                    else
                    {                        
                        int thisWD_Ind = metList.GetWD_Ind(thisSite.thisWD);
                        Met.TOD thisTOD = metList.GetTOD(thisSite.thisDate);
                        Met.Season thisSeason = metList.GetSeason(thisSite.thisDate);

                        if ((thisWD_Ind == WD_index) && (thisTOD == TOD) && (thisSeason == season))
                            avgCount++;
                    }                                    
                }            

            return avgCount;
        }

        
        /// <summary>  Calculates and returns the variance of vals[] </summary> 
        public double CalcVariance(double[] vals)
        {           
            double variance = 0;
            double sum_x = 0;                       

            if (vals != null)
            {
                int val_length = vals.Length;

                foreach (double value in vals)
                    sum_x = sum_x + value;

                if (val_length > 0)
                {
                    double mean = sum_x / val_length;

                    foreach (double value in vals)
                        variance = variance + (Math.Pow(value - mean, 2) / (val_length));
                }                
            }

            return variance;
        }

        
        /// <summary>   Calculates and returns the covariance between x_vals and y_vals. </summary>        
        public double CalcCovariance(double[] x_vals, double[] y_vals)
        {            

            double covar = 0;
            double sum_XY = 0;
            double sum_x = 0;
            double sum_y = 0;
            
            if ((x_vals != null) && (y_vals != null))
                if (x_vals.Length == y_vals.Length)
                {
                    for (int i = 0; i < x_vals.Length; i++)                    
                        sum_x = sum_x + x_vals[i];                    

                    double mean_x = sum_x / x_vals.Length;

                    for (int i = 0; i < x_vals.Length; i++)                    
                        sum_y = sum_y + y_vals[i];                    

                    double mean_y = sum_y / y_vals.Length;

                    for (int i = 0; i < x_vals.Length; i++)                    
                        sum_XY = sum_XY + (x_vals[i] - mean_x) * (y_vals[i] - mean_y);                    

                    covar = sum_XY / x_vals.Length;
                }

            return covar;
        }
                
        /// <summary>   Calculates the coefficient of determination, R-squared. </summary>        
        public double CalcR_Sqr(double covar_xy, double var_x, double var_y)
        {
            double R_sqr = Math.Pow(covar_xy / Math.Pow(var_x, 0.5) / Math.Pow(var_y, 0.5), 2);
            return R_sqr;
        }
                
        /// <summary>   Calculates the intercept based on mean x, mean y, and slope. </summary> 
        public double CalcIntercept(double avgY, double slope, double avgX)
        {
            double intercept = avgY - slope * avgX;
            return intercept;
        }
}
}
