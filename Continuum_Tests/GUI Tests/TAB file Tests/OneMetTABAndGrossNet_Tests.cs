using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Continuum_Tests.GUI_Tests
{
    
    [TestClass]
    public class OneMetTABAndGrossNet_Tests
    {
        string testingFolder = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\TestFolder";
        string saveFolder = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder";

        string MCP_Method;
        
        [TestMethod]
        public void OneMetTABAndGrossNet_1()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");
            Thread.Sleep(1000);

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_1";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();

            
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_10()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");
            Thread.Sleep(1000);

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_10";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_100()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_100";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_101()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_101";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_102()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_102";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_103()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_103";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_104()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_104";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_105()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_105";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_106()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_106";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_107()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_107";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_108()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_108";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_109()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_109";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_11()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_11";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_110()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_110";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_111()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_111";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_112()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_112";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_113()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_113";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_114()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_114";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_115()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_115";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_116()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_116";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_117()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_117";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_118()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_118";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_119()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_119";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_12()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_12";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_120()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_120";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_121()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_121";

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
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_122()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_122";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_123()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_123";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_124()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_124";

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
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_125()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_125";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_126()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_126";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_127()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_127";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_128()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_128";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_129()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_129";

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
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_13()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_13";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_130()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_130";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_131()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_131";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_132()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_132";

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
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_133()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_133";

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
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_134()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_134";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_135()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_135";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_136()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_136";

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
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_137()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_137";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_138()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_138";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_139()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_139";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_14()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_14";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_140()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_140";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_141()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_141";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void OneMetTABAndGrossNet_142()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_142";

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
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Findlay\\Ball 2 Turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Findlay\\Findlay Land Cover.tif";

            BackgroundWork.Vars_for_BW LC_Vars_for_BW = new BackgroundWork.Vars_for_BW();
            LC_Vars_for_BW.thisInst = thisInst;
            LC_Vars_for_BW.Filename = landCoverFile;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_LandCoverImport(LC_Vars_for_BW);
            thisInst.chkUseSR.Checked = true;

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Findlay\\Archbold Findlay coords.TAB";
            thisInst.ImportMetsTAB();

            // The following are all called in btnMetTAB_Click
            thisInst.turbineList.ClearAllWSEsts();
            thisInst.updateThe.AllTABs();

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
                thisInst.updateThe.AllTABs();
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Findlay\\Ball 2 Zones.csv";
            thisInst.ImportZones(zonesFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

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

            thisInst.updateThe.AllTABs();
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

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(100);

            if (thisInst.BW_worker.WasReturned == true)
                Assert.Fail();

            Assert.AreEqual(thisInst.turbineList.exceed.compositeLoss.isComplete, true, "Didn't do Monte Carlo model");

            thisInst.updateThe.AllTABs();
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

            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();

            thisInst.SaveFile(true);
            thisInst.Close();
        }


    }
}
