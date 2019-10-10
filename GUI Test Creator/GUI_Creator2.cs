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
using ContinuumNS;

namespace GUI_Test_Creator
{
    public partial class GUI_Creator2 : Form
    {
        string testFolder = "C:\\Users\\OEE2017_27\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum\\Snippets";
     //   string saveToFolder = "C:\\Users\\OEE2017_27\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum\\Tests";
        string saveToFolder = "C:\\Users\\OEE2017_27\\Desktop\\Tests";
        string continuumFolder = "C:\\Users\\OEE2017_27\\Desktop\\Continuum tests";
        string combinedTestsFolder = "C:\\Users\\OEE2017_27\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum\\Combined Tests";

        string firewheelMERRA = "C:\\Users\\OEE2017_27\\Desktop\\MERRA2";
        string ohioMERRA = "C:\\Users\\OEE2017_27\\Dropbox (OEE)\\Due Diligence - Raw Data\\MERRA Data\\Ohio\\Ohio plus - tavg data";

        bool okToUpdate = true;

        string startSnip = "\\Test start.txt";
        string startDeleteSnip = "\\Test start delete.txt";
        string endSnip = "\\Test end.txt";
        string endDeleteSnip = "\\Test delete end.txt";

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
        string saveAsSnip = "\\Action Save As.txt";

        // Delete snippets
        string deleteCurves = "\\Delete Power Curves.txt";
        string deleteZones = "\\Delete Zones.txt";
        string deleteTurbine = "\\Delete One Turbine.txt";
        string deleteMet = "\\Delete Met.txt";

        public GUI_Creator2()
        {
            InitializeComponent();
            okToUpdate = false;
            cboProject.SelectedIndex = 0;
            cboTABorTS.SelectedIndex = 0;
            cboNorthOrSouth.SelectedIndex = 0;
            cboPowerCurves.SelectedIndex = 0;
            UpdateCFNList();
            okToUpdate = true;
            
        }

        private void BtnAllInputCombos_Click(object sender, EventArgs e)
        {
            
        }

        private void CboProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
                        lineToWrite = lineToWrite + thisFile + ".TAB\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteMetTSSnippet(StreamWriter sw, string thisFile, bool doFiltering)
        {
            StreamReader sr = new StreamReader(testFolder + metTSSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 8)
                    if (trimmedLine.Substring(0, 9) == "metTSFile")
                        lineToWrite = lineToWrite + thisFile + "\";";

                if (trimmedLine.Length > 34)
                    if (trimmedLine.Substring(0, 35) == "thisInst.metList.filteringEnabled =")
                    {
                        if (doFiltering)
                            lineToWrite = lineToWrite + " true;";
                        else
                            lineToWrite = lineToWrite + " false;";
                    }                        

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteTurbineSnippet(StreamWriter sw, string thisFile, string projectName)
        {
            StreamReader sr = new StreamReader(testFolder + turbineSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 18)
                    if (trimmedLine.Substring(0, 18) == "string turbineFile")
                        lineToWrite = lineToWrite + projectName + "\\\\" + thisFile + ".csv\";";

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
                        lineToWrite = lineToWrite + thisFile + ".csv\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteZoneSnippet(StreamWriter sw, string thisFile, string projectName)
        {
            StreamReader sr = new StreamReader(testFolder + zonesSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 15)
                    if (trimmedLine.Substring(0, 16) == "string zonesFile")
                        lineToWrite = lineToWrite + projectName + "\\\\" + thisFile + ".csv\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteMERRA2Snippet(StreamWriter sw, int metInd, DateTime startTime, DateTime endTime, int merraFolderInd)
        {
            
            StreamReader sr = new StreamReader(testFolder + merraSnip);
            int merraFolderCount = 0; 

            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 6)
                    if (trimmedLine.Substring(0, 7) == "thisMet")
                        lineToWrite = lineToWrite + metInd + "];";

                if (trimmedLine.Length > 30)
                    if (trimmedLine.Substring(0, 31) == "thisInst.dateMERRAStart.Value =")
                        lineToWrite = lineToWrite + "\"" + startTime.ToString() + "\");";

                if (trimmedLine.Length > 28)
                    if (trimmedLine.Substring(0, 29) == "thisInst.dateMERRAEnd.Value =")
                        lineToWrite = lineToWrite + "\"" + endTime.ToString() + "\");";

                if (trimmedLine.Length > 12)
                    if (trimmedLine.Substring(0, 13) == "merraFolder =")
                    {
                        if (merraFolderInd == merraFolderCount)
                            lineToWrite = lineToWrite;
                        else
                            lineToWrite = "";

                        merraFolderCount++;
                    }                   
                       

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteMCPSnippet(StreamWriter sw, int metInd)
        {
            StreamReader sr = new StreamReader(testFolder + mcpSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 8)
                    if (trimmedLine.Substring(0, 9) == "thisMet =")
                        lineToWrite = lineToWrite + metInd + "];";

                sw.WriteLine(lineToWrite);
            }

            sr.Close();
        }

        public int GetNumberInputCombos(int numInputs)
        {
            int N_fact = 1;

            for (int i = numInputs; i >= 1; i--)
                N_fact = N_fact * i;                        
            
            return N_fact;
        }


        public int[,] GetAllPermutations(int numInputs)
        {
            // Returns all possible combinations of inputs for specified number of inputs
            int numCombos = GetNumberInputCombos(numInputs);
                       
            int[,] inputOrderAllPerms = new int[numInputs, numCombos];
            int[] inputInts = new int[numInputs];
            int comboInd = 0;

            for (int i = 0; i < numInputs; i++)
                inputInts[i] = i;

            HeapsPermAlgorithm(inputInts, numInputs, ref inputOrderAllPerms, ref comboInd);
            
            return inputOrderAllPerms;
        }

        public void HeapsPermAlgorithm(int[] a, int n, ref int[,] inputOrderAllPerms, ref int comboInd)
        {
            if (n == 1)
            {
                // (got a new permutation)
                for (int i = 0; i < a.Length; i++)
                    inputOrderAllPerms[i, comboInd] = a[i];

                comboInd++;
                return;
            }
            for (int i = 0; i < (n - 1); i++)
            {
                HeapsPermAlgorithm(a, n - 1, ref inputOrderAllPerms, ref comboInd);
                // always swap the first when odd,
                // swap the i-th when even
                if (n % 2 == 0) SwapInts(ref a, n - 1, i);
                else SwapInts(ref a, n - 1, 0);
            }
            HeapsPermAlgorithm(a, n - 1, ref inputOrderAllPerms, ref comboInd);
        }

        public void SwapInts(ref int[] a, int pos1, int pos2)
        {
            // Swaps elements in an array.
            //
            int temp = a[pos1]; // Copy the first position's element
            a[pos1] = a[pos2]; // Assign to the second element
            a[pos2] = temp; // Assign to the first element
        }
    

        public string GetTopoFileName(string projectName)
        {
            string topoFilename = "";

            if (projectName == "NW Ohio")
                topoFilename = "NW Ohio\\\\NW Ohio Topo.tif";
            else if (projectName == "Findlay")
                topoFilename = "Findlay\\\\Findlay Topo.tif";
            else if (projectName == "Firewheel")
                topoFilename = "Firewheel\\\\Firewheel Topo small.tif";
            else if (projectName == "Bobcat Bluff")
                topoFilename = "Bobcat Bluff\\\\w001001.adf";

            return topoFilename;
        }

        public string GetLandCoverFileName(string projectName)
        {
            string topoFilename = "";

            if (projectName == "NW Ohio")
                topoFilename = "NW Ohio\\\\NW Ohio Land Cover.tif";
            else if (projectName == "Findlay")
                topoFilename = "Findlay\\\\Findlay Land Cover.tif";
            else if (projectName == "Firewheel")
                topoFilename = "Firewheel\\\\Firewheel LC small.tif";
            else if (projectName == "Bobcat Bluff")
                topoFilename = "Bobcat Bluff\\\\LC1108144640.tif";

            return topoFilename;
        }

        public string[] GetMetFilenames(string projectName)
        {
            if (chkMets.CheckedItems.Count == 0)
                return null;

            string[] metFileNames = new string[chkMets.CheckedItems.Count];

            for (int i = 0; i < chkMets.CheckedItems.Count; i++)
                metFileNames[i] = projectName + "\\\\" + chkMets.CheckedItems[i].ToString();

            return metFileNames;
        }

        

        private void ChkMets_ItemCheckChanged(object sender, EventArgs e)
        {
            
        }

        private void ChkImportMERRA_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void BtnCombineTests_Click(object sender, EventArgs e)
        {            
            

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
         

        private void ChkMets_ItemCheckChanged(object sender, ItemCheckEventArgs e)
        {

        }

        private void BtnCreateTest_Click(object sender, EventArgs e)
        {
            // Generate test with specified parameters
            try
            {
                string fileName = saveToFolder + "\\" + txtCustomFilename.Text;
                string projectName = cboProject.Text;
                DateTime startMERRA = dateCustomMERRAStart.Value;
                DateTime endMERRA = dateCustomMERRAEnd.Value;

                int merraInd = 0;

                if (projectName == "Findlay" || projectName == "NW Ohio")
                    merraInd = 0;
                else if (projectName == "Firewheel")
                    merraInd = 1;

                bool doFiltering = chkDoFilterCustom.Checked;
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

                    string thisFilename = "";

                    if (thisFile.Items.Count != 0)
                        thisFilename = thisFile.SelectedItem.ToString();

                    if (thisInputOrAcion.Items.Count == 0)
                        break;

                    // Import/Delete/Action 1
                    if (thisInputOrAcion.SelectedItem != null)
                        if (thisInputOrAcion.SelectedItem.ToString() == "Topography")
                            WriteTopographySnippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Land Cover")
                            WriteLandCoverSnippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Met TAB file")
                            WriteMetTABSnippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Met TS file")
                            WriteMetTSSnippet(sw, thisFilename, doFiltering);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Turbines")
                            WriteTurbineSnippet(sw, thisFilename, projectName);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Power Curve")
                            WritePowerCurveSnippet(sw, thisFilename);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Zones")
                            WriteZoneSnippet(sw, thisFilename, projectName);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "MERRA")
                            WriteMERRA2Snippet(sw, 0, startMERRA, endMERRA, merraInd);
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
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Create map around turbs")
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

        public void InitializeCboImpDelAct(ref ComboBox thisInputsActions, ComboBox thisCboImpDelAct)
        {
            thisInputsActions.Items.Clear();

            if (thisCboImpDelAct.SelectedItem != null)
            {
                if (thisCboImpDelAct.SelectedItem.ToString() == "Import")
                {                    
                    thisInputsActions.Items.Add("Topography");
                    thisInputsActions.Items.Add("Land Cover");
                    thisInputsActions.Items.Add("Turbines");
                    thisInputsActions.Items.Add("Power Curve");
                    thisInputsActions.Items.Add("Met TAB file");
                    thisInputsActions.Items.Add("Met TS file");
                    thisInputsActions.Items.Add("Zones");
                    thisInputsActions.Items.Add("MERRA");
                }
                else if (thisCboImpDelAct.SelectedItem.ToString() == "Delete")
                {
                    thisInputsActions.Items.Add("Turbines");
                    thisInputsActions.Items.Add("Power Curve");
                    thisInputsActions.Items.Add("Met");
                    thisInputsActions.Items.Add("Zones");
                }
                else if (thisCboImpDelAct.SelectedItem.ToString() == "Action") // Action
                {                    
                    thisInputsActions.Items.Add("Analyze Mets");
                    thisInputsActions.Items.Add("Gen. Turb. Ests.");
                    thisInputsActions.Items.Add("Create Wake Ests.");
                    thisInputsActions.Items.Add("Do Monte Carlo");
                    thisInputsActions.Items.Add("Create wake map");
                    thisInputsActions.Items.Add("Create largest map");
                    thisInputsActions.Items.Add("Create map around turbs");
                    thisInputsActions.Items.Add("Run Round Robin");
                    thisInputsActions.Items.Add("Do Ice Throw");
                    thisInputsActions.Items.Add("Do Shadow");
                    thisInputsActions.Items.Add("Do Noise model");
                    thisInputsActions.Items.Add("Do MCP");
                }
            }
            else
            {
                thisInputsActions.Text = "";
            }
            
        }

        public void PopulateCboFilename(ref ComboBox thisCbo, string inputActDel)
        {            
            string projectName = cboProject.Text;
            thisCbo.Items.Clear();

            if (inputActDel != null)
            {
                if (inputActDel == "Topography")
                {
                    if (projectName == "Findlay")
                        thisCbo.Items.Add("Findlay Topo.tif");
                    else if (projectName == "NW Ohio")
                        thisCbo.Items.Add("NW Ohio Topo.tif");
                    else if (projectName == "Firewheel")
                        thisCbo.Items.Add("w001001.adf");
                }
                else if (inputActDel == "Land Cover")
                {
                    if (projectName == "Findlay")
                        thisCbo.Items.Add("Findlay Land Cover.tif");
                    else if (projectName == "NW Ohio")
                        thisCbo.Items.Add("NW Ohio Land Cover.tif");
                    else if (projectName == "Firewheel")
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
                    if (projectName == "Findlay")
                    {
                        thisCbo.Items.Add("Archbold Findlay coords.TAB");
                        thisCbo.Items.Add("Paulding Findlay coords.TAB");
                        thisCbo.Items.Add("Sullivan Findlay coords.TAB");
                        thisCbo.Items.Add("Wapakoneta Findlay coords.TAB");
                    }                        
                    else if (projectName == "NW Ohio")
                    {
                        thisCbo.Items.Add("Archbold.TAB");
                        thisCbo.Items.Add("Paulding.TAB");
                        thisCbo.Items.Add("Sullivan.TAB");
                        thisCbo.Items.Add("Wapakoneta.TAB");
                    }                        
                    else if (projectName == "Firewheel")
                    {
                        thisCbo.Items.Add("Met_1.TAB");
                        thisCbo.Items.Add("Met_2.TAB");
                        thisCbo.Items.Add("Met_3.TAB");
                        thisCbo.Items.Add("Met_4.TAB");
                    }                   
                }
                else if (inputActDel == "Met TS")
                {
                    if (projectName == "Findlay")
                    {
                        thisCbo.Items.Add("Archbold TS short Findlay coords.csv");
                        thisCbo.Items.Add("Archbold TS Findlay coords.csv");
                        thisCbo.Items.Add("Ashtabula Findlay coords.csv");
                        thisCbo.Items.Add("New Bremen Findlay coords.csv");
                    }
                    else if (projectName == "NW Ohio")
                    {
                        thisCbo.Items.Add("OH - Archbold Formatted - 20180709.csv");
                        thisCbo.Items.Add("OH - Ashtabula Iten Formatted - 20180709.csv");
                        thisCbo.Items.Add("OH - New Bremen Formatted - 20180709.csv");
                        thisCbo.Items.Add("OH - Paulding formatted - 20180709.csv");
                        thisCbo.Items.Add("OH - Pettisville formatted - 20180709.csv");
                        thisCbo.Items.Add("OH - Port Clinton Formatted C3 - 20190813.csv");
                        thisCbo.Items.Add("OH - Wapakoneta Formatted - no Vane95 - 20180709.csv");                        
                    }
                    else if (projectName == "Firewheel")
                    {
                        thisCbo.Items.Add("Met1.csv");
                        thisCbo.Items.Add("Met2.csv");
                        thisCbo.Items.Add("Met3.csv");
                        thisCbo.Items.Add("Met4.csv");
                    }
                }
                else if (inputActDel == "Zones")
                {
                    if (projectName == "Findlay")
                    {
                        thisCbo.Items.Add("Ball 2 Zones.csv");                        
                    }
                    else if (projectName == "NW Ohio")
                    {
                        thisCbo.Items.Add("Ball 2 Zones.csv");
                        thisCbo.Items.Add("Haviland one zone.csv");
                        thisCbo.Items.Add("Haviland zones.csv");
                        thisCbo.Items.Add("Marion zones.csv");                        
                    }
                    else if (projectName == "Firewheel")
                    {
                        thisCbo.Items.Add("Firewheel Zones.csv");                        
                    }
                }
                else if (inputActDel == "MERRA2")
                {
                    thisCbo.Items.Add("Met index = 0");
                    thisCbo.Items.Add("Met index = 1");
                    thisCbo.Items.Add("Met index = 2");
                }
            }
            
        }

        private void CboImpDelAct_1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            InitializeCboImpDelAct(ref cboInputsOrActions_1, cboImpDelAct_1);            
        }

        private void CboImpDelAct_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCboImpDelAct(ref cboInputsOrActions_2, cboImpDelAct_2);
        }

        private void CboImpDelAct_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCboImpDelAct(ref cboInputsOrActions_3, cboImpDelAct_3);
        }

        private void CboImpDelAct_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCboImpDelAct(ref cboInputsOrActions_4, cboImpDelAct_4);
        }

        private void CboImpDelAct_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCboImpDelAct(ref cboInputsOrActions_5, cboImpDelAct_5);
        }

        private void CboImpDelAct_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCboImpDelAct(ref cboInputsOrActions_6, cboImpDelAct_6);
        }

        private void CboImpDelAct_7_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCboImpDelAct(ref cboInputsOrActions_7, cboImpDelAct_7);
        }

        private void CboImpDelAct_8_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCboImpDelAct(ref cboInputsOrActions_8, cboImpDelAct_8);
        }

        private void CboImpDelAct_9_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCboImpDelAct(ref cboInputsOrActions_9, cboImpDelAct_9);
        }

        private void CboImpDelAct_10_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCboImpDelAct(ref cboInputsOrActions_10, cboImpDelAct_10);
        }

        private void CboImpDelAct_11_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCboImpDelAct(ref cboInputsOrActions_11, cboImpDelAct_11);
        }

        private void CboInputsOrActions_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_1.SelectedItem != null)
                PopulateCboFilename(ref cboFilename_1, cboInputsOrActions_1.SelectedItem.ToString());
        }

        private void CboInputsOrActions_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_2.SelectedItem != null)
                PopulateCboFilename(ref cboFilename_2, cboInputsOrActions_2.SelectedItem.ToString());
        }

        private void CboInputsOrActions_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_3.SelectedItem != null)
                PopulateCboFilename(ref cboFilename_3, cboInputsOrActions_3.SelectedItem.ToString());
        }

        private void CboInputsOrActions_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_4.SelectedItem != null)
                PopulateCboFilename(ref cboFilename_4, cboInputsOrActions_4.SelectedItem.ToString());
        }

        private void CboInputsOrActions_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_5.SelectedItem != null)
                PopulateCboFilename(ref cboFilename_5, cboInputsOrActions_5.SelectedItem.ToString());
        }

        private void CboInputsOrActions_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_6.SelectedItem != null)
                PopulateCboFilename(ref cboFilename_6, cboInputsOrActions_6.SelectedItem.ToString());
        }

        private void CboInputsOrActions_7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_7.SelectedItem != null)
                PopulateCboFilename(ref cboFilename_7, cboInputsOrActions_7.SelectedItem.ToString());
        }

        private void CboInputsOrActions_8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_8.SelectedItem != null)
                PopulateCboFilename(ref cboFilename_8, cboInputsOrActions_8.SelectedItem.ToString());
        }

        private void CboInputsOrActions_9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_9.SelectedItem != null)
                PopulateCboFilename(ref cboFilename_9, cboInputsOrActions_9.SelectedItem.ToString());
        }

        private void CboInputsOrActions_10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_10.SelectedItem != null)
                PopulateCboFilename(ref cboFilename_10, cboInputsOrActions_10.SelectedItem.ToString());
        }

        private void CboInputsOrActions_11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInputsOrActions_11.SelectedItem != null)
                PopulateCboFilename(ref cboFilename_11, cboInputsOrActions_11.SelectedItem.ToString());
        }

        private void BtnAllInputCombos_Click_1(object sender, EventArgs e)
        {
            // Create test files for every combination of inputs

            // For TAB files, there are six possible inputs:
            // Topo, Land Cover, Met TABs, Turbines, Power Curves, Zones

            // For TS files, there are seven inputs with one additional input: MERRA2
            string TABorTS = cboTABorTS.SelectedItem.ToString();
            bool InputMERRA = chkImportMERRA.Checked;
            DateTime startMERRA = dateMERRAStart.Value;
            DateTime endMERRA = dateMERRAEnd.Value;

            int numInputs = 6;

            if (TABorTS == "Time Series" && InputMERRA == true)
                numInputs = 7;

            int[,] inputOrder = GetAllPermutations(numInputs);
            int numCombos = inputOrder.GetUpperBound(1) + 1;

            string txtFileName = txtTestFilename.Text;
            string projectName = cboProject.Text;
            int merraInd = 0;

            if (projectName == "Findlay" || projectName == "NW Ohio")
                merraInd = 0;
            else if (projectName == "Firewheel")
                merraInd = 1;
            else if (projectName == "Bobcat Bluff")
                merraInd = 2;

            string topoFile = GetTopoFileName(projectName);
            string landCoverFile = GetLandCoverFileName(projectName);
            string[] metFiles = GetMetFilenames(projectName);

            int modeledHeight = Convert.ToInt32(txtModeledHeight.Text.ToString());
            
            if (metFiles == null)
            {
                MessageBox.Show("Select at least one met site to use in tests.");
                return;
            }

            string turbineFile = cboTurbineSite.SelectedItem.ToString();
            string powerCurveFile = cboPowerCurves.SelectedItem.ToString();
            string zoneFile = cboZones.SelectedItem.ToString();
            
            bool doFiltering = chkDoFiltering.Checked;
            bool doGross = chkDoGrossEsts.Checked;
            bool doNet = chkDoNetEsts.Checked;
            bool doMCP = chkDoMCP.Checked;

            int fileCount = 0;
            
            for (int i = 0; i < numCombos; i++)
            {
                // Generate test with specified parameters
                // Check that MERRA load is called after the met load. If not, go to next 
                int merraOrder = 0;
                int metOrder = 0;

                for (int j = 0; j < numInputs; j++)
                {
                    if (inputOrder[j, i] == 2)
                        metOrder = j;
                    else if (inputOrder[j, i] == 6)
                        merraOrder = j;
                }

                if (merraOrder > metOrder || numInputs == 6)
                {

                    try
                    {
                        string fileName = saveToFolder + "\\" + txtFileName + "_" + (fileCount + 1).ToString();

                        StreamWriter sw = new StreamWriter(fileName);

                        // First, write start of test
                        StreamReader sr = new StreamReader(testFolder + startSnip);
                        while (sr.EndOfStream == false)
                        {
                            string lineToWrite = sr.ReadLine();
                            string trimmedLine = lineToWrite.Trim();

                            if (lineToWrite == "public void ")
                                lineToWrite = lineToWrite + txtFileName + "_" + (i + 1).ToString() + "()";

                            if (trimmedLine.Length > 14)
                                if (trimmedLine.Substring(0, 15) == "string fileName")
                                    lineToWrite = lineToWrite + txtFileName + "_" + (i + 1).ToString() + "\";";

                            if (trimmedLine.Length > 39)
                                if (trimmedLine.Substring(0, 40) == "thisInst.UTM_conversions.UTMZoneNumber =")
                                    lineToWrite = lineToWrite + cboUTMZone.Text + ";";

                            if (trimmedLine.Length > 36)
                                if (trimmedLine.Substring(0, 37) == "thisInst.UTM_conversions.hemisphere =")
                                    lineToWrite = lineToWrite + "\"" + cboNorthOrSouth.Text + "\";";

                            if (trimmedLine.Length > 23)
                                if (trimmedLine.Substring(0, 24) == "thisInst.modeledHeight =")
                                    lineToWrite = lineToWrite + " " + modeledHeight.ToString() + ";";

                            sw.WriteLine(lineToWrite);
                        }
                        
                        // Now create test file using inputs in specfied order

                        for (int j = 0; j < numInputs; j++)
                        {
                            if (inputOrder[j, i] == 0)
                                WriteTopographySnippet(sw, topoFile);
                            else if (inputOrder[j, i] == 1)
                                WriteLandCoverSnippet(sw, landCoverFile);
                            else if (inputOrder[j, i] == 2) // Met sites
                            {
                                for (int m = 0; m < metFiles.Length; m++)
                                {
                                    if (TABorTS == "TABs")
                                        WriteMetTABSnippet(sw, metFiles[m]);
                                    else
                                        WriteMetTSSnippet(sw, metFiles[m], doFiltering);
                                }
                            }
                            else if (inputOrder[j, i] == 3)
                                WriteTurbineSnippet(sw, turbineFile, projectName);
                            else if (inputOrder[j, i] == 4)
                                WritePowerCurveSnippet(sw, powerCurveFile);
                            else if (inputOrder[j, i] == 5)
                                WriteZoneSnippet(sw, zoneFile, projectName);
                            else if (inputOrder[j, i] == 6)
                                for (int m = 0; m < metFiles.Length; m++)
                                    WriteMERRA2Snippet(sw, m, startMERRA, endMERRA, merraInd);
                        }

                        sw.WriteLine();

                        if (doMCP)
                            for (int m = 0; m < metFiles.Length; m++)
                                WriteMCPSnippet(sw, m);

                        if (doGross)
                        {
                            // Do turbine gross estimates
                            WriteTurbineGrossEsts(sw);

                            if (doNet)
                            {
                                // Do Monte Carlo ests
                                WriteDoMonteCarlo(sw);

                                // Do turbine net estimates
                                WriteNetEsts(sw);
                            }
                        }

                        sr = new StreamReader(testFolder + endSnip);
                        while (sr.EndOfStream == false)
                        {
                            string lineToWrite = sr.ReadLine();
                            sw.WriteLine(lineToWrite);
                        }

                        sw.Close();
                        fileCount++;
                    }
                    catch
                    {

                    }
                }
            }
            
        }

        private void BtnCombineTests_Click_1(object sender, EventArgs e)
        {
            // Find all tests that start with filename and merge into one file
            string fileName = txtTestFilename.Text;
            int underScore = fileName.LastIndexOf('_');

            if (underScore != -1)
                fileName = fileName.Substring(0, underScore);

            string combinedFilename = fileName + "_Tests.txt";

            StreamWriter sw = new StreamWriter(combinedTestsFolder + "\\" + combinedFilename);

            string[] testfiles = Directory.GetFiles(saveToFolder, fileName + "*");

            foreach (string testfile in testfiles)
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

            // Delete individual text files
            foreach (string testfile in testfiles)
            {
                File.Delete(testfile);
            }
        }

        private void CboProject_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (okToUpdate)
                UpdateProjectDetails();
        }

        public void UpdateProjectDetails()
        {
            // Update met list, turbine list, zone list, and Met for MERRA list 
            chkMets.Items.Clear();
            cboTurbineSite.Items.Clear();
            cboZones.Items.Clear();
            
            string thisProject = cboProject.SelectedItem.ToString();
            bool isTimeSeries = false;
            if (cboTABorTS.SelectedItem.ToString() == "Time Series")
                isTimeSeries = true;

            if (thisProject == "NW Ohio")
            {
                if (isTimeSeries)
                {
                    chkMets.Items.Add("OH - Archbold Formatted - 20180709.csv", true);
                    chkMets.Items.Add("OH - Ashtabula Iten Formatted - 20180709.csv", true);
                    chkMets.Items.Add("OH - New Bremen Formatted - 20180709.csv", true);
                    chkMets.Items.Add("OH - Paulding formatted - 20180709.csv", true);
                    chkMets.Items.Add("OH - Pettisville formatted - 20180709.csv", true);
                    chkMets.Items.Add("OH - Port Clinton Formatted C3 - 20190813.csv", true);
                    chkMets.Items.Add("OH - Wapakoneta Formatted - no Vane95 - 20180709.csv", true);                                        
                }
                else
                {
                    chkMets.Items.Add("Archbold", true);
                    chkMets.Items.Add("Paulding", true);
                    chkMets.Items.Add("Sullivan", true);
                    chkMets.Items.Add("Wapakoneta", true);                    
                }

                cboTurbineSite.Items.Add("Ball 2 Turbines");
                cboTurbineSite.Items.Add("Findlay Turbine sites");
                cboTurbineSite.Items.Add("Whirlpool W1");
                cboTurbineSite.Items.Add("Harpster");
                cboTurbineSite.Items.Add("Haviland turbines");
                cboTurbineSite.Items.Add("Marion Turbines");
                cboTurbineSite.Items.Add("OE Operating Turbine Sites");
                cboTurbineSite.Items.Add("Paulding Met as Turb");

                cboZones.Items.Add("Ball 2 Zones");
                cboZones.Items.Add("Haviland one zone");
                cboZones.Items.Add("Haviland zones");
                cboZones.Items.Add("Marion zones");

            }
            else if (thisProject == "Findlay")
            {
                if (isTimeSeries)
                {
                    chkMets.Items.Add("Archbold TS short Findlay coords.csv", true);
                    chkMets.Items.Add("Archbold TS Findlay coords.csv", true);
                    chkMets.Items.Add("Ashtabula Findlay coords.csv", true);
                    chkMets.Items.Add("New Bremen Findlay coords.csv", true);                                        
                }
                else
                {
                    chkMets.Items.Add("Archbold Findlay coords", true);
                    chkMets.Items.Add("Paulding Findlay coords", true);
                    chkMets.Items.Add("Sullivan Findlay coords", true);
                    chkMets.Items.Add("Wapakoneta Findlay coords", true);                    
                }

                cboTurbineSite.Items.Add("Ball 2 Turbines");
                cboTurbineSite.Items.Add("Findlay Turbine sites");
                cboTurbineSite.Items.Add("Whirlpool W1");

                cboZones.Items.Add("Ball 2 Zones");

            }
            else if (thisProject == "Firewheel")
            {
                if (isTimeSeries)
                {
                    chkMets.Items.Add("Met_1.csv", true);
                    chkMets.Items.Add("Met_1 short.csv", true);
                    chkMets.Items.Add("Met_2.csv", true);
                    chkMets.Items.Add("Met_2 short.csv", true);
                    chkMets.Items.Add("Met_3.csv", true);
                    chkMets.Items.Add("Met_4.csv", true);
                }
                else
                {
                    chkMets.Items.Add("Met_1", true);
                    chkMets.Items.Add("Met_2", true);
                    chkMets.Items.Add("Met_3", true);
                    chkMets.Items.Add("Met_4", true);                    
                }

                cboTurbineSite.Items.Add("Firewheel turbine sites");
                cboTurbineSite.Items.Add("Firewheel 5 turbine sites");

                cboZones.Items.Add("Firewheel Zones");                
            }
            else if (thisProject == "Bobcat Bluff")
            {
                if (isTimeSeries)
                {
                    chkMets.Items.Add("Met_2001.csv", true);
                    chkMets.Items.Add("Met_5444.csv", true);
                    chkMets.Items.Add("Met_9900.csv", true);
                    chkMets.Items.Add("Met_2001 short.csv", true);
                    chkMets.Items.Add("Met_5444 short.csv", true);
                    chkMets.Items.Add("Met_9900 short.csv", true);
                }
                else
                {
                    chkMets.Items.Add("Met_2001", true);
                    chkMets.Items.Add("Met_5444", true);
                    chkMets.Items.Add("Met_9900", true);
                }

                cboTurbineSite.Items.Add("Bobcat Bluff turbines");
                cboTurbineSite.Items.Add("Bobcat Bluff 3 turbines");

                cboZones.Items.Add("Bobcat Bluff zones");                
            }

            cboTurbineSite.SelectedIndex = 0;
            cboZones.SelectedIndex = 0;            
        }

        private void CboTABorTS_SelectedIndexChanged(object sender, EventArgs e)
        {           

            if (cboTABorTS.SelectedItem.ToString() == "TABs")
            {
                chkImportMERRA.Enabled = false;
                chkDoFiltering.Enabled = false;
                chkDoMCP.Enabled = false;
                dateMERRAStart.Enabled = false;
                dateMERRAEnd.Enabled = false;
            }
            else
            {
                chkImportMERRA.Enabled = true;
                chkDoFiltering.Enabled = true;
                chkDoMCP.Enabled = true;
                dateMERRAStart.Enabled = true;
                dateMERRAEnd.Enabled = true;
            }

            if (okToUpdate)
                UpdateProjectDetails();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProjectDetails();
        }

        public void UpdateCFNList()
        {
            cboCFNFiles.Items.Clear();
            string[] CFNfiles = Directory.GetFiles(continuumFolder, "*.CFM");

            if (CFNfiles == null)
                return;

            for (int i = 0; i < CFNfiles.Length; i++)
            {
                int lastSlash = CFNfiles[i].LastIndexOf("\\") + 1;
                int lastDot = CFNfiles[i].LastIndexOf(".");
                string cfnName = CFNfiles[i].Substring(lastSlash, lastDot - lastSlash);
                cboCFNFiles.Items.Add(cfnName);
            }                

            cboCFNFiles.SelectedIndex = 0;
        }

        private void BtnDeleteAllPerms_Click(object sender, EventArgs e)
        {
            // Generates test files for deleting inputs in every possible order            
            string txtFileName = txtTestFilename.Text;
            string cfnFileName = continuumFolder + "\\" + cboCFNFiles.SelectedItem.ToString();

            if (txtFileName == "")
            {
                MessageBox.Show("Enter a filename");
                return;
            }

            // Things to delete:
            // 1) Mets (TS or TAB)
            // 2) Turbine site (1 or all)
            // 3) Power curve
            // 4) Zones (1 or all)

            int[,] deleteOrder = GetAllPermutations(4);
            int numCombos = deleteOrder.GetUpperBound(1) + 1;                       

            int numMetsToDelete = chkMetsToDelete.CheckedItems.Count;
            int numTurbsToDelete = chkTurbinesToDelete.CheckedItems.Count;
            
            for (int i = 0; i < numCombos; i++)
            {
                string testName = saveToFolder + "\\" + txtFileName + "_" + (i + 1).ToString();

                StreamWriter sw = new StreamWriter(testName);
                string newCFNname = continuumFolder + "\\" + txtFileName + "_" + (i + 1).ToString();

                WriteTestStartDelete(sw, txtFileName + "_" + (i + 1).ToString(), cfnFileName);
                WriteSaveAs(sw, newCFNname);
                
                for (int j = 0; j < 4; j++)
                {
                    if (deleteOrder[j, i] == 0)
                    {
                        for (int m = 0; m < numMetsToDelete; m++)
                            WriteDeleteMetSnippet(sw, chkMetsToDelete.CheckedItems[m].ToString());
                    }
                    else if (deleteOrder[j, i] == 1)
                    {
                        for (int t = 0; t < numTurbsToDelete; t++)
                            WriteDeleteOneTurbineSiteSnippet(sw, chkTurbinesToDelete.CheckedItems[t].ToString());
                    }
                    else if (deleteOrder[j, i] == 2)
                    {
                        string[] crvsToDelete = new string[chkCrvsToDelete.CheckedItems.Count];

                        if (crvsToDelete == null)
                            break;

                        for (int p = 0; p < crvsToDelete.Count(); p++)
                            crvsToDelete[p] = chkCrvsToDelete.CheckedItems[p].ToString();

                        WriteDeletePowerCurve(sw, crvsToDelete);
                    }
                    else if (deleteOrder[j, i] == 3)
                    {
                        string[] zonesToDelete = new string[chkZonesToDelete.CheckedItems.Count];

                        if (zonesToDelete == null)
                            break;

                        for (int z = 0; z < zonesToDelete.Count(); z++)
                            zonesToDelete[z] = chkZonesToDelete.CheckedItems[z].ToString();

                        WriteDeleteZone(sw, zonesToDelete);
                    }
                }

                StreamReader sr = new StreamReader(testFolder + endDeleteSnip);
                while (sr.EndOfStream == false)
                {
                    string lineToWrite = sr.ReadLine();
                    sw.WriteLine(lineToWrite);
                }

                sw.Close();
            }  
        }

        public void WriteDeleteMetSnippet(StreamWriter sw, string metName)
        {
            StreamReader sr = new StreamReader(testFolder + deleteMet);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 10)
                    if (trimmedLine.Substring(0, 11) == "metToDelete")
                        lineToWrite = lineToWrite + "\"" + metName + "\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteDeleteOneTurbineSiteSnippet(StreamWriter sw, string turbineName)
        {
            StreamReader sr = new StreamReader(testFolder + deleteTurbine);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 10)
                    if (trimmedLine.Substring(0, 11) == "turbineName")
                        lineToWrite = lineToWrite + "\"" + turbineName + "\";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteDeletePowerCurve(StreamWriter sw, string[] powerCurveNames)
        {
            if (powerCurveNames == null)
                return;

            int numCrvs = powerCurveNames.Length;

            StreamReader sr = new StreamReader(testFolder + deleteCurves);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 46)
                    if (trimmedLine.Substring(0, 47) == "if (thisInst.lstPowerCurveList.Items[i].Text ==")
                    {
                        for (int i = 0; i < numCrvs; i++)
                        {
                            lineToWrite = lineToWrite + "\"" + powerCurveNames[i] + "\")";
                            sw.WriteLine(lineToWrite);

                            sw.WriteLine("thisInst.lstPowerCurveList.Items[i].Selected = true;");
                            sw.WriteLine();
                        }
                        sr.ReadLine(); // this reads in line "thisInst.lstPowerCurveList.Items[i].Selected = true"
                        lineToWrite = "";
                    }                        

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteDeleteZone(StreamWriter sw, string[] zoneNames)
        {
            if (zoneNames == null)
                return;

            int numZones = zoneNames.Length;

            StreamReader sr = new StreamReader(testFolder + deleteZones);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 37)
                    if (trimmedLine.Substring(0, 38) == "if (thisInst.lstZones.Items[i].Text ==")
                    {
                        for (int i = 0; i < numZones; i++)
                        {                            
                            sw.WriteLine("if (thisInst.lstZones.Items[i].Text == " + "\"" + zoneNames[i] + "\")");
                            sw.WriteLine("\tthisInst.lstZones.Items[i].Selected = true;");
                            sw.WriteLine();
                        }
                        sr.ReadLine(); // this reads in line "thisInst.lstZones.Items[i].Selected = true"
                        lineToWrite = "";
                    }

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteTestStartDelete(StreamWriter sw, string testName, string cfnName)
        {
            // Write start of test for delete test
            StreamReader sr = new StreamReader(testFolder + startDeleteSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();

                if (lineToWrite == "public void ")
                    lineToWrite = lineToWrite + testName + "()";

                if (trimmedLine.Length > 14)
                    if (trimmedLine.Substring(0, 15) == "string fileName")
                    {
                        lineToWrite = "string fileName = \"";
                        sw.Write("\t");

                        for (int i = 0; i < lineToWrite.Length; i++)
                            sw.Write(lineToWrite[i]);

                        for (int i = 0; i < cfnName.Length; i++)
                        {
                            if (cfnName.Substring(i, 1) == "\\")
                            {
                                sw.Write(cfnName.Substring(i, 1));
                                sw.Write("\\");
                            }
                            else
                                sw.Write(cfnName.Substring(i, 1));
                        }

                        sw.Write("\";");
                        lineToWrite = "";
                    }                        

                sw.WriteLine(lineToWrite);
            }

            sr.Close();
        }

        public void WriteSaveAs(StreamWriter sw, string newFileName)
        {
            // Save file using filename
            StreamReader sr = new StreamReader(testFolder + saveAsSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();

                if (trimmedLine.Length > 29)
                    if (trimmedLine.Substring(0, 30) == "thisInst.sfdCFMfile.FileName =")                        
                    {
                        lineToWrite = "thisInst.sfdCFMfile.FileName = \"";
                        sw.Write("\t");

                        for (int i = 0; i < lineToWrite.Length; i++)
                            sw.Write(lineToWrite[i]);

                        for (int i = 0; i < newFileName.Length; i++)
                        {
                            if (newFileName.Substring(i, 1) == "\\")
                            {
                                sw.Write(newFileName.Substring(i, 1));
                                sw.Write("\\");
                            }
                            else
                                sw.Write(newFileName.Substring(i, 1));
                        }

                        sw.Write("\";");
                        lineToWrite = "";
                    }

                sw.WriteLine(lineToWrite);
            }

            sr.Close();
        }

        private void BtnGetMetsAndTurbs_Click(object sender, EventArgs e)
        {
            UpdateDeleteTables();

        }

        public void UpdateDeleteTables()
        {
            string continuumFile = cboCFNFiles.SelectedItem.ToString();
            Continuum thisInst = new Continuum("");

            try
            {
                thisInst.Open(continuumFolder + "\\" + continuumFile + ".cfm");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Continuum file");
            }

            int numMets = thisInst.metList.ThisCount;
            int numTurbines = thisInst.turbineList.TurbineCount;
            int numCrvs = thisInst.turbineList.PowerCurveCount;
            int numZones = thisInst.siteSuitability.GetNumZones();

            // Populate Mets to Delete table
            chkMetsToDelete.Items.Clear();

            for (int i = 0; i < numMets; i++)
                chkMetsToDelete.Items.Add(thisInst.metList.metItem[i].name, true);

            // Populate Turbines to Delete table
            chkTurbinesToDelete.Items.Clear();

            for (int i = 0; i < numTurbines; i++)
                chkTurbinesToDelete.Items.Add(thisInst.turbineList.turbineEsts[i].name, true);

            // Populate Power Curves to Delete table
            chkCrvsToDelete.Items.Clear();

            for (int i = 0; i < numCrvs; i++)
                chkCrvsToDelete.Items.Add(thisInst.turbineList.powerCurves[i].name, true);

            // Populate Zones to Delete table
            chkZonesToDelete.Items.Clear();

            for (int i = 0; i < numZones; i++)
                chkZonesToDelete.Items.Add(thisInst.siteSuitability.zones[i].name, true);

            thisInst.Close();
        }

        private void CboCFNFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDeleteTables();
        }
    }
}
