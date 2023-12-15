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
            Continuum thisInst = new Continuum("");
            

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
            Continuum thisInst = new Continuum("");
            

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
            Continuum thisInst = new Continuum("");
            
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
            Continuum thisInst = new Continuum("");
            
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
            Continuum thisInst = new Continuum("");
            

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

            Continuum Change_Before = new Continuum("");
            Change_Before.Open(testingFolder + "\\Node_ReCalc\\" + LC_Change_Before);

            Continuum Change_After = new Continuum("");
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
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\CalcProbOfWakeForEffectiveTI\\Turbine Marion TI testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[1];
            double[,] probWakes = thisInst.turbineList.CalcProbOfWakeForEffectiveTI(thisInst, thisTurb.UTMX, thisTurb.UTMY, thisInst.turbineList.powerCurves[0]);

            Assert.AreEqual(probWakes[0, 12], 0.426, 0.01, "Wrong Wake Prob Test 1");
            Assert.AreEqual(probWakes[0, 13], 0.288, 0.01, "Wrong Wake Prob Test 2");
            Assert.AreEqual(probWakes[2, 4], 0.449, 0.01, "Wrong Wake Prob Test 3");
            Assert.AreEqual(probWakes[2, 5], 0.276, 0.01, "Wrong Wake Prob Test 4");

            thisInst.Close();
            thisInst = new Continuum("");
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
            Continuum thisInst = new Continuum("");
            
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
    }
}
