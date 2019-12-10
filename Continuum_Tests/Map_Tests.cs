using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class Map_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Map";

        [TestMethod]
        public void CalcMapExposures_Test()
        {
            Continuum thisInst = new Continuum();
            
            NodeCollection nodeList = new NodeCollection();

            string Filename = testingFolder + "Map testing.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            Map New_Map = new Map();
            Map.MapNode New_Map_Node = new Map.MapNode();
            Met[] metsUsed = thisInst.metList.GetMets(thisInst.metList.GetMetsUsed(), null);
            Model[] Models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);

            thisInst.mapList.AddMap("New_Map", 470000, 5105000, 250, 30, 30, 0, "None", thisInst, false, null, metsUsed, Models, false);
            thisInst.BW_worker.Close();
            New_Map_Node.UTMX = 475000;
            New_Map_Node.UTMY = 5110000;
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, thisInst.mapList.mapItem[0], false);

            Assert.AreSame(New_Map_Node.expo, null, "expo are not null");
            New_Map.CalcMapExposures(ref New_Map_Node, 1, thisInst);

            Assert.AreNotSame(New_Map_Node.expo, null, "Didn't calculate exposures");
            thisInst.Close();
        }
    }
}
