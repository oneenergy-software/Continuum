using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Threading;
using System.IO;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Continuum_Tests
{
    [TestClass]
    public class Continuum_Test
    {

        /*  Loss_factors class not used in Continuum 3    [TestMethod]
            public void Get_Total_Losses_Test()
            {
                Loss_factors loss_Factors = new Loss_factors();
                loss_Factors.Set_Defaults();
                double totalLoss = loss_Factors.Get_Total_Losses();

                Assert.AreEqual(0.10526, totalLoss, 0.0001, "Wrong total losses: Test 1");

                loss_Factors.Icing_Loss = 0.03;
                totalLoss = loss_Factors.Get_Total_Losses();
                Assert.AreEqual(0.132102, totalLoss, 0.0001, "Wrong total losses: Test 2");

                loss_Factors.HighLowTemp_Loss = 0.02;
                totalLoss = loss_Factors.Get_Total_Losses();
                Assert.AreEqual(0.14946, totalLoss, 0.0001, "Wrong total losses: Test 3");

                loss_Factors.Wake_Sect_Curtail_Loss = 0.015;
                totalLoss = loss_Factors.Get_Total_Losses();
                Assert.AreEqual(0.162218, totalLoss, 0.0001, "Wrong total losses: Test 4");

                loss_Factors.Environ_Curtail_Loss = 0.04;
                totalLoss = loss_Factors.Get_Total_Losses();
                Assert.AreEqual(0.19573, totalLoss, 0.0001, "Wrong total losses: Test 5");

                loss_Factors.Grid_Curtail_Loss = 0.01;
                totalLoss = loss_Factors.Get_Total_Losses();
                Assert.AreEqual(0.203772, totalLoss, 0.0001, "Wrong total losses: Test 6");

            }
            */


        /*     [TestMethod] Not used in Continuum 3. Uncertainty now based on Round Robin results
             public void FindModelBounds_Test()
             {
                 Continuum thisInst = new Continuum();
                 
                 string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\GetErrorEst\\Testing Error Estimate.cfm";
                 thisInst.Open(Filename);

                 // Test 1: WD_ind = 0 Radius = 4000
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].min, 7.05, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].max, 7.05, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].metMinP10.name, "Met_2903", "Wrong Min Met SpeedUp Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 0");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].min, 0.47, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].max, 7.05, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].metMinP10.name, "Met_2904", "Wrong Min Met Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 0");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].min, 0.47, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].max, 19.99, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[0].metMaxP10.name, "Met_2907", "Wrong Max Met Uphill Flow WD = 0");

                 // Test 2: WD_ind = 2 Radius = 4000
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].min, -999, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].max, -999, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].metMinP10, null, "Wrong Min Met SpeedUp Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[2].metMaxP10, null, "Wrong Max Met SpeedUp Flow WD = 2");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].min, 8.08, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].max, 8.08, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].metMinP10.name, "Met_2903", "Wrong Min Met Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[2].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 2");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].min, 7.04, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].max, 9.29, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[2].metMaxP10.name, "Met_2907", "Wrong Max Met Uphill Flow WD = 2");

                 // Test 3: WD_ind = 7 Radius = 4000
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].min, 2.51, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].max, 6.15, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].metMinP10.name, "Met_2904", "Wrong Min Met SpeedUp Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[7].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 7");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].min, 6.15, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].max, 12.83, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].metMinP10.name, "Met_2903", "Wrong Min Met Downhill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[7].metMaxP10.name, "Met_2907", "Wrong Max Met Downhill Flow WD = 7");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].min, 2.51, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].max, 8.18, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 7");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[7].metMaxP10.name, "Met_2905", "Wrong Max Met Uphill Flow WD = 7");

                 // Test 4: WD_ind = 13 Radius = 4000
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].min, 10.51, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].max, 10.51, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].metMinP10.name, "Met_2903", "Wrong Min Met SpeedUp Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].spdUpBounds[13].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 13");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].min, 3.67, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].max, 10.51, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].metMinP10.name, "Met_2907", "Wrong Min Met Downhill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].downhillBounds[13].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 13");

                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].min, -999, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].max, -999, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].metMinP10, null, "Wrong Min Met Uphill Flow WD = 13");
                 Assert.AreEqual(thisInst.modelList.models[1, 0].uphillBounds[13].metMaxP10, null, "Wrong Max Met Uphill Flow WD = 13");

                 // Test 5: WD_ind = 0 Radius = 8000
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].min, 9.96, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].max, 9.96, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].metMinP10.name, "Met_2903", "Wrong Min Met SpeedUp Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met SpeedUp Flow WD = 0");

                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].min, 2.23, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].max, 9.96, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].metMinP10.name, "Met_2905", "Wrong Min Met Downhill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[0].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 0");

                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].min, 8.09, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].max, 32.52, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].metMinP10.name, "Met_2904", "Wrong Min Met Uphill Flow WD = 0");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[0].metMaxP10.name, "Met_2907", "Wrong Max Met Uphill Flow WD = 0");

                 // Test 6: WD_ind = 2 Radius = 8000
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].min, 7.15, 0.01, "Wrong Min P10 Expo Speedup Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].max, 10.61, 0.01, "Wrong Max P10 Expo Speedup Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].metMinP10.name, "Met_2905", "Wrong Min Met SpeedUp Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].spdUpBounds[2].metMaxP10.name, "Met_2907", "Wrong Max Met SpeedUp Flow WD = 2");

                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].min, 13.56, 0.01, "Wrong Min P10 Expo Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].max, 13.56, 0.01, "Wrong Max P10 Expo Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].metMinP10.name, "Met_2903", "Wrong Min Met Downhill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].downhillBounds[2].metMaxP10.name, "Met_2903", "Wrong Max Met Downhill Flow WD = 2");

                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].min, 7.15, 0.01, "Wrong Min P10 Expo Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].max, 13.56, 0.01, "Wrong Max P10 Expo Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].metMinP10.name, "Met_2905", "Wrong Min Met Uphill Flow WD = 2");
                 Assert.AreEqual(thisInst.modelList.models[1, 2].uphillBounds[2].metMaxP10.name, "Met_2903", "Wrong Max Met Uphill Flow WD = 2");

                 thisInst.Close();
             }
     */


        /*       [TestMethod] Not used in Continuum 3. Uncertainty now based on Round Robin results
               public void GetUncertaintyEstimate_Test()
               {
                   Continuum thisInst = new Continuum();
                   
                   string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\GetErrorEst\\Testing Error Estimate.cfm";
                   thisInst.Open(Filename);

                   Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), 4000, 10000, true, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
                   double thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 5, 5, 0.5, 10, 0, Met.TOD.All, Met.Season.All, 80);
                   Assert.AreEqual(thisUncert, 0.05171, 0.01, "Wrong uncertainty Test 1");

                   thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 5, 5, 3, 10, 0, Met.TOD.All, Met.Season.All, 80);
                   Assert.AreEqual(thisUncert, 0.05171, 0.01, "Wrong uncertainty Test 2");

                   thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 12, 12, -5, 10, 0, Met.TOD.All, Met.Season.All, 80);
                   Assert.AreEqual(thisUncert, 0.007371, 0.01, "Wrong uncertainty Test 3");

                   thisUncert = theseModels[0].GetUncertaintyEstimate(thisInst, 0.1, 0.1, 10, 10, 0, Met.TOD.All, Met.Season.All, 80);
                   Assert.AreEqual(thisUncert, 0.056710, 0.01, "Wrong uncertainty Test 4");

                   thisInst.Close();
               }
       */
        /*       [TestMethod] Not used in Continuum 3. Uncertainty now based on Round Robin results
               public void GetErrorEst_Test()
               {
                   Continuum thisInst = new Continuum();
                   
                   string Filename = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\GetErrorEst\\Testing Error Estimate.cfm";
                   thisInst.Open(Filename);

                   Model[] models = new Model[thisInst.radiiList.ThisCount];
                   Turbine thisTurb = thisInst.turbineList.turbineEsts[0];

                   for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
                       models[i] = thisInst.modelList.models[1, i];

                   double[] theseWgts = thisInst.modelList.GetModelWeights(models);

                   int numWD = thisInst.GetNumWD();
                   double[,] thisUncert = new double[4, 16];

                   for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
                   {
                       for (int j = 0; j < numWD; j++)
                       {
                           thisUncert[i, j] = models[i].GetUncertaintyEstimate(thisInst, thisTurb.gridStats.stats[i].P10_UW[j], thisTurb.gridStats.stats[i].P10_DW[j],
                               thisTurb.expo[i].expo[j], thisTurb.expo[i].GetDW_Param(j, "Expo"), j, Met.TOD.All, Met.Season.All, 80);
                       }
                   }

                   double[] windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), thisTurb.UTMX, thisTurb.UTMY, Met.TOD.All, Met.Season.All, 80);
                   double Uncert = thisTurb.GetErrorEst(thisInst, windRose);

                   Assert.AreEqual(Uncert, 0.042022, 0.01, "Wrong overall uncertainty");

                   thisInst.Close();
               }
       */
        
        [TestMethod]
        public void ModelIceThrow_Test()
        {
            // Outputs ice throw results to .csv file
            SiteSuitability siteSuitability = new SiteSuitability();

            string fileName = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Site Suitability\\Ice Throw\\Test1.csv";

            // Test 1
            double degs = 180;
            double hubHeigt = 80;
            double elevDiff = 0;
            double randRad = 44.5;
            double iceSpeed = 20;
            double cD = 1.1;
            double iceArea = 0.02;
            double iceMass = 1.13;
            double thisWS = 6;
            double thisWD = 0; // North
            Turbine thisTurb = new Turbine();

         //   siteSuitability.ModelIceThrow(degs, hubHeigt, elevDiff, randRad, iceSpeed, cD, iceArea, iceMass, thisWS, thisWD, thisTurb, fileName);

            // Test 2
            fileName = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Site Suitability\\Ice Throw\\Test2.csv";
            degs = 360; // Blade pointing straight up           
         //   siteSuitability.ModelIceThrow(degs, hubHeigt, elevDiff, randRad, iceSpeed, cD, iceArea, iceMass, thisWS, thisWD, thisTurb, fileName);

            // Test 3
            fileName = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Site Suitability\\Ice Throw\\Test3.csv";
            degs = 270; // Blade pointing straight out           
         //   siteSuitability.ModelIceThrow(degs, hubHeigt, elevDiff, randRad, iceSpeed, cD, iceArea, iceMass, thisWS, thisWD, thisTurb, fileName);



        }

        

        

        

        
    }
}
