using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class Pair_Of_Mets
    {
        public Met met1;
        public Met met2;
        public WS_CrossPreds[,] WS_Pred; // Wind speed cross predictions; i = Model num j = radius ind

        [Serializable()] public struct WS_CrossPreds
        {
            public double[] WS_Ests; // cross-predicted wind speeds, index 0 : Est at Met 2; index 1: Est at Met 1
            public double[] percErr; // error of cross-predicted wind speeds. 0 (Est at Met 2) or 1 (Est at Met 1)
            public double[,] percErrSector; // error of cross-predicted sectorwise wind speeds i = 0 (Est at Met 2) or 1 (Est at Met 1); j = WD sector
            public Nodes[] nodePath; // Path of nodes from Met 1 to Met 2
            public double[,] nodeSectorWS_Ests1to2; // Estimated WS from Met 1 to 2 in each WD sector; i = Node num j = WD sector
            public double[,] nodeSectorWS_Ests2to1; // Estimated WS from Met 2 to 1; i = Node num j = WD sector
            public double[] nodeWS_Ests1to2; // Overall Estimated WS from Met 1 to 2
            public double[] nodeWS_Ests2to1; // Overall Estimated WS from Met 1 to 2
            public double[,] sectorWS_Ests; // Sectorwise cross-predicted wind speeds i = Met ind (0 = Est at met2, 1 = Est at met1)  j = WD sector
            public Model model; // Model used to generate cross-prediction
        }

        public int WS_PredCount
        {
            get
            {
                if (WS_Pred == null)
                    return 0;
                else
                    return WS_Pred.GetUpperBound(0) + 1;
            }
        }

        public void AddWS_Pred(Model[] model)
        {
            // Adds a WS_CrossPreds to WS_Pred(,) list
            int numRadii = model.Length;
            int numWD = model[0].downhill_A.Length;
            WS_CrossPreds[,] tempWS_Pred = new WS_CrossPreds[WS_PredCount, numRadii];
            
            for (int i = 0; i < WS_PredCount; i++)  
                for (int j = 0; j < numRadii; j++) 
                    tempWS_Pred[i, j] = WS_Pred[i, j];

            WS_Pred = new WS_CrossPreds[WS_PredCount + 1, numRadii];

            for (int i = 0; i <= WS_PredCount - 2; i++)
                for (int j = 0; j < numRadii; j++)
                    WS_Pred[i, j] = tempWS_Pred[i, j];
            
            if (WS_PredCount == 0) {
                for (int i = 0; i < numRadii; i++)
                {
                    WS_Pred[0, i] = new WS_CrossPreds();
                    WS_Pred[0, i].model = model[i];
                    WS_Pred[0, i].WS_Ests = new double[2];
                    WS_Pred[0, i].percErr = new double[2];
                    WS_Pred[0, i].sectorWS_Ests = new double[2, numWD];
                    WS_Pred[0, i].percErrSector = new double[2, numWD];
                }
            }
            else {
                for (int i = 0; i < numRadii; i++) {
                    WS_Pred[WS_PredCount - 1, i] = new WS_CrossPreds();
                    WS_Pred[WS_PredCount - 1, i].model = model[i];
                    WS_Pred[WS_PredCount - 1, i].WS_Ests = new double[2];
                    WS_Pred[WS_PredCount - 1, i].percErr = new double[2];
                    WS_Pred[WS_PredCount - 1, i].sectorWS_Ests = new double[2, numWD];
                    WS_Pred[WS_PredCount - 1, i].percErrSector = new double[2, numWD];
                }
            }
        }

        public void AddNodesToWS_Pred(Model[] model, Nodes[] pathOfNodes, int radius, ModelCollection modelList)
        {
            // Assigns pathOfNodes to WS_Cross_Pred's nodePath
            // Find WS_Pred with same UWDW model
            bool isSameModel;

            for (int i = 0; i < WS_PredCount; i++)
            {
                isSameModel = modelList.IsSameModel(WS_Pred[i, 0].model, model[0]);

                if (isSameModel == true) {
                    for (int j = 0; j <= WS_Pred.GetUpperBound(1); j++) { 
                        if (WS_Pred[i, j].model.radius == radius) {
                            WS_Pred[i, j].nodePath = pathOfNodes;
                            break;
                        }
                    }
                }
            }
        }

        public void DoMetCrossPred(int crossPredInd, int radiusIndex, Continuum thisInst)
        {
            // Conducts wind speed estimate from Met 1 to Met 2 and vice-versa and calculates WS cross-prediction struct (WS_CrossPreds[,])
            int numWD = thisInst.metList.numWD;
            int numNodes = 0;
            NodeCollection nodeList = new NodeCollection();

            if (WS_Pred[crossPredInd, radiusIndex].nodePath == null)
                numNodes = 0;
            else
                numNodes = WS_Pred[crossPredInd, radiusIndex].nodePath.Length;
        
            if (numNodes > 0) {

                WS_Pred[crossPredInd, radiusIndex].nodeWS_Ests1to2 = new double[numNodes];
                WS_Pred[crossPredInd, radiusIndex].nodeSectorWS_Ests1to2 = new double[numNodes, numWD];

                WS_Pred[crossPredInd, radiusIndex].nodeWS_Ests2to1 = new double[numNodes];
                WS_Pred[crossPredInd, radiusIndex].nodeSectorWS_Ests2to1 = new double[numNodes, numWD];                
            }
                                    
            Nodes[] pathOfNodes;
            Model thisModel = WS_Pred[crossPredInd, radiusIndex].model;

            // Get WS distributions at each site (used to calculate the cross-prediction percent error)
            Met.WSWD_Dist met1Dist = met1.GetWS_WD_Dist(thisInst.modeledHeight, thisModel.timeOfDay, thisModel.season);
            Met.WSWD_Dist met2Dist = met2.GetWS_WD_Dist(thisInst.modeledHeight, thisModel.timeOfDay, thisModel.season);

            // Conduct cross-prediction from Met 1 to Met 2
            Nodes endNode = nodeList.GetMetNode(met2);
            ModelCollection.WS_Est_Struct WS_EstStr = thisInst.modelList.DoWS_Estimate(met1, endNode, WS_Pred[crossPredInd, radiusIndex].nodePath, thisModel, thisInst);
                
            // Get sectorwise wind speed and calculate sectorwise percent error (for Met 1 predicting Met 2)
            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                WS_Pred[crossPredInd, radiusIndex].sectorWS_Ests[0, WD_Ind] = WS_EstStr.sectorWS[WD_Ind];
                double met2WS = met2Dist.WS * met2Dist.sectorWS_Ratio[WD_Ind];
                WS_Pred[crossPredInd, radiusIndex].percErrSector[0, WD_Ind] = (WS_EstStr.sectorWS[WD_Ind] - met2WS) / met2WS;
            }

            WS_Pred[crossPredInd, radiusIndex].nodeSectorWS_Ests1to2 = WS_EstStr.sectorWS_AtNodes;
            double avgWS = 0;
            double WR_count = 0;
        
            // Get overall wind speed at nodes (for Met 1 predicting Met 2)
            for (int nodeInd = 0; nodeInd < numNodes; nodeInd++)
            {
                avgWS = 0;
                WR_count = 0;
                Nodes thisNode = WS_Pred[crossPredInd, radiusIndex].nodePath[nodeInd];
                double[] nodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, thisNode.UTMX, thisNode.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);

                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++) {
                    avgWS = avgWS + nodeWindRose[WD_Ind] * WS_Pred[crossPredInd, radiusIndex].nodeSectorWS_Ests1to2[nodeInd, WD_Ind];
                    WR_count = WR_count + nodeWindRose[WD_Ind];
                }

                WS_Pred[crossPredInd, radiusIndex].nodeWS_Ests1to2[nodeInd] = avgWS / WR_count;

            }

            avgWS = 0;
            WR_count = 0;

            for (int WD = 0; WD < numWD; WD++) {
                avgWS = avgWS + WS_Pred[crossPredInd, radiusIndex].sectorWS_Ests[0, WD] * met2Dist.windRose[WD];
                WR_count = WR_count + met2Dist.windRose[WD];
            }

            // Calculate avg WS estimate and overall error at Met 2
            avgWS = avgWS / WR_count;
            WS_Pred[crossPredInd, radiusIndex].WS_Ests[0] = avgWS;
            WS_Pred[crossPredInd, radiusIndex].percErr[0] = (avgWS - met2Dist.WS) / met2Dist.WS;

            // Now conduct same calculations but going from Met 2 to Met 1.
            endNode = nodeList.GetMetNode(met1);

            if (numNodes > 0)
                pathOfNodes = new Nodes[numNodes];
            else
                pathOfNodes = null;

            // Reverse order of nodes when going from Met 2 to Met 1
            for (int i = 0; i < numNodes; i++)
                pathOfNodes[i] = WS_Pred[crossPredInd, radiusIndex].nodePath[numNodes - 1 - i];

            // Do WS Estimate for Met 2 predicting Met 1
            WS_EstStr = thisInst.modelList.DoWS_Estimate(met2, endNode, pathOfNodes, thisModel, thisInst);
        
            // Get sectorwise wind speed and sectorwise percent error (for Met 2 predicting Met 1)
            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++) {
                WS_Pred[crossPredInd, radiusIndex].sectorWS_Ests[1, WD_Ind] = WS_EstStr.sectorWS[WD_Ind];
                WS_Pred[crossPredInd, radiusIndex].percErrSector[1, WD_Ind] = (WS_EstStr.sectorWS[WD_Ind] - met1Dist.WS * met1Dist.sectorWS_Ratio[WD_Ind])
                                                                            / (met1Dist.WS * met1Dist.sectorWS_Ratio[WD_Ind]);
            }

            WS_Pred[crossPredInd, radiusIndex].nodeSectorWS_Ests2to1 = WS_EstStr.sectorWS_AtNodes;
            avgWS = 0;
            WR_count = 0;
            // Get overall wind speed at nodes (for Met 2 predicting Met 1)
            for (int nodeInd = 0; nodeInd < numNodes; nodeInd++) {
                avgWS = 0;
                WR_count = 0;
                Nodes thisNode = pathOfNodes[nodeInd];
                double[] nodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, thisNode.UTMX, thisNode.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);

                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++) {
                    avgWS = avgWS + nodeWindRose[WD_Ind] * WS_Pred[crossPredInd, radiusIndex].nodeSectorWS_Ests2to1[nodeInd, WD_Ind];
                    WR_count = WR_count + nodeWindRose[WD_Ind];
                }

                WS_Pred[crossPredInd, radiusIndex].nodeWS_Ests2to1[nodeInd] = avgWS / WR_count;
            }

            avgWS = 0;
            WR_count = 0;
            for (int WD = 0; WD < numWD; WD++) {
                avgWS = avgWS + WS_Pred[crossPredInd, radiusIndex].sectorWS_Ests[1, WD] * met1Dist.windRose[WD];
                WR_count = WR_count + met1Dist.windRose[WD];
            }

            // Calculate avg WS estimate and overall error at Met 1
            avgWS = avgWS / WR_count;
            WS_Pred[crossPredInd, radiusIndex].WS_Ests[1] = avgWS;
            WS_Pred[crossPredInd, radiusIndex].percErr[1] = (avgWS - met1Dist.WS) / met1Dist.WS;

        }

        public bool HaveWS_Pred(Model[] model, ModelCollection modelList)
        {
            // Returns true if WS Estimate exists
            bool gotIt = false;

            for (int i = 0; i < WS_PredCount; i++) {
                gotIt = modelList.IsSameModel(model[0], WS_Pred[i, 0].model);
                if (gotIt == true)
                    break;
            }

            return gotIt;
        }

        public int GetWS_PredInd(Model[] model, ModelCollection modelList)
        {
            //  Returns index of WS estimate with specified model()
            int WS_PredInd = -1;
            bool isSameModel = false;

            for (int i = 0; i < WS_PredCount; i++) {
                isSameModel = modelList.IsSameModel(model[0], WS_Pred[i, 0].model); // just comparing with first radii to find WS_PredInd

                if (isSameModel == true) {
                    WS_PredInd = i;
                    break;
                }
            }

            return WS_PredInd;
        }

        public WS_CrossPreds GetWS_CrossPred(Model model)
        {
            //  Returns index of WS estimate with specified model()
            WS_CrossPreds thisCrossPred = new WS_CrossPreds();
            ModelCollection modelList = new ModelCollection();
            bool isSameModel = false;

            for (int i = 0; i < WS_PredCount; i++)
            {
                for (int j = 0; j <= WS_Pred.GetUpperBound(1); j++)
                {
                    isSameModel = modelList.IsSameModel(model, WS_Pred[i, j].model); 

                    if (isSameModel == true)
                    {
                        thisCrossPred = WS_Pred[i, j];
                        break;
                    }
                }
            }

            return thisCrossPred;
        }

        public int GetWS_PredIndOneModel(Model model, ModelCollection modelList)
        {
            //  Returns index of WS estimate with default model
            int WS_PredInd = -1;
            bool isSameModel = false;
            int numRadii = 0;
            bool gotWS_PredInd = false;

            for (int i = WS_PredCount - 1; i >= 0; i--) {
                numRadii = WS_Pred.GetUpperBound(1) + 1;
                for (int j = 0; j <= numRadii - 1; j++) {
                    isSameModel = modelList.IsSameModel(model, WS_Pred[i, j].model);
                    if (isSameModel == true) {
                        WS_PredInd = i;
                        gotWS_PredInd = true;
                        break;
                    }
                }
            if (gotWS_PredInd == true) 
                break;

            }

            return WS_PredInd;
        }

        public void RemoveWS_PredByMet(string metName)
        {
            //  Deletes WS_Estimates that were formed with specified met
            WS_CrossPreds[,] newWS_Preds = null;
            int newWS_count = 0;
            bool keepWS_Pred = false;
            int numRadii = 0;

            for (int i = 0; i < WS_PredCount; i++) { 
                for (int j = 0; j <= WS_Pred.GetUpperBound(1); j++) {
                    keepWS_Pred = true;
                    numRadii = WS_Pred.GetUpperBound(1);
                    for (int k = 0; k < WS_Pred[i, j].model.metsUsed.Length; k++) { 
                        if (WS_Pred[i, j].model.metsUsed[k] == metName) {
                            keepWS_Pred = false;
                            break;
                        }
                    }

                    if (keepWS_Pred == true) {
                        Continuum thisInst = new Continuum();
                        newWS_Preds = thisInst.ResizeArray(ref newWS_Preds, newWS_count + 1, numRadii);
                        newWS_Preds[newWS_count, numRadii - 1] = WS_Pred[i, j];
                        newWS_count++;

                    }
                }
            }

            WS_Pred = new WS_CrossPreds[newWS_count, numRadii];

            for (int i = 0; i < newWS_count; i++)  
                for (int j = 0; j < numRadii; j++) 
                    WS_Pred[i, j] = newWS_Preds[i, j];

        }

        public void RemoveWS_Pred(int WS_PredInd)
        {
            // Deletes WS_Estimate with specified index
            int newCount = WS_PredCount - 1;
            int numRadii = 0;

            if (newCount > 0) {
                numRadii = WS_Pred.GetUpperBound(1) + 1;
                WS_CrossPreds[,] tempList = new WS_CrossPreds[newCount, numRadii];   // Create list of radii that you're keeping(so size one less than before)
                int tempIndex = 0;

                for (int i = 0; i < WS_PredCount; i++) {
                    if (i != WS_PredInd) {
                        for (int j = 0; j <= numRadii - 1; j++)
                            tempList[tempIndex, j] = WS_Pred[i, j];

                        tempIndex++;
                    }
                }

                WS_Pred = tempList;
            }
            else 
                WS_Pred = new WS_CrossPreds[newCount + 1, numRadii];
        }

        public void RemoveWS_PredExceptDefault() // this doesn't look like it does anything
        {
            // Deletes all WS_Estimate except default model
            int newCount = 1;
            int numRadii = WS_Pred.GetUpperBound(1) + 1;

            WS_CrossPreds[,] tempList = new WS_CrossPreds[newCount, numRadii];
            int tempIndex = 0;

            for (int j = 0; j <= numRadii - 1; j++)
                tempList[0, j] = WS_Pred[0, j];

            tempIndex = tempIndex + 1;

            WS_Pred = tempList;

        }

        
    }
}
