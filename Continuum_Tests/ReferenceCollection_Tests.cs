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
        string testingFolder = "C:\\Users\\liz_w\\Dropbox\\Continuum 3 Source code\\Critical Unit Test Docs\\MERRACollection";
        string MERRA2Folder = "C:\\Users\\liz_w\\Desktop\\MERRA2";

        [TestMethod]
        public void GetInterpData_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\GetInterpData test.cfm";
            thisInst.Open(Filename);
            
            // Test site
            double thisLat = 41.23;
            double thisLong = -83.4;

            Met thisMet = new Met();

            ReferenceCollection.RefDataDownload refDataDown = thisInst.refList.ReadFileAndDefineRefDataDownload(MERRA2Folder);
            if (thisInst.refList.HaveThisRefDataDownload(refDataDown) == false)
                thisInst.refList.AddRefDataDownload(refDataDown);

            Reference newRef = new Reference();
            newRef.Set_Interp_LatLon_Dates_Offset(thisLat, thisLong, thisInst.UTM_conversions.GetUTC_Offset(thisLat, thisLong), thisInst.UTM_conversions);
            newRef.numNodes = 4;
            newRef.refDataDownload = refDataDown;
            thisInst.refList.AddReference(newRef);

            thisInst.refList.GetDataFromTextFiles(newRef, thisInst);                       

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false && thisInst.BW_worker.IsBusy()) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            Reference thisMERRA = thisInst.refList.GetAllRefsAtLatLong(thisLat, thisLong)[0];
           
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
            MERRACollection merraList = new MERRACollection();
            merraList.numMERRA_Nodes = 1;

            double thisLat = 41.09805;
            double thisLong = -83.6422;

            UTM_conversion.Lat_Long[] theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

            Assert.AreEqual(theseNodes[0].latitude, 41.0, 0, "Wrong lat Test 1");
            Assert.AreEqual(theseNodes[0].longitude, -83.75, 0, "Wrong long Test 1");

            merraList.numMERRA_Nodes = 4;
            theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

            Assert.AreEqual(theseNodes[0].latitude, 41.0, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[0].longitude, -83.75, 0, "Wrong long Test 2");
            Assert.AreEqual(theseNodes[1].latitude, 41.0, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[1].longitude, -83.125, 0, "Wrong long Test 2");
            Assert.AreEqual(theseNodes[2].latitude, 41.5, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[2].longitude, -83.75, 0, "Wrong long Test 2");
            Assert.AreEqual(theseNodes[3].latitude, 41.5, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[3].longitude, -83.125, 0, "Wrong long Test 2");

            merraList.numMERRA_Nodes = 16;
            theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

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
            thisLat = -35.279;
            thisLong = 149.118;

            merraList.numMERRA_Nodes = 1;
            theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

            Assert.AreEqual(theseNodes[0].latitude, -35.5, 0, "Wrong lat Test 4");
            Assert.AreEqual(theseNodes[0].longitude, 149.375, 0, "Wrong long Test 4");

            merraList.numMERRA_Nodes = 4;
            theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

            Assert.AreEqual(theseNodes[0].latitude, -35.5, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[0].longitude, 148.75, 0, "Wrong long Test 5");
            Assert.AreEqual(theseNodes[1].latitude, -35.5, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[1].longitude, 149.375, 0, "Wrong long Test 5");
            Assert.AreEqual(theseNodes[2].latitude, -35.0, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[2].longitude, 148.75, 0, "Wrong long Test 5");
            Assert.AreEqual(theseNodes[3].latitude, -35.0, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[3].longitude, 149.375, 0, "Wrong long Test 5");

            merraList.numMERRA_Nodes = 16;
            theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

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
