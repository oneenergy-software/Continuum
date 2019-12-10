using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace Continuum_Tests.GUI_Tests
{
    [TestClass]
    public class ThreeMetTABAndGrossNet
    {
        string testingFolder = "C:\\Users\\Liz\\Desktop\\Continuum 3 GUI Testing\\TestFolder";
        string saveFolder = "C:\\Users\\Liz\\Desktop\\Continuum 3 GUI Testing\\SaveFolder";

        string firewheelMERRA = "C:\\Users\\OEE2017_27\\Desktop\\MERRA2";
        string ohioMERRA = "C:\\Users\\Liz\\Desktop\\MERRA2";
        string merraFolder = "C:\\Users\\Liz\\Desktop\\MERRA2";

        string metTSFile;
        string metName;
        string MCP_Method;
        Met thisMet;
        UTM_conversion.Lat_Long theseLL;
        int offset;
        MERRA thisMERRA;

        [TestMethod]
        public void ThreeMetTABAndGrossNet_1()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_1";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_10()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_10";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_100()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_100";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_101()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_101";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_102()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_102";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_103()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_103";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_104()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_104";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_105()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_105";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_106()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_106";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_107()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_107";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_108()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_108";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_109()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_109";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_11()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_11";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_110()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_110";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_111()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_111";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_112()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_112";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_113()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_113";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_114()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_114";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_115()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_115";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_116()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_116";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_117()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_117";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_118()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_118";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_119()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_119";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_12()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_12";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_120()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_120";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_121()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_121";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_122()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_122";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_123()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_123";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_124()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_124";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_125()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_125";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_126()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_126";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_127()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_127";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_128()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_128";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_129()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_129";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_13()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_13";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_130()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_130";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_131()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_131";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_132()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_132";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_133()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_133";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_134()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_134";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_135()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_135";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_136()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_136";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_137()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_137";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_138()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_138";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_139()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_139";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_14()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_14";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_140()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_140";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_141()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_141";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_142()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_142";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_143()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_143";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_144()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_144";

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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
        public void ThreeMetTABAndGrossNet_145()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_145";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
        public void ThreeMetTABAndGrossNet_146()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_146";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
        public void ThreeMetTABAndGrossNet_147()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_147";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
        public void ThreeMetTABAndGrossNet_148()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_148";

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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
        public void ThreeMetTABAndGrossNet_149()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_149";

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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
        public void ThreeMetTABAndGrossNet_15()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_15";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load topo data
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
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
        public void ThreeMetTABAndGrossNet_150()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_150";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
        public void ThreeMetTABAndGrossNet_151()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_151";

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
            string zonesFile = testingFolder + "\\Zones\\Bobcat Bluff\\Bobcat Bluff zones.csv";
            thisInst.ImportZones(zonesFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TAB file
            thisInst.metList.isTimeSeries = false;
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_2001.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_5444.TAB";
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
            thisInst.ofdMets.FileName = testingFolder + "\\Met TAB files\\Bobcat Bluff\\Met_9900.TAB";
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
            string topoFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\w001001.adf";


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
            string turbineFile = testingFolder + "\\Turbine sites\\Bobcat Bluff\\Bobcat Bluff 3 turbines.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load land cover data
            thisInst.topo.SetUS_NLCD_Key();
            string landCoverFile = testingFolder + "\\Topo & LC\\Bobcat Bluff\\LC1108144640.tif";

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
