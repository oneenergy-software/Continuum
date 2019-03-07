using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class Met
    {
        public string name; // Name of met site
        public double WS; // Wind speed at height 1
        public double height; // height 1
        public double[] sectorWS_Ratio; // Directional wind speed ratios
        public double[] windRose; // Wind direction frequency (wind rose)
        public double[,] sectorWS_Dist; // Sectorwise wind speed distribution i = Sector num, j = WS interval
        public double[] WS_Dist; // Overall Wind speed distribution
        public double WS_FirstInt; // First wind speed in distribution
        public double WS_IntSize; // Wind speed bin size
        public double UTMX; // UTM X Coordinate, m
        public double UTMY; // UTM Y Coordinate, m
        public double elev; // Elevation
        public Exposure[] expo; // Calculated exposure and SR/DH
        public Grid_Info gridStats = new Grid_Info(); // Calculated terrain complexity
        public NodeCollection.Sep_Nodes[] flowSepNodes; // If flow separation model enabled, nodes surrounding met where flow separation occurs

        public int ExposureCount
        {
            get { if (expo == null)
                    return 0;
                else
                    return expo.Length; }
        }

        public void AddExposure(int radius, double exponent, int numSectors)
        {
            // Add exposure to list
            int expCount = 0;
            if (expo != null) expCount = expo.Length;
            int insertIndex = 0;

            if (expCount > 0)
            {
                if (radius > expo[expCount - 1].radius)  // Larger radius than largest in list
                    insertIndex = expCount - 1;
                else {
                    for (int i = 0; i <= expCount - 2; i++)
                    {
                        if (expo[i].radius < radius && expo[i + 1].radius >= radius) {
                            insertIndex = i;
                            break;
                        }
                    }
                }

                Exposure[] existingExpos = new Exposure[expCount];
                for (int j = 0; j < expCount; j++)
                    existingExpos[j] = expo[j];

                expo = new Exposure[expCount + 1];

                for (int j = 0; j <= insertIndex; j++)
                    expo[j] = existingExpos[j];


                expo[insertIndex + 1] = new Exposure();
                expo[insertIndex + 1].radius = radius;
                expo[insertIndex + 1].exponent = exponent;
                expo[insertIndex + 1].numSectors = numSectors;

                for (int j = insertIndex + 2; j <= expCount; j++)
                    expo[j] = existingExpos[j - 1];
            }

            else {
                expo = new Exposure[1];
                expo[0] = new Exposure();
                expo[0].radius = radius;
                expo[0].exponent = exponent;
                expo[0].numSectors = numSectors;

            }

        }

        public void ClearExposures()
        {
            // Clear all calculated exposure and SRDH
            expo = null;
        }

        public bool IsNewExposure(int radius, double exponent, int numSectors)
        {
            // Returns true if the exposure with specified radius and exponent has not been calculated

            bool isNew = true;
            if (expo == null)
                isNew = true;
            else
            {
                for (int i = 0; i < expo.Length; i++)
                {
                    if (expo[i].exponent == exponent && expo[i].radius == radius && expo[i].numSectors == numSectors) // the exposures based on radius and exp combo and number of sectors to avg already calculated
                    {
                        isNew = false;
                        break;
                    }                    
                }
            }

            return isNew;

        }

        public bool IsNewSRDH(int radius, double exponent, int numSectors)
        {

            bool isNew = true;
            if (expo == null)
                isNew = true;
            else
            {
                for (int i = 0; i < expo.Length; i++)
                {
                    if (expo[i].exponent == exponent && expo[i].radius == radius && expo[i].numSectors == numSectors && expo[i].SR != null)
                    {
                        isNew = false;
                        break;
                    }
                                       
                }
            }
            return isNew;
        }   
          
        public void CalcAvgWS()
        {
            // Calculates overall average wind speed using sectorwise wind speed distribution and wind rose (i.e. TAB file)

            // first calc WS dist over all sectors
            int numWS = sectorWS_Dist.GetUpperBound(1) + 1;
            WS_Dist = new double[numWS];

            double sumRose = 0;

            for (int i = 0; i < numWS; i++)
            {
                sumRose = 0;
                for (int j = 0; j <= sectorWS_Dist.GetUpperBound(0); j++)
                {
                    WS_Dist[i] = WS_Dist[i] + sectorWS_Dist[j, i] * windRose[j];
                    sumRose = sumRose + windRose[j];
                }
                WS_Dist[i] = WS_Dist[i] / sumRose / 1000;
            }

            WS = 0;
            double sumDist = 0;

            for (int i = 0; i < numWS; i++)
            {
                WS = WS + WS_Dist[i] * (WS_FirstInt + i * WS_IntSize - WS_IntSize / 2);
                sumDist = sumDist + WS_Dist[i];
            }

            WS = WS / sumDist;

        }

        public void CalcSectorWS_Ratios()
        {
            // Calculate directional wind speed ratios

            if (windRose == null || WS == 0) return;

            int numWS = WS_Dist.Length;
            int numWD = windRose.Length;

            double[] WS_by_dir = new double[numWD];
            sectorWS_Ratio = new double[numWD];
            
            double sumWS = 0;
                    
            for (int i = 0; i < numWD; i++)
            {
                sumWS = 0;
                for (int j = 0; j <= numWS - 1; j++)
                {
                    WS_by_dir[i] = WS_by_dir[i] + sectorWS_Dist[i, j] * (WS_FirstInt + j * WS_IntSize - WS_IntSize / 2);
                    sumWS = sumWS + sectorWS_Dist[i, j];
                }
                WS_by_dir[i] = WS_by_dir[i] / sumWS;
            }

            for (int i = 0; i < numWD; i++)
                sectorWS_Ratio[i] = WS_by_dir[i] / WS;
        }
            

        public void GetFlowSepNodes(NodeCollection nodeList, Continuum thisInst)
        {

            // if flow separation model is enabled, this function searches for points surrounding met where flow separation will occur 
            if (windRose == null) return;
            int numWD = windRose.Length;

            Nodes thisNode = nodeList.GetMetNode(this);
            Nodes[] newNodesNull = null;
            flowSepNodes = nodeList.FindAllFlowSeps(thisNode, thisInst, numWD, ref newNodesNull);


        }
    }
}
