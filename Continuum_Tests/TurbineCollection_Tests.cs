using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class TurbineCollection_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;

        public TurbineCollection_Tests()
        {
            testingFolder = globals.testingFolder + "TurbineCollection";
        }

        [TestMethod]
        public void ExportWSDist()
        {
            Continuum thisInst = new Continuum("", false);
            

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
            Continuum thisInst = new Continuum("", false);
            

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

 /*       [TestMethod] Function no longer used
        public void GetOverallWakeLoss_Test()
        {
            Continuum thisInst = new Continuum("", false);
            
            string Filename =  testingFolder + "\\Bobcat Bluff TurbineCollection testing.cfm";
            thisInst.Open(Filename);

            double This_Overall_Wake = thisInst.turbineList.GetOverallWakeLoss(thisInst.wakeModelList.wakeModels[0], 16);
            Assert.AreEqual(This_Overall_Wake, 0.0306, 0.001, "Wrong overall wake loss");

            thisInst.Close();
        }
        */
        [TestMethod]
        public void CalcTurbineExposures_Test()
        {
            Continuum thisInst = new Continuum("", false);
            
            string Filename = testingFolder + "\\Bobcat Bluff TurbineCollection testing.cfm";
            thisInst.Open(Filename);                      

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
            Continuum thisInst = new Continuum("", false);
            

            string Filename = testingFolder + "\\Bobcat Bluff TurbineCollection testing.cfm";
            thisInst.Open(Filename);
                       
            
            for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
            {
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[i].grossAEP, null, "Didn't calculate gross AEP");
                Assert.AreEqual(thisInst.turbineList.turbineEsts[i].grossAEP.Length, 1, "Didn't calculate gross AEP");
                Assert.AreNotEqual(thisInst.turbineList.turbineEsts[i].grossAEP[0].CF, 0, 0, "Didn't calculate gross AEP");
                Assert.AreNotEqual(thisInst.turbineList.turbineEsts[i].grossAEP[0].AEP, 0, 0, "Didn't calculate gross AEP");
                Assert.AreNotEqual(thisInst.turbineList.turbineEsts[i].grossAEP[0].P90, 0, 0, "Didn't calculate gross AEP");
                Assert.AreNotEqual(thisInst.turbineList.turbineEsts[i].grossAEP[0].P99, 0, 0, "Didn't calculate gross AEP");
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[i].grossAEP[0].powerCurve, 0, "Didn't calculate gross AEP");
                Assert.AreNotSame(thisInst.turbineList.turbineEsts[i].grossAEP[0].sectorEnergy, null, "Didn't calculate gross AEP");
            }

            thisInst.Close();
        }

        [TestMethod]
        public void ReCalcTurbine_SRDH_Test()
        {
            // Tests changing LC key after a met has been imported and turbine estimates have been created
            
            string LC_Change_Before = "Test_Change_LCKey_Before_Mets_and_Turbines.cfm";
            string LC_Change_After = "Test_Change_LCKey_After_Mets_and_Turbines.cfm";

            Continuum Change_Before = new Continuum("", false);
            Change_Before.Open(testingFolder + "\\Node_ReCalc\\" + LC_Change_Before);

            Continuum Change_After = new Continuum("", false);
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
            Continuum thisInst = new Continuum("", false);
            
            string fileName = testingFolder + "\\CalcProbOfWakeForEffectiveTI\\Turbine Marion TI testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[1];
            double[,] probWakes = thisInst.turbineList.CalcProbOfWakeForEffectiveTI(thisInst, thisTurb.UTMX, thisTurb.UTMY, thisInst.turbineList.powerCurves[0]);

            Assert.AreEqual(probWakes[0, 12], 0.426, 0.01, "Wrong Wake Prob Test 1");
            Assert.AreEqual(probWakes[0, 13], 0.288, 0.01, "Wrong Wake Prob Test 2");
            Assert.AreEqual(probWakes[2, 4], 0.449, 0.01, "Wrong Wake Prob Test 3");
            Assert.AreEqual(probWakes[2, 5], 0.276, 0.01, "Wrong Wake Prob Test 4");

            thisInst.Close();
            thisInst = new Continuum("", false);
            fileName = testingFolder + "\\CalcProbOfWakeForEffectiveTI\\Turbine Findlay TI testing.cfm";
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
            Continuum thisInst = new Continuum("", false);
            
            string fileName = testingFolder + "\\Bobcat Bluff TurbineCollection testing.cfm";
            thisInst.Open(fileName);

            string powerStr = testingFolder + "\\GetInterpPowerOrThrust\\Power by 0.1 ms.csv";
            StreamReader srPower = new StreamReader(powerStr);
            string thrustStr = testingFolder + "\\GetInterpPowerOrThrust\\Thrust by 0.1 ms.csv";
            StreamReader srThrust = new StreamReader(thrustStr);

            for (double i = 3; i < 13.5; i = i + 0.1)
            {
                double power = thisInst.turbineList.GetInterpPowerOrThrust(i, thisInst.turbineList.powerCurves[0], "Power");
                double testP = Convert.ToDouble(srPower.ReadLine());
                Assert.AreEqual(power, testP, 0.1, "Wrong power test " + i.ToString());               
            }

            for (double i = 3; i <= 25.0; i = i + 0.1)
            {              
                double thrust = thisInst.turbineList.GetInterpPowerOrThrust(i, thisInst.turbineList.powerCurves[0], "Thrust");
                double testT = Convert.ToDouble(srThrust.ReadLine());
                Assert.AreEqual(thrust, testT, 0.1, "Wrong thrust test " + i.ToString());
            }

            srPower.Close();
            srThrust.Close();

            thisInst.Close();
        }

        [TestMethod]
        public void CalcAvgWS_ByMonthInd_Test()
        {
            Continuum thisInst = new Continuum("", false);

            string fileName = testingFolder + "\\Time Series tests\\TS testing.cfm";
            thisInst.isTest = true;
            thisInst.Open(fileName, false);

            // Test 1: Jan 2005
            double avgWS = thisInst.turbineList.CalcAvgWS_ByMonthInd(0);
            Assert.AreEqual(avgWS, 6.692658602, 0.0001, "Wrong avg WS for month Ind 0");

            // Test 2: Dec 2007
            avgWS = thisInst.turbineList.CalcAvgWS_ByMonthInd(35);
            Assert.AreEqual(avgWS, 7.014716398, 0.0001, "Wrong avg WS for month Ind 35");

            // Test 3: Jun 2020
            avgWS = thisInst.turbineList.CalcAvgWS_ByMonthInd(185);
            Assert.AreEqual(avgWS, 5.804454167, 0.0001, "Wrong avg WS for month Ind 185");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcAvgWS_ByMonthYear_Test()
        {
            Continuum thisInst = new Continuum("", false);

            string fileName = testingFolder + "\\Time Series tests\\TS testing.cfm";
            thisInst.isTest = true;
            thisInst.Open(fileName, false);

            // Test 1: Jan 2005
            double avgWS = thisInst.turbineList.CalcAvgWS_ByMonthYear(1, 2005, "Free-stream");
            Assert.AreEqual(avgWS, 6.692658602, 0.0001, "Wrong avg WS for Jan 2005");

            // Test 2: Dec 2007
            avgWS = thisInst.turbineList.CalcAvgWS_ByMonthYear(12, 2007, "Free-stream");
            Assert.AreEqual(avgWS, 7.014716398, 0.0001, "Wrong avg WS for Dec 2007");

            // Test 3: Jun 2020
            avgWS = thisInst.turbineList.CalcAvgWS_ByMonthYear(6, 2020, "Free-stream");
            Assert.AreEqual(avgWS, 5.804454167, 0.0001, "Wrong avg WS for Jun 2020");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcWindFarmHourlyValues_Test()
        {
            Continuum thisInst = new Continuum("", false);

            string fileName = testingFolder + "\\Time Series tests\\TS testing.cfm";
            thisInst.isTest = true;
            thisInst.Open(fileName, false);
            Wake_Model thisWakeModel = thisInst.wakeModelList.wakeModels[0];

            // Test 1: Jan 2, 2005 1:00
            ModelCollection.TimeSeries thisTS = thisInst.turbineList.CalcWindFarmHourlyValues(25, thisWakeModel);
            Assert.AreEqual(thisTS.freeStreamWS, 7.1076, 0.001, "Wrong avg WS for Index 25");
            Assert.AreEqual(thisTS.grossEnergy, 5795.60, 1, "Wrong gross AEP for Index 25");
            Assert.AreEqual(thisTS.netEnergy, 4628.70, 1, "Wrong net AEP for Index 25");

            // Test 2: July 25, 2017 11:00
            thisTS = thisInst.turbineList.CalcWindFarmHourlyValues(110123, thisWakeModel);
            Assert.AreEqual(thisTS.freeStreamWS, 3.5476, 0.001, "Wrong avg WS for Index 25");
            Assert.AreEqual(thisTS.grossEnergy, 596.2, 1, "Wrong gross AEP for Index 25");
            Assert.AreEqual(thisTS.netEnergy, 527.2, 1, "Wrong net AEP for Index 25");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcGrossEnergy_ByMonthInd_Test()
        {
            Continuum thisInst = new Continuum("", false);

            string fileName = testingFolder + "\\Time Series tests\\TS testing.cfm";
            thisInst.isTest = true;
            thisInst.Open(fileName, false);
            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0];

            // Test 1: Jan 2005
            double grossAEP = thisInst.turbineList.CalcGrossEnergy_ByMonthInd(powerCurve, 0);
            Assert.AreEqual(grossAEP, 4396.3598, 1, "Wrong gross energy for month Ind 0");

            // Test 2: Dec 2007
            grossAEP = thisInst.turbineList.CalcGrossEnergy_ByMonthInd(powerCurve, 35);
            Assert.AreEqual(grossAEP, 4639.4766, 1, "Wrong gross energy for month Ind 35");

            // Test 3: Jun 2020
            grossAEP = thisInst.turbineList.CalcGrossEnergy_ByMonthInd(powerCurve, 185);
            Assert.AreEqual(grossAEP, 3105.1379, 1, "Wrong gross energy for month Ind 185");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcNetEnergy_ByMonthInd_Test()
        {
            Continuum thisInst = new Continuum("", false);

            string fileName = testingFolder + "\\Time Series tests\\TS testing.cfm";
            thisInst.isTest = true;
            thisInst.Open(fileName, false);
            Wake_Model thisWakeModel = thisInst.wakeModelList.wakeModels[0];

            // Test 1: Jan 2005
            double netAEP = thisInst.turbineList.CalcNetEnergy_ByMonthInd(0, thisWakeModel);
            Assert.AreEqual(netAEP, 3731.5013, 1, "Wrong net energy for month Ind 0");

            // Test 2: Dec 2007
            netAEP = thisInst.turbineList.CalcNetEnergy_ByMonthInd(35, thisWakeModel);
            Assert.AreEqual(netAEP, 4012.2173, 1, "Wrong net energy for month Ind 35");

            // Test 3: Jun 2020
            netAEP = thisInst.turbineList.CalcNetEnergy_ByMonthInd(185, thisWakeModel);
            Assert.AreEqual(netAEP, 2702.3186, 1, "Wrong net energy for month Ind 185");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcNetEnergy_ByMonthYear_Test()
        {
            Continuum thisInst = new Continuum("", false);

            string fileName = testingFolder + "\\Time Series tests\\TS testing.cfm";
            thisInst.isTest = true;
            thisInst.Open(fileName, false);
            Wake_Model thisWakeModel = thisInst.wakeModelList.wakeModels[0];

            // Test 1: Jan 2005
            double netEnergy = thisInst.turbineList.CalcNetEnergy_ByMonthYear(1, 2005, thisWakeModel);
            Assert.AreEqual(netEnergy, 3731.5013, 1, "Wrong avg WS for Jan 2005");

            // Test 2: Dec 2007
            netEnergy = thisInst.turbineList.CalcNetEnergy_ByMonthYear(12, 2007, thisWakeModel);
            Assert.AreEqual(netEnergy, 4012.2173, 1, "Wrong avg WS for Dec 2007");

            // Test 3: Jun 2020
            netEnergy = thisInst.turbineList.CalcNetEnergy_ByMonthYear(6, 2020, thisWakeModel);
            Assert.AreEqual(netEnergy, 2702.3186, 1, "Wrong avg WS for Jun 2020");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcGrossEnergy_ByMonthYear_Test()
        {
            Continuum thisInst = new Continuum("", false);

            string fileName = testingFolder + "\\Time Series tests\\TS testing.cfm";
            thisInst.isTest = true;
            thisInst.Open(fileName, false);
            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0];

            // Test 1: Jan 2005
            double netEnergy = thisInst.turbineList.CalcGrossEnergy_ByMonthYear(1, 2005, powerCurve);
            Assert.AreEqual(netEnergy, 4396.3598, 1, "Wrong gross energy for Jan 2005");

            // Test 2: Dec 2007
            netEnergy = thisInst.turbineList.CalcGrossEnergy_ByMonthYear(12, 2007, powerCurve);
            Assert.AreEqual(netEnergy, 4639.4766, 1, "Wrong gross energy for Dec 2007");

            // Test 3: Jun 2020
            netEnergy = thisInst.turbineList.CalcGrossEnergy_ByMonthYear(6, 2020, powerCurve);
            Assert.AreEqual(netEnergy, 3105.1379, 1, "Wrong gross energy for Jun 2020");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcLT_MonthlyValue_Test()
        {
            Continuum thisInst = new Continuum("", false);

            string fileName = testingFolder + "\\Time Series tests\\TS testing.cfm";
            thisInst.isTest = true;
            thisInst.Open(fileName, false);
            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0];
            Wake_Model thisWakeModel = thisInst.wakeModelList.wakeModels[0];

            // Test 1: Jan - Avg WS
            double avgWS = thisInst.turbineList.CalcLT_MonthlyValue("Avg WS", 1, null, powerCurve);
            Assert.AreEqual(avgWS, 7.533650403, 0.0001, "Wrong LT WS for Jan");

            // Test 2: Dec - Gross AEP
            double grossEnergy = thisInst.turbineList.CalcLT_MonthlyValue("Gross AEP", 12, null, powerCurve);
            Assert.AreEqual(grossEnergy, 5208.67583, 1, "Wrong LT Gross for Dec");

            // Test 3: Aug - Net AEP
            double netEnergy = thisInst.turbineList.CalcLT_MonthlyValue("Net AEP", 8, thisWakeModel, powerCurve);
            Assert.AreEqual(netEnergy, 1680.958395, 1, "Wrong LT Net for Aug");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcYearlyValue_Test()
        {
            Continuum thisInst = new Continuum("", false);

            string fileName = testingFolder + "\\Time Series tests\\TS testing.cfm";
            thisInst.isTest = true;
            thisInst.Open(fileName, false);
            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0];
            Wake_Model thisWakeModel = thisInst.wakeModelList.wakeModels[0];

            // Test 1: 2011 - Avg WS
            double avgWS = thisInst.turbineList.CalcYearlyValue("Avg WS", 2011, null, powerCurve);
            Assert.AreEqual(avgWS, 6.572843265, 0.0001, "Wrong LT WS for Jan");

            // Test 2: 2024 - Gross AEP
            double grossEnergy = thisInst.turbineList.CalcYearlyValue("Gross AEP", 2024, thisWakeModel, powerCurve);
            Assert.AreEqual(grossEnergy, 48176.2139, 1, "Wrong LT Gross for Dec");

            // Test 3: 2005 - Net AEP
            double netEnergy = thisInst.turbineList.CalcYearlyValue("Net AEP", 2005, thisWakeModel, powerCurve);
            Assert.AreEqual(netEnergy, 38401.7291, 1, "Wrong LT Net for Aug");

            thisInst.Close();
        }

    }
}
