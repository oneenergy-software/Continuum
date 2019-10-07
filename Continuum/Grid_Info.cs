////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Grid_Info.cs
//
// summary:	Implements the grid information class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ContinuumNS
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   (Serializable) information about the grid. </summary>
    ///
    /// <remarks>   OEE, 7/15/2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Serializable()]
    public class Grid_Info
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// radius of investigation to use in grid statistics (terrain complexity) calculation.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int gridRadius = 750;
        /// <summary>   Grid resolution to use in grid statistics. </summary>
        public int reso = 250;
        /// <summary>   Terrain complexity statistics. </summary>
        public Grid_Avg_SD[] stats;

        
        /// <summary>  Holds the radius of investigation and arrays of upwind and downwind P10 exposures. </summary>        
        [Serializable()]
        public struct Grid_Avg_SD
        {
            /// <summary>   Radius of investigation. </summary>
            public int radius;
            /// <summary>   Upwind P10 terrain exposure by wind direction. </summary>
            public double[] P10_UW;
            /// <summary>   Downwind P10 terrain exposure by wind direction. </summary>
            public double[] P10_DW;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the number of stats. </summary>
        ///
        /// <value> The number of stats. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int StatCount
        {
            get { if (stats == null)
                    return 0;
                  else
                    return stats.Length; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds the statistics. </summary>
        ///
        /// <remarks>   OEE, 7/15/2019. </remarks>
        ///
        /// <param name="radius">   The radius. </param>
        /// <param name="P10_UW">   The 10 uw. </param>
        /// <param name="P10_DW">   The 10 double-word. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddStats(int radius, double[] P10_UW, double[] P10_DW)
        {
            // Adds Grid_Avg_SD to list of stats

            // First check to see if stats have already been calculated
            
            int numStats = 0;

            try {
                numStats = stats.Length;
            }
            catch  {
                numStats = 0;
            }

            if (numStats > 0)
            {
                for (int i = 0; i < numStats; i++)
                    if (stats[i].radius == radius)
                    {                        
                        MessageBox.Show("Grid stats have already been calculated at this radius and exponent.", "Continuum 2.2");
                        return;
                    }
            }
            
            int numWD = P10_UW.Length;

            Array.Resize(ref stats, numStats + 1);

            stats[numStats] = new Grid_Avg_SD();
            stats[numStats].radius = radius;

            Array.Resize(ref stats[numStats].P10_UW, numWD);
            Array.Resize(ref stats[numStats].P10_DW, numWD);

            for (int i = 0; i < numWD; i++)
            {
                stats[numStats].P10_UW[i] = P10_UW[i];
                stats[numStats].P10_DW[i] = P10_DW[i];
            }

        }
                
        /// <summary>   Finds wind direction sectors with the highest frequency and that account for 60% of wind rose. 
        /// Returns array of boolean with the top sectors flagged as true. Used to define the grid for P10 calculations. </summary>        
        public bool[] FindSectorsForGrid(double[] windRose)
        {            
            int numWD = 0;

            try {
                numWD = windRose.Length;
            }
            catch {
                MessageBox.Show("Error occurred while trying to find sectors in grid area", "Continuum 2.2");
                return null;
            }

            int midRose = numWD / 2;
            double sectSum = 0;             
            int sectInd = 0;
            double lastFreq = 100;
             
            bool[] sectorsToUse = new bool[numWD];

            while (sectSum < 0.6)
            {
                double sectFreq = 0;
                for (int i = 0; i < numWD; i++)
                {
                    double thisFreq = windRose[i];

                    if (thisFreq > sectFreq && thisFreq < lastFreq)
                    {
                        sectFreq = thisFreq;
                        sectInd = i;
                    }
                }

                lastFreq = sectFreq;
                sectorsToUse[sectInd] = true;

                // DW sector
                if (sectInd >= midRose)
                    sectorsToUse[sectInd - midRose] = true;
                else
                    sectorsToUse[sectInd + midRose] = true;

                sectSum = sectSum + sectFreq;

            }

            return sectorsToUse;
        }

        /// <summary>  Returns array of grid points surrounding met/node for P10 exposure calculation. </summary>
        public TopoInfo.TopoGrid[] GetGridArray(double UTMX, double UTMY, Continuum thisInst) // 
        {                        
            double minX = UTMX - gridRadius;
            double minY = UTMY - gridRadius;

            double maxX = UTMX + gridRadius;
            double maxY = UTMY + gridRadius;

            // Find closest nodes to minX and minY and maxX and maxY with a minimum distance of 10 km from edge of topography 
            TopoInfo.TopoGrid closestNodeToMin = thisInst.topo.GetClosestNodeFixedGrid(minX, minY, reso, 10000);
            TopoInfo.TopoGrid closestNodeToMax = thisInst.topo.GetClosestNodeFixedGrid(maxX, maxY, reso, 10000);

            minX = closestNodeToMin.UTMX;
            minY = closestNodeToMin.UTMY;
            maxX = closestNodeToMax.UTMX;
            maxY = closestNodeToMax.UTMY;

            Check_class check = new Check_class();

            int gridCount = 0;
            TopoInfo.TopoGrid[] gridArray = new TopoInfo.TopoGrid[1];

            double dirBinSize = (double)360 / thisInst.metList.numWD;
            
            double[] windRose = thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
            bool[] sectorsToUse = FindSectorsForGrid(windRose);
            
            for (double i = minX; i <= maxX; i = i + reso)
            {
                for (double j = minY; j <= maxY; j = j + reso)
                {
                    double dist = thisInst.topo.CalcDistanceBetweenPoints(i, j, UTMX, UTMY);
                    double deltaX = i - UTMX;
                    double deltaY = j - UTMY;
                    int dirInd = thisInst.topo.CalcDirInd(deltaX, deltaY, dirBinSize);
                    int goodToGo = check.NewNodeCheck(thisInst.topo, i, j, 10000);
                  //  bool gridOk = thisInst.topo.newNode(i, j, thisInst.radiiList, gridRadius);

                    if (goodToGo == 100 && ((dist < gridRadius && sectorsToUse[dirInd] == true) || dist < gridRadius / 2))
                    {
                        gridCount = gridCount + 1;
                        Array.Resize(ref gridArray, gridCount);

                        gridArray[gridCount - 1].UTMX = i;
                        gridArray[gridCount - 1].UTMY = j;
                    }
                }

            }                     

            return gridArray;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets overall p 10. </summary>
        ///
        /// <remarks>   OEE, 7/15/2019. </remarks>
        ///
        /// <param name="windRose">     The wind rose. </param>
        /// <param name="radiusIndex">  Zero-based index of the radius. </param>
        /// <param name="UW_or_DW">     The uw or double-word. </param>
        ///
        /// <returns>   The overall p 10. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double GetOverallP10(double[] windRose, int radiusIndex, string UW_or_DW)
        {
            // Calculates and returns the overall P10 UW or DW exposure
            double overallP10 = 0;
            int numWD = windRose.Length;

            for (int WD = 0; WD < numWD; WD++)
            {
                if (UW_or_DW == "UW")
                    overallP10 = overallP10 + stats[radiusIndex].P10_UW[WD] * windRose[WD];
                else
                    overallP10 = overallP10 + stats[radiusIndex].P10_DW[WD] * windRose[WD];
            }

            return overallP10;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Query if this object is new grid point. </summary>
        ///
        /// <remarks>   OEE, 7/15/2019. </remarks>
        ///
        /// <param name="UTMX">             The utmx. </param>
        /// <param name="UTMY">             The utmy. </param>
        /// <param name="thisRadius">       this radius. </param>
        /// <param name="savedFileName">    Filename of the saved file. </param>
        ///
        /// <returns>   True if new grid point, false if not. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsNewGridPoint(int UTMX, int UTMY, int thisRadius, string savedFileName)
        {
            // Returns true if grid point is new (i.e. exposure not stored in DB)
            bool isNew = true;
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(savedFileName);

            try {
                using (var ctx = new Continuum_EDMContainer(connString))
                {
                    var allNodes = from N in ctx.Node_table where N.UTMX == UTMX & N.UTMY == UTMY select N;
                    
                    if (allNodes.Count() == 0)
                        isNew = true;
                    else
                        isNew = false;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.InnerException.ToString());                
                return isNew;
            }

            return isNew;
        }     

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds the nodes database to 'savedFileName'. </summary>
        ///
        /// <remarks>   OEE, 7/15/2019. </remarks>
        ///
        /// <param name="theseNodes">       The these nodes. </param>
        /// <param name="savedFileName">    Filename of the saved file. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddNodesDB(Node_table[] theseNodes, string savedFileName)
        { 
            // Adds calculated exposures to database
            if (theseNodes == null) 
                return;

            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(savedFileName);

            try
            {
                using (var ctx = new Continuum_EDMContainer(connString))
                {
                    ctx.Node_table.AddRange(theseNodes);
                    ctx.SaveChanges();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());                 
                return;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Calculates P10 UW and P10 DW exposure in each WD sector for specified radius. </summary>
        ///
        /// <remarks>   OEE, 7/15/2019. </remarks>
        ///
        /// <param name="radiusInd">        The radius ind. </param>
        /// <param name="gridArray">        [in,out] Array of grids. </param>
        /// <param name="allNodesForDB">    [in,out] all nodes for database. </param>
        /// <param name="nodesFromDB">      The nodes from database. </param>
        /// <param name="thisInst">         this instance. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void CalcGridStats(int radiusInd, ref TopoInfo.TopoGrid[] gridArray, ref Node_table[] allNodesForDB, Nodes[] nodesFromDB, Continuum thisInst,
            bool isTest, string outFilename)
        {            
            Grid_Avg_SD thisStats = new Grid_Avg_SD();
            int gridCount = gridArray.Length;            
            int numWD = thisInst.metList.numWD;

       //     double[] P10_GridUW = new double[numWD]; // P10 UW exposure in each WD sector            
            double[] thisGridUW = new double[gridCount]; // UW exposure at each grid point for specific WD sector

       //     double[] P10_Grid_DW = new double[numWD];            
            double[] thisGridDW = new double[gridCount];

            Exposure[] gridExpos = new Exposure[gridCount];

            int numDB = 0;            
            int radius = thisInst.radiiList.investItem[radiusInd].radius;
            
            double[,] gridUW = new double[gridCount, numWD]; // UW exposure at each grid point for each WD sector
            double[,] gridDW = new double[gridCount, numWD]; 
            
            BinaryFormatter bin = new BinaryFormatter();

            for (int j = 0; j < gridCount; j++)
            {   
                if (gridArray[j].elev == 0)
                    gridArray[j].elev = thisInst.topo.CalcElevs(gridArray[j].UTMX, gridArray[j].UTMY);                               

                gridExpos[j] = new Exposure();

                if (nodesFromDB != null)
                {
                    for (int i = 0; i < nodesFromDB.Length; i++)
                    {
                        if (nodesFromDB[i].UTMX == gridArray[j].UTMX && nodesFromDB[i].UTMY == gridArray[j].UTMY)
                        {
                            gridExpos[j] = nodesFromDB[i].expo.ElementAt(radiusInd);
                            break;
                        }
                    }
                }
                          
                if (gridExpos[j].expo == null)
                {
                    // Calculate exposure for each radius
                    // Save calculated node to list
                    Array.Resize(ref allNodesForDB, numDB + 1);

                    allNodesForDB[numDB] = new Node_table();
                    allNodesForDB[numDB].UTMX = gridArray[j].UTMX;
                    allNodesForDB[numDB].UTMY = gridArray[j].UTMY;
                    allNodesForDB[numDB].elev = gridArray[j].elev;                                       

                    for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
                    {
                        int thisRadius = thisInst.radiiList.investItem[i].radius;
                        double thisExponent = thisInst.radiiList.investItem[i].exponent;

                        if (gridArray[j].smallerR_Exposure == null)
                            gridExpos[j] = thisInst.topo.CalcExposures(gridArray[j].UTMX, gridArray[j].UTMY, gridArray[j].elev, thisRadius, thisExponent, 1, thisInst.topo, numWD);
                        else
                        {
                            int smallerRadius = gridArray[j].smallerRadius;
                            Exposure smallerExposure = gridArray[j].smallerR_Exposure;
                            gridExpos[j] = thisInst.topo.CalcExposuresWithSmallerRadius(gridArray[j].UTMX, gridArray[j].UTMY, gridArray[j].elev, thisRadius, thisExponent, 1, smallerRadius, smallerExposure, numWD);
                        }

                        if (thisRadius == radius)
                        {
                            for (int WD = 0; WD < numWD; WD++)
                            {
                                gridUW[j, WD] = gridExpos[j].expo[WD];
                                gridDW[j, WD] = gridExpos[j].GetDW_Param(WD, "Expo");
                            }
                        }
                                         
                        MemoryStream MS1 = new MemoryStream();
                        bin.Serialize(MS1, gridExpos[j].expo);
                        Expo_table expoTable = new Expo_table();
                        allNodesForDB[numDB].expo.Add(expoTable);
                        allNodesForDB[numDB].expo.ElementAt(i).exponent = thisExponent;
                        allNodesForDB[numDB].expo.ElementAt(i).radius = thisRadius;
                        allNodesForDB[numDB].expo.ElementAt(i).Expo_Array = MS1.ToArray();

                        MemoryStream MS2 = new MemoryStream();
                        bin.Serialize(MS2, gridExpos[j].expoDist);
                        allNodesForDB[numDB].expo.ElementAt(i).ExpoDist_Array = MS2.ToArray();

                        gridArray[j].smallerRadius = thisRadius;
                        gridArray[j].smallerR_Exposure = gridExpos[j];
                    }

                    numDB = numDB + 1;
                }
                else {
                    for (int WD = 0; WD < numWD; WD++)
                    {
                        gridUW[j, WD] = gridExpos[j].expo[WD];
                        gridDW[j, WD] = gridExpos[j].GetDW_Param(WD, "Expo");
                    }
                }                
            }

            // Calc grid stats
            thisStats.P10_UW = new double[numWD];
            thisStats.P10_DW = new double[numWD];

            StreamWriter wrUW = null;
            StreamWriter wrDW = null;

            if (isTest)
            {
                wrUW = new StreamWriter(outFilename + " Grid UW Expo.csv");
                wrDW = new StreamWriter(outFilename + " Grid DW Expo.csv");
            }

            for (int WD = 0; WD < numWD; WD++)
            {
                for (int j = 0; j < gridCount; j++)
                {
                    thisGridUW[j] = gridUW[j, WD];
                    thisGridDW[j] = gridDW[j, WD];

                    if (isTest)
                    {
                        wrUW.Write(gridUW[j, WD] + ",");
                        wrDW.Write(gridDW[j, WD] + ",");
                    }
                }

                if (isTest)
                {
                    wrUW.WriteLine();
                    wrDW.WriteLine();
                }

                Array.Sort(thisGridUW);
                Array.Sort(thisGridDW);                

                thisStats.radius = radius;
                thisStats.P10_UW[WD] = thisGridUW[Convert.ToInt16(gridCount * 0.9)];
                thisStats.P10_DW[WD] = thisGridDW[Convert.ToInt16(gridCount * 0.9)];

                if (thisStats.P10_UW[WD] > 100)
                    WD = WD;
            }

            Array.Resize(ref stats, radiusInd + 1);
            stats[radiusInd] = thisStats;

            if (isTest)
            {
                wrUW.Close();
                wrDW.Close();
            }
        }                

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets grid array and calculate statistics. </summary>
        ///
        /// <remarks>   OEE, 7/15/2019. </remarks>
        ///
        /// <param name="UTMX">     The utmx. </param>
        /// <param name="UTMY">     The utmy. </param>
        /// <param name="thisInst"> this instance. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void GetGridArrayAndCalcStats(double UTMX, double UTMY, Continuum thisInst)
        {
            // Get grid array surrounding UTMX and UTMY
            TopoInfo.TopoGrid[] gridArray = GetGridArray(UTMX, UTMY, thisInst);

            if (gridArray.Length < 2)
                gridArray = gridArray;

            Node_table[] allNodesForDB = null; // Holds all newly calculated nodes that will be added to database

            // Get grid elevations
            for (int k = 0; k < gridArray.Length; k++)
            {
                if (gridArray[k].UTMX == 0)
                    k = k;

                gridArray[k].elev = thisInst.topo.CalcElevs(gridArray[k].UTMX, gridArray[k].UTMY);
            }
            
            // Find min/max X/Y of grid so the All_Expos table from database can be queried and extract exposure values that have already been calculated
            double minX = 10000000;
            double maxX = 0;
            double minY = 10000000;
            double maxY = 0;

            for (int j = 0; j < gridArray.Length; j++)
            {
                if (gridArray[j].UTMX < minX) minX = gridArray[j].UTMX;
                if (gridArray[j].UTMX > maxX) maxX = gridArray[j].UTMX;
                if (gridArray[j].UTMY < minY) minY = gridArray[j].UTMY;
                if (gridArray[j].UTMY > maxY) maxY = gridArray[j].UTMY;
            }

            // Get saved exposure data from local database (if any exist) within Min/Max X/Y
          //  Grid_Info.Expo_and_UTM[] Expos_from_DB = Get_Range_of_Expos(minX, minY, maxX, maxY, thisInst.savedParams.savedFileName);
            NodeCollection nodeList = new NodeCollection();
            Nodes[] nodesFromDB = nodeList.GetNodes(minX, minY, maxX, maxY, thisInst, true);

            stats = new Grid_Info.Grid_Avg_SD[thisInst.radiiList.ThisCount];
            // Calculate grid statistics (i.e. P10 UW and P10 DW exposure) for each radius 
            for (int r = 0; r <= thisInst.radiiList.ThisCount - 1; r++)
                CalcGridStats(r, ref gridArray, ref allNodesForDB, nodesFromDB, thisInst, false, "");

            // Add newly calculated exposures to database
            if (allNodesForDB != null)
                AddNodesDB(allNodesForDB, thisInst.savedParams.savedFileName);

        }

    }

}
