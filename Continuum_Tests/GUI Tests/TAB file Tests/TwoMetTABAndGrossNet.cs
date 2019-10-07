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
    /// <summary>
    /// Summary description for TwoMetTABAndGrossNet
    /// </summary>
    [TestClass]
    public class TwoMetTABAndGrossNet
    {
        string testingFolder = "C:\\Users\\OEE2017_27\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum";
        string saveFolder = "C:\\Users\\OEE2017_27\\Desktop\\Continuum tests";

        string MCP_Method;

        [TestMethod]
        public void TwoMetTABAndGrossNet_1()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_1";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_10()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_10";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_100()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_100";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_101()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_101";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_102()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_102";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_103()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_103";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_104()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_104";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_105()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_105";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_106()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_106";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_107()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_107";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_108()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_108";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_109()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_109";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_11()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_11";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_110()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_110";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_111()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_111";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_112()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_112";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_113()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_113";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_114()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_114";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_115()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_115";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_116()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_116";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_117()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_117";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_118()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_118";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_119()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_119";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_12()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_12";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_120()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_120";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_121()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_121";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_122()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_122";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_123()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_123";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_124()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_124";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_125()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_125";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_126()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_126";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_127()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_127";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_128()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_128";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_129()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_129";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_13()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_13";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_130()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_130";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_131()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_131";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_132()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_132";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_133()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_133";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_134()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_134";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }

        [TestMethod]
        public void TwoMetTABAndGrossNet_135()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\TwoMetTABAndGrossNet_135";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }

            thisInst.savedParams.savedFileName = fileName + ".cfm";
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 14;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 60;
            thisInst.SaveFile(true);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_1.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Firewheel\\Met_2.TAB";
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

                while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                    Thread.Sleep(1000);

                if (thisInst.BW_worker.WasReturned)
                    Assert.Fail();

                thisInst.SaveFile(true);
                thisInst.updateThe.AllTABs(thisInst);
                thisInst.BW_worker.Close();
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel LC small.tif";

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
            thisInst.updateThe.AllTABs(thisInst);
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Firewheel\\Firewheel Topo small.tif";


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
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 93-1500 PC_1.205.csv";
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


            BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

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

            thisInst.SaveFile(true);
            thisInst.Close();
        }



    }
}
