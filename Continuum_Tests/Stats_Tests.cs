using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;

namespace Continuum_Tests
{
    [TestClass]
    public class Stats_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Stats";

        [TestMethod]
        public void Calc_Avg_WS_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\Stats testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;

            Stats thisStats = new Stats();
            DateTime Start = Convert.ToDateTime("10/1/2008 12:00:00 AM");
            DateTime End = Convert.ToDateTime("10/31/2008 11:00:00 PM");

            // Test 1
            double This_Avg = thisStats.CalcAvgWS(thisMCP.targetData, 6, 7, Start, End, 90, 270, Met.TOD.All, Met.Season.All, thisMCP);
            Assert.AreEqual(This_Avg, 6.49889, 0.001, "Wrong Avg WS");

            // Test 2
            Start = Convert.ToDateTime("2/1/2009 12:00:00 AM");
            End = Convert.ToDateTime("2/13/2009 1:00:00 PM");
            thisMCP.numTODs = 2;

            This_Avg = thisStats.CalcAvgWS(thisMCP.targetData, 5, 10, Start, End, 210, 300, Met.TOD.All, Met.Season.All, thisMCP);
            Assert.AreEqual(This_Avg, 6.783322, 0.001, "Wrong Avg WS");

            thisInst.Close();
        }

        [TestMethod]
        public void Get_Data_Count_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\Stats testing.cfm";
            thisInst.Open(Filename);
            Met thisMet = thisInst.metList.metItem[0];
            MCP thisMCP = thisMet.mcp;
            thisMCP.numWD = 16;
            thisMCP.numTODs = 1;
            thisMCP.numSeasons = 2;
            
            DateTime Start = Convert.ToDateTime("3/4/2009 4:00:00 AM");
            DateTime End = Convert.ToDateTime("5/16/2009 4:00:00 PM");
            Stats thisStats = new Stats();

            // Test 1
            int thisCount = thisStats.Get_Data_Count(thisMCP.refData, Start, End, 7, Met.TOD.All, Met.Season.All, thisMCP, false);
            Assert.AreEqual(thisCount, 45, 0, "Wrong data count");

            thisMCP.numWD = 4;
            thisMCP.numTODs = 2;
            thisMCP.numSeasons = 2;
            
            Start = Convert.ToDateTime("1/1/1985 12:00:00 AM");
            End = Convert.ToDateTime("12/31/2015 12:00:00 AM");

            // Test 2
            thisCount = thisStats.Get_Data_Count(thisMCP.refData, Start, End, 0, Met.TOD.All, Met.Season.All, thisMCP, false);
            Assert.AreEqual(thisCount, 14114, 0, "Wrong data count");

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

            double This_Var = These_Stats.Calc_Variance(values);
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
            double This_Cov = These_Stats.Calc_Covariance(X_Values, Y_Values);
            Assert.AreEqual(This_Cov, 11.93237, 0.001, "Wrong co-variance");

        }

        [TestMethod]
        public void Calc_R_sqr()
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
            double Var_X = These_Stats.Calc_Variance(X_Values);
            double Var_Y = These_Stats.Calc_Variance(Y_Values);
            double This_Cov = These_Stats.Calc_Covariance(X_Values, Y_Values);
            double This_Rsq = These_Stats.Calc_R_sqr((float)This_Cov, (float)Var_X, (float)Var_Y);

            Assert.AreEqual(This_Rsq, 0.00392, 0.0001, "Wrong R sq");
        }

        [TestMethod]
        public void Calc_Intercept_Test()
        {
            double X = 6.456f;
            double Y = 5.289f;
            double Slope = 1.0943f;

            Stats These_Stats = new Stats();
            double This_Int = These_Stats.Calc_Intercept(Y, Slope, X);

            Assert.AreEqual(This_Int, -1.7758, 0.001, "Wrong Intercept");

        }
        
    }
    
}
