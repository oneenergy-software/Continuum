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
            bool inputMet = true;

            if (thisInst.topo.gotTopo == true) {                                
                string goodToGo = CheckLocation(thisInst.topo, UTMX, UTMY, thisInst.radiiList.GetMaxRadius());

                if (goodToGo != "Good to go")
                {
                    if (showMsg == true)
                        MessageBox.Show(goodToGo);
                    inputMet = false;
                }                             

            }
            return inputMet;

        }

        public bool CheckTurbName(string turbineName, MetCollection metList)
        { 
            // Returns false if a turbine with same name exists
            bool inputTurbine = true;
            int numMets = metList.ThisCount;

            for (int i = 0; i < numMets; i++) { 
                if (metList.metItem[i].name == turbineName) {
                    inputTurbine = false;
                    MessageBox.Show("There is a met with the same name as turbine site: " + turbineName + ".  There cannot be a turbine and met site with the same name.",  "Continuum 2.3");
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
                    MessageBox.Show("There is a turbine with the same name as met site: " + metName + ".  There cannot be a turbine and met site with the same name.", "Continuum 2.3");
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
                        MessageBox.Show("A met of the same name has already been imported.", "Continuum 2.3");
                        TAB_ok = false;
                        return TAB_ok;
                }
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

            // Check the WS distribution first interval and WS bin and make sure the same as the other mets
            for (int i = 0; i < thisInst.metList.ThisCount; i++) { 
                if (thisInst.metList.metItem[i].WS_FirstInt!= WS_FirstInt || thisInst.metList.metItem[i].WS_IntSize!= WS_IntSize) {
                        MessageBox.Show("Error reading in TAB file: " + metName + " WS distribution bins and bin size must be the same as the other mets currently loaded.");
                        TAB_ok = false;
                }
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

        public bool NewTopoOrLC(double minUTMX, double maxUTMX, double minUTMY, double maxUTMY, Continuum thisInst)
        {
            // Returns true if topo and/or land cover data covers the existing mets and turbines
            bool inputTopo = true;
                        
            int numMets = thisInst.metList.ThisCount;
            int numTurbines = thisInst.turbineList.TurbineCount;
            
            // Check that all existing mets and turbines fall within bounds
            for (int i = 0; i < numMets; i++) {
                double UTMX = thisInst.metList.metItem[i].UTMX;
                double UTMY = thisInst.metList.metItem[i].UTMY;
                string thisName = thisInst.metList.metItem[i].name;
                string goodToGo = CheckLocation(thisInst.topo, UTMX, UTMY, thisInst.radiiList.GetMaxRadius() + 2000);

                if (goodToGo != "Good to go")
                {
                    MessageBox.Show("Met site: " + thisName + " falls outside the bounds of the topographic data that you are trying to load. " + goodToGo +
                           " Either delete this met site or update your XYZ file.", "Continuum 2.3");
                    inputTopo = false;
                }
            }

            for (int i = 0; i < numTurbines; i++) {
                double UTMX = thisInst.turbineList.turbineEsts[i].UTMX;
                double UTMY = thisInst.turbineList.turbineEsts[i].UTMY;
                string thisName = thisInst.turbineList.turbineEsts[i].name;
                string goodToGo = CheckLocation(thisInst.topo, UTMX, UTMY, thisInst.radiiList.GetMaxRadius() + 2000);

                if (goodToGo != "Good to go")
                {
                    MessageBox.Show("Turbine site: " + thisName + " falls outside the bounds of the topographic data that you are trying to load. " + goodToGo +
                           " Either delete this turbine site or update your XYZ file.", "Continuum 2.3");
                    inputTopo = false;
                }
            }          

            return inputTopo;

        }               

        public string CheckLocation (TopoInfo topo, double newX, double newY, int maxRadius) 
        {
            // Calculate distance to each side of topo and land cover map. Make sure they are more than maxRadius.
            string isWithinBounds = "Good to go";
            double thisDist;
            
            // Check Topography 
            if (topo.topoNumXY.X.all.reso != 0) // (Can't check gotTopo since it won't be set when this is called)
            {
                // South
                thisDist = topo.CalcDistanceBetweenPoints(newX, topo.topoNumXY.Y.all.min, newX, newY);
                if (thisDist < maxRadius)
                    isWithinBounds = "Too close to south edge of topography map. distance = " + Math.Round(thisDist,0).ToString();

                // North
                thisDist = topo.CalcDistanceBetweenPoints(newX, topo.topoNumXY.Y.all.max, newX, newY);
                if (thisDist < maxRadius)
                    isWithinBounds = "Too close to north edge of topography map. distance = " + Math.Round(thisDist, 0).ToString();

                // West
                thisDist = topo.CalcDistanceBetweenPoints(topo.topoNumXY.X.all.min, newY, newX, newY);
                if (thisDist < maxRadius)
                    isWithinBounds = "Too close to west edge of topography map. distance = " + Math.Round(thisDist, 0).ToString();

                // East
                thisDist = topo.CalcDistanceBetweenPoints(topo.topoNumXY.X.all.max, newY, newX, newY);
                if (thisDist < maxRadius)
                    isWithinBounds = "Too close to east edge of topography map. distance = " + Math.Round(thisDist, 0).ToString();
            }

            // Check Land Cover 
            if (topo.LC_NumXY.X.all.reso != 0)
            {
                // South
                thisDist = topo.CalcDistanceBetweenPoints(newX, topo.LC_NumXY.Y.all.min, newX, newY);
                if (thisDist < maxRadius)
                    isWithinBounds = "Too close to south edge of land cover/roughness map. distance = " + Math.Round(thisDist, 0).ToString();

                // North
                thisDist = topo.CalcDistanceBetweenPoints(newX, topo.LC_NumXY.Y.all.max, newX, newY);
                if (thisDist < maxRadius)
                    isWithinBounds = "Too close to north edge of land cover/roughness map. distance = " + Math.Round(thisDist, 0).ToString();

                // West
                thisDist = topo.CalcDistanceBetweenPoints(topo.LC_NumXY.X.all.min, newY, newX, newY);
                if (thisDist < maxRadius)
                    isWithinBounds = "Too close to west edge of land cover/roughness map. distance = " + Math.Round(thisDist, 0).ToString();

                // East
                thisDist = topo.CalcDistanceBetweenPoints(topo.LC_NumXY.X.all.max, newY, newX, newY);
                if (thisDist < maxRadius)
                    isWithinBounds = "Too close to east edge of land cover/roughness map. distance = " + Math.Round(thisDist, 0).ToString();
            }

            return isWithinBounds;
        }

    }
}
