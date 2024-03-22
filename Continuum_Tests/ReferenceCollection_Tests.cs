using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;
using System.Threading;

namespace Continuum_Tests
{
    [TestClass]
    public class ReferenceCollection_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;
        string MERRA2Folder;
        string ERA5Folder;

        public ReferenceCollection_Tests()
        {
            testingFolder = globals.testingFolder + "ReferenceCollection";
            MERRA2Folder = globals.merraFolder;
            ERA5Folder = globals.era5Folder;
        }

        [TestMethod]
        public void GetInterpData_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\GetInterpData test.cfm";
            thisInst.Open(Filename);
       //     thisInst.merraList.MERRAfolder = MERRA2Folder;

            // Test site
            double thisLat = 41.23;
            double thisLong = -83.4;
            Met thisMet = new Met();
                        
            Reference thisRef = new Reference();
            thisRef.numNodes = 4;
            thisRef.refDataDownload = thisInst.refList.refDataDownloads[0];
            thisRef.Set_Interp_LatLon_Dates_Offset(thisLat, thisLong, thisInst.UTM_conversions.GetUTC_Offset(thisLat, thisLong), thisInst.UTM_conversions);
            thisRef.nodes = new Reference.Node_Data[thisRef.numNodes];
            thisRef.FindCoords(thisInst.refList);
            thisRef.GetReferenceDataFromDB(thisInst);
            thisInst.refList.GetDataFromTextFiles(thisRef, thisInst);
            thisRef.GetInterpData(thisInst.UTM_conversions);

            thisInst.refList.AddReference(thisRef);

            //      thisInst.merraList.AddMERRA_GetDataFromTextFiles(thisLat, thisLong, -5, thisInst, thisMet, true);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false && thisInst.BW_worker.IsBusy()) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            Reference thisMERRA = thisInst.refList.GetAllRefsAtLatLong(thisLat, thisLong)[1];
           

            Assert.AreEqual(thisMERRA.interpData.TS_Data[226].WS, 14.60535, 0.001, "Wrong interp WS Index 226");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[1000].WS, 6.364464, 0.001, "Wrong interp WS Index 1000");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[2475].WS, 6.745679, 0.001, "Wrong interp WS Index 2475");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[4444].WS, 4.426161, 0.001, "Wrong interp WS Index 4444");

            Assert.AreEqual(thisMERRA.interpData.TS_Data[226].WD, 220.7011, 0.01, "Wrong interp WD Index 226");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[1000].WD, 347.4748, 0.01, "Wrong interp WD Index 1000");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[2475].WD, 131.8697, 0.01, "Wrong interp WD Index 2475");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[4444].WD, 44.31306, 0.01, "Wrong interp WD Index 4444");

            thisInst.Close();
        }

        [TestMethod]
        public void GetRequiredMERRACoords_Test()
        {
            ReferenceCollection refList = new ReferenceCollection();
            ReferenceCollection.RefDataDownload refDataDown = new ReferenceCollection.RefDataDownload();
            refDataDown.refType = "MERRA2";
            refList.AddRefDataDownload(refDataDown);

            Reference thisRef = new Reference();
            thisRef.refDataDownload = refDataDown;
            thisRef.numNodes = 1;

            double thisLat = 41.09805;
            double thisLong = -83.6422;
            UTM_conversion utmConvs = new UTM_conversion();
            thisRef.Set_Interp_LatLon_Dates_Offset(thisLat, thisLong, utmConvs.GetUTC_Offset(thisLat, thisLong), utmConvs);

            UTM_conversion.Lat_Long[] theseNodes = thisRef.GetRequiredCoords(refList);

            Assert.AreEqual(theseNodes[0].latitude, 41.0, 0, "Wrong lat Test 1");
            Assert.AreEqual(theseNodes[0].longitude, -83.75, 0, "Wrong long Test 1");

            thisRef.numNodes = 4;
            theseNodes = thisRef.GetRequiredCoords(refList);

            Assert.AreEqual(theseNodes[0].latitude, 41.0, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[0].longitude, -83.75, 0, "Wrong long Test 2");
            Assert.AreEqual(theseNodes[1].latitude, 41.0, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[1].longitude, -83.125, 0, "Wrong long Test 2");
            Assert.AreEqual(theseNodes[2].latitude, 41.5, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[2].longitude, -83.75, 0, "Wrong long Test 2");
            Assert.AreEqual(theseNodes[3].latitude, 41.5, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[3].longitude, -83.125, 0, "Wrong long Test 2");

            thisRef.numNodes = 16;
            theseNodes = thisRef.GetRequiredCoords(refList);

            Assert.AreEqual(theseNodes[0].latitude, 40.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[0].longitude, -84.375, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[1].latitude, 41.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[1].longitude, -84.375, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[2].latitude, 41.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[2].longitude, -84.375, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[3].latitude, 42.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[3].longitude, -84.375, 0, "Wrong long Test 3");

            Assert.AreEqual(theseNodes[4].latitude, 40.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[4].longitude, -83.75, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[5].latitude, 41.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[5].longitude, -83.75, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[6].latitude, 41.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[6].longitude, -83.75, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[7].latitude, 42.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[7].longitude, -83.75, 0, "Wrong long Test 3");

            Assert.AreEqual(theseNodes[8].latitude, 40.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[8].longitude, -83.125, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[9].latitude, 41.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[9].longitude, -83.125, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[10].latitude, 41.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[10].longitude, -83.125, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[11].latitude, 42.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[11].longitude, -83.125, 0, "Wrong long Test 3");

            Assert.AreEqual(theseNodes[12].latitude, 40.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[12].longitude, -82.5, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[13].latitude, 41.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[13].longitude, -82.5, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[14].latitude, 41.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[14].longitude, -82.5, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[15].latitude, 42.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[15].longitude, -82.5, 0, "Wrong long Test 3");

            // Southern hemisphere
            thisRef.Set_Interp_LatLon_Dates_Offset(-35.279, 149.118, utmConvs.GetUTC_Offset(-35.279, 149.118), utmConvs);

            thisRef.numNodes = 1;
            theseNodes = thisRef.GetRequiredCoords(refList);

            Assert.AreEqual(theseNodes[0].latitude, -35.5, 0, "Wrong lat Test 4");
            Assert.AreEqual(theseNodes[0].longitude, 149.375, 0, "Wrong long Test 4");

            thisRef.numNodes = 4;
            theseNodes = thisRef.GetRequiredCoords(refList);

            Assert.AreEqual(theseNodes[0].latitude, -35.5, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[0].longitude, 148.75, 0, "Wrong long Test 5");
            Assert.AreEqual(theseNodes[1].latitude, -35.5, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[1].longitude, 149.375, 0, "Wrong long Test 5");
            Assert.AreEqual(theseNodes[2].latitude, -35.0, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[2].longitude, 148.75, 0, "Wrong long Test 5");
            Assert.AreEqual(theseNodes[3].latitude, -35.0, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[3].longitude, 149.375, 0, "Wrong long Test 5");

            thisRef.numNodes = 16;
            theseNodes = thisRef.GetRequiredCoords(refList);

            Assert.AreEqual(theseNodes[0].latitude, -36, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[0].longitude, 148.125, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[1].latitude, -35.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[1].longitude, 148.125, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[2].latitude, -35.0, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[2].longitude, 148.125, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[3].latitude, -34.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[3].longitude, 148.125, 0, "Wrong long Test 6");

            Assert.AreEqual(theseNodes[4].latitude, -36, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[4].longitude, 148.75, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[5].latitude, -35.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[5].longitude, 148.75, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[6].latitude, -35.0, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[6].longitude, 148.75, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[7].latitude, -34.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[7].longitude, 148.75, 0, "Wrong long Test 6");

            Assert.AreEqual(theseNodes[8].latitude, -36, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[8].longitude, 149.375, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[9].latitude, -35.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[9].longitude, 149.375, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[10].latitude, -35.0, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[10].longitude, 149.375, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[11].latitude, -34.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[11].longitude, 149.375, 0, "Wrong long Test 6");

            Assert.AreEqual(theseNodes[12].latitude, -36, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[12].longitude, 150, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[13].latitude, -35.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[13].longitude, 150, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[14].latitude, -35.0, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[14].longitude, 150, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[15].latitude, -34.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[15].longitude, 150, 0, "Wrong long Test 6");
        }

        [TestMethod]
        public void GetDataFromTextFiles_Test()
        {
            string cfmName = testingFolder + "\\Reference Textfiles\\Reference Textfile Test";

            if (File.Exists(cfmName + ".cfm"))
                File.Delete(cfmName + ".cfm");

            if (File.Exists(cfmName + ".mdf"))
                File.Delete(cfmName + ".mdf");

            if (File.Exists(cfmName + "_log.ldf"))
                File.Delete(cfmName + "_log.ldf");

            cfmName = cfmName + ".cfm";

            Continuum thisInst = new Continuum("");
            thisInst.savedParams.savedFileName = cfmName;
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 17;
            thisInst.UTM_conversions.hemisphere = "Northern";

            thisInst.modeledHeight = 80;
            thisInst.SaveFile();

            // Test MERRA2 file reading
            ReferenceCollection.RefDataDownload refDataDown = new ReferenceCollection.RefDataDownload();
            refDataDown.refType = "MERRA2";
            refDataDown.folderLocation = testingFolder + "\\Reference Textfiles\\MERRA2";
            refDataDown = thisInst.refList.ReadFileAndDefineRefDataDownload(refDataDown.folderLocation);
            thisInst.refList.AddRefDataDownload(refDataDown);

            Reference thisRef = new Reference();
            thisRef.refDataDownload = refDataDown;
            thisRef.numNodes = 1;

            double thisLat = 39.5;
            double thisLong = -84.375;

            int offset = thisInst.UTM_conversions.GetUTC_Offset(thisLat, thisLong);
            thisRef.Set_Interp_LatLon_Dates_Offset(thisLat, thisLong, offset, thisInst.UTM_conversions);
            thisRef.nodes = new Reference.Node_Data[thisRef.numNodes];
            thisRef.startDate = new DateTime(2017, 12, 31, 19, 0, 0);
            thisRef.endDate = new DateTime(2018, 1, 1, 18, 0, 0);
            bool gotCoords = thisRef.FindCoords(thisInst.refList);

            thisInst.refList.GetDataFromTextFiles(thisRef, thisInst);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Assert.AreEqual(thisRef.interpData.TS_Data[0].WS, 5.333604607, 0.001);
            Assert.AreEqual(thisRef.interpData.TS_Data[9].WS, 5.616514144, 0.001);
            Assert.AreEqual(thisRef.interpData.TS_Data[22].WS, 6.315440694, 0.001);

            // Test ERA5 file reading
            refDataDown = new ReferenceCollection.RefDataDownload();
            refDataDown.refType = "ERA5";
            refDataDown.folderLocation = testingFolder + "\\Reference Textfiles\\ERA5";
            refDataDown = thisInst.refList.ReadFileAndDefineRefDataDownload(refDataDown.folderLocation);
            thisInst.refList.AddRefDataDownload(refDataDown);

            thisRef = new Reference();
            thisRef.refDataDownload = refDataDown;
            thisRef.numNodes = 1;

            thisLat = 41.25;
            thisLong = -83;

            offset = thisInst.UTM_conversions.GetUTC_Offset(thisLat, thisLong);
            thisRef.Set_Interp_LatLon_Dates_Offset(thisLat, thisLong, offset, thisInst.UTM_conversions);
            thisRef.nodes = new Reference.Node_Data[thisRef.numNodes];
            thisRef.startDate = new DateTime(2002, 3, 31, 19, 0, 0);
            thisRef.endDate = new DateTime(2002, 4, 1, 18, 0, 0);
            gotCoords = thisRef.FindCoords(thisInst.refList);

            thisInst.refList.GetDataFromTextFiles(thisRef, thisInst);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Assert.AreEqual(thisRef.interpData.TS_Data[0].WS, 0.760626764, 0.001);
            Assert.AreEqual(thisRef.interpData.TS_Data[9].WS, 7.018839979, 0.001);
            Assert.AreEqual(thisRef.interpData.TS_Data[22].WS, 2.659684626, 0.001);

            thisInst.Close();
        }

        

        [TestMethod]
        public void CalcAvgWindRose_Test()
        {
            Continuum thisInst = new Continuum("");

            string Filename = testingFolder + "\\CalcAvgWindRose\\CalcAvgWindRose file.cfm";
            thisInst.Open(Filename);

            double[] avgRose = thisInst.refList.CalcAvgWindRose(thisInst.UTM_conversions, 16);

            // Compare average wind rose to the values calculated in Excel

            Assert.AreEqual(avgRose[0], 0.042547415, 0.001);
            Assert.AreEqual(avgRose[3], 0.049527886, 0.001);
            Assert.AreEqual(avgRose[9], 0.103742867, 0.001);
            Assert.AreEqual(avgRose[15], 0.050240757, 0.001);

            avgRose = thisInst.refList.CalcAvgWindRose(thisInst.UTM_conversions, 12);

            Assert.AreEqual(avgRose[0], 0.057100056, 0.001);
            Assert.AreEqual(avgRose[3], 0.048960511, 0.001);
            Assert.AreEqual(avgRose[9], 0.117506955, 0.001);
            
            thisInst.Close();
        }

        [TestMethod]
        public void ReadFileAndDefineRefDataDownload_Test()
        {
            ReferenceCollection refList = new ReferenceCollection();

            ReferenceCollection.RefDataDownload merraData = refList.ReadFileAndDefineRefDataDownload(MERRA2Folder);
            ReferenceCollection.RefDataDownload eraData = refList.ReadFileAndDefineRefDataDownload(ERA5Folder);

            Assert.AreSame(merraData.refType, "MERRA2");
            Assert.AreEqual(merraData.minLat, 39, 0);
            Assert.AreEqual(merraData.maxLat, 41.5, 0);
            Assert.AreEqual(merraData.minLon, -85, 0);
            Assert.AreEqual(merraData.maxLon, -80.625, 0);
            
            Assert.AreSame(eraData.refType, "ERA5");
            Assert.AreEqual(eraData.minLat, 39, 0);
            Assert.AreEqual(eraData.maxLat, 41.5, 0);
            Assert.AreEqual(eraData.minLon, -85, 0);
            Assert.AreEqual(eraData.maxLon, -80, 0);
            
        }

        [TestMethod]
        public void GetDataFileStartEndDate_Test()
        {
            ReferenceCollection refList = new ReferenceCollection();
            ReferenceCollection.DateRangeAndCompletion datesAndComplete = refList.GetDataFileStartEndDateAndCompletion(MERRA2Folder, "MERRA2");
            
            Assert.AreEqual(datesAndComplete.startEnd[0], new DateTime(1988, 1, 1));
            Assert.AreEqual(datesAndComplete.startEnd[1], new DateTime(2019, 1, 31, 23, 0, 0));

            datesAndComplete = refList.GetDataFileStartEndDateAndCompletion(ERA5Folder, "ERA5");

            Assert.AreEqual(datesAndComplete.startEnd[0], new DateTime(2002, 1, 1));
            Assert.AreEqual(datesAndComplete.startEnd[1], new DateTime(2003, 1, 2, 23, 0, 0));

        }

        
    }
}
