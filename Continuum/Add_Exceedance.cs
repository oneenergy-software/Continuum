using System;
using System.IO;
using System.Windows.Forms;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot;

namespace ContinuumNS
{
    /// <summary>
    ///  This class defines a GUI where the user can define an exceedance (i.e. performance factor) curve and adds it to the collection of curves
    /// </summary>
    public partial class Add_Exceedance : Form        
    {
        /// <summary> Exceedance curve that is being defined </summary>
        public Exceedance.ExceedanceCurve thisExceed;
        /// <summary> True if exceedance curve was successfully created </summary>
        public bool goodToGo;

        ////////////////////////////////////////////////////////////////////////////////

        /// <summary> Initializes class</summary>            
        public Add_Exceedance()
        {
            InitializeComponent();
        }
              
        /// <summary> "Add Mode" button clicked </summary>        
        public void btn_AddMode_Click(object sender, EventArgs e)
        {
            Add_Edit_Mode(true);
        }

        /// <summary> Adds or edits one mode of exceedance curve. (Exceedance curves may be defined as multimodal distributions.) </summary>        
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
            else if (formMode.okAdd == false)
                return;

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

        /// <summary> Updates plots on form. Plots probability and cumulative density function of defined exceedance </summary>
        public void Update_plot()
        {                        
            plotExceed.Model = new PlotModel();
            var model = plotExceed.Model;
            model.IsLegendVisible = false;

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Performance Factor";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "PDF";
            yAxis.Key = "PDF";
            LinearAxis secYAxis = new LinearAxis();
            secYAxis.Position = AxisPosition.Right;
            secYAxis.Title = "CDF";
            secYAxis.Key = "CDF";
            
            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);
            model.Axes.Add(secYAxis);

            // Add PDF  
            var pdfSeries = new LineSeries();
            pdfSeries.YAxisKey = "PDF";
            pdfSeries.Title = "PDF";
            model.Series.Add(pdfSeries);

            // Add CDF
            var cdfSeries = new LineSeries();
            cdfSeries.YAxisKey = "CDF";
            cdfSeries.Title = "CDF";
            model.Series.Add(cdfSeries);
                                        

            for (int i = 0; i < thisExceed.distSize; i++)
            {
                pdfSeries.Points.Add(new DataPoint(Math.Round(thisExceed.xVals[i] * 100, 2), Math.Round(thisExceed.probDist[i], 4)));
                cdfSeries.Points.Add(new DataPoint(Math.Round(thisExceed.xVals[i] * 100, 2), Math.Round(thisExceed.cumulDist[i], 4)));               
            }

            plotExceed.Refresh();
            
        }

        /// <summary> Adds mode to list and updates GUI </summary>
        public void Update_Mode_List()
        {          
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
        
        
        /// <summary> Cancel button </summary>        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            thisExceed.exceedStr = "";
            goodToGo = false;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary> Opens dialog box to import CSV file with CDF </summary>        
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

        /// <summary> Reads CSV file with defined CDF. Expected file format: Perf factor, CDF. </summary> 
        /// <returns> double[,] Import_PF_CDF = new double[num_pts, 2]; i = Perf Fact, j = CDF </returns>
        public double[,] Read_CDF_file(string filename)
        {
            // Import user-defined CFD instead of specifying mean and SD
                        
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

        /// <summary> User edits a defined mode used to define CDF </summary>        
        private void btn_EditMode_Click(object sender, EventArgs e)
        {
            Add_Edit_Mode(false);
        }
              
        
    }
}
