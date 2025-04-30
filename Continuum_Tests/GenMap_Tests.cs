using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;

namespace Continuum_Tests
{
    [TestClass]
    public class GenMap_Tests
    { 
        Globals globals = new Globals();
        string testingFolder;

        public GenMap_Tests()
        {
            testingFolder = globals.testingFolder + "GenMap";
        }

        [TestMethod]
        public void UpdateLimits_Test()
        {
            Continuum thisInst = new Continuum("", false);
            
            string Filename = testingFolder + "\\GenMap testing.cfm";
            thisInst.Open(Filename);
            GenMap New_Map = new GenMap(thisInst);
            New_Map.gridReso = 5000;            
            New_Map.FindLargestArea();
            New_Map.GetBiggestArea();
            
            Assert.AreEqual(New_Map.minUTMX_Limit, 426087, 0, "Test 1: Wrong Min UTMX Limit");
            Assert.AreEqual(New_Map.minUTMY_Limit, 3995085, 0, "Test 1: Wrong Min UTMY Limit");
            Assert.AreEqual(New_Map.maxUTMX_Limit, 466087, 0, "Test 1: Wrong Max UTMX Limit");
            Assert.AreEqual(New_Map.maxUTMY_Limit, 4010085, 0, "Test 1: Wrong Max UTMY Limit");
            Assert.AreEqual(New_Map.numX, 9, 0, "Test 1: Wrong Num X");
            Assert.AreEqual(New_Map.numY, 4, 0, "Test 1: Wrong Num Y");
            Assert.AreEqual(New_Map.numGrid, 36, 0, "Test 1: Wrong Num Grid");

            New_Map.gridReso = 1000;           
            New_Map.FindLargestArea();
            New_Map.GetBiggestArea();
           
            Assert.AreEqual(New_Map.minUTMX_Limit, 424087, 0, "Test 2: Wrong Min UTMX Limit");
            Assert.AreEqual(New_Map.minUTMY_Limit, 3993085, 0, "Test 2: Wrong Min UTMY Limit");
            Assert.AreEqual(New_Map.maxUTMX_Limit, 469087, 0, "Test 2: Wrong Max UTMX Limit");
            Assert.AreEqual(New_Map.maxUTMY_Limit, 4014085, 0, "Test 2: Wrong Max UTMY Limit");
            Assert.AreEqual(New_Map.numX, 46, 0, "Test 2: Wrong Num X");
            Assert.AreEqual(New_Map.numY, 22, 0, "Test 2: Wrong Num Y");
            Assert.AreEqual(New_Map.numGrid, 1012, 0, "Test 2: Wrong Num Grid");

            thisInst.Close();
        }
    }
}
