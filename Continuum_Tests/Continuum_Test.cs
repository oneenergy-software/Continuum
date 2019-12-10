using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Threading;
using System.IO;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Diagnostics;

namespace Continuum_Tests
{
    [TestClass]
    public class Continuum_Test
    {
        // Unit tests that run through model creation
        string testingFolder = "C:\\Users\\Liz\\Desktop\\Continuum 3 GUI Testing\\TestFolder";
        string merraFolder = "C:\\Users\\Liz\\Desktop\\MERRA2";

        [TestMethod]
        public void CreateModelWithOneTABAndMap()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = testingFolder + "\\Continuum One TAB And Map test";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.SaveFile(true);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay Topo.tif";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.topo.gotTopo == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            /*  TimeSpan ts = stopWatch.Elapsed;                        
              string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                  ts.Hours, ts.Minutes, ts.Seconds,
                  ts.Milliseconds / 10);

                Console.WriteLine("Loaded topo data " + elapsedTime);
                stopWatch.Restart();
            */
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay Land Cover.tif";

            Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.topo.gotSR == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            /*   ts = stopWatch.Elapsed;
               elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                   ts.Hours, ts.Minutes, ts.Seconds,
                   ts.Milliseconds / 10);
               MessageBox.Show("Loaded land cover data " + elapsedTime);
               stopWatch.Restart();
   */
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs(thisInst);

            if (thisInst.topo.gotTopo == false)
                return;

            if (thisInst.chkUseSR.Checked == true && thisInst.topo.gotSR == true)
                thisInst.topo.useSR = true;
            else
                thisInst.topo.useSR = false;

            if (thisInst.chk_Use_Sep.Checked == true)
                thisInst.topo.useSepMod = true;
            else
                thisInst.topo.useSepMod = false;

            // Call background worker to run calculations
            // In background worker, performs exposure and grid stat calculations, finds path of nodes and does site calibration.
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
            theArgs.thisInst = thisInst;
            theArgs.MCP_type = thisInst.Get_MCP_Method();
            thisInst.BW_worker.Call_BW_MetCalcs(theArgs);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Generate map

            GenMap newMap = new GenMap(thisInst);
            newMap.cboWhatToMap.SelectedIndex = 2;

            // if there are existing maps, use the coordinates from the last one created
            int numMaps = thisInst.mapList.ThisCount;

            if (numMaps > 0 && thisInst.savedParams.genMapMinUTMX == 0)
            {
                newMap.gridReso = thisInst.mapList.mapItem[numMaps - 1].reso;
                newMap.minUTMX = thisInst.mapList.mapItem[numMaps - 1].minUTMX;
                newMap.maxUTMX = newMap.minUTMX + (thisInst.mapList.mapItem[numMaps - 1].numX - 1) * thisInst.mapList.mapItem[numMaps - 1].reso;
                newMap.minUTMY = thisInst.mapList.mapItem[numMaps - 1].minUTMY;
                newMap.maxUTMY = newMap.minUTMY + (thisInst.mapList.mapItem[numMaps - 1].numY - 1) * thisInst.mapList.mapItem[numMaps - 1].reso;
            }
            else if (thisInst.savedParams.genMapMinUTMX != 0)
            {
                newMap.minUTMX = thisInst.savedParams.genMapMinUTMX;
                newMap.maxUTMX = thisInst.savedParams.genMapMaxUTMX;
                newMap.minUTMY = thisInst.savedParams.genMapMinUTMY;
                newMap.maxUTMY = thisInst.savedParams.genMapMaxUTMY;
                newMap.gridReso = thisInst.savedParams.genMapReso;
            }
            else
                newMap.gridReso = 250;

            newMap.gridReso = 2000;
            newMap.txtMapReso.Text = newMap.gridReso.ToString();
            newMap.FindLargestArea();
            newMap.UpdateTextboxes();
            newMap.GetBiggestArea();

            newMap.chkMetsToUse.Items.Clear();
            Met thisMet = null;

            for (int j = 0; j < thisInst.metList.ThisCount; j++)
            {
                thisMet = thisInst.metList.metItem[j];
                newMap.chkMetsToUse.Items.Add(thisMet.name, true);
            }

            thisInst.metList.AreAllMetsMCPd();

            if (thisInst.metList.isTimeSeries == false || thisInst.metList.allMCPd == false)
            {
                newMap.cboUseTimeSeries.SelectedIndex = 0; // Use Avg Dist.
                newMap.cboUseTimeSeries.Enabled = false; // User has no choice if not time series model
            }
            else
            {
                newMap.cboUseTimeSeries.SelectedIndex = 0; // Use Avg Dist.
                newMap.cboUseTimeSeries.Enabled = true; // User can choose to generate map using average distributions or using GenerateTimeSeries
            }

            newMap.GetMapSettings();
            thisInst.mapList.AddMap(newMap.mapName, newMap.minUTMX, newMap.minUTMY, newMap.gridReso, newMap.numX, newMap.numY, newMap.whatToMap,
                newMap.powerCurve, thisInst, false, new Wake_Model(), newMap.metsUsed, newMap.models, newMap.useTimeSeries);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(1000);
                        
            thisInst.BW_worker.Close();

            Assert.AreEqual(thisInst.mapList.ThisCount, numMaps + 1, "Didn't create map");   
            
            
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            MessageBox.Show("Finished test " + elapsedTime);
            stopWatch.Restart();

        }

        [TestMethod]
        public void CreateModelWithOneTABAndMapAroundTurb()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = testingFolder + "\\Continuum One TAB And Map Around Turb test";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.SaveFile(true);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay Topo.tif";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.topo.gotTopo == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();
                        
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay Land Cover.tif";

            Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.topo.gotSR == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Ball 2 Turbines.csv";

            thisInst.LoadTurbines(turbineFile);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs(thisInst);

            if (thisInst.topo.gotTopo == false)
                return;

            if (thisInst.chkUseSR.Checked == true && thisInst.topo.gotSR == true)
                thisInst.topo.useSR = true;
            else
                thisInst.topo.useSR = false;

            if (thisInst.chk_Use_Sep.Checked == true)
                thisInst.topo.useSepMod = true;
            else
                thisInst.topo.useSepMod = false;

            // Call background worker to run calculations
            // In background worker, performs exposure and grid stat calculations, finds path of nodes and does site calibration.
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
            theArgs.thisInst = thisInst;
            theArgs.MCP_type = thisInst.Get_MCP_Method();
            thisInst.BW_worker.Call_BW_MetCalcs(theArgs);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Generate map

            GenMap newMap = new GenMap(thisInst);
            newMap.cboWhatToMap.SelectedIndex = 2;

            // if there are existing maps, use the coordinates from the last one created
            int numMaps = thisInst.mapList.ThisCount;

            if (numMaps > 0 && thisInst.savedParams.genMapMinUTMX == 0)
            {
                newMap.gridReso = thisInst.mapList.mapItem[numMaps - 1].reso;
                newMap.minUTMX = thisInst.mapList.mapItem[numMaps - 1].minUTMX;
                newMap.maxUTMX = newMap.minUTMX + (thisInst.mapList.mapItem[numMaps - 1].numX - 1) * thisInst.mapList.mapItem[numMaps - 1].reso;
                newMap.minUTMY = thisInst.mapList.mapItem[numMaps - 1].minUTMY;
                newMap.maxUTMY = newMap.minUTMY + (thisInst.mapList.mapItem[numMaps - 1].numY - 1) * thisInst.mapList.mapItem[numMaps - 1].reso;
            }
            else if (thisInst.savedParams.genMapMinUTMX != 0)
            {
                newMap.minUTMX = thisInst.savedParams.genMapMinUTMX;
                newMap.maxUTMX = thisInst.savedParams.genMapMaxUTMX;
                newMap.minUTMY = thisInst.savedParams.genMapMinUTMY;
                newMap.maxUTMY = thisInst.savedParams.genMapMaxUTMY;
                newMap.gridReso = thisInst.savedParams.genMapReso;
            }
            else
                newMap.gridReso = 250;

            newMap.gridReso = 100;
            newMap.txtMapReso.Text = newMap.gridReso.ToString();
            newMap.FindLargestArea();
            newMap.UpdateTextboxes();
            newMap.FindAreaAroundTurbines();

            newMap.chkMetsToUse.Items.Clear();
            Met thisMet = null;

            for (int j = 0; j < thisInst.metList.ThisCount; j++)
            {
                thisMet = thisInst.metList.metItem[j];
                newMap.chkMetsToUse.Items.Add(thisMet.name, true);
            }

            thisInst.metList.AreAllMetsMCPd();

            if (thisInst.metList.isTimeSeries == false || thisInst.metList.allMCPd == false)
            {
                newMap.cboUseTimeSeries.SelectedIndex = 0; // Use Avg Dist.
                newMap.cboUseTimeSeries.Enabled = false; // User has no choice if not time series model
            }
            else
            {
                newMap.cboUseTimeSeries.SelectedIndex = 0; // Use Avg Dist.
                newMap.cboUseTimeSeries.Enabled = true; // User can choose to generate map using average distributions or using GenerateTimeSeries
            }

            newMap.GetMapSettings();
            thisInst.mapList.AddMap(newMap.mapName, newMap.minUTMX, newMap.minUTMY, newMap.gridReso, newMap.numX, newMap.numY, newMap.whatToMap,
                newMap.powerCurve, thisInst, false, new Wake_Model(), newMap.metsUsed, newMap.models, newMap.useTimeSeries);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            Assert.AreEqual(thisInst.mapList.ThisCount, numMaps + 1, "Didn't create map");


            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            MessageBox.Show("Finished test " + elapsedTime);
            stopWatch.Restart();

        }

        [TestMethod]
        public void CreateModelWithOneTAB()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            Continuum thisInst = new Continuum();

            string fileName = testingFolder + "\\Continuum One TAB test";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.SaveFile(true);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay Topo.tif";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.topo.gotTopo == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();
                      
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();            
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay Land Cover.tif";
            
            Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.topo.gotSR == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();
                       
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder +"\\Met TAB files\\Archbold Findlay coords.TAB";            
            thisInst.ImportMetsTAB();
                        
            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs(thisInst);

            if (thisInst.topo.gotTopo == false)
                return;

            if (thisInst.chkUseSR.Checked == true && thisInst.topo.gotSR == true)
                thisInst.topo.useSR = true;
            else
                thisInst.topo.useSR = false;

            if (thisInst.chk_Use_Sep.Checked == true)
                thisInst.topo.useSepMod = true;
            else
                thisInst.topo.useSepMod = false;

            // Call background worker to run calculations
            // In background worker, performs exposure and grid stat calculations, finds path of nodes and does site calibration.
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
            theArgs.thisInst = thisInst;
            theArgs.MCP_type = thisInst.Get_MCP_Method();
            thisInst.BW_worker.Call_BW_MetCalcs(theArgs);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Whirlpool W1.csv";
            thisInst.LoadTurbines(turbineFile);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 87-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Generate turbine wind speed and gross energy estimates
            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

            argsForBW.thisInst = thisInst;
            argsForBW.thisWakeModel = null;            

            // Call background worker to run calculations
            thisInst.BW_worker = new BackgroundWork();
            string MCP_Method = thisInst.Get_MCP_Method();
            argsForBW.MCP_Method = MCP_Method;
            thisInst.BW_worker.Call_BW_TurbCalcs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Do Exceedance calcs
            // Delete 10 of the curves to speed up calcs
            for (int i = 0; i < 10; i++)
                thisInst.turbineList.exceed.Delete_Exceed(thisInst.lstDefinedLosses.Items[i].Text);

            // Reduce the number simulations to 1000
            thisInst.turbineList.exceed.numSims = 1000;

            BackgroundWork.Vars_for_BW varsForBW = new BackgroundWork.Vars_for_BW();
            varsForBW.thisInst = thisInst;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_Exceed(varsForBW);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Create Eddy Viscosity Wake Model
            Gen_WakeModel thisWake = new Gen_WakeModel(thisInst);

            thisWake.cboWakeCombo.SelectedIndex = 0;

            thisWake.cboWakeModel.SelectedIndex = 0;
            thisWake.cboPowerCrvs.SelectedIndex = 0;
            thisWake.txtHorizWakeExp.Text = "5";
            thisWake.txtAmbTI.Text = "10";
            
            
            thisWake.GenWakeModel();

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Create wake map
            GenWakeMap wakeMap = new GenWakeMap(thisInst);

            wakeMap.cboWakeModels.Items.Clear();
            for (int i = 0; i <= thisInst.wakeModelList.NumWakeModels - 1; i++)
            {
                string Wake_String = thisInst.wakeModelList.CreateWakeModelString(thisInst.wakeModelList.wakeModels[i]);
                wakeMap.cboWakeModels.Items.Add(Wake_String);
            }                     

            wakeMap.cboWakeModels.SelectedIndex = 0;
            wakeMap.cboUseTimeSeries.SelectedIndex = 0; // Use Avg Dist.
            wakeMap.gridReso = 250;
            wakeMap.UpdateLimits();            
            wakeMap.GetCoordsAroundTurbs();
            wakeMap.GenerateWakeMap();

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();
                        

        }

        [TestMethod]
        public void CreateModelwithOneTS()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = testingFolder + "\\Continuum Met TS test";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.SaveFile(true);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay Topo.tif";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.topo.gotTopo == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            // Called in BackgroundWorker RunWorkerCompleted
            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
                        
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay Land Cover.tif";

            Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.topo.gotSR == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
                        
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            string metTSFile = testingFolder + "\\Met TS files\\Archbold TS Findlay coords.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.isTimeSeries = true;
            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            string metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            Met thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Analyze mets
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
            theArgs.thisInst = thisInst;
            theArgs.MCP_type = thisInst.Get_MCP_Method();
            thisInst.BW_worker.Call_BW_MetCalcs(theArgs);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Whirlpool W1.csv";
            thisInst.LoadTurbines(turbineFile);
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 87-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Generate turbine wind speed and gross energy estimates
            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

            argsForBW.thisInst = thisInst;
            argsForBW.thisWakeModel = null;

            // Call background worker to run calculations
            thisInst.BW_worker = new BackgroundWork();
            string MCP_Method = thisInst.Get_MCP_Method();
            argsForBW.MCP_Method = MCP_Method;
            thisInst.BW_worker.Call_BW_TurbCalcs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Do Exceedance calcs
            // Delete 10 of the curves to speed up calcs
            for (int i = 0; i < 10; i++)
                thisInst.turbineList.exceed.Delete_Exceed(thisInst.lstDefinedLosses.Items[i].Text);

            // Reduce the number simulations to 1000
            thisInst.turbineList.exceed.numSims = 1000;

            BackgroundWork.Vars_for_BW varsForBW = new BackgroundWork.Vars_for_BW();
            varsForBW.thisInst = thisInst;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_Exceed(varsForBW);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Create Eddy Viscosity Wake Model
            Gen_WakeModel thisWake = new Gen_WakeModel(thisInst);

            thisWake.cboWakeCombo.SelectedIndex = 0;

            thisWake.cboWakeModel.SelectedIndex = 0;
            thisWake.cboPowerCrvs.SelectedIndex = 0;
            thisWake.txtHorizWakeExp.Text = "5";
            thisWake.txtAmbTI.Text = "10";
            
            
            thisWake.GenWakeModel();

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Create wake map
            GenWakeMap wakeMap = new GenWakeMap(thisInst);

            wakeMap.cboWakeModels.Items.Clear();
            for (int i = 0; i <= thisInst.wakeModelList.NumWakeModels - 1; i++)
            {
                string Wake_String = thisInst.wakeModelList.CreateWakeModelString(thisInst.wakeModelList.wakeModels[i]);
                wakeMap.cboWakeModels.Items.Add(Wake_String);
            }

            wakeMap.cboWakeModels.SelectedIndex = 0;
            wakeMap.cboUseTimeSeries.SelectedIndex = 0; // Use Avg Dist.
            wakeMap.gridReso = 250;
            wakeMap.UpdateLimits();
            wakeMap.GetCoordsAroundTurbs();
            wakeMap.GenerateWakeMap();

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);

   /*         TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            MessageBox.Show("Finished test " + elapsedTime);
            stopWatch.Restart();  */
        }

        [TestMethod]
        public void CreateModelWithZones()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = testingFolder + "\\Continuum Zone test";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.SaveFile(true);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Archbold Findlay coords.TAB";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";
            
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs(thisInst);

            if (thisInst.topo.gotTopo == true)
            {
                if (thisInst.chkUseSR.Checked == true && thisInst.topo.gotSR == true)
                    thisInst.topo.useSR = true;
                else
                    thisInst.topo.useSR = false;

                if (thisInst.chk_Use_Sep.Checked == true)
                    thisInst.topo.useSepMod = true;
                else
                    thisInst.topo.useSepMod = false;

                // Call background worker to run calculations
                // In background worker, performs exposure and grid stat calculations, finds path of nodes and does site calibration.
                thisInst.BW_worker = new BackgroundWork();
                BackgroundWork.Vars_for_MetCalcs metArgs = new BackgroundWork.Vars_for_MetCalcs();
                metArgs.thisInst = thisInst;
                metArgs.MCP_type = thisInst.Get_MCP_Method();
                thisInst.BW_worker.Call_BW_MetCalcs(metArgs);

                while (thisInst.BW_worker.DoWorkDone == false)
                    Thread.Sleep(1000);

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay Topo.tif";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.topo.gotTopo == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Analyze mets
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
            theArgs.thisInst = thisInst;
            theArgs.MCP_type = thisInst.Get_MCP_Method();
            thisInst.BW_worker.Call_BW_MetCalcs(theArgs);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Whirlpool W1.csv";
            thisInst.LoadTurbines(turbineFile);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";

            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Generate turbine wind speed and gross energy estimates
            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

            argsForBW.thisInst = thisInst;
            argsForBW.thisWakeModel = null;

            // Call background worker to run calculations
            thisInst.BW_worker = new BackgroundWork();
            string MCP_Method = thisInst.Get_MCP_Method();
            argsForBW.MCP_Method = MCP_Method;
            thisInst.BW_worker.Call_BW_TurbCalcs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].avgWS_Est[0].freeStream.WS, 0, "Didn't calculate average WS");

            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Run ice throw model
            int numIceThrowsPerDay = 100;
            int numIceDaysPerYear = 2;

            if (numIceThrowsPerDay != -999 && numIceDaysPerYear != -999)
            {
                thisInst.siteSuitability.numIceDaysPerYear = numIceDaysPerYear;
                thisInst.siteSuitability.iceThrowsPerIceDay = numIceThrowsPerDay;

                BackgroundWork.Vars_for_BW varsForBW = new BackgroundWork.Vars_for_BW();
                varsForBW.thisInst = thisInst;
                thisInst.BW_worker = new BackgroundWork();
                thisInst.BW_worker.Call_BW_IceThrow(varsForBW);
            }

            while (thisInst.BW_worker.DoWorkDone == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            Assert.AreNotEqual(thisInst.siteSuitability.yearlyIceHits[0].iceHits.Length, 0, "Didn't calculate ice throw");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Run shadow flicker model
            thisInst.siteSuitability.RunShadowFlickerModel(thisInst);

            Assert.AreNotEqual(thisInst.siteSuitability.flickerMap.Length, 0, "Didn't calculate shadow flicker");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Run turbine noise model

            double turbineSound = thisInst.GetTurbineNoise();

            if (turbineSound != -999)
            {
                thisInst.siteSuitability.turbineSound = turbineSound;
                thisInst.siteSuitability.CreateSoundMap(thisInst);
                thisInst.updateThe.SoundMap(thisInst);
                thisInst.updateThe.SoundAtZones(thisInst);
                thisInst.updateThe.SiteSuitabilityDropdown(thisInst, "Sound");
                thisInst.updateThe.ColoredButtons(thisInst);
                thisInst.updateThe.SiteSuitabilityVisibility(thisInst);
            }

            Assert.AreNotEqual(thisInst.siteSuitability.soundMap.Length, 0, "Didn't calculate noise map");

     /*       TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            MessageBox.Show("Finished test " + elapsedTime);
            stopWatch.Restart();
*/
        }

        [TestMethod]
        public void CreateModelWithOneTSAndMERRAAndMCP()
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Create model
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

	        Continuum thisInst = new Continuum();

            string fileName = testingFolder + "\\Continuum One TS with MERRA and MCP";

	        if (File.Exists(fileName + ".cfm"))
	        {
	        File.Delete(fileName + ".cfm");
	        File.Delete(fileName + ".mdf");
	        File.Delete(fileName + "_log.ldf");
	        }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
	        thisInst.SaveFile(true);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay Topo.tif";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.BW_worker.DoWorkDone == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();
                        
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay Land Cover.tif";

            Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();
                        
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            string metTSFile = testingFolder + "\\Met TS files\\Archbold TS short Findlay coords.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.isTimeSeries = true;
            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            string metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            Met thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Load MERRA
            Met metMERRA = thisInst.metList.metItem[0];
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(metMERRA.UTMX, metMERRA.UTMY);
            int offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
            thisInst.merraList.startDate = Convert.ToDateTime("1/1/2005 12:00 am");
            thisInst.dateMERRAStart.Value = thisInst.merraList.startDate;
            thisInst.merraList.endDate = Convert.ToDateTime("12/31/2010 11:0 pm");
            thisInst.dateMERRAEnd.Value = thisInst.merraList.endDate;
            
            thisInst.merraList.AddMERRA_GetDataFromTextFiles(theseLL.latitude, theseLL.longitude, offset, thisInst, metMERRA, true);

            while (thisInst.BW_worker.DoWorkDone == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            Assert.AreNotEqual(thisInst.merraList.numMERRA_Nodes, 0, "Didn't load MERRA data");

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Do MCP
            Met mcpMet = thisInst.metList.metItem[0];
            string MCP_Method = thisInst.Get_MCP_Method();
            UTM_conversion.Lat_Long mcpLL = thisInst.UTM_conversions.UTMtoLL(mcpMet.UTMX, mcpMet.UTMY);
            int mcpOffset = thisInst.UTM_conversions.GetUTC_Offset(mcpLL.latitude, mcpLL.longitude);
            MERRA thisMERRA = thisInst.merraList.GetMERRA(mcpLL.latitude, mcpLL.longitude);
            thisMet.WSWD_Dists = new Met.WSWD_Dist[0];
            thisInst.metList.RunMCP(ref thisMet, thisMERRA, thisInst, MCP_Method);
            thisMet.CalcAllLT_WSWD_Dists(thisInst, thisMet.mcp.LT_WS_Ests); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)

            thisInst.metList.isMCPd = true;
            thisInst.metList.AreAllMetsMCPd();

            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreNotEqual(thisInst.metList.metItem[0].mcp, null, "Didn't do MCP calc");

      /*      TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            MessageBox.Show("Finished test " + elapsedTime);
            stopWatch.Restart();
*/
        }

        [TestMethod]
        public void CreateModelWithOneTABDeleteOne()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = testingFolder + "\\Continuum One TAB Delete One test";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.SaveFile(true);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay Topo.tif";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.topo.gotTopo == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();
                        
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay Land Cover.tif";

            Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.topo.gotSR == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();
                       
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs(thisInst);

            if (thisInst.topo.gotTopo == false)
                return;

            if (thisInst.chkUseSR.Checked == true && thisInst.topo.gotSR == true)
                thisInst.topo.useSR = true;
            else
                thisInst.topo.useSR = false;

            if (thisInst.chk_Use_Sep.Checked == true)
                thisInst.topo.useSepMod = true;
            else
                thisInst.topo.useSepMod = false;

            // Call background worker to run calculations
            // In background worker, performs exposure and grid stat calculations, finds path of nodes and does site calibration.
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
            theArgs.thisInst = thisInst;
            theArgs.MCP_type = thisInst.Get_MCP_Method();
            thisInst.BW_worker.Call_BW_MetCalcs(theArgs);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Whirlpool W1.csv";
            thisInst.LoadTurbines(turbineFile);
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 87-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Generate turbine wind speed and gross energy estimates
            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

            argsForBW.thisInst = thisInst;
            argsForBW.thisWakeModel = null;

            // Call background worker to run calculations
            thisInst.BW_worker = new BackgroundWork();
            string MCP_Method = thisInst.Get_MCP_Method();
            argsForBW.MCP_Method = MCP_Method;
            thisInst.BW_worker.Call_BW_TurbCalcs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Do Exceedance calcs
            // Delete 10 of the curves to speed up calcs
            for (int i = 0; i < 10; i++)
                thisInst.turbineList.exceed.Delete_Exceed(thisInst.lstDefinedLosses.Items[i].Text);

            // Reduce the number simulations to 1000
            thisInst.turbineList.exceed.numSims = 1000;

            BackgroundWork.Vars_for_BW varsForBW = new BackgroundWork.Vars_for_BW();
            varsForBW.thisInst = thisInst;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_Exceed(varsForBW);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Create Eddy Viscosity Wake Model
            Gen_WakeModel thisWake = new Gen_WakeModel(thisInst);

            thisWake.cboWakeCombo.SelectedIndex = 0;

            thisWake.cboWakeModel.SelectedIndex = 0;
            thisWake.cboPowerCrvs.SelectedIndex = 0;
            thisWake.txtHorizWakeExp.Text = "5";
            thisWake.txtAmbTI.Text = "10";
            
            
            thisWake.GenWakeModel();

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            string metToDelete = "Archbold";
            int metCount = thisInst.metList.ThisCount;                                  
            
            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            
            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - 1, "Didn't delete met site");                       

      /*      TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            MessageBox.Show("Finished test " + elapsedTime);
          */  
        }

        [TestMethod]
        public void CreateModelWithOneTABDeleteTurbines()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = testingFolder + "\\Continuum One TAB Delete Turbines test";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.SaveFile(true);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay Topo.tif";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.topo.gotTopo == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay Land Cover.tif";

            Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.topo.gotSR == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs(thisInst);

            if (thisInst.topo.gotTopo == false)
                return;

            if (thisInst.chkUseSR.Checked == true && thisInst.topo.gotSR == true)
                thisInst.topo.useSR = true;
            else
                thisInst.topo.useSR = false;

            if (thisInst.chk_Use_Sep.Checked == true)
                thisInst.topo.useSepMod = true;
            else
                thisInst.topo.useSepMod = false;

            // Call background worker to run calculations
            // In background worker, performs exposure and grid stat calculations, finds path of nodes and does site calibration.
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
            theArgs.thisInst = thisInst;
            theArgs.MCP_type = thisInst.Get_MCP_Method();
            thisInst.BW_worker.Call_BW_MetCalcs(theArgs);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Whirlpool W1.csv";
            thisInst.LoadTurbines(turbineFile);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 87-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Generate turbine wind speed and gross energy estimates
            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

            argsForBW.thisInst = thisInst;
            argsForBW.thisWakeModel = null;

            // Call background worker to run calculations
            thisInst.BW_worker = new BackgroundWork();
            string MCP_Method = thisInst.Get_MCP_Method();
            argsForBW.MCP_Method = MCP_Method;
            thisInst.BW_worker.Call_BW_TurbCalcs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            DialogResult goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            int numTurbines = thisInst.lstTurbines.Items.Count;
                        
            for (int i = 0; i < numTurbines; i++)
            {
                string turbineName = thisInst.lstTurbines.Items[i].Text;
                thisInst.turbineList.DeleteTurbine(turbineName);
            }

            thisInst.ChangesMade();
            
            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            thisInst.SaveFile(true);
            
        }

        [TestMethod]
        public void CreateModelWithZonesDeleteZones()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = testingFolder + "\\Continuum Zone Delete Zones test";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.SaveFile(true);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Archbold Findlay coords.TAB";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs(thisInst);

            if (thisInst.topo.gotTopo == true)
            {
                if (thisInst.chkUseSR.Checked == true && thisInst.topo.gotSR == true)
                    thisInst.topo.useSR = true;
                else
                    thisInst.topo.useSR = false;

                if (thisInst.chk_Use_Sep.Checked == true)
                    thisInst.topo.useSepMod = true;
                else
                    thisInst.topo.useSepMod = false;

                // Call background worker to run calculations
                // In background worker, performs exposure and grid stat calculations, finds path of nodes and does site calibration.
                thisInst.BW_worker = new BackgroundWork();
                BackgroundWork.Vars_for_MetCalcs metArgs = new BackgroundWork.Vars_for_MetCalcs();
                metArgs.thisInst = thisInst;
                metArgs.MCP_type = thisInst.Get_MCP_Method();
                thisInst.BW_worker.Call_BW_MetCalcs(metArgs);

                while (thisInst.BW_worker.DoWorkDone == false)
                    Thread.Sleep(1000);

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay Topo.tif";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.topo.gotTopo == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Analyze mets
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
            theArgs.thisInst = thisInst;
            theArgs.MCP_type = thisInst.Get_MCP_Method();
            thisInst.BW_worker.Call_BW_MetCalcs(theArgs);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Whirlpool W1.csv";
            thisInst.LoadTurbines(turbineFile);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";

            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Generate turbine wind speed and gross energy estimates
            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

            argsForBW.thisInst = thisInst;
            argsForBW.thisWakeModel = null;

            // Call background worker to run calculations
            thisInst.BW_worker = new BackgroundWork();
            string MCP_Method = thisInst.Get_MCP_Method();
            argsForBW.MCP_Method = MCP_Method;
            thisInst.BW_worker.Call_BW_TurbCalcs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].avgWS_Est[0].freeStream.WS, 0, "Didn't calculate average WS");

            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Run ice throw model
            int numIceThrowsPerDay = 100;
            int numIceDaysPerYear = 2;

            if (numIceThrowsPerDay != -999 && numIceDaysPerYear != -999)
            {
                thisInst.siteSuitability.numIceDaysPerYear = numIceDaysPerYear;
                thisInst.siteSuitability.iceThrowsPerIceDay = numIceThrowsPerDay;

                BackgroundWork.Vars_for_BW varsForBW = new BackgroundWork.Vars_for_BW();
                varsForBW.thisInst = thisInst;
                thisInst.BW_worker = new BackgroundWork();
                thisInst.BW_worker.Call_BW_IceThrow(varsForBW);
            }

            while (thisInst.BW_worker.DoWorkDone == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            Assert.AreNotEqual(thisInst.siteSuitability.yearlyIceHits[0].iceHits.Length, 0, "Didn't calculate ice throw");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Run shadow flicker model
            thisInst.siteSuitability.RunShadowFlickerModel(thisInst);

            Assert.AreNotEqual(thisInst.siteSuitability.flickerMap.Length, 0, "Didn't calculate shadow flicker");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Run turbine noise model

            double turbineSound = thisInst.GetTurbineNoise();

            if (turbineSound != -999)
            {
                thisInst.siteSuitability.turbineSound = turbineSound;
                thisInst.siteSuitability.CreateSoundMap(thisInst);
                thisInst.updateThe.SoundMap(thisInst);
                thisInst.updateThe.SoundAtZones(thisInst);
                thisInst.updateThe.SiteSuitabilityDropdown(thisInst, "Sound");
                thisInst.updateThe.ColoredButtons(thisInst);
                thisInst.updateThe.SiteSuitabilityVisibility(thisInst);
            }

            Assert.AreNotEqual(thisInst.siteSuitability.soundMap.Length, 0, "Didn't calculate noise map");

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Zones

            int numZonesToDelete = thisInst.lstZones.SelectedItems.Count;
            DialogResult goodToGo = DialogResult.Yes; // MessageBox.Show("Are you sure you want to delete these " + numZonesToDelete + " zones?", "Continuum 3", MessageBoxButtons.YesNo);

            for (int i = 0; i < thisInst.lstZones.Items.Count; i++)
                thisInst.lstZones.Items[i].Selected = true;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstZones.AccessibilityObject.ToString();

            ListView.SelectedListViewItemCollection SelectedZones = thisInst.lstZones.SelectedItems;

            if (goodToGo == DialogResult.Yes)
            {
                for (int i = 0; i < numZonesToDelete; i++)
                {
                    string zoneName = SelectedZones[i].Text;
                    thisInst.siteSuitability.DeleteZone(zoneName);
                }
                thisInst.ChangesMade();
            }

            // If sound map has been created, recreate using new bounds
            if (thisInst.siteSuitability.soundMap != null)
                thisInst.siteSuitability.CreateSoundMap(thisInst);

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            thisInst.SaveFile(true);

            /*       TimeSpan ts = stopWatch.Elapsed;
                   string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                       ts.Hours, ts.Minutes, ts.Seconds,
                       ts.Milliseconds / 10);
                   MessageBox.Show("Finished test " + elapsedTime);
                   stopWatch.Restart();
       */
        }

        [TestMethod]
        public void CreateModelwithOneTSDeletePowerCurve()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = testingFolder + "\\Continuum Met TS test";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.SaveFile(true);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay Topo.tif";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.topo.gotTopo == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            // Called in BackgroundWorker RunWorkerCompleted
            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay Land Cover.tif";

            Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.topo.gotSR == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            string metTSFile = testingFolder + "\\Met TS files\\Archbold TS short Findlay coords.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.isTimeSeries = true;
            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            string metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            Met thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Analyze mets
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_MetCalcs theArgs = new BackgroundWork.Vars_for_MetCalcs();
            theArgs.thisInst = thisInst;
            theArgs.MCP_type = thisInst.Get_MCP_Method();
            thisInst.BW_worker.Call_BW_MetCalcs(theArgs);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(1000);

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Whirlpool W1.csv";
            thisInst.LoadTurbines(turbineFile);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 87-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Generate turbine wind speed and gross energy estimates
            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

            argsForBW.thisInst = thisInst;
            argsForBW.thisWakeModel = null;

            // Call background worker to run calculations
            thisInst.BW_worker = new BackgroundWork();
            string MCP_Method = thisInst.Get_MCP_Method();
            argsForBW.MCP_Method = MCP_Method;
            thisInst.BW_worker.Call_BW_TurbCalcs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Do Exceedance calcs
            // Delete 10 of the curves to speed up calcs
            for (int i = 0; i < 10; i++)
                thisInst.turbineList.exceed.Delete_Exceed(thisInst.lstDefinedLosses.Items[i].Text);

            // Reduce the number simulations to 1000
            thisInst.turbineList.exceed.numSims = 1000;

            BackgroundWork.Vars_for_BW varsForBW = new BackgroundWork.Vars_for_BW();
            varsForBW.thisInst = thisInst;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_Exceed(varsForBW);

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Create Eddy Viscosity Wake Model
            Gen_WakeModel thisWake = new Gen_WakeModel(thisInst);

            thisWake.cboWakeCombo.SelectedIndex = 0;

            thisWake.cboWakeModel.SelectedIndex = 0;
            thisWake.cboPowerCrvs.SelectedIndex = 0;
            thisWake.txtHorizWakeExp.Text = "5";
            thisWake.txtAmbTI.Text = "10";
            
            
            thisWake.GenWakeModel();

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Create wake map
            GenWakeMap wakeMap = new GenWakeMap(thisInst);

            wakeMap.cboWakeModels.Items.Clear();
            for (int i = 0; i <= thisInst.wakeModelList.NumWakeModels - 1; i++)
            {
                string Wake_String = thisInst.wakeModelList.CreateWakeModelString(thisInst.wakeModelList.wakeModels[i]);
                wakeMap.cboWakeModels.Items.Add(Wake_String);
            }

            wakeMap.cboWakeModels.SelectedIndex = 0;
            wakeMap.cboUseTimeSeries.SelectedIndex = 0; // Use Avg Dist.
            wakeMap.gridReso = 250;
            wakeMap.UpdateLimits();
            wakeMap.GetCoordsAroundTurbs();
            wakeMap.GenerateWakeMap();

            while (thisInst.BW_worker.DoWorkDone == false)
                Thread.Sleep(100);

            thisInst.BW_worker.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Power Curve                                   

            for (int i = 0; i < thisInst.lstPowerCurveList.Items.Count; i++)
                thisInst.lstPowerCurveList.Items[i].Selected = true;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstPowerCurveList.AccessibilityObject.ToString();

            if (thisInst.lstPowerCurveList.Items.Count > 0)
            {
                for (int i = 0; i < thisInst.lstPowerCurveList.SelectedItems.Count; i++)
                {
                    string powerCurveToDelete = thisInst.lstPowerCurveList.SelectedItems[i].Text;
                    thisInst.turbineList.DeletePowerCurve(powerCurveToDelete);

                    for (int t = 0; t < thisInst.turbineList.TurbineCount; t++)
                        thisInst.turbineList.turbineEsts[t].ClearGrossEstsFromAvgWS(powerCurveToDelete);

                    thisInst.wakeModelList.RemoveWakeModelByPowerCurve(thisInst.turbineList, thisInst.mapList, powerCurveToDelete);
                }

                thisInst.turbineList.ClearDuplicateAvgWS(thisInst.metList.isTimeSeries);
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }            
            else            
                MessageBox.Show("No power curves to delete", "Continuum 3");                                

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, 0, "Didn't delete power curves");

            thisInst.SaveFile(true);

            /*         TimeSpan ts = stopWatch.Elapsed;
                     string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                         ts.Hours, ts.Minutes, ts.Seconds,
                         ts.Milliseconds / 10);
                     MessageBox.Show("Finished test " + elapsedTime);
                     stopWatch.Restart();  */
        }

        /*  Loss_factors class not used in Continuum 3    [TestMethod]
            public void Get_Total_Losses_Test()
            {
                Loss_factors loss_Factors = new Loss_factors();
                loss_Factors.Set_Defaults();
                double totalLoss = loss_Factors.Get_Total_Losses();

                Assert.AreEqual(0.10526, totalLoss, 0.0001, "Wrong total losses: Test 1");

                loss_Factors.Icing_Loss = 0.03;
                totalLoss = loss_Factors.Get_Total_Losses();
                Assert.AreEqual(0.132102, totalLoss, 0.0001, "Wrong total losses: Test 2");

                loss_Factors.HighLowTemp_Loss = 0.02;
                totalLoss = loss_Factors.Get_Total_Losses();
                Assert.AreEqual(0.14946, totalLoss, 0.0001, "Wrong total losses: Test 3");

                loss_Factors.Wake_Sect_Curtail_Loss = 0.015;
                totalLoss = loss_Factors.Get_Total_Losses();
                Assert.AreEqual(0.162218, totalLoss, 0.0001, "Wrong total losses: Test 4");

                loss_Factors.Environ_Curtail_Loss = 0.04;
                totalLoss = loss_Factors.Get_Total_Losses();
                Assert.AreEqual(0.19573, totalLoss, 0.0001, "Wrong total losses: Test 5");

                loss_Factors.Grid_Curtail_Loss = 0.01;
                totalLoss = loss_Factors.Get_Total_Losses();
                Assert.AreEqual(0.203772, totalLoss, 0.0001, "Wrong total losses: Test 6");

            }
            */


        /*     [TestMethod] Not used in Continuum 3. Uncertainty now based on Round Robin results
             public void FindModelBounds_Test()
             {
                 Continuum thisInst = new Continuum();
                 
                 string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\GetErrorEst\\Testing Error Estimate.cfm";
                 thisInst.Open(Filename);

                 // Test 1: WD_ind = 0 Radius = 4000
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].min, 7.05, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].max, 7.05, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].metMinP10.name, "Met_2903", "Wrong Min Met SpeedUp Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 0");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].min, 0.47, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].max, 7.05, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].metMinP10.name, "Met_2904", "Wrong Min Met Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 0");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].min, 0.47, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].max, 19.99, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].metMaxP10.name, "Met_2907", "Wrong Max Met Uphill Flow WD = 0");

                 // Test 2: WD_ind = 2 Radius = 4000
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].min, -999, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].max, -999, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].metMinP10, null, "Wrong Min Met SpeedUp Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].metMaxP10, null, "Wrong Max Met SpeedUp Flow WD = 2");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].min, 8.08, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].max, 8.08, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].metMinP10.name, "Met_2903", "Wrong Min Met Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 2");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].min, 7.04, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].max, 9.29, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].metMaxP10.name, "Met_2907", "Wrong Max Met Uphill Flow WD = 2");

                 // Test 3: WD_ind = 7 Radius = 4000
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].min, 2.51, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].max, 6.15, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].metMinP10.name, "Met_2904", "Wrong Min Met SpeedUp Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 7");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].min, 6.15, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].max, 12.83, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].metMinP10.name, "Met_2903", "Wrong Min Met Downhill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].metMaxP10.name, "Met_2907", "Wrong Max Met Downhill Flow WD = 7");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].min, 2.51, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].max, 8.18, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].metMaxP10.name, "Met_2905", "Wrong Max Met Uphill Flow WD = 7");

                 // Test 4: WD_ind = 13 Radius = 4000
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].min, 10.51, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].max, 10.51, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].metMinP10.name, "Met_2903", "Wrong Min Met SpeedUp Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 13");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].min, 3.67, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].max, 10.51, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].metMinP10.name, "Met_2907", "Wrong Min Met Downhill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 13");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].min, -999, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].max, -999, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].metMinP10, null, "Wrong Min Met Uphill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].metMaxP10, null, "Wrong Max Met Uphill Flow WD = 13");

                 // Test 5: WD_ind = 0 Radius = 8000
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].min, 9.96, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].max, 9.96, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].metMinP10.name, "Met_2903", "Wrong Min Met SpeedUp Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 0");

                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].min, 2.23, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].max, 9.96, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].metMinP10.name, "Met_2905", "Wrong Min Met Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 0");

                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].min, 8.09, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].max, 32.52, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].metMaxP10.name, "Met_2907", "Wrong Max Met Uphill Flow WD = 0");

                 // Test 6: WD_ind = 2 Radius = 8000
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].min, 7.15, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].max, 10.61, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].metMinP10.name, "Met_2905", "Wrong Min Met SpeedUp Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].metMaxP10.name, "Met_2907", "Wrong Max Met SpeedUp Flow WD = 2");

                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].min, 13.56, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].max, 13.56, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].metMinP10.name, "Met_2903", "Wrong Min Met Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 2");

                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].min, 7.15, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].max, 13.56, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].metMinP10.name, "Met_2905", "Wrong Min Met Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].metMaxP10.name, "Met_2903", "Wrong Max Met Uphill Flow WD = 2");

                 thisInst.Close();
             }
     */


        /*       [TestMethod] Not used in Continuum 3. Uncertainty now based on Round Robin results
               public void GetUncertaintyEstimate_Test()
               {
                   Continuum thisInst = new Continuum();
                   
                   string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\GetErrorEst\\Testing Error Estimate.cfm";
                   thisInst.Open(Filename);

                   Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), 4000, 10000, true, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
                   double thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 5, 5, 0.5, 10, 0, Met.TOD.All, Met.Season.All, 80);
                   Assert.AreEqual(thisUncert, 0.05171, 0.01, "Wrong uncertainty Test 1");

                   thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 5, 5, 3, 10, 0, Met.TOD.All, Met.Season.All, 80);
                   Assert.AreEqual(thisUncert, 0.05171, 0.01, "Wrong uncertainty Test 2");

                   thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 12, 12, -5, 10, 0, Met.TOD.All, Met.Season.All, 80);
                   Assert.AreEqual(thisUncert, 0.007371, 0.01, "Wrong uncertainty Test 3");

                   thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 0.1, 0.1, 10, 10, 0, Met.TOD.All, Met.Season.All, 80);
                   Assert.AreEqual(thisUncert, 0.056710, 0.01, "Wrong uncertainty Test 4");

                   thisInst.Close();
               }
       */
        /*       [TestMethod] Not used in Continuum 3. Uncertainty now based on Round Robin results
               public void GetErrorEst_Test()
               {
                   Continuum thisInst = new Continuum();
                   
                   string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\GetErrorEst\\Testing Error Estimate.cfm";
                   thisInst.Open(Filename);

                   Model[] models = new Model[thisInst.radiiList.ThisCount];
                   Turbine thisTurb = thisInst.turbineList.turbineEsts[0];

                   for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
                       models[i] = thisInst.modelList.models[1, i];

                   double[] theseWgts = thisInst.modelList.GetModelWeights(models);

                   int numWD = thisInst.GetNumWD();
                   double[,] thisUncert = new double[4, 16];

                   for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
                   {
                       for (int j = 0; j < numWD; j++)
                       {
                           thisUncert[i, j] = models[i].GetUncertaintyEstimate(thisInst, thisTurb.gridStats.stats[i].P10_UW[j], thisTurb.gridStats.stats[i].P10_DW[j],
                               thisTurb.expo[i].expo[j], thisTurb.expo[i].GetDW_Param(j, "Expo"), j, Met.TOD.All, Met.Season.All, 80);
                       }
                   }

                   double[] windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), thisTurb.UTMX, thisTurb.UTMY, Met.TOD.All, Met.Season.All, 80);
                   double Uncert = thisTurb.GetErrorEst(thisInst, windRose);

                   Assert.AreEqual(Uncert, 0.042022, 0.01, "Wrong overall uncertainty");

                   thisInst.Close();
               }
       */

        [TestMethod]
        public void ModelIceThrow_Test()
        {
            // Outputs ice throw results to .csv file
            SiteSuitability siteSuitability = new SiteSuitability();

            string fileName = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Site Suitability\\Ice Throw\\Test1.csv";

            // Test 1
            double degs = 180;
            double hubHeigt = 80;
            double elevDiff = 0;
            double randRad = 44.5;
            double iceSpeed = 20;
            double cD = 1.1;
            double iceArea = 0.02;
            double iceMass = 1.13;
            double thisWS = 6;
            double thisWD = 0; // North
            Turbine thisTurb = new Turbine();

         //   siteSuitability.ModelIceThrow(degs, hubHeigt, elevDiff, randRad, iceSpeed, cD, iceArea, iceMass, thisWS, thisWD, thisTurb, fileName);

            // Test 2
            fileName = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Site Suitability\\Ice Throw\\Test2.csv";
            degs = 360; // Blade pointing straight up           
         //   siteSuitability.ModelIceThrow(degs, hubHeigt, elevDiff, randRad, iceSpeed, cD, iceArea, iceMass, thisWS, thisWD, thisTurb, fileName);

            // Test 3
            fileName = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Site Suitability\\Ice Throw\\Test3.csv";
            degs = 270; // Blade pointing straight out           
         //   siteSuitability.ModelIceThrow(degs, hubHeigt, elevDiff, randRad, iceSpeed, cD, iceArea, iceMass, thisWS, thisWD, thisTurb, fileName);



        }

        

        

        

        
    }
}
