using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;

namespace ContinuumNS
{
    public class NodeCollection
    {
        [Serializable()]
        public struct Node_UTMs
        {
            public double UTMX;
            public double UTMY;
        }

        [Serializable()]
        public struct Sep_Nodes
        {
            public Nodes highNode;
            public Nodes turbEndNode;
            public int flowSepWD;
        }

        public struct Path_of_Nodes_w_Rad_and_Met_Name
        {
            public double distToClosestNode;
            public string predMet;
            public int radius;
            public Nodes[] pathOfNodes;
        }

        /// <summary>
        /// Holds min/max X/Y values
        /// </summary>
        public struct MinMax_XY
        {
            public double minX;
            public double minY;
            public double maxX;
            public double maxY;
        }

        public string GetDB_ConnectionString(string savedFileName)
        {
            // Returns connection string to database
            string fileName = savedFileName;
            string serverName = "(localdb)\\MSSQLLocalDB";
            string databaseName = "";

            if (fileName != "" && fileName != null)
            {
                int dotInd = fileName.LastIndexOf(".");
                int slashInd = fileName.LastIndexOf("\\");

                fileName = fileName.Substring(slashInd + 1, dotInd - slashInd - 1);
                databaseName = fileName + ".mdf";
            }
            else
                databaseName = "Continuum_DB_test.mdf";

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            string thisDirectory;

            if (fileName != "" && fileName != null)
            {
                int dotInd = savedFileName.LastIndexOf(".");
                int slashInd = savedFileName.LastIndexOf("\\");

                thisDirectory = savedFileName.Substring(0, slashInd);
            }
            else
                thisDirectory = System.IO.Directory.GetCurrentDirectory();

            sqlBuilder.DataSource = serverName;            
            sqlBuilder.AttachDBFilename = thisDirectory + "\\" + databaseName;
            
            sqlBuilder.IntegratedSecurity = true;

            return sqlBuilder.ToString();
        }

        public void AddNodes(Nodes[] newNodes, string savedFileName)
        {
            // Adds a node to database including exposure, SRDH and grid stats
            string connString = GetDB_ConnectionString(savedFileName);
            int numNewNodes = 0;

            if (newNodes == null)
                return;
            else
                numNewNodes = newNodes.Length;

            Node_table[] newNodesDB = new Node_table[numNewNodes];

            for (int i = 0; i < numNewNodes; i++)
            {
                newNodesDB[i] = new Node_table(); 
                newNodesDB[i].UTMX = newNodes[i].UTMX;
                newNodesDB[i].UTMY = newNodes[i].UTMY;
                newNodesDB[i].elev = newNodes[i].elev;

                BinaryFormatter bin = new BinaryFormatter();
                int numRadii = newNodes[i].expo.Length;

                for (int j = 0; j < numRadii; j++)
                {
                    Expo_table newExpo = new Expo_table();

                    MemoryStream MS1 = new MemoryStream();
                    MemoryStream MS2 = new MemoryStream();
                    MemoryStream MS3 = new MemoryStream();
                    MemoryStream MS4 = new MemoryStream();
                    MemoryStream MS5 = new MemoryStream();
                    MemoryStream MS6 = new MemoryStream();
                    MemoryStream MS7 = new MemoryStream();
                    MemoryStream MS8 = new MemoryStream();

                    bin.Serialize(MS1, newNodes[i].expo[j].expo);
                    newExpo.Expo_Array = MS1.ToArray();

                    bin.Serialize(MS2, newNodes[i].expo[j].expoDist);
                    newExpo.ExpoDist_Array = MS2.ToArray();

                    newExpo.radius = newNodes[i].expo[j].radius;
                    newExpo.exponent = newNodes[i].expo[j].exponent;
                    
                    bin.Serialize(MS3, newNodes[i].expo[j].UW_P10CrossGrade);
                    newExpo.UW_Cross_Grade = MS3.ToArray();

                    bin.Serialize(MS6, newNodes[i].expo[j].UW_ParallelGrade);
                    newExpo.UW_ParallelGrade = MS6.ToArray();

                    if (newNodes[i].expo[j].SR != null) {
                        bin.Serialize(MS7, newNodes[i].expo[j].SR);
                        newExpo.SR_Array = MS7.ToArray();
                    }

                    if (newNodes[i].expo[j].dispH != null) {
                        bin.Serialize(MS8, newNodes[i].expo[j].dispH);
                        newExpo.DH_Array = MS8.ToArray();
                    }

                    GridStat_table newGridStat = new GridStat_table();

                    newGridStat.radius = newNodes[i].gridStats.stats[j].radius;

                    bin.Serialize(MS4, newNodes[i].gridStats.stats[j].P10_UW);
                    newGridStat.P10_UW = MS4.ToArray();

                    bin.Serialize(MS5, newNodes[i].gridStats.stats[j].P10_DW);
                    newGridStat.P10_DW = MS5.ToArray();

                    newNodesDB[i].expo.Add(newExpo);                  
                    newNodesDB[i].GridStats.Add(newGridStat);
                }
            }
                        
            try {
                using (var context = new Continuum_EDMContainer(connString)) {
                    context.Node_table.AddRange(newNodesDB);
                    context.SaveChanges();                    
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.InnerException.ToString());                
                return;
            }

        }

        public void AddNodeOrUpdateNodeGridStat(Nodes nodeToUpdate, string savedFileName)
        {
            // Finds a node in database and updates exposure, SRDH and grid stats
            string connString = GetDB_ConnectionString(savedFileName);
            
            if (nodeToUpdate == null)
                return;

            try
            {
                using (var context = new Continuum_EDMContainer(connString))
                {
                    var node_db = from N in context.Node_table where N.UTMX == nodeToUpdate.UTMX & N.UTMY == nodeToUpdate.UTMY select N;
                    Node_table thisNode = new Node_table();

                    foreach (var N in node_db)
                        thisNode = N;

                    if (thisNode.elev == 0) // didn't find it in the database so need to add it
                    {
                        Nodes[] nodeToAdd = new Nodes[1];
                        nodeToAdd[0] = nodeToUpdate;
                        AddNodes(nodeToAdd, savedFileName);
                    }
                    else // need to update existing database entry
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        int numRadii = nodeToUpdate.expo.Length;

                        for (int j = 0; j < numRadii; j++)
                        {
                            MemoryStream MS1 = new MemoryStream();
                            MemoryStream MS2 = new MemoryStream();

                            if (nodeToUpdate.gridStats.StatCount > 0)
                            {
                                GridStat_table newGridStat = new GridStat_table();
                                newGridStat.radius = nodeToUpdate.gridStats.stats[j].radius;

                                bin.Serialize(MS1, nodeToUpdate.gridStats.stats[j].P10_UW);
                                newGridStat.P10_UW = MS1.ToArray();

                                bin.Serialize(MS2, nodeToUpdate.gridStats.stats[j].P10_DW);
                                newGridStat.P10_DW = MS2.ToArray();

                                thisNode.GridStats.Add(newGridStat);
                            }
                        }

                        context.SaveChanges();                        
                    }
                    
                }
            }            
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
                return;
            }

        }

        public Nodes GetMetNode(Met thisMet)
        {
            // Returns a node with met expos, stats, flow sep nodes
            Nodes metNode = new Nodes();
            metNode.UTMX = thisMet.UTMX;
            metNode.UTMY = thisMet.UTMY;
            metNode.elev = thisMet.elev;
            metNode.gridStats = thisMet.gridStats;
            metNode.expo = thisMet.expo;            
            metNode.flowSepNodes = thisMet.flowSepNodes;

            return metNode;
        }

        public Nodes GetTurbNode(Turbine thisTurb)
        {
            // Returns a node with turbine expos, stats, flow sep nodes
            Nodes turbNode = new Nodes();
            turbNode.UTMX = thisTurb.UTMX;
            turbNode.UTMY = thisTurb.UTMY;
            turbNode.elev = thisTurb.elev;
            turbNode.gridStats = thisTurb.gridStats;
            turbNode.expo = thisTurb.expo;            
            turbNode.flowSepNodes = thisTurb.flowSepNodes;

            return turbNode;
        }

        /// <summary>
        /// Returns a node with map node exposure, grid stats (P10 exposures), and flow separation nodes (if flow sep. model used)
        /// </summary>
        public Nodes GetMapAsNode(Map.MapNode thisMapNode)
        {            
            Nodes mapNode = new Nodes();
            mapNode.UTMX = thisMapNode.UTMX;
            mapNode.UTMY = thisMapNode.UTMY;
            mapNode.elev = thisMapNode.elev;
            mapNode.gridStats = thisMapNode.gridStats;
            mapNode.expo = thisMapNode.expo;           
            mapNode.flowSepNodes = thisMapNode.flowSepNodes;

            return mapNode;
        }

        public Nodes GetANode(double UTMX, double UTMY, Continuum thisInst)
        {
            // Searches database for node with specified UTMX/Y and returns it
            Nodes newNode = new Nodes();
            newNode.gridStats = new Grid_Info();
            int numRadii = 0;
            int numWD = thisInst.metList.numWD;

            string connString = GetDB_ConnectionString(thisInst.savedParams.savedFileName);
            
            try {
                using (var context = new Continuum_EDMContainer(connString)) {
                    var node_db = from N in context.Node_table.Include("expo") where N.UTMX == UTMX & N.UTMY == UTMY select N;

                    foreach (var N in node_db)
                    {
                        if (newNode.expo == null) { // in case there are more than node with this X and Y
                            newNode.UTMX = N.UTMX;
                            newNode.UTMY = N.UTMY;
                            newNode.elev = N.elev;

                            try {
                                numRadii = N.expo.Count();                                
                            }
                            catch {
                                newNode.UTMX = 0;
                                newNode.UTMY = 0;
                                return newNode;
                            }

                            BinaryFormatter bin = new BinaryFormatter();

                            for (int i = 0; i < numRadii; i++)
                            {
                                newNode.AddExposure(N.expo.ElementAt(i).radius, N.expo.ElementAt(i).exponent, 1, numWD);
                                
                                if (N.expo.ElementAt(i).Expo_Array != null)
                                {
                                    MemoryStream MS1 = new MemoryStream(N.expo.ElementAt(i).Expo_Array);
                                    newNode.expo[i].expo = (double[])bin.Deserialize(MS1);
                                }

                                if (N.expo.ElementAt(i).ExpoDist_Array != null) {
                                    MemoryStream MS2 = new MemoryStream(N.expo.ElementAt(i).ExpoDist_Array);
                                    newNode.expo[i].expoDist = (double[])bin.Deserialize(MS2);
                                }

                                if (N.expo.ElementAt(i).UW_Cross_Grade != null) {
                                    MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).UW_Cross_Grade);
                                    newNode.expo[i].UW_P10CrossGrade = (double[])bin.Deserialize(MS3);
                                }

                                if (N.expo.ElementAt(i).UW_ParallelGrade != null) {
                                    MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).UW_ParallelGrade);
                                    newNode.expo[i].UW_ParallelGrade = (double[])bin.Deserialize(MS3);
                                }

                                if (N.expo.ElementAt(i).SR_Array != null) {
                                    MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).SR_Array);
                                    newNode.expo[i].SR = (double[])bin.Deserialize(MS3);
                                }

                                if (N.expo.ElementAt(i).DH_Array != null) {
                                    MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).DH_Array);
                                    newNode.expo[i].dispH = (double[])bin.Deserialize(MS3);
                                }

                                newNode.expo[i].radius = N.expo.ElementAt(i).radius;
                                newNode.expo[i].exponent = N.expo.ElementAt(i).exponent;
                            }
                        }
                    }

                    var grid_db = from N in context.Node_table.Include("GridStats") where N.UTMX == UTMX & N.UTMY == UTMY select N;

                    foreach (var N in grid_db)
                    {
                        if (newNode.gridStats.stats == null) {
                            newNode.UTMX = N.UTMX;
                            newNode.UTMY = N.UTMY;
                            newNode.elev = N.elev;

                            try {
                                numRadii = N.GridStats.Count();
                            }
                            catch {
                                newNode.UTMX = 0;
                                newNode.UTMY = 0;
                                return newNode;
                            }

                            BinaryFormatter bin = new BinaryFormatter();
                            newNode.gridStats.stats = new Grid_Info.Grid_Avg_SD[numRadii];

                            for (int i = 0; i < numRadii; i++)
                            {
                                newNode.gridStats.stats[i].radius = N.GridStats.ElementAt(i).radius;

                                if (N.GridStats.ElementAt(i).P10_UW != null) {
                                    MemoryStream MS3 = new MemoryStream(N.GridStats.ElementAt(i).P10_UW);
                                    newNode.gridStats.stats[i].P10_UW = (double[])bin.Deserialize(MS3);
                                }

                                if (N.GridStats.ElementAt(i).P10_DW != null) {
                                    MemoryStream MS4 = new MemoryStream(N.GridStats.ElementAt(i).P10_DW);
                                    newNode.gridStats.stats[i].P10_DW = (double[])bin.Deserialize(MS4);
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.InnerException.ToString());                
                return newNode;
            }

            if (newNode.expo != null) {
                for (int i = 0; i < newNode.expo.Length; i++)
                    if (thisInst.topo.gotSR == true && newNode.expo[i].SR == null)
                    {
                        if (thisInst.topo.SR_ForCalcs == null)
                            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, true);
                        thisInst.topo.CalcSRDH(ref newNode.expo[i], newNode.UTMX, newNode.UTMY, newNode.expo[i].radius, newNode.expo[i].exponent, numWD);

                    }
                                                        
            }

            string[] metsUsed = new string[thisInst.metList.ThisCount];
            for (int i = 0; i < thisInst.metList.ThisCount; i++)
                metsUsed[i] = thisInst.metList.metItem[i].name;                       

            return newNode;
        }

        public Nodes[] GetNodes(double minX, double minY, double maxX, double maxY, Continuum thisInst, bool forGridStatCalcs)
        {
            // Searches database for nodes within specified Min/Max UTMX/Y and returns array of nodes
            Nodes[] nodesFromDB = new Nodes[0];
            
            int numRadii = thisInst.radiiList.ThisCount;
            int numWD = thisInst.metList.numWD;
            int newNodeCount = 0;

            string connString = GetDB_ConnectionString(thisInst.savedParams.savedFileName);
            //MessageBox.Show(thisInst.savedParams.savedFileName + "," + "GetNodes");
            BinaryFormatter bin = new BinaryFormatter();

            try
            {
                using (var context = new Continuum_EDMContainer(connString))
                {
                    var node_db = from N in context.Node_table.Include("expo") where N.UTMX >= minX & N.UTMX <= maxX & N.UTMY >= minY & N.UTMY <= maxY select N;

                    foreach (var N in node_db)
                    {                        
                        Array.Resize(ref nodesFromDB, newNodeCount + 1);
                        nodesFromDB[newNodeCount] = new Nodes();
                        nodesFromDB[newNodeCount].UTMX = N.UTMX;
                        nodesFromDB[newNodeCount].UTMY = N.UTMY;
                        nodesFromDB[newNodeCount].elev = N.elev;
                        nodesFromDB[newNodeCount].gridStats = new Grid_Info();
                        nodesFromDB[newNodeCount].gridStats.stats = new Grid_Info.Grid_Avg_SD[numRadii];

                        for (int i = 0; i < numRadii; i++)
                        {
                            nodesFromDB[newNodeCount].AddExposure(N.expo.ElementAt(i).radius, N.expo.ElementAt(i).exponent, 1, numWD);
                            nodesFromDB[newNodeCount].gridStats.stats = new Grid_Info.Grid_Avg_SD[numRadii];

                            if (N.expo.ElementAt(i).Expo_Array != null)
                            {
                                MemoryStream MS = new MemoryStream(N.expo.ElementAt(i).Expo_Array);
                                nodesFromDB[newNodeCount].expo[i].expo = (double[])bin.Deserialize(MS);
                                MS.Close();
                            }

                            if (N.expo.ElementAt(i).ExpoDist_Array != null)
                            {
                                MemoryStream MS = new MemoryStream(N.expo.ElementAt(i).ExpoDist_Array);
                                nodesFromDB[newNodeCount].expo[i].expoDist = (double[])bin.Deserialize(MS);
                                MS.Close();
                            }

                            if (N.expo.ElementAt(i).SR_Array != null)
                            {
                                MemoryStream MS = new MemoryStream(N.expo.ElementAt(i).SR_Array);
                                nodesFromDB[newNodeCount].expo[i].SR = (double[])bin.Deserialize(MS);
                                MS.Close();
                            }

                            if (N.expo.ElementAt(i).DH_Array != null)
                            {
                                MemoryStream MS = new MemoryStream(N.expo.ElementAt(i).DH_Array);
                                nodesFromDB[newNodeCount].expo[i].dispH = (double[])bin.Deserialize(MS);
                                MS.Close();
                            }

                            if (N.expo.ElementAt(i).UW_Cross_Grade != null)
                            {
                                MemoryStream MS = new MemoryStream(N.expo.ElementAt(i).UW_Cross_Grade);
                                nodesFromDB[newNodeCount].expo[i].UW_P10CrossGrade = (double[])bin.Deserialize(MS);
                                MS.Close();
                            }

                            if (N.expo.ElementAt(i).UW_ParallelGrade != null)
                            {
                                MemoryStream MS = new MemoryStream(N.expo.ElementAt(i).UW_ParallelGrade);
                                nodesFromDB[newNodeCount].expo[i].UW_ParallelGrade = (double[])bin.Deserialize(MS);
                                MS.Close();
                            }

                            nodesFromDB[newNodeCount].expo[i].radius = N.expo.ElementAt(i).radius;
                            nodesFromDB[newNodeCount].expo[i].exponent = N.expo.ElementAt(i).exponent;
                                                       
                            
                            if (nodesFromDB[newNodeCount].expo != null)
                            {
                                // This should never happen
                                if (thisInst.topo.gotSR == true && nodesFromDB[newNodeCount].expo[i].SR == null && forGridStatCalcs == false)
                                { // calculate SRDH then add to database
                                    thisInst.topo.CalcSRDH(ref nodesFromDB[newNodeCount].expo[i], nodesFromDB[newNodeCount].UTMX, nodesFromDB[newNodeCount].UTMY,
                                        nodesFromDB[newNodeCount].expo[i].radius, nodesFromDB[newNodeCount].expo[i].exponent, numWD);

                                    MemoryStream MS = new MemoryStream();

                                    bin.Serialize(MS, nodesFromDB[newNodeCount].expo[i].SR);
                                    N.expo.ElementAt(i).SR_Array = MS.ToArray();                                    
                                    
                                    bin.Serialize(MS, nodesFromDB[newNodeCount].expo[i].dispH);
                                    N.expo.ElementAt(i).DH_Array = MS.ToArray();                                    
                                    MS.Close();
                                }

                            }                                                      
                                                        
                        }
                        newNodeCount++;
                    }

                    for (int i = 0; i < newNodeCount; i++)
                    {
                        if (nodesFromDB[i].gridStats != null && forGridStatCalcs == false)
                        {
                            double thisX = nodesFromDB[i].UTMX;
                            double thisY = nodesFromDB[i].UTMY;
                            var grid_db = from G in context.Node_table.Include("GridStats") where G.UTMX == thisX & G.UTMY == thisY select G;
                            int grid_db_count = grid_db.Count();

                            foreach (var G in grid_db)
                            {
                                if (G.GridStats.Count() == numRadii)
                                {
                                    for (int r = 0; r < numRadii; r++)
                                    {
                                        nodesFromDB[i].gridStats.stats[r].radius = G.GridStats.ElementAt(r).radius;

                                        if (G.GridStats.ElementAt(r).P10_UW != null)
                                        {
                                            MemoryStream MS = new MemoryStream(G.GridStats.ElementAt(r).P10_UW);
                                            nodesFromDB[i].gridStats.stats[r].P10_UW = (double[])bin.Deserialize(MS);
                                            MS.Close();
                                        }

                                        if (G.GridStats.ElementAt(r).P10_DW != null)
                                        {
                                            MemoryStream MS = new MemoryStream(G.GridStats.ElementAt(r).P10_DW);
                                            nodesFromDB[i].gridStats.stats[r].P10_DW = (double[])bin.Deserialize(MS);
                                            MS.Close();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
                return nodesFromDB;
            }            

            return nodesFromDB;
        }

        /// <summary>
        /// Recalculates surface roughness and displacement height for all nodes in the database with new land cover key
        /// This function is not called. It was copied to BackgroundWorker in order to have a responsive progress bar 
        /// </summary>
        /// <param name="thisInst"></param>
        public void ReCalcNodeSRDH(Continuum thisInst)
        {                        
            int numWD = thisInst.metList.numWD;
            if (numWD == 0) return;
            
            Nodes nodeFromDB = new Nodes();
            string connString = GetDB_ConnectionString(thisInst.savedParams.savedFileName);
            MessageBox.Show(thisInst.savedParams.savedFileName + "," + "ReCalcNodeSRDH");
            
            try {
                using (var context = new Continuum_EDMContainer(connString)) {
                    var node_db = from N in context.Node_table.Include("expo") select N;

                    foreach (var N in node_db)
                    {
                        int numExpo = N.expo.Count;
                        nodeFromDB.UTMX = N.UTMX;
                        nodeFromDB.UTMY = N.UTMY;

                        nodeFromDB.expo = new Exposure[numExpo];

                        for (int i = 0; i < numExpo; i++)
                        {
                            nodeFromDB.expo[i] = new Exposure();
                            nodeFromDB.expo[i].radius = N.expo.ElementAt(i).radius;
                            nodeFromDB.expo[i].exponent = N.expo.ElementAt(i).exponent;
                            int numSectors = 1;
                            int smallerRadius = thisInst.topo.GetSmallerRadius(nodeFromDB.expo, nodeFromDB.expo[i].radius, nodeFromDB.expo[i].exponent, numSectors);

                            if (smallerRadius == 0 || numSectors > 1)
                            { // when sector avg is used, can//t add on to exposure calcs...so gotta do it the long way
                                if (thisInst.topo.gotSR == true)
                                    thisInst.topo.CalcSRDH(ref nodeFromDB.expo[i], nodeFromDB.UTMX, nodeFromDB.UTMY, nodeFromDB.expo[i].radius, nodeFromDB.expo[i].exponent, thisInst.metList.numWD);
                            }
                            else
                            {
                                Exposure smallerExpo = thisInst.topo.GetSmallerRadiusExpo(nodeFromDB.expo, smallerRadius, nodeFromDB.expo[i].exponent, numSectors);
                                if (thisInst.topo.gotSR == true)
                                    thisInst.topo.CalcSRDHwithSmallerRadius(ref nodeFromDB.expo[i], nodeFromDB.UTMX, nodeFromDB.UTMY, nodeFromDB.expo[i].radius, nodeFromDB.expo[i].exponent, numSectors, smallerRadius, smallerExpo, thisInst.metList.numWD);

                            }

                            // Write back to expo_db
                            MemoryStream MS7 = new MemoryStream();
                            MemoryStream MS8 = new MemoryStream();
                            BinaryFormatter bin = new BinaryFormatter();

                            if (nodeFromDB.expo[i].SR != null)
                            {
                                bin.Serialize(MS7, nodeFromDB.expo[i].SR);
                                N.expo.ElementAt(i).SR_Array = MS7.ToArray();
                            }

                            if (nodeFromDB.expo[i].dispH != null)
                            {
                                bin.Serialize(MS8, nodeFromDB.expo[i].dispH);
                                N.expo.ElementAt(i).DH_Array = MS8.ToArray();
                            }
                        }
                    }
                                        
                    context.SaveChanges(); // Save DB                    
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.InnerException.ToString());                
                return;
            }

        }

        public Nodes[] GetPathOfNodesFromUTMs(Node_UTMs[] pathOfNodesUTMs, Continuum thisInst)
        {
            // Returns path of nodes using path of UTM coords
            Nodes[] pathOfNodes = null;
            int numWD = thisInst.metList.numWD;
            
            if (pathOfNodesUTMs == null)
                return pathOfNodes;

            int numNodes = pathOfNodesUTMs.Length;
            string connString = GetDB_ConnectionString(thisInst.savedParams.savedFileName);
            
            try {
                using (var context = new Continuum_EDMContainer(connString)) {
                    for (int nodeInd = 0; nodeInd < numNodes; nodeInd++) {
                        Array.Resize(ref pathOfNodes, nodeInd + 1);
                        pathOfNodes[nodeInd] = GetANode(pathOfNodesUTMs[nodeInd].UTMX, pathOfNodesUTMs[nodeInd].UTMY, thisInst);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.InnerException.ToString());
                return pathOfNodes;
            }

            for (int nodeInd = 0; nodeInd < numNodes; nodeInd++)
            {
                if (pathOfNodes[nodeInd].expo != null)
                {
                    for (int i = 0; i < pathOfNodes[nodeInd].expo.Length; i++)
                    {
                        if (thisInst.topo.gotSR == true && pathOfNodes[nodeInd].expo[i].SR == null)
                            thisInst.topo.CalcSRDH(ref pathOfNodes[nodeInd].expo[i], pathOfNodes[nodeInd].UTMX, pathOfNodes[nodeInd].UTMY, pathOfNodes[nodeInd].expo[i].radius,
                                pathOfNodes[nodeInd].expo[i].exponent, numWD);
                    }                    
                }
                                
            }

            return pathOfNodes;
        }

        /// <summary>
        /// Finds and returns a path of nodes between start and end node
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <param name="model"></param>
        /// <param name="thisInst"></param>
        /// <returns></returns>
        public Nodes[] FindPathOfNodes(Nodes startNode, Nodes endNode, Model model, Continuum thisInst)
        {  
            Nodes[] nodePath1to2 = null;
                       
            bool madeItToTarget = false;
            int radiusStep = 100000;
            int lastStepSize = 0;
            double minDir = 100;
            double maxDir = 100;

            int numMets = thisInst.metList.ThisCount;
            double[] windRose = thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, model.timeOfDay, model.season);
            int radiusIndex = thisInst.radiiList.GetRadiusInd(model.radius);

            bool foundNode = false;
            Nodes nextNode = new Nodes();
            Nodes lastNode = new Nodes();

            Nodes[] nodesFromStart = new Nodes[1];
            nodesFromStart[0] = startNode;
            int startInd = 0;
            
            Nodes[] nodesFromEnd = new Nodes[1];
            nodesFromEnd[0] = endNode;
            int endInd = 0;

            bool isTerrainSame = TerrainSame(startNode, endNode, model, 1.0, windRose, radiusIndex);
            bool isElevClose = ElevClose(startNode, endNode, model, 1);                       

            Nodes targetNode = endNode;
            double distToTarget = thisInst.topo.CalcDistanceBetweenPoints(nodesFromStart[0].UTMX, nodesFromStart[0].UTMY, targetNode.UTMX, targetNode.UTMY);

            if ((isTerrainSame == true && isElevClose == true) || distToTarget < 500)
                // No nodes needed in between start and end mets
                nodePath1to2 = null;
            else {
                while (madeItToTarget == false)
                {
                    bool goToNextStep = false;
                    double stepSplit = 1;
                    double adjFac = 1;

                    // First find node starting with last node in nodesFromStart array
                    while (goToNextStep == false)
                    {
                        if (stepSplit >= 16 || radiusStep < 500)
                            adjFac = adjFac * 1.5f;                                               

                        startNode = nodesFromStart[startInd];
                        targetNode = nodesFromEnd[endInd];

                        distToTarget = thisInst.topo.CalcDistanceBetweenPoints(startNode.UTMX, startNode.UTMY, targetNode.UTMX, targetNode.UTMY);

                        if (lastStepSize == 0 || lastStepSize * 2 > distToTarget)
                            radiusStep = Convert.ToInt32(distToTarget / (2 * stepSplit));
                        else
                            radiusStep = Convert.ToInt32((lastStepSize * 2 / stepSplit));

                        if (radiusStep > 7500)
                            radiusStep = 7500;

                        if (startInd > 100) {
                            Array.Resize(ref nodePath1to2, 200);
                            return nodePath1to2;
                        }
                        
                        // Find direction sector with the lowest grade
                        if (startInd > 0)
                            lastNode = nodesFromStart[startInd - 1];

                        double minSlopeDir = GetWD_MinGrade(startNode, targetNode, radiusStep, thisInst.topo);
                        minDir = minSlopeDir - 30;
                        maxDir = minSlopeDir + 30;

                        nextNode = FindNodeInSectorHighSpot(thisInst, startNode, minDir, maxDir, radiusStep, (int)(radiusStep * 0.7));

                        if (nextNode.UTMX == 0 && nextNode.UTMY == 0)
                            foundNode = false;
                        else
                            foundNode = true;

                        // Check to see if next node is similar enough to start node in terms of terrain complex and elev
                        if (foundNode == true) {
                            isTerrainSame = TerrainSame(startNode, nextNode, model, adjFac, windRose, radiusIndex);
                            isElevClose = ElevClose(startNode, nextNode, model, adjFac);
                        }

                        if (foundNode == true && ((isTerrainSame == true && isElevClose == true) || radiusStep < 500))
                        { // found next node to use
                            if (thisInst.topo.useSepMod == true && nextNode.expo != null)
                                nextNode.flowSepNodes = FindAllFlowSeps(nextNode, thisInst, thisInst.metList.numWD);
                            
                            startInd++;
                            Array.Resize(ref nodesFromStart, startInd + 1);
                            nodesFromStart[startInd] = nextNode;
                          //  nodesFromStart[startInd].CalcP10UW_Grade(thisInst.topo, radiiList, savedFileName);
                            goToNextStep = true;
                            lastStepSize = radiusStep;
                        }
                        else 
                            stepSplit = stepSplit * 2;                        

                    }

                    // Check to see if node path is complete
                    // Compare terrain and elevation between new node and newest node found from end (or end met)
                    adjFac = 1;                    
                    isTerrainSame = TerrainSame(nextNode, nodesFromEnd[endInd], model, adjFac, windRose, radiusIndex);
                    isElevClose = ElevClose(nextNode, nodesFromEnd[endInd], model, adjFac);
                    distToTarget = thisInst.topo.CalcDistanceBetweenPoints(nextNode.UTMX, nextNode.UTMY, nodesFromEnd[endInd].UTMX, nodesFromEnd[endInd].UTMY);
                    
                    if ((isTerrainSame == true && isElevClose == true) || distToTarget < 500)
                        madeItToTarget = true;

                    goToNextStep = false;
                    stepSplit = 1;
                    
                    if (madeItToTarget == false)
                    { // find node from last end node with last start node as new target
                        while (goToNextStep == false)
                        {
                            if (stepSplit >= 16)
                                adjFac = adjFac * 1.5f;                                                       

                            startNode = nodesFromEnd[endInd];
                            targetNode = nodesFromStart[startInd];

                            distToTarget = thisInst.topo.CalcDistanceBetweenPoints(startNode.UTMX, startNode.UTMY, targetNode.UTMX, targetNode.UTMY);

                            if (lastStepSize == 0 || lastStepSize * 2 > distToTarget)
                                radiusStep = Convert.ToInt16(distToTarget / (2 * stepSplit));
                            else
                                radiusStep = Convert.ToInt16(lastStepSize * 2 / stepSplit);

                            if (radiusStep > 7500)
                                radiusStep = 7500;
                            
                            if (endInd > 100)
                            {
                                Array.Resize(ref nodePath1to2, 200);
                                return nodePath1to2;
                            }

                            // Find direction sector with the lowest grade

                            if (endInd > 0)
                                lastNode = nodesFromEnd[endInd - 1];

                            double minSlopeDir = GetWD_MinGrade(startNode, targetNode, radiusStep, thisInst.topo);
                            minDir = minSlopeDir - 30;
                            maxDir = minSlopeDir + 30;

                            nextNode = FindNodeInSectorHighSpot(thisInst, startNode, minDir, maxDir, radiusStep, (int)(radiusStep * 0.7));

                            if (nextNode.UTMX == 0 && nextNode.UTMY == 0)
                                foundNode = false;
                            else
                                foundNode = true;
                            
                            // Check to see if next node is similar enough to start node in terms of terrain complex and elev
                            if (foundNode == true) {
                                isTerrainSame = TerrainSame(startNode, nextNode, model, adjFac, windRose, radiusIndex);
                                isElevClose = ElevClose(startNode, nextNode, model, adjFac);
                            }

                            if (foundNode = true && ((isTerrainSame == true && isElevClose == true) || radiusStep < 500))
                            { // found next node to use
                                if (thisInst.topo.useSepMod == true && nextNode.expo != null)
                                    nextNode.flowSepNodes = FindAllFlowSeps(nextNode, thisInst, thisInst.metList.numWD);

                                endInd++;
                                Array.Resize(ref nodesFromEnd, endInd + 1);
                                nodesFromEnd[endInd] = nextNode;
                             //   nodesFromEnd[endInd].CalcP10UW_Grade(topo, radiiList, savedFileName);
                                goToNextStep = true;
                                lastStepSize = radiusStep;
                            }
                            else 
                                stepSplit = stepSplit * 2;
                            
                        }

                        // Check to see if node path is complete
                        // Compare terrain and elevation between new node and newest node found from end (or end met)
                        adjFac = 1;
                        
                        isTerrainSame = TerrainSame(nextNode, nodesFromStart[startInd], model, adjFac, windRose, radiusIndex);
                        isElevClose = ElevClose(nextNode, nodesFromStart[startInd], model, adjFac);
                        distToTarget = thisInst.topo.CalcDistanceBetweenPoints(nextNode.UTMX, nextNode.UTMY, nodesFromStart[startInd].UTMX, nodesFromStart[startInd].UTMY);

                        if ((isTerrainSame == true && isElevClose == true) || distToTarget < 500)
                            madeItToTarget = true;

                    }
                }
            }

            // combine node arrays into one
            int numNodes = startInd + endInd;
            Array.Resize(ref nodePath1to2, numNodes);
                        
            for (int i = 1; i <= startInd; i++)
                nodePath1to2[i - 1] = nodesFromStart[i];

            for (int i = 1; i <= endInd; i++)
                nodePath1to2[numNodes - i] = nodesFromEnd[i];

            return nodePath1to2;

        }

        /// <summary>
        /// Calculates the average slope of terrain from the start node to the target node within specified radius
        /// and returns the WD sector with minimum grade
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="targetNode"></param>        
        /// <param name="topo"></param>   
        /// <param name="maxRadius"></param>
        /// <returns></returns>
        public double GetWD_MinGrade(Nodes startNode, Nodes targetNode, int maxRadius, TopoInfo topo)
        {
          
            bool posGrade; // If true then looking for minimum positive grade   
            if (targetNode.elev > startNode.elev)
                posGrade = true;
            else
                posGrade = false;

            double orientStartToTarget = topo.GetDirection(targetNode.UTMX - startNode.UTMX, targetNode.UTMY - startNode.UTMY);            
            double minDirSec = orientStartToTarget - 45; 
            double maxDirSec = orientStartToTarget + 45;

            int radiusReso = 100; // Radius step size
            int dirStep = 2; // Direction step size

            Check_class check = new Check_class();
            Nodes thisNode = new Nodes();
            double minPosSlope = 1000;
            double minPosSlopeDir = 1000;
            double minNegSlope = 1000;
            double minNegSlopeDir = 1000;

            // Loop through direction and through radius
            for (double i = minDirSec; i <= maxDirSec; i = i + dirStep)
            {
                double avgSlope = 0;
                int count = 0;

                for (double j = radiusReso; j <= maxRadius; j = j + radiusReso)
                {
                    thisNode.UTMX = startNode.UTMX + j * Math.Cos((90 - i) * Math.PI / 180);
                    thisNode.UTMY = startNode.UTMY + j * Math.Sin((90 - i) * Math.PI / 180); 

                    int nodeOk = check.NewNodeCheck(topo, thisNode.UTMX, thisNode.UTMY, 0, "Calcs"); // Min distance is zero since we just need an elevation at this location

                    if (nodeOk == 100)
                    {
                        thisNode.elev = topo.CalcElevs(thisNode.UTMX, thisNode.UTMY); 
                        double rise = thisNode.elev - startNode.elev;
                        double runLength = topo.CalcDistanceBetweenPoints(thisNode.UTMX, thisNode.UTMY, startNode.UTMX, startNode.UTMY);

                        if (runLength > 0)
                        {
                            avgSlope = avgSlope + 100 * (rise / runLength);
                            count++;
                        }
                    }
                }

                if (count > 0)
                {
                    avgSlope = avgSlope / count;

                    if (avgSlope < 0) // negative slope
                    {
                        if (Math.Abs(avgSlope) < Math.Abs(minNegSlope))
                        {
                            minNegSlope = avgSlope;
                            minNegSlopeDir = i;
                        }                        
                    }
                    else // positive slope
                    {
                        if (avgSlope < minPosSlope)
                        {
                            minPosSlope = avgSlope;
                            minPosSlopeDir = i;
                        }
                    }
                }
            }

            if (posGrade == true && minPosSlope != 1000)
                return minPosSlopeDir;
            else if (posGrade == true && minPosSlope == 1000)
                return minNegSlopeDir;
            else if (posGrade == false && minNegSlope != 1000)
                return minNegSlopeDir;
            else // if (posGrade == false && minNegSlope == 1000)
                return minPosSlopeDir;            
        }


        public double Calc_Dist(Nodes node1, Nodes node2)
        {
            // Calculates and returns distance between the two nodes
            double dist = Math.Sqrt((node1.UTMX - node2.UTMX) * (node1.UTMX - node2.UTMX) + (node1.UTMY - node2.UTMY) * (node1.UTMY - node2.UTMY));
            return dist;
        }


        /*       public MinMax_XY GetMinMax_X_Y(Nodes startNode, double minDir, double maxDir, int radiusStep, TopoInfo topo)
               {
                   // Returns min and max UTMX/Y of search area with specified min/max WD sector and radius
                   MinMax_XY thisMinMax_XY = new MinMax_XY();

                   if (minDir >= 0 && minDir < 180)
                       thisMinMax_XY.minX = startNode.UTMX + radiusStep * Math.Abs(Math.Cos((minDir + 90) * Math.PI / 180));
                   else
                       thisMinMax_XY.minX = startNode.UTMX - radiusStep * Math.Abs(Math.Cos((minDir + 90) * Math.PI / 180));                   

                   if (minDir >= 90 && minDir < 270)
                       thisMinMax_XY.minY = startNode.UTMY - radiusStep * Math.Abs(Math.Sin((minDir + 90) * Math.PI / 180));
                   else
                       thisMinMax_XY.minY = startNode.UTMY + radiusStep * Math.Abs(Math.Sin((minDir + 90) * Math.PI / 180));

                   if (maxDir >= 0 && maxDir < 180)
                       thisMinMax_XY.maxX = startNode.UTMX + radiusStep * Math.Abs(Math.Cos((maxDir + 90) * Math.PI / 180));
                   else
                       thisMinMax_XY.maxX = startNode.UTMX - radiusStep * Math.Abs(Math.Cos((maxDir + 90) * Math.PI / 180));

                   if (maxDir >= 90 && maxDir < 270)
                       thisMinMax_XY.maxY = startNode.UTMY - radiusStep * Math.Abs(Math.Sin((maxDir + 90) * Math.PI / 180));
                   else
                       thisMinMax_XY.maxY = startNode.UTMY + radiusStep * Math.Abs(Math.Sin((maxDir + 90) * Math.PI / 180));

                   if (thisMinMax_XY.minX > thisMinMax_XY.maxX)
                   {
                       double tempX = thisMinMax_XY.minX;
                       thisMinMax_XY.minX = thisMinMax_XY.maxX;
                       thisMinMax_XY.maxX = tempX;
                   }

                   if (thisMinMax_XY.minX < startNode.UTMX && thisMinMax_XY.maxX < startNode.UTMX)
                       thisMinMax_XY.maxX = startNode.UTMX;
                   else if (thisMinMax_XY.minX > startNode.UTMX && thisMinMax_XY.maxX > startNode.UTMX)
                       thisMinMax_XY.minX = startNode.UTMX;

                   if (thisMinMax_XY.minY > thisMinMax_XY.maxY)
                   {
                       double tempY = thisMinMax_XY.minY;
                       thisMinMax_XY.minY = thisMinMax_XY.maxY;
                       thisMinMax_XY.maxY = tempY;
                   }

                   if (thisMinMax_XY.minY < startNode.UTMY && thisMinMax_XY.maxY < startNode.UTMY)
                       thisMinMax_XY.maxY = startNode.UTMY;
                   else if (thisMinMax_XY.minY > startNode.UTMY && thisMinMax_XY.maxY > startNode.UTMY)
                       thisMinMax_XY.minY = startNode.UTMY;

                   TopoInfo.TopoGrid minXY = topo.GetClosestNode(thisMinMax_XY.minX, thisMinMax_XY.minY, "Topography");
                   thisMinMax_XY.minX = (int)minXY.UTMX;
                   thisMinMax_XY.minY = (int)minXY.UTMY;

                   TopoInfo.TopoGrid maxXY = topo.GetClosestNode(thisMinMax_XY.maxX, thisMinMax_XY.maxY, "Topography");
                   thisMinMax_XY.maxX = (int)maxXY.UTMX;
                   thisMinMax_XY.maxY = (int)maxXY.UTMY;

                   return thisMinMax_XY;

               }

               /// <summary>
               /// 
               /// </summary>
               /// <param name="startNode"></param>
               /// <param name="minDir"></param>
               /// <param name="maxDir"></param>
               /// <param name="radiusStep"></param>
               /// <param name="topo"></param>
               /// <returns></returns>
               public MinMax_XY GetMinMax_X_Y_NEW(Nodes startNode, double minDir, double maxDir, int radiusStep, TopoInfo topo)
               {
                   // Returns min and max UTMX/Y of search area with specified min/max WD sector and radius
                   MinMax_XY thisMinMax_XY = new MinMax_XY();

                   thisMinMax_XY.minX = startNode.UTMX + radiusStep * Math.Cos((90 - minDir) * Math.PI / 180);            
                   thisMinMax_XY.minY = startNode.UTMY + radiusStep * Math.Sin((90 - minDir) * Math.PI / 180);

                   thisMinMax_XY.maxX = startNode.UTMX + radiusStep * Math.Cos((90 - maxDir) * Math.PI / 180);            
                   thisMinMax_XY.maxY = startNode.UTMY + radiusStep * Math.Sin((90 - maxDir) * Math.PI / 180);

                   TopoInfo.TopoGrid minXY = topo.GetClosestNodeFixedGrid(thisMinMax_XY.minX, thisMinMax_XY.minY, 250, 12000);
                   thisMinMax_XY.minX = (int)minXY.UTMX;
                   thisMinMax_XY.minY = (int)minXY.UTMY;

                   TopoInfo.TopoGrid maxXY = topo.GetClosestNodeFixedGrid(thisMinMax_XY.maxX, thisMinMax_XY.maxY, 250, 12000);
                   thisMinMax_XY.maxX = (int)maxXY.UTMX;
                   thisMinMax_XY.maxY = (int)maxXY.UTMY;

                   return thisMinMax_XY;

               }
       */
        /// <summary>
        /// Finds and returns a node with highest elvation within min/max WD and min/max radius
        /// </summary>
        /// <param name="thisInst"></param>
        /// <param name="startNode"></param>
        /// <param name="minDir"></param>
        /// <param name="maxDir"></param>
        /// <param name="maxRadius"></param>
        /// <param name="minRadius"></param>        
        /// <returns></returns>
        public Nodes FindNodeInSectorHighSpot(Continuum thisInst, Nodes startNode, double minDir, double maxDir, int maxRadius, int minRadius)
        {             
            Nodes highNode = new Nodes();
            Nodes thisNode = new Nodes();
            Check_class check = new Check_class();
            
            int reso = 250;            
            
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

                        if (highNode == null || thisNode.elev > highNode.elev)
                        {
                            highNode.UTMX = thisNode.UTMX;
                            highNode.UTMY = thisNode.UTMY;
                            highNode.elev = thisNode.elev;
                        }
                    }
                    
                }
            }
            
            // Do exposure and grid stat calculations at high node
            // First check to see if it has already been calculated
            bool foundHighNode = false;

            if (highNode.UTMX != 0 && highNode.UTMY != 0)
            {
                thisNode = GetANode(highNode.UTMX, highNode.UTMY, thisInst);

                if (thisNode.UTMX == highNode.UTMX && thisNode.UTMY == highNode.UTMY)
                {
                    foundHighNode = true;
                    highNode = thisNode;
                    if (highNode.gridStats.StatCount == 0)
                    {
                        highNode.gridStats = new Grid_Info();
                        highNode.CalcGridStatsAndExposures(thisInst);
                        AddNodeOrUpdateNodeGridStat(highNode, thisInst.savedParams.savedFileName);
                    }
                }

                if (foundHighNode == false && highNode.UTMX != 0 && highNode.UTMY != 0)
                {
                    highNode.gridStats = new Grid_Info();
                    highNode.CalcGridStatsAndExposures(thisInst);
                    AddNodeOrUpdateNodeGridStat(highNode, thisInst.savedParams.savedFileName);
                }
            }

            return highNode;
        }


        public Sep_Nodes[] GetSepNodes1and2(Sep_Nodes[] flowSep1, Sep_Nodes[] flowSep2, int WD_Ind)
        {
            // Returns flow separation nodes for specified WD sector
            Sep_Nodes[] pairSepNodes = new Sep_Nodes[2];

            int numSep1;
            int numSep2;

            if (flowSep1 == null)
                numSep1 = 0;
            else
                numSep1 = flowSep1.Length;


            if (flowSep2 == null)
                numSep2 = 0;
            else
                numSep2 = flowSep2.Length;


            for (int i = 0; i < numSep1; i++)
            {
                if (flowSep1[i].flowSepWD == WD_Ind)
                {
                    pairSepNodes[0] = flowSep1[i];
                    break;
                }

            }

            for (int i = 0; i < numSep2; i++)
            {
                if (flowSep2[i].flowSepWD == WD_Ind)
                {
                    pairSepNodes[1] = flowSep2[i];
                    break;
                }
            }

            return pairSepNodes;
        }


        public Sep_Nodes[] FindAllFlowSeps(Nodes thisNode, Continuum thisInst, int numWD)
        {

            // Returns Flow_sep nodes for every WD where UW is < 0 and DW > 0 
            int numSepNodes = 0;
            Nodes highNode = new Nodes();
            Nodes endZoneNode = new Nodes();
            int turbZoneLength;
            Model thisModel = new Model();
            int minDir;
            int maxDir;
            double sumUWDW;
            double distToSep;
            Sep_Nodes[] flowSepNodes = null;
            MetCollection metList = thisInst.metList;
            ModelCollection modelList = thisInst.modelList;
            string savedFileName = thisInst.savedParams.savedFileName;
            TopoInfo topo = thisInst.topo;
            InvestCollection radiiList = thisInst.radiiList;

            // Check to see if either met is upwind of flow separation

            for (int WD = 0; WD < numWD; WD++)
            {
                if (thisNode.expo[3].expo[WD] < 0 && thisNode.expo[0].GetDW_Param(WD, "Expo") > 0)
                { // if ( UW expo @ 10,000 m < 0 and DW expo @ 4000 m > 0 ) { flow sep could occur. 
                  // The idea is to look further UW for point of separation and use shorter length DW to ensure downhill flow (i.e. it could start to slope up at greater distances and have a negative DW expo but still be in turbulent zone)

                    // Find node at highest point
                    minDir = (int)(WD * (double)360 / numWD - 5);
                    if (minDir < 0) minDir = minDir + 360;
                    maxDir = (int)(WD * (double)360 / numWD + 5);
                    if (maxDir > 360) maxDir = maxDir - 360;
                    Nodes[] blankNodes = null;

                    highNode = FindNodeInSectorHighSpot(thisInst, thisNode, minDir, maxDir, 5000, 0);
                    sumUWDW = Math.Abs(highNode.expo[3].expo[WD]) + highNode.expo[0].GetDW_Param(WD, "Expo");
                    Array.Resize(ref flowSepNodes, numSepNodes + 1);

                    flowSepNodes[numSepNodes].highNode = highNode;

                    turbZoneLength = (int)thisModel.CalcTurbulentZoneLength(sumUWDW);
                    distToSep = Calc_Dist(thisNode, highNode);

                    flowSepNodes[numSepNodes].flowSepWD = WD;

                    if (distToSep > turbZoneLength)
                    { // outside of turbulent zone
                        endZoneNode = FindNodeInSectorHighSpot(thisInst, thisNode, minDir, maxDir, (int)((distToSep - turbZoneLength) * 1.1), 
                            (int)((distToSep - turbZoneLength) * 0.9));
                        flowSepNodes[numSepNodes].turbEndNode = endZoneNode;

                        if (endZoneNode.UTMX == 0)                          
                            flowSepNodes[numSepNodes].turbEndNode = null;
                        
                    }
                    
                    numSepNodes++;

                }
            }

            return flowSepNodes;

        }

        /// <summary>
        /// Returns true if elevation difference between two nodes is lower than max allowed
        /// </summary>        
        /// <returns></returns>
        public bool ElevClose(Nodes node1, Nodes node2, Model model, double adjFac)
        {            
            bool isClose = false;
            double maxElevDiff = adjFac * model.maxElevDiff;
            double elevDiff = Math.Abs(node1.elev - node2.elev);

            if (elevDiff > maxElevDiff)
                isClose = false;
            else
                isClose = true;

            return isClose;

        }

        /// <summary>
        /// Compares the terrain complexity (P10 Exposure) at two sites and returns true if P10 exposure difference between two nodes 
        /// is lower than max allowed
        /// </summary>

        /// <returns></returns>
        public bool TerrainSame(Nodes node1, Nodes node2, Model model, double adjFac, double[] windRose, int radiusIndex)
        {            
            bool isSame = false;

            double P10_UW_1 = node1.gridStats.GetOverallP10(windRose, radiusIndex, "UW");
            double P10_UW_2 = node2.gridStats.GetOverallP10(windRose, radiusIndex, "UW");
            double P10_DW_1 = node1.gridStats.GetOverallP10(windRose, radiusIndex, "DW");
            double P10_DW_2 = node2.gridStats.GetOverallP10(windRose, radiusIndex, "DW");

            double avgP10UW = Math.Abs(P10_UW_1 + P10_UW_2) / 2;
            double avgP10DW = Math.Abs(P10_DW_1 + P10_DW_2) / 2;

            double maxUW_Diff = avgP10UW * model.maxP10DiffSlope + adjFac * model.maxP10DiffOffset;
            double maxDW_Diff = avgP10DW * model.maxP10DiffSlope + adjFac * model.maxP10DiffOffset;

            double UW_Diff = Math.Abs(P10_UW_1 - P10_UW_2);
            double DW_Diff = Math.Abs(P10_DW_1 - P10_DW_2);

            // Only the highest Avg P10 must meet the max change criteria (since the coeffs are calculated from Max P10)
            if (P10_UW_1 < 4 && P10_UW_2 < 4 && P10_DW_1 < 4 && P10_DW_2 < 4)
                isSame = true;
            else if (avgP10UW > avgP10DW)
            {
                if (UW_Diff < maxUW_Diff)
                    isSame = true;
            }
            else {
                if (DW_Diff < maxDW_Diff)
                    isSame = true;
            }

            return isSame;
        }           

        public void ClearExposGridStatsFromDB(Continuum thisInst)
        {
            // Clears all exposure and grid stat calculations from database. Called when the number of WD bins is changed on the MCP tab

            string connString = GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            try
            {
                using (var ctx = new Continuum_EDMContainer(connString))
                {                    
                    ctx.Database.ExecuteSqlCommand("DELETE FROM GridStat_table");
                    ctx.Database.ExecuteSqlCommand("DELETE FROM Expo_table");
                    ctx.Database.ExecuteSqlCommand("DELETE FROM Node_table");

                    ctx.SaveChanges();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
                            
