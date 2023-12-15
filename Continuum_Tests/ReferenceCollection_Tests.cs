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

        public ReferenceCollection_Tests()
        {
            testingFolder = globals.testingFolder + "ReferenceCollection";
            MERRA2Folder = globals.merraFolder;
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
    }
}
