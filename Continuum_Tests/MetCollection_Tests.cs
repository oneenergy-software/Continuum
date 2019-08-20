using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class MetCollection_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\MetCollection";

        [TestMethod]
        public void CalcWS_DistForTurbOrMap_Test()
        {
            Continuum thisInst = new Continuum();
            

            string Filename = testingFolder + "\\RDM test.cfm";
            thisInst.Open(Filename);

            double[] thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 7.5f, 24, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

            Assert.AreEqual(thisDist[0], 0.024216, 0.001, "Wrong WS distribution in Test 1");
            Assert.AreEqual(thisDist[9], 0.093039, 0.001, "Wrong WS distribution in Test 1");

            thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 7.0f, 24, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

            Assert.AreEqual(thisDist[0], 0.029410, 0.001, "Wrong WS distribution in Test 2");
            Assert.AreEqual(thisDist[6], 0.125111, 0.001, "Wrong WS distribution in Test 2");
            Assert.AreEqual(thisDist[10], 0.069316, 0.001, "Wrong WS distribution in Test 2");

            thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 9.4f, 24, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

            Assert.AreEqual(thisDist[3], 0.036817, 0.001, "Wrong WS distribution in Test 3");
            Assert.AreEqual(thisDist[8], 0.068238, 0.001, "Wrong WS distribution in Test 3");
            Assert.AreEqual(thisDist[12], 0.071463, 0.001, "Wrong WS distribution in Test 3");

            thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 5f, 3, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

            Assert.AreEqual(thisDist[2], 0.09668, 0.001, "Wrong WS distribution in Test 4");
            Assert.AreEqual(thisDist[4], 0.158107, 0.001, "Wrong WS distribution in Test 4");
            Assert.AreEqual(thisDist[14], 0.0065201, 0.001, "Wrong WS distribution in Test 4");

            thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 4.2f, 3, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

            Assert.AreEqual(thisDist[1], 0.0682394, 0.001, "Wrong WS distribution in Test 5");
            Assert.AreEqual(thisDist[6], 0.1050160, 0.001, "Wrong WS distribution in Test 5");
            Assert.AreEqual(thisDist[12], 0.0036309, 0.001, "Wrong WS distribution in Test 5");

            thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 5.3f, 3, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

            Assert.AreEqual(thisDist[0], 0.0430541, 0.001, "Wrong WS distribution in Test 6");
            Assert.AreEqual(thisDist[4], 0.135551, 0.001, "Wrong WS distribution in Test 6");
            Assert.AreEqual(thisDist[10], 0.0240220, 0.001, "Wrong WS distribution in Test 6");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcMetExposures_Test()
        {
            Continuum thisInst = new Continuum();
            

            string Filename = testingFolder + "\\RDM test.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
            thisInst.metList.ClearAllExposuresAndGridStats();

            for (int i = 0; i < thisInst.metList.ThisCount; i++)
                for (int j = 0; j < thisInst.radiiList.ThisCount; j++)
                    thisInst.metList.CalcMetExposures(i, j, thisInst);

            for (int i = 0; i < thisInst.metList.ThisCount; i++)
            {
                Assert.AreNotSame(thisInst.metList.metItem[i].expo, null, "Didn't calculate exposure");
                Assert.AreNotSame(thisInst.metList.metItem[i].expo[0].expo, null, "Didn't calculate exposure");
                Assert.AreNotSame(thisInst.metList.metItem[i].expo[0].SR, null, "Didn't calculate exposure");
                Assert.AreNotSame(thisInst.metList.metItem[i].expo[0].dispH, null, "Didn't calculate exposure");
            }

            thisInst.Close();

        }

        [TestMethod]
        public void AddMetTAB_Test()
        {
            string WR_file = testingFolder + "\\Wind_Rose.txt";
            StreamReader sr = new StreamReader(WR_file);
            double[] windRose = new double[16];
            for (int i = 0; i <= 15; i++)
                windRose[i] = Convert.ToSingle(sr.ReadLine()) / 100;

            string Sect_WS_file = testingFolder + "\\Sect_WS.txt";
            sr = new StreamReader(Sect_WS_file);
            double[,] sectorWS = new double[16, 31];
            for (int i = 0; i <= 15; i++)
            {
                string This_WS_Array = sr.ReadLine();
                string[] This_Array_Split = This_WS_Array.Split('\t');
                for (int j = 0; j <= 30; j++)
                    sectorWS[i, j] = Convert.ToSingle(This_Array_Split[j]);
            }

            Continuum ThisNewInst = new Continuum();
            ThisNewInst.metList.AddMetTAB("Test_Add", 10000, 100000, 80, windRose, sectorWS, 0.5f, 1, ThisNewInst);

            Assert.AreEqual(ThisNewInst.metList.ThisCount, 1, 0, "Wrong met count");
            Met.WSWD_Dist thisDist = ThisNewInst.metList.metItem[0].GetWS_WD_Dist(ThisNewInst.modeledHeight, Met.TOD.All, Met.Season.All);
            Assert.AreNotSame(thisDist.sectorWS_Dist, null, "Didn't read in sectorwise WS distribution");
            Assert.AreNotSame(thisDist.windRose, null, "Didn't read in wind rose");
            Assert.AreNotSame(thisDist.sectorWS_Ratio, null, "Didn't calculate directional WS ratios");
            Assert.AreNotEqual(thisDist.height, 0, 0, "Didn't read in height");

            ThisNewInst.Close();

        }

    }
}
