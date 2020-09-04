using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class WakeCollection_Tests
    {
        string testingFolder = "C:\\Users\\liz_w\\Dropbox\\Continuum 3 Source code\\Critical Unit Test Docs\\WakeCollection";

        [TestMethod]
        public void CalcWakeProfileFit_Test()
        {
            WakeCollection WakeModList = new WakeCollection();

            // Test 1
            string velDefFile = testingFolder + "\\CalcWakeProfileFit\\Vel Deficit Test 1.csv";
            StreamReader srVelDef = new StreamReader(velDefFile);

            double[] Vel_Def = new double[21];
            double[] R_RD = new double[21];

            for (int i = 0; i < 21; i++)
            {
                string thisData = srVelDef.ReadLine();
                string[] parsedData = thisData.Split(',');
                Vel_Def[i] = Convert.ToSingle(parsedData[0]);
                R_RD[i] = Convert.ToSingle(parsedData[1]);
            }

            srVelDef.Close();
                       
            string calcCoeffs = testingFolder + "\\CalcWakeProfileFit\\Wake Profile Coeffs.csv";
            StreamReader srCoeffs = new StreamReader(calcCoeffs);
            string strCoeffs = srCoeffs.ReadLine();
            string[] strCoeffsParse = strCoeffs.Split(',');

            double[] coeffs = WakeModList.CalcWakeProfileFit(Vel_Def, R_RD);

            for (int i = 0; i < 5; i++)
                Assert.AreEqual(coeffs[i], Convert.ToSingle(strCoeffsParse[i]), 0.001, "Test 1 Wrong Coeff " + i);
            
            // Test 2
            velDefFile = testingFolder + "\\CalcWakeProfileFit\\Vel Deficit Test 2.csv";
            srVelDef = new StreamReader(velDefFile);

            Vel_Def = new double[21];
            R_RD = new double[21];

            for (int i = 0; i < 21; i++)
            {
                string thisData = srVelDef.ReadLine();
                string[] parsedData = thisData.Split(',');
                Vel_Def[i] = Convert.ToSingle(parsedData[0]);
                R_RD[i] = Convert.ToSingle(parsedData[1]);
            }

            srVelDef.Close();

            strCoeffs = srCoeffs.ReadLine();
            strCoeffsParse = strCoeffs.Split(',');

            coeffs = WakeModList.CalcWakeProfileFit(Vel_Def, R_RD);

            for (int i = 0; i < 5; i++)
                Assert.AreEqual(coeffs[i], Convert.ToSingle(strCoeffsParse[i]), 0.001, "Test 2 Wrong Coeff " + i);

            srCoeffs.Close();

        }

        [TestMethod]
        public void FindAllUW_Turbines_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();
            turbineList.AddTurbine("Turb_1", 275000, 4553000, 1);
            turbineList.AddTurbine("Turb_2", 275000, 4553300, 1);
            turbineList.AddTurbine("Turb_3", 275000, 4553600, 1);
            turbineList.AddTurbine("Turb_4", 275000, 4553900, 1);
            turbineList.AddTurbine("Turb_5", 275000, 4554200, 1);
            turbineList.AddTurbine("Turb_6", 283000, 4553000, 1);
            turbineList.AddTurbine("Turb_7", 283000, 4553300, 1);
            turbineList.AddTurbine("Turb_8", 283000, 4553600, 1);
            turbineList.AddTurbine("Turb_9", 283000, 4553900, 1);
            turbineList.AddTurbine("Turb_10", 283000, 4554200, 1);

            Turbine[] UW_Turbs = WakeModList.FindAllUW_Turbines(45, 279000, 4553200, turbineList);
            Assert.AreEqual(UW_Turbs.Length, 5, "Wrong number of UW turbines in Test 1 Sector 45");
            Assert.AreSame(UW_Turbs[0].name, "Turb_10", "Wrong UW turbines in Test 1 Sector 45");
            Assert.AreSame(UW_Turbs[1].name, "Turb_9", "Wrong UW turbines in Test 1 Sector 45");
            Assert.AreSame(UW_Turbs[2].name, "Turb_8", "Wrong UW turbines in Test 1 Sector 45");
            Assert.AreSame(UW_Turbs[3].name, "Turb_7", "Wrong UW turbines in Test 1 Sector 45");
            Assert.AreSame(UW_Turbs[4].name, "Turb_6", "Wrong UW turbines in Test 1 Sector 45");

            UW_Turbs = WakeModList.FindAllUW_Turbines(180, 279000, 4553200, turbineList);
            Assert.AreEqual(UW_Turbs.Length, 2, "Wrong number of UW turbines in Test 2 Sector 180");
            Assert.AreSame(UW_Turbs[0].name, "Turb_1", "Wrong UW turbines in Test 2 Sector 180");
            Assert.AreSame(UW_Turbs[1].name, "Turb_6", "Wrong UW turbines in Test 2 Sector 180");

            UW_Turbs = WakeModList.FindAllUW_Turbines(337.5f, 279000, 4554900, turbineList);
            Assert.AreEqual(UW_Turbs.Length, 4, "Wrong number of UW turbines in Test 3 Sector 337.5");
            Assert.AreSame(UW_Turbs[0].name, "Turb_5", "Wrong UW turbines in Test 3 Sector 180");
            Assert.AreSame(UW_Turbs[1].name, "Turb_4", "Wrong UW turbines in Test 3 Sector 180");
            Assert.AreSame(UW_Turbs[2].name, "Turb_3", "Wrong UW turbines in Test 3 Sector 180");
            Assert.AreSame(UW_Turbs[3].name, "Turb_2", "Wrong UW turbines in Test 3 Sector 180");

            UW_Turbs = WakeModList.FindAllUW_Turbines(225, 275500, 4553200, turbineList);
            Assert.AreEqual(UW_Turbs.Length, 3, "Wrong number of UW turbines in Test 4 Sector 225");
            Assert.AreSame(UW_Turbs[0].name, "Turb_1", "Wrong UW turbines in Test 3 Sector 180");
            Assert.AreSame(UW_Turbs[1].name, "Turb_2", "Wrong UW turbines in Test 3 Sector 180");
            Assert.AreSame(UW_Turbs[2].name, "Turb_3", "Wrong UW turbines in Test 3 Sector 180");
        }

        [TestMethod]
        public void CalcWS_DeficitEddyViscosityGrid_Test()
        {
            WakeCollection wakeModelList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[23];
            string Power_file = testingFolder + "\\CalcWS_DeficitEddyViscosityGrid\\Power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[23];
            string Thrust_file = testingFolder + "\\CalcWS_DeficitEddyViscosityGrid\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87, 16, 10, 1, 0);
            wakeModelList.AddWakeModel(0, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear");
            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();

            double[,] Vel_Def = wakeModelList.CalcWS_DeficitEddyViscosityGrid(2, 30, 0.1f, 0.025f, 8, wakeModelList.wakeModels[0], metList);
            Assert.AreEqual(Vel_Def[16, 2], 0.450777, 0.01, "Wrong Vel Def Test 1 r = 0.05");
            Assert.AreEqual(Vel_Def[16, 10], 0.196172, 0.01, "Wrong Vel Def Test 1 r = 0.25");
            Assert.AreEqual(Vel_Def[16, 17], 0.03166, 0.01, "Wrong Vel Def Test 1 r = 0.425");

            Assert.AreEqual(Vel_Def[40, 0], 0.255068, 0.01, "Wrong Vel Def Test 2 r = 0.05");
            Assert.AreEqual(Vel_Def[40, 7], 0.194434, 0.01, "Wrong Vel Def Test 2 r = 0.25");
            Assert.AreEqual(Vel_Def[40, 18], 0.02328, 0.01, "Wrong Vel Def Test 2 r = 0.425");

            wakeModelList.AddWakeModel(0, 10, 14, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear");
            Vel_Def = wakeModelList.CalcWS_DeficitEddyViscosityGrid(2, 30, 0.1f, 0.025f, 6, wakeModelList.wakeModels[1], metList);
            Assert.AreEqual(Vel_Def[4, 2], 0.513121, 0.01, "Wrong Vel Def Test 3 r = 0.05");
            Assert.AreEqual(Vel_Def[4, 10], 0.20352, 0.01, "Wrong Vel Def Test 3 r = 0.25");
            Assert.AreEqual(Vel_Def[4, 16], 0.04366, 0.01, "Wrong Vel Def Test 3 r = 0.4");

            Assert.AreEqual(Vel_Def[42, 4], 0.18052, 0.01, "Wrong Vel Def Test 4 r = 0.05");
            Assert.AreEqual(Vel_Def[42, 19], 0.01031, 0.01, "Wrong Vel Def Test 4 r = 0.25");

        }

        [TestMethod]
        public void CalcNetEnergy_Test()
        {
            WakeCollection wakeModelList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[31];
            string Power_file = testingFolder + "\\CalcNetEnergy\\Power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, null, 87, 16, 10, 1, 0);
            wakeModelList.AddWakeModel(0, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear");
            
            string Dist_file = testingFolder + "\\CalcNetEnergy\\WS_Dist.txt";
            sr = new StreamReader(Dist_file);
            double[] thisDist = new double[31];

            for (int i = 0; i <= 30; i++)
                thisDist[i] = Convert.ToSingle(sr.ReadLine());

            Continuum thisInst = new Continuum();
            double[] dummyRose = new double[16];
            double[,] dummySect = new double[16, 31];
            thisInst.metList.AddMetTAB("Dummy", 0, 0, 0, dummyRose, dummySect, 0.5, 1, thisInst);
            double loss = 0.8782;
            double This_AEP = wakeModelList.CalcNetEnergy(wakeModelList.wakeModels[0], thisDist, thisInst, loss);
            Assert.AreEqual(This_AEP, 8956.778, 5, "Wrongt Net AEP");

        }

        [TestMethod]
        public void CalcEquivRoughness_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[23];

            string Power_file = testingFolder + "\\CalcEquivRoughness\\Power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[23];
            string Thrust_file = testingFolder + "\\CalcEquivRoughness\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87, 16, 10, 1, 0);
            WakeModList.AddWakeModel(1, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear");

            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();
            metList.WS_FirstInt = 0.5;
            metList.WS_IntSize = 1;
            metList.numWS = 30;

            double This_Equiv_Rough = WakeModList.CalcEquivRoughness(metList, 8, WakeModList.wakeModels[0], 80.0);
            Assert.AreEqual(This_Equiv_Rough, 1.9316, 0.01, "Wrong equivalent roughness for DAWM");


        }

        [TestMethod]
        public void CalcIBL_H1_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[23];
            string Power_file = testingFolder + "\\Calc_IBL_H1\\Power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[23];
            string Thrust_file = testingFolder + "\\Calc_IBL_H1\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87, 16, 10, 1, 0);

            Turbine[] UW_Turbs = new Turbine[1];
            UW_Turbs[0] = new Turbine();
            UW_Turbs[0].UTMX = 283000;
            UW_Turbs[0].UTMY = 4553300;

            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();

            WakeModList.AddWakeModel(1, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear");
            double This_IBL_H1 = WakeModList.Calc_IBL_H1(UW_Turbs[0], 280000, 4553500, WakeModList.wakeModels[0], 90f, 1.9316f, 80.0);

            Assert.AreEqual(This_IBL_H1, 750.4, 1, "Wrong IBL H1");

        }

        [TestMethod]
        public void CalcIBL_H2_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[23];
            string Power_file = testingFolder + "\\Calc_IBL_H2\\Power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[23];
            string Thrust_file = testingFolder + "\\Calc_IBL_H2\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87, 16, 10, 1, 0);

            Turbine[] UW_Turbs = new Turbine[1];
            UW_Turbs[0] = new Turbine();
            UW_Turbs[0].UTMX = 283000;
            UW_Turbs[0].UTMY = 4553300;

            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();
            metList.WS_FirstInt = 0.5f;
            metList.WS_IntSize = 1;
            metList.numWS = 30;

            WakeModList.AddWakeModel(1, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear");
            double This_IBL_H2 = WakeModList.Calc_IBL_H2(UW_Turbs[0], 280000, 4553500, WakeModList.wakeModels[0], 90f, metList, 80.0);

            Assert.AreEqual(This_IBL_H2, 394.2, 1, "Wrong IBL H2");

        }

        [TestMethod]
        public void CalcDAWM_Deficit_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[23];
            string Power_file = testingFolder + "\\CalcDAWM_Deficit\\Power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[23];
            string Thrust_file = testingFolder + "\\CalcDAWM_Deficit\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87, 16, 10, 1, 0);

            Turbine[] UW_Turbs = new Turbine[1];
            UW_Turbs[0] = new Turbine();
            UW_Turbs[0].UTMX = 283000;
            UW_Turbs[0].UTMY = 4553300;

            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();
            metList.WS_FirstInt = 0.5f;
            metList.WS_IntSize = 1;
            metList.numWS = 30;

            WakeModList.AddWakeModel(1, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear");

            double This_Def = WakeModList.Calc_DAWM_Deficit(UW_Turbs, 280000, 4553500, 90, 8, WakeModList.wakeModels[0], metList, 80.0);
            Assert.AreEqual(This_Def, 0.04742, 1, "Wrong wind speed deficit in DAWM");

        }

        [TestMethod]
        public void CalcNetSectEnergy_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[31];
            string Power_file = testingFolder + "\\CalcNetSectEnergy\\Power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[31];
            string Thrust_file = testingFolder + "\\CalcNetSectEnergy\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87, 16, 10, 1, 0);
            WakeModList.AddWakeModel(0, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear");

            string WR_file = testingFolder + "\\CalcNetSectEnergy\\Wind_Rose.txt";
            sr = new StreamReader(WR_file);
            double[] windRose = new double[16];
            for (int i = 0; i <= 15; i++)
                windRose[i] = Convert.ToSingle(sr.ReadLine()) / 100;

            string Sect_WS_file = testingFolder + "\\CalcNetSectEnergy\\Sect_WS.txt";
            sr = new StreamReader(Sect_WS_file);
            double[,] sectorWS = new double[16, 31];
            for (int i = 0; i <= 15; i++)
            {
                string This_WS_Array = sr.ReadLine();
                string[] This_Array_Split = This_WS_Array.Split('\t');
                for (int j = 0; j <= 30; j++)
                    sectorWS[i, j] = Convert.ToSingle(This_Array_Split[j]);
            }

            Continuum thisInst = new Continuum();
            thisInst.metList.WS_FirstInt = 0.5;
            thisInst.metList.WS_IntSize = 1;
            thisInst.metList.numWS = 30;
            Met met = new Met();
            thisInst.metList.AddMetTAB("dummy", 0, 0, 0, windRose, sectorWS, 0.5, 1, thisInst);
            double noLoss = 1.0;

            double[] This_Net_Energy = WakeModList.CalcNetSectEnergy(WakeModList.wakeModels[0], sectorWS, windRose, thisInst, noLoss);
            Assert.AreEqual(This_Net_Energy[0], 102.5134, 0.1, "Wrong net energy calculated Sector 0");
            Assert.AreEqual(This_Net_Energy[6], 96.69486, 0.1, "Wrong net energy calculated Sector 6");
            Assert.AreEqual(This_Net_Energy[14], 223.3589, 0.1, "Wrong net energy calculated Sector 14");
            Assert.AreEqual(This_Net_Energy[15], 124.5820, 0.1, "Wrong net energy calculated Sector 15");

        }

        [TestMethod]
        public void CalcWakeLosses_Test()
        {
            WakeCollection WakeModList = new WakeCollection();
            TurbineCollection turbineList = new TurbineCollection();

            double[] power = new double[31];
            string Power_file = testingFolder + "\\CalcWakeLosses\\power.txt";
            StreamReader sr = new StreamReader(Power_file);

            for (int i = 0; i <= 22; i++)
                power[i] = Convert.ToSingle(sr.ReadLine());

            double[] Thrust = new double[31];
            string Thrust_file = testingFolder + "\\CalcWakeLosses\\Thrust.txt";
            sr = new StreamReader(Thrust_file);

            for (int i = 0; i <= 22; i++)
                Thrust[i] = Convert.ToSingle(sr.ReadLine());

            turbineList.AddPowerCurve("GW 1500/87", 3, 22, 1500, power, Thrust, 87, 16, 10, 1, 0);
            WakeModList.AddWakeModel(0, 5, 10, turbineList.powerCurves[0], 10, 3.5f, 0.03f, "Linear");

            MetCollection metList = new MetCollection();
            metList.metItem = new Met[1];
            metList.metItem[0] = new Met();

            // Load sectorwise wind speed distribution
            string Sect_WS_file = testingFolder + "\\CalcWakeLosses\\Sect_WS.txt";
            sr = new StreamReader(Sect_WS_file);
            double[,] sectorWS = new double[16, 31];
            for (int i = 0; i <= 15; i++)
            {
                string This_WS_Array = sr.ReadLine();
                string[] This_Array_Split = This_WS_Array.Split('\t');
                for (int j = 0; j <= 30; j++)
                    sectorWS[i, j] = Convert.ToSingle(This_Array_Split[j]);
            }

            string WR_file = testingFolder + "\\CalcWakeLosses\\Wind_Rose.txt";
            sr = new StreamReader(WR_file);
            double[] windRose = new double[16];
            for (int i = 0; i <= 15; i++)
                windRose[i] = Convert.ToSingle(sr.ReadLine()) / 100;

            string Sect_AEP_file = testingFolder + "\\CalcWakeLosses\\Sect_AEP.txt";
            sr = new StreamReader(Sect_AEP_file);
            double[] Sect_AEP = new double[16];
            for (int i = 0; i <= 15; i++)
                Sect_AEP[i] = Convert.ToSingle(sr.ReadLine());

            double[] R_RD = new double[21];

            for (int i = 0; i <= 20; i++)
                R_RD[i] = i * 0.025f;

            WakeCollection.WakeLossCoeffs[] These_Coeffs = new WakeCollection.WakeLossCoeffs[140]; // 5 (DW_RDs) x 28 (WS > 3)
            int Ind_Count = 0;

            for (double DW_RD = 5.5f; DW_RD <= 5.9; DW_RD = DW_RD + 0.1f)
            {
                int DW_ind = (int)Math.Round((DW_RD - 2) / 0.1, 0);

                for (int i = 3; i <= 30; i++)
                {
                    double[] Vel_Def_Rad = new double[21];   // Velocity Deficit profile at X_Length_RD
                    double[,] WS_Def_EV_Grid = WakeModList.CalcWS_DeficitEddyViscosityGrid(2f, 6f, 0.1f, 0.025f, i, WakeModList.wakeModels[0], metList);
                    for (int radiusInd = 0; radiusInd <= 19; radiusInd++)
                        Vel_Def_Rad[radiusInd] = WS_Def_EV_Grid[DW_ind, radiusInd]; // index = 37 corresponds to DW dist = 5.7 (i.e. 2 + 0.1 * 37 = 5.7)

                    Vel_Def_Rad[20] = 0;
                    double[] Coeffs = WakeModList.CalcWakeProfileFit(Vel_Def_Rad, R_RD);
                    These_Coeffs[Ind_Count].freeStream = i;
                    These_Coeffs[Ind_Count].X_LengthRD = DW_RD;

                    These_Coeffs[Ind_Count].linRegInt = Coeffs[0];
                    These_Coeffs[Ind_Count].linCoeff4 = Coeffs[1];
                    These_Coeffs[Ind_Count].linCoeff3 = Coeffs[2];
                    These_Coeffs[Ind_Count].linCoeff2 = Coeffs[3];
                    These_Coeffs[Ind_Count].linCoeff1 = Coeffs[4];
                    Ind_Count++;
                }
            }
            turbineList.AddTurbine("Target Site", 280050, 4552500, 1);
            turbineList.AddTurbine("UW Site", 280000, 4553000, 1);
            Continuum thisInst = new Continuum();
            thisInst.turbineList = turbineList;
            thisInst.metList.WS_FirstInt = 0.5;
            thisInst.metList.WS_IntSize = 1;
            thisInst.metList.numWS = 30;           
            thisInst.metList.AddMetTAB("dummy", 0, 0, 0, windRose, sectorWS, 0.5, 1, thisInst);

            // Need to define exceed model
            thisInst.turbineList.exceed = new Exceedance();
            thisInst.turbineList.exceed.compositeLoss = new Exceedance.Monte_Carlo();
            thisInst.turbineList.exceed.compositeLoss.pVals1yr = new double[100];

            for (int i = 0; i < 100; i++)
                thisInst.turbineList.exceed.compositeLoss.pVals1yr[i] = 1.0;

            WakeCollection.WakeCalcResults WakeResults = WakeModList.CalcWakeLosses(These_Coeffs, 280050, 4552500, sectorWS, 4130, Sect_AEP, thisInst, WakeModList.wakeModels[0], windRose);

            Assert.AreEqual(WakeResults.sectorNetEnergy[0], 79.1604, 0.1, "Wrong net AEP");
            Assert.AreEqual(WakeResults.sectorWakedWS[0], 4.2851, 0.1, "Wrong waked wind speed");
            Assert.AreEqual(WakeResults.sectorWakeLoss[0], 0.2278, 0.1, "Wrong wake loss");
        }

        [TestMethod]
        public void CalcWakeWidth_Test()
        {
            WakeCollection wakeList = new WakeCollection();
            Wake_Model wakeModel = new Wake_Model();
            wakeModel.horizWakeExp = 5;
            double width = wakeList.CalcWakeWidth(0.9, wakeModel, 2.4);
            Assert.AreEqual(width, 0.934818, 0.01, "Wrong wake width Test 1");

            width = wakeList.CalcWakeWidth(0.9, wakeModel, 5.8);
            Assert.AreEqual(width, 1.230775, 0.01, "Wrong wake width Test 2");

            wakeModel.horizWakeExp = 9;
            width = wakeList.CalcWakeWidth(1.1, wakeModel, 3.8);
            Assert.AreEqual(width, 1.380452, 0.01, "Wrong wake width Test 3");

            width = wakeList.CalcWakeWidth(1.1, wakeModel, 6.4);
            Assert.AreEqual(width, 1.785549, 0.01, "Wrong wake width Test 4");
        }

        [TestMethod]
        public void CalcDownwindAndLateralDistanceFromUW_Turb_Test()
        {
            WakeCollection wakeList = new WakeCollection();
            Turbine UW_Turb = new Turbine();
            UW_Turb.UTMX = 277930;
            UW_Turb.UTMY = 4553361;
            Turbine DW_Turb = new Turbine();
            DW_Turb.UTMX = 277998;
            DW_Turb.UTMY = 4553199;

            // Test 1
            double thisWD = 350;
            double[] DWandLatDists = wakeList.CalcDownwindAndLateralDistanceFromUW_Turb(UW_Turb.UTMX, UW_Turb.UTMY, DW_Turb.UTMX, DW_Turb.UTMY, 87, thisWD);
            Assert.AreEqual(DWandLatDists[0], 1.969505, 0.001, "Wrong DW Dist Test 1");
            Assert.AreEqual(DWandLatDists[1], 0.44639, 0.001, "Wrong Lat Dist Test 1");

            // Test 2
            thisWD = 20;
            DWandLatDists = wakeList.CalcDownwindAndLateralDistanceFromUW_Turb(UW_Turb.UTMX, UW_Turb.UTMY, DW_Turb.UTMX, DW_Turb.UTMY, 87, thisWD);
            Assert.AreEqual(DWandLatDists[0], 1.4824463, 0.001, "Wrong DW Dist Test 2");
            Assert.AreEqual(DWandLatDists[1], 1.371337, 0.001, "Wrong Lat Dist Test 2");

            // Test 3
            thisWD = 90;
            DWandLatDists = wakeList.CalcDownwindAndLateralDistanceFromUW_Turb(UW_Turb.UTMX, UW_Turb.UTMY, DW_Turb.UTMX, DW_Turb.UTMY, 87, thisWD);
            Assert.AreEqual(DWandLatDists[0], -0.781609, 0.001, "Wrong DW Dist Test 3");
            Assert.AreEqual(DWandLatDists[1], 1.8620689, 0.001, "Wrong Lat Dist Test 3");

            // Test 4
            thisWD = 170;
            DWandLatDists = wakeList.CalcDownwindAndLateralDistanceFromUW_Turb(UW_Turb.UTMX, UW_Turb.UTMY, DW_Turb.UTMX, DW_Turb.UTMY, 87, thisWD);
            Assert.AreEqual(DWandLatDists[0], -1.9695049, 0.001, "Wrong DW Dist Test 4");
            Assert.AreEqual(DWandLatDists[1], 0.4463899, 0.001, "Wrong Lat Dist Test 4");

            // Test 5
            thisWD = 220;
            DWandLatDists = wakeList.CalcDownwindAndLateralDistanceFromUW_Turb(UW_Turb.UTMX, UW_Turb.UTMY, DW_Turb.UTMX, DW_Turb.UTMY, 87, thisWD);
            Assert.AreEqual(DWandLatDists[0], -0.9240188, 0.001, "Wrong DW Dist Test 5");
            Assert.AreEqual(DWandLatDists[1], 1.7956622, 0.001, "Wrong Lat Dist Test 5");
        }

        [TestMethod]
        public void InterpolateFindWakedDist_Test()
        {
            string fileName = testingFolder + "\\InterpolateFindWakedDist\\Dist.csv";
            StreamReader sr = new StreamReader(fileName);

            double[] dist = new double[31];
            double[] wakedDistWS = new double[31];
            int counter = 0;

            while (sr.EndOfStream == false)
            {
                double thisVal = Convert.ToDouble(sr.ReadLine());
                dist[counter] = thisVal;
                counter++;
            }

            sr.Close();

            fileName = testingFolder + "\\InterpolateFindWakedDist\\WakedDistWS.csv";
            sr = new StreamReader(fileName);

            counter = 0;
            while (sr.EndOfStream == false)
            {
                double thisVal = Convert.ToDouble(sr.ReadLine());
                wakedDistWS[counter] = thisVal;
                counter++;
            }

            MetCollection metList = new MetCollection();
            metList.WS_FirstInt = 0.5;
            metList.WS_IntSize = 1;
            WakeCollection wakeList = new WakeCollection();
            double[] wakedDist = wakeList.InterpolateFindWakedDist(wakedDistWS, dist, metList);

            Assert.AreEqual(wakedDist[2], 0.1715, 0.001, "Wrong waked dist Test 1");
            Assert.AreEqual(wakedDist[4], 0.296506, 0.001, "Wrong waked dist Test 2");
            Assert.AreEqual(wakedDist[6], 0.01881, 0.001, "Wrong waked dist Test 3");
            Assert.AreEqual(wakedDist[8], 0.0059, 0.001, "Wrong waked dist Test 4");

            sr.Close();
        }

        [TestMethod]
        public void CalcNetEnergyTimeSeries_Test()
        {
            Continuum thisInst = new Continuum();            
            string fileName = testingFolder + "\\WakeCollection TS testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            NodeCollection nodeList = new NodeCollection();
            Nodes targetNode = nodeList.GetTurbNode(thisTurb);
            string MCP_Method = thisInst.Get_MCP_Method();
            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0]; ;
            Wake_Model wakeModel = thisInst.wakeModelList.wakeModels[0];

            // Find wake loss coeffs
            WakeCollection.WakeLossCoeffs[] wakeCoeffs = null;
            int minDistance = 10000000;
            int maxDistance = 0;

            for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
            {
                int[] Min_Max_Dist = thisInst.turbineList.CalcMinMaxDistanceToTurbines(thisInst.turbineList.turbineEsts[i].UTMX, thisInst.turbineList.turbineEsts[i].UTMY);
                if (Min_Max_Dist[0] < minDistance) minDistance = Min_Max_Dist[0]; // this is min distance to turbine but when WD is at a different angle (not in line with turbines) the X dist is less than this value so making this always equal to 2*RD
                if (Min_Max_Dist[1] > maxDistance) maxDistance = Min_Max_Dist[1];
            }

            minDistance = (int)(2 * wakeModel.powerCurve.RD);
            if (maxDistance == 0) maxDistance = 15000; // maxDistance will be zero when there is only one turbine. Might be good to make this value constant
            wakeCoeffs = thisInst.wakeModelList.GetWakeLossesCoeffs(minDistance, maxDistance, wakeModel, thisInst.metList);

            Turbine wakedTurb = thisInst.turbineList.turbineEsts[2];
            double[] netEsts = thisInst.wakeModelList.CalcNetEnergyTimeSeries(wakeCoeffs, wakedTurb.UTMX, wakedTurb.UTMY, 6, thisInst, wakeModel, 270, 3000, 1);
            Assert.AreEqual(netEsts[0], 4.75284, 0.001, "Wrong waked WS Test 1");

            thisInst.Close();
        }
    }
}
