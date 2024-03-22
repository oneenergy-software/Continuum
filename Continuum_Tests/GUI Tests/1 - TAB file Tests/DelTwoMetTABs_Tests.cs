using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Continuum_Tests.GUI_Tests
{
   [TestClass]
   public class DelTwoMetTABs
   {
      string testingFolder;
      string saveFolder;
      string refFolder;
      Globals globals = new Globals();

        DialogResult goodToGo;
      string metToDelete;
      int turbCount;
      string turbineName;
      int crvCount;
      int numToDelete;
      int numZones;
      int numZonesToDelete;
      Reference thisRef;

      public DelTwoMetTABs()
      {
         testingFolder = globals.testFolder;
         saveFolder = globals.saveFolder;
         refFolder = globals.merraFolder;
      }

[TestMethod]
public void DelTwoMetTABs_1()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\TwoMetTABAndGrossNet_100.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "NW Ohio\\Archbold";
                                   
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
            metToDelete = "NW Ohio\\Paulding";
                                   
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
            metToDelete = "NW Ohio\\Sullivan";
                                   
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
            metToDelete = "NW Ohio\\Wapakoneta";
                                   
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
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z4";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z5";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z6";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Power Curve                                   

	for (int i = 0; i < thisInst.lstPowerCurveList.Items.Count; i++)
	{
	    if (thisInst.lstPowerCurveList.Items[i].Text == "Vestas_V150@1.041kgm^-3")
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
	else            
	MessageBox.Show("No power curves to delete", "Continuum 3");                                

	thisInst.updateThe.ColoredButtons();
	Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Zones
	
	goodToGo = DialogResult.Yes; // MessageBox.Show("Are you sure you want to delete these " + numZonesToDelete + " zones?", "Continuum 3", MessageBoxButtons.YesNo);

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

[TestMethod]
public void DelTwoMetTABs_16()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\TwoMetTABAndGrossNet_100.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "NW Ohio\\Archbold";
                                   
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
            metToDelete = "NW Ohio\\Paulding";
                                   
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
            metToDelete = "NW Ohio\\Sullivan";
                                   
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
            metToDelete = "NW Ohio\\Wapakoneta";
                                   
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
	
	goodToGo = DialogResult.Yes; // MessageBox.Show("Are you sure you want to delete these " + numZonesToDelete + " zones?", "Continuum 3", MessageBoxButtons.YesNo);

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

	thisInst.updateThe.ZoneList();            
	thisInst.updateThe.SiteSuitabilityTAB(); 
	
	Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Power Curve                                   

	for (int i = 0; i < thisInst.lstPowerCurveList.Items.Count; i++)
	{
	    if (thisInst.lstPowerCurveList.Items[i].Text == "Vestas_V150@1.041kgm^-3")
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
	else            
	MessageBox.Show("No power curves to delete", "Continuum 3");                                

	thisInst.updateThe.ColoredButtons();
	Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z4";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z5";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z6";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
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
public void DelTwoMetTABs_19()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\TwoMetTABAndGrossNet_100.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Zones
	
	goodToGo = DialogResult.Yes; // MessageBox.Show("Are you sure you want to delete these " + numZonesToDelete + " zones?", "Continuum 3", MessageBoxButtons.YesNo);

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

	thisInst.updateThe.ZoneList();            
	thisInst.updateThe.SiteSuitabilityTAB(); 
	
	Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Power Curve                                   

	for (int i = 0; i < thisInst.lstPowerCurveList.Items.Count; i++)
	{
	    if (thisInst.lstPowerCurveList.Items[i].Text == "Vestas_V150@1.041kgm^-3")
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
	else            
	MessageBox.Show("No power curves to delete", "Continuum 3");                                

	thisInst.updateThe.ColoredButtons();
	Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z4";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z5";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z6";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "NW Ohio\\Archbold";
                                   
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
            metToDelete = "NW Ohio\\Paulding";
                                   
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
            metToDelete = "NW Ohio\\Sullivan";
                                   
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
            metToDelete = "NW Ohio\\Wapakoneta";
                                   
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
public void DelTwoMetTABs_21()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\TwoMetTABAndGrossNet_100.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z4";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z5";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z6";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Zones
	
	goodToGo = DialogResult.Yes; // MessageBox.Show("Are you sure you want to delete these " + numZonesToDelete + " zones?", "Continuum 3", MessageBoxButtons.YesNo);

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

	thisInst.updateThe.ZoneList();            
	thisInst.updateThe.SiteSuitabilityTAB(); 
	
	Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Power Curve                                   

	for (int i = 0; i < thisInst.lstPowerCurveList.Items.Count; i++)
	{
	    if (thisInst.lstPowerCurveList.Items[i].Text == "Vestas_V150@1.041kgm^-3")
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
	else            
	MessageBox.Show("No power curves to delete", "Continuum 3");                                

	thisInst.updateThe.ColoredButtons();
	Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "NW Ohio\\Archbold";
                                   
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
            metToDelete = "NW Ohio\\Paulding";
                                   
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
            metToDelete = "NW Ohio\\Sullivan";
                                   
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
            metToDelete = "NW Ohio\\Wapakoneta";
                                   
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
public void DelTwoMetTABs_24()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\TwoMetTABAndGrossNet_100.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z4";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z5";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z6";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Power Curve                                   

	for (int i = 0; i < thisInst.lstPowerCurveList.Items.Count; i++)
	{
	    if (thisInst.lstPowerCurveList.Items[i].Text == "Vestas_V150@1.041kgm^-3")
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
	else            
	MessageBox.Show("No power curves to delete", "Continuum 3");                                

	thisInst.updateThe.ColoredButtons();
	Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Zones
	
	goodToGo = DialogResult.Yes; // MessageBox.Show("Are you sure you want to delete these " + numZonesToDelete + " zones?", "Continuum 3", MessageBoxButtons.YesNo);

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

	thisInst.updateThe.ZoneList();            
	thisInst.updateThe.SiteSuitabilityTAB(); 
	
	Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "NW Ohio\\Archbold";
                                   
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
            metToDelete = "NW Ohio\\Paulding";
                                   
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
            metToDelete = "NW Ohio\\Sullivan";
                                   
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
            metToDelete = "NW Ohio\\Wapakoneta";
                                   
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
public void DelTwoMetTABs_4()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\TwoMetTABAndGrossNet_100.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "NW Ohio\\Archbold";
                                   
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
            metToDelete = "NW Ohio\\Paulding";
                                   
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
            metToDelete = "NW Ohio\\Sullivan";
                                   
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
            metToDelete = "NW Ohio\\Wapakoneta";
                                   
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
	    if (thisInst.lstPowerCurveList.Items[i].Text == "Vestas_V150@1.041kgm^-3")
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
	else            
	MessageBox.Show("No power curves to delete", "Continuum 3");                                

	thisInst.updateThe.ColoredButtons();
	Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z4";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z5";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z6";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Zones
	
	goodToGo = DialogResult.Yes; // MessageBox.Show("Are you sure you want to delete these " + numZonesToDelete + " zones?", "Continuum 3", MessageBoxButtons.YesNo);

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

[TestMethod]
public void DelTwoMetTABs_5()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\TwoMetTABAndGrossNet_100.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z4";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z5";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z6";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Power Curve                                   

	for (int i = 0; i < thisInst.lstPowerCurveList.Items.Count; i++)
	{
	    if (thisInst.lstPowerCurveList.Items[i].Text == "Vestas_V150@1.041kgm^-3")
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
	else            
	MessageBox.Show("No power curves to delete", "Continuum 3");                                

	thisInst.updateThe.ColoredButtons();
	Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "NW Ohio\\Archbold";
                                   
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
            metToDelete = "NW Ohio\\Paulding";
                                   
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
            metToDelete = "NW Ohio\\Sullivan";
                                   
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
            metToDelete = "NW Ohio\\Wapakoneta";
                                   
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
	
	goodToGo = DialogResult.Yes; // MessageBox.Show("Are you sure you want to delete these " + numZonesToDelete + " zones?", "Continuum 3", MessageBoxButtons.YesNo);

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

[TestMethod]
public void DelTwoMetTABs_6()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\TwoMetTABAndGrossNet_100.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Power Curve                                   

	for (int i = 0; i < thisInst.lstPowerCurveList.Items.Count; i++)
	{
	    if (thisInst.lstPowerCurveList.Items[i].Text == "Vestas_V150@1.041kgm^-3")
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
	else            
	MessageBox.Show("No power curves to delete", "Continuum 3");                                

	thisInst.updateThe.ColoredButtons();
	Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z4";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z5";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z6";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "NW Ohio\\Archbold";
                                   
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
            metToDelete = "NW Ohio\\Paulding";
                                   
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
            metToDelete = "NW Ohio\\Sullivan";
                                   
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
            metToDelete = "NW Ohio\\Wapakoneta";
                                   
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
	
	goodToGo = DialogResult.Yes; // MessageBox.Show("Are you sure you want to delete these " + numZonesToDelete + " zones?", "Continuum 3", MessageBoxButtons.YesNo);

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

[TestMethod]
public void DelTwoMetTABs_8()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\TwoMetTABAndGrossNet_100.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z4";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z5";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Turbines

	goodToGo = DialogResult.Yes;
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

	turbCount = thisInst.turbineList.TurbineCount;
        
	turbineName = "Ball_Z6";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Delete Zones
	
	goodToGo = DialogResult.Yes; // MessageBox.Show("Are you sure you want to delete these " + numZonesToDelete + " zones?", "Continuum 3", MessageBoxButtons.YesNo);

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

	thisInst.updateThe.ZoneList();            
	thisInst.updateThe.SiteSuitabilityTAB(); 
	
	Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "NW Ohio\\Archbold";
                                   
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
            metToDelete = "NW Ohio\\Paulding";
                                   
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
            metToDelete = "NW Ohio\\Sullivan";
                                   
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
            metToDelete = "NW Ohio\\Wapakoneta";
                                   
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
	    if (thisInst.lstPowerCurveList.Items[i].Text == "Vestas_V150@1.041kgm^-3")
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
	else            
	MessageBox.Show("No power curves to delete", "Continuum 3");                                

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

}
}
