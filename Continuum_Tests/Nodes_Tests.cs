using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class Nodes_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;

        public Nodes_Tests()
        {
            testingFolder = globals.testingFolder + "Nodes";
        }

        [TestMethod]
        public void CalcGridStatsAndExposures_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\Findlay.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            Nodes newNode = new Nodes();
            newNode.gridStats = new Grid_Info();
            newNode.UTMX = 280000;
            newNode.UTMY = 4550000;

            Check_class check = new Check_class();
            int isOk = check.NewNodeCheck(thisInst.topo, newNode.UTMX, newNode.UTMY, 10000, "Calcs");
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
