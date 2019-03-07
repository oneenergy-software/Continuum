using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class UTM_conversion
    {
        // lat Long - UTM, UTM - lat Long conversions

        // Tom Lambert generously shared this code with me. I cross-checked it
        // with this website: http://www.uwgb.edu/dutchs/usefuldata/utmformulas.htm
        // and it//s all consistent.The website that he lists is no longer up so
        // I//m using the reference ellipsoids on the uwgb.edu page(11 of them)
        ///*Reference ellipsoids derived from Peter H. Dana//s website- 
        //http://www.utexas.edu/depts/grg/gcraft/notes/datum/elist.html
        //Department of Geography, University of Texas at Austin
        //Internet: pdana@mail.utexas.edu
        //3/22/95

        //Source
        //Defense Mapping Agency. 1987b. DMA Technical Report: Supplement to Department of Defense World Geodetic System
        //1984 Technical Report. Part I and II. Washington, DC: Defense Mapping Agency
        //*/

        public int savedDatumIndex = 100; /// 0 = NAD83/WGS84; 1 = GRS 80; 2 = WGS72 (NASA, DOD) 100 = none selected
        public int UTMZoneNumber = -999;
        public string hemisphere = "";
        double deg2rad = Math.PI / 180;
        double rad2deg = 180 / Math.PI;

        public struct Ellipsoid_Shapes
        {
            public double equatorialRadius;
            public double eccentricitySquared;
        }       

        public struct UTM_coords
        {
            public int UTMZoneNumber;
            public string hemisphere;
            public double UTMNorthing;
            public double UTMEasting;
        }

        public struct Lat_Long
        {
            public double latitude;
            public double longitude;
        }

        public void ResetDefaults()
        { // Clears UTM_zone and datum
            savedDatumIndex = 100;
            UTMZoneNumber = -999;
            hemisphere = "";
        }

        public Ellipsoid_Shapes GetEllipsoidShape(int ellipsoidIndex) // Returns ellipse shapr based on index
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

        public string GetDatumString(int datumInd) // Returns name of datum based on index
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
                                                   

        public UTM_coords LLtoUTM(double thisLat, double thisLong)
        {
            // //converts lat/long to UTM coords.  Equations from USGS Bulletin 1532 
            // //East Longitudes are positive, West longitudes are negative. 
            // //North latitudes are positive, South latitudes are negative
            // //lat and Long are in decimal degrees
            //	//Written by Chuck Gantz- chuck.gantz@globalstar.com
            UTM_coords theseUTMcoords = new UTM_coords();

            if ((savedDatumIndex < 0 || savedDatumIndex > 10))
                return theseUTMcoords;

            Ellipsoid_Shapes thisEllip = GetEllipsoidShape(savedDatumIndex);
            double equatRad = thisEllip.equatorialRadius;
            double eccSquared = thisEllip.eccentricitySquared;
            double k0 = 0.9996f;

            double longOrigin;
            double eccPrimeSquared;
            double N;
            double T;
            double C;
            double A;
            double M;

            //   //Make sure the longitude is between -180.00 .. 179.9
            double longTemp = (thisLong + 180) - (int)((thisLong + 180) / 360) * 360 - 180; //// -180.00 .. 179.9;

            double latRad = thisLat * deg2rad;
            double longRad = longTemp * deg2rad;
            double longOriginRad;

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
            longOrigin = (UTMZoneNumber - 1) * 6 - 180 + 3;  ////+3 puts origin in middle of zone
            longOriginRad = longOrigin * deg2rad;

            ////compute the UTM Zone from the latitude and longitude
            theseUTMcoords.UTMZoneNumber = UTMZoneNumber;
            theseUTMcoords.hemisphere = hemisphere;

            eccPrimeSquared = (eccSquared) / (1 - eccSquared);

            N = equatRad / Math.Pow((1 - eccSquared * Math.Sin(latRad) * Math.Sin(latRad)), 0.5);
            T = Math.Tan(latRad) * Math.Tan(latRad);
            C = eccPrimeSquared * Math.Cos(latRad) * Math.Cos(latRad);
            A = Math.Cos(latRad) * (longRad - longOriginRad);

            M = equatRad * ((1.0f - eccSquared / 4.0f - 3.0f * eccSquared * eccSquared / 64.0f - 5.0f * eccSquared * eccSquared * eccSquared / 256.0f) * latRad
                - (3 * eccSquared / 8 + 3 * eccSquared * eccSquared / 32 + 45 * eccSquared * eccSquared * eccSquared / 1024) * Math.Sin(2 * latRad)
                + (15 * eccSquared * eccSquared / 256 + 45 * eccSquared * eccSquared * eccSquared / 1024) * Math.Sin(4 * latRad)
                - (35 * eccSquared * eccSquared * eccSquared / 3072) * Math.Sin(6 * latRad));

            theseUTMcoords.UTMEasting = k0 * N * (A + (1 - T + C) * A * A * A / 6 + (5 - 18 * T + T * T + 72 * C - 58 * eccPrimeSquared) * A * A * A * A * A / 120) + 500000.0;

            theseUTMcoords.UTMNorthing = k0 * (M + N * Math.Tan(latRad) * (A * A / 2 + (5 - T + 9 * C + 4 * C * C) * A * A * A * A / 24 + (61 - 58 * T + T * T + 600 * C - 330 * eccPrimeSquared) * A * A * A * A * A * A / 720));

            if ((thisLat < 0))
                theseUTMcoords.UTMNorthing += 10000000.0; ////10000000 meter offset for southern hemisphere

            return theseUTMcoords;

      }


        public char UTMLetterDesignator(double lat)
        {
            // //This routine determines the correct UTM letter designator for the given latitude
            // //returns //Z// if latitude is outside the UTM limits of 84N to 80S
            //	//Written by Chuck Gantz- chuck.gantz@globalstar.com
            char letterDesignator;

            //  if ( ((84 >= lat) && (lat >= 0)) ) {
            // letterDesignator = "N"
            // else if ( ((0 > lat) && (lat >= -80)) ) {
            // letterDesignator = "S"

            if (((84 >= lat) && (lat >= 72)))
                letterDesignator = 'X';
            else if (((72 > lat) && (lat >= 64)))
                letterDesignator = 'W';
            else if (((64 > lat) && (lat >= 56)))
                letterDesignator = 'V';
            else if (((56 > lat) && (lat >= 48)))
                letterDesignator = 'U';
            else if (((48 > lat) && (lat >= 40)))
                letterDesignator = 'T';
            else if (((40 > lat) && (lat >= 32)))
                letterDesignator = 'S';
            else if (((32 > lat) && (lat >= 24)))
                letterDesignator = 'R';
            else if (((24 > lat) && (lat >= 16)))
                letterDesignator = 'Q';
            else if (((16 > lat) && (lat >= 8)))
                letterDesignator = 'P';
            else if (((8 > lat) && (lat >= 0)))
                letterDesignator = 'N';
            else if (((0 > lat) && (lat >= -8)))
                letterDesignator = 'M';
            else if (((-8 > lat) && (lat >= -16)))
                letterDesignator = 'L';
            else if (((-16 > lat) && (lat >= -24)))
                letterDesignator = 'K';
            else if (((-24 > lat) && (lat >= -32)))
                letterDesignator = 'J';
            else if (((-32 > lat) && (lat >= -40)))
                letterDesignator = 'H';
            else if (((-40 > lat) && (lat >= -48)))
                letterDesignator = 'G';
            else if (((-48 > lat) && (lat >= -56)))
                letterDesignator = 'F';
            else if (((-56 > lat) && (lat >= -64)))
                letterDesignator = 'E';
            else if (((-64 > lat) && (lat >= -72)))
                letterDesignator = 'D';
            else if (((-72 > lat) && (lat >= -80)))
                letterDesignator = 'C';
            else
                letterDesignator = 'Z'; // //This is here as an error flag to show that the latitude is outside the UTM limits

            return letterDesignator;
        }


        public Lat_Long UTMtoLL(double UTMEasting, double UTMNorthing)
        {

            //   //converts UTM coords to lat/long.  Equations from USGS Bulletin 1532 
            //   //East Longitudes are positive, West longitudes are negative. 
            //   //North latitudes are positive, South latitudes are negative
            //   //lat and Long are in decimal degrees. 
            //	//Written by Chuck Gantz- chuck.gantz@globalstar.com
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
    }
}
