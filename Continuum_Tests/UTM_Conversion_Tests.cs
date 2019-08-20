using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;

namespace Continuum_Tests
{
    [TestClass]
    public class UTM_Conversion_Tests
    {
        [TestMethod]
        public void LLtoUTM_Test()
        {
            UTM_conversion thisConverter = new UTM_conversion();
            thisConverter.savedDatumIndex = 0;
            thisConverter.UTMZoneNumber = 16;
            double thisLat = 39.852;
            double thisLong = -84.549;
            UTM_conversion.UTM_coords theseCoords = thisConverter.LLtoUTM(thisLat, thisLong);

            Assert.AreEqual(709679, theseCoords.UTMEasting, 1, "Wrong easting in Test 1");
            Assert.AreEqual(4414205, theseCoords.UTMNorthing, 1, "Wrong northing in Test 1");

            thisConverter.UTMZoneNumber = 17;
            theseCoords = thisConverter.LLtoUTM(thisLat, thisLong);

            Assert.AreEqual(196370, theseCoords.UTMEasting, 1, "Wrong easting in Test 2");
            Assert.AreEqual(4417361, theseCoords.UTMNorthing, 1, "Wrong northing in Test 2");
        }

        [TestMethod]
        public void UTMtoLL_Test()
        {
            UTM_conversion thisConverter = new UTM_conversion();
            thisConverter.savedDatumIndex = 0;
            thisConverter.UTMZoneNumber = 17;
            thisConverter.hemisphere = "Northern";
            double UTMX = 477685;
            double UTMY = 4618287;
            UTM_conversion.Lat_Long theseLatLong = thisConverter.UTMtoLL(UTMX, UTMY);

            Assert.AreEqual(41.716058, theseLatLong.latitude, 1, "Wrong latitude in Test 1");
            Assert.AreEqual(-81.268258, theseLatLong.longitude, 1, "Wrong longitude in Test 1");

            thisConverter.UTMZoneNumber = 14;
            thisConverter.hemisphere = "Northern";
            UTMX = 292802;
            UTMY = 4346641.9;
            theseLatLong = thisConverter.UTMtoLL(UTMX, UTMY);

            Assert.AreEqual(39.244365, theseLatLong.latitude, 1, "Wrong latitude in Test 2");
            Assert.AreEqual(-101.400952, theseLatLong.longitude, 1, "Wrong longitude in Test 2");

        }
    }
}
