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

        public double CalcAvgWS(MCP.Site_data[] site, double minWS, double maxWS, DateTime startTime, DateTime endTime, double minWD, double maxWD,
            Met.TOD thisTOD, Met.Season thisSeason, MCP thisMCP)
        {

            double avgWS = 0;
            int avgCount = 0;
            MetCollection metList = new MetCollection();

            foreach (MCP.Site_data thisSite in site)
            {
                if (thisTOD == Met.TOD.All || (thisSite.thisDate.Hour >= metList.dayStartHour && thisSite.thisDate.Hour <= metList.dayEndHour && thisTOD == Met.TOD.Day) 
                    || ((thisSite.thisDate.Hour < metList.dayStartHour || thisSite.thisDate.Hour > metList.dayEndHour) && thisTOD == Met.TOD.Night))
                {
                    if (maxWD > minWD)
                    {
                        if (thisSite.thisWD >= minWD && thisSite.thisWD <= maxWD)
                        {
                            avgWS = avgWS + thisSite.thisWS;
                            avgCount = avgCount + 1;
                        }
                    }
                    else if (thisSite.thisWD >= minWD || thisSite.thisWD <= maxWD)
                    {
                        avgWS = avgWS + thisSite.thisWS;
                        avgCount = avgCount + 1;
                    }
                }
                
            }

            if (avgCount > 0)
                avgWS = avgWS / avgCount;

            return avgWS;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>  Returns the count of Site_data for specified start/end time, WD bounds, Hourly bin
        ///             and Temp bin. </summary>
        ///
        /// <remarks>  Liz, 5/26/2017. </remarks>
        ///
        /// <param name="site">         Target or Reference Site_data. </param>
        /// <param name="startTime">   Start time. </param>
        /// <param name="endTime">     End time. </param>
        /// <param name="WD_index">     Wind direction index. </param>
        /// <param name="hourlyInd"> Hourly bin. </param>
        /// <param name="tempInd">   Temperature bin. </param>
        /// <param name="thisMCP">     MCP object. </param>
        /// <param name="getAll">      True to get count of all data. </param>
        ///
        /// <returns>   The data count. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Get_Data_Count(MCP.Site_data[] site, DateTime startTime, DateTime endTime, int WD_index, Met.TOD TOD, Met.Season season, 
            MCP thisMCP, bool getAll)
        {            
           
            int avgCount = 0;
            int thisWD_Ind = 0;
            Met.TOD thisTOD = 0;
            Met.Season thisSeason = 0;
            Met thisMet = new Met();
            MetCollection metList = new MetCollection();
            metList.numWD = thisMCP.numWD;
            metList.numTOD = thisMCP.numTODs;
            metList.numSeason = thisMCP.numSeasons;
                        
            foreach (MCP.Site_data thisSite in site)
                if (thisSite.thisDate >= startTime && thisSite.thisDate <= endTime )
                {
                    if (getAll == true)
                    {
                        avgCount++;
                    }
                    else
                    {
                        thisWD_Ind = thisMCP.Get_WD_ind(thisSite.thisWD);
                        thisTOD = metList.GetTOD(thisSite.thisDate);
                        thisSeason = metList.GetSeason(thisSite.thisDate);

                        if ((thisWD_Ind == WD_index) && (thisTOD == TOD) && (thisSeason == season))
                            avgCount++;
                    }                                    
                }            

            return avgCount;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Calculates and returns the variance of vals[]. Used to calculate R^2 and in 
        /// orthogonal regression and variance ratio </summary>
        ///
        /// <remarks>   Liz, 5/26/2017. </remarks>
        ///
        /// <param name="vals"> Array of double values. </param>
        ///
        /// <returns>   The calculated variance. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Calc_Variance(double[] vals)
        {           
            double variance = 0;
            double sum_x = 0;
            double mean = 0;
            int val_length = vals.Length;

            if (vals != null)
            {
                foreach (double value in vals)
                    sum_x = sum_x + value;
                

                mean = sum_x / val_length;

                foreach (double value in vals)
                    variance = variance + (Math.Pow(value - mean, 2) / (val_length));
                
            }

            return variance;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Calculates and returns the covariance between x_vals and y_vals. Used in orthogonal 
        ///             regression. </summary>
        ///
        /// <remarks>   Liz, 5/26/2017. </remarks>
        ///
        /// <param name="x_vals">   Array of double X values. </param>
        /// <param name="y_vals">   Array of double Y values. </param>
        ///
        /// <returns>   The calculated covariance. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Calc_Covariance(double[] x_vals, double[] y_vals)
        {            

            double covar = 0;
            double sum_XY = 0;
            double sum_x = 0;
            double sum_y = 0;
            double mean_x = 0;
            double mean_y = 0;

            if ((x_vals != null) && (y_vals != null))
                if (x_vals.Length == y_vals.Length)
                {
                    for (int i = 0; i < x_vals.Length; i++)                    
                        sum_x = sum_x + x_vals[i];                    

                    mean_x = sum_x / x_vals.Length;

                    for (int i = 0; i < x_vals.Length; i++)                    
                        sum_y = sum_y + y_vals[i];                    

                    mean_y = sum_y / y_vals.Length;

                    for (int i = 0; i < x_vals.Length; i++)                    
                        sum_XY = sum_XY + (x_vals[i] - mean_x) * (y_vals[i] - mean_y);                    

                    covar = sum_XY / x_vals.Length;
                }

            return covar;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Calculates the r sqr. </summary>
        ///
        /// <remarks>   OEE, 5/18/2017. </remarks>
        ///
        /// <param name="covar_xy"> The covar xy. </param>
        /// <param name="var_x">    The variable x coordinate. </param>
        /// <param name="var_y">    The variable y coordinate. </param>
        ///
        /// <returns>   The calculated r sqr. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Calc_R_sqr(double covar_xy, double var_x, double var_y)
        {
            double R_sqr = (double)Math.Pow(covar_xy / (double)Math.Pow(var_x, 0.5) / (double)Math.Pow(var_y, 0.5), 2);
            return R_sqr;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Calculates the intercept. </summary>
        ///
        /// <remarks>   OEE, 5/18/2017. </remarks>
        ///
        /// <param name="Avg_Y">    The average y coordinate. </param>
        /// <param name="slope">    The slope. </param>
        /// <param name="Avg_X">    The average x coordinate. </param>
        ///
        /// <returns>   The calculated intercept. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Calc_Intercept(double Avg_Y, double slope, double Avg_X)
        {
            double Intercept = Avg_Y - slope * Avg_X;
            return Intercept;
        }
}
}
