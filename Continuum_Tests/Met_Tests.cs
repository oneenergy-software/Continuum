using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class Met_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;

        public Met_Tests()
        {
            testingFolder = globals.testingFolder + "Met";
        }

        [TestMethod]
        public void CalcAvgWS_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\Met testing RDM.cfm";
            thisInst.Open(Filename);
            thisInst.metList.metItem[0].CalcAvgWS(thisInst);
            thisInst.metList.metItem[1].CalcAvgWS(thisInst);
            thisInst.metList.metItem[2].CalcAvgWS(thisInst);
            thisInst.metList.metItem[3].CalcAvgWS(thisInst);

            Met.WSWD_Dist thisDist = thisInst.metList.metItem[0].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
            Assert.AreEqual(thisDist.WS, 7.478587, 0.001, "Wrong WS at Met 10");

            thisDist = thisInst.metList.metItem[1].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
            Assert.AreEqual(thisDist.WS, 8.07606, 0.001, "Wrong WS at Met 15");

            thisDist = thisInst.metList.metItem[2].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
            Assert.AreEqual(thisDist.WS, 8.28380, 0.001, "Wrong WS at Met 16");

            thisDist = thisInst.metList.metItem[3].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
            Assert.AreEqual(thisDist.WS, 7.140587, 0.001, "Wrong WS at Met 17");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcSectorWS_Ratios_Test()
        {
            Continuum thisInst = new Continuum("");
            
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.hemisphere = "Northern";
            thisInst.ofdMets.FileName = testingFolder + "\\Archbold.TAB";
            thisInst.ImportMetsTAB();
            Met thisMet = thisInst.metList.metItem[0];
            thisMet.CalcSectorWS_Ratios(thisInst);

            Assert.AreEqual(thisMet.WSWD_Dists[0].sectorWS_Ratio[0], 0.868457, 0.001, "Wrong Sector WS index 0");
            Assert.AreEqual(thisMet.WSWD_Dists[0].sectorWS_Ratio[4], 0.880843, 0.001, "Wrong Sector WS index 4");
            Assert.AreEqual(thisMet.WSWD_Dists[0].sectorWS_Ratio[9], 1.027484, 0.001, "Wrong Sector WS index 9");
            Assert.AreEqual(thisMet.WSWD_Dists[0].sectorWS_Ratio[15], 0.962568, 0.001, "Wrong Sector WS index 15");

        }

        [TestMethod]
        public void CalcLT_WSWD_Dists_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\Met testing.cfm";
            thisInst.Open(fileName);

            Met thisMet = thisInst.metList.metItem[0];
            thisInst.metList.numWD = 16;
            thisInst.metList.numTOD = 1;
            thisInst.metList.numSeason = 1;
            thisInst.ResetTimeSeries();

            string MCP_Method = "Orth. Regression";
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
            int offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
            Reference thisMERRA = thisInst.refList.GetAllRefsAtLatLong(theseLL.latitude, theseLL.longitude)[0];
            thisInst.metList.RunMCP(ref thisMet, thisMERRA, thisInst, MCP_Method);
            thisInst.metList.isMCPd = true;

            // Test 1
            Met.WSWD_Dist thisDist = thisMet.CalcLT_WSWD_Dists(80, Met.TOD.All, Met.Season.All, thisInst, thisMet.mcp.LT_WS_Ests);

            Assert.AreEqual(thisDist.WS, 6.566949, 0.01, "Wrong overall WS Test 1");
            Assert.AreEqual(thisDist.sectorWS_Ratio[0] * thisDist.WS, 4.993328, 0.01, "Wrong WS in WD 0 Test 1");
            Assert.AreEqual(thisDist.sectorWS_Ratio[4] * thisDist.WS, 5.192708, 0.01, "Wrong WS in WD 4 Test 1");
            Assert.AreEqual(thisDist.sectorWS_Ratio[13] * thisDist.WS, 6.9277, 0.01, "Wrong WS in WD 13 Test 1");
            Assert.AreEqual(thisDist.sectorWS_Ratio[15] * thisDist.WS, 5.627835, 0.01, "Wrong WS in WD 15 Test 1");

            // Test 2 - 3
            thisInst.ResetTimeSeries();
            thisInst.metList.numTOD = 2;
            thisInst.metList.RunMCP(ref thisMet, thisMERRA, thisInst, MCP_Method);
            thisInst.metList.isMCPd = true;

            thisDist = thisMet.CalcLT_WSWD_Dists(80, Met.TOD.Day, Met.Season.All, thisInst, thisMet.mcp.LT_WS_Ests);

            Assert.AreEqual(thisDist.WS, 6.296237, 0.01, "Wrong overall WS Test 2");
            Assert.AreEqual(thisDist.sectorWS_Ratio[0] * thisDist.WS, 4.80529, 0.01, "Wrong WS in WD 0 Test 2");
            Assert.AreEqual(thisDist.sectorWS_Ratio[2] * thisDist.WS, 5.194316, 0.01, "Wrong WS in WD 2 Test 2");
            Assert.AreEqual(thisDist.sectorWS_Ratio[8] * thisDist.WS, 6.918986, 0.01, "Wrong WS in WD 8 Test 2");
            Assert.AreEqual(thisDist.sectorWS_Ratio[15] * thisDist.WS, 5.327543, 0.01, "Wrong WS in WD 15 Test 2");

            thisDist = thisMet.CalcLT_WSWD_Dists(80, Met.TOD.Night, Met.Season.All, thisInst, thisMet.mcp.LT_WS_Ests);

            Assert.AreEqual(thisDist.WS, 6.83764, 0.01, "Wrong overall WS Test 3");
            Assert.AreEqual(thisDist.sectorWS_Ratio[0] * thisDist.WS, 5.182166, 0.01, "Wrong WS in WD 0 Test 3");
            Assert.AreEqual(thisDist.sectorWS_Ratio[5] * thisDist.WS, 5.764806, 0.01, "Wrong WS in WD 2 Test 3");
            Assert.AreEqual(thisDist.sectorWS_Ratio[12] * thisDist.WS, 7.685679, 0.01, "Wrong WS in WD 8 Test 3");
            Assert.AreEqual(thisDist.sectorWS_Ratio[15] * thisDist.WS, 5.916732, 0.01, "Wrong WS in WD 15 Test 3");

            // Test 4 - 7
            thisInst.ResetTimeSeries();
            thisInst.metList.numTOD = 1;
            thisInst.metList.numSeason = 4;
            thisInst.metList.RunMCP(ref thisMet, thisMERRA, thisInst, MCP_Method);
            thisInst.metList.isMCPd = true;

            thisDist = thisMet.CalcLT_WSWD_Dists(80, Met.TOD.All, Met.Season.Winter, thisInst, thisMet.mcp.LT_WS_Ests);

            Assert.AreEqual(thisDist.WS, 7.413855, 0.01, "Wrong overall WS Test 4");
            Assert.AreEqual(thisDist.sectorWS_Ratio[0] * thisDist.WS, 5.1053, 0.01, "Wrong WS in WD 0 Test 4");
            Assert.AreEqual(thisDist.sectorWS_Ratio[4] * thisDist.WS, 5.0283, 0.01, "Wrong WS in WD 4 Test 4");
            Assert.AreEqual(thisDist.sectorWS_Ratio[8] * thisDist.WS, 8.429297, 0.01, "Wrong WS in WD 8 Test 4");
            Assert.AreEqual(thisDist.sectorWS_Ratio[15] * thisDist.WS, 6.318312, 0.01, "Wrong WS in WD 15 Test 4");

            thisDist = thisMet.CalcLT_WSWD_Dists(80, Met.TOD.All, Met.Season.Spring, thisInst, thisMet.mcp.LT_WS_Ests);

            Assert.AreEqual(thisDist.WS, 7.015339, 0.01, "Wrong overall WS Test 5");
            Assert.AreEqual(thisDist.sectorWS_Ratio[2] * thisDist.WS, 6.313987, 0.01, "Wrong WS in WD 2 Test 5");
            Assert.AreEqual(thisDist.sectorWS_Ratio[6] * thisDist.WS, 6.10201, 0.01, "Wrong WS in WD 6 Test 5");
            Assert.AreEqual(thisDist.sectorWS_Ratio[12] * thisDist.WS, 8.21204, 0.01, "Wrong WS in WD 12 Test 5");

            thisDist = thisMet.CalcLT_WSWD_Dists(80, Met.TOD.All, Met.Season.Summer, thisInst, thisMet.mcp.LT_WS_Ests);

            Assert.AreEqual(thisDist.WS, 5.344627, 0.01, "Wrong overall WS Test 6");
            Assert.AreEqual(thisDist.sectorWS_Ratio[0] * thisDist.WS, 4.7078, 0.01, "Wrong WS in WD 2 Test 6");
            Assert.AreEqual(thisDist.sectorWS_Ratio[4] * thisDist.WS, 4.88008, 0.01, "Wrong WS in WD 6 Test 6");
            Assert.AreEqual(thisDist.sectorWS_Ratio[14] * thisDist.WS, 4.95684, 0.01, "Wrong WS in WD 12 Test 6");

            thisDist = thisMet.CalcLT_WSWD_Dists(80, Met.TOD.All, Met.Season.Fall, thisInst, thisMet.mcp.LT_WS_Ests);

            Assert.AreEqual(thisDist.WS, 6.50441, 0.01, "Wrong overall WS Test 7");
            Assert.AreEqual(thisDist.sectorWS_Ratio[1] * thisDist.WS, 4.96994, 0.01, "Wrong WS in WD 1 Test 7");
            Assert.AreEqual(thisDist.sectorWS_Ratio[3] * thisDist.WS, 5.28866, 0.01, "Wrong WS in WD 3 Test 7");
            Assert.AreEqual(thisDist.sectorWS_Ratio[9] * thisDist.WS, 8.097313, 0.01, "Wrong WS in WD 9 Test 7");

            // Test 9
            thisInst.ResetTimeSeries();
            thisInst.metList.numTOD = 2;
            thisInst.metList.numSeason = 4;
            thisInst.metList.numWD = 24;
            thisInst.metList.RunMCP(ref thisMet, thisMERRA, thisInst, MCP_Method);
            thisInst.metList.isMCPd = true;

            thisDist = thisMet.CalcLT_WSWD_Dists(80, Met.TOD.Night, Met.Season.Summer, thisInst, thisMet.mcp.LT_WS_Ests);

            Assert.AreEqual(thisDist.WS, 5.76306, 0.01, "Wrong overall WS Test 9");
            Assert.AreEqual(thisDist.sectorWS_Ratio[0] * thisDist.WS, 4.90395, 0.01, "Wrong WS in WD 0 Test 4");
            Assert.AreEqual(thisDist.sectorWS_Ratio[11] * thisDist.WS, 5.706386, 0.01, "Wrong WS in WD 4 Test 4");
            Assert.AreEqual(thisDist.sectorWS_Ratio[20] * thisDist.WS, 5.528143, 0.01, "Wrong WS in WD 8 Test 4");
            Assert.AreEqual(thisDist.sectorWS_Ratio[23] * thisDist.WS, 5.174853, 0.01, "Wrong WS in WD 15 Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcMeas_WSWD_Dists_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\Met testing.cfm";
            thisInst.Open(fileName);

            Met thisMet = thisInst.metList.metItem[0];
            thisMet.metData.GetSensorDataFromDB(thisInst, thisMet.name);

            // Test 1 
            Met.WSWD_Dist thisDist = thisMet.CalcMeas_WSWD_Dists(80, Met.TOD.All, Met.Season.All, thisInst, thisMet.metData.GetSimulatedTimeSeries(80));

            Assert.AreEqual(thisDist.WS, 6.617531, 0.01, "Wrong WS Test 1");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[0], 4.842588, 0.01, "Wrong WS Test 1");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[5], 5.461858, 0.01, "Wrong WS Test 1");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[15], 5.159092, 0.01, "Wrong WS Test 1");

            // Test 2
            thisInst.metList.numTOD = 2;
            thisDist = thisMet.CalcMeas_WSWD_Dists(80, Met.TOD.Day, Met.Season.All, thisInst, thisMet.metData.GetSimulatedTimeSeries(80));
            Assert.AreEqual(thisDist.WS, 6.374398, 0.01, "Wrong WS Test 2");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[0], 4.51033, 0.01, "Wrong WS Test 2");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[3], 5.039229, 0.01, "Wrong WS Test 2");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[10], 7.798786, 0.01, "Wrong WS Test 2");

            // Test 3
            thisDist = thisMet.CalcMeas_WSWD_Dists(80, Met.TOD.Night, Met.Season.All, thisInst, thisMet.metData.GetSimulatedTimeSeries(80));
            Assert.AreEqual(thisDist.WS, 6.8643, 0.01, "Wrong WS Test 3");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[1], 5.241824, 0.01, "Wrong WS Test 3");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[7], 7.063508, 0.01, "Wrong WS Test 3");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[15], 5.41911, 0.01, "Wrong WS Test 3");

            // Test 4
            thisInst.metList.numTOD = 1;
            thisInst.metList.numSeason = 4;
            thisDist = thisMet.CalcMeas_WSWD_Dists(80, Met.TOD.All, Met.Season.Winter, thisInst, thisMet.metData.GetSimulatedTimeSeries(80));
            Assert.AreEqual(thisDist.WS, 8.033652, 0.01, "Wrong WS Test 4");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[0], 4.493193, 0.01, "Wrong WS Test 4");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[11], 9.351834, 0.01, "Wrong WS Test 4");

            thisDist = thisMet.CalcMeas_WSWD_Dists(80, Met.TOD.All, Met.Season.Summer, thisInst, thisMet.metData.GetSimulatedTimeSeries(80));
            Assert.AreEqual(thisDist.WS, 5.437018, 0.01, "Wrong WS Test 5");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[4], 4.760074, 0.01, "Wrong WS Test 5");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[15], 4.57385, 0.01, "Wrong WS Test 5");

            // Test 6
            thisInst.metList.numTOD = 2;
            thisDist = thisMet.CalcMeas_WSWD_Dists(80, Met.TOD.Day, Met.Season.Spring, thisInst, thisMet.metData.GetSimulatedTimeSeries(80));
            Assert.AreEqual(thisDist.WS, 6.733493, 0.01, "Wrong WS Test 6");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[1], 5.530139, 0.01, "Wrong WS Test 6");
            Assert.AreEqual(thisDist.WS * thisDist.sectorWS_Ratio[15], 5.760701, 0.01, "Wrong WS Test 6");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcTurbulenceIntensity_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\Met testing.cfm";
            thisInst.Open(fileName);

            Met thisMet = thisInst.metList.metItem[0];
            thisMet.CalcTurbulenceIntensity(thisMet.metData.startDate, thisMet.metData.endDate, 80, thisInst);

            double test1 = thisMet.turbulence.avgSD[6, 0] / thisMet.turbulence.avgWS[6, 0];
            double test2 = thisMet.turbulence.p90SD[6, 0] / thisMet.turbulence.avgWS[6, 0];
            double test3 = thisMet.turbulence.avgSD[7, 10] / thisMet.turbulence.avgWS[7, 10];
            double test4 = thisMet.turbulence.p90SD[7, 10] / thisMet.turbulence.avgWS[7, 10];
            double test5 = thisMet.turbulence.avgSD[10, 12] / thisMet.turbulence.avgWS[10, 12];
            double test6 = thisMet.turbulence.p90SD[10, 12] / thisMet.turbulence.avgWS[10, 12];

            // Test 1: Avg TI, WD = 0, WS = 6
            Assert.AreEqual(test1, 0.098983, 0.001, "Wrong Avg TI Test 1");

            // Test 2: Representative TI, WD = 0, WS = 6
            Assert.AreEqual(test2, 0.16468, 0.001, "Wrong Rep TI Test 2");

            // Test 3: Avg TI, WD = 10, WS = 7
            Assert.AreEqual(test3, 0.090581, 0.001, "Wrong Avg TI Test 3");

            // Test 4: Representative TI, WD = 10, WS = 7
            Assert.AreEqual(test4, 0.14028, 0.001, "Wrong Rep TI Test 4");

            // Test 5: Avg TI, WD = 12, WS = 10
            Assert.AreEqual(test5, 0.097486, 0.001, "Wrong Avg TI Test 5");

            // Test 6: Representative TI, WD = 12, WS = 10
            Assert.AreEqual(test6, 0.13010, 0.001, "Wrong Rep TI Test 6");
            
            thisInst.Close();

        }

        [TestMethod]
        public void CalcOverallTurbulenceIntensity()
        {
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\Met testing.cfm";
            thisInst.Open(fileName);

            Met thisMet = thisInst.metList.metItem[0];
            thisMet.CalcTurbulenceIntensity(thisMet.metData.startDate, thisMet.metData.endDate, 80, thisInst);
            Met.TIandCount[] thisTI = thisMet.CalcOverallTurbulenceIntensity("Average", thisInst);

            Assert.AreEqual(thisTI[3].overallTI, 0.1498, 0.001, "Wrong Avg TI Test 1");
            Assert.AreEqual(thisTI[6].overallTI, 0.0987, 0.001, "Wrong Avg TI Test 2");
            Assert.AreEqual(thisTI[9].overallTI, 0.0894, 0.001, "Wrong Avg TI Test 3");
            Assert.AreEqual(thisTI[12].overallTI, 0.1002, 0.001, "Wrong Avg TI Test 4");

            thisTI = thisMet.CalcOverallTurbulenceIntensity("Representative", thisInst);
            Assert.AreEqual(thisTI[3].overallTI, 0.2417, 0.001, "Wrong Avg TI Test 1");
            Assert.AreEqual(thisTI[6].overallTI, 0.1585, 0.001, "Wrong Avg TI Test 2");
            Assert.AreEqual(thisTI[9].overallTI, 0.1321, 0.001, "Wrong Avg TI Test 3");
            Assert.AreEqual(thisTI[12].overallTI, 0.1332, 0.001, "Wrong Avg TI Test 4");
            Assert.AreEqual(thisTI[15].overallTI, 0.1330, 0.001, "Wrong Avg TI Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void GetAlphaHistogram()
        {
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\Met testing.cfm";
            thisInst.Open(fileName);

            Met thisMet = thisInst.metList.metItem[0];
            string WS_Range = "All > Cut-In";

            // Test 1: All > Cut-In
            double[] alphaHisto = thisMet.GetAlphaHistogram(WS_Range, thisInst, thisMet.metData.allStartDate, thisMet.metData.allEndDate);

            int thisDelta = Convert.ToInt16(3 * 0.01);
            Assert.AreEqual(alphaHisto[0], 3, thisDelta, "Wrong Alpha Histo Test 1");

            thisDelta = Convert.ToInt16(377 * 0.01);
            Assert.AreEqual(alphaHisto[24], 377, thisDelta, "Wrong Alpha Histo Test 1");

            thisDelta = Convert.ToInt16(841 * 0.01);
            Assert.AreEqual(alphaHisto[48], 841, thisDelta, "Wrong Alpha Histo Test 1");

            thisDelta = Convert.ToInt16(236 * 0.01);
            Assert.AreEqual(alphaHisto[62], 236, thisDelta, "Wrong Alpha Histo Test 1");

            thisDelta = Convert.ToInt16(39 * 0.01);
            Assert.AreEqual(alphaHisto[79], 39, 0, "Wrong Alpha Histo Test 1");

            Assert.AreEqual(alphaHisto[87], 28, 0, "Wrong Alpha Histo Test 1");
            Assert.AreEqual(alphaHisto[99], 15, 0, "Wrong Alpha Histo Test 1");

            // Test 2: 5 - 10 m/s
            WS_Range = "5 - 10 m/s";
            alphaHisto = thisMet.GetAlphaHistogram(WS_Range, thisInst, thisMet.metData.allStartDate, thisMet.metData.allEndDate);

            thisDelta = Convert.ToInt16(0 * 0.01);
            Assert.AreEqual(alphaHisto[3], 0, thisDelta, "Wrong Alpha Histo Test 2");

            thisDelta = Convert.ToInt16(1157 * 0.01);
            Assert.AreEqual(alphaHisto[35], 1157, thisDelta, "Wrong Alpha Histo Test 2");

            thisDelta = Convert.ToInt16(759 * 0.01);
            Assert.AreEqual(alphaHisto[44], 759, thisDelta, "Wrong Alpha Histo Test 2");

            thisDelta = Convert.ToInt16(70 * 0.01);
            Assert.AreEqual(alphaHisto[71], 70, thisDelta, "Wrong Alpha Histo Test 2");

            thisDelta = Convert.ToInt16(11 * 0.01);
            Assert.AreEqual(alphaHisto[95], 11, thisDelta, "Wrong Alpha Histo Test 2");

            // Test 3: 10 - 15 m/s
            WS_Range = "10 - 15 m/s";
            alphaHisto = thisMet.GetAlphaHistogram(WS_Range, thisInst, thisMet.metData.allStartDate, thisMet.metData.allEndDate);

            thisDelta = Convert.ToInt16(423 * 0.01);
            Assert.AreEqual(alphaHisto[34], 423, thisDelta, "Wrong Alpha Histo Test 3");

            thisDelta = Convert.ToInt16(235 * 0.01);
            Assert.AreEqual(alphaHisto[40], 235, thisDelta, "Wrong Alpha Histo Test 3");

            // Test 4: 15+ m/s
            WS_Range = "15+ m/s";
            alphaHisto = thisMet.GetAlphaHistogram(WS_Range, thisInst, thisMet.metData.allStartDate, thisMet.metData.allEndDate);

            thisDelta = Convert.ToInt16(46 * 0.01);
            Assert.AreEqual(alphaHisto[29], 46, thisDelta, "Wrong Alpha Histo Test 3");

            thisDelta = Convert.ToInt16(53 * 0.01);
            Assert.AreEqual(alphaHisto[35], 53, thisDelta, "Wrong Alpha Histo Test 3");

            thisInst.Close();
        }

        [TestMethod]
        public void GetAlphaPValue_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\Met testing.cfm";
            thisInst.Open(fileName);

            Met thisMet = thisInst.metList.metItem[0];

            double thisAlphaVal = thisMet.GetAlphaPValue(5, 10, 10, thisInst, thisMet.metData.allStartDate, thisMet.metData.allEndDate);
            Assert.AreEqual(thisAlphaVal, 0.6537, 0.001, "Wrong Alpha P Value Test 1");

            thisAlphaVal = thisMet.GetAlphaPValue(5, 10, 50, thisInst, thisMet.metData.allStartDate, thisMet.metData.allEndDate);
            Assert.AreEqual(thisAlphaVal, 0.276, 0.001, "Wrong Alpha P Value Test 2");

            thisAlphaVal = thisMet.GetAlphaPValue(5, 10, 90, thisInst, thisMet.metData.allStartDate, thisMet.metData.allEndDate);
            Assert.AreEqual(thisAlphaVal, 0.0791, 0.001, "Wrong Alpha P Value Test 3");

            thisAlphaVal = thisMet.GetAlphaPValue(5, 10, 99, thisInst, thisMet.metData.allStartDate, thisMet.metData.allEndDate);
            Assert.AreEqual(thisAlphaVal, -0.0082, 0.001, "Wrong Alpha P Value Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void GetMaxYearlyWinds_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\Extreme WS\\Extreme WS Ashta Iten.cfm";
            thisInst.Open(fileName);
            Met thisMet = thisInst.metList.metItem[0];

            Met.MaxYearlyWind[] maxWS = thisMet.GetMaxYearlyWinds("10-min", thisInst);

            Assert.AreEqual(maxWS[0].maxWS, 16.0934, 0.001, "Wrong max 10-min WS 2007");
            Assert.AreEqual(maxWS[1].maxWS, 18.28394, 0.001, "Wrong max 10-min WS 2008");

            maxWS = thisMet.GetMaxYearlyWinds("Gust", thisInst);

            Assert.AreEqual(maxWS[0].maxWS, 25.973, 0.001, "Wrong max gust WS 2007");
            Assert.AreEqual(maxWS[1].maxWS, 29.0576, 0.001, "Wrong max gust WS 2008");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcExtremeWindSpeeds_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\Extreme WS\\Extreme WS Ashta Iten.cfm";
            thisInst.Open(fileName);
            Met thisMet = thisInst.metList.metItem[0];

            Met.Extreme_WindSpeed maxWS = thisMet.CalcExtremeWindSpeeds(thisInst, thisInst.refList.reference[0]);

            Assert.AreEqual(maxWS.gust1yr, 27.0, 0.1, "Wrong Gust 1-year WS");
            Assert.AreEqual(maxWS.tenMin1yr, 16.87, 0.1, "Wrong Ten-Min 1yrear WS");
            Assert.AreEqual(maxWS.gust50yr, 32.0, 0.1, "Wrong Gust 50-year WS");
            Assert.AreEqual(maxWS.tenMin50yr, 20.0, 0.1, "Wrong Ten-Min 50-year WS");

            thisInst.Close();
        }
    }
}
