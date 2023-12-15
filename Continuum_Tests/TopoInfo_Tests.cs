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
    public class TopoInfo_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;

        public TopoInfo_Tests()
        {
            testingFolder = globals.testingFolder + "TopoInfo";
        }

        [TestMethod]
        public void ExportTopoData()
        {
            // Exports topography data to .csv for testing
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\TopoInfo testing.cfm";
            thisInst.Open(Filename);

            // Creates Topo_export.csv. Excel macro queries it for elevations specified below. Only needs to be run once.

            Export export = new Export();
            export.ExportTopo(thisInst, testingFolder);

            thisInst.Close();
        }

        [TestMethod]
        public void ExportLandCoverData()
        {
            // Exports land cover data to .csv for testing
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\TopoInfo testing.cfm";
            thisInst.Open(Filename);

            // Creates LC_export.csv. Excel macro queries it for land covers specified below. Only needs to be run once.

            Export export = new Export();
            export.ExportLC(thisInst, testingFolder);

            thisInst.Close();
        }

        [TestMethod]
        public void CalcElevs_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\TopoInfo testing.cfm";
            thisInst.Open(Filename);                    

            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 694370;
            int thisY = 4537997;

            double This_Elev = thisInst.topo.CalcElevs(thisX, thisY);
            Assert.AreEqual(This_Elev, 232.3494, 0.25, "Wrong elevation Test 1");

            // Test 2
            thisX = 712026;
            thisY = 4539040;

            This_Elev = thisInst.topo.CalcElevs(thisX, thisY);
            Assert.AreEqual(This_Elev, 221.6944, 0.25, "Wrong elevation Test 2");

            // Test 3
            thisX = 711325;
            thisY = 4552918;

            This_Elev = thisInst.topo.CalcElevs(thisX, thisY);
            Assert.AreEqual(This_Elev, 217.8234, 0.25, "Wrong elevation Test 3");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcExposures_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\TopoInfo testing.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 694370;
            int thisY = 4537997;
            double thisElev = 232.34937;
            int radius = 4000;
            
            Exposure thisExpo = thisInst.topo.CalcExposures(thisX, thisY, thisElev, radius, 1.0f, 1, thisInst.topo, 16);
            StreamReader sr = new StreamReader(testingFolder + "\\CalcExposures Test 1.txt");

            for (int i = 0; i < 16; i++)
            {
                string thisData = sr.ReadLine();
                Assert.AreEqual(Convert.ToDouble(thisData), thisExpo.expo[i], 0.05, "Wrong Exposure Test 1 WD =" + i);
            }

            sr.Close();
                
            // Test 2
            thisX = 712026;
            thisY = 4539040;
            thisElev = 221.694;
            radius = 6000;

            thisExpo = thisInst.topo.CalcExposures(thisX, thisY, thisElev, radius, 1.0f, 1, thisInst.topo, 16);
            sr = new StreamReader(testingFolder + "\\CalcExposures Test 2.txt");

            for (int i = 0; i < 16; i++)
            {
                string thisData = sr.ReadLine();
                Assert.AreEqual(Convert.ToDouble(thisData), thisExpo.expo[i], 0.05, "Wrong Exposure Test 2 WD =" + i);
            }

            sr.Close();

            // Test 3
            thisX = 711325;
            thisY = 4552918;
            thisElev = 217.823;
            radius = 10000;

            thisExpo = thisInst.topo.CalcExposures(thisX, thisY, thisElev, radius, 1.0f, 1, thisInst.topo, 16);
            sr = new StreamReader(testingFolder + "\\CalcExposures Test 3.txt");

            for (int i = 0; i < 16; i++)
            {
                string thisData = sr.ReadLine();
                Assert.AreEqual(Convert.ToDouble(thisData), thisExpo.expo[i], 0.05, "Wrong Exposure Test 3 WD =" + i);
            }

            sr.Close();
            thisInst.Close();
        }
                
        [TestMethod]
        public void ReadGeoTiffTopo_Test()
        {
            Continuum thisInst = new Continuum("");
            
            thisInst.savedParams.savedFileName = testingFolder + "\\Testing GeoTiff load.cfm";
            thisInst.SaveFile(true);
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 17;
            thisInst.UTM_conversions.hemisphere = "Northern";

            string Filename = testingFolder + "\\NED_15051924\\NED_15051924.tif";
            thisInst.topo.ReadGeoTiffTopo(Filename, thisInst);
            thisInst.topo.topoNumXY.X.calcs = thisInst.topo.topoNumXY.X.all;
            thisInst.topo.topoNumXY.Y.calcs = thisInst.topo.topoNumXY.Y.all;
            thisInst.topo.elevsForCalcs = thisInst.topo.topoElevs;

            // Test 1
            double UTMX = 268339.8f;
            double UTMY = 4564816.2f;
            double elev = thisInst.topo.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(214.557, elev, 0.25, "Wrong elevation Test 1");

            // Test 2
            UTMX = 291638.58f;
            UTMY = 4555225.2f;
            elev = thisInst.topo.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(242.645, elev, 0.25, "Wrong elevation Test 2");

            // Test 3
            UTMX = 268193.992f;
            UTMY = 4550969.3f;
            elev = thisInst.topo.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(245.988, elev, 0.25, "Wrong elevation Test 3");

            // Test 4
            UTMX = 285429.3f;
            UTMY = 4562086f;
            elev = thisInst.topo.CalcElevs(UTMX, UTMY);

            Assert.AreEqual(225.246, elev, 0.25, "Wrong elevation Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void ReadGeoTiffLandCover_Test()
        {
            Continuum thisInst = new Continuum("");
            
            thisInst.savedParams.savedFileName = testingFolder + "\\Testing LandCover load.cfm";
            thisInst.SaveFile(true);
            thisInst.UTM_conversions.savedDatumIndex = 0;
            thisInst.UTM_conversions.UTMZoneNumber = 17;
            thisInst.UTM_conversions.hemisphere = "Northern";

            string Filename = testingFolder + "\\LC1178895271 smaller\\LC1178895271.tif";
            thisInst.topo.SetUS_NLCD_Key();
            thisInst.topo.ReadGeoTiffLandCover(Filename, thisInst);
            thisInst.topo.LC_NumXY.X.plot = thisInst.topo.LC_NumXY.X.all;
            thisInst.topo.LC_NumXY.Y.plot = thisInst.topo.LC_NumXY.Y.all;

            // Test 1
            double UTMX = 273126f;
            double UTMY = 4546996f;
            int LC_Code = thisInst.topo.GetLC_Code(UTMX, UTMY);

            Assert.AreEqual(41, LC_Code, 0, "Wrong land cover code Test 1");

            // Test 2
            UTMX = 275955f;
            UTMY = 4544692f;
            LC_Code = thisInst.topo.GetLC_Code(UTMX, UTMY);

            Assert.AreEqual(23, LC_Code, 0, "Wrong land cover code Test 2");

            // Test 3
            UTMX = 292223.4f;
            UTMY = 4542114.2f;
            LC_Code = thisInst.topo.GetLC_Code(UTMX, UTMY);

            Assert.AreEqual(95, LC_Code, 0, "Wrong land cover code Test 3");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcSRDH_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\TopoInfo testing.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 694370;
            int thisY = 4537997;
            double thisElev = 232.3494;
            int radius = 4000;
            
            Exposure thisExpo = thisInst.topo.CalcExposures(thisX, thisY, thisElev, radius, 1.0f, 1, thisInst.topo, 16);
            thisInst.topo.CalcSRDH(ref thisExpo, thisX, thisY, radius, 1.0f, 16);

            StreamReader sr = new StreamReader(testingFolder + "\\CalcSRDH Test 1.csv");

            for (int i = 0; i < 16; i++)
            {
                string thisData = sr.ReadLine();
                string[] parsedData = thisData.Split(',');
                Assert.AreEqual(Convert.ToDouble(parsedData[0]), thisExpo.SR[i], 0.05, "Wrong Surface Roughness Test 1 WD =" + i);
                Assert.AreEqual(Convert.ToDouble(parsedData[1]), thisExpo.dispH[i], 0.05, "Wrong Displacement Height Test 1 WD =" + i);
            }

            sr.Close();

            // Test 2
            thisX = 712026;
            thisY = 4539040;
            thisElev = 221.6944;
            radius = 8000;

            thisExpo = thisInst.topo.CalcExposures(thisX, thisY, thisElev, radius, 1.0f, 1, thisInst.topo, 16);
            thisInst.topo.CalcSRDH(ref thisExpo, thisX, thisY, radius, 1.0f, 16);

            sr = new StreamReader(testingFolder + "\\CalcSRDH Test 2.csv");

            for (int i = 0; i < 16; i++)
            {
                string thisData = sr.ReadLine();
                string[] parsedData = thisData.Split(',');
                Assert.AreEqual(Convert.ToDouble(parsedData[0]), thisExpo.SR[i], 0.05, "Wrong Surface Roughness Test 2 WD =" + i);
                Assert.AreEqual(Convert.ToDouble(parsedData[1]), thisExpo.dispH[i], 0.05, "Wrong Displacement Height Test 2 WD =" + i);
            }

            sr.Close();
                        
            thisInst.Close();
        }

        [TestMethod]
        public void CalcExposuresWithSmallerR_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\TopoInfo testing.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 711325;
            int thisY = 4552918;
            double thisElev = 217.823;
            int minRadius = 8000;
            int maxRadius = 10000;
            
            Exposure smallerExposure = thisInst.topo.CalcExposures(thisX, thisY, thisElev, minRadius, 1.0f, 1, thisInst.topo, 16);
            Exposure thisExpo = thisInst.topo.CalcExposuresWithSmallerRadius(thisX, thisY, thisElev, maxRadius, 1.0f, 1, minRadius, smallerExposure, 16);

            StreamReader sr = new StreamReader(testingFolder + "\\CalcExposures Test 3.txt");

            for (int i = 0; i < 16; i++)
            {
                string thisData = sr.ReadLine();
                Assert.AreEqual(Convert.ToDouble(thisData), thisExpo.expo[i], 0.05, "Wrong Exposure from Smaller R Test 1 WD =" + i);
            }

            sr.Close();
            thisInst.Close();

        }

        [TestMethod]
        public void CalcSRDHWithSmallerR_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\TopoInfo testing.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 712026;
            int thisY = 4539040;
            double thisElev = 221.6944;
            int minRadius = 6000;
            int maxRadius = 8000;            
            Exposure thisExpo = new Exposure();

            Exposure smallerExposure = thisInst.topo.CalcExposures(thisX, thisY, thisElev, minRadius, 1.0f, 1, thisInst.topo, 16);
            thisInst.topo.CalcSRDH(ref smallerExposure, thisX, thisY, minRadius, 1.0f, 16);
            thisInst.topo.CalcSRDHwithSmallerRadius(ref thisExpo, thisX, thisY, maxRadius, 1.0f, 1, minRadius, smallerExposure, 16);

            StreamReader sr = new StreamReader(testingFolder + "\\CalcSRDH Test 2.csv");

            for (int i = 0; i < 16; i++)
            {
                string thisData = sr.ReadLine();
                string[] parsedData = thisData.Split(',');
                Assert.AreEqual(Convert.ToDouble(parsedData[0]), thisExpo.SR[i], 0.05, "Wrong SR from Smaller R Test 1 WD =" + i);
                Assert.AreEqual(Convert.ToDouble(parsedData[1]), thisExpo.dispH[i], 0.05, "Wrong DH from Smaller R Test 1 WD =" + i);
            }

            sr.Close();
            thisInst.Close();
        }
    }
}
