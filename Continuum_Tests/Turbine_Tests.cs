using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class Turbine_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Turbine";

        [TestMethod]
        public void DoWS_EstAlongNodes_Test()
        {
            Continuum thisInst = new Continuum();
            

            string Filename = testingFolder + "\\Turbine test.cfm";
            thisInst.Open(Filename);
            thisInst.turbineList.AddTurbine("Test", 347212, 5307579, 1);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 4000, 1.0f, 1);
            double[] windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), 347212, 5307579, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

            thisInst.turbineList.turbineEsts[0].gridStats.GetGridArrayAndCalcStats(thisInst.turbineList.turbineEsts[0].UTMX, thisInst.turbineList.turbineEsts[0].UTMY, thisInst);
            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(),
                false, Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            Met thisMet = thisInst.metList.metItem[0];
            Turbine.WS_Ests New_WS_Est = new Turbine.WS_Ests();
            New_WS_Est.predictorMetName = thisMet.name;
            New_WS_Est.model = models[0];
            New_WS_Est.radius = 4000;

            NodeCollection nodeList = new NodeCollection();
            thisInst.turbineList.turbineEsts[0].AddWS_Estimate(New_WS_Est);
            Nodes startNode = nodeList.GetMetNode(thisMet);
            Nodes targetNode = nodeList.GetTurbNode(thisInst.turbineList.turbineEsts[0]);
            
            Nodes[] pathOfNodes = nodeList.FindPathOfNodes(startNode, targetNode, models[0], thisInst);
            int WS_Est_ind = thisInst.turbineList.turbineEsts[0].WSEst_Count - 1;
            thisInst.turbineList.turbineEsts[0].DoWS_EstAlongNodes(thisMet, models[0], pathOfNodes, thisInst, windRose);

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
        public void GenerateAvgWSFromTABs_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\Turbine test.cfm";
            thisInst.Open(Filename);
            thisInst.turbineList.AddTurbine("Test", 347212, 5307579, 1);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 4000, 1.0f, 1);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 6000, 1.0f, 1);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 8000, 1.0f, 1);
            thisInst.turbineList.CalcTurbineExposures(thisInst, 10000, 1.0f, 1);
            double[] windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), 347212, 5307579, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), 4000, 10000, true, Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            thisInst.turbineList.turbineEsts[0].DoTurbineCalcs(thisInst, models);
            thisInst.turbineList.turbineEsts[0].GenerateAvgWSFromTABs(thisInst, models, windRose, false);

            Assert.AreEqual(thisInst.turbineList.turbineEsts[0].avgWS_Est[0].freeStream.WS, 7.950309, 0.001, "Wrong average wind speed");

            thisInst.Close();


        }                

        [TestMethod]
        public void DoTurbineCalcs_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(Filename);

            thisInst.turbineList.AddTurbine("Test1", 347212, 5307579, 1);
            thisInst.turbineList.AddTurbine("Test2", 346306, 5311256, 1);
            thisInst.turbineList.AddTurbine("Test3", 342100, 5314708, 1);

            double[] power = new double[31];
            string Power_file = testingFolder + "\\Power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[31];
            string Thrust_file = testingFolder + "\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            thisInst.turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87, 16, 10, 1, 0);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            for (int i = 0; i <= thisInst.radiiList.ThisCount - 1; i++)
            {
                int radius = thisInst.radiiList.investItem[i].radius;
                double exponent = thisInst.radiiList.investItem[i].exponent;

                thisInst.turbineList.CalcTurbineExposures(thisInst, radius, exponent, 1);
            }

            for (int i = 0; i <= 2; i++)
            {
                double[] windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), thisInst.turbineList.turbineEsts[i].UTMX,
                    thisInst.turbineList.turbineEsts[i].UTMY, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
                Model[] UWDW_Mods = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(),
                    false, Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
                thisInst.turbineList.turbineEsts[i].DoTurbineCalcs(thisInst, UWDW_Mods);

                UWDW_Mods = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(),
                    true, Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
                thisInst.turbineList.turbineEsts[i].DoTurbineCalcs(thisInst, UWDW_Mods);
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
        public void CalcTurbineWakeLosses_Test()
        {
            Continuum thisInst = new Continuum();
            

            string Filename = testingFolder + "Turbine testing.cfm";
            thisInst.Open(Filename);

            thisInst.wakeModelList.AddWakeModel(0, 5, 12, thisInst.turbineList.GetPowerCurve("GW 87/1500 1.205 kg/m^3,,"), 10, 3, 0.05f, "Linear");
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

            minDist = (2 * 87);
            if (maxDist == 0) maxDist = 15000; // maxDist will be zero when there is only one turbine. Might be good to make this value constant
            Wake_coeffs = thisInst.wakeModelList.GetWakeLossesCoeffs(minDist, maxDist, thisInst.wakeModelList.wakeModels[0], thisInst.metList);

            thisInst.turbineList.turbineEsts[0].CalcTurbineWakeLosses(thisInst, Wake_coeffs, thisInst.wakeModelList.wakeModels[0], false);

            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].AvgWSEst_Count, 0, "Didn't add average WS est");
            int avgWS_Ind = thisInst.turbineList.turbineEsts[0].AvgWSEst_Count - 1;            
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].avgWS_Est[avgWS_Ind].wakeModel, null, "Didn't save wake model");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].avgWS_Est[avgWS_Ind].waked.sectorWS, null, "Didn't save sectorwise wind speeds");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].avgWS_Est[avgWS_Ind].waked.WS, null, "Didn't save wind speeds");

            int Net_AEP_ind = thisInst.turbineList.turbineEsts[0].NetAEP_Count - 1;
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].wakeLoss, 0, "Didn't calculate wake loss");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].wakeModel, null, "Didn't save wake model");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].CF, null, "Didn't calculate CF");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].AEP, null, "Didn't calculate AEP est");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].P90, 0, "Didn't calculate P90");
            Assert.AreNotEqual(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].P99, 0, "Didn't calculate P99");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].sectorEnergy, null, "Didn't calculate sectorwise WS dists");
            Assert.AreNotSame(thisInst.turbineList.turbineEsts[0].netAEP[Net_AEP_ind].sectorWakeLoss, null, "Didn't calculate sectorwise wake loss");

            thisInst.Close();
        }

        [TestMethod]
        public void GenerateAvgWSTimeSeries_Test()
        {

            Continuum thisInst = new Continuum();
            
            string fileName = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            NodeCollection nodeList = new NodeCollection();
            Nodes targetNode = nodeList.GetTurbNode(thisTurb);
            string MCP_Method = thisInst.Get_MCP_Method();
            TurbineCollection.PowerCurve powerCurve = new TurbineCollection.PowerCurve();
            Wake_Model wakeModel = new Wake_Model();

            ModelCollection.TimeSeries[] thisTS = thisInst.modelList.GenerateTimeSeries(thisInst, thisInst.metList.GetMetsUsed(), targetNode, false,
                powerCurve, wakeModel, null, MCP_Method);
            thisTurb.GenerateAvgWSTimeSeries(thisTS, thisInst, wakeModel, false, false, MCP_Method, false, powerCurve);

            Assert.AreEqual(thisTurb.avgWS_Est[0].freeStream.WS, 5.785786, 0.01, "Wrong WS Test 1");
            Assert.AreEqual(thisTurb.avgWS_Est[0].freeStream.sectorWS[8], 4.895387, 0.01, "Wrong WS Test 2");
            Assert.AreEqual(thisTurb.avgWS_Est[0].freeStream.sectorWS[10], 6.36991, 0.01, "Wrong WS Test 3");
            Assert.AreEqual(thisTurb.avgWS_Est[0].freeStream.sectorWS[15], 5.52052, 0.01, "Wrong WS Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcMonthlyWS_Values_Test()
        {
            Continuum thisInst = new Continuum();
            
            string fileName = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            NodeCollection nodeList = new NodeCollection();
            Nodes targetNode = nodeList.GetTurbNode(thisTurb);
            string MCP_Method = thisInst.Get_MCP_Method();
            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0]; ;
            Wake_Model wakeModel = new Wake_Model();

            ModelCollection.TimeSeries[] thisTS = thisInst.modelList.GenerateTimeSeries(thisInst, thisInst.metList.GetMetsUsed(), targetNode, false,
                powerCurve, wakeModel, null, MCP_Method);

            Turbine.MonthlyWS_Vals[] monthlyWS = thisTurb.CalcMonthlyWS_Values(thisTS, thisInst, "Freestream");

            Assert.AreEqual(monthlyWS[0].avgWS, 6.136874, 0.01, "Wrong monthly WS Jan 2005 Test 1");
            Assert.AreEqual(monthlyWS[15].avgWS, 6.39317, 0.01, "Wrong monthly WS Apr 2006 Test 2");
            Assert.AreEqual(monthlyWS[45].avgWS, 5.8777, 0.01, "Wrong monthly WS Oct 2008 Test 3");
            Assert.AreEqual(monthlyWS[71].avgWS, 6.03145, 0.01, "Wrong monthly WS Dec 2010 Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcYearlyValue_Test()
        {
            Continuum thisInst = new Continuum();
            
            string fileName = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            Wake_Model wakeModel = thisInst.wakeModelList.wakeModels[0];

            double thisVal = thisTurb.CalcYearlyValue(2005, "Avg WS", false, null, new TurbineCollection.PowerCurve());
            Assert.AreEqual(thisVal, 5.710609, 0.01, "Wrong Avg WS Test 1");

            thisVal = thisTurb.CalcYearlyValue(2007, "Gross AEP", false, wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 3779.421, 0.01, "Wrong Gross AEP Test 2");

            thisVal = thisTurb.CalcYearlyValue(2008, "Net AEP", false, wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 3819.539, 0.01, "Wrong Net AEP Test 3");

            thisVal = thisTurb.CalcYearlyValue(2010, "Net AEP", false, wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 3495.726, 0.01, "Wrong Net AEP Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcLT_MonthlyValue_Test()
        {
            Continuum thisInst = new Continuum();
            
            string fileName = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            Wake_Model wakeModel = thisInst.wakeModelList.wakeModels[0];

            // Test 1 Waked WS
            double thisVal = thisTurb.CalcLT_MonthlyValue("Avg WS", 1, false, wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 6.372955, 0.01, "Wrong Avg WS Test 1");

            // Test 2 Freestream WS
            thisVal = thisTurb.CalcLT_MonthlyValue("Avg WS", 4, false, null, new TurbineCollection.PowerCurve());
            Assert.AreEqual(thisVal, 6.716374, 0.01, "Wrong Avg WS Test 2");

            // Test 3
            thisVal = thisTurb.CalcLT_MonthlyValue("Gross AEP", 3, false, wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 387.3117, 0.1, "Wrong Gross Energy Test 3");

            // Test 4
            thisVal = thisTurb.CalcLT_MonthlyValue("Net AEP", 12, false, wakeModel, wakeModel.powerCurve);
            Assert.AreEqual(thisVal, 381.2883, 0.1, "Wrong Net Energy Test 4");


            thisInst.Close();
        }

        [TestMethod]
        public void CalcWakedStDev_Test()
        {
            Continuum thisInst = new Continuum();
            
            string fileName = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(fileName);

            Turbine turbine = new Turbine();
            turbine.UTMX = 704907;
            turbine.UTMY = 4532165;
            double utmX = 705207;
            double utmY = 4531965;

            double thisWS = 6;
            double thisP90SD = 0.2;

            double thisWakeP90SD = turbine.CalcWakedStDev(utmX, utmY, thisInst.turbineList.powerCurves[0], thisP90SD,
                thisWS, thisInst);

            Assert.AreEqual(thisWakeP90SD, 1.169216, 0.001, "Wrong waked SD for effective TI");

            thisInst.Close();

        }

        [TestMethod]
        public void CalcEffectiveTI_Test()
        {
            Continuum thisInst = new Continuum();
            
            string fileName = testingFolder + "\\Turbine testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[1];
            Met thisMet = thisInst.metList.metItem[0];

            // Test 1 
            double[] effectiveTI = thisTurb.CalcEffectiveTI(thisMet, 1.0, thisInst, thisInst.turbineList.powerCurves[0], 4);
            Assert.AreEqual(effectiveTI[7], 0.232564, 0.001, "Wrong Effective TI Test 1");

            // Test 2 
            effectiveTI = thisTurb.CalcEffectiveTI(thisMet, 1.0, thisInst, thisInst.turbineList.powerCurves[0], 13);
            Assert.AreEqual(effectiveTI[12], 0.192811, 0.001, "Wrong Effective TI Test 2");

            // Test 3 
            effectiveTI = thisTurb.CalcEffectiveTI(thisMet, 1.0, thisInst, thisInst.turbineList.powerCurves[0], 16);
            Assert.AreEqual(effectiveTI[5], 0.191666, 0.001, "Wrong Effective TI Test 3");

            // Test 4
            effectiveTI = thisTurb.CalcEffectiveTI(thisMet, 1.0, thisInst, thisInst.turbineList.powerCurves[0], 16);
            Assert.AreEqual(effectiveTI[9], 0.166623, 0.001, "Wrong Effective TI Test 4");

            // Test 5
            effectiveTI = thisTurb.CalcEffectiveTI(thisMet, 10.0, thisInst, thisInst.turbineList.powerCurves[0], 5);
            Assert.AreEqual(effectiveTI[6], 0.216989, 0.001, "Wrong Effective TI Test 5");

            // Test 6
            effectiveTI = thisTurb.CalcEffectiveTI(thisMet, 10.0, thisInst, thisInst.turbineList.powerCurves[0], 12);
            Assert.AreEqual(effectiveTI[4], 0.285484, 0.001, "Wrong Effective TI Test 6");

            thisInst.Close();
        }
    }
}
