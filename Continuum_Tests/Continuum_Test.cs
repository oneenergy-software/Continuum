using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Threading;
using System.IO;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Continuum_Tests
{
    [TestClass]
    public class Continuum_Test
    {
        [TestMethod]
        public void TopoInfo_Calc_Elevs_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 264598;
            int thisY = 4537698;

            double This_Elev = thisInst.topo.CalcElevs(thisX, thisY);
            Assert.AreEqual(This_Elev, 248.139, 0.25, "Wrong elevation Test 1");

            // Test 2
            thisX = 276898;
            thisY = 4568630;

            This_Elev = thisInst.topo.CalcElevs(thisX, thisY);
            Assert.AreEqual(This_Elev, 211.528, 0.25, "Wrong elevation Test 2");

            // Test 3
            thisX = 292298;
            thisY = 4541030;

            This_Elev = thisInst.topo.CalcElevs(thisX, thisY);
            Assert.AreEqual(This_Elev, 247.330, 0.25, "Wrong elevation Test 3");

            thisInst.Close();
        }

        [TestMethod]
        public void TopoInfo_CalcExposures_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 277627;
            int thisY = 4553420;
            double This_Elev = 248.31f;
            int radius = 4000;
            double[] Dummy_Rose = new double[16];

            Exposure thisExpo = thisInst.topo.CalcExposures(thisX, thisY, This_Elev, radius, 1.0f, 1, thisInst.topo, 16);

            Assert.AreEqual(thisExpo.expo[0], 6.6866, 0.05);
            Assert.AreEqual(thisExpo.expo[1], 5.4985, 0.05);
            Assert.AreEqual(thisExpo.expo[2], 3.9700, 0.05);
            Assert.AreEqual(thisExpo.expo[3], 2.8177, 0.05);
            Assert.AreEqual(thisExpo.expo[4], 1.3148, 0.05);
            Assert.AreEqual(thisExpo.expo[5], 1.4562, 0.05);
            Assert.AreEqual(thisExpo.expo[6], -0.5943, 0.05);
            Assert.AreEqual(thisExpo.expo[7], 0.0693, 0.05);
            Assert.AreEqual(thisExpo.expo[8], -0.2009, 0.05);
            Assert.AreEqual(thisExpo.expo[9], -2.6497, 0.05);
            Assert.AreEqual(thisExpo.expo[10], -2.7475, 0.05);
            Assert.AreEqual(thisExpo.expo[11], -1.0163, 0.05);
            Assert.AreEqual(thisExpo.expo[12], 2.4154, 0.05);
            Assert.AreEqual(thisExpo.expo[13], 4.3654, 0.05);
            Assert.AreEqual(thisExpo.expo[14], 5.8747, 0.05);
            Assert.AreEqual(thisExpo.expo[15], 7.0703, 0.05);

            // Test 2
            thisX = 281243;
            thisY = 4550892;
            This_Elev = 250.340f;
            radius = 10000;

            thisExpo = thisInst.topo.CalcExposures(thisX, thisY, This_Elev, radius, 1.0f, 1, thisInst.topo, 16);

            Assert.AreEqual(thisExpo.expo[0], 10.921, 0.05);
            Assert.AreEqual(thisExpo.expo[1], 9.617, 0.05);
            Assert.AreEqual(thisExpo.expo[2], 6.969, 0.05);
            Assert.AreEqual(thisExpo.expo[3], 3.4540, 0.05);
            Assert.AreEqual(thisExpo.expo[4], -1.1413, 0.05);
            Assert.AreEqual(thisExpo.expo[5], -1.0659, 0.05);
            Assert.AreEqual(thisExpo.expo[6], 4.3903, 0.05);
            Assert.AreEqual(thisExpo.expo[7], 4.8372, 0.05);
            Assert.AreEqual(thisExpo.expo[8], 6.9035, 0.05);
            Assert.AreEqual(thisExpo.expo[9], 6.8344, 0.05);
            Assert.AreEqual(thisExpo.expo[10], 7.553, 0.05);
            Assert.AreEqual(thisExpo.expo[11], 7.537, 0.05);
            Assert.AreEqual(thisExpo.expo[12], 2.0476, 0.05);
            Assert.AreEqual(thisExpo.expo[13], 4.1346, 0.05);
            Assert.AreEqual(thisExpo.expo[14], 8.5620, 0.05);
            Assert.AreEqual(thisExpo.expo[15], 11.0025, 0.05);

            thisInst.Close();
        }

        [TestMethod]
        public void TopoInfo_Read_GeoTiff_Topo_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            thisInst.savedParams.savedFileName = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing GeoTiff load.cfm";
            thisInst.SaveFile();
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 17;
            thisInst.UTM_conversions.hemisphere = "Northern";

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\NED_15051924 smaller\\NED_15051924\\NED_15051924.tif";
            thisInst.topo.ReadGeoTiffTopo(Filename, thisInst);
            thisInst.topo.topoNumXY.X.calcs = thisInst.topo.topoNumXY.X.all;
            thisInst.topo.topoNumXY.Y.calcs = thisInst.topo.topoNumXY.Y.all;
            thisInst.topo.elevsForCalcs = thisInst.topo.topoElevs;

            // Test 1
            double UTMX = 268339.8f;
            double UTMY = 4564816.2f;
            double elev = thisInst.topo.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(214.557, elev, 0.1, "Wrong elevation Test 1");

            // Test 2
            UTMX = 291638.58f;
            UTMY = 4555225.2f;
            elev = thisInst.topo.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(242.645, elev, 0.1, "Wrong elevation Test 2");

            // Test 3
            UTMX = 268193.992f;
            UTMY = 4550969.3f;
            elev = thisInst.topo.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(245.988, elev, 0.1, "Wrong elevation Test 3");

            // Test 4
            UTMX = 285429.3f;
            UTMY = 4562086f;
            elev = thisInst.topo.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(225.246, elev, 0.1, "Wrong elevation Test 3");

            thisInst.Close();
        }

        [TestMethod]
        public void TopoInfo_Read_GeoTiff_LC_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            thisInst.savedParams.savedFileName = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing LandCover load.cfm";
            thisInst.SaveFile();
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 17;
            thisInst.UTM_conversions.hemisphere = "Northern";

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\LC1178895271 smaller\\LC1178895271.tif";
            thisInst.topo.SetUS_NLCD_Key();
            thisInst.topo.ReadGeoTiffLandCover(Filename, thisInst);
            thisInst.topo.LC_NumXY.X.calcs = thisInst.topo.LC_NumXY.X.all;
            thisInst.topo.LC_NumXY.Y.calcs = thisInst.topo.LC_NumXY.Y.all;

            // Test 1
            double UTMX = 273126f;
            double UTMY = 4546996f;
            int LC_Code = thisInst.topo.GetLC_Code(UTMX, UTMY);

            Assert.AreEqual(41, LC_Code, 0, "Wrong land cover code Test 1");

            // Test 2
            UTMX = 275955f;
            UTMY = 4544692f;
            LC_Code = thisInst.topo.GetLC_Code(UTMX, UTMY);

            Assert.AreEqual(23, LC_Code, 0, "Wrong land cover code Test 2");

            // Test 3
            UTMX = 292223.4f;
            UTMY = 4542114.2f;
            LC_Code = thisInst.topo.GetLC_Code(UTMX, UTMY);

            Assert.AreEqual(95, LC_Code, 0, "Wrong land cover code Test 3");

            thisInst.Close();
        }

        [TestMethod]
        public void TopoInfo_Calc_SR_and_DH_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 277627;
            int thisY = 4553420;
            double This_Elev = 248.31f;
            int radius = 10000;
            double[] Dummy_Rose = new double[16];

            Exposure thisExpo = thisInst.topo.CalcExposures(thisX, thisY, This_Elev, radius, 1.0f, 1, thisInst.topo, 16);
            thisInst.topo.CalcSRDH(ref thisExpo, thisX, thisY, radius, 1.0f, 16);

            Assert.AreEqual(thisExpo.SR[0], 0.36083, 0.05);
            Assert.AreEqual(thisExpo.SR[1], 0.32344, 0.05);
            Assert.AreEqual(thisExpo.SR[2], 0.3057, 0.05);
            Assert.AreEqual(thisExpo.SR[3], 0.27809, 0.05);
            Assert.AreEqual(thisExpo.SR[4], 0.2954, 0.05);
            Assert.AreEqual(thisExpo.SR[5], 0.3311, 0.05);
            Assert.AreEqual(thisExpo.SR[6], 0.5134, 0.05);
            Assert.AreEqual(thisExpo.SR[7], 0.5157, 0.05);
            Assert.AreEqual(thisExpo.SR[8], 0.6470, 0.05);
            Assert.AreEqual(thisExpo.SR[9], 0.5047, 0.05);
            Assert.AreEqual(thisExpo.SR[10], 0.3617, 0.05);
            Assert.AreEqual(thisExpo.SR[11], 0.3280, 0.05);
            Assert.AreEqual(thisExpo.SR[12], 0.3603, 0.05);
            Assert.AreEqual(thisExpo.SR[13], 0.2932, 0.05);
            Assert.AreEqual(thisExpo.SR[14], 0.2955, 0.05);
            Assert.AreEqual(thisExpo.SR[15], 0.3386, 0.05);

            Assert.AreEqual(thisExpo.dispH[0], 2.88253, 0.05);
            Assert.AreEqual(thisExpo.dispH[1], 2.76889, 0.05);
            Assert.AreEqual(thisExpo.dispH[2], 2.41572, 0.05);
            Assert.AreEqual(thisExpo.dispH[3], 1.90553, 0.05);
            Assert.AreEqual(thisExpo.dispH[4], 1.86140, 0.05);
            Assert.AreEqual(thisExpo.dispH[5], 2.63378, 0.05);
            Assert.AreEqual(thisExpo.dispH[6], 3.75947, 0.05);
            Assert.AreEqual(thisExpo.dispH[7], 3.26826, 0.05);
            Assert.AreEqual(thisExpo.dispH[8], 3.92466, 0.05);
            Assert.AreEqual(thisExpo.dispH[9], 2.96067, 0.05);
            Assert.AreEqual(thisExpo.dispH[10], 2.5779, 0.05);
            Assert.AreEqual(thisExpo.dispH[11], 2.6053, 0.05);
            Assert.AreEqual(thisExpo.dispH[12], 3.3680, 0.05);
            Assert.AreEqual(thisExpo.dispH[13], 2.0482, 0.05);
            Assert.AreEqual(thisExpo.dispH[14], 2.0700, 0.05);
            Assert.AreEqual(thisExpo.dispH[15], 2.5531, 0.05);

            thisInst.Close();
        }

        [TestMethod]
        public void TopoInfo_CalcExposures_with_smaller_R_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 281043;
            int thisY = 4551532;
            double This_Elev = thisInst.topo.CalcElevs(thisX, thisY);
            int minRadius = 6000;
            int maxRadius = 8000;
            double[] Dummy_Rose = new double[16];

            Exposure smallerExposure = thisInst.topo.CalcExposures(thisX, thisY, This_Elev, minRadius, 1.0f, 1, thisInst.topo, 16);
            Exposure thisExpo = thisInst.topo.CalcExposuresWithSmallerRadius(thisX, thisY, This_Elev, maxRadius, 1.0f, 1, minRadius, smallerExposure, 16);

            Assert.AreEqual(thisExpo.expo[0], 7.3685, 5);
            Assert.AreEqual(thisExpo.expo[1], 6.7927, 0.05);
            Assert.AreEqual(thisExpo.expo[2], 4.6486, 0.05);
            Assert.AreEqual(thisExpo.expo[3], 2.3020, 0.05);
            Assert.AreEqual(thisExpo.expo[4], -1.388, 0.05);
            Assert.AreEqual(thisExpo.expo[5], -3.8222, 0.05);
            Assert.AreEqual(thisExpo.expo[6], -0.0171, 0.05);
            Assert.AreEqual(thisExpo.expo[7], 1.6268, 0.05);
            Assert.AreEqual(thisExpo.expo[8], 3.7281, 0.05);
            Assert.AreEqual(thisExpo.expo[9], 3.9263, 0.05);
            Assert.AreEqual(thisExpo.expo[10], 4.366, 0.05);
            Assert.AreEqual(thisExpo.expo[11], 3.058, 0.05);
            Assert.AreEqual(thisExpo.expo[12], -1.034, 0.05);
            Assert.AreEqual(thisExpo.expo[13], 2.1024, 0.05);
            Assert.AreEqual(thisExpo.expo[14], 5.4400, 0.05);
            Assert.AreEqual(thisExpo.expo[15], 7.4735, 0.05);
        }

        [TestMethod]
        public void TopoInfo_Calc_SRDH_with_smaller_R_BW_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 277893;
            int thisY = 4554412;
            double This_Elev = thisInst.topo.CalcElevs(thisX, thisY);
            int minRadius = 4000;
            int maxRadius = 6000;
            double[] Dummy_Rose = new double[16];
            Exposure thisExpo = new Exposure();

            Exposure smallerExposure = thisInst.topo.CalcExposures(thisX, thisY, This_Elev, minRadius, 1.0f, 1, thisInst.topo, 16);
            thisInst.topo.CalcSRDH(ref smallerExposure, thisX, thisY, minRadius, 1.0f, 16);
            thisInst.topo.CalcSRDHwithSmallerRadius(ref thisExpo, thisX, thisY, maxRadius, 1.0f, 1, minRadius, smallerExposure, 16);

            Assert.AreEqual(thisExpo.SR[0], 0.34706, 0.05);
            Assert.AreEqual(thisExpo.SR[1], 0.3182, 0.05);
            Assert.AreEqual(thisExpo.SR[2], 0.3357, 0.05);
            Assert.AreEqual(thisExpo.SR[3], 0.3239, 0.05);
            Assert.AreEqual(thisExpo.SR[4], 0.2600, 0.05);
            Assert.AreEqual(thisExpo.SR[5], 0.3065, 0.05);
            Assert.AreEqual(thisExpo.SR[6], 0.3375, 0.05);
            Assert.AreEqual(thisExpo.SR[7], 0.4300, 0.05);
            Assert.AreEqual(thisExpo.SR[8], 0.5101, 0.05);
            Assert.AreEqual(thisExpo.SR[9], 0.4934, 0.05);
            Assert.AreEqual(thisExpo.SR[10], 0.324, 0.05);
            Assert.AreEqual(thisExpo.SR[11], 0.3786, 0.05);
            Assert.AreEqual(thisExpo.SR[12], 0.3258, 0.05);
            Assert.AreEqual(thisExpo.SR[13], 0.3173, 0.05);
            Assert.AreEqual(thisExpo.SR[14], 0.3377, 0.05);
            Assert.AreEqual(thisExpo.SR[15], 0.3912, 0.05);

            Assert.AreEqual(thisExpo.dispH[0], 2.911, 0.05);
            Assert.AreEqual(thisExpo.dispH[1], 2.679, 0.05);
            Assert.AreEqual(thisExpo.dispH[2], 3.056, 0.05);
            Assert.AreEqual(thisExpo.dispH[3], 2.783, 0.05);
            Assert.AreEqual(thisExpo.dispH[4], 1.493, 0.05);
            Assert.AreEqual(thisExpo.dispH[5], 2.542, 0.05);
            Assert.AreEqual(thisExpo.dispH[6], 2.512, 0.05);
            Assert.AreEqual(thisExpo.dispH[7], 2.989, 0.05);
            Assert.AreEqual(thisExpo.dispH[8], 3.304, 0.05);
            Assert.AreEqual(thisExpo.dispH[9], 3.619, 0.05);
            Assert.AreEqual(thisExpo.dispH[10], 2.248, 0.05);
            Assert.AreEqual(thisExpo.dispH[11], 3.496, 0.05);
            Assert.AreEqual(thisExpo.dispH[12], 2.542, 0.05);
            Assert.AreEqual(thisExpo.dispH[13], 2.626, 0.05);
            Assert.AreEqual(thisExpo.dispH[14], 3.083, 0.05);
            Assert.AreEqual(thisExpo.dispH[15], 3.448, 0.05);

            thisInst.Close();
        }

        [TestMethod]
        public void Find_Sectors_for_Grid_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            double[] windRose = new double[16];
            windRose[0] = 0.04575f;
            windRose[1] = 0.04290f;
            windRose[2] = 0.04477f;
            windRose[3] = 0.05054f;
            windRose[4] = 0.04887f;
            windRose[5] = 0.03913f;
            windRose[6] = 0.03403f;
            windRose[7] = 0.04028f;
            windRose[8] = 0.05905f;
            windRose[9] = 0.08955f;
            windRose[10] = 0.10486f;
            windRose[11] = 0.09677f;
            windRose[12] = 0.090794f;
            windRose[13] = 0.082136f;
            windRose[14] = 0.072797f;
            windRose[15] = 0.057713f;

            Grid_Info gridStats = new Grid_Info();
            bool[] Sectors_to_use = gridStats.FindSectorsForGrid(windRose);

            Assert.AreEqual(true, Sectors_to_use[0], "Wrong Sector 0");
            Assert.AreEqual(true, Sectors_to_use[1], "Wrong Sector 1");
            Assert.AreEqual(true, Sectors_to_use[2], "Wrong Sector 2");
            Assert.AreEqual(true, Sectors_to_use[3], "Wrong Sector 3");
            Assert.AreEqual(true, Sectors_to_use[4], "Wrong Sector 4");
            Assert.AreEqual(true, Sectors_to_use[5], "Wrong Sector 5");
            Assert.AreEqual(true, Sectors_to_use[6], "Wrong Sector 6");
            Assert.AreEqual(true, Sectors_to_use[7], "Wrong Sector 7");
            Assert.AreEqual(true, Sectors_to_use[8], "Wrong Sector 8");
            Assert.AreEqual(true, Sectors_to_use[9], "Wrong Sector 9");
            Assert.AreEqual(true, Sectors_to_use[10], "Wrong Sector 10");
            Assert.AreEqual(true, Sectors_to_use[11], "Wrong Sector 11");
            Assert.AreEqual(true, Sectors_to_use[12], "Wrong Sector 12");
            Assert.AreEqual(true, Sectors_to_use[13], "Wrong Sector 13");
            Assert.AreEqual(true, Sectors_to_use[14], "Wrong Sector 14");
            Assert.AreEqual(true, Sectors_to_use[15], "Wrong Sector 15");

            // Test 2

            windRose = new double[16];
            windRose[0] = 0.06061f;
            windRose[1] = 0.05352f;
            windRose[2] = 0.04293f;
            windRose[3] = 0.01726f;
            windRose[4] = 0.01274f;
            windRose[5] = 0.03618f;
            windRose[6] = 0.07353f;
            windRose[7] = 0.13875f;
            windRose[8] = 0.11183f;
            windRose[9] = 0.12792f;
            windRose[10] = 0.09757f;
            windRose[11] = 0.05613f;
            windRose[12] = 0.03627f;
            windRose[13] = 0.02978f;
            windRose[14] = 0.0407f;
            windRose[15] = 0.06426f;

            gridStats = new Grid_Info();
            Sectors_to_use = gridStats.FindSectorsForGrid(windRose);

            Assert.AreEqual(true, Sectors_to_use[0], "Wrong Sector 0");
            Assert.AreEqual(true, Sectors_to_use[1], "Wrong Sector 1");
            Assert.AreEqual(true, Sectors_to_use[2], "Wrong Sector 2");
            Assert.AreEqual(false, Sectors_to_use[3], "Wrong Sector 3");
            Assert.AreEqual(false, Sectors_to_use[4], "Wrong Sector 4");
            Assert.AreEqual(false, Sectors_to_use[5], "Wrong Sector 5");
            Assert.AreEqual(true, Sectors_to_use[6], "Wrong Sector 6");
            Assert.AreEqual(true, Sectors_to_use[7], "Wrong Sector 7");
            Assert.AreEqual(true, Sectors_to_use[8], "Wrong Sector 8");
            Assert.AreEqual(true, Sectors_to_use[9], "Wrong Sector 9");
            Assert.AreEqual(true, Sectors_to_use[10], "Wrong Sector 10");
            Assert.AreEqual(false, Sectors_to_use[11], "Wrong Sector 11");
            Assert.AreEqual(false, Sectors_to_use[12], "Wrong Sector 12");
            Assert.AreEqual(false, Sectors_to_use[13], "Wrong Sector 13");
            Assert.AreEqual(true, Sectors_to_use[14], "Wrong Sector 14");
            Assert.AreEqual(true, Sectors_to_use[15], "Wrong Sector 15");


            thisInst.Close();

        }

        [TestMethod]
        public void Get_Closest_Node_Fixed_Grid()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            double UTMX = 274250;
            double UTMY = 4554602;

            TopoInfo.TopoGrid closestNode = thisInst.topo.GetClosestNodeFixedGrid(UTMX, UTMY, 250);
            TopoInfo.TopoGrid Closest_Node_new = thisInst.topo.GetClosestNodeFixedGrid(UTMX, UTMY, 250);

            Assert.AreEqual(closestNode.UTMX, Closest_Node_new.UTMX, 1, "Wrong UTMX");
            Assert.AreEqual(closestNode.UTMY, Closest_Node_new.UTMY, 1, "Wrong UTMY");

            UTMX = 276873;
            UTMY = 4558259;

            closestNode = thisInst.topo.GetClosestNodeFixedGrid(UTMX, UTMY, 250);
            Closest_Node_new = thisInst.topo.GetClosestNodeFixedGrid(UTMX, UTMY, 250);

            Assert.AreEqual(closestNode.UTMX, Closest_Node_new.UTMX, 1, "Wrong UTMX");
            Assert.AreEqual(closestNode.UTMY, Closest_Node_new.UTMY, 1, "Wrong UTMY");

            thisInst.Close();
        }

        [TestMethod]
        public void Get_Grid_Array_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 281043;
            int thisY = 4551532;
            double minX = thisInst.topo.topoNumXY.X.all.min;
            double minY = thisInst.topo.topoNumXY.Y.all.min;

            TopoInfo.TopoGrid[] Grid_Array = thisInst.metList.metItem[0].gridStats.GetGridArray(thisX, thisY, thisInst);

            Assert.AreEqual(Grid_Array.Length, 28, 0, "Wrong count in test 1");
            Assert.AreEqual(Grid_Array[0].UTMX, 280317.344, 0.01, "Wrong UTMX at index 0");
            Assert.AreEqual(Grid_Array[0].UTMY, 4551411.5, 0.01, "Wrong UTMY at index 0");
            Assert.AreEqual(Grid_Array[27].UTMX, 281567.344, 0.01, "Wrong UTMX at index 28");
            Assert.AreEqual(Grid_Array[27].UTMY, 4551911.5, 0.01, "Wrong UTMY at index 28");

            thisInst.Close();

            // Test 2
            thisInst = new Continuum();
            Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Great Western\\Great Western model.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            thisX = 424362;
            thisY = 4005776;
            minX = thisInst.topo.topoNumXY.X.all.min;
            minY = thisInst.topo.topoNumXY.Y.all.min;

            Grid_Array = thisInst.metList.metItem[0].gridStats.GetGridArray(thisX, thisY, thisInst);

            Assert.AreEqual(Grid_Array.Length, 17, 0, "Wrong count in test 2");
            Assert.AreEqual(Grid_Array[0].UTMX, 424088.031, 0.01, "Wrong UTMX at index 0");
            Assert.AreEqual(Grid_Array[0].UTMY, 4005085, 0.01, "Wrong UTMY at index 0");
            Assert.AreEqual(Grid_Array[16].UTMX, 424588.031, 0.01, "Wrong UTMX at index 28");
            Assert.AreEqual(Grid_Array[16].UTMY, 4006335, 0.01, "Wrong UTMY at index 28");
            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Grid_Stats_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 281043;
            int thisY = 4551532;
            double minX = thisInst.topo.topoNumXY.X.all.min;
            double minY = thisInst.topo.topoNumXY.Y.all.min;

            TopoInfo.TopoGrid[] Grid_Array = thisInst.metList.metItem[0].gridStats.GetGridArray(thisX, thisY, thisInst);

            for (int i = 0; i < Grid_Array.Length; i++)
                Grid_Array[i].elev = thisInst.topo.CalcElevs(Grid_Array[i].UTMX, Grid_Array[i].UTMY);

            Grid_Info This_Grid_Info = new Grid_Info();
            Node_table[] Dummy_nulls = null;
            This_Grid_Info.CalcGridStats(0, ref Grid_Array, ref Dummy_nulls, null, thisInst);

            Assert.AreEqual(1.0758, This_Grid_Info.stats[0].P10_UW[4], 0.01, "Wrong P10 UW Expo for Test 1");
            Assert.AreEqual(2.2546, This_Grid_Info.stats[0].P10_DW[4], 0.01, "Wrong P10 DW Expo for Test 1");

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_DW_Coeff_Test()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.setDefaultModelCoeffs(16);

            double This_Coeff = thisModel.CalcDW_Coeff(3, 2.5f, 0, "Downhill");
            Assert.AreEqual(0.069831, This_Coeff, 0.001, "Wrong downhill coeff");

            This_Coeff = thisModel.CalcDW_Coeff(48, 58, 0, "Uphill");
            Assert.AreEqual(0.0064945, This_Coeff, 0.001, "Wrong uphill coeff");

            This_Coeff = thisModel.CalcDW_Coeff(200, 180, 0, "Turbulent");
            Assert.AreEqual(-0.00300, This_Coeff, 0.001, "Wrong Turbulent coeff");

        }

        [TestMethod]
        public void Calc_UW_Coeff_Test()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.setDefaultModelCoeffs(16);

            double This_Coeff = thisModel.CalcUW_Coeff(2, 1, 0, "Downhill");
            Assert.AreEqual(-0.091025, This_Coeff, 0.0001, "Wrong downhill coeff");

            This_Coeff = thisModel.CalcUW_Coeff(44, 52, 0, "Uphill");
            Assert.AreEqual(-0.006953, This_Coeff, 0.0001, "Wrong uphill coeff");

            This_Coeff = thisModel.CalcUW_Coeff(13, 11, 0, "SpdUp");
            Assert.AreEqual(0.00357195, This_Coeff, 0.0001, "Wrong Speed-Up coeff");

        }

        [TestMethod]
        public void Get_Flow_Type_Test()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.setDefaultModelCoeffs(16);


            string flowType = thisModel.GetFlowType(-10, -10, 0, "UW", null, 0, false, 0); // Test 1
            Assert.AreSame("Downhill", flowType, "Wrong flow type: Test 1");

            flowType = thisModel.GetFlowType(-10, -10, 0, "DW", null, 0, false, 0); // Test 2
            Assert.AreSame("Uphill", flowType, "Wrong flow type: Test 2");

            flowType = thisModel.GetFlowType(-10, 10, 0, "UW", null, 0, false, 0); // Test 3
            Assert.AreSame("Downhill", flowType, "Wrong flow type: Test 3");

            flowType = thisModel.GetFlowType(-10, 10, 0, "DW", null, 0, false, 0); // Test 4
            Assert.AreSame("Downhill", flowType, "Wrong flow type: Test 4");

            flowType = thisModel.GetFlowType(10, -10, 0, "UW", null, 0, false, 0); // Test 5
            Assert.AreSame("SpdUp", flowType, "Wrong flow type: Test 5");

            flowType = thisModel.GetFlowType(10, -10, 0, "DW", null, 0, false, 0); // Test 6
            Assert.AreSame("Uphill", flowType, "Wrong flow type: Test 6");

            flowType = thisModel.GetFlowType(10, 10, 0, "UW", null, 0, false, 0); // Test 7
            Assert.AreSame("SpdUp", flowType, "Wrong flow type: Test 7");

            flowType = thisModel.GetFlowType(10, 10, 0, "DW", null, 0, false, 0); // Test 8
            Assert.AreSame("Downhill", flowType, "Wrong flow type: Test 8");

            flowType = thisModel.GetFlowType(30, 10, 0, "UW", null, 0, false, 0); // Test 9
            Assert.AreSame("Uphill", flowType, "Wrong flow type: Test 9");

            flowType = thisModel.GetFlowType(10, 10, 0, "DW", null, 0, false, 0); // Test 10
            Assert.AreSame("Downhill", flowType, "Wrong flow type: Test 10");

            flowType = thisModel.GetFlowType(30, -10, 0, "UW", null, 0, false, 0); // Test 11
            Assert.AreSame("Uphill", flowType, "Wrong flow type: Test 11");

            flowType = thisModel.GetFlowType(30, -10, 0, "DW", null, 0, false, 0); // Test 12
            Assert.AreSame("Uphill", flowType, "Wrong flow type: Test 12");

            // Turbulent tests
            NodeCollection.Sep_Nodes[] This_Sep_Node = new NodeCollection.Sep_Nodes[1];
            This_Sep_Node[0].highNode = new Nodes();
            This_Sep_Node[0].highNode.AddExposure(4000, 1, 1, 16);
            This_Sep_Node[0].highNode.expo[0].expo[0] = 200; // UW expo
            This_Sep_Node[0].highNode.expo[0].expo[8] = 200; // DW expo

            flowType = thisModel.GetFlowType(-10, 10, 0, "UW", This_Sep_Node, 8, true, 0); // Test 13
            Assert.AreSame("Turbulent", flowType, "Wrong flow type: Test 13");

            flowType = thisModel.GetFlowType(200, 200, 0, "DW", null, 8, true, 0); // Test 14
            Assert.AreSame("Turbulent", flowType, "Wrong flow type: Test 14");

        }

        [TestMethod]
        public void Get_DeltaWS_DW_Expo_Test()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.setDefaultModelCoeffs(16);
            thisModel.radius = 4000;

            ModelCollection modelList = new ModelCollection();
            ModelCollection.Coeff_Delta_WS[] This_Delta_WS = new ModelCollection.Coeff_Delta_WS[0];

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(6, 10, 10, 20, 30, 20, 20, thisModel, 0, false); // Test 1
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.20205, 0.001, "Wrong delta WS in Test 1");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(6, 10, 10, 15, -10, 30, 30, thisModel, 0, false); // Test 2
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.23250, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.09801, 0.001, "Wrong delta WS in Test 2");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(6, 10, 10, -8, -20, 5, 5, thisModel, 0, false); // Test 3
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.35988, 0.001, "Wrong delta WS in Test 3");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(6, 10, 10, -4, 15, 8, 8, thisModel, 0, false); // Test 4
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.089461, 0.001, "Wrong delta WS in Test 4");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.551676, 0.001, "Wrong delta WS in Test 4");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(10, 50, 200, 50, 200, 220, 220, thisModel, 0, true); // Test 5
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.316054, 0.001, "Wrong delta WS in Test 5");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.21193, 0.001, "Wrong delta WS in Test 5");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(8, 180, 150, 130, 75, 100, 100, thisModel, 0, true); // Test 6
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.208015, 0.001, "Wrong delta WS in Test 6");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.07055, 0.001, "Wrong delta WS in Test 6");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(8, 75, 125, -10, 160, 110, 110, thisModel, 0, true); // Test 7
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.043555, 0.001, "Wrong delta WS in Test 7");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.994433, 0.001, "Wrong delta WS in Test 7");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.04355, 0.001, "Wrong delta WS in Test 7");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(8, 160, 140, 200, -5, 160, 160, thisModel, 0, true); // Test 8
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.344724, 0.001, "Wrong delta WS in Test 8");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.518931, 0.001, "Wrong delta WS in Test 8");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.01723, 0.001, "Wrong delta WS in Test 8");

        }

        [TestMethod]
        public void Get_DeltaWS_UW_Expo()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.setDefaultModelCoeffs(16);
            thisModel.radius = 4000;

            ModelCollection modelList = new ModelCollection();
            ModelCollection.Coeff_Delta_WS[] This_Delta_WS = new ModelCollection.Coeff_Delta_WS[0];

            NodeCollection.Sep_Nodes[] Sep_Node_1 = new NodeCollection.Sep_Nodes[1];
            NodeCollection.Sep_Nodes[] Sep_Node_2 = new NodeCollection.Sep_Nodes[1];

            NodeCollection.Node_UTMs Node_1 = new NodeCollection.Node_UTMs();
            Node_1.UTMX = 280560;
            Node_1.UTMY = 4558410;
            NodeCollection.Node_UTMs Node_2 = new NodeCollection.Node_UTMs();
            Node_2.UTMX = 280860;
            Node_2.UTMY = 4552410;

            // Test 1: Scenario 1, Site 1 and Site 2 do not have turb_end_nodes (i.e. Both sites inside turbulent zone)
            Sep_Node_1[0].highNode = new Nodes();
            Sep_Node_1[0].highNode.UTMX = 280610;
            Sep_Node_1[0].highNode.UTMY = 4558900;
            Sep_Node_1[0].highNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            Sep_Node_1[0].highNode.expo[0].expo[8] = 200;
            Sep_Node_2[0].highNode = new Nodes();
            Sep_Node_2[0].highNode.UTMX = 279600;
            Sep_Node_2[0].highNode.UTMY = 4550266;
            Sep_Node_2[0].highNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_2[0].highNode.expo[0].expo[0] = 150;
            Sep_Node_2[0].highNode.expo[0].expo[8] = 150;

            Sep_Node_1[0].turbEndNode = null;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-10, 0, 20, 30, 80, 80, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725, 0.001, "Wrong delta WS in Test 1");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.265671, 0.001, "Wrong delta WS in Test 1");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.31577, 0.001, "Wrong delta WS in Test 1");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -2.39337, 0.001, "Wrong delta WS in Test 1");

            // Test 2: Scenario 1, Site 1 and Site 2 both have turb_end_nodes (i.e. Both sites outside turbulent zone)
            Sep_Node_1[0].turbEndNode = new Nodes();
            Sep_Node_2[0].turbEndNode = new Nodes();
            Sep_Node_1[0].turbEndNode.UTMX = 280000;
            Sep_Node_1[0].turbEndNode.UTMY = 4558000;
            Sep_Node_1[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_1[0].turbEndNode.expo[0].expo[0] = -2;
            Sep_Node_1[0].turbEndNode.expo[0].expo[8] = 25;
            Sep_Node_2[0].turbEndNode.UTMX = 279800;
            Sep_Node_2[0].turbEndNode.UTMY = 4551500;
            Sep_Node_2[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_2[0].turbEndNode.expo[0].expo[0] = -14;
            Sep_Node_2[0].turbEndNode.expo[0].expo[8] = 30;

            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-15, -2, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.091724715, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.231128716, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -0.084668968, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[5].deltaWS_Expo, -0.162409197, 0.001, "Wrong delta WS in Test 2");

            // Test 3: Scenario 1, Site 1 has turb_end_node and Site 2 does not (i.e. Site 1 outside turbulent zone, Sie 2 inside turbulent zone)
            Sep_Node_2[0].turbEndNode = null;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-9, -4, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.049390231, 0.001, "Wrong delta WS in Test 3");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 3");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.231128716, 0.001, "Wrong delta WS in Test 3");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 3");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -1.68437925, 0.001, "Wrong delta WS in Test 3");

            // Test 4: Scenario 1, Site 1 does not has turb_end_node and Site 2 does (i.e. Site 1 inside turbulent zone, Sie 2 outside turbulent zone)
            Sep_Node_1[0].turbEndNode = null;
            Sep_Node_2[0].turbEndNode = new Nodes();
            Sep_Node_2[0].turbEndNode.UTMX = 279800;
            Sep_Node_2[0].turbEndNode.UTMY = 4551500;
            Sep_Node_2[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_2[0].turbEndNode.expo[0].expo[0] = -14;
            Sep_Node_2[0].turbEndNode.expo[0].expo[8] = 30;

            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-15, -2, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 4");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.231128716, 0.001, "Wrong delta WS in Test 4");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 4");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.084668968, 0.001, "Wrong delta WS in Test 4");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -0.863617561, 0.001, "Wrong delta WS in Test 4");

            // Test 5: Scenario 2, flow = Downhill
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-30, -46, 40, 60, 10, 10, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.50858436, 0.001, "Wrong delta WS in Test 5");

            // Test 6: Scenario 2, flow = Uphill
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(80, 82, 40, 60, 60, 60, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.012717211, 0.001, "Wrong delta WS in Test 6");

            // Test 7: Scenario 2, flow = Speed-Up
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(2, 10, 40, 60, 60, 60, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.011474371, 0.001, "Wrong delta WS in Test 7");

            // Test 8: Scenario 3, Flow 1 = Downhill; Flow 2 = Uphill
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-30, 82, 40, 60, 55, 55, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.312888198, 0.001, "Wrong delta WS in Test 8");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.03172509, 0.001, "Wrong delta WS in Test 8");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.409523988, 0.001, "Wrong delta WS in Test 8");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.690687097, 0.001, "Wrong delta WS in Test 8");

            // Test 9: Scenario 4, Flow 1 = Downhill; Flow 2 = Speed-Up
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-22, 18, 40, 60, 15, 15, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.536481614, 0.001, "Wrong delta WS in Test 9");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.059033754, 0.001, "Wrong delta WS in Test 9");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.47744786, 0.001, "Wrong delta WS in Test 9");

            // Test 10: Scenario 6, Flow 1 = Downhill, Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is outside turbulent zone)
            Sep_Node_1 = null;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 120;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-15, -2, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.105836209, 0.001, "Wrong delta WS in Test 10");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.022207682, 0.001, "Wrong delta WS in Test 10");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.457634857, 0.001, "Wrong delta WS in Test 10");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 10");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -0.084668968, 0.001, "Wrong delta WS in Test 10");
            Assert.AreEqual(This_Delta_WS[5].deltaWS_Expo, -2.292735546, 0.001, "Wrong delta WS in Test 10");

            // Test 11: Scenario 6, Flow 1 = Downhill, Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is outside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 20;
            Sep_Node_2[0].highNode.expo[0].expo[8] = 250;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-9, -11, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.063501726, 0.001, "Wrong delta WS in Test 11");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.021150174, 0.001, "Wrong delta WS in Test 11");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 11");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.021167242, 0.001, "Wrong delta WS in Test 11");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -1.730321988, 0.001, "Wrong delta WS in Test 11");

            // Test 12: Scenario 6, Flow 1 = Downhill, Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is inside turbulent zone)
            Sep_Node_2[0].turbEndNode = null;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 120;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-9, -11, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.063501726, 0.001, "Wrong delta WS in Test 12");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.022207682, 0.001, "Wrong delta WS in Test 12");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.457634857, 0.001, "Wrong delta WS in Test 12");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 12");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -3.814705598, 0.001, "Wrong delta WS in Test 12");

            // Test 13: Scenario 6, Flow 1 = Downhill, Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is inside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 20;
            Sep_Node_2[0].highNode.expo[0].expo[8] = 250;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-9, -11, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.063501726, 0.001, "Wrong delta WS in Test 13");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.021150174, 0.001, "Wrong delta WS in Test 13");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 13");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -3.35812825, 0.001, "Wrong delta WS in Test 13");

            // Test 14: Scenario 7, Flow 1 = Uphill, Flow 2 = Downhill 
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(40, -11, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.18621661, 0.001, "Wrong delta WS in Test 14");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.045546258, 0.001, "Wrong delta WS in Test 14");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.170506735, 0.001, "Wrong delta WS in Test 14");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 0.311177087, 0.001, "Wrong delta WS in Test 14");

            // Test 15: Scenario 8, Flow 1 = Uphill, Flow 2 = SpdUp
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(40, 15, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.18621661, 0.001, "Wrong delta WS in Test 15");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.013013216, 0.001, "Wrong delta WS in Test 15");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.173203393, 0.001, "Wrong delta WS in Test 15");

            // Test 16: Scenario 8, Flow 1 = Uphill, Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is outside turbulent zone)
            Sep_Node_2[0].turbEndNode = new Nodes();
            Sep_Node_2[0].turbEndNode.UTMX = 279800;
            Sep_Node_2[0].turbEndNode.UTMY = 4551500;
            Sep_Node_2[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_2[0].turbEndNode.expo[0].expo[0] = -14;
            Sep_Node_2[0].turbEndNode.expo[0].expo[8] = 30;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 120;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(30, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.882078678, 0.001, "Wrong delta WS in Test 16");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 16");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.186007348, 0.001, "Wrong delta WS in Test 16");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -2.734889219, 0.001, "Wrong delta WS in Test 16");

            // Test 17: Scenario 8, Flow 1 = Uphill, Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is outside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 19;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(30, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.088207868, 0.001, "Wrong delta WS in Test 17");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.004337739, 0.001, "Wrong delta WS in Test 17");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 17");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.186007348, 0.001, "Wrong delta WS in Test 17");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -1.768940413, 0.001, "Wrong delta WS in Test 17");

            // Test 18: Scenario 8, Flow 1 = Uphill, Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is inside turbulent zone)
            Sep_Node_2[0].turbEndNode = null;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 50;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(30, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.196017484, 0.001, "Wrong delta WS in Test 18");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 18");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.511794182, 0.001, "Wrong delta WS in Test 18");

            // Test 19: Scenario 8, Flow 1 = Uphill, Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is inside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 15;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(30, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.088207868, 0.001, "Wrong delta WS in Test 19");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.013013216, 0.001, "Wrong delta WS in Test 19");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 19");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -3.240582047, 0.001, "Wrong delta WS in Test 19");

            // Test 20: Flow 1 = Speed-Up; Flow 2 = Uphill
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, 30, 40, 60, 25, 25, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.026599019, 0.001, "Wrong delta WS in Test 20");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.098839891, 0.001, "Wrong delta WS in Test 20");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.072240872, 0.001, "Wrong delta WS in Test 20");

            // Test 21: Flow 1 = Speed-Up; Flow 2 = Downhill
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, -10, 40, 60, 25, 25, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.024180926, 0.001, "Wrong delta WS in Test 20");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.174626293, 0.001, "Wrong delta WS in Test 20");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.150445366, 0.001, "Wrong delta WS in Test 20");

            // Test 22: Flow 1 = Speed-Up; Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is outside turbulent zone)
            Sep_Node_2[0].turbEndNode = new Nodes();
            Sep_Node_2[0].turbEndNode.UTMX = 279800;
            Sep_Node_2[0].turbEndNode.UTMY = 4551500;
            Sep_Node_2[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_2[0].turbEndNode.expo[0].expo[0] = -14;
            Sep_Node_2[0].turbEndNode.expo[0].expo[8] = 30;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 120;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.023857564, 0.001, "Wrong delta WS in Test 22");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.970286545, 0.001, "Wrong delta WS in Test 22");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 22");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.186007348, 0.001, "Wrong delta WS in Test 22");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -2.799239524, 0.001, "Wrong delta WS in Test 22");

            // Test 23: Flow 1 = Speed-Up; Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is outside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 18;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.017350955, 0.001, "Wrong delta WS in Test 23");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 23");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.186007348, 0.001, "Wrong delta WS in Test 23");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -1.835459587, 0.001, "Wrong delta WS in Test 23");

            // Test 24: Flow 1 = Speed-Up; Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is inside turbulent zone)
            Sep_Node_2[0].turbEndNode = null;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 120;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.023857564, 0.001, "Wrong delta WS in Test 24");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.970286545, 0.001, "Wrong delta WS in Test 24");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 24");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -4.26220568, 0.001, "Wrong delta WS in Test 24");

            // Test 25: Flow 1 = Speed-Up; Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is inside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 18;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.017350955, 0.001, "Wrong delta WS in Test 25");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 25");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.298425743, 0.001, "Wrong delta WS in Test 25");

            // Test 26: Flow 1 = Turbulent; Flow 2 = Uphill (Sep Pt 1 is Uphill, Site 1 is outside turbulent zone)
            Sep_Node_1 = Sep_Node_2;
            Sep_Node_2 = null;
            Sep_Node_1[0].turbEndNode = new Nodes();
            Sep_Node_1[0].turbEndNode.UTMX = 280000;
            Sep_Node_1[0].turbEndNode.UTMY = 4558000;
            Sep_Node_1[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_1[0].turbEndNode.expo[0].expo[0] = -2;
            Sep_Node_1[0].highNode.UTMX = 280610;
            Sep_Node_1[0].highNode.UTMY = 4558900;
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 110, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 26");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 26");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.882078678, 0.001, "Wrong delta WS in Test 26");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 2.362738866, 0.001, "Wrong delta WS in Test 26");

            // Test 27: Flow 1 = Turbulent; Flow 2 = Uphill (Sep Pt 1 is SpdUp, Site 1 is outside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 110, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 27");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 27");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.002168869, 0.001, "Wrong delta WS in Test 27");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.872277803, 0.001, "Wrong delta WS in Test 27");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, 0.610551254, 0.001, "Wrong delta WS in Test 27");

            // Test 28: Flow 1 = Turbulent; Flow 2 = Uphill (Sep Pt 1 is Uphill, Site 1 is inside turbulent zone)
            Sep_Node_1[0].turbEndNode = null;
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 110, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 28");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.882078678, 0.001, "Wrong delta WS in Test 28");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 1.538804563, 0.001, "Wrong delta WS in Test 28");

            // Test 29: Flow 1 = Turbulent; Flow 2 = Uphill (Sep Pt 1 is SpdUp, Site 1 is inside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 110, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 29");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.002168869, 0.001, "Wrong delta WS in Test 29");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.872277803, 0.001, "Wrong delta WS in Test 29");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.213383048, 0.001, "Wrong delta WS in Test 29");

            // Test 30: Flow 1 = Turbulent; Flow 2 = Downhill (Sep Pt 1 is Uphill, Site 1 is outside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            Sep_Node_1[0].turbEndNode = new Nodes();
            Sep_Node_1[0].turbEndNode.UTMX = 280000;
            Sep_Node_1[0].turbEndNode.UTMY = 4558000;
            Sep_Node_1[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_1[0].turbEndNode.expo[0].expo[0] = -2;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, -20, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 30");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 30");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 1.754356481, 0.001, "Wrong delta WS in Test 30");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.045546258, 0.001, "Wrong delta WS in Test 30");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, 0.310012246, 0.001, "Wrong delta WS in Test 30");
            Assert.AreEqual(This_Delta_WS[5].deltaWS_Expo, 3.499482657, 0.001, "Wrong delta WS in Test 30");

            // Test 31: Flow 1 = Turbulent; Flow 2 = Downhill (Sep Pt 1 is SpdUp, Site 1 is outside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, -20, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 31");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 31");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.043377388, 0.001, "Wrong delta WS in Test 31");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 0.310012246, 0.001, "Wrong delta WS in Test 31");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, 1.747295046, 0.001, "Wrong delta WS in Test 31");

            // Test 32: Flow 1 = Turbulent; Flow 2 = Downhill (Sep Pt 1 is Uphill, Site 1 is inside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            Sep_Node_1[0].turbEndNode = null;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, -20, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 32");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.754356481, 0.001, "Wrong delta WS in Test 32");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.045546258, 0.001, "Wrong delta WS in Test 32");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 0.310012246, 0.001, "Wrong delta WS in Test 32");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, 2.675548355, 0.001, "Wrong delta WS in Test 32");

            // Test 33: Flow 1 = Turbulent; Flow 2 = Downhill (Sep Pt 1 is SpdUp, Site 1 is inside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, -20, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 33");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.043377388, 0.001, "Wrong delta WS in Test 33");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.310012246, 0.001, "Wrong delta WS in Test 33");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 0.923360743, 0.001, "Wrong delta WS in Test 33");

            // Test 34: Flow 1 = Turbulent; Flow 2 = Speed-Up (Sep Pt 1 is Uphill, Site 1 is outside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            Sep_Node_1[0].turbEndNode = new Nodes();
            Sep_Node_1[0].turbEndNode.UTMX = 280000;
            Sep_Node_1[0].turbEndNode.UTMY = 4558000;
            Sep_Node_1[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_1[0].turbEndNode.expo[0].expo[0] = -2;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 10, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 34");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 34");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 1.754356481, 0.001, "Wrong delta WS in Test 34");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.023857564, 0.001, "Wrong delta WS in Test 34");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, 3.211159106, 0.001, "Wrong delta WS in Test 34");

            // Test 34: Flow 1 = Turbulent; Flow 2 = Speed-Up (Sep Pt 1 is SpdUp, Site 1 is outside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 10, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 35");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 35");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.021688694, 0.001, "Wrong delta WS in Test 35");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 1.458971494, 0.001, "Wrong delta WS in Test 35");

            // Test 35: Flow 1 = Turbulent; Flow 2 = Speed-Up (Sep Pt 1 is Uphill, Site 1 is inside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            Sep_Node_1[0].turbEndNode = null;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 10, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 36");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.754356481, 0.001, "Wrong delta WS in Test 36");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.023857564, 0.001, "Wrong delta WS in Test 36");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 2.387224803, 0.001, "Wrong delta WS in Test 36");

            // Test 35: Flow 1 = Turbulent; Flow 2 = Speed-Up (Sep Pt 1 is SpdUp, Site 1 is inside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 10, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 37");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.021688694, 0.001, "Wrong delta WS in Test 37");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.635037192, 0.001, "Wrong delta WS in Test 37");

        }

        [TestMethod]
        public void Get_DeltaWS_SRDH_Test()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.setDefaultModelCoeffs(16);
            thisModel.radius = 4000;

            ModelCollection modelList = new ModelCollection();

            double This_Delta_WS = modelList.GetDeltaWS_SRDH(6, 80, 0.2f, 0.3f, 5, 7, 5, 4);
            Assert.AreEqual(-0.78659, This_Delta_WS, 0.001, "Wrong delta WS in Test 1");

            This_Delta_WS = modelList.GetDeltaWS_SRDH(10, 80, 0.05f, 0.1f, 2, 1, 3, 5);
            Assert.AreEqual(1.27466, This_Delta_WS, 0.001, "Wrong delta WS in Test 2");

        }

        [TestMethod]
        public void Get_WS_Equiv_Test()
        {
            ModelCollection modelList = new ModelCollection();
            string Filepath = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\GetWS_Equiv\\";
            string WR_Pred_file = Filepath + "WR_Pred.txt";
            StreamReader sr = new StreamReader(WR_Pred_file);

            double[] WR_Pred = new double[16];
            for (int i = 0; i < 16; i++)
                WR_Pred[i] = Convert.ToSingle(sr.ReadLine()) / 100;

            sr.Close();

            string WR_Target_file = Filepath + "WR_Target.txt";
            sr = new StreamReader(WR_Target_file);

            double[] WR_Target = new double[16];
            for (int i = 0; i < 16; i++)
                WR_Target[i] = Convert.ToSingle(sr.ReadLine()) / 100;

            sr.Close();

            string WS_Pred_file = Filepath + "WS_Pred.txt";
            sr = new StreamReader(WS_Pred_file);

            double[] WS_Pred = new double[16];
            for (int i = 0; i < 16; i++)
                WS_Pred[i] = Convert.ToSingle(sr.ReadLine());

            sr.Close();

            double[] WS_Equiv = modelList.GetWS_Equiv(WR_Pred, WR_Target, WS_Pred);

            Assert.AreEqual(WS_Equiv[0], 4.309481, 0.001, "Wrong equivalent WS in Sector 0");
            Assert.AreEqual(WS_Equiv[4], 6.333102, 0.001, "Wrong equivalent WS in Sector 4");
            Assert.AreEqual(WS_Equiv[8], 5.988821, 0.001, "Wrong equivalent WS in Sector 8");
            Assert.AreEqual(WS_Equiv[15], 5.248235, 0.001, "Wrong equivalent WS in Sector 15");

        }

        [TestMethod]
        public void Calc_RMS_Overall_and_Sectorwise_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            thisInst.Open(Filename);

            // When this model was created, the function 'CalcRMS_Overall_and_Sectorwise' was called so RMS errors exist in models. 

            Assert.AreEqual(thisInst.modelList.models[1, 0].RMS_WS_Est, 0.022025, 0.001, "Wrong RMS error at R = 4000 overall");
            Assert.AreEqual(thisInst.modelList.models[1, 3].RMS_Sect_WS_Est[0], 0.086571, 0.001, "Wrong RMS error at R = 10000 WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 3].RMS_Sect_WS_Est[10], 0.013408, 0.001, "Wrong RMS error at R = 10000 WD = 225");
            Assert.AreEqual(thisInst.modelList.models[1, 3].RMS_Sect_WS_Est[15], 0.08369, 0.001, "Wrong RMS error at R = 10000 WD = 337.5");
            Assert.AreEqual(thisInst.modelList.models[1, 2].RMS_WS_Est, 0.030119, 0.001, "Wrong RMS error at R = 8000 overall");

            thisInst.Close();
        }

        [TestMethod]
        public void Get_WS_Est_Weights_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            Nodes targetNode = new Nodes();
            targetNode.UTMX = 279653.9f;
            targetNode.UTMY = 4549630.9f;
            targetNode.elev = thisInst.topo.CalcElevs(targetNode.UTMX, targetNode.UTMY);
            targetNode.windRose = new double[16];
            targetNode.gridStats = new Grid_Info();

            targetNode.CalcGridStatsAndExposures(thisInst);

            Met[] metsUsed = thisInst.metList.GetMets(thisInst.metList.GetMetsUsed(), null);
            Model[] Models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), 4000, 10000, false);


            double[,] These_Weights = thisInst.modelList.GetWS_EstWeights(metsUsed, targetNode, Models, thisInst.metList.GetAvgWindRose());

            Assert.AreEqual(These_Weights[0, 0], 0.6363064, 0.001, "Wrong weight at Met 1, R = 4000");
            Assert.AreEqual(These_Weights[1, 0], 0.6779520, 0.001, "Wrong weight at Met 2, R = 4000");
            Assert.AreEqual(These_Weights[2, 0], 0.6857415, 0.001, "Wrong weight at Met 3, R = 4000");

            Assert.AreEqual(These_Weights[0, 1], 0.3949008, 0.001, "Wrong weight at Met 1, R = 6000");
            Assert.AreEqual(These_Weights[1, 1], 0.4096393, 0.001, "Wrong weight at Met 2, R = 6000");
            Assert.AreEqual(These_Weights[2, 1], 0.4199496, 0.001, "Wrong weight at Met 3, R = 6000");

            thisInst.Close();

        }

        [TestMethod]
        public void Do_WS_Estimate_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0 Great Western TAB.cfm";
            thisInst.Open(Filename);

            // Values read through debugger from function Update/Pat_Node_List_Update (using default model)

            // Test 1: Met 474 to Met 475
            int Met_Pair_Ind = 0;
            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
            {
                if ((thisInst.metPairList.metPairs[i].met1.name.TrimEnd() == "Met_474") && (thisInst.metPairList.metPairs[i].met2.name.TrimEnd() == "Met_475"))
                {
                    Met_Pair_Ind = i;
                    break;
                }

            }

            Pair_Of_Mets thisPair = thisInst.metPairList.metPairs[Met_Pair_Ind];
            int WS_PredInd = 0;
            for (int i = 0; i < thisPair.WS_PredCount; i++)
            {
                if (thisPair.WS_Pred[i, 0].model.isCalibrated == false)
                {
                    WS_PredInd = i;
                    break;
                }
            }

            Assert.AreEqual(thisPair.WS_Pred[WS_PredInd, 0].sectorWS_Ests[0, 0], 8.373, 0.01, "Wrong estimated WS at Test 1");
            Assert.AreEqual(thisPair.WS_Pred[WS_PredInd, 0].sectorWS_Ests[0, 20], 7.232, 0.01, "Wrong estimated WS at Test 2");

            Turbine thisTurb = new Turbine();

            for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
            {
                if (thisInst.turbineList.turbineEsts[i].name == "Turb_464")
                {
                    thisTurb = thisInst.turbineList.turbineEsts[i];
                    break;
                }
            }

            for (int i = 0; i < thisTurb.WSEst_Count; i++)
            {
                if (thisTurb.WS_Estimate[i].predictorMetName.TrimEnd() == "Met_475" && thisTurb.WS_Estimate[i].radius == 4000 && thisTurb.WS_Estimate[i].model.isCalibrated == false)
                {
                    WS_PredInd = i;
                    break;
                }
            }

            Assert.AreEqual(thisTurb.WS_Estimate[WS_PredInd].sectorWS[0], 7.41, 0.01, "Wrong WS estimated at Test 3");
            thisInst.Close();
        }

        [TestMethod]
        public void LLtoUTM_Test()
        {
            UTM_conversion ThisConverter = new UTM_conversion();
            ThisConverter.savedDatumIndex = 0;
            ThisConverter.UTMZoneNumber = 16;
            double ThisLat = 39.852;
            double ThisLong = -84.549;
            UTM_conversion.UTM_coords These_Coords = ThisConverter.LLtoUTM(ThisLat, ThisLong);

            Assert.AreEqual(709679, These_Coords.UTMEasting, 1, "Wrong easting in Test 1");
            Assert.AreEqual(4414205, These_Coords.UTMNorthing, 1, "Wrong northing in Test 1");

            ThisConverter.UTMZoneNumber = 17;
            These_Coords = ThisConverter.LLtoUTM(ThisLat, ThisLong);

            Assert.AreEqual(196370, These_Coords.UTMEasting, 1, "Wrong easting in Test 2");
            Assert.AreEqual(4417361, These_Coords.UTMNorthing, 1, "Wrong northing in Test 2");
        }

        [TestMethod]
        public void UTMtoLL_Test()
        {
            UTM_conversion ThisConverter = new UTM_conversion();
            ThisConverter.savedDatumIndex = 0;
            ThisConverter.UTMZoneNumber = 17;
            ThisConverter.hemisphere = "Northern";
            double UTMX = 477685;
            double UTMY = 4618287;
            UTM_conversion.Lat_Long These_LatLong = ThisConverter.UTMtoLL(UTMX, UTMY);

            Assert.AreEqual(41.716058, These_LatLong.latitude, 1, "Wrong latitude in Test 1");
            Assert.AreEqual(-81.268258, These_LatLong.longitude, 1, "Wrong longitude in Test 1");

            ThisConverter.UTMZoneNumber = 14;
            ThisConverter.hemisphere = "Northern";
            UTMX = 292802;
            UTMY = 4346641.9;
            These_LatLong = ThisConverter.UTMtoLL(UTMX, UTMY);

            Assert.AreEqual(39.244365, These_LatLong.latitude, 1, "Wrong latitude in Test 2");
            Assert.AreEqual(-101.400952, These_LatLong.longitude, 1, "Wrong longitude in Test 2");

        }

        [TestMethod]
        public void Calc_and_Return_Gross_AEP_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0 Great Western TAB.cfm";
            thisInst.Open(Filename);

            string Dist_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\CalcGrossAEP\\WS_Dist.txt";
            StreamReader sr = new StreamReader(Dist_file);
            double[] thisDist = new double[31];

            for (int i = 0; i <= 30; i++)
                thisDist[i] = Convert.ToSingle(sr.ReadLine());

            double This_AEP = thisInst.turbineList.CalcAndReturnGrossAEP(thisDist, "GW 87/1500 1.205 kg/m^3,,");

            Assert.AreEqual(10199.0, This_AEP, 0.1, "Wrong gross AEP");

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Capacity_factor_Test()
        {
            TurbineCollection turbineList = new TurbineCollection();

            double This_AEP = 4569;
            double Rated_P = 1500;
            double This_CF = turbineList.CalcCapacityFactor(This_AEP, Rated_P);
            Assert.AreEqual(0.3477, This_CF, 0.01, "Wrong capaciy factor Test 1");

            This_AEP = 8922;
            Rated_P = 2300;
            This_CF = turbineList.CalcCapacityFactor(This_AEP, Rated_P);
            Assert.AreEqual(0.4428, This_CF, 0.01, "Wrong capaciy factor Test 2");

        }

        [TestMethod]
        public void CalcWakeProfileFit()
        {
            WakeCollection WakeModList = new WakeCollection();

            string Vel_Dif_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Wake loss calcs\\CalcWakeProfileFit\\Vel_Deficit.txt";
            StreamReader sr = new StreamReader(Vel_Dif_file);

            double[] Vel_Def = new double[21];
            for (int i = 0; i < 21; i++)
                Vel_Def[i] = Convert.ToSingle(sr.ReadLine());

            sr.Close();

            string X_Length_RD_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Wake loss calcs\\CalcWakeProfileFit\\X_Length_RD.txt";
            sr = new StreamReader(X_Length_RD_file);
            double[] X_Length_RD = new double[21];
            for (int i = 0; i < 21; i++)
                X_Length_RD[i] = Convert.ToSingle(sr.ReadLine());

            double[] Coeffs = WakeModList.CalcWakeProfileFit(Vel_Def, X_Length_RD);
            sr.Close();

            Assert.AreEqual(0.7859, Coeffs[0], 0.01, "Wrong coeff 1");
            Assert.AreEqual(0.425, Coeffs[1], 0.01, "Wrong coeff 2");
            Assert.AreEqual(-19.956, Coeffs[2], 0.1, "Wrong coeff 3");
            Assert.AreEqual(52.212, Coeffs[3], 0.1, "Wrong coeff 4");
            Assert.AreEqual(-40.497, Coeffs[4], 0.1, "Wrong coeff 5");
        }

        [TestMethod]
        public void Find_All_UW_Turbines_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();
            turbineList.AddTurbine("Turb_1", 275000, 4553000, 1);
            turbineList.AddTurbine("Turb_2", 275000, 4553300, 1);
            turbineList.AddTurbine("Turb_3", 275000, 4553600, 1);
            turbineList.AddTurbine("Turb_4", 275000, 4553900, 1);
            turbineList.AddTurbine("Turb_5", 275000, 4554200, 1);
            turbineList.AddTurbine("Turb_6", 283000, 4553000, 1);
            turbineList.AddTurbine("Turb_7", 283000, 4553300, 1);
            turbineList.AddTurbine("Turb_8", 283000, 4553600, 1);
            turbineList.AddTurbine("Turb_9", 283000, 4553900, 1);
            turbineList.AddTurbine("Turb_10", 283000, 4554200, 1);

            Turbine[] UW_Turbs = WakeModList.FindAllUW_Turbines(45, 279000, 4553200, turbineList);
            Assert.AreEqual(UW_Turbs.Length, 5, "Wrong number of UW turbines in Test 1 Sector 45");
            Assert.AreSame(UW_Turbs[0].name, "Turb_10", "Wrong UW turbines in Test 1 Sector 45");
            Assert.AreSame(UW_Turbs[1].name, "Turb_9", "Wrong UW turbines in Test 1 Sector 45");
            Assert.AreSame(UW_Turbs[2].name, "Turb_8", "Wrong UW turbines in Test 1 Sector 45");
            Assert.AreSame(UW_Turbs[3].name, "Turb_7", "Wrong UW turbines in Test 1 Sector 45");
            Assert.AreSame(UW_Turbs[4].name, "Turb_6", "Wrong UW turbines in Test 1 Sector 45");

            UW_Turbs = WakeModList.FindAllUW_Turbines(180, 279000, 4553200, turbineList);
            Assert.AreEqual(UW_Turbs.Length, 2, "Wrong number of UW turbines in Test 2 Sector 180");
            Assert.AreSame(UW_Turbs[0].name, "Turb_1", "Wrong UW turbines in Test 2 Sector 180");
            Assert.AreSame(UW_Turbs[1].name, "Turb_6", "Wrong UW turbines in Test 2 Sector 180");

            UW_Turbs = WakeModList.FindAllUW_Turbines(337.5f, 279000, 4554900, turbineList);
            Assert.AreEqual(UW_Turbs.Length, 4, "Wrong number of UW turbines in Test 3 Sector 337.5");
            Assert.AreSame(UW_Turbs[0].name, "Turb_5", "Wrong UW turbines in Test 3 Sector 180");
            Assert.AreSame(UW_Turbs[1].name, "Turb_4", "Wrong UW turbines in Test 3 Sector 180");
            Assert.AreSame(UW_Turbs[2].name, "Turb_3", "Wrong UW turbines in Test 3 Sector 180");
            Assert.AreSame(UW_Turbs[3].name, "Turb_2", "Wrong UW turbines in Test 3 Sector 180");

            UW_Turbs = WakeModList.FindAllUW_Turbines(225, 275500, 4553200, turbineList);
            Assert.AreEqual(UW_Turbs.Length, 3, "Wrong number of UW turbines in Test 4 Sector 225");
            Assert.AreSame(UW_Turbs[0].name, "Turb_1", "Wrong UW turbines in Test 3 Sector 180");
            Assert.AreSame(UW_Turbs[1].name, "Turb_2", "Wrong UW turbines in Test 3 Sector 180");
            Assert.AreSame(UW_Turbs[2].name, "Turb_3", "Wrong UW turbines in Test 3 Sector 180");
        }

        [TestMethod]
        public void Calc_WS_Deficit_Eddy_Viscosity_Grid_Test()
        {
            WakeCollection wakeModelList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[23];
            string Power_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[23];
            string Thrust_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87);
            wakeModelList.AddWakeModel(0, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear", 0, 0);
            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();
            metList.metItem[0].WS_FirstInt = 0.5f;
            metList.metItem[0].WS_IntSize = 1f;

            double[,] Vel_Def = wakeModelList.CalcWS_DeficitEddyViscosityGrid(2, 30, 0.1f, 0.025f, 8, wakeModelList.wakeModels[0], metList);
            Assert.AreEqual(Vel_Def[16, 2], 0.450777, 0.01, "Wrong Vel Def Test 1 r = 0.05");
            Assert.AreEqual(Vel_Def[16, 10], 0.196172, 0.01, "Wrong Vel Def Test 1 r = 0.25");
            Assert.AreEqual(Vel_Def[16, 17], 0.03166, 0.01, "Wrong Vel Def Test 1 r = 0.425");

            Assert.AreEqual(Vel_Def[40, 0], 0.255068, 0.01, "Wrong Vel Def Test 2 r = 0.05");
            Assert.AreEqual(Vel_Def[40, 7], 0.194434, 0.01, "Wrong Vel Def Test 2 r = 0.25");
            Assert.AreEqual(Vel_Def[40, 18], 0.02328, 0.01, "Wrong Vel Def Test 2 r = 0.425");

            wakeModelList.AddWakeModel(0, 10, 14, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear", 0, 0);
            Vel_Def = wakeModelList.CalcWS_DeficitEddyViscosityGrid(2, 30, 0.1f, 0.025f, 6, wakeModelList.wakeModels[1], metList);
            Assert.AreEqual(Vel_Def[4, 2], 0.513121, 0.01, "Wrong Vel Def Test 3 r = 0.05");
            Assert.AreEqual(Vel_Def[4, 10], 0.20352, 0.01, "Wrong Vel Def Test 3 r = 0.25");
            Assert.AreEqual(Vel_Def[4, 16], 0.04366, 0.01, "Wrong Vel Def Test 3 r = 0.4");

            Assert.AreEqual(Vel_Def[42, 4], 0.18052, 0.01, "Wrong Vel Def Test 4 r = 0.05");
            Assert.AreEqual(Vel_Def[42, 19], 0.01031, 0.01, "Wrong Vel Def Test 4 r = 0.25");

        }

        [TestMethod]
        public void Calc_Net_Energy_Test()
        {
            WakeCollection wakeModelList = new WakeCollection();

            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[31];
            string Power_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, null, 87);
            wakeModelList.AddWakeModel(0, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear", 0, 0);

            string Dist_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\CalcGrossAEP\\WS_Dist.txt";
            sr = new StreamReader(Dist_file);
            double[] thisDist = new double[31];

            for (int i = 0; i <= 30; i++)
                thisDist[i] = Convert.ToSingle(sr.ReadLine());

            double This_AEP = wakeModelList.CalcNetEnergy(wakeModelList.wakeModels[0], thisDist);
            Assert.AreEqual(This_AEP, 10199, 0.1, "Wrongt Net AEP");

        }

        [TestMethod]
        public void Calc_Equiv_Roughness_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[23];

            string Power_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[23];
            string Thrust_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87);
            WakeModList.AddWakeModel(1, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear", 0, 0);

            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();
            metList.metItem[0].WS_FirstInt = 0.5f;
            metList.metItem[0].WS_IntSize = 1f;
            metList.metItem[0].height = 80;

            double This_Equiv_Rough = WakeModList.CalcEquivRoughness(metList, 8, WakeModList.wakeModels[0]);
            Assert.AreEqual(This_Equiv_Rough, 1.9316, 0.01, "Wrong equivalent roughness for DAWM");


        }

        [TestMethod]
        public void Calc_IBL_H1_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[23];
            string Power_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[23];
            string Thrust_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87);

            Turbine[] UW_Turbs = new Turbine[1];
            UW_Turbs[0] = new Turbine();
            UW_Turbs[0].UTMX = 283000;
            UW_Turbs[0].UTMY = 4553300;

            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();
            metList.metItem[0].WS_FirstInt = 0.5f;
            metList.metItem[0].WS_IntSize = 1f;
            metList.metItem[0].height = 80;

            WakeModList.AddWakeModel(1, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear", 0, 0);
            double This_IBL_H1 = WakeModList.Calc_IBL_H1_New(UW_Turbs[0], 280000, 4553500, WakeModList.wakeModels[0], 90f, 1.9316f, metList, 8);

            Assert.AreEqual(This_IBL_H1, 750.4, 1, "Wrong IBL H1");

        }

        [TestMethod]
        public void Calc_IBL_H2_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[23];
            string Power_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[23];
            string Thrust_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87);

            Turbine[] UW_Turbs = new Turbine[1];
            UW_Turbs[0] = new Turbine();
            UW_Turbs[0].UTMX = 283000;
            UW_Turbs[0].UTMY = 4553300;

            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();
            metList.metItem[0].WS_FirstInt = 0.5f;
            metList.metItem[0].WS_IntSize = 1f;
            metList.metItem[0].height = 80;

            WakeModList.AddWakeModel(1, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear", 0, 0);
            double This_IBL_H2 = WakeModList.Calc_IBL_H2_NEW(UW_Turbs[0], 280000, 4553500, WakeModList.wakeModels[0], 90f, metList, 8);

            Assert.AreEqual(This_IBL_H2, 394.2, 1, "Wrong IBL H2");

        }

        [TestMethod]
        public void Calc_DAWM_Deficit_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[23];
            string Power_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[23];
            string Thrust_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87);

            Turbine[] UW_Turbs = new Turbine[1];
            UW_Turbs[0] = new Turbine();
            UW_Turbs[0].UTMX = 283000;
            UW_Turbs[0].UTMY = 4553300;

            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();
            metList.metItem[0].WS_FirstInt = 0.5f;
            metList.metItem[0].WS_IntSize = 1f;
            metList.metItem[0].height = 80;

            WakeModList.AddWakeModel(1, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear", 0, 0);

            double This_Def = WakeModList.Calc_DAWM_Deficit(UW_Turbs, 280000, 4553500, 90, 8, WakeModList.wakeModels[0], metList);
            Assert.AreEqual(This_Def, 0.04742, 1, "Wrong wind speed deficit in DAWM");

        }

        [TestMethod]
        public void Calc_Net_Sect_Energy_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[31];
            string Power_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[31];
            string Thrust_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87);
            WakeModList.AddWakeModel(0, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear", 0, 0);

            string WR_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\CalcNetSectEnergy\\windRose.txt";
            sr = new StreamReader(WR_file);
            double[] windRose = new double[16];
            for (int i = 0; i <= 15; i++)
                windRose[i] = Convert.ToSingle(sr.ReadLine()) / 100;

            string Sect_WS_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\CalcNetSectEnergy\\sectorWS.txt";
            sr = new StreamReader(Sect_WS_file);
            double[,] sectorWS = new double[16, 31];
            for (int i = 0; i <= 15; i++)
            {
                string This_WS_Array = sr.ReadLine();
                string[] This_Array_Split = This_WS_Array.Split('\t');
                for (int j = 0; j <= 30; j++)
                    sectorWS[i, j] = Convert.ToSingle(This_Array_Split[j]);
            }


            double[] This_Net_Energy = WakeModList.CalcNetSectEnergy(WakeModList.wakeModels[0], sectorWS, windRose);
            Assert.AreEqual(This_Net_Energy[0], 102.5134, 0.01, "Wrong net energy calculated Sector 0");
            Assert.AreEqual(This_Net_Energy[6], 96.69486, 0.01, "Wrong net energy calculated Sector 0");
            Assert.AreEqual(This_Net_Energy[14], 223.3589, 0.01, "Wrong net energy calculated Sector 0");
            Assert.AreEqual(This_Net_Energy[15], 124.5820, 0.01, "Wrong net energy calculated Sector 0");

        }

        [TestMethod]
        public void Calc_Wake_Losses_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[31];
            string Power_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[31];
            string Thrust_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87);
            WakeModList.AddWakeModel(0, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear", 0, 0);

            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();
            metList.metItem[0].WS_FirstInt = 0.5f;
            metList.metItem[0].WS_IntSize = 1f;
            metList.metItem[0].height = 80;

            // Load sectorwise wind speed distribution
            string Sect_WS_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\CalcNetSectEnergy\\sectorWS.txt";
            sr = new StreamReader(Sect_WS_file);
            double[,] sectorWS = new double[16, 31];
            for (int i = 0; i <= 15; i++)
            {
                string This_WS_Array = sr.ReadLine();
                string[] This_Array_Split = This_WS_Array.Split('\t');
                for (int j = 0; j <= 30; j++)
                    sectorWS[i, j] = Convert.ToSingle(This_Array_Split[j]);
            }

            string WR_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\CalcNetSectEnergy\\windRose.txt";
            sr = new StreamReader(WR_file);
            double[] windRose = new double[16];
            for (int i = 0; i <= 15; i++)
                windRose[i] = Convert.ToSingle(sr.ReadLine()) / 100;

            string Sect_AEP_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Wake loss calcs\\CalcWakeLosses\\Sect_AEP.txt";
            sr = new StreamReader(Sect_AEP_file);
            double[] Sect_AEP = new double[16];
            for (int i = 0; i <= 15; i++)
                Sect_AEP[i] = Convert.ToSingle(sr.ReadLine());

            double[] R_RD = new double[21];

            for (int i = 0; i <= 20; i++)
                R_RD[i] = i * 0.025f;

            WakeCollection.WakeLossCoeffs[] These_Coeffs = new WakeCollection.WakeLossCoeffs[140]; // 5 (DW_RDs) x 28 (WS > 3)
            int Ind_Count = 0;

            for (double DW_RD = 5.5f; DW_RD <= 5.9; DW_RD = DW_RD + 0.1f)
            {
                int DW_ind = (int)Math.Round((DW_RD - 2) / 0.1, 0);

                for (int i = 3; i <= 30; i++)
                {
                    double[] Vel_Def_Rad = new double[21];   // Velocity Deficit profile at X_Length_RD
                    double[,] WS_Def_EV_Grid = WakeModList.CalcWS_DeficitEddyViscosityGrid(2f, 6f, 0.1f, 0.025f, i, WakeModList.wakeModels[0], metList);
                    for (int radiusInd = 0; radiusInd <= 19; radiusInd++)
                        Vel_Def_Rad[radiusInd] = WS_Def_EV_Grid[DW_ind, radiusInd]; // index = 37 corresponds to DW dist = 5.7 (i.e. 2 + 0.1 * 37 = 5.7)

                    Vel_Def_Rad[20] = 0;
                    double[] Coeffs = WakeModList.CalcWakeProfileFit(Vel_Def_Rad, R_RD);
                    These_Coeffs[Ind_Count].freeStream = i;
                    These_Coeffs[Ind_Count].X_LengthRD = DW_RD;

                    These_Coeffs[Ind_Count].linRegInt = Coeffs[0];
                    These_Coeffs[Ind_Count].linCoeff4 = Coeffs[1];
                    These_Coeffs[Ind_Count].linCoeff3 = Coeffs[2];
                    These_Coeffs[Ind_Count].linCoeff2 = Coeffs[3];
                    These_Coeffs[Ind_Count].linCoeff1 = Coeffs[4];
                    Ind_Count++;
                }
            }
            turbineList.AddTurbine("Target Site", 280050, 4552500, 1);
            turbineList.AddTurbine("UW Site", 280000, 4553000, 1);
            Continuum thisInst = new Continuum();
            WakeCollection.WakeCalcResults WakeResults = WakeModList.CalcWakeLosses(These_Coeffs, 280050, 4552500, sectorWS, 4130, Sect_AEP, thisInst, WakeModList.wakeModels[0]);

            Assert.AreEqual(WakeResults.sectorNetEnergy[0], 79.1604, 0.1, "Wrong net AEP");
            Assert.AreEqual(WakeResults.sectorWakedWS[0], 4.2851, 0.1, "Wrong waked wind speed");
            Assert.AreEqual(WakeResults.sectorWakeLoss[0], 0.2278, 0.1, "Wrong wake loss");
        }

        [TestMethod]
        public void Get_Overall_Wake_Loss_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Panhandle\\Panhandle test.cfm";
            thisInst.Open(Filename);

            double This_Overall_Wake = thisInst.turbineList.GetOverallWakeLoss(thisInst.wakeModelList.wakeModels[0], 16, false);
            Assert.AreEqual(This_Overall_Wake, 0.07819, 0.01, "Wrong overall wake loss");

            thisInst.Close();
        }

        [TestMethod]
        public void Do_WS_Est_along_nodes_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RDM\\RDM test.cfm";
            thisInst.Open(Filename);
            thisInst.turbineList.AddTurbine("Test", 347212, 5307579, 1);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 4000, 1.0f, 1);
            thisInst.turbineList.turbineEsts[0].windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), 347212, 5307579);

            thisInst.turbineList.turbineEsts[0].gridStats.GetGridArrayAndCalcStats(thisInst.turbineList.turbineEsts[0].UTMX, thisInst.turbineList.turbineEsts[0].UTMY, thisInst);
            Model[] UWDW_Mods = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(), false);
            Met thisMet = thisInst.metList.metItem[0];
            Turbine.WS_Ests New_WS_Est = new Turbine.WS_Ests();
            New_WS_Est.predictorMetName = thisMet.name;
            New_WS_Est.model = UWDW_Mods[0];
            New_WS_Est.radius = 4000;

            NodeCollection nodeList = new NodeCollection();
            thisInst.turbineList.turbineEsts[0].AddWS_Estimate(New_WS_Est);
            Nodes startNode = nodeList.GetMetNode(thisMet);
            Nodes targetNode = nodeList.GetTurbNode(thisInst.turbineList.turbineEsts[0]);
            Nodes[] blankNodes = null;
            Nodes[] pathOfNodes = nodeList.FindPathOfNodes(startNode, targetNode, UWDW_Mods[0], thisInst, ref blankNodes, ref blankNodes);
            int WS_Est_ind = thisInst.turbineList.turbineEsts[0].WSEst_Count - 1;
            thisInst.turbineList.turbineEsts[0].DoWS_EstAlongNodes(WS_Est_ind, 0, pathOfNodes, thisInst);

            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].sectorWS_at_nodes[0, 0], 0, 0, "Didn't calculate WS at Node 1");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].sectorWS_at_nodes[1, 0], 0, 0, "Didn't calculate WS at Node 2");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].sectorWS_at_nodes[2, 0], 0, 0, "Didn't calculate WS at Node 3");

            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].sectorWS_at_nodes[0, 8], 0, 0, "Didn't calculate WS at Node 1");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].sectorWS_at_nodes[1, 8], 0, 0, "Didn't calculate WS at Node 2");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].sectorWS_at_nodes[2, 8], 0, 0, "Didn't calculate WS at Node 3");

            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].sectorWS_at_nodes[0, 23], 0, 0, "Didn't calculate WS at Node 1");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].sectorWS_at_nodes[1, 23], 0, 0, "Didn't calculate WS at Node 2");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].sectorWS_at_nodes[2, 23], 0, 0, "Didn't calculate WS at Node 3");

            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].WS_at_nodes[0], 0, 0, "Didn't calculate WS at Node 1");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].WS_at_nodes[1], 0, 0, "Didn't calculate WS at Node 1");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].WS_at_nodes[2], 0, 0, "Didn't calculate WS at Node 1");

            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].sectorWS[0], 0, 0, "Didn't calculate WS at Target site");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].sectorWS[23], 0, 0, "Didn't calculate WS at Target site");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[WS_Est_ind].WS, 0, 0, "Didn't calculate WS at Target site");


            thisInst.Close();
        }

        [TestMethod]
        public void Generate_Avg_WS_at_Turbs_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RDM\\RDM test.cfm";
            thisInst.Open(Filename);
            thisInst.turbineList.AddTurbine("Test", 347212, 5307579, 1);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 4000, 1.0f, 1);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 6000, 1.0f, 1);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 8000, 1.0f, 1);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 10000, 1.0f, 1);
            thisInst.turbineList.turbineEsts[0].windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), 347212, 5307579);
            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), 4000, 10000, true);
            thisInst.turbineList.turbineEsts[0].DoTurbineCalcs(null, thisInst, models);
            thisInst.turbineList.turbineEsts[0].GenerateAvgWS(thisInst, models);

            Assert.AreEqual(thisInst.turbineList.turbineEsts[0].avgWS_Est[0].WS, 7.950309, 0.001, "Wrong average wind speed");

            thisInst.Close();

        }

        [TestMethod]
        public void Calc_WS_Dist_for_turb_or_map_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RDM\\RDM test.cfm";
            thisInst.Open(Filename);

            double[] thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 7.5f, 24);

            Assert.AreEqual(thisDist[0], 0.024216, 0.001, "Wrong WS distribution in Test 1");
            Assert.AreEqual(thisDist[9], 0.093039, 0.001, "Wrong WS distribution in Test 1");

            thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 7.0f, 24);

            Assert.AreEqual(thisDist[0], 0.029410, 0.001, "Wrong WS distribution in Test 2");
            Assert.AreEqual(thisDist[6], 0.125111, 0.001, "Wrong WS distribution in Test 2");
            Assert.AreEqual(thisDist[10], 0.069316, 0.001, "Wrong WS distribution in Test 2");

            thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 9.4f, 24);

            Assert.AreEqual(thisDist[3], 0.036817, 0.001, "Wrong WS distribution in Test 3");
            Assert.AreEqual(thisDist[8], 0.068238, 0.001, "Wrong WS distribution in Test 3");
            Assert.AreEqual(thisDist[12], 0.071463, 0.001, "Wrong WS distribution in Test 3");

            thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 5f, 3);

            Assert.AreEqual(thisDist[2], 0.09668, 0.001, "Wrong WS distribution in Test 4");
            Assert.AreEqual(thisDist[4], 0.158107, 0.001, "Wrong WS distribution in Test 4");
            Assert.AreEqual(thisDist[14], 0.0065201, 0.001, "Wrong WS distribution in Test 4");

            thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 4.2f, 3);

            Assert.AreEqual(thisDist[1], 0.0682394, 0.001, "Wrong WS distribution in Test 5");
            Assert.AreEqual(thisDist[6], 0.1050160, 0.001, "Wrong WS distribution in Test 5");
            Assert.AreEqual(thisDist[12], 0.0036309, 0.001, "Wrong WS distribution in Test 5");

            thisDist = thisInst.metList.CalcWS_DistForTurbOrMap(thisInst.metList.GetMetsUsed(), 5.3f, 3);

            Assert.AreEqual(thisDist[0], 0.0430541, 0.001, "Wrong WS distribution in Test 6");
            Assert.AreEqual(thisDist[4], 0.135551, 0.001, "Wrong WS distribution in Test 6");
            Assert.AreEqual(thisDist[10], 0.0240220, 0.001, "Wrong WS distribution in Test 6");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcTurbineExposures_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RDM\\RDM test.cfm";
            thisInst.Open(Filename);

            thisInst.turbineList.AddTurbine("Test1", 347212, 5307579, 1);
            thisInst.turbineList.AddTurbine("Test2", 346306, 5311256, 1);
            thisInst.turbineList.AddTurbine("Test3", 342100, 5314708, 1);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 4000, 1.0f, 1);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 6000, 1.0f, 1);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 8000, 1.0f, 1);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 10000, 1.0f, 1);

            for (int i = 0; i <= 3; i++)
            {
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].expo, null, "Didn't calc exposure object.");
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].expo[0].expo, null, "Didn't calc exposures.");
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].expo[0].SR, null, "Didn't calc exposures.");
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].expo[0].dispH, null, "Didn't calc exposures.");
            }

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Gross_AEP_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RDM\\RDM test.cfm";
            thisInst.Open(Filename);

            thisInst.turbineList.AddTurbine("Test1", 347212, 5307579, 1);
            thisInst.turbineList.AddTurbine("Test2", 346306, 5311256, 1);
            thisInst.turbineList.AddTurbine("Test3", 342100, 5314708, 1);

            double[] power = new double[31];
            string Power_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[31];
            string Thrust_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            thisInst.turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            for (int i = 0; i <= thisInst.radiiList.ThisCount - 1; i++)
            {
                int radius = thisInst.radiiList.investItem[i].radius;
                double exponent = thisInst.radiiList.investItem[i].exponent;

                thisInst.turbineList.CalcTurbineExposures(thisInst, radius, exponent, 1);
            }

            for (int i = 0; i <= 2; i++)
            {
                thisInst.turbineList.turbineEsts[i].windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), thisInst.turbineList.turbineEsts[i].UTMX, thisInst.turbineList.turbineEsts[i].UTMY);
                Model[] UWDW_Mods = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(), false);
                thisInst.turbineList.turbineEsts[i].DoTurbineCalcs(null, thisInst, UWDW_Mods);
                thisInst.turbineList.turbineEsts[i].GenerateAvgWS(thisInst, UWDW_Mods);

                UWDW_Mods = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(), true);
                thisInst.turbineList.turbineEsts[i].DoTurbineCalcs(null, thisInst, UWDW_Mods);
                thisInst.turbineList.turbineEsts[i].GenerateAvgWS(thisInst, UWDW_Mods);
            }

            thisInst.turbineList.CalcGrossAEP(thisInst, true);
            thisInst.turbineList.CalcGrossAEP(thisInst, false);

            for (int i = 0; i <= 3; i++)
            {
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].grossAEP, null, "Didn't calculate gross AEP");
                Assert.AreEqual(thisInst.turbineList.turbineEsts[0].grossAEP.Length, 2, "Didn't calculate gross AEP");
                Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].grossAEP[0].CF, 0, 0, "Didn't calculate gross AEP");
                Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].grossAEP[0].AEP, 0, 0, "Didn't calculate gross AEP");
                Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].grossAEP[0].P90, 0, 0, "Didn't calculate gross AEP");
                Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].grossAEP[0].P99, 0, 0, "Didn't calculate gross AEP");
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].grossAEP[0].powerCurve, 0, "Didn't calculate gross AEP");
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].grossAEP[0].sectorEnergy, null, "Didn't calculate gross AEP");
            }

            thisInst.Close();
        }

        [TestMethod]
        public void Do_Turbine_Calcs_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RDM\\RDM test.cfm";
            thisInst.Open(Filename);

            thisInst.turbineList.AddTurbine("Test1", 347212, 5307579, 1);
            thisInst.turbineList.AddTurbine("Test2", 346306, 5311256, 1);
            thisInst.turbineList.AddTurbine("Test3", 342100, 5314708, 1);

            double[] power = new double[31];
            string Power_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[31];
            string Thrust_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            thisInst.turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            for (int i = 0; i <= thisInst.radiiList.ThisCount - 1; i++)
            {
                int radius = thisInst.radiiList.investItem[i].radius;
                double exponent = thisInst.radiiList.investItem[i].exponent;

                thisInst.turbineList.CalcTurbineExposures(thisInst, radius, exponent, 1);
            }

            for (int i = 0; i <= 2; i++)
            {
                thisInst.turbineList.turbineEsts[i].windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), thisInst.turbineList.turbineEsts[i].UTMX, thisInst.turbineList.turbineEsts[i].UTMY);
                Model[] UWDW_Mods = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(), false);
                thisInst.turbineList.turbineEsts[i].DoTurbineCalcs(null, thisInst, UWDW_Mods);

                UWDW_Mods = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(), true);
                thisInst.turbineList.turbineEsts[i].DoTurbineCalcs(null, thisInst, UWDW_Mods);
            }

            for (int i = 0; i <= 3; i++)
            {
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].WS_Estimate, null, "Didn't calculate wind speed estimates");
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].gridStats, null, "Didn't calculate grid stats");
                Assert.AreEqual(thisInst.turbineList.turbineEsts[0].WSEst_Count, 32, 0, "Didn't calculate 32 wind spped estimates (4 mets, 4 radii, default/calibrated models");
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].WS_Estimate[0].predictorMetName, "", "Didn't save pred met name");
                Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[0].radius, 0, 0, "Didn't save radius");
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].WS_Estimate[0].sectorWS, null, "Didn't save sectorwse wind speeds");
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].WS_Estimate[0].model, null, "Didn't save model");
                Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].WS_Estimate[0].WS, 0, 0, "Didn't save wind speed");
            }

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_AvgWS_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RDM\\RDM test.cfm";
            thisInst.Open(Filename);
            thisInst.metList.metItem[0].CalcAvgWS();
            thisInst.metList.metItem[1].CalcAvgWS();
            thisInst.metList.metItem[2].CalcAvgWS();
            thisInst.metList.metItem[3].CalcAvgWS();

            Assert.AreEqual(thisInst.metList.metItem[0].WS, 7.478587, 0.001, "Wrong WS at Met 10");
            Assert.AreEqual(thisInst.metList.metItem[1].WS, 8.07606, 0.001, "Wrong WS at Met 15");
            Assert.AreEqual(thisInst.metList.metItem[2].WS, 8.28380, 0.001, "Wrong WS at Met 16");
            Assert.AreEqual(thisInst.metList.metItem[3].WS, 7.140587, 0.001, "Wrong WS at Met 17");

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Wake_Losses_Turb_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Wake loss calcs\\Model for Wake calcs.cfm";
            thisInst.Open(Filename);

            thisInst.wakeModelList.AddWakeModel(0, 5, 12, thisInst.turbineList.GetPowerCurve("GW 87/1500 1.205 kg/m^3,,"), 10, 3, 0.05f, "Linear", 0, 0);
            // Find wake loss coeffs
            WakeCollection.WakeLossCoeffs[] Wake_coeffs = null;
            int minDist = 10000000;
            int maxDist = 0;

            for (int i = 0; i <= thisInst.turbineList.TurbineCount - 1; i++)
            {
                int[] Min_Max_Dist = thisInst.turbineList.CalcMinMaxDistanceToTurbines(thisInst.turbineList.turbineEsts[i].UTMX, thisInst.turbineList.turbineEsts[i].UTMY);
                if (Min_Max_Dist[0] < minDist) minDist = Min_Max_Dist[0]; // this is min distance to turbine but when WD is at a different angle (not in line with turbines) the X dist is less than this value so making this always equal to 2*RD
                if (Min_Max_Dist[1] > maxDist) maxDist = Min_Max_Dist[1];
            }

            minDist = (int)(2 * 87);
            if (maxDist == 0) maxDist = 15000; // maxDist will be zero when there is only one turbine. Might be good to make this value constant
            Wake_coeffs = thisInst.wakeModelList.GetWakeLossesCoeffs(minDist, maxDist, thisInst.wakeModelList.wakeModels[0], thisInst.metList);

            thisInst.turbineList.turbineEsts[0].CalcTurbineWakeLosses(thisInst, Wake_coeffs, thisInst.wakeModelList.wakeModels[0], false);

            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].AvgWSEst_Count, 0, "Didn't add average WS est");
            int avgWS_Ind = thisInst.turbineList.turbineEsts[0].AvgWSEst_Count - 1;
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].avgWS_Est[avgWS_Ind].isWaked, false, "Didn't flag as waked");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].avgWS_Est[avgWS_Ind].wakeModel, null, "Didn't save wake model");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].avgWS_Est[avgWS_Ind].sectorWS, null, "Didn't save sectorwise wind speeds");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].avgWS_Est[avgWS_Ind].WS, null, "Didn't save wind speeds");

            int Net_AEP_ind = thisInst.turbineList.turbineEsts[0].NetAEP_Count - 1;
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].wakeLoss, 0, "Didn't calculate wake loss");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].wakeModel, null, "Didn't save wake model");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].CF, null, "Didn't calculate CF");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].AEP, null, "Didn't calculate AEP est");
            //   Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].P90, 0, "Didn't calculate P90");
            //   Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].P99, 0, "Didn't calculate P99"); Should P90 and P99s be calculated yet?
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].sectorEnergy, null, "Didn't calculate sectorwise WS dists");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].sectorWakeLoss, null, "Didn't calculate sectorwise wake loss");

            thisInst.Close();
        }

        [TestMethod]
        public void Do_Met_Cross_Pred_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RDM\\RDM test.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            NodeCollection nodeList = new NodeCollection();
            thisInst.metPairList.ClearAll();
            thisInst.metPairList.CreateMetPairs(thisInst);

            Nodes Met1_Node = new Nodes();
            Nodes Met2_Node = new Nodes();

            // Find path of nodes in between mets.  if ( met pairs already exist then just add new UWDW default model
            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
            {
                for (int j = 0; j < thisInst.radiiList.ThisCount; j++)
                {
                    if (thisInst.metPairList.metPairs[i].WS_Pred[0, j].nodePath == null)
                    {
                        double[] windRose = new double[thisInst.metPairList.metPairs[i].met1.windRose.Length];
                        for (int WD = 0; WD <= windRose.Length - 1; WD++)
                            windRose[WD] = (thisInst.metPairList.metPairs[i].met1.windRose[WD] + thisInst.metPairList.metPairs[i].met2.windRose[WD]) / 2;

                        Met1_Node = nodeList.GetMetNode(thisInst.metPairList.metPairs[i].met1);
                        Met2_Node = nodeList.GetMetNode(thisInst.metPairList.metPairs[i].met2);
                        Nodes[] blankNodes = null;
                        Nodes[] pathOfNodes = nodeList.FindPathOfNodes(Met1_Node, Met2_Node, thisInst.metPairList.metPairs[i].WS_Pred[0, j].model, thisInst, ref blankNodes, ref blankNodes);

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

        [TestMethod]
        public void Calc_Grid_stats_and_Exposures_Nodes_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RDM\\RDM test.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            Nodes newNode = new Nodes();
            newNode.gridStats = new Grid_Info();
            newNode.UTMX = 344454;
            newNode.UTMY = 5312533;
            newNode.CalcGridStatsAndExposures(thisInst);

            Assert.AreNotSame(newNode.expo, null, "Didn't calculate exposures");
            Assert.AreNotSame(newNode.gridStats, null, "Didn't calculate grid stats");
            Assert.AreNotSame(newNode.expo[0].expo, null, "Didn't calculate exposures");
            Assert.AreNotSame(newNode.expo[0].SR, null, "Didn't calculate roughness");
            Assert.AreNotSame(newNode.expo[0].dispH, null, "Didn't calculate displacement height");
            Assert.AreNotSame(newNode.gridStats.stats, null, "Didn't calculate grid stats");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcMetExposures_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RDM\\RDM test.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
            thisInst.metList.ClearAllExposuresAndGridStats();

            for (int i = 0; i < thisInst.metList.ThisCount; i++)
                for (int j = 0; j < thisInst.radiiList.ThisCount; j++)
                    thisInst.metList.CalcMetExposures(i, j, thisInst);

            for (int i = 0; i < thisInst.metList.ThisCount; i++)
            {
                Assert.AreNotSame(thisInst.metList.metItem[i].expo, null, "Didn't calculate exposure");
                Assert.AreNotSame(thisInst.metList.metItem[i].expo[0].expo, null, "Didn't calculate exposure");
                Assert.AreNotSame(thisInst.metList.metItem[i].expo[0].SR, null, "Didn't calculate exposure");
                Assert.AreNotSame(thisInst.metList.metItem[i].expo[0].dispH, null, "Didn't calculate exposure");
            }

            thisInst.Close();

        }

        [TestMethod]
        public void Get_RMS_Sect_Err_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RDM\\RDM test.cfm";
            thisInst.Open(Filename);

            Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), 4000, 10000, false);

            double thisErr = thisInst.metPairList.GetRMS_SectorErr(theseModels[0], 0);
            Assert.AreEqual(thisErr, 0.027926, 0.001, "Wrong RMS error");

            thisErr = thisInst.metPairList.GetRMS_SectorErr(theseModels[0], 8);
            Assert.AreEqual(thisErr, 0.0221153, 0.001, "Wrong RMS error");

            thisErr = thisInst.metPairList.GetRMS_SectorErr(theseModels[0], 21);
            Assert.AreEqual(thisErr, 0.078873, 0.001, "Wrong RMS error");

            thisErr = thisInst.metPairList.GetRMS_SectorErr(theseModels[3], 3);
            Assert.AreEqual(thisErr, 0.0207874, 0.001, "Wrong RMS error");

            thisErr = thisInst.metPairList.GetRMS_SectorErr(theseModels[3], 18);
            Assert.AreEqual(thisErr, 0.0090522, 0.001, "Wrong RMS error");

            thisErr = thisInst.metPairList.GetRMS_SectorErr(theseModels[3], 23);
            Assert.AreEqual(thisErr, 0.0092268, 0.001, "Wrong RMS error");

            thisInst.Close();
        }

        [TestMethod]
        public void Find_Node_in_Sector_High_Spot_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Find_Node_in_High_Spot\\Ashley.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            NodeCollection nodeList = new NodeCollection();
            Nodes startNode = new Nodes();
            startNode.UTMX = 469972;
            startNode.UTMY = 5113693;
            startNode.elev = thisInst.topo.CalcElevs(startNode.UTMX, startNode.UTMY);
            startNode.windRose = thisInst.metList.GetAvgWindRose();
            startNode.gridStats = new Grid_Info();
            Nodes[] blankNodes = null;

            Nodes highNode = nodeList.FindNodeInSectorHighSpot(thisInst, startNode, 56.16f, 86.16f, 2822, 1975, ref blankNodes, ref blankNodes, false);

            Assert.AreEqual(highNode.UTMX, 472379, 1, "Wrong UTMX in Test 1");
            Assert.AreEqual(highNode.UTMY, 5113886, 1, "Wrong UTMY in Test 1");
            Assert.AreEqual(highNode.elev, 655, 1, "Wrong elevation in Test 1");

            highNode = nodeList.FindNodeInSectorHighSpot(thisInst, startNode, 116.5f, 146.16f, 1411, 987, ref blankNodes, ref blankNodes, false);

            Assert.AreEqual(highNode.UTMX, 470879, 1, "Wrong UTMX in Test 2");
            Assert.AreEqual(highNode.UTMY, 5112613, 1, "Wrong UTMY in Test 2");
            Assert.AreEqual(highNode.elev, 648, 1, "Wrong elevation in Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void AddMet_TAB_Test()
        {
            string WR_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\AddMetTAB\\windRose.txt";
            StreamReader sr = new StreamReader(WR_file);
            double[] windRose = new double[16];
            for (int i = 0; i <= 15; i++)
                windRose[i] = Convert.ToSingle(sr.ReadLine()) / 100;

            string Sect_WS_file = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\AddMetTAB\\sectorWS.txt";
            sr = new StreamReader(Sect_WS_file);
            double[,] sectorWS = new double[16, 31];
            for (int i = 0; i <= 15; i++)
            {
                string This_WS_Array = sr.ReadLine();
                string[] This_Array_Split = This_WS_Array.Split('\t');
                for (int j = 0; j <= 30; j++)
                    sectorWS[i, j] = Convert.ToSingle(This_Array_Split[j]);
            }

            Continuum ThisNewInst = new Continuum();
            ThisNewInst.metList.AddMetTAB("Test_Add", 10000, 100000, 80, windRose, sectorWS, 0.5f, 1);

            Assert.AreEqual(ThisNewInst.metList.ThisCount, 1, 0, "Wrong met count");
            Assert.AreNotSame(ThisNewInst.metList.metItem[0].sectorWS_Dist, null, "Didn't read in sectorwise WS distribution");
            Assert.AreNotSame(ThisNewInst.metList.metItem[0].windRose, null, "Didn't read in wind rose");
            Assert.AreNotSame(ThisNewInst.metList.metItem[0].sectorWS_Ratio, null, "Didn't calculate directional WS ratios");
            Assert.AreNotEqual(ThisNewInst.metList.metItem[0].height, 0, 0, "Didn't read in height");

            ThisNewInst.Close();

        }

        [TestMethod]
        public void Do_RR_Calcs()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0 Great Western TAB doubles.cfm";
            thisInst.Open(Filename);
            MetPairCollection.RR_funct_obj[] thisRR_Obj = new MetPairCollection.RR_funct_obj[3];
            string[,] Mets_for_UWDW = new string[3, 3];
            Mets_for_UWDW[0, 0] = "Met_474";
            Mets_for_UWDW[0, 1] = "Met_474";
            Mets_for_UWDW[0, 2] = "Met_475";
            Mets_for_UWDW[1, 0] = "Met_475";
            Mets_for_UWDW[1, 1] = "Met_3350";
            Mets_for_UWDW[1, 2] = "Met_3350";

            Assert.AreEqual(thisInst.metPairList.RoundRobinCount, 0, "Round Robin calcs already done");

            string[] theseMets = new string[2];
            theseMets[0] = "Met_474";
            theseMets[1] = "Met_475";
            thisRR_Obj[0] = thisInst.metPairList.DoRR_Calc(theseMets, thisInst, thisInst.metList.GetMetsUsed());

            theseMets = new string[2];
            theseMets[0] = "Met_474";
            theseMets[1] = "Met_3350";
            thisRR_Obj[1] = thisInst.metPairList.DoRR_Calc(theseMets, thisInst, thisInst.metList.GetMetsUsed());

            theseMets = new string[2];
            theseMets[0] = "Met_475";
            theseMets[1] = "Met_3350";
            thisRR_Obj[2] = thisInst.metPairList.DoRR_Calc(theseMets, thisInst, thisInst.metList.GetMetsUsed());

            thisInst.metPairList.AddRoundRobinEst(thisRR_Obj, thisInst.metList.GetMetsUsed(), 2, Mets_for_UWDW, true, thisInst.metList);
            Assert.AreEqual(thisInst.metPairList.roundRobinEsts[0].RMS_All, 0.008949, 0.00005, "Wrong RMS error");
            Assert.AreEqual(thisInst.metPairList.roundRobinEsts[0].RMS_Err[0], 0.00001, 0.00005, "Wrong error in first model");
            Assert.AreEqual(thisInst.metPairList.roundRobinEsts[0].RMS_Err[1], 0.0035, 0.00005, "Wrong error in first model");
            Assert.AreEqual(thisInst.metPairList.roundRobinEsts[0].RMS_Err[2], 0.0151, 0.00005, "Wrong error in first model");

            thisInst.Close();
        }

        [TestMethod]
        public void Find_Site_Calibrated_Models_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0 Great Western TAB doubles.cfm";
            thisInst.Open(Filename);

            thisInst.modelList.ClearAllExceptDefaultAndImported();
            Assert.AreEqual(thisInst.modelList.ModelCount, 1, 0, "Didn't clear models");

            thisInst.modelList.FindSiteCalibratedModels(thisInst);
            Assert.AreEqual(thisInst.modelList.ModelCount, 2, 0, "Didn't create site-calibrated model");
            Assert.AreEqual(thisInst.modelList.models[1, 0].isCalibrated, true, "Didn't create site-calibrated model");

            thisInst.Close();
        }

        [TestMethod]
        public void Find_Path_of_Nodes_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            NodeCollection nodeList = new NodeCollection();

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Ashley ND\\Ashley Test.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Clear met pairs
            thisInst.metPairList.ClearAll();
            thisInst.metPairList.CreateMetPairs(thisInst);

            Nodes[] newNodes = null;
            double[] windRose = new double[16];

            // Find path of nodes in between mets for R = 4000
            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
            {
                windRose = new double[16];
                for (int WD = 0; WD < 16; WD++)
                    windRose[WD] = (thisInst.metPairList.metPairs[i].met1.windRose[WD] + thisInst.metPairList.metPairs[i].met2.windRose[WD]) / 2;

                Nodes Met1_Node = nodeList.GetMetNode(thisInst.metPairList.metPairs[i].met1);
                Nodes Met2_Node = nodeList.GetMetNode(thisInst.metPairList.metPairs[i].met2);
                Nodes[] blankNodes = null;
                Nodes[] pathOfNodes = nodeList.FindPathOfNodes(Met1_Node, Met2_Node, thisInst.metPairList.metPairs[i].WS_Pred[0, 0].model, thisInst, ref newNodes, ref blankNodes);
                thisInst.metPairList.metPairs[i].WS_Pred[0, 0].nodePath = pathOfNodes;
            }

            Assert.AreEqual(thisInst.metPairList.metPairs[6].WS_Pred[0, 0].nodePath.Length, 1, "Didn't find node between Met 2 and Met 5");

            // Confirm that Met 2 and Met 5 have terrain exposure that is outside acceptable range
            Nodes Met_2_Node = nodeList.GetMetNode(thisInst.metList.metItem[1]);
            Nodes Met_5_Node = nodeList.GetMetNode(thisInst.metList.metItem[4]);
            windRose = thisInst.metList.GetAvgWindRose();

            bool isTerrainSame = nodeList.TerrainSame(Met_2_Node, Met_5_Node, thisInst.modelList.models[0, 0], 1, 5, windRose, 0);
            Assert.AreEqual(isTerrainSame, false, "Didn't find terrain different between Met 2 and Met 5");

            // Confirm that Met 1 and Met 2 don't need a node in between
            Assert.AreEqual(thisInst.metPairList.metPairs[0].WS_Pred[0, 0].nodePath.Length, 0, "Found node between Met 1 and Met 2");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcMapExposures_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            NodeCollection nodeList = new NodeCollection();

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Ashley ND\\Ashley Test LocalDB 2014.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            Map New_Map = new Map();
            Map.mapNode New_Map_Node = new Map.mapNode();
            Met[] metsUsed = thisInst.metList.GetMets(thisInst.metList.GetMetsUsed(), null);
            Model[] Models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), 4000, 10000, true);

            thisInst.mapList.AddMap("New_Map", 470000, 5105000, 250, 30, 30, 0, "None", thisInst, false, null, metsUsed, Models);
            thisInst.BW_worker.Close();
            New_Map_Node.UTMX = 475000;
            New_Map_Node.UTMY = 5110000;
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, thisInst.mapList.mapItem[0], false);

            Assert.AreSame(New_Map_Node.expo, null, "expo are not null");
            New_Map.CalcMapExposures(ref New_Map_Node, 1, thisInst);

            Assert.AreNotSame(New_Map_Node.expo, null, "Didn't calculate exposures");
            thisInst.Close();
        }

        [TestMethod]
        public void GenMap_Update_Limits()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing Outside VS Models\\Great Western TurbCalc Testing 20190201.cfm";
            thisInst.Open(Filename);
            GenMap New_Map = new GenMap(thisInst);
            New_Map.gridReso = 5000;
            New_Map.UpdateLimits();

            Assert.AreEqual(New_Map.minUTMX_Limit, 426087, 0, "Test 1: Wrong Min UTMX Limit");
            Assert.AreEqual(New_Map.minUTMY_Limit, 3995085, 0, "Test 1: Wrong Min UTMY Limit");
            Assert.AreEqual(New_Map.maxUTMX_Limit, 466087, 0, "Test 1: Wrong Max UTMX Limit");
            Assert.AreEqual(New_Map.maxUTMY_Limit, 4010085, 0, "Test 1: Wrong Max UTMY Limit");
            Assert.AreEqual(New_Map.numX, 9, 0, "Test 1: Wrong Num X");
            Assert.AreEqual(New_Map.numY, 4, 0, "Test 1: Wrong Num Y");
            Assert.AreEqual(New_Map.numGrid, 36, 0, "Test 1: Wrong Num Grid");

            New_Map.gridReso = 1000;
            New_Map.UpdateLimits();
            New_Map.GetBiggestArea();

            Assert.AreEqual(New_Map.minUTMX_Limit, 423087, 0, "Test 2: Wrong Min UTMX Limit");
            Assert.AreEqual(New_Map.minUTMY_Limit, 3992085, 0, "Test 2: Wrong Min UTMY Limit");
            Assert.AreEqual(New_Map.maxUTMX_Limit, 469087, 0, "Test 2: Wrong Max UTMX Limit");
            Assert.AreEqual(New_Map.maxUTMY_Limit, 4014085, 0, "Test 2: Wrong Max UTMY Limit");
            Assert.AreEqual(New_Map.numX, 47, 0, "Test 2: Wrong Num X");
            Assert.AreEqual(New_Map.numY, 23, 0, "Test 2: Wrong Num Y");
            Assert.AreEqual(New_Map.numGrid, 1081, 0, "Test 2: Wrong Num Grid");

            thisInst.Close();
        }

        [TestMethod]
        public void Node_ReCalc_Test()
        {
            // Creates new CFM, loads topography
            string dir = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Node_ReCalc";

            string DB_Load_LC_Before_Met = "Test_Load_LC_Before_Met.cfm";
            string DB_Load_LC_After_Met = "Test_Load_LC_After_Met.cfm";

            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connBefore = nodeList.GetDB_ConnectionString(dir + "\\" + DB_Load_LC_Before_Met);
            string connAfter = nodeList.GetDB_ConnectionString(dir + "\\" + DB_Load_LC_After_Met);

            Nodes[] LC_Load_Before_Met = new Nodes[0]; // array of Nodes with SRDH calcs from model where Land Cover was loaded before the met import and map generation
            Nodes[] LC_Load_After_Met = new Nodes[0]; // same as above but from model where Land Cover was loaded after the met import and map generation

            // Grab all Nodes with SRDH calcs from 'LC Load Before' model
            using (var context = new Continuum_EDMContainer(connBefore))
            {
                var node_db = from N in context.Node_table.Include("expo") select N;

                foreach (var N in node_db)
                {
                    int numRadii = N.expo.Count;

                    for (int i = 0; i < numRadii; i++)
                    {
                        if (N.expo.ElementAt(i).SR_Array != null)
                        {
                            int numNodes = LC_Load_Before_Met.Length;
                            Array.Resize(ref LC_Load_Before_Met, numNodes + 1);
                            LC_Load_Before_Met[numNodes] = new Nodes();
                            LC_Load_Before_Met[numNodes].UTMX = N.UTMX;
                            LC_Load_Before_Met[numNodes].UTMY = N.UTMY;
                            LC_Load_Before_Met[numNodes].elev = N.elev;

                            MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).SR_Array);
                            LC_Load_Before_Met[numNodes].AddExposure(N.expo.ElementAt(i).radius, N.expo.ElementAt(i).exponent, 1, 24);
                            LC_Load_Before_Met[numNodes].expo[0].SR = new double[24];
                            LC_Load_Before_Met[numNodes].expo[0].SR = (double[])bin.Deserialize(MS3);

                            MS3 = new MemoryStream(N.expo.ElementAt(i).DH_Array);
                            LC_Load_Before_Met[numNodes].expo[0].dispH = new double[24];
                            LC_Load_Before_Met[numNodes].expo[0].dispH = (double[])bin.Deserialize(MS3);
                        }
                    }
                }
            }

            // Grab all Nodes with SRDH calcs from 'LC Load After' model
            using (var context = new Continuum_EDMContainer(connAfter))
            {
                var node_db = from N in context.Node_table.Include("expo") select N;

                foreach (var N in node_db)
                {
                    int numRadii = N.expo.Count;

                    for (int i = 0; i < numRadii; i++)
                    {
                        if (N.expo.ElementAt(i).SR_Array != null)
                        {
                            int numNodes = LC_Load_After_Met.Length;
                            Array.Resize(ref LC_Load_After_Met, numNodes + 1);
                            LC_Load_After_Met[numNodes] = new Nodes();
                            LC_Load_After_Met[numNodes].UTMX = N.UTMX;
                            LC_Load_After_Met[numNodes].UTMY = N.UTMY;
                            LC_Load_After_Met[numNodes].elev = N.elev;

                            MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).SR_Array);
                            LC_Load_After_Met[numNodes].AddExposure(N.expo.ElementAt(i).radius, N.expo.ElementAt(i).exponent, 1, 24);
                            LC_Load_After_Met[numNodes].expo[0].SR = new double[24];
                            LC_Load_After_Met[numNodes].expo[0].SR = (double[])bin.Deserialize(MS3);

                            MS3 = new MemoryStream(N.expo.ElementAt(i).DH_Array);
                            LC_Load_After_Met[numNodes].expo[0].dispH = new double[24];
                            LC_Load_After_Met[numNodes].expo[0].dispH = (double[])bin.Deserialize(MS3);
                        }
                    }
                }
            }

            // Loop through nodes LC_Load_Before_Met and find same coords in LC_Load_After_Met and compare SR/DH
            for (int i = 0; i < LC_Load_Before_Met.Length; i++)
            {
                for (int j = 0; j < LC_Load_After_Met.Length; j++)
                {
                    if (LC_Load_Before_Met[i].UTMX == LC_Load_After_Met[j].UTMX && LC_Load_Before_Met[i].UTMY == LC_Load_After_Met[j].UTMY && LC_Load_Before_Met[i].expo[0].radius == LC_Load_After_Met[j].expo[0].radius)
                    {
                        for (int k = 0; k < 24; k++)
                        {
                            Assert.AreEqual(LC_Load_Before_Met[i].expo[0].SR[k], LC_Load_After_Met[j].expo[0].SR[k], 0.00001, "Different SR" + LC_Load_Before_Met[i].UTMX.ToString() + "," + LC_Load_Before_Met[i].UTMY.ToString());
                            Assert.AreEqual(LC_Load_Before_Met[i].expo[0].dispH[k], LC_Load_After_Met[j].expo[0].dispH[k], 0.00001, "Different SR" + LC_Load_Before_Met[i].UTMX.ToString() + "," + LC_Load_Before_Met[i].UTMY.ToString());
                        }
                        break;
                    }
                }
            }


        }

        [TestMethod]
        public void LCKeyOK_Test()
        {
            // Tests changing LC key after a met has been imported and a map has been created
            string dir = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Node_ReCalc";

            string DB_Change_LC_Before_Met = "Test_Change_LCKey_Before_Met.cfm";
            string DB_Change_LC_After_Met = "Test_Change_LCKey_After_Met.cfm";

            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connBefore = nodeList.GetDB_ConnectionString(dir + "\\" + DB_Change_LC_Before_Met);
            string connAfter = nodeList.GetDB_ConnectionString(dir + "\\" + DB_Change_LC_After_Met);

            Nodes[] LC_Change_Before_Met = new Nodes[0]; // array of Nodes with SRDH calcs from model where Land Cover key was changed before the met import and map generation
            Nodes[] LC_Change_After_Met = new Nodes[0]; // same as above but from model where Land Cover key was changed after the met import and map generation

            // Grab all Nodes with SRDH calcs from 'LC Key Change Before' model
            using (var context = new Continuum_EDMContainer(connBefore))
            {
                var node_db = from N in context.Node_table.Include("expo") select N;

                foreach (var N in node_db)
                {
                    int numRadii = N.expo.Count;

                    for (int i = 0; i < numRadii; i++)
                    {
                        if (N.expo.ElementAt(i).SR_Array != null)
                        {
                            int numNodes = LC_Change_Before_Met.Length;
                            Array.Resize(ref LC_Change_Before_Met, numNodes + 1);
                            LC_Change_Before_Met[numNodes] = new Nodes();
                            LC_Change_Before_Met[numNodes].UTMX = N.UTMX;
                            LC_Change_Before_Met[numNodes].UTMY = N.UTMY;
                            LC_Change_Before_Met[numNodes].elev = N.elev;

                            MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).SR_Array);
                            LC_Change_Before_Met[numNodes].AddExposure(N.expo.ElementAt(i).radius, N.expo.ElementAt(i).exponent, 1, 24);
                            LC_Change_Before_Met[numNodes].expo[0].SR = new double[24];
                            LC_Change_Before_Met[numNodes].expo[0].SR = (double[])bin.Deserialize(MS3);

                            MS3 = new MemoryStream(N.expo.ElementAt(i).DH_Array);
                            LC_Change_Before_Met[numNodes].expo[0].dispH = new double[24];
                            LC_Change_Before_Met[numNodes].expo[0].dispH = (double[])bin.Deserialize(MS3);
                        }
                    }
                }
            }

            // Grab all Nodes with SRDH calcs from 'LC Key Change After' model
            using (var context = new Continuum_EDMContainer(connAfter))
            {
                var node_db = from N in context.Node_table.Include("expo") select N;

                foreach (var N in node_db)
                {
                    int numRadii = N.expo.Count;

                    for (int i = 0; i < numRadii; i++)
                    {
                        if (N.expo.ElementAt(i).SR_Array != null)
                        {
                            int numNodes = LC_Change_After_Met.Length;
                            Array.Resize(ref LC_Change_After_Met, numNodes + 1);
                            LC_Change_After_Met[numNodes] = new Nodes();
                            LC_Change_After_Met[numNodes].UTMX = N.UTMX;
                            LC_Change_After_Met[numNodes].UTMY = N.UTMY;
                            LC_Change_After_Met[numNodes].elev = N.elev;

                            MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).SR_Array);
                            LC_Change_After_Met[numNodes].AddExposure(N.expo.ElementAt(i).radius, N.expo.ElementAt(i).exponent, 1, 24);
                            LC_Change_After_Met[numNodes].expo[0].SR = new double[24];
                            LC_Change_After_Met[numNodes].expo[0].SR = (double[])bin.Deserialize(MS3);

                            MS3 = new MemoryStream(N.expo.ElementAt(i).DH_Array);
                            LC_Change_After_Met[numNodes].expo[0].dispH = new double[24];
                            LC_Change_After_Met[numNodes].expo[0].dispH = (double[])bin.Deserialize(MS3);
                        }
                    }
                }
            }

            // Loop through nodes LC_Change_Before_Met and find same coords in LC_Change_After_Met and compare SR/DH
            for (int i = 0; i < LC_Change_Before_Met.Length; i++)
            {
                for (int j = 0; j < LC_Change_After_Met.Length; j++)
                {
                    if (LC_Change_Before_Met[i].UTMX == LC_Change_After_Met[j].UTMX && LC_Change_Before_Met[i].UTMY == LC_Change_After_Met[j].UTMY && LC_Change_Before_Met[i].expo[0].radius == LC_Change_After_Met[j].expo[0].radius)
                    {
                        for (int k = 0; k < 24; k++)
                        {
                            Assert.AreEqual(LC_Change_Before_Met[i].expo[0].SR[k], LC_Change_After_Met[j].expo[0].SR[k], 0.00001, "Different SR" + LC_Change_Before_Met[i].UTMX.ToString() + "," + LC_Change_Before_Met[i].UTMY.ToString());
                            Assert.AreEqual(LC_Change_Before_Met[i].expo[0].dispH[k], LC_Change_After_Met[j].expo[0].dispH[k], 0.00001, "Different SR" + LC_Change_Before_Met[i].UTMX.ToString() + "," + LC_Change_Before_Met[i].UTMY.ToString());
                        }
                        break;
                    }
                }
            }


        }

        [TestMethod]
        public void ReCalcTurbine_SRDH_Test()
        {
            // Tests changing LC key after a met has been imported and turbine estimates have been created
            string dir = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Node_ReCalc";

            string LC_Change_Before = "Test_Change_LCKey_Before_Mets_and_Turbs.cfm";
            string LC_Change_After = "Test_Change_LCKey_After_Mets_and_Turbs.cfm";

            Continuum Change_Before = new Continuum();
            Change_Before.Open(dir + "\\" + LC_Change_Before);

            Continuum Change_After = new Continuum();
            Change_After.Open(dir + "\\" + LC_Change_After);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int WD = 0; WD < 24; WD++)
                    {
                        Assert.AreEqual(Change_Before.metList.metItem[i].expo[j].SR[WD], Change_After.metList.metItem[i].expo[j].SR[WD], 0.00001);
                        Assert.AreEqual(Change_Before.metList.metItem[i].expo[j].dispH[WD], Change_After.metList.metItem[i].expo[j].dispH[WD], 0.00001);
                    }
                    
                }
            }

            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int WD = 0; WD < 24; WD++)
                    {
                        Assert.AreEqual(Change_Before.turbineList.turbineEsts[i].expo[j].SR[WD], Change_After.turbineList.turbineEsts[i].expo[j].SR[WD], 0.00001);
                        Assert.AreEqual(Change_Before.turbineList.turbineEsts[i].expo[j].dispH[WD], Change_After.turbineList.turbineEsts[i].expo[j].dispH[WD], 0.00001);
                    }

                }
            }


            Change_Before.Close();
            Change_After.Close();

        }

        [TestMethod]
        public void Get_Total_Losses_Test()
        {
            Loss_factors loss_Factors = new Loss_factors();
            loss_Factors.Set_Defaults();
            double totalLoss = loss_Factors.Get_Total_Losses();

            Assert.AreEqual(0.10526, totalLoss, 0.0001, "Wrong total losses: Test 1");

            loss_Factors.Icing_Loss = 0.03;
            totalLoss = loss_Factors.Get_Total_Losses();
            Assert.AreEqual(0.132102, totalLoss, 0.0001, "Wrong total losses: Test 2");

            loss_Factors.HighLowTemp_Loss = 0.02;
            totalLoss = loss_Factors.Get_Total_Losses();
            Assert.AreEqual(0.14946, totalLoss, 0.0001, "Wrong total losses: Test 3");

            loss_Factors.Wake_Sect_Curtail_Loss = 0.015;
            totalLoss = loss_Factors.Get_Total_Losses();
            Assert.AreEqual(0.162218, totalLoss, 0.0001, "Wrong total losses: Test 4");

            loss_Factors.Environ_Curtail_Loss = 0.04;
            totalLoss = loss_Factors.Get_Total_Losses();
            Assert.AreEqual(0.19573, totalLoss, 0.0001, "Wrong total losses: Test 5");

            loss_Factors.Grid_Curtail_Loss = 0.01;
            totalLoss = loss_Factors.Get_Total_Losses();
            Assert.AreEqual(0.203772, totalLoss, 0.0001, "Wrong total losses: Test 6");

        }

        [TestMethod]
        public void Get_DW_Param_Test()
        {
            Exposure thisExpo = new Exposure();
            thisExpo.expo = new double[16];
            thisExpo.SR = new double[16];
            thisExpo.dispH = new double[16];

            for (int i = 0; i < 16; i++)
            {
                thisExpo.expo[i] = i;
                thisExpo.SR[i] = 0.1 + (double)i / 10;
                thisExpo.dispH[i] = 10 + 10 * i;
            }

            Assert.AreEqual(thisExpo.GetDW_Param(0, "Expo"), 8, 1e-6, "Wrong DW Expo Test 1");
            Assert.AreEqual(thisExpo.GetDW_Param(3, "Expo"), 11, 1e-6, "Wrong DW Expo Test 2");
            Assert.AreEqual(thisExpo.GetDW_Param(12, "Expo"), 4, 1e-6, "Wrong DW Expo Test 3");
            Assert.AreEqual(thisExpo.GetDW_Param(4, "SR"), 1.3, 1e-6, "Wrong DW SR Test 4");
            Assert.AreEqual(thisExpo.GetDW_Param(9, "SR"), 0.2, 1e-6, "Wrong DW SR Test 5");
            Assert.AreEqual(thisExpo.GetDW_Param(15, "SR"), 0.8, 1e-6, "Wrong DW SR Test 6");
            Assert.AreEqual(thisExpo.GetDW_Param(1, "DH"), 100, 1e-6, "Wrong DW DH Test 7");
            Assert.AreEqual(thisExpo.GetDW_Param(8, "DH"), 10, 1e-6, "Wrong DW DH Test 8");
            Assert.AreEqual(thisExpo.GetDW_Param(14, "DH"), 70, 1e-6, "Wrong DW DH Test 9");
        }

        [TestMethod]
        public void Sweep_get_RMS_All_WD_Adj_by_WD_Test()
        {
            // Creates a model with modified values and an Model_Adj with adjustments that should equate to same model as modified values
            // Calls SweepGetRMSWithAdjModel with the Model_Adj and calls Do_Met_Cross_Preds using the model with modified values. 
            // Compares the RMS error found from the two
            
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            NodeCollection nodeList = new NodeCollection();

            string dir = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0";
            string Filename = dir + "\\Sweep_find_min\\Panhandle Sweep Testing.cfm";
            thisInst.Open(Filename);

            // Clear other wind speed cross-predictions
            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
                thisInst.metPairList.metPairs[i].WS_Pred = null;

            int numWD = thisInst.metList.numWD;
            // First create model with modified values and Model_Adj with modifying factors/values
            Model[] Modified_UWDW = new Model[1];
            Modified_UWDW[0] = new Model();
            Modified_UWDW[0].SizeArrays(numWD);
            Modified_UWDW[0].setDefaultModelCoeffs(numWD);
            Modified_UWDW[0].radius = 4000;
            Modified_UWDW[0].metsUsed = thisInst.metList.GetMetsUsed();

            Model[] defaultModel = new Model[1];
            defaultModel[0] = new Model();
            defaultModel[0].SizeArrays(numWD);
            defaultModel[0].metsUsed = thisInst.metList.GetMetsUsed();
            defaultModel[0].radius = 4000;

            // Add WS Pred with Default model
            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
                thisInst.metPairList.metPairs[i].AddWS_Pred(defaultModel);                    

            MetPairCollection.Model_Adj Adj_UWDW = new MetPairCollection.Model_Adj();            
            thisInst.metPairList.SizeAdjModel(ref Adj_UWDW, numWD);

            for (int i = 0; i < numWD; i++)
            {
                Modified_UWDW[0].downhill_A[i] = (0.5 + i / 10.0) * Modified_UWDW[0].downhill_A[i];
                Adj_UWDW.DH_A_Adj[i] = (0.5 + i / 10.0);

                Modified_UWDW[0].downhill_B[i] = (2 - i / 10.0) * Modified_UWDW[0].downhill_B[i];
                Adj_UWDW.DH_B_Adj[i] = (2 - i / 10.0);

                Modified_UWDW[0].uphill_A[i] = (0.5 + i / 10.0) * Modified_UWDW[0].uphill_A[i];
                Adj_UWDW.UH_A_Adj[i] = (0.5 + i / 10.0);

                Modified_UWDW[0].uphill_B[i] = (2 - i / 10.0) * Modified_UWDW[0].uphill_B[i];
                Adj_UWDW.UH_B_Adj[i] = (2 - i / 10.0);

                Modified_UWDW[0].UW_crit[i] = 10 + 2 * i;
                Adj_UWDW.UW_Crit[i] = 10 + 2 * i;

                Modified_UWDW[0].spdUp_A[i] = (0.5 + i / 10.0) * Modified_UWDW[0].spdUp_A[i];
                Adj_UWDW.SU_A_Adj[i] = (0.5 + i / 10.0);

                Modified_UWDW[0].spdUp_B[i] = (2 - i / 10.0) * Modified_UWDW[0].spdUp_B[i];
                Adj_UWDW.SU_B_Adj[i] = (2 - i / 10.0);

                Modified_UWDW[0].DH_Stab_A[i] = (0.5 + i / 10.0);
                Adj_UWDW.DH_Stab_A[i] = (0.5 + i / 10.0);

                Modified_UWDW[0].UH_Stab_A[i] = (3 - i / 10.0);
                Adj_UWDW.UH_Stab_A[i] = (3 - i / 10.0);

                Modified_UWDW[0].SU_Stab_A[i] = (0.2 + i / 5.0);
                Adj_UWDW.SU_Stab_A[i] = (0.2 + i / 5.0);
            }

            double Sweep_get_RMS_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref defaultModel[0], Adj_UWDW, thisInst);

            // Calculate met cross-prediction error using Modified_UWDW and compare to Sweep_get_RMS_Error

            // Clear other wind speed cross-predictions
            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
                thisInst.metPairList.metPairs[i].WS_Pred = null;

            thisInst.modelList.AddModel(Modified_UWDW);
            
            Pair_Of_Mets[] metPairs = thisInst.metPairList.metPairs;
            double RMS_err = 0;
            int RMS_Count = 0;

            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
            {
                metPairs[i].AddWS_Pred(Modified_UWDW);
                int WS_PredInd = metPairs[i].GetWS_PredIndOneModel(Modified_UWDW[0], thisInst.modelList);
                
                metPairs[i].DoMetCrossPred(WS_PredInd, 0, thisInst);                               

                RMS_err = RMS_err + Math.Pow(metPairs[i].WS_Pred[WS_PredInd, 0].percErr[0], 2);
                RMS_Count++;                             

                RMS_err = RMS_err + Math.Pow(metPairs[i].WS_Pred[WS_PredInd, 0].percErr[1], 2);
                RMS_Count++;                                
            }

            RMS_err = Math.Pow((RMS_err / (RMS_Count)), 0.5);

            Assert.AreEqual(Sweep_get_RMS_Error, RMS_err);

            thisInst.Close();
        }

        [TestMethod]
        public void Sweep_a_Param_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            NodeCollection nodeList = new NodeCollection();

            string dir = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0";
            string Filename = dir + "\\Sweep_find_min\\Panhandle Sweep Testing.cfm";
            thisInst.Open(Filename);
            int numWD = thisInst.metList.numWD;

            // For each Iter_type, call SweepGetRMSWithAdjModel and GetRMS_SectorErr for each value interval. Find Minimum Error
            // Call Sweep_a_Param with same Iter_type and value intervals.
            // Compare the adjusted model coefficients

            // Used in Sweep_get_RMS_All_WD
            MetPairCollection.Model_Adj This_Model_Adj_1 = new MetPairCollection.Model_Adj();            
            thisInst.metPairList.SizeAdjModel(ref This_Model_Adj_1, numWD);

            // Used in Sweep_get_RMS_All_WD
            MetPairCollection.Model_Adj This_Model_Adj_2 = new MetPairCollection.Model_Adj();            
            thisInst.metPairList.SizeAdjModel(ref This_Model_Adj_2, numWD);

            MetPairCollection.Init_Params theseInitParams = new MetPairCollection.Init_Params();
            double[] initCoeffs = new double[4]; // 0: Intercept 1: m_uw 2: m_dw 3: Rsq

            MetPairCollection.MinMax_Expos theseMinMax = new MetPairCollection.MinMax_Expos();
            thisInst.metPairList.InitMinMaxExpos(ref theseMinMax, numWD);

            Model[] thisModel = new Model[1];
            thisModel[0] = new Model();
            thisModel[0].radius = 4000;
            thisModel[0].metsUsed = thisInst.metList.GetMetsUsed();
            thisModel[0].SizeArrays(numWD);
            thisModel[0].setDefaultModelCoeffs(numWD);

            for (int i = 0; i < thisInst.metPairList.PairCount; i++)
            {
                thisInst.metPairList.metPairs[i].WS_Pred = null;
                thisInst.metPairList.metPairs[i].AddWS_Pred(thisModel);
            }
                

            Model defaultModel = new Model();
            defaultModel.radius = 4000;
            defaultModel.metsUsed = thisInst.metList.GetMetsUsed();
            defaultModel.SizeArrays(numWD);
            defaultModel.setDefaultModelCoeffs(numWD);

            thisInst.metPairList.Calc_MinMax_Expos(ref theseMinMax, thisInst.metList.GetAvgWindRose(), 0, thisInst.metList, thisModel[0]); // finds min/max UW expo (used to initialize UW crit), min/max sum of UWDW (used in flow separation model), avg P10 expo and min/max WS

            //calculate linear regression to find initial coefficients
            thisInst.metPairList.FindBestInitCoeffs(thisModel[0], ref theseInitParams, ref initCoeffs, thisInst.metList, 0, theseMinMax);
            thisInst.metPairList.InitializeAdjModel(ref This_Model_Adj_1, numWD, theseInitParams, initCoeffs, thisModel[0], theseMinMax);
            thisInst.metPairList.InitializeAdjModel(ref This_Model_Adj_2, numWD, theseInitParams, initCoeffs, thisModel[0], theseMinMax);

            double Mid_Val_1 = 0;
            double Mid_Val_2 = 0;
            int Mid_Int = Convert.ToInt16((((2 - 0.2) / 0.2) + 1)/2);
            bool Error_Changed = false;
            double Last_Error = 0;

            // TEST 1
            // Downhill flow A. Sweep params from 0.2 to 2 with interval = 0.2
            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 10; i++)
                {
                    double Val = 0.2 * i + 0.2;
                    This_Model_Adj_1.DH_A_Adj[WD_Ind] = Val * theseInitParams.DH / defaultModel.downhill_A[WD_Ind]; // B = 0

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.DH_A_Adj[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.DH_A_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                This_Model_Adj_1.DH_A_Adj[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0.2f, 2, 0.2f, WD_Ind, "Downhill A", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.DH_A_Adj[WD_Ind], This_Model_Adj_2.DH_A_Adj[WD_Ind], 0.00001, "Wrong DH A coeff. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 2
            // Uphill flow A. Sweep params from 0.2 to 2 with interval = 0.2
            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 10; i++)
                {
                    double Val = 0.2 * i + 0.2;
                    This_Model_Adj_1.UH_A_Adj[WD_Ind] = Val * Math.Abs(theseInitParams.UH) / defaultModel.uphill_A[WD_Ind]; // B = 0

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.UH_A_Adj[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.UH_A_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                This_Model_Adj_1.UH_A_Adj[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0.2f, 2, 0.2f, WD_Ind, "Uphill A", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.UH_A_Adj[WD_Ind], This_Model_Adj_2.UH_A_Adj[WD_Ind], 0.00001, "Wrong UH A coeff. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 3
            // UW Critical Exposure. Sweep params from 1 to 40 with interval = 4.875
            Mid_Int = Convert.ToInt16((((40 - 1) / 4.875) + 1) / 2);

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 9; i++)
                {
                    double Val = 4.875 * i + 1;
                    This_Model_Adj_1.UW_Crit[WD_Ind] = Val;

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.UW_Crit[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.UW_Crit[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                This_Model_Adj_1.UW_Crit[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 1f, 40, 4.875f, WD_Ind, "UW Critical", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.UW_Crit[WD_Ind], This_Model_Adj_2.UW_Crit[WD_Ind], 0.00001, "Wrong UW Critical. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 4
            // Speed-Up flow A. Sweep params from 0.65 to 1.5 with interval = 0.2
            Mid_Int = Convert.ToInt16((((1.5 - 0.65) / 0.2) + 1) / 2);

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 5; i++)
                {
                    double Val = 0.2 * i + 0.65;
                    This_Model_Adj_1.SU_A_Adj[WD_Ind] = Val * Math.Abs(theseInitParams.SU) / defaultModel.spdUp_A[WD_Ind]; // B = 0;

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.SU_A_Adj[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.SU_A_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                This_Model_Adj_1.SU_A_Adj[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0.65f, 1.5, 0.2f, WD_Ind, "SpdUp A", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.SU_A_Adj[WD_Ind], This_Model_Adj_2.SU_A_Adj[WD_Ind], 0.00001, "Wrong Speed-Up A. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 5
            // Downhill Stability Factor. Sweep from 0 to 3 with interval = 0.5
            Mid_Int = Convert.ToInt16((((3 - 0) / 0.5) + 1) / 2);

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 7; i++)
                {
                    double Val = 0.5 * i;
                    This_Model_Adj_1.DH_Stab_A[WD_Ind] = Val;

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.DH_Stab_A[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.DH_Stab_A[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                This_Model_Adj_1.DH_Stab_A[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 3, 0.5f, WD_Ind, "DH Stability", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.DH_Stab_A[WD_Ind], This_Model_Adj_2.DH_Stab_A[WD_Ind], 0.00001, "Wrong Downhill Stability. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 6
            // Uphill Stability Factor. Sweep from 0 to 3 with interval = 0.5
            Mid_Int = Convert.ToInt16((((3 - 0) / 0.5) + 1) / 2);

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 7; i++)
                {
                    double Val = 0.5 * i;
                    This_Model_Adj_1.UH_Stab_A[WD_Ind] = Val;

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.UH_Stab_A[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.UH_Stab_A[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                This_Model_Adj_1.UH_Stab_A[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 3, 0.5f, WD_Ind, "UH Stability", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.UH_Stab_A[WD_Ind], This_Model_Adj_2.UH_Stab_A[WD_Ind], 0.00001, "Wrong Uphill Stability. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 7
            // Speed-Up Stability Factor. Sweep from 0 to 3 with interval = 0.5
            Mid_Int = Convert.ToInt16((((3 - 0) / 0.5) + 1) / 2);

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 7; i++)
                {
                    double Val = 0.5 * i;
                    This_Model_Adj_1.SU_Stab_A[WD_Ind] = Val;

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                        Mid_Val_1 = This_Model_Adj_1.SU_Stab_A[WD_Ind];

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.SU_Stab_A[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                    Val_Min_1 = Mid_Val_1;

                This_Model_Adj_1.SU_Stab_A[WD_Ind] = Val_Min_1;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 3, 0.5f, WD_Ind, "SU Stability", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.SU_Stab_A[WD_Ind], This_Model_Adj_2.SU_Stab_A[WD_Ind], 0.00001, "Wrong Speed-Up Stability. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 8
            // Downhill flow B Factor. Sweep from 0 to 1.4 with interval = 0.2
            Mid_Int = Convert.ToInt16((((1.4 - 0) / 0.2) + 1) / 2);
            int counter = 0;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                double Val_Min_2 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 8; i++)
                {
                    double Val = 0.2 * i;
                    
                    // Calculates the coefficient at average P10 Expo and then alters B to change the slope but keep magnitude at Avg P10 Expo fixed
                    
                    double Avg_Coeff = This_Model_Adj_1.DH_A_Adj[WD_Ind] * defaultModel.downhill_A[WD_Ind] * Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (This_Model_Adj_1.DH_B_Adj[WD_Ind] * defaultModel.downhill_B[WD_Ind]));
                    This_Model_Adj_1.DH_B_Adj[WD_Ind] = Val;
                    This_Model_Adj_1.DH_A_Adj[WD_Ind] = Avg_Coeff / defaultModel.downhill_A[WD_Ind] / (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (defaultModel.downhill_B[WD_Ind] * This_Model_Adj_1.DH_B_Adj[WD_Ind])));
                    if (counter == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.DH_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.DH_B_Adj[WD_Ind];
                    }                                      

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.DH_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.DH_B_Adj[WD_Ind];
                    }                        

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.DH_A_Adj[WD_Ind];
                        Val_Min_2 = This_Model_Adj_1.DH_B_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                {
                    Val_Min_1 = Mid_Val_1;
                    Val_Min_2 = Mid_Val_2;
                }

                This_Model_Adj_1.DH_A_Adj[WD_Ind] = Val_Min_1;
                This_Model_Adj_1.DH_B_Adj[WD_Ind] = Val_Min_2;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 1.4, 0.2f, WD_Ind, "Downhill B", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.DH_A_Adj[WD_Ind], This_Model_Adj_2.DH_A_Adj[WD_Ind], 0.00001, "Wrong Downhill A Test 8. WD_Ind = " + WD_Ind.ToString());
                Assert.AreEqual(This_Model_Adj_1.DH_B_Adj[WD_Ind], This_Model_Adj_2.DH_B_Adj[WD_Ind], 0.00001, "Wrong Downhill B Test 8. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 9
            // Uphill flow B Factor. Sweep from 0 to 1.4 with interval = 0.2
            Mid_Int = Convert.ToInt16((((1.4 - 0) / 0.2) + 1) / 2);
            counter = 0;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                double Val_Min_2 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 8; i++)
                {
                    double Val = 0.2 * i;

                    // Calculates the coefficient at average P10 Expo and then alters B to change the slope but keep magnitude at Avg P10 Expo fixed

                    double Avg_Coeff = This_Model_Adj_1.UH_A_Adj[WD_Ind] * defaultModel.uphill_A[WD_Ind] * Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (This_Model_Adj_1.UH_B_Adj[WD_Ind] * defaultModel.uphill_B[WD_Ind]));
                    This_Model_Adj_1.UH_B_Adj[WD_Ind] = Val;
                    This_Model_Adj_1.UH_A_Adj[WD_Ind] = Avg_Coeff / defaultModel.uphill_A[WD_Ind] / (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (defaultModel.uphill_B[WD_Ind] * This_Model_Adj_1.UH_B_Adj[WD_Ind])));

                    if (counter == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.UH_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.UH_B_Adj[WD_Ind];
                    }

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.UH_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.UH_B_Adj[WD_Ind];
                    }

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.UH_A_Adj[WD_Ind];
                        Val_Min_2 = This_Model_Adj_1.UH_B_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                {
                    Val_Min_1 = Mid_Val_1;
                    Val_Min_2 = Mid_Val_2;
                }

                This_Model_Adj_1.UH_A_Adj[WD_Ind] = Val_Min_1;
                This_Model_Adj_1.UH_B_Adj[WD_Ind] = Val_Min_2;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 1.4, 0.2f, WD_Ind, "Uphill B", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.UH_A_Adj[WD_Ind], This_Model_Adj_2.UH_A_Adj[WD_Ind], 0.00001, "Wrong Uphill A Test 9. WD_Ind = " + WD_Ind.ToString());
                Assert.AreEqual(This_Model_Adj_1.UH_B_Adj[WD_Ind], This_Model_Adj_2.UH_B_Adj[WD_Ind], 0.00001, "Wrong Uphill B Test 9. WD_Ind = " + WD_Ind.ToString());
            }

            // TEST 10
            // Speed-Up flow B Factor. Sweep from 0 to 1.4 with interval = 0.2
            Mid_Int = Convert.ToInt16((((1.4 - 0) / 0.2) + 1) / 2);
            counter = 0;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double Min_Error = 1000;
                double Val_Min_1 = 0;
                double Val_Min_2 = 0;
                Error_Changed = false;
                Last_Error = 0;
                Mid_Val_1 = 0;

                for (int i = 0; i < 8; i++)
                {
                    double Val = 0.2 * i;

                    // Calculates the coefficient at average P10 Expo and then alters B to change the slope but keep magnitude at Avg P10 Expo fixed

                    double Avg_Coeff = This_Model_Adj_1.SU_A_Adj[WD_Ind] * defaultModel.uphill_A[WD_Ind] * Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (This_Model_Adj_1.SU_B_Adj[WD_Ind] * defaultModel.spdUp_B[WD_Ind]));
                    This_Model_Adj_1.SU_B_Adj[WD_Ind] = Val;
                    This_Model_Adj_1.SU_A_Adj[WD_Ind] = Avg_Coeff / defaultModel.uphill_A[WD_Ind] / (Math.Pow(theseMinMax.avgP10ExpoWD[WD_Ind], (defaultModel.spdUp_B[WD_Ind] * This_Model_Adj_1.SU_B_Adj[WD_Ind])));

                    if (counter == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.SU_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.SU_B_Adj[WD_Ind];
                    }

                    double This_Error = thisInst.metPairList.SweepGetRMSWithAdjModel(ref thisModel[0], This_Model_Adj_1, thisInst);
                    double Sect_Error = thisInst.metPairList.GetRMS_SectorErr(thisModel[0], WD_Ind);

                    if (Last_Error != 0 && Sect_Error != Last_Error)
                        Error_Changed = true;

                    if (i == Mid_Int)
                    {
                        Mid_Val_1 = This_Model_Adj_1.SU_A_Adj[WD_Ind];
                        Mid_Val_2 = This_Model_Adj_1.SU_B_Adj[WD_Ind];
                    }

                    if (Sect_Error < Min_Error)
                    {
                        Val_Min_1 = This_Model_Adj_1.SU_A_Adj[WD_Ind];
                        Val_Min_2 = This_Model_Adj_1.SU_B_Adj[WD_Ind];
                        Min_Error = Sect_Error;
                    }

                    Last_Error = Sect_Error;
                }

                if (Error_Changed == false)
                {
                    Val_Min_1 = Mid_Val_1;
                    Val_Min_2 = Mid_Val_2;
                }

                This_Model_Adj_1.SU_A_Adj[WD_Ind] = Val_Min_1;
                This_Model_Adj_1.SU_B_Adj[WD_Ind] = Val_Min_2;
                thisInst.metPairList.Sweep_a_Param(thisInst, 0, 1.4, 0.2f, WD_Ind, "SpdUp B", ref thisModel[0], This_Model_Adj_2, theseMinMax.maxSumUWDW_ExpoWD, theseMinMax.avgP10ExpoWD, theseInitParams);

                Assert.AreEqual(This_Model_Adj_1.SU_A_Adj[WD_Ind], This_Model_Adj_2.SU_A_Adj[WD_Ind], 0.00001, "Wrong Speed-Up A Test 10. WD_Ind = " + WD_Ind.ToString());
                Assert.AreEqual(This_Model_Adj_1.SU_B_Adj[WD_Ind], This_Model_Adj_2.SU_B_Adj[WD_Ind], 0.00001, "Wrong Speed-Up B Test 10. WD_Ind = " + WD_Ind.ToString());
            }

            thisInst.Close();
        }

        [TestMethod]
        public void FindModelBounds_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\GetErrorEst\\Testing Error Estimate.cfm";
            thisInst.Open(Filename);

            // Test 1: WD_ind = 0 Radius = 4000
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].min, 7.05, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].max, 7.05, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].metMinP10.name, "Met_2903", "Wrong Min Met SpeedUp Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 0");

            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].min, 0.47, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].max, 7.05, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].metMinP10.name, "Met_2904", "Wrong Min Met Downhill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 0");

            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].min, 0.47, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].max, 19.99, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].metMaxP10.name, "Met_2907", "Wrong Max Met Uphill Flow WD = 0");

            // Test 2: WD_ind = 2 Radius = 4000
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].min, -999, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].max, -999, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].metMinP10, null, "Wrong Min Met SpeedUp Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].metMaxP10, null, "Wrong Max Met SpeedUp Flow WD = 2");

            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].min, 8.08, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].max, 8.08, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].metMinP10.name, "Met_2903", "Wrong Min Met Downhill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 2");

            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].min, 7.04, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].max, 9.29, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].metMaxP10.name, "Met_2907", "Wrong Max Met Uphill Flow WD = 2");

            // Test 3: WD_ind = 7 Radius = 4000
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].min, 2.51, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 7");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].max, 6.15, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 7");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].metMinP10.name, "Met_2904", "Wrong Min Met SpeedUp Flow WD = 7");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 7");

            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].min, 6.15, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 7");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].max, 12.83, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 7");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].metMinP10.name, "Met_2903", "Wrong Min Met Downhill Flow WD = 7");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].metMaxP10.name, "Met_2907", "Wrong Max Met Downhill Flow WD = 7");

            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].min, 2.51, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 7");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].max, 8.18, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 7");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 7");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].metMaxP10.name, "Met_2905", "Wrong Max Met Uphill Flow WD = 7");

            // Test 4: WD_ind = 13 Radius = 4000
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].min, 10.51, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 13");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].max, 10.51, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 13");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].metMinP10.name, "Met_2903", "Wrong Min Met SpeedUp Flow WD = 13");
            Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 13");

            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].min, 3.67, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 13");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].max, 10.51, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 13");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].metMinP10.name, "Met_2907", "Wrong Min Met Downhill Flow WD = 13");
            Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 13");

            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].min, -999, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 13");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].max, -999, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 13");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].metMinP10, null, "Wrong Min Met Uphill Flow WD = 13");
            Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].metMaxP10, null, "Wrong Max Met Uphill Flow WD = 13");

            // Test 5: WD_ind = 0 Radius = 8000
            Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].min, 9.96, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].max, 9.96, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].metMinP10.name, "Met_2903", "Wrong Min Met SpeedUp Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 0");

            Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].min, 2.23, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].max, 9.96, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].metMinP10.name, "Met_2905", "Wrong Min Met Downhill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 0");

            Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].min, 8.09, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].max, 32.52, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 0");
            Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].metMaxP10.name, "Met_2907", "Wrong Max Met Uphill Flow WD = 0");

            // Test 6: WD_ind = 2 Radius = 8000
            Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].min, 7.15, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].max, 10.61, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].metMinP10.name, "Met_2905", "Wrong Min Met SpeedUp Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].metMaxP10.name, "Met_2907", "Wrong Max Met SpeedUp Flow WD = 2");

            Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].min, 13.56, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].max, 13.56, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].metMinP10.name, "Met_2903", "Wrong Min Met Downhill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 2");

            Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].min, 7.15, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].max, 13.56, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].metMinP10.name, "Met_2905", "Wrong Min Met Uphill Flow WD = 2");
            Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].metMaxP10.name, "Met_2903", "Wrong Max Met Uphill Flow WD = 2");
            
            thisInst.Close();
        }

        [TestMethod]
        public void GetModelWeights_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\GetErrorEst\\Testing Error Estimate.cfm";
            thisInst.Open(Filename);

            Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), 4000, 10000, true);
            double[] theseWeights = thisInst.modelList.GetModelWeights(theseModels);

            Assert.AreEqual(theseWeights[0], 0.25, 0.001, "Wrong Model Weight 4000 m");
            Assert.AreEqual(theseWeights[1], 1.00, 0.001, "Wrong Model Weight 6000 m");
            Assert.AreEqual(theseWeights[2], 0.99255, 0.001, "Wrong Model Weight 8000 m");
            Assert.AreEqual(theseWeights[3], 0.68611, 0.001, "Wrong Model Weight 10000 m");

            thisInst.Close();
        }

        [TestMethod]
        public void GetUncertaintyEstimate_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\GetErrorEst\\Testing Error Estimate.cfm";
            thisInst.Open(Filename);

            Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), 4000, 10000, true);
            double thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 5, 5, 0.5, 10, 0);
            Assert.AreEqual(thisUncert, 0.05171, 0.01, "Wrong uncertainty Test 1");

            thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 5, 5, 3, 10, 0);
            Assert.AreEqual(thisUncert, 0.05171, 0.01, "Wrong uncertainty Test 2");

            thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 12, 12, -5, 10, 0);
            Assert.AreEqual(thisUncert, 0.007371, 0.01, "Wrong uncertainty Test 3");

            thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 0.1, 0.1, 10, 10, 0);
            Assert.AreEqual(thisUncert, 0.056710, 0.01, "Wrong uncertainty Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void GetErrorEst_Test()
        {
            Continuum thisInst = new Continuum();
            thisInst.functionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\GetErrorEst\\Testing Error Estimate.cfm";
            thisInst.Open(Filename);

            Model[] models = new Model[thisInst.radiiList.ThisCount];
            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];

            for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
                models[i] = thisInst.modelList.models[1, i];

            double[] theseWgts = thisInst.modelList.GetModelWeights(models);
            
            int numWD = thisInst.GetNumWD();
            double[,] thisUncert = new double[4, 16]; 

            for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
            {                                
                for (int j = 0; j < numWD; j++)
                {
                    thisUncert[i, j] = models[i].GetUncertaintyEstimate(thisInst, thisTurb.gridStats.stats[i].P10_UW[j], thisTurb.gridStats.stats[i].P10_DW[j],
                        thisTurb.expo[i].expo[j], thisTurb.expo[i].GetDW_Param(j, "Expo"), j);                    
                }                
            }

            double Uncert = thisTurb.GetErrorEst(thisInst);

            Assert.AreEqual(Uncert, 0.042022, 0.01, "Wrong overall uncertainty");                          

            thisInst.Close();
        }
    }
}
