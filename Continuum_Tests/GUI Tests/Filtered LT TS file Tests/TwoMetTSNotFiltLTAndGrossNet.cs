using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Continuum_Tests.GUI_Tests
{
    [TestClass]
    public class TwoMetTSNotFiltLTAndGrossNet
    {
        string testingFolder = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\TestFolder";
        string saveFolder = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder";

        string metTSFile;
        string metName;
        string MCP_Method;
        Met thisMet;
        UTM_conversion.Lat_Long theseLL;
        int offset;
        MERRA thisMERRA;

        [TestMethod]
        public void TwoMetTSNotFiltLTAndGrossNet_1()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_1";

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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_10()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_10";

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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_100()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_100";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_101()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_101";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_102()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_102";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_103()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_103";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_104()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_104";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_105()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_105";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_106()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_106";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_107()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_107";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_108()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_108";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_109()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_109";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_11()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_11";

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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_110()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_110";

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
            thisInst.updateThe.AllTABs();
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_111()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_111";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_112()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_112";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_113()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_113";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_114()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_114";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_115()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_115";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_116()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_116";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_117()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_117";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_118()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_118";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_119()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_119";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
            thisInst.ofdPowerCurve.FileName = powerCurveFile;
            thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_12()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_12";

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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_120()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_120";

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
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
        public void TwoMetTSNotFiltLTAndGrossNet_121()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_121";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_122()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_122";

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
            thisInst.updateThe.AllTABs();
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_123()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_123";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_124()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_124";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_125()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_125";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_126()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_126";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_127()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_127";

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
            thisInst.updateThe.AllTABs();
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_128()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_128";

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
            thisInst.updateThe.AllTABs();
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_129()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_129";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_13()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_13";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_130()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_130";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_131()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_131";

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
            thisInst.updateThe.AllTABs();
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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_132()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_132";

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
            thisInst.updateThe.AllTABs();
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_133()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_133";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_134()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_134";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_135()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_135";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_136()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_136";

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
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_137()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_137";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_138()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_138";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_139()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_139";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_14()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_14";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_140()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_140";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_141()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_141";

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
            thisInst.updateThe.AllTABs();
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_142()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_142";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_143()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_143";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_144()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_144";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one turbine
            string turbineFile = testingFolder + "\\Turbine sites\\Firewheel\\Firewheel 5 turbine sites.csv";
            thisInst.LoadTurbines(turbineFile);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load zone sites
            string zonesFile = testingFolder + "\\Zones\\Firewheel\\Firewheel Zones.csv";
            thisInst.ImportZones(zonesFile);
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_145()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_145";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_146()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_146";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_147()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_147";

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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_1 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_148()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_148";

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
            thisInst.updateThe.AllTABs();
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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
        public void TwoMetTSNotFiltLTAndGrossNet_149()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\TwoMetTSNotFiltLTAndGrossNet_149";

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
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = false;
            thisInst.turbineList.genTimeSeries = true;

            metTSFile = testingFolder + "\\Met TS files\\Firewheel\\Met_2 short.csv";
            thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
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
            thisInst.updateThe.AllTABs();
            thisInst.BW_worker.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load a power curve
            string powerCurveFile = testingFolder + "\\Power Curves\\Vestas_V150@1.041kgm^-3.csv";
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
