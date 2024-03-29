﻿using System;
using System.IO;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary>
    /// Exceedance class contains the defined performance factors (i.e. exceedance curves) which describe losses and uncertainty distributions.
    /// 17 Performance Factor (PF) categories with default values are provided. The user may define PFs as a normal distribution (with one or 
    /// many modes) and may define min/max limits (eg. Max 100% for availability). Or the user may import a cumulative density function from a CSV 
    /// file to represent a defined PF. The PFs are combined using a Monte Carlo approach which produces a composite PF curve. The composite PF 
    /// curve defines the P-values of the overall, combined losses.
    /// </summary>
    [Serializable()]
    public partial class Exceedance
    {
        /// <summary> List of all exceedance (i.e. performance factor) curves </summary>
        public ExceedanceCurve[] exceedCurves;
        /// <summary>  Holds the composite curve P-values for recurrence of 1 yr / 10 yrs / 20 yrs </summary>
        public Monte_Carlo compositeLoss = new Monte_Carlo();
        /// <summary> Number of Monte Carlo simulations to run </summary>
        public int numSims = 100000; 
                
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    
        /// <summary> Contains exceedance curve parameters and probability distribution. </summary>
        [Serializable()]
        public struct ExceedanceCurve
        {
            /// <summary> Upper bound of probability distribution </summary>
            public double upperBound;
            /// <summary> Lower bound of probability distribution </summary>
            public double lowerBound;
            /// <summary> Defined mode(s) that make up the overall distribution </summary>
            public Mode_Def[] modes;
            /// <summary> Name of exceedance curve </summary>
            public string exceedStr;

            /// <summary> Size (length) of probability distribution </summary>
            public int distSize;
            /// <summary> X-values of probability distribution </summary>
            public double[] xVals;
            /// <summary> Probability distribution </summary>
            public double[] probDist;
            /// <summary> Cumulative probability distribution </summary>
            public double[] cumulDist;
        }

        /// <summary> Contains mode mean, SD, and weight </summary>
        [Serializable()]
        public struct Mode_Def
        {
            /// <summary> Mode mean </summary>
            public double mean;
            /// <summary> Mode standard deviation </summary>
            public double SD;
            /// <summary> Mode weight (1 - 100) </summary>
            public double weight;
        }

        /// <summary> Contains composite performance factor curves for 1, 10, and 20 year recurrence </summary>
        [Serializable()]
        public struct Monte_Carlo
        {         
            /// <summary> 1-year Composite P-Values </summary>
            public double[] pVals1yr;
            /// <summary> 10-year Composite P-Values </summary>
            public double[] pVals10yrs;
            /// <summary> 20-year Composite P-Values </summary>
            public double[] pVals20yrs;
            /// <summary> Set to true if Monte Carlo has been run </summary>
            public bool isComplete;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Sizes the exceedance curve arrays </summary>
        public void SizeExceedCurveArrays()
        {
            for (int i = 0; i < Num_Exceed(); i++)
            {
                exceedCurves[i].distSize = 1000;
                exceedCurves[i].xVals = new double[1000];
                exceedCurves[i].probDist = new double[1000];
                exceedCurves[i].cumulDist = new double[1000];
            }
        }

        /// <summary> Sizes the composite curve arrays </summary>
        public void SizeMonteCarloArrays()
        {
            compositeLoss.pVals1yr = new double[99];
            compositeLoss.pVals10yrs = new double[99];
            compositeLoss.pVals20yrs = new double[99];
            compositeLoss.isComplete = false;
        }

        /// <summary> Creates default performance factor curves </summary>
        public void CreateDefaultCurve()
        {
            Array.Resize(ref exceedCurves, 18);

            exceedCurves[0].exceedStr = "Turbine Availability - Owner/Operator";
            Array.Resize(ref exceedCurves[0].modes, 1);
            exceedCurves[0].modes[0].mean = 0.9912f;
            exceedCurves[0].modes[0].SD = 0.0167f;
            exceedCurves[0].modes[0].weight = 1.0f;
            exceedCurves[0].lowerBound = 0.945f;
            exceedCurves[0].upperBound = 0.995f;
            
            exceedCurves[1].exceedStr = "Turbine Availability - OEM";
            Array.Resize(ref exceedCurves[1].modes, 1);
            exceedCurves[1].modes[0].mean = 0.98f;
            exceedCurves[1].modes[0].SD = 0.02f;
            exceedCurves[1].modes[0].weight = 1.0f;
            exceedCurves[1].lowerBound = 0.945f;
            exceedCurves[1].upperBound = 0.995f;

            exceedCurves[2].exceedStr = "Power Curve";
            Array.Resize(ref exceedCurves[2].modes, 1);
            exceedCurves[2].modes[0].mean = 1.02f;
            exceedCurves[2].modes[0].SD = 0.01025f;
            exceedCurves[2].modes[0].weight = 1.0f;
            exceedCurves[2].lowerBound = 0.95f;
            exceedCurves[2].upperBound = 1.27f;

            exceedCurves[3].exceedStr = "Power Curve Degradation";
            Array.Resize(ref exceedCurves[3].modes, 1);
            exceedCurves[3].modes[0].mean = 0.995f;
            exceedCurves[3].modes[0].SD = 0.005f;
            exceedCurves[3].modes[0].weight = 1.0f;
            exceedCurves[3].lowerBound = 0.80f;
            exceedCurves[3].upperBound = 1.0f;

            exceedCurves[4].exceedStr = "Electrical";
            Array.Resize(ref exceedCurves[4].modes, 1);
            exceedCurves[4].modes[0].mean = 0.996f;
            exceedCurves[4].modes[0].SD = 0.025f;
            exceedCurves[4].modes[0].weight = 1.0f;
            exceedCurves[4].lowerBound = 0.80f;
            exceedCurves[4].upperBound = 1.0f;

            exceedCurves[5].exceedStr = "Wind Rose Sensitivity";
            Array.Resize(ref exceedCurves[5].modes, 1);
            exceedCurves[5].modes[0].mean = 1.0f;
            exceedCurves[5].modes[0].SD = 0.0083f;
            exceedCurves[5].modes[0].weight = 1.0f;
            exceedCurves[5].lowerBound = 0.95f;
            exceedCurves[5].upperBound = 1.05f;

            exceedCurves[6].exceedStr = "Data Measurement";
            Array.Resize(ref exceedCurves[6].modes, 1);
            exceedCurves[6].modes[0].mean = 1.0f;
            exceedCurves[6].modes[0].SD = 0.04f;
            exceedCurves[6].modes[0].weight = 1.0f;
            exceedCurves[6].lowerBound = 0.75f;
            exceedCurves[6].upperBound = 1.25f;

            exceedCurves[7].exceedStr = "Balance of Plant";
            Array.Resize(ref exceedCurves[7].modes, 1);
            exceedCurves[7].modes[0].mean = 0.995f;
            exceedCurves[7].modes[0].SD = 0.01f;
            exceedCurves[7].modes[0].weight = 1.0f;
            exceedCurves[7].lowerBound = 0.94f;
            exceedCurves[7].upperBound = 1.0f;

            exceedCurves[8].exceedStr = "Annual Wind Variability";
            Array.Resize(ref exceedCurves[8].modes, 1);
            exceedCurves[8].modes[0].mean = 1.0f;
            exceedCurves[8].modes[0].SD = 0.05f;
            exceedCurves[8].modes[0].weight = 1.0f;
            exceedCurves[8].lowerBound = 0.9f;
            exceedCurves[8].upperBound = 1.1f;

            exceedCurves[9].exceedStr = "Extreme Wind";
            Array.Resize(ref exceedCurves[9].modes, 1);
            exceedCurves[9].modes[0].mean = 0.997f;
            exceedCurves[9].modes[0].SD = 0.005f;
            exceedCurves[9].modes[0].weight = 1.0f;
            exceedCurves[9].lowerBound = 0.95f;
            exceedCurves[9].upperBound = 1.0f;                       

            exceedCurves[10].exceedStr = "Icing";
            Array.Resize(ref exceedCurves[10].modes, 1);
            exceedCurves[10].modes[0].mean = 0.993f;
            exceedCurves[10].modes[0].SD = 0.004f;
            exceedCurves[10].modes[0].weight = 1.0f;
            exceedCurves[10].lowerBound = 0.95f;
            exceedCurves[10].upperBound = 1.0f;                      

            exceedCurves[11].exceedStr = "Grid";
            Array.Resize(ref exceedCurves[11].modes, 1);
            exceedCurves[11].modes[0].mean = 0.996f;
            exceedCurves[11].modes[0].SD = 0.017f;
            exceedCurves[11].modes[0].weight = 1.0f;
            exceedCurves[11].lowerBound = 0.95f;
            exceedCurves[11].upperBound = 1.0f;

            exceedCurves[12].exceedStr = "Catastrophic Event";
            Array.Resize(ref exceedCurves[12].modes, 2);
            exceedCurves[12].modes[0].mean = 0.87f;
            exceedCurves[12].modes[0].SD = 0.001f;
            exceedCurves[12].modes[0].weight = 0.01f;
            exceedCurves[12].modes[1].mean = 1.00f;
            exceedCurves[12].modes[1].SD = 0.001f;
            exceedCurves[12].modes[1].weight = 0.999f;
            exceedCurves[12].lowerBound = 0.8f;
            exceedCurves[12].upperBound = 1.0f;

            exceedCurves[13].exceedStr = "Short-Term MCP";
            Array.Resize(ref exceedCurves[13].modes, 1);
            exceedCurves[13].modes[0].mean = 1.0f;
            exceedCurves[13].modes[0].SD = 0.01f;
            exceedCurves[13].modes[0].weight = 1.0f;
            exceedCurves[13].lowerBound = 0.75f;
            exceedCurves[13].upperBound = 1.25f;

            exceedCurves[14].exceedStr = "Shear Extrapolation";
            Array.Resize(ref exceedCurves[14].modes, 1);
            exceedCurves[14].modes[0].mean = 1.0f;
            exceedCurves[14].modes[0].SD = 0.018f;
            exceedCurves[14].modes[0].weight = 1.0f;
            exceedCurves[14].lowerBound = 0.9f;
            exceedCurves[14].upperBound = 1.1f;

            exceedCurves[15].exceedStr = "Wind Flow Model";
            Array.Resize(ref exceedCurves[15].modes, 1);
            exceedCurves[15].modes[0].mean = 1.0f;
            exceedCurves[15].modes[0].SD = 0.048f;
            exceedCurves[15].modes[0].weight = 1.0f;
            exceedCurves[15].lowerBound = 0.85f;
            exceedCurves[15].upperBound = 1.10f;

            exceedCurves[16].exceedStr = "Wake Loss Model";
            Array.Resize(ref exceedCurves[16].modes, 1);
            exceedCurves[16].modes[0].mean = 1.0f;
            exceedCurves[16].modes[0].SD = 0.0172f;
            exceedCurves[16].modes[0].weight = 1.0f;
            exceedCurves[16].lowerBound = 0.9f;
            exceedCurves[16].upperBound = 1.1f;

            exceedCurves[17].exceedStr = "Parasitic";
            Array.Resize(ref exceedCurves[17].modes, 1);
            exceedCurves[17].modes[0].mean = 0.997f;
            exceedCurves[17].modes[0].SD = 0.003f;
            exceedCurves[17].modes[0].weight = 1.0f;
            exceedCurves[17].lowerBound = 0.9f;
            exceedCurves[17].upperBound = 1.0f;

            SizeExceedCurveArrays();
            for (int i = 0; i <= 17; i++)
            {
                CalculateProbDist(ref exceedCurves[i]);
                Normalize_Dists(ref exceedCurves[i]);
            }            

        }

        /// <summary> Gets number of exceedance curves </summary>        
        public int Num_Exceed()
        {
            if (exceedCurves == null)
            {
                return 0;
            }
            else
            {
                return exceedCurves.Length;
            }
        }
            
        /// <summary> Deletes specified exceedance curve from list </summary>        
        public void Delete_Exceed(string Delete_Exceed_str)
        {
            ExceedanceCurve[] newCurves = new ExceedanceCurve[Num_Exceed() - 1];
            int newCrvInd = 0;

            foreach (ExceedanceCurve theseExceed in exceedCurves)
            {
                if (theseExceed.exceedStr != Delete_Exceed_str)
                {
                    newCurves[newCrvInd] = theseExceed;
                    newCrvInd++;
                }
            }

            exceedCurves = newCurves;
        }

        /// <summary> Gets performance factor for specified exceedance curve with specified X-value (0 - 1) </summary>        
        /// <returns> Returns performance factor </returns>
        public double Get_PF_Value(double val, ExceedanceCurve thisCurve)
        {            
            double Exceed_Val = 0;

            for (int i = 0; i < thisCurve.distSize - 1; i++)
            {
                if (val <= thisCurve.cumulDist[i + 1] && val >= thisCurve.cumulDist[i])
                {
                    Exceed_Val = thisCurve.xVals[i];
                    break;
                }
            }

            if (Exceed_Val == 0) Exceed_Val = thisCurve.xVals[0];

            return Exceed_Val;
        }

        /// <summary> Gets a random number between 0 - 1 </summary>        
        public Random GetRandomNumber()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            return rnd;
        }

        /// <summary> Creates composite performance factor curves (for either 1, 10, or 20 year recurrence) </summary>        
        public void FindPValues(double[] totalPFs, int numYears)
        {            
            if (numYears == 1) Array.Resize(ref compositeLoss.pVals1yr, 100);
            if (numYears == 10) Array.Resize(ref compositeLoss.pVals10yrs, 100);
            if (numYears == 20) Array.Resize(ref compositeLoss.pVals20yrs, 100);

            for (int i = 1; i <= 100; i++)
            {
                int thisInd = numSims - numSims * i / 100;

                if (numYears == 1) compositeLoss.pVals1yr[i - 1] = totalPFs[thisInd];
                if (numYears == 10) compositeLoss.pVals10yrs[i - 1] = totalPFs[thisInd];
                if (numYears == 20) compositeLoss.pVals20yrs[i - 1] = totalPFs[thisInd];
            }
        }

        /// <summary> Calculates weighted average probability and cumulative distribution with specified mean, SD and weights of defined modes </summary>        
        public void CalculateProbDist(ref ExceedanceCurve thisCurve)
        {
            double X_int;
            int numModes = thisCurve.modes.Length;

            if (numModes == 0) return;

            if (thisCurve.upperBound > thisCurve.lowerBound && thisCurve.upperBound != thisCurve.lowerBound)
                X_int = (thisCurve.upperBound - thisCurve.lowerBound) / (thisCurve.distSize - 1);
            else
                return;

            // Reset PDF
            Array.Resize(ref thisCurve.probDist, thisCurve.probDist.Length);

            for (int i = 0; i < thisCurve.distSize; i++)
            {
                thisCurve.xVals[i] = thisCurve.lowerBound + X_int * i;

                for (int j = 0; j < numModes; j++)
                {
                    double thisMean = thisCurve.modes[j].mean;
                    double thisSD = thisCurve.modes[j].SD;
                    double thisWeight = thisCurve.modes[j].weight;

                    // Probability density function of normal distribution
                    thisCurve.probDist[i] = thisCurve.probDist[i] + thisWeight *
                        (1 / Math.Pow((2 * Math.Pow(thisSD, 2) * Math.PI), 0.5) * Math.Pow(Math.E, -(Math.Pow((thisCurve.xVals[i] - thisMean), 2) / (2 * Math.Pow(thisSD, 2)))));

                }
            }
        }

        /// <summary> Normalizes PDF and CDF such that area under curve adds to 100 </summary>        
        public void Normalize_Dists(ref ExceedanceCurve thisCurve)
        {
            double pdfSum = 0;
            
            if (thisCurve.distSize <= 1)
                return;

            double pdfInt = (thisCurve.upperBound - thisCurve.lowerBound) / (thisCurve.distSize - 1);

            for (int i = 0; i < thisCurve.distSize; i++)            
                pdfSum = pdfSum + thisCurve.probDist[i];

            pdfSum = pdfSum * pdfInt;
                
            if (pdfSum > 0)
            {                
                for (int i = 0; i < thisCurve.distSize; i++)                
                    thisCurve.probDist[i] = thisCurve.probDist[i] / pdfSum;                
            }

            thisCurve.cumulDist[0] = thisCurve.probDist[0] * pdfInt;

            for (int i = 1; i < thisCurve.distSize; i++) // Cumulative distribution function (CDF) of normal distribution = sum of PDF for all x <= X             
                thisCurve.cumulDist[i] = thisCurve.cumulDist[i - 1] + thisCurve.probDist[i] * pdfInt;
                            
        }

        /// <summary> Used when importing CDF from a .CSV file. Fills in all Performance factors by interpolating between points in file. </summary>
        public void Interpolate_CDF(ref ExceedanceCurve thisCurve, double[,] Imported_CDF)
        {             
            thisCurve.lowerBound = Imported_CDF[0, 0];
            thisCurve.upperBound = Imported_CDF[Imported_CDF.GetUpperBound(0), 0];

            int numPts = Imported_CDF.GetUpperBound(0) + 1;

            double Delta_X = (thisCurve.upperBound - thisCurve.lowerBound) / (thisCurve.distSize - 1);

            for (int i = 0; i < thisCurve.distSize; i++)
            {
                thisCurve.xVals[i] = thisCurve.lowerBound + i * Delta_X;

                for (int j = 0; j < numPts - 1; j++)
                {
                    if ((thisCurve.xVals[i] >= Imported_CDF[j, 0]) && (thisCurve.xVals[i] <= Imported_CDF[j + 1, 0]))
                    {
                        // interpolate between points
                        thisCurve.cumulDist[i] = Imported_CDF[j, 1] + (thisCurve.xVals[i] - Imported_CDF[j, 0]) / (Imported_CDF[j + 1, 0] - Imported_CDF[j, 0]) * (Imported_CDF[j + 1, 1] - Imported_CDF[j, 1]);
                        break;
                    }
                }
            }

        }

        /// <summary> Calculates the probability density function from the cumulative distribution function </summary>        
        public void Calc_PDF_from_CDF(ref ExceedanceCurve thisExceed)
        {                                 
            double distInt = (thisExceed.upperBound - thisExceed.lowerBound) / (thisExceed.distSize - 1);

            thisExceed.probDist[0] = thisExceed.cumulDist[0] * distInt;

            if (distInt == 0)
                return;

            for (int i = 1; i < thisExceed.distSize - 1; i++)            
                thisExceed.probDist[i] = (thisExceed.cumulDist[i + 1] - thisExceed.cumulDist[i]) / distInt;
            
        }
                
        /// <summary> Gets overall P-Value for 1-year recurrence </summary>        
        public double GetOverallPValue_1yr(int pVal)
        {
            double thisVal = 0;

            if (pVal > 0 && pVal < 101)
                thisVal = compositeLoss.pVals1yr[pVal - 1];
            
            return thisVal;
        }


        /// <summary> Imports exeedance curves </summary>        
        public void ImportExceedCurves(Continuum thisInst)
        {
            if (thisInst.ofdExceedCurves.ShowDialog() == DialogResult.OK)
            {
                string fileName = thisInst.ofdExceedCurves.FileName;
                StreamReader sr = new StreamReader(fileName);

                sr.ReadLine(); // Header
                sr.ReadLine(); // Date
                sr.ReadLine(); // Blank

                ExceedanceCurve[] importedCurves = new ExceedanceCurve[0];
                int curveCount = 0;

                while (sr.EndOfStream == false)
                {
                    try
                    {
                        // Read in exceed curve name
                        string exceedName = sr.ReadLine();
                        curveCount++;
                        string[] exceedNameStrs = exceedName.Split(',');
                        Array.Resize(ref importedCurves, curveCount);
                        importedCurves[curveCount - 1].exceedStr = exceedNameStrs[0];

                        // Read in lower bound
                        string lwrBound = sr.ReadLine();
                        string[] lwrBounds = lwrBound.Split(',');
                        importedCurves[curveCount - 1].lowerBound = Convert.ToDouble(lwrBounds[1]) / 100;

                        // Read in lower bound
                        string uprBound = sr.ReadLine();
                        string[] uprBounds = uprBound.Split(',');
                        importedCurves[curveCount - 1].upperBound = Convert.ToDouble(uprBounds[1]) / 100;

                        // Read in modes
                        string thisMode = sr.ReadLine();
                        string[] modeNum = thisMode.Split(',', ' ');
                        int numModes = 0;

                        while (modeNum[0] == "Mode")
                        {
                            numModes++;

                            if (numModes == 1)
                                importedCurves[curveCount - 1].modes = new Mode_Def[1];
                            else
                                Array.Resize(ref importedCurves[curveCount - 1].modes, numModes);

                            // Read in mode mean
                            string meanStr = sr.ReadLine();
                            string[] meanStrs = meanStr.Split(',');
                            importedCurves[curveCount - 1].modes[numModes - 1].mean = Convert.ToSingle(meanStrs[2]) / 100;

                            // Read in mode SD
                            string sdStr = sr.ReadLine();
                            string[] sdStrs = sdStr.Split(',');
                            importedCurves[curveCount - 1].modes[numModes - 1].SD = Convert.ToSingle(sdStrs[2]) / 100;

                            // Read in mode weight
                            string weightStr = sr.ReadLine();
                            string[] weightStrs = weightStr.Split(',');
                            importedCurves[curveCount - 1].modes[numModes - 1].weight = Convert.ToSingle(weightStrs[2]) / 100;

                            thisMode = sr.ReadLine();
                            modeNum = thisMode.Split(',', ' ');
                        }
                        
                        // Read in performance factors (x values)
                        string[] pf = modeNum; // modeNum holds the performance factor array  
                        
                        if (numModes == 0)
                        {
                            string allPFs = sr.ReadLine();
                            pf = allPFs.Split(',');
                        }

                        int distSize = pf.Length - 2; // Minus due to "PF" at the beginning and one empty space at end
                        Array.Resize(ref importedCurves[curveCount - 1].xVals, distSize);
                        Array.Resize(ref importedCurves[curveCount - 1].probDist, distSize);
                        Array.Resize(ref importedCurves[curveCount - 1].cumulDist, distSize);
                        importedCurves[curveCount - 1].distSize = distSize;
                        
                        for (int i = 0; i < distSize; i++)                        
                            importedCurves[curveCount - 1].xVals[i] = Convert.ToDouble(pf[i + 1]) / 100;

                        string allPDFs = sr.ReadLine();
                        pf = allPDFs.Split(',');

                        // Read in PDFs
                        for (int i = 0; i < distSize; i++)
                            importedCurves[curveCount - 1].probDist[i] = Convert.ToDouble(pf[i + 1]);

                        string allCDFs = sr.ReadLine();
                        pf = allCDFs.Split(',');

                        // Read in CDFs
                        for (int i = 0; i < distSize; i++)
                            importedCurves[curveCount - 1].cumulDist[i] = Convert.ToDouble(pf[i + 1]);

                        // Empty line before next 
                        sr.ReadLine();
                        
                    }
                    catch
                    {
                        MessageBox.Show("Error reading in exceedance curves.", "Continuum 3");
                        sr.Close();
                        return;
                    }
                    
                }

                thisInst.turbineList.exceed.exceedCurves = importedCurves;
                
                sr.Close();
            }
        }
        
    }
}
