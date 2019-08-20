using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class MCP_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\MCP";

        [TestMethod]
        public void CalcOrthoSlope_Test()
        {
            double varx = 1.45;
            double vary = 2.20;
            double covar = 1.8;

            double slope_expected = 1.229804;

            MCP thisMCP = new MCP();
            double slope_calc = thisMCP.CalcOrthoSlope(varx, vary, covar);

            Assert.AreEqual(slope_expected, slope_calc, 0.00001, "Orthogonal Slope not calculated correctly");

        }

        [TestMethod]
        public void GenerateMatrixCDFs_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            // Test 1
            thisMCP.numWD = 1;
            thisMCP.numTODs = 1;
            thisMCP.numSeasons = 1;
            thisMCP.WS_BinWidth = 1;

            MCP.CDF_Obj[] ThisCDF_Obj = thisMCP.GenerateMatrixCDFs();

            // check counts of each CDF
            Assert.AreEqual(ThisCDF_Obj[0].count, 9);
            Assert.AreEqual(ThisCDF_Obj[1].count, 214);
            Assert.AreEqual(ThisCDF_Obj[2].count, 464);
            Assert.AreEqual(ThisCDF_Obj[3].count, 652);
            Assert.AreEqual(ThisCDF_Obj[4].count, 786);
            Assert.AreEqual(ThisCDF_Obj[5].count, 1012);

            // check CDF in three locations
            Assert.AreEqual(ThisCDF_Obj[0].CDF[25], 0.333, 0.01, "Wrong CDF[25] for WS_ind = 0");
            Assert.AreEqual(ThisCDF_Obj[0].CDF[50], 0.667, 0.01, "Wrong CDF[50] for WS_ind = 0");
            Assert.AreEqual(ThisCDF_Obj[0].CDF[75], 0.778, 0.01, "Wrong CDF[75] for WS_ind = 0");

            Assert.AreEqual(ThisCDF_Obj[1].CDF[25], 0.6028, 0.01, "Wrong CDF[25] for WS_ind = 1");
            Assert.AreEqual(ThisCDF_Obj[1].CDF[50], 0.9065, 0.01, "Wrong CDF[50] for WS_ind = 1");
            Assert.AreEqual(ThisCDF_Obj[1].CDF[75], 0.9813, 0.01, "Wrong CDF[75] for WS_ind = 1");

            Assert.AreEqual(ThisCDF_Obj[2].CDF[25], 0.3987, 0.01, "Wrong CDF[25] for WS_ind = 2");
            Assert.AreEqual(ThisCDF_Obj[2].CDF[50], 0.8836, 0.01, "Wrong CDF[50] for WS_ind = 2");
            Assert.AreEqual(ThisCDF_Obj[2].CDF[75], 0.9935, 0.01, "Wrong CDF[75] for WS_ind = 2");

            // Test 2
            thisMCP.numWD = 16;
            thisMCP.numTODs = 2;
            thisMCP.numSeasons = 4;
            thisMCP.WS_BinWidth = 1;            
            ThisCDF_Obj = thisMCP.GenerateMatrixCDFs();

            // find CDF with WS ind = 7, Hour ind = 2, Temp ind = 1, WD ind = 1

            foreach (MCP.CDF_Obj CDF in ThisCDF_Obj)
            {
                if (CDF.WS_Ind == 7 && CDF.season == Met.Season.Winter && CDF.TOD == Met.TOD.Day && CDF.WD_Ind == 1)
                {
                    Assert.AreEqual(CDF.count, 7, 0, "Wrong count for Test2");
                    Assert.AreEqual(CDF.CDF[25], 0.2857, 0.01, "Wrong CDF[25] for Test2");
                    Assert.AreEqual(CDF.CDF[50], 0.428571, 0.01, "Wrong CDF[50] for Test2");
                    Assert.AreEqual(CDF.CDF[75], 0.857143, 0.01, "Wrong CDF[75] for Test2");
                }

            }

            thisInst.Close();
        }

        [TestMethod]
        public void GetConcAvgsCount_Test()
        {            
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            // Test 1
            thisMCP.ResetMCP("All", thisInst.metList);
            thisMCP.numWD = 16;
            thisMCP.numTODs = 2;
            thisMCP.numSeasons = 4;
            thisMCP.FindConcurrentData(false, thisMet.metData.startDate, thisMet.metData.endDate);

            double[] Avg_WS_WD = thisMCP.GetConcAvgsCount(16, Met.TOD.Day, Met.Season.Winter);  // 0: Target WS; 1: Reference WS; 2: Data Count'

            Assert.AreEqual(Avg_WS_WD[0], 3.543, 0.01, "Wrong average target wind speed in Test 1");
            Assert.AreEqual(Avg_WS_WD[1], 4.1508, 0.01, "Wrong average reference wind speed in Test 1");
            Assert.AreEqual(Avg_WS_WD[2], 39, 0, "Wrong data count in Test 1");

            // Test 2
            thisMCP.numWD = 16;
            thisMCP.numTODs = 1;
            thisMCP.numSeasons = 1;
            
            Avg_WS_WD = thisMCP.GetConcAvgsCount(15, Met.TOD.All, Met.Season.All);

            Assert.AreEqual(Avg_WS_WD[2], 554, 0, "Wrong data count in Test 2");
            Assert.AreEqual(Avg_WS_WD[0], 5.43388, 0.01, "Wrong average target wind speed in Test 2");
            Assert.AreEqual(Avg_WS_WD[1], 6.2731, 0.01, "Wrong average reference wind speed in Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void GetConcWS_Array_test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            thisMCP.numWD = 8;
            thisMCP.numTODs = 2;
            thisMCP.numSeasons = 1;

            double[] theseWS = thisMCP.GetConcWS_Array("Target", 2, Met.TOD.Day, Met.Season.All, 4.5f, 5.5f, false);
            Assert.AreEqual(theseWS.Length, 77, 0, "Wrong data count in Test 1");

            theseWS = thisMCP.GetConcWS_Array("Reference", 2, Met.TOD.Day, Met.Season.All, 4.5f, 5.5f, false);
            Assert.AreEqual(theseWS.Length, 77, 0, "Wrong data count in Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void Get_Min_Max_WD_test()
        {
            MCP thisMCP = new MCP();
            thisMCP.numWD = 4;

            double[] Min_Max_WD = thisMCP.Get_Min_Max_WD(0);
            Assert.AreEqual(Min_Max_WD[0], 315, 0.01, "Wrong minimum WD in Test 1");
            Assert.AreEqual(Min_Max_WD[1], 45, 0.01, "Wrong minimum WD in Test 2");

            Min_Max_WD = thisMCP.Get_Min_Max_WD(2);
            Assert.AreEqual(Min_Max_WD[0], 135, 0.01, "Wrong minimum WD in Test 3");
            Assert.AreEqual(Min_Max_WD[1], 225, 0.01, "Wrong minimum WD in Test 4");

            thisMCP.numWD = 8;
            Min_Max_WD = thisMCP.Get_Min_Max_WD(2);
            Assert.AreEqual(Min_Max_WD[0], 67.5, 0.01, "Wrong minimum WD in Test 5");
            Assert.AreEqual(Min_Max_WD[1], 112.5, 0.01, "Wrong minimum WD in Test 6");

            Min_Max_WD = thisMCP.Get_Min_Max_WD(7);
            Assert.AreEqual(Min_Max_WD[0], 292.5, 0.01, "Wrong minimum WD in Test 7");
            Assert.AreEqual(Min_Max_WD[1], 337.5, 0.01, "Wrong minimum WD in Test 8");

            thisMCP.numWD = 16;
            Min_Max_WD = thisMCP.Get_Min_Max_WD(0);
            Assert.AreEqual(Min_Max_WD[0], 348.75, 0.01, "Wrong minimum WD in Test 9");
            Assert.AreEqual(Min_Max_WD[1], 11.25, 0.01, "Wrong minimum WD in Test 10");

            Min_Max_WD = thisMCP.Get_Min_Max_WD(1);
            Assert.AreEqual(Min_Max_WD[0], 11.25, 0.01, "Wrong minimum WD in Test 11");
            Assert.AreEqual(Min_Max_WD[1], 33.75, 0.01, "Wrong minimum WD in Test 12");

            Min_Max_WD = thisMCP.Get_Min_Max_WD(8);
            Assert.AreEqual(Min_Max_WD[0], 168.75, 0.01, "Wrong minimum WD in Test 13");
            Assert.AreEqual(Min_Max_WD[1], 191.25, 0.01, "Wrong minimum WD in Test 14");

            Min_Max_WD = thisMCP.Get_Min_Max_WD(15);
            Assert.AreEqual(Min_Max_WD[0], 326.25, 0.01, "Wrong minimum WD in Test 15");
            Assert.AreEqual(Min_Max_WD[1], 348.75, 0.01, "Wrong minimum WD in Test 16");
                        
        }

        [TestMethod]
        public void GetSubsetConcData_test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            thisMCP.numWD = 8;
            thisMCP.numTODs = 2;
            thisMCP.numSeasons = 1;

            DateTime thisStart = new DateTime(2008, 12, 1);
            DateTime thisEnd = new DateTime(2009, 7, 1);

            thisMCP.GetSubsetConcData(thisStart, thisEnd);
            Assert.AreEqual(thisMCP.concData[0].thisDate, thisStart, "Wrong start date");
            int Last_ind = thisMCP.concData.Length - 1;
            Assert.AreEqual(thisMCP.concData[Last_ind].thisDate, thisEnd, "Wrong end date");

            thisStart = new DateTime(2009, 8, 31);
            thisEnd = new DateTime(2009, 9, 2);

            thisMCP.GetSubsetConcData(thisStart, thisEnd);
            Assert.AreEqual(thisMCP.concData[0].thisDate, thisStart, "Wrong start date");
            Last_ind = thisMCP.concData.Length - 1;
            Assert.AreEqual(thisMCP.concData[Last_ind].thisDate, thisEnd, "Wrong end date");

            thisInst.Close();

        }

        [TestMethod]
        public void FindCDF_Index_test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            thisMCP.numWD = 1;
            thisMCP.numTODs = 1;
            thisMCP.numSeasons = 1;
            thisMCP.WS_BinWidth = 1;

            MCP.CDF_Obj[] thisCDF_Obj = thisMCP.GenerateMatrixCDFs();

            float Rando = 0.2f;
            int CDF_ind = thisMCP.FindCDF_Index(thisCDF_Obj[3], Rando);
            Assert.AreEqual(CDF_ind, 21, 0, "Wrong CDF index");

            Rando = 0.4f;
            CDF_ind = thisMCP.FindCDF_Index(thisCDF_Obj[3], Rando);
            Assert.AreEqual(CDF_ind, 28, 0, "Wrong CDF index");

            Rando = 0.6f;
            CDF_ind = thisMCP.FindCDF_Index(thisCDF_Obj[3], Rando);
            Assert.AreEqual(CDF_ind, 35, 0, "Wrong CDF index");

            Rando = 0.8f;
            CDF_ind = thisMCP.FindCDF_Index(thisCDF_Obj[3], Rando);
            Assert.AreEqual(CDF_ind, 44, 0, "Wrong CDF index");

            thisInst.Close();
        }

        [TestMethod]
        public void FindSD_ChangeInWS_test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            thisMCP.WS_BinWidth = 1;
            thisMCP.FindSD_ChangeInWS();

            Assert.AreEqual(thisMCP.SD_WS_Lag[0], 0.656435, 0.001, "Wrong Last WS Standard deviation");
            Assert.AreEqual(thisMCP.SD_WS_Lag[1], 1.1078, 0.001, "Wrong Last WS Standard deviation");
            Assert.AreEqual(thisMCP.SD_WS_Lag[2], 0.980807, 0.001, "Wrong Last WS Standard deviation");

            thisInst.Close();
        }

        [TestMethod]
        public void GetLagWS_CDF_test()
        {
            double thisMinWS = 0.454f;
            double thisLastWS = 6.105f;
            double thisWS_Int = 0.1128f;

            MCP thisMCP = new MCP();
            thisMCP.WS_BinWidth = 1;
            
            thisMCP.FindSD_ChangeInWS();
            double[] This_CDF = thisMCP.GetLagWS_CDF(thisLastWS, thisMinWS, thisWS_Int);

            Assert.AreEqual(This_CDF[50], 0.49457, 0.01, "Wrong Last WS CDF");
            Assert.AreEqual(This_CDF[20], 0.0003113, 0.01, "Wrong Last WS CDF");
            Assert.AreEqual(This_CDF[90], 0.99999, 0.01, "Wrong Last WS CDF");
        }

        [TestMethod]
        public void CalcVarianceRatioSlope_test()
        {
            MCP thisMCP = new MCP();
            
            double This_Var_X = 9.750792f;
            double This_Var_Y = 8.325867f;
            double thisSlope = thisMCP.CalcVarianceRatioSlope(This_Var_X, This_Var_Y);

            Assert.AreEqual(thisSlope, 0.924049, 0.001, "Wrong slope for variance ratio MCP method");
        }

        [TestMethod]
        public void CalcAvgSD_Uncert_test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
            MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
                        
            thisMCP.numWD = 1;
            thisMCP.Do_MCP_Uncertainty(thisInst, merra, thisMet);

            Assert.AreEqual(thisMCP.uncertOrtho[0].avg, 6.30065, 0.001, "Wrong calculated average of LT estimates in uncertainty analysis");
            Assert.AreEqual(thisMCP.uncertOrtho[0].stDev, 0.211046, 0.001, "Wrong calculated standard deviation of LT estimates in uncertainty analysis");

            thisInst.Close();
        }

        [TestMethod]
        public void Do_MCP_Uncertainty_test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
            MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
            thisMCP.numWD = 1;

            thisMCP.Do_MCP_Uncertainty(thisInst, merra, thisMet);

            Assert.AreEqual(thisMCP.uncertOrtho.Length, 12, 0, "Wrong number of uncertainty objects");
            Assert.AreEqual(thisMCP.uncertOrtho[0].NWindows, 12, 0, "Wrong number of monthly intervals");
            Assert.AreEqual(thisMCP.uncertOrtho[7].NWindows, 5, 0, "Wrong number ofintervals");
            Assert.AreEqual(thisMCP.uncertOrtho[5].WSize, 6, 0, "Wrong wrong window size");
            Assert.AreEqual(thisMCP.uncertOrtho[2].avg, 6.3326, 0.001, "Wrong average LT Estimate in uncertainty calculation");

            thisInst.Close();
        }

        [TestMethod]
        public void FindConcurrentData_test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            thisMCP.FindConcurrentData(true, thisMCP.concStart, thisMCP.concEnd);
            Assert.AreEqual(thisMCP.concData.Length, 8627, 0, "Wrong concurrent data length");
            
            double[] Conc_Avgs = thisMCP.GetConcAvgsCount(0, Met.TOD.All, Met.Season.All);
            Assert.AreEqual(Conc_Avgs[0], 6.266685, 0.001, "Wrong average target wind speed");
            Assert.AreEqual(Conc_Avgs[1], 6.68748, 0.001, "Wrong average reference wind speed");
            Assert.AreEqual(Conc_Avgs[2], 8627, 0, "Wrong concurrent data count");

            thisInst.Close();
        }

        [TestMethod]
        public void Get_Sector_Counts_test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            thisMCP.numWD = 16;
            thisMCP.numTODs = 1;
            thisMCP.numSeasons = 1;
            thisMCP.Find_Sector_Counts();

            Assert.AreEqual(thisMCP.Get_Sector_Count(0, 0, 0), 12529, 0, "Wrong sector count in WD = 0");
            Assert.AreEqual(thisMCP.Get_Sector_Count(1, 0, 0), 11360, 0, "Wrong sector count in WD = 1");
            Assert.AreEqual(thisMCP.Get_Sector_Count(2, 0, 0), 12149, 0, "Wrong sector count in WD = 2");
            Assert.AreEqual(thisMCP.Get_Sector_Count(3, 0, 0), 13874, 0, "Wrong sector count in WD = 3");
            Assert.AreEqual(thisMCP.Get_Sector_Count(4, 0, 0), 13380, 0, "Wrong sector count in WD = 4");
            Assert.AreEqual(thisMCP.Get_Sector_Count(5, 0, 0), 10538, 0, "Wrong sector count in WD = 5");
            Assert.AreEqual(thisMCP.Get_Sector_Count(6, 0, 0), 9180, 0, "Wrong sector count in WD = 6");
            Assert.AreEqual(thisMCP.Get_Sector_Count(7, 0, 0), 10799, 0, "Wrong sector count in WD = 7");
            Assert.AreEqual(thisMCP.Get_Sector_Count(8, 0, 0), 15793, 0, "Wrong sector count in WD = 8");
            Assert.AreEqual(thisMCP.Get_Sector_Count(9, 0, 0), 24231, 0, "Wrong sector count in WD = 9");
            Assert.AreEqual(thisMCP.Get_Sector_Count(10, 0, 0), 29027, 0, "Wrong sector count in WD = 10");
            Assert.AreEqual(thisMCP.Get_Sector_Count(11, 0, 0), 26659, 0, "Wrong sector count in WD = 11");
            Assert.AreEqual(thisMCP.Get_Sector_Count(12, 0, 0), 24669, 0, "Wrong sector count in WD = 12");
            Assert.AreEqual(thisMCP.Get_Sector_Count(13, 0, 0), 22260, 0, "Wrong sector count in WD = 13");
            Assert.AreEqual(thisMCP.Get_Sector_Count(14, 0, 0), 19661, 0, "Wrong sector count in WD = 14");
            Assert.AreEqual(thisMCP.Get_Sector_Count(15, 0, 0), 15538, 0, "Wrong sector count in WD = 15");

            thisInst.Close();
        }
        
    }
}
