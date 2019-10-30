using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class Check_class_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Check_class";
        
        [TestMethod]
        public void TopoCheck_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\TopoCheck model.cfm";
            thisInst.Open(Filename);

            Check_class check = new Check_class();
            string allOrPlot = "Plot";

            bool isOk = check.TopoCheck(thisInst.topo, 850000, 4500000, "Test 1");
            Assert.AreEqual(isOk, true, "Wrong TopoCheck Test 1");

            isOk = check.TopoCheck(thisInst.topo, 684480, 4663890, "Test 2");
            Assert.AreEqual(isOk, false, "Wrong TopoCheck Test 2");

            isOk = check.TopoCheck(thisInst.topo, 1053800, 4350000, "Test 3");
            Assert.AreEqual(isOk, false, "Wrong TopoCheck Test 3");

            isOk = check.TopoCheck(thisInst.topo, 688300, 4335000, "Test 4");
            Assert.AreEqual(isOk, true, "Wrong TopoCheck Test 4");

            isOk = check.TopoCheck(thisInst.topo, 1034000, 4677000, "Test 5");
            Assert.AreEqual(isOk, false, "Wrong TopoCheck Test 5");

            thisInst.Close();

        }

        [TestMethod]
        public void LandCoverCheck_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\TopoCheck model.cfm";
            thisInst.Open(Filename);

            Check_class check = new Check_class();
            string allOrPlot = "Plot";

            bool isOk = check.LandCoverCheck(thisInst.topo, 682000, 4709000, "Test 1");
            Assert.AreEqual(isOk, true, "Wrong TopoCheck Test 1");

            isOk = check.LandCoverCheck(thisInst.topo, 682000, 4716600, "Test 2");
            Assert.AreEqual(isOk, false, "Wrong TopoCheck Test 2");

            isOk = check.LandCoverCheck(thisInst.topo, 1077700, 4674500, "Test 3");
            Assert.AreEqual(isOk, true, "Wrong TopoCheck Test 3");

            isOk = check.LandCoverCheck(thisInst.topo, 1083650, 4674000, "Test 4");
            Assert.AreEqual(isOk, false, "Wrong TopoCheck Test 4");

            thisInst.Close();

        }
    }
}
