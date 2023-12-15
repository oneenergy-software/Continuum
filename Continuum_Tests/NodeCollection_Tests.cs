using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Continuum_Tests
{
    [TestClass]
    public class NodeCollection_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;

        public NodeCollection_Tests()
        {
            testingFolder = globals.testingFolder + "NodeCollection";
        }

        [TestMethod]        public void CreateTestFilesForFindHighNodes()
        {            
            Continuum thisInst = new Continuum("");
            string Filename = testingFolder + "\\Ashley.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            string exportFile = testingFolder + "\\Find_Node_in_High_Spot\\Test 1 Nodes.csv";
            Nodes startNode = new Nodes();
            startNode.UTMX = 469972;
            startNode.UTMY = 5113693;
            startNode.elev = thisInst.topo.CalcElevs(startNode.UTMX, startNode.UTMY);
            startNode.gridStats = new Grid_Info();                      

            int reso = 250;
            double minDir = 56.16f;
            double maxDir = 86.16f;
            int maxRadius = 2822;
            int minRadius = 1975;

            ExportNodesInSector(thisInst, startNode, reso, minDir, maxDir, minRadius, maxRadius, exportFile);

            // Test 2
            exportFile = testingFolder + "\\Find_Node_in_High_Spot\\Test 2 Nodes.csv";            
            reso = 250;
            minDir = 116.5f;
            maxDir = 146.16f;
            minRadius = 987;
            maxRadius = 1411;
            
            ExportNodesInSector(thisInst, startNode, reso, minDir, maxDir, minRadius, maxRadius, exportFile);
                        
        }


        public void ExportNodesInSector(Continuum thisInst, Nodes startNode, int reso, double minDir, double maxDir, int minRadius, int maxRadius, string exportFile)
        {            

            NodeCollection nodeList = new NodeCollection();                                  
            Nodes thisNode = new Nodes();
            Check_class check = new Check_class();            
            
            StreamWriter sw = new StreamWriter(exportFile);                

            // Adjust maxDir if it is less than minDir (i.e. if it crosses over 0 degs)
            if (maxDir < minDir) maxDir = maxDir + 360;

            // Do polar coordinate sweep and find node at each angle and radius. Find node with highest elevation            
            for (double i = minDir; i <= maxDir; i++)
            {
                for (double j = minRadius; j <= maxRadius; j = j + reso)
                {
                    thisNode.UTMX = startNode.UTMX + j * Math.Cos((90 - i) * Math.PI / 180);
                    thisNode.UTMY = startNode.UTMY + j * Math.Sin((90 - i) * Math.PI / 180);

                    // Find closest nodes on fixed grid
                    TopoInfo.TopoGrid closestNode = thisInst.topo.GetClosestNodeFixedGrid(thisNode.UTMX, thisNode.UTMY, 250, 12000);
                    thisNode.UTMX = closestNode.UTMX;
                    thisNode.UTMY = closestNode.UTMY;

                    int newOk = check.NewNodeCheck(thisInst.topo, thisNode.UTMX, thisNode.UTMY, 10000, "Calcs");

                    if (newOk == 100)
                    {
                        thisNode.elev = thisInst.topo.CalcElevs(thisNode.UTMX, thisNode.UTMY);                                               
                        sw.WriteLine(thisNode.UTMX + "," + thisNode.UTMY + "," + thisNode.elev);                        
                    }
                }
            }

            sw.Close();
            
        }

        [TestMethod]
        public void FindNodeInSectorHighSpot_Test()
        {
            Continuum thisInst = new Continuum("");
            

            string Filename = testingFolder + "\\Ashley.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            NodeCollection nodeList = new NodeCollection();
            Nodes startNode = new Nodes();
            startNode.UTMX = 469972;
            startNode.UTMY = 5113693;
            startNode.elev = thisInst.topo.CalcElevs(startNode.UTMX, startNode.UTMY);
            startNode.gridStats = new Grid_Info();
            
            Nodes highNode = nodeList.FindNodeInSectorHighSpot(thisInst, startNode, 56.16f, 86.16f, 2822, 1975);

            Assert.AreEqual(highNode.UTMX, 471653, 1, "Wrong UTMX in Test 1");
            Assert.AreEqual(highNode.UTMY, 5114688, 1, "Wrong UTMY in Test 1");
            Assert.AreEqual(highNode.elev, 656.017, 0.1, "Wrong elevation in Test 1");

            highNode = nodeList.FindNodeInSectorHighSpot(thisInst, startNode, 116.5f, 146.16f, 1411, 987);

            Assert.AreEqual(highNode.UTMX, 470903, 1, "Wrong UTMX in Test 2");
            Assert.AreEqual(highNode.UTMY, 5112688, 1, "Wrong UTMY in Test 2");
            Assert.AreEqual(highNode.elev, 643.89, 1, "Wrong elevation in Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void FindPathOfNodes_Test()
        {
            Continuum thisInst = new Continuum("");
            
            NodeCollection nodeList = new NodeCollection();

            string Filename = testingFolder + "\\Ashley.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Clear met pairs nad models
            thisInst.metPairList.ClearAll();
            thisInst.metPairList.CreateMetPairs(thisInst);
            thisInst.modelList.ClearAll();
            thisInst.modelList.FindSiteCalibratedModels(thisInst, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

            double[] windRose = new double[16];

            // Find path of nodes in between mets for R = 4000
            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
            {
                windRose = new double[16];
                Met.WSWD_Dist thisDist1 = thisInst.metPairList.metPairs[i].met1.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                Met.WSWD_Dist thisDist2 = thisInst.metPairList.metPairs[i].met2.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                for (int WD = 0; WD < 16; WD++)
                    windRose[WD] = (thisDist1.windRose[WD] + thisDist2.windRose[WD]) / 2;

                Nodes Met1_Node = nodeList.GetMetNode(thisInst.metPairList.metPairs[i].met1);
                Nodes Met2_Node = nodeList.GetMetNode(thisInst.metPairList.metPairs[i].met2);

                Nodes[] pathOfNodes = nodeList.FindPathOfNodes(Met1_Node, Met2_Node, thisInst.metPairList.metPairs[i].WS_Pred[0, 0].model, thisInst);
                thisInst.metPairList.metPairs[i].WS_Pred[0, 0].nodePath = pathOfNodes;
            }

            Assert.AreEqual(thisInst.metPairList.metPairs[4].WS_Pred[0, 0].nodePath.Length, 1, "Didn't find node between Met 2 and Met 3");

            // Confirm that Met 2 and Met 3 have terrain exposure that is outside acceptable range
            Nodes Met_2_Node = nodeList.GetMetNode(thisInst.metList.metItem[1]);
            Nodes Met_3_Node = nodeList.GetMetNode(thisInst.metList.metItem[2]);
            windRose = thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

            bool isTerrainSame = nodeList.TerrainSame(Met_2_Node, Met_3_Node, thisInst.modelList.models[0, 0], 1, windRose, 0);
            Assert.AreEqual(isTerrainSame, false, "Didn't find terrain different between Met 2 and Met 3");

            // Confirm that Met 1 and Met 2 don't need a node in between
            Assert.AreEqual(thisInst.metPairList.metPairs[0].WS_Pred[0, 0].nodePath.Length, 0, "Found node between Met 1 and Met 2");

            thisInst.Close();
        }

        [TestMethod]
        public void ReCalcNodeSRDH_Test()
        {

            int numWD = 16;
            string DB_Load_LC_Before_Met = testingFolder + "\\Test_Load_LC_Before_Met.cfm";
            string DB_Load_LC_After_Met = testingFolder + "\\Test_Load_LC_After_Met.cfm";

            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connBefore = nodeList.GetDB_ConnectionString(DB_Load_LC_Before_Met);
            string connAfter = nodeList.GetDB_ConnectionString(DB_Load_LC_After_Met);

            Nodes[] LC_Load_Before_Met = new Nodes[0]; // array of Nodes with SRDH calcs from model where Land Cover was loaded before the met import and map generation
            Nodes[] LC_Load_After_Met = new Nodes[0]; // same as above but from model where Land Cover was loaded after the met import and map generation

            // Grab all Nodes with SRDH calcs from 'LC Load Before' model
            using (var context = new Continuum_EDMContainer(connBefore))
            {
                var node_db = from N in context.Node_table.Include("expo") select N;

                foreach (var N in node_db)
                {
                    int numRadii = N.expo.Count;

                    for (int i = 0; i < numRadii; i++)
                    {
                        if (N.expo.ElementAt(i).SR_Array != null)
                        {
                            int numNodes = LC_Load_Before_Met.Length;
                            Array.Resize(ref LC_Load_Before_Met, numNodes + 1);
                            LC_Load_Before_Met[numNodes] = new Nodes();
                            LC_Load_Before_Met[numNodes].UTMX = N.UTMX;
                            LC_Load_Before_Met[numNodes].UTMY = N.UTMY;
                            LC_Load_Before_Met[numNodes].elev = N.elev;

                            MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).SR_Array);
                            LC_Load_Before_Met[numNodes].AddExposure(N.expo.ElementAt(i).radius, N.expo.ElementAt(i).exponent, 1, numWD);
                            LC_Load_Before_Met[numNodes].expo[0].SR = new double[numWD];
                            LC_Load_Before_Met[numNodes].expo[0].SR = (double[])bin.Deserialize(MS3);

                            MS3 = new MemoryStream(N.expo.ElementAt(i).DH_Array);
                            LC_Load_Before_Met[numNodes].expo[0].dispH = new double[numWD];
                            LC_Load_Before_Met[numNodes].expo[0].dispH = (double[])bin.Deserialize(MS3);
                        }
                    }
                }
            }

            // Grab all Nodes with SRDH calcs from 'LC Load After' model
            using (var context = new Continuum_EDMContainer(connAfter))
            {
                var node_db = from N in context.Node_table.Include("expo") select N;

                foreach (var N in node_db)
                {
                    int numRadii = N.expo.Count;

                    for (int i = 0; i < numRadii; i++)
                    {
                        if (N.expo.ElementAt(i).SR_Array != null)
                        {
                            int numNodes = LC_Load_After_Met.Length;
                            Array.Resize(ref LC_Load_After_Met, numNodes + 1);
                            LC_Load_After_Met[numNodes] = new Nodes();
                            LC_Load_After_Met[numNodes].UTMX = N.UTMX;
                            LC_Load_After_Met[numNodes].UTMY = N.UTMY;
                            LC_Load_After_Met[numNodes].elev = N.elev;

                            MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).SR_Array);
                            LC_Load_After_Met[numNodes].AddExposure(N.expo.ElementAt(i).radius, N.expo.ElementAt(i).exponent, 1, numWD);
                            LC_Load_After_Met[numNodes].expo[0].SR = new double[numWD];
                            LC_Load_After_Met[numNodes].expo[0].SR = (double[])bin.Deserialize(MS3);

                            MS3 = new MemoryStream(N.expo.ElementAt(i).DH_Array);
                            LC_Load_After_Met[numNodes].expo[0].dispH = new double[numWD];
                            LC_Load_After_Met[numNodes].expo[0].dispH = (double[])bin.Deserialize(MS3);
                        }
                    }
                }
            }

            // Loop through nodes LC_Load_Before_Met and find same coords in LC_Load_After_Met and compare SR/DH
            for (int i = 0; i < LC_Load_Before_Met.Length; i++)
            {
                for (int j = 0; j < LC_Load_After_Met.Length; j++)
                {
                    if (LC_Load_Before_Met[i].UTMX == LC_Load_After_Met[j].UTMX && LC_Load_Before_Met[i].UTMY == LC_Load_After_Met[j].UTMY && LC_Load_Before_Met[i].expo[0].radius == LC_Load_After_Met[j].expo[0].radius)
                    {
                        for (int k = 0; k < numWD; k++)
                        {
                            Assert.AreEqual(LC_Load_Before_Met[i].expo[0].SR[k], LC_Load_After_Met[j].expo[0].SR[k], 0.00001, "Different SR" + LC_Load_Before_Met[i].UTMX.ToString() + "," + LC_Load_Before_Met[i].UTMY.ToString());
                            Assert.AreEqual(LC_Load_Before_Met[i].expo[0].dispH[k], LC_Load_After_Met[j].expo[0].dispH[k], 0.00001, "Different SR" + LC_Load_Before_Met[i].UTMX.ToString() + "," + LC_Load_Before_Met[i].UTMY.ToString());
                        }
                        break;
                    }
                }
            }

        }
        
    }
}
