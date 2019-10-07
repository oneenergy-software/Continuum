using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class Nodes
    {
        public double UTMX;
        public double UTMY;
        public double elev;
        public Exposure[] expo;
        public Grid_Info gridStats;     
        public NodeCollection.Sep_Nodes[] flowSepNodes;

        // Properties
        public int ExposureCount { 
            get {

                if (expo == null)
                    return 0;
                else
                    return expo.Length;
            }
        }            

        public void AddExposure(int radius, double exponent, int numSectors, int numWD)
        {
            // Adds exposure to expo() list
            int expCount = ExposureCount;
            int insertInd = 0;

            if (expCount > 0)
            {
                if (radius > expo[expCount - 1].radius) // Larger radius than largest in list
                    insertInd = expCount - 1;
                else
                {
                    for (int i = 0; i <= expCount - 2; i++)
                    {
                        if (expo[i].radius < radius && expo[i + 1].radius >= radius) {
                            insertInd = i;
                            break;
                        }
                    }
                }

                Exposure[] existingExpos = expo;
                expo = new Exposure[expCount + 1];

                for (int j = 0; j <= insertInd; j++)
                    expo[j] = existingExpos[j];

                expo[insertInd + 1] = new Exposure();
                expo[insertInd + 1].radius = radius;
                expo[insertInd + 1].exponent = exponent;
                expo[insertInd + 1].numSectors = numSectors;
                expo[insertInd + 1].expo = new double[numWD];

                for (int j = insertInd + 2; j <= expCount; j++)
                    expo[j] = existingExpos[j - 1];
            }
            else {
                expo = new Exposure[1];
                expo[0] = new Exposure();
                expo[0].radius = radius;
                expo[0].exponent = exponent;
                expo[0].numSectors = numSectors;
                expo[0].expo = new double[numWD];
            }

        }

        public double CalcAvgWS(double[] sectorWS, double[] windRose)
        {
            // Calculates and returns average wind speed weighted by Nodes' wind rose
            double avgWS = 0;
            int numWD = 0;

            if (sectorWS != null)
                numWD = sectorWS.Length;

            if (numWD > 0)
                for (int i = 0; i < numWD; i++)
                    avgWS = avgWS + sectorWS[i] * windRose[i];

            return avgWS;
        }

        /// <summary>
        /// Calculates the grid stats (i.e. P10 exposure), exposure, surface roughness and disp. height at node for each radius of investigation
        /// </summary>        
        public void CalcGridStatsAndExposures(Continuum thisInst)
        {   
            int numWD = thisInst.metList.numWD;                        
            int smallerRadius = 0;
            Exposure smallerExposure = new Exposure();

            if (ExposureCount == 0)
            {
                // Calculate exposures and grid stats at all radii
                for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
                {
                    int thisRadius = thisInst.radiiList.investItem[i].radius;
                    double thisExponent = thisInst.radiiList.investItem[i].exponent;

                    AddExposure(thisRadius, thisExponent, 1, numWD);
                    int expInd = 0;

                    for (expInd = 0; expInd <= ExposureCount - 1; expInd++)
                        if (expo[expInd].radius == thisRadius && expo[expInd].exponent == thisExponent)
                            //Found exposure that was just added
                            break;

                    if (smallerRadius == 0 || smallerExposure.radius >= thisRadius)
                    {
                        expo[expInd] = thisInst.topo.CalcExposures(UTMX, UTMY, elev, thisRadius, thisExponent, 1, thisInst.topo, numWD);
                        if (thisInst.topo.gotSR == true)
                            thisInst.topo.CalcSRDH(ref expo[expInd], UTMX, UTMY, thisRadius, thisExponent, numWD);
                    }
                    else
                    {
                        expo[expInd] = thisInst.topo.CalcExposuresWithSmallerRadius(UTMX, UTMY, elev, thisRadius, thisExponent, 1, smallerRadius, smallerExposure, numWD);
                        if (thisInst.topo.gotSR == true)
                            thisInst.topo.CalcSRDHwithSmallerRadius(ref expo[expInd], UTMX, UTMY, thisRadius, thisExponent, 1, smallerRadius, smallerExposure, numWD);
                    }

                    smallerExposure = expo[expInd];
                    smallerRadius = smallerExposure.radius;
                }
            }
             
            if (gridStats.stats == null)
                gridStats.GetGridArrayAndCalcStats(UTMX, UTMY, thisInst);
                        
            // Calc P10 UW Crosswind and Parallel Grade            
            for (int expInd = 0; expInd < thisInst.radiiList.ThisCount; expInd++) {
                expo[expInd].UW_P10CrossGrade = new double[numWD];
                expo[expInd].UW_ParallelGrade = new double[numWD];
            }

            for (int WD_sec = 0; WD_sec < numWD; WD_sec++)
            {
                double UW_CW_Grade = thisInst.topo.CalcP10_UW_CrosswindGrade(UTMX, UTMY, thisInst.radiiList, WD_sec, numWD);
                double UW_PL_Grade = thisInst.topo.CalcP10_UW_ParallelGrade(UTMX, UTMY, thisInst.radiiList, WD_sec, numWD);
                for (int expInd = 0; expInd <= thisInst.radiiList.ThisCount - 1; expInd++) {
                    expo[expInd].UW_P10CrossGrade[WD_sec] = UW_CW_Grade;
                    expo[expInd].UW_ParallelGrade[WD_sec] = UW_PL_Grade;
                }
            }
        }        

        public void CalcP10UW_Grade(TopoInfo topo, InvestCollection radiiList, string savedFileName)
        {
            // Calculates the upwind crossing and parallel terrain grade - to be used in flow around vs. flow over hills algorithm
            int numRadii = ExposureCount;

            if (numRadii == 0) 
                return;

            int numWD = 0;
            try {
                numWD = expo[0].expo.Length;
            }
            catch  { 
                return;
            }

            bool Calc_Grade = false;

            try {
                if (expo[0].UW_P10CrossGrade[0] == 0 || expo[0].UW_ParallelGrade[0] == 0)
                    Calc_Grade = true;
            }
            catch  {
                return;
            }

            if (Calc_Grade == true )
            {
                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                {
                    double UW_CW_Grade = topo.CalcP10_UW_CrosswindGrade(UTMX, UTMY, radiiList, WD_Ind, numWD);
                    double UW_PL_Grade = topo.CalcP10_UW_ParallelGrade(UTMX, UTMY, radiiList, WD_Ind, numWD);
                    
                    for (int i = 0; i < numRadii; i++)
                    {
                        if (WD_Ind == 0) expo[i].UW_P10CrossGrade = new double[numWD];
                        if (WD_Ind == 0) expo[i].UW_ParallelGrade = new double[numWD];
                        expo[i].UW_P10CrossGrade[WD_Ind] = UW_CW_Grade;
                        expo[i].UW_ParallelGrade[WD_Ind] = UW_PL_Grade;
                    }
                }
            }
            
        }

        public void GetFlowSepNodes(NodeCollection nodeList, Continuum thisInst, Nodes[] newNodes, double[] windRose)
        {
            // Gets nodes for upwind flow separation model
            int numWD = 0;

            try {
                numWD = windRose.Length;
            }
            catch { 
                return;
            }

            flowSepNodes = nodeList.FindAllFlowSeps(this, thisInst, numWD);
            
        }
            
    }
}
