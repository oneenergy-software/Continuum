using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ContinuumNS
{
    public partial class BackgroundWork : Form
    {
        Update updateThe = new Update();

        public BackgroundWork()
        {
            InitializeComponent();
        }
        
        public struct Vars_for_BW
        {
            public Continuum thisInst;
            public string Filename;
        }              

        public struct Vars_for_WRG_file
        {
            public Continuum thisInst;
            public string mapName;
            public double density;
            public string outputFile;
        }            

        public struct Vars_for_Save_As
        {
            public string oldFilename;
            public string newFilename;
        }

        public struct Vars_for_Gen_Map {
            public Continuum thisInst;
            public Map thisMap;
        }
    
        public struct Vars_for_Turbine_and_Node_Calcs {
            public Continuum thisInst;
            public Wake_Model thisWakeModel;
            public bool isCalibrated;
        }
     
        public struct Vars_for_RoundRobin {    
            public int Min_RR_Size; 
            public Continuum thisInst;
        }

        public struct Topo_struct {
            public double UTMX;
            public double[,] UTMYs_and_Elevs;
        }

        public void Call_BW_MetCalcs(Continuum thisInst)
        {
            // Calls Met Calcs background worker
            Show();
            BackgroundWorker_MetCalcs.RunWorkerAsync(thisInst);
        }

        private void BackgroundWorker_MetCalcs_DoWork(object sender, DoWorkEventArgs e)
        {
            // For each met, calculates exposure and grid stats, create met pairs and perform WS cross-prediction, finds site-calibrated model (if >1 met)
            // updates turbine calculations (if any)
                        
            Continuum thisInst = (Continuum)e.Argument;
            MetCollection metList = thisInst.metList;
            TopoInfo topo = thisInst.topo;
            InvestCollection radiiList = thisInst.radiiList;
            MetPairCollection metPairs = thisInst.metPairList;
            ModelCollection modelList   = thisInst.modelList;
            NodeCollection nodeList = new NodeCollection();
          
            string savedFilename = thisInst.savedParams.savedFileName;
            TurbineCollection turbList = thisInst.turbineList;
                      
            string textForProgBar = "Importing Met Sites";
            BackgroundWorker_MetCalcs.ReportProgress(0, textForProgBar);
            int numMets = thisInst.metList.ThisCount;
            int numRadii = thisInst.radiiList.ThisCount;                   

            textForProgBar = "Calculating exposures at met sites.";
            BackgroundWorker_MetCalcs.ReportProgress(0, textForProgBar);

            // Get elevs for calcs
            topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
            
            // Do Exposure calcs at new met site(s) and update bulk UW and DW expos at all other mets
            for (int m = 0; m < numMets; m++) {
                for (int r = 0; r < numRadii; r++) {
                    if (BackgroundWorker_MetCalcs.CancellationPending == true)
                    {
                        e.Cancel = true;
                        return;
                    }                   

                    metList.CalcMetExposures(m, r, thisInst);
                }
            }

            metList.expoIsCalc = true;

            if (topo.gotSR == true)
                metList.SRDH_IsCalc = true;

            textForProgBar = "Finished exposure calculations at met sites.";
            BackgroundWorker_MetCalcs.ReportProgress(10, textForProgBar);                        

            for (int i = 0; i < numMets; i ++) {
                if (BackgroundWorker_MetCalcs.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }

                Met thisMet = metList.metItem[i];
                
                if (thisMet.gridStats.stats == null)
                    thisMet.gridStats.GetGridArrayAndCalcStats(thisMet.UTMX, thisMet.UTMY, thisInst);                   
                                            
                // Find flow separation nodes for met site
                if (topo.useSepMod == true) thisMet.GetFlowSepNodes(nodeList, thisInst);

                textForProgBar = "Finished grid stat calculations at " + (i + 1).ToString() + "/" + numMets + " met sites.";
                BackgroundWorker_MetCalcs.ReportProgress(10 + 40 * (i + 1) / numMets, textForProgBar);
            }
                        
            // Create met pairs with single met models at all radii in list.  First checks to see what pairs already exist before making new ones.
            metPairs.CreateMetPairs(thisInst);
            int numPairs = metPairs.PairCount;
                        
            Nodes[] newNodes = null;
            
            // Find path of nodes in between mets.  if met pairs already exist then just add new default model
            for (int i = 0; i < numPairs; i++)
            { 
                if (BackgroundWorker_MetCalcs.CancellationPending == true)
                    e.Cancel = true;

                textForProgBar = "Finding path of nodes at " + (i + 1).ToString() + "/" + numPairs + " pairs of met sites.";
                BackgroundWorker_MetCalcs.ReportProgress(50 + 50 * (i + 1) / numPairs, textForProgBar);

                for (int j = 0; j < numRadii; j++)
                {
                    if (BackgroundWorker_MetCalcs.CancellationPending == true)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (metPairs.metPairs[i].WS_Pred[0, j].nodePath == null)
                    {
                        double[] windRose = new double[metList.numWD];
                        for (int WD = 0; WD < metList.numWD; WD++)
                            windRose[WD] = (metPairs.metPairs[i].met1.windRose[WD] + metPairs.metPairs[i].met2.windRose[WD]) / 2;

                        Nodes met1Node = nodeList.GetMetNode(metPairs.metPairs[i].met1);
                        Nodes met2Node = nodeList.GetMetNode(metPairs.metPairs[i].met2);
                        Nodes[] blankNodes = null;
                        Nodes[] pathOfNodes = nodeList.FindPathOfNodes(met1Node, met2Node, metPairs.metPairs[i].WS_Pred[0, j].model, thisInst, ref newNodes, ref blankNodes);

                        if (pathOfNodes.Length == 200)
                        { // couldn't find path in between mets
                            MessageBox.Show("Continuum could not find a path of nodes in between Met: " + metPairs.metPairs[i].met1.name + " and Met: " + metPairs.metPairs[i].met2.name + ". Cannot continue with calculations.  Please remove one of these mets from the analysis", "Continuum 2.3");
                            return;
                        }
                        metPairs.metPairs[i].WS_Pred[0, j].nodePath = pathOfNodes;
                    }

                    metPairs.metPairs[i].DoMetCrossPred(0, j, thisInst);
                } 
            }

            if (newNodes != null)
                nodeList.AddNodes(newNodes, savedFilename);

            Model[] models = new Model[numRadii];
                        
            for (int j = 0; j <= numRadii - 1; j++)
                models[j] = modelList.models[0, j];
                                    
            modelList.CalcRMS_Overall_and_Sectorwise(ref models, thisInst); // Calculates RMS error of single met model

            // if imported coeffs are used, don't do site-calibration
            int importedInd = modelList.GetImportedModelInd();
            int Keep_Imported = 0;

            if (importedInd >= 0) {
                if (numPairs > 0)
                    Keep_Imported = (int)MessageBox.Show("Model coefficients have been imported for this analysis. Do you wish to keep this model or create a new site-calibrated model" +
                        " with the mets being imported? Click Yes to keep imported model. Click No to delete imported model and create site-calibrated model.", "Continuum 2.3", MessageBoxButtons.YesNo);

                if (Keep_Imported == (int)DialogResult.No)
                    modelList.ClearImported();
                                    
            }
            else if (importedInd == -1) 
                Keep_Imported = 0;
            
            if (Keep_Imported == 0) {
                textForProgBar = "Finding site-calibrated model...";
                BackgroundWorker_MetCalcs.ReportProgress(50, textForProgBar);
                modelList.ClearImported();
                modelList.FindSiteCalibratedModels(thisInst);
            }
                
            // Needs to update turbine calcs if any are done
            if (turbList.turbineCalcsDone == true) {
                Nodes[] All_Nodes_in_Paths = turbList.GetAllNodesInPaths(nodeList, thisInst);
                for (int i = 0; i <= turbList.TurbineCount - 1; i++) { 
                    if (BackgroundWorker_MetCalcs.CancellationPending == true)
                    {
                        e.Cancel = true;
                        return;
                    }                       

                    textForProgBar = "Calculating wind speeds at " + (i + 1).ToString() + "/" + turbList.TurbineCount + " turbine sites.";
                    int Prog = Convert.ToInt16(100.0f * (i + 1) / turbList.TurbineCount);
                    BackgroundWorker_MetCalcs.ReportProgress(Prog, textForProgBar);
                    turbList.turbineEsts[i].windRose = metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), turbList.turbineEsts[i].UTMX, turbList.turbineEsts[i].UTMY);
                    models = thisInst.modelList.GetModels(thisInst, metList.GetMetsUsed(), radiiList.investItem[0].radius, radiiList.GetMaxRadius(), false);
                    turbList.turbineEsts[i].DoTurbineCalcs(All_Nodes_in_Paths, thisInst, models);
                    turbList.turbineEsts[i].GenerateAvgWS(thisInst, models);

                    models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), radiiList.investItem[0].radius, radiiList.GetMaxRadius(), true);
                    turbList.turbineEsts[i].DoTurbineCalcs(All_Nodes_in_Paths, thisInst, models);
                    turbList.turbineEsts[i].GenerateAvgWS(thisInst, models);
                }
                               
                turbList.CalcGrossAEP(thisInst, false);
                turbList.CalcGrossAEP(thisInst, true);
            }

            e.Result = thisInst;                        
        }

        private void BackgroundWorker_MetCalcs_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Updates the met calculation progress bar
            BringToFront();
            string textForLabel = e.UserState.ToString();
            progbar.Value = e.ProgressPercentage;
            Text = "Continuum 2.3";
            lblprogbar.Text = textForLabel;
            Refresh();
        }

        private void BackgroundWorker_MetCalcs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Updates all met lists, turbine calcs, model plots etc on main form 
            if (e.Cancelled == false)
            {
                Continuum thisInst = (Continuum)e.Result;
                thisInst.ChangesMade();
                updateThe.AllTABs(thisInst);

            }                                 
                
            Close();
        }

        public void Call_BW_TopoImport(Vars_for_BW Args)
        {
            // Calls topo import background worker
            Show();
            BackgroundWorker_Topo.RunWorkerAsync(Args);
        }

        private void BackgroundWorker_Topo_DoWork(object sender, DoWorkEventArgs e)
        {
            // Opens and reads topography data from a .XYZ, .TIF, or .ADF file. Saves elevation data to local database.
            NodeCollection nodeList = new NodeCollection();
            //  Dim The_args  Vars_for_Topo_load = DirectCast(e.Argument, Vars_for_Topo_load)
            Vars_for_BW args = (Vars_for_BW)e.Argument;
            Continuum thisInst = args.thisInst;

            string filename = args.Filename;
            TopoInfo topo = thisInst.topo;
            string savedFilename = thisInst.savedParams.savedFileName;
          
            string connString = nodeList.GetDB_ConnectionString(savedFilename);            
            string textForProgBar = "Reading in topography...";
            BackgroundWorker_Topo.ReportProgress(10, textForProgBar);
                        
            bool isTIF = false;
            bool isSHP = false;
            bool isADF = false;

            string thisExt = filename.Substring(filename.Length - 3);

            if (thisExt == "TIF" || thisExt == "tif")
                isTIF = true;
            else if (thisExt == "SHP" || thisExt == "shp")
                isSHP = true;
            else if (thisExt == "ADF" || thisExt == "adf")
                isADF = true;                     

            using (var ctx = new Continuum_EDMContainer(connString))
            {
                ctx.Database.CommandTimeout = 3000;
                
                try
                {
                    if (ctx.Database.Exists() && thisInst.topo.gotSR == false)
                        ctx.Database.Delete();                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
            }
                
            if (isTIF == true || isSHP == true || isADF == true)
            {
                bool goodToGo = topo.ReadGeoTiffTopo(filename, thisInst);

                if (topo.topoElevs == null || goodToGo == false)
                {
                    e.Result = thisInst;
                    return;
                }

                Check_class checker = new Check_class();                
                bool inputTopo = checker.NewTopoOrLC(topo.topoNumXY.X.all.min, topo.topoNumXY.X.all.max, topo.topoNumXY.Y.all.min, topo.topoNumXY.Y.all.max, thisInst);  // Check that all existing mets and turbines fall within bounds of new topo data
                
                if (inputTopo == false)
                    return;

                int thisCount = 0;
                int numAll = topo.topoNumXY.X.all.num * topo.topoNumXY.Y.all.num;
                Topo_table topoEntry = new Topo_table();
                
                float[] elevsAlongX = new float[topo.topoNumXY.Y.all.num];
                int entryInd = 0;
                BinaryFormatter bin = new BinaryFormatter();

                using (var ctx = new Continuum_EDMContainer(connString))
                {
                    for (int i = 0; i <= topo.topoElevs.GetUpperBound(0); i++)
                    {
                        for (int j = 0; j <= topo.topoElevs.GetUpperBound(1); j++)
                        {
                            thisCount++;

                            elevsAlongX[entryInd] = (float)topo.topoElevs[i, j];
                            entryInd++;

                            if (entryInd == topo.topoNumXY.Y.all.num)
                            {
                                textForProgBar = "Reading in topography...";
                                int Prog = Convert.ToInt16(100 * ((double)thisCount / (numAll - 1)));
                                BackgroundWorker_Topo.ReportProgress(Prog, textForProgBar);

                                MemoryStream MS2 = new MemoryStream();
                                bin.Serialize(MS2, elevsAlongX);
                                topoEntry.Elevs = MS2.ToArray();

                                ctx.Topo_table.Add(topoEntry);
                                ctx.SaveChanges();
                                entryInd = 0;
                                elevsAlongX = new float[topo.topoNumXY.Y.all.num];
                            }                                                   
                                                      

                            if (BackgroundWorker_Topo.CancellationPending == true)
                            {
                                e.Result = thisInst;
                                return;
                            }

                        }
                    }
                                                 
                    ctx.SaveChanges();
                }
            }

            topo.DecimateForPlot("topo");
            topo.gotTopo = true;
                       
            e.Result = thisInst;
        }

        private void BackgroundWorker_Topo_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Updates the topo data import progress bar
            string Text_for_label = e.UserState.ToString();

            if (e.ProgressPercentage <= 100)
                progbar.Value = e.ProgressPercentage;
            else
                progbar.Value = 100;

            Text = "Continuum 2.3";
            lblprogbar.Text = Text_for_label;
            Refresh();
        }

        private void BackgroundWorker_Topo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Updates the topography contour map and map on Advanced tab and saves file
            Continuum thisInst = (Continuum)e.Result;
            
            if (e.Cancelled == true)            
                updateThe.ClearDB(thisInst.savedParams.savedFileName);            
            else                   
                thisInst.SaveFile();               
            
            updateThe.AllTABs((Continuum)e.Result);
            Close();
        }        

        public void Call_BW_LandCoverImport(Vars_for_BW Args)
        {
            // Calls land cover import background worker
            Show();
            BackgroundWorker_LandCover.RunWorkerAsync(Args);
        }

        private void BackgroundWorker_LandCover_DoWork(object sender, DoWorkEventArgs e)
        {
            // Opens and reads in land cover data as .TIF file, calculate SRDH at mets and turbine sites

            Vars_for_BW The_args = (Vars_for_BW)e.Argument;
            Continuum thisInst = The_args.thisInst;
            TopoInfo topo = thisInst.topo;
            string savedFilename = thisInst.savedParams.savedFileName;
            string wholePath = The_args.Filename;       
            MetCollection metList = thisInst.metList;      
            TurbineCollection turbList = thisInst.turbineList;
            NodeCollection nodeList = new NodeCollection();
                        
            string textForProgBar = "Reading in land cover data...";
            BackgroundWorker_LandCover.ReportProgress(10, textForProgBar);

            if (topo.LC_Key == null)
                topo.SetUS_NLCD_Key();

            bool goodToGo = topo.ReadGeoTiffLandCover(wholePath, thisInst);

            if (topo.landCover == null || goodToGo == false)
            {
                e.Result = thisInst;
                return;
            }

            Check_class checker = new Check_class();
            
            bool Input_LC = checker.NewTopoOrLC(topo.LC_NumXY.X.all.min, topo.LC_NumXY.X.all.max, topo.LC_NumXY.Y.all.min, topo.LC_NumXY.Y.all.max, thisInst);  // Check that all existing mets and turbines fall within bounds of new topo data
            
            if (Input_LC == false)
            {
                e.Result = thisInst;
                return;
            }
            
            int thisCount = 0;
            int numAll = topo.LC_NumXY.X.all.num * topo.LC_NumXY.Y.all.num;
            LandCover_table LC_Entry = new LandCover_table();

            int[] LCsAlongX = new int[topo.LC_NumXY.Y.all.num];
            int entryInd = 0;
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(savedFilename);

            using (var ctx = new Continuum_EDMContainer(connString))
            {
                for (int i = 0; i <= topo.landCover.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= topo.landCover.GetUpperBound(1); j++)
                    {
                        thisCount++;

                        LCsAlongX[entryInd] = topo.landCover[i, j];
                        entryInd++;

                        if (entryInd == topo.LC_NumXY.Y.all.num)
                        {
                            textForProgBar = "Reading in land cover...";
                            int prog = Convert.ToInt16(100 * ((double)thisCount / (numAll - 1)));
                            BackgroundWorker_LandCover.ReportProgress(prog, textForProgBar);

                            MemoryStream MS2 = new MemoryStream();
                            bin.Serialize(MS2, LCsAlongX);
                            LC_Entry.LandCover = MS2.ToArray();

                            ctx.LandCover_table.Add(LC_Entry);
                            ctx.SaveChanges();
                            entryInd = 0;
                            LCsAlongX = new int[topo.LC_NumXY.Y.all.num];
                        }

                        if (BackgroundWorker_LandCover.CancellationPending == true)
                        {
                            e.Result = thisInst;
                            return;
                        }
                    }
                }
                               
                ctx.SaveChanges();
            }
                        
            topo.DecimateForPlot("LC");                      

            if (BackgroundWorker_LandCover.CancellationPending == true)
            {
                e.Result = thisInst;                
                return;
            }

            // Calculate SR/DH at mets, turbines, and nodes in database
            
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, true);
            metList.ReCalcSRDH(thisInst.topo, thisInst.radiiList);
            turbList.ReCalcTurbine_SRDH(thisInst);

            // Recalculates SR/DH with new land cover key (or calculates SR/DH with newly imported land cover)
            Nodes nodeFromDB = new Nodes();           

            int nodeCount = 0;
                        
            try
            {
                using (var context = new Continuum_EDMContainer(connString))
                {
                    var node_db = from N in context.Node_table.Include("expo") select N;
                    int totalCount = node_db.Count();

                    foreach (var N in node_db)
                    {
                        int numExpo = N.expo.Count();
                        nodeFromDB.UTMX = N.UTMX;
                        nodeFromDB.UTMY = N.UTMY;

                        nodeFromDB.expo = new Exposure[numExpo];

                        if (nodeCount % 10 == 0)
                        {
                            textForProgBar = "Recalculating SR/DH at Nodes in database. Node: " + nodeCount + "/" + totalCount;
                            BackgroundWorker_LandCover.ReportProgress(100 * nodeCount / totalCount, textForProgBar);
                        }

                        for (int i = 0; i <= numExpo - 1; i++)
                        {
                            nodeFromDB.expo[i] = new Exposure();
                            nodeFromDB.expo[i].radius = N.expo.ElementAt(i).radius;
                            nodeFromDB.expo[i].exponent = N.expo.ElementAt(i).exponent;
                            int numSectors = 1;

                            // Recalc SR / DH                            
                            int smallerRadius = thisInst.topo.GetSmallerRadius(nodeFromDB.expo, nodeFromDB.expo[i].radius, nodeFromDB.expo[i].exponent, numSectors);

                            if (smallerRadius == 0 || numSectors > 1)
                            { // when sector avg is used, can//t add on to exposure calcs...so gotta do it the long way
                                thisInst.topo.CalcSRDH(ref nodeFromDB.expo[i], nodeFromDB.UTMX, nodeFromDB.UTMY, nodeFromDB.expo[i].radius, nodeFromDB.expo[i].exponent, thisInst.metList.numWD);
                            }
                            else
                            {
                                Exposure smallerExpo = thisInst.topo.GetSmallerRadiusExpo(nodeFromDB.expo, smallerRadius, nodeFromDB.expo[i].exponent, numSectors);
                                thisInst.topo.CalcSRDHwithSmallerRadius(ref nodeFromDB.expo[i], nodeFromDB.UTMX, nodeFromDB.UTMY, nodeFromDB.expo[i].radius, nodeFromDB.expo[i].exponent, numSectors, smallerRadius, smallerExpo, thisInst.metList.numWD);
                            }

                            // Write back to expo_db
                            MemoryStream MS7 = new MemoryStream();
                            MemoryStream MS8 = new MemoryStream();
                            
                            if (nodeFromDB.expo[i].SR != null)
                            {
                                bin.Serialize(MS7, nodeFromDB.expo[i].SR);
                                N.expo.ElementAt(i).SR_Array = MS7.ToArray();
                            }

                            if (nodeFromDB.expo[i].dispH != null)
                            {
                                bin.Serialize(MS8, nodeFromDB.expo[i].dispH);
                                N.expo.ElementAt(i).DH_Array = MS8.ToArray();
                            }
                        }

                        nodeCount++;
                    }

                    // Save DB
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
                return;
            }

            e.Result = thisInst;
        }

        private void BackgroundWorker_LandCover_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update progress bar for land cover data import
            string Text_for_label = e.UserState.ToString();
            if (e.ProgressPercentage <= 100)
                progbar.Value = e.ProgressPercentage;
            else
                progbar.Value = 100;

            Text = "Continuum 2.3";
            lblprogbar.Text = Text_for_label;
            Refresh();
        }

        private void BackgroundWorker_LandCover_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Updates met and turbine SRDH plots and tables, topo map, Math.Round Robin, Advanced tab after land cover data import
            Continuum thisInst = (Continuum)e.Result;
            
            if (e.Cancelled == false)
            {
                thisInst.SaveFile();
            }
            else // was cancelled so clear Land Cover table
            {
                thisInst.topo.gotSR = false;
                thisInst.updateThe.Clear_LandCover_DB(thisInst.savedParams.savedFileName);                
                thisInst.SaveFile();
            }

            updateThe.AllTABs(thisInst);
            Close();
        }

        public void Call_BW_WAsP_Map(Vars_for_BW Args)
        {
            // Calls .MAP import background worker
            Show();
            BackgroundWorker_WAsP_Map.RunWorkerAsync(Args);
        }

        private void BackgroundWorker_WAsP_Map_DoWork(object sender, DoWorkEventArgs e)
        {
            // Reads in a .MAP surface roughness contour file and creates the LandCover map (to do: import shape files)
            Vars_for_BW theArgs = (Vars_for_BW)e.Argument;
            Continuum thisInst = theArgs.thisInst;
            string fileName = theArgs.Filename;
            TopoInfo topo = thisInst.topo;
            MetCollection metList = thisInst.metList;
            TurbineCollection turbList = thisInst.turbineList;
            InvestCollection radiiList = thisInst.radiiList;
            string savedFilename = thisInst.savedParams.savedFileName;
            NodeCollection nodeList = new NodeCollection();

            bool Is_TIF = false;
            bool Is_SHP = false;

            if (fileName.Substring(fileName.Length - 3) == "TIF" || fileName.Substring(fileName.Length - 3) == "tif")
                Is_TIF = true;
            else if (fileName.Substring(fileName.Length - 3) == "SHP" || fileName.Substring(fileName.Length - 3) == "shp")
                Is_SHP = true;

            if (Is_SHP == true)
            {
                UTM_conversion UTM = new UTM_conversion();
                topo.Read_SHP_Roughness(fileName, UTM); //// this doesn;t work yet
            }

            StreamReader sr = new StreamReader(fileName);

            for (int i = 0; i <= 3; i++)
                sr.ReadLine();

            TopoInfo.Roughness_Map_Struct[] theseShapes = null;
            int shapeCount = 0;
            int pointCount = 0;

            string textForProgBar = "";
            BackgroundWorker_WAsP_Map.ReportProgress(20, textForProgBar);

            topo.LC_NumXY.X.all.min = 10000000;
            topo.LC_NumXY.X.all.max = 0;
            topo.LC_NumXY.Y.all.min = 10000000;
            topo.LC_NumXY.Y.all.max = 0;

            // Read file and save all shapes in These_shapes() array
            // Roughness_Map_Struct has: minX/Y, maxX/Y, Left_Rough, Right_Rough, numPoints, Points()  UTM_X_Y, Is_Closed as bool
            // Define Min/Max_UTM_LC based on Min/Max X/Y of shapes

            // Read file and create array of Roughness_Map_Struct objects: These_shapes()
            // Also find min/max X/Y of all shapes to determine extent and dimensions of landCover array

            while (sr.EndOfStream == false)
            {
                Array.Resize(ref theseShapes, shapeCount + 1);
                theseShapes[shapeCount] = new TopoInfo.Roughness_Map_Struct();

                string thisDataString = sr.ReadLine();
                string[] dataSplit = thisDataString.Split(' ');

                string[] trimmedDataSplit = new string[0];
                int dataCount = 0;

                if (dataSplit == null)
                {
                    MessageBox.Show("Error importing .MAP file.");
                    e.Result = thisInst;
                    return;
                }

                for (int i = 0; i < dataSplit.Length; i++)
                {
                    if (dataSplit[i] != "")
                    {
                        dataCount++;
                        Array.Resize(ref trimmedDataSplit, dataCount);
                        trimmedDataSplit[dataCount - 1] = dataSplit[i];
                    }
                }                        

                if (trimmedDataSplit.Length == 3)
                {
                    theseShapes[shapeCount].leftRough = Convert.ToSingle(trimmedDataSplit[0]);
                    theseShapes[shapeCount].rightRough = Convert.ToSingle(trimmedDataSplit[1]);
                    theseShapes[shapeCount].numPoints = Convert.ToInt16(trimmedDataSplit[2]);
                }
                else
                {
                    MessageBox.Show("Error importing .MAP file.");
                    e.Result = thisInst;
                    return;
                }

                theseShapes[shapeCount].isClosed = true;

                pointCount = 0;
                theseShapes[shapeCount].points = new TopoInfo.UTM_X_Y[theseShapes[shapeCount].numPoints];
                
                while (pointCount < theseShapes[shapeCount].numPoints)
                {
                    thisDataString = sr.ReadLine();
                    dataSplit = thisDataString.Split(' ');

                    if (dataSplit != null)
                    {
                        dataCount = 0;
                        trimmedDataSplit = new string[0];
                        for (int i = 0; i < dataSplit.Length; i++)
                        {
                            if (dataSplit[i] != "")
                            {
                                dataCount++;
                                Array.Resize(ref trimmedDataSplit, dataCount);
                                trimmedDataSplit[dataCount - 1] = dataSplit[i];
                            }
                        }

                        for (int i = 0; i <= trimmedDataSplit.Length - 2; i = i + 2)
                        {
                            theseShapes[shapeCount].points[pointCount].UTMX = Convert.ToSingle(trimmedDataSplit[i]);
                            theseShapes[shapeCount].points[pointCount].UTMY = Convert.ToSingle(trimmedDataSplit[i + 1]);

                            if (theseShapes[shapeCount].points[pointCount].UTMX < topo.LC_NumXY.X.all.min) topo.LC_NumXY.X.all.min = theseShapes[shapeCount].points[pointCount].UTMX;
                            if (theseShapes[shapeCount].points[pointCount].UTMX > topo.LC_NumXY.X.all.max) topo.LC_NumXY.X.all.max = theseShapes[shapeCount].points[pointCount].UTMX;
                            if (theseShapes[shapeCount].points[pointCount].UTMY < topo.LC_NumXY.Y.all.min) topo.LC_NumXY.Y.all.min = theseShapes[shapeCount].points[pointCount].UTMY;
                            if (theseShapes[shapeCount].points[pointCount].UTMY > topo.LC_NumXY.Y.all.max) topo.LC_NumXY.Y.all.max = theseShapes[shapeCount].points[pointCount].UTMY;
                            
                            pointCount++;
                        }
                    }                                         
                }

                shapeCount++;
                textForProgBar = "Reading surface roughness file.";
                BackgroundWorker_WAsP_Map.ReportProgress(20, textForProgBar);
            }

            sr.Close();

            // Find Min/Max X/Y of each shape and determine if the shape is closed or if it is a line

            for (int i = 0; i < shapeCount; i++)
                topo.FindShapeMinMaxAndIsClosed(ref theseShapes[i]);

            // Now create land cover key: count all unique roughness values and assign default DH height to each

            topo.CreateLC_KeyUsingMAP_Shapes(theseShapes);

            topo.LC_NumXY.X.all.reso = 30;
            topo.LC_NumXY.Y.all.reso = 30;

            topo.LC_NumXY.X.all.num = Convert.ToInt16((topo.LC_NumXY.X.all.max - topo.LC_NumXY.X.all.min) / topo.LC_NumXY.X.all.reso + 1);
            topo.LC_NumXY.Y.all.num = Convert.ToInt16((topo.LC_NumXY.Y.all.max - topo.LC_NumXY.Y.all.min) / topo.LC_NumXY.Y.all.reso + 1);

            topo.landCover = new int[topo.LC_NumXY.X.all.num, topo.LC_NumXY.Y.all.num];

            // Traces each line of each shape and assigns LC codes to +/-90m from each line segment
            topo.AssignLC_CodesToShapeContours(theseShapes);

            // Now fill in blanks
            topo.FillInLC_Array();

            LandCover_table LC_Entry = new LandCover_table();

            int[] LCs_Along_X = new int[topo.LC_NumXY.Y.all.num];
            int entryInd = 0;
            int thisCount = 0;
            int numAll = topo.LC_NumXY.X.all.num * topo.LC_NumXY.Y.all.num;
            BinaryFormatter bin = new BinaryFormatter();
            string connString = nodeList.GetDB_ConnectionString(savedFilename);

            using (var ctx = new Continuum_EDMContainer(connString))
            {
                for (int i = 0; i <= topo.landCover.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= topo.landCover.GetUpperBound(1); j++)
                    {
                        thisCount++;

                        LCs_Along_X[entryInd] = topo.landCover[i, j];
                        entryInd++;

                        if (entryInd == topo.LC_NumXY.Y.all.num)
                        {
                            textForProgBar = "Reading in land cover...";
                            int Prog = Convert.ToInt16(100 * ((double)thisCount / (numAll - 1)));
                            BackgroundWorker_WAsP_Map.ReportProgress(Prog, textForProgBar);

                            MemoryStream MS2 = new MemoryStream();
                            bin.Serialize(MS2, LCs_Along_X);
                            LC_Entry.LandCover = MS2.ToArray();

                            ctx.LandCover_table.Add(LC_Entry);
                            ctx.SaveChanges();
                            entryInd = 0;
                            LCs_Along_X = new int[topo.LC_NumXY.Y.all.num];
                        }

                        if (BackgroundWorker_WAsP_Map.CancellationPending == true)
                        {
                            e.Result = thisInst;
                            return;
                        }
                    }
                }

                ctx.SaveChanges();
            }

            topo.useSR = true;
            topo.gotSR = true;
            topo.DecimateForPlot("LC");

            int numMets = metList.ThisCount;
            int numRad = radiiList.ThisCount;

            double[] overallWindRose = metList.GetAvgWindRose();

            if (overallWindRose != null) {
                if (metList.ThisCount > 0) {
                    topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
                                        
                    if (BackgroundWorker_WAsP_Map.CancellationPending == true)
                    {
                        e.Result = thisInst;                        
                        return;
                    }

                    for (int m = 0; m <= numMets - 1; m++) {
                        for (int r = 0; r <= numRad - 1; r++) {
                            if (BackgroundWorker_WAsP_Map.CancellationPending == true)
                            {
                                e.Result = thisInst;                                
                                return;
                            }
                            metList.CalcMetExposures(m, r, thisInst);
                        }
                    }

                    metList.SRDH_IsCalc = true;
                }

                if (turbList.TurbineCount > 0)
                {
                    numRad = radiiList.ThisCount;                    

                    for (int i = 0; i <= numRad - 1; i++)
                    {
                        int radius = radiiList.investItem[i].radius;
                        double exponent = radiiList.investItem[i].exponent;

                        if (BackgroundWorker_WAsP_Map.CancellationPending == true)
                        {
                            e.Result = thisInst;                            
                            return;
                        }

                        turbList.CalcTurbineExposures(thisInst, radius, exponent, 1);
                        turbList.turbineCalcsDone = false;

                        textForProgBar = "Calculating surface roughness at turbine sites.";
                        BackgroundWorker_WAsP_Map.ReportProgress((i + 1) / numRad * 100, textForProgBar);
                    }
                }
            }
            e.Result = thisInst;
        }

        private void BackgroundWorker_WAsP_Map_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Updates progress bar of .MAP import
            string Text_for_label = e.UserState.ToString();

            if (e.ProgressPercentage <= 100)
                progbar.Value = e.ProgressPercentage;
            else
                progbar.Value = 100;

            Text = "Continuum 2.3";
            lblprogbar.Text = Text_for_label;
            Refresh();
        }

        private void BackgroundWorker_WAsP_Map_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Updates topo map, turbine and met SRDH plots and tables after .MAP roughness file import

            if (e.Cancelled == false)
            {
                Continuum thisInst = (Continuum)e.Result;
                updateThe.AllTABs(thisInst);
                thisInst.ChangesMade();
            }

            Close();

        }

        public void Call_BW_TurbCalcs(Vars_for_Turbine_and_Node_Calcs Args)
        {
            // Calls Turbine Calcs background worker
            Show();
            BackgroundWorker_TurbCalcs.RunWorkerAsync(Args);
        }

        private void BackgroundWorker_TurbCalcs_DoWork(object sender, DoWorkEventArgs e)
        {
            // Calculates exposure and SRDH at each turbine site, calls DoTurbineCalcs, generates avg WS estimates and energy estimate and wake loss calcs (if net calcs)

            Vars_for_Turbine_and_Node_Calcs theArgs = (Vars_for_Turbine_and_Node_Calcs)(e.Argument);
            Continuum thisInst = theArgs.thisInst;
            Wake_Model wakeModel = theArgs.thisWakeModel;
            bool isCalibrated = theArgs.isCalibrated;
            NodeCollection nodeList = new NodeCollection();
            TurbineCollection turbList = thisInst.turbineList;
            int numTurbs = turbList.TurbineCount;
            int numWD = thisInst.metList.numWD;       
          
            string textForProgBar = "Turbine calculations starting...";
            BackgroundWorker_TurbCalcs.ReportProgress(0, textForProgBar);            
            
            if (wakeModel != null) {
                textForProgBar = "Calculating wake losses at turbine sites.";                
            }
            else {
                textForProgBar = "Calculating exposures at turbine sites.";                
            }

            BackgroundWorker_TurbCalcs.ReportProgress(0, textForProgBar);            
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false); // Get elevs for calcs           

            for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
            {
                int radius = thisInst.radiiList.investItem[i].radius;
                double exponent = thisInst.radiiList.investItem[i].exponent;

                turbList.CalcTurbineExposures(thisInst, radius, exponent, 1);

                if (wakeModel != null) {
                    textForProgBar = "Calculating wake losses at turbine sites.";
                    BackgroundWorker_TurbCalcs.ReportProgress(0, textForProgBar);
                }
                else {
                    textForProgBar = "Calculating exposures at turbine sites.";
                    int prog = (int)((double)(i + 1) / thisInst.radiiList.ThisCount * 100);
                    BackgroundWorker_TurbCalcs.ReportProgress(prog, textForProgBar);
                }

                if (BackgroundWorker_TurbCalcs.CancellationPending == true) {
                    e.Result = thisInst;                    
                    return;
                }
            }

            if (wakeModel != null) {
                textForProgBar = "Calculating wake losses at turbine sites.";
                BackgroundWorker_TurbCalcs.ReportProgress(0, textForProgBar);
            }
            else {
                textForProgBar = "Finished exposure calculations at turbine sites.";
                BackgroundWorker_TurbCalcs.ReportProgress(10, textForProgBar);
            }

            Nodes[] allNodesInPaths = turbList.GetAllNodesInPaths(nodeList, thisInst);

            for (int i = 0; i < turbList.TurbineCount; i++) {                
                if (thisInst.topo.useSepMod == true) turbList.turbineEsts[i].GetFlowSepNodes(thisInst);
                turbList.turbineEsts[i].windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), turbList.turbineEsts[i].UTMX, turbList.turbineEsts[i].UTMY);

                Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(), false);
                turbList.turbineEsts[i].DoTurbineCalcs(allNodesInPaths, thisInst, models);
                turbList.turbineEsts[i].GenerateAvgWS(thisInst, models); // calculates avg WS est at turb and uncertainty of WS

                models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(), true);
                turbList.turbineEsts[i].DoTurbineCalcs(allNodesInPaths, thisInst, models);
                turbList.turbineEsts[i].GenerateAvgWS(thisInst, models); // calculates avg WS est at turb and uncertainty of WS

                int prog = (int)(100.0f * (i + 1) / numTurbs);
                textForProgBar = "Calculating wind speeds at " + (i + 1) + "/" + turbList.TurbineCount + " turbine sites.";
                BackgroundWorker_TurbCalcs.ReportProgress(prog, textForProgBar);

                if (BackgroundWorker_TurbCalcs.CancellationPending == true) {
                    e.Result = thisInst;                    
                    return;
                }
            }

            // Combine WS ests from various mets into one average
            // Check UWDW pointers to turbines (they get messed up for some reason that I don//t know yet...and this function re-points the turbine//s UWDW model to the UWDW in the list
           // turbList.Check_UWDW_pointers_to_turbs(thisInstUWDWList);
           
            textForProgBar = "Calculating gross AEP at turbine sites.";
            BackgroundWorker_TurbCalcs.ReportProgress(90, textForProgBar);
            turbList.CalcGrossAEP(thisInst, true);
            turbList.CalcGrossAEP(thisInst, false);

            BackgroundWorker_TurbCalcs.ReportProgress(0, textForProgBar);

            if (wakeModel != null) {

                // Find wake loss coeffs
                WakeCollection.WakeLossCoeffs[] wakeCoeffs = null;                 
                int minDistance = 10000000;
                int maxDistance = 0;

                for (int i = 0; i < numTurbs; i++) {
                    int[] Min_Max_Dist = turbList.CalcMinMaxDistanceToTurbines(turbList.turbineEsts[i].UTMX, turbList.turbineEsts[i].UTMY);
                    if (Min_Max_Dist[0] < minDistance) minDistance = Min_Max_Dist[0]; // this is min distance to turbine but when WD is at a different angle (not in line with turbines) the X dist is less than this value so making this always equal to 2*RD
                    if (Min_Max_Dist[1] > maxDistance) maxDistance = Min_Max_Dist[1];
                }

                minDistance = (int)(2 * wakeModel.powerCurve.RD);
                if (maxDistance == 0) maxDistance = 15000; // maxDistance will be zero when there is only one turbine. Might be good to make this value constant
                wakeCoeffs = thisInst.wakeModelList.GetWakeLossesCoeffs(minDistance, maxDistance, wakeModel, thisInst.metList);

                for (int i = 0; i < numTurbs; i++) { 
                    textForProgBar = "Calculating wake losses and net estimates at turbine sites. " + ((i + 1) / (float)numTurbs).ToString("P") + " complete";
                    BackgroundWorker_TurbCalcs.ReportProgress((int)(100 * (i + 1) / (float)numTurbs), textForProgBar);

                    if (BackgroundWorker_TurbCalcs.CancellationPending == true) {
                        turbList.CleanUpEsts(thisInst.wakeModelList);
                        thisInst.wakeModelList.CleanUpWakeModelsAndGrid();
                        e.Result = thisInst;                        
                        return;
                    }

                    turbList.turbineEsts[i].CalcTurbineWakeLosses(thisInst, wakeCoeffs, wakeModel, isCalibrated);
                }
            }

            turbList.AssignStringNumber();
            turbList.turbineCalcsDone = true;                     
            e.Result = thisInst;
        }

        private void BackgroundWorker_TurbCalcs_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Updates the Turbine Calcs progress bar
            string Text_for_label = e.UserState.ToString();
            progbar.Value = e.ProgressPercentage;
            Text = "Continuum 2.3";
            lblprogbar.Text = Text_for_label;
            BringToFront();
            Refresh();
        }

        private void BackgroundWorker_TurbCalcs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Updates the turbine estimates on main form
            if (e.Cancelled == false)
            {
                Continuum thisInst = (Continuum)e.Result;
                updateThe.AllTABs(thisInst);
                thisInst.ChangesMade();
            }

            Close();
        }

        public void Call_BW_GenMap(Vars_for_Gen_Map Args)
        {
            // Calls Map background worker
            Show();
            BackgroundWorker_Map.RunWorkerAsync(Args);
        }
   
        private void BackgroundWorker_Map_DoWork(object sender, DoWorkEventArgs e)
        {
            // for each map node, calculates exposure, SRDH, grid stats, and calls DoMapCalcs
            // Generates a map using the Background worker
            
            Vars_for_Gen_Map theArgs = (Vars_for_Gen_Map)(e.Argument);

            Continuum thisInst = theArgs.thisInst;
            TopoInfo topo = thisInst.topo;
            Map thisMap = theArgs.thisMap;
            MapCollection mapList = thisInst.mapList;
            MetCollection metList = thisInst.metList;
            NodeCollection nodeList = new NodeCollection();
            
            ModelCollection modelList = thisInst.modelList;
            TurbineCollection turbList = thisInst.turbineList;
            
            MetPairCollection metPairList = thisInst.metPairList;
            WakeCollection wakeModelList = thisInst.wakeModelList;                       

            int numMaps = mapList.ThisCount;
            int numWD = thisInst.metList.numWD;            
            
            int numX = thisMap.numX;
            int numY = thisMap.numY;
            int numMapNodes = numX * numY;
                        
            int wakeGridInd = wakeModelList.GetWakeGridInd(thisMap.wakeModel, thisMap.minUTMX, thisMap.minUTMY, thisMap.numX, thisMap.numY, thisMap.reso);                   

            string textForProgBar = "Retrieving elevation data from local database...";            

            int mapNodeCount = 0;
            BackgroundWorker_Map.ReportProgress(0, textForProgBar);

            // Get elevs for calcs
            topo.GetElevsAndSRDH_ForCalcs(thisInst, thisMap, false);

            if (thisMap.parameterToMap == null)
            {
                thisMap.parameterToMap = new double[numX, numY];
                if (thisMap.modelType == 2 || thisMap.modelType == 4 || thisMap.isWaked == true)
                    thisMap.sectorParamToMap = new double[numX, numY, numWD];
            }            

            Stopwatch thisStopwatch = new Stopwatch();
            thisStopwatch.Start();

            double timeElapsed = 0;
            double avgTimePerNode = 0;
            double timeToFinish;
            Nodes[] lastAllNodesInPath = null;
            NodeCollection.Path_of_Nodes_w_Rad_and_Met_Name[] pathsToMets = null;            
        
            Nodes[] allNodesInX = new Nodes[numY];          
            Nodes firstNodeLastCol = new Nodes();
           
            Nodes[] nodesToAdd = new Nodes[1];
            Nodes[] nodesFromDB = null;
         
            WakeCollection.WakeLossCoeffs[] wakeCoeffs = null;  

            if (thisMap.isWaked == true) {
                // Find wake loss coeffs

                int minDist = (int)(2 * thisMap.wakeModel.powerCurve.RD);
                int maxDist = 0;

                int[] minMaxDist = turbList.CalcMinMaxDistanceToTurbines(thisMap.minUTMX, thisMap.minUTMY);
                if (minMaxDist[1] > maxDist) maxDist = minMaxDist[1];

                minMaxDist = turbList.CalcMinMaxDistanceToTurbines(thisMap.minUTMX, thisMap.minUTMY + thisMap.numY * thisMap.reso);
                if (minMaxDist[1] > maxDist) maxDist = minMaxDist[1];

                minMaxDist = turbList.CalcMinMaxDistanceToTurbines(thisMap.minUTMX + thisMap.numX * thisMap.reso, thisMap.minUTMY);
                if (minMaxDist[1] > maxDist) maxDist = minMaxDist[1];

                minMaxDist = turbList.CalcMinMaxDistanceToTurbines(thisMap.minUTMX + thisMap.numX * thisMap.reso, thisMap.minUTMY + thisMap.numY * thisMap.reso);
                if (minMaxDist[1] > maxDist) maxDist = minMaxDist[1];
                                
                wakeCoeffs = wakeModelList.GetWakeLossesCoeffs(minDist, maxDist, thisMap.wakeModel, metList);
            }
                       

            for (int xind = 0; xind < numX; xind++)
            {
                double thisX = thisMap.minUTMX + xind * thisMap.reso;
                int minY = thisMap.minUTMY;
                int maxY = thisMap.minUTMY + thisMap.numY * thisMap.reso;

                nodesFromDB = nodeList.GetNodes(thisX, minY, thisX, maxY, thisInst, false);

                for (int yind = 0; yind <= numY - 1; yind++)
                {
                    mapNodeCount++;
                    if (mapNodeCount > 10)
                    {
                        timeElapsed = (thisStopwatch.Elapsed.TotalSeconds - timeElapsed);
                        avgTimePerNode = (thisStopwatch.Elapsed.TotalSeconds / (mapNodeCount + 1));
                        timeToFinish = (numMapNodes - mapNodeCount) * avgTimePerNode / 60;
                        textForProgBar = "Node " + mapNodeCount + "/" + numMapNodes + " Avg time/node: " + Math.Round(avgTimePerNode, 1) +
                            " secs." + " Est. time to finish: " + Math.Round(timeToFinish, 1) + " mins.";
                        int Prog = Convert.ToInt16(100.0f * mapNodeCount / numMapNodes);
                        BackgroundWorker_Map.ReportProgress(Prog, textForProgBar);
                    }
                    else
                    {
                        textForProgBar = "Generating map...";
                        BackgroundWorker_Map.ReportProgress((int)(mapNodeCount / (float)numMapNodes * 100), textForProgBar);
                    }

                    if (thisMap.parameterToMap[xind, yind] == 0)
                    {
                        double thisY = thisMap.minUTMY + yind * thisMap.reso;
                        Map.mapNode thisMapNode = new Map.mapNode();
                        thisMapNode.UTMX = thisX;
                        thisMapNode.UTMY = thisY;
                        Nodes thisNode = nodeList.GetANode(thisX, thisY, thisInst, ref nodesFromDB, null); // this looks through the list of nodes extracted and pulls from DB if needed   
                        thisMapNode.elev = thisNode.elev;
                        thisMapNode.expo = thisNode.expo;
                        thisMapNode.gridStats = thisNode.gridStats;

                        if (BackgroundWorker_Map.CancellationPending == true)
                        {
                            //  nodeList.AddNodes(Nodes_to_add, thisInst.savedParams.savedFileName);
                            e.Result = thisInst;
                            return;
                        }

                        if ((thisMapNode.expo == null) || (thisMapNode.gridStats.stats == null))
                        {
                            if (thisMapNode.expo == null)
                                thisMap.CalcMapExposures(ref thisMapNode, 1, thisInst);

                            if (thisMapNode.gridStats.stats == null)
                                thisMapNode.gridStats.GetGridArrayAndCalcStats(thisMapNode.UTMX, thisMapNode.UTMY, thisInst);

                            nodesToAdd[0] = nodeList.GetMapAsNode(thisMapNode);
                            nodeList.AddNodes(nodesToAdd, thisInst.savedParams.savedFileName);
                        }

                        if (thisMapNode.windRose == null)
                            thisMapNode.windRose = metList.GetInterpolatedWindRose(metList.GetMetsUsed(), thisMapNode.UTMX, thisMapNode.UTMY);

                        if (thisMap.useFlowSep == true) thisMap.GetFlowSepNodes(ref thisMapNode, thisInst, nodeList, null);

                        if (thisMap.modelType == 2 || thisMap.modelType == 3)
                            thisMap.DoMapCalcs(ref thisMapNode, thisInst, nodeList, pathsToMets, null, ref lastAllNodesInPath, "Best");
                        else if (thisMap.modelType == 4 || thisMap.modelType == 5)  // WS or AEP map using default model
                            thisMap.DoMapCalcs(ref thisMapNode, thisInst, nodeList, pathsToMets, null, ref lastAllNodesInPath, "Default");

                        // Combine WS ests from various mets into one average
                        if (thisMap.modelType >= 2)
                            thisMap.GenerateAvgWS_AtOneMapNode(ref thisMapNode, thisInst);

                        if ((thisMap.modelType == 5 || thisMap.modelType == 3) && thisMapNode.avgWS_Est != 0)
                        {
                            thisMap.CalcWS_DistAtMapNode(ref thisMapNode, metList, numWD);
                            thisMap.CalcGrossAEP_AtMapNode(ref thisMapNode, metList, turbList);
                        }

                        if (thisMap.isWaked)
                        {
                            thisMap.CalcWakeLossesMap(ref thisMapNode, thisInst, thisMap.wakeModel, wakeCoeffs);
                            wakeModelList.PopulateWakeGrid(thisMapNode, wakeGridInd);
                        }

                        // Finally, populate parameterToMap 
                        
                        if (thisMap.modelType == 0)  // UW exposure
                            thisMap.parameterToMap[xind, yind] = thisMapNode.expo[thisMap.expoMapRadius].GetOverallValue(thisMapNode.windRose, "Expo", "UW");
                        else if (thisMap.modelType == 1)  // DW Exposure
                            thisMap.parameterToMap[xind, yind] = thisMapNode.expo[thisMap.expoMapRadius].GetOverallValue(thisMapNode.windRose, "Expo", "DW");
                        else if (thisMap.modelType == 2 || thisMap.modelType == 4)
                        { // WS Best UWDW
                            thisMap.parameterToMap[xind, yind] = thisMapNode.avgWS_Est;
                            for (int WD = 0; WD < numWD; WD++)
                                thisMap.sectorParamToMap[xind, yind, WD] = thisMapNode.sectorWS[WD];
                        }
                        else if (thisMap.isWaked == false && (thisMap.modelType == 3 || thisMap.modelType == 5))  // AEP Best UWSW
                            thisMap.parameterToMap[xind, yind] = thisMapNode.grossAEP;
                        else if (thisMap.isWaked)
                        {
                            thisMap.parameterToMap[xind, yind] = thisMapNode.avgWS_Est;
                            for (int WD = 0; WD < numWD; WD++)
                                thisMap.sectorParamToMap[xind, yind, WD] = thisMapNode.sectorWS[WD];
                        }                        
                    }
                }
            }
                        
            thisMap.isComplete = true;
            if (thisMap.isWaked)
                wakeModelList.wakeGridMaps[wakeGridInd].isComplete = true;
                        
            e.Result = thisInst; 

        }

        private void BackgroundWorker_Map_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Updates the map generation progress bar
            BringToFront();
            string Text_for_label = e.UserState.ToString();
            progbar.Value = e.ProgressPercentage;
            Text = "Continuum 2.3";
            lblprogbar.Text = Text_for_label;
            Refresh();
        }

        private void BackgroundWorker_Map_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Updates map list and wake map list on main form
            Continuum thisInst = (Continuum)e.Result;
                   
                                
            updateThe.MapsTAB(thisInst);
            updateThe.NetTurbineEstsTAB(thisInst);

         //   if (e.Cancelled == false)
            thisInst.SaveFile();                

            Close();
        }

        public void Call_BW_RoundRobin(Vars_for_RoundRobin Args)
        {
            // Calls Math.Round Robin background worker
            Show();
            BackgroundWorker_RoundRobin.RunWorkerAsync(Args);            
        }

        private void BackgroundWorker_RoundRobin_DoWork(object sender, DoWorkEventArgs e)
        {
            // Conducts Math.Round Robin analysis for specified met subset size

            //  Dim The_args  Vars_for_RoundRobin = DirectCast(e.Argument, Vars_for_RoundRobin)
            Vars_for_RoundRobin theArgs = (Vars_for_RoundRobin)e.Argument;

            Continuum thisInst = theArgs.thisInst;
      
            MetCollection metList = thisInst.metList;
            MetPairCollection metPairList = thisInst.metPairList;
      
            int minRR_Size = theArgs.Min_RR_Size;           
            string[] metsUsed = metList.GetMetsUsed();
                                  
            int numMets = metsUsed.Length;                                        
                                 
            MetPairCollection.RR_funct_obj thisRR_obj = new MetPairCollection.RR_funct_obj();
            MetPairCollection.RR_funct_obj[] RR_obj_coll = new MetPairCollection.RR_funct_obj[1];

            string textForProgBar = "Preparing for Math.Round Robin Analysis";
            BackgroundWorker_RoundRobin.ReportProgress(0, textForProgBar);                    
            
            for (int n = metsUsed.Length - minRR_Size; n <= numMets - minRR_Size; n++)
            {
                int numMetsInModel = metsUsed.Length - n;
                int numMetsToPredict = numMets - numMetsInModel;
                bool RR_Done = metPairList.RR_DoneAlready(true, false, metsUsed, numMetsInModel, metList);

                if (RR_Done == false) {
                    if (numMetsInModel == 1) {
                        int numModels = numMets; // a single met model for each met
                        string[,] metsForModels = new string[1, numModels];

                        for (int i = 0; i <= numModels - 1; i++) {
                            string[] metsForThisModel = new string[1];
                            metsForThisModel[0] = metsUsed[i];
                            metsForModels[0, i] = metsUsed[i];
                            textForProgBar = "Calculating model error using " + (metsUsed.Length - 1).ToString() + " met sites";
                            BackgroundWorker_RoundRobin.ReportProgress(i / numModels, textForProgBar);

                            thisRR_obj = metPairList.DoRR_Calc(metsForThisModel, thisInst, metsUsed);
                            Array.Resize(ref RR_obj_coll, i + 1);
                            RR_obj_coll[i] = thisRR_obj;
                        }

                        metPairList.AddRoundRobinEst(RR_obj_coll, metsUsed, numMetsInModel, metsForModels, true, metList);
                    }
                    else { // more than one met
                        double N_fact = 1;

                        for (int i = numMets; i >= 1; i--)
                            N_fact = N_fact * i;

                        double K_fact = 1;
                        for (int j = numMetsInModel; j >= 1; j--)
                            K_fact = K_fact * j;                    

                        double N_minus_k_fact = 1;
                        for (int j = (numMets - numMetsInModel); j >= 1; j--)
                            N_minus_k_fact = N_minus_k_fact * j;

                        int numModels = (int)(N_fact / (K_fact * N_minus_k_fact));
                        string[,] metsForModels = metPairList.GetAllCombos(metsUsed, numMets, numMetsInModel, numModels);

                        for (int i = 0; i <= numModels - 1; i++) {
                            string[] metsForThisModel = new string[numMetsInModel];

                            for (int j = 0; j <= numMetsInModel - 1; j++)
                                metsForThisModel[j] = metsForModels[j, i];

                            textForProgBar = "Calculating model error using " + (metsUsed.Length - n).ToString() + " met sites : " + i + "/" + numModels;
                            BackgroundWorker_RoundRobin.ReportProgress(100 * i / numModels, textForProgBar);

                            thisRR_obj = metPairList.DoRR_Calc(metsForThisModel, thisInst, metsUsed);
                            Array.Resize(ref RR_obj_coll, i + 1);
                            RR_obj_coll[i] = thisRR_obj;

                            if (BackgroundWorker_RoundRobin.CancellationPending == true) {
                                e.Cancel = true;
                                e.Result = thisInst;
                                return;
                            }

                        }
                        metPairList.AddRoundRobinEst(RR_obj_coll, metsUsed, numMetsInModel, metsForModels, true, metList);
                    }
                }
            }
            e.Result = thisInst;
        }

        private void BackgroundWorker_RoundRobin_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Updates the Math.Round Robin progress bar
            string Text_for_label = e.UserState.ToString();
            progbar.Value = e.ProgressPercentage;
            Text = "Continuum 2.3";
            lblprogbar.Text = Text_for_label;
            Refresh();
        }

        private void BackgroundWorker_RoundRobin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Updates Math.Round Robin dropdown menu on main form

            if (e.Cancelled)
            {
            }
            else
            {                
                Continuum thisInst = (Continuum)e.Result;
                updateThe.Uncertainty_TAB_Round_Robin(thisInst);
                thisInst.ChangesMade();
            }
            
            Close();
        }                  

        public void Call_BW_SaveAs(Vars_for_Save_As Args)
        {
            // Calls //Save // background worker
            Show();
            BackgroundWorker_SaveAs.RunWorkerAsync(Args);
        }

        private void BackgroundWorker_SaveAs_DoWork(object sender, DoWorkEventArgs e)
        {
            // Saves a copy of database at new save location
            NodeCollection nodeList = new NodeCollection();

            Vars_for_Save_As The_args = (Vars_for_Save_As)e.Argument;
            string newFilename = The_args.newFilename;
            string oldFilename = The_args.oldFilename;

            string newConnString = nodeList.GetDB_ConnectionString(newFilename);
            string oldConnString = nodeList.GetDB_ConnectionString(oldFilename);

            // Check to see if there is a database to copy over
            if (File.Exists(oldFilename) == true)
            {
                string textForProgBar = "Preparing to copy database...";
                BackgroundWorker_SaveAs.ReportProgress(0, textForProgBar);
                // Copy Nodes (and expo, GridStats, MapNodes and MapWSEsts)
                int numNodes;
                                
                // Delete old database if there is one
                try
                {
                    using (var ctx = new Continuum_EDMContainer(newConnString))
                    {
                        if (ctx.Database.Exists())
                            ctx.Database.Delete();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());                    
                    return;
                }              

                // Copy over node data
                try
                {
                    using (var ctx = new Continuum_EDMContainer(oldConnString))
                    {
                        numNodes = ctx.Node_table.Count();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());                    
                    return;
                }

                int minId = 1;
                int maxId;
                Node_table[] nodeDB = new Node_table[2000];
                if (numNodes > 2000)
                    maxId = minId + 1999;
                else
                {
                    maxId = numNodes;
                    nodeDB = new Node_table[numNodes];
                }
                bool gotThemAll = false;

                while (gotThemAll == false && numNodes > 0)
                {
                    textForProgBar = "Saving node data...";
                    int prog = (int)(100 * (double)maxId / numNodes);
                    BackgroundWorker_SaveAs.ReportProgress(prog, textForProgBar);
                    int dataInd = 0;

                    // Copy 20000 entries from old DB
                    try
                    {
                        using (var ctx = new Continuum_EDMContainer(oldConnString))
                        {
                            for (int This_Id = minId; This_Id <= maxId; This_Id++)
                            {
                                var node_expo_db = from N in ctx.Node_table.Include("expo") where N.Id == This_Id select N;

                                foreach (var N in node_expo_db)
                                {
                                    nodeDB[dataInd] = new Node_table();
                                    nodeDB[dataInd].UTMX = N.UTMX;
                                    nodeDB[dataInd].UTMY = N.UTMY;
                                    nodeDB[dataInd].elev = N.elev;

                                    int numExpos = N.expo.Count();

                                    for (int expInd = 0; expInd < numExpos; expInd++)
                                    {
                                        Expo_table newExpo = new Expo_table();
                                        newExpo.Expo_Array = N.expo.ElementAt(expInd).Expo_Array;
                                        newExpo.ExpoDist_Array = N.expo.ElementAt(expInd).ExpoDist_Array;
                                        newExpo.radius = N.expo.ElementAt(expInd).radius;
                                        newExpo.exponent = N.expo.ElementAt(expInd).exponent;
                                        newExpo.UW_Cross_Grade = N.expo.ElementAt(expInd).UW_Cross_Grade;
                                        newExpo.UW_ParallelGrade = N.expo.ElementAt(expInd).UW_ParallelGrade;
                                        newExpo.SR_Array = N.expo.ElementAt(expInd).SR_Array;
                                        newExpo.DH_Array = N.expo.ElementAt(expInd).DH_Array;
                                        nodeDB[dataInd].expo.Add(newExpo);
                                    }
                                }

                                var node_gridstat_db = from N in ctx.Node_table.Include("GridStats") where N.Id == This_Id select N;

                                foreach (var N in node_gridstat_db)
                                {
                                    int numGridStats = N.GridStats.Count();
                                    for (int gridStatInd = 0; gridStatInd <= numGridStats - 1; gridStatInd++)
                                    {
                                        GridStat_table newGridStat = new GridStat_table();
                                        newGridStat.radius = N.GridStats.ElementAt(gridStatInd).radius;
                                        newGridStat.P10_UW = N.GridStats.ElementAt(gridStatInd).P10_UW;
                                        newGridStat.P10_DW = N.GridStats.ElementAt(gridStatInd).P10_DW;
                                        nodeDB[dataInd].GridStats.Add(newGridStat);
                                    }
                                }
                           
                                dataInd++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());                        
                        return;
                    }

                    try
                    {
                        using (var ctx = new Continuum_EDMContainer(newConnString))
                        {
                            ctx.Node_table.AddRange(nodeDB);
                            ctx.SaveChanges();
                            dataInd = 0;
                            nodeDB = new Node_table[2000];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());                        
                        return;
                    }

                    if (maxId == numNodes)
                        gotThemAll = true;

                    minId = maxId + 1;
                    maxId = maxId + 2000;

                    if (maxId > numNodes)
                    {
                        maxId = numNodes;
                        nodeDB = new Node_table[maxId - minId + 1];
                    }
                }

                // Copy over topo data
                int numTopo = 0;
                try
                {
                    using (var ctx = new Continuum_EDMContainer(oldConnString))
                    {
                        numTopo = ctx.Topo_table.Count();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());                    
                    return;
                }

                minId = 1;
                Topo_table[] topoDB = new Topo_table[20000];
                if (numTopo > 20000)
                    maxId = minId + 19999;
                else
                {
                    maxId = numTopo;
                    topoDB = new Topo_table[numTopo];
                }

                gotThemAll = false;

                while (gotThemAll == false)
                {
                    textForProgBar = "Saving topography data...";
                    int prog = (int)(100 * (double)maxId / numTopo);
                    BackgroundWorker_SaveAs.ReportProgress(prog, textForProgBar);
                    int dataInd = 0;
                    // Copy 20000 entries from old DB

                    try
                    {
                        using (var ctx = new Continuum_EDMContainer(oldConnString))
                        {
                            var topo_exist_db = from N in ctx.Topo_table where N.Id >= minId && N.Id <= maxId select N;

                            foreach (var N in topo_exist_db)
                            {
                                topoDB[dataInd] = new Topo_table();
                                topoDB[dataInd].Elevs = N.Elevs;                                
                                dataInd++;
                            }
                        }

                        using (var ctx = new Continuum_EDMContainer(newConnString))
                        {
                            ctx.Topo_table.AddRange(topoDB);
                            ctx.SaveChanges();
                            dataInd = 0;
                            topoDB = new Topo_table[20000];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());                        
                        return;
                    }

                    if (maxId == numTopo)
                        gotThemAll = true;

                    minId = maxId + 1;
                    maxId = maxId + 20000;

                    if (maxId > numTopo)
                    {
                        maxId = numTopo;
                        topoDB = new Topo_table[maxId - minId + 1];
                    }
                }

                // Copy over land cover data
                int numLC = 0;
                try
                {
                    using (var ctx = new Continuum_EDMContainer(oldConnString))
                    {
                        numLC = ctx.LandCover_table.Count();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                    return;
                }

                minId = 1;
                LandCover_table[] landCoverDB = new LandCover_table[20000];
                if (numLC > 20000)
                    maxId = minId + 19999;
                else
                {
                    maxId = numLC;
                    landCoverDB = new LandCover_table[numLC];
                }

                gotThemAll = false;

                while (numLC > 0 && gotThemAll == false)
                {
                    textForProgBar = "Saving land cover data...";
                    int prog = (int)(100 * (double)maxId / numLC);
                    BackgroundWorker_SaveAs.ReportProgress(prog, textForProgBar);
                    int dataInd = 0;
                    // Copy 20000 entries from old DB

                    try
                    {
                        using (var ctx = new Continuum_EDMContainer(oldConnString))
                        {
                            var lc_exist_db = from N in ctx.LandCover_table where N.Id >= minId && N.Id <= maxId select N;

                            foreach (var N in lc_exist_db)
                            {
                                landCoverDB[dataInd] = new LandCover_table();
                                landCoverDB[dataInd].LandCover = N.LandCover;                                
                                dataInd++;
                            }
                        }

                        using (var ctx = new Continuum_EDMContainer(newConnString))
                        {
                            ctx.LandCover_table.AddRange(landCoverDB);
                            ctx.SaveChanges();
                            dataInd = 0;
                            landCoverDB = new LandCover_table[20000];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                        return;
                    }

                    if (maxId == numLC)
                        gotThemAll = true;

                    minId = maxId + 1;
                    maxId = maxId + 20000;

                    if (maxId > numLC)
                    {
                        maxId = numLC;
                        topoDB = new Topo_table[maxId - minId + 1];
                    }
                }
            }
            else
            {
                // See if there is an existing DB and delete it if there is
                if (File.Exists(newFilename) == true)
                {
                    try
                    {
                        using (var ctx = new Continuum_EDMContainer(newConnString))
                        {
                            if (ctx.Database.Exists())
                                ctx.Database.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());                        
                        return;
                    }

                }
            }
        }


        private void BackgroundWorker_SaveAs_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Updates the 'Save As' progress bar
            string Text_for_label = e.UserState.ToString();
            if (e.ProgressPercentage <= 100)
                progbar.Value = e.ProgressPercentage;
            else
                progbar.Value = 100;

            Text = "Continuum 2.3";
            lblprogbar.Text = Text_for_label;
            Refresh();
        }

        private void BackgroundWorker_SaveAs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();     
        }

        public void Call_BW_Node_Recalc(Continuum thisInst)
        {
            // Calls .MAP import background worker
            Show();
            BackgroundWorker_Node_SR_Recalc.RunWorkerAsync(thisInst);
        }

        private void BackgroundWorker_Node_SR_Recalc_DoWork(object sender, DoWorkEventArgs e)
        {
            Continuum thisInst = (Continuum)e.Argument;

            // Recalculates SR/DH with new land cover key (or calculates SR/DH with newly imported land cover)
            Nodes nodeFromDB = new Nodes();
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);
            
            int nodeCount = 0;
            string textForProgBar = "Recalculating SR/DH at Nodes in database";
            BackgroundWorker_Node_SR_Recalc.ReportProgress(0, textForProgBar);

            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, true);

            try
            {
                using (var context = new Continuum_EDMContainer(connString))
                {
                    var node_db = from N in context.Node_table.Include("expo") select N;
                    int totalCount = node_db.Count();

                    foreach (var N in node_db)
                    {
                        int numExpo = N.expo.Count();
                        nodeFromDB.UTMX = N.UTMX;
                        nodeFromDB.UTMY = N.UTMY;

                        nodeFromDB.expo = new Exposure[numExpo];

                        if (nodeCount % 10 == 0)
                        {
                            textForProgBar = "Recalculating SR/DH at Nodes in database. Node: " + nodeCount + "/" + totalCount;
                            BackgroundWorker_Node_SR_Recalc.ReportProgress(100 * nodeCount / totalCount, textForProgBar);
                        }
                                                
                        for (int i = 0; i < numExpo; i++)
                        {
                            nodeFromDB.expo[i] = new Exposure();
                            nodeFromDB.expo[i].radius = N.expo.ElementAt(i).radius;
                            nodeFromDB.expo[i].exponent = N.expo.ElementAt(i).exponent;
                            int numSectors = 1;                                                                                    
                            int smallerRadius = thisInst.topo.GetSmallerRadius(nodeFromDB.expo, nodeFromDB.expo[i].radius, nodeFromDB.expo[i].exponent, numSectors);

                            if (smallerRadius == 0 || numSectors > 1)
                            { // when sector avg is used, can//t add on to exposure calcs...so gotta do it the long way
                                if (thisInst.topo.gotSR == true)
                                    thisInst.topo.CalcSRDH(ref nodeFromDB.expo[i], nodeFromDB.UTMX, nodeFromDB.UTMY, nodeFromDB.expo[i].radius, nodeFromDB.expo[i].exponent, thisInst.metList.numWD);
                            }
                            else
                            {
                                Exposure smallerExpo = thisInst.topo.GetSmallerRadiusExpo(nodeFromDB.expo, smallerRadius, nodeFromDB.expo[i].exponent, numSectors);                                                               
                                if (thisInst.topo.gotSR == true)
                                    thisInst.topo.CalcSRDHwithSmallerRadius(ref nodeFromDB.expo[i], nodeFromDB.UTMX, nodeFromDB.UTMY, nodeFromDB.expo[i].radius, nodeFromDB.expo[i].exponent, numSectors, smallerRadius, smallerExpo, thisInst.metList.numWD);

                            }                            

                            // Write back to expo_db
                            MemoryStream MS7 = new MemoryStream();
                            MemoryStream MS8 = new MemoryStream();
                            BinaryFormatter bin = new BinaryFormatter();

                            if (nodeFromDB.expo[i].SR != null)
                            {
                                bin.Serialize(MS7, nodeFromDB.expo[i].SR);
                                N.expo.ElementAt(i).SR_Array = MS7.ToArray();
                            }

                            if (nodeFromDB.expo[i].dispH != null)
                            {
                                bin.Serialize(MS8, nodeFromDB.expo[i].dispH);
                                N.expo.ElementAt(i).DH_Array = MS8.ToArray();
                            }
                        }

                        nodeCount++;
                    }

                    // Save DB
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());                
                return;
            }
        }

        private void BackgroundWorker_Node_SR_Recalc_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Updates progress bar of .MAP import

            string Text_for_label = e.UserState.ToString();
            if (e.ProgressPercentage <= 100)
                progbar.Value = e.ProgressPercentage;
            else
                progbar.Value = 100;

            Text = "Continuum 2.3";
            lblprogbar.Text = Text_for_label;
            BringToFront();
            Refresh();
        }

        private void BackgroundWorker_Node_SR_Recalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        public void Call_BW_WRG_create(Vars_for_WRG_file Args)
        {
            // Calls WRG file creator background worker
            Show();
            BackgroundWorker_WRG.RunWorkerAsync(Args);
        }        
        
        private void BackgroundWorker_WRG_DoWork(object sender, DoWorkEventArgs e)
        {
            // Generates WRG (Wind Resource Grid) file based on selected map  
            Vars_for_WRG_file theArgs = (Vars_for_WRG_file)(e.Argument);
            Continuum thisInst = theArgs.thisInst;
            MetCollection metList = thisInst.metList;
            MapCollection mapList = thisInst.mapList;
            string mapName = theArgs.mapName;
            string savedFilename = thisInst.savedParams.savedFileName;
            TurbineCollection turbList = thisInst.turbineList;
            double density = theArgs.density;
            ModelCollection modelList = thisInst.modelList;
            string outputFile = theArgs.outputFile;
            InvestCollection radiiList = thisInst.radiiList;
            TopoInfo topo = thisInst.topo;
            double[] windRose = metList.GetAvgWindRose();
            Map thisMap = new Map();
            double height = 0;

            if (mapList.ThisCount > 0) {
                for (int i = 0; i < mapList.ThisCount; i++) {
                    if (mapName == mapList.mapItem[i].mapName)
                    {  // Found map
                        thisMap = mapList.mapItem[i];

                        if (thisMap.modelType != 2 && thisMap.modelType != 4)
                        {
                            if (thisMap.isWaked == false)
                            {
                                string modelType = "";
                                if (thisMap.modelType == 0)
                                    modelType = "UW Exposure";
                                else if (thisMap.modelType == 1)
                                    modelType = "DW Exposure";
                                else if ((thisMap.modelType == 3 || thisMap.modelType == 5) && thisMap.isWaked == false)
                                    modelType = "Energy Production";

                                MessageBox.Show("WRG maps are created using WS maps.  Please select a WS map and not a map of " + modelType);
                                return;
                            }                            
                        }

                        break;
                    }
                }
            }
            else {
                MessageBox.Show("No maps defined.", "Continuum 2.3");
                return;
            }

            if (metList.ThisCount > 0)
                height = metList.metItem[0].height;
            else {
                MessageBox.Show("There are no met sites loaded.", "Continuum 2.3");
                return;
            }

            StreamWriter sw = new StreamWriter(outputFile);                   
                        
            double weibull_A_10 = 0;            
            double weibull_k_100 = 0;   

            int numX = thisMap.numX;
            int numY = thisMap.numY;
            double minUTMX = thisMap.minUTMX;
            double minUTMY = thisMap.minUTMY;
            int reso = thisMap.reso;

            string numX_String = numX.ToString();
            numX_String = numX_String.PadRight(8);

            string numY_String = numY.ToString();
            numY_String = numY_String.PadRight(8);

            string Min_UTMX_Str = minUTMX.ToString();
            Min_UTMX_Str = Min_UTMX_Str.PadRight(8);

            string Min_UTMY_Str = minUTMY.ToString();
            Min_UTMY_Str = Min_UTMY_Str.PadRight(8);

            string resoString = reso.ToString();

            sw.Write(numX_String);
            sw.Write(numY_String);
            sw.Write(Min_UTMX_Str);
            sw.Write(Min_UTMY_Str);
            sw.Write(resoString);
            sw.WriteLine();
                                    
            double[] power = null;

            if (turbList.PowerCurveCount > 0)            
                power = turbList.powerCurves[0].power;

            int numPoints = numX * numY;
            int pointInd = 0;
            string[] metsUsed = metList.GetMetsUsed();                       
                        
            double[,] thisSectDist = null;

            // Get elevs for calcs
            topo.GetElevsAndSRDH_ForCalcs(thisInst, thisMap, false);

            for (int xind = 0; xind <= numX - 1; xind++)
            {
                double thisX = thisMap.minUTMX + xind * thisMap.reso;
                double minY = thisMap.minUTMY;
                double maxY = thisMap.minUTMY + (numY - 1) * thisMap.reso;

                for (int yind = 0; yind <= numY - 1; yind++)
                {
                    if (BackgroundWorker_WRG.CancellationPending == true) {
                        sw.Close();                        
                        return;
                    }

                    double thisY = thisMap.minUTMY + yind * thisMap.reso;
                    pointInd++;
                    BackgroundWorker_WRG.ReportProgress(100 * pointInd / numPoints);

                    string easting = thisX.ToString();
                    easting = easting.PadRight(10);

                    string northing = thisY.ToString();
                    northing = northing.PadRight(10);

                    string elev = Math.Round(topo.CalcElevs(thisX, thisY), 2).ToString();
                    elev = elev.PadRight(8);

                    string heightString = height.ToString();
                    heightString = heightString.PadRight(5);

                    int numWS = metList.metItem[0].WS_Dist.Length;
                    int numWD = windRose.Length;
                    thisSectDist = new double[numWD, numWS];                    
                    double[] thisWindRose = metList.GetInterpolatedWindRose(metsUsed, thisX, thisY);
                    
                    for (int WD = 0; WD <= numWD - 1; WD++)
                    {
                        double[] This_WS_Dist = metList.CalcWS_DistForTurbOrMap(metsUsed, thisMap.sectorParamToMap[xind, yind, WD], WD);

                        for (int WS = 0; WS <= numWS - 1; WS++) 
                            thisSectDist[WD, WS] = This_WS_Dist[WS] * 1000;
                    }

                    double[] thisDist = metList.CalcOverallWS_Dist(thisSectDist, thisWindRose);
                    MetCollection.Weibull_params weibull = metList.CalcWeibullParams(thisDist, thisSectDist, thisMap.parameterToMap[xind, yind]);

                    string weibull_k_string = Math.Round(weibull.overall_k, 3).ToString();
                    weibull_k_string = weibull_k_string.PadRight(6);

                    string weibull_A_string = Math.Round(weibull.overall_A, 2).ToString();
                    weibull_A_string = weibull_A_string.PadRight(5);

                    double powerDensity = 0.5f * density * Math.Pow(thisMap.parameterToMap[xind, yind], 3);
                    string powerDensityString = Math.Round(powerDensity, 4).ToString();
                    powerDensityString = powerDensityString.PadRight(15);

                    string numSectorsString = numWD.ToString();
                    numSectorsString = numSectorsString.PadRight(3);

                    sw.Write("GridPoint ");
                    sw.Write(easting);
                    sw.Write(northing);
                    sw.Write(elev);
                    sw.Write(heightString);
                    sw.Write(weibull_A_string);
                    sw.Write(weibull_k_string);
                    sw.Write(powerDensityString);
                    sw.Write(numSectorsString);

                    for (int k = 0; k <= numWD - 1; k++) {

                        string freqString = Math.Round(windRose[k] * 1000, 0).ToString();
                        freqString = freqString.PadRight(4);
                        sw.Write(freqString);

                        if (weibull.sector_A != null)
                            weibull_A_10 = weibull.sector_A[k] * 10;
                        else
                            weibull_A_10 = 0;

                        string weibull_A_10_string = Math.Round(weibull_A_10, 0).ToString();
                        weibull_A_10_string = weibull_A_10_string.PadRight(4);
                        sw.Write(weibull_A_10_string);

                        if (weibull.sector_k != null)
                            weibull_k_100 = weibull.sector_k[k] * 100;
                        else
                            weibull_k_100 = 0;

                        string Weibull_k_100_str = Math.Round(weibull_k_100, 0).ToString();
                        Weibull_k_100_str = Weibull_k_100_str.PadRight(5);
                        sw.Write(Weibull_k_100_str);
                    }

                    sw.WriteLine();
                }
            }

            sw.Close();
        }

        private void BackgroundWorker_WRG_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Updates progress bar of WRG file background worker
            string Text_for_label = "Creating WRG file...";

            if (e.ProgressPercentage <= 100)
                progbar.Value = e.ProgressPercentage;
            else
                progbar.Value = 100;

            Text = "Continuum 2.3";
            lblprogbar.Text = Text_for_label;
            Refresh();
        }
        
        private void BackgroundWorker_WRG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Closes background worker after WRG file complete
            if (e.Cancelled)
                Close();
            else
                Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Cancels background worker and closes progress bar
            DialogResult Yes_or_no = new DialogResult();

            if (BackgroundWorker_Map.IsBusy == true)
            {
                Yes_or_no = MessageBox.Show("Are you sure that you want to cancel this map generation?", "Continuum 2.3", MessageBoxButtons.YesNo);
                if (Yes_or_no == DialogResult.Yes)             
                    BackgroundWorker_Map.CancelAsync();
                
            }
            else if (BackgroundWorker_TurbCalcs.IsBusy == true)
            {
                Yes_or_no = MessageBox.Show("Are you sure that you want to cancel the turbine estimate calculations?", "Continuum 2.3", MessageBoxButtons.YesNo);
                if (Yes_or_no == DialogResult.Yes)
                    BackgroundWorker_TurbCalcs.CancelAsync();
                
            }
            else if (BackgroundWorker_RoundRobin.IsBusy == true)
            {
                Yes_or_no = MessageBox.Show("Are you sure that you want to cancel the round robin uncertainty analysis?", "Continuum 2.3", MessageBoxButtons.YesNo);
                if (Yes_or_no == DialogResult.Yes)
                    BackgroundWorker_RoundRobin.CancelAsync();
            }
            else if (BackgroundWorker_MetCalcs.IsBusy == true)
            {
                Yes_or_no = MessageBox.Show("Are you sure that you want to cancel this met exposure and statistics calculation?", "Continuum 2.3", MessageBoxButtons.YesNo);
                if (Yes_or_no == DialogResult.Yes)
                    BackgroundWorker_MetCalcs.CancelAsync();
            }
            else if (BackgroundWorker_Topo.IsBusy == true)
            {
                Yes_or_no = MessageBox.Show("Are you sure that you want to cancel the topo data import?  All data loaded in so far will be removed from database.", "Continuum 2.3", MessageBoxButtons.YesNo);
                if (Yes_or_no == DialogResult.Yes)
                    BackgroundWorker_Topo.CancelAsync();
            }
            else if (BackgroundWorker_WAsP_Map.IsBusy == true)
            {
                Yes_or_no = MessageBox.Show("Are you sure that you want to cancel the roughness data import?", "Continuum 2.3", MessageBoxButtons.YesNo);
                if (Yes_or_no == DialogResult.Yes)
                    BackgroundWorker_WAsP_Map.CancelAsync();
            }

        }
        
        public bool IsBusy()
        {
            bool Busy = false;

            if (BackgroundWorker_LandCover.IsBusy || BackgroundWorker_Map.IsBusy || BackgroundWorker_MetCalcs.IsBusy || BackgroundWorker_Node_SR_Recalc.IsBusy
                || BackgroundWorker_RoundRobin.IsBusy || BackgroundWorker_SaveAs.IsBusy || BackgroundWorker_Topo.IsBusy || BackgroundWorker_TurbCalcs.IsBusy
                || BackgroundWorker_WAsP_Map.IsBusy || BackgroundWorker_WRG.IsBusy)
                Busy = true;

            return Busy;
        }
    }
}
