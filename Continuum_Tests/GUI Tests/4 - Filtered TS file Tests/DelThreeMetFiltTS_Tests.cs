using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Continuum_Tests.GUI_Tests
{
   [TestClass]
   public class DelThreeMetFiltTS
   {
      string testingFolder;
      string saveFolder;
      string refFolder;
      Globals globals = new Globals();

      string metToDelete;
      int turbCount;
      string turbineName;
      int crvCount;
      int numToDelete;
      int numZones;
      int numZonesToDelete;
      Reference thisRef;

      public DelThreeMetFiltTS()
      {
         testingFolder = globals.testFolder;
         saveFolder = globals.saveFolder;
         refFolder = globals.merraFolder;
      }

[TestMethod]
public void DelThreeMetFiltTS_11()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("", false);
	thisInst.isTest = true;
string fileName = saveFolder + "\\ThreeMetFiltTS_GrossNet_311.cfm";

	thisInst.Open(fileName);
	thisInst.metList.GetTimeSeriesData(thisInst);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	// Clear all net estimates (different set of turbines)
	if (thisInst.wakeModelList.NumWakeModels > 0)
	{
	   thisInst.wakeModelList.ClearAll();
	   thisInst.turbineList.ClearAllNetEsts();
	   thisInst.mapList.ClearAllWakedMaps();
	   thisInst.siteSuitability.ClearAll();
	}
	
	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Harpster";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Paulding";
                                   
            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;
                
            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();                        
            
            thisInst.DeleteMet(true);

            thisInst.SaveFile();
            thisInst.updateThe.AllTABs();

            Assert.AreEqual(thisInst.metList.ThisCount, thisInst.metList.ThisCount - thisInst.lstMetTowers.SelectedItems.Count, "Didn't delete met site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Pettisville";
                                   
            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;
                
            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();                        
            
            thisInst.DeleteMet(true);

            thisInst.SaveFile();
            thisInst.updateThe.AllTABs();

            Assert.AreEqual(thisInst.metList.ThisCount, thisInst.metList.ThisCount - thisInst.lstMetTowers.SelectedItems.Count, "Didn't delete met site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Port Clinton";
                                   
            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;
                
            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();                        
            
            thisInst.DeleteMet(true);

            thisInst.SaveFile();
            thisInst.updateThe.AllTABs();

            Assert.AreEqual(thisInst.metList.ThisCount, thisInst.metList.ThisCount - thisInst.lstMetTowers.SelectedItems.Count, "Didn't delete met site");
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Zones
		
	for (int i = 0; i < thisInst.lstZones.Items.Count; i++)
	{
if (thisInst.lstZones.Items[i].Text == "Z1")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z2")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z3")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z4")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z5")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z6")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z7")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z8")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z9")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z10")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z11")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z12")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z13")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z14")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z15")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z16")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z17")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z18")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z19")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z20")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z21")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z22")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z23")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z24")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z25")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z26")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z27")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z28")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z29")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z30")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z31")
	thisInst.lstZones.Items[i].Selected = true;


	}

	// This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
	thisInst.lstZones.AccessibilityObject.ToString();

	numZones = thisInst.siteSuitability.GetNumZones();
	numZonesToDelete = thisInst.lstZones.SelectedItems.Count;
		
	for (int i = 0; i < numZonesToDelete; i++)
	{                    
	    string zoneName = thisInst.lstZones.SelectedItems[i].Text;
	    thisInst.siteSuitability.DeleteZone(zoneName);
	}
	thisInst.ChangesMade();
	
	// If sound map has been created, recreate using new bounds
	if (thisInst.siteSuitability.soundMap != null)
	thisInst.siteSuitability.CreateSoundMap(thisInst);

	thisInst.updateThe.ZoneList();            
	thisInst.updateThe.SiteSuitabilityTAB(); 
	
	Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Power Curve                                   

	for (int i = 0; i < thisInst.lstPowerCurveList.Items.Count; i++)
	{
	    if (thisInst.lstPowerCurveList.Items[i].Text == "Goldwind 87-1500 PC_1.205")
thisInst.lstPowerCurveList.Items[i].Selected = true;


	}
	
	// This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
	thisInst.lstPowerCurveList.AccessibilityObject.ToString();
	
	crvCount = thisInst.turbineList.PowerCurveCount;
        numToDelete = thisInst.lstPowerCurveList.SelectedItems.Count;

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
	thisInst.refList.ClearReferenceProdStats();
	thisInst.updateThe.AllTABs();

	}            	                              

	thisInst.updateThe.ColoredButtons();
	Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
	thisInst.SaveFile();
		
	NodeCollection nodeList = new NodeCollection();
	
	if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
	{
	    
	     string connString = nodeList.GetDB_ConnectionString(thisInst.sfdCFMfile.FileName + ".cfm");

	using (var ctx = new Continuum_EDMContainer(connString))
	{
	    ctx.Database.Delete();
	}
	
	File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
    }
	
	thisInst.Close();
}

[TestMethod]
public void DelThreeMetFiltTS_2()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("", false);
	thisInst.isTest = true;
string fileName = saveFolder + "\\ThreeMetFiltTS_GrossNet_311.cfm";

	thisInst.Open(fileName);
	thisInst.metList.GetTimeSeriesData(thisInst);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	// Clear all net estimates (different set of turbines)
	if (thisInst.wakeModelList.NumWakeModels > 0)
	{
	   thisInst.wakeModelList.ClearAll();
	   thisInst.turbineList.ClearAllNetEsts();
	   thisInst.mapList.ClearAllWakedMaps();
	   thisInst.siteSuitability.ClearAll();
	}
	
	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Harpster";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Paulding";
                                   
            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;
                
            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();                        
            
            thisInst.DeleteMet(true);

            thisInst.SaveFile();
            thisInst.updateThe.AllTABs();

            Assert.AreEqual(thisInst.metList.ThisCount, thisInst.metList.ThisCount - thisInst.lstMetTowers.SelectedItems.Count, "Didn't delete met site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Pettisville";
                                   
            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;
                
            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();                        
            
            thisInst.DeleteMet(true);

            thisInst.SaveFile();
            thisInst.updateThe.AllTABs();

            Assert.AreEqual(thisInst.metList.ThisCount, thisInst.metList.ThisCount - thisInst.lstMetTowers.SelectedItems.Count, "Didn't delete met site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Port Clinton";
                                   
            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;
                
            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();                        
            
            thisInst.DeleteMet(true);

            thisInst.SaveFile();
            thisInst.updateThe.AllTABs();

            Assert.AreEqual(thisInst.metList.ThisCount, thisInst.metList.ThisCount - thisInst.lstMetTowers.SelectedItems.Count, "Didn't delete met site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Power Curve                                   

	for (int i = 0; i < thisInst.lstPowerCurveList.Items.Count; i++)
	{
	    if (thisInst.lstPowerCurveList.Items[i].Text == "Goldwind 87-1500 PC_1.205")
thisInst.lstPowerCurveList.Items[i].Selected = true;


	}
	
	// This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
	thisInst.lstPowerCurveList.AccessibilityObject.ToString();
	
	crvCount = thisInst.turbineList.PowerCurveCount;
        numToDelete = thisInst.lstPowerCurveList.SelectedItems.Count;

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
	thisInst.refList.ClearReferenceProdStats();
	thisInst.updateThe.AllTABs();

	}            	                              

	thisInst.updateThe.ColoredButtons();
	Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Zones
		
	for (int i = 0; i < thisInst.lstZones.Items.Count; i++)
	{
if (thisInst.lstZones.Items[i].Text == "Z1")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z2")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z3")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z4")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z5")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z6")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z7")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z8")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z9")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z10")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z11")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z12")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z13")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z14")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z15")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z16")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z17")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z18")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z19")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z20")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z21")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z22")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z23")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z24")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z25")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z26")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z27")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z28")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z29")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z30")
	thisInst.lstZones.Items[i].Selected = true;

if (thisInst.lstZones.Items[i].Text == "Z31")
	thisInst.lstZones.Items[i].Selected = true;


	}

	// This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
	thisInst.lstZones.AccessibilityObject.ToString();

	numZones = thisInst.siteSuitability.GetNumZones();
	numZonesToDelete = thisInst.lstZones.SelectedItems.Count;
		
	for (int i = 0; i < numZonesToDelete; i++)
	{                    
	    string zoneName = thisInst.lstZones.SelectedItems[i].Text;
	    thisInst.siteSuitability.DeleteZone(zoneName);
	}
	thisInst.ChangesMade();
	
	// If sound map has been created, recreate using new bounds
	if (thisInst.siteSuitability.soundMap != null)
	thisInst.siteSuitability.CreateSoundMap(thisInst);

	thisInst.updateThe.ZoneList();            
	thisInst.updateThe.SiteSuitabilityTAB(); 
	
	Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
	thisInst.SaveFile();
		
	NodeCollection nodeList = new NodeCollection();
	
	if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
	{
	    
	     string connString = nodeList.GetDB_ConnectionString(thisInst.sfdCFMfile.FileName + ".cfm");

	using (var ctx = new Continuum_EDMContainer(connString))
	{
	    ctx.Database.Delete();
	}
	
	File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
    }
	
	thisInst.Close();
}

}
}
