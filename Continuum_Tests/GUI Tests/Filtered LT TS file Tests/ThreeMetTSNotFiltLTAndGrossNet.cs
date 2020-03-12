using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Continuum_Tests.GUI_Tests
{
    [TestClass]
    public class ThreeMetTSNotFiltLTAndGrossNet
    {
        string testingFolder = "C:\\Users\\Liz\\Desktop\\Continuum 3 GUI Testing\\TestFolder";
        string saveFolder = "C:\\Users\\Liz\\Desktop\\Continuum 3 GUI Testing\\SaveFolder";

        string metTSFile;
        string metName;
        string MCP_Method;
        Met thisMet;

        [TestMethod]
        public void ThreeMetTSNotFiltLTAndGrossNet_1()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_1";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_10()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_10";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_100()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_100";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_101()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_101";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_102()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_102";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_103()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_103";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_104()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_104";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_105()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_105";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_106()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_106";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_107()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_107";

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
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_108()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_108";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_109()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_109";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_11()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_11";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_110()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_110";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_111()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_111";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_112()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_112";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_113()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_113";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_114()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_114";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_115()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_115";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_116()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_116";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_117()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_117";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_118()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_118";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_119()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_119";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_12()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_12";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_120()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_120";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_121()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_121";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_122()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_122";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_123()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_123";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_124()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_124";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_125()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_125";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_126()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_126";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_127()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_127";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_128()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_128";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_129()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_129";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
        public void ThreeMetTSNotFiltLTAndGrossNet_13()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_13";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
            string powerCurveFile = testingFolder + "\\Power Curves\\GE_1_85_CRV.csv";
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
        public void ThreeMetTSNotFiltLTAndGrossNet_130()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = saveFolder + "\\ThreeMetTSNotFiltLTAndGrossNet_130";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_3.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);
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
