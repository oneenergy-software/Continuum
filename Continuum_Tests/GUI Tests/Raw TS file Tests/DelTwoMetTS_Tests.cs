using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Continuum_Tests.GUI_Tests
{
    [TestClass]
    public class DelTwoMetTS_Tests
    {
        string testingFolder = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\TestFolder";
        string saveFolder = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder";

        string merraFolder = "C:\\Users\\liz_w\\Desktop\\MERRA2";
        string metTSFile;
        string metName;
        string MCP_Method;
        Met thisMet;
        UTM_conversion.Lat_Long theseLL;
        int offset;
        MERRA thisMERRA;
        DialogResult goodToGo;
        string turbineName;
        string metToDelete;
        int numToDelete;
        int numZones;
        int metCount;
        int numZonesToDelete;
        int turbCount;
        int numTurbines;
        int crvCount;

        [TestMethod]
        public void DelTwoMetTS_1()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_1";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_10()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_10";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_11()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_11";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_12()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_12";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_13()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_13";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_14()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_14";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_15()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_15";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_16()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_16";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_17()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_17";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_18()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_18";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_19()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_19";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_2()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_2";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_20()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_20";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_21()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_21";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_22()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_22";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_23()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_23";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_24()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_24";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_3()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_3";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_4()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_4";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_5()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_5";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_6()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_6";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_7()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_7";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_8()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_8";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

            Assert.AreEqual(thisInst.siteSuitability.GetNumZones(), numZones - numZonesToDelete, "Didn't delete zone site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            thisInst.SaveFile(true);

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
        public void DelTwoMetTS_9()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Continuum thisInst = new Continuum();

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\TwoMetRawTSWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");

            thisInst.sfdCFMfile.FileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\DelTwoMetTS_9";

            if (File.Exists(thisInst.sfdCFMfile.FileName + ".cfm"))
            {
                File.Delete(thisInst.sfdCFMfile.FileName + ".cfm");
                File.Delete(thisInst.sfdCFMfile.FileName + ".mdf");
                File.Delete(thisInst.sfdCFMfile.FileName + "_log.ldf");
            }

            // Get all met data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);

            // Save mdf file to new location, update connection string
            // create database in new location
            BackgroundWork.Vars_for_Save_As argsForBW = new BackgroundWork.Vars_for_Save_As();
            argsForBW.oldFilename = thisInst.savedParams.savedFileName;
            argsForBW.newFilename = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.savedParams.savedFileName = thisInst.sfdCFMfile.FileName + ".cfm";
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_SaveAs(argsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            FileStream fStream = new FileStream(thisInst.sfdCFMfile.FileName + ".cfm", FileMode.Create, FileAccess.Write);
            BinaryFormatter bin = new BinaryFormatter();

            thisInst.topo.elevsForCalcs = null;
            thisInst.topo.DH_ForCalcs = null;
            thisInst.topo.SR_ForCalcs = null;

            // Save all anem, vane, and temperature data to database and clear from metItem            
            thisInst.metList.AddAllMetDataToDBAndClear(thisInst);

            // Clear all MCP reference, target, and concurrent data
            thisInst.metList.ClearMCPRefTargetConcLTEstData();

            // Clear all MERRA interp data and node data
            thisInst.merraList.ClearMERRA_Data();

            // Clear turbine time series data
            thisInst.turbineList.ClearTimeSeries();

            bin.Serialize(fStream, thisInst.topo);
            bin.Serialize(fStream, thisInst.radiiList);
            bin.Serialize(fStream, thisInst.metList);
            bin.Serialize(fStream, thisInst.turbineList);
            bin.Serialize(fStream, thisInst.savedParams);
            bin.Serialize(fStream, thisInst.mapList);
            bin.Serialize(fStream, thisInst.metPairList);
            bin.Serialize(fStream, thisInst.modelList);
            bin.Serialize(fStream, thisInst.UTM_conversions);
            bin.Serialize(fStream, thisInst.wakeModelList);
            bin.Serialize(fStream, thisInst.modeledHeight);
            bin.Serialize(fStream, thisInst.merraList);
            bin.Serialize(fStream, thisInst.siteSuitability);

            fStream.Close();

            thisInst.Text = thisInst.savedParams.savedFileName;

            thisInst.fileChanged = false;
            thisInst.saveToolStripMenuItem.Enabled = true;

            // Get met sensor data from DB
            if (thisInst.metList.isTimeSeries)
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                    thisInst.metList.metItem[i].metData.GetSensorDataFromDB(thisInst, thisInst.metList.metItem[i].name);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "Ashtabula";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete met
            metToDelete = "New Bremen";

            for (int i = 0; i < thisInst.lstMetTowers.Items.Count; i++)
                if (thisInst.lstMetTowers.Items[i].Text == metToDelete)
                    thisInst.lstMetTowers.Items[i].Selected = true;
                else
                    thisInst.lstMetTowers.Items[i].Selected = false;

            // This refreshes the SelectedItems parameter (bug in Visual Studio unit tests... SelectedItems and SelectedIndices not updated)
            thisInst.lstMetTowers.AccessibilityObject.ToString();
            metCount = thisInst.metList.ThisCount;
            numToDelete = thisInst.lstMetTowers.SelectedItems.Count;

            thisInst.DeleteMet(true);

            thisInst.SaveFile(true);
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.metList.ThisCount, metCount - numToDelete, "Didn't delete met site");
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

            thisInst.updateThe.ZoneList(thisInst);
            thisInst.updateThe.SiteSuitabilityTAB(thisInst);

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
            numToDelete = 1;
            turbineName = "Ball_Z4";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z5";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delete Turbines

            goodToGo = DialogResult.Yes;
            // Clear all net estimates (different set of turbines)
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                //   goodToGo = MessageBox.Show("Deleting turbine sites will cause all wake models and wake maps to be deleted. Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.Yes)
                {
                    thisInst.wakeModelList.ClearAll();
                    thisInst.turbineList.ClearAllNetEsts();
                    thisInst.mapList.ClearAllWakedMaps();
                    thisInst.siteSuitability.ClearAll();
                }
                else
                    return;

            }

            turbCount = thisInst.turbineList.TurbineCount;
            numToDelete = 1;
            turbineName = "Ball_Z6";
            thisInst.turbineList.DeleteTurbine(turbineName);

            thisInst.ChangesMade();

            thisInst.siteSuitability.ClearAll();
            thisInst.updateThe.AllTABs(thisInst);

            Assert.AreEqual(thisInst.turbineList.TurbineCount, turbCount - numToDelete, "Didn't delete turbine site");
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
                thisInst.merraList.ClearMERRA_ProdStats();
                thisInst.updateThe.AllTABs(thisInst);

            }
            else
                MessageBox.Show("No power curves to delete", "Continuum 3");

            thisInst.updateThe.ColoredButtons(thisInst);
            Assert.AreEqual(thisInst.turbineList.PowerCurveCount, crvCount - numToDelete, "Didn't delete power curves");
            thisInst.SaveFile(true);

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
