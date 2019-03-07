using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace ContinuumNS
{
    public partial class Continuum : Form
    {
        public TopoInfo topo = new TopoInfo(); // Topography (digital elevation) and land cover data
        public MetCollection metList = new MetCollection(); // List of Met objects 
        public InvestCollection radiiList = new InvestCollection(); // List of radii and inverse distance exponents used to calculate exposure and SR/DH in each model
        public MapCollection mapList = new MapCollection(); // List of generated maps
        public Saved_Parameters savedParams = new Saved_Parameters();  // Saved map display settings and topo data filename (C3: will add saved filename, path name, land cover filename)
        public MetPairCollection metPairList = new MetPairCollection();  // List of MetPair objects
        public ModelCollection modelList = new ModelCollection();  // List of Continuum models (i.e. coefficients, RMS errors, etc)
        public TurbineCollection turbineList = new TurbineCollection(); // List of Turbine objects
        public WakeCollection wakeModelList = new WakeCollection(); // List of wake models
        public Loss_factors otherLosses = new Loss_factors(); // All losses other than wake loss used to find net AEP
        public UTM_conversion UTM_conversions = new UTM_conversion(); // UTM datum and zone info
        public BackgroundWork BW_worker = new BackgroundWork();
        public Update updateThe = new Update();

        public string validationKey = "AMAAMADbK7NxKwWq1M+GI+5LL12Tsr63QOVxaK1U9949tV7Wk5aEVzqbuRm5JlZ04QySWWMDAAEAAQ=="; // CryptoLicensing validationKey used to validate the User//s license key
        public bool functionalitiesEnabled = false; // if false then Continuum closes
        public string usersKey = ""; // User's license key
        public bool fileChanged;
        public bool okToUpdate = true;

        public Continuum()
        {
            SplashScreen Splash = new SplashScreen();
            Splash.ShowDialog();          
            InitializeComponent();
            radiiList.New(); // populates with R = 4000, 6000, 8000, 10000 and invserse distance exponent = 1
            otherLosses.Set_Defaults();
        }

        public void ClearWindRose()
        {
            // Clears all dropdown menus with WD sectors        
            cboGrossWD.Items.Clear();
            cboMapWD.Items.Clear();
            cboSummaryWD.Items.Clear();
            cboNetWD.Items.Clear();
            cboAdvancedWD.Items.Clear();
        }

        private void btnLoadXYZ_Click(object sender, EventArgs e)
        { 
            // if ( file is not saved, prompts user to save, then calls LoadTopo
            // Can't load new topo data if there are calcs going on
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import topo data while calculations are under way.", "Continuum 2.3");
                return;
            }

            bool wasSaved = false;
            if (savedParams.savedFileName == null)
            {
                MessageBox.Show("Please specify the file location.", "Continuum 2.3");
                wasSaved = SaveAs();
                if (wasSaved == false)
                {
                    MessageBox.Show("A file location is needed in order to create local database. Cancelling topography import.", "Continuum 2.3");
                    return;
                }
            }
            
            Topo_Buffer_Warning warningMsg = new Topo_Buffer_Warning();
            warningMsg.ShowDialog();

            int goodToGo = topo.OkToReload();
            if (goodToGo == 6) {                
                updateThe.NewModel(this);
                LoadTopo();
            }
            else if (goodToGo == 1) 
                LoadTopo();

        }

        public void SetDefaultFolderLocations(string wholePath)
        {
            // Sets default folder location for all OpenFileDialog and SaveFileDialog
                        
            int Ind = wholePath.LastIndexOf("\\");
            savedParams.pathName = wholePath.Substring(1, Ind);
            ofdCFMfile.InitialDirectory = savedParams.pathName;
            ofdMets.InitialDirectory = savedParams.pathName;
            ofdTurbines.InitialDirectory = savedParams.pathName;
            ofdXYZfile.InitialDirectory = savedParams.pathName;
            ofdPowerCurve.InitialDirectory = savedParams.pathName;
            ofdImportCoeffs.InitialDirectory = savedParams.pathName;
            ofdImportMap.InitialDirectory = savedParams.pathName;
            ofdLandCover.InitialDirectory = savedParams.pathName;
            ofdLC_Key.InitialDirectory = savedParams.pathName;

            sfdCFMfile.InitialDirectory = savedParams.pathName;
            sfd60mWS.InitialDirectory = savedParams.pathName;
            sfdExpos.InitialDirectory = savedParams.pathName;
            sfdWRG.InitialDirectory = savedParams.pathName;
            sfdrsf.InitialDirectory = savedParams.pathName;
        }

        public void LoadTopo()
        {
            // Prompts user to find open topo data file and then calls LoadXYZ_BW function (calls background worker)

            if (ofdXYZfile.ShowDialog() == DialogResult.OK)
            {

                string wholePath = ofdXYZfile.FileName;
                SetDefaultFolderLocations(wholePath);

                txtTopoSource.Text = ofdXYZfile.FileName;
                savedParams.topoText = txtTopoSource.Text;

                if (UTM_conversions.savedDatumIndex == 100)
                {
                    int datumInd = 0;
                    int zoneNumber = 0;
                    string northOrSouth = "";

                    Topo_Load_UTM_Datum_Zone topoLoad = new Topo_Load_UTM_Datum_Zone();
                    topoLoad.cboNorthOrSouth.SelectedIndex = 0;
                    topoLoad.ShowDialog();
                    try
                    {
                        datumInd = topoLoad.cbo_Datums.SelectedIndex;
                    }
                    catch
                    {
                        return;
                    }

                    try
                    {
                        zoneNumber = Convert.ToInt16(topoLoad.cboUTMZone.SelectedItem.ToString());
                    }
                    catch
                    {
                        return;
                    }

                    try
                    {
                        northOrSouth = topoLoad.cboNorthOrSouth.SelectedItem.ToString();
                    }
                    catch
                    {
                        return;
                    }

                    UTM_conversions.savedDatumIndex = datumInd;
                    UTM_conversions.UTMZoneNumber = zoneNumber;
                    UTM_conversions.hemisphere = northOrSouth;
                    txtUTMDatum.Text = UTM_conversions.GetDatumString(datumInd);
                    txtUTMZone.Text = UTM_conversions.UTMZoneNumber.ToString() + UTM_conversions.hemisphere.Substring(0, 1);

                }


                // Creates struct needed for topo import and then calls background worker to read topo data

                BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
                Vars_for_BW.thisInst = this;
                Vars_for_BW.Filename = wholePath;
                BW_worker = new BackgroundWork();
                BW_worker.Call_BW_TopoImport(Vars_for_BW);

                ChangesMade();

            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Checks for changes, asks user to save, then closes program
            DialogResult saveChanges = DialogResult.Yes;
            if (fileChanged == true) {
                saveChanges = MessageBox.Show("Do you want to save changes?", "Closing Continuum", MessageBoxButtons.YesNo);
                if (saveChanges == DialogResult.Yes) {
                    if (savedParams.savedFileName != "")
                        SaveFile();
                    else
                        SaveAs();

                }
            }
            else
                Close();

            if (saveChanges == DialogResult.Yes && fileChanged == false)
                Close();
            else if (saveChanges == DialogResult.No)
                Close();
            
        }
        
        private void btnDelMet_Click(object sender, EventArgs e)
        { 
            // Confirms that user wants to delete a met, calls DeleteMet, updates GUI with new met list
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot delete met data while calculations are under way.", "Continuum 2.3");
                return;
            }

            int numToDelete = lstMetTowers.SelectedItems.Count;
            string[] metNames = new string[numToDelete];  

            if (numToDelete == 0) {
                MessageBox.Show("Select one or more met sites to delete by clicking met names.", "Continuum 2.3");
                return;
            }

            DialogResult confirm = DialogResult.Yes;
            if (numToDelete == 1)
                confirm = MessageBox.Show("Are you sure that you want to delete this met tower?", "Continuum 2.3", MessageBoxButtons.YesNo);
            else
                confirm = MessageBox.Show("Are you sure that you want to delete these " + numToDelete + " met towers?", "Continuum 2.3", MessageBoxButtons.YesNo);
            
            if (confirm == DialogResult.Yes)
            {
                // check if models and turbine parameters have already been calculated

                DeleteMet();

                ChangesMade();                
                updateThe.AllTABs(this);
            }
        }

        public void DeleteMet()
        {
            // Deletes all selected mets, clears all calculations that used mets, calls background worker to perform met calcs (needed?)
            int metCount = metList.ThisCount;
            int numToDelete = lstMetTowers.SelectedItems.Count;

            string[] metNames = new string[numToDelete];

            for (int i = 0; i < numToDelete; i++)
                metNames[i] = lstMetTowers.SelectedItems[i].Text;           
                        
            for (int i = 0; i < numToDelete; i++) { 
                for (int j = 0; j < metCount; j++) { 
                    if (metList.metItem[j].name == metNames[i]) {
                        metList.Remove(metNames[i]);
                        ChangesMade();
                        break;
                    }
                }
            }

            if (metList.ThisCount > 0) {
                modelList.ClearAllExceptDefaultAndImported();
                modelList.UpdateDefaultModel(this);                
                mapList.ClearAllWakedMaps();
            }
            else {
                modelList.ClearAllExceptDefaultAndImported();
                modelList.UpdateDefaultModel(this);
                turbineList.ClearAllCalcs();
                metPairList.ClearAll();                
                mapList.ClearAllMaps();
            }

            wakeModelList.ClearAll();
            lstWakeModels.Items.Clear();

            turbineList.ClearWS_EstCalcs(metList); // clears WS estimates, gross and net AEP but keeps path of nodes between turbines and remaining mets
            turbineList.ClearWS_EstsDeletedMets(metList);

            metPairList.RemoveAllWithDeletedMets(metList);
            metPairList.RemoveAllExceptDefault();
            metPairList.ClearRoundRobin();

            mapList.DeleteMapsUsingDeletedMets(this, metNames);

            if (metList.ThisCount == 0)
                metList.expoIsCalc = false;

            if (topo.gotTopo == true && metList.ThisCount > 0) {

                // Call background worker to run calculations
                BW_worker = new BackgroundWork();
                BW_worker.Call_BW_MetCalcs(this);
            }

            ChangesMade();

        }

        public bool ImportMetsTAB()
        {
            // Clears calculations generated from the site calibration using previous set of met sites (UWDW models, Met Pair lists, turbine wind speed and energy estimates
            // and Round robin calculations. Reads in one or many met TAB files and adds to metList. Returns true if TAB files imported successfully.

            if (ofdMets.ShowDialog() == DialogResult.OK)
            {
                modelList.ClearAllExceptImported();
                metPairList.ClearAll();
                turbineList.ClearWS_EstCalcs(metList);
                turbineList.ClearAllGrossEsts();
                turbineList.ClearAllNetEsts();
                wakeModelList.ClearAll();
                mapList.ClearAllWakedMaps();
                metPairList.ClearRoundRobin();                             
                updateThe.ClearStats(this);

                string wholePath = ofdMets.FileName;
                SetDefaultFolderLocations(wholePath);

                int numFiles = ofdMets.FileNames.Length;

                bool showMsg = true;
                int lastH = 0;

                for (int n = 0; n < numFiles; n++)
                {                    
                    int height = 0;
                    string metName = "";
                    double UTMX;
                    double UTMY;

                    double[] sectorWS_Ratios = null;
                    double[] metWindRose = null;
                    double[,] WS_Dist = new double[0,0];
                    double WS_FirstInt = 0;
                    double WS_IntSize = 0;
                    int WR_size = 0;
                    int WD_offset = 0;

                    bool inputMet = false;
                    
                    StreamReader sr = new StreamReader(ofdMets.FileNames[n]);

                    char[] delims = new char[4];
                    delims[0] = '\t';
                    delims[1] = ',';
                    delims[2] = ' ';
                    delims[3] = ':';
                    //  MyReader.SetDelimiters(delims)

                    // Read in Met name
                    string dataStr = sr.ReadLine();
                    string[] fileRow = dataStr.Split(delims);

                    if (fileRow.Length > 0)
                    {
                        metName = fileRow[0];
                        for (int i = 1; i < fileRow.Length; i++)
                            metName = metName + " " + fileRow[i];

                        int lastChar = 0;
                        char thisChar;

                        for (int i = 0; i < metName.Length; i++)
                        {
                            thisChar = metName[i];
                            if (metName[i] == '\0' && i > 5)
                            {
                                lastChar = i;
                                break;
                            }
                        }

                        if (lastChar != 0) metName = metName.Substring(1, lastChar + 1);
                    }
                    else
                        metName = metList.MakeUpNameIfBlank();

                    metName = metName.Trim();

                    // Read in coordinates and height
                    dataStr = sr.ReadLine();
                    
                    fileRow = dataStr.Split(delims);
                    fileRow = TrimBlanks(fileRow);

                    if (fileRow.Length < 3)
                    {
                        MessageBox.Show("Error reading in met UTMX, UTMY and height for file: " + ofdMets.FileName);
                        sr.Close();
                        return false;
                    }

                    try
                    {
                        UTMX = Convert.ToSingle(fileRow[0]);
                        UTMY = Convert.ToSingle(fileRow[1]);

                        if (UTMX < 1000 && UTMY < 1000)
                        {
                            // Convert to UTM
                            UTM_conversion.UTM_coords theseUTM = new UTM_conversion.UTM_coords();

                            if (UTM_conversions.savedDatumIndex == 100)
                            {
                                UTM_datum thisDatum = new UTM_datum();
                                thisDatum.ShowDialog();
                                UTM_conversions.savedDatumIndex = thisDatum.cbo_Datums.SelectedIndex;
                            }

                            theseUTM = UTM_conversions.LLtoUTM(UTMX, UTMY);
                            UTMX = Convert.ToInt32(theseUTM.UTMEasting);
                            UTMY = Convert.ToInt32(theseUTM.UTMNorthing);
                            UTM_conversions.UTMZoneNumber = theseUTM.UTMZoneNumber;
                            txtUTMDatum.Text = UTM_conversions.GetDatumString(UTM_conversions.savedDatumIndex);
                            txtUTMZone.Text = UTM_conversions.UTMZoneNumber.ToString() + UTM_conversions.hemisphere.Substring(0,1);
                        }
                        else if (UTM_conversions.savedDatumIndex == 100)
                        {
                            int datumInd = 0;
                            int zoneNumber = 0;
                            string hemisphere = "";

                            Topo_Load_UTM_Datum_Zone thisTopo = new Topo_Load_UTM_Datum_Zone();
                            thisTopo.cboNorthOrSouth.SelectedIndex = 0;
                            thisTopo.ShowDialog();

                            try
                            {
                                datumInd = thisTopo.cbo_Datums.SelectedIndex;
                            }
                            catch 
                            {
                                return false;
                            }

                            try
                            {
                                zoneNumber = Convert.ToInt16(thisTopo.cboUTMZone.SelectedItem.ToString());
                            }
                            catch 
                            {
                                return false;
                            }

                            try
                            {
                                hemisphere = thisTopo.cboNorthOrSouth.SelectedItem.ToString();
                            }
                            catch 
                            {
                                return false;
                            }

                            UTM_conversions.savedDatumIndex = datumInd;
                            UTM_conversions.UTMZoneNumber = zoneNumber;
                            UTM_conversions.hemisphere = hemisphere;
                            txtUTMDatum.Text = UTM_conversions.GetDatumString(datumInd);
                            txtUTMZone.Text = UTM_conversions.UTMZoneNumber.ToString() + UTM_conversions.hemisphere.Substring(0,1);

                        }
                    }
                    catch 
                    {                        
                        MessageBox.Show("Error reading UTMX");
                        sr.Close();
                        return false;
                    }

                    try
                    {
                        height = (int)Convert.ToDouble(fileRow[2]);
                    }
                    catch
                    {
                        MessageBox.Show("Error reading Met height");
                        sr.Close();
                        return false;
                    }

                    // check to see if different height than last one
                    if (lastH == 0)
                        lastH = height;
                    else if (lastH != height)
                    {
                        MessageBox.Show("Error reading the TAB files. All TAB files must represent the same height.", "Continuum 2.3");
                        return false;
                    }

                    // Read in rose size and WS int
                    dataStr = sr.ReadLine();
                    fileRow = dataStr.Split(delims);
                    fileRow = TrimBlanks(fileRow);

                    if (fileRow.Length < 3)
                    {
                        MessageBox.Show("Error reading line 3 of file: " + ofdMets.FileName);
                        sr.Close();
                        return false;
                    }
                    else
                    {
                        try
                        {
                            WR_size = Convert.ToInt16(fileRow[0]);
                            if (WR_size != 12 && WR_size != 16 && WR_size != 24)
                            {
                                MessageBox.Show("Error reading TAB file.  Number of sectors can only be 12, 16 or 24.  Read in :" + WR_size + ".  check your TAB file", "Continuum 2.3");
                                return false;
                            }
                        }
                        catch 
                        {
                            MessageBox.Show("Error reading WR size.", "Continuum 2.3");
                            return false;
                        }

                        try
                        {
                            WS_IntSize = Convert.ToSingle(fileRow[1]);
                            //   if ( WS_IntSize != 1 ) {
                            // MessageBox.Show("Error reading TAB file.  WS interval size must be 1.0 m/s.  Read in :" + WS_IntSize + ".  check your TAB file", , "Continuum 2.3")
                            // return;
                        }
                        catch 
                        {
                            MessageBox.Show("Error reading WS interval size.", "Continuum 2.3");
                            return false;
                        }

                        try
                        {
                            WD_offset = (int)Convert.ToSingle(fileRow[2]);
                            if (WD_offset != 0)
                            {
                                MessageBox.Show("Error reading TAB file.  WD offset size must be 0.  Read in :" + WD_offset + ".  check your TAB file", "Continuum 2.3");
                                return false;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Error reading WD offset.", "Continuum 2.3");
                            return false;
                        }
                    }

                    if (WR_size == 0 || WS_IntSize == 0)
                    {
                        MessageBox.Show("Error reading WS bin size.", "Continuum 2.3");
                        return false;
                    }

                    if (WD_offset != 0)
                    {
                        MessageBox.Show("Sorry the WD offset is not supported in this version of Continuum.  You must create a TAB file with no wind direction offset.");
                        sr.Close();
                        return false;
                    }

                    // check the size of the wind rose for other mets

                    for (int i = 0; i < metList.ThisCount; i++)
                    {
                        if (metList.metItem[i].windRose.Length != WR_size)
                        {
                            MessageBox.Show("A " + metList.metItem[i].windRose.Length + "-sector wind rose has been imported for other mets but this met has " + WR_size + " sectors. check your input file.", "Continuum 2.3");
                            sr.Close();
                            return false;
                        }
                    }

                    metWindRose = new double[WR_size];
                    sectorWS_Ratios = new double[WR_size];

                    // Read in wind rose
                    dataStr = sr.ReadLine();
                    fileRow = dataStr.Split(delims);
                    fileRow = TrimBlanks(fileRow);

                    if (fileRow == null)
                    {
                        MessageBox.Show("Error reading the wind rose. check your TAB file.");
                        return false;
                    }
                    else if (fileRow.Length != WR_size)
                    {
                        MessageBox.Show("Error reading the wind rose. check your TAB file.");
                        return false;
                    }                        

                    for (int i = 0; i < WR_size; i++)
                        metWindRose[i] = Convert.ToSingle(fileRow[i]) / 100;

                    // Read in WS distribution                  
                    int WS_Int = 1;

                    while (sr.EndOfStream == false)
                    {
                        dataStr = sr.ReadLine();
                        dataStr.Trim();
                        fileRow = dataStr.Split(delims);
                        fileRow = TrimBlanks(fileRow);
                        
                        if (fileRow == null)
                            return false;

                        if (WS_Int == 1)
                            WS_FirstInt = Convert.ToSingle(fileRow[0]);

                        if (WS_Int == 2)
                            WS_IntSize = Convert.ToSingle(fileRow[0]) - WS_FirstInt;

                        if (fileRow.Length - 1 != WR_size)
                        {
                            MessageBox.Show("A " + WR_size + "-sector wind rose has been imported but read in " + (fileRow.Length - 1).ToString() + ". check your input file.", "Continuum 2.3");
                            sr.Close();
                            return false;
                        }

                        ResizeArray(ref WS_Dist, WR_size, WS_Int);

                        for (int i = 0; i <= fileRow.Length - 2; i++)
                            WS_Dist[i, WS_Int - 1] = Convert.ToSingle(fileRow[i + 1]);

                        WS_Int++;
                    }

                    for (int i = 0; i < metList.ThisCount; i++)
                    {
                        if (metList.metItem[i].WS_IntSize != WS_IntSize)
                        {
                            MessageBox.Show("A different WS interval size of " + metList.metItem[i].WS_IntSize + " was used for other mets.  The WS intervals must be consistent for all met sites.");
                            sr.Close();
                            return false;
                        }
                    }

                    sr.Close();

                    // check TAB file
                    Check_class check = new Check_class();
                    inputMet = check.NewTAB(WS_Dist, WS_FirstInt, WS_IntSize, metWindRose, metName, this); // checks that sector WS dists add to 1000, wind rose adds to 100 and that WS dist intervals line up with other mets or power curves

                    if (inputMet == false)
                        return false;

                    inputMet = check.CheckMetName(metName, turbineList);

                    if (inputMet == false)
                        return false;

                    inputMet = check.NewTurbOrMet(this, metName, UTMX, UTMY, showMsg); // checks the distance between met and topo grid edges to make sure that there//s enough distance for expo calcs

                    if (inputMet == true)
                        metList.AddMetTAB(metName, UTMX, UTMY, height, metWindRose, WS_Dist, WS_FirstInt, WS_IntSize);

                }

                metList.MakeAllSameLength();
                ChangesMade();
                return true;
            }
            else
                return false;            

        }

        public string[] TrimBlanks(string[] Array_with_Blanks)
        {
            string[] Trimmed_data = null;
            int Trim_count = 0;

            if (Array_with_Blanks == null)
                return Trimmed_data;

            for (int i = 0; i < Array_with_Blanks.Length; i++)
            {
                if (Array_with_Blanks[i] != "")
                {
                    Trim_count++;
                    Array.Resize(ref Trimmed_data, Trim_count);
                    Trimmed_data[Trim_count - 1] = Array_with_Blanks[i];
                }
            }
            
            return Trimmed_data;
        }

        public T[,] ResizeArray<T>(ref T[,] original, int rows, int cols)
        {
            T[,] newArray = new T[rows, cols];
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));
            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    newArray[i, j] = original[i, j];

            original = newArray;
            return original;
        }

        private void btnTurbines_Click(object sender, EventArgs e)
        { 
            // Checks that turbines can be imported, calls LoadTurbines then calls background worker to perform turbine calculations
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import turbine sites while calculations are under way.", "Continuum 2.3");
                return;
            }

            if (wakeModelList.NumWakeModels > 0) {
                DialogResult goodToGo = MessageBox.Show("Importing additional turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 2.3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    wakeModelList.ClearAll();
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
                else                
                    return;
            }

            LoadTurbines();

            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

            if (metList.ThisCount > 0 && turbineList.turbineCalcsDone == true) {
                argsForBW.thisInst = this;
                argsForBW.thisWakeModel = null;
                argsForBW.isCalibrated = false; // this only affects net turbine calcs (i.e. waked calcs) so this setting can be either true or false

                // Call background worker to run calculations
                BW_worker = new BackgroundWork();
                BW_worker.Call_BW_TurbCalcs(argsForBW);
            }
            else
            {
                okToUpdate = false;
                updateThe.TurbineList(this);
                okToUpdate = true;
                updateThe.InputTAB(this);
            }
                            
        }

        public void LoadTurbines()
        { 
            // Open .csv or .txt file with turbine coordinates, reads in each turbine site and adds to list of Turbines 
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import turbine sites while calculations are under way.", "Continuum 2.3");
                return;
            }

            if (ofdTurbines.ShowDialog() == DialogResult.OK) {

                string wholePath = ofdTurbines.FileName;
                SetDefaultFolderLocations(wholePath);
                                
                string[] fileRow;
                int numTurbines = 0;
                string turbineName = "";
                double UTMX = 0;
                double UTMY = 0;
                bool inputTurbine = false;
                int thisString = 0;
                StreamReader sr;
                try
                {
                    sr = new StreamReader(wholePath);
                }
                catch
                {
                    MessageBox.Show("Error opening file. Make sure that it's not open in another program.");
                    return;
                }
                
                char[] delims = new char[2];
                delims[0] = '\t';
                delims[1] = ',';
                
                bool showMsg = true;

                while (sr.EndOfStream == false)
                {
                    string dataStr = sr.ReadLine();
                    char[] Trim_Chars = new char[2];
                    Trim_Chars[0] = ',';
                    Trim_Chars[1] = '\t';
                    dataStr = dataStr.Trim(Trim_Chars);
                    fileRow = dataStr.Split(delims);

                    if (fileRow.Length >= 3) { // to allow turbine names to have spaces, read last two columns to find UTMX/Y or Lat/Long

                        try {                            
                            UTMX = Convert.ToDouble(fileRow[fileRow.Length - 2]);
                        }
                        catch  {
                            MessageBox.Show("Error reading the turbine input file.  The format should be Name, Easting, Northing.", "Continuum 2.3");
                            sr.Close();
                            return;
                        }

                        try {                            
                            UTMY = Convert.ToDouble(fileRow[fileRow.Length - 1]);
                        }
                        catch  {
                            MessageBox.Show("Error reading the turbine input file.  The format should be Name, Easting, Northing.", "Continuum 2.3");
                            sr.Close();
                            return;
                        }

                        turbineName = fileRow[0];

                        for (int i = 1; i <= fileRow.Length - 3; i++)
                            turbineName = turbineName + " " + fileRow[i];

                        if (Math.Abs(UTMX) < 1000 && Math.Abs(UTMY) < 1000) {
                            if (UTM_conversions.savedDatumIndex == 100) {
                                UTM_datum thisDatum = new UTM_datum();
                                thisDatum.ShowDialog();
                                UTM_conversions.savedDatumIndex = thisDatum.cbo_Datums.SelectedIndex;
                            }

                            UTM_conversion.UTM_coords theseUTM = UTM_conversions.LLtoUTM(UTMX, UTMY);
                            UTMX = theseUTM.UTMEasting;
                            UTMY = theseUTM.UTMNorthing;
                            txtUTMDatum.Text = UTM_conversions.GetDatumString(UTM_conversions.savedDatumIndex);
                            txtUTMZone.Text = UTM_conversions.UTMZoneNumber.ToString() + UTM_conversions.hemisphere.Substring(0,1);

                            if (UTM_conversions.UTMZoneNumber == -999) UTM_conversions.UTMZoneNumber = theseUTM.UTMZoneNumber;
                        }
                        else {
                            if (UTM_conversions.savedDatumIndex == 100)
                            {
                                int datumInd = 0;
                                int zoneNumber = 0;
                                string hemisphere = "";

                                Topo_Load_UTM_Datum_Zone topoLoad = new Topo_Load_UTM_Datum_Zone();
                                topoLoad.ShowDialog();

                                try
                                {
                                    datumInd = topoLoad.cbo_Datums.SelectedIndex;
                                }
                                catch
                                {
                                    return;
                                }

                                try
                                {
                                    zoneNumber = Convert.ToInt16(topoLoad.cboUTMZone.SelectedItem.ToString());
                                }
                                catch
                                {
                                    return;
                                }

                                try
                                {
                                    hemisphere = topoLoad.cboNorthOrSouth.SelectedItem.ToString();
                                }
                                catch
                                {
                                    return;
                                }

                                UTM_conversions.savedDatumIndex = datumInd;
                                UTM_conversions.UTMZoneNumber = zoneNumber;
                                UTM_conversions.hemisphere = hemisphere;
                                txtUTMDatum.Text = UTM_conversions.GetDatumString(datumInd);
                                txtUTMZone.Text = UTM_conversions.UTMZoneNumber.ToString() + UTM_conversions.hemisphere.Substring(0,1);
                            }
                        }
                        
                        if (turbineName == "") {
                            MessageBox.Show("Couldn't read in a turbine name at line: " + numTurbines, "Continuum 2.3");
                            sr.Close();
                            return;
                        }

                        if (UTMX == 0) {
                            MessageBox.Show("Couldn't read in an easting at line: " + numTurbines, "Continuum 2.3");
                            sr.Close();
                            return;
                        }

                        if (UTMY == 0) {
                            MessageBox.Show("Couldn't read in a northing at line: " + numTurbines, "Continuum 2.3");
                            sr.Close();
                            return;
                        }

                        Check_class check = new Check_class();
                        inputTurbine = check.NewTurbOrMet(this, turbineName, UTMX, UTMY, showMsg);
                        if (inputTurbine == true)
                            inputTurbine = check.CheckTurbName(turbineName, metList);
                        else
                            showMsg = false;

                        if (inputTurbine == true) turbineList.AddTurbine(turbineName, UTMX, UTMY, thisString);
                        numTurbines++;
                    }
                    else {
                        MessageBox.Show("Error reading in the file at line: " + numTurbines + "The format should be Name, Easting, Northing.  " +
                            "Make sure that there are no spaces in your turbine names", "Continuum 2.3");
                        sr.Close();
                        return;
                    }

                    turbineName = "";
                    UTMX = 0;
                    UTMY = 0;
                }
                sr.Close();
            }
                        
            updateThe.ColoredButtons(this);
        }

        private void btnAddTurb_Click(object sender, EventArgs e)
        { 
            // Opens NewTurbine dialog box for user to manually enter a new turbine site
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot add turbine site while calculations are under way.", "Continuum 2.3");
                return;
            }

            if (wakeModelList.NumWakeModels > 0 ) {
                DialogResult goodToGo = MessageBox.Show("Importing additional turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 2.3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    wakeModelList.ClearAll();
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
                else                
                    return;               

            }

            NewTurbine thisNew = new NewTurbine(this);
            thisNew.txtName.Clear();
            thisNew.txtUTMX.Clear();
            thisNew.txtUTMY.Clear();
            thisNew.ShowDialog();
        }

        private void btnEditTurb_Click(object sender, EventArgs e)
        { 
            // Opens EditTurbine dialog box so user can manually change the turbine coordinates
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot edit a turbine site while calculations are under way.", "Continuum 2.3");
                return;
            }

            if (lstTurbines.SelectedItems.Count == 0) {
                MessageBox.Show("Select a turbine to edit by clicking a turbine name.", "Continuum 2.3");
                return;
            }

            if (wakeModelList.NumWakeModels > 0) {

                DialogResult goodToGo = MessageBox.Show("Editing turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 2.3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    wakeModelList.ClearAll();
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
                else                
                    return;
            }

            string turbineName = lstTurbines.SelectedItems[0].Text;
            int turbInd = 0;
            
            for (turbInd = 0; turbInd < turbineList.TurbineCount; turbInd++)  
                if (turbineName == turbineList.turbineEsts[turbInd].name) 
                    // Found turbine to edit
                    break;

            EditTurbine thisEdit = new EditTurbine(this);
            thisEdit.txtName.Text = turbineList.turbineEsts[turbInd].name;
            thisEdit.txtName.ReadOnly = true;
            thisEdit.txtUTMX.Text = turbineList.turbineEsts[turbInd].UTMX.ToString();
            thisEdit.txtUTMY.Text = turbineList.turbineEsts[turbInd].UTMY.ToString();

            thisEdit.ShowDialog();
        }

        private void btnDelTurb_Click(object sender, EventArgs e)
        {
            
            // Checks that turbines can be deleted and checks with user that they want to delete them. Calls turbineList.DeleteTurbine and clears all net estimates
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot delete turbine sites while calculations are under way.", "Continuum 2.3");
                return;
            }

            if (BW_worker.BackgroundWorker_TurbCalcs.IsBusy) {
                MessageBox.Show("Cannot delete a turbine site while calculations are under way.", "Continuum 2.3");
                return;
            }

            if (lstTurbines.SelectedItems.Count == 0) {
                MessageBox.Show("Select at least one turbine to delete by clicking on a turbine name", "Continuum 2.3");                
                updateThe.TurbineList(this);
                return;
            }

            DialogResult goodToGo;
            // Clear all net estimates (different set of turbines)
            if (wakeModelList.NumWakeModels > 0) {
                goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 2.3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    wakeModelList.ClearAll();
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
                else                
                    return;         

            }
                        
            string turbineName;
            if (lstTurbines.SelectedItems.Count == 1) {

                turbineName = lstTurbines.SelectedItems[0].Text;
                goodToGo = MessageBox.Show("Are you sure you want to delete turbine: " + turbineName + "?", "Continuum 2.3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes) {
                    turbineList.DeleteTurbine(turbineName);
                    ChangesMade();
                }
            }
            else {
                int numTurbines = lstTurbines.SelectedItems.Count;
                goodToGo = MessageBox.Show("Are you sure you want to delete these " + numTurbines + " turbines?", "Continuum 2.3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes) {
                    for (int i = 0; i < numTurbines; i++) {
                        turbineName = lstTurbines.SelectedItems[i].Text;
                        turbineList.DeleteTurbine(turbineName);
                    }
                    ChangesMade();
                }
            }

            updateThe.AllTABs(this);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Calls SaveAs()
            SaveAs();
        }

        public bool SaveAs()
        {
            // Prompts user for folder and saves .CFM file
            Registration_key thisKey = new Registration_key(this);
            // TAKING OUT FOR TESTING CHANGE BACK BEFORE RELEASE
            //  thisKey.Check_Registration();
            functionalitiesEnabled = true;

            if (functionalitiesEnabled == false) {
                MessageBox.Show("Closing Continuum 2.3...", "Continuum 2.3");
                Close();
            }

            // Get map settings            
            updateThe.GetMapSettings(this);

            if (sfdCFMfile.ShowDialog() == DialogResult.OK) {
                string wholePath = sfdCFMfile.FileName;
                SetDefaultFolderLocations(wholePath);

                // Save mdf file to new location, update connection string
                // create database in new location
                BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
                argsForBW.oldFilename = savedParams.savedFileName;
                argsForBW.newFilename = sfdCFMfile.FileName;
                savedParams.savedFileName = sfdCFMfile.FileName;
                BW_worker = new BackgroundWork();
                BW_worker.Call_BW_SaveAs(argsForBW);

                FileStream fStream = new FileStream(sfdCFMfile.FileName, FileMode.Create, FileAccess.Write);
                BinaryFormatter bin = new BinaryFormatter();

                bin.Serialize(fStream, topo);
                bin.Serialize(fStream, radiiList);
                bin.Serialize(fStream, metList);
                bin.Serialize(fStream, turbineList);
                bin.Serialize(fStream, savedParams);
                bin.Serialize(fStream, mapList);
                bin.Serialize(fStream, metPairList);
                bin.Serialize(fStream, modelList);
                bin.Serialize(fStream, UTM_conversions);
                bin.Serialize(fStream, wakeModelList);
                bin.Serialize(fStream, otherLosses);

                fStream.Close();
                
                Text = savedParams.savedFileName;

                fileChanged = false;
                saveToolStripMenuItem.Enabled = true;
            }

            bool wasSaved = false;
            if (sfdCFMfile.FileName != "")
                wasSaved = true;

            return wasSaved;
        }

        public void SaveFile()
        {
            // Saves file using stored filename and path
            Registration_key thisKey = new Registration_key(this);
            thisKey.Check_Registration();

            if (functionalitiesEnabled == false) {
                MessageBox.Show("Closing Continuum 2.3...", "Continuum 2.3");
                Close();
            }

            // Get map settings            
            updateThe.GetMapSettings(this);

            if (savedParams.savedFileName == "" || savedParams.savedFileName == null) 
                return;

            FileStream fStream = new FileStream(savedParams.savedFileName, FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            topo.elevsForCalcs = null;
            topo.DH_ForCalcs = null;
            topo.SR_ForCalcs = null;

            bin.Serialize(fStream, topo);
            bin.Serialize(fStream, radiiList);
            bin.Serialize(fStream, metList);
            bin.Serialize(fStream, turbineList);
            bin.Serialize(fStream, savedParams);
            bin.Serialize(fStream, mapList);
            bin.Serialize(fStream, metPairList);
            bin.Serialize(fStream, modelList);
            bin.Serialize(fStream, UTM_conversions);
            bin.Serialize(fStream, wakeModelList);
            bin.Serialize(fStream, otherLosses);

            fStream.Close();
            fileChanged = false;
            Text = savedParams.savedFileName;

        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            // Prompts user to select a .CFM file and calls Open()            
            if (fileChanged == true) {

                DialogResult Save_file_int = MessageBox.Show("Do you want to save changes?", "", MessageBoxButtons.YesNo);

                if (Save_file_int == DialogResult.Yes) {
                    if (savedParams.savedFileName == "")
                        SaveAs();
                    else
                        SaveFile();                    
                }
            }

            // Prompt user to find cfm file
            if (ofdCFMfile.ShowDialog() == DialogResult.OK)
                Open(ofdCFMfile.FileName);           

        }

        public void Open(string Filename)
        {
            // Opens CFM file and updates GUI
            string wholePath = "";
            
            if (savedParams.pathName == null) {
                wholePath = Filename;
                SetDefaultFolderLocations(wholePath);
            }

            Registration_key thisKey = new Registration_key(this);
            thisKey.Check_Registration();

            if (functionalitiesEnabled == false) {
                MessageBox.Show("Closing Continuum...", "");
                Close();
            }
                        
            updateThe.NewProject(this);
            savedParams.savedFileName = Filename;

            FileStream fstream = new FileStream(Filename, FileMode.Open, FileAccess.Read);
            BinaryFormatter bin = new BinaryFormatter();
            bin.AssemblyFormat = FormatterAssemblyStyle.Simple;

            try {
                topo = (TopoInfo)bin.Deserialize(fstream);                
            }
            catch  {
                try
                {
                    // try to cast old TopoInfo object to new one
                }
                catch 
                {
                    MessageBox.Show("Error loading. Files created in previous versions of Continuum (prior to v2.2) cannot be opened and will need to be recreated.", "Continuum 2.3");
                    fstream.Close();
                    updateThe.NewProject(this);
                    return;
                }
            }

            try {
                radiiList = (InvestCollection)bin.Deserialize(fstream);
            }
            catch { 
              //  MessageBox.Show("Error reading the list of radii and exponents.", "");
              //  fstream.Close();
              //  updateThe.NewProject(this);
              //  return;
            }

            try {
                metList = (MetCollection)bin.Deserialize(fstream);
            }
            catch  {
                MessageBox.Show("Error reading the list of met sites.", "");
                fstream.Close();
                updateThe.NewProject(this);
                return;
            }

            if (metList.ThisCount > 0) 
                updateThe.WindRose(this);            

            // Older versions didn//t specify the number of sectors that the exposures were averaged.This goes through the list of mets and sets numSectors to 1.
            for (int i = 0; i < metList.ThisCount; i++) { 
                for (int j = 0; j < metList.metItem[i].ExposureCount; j++) {
                    if (metList.metItem[i].expo[j].numSectors == 0)
                        metList.metItem[i].expo[j].numSectors = 1;                    
                }
                if (metList.metItem[i].ExposureCount > 0)
                    metList.expoIsCalc = true;                
            }

            try {
                turbineList = (TurbineCollection)bin.Deserialize(fstream);
            }
            catch  {
                MessageBox.Show("Error reading the list of turbines.", "");
                fstream.Close();
                updateThe.NewProject(this);
                return;
            }

            try {
                savedParams = (Saved_Parameters)bin.Deserialize(fstream);
            }
            catch  {
                MessageBox.Show("Error reading the file.", "");
                fstream.Close();
                updateThe.NewProject(this);
                return;
            }

            try {
                mapList = (MapCollection)bin.Deserialize(fstream);
            }
            catch  {
                MessageBox.Show("Error reading the maps.", "");
                fstream.Close();
                updateThe.NewProject(this);
                return;
            }

            try {
                metPairList = (MetPairCollection)bin.Deserialize(fstream);
            }
            catch  {
                MessageBox.Show("Error reading the met pairs.", "");
                fstream.Close();
                updateThe.NewProject(this);
                return;
            }

            try {
                modelList = (ModelCollection)bin.Deserialize(fstream);
            }
            catch  {
                MessageBox.Show("Error reading the models.", "");
                fstream.Close();
                updateThe.NewProject(this);
                return;
            }           

            // check for database
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(savedParams.savedFileName);

            using (var ctx = new Continuum_EDMContainer(connString))
            {               
                try {
                    ctx.Database.Exists();
                }
                catch (Exception ex)  {
                    MessageBox.Show(ex.InnerException.ToString());                    
                    int slashInd = savedParams.savedFileName.LastIndexOf("\\");
                    string DB_name = savedParams.savedFileName.Substring(slashInd + 2, savedParams.savedFileName.Length - slashInd);
                    return;
                }
            }

            try {
                UTM_conversions = (UTM_conversion)bin.Deserialize(fstream);
                if (UTM_conversions.savedDatumIndex != 100 && UTM_conversions.UTMZoneNumber != -999) {
                    txtUTMDatum.Text = UTM_conversions.GetDatumString(UTM_conversions.savedDatumIndex);
                    txtUTMZone.Text = UTM_conversions.UTMZoneNumber.ToString() + UTM_conversions.hemisphere.Substring(0,1);
                }
            }
            catch  {
                MessageBox.Show("Error reading in UTM zone settings in Continuum file.", "Continuum 2.3");
                return;
            }

            try {
                wakeModelList = (WakeCollection)bin.Deserialize(fstream);
                wakeModelList.CleanUpWakeModelsAndGrid();
            }
            catch  {
                MessageBox.Show("Error reading in wake models in Continuum file.", "Continuum 2.3");
                return;
            }

            try {
                otherLosses = (Loss_factors)bin.Deserialize(fstream);
            }
            catch  {
                MessageBox.Show("Error reading in loss factors in Continuum file.", "Continuum 2.3");
                return;
            }

            fstream.Close();
                       

            if (topo.useSR == true || (topo.gotSR == true && metList.ThisCount == 0)) {
                chkUseSR.Checked = true;
                topo.useSR = true;
            }
            else 
                chkUseSR.Checked = false;

            if (topo.useSepMod == true)
                chk_Use_Sep.Checked = true;
            else
                chk_Use_Sep.Checked = false;

            updateThe.AllTABs(this);
            updateThe.SetDefaultCheckAdvanced(this);
           
            Text = savedParams.savedFileName;

            fileChanged = false;
            saveToolStripMenuItem.Enabled = true;
            tabContinuum.SelectedIndex = 0;                      

        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            // Checks if user wants to save and calls updateThe.NewProject()            
            if (fileChanged == true) {
                DialogResult Want_to_save_file = MessageBox.Show("Do you want to save changes?", "", MessageBoxButtons.YesNo);
                if (Want_to_save_file == DialogResult.Yes) {
                    if (savedParams.savedFileName != "")
                        SaveFile();
                    else
                        SaveAs();                    
                }
            }

            savedParams.savedFileName = "";
            fileChanged = false;
            Text = "Continuum Model.cfm";            
            updateThe.NewProject(this);
            MessageBox.Show("Please specify where to save file.", "Continuum 2.3");
            SaveAs();
            tabContinuum.SelectedIndex = 0;

        }

        public void ChangesMade()
        {
            // Updates file name on top of form with an * if the file has changed since it was last saved
            fileChanged = true;
            if (savedParams.savedFileName == "")
                Text = "Continuum Wind Flow Model.cfm*";
            else
                Text = savedParams.savedFileName + "*";            
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Calls SaveFile
            SaveFile();
        }

        private void ExportCalculatedValues()
        { 
            // Lists all mets and turbines on Expo_Export form and opens form
            if (metList.ThisCount == 0)                
                return;                                

            if (metList.metItem[0].ExposureCount == 0) {
                MessageBox.Show("You need to calculate exposures first.", "");
                return;
            }

            Expo_Export thisExport = new Expo_Export(this);
            thisExport.chkboxAllMets.Checked = true;

            thisExport.chkMets.Items.Clear();

            if (metList.ThisCount > 0) {
                thisExport.chkboxAllMets.Enabled = true;
                thisExport.chkboxAllMets.AutoCheck = true;
                thisExport.chkMets.Items.Clear();

                for (int i = 0; i < metList.ThisCount; i++)
                    thisExport.chkMets.Items.Add(metList.metItem[i].name, true);
            }
            else {
                thisExport.chkboxAllMets.Enabled = false;
                thisExport.chkMets.Items.Clear();
            }

            if (turbineList.turbineCalcsDone == true) {
                if (turbineList.TurbineCount > 0) {
                    thisExport.chkboxAllTurbs.Enabled = true;
                    thisExport.chkboxAllTurbs.Checked = true;
                    thisExport.chkTurbs.Items.Clear();

                    for (int i = 0; i < turbineList.TurbineCount; i++)
                        thisExport.chkTurbs.Items.Add(turbineList.turbineEsts[i].name, true);
                }
                else {
                    thisExport.chkboxAllTurbs.Enabled = false;
                    thisExport.chkTurbs.Items.Clear();
                }
                
            }
            else {
                thisExport.chkboxAllTurbs.Enabled = false;
                thisExport.chkTurbs.Items.Clear();
            }

            thisExport.chkboxAllRadii.Checked = true;
            thisExport.chkRadii.Items.Clear();
            
            for (int i = 0; i < radiiList.ThisCount; i++)
            {
                string Invest_str = "R= " + radiiList.investItem[i].radius.ToString();
                thisExport.chkRadii.Items.Add(Invest_str, true);
            }
            thisExport.chkboxAllRadii.Checked = true;

            thisExport.ShowDialog();
        }

        private void btnGenMap_Click(object sender, EventArgs e)
        { 
            // Sets default Min/Max X/Y for a map (or uses last map//s bounds) on GenMap form and opens form
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot generate a map while other calculations are under way.", "Continuum 2.3");
                return;
            }

            if (metList.ThisCount == 0) {
                MessageBox.Show("You need to import met TAB files first.", "Continuum 2.3");
                return;
            }

            if (topo.gotTopo == false)
            {
                MessageBox.Show("You need to import topography data first.", "Continuum 2.3");
                return;
            }

            if ((modelList.ModelCount <= 1 && metList.ThisCount > 1) || metList.expoIsCalc == false)
            {
                MessageBox.Show("The met sites have not been analyzed and a site-calibrated model has not been created yet. Starting those calculations now.", "No Site-Calibrated Model");
                Analyze_Mets();
                return;
            }
        
            if (BW_worker.BackgroundWorker_Map.IsBusy == false) {
                                
                double height = metList.metItem[0].height;
                int gridRadius = metList.metItem[0].gridStats.gridRadius;

                GenMap thisMap = new GenMap(this);
                thisMap.cboWhatToMap.SelectedIndex = 2;
                thisMap.cbo_GenMap_UWDW_mod.Items.Clear();
                                
                string modelStr = "Default Model";
                thisMap.cbo_GenMap_UWDW_mod.Items.Add(modelStr);

                if (metList.ThisCount > 1)
                {
                    modelStr = "Site-calibrated Model";
                    thisMap.cbo_GenMap_UWDW_mod.Items.Add(modelStr);
                }

                if (thisMap.cbo_GenMap_UWDW_mod.Items.Count > 1)
                    thisMap.cbo_GenMap_UWDW_mod.SelectedIndex = 1;
                else if (thisMap.cbo_GenMap_UWDW_mod.Items.Count > 0)
                    thisMap.cbo_GenMap_UWDW_mod.SelectedIndex = 0;                                          

                // if there are existing maps, use the coordinates from the last one created
                int numMaps = mapList.ThisCount;
                
                if (numMaps > 0 && savedParams.genMapMinUTMX == 0)
                {
                    thisMap.gridReso = mapList.mapItem[numMaps - 1].reso;
                    thisMap.minUTMX = mapList.mapItem[numMaps - 1].minUTMX;
                    thisMap.maxUTMX = thisMap.minUTMX + (mapList.mapItem[numMaps - 1].numX - 1) * mapList.mapItem[numMaps - 1].reso;
                    thisMap.minUTMY = mapList.mapItem[numMaps - 1].minUTMY;
                    thisMap.maxUTMY = thisMap.minUTMY + (mapList.mapItem[numMaps - 1].numY - 1) * mapList.mapItem[numMaps - 1].reso;                    
                }
                else if (savedParams.genMapMinUTMX != 0)
                {
                    thisMap.minUTMX = savedParams.genMapMinUTMX;
                    thisMap.maxUTMX = savedParams.genMapMaxUTMX;
                    thisMap.minUTMY = savedParams.genMapMinUTMY;
                    thisMap.maxUTMY = savedParams.genMapMaxUTMY;
                    thisMap.gridReso = savedParams.genMapReso;                    
                }
                else                
                    thisMap.gridReso = 250;

                thisMap.txtMapReso.Text = thisMap.gridReso.ToString();
                thisMap.UpdateLimits();                
                thisMap.UpdateTextboxes();
                
                thisMap.chkMetsToUse.Items.Clear();
                Met thisMet = null;

                for (int j = 0; j < metList.ThisCount; j++) {
                    thisMet = metList.metItem[j];
                    thisMap.chkMetsToUse.Items.Add(thisMet.name, true);
                }

                thisMap.ShowDialog();
            }
            else {
                MessageBox.Show("You can only generate one map at a time.", "Continuum 2.3");
                return;
            }

        }

        private void lstMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates displayed map and map stats            
            updateThe.MapsTAB(this);            
        }

        private void btnDelMaps_Click(object sender, EventArgs e)
        {
            // Asks user if they want to delete the map, if yes, calls mapList.RemoveMap
            if (BW_worker.BackgroundWorker_Map.IsBusy == false) {
                int numToDelete = lstMaps.SelectedItems.Count;

                if (numToDelete == 0) {
                    MessageBox.Show("Select one or more maps to delete.", "");
                    return;
                }

                string[] mapsToDelete = new string[numToDelete];                               

                for (int i = 0; i < lstMaps.SelectedItems.Count; i++)
                    mapsToDelete[i] = lstMaps.SelectedItems[i].Text;               
                               
                DialogResult yes_or_no = MessageBox.Show("Are you sure that you want to delete this map?", "", MessageBoxButtons.YesNo);

                if (yes_or_no == DialogResult.Yes) {
                    mapList.RemoveMap(mapsToDelete);
                    wakeModelList.RemoveWakeGridMapByName(mapsToDelete);
                    ChangesMade();
                }

                updateThe.MapsTAB(this);
                updateThe.NetTurbineEstsTAB(this);
                
            }
            else {
                MessageBox.Show("Cannot delete a map while one is being generated.", "Continuum 2.3");
                return;
            }

        }

        private void btnMapExportCSV_Click(object sender, EventArgs e)
        { 
            // Calls Export.ExportMapCSV to export map as a .CSV file
            if (lstMaps.SelectedItems.Count == 0) {
                MessageBox.Show("Select a map to export.");
                return;
            }

            if (lstMaps.SelectedItems.Count > 1) {
                MessageBox.Show("Can only export one map at a time.  Please select just one.");
                return;
            }

            string map_export = lstMaps.SelectedItems[0].Text;

            int numWD = GetNumWD();
            int WD_Ind = GetWD_ind("Maps");

            Export thisExport = new Export();
            thisExport.ExportMapCSV(this, map_export, WD_Ind, numWD);
        }

        private void btnExportWRG_Click(object sender, EventArgs e)
        { 
            // Asks user for air density and calls background worker to create a WRG (Wind Resource Grid) file
            if (lstMaps.SelectedItems.Count == 0) {
                MessageBox.Show("You need to select one map to export.", "Continuum 2.3");
                return;
            }

            if (lstMaps.SelectedItems.Count > 1) {
                MessageBox.Show("Can only create one WRG file at a time.  Please select only one map to use.", "Continuum 2.3");
                return;
            }

            string mapName = lstMaps.SelectedItems[0].Text;
            double density = 0;

            try {
                density = Convert.ToSingle(Interaction.InputBox("Enter the average density at the site (for power density calculation).  Units: kg/m^3"));
            }
            catch {
                return;
            }

            if (density <= 0.5 || density > 2 ) {
                MessageBox.Show("Invalid entry for density.");
                return;
            }

            //  Export.Export_Map_WRG(mapName, density, metList, turbineList, mapList, modelList)
            string outputFile = "";

            if (sfdWRG.ShowDialog() == DialogResult.OK) 
                outputFile = sfdWRG.FileName;            
            else {
                return;
            }

            BackgroundWork.Vars_for_WRG_file The_args = new BackgroundWork.Vars_for_WRG_file();

            The_args.density = density;
            The_args.thisInst = this;
            The_args.mapName = mapName;
            The_args.outputFile = outputFile;
            BW_worker = new BackgroundWork();
            BW_worker.Call_BW_WRG_create(The_args);
            ChangesMade();

        }

        private void btnRefreshMap_Click(object sender, EventArgs e)
        {
            // Updates the displayed map            
            updateThe.Generated2DMap(this);
        }

        private void AboutContinuumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Shows the //About Continuum// form
            AboutContinuum thisAbout = new AboutContinuum();
            thisAbout.ShowDialog();          
        
        }               

        private void btnRefreshMain_Click(object sender, EventArgs e)
        {
            // Refreshes topo/LC/SR/DH map            
            updateThe.TopoMap(this);
            ChangesMade();
        }

        private void EnterRegistrationKeyToolStripMenuItem1_Click(Object sender, EventArgs e)
        {
            // Opens Registration Key dialog form
            Registration_key thisKey = new Registration_key(this);
            thisKey.Check_Registration();
            thisKey.ShowDialog();
            if (functionalitiesEnabled == false) {
                MessageBox.Show("Closing Continuum...", "Continuum 2.3");
                Close();
            }
        }

        private void chkMetLabels_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates labels on map and series shown on wind rose and directional WS ratio plots based on selected met sites
            if (okToUpdate == true) {
                updateThe.InputTAB(this);
                ChangesMade();
            }
        }

        private void chkTurbLabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates labels on map based on selected turbine sites
            if (okToUpdate == true)
            {                
                updateThe.TopoMap(this);
                ChangesMade();
            }
        }

        private void chkAllMetLabels_CheckedChanged(object sender, EventArgs e)
        {
            // Selects or deselects all met sites in list (on Input tab) updates the labels on the topo map and directional WS ratio plot (to do: update windRose plot) 
            if (okToUpdate == true)
            {
                if (chkMetLabels.Items.Count > 0)
                {
                    if (chkAllMetLabels.CheckState == CheckState.Checked)
                    {
                        for (int i = 0; i < chkMetLabels.Items.Count; i++)
                            chkMetLabels.SetItemChecked(i, true);
                    }
                    else
                    {
                        for (int i = 0; i < chkMetLabels.Items.Count; i++)
                            chkMetLabels.SetItemChecked(i, false);
                    }
                }                          
                
                updateThe.InputTAB(this);
            }
        }

        private void chkAllTurbLabels_CheckedChanged(object sender, EventArgs e)
        {
            // Selects or deselects all turbines and updates labels on map (Input tab)
            if (okToUpdate == true)
            {
                if (chkTurbLabels.Items.Count > 0)
                {
                    if (chkAllTurbLabels.CheckState == CheckState.Checked)
                    {
                        for (int i = 0; i < chkTurbLabels.Items.Count; i++)
                            chkTurbLabels.SetItemChecked(i, true);
                    }
                    else
                    {
                        for (int i = 0; i < chkTurbLabels.Items.Count; i++)
                            chkTurbLabels.SetItemChecked(i, false);
                    }
                }

                updateThe.InputTAB(this);
            }
        }

        private void chkMetLabels_Maps_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the met labels on the map (Map tab)
            if (okToUpdate == true)
            {                
                updateThe.Generated2DMap(this);
                ChangesMade();
            }
        }

        private void chkAllTurbs_Maps_CheckedChanged(object sender, EventArgs e)
        { 
            // Selects/deselects all turbines in list and updates the turbine labels on the map (Map tab)
            if (chkTurbLabels_Maps.Items.Count > 0) {
                if (chkAllTurbs_Maps.CheckState == CheckState.Checked) {
                    for (int i = 0; i < chkTurbLabels_Maps.Items.Count; i++)
                        chkTurbLabels_Maps.SetItemChecked(i, true);
                }
                else {
                    for (int i = 0; i < chkTurbLabels_Maps.Items.Count; i++)
                        chkTurbLabels_Maps.SetItemChecked(i, false);
                }
            }
            
            updateThe.Generated2DMap(this);            
        }

        private void chkAllMets_Maps_CheckedChanged(object sender, EventArgs e)
        { 
            // Selects/deselects all mets in list and updates the met labels on the map (Map tab)
            if (chkMetLabels_Maps.Items.Count > 0) {
                if (chkAllMets_Maps.CheckState == CheckState.Checked ) {
                    for (int i = 0; i < chkMetLabels_Maps.Items.Count; i++)
                        chkMetLabels_Maps.SetItemChecked(i, true);
                }
                else {
                    for (int i = 0; i < chkMetLabels_Maps.Items.Count; i++)
                        chkMetLabels_Maps.SetItemChecked(i, false);                    
                }
            }
                            
            updateThe.Generated2DMap(this);            
        }

        private void chkTurbLabels_Maps_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the turbine labels on the map (Map tab)
            if (okToUpdate == true)
            {                
                updateThe.MapTAB(this);
                ChangesMade();
            }
        }

        private void HelpMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Opens user manual
            string filePath = "C:\\Program Files\\One Energy\\Continuum 2.3\\Documentation\\Continuum 2.3 Users Manual v1.0.pdf";

            if (File.Exists(filePath))
            {
                try
                {
                    Process.Start(filePath);
                }
                catch 
                { }
            }

        }

        private void cboStartMet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the mets/turbines listed in //To Met or Turbine// dropdown on Advanced tab (which then updates the plots and tables)
            if (okToUpdate == true)                            
                updateThe.AdvancedTAB(this);
        }

        private void cboEndMet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the node path plot and table and map (Advanced tab)
            if (okToUpdate == true) 
                updateThe.AdvancedTAB(this);            
        }

        private void cboOrig_or_Mod_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the Advanced tab plots and tables based on selected Continuum model
            if (okToUpdate == true) 
                updateThe.AdvancedTAB(this);
        }

        private void chkAllMetLabelsStep_CheckedChanged(object sender, EventArgs e)
        { 
            // Selects/deselects all Mets listed on Advanced tab and updates labels on map
            if (chkMetlabelsStep.Items.Count > 0) {
                if (chkAllMetLabelsStep.CheckState == CheckState.Checked) {
                    for (int i = 0; i < chkMetlabelsStep.Items.Count; i++)
                        chkMetlabelsStep.SetItemChecked(i, true);
                }
                else {
                    for (int i = 0; i < chkMetlabelsStep.Items.Count; i++)
                        chkMetlabelsStep.SetItemChecked(i, false);
                }
            }            
            
            updateThe.StepTopoMap(this);            
        }

        private void chkAllTurbsStep_CheckedChanged(object sender, EventArgs e)
        { 
            // Selects/deselects all Turbines listed on Advanced tab and updates labels on map
            if (chkTurbLabelStep.Items.Count > 0) {
                if (chkAllTurbsStep.CheckState == CheckState.Checked ) {
                    for (int i = 0; i < chkTurbLabelStep.Items.Count; i++) 
                        chkTurbLabelStep.SetItemChecked(i, true);
                }
                else {
                    for (int i = 0; i < chkTurbLabelStep.Items.Count; i++) 
                            chkTurbLabelStep.SetItemChecked(i, false);                    
                }
            }
               
            updateThe.StepTopoMap(this);            
        }

        private void chkMetlabelsStep_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates labels on map (Advanced tab) based on selected met sites            
            updateThe.StepTopoMap(this);
        }

        private void chkTurbLabelStep_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates labels on map (Advanced tab) based on selected turbine sites            
            updateThe.StepTopoMap(this);
        }

        private void cboPowerCrvs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the gross turbine estimates plots and tables based on selected power curve
            if (okToUpdate == true)
            {
                int selInd = 0;

                if (cboPowerCrvs.Items.Count == 0)
                    return;
                else
                {
                    try
                    {
                        selInd = cboPowerCrvs.SelectedIndex;
                    }
                    catch 
                    {
                        cboPowerCrvs.SelectedIndex = 0;
                        selInd = 0;
                    }

                    updateThe.GrossTurbineEstsTAB(this);
                }
            }
        }

        private void btnExportStepwise_Click(object sender, EventArgs e)
        {
            // Calls Export.ExportNodesAndWS to export estimates calculated along path of nodes for selected start and end met/turbine
                  
            if (cboAdvancedModel.Items.Count == 0) {
                MessageBox.Show("No models have been created yet.", "Continuum 2.3");
                return;
            }                   

            Export thisExport = new Export();
            thisExport.ExportNodesAndWS(this);
        }

        private void btnDoRRCalcs_Click(object sender, EventArgs e)
        { 
            // if there are enough mets for a Round Robin analysis, creates struct for Round Robin calculations and calls background worker to conduct analysis
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot conduct Round Robin analysis while other calculations are under way.", "Continuum 2.3");
                return;
            }

            if (metList.expoIsCalc == false)
            {
                MessageBox.Show("Met sites have been analyzed yet. Conducting calculations now.");
                Analyze_Mets();
                return;
            }

            int minRR_Size = 0;

            try {
                string selStr = cboRR_MinSize.SelectedItem.ToString();
                minRR_Size = Convert.ToInt16(selStr.Substring(0, selStr.Length - 5));
            }
            catch  {
                return;
            }                       

            string[] metsUsed = metList.GetMetsUsed();

            // check to see if RR has been conducted already
            bool RR_done = metPairList.RR_DoneAlready(true, false, metsUsed, minRR_Size, metList);

            if (RR_done == true) // Update plots and tables
                return;

            BackgroundWork.Vars_for_RoundRobin argsForBW = new BackgroundWork.Vars_for_RoundRobin();
            argsForBW.thisInst = this;
            argsForBW.Min_RR_Size = minRR_Size;

            // Call background worker to run calculations
            BW_worker = new BackgroundWork();
            BW_worker.Call_BW_RoundRobin(argsForBW);
            
        }

        private void cboRoundRobin_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates the Round Robin plots and tables based on selected Round Robin from dropdown
            if (okToUpdate == true)
            {
                updateThe.RoundRobinIndivResults(this);
                updateThe.RoundRobinHistogram(this);
            }
                           

        }

        private void btnExportRR_Click(object sender, EventArgs e)
        {
            // Calls Export.Export_RoundRobin_CSV
            Export thisExport = new Export();
            thisExport.Export_RoundRobin_CSV(this);
        }

        private void cboRadDisplay_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates plots and tables on Advanced tab based on selected radius of investigation
            if (okToUpdate == true)                 
                updateThe.AdvancedTAB(this);            
        }

        private void btnGenTurbEsts_Click(object sender, EventArgs e)
        { 
            // Checks that turbine calculations can take place, creates the struct needed for turbine calcs and calls background worker
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot generate turbine estimates while other calculations are under way.", "Continuum 2.3");
                return;
            }   
            
            if (metList.ThisCount == 0)
            {
                MessageBox.Show("You need to import met sites fist.", "Continuum 2.3");
                return;
            }

            if (turbineList.TurbineCount == 0) {
                MessageBox.Show("You need to import turbines fist.", "Continuum 2.3");
                return;
            }

            if (modelList.ModelCount <= 1 && metList.ThisCount > 1 ) {
                MessageBox.Show("Create a site-calibrated model first clickng Analyze Mets.", "Continuum 2.3");
                return;
            }

            if (modelList.ModelCount == 0) {
                MessageBox.Show("Create a Continuum model first clickng Analyze Mets.", "Continuum 2.3");
                return;
            }
                  
             turbineList.ClearWS_EstCalcs(metList);

            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();
                        
            argsForBW.thisInst = this;
            argsForBW.thisWakeModel = null;
            argsForBW.isCalibrated = false;

            // Call background worker to run calculations
            BW_worker = new BackgroundWork();
            BW_worker.Call_BW_TurbCalcs(argsForBW);
            ChangesMade();
            
        }
        
        private void btnImportTAB_Click(object sender, EventArgs e)
        { 
            // Calls ImportMetsTAB, if successful and if have topo and land cover data, creates struct needed for Met calcs and calls background worker otherwise updates 
            // all plots and tables referencing the met sites
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import met data while calculations are under way.", "Continuum 2.3");
                return;
            }

            bool reCalcEsts = ImportMetsTAB();

            if (reCalcEsts == true) {
                if (metList.ThisCount == 0) 
                    return;           
                                                          
                updateThe.AllTABs(this);

                if (topo.gotTopo == false)                     
                    return;                

                if (chkUseSR.Checked == true && topo.gotSR == true)
                    topo.useSR = true;
                else
                    topo.useSR = false;

                if (chk_Use_Sep.Checked == true)
                    topo.useSepMod = true;
                else
                    topo.useSepMod = false;              
                                
                if (topo.gotSR == false) {
                    DialogResult yes_or_no = MessageBox.Show("Do you have land cover data to import? Click No and Continuum will start the site-calibration using only the topography data.", "Continuum 2.3", MessageBoxButtons.YesNo);
                    if (yes_or_no == DialogResult.Yes) {                        
                        updateThe.AllTABs(this);                        
                        updateThe.SetDefaultCheckAdvanced(this); 
                        return;
                    }
                }
                
                // Call background worker to run calculations
                // In background worker, performs exposure and grid stat calculations, finds path of nodes and does site calibration.
                BW_worker = new BackgroundWork();
                BW_worker.Call_BW_MetCalcs(this);
                ChangesMade();
            }

        }

        private void btnImportCRV_Click(object sender, EventArgs e)
        { 
            // Prompts user to open a power curve. thisPowerCurve file and updates turbine calcs if they were done before
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import a power curve while calculations are under way.", "Continuum 2.3");
                return;
            }

            if (ofdPowerCurve.ShowDialog() == DialogResult.OK) {

                          
                int WS_Ind = 0;
                double[,] powerCurve = new double[3, WS_Ind + 1];

                string wholePath = ofdPowerCurve.FileName;

                string[] fileRow;
                string fileName = ofdPowerCurve.FileName;

                StreamReader sr = new StreamReader(fileName);

                char[] delims = new char[2];
                delims[0] = '\t';
                delims[1] = ',';
                
                // Read in power curve name               
                string powerCurveName = sr.ReadLine();

                // Make sure power curve hasn't already been entered                
                int numPowerCurves = turbineList.PowerCurveCount;

                for (int i = 0; i < numPowerCurves; i++) { 
                    if (turbineList.powerCurves[i].name == powerCurveName) {
                        MessageBox.Show("A power curve of the same name has already been imported.", "Continuum 2.3");
                        sr.Close();
                        return;
                    }
                }

                while (sr.EndOfStream == false)
                {
                    // Read in Met name
                    string dataStr = sr.ReadLine();
                    fileRow = dataStr.Split(delims);

                    if (fileRow.Length >= 3) {
                        try {
                            if (Convert.ToSingle(fileRow[0]) > 0 && Convert.ToSingle(fileRow[1]) > 0 && Convert.ToSingle(fileRow[2]) > 0) {
                                ResizeArray(ref powerCurve, 3, WS_Ind + 1);
                                powerCurve[0, WS_Ind] = Convert.ToSingle(fileRow[0]);
                                powerCurve[1, WS_Ind] = Convert.ToSingle(fileRow[1]);
                                powerCurve[2, WS_Ind] = Convert.ToSingle(fileRow[2]);
                                WS_Ind++;
                            }
                        }
                        catch {
                            MessageBox.Show("Error while importing power curve file.  Please check your file.", "Continuum 2.3");
                            sr.Close();
                            return;
                        }
                    }
                    else if (fileRow.Length == 2) {
                        MessageBox.Show("power curve files must include wind speed, power and thrust coefficients. Please check your file.", "Continuum 2.3");
                        sr.Close();
                        return;
                    }
                }

                sr.Close();

                turbineList.ReadPowerCurve(this, powerCurveName, powerCurve);
                if (turbineList.turbineCalcsDone == true) {
                    turbineList.CalcGrossAEP(this, true);
                    turbineList.CalcGrossAEP(this, false);
                }
                                
                updateThe.GrossTurbineEstsTAB(this);
                updateThe.Uncertainty_TAB_Turbine_Ests(this);                
                updateThe.ColoredButtons(this);
            }
        }

        private void btnDelPowerCrv_Click(object sender, EventArgs e)
        {
            // Calls turbineList.DeletePowerCurve()
            string powerCurveToDelete = "";

            if (cboPowerCrvs.Items != null) {
                if (cboPowerCrvs.Items.Count > 0) {
                    try {
                        powerCurveToDelete = cboPowerCrvs.SelectedItem.ToString();
                    }
                    catch {
                        MessageBox.Show("No power curve selected to delete.", "Continuum 2.3");
                        return;
                    }

                    turbineList.DeletePowerCurve(powerCurveToDelete);                    
                    updateThe.GrossTurbineEstsTAB(this);
                    updateThe.Uncertainty_TAB_Turbine_Ests(this);
                }
            }
            else {
                MessageBox.Show("No power curves to delete", "Continuum 2.3");
            }
        }

        private void chkTurbGross_SelectedIndexChanged(object sender, EventArgs e) 
            { 
            // Updates the plots and tables on Gross Turb Est tab based on selected turbines
            if (okToUpdate == true)                 
                updateThe.GrossTurbineEstsTAB(this); 
        }

        private void chkMetSummAll_CheckedChanged(object sender, EventArgs e)
        { 
            // Selects/Deselects all Met sites on //Met and Turbine Summary// tab and calls updateThe.Met_Summary_Tables_and_Plots
            if (IsHandleCreated) {
                if (chkMetSumm.Items.Count > 0) {
                    if (chkMetSummAll.CheckState == CheckState.Checked) {
                        for (int i = 0; i < chkMetSumm.Items.Count; i++)
                            chkMetSumm.SetItemChecked(i, true);
                    }
                    else {
                        for (int i = 0; i < chkMetSumm.Items.Count; i++)
                            chkMetSumm.SetItemChecked(i, false);
                    }
                }
                                
                updateThe.Met_Turbine_Summary_TAB(this);
            }
        }

        private void chkTurbGrossAll_CheckedChanged(object sender, EventArgs e)
        { 
            // Selects/Deselects all Turbines sites on //Gross Turbine Est// tab and updates plots and tables
            if (IsHandleCreated) {
                if (chkTurbGross.Items.Count > 0) {
                    if (chkTurbGrossAll.CheckState == CheckState.Checked) {
                        for (int i = 0; i < chkTurbGross.Items.Count; i++) 
                            chkTurbGross.SetItemChecked(i, true);
                    }
                    else {
                        for (int i = 0; i < chkTurbGross.Items.Count; i++) 
                            chkTurbGross.SetItemChecked(i, false);                        
                    }
                }
                
                updateThe.GrossTurbineEstsTAB(this);                                
            }
        }

        private void btnWS_AEP_Exprt_Click(object sender, EventArgs e)
        {
            // Calls Export.Export_WS_AEP() to export estimated gross AEP at selected turbine sites
            string powerCurve = "";

            if (cboPowerCrvs.Items.Count > 0) {
                try
                {
                    powerCurve = cboPowerCrvs.SelectedItem.ToString();
                }
                catch {
                    return;
                }
            }

            Export thisExport = new Export();
            thisExport.Export_WS_AEP(this, powerCurve);
        }

        private void cboUncert_WS_AEP_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Calls updateThe.TurbineUncertPlot based on whether WS or AEP is chosen from dropdown
            if (okToUpdate == true)                 
                updateThe.Uncertainty_TAB_Turbine_Ests(this);            
        }

        private void cboUncertPowerCrv_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Calls functions to update turbine uncertainty estimates based on selected power curve
            if (okToUpdate == true)                 
                updateThe.Uncertainty_TAB_Turbine_Ests(this);
        }

        private void chkP99_CheckedChanged(object sender, EventArgs e)
        { 
            // Calls updateThe.TurbineUncertPlot to display P99 estimates or not
            if (okToUpdate == true)
                updateThe.Uncertainty_TAB_Turbine_Ests(this);            
        }

        private void chkP50_CheckedChanged(object sender, EventArgs e)
        { 
            // Calls updateThe.TurbineUncertPlot to display P50 estimates or not
            if (okToUpdate == true)
                updateThe.Uncertainty_TAB_Turbine_Ests(this);            
        }

        private void chkP90_CheckedChanged(object sender, EventArgs e)
        { 
            // Calls updateThe.TurbineUncertPlot to display P90 estimates or not
            if (okToUpdate == true)
                updateThe.Uncertainty_TAB_Turbine_Ests(this);            
        }

        private void btnExportWSDists_Click(object sender, EventArgs e)
        {
            // Calls Export.ExportGrossWS_Dists to export gross wind speed distributions at selected mets and turbines
            Export thisExport = new Export();
            thisExport.ExportGrossWS_Dists(this);
        }

        private void btnExportTurbUncert_Click(object sender, EventArgs e)
        {
            // Calls Export.Export_WS_AEP_Uncert to export wind speed and AEP P50/P90/P99 estimates
            string powerCurve = "";

            try {
                powerCurve = cboUncertPowerCrv.SelectedItem.ToString();
            }
            catch {
                return;
            }

            Export thisExport = new Export();
            thisExport.Export_WS_AEP_Uncert(this, powerCurve);
        }

        private void btnExportCRV_Click(object sender, EventArgs e)
        {
            // Calls Export.Export_CRV to export selected power curve
            string powerCurve = "";

            try {
                powerCurve = cboPowerCrvs.SelectedItem.ToString();
            }
            catch {
                return;
            }

            if (powerCurve == "No power Curves Imported") {
                MessageBox.Show("No power curves have been imported yet.", "Continuum 2.3");
                return;
            }
            Export thisExport = new Export();
            thisExport.Export_CRV(this, powerCurve);
        }

        private void cbo_TurbEst_Modtype_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates the gross turbine estimates based on selected Continuum model
            if (okToUpdate == true)                 
                updateThe.GrossTurbineEstsTAB(this); 
        }

        private void cbo_Uncert_ModType_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates the turbine uncertainty estimates based on selected Continuum model
            if (okToUpdate == true)
                updateThe.Uncertainty_TAB_Turbine_Ests(this); 
        }

        private void btnAnalyzeMets_Click(object sender, EventArgs e)
        { 
            // Creates struct needed for analyzing met sites and calls background worker to analyze mets and create site-calibrated models
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot analyze met data while calculations are under way.", "Continuum 2.3");
                return;
            }

            Analyze_Mets();            
        }

        public void Analyze_Mets()
        {
            if (metList.ThisCount == 0)
                return;                      

            modelList.ClearAllExceptDefaultAndImported();

            if (chkUseSR.Checked == true)
                topo.useSR = true;
            else
                topo.useSR = false;

            // Call background worker to run calculations
            BW_worker = new BackgroundWork();
            BW_worker.Call_BW_MetCalcs(this);
            ChangesMade();
        }
             
        private void btnExportCrossPreds_Click(object sender, EventArgs e)
        {
            // Calls Export.ExportMetCrossPreds to export met cross-prediction errors based on selected model, radius, and wind direction sector             
            Export thisExport = new Export();
            thisExport.ExportMetCrossPreds(this);
        }

        private void cboWDsector_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Calls Update class functions to update advanced tab based on selected WD sector
            if (okToUpdate == true)                 
                updateThe.AdvancedTAB(this);            
        }

        private void cboUphill_to_show_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Calls updateThe.ModelPlots to show either uphill or induced speed-up            
            updateThe.ModelPlots(this);
        }

        private void btnExportModel_Click(object sender, EventArgs e)
        { 
            // Calls Export.Export_Models_CSV() to export model coeffs to a CSV file
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot export model info while calculations are under way.", "Continuum 2.3");
                return;
            }                   

            Export thisExport = new Export();
            thisExport.Export_Models_CSV(this);
        }

        private void btnExportDirWS_Click(object sender, EventArgs e)
        {
            // Calls Export.Export_Directional_WS to export directional wind speeds at selected mets and turbines (Met and Turbine Summary tab) 
            Export thisExport = new Export();
            thisExport.Export_Directional_WS(this, "Summary");
        }

        private void cboGrossTurbWD_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Calls Update functions to update Gross Turb Est tab based on selected WD sector
            if (okToUpdate == true)                
                updateThe.GrossTurbineEstsTAB(this);            
        }

        private void cboMapWD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Calls updateThe.Generated2DMap to show map selected from list
            if (okToUpdate == true)
            {
                updateThe.Generated2DMap(this);
                updateThe.MapStats(this);
            }
        }

        private void btnExportDirWSDists_Click(object sender, EventArgs e)
        {
            // Calls Export.ExportDirectionalWS_Dists to export the directional wind speed distributions at selected met and turbine sites
            Export thisExport = new Export();
            thisExport.ExportDirectionalWS_Dists(this);
        }

        private void cboTopo_Or_Roughness_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Calls updateThe.TopoMap to show the selected map (Input tab)
            if (Created) {
                if (cboTopo_Or_Roughness.SelectedIndex == 0 && topo.gotTopo == false)
                    return;
                else if (cboTopo_Or_Roughness.SelectedIndex > 0 && topo.gotSR == false)
                    cboTopo_Or_Roughness.SelectedIndex = 0;
                                
                updateThe.TopoMap(this);
            }
        }

        public void Populate_LC_Key_Form(LC_Key thisKey)
        {
            // Configures settings on LC_Key_Form and opens form            
            thisKey.lstLC_SR_DH.Items.Clear();

            int numSR = 0;

            try
            {
                numSR = thisKey.LC_Key_New.Length;
            }
            catch { }                   

            if (numSR == 0) {                
                topo.SetUS_NLCD_Key();
                numSR = topo.LC_Key.Length;
                thisKey.LC_Key_Orig = topo.LC_Key;
                thisKey.LC_Key_New = topo.LC_Key;
            }

            // See if using a default or user-defined key
            bool Using_NLCD = topo.LC_IsDefaultNLCD(thisKey.LC_Key_New);
            bool Using_NALC = topo.LC_IsDefaultNALC(thisKey.LC_Key_New);
            bool Using_EULC = topo.LC_IsDefaultEU_Corine(thisKey.LC_Key_New);

            if (Using_NLCD == true)
                thisKey.cboLC_Key.SelectedIndex = 0;
            else if (Using_NALC == true)
                thisKey.cboLC_Key.SelectedIndex = 1;
            else if (Using_EULC == true)
                thisKey.cboLC_Key.SelectedIndex = 2;
            else
                thisKey.cboLC_Key.SelectedIndex = 3;

            thisKey.Update_LC_table();

        }

        private void btnViewModNLCD_Click(object sender, EventArgs e)
        { 
            // Opens land cover key to view/edit surface roughness and displacement height values
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import land cover data while calculations are under way.", "Continuum 2.3");
                return;
            }

            if (topo.LC_Key == null)
                topo.SetUS_NLCD_Key();

            LC_Key thisKey = new LC_Key(this);

            if (topo.LC_Key != null)                            
                thisKey.Read_Orig_and_New(topo.LC_Key);
            
            Populate_LC_Key_Form(thisKey);            
            updateThe.LC_KeySelected(this);

            thisKey.ShowDialog();
        }

        private void btnExportExpoSRDH_Click(object sender, EventArgs e)
        {
            // Opens exposure and SR/DH met/turbine export form
            ExportCalculatedValues();
        }

        private void chkMetGrossAll_CheckedChanged(object sender, EventArgs e)
        { 
            // Updates tables and plots on Gross Energy Estimate tab based on selected mets in list
            if (Created) {
                if (chkMetGross.Items.Count > 0) {
                    if (chkMetGrossAll.CheckState == CheckState.Checked) {
                        for (int i = 0; i < chkMetGross.Items.Count; i++) 
                            chkMetGross.SetItemChecked(i, true);
                    }
                    else {
                        for (int i = 0; i < chkMetGross.Items.Count; i++)
                            chkMetGross.SetItemChecked(i, false);                        
                    }
                }
                
                updateThe.GrossTurbineEstsTAB(this);
            }
        }

        private void chkMetSumm_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (okToUpdate == true)                 
                updateThe.Met_Turbine_Summary_TAB(this);            
        }

        private void chkTurbSumm_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (okToUpdate == true)                 
                updateThe.Met_Turbine_Summary_TAB(this);            
        }

        private void chkTurbSummAll_CheckedChanged(object sender, EventArgs e)
        { 
            // Updates tables and plots on Gross Energy Estimate tab based on selected turbines in list
            if (Created) {
                if (chkTurbSumm.Items.Count > 0) {
                    if (chkTurbSummAll.CheckState == CheckState.Checked) {
                        for (int i = 0; i < chkTurbSumm.Items.Count; i++)
                            chkTurbSumm.SetItemChecked(i, true);
                    }
                    else {
                        for (int i = 0; i < chkTurbSumm.Items.Count; i++)
                            chkTurbSumm.SetItemChecked(i, false);
                    }
                }
                                
                updateThe.Met_Turbine_Summary_TAB(this);
            }
        }         

        private void cboMetSum_Rad_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates the plots and tables on the //Met and Turbine Summary// tab based on selected radius from drop-down
            if (okToUpdate == true)                 
                updateThe.Met_Turbine_Summary_TAB(this);            
        }

        private void cboMetSum_WD_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates the plots and tables on the //Met and Turbine Summary// tab based on selected wind direction sector from drop-down
            if (okToUpdate == true)                 
                updateThe.Met_Turbine_Summary_TAB(this);            
        }

        private void chkMetGross_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates the plots and tables on the //Gross Energy Ests.// tab based on selected mets from list
            if (okToUpdate == true)                
                updateThe.GrossTurbineEstsTAB(this);
        }

        private void chkPowerCurveList_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates the power curve plot on //Gross Energy Ests.// tab based on power curve selected in list
            if (okToUpdate == true)                 
                updateThe.PowerCrvPlot(this);            
        }

        private void cboGrossParam_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates the histogram on //Gross Energy Ests// tab based on parameter chosen from dropdown menu
            if (okToUpdate == true)                 
                updateThe.GrossHistogram(this);            
        }

        private void chkUseSR_CheckedChanged(object sender, EventArgs e)
        { 
            // Recreates site-calibrated model with/without the surface roughness model based on whether //Use Surface roughness// checkbox is checked or not
            if (Created && metList.ThisCount > 0 && modelList.ModelCount > 4 && BW_worker.IsBusy())
            {
                if ( (chkUseSR.Checked = false && topo.useSR == false) ||(chkUseSR.Checked == true && topo.useSR == true) ||(chkUseSR.Checked == false && topo.gotSR == false) ) 
                  return;
              else if ((chkUseSR.Checked == true && topo.gotSR == false) ) {
                    chkUseSR.Checked = false;
                 return;

                }

                DialogResult yes_or_no = MessageBox.Show("Changing this setting will recreate the wind flow model and estimates. Do you want to continue?", "Continuum 2.3", MessageBoxButtons.YesNo);
                
                if (yes_or_no == DialogResult.Yes) {

                    modelList.ClearAllExceptDefaultAndImported();
                    metPairList.RemoveAllExceptDefault();
                    turbineList.ClearWS_EstCalcs(metList);
                    turbineList.ClearAllGrossEsts();
                    turbineList.ClearAllNetEsts();
                    metPairList.ClearRoundRobin();                                    

                    if (chkUseSR.Checked == true)
                        topo.useSR = true;
                    else
                        topo.useSR = false;                                       

                    // Call background worker to run calculations
                    BW_worker = new BackgroundWork();
                    BW_worker.Call_BW_MetCalcs(this);
                    ChangesMade();
                 }
            }
        }

        private void cboExpo_or_Stab_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the log-log plots on //Advanced// tab based on whether Exposure or Stability is chosen from dropdown                
            updateThe.ModelPlots(this);
        }
        
        private void chkAdvToShow_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates the table and plot on Advanced tab showing values along path of nodes
             if (okToUpdate == true)             
                updateThe.AdvancedTAB(this);
        }
        
        private void btnWakeLossCalc_Click(object sender, EventArgs e)
        {
            // Opens Gen_WakeModel form to create a new wake loss model
            if (turbineList.PowerCurveCount == 0) {
                MessageBox.Show("Import a power and thrust curve to conduct wake modeling.", "Continuum 2.3");
                    return;
            }

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot generate a wake model while other calculations are under way.", "Continuum 2.3");
                    return;
            }

            Gen_WakeModel thisWake = new Gen_WakeModel(this);
            
            thisWake.cboWakeCombo.SelectedIndex = 0;
            thisWake.cboWakeModel.SelectedIndex = 0;
            thisWake.cboPowerCrvs.SelectedIndex = 0;
            thisWake.txtHorizWakeExp.Text = "5";
            thisWake.txtAmbTI.Text = "10";
            thisWake.numWRR.Value = 0;
            thisWake.numWakeExp.Value = 0;

            thisWake.txtCrossSpace.Clear();
            thisWake.txtCrossSpace.Enabled = false;
            thisWake.txtDownSpace.Clear();
            thisWake.txtDownSpace.Enabled = false;
            thisWake.txtAmbRough.Clear();
            thisWake.txtAmbRough.Enabled = false;

            thisWake.ShowDialog();
        }

        private void cboTurbEstWD_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates the plots and tables on Net Turbine Ests. tab based on WD sector selected from dropdown
            if (okToUpdate == true)                 
                updateThe.NetTurbineEstsTAB(this);            
        }

        private void cboWakePlot_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates the wake loss map and Turb by string plot on Net Turbine Ests. tab based on whether Wind Speed, Wake loss % or Net Energy is selected from dropdown
            if (okToUpdate == true)                 
                updateThe.NetTurbineEstsTAB(this); 
        }
                
        private void lstWakeModels_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates plots and tables on Net Turbine Ests tab based on selected wake loss model
            if (okToUpdate == true)                 
                updateThe.NetTurbineEstsTAB(this);            
        }

        private void btnDelWakeModel_Click(object sender, EventArgs e)
        {
            // Deletes selected wake loss model and updates plots and tables on Net Turbine Ests.// tab
            if (BW_worker.BackgroundWorker_Map.IsBusy == false && BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false) {
                if (lstWakeModels.SelectedItems.Count == 0) {
                    MessageBox.Show("Select one or more wake models to delete.", "");
                    return;
                }

                DialogResult yes_or_no = MessageBox.Show("Are you sure that you want to delete this wake model?", "", MessageBoxButtons.YesNo);

                if (yes_or_no == DialogResult.Yes) {
                    TurbineCollection.PowerCurve thisPowerCurve = new TurbineCollection.PowerCurve();
                    string powerStr = lstWakeModels.SelectedItems[0].SubItems[0].ToString();

                    for (int i = 0; i < turbineList.PowerCurveCount; i++) {
                        if (turbineList.powerCurves[i].name == powerStr) {
                            thisPowerCurve = turbineList.powerCurves[i];
                            break;
                        }
                    }

                    Wake_Model thisWakeModel = new Wake_Model();
                    if (wakeModelList.NumWakeModels > 0) {
                        thisWakeModel = GetSelectedWakeModel();
                        wakeModelList.RemoveWakeModel(turbineList, mapList, thisWakeModel);
                    }

                    ChangesMade();
                }
                
                updateThe.NetTurbineEstsTAB(this);
                updateThe.MapsTAB(this);
                updateThe.ColoredButtons(this);
            }
            else {
                MessageBox.Show("Cannot delete a map while one is being generated.", "Continuum 2.3");
                return;
            }
        }

        public Wake_Model GetSelectedWakeModel()
        {
            // Returns Wake_Model object based on model selected from list on //Net Turbine Ests.// tab
            int wakeModelType = 0;                    
            Wake_Model thisWakeModel = null;

            if (lstWakeModels.Items.Count == 0)
                return thisWakeModel;

            if (wakeModelList.NumWakeModels == 0)
                return thisWakeModel;

            ListViewItem objListView;

            try {
                objListView = lstWakeModels.SelectedItems[0];
            }
            catch {
                return wakeModelList.wakeModels[0];
            }

            if (objListView.Text == "Eddy Viscosity")
                wakeModelType = 0;
            else if (objListView.Text == "DAWM with EV")
                wakeModelType = 1;
            else if (objListView.Text == "Continuum Wake")
                wakeModelType = 2;

            string combo = objListView.SubItems[1].Text;
            string powerStr = objListView.SubItems[2].Text;
            double WRR = Convert.ToSingle(objListView.SubItems[3].Text);
            double horiz = Convert.ToSingle(objListView.SubItems[4].Text);
            double TI = Convert.ToSingle(objListView.SubItems[5].Text);
            double DW_Spacing = Convert.ToSingle(objListView.SubItems[6].Text);
            double CW_Spacing = Convert.ToSingle(objListView.SubItems[7].Text);
            double roughness = Convert.ToSingle(objListView.SubItems[8].Text);

            thisWakeModel = wakeModelList.GetWakeModel(wakeModelType, horiz, TI, DW_Spacing, CW_Spacing, roughness, powerStr, combo, WRR);

            return thisWakeModel;
        }

        public WakeCollection.WakeGridMap GetSelectedWakeGrid()
        {
            // Returns WakeGridMap object based on model selected from list on //Net Turbine Ests.// tab
                                   
            WakeCollection.WakeGridMap thisWakeGrid = new WakeCollection.WakeGridMap();

            if (lstWakeModels.Items.Count == 0)
                return thisWakeGrid;

            if (wakeModelList.NumWakeGridMaps == 0)
                return thisWakeGrid;

            ListViewItem objListView;

            try {
                objListView = lstWakeModels.SelectedItems[0];
            }
            catch  {
                if (wakeModelList.NumWakeGridMaps != 0) 
                    thisWakeGrid = wakeModelList.wakeGridMaps[0];

                return thisWakeGrid;
            }

            int wakeModelType = 0;
            if (objListView.Text == "Eddy Viscosity")
                wakeModelType = 0;
            else if (objListView.Text == "DAWM with EV")
                wakeModelType = 1;
            else if (objListView.Text == "Continuum Wake")
                wakeModelType = 2;

            string combo = objListView.SubItems[1].Text.ToString();
            string powerStr = objListView.SubItems[2].Text.ToString();
            double WRR = Convert.ToSingle(objListView.SubItems[3].Text);
            double horiz = Convert.ToSingle(objListView.SubItems[4].Text);
            double TI = Convert.ToSingle(objListView.SubItems[5].Text);
            double DW_Spc = Convert.ToSingle(objListView.SubItems[6].Text);
            double CW_Spc = Convert.ToSingle(objListView.SubItems[7].Text);
            double roughness = Convert.ToSingle(objListView.SubItems[8].Text);

            try {
                int minX = Convert.ToInt32(objListView.SubItems[9].Text);
                int minY = Convert.ToInt32(objListView.SubItems[10].Text);
                int maxX = Convert.ToInt32(objListView.SubItems[11].Text);
                int maxY = Convert.ToInt32(objListView.SubItems[12].Text);
                int reso = Convert.ToInt16(objListView.SubItems[13].Text);

                int numX = (maxX - minX) / reso;
                int numY = (maxY - minY) / reso;

                Wake_Model thisWakeModel = wakeModelList.GetWakeModel(wakeModelType, horiz, TI, DW_Spc, CW_Spc, roughness, powerStr, combo, WRR);
                thisWakeGrid = wakeModelList.GetWakeGrid(thisWakeModel, minX, minY, numX, numY, reso);
            }
            catch  {
                return thisWakeGrid;
            }

            return thisWakeGrid;
        }

        private void btnDelWakeGrid_Click(object sender, EventArgs e)
        {
            // Deletes wake loss map and updates associated plots and tables
            WakeCollection.WakeGridMap thisWakeGrid = GetSelectedWakeGrid();

            if (thisWakeGrid.minUTMX == 0) {
                return;
            }
            else {
                mapList.RemoveMapByWakeGridMap(thisWakeGrid, wakeModelList);
                wakeModelList.RemoveWakeGridMap(thisWakeGrid);

                updateThe.NetTurbineEstsTAB(this);
                updateThe.MapsTAB(this);                
                updateThe.ColoredButtons(this);
            }
        }

        private void btnExportNetEsts_Click(object sender, EventArgs e)
            {
            // Calls Export.Export_Net_WS_AEP to export net energy estimates to a CSV file
            
            Export thisExport = new Export();
            thisExport.Export_Net_WS_AEP(this);
        }

        private void chkTurbNetAll_CheckedChanged(object sender, EventArgs e)
        { 
            // Selects/deselects all turbines in list and calls updateThe.NetTurbineEstTab 
            if (Created) {
                if (chkTurbNet.Items.Count > 0) {
                    if (chkTurbNetAll.CheckState == CheckState.Checked) {
                        for (int i = 0; i < chkTurbNet.Items.Count; i++)
                            chkTurbNet.SetItemChecked(i, true);
                    }
                    else {
                        for (int i = 0; i < chkTurbNet.Items.Count; i++)
                            chkTurbNet.SetItemChecked(i, false);
                    }
                }
                                
                updateThe.NetTurbineEstsTAB(this);
            }
        }

        private void chkStringAll_CheckedChanged(object sender, EventArgs e)
    { 
            // Selects/deselects all turbine strings in list and calls updateThe.NetTurbineEstTab
            if (Created) {
                if (chkStrings.Items.Count > 0) {
                    if (chkStringAll.CheckState == CheckState.Checked) {
                        for (int i = 0; i < chkStrings.Items.Count; i++) 
                            chkStrings.SetItemChecked(i, true);
                    }
                    else {
                        for (int i = 0; i < chkStrings.Items.Count; i++)
                            chkStrings.SetItemChecked(i, false);
                    }
                }
                
                updateThe.TurbsByString(this);
            }
        }

        private void btnViewLosses_Click(object sender, EventArgs e)
        {
            // Populates and opens //Other Losses// form
            Wake_Model thisWakeModel = null;
            if (wakeModelList.NumWakeModels > 0) 
                thisWakeModel = GetSelectedWakeModel();
               
            bool isCalibrated = GetSelectedModel("Net");
            double overallWakeLoss = turbineList.GetOverallWakeLoss(thisWakeModel, metList.GetAvgWindRose().Length, isCalibrated);             

            Losses thisLoss = new Losses(this);
            thisLoss.txtWakeLossAll.Text = Math.Round(overallWakeLoss * 100, 2).ToString() + " %";
            thisLoss.txtWakeEffAll.Text = Math.Round((1 - overallWakeLoss) * 100, 2).ToString() + " %";

            thisLoss.txtTurbAvail.Text = Math.Round(otherLosses.Turb_Avail_Loss * 100, 2).ToString() + " %";
            thisLoss.txtBOPAvail.Text = Math.Round(otherLosses.BOP_Avail_Loss * 100, 2).ToString() + " %";

            thisLoss.txtIcingLoss.Text = Math.Round(otherLosses.Icing_Loss * 100, 2).ToString() + " %";
            thisLoss.txtBladeSoilLoss.Text = Math.Round(otherLosses.Blade_Soil_Loss * 100, 2).ToString() + " %";
            thisLoss.txtBladeDegradeLoss.Text = Math.Round(otherLosses.Blade_Degrade_Loss * 100, 2).ToString() + " %";
            thisLoss.txtHighLowTempLoss.Text = Math.Round(otherLosses.HighLowTemp_Loss * 100, 2).ToString() + " %";

            thisLoss.txtElecLoss.Text = Math.Round(otherLosses.Electrical_Loss * 100, 2).ToString() + " %";

            thisLoss.txtPowerCrvLoss.Text = Math.Round(otherLosses.Power_Crv_Loss * 100, 2).ToString() + " %";
            thisLoss.txtTurbulenceLoss.Text = Math.Round(otherLosses.Turbulence_Loss * 100, 2).ToString() + " %";

            thisLoss.txtGrid_Curt.Text = Math.Round(otherLosses.Grid_Curtail_Loss * 100, 2).ToString() + " %";
            thisLoss.txtWakeSM_Loss.Text = Math.Round(otherLosses.Wake_Sect_Curtail_Loss * 100, 2).ToString() + " %";
            thisLoss.txtEnviro_Curt.Text = Math.Round(otherLosses.Environ_Curtail_Loss * 100, 2).ToString() + " %";

            thisLoss.Previous_Other_Losses = otherLosses.Get_Total_Losses();
            thisLoss.ShowDialog();
        }


        private void chkStrings_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates Turb by string plot based on strings selected in list
            if (okToUpdate == true) 
                updateThe.TurbsByString(this);            
        }

        private void chkTurbNet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates Net Turbine Ests tab based on selected turbines in list
            if (okToUpdate == true)  
                updateThe.NetTurbineEstsTAB(this);            
        }

        private void cboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates Net Turbine Ests tab based on selected model
            if (okToUpdate == true)
                updateThe.NetTurbineEstsTAB(this);            
        }

        private void btnCreateWakeMap_Click(object sender, EventArgs e)
        { 
            // Populates and opens GenWakeMap form to create a wake loss map
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot generate a map while other calculations are under way.", "Continuum 2.3");
                return;
            }

            GenWakeMap thisMap = new GenWakeMap(this);

            // if there are existing maps, use the coordinates from the last one created
            int numMaps = wakeModelList.NumWakeGridMaps;

            if (numMaps > 0)
            {
                thisMap.gridReso = wakeModelList.wakeGridMaps[numMaps - 1].wakeGridReso;
                thisMap.minUTMX = wakeModelList.wakeGridMaps[numMaps - 1].minUTMX;
                thisMap.maxUTMX = thisMap.minUTMX + (wakeModelList.wakeGridMaps[numMaps - 1].numX - 1) * wakeModelList.wakeGridMaps[numMaps - 1].wakeGridReso;
                thisMap.minUTMY = wakeModelList.wakeGridMaps[numMaps - 1].minUTMY;
                thisMap.maxUTMY = thisMap.minUTMY + (wakeModelList.wakeGridMaps[numMaps - 1].numY - 1) * wakeModelList.wakeGridMaps[numMaps - 1].wakeGridReso;                
            }
            else
            {
                thisMap.gridReso = 250;
                thisMap.UpdateLimits();
                thisMap.GetCoordsAroundTurbs();
            }

            thisMap.txtMapReso.Text = thisMap.gridReso.ToString();
            thisMap.UpdateLimits();                                
            thisMap.UpdateTextboxes();

            if (wakeModelList.NumWakeModels == 0) {
                MessageBox.Show("Create a wake model first.", "Continuum 2.3");
                return;
            }
            else {
                thisMap.cboWakeModels.Items.Clear();
                for (int i = 0; i <= wakeModelList.NumWakeModels - 1; i++) {
                    string Wake_String = wakeModelList.CreateWakeModelString(wakeModelList.wakeModels[i]);
                    thisMap.cboWakeModels.Items.Add(Wake_String);
                }

                try {
                    thisMap.cboWakeModels.SelectedIndex = 0;
                }
                catch {
                    return;
                }

                thisMap.ShowDialog();
            }
        }

        private void btnRefreshWakeMap_Click(object sender, EventArgs e)
        {
            // Updates the wake loss map to show specified min/max and contour interval            
            updateThe.WakeLossMap(this);
        }

        private void btnImportModel_Click(object sender, EventArgs e)
        {
            // Calls modelList.ImportModelsCSV to read in Continuum model coefficients
            DialogResult okToClear = DialogResult.Yes;

            if (modelList.ModelCount > 4) 
                okToClear = MessageBox.Show("Importing coefficients will delete the site-calibrated coefficients. Do you wish to continue?", "Continuum 2.3", MessageBoxButtons.YesNo);

            if (okToClear == DialogResult.Yes) {
                modelList.ClearImported();
                modelList.ClearAllExceptDefaultAndImported();
                
                turbineList.ClearAllGrossEsts();
                turbineList.ClearAllNetEsts();
                turbineList.ClearAllWSEsts();

                turbineList.turbineCalcsDone = false;

                mapList.ClearAllMaps();

                wakeModelList.ClearAll();

                metPairList.RemoveAllExceptDefault();
            }
            else {
                return;
            }

            modelList.ImportModelsCSV(this);            
            updateThe.AllTABs(this);
        }

        private void chk_Use_Sep_CheckedChanged(object sender, EventArgs e)
        { 
            // Recreates site-calibrated model based on whether the //Use Flow Separation Model// is checked or unchecked
            if (Created && metList.ThisCount > 0 && BW_worker.IsBusy() == false) {
                if ( (chk_Use_Sep.Checked == false && topo.useSepMod == false) ||(chk_Use_Sep.Checked == true && topo.useSepMod == true) ) 
                   return;

                DialogResult yes_or_no = MessageBox.Show("Changing this setting will recreate the wind flow model and estimates. Do you want to continue?", "Continuum 2.3", MessageBoxButtons.YesNo);

                if (yes_or_no == DialogResult.Yes) {

                    modelList.ClearAllExceptDefaultAndImported();
                    metPairList.ClearAll();
                    turbineList.ClearWS_EstCalcs(metList);
                    turbineList.ClearAllGrossEsts();
                    turbineList.ClearAllNetEsts();
                    metPairList.ClearRoundRobin();
                    
                    if (chk_Use_Sep.Checked == true)
                        topo.useSepMod = true;
                    else
                        topo.useSepMod = false;

                    // Call background worker to run calculations
                    BW_worker = new BackgroundWork();
                    BW_worker.Call_BW_MetCalcs(this);
                    ChangesMade();
                }
            }
        }


        private void cboDHplot_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the log-log plots on the Advanced tab based on whether Attached flow or Separated flow is selected from dropdown            
            updateThe.ModelPlots(this);
        }

        private void chkWeight_RMS_CheckedChanged(object sender, EventArgs e)
        { 
            // Recalculates Sectorwise RMS error to either be weighted by the wind rose or not
            if (okToUpdate == true)                 
                updateThe.ModelParams(this);            
        }

        private void btnImportRoughness_Click(object sender, EventArgs e)
        { 
            // Gets UTM zone info (if needed), calls background worker to import a .TIF land cover or .MAP surface roughness file 
            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import land cover data while calculations are under way.", "Continuum 2.3");
                return;
            }
                        
            if (savedParams.savedFileName == null)
            {
                MessageBox.Show("Please specify the file location.", "Continuum 2.3");
                bool wasSaved = false;
                wasSaved = SaveAs();
                if (wasSaved == false)
                {
                    MessageBox.Show("A file location is needed in order to create local database. Cancelling land cover import.", "Continuum 2.3");
                    return;
                }
            }

            if (UTM_conversions.savedDatumIndex == 100) {
                int datumInd = 0;
                int zoneNumber = 0;
                string hemisphere = "";

                Topo_Load_UTM_Datum_Zone topoLoad = new Topo_Load_UTM_Datum_Zone();
                topoLoad.cboNorthOrSouth.SelectedIndex = 0;
                topoLoad.ShowDialog();
                try {
                    datumInd = topoLoad.cbo_Datums.SelectedIndex;
                }
                catch {
                    return;
                }

                try {
                    zoneNumber = Convert.ToInt16(topoLoad.cboUTMZone.SelectedItem.ToString());
                }
                catch {
                    return;
                }

                try {
                    hemisphere = topoLoad.cboNorthOrSouth.SelectedItem.ToString();
                }
                catch {
                    return;
                }

                UTM_conversions.savedDatumIndex = datumInd;
                UTM_conversions.UTMZoneNumber = zoneNumber;
                UTM_conversions.hemisphere = hemisphere;
                txtUTMDatum.Text = UTM_conversions.GetDatumString(datumInd);
                txtUTMZone.Text = UTM_conversions.UTMZoneNumber.ToString() + UTM_conversions.hemisphere.Substring(0,1);

            }

            bool okToLoad = topo.LC_OkToReload(savedParams.savedFileName, this);

            if (okToLoad == false) 
                return;

            Roughness_File_Format thisRough = new Roughness_File_Format();
            thisRough.cboRoughnessFile.SelectedIndex = 0;
            thisRough.ShowDialog();

            int geoTiffOrMap = thisRough.cboRoughnessFile.SelectedIndex;

            modelList.ClearAllExceptDefaultAndImported();
            metPairList.ClearAll();
            turbineList.ClearWS_EstCalcs(metList);
            turbineList.ClearAllGrossEsts();
            turbineList.ClearAllNetEsts();
            metPairList.ClearRoundRobin();
            
            if (geoTiffOrMap == 0) { // GeoTIFF
                if (topo.LC_Key == null) {
                    topo.SetUS_NLCD_Key();

                    LC_Key thisKey = new LC_Key(this);                                        

                    Populate_LC_Key_Form(thisKey);
                    updateThe.LC_KeySelected(this);

                    thisKey.ShowDialog();
                }

                if (ofdLandCover.ShowDialog() == DialogResult.OK) {

                    string wholePath = ofdLandCover.FileName;
                    SetDefaultFolderLocations(wholePath);

                    BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
                    Vars_for_BW.thisInst = this;
                    Vars_for_BW.Filename = wholePath;
                    BW_worker = new BackgroundWork();
                    BW_worker.Call_BW_LandCoverImport(Vars_for_BW);
                    chkUseSR.Checked = true;

                }
            }
            else if (geoTiffOrMap == 1) { // roughness map (.MAP or .SHP file)
                // Calls Background Worker to read in roughness .map file
                if (ofdImportMap.ShowDialog() == DialogResult.OK) {

                    string wholePath = ofdImportMap.FileName;
                    SetDefaultFolderLocations(wholePath);

                    BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
                    Vars_for_BW.thisInst = this;
                    Vars_for_BW.Filename = wholePath;
                    BW_worker = new BackgroundWork();
                    BW_worker.Call_BW_WAsP_Map(Vars_for_BW);
                    chkUseSR.Checked = true;
                }

            }
        }

        private void cboWS_or_WD_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates //Gross Turbine Ests.// tab to show either WS distribution or wind rose
            if (okToUpdate == true) {
                Update updateThe = new Update();
                updateThe.WS_or_WR_Plot(this);
            }
        }

        private void btnExportNetWSDists_Click(object sender, EventArgs e)
        {
            // Calls Export.ExportNetWS_Dists to export net wind speed distributions of selected model and wind direction sector (why is WD needed??)
            Export thisExport = new Export();
            thisExport.ExportNetWS_Dists(this);
        }

        private void btnExportNetDirWSDists_Click(object sender, EventArgs e)
        {
            // Calls Export.ExportNetDirectionalWS_Dists to export net wind speed distributions of selected model and wind direction sector (why is WD needed??)
            Export thisExport = new Export();
            thisExport.ExportNetDirectionalWS_Dists(this);
        }

        private void btnExportNetDirWS_Click(object sender, EventArgs e)
        {
            // Calls Export.Export_Directional_WS to export net estimated directional wind speeds
            Export thisExport = new Export();
            thisExport.Export_Directional_WS(this, "Net");
        }

        private void btnExportTopo_Click(object sender, EventArgs e)
        {
            Export ThisExport = new Export();
            ThisExport.ExportTopo(this);
        }

        public Met[] GetCheckedMets(string tabName) // Returns list of mets checked on any of the tabs (tabName can be "Input", "Summary", "Gross", "Map", "Advanced")
        {
            Met[] checkedMets = new Met[0];
            int metCount;
            CheckedListBox thisMetList = null;

            try
            {
                if (tabName == "Input")
                    thisMetList = chkMetLabels;
                else if (tabName == "Summary")
                    thisMetList = chkMetSumm;
                else if (tabName == "Gross")
                    thisMetList = chkMetGross;
                else if (tabName == "Map")
                    thisMetList = chkMetLabels_Maps;
                else if (tabName == "Advanced")
                    thisMetList = chkMetlabelsStep;

                metCount = thisMetList.CheckedItems.Count;

                if (metCount == 0) return checkedMets;

            }
            catch 
            {
                return checkedMets;
            }

            checkedMets = new Met[metCount];
            int metInd = 0;

            for (int i = 0; i < metList.ThisCount; i++)
            {
                for (int j = 0; j < metCount; j++)
                {
                    if (metList.metItem[i].name == thisMetList.CheckedItems[j].ToString())
                    {
                        checkedMets[metInd] = metList.metItem[i];
                        metInd++;
                        break;
                    }
                }
            }

            return checkedMets;
        }

        public Turbine[] GetCheckedTurbs(string tabName) // Returns list of turbines checked on any of the tabs (tabName can be "Input", "Summary", "Gross", "Net", "Map", "Advanced")
        {
            Turbine[] checkedTurbines = new Turbine[0];
            int turbCount = 0;
            CheckedListBox thisTurbList = null;

            try
            {
                if (tabName == "Input")
                    thisTurbList = chkTurbLabels;
                else if (tabName == "Summary")
                    thisTurbList = chkTurbSumm;
                else if (tabName == "Gross")
                    thisTurbList = chkTurbGross;
                else if (tabName == "Net")
                    thisTurbList = chkTurbNet;
                else if (tabName == "Map")
                    thisTurbList = chkTurbLabels_Maps;
                else if (tabName == "Advanced")
                    thisTurbList = chkTurbLabelStep;

                turbCount = thisTurbList.CheckedItems.Count;
                if (turbCount == 0) return checkedTurbines;
            }
            catch
            {
                return checkedTurbines;
            }

            checkedTurbines = new Turbine[turbCount];
            int turbInd = 0;

            for (int i = 0; i < turbineList.TurbineCount; i++)
            {
                for (int j = 0; j < turbCount; j++)
                {
                    if (turbineList.turbineEsts[i].name == thisTurbList.CheckedItems[j].ToString())
                    {
                        checkedTurbines[turbInd] = turbineList.turbineEsts[i];
                        turbInd++;
                        break;
                    }
                }
            }

            return checkedTurbines;
        }

        private void btnExportLC_Click(object sender, EventArgs e)
        {
            Export ThisExport = new Export();
            ThisExport.Export_LC(this);
        }

        private void lstMaps_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            updateThe.Generated2DMap(this);
            updateThe.MapStats(this);
        }

        private void lstWakeModels_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            updateThe.WakeLossMap(this);
            updateThe.WakedTurbList(this);
            updateThe.WakedWSDistPlot(this);
            updateThe.LossTextboxes(this);
        }

        public string GetEndSiteAdvanced() // Returns name of met site selected as //End Met// on //Advanced// tab
        {
            string endStr = "";
            try
            {
                endStr = cboEndMet.SelectedItem.ToString();
            }
            catch { }

            return endStr;
        }

        public int GetNumWD()
        {
            // Returns number of wind direction sectors based on wind rose length
            int numWD = 0;

            if (metList.ThisCount > 0)
                numWD = metList.metItem[0].windRose.Length;

            return numWD;
        }


        public bool GetSelectedModel(string TABname) // returns selected isCalibrated (false = default, true = site-calibrated) for TABs: Gross, Net, Uncertainty, and Advanced
        {
            bool isCalibrated = false;
            int modelInd = 0;

            if (TABname == "Gross")
            {
                try
                {
                    modelInd = cboGrossModel.SelectedIndex;
                }
                catch { }
            }
            else if (TABname == "Net")
            {
                try
                {
                    modelInd = cboNetModel.SelectedIndex;
                }
                catch { }
            }
            else if (TABname == "Uncertainty")
            {
                try
                {
                    modelInd = cboUncertModel.SelectedIndex;
                }
                catch { }
            }
            else if (TABname == "Advanced")
            {
                try
                {
                    modelInd = cboAdvancedModel.SelectedIndex;
                }
                catch { }
            }

            if (modelInd == 1)
                isCalibrated = true;

            return isCalibrated;
        }


        public int GetRadiusInd(string TAB_name) // Returns selected radius index (Summary, Advanced) 
        {
            int radiusInd = 0;
            if (TAB_name == "Summary")
            {
                try
                {
                    radiusInd = cboMetSum_Rad.SelectedIndex;
                }
                catch { }
            }
            else if (TAB_name == "Advanced")
            {
                try
                {
                    radiusInd = cboAdvancedRad.SelectedIndex;
                }
                catch { }
            }

            if (radiusInd == -1)
                radiusInd = 0;

            return radiusInd;
        }

        public string GetStartMetAdvanced() // Returns name of met site selected as //Start Met// on //Advanced// tab
        {
            string met1Str = "";
            try
            {
                met1Str = cboStartMet.SelectedItem.ToString();
            }
            catch { }

            return met1Str;
        }
        public int GetWD_ind(string TAB_name) // Returns WD sector selected on Met & Turbine Summary tab (To Do: combine with //Get_WD_sector_Gross_Turb)
        {
            int WD_Ind = 0;

            if (metList.ThisCount == 0) return WD_Ind;

            if (TAB_name == "Summary")
            {
                try
                {
                    WD_Ind = cboSummaryWD.SelectedIndex;
                }
                catch { }
            }
            else if (TAB_name == "Gross")
            {
                try
                {
                    WD_Ind = cboGrossWD.SelectedIndex;
                }
                catch { }
            }
            else if (TAB_name == "Net")
            {
                try
                {
                    WD_Ind = cboNetWD.SelectedIndex;
                }
                catch { }
            }
            else if (TAB_name == "Maps")
            {
                try
                {
                    WD_Ind = cboMapWD.SelectedIndex;
                }
                catch { }
            }
            else if (TAB_name == "Advanced")
            {
                try
                {
                    WD_Ind = cboAdvancedWD.SelectedIndex;
                }
                catch { }
            }

            return WD_Ind;
        }

        public Model[] GetModels(Continuum thisInst, string tabName)
        {
            // Returns UWDW models for specified rad_ind. if ( radiusInd = Num  of Radii (4) then return all four models otherwise returns the one specified by radius

            bool isCalibrated = GetSelectedModel(tabName);
            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(), isCalibrated);

            return models;

        }

        
    }
}
