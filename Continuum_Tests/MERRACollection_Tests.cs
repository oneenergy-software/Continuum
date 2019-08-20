using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class MERRACollection_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\MERRACollection";
        
        [TestMethod]
        public void GetInterpData_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\GetInterpData\\GetInterpData test.cfm";
            thisInst.Open(Filename);

            // Test site
            double thisLat = 41.23;
            double thisLong = -83.4;
            Met thisMet = new Met();

            thisInst.merraList.numMERRA_Nodes = 4;
            thisInst.merraList.AddMERRA_GetDataFromTextFiles(thisLat, thisLong, -5, thisInst, thisMet);
            MERRA thisMERRA = thisInst.merraList.GetMERRA(thisLat, thisLong);

            Assert.AreEqual(thisMERRA.interpData.TS_Data[226].WS50m, 14.60535, 0.001, "Wrong interp WS Index 226");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[1000].WS50m, 6.364464, 0.001, "Wrong interp WS Index 1000");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[2475].WS50m, 6.745679, 0.001, "Wrong interp WS Index 2475");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[4444].WS50m, 4.426161, 0.001, "Wrong interp WS Index 4444");

            Assert.AreEqual(thisMERRA.interpData.TS_Data[226].WD50m, 220.7011, 0.01, "Wrong interp WD Index 226");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[1000].WD50m, 347.4748, 0.01, "Wrong interp WD Index 1000");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[2475].WD50m, 131.8697, 0.01, "Wrong interp WD Index 2475");
            Assert.AreEqual(thisMERRA.interpData.TS_Data[4444].WD50m, 44.31306, 0.01, "Wrong interp WD Index 4444");

            thisInst.Close();
        }

        [TestMethod]
        public void GetRequiredMERRACoords_Test()
        {
            MERRACollection merraList = new MERRACollection();
            merraList.numMERRA_Nodes = 1;

            double thisLat = 41.09805;
            double thisLong = -83.6422;

            MERRA.DecimalCoords[] theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

            Assert.AreEqual(theseNodes[0].Lat, 41.0, 0, "Wrong lat Test 1");
            Assert.AreEqual(theseNodes[0].Lon, -83.75, 0, "Wrong long Test 1");

            merraList.numMERRA_Nodes = 4;
            theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

            Assert.AreEqual(theseNodes[0].Lat, 41.0, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[0].Lon, -83.75, 0, "Wrong long Test 2");
            Assert.AreEqual(theseNodes[1].Lat, 41.0, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[1].Lon, -83.125, 0, "Wrong long Test 2");
            Assert.AreEqual(theseNodes[2].Lat, 41.5, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[2].Lon, -83.75, 0, "Wrong long Test 2");
            Assert.AreEqual(theseNodes[3].Lat, 41.5, 0, "Wrong lat Test 2");
            Assert.AreEqual(theseNodes[3].Lon, -83.125, 0, "Wrong long Test 2");

            merraList.numMERRA_Nodes = 16;
            theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

            Assert.AreEqual(theseNodes[0].Lat, 40.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[0].Lon, -84.375, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[1].Lat, 41.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[1].Lon, -84.375, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[2].Lat, 41.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[2].Lon, -84.375, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[3].Lat, 42.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[3].Lon, -84.375, 0, "Wrong long Test 3");

            Assert.AreEqual(theseNodes[4].Lat, 40.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[4].Lon, -83.75, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[5].Lat, 41.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[5].Lon, -83.75, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[6].Lat, 41.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[6].Lon, -83.75, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[7].Lat, 42.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[7].Lon, -83.75, 0, "Wrong long Test 3");

            Assert.AreEqual(theseNodes[8].Lat, 40.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[8].Lon, -83.125, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[9].Lat, 41.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[9].Lon, -83.125, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[10].Lat, 41.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[10].Lon, -83.125, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[11].Lat, 42.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[11].Lon, -83.125, 0, "Wrong long Test 3");

            Assert.AreEqual(theseNodes[12].Lat, 40.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[12].Lon, -82.5, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[13].Lat, 41.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[13].Lon, -82.5, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[14].Lat, 41.5, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[14].Lon, -82.5, 0, "Wrong long Test 3");
            Assert.AreEqual(theseNodes[15].Lat, 42.0, 0, "Wrong lat Test 3");
            Assert.AreEqual(theseNodes[15].Lon, -82.5, 0, "Wrong long Test 3");

            // Southern hemisphere
            thisLat = -35.279;
            thisLong = 149.118;

            merraList.numMERRA_Nodes = 1;
            theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

            Assert.AreEqual(theseNodes[0].Lat, -35.5, 0, "Wrong lat Test 4");
            Assert.AreEqual(theseNodes[0].Lon, 149.375, 0, "Wrong long Test 4");

            merraList.numMERRA_Nodes = 4;
            theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

            Assert.AreEqual(theseNodes[0].Lat, -35.5, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[0].Lon, 148.75, 0, "Wrong long Test 5");
            Assert.AreEqual(theseNodes[1].Lat, -35.5, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[1].Lon, 149.375, 0, "Wrong long Test 5");
            Assert.AreEqual(theseNodes[2].Lat, -35.0, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[2].Lon, 148.75, 0, "Wrong long Test 5");
            Assert.AreEqual(theseNodes[3].Lat, -35.0, 0, "Wrong lat Test 5");
            Assert.AreEqual(theseNodes[3].Lon, 149.375, 0, "Wrong long Test 5");

            merraList.numMERRA_Nodes = 16;
            theseNodes = merraList.GetRequiredMERRACoords(thisLat, thisLong);

            Assert.AreEqual(theseNodes[0].Lat, -36, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[0].Lon, 148.125, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[1].Lat, -35.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[1].Lon, 148.125, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[2].Lat, -35.0, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[2].Lon, 148.125, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[3].Lat, -34.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[3].Lon, 148.125, 0, "Wrong long Test 6");

            Assert.AreEqual(theseNodes[4].Lat, -36, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[4].Lon, 148.75, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[5].Lat, -35.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[5].Lon, 148.75, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[6].Lat, -35.0, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[6].Lon, 148.75, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[7].Lat, -34.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[7].Lon, 148.75, 0, "Wrong long Test 6");

            Assert.AreEqual(theseNodes[8].Lat, -36, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[8].Lon, 149.375, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[9].Lat, -35.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[9].Lon, 149.375, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[10].Lat, -35.0, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[10].Lon, 149.375, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[11].Lat, -34.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[11].Lon, 149.375, 0, "Wrong long Test 6");

            Assert.AreEqual(theseNodes[12].Lat, -36, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[12].Lon, 150, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[13].Lat, -35.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[13].Lon, 150, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[14].Lat, -35.0, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[14].Lon, 150, 0, "Wrong long Test 6");
            Assert.AreEqual(theseNodes[15].Lat, -34.5, 0, "Wrong lat Test 6");
            Assert.AreEqual(theseNodes[15].Lon, 150, 0, "Wrong long Test 6");
        }
    }
}
