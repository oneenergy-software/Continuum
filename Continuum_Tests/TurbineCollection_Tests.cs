using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class TurbineCollection_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\TurbineCollection";

        [TestMethod]
        public void ExportWSDist()
        {
            Continuum thisInst = new Continuum();
            

            string Filename = testingFolder + "\\Bobcat Bluff TurbineCollection testing.cfm";
            thisInst.Open(Filename);

            string distFile = testingFolder + "\\CalcAndReturnGrossAEP\\Met 2001 WS Dist.csv";
            StreamWriter sw = new StreamWriter(distFile);

            Met.WSWD_Dist thisDist = thisInst.metList.metItem[0].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

            for (int i = 0; i < thisDist.WS_Dist.Length; i++)
                sw.WriteLine(Math.Round(thisDist.WS_Dist[i], 4));

            sw.Close();
            thisInst.Close();
        }


        [TestMethod]
        public void CalcAndReturnGrossAEP_Test()
        {
            Continuum thisInst = new Continuum();
            

            string Filename = testingFolder + "\\Bobcat Bluff TurbineCollection testing.cfm";
            thisInst.Open(Filename);

            string distFile = testingFolder + "\\CalcAndReturnGrossAEP\\Met 2001 WS Dist.csv";
            StreamReader sr = new StreamReader(distFile);
            double[] thisDist = new double[31];

            for (int i = 0; i <= 30; i++)
                thisDist[i] = Convert.ToSingle(sr.ReadLine());

            double thisAEP = thisInst.turbineList.CalcAndReturnGrossAEP(thisDist, thisInst.metList, thisInst.turbineList.powerCurves[0].name);

            Assert.AreEqual(6388.618, thisAEP, 0.1, "Wrong gross AEP");

            sr.Close();
            thisInst.Close();
        }

        [TestMethod]
        public void CalcCapacityFactor_Test()
        {
            TurbineCollection turbineList = new TurbineCollection();

            double thisAEP = 4569;
            double ratedP = 1500;
            double thisCF = turbineList.CalcCapacityFactor(thisAEP, ratedP);
            Assert.AreEqual(0.3477, thisCF, 0.01, "Wrong capaciy factor Test 1");

            thisAEP = 8922;
            ratedP = 2300;
            thisCF = turbineList.CalcCapacityFactor(thisAEP, ratedP);
            Assert.AreEqual(0.4428, thisCF, 0.01, "Wrong capaciy factor Test 2");

        }

        [TestMethod]
        public void GetOverallWakeLoss_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename =  testingFolder + "\\Bobcat Bluff TurbineCollection testing.cfm";
            thisInst.Open(Filename);

            double This_Overall_Wake = thisInst.turbineList.GetOverallWakeLoss(thisInst.wakeModelList.wakeModels[0], 16, false);
            Assert.AreEqual(This_Overall_Wake, 0.07819, 0.01, "Wrong overall wake loss");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcTurbineExposures_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\Bobcat Bluff TurbineCollection testing.cfm";
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
        public void CalcGrossAEPFromTABs_Test()
        {
            Continuum thisInst = new Continuum();
            

            string Filename = testingFolder + "\\Bobcat Bluff TurbineCollection testing.cfm";
            thisInst.Open(Filename);

            thisInst.turbineList.AddTurbine("Test1", 347212, 5307579, 1);
            thisInst.turbineList.AddTurbine("Test2", 346306, 5311256, 1);
            thisInst.turbineList.AddTurbine("Test3", 342100, 5314708, 1);

            double[] power = new double[31];
            string Power_file = testingFolder + "\\CalcGrossAEPFromTABs\\Power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[31];
            string Thrust_file = testingFolder + "\\CalcGrossAEPFromTABs\\Thrust.txt";
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
                thisInst.turbineList.turbineEsts[i].GenerateAvgWSFromTABs(thisInst, UWDW_Mods, windRose, false);

                UWDW_Mods = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(), true,
                    Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
                thisInst.turbineList.turbineEsts[i].DoTurbineCalcs(thisInst, UWDW_Mods);
                thisInst.turbineList.turbineEsts[i].GenerateAvgWSFromTABs(thisInst, UWDW_Mods, windRose, false);
            }

            thisInst.turbineList.CalcGrossAEPFromTABs(thisInst, true);
            thisInst.turbineList.CalcGrossAEPFromTABs(thisInst, false);

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
        public void ReCalcTurbine_SRDH_Test()
        {
            // Tests changing LC key after a met has been imported and turbine estimates have been created
            
            string LC_Change_Before = "Test_Change_LCKey_Before_Mets_and_Turbs.cfm";
            string LC_Change_After = "Test_Change_LCKey_After_Mets_and_Turbs.cfm";

            Continuum Change_Before = new Continuum();
            Change_Before.Open(testingFolder + "\\Node_ReCalc\\" + LC_Change_Before);

            Continuum Change_After = new Continuum();
            Change_After.Open(testingFolder + "\\Node_ReCalc\\" + LC_Change_After);

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
        public void CalcProbOfWakeForEffectiveTI_Test()
        {
            Continuum thisInst = new Continuum();
            
            string fileName = testingFolder + "\\CalcProbOfWakeForEffectiveTI\\Turbine TI testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[1];
            double[,] probWakes = thisInst.turbineList.CalcProbOfWakeForEffectiveTI(thisInst, thisTurb.UTMX, thisTurb.UTMY, thisInst.turbineList.powerCurves[0]);

            Assert.AreEqual(probWakes[0, 12], 0.60, 0.01, "Wrong Wake Prob Test 1");
            Assert.AreEqual(probWakes[0, 13], 0.114, 0.01, "Wrong Wake Prob Test 2");
            Assert.AreEqual(probWakes[2, 4], 0.62235, 0.01, "Wrong Wake Prob Test 3");
            Assert.AreEqual(probWakes[2, 5], 0.101706, 0.01, "Wrong Wake Prob Test 4");

            thisInst.Close();
            thisInst = new Continuum();
            fileName = testingFolder + "\\CalcProbOfWakeForEffectiveTI\\Turbine testing Findlay turbs.cfm";
            thisInst.Open(fileName);
            thisTurb = thisInst.turbineList.turbineEsts[4];
            probWakes = thisInst.turbineList.CalcProbOfWakeForEffectiveTI(thisInst, thisTurb.UTMX, thisTurb.UTMY, thisInst.turbineList.powerCurves[0]);

            Assert.AreEqual(probWakes[5, 7], 0.46, 0.01, "Wrong Wake Prob Test 5");
            Assert.AreEqual(probWakes[1, 15], 0.22, 0.01, "Wrong Wake Prob Test 6");
            Assert.AreEqual(probWakes[3, 14], 0.82, 0.01, "Wrong Wake Prob Test 7");

            thisInst.Close();
        }

        [TestMethod]
        public void GetInterpPowerOrThrust_Test()
        {
            Continuum thisInst = new Continuum();
            
            string fileName = testingFolder + "Bobcat Bluff TurbineCollection testing.cfm";
            thisInst.Open(fileName);

            // Test 1
            double power = thisInst.turbineList.GetInterpPowerOrThrust(3.2, thisInst.turbineList.powerCurves[0], "Power");
            Assert.AreEqual(power, 37.24, 0.1, "Wrong power test 1");

            // Test 2
            power = thisInst.turbineList.GetInterpPowerOrThrust(6.9, thisInst.turbineList.powerCurves[0], "Power");
            Assert.AreEqual(power, 529.14, 0.1, "Wrong power test 2");

            // Test 3
            power = thisInst.turbineList.GetInterpPowerOrThrust(8.3, thisInst.turbineList.powerCurves[0], "Power");
            Assert.AreEqual(power, 921.94, 0.1, "Wrong power test 3");

            // Test 4
            power = thisInst.turbineList.GetInterpPowerOrThrust(12.1, thisInst.turbineList.powerCurves[0], "Power");
            Assert.AreEqual(power, 1500, 0.1, "Wrong power test 4");

            // Test 5
            power = thisInst.turbineList.GetInterpPowerOrThrust(3.1, thisInst.turbineList.powerCurves[0], "Thrust");
            Assert.AreEqual(power, 0.965598, 0.1, "Wrong power test 5");

            // Test 6
            power = thisInst.turbineList.GetInterpPowerOrThrust(6.4, thisInst.turbineList.powerCurves[0], "Thrust");
            Assert.AreEqual(power, 0.7993, 0.001, "Wrong power test 6");

            // Test 7
            power = thisInst.turbineList.GetInterpPowerOrThrust(13.7, thisInst.turbineList.powerCurves[0], "Thrust");
            Assert.AreEqual(power, 0.202073, 0.001, "Wrong power test 7");

            thisInst.Close();
        }
    }
}
