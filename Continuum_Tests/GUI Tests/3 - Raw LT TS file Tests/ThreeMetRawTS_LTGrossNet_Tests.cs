using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Continuum_Tests.GUI_Tests
{
   [TestClass]
   public class ThreeMetRawTS_LTGrossNet
   {
      string testingFolder;
      string saveFolder;
      string refFolder;
      Globals globals = new Globals();

      string metTSFile;
      string metName;
      string MCP_Method;
      Met thisMet;
      UTM_conversion.Lat_Long theseLL;
      int offset;
      Reference thisRef;

      public ThreeMetRawTS_LTGrossNet()
      {
         testingFolder = globals.testFolder;
         saveFolder = globals.saveFolder;
         refFolder = globals.merraFolder;
      }

[TestMethod]
public void ThreeMetRawTS_LTGrossNet_3178()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("", false);

	string fileName = saveFolder + "\\ThreeMetRawTS_LTGrossNet_3178";
	
	if (File.Exists(fileName + ".cfm"))
	{
	File.Delete(fileName + "cfm");
        File.Delete(fileName + "mdf");
        File.Delete(fileName + "_log.ldf");
        }	

	thisInst.savedParams.savedFileName = fileName + ".cfm";
	thisInst.UTM_conversions.savedDatumIndex = 0;
	thisInst.UTM_conversions.UTMZoneNumber = 17;
	thisInst.UTM_conversions.hemisphere ="Northern";
	thisInst.isTest = true;
	thisInst.modeledHeight =  80;
	thisInst.SaveFile();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Load one met TS
	thisInst.metList.isTimeSeries = true;	
	thisInst.turbineList.genTimeSeries = true;
		
	metTSFile = testingFolder + "\\Met TS files\\NW Ohio\\OH - Paulding formatted - 20180709.csv";
	thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst, false, true);
	
	thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
	thisInst.metList.numWS = 30;

	metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
	thisMet = thisInst.metList.GetMet(metName);
	thisMet.isMCPd = false;
		
	thisMet.metData.FindStartEndDatesWithMaxRecovery();
	thisMet.metData.AddSensorDatatoDB(thisInst, thisMet.name);  
	thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

	thisInst.updateThe.AllTABs();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Load one met TS
	thisInst.metList.isTimeSeries = true;	
	thisInst.turbineList.genTimeSeries = true;
		
	metTSFile = testingFolder + "\\Met TS files\\NW Ohio\\OH - Pettisville formatted - 20180709.csv";
	thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst, false, true);
	
	thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
	thisInst.metList.numWS = 30;

	metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
	thisMet = thisInst.metList.GetMet(metName);
	thisMet.isMCPd = false;
		
	thisMet.metData.FindStartEndDatesWithMaxRecovery();
	thisMet.metData.AddSensorDatatoDB(thisInst, thisMet.name);  
	thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

	thisInst.updateThe.AllTABs();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Load one met TS
	thisInst.metList.isTimeSeries = true;	
	thisInst.turbineList.genTimeSeries = true;
		
	metTSFile = testingFolder + "\\Met TS files\\NW Ohio\\OH - Port Clinton Formatted C3 - 20190813.csv";
	thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst, false, true);
	
	thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
	thisInst.metList.numWS = 30;

	metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
	thisMet = thisInst.metList.GetMet(metName);
	thisMet.isMCPd = false;
		
	thisMet.metData.FindStartEndDatesWithMaxRecovery();
	thisMet.metData.AddSensorDatatoDB(thisInst, thisMet.name);  
	thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

	thisInst.updateThe.AllTABs();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Load one turbine
	string turbineFile = testingFolder + "\\Turbine sites\\NW Ohio\\Harpster.csv";
	thisInst.LoadTurbines(turbineFile);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Load zone sites
	string zonesFile = testingFolder + "\\Zones\\NW Ohio\\Ball 2 Zones.csv";
	thisInst.ImportZones(zonesFile);
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Load land cover data
	thisInst.topo.SetUS_NLCD_Key();            
	string landCoverFile = testingFolder + "\\Topo & LC\\NW Ohio\\NW Ohio Land Cover.tif";

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

	thisInst.SaveFile();
	thisInst.updateThe.AllTABs();
	thisInst.BW_worker.Close();
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Load topo data
	string topoFile = testingFolder + "\\Topo & LC\\NW Ohio\\NW Ohio Topo.tif";
	

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
	thisInst.SaveFile();
	thisInst.updateThe.AllTABs();
	thisInst.BW_worker.Close();
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Load Reference
	thisMet = thisInst.metList.metItem[0];
	
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = new Reference();
	
	if (thisInst.refList.numRefDataDownloads == 0)
	{	
		string refDataFolder ="C:\\Users\\OEE2021_03\\OEE Dropbox\\Liz Walls\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum 3 GUI Testing\\TestFolder\\MERRA2 NW Ohio";
		ReferenceCollection.RefDataDownload refDataDown = thisInst.refList.ReadFileAndDefineRefDataDownload(refDataFolder);
		ReferenceCollection.DateRangeAndCompletion dateAndComplete = thisInst.refList.GetDataFileStartEndDateAndCompletion(refDataFolder, "MERRA2");
		refDataDown.startDate = dateAndComplete.startEnd[0];
                refDataDown.endDate = dateAndComplete.startEnd[1];
		
		thisInst.refList.AddRefDataDownload(refDataDown);
		thisRef.refDataDownload = refDataDown;
	}
	else
		thisRef.refDataDownload = thisInst.refList.refDataDownloads[0];
		
	thisRef.SetDefaultsForReferenceType();
        thisRef.numNodes = 1;
	thisRef.startDate = thisRef.refDataDownload.startDate;                 
        thisRef.endDate = thisRef.refDataDownload.endDate.AddHours(offset); 
        
        thisRef.Set_Interp_LatLon_Dates_Offset(theseLL.latitude, theseLL.longitude, offset, thisInst.UTM_conversions);
	thisRef.nodes = new Reference.Node_Data[thisRef.numNodes];      
	thisRef.FindCoords(thisInst.refList);
		
	thisInst.refList.AddReference(thisRef);	                    
        thisInst.refList.GetDataFromTextFiles(thisInst.refList.reference[thisInst.refList.numReferences - 1], thisInst);                	

	while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
		    Thread.Sleep(1000);
		    
	if (thisInst.BW_worker.WasReturned)
	     Assert.Fail();

	thisInst.BW_worker.Close();
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Load Reference
	thisMet = thisInst.metList.metItem[1];
	
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = new Reference();
	
	if (thisInst.refList.numRefDataDownloads == 0)
	{	
		string refDataFolder ="C:\\Users\\OEE2021_03\\OEE Dropbox\\Liz Walls\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum 3 GUI Testing\\TestFolder\\MERRA2 NW Ohio";
		ReferenceCollection.RefDataDownload refDataDown = thisInst.refList.ReadFileAndDefineRefDataDownload(refDataFolder);
		ReferenceCollection.DateRangeAndCompletion dateAndComplete = thisInst.refList.GetDataFileStartEndDateAndCompletion(refDataFolder, "MERRA2");
		refDataDown.startDate = dateAndComplete.startEnd[0];
                refDataDown.endDate = dateAndComplete.startEnd[1];
		
		thisInst.refList.AddRefDataDownload(refDataDown);
		thisRef.refDataDownload = refDataDown;
	}
	else
		thisRef.refDataDownload = thisInst.refList.refDataDownloads[0];
		
	thisRef.SetDefaultsForReferenceType();
        thisRef.numNodes = 1;
	thisRef.startDate = thisRef.refDataDownload.startDate;                 
        thisRef.endDate = thisRef.refDataDownload.endDate.AddHours(offset); 
        
        thisRef.Set_Interp_LatLon_Dates_Offset(theseLL.latitude, theseLL.longitude, offset, thisInst.UTM_conversions);
	thisRef.nodes = new Reference.Node_Data[thisRef.numNodes];      
	thisRef.FindCoords(thisInst.refList);
		
	thisInst.refList.AddReference(thisRef);	                    
        thisInst.refList.GetDataFromTextFiles(thisInst.refList.reference[thisInst.refList.numReferences - 1], thisInst);                	

	while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
		    Thread.Sleep(1000);
		    
	if (thisInst.BW_worker.WasReturned)
	     Assert.Fail();

	thisInst.BW_worker.Close();
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Load Reference
	thisMet = thisInst.metList.metItem[2];
	
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = new Reference();
	
	if (thisInst.refList.numRefDataDownloads == 0)
	{	
		string refDataFolder ="C:\\Users\\OEE2021_03\\OEE Dropbox\\Liz Walls\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum 3 GUI Testing\\TestFolder\\MERRA2 NW Ohio";
		ReferenceCollection.RefDataDownload refDataDown = thisInst.refList.ReadFileAndDefineRefDataDownload(refDataFolder);
		ReferenceCollection.DateRangeAndCompletion dateAndComplete = thisInst.refList.GetDataFileStartEndDateAndCompletion(refDataFolder, "MERRA2");
		refDataDown.startDate = dateAndComplete.startEnd[0];
                refDataDown.endDate = dateAndComplete.startEnd[1];
		
		thisInst.refList.AddRefDataDownload(refDataDown);
		thisRef.refDataDownload = refDataDown;
	}
	else
		thisRef.refDataDownload = thisInst.refList.refDataDownloads[0];
		
	thisRef.SetDefaultsForReferenceType();
        thisRef.numNodes = 1;
	thisRef.startDate = thisRef.refDataDownload.startDate;                 
        thisRef.endDate = thisRef.refDataDownload.endDate.AddHours(offset); 
        
        thisRef.Set_Interp_LatLon_Dates_Offset(theseLL.latitude, theseLL.longitude, offset, thisInst.UTM_conversions);
	thisRef.nodes = new Reference.Node_Data[thisRef.numNodes];      
	thisRef.FindCoords(thisInst.refList);
		
	thisInst.refList.AddReference(thisRef);	                    
        thisInst.refList.GetDataFromTextFiles(thisInst.refList.reference[thisInst.refList.numReferences - 1], thisInst);                	

	while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
		    Thread.Sleep(1000);
		    
	if (thisInst.BW_worker.WasReturned)
	     Assert.Fail();

	thisInst.BW_worker.Close();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Load a power curve
	string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 87-1500 PC_1.205.csv";
	thisInst.ofdPowerCurve.FileName = powerCurveFile;
	thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Do MCP
	thisMet = thisInst.metList.metItem[0];
	MCP_Method = thisInst.Get_MCP_Method();
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = thisInst.refList.GetAllRefsAtLatLong(theseLL.latitude, theseLL.longitude)[0];
	
	thisMet.WSWD_Dists = new Met.WSWD_Dist[0];
	thisInst.metList.RunMCP(ref thisMet, thisRef, thisInst, MCP_Method);
	thisMet.CalcAllLT_WSWD_Dists(thisInst); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)
	
	thisInst.metList.AreAllMetsMCPd();

	thisInst.updateThe.AllTABs();
	
        Assert.AreNotEqual(thisMet.mcpList, null, "Didn't do MCP calc");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Do MCP
	thisMet = thisInst.metList.metItem[1];
	MCP_Method = thisInst.Get_MCP_Method();
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = thisInst.refList.GetAllRefsAtLatLong(theseLL.latitude, theseLL.longitude)[0];
	
	thisMet.WSWD_Dists = new Met.WSWD_Dist[0];
	thisInst.metList.RunMCP(ref thisMet, thisRef, thisInst, MCP_Method);
	thisMet.CalcAllLT_WSWD_Dists(thisInst); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)
	
	thisInst.metList.AreAllMetsMCPd();

	thisInst.updateThe.AllTABs();
	
        Assert.AreNotEqual(thisMet.mcpList, null, "Didn't do MCP calc");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Do MCP
	thisMet = thisInst.metList.metItem[2];
	MCP_Method = thisInst.Get_MCP_Method();
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = thisInst.refList.GetAllRefsAtLatLong(theseLL.latitude, theseLL.longitude)[0];
	
	thisMet.WSWD_Dists = new Met.WSWD_Dist[0];
	thisInst.metList.RunMCP(ref thisMet, thisRef, thisInst, MCP_Method);
	thisMet.CalcAllLT_WSWD_Dists(thisInst); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)
	
	thisInst.metList.AreAllMetsMCPd();

	thisInst.updateThe.AllTABs();
	
        Assert.AreNotEqual(thisMet.mcpList, null, "Didn't do MCP calc");
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
	
	if (thisInst.metList.ThisCount > 0)
	{
		if (thisInst.metList.metItem[0].isMCPd)
		{	
			string MCP_Method = thisInst.metList.GetMCP_MethodUsed();
			argsForBW.MCP_Method = MCP_Method;
		}
        }
	
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
	
	
	thisInst.Close();
}

[TestMethod]
public void ThreeMetRawTS_LTGrossNet_867()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("", false);

	string fileName = saveFolder + "\\ThreeMetRawTS_LTGrossNet_867";
	
	if (File.Exists(fileName + ".cfm"))
	{
	File.Delete(fileName + "cfm");
        File.Delete(fileName + "mdf");
        File.Delete(fileName + "_log.ldf");
        }	

	thisInst.savedParams.savedFileName = fileName + ".cfm";
	thisInst.UTM_conversions.savedDatumIndex = 0;
	thisInst.UTM_conversions.UTMZoneNumber = 17;
	thisInst.UTM_conversions.hemisphere ="Northern";
	thisInst.isTest = true;
	thisInst.modeledHeight =  80;
	thisInst.SaveFile();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Load one met TS
	thisInst.metList.isTimeSeries = true;	
	thisInst.turbineList.genTimeSeries = true;
		
	metTSFile = testingFolder + "\\Met TS files\\NW Ohio\\OH - Paulding formatted - 20180709.csv";
	thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst, false, true);
	
	thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
	thisInst.metList.numWS = 30;

	metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
	thisMet = thisInst.metList.GetMet(metName);
	thisMet.isMCPd = false;
		
	thisMet.metData.FindStartEndDatesWithMaxRecovery();
	thisMet.metData.AddSensorDatatoDB(thisInst, thisMet.name);  
	thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

	thisInst.updateThe.AllTABs();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Load one met TS
	thisInst.metList.isTimeSeries = true;	
	thisInst.turbineList.genTimeSeries = true;
		
	metTSFile = testingFolder + "\\Met TS files\\NW Ohio\\OH - Pettisville formatted - 20180709.csv";
	thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst, false, true);
	
	thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
	thisInst.metList.numWS = 30;

	metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
	thisMet = thisInst.metList.GetMet(metName);
	thisMet.isMCPd = false;
		
	thisMet.metData.FindStartEndDatesWithMaxRecovery();
	thisMet.metData.AddSensorDatatoDB(thisInst, thisMet.name);  
	thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

	thisInst.updateThe.AllTABs();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Load one met TS
	thisInst.metList.isTimeSeries = true;	
	thisInst.turbineList.genTimeSeries = true;
		
	metTSFile = testingFolder + "\\Met TS files\\NW Ohio\\OH - Port Clinton Formatted C3 - 20190813.csv";
	thisInst.metList.ImportFilterExtrapMetData(metTSFile, thisInst, false, true);
	
	thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
	thisInst.metList.numWS = 30;

	metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
	thisMet = thisInst.metList.GetMet(metName);
	thisMet.isMCPd = false;
		
	thisMet.metData.FindStartEndDatesWithMaxRecovery();
	thisMet.metData.AddSensorDatatoDB(thisInst, thisMet.name);  
	thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

	thisInst.updateThe.AllTABs();
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Load Reference
	thisMet = thisInst.metList.metItem[0];
	
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = new Reference();
	
	if (thisInst.refList.numRefDataDownloads == 0)
	{	
		string refDataFolder ="C:\\Users\\OEE2021_03\\OEE Dropbox\\Liz Walls\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum 3 GUI Testing\\TestFolder\\MERRA2 NW Ohio";
		ReferenceCollection.RefDataDownload refDataDown = thisInst.refList.ReadFileAndDefineRefDataDownload(refDataFolder);
		ReferenceCollection.DateRangeAndCompletion dateAndComplete = thisInst.refList.GetDataFileStartEndDateAndCompletion(refDataFolder, "MERRA2");
		refDataDown.startDate = dateAndComplete.startEnd[0];
                refDataDown.endDate = dateAndComplete.startEnd[1];
		
		thisInst.refList.AddRefDataDownload(refDataDown);
		thisRef.refDataDownload = refDataDown;
	}
	else
		thisRef.refDataDownload = thisInst.refList.refDataDownloads[0];
		
	thisRef.SetDefaultsForReferenceType();
        thisRef.numNodes = 1;
	thisRef.startDate = thisRef.refDataDownload.startDate;                 
        thisRef.endDate = thisRef.refDataDownload.endDate.AddHours(offset); 
        
        thisRef.Set_Interp_LatLon_Dates_Offset(theseLL.latitude, theseLL.longitude, offset, thisInst.UTM_conversions);
	thisRef.nodes = new Reference.Node_Data[thisRef.numNodes];      
	thisRef.FindCoords(thisInst.refList);
		
	thisInst.refList.AddReference(thisRef);	                    
        thisInst.refList.GetDataFromTextFiles(thisInst.refList.reference[thisInst.refList.numReferences - 1], thisInst);                	

	while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
		    Thread.Sleep(1000);
		    
	if (thisInst.BW_worker.WasReturned)
	     Assert.Fail();

	thisInst.BW_worker.Close();
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Load Reference
	thisMet = thisInst.metList.metItem[1];
	
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = new Reference();
	
	if (thisInst.refList.numRefDataDownloads == 0)
	{	
		string refDataFolder ="C:\\Users\\OEE2021_03\\OEE Dropbox\\Liz Walls\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum 3 GUI Testing\\TestFolder\\MERRA2 NW Ohio";
		ReferenceCollection.RefDataDownload refDataDown = thisInst.refList.ReadFileAndDefineRefDataDownload(refDataFolder);
		ReferenceCollection.DateRangeAndCompletion dateAndComplete = thisInst.refList.GetDataFileStartEndDateAndCompletion(refDataFolder, "MERRA2");
		refDataDown.startDate = dateAndComplete.startEnd[0];
                refDataDown.endDate = dateAndComplete.startEnd[1];
		
		thisInst.refList.AddRefDataDownload(refDataDown);
		thisRef.refDataDownload = refDataDown;
	}
	else
		thisRef.refDataDownload = thisInst.refList.refDataDownloads[0];
		
	thisRef.SetDefaultsForReferenceType();
        thisRef.numNodes = 1;
	thisRef.startDate = thisRef.refDataDownload.startDate;                 
        thisRef.endDate = thisRef.refDataDownload.endDate.AddHours(offset); 
        
        thisRef.Set_Interp_LatLon_Dates_Offset(theseLL.latitude, theseLL.longitude, offset, thisInst.UTM_conversions);
	thisRef.nodes = new Reference.Node_Data[thisRef.numNodes];      
	thisRef.FindCoords(thisInst.refList);
		
	thisInst.refList.AddReference(thisRef);	                    
        thisInst.refList.GetDataFromTextFiles(thisInst.refList.reference[thisInst.refList.numReferences - 1], thisInst);                	

	while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
		    Thread.Sleep(1000);
		    
	if (thisInst.BW_worker.WasReturned)
	     Assert.Fail();

	thisInst.BW_worker.Close();
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Load Reference
	thisMet = thisInst.metList.metItem[2];
	
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = new Reference();
	
	if (thisInst.refList.numRefDataDownloads == 0)
	{	
		string refDataFolder ="C:\\Users\\OEE2021_03\\OEE Dropbox\\Liz Walls\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum 3 GUI Testing\\TestFolder\\MERRA2 NW Ohio";
		ReferenceCollection.RefDataDownload refDataDown = thisInst.refList.ReadFileAndDefineRefDataDownload(refDataFolder);
		ReferenceCollection.DateRangeAndCompletion dateAndComplete = thisInst.refList.GetDataFileStartEndDateAndCompletion(refDataFolder, "MERRA2");
		refDataDown.startDate = dateAndComplete.startEnd[0];
                refDataDown.endDate = dateAndComplete.startEnd[1];
		
		thisInst.refList.AddRefDataDownload(refDataDown);
		thisRef.refDataDownload = refDataDown;
	}
	else
		thisRef.refDataDownload = thisInst.refList.refDataDownloads[0];
		
	thisRef.SetDefaultsForReferenceType();
        thisRef.numNodes = 1;
	thisRef.startDate = thisRef.refDataDownload.startDate;                 
        thisRef.endDate = thisRef.refDataDownload.endDate.AddHours(offset); 
        
        thisRef.Set_Interp_LatLon_Dates_Offset(theseLL.latitude, theseLL.longitude, offset, thisInst.UTM_conversions);
	thisRef.nodes = new Reference.Node_Data[thisRef.numNodes];      
	thisRef.FindCoords(thisInst.refList);
		
	thisInst.refList.AddReference(thisRef);	                    
        thisInst.refList.GetDataFromTextFiles(thisInst.refList.reference[thisInst.refList.numReferences - 1], thisInst);                	

	while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
		    Thread.Sleep(1000);
		    
	if (thisInst.BW_worker.WasReturned)
	     Assert.Fail();

	thisInst.BW_worker.Close();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Load land cover data
	thisInst.topo.SetUS_NLCD_Key();            
	string landCoverFile = testingFolder + "\\Topo & LC\\NW Ohio\\NW Ohio Land Cover.tif";

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

	thisInst.SaveFile();
	thisInst.updateThe.AllTABs();
	thisInst.BW_worker.Close();
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Load topo data
	string topoFile = testingFolder + "\\Topo & LC\\NW Ohio\\NW Ohio Topo.tif";
	

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
	thisInst.SaveFile();
	thisInst.updateThe.AllTABs();
	thisInst.BW_worker.Close();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Load a power curve
	string powerCurveFile = testingFolder + "\\Power Curves\\Goldwind 87-1500 PC_1.205.csv";
	thisInst.ofdPowerCurve.FileName = powerCurveFile;
	thisInst.turbineList.ImportPowerCurve(thisInst, 87, 16);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Load zone sites
	string zonesFile = testingFolder + "\\Zones\\NW Ohio\\Ball 2 Zones.csv";
	thisInst.ImportZones(zonesFile);
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Load one turbine
	string turbineFile = testingFolder + "\\Turbine sites\\NW Ohio\\Harpster.csv";
	thisInst.LoadTurbines(turbineFile);

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Do MCP
	thisMet = thisInst.metList.metItem[0];
	MCP_Method = thisInst.Get_MCP_Method();
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = thisInst.refList.GetAllRefsAtLatLong(theseLL.latitude, theseLL.longitude)[0];
	
	thisMet.WSWD_Dists = new Met.WSWD_Dist[0];
	thisInst.metList.RunMCP(ref thisMet, thisRef, thisInst, MCP_Method);
	thisMet.CalcAllLT_WSWD_Dists(thisInst); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)
	
	thisInst.metList.AreAllMetsMCPd();

	thisInst.updateThe.AllTABs();
	
        Assert.AreNotEqual(thisMet.mcpList, null, "Didn't do MCP calc");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Do MCP
	thisMet = thisInst.metList.metItem[1];
	MCP_Method = thisInst.Get_MCP_Method();
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = thisInst.refList.GetAllRefsAtLatLong(theseLL.latitude, theseLL.longitude)[0];
	
	thisMet.WSWD_Dists = new Met.WSWD_Dist[0];
	thisInst.metList.RunMCP(ref thisMet, thisRef, thisInst, MCP_Method);
	thisMet.CalcAllLT_WSWD_Dists(thisInst); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)
	
	thisInst.metList.AreAllMetsMCPd();

	thisInst.updateThe.AllTABs();
	
        Assert.AreNotEqual(thisMet.mcpList, null, "Didn't do MCP calc");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Do MCP
	thisMet = thisInst.metList.metItem[2];
	MCP_Method = thisInst.Get_MCP_Method();
	theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
	offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
	
	thisRef = thisInst.refList.GetAllRefsAtLatLong(theseLL.latitude, theseLL.longitude)[0];
	
	thisMet.WSWD_Dists = new Met.WSWD_Dist[0];
	thisInst.metList.RunMCP(ref thisMet, thisRef, thisInst, MCP_Method);
	thisMet.CalcAllLT_WSWD_Dists(thisInst); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)
	
	thisInst.metList.AreAllMetsMCPd();

	thisInst.updateThe.AllTABs();
	
        Assert.AreNotEqual(thisMet.mcpList, null, "Didn't do MCP calc");
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
	
	if (thisInst.metList.ThisCount > 0)
	{
		if (thisInst.metList.metItem[0].isMCPd)
		{	
			string MCP_Method = thisInst.metList.GetMCP_MethodUsed();
			argsForBW.MCP_Method = MCP_Method;
		}
        }
	
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
	
	
	thisInst.Close();
}

}
}
