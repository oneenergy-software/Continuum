using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class Nodes_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Nodes";

        [TestMethod]
        public void CalcGridStatsAndExposures_Test()
        {
            Continuum thisInst = new Continuum();
            

            string Filename = testingFolder + "\\RDM test.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            Nodes newNode = new Nodes();
            newNode.gridStats = new Grid_Info();
            newNode.UTMX = 344454;
            newNode.UTMY = 5312533;
            newNode.CalcGridStatsAndExposures(thisInst);

            Assert.AreNotSame(newNode.expo, null, "Didn't calculate exposures");
            Assert.AreNotSame(newNode.gridStats, null, "Didn't calculate grid stats");
            Assert.AreNotSame(newNode.expo[0].expo, null, "Didn't calculate exposures");
            Assert.AreNotSame(newNode.expo[0].SR, null, "Didn't calculate roughness");
            Assert.AreNotSame(newNode.expo[0].dispH, null, "Didn't calculate displacement height");
            Assert.AreNotSame(newNode.gridStats.stats, null, "Didn't calculate grid stats");

            thisInst.Close();
        }
    }
}
