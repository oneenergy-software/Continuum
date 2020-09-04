using System;
using GeoTimeZone;

namespace ContinuumNS
{
    /// <summary> Class with functions to convert UTM to/from lat/long coordinates </summary>
    [Serializable()]
    public class UTM_conversion
    {
        /// <summary> Saved datum index. 0 = NAD83/WGS84; 1 = GRS 80; 2 = WGS72 (NASA, DOD) 100 = none selected </summary>
        public int savedDatumIndex = 100;
        /// <summary> UTM zone number. -999 if not specified </summary>
        public int UTMZoneNumber = -999;
        /// <summary> Hemisphere: "Northern" or "Southern" </summary>
        public string hemisphere = "";
        /// <summary> Convert to radians multiplier </summary>
        double deg2rad = Math.PI / 180;
        /// <summary> Convert to degrees multiplier </summary>
        double rad2deg = 180 / Math.PI;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Holds equatorial radius and square of eccentricity for an ellipsoid </summary>
        public struct Ellipsoid_Shapes
        {
            /// <summary> Radius at equator </summary>
            public double equatorialRadius;
            /// <summary> Square of eccentricity (i.e. measure of how much a conic section deviates from circular) </summary>
            public double eccentricitySquared;
        }

        /// <summary> Holds UTM zone, datum, and coordiantes (northing, easting) </summary>
        [Serializable()]
        public struct UTM_coords
        {
            /// <summary> UTM Zone number </summary>
            public int UTMZoneNumber;
            /// <summary> Northern or Southern hemisphere </summary>
            public string hemisphere;
            /// <summary> Northing </summary>
            public double UTMNorthing;
            /// <summary> Easting </summary>
            public double UTMEasting;
        }

        /// <summary> Holds geographic coordiantes </summary>
        [Serializable()]
        public struct Lat_Long
        {
            /// <summary> Latitude (degs) </summary>
            public double latitude;
            /// <summary> Longitude (degs) </summary>
            public double longitude;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Clears UTM datum, zone and hemisphere </summary>
        public void ResetDefaults()
        { 
            savedDatumIndex = 100;
            UTMZoneNumber = -999;
            hemisphere = "";
        }

        /// <summary> Returns ellipse shape based on index </summary>
        public Ellipsoid_Shapes GetEllipsoidShape(int ellipsoidIndex) 
        {
            Ellipsoid_Shapes thisShape = new Ellipsoid_Shapes();

            if (ellipsoidIndex == 0) { // NAD83/WGS84 (Global)
                thisShape.equatorialRadius = 6378137;
                thisShape.eccentricitySquared = 0.00669438;
            }
            else if (ellipsoidIndex == 1) { // GRS 80 (US)
                thisShape.equatorialRadius = 6378137;
                thisShape.eccentricitySquared = 0.00669438;
            }
            else if (ellipsoidIndex == 2) { // WGS72 (NASA, DOD)
                thisShape.equatorialRadius = 6378135;
                thisShape.eccentricitySquared = 0.006694324;
            }
            else if (ellipsoidIndex == 3) { // Australian 1965
                thisShape.equatorialRadius = 6378160;
                thisShape.eccentricitySquared = 0.006694548;
            }
            else if (ellipsoidIndex == 4) { // Krasovsky 1940 (Russia)
                thisShape.equatorialRadius = 6378245;
                thisShape.eccentricitySquared = 0.006693427;
            }
            else if (ellipsoidIndex == 5) { // International (1924) -Hayford (1909) (Europe)
                thisShape.equatorialRadius = 6378388;
                thisShape.eccentricitySquared = 0.006722684;
            }
            else if (ellipsoidIndex == 6) { // Clake 1880 (France, Africa)
                thisShape.equatorialRadius = 6378249.1;
                thisShape.eccentricitySquared = 0.006803488;
            }
            else if (ellipsoidIndex == 7) { // NAD27/Clarke 1866 (North America)
                thisShape.equatorialRadius = 6378206.4;
                thisShape.eccentricitySquared = 0.006768658;
            }
            else if (ellipsoidIndex == 8) { // Airy 1830 (Great Britain)
                thisShape.equatorialRadius = 6377563.4;
                thisShape.eccentricitySquared = 0.006670544;
            }
            else if (ellipsoidIndex == 9) { // Bessel 1841 (Central Europe, Chile, Indonesia)
                thisShape.equatorialRadius = 6377397.2;
                thisShape.eccentricitySquared = 0.006674375;
            }
            else if (ellipsoidIndex == 10) { // Everest 1830 (South Asia)
                thisShape.equatorialRadius = 6377276.3;
                thisShape.eccentricitySquared = 0.006637837;
            }

            return thisShape;
        }

        /// <summary> Returns name of datum based on index </summary>
        public string GetDatumString(int datumInd) 
        {
            string datumString = "";

            if (datumInd == 0)
                datumString = "NAD83/WGS84";
            else if (datumInd == 1)
                datumString = "GRS80";
            else if (datumInd == 2)
                datumString = "WGS72";
            else if (datumInd == 3)
                datumString = "Australian(1965)";
            else if (datumInd == 4)
                datumString = "Krasovsky(1940)";
            else if (datumInd == 5)
                datumString = "International(1924)";
            else if (datumInd == 6)
                datumString = "Clarke(1880)";
            else if (datumInd == 7)
                datumString = "NAD27/Clarke 1866 ";
            else if (datumInd == 8)
                datumString = "Airy 1830 ";
            else if (datumInd == 9)
                datumString = "Bessel 1841";
            else if (datumInd == 10)
                datumString = "Everest 1830";

            return datumString;
        }

        /// <summary> Converts lat/long to UTM coords.  Equations from USGS Bulletin 1532 </summary>
        public UTM_coords LLtoUTM(double thisLat, double thisLong)
        {           
            
            UTM_coords theseUTMcoords = new UTM_coords();

            if ((savedDatumIndex < 0 || savedDatumIndex > 10))
                return theseUTMcoords;

            Ellipsoid_Shapes thisEllip = GetEllipsoidShape(savedDatumIndex);
            double equatRad = thisEllip.equatorialRadius;
            double eccSquared = thisEllip.eccentricitySquared;
            double k0 = 0.9996f;                      

            //   //Make sure the longitude is between -180.00 .. 179.9
            double longTemp = (thisLong + 180) - (int)((thisLong + 180) / 360) * 360 - 180; //// -180.00 .. 179.9;

            double latRad = thisLat * deg2rad;
            double longRad = longTemp * deg2rad;
         //   double longOriginRad;

            if (UTMZoneNumber == -999)
                UTMZoneNumber = (int)((longTemp + 180) / 6) + 1;

            if ((thisLat >= 56.0 && thisLat < 64.0 && longTemp >= 3.0 && longTemp < 12.0)) UTMZoneNumber = 32;

            //// Special zones for Svalbard
            if ((thisLat >= 72.0 && thisLat < 84.0)) {

                if ((longTemp >= 0.0 && longTemp < 9.0))
                    UTMZoneNumber = 31;
                else if ((longTemp >= 9.0 && longTemp < 21.0))
                    UTMZoneNumber = 33;
                else if ((longTemp >= 21.0 && longTemp < 33.0))
                    UTMZoneNumber = 35;
                else if ((longTemp >= 33.0 && longTemp < 42.0))
                    UTMZoneNumber = 37;                
            }
            double longOrigin = (UTMZoneNumber - 1) * 6 - 180 + 3;  ////+3 puts origin in middle of zone
            double longOriginRad = longOrigin * deg2rad;

            ////compute the UTM Zone from the latitude and longitude
            theseUTMcoords.UTMZoneNumber = UTMZoneNumber;
            theseUTMcoords.hemisphere = hemisphere;

            double eccPrimeSquared = (eccSquared) / (1 - eccSquared);

            double N = equatRad / Math.Pow((1 - eccSquared * Math.Sin(latRad) * Math.Sin(latRad)), 0.5);
            double T = Math.Tan(latRad) * Math.Tan(latRad);
            double C = eccPrimeSquared * Math.Cos(latRad) * Math.Cos(latRad);
            double A = Math.Cos(latRad) * (longRad - longOriginRad);

            double M = equatRad * ((1.0f - eccSquared / 4.0f - 3.0f * eccSquared * eccSquared / 64.0f - 5.0f * eccSquared * eccSquared * eccSquared / 256.0f) * latRad
                - (3 * eccSquared / 8 + 3 * eccSquared * eccSquared / 32 + 45 * eccSquared * eccSquared * eccSquared / 1024) * Math.Sin(2 * latRad)
                + (15 * eccSquared * eccSquared / 256 + 45 * eccSquared * eccSquared * eccSquared / 1024) * Math.Sin(4 * latRad)
                - (35 * eccSquared * eccSquared * eccSquared / 3072) * Math.Sin(6 * latRad));

            theseUTMcoords.UTMEasting = k0 * N * (A + (1 - T + C) * A * A * A / 6 + (5 - 18 * T + T * T + 72 * C - 58 * eccPrimeSquared) * A * A * A * A * A / 120) + 500000.0;

            theseUTMcoords.UTMNorthing = k0 * (M + N * Math.Tan(latRad) * (A * A / 2 + (5 - T + 9 * C + 4 * C * C) * A * A * A * A / 24 + (61 - 58 * T + T * T + 600 * C - 330 * eccPrimeSquared) * A * A * A * A * A * A / 720));

            if ((thisLat < 0))
                theseUTMcoords.UTMNorthing += 10000000.0; ////10000000 meter offset for southern hemisphere

            return theseUTMcoords;      
        }

        /// <summary> Converts UTM coords to lat/long.  Equations from USGS Bulletin 1532  </summary>  
        public Lat_Long UTMtoLL(double UTMEasting, double UTMNorthing)
        {            
            Lat_Long theseLL = new Lat_Long();            

            if ((savedDatumIndex < 0 || savedDatumIndex > 10)) 
                return theseLL;

            Ellipsoid_Shapes thisShape = GetEllipsoidShape(savedDatumIndex);
            double k0 = 0.9996;
            double equatRad = thisShape.equatorialRadius;

            double eccSquared = thisShape.eccentricitySquared;             
            double e1 = (1 - Math.Pow((1 - eccSquared), 0.5)) / (1 + Math.Pow((1 - eccSquared), 0.5));           
                                    
            double douDimble;
            int NorthernHemisphere = 0;   // 1 for northern hemisphere, 0 for southern

            double x = UTMEasting - 500000.0; ////remove 500,000 meter offset for longitude
            double y = UTMNorthing;

            //	//if((*UTMZoneLetter - 'N') >= 0)
            if (hemisphere == "Northern")
                NorthernHemisphere = 1; ////point is in northern hemisphere
            else {
                NorthernHemisphere = 0; ////point is in southern hemisphere
                y -= 10000000.0; ////remove 10,000,000 meter offset used for southern hemisphere
            }

            double longOrigin = (UTMZoneNumber - 1) * 6 - 180 + 3;  ////+3 puts origin in middle of zone

            double eccPrimeSquared = (eccSquared) / (1 - eccSquared);

            double M = y / k0;
            double mu = M / (equatRad * (1 - eccSquared / 4 - 3 * eccSquared * eccSquared / 64 - 5 * eccSquared * eccSquared * eccSquared / 256));

            double phi1Rad = mu + (3 * e1 / 2 - 27 * e1 * e1 * e1 / 32) * Math.Sin(2 * mu)
                    + (21 * e1 * e1 / 16 - 55 * e1 * e1 * e1 * e1 / 32) * Math.Sin(4 * mu)
                    + (151 * e1 * e1 * e1 / 96) * Math.Sin(6 * mu);
            double phi1 = phi1Rad * rad2deg;

            double N1 = equatRad / Math.Pow((1 - eccSquared * Math.Sin(phi1Rad) * Math.Sin(phi1Rad)), 0.5);
            double T1 = Math.Tan(phi1Rad) * Math.Tan(phi1Rad);
            double C1 = eccPrimeSquared * Math.Cos(phi1Rad) * Math.Cos(phi1Rad);
            double R1 = equatRad * (1 - eccSquared) / Math.Pow(1 - eccSquared * Math.Sin(phi1Rad) * Math.Sin(phi1Rad), 1.5);
            double D = x / (N1 * k0);

            theseLL.latitude = phi1Rad - (N1 * Math.Tan(phi1Rad) / R1) * (D * D / 2 - (5 + 3 * T1 + 10 * C1 - 4 * C1 * C1 - 9 * eccPrimeSquared) * D * D * D * D / 24
                     + (61 + 90 * T1 + 298 * C1 + 45 * T1 * T1 - 252 * eccPrimeSquared - 3 * C1 * C1) * D * D * D * D * D * D / 720);
            theseLL.latitude = theseLL.latitude * rad2deg;

            theseLL.longitude = (D - (1 + 2 * T1 + C1) * D * D * D / 6 + (5 - 2 * C1 + 28 * T1 - 3 * C1 * C1 + 8 * eccPrimeSquared + 24 * T1 * T1)
                    * D * D * D * D * D / 120) / Math.Cos(phi1Rad);
            theseLL.longitude = longOrigin + theseLL.longitude * rad2deg;

            return theseLL;
        }

        /// <summary> Returns UTC offset (in hours) at specified lat/long  </summary>  
        public int GetUTC_Offset(double thisLat, double thisLong)
        {           
            string ianaTimeZone = TimeZoneLookup.GetTimeZone(thisLat, thisLong).Result;
            string windowsTimeZone = TimeZoneConverter.TZConvert.IanaToWindows(ianaTimeZone);

            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZone);
            int offset = timeZoneInfo.BaseUtcOffset.Hours;
                        
            return offset;
        }
    }
}
