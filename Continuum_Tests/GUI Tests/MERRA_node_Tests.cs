using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Continuum_Tests.GUI_Tests
{
    [TestClass]
    public class MERRA_node_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_27\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Continuum";
        string saveFolder = "C:\\Users\\OEE2017_27\\Desktop\\Continuum tests";

        [TestMethod]
        public void Changing_Num_MERRA_Nodes_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.isTest = true;

            string fileName = "C:\\Users\\OEE2017_27\\Desktop\\Continuum tests\\MERRA_Node_Test";
            thisInst.savedParams.savedFileName = fileName + ".cfm";

            if (File.Exists(fileName + ".cfm"))
            {
                File.Delete(fileName + ".cfm");
                File.Delete(fileName + ".mdf");
                File.Delete(fileName + "_log.ldf");
            }
                       
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 16;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 80;
            thisInst.SaveFile(true);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Load one met TS
            thisInst.metList.isTimeSeries = true;
            thisInst.metList.filteringEnabled = true;
            thisInst.turbineList.genTimeSeries = true;

            string metTSFile = testingFolder + "\\Met TS files\\Findlay\\Archbold TS short Findlay coords.csv";
            thisInst.metList.ImportFilterExtrapMetDataContinuum(metTSFile, thisInst); // Reads in formatted .csv file, filters and extrapolates to modeled height

            thisInst.metList.numWD = Convert.ToInt16(thisInst.cboMCPNumWD.SelectedItem);
            thisInst.metList.numWS = 30;

            string metName = thisInst.metList.metItem[thisInst.metList.ThisCount - 1].name;
            Met thisMet = thisInst.metList.GetMet(metName);
            thisMet.metData.FindStartEndDatesWithMaxRecovery();

            thisInst.metList.isMCPd = false;
            thisMet.CalcAllMeas_WSWD_Dists(thisInst, thisMet.metData.GetSimulatedTimeSeries(thisInst.modeledHeight));

            thisInst.updateThe.AllTABs(thisInst);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Load one MERRA node
            thisMet = thisInst.metList.metItem[0];
            thisInst.dateMERRAStart.Value = Convert.ToDateTime("1/1/2008 12:00:00 AM");
            thisInst.dateMERRAEnd.Value = Convert.ToDateTime("12/31/2009 11:00:00 PM");
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
            int offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
            string merraFolder = "C:\\Users\\OEE2017_27\\Dropbox (OEE)\\Due Diligence - Raw Data\\MERRA Data\\Ohio\\Ohio plus - tavg data";
            thisInst.merraList.MERRAfolder = merraFolder;
            
            thisInst.merraList.AddMERRA_GetDataFromTextFiles(theseLL.latitude, theseLL.longitude, offset, thisInst, thisMet, true);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            Assert.AreEqual(thisInst.merraList.merraData[0].MERRA_Nodes.Length, 1, "Didn't load one MERRA node");

            // Change to 4 MERRA nodes
            thisInst.cboNumMERRA_Nodes.SelectedIndex = 1;

            thisInst.merraList.AddMERRA_GetDataFromTextFiles(theseLL.latitude, theseLL.longitude, offset, thisInst, thisMet, true);
            
            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            Assert.AreEqual(thisInst.merraList.merraData[0].MERRA_Nodes.Length, 4, "Didn't load four MERRA nodes");
            thisInst.BW_worker.Close();

            // Change to 16 MERRA nodes
            thisInst.cboNumMERRA_Nodes.SelectedIndex = 2;
            thisInst.merraList.AddMERRA_GetDataFromTextFiles(theseLL.latitude, theseLL.longitude, offset, thisInst, thisMet, true);
            
            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            Assert.AreEqual(thisInst.merraList.merraData[0].MERRA_Nodes.Length, 16, "Didn't load 16 MERRA nodes");
            thisInst.BW_worker.Close();

            thisInst.Close();
        }
    }
}
