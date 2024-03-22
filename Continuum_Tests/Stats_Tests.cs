using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;

namespace Continuum_Tests
{
    [TestClass]
    public class Stats_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;

        public Stats_Tests()
        {
            testingFolder = globals.testingFolder + "Stats";
        }

        [TestMethod]
        public void Calc_Avg_WS_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\Stats testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcpList[0];

            Stats thisStats = new Stats();

            // Test 1
            DateTime Start = Convert.ToDateTime("1/1/2005 12:00:00 AM");
            DateTime End = Convert.ToDateTime("12/31/2010 11:00:00 PM");
            double minDir = 0;
            double maxDir = 360;
            Met.TOD TOD = Met.TOD.All;
            Met.Season season = Met.Season.All;
            
            double This_Avg = thisStats.CalcAvgWS(thisMCP.refData, Start, End, minDir, maxDir, TOD, season, thisInst.metList);
            Assert.AreEqual(This_Avg, 6.61373, 0.001, "Wrong Avg WS Test 1");

            // Test 2
            Start = Convert.ToDateTime("8/5/2007 1:30");
            End = Convert.ToDateTime("11/2/2010 21:00");
            minDir = 78.75;
            maxDir = 101.25;
            TOD = Met.TOD.Day;
            season = Met.Season.Winter;
            thisInst.metList.numTOD = 2;
            thisInst.metList.numSeason = 4;

            This_Avg = thisStats.CalcAvgWS(thisMCP.refData, Start, End, minDir, maxDir, TOD, season, thisInst.metList);
            Assert.AreEqual(This_Avg, 5.853279, 0.001, "Wrong Avg WS Test 2");

            // Test 3
            Start = Convert.ToDateTime("6/24/2008 15:00");
            End = Convert.ToDateTime("9/30/2008 23:50");
            minDir = 0;
            maxDir = 360;
            TOD = Met.TOD.All;
            season = Met.Season.All;
            thisInst.metList.numTOD = 1;
            thisInst.metList.numSeason = 1;

            This_Avg = thisStats.CalcAvgWS(thisMCP.targetData, Start, End, minDir, maxDir, TOD, season, thisInst.metList);
            Assert.AreEqual(This_Avg, 5.134299, 0.001, "Wrong Avg WS Test 3");

            // Test 4
            Start = Convert.ToDateTime("9/3/2008 2:40");
            End = Convert.ToDateTime("9/7/2008 15:20");
            minDir = 326.25;
            maxDir = 348.75;
            TOD = Met.TOD.Night;
            season = Met.Season.Fall;
            thisInst.metList.numTOD = 2;
            thisInst.metList.numSeason = 4;

            This_Avg = thisStats.CalcAvgWS(thisMCP.targetData, Start, End, minDir, maxDir, TOD, season, thisInst.metList);
            Assert.AreEqual(This_Avg, 4.765827, 0.001, "Wrong Avg WS Test 4");

            // Test 5
            Start = Convert.ToDateTime("6/24/2008 15:00");
            End = Convert.ToDateTime("9/30/2008 23:50");
            minDir = 348.75;
            maxDir = 11.25;
            TOD = Met.TOD.All;
            season = Met.Season.All;
            thisInst.metList.numTOD = 1;
            thisInst.metList.numSeason = 1;

            This_Avg = thisStats.CalcAvgWS(thisMCP.targetData, Start, End, minDir, maxDir, TOD, season, thisInst.metList);
            Assert.AreEqual(This_Avg, 5.166236, 0.001, "Wrong Avg WS Test 5");

            thisInst.Close();
        }

        [TestMethod]
        public void Get_Data_Count_Test()
        {
            Continuum thisInst = new Continuum("");
            thisInst.isTest = true;
            string Filename = testingFolder + "\\Stats testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcpList[0];

            Stats thisStats = new Stats();

            // Test 1
            DateTime Start = Convert.ToDateTime("1/1/2005 12:00:00 AM");
            DateTime End = Convert.ToDateTime("12/31/2010 11:00:00 PM");
            int WD_Ind = 16;            
            Met.TOD TOD = Met.TOD.All;
            Met.Season season = Met.Season.All;

            int thisCount = thisStats.GetDataCount(thisMCP.refData, Start, End, WD_Ind, TOD, season, thisInst.metList, false);
            Assert.AreEqual(thisCount, 52584, 1, "Wrong Count Test 1");

            // Test 2
            Start = Convert.ToDateTime("8/5/2007 1:30");
            End = Convert.ToDateTime("11/2/2010 21:00");
            WD_Ind = 4;            
            TOD = Met.TOD.Day;
            season = Met.Season.Winter;
            thisInst.metList.numTOD = 2;
            thisInst.metList.numSeason = 4;

            thisCount = thisStats.GetDataCount(thisMCP.refData, Start, End, WD_Ind, TOD, season, thisInst.metList, false);
            Assert.AreEqual(thisCount, 163, 0, "Wrong Count Test 2");

            // Test 3
            Start = Convert.ToDateTime("6/24/2008 15:00");
            End = Convert.ToDateTime("9/30/2008 23:50");
            WD_Ind = 16;            
            TOD = Met.TOD.All;
            season = Met.Season.All;
            thisInst.metList.numTOD = 1;
            thisInst.metList.numSeason = 1;

            thisCount = thisStats.GetDataCount(thisMCP.targetData, Start, End, WD_Ind, TOD, season, thisInst.metList, false);
            Assert.AreEqual(thisCount, 2361, 0, "Wrong Count Test 3");

            // Test 4
            Start = Convert.ToDateTime("9/3/2008 2:40");
            End = Convert.ToDateTime("9/7/2008 15:20");
            WD_Ind = 15;            
            TOD = Met.TOD.Night;
            season = Met.Season.Fall;
            thisInst.metList.numTOD = 2;
            thisInst.metList.numSeason = 4;

            thisCount = thisStats.GetDataCount(thisMCP.targetData, Start, End, WD_Ind, TOD, season, thisInst.metList, false);
            Assert.AreEqual(thisCount, 7, 0, "Wrong Count Test 4");

            // Test 5
            Start = Convert.ToDateTime("6/24/2008 15:00");
            End = Convert.ToDateTime("9/30/2008 23:50");
            WD_Ind = 0;            
            TOD = Met.TOD.All;
            season = Met.Season.All;
            thisInst.metList.numTOD = 1;
            thisInst.metList.numSeason = 1;

            thisCount = thisStats.GetDataCount(thisMCP.targetData, Start, End, WD_Ind, TOD, season, thisInst.metList, false);
            Assert.AreEqual(thisCount, 93, 0.001, "Wrong Count Test 5");

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Variance_Test()
        {
            double[] values = new double[12];
            values[0] = 0.54f;
            values[1] = 0.108f;
            values[2] = 0.789f;
            values[3] = 0.55f;
            values[4] = 0.83f;
            values[5] = 3.64f;
            values[6] = 87.6f;
            values[7] = 5.3f;
            values[8] = 0.95f;
            values[9] = 3.5f;
            values[10] = 0.605f;
            values[11] = 2.36f;

            Stats These_Stats = new Stats();

            double This_Var = These_Stats.CalcVariance(values);
            Assert.AreEqual(This_Var, 565.507, 0.001, "Wrong Variance");

        }

        [TestMethod]
        public void Calc_Covariance_Test()
        {
            double[] X_Values = new double[12];
            X_Values[0] = 0.54f;
            X_Values[1] = 0.108f;
            X_Values[2] = 0.789f;
            X_Values[3] = 0.55f;
            X_Values[4] = 0.83f;
            X_Values[5] = 3.64f;
            X_Values[6] = 87.6f;
            X_Values[7] = 5.3f;
            X_Values[8] = 0.95f;
            X_Values[9] = 3.5f;
            X_Values[10] = 0.605f;
            X_Values[11] = 2.36f;

            double[] Y_Values = new double[12];
            Y_Values[0] = 4.56f;
            Y_Values[1] = 6.07f;
            Y_Values[2] = 0.95f;
            Y_Values[3] = 31f;
            Y_Values[4] = 1.67f;
            Y_Values[5] = 0.21f;
            Y_Values[6] = 8.34f;
            Y_Values[7] = 11.54f;
            Y_Values[8] = 3.66f;
            Y_Values[9] = 4.54f;
            Y_Values[10] = 0.85f;
            Y_Values[11] = 5.89f;

            Stats These_Stats = new Stats();
            double This_Cov = These_Stats.CalcCovariance(X_Values, Y_Values);
            Assert.AreEqual(This_Cov, 11.93237, 0.001, "Wrong co-variance");

        }

        [TestMethod]
        public void CalcR_Sqr()
        {
            double[] X_Values = new double[12];
            X_Values[0] = 0.54f;
            X_Values[1] = 0.108f;
            X_Values[2] = 0.789f;
            X_Values[3] = 0.55f;
            X_Values[4] = 0.83f;
            X_Values[5] = 3.64f;
            X_Values[6] = 87.6f;
            X_Values[7] = 5.3f;
            X_Values[8] = 0.95f;
            X_Values[9] = 3.5f;
            X_Values[10] = 0.605f;
            X_Values[11] = 2.36f;

            double[] Y_Values = new double[12];
            Y_Values[0] = 4.56f;
            Y_Values[1] = 6.07f;
            Y_Values[2] = 0.95f;
            Y_Values[3] = 31f;
            Y_Values[4] = 1.67f;
            Y_Values[5] = 0.21f;
            Y_Values[6] = 8.34f;
            Y_Values[7] = 11.54f;
            Y_Values[8] = 3.66f;
            Y_Values[9] = 4.54f;
            Y_Values[10] = 0.85f;
            Y_Values[11] = 5.89f;

            Stats These_Stats = new Stats();
            double Var_X = These_Stats.CalcVariance(X_Values);
            double Var_Y = These_Stats.CalcVariance(Y_Values);
            double This_Cov = These_Stats.CalcCovariance(X_Values, Y_Values);
            double This_Rsq = These_Stats.CalcR_Sqr((float)This_Cov, (float)Var_X, (float)Var_Y);

            Assert.AreEqual(This_Rsq, 0.00392, 0.0001, "Wrong R sq");
        }

        [TestMethod]
        public void Calc_Intercept_Test()
        {
            double X = 6.456f;
            double Y = 5.289f;
            double Slope = 1.0943f;

            Stats These_Stats = new Stats();
            double This_Int = These_Stats.CalcIntercept(Y, Slope, X);

            Assert.AreEqual(This_Int, -1.7758, 0.001, "Wrong Intercept");

        }
        
    }
    
}
