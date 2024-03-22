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
   public class DelThreeMetTABs
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

      public DelThreeMetTABs()
      {
         testingFolder = globals.testFolder;
         saveFolder = globals.saveFolder;
         refFolder = globals.merraFolder;
      }

[TestMethod]
public void DelThreeMetTABs_11()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_1.cfm";

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
        
	turbineName = "1";
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
        
	turbineName = "2";
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
        
	turbineName = "3";
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
        
	turbineName = "4";
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
        
	turbineName = "5";
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
        
	turbineName = "6";
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
        
	turbineName = "7";
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
        
	turbineName = "8";
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
        
	turbineName = "9";
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
        
	turbineName = "10";
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
        
	turbineName = "11";
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
        
	turbineName = "12";
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
        
	turbineName = "13";
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
        
	turbineName = "14";
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
        
	turbineName = "15";
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
        
	turbineName = "16";
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
        
	turbineName = "17";
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
        
	turbineName = "18";
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
        
	turbineName = "19";
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
        
	turbineName = "20";
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
        
	turbineName = "21";
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
        
	turbineName = "22";
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
        
	turbineName = "23";
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
        
	turbineName = "24";
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
        
	turbineName = "25";
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
        
	turbineName = "26";
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
        
	turbineName = "27";
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
        
	turbineName = "28";
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
        
	turbineName = "29";
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
        
	turbineName = "30";
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
        
	turbineName = "31";
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
        
	turbineName = "32";
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
        
	turbineName = "33";
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
        
	turbineName = "34";
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
        
	turbineName = "35";
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
        
	turbineName = "36";
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
        
	turbineName = "37";
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
        
	turbineName = "38";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Firewheel\\Met_1";
                                   
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
            metToDelete = "Firewheel\\Met_2";
                                   
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
            metToDelete = "Firewheel\\Met_3";
                                   
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
            metToDelete = "Firewheel\\Met_4";
                                   
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

[TestMethod]
public void DelThreeMetTABs_13()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_1.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Firewheel\\Met_1";
                                   
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
            metToDelete = "Firewheel\\Met_2";
                                   
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
            metToDelete = "Firewheel\\Met_3";
                                   
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
            metToDelete = "Firewheel\\Met_4";
                                   
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
        
	turbineName = "1";
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
        
	turbineName = "2";
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
        
	turbineName = "3";
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
        
	turbineName = "4";
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
        
	turbineName = "5";
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
        
	turbineName = "6";
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
        
	turbineName = "7";
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
        
	turbineName = "8";
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
        
	turbineName = "9";
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
        
	turbineName = "10";
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
        
	turbineName = "11";
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
        
	turbineName = "12";
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
        
	turbineName = "13";
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
        
	turbineName = "14";
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
        
	turbineName = "15";
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
        
	turbineName = "16";
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
        
	turbineName = "17";
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
        
	turbineName = "18";
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
        
	turbineName = "19";
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
        
	turbineName = "20";
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
        
	turbineName = "21";
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
        
	turbineName = "22";
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
        
	turbineName = "23";
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
        
	turbineName = "24";
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
        
	turbineName = "25";
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
        
	turbineName = "26";
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
        
	turbineName = "27";
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
        
	turbineName = "28";
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
        
	turbineName = "29";
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
        
	turbineName = "30";
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
        
	turbineName = "31";
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
        
	turbineName = "32";
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
        
	turbineName = "33";
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
        
	turbineName = "34";
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
        
	turbineName = "35";
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
        
	turbineName = "36";
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
        
	turbineName = "37";
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
        
	turbineName = "38";
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
public void DelThreeMetTABs_16()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_1.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Firewheel\\Met_1";
                                   
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
            metToDelete = "Firewheel\\Met_2";
                                   
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
            metToDelete = "Firewheel\\Met_3";
                                   
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
            metToDelete = "Firewheel\\Met_4";
                                   
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
        
	turbineName = "1";
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
        
	turbineName = "2";
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
        
	turbineName = "3";
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
        
	turbineName = "4";
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
        
	turbineName = "5";
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
        
	turbineName = "6";
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
        
	turbineName = "7";
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
        
	turbineName = "8";
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
        
	turbineName = "9";
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
        
	turbineName = "10";
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
        
	turbineName = "11";
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
        
	turbineName = "12";
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
        
	turbineName = "13";
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
        
	turbineName = "14";
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
        
	turbineName = "15";
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
        
	turbineName = "16";
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
        
	turbineName = "17";
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
        
	turbineName = "18";
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
        
	turbineName = "19";
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
        
	turbineName = "20";
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
        
	turbineName = "21";
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
        
	turbineName = "22";
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
        
	turbineName = "23";
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
        
	turbineName = "24";
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
        
	turbineName = "25";
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
        
	turbineName = "26";
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
        
	turbineName = "27";
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
        
	turbineName = "28";
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
        
	turbineName = "29";
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
        
	turbineName = "30";
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
        
	turbineName = "31";
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
        
	turbineName = "32";
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
        
	turbineName = "33";
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
        
	turbineName = "34";
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
        
	turbineName = "35";
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
        
	turbineName = "36";
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
        
	turbineName = "37";
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
        
	turbineName = "38";
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
public void DelThreeMetTABs_17()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_1.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
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
            metToDelete = "Firewheel\\Met_1";
                                   
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
            metToDelete = "Firewheel\\Met_2";
                                   
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
            metToDelete = "Firewheel\\Met_3";
                                   
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
            metToDelete = "Firewheel\\Met_4";
                                   
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
        
	turbineName = "1";
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
        
	turbineName = "2";
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
        
	turbineName = "3";
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
        
	turbineName = "4";
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
        
	turbineName = "5";
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
        
	turbineName = "6";
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
        
	turbineName = "7";
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
        
	turbineName = "8";
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
        
	turbineName = "9";
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
        
	turbineName = "10";
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
        
	turbineName = "11";
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
        
	turbineName = "12";
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
        
	turbineName = "13";
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
        
	turbineName = "14";
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
        
	turbineName = "15";
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
        
	turbineName = "16";
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
        
	turbineName = "17";
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
        
	turbineName = "18";
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
        
	turbineName = "19";
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
        
	turbineName = "20";
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
        
	turbineName = "21";
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
        
	turbineName = "22";
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
        
	turbineName = "23";
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
        
	turbineName = "24";
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
        
	turbineName = "25";
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
        
	turbineName = "26";
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
        
	turbineName = "27";
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
        
	turbineName = "28";
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
        
	turbineName = "29";
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
        
	turbineName = "30";
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
        
	turbineName = "31";
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
        
	turbineName = "32";
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
        
	turbineName = "33";
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
        
	turbineName = "34";
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
        
	turbineName = "35";
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
        
	turbineName = "36";
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
        
	turbineName = "37";
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
        
	turbineName = "38";
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
public void DelThreeMetTABs_18()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_1.cfm";

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
	else            
	MessageBox.Show("No power curves to delete", "Continuum 3");                                

	thisInst.updateThe.ColoredButtons();
	Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Firewheel\\Met_1";
                                   
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
            metToDelete = "Firewheel\\Met_2";
                                   
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
            metToDelete = "Firewheel\\Met_3";
                                   
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
            metToDelete = "Firewheel\\Met_4";
                                   
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
        
	turbineName = "1";
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
        
	turbineName = "2";
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
        
	turbineName = "3";
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
        
	turbineName = "4";
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
        
	turbineName = "5";
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
        
	turbineName = "6";
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
        
	turbineName = "7";
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
        
	turbineName = "8";
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
        
	turbineName = "9";
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
        
	turbineName = "10";
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
        
	turbineName = "11";
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
        
	turbineName = "12";
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
        
	turbineName = "13";
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
        
	turbineName = "14";
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
        
	turbineName = "15";
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
        
	turbineName = "16";
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
        
	turbineName = "17";
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
        
	turbineName = "18";
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
        
	turbineName = "19";
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
        
	turbineName = "20";
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
        
	turbineName = "21";
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
        
	turbineName = "22";
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
        
	turbineName = "23";
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
        
	turbineName = "24";
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
        
	turbineName = "25";
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
        
	turbineName = "26";
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
        
	turbineName = "27";
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
        
	turbineName = "28";
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
        
	turbineName = "29";
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
        
	turbineName = "30";
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
        
	turbineName = "31";
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
        
	turbineName = "32";
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
        
	turbineName = "33";
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
        
	turbineName = "34";
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
        
	turbineName = "35";
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
        
	turbineName = "36";
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
        
	turbineName = "37";
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
        
	turbineName = "38";
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
public void DelThreeMetTABs_2()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_1.cfm";

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
        
	turbineName = "1";
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
        
	turbineName = "2";
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
        
	turbineName = "3";
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
        
	turbineName = "4";
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
        
	turbineName = "5";
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
        
	turbineName = "6";
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
        
	turbineName = "7";
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
        
	turbineName = "8";
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
        
	turbineName = "9";
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
        
	turbineName = "10";
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
        
	turbineName = "11";
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
        
	turbineName = "12";
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
        
	turbineName = "13";
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
        
	turbineName = "14";
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
        
	turbineName = "15";
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
        
	turbineName = "16";
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
        
	turbineName = "17";
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
        
	turbineName = "18";
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
        
	turbineName = "19";
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
        
	turbineName = "20";
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
        
	turbineName = "21";
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
        
	turbineName = "22";
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
        
	turbineName = "23";
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
        
	turbineName = "24";
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
        
	turbineName = "25";
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
        
	turbineName = "26";
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
        
	turbineName = "27";
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
        
	turbineName = "28";
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
        
	turbineName = "29";
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
        
	turbineName = "30";
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
        
	turbineName = "31";
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
        
	turbineName = "32";
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
        
	turbineName = "33";
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
        
	turbineName = "34";
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
        
	turbineName = "35";
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
        
	turbineName = "36";
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
        
	turbineName = "37";
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
        
	turbineName = "38";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Firewheel\\Met_1";
                                   
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
            metToDelete = "Firewheel\\Met_2";
                                   
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
            metToDelete = "Firewheel\\Met_3";
                                   
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
            metToDelete = "Firewheel\\Met_4";
                                   
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
public void DelThreeMetTABs_24()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_1.cfm";

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
        
	turbineName = "1";
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
        
	turbineName = "2";
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
        
	turbineName = "3";
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
        
	turbineName = "4";
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
        
	turbineName = "5";
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
        
	turbineName = "6";
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
        
	turbineName = "7";
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
        
	turbineName = "8";
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
        
	turbineName = "9";
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
        
	turbineName = "10";
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
        
	turbineName = "11";
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
        
	turbineName = "12";
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
        
	turbineName = "13";
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
        
	turbineName = "14";
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
        
	turbineName = "15";
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
        
	turbineName = "16";
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
        
	turbineName = "17";
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
        
	turbineName = "18";
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
        
	turbineName = "19";
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
        
	turbineName = "20";
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
        
	turbineName = "21";
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
        
	turbineName = "22";
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
        
	turbineName = "23";
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
        
	turbineName = "24";
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
        
	turbineName = "25";
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
        
	turbineName = "26";
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
        
	turbineName = "27";
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
        
	turbineName = "28";
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
        
	turbineName = "29";
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
        
	turbineName = "30";
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
        
	turbineName = "31";
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
        
	turbineName = "32";
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
        
	turbineName = "33";
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
        
	turbineName = "34";
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
        
	turbineName = "35";
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
        
	turbineName = "36";
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
        
	turbineName = "37";
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
        
	turbineName = "38";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
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
            metToDelete = "Firewheel\\Met_1";
                                   
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
            metToDelete = "Firewheel\\Met_2";
                                   
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
            metToDelete = "Firewheel\\Met_3";
                                   
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
            metToDelete = "Firewheel\\Met_4";
                                   
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
public void DelThreeMetTABs_6()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_1.cfm";

	thisInst.Open(fileName);
	string newFileName = fileName.Substring(0, fileName.LastIndexOf('.') - 1) + "DelTest.cfm";
	thisInst.savedParams.savedFileName = newFileName;
	thisInst.SaveFile();
	
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
        
	turbineName = "1";
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
        
	turbineName = "2";
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
        
	turbineName = "3";
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
        
	turbineName = "4";
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
        
	turbineName = "5";
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
        
	turbineName = "6";
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
        
	turbineName = "7";
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
        
	turbineName = "8";
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
        
	turbineName = "9";
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
        
	turbineName = "10";
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
        
	turbineName = "11";
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
        
	turbineName = "12";
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
        
	turbineName = "13";
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
        
	turbineName = "14";
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
        
	turbineName = "15";
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
        
	turbineName = "16";
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
        
	turbineName = "17";
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
        
	turbineName = "18";
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
        
	turbineName = "19";
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
        
	turbineName = "20";
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
        
	turbineName = "21";
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
        
	turbineName = "22";
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
        
	turbineName = "23";
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
        
	turbineName = "24";
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
        
	turbineName = "25";
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
        
	turbineName = "26";
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
        
	turbineName = "27";
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
        
	turbineName = "28";
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
        
	turbineName = "29";
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
        
	turbineName = "30";
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
        
	turbineName = "31";
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
        
	turbineName = "32";
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
        
	turbineName = "33";
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
        
	turbineName = "34";
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
        
	turbineName = "35";
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
        
	turbineName = "36";
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
        
	turbineName = "37";
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
        
	turbineName = "38";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Firewheel\\Met_1";
                                   
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
            metToDelete = "Firewheel\\Met_2";
                                   
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
            metToDelete = "Firewheel\\Met_3";
                                   
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
            metToDelete = "Firewheel\\Met_4";
                                   
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
public void DelThreeMetTABs_7()
{
	Stopwatch stopWatch = new Stopwatch();
	stopWatch.Start();

	Continuum thisInst = new Continuum("");

string fileName = saveFolder + "\\ThreeMetTABAndGrossNet_1.cfm";

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
        
	turbineName = "1";
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
        
	turbineName = "2";
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
        
	turbineName = "3";
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
        
	turbineName = "4";
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
        
	turbineName = "5";
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
        
	turbineName = "6";
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
        
	turbineName = "7";
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
        
	turbineName = "8";
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
        
	turbineName = "9";
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
        
	turbineName = "10";
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
        
	turbineName = "11";
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
        
	turbineName = "12";
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
        
	turbineName = "13";
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
        
	turbineName = "14";
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
        
	turbineName = "15";
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
        
	turbineName = "16";
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
        
	turbineName = "17";
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
        
	turbineName = "18";
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
        
	turbineName = "19";
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
        
	turbineName = "20";
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
        
	turbineName = "21";
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
        
	turbineName = "22";
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
        
	turbineName = "23";
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
        
	turbineName = "24";
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
        
	turbineName = "25";
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
        
	turbineName = "26";
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
        
	turbineName = "27";
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
        
	turbineName = "28";
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
        
	turbineName = "29";
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
        
	turbineName = "30";
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
        
	turbineName = "31";
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
        
	turbineName = "32";
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
        
	turbineName = "33";
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
        
	turbineName = "34";
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
        
	turbineName = "35";
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
        
	turbineName = "36";
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
        
	turbineName = "37";
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
        
	turbineName = "38";
	thisInst.turbineList.DeleteTurbine(turbineName);
	
	thisInst.ChangesMade();

	thisInst.siteSuitability.ClearAll();
	thisInst.updateThe.AllTABs();
	
	Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - 1, "Didn't delete turbine site");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Firewheel\\Met_1";
                                   
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
            metToDelete = "Firewheel\\Met_2";
                                   
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
            metToDelete = "Firewheel\\Met_3";
                                   
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
            metToDelete = "Firewheel\\Met_4";
                                   
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
