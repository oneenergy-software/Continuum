using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;

namespace Continuum_Tests
{
    [TestClass]
    public class GenMap_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\GenMap";

        [TestMethod]
        public void UpdateLimits_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\Great Western TurbCalc Testing 20190201.cfm";
            thisInst.Open(Filename);
            GenMap New_Map = new GenMap(thisInst);
            New_Map.gridReso = 5000;
            New_Map.UpdateLimits();

            Assert.AreEqual(New_Map.minUTMX_Limit, 426087, 0, "Test 1: Wrong Min UTMX Limit");
            Assert.AreEqual(New_Map.minUTMY_Limit, 3995085, 0, "Test 1: Wrong Min UTMY Limit");
            Assert.AreEqual(New_Map.maxUTMX_Limit, 466087, 0, "Test 1: Wrong Max UTMX Limit");
            Assert.AreEqual(New_Map.maxUTMY_Limit, 4010085, 0, "Test 1: Wrong Max UTMY Limit");
            Assert.AreEqual(New_Map.numX, 9, 0, "Test 1: Wrong Num X");
            Assert.AreEqual(New_Map.numY, 4, 0, "Test 1: Wrong Num Y");
            Assert.AreEqual(New_Map.numGrid, 36, 0, "Test 1: Wrong Num Grid");

            New_Map.gridReso = 1000;
            New_Map.UpdateLimits();
            New_Map.GetBiggestArea();

            Assert.AreEqual(New_Map.minUTMX_Limit, 423087, 0, "Test 2: Wrong Min UTMX Limit");
            Assert.AreEqual(New_Map.minUTMY_Limit, 3992085, 0, "Test 2: Wrong Min UTMY Limit");
            Assert.AreEqual(New_Map.maxUTMX_Limit, 469087, 0, "Test 2: Wrong Max UTMX Limit");
            Assert.AreEqual(New_Map.maxUTMY_Limit, 4014085, 0, "Test 2: Wrong Max UTMY Limit");
            Assert.AreEqual(New_Map.numX, 47, 0, "Test 2: Wrong Num X");
            Assert.AreEqual(New_Map.numY, 23, 0, "Test 2: Wrong Num Y");
            Assert.AreEqual(New_Map.numGrid, 1081, 0, "Test 2: Wrong Num Grid");

            thisInst.Close();
        }
    }
}
