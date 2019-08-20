using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuumNS
{
    public class Check_class
    {
        public bool NewTurbOrMet(Continuum thisInst, string metname, double UTMX, double UTMY, bool showMsg)
        {
            // Checks the distance between the mets/turbines and edges of topo data to make sure they all fit within defined radii. Returns false if outside bounds.
            bool goodToGo = true;

            if (thisInst.topo.gotTopo)                               
                goodToGo = TopoCheck(thisInst.topo, UTMX, UTMY, metname, "Plot"); 

            if (thisInst.topo.gotSR)
                goodToGo = LandCoverCheck(thisInst.topo, UTMX, UTMY, metname, "Plot");         
                      
            return goodToGo;

        }

        public bool CheckTurbName(string turbineName, MetCollection metList)
        { 
            // Returns false if a turbine with same name exists
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

        public bool CheckMetName(string metName, TurbineCollection turbineList)
        {
            // Returns false if a met with the same name exists
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

        public bool NewTAB(double[,] sectorWS_Dist, double WS_FirstInt, double WS_IntSize, double[] windRose, string metName, Continuum thisInst)
        {
            // Returns true if TAB file has unique met name, each WD sector dist adds to 1000, wind rose adds to 100, and is defined on same WS bin interval as power curve
            bool TAB_ok = true;

            int numWD = windRose.Length;
            int numWS = sectorWS_Dist.GetUpperBound(1) + 1;

            double WS_Total = 0;
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
                WS_Total = 0;

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
                        

            // Check the WS distribution first interval and WS bin and make sure the same as the power curves (if any)
            for (int i = 0; i < thisInst.turbineList.PowerCurveCount; i++) {
                TurbineCollection.PowerCurve thisPowerCurve = new TurbineCollection.PowerCurve();
                bool sameInt = false;
                double thisWS = 0;

                for (int j = 0; j < numWS; j++) {
                    thisWS = WS_FirstInt + WS_IntSize * j - WS_IntSize / 2;
                    if (thisWS == thisPowerCurve.cutInWS) {
                        sameInt = true;
                        break; ;
                    }
                }

                if (sameInt == false) {
                    MessageBox.Show("Error reading in TAB file: " + metName + " WS distribution bins and bin size must line up with the power curves that are currently loaded. " +
                        "Recall that TAB files use WS bins that represent the max value in that bin while the power curve files use WS bins that represent the mid value of the bin.");
                    TAB_ok = false;
                }
            }

            return TAB_ok;
        }
               
        
        public bool TopoCheck(TopoInfo topo, double UTMX, double UTMY, string siteName, string allOrPlot)
        {
            // Check elev at 8 points +/- 12000 m from UTMX/Y. If elev = -999, return false

            bool goodToGo = true;
            int maxDist = 12000;
            double X_Loc = 0;
            double Y_Loc = 0;
                        
            int[] indices = topo.GetXYIndices("Topo", UTMX, UTMY, allOrPlot); // Site location
            
            // Check that there is topography data
            if (indices[0] == -999 && indices[1] == -999)
            {
                MessageBox.Show("Topography data not loaded.", "Continuum 3");
                return goodToGo = false;
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

                indices = topo.GetXYIndices("Topo", X_Loc, Y_Loc, allOrPlot); 

                if (allOrPlot == "All")
                {
                    if (indices[0] < 0 || indices[0] >= topo.topoNumXY.X.all.num || indices[1] < 0 || indices[1] >= topo.topoNumXY.Y.all.num)
                    {
                        MessageBox.Show("Site: " + siteName + " is outside range of topography data.", "Continuum 3");
                        return goodToGo = false;
                    }
                }
                else
                {
                    if (indices[0] < 0 || indices[0] >= topo.topoNumXY.X.plot.num || indices[1] < 0 || indices[1] >= topo.topoNumXY.Y.plot.num)
                    {
                        MessageBox.Show("Site: " + siteName + " is outside range of topography data.", "Continuum 3");
                        return goodToGo = false;
                    }                    
                }

                if (topo.topoElevs[indices[0], indices[1]] == -999)
                {
                    MessageBox.Show("Site: " + siteName + " is outside range of topography data.", "Continuum 3");
                    return goodToGo = false;
                }

            }

            return goodToGo;

        }

        public bool LandCoverCheck(TopoInfo topo, double UTMX, double UTMY, string siteName, string allOrPlot)
        {
            // Check land cover at 8 points +/- 12000 m from UTMX/Y. If land cover = -999 or if outside range, return false

            bool goodToGo = true;
            int maxDist = 12000;
            double X_Loc = 0;
            double Y_Loc = 0;
            
            int[] indices = topo.GetXYIndices("Land Cover", UTMX, UTMY, allOrPlot); // Site location

            // Check that there is topography data
            if (indices[0] == -999 && indices[1] == -999)
            {
                MessageBox.Show("Land Cover data not loaded.", "Continuum 3");
                return goodToGo = false;
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

                indices = topo.GetXYIndices("Land Cover", X_Loc, Y_Loc, allOrPlot); // Northeast of site

                if (allOrPlot == "All")
                {
                    if (indices[0] < 0 || indices[0] >= topo.LC_NumXY.X.all.num || indices[1] < 0 || indices[1] >= topo.LC_NumXY.Y.all.num)
                    {
                        MessageBox.Show("Site: " + siteName + " is outside range of land cover data.", "Continuum 3");
                        return goodToGo = false;
                    }
                }
                else
                {
                    if (indices[0] < 0 || indices[0] >= topo.LC_NumXY.X.plot.num || indices[1] < 0 || indices[1] >= topo.LC_NumXY.Y.plot.num)
                    {
                        MessageBox.Show("Site: " + siteName + " is outside range of land cover data.", "Continuum 3");
                        return goodToGo = false;
                    }
                }

                if (topo.landCover[indices[0], indices[1]] == -999)
                {
                    MessageBox.Show("Site: " + siteName + " is outside range of land cover data.", "Continuum 3");
                    return goodToGo = false;
                }
            }

            return goodToGo;

        }

        public bool NewTopo(TopoInfo topo, MetCollection metList, TurbineCollection turbList, string allOrPlot)
        {
            // Go through each met and turbine site and check elev at 8 points +/- 12000 m. If elev = -999, return false
            
            bool goodToGo = true;
            
            for (int i = 0; i < metList.ThisCount; i++)            
                goodToGo = TopoCheck(topo, metList.metItem[i].UTMX, metList.metItem[i].UTMY, metList.metItem[i].name, allOrPlot); 
            
            for (int i = 0; i < turbList.TurbineCount; i++)
                goodToGo = TopoCheck(topo, turbList.turbineEsts[i].UTMX, turbList.turbineEsts[i].UTMY, turbList.turbineEsts[i].name, allOrPlot); 

            return goodToGo;

        }

        public bool NewLandCover(TopoInfo topo, MetCollection metList, TurbineCollection turbList, string allOrPlot)
        {
            // Go through each met and turbine site and check land cover at 8 points +/- 12000 m. If elev = -999, return false

            bool goodToGo = true;

            for (int i = 0; i < metList.ThisCount; i++)
                goodToGo = LandCoverCheck(topo, metList.metItem[i].UTMX, metList.metItem[i].UTMY, metList.metItem[i].name, allOrPlot);

            for (int i = 0; i < turbList.TurbineCount; i++)
                goodToGo = LandCoverCheck(topo, turbList.turbineEsts[i].UTMX, turbList.turbineEsts[i].UTMY, turbList.turbineEsts[i].name, allOrPlot);

            return goodToGo;

        }

        public bool CheckBinsAndMatrixSettings(Continuum thisInst)
        {
            // Checks the Method of Bins and Matrix settings. Returns true if valid. Shows error message if not.
            bool goodToGo = true;

            if (thisInst.Get_MCP_Method() == "Method of Bins" || thisInst.Get_MCP_Method() == "Matrix")
            {
                if (thisInst.metList.mcpWS_BinWidth < 0.1 || thisInst.metList.mcpWS_BinWidth > 2)
                {
                    MessageBox.Show("Invalid wind speed bin width. Enter a value between 0.1 and 2.", "Continuum 3.0");
                    return goodToGo = false;
                }

            }
            else if (thisInst.Get_MCP_Method() == "Matrix")
            {
                if (thisInst.metList.mcpMatrixWgt < 0 || thisInst.metList.mcpMatrixWgt > 10)
                {
                    MessageBox.Show("Invalid matrix weight. Enter a value between 0 and 10.", "Continuum 3.0");
                    return goodToGo = false;
                }
                else if (thisInst.metList.mcpLastWS_Wgt < 0 || thisInst.metList.mcpLastWS_Wgt > 10)
                {
                    MessageBox.Show("Invalid last wind speed weight. Enter a value between 0 and 10.", "Continuum 3.0");
                    return goodToGo = false;
                }
                else if (thisInst.metList.mcpMatrixWgt == 0 && thisInst.metList.mcpLastWS_Wgt == 0)
                {
                    MessageBox.Show("Both Matrix weights cannot be zero.", "Continuum 3.0");
                    return goodToGo = false;
                }
            }



            return goodToGo;
        }

    }
}
