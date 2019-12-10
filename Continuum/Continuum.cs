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
        public Saved_Parameters savedParams = new Saved_Parameters();  // Saved map display settings, topo data filename, saved filename, path name, land cover filename
        public MetPairCollection metPairList = new MetPairCollection();  // List of MetPair objects
        public ModelCollection modelList = new ModelCollection();  // List of Continuum models (i.e. coefficients, RMS errors, etc)
        public TurbineCollection turbineList = new TurbineCollection(); // List of Turbine objects
        public WakeCollection wakeModelList = new WakeCollection(); // List of wake models        
        public UTM_conversion UTM_conversions = new UTM_conversion(); // UTM datum and zone info
        public BackgroundWork BW_worker = new BackgroundWork(); // Background worker where longer calculations are run on a separate thread
        public Update updateThe = new Update();
        public double modeledHeight = 80; // Extrapolation height and modeled height. Default 80 m
        public MERRACollection merraList = new MERRACollection(); // List of MERRA data for long-term reference calcs     
        public SiteSuitability siteSuitability = new SiteSuitability(); // Holds results of ice throw, shadow flicker, and sound model
                
        public bool fileChanged;
        public bool okToUpdate = true; // Used to determine when the GUI plots and tables should be updated
        public bool isTest = false;
                

        public Continuum()
        {
            SplashScreen Splash = new SplashScreen();
            Splash.ShowDialog();                        

            InitializeComponent();      

            radiiList.New(); // populates with R = 4000, 6000, 8000, 10000 and invserse distance exponent = 1
            metList.NewList(); // sets MCP settings and day/night hours
            turbineList.SetExceedCurves(); // initializes Exceedance curves
            updateThe.Exceedance_TAB(this);
            
            chkSelectedTurbineParam.Items.Add("Avg WS", true);
            chkSelectedTurbineParam.Items.Add("Gross AEP", false);
            chkSelectedTurbineParam.Items.Add("Net AEP", false);
            chkSelectedTurbineParam.Items.Add("Wake Loss", false);
            chkSelectedTurbineParam.Items.Add("% Diff", false);

            txtNumIceDays.Text = siteSuitability.numIceDaysPerYear.ToString();
            txtNumIceThrowsPerDay.Text = siteSuitability.iceThrowsPerIceDay.ToString();
            
            cboTI_Type.SelectedIndex = 0;
            cboEffectiveTI_m.SelectedIndex = 0;
            cboMERRASelectedMet.SelectedIndex = 0;
            cboMCP_Type.SelectedIndex = 0;
            updateThe.MCP_Settings(this);
                        
            
        }               

        private void btnLoadXYZ_Click(object sender, EventArgs e)
        {
            // If file is not saved, prompts user to save, then calls LoadTopo
            // Can't load new topo data if there are calcs going on
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import topo data while calculations are under way.", "Continuum 3");
                return;
            }

            bool wasSaved = false;
            if (savedParams.savedFileName == null)
            {
                MessageBox.Show("Please specify the file location.", "Continuum 3");
                wasSaved = SaveAs();
                if (wasSaved == false)
                {
                    MessageBox.Show("A file location is needed in order to create local database. Cancelling topography import.", "Continuum 3");
                    return;
                }
            }                        

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
            ofdZones.InitialDirectory = savedParams.pathName;
            ofdMetData.InitialDirectory = savedParams.pathName;
            ofdExceedCurves.InitialDirectory = savedParams.pathName;

            sfdCFMfile.InitialDirectory = savedParams.pathName;
            sfd60mWS.InitialDirectory = savedParams.pathName;
            sfdExpos.InitialDirectory = savedParams.pathName;
            sfdWRG.InitialDirectory = savedParams.pathName;
            sfdrsf.InitialDirectory = savedParams.pathName;
        }

        public void LoadTopo()
        {
            // Prompts user to find open topo data file and then calls Call_BW_TopoImport function (calls background worker)

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
                        SaveFile(false);
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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot delete met data while calculations are under way.", "Continuum 3");
                return;
            }

            int numToDelete = lstMetTowers.SelectedItems.Count;
            string[] metNames = new string[numToDelete];  

            if (numToDelete == 0) {
                MessageBox.Show("Select one or more met sites to delete by clicking met names.", "Continuum 3");
                return;
            }

            DialogResult confirm = DialogResult.Yes;
            if (numToDelete == 1)
                confirm = MessageBox.Show("Are you sure that you want to delete this met tower?", "Continuum 3", MessageBoxButtons.YesNo);
            else
                confirm = MessageBox.Show("Are you sure that you want to delete these " + numToDelete + " met towers?", "Continuum 3", MessageBoxButtons.YesNo);
            
            if (confirm == DialogResult.Yes)
            {
                // check if models and turbine parameters have already been calculated
                
                DeleteMet(false);

                SaveFile(false);                
                updateThe.AllTABs(this);
            }
        }

        public void DeleteMet(bool isTest)
        {
            // Deletes all selected mets, clears all calculations that used mets, calls background worker to perform met calcs
            int metCount = metList.ThisCount;
            int numToDelete = lstMetTowers.SelectedItems.Count;

            string[] metNames = new string[numToDelete];

            for (int i = 0; i < numToDelete; i++)
                metNames[i] = lstMetTowers.SelectedItems[i].Text;           
                        
            for (int i = 0; i < numToDelete; i++) { 
                for (int j = 0; j < metCount; j++) { 
                    if (metList.metItem[j].name == metNames[i]) {
                        UTM_conversion.Lat_Long theseLL = UTM_conversions.UTMtoLL(metList.metItem[j].UTMX, metList.metItem[j].UTMY);
                        metList.Delete(metNames[i], this, theseLL.latitude, theseLL.longitude);
                        ChangesMade();
                        break;
                    }
                }
            }                       
                        
            modelList.ClearAllExceptImported();            
            mapList.DeleteMapsUsingDeletedMets(this, metNames);                      

            turbineList.ClearAllWSEsts(); // clears all WS estimates
            turbineList.ClearAllGrossEsts();
            turbineList.ClearAllNetEsts();

            metPairList.ClearAll();                          

            if (metList.ThisCount == 0)
                metList.expoIsCalc = false;

            if (metList.isTimeSeries == false)
                ChangesMade();
            else
                SaveFile(isTest);

            updateThe.AllTABs(this);

        }

        public bool ImportMetsTAB()
        {
            // Clears calculations generated from the site calibration using previous set of met sites (UWDW models, Met Pair lists, turbine wind speed and energy estimates
            // and Round robin calculations. Reads in one or many met TAB files and adds to metList. Returns true if TAB files imported successfully.
                        
            modelList.ClearAllExceptImported();
            metPairList.ClearAll();                
            turbineList.ClearAllWSEsts();
            turbineList.ClearAllGrossEsts();
            turbineList.ClearAllNetEsts();                
            mapList.ClearAllWakedMaps();                                            
            updateThe.ClearStats(this);
            metList.isTimeSeries = false;
            metList.isMCPd = false;
            metList.numSeason = 1;
            metList.numTOD = 1;

            string wholePath = ofdMets.FileName;
            SetDefaultFolderLocations(wholePath);

            int numFiles = ofdMets.FileNames.Length;

            bool showMsg = true;
            int lastH = 0;

            for (int n = 0; n < numFiles; n++)
            {                    
                int height = 0;
                string metName = "";
                    
                //  double[] sectorWS_Ratios = null;
                //  double[] metWindRose = null;
                //   double[,] WS_Dist = new double[0,0];
                double WS_FirstInt = 0;
                double WS_IntSize = 0;
                int WR_size = 0;
                int WD_offset = 0;
                Met.WSWD_Dist thisDist = new Met.WSWD_Dist();

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
                    MessageBox.Show("Error reading in met latitude, longitude and height for file: " + ofdMets.FileName);
                    sr.Close();
                    return false;
                }
                                        
                double latitude = Convert.ToSingle(fileRow[0]);
                double longitude = Convert.ToSingle(fileRow[1]);

                if (latitude > 100)
                {
                    MessageBox.Show("Invalid Latitude in TAB file : " + Math.Round(latitude, 3).ToString());
                    sr.Close();
                    return false;
                }

                if (longitude > 200)
                {
                    MessageBox.Show("Invalid Longitude in TAB file : " + Math.Round(longitude, 3).ToString());
                    sr.Close();
                    return false;
                }
                        
                // Convert to UTM
                UTM_conversion.UTM_coords theseUTM = new UTM_conversion.UTM_coords();

                if (UTM_conversions.savedDatumIndex == 100)
                {
                    UTM_datum thisDatum = new UTM_datum();
                    thisDatum.ShowDialog();
                    UTM_conversions.savedDatumIndex = thisDatum.cbo_Datums.SelectedIndex;
                    UTM_conversions.hemisphere = thisDatum.cboNorthOrSouth.SelectedItem.ToString();
                }

                theseUTM = UTM_conversions.LLtoUTM(latitude, longitude);
                double UTMX = theseUTM.UTMEasting;
                double UTMY = theseUTM.UTMNorthing;
                UTM_conversions.UTMZoneNumber = theseUTM.UTMZoneNumber;
                txtUTMDatum.Text = UTM_conversions.GetDatumString(UTM_conversions.savedDatumIndex);
                txtUTMZone.Text = UTM_conversions.UTMZoneNumber.ToString() + UTM_conversions.hemisphere.Substring(0,1);
                    
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
                    MessageBox.Show("Error reading the TAB files. All TAB files must represent the same height.", "Continuum 3");
                    return false;
                }
                modeledHeight = height;
                txtModeledHeight.Text = height.ToString();

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
                            MessageBox.Show("Error reading TAB file.  Number of sectors can only be 12, 16 or 24.  Read in :" + WR_size + ".  check your TAB file", "Continuum 3");
                            return false;
                        }
                    }
                    catch 
                    {
                        MessageBox.Show("Error reading WR size.", "Continuum 3");
                        return false;
                    }

                    try
                    {
                        WS_IntSize = Convert.ToSingle(fileRow[1]);
                        //   if ( WS_IntSize != 1 ) {
                        // MessageBox.Show("Error reading TAB file.  WS interval size must be 1.0 m/s.  Read in :" + WS_IntSize + ".  check your TAB file", , "Continuum 3")
                        // return;
                    }
                    catch 
                    {
                        MessageBox.Show("Error reading WS interval size.", "Continuum 3");
                        return false;
                    }

                    try
                    {
                        WD_offset = (int)Convert.ToSingle(fileRow[2]);
                        if (WD_offset != 0)
                        {
                            MessageBox.Show("Error reading TAB file.  WD offset size must be 0.  Read in :" + WD_offset + ".  check your TAB file", "Continuum 3");
                            return false;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error reading WD offset.", "Continuum 3");
                        return false;
                    }
                }

                if (WR_size == 0 || WS_IntSize == 0)
                {
                    MessageBox.Show("Error reading WS bin size.", "Continuum 3");
                    return false;
                }

                if (WD_offset != 0)
                {
                    MessageBox.Show("Sorry the WD offset is not supported in this version of Continuum.  You must create a TAB file with no wind direction offset.");
                    sr.Close();
                    return false;
                }

                // If this is first met read in, assign metList.numWD otherwise compare WR_size to metList.numWD
                if (metList.ThisCount == 0)
                    metList.numWD = WR_size;
                else if (metList.numWD != WR_size)
                { 
                    MessageBox.Show("A " + metList.numWD + "-sector wind rose has been imported for other mets but this met has " + WR_size + " sectors. check your input file.", "Continuum 3");
                    sr.Close();
                    return false;
                }
                
                thisDist.windRose = new double[WR_size];
                thisDist.sectorWS_Ratio = new double[WR_size];
                thisDist.sectorWS_Dist = new double[0, 0];

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
                    thisDist.windRose[i] = Convert.ToSingle(fileRow[i]) / 100;

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
                        MessageBox.Show("A " + WR_size + "-sector wind rose has been imported but read in " + (fileRow.Length - 1).ToString() + ". check your input file.", "Continuum 3");
                        sr.Close();
                        return false;
                    }

                    ResizeArray(ref thisDist.sectorWS_Dist, WR_size, WS_Int);

                    for (int i = 0; i <= fileRow.Length - 2; i++)
                        thisDist.sectorWS_Dist[i, WS_Int - 1] = Convert.ToSingle(fileRow[i + 1]);

                    WS_Int++;
                }

                // If this is first met read in, assign metList.WS_IntSize otherwise compare WS_IntSize to metList.numWD
                if (metList.ThisCount == 0)
                    metList.WS_IntSize = WS_IntSize;
                else if (metList.WS_IntSize != WS_IntSize)
                {
                    MessageBox.Show("A different WS interval size of " + metList.WS_IntSize + " was used for other mets.  The WS intervals must be consistent for all met sites.");

                    sr.Close();
                    return false;
                }

                // If this is first met read in, assign metList.WS_FirstInt otherwise compare WS_FirstInt to metList.numWD
                if (metList.ThisCount == 0)
                    metList.WS_FirstInt = WS_FirstInt;
                else if (metList.WS_FirstInt != WS_FirstInt)
                {
                    MessageBox.Show("A different first WS bin of " + metList.WS_FirstInt + " was used for other mets.  The WS intervals must be consistent for all met sites.");
                    sr.Close();
                    return false;
                }

                sr.Close();

                // check TAB file
                Check_class check = new Check_class();
                inputMet = check.NewTAB(thisDist.sectorWS_Dist, WS_FirstInt, WS_IntSize, thisDist.windRose, metName, this); // checks that sector WS dists add to 1000, wind rose adds to 100 and that WS dist intervals line up with other mets or power curves

                if (inputMet == false)
                    return false;

                // Divide Sector WS dists by 1000
                for (int i = 0; i < WR_size; i++)
                    for (int j = 0; j < WS_Int - 1; j++)
                        thisDist.sectorWS_Dist[i, j] = thisDist.sectorWS_Dist[i, j] / 1000;

                inputMet = check.CheckMetName(metName, turbineList);

                if (inputMet == false)
                    return false;

                inputMet = check.NewTurbOrMet(this.topo, metName, UTMX, UTMY, showMsg); // checks the distance between met and topo grid edges to make sure that there//s enough distance for expo calcs
                                                                           
                if (inputMet == true)
                    metList.AddMetTAB(metName, UTMX, UTMY, height, thisDist.windRose, thisDist.sectorWS_Dist, WS_FirstInt, WS_IntSize, this);

            }

            metList.MakeAllSameLength();
            ChangesMade();
            return true;
                      

        }

        public string[] TrimBlanks(string[] Array_with_Blanks)
        {
            // Reads in array of strings, trims all blank strings and returns array of non-blank strings
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
            // Resizes two-dimensional array while keeping data in original array
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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import turbine sites while calculations are under way.", "Continuum 3");
                return;
            }

            if (wakeModelList.NumWakeModels > 0) {
                DialogResult goodToGo = MessageBox.Show("Importing additional turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    wakeModelList.ClearAll();
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
                else                
                    return;
            }

            if (ofdTurbines.ShowDialog() != DialogResult.OK)
                return;

            LoadTurbines(ofdTurbines.FileName);
            ChangesMade();
            topo.GetElevsAndSRDH_ForCalcs(this, null, false);

            // Define exceedance curves if doesn't exist yet
            if (turbineList.exceed == null)
            {
                turbineList.exceed = new Exceedance();
                turbineList.exceed.compositeLoss.isComplete = false;
                turbineList.exceed.CreateDefaultCurve();
            }       
             
            updateThe.AllTABs(this);
                                        
        }

        public void LoadTurbines(string fileName)
        {
            // Open .csv or .txt file with turbine coordinates, reads in each turbine site and adds to list of Turbines 
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import turbine sites while calculations are under way.", "Continuum 3");
                return;
            }
            
            SetDefaultFolderLocations(fileName);
                                
            string[] fileRow;
            int numTurbines = 0;
            string turbineName = "";
            double latitude = 0;
            double longitude = 0;
            bool inputTurbine = false;
            int thisString = 0;
            StreamReader sr;
            try
            {
                sr = new StreamReader(fileName);
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

                if (fileRow.Length >= 3) { // to allow turbine names to have spaces, read last two columns to find Lat/Long

                    try {                            
                        latitude = Convert.ToDouble(fileRow[fileRow.Length - 2]);
                    }
                    catch  {
                        MessageBox.Show("Error reading the turbine input file.  The format should be Name, Easting, Northing.", "Continuum 3");
                        sr.Close();
                        return;
                    }

                    try {                            
                        longitude = Convert.ToDouble(fileRow[fileRow.Length - 1]);
                    }
                    catch  {
                        MessageBox.Show("Error reading the turbine input file.  The format should be Name, Easting, Northing.", "Continuum 3");
                        sr.Close();
                        return;
                    }

                    turbineName = fileRow[0];

                    for (int i = 1; i <= fileRow.Length - 3; i++)
                        turbineName = turbineName + " " + fileRow[i];

                    if (latitude > 100)
                    {
                        MessageBox.Show("Invalid Latitude in TAB file : " + latitude.ToString());
                        sr.Close();
                        return;
                    }

                    if (longitude > 200)
                    {
                        MessageBox.Show("Invalid Longitude in TAB file : " + longitude.ToString());
                        sr.Close();
                        return;
                    }
                                                
                    if (UTM_conversions.savedDatumIndex == 100) {
                        UTM_datum thisDatum = new UTM_datum();
                        thisDatum.ShowDialog();
                        UTM_conversions.savedDatumIndex = thisDatum.cbo_Datums.SelectedIndex;
                        UTM_conversions.hemisphere = thisDatum.cboNorthOrSouth.SelectedItem.ToString();
                    }

                    UTM_conversion.UTM_coords theseUTM = UTM_conversions.LLtoUTM(latitude, longitude);
                    double UTMX = theseUTM.UTMEasting;
                    double UTMY = theseUTM.UTMNorthing;
                    txtUTMDatum.Text = UTM_conversions.GetDatumString(UTM_conversions.savedDatumIndex);
                    txtUTMZone.Text = UTM_conversions.UTMZoneNumber.ToString() + UTM_conversions.hemisphere.Substring(0,1);                                              
                                                
                    if (turbineName == "") {
                        MessageBox.Show("Couldn't read in a turbine name at line: " + numTurbines, "Continuum 3");
                        sr.Close();
                        return;
                    }

                    if (UTMX == 0) {
                        MessageBox.Show("Couldn't read in an easting at line: " + numTurbines, "Continuum 3");
                        sr.Close();
                        return;
                    }

                    if (UTMY == 0) {
                        MessageBox.Show("Couldn't read in a northing at line: " + numTurbines, "Continuum 3");
                        sr.Close();
                        return;
                    }

                    Check_class check = new Check_class();
                    inputTurbine = check.NewTurbOrMet(topo, turbineName, UTMX, UTMY, showMsg);
                    if (inputTurbine == true)
                        inputTurbine = check.CheckTurbName(turbineName, metList);
                    else
                        showMsg = false;

                    if (inputTurbine == true) turbineList.AddTurbine(turbineName, UTMX, UTMY, thisString);
                    numTurbines++;
                }
                else {
                    MessageBox.Show("Error reading in the file at line: " + numTurbines + "The format should be Name, Easting, Northing.  " +
                        "Make sure that there are no spaces in your turbine names", "Continuum 3");
                    sr.Close();
                    return;
                }

                turbineName = "";
                    
            }
            sr.Close();
                                    
        updateThe.ColoredButtons(this);
        }

        private void btnAddTurb_Click(object sender, EventArgs e)
        {
            // Opens NewTurbine dialog box for user to manually enter a new turbine site
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot add turbine site while calculations are under way.", "Continuum 3");
                return;
            }

            if (wakeModelList.NumWakeModels > 0 ) {
                DialogResult goodToGo = MessageBox.Show("Importing additional turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    wakeModelList.ClearAll();
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
                else                
                    return;               

            }

            // Define exceedance curves if doesn't exist yet
            if (turbineList.exceed == null)
            {
                turbineList.exceed = new Exceedance();
                turbineList.exceed.compositeLoss.isComplete = false;
                turbineList.exceed.CreateDefaultCurve();
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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot edit a turbine site while calculations are under way.", "Continuum 3");
                return;
            }

            if (lstTurbines.SelectedItems.Count == 0) {
                MessageBox.Show("Select a turbine to edit by clicking a turbine name.", "Continuum 3");
                return;
            }

            if (wakeModelList.NumWakeModels > 0) {

                DialogResult goodToGo = MessageBox.Show("Editing turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    wakeModelList.ClearAll();
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
                else                
                    return;
            }

            // Define exceedance curves if doesn't exist yet
            if (turbineList.exceed == null)
            {
                turbineList.exceed = new Exceedance();
                turbineList.exceed.compositeLoss.isComplete = false;
                turbineList.exceed.CreateDefaultCurve();
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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot delete turbine sites while calculations are under way.", "Continuum 3");
                return;
            }                        

            if (lstTurbines.SelectedItems.Count == 0) {
                MessageBox.Show("Select at least one turbine to delete by clicking on a turbine name", "Continuum 3"); 
                return;
            }

            DialogResult goodToGo;
            // Clear all net estimates (different set of turbines)
            if (wakeModelList.NumWakeModels > 0) {
                goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    wakeModelList.ClearAll();
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                    siteSuitability.ClearAll();
                }
                else                
                    return;         

            }
                        
            string turbineName;
            if (lstTurbines.SelectedItems.Count == 1) {

                turbineName = lstTurbines.SelectedItems[0].Text;
                goodToGo = MessageBox.Show("Are you sure you want to delete turbine: " + turbineName + "?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes) {
                    turbineList.DeleteTurbine(turbineName);
                    ChangesMade();
                }
            }
            else {
                int numTurbines = lstTurbines.SelectedItems.Count;
                goodToGo = MessageBox.Show("Are you sure you want to delete these " + numTurbines + " turbines?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes) {
                    for (int i = 0; i < numTurbines; i++) {
                        turbineName = lstTurbines.SelectedItems[i].Text;
                        turbineList.DeleteTurbine(turbineName);
                    }
                    ChangesMade();
                }
            }

            siteSuitability.ClearAll();
            updateThe.AllTABs(this);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Calls SaveAs()
            SaveAs();
        }

        public bool SaveAs()
        {
            // Prompts user for folder and saves .CFM file. Returns true if saved successfully.

            DialogResult goodToGo = DialogResult.Yes;
            if (metList.isTimeSeries && turbineList.TurbineCount > 0)
            {
                if (turbineList.turbineEsts[0].AvgWSEst_Count > 0)
                    if (turbineList.turbineEsts[0].avgWS_Est[0].timeSeries != null)
                        if (turbineList.turbineEsts[0].avgWS_Est[0].timeSeries.Length != 0)
                            goodToGo = MessageBox.Show("Hourly turbine estimates will be cleared when saving the CFM file. Do you want to continue with save?", "Continuum 3", MessageBoxButtons.YesNo);
            }

            if (goodToGo == DialogResult.No)
                return false;
                      
            // Get map settings            
            updateThe.GetMapSettings(this);

            if (sfdCFMfile.ShowDialog() == DialogResult.OK) {
                string wholePath = sfdCFMfile.FileName;
                SetDefaultFolderLocations(wholePath);

                // Get all met data from DB
                if (metList.isTimeSeries)
                    for (int i = 0; i < metList.ThisCount; i++)
                        metList.metItem[i].metData.GetSensorDataFromDB(this, metList.metItem[i].name);

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

                topo.elevsForCalcs = null;
                topo.DH_ForCalcs = null;
                topo.SR_ForCalcs = null;

                // Save all anem, vane, and temperature data to database and clear from metItem            
                metList.AddAllMetDataToDBAndClear(this);

                // Clear all MCP reference, target, and concurrent data
                metList.ClearMCPRefTargetConcLTEstData();

                // Clear all MERRA interp data and node data
                merraList.ClearMERRA_Data(this);

                // Clear turbine time series data
                turbineList.ClearTimeSeries();

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
                bin.Serialize(fStream, modeledHeight);
                bin.Serialize(fStream, merraList);
                bin.Serialize(fStream, siteSuitability);

                fStream.Close();
                
                Text = savedParams.savedFileName;

                fileChanged = false;
                saveToolStripMenuItem.Enabled = true;

                // Get met sensor data from DB
                if (metList.isTimeSeries)
                    for (int i = 0; i < metList.ThisCount; i++)
                        metList.metItem[i].metData.GetSensorDataFromDB(this, metList.metItem[i].name);
            }

            bool wasSaved = false;
            if (sfdCFMfile.FileName != "")
                wasSaved = true;

            return wasSaved;
        }

        public void SaveFile(bool isTest)
        {
            // Saves file using stored filename and path
            
            // Get map settings            
            updateThe.GetMapSettings(this);

            if (savedParams.savedFileName == "" || savedParams.savedFileName == null) 
                return;

            DialogResult goodToGo = DialogResult.Yes; 
            if (metList.isTimeSeries && turbineList.TurbineCount > 0 && isTest == false)
            {
                if (turbineList.turbineEsts[0].AvgWSEst_Count > 0)
                {
                    if (turbineList.turbineEsts[0].avgWS_Est[0].timeSeries != null)
                        if (turbineList.turbineEsts[0].avgWS_Est[0].timeSeries.Length != 0)
                            goodToGo = MessageBox.Show("Hourly turbine estimates will be cleared when saving the CFM file. Do you want to continue with save?", "Continuum 3", MessageBoxButtons.YesNo);
                }

            }

            if (goodToGo == DialogResult.No)
                return;

            FileStream fStream = new FileStream(savedParams.savedFileName, FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            topo.elevsForCalcs = null;
            topo.DH_ForCalcs = null;
            topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            metList.AddAllMetDataToDBAndClear(this);

            // Clear all MCP reference, target, concurrent, and LT estimate data
            metList.ClearMCPRefTargetConcLTEstData();
            
            // Clear all MERRA interp data and MERRA node data
            merraList.ClearMERRA_Data(this);

            // Clear turbine time series data
            turbineList.ClearTimeSeries();

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
            bin.Serialize(fStream, modeledHeight);
            bin.Serialize(fStream, merraList);
            bin.Serialize(fStream, siteSuitability);

            fStream.Close();
            fileChanged = false;
            Text = savedParams.savedFileName;

            // Get met sensor data from DB
            if (metList.isTimeSeries)
                for (int i = 0; i < metList.ThisCount; i++)
                    metList.metItem[i].metData.GetSensorDataFromDB(this, metList.metItem[i].name);

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
                        SaveFile(false);                    
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
                    MessageBox.Show("Error loading. Files created in previous versions of Continuum (prior to v2.2) cannot be opened and will need to be recreated.", "Continuum 3");
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

        //    if (metList.ThisCount > 0) 
        //        updateThe.WindRose(this);

            okToUpdate = false;
            if (metList.filteringEnabled)
                chkDisableFilter.CheckState = CheckState.Checked;
            else
                chkDisableFilter.CheckState = CheckState.Unchecked;
            okToUpdate = true;

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

            savedParams.savedFileName = Filename;

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
                MessageBox.Show("Error reading in UTM zone settings in Continuum file.", "Continuum 3");
                return;
            }

            try {
                wakeModelList = (WakeCollection)bin.Deserialize(fstream);
                wakeModelList.CleanUpWakeModelsAndGrid();
            }
            catch  {
                MessageBox.Show("Error reading in wake models in Continuum file.", "Continuum 3");
                return;
            }                        

            try
            {
                modeledHeight = (double)bin.Deserialize(fstream);
                txtModeledHeight.Text = Math.Round(modeledHeight, 0).ToString();
            }
            catch
            {
                MessageBox.Show("Error reading in modeled height in Continuum file.", "Continuum 3");
                return;
            }

            try
            {
                merraList = (MERRACollection)bin.Deserialize(fstream);
            }
            catch
            {
                MessageBox.Show("Error reading in MERRA2 data in Continuum file.", "Continuum 3");
                return;
            }

            try
            {
                siteSuitability = (SiteSuitability)bin.Deserialize(fstream);
            }
            catch
            {
                MessageBox.Show("Error reading in Site Suitability data in Continuum file.", "Continuum 3");
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

            if (turbineList.genTimeSeries && metList.allMCPd)
                chkCreateTurbTS.Checked = true;
            else
                chkCreateTurbTS.Checked = false;

            // Taking out, just get sensor data if toggling Met Data QC dropdown
            // Get sensor data from database
            //   if (metList.isTimeSeries)
            //   for (int i = 0; i < metList.ThisCount; i++)
            //       metList.metItem[i].metData.GetSensorDataFromDB(this, metList.metItem[i].name);

            // Get MCP reference and target data, if MCP done
            /*      for (int i = 0; i < metList.ThisCount; i++)
                  {
                      Met thisMet = metList.metItem[i];

                      if (thisMet.mcp != null)
                      {
                          UTM_conversion.Lat_Long theseLL = UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);

                          if (thisMet.mcp.gotMCP_Est == true)
                          {
                              MERRA thisMERRA = merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
                              thisMet.mcp.GetRefData(thisMERRA, ref thisMet, this);
                              thisMet.mcp.GetTargetData(modeledHeight, thisMet);
                          }
                      }
                  }
                */
            merraList.SetMERRA2LatLong(this);
            updateThe.AllTABs(this);
                       
            Text = savedParams.savedFileName;
            fileChanged = false;
            saveToolStripMenuItem.Enabled = true;
            tabContinuum.SelectedIndex = 0;                      

        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Make sure the user wants a new project
            DialogResult goodToGo = MessageBox.Show("Do you want to create a new Continuum file?", "Continuum 3.0", MessageBoxButtons.YesNo);
            if (goodToGo == DialogResult.No)
                return;            
            
            // Checks if user wants to save and calls updateThe.NewProject()            
            if (fileChanged == true) {
                DialogResult Want_to_save_file = MessageBox.Show("Do you want to save changes?", "", MessageBoxButtons.YesNo);
                if (Want_to_save_file == DialogResult.Yes) {
                    if (savedParams.savedFileName != "")
                        SaveFile(false);
                    else
                        SaveAs();                    
                }
            }
                                    
            fileChanged = false;
            Text = "Continuum Model.cfm";            
            updateThe.NewProject(this);
            savedParams.savedFileName = null;
            MessageBox.Show("Please specify where to save file.", "Continuum 3");
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
            SaveFile(false);
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

            if (turbineList.GotEst("Expo", new TurbineCollection.PowerCurve(), null) == true) {
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
            // Sets default Min/Max X/Y for a map (or uses last map's bounds) on GenMap form and opens form
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot generate a map while other calculations are under way.", "Continuum 3");
                return;
            }

            if (metList.ThisCount == 0) {
                MessageBox.Show("You need to import met TAB files first.", "Continuum 3");
                return;
            }

            if (topo.gotTopo == false)
            {
                MessageBox.Show("You need to import topography data first.", "Continuum 3");
                return;
            }

            if ((modelList.ModelCount <= 1 && metList.ThisCount > 1) || metList.expoIsCalc == false)
            {
                MessageBox.Show("The met sites have not been analyzed and a site-calibrated model has not been created yet. Starting those calculations now.", "No Site-Calibrated Model");
                Analyze_Mets();
                return;
            }
        
            if (BW_worker.BackgroundWorker_Map.IsBusy == false) {                                
                                
                GenMap thisMap = new GenMap(this);
                thisMap.cboWhatToMap.SelectedIndex = 2;    

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
                                
                thisMap.FindLargestArea();
                thisMap.txtMapReso.Text = thisMap.gridReso.ToString();

                if (thisMap.minUTMX == 0)
                    thisMap.GetBiggestArea();
                
                thisMap.UpdateTextboxes();
                
                thisMap.chkMetsToUse.Items.Clear();
                Met thisMet = null;

                for (int j = 0; j < metList.ThisCount; j++) {
                    thisMet = metList.metItem[j];
                    thisMap.chkMetsToUse.Items.Add(thisMet.name, true);
                }

                metList.AreAllMetsMCPd();

                if (metList.isTimeSeries == false || metList.allMCPd == false)
                {
                    thisMap.cboUseTimeSeries.SelectedIndex = 0; // Use Avg Dist.
                    thisMap.cboUseTimeSeries.Enabled = false; // User has no choice if not time series model
                }
                else
                {
                    thisMap.cboUseTimeSeries.SelectedIndex = 0; // Use Avg Dist.
                    thisMap.cboUseTimeSeries.Enabled = true; // User can choose to generate map using average distributions or using GenerateTimeSeries
                }

                thisMap.ShowDialog();
            }
            else {
                MessageBox.Show("You can only generate one map at a time.", "Continuum 3");
                return;
            }

        }

        private void lstMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates displayed map and map stats            
            updateThe.Generated2DMap(this);
            updateThe.MapStats(this);
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
                MessageBox.Show("Cannot delete a map while one is being generated.", "Continuum 3");
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
                MessageBox.Show("You need to select one map to export.", "Continuum 3");
                return;
            }

            if (lstMaps.SelectedItems.Count > 1) {
                MessageBox.Show("Can only create one WRG file at a time.  Please select only one map to use.", "Continuum 3");
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
            
        }

        private void btnRefreshMap_Click(object sender, EventArgs e)
        {
            // Updates the displayed map            
            updateThe.Generated2DMap(this);
        }

        private void AboutContinuumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Shows the "About Continuum" form
            AboutContinuum thisAbout = new AboutContinuum();
            thisAbout.ShowDialog();          
        
        }               

        private void btnRefreshMain_Click(object sender, EventArgs e)
        {
            // Refreshes topo/LC/SR/DH map            
            updateThe.TopoMap(this);
            ChangesMade();
        }

         private void chkMetLabels_SelectedIndexChanged(object sender, EventArgs e)
        { 
            // Updates labels on map and series shown on wind rose and directional WS ratio plots based on selected met sites
            if (okToUpdate == true) 
                updateThe.InputTAB(this);                
            }        

        private void chkTurbLabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates labels on map based on selected turbine sites
            if (okToUpdate == true)                           
                updateThe.TopoMap(this);                
            
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
                updateThe.Generated2DMap(this);                
            
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
                updateThe.MapTAB(this);
                            
        }

        private void HelpMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Opens user manual
            string filePath = "C:\\Program Files\\One Energy\\Continuum 3\\Documentation\\Continuum 3 Users Manual v1.0.pdf";

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
            {
                updateThe.EndMet_Dropdown(this);
                updateThe.AdvancedTAB(this);
            }
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

                    updateThe.GrossTurbEstList(this);
                    updateThe.GrossHistogram(this);
                    updateThe.TurbStats(this);
                    updateThe.WS_or_WR_Plot(this);
                }
            }
        }

        private void btnExportStepwise_Click(object sender, EventArgs e)
        {
            // Calls Export.ExportNodesAndWS to export estimates calculated along path of nodes for selected start and end met/turbine
                  
            if (modelList.ModelCount == 0) {
                MessageBox.Show("No models have been created yet.", "Continuum 3");
                return;
            }                   

            Export thisExport = new Export();
            thisExport.ExportNodesAndWS(this);
        }

        private void btnDoRRCalcs_Click(object sender, EventArgs e)
        {
            // if there are enough mets for a Round Robin analysis, creates struct for Round Robin calculations and calls background worker to conduct analysis
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot conduct Round Robin analysis while other calculations are under way.", "Continuum 3");
                return;
            }

            if (metList.expoIsCalc == false)
            {
                MessageBox.Show("Met sites have not been analyzed yet. Conducting calculations now.");
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
            bool RR_done = metPairList.RR_DoneAlready(metsUsed, minRR_Size, metList);

            if (RR_done == true) // Update plots and tables
                return;

            BackgroundWork.Vars_for_RoundRobin argsForBW = new BackgroundWork.Vars_for_RoundRobin();
            argsForBW.thisInst = this;
            argsForBW.Min_RR_Size = minRR_Size;
            argsForBW.MCP_Method = Get_MCP_Method();

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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot generate turbine estimates while other calculations are under way.", "Continuum 3");
                return;
            }   
            
            if (metList.ThisCount == 0)
            {
                MessageBox.Show("You need to import met sites fist.", "Continuum 3");
                return;
            }

            if (turbineList.TurbineCount == 0) {
                MessageBox.Show("You need to import turbines fist.", "Continuum 3");
                return;
            }

            if (modelList.HaveRequiredModels(metList) == false) {
                MessageBox.Show("Create a site-calibrated model first clickng Analyze Mets.", "Continuum 3");
                return;
            }

            if (modelList.ModelCount == 0) {
                MessageBox.Show("Create a Continuum model first clickng Analyze Mets.", "Continuum 3");
                return;
            }

            if (chkCreateTurbTS.CheckState == CheckState.Checked)
                turbineList.genTimeSeries = true;
            else
                turbineList.genTimeSeries = false;
                  
             turbineList.ClearWS_EstCalcs();

            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();
                        
            argsForBW.thisInst = this;
            argsForBW.thisWakeModel = null;
            
            // Call background worker to run calculations
            BW_worker = new BackgroundWork();
            string MCP_Method = Get_MCP_Method();
            argsForBW.MCP_Method = MCP_Method;
            if (topo.gotTopo)
                BW_worker.Call_BW_TurbCalcs(argsForBW);
            ChangesMade();
            
        }
        
        private void btnImportTAB_Click(object sender, EventArgs e)
        {
            // Calls ImportMetsTAB, if successful and if have topo and land cover data, creates struct needed for Met calcs and calls background worker otherwise updates 
            // all plots and tables referencing the met sites
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import met data while calculations are under way.", "Continuum 3");
                return;
            }
                        
            if (metList.isTimeSeries == true && metList.ThisCount > 0)
            {
                DialogResult goodToGo = MessageBox.Show("Time Series Met data has already been imported. Importing TAB files will delete time series data and " +
                    "models will be formed using TAB file WSWD distributions. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);
                if (goodToGo == DialogResult.No)
                    return;
                else
                {
                    // Clear met objects and data from DB
                    metList.ClearAllMets(this, true);
                }
            }

            if (ofdMets.ShowDialog() != DialogResult.OK)
                return;
            
            bool reCalcEsts = ImportMetsTAB();

            if (reCalcEsts == true) {
                
                if (metList.ThisCount == 0) 
                    return;

                metList.isTimeSeries = false;
                turbineList.ClearAllWSEsts();                
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
                    DialogResult yes_or_no = MessageBox.Show("Do you have land cover data to import? Click No and Continuum will start the site-calibration using only the topography data.", "Continuum 3", MessageBoxButtons.YesNo);
                    if (yes_or_no == DialogResult.Yes) {                        
                        updateThe.AllTABs(this);
                        return;
                    }
                }
                
                // Call background worker to run calculations
                // In background worker, performs exposure and grid stat calculations, finds path of nodes and does site calibration.
                BW_worker = new BackgroundWork();
                BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
                theArgs.thisInst = this;
                theArgs.MCP_type = Get_MCP_Method();
                BW_worker.Call_BW_MetCalcs(theArgs);
                ChangesMade();
            }
            else
            {
                updateThe.AllTABs(this);
            }

        }

        private void btnImportCRV_Click(object sender, EventArgs e)
        {
            // Prompts user to open a power curve. thisPowerCurve file and updates turbine calcs if they were done before

            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy())
            {
                MessageBox.Show("Cannot import a power curve while calculations are under way.", "Continuum 3");
                return;
            }

            if (ofdPowerCurve.ShowDialog() != DialogResult.OK)
                return;

            double RD = Convert.ToSingle(Interaction.InputBox("What is the rotor diameter [m] of the turbine?", "Continuum 3"));
            double RPM = Convert.ToSingle(Interaction.InputBox("What is the rated RPM of the turbine?", "Continuum 3"));

            turbineList.ImportPowerCurve(this, RD, RPM);
        }

        private void btnDelPowerCrv_Click(object sender, EventArgs e)
        {           
                       
            if (lstPowerCurveList.SelectedItems.Count > 0) {
                for (int i = 0; i < lstPowerCurveList.SelectedItems.Count; i++)
                {
                    string powerCurveToDelete = lstPowerCurveList.SelectedItems[i].Text;
                    turbineList.DeletePowerCurve(powerCurveToDelete);

                    for (int t = 0; t < turbineList.TurbineCount; t++)
                        turbineList.turbineEsts[t].ClearGrossEstsFromAvgWS(powerCurveToDelete);

                    wakeModelList.RemoveWakeModelByPowerCurve(turbineList, mapList, powerCurveToDelete);
                }

                turbineList.ClearDuplicateAvgWS(metList.isTimeSeries);
                merraList.ClearMERRA_ProdStats();                    
                updateThe.AllTABs(this);
                    
            }            
            else 
                MessageBox.Show("No power curves to delete", "Continuum 3");            

            updateThe.ColoredButtons(this);

        }

        private void chkTurbGross_SelectedIndexChanged(object sender, EventArgs e) 
            { 
            // Updates the plots and tables on Gross Turb Est tab based on selected turbines
            if (okToUpdate == true)                 
                updateThe.GrossTurbineEstsTAB(this); 
        }

        private void chkMetSummAll_CheckedChanged(object sender, EventArgs e)
        { 
            // Selects/Deselects all Met sites on 'Met and Turbine Summary' tab and calls updateThe.Met_Summary_Tables_and_Plots
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
            // Selects/Deselects all Turbines sites on 'Gross Turbine Est' tab and updates plots and tables
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

            if (powerCurve == "No Power Curves Imported") {
                MessageBox.Show("No power curves have been imported yet.", "Continuum 3");
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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot analyze met data while calculations are under way.", "Continuum 3");
                return;
            }

            Analyze_Mets();            
        }

        public void Analyze_Mets()
        {
            // Call background worker to run all met calculations (exposure, SDH, grid stats, site-calibrated model)
            if (metList.ThisCount == 0)
                return;                      

            modelList.ClearAllExceptImported();

            if (chkUseSR.Checked == true)
                topo.useSR = true;
            else
                topo.useSR = false;
                        
            BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
            theArgs.thisInst = this;
            theArgs.MCP_type = Get_MCP_Method();
            BW_worker.Call_BW_MetCalcs(theArgs);
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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot export model info while calculations are under way.", "Continuum 3");
                return;
            }                   

            Export thisExport = new Export();
            thisExport.Export_Models_CSV(this);
        }

        private void btnExportDirWS_Click(object sender, EventArgs e)
        {
            // Calls Export.Export_Directional_WS to export directional wind speeds at selected mets and turbines (Met and Turbine Summary tab) 
            Export thisExport = new Export();
            thisExport.Export_Directional_WS(this, "Gross");
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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import land cover data while calculations are under way.", "Continuum 3");
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
            // Updates the plots and tables on the 'Met and Turbine Summary' tab based on selected radius from drop-down
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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (Created && metList.ThisCount > 0 && modelList.ModelCount > 0 && BW_worker.IsBusy() == false)
            {
                if ((chkUseSR.Checked == false && topo.useSR == false) ||(chkUseSR.Checked == true && topo.useSR == true) ||(chkUseSR.Checked == false && topo.gotSR == false) ) 
                  return;
              else if ((chkUseSR.Checked == true && topo.gotSR == false) ) {
                    chkUseSR.Checked = false;
                 return;

                }

                DialogResult yes_or_no = MessageBox.Show("Changing this setting will recreate the wind flow model and estimates. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);
                
                if (yes_or_no == DialogResult.Yes) {

                    modelList.ClearAllExceptImported();
                    metPairList.RemoveAllExceptDefault();
               //     turbineList.ClearWS_EstCalcs();
                    turbineList.ClearAllWSEsts();
                    turbineList.ClearAllGrossEsts();
                    turbineList.ClearAllNetEsts();
                    metPairList.ClearRoundRobin();
                    mapList.ClearAllMaps();
                    wakeModelList.ClearWakeMaps();

                    if (chkUseSR.Checked == true)
                        topo.useSR = true;
                    else
                        topo.useSR = false;                                       

                    // Call background worker to run calculations
                    BW_worker = new BackgroundWork();
                    BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
                    theArgs.thisInst = this;
                    theArgs.MCP_type = Get_MCP_Method();
                    BW_worker.Call_BW_MetCalcs(theArgs);
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
                MessageBox.Show("Import a power and thrust curve to conduct wake modeling.", "Continuum 3");
                    return;
            }

            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot generate a wake model while other calculations are under way.", "Continuum 3");
                    return;
            }

            if (turbineList.exceed == null)
            {
                MessageBox.Show("Run Exceedance Monte Carlo model before calculating net estimates");
                return;
            }

            if (turbineList.exceed.compositeLoss.isComplete == false)
            {
                MessageBox.Show("Run Exceedance Monte Carlo model before calculating net estimates");
                return;
            }

            Gen_WakeModel thisWake = new Gen_WakeModel(this);
            
            thisWake.cboWakeCombo.SelectedIndex = 0;
            thisWake.cboWakeModel.SelectedIndex = 0;
            thisWake.cboPowerCrvs.SelectedIndex = 0;
            thisWake.txtHorizWakeExp.Text = "5";
            thisWake.txtAmbTI.Text = "10";
          //  
          //  

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
            {
                updateThe.WakedTurbList(this);
                updateThe.WakedWSDistPlot(this);
                updateThe.TurbsByString(this);
                updateThe.WakeLossMap(this);
            }                           
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
                        turbineList.ClearDuplicateAvgWS(metList.isTimeSeries);
                    }

                    ChangesMade();
                }
                
                updateThe.NetTurbineEstsTAB(this);
                updateThe.MapsTAB(this);
                updateThe.Monthly_TAB(this);
                updateThe.ColoredButtons(this);
            }
            else {
                MessageBox.Show("Cannot delete a map while one is being generated.", "Continuum 3");
                return;
            }
        }

        public Wake_Model GetSelectedWakeModel()
        {
            // Returns Wake_Model object based on model selected from list on "Net Turbine Ests." tab
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
            else if (objListView.Text == "Jensen")
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
            // Returns WakeGridMap object based on model selected from list on "Net Turbine Ests." tab
                                   
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
            else if (objListView.Text == "Jensen")
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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot generate a map while other calculations are under way.", "Continuum 3");
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
            
            int selectedWakeInd = 0;

            try
            {
                selectedWakeInd = lstWakeModels.SelectedIndices[0];
            }
            catch
            {
                selectedWakeInd = 0;
            }
            
            if (wakeModelList.NumWakeModels == 0) {
                MessageBox.Show("Create a wake model first.", "Continuum 3");
                return;
            }
            else {
                thisMap.cboWakeModels.Items.Clear();
                for (int i = 0; i <= wakeModelList.NumWakeModels - 1; i++) {
                    string Wake_String = wakeModelList.CreateWakeModelString(wakeModelList.wakeModels[i]);
                    thisMap.cboWakeModels.Items.Add(Wake_String);
                }

                try {
                    thisMap.cboWakeModels.SelectedIndex = selectedWakeInd;
                }
                catch {
                    return;
                }

                metList.AreAllMetsMCPd();

                if (metList.isTimeSeries == false || metList.allMCPd == false)
                {
                    thisMap.cboUseTimeSeries.SelectedIndex = 0; // Use Avg Dist.
                    thisMap.cboUseTimeSeries.Enabled = false; // User has no choice if not time series model
                }
                else
                {
                    thisMap.cboUseTimeSeries.SelectedIndex = 0; // Use Avg Dist
                    thisMap.cboUseTimeSeries.Enabled = true; // User can choose to generate map using average distributions or using GenerateTimeSeries
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
                okToClear = MessageBox.Show("Importing coefficients will delete the site-calibrated coefficients. Do you wish to continue?", "Continuum 3", MessageBoxButtons.YesNo);

            if (okToClear == DialogResult.Yes) {
                
                modelList.ClearAll();                
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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (Created && metList.ThisCount > 0 && BW_worker.IsBusy() == false) {
                if ( (chk_Use_Sep.Checked == false && topo.useSepMod == false) ||(chk_Use_Sep.Checked == true && topo.useSepMod == true) ) 
                   return;

                DialogResult yes_or_no = MessageBox.Show("Changing this setting will recreate the wind flow model and estimates. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (yes_or_no == DialogResult.Yes) {

                    modelList.ClearAllExceptImported();
                    metPairList.ClearAll();
                    turbineList.ClearWS_EstCalcs();
                    turbineList.ClearAllGrossEsts();
                    turbineList.ClearAllNetEsts();
                    metPairList.ClearRoundRobin();
                    
                    if (chk_Use_Sep.Checked == true)
                        topo.useSepMod = true;
                    else
                        topo.useSepMod = false;

                    // Call background worker to run calculations
                    BW_worker = new BackgroundWork();
                    BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
                    theArgs.thisInst = this;
                    theArgs.MCP_type = Get_MCP_Method();
                    BW_worker.Call_BW_MetCalcs(theArgs);
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
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot import land cover data while calculations are under way.", "Continuum 3");
                return;
            }
                                    
            if (savedParams.savedFileName == null)
            {
                MessageBox.Show("Please specify the file location.", "Continuum 3");
                bool wasSaved = false;
                wasSaved = SaveAs();
                if (wasSaved == false)
                {
                    MessageBox.Show("A file location is needed in order to create local database. Cancelling land cover import.", "Continuum 3");
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

            DialogResult Good_to_go = DialogResult.Yes;
            if (turbineList.turbineCalcsDone)
                Good_to_go = MessageBox.Show("Importing land cover data will reset all model calculations and estimates. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);
            
            if (Good_to_go == DialogResult.No)
                return;

            Roughness_File_Format thisRough = new Roughness_File_Format();
            thisRough.cboRoughnessFile.SelectedIndex = 0;
            thisRough.ShowDialog();

            int geoTiffOrMap = thisRough.cboRoughnessFile.SelectedIndex;

            modelList.ClearAllExceptImported();
            metPairList.ClearAll();            
            turbineList.ClearAllWSEsts();
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
        
        public string GetEndSiteAdvanced() // Returns name of met site selected as "End Met" on Advanced tab
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
            // Returns number of wind direction sectors used in model
            int numWD = 0;

            numWD = metList.numWD;

            return numWD;
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

        public string GetStartMetAdvanced() // Returns name of met site selected as "Start Met" on Advanced tab
        {
            string met1Str = "";
            try
            {
                met1Str = cboStartMet.SelectedItem.ToString();
            }
            catch { }

            return met1Str;
        }
        public int GetWD_ind(string TAB_name) // Returns WD sector selected on specified tab
        {
            int WD_Ind = 0;

            if (metList.numWD == 0) return WD_Ind;

            if (TAB_name == "Summary")
            {
                try
                {
                    WD_Ind = cboSummaryWD.SelectedIndex;
                }
                catch { }
            }
            else if (TAB_name == "MCP")
            {
                try
                {
                    WD_Ind = cboMCP_WD.SelectedIndex;
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
            else if (TAB_name == "Site Conditions TI")
            {
                try
                {
                    WD_Ind = cboTurbWD.SelectedIndex;
                }
                catch { }
            }
            
            return WD_Ind;
        }

        public Model[] GetModels(Continuum thisInst, string tabName)
        {
            // Returns models based on tabName and selected model, time of day and season on specified tab
                        
            Met.TOD thisTOD = GetSelectedTOD(tabName);
            Met.Season thisSeason = GetSelectedSeason(tabName);
            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisTOD, thisSeason, thisInst.modeledHeight, false);

            return models;

        }

        private void btnMetData_Click(object sender, EventArgs e)
        {
            // Imports met time series data. Asks user for met file format. Calls Do MCP if have MERRA data then calls Do Met Calcs.
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy())
            {
                MessageBox.Show("Cannot import met data while other calculations underway.", "Continuum 3");
                return;
            }

            bool wasSaved = false;
            if (savedParams.savedFileName == null)
            {
                MessageBox.Show("Please specify the file location.", "Continuum 3");
                wasSaved = SaveAs();
                if (wasSaved == false)
                {
                    MessageBox.Show("A file location is needed in order to create local database. Cancelling met data import.", "Continuum 3");
                    return;
                }
            }

            string filename = "";
            DialogResult Good_to_go = DialogResult.Yes;

            if (metList.ThisCount > 0 && metList.isTimeSeries == false)
            {
                Good_to_go = MessageBox.Show("Annual TAB files have been entered for other met sites. If you import this time series data, the model will be reset. Do you " +
                    "want to continue?", "Continuum 3", MessageBoxButtons.YesNo);
            }

            if (Good_to_go == DialogResult.No)
                return;

            DialogResult doFiltering = new DialogResult();            
            // On first import, ask user if the modeled height is the one that they want
            if (metList.ThisCount == 0)
            {
                doFiltering = MessageBox.Show("Do you want to apply met data QC filters?", "Continuum 3", MessageBoxButtons.YesNo);
                Good_to_go = MessageBox.Show("The modeled height is set at " + modeledHeight.ToString() + " m. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (Good_to_go == DialogResult.No)
                    return;

                Good_to_go = MessageBox.Show("The number of MERRA2 nodes is set to " + merraList.numMERRA_Nodes.ToString() + ". Options are 1, 4, or 16 nodes. " +
                    "Do you want to continue?", "Continuum 3.0", MessageBoxButtons.YesNo);

                if (Good_to_go == DialogResult.No)
                {
                    tabContinuum.SelectedIndex = 2;
                    return;
                }

                Good_to_go = MessageBox.Show("The MCP settings are " + Get_MCP_Method() + " Num WD bins: " + metList.numWD.ToString() + ", Num TOD bins: " +
                    metList.numTOD.ToString() + ", Num Seasonal bins: " + metList.numSeason.ToString() + ". Do you want to continue?", "Continuum 3.0", MessageBoxButtons.YesNo);

                if (Good_to_go == DialogResult.No)
                {
                    tabContinuum.SelectedIndex = 3;
                    return;
                }
                
            }
            else
            {
                if (metList.filteringEnabled == true)
                    doFiltering = DialogResult.Yes;
                else
                    doFiltering = DialogResult.No;
            }

            if (Good_to_go == DialogResult.Yes)
            {               

                if (ofdMetData.ShowDialog() == DialogResult.OK)
                {
                    filename = ofdMetData.FileName;

                    string wholePath = ofdMetData.FileName;
                    SetDefaultFolderLocations(wholePath);

                    if (metList.ThisCount > 0 & metList.isTimeSeries == false)
                        metList.ClearAllMets(this, true);

                    metList.expoIsCalc = false;
                    turbineList.ClearAllWSEsts();
                    turbineList.ClearAllGrossEsts();
                    turbineList.ClearAllNetEsts();
                    modelList.ClearAllExceptImported();
                    metPairList.ClearAll();
                    mapList.ClearAllWakedMaps();                                       

                    if (doFiltering == DialogResult.Yes)
                    {
                        okToUpdate = false;
                        chkDisableFilter.CheckState = CheckState.Checked;
                        metList.filteringEnabled = true;
                        okToUpdate = true;
                    }
                    else
                    {
                        okToUpdate = false;
                        chkDisableFilter.CheckState = CheckState.Unchecked;
                        metList.filteringEnabled = false;
                        okToUpdate = true;
                    }
                                        
                    bool gotIt = metList.ImportFilterExtrapMetDataContinuum(filename, this); // Reads in formatted .csv file, filters and extrapolates to modeled height
                    
                    if (gotIt == true)
                    {
                        metList.isTimeSeries = true;
                        metList.numWD = Convert.ToInt16(cboMCPNumWD.SelectedItem);
                        metList.numWS = 30;

                        string metName = metList.metItem[metList.ThisCount - 1].name;
                        Met thisMet = metList.GetMet(metName);
                        thisMet.metData.FindStartEndDatesWithMaxRecovery();
                        // Check that WS_First and WS_int_size were initialized                                            

                        // Check to see if user wants to load MERRA2 data

                        DialogResult getMERRA = DialogResult.No;

                        if (metList.ThisCount == 1)
                            getMERRA = MessageBox.Show("Do you want to import MERRA2 data and conduct MCP?", "Continuum 3.0", MessageBoxButtons.YesNo);
                        else if (metList.ThisCount > 1 & metList.isMCPd == true)
                            getMERRA = DialogResult.Yes;

                        if (getMERRA == DialogResult.Yes)
                        {
                            metList.isMCPd = true;
                            // check to see if MERRA2 data has been loaded for this met
                            UTM_conversion.Lat_Long theseLL = UTM_conversions.UTMtoLL(metList.metItem[metList.ThisCount - 1].UTMX, metList.metItem[metList.ThisCount - 1].UTMY);
                            int UTC_offset = UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);

                            // Adds new MERRA object to list, figures out if new MERRA nodes are needed, runs MCP
                            merraList.AddMERRA_GetDataFromTextFiles(theseLL.latitude, theseLL.longitude, UTC_offset, this, metList.metItem[metList.ThisCount - 1], false);
                            MERRA thisMERRA = merraList.GetMERRA(theseLL.latitude, theseLL.longitude);

                            if (BW_worker.IsBusy() == false && thisMERRA.interpData.TS_Data != null) // found existing MERRA2 data so let's MCP!
                            {                                
                                string MCP_Method = Get_MCP_Method();

                                metList.RunMCP(ref thisMet, thisMERRA, this, MCP_Method); // Runs MCP and generates LT WS estimates                                                                                 

                                thisMet.CalcAllLT_WSWD_Dists(this, thisMet.mcp.LT_WS_Ests); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)

                                if (topo.gotTopo == false)
                                {
                                    updateThe.AllTABs(this);
                                    return;
                                }

                                if (chkUseSR.Checked == true && topo.gotSR == true)
                                    topo.useSR = true;
                                else
                                    topo.useSR = false;

                                if (chk_Use_Sep.Checked == true)
                                    topo.useSepMod = true;
                                else
                                    topo.useSepMod = false;                                                            
                                                          
                                ChangesMade();
                                updateThe.AllTABs(this);
                            }
                        }
                        else
                        {
                            metList.isMCPd = false;                            
                            thisMet.CalcAllMeas_WSWD_Dists(this, thisMet.metData.GetSimulatedTimeSeries(modeledHeight));

                            updateThe.AllTABs(this);
                            ChangesMade();
                        }
                    }                    

                }
                else
                {
                    updateThe.AllTABs(this);
                }
            }           
        
        }
               

        private void btnViewFilters_Click(object sender, EventArgs e)
        {
            // Opens form showing the Met Data QC filter settings
            Filter_Settings theseFilts = new Filter_Settings();
            Met thisMet = GetSelectedMet("Met Data QC");

            theseFilts.txtIcingMaxTemp.Text = thisMet.metData.maxIcingTemp.ToString();
            theseFilts.txtIcingMinAnemSD.Text = thisMet.metData.anemIcingMinSD.ToString();
            theseFilts.txtIcingMinVaneSD.Text = thisMet.metData.vaneIcingMinSD.ToString();
            theseFilts.txtMaxAnemSD.Text = thisMet.metData.maxSD_FiltSlope + " x WS + " + thisMet.metData.maxSD_FiltInt;
            theseFilts.txtMaxRange.Text = thisMet.metData.maxRange.ToString();
            theseFilts.txtMin_WS_for_SD_Filts.Text = thisMet.metData.minWS_ForSD_Filts.ToString();
            theseFilts.txtMinAnemSD.Text = thisMet.metData.minSD_FiltSlope + " x WS";
            theseFilts.txtShadowThresh.Text = thisMet.metData.towerShadowThresh.ToString();
            theseFilts.txtShadow_Width.Text = thisMet.metData.towerShadowWidth.ToString();
            theseFilts.txtMaxWSForMinWSFilt.Text = thisMet.metData.minWS_Filt.ToString();
            theseFilts.txtMinClosestWS.Text = thisMet.metData.minClosestWS.ToString();

            // Update tower shadow list
            theseFilts.lstTowerShadow.Items.Clear();
            ListViewItem lstView = new ListViewItem();

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
            {
                lstView = theseFilts.lstTowerShadow.Items.Add(Convert.ToString(thisMet.metData.anems[i].height));
                lstView.SubItems.Add(thisMet.metData.anems[i].orientation.ToString());
                lstView.SubItems.Add(thisMet.metData.anems[i].shadow.center.ToString());
                lstView.SubItems.Add(thisMet.metData.anems[i].shadow.minWD.ToString());
                lstView.SubItems.Add(thisMet.metData.anems[i].shadow.maxWD.ToString());

                int Width = 0;
                if (thisMet.metData.anems[i].shadow.maxWD > thisMet.metData.anems[i].shadow.minWD)
                    Width = thisMet.metData.anems[i].shadow.maxWD - thisMet.metData.anems[i].shadow.minWD;
                else
                    Width = 360 - thisMet.metData.anems[i].shadow.minWD + thisMet.metData.anems[i].shadow.maxWD;

                lstView.SubItems.Add(Width.ToString());
            }

            theseFilts.ShowDialog();
        }

        public Met GetSelectedMet(string TAB_Name)
        {
            // Returns met object based on met selected on specified tab
            Met thisMet = new Met();

            if (TAB_Name == "Met Data QC")
            {
                try
                {
                    string thisMetStr = cboMetQC_SelectedMet.SelectedItem.ToString();
                    thisMet = metList.GetMet(thisMetStr);
                }
                catch { }
            }
            else if (TAB_Name == "MCP")
            {
                try
                {
                    string thisMetStr = cboMCP_Met.SelectedItem.ToString();
                    thisMet = metList.GetMet(thisMetStr);
                }
                catch { }
            }
            else if (TAB_Name == "MERRA")
            {
                try
                {
                    string thisMetStr = cboMERRASelectedMet.SelectedItem.ToString();
                    thisMet = metList.GetMet(thisMetStr);
                }
                catch { }
            }
            else if (TAB_Name == "Site Conditions TI")
            {
                try
                {
                    string thisMetStr = cboTurbMet.SelectedItem.ToString();
                    thisMet = metList.GetMet(thisMetStr);
                }
                catch { }
            }
            else if (TAB_Name == "Site Conditions Shear")
            {
                try
                {
                    string thisMetStr = cboExtremeShearMet.SelectedItem.ToString();
                    thisMet = metList.GetMet(thisMetStr);
                }
                catch { }
            }
            else if (TAB_Name == "Site Conditions Extreme WS")
            {
                try
                {
                    string thisMetStr = cboExtremeWSMet.SelectedItem.ToString();
                    thisMet = metList.GetMet(thisMetStr);
                }
                catch { }
            }


            return thisMet;
        }

        public Turbine GetSelectedTurbine(string TAB_Name)
        {
            // Returns turbine object based on turbine selected on specified tab
            Turbine thisTurb = new Turbine();

            if (TAB_Name == "Monthly")
            {
                try
                {
                    string thisTurbStr = cboSelectedTurbine.SelectedItem.ToString();
                    thisTurb = turbineList.GetTurbine(thisTurbStr);
                }
                catch { }
            }
            else if (TAB_Name == "Exceedance")
            {
                try
                {
                    string thisTurbStr = cboExceedTurbine.SelectedItem.ToString();
                    thisTurb = turbineList.GetTurbine(thisTurbStr);
                }
                catch { }
            }
            else if (TAB_Name == "Turbulence")
            {
                try
                {
                    string thisTurbStr = cboTurbineTI.SelectedItem.ToString();
                    thisTurb = turbineList.GetTurbine(thisTurbStr);
                }
                catch { }
            }
            else if (TAB_Name == "Inflow Angle")
            {
                try
                {
                    string thisTurbStr = cboInflowTurbine.SelectedItem.ToString();
                    thisTurb = turbineList.GetTurbine(thisTurbStr);
                }
                catch { }
            }

            return thisTurb;
        }

        private void cboMetWindRose_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates wind rose plot on Met Data QC tab showing either wind rose or wind speed by WD
            updateThe.MetWindRoseOrWS_byWDPlot(this, GetSelectedMet("Met Data QC"));
        }               

        public Met.TOD GetSelectedTOD(string TAB_name)
        {
            // Returns time of day (TOD) selected on specified tab
            string thisTOD_Str = "";
            Met.TOD thisTOD = Met.TOD.All;

            if (TAB_name == "Advanced")
                thisTOD_Str = cboTODAdvanced.SelectedItem.ToString();
            else if (TAB_name == "Summary")
                thisTOD_Str = cboSummTOD.SelectedItem.ToString();
            else if (TAB_name == "MCP")
                thisTOD_Str = cboMCP_TOD.SelectedItem.ToString();

            if (thisTOD_Str == Met.TOD.All.ToString())
                thisTOD = Met.TOD.All;
            else if (thisTOD_Str == Met.TOD.Day.ToString())
                thisTOD = Met.TOD.Day;
            else if (thisTOD_Str == Met.TOD.Night.ToString())
                thisTOD = Met.TOD.Night;
            
            return thisTOD;
        }

        public Met.Season GetSelectedSeason(string TAB_name)
        {
            // Returns season selected on specified tab
            string thisSeasonStr = "";
            Met.Season thisSeason = Met.Season.All;

            if (TAB_name == "Advanced")
                thisSeasonStr = cboSeasonAdvanced.SelectedItem.ToString();
            else if (TAB_name == "Summary")
                thisSeasonStr = cboSummSeason.SelectedItem.ToString();
            else if (TAB_name == "MCP")
                thisSeasonStr = cboMCP_Season.SelectedItem.ToString();

            if (thisSeasonStr == Met.Season.All.ToString())
                thisSeason = Met.Season.All;
            else if (thisSeasonStr == Met.Season.Winter.ToString())
                thisSeason = Met.Season.Winter;
            else if (thisSeasonStr == Met.Season.Spring.ToString())
                thisSeason = Met.Season.Spring;
            else if (thisSeasonStr == Met.Season.Summer.ToString())
                thisSeason = Met.Season.Summer;
            else if (thisSeasonStr == Met.Season.Fall.ToString())
                thisSeason = Met.Season.Fall;

            return thisSeason;
        }

        private void btnExportExtrap_Click(object sender, EventArgs e)
        {
            // Export estimated met data at height specified by user

            // Get selected met on Met Data QC tab
            Met thisMet = GetSelectedMet("Met Data QC");

            if (thisMet.metData.GetNumSimData() == 0)
            {
                MessageBox.Show("Generate estimated data by extrapolating from met data and then export.");
                return;
            }

            double thisHeight = 0;

            // if more than one height has been estimated, ask user which one to export
            if (thisMet.metData.GetNumSimData() > 1)
            {
                Pick_a_Height userPick = new Pick_a_Height();

                for (int i = 0; i < thisMet.metData.GetNumSimData(); i++)
                    userPick.lstHeights.Items.Add(thisMet.metData.simData[i].height.ToString());

                userPick.ShowDialog();
                thisHeight = Convert.ToDouble(userPick.lstHeights.SelectedItems[0].Text);
            }
            else
                thisHeight = thisMet.metData.simData[0].height;

            Export thisExport = new Export();
            thisExport.Export_Estimated_data(this, thisMet.metData, thisHeight, false, thisMet.name);
        }               

        private void btnExportAnnualMax_Click(object sender, EventArgs e)
        {
            // Exports met data annual maximum wind speed and maximum gust of selected met on Met Data QC tab
            Export export = new Export();
            export.ExportAnnualMax(this);
        }

        private void btnResetDates_Click(object sender, EventArgs e)
        {
            // Resets MCP export dates on MCP tab
            Met thisMet = GetSelectedMet("MCP");
            thisMet.mcp.ResetExportDates(this);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets MCP method selected on main form. </summary>
        ///
        /// <remarks>   OEE, 10/19/2017. </remarks>
        ///
        /// <returns>   The MCP method string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Get_MCP_Method()
        {
            // Returns MCP type selected on MCP tab
            string MCP_Method = "";

            if (cboMCP_Type.SelectedItem == null && cboMCP_Type.Items.Count > 0)
            {
                okToUpdate = false;
                cboMCP_Type.SelectedIndex = 0;
                okToUpdate = true;
            }                

            try
            {
                MCP_Method = cboMCP_Type.SelectedItem.ToString();
            }
            catch
            { }

            return MCP_Method;
        }

        private void cboMCPNumWD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If user changes numWD here, metList numWD is updated, all calcs and estimates should be deleted and all WSWD_dists need to be recalculated with new numWD
            if (okToUpdate == false)
                return;

            if (metList.isTimeSeries && metList.ThisCount > 0)
            {
                DialogResult result = DialogResult.Yes;

                if (isTest == false)
                {
                    string message = "Changing the number of WD bins will reset the MCP and all estimated values. Do you want to continue?";
                    result = MessageBox.Show(message, "", MessageBoxButtons.YesNo);
                }
                
                if (result == DialogResult.Yes)
                {
                    metList.numWD = Convert.ToInt16(cboMCPNumWD.SelectedItem.ToString());

                    updateThe.WindDirectionToDisplay(this);

                    metList.ResetMetParams();
                    ResetTimeSeries();
                    turbineList.ClearAllCalcs();
                    NodeCollection nodeList = new NodeCollection();
                    nodeList.ClearExposGridStatsFromDB(this);
                    updateThe.AllTABs(this);
                }
                else
                    cboMCPNumWD.Text = metList.numWD.ToString();
            }
            else
            {
                metList.numWD = Convert.ToInt16(cboMCPNumWD.SelectedItem.ToString());
            }

        }

        private void cboMCP_WD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the MCP tab after the selected wind direction is changed by user
            if (okToUpdate)
                updateThe.MCP_TAB(this);
        }

        private void dateMCPExportStart_ValueChanged(object sender, EventArgs e)
        {
            // Changes MCP export start date based on user-selected date
            Met thisMet = GetSelectedMet("MCP");

            if (dateMCPExportStart.Value > thisMet.mcp.refEnd)
            {
                MessageBox.Show("Export date cannot be later than the end of the reference site data.");
                dateMCPExportStart.Value = thisMet.mcp.refStart;
            }
            else
                thisMet.mcp.exportStart = dateMCPExportStart.Value;
        }

        private void cboMCP_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Resets MCP and time series calculations after user changes the selected MCP method
                       
            if (okToUpdate && metList.isTimeSeries && metList.isMCPd && metList.ThisCount > 0)
            {
                DialogResult result = DialogResult.Yes;

                if (isTest == false)
                {
                    string message = "Changing the MCP method will reset the MCP and all estimated values. Do you want to continue?";
                    result = MessageBox.Show(message, "", MessageBoxButtons.YesNo);
                }
                
                if (result == DialogResult.Yes)
                {
                    ResetTimeSeries();
                    updateThe.AllTABs(this);
                }
                else
                {
                    // Figure out what MCP method was used before user changed it
                    for (int i = 0; i < metList.ThisCount; i++)
                        if (metList.metItem[i].mcp.gotMCP_Est == true)
                        {
                            if (metList.metItem[i].mcp.MCP_Ortho.allR_Sq != 0)
                                cboMCP_Type.SelectedIndex = 0;
                            else if (metList.metItem[i].mcp.MCP_Bins.binAvgSD_Cnt != null)
                                cboMCP_Type.SelectedIndex = 1;
                            else if (metList.metItem[i].mcp.MCP_Varrat.allR_Sq != 0)
                                cboMCP_Type.SelectedIndex = 2;
                            else if (metList.metItem[i].mcp.MCP_Matrix.WS_CDFs != null)
                                cboMCP_Type.SelectedIndex = 3;

                            break;
                        }
                }
                
            }

            updateThe.MCP_Settings(this);
        }

        private void btnExportMCP_TS_Click(object sender, EventArgs e)
        {
            // Exports MCP'd estimated time series data
            Export export = new Export();
            export.ExportMCP_TimeSeries(this);
        }

        private void btnExportBinRatios_Click(object sender, EventArgs e)
        {
            // Exports calclated target/reference wind speed bin ratios 
            Export export = new Export();
            export.ExportMCP_BinRatios(this);
        }

        private void btnExportMCP_TAB_Click(object sender, EventArgs e)
        {
            // Exports TAB file based on MCP data
            Export export = new Export();
            export.ExportMCP_TAB(this);
        }

        private void txtWS_bin_width_TextChanged(object sender, EventArgs e)
        {
            // Resets MCP and time series estimates if user changes wind speed bin width
            DialogResult result = DialogResult.Yes;
            Met thisMet = GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;

            if (thisMCP != null && isTest == false)
            {
                if (thisMCP.MCP_Bins.binAvgSD_Cnt != null || thisMCP.MCP_Matrix.WS_CDFs != null || thisMCP.uncertMatrix.Length > 0 || thisMCP.uncertBins.Length > 0)
                {
                    string message = "Changing the WS bin width will reset the MCP. Do you want to continue?";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    result = MessageBox.Show(message, "", buttons);
                }
            }

            if (result == DialogResult.Yes)
            {
                if (metList.isMCPd)                
                    ResetTimeSeries();                   
                
                try
                {
                    metList.mcpWS_BinWidth = Convert.ToSingle(txtWS_bin_width.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid wind speed bin width.", "Continuum 3.0");
                    txtWS_bin_width.Text = metList.mcpWS_BinWidth.ToString();
                }

                updateThe.AllTABs(this);
            }
            else
                txtWS_bin_width.Text = thisMCP.WS_BinWidth.ToString();
        }

        private void dateMCPExportEnd_ValueChanged(object sender, EventArgs e)
        {
            // Updates MCP export end date based on user-selected date
            Met thisMet = GetSelectedMet("MCP");

            if (dateMCPExportEnd.Value < thisMet.mcp.refStart && thisMet.mcp.gotRef == true)
            {
                MessageBox.Show("Export end date cannot be before the start of the reference site data.");
                dateMCPExportEnd.Value = thisMet.mcp.refEnd;
            }
            else
                thisMet.mcp.exportEnd = dateMCPExportEnd.Value;
        }

        private void btnMCP_Uncert_Click(object sender, EventArgs e)
        {
            // Runs MCP uncertainty analysis
            Met thisMet = GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;
            UTM_conversion.Lat_Long theseLL = UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
            MERRA thisMERRA = merraList.GetMERRA(theseLL.latitude, theseLL.longitude);

            thisMCP.Do_MCP_Uncertainty(this, thisMERRA, thisMet);
            updateThe.MCP_TAB(this);
        }

        private void btnExportMCPUncert_Click(object sender, EventArgs e)
        {
            // Exports MCP Uncertainty analysis
            Export export = new Export();
            export.ExportMCP_Uncertainty(this);
        }

        private void cboMCPNumHours_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Resets time series and MCP if user changes the number of time of day bins to use
            // If user changes numWD here then metList numWD should be updated, all calcs and estiamtes should be deleted and all WSWD_dists need to be recalculated with new numWD

            if (okToUpdate && metList.isTimeSeries && metList.isMCPd && metList.ThisCount > 0)
            {
                DialogResult result = DialogResult.Yes;

                if (isTest == false)
                {
                    string message = "Changing the number of time of day bins will reset the MCP and all estimated values. Do you want to continue?";
                    result = MessageBox.Show(message, "", MessageBoxButtons.YesNo);
                }                

                if (result == DialogResult.Yes)
                {
                    metList.numTOD = Convert.ToInt16(cboMCPNumHours.SelectedItem.ToString());
                    ResetTimeSeries();                    
                    updateThe.AllTABs(this);
                }
                else
                    cboMCPNumHours.Text = metList.numTOD.ToString();
            }
            else if (okToUpdate && metList.isTimeSeries && metList.isMCPd == false)
                metList.numTOD = Convert.ToInt16(cboMCPNumHours.SelectedItem.ToString());
            else if (metList.ThisCount == 0)
                metList.numTOD = Convert.ToInt16(cboMCPNumHours.SelectedItem.ToString());
        }

        private void cboMCP_TOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates MCP tab (scatterplot and MCP textboxes) when user changes the TOD dropdown
            if (okToUpdate)
                updateThe.MCP_TAB(this);
        }

        private void cboMCP_Season_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates MCP tab (scatterplot and MCP textboxes) when user changes the season dropdown
            if (okToUpdate)
                updateThe.MCP_TAB(this);
        }

        private void cboMCPNumSeasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Resets time series and MCP if user changes the number of season bins to use
            if (okToUpdate && metList.isTimeSeries && metList.isMCPd && metList.ThisCount > 0)
            {
                DialogResult result = DialogResult.Yes;

                if (isTest == false)
                {
                    string message = "Changing the number of season bins will reset the MCP and all estimated values. Do you want to continue?";
                    result = MessageBox.Show(message, "", MessageBoxButtons.YesNo);
                }
                
                if (result == DialogResult.Yes)
                {
                    metList.numSeason = Convert.ToInt16(cboMCPNumSeasons.SelectedItem.ToString());

                    ResetTimeSeries();
                    updateThe.AllTABs(this);
                }
                else
                    cboMCPNumSeasons.Text = metList.numSeason.ToString();
            }
            else if (okToUpdate && metList.isTimeSeries && metList.isMCPd == false)
                metList.numSeason = Convert.ToInt16(cboMCPNumSeasons.SelectedItem.ToString());
            else if (metList.ThisCount == 0)
                metList.numSeason = Convert.ToInt16(cboMCPNumSeasons.SelectedItem.ToString());
        }

        private void cboUncertStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates MCP uncertaint step size (i.e. number of months that window is moved on each step). Resets if uncertainty analysis has already been run
            Met thisMet = GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;

            if ((thisMCP.uncertOrtho.Length > 0) || (thisMCP.uncertVarrat.Length > 0) || (thisMCP.uncertBins.Length > 0) || (thisMCP.uncertMatrix.Length > 0))
            {
                bool show_msg = true;

                if (show_msg == true)
                {
                    string message = "Changing the uncertainty step size will reset all uncertainty calculations. Do you want to continue?";

                    DialogResult result = MessageBox.Show(message, "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        thisMCP.uncertStepSize = Convert.ToInt16(cboUncertStep.SelectedItem.ToString());
                        for (int i = 0; i < metList.ThisCount; i++)
                        {
                            if (metList.metItem[i].mcp.uncertOrtho != null)
                                for (int j = 0; j < metList.metItem[i].mcp.uncertOrtho.Length; j++)
                                    metList.metItem[i].mcp.uncertOrtho[0].Clear();

                            if (metList.metItem[i].mcp.uncertVarrat != null)
                                for (int j = 0; j < metList.metItem[i].mcp.uncertVarrat.Length; j++)
                                    metList.metItem[i].mcp.uncertVarrat[0].Clear();

                            if (metList.metItem[i].mcp.uncertMatrix != null)
                                for (int j = 0; j < metList.metItem[i].mcp.uncertMatrix.Length; j++)
                                    metList.metItem[i].mcp.uncertMatrix[0].Clear();

                            if (metList.metItem[i].mcp.uncertBins != null)
                                for (int j = 0; j < metList.metItem[i].mcp.uncertBins.Length; j++)
                                    metList.metItem[i].mcp.uncertBins[0].Clear();
                        }
                    }
                    else
                        cboUncertStep.Text = thisMCP.uncertStepSize.ToString();
                }
            }
            else
                thisMCP.uncertStepSize = Convert.ToInt16(cboUncertStep.SelectedItem.ToString());

            updateThe.ColoredButtons(this);
            updateThe.MCP_TAB(this);
        }

        private void txtWS_PDF_Wgt_TextChanged(object sender, EventArgs e)
        {
            // Called when user changes the WS PDF weight used in Matrix MCP. If Matrix MCP was created, asks user if they want to continue and then resets Matrix MCP (if created) 
            Met thisMet = GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;                      

            if (thisMCP != null)
            {
                if (thisMCP.uncertMatrix.Length > 0 || thisMCP.MCP_Matrix.WS_CDFs != null)
                {
                    DialogResult result = DialogResult.Yes;

                    if (isTest == false)
                        result = MessageBox.Show("Changing the WS matrix weight will reset the Matrix MCP. Do you want to continue?", "Continuum 3.0",
                        MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        ResetTimeSeries();                        
                    }
                    else
                    {
                        txtWS_PDF_Wgt.Text = metList.mcpMatrixWgt.ToString();
                        return;
                    }

                    updateThe.AllTABs(this);
                }

                try
                {
                    metList.mcpMatrixWgt = Convert.ToSingle(txtWS_PDF_Wgt.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid Matrix weight.", "Continuum 3.0");
                    txtWS_PDF_Wgt.Text = metList.mcpMatrixWgt.ToString();
                }
            }
            else
            {
                try
                {
                    metList.mcpMatrixWgt = Convert.ToSingle(txtWS_PDF_Wgt.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid Matrix weight.", "Continuum 3.0");
                    txtWS_PDF_Wgt.Text = metList.mcpMatrixWgt.ToString();
                }
            }

        }

        private void txtLast_WS_Wgt_TextChanged(object sender, EventArgs e)
        {
            // Called when user changes the Last WS (lag) PDF weight used in Matrix MCP. If Matrix MCP was created, asks user if they want to continue and then resets Matrix MCP (if created)
            Met thisMet = GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;

            if (thisMCP != null)
            {
                if (thisMCP.uncertMatrix.Length > 0 || thisMCP.MCP_Matrix.WS_CDFs != null)
                {
                    DialogResult result = DialogResult.Yes;

                    if (isTest == false)
                        result = MessageBox.Show("Changing the WS matrix weight will reset the Matrix MCP. Do you want to continue?", "Continuum 3.0",
                        MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        ResetTimeSeries();                        
                    }
                    else
                    {
                        txtLast_WS_Wgt.Text = metList.mcpLastWS_Wgt.ToString();
                        return;
                    }

                    updateThe.AllTABs(this);
                }

                try
                {
                    metList.mcpLastWS_Wgt = Convert.ToSingle(txtLast_WS_Wgt.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid Matrix weight.", "Continuum 3.0");
                    txtLast_WS_Wgt.Text = metList.mcpLastWS_Wgt.ToString();
                } 
            }
            else
            {
                try
                {
                    metList.mcpLastWS_Wgt = Convert.ToSingle(txtLast_WS_Wgt.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid Matrix weight.", "Continuum 3.0");
                    txtLast_WS_Wgt.Text = metList.mcpLastWS_Wgt.ToString();
                }
            }
        }

        public MERRA GetSelectedMERRA()
        {
            // Returns MERRA object based on what is selected on MERRA tab
            Met thisMet = GetSelectedMet("MERRA");                   
            MERRA thisMERRA = new MERRA();

            if (thisMet.name != null) // it is associated with a met
            {
                UTM_conversion.Lat_Long thisLL = UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                thisMERRA = merraList.GetMERRA(thisLL.latitude, thisLL.longitude);
            }
            else // not associated with a met
            {
                string thisLLStr = cboMERRASelectedMet.SelectedItem.ToString();
                int firstColon = thisLLStr.IndexOf(':');
                int secColon = thisLLStr.LastIndexOf(':');

                if (thisLLStr == "" || thisLLStr == "User-Defined Lat/Long")
                    return thisMERRA;

                double thisLat = Convert.ToDouble(thisLLStr.Substring(firstColon + 2, secColon - firstColon - 6));
                double thisLong = Convert.ToDouble(thisLLStr.Substring(secColon + 2, thisLLStr.Length - secColon - 2));

                thisMERRA = merraList.GetMERRA(thisLat, thisLong);
            }

            TurbineCollection.PowerCurve powerCurve = GetSelectedPowerCurve("MERRA");
            thisMERRA.powerCurve = powerCurve;
            thisMERRA.ApplyPC(ref thisMERRA.interpData.TS_Data);
            thisMERRA.Calc_MonthProdStats(UTM_conversions);
            thisMERRA.CalcAnnualProd(ref thisMERRA.interpData.Annual_Prod, thisMERRA.interpData.Monthly_Prod, UTM_conversions);
            
            return thisMERRA;
        }
        
        public string GetMERRA_SelectedPlotParameter()
        {
            // Returns selected parameter to plot on MERRA tab
            string plotParam = "";

            try
            {
                plotParam = cboMERRA_PlotParam.SelectedItem.ToString();
            }
            catch
            {
                plotParam = "CF (%)";
            }

            return plotParam;
        }         
             

        private void btn_ExportWR_Click(object sender, EventArgs e)
        {
            // Exports MERRA2 wind rose selected on MERRA tab
            Export export = new Export();
            export.ExportMERRA_WindRose(this);
        }
        
        private void cboMERRA_Selected_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update MERRA2 tab based on selected month
            if (okToUpdate)
                updateThe.MERRA_TAB(this);
        }

        private void cboMERRA_PlotParam_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update MERRA2 tab based on selected plot parameter
            if (okToUpdate)                            
                updateThe.MERRA_TAB(this);
                            
        }

        private void btn_Export_All_Months_All_Years_Click(object sender, EventArgs e)
        {
            // Exports all months and all years of MERRA2 interpolated data
            Export export = new Export();
            export.ExportMERRA_All_Months_All_Years(this);
        }

        private void btn_Export_Interp_Click(object sender, EventArgs e)
        {
            // Exports interpolated hourly MERRA2 data
            Export export = new Export();
            export.ExportMERRA_Interp_Data(this);
        }

        private void cboNumMERRA_Nodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates number of MERRA2 nodes to interpolate between (1, 4, or 16). Resets merraList if have already imported MERRA2 data
            if (merraList.numMERRA_Data > 0 && okToUpdate)
            {
                DialogResult goodToGo = DialogResult.Yes;

                if (isTest == false)
                    goodToGo = MessageBox.Show("Changing the number of MERRA2 nodes to use will reset all time series estimates. Do you want to continue?",
                        "Continuum 3.0", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    merraList.Set_Num_MERRA_Nodes(this); // Clears all MERRA2 data. TO DO: Update extract button or automatically reload 
                    ResetTimeSeries();
                    updateThe.AllTABs(this);
                }
                else
                {
                    okToUpdate = false;
                    if (merraList.numMERRA_Nodes == 1) cboNumMERRA_Nodes.SelectedIndex = 0;
                    if (merraList.numMERRA_Nodes == 4) cboNumMERRA_Nodes.SelectedIndex = 1;
                    if (merraList.numMERRA_Nodes == 16) cboNumMERRA_Nodes.SelectedIndex = 2;
                    okToUpdate = true;
                }
                    
            }
            else
                merraList.Set_Num_MERRA_Nodes(this);
        }

        public TurbineCollection.PowerCurve GetSelectedPowerCurve(string TAB_name)
        {
            // Returns Power Curve object selected on specified tab
            TurbineCollection.PowerCurve thisPowerCurve = new TurbineCollection.PowerCurve();
            string powerCurveString = "";
            try
            {
                if (TAB_name == "MERRA")
                    powerCurveString = cboMERRA_PowerCurves.SelectedItem.ToString();
                else if (TAB_name == "Gross")
                    powerCurveString = cboPowerCrvs.SelectedItem.ToString();
                else if (TAB_name == "Uncertainty")
                    powerCurveString = cboUncertPowerCrv.SelectedItem.ToString();
                else if (TAB_name == "Site Suitability")
                    powerCurveString = cboSiteSuitPowerCurve.SelectedItem.ToString();
                else if (TAB_name == "Monthly")
                    powerCurveString = cboMonthlyPowerCurve.SelectedItem.ToString();                
                else if (TAB_name == "Turbulence")
                    powerCurveString = cboTurbPowerCurve.SelectedItem.ToString();
            }
            catch
            {                
            }                     

            if (powerCurveString != "No Power Curves Imported")
            {
                for (int i = 0; i < turbineList.PowerCurveCount; i++)
                {
                    thisPowerCurve = turbineList.powerCurves[i];
                    if (thisPowerCurve.name == powerCurveString)
                        break;
                }
            }
            
            return thisPowerCurve;
        }

        public void ImportZones(string fileName)
        {           
                       
            siteSuitability.mapMinBounds = new TopoInfo.UTM_X_Y();
            siteSuitability.mapMaxBounds = new TopoInfo.UTM_X_Y();
                            
            SetDefaultFolderLocations(fileName);

            StreamReader sr;
            try
            {
                sr = new StreamReader(fileName);
            }
            catch
            {
                MessageBox.Show("Error opening file. Make sure that it's not open in another program.");
                return;
            }

            char[] delims = new char[2];
            delims[0] = '\t';
            delims[1] = ',';

            int lineNum = 0;

            while (sr.EndOfStream == false)
            {
                string dataStr = sr.ReadLine();
                char[] Trim_Chars = new char[2];
                Trim_Chars[0] = ',';
                Trim_Chars[1] = '\t';
                dataStr = dataStr.Trim(Trim_Chars);
                string[] fileRow = dataStr.Split(delims);
                lineNum++;

                int numZones = siteSuitability.GetNumZones();

                if (fileRow.Length >= 5)
                {
                    bool isDuplicate = false;

                    for (int i = 0; i < siteSuitability.GetNumZones(); i++)
                        if (fileRow[0] == siteSuitability.zones[i].name)
                            isDuplicate = true;

                    if (isDuplicate == true)
                        MessageBox.Show("Zone with same name already imported.", "Continuum 3.0");
                    else
                    {

                        Array.Resize(ref siteSuitability.zones, numZones + 1);

                        try
                        {
                            siteSuitability.zones[numZones].name = fileRow[0];
                        }
                        catch
                        {
                            MessageBox.Show("Error reading in zone name at line: " + lineNum + ". Format should be: Name, Lat, Long, XSize, YSize");
                            Array.Resize(ref siteSuitability.zones, numZones);
                            return;
                        }

                        try
                        {
                            siteSuitability.zones[numZones].latitude = Convert.ToDouble(fileRow[1]);
                        }
                        catch
                        {
                            MessageBox.Show("Error reading in zone latitude at line: " + lineNum + ". Format should be: Name, Lat, Long, XSize, YSize");
                            Array.Resize(ref siteSuitability.zones, numZones);
                            return;
                        }

                        try
                        {
                            siteSuitability.zones[numZones].longitude = Convert.ToDouble(fileRow[2]);
                        }
                        catch
                        {
                            MessageBox.Show("Error reading in zone longitude at line: " + lineNum + ". Format should be: Name, Lat, Long, XSize, YSize");
                            Array.Resize(ref siteSuitability.zones, numZones);
                            return;
                        }

                        try
                        {
                            siteSuitability.zones[numZones].xSize = Convert.ToInt16(fileRow[3]);
                        }
                        catch
                        {
                            MessageBox.Show("Error reading in zone X size at line: " + lineNum + ". Format should be: Name, Lat, Long, XSize, YSize");
                            Array.Resize(ref siteSuitability.zones, numZones);
                            return;
                        }

                        try
                        {
                            siteSuitability.zones[numZones].ySize = Convert.ToInt16(fileRow[4]);
                        }
                        catch
                        {
                            MessageBox.Show("Error reading in zone Y size at line: " + lineNum + ". Format should be: Name, Lat, Long, XSize, YSize");
                            Array.Resize(ref siteSuitability.zones, numZones);
                            return;
                        }
                    }
                }
            }
            sr.Close();

            if (topo.gotTopo)
            {
                topo.GetElevsAndSRDH_ForCalcs(this, null, false);

                for (int i = 0; i < siteSuitability.GetNumZones(); i++)
                {
                    UTM_conversion.UTM_coords theseUTM = UTM_conversions.LLtoUTM(siteSuitability.zones[i].latitude, siteSuitability.zones[i].longitude);
                    siteSuitability.zones[i].elev = topo.CalcElevs(theseUTM.UTMEasting, theseUTM.UTMNorthing);
                }
            }

            updateThe.ZoneList(this);
            updateThe.SiteSuitabilityDropdown(this, null);
            updateThe.ColoredButtons(this);
            updateThe.SiteSuitabilityTAB(this);
            ChangesMade();
                        
        }

        private void btnImportZones_Click(object sender, EventArgs e)
        {
            // Open .csv or .txt file with zone (i.e. houses, buildings) coordinates and X/Y size
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy())
            {
                MessageBox.Show("Cannot import zones while calculations are under way.", "Continuum 3");
                return;
            }

            if (siteSuitability.flickerMap.Length != 0)
            {
                DialogResult goodToGo = MessageBox.Show("Importing zones will reset the shadow flicker model. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);
                if (goodToGo == DialogResult.No)
                    return;
                else
                    siteSuitability.ClearShadowFlicker();
            }

            if (ofdZones.ShowDialog() == DialogResult.OK)
                ImportZones(ofdZones.FileName);

            
        }

        private void btnRunIceThrow_Click(object sender, EventArgs e)
        {
            // Calls ice throw model calcs (in backgound worker)
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy()) {
                MessageBox.Show("Cannot generate an ice throw model while other calculations are under way.", "Continuum 3");
                return;
            }

            TurbineCollection.PowerCurve powerCurve = GetSelectedPowerCurve("Site Suitability");

            if (powerCurve.name == null)
            {
                MessageBox.Show("A power curve is needed to run the ice throw model.", "Continuum 3");
                return;
            }

            if (turbineList.TurbineCount == 0)
            {
                MessageBox.Show("Turbine sites are needed to run the ice throw model.", "Continuum 3");
                return;
            }

            int numIceThrowsPerDay = GetNumIceThrowsPerDay();
            int numIceDaysPerYear = GetNumIcingDays();

            if (numIceThrowsPerDay != -999 && numIceDaysPerYear != -999)
            {
                siteSuitability.numIceDaysPerYear = numIceDaysPerYear;
                siteSuitability.iceThrowsPerIceDay = numIceThrowsPerDay;

                BackgroundWork.Vars_for_BW varsForBW = new BackgroundWork.Vars_for_BW();
                varsForBW.thisInst = this;
                BW_worker = new BackgroundWork();
                BW_worker.Call_BW_IceThrow(varsForBW);
            }
            else // reset textboxes
            {
                okToUpdate = false;
                txtNumIceThrowsPerDay.Text = siteSuitability.iceThrowsPerIceDay.ToString();
                txtNumIceDays.Text = siteSuitability.numIceDaysPerYear.ToString();
                okToUpdate = true;
            }                      
                        
        }

        private void btnRunShadowFlicker_Click(object sender, EventArgs e)
        {
            // Calls Run Shadow Flicker model
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy())
            {
                MessageBox.Show("Cannot generate a shadow flicker model while other calculations are under way.", "Continuum 3");
                return;
            }                        

            if (turbineList.TurbineCount == 0)
            {
                MessageBox.Show("Turbine sites are needed to run the shadow flicker model", "Continuum 3");
                return;
            }

            siteSuitability.RunShadowFlickerModel(this);

        }

        private void cboMERRASelectedMet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected MERRA data based on selected met and update plots and tables
            if (okToUpdate == true)                            
                updateThe.MERRA_TAB(this);
            
        }

        private void cboMetQC_SelectedMet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates Met Data QC tab with newly selected met
            if (okToUpdate == true)
                updateThe.MetDataQC_TAB(this);
        }

        private void cboMCP_Met_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates MCP tab with newly selected met
            if (okToUpdate == true)
                updateThe.MCP_TAB(this);
        }

        private void cboSensorHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates Met Data QC plots based on selected sensor height
            if (okToUpdate == true)
            {
                Met thisMet = GetSelectedMet("Met Data QC");                
                updateThe.MetAnemScatterplot(this, thisMet);
                updateThe.MetWS_DiffvsWD(this, thisMet);
                updateThe.MetWS_DiffvsWindSpeed(this, thisMet);
                updateThe.MetWindRoseOrWS_byWDPlot(this, thisMet);
            }
        }

        private void cboFilt_or_Not_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates plots on Met Data QC plots based on selected: filtered vs unfiltered
            if (okToUpdate == true)
            {
                Met thisMet = GetSelectedMet("Met Data QC");
                updateThe.MetAnemScatterplot(this, thisMet);
                updateThe.MetWS_DiffvsWD(this, thisMet);
                updateThe.MetWS_DiffvsWindSpeed(this, thisMet);
            }
        }

        private void dateMERRAStart_ValueChanged(object sender, EventArgs e)
        {
            // Changes the start date of the MERRA2 data. If MERRA2 data has already been imported, asks the user if they want to continue and clears MERRA2 data if they do
            if (merraList.numMERRA_Data > 1 && metList.isTimeSeries == true && metList.isMCPd == true && okToUpdate)
            {
                DialogResult goodToGo = MessageBox.Show("Resetting the MERRA2 start date will delete all imported MERRA2 data and will reset time series model calculations. Do " +
                    "you want to continue?", "Continuum 3.0", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    merraList.ClearMERRA();
                    merraList.ClearMERRA_Data(this);
                    merraList.DeleteAllMERRADataFromDB(this);
                    ResetTimeSeries();

                    merraList.startDate = dateMERRAStart.Value;
                    ChangesMade();
                }
                else
                {
                    okToUpdate = false;
                    dateMERRAStart.Value = merraList.startDate;
                    okToUpdate = true;
                }
            }
            else
                merraList.startDate = dateMERRAStart.Value;
        }

        public void ResetTimeSeries()
        {
            // Resets all time series estimates
            metList.DeleteAllTimeSeriesEsts(this);
            metList.isMCPd = false;

            for (int i = 0; i < metList.ThisCount; i++)
            {
                metList.metItem[i].mcp = null;

                if (metList.metItem[i].metData.GetNumAnems() > 0)
                    if (metList.metItem[i].metData.anems[0].windData == null)
                        metList.metItem[i].metData.GetSensorDataFromDB(this, metList.metItem[i].name);

                metList.metItem[i].CalcAllMeas_WSWD_Dists(this, metList.metItem[i].metData.GetSimulatedTimeSeries(modeledHeight));
            }               

            turbineList.ClearAllWSEsts();
            turbineList.ClearAllGrossEsts();
            turbineList.ClearAllNetEsts();

            mapList.ClearAllMaps();
            wakeModelList.ClearWakeMaps();

            metPairList.ClearAll();
            modelList.ClearAll();
        }

        private void dateMERRAEnd_ValueChanged(object sender, EventArgs e)
        {
            // Changes the end date of the MERRA2 data. If MERRA2 data has already been imported, asks the user if they want to continue and clears MERRA2 data if they do
            if (merraList.numMERRA_Data > 1 && metList.isTimeSeries == true && okToUpdate)
            {
                DialogResult goodToGo = MessageBox.Show("Resetting the MERRA2 end date will delete all imported MERRA2 data and will reset time series model calculations. Do " +
                    "you want to continue?", "Continuum 3.0", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    merraList.ClearMERRA();
                    ResetTimeSeries();
                    merraList.DeleteAllMERRADataFromDB(this);
                    merraList.endDate = dateMERRAEnd.Value;
                }
                else
                {
                    okToUpdate = false;
                    dateMERRAEnd.Value = merraList.endDate;
                    okToUpdate = true;
                }
            }
            else
                merraList.endDate = dateMERRAEnd.Value;

        }               

        private void cboMERRA_PowerCurves_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates MERRA2 tab based on selected power curve
            if (okToUpdate)
            {
                updateThe.MERRA_AnnualTableAndPlot(this);
                updateThe.MERRA_MonthlyTableAndPlot(this);
                updateThe.MERRA_Textboxes(this);
            }            
        }

        private void cboMERRAYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates wind rose plot on MERRA2 tab based on selected year
            if (okToUpdate)
                updateThe.MERRA_WindRosePlot(this);
        }

        private void cboMERRA_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates wind rose plot on MERRA2 tab based on selected month
            if (okToUpdate)
                updateThe.MERRA_WindRosePlot(this);
        }

        private void chkYearsToDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates MERRA2 tab based on which years are selected from checked list
            if (okToUpdate)
                updateThe.MERRA_TAB(this);
        }

        private void chkYearsToDisplayAll_CheckedChanged(object sender, EventArgs e)
        {
            // Selects or deselects all years in list and then updates the MERRA2 tab
            if (Created && okToUpdate)
            {
                if (chkYearsToDisplay.Items.Count > 0)
                {
                    if (chkYearsToDisplayAll.CheckState == CheckState.Checked)
                    {
                        for (int i = 0; i < chkYearsToDisplay.Items.Count; i++)
                            chkYearsToDisplay.SetItemChecked(i, true);
                    }
                    else
                    {
                        for (int i = 0; i < chkYearsToDisplay.Items.Count; i++)
                            chkYearsToDisplay.SetItemChecked(i, false);
                    }
                }

                updateThe.MERRA_TAB(this);
            }
        }

        private void btnImportCRV_MERRA_Click(object sender, EventArgs e)
        {
            // Imports power curve (called from MERRA2 tab "Import Power Curve" button) 
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy())
            {
                MessageBox.Show("Cannot import a power curve while calculations are under way.", "Continuum 3");
                return;
            }

            if (ofdPowerCurve.ShowDialog() != DialogResult.OK)
                return;

            double RD = Convert.ToSingle(Interaction.InputBox("What is the rotor diameter [m] of the turbine?", "Continuum 3"));
            double RPM = Convert.ToSingle(Interaction.InputBox("What is the rated RPM of the turbine?", "Continuum 3"));

            turbineList.ImportPowerCurve(this, RD, RPM);
        }

        private void cboSelectedTurbine_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates "Time Series Analysis" tab based on selected turbine
            if (okToUpdate)
                updateThe.Monthly_TAB(this);
        }

        private void chkSelectAllTurbineYears_CheckedChanged(object sender, EventArgs e)
        {
            // Selects or deselects all years in list on "Time Series Analysis" tab and then updates Time Series Analysis tab
            if (Created)
            {
                if (chkYears_Monthly.Items.Count > 0)
                {
                    if (chkSelectAllTurbineYears.CheckState == CheckState.Checked)
                    {
                        for (int i = 0; i < chkYears_Monthly.Items.Count; i++)
                            chkYears_Monthly.SetItemChecked(i, true);
                    }
                    else
                    {
                        for (int i = 0; i < chkYears_Monthly.Items.Count; i++)
                            chkYears_Monthly.SetItemChecked(i, false);
                    }
                }

                updateThe.Monthly_TAB(this);
            }
        }

        private void btnExportYearlyTurbineValues_Click(object sender, EventArgs e)
        {
            // Exports annual estimates at turbine site selected on "Time Series Analysis" tab
            Export export = new Export();
            export.ExportYearlyTurbineValues(this);
        }

        private void btnExportHourlyTurbineValues_Click(object sender, EventArgs e)
        {
            // Exports hourly estimates at turbine site selected on "Time Series Analysis" tab
            Export export = new Export();
            export.ExportHourlyTurbineValues(this);
        }

        private void cboMonthlyPowerCurve_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the wake model dropdown list (on "Time Series Analysis" tab) so only wake models that use selected power curve are shown and updates Time Series tab
            updateThe.WakeModelList(this);
            updateThe.Monthly_TAB(this);
        }

        private void chkYears_Monthly_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates monthly plot and table on "Time Series Analysis" tab based on years selected in checked list
            if (okToUpdate)
                updateThe.TurbineMonthlyPlot(this);
        }

        private void chkSelectedTurbineParam_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates plots and tables on "Time Series Analysis" tab based on plot parameters chosen from checked list
            if (okToUpdate)
                updateThe.Monthly_TAB(this);
        }

        private void cboSiteSuitabilitySelectPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates "Site Suitability" tab based on which model is selected from dropdown menu
            if (okToUpdate)
                updateThe.SiteSuitabilityTAB(this);
        }

        private void btnDelZones_Click(object sender, EventArgs e)
        {
            // Deletes zones selected by user on "Site Suitability" tab
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy())
            {
                MessageBox.Show("Cannot delete zones while calculations are under way.", "Continuum 3");
                return;
            }

            int numZonesToDelete = lstZones.SelectedItems.Count;
            DialogResult goodToGo = MessageBox.Show("Are you sure you want to delete these " + numZonesToDelete + " zones?", "Continuum 3", MessageBoxButtons.YesNo);

            ListView.SelectedListViewItemCollection SelectedZones = lstZones.SelectedItems;

            if (goodToGo == DialogResult.Yes)
            {
                for (int i = 0; i < numZonesToDelete; i++)
                {                    
                    string zoneName = SelectedZones[i].Text;
                    siteSuitability.DeleteZone(zoneName);
                }
                ChangesMade();
            }

            // If sound map has been created, recreate using new bounds
            if (siteSuitability.soundMap != null)
                siteSuitability.CreateSoundMap(this);
            
            updateThe.ZoneList(this);            
            updateThe.SiteSuitabilityTAB(this);            
        }               

        private void btnSiteSuitImportCRV_Click(object sender, EventArgs e)
        {
            // Imports power curve (called from Site Suitability tab "Import Power Curve" button)
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy())
            {
                MessageBox.Show("Cannot import a power curve while calculations are under way.", "Continuum 3");
                return;
            }

            if (ofdPowerCurve.ShowDialog() != DialogResult.OK)
                return;

            double RD = Convert.ToSingle(Interaction.InputBox("What is the rotor diameter [m] of the turbine?", "Continuum 3"));
            double RPM = Convert.ToSingle(Interaction.InputBox("What is the rated RPM of the turbine?", "Continuum 3"));

            turbineList.ImportPowerCurve(this, RD, RPM);
        }

        private void cboSiteSuitMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates Shadow Flicker surface plot based on selected month on "Site Suitability" tab
            updateThe.ShadowFlickerSurfacePlot(this);
        }

        private void cboSiteSuitHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates Shadow Flicker surface plot based on selected hour on "Site Suitability" tab
            updateThe.ShadowFlickerSurfacePlot(this);
        }              

        private void cboZoneList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates Site Suitability plots and tables based on selected zone from dropdown menu
            if (okToUpdate)
            {
                string selectedModel = GetSelectedSiteSuitabilityModel();

                if (selectedModel == "Shadow Flicker")
                {
                    updateThe.ShadowFlicker12x24(this);
                    updateThe.ShadowFlickerMaxDay(this);
                }                    
                else if (selectedModel == "Ice Throw")
                {
                    if (cboIceDistORIceHisto.SelectedItem.ToString() == "Ice Hit vs. Distance")
                    {
                        updateThe.IceHitsVsDistancePlot(this);
                        updateThe.IceHitVsDistTable(this);
                    }                        
                    else
                        updateThe.IceHitHistogram(this);                                       
                    
                }                
            }                
        }

        private void cboIcingYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates ice throw plots and tables on Site Suitability tab based on selected year of icing
            if (okToUpdate)
            {
                updateThe.IceThrowSurfacePlot(this);
                updateThe.IceHitsByZone(this);
                
                if (cboIceDistORIceHisto.SelectedItem.ToString() == "Ice Hit vs. Distance")
                {
                    updateThe.IceHitsVsDistancePlot(this);
                    updateThe.IceHitVsDistTable(this);
                }
            }
                   
        }               

        private void btnRunSoundModel_Click(object sender, EventArgs e)
        {
            // Runs sound model and updates site suitability tab
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy())
            {
                MessageBox.Show("Cannot generate a turbine noise model while other calculations are under way.", "Continuum 3");
                return;
            }

            if (turbineList.TurbineCount == 0)
            {
                MessageBox.Show("Turbine sites are needed to run the sound model", "Continuum 3");
                return;
            }

            double turbineSound = GetTurbineNoise();

            if (turbineSound != -999)
            {
                siteSuitability.turbineSound = turbineSound;
                siteSuitability.CreateSoundMap(this);
                updateThe.SoundMap(this);
                updateThe.SoundAtZones(this);
                updateThe.SiteSuitabilityDropdown(this, "Sound");
                updateThe.ColoredButtons(this);
                updateThe.SiteSuitabilityVisibility(this);
            }
            
        }

        private void btn_AddProj_Click(object sender, EventArgs e)
        {
            // Adds a new exceedance curve to list of all turbine's exceedance
            // If net estimates have been formed, ask user before clearing net ests and waked maps
            
            if (wakeModelList.NumWakeModels > 0)
            {
                DialogResult goodToGo = MessageBox.Show("Adding a new exceedance curve will clear all turbine net estimates and waked maps. Do you " +
                    "want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.No)
                    return;
                else
                {
                    wakeModelList.ClearAll();
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
            }

            AddPFCurve Add_newCurve = new AddPFCurve();

            // Update list in dropdown to only show curves that have yet to be defined
            Add_newCurve.UpdateDropdown(turbineList.exceed.exceedCurves);

            turbineList.exceed.SizeMonteCarloArrays(); // resets the composite value arrays
            
            Add_newCurve.ShowDialog();

            if (Add_newCurve.goodToGo == true)
            {
                Add_Exceedance newCurve = new Add_Exceedance();

                string thisExceed = Add_newCurve.cboCurves.SelectedItem.ToString();

                newCurve.txtExceed.Text = thisExceed;
                bool isNew = true;

                // If Exceed curve has already been defined, fill in lower/upper bounds and modes
                for (int i = 0; i < turbineList.exceed.Num_Exceed(); i++)
                    if (turbineList.exceed.exceedCurves[i].exceedStr == thisExceed)
                    {
                        MessageBox.Show("Curve is already defined.");
                        isNew = false;
                    }

                if (isNew == true)
                {
                    newCurve.ShowDialog();

                    // if new (and valid) exceedance was added then add to list
                    if (isNew == true && newCurve.DialogResult == DialogResult.OK)
                    {      
                        Array.Resize(ref turbineList.exceed.exceedCurves, turbineList.exceed.Num_Exceed() + 1);
                        turbineList.exceed.exceedCurves[turbineList.exceed.Num_Exceed() - 1] = newCurve.thisExceed;
                    }
                }

            }

            // Update list of exceedance curves
            updateThe.Exceedance_TAB(this);
            ChangesMade();
        }

        private void btnDoMonteCarlo_Click(object sender, EventArgs e)
        {
            // Calls Background Worker and runs Exceedance Monte Carlo model
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy())
            {
                MessageBox.Show("Cannot perform Monte Carlo while other calculations are underway.", "Continuum 3");
                return;
            }

            if (wakeModelList.NumWakeModels > 0)
            {
                DialogResult goodToGo = MessageBox.Show("Running the Monte Carlo model will clear all turbine net estimates and waked maps. Do you " +
                    "want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.No)
                    return;
                else
                {
                    wakeModelList.ClearAll();
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
            }

            BackgroundWork.Vars_for_BW varsForBW = new BackgroundWork.Vars_for_BW();
            varsForBW.thisInst = this;
            BW_worker = new BackgroundWork();
            BW_worker.Call_BW_Exceed(varsForBW);

        }

        private void btnExport_P_Vals_Click(object sender, EventArgs e)
        {
            // Exports P1/P10/P50/P90/P99 exceedance estimates to .csv
            Export export = new Export();
            export.Export_P_tables_and_Curves(false, true, false, this);
        }

        private void btnExportAllPVals_Click(object sender, EventArgs e)
        {
            // Exports all P values (i.e. P1 - P99) exceedance estimates to .csv
            Export export = new Export();
            export.Export_P_tables_and_Curves(false, false, true, this);
        }

        private void btnDeleteExceed_Click(object sender, EventArgs e)
        {
            // If net estimates exist, asks user if they want to continue and clears net estimates and waked map if they do. Deletes selected exceedance curve, 
            // resets Monte Carlo and updates plots
            
            DialogResult result = new DialogResult();

            if (wakeModelList.NumWakeModels > 0)
            {
                result = MessageBox.Show("Deleting an exceedance curve will clear all turbine net estimates and waked maps. Do you" +
                    "want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                    return;
                else
                {
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
            }

            if (turbineList.exceed.compositeLoss.isComplete)            
                result = MessageBox.Show("Deleting an exceedance curve will reset the Monte Carlo simulation. Do you want to continue?", "", MessageBoxButtons.YesNo);            
            else            
                result = MessageBox.Show("Are you sure you want to delete this exceedance curve?", "", MessageBoxButtons.YesNo);
            
            if (result == DialogResult.Yes)
            {                
                int numToDelete = lstDefinedLosses.SelectedItems.Count;
                
                if (numToDelete == 0)
                {
                    MessageBox.Show("Select an exceedance curve to delete.", "", MessageBoxButtons.OK);
                    return;
                }
                                                
                for (int i = 0; i < numToDelete; i++)
                    turbineList.exceed.Delete_Exceed(lstDefinedLosses.SelectedItems[i].Text);

                turbineList.ClearAllExceedance();
                
                updateThe.Exceedance_TAB(this);
                ChangesMade();
            }
        }               

        private void btn_editloss_Click(object sender, EventArgs e)
        {
            // Edits an exceedance curve settings 
            // If a Monte Carlo has already been run, this prompts the user to make sure and then clears the Monte Carlo
                        
            if (turbineList.exceed.compositeLoss.isComplete)
            {
                DialogResult result = MessageBox.Show("An exceedance table has already been calculated for these curves. Changing or adding one will reset the Monte Carlo simulation. Do you want to continue?", "Monte Carlo simulation will reset. Are you sure?", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)                
                    return;                
                else                
                    turbineList.ClearAllExceedance();                
            }

            if (wakeModelList.NumWakeModels > 0)
            {
                DialogResult goodToGo = MessageBox.Show("Changing exceedance curve will clear all turbine net estimates and waked maps. Do you" +
                    "want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.No)
                    return;
                else
                {
                    wakeModelList.ClearAll();
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
            }

            Add_Exceedance newCurve = new Add_Exceedance();
            
            string thisExceed = "";

            try
            {
                if (lstDefinedLosses.SelectedItems.Count > 1)
                {
                    MessageBox.Show("You can only edit one curve at a time. Select one.");
                    return;
                }
                else if (lstDefinedLosses.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Select a curve from the list to edit.");
                    return;
                }
                else                
                    thisExceed = lstDefinedLosses.SelectedItems[0].Text;               

            }
            catch
            {
                MessageBox.Show("Select a curve from the list to edit.");
                return;
            }

            newCurve.txtExceed.Text = thisExceed;
                        
            // Fill in lower/upper bounds and modes
            for (int i = 0; i < turbineList.exceed.Num_Exceed(); i++)
            {
                if (turbineList.exceed.exceedCurves[i].exceedStr == thisExceed)
                {
                    newCurve.thisExceed = turbineList.exceed.exceedCurves[i];

                    newCurve.txt_LowerBound.Text = Math.Round(newCurve.thisExceed.lowerBound * 100,3).ToString();
                    newCurve.txt_UpperBound.Text = Math.Round(newCurve.thisExceed.upperBound * 100, 3).ToString();

                    newCurve.Update_Mode_List();
                    newCurve.Update_plot();                    
                }
            }

            newCurve.ShowDialog();

            if (newCurve.goodToGo)            
                for (int i = 0; i < turbineList.exceed.Num_Exceed(); i++)                
                    if (turbineList.exceed.exceedCurves[i].exceedStr == thisExceed)                    
                        turbineList.exceed.exceedCurves[i] = newCurve.thisExceed;                    
            
            // Update list of exceedance curves
            updateThe.Exceedance_TAB(this);
            ChangesMade();
        }

        private void btnExportCurves_Click(object sender, EventArgs e)
        {
            // Exports all defined exceedance curves
            Export export = new Export();
            export.Export_P_tables_and_Curves(true, false, false, this);
        }

        private void chkShowPDF_CheckedChanged(object sender, EventArgs e)
        {
            // Updates Exceedance plot to show PDFs (probability density functions) of selected exceedance curves
            updateThe.PerformanceFactorsPlot(this);
        }

        private void chkShowCDFs_CheckedChanged(object sender, EventArgs e)
        {
            // Updates Exceedance plot to show CDFs (cumulative distribution functions) of selected exceedance curves
            updateThe.PerformanceFactorsPlot(this);
        }

        private void cboExceedTurbine_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the Exceedance tab plots and tables based on selected turbine
            if (okToUpdate)
                updateThe.Exceedance_TAB(this);
        }

        private void cboExceedPowerCurve_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the Exceedance tab plots and tables based on selected power curve
            if (okToUpdate)
                updateThe.Exceedance_TAB(this);
        }

        private void cboExceedWake_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the Exceedance tab plots and tables based on selected wake model
            if (okToUpdate)
                updateThe.Exceedance_TAB(this);
        }               

        private void btnExportIceSummary_Click(object sender, EventArgs e)
        {
            // Exports the coordinates of all ice hits from ice throw model on Site Suitability tab
            Export export = new Export();
            export.ExportIceHitCoordinates(this);
        }

        public string GetSelectedSiteSuitabilityModel()
        {
            // Returns name of model selected on Site Suitability tab
            string thisModel = "";

            if (cboSiteSuitabilitySelectPlot.Items.Count > 0)
            {
                if (cboSiteSuitabilitySelectPlot.SelectedItem == null)
                    cboSiteSuitabilitySelectPlot.SelectedIndex = 0;

                if (cboSiteSuitabilitySelectPlot.SelectedItem.ToString() == "Ice Throw")
                    thisModel = "Ice Throw";
                else if (cboSiteSuitabilitySelectPlot.SelectedItem.ToString() == "Shadow Flicker")
                    thisModel = "Shadow Flicker";
                else if (cboSiteSuitabilitySelectPlot.SelectedItem.ToString() == "Sound")
                    thisModel = "Sound";
            }

            return thisModel;
        }

        private void btnExportIceVsDist_Click(object sender, EventArgs e)
        {
            // Exports number of ice hits versus distance
            Export export = new Export();
            export.exportIceVsDistance(this);
        }

        private void txtNumIceDays_TextChanged(object sender, EventArgs e)
        {
            // Changes number of icing days assumed per year in ice throw model

            if (okToUpdate && siteSuitability.yearlyIceHits.Length != 0)
            {
                DialogResult goAhead = MessageBox.Show("Changing the number of icing days will reset the ice throw model. Do you want to continue?", "Continuum 3.0", MessageBoxButtons.YesNo);

                if (goAhead == DialogResult.Yes)
                {
                    siteSuitability.ClearIceThrow();
                    updateThe.IceThrowPlotsAndTables(this);
                    updateThe.ColoredButtons(this);
                }
                else
                {
                    okToUpdate = false;
                    txtNumIceDays.Text = siteSuitability.numIceDaysPerYear.ToString();
                    okToUpdate = true;
                }                    
            }                
        }

        public int GetNumIcingDays()
        {
            // Returns number of icing days / year specified on Site Suitability tab
            int numIcingDays = 0;

            try
            {
                numIcingDays = Convert.ToInt16(txtNumIceDays.Text);
            }
            catch
            {
                MessageBox.Show("Invalid number of icing days.", "Continuum 3");
                numIcingDays = -999;
            }

            if (numIcingDays < 0 || numIcingDays > 365)
            {
                MessageBox.Show("Invalid number of icing days.", "Continuum 3");
                numIcingDays = -999;
            }                

            return numIcingDays;
        }

        public int GetNumIceThrowsPerDay()
        {
            // Returns number of ice throws / ice day specified on Site Suitability tab
            int numIceThrowsPerDay = 0;

            try
            {
                numIceThrowsPerDay = Convert.ToInt16(txtNumIceThrowsPerDay.Text);
            }
            catch
            {
                MessageBox.Show("Invalid number of ice throws per day.", "Continuum 3");
                numIceThrowsPerDay = -999;
            }

            if (numIceThrowsPerDay < 0 || numIceThrowsPerDay > 1000000)
            {
                MessageBox.Show("Invalid number of ice throws per day.", "Continuum 3");
                numIceThrowsPerDay = -999;
            }                

            return numIceThrowsPerDay;
        }

        public double GetTurbineNoise()
        {
            // Gets and returns turbine noise level in dBA
            double thisNoise = 0;

            try
            {
                thisNoise = Convert.ToDouble(txtTurbineNoise.Text);
            }
            catch
            {
                MessageBox.Show("Invalid turbine noise level entered.", "Continuum 3");
                thisNoise = -999;
            }

            return thisNoise;
        }

        private void btnExportShadowFlicker_Click(object sender, EventArgs e)
        {
            // Exports Shadow Flicker 12x24 hours for all zones
            Export export = new Export();
            export.ExportShadowFlicker12x24(this);
        }

        private void btnExportSoundSummary_Click(object sender, EventArgs e)
        {
            // Exports Sound Levels at Zones
            Export export = new Export();
            export.ExportSoundZones(this);
        }               

        private void cboIceDistORIceHisto_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates Site Suitability tab to show either ice hit vs Distance plot or histogram of annual ice hits at selected zone
            if (okToUpdate)
            {
                string plotSelected = cboIceDistORIceHisto.SelectedItem.ToString();

                if (plotSelected == "Ice Hit vs. Distance")
                {
                    okToUpdate = false;
                    cboZoneList.Items.Clear();
                    for (int i = 0; i < 16; i++)
                        cboZoneList.Items.Add(Math.Round((double)i * 360 / metList.numWD, 1));

                    cboZoneList.Items.Add("All");
                    cboZoneList.SelectedIndex = 16;
                    okToUpdate = true;

                    updateThe.IceHitsVsDistancePlot(this);
                }                    
                else
                {
                    okToUpdate = false;
                    cboZoneList.Items.Clear();
                    for (int i = 0; i < siteSuitability.GetNumZones(); i++)
                        cboZoneList.Items.Add(siteSuitability.zones[i].name);

                    cboZoneList.SelectedIndex = 0;
                    okToUpdate = true;

                    updateThe.IceHitHistogram(this);
                }
                    
            }
        }

        private void cboTurbWD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates turbulence intensity plot and table on Site Conditions tab based on selected wind direction
            if (okToUpdate)
                updateThe.TurbulenceIntensityPlotAndTable(this);
        }

        private void cboTurbMet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates met start/end date boxes and turbulence intensity plots and tables based on met selected under "Turbulence Intensity" on Site Conditions tab
            if (okToUpdate)
            {
                updateThe.SiteConditionsMetDates(this);
                updateThe.TurbulenceIntensityPlotAndTable(this);
            }
                
        }

        private void cboTI_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates turbulence intensity plots and tables on Site Conditions tab based on selected type of TI (average, representative, or effective)
            if (okToUpdate)
            {
                if (cboTI_Type.SelectedItem.ToString() == "Effective" && (turbineList.TurbineCount == 0 || turbineList.PowerCurveCount == 0))
                {
                    MessageBox.Show("Turbine sites and a power curve are required to conduct effective TI calculations.", "Continuum 3.0");
                    cboTI_Type.SelectedIndex = 0;
                    return;
                }

                if (cboTI_Type.SelectedItem.ToString() != "Effective")
                    cboEffectiveTI_m.Enabled = false;
                else
                    cboEffectiveTI_m.Enabled = true;

                updateThe.TurbulenceIntensityPlotAndTable(this);
            }
                
        }

        private void dateTIEnd_ValueChanged(object sender, EventArgs e)
        {
            // Updates turbulence intensity plots and tables based on newly selected end date in TI section of Site Conditions tab
            if (okToUpdate)
                updateThe.TurbulenceIntensityPlotAndTable(this);
        }

        private void dateTIStart_ValueChanged(object sender, EventArgs e)
        {
            // Updates turbulence intensity plots and tables based on newly selected start date in TI section of Site Conditions tab
            if (okToUpdate)
                updateThe.TurbulenceIntensityPlotAndTable(this);
        }

        private void cboExtremeShearMet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update extreme shear section of Site Conditions tab based on selected met site
            if (okToUpdate)
            {
                updateThe.SiteConditionsMetDates(this);
                updateThe.SiteConditionsAlpha(this);
            }
        }

        private void cboExtremeShearRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update extreme shear section of Site Conditions tab based on selected alpha range
            if (okToUpdate)
                updateThe.SiteConditionsAlpha(this);
        }

        private void btnExportTI_Click(object sender, EventArgs e)
        {
            // Exports selected turbulence intensity (on Site Conditions tab)
            Export export = new Export();
            export.ExportTurbulenceIntensity(this);
        }

        private void cboEffectiveTI_m_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  Updates turbulence intensity plots and tables using selected Wohler exponent (m) on Site Conditions tab
            if (okToUpdate)
                updateThe.TurbulenceIntensityPlotAndTable(this);
        }

        private void cboTurbineTI_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates turbulence intensity plots and tables based on selected turbine site on Site Conditions tab
            if (okToUpdate)
                updateThe.TurbulenceIntensityPlotAndTable(this);
        }

        private void cboExtremeWSMet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates extreme wind speed plot on Site Conditions tab based on selected met site
            if (okToUpdate)
                updateThe.ExtremeWindSpeed(this);
        }

        private void btnExtremeWS_Click(object sender, EventArgs e)
        {
            // Exports estimate of extreme wind speed at selected met on Site Conditions tab
            Export export = new Export();
            export.ExportExtremeWS(this);
        }

        private void btnExportShearStats_Click(object sender, EventArgs e)
        {
            // Exports extreme wind shear statistics (alpha P values for various ranges of WS)
            Export export = new Export();
            export.ExportExtremeShear(this);
        }

        private void cboInflowTurbine_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates inflow angle plot and table on Site Conditions tab based on selected turbine
            if (okToUpdate)
                updateThe.InflowAnglePlotAndTable(this);
        }

        private void cboInflowWD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates inflow angle plot and table on Site Conditions tab based on selected wind direction
            if (okToUpdate)
                updateThe.InflowAnglePlotAndTable(this);
        }

        private void cboInflowRadius_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates inflow angle plot and table on Site Conditions tab based on selected radius of investigation
            if (okToUpdate)
                updateThe.InflowAnglePlotAndTable(this);
        }

        private void cboInflowReso_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates inflow angle plot and table on Site Conditions tab based on selected grid resolution
            if (okToUpdate)
                updateThe.InflowAnglePlotAndTable(this);
        }

        private void btn_Import_MERRA_Click(object sender, EventArgs e)
        {
            // Extracts MERRA2 data closest to specified lat/long and updates MERRA2 tab

            // Read user-defined lat/long
            double thisLat = 0;
            double thisLong = 0;
                        
            try
            {
                thisLat = Convert.ToDouble(txtMERRA_SelectedLat.Text);
            }
            catch
            {
                MessageBox.Show("Invalid latitude entered.", "Continuum 3");
                return;
            }

            try
            {
                thisLong = Convert.ToDouble(txtMERRA_SelectedLong.Text);
            }
            catch
            {
                MessageBox.Show("Invalid longitude entered.", "Continuum 3");
                return;
            }

            bool wasSaved = false;
            if (savedParams.savedFileName == null)
            {
                MessageBox.Show("Please specify the file location.", "Continuum 3");
                wasSaved = SaveAs();
                if (wasSaved == false)
                {
                    MessageBox.Show("A file location is needed in order to create local database. Cancelling MERRA2 import.", "Continuum 3");
                    return;
                }
            }

            int offset = UTM_conversions.GetUTC_Offset(thisLat, thisLong);
            Met thisMet = GetSelectedMet("MERRA");                       

            merraList.AddMERRA_GetDataFromTextFiles(thisLat, thisLong, offset, this, thisMet, false);                       
          //  updateThe.MERRA_TAB(this);
        }

        private void btnDoMCP_Click(object sender, EventArgs e) 
        {

            DoMCP();
        }     
        
        public void DoMCP()
        {
            // Runs MCP at seleced met site
            Met thisMet = GetSelectedMet("MCP");
            string MCP_Method = Get_MCP_Method();

            Check_class check = new Check_class();
            bool goodToGo = check.CheckBinsAndMatrixSettings(this);

            if (goodToGo == false)
                return;

            UTM_conversion.Lat_Long theseLL = UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
            int offset = UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
            MERRA thisMERRA = merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
            thisMet.WSWD_Dists = new Met.WSWD_Dist[0];
            metList.RunMCP(ref thisMet, thisMERRA, this, MCP_Method);
            thisMet.CalcAllLT_WSWD_Dists(this, thisMet.mcp.LT_WS_Ests); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)

            metList.isMCPd = true;
            metList.AreAllMetsMCPd();

            updateThe.AllTABs(this);
        }

        private void Start_Time_ValueChanged(object sender, EventArgs e)
        {
            // Changes the start time of met data used in MCP
            if (okToUpdate == false)
                return;

            Met thisMet = GetSelectedMet("Met Data QC");
            DateTime newStartTime = Start_Time.Value;

            if (newStartTime > thisMet.metData.endDate)
            {
                MessageBox.Show("Start of met analysis cannot be after the end of met analysis", "Continuum 3");
                Start_Time.Value = thisMet.metData.startDate;
                return;
            }

            if (metList.isTimeSeries && metList.isMCPd)
            {
                DialogResult goodToGo = MessageBox.Show("Changing the start time of the met data analysis will reset the MCP estimates and all " +
                    "modeled results. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisMet.metData.startDate = Start_Time.Value;
                    ResetTimeSeries();
                    updateThe.AllTABs(this);
                }
                else
                {
                    Start_Time.Value = thisMet.metData.startDate;
                    return;
                }
            }
            else if (metList.isTimeSeries)
            {
                thisMet.metData.startDate = Start_Time.Value;
                ResetTimeSeries();                
                updateThe.AllTABs(this);
            }
            else
            {
                thisMet.metData.startDate = Start_Time.Value;
                thisMet.CalcAllMeas_WSWD_Dists(this, thisMet.metData.GetSimulatedTimeSeries(this.modeledHeight));
                updateThe.MetTurbSummaryAndStatsTable(this);
                updateThe.MetDataQC_TAB(this);
                updateThe.MCP_TAB(this);
                updateThe.SiteConditionsMetDates(this);
            }
        }

        private void End_Time_ValueChanged(object sender, EventArgs e)
        {
            // Changes the end time of met data used in MCP
            if (okToUpdate == false)
                return;

            Met thisMet = GetSelectedMet("Met Data QC");
            DateTime newEndTime = End_Time.Value;

            if (newEndTime < thisMet.metData.startDate)
            {
                MessageBox.Show("End of met analysis cannot be before the start of met analysis", "Continuum 3");
                End_Time.Value = thisMet.metData.endDate;
                return;
            }

            if (metList.isTimeSeries && metList.isMCPd)
            {
                DialogResult goodToGo = MessageBox.Show("Changing the end time of the met data analysis will reset the MCP estimates and all " +
                    "modeled results. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisMet.metData.endDate = End_Time.Value;
                    ResetTimeSeries();                    
                    updateThe.AllTABs(this);
                }
                else
                {
                    End_Time.Value = thisMet.metData.endDate;
                    return;
                }
            }
            else if (metList.isTimeSeries)
            {
                thisMet.metData.endDate = End_Time.Value;
                ResetTimeSeries();
                updateThe.AllTABs(this);
            }
            else
            {
                thisMet.metData.endDate = End_Time.Value;
                updateThe.MetDataQC_TAB(this);
                updateThe.MCP_TAB(this);
                updateThe.SiteConditionsMetDates(this);
            }
        }

        private void dateTimeExtremeShearStart_ValueChanged(object sender, EventArgs e)
        {
            // Updates extreme shear plot and table based on selected start date
            if (okToUpdate)
                updateThe.SiteConditionsAlpha(this);
        }

        private void dateTimeExtremeShearEnd_ValueChanged(object sender, EventArgs e)
        {
            // Updates extreme shear plot and table based on selected end date
            if (okToUpdate)
                updateThe.SiteConditionsAlpha(this);
        }

        private void cboSummTOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the plots and tables on the 'Met and Turbine Summary' tab based on selected radius from drop-down
            if (okToUpdate == true)
            {
                Met.TOD selectedTOD = GetSelectedTOD("Summary");
                Met.Season selectedSeason = GetSelectedSeason("Summary");

                if ((selectedTOD == Met.TOD.Day || selectedTOD == Met.TOD.Night) && selectedSeason == Met.Season.All && metList.numSeason == 4)
                    cboSummSeason.SelectedIndex = 0;
                else if (selectedTOD == Met.TOD.All && selectedSeason != Met.Season.All)
                    cboSummSeason.SelectedIndex = metList.numSeason;

                updateThe.Met_Turbine_Summary_TAB(this);
            }
        }

        private void cboSummSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates the plots and tables on the 'Met and Turbine Summary' tab based on selected radius from drop-down
            if (okToUpdate == true)
            {
                Met.TOD selectedTOD = GetSelectedTOD("Summary");
                Met.Season selectedSeason = GetSelectedSeason("Summary");

                if ((selectedSeason == Met.Season.Winter || selectedSeason == Met.Season.Spring || selectedSeason == Met.Season.Summer ||
                    selectedSeason == Met.Season.Fall) && selectedTOD == Met.TOD.All && metList.numTOD == 2)
                    cboSummTOD.SelectedIndex = 0;
                else if (selectedSeason == Met.Season.All && selectedTOD != Met.TOD.All)
                    cboSummTOD.SelectedIndex = metList.numTOD;

                updateThe.Met_Turbine_Summary_TAB(this);
            }
        }

        private void btnExportFlags_Click(object sender, EventArgs e)
        {
            // Exports met data with filter flags
            Export export = new Export();
            Met thisMet = GetSelectedMet("Met Data QC");

            if (thisMet.metData == null)
                return;

            export.ExportFlaggedData(this, thisMet.metData, thisMet.name);
        }

        private void btnExportAlpha_Click(object sender, EventArgs e)
        {
            // Exports met data with filter flags
            Export export = new Export();
            Met thisMet = GetSelectedMet("Met Data QC");

            if (thisMet.metData == null)
                return;

            if (thisMet.metData.alpha == null)
                thisMet.metData.EstimateAlpha();

            export.ExportShearData(this, thisMet.metData, thisMet.name);
        }

        private void txtMERRA_WS_ScaleFact_TextChanged(object sender, EventArgs e)
        {
            if (okToUpdate)
            {
                // Wind speed scale factor adjusts MERRA2 wind speed when calculating energy production

                double newScaleFactor = 1;

                try
                {
                    newScaleFactor = Convert.ToDouble(txtMERRA_WS_ScaleFact.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid wind speed scale factor.", "Continuum 3");
                    return;
                }

                MERRA thisMERRA = GetSelectedMERRA();
                thisMERRA.WS_ScaleFactor = newScaleFactor;
                thisMERRA.ApplyPC(ref thisMERRA.interpData.TS_Data);
            }
        }

        private void btnDoMCPAllMets_Click(object sender, EventArgs e)
        {
            // Runs MCP at all met sites
            string MCP_method = Get_MCP_Method();
            Check_class check = new Check_class();
            bool goodToGo = check.CheckBinsAndMatrixSettings(this);

            if (goodToGo == false)
                return;

            for (int i = 0; i < metList.ThisCount; i++)
            {
                Met thisMet = metList.metItem[i];

                if (thisMet.metData.GetNumAnems() > 0)
                {
                    if (thisMet.metData.anems[0].windData == null)
                        thisMet.metData.GetSensorDataFromDB(this, thisMet.name);
                }
                else
                    break;
                
                UTM_conversion.Lat_Long theseLL = UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                MERRA thisMERRA = merraList.GetMERRA(theseLL.latitude, theseLL.longitude);

                if (thisMERRA.interpData.TS_Data.Length == 0)
                {
                    thisMERRA.GetMERRADataFromDB(this);
                    thisMERRA.GetInterpData(UTM_conversions);
                }

                if (thisMERRA.interpData.TS_Data.Length == 0)
                    break;

                thisMet.WSWD_Dists = new Met.WSWD_Dist[0];
                metList.RunMCP(ref metList.metItem[i], thisMERRA, this, MCP_method);
                thisMet.CalcAllLT_WSWD_Dists(this, thisMet.mcp.LT_WS_Ests); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)
                
            }

            if (metList.ThisCount > 0)
                metList.isMCPd = true;

            metList.AreAllMetsMCPd();                      

            updateThe.AllTABs(this);
        }

        private void btnGetDefaultExceed_Click(object sender, EventArgs e)
        {
            // Set default exceedance curves
            
            if (turbineList.exceed.compositeLoss.isComplete)
            {
                DialogResult goodToGo = MessageBox.Show("Setting the exceedance curves to the default values will reset the Monte Carlo estimates" +
                    "and all net energy estimates. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.No)
                    return;
                else
                {
                    turbineList.ClearAllNetEsts();
                    mapList.ClearAllWakedMaps();
                }
            }

            turbineList.ClearAllExceedance();
            turbineList.exceed.CreateDefaultCurve();
            updateThe.Exceedance_TAB(this);
        }

        private void btnImportCurves_Click(object sender, EventArgs e)
        {
            // Import exceedance curves
            turbineList.exceed.ImportExceedCurves(this);
            updateThe.Exceedance_TAB(this);
        }

        private void lstDefinedLosses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (okToUpdate)
                updateThe.PerformanceFactorsPlot(this);
        }

        private void lstZones_Click(object sender, EventArgs e)
        {
        //    if (okToUpdate)
           //     updateThe.SiteSuitabilitySurfacePlotLabels(this);
        }

        private void cboWS_or_WD_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (okToUpdate)
                updateThe.WS_or_WR_Plot(this);
        }

        private void btnModHeight_Click(object sender, EventArgs e)
        {
            // Changes height that met data is extrapolated to and height of wind flow model
            // If estimates have been formed, resets model, turbine estimates, and round robin
            if (okToUpdate == false)
                return;

            double newHeight = modeledHeight;

            try
            {
                newHeight = Convert.ToSingle(Interaction.InputBox("What height [m] do you want to model?", "Continuum 3"));
            }
            catch
            {
                MessageBox.Show("Invalid entry for modeled height.", "Continuum 3");
                return;
            }

            if (newHeight == modeledHeight)
            {
                MessageBox.Show("Modeled height unchanged.", "Continuum 3");
                return;
            }

            if (modelList.ModelCount > 0)
            {
                DialogResult goodToGo = MessageBox.Show("Changing the modeled height will reset models and all estimated values. Do you " +
                    "want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.No)
                    return;
                else
                {
                    modelList.ClearAll();
                    ResetTimeSeries();
                }
            }

            txtModeledHeight.Text = newHeight.ToString();
            modeledHeight = newHeight;

            // Extrapolate all met data to new height and run MCP if isMCPd and have MERRA data
            for (int i = 0; i < metList.ThisCount; i++)
            {
                Met thisMet = metList.metItem[i];                
                thisMet.metData.ExtrapolateData(modeledHeight);
                thisMet.CalcAllMeas_WSWD_Dists(this, thisMet.metData.GetSimulatedTimeSeries(modeledHeight));                               
            }

            metList.AreAllMetsMCPd(); // sets allMCPd flag
            updateThe.AllTABs(this);
        }

        private void cboMonthlyWakeModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Updates Time Series tab when user changes the selected wake model
            if (okToUpdate)
                updateThe.Monthly_TAB(this);
        }

        private void chkDisableFilter_CheckedChanged(object sender, EventArgs e)
        {
            // Enables or disables met data filtering. If a user brings in data that was filtered in another program, data filtering can be 
            // bypassed in Continuum
            if (okToUpdate == false)
                return;

            CheckState checkState = chkDisableFilter.CheckState;

            if (metList.ThisCount > 0 && metList.isTimeSeries)
            {
                DialogResult goodToGo = DialogResult.Yes;
                if (metList.metItem[0].metData.filteringDone && checkState == CheckState.Unchecked)
                    goodToGo = MessageBox.Show("Disabling met data filters will reset the model and all calculations. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);
                else if (metList.metItem[0].metData.filteringDone == false && checkState == CheckState.Checked)
                    goodToGo = MessageBox.Show("Enabling met data filters will reset the model and all calculations. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.No)
                {
                    okToUpdate = false;
                    CheckState oldState = CheckState.Checked;
                    if (checkState == CheckState.Checked)
                        oldState = CheckState.Unchecked;

                    chkDisableFilter.CheckState = oldState;
                    okToUpdate = true;
                }
                else
                {
                    ResetTimeSeries();

                    if (checkState == CheckState.Checked)
                        metList.filteringEnabled = true;
                    else
                        metList.filteringEnabled = false;

                    for (int i = 0; i < metList.ThisCount; i++)
                    {
                        metList.metItem[i].metData.ClearAlphaAndSimulatedEstimates();
                        metList.metItem[i].metData.ClearFilterFlagsAndEstimatedData();

                        if (metList.filteringEnabled)
                            metList.metItem[i].metData.FilterData("All");
                        metList.metItem[i].metData.EstimateAlpha();
                        metList.metItem[i].metData.ExtrapolateData(modeledHeight);
                    }
                                       
                }
            }
            else
            {
                if (checkState == CheckState.Checked)
                    metList.filteringEnabled = true;
                else
                    metList.filteringEnabled = false;
            }

            updateThe.AllTABs(this);
        }

        private void lstPowerCurveList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (okToUpdate)
                updateThe.PowerCrvPlot(this);
        }

        private void cboTODAdvanced_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (okToUpdate)
            {
                Met.TOD selectedTOD = GetSelectedTOD("Advanced");
                Met.Season selectedSeason = GetSelectedSeason("Advanced");

                if ((selectedTOD == Met.TOD.Day || selectedTOD == Met.TOD.Night) && selectedSeason == Met.Season.All && metList.numSeason == 4)
                    cboSeasonAdvanced.SelectedIndex = 0;
                else if (selectedTOD == Met.TOD.All && selectedSeason != Met.Season.All)
                    cboSeasonAdvanced.SelectedIndex = metList.numSeason;

                updateThe.AdvancedTAB(this);
            }
        }

        private void cboSeasonAdvanced_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (okToUpdate)
            {
                Met.TOD selectedTOD = GetSelectedTOD("Advanced");
                Met.Season selectedSeason = GetSelectedSeason("Advanced");

                if ((selectedSeason == Met.Season.Winter || selectedSeason == Met.Season.Spring || selectedSeason == Met.Season.Summer ||
                    selectedSeason == Met.Season.Fall) && selectedTOD == Met.TOD.All && metList.numTOD == 2)
                    cboTODAdvanced.SelectedIndex = 0;
                else if (selectedSeason == Met.Season.All && selectedTOD != Met.TOD.All)
                    cboTODAdvanced.SelectedIndex = metList.numTOD;

                updateThe.AdvancedTAB(this);
            }
        }

        private void cboCreateTurbTS_CheckedChanged(object sender, EventArgs e)
        {
            if (okToUpdate)
            {
                DialogResult goodToGo = DialogResult.Yes;

                if (turbineList.turbineCalcsDone && turbineList.genTimeSeries == true && chkCreateTurbTS.CheckState == CheckState.Unchecked)
                {
                    goodToGo = MessageBox.Show("Turbine calculations have already been generated as a time series. Changing this setting will clear the estimates. Do you want " +
                        "to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                    if (goodToGo == DialogResult.No)
                    {
                        chkCreateTurbTS.CheckState = CheckState.Checked;
                        return;
                    }
                    else
                    {
                        turbineList.ClearAllWSEsts();
                        turbineList.ClearAllGrossEsts();
                        turbineList.ClearAllNetEsts();
                        wakeModelList.ClearAll();
                    }
                    
                }
                else if (turbineList.turbineCalcsDone && turbineList.genTimeSeries == false && chkCreateTurbTS.CheckState == CheckState.Checked)
                {
                    goodToGo = MessageBox.Show("Turbine calculations have already been generated on an annual basis. Changing this setting will clear the estimates. Do you want " +
                        "to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                    if (goodToGo == DialogResult.No)
                    {
                        chkCreateTurbTS.CheckState = CheckState.Unchecked;
                        return;
                    }
                    else
                    {
                        turbineList.ClearAllWSEsts();
                        turbineList.ClearAllGrossEsts();
                        turbineList.ClearAllNetEsts();
                        wakeModelList.ClearAll();
                    }
                }

                if (chkCreateTurbTS.CheckState == CheckState.Checked)
                    turbineList.genTimeSeries = true;
                else
                    turbineList.genTimeSeries = false;

                updateThe.AllTABs(this);
            }
        }

        private void FolderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void btnGenerateHeaders_Click(object sender, EventArgs e)
        {
            GenHeaders genHeaders = new GenHeaders();
            genHeaders.ShowDialog();
        }

  /*      private void lstZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (okToUpdate)
            {
                string eventArgs = e.ToString();
                string[] parsedArgs = eventArgs.Split();
                int changedInd = Convert.ToInt16(parsedArgs[0]);
                bool isChecked = Convert.ToBoolean(parsedArgs[1]);
                updateThe.SiteSuitabilitySurfacePlotLabels(this, changedInd, isChecked);
            }
        }
*/
        private void showHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Opens user manual
            string filePath = "C:\\Program Files\\One Energy Enterprises LLC\\Continuum 3.0\\Documentation\\Continuum 3.0 Quick User Guide.pdf";

            if (File.Exists(filePath))
            {
                try {
                    Process.Start(filePath);
                            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("User's guide cannot be found.", "Continuum 3");
            }

        }

        private void btnExportMonthlyTurbineValues_Click(object sender, EventArgs e)
        {
            // Exports hourly estimates at turbine site selected on "Time Series Analysis" tab
            Export export = new Export();
            export.ExportMonthlyTurbineValues(this);
        }

        private void btnInflowAngles_Click(object sender, EventArgs e)
        {
            Export export = new Export();
            export.ExportInflowAngles(this);
        }

        private void btnGenTurbGross_Click(object sender, EventArgs e)
        {
            // Checks that turbine calculations can take place, creates the struct needed for turbine calcs and calls background worker
            if (BW_worker.IsHandleCreated == false && BW_worker.IsBusy()) // it was force closed
                BW_worker = new BackgroundWork();

            if (BW_worker.IsBusy())
            {
                MessageBox.Show("Cannot generate turbine estimates while other calculations are under way.", "Continuum 3");
                return;
            }

            if (metList.ThisCount == 0)
            {
                MessageBox.Show("You need to import met sites fist.", "Continuum 3");
                return;
            }

            if (turbineList.TurbineCount == 0)
            {
                MessageBox.Show("You need to import turbines fist.", "Continuum 3");
                return;
            }

            if (modelList.ModelCount <= 1 && metList.ThisCount > 1)
            {
                MessageBox.Show("Create a site-calibrated model first clickng Analyze Mets.", "Continuum 3");
                return;
            }

            if (modelList.ModelCount == 0)
            {
                MessageBox.Show("Create a Continuum model first clickng Analyze Mets.", "Continuum 3");
                return;
            }

            if (chkCreateTurbTS.CheckState == CheckState.Checked)
                turbineList.genTimeSeries = true;
            else
                turbineList.genTimeSeries = false;

            turbineList.ClearWS_EstCalcs();

            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

            argsForBW.thisInst = this;
            argsForBW.thisWakeModel = null;

            // Call background worker to run calculations
            BW_worker = new BackgroundWork();
            string MCP_Method = Get_MCP_Method();
            argsForBW.MCP_Method = MCP_Method;
            if (topo.gotTopo)
                BW_worker.Call_BW_TurbCalcs(argsForBW);
            ChangesMade();
        }

        public void lstZones_ItemCheckChanged(object sender, EventArgs e)
        {
            if (okToUpdate)
            {                
                var theArgs = (ItemCheckEventArgs)e;
                int thisInd =  theArgs.Index;
                bool isChecked = false;
                if (theArgs.NewValue == CheckState.Checked)
                    isChecked = true;   

                updateThe.SiteSuitabilitySurfacePlotLabels(this, thisInd, isChecked);
            }


        }

        private void btnShowMCPRanges_Click(object sender, EventArgs e)
        {
            // Displays form 
            MCP_ValidSettings theseSettings = new MCP_ValidSettings();
            theseSettings.ShowDialog();

        }

        private void PgeMCP_Click(object sender, EventArgs e)
        {

        }

        private void TxtTurbineNoise_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDownloadMERRA2_Click(object sender, EventArgs e)
        {
            merraList.NASA_LogInAsync(this);
        }

        private void btnChangeFolder_Click(object sender, EventArgs e)
        {
            merraList.ChangeMERRA2Folder(this);
        }
    }
}
