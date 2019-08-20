using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Nevron.Chart;

namespace ContinuumNS
{
    public partial class Add_Exceedance : Form        
    {
        public Exceedance.ExceedanceCurve thisExceed;
        public bool goodToGo;

        public int Get_numModes(ref Exceedance.ExceedanceCurve thisExceed)
        {
            int numModes = 0;

            if (thisExceed.modes == null)            
                numModes = 0;            
            else            
                numModes = thisExceed.modes.Length;
            
            return numModes;
        }
            
        public Add_Exceedance()
        {
            InitializeComponent();
        }
                
        public void btn_AddMode_Click(object sender, EventArgs e)
        {

            Add_Edit_Mode(true);
        }

        public void Add_Edit_Mode(bool isAddMode)
        {          
            Add_Mode formMode = new Add_Mode();
            int editInd = 0;
            int numModes = 0;

            if (thisExceed.modes != null)
                numModes = thisExceed.modes.Length;

            if (isAddMode == false)
            { // need to find the mode that was selected
                if (lstModes.SelectedItems.Count != 1)
                {
                    MessageBox.Show("Select one mode to edit.", "", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    for (int i = 0; i < numModes; i++ )
                    {
                        double selMean = Convert.ToDouble(lstModes.SelectedItems[0].SubItems[0].Text);
                        double selSD = Convert.ToDouble(lstModes.SelectedItems[0].SubItems[1].Text);
                        double selWeight = Convert.ToDouble(lstModes.SelectedItems[0].SubItems[2].Text);

                        if (Math.Round(thisExceed.modes[i].mean, 3) == Math.Round(selMean/100,3) && Math.Round(thisExceed.modes[i].SD, 3) == Math.Round(selSD /100, 3) 
                            && Math.Round(thisExceed.modes[i].weight, 0) == Math.Round(selWeight /100))
                        {
                            formMode.txtMean.Text = Math.Round(thisExceed.modes[i].mean * 100, 2).ToString();
                            formMode.txtSD.Text = Math.Round(thisExceed.modes[i].SD * 100, 3).ToString();
                            formMode.txtWeight.Text = Math.Round(thisExceed.modes[i].weight * 100, 0).ToString();
                            editInd = i;
                            break;
                        }
                    }
                }
            }
                                 
            formMode.ShowDialog();

            if (formMode.okAdd == true && isAddMode == true)
            {
                // Add to list of modes
                int newModeInd = numModes;
                Array.Resize(ref thisExceed.modes, newModeInd + 1);
                thisExceed.modes[newModeInd].mean = formMode.mean / 100;
                thisExceed.modes[newModeInd].SD = formMode.SD / 100;
                thisExceed.modes[newModeInd].weight = formMode.weight / 100;
            }
            else if (formMode.okAdd == true && isAddMode == false)
            {
                thisExceed.modes[editInd].mean = formMode.mean / 100;
                thisExceed.modes[editInd].SD = formMode.SD / 100;
                thisExceed.modes[editInd].weight = formMode.weight / 100;
            }

            Update_Mode_List();
            thisExceed.lowerBound = Convert.ToSingle(txt_LowerBound.Text) / 100;
            thisExceed.upperBound = Convert.ToSingle(txt_UpperBound.Text) / 100;

            if (thisExceed.distSize == 0)
            {
                thisExceed.distSize = 1000;
                Array.Resize(ref thisExceed.xVals, 1000);
                Array.Resize(ref thisExceed.probDist, 1000);
                Array.Resize(ref thisExceed.cumulDist, 1000);
            }

            // Reset PDF and CDF            
            thisExceed.probDist = new double[thisExceed.distSize];
            thisExceed.cumulDist = new double[thisExceed.distSize];

            Exceedance exceed = new Exceedance();
            exceed.CalculateProbDist(ref thisExceed);
            exceed.Normalize_Dists(ref thisExceed);
            Update_plot();
        }

        public void Update_plot()
        {
            // Plots probability and cumulative density function of defined exceedance
            
            chtExceedDist.Charts[0].Series.Clear();
            chtExceedDist.Labels.Clear();
            chtExceedDist.Controller.Tools.Clear();
            NAxis secondaryY = chtExceedDist.Charts[0].Axis(StandardAxis.SecondaryY);
            secondaryY.Visible = true;

            // Add PDF            
            NLineSeries pdfSeries = new NLineSeries();
            pdfSeries.DataLabelStyle.Visible = false;
            pdfSeries.Name = "PDF";
            pdfSeries.UseXValues = true;
            pdfSeries.BorderStyle.Color = Color.Red;
            chtExceedDist.Charts[0].Series.Add(pdfSeries);
            pdfSeries.DisplayOnAxis(StandardAxis.SecondaryY, true);
            pdfSeries.DisplayOnAxis(StandardAxis.PrimaryY, false);

            NLineSeries cdfSeries = new NLineSeries();
            cdfSeries.DataLabelStyle.Visible = false;
            cdfSeries.Name = "CDF";
            cdfSeries.UseXValues = true;
            cdfSeries.BorderStyle.Color = Color.Blue;
            chtExceedDist.Charts[0].Series.Add(cdfSeries);
            cdfSeries.DisplayOnAxis(StandardAxis.PrimaryY, true);
            cdfSeries.DisplayOnAxis(StandardAxis.SecondaryY, false);

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(chtExceedDist.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Performance Factor";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(chtExceedDist.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "CDF";

            NStandardScaleConfigurator scaleConfiguratorY2 = (NStandardScaleConfigurator)(chtExceedDist.Charts[0].Axis(StandardAxis.SecondaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "PDF";                       

            for (int i = 0; i < thisExceed.distSize; i++)
            {
                pdfSeries.XValues.Add(thisExceed.xVals[i] * 100);
                pdfSeries.Values.Add(thisExceed.probDist[i]);

                cdfSeries.XValues.Add(thisExceed.xVals[i] * 100);
                cdfSeries.Values.Add(thisExceed.cumulDist[i]);
            }

            chtExceedDist.Refresh();
        }

        public void Update_Mode_List()
        {
             // Add to table on form
            
            lstModes.Items.Clear();

            if (thisExceed.modes == null)
                return;

            int numModes = thisExceed.modes.Length;

            for (int i = 0; i < numModes; i++)
            {
                ListViewItem lvi = new ListViewItem((100 * thisExceed.modes[i].mean).ToString());
                lvi.SubItems.Add((thisExceed.modes[i].SD * 100).ToString());
                lvi.SubItems.Add((thisExceed.modes[i].weight * 100).ToString());
                                    
                lstModes.Items.Add(lvi);
            }

            lstModes.Columns[0].Width = 125;
            lstModes.Columns[1].Width = 125;
            lstModes.Columns[2].Width = 125;
        }

        private void button1_Click(object sender, EventArgs e)
        {            

            if (thisExceed.lowerBound == 0)
            {
                MessageBox.Show("Need to specify a lower bound for curve");
                return;
            }
            else if (thisExceed.upperBound == 0)
            {
                MessageBox.Show("Need to specify a upper bound for curve");
                return;
            }

            // TO DO: if statements for the rest of curve definition
            goodToGo = true;

            if (goodToGo)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            
        }

        
        public float Get_Lower_Bound()
        {
            float lowerBound = -999;
            try
            {
                lowerBound = Convert.ToSingle(txt_LowerBound.Text);
            }
            catch
            {
                lowerBound = -999;
            }

            return lowerBound;
        }

        public float Get_Upper_Bound()
        {
            float upperBound = 999;
            try
            {
                upperBound = Convert.ToSingle(txt_UpperBound.Text);
            }
            catch
            {
                upperBound = 999;
            }

            return upperBound;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            thisExceed.exceedStr = "";
            goodToGo = false;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnImportCDF_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (ofdImportCFD.ShowDialog() == DialogResult.OK)
                filename = ofdImportCFD.FileName;
            else
                return;

            if (filename == "")
                return;

            double[,] importedCDF = Read_CDF_file(filename);

            if (importedCDF == null)
                return;

            Exceedance exceed = new Exceedance();

            if (importedCDF.GetUpperBound(0) > 0)
            {
                thisExceed = new Exceedance.ExceedanceCurve();
                thisExceed.exceedStr = txtExceed.Text.ToString();
                thisExceed.distSize = 1000;
                thisExceed.xVals = new double[1000];
                thisExceed.probDist = new double[1000];
                thisExceed.cumulDist = new double[1000];

                exceed.Interpolate_CDF(ref thisExceed, importedCDF);
                exceed.Calc_PDF_from_CDF(ref thisExceed);

                txt_LowerBound.Text = Math.Round(importedCDF[0, 0] * 100, 3).ToString();
                txt_UpperBound.Text = Math.Round(importedCDF[importedCDF.GetUpperBound(0), 0] * 100, 3).ToString();

                Update_Mode_List();
                Update_plot();

            }           

        }               

        public double[,] Read_CDF_file(string filename)
        {
            // Import user-defined CFD instead of specifying mean and SD
            // Expected file format: Perf factor, CDF
            
            string line = "";
            double thisPF = 0;
            double thisCDF = 0;
            int num_pts = 0;
            double[] importPF = null;
            double[] importCDF = null;
            StreamReader file = null;

            if (filename != "")
            {
                try
                {
                    file = new StreamReader(filename);
                }
                catch
                {
                    MessageBox.Show("Error opening CDF file. Make sure that it is not open in another program.", "Continuum 3");
                    return null;
                }

                while ((line = file.ReadLine()) != null)
                {
                    try
                    {
                        Char[] delims = { ',' };
                        String[] substrings = line.Split(delims);
                        // Only read in data intervals with valid WS & WD
                        if (substrings.Length == 2)
                        {
                            thisPF = Convert.ToDouble(substrings[0]);
                            thisCDF = Convert.ToSingle(substrings[1]);

                            num_pts++;
                            Array.Resize(ref importPF, num_pts);
                            importPF[num_pts - 1] = thisPF;

                            Array.Resize(ref importCDF, num_pts);
                            importCDF[num_pts - 1] = thisCDF;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error reading CDF fie");
                        break;
                    }
                }
            }

            double[,] Import_PF_CDF = new double[num_pts, 2]; // 0 rows, 2 columns (Perf Fact, CDF)

            for (int i = 0; i < num_pts; i++ )
            {
                Import_PF_CDF[i, 0] = importPF[i] / 100;
                Import_PF_CDF[i, 1] = importCDF[i];
            }

            return Import_PF_CDF;
        }

        private void btn_EditMode_Click(object sender, EventArgs e)
        {
            Add_Edit_Mode(false);
        }
              
        
    }
}
