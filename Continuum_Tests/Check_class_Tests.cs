using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;
using OSGeo.GDAL;
using OSGeo.OGR;
using OSGeo.OSR;

namespace Continuum_Tests
{
    [TestClass]
    public class Check_class_Tests
    {
        Globals globals = new Globals();
        string testingFolder;
       
        public Check_class_Tests()
        {
            testingFolder = globals.testingFolder + "Check_class";
        }

        [TestMethod]
        public void TopoCheck_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\TopoCheck model.cfm";
            thisInst.Open(Filename);

            Check_class check = new Check_class();
            string allOrPlot = "Plot";

            bool isOk = check.TopoCheck(thisInst.topo, 850000, 4500000, "Test 1", false);
            Assert.AreEqual(isOk, true, "Wrong TopoCheck Test 1");

            isOk = check.TopoCheck(thisInst.topo, 684480, 4663890, "Test 2", false);
            Assert.AreEqual(isOk, false, "Wrong TopoCheck Test 2");

            isOk = check.TopoCheck(thisInst.topo, 1053800, 4350000, "Test 3", false);
            Assert.AreEqual(isOk, false, "Wrong TopoCheck Test 3");

            isOk = check.TopoCheck(thisInst.topo, 688300, 4335000, "Test 4", false);
            Assert.AreEqual(isOk, true, "Wrong TopoCheck Test 4");

            isOk = check.TopoCheck(thisInst.topo, 1034000, 4677000, "Test 5", false);
            Assert.AreEqual(isOk, false, "Wrong TopoCheck Test 5");

            thisInst.Close();

        }

        [TestMethod]
        public void LandCoverCheck_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\TopoCheck model.cfm";
            thisInst.Open(Filename);

            Check_class check = new Check_class();
            string allOrPlot = "Plot";

            bool isOk = check.LandCoverCheck(thisInst.topo, 682000, 4709000, "Test 1", false);
            Assert.AreEqual(isOk, true, "Wrong TopoCheck Test 1");

            isOk = check.LandCoverCheck(thisInst.topo, 682000, 4716600, "Test 2", false);
            Assert.AreEqual(isOk, false, "Wrong TopoCheck Test 2");

            isOk = check.LandCoverCheck(thisInst.topo, 1077700, 4674500, "Test 3", false);
            Assert.AreEqual(isOk, true, "Wrong TopoCheck Test 3");

            isOk = check.LandCoverCheck(thisInst.topo, 1083650, 4674000, "Test 4", false);
            Assert.AreEqual(isOk, false, "Wrong TopoCheck Test 4");

            thisInst.Close();

        }

        [TestMethod]
        public void IsGeoTiff_Test()
        {
            // Opens a GeoTiff with elevation data. IsGeoTiff should return true. Then opens Tiff with color RGB values. IsGeoTiff should return false
            string goodTiffName = testingFolder + "Findlay Topo.tif";
            string badTiffName = testingFolder + "DEM JM.tif";

            TopoInfo topo = new TopoInfo();
            Check_class check = new Check_class();

            GdalConfiguration.ConfigureGdal();
            Gdal.AllRegister();
            Dataset GDAL_obj;

            // Test function with GeoTiff file 
            try
            {
                GDAL_obj = Gdal.Open(goodTiffName, Access.GA_ReadOnly);
            }
            catch
            {
                return;
            }
            
            int width = GDAL_obj.RasterXSize;
            int height = GDAL_obj.RasterYSize;                       

            Band GD_Raster = GDAL_obj.GetRasterBand(1);

            double[] buff = new double[width * height];
            GD_Raster.ReadRaster(0, 0, width, height, buff, width, height, 0, 0);

            bool isGeoTiff = check.IsGeoTIFF(buff);
            Assert.AreSame(true, isGeoTiff);

            // Test function with a TIF (color RGB) file
            try
            {
                GDAL_obj = Gdal.Open(goodTiffName, Access.GA_ReadOnly);
            }
            catch
            {
                return;
            }

            width = GDAL_obj.RasterXSize;
            height = GDAL_obj.RasterYSize;

            GD_Raster = GDAL_obj.GetRasterBand(1);

            buff = new double[width * height];
            GD_Raster.ReadRaster(0, 0, width, height, buff, width, height, 0, 0);

            isGeoTiff = check.IsGeoTIFF(buff);
            Assert.AreSame(false, isGeoTiff);



        }
    }
}
