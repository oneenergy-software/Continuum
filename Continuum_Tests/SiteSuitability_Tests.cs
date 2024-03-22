using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class SiteSuitability_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;

        public SiteSuitability_Tests()
        {
            testingFolder = globals.testingFolder + "SiteSuitability";
        }

        [TestMethod]
        public void GetFlickerAngles_Test()
        {
            Continuum thisInst = new Continuum("");

            string fileName = testingFolder + "\\Shadow Flicker\\Flicker testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0]; // turbine W1         
            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Site Suitability");
            SiteSuitability.Zone[] zones = thisInst.siteSuitability.zones;
            double delta = 0.1;

            // Test 1
            SiteSuitability.FlickerAngles flickerAngles = thisInst.siteSuitability.GetFlickerAngles(thisInst, thisTurb, zones[0].latitude,
                zones[0].longitude, zones[0].xSize, zones[0].ySize, zones[0].elev, thisInst.modeledHeight, powerCurve);

            Assert.AreEqual(flickerAngles.targetAzimuthAngle, 63.08305, delta, "Wrong Azimuth Angle 1");
            Assert.AreEqual(flickerAngles.targetAltitudeAngle, 10.312314, delta, "Wrong Altitude Angle 1");
            Assert.AreEqual(flickerAngles.angleVariance, 11.98010, delta, "Wrong Angle Variance Angle 1");

            // Test 2
            flickerAngles = thisInst.siteSuitability.GetFlickerAngles(thisInst, thisTurb, zones[13].latitude,
                zones[13].longitude, zones[13].xSize, zones[13].ySize, zones[13].elev, thisInst.modeledHeight, powerCurve);

            Assert.AreEqual(flickerAngles.targetAzimuthAngle, -45.82536, delta, "Wrong Azimuth Angle 2");
            Assert.AreEqual(flickerAngles.targetAltitudeAngle, 2.0928330, delta, "Wrong Altitude Angle 2");
            Assert.AreEqual(flickerAngles.angleVariance, 2.3516538, delta, "Wrong Angle Variance Angle 2");

            // Test 3
            thisTurb = thisInst.turbineList.turbineEsts[1];
            flickerAngles = thisInst.siteSuitability.GetFlickerAngles(thisInst, thisTurb, zones[7].latitude,
                zones[7].longitude, zones[7].xSize, zones[7].ySize, zones[7].elev, thisInst.modeledHeight, powerCurve);

            Assert.AreEqual(flickerAngles.targetAzimuthAngle, -76.17705, delta, "Wrong Azimuth Angle 3");
            Assert.AreEqual(flickerAngles.targetAltitudeAngle, 4.5205311, delta, "Wrong Altitude Angle 3");
            Assert.AreEqual(flickerAngles.angleVariance, 4.425766, delta, "Wrong Angle Variance Angle 3");

            // Test 4
            thisTurb = thisInst.turbineList.turbineEsts[9];
            flickerAngles = thisInst.siteSuitability.GetFlickerAngles(thisInst, thisTurb, zones[19].latitude,
                zones[19].longitude, zones[19].xSize, zones[19].ySize, zones[19].elev, thisInst.modeledHeight, powerCurve);

            Assert.AreEqual(flickerAngles.targetAzimuthAngle, -142.61909, delta, "Wrong Azimuth Angle 4");
            Assert.AreEqual(flickerAngles.targetAltitudeAngle, 9.6506959, delta, "Wrong Altitude Angle 4");
            Assert.AreEqual(flickerAngles.angleVariance, 9.9490992, delta, "Wrong Angle Variance Angle 4");

            // Test 5
            thisTurb = thisInst.turbineList.turbineEsts[5];
            flickerAngles = thisInst.siteSuitability.GetFlickerAngles(thisInst, thisTurb, zones[19].latitude,
                zones[19].longitude, zones[19].xSize, zones[19].ySize, zones[19].elev, thisInst.modeledHeight, powerCurve);

            Assert.AreEqual(flickerAngles.targetAzimuthAngle, 29.920478, delta, "Wrong Azimuth Angle 5");
            Assert.AreEqual(flickerAngles.targetAltitudeAngle, 7.4365520, delta, "Wrong Altitude Angle 5");
            Assert.AreEqual(flickerAngles.angleVariance, 7.6212801, delta, "Wrong Angle Variance Angle 5");

            // Test 6
            thisTurb = thisInst.turbineList.turbineEsts[7];
            flickerAngles = thisInst.siteSuitability.GetFlickerAngles(thisInst, thisTurb, zones[17].latitude,
                zones[17].longitude, zones[17].xSize, zones[17].ySize, zones[17].elev, thisInst.modeledHeight, powerCurve);

            Assert.AreEqual(flickerAngles.targetAzimuthAngle, 105.7185, delta, "Wrong Azimuth Angle 5");
            Assert.AreEqual(flickerAngles.targetAltitudeAngle, 29.4846112, delta, "Wrong Altitude Angle 5");
            Assert.AreEqual(flickerAngles.angleVariance, 24.1787613, delta, "Wrong Angle Variance Angle 5");


            thisInst.Close();
        }

        [TestMethod]
        public void IsSunUp_Test()
        {
            SiteSuitability siteSuitability = new SiteSuitability();

            // Test 1
            double thisLat = 41.0;
            double thisLong = -82.0;
            int utcOFfset = -5;
            DateTime thisDate = Convert.ToDateTime("1/6/2019 7:30");
            bool isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(false, isSunUp, "Wrong SunUp Test 1");

            // Test 2            
            thisDate = Convert.ToDateTime("1/6/2019 8:30");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(true, isSunUp, "Wrong SunUp Test 2");

            // Test 3            
            thisDate = Convert.ToDateTime("1/6/2019 16:30");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(true, isSunUp, "Wrong SunUp Test 3");

            // Test 4            
            thisDate = Convert.ToDateTime("1/6/2019 18:30");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(false, isSunUp, "Wrong SunUp Test 4");

            // Test 5            
            thisDate = Convert.ToDateTime("8/5/2019 5:00");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(false, isSunUp, "Wrong SunUp Test 5");

            // Test 6            
            thisDate = Convert.ToDateTime("8/5/2019 6:00");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(true, isSunUp, "Wrong SunUp Test 6");

            // Test 7            
            thisDate = Convert.ToDateTime("8/5/2019 18:30");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(true, isSunUp, "Wrong SunUp Test 7");

            // Test 8            
            thisDate = Convert.ToDateTime("8/5/2019 20:30");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(false, isSunUp, "Wrong SunUp Test 8");

            // Test 9
            thisLat = -33;
            thisLong = 151.0;
            utcOFfset = 10;
            thisDate = Convert.ToDateTime("1/6/2019 4:30");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(false, isSunUp, "Wrong SunUp Test 9");

            // Test 10            
            thisDate = Convert.ToDateTime("1/6/2019 5:30");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(true, isSunUp, "Wrong SunUp Test 10");

            // Test 11            
            thisDate = Convert.ToDateTime("1/6/2019 18:30");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(true, isSunUp, "Wrong SunUp Test 11");

            // Test 12            
            thisDate = Convert.ToDateTime("1/6/2019 20:00");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(false, isSunUp, "Wrong SunUp Test 12");

            // Test 13            
            thisDate = Convert.ToDateTime("8/5/2019 6:15");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(false, isSunUp, "Wrong SunUp Test 13");

            // Test 14            
            thisDate = Convert.ToDateTime("8/5/2019 7:00");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(true, isSunUp, "Wrong SunUp Test 14");

            // Test 15            
            thisDate = Convert.ToDateTime("8/5/2019 17:00");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(true, isSunUp, "Wrong SunUp Test 15");

            // Test 16            
            thisDate = Convert.ToDateTime("8/5/2019 18:00");
            isSunUp = siteSuitability.isSunUp(thisDate, utcOFfset, thisLat, thisLong);
            Assert.AreEqual(false, isSunUp, "Wrong SunUp Test 16");
        }

        [TestMethod]
        public void GetSunPosition_Test()
        {
            SiteSuitability siteSuitability = new SiteSuitability();

            DateTime thisDate = Convert.ToDateTime("3/15/2019 9:30");
            double thisLat = 50;
            double thisLong = 15;
            int offset = 2;

            // Test 1
            SiteSuitability.SunPosition sunPosition = siteSuitability.GetSunPosition(thisDate, offset, thisLat, thisLong);

            Assert.AreEqual(119.7573, sunPosition.azimuth, 1, "Wrong Azimuth Test 1");
            Assert.AreEqual(19.93, sunPosition.altitude, 1, "Wrong Altitude Test 1");

            // Test 2
            thisLat = 50;
            thisLong = -115;
            offset = -6;
            thisDate = Convert.ToDateTime("9/27/2019 10:15");
            sunPosition = siteSuitability.GetSunPosition(thisDate, offset, thisLat, thisLong);

            Assert.AreEqual(123.9478, sunPosition.azimuth, 1, "Wrong Azimuth Test 2");
            Assert.AreEqual(23.1026, sunPosition.altitude, 1, "Wrong Altitude Test 2");

            // Test 3  
            thisDate = Convert.ToDateTime("1/27/2019 16:45");
            sunPosition = siteSuitability.GetSunPosition(thisDate, offset, thisLat, thisLong);
            Assert.AreEqual(-137.897, sunPosition.azimuth, 1, "Wrong Azimuth Test 3");
            Assert.AreEqual(12.13, sunPosition.altitude, 1, "Wrong Altitude Test 3");

        }

        [TestMethod]
        public void CalcNoiseLevel_Test()
        {
            Continuum thisInst = new Continuum("");

            string fileName = testingFolder + "\\Sound\\Sound testing.cfm";
            thisInst.Open(fileName);

            // Test 1
            UTM_conversion.UTM_coords theseUTM = thisInst.UTM_conversions.LLtoUTM(thisInst.siteSuitability.zones[6].latitude, thisInst.siteSuitability.zones[6].longitude);
            double thisNoise = thisInst.siteSuitability.CalcNoiseLevel(Convert.ToInt32(theseUTM.UTMEasting), Convert.ToInt32(theseUTM.UTMNorthing), thisInst);
            Assert.AreEqual(thisNoise, 42.2752, 0.1, "Wrong Noise Test 1");

            // Test 2
            theseUTM = thisInst.UTM_conversions.LLtoUTM(thisInst.siteSuitability.zones[12].latitude, thisInst.siteSuitability.zones[12].longitude);
            thisNoise = thisInst.siteSuitability.CalcNoiseLevel(Convert.ToInt32(theseUTM.UTMEasting), Convert.ToInt32(theseUTM.UTMNorthing), thisInst);
            Assert.AreEqual(thisNoise, 36.47677, 0.1, "Wrong Noise Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void GetIceMass_Test()
        {
            SiteSuitability siteSuitability = new SiteSuitability();

            double thisMass = siteSuitability.GetIceMass(0.554152);
            Assert.AreEqual(thisMass, 1.39652538, 0.001, "Wrong iceMass Test 1");

            thisMass = siteSuitability.GetIceMass(0.2919690);
            Assert.AreEqual(thisMass, 0.74762333, 0.001, "Wrong iceMass Test 2");

            thisMass = siteSuitability.GetIceMass(0.6717848);
            Assert.AreEqual(thisMass, 1.6876676, 0.001, "Wrong iceMass Test 3");

            thisMass = siteSuitability.GetIceMass(0.9678211);
            Assert.AreEqual(thisMass, 2.42035737, 0.001, "Wrong iceMass Test 4");

        }

        [TestMethod]
        public void GetIceShape_Test()
        {
            SiteSuitability siteSuitability = new SiteSuitability();

            double thisShape = siteSuitability.GetIceShape(0.554152);
            Assert.AreEqual(thisShape, 3.5032493, 0.001, "Wrong iceShape Test 1");

            thisShape = siteSuitability.GetIceShape(0.2919690);
            Assert.AreEqual(thisShape, 2.0350265, 0.001, "Wrong iceShape Test 2");

            thisShape = siteSuitability.GetIceShape(0.6717848);
            Assert.AreEqual(thisShape, 4.161995, 0.001, "Wrong iceShape Test 3");

            thisShape = siteSuitability.GetIceShape(0.9678211);
            Assert.AreEqual(thisShape, 5.81979848, 0.001, "Wrong iceShape Test 4");

        }

        [TestMethod]
        public void GetIceCrossSecArea_Test()
        {
            SiteSuitability siteSuitability = new SiteSuitability();

            double thisCrossSec = siteSuitability.GetIceCrossSecArea(1.3965253, 3.5032493);
            Assert.AreEqual(thisCrossSec, 0.0305128, 0.001, "Wrong iceShape Test 1");

            thisCrossSec = siteSuitability.GetIceCrossSecArea(0.74762332, 2.0350265);
            Assert.AreEqual(thisCrossSec, 0.01400570, 0.001, "Wrong iceShape Test 2");

            thisCrossSec = siteSuitability.GetIceCrossSecArea(1.68766761, 4.16199542);
            Assert.AreEqual(thisCrossSec, 0.0388325, 0.001, "Wrong iceShape Test 3");

            thisCrossSec = siteSuitability.GetIceCrossSecArea(2.42035736, 5.8197984);
            Assert.AreEqual(thisCrossSec, 0.0617534, 0.001, "Wrong iceShape Test 4");

        }

        [TestMethod]
        public void GetCd_Test()
        {
            SiteSuitability siteSuitability = new SiteSuitability();
            double iceShape = siteSuitability.GetIceShape(0.554152);
            double iceCd = siteSuitability.GetCd(iceShape);
            Assert.AreEqual(iceCd, 1.152679, 0.001, "Wrong drag coeff. Test 1");

            iceShape = siteSuitability.GetIceShape(0.291969);
            iceCd = siteSuitability.GetCd(iceShape);
            Assert.AreEqual(iceCd, 1.071926, 0.001, "Wrong drag coeff. Test 2");

            iceShape = siteSuitability.GetIceShape(0.671785);
            iceCd = siteSuitability.GetCd(iceShape);
            Assert.AreEqual(iceCd, 1.18891, 0.001, "Wrong drag coeff. Test 3");

            iceShape = siteSuitability.GetIceShape(0.967821);
            iceCd = siteSuitability.GetCd(iceShape);
            Assert.AreEqual(iceCd, 1.280089, 0.001, "Wrong drag coeff. Test 4");
        }

        [TestMethod]
        public void GetDegrees_Test()
        {
            SiteSuitability siteSuitability = new SiteSuitability();
            double thisDegs = siteSuitability.GetDegrees(0);
            Assert.AreEqual(0, thisDegs, 1, "Wrong blade throw angle Test 1");

            thisDegs = siteSuitability.GetDegrees(0.2);
            Assert.AreEqual(73, thisDegs, 1, "Wrong blade throw angle Test 2");

            thisDegs = siteSuitability.GetDegrees(0.4);
            Assert.AreEqual(147, thisDegs, 1, "Wrong blade throw angle Test 3");

            thisDegs = siteSuitability.GetDegrees(0.6);
            Assert.AreEqual(220, thisDegs, 1, "Wrong blade throw angle Test 4");

            thisDegs = siteSuitability.GetDegrees(0.8);
            Assert.AreEqual(294, thisDegs, 1, "Wrong blade throw angle Test 5");

            thisDegs = siteSuitability.GetDegrees(1);
            Assert.AreEqual(360, thisDegs, 1, "Wrong blade throw angle Test 6");

        }

        [TestMethod]
        public void GetRandomRadius_Test()
        {
            SiteSuitability siteSuitability = new SiteSuitability();
            TurbineCollection.PowerCurve powerCurve = new TurbineCollection.PowerCurve();
            powerCurve.RD = 87;

            double thisRad = siteSuitability.GetRandomRadius(0, powerCurve);
            Assert.AreEqual(thisRad, 1, 0.1, "Wrong rotor radius Test 1");

            thisRad = siteSuitability.GetRandomRadius(0.2, powerCurve);
            Assert.AreEqual(thisRad, 9.7, 0.1, "Wrong rotor radius Test 2");

            thisRad = siteSuitability.GetRandomRadius(0.4, powerCurve);
            Assert.AreEqual(thisRad, 18.4, 0.1, "Wrong rotor radius Test 3");

            thisRad = siteSuitability.GetRandomRadius(0.6, powerCurve);
            Assert.AreEqual(thisRad, 27.1, 0.1, "Wrong rotor radius Test 4");

            thisRad = siteSuitability.GetRandomRadius(0.8, powerCurve);
            Assert.AreEqual(thisRad, 35.8, 0.1, "Wrong rotor radius Test 5");

            thisRad = siteSuitability.GetRandomRadius(1, powerCurve);
            Assert.AreEqual(thisRad, 44.5, 0.1, "Wrong rotor radius Test 6");
        }

        [TestMethod]
        public void GetTipSpeed_Test()
        {
            Continuum thisInst = new Continuum("");

            string fileName = testingFolder + "\\Ice throw\\Ice throw testing.cfm";
            thisInst.Open(fileName);

            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0];
            double thisSpeed = thisInst.siteSuitability.GetTipSpeed(powerCurve, 2);
            Assert.AreEqual(thisSpeed, 0, 0.01, "Wrong Tip Speed Test 1");

            thisSpeed = thisInst.siteSuitability.GetTipSpeed(powerCurve, 4);
            Assert.AreEqual(thisSpeed, 3.767647, 0.01, "Wrong Tip Speed Test 2");

            thisSpeed = thisInst.siteSuitability.GetTipSpeed(powerCurve, 6);
            Assert.AreEqual(thisSpeed, 17.89632, 0.01, "Wrong Tip Speed Test 3");

            thisSpeed = thisInst.siteSuitability.GetTipSpeed(powerCurve, 8);
            Assert.AreEqual(thisSpeed, 44.62307, 0.01, "Wrong Tip Speed Test 4");

            thisSpeed = thisInst.siteSuitability.GetTipSpeed(powerCurve, 10);
            Assert.AreEqual(thisSpeed, 78.8262, 0.01, "Wrong Tip Speed Test 5");

            thisSpeed = thisInst.siteSuitability.GetTipSpeed(powerCurve, 12);
            Assert.AreEqual(thisSpeed, 103.7869, 0.01, "Wrong Tip Speed Test 6");

            thisSpeed = thisInst.siteSuitability.GetTipSpeed(powerCurve, 14);
            Assert.AreEqual(thisSpeed, 108.9085, 0.01, "Wrong Tip Speed Test 7");

            thisInst.Close();
        }

        [TestMethod]
        public void GetIceSpeed_Test()
        {
            SiteSuitability siteSuitability = new SiteSuitability();
            TurbineCollection.PowerCurve powerCurve = new TurbineCollection.PowerCurve();
            powerCurve.RD = 87;

            double iceSpeed = siteSuitability.GetIceSpeed(45, 18, powerCurve);
            Assert.AreEqual(iceSpeed, 18.62069, 0.001, "Wrong ice speed Test 1");

            iceSpeed = siteSuitability.GetIceSpeed(90, 18, powerCurve);
            Assert.AreEqual(iceSpeed, 37.24138, 0.001, "Wrong ice speed Test 2");

            iceSpeed = siteSuitability.GetIceSpeed(90, 45, powerCurve);
            Assert.AreEqual(iceSpeed, 93.10345, 0.001, "Wrong ice speed Test 3");

            iceSpeed = siteSuitability.GetIceSpeed(70, 30, powerCurve);
            Assert.AreEqual(iceSpeed, 48.27586, 0.001, "Wrong ice speed Test 4");
        }

        [TestMethod]
        public void ExportIceThrows()
        {
            Continuum thisInst = new Continuum("");

            string fileName = testingFolder + "\\Ice throw\\Ice throw testing.cfm";
            thisInst.Open(fileName);

            // Test 1: W1 WD Ind = 0
            string test1Hits = testingFolder + "\\Ice throw\\Test 1 W1 Hits.csv";
            double turbUTMX = 277929.9674;
            double turbUTMY = 4553360.947;
            int test1WD_Ind = 0;
            StreamWriter sw1 = new StreamWriter(test1Hits);

            // Test 2: W1 WD Ind = 10
            string test2Hits = testingFolder + "\\Ice throw\\Test 2 W1 Hits.csv";
            int test2WD_Ind = 10;
            StreamWriter sw2 = new StreamWriter(test2Hits);

            // Test 3: W1 WD Ind = 15
            string test3Hits = testingFolder + "\\Ice throw\\Test 3 W1 Hits.csv";
            int test3WD_Ind = 15;
            StreamWriter sw3 = new StreamWriter(test3Hits);

            SiteSuitability.FinalPosition[] iceHits = thisInst.siteSuitability.yearlyIceHits[0].iceHits; // ice hits in first year of simulation

            for (int i = 0; i < iceHits.Length; i++)
            {

                if (iceHits[i].turbineName == "W1")
                {
                    double thisDirection = thisInst.topo.GetDirection((iceHits[i].thisX - turbUTMX), (iceHits[i].thisZ - turbUTMY));
                    int thisWD_Ind = thisInst.metList.GetWD_Ind(thisDirection);

                    if (thisWD_Ind == test1WD_Ind)
                        sw1.WriteLine(iceHits[i].thisX + "," + iceHits[i].thisZ + ",");
                    else if (thisWD_Ind == test2WD_Ind)
                        sw2.WriteLine(iceHits[i].thisX + "," + iceHits[i].thisZ + ",");
                    else if (thisWD_Ind == test3WD_Ind)
                        sw3.WriteLine(iceHits[i].thisX + "," + iceHits[i].thisZ + ",");
                }
            }

            sw1.Close();
            sw2.Close();
            sw3.Close();

        }

        [TestMethod]
        public void CalcIceHitVersusDistance_Test()
        {
            Continuum thisInst = new Continuum("");

            string fileName = testingFolder + "\\Ice throw\\Ice throw testing.cfm";
            thisInst.Open(fileName);

            // Test 1
            double[] iceHits = thisInst.siteSuitability.CalcIceHitVersusDistance(thisInst.siteSuitability.yearlyIceHits[0].iceHits, 0, "W1", thisInst);

            Assert.AreEqual(iceHits[0], 62, 0, "Wrong number of ice hits. Test 1 Index 0");
            Assert.AreEqual(iceHits[1], 45, 0, "Wrong number of ice hits. Test 1 Index 1");
            Assert.AreEqual(iceHits[2], 7, 0, "Wrong number of ice hits. Test 1 Index 2");

            // Test 2
            iceHits = thisInst.siteSuitability.CalcIceHitVersusDistance(thisInst.siteSuitability.yearlyIceHits[0].iceHits, 10, "W1", thisInst);

            Assert.AreEqual(iceHits[0], 43, 0, "Wrong number of ice hits. Test 2 Index 0");
            Assert.AreEqual(iceHits[1], 20, 0, "Wrong number of ice hits. Test 2 Index 1");
            Assert.AreEqual(iceHits[2], 0, 0, "Wrong number of ice hits. Test 2 Index 2");

            // Test 3
            iceHits = thisInst.siteSuitability.CalcIceHitVersusDistance(thisInst.siteSuitability.yearlyIceHits[0].iceHits, 15, "W1", thisInst);

            Assert.AreEqual(iceHits[0], 65, 0, "Wrong number of ice hits. Test 3 Index 0");
            Assert.AreEqual(iceHits[1], 53, 0, "Wrong number of ice hits. Test 3 Index 1");
            Assert.AreEqual(iceHits[2], 13, 0, "Wrong number of ice hits. Test 3 Index 2");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcProbabilityOfHits()
        {
            Continuum thisInst = new Continuum("");

            string fileName = testingFolder + "\\Ice Throw\\Ice Throw testing.cfm";
            thisInst.Open(fileName);

            UTM_conversion.UTM_coords theseUTM = new UTM_conversion.UTM_coords();
            theseUTM.UTMZoneNumber = thisInst.UTM_conversions.UTMZoneNumber;
            theseUTM.hemisphere = "Northern";
            theseUTM.UTMEasting = 277870;
            theseUTM.UTMNorthing = 4553300;
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(theseUTM.UTMEasting, theseUTM.UTMNorthing);

            SiteSuitability.Zone zone = new SiteSuitability.Zone();
            zone.latitude = theseLL.latitude;
            zone.longitude = theseLL.longitude;
            zone.xSize = 20;
            zone.ySize = 20;

            // Test 1: Probability of at least one ice hit in a year
            double probHits = thisInst.siteSuitability.CalcProbabilityOfHits(zone, 0, thisInst);
            Assert.AreEqual(probHits, 1.0, 0.001, "Wrong Ice hit probability Test 1");

            // Test 2: Prob of 1 or more hits
            probHits = thisInst.siteSuitability.CalcProbabilityOfHits(zone, 1, thisInst);
            Assert.AreEqual(probHits, 0.3, 0.001, "Wrong Ice hit probability Test 2");

            // Test 3: Prob of 2 or more hits
            probHits = thisInst.siteSuitability.CalcProbabilityOfHits(zone, 2, thisInst);
            Assert.AreEqual(probHits, 0.05, 0.001, "Wrong Ice hit probability Test 3");

            // Test 4: Prob of 3 or more hits
            probHits = thisInst.siteSuitability.CalcProbabilityOfHits(zone, 3, thisInst);
            Assert.AreEqual(probHits, 0.00, 0.001, "Wrong Ice hit probability Test 4");

            // Test 5: Prob of 4 or more hits
            probHits = thisInst.siteSuitability.CalcProbabilityOfHits(zone, 4, thisInst);
            Assert.AreEqual(probHits, 0.00, 0.001, "Wrong Ice hit probability Test 5");

            // Test 6: Prob of more than 4 hits
            probHits = thisInst.siteSuitability.CalcProbabilityOfHits(zone, 5, thisInst);
            Assert.AreEqual(probHits, 0.00, 0.001, "Wrong Ice hit probability Test 6");

            thisInst.Close();
        }

        [TestMethod]
        public void GetSunriseAndSunsetTimes_Test()
        {
            SiteSuitability siteSuitability = new SiteSuitability();

            // Test 1
            DateTime thisDate = Convert.ToDateTime("1/6/2019");
            DateTime[] sunTimes = siteSuitability.GetSunriseAndSunsetTimes(41, -85, -5, thisDate);

            Assert.AreEqual(sunTimes[0].Hour, 8, 0, "Wrong sunrise hour Test 1");
            Assert.AreEqual(sunTimes[0].Minute, 5, 1, "Wrong sunrise minute Test 1");

            Assert.AreEqual(sunTimes[1].Hour, 17, 0, "Wrong sunset hour Test 1");
            Assert.AreEqual(sunTimes[1].Minute, 26, 1, "Wrong sunset minute Test 1");

            // Test 2
            thisDate = Convert.ToDateTime("8/5/2019");
            sunTimes = siteSuitability.GetSunriseAndSunsetTimes(41, -85, -5, thisDate);

            Assert.AreEqual(sunTimes[0].Hour, 5, 0, "Wrong sunrise hour Test 2");
            Assert.AreEqual(sunTimes[0].Minute, 39, 1, "Wrong sunrise minute Test 2");

            Assert.AreEqual(sunTimes[1].Hour, 19, 0, "Wrong sunset hour Test 2");
            Assert.AreEqual(sunTimes[1].Minute, 53, 1, "Wrong sunset minute Test 2");

            // Test 3
            thisDate = Convert.ToDateTime("1/6/2019");
            sunTimes = siteSuitability.GetSunriseAndSunsetTimes(-0.26, -77.9, -5, thisDate);

            Assert.AreEqual(sunTimes[0].Hour, 6, 0, "Wrong sunrise hour Test 3");
            Assert.AreEqual(sunTimes[0].Minute, 13, 1, "Wrong sunrise minute Test 3");

            Assert.AreEqual(sunTimes[1].Hour, 18, 0, "Wrong sunset hour Test 3");
            Assert.AreEqual(sunTimes[1].Minute, 21, 1, "Wrong sunset minute Test 3");

            // Test 4
            thisDate = Convert.ToDateTime("8/5/2019");
            sunTimes = siteSuitability.GetSunriseAndSunsetTimes(-0.26, -77.9, -5, thisDate);

            Assert.AreEqual(sunTimes[0].Hour, 6, 0, "Wrong sunrise hour Test 4");
            Assert.AreEqual(sunTimes[0].Minute, 15, 1, "Wrong sunrise minute Test 4");

            Assert.AreEqual(sunTimes[1].Hour, 18, 0, "Wrong sunset hour Test 4");
            Assert.AreEqual(sunTimes[1].Minute, 21, 1, "Wrong sunset minute Test 4");

        }

        [TestMethod]
        public void GetTotalFlickerHoursByMonthAndH_Test()
        {
            Continuum thisInst = new Continuum("");

            string fileName = testingFolder + "\\Shadow Flicker\\Flicker testing.cfm";
            thisInst.Open(fileName);

            // Test 1 Jan 8:00
            SiteSuitability.FlickerGrid[] flickerHours = thisInst.siteSuitability.GetTotalFlickerHoursByMonthAndHour(0, 8);
            Assert.AreEqual(flickerHours[12].flickerStats.totalShadowMinsPerYear, 6, 0.1, "Wrong number of flicker mins Test 1");

            // Test 2 Jan 9:00
            flickerHours = thisInst.siteSuitability.GetTotalFlickerHoursByMonthAndHour(0, 9);
            Assert.AreEqual(flickerHours[21].flickerStats.totalShadowMinsPerYear, 2, 0.1, "Wrong number of flicker mins Test 2");

            // Test 3 July 6:00
            flickerHours = thisInst.siteSuitability.GetTotalFlickerHoursByMonthAndHour(6, 6);
            Assert.AreEqual(flickerHours[19].flickerStats.totalShadowMinsPerYear, 3, 0.1, "Wrong number of flicker mins Test 3");

            thisInst.Close();
        }

        [TestMethod]
        public void GetTotalFlickerHours_Test()
        {
            Continuum thisInst = new Continuum("");

            string fileName = testingFolder + "\\Shadow Flicker\\Flicker testing.cfm";
            thisInst.Open(fileName);

            // Test 1
            double flickerHours = thisInst.siteSuitability.GetTotalFlickerHours(thisInst.siteSuitability.zones[0], 1, 8);
            Assert.AreEqual(flickerHours, 24, 0, "Wrong number flicker hours Test 1");

            // Test 2
            flickerHours = thisInst.siteSuitability.GetTotalFlickerHours(thisInst.siteSuitability.zones[1], 9, 16);
            Assert.AreEqual(flickerHours, 26, 0, "Wrong number flicker hours Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void GenerateWS_CDFs_Test()
        {
            Continuum thisInst = new Continuum("");

            string fileName = testingFolder + "\\Ice throw\\Ice throw testing.cfm";
            thisInst.Open(fileName);

            double[,] theseCDFs = thisInst.siteSuitability.GenerateWS_CDFs(thisInst.metList.metItem[0].WSWD_Dists[0].sectorWS_Dist); // i = WD; j = WS

            Assert.AreEqual(theseCDFs[2, 2], 0.099695, 0.001, "Wrong CDF Value Test 1");
            Assert.AreEqual(theseCDFs[6, 4], 0.525804, 0.001, "Wrong CDF Value Test 2");
            Assert.AreEqual(theseCDFs[0, 6], 0.762135, 0.001, "Wrong CDF Value Test 3");
            Assert.AreEqual(theseCDFs[15, 8], 0.912769, 0.001, "Wrong CDF Value Test 4");


            thisInst.Close();
        }

        [TestMethod]
        public void FindCDF_WS_Test()
        {
            Continuum thisInst = new Continuum("");

            string fileName = testingFolder + "\\Ice Throw\\Ice throw testing.cfm";
            thisInst.Open(fileName);

            double[,] theseCDFs = thisInst.siteSuitability.GenerateWS_CDFs(thisInst.metList.metItem[0].WSWD_Dists[0].sectorWS_Dist); // i = WD; j = WS
            double[] thisCDF = thisInst.siteSuitability.GetOneWS_CDF(theseCDFs, 0);

            // Test 1
            double thisWS = thisInst.siteSuitability.FindCDF_WS(thisCDF, 0.035449, thisInst.metList);
            Assert.AreEqual(thisWS, 1, 0, "Wrong WS Test 1");

            // Test 2
            thisWS = thisInst.siteSuitability.FindCDF_WS(thisCDF, 0.266584, thisInst.metList);
            Assert.AreEqual(thisWS, 3, 0, "Wrong WS Test 2");

            // Test 3
            thisWS = thisInst.siteSuitability.FindCDF_WS(thisCDF, 0.658374, thisInst.metList);
            Assert.AreEqual(thisWS, 5, 0, "Wrong WS Test 3");

            // Test 4
            thisWS = thisInst.siteSuitability.FindCDF_WS(thisCDF, 0.730383, thisInst.metList);
            Assert.AreEqual(thisWS, 6, 0, "Wrong WS Test 4");

            // Test 5
            thisWS = thisInst.siteSuitability.FindCDF_WS(thisCDF, 0.99066, thisInst.metList);
            Assert.AreEqual(thisWS, 12, 0, "Wrong WS Test 5");

            thisInst.Close();
        }

        [TestMethod]
        public void ExportShadowFlickerMap12x24()
        {
            string exportFile = testingFolder + "\\Shadow Flicker\\Flicker Map vals.csv";
            Continuum thisInst = new Continuum("");

            string fileName = testingFolder + "\\Shadow Flicker\\Flicker testing.cfm";
            thisInst.Open(fileName);

            StreamWriter sw = new StreamWriter(exportFile);
            sw.Write("UTMX, UTMY,");
            for (int i = 1; i <= 12; i++)
                for (int j = 0; j <= 23; j++)
                    sw.Write("Month " + i + " Hour " + j + ",");

            sw.WriteLine();
            int totalNum = thisInst.siteSuitability.numXFlicker * thisInst.siteSuitability.numYFlicker;

            for (int i = 0; i < totalNum; i++)
            {
                sw.Write(thisInst.siteSuitability.flickerMap[i].UTMX.ToString() + "," + thisInst.siteSuitability.flickerMap[i].UTMY.ToString() + ",");

                for (int m = 0; m < 12; m++)
                    for (int h = 0; h < 24; h++)
                        sw.Write(thisInst.siteSuitability.flickerMap[i].flickerStats.shadowMins12x24[m, h].ToString() + ",");

                sw.WriteLine();

            }

            sw.Close();
            thisInst.Close();

        }
    }
}

