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

namespace GUI_Test_Creator
{
    public partial class GUI_Creator : Form
    {
        string testFolder = "C:\\Users\\OEE2017_27\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum\\Snippets";
        string saveToFolder = "C:\\Users\\OEE2017_27\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum\\Tests";
        string combinedTestsFolder = "C:\\Users\\OEE2017_27\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum\\Combined Tests";
        string startSnip = "\\Test start.txt";
        string endSnip = "\\Test end.txt";

        // Import snippets
        string topoSnip = "\\Import Topo.txt";
        string landCoverSnip = "\\Import Land Cover.txt";
        string metTABSnip = "\\Import Met TAB.txt";
        string metTSSnip = "\\Import Met TS.txt";
        string turbineSnip = "\\Import Turbine sites.txt";
        string powerCurveSnip = "\\Import Power Curve.txt";
        string zonesSnip = "\\Import Zones.txt";
        string merraSnip = "\\Import MERRA.txt";

        // Action snippets
        string genTurbEstsSnip = "\\Action Gen Turb Ests.txt";
        string analyzeMetsSnip = "\\Action Analyze Mets.txt";
        string monteCarloSnip = "\\Action Monte Carlo.txt";
        string wakeNetEstSnip = "\\Action Wake Net Ests.txt";
        string wakeMapSnip = "\\Action Wake Map.txt";
        string mapTurbine = "\\Action Gen Map Turbines.txt";
        string mapLargest = "\\Action Gen Map Largest.txt";
        string roundRobinSnip = "\\Action Run Round Robin.txt";
        string iceThrowSnip = "\\Action Ice Throw.txt";
        string shadowFlickerSnip = "\\Action Run Shadow Flicker.txt";
        string noiseModelSnip = "\\Action Do Noise.txt";
        string mcpSnip = "\\Action MCP.txt";

        // Delete snippets
        string deleteCurves = "\\Delete Power Curves.txt";
        string deleteZones = "\\Delete Zones.txt";
        string deleteTurbines = "\\Delete Turbines.txt";
        string deleteMet = "\\Delete Met.txt";

        public GUI_Creator()
        {
            InitializeComponent();
        }

        public ComboBox InitializeCboImpDelAct()
        {
            ComboBox thisCbo = new ComboBox();

            if (cboImpDelAct_1.SelectedItem != null)
            {
                if (cboImpDelAct_1.SelectedItem.ToString() == "Import")
                {
                    thisCbo.Items.Clear();
                    thisCbo.Items.Add("Topography");
                    thisCbo.Items.Add("Land Cover");
                    thisCbo.Items.Add("Turbines");
                    thisCbo.Items.Add("Power Curve");
                    thisCbo.Items.Add("Met TAB file");
                    thisCbo.Items.Add("Met TS file");
                    thisCbo.Items.Add("Zones");
                    thisCbo.Items.Add("MERRA");
                }
                else if (cboImpDelAct_1.SelectedItem.ToString() == "Delete")
                {
                    thisCbo.Items.Add("Turbines");
                    thisCbo.Items.Add("Power Curve");
                    thisCbo.Items.Add("Met");
                    thisCbo.Items.Add("Zones");
                }
                else if (cboImpDelAct_1.SelectedItem.ToString() == "Action") // Action
                {
                    thisCbo.Items.Clear();
                    thisCbo.Items.Add("Analyze Mets");
                    thisCbo.Items.Add("Gen. Turb. Ests.");
                    thisCbo.Items.Add("Create Wake Ests.");
                    thisCbo.Items.Add("Do Monte Carlo");
                    thisCbo.Items.Add("Create wake map");
                    thisCbo.Items.Add("Create largest map");
                    thisCbo.Items.Add("Create map around turbs");
                    thisCbo.Items.Add("Run Round Robin");
                    thisCbo.Items.Add("Do Ice Throw");
                    thisCbo.Items.Add("Do Shadow");
                    thisCbo.Items.Add("Do Noise model");
                    thisCbo.Items.Add("Do MCP");
                }
            }
            else
            {
                thisCbo.Text = "";
            }

            return thisCbo;
        }

        private void CboImpDelAct_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboImpDelAct_1 = InitializeCboImpDelAct();            
        }

        private void CboImpDelAct_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboImpDelAct_2 = InitializeCboImpDelAct();
        }

        private void CboImpDelAct_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboImpDelAct_3 = InitializeCboImpDelAct();
        }

        private void CboImpDelAct_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboImpDelAct_4 = InitializeCboImpDelAct();
        }

        private void CboImpDelAct_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboImpDelAct_5 = InitializeCboImpDelAct();
        }

        private void CboImpDelAct_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboImpDelAct_6 = InitializeCboImpDelAct();
        }

        private void CboImpDelAct_7_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboImpDelAct_7 = InitializeCboImpDelAct();
        }

        private void CboImpDelAct_8_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboImpDelAct_8 = InitializeCboImpDelAct();
        }

        private void CboImpDelAct_9_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboImpDelAct_9 = InitializeCboImpDelAct();
        }

        private void CboImpDelAct_10_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboImpDelAct_10 = InitializeCboImpDelAct();
        }

        private void CboImpDelAct_11_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboImpDelAct_11 = InitializeCboImpDelAct();
        }

        public ComboBox PopulateCboFilename(string inputActDel)
        {
            ComboBox thisCbo = new ComboBox();

            thisCbo.Items.Clear();

            if (inputActDel != null)
            {
                if (inputActDel == "Topography")
                {
                    thisCbo.Items.Add("NW Ohio Topo.tif");
                    thisCbo.Items.Add("Findlay Topo.tif");
                    thisCbo.Items.Add("Firewheel Topo.adf");
                }
                else if (inputActDel == "Land Cover")
                {
                    thisCbo.Items.Add("NW Ohio Land Cover.tif");
                    thisCbo.Items.Add("Findlay Land Cover.tif");
                    thisCbo.Items.Add("Firewheel Land Cover.tif");
                }
                else if (inputActDel == "Turbines")
                {
                    thisCbo.Items.Add("Whirlpool W1.csv");
                    thisCbo.Items.Add("Ball 2 Turbines.csv");
                }
                else if (inputActDel == "Power Curve")
                {
                    thisCbo.Items.Add("GE_1_85_CRV.csv");
                    thisCbo.Items.Add("Goldwind 87-1500 PC_1.205.csv");
                    thisCbo.Items.Add("Goldwind 93-1500 PC_1.205.csv");
                }
                else if (inputActDel == "Met TAB file")
                {
                    thisCbo.Items.Add("Archbold Findlay coords.TAB");
                    thisCbo.Items.Add("Archbold.TAB");
                }
                else if (inputActDel == "Met TS")
                {
                    thisCbo.Items.Add("OH - Archbold Formatted - 20180709.csv");
                    thisCbo.Items.Add("Archbold TS short Findlay coords.csv");
                }
                else if (inputActDel == "Zones")
                {
                    thisCbo.Items.Add("Ball 2 Zones.csv");
                }
            }

            return thisCbo;
        }

        private void CboInputsOrActions_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_1.SelectedItem != null)
                cboInputsOrActions_1 = PopulateCboFilename(cboInputsOrActions_1.SelectedItem.ToString());
        }

        private void CboInputsOrActions_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_2.SelectedItem != null)
                cboInputsOrActions_2 = PopulateCboFilename(cboInputsOrActions_2.SelectedItem.ToString());
        }

        private void CboInputsOrActions_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_3.SelectedItem != null)
                cboInputsOrActions_3 = PopulateCboFilename(cboInputsOrActions_3.SelectedItem.ToString());
        }

        private void CboInputsOrActions_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_4.SelectedItem != null)
                cboInputsOrActions_4 = PopulateCboFilename(cboInputsOrActions_4.SelectedItem.ToString());
        }

        private void CboInputsOrActions_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_5.SelectedItem != null)
                cboInputsOrActions_5 = PopulateCboFilename(cboInputsOrActions_5.SelectedItem.ToString());
        }

        private void CboInputsOrActions_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_6.SelectedItem != null)
                cboInputsOrActions_6 = PopulateCboFilename(cboInputsOrActions_6.SelectedItem.ToString());
        }

        private void CboInputsOrActions_7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_7.SelectedItem != null)
                cboInputsOrActions_7 = PopulateCboFilename(cboInputsOrActions_7.SelectedItem.ToString());
        }

        private void CboInputsOrActions_8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_8.SelectedItem != null)
                cboInputsOrActions_8 = PopulateCboFilename(cboInputsOrActions_8.SelectedItem.ToString());
        }

        private void CboInputsOrActions_9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_9.SelectedItem != null)
                cboInputsOrActions_9 = PopulateCboFilename(cboInputsOrActions_9.SelectedItem.ToString());
        }

        private void CboInputsOrActions_10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_10.SelectedItem != null)
                cboInputsOrActions_10 = PopulateCboFilename(cboInputsOrActions_10.SelectedItem.ToString());
        }

        private void CboInputsOrActions_11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_11.SelectedItem != null)
                cboInputsOrActions_11 = PopulateCboFilename(cboInputsOrActions_11.SelectedItem.ToString());
        }

        private void BtnCreateTest_Click(object sender, EventArgs e)
        {
            // Generate test with specified parameters
            try
            {
                string fileName = saveToFolder + "\\" + txtTestFilename.Text;
                string projectName = cboProject.Text;
                
                StreamWriter sw = new StreamWriter(fileName);
                
                // First, write start of test
                StreamReader sr = new StreamReader(testFolder + startSnip);
                while (sr.EndOfStream == false)
                {
                    string lineToWrite = sr.ReadLine();
                    string trimmedLine = lineToWrite.Trim();

                    if (lineToWrite == "public void ")
                        lineToWrite = lineToWrite + txtTestFilename.Text + "()";

                    if (trimmedLine.Length > 14)
                        if (trimmedLine.Substring(0, 15) == "string fileName")
                            lineToWrite = lineToWrite + txtTestFilename.Text + "\";";

                    if (trimmedLine.Length > 42)
                        if (trimmedLine.Substring(0, 43) == "thisInst.UTM_conversions.UTMZoneNumber = ")
                            lineToWrite = lineToWrite + cboUTMZone.Text + "\";";

                    if (trimmedLine.Length > 34)
                        if (trimmedLine.Substring(0, 35) == "thisInst.UTM_conversions.hemisphere = ")
                            lineToWrite = lineToWrite + cboNorthOrSouth.Text + "\";";

                    sw.WriteLine(lineToWrite);
                }
                
                for (int i = 0; i < 11; i++)
                {
                    sw.WriteLine();
                    ComboBox thisInputOrAcion = new ComboBox();
                    ComboBox thisFile = new ComboBox();

                    if (i == 0)
                    {
                        thisInputOrAcion = cboInputsOrActions_1;
                        thisFile = cboFilename_1;
                    }
                    else if (i == 1)
                    {
                        thisInputOrAcion = cboInputsOrActions_2;
                        thisFile = cboFilename_2;
                    }
                    else if (i == 2)
                    {
                        thisInputOrAcion = cboInputsOrActions_3;
                        thisFile = cboFilename_3;
                    }
                    else if (i == 3)
                    {
                        thisInputOrAcion = cboInputsOrActions_4;
                        thisFile = cboFilename_4;
                    }
                    else if (i == 4)
                    {
                        thisInputOrAcion = cboInputsOrActions_5;
                        thisFile = cboFilename_5;
                    }
                    else if (i == 5)
                    {
                        thisInputOrAcion = cboInputsOrActions_6;
                        thisFile = cboFilename_6;
                    }
                    else if (i == 6)
                    {
                        thisInputOrAcion = cboInputsOrActions_7;
                        thisFile = cboFilename_7;
                    }
                    else if (i == 7)
                    {
                        thisInputOrAcion = cboInputsOrActions_8;
                        thisFile = cboFilename_8;
                    }
                    else if (i == 8)
                    {
                        thisInputOrAcion = cboInputsOrActions_9;
                        thisFile = cboFilename_9;
                    }
                    else if (i == 9)
                    {
                        thisInputOrAcion = cboInputsOrActions_10;
                        thisFile = cboFilename_10;
                    }
                    else if (i == 10)
                    {
                        thisInputOrAcion = cboInputsOrActions_11;
                        thisFile = cboFilename_11;
                    }

                    string thisFilename = thisFile.SelectedItem.ToString();
                    // Import/Delete/Action 1
                    if (thisInputOrAcion.SelectedItem != null)
                        if (thisInputOrAcion.SelectedItem.ToString() == "Topography")
                            WriteTopographySnippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Land Cover")
                            WriteLandCoverSnippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Met TAB file")
                            WriteMetTABSnippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Met TS file")
                            WriteMetTSSnippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Turbines")
                            WriteTurbineSnippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Power Curve")
                            WritePowerCurveSnippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Zones")
                            WriteZoneSnippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "MERRA")
                            WriteMERRA2Snippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Gen. Turb. Ests.")
                            WriteTurbineGrossEsts(sw);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Analyze Mets")
                        {
                            sr = new StreamReader(testFolder + analyzeMetsSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Do Monte Carlo")
                            WriteDoMonteCarlo(sw);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Create Wake Ests.")
                        {
                            
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Create wake map")
                        {
                            sr = new StreamReader(testFolder + wakeMapSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Create largest map")
                        {
                            sr = new StreamReader(testFolder + mapLargest);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Create map around turbines")
                        {
                            sr = new StreamReader(testFolder + mapTurbine);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Run Round Robin")
                        {
                            sr = new StreamReader(testFolder + roundRobinSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Do Ice Throw")
                        {
                            sr = new StreamReader(testFolder + iceThrowSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Do Shadow")
                        {
                            sr = new StreamReader(testFolder + shadowFlickerSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Do Noise")
                        {
                            sr = new StreamReader(testFolder + noiseModelSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "MCP")
                        {
                            sr = new StreamReader(testFolder + mcpSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }

                }

                sr = new StreamReader(testFolder + endSnip);
                while (sr.EndOfStream == false)
                {
                    string lineToWrite = sr.ReadLine();
                    sw.WriteLine(lineToWrite);
                }

                sr.Close();
                sw.Close();
            }
            catch
            {
                MessageBox.Show("Error writing to specified file.");                
                return;
            }

        }

        private void BtnCombineTests_Click(object sender, EventArgs e)
        {
            // Find all tests that start with filename and merge into one file
            string fileName = txtTestFilename.Text;
            int underScore = fileName.LastIndexOf('_');

            if (underScore != -1)
                fileName = fileName.Substring(0, underScore);

            string combinedFilename = fileName + "_Tests.txt";

            StreamWriter sw = new StreamWriter(combinedTestsFolder + "\\" + combinedFilename);

            string[] testfiles = Directory.GetFiles(saveToFolder, fileName + "*");

            foreach(string testfile in testfiles)
            {
                StreamReader sr = new StreamReader(testfile);

                while (sr.EndOfStream == false)
                {
                    string lineToWrite = sr.ReadLine();
                    sw.WriteLine(lineToWrite);
                }

                sw.WriteLine();
                sr.Close();
            }

            sw.Close();
            
        }

        public void WriteTopographySnippet(StreamWriter sw, string thisFile)
        {
            StreamReader sr = new StreamReader(testFolder + topoSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 15)
                    if (trimmedLine.Substring(0, 15) == "string topoFile")
                        lineToWrite = lineToWrite + thisFile + "\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteLandCoverSnippet(StreamWriter sw, string thisFile)
        {
            StreamReader sr = new StreamReader(testFolder + landCoverSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 20)
                    if (trimmedLine.Substring(0, 20) == "string landCoverFile")
                        lineToWrite = lineToWrite + thisFile + "\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteMetTABSnippet(StreamWriter sw, string thisFile)
        {
            StreamReader sr = new StreamReader(testFolder + metTABSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 25)
                    if (trimmedLine.Substring(0, 25) == "thisInst.ofdMets.FileName")
                        lineToWrite = lineToWrite + thisFile + "\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteMetTSSnippet(StreamWriter sw, string thisFile)
        {
            StreamReader sr = new StreamReader(testFolder + metTSSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 24)
                    if (trimmedLine.Substring(0, 25) == "string metTSFile")
                        lineToWrite = lineToWrite + thisFile + "\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteTurbineSnippet(StreamWriter sw, string thisFile)
        {
            StreamReader sr = new StreamReader(testFolder + turbineSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 18)
                    if (trimmedLine.Substring(0, 18) == "string turbineFile")
                        lineToWrite = lineToWrite + thisFile + "\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WritePowerCurveSnippet(StreamWriter sw, string thisFile)
        {
            StreamReader sr = new StreamReader(testFolder + powerCurveSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 21)
                    if (trimmedLine.Substring(0, 21) == "string powerCurveFile")
                        lineToWrite = lineToWrite + thisFile + "\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteZoneSnippet(StreamWriter sw, string thisFile)
        {
            StreamReader sr = new StreamReader(testFolder + zonesSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 15)
                    if (trimmedLine.Substring(0, 16) == "string zonesFile")
                        lineToWrite = lineToWrite + thisFile + "\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteMERRA2Snippet(StreamWriter sw, string thisFile)
        {
            StreamReader sr = new StreamReader(testFolder + merraSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 11)
                    if (trimmedLine.Substring(0, 12) == "Met metMERRA")
                        lineToWrite = lineToWrite + thisFile + "\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteTurbineGrossEsts(StreamWriter sw)
        {
            StreamReader sr = new StreamReader(testFolder + genTurbEstsSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteDoMonteCarlo(StreamWriter sw)
        {
            StreamReader sr = new StreamReader(testFolder + monteCarloSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteNetEsts(StreamWriter sw)
        {
            StreamReader sr = new StreamReader(testFolder + wakeNetEstSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }
    }
}
