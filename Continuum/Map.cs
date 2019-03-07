using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class Map
    {
        public string mapName;   // Name of map
        public double[,] parameterToMap; // Mapped parameter (overall). 
        public double[,,] sectorParamToMap; // Mapped parameter (sectorwise)  i = X ind; j = Y ind; k = WD sec (for WS maps that have sectorwise WS (so Num k = Num WD sectors) and other maps Num k = 1)
        public bool isComplete = false; // true if map is complete
        public int modelType;   // 0 = UW map, 1 = DW map, 2 = WS map (site-calibrated), 3 = Gross AEP map (site-calibrated), 4 = WS map (default), 5 = Gross AEP map (default)
        public int expoMapRadius = 2; // 8000 m: Used when map is an exposure map
        public string powerCurve = ""; // if ( map is an energy map, this specifies the power curve used
        public int minUTMX;  // min UTMX coordinate of map
        public int minUTMY;  // min UTMY coordinate of map
        public int numX;   // Grid size along X
        public int numY;  // Grid size along Y
        public int reso;  // Map resolution
        public string[] metsUsed; // Met sites used to form the estimates at map nodes (note: the model can be formed with a group of mets and then any set 
                                   // of mets can be used to create a map which are selected on the Gen_Map form)
        public Model[] model;   //  Models: one for each radius of investigation
        public bool isWaked;   // true if it is a wake map
        public Wake_Model wakeModel;   // Wake model used if it is a wake map
        public bool useSR;  // true if surface roughness model is used
        public bool useFlowSep;  // true if flow separation model is used
        
        [Serializable()] public struct mapNode {
            public double UTMX;
            public double UTMY;
            public double elev;
            public Exposure[] expo;
            public Grid_Info gridStats;
            public WS_Ests[] WS_Estimates; // All wind speed estimates formed by each predictor met and each model
            public double avgWS_Est; // Overall wind speed estimate at map node
            public double[] sectorWS; // Sectorwise wind speed estimates i = Sect num
            public double[] WS_Dist;  // Overall wind speed distribution
            public double[,] sectDist; // Sectorwise wind speed distribution // i = Sector num, j = WS interval
            public double grossAEP;   // Overall gross AEP, (only if map is Gross AEP)
            public double[] sectorGross;  // Sectorwise gross AEP, (only if map is Gross AEP)
            public bool isCalibrated;  // false if site-calibrated model is used
            public bool useSR; // true if land cover data is used
            public bool useFlowSep;  // true if flow separation model is used
            public Net_Energy_Est netEnergyEsts;   // for wake loss maps where wind speed, wake losses and net energy are calculated
            public string powerCurve;   // if map is AEP then this is power curve used
            public bool isWaked;
            public Wake_Model wakeModel;
            public double[] windRose; // Wind rose estimated at map node by interpolating (based on distance) from met site wind roses
            public NodeCollection.Sep_Nodes[] flowSepNodes;// Used in flow separation model if any sectors have UW Expo < 0 and DW Expo > 0 then finds the
        // highest point upwind (within 5000 m) and the node at the end of the turbulent zone. These are stored in flowSepNodes array
        }

        [Serializable()] public struct WS_Ests
        { 
            public string predictorMetName;  // Predictor Met name
            public NodeCollection.Node_UTMs[] pathOfNodesUTMs; // just the UTM coords of path of nodes (other info is retrieved from DB when needed)
            public int radius;   // radius used in UWDW model
            public double WS;  // Estimated wind speed
            public double WS_weight;  // Wind speed weight (calculated based on similarity in terrain and met cross-predictions error of UWDW model)
            public double[] sectorWS; // Sectorwise wind speed estimates
            public bool elevDiffTooBig;  // true if difference in elevation between map node and predictor met is larger than max value (specified in UWDW model)
            public bool expoDiffTooBig;   // true if difference in exposure between map node and predictor met is larger than max value (specified in UWDW model)
        }

        [Serializable()] public struct Net_Energy_Est
        {
            public double est;  // Net annual energy estimate
            public double[] sectorEnergy; // Sectorwise net energy
            public double wakeLoss;
            public double[] sectorWakeLoss; // Sectorwise wake loss %
        }

        public void DoMapCalcs(ref mapNode thisMapNode, Continuum thisInst, NodeCollection nodeList, NodeCollection.Path_of_Nodes_w_Rad_and_Met_Name[] pathsToMets,
                                mapNode[] allMapNodesInX, ref Nodes[] lastAllNodesInPath, string isCalibrated)
        {
            // This sub-routine performs all necessary calculations for referenced mapNode. 
            // First, the grid statistics are calculated. Then a path of nodes from each predictor met to the map node are found
            // It looks at near-by map nodes and turbine sites and uses existing path of nodes if available.
            // then it estimates the wind speed along the path of nodes for each predictor met and each model and creates WS_Ests()

            MetCollection metList = thisInst.metList;
            MapCollection mapList = thisInst.mapList;
            TopoInfo topo = thisInst.topo;
            MetPairCollection metPairList = thisInst.metPairList;
            Nodes[] newNodes = null;  // Collect new nodes and add to DB after calcs finished
            int numMetsUsed = metsUsed.Length;            
            int numRadii = thisInst.radiiList.ThisCount;
                        
            Met[] theseMets = metList.GetMets(metList.GetMetsUsed(), null);

            // For each met and each radius of investigation (i.e. each Continuum model), generate wind speed estimate at map node. 
            for (int j = 0; j < numMetsUsed; j++)
            {
                for (int r = 0; r < numRadii; r++)
                {
                    int thisRadius = thisInst.radiiList.investItem[r].radius;
                    WS_Ests newWS_Est = new WS_Ests();
                    newWS_Est.predictorMetName = theseMets[j].name;
                    newWS_Est.radius = thisRadius;
                    AddWS_Estimate(ref thisMapNode, ref newWS_Est);
                    int WS_Est_ind = thisMapNode.WS_Estimates.Length - 1;
                    // Check to see if elevation and exposure difference b/w met and turbine is within allowed limits 
                    bool isWithinModelLimit = thisInst.modelList.IsWithinModelLimit(theseMets[j].gridStats, theseMets[j].elev, thisMapNode.gridStats, thisMapNode.elev, r, theseMets[j].windRose);
                                        
                    if (isWithinModelLimit == true)
                    {
                        Nodes targetNode = nodeList.GetMapAsNode(thisMapNode);
                        Nodes startNode = nodeList.GetMetNode(theseMets[j]);

                        Nodes[]  pathOfNodes = nodeList.FindPathOfNodes(startNode, targetNode, model[r], thisInst, ref newNodes, ref lastAllNodesInPath);
                        
                        int pathLength = 0;
                        if (pathOfNodes != null)
                            pathLength = pathOfNodes.Length;

                        if (pathLength == 201) // It couldn't find a good path
                            pathOfNodes = null;
                        else
                        {
                            if (pathLength > 0)
                            {
                                NodeCollection.Node_UTMs[] pathUTMs = new NodeCollection.Node_UTMs[pathOfNodes.Length];
                                for (int m = 0; m < pathOfNodes.Length; m++)
                                {
                                    pathUTMs[m].UTMX = pathOfNodes[m].UTMX;
                                    pathUTMs[m].UTMY = pathOfNodes[m].UTMY;
                                }
                                thisMapNode.WS_Estimates[WS_Est_ind].pathOfNodesUTMs = pathUTMs;
                            }

                            thisMapNode.WS_Estimates[WS_Est_ind].radius = model[r].radius;
                            DoWS_EstAlongNodes(thisInst, ref thisMapNode, WS_Est_ind, pathOfNodes, ref lastAllNodesInPath);
                        }                                              

                    }
                }
            }  
        }

        public void CalcWakeLossesMap(ref mapNode thisMapNode, Continuum thisInst, Wake_Model thisWakeModel, WakeCollection.WakeLossCoeffs[] wakeLossCoeffs)
            {
            // Using calculated wake profile polynomials (i.e. wakeLossCoeffs), calculates the wake losses and net energy at the map node
            WakeCollection.WakeCalcResults wakeResults = thisInst.wakeModelList.CalcWakeLosses(wakeLossCoeffs, thisMapNode.UTMX, thisMapNode.UTMY, 
                thisMapNode.sectDist, thisMapNode.grossAEP, thisMapNode.sectorGross, thisInst, thisWakeModel);

            thisMapNode.isWaked = true;
            thisMapNode.avgWS_Est = wakeResults.wakedWS;
            thisMapNode.sectDist = wakeResults.sectorDist;
            thisMapNode.sectorWS = wakeResults.sectorWakedWS;

            thisMapNode.netEnergyEsts.est = wakeResults.netEnergy;
            thisMapNode.netEnergyEsts.sectorEnergy = wakeResults.sectorNetEnergy;
            thisMapNode.netEnergyEsts.wakeLoss = wakeResults.wakeLoss;
            thisMapNode.netEnergyEsts.sectorWakeLoss = wakeResults.sectorWakeLoss;

        }

        public void GenerateAvgWS_AtOneMapNode(ref mapNode thisMapNode, Continuum thisInst)
            {
            // Combines all of the wind speed estimates formed by each predictor met and each site-calibrated UWDW model to form overall 
            // average wind speed estimate (including sectorwise WS) at map node
            double avgWS = 0;
            double avgWeight = 0;
            
            bool isMetUsed = false;
            MetCollection metList = thisInst.metList;
            ModelCollection modelList = thisInst.modelList;
            InvestCollection radiiList = thisInst.radiiList;
            double[] windRose = metList.GetAvgWindRose();
            int numRadii = thisInst.radiiList.ThisCount;
            int numWD = windRose.Length;
            thisMapNode.sectorWS = new double[numWD];

            double[,] indivMetWeights = null;
                    
            Model[] models;
            int predMetInd = 0;
            
            double[] sectorWS = new double[numWD];
            int numMetsUsed = metsUsed.Length;
            Met[] predMets = new Met[numMetsUsed];

            for (int i = 0; i < numMetsUsed; i++) { 
                for (int j = 0; j < metList.ThisCount; j++) { 
                    if (metsUsed[i] == metList.metItem[j].name) {
                        predMets[i] = metList.metItem[j];
                        break;
                    }
                }
            }
            // public modelType  int // 0 = UW map, 1 = DW map, 2 = WS map (using best UWDW), 3 = Gross AEP map(using best UWDW), 4 = WS map(using default UWDW), 5 = Gross AEP map(using default UWDW)
            models = modelList.GetModels(thisInst, metList.GetMetsUsed(), radiiList.investItem[0].radius, radiiList.GetMaxRadius(), thisMapNode.isCalibrated);
            
            NodeCollection nodeList = new NodeCollection();
            Nodes mapNode = nodeList.GetMapAsNode(thisMapNode);
            indivMetWeights = modelList.GetWS_EstWeights(predMets, mapNode, models, metList.GetAvgWindRose());

            for (int r = 0; r < numRadii; r++)
            { 
                for (int j = 0; j < thisMapNode.WS_Estimates.Length; j++) {
                    isMetUsed = false;

                    for (int k = 0; k < metsUsed.Length; k++) { 
                        if (metsUsed[k] == thisMapNode.WS_Estimates[j].predictorMetName) {
                            isMetUsed = true;
                            predMetInd = k;
                            break;
                        }
                    }

                    if (isMetUsed == true && thisMapNode.WS_Estimates[j].radius == radiiList.investItem[r].radius && thisMapNode.WS_Estimates[j].elevDiffTooBig == false 
                        && thisMapNode.WS_Estimates[j].expoDiffTooBig == false && thisMapNode.WS_Estimates[j].WS!= 0 ) {
                        avgWS = avgWS + thisMapNode.WS_Estimates[j].WS * indivMetWeights[predMetInd, r];
                        thisMapNode.WS_Estimates[j].WS_weight = indivMetWeights[predMetInd, r];
                        avgWeight = avgWeight + indivMetWeights[predMetInd, r];
                        for (int WD = 0; WD < numWD; WD++)
                            sectorWS[WD] = sectorWS[WD] + thisMapNode.WS_Estimates[j].sectorWS[WD] * indivMetWeights[predMetInd, r];                        
                    }

                }
            }

            thisMapNode.avgWS_Est = 0;

            if (avgWeight != 0) { 
                thisMapNode.avgWS_Est = avgWS / avgWeight;

                for (int WD = 0; WD < numWD; WD++)
                    thisMapNode.sectorWS[WD] = thisMapNode.sectorWS[WD] + sectorWS[WD] / avgWeight;
            }
        }

        public void CalcWS_DistAtMapNode(ref mapNode thisMapNode, MetCollection metList, int numWD)
        {
            // Calculates overall and sectorwise wind speed distribution for map node
            string[] metsUsed = metList.GetMetsUsed();

            int numWS = metList.metItem[0].WS_Dist.Length;
            thisMapNode.sectDist = new double[numWD, numWS];

            for (int WD = 0; WD < numWD; WD++) {
                double[] WS_Dist = metList.CalcWS_DistForTurbOrMap(metsUsed, thisMapNode.sectorWS[WD], WD);
                for (int WS = 0; WS < numWS; WS++)
                    thisMapNode.sectDist[WD, WS] = WS_Dist[WS] * 1000;
                
            }

            thisMapNode.WS_Dist = metList.CalcOverallWS_Dist(thisMapNode.sectDist, thisMapNode.windRose);
        }

        public void CalcGrossAEP_AtMapNode(ref mapNode thisMapNode, MetCollection metList, TurbineCollection turbineList)
        {
            // Calculates gross AEP at referenced mapNode            
            int numWS = metList.numWS;
            int numWD = metList.numWD;            
            thisMapNode.sectorGross = new double[numWD];                       
            
            TurbineCollection.PowerCurve thisPowerCurve = turbineList.GetPowerCurve(powerCurve);                      

            thisMapNode.grossAEP = 0;
            for (int k = 0; k < numWS; k++) {
                thisMapNode.grossAEP = thisMapNode.grossAEP + thisMapNode.WS_Dist[k] * thisPowerCurve.power[k];
                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                    thisMapNode.sectorGross[WD_Ind] = thisMapNode.sectorGross[WD_Ind] + thisMapNode.sectDist[WD_Ind, k] / 1000 * thisPowerCurve.power[k];                
            }

            thisMapNode.grossAEP = thisMapNode.grossAEP * 365 * 24 / 1000;
            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                thisMapNode.sectorGross[WD_Ind] = thisMapNode.sectorGross[WD_Ind] * 365 * 24 / 1000 * thisMapNode.windRose[WD_Ind];            

        }

        public void DoWS_EstAlongNodes(Continuum thisInst, ref mapNode thisMapNode, int WS_Est_Ind, Nodes[] pathOfNodes, ref Nodes[] lastAllNodesInPath)
        {
            // Calculates wind speed from Met to Map node along path of nodes             
            InvestCollection radiiList = thisInst.radiiList;
            ModelCollection modelList = thisInst.modelList;
            NodeCollection nodeList = new NodeCollection();
                        
            Met thisMet = thisInst.metList.GetMet(thisMapNode.WS_Estimates[WS_Est_Ind].predictorMetName);
            int thisRadius = thisMapNode.WS_Estimates[WS_Est_Ind].radius;                      

            if (thisMet == null) return;

            int numWD = thisInst.metList.numWD;
            int radiusIndex = 0;            

            for (int i = 0; i < radiiList.ThisCount; i++) { 
                if (thisRadius == radiiList.investItem[i].radius) {
                    radiusIndex = i;
                    break;
                }
            }

            // 0 = UW map, 1 = DW map, 2 = WS map (using best UWDW), 3 = Gross AEP map (using best UWDW), 4 = WS map (using default UWDW), 5 = Gross AEP map (using default UWDW)
            Model[] models = modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisRadius, thisRadius, thisMapNode.isCalibrated);
            Model thisModel = models[0];
            
            if (pathOfNodes == null) pathOfNodes = nodeList.GetPathOfNodes(thisMapNode.WS_Estimates[WS_Est_Ind].pathOfNodesUTMs,thisInst, ref lastAllNodesInPath);

            Nodes endNode = nodeList.GetMapAsNode(thisMapNode);
            ModelCollection.WS_Est_Struct WS_EstStr = modelList.DoWS_Estimate(thisMet, endNode, pathOfNodes, radiusIndex, thisModel, thisInst);

            thisMapNode.WS_Estimates[WS_Est_Ind].sectorWS = WS_EstStr.sectorWS;

            // Have sectorwise wind speed at map node now need to calculate avg WS using wind rose            
            for (int WD = 0; WD < numWD; WD++) 
                thisMapNode.WS_Estimates[WS_Est_Ind].WS = thisMapNode.WS_Estimates[WS_Est_Ind].WS + thisMapNode.WS_Estimates[WS_Est_Ind].sectorWS[WD] * thisMapNode.windRose[WD];             

        }

        public void AddWS_Estimate(ref mapNode thisMapNode, ref WS_Ests newWS_Est)
        {
            // Adds a wind speed estimate to the list of WS_Ests
            int newCount = 0;
            if (thisMapNode.WS_Estimates != null)
                newCount = thisMapNode.WS_Estimates.Length;
            else
                newCount = 0;

            if (newCount > 0) {
                Array.Resize(ref thisMapNode.WS_Estimates, newCount + 1);
                thisMapNode.WS_Estimates[newCount] = newWS_Est;
            }
            else {
                thisMapNode.WS_Estimates = new WS_Ests[1];
                thisMapNode.WS_Estimates[0] = newWS_Est;
            }

        }

        public bool IsNewSRDH(mapNode thisMapNode, int radius, double exponent, int numSectors)
        {
            // Checks to see if surface roughness and displacement height have been calculated and returns true if new and needs to be calculated
            int thisCount = 0;

            try {
                thisCount = thisMapNode.expo.Length;
            }
            catch {
                thisCount = 0;
            }

            bool isNew = false;
            if (thisCount == 0) isNew = true;

            for (int i = 0; i < thisCount; i++) {
                if (thisMapNode.expo[i].exponent == exponent && thisMapNode.expo[i].radius == radius && thisMapNode.expo[i].numSectors == numSectors
                    && thisMapNode.expo[i].SR != null && thisMapNode.expo[i].dispH != null)
                { // the exposures based on radius and exp combo and number of sectors to avg already calculated
                    //MsgBox("caught it" & exponent & " " & radius)
                    isNew = false;
                    break;
                }
                else
                    isNew = true;                
            }

            return isNew;
        }

        public void CalcMapExposures(ref mapNode thisMapNode, int numSectors, Continuum thisInst)
        {
            // Calculates exposure and SRDH at referenced map node 
            int numWD = thisInst.GetNumWD();
           
            for (int i = 0; i < thisInst.radiiList.ThisCount; i++) {
                int radius = thisInst.radiiList.investItem[i].radius;
                double exponent = thisInst.radiiList.investItem[i].exponent;

                // First find elevation
                if (thisMapNode.elev == 0)
                    thisMapNode.elev = thisInst.topo.CalcElevs(thisMapNode.UTMX, thisMapNode.UTMY);

                // Check to see if the exposures have already been calculated
                bool isNew = IsNewExposure(thisMapNode, radius, exponent, numSectors);
                bool isNewSRDH = IsNewSRDH(thisMapNode, radius, exponent, numSectors);

                int smallerRadius = 0;
                Exposure smallerExposure;

                if (isNew == true) {
                    AddExposure(ref thisMapNode, radius, exponent, numSectors, numWD);
                    // Find exposure index
                    int expoInd = 0;
                    for (expoInd = 0; expoInd <= thisMapNode.expo.Length - 1; expoInd++) {
                        if (thisMapNode.expo[expoInd].radius == radius && thisMapNode.expo[expoInd].exponent == exponent && thisMapNode.expo[expoInd].numSectors == numSectors) {
                            // Found the exposure index
                            break;
                        }
                    }

                    // Check to see if an exposure with a smaller radii has been calculated
                    smallerRadius = thisInst.topo.GetSmallerRadius(thisMapNode.expo, radius, exponent, numSectors);

                    if (smallerRadius == 0 || numSectors > 1) { // when sector avg is used, can't add on to exposure calcs... so gotta do it the long way
                        thisMapNode.expo[expoInd] = thisInst.topo.CalcExposures(thisMapNode.UTMX, thisMapNode.UTMY, thisMapNode.elev, radius, exponent, numSectors, thisInst.topo, numWD);
                        if (thisInst.topo.gotSR == true) {
                            thisInst.topo.CalcSRDH(ref thisMapNode.expo[expoInd], thisMapNode.UTMX, thisMapNode.UTMY, radius, exponent, numWD);
                        }
                    }
                    else {
                        smallerExposure = thisInst.topo.GetSmallerRadiusExpo(thisMapNode.expo, smallerRadius, exponent, numSectors);

                        thisMapNode.expo[expoInd] = thisInst.topo.CalcExposuresWithSmallerRadius(thisMapNode.UTMX, thisMapNode.UTMY, thisMapNode.elev, radius, exponent, numSectors,
                            smallerRadius, smallerExposure, numWD);
                        if (thisInst.topo.gotSR == true)
                            thisInst.topo.CalcSRDHwithSmallerRadius(ref thisMapNode.expo[expoInd], thisMapNode.UTMX, thisMapNode.UTMY, radius, exponent, numSectors, smallerRadius, smallerExposure, numWD);

                    }

                }
                else if (isNewSRDH == true) {
                    int expoInd = 0;
                    for (expoInd = 0; expoInd <= thisMapNode.expo.Length - 1; expoInd++) { 
                        if (thisMapNode.expo[expoInd].radius == radius && thisMapNode.expo[expoInd].exponent == exponent && thisMapNode.expo[expoInd].numSectors == numSectors) 
                            // Found the exposure index
                            break;                        
                    }

                    if (thisInst.topo.gotSR == true) {
                        // Check to see if an exposure with a smaller radii has been calculated
                        smallerRadius = thisInst.topo.GetSmallerRadius(thisMapNode.expo, radius, exponent, numSectors);

                        if (smallerRadius == 0)
                            thisInst.topo.CalcSRDH(ref thisMapNode.expo[expoInd], thisMapNode.UTMX, thisMapNode.UTMY, radius, exponent, numWD);
                        else {
                            smallerExposure = thisInst.topo.GetSmallerRadiusExpo(thisMapNode.expo, smallerRadius, exponent, numSectors);
                            thisInst.topo.CalcSRDHwithSmallerRadius(ref thisMapNode.expo[expoInd], thisMapNode.UTMX, thisMapNode.UTMY, radius, exponent, numSectors, smallerRadius, smallerExposure, numWD);
                        } 
                    }
                }
            }

            // Calc P10 UW Crosswind Grade            
            for (int expoInd = 0; expoInd < thisInst.radiiList.ThisCount; expoInd++) {
                thisMapNode.expo[expoInd].UW_P10CrossGrade = new double[numWD];
                thisMapNode.expo[expoInd].UW_ParallelGrade = new double[numWD];
            }

            for (int r = 0; r < numWD; r++) {
                double UW_CW_Grade = thisInst.topo.CalcP10_UW_CrosswindGrade(thisMapNode.UTMX, thisMapNode.UTMY, thisInst.radiiList, r, numWD);
                double UW_PL_Grade = thisInst.topo.CalcP10_UW_ParallelGrade(thisMapNode.UTMX, thisMapNode.UTMY, thisInst.radiiList, r, numWD);
                for (int expoInd = 0; expoInd < thisInst.radiiList.ThisCount; expoInd++) {
                    thisMapNode.expo[expoInd].UW_P10CrossGrade[r] = UW_CW_Grade;
                    thisMapNode.expo[expoInd].UW_P10CrossGrade[r] = UW_PL_Grade;
                }
            }
        }

        public void AddExposure(ref mapNode thisMapNode, int radius, double exponent, int numSectors, int numWD)
        {
            // Adds another exposure to the list
            int expoCount = 0;
            int insertInd = 0;

            if (thisMapNode.expo != null)
                expoCount = thisMapNode.expo.Length;
            else
                expoCount = 0;
            
            if (expoCount > 0) {
                if (radius > thisMapNode.expo[expoCount - 1].radius)  // Larger radius than largest in list
                    insertInd = expoCount;
                else if (radius < thisMapNode.expo[0].radius)  // Smaller than smallest in list
                    insertInd = 0;
                else {
                    for (int i = 0; i <= expoCount - 2; i++) { 
                            if (thisMapNode.expo[i].radius < radius && thisMapNode.expo[i + 1].radius >= radius) {
                            insertInd = i + 1;
                            break;
                        }
                    }
                }

                Exposure[] existingExpos = new Exposure[expoCount];

                for (int j = 0; j < expoCount; j++)
                    existingExpos[j] = thisMapNode.expo[j];

                thisMapNode.expo = new Exposure[expoCount + 1];

                for (int j = 0; j < insertInd; j++) // this code is here because when the array is resized, it erases the previous entries
                    thisMapNode.expo[j] = existingExpos[j]; // there//s probably a better way to do this...

                thisMapNode.expo[insertInd] = new Exposure();
                thisMapNode.expo[insertInd].radius = radius;
                thisMapNode.expo[insertInd].exponent = exponent;
                thisMapNode.expo[insertInd].numSectors = numSectors;
                thisMapNode.expo[insertInd].expo = new double[numWD];

                for (int j = insertInd + 1; j <= expoCount; j++)
                    thisMapNode.expo[j] = existingExpos[j - 1];
            }
            else {
                thisMapNode.expo = new Exposure[1];
                thisMapNode.expo[0] = new Exposure();
                thisMapNode.expo[0].radius = radius;
                thisMapNode.expo[0].exponent = exponent;
                thisMapNode.expo[0].numSectors = numSectors;
                thisMapNode.expo[0].expo = new double[numWD];
            }

        }

        public bool IsNewExposure(mapNode thisMapNode, int radius, double exponent, int numSectors)
        {
            // Checks to see if exposure has already been calculated. Returns true if needs to be calculated.
            bool isNew = true;
            if (thisMapNode.expo != null) {
                int thisCount = thisMapNode.expo.Length;
                if (thisCount == 0) isNew = true;

                for (int i = 0; i < thisCount; i++) {
                    if (thisMapNode.expo[i].exponent == exponent && thisMapNode.expo[i].radius == radius && thisMapNode.expo[i].numSectors == numSectors
                        && thisMapNode.expo[i].expo != null)
                    { // the exposures based on radius and exp combo already calculated
                        isNew = false;
                        break;
                    }
                    else
                        isNew = true;                    
                }
            }

            return isNew;
        }

        public void FindMapStats(Continuum thisInst)
        {
            // Calculates the statistics (avg, min, max, SD) of map and updates the textboxes on Maps tab (TO DO: Move to Update class)
            double avg = 0;
            double stdev = 0;
            double min = 1000;
            double max = 0;
            double param;
            int dataCount = 0;

            for (int i = 0; i < numX; i++) { 
                for (int j = 0; j < numY; j++) {
                    param = parameterToMap[i, j];
                    avg = avg + param;
                    stdev = stdev + Math.Pow(param, 2);
                    if (param < min) min = param;
                    if (param > max) max = param;
                    dataCount++;
                }
            }

            if (dataCount > 0) {
                avg = avg / dataCount;
                stdev = (Math.Pow((stdev / dataCount - Math.Pow(avg, 2)), 0.5));
            }

            if (modelType == 3 || modelType == 5) {
                thisInst.txtMapAvg.Text = Math.Round(avg, 1).ToString();
                thisInst.txtMapStDev.Text = Math.Round(stdev, 1).ToString();
                thisInst.txtMapMin.Text = Math.Round(min, 1).ToString();
                thisInst.txtMapMax.Text = Math.Round(max, 1).ToString();
                thisInst.txtMapCount.Text = dataCount.ToString();
            }
            else {
                thisInst.txtMapAvg.Text = Math.Round(avg, 3).ToString();
                thisInst.txtMapStDev.Text = Math.Round(stdev, 3).ToString();
                thisInst.txtMapMin.Text = Math.Round(min, 3).ToString();
                thisInst.txtMapMax.Text = Math.Round(max, 3).ToString();
                thisInst.txtMapCount.Text = dataCount.ToString();
            }

            string theseMetsUsed = thisInst.metList.CreateMetString(metsUsed, true);
            thisInst.txtMap_MetsUsed.Text = theseMetsUsed;

        }

        public double FindMin(int WD_Ind, int numWD)
        {
            // Finds and returns the minimum value in map
            double min = 100000;
            double param = 0;

            for (int i = 0; i < numX; i++) { 
                for (int j = 0; j < numY; j++) {
                    if (WD_Ind == numWD || sectorParamToMap == null)
                        param = parameterToMap[i, j];
                    else
                        param = sectorParamToMap[i, j, WD_Ind];

                    if (param < min && param != 0) min = param;
                }
            }

            return min;
        }

        public double FindMax(int WD_Ind, int numWD)
        {
            // Finds and returns the maximum value in map
            double max = 0;
            double param = 0;  

            for (int i = 0; i < numX; i++) { 
                for (int j = 0; j < numY; j++) {
                    if (WD_Ind == numWD || sectorParamToMap == null)
                        param = parameterToMap[i, j];
                    else
                        param = sectorParamToMap[i, j, WD_Ind];

                    if (param > max) max = param;
                }
            }

            return max;
        }

        public void GetFlowSepNodes(ref mapNode thisMapNode, Continuum thisInst, NodeCollection nodeList, Nodes[] newNodes)
        {
            // Gets the flow separation nodes for map node (if flow separation model is used)
            int numWD = 0;
            try {
                numWD = thisMapNode.windRose.Length;
            }
            catch {
                return;
            }

            Nodes thisNode = nodeList.GetMapAsNode(thisMapNode);

            thisMapNode.flowSepNodes = nodeList.FindAllFlowSeps(thisNode, thisInst, numWD, ref newNodes);

        }

    }
}
