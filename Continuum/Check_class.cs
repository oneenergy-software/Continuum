using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary> Check_class contains Boolean functions that check new met sites, turbine sites and other files that the user is trying to import. </summary>
    public class Check_class
    {
        /// <summary> Checks the distance between the mets/turbines and edges of topo data to make sure they all fit within defined radii. </summary>
        /// <returns> Returns false if outside bounds.</returns>
        public bool NewTurbOrMet(TopoInfo topo, string metname, double UTMX, double UTMY, bool showMsg)
        {            
            bool goodToGo = true;

            if (topo.gotTopo)                               
                goodToGo = TopoCheck(topo, UTMX, UTMY, metname, showMsg); 

            if (topo.gotSR)
                goodToGo = LandCoverCheck(topo, UTMX, UTMY, metname, showMsg);         
                      
            return goodToGo;

        }

        /// <summary> Compares turbine name to met site names to ensure there are no duplicate names. </summary>        
        /// <returns> Returns false if a turbine with same name exists </returns>
        public bool CheckTurbName(string turbineName, MetCollection metList)
        {              
            bool inputTurbine = true;
            int numMets = metList.ThisCount;

            for (int i = 0; i < numMets; i++) { 
                if (metList.metItem[i].name == turbineName) {
                    inputTurbine = false;
                    MessageBox.Show("There is a met with the same name as turbine site: " + turbineName + ".  There cannot be a turbine and met site with the same name.",  "Continuum 3");
                break;
                }
            }

            return inputTurbine;
        }

        /// <summary> Compares met name to turbine names to ensure there are no duplicate names. </summary>        
        /// <returns> Returns false if a met with the same name exists </returns>
        public bool CheckMetName(string metName, TurbineCollection turbineList)
        { 
            bool inputMet = true;
            int numTurbines = turbineList.TurbineCount;

            for (int i = 0; i < numTurbines; i++) { 
                if (turbineList.turbineEsts[i].name == metName) {
                    inputMet = false;
                    MessageBox.Show("There is a turbine with the same name as met site: " + metName + ".  There cannot be a turbine and met site with the same name.", "Continuum 3");
                    break;
                }
            }

            return inputMet;
        }

        /// <summary> Checks that TAB file has unique met name, each WD sector dist adds to 1000, wind rose adds to 100, and bins align with other mets' bins </summary>        
        /// <returns> Returns true if TAB file has unique met name, each WD sector dist adds to 1000, wind rose adds to 100 </returns>
        public bool NewTAB(double[,] sectorWS_Dist, double WS_FirstInt, double WS_IntSize, double[] windRose, string metName, Continuum thisInst)
        {            
            bool TAB_ok = true;

            int numWD = windRose.Length;
            int numWS = sectorWS_Dist.GetUpperBound(1) + 1;                        
            double WR_Total = 0;

            for (int i = 0; i < thisInst.metList.ThisCount; i++) { 
                if (thisInst.metList.metItem[i].name == metName ) {
                        MessageBox.Show("A met of the same name has already been imported.", "Continuum 3");
                        TAB_ok = false;
                        return TAB_ok;
                }
            }

            if (thisInst.metList.ThisCount > 0 && (thisInst.metList.WS_FirstInt != WS_FirstInt || thisInst.metList.WS_IntSize != WS_IntSize))
            {
                MessageBox.Show("WS intervals in this TAB file don't line up with intervals of other mets. " +
                    "Expecting first WS = " + Math.Round(WS_FirstInt, 1).ToString() + " and WS interval = " + Math.Round(WS_IntSize, 1).ToString());
                TAB_ok = false;
                return TAB_ok;
            }

            for (int i = 0; i < numWD; i++) {
                WR_Total = WR_Total + windRose[i];
                double WS_Total = 0;

                for (int j = 0; j < numWS; j++) 
                    WS_Total = WS_Total + sectorWS_Dist[i, j];
                
                if (WS_Total< 999 || WS_Total> 1001) {
                        MessageBox.Show("Error reading in TAB file: " + metName + ".  WS distribution must add to 1000 for each sector");
                        TAB_ok = false;
                        return TAB_ok;
                }
            }

            if (WR_Total< 0.99 || WR_Total > 1.01) {
                MessageBox.Show("Error reading in TAB file :" + metName + ".  Wind Rose must add to 100");
                TAB_ok = false;
            }                       
                        
            return TAB_ok;
        }

        /// <summary> Checks if the elevation is available at 8 points +/- 12000 m from specified UTMX/Y and returns false if all are not available. </summary>        
        public bool TopoCheck(TopoInfo topo, double UTMX, double UTMY, string siteName, bool showMessage)
        {            
            bool goodToGo = true;
            int maxDist = 12000;
            double X_Loc = 0;
            double Y_Loc = 0;
                        
            int[] indices = topo.GetXYIndices("Topo", UTMX, UTMY, "Plot"); // Site location
            
            // Check that there is topography data
            if (indices[0] == -999 && indices[1] == -999)
            {
                goodToGo = false;
                if (showMessage)
                    MessageBox.Show("Topography data not loaded.", "Continuum 3");
                return goodToGo;
            }

            if (indices[0] == -888 && indices[1] == -888)
            {
                goodToGo = false;
                if (showMessage)
                    MessageBox.Show("Error reading file.", "Continuum 3");
                return goodToGo;
            }

            for (int i = 0; i < 8; i++)
            {
                if (i == 0) // North
                {                    
                    X_Loc = UTMX;
                    Y_Loc = UTMY + maxDist;
                }
                else if (i == 1) // Northeast
                {                    
                    X_Loc = UTMX + maxDist * 0.7071;
                    Y_Loc = UTMY + maxDist * 0.7071;
                }
                else if (i == 2) // East
                {                    
                    X_Loc = UTMX + maxDist;
                    Y_Loc = UTMY;
                }
                else if (i == 3) // Southeast
                {                    
                    X_Loc = UTMX + maxDist * 0.7071;
                    Y_Loc = UTMY - maxDist * 0.7071;
                }
                else if (i == 4) // South
                {                    
                    X_Loc = UTMX;
                    Y_Loc = UTMY - maxDist;
                }
                else if (i == 5) // Southwest
                {                    
                    X_Loc = UTMX - maxDist * 0.7071;
                    Y_Loc = UTMY - maxDist * 0.7071;
                }
                else if (i == 6) // West
                {                    
                    X_Loc = UTMX - maxDist;
                    Y_Loc = UTMY;
                }
                else if (i == 7) // Northwest
                {                    
                    X_Loc = UTMX - maxDist * 0.7071;
                    Y_Loc = UTMY + maxDist * 0.7071;
                }

                indices = topo.GetXYIndices("Topo", X_Loc, Y_Loc, "Plot"); 

                if (indices[0] == -888)
                {
                    goodToGo = false;
                    return goodToGo;
                }
                                
                if (indices[0] < 0 || indices[0] >= topo.topoNumXY.X.plot.num || indices[1] < 0 || indices[1] >= topo.topoNumXY.Y.plot.num)
                {
                    goodToGo = false;
                    if (showMessage == true)
                        MessageBox.Show("Site: " + siteName + " is outside range of topography data.", "Continuum 3");
                    return goodToGo;
                }

                double thisElev = topo.topoElevs[indices[0], indices[1]];

                if (thisElev == -999)
                {
                    goodToGo = false;
                    if (showMessage == true)
                        MessageBox.Show("Site: " + siteName + " is outside range of topography data.", "Continuum 3");
                    return goodToGo;
                }
            }

            return goodToGo;
        }

        /// <summary> Checks land cover at 8 points +/- 12000 m from UTMX/Y. If land cover = -999 or if outside range, return false </summary>        
        public bool LandCoverCheck(TopoInfo topo, double UTMX, double UTMY, string siteName, bool showMessage)
        {
            bool goodToGo = true;
            int maxDist = 12000;
            double X_Loc = 0;
            double Y_Loc = 0;
            
            int[] indices = topo.GetXYIndices("Land Cover", UTMX, UTMY, "Plot"); // Site location

            if (indices[0] == -888)
            {
                goodToGo = false;
                return goodToGo;
            }                

            // Check that there is land cover  data
            if (indices[0] == -999 && indices[1] == -999)
            {
                goodToGo = false;
                if (showMessage)
                    MessageBox.Show("Land Cover data not loaded.", "Continuum 3");
                return goodToGo;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (i == 0) // North
                {                    
                    X_Loc = UTMX;
                    Y_Loc = UTMY + maxDist;
                }
                else if (i == 1) // Northeast
                {                    
                    X_Loc = UTMX + maxDist * 0.7071;
                    Y_Loc = UTMY + maxDist * 0.7071;
                }
                else if (i == 2) // East
                {                   
                    X_Loc = UTMX + maxDist;
                    Y_Loc = UTMY;
                }
                else if (i == 3) // Southeast
                {                   
                    X_Loc = UTMX + maxDist * 0.7071;
                    Y_Loc = UTMY - maxDist * 0.7071;
                }
                else if (i == 4) // South
                {                    
                    X_Loc = UTMX;
                    Y_Loc = UTMY - maxDist;
                }
                else if (i == 5) // Southwest
                {                    
                    X_Loc = UTMX - maxDist * 0.7071;
                    Y_Loc = UTMY - maxDist * 0.7071;
                }
                else if (i == 6) // West
                {                   
                    X_Loc = UTMX - maxDist;
                    Y_Loc = UTMY;
                }
                else if (i == 7) // Northwest
                {                    
                    X_Loc = UTMX - maxDist * 0.7071;
                    Y_Loc = UTMY + maxDist * 0.7071;
                }

                indices = topo.GetXYIndices("Land Cover", X_Loc, Y_Loc, "Plot");

                if (indices[0] == -888)
                {
                    goodToGo = false;
                    return goodToGo;
                }                    
                                
                if (indices[0] < 0 || indices[0] >= topo.LC_NumXY.X.plot.num || indices[1] < 0 || indices[1] >= topo.LC_NumXY.Y.plot.num)
                {
                    goodToGo = false;
                    if (showMessage == true)
                        MessageBox.Show("Site: " + siteName + " is outside range of land cover data.", "Continuum 3");

                    return goodToGo;
                }                

                if (topo.landCover[indices[0], indices[1]] == -999)
                {
                    goodToGo = false;
                    if (showMessage == true)
                        MessageBox.Show("Site: " + siteName + " is outside range of land cover data.", "Continuum 3");
                    return goodToGo;
                }
            }

            return goodToGo;

        }

        /// <summary> Goes through each met and turbine site and check elev at 8 points +/- 12000 m. If elev = -999, return false </summary>        
        public bool NewTopo(TopoInfo topo, MetCollection metList, TurbineCollection turbList)
        {            
            bool goodToGo = true;
            
            for (int i = 0; i < metList.ThisCount; i++)            
                goodToGo = TopoCheck(topo, metList.metItem[i].UTMX, metList.metItem[i].UTMY, metList.metItem[i].name, true); 
            
            for (int i = 0; i < turbList.TurbineCount; i++)
                goodToGo = TopoCheck(topo, turbList.turbineEsts[i].UTMX, turbList.turbineEsts[i].UTMY, turbList.turbineEsts[i].name, true); 

            return goodToGo;

        }

        /// <summary> Goes through each met and turbine site and check land cover at 8 points +/- 12000 m. If elev = -999, return false </summary>   
        public bool NewLandCover(TopoInfo topo, MetCollection metList, TurbineCollection turbList)
        {
            // Go through each met and turbine site and check land cover at 8 points +/- 12000 m. If elev = -999, return false

            bool goodToGo = true;

            for (int i = 0; i < metList.ThisCount; i++)
                goodToGo = LandCoverCheck(topo, metList.metItem[i].UTMX, metList.metItem[i].UTMY, metList.metItem[i].name, true);

            for (int i = 0; i < turbList.TurbineCount; i++)
                goodToGo = LandCoverCheck(topo, turbList.turbineEsts[i].UTMX, turbList.turbineEsts[i].UTMY, turbList.turbineEsts[i].name, true);

            return goodToGo;

        }

        /// <summary> Checks that the Method of Bins and Matrix settings are within allowable limits. </summary>        
        /// <returns> Returns true if valid. Shows error message if not. </returns>
        public bool CheckBinsAndMatrixSettings(Continuum thisInst)
        {            
            bool goodToGo = true;

            if (thisInst.Get_MCP_Method() == "Method of Bins" || thisInst.Get_MCP_Method() == "Matrix")
            {
                if (thisInst.metList.mcpWS_BinWidth < 0.1 || thisInst.metList.mcpWS_BinWidth > 2)
                {
                    goodToGo = false;
                    MessageBox.Show("Invalid wind speed bin width. Enter a value between 0.1 and 2.", "Continuum 3.0");
                    return goodToGo;
                }

            }
            else if (thisInst.Get_MCP_Method() == "Matrix")
            {
                if (thisInst.metList.mcpMatrixWgt < 0 || thisInst.metList.mcpMatrixWgt > 10)
                {
                    goodToGo = false;
                    MessageBox.Show("Invalid matrix weight. Enter a value between 0 and 10.", "Continuum 3.0");
                    return goodToGo;
                }
                else if (thisInst.metList.mcpLastWS_Wgt < 0 || thisInst.metList.mcpLastWS_Wgt > 10)
                {
                    goodToGo = false;
                    MessageBox.Show("Invalid last wind speed weight. Enter a value between 0 and 10.", "Continuum 3.0");
                    return goodToGo;
                }
                else if (thisInst.metList.mcpMatrixWgt == 0 && thisInst.metList.mcpLastWS_Wgt == 0)
                {
                    goodToGo = false;
                    MessageBox.Show("Both Matrix weights cannot be zero.", "Continuum 3.0");
                    return goodToGo;
                }
            }

            return goodToGo;
        }

        /// <summary> Checks if the 8 points +/- specified distance from specified UTMX/Y on specified grid (plot vs calcs) fall within bounds. /// </summary> 
        /// <returns> Returns 100 if all points are within defined area and returns index of point outside range if all are not available. </returns>
        public int NewNodeCheck(TopoInfo topo, double UTMX, double UTMY, int minDist, string plotOrCalcs)
        {
            int goodToGo = 100;
            
            double X_Loc = 0;
            double Y_Loc = 0;

            int[] topoIndices = topo.GetXYIndices("Topo", UTMX, UTMY, plotOrCalcs); // topoElevs indices at site location
            int[] landCoverIndices = topo.GetXYIndices("Land cover", UTMX, UTMY, plotOrCalcs); // landCover indices at site location

            bool[] indexChecks = new bool[8];
            
            // Check that there is topography data
            if (topoIndices[0] == -999 && topoIndices[1] == -999)
            {
                goodToGo = -999;
                return goodToGo;
            }                

            if (topo.gotSR && landCoverIndices[0] == -999 && landCoverIndices[1] == -999)
            {
                goodToGo = -999;
                return goodToGo;
            }                

            if (topoIndices[0] == -888)
            {
                goodToGo = -888;
                return goodToGo;
            }                

            if (landCoverIndices[0] == -888)
            {
                goodToGo = -888;
                return goodToGo;
            }                

            for (int i = 0; i < 8; i++)
            {
                indexChecks[i] = true;

                if (i != 1 && i != 3 && i != 5 && i != 7) // only looking at N, E, S, W
                {
                    if (i == 0) // North
                    {
                        X_Loc = UTMX;
                        Y_Loc = UTMY + minDist;
                    }
                    else if (i == 1) // Northeast
                    {
                        X_Loc = UTMX + minDist * 0.7071;
                        Y_Loc = UTMY + minDist * 0.7071;
                    }
                    else if (i == 2) // East
                    {
                        X_Loc = UTMX + minDist;
                        Y_Loc = UTMY;
                    }
                    else if (i == 3) // Southeast
                    {
                        X_Loc = UTMX + minDist * 0.7071;
                        Y_Loc = UTMY - minDist * 0.7071;
                    }
                    else if (i == 4) // South
                    {
                        X_Loc = UTMX;
                        Y_Loc = UTMY - minDist;
                    }
                    else if (i == 5) // Southwest
                    {
                        X_Loc = UTMX - minDist * 0.7071;
                        Y_Loc = UTMY - minDist * 0.7071;
                    }
                    else if (i == 6) // West
                    {
                        X_Loc = UTMX - minDist;
                        Y_Loc = UTMY;
                    }
                    else if (i == 7) // Northwest
                    {
                        X_Loc = UTMX - minDist * 0.7071;
                        Y_Loc = UTMY + minDist * 0.7071;
                    }

                    topoIndices = topo.GetXYIndices("Topo", X_Loc, Y_Loc, plotOrCalcs);

                    int numXTopo = 0;
                    int numYTopo = 0;

                    if (plotOrCalcs == "Plot")
                    {
                        numXTopo = topo.topoNumXY.X.plot.num;
                        numYTopo = topo.topoNumXY.Y.plot.num;
                    }
                    else if (plotOrCalcs == "Calcs")
                    {
                        numXTopo = topo.topoNumXY.X.calcs.num;
                        numYTopo = topo.topoNumXY.Y.calcs.num;
                    }

                    if (topoIndices[0] < 0 || topoIndices[0] >= numXTopo || topoIndices[1] < 0 || topoIndices[1] >= numYTopo)
                        indexChecks[i] = false;

                    if (indexChecks[i] == true)
                    {
                        if (plotOrCalcs == "Plot")
                            if (topo.topoElevs[topoIndices[0], topoIndices[1]] == -999)
                                indexChecks[i] = false;
                    }

                    if (topo.gotSR)
                    {
                        landCoverIndices = topo.GetXYIndices("Land cover", X_Loc, Y_Loc, plotOrCalcs);

                        int numXLC = 0;
                        int numYLC = 0;

                        if (plotOrCalcs == "Plot")
                        {
                            numXLC = topo.LC_NumXY.X.plot.num;
                            numYLC = topo.LC_NumXY.Y.plot.num;
                        }
                        else if (plotOrCalcs == "Calcs")
                        {
                            numXLC = topo.LC_NumXY.X.calcs.num;
                            numYLC = topo.LC_NumXY.Y.calcs.num;
                        }

                        if (landCoverIndices[0] < 0 || landCoverIndices[0] >= numXLC || landCoverIndices[1] < 0 || landCoverIndices[1] >= numYLC)
                            indexChecks[i] = false;

                        if (indexChecks[i] == true)
                            if (plotOrCalcs == "Plot")
                                if (topo.landCover[landCoverIndices[0], landCoverIndices[1]] == -999)
                                    indexChecks[i] = false;
                    }
                }

            }

            // Go through array of booleans to figure out which way to move location
            bool allFalse = true;
            for (int i = 0; i < 8; i++)
            {
                if (i != 1 && i != 3 && i != 5 && i != 7) // only looking at N, E, S, W
                {
                    if (indexChecks[i] != true)
                    {
                        int oppSect = i - 4;
                        if (oppSect < 0) oppSect = oppSect + 8;

                        if (indexChecks[oppSect] == true)
                            return i;
                    }
                    else
                        allFalse = false;
                }

            }


            // If false at all locations, figure out which way to move based on distance to four corners
            if (allFalse == true)
            {
                double distToMinXMinY = topo.CalcDistanceBetweenPoints(UTMX, UTMY, topo.topoNumXY.X.all.min, topo.topoNumXY.Y.all.min);
                double distToMinXMaxY = topo.CalcDistanceBetweenPoints(UTMX, UTMY, topo.topoNumXY.X.all.min, topo.topoNumXY.Y.all.max);
                double distToMaxXMinY = topo.CalcDistanceBetweenPoints(UTMX, UTMY, topo.topoNumXY.X.all.max, topo.topoNumXY.Y.all.min);
                double distToMaxXMaxY = topo.CalcDistanceBetweenPoints(UTMX, UTMY, topo.topoNumXY.X.all.max, topo.topoNumXY.Y.all.max);

                if (distToMinXMinY > distToMinXMaxY && distToMinXMinY > distToMaxXMinY && distToMinXMinY > distToMaxXMaxY)
                    goodToGo = 1; // Furthest from Min X/Y
                else if (distToMinXMaxY > distToMinXMinY && distToMinXMaxY > distToMaxXMinY && distToMinXMaxY > distToMaxXMaxY)
                    goodToGo = 3; // Furthest from MinX and MaxY 
                else if (distToMaxXMinY > distToMinXMinY && distToMaxXMinY > distToMinXMaxY && distToMaxXMinY > distToMaxXMaxY)
                    goodToGo = 5; // Furthest from Max X/Y
                else
                    goodToGo = 7; // Furthest from Max X and Min Y

            }
            

            return goodToGo;

        }

        /// <summary> Checks the raster data contained in TIFF file. Sometimes users accidentally create TIFF files that contain the RGB color codes and not the elevation.
        ///           If there is no decimal point, max value is 255, it is likely not elevation data so it returns false </summary>              
        public bool IsGeoTIFF(double[] elevData)
        {             
            bool IsGeoTiff = true;
            double maxValue = 0;
            bool hasDecimal = false;
            
            if (elevData != null)
            {
                int numData = elevData.Length;
                
                for (int i = 0; i < numData; i++)
                {
                    if (elevData[i] > maxValue)                    
                        maxValue = elevData[i];

                    if (elevData[i] % 1 != 0)
                        hasDecimal = true;
                }
            }

            if (maxValue == 255 && hasDecimal == false)
                IsGeoTiff = false;
            
            return IsGeoTiff;
        }
    }
}
