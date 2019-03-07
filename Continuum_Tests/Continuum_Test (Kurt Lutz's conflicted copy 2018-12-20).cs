using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Threading;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class Continuum_Test
    {
        [TestMethod]
        public void TopoInfo_Calc_Elevs_Test()
        {
            Continuum ThisInst = new Continuum();
            ThisInst.FunctionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            ThisInst.Open(Filename);
            ThisInst.TopoData.Get_Elevs_and_SRDH_for_Calcs(ThisInst);

            // Test 1
            int This_X = 264598;
            int This_Y = 4537698;

            float This_Elev = ThisInst.TopoData.CalcElevs(This_X, This_Y);
            Assert.AreEqual(This_Elev, 248.139, 0.25, "Wrong elevation Test 1");

            // Test 2
            This_X = 276898;
            This_Y = 4568630;

            This_Elev = ThisInst.TopoData.CalcElevs(This_X, This_Y);
            Assert.AreEqual(This_Elev, 211.528, 0.25, "Wrong elevation Test 2");

            // Test 3
            This_X = 292298;
            This_Y = 4541030;

            This_Elev = ThisInst.TopoData.CalcElevs(This_X, This_Y);
            Assert.AreEqual(This_Elev, 247.330, 0.25, "Wrong elevation Test 3");
            
            ThisInst.Close();
        }

        [TestMethod]
        public void TopoInfo_CalcExposures_Test()
        {
            Continuum ThisInst = new Continuum();
            ThisInst.FunctionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            ThisInst.Open(Filename);
            ThisInst.TopoData.Get_Elevs_and_SRDH_for_Calcs(ThisInst);

            // Test 1
            int This_X = 277627;
            int This_Y = 4553420;
            float This_Elev = 248.31f;
            int Radius = 4000;
            float[] Dummy_Rose = new float[16];

            Exposure This_Expo = ThisInst.TopoData.CalcExposures(This_X, This_Y, This_Elev, Radius, 1.0f, 1, ThisInst.TopoData, Dummy_Rose);

            Assert.AreEqual(This_Expo.Expo[0], 6.6866, 0.05);
            Assert.AreEqual(This_Expo.Expo[1], 5.4985, 0.05);
            Assert.AreEqual(This_Expo.Expo[2], 3.9700, 0.05);
            Assert.AreEqual(This_Expo.Expo[3], 2.8177, 0.05);
            Assert.AreEqual(This_Expo.Expo[4], 1.3148, 0.05);
            Assert.AreEqual(This_Expo.Expo[5], 1.4562, 0.05);
            Assert.AreEqual(This_Expo.Expo[6], -0.5943, 0.05);
            Assert.AreEqual(This_Expo.Expo[7], 0.0693, 0.05);
            Assert.AreEqual(This_Expo.Expo[8], -0.2009, 0.05);
            Assert.AreEqual(This_Expo.Expo[9], -2.6497, 0.05);
            Assert.AreEqual(This_Expo.Expo[10], -2.7475, 0.05);
            Assert.AreEqual(This_Expo.Expo[11], -1.0163, 0.05);
            Assert.AreEqual(This_Expo.Expo[12], 2.4154, 0.05);
            Assert.AreEqual(This_Expo.Expo[13], 4.3654, 0.05);
            Assert.AreEqual(This_Expo.Expo[14], 5.8747, 0.05);
            Assert.AreEqual(This_Expo.Expo[15], 7.0703, 0.05);

            // Test 2
            This_X = 281243;
            This_Y = 4550892;
            This_Elev = 250.340f;
            Radius = 10000;
            
            This_Expo = ThisInst.TopoData.CalcExposures(This_X, This_Y, This_Elev, Radius, 1.0f, 1, ThisInst.TopoData, Dummy_Rose);

            Assert.AreEqual(This_Expo.Expo[0], 10.921, 0.05);
            Assert.AreEqual(This_Expo.Expo[1], 9.617, 0.05);
            Assert.AreEqual(This_Expo.Expo[2], 6.969, 0.05);
            Assert.AreEqual(This_Expo.Expo[3], 3.4540, 0.05);
            Assert.AreEqual(This_Expo.Expo[4], -1.1413, 0.05);
            Assert.AreEqual(This_Expo.Expo[5], -1.0659, 0.05);
            Assert.AreEqual(This_Expo.Expo[6], 4.3903, 0.05);
            Assert.AreEqual(This_Expo.Expo[7], 4.8372, 0.05);
            Assert.AreEqual(This_Expo.Expo[8], 6.9035, 0.05);
            Assert.AreEqual(This_Expo.Expo[9], 6.8344, 0.05);
            Assert.AreEqual(This_Expo.Expo[10], 7.553, 0.05);
            Assert.AreEqual(This_Expo.Expo[11], 7.537, 0.05);
            Assert.AreEqual(This_Expo.Expo[12], 2.0476, 0.05);
            Assert.AreEqual(This_Expo.Expo[13], 4.1346, 0.05);
            Assert.AreEqual(This_Expo.Expo[14], 8.5620, 0.05);
            Assert.AreEqual(This_Expo.Expo[15], 11.0025, 0.05);

            ThisInst.Close();
        }

        [TestMethod]
        public void TopoInfo_Read_GeoTiff_Topo_Test()
        {
            Continuum ThisInst = new Continuum();
            ThisInst.FunctionalitiesEnabled = true;
            ThisInst.Saved_Params.Saved_Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing GeoTiff load.cfm";
            ThisInst.Save_File();
            ThisInst.UTM_conversions.Saved_Datum_Index = 0;
            ThisInst.UTM_conversions.UTMZone = "17T";
            
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\NED_15051924 smaller\\NED_15051924\\NED_15051924.tif";
            ThisInst.TopoData.Read_GeoTiff_Topo(Filename, ThisInst);
            ThisInst.TopoData.Topo_Num_XY.X.Calcs = ThisInst.TopoData.Topo_Num_XY.X.All;
            ThisInst.TopoData.Topo_Num_XY.Y.Calcs = ThisInst.TopoData.Topo_Num_XY.Y.All;
            ThisInst.TopoData.Elevs_for_Calcs = ThisInst.TopoData.Topo_Elevs;

            // Test 1
            float UTMX = 268339.8f;
            float UTMY = 4564816.2f;
            float Elev = ThisInst.TopoData.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(214.557, Elev, 0.1, "Wrong elevation Test 1");

            // Test 2
            UTMX = 291638.58f;
            UTMY = 4555225.2f;
            Elev = ThisInst.TopoData.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(242.645, Elev, 0.1, "Wrong elevation Test 2");

            // Test 3
            UTMX = 268193.992f;
            UTMY = 4550969.3f;
            Elev = ThisInst.TopoData.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(245.988, Elev, 0.1, "Wrong elevation Test 3");

            // Test 4
            UTMX = 285429.3f;
            UTMY = 4562086f;
            Elev = ThisInst.TopoData.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(225.246, Elev, 0.1, "Wrong elevation Test 3");
            
            ThisInst.Close();
        }

        [TestMethod]
        public void TopoInfo_Read_GeoTiff_LC_Test()
        {
            Continuum ThisInst = new Continuum();
            ThisInst.FunctionalitiesEnabled = true;
            ThisInst.Saved_Params.Saved_Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing LandCover load.cfm";
            ThisInst.Save_File();
            ThisInst.UTM_conversions.Saved_Datum_Index = 0;
            ThisInst.UTM_conversions.UTMZone = "17T";

            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\LC1178895271 smaller\\LC1178895271.tif";
            ThisInst.TopoData.Set_US_NLCD_Key();
            ThisInst.TopoData.Read_GeoTiff_LandCover(Filename, ThisInst);
            ThisInst.TopoData.LC_Num_XY.X.Calcs = ThisInst.TopoData.LC_Num_XY.X.All;
            ThisInst.TopoData.LC_Num_XY.Y.Calcs = ThisInst.TopoData.LC_Num_XY.Y.All;
            
            // Test 1
            float UTMX = 273126f;
            float UTMY = 4546996f;
            int LC_Code = ThisInst.TopoData.Get_LC_Code(UTMX, UTMY);

            Assert.AreEqual(41, LC_Code, 0, "Wrong land cover code Test 1");

            // Test 2
            UTMX = 275955f;
            UTMY = 4544692f;
            LC_Code = ThisInst.TopoData.Get_LC_Code(UTMX, UTMY);

            Assert.AreEqual(23, LC_Code, 0, "Wrong land cover code Test 2");

            // Test 3
            UTMX = 292223.4f;
            UTMY = 4542114.2f;
            LC_Code = ThisInst.TopoData.Get_LC_Code(UTMX, UTMY);

            Assert.AreEqual(95, LC_Code, 0, "Wrong land cover code Test 3");                       

            ThisInst.Close();
        }

        [TestMethod]
        public void TopoInfo_Calc_SR_and_DH_Test()
        {
            Continuum ThisInst = new Continuum();
            ThisInst.FunctionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            ThisInst.Open(Filename);
            ThisInst.TopoData.Get_Elevs_and_SRDH_for_Calcs(ThisInst);

            // Test 1
            int This_X = 277627;
            int This_Y = 4553420;
            float This_Elev = 248.31f;
            int Radius = 10000;
            float[] Dummy_Rose = new float[16];

            Exposure This_Expo = ThisInst.TopoData.CalcExposures(This_X, This_Y, This_Elev, Radius, 1.0f, 1, ThisInst.TopoData, Dummy_Rose);
            ThisInst.TopoData.Calc_SR_and_DH(ref This_Expo, This_X, This_Y, Radius, 1.0f, Dummy_Rose);

            Assert.AreEqual(This_Expo.SR[0], 0.36083, 0.05);
            Assert.AreEqual(This_Expo.SR[1], 0.32344, 0.05);
            Assert.AreEqual(This_Expo.SR[2], 0.3057, 0.05);
            Assert.AreEqual(This_Expo.SR[3], 0.27809, 0.05);
            Assert.AreEqual(This_Expo.SR[4], 0.2954, 0.05); 
            Assert.AreEqual(This_Expo.SR[5], 0.3311, 0.05);
            Assert.AreEqual(This_Expo.SR[6], 0.5134, 0.05);
            Assert.AreEqual(This_Expo.SR[7], 0.5157, 0.05);
            Assert.AreEqual(This_Expo.SR[8], 0.6470, 0.05);
            Assert.AreEqual(This_Expo.SR[9], 0.5047, 0.05);
            Assert.AreEqual(This_Expo.SR[10], 0.3617, 0.05);
            Assert.AreEqual(This_Expo.SR[11], 0.3280, 0.05);
            Assert.AreEqual(This_Expo.SR[12], 0.3603, 0.05);
            Assert.AreEqual(This_Expo.SR[13], 0.2932, 0.05);
            Assert.AreEqual(This_Expo.SR[14], 0.2955, 0.05);
            Assert.AreEqual(This_Expo.SR[15], 0.3386, 0.05);

            Assert.AreEqual(This_Expo.Disp_H[0], 2.88253, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[1], 2.76889, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[2], 2.41572, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[3], 1.90553, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[4], 1.86140, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[5], 2.63378, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[6], 3.75947, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[7], 3.26826, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[8], 3.92466, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[9], 2.96067, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[10], 2.5779, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[11], 2.6053, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[12], 3.3680, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[13], 2.0482, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[14], 2.0700, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[15], 2.5531, 0.05);                       

            ThisInst.Close();
        }

        [TestMethod]
        public void TopoInfo_CalcExposures_with_smaller_R_Test()
        {
            Continuum ThisInst = new Continuum();
            ThisInst.FunctionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            ThisInst.Open(Filename);
            ThisInst.TopoData.Get_Elevs_and_SRDH_for_Calcs(ThisInst);

            // Test 1
            int This_X = 281043;
            int This_Y = 4551532;
            float This_Elev = ThisInst.TopoData.CalcElevs(This_X, This_Y);
            int Min_Radius = 6000;
            int Max_Radius = 8000;
            float[] Dummy_Rose = new float[16];

            Exposure Smaller_Expo = ThisInst.TopoData.CalcExposures(This_X, This_Y, This_Elev, Min_Radius, 1.0f, 1, ThisInst.TopoData, Dummy_Rose);
            Exposure This_Expo = ThisInst.TopoData.CalcExposures_with_smaller_R_BW(This_X, This_Y, This_Elev, Max_Radius, 1.0f, 1, Min_Radius, Smaller_Expo, Dummy_Rose);

            Assert.AreEqual(This_Expo.Expo[0], 7.3685, 5);
            Assert.AreEqual(This_Expo.Expo[1], 6.7927, 0.05);
            Assert.AreEqual(This_Expo.Expo[2], 4.6486, 0.05);
            Assert.AreEqual(This_Expo.Expo[3], 2.3020, 0.05);
            Assert.AreEqual(This_Expo.Expo[4], -1.388, 0.05);
            Assert.AreEqual(This_Expo.Expo[5], -3.8222, 0.05);
            Assert.AreEqual(This_Expo.Expo[6], -0.0171, 0.05);
            Assert.AreEqual(This_Expo.Expo[7], 1.6268, 0.05);
            Assert.AreEqual(This_Expo.Expo[8], 3.7281, 0.05);
            Assert.AreEqual(This_Expo.Expo[9], 3.9263, 0.05);
            Assert.AreEqual(This_Expo.Expo[10], 4.366, 0.05);
            Assert.AreEqual(This_Expo.Expo[11], 3.058, 0.05);
            Assert.AreEqual(This_Expo.Expo[12], -1.034, 0.05);
            Assert.AreEqual(This_Expo.Expo[13], 2.1024, 0.05);
            Assert.AreEqual(This_Expo.Expo[14], 5.4400, 0.05);
            Assert.AreEqual(This_Expo.Expo[15], 7.4735, 0.05);
        }

        [TestMethod]
        public void TopoInfo_Calc_SRDH_with_smaller_R_BW_Test()
        {
            Continuum ThisInst = new Continuum();
            ThisInst.FunctionalitiesEnabled = true;
            string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Testing 3.0.cfm";
            ThisInst.Open(Filename);
            ThisInst.TopoData.Get_Elevs_and_SRDH_for_Calcs(ThisInst);

            // Test 1
            int This_X = 277893;
            int This_Y = 4554412;
            float This_Elev = ThisInst.TopoData.CalcElevs(This_X, This_Y);            
            int Min_Radius = 4000;
            int Max_Radius = 6000;
            float[] Dummy_Rose = new float[16];
            Exposure This_Expo = new Exposure();

            Exposure Smaller_Expo = ThisInst.TopoData.CalcExposures(This_X, This_Y, This_Elev, Min_Radius, 1.0f, 1, ThisInst.TopoData, Dummy_Rose);
            ThisInst.TopoData.Calc_SR_and_DH(ref Smaller_Expo, This_X, This_Y, Min_Radius, 1.0f, Dummy_Rose);
            ThisInst.TopoData.Calc_SRDH_with_smaller_R_BW(ref This_Expo, This_X, This_Y, Max_Radius, 1.0f, 1, Min_Radius, Smaller_Expo, Dummy_Rose);

            Assert.AreEqual(This_Expo.SR[0], 0.34706, 0.05);
            Assert.AreEqual(This_Expo.SR[1], 0.3182, 0.05);
            Assert.AreEqual(This_Expo.SR[2], 0.3357, 0.05);
            Assert.AreEqual(This_Expo.SR[3], 0.3239, 0.05);
            Assert.AreEqual(This_Expo.SR[4], 0.2600, 0.05);
            Assert.AreEqual(This_Expo.SR[5], 0.3065, 0.05);
            Assert.AreEqual(This_Expo.SR[6], 0.3375, 0.05);
            Assert.AreEqual(This_Expo.SR[7], 0.4300, 0.05);
            Assert.AreEqual(This_Expo.SR[8], 0.5101, 0.05);
            Assert.AreEqual(This_Expo.SR[9], 0.4934, 0.05);
            Assert.AreEqual(This_Expo.SR[10], 0.324, 0.05);
            Assert.AreEqual(This_Expo.SR[11], 0.3786, 0.05);
            Assert.AreEqual(This_Expo.SR[12], 0.3258, 0.05);
            Assert.AreEqual(This_Expo.SR[13], 0.3173, 0.05);
            Assert.AreEqual(This_Expo.SR[14], 0.3377, 0.05);
            Assert.AreEqual(This_Expo.SR[15], 0.3912, 0.05);

            Assert.AreEqual(This_Expo.Disp_H[0], 2.911, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[1], 2.679, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[2], 3.056, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[3], 2.783, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[4], 1.493, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[5], 2.542, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[6], 2.512, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[7], 2.989, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[8], 3.304, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[9], 3.619, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[10], 2.248, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[11], 3.496, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[12], 2.542, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[13], 2.626, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[14], 3.083, 0.05);
            Assert.AreEqual(This_Expo.Disp_H[15], 3.448, 0.05);

            ThisInst.Close();
        }

        [TestMethod]
        public void Find_Sectors_for_Grid_Test()
        {
            Continuum ThisInst = new Continuum();
            ThisInst.FunctionalitiesEnabled = true;

            float[] Wind_Rose = new float[16];
            Wind_Rose[0] = 0.04575f;
            Wind_Rose[1] = 0.04290f;
            Wind_Rose[2] = 0.04477f;
            Wind_Rose[3] = 0.05054f;
            Wind_Rose[4] = 0.04887f;
            Wind_Rose[5] = 0.03913f;
            Wind_Rose[6] = 0.03403f;
            Wind_Rose[7] = 0.04028f;
            Wind_Rose[8] = 0.05905f;
            Wind_Rose[9] = 0.08955f;
            Wind_Rose[10] = 0.10486f;
            Wind_Rose[11] = 0.09677f;
            Wind_Rose[12] = 0.090794f;
            Wind_Rose[13] = 0.082136f;
            Wind_Rose[14] = 0.072797f;
            Wind_Rose[15] = 0.057713f;

            Grid_Info Grid_Stats = new Grid_Info();
            bool[] Sectors_to_use = Grid_Stats.Find_Sectors_for_Grid(Wind_Rose);

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

            Wind_Rose = new float[16];
            Wind_Rose[0] = 0.06061f;
            Wind_Rose[1] = 0.05352f;
            Wind_Rose[2] = 0.04293f;
            Wind_Rose[3] = 0.01726f;
            Wind_Rose[4] = 0.01274f;
            Wind_Rose[5] = 0.03618f;
            Wind_Rose[6] = 0.07353f;
            Wind_Rose[7] = 0.13875f;
            Wind_Rose[8] = 0.11183f;
            Wind_Rose[9] = 0.12792f;
            Wind_Rose[10] = 0.09757f;
            Wind_Rose[11] = 0.05613f;
            Wind_Rose[12] = 0.03627f;
            Wind_Rose[13] = 0.02978f;
            Wind_Rose[14] = 0.0407f;
            Wind_Rose[15] = 0.06426f;

            Grid_Stats = new Grid_Info();
            Sectors_to_use = Grid_Stats.Find_Sectors_for_Grid(Wind_Rose);

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


            ThisInst.Close();

        }

        [TestMethod]
        public void Get_Grid_Array_Test()
        {

        }

    }
}
