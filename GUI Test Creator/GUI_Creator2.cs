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
using System.Drawing.Imaging;

namespace GUI_Test_Creator
{
    public partial class GUI_Creator2 : Form
    {
        // TO DO: Get user folder and create folder names
        string unitTestFolder = ""; // Continuum 3 GUI Test folder which contains SaveFolder and TestFolder
        string testFolder = ""; // Contains folders with all input files (i.e. topo, land cover, LT Reference, Met tab, Met TS, Power curves, Turbine sites, Zones
        string snippetFolder = ""; // Contains code snippets used to create GUI unit tests
        string saveToFolder = ""; // Folder where .CFM are saved (and then used in GUI tests)
        string continuumFolder = ""; // 
        string combinedTestsFolder = "";

        string vsTestFolder = ""; // Visual Studio Continuum_Tests folder

        string firewheelMERRA = "";
        string ohioMERRA = "";

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
        string referenceSnip = "\\Import Reference.txt";

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

        // Standard test code
        string mcpTestCode = "\\MCP Test code.txt";

        public GUI_Creator2()
        {
            InitializeComponent();
            MessageBox.Show("Please specify location of folder: Continuum 3 GUI Testing");
            DialogResult goodToGo = fbd_Folder.ShowDialog();

            if (goodToGo == DialogResult.Cancel)
                return;

            unitTestFolder = fbd_Folder.SelectedPath;

            MessageBox.Show("Please specify location of Visual Studio project 'Continuum_Tests'");
            goodToGo = fbd_Folder.ShowDialog();

            if (goodToGo == DialogResult.Cancel)
                return;

            vsTestFolder = fbd_Folder.SelectedPath;

            testFolder = unitTestFolder + "\\TestFolder";
            snippetFolder = unitTestFolder + "\\TestFolder\\Snippets";
            saveToFolder = unitTestFolder + "\\SaveFolder";
            continuumFolder = unitTestFolder + "\\SaveFolder";
            combinedTestsFolder = unitTestFolder + "\\Combined Tests";

            firewheelMERRA = unitTestFolder + "\\TestFolder\\MERRA2 Firewheel";
            ohioMERRA = unitTestFolder + "\\TestFolder\\MERRA2 NW Ohio";

            okToUpdate = false;
            cboProject.SelectedIndex = 0;
            cboTABorTS.SelectedIndex = 0;
            cboNorthOrSouth.SelectedIndex = 0;
            cboPowerCurves.SelectedIndex = 0;
            txtGUI_TestFolder.Text = unitTestFolder;
            txtContinuumTestsVS.Text = vsTestFolder;

            UpdateCFNList();
            UpdateListOfTests();
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
            StreamReader sr = new StreamReader(snippetFolder + topoSnip);
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
            StreamReader sr = new StreamReader(snippetFolder + landCoverSnip);
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
            StreamReader sr = new StreamReader(snippetFolder + metTABSnip);
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
            StreamReader sr = new StreamReader(snippetFolder + metTSSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 8)
                    if (trimmedLine.Substring(0, 9) == "metTSFile")
                        lineToWrite = lineToWrite + thisFile + "\";";
                
                if (trimmedLine.Length > 33)
                    if (trimmedLine.Substring(0, 34) == "thisMet.metData.filteringEnabled =")
                    {
                        if (doFiltering)
                            lineToWrite = lineToWrite + " true;";
                        else
                            lineToWrite = lineToWrite + " false;";
                    }

                if (trimmedLine.Length > 42)
                    if (trimmedLine.Substring(0, 42) == "thisInst.metList.ImportFilterExtrapMetData")
                        lineToWrite = lineToWrite + doFiltering.ToString().ToLower() + ");";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteTurbineSnippet(StreamWriter sw, string thisFile, string projectName)
        {
            StreamReader sr = new StreamReader(snippetFolder + turbineSnip);
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
            StreamReader sr = new StreamReader(snippetFolder + powerCurveSnip);
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
            StreamReader sr = new StreamReader(snippetFolder + zonesSnip);
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

        public void WriteMERRA2Snippet(StreamWriter sw, int metInd, string merraFolder)
        {            
            StreamReader sr = new StreamReader(snippetFolder + referenceSnip);
           
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 6)
                    if (trimmedLine.Substring(0, 7) == "thisMet")
                        lineToWrite = lineToWrite + metInd + "];";

                if (trimmedLine.Length > 19)
                    if (trimmedLine.Substring(0, 20) == "string refDataFolder")
                        lineToWrite = lineToWrite + "\"" + merraFolder.Replace("\\", "\\\\") + "\"" + ";";
                       
                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteReferenceSnippet(StreamWriter sw, int metInd, string referenceName, int numNodes)
        {

            StreamReader sr = new StreamReader(snippetFolder + referenceSnip);
            string refFolderPath = ohioMERRA;

            if (referenceName == "Firewheel")
                refFolderPath = firewheelMERRA;

            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();
                if (trimmedLine.Length > 6)
                    if (trimmedLine.Substring(0, 7) == "thisMet")
                        lineToWrite = lineToWrite + metInd + "];";                               

                if (trimmedLine.Contains("ReadFileAndDefineRefDataDownload"))                    
                    lineToWrite = lineToWrite + refFolderPath + ");";

                if (trimmedLine.Contains("thisRef.numNodes ="))
                    lineToWrite = lineToWrite + numNodes.ToString() + ";";

                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteMCPSnippet(StreamWriter sw, int metInd)
        {
            StreamReader sr = new StreamReader(snippetFolder + mcpSnip);
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
            StreamReader sr = new StreamReader(snippetFolder + genTurbEstsSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteDoMonteCarlo(StreamWriter sw)
        {
            StreamReader sr = new StreamReader(snippetFolder + monteCarloSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                sw.WriteLine(lineToWrite);
            }
            sr.Close();
        }

        public void WriteNetEsts(StreamWriter sw)
        {
            StreamReader sr = new StreamReader(snippetFolder + wakeNetEstSnip);
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
                string fileName = testFolder + "\\" + txtCustomFilename.Text;
                string projectName = cboProject.Text;
                DateTime startMERRA = dateCustomMERRAStart.Value;
                DateTime endMERRA = dateCustomMERRAEnd.Value;
                                
                string merraFolder = ohioMERRA;

                if (projectName == "Firewheel")
                    merraFolder = firewheelMERRA;

                bool doFiltering = chkDoFilterCustom.Checked;
                StreamWriter sw = new StreamWriter(fileName);

                // First, write start of test
                StreamReader sr = new StreamReader(snippetFolder + startSnip);
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
                            WriteMERRA2Snippet(sw, 0, merraFolder);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Gen. Turb. Ests.")
                            WriteTurbineGrossEsts(sw);
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Analyze Mets")
                        {
                            sr = new StreamReader(snippetFolder + analyzeMetsSnip);
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
                            sr = new StreamReader(snippetFolder + wakeMapSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Create largest map")
                        {
                            sr = new StreamReader(snippetFolder + mapLargest);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Create map around turbs")
                        {
                            sr = new StreamReader(snippetFolder + mapTurbine);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Run Round Robin")
                        {
                            sr = new StreamReader(snippetFolder + roundRobinSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Do Ice Throw")
                        {
                            sr = new StreamReader(snippetFolder + iceThrowSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Do Shadow")
                        {
                            sr = new StreamReader(snippetFolder + shadowFlickerSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "Do Noise")
                        {
                            sr = new StreamReader(snippetFolder + noiseModelSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }
                        else if (thisInputOrAcion.SelectedItem.ToString() == "MCP")
                        {
                            sr = new StreamReader(snippetFolder + mcpSnip);
                            while (sr.EndOfStream == false)
                            {
                                string lineToWrite = sr.ReadLine();
                                sw.WriteLine(lineToWrite);
                            }
                            sr.Close();
                        }

                }

                sr = new StreamReader(snippetFolder + endSnip);
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
            string merraFolder = ohioMERRA;

            if (projectName == "Firewheel")
                merraFolder = firewheelMERRA;
            
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

            string folderLoc = testFolder + "\\Tests\\" + cboTestFolder.SelectedItem.ToString();

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
                    string fileName = folderLoc + "\\" + txtFileName + "_" + (fileCount + 1).ToString();
                    StreamWriter sw = new StreamWriter(fileName);

                    try
                    {
                        // First, write start of test
                        StreamReader sr = new StreamReader(snippetFolder + startSnip);
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
                                    WriteMERRA2Snippet(sw, m, merraFolder);
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

                        sr = new StreamReader(snippetFolder + endSnip);
                        while (sr.EndOfStream == false)
                        {
                            string lineToWrite = sr.ReadLine();
                            sw.WriteLine(lineToWrite);
                        }

                        sw.Close();
                        fileCount++;
                    }
                    catch (Exception ex)
                    {
                        sw.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            
        }

        public void WriteClassStart(StreamWriter sw, string className)
        {
            sw.WriteLine("using System;");
            sw.WriteLine("using Microsoft.VisualStudio.TestTools.UnitTesting;");
            sw.WriteLine("using ContinuumNS;");
            sw.WriteLine("using System.Diagnostics;");
            sw.WriteLine("using System.IO;");
            sw.WriteLine("using System.Threading;");
            sw.WriteLine();

            sw.WriteLine("namespace Continuum_Tests.GUI_Tests");
            sw.WriteLine("{");
            sw.WriteLine("   [TestClass]");
            sw.WriteLine("   public class " + className);
            sw.WriteLine("   {");
            sw.WriteLine("      string testingFolder;");
            sw.WriteLine("      string saveFolder;");
            sw.WriteLine("      string refFolder;");
            sw.WriteLine("      Globals globals = new Globals();");
            sw.WriteLine();
            sw.WriteLine("      string metTSFile;");
            sw.WriteLine("      string metName;");
            sw.WriteLine("      string MCP_Method;");
            sw.WriteLine("      Met thisMet;");
            sw.WriteLine("      UTM_conversion.Lat_Long theseLL;");
            sw.WriteLine("      int offset;");
            sw.WriteLine("      Reference thisRef;");
            sw.WriteLine();

            // Constructor
            sw.WriteLine("      public " + className + "()");
            sw.WriteLine("      {");
            sw.WriteLine("         testingFolder = globals.testFolder;");
            sw.WriteLine("         saveFolder = globals.saveFolder;");
            sw.WriteLine("         refFolder = globals.merraFolder;");
            sw.WriteLine("      }");
            sw.WriteLine();
                       

        }

        public void WriteDeleteClassStart(StreamWriter sw, string className)
        {
            sw.WriteLine("using System;");
            sw.WriteLine("using Microsoft.VisualStudio.TestTools.UnitTesting;");
            sw.WriteLine("using ContinuumNS;");
            sw.WriteLine("using System.Diagnostics;");
            sw.WriteLine("using System.IO;");
            sw.WriteLine("using System.Threading;");
            sw.WriteLine();

            sw.WriteLine("namespace Continuum_Tests.GUI_Tests");
            sw.WriteLine("{");
            sw.WriteLine("   [TestClass]");
            sw.WriteLine("   public class " + className);
            sw.WriteLine("   {");
            sw.WriteLine("      string testingFolder;");
            sw.WriteLine("      string saveFolder;");
            sw.WriteLine("      string refFolder;");
            sw.WriteLine("      Globals globals = new Globals();");
            sw.WriteLine();            
            sw.WriteLine("      string metToDelete;");            
            sw.WriteLine("      int turbCount;");
            sw.WriteLine("      string turbineName;");
            sw.WriteLine("      int crvCount;");
            sw.WriteLine("      int numToDelete;");
            sw.WriteLine("      int numZones;");
            sw.WriteLine("      int numZonesToDelete;");           
            sw.WriteLine("      Reference thisRef;");
            sw.WriteLine();

            // Constructor
            sw.WriteLine("      public " + className + "()");
            sw.WriteLine("      {");
            sw.WriteLine("         testingFolder = globals.testFolder;");
            sw.WriteLine("         saveFolder = globals.saveFolder;");
            sw.WriteLine("         refFolder = globals.merraFolder;");
            sw.WriteLine("      }");
            sw.WriteLine();


        }

        public int CombineTestsToOneFile(string fileName, string testFolderName, int numTestsToKeep, bool isDeleteTest = false)
        {
            // Finds all tests that start with filename and randomly picks subset (numTestsToKeep) and merges into one file
            // Returns the first (randomly chosen) test number (used for Delete tests)
     //       int underScore = fileName.LastIndexOf('_');
            int firstTestNum = -999;

      //      if (underScore != -1)
      //          fileName = fileName.Substring(0, underScore);

            // Ask user where to save the .cs file
            string combinedFilename = fileName + "_Tests.cs";
            combinedFilename = testFolder + "\\Tests\\GUI Tests\\" + testFolderName + "\\" + combinedFilename;

            if (File.Exists(combinedFilename))
                File.Delete(combinedFilename);

            StreamWriter sw = new StreamWriter(combinedFilename);

            // First, write start of class
            if (isDeleteTest)
                WriteDeleteClassStart(sw, fileName);
            else
                WriteClassStart(sw, fileName);

            string[] testfiles = Directory.GetFiles(testFolder + "\\Tests\\GUI Tests\\" + testFolderName, fileName + "_*");
            int numTestsAll = testfiles.Length;

            if (numTestsToKeep < numTestsAll)
            {
                // Randomly delete testfiles until reaches numTestsToKeep
                Random random = new Random();

                while (numTestsToKeep < numTestsAll)
                {
                    double randDecimal = random.NextDouble();
                    int testToDel = Convert.ToInt32(randDecimal * numTestsAll);

                    string[] tempArray = new string[testfiles.Length - 1];
                    int tempInd = 0;

                    if (testToDel >= testfiles.Length - 1)
                        testToDel--;
                    
                    for (int t = 0; t < testfiles.Length; t++)
                    {
                        if (t != testToDel)
                        {
                            tempArray[tempInd] = testfiles[t];
                            tempInd++;
                        }                        
                    }

                    testfiles = tempArray;
                    numTestsAll = testfiles.Length;
                }
            }

            foreach (string testfile in testfiles)
            {
                if (testfile == combinedFilename)
                    continue;

                StreamReader sr = new StreamReader(testfile);

                while (sr.EndOfStream == false)
                {
                    string lineToWrite = sr.ReadLine();
                    sw.WriteLine(lineToWrite);
                }

                sw.WriteLine();
                sr.Close();
            }

            sw.WriteLine("}");
            sw.WriteLine("}");

            sw.Close();

            int underScoreInd = testfiles[0].LastIndexOf("_");
            firstTestNum = Convert.ToInt32(testfiles[0].Substring(underScoreInd + 1, testfiles[0].Length - underScoreInd - 1));

            // Delete individual text files
            testfiles = Directory.GetFiles(testFolder + "\\Tests\\GUI Tests\\" + testFolderName, fileName + "_*");

            foreach (string testfile in testfiles)
            {
                if (testfile == combinedFilename)
                    continue;

                File.Delete(testfile);
            }
                       
            string vsFileName = vsTestFolder + "\\GUI Tests\\" + testFolderName + "\\" + fileName + "_Tests.cs";

            if (File.Exists(vsFileName))
                File.Delete(vsFileName);

            File.Copy(combinedFilename, vsFileName);

            UpdateListOfTests();                      
                        
            return firstTestNum;
        }

        private void BtnCombineTests_Click_1(object sender, EventArgs e)
        {
            // Find all tests that start with filename and merge into one file
            string fileName = txtTestFilename.Text;
            int underScore = fileName.LastIndexOf('_');

            if (underScore != -1)
                fileName = fileName.Substring(0, underScore);

            // Ask user where to save the .cs file
            string combinedFilename = fileName + "_Tests.cs";
            combinedFilename = testFolder + "\\Tests\\" + cboTestFolder.SelectedItem.ToString() + "\\" + combinedFilename;

            if (File.Exists(combinedFilename))
                File.Delete(combinedFilename);

            StreamWriter sw = new StreamWriter(combinedFilename);

            // First, write start of class
            WriteClassStart(sw, fileName);

            string[] testfiles = Directory.GetFiles(testFolder + "\\Tests\\" + cboTestFolder.SelectedItem.ToString(), fileName + "_*");

            foreach (string testfile in testfiles)
            {
                if (testfile == combinedFilename)
                    continue;

                StreamReader sr = new StreamReader(testfile);

                while (sr.EndOfStream == false)
                {
                    string lineToWrite = sr.ReadLine();
                    sw.WriteLine(lineToWrite);
                }

                sw.WriteLine();
                sr.Close();
            }

            sw.WriteLine("}");
            sw.WriteLine("}");

            sw.Close();

            // Delete individual text files
            foreach (string testfile in testfiles)
            {
                if (testfile == combinedFilename)
                    continue;

                File.Delete(testfile);
            }

            UpdateListOfTests();
        }

        private void CboProject_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (okToUpdate)
                UpdateProjectDetails("Create Tests All Perms");
        }

        public string[] GetTurbineFilesAtProject(string projectName)
        {
            string[] turbineFiles = new string[0];

            if (projectName == "NW Ohio")
            {
                turbineFiles = new string[8];
                turbineFiles[0] = "Ball 2 Turbines";
                turbineFiles[1] = "Findlay Turbine sites";
                turbineFiles[2] = "Whirlpool W1";
                turbineFiles[3] = "Harpster";
                turbineFiles[4] = "Haviland turbines";
                turbineFiles[5] = "Marion Turbines";
                turbineFiles[6] = "OE Operating Turbine Sites";
                turbineFiles[7] = "Paulding Met as Turb";               
            }
            else if (projectName == "Findlay")
            {
                turbineFiles = new string[3];
                turbineFiles[0] = "Ball 2 Turbines";
                turbineFiles[1] = "Findlay Turbine sites";
                turbineFiles[2] = "Whirlpool W1";               
            }
            else if (projectName == "Firewheel")
            {
                turbineFiles = new string[2];
                turbineFiles[0] = "Firewheel turbine sites";
                turbineFiles[1] = "Firewheel 5 turbine sites";                
            }
            else if (projectName == "Bobcat Bluff")
            {
                turbineFiles = new string[2];
                turbineFiles[0] = "Bobcat Bluff turbines";
                turbineFiles[1] = "Bobcat Bluff 3 turbines";               
            }

            return turbineFiles;
        }

        public string[] GetZonesAtProject(string projectName)
        {
            string[] zoneFiles = new string[0];

            if (projectName == "NW Ohio")
            {
                zoneFiles = new string[4];
                zoneFiles[0] = "Ball 2 Zones";
                zoneFiles[1] = "Haviland one zone";
                zoneFiles[2] = "Haviland zones";
                zoneFiles[3] = "Marion zones";                
            }
            else if (projectName == "Findlay")
            {
                zoneFiles = new string[1];
                zoneFiles[0] = "Ball 2 Zones";                
            }
            else if (projectName == "Firewheel")
            {
                zoneFiles = new string[1];
                zoneFiles[0] = "Firewheel Zones";                
            }
            else if (projectName == "Bobcat Bluff")
            {
                zoneFiles = new string[1];
                zoneFiles[0] = "Bobcat Bluff zones";
            }

            return zoneFiles;
        }

        public string[] GetMetsAtProject(string projectName, string TABorTS)
        {
            string[] metNames = new string[0];

            if (projectName == "NW Ohio")
            {
                if (TABorTS == "TS")
                {
                    metNames = new string[7];
                    metNames[0] = projectName + "\\\\OH - Archbold Formatted - 20180709.csv";
                    metNames[1] = projectName + "\\\\OH - Ashtabula Iten Formatted - 20180709.csv";
                    metNames[2] = projectName + "\\\\OH - New Bremen Formatted - 20180709.csv";
                    metNames[3] = projectName + "\\\\OH - Paulding formatted - 20180709.csv";
                    metNames[4] = projectName + "\\\\OH - Pettisville formatted - 20180709.csv";
                    metNames[5] = projectName + "\\\\OH - Port Clinton Formatted C3 - 20190813.csv";
                    metNames[6] = projectName + "\\\\OH - Wapakoneta Formatted - no Vane95 - 20180709.csv";                                        
                }
                else
                {
                    metNames = new string[4];
                    metNames[0] = projectName + "\\\\Archbold";
                    metNames[1] = projectName + "\\\\Paulding";
                    metNames[2] = projectName + "\\\\Sullivan";
                    metNames[3] = projectName + "\\\\Wapakoneta";                    
                }
            }
            else if (projectName == "Findlay")
            {
                if (TABorTS == "TS")
                {
                    metNames = new string[4];
                    metNames[0] = projectName + "\\\\Archbold TS short Findlay coords.csv";
                    metNames[1] = projectName + "\\\\Archbold TS Findlay coords.csv";
                    metNames[2] = projectName + "\\\\Ashtabula Findlay coords.csv";
                    metNames[3] = projectName + "\\\\New Bremen Findlay coords.csv";                   
                }
                else
                {
                    metNames = new string[4];
                    metNames[0] = projectName + "\\\\Archbold Findlay coords";
                    metNames[1] = projectName + "\\\\Paulding Findlay coords";
                    metNames[2] = projectName + "\\\\Sullivan Findlay coords";
                    metNames[3] = projectName + "\\\\Wapakoneta Findlay coords";                   
                } 
            }
            else if (projectName == "Firewheel")
            {
                if (TABorTS == "TS")
                {
                    metNames = new string[6];
                    metNames[0] = projectName + "\\\\Met_1.csv";
                    metNames[1] = projectName + "\\\\Met_1 short.csv";
                    metNames[2] = projectName + "\\\\Met_2.csv";
                    metNames[3] = projectName + "\\\\Met_2 short.csv";
                    metNames[4] = projectName + "\\\\Met_3.csv";
                    metNames[5] = projectName + "\\\\Met_4.csv";                    
                }
                else
                {
                    metNames = new string[4];
                    metNames[0] = projectName + "\\\\Met_1";
                    metNames[1] = projectName + "\\\\Met_2";
                    metNames[2] = projectName + "\\\\Met_3";
                    metNames[3] = projectName + "\\\\Met_4";                   
                } 
            }
            else if (projectName == "Bobcat Bluff")
            {
                if (TABorTS == "TS")
                {
                    metNames = new string[6];
                    metNames[0] = projectName + "\\\\Met_2001.csv";
                    metNames[1] = projectName + "\\\\Met_5444.csv";
                    metNames[2] = projectName + "\\\\Met_9900.csv";
                    metNames[3] = projectName + "\\\\Met_2001 short.csv";
                    metNames[4] = projectName + "\\\\Met_5444 short.csv";
                    metNames[5] = projectName + "\\\\Met_9900 short.csv";                   
                }
                else
                {
                    metNames = new string[6];
                    metNames[0] = projectName + "\\\\Met_2001";
                    metNames[1] = projectName + "\\\\Met_5444";
                    metNames[2] = projectName + "\\\\Met_9900";                   
                }                
            }

            return metNames;
        }

        public string[] GetMetsNamesAtProject(string projectName, string TABorTS)
        {
            string[] metNames = new string[0];

            if (projectName == "NW Ohio")
            {
                if (TABorTS == "TS")
                {
                    metNames = new string[7];
                    metNames[0] = "Archbold";
                    metNames[1] = "Ashtabula";
                    metNames[2] = "New Bremen";
                    metNames[3] = "Paulding";
                    metNames[4] = "Pettisville";
                    metNames[5] = "Port Clinton";
                    metNames[6] = "Wapakoneta";
                }
                else
                {
                    metNames = new string[4];
                    metNames[0] = "Archbold";
                    metNames[1] = "Paulding";
                    metNames[2] = "Sullivan";
                    metNames[3] = "Wapakoneta";
                }
            }
            else if (projectName == "Findlay")
            {
                if (TABorTS == "TS")
                {
                    metNames = new string[4];
                    metNames[0] = "Archbold";
                    metNames[1] = "Archbold";
                    metNames[2] = "Ashtabula";
                    metNames[3] = "New Bremen";
                }
                else
                {
                    metNames = new string[4];
                    metNames[0] = "Archbold";
                    metNames[1] = "Paulding";
                    metNames[2] = "Sullivan";
                    metNames[3] = "Wapakoneta";
                }
            }
            else if (projectName == "Firewheel")
            {
                if (TABorTS == "TS")
                {
                    metNames = new string[6];
                    metNames[0] = "Met_1";
                    metNames[1] = "Met_1";
                    metNames[2] = "Met_2";
                    metNames[3] = "Met_2";
                    metNames[4] = "Met_3";
                    metNames[5] = "Met_4";
                }
                else
                {
                    metNames = new string[4];
                    metNames[0] = "Met_1";
                    metNames[1] = "Met_2";
                    metNames[2] = "Met_3";
                    metNames[3] = "Met_4";
                }
            }
            else if (projectName == "Bobcat Bluff")
            {
                if (TABorTS == "TS")
                {
                    metNames = new string[6];
                    metNames[0] = "Met_2001";
                    metNames[1] = "Met_5444";
                    metNames[2] = "Met_9900";
                    metNames[3] = "Met_2001";
                    metNames[4] = "Met_5444";
                    metNames[5] = "Met_9900";
                }
                else
                {
                    metNames = new string[6];
                    metNames[0] = "Met_2001";
                    metNames[1] = "Met_5444";
                    metNames[2] = "Met_9900";
                }
            }

            return metNames;
        }

        public void UpdateProjectDetails(string tabName)
        {
            // Update met list, turbine list, zone list, and Met for MERRA list 
            CheckedListBox thisChkMets = new CheckedListBox();
            ComboBox thisTurbCbo = new ComboBox();
            ComboBox thisZoneCbo = new ComboBox();

            if (tabName == "Create Tests All Perms")
            {
                thisChkMets = chkMets;
                thisTurbCbo = cboTurbineSite;
                thisZoneCbo = cboZones;
            }
            else if (tabName == "Maps")
            {
                thisChkMets = chkListMetsMaps;
                thisTurbCbo = cboTurbSitesMaps;
            }
                        
            thisChkMets.Items.Clear();
            thisTurbCbo.Items.Clear();
            thisZoneCbo.Items.Clear();
            
            string thisProject = cboProject.SelectedItem.ToString();
            bool isTimeSeries = false;
            if (cboTABorTS.SelectedItem.ToString() == "Time Series")
                isTimeSeries = true;

            if (thisProject == "NW Ohio")
            {
                if (isTimeSeries)
                {
                    thisChkMets.Items.Add("OH - Archbold Formatted - 20180709.csv", true);
                    thisChkMets.Items.Add("OH - Ashtabula Iten Formatted - 20180709.csv", true);
                    thisChkMets.Items.Add("OH - New Bremen Formatted - 20180709.csv", true);
                    thisChkMets.Items.Add("OH - Paulding formatted - 20180709.csv", true);
                    thisChkMets.Items.Add("OH - Pettisville formatted - 20180709.csv", true);
                    thisChkMets.Items.Add("OH - Port Clinton Formatted C3 - 20190813.csv", true);
                    thisChkMets.Items.Add("OH - Wapakoneta Formatted - no Vane95 - 20180709.csv", true);                                        
                }
                else
                {
                    thisChkMets.Items.Add("Archbold", true);
                    thisChkMets.Items.Add("Paulding", true);
                    thisChkMets.Items.Add("Sullivan", true);
                    thisChkMets.Items.Add("Wapakoneta", true);                    
                }

                thisTurbCbo.Items.Add("Ball 2 Turbines");
                thisTurbCbo.Items.Add("Findlay Turbine sites");
                thisTurbCbo.Items.Add("Whirlpool W1");
                thisTurbCbo.Items.Add("Harpster");
                thisTurbCbo.Items.Add("Haviland turbines");
                thisTurbCbo.Items.Add("Marion Turbines");
                thisTurbCbo.Items.Add("OE Operating Turbine Sites");
                thisTurbCbo.Items.Add("Paulding Met as Turb");

                thisZoneCbo.Items.Add("Ball 2 Zones");
                thisZoneCbo.Items.Add("Haviland one zone");
                thisZoneCbo.Items.Add("Haviland zones");
                thisZoneCbo.Items.Add("Marion zones");

            }
            else if (thisProject == "Findlay")
            {
                if (isTimeSeries)
                {
                    thisChkMets.Items.Add("Archbold TS short Findlay coords.csv", true);
                    thisChkMets.Items.Add("Archbold TS Findlay coords.csv", true);
                    thisChkMets.Items.Add("Ashtabula Findlay coords.csv", true);
                    thisChkMets.Items.Add("New Bremen Findlay coords.csv", true);                                        
                }
                else
                {
                    thisChkMets.Items.Add("Archbold Findlay coords", true);
                    thisChkMets.Items.Add("Paulding Findlay coords", true);
                    thisChkMets.Items.Add("Sullivan Findlay coords", true);
                    thisChkMets.Items.Add("Wapakoneta Findlay coords", true);                    
                }

                thisTurbCbo.Items.Add("Ball 2 Turbines");
                thisTurbCbo.Items.Add("Findlay Turbine sites");
                thisTurbCbo.Items.Add("Whirlpool W1");

                thisZoneCbo.Items.Add("Ball 2 Zones");

            }
            else if (thisProject == "Firewheel")
            {
                if (isTimeSeries)
                {
                    thisChkMets.Items.Add("Met_1.csv", true);
                    thisChkMets.Items.Add("Met_1 short.csv", true);
                    thisChkMets.Items.Add("Met_2.csv", true);
                    thisChkMets.Items.Add("Met_2 short.csv", true);
                    thisChkMets.Items.Add("Met_3.csv", true);
                    thisChkMets.Items.Add("Met_4.csv", true);
                }
                else
                {
                    thisChkMets.Items.Add("Met_1", true);
                    thisChkMets.Items.Add("Met_2", true);
                    thisChkMets.Items.Add("Met_3", true);
                    thisChkMets.Items.Add("Met_4", true);                    
                }

                thisTurbCbo.Items.Add("Firewheel turbine sites");
                thisTurbCbo.Items.Add("Firewheel 5 turbine sites");

                thisZoneCbo.Items.Add("Firewheel Zones");                
            }
            else if (thisProject == "Bobcat Bluff")
            {
                if (isTimeSeries)
                {
                    thisChkMets.Items.Add("Met_2001.csv", true);
                    thisChkMets.Items.Add("Met_5444.csv", true);
                    thisChkMets.Items.Add("Met_9900.csv", true);
                    thisChkMets.Items.Add("Met_2001 short.csv", true);
                    thisChkMets.Items.Add("Met_5444 short.csv", true);
                    thisChkMets.Items.Add("Met_9900 short.csv", true);
                }
                else
                {
                    thisChkMets.Items.Add("Met_2001", true);
                    thisChkMets.Items.Add("Met_5444", true);
                    thisChkMets.Items.Add("Met_9900", true);
                }

                thisTurbCbo.Items.Add("Bobcat Bluff turbines");
                thisTurbCbo.Items.Add("Bobcat Bluff 3 turbines");

                thisZoneCbo.Items.Add("Bobcat Bluff zones");                
            }

            thisTurbCbo.SelectedIndex = 0;
            thisZoneCbo.SelectedIndex = 0;            
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
                UpdateProjectDetails("Create Tests All Perms");
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProjectDetails("Create Tests All Perms");
        }

        public void UpdateCFNList()
        {
            cboCFNFiles.Items.Clear();
            cboCFM_ForMCP_Test.Items.Clear();

            string[] CFNfiles = Directory.GetFiles(continuumFolder, "*.CFM", SearchOption.AllDirectories);

            if (CFNfiles == null)
                return;

            if (CFNfiles.Length == 0)
                return;

            for (int i = 0; i < CFNfiles.Length; i++)
            {
                int lastSlash = CFNfiles[i].LastIndexOf("\\") + 1;
                int lastDot = CFNfiles[i].LastIndexOf(".");
                string cfnName = CFNfiles[i].Substring(lastSlash, lastDot - lastSlash);
                cboCFNFiles.Items.Add(cfnName);
                cboCFM_ForMCP_Test.Items.Add(cfnName);
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
                string testName = testFolder + "\\" + txtFileName + "_" + (i + 1).ToString();

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

                        WriteDeletePowerCurves(sw, crvsToDelete);
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

                StreamReader sr = new StreamReader(snippetFolder + endDeleteSnip);
                while (sr.EndOfStream == false)
                {
                    string lineToWrite = sr.ReadLine();
                    sw.WriteLine(lineToWrite);
                }

                sw.Close();
            }  
        }

        public string[] ReadAndGetTurbineNames(string projectName, string turbineFile)
        {
            string[] turbNames = new string[0];

            StreamReader sr = new StreamReader(testFolder + "\\Turbine sites\\" + projectName + "\\" + turbineFile + ".csv");

            while (sr.EndOfStream == false)
            {
                string[] turbNameLL = sr.ReadLine().Split(',');
                int numTurbs = turbNames.Length;
                Array.Resize(ref turbNames, numTurbs + 1);
                turbNames[turbNames.Length - 1] = turbNameLL[0];
            }

            sr.Close();

            return turbNames;
        }

        public string[] ReadAndGetZoneNames(string projectName, string zoneFile)
        {
            string[] zoneNames = new string[0];

            StreamReader sr = new StreamReader(testFolder + "\\Zones\\" + projectName + "\\" + zoneFile + ".csv");

            while (sr.EndOfStream == false)
            {
                string[] zoneNameLL = sr.ReadLine().Split(',');
                int numTurbs = zoneNames.Length;
                Array.Resize(ref zoneNames, numTurbs + 1);
                zoneNames[zoneNames.Length - 1] = zoneNameLL[0];
            }

            sr.Close();

            return zoneNames;
        }

        public void CreateDeleteTests(string[] metsToDel, string testFolderName, string setupTestName, int setupTestNum, string testFilename, string projectName, string TABorTS, string turbineFile, 
            string powerCurveFile, string zoneFile)
        {
            // Generates test files for deleting inputs in every possible order using Continuum file created for specified test            
                        
            string cfnFileName = setupTestName + "_" + setupTestNum + ".cfm";
            
            // Things to delete:
            // 1) Mets (TS or TAB)
            // 2) Turbine site (1 or all)
            // 3) Power curve
            // 4) Zones (1 or all)

            int[,] deleteOrder = GetAllPermutations(4);
            int numCombos = deleteOrder.GetUpperBound(1) + 1;
            
            int numMetsToDelete = metsToDel.Length;
            string[] turbsToDelete = ReadAndGetTurbineNames(projectName, turbineFile);
            int numTurbsToDelete = turbsToDelete.Length;

            string[] powerCrvsToDel = new string[1];
            powerCrvsToDel[0] = powerCurveFile;

            string[] zonesToDel = ReadAndGetZoneNames(projectName, zoneFile);

            for (int i = 0; i < numCombos; i++)
            {
                string testName = testFolder + "\\Tests\\GUI Tests\\" + testFolderName + "\\" + testFilename + "_" + (i + 1).ToString();

                StreamWriter sw = new StreamWriter(testName);
               
                WriteTestStartDelete(sw, testFilename + "_" + (i + 1).ToString(), cfnFileName);
               
                for (int j = 0; j < 4; j++)
                {
                    if (deleteOrder[j, i] == 0)
                    {
                        for (int m = 0; m < numMetsToDelete; m++)
                            WriteDeleteMetSnippet(sw, metsToDel[m]);
                    }
                    else if (deleteOrder[j, i] == 1)
                    {
                        for (int t = 0; t < numTurbsToDelete; t++)
                            WriteDeleteOneTurbineSiteSnippet(sw, turbsToDelete[t]);
                    }
                    else if (deleteOrder[j, i] == 2)                    
                        WriteDeletePowerCurves(sw, powerCrvsToDel);                    
                    else if (deleteOrder[j, i] == 3)                    
                        WriteDeleteZone(sw, zonesToDel);                    
                }

                StreamReader sr = new StreamReader(snippetFolder + endDeleteSnip);
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
            StreamReader sr = new StreamReader(snippetFolder + deleteMet);
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
            StreamReader sr = new StreamReader(snippetFolder + deleteTurbine);
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

        public void WriteDeletePowerCurves(StreamWriter sw, string[] powerCurveNames)
        {
            if (powerCurveNames == null)
                return;

            int numCrvs = powerCurveNames.Length;

            StreamReader sr = new StreamReader(snippetFolder + deleteCurves);
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

            StreamReader sr = new StreamReader(snippetFolder + deleteZones);
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
            StreamReader sr = new StreamReader(snippetFolder + startDeleteSnip);
            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();

                if (lineToWrite == "public void ")
                    lineToWrite = lineToWrite + testName + "()";

                if (trimmedLine.Length > 14)
                    if (trimmedLine.Substring(0, 15) == "string fileName")                    
                        lineToWrite = "string fileName = saveFolder + " + "\"" + "\\\\" + cfnName  +"\"" + ";";                                             
                                        
                sw.WriteLine(lineToWrite);
            }

            sr.Close();
        }

        public void WriteSaveAs(StreamWriter sw, string newFileName)
        {
            // Save file using filename
            StreamReader sr = new StreamReader(snippetFolder + saveAsSnip);
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

            continuumFile = continuumFolder + "\\" + continuumFile + ".cfm";
            Continuum thisInst = new Continuum(continuumFile);

 /*           try
            {
                thisInst.Open(continuumFolder + "\\" + continuumFile + ".cfm");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Continuum file");
            }
 */
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

        private void btnCreateMCP_Tests_Click(object sender, EventArgs e)
        {
            // Create MCP_Tests.cs file which tests running each type of MCP algorithm
            string selCFM_file = saveToFolder + "\\" + cboCFM_ForMCP_Test.SelectedItem.ToString();

            string fileName = testFolder + "\\Tests\\MCP_Tests.cs";
            StreamWriter sw = new StreamWriter(fileName);

            // First, write start of test
            StreamReader sr = new StreamReader(snippetFolder + "\\" + mcpTestCode);

            while (sr.EndOfStream == false)
            {
                string lineToWrite = sr.ReadLine();
                string trimmedLine = lineToWrite.Trim();

                if (lineToWrite.Contains("string testingFolder = "))
                    lineToWrite = lineToWrite + "\"" + testFolder + "\";";

                if (lineToWrite.Contains("string saveFolder = "))
                    lineToWrite = lineToWrite + "\"" + saveToFolder + "\";";
                                
                if (lineToWrite.Contains("string fileName = "))
                    lineToWrite = lineToWrite + "\"" + selCFM_file + "\";";                               

                sw.WriteLine(lineToWrite);
            }

            sr.Close();
            sw.Close();

            UpdateListOfTests();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        public void UpdateListOfTests()
        {
            lstTestsInC3GUI_TestsFolder.Items.Clear();
            lstTestsInVS_Folder.Items.Clear();

            // Update list of tests in 'Continuum 3 GUI Testing' folder
            string[] allTests = Directory.GetFiles(testFolder, "*.cs", SearchOption.AllDirectories);

            for (int t = 0; t < allTests.Length; t++)
                lstTestsInC3GUI_TestsFolder.Items.Add(allTests[t].Replace(testFolder + "\\Tests\\", ""));

            // Update list of tests in Visual Studio Continuum_Tests project folder
            allTests = Directory.GetFiles(vsTestFolder, "*.cs", SearchOption.AllDirectories);

            for (int t = 0; t < allTests.Length; t++)
                lstTestsInVS_Folder.Items.Add(allTests[t].Replace(vsTestFolder + "\\", ""));
        }

        private void btnClearTestsFromC3GUIFolder_Click(object sender, EventArgs e)
        {
            // Clear all tests from Continuum 3 GUI Test folder

            string[] allTests = Directory.GetFiles(testFolder, "*.cs", SearchOption.AllDirectories);

            for (int t = 0; t < allTests.Length; t++)
                File.Delete(allTests[t]);

            UpdateListOfTests();
        }

        private void btnClearSelectedTests_Click(object sender, EventArgs e)
        {
            // Clear selected tests from 'Continuum 3 GUI Test folder'

            if (lstTestsInC3GUI_TestsFolder.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a test(s) to clear from 'Continuum 3 GUI Testing' folder.");
                return;
            }

            for (int t = 0; t < lstTestsInC3GUI_TestsFolder.SelectedItems.Count; t++)
                File.Delete(testFolder + "\\Tests\\" + lstTestsInC3GUI_TestsFolder.SelectedItems[t].Text);

            UpdateListOfTests();
        }

        private void btnClearTestsFromVS_Click(object sender, EventArgs e)
        {
            // Clear all tests from Visual Studio project folder
            string[] allTests = Directory.GetFiles(vsTestFolder, "*.cs", SearchOption.AllDirectories);

            for (int t = 0; t < allTests.Length; t++)
                File.Delete(allTests[t]);

            UpdateListOfTests();

        }

        private void btnClearSelectedFromVS_Click(object sender, EventArgs e)
        {
            // Clear selected tests from Visual Studio 'Continuum_Tests' folder'

            if (lstTestsInVS_Folder.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a test(s) to clear from the VS Continuum_Tests folder");
                return;
            }

            for (int t = 0; t < lstTestsInVS_Folder.SelectedItems.Count; t++)
                File.Delete(vsTestFolder + "//" + lstTestsInVS_Folder.SelectedItems[t].Text);

            UpdateListOfTests();
        }

        private void btnCopyTestsToVSFolder_Click(object sender, EventArgs e)
        {
            // Copy selected tests to VS Folder

            if (lstTestsInC3GUI_TestsFolder.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select one or more tests to copy to Visual Studio Continuum_Tests project");
                return;
            }

            for (int t = 0; t < lstTestsInC3GUI_TestsFolder.SelectedItems.Count; t++)
            {
                string selFileName = lstTestsInC3GUI_TestsFolder.SelectedItems[t].Text;
                string testFileName = testFolder + "\\Tests\\" + selFileName;
                string vsFileName = vsTestFolder + "\\" + selFileName;

                if (File.Exists(vsFileName))
                    File.Delete(vsFileName);                                

                File.Copy(testFileName, vsFileName);
            }

            UpdateListOfTests();
        }

        private void cboCFM_ForMCP_Test_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateMapTest_Click(object sender, EventArgs e)
        {
            // Create Map Tests: Load topography and land cover, load met data, load turbine sites, analyze mets, generate map (largest and 
            // around turbines if turb sites loaded)

            string fileName = testFolder + "\\" + txtMapsTestFilename.Text;
            string projectName = cboProject_Maps.Text;

            bool doFiltering = chkDoMetFiltMaps.Checked;

            try
            {
                StreamWriter sw = new StreamWriter(fileName);

                // First, write start of test
                StreamReader sr = new StreamReader(snippetFolder + startSnip);
                while (sr.EndOfStream == false)
                {
                    string lineToWrite = sr.ReadLine();
                    string trimmedLine = lineToWrite.Trim();

                    if (lineToWrite == "public void ")
                        lineToWrite = lineToWrite + txtMapsTestFilename.Text + "()";

                    if (trimmedLine.Length > 14)
                        if (trimmedLine.Substring(0, 15) == "string fileName")
                            lineToWrite = lineToWrite + txtMapsTestFilename.Text + "\";";

                    if (trimmedLine.Length > 42)
                        if (trimmedLine.Substring(0, 43) == "thisInst.UTM_conversions.UTMZoneNumber = ")
                            lineToWrite = lineToWrite + cboUTMZoneMaps.Text + "\";";

                    if (trimmedLine.Length > 34)
                        if (trimmedLine.Substring(0, 35) == "thisInst.UTM_conversions.hemisphere = ")
                            lineToWrite = lineToWrite + cboHemisphereMaps.Text + "\";";

                    sw.WriteLine(lineToWrite);
                }

                sw.WriteLine();

                string thisFilename = "";

                // Import Topography 
                if (projectName == "Findlay")
                    thisFilename = "Findlay Topo.tif";
                else if (projectName == "NW Ohio")
                    thisFilename = "NW Ohio Topo.tif";
                else if (projectName == "Firewheel")
                    thisFilename = "w001001.adf";

                WriteTopographySnippet(sw, thisFilename);

                // Import Land Cover
                if (projectName == "Findlay")
                    thisFilename = "Findlay Land Cover.tif";
                else if (projectName == "NW Ohio")
                    thisFilename = "NW Ohio Land Cover.tif";
                else if (projectName == "Firewheel")
                    thisFilename = "Firewheel Land Cover.tif";

                WriteLandCoverSnippet(sw, thisFilename);

                // Import met data (either time series or TAB files)
                if (cboTAB_or_TS_Maps.SelectedItem.ToString() == "TAB files")
                {
                    for (int m = 0; m < chkListMetsMaps.CheckedItems.Count; m++)
                        WriteMetTABSnippet(sw, chkListMetsMaps.CheckedItems[m].ToString());
                }
                else
                {
                    for (int m = 0; m < chkListMetsMaps.CheckedItems.Count; m++)
                        WriteMetTSSnippet(sw, chkListMetsMaps.CheckedItems[m].ToString(), doFiltering);
                }

                // Import Turbines sites (if selected)
                if (cboTurbSitesMaps.SelectedItem != null)
                    WriteTurbineSnippet(sw, cboTurbSitesMaps.SelectedItem.ToString(), projectName);

                // Analzye mets            
                sr = new StreamReader(snippetFolder + analyzeMetsSnip);
                while (sr.EndOfStream == false)
                {
                    string lineToWrite = sr.ReadLine();
                    sw.WriteLine(lineToWrite);
                }
                sr.Close();

                if (cboWakeModelMaps.SelectedItem.ToString() != "Unwaked")
                {
                    WritePowerCurveSnippet(sw, cboPowerCrvsMaps.SelectedItem.ToString());
                    WriteDoMonteCarlo(sw);

                    sr = new StreamReader(snippetFolder + wakeMapSnip);
                    while (sr.EndOfStream == false)
                    {
                        string lineToWrite = sr.ReadLine();
                        sw.WriteLine(lineToWrite);
                    }
                    sr.Close();
                }
                else
                {
                    sr = new StreamReader(snippetFolder + mapLargest);
                    while (sr.EndOfStream == false)
                    {
                        string lineToWrite = sr.ReadLine();
                        sw.WriteLine(lineToWrite);
                    }
                    sr.Close();

                    sr = new StreamReader(snippetFolder + mapTurbine);
                    while (sr.EndOfStream == false)
                    {
                        string lineToWrite = sr.ReadLine();
                        sw.WriteLine(lineToWrite);
                    }
                    sr.Close();
                }


                sr = new StreamReader(snippetFolder + endSnip);
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

        private void cboTAB_or_TS_Maps_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateGUI_SetupFiles_Click(object sender, EventArgs e)
        {
            // Using selected project input data, creates default model setup (import) and delete GUI tests with the following tests created and saved in 5 folders:
            // Each folder represents a type of model that may be created in Continuum:
            // 1) TAB file Tests: Met data imported as a WS/WD distribution in TAB file format (no filtering or MCP'ing may be done)
            // 2) Raw TS file Tests: Met data imported as a time series.  Data is not filtered and is not MCP'd in these tests.
            // 3) Raw LT TS file Tests: Met data imported as a time series.  Data is not filtered but is MCP'd in these tests.
            // 4) Filtered TS file Tests: Met data imported as a time series.  Data is filtered but is not MCP'd in these tests.
            // 5) Filtered LT TS file Tests: Met data imported as a time series.  Data is filtered and is MCP'd in these tests.

            // Each folder contains 6 test class files with:
            // 3 test class files testing the import of model inputs using 1, 2, or 3 met sites
            // 3 tests class testing the delete of model inputs from a model with 1, 2, or 3 met sites

            // For the model setup tests, Continuum files (.cfn and corresponding SQL files) are created and saved in 'SaveFolder'.  These are used by the delete tests.            
            // Saves test files in 'Continuum 3 GUI Testing folder' and automatically copies over to Visual studio project

            CreateTAB_Tests();
            CreateRawTS_Tests();
            CreateRawTS_LT_Tests();
            CreateFiltTS_Tests();
            CreateFiltLT_TS_Tests();          
                        
        }

        public void CreateRawTS_Tests()
        {
            // TESTS: 2 - Raw TS file Tests            
            // Using one met TS file
            string testFolderName = "2 - Raw TS file Tests";
            string testFileName = "OneMetRawTS_GrossNet";
            string deleteTestName = "DelOneMetTS";
            string projectName = "Findlay"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            string utmZone = "17"; // UTM zone
            string northOrSouth = "Northern";
            string[] metFiles = GetMetsAtProject(projectName, "TS");
            string[] metFilesForTest = new string[1];
            metFilesForTest[0] = metFiles[0];

            string[] allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            string[] metsToDel = new string[1];
            metsToDel[0] = allMetsToDel[0];

            string[] turbineFiles = GetTurbineFilesAtProject(projectName);
            string turbineFile = turbineFiles[0];

            string powerCurveFile = "Goldwind 87-1500 PC_1.205"; // Options: "GE_1_85_CRV", "Goldwind 87-1500 PC_1.205", "Vestas_V150@1.041kgm ^ -3", "Goldwind 93-1500 PC_1.205", 

            string[] zoneFiles = GetZonesAtProject(projectName);
            string zoneFile = zoneFiles[0];

            bool inputReference = false;
            string merraFolder = "";
            bool doFiltering = false;
            bool doGross = true;
            bool doNet = true;
            bool doMCP = false;

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);

            int firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

            // Using two met TS file            
            testFileName = "TwoMetRawTS_GrossNet";
            deleteTestName = "DelTwoMetTS";
            projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            utmZone = "17"; // UTM zone
            northOrSouth = "Northern";
            metFiles = GetMetsAtProject(projectName, "TS");
            metFilesForTest = new string[2];
            metFilesForTest[0] = metFiles[2];
            metFilesForTest[1] = metFiles[3];

            allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            metsToDel = new string[2];
            metsToDel[0] = allMetsToDel[2];
            metsToDel[1] = allMetsToDel[3];

            turbineFiles = GetTurbineFilesAtProject(projectName);
            turbineFile = turbineFiles[1];

            powerCurveFile = "Vestas_V150@1.041kgm^-3"; // Options: "GE_1_85_CRV", "Goldwind 87 - 1500 PC_1.205", "Vestas_V150@1.041kgm^-3", "Goldwind 93 - 1500 PC_1.205", 

            zoneFiles = GetZonesAtProject(projectName);
            zoneFile = zoneFiles[0];

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);
            firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

            // Using three met TS file            
            testFileName = "ThreeMetRawTS_GrossNet";
            deleteTestName = "DelThreeMetTS";
            projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            utmZone = "17"; // UTM zone
            northOrSouth = "Northern";
            metFiles = GetMetsAtProject(projectName, "TS");
            metFilesForTest = new string[3];
            metFilesForTest[0] = metFiles[3];
            metFilesForTest[1] = metFiles[4];
            metFilesForTest[2] = metFiles[5];

            allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            metsToDel = new string[3];
            metsToDel[0] = allMetsToDel[3];
            metsToDel[1] = allMetsToDel[4];
            metsToDel[2] = allMetsToDel[5];

            turbineFiles = GetTurbineFilesAtProject(projectName);
            turbineFile = turbineFiles[3];

            powerCurveFile = "Goldwind 87-1500 PC_1.205"; // Options: "GE_1_85_CRV", "Goldwind 87 - 1500 PC_1.205", "Vestas_V150@1.041kgm^-3", "Goldwind 93 - 1500 PC_1.205", 

            zoneFiles = GetZonesAtProject(projectName);
            zoneFile = zoneFiles[0];

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);
            firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

        }

        public void CreateRawTS_LT_Tests()
        {
            // TESTS: 3 - Raw LT TS file Tests            
            // Using one met TS file and MCPing with MERRA2 data
            string testFolderName = "3 - Raw LT TS file Tests";
            string testFileName = "OneMetRawTS_LTGrossNet";
            string deleteTestName = "DelOneMetTS_LT";
            string projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            string utmZone = "17"; // UTM zone
            string northOrSouth = "Northern";
            string[] metFiles = GetMetsAtProject(projectName, "TS");
            string[] metFilesForTest = new string[1];
            metFilesForTest[0] = metFiles[2];

            string[] allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            string[] metsToDel = new string[1];
            metsToDel[0] = allMetsToDel[2];

            string[] turbineFiles = GetTurbineFilesAtProject(projectName);
            string turbineFile = turbineFiles[1];

            string powerCurveFile = "GE_1_85_CRV"; // Options: "GE_1_85_CRV", "Goldwind 87-1500 PC_1.205", "Vestas_V150@1.041kgm ^ -3", "Goldwind 93-1500 PC_1.205", 

            string[] zoneFiles = GetZonesAtProject(projectName);
            string zoneFile = zoneFiles[0];

            bool inputReference = true;
            string merraFolder = ohioMERRA;
            bool doMCP = true;
            bool doFiltering = false;
            bool doGross = true;
            bool doNet = true;

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);

            int firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

            // Using two met TS file and MCPing with MERRA2 data           
            testFileName = "TwoMetRawTS_LTGrossNet";
            deleteTestName = "DelTwoMetTS_LT";
            projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            utmZone = "17"; // UTM zone
            northOrSouth = "Northern";
            metFiles = GetMetsAtProject(projectName, "TS");
            metFilesForTest = new string[2];
            metFilesForTest[0] = metFiles[2];
            metFilesForTest[1] = metFiles[3];

            allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            metsToDel = new string[2];
            metsToDel[0] = allMetsToDel[2];
            metsToDel[1] = allMetsToDel[3];

            turbineFiles = GetTurbineFilesAtProject(projectName);
            turbineFile = turbineFiles[1];

            powerCurveFile = "Vestas_V150@1.041kgm^-3"; // Options: "GE_1_85_CRV", "Goldwind 87 - 1500 PC_1.205", "Vestas_V150@1.041kgm^-3", "Goldwind 93 - 1500 PC_1.205", 

            zoneFiles = GetZonesAtProject(projectName);
            zoneFile = zoneFiles[0];

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);
            firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

            // Using three met TS file            
            testFileName = "ThreeMetRawTS_LTGrossNet";
            deleteTestName = "DelThreeMetTS_LT";
            projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            utmZone = "17"; // UTM zone
            northOrSouth = "Northern";
            metFiles = GetMetsAtProject(projectName, "TS");
            metFilesForTest = new string[3];
            metFilesForTest[0] = metFiles[3];
            metFilesForTest[1] = metFiles[4];
            metFilesForTest[2] = metFiles[5];

            allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            metsToDel = new string[3];
            metsToDel[0] = allMetsToDel[3];
            metsToDel[1] = allMetsToDel[4];
            metsToDel[2] = allMetsToDel[5];

            turbineFiles = GetTurbineFilesAtProject(projectName);
            turbineFile = turbineFiles[3];

            powerCurveFile = "Goldwind 87-1500 PC_1.205"; // Options: "GE_1_85_CRV", "Goldwind 87 - 1500 PC_1.205", "Vestas_V150@1.041kgm^-3", "Goldwind 93 - 1500 PC_1.205", 

            zoneFiles = GetZonesAtProject(projectName);
            zoneFile = zoneFiles[0];

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);
            firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);


        }

        public void CreateFiltTS_Tests()
        {
            // TESTS: 4 - Filtered TS file Tests            
            // Using one met TS file QC filtered
            string testFolderName = "4 - Filtered TS file Tests";
            string testFileName = "OneMetFiltTS_GrossNet";
            string deleteTestName = "DelOneMetFiltTS";
            string projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            string utmZone = "17"; // UTM zone
            string northOrSouth = "Northern";
            string[] metFiles = GetMetsAtProject(projectName, "TS");
            string[] metFilesForTest = new string[1];
            metFilesForTest[0] = metFiles[2];

            string[] allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            string[] metsToDel = new string[1];
            metsToDel[0] = allMetsToDel[2];

            string[] turbineFiles = GetTurbineFilesAtProject(projectName);
            string turbineFile = turbineFiles[1];

            string powerCurveFile = "GE_1_85_CRV"; // Options: "GE_1_85_CRV", "Goldwind 87-1500 PC_1.205", "Vestas_V150@1.041kgm ^ -3", "Goldwind 93-1500 PC_1.205", 

            string[] zoneFiles = GetZonesAtProject(projectName);
            string zoneFile = zoneFiles[0];

            bool doFiltering = true;
            bool inputReference = false;
            string merraFolder = "";
            bool doMCP = false;
            bool doGross = true;
            bool doNet = true;

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);

            int firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

            // Using two met TS file QC filtered          
            testFileName = "TwoMetFiltTS_GrossNet";
            deleteTestName = "DelTwoMetFiltTS";
            projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            utmZone = "17"; // UTM zone
            northOrSouth = "Northern";
            metFiles = GetMetsAtProject(projectName, "TS");
            metFilesForTest = new string[2];
            metFilesForTest[0] = metFiles[2];
            metFilesForTest[1] = metFiles[3];

            allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            metsToDel = new string[2];
            metsToDel[0] = allMetsToDel[2];
            metsToDel[1] = allMetsToDel[3];

            turbineFiles = GetTurbineFilesAtProject(projectName);
            turbineFile = turbineFiles[1];

            powerCurveFile = "Vestas_V150@1.041kgm^-3"; // Options: "GE_1_85_CRV", "Goldwind 87 - 1500 PC_1.205", "Vestas_V150@1.041kgm^-3", "Goldwind 93 - 1500 PC_1.205", 

            zoneFiles = GetZonesAtProject(projectName);
            zoneFile = zoneFiles[0];

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);
            firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

            // Using three met TS file QC filtered           
            testFileName = "ThreeMetFiltTS_GrossNet";
            deleteTestName = "DelThreeMetFiltTS";
            projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            utmZone = "17"; // UTM zone
            northOrSouth = "Northern";
            metFiles = GetMetsAtProject(projectName, "TS");
            metFilesForTest = new string[3];
            metFilesForTest[0] = metFiles[2];
            metFilesForTest[1] = metFiles[3];
            metFilesForTest[2] = metFiles[4];

            allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            metsToDel = new string[3];
            metsToDel[0] = allMetsToDel[3];
            metsToDel[1] = allMetsToDel[4];
            metsToDel[2] = allMetsToDel[5];

            turbineFiles = GetTurbineFilesAtProject(projectName);
            turbineFile = turbineFiles[3];

            powerCurveFile = "Goldwind 87-1500 PC_1.205"; // Options: "GE_1_85_CRV", "Goldwind 87 - 1500 PC_1.205", "Vestas_V150@1.041kgm^-3", "Goldwind 93 - 1500 PC_1.205", 

            zoneFiles = GetZonesAtProject(projectName);
            zoneFile = zoneFiles[0];

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);
            firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

        }

        public void CreateFiltLT_TS_Tests()
        {
            // TESTS: 5 - Filtered LT TS file Tests            
            // Using one met TS file QC filtered and MCPd with MERRA2
            string testFolderName = "5 - Filtered LT TS file Tests";
            string testFileName = "OneMetFiltTS_LTGrossNet";
            string deleteTestName = "DelOneMetFiltTS_LT";
            string projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            string utmZone = "17"; // UTM zone
            string northOrSouth = "Northern";
            string[] metFiles = GetMetsAtProject(projectName, "TS");
            string[] metFilesForTest = new string[1];
            metFilesForTest[0] = metFiles[2];

            string[] allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            string[] metsToDel = new string[1];
            metsToDel[0] = allMetsToDel[2];

            string[] turbineFiles = GetTurbineFilesAtProject(projectName);
            string turbineFile = turbineFiles[1];

            string powerCurveFile = "GE_1_85_CRV"; // Options: "GE_1_85_CRV", "Goldwind 87-1500 PC_1.205", "Vestas_V150@1.041kgm ^ -3", "Goldwind 93-1500 PC_1.205", 

            string[] zoneFiles = GetZonesAtProject(projectName);
            string zoneFile = zoneFiles[0];

            bool doFiltering = true;
            bool inputReference = true;
            string merraFolder = ohioMERRA;
            bool doMCP = true;

            bool doGross = true;
            bool doNet = true;

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);

            int firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

            // Using two met TS file QC filtered and MCPd with MERRA2         
            testFileName = "TwoMetFiltTS_LTGrossNet";
            deleteTestName = "DelTwoMetFiltTS_LT";
            projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            utmZone = "17"; // UTM zone
            northOrSouth = "Northern";
            metFiles = GetMetsAtProject(projectName, "TS");
            metFilesForTest = new string[2];
            metFilesForTest[0] = metFiles[2];
            metFilesForTest[1] = metFiles[3];

            allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            metsToDel = new string[2];
            metsToDel[0] = allMetsToDel[2];
            metsToDel[1] = allMetsToDel[3];

            turbineFiles = GetTurbineFilesAtProject(projectName);
            turbineFile = turbineFiles[1];

            powerCurveFile = "Vestas_V150@1.041kgm^-3"; // Options: "GE_1_85_CRV", "Goldwind 87 - 1500 PC_1.205", "Vestas_V150@1.041kgm^-3", "Goldwind 93 - 1500 PC_1.205", 

            zoneFiles = GetZonesAtProject(projectName);
            zoneFile = zoneFiles[0];

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);
            firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

            // Using three met TS file QC filtered and MCPd with MERRA2           
            testFileName = "ThreeMetFiltTS_LTGrossNet";
            deleteTestName = "DelThreeMetFiltTS_LT";
            projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            utmZone = "17"; // UTM zone
            northOrSouth = "Northern";
            metFiles = GetMetsAtProject(projectName, "TS");
            metFilesForTest = new string[3];
            metFilesForTest[0] = metFiles[3];
            metFilesForTest[1] = metFiles[4];
            metFilesForTest[2] = metFiles[5];

            allMetsToDel = GetMetsNamesAtProject(projectName, "TS");
            metsToDel = new string[3];
            metsToDel[0] = allMetsToDel[3];
            metsToDel[1] = allMetsToDel[4];
            metsToDel[2] = allMetsToDel[5];

            turbineFiles = GetTurbineFilesAtProject(projectName);
            turbineFile = turbineFiles[3];

            powerCurveFile = "Goldwind 87-1500 PC_1.205"; // Options: "GE_1_85_CRV", "Goldwind 87 - 1500 PC_1.205", "Vestas_V150@1.041kgm^-3", "Goldwind 93 - 1500 PC_1.205", 

            zoneFiles = GetZonesAtProject(projectName);
            zoneFile = zoneFiles[0];

            CreateGUITests("TS", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);
            firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metsToDel, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TS", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);
        }

        public void CreateTAB_Tests()
        {
            // TESTS: 1 - TAB file Tests            
            // Using one met TAB file
            string testFolderName = "1 - TAB file Tests";
            string testFileName = "OneMetTABAndGrossNet";
            string deleteTestName = "DelOneMetTAB";
            string projectName = "Findlay"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            string utmZone = "17"; // UTM zone
            string northOrSouth = "Northern";
            string[] metFiles = GetMetsAtProject(projectName, "TAB");
            string[] metFilesForTest = new string[1];
            metFilesForTest[0] = metFiles[0];

            string[] turbineFiles = GetTurbineFilesAtProject(projectName);
            string turbineFile = turbineFiles[0];

            string powerCurveFile = "GE_1_85_CRV"; // Options: "GE_1_85_CRV", "Goldwind 87-1500 PC_1.205", "Vestas_V150@1.041kgm^-3", "Goldwind 93-1500 PC_1.205", 

            string[] zoneFiles = GetZonesAtProject(projectName);
            string zoneFile = zoneFiles[0];

            string merraFolder = ""; // Options: ohioMERRA, firewheelMERRA;
            bool inputReference = false;

            bool doFiltering = false;
            bool doGross = true;
            bool doNet = true;
            bool doMCP = false;

            CreateGUITests("TAB", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);
            int firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metFilesForTest, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TAB", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

            // Using two met TAB file            
            testFileName = "TwoMetTABAndGrossNet";
            deleteTestName = "DelTwoMetTABs";
            projectName = "NW Ohio"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            utmZone = "17"; // UTM zone
            northOrSouth = "Northern";
            metFiles = GetMetsAtProject(projectName, "TAB");
            metFilesForTest = new string[2];
            metFilesForTest[0] = metFiles[0];
            metFilesForTest[1] = metFiles[1];

            turbineFiles = GetTurbineFilesAtProject(projectName);
            turbineFile = turbineFiles[0];

            powerCurveFile = "Vestas_V150@1.041kgm^-3"; // Options: "GE_1_85_CRV", "Goldwind 87 - 1500 PC_1.205", "Vestas_V150@1.041kgm^-3", "Goldwind 93 - 1500 PC_1.205", 

            zoneFiles = GetZonesAtProject(projectName);
            zoneFile = zoneFiles[0];

            CreateGUITests("TAB", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);
            firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metFilesForTest, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TAB", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);

            // Using three met TAB file            
            testFileName = "ThreeMetTABAndGrossNet";
            deleteTestName = "DelThreeMetTABs";
            projectName = "Firewheel"; // Options: "Findlay", "NW Ohio", "Firewheel", "Bobcat Bluff"
            utmZone = "14"; // UTM zone
            northOrSouth = "Northern";
            metFiles = GetMetsAtProject(projectName, "TAB");
            metFilesForTest = new string[3];
            metFilesForTest[0] = metFiles[0];
            metFilesForTest[1] = metFiles[1];
            metFilesForTest[2] = metFiles[2];

            turbineFiles = GetTurbineFilesAtProject(projectName);
            turbineFile = turbineFiles[0];

            powerCurveFile = "Goldwind 87-1500 PC_1.205"; // Options: "GE_1_85_CRV", "Goldwind 87 - 1500 PC_1.205", "Vestas_V150@1.041kgm^-3", "Goldwind 93 - 1500 PC_1.205", 

            zoneFiles = GetZonesAtProject(projectName);
            zoneFile = zoneFiles[0];

            CreateGUITests("TAB", inputReference, merraFolder, testFileName, projectName, utmZone, northOrSouth, metFilesForTest, 80, turbineFile, powerCurveFile,
                zoneFile, doFiltering, doGross, doNet, doMCP, testFolderName);
            firstTestNum = CombineTestsToOneFile(testFileName, testFolderName, 2);

            CreateDeleteTests(metFilesForTest, testFolderName, testFileName, firstTestNum, deleteTestName, projectName, "TAB", turbineFile, powerCurveFile, zoneFile);
            CombineTestsToOneFile(deleteTestName, testFolderName, 2, true);
        }

        public void CreateGUITests(string TABorTS, bool inputMERRA, string merraFolder, string txtFileName, string projectName, string utmZone, string northOrSouth, string[] metFiles,
            int modeledHeight, string turbineFile, string powerCurveFile, string zoneFile, bool doFiltering, bool doGross, bool doNet, bool doMCP, string testFolderName)
        {

            // Create test files for every combination of inputs

            // For TAB files, there are six possible inputs:
            // Topo, Land Cover, Met TABs, Turbines, Power Curves, Zones

            // For TS files, there are seven inputs with one additional input: MERRA2
            string folderLoc = testFolder + "\\Tests\\GUI Tests\\" + testFolderName;
            int numInputs = 6;

            if ((TABorTS == "Time Series" && inputMERRA == true) || (TABorTS == "TS" && inputMERRA == true))
                numInputs = 7;

            int[,] inputOrder = GetAllPermutations(numInputs);
            int numCombos = inputOrder.GetUpperBound(1) + 1;                       
            
            string topoFile = GetTopoFileName(projectName);
            string landCoverFile = GetLandCoverFileName(projectName);                                  

            if (metFiles == null)
            {
                MessageBox.Show("Select at least one met site to use in tests.");
                return;
            }            

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
                    string fileName = folderLoc + "\\" + txtFileName + "_" + (fileCount + 1).ToString();
                    StreamWriter sw = new StreamWriter(fileName);

                    Directory.GetFiles(saveToFolder);

                    try
                    {
                        // First, write start of test
                        StreamReader sr = new StreamReader(snippetFolder + startSnip);
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
                                    lineToWrite = lineToWrite + utmZone + ";";

                            if (trimmedLine.Length > 36)
                                if (trimmedLine.Substring(0, 37) == "thisInst.UTM_conversions.hemisphere =")
                                    lineToWrite = lineToWrite + "\"" + northOrSouth + "\";";

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
                                    if (TABorTS == "TAB")
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
                                    WriteMERRA2Snippet(sw, m, merraFolder);
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

                        sr = new StreamReader(snippetFolder + endSnip);
                        while (sr.EndOfStream == false)
                        {
                            string lineToWrite = sr.ReadLine();
                            sw.WriteLine(lineToWrite);
                        }

                        sw.Close();
                        fileCount++;
                    }
                    catch (Exception ex)
                    {
                        sw.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnCreateGUI_SetupFiles1_Click(object sender, EventArgs e)
        {
            CreateTAB_Tests();
        }

        private void btnCreateGUI_SetupFiles2_Click(object sender, EventArgs e)
        {
            CreateRawTS_Tests();
        }

        private void btnCreateGUI_SetupFiles3_Click(object sender, EventArgs e)
        {
            CreateRawTS_LT_Tests();
        }

        private void btnCreateGUI_SetupFiles4_Click(object sender, EventArgs e)
        {
            CreateFiltTS_Tests();
        }

        private void btnCreateGUI_SetupFiles5_Click(object sender, EventArgs e)
        {
            CreateFiltLT_TS_Tests();
        }
    }
}
