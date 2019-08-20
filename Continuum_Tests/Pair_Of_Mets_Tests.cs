using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class Pair_Of_Mets_Tests
    {

        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Pair_Of_Mets";

        [TestMethod]
        public void DoMetCrossPred_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\Pair_Of_Mets testing.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            NodeCollection nodeList = new NodeCollection();
            thisInst.metPairList.ClearAll();
            thisInst.metPairList.CreateMetPairs(thisInst);

            Nodes Met1_Node = new Nodes();
            Nodes Met2_Node = new Nodes();

            // Find path of nodes in between mets.  if met pairs already exist then just add new UWDW default model
            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
            {
                for (int j = 0; j < thisInst.radiiList.ThisCount; j++)
                {
                    if (thisInst.metPairList.metPairs[i].WS_Pred[0, j].nodePath == null)
                    {
                        Met.WSWD_Dist thisDist1 = thisInst.metPairList.metPairs[i].met1.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                        Met.WSWD_Dist thisDist2 = thisInst.metPairList.metPairs[i].met2.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                        double[] windRose = new double[thisDist1.windRose.Length];
                        for (int WD = 0; WD <= windRose.Length - 1; WD++)
                            windRose[WD] = (thisDist1.windRose[WD] + thisDist2.windRose[WD]) / 2;

                        Met1_Node = nodeList.GetMetNode(thisInst.metPairList.metPairs[i].met1);
                        Met2_Node = nodeList.GetMetNode(thisInst.metPairList.metPairs[i].met2);
                        Nodes[] blankNodes = null;
                        Nodes[] pathOfNodes = nodeList.FindPathOfNodes(Met1_Node, Met2_Node, thisInst.metPairList.metPairs[i].WS_Pred[0, j].model, thisInst);

                        thisInst.metPairList.metPairs[i].WS_Pred[0, j].nodePath = pathOfNodes;
                    }

                    thisInst.metPairList.metPairs[i].DoMetCrossPred(0, j, thisInst);
                }
            }

            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
            {
                for (int j = 0; j < thisInst.radiiList.ThisCount; j++)
                {
                    Assert.AreNotEqual(thisInst.metPairList.metPairs[i].WS_Pred[0, j].WS_Ests[0], 0, 0, "Didn't calc WS est");
                    Assert.AreNotEqual(thisInst.metPairList.metPairs[i].WS_Pred[0, j].WS_Ests[1], 0, 0, "Didn't calc WS est");
                    Assert.AreNotEqual(thisInst.metPairList.metPairs[i].WS_Pred[0, j].percErr[0], 0, 0, "Didn't calc error");
                    Assert.AreNotEqual(thisInst.metPairList.metPairs[i].WS_Pred[0, j].percErr[1], 0, 0, "Didn't calc error");
                }
            }

            thisInst.Close();
        }
    }
}
