using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Continuum_Tests.GUI_Tests
{
    [TestClass]
    public class Map_tests
    {
        string testingFolder = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\TestFolder";
        string saveFolder = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder";

        Met thisMet;
        GenMap newMap;
        int numMaps;
        string MCP_Method;

        [TestMethod]
        public void Unwaked_Maps()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\Maps_Unwaked";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 80;
            thisInst.SaveFile(true);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Topo.tif";
            
            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            // Called in BackgroundWorker RunWorkerCompleted
            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Paulding Findlay coords.TAB";
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
                BackgroundWork.Vars_for_BW metArgs = new BackgroundWork.Vars_for_BW();
                metArgs.thisInst = thisInst;
                
                thisInst.BW_worker.Call_BW_MetCalcs(metArgs);

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Wapakoneta Findlay coords.TAB";
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
                BackgroundWork.Vars_for_BW metArgs = new BackgroundWork.Vars_for_BW();
                metArgs.thisInst = thisInst;
                
                thisInst.BW_worker.Call_BW_MetCalcs(metArgs);

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Analyze mets
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_BW theArgs = new BackgroundWork.Vars_for_BW();
            theArgs.thisInst = thisInst;            
            thisInst.BW_worker.Call_BW_MetCalcs(theArgs);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

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
            thisMet = null;

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

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Generate map

            newMap = new GenMap(thisInst);
            newMap.cboWhatToMap.SelectedIndex = 2;

            // if there are existing maps, use the coordinates from the last one created
            numMaps = thisInst.mapList.ThisCount;

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
            newMap.FindAreaAroundTurbines();

            newMap.chkMetsToUse.Items.Clear();
            thisMet = null;

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void Maps_Waked()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\Maps_Waked";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 80;
            thisInst.SaveFile(true);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Topo.tif";
            
            BackgroundWork.Vars_for_BW Vars_for_BW = new BackgroundWork.Vars_for_BW();
            Vars_for_BW.thisInst = thisInst;
            Vars_for_BW.Filename = topoFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_TopoImport(Vars_for_BW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            // Called in BackgroundWorker RunWorkerCompleted
            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Paulding Findlay coords.TAB";
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
                BackgroundWork.Vars_for_BW metArgs = new BackgroundWork.Vars_for_BW();
                metArgs.thisInst = thisInst;
                
                thisInst.BW_worker.Call_BW_MetCalcs(metArgs);

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Analyze mets
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_BW theArgs = new BackgroundWork.Vars_for_BW();
            theArgs.thisInst = thisInst;            
            thisInst.BW_worker.Call_BW_MetCalcs(theArgs);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Generate turbine wind speed and gross energy estimates

            // First check that a model has been created
            if (thisInst.modelList.ModelCount == 0)
            {
                thisInst.Analyze_Mets();

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.BW_worker.Close();
            }


            BackgroundWork.Vars_for_TurbCalcs argsForBW = new BackgroundWork.Vars_for_TurbCalcs();

            argsForBW.thisInst = thisInst;
            argsForBW.thisWakeModel = null;

            // Call background worker to run calculations
            thisInst.BW_worker = new BackgroundWork();
            MCP_Method = thisInst.Get_MCP_Method();
            argsForBW.MCP_Method = MCP_Method;
            thisInst.BW_worker.Call_BW_TurbCalcs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].avgWS_Est[0].freeStream.WS, 0, "Didn't calculate average WS");

            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";

            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs(thisInst);
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            Assert.AreEqual(thisInst.wakeModelList.NumWakeModels, 1, "Didn't create a wake model");

            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Create wake map
            GenWakeMap wakeMap = new GenWakeMap(thisInst);

            numMaps = thisInst.wakeModelList.NumCompleteWakeGridMaps;
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

            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();

            Assert.AreEqual(thisInst.wakeModelList.NumCompleteWakeGridMaps, numMaps + 1, "Didn't create waked map");

            thisInst.SaveFile(true);
            thisInst.Close();
        }



    }
}
