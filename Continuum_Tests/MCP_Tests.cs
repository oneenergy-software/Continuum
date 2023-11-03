using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class MCP_Tests
    {
        string testingFolder = "C:\\Users\\liz_w\\Dropbox\\Continuum 3 Source code\\Critical Unit Test Docs\\MCP";
  //      string MERRA2Folder = "C:\\Users\\liz_w\\Desktop\\MERRA2";

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
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            // Test 1
            thisMCP.numWD = 4;
            thisMCP.numTODs = 1;
            thisMCP.numSeasons = 1;
            thisMCP.WS_BinWidth = 1;

            MCP.CDF_Obj[] ThisCDF_Obj = thisMCP.GenerateMatrixCDFs();

            // check counts of each CDF
            Assert.AreEqual(ThisCDF_Obj[6].count, 266);
            Assert.AreEqual(ThisCDF_Obj[32].count, 155);
            Assert.AreEqual(ThisCDF_Obj[70].count, 147);
            Assert.AreEqual(ThisCDF_Obj[100].count, 208);
            
            // check CDF in three locations
            Assert.AreEqual(ThisCDF_Obj[5].CDF[25], 0.02381, 0.01, "Wrong CDF[25] for WS_ind = 0");
            Assert.AreEqual(ThisCDF_Obj[5].CDF[40], 0.178571, 0.01, "Wrong CDF[40] for WS_ind = 0");
            Assert.AreEqual(ThisCDF_Obj[5].CDF[82], 0.940476, 0.01, "Wrong CDF[82] for WS_ind = 0");                                 

            thisInst.Close();
        }

        [TestMethod]
        public void GetConcAvgsCount_Test()
        {            
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            // Test 1
            thisMCP.ResetMCP("All", thisInst.metList);
            thisMCP.numWD = 16;
            thisMCP.numTODs = 2;
            thisMCP.numSeasons = 4;
            thisMCP.FindConcurrentData(thisMet.metData.startDate, thisMet.metData.endDate);

            double[] Avg_WS_WD = thisMCP.GetConcAvgsCount(16, Met.TOD.Day, Met.Season.Winter);  // 0: Target WS; 1: Reference WS; 2: Data Count'

            Assert.AreEqual(Avg_WS_WD[0], 7.590759, 0.01, "Wrong average target wind speed in Test 1");
            Assert.AreEqual(Avg_WS_WD[1], 8.161251, 0.01, "Wrong average reference wind speed in Test 1");
            Assert.AreEqual(Avg_WS_WD[2], 952, 0, "Wrong data count in Test 1");

            // Test 2
            thisMCP.numWD = 16;
            thisMCP.numTODs = 1;
            thisMCP.numSeasons = 1;
            
            Avg_WS_WD = thisMCP.GetConcAvgsCount(15, Met.TOD.All, Met.Season.All);

            Assert.AreEqual(Avg_WS_WD[2], 524, 0, "Wrong data count in Test 2");
            Assert.AreEqual(Avg_WS_WD[0], 5.716328, 0.01, "Wrong average target wind speed in Test 2");
            Assert.AreEqual(Avg_WS_WD[1], 5.76551, 0.01, "Wrong average reference wind speed in Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void GetConcWS_Array_test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            thisMCP.numWD = 8;
            thisMCP.numTODs = 2;
            thisMCP.numSeasons = 1;

            double[] theseWS = thisMCP.GetConcWS_Array("Target", 2, Met.TOD.Day, Met.Season.All, 4.5f, 5.5f, false);
            Assert.AreEqual(theseWS.Length, 50, 0, "Wrong data count in Test 1");

            theseWS = thisMCP.GetConcWS_Array("Reference", 2, Met.TOD.Day, Met.Season.All, 4.5f, 5.5f, false);
            Assert.AreEqual(theseWS.Length, 50, 0, "Wrong data count in Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void GetMinMaxWD_test()
        {
            MCP thisMCP = new MCP();
            thisMCP.numWD = 4;

            double[] Min_Max_WD = thisMCP.GetMinMaxWD(0);
            Assert.AreEqual(Min_Max_WD[0], 315, 0.01, "Wrong minimum WD in Test 1");
            Assert.AreEqual(Min_Max_WD[1], 45, 0.01, "Wrong minimum WD in Test 2");

            Min_Max_WD = thisMCP.GetMinMaxWD(2);
            Assert.AreEqual(Min_Max_WD[0], 135, 0.01, "Wrong minimum WD in Test 3");
            Assert.AreEqual(Min_Max_WD[1], 225, 0.01, "Wrong minimum WD in Test 4");

            thisMCP.numWD = 8;
            Min_Max_WD = thisMCP.GetMinMaxWD(2);
            Assert.AreEqual(Min_Max_WD[0], 67.5, 0.01, "Wrong minimum WD in Test 5");
            Assert.AreEqual(Min_Max_WD[1], 112.5, 0.01, "Wrong minimum WD in Test 6");

            Min_Max_WD = thisMCP.GetMinMaxWD(7);
            Assert.AreEqual(Min_Max_WD[0], 292.5, 0.01, "Wrong minimum WD in Test 7");
            Assert.AreEqual(Min_Max_WD[1], 337.5, 0.01, "Wrong minimum WD in Test 8");

            thisMCP.numWD = 16;
            Min_Max_WD = thisMCP.GetMinMaxWD(0);
            Assert.AreEqual(Min_Max_WD[0], 348.75, 0.01, "Wrong minimum WD in Test 9");
            Assert.AreEqual(Min_Max_WD[1], 11.25, 0.01, "Wrong minimum WD in Test 10");

            Min_Max_WD = thisMCP.GetMinMaxWD(1);
            Assert.AreEqual(Min_Max_WD[0], 11.25, 0.01, "Wrong minimum WD in Test 11");
            Assert.AreEqual(Min_Max_WD[1], 33.75, 0.01, "Wrong minimum WD in Test 12");

            Min_Max_WD = thisMCP.GetMinMaxWD(8);
            Assert.AreEqual(Min_Max_WD[0], 168.75, 0.01, "Wrong minimum WD in Test 13");
            Assert.AreEqual(Min_Max_WD[1], 191.25, 0.01, "Wrong minimum WD in Test 14");

            Min_Max_WD = thisMCP.GetMinMaxWD(15);
            Assert.AreEqual(Min_Max_WD[0], 326.25, 0.01, "Wrong minimum WD in Test 15");
            Assert.AreEqual(Min_Max_WD[1], 348.75, 0.01, "Wrong minimum WD in Test 16");
                        
        }

        [TestMethod]
        public void GetSubsetConcData_test()
        {
            Continuum thisInst = new Continuum("");
            
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
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            thisMCP.numWD = 4;
            thisMCP.numTODs = 1;
            thisMCP.numSeasons = 1;
            thisMCP.WS_BinWidth = 1;

            MCP.CDF_Obj[] thisCDF_Obj = thisMCP.GenerateMatrixCDFs();

            float Rando = 0.2f;
            int CDF_ind = thisMCP.FindCDF_Index(thisCDF_Obj[5], Rando);
            Assert.AreEqual(CDF_ind, 42, 0, "Wrong CDF index");

            Rando = 0.4f;
            CDF_ind = thisMCP.FindCDF_Index(thisCDF_Obj[5], Rando);
            Assert.AreEqual(CDF_ind, 51, 0, "Wrong CDF index");

            Rando = 0.6f;
            CDF_ind = thisMCP.FindCDF_Index(thisCDF_Obj[5], Rando);
            Assert.AreEqual(CDF_ind, 58, 0, "Wrong CDF index");

            Rando = 0.8f;
            CDF_ind = thisMCP.FindCDF_Index(thisCDF_Obj[5], Rando);
            Assert.AreEqual(CDF_ind, 69, 0, "Wrong CDF index");

            thisInst.Close();
        }

        [TestMethod]
        public void FindSD_ChangeInWS_test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            thisMCP.WS_BinWidth = 1;
            thisMCP.FindSD_ChangeInWS();

            Assert.AreEqual(thisMCP.SD_WS_Lag[0], 0.823, 0.01, "Wrong Last WS Standard deviation");
            Assert.AreEqual(thisMCP.SD_WS_Lag[1], 1.138, 0.01, "Wrong Last WS Standard deviation");
            Assert.AreEqual(thisMCP.SD_WS_Lag[2], 0.97996, 0.01, "Wrong Last WS Standard deviation");
            Assert.AreEqual(thisMCP.SD_WS_Lag[3], 1.0479, 0.01, "Wrong Last WS Standard deviation");

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
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;
         //   UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
            //      thisInst.merraList.MERRAfolder = MERRA2Folder;
            //      MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
            Reference thisRef = thisInst.refList.GetReferenceByUTM(thisMet.UTMX, thisMet.UTMY, "MERRA2");
                        
            thisMCP.numWD = 16;
            thisInst.metList.numWD = 16;
            thisMCP.Do_MCP_Uncertainty(thisInst, thisRef, thisMet);

            Assert.AreEqual(thisMCP.uncertOrtho[0].avg, 6.411818, 0.001, "Wrong calculated average of LT estimates in uncertainty analysis");
            Assert.AreEqual(thisMCP.uncertOrtho[0].stDev, 0.19069, 0.001, "Wrong calculated standard deviation of LT estimates in uncertainty analysis");

            thisInst.Close();
        }

        [TestMethod]
        public void Do_MCP_Uncertainty_test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;
         //   UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
            Reference thisRef = thisInst.refList.GetReferenceByUTM(thisMet.UTMX, thisMet.UTMY, "MERRA2");
         //   MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
            thisMCP.numWD = 16;

            thisMCP.Do_MCP_Uncertainty(thisInst, thisRef, thisMet);

            Assert.AreEqual(thisMCP.uncertOrtho.Length, 12, 0, "Wrong number of uncertainty objects");
            Assert.AreEqual(thisMCP.uncertOrtho[0].NWindows, 12, 0, "Wrong number of monthly intervals");
            Assert.AreEqual(thisMCP.uncertOrtho[7].NWindows, 5, 0, "Wrong number ofintervals");
            Assert.AreEqual(thisMCP.uncertOrtho[5].WSize, 6, 0, "Wrong wrong window size");
            Assert.AreEqual(thisMCP.uncertOrtho[0].avg, 6.411818, 0.001, "Wrong average LT Estimate in uncertainty calculation");

            thisInst.Close();
        }

        [TestMethod]
        public void FindConcurrentData_test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            thisMCP.FindConcurrentData(thisMCP.GetStartOrEndDate("Concurrent", "Start"), thisMCP.GetStartOrEndDate("Concurrent", "End"));
            Assert.AreEqual(thisMCP.concData.Length, 8500, 0, "Wrong concurrent data length");
            
            double[] Conc_Avgs = thisMCP.GetConcAvgsCount(thisInst.metList.numWD, Met.TOD.All, Met.Season.All);
            Assert.AreEqual(Conc_Avgs[0], 6.418475, 0.001, "Wrong average target wind speed");
            Assert.AreEqual(Conc_Avgs[1], 6.669817, 0.001, "Wrong average reference wind speed");
            Assert.AreEqual(Conc_Avgs[2], 8500, 0, "Wrong concurrent data count");

            thisInst.Close();
        }

        [TestMethod]
        public void Get_Sector_Counts_test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\MCP testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            thisInst.metList.numWD = 16;
            thisInst.metList.numTOD = 1;
            thisInst.metList.numSeason = 1;
            thisMCP.numWD = 16;
            thisMCP.numTODs = 1;
            thisMCP.numSeasons = 1;
            thisMCP.Find_Sector_Counts(thisInst.metList);

            // Test 1
            Assert.AreEqual(thisMCP.GetSectorCount(0, Met.TOD.All, Met.Season.All, thisInst.metList), 4204, 0, "Wrong sector count in Test 1");

            thisInst.metList.numWD = 16;
            thisInst.metList.numTOD = 2;
            thisInst.metList.numSeason = 1;
            thisMCP.numWD = 16;
            thisMCP.numTODs = 2;
            thisMCP.numSeasons = 1;
            thisMCP.Find_Sector_Counts(thisInst.metList);

            // Test 2
            Assert.AreEqual(thisMCP.GetSectorCount(15, Met.TOD.Day, Met.Season.All, thisInst.metList), 2629, 0, "Wrong sector count in Test 2");

            // Test 3
            Assert.AreEqual(thisMCP.GetSectorCount(1, Met.TOD.Night, Met.Season.All, thisInst.metList), 2081, 0, "Wrong sector count in Test 3");

            thisInst.metList.numWD = 8;
            thisInst.metList.numTOD = 1;
            thisInst.metList.numSeason = 4;
            thisMCP.numWD = 8;
            thisMCP.numTODs = 1;
            thisMCP.numSeasons = 4;
            thisMCP.Find_Sector_Counts(thisInst.metList);

            // Test 4
            Assert.AreEqual(thisMCP.GetSectorCount(6, Met.TOD.All, Met.Season.Winter, thisInst.metList), 6217, 0, "Wrong sector count in Test 4");

            // Test 5
            Assert.AreEqual(thisMCP.GetSectorCount(3, Met.TOD.All, Met.Season.Spring, thisInst.metList), 1997, 0, "Wrong sector count in Test 5");

            // Test 6
            Assert.AreEqual(thisMCP.GetSectorCount(0, Met.TOD.All, Met.Season.Summer, thisInst.metList), 2567, 0, "Wrong sector count in Test 6");

            // Test 7
            Assert.AreEqual(thisMCP.GetSectorCount(7, Met.TOD.All, Met.Season.Fall, thisInst.metList), 2811, 0, "Wrong sector count in Test 7");

            thisInst.metList.numWD = 24;
            thisInst.metList.numTOD = 2;
            thisInst.metList.numSeason = 4;
            thisMCP.numWD = 24;
            thisMCP.numTODs = 2;
            thisMCP.numSeasons = 4;
            thisMCP.Find_Sector_Counts(thisInst.metList);

            // Test 8
            Assert.AreEqual(thisMCP.GetSectorCount(19, Met.TOD.Day, Met.Season.Fall, thisInst.metList), 686, 0, "Wrong sector count in Test 8");

            thisInst.Close();
        }
        
    }
}
