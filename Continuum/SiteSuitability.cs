using MathNet.Numerics;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.IO;
using System.Security.Policy;
using System.Windows.Forms;
using static ContinuumNS.SiteSuitability;
using static IronPython.Modules._ast;
using System.Xml.Linq;

namespace ContinuumNS
{
    /// <summary> Class that holds information and function to generate site suitability models which include: shadow flicker, ice throw, and turbine sound. </summary>
    [Serializable()]
    public class SiteSuitability
    {
        // Ice Throw Model constants
        /// <summary> Acceleration due to gravity. (-9.80665 m/s^2) </summary>
        double gravity = -9.80665;
        /// <summary> Air density for ice throw model (1.225 kg/m^3). </summary>
        double airDensity = 1.225;
        /// <summary> Ice density (917.9 kg/m^3). </summary>
        double iceDensity = 917.9;        
        /// <summary> Largest ice mass (2.5 kg). </summary>
        double largestIce = 2.5;
        /// <summary> Smallest ice mass (0.025 kg). </summary>
        double smallestIce = 0.025;
        /// <summary> Minimum ice shape parameter (0.4). </summary>
        double shapeLow = 0.4;
        /// <summary> Maximum ice shape parameter. (6) </summary>
        double shapeHigh = 6;
        /// <summary> Number of ice throws per turbine per year. Default is 300, user-defined. </summary>
        public int iceThrowsPerIceDay = 300;
        /// <summary> Number of ice days per year. Default is 5, User-defined. </summary>
        public int numIceDaysPerYear = 5;
        /// <summary> Number of years to model. Default = 20. </summary>
        public int numYearsToModel = 20;
        /// <summary> List of yearly ice hits. Each entry holds the final positions of each ice throw for a year. </summary>
        public YearlyHits[] yearlyIceHits = new YearlyHits[0];

        // Shadow Flicker constants
        /// <summary> Shadow flicker map minimum UTM values. </summary>
        public TopoInfo.UTM_X_Y mapMinBounds = new TopoInfo.UTM_X_Y();
        /// <summary> Shadow flicker map maximum UTM values. </summary>
        public TopoInfo.UTM_X_Y mapMaxBounds = new TopoInfo.UTM_X_Y();
        /// <summary> Shadow flicker map grid resolution (250 m). </summary>
        public int flickerGridReso = 250;
        /// <summary> Assumed zone dimensions to use in flicker map calculations (20 m x 20 m) . </summary>
        public int flickerGridDimension = 20;
        /// <summary> Shadow flicker map. </summary>
        public FlickerGrid[] flickerMap = new FlickerGrid[0];
        /// <summary> Number of X grid nodes of Shadow flicker map. </summary>
        public int numXFlicker;
        /// <summary> Number of Y grid nodes of Shadow flicker map. </summary>
        public int numYFlicker;
        /// <summary> Angle from center of sun to its edge based on minimum distance from sun (0.2725 degrees). </summary>
        double sunVariation = 0.2725;

        // Sound Model constants
        /// <summary> Turbine sound level (dBA). </summary>
        public double turbineSound = 105;
        /// <summary> Turbine sound map grid resolution (50 m). </summary>
        public int soundGridReso = 50;
        /// <summary> Number of X grid nodes of Turbine Sound map. </summary>
        public int numXSound;
        /// <summary> Number of Y grid nodes of Turbine Sound map. </summary>
        public int numYSound;
        /// <summary> Sound attenuation, default = 0.005 dBa/m. </summary>
        public double noiseAlpha = 0.005;
        /// <summary> Turbine Sound level map. </summary>
        public double[,] soundMap = new double[0,0];

        // Angle converters
        /// <summary> Factor to convert degrees to radians. </summary>
        double degsToRad = Math.PI / 180;
        /// <summary> Factor to convert radians to degrees. </summary>
        double radToDegs = 180 / Math.PI;
                
        /// <summary> List of zones (i.e. sites of interest) to include in site suitability models </summary>
        public Zone[] zones = new Zone[0];

        /// <summary> Used in IEC terrain complexity calcs. If true, fitted plane is forced through turbine base elevation </summary>
        public bool forceThruTurbBase;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Holds list of yearly ice hits. </summary>
        [Serializable()]
        public struct YearlyHits
        {
            /// <summary> List of yearly ice hits (which includes coordinates of ice throw final position). </summary>
            public FinalPosition[] iceHits; 
        }

        /// <summary> Holds UTM coordinates of final position of an ice throw and name of the turbine where ice was thrown. </summary>
        [Serializable()]
        public struct FinalPosition
        {
            /// <summary> UTMX coordinate. </summary>
            public double thisX;
            /// <summary> UTMY coordinate. </summary>
            public double thisZ;
            /// <summary> Name of turbine that threw the ice. </summary>
            public string turbineName;
        }

        /// <summary> Holds location, elevation, and dimensions of a zone (i.e. site of interest) and shadow flicker results and turbine flicker angles. </summary>
        [Serializable()]
        public struct Zone
        {
            /// <summary> Zone name. </summary>
            public string name;
            /// <summary> Zone latitude. </summary>
            public double latitude;
            /// <summary> Zone longitude. </summary>
            public double longitude;
            /// <summary> Zone elevation. </summary>
            public double elev;
            /// <summary> Zone X dimension. </summary>
            public int xSize;
            /// <summary> Zone Y dimension. </summary>
            public int ySize;
            /// <summary> Shadow flicker statistics at zone. </summary>
            public ShadowFlickerStats flickerStats;
            /// <summary> Azimuth, altitude, and angle variance between zone and each turbine. </summary>
            public FlickerAngles[] flickerAngles;
        }

        /// <summary> Holds a shadow flicker map grid node including shadow flicker statistics and shadow flicker angles. </summary>
        [Serializable()]
        public struct FlickerGrid
        {
            /// <summary> Grid node UTMX coordinate. </summary>
            public double UTMX;
            /// <summary> Grid node UTMY coordinate. </summary>
            public double UTMY;
            /// <summary> Grid node latitude. </summary>
            public double latitude;
            /// <summary> Grid node longitude. </summary>
            public double longitude;
            /// <summary> Grid node elevation. </summary>
            public double elev;
            /// <summary> Grid node shadow flicker statistics. </summary>
            public ShadowFlickerStats flickerStats;
            /// <summary> Azimuth, altitude, and angle variance between grid point and each turbine. </summary>
            public FlickerAngles[] flickerAngles;
        }

        /// <summary> Holds shadow flicker statistics including yearly, monthly, and 12x24 shadow flicker hours and day with maximum shadow flicker. </summary>
        [Serializable()]
        public struct ShadowFlickerStats
        {
            /// <summary> Total shadow flicker minutes per year. </summary>
            public int totalShadowMinsPerYear;
            /// <summary> Total shadow flicker minutes per month. </summary>
            public int[] totalShadowMinsPerMonth;
            /// <summary> Total number of shadow flicker minutes by month and hour, i = month index; j = hour index. </summary>
            public int[,] shadowMins12x24;
            /// <summary> Maximum daily shadow flicker (minutes). </summary>
            public int maxDailyShadowMins;
            /// <summary> Day of maximum shadow flicker. </summary>
            public DateTime maxShadowDay;            
        }

        /// <summary> Holds azimuth, altitude, and angle variance between site and turbine. </summary>
        [Serializable()]
        public struct FlickerAngles
        {
            /// <summary> Azimuth angle between site and turbine. </summary>
            public double targetAzimuthAngle;
            /// <summary> Altitude angle between site and turbine. </summary>
            public double targetAltitudeAngle;
            /// <summary> Angle variance between site and turbine. </summary>
            public double angleVariance;
        }

        /// <summary> Holds sun altitude, azimuth, and a boolean flag stating whether the sun is up. </summary>
        public struct SunPosition
        {
            /// <summary> Sun altitude angle. </summary>
            public double altitude;
            /// <summary> Sun azimuth angle. </summary>
            public double azimuth;
            /// <summary> True if sun is up. </summary>
            public bool isSunUp;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Defines bounds of map to calculate shadow flicker, sizes and initializes flickerMap, and calls BackgroundWorker to run shadow flicker model </summary>
        public void RunShadowFlickerModel(Continuum thisInst)
        {            
            if (zones == null)
            {
                DialogResult goodToGo = MessageBox.Show("No zones have been imported. Are you sure you want to run shadow flicker model? " +
                    "It will be reset if zones are imported later.", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.No)
                    return;
            }

            if (thisInst.topo.gotTopo == false)
            {
                DialogResult goodToGo = MessageBox.Show("No topography data is loaded so flat terrain will be assumed. Do you want to proceed with shadow flicker model?", "Continuum 3", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.No)
                    return;
            }

            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, true);

            // Make sure that elevation has been calculated at turbine sites
            for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
                if (thisInst.turbineList.turbineEsts[i].elev == 0)
                    thisInst.turbineList.turbineEsts[i].elev = thisInst.topo.CalcElevs(thisInst.turbineList.turbineEsts[i].UTMX, thisInst.turbineList.turbineEsts[i].UTMY);

            FindShadowMapBounds(thisInst, false);
                        
            numXFlicker = 1 + (int)((mapMaxBounds.UTMX - mapMinBounds.UTMX) / flickerGridReso);
            numYFlicker = 1 + (int)((mapMaxBounds.UTMY - mapMinBounds.UTMY) / flickerGridReso);
            int numPts = numXFlicker * numYFlicker;

            Array.Resize(ref flickerMap, numPts);
            // Size flickerAngles array which holds azimuth, etc, between turbine and grid node
            for (int i = 0; i < numPts; i++)
                Array.Resize(ref flickerMap[i].flickerAngles, thisInst.turbineList.TurbineCount);
            
            int flickerInd = 0;

            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Site Suitability");                      

            // Assign UTMX/Y to grid points
            for (int i = 0; i < numXFlicker; i++)
                for (int j = 0; j < numYFlicker; j++)
                {                    
                    flickerMap[flickerInd].UTMX = mapMinBounds.UTMX + i * flickerGridReso;
                    flickerMap[flickerInd].UTMY = mapMinBounds.UTMY + j * flickerGridReso;
                    UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(flickerMap[flickerInd].UTMX, flickerMap[flickerInd].UTMY);
                    flickerMap[flickerInd].latitude = theseLL.latitude;
                    flickerMap[flickerInd].longitude = theseLL.longitude;

                    if (thisInst.topo.gotTopo)
                        flickerMap[flickerInd].elev = thisInst.topo.CalcElevs(flickerMap[flickerInd].UTMX, flickerMap[flickerInd].UTMY);

                    flickerMap[flickerInd].flickerStats = new ShadowFlickerStats();
                    flickerMap[flickerInd].flickerStats.shadowMins12x24 = new int[12, 24];
                    flickerMap[flickerInd].flickerStats.totalShadowMinsPerMonth = new int[12];
                                        
                    // Find flicker angles between grid point and each turbine
                    for (int t = 0; t < thisInst.turbineList.TurbineCount; t++)
                    {                       
                        flickerMap[flickerInd].flickerAngles[t] = GetFlickerAngles(thisInst, thisInst.turbineList.turbineEsts[t], flickerMap[flickerInd].latitude, flickerMap[flickerInd].longitude,
                            flickerGridDimension, flickerGridDimension, thisInst.turbineList.turbineEsts[t].elev, thisInst.modeledHeight, powerCurve);
                    }

                    flickerInd++;
                }

            // Find flicker angles between each zone and each turbine. Saved in zones[i].flickerAngles.
            for (int i = 0; i < GetNumZones(); i++)
            {               

                zones[i].flickerAngles = new FlickerAngles[thisInst.turbineList.TurbineCount];
                if (zones[i].elev == 0 && thisInst.topo.gotTopo)
                {
                    UTM_conversion.UTM_coords theseUTM = thisInst.UTM_conversions.LLtoUTM(zones[i].latitude, zones[i].longitude);
                    zones[i].elev = thisInst.topo.CalcElevs(theseUTM.UTMEasting, theseUTM.UTMNorthing);
                }
                    
                for (int j = 0; j < thisInst.turbineList.TurbineCount; j++)
                    zones[i].flickerAngles[j] = GetFlickerAngles(thisInst, thisInst.turbineList.turbineEsts[j], zones[i].latitude, zones[i].longitude, zones[i].xSize, zones[i].ySize, zones[i].elev, thisInst.modeledHeight, powerCurve);

                zones[i].flickerStats.shadowMins12x24 = new int[12, 24];
                zones[i].flickerStats.totalShadowMinsPerMonth = new int[12];
            }            
                        
            // Call BW worker 
            thisInst.BW_worker = new BackgroundWork();
            BackgroundWork.Vars_for_BW varsForBW = new BackgroundWork.Vars_for_BW();
            varsForBW.thisInst = thisInst;
            thisInst.BW_worker.Call_BW_Shadow(varsForBW);
            
        }

        /// <summary> Calculates and returns shadow flicker angles between specified turbine and zone. </summary>
        public FlickerAngles GetFlickerAngles(Continuum thisInst, Turbine thisTurb, double zoneLat, double zoneLong, int xSize, int ySize, double zoneElev, double hubHeight, TurbineCollection.PowerCurve powerCurve)
        {
            FlickerAngles flickerAngles = new FlickerAngles();
            
            UTM_conversion.UTM_coords zoneUTM = thisInst.UTM_conversions.LLtoUTM(zoneLat, zoneLong);
            double xDist = thisTurb.UTMX - zoneUTM.UTMEasting; // 4/25/19 Switching this order to measure FROM turbine TO zone (since solar azimuth is measured FROM sun TO zone)
            double yDist = thisTurb.UTMY - zoneUTM.UTMNorthing;
            double elevDiff = hubHeight + thisTurb.elev - zoneElev;

            flickerAngles.targetAzimuthAngle = (90 - Math.Atan2(yDist, xDist) * radToDegs);

            if (flickerAngles.targetAzimuthAngle > 180)
                flickerAngles.targetAzimuthAngle = flickerAngles.targetAzimuthAngle - 360;

            double distance_to_base = Math.Sqrt(Math.Pow(xDist, 2) + Math.Pow(yDist, 2));
            double distance_to_hub = Math.Sqrt(Math.Pow(distance_to_base, 2) + Math.Pow(elevDiff, 2)); // always positive so angle_variation is always positive

            flickerAngles.targetAltitudeAngle = Math.Atan2(elevDiff, distance_to_base) * radToDegs;
            double largest = xSize;

            if (xSize < ySize)
                largest = ySize;

            flickerAngles.angleVariance = Math.Atan2(powerCurve.RD/2 + (largest / 2), distance_to_hub) * radToDegs + sunVariation;

            return flickerAngles;
        }

        /// <summary> Defines shadow flicker or ice throw map extent based on turbine locations or ice throw final positions. </summary>
        public void FindShadowMapBounds(Continuum thisInst, bool isIceMap)
        {
            double minX = 10000000;
            double minY = 10000000;
            double maxX = 0;
            double maxY = 0;
            TurbineCollection turbList = thisInst.turbineList;
            
            for (int i = 0; i < turbList.TurbineCount; i++)
            {
                if (turbList.turbineEsts[i].UTMX < minX)
                    minX = turbList.turbineEsts[i].UTMX;

                if (turbList.turbineEsts[i].UTMY < minY)
                    minY = turbList.turbineEsts[i].UTMY;

                if (turbList.turbineEsts[i].UTMX > maxX)
                    maxX = turbList.turbineEsts[i].UTMX;

                if (turbList.turbineEsts[i].UTMY > maxY)
                    maxY = turbList.turbineEsts[i].UTMY;
            }

            for (int i = 0; i < GetNumZones(); i++)
            {
                UTM_conversion.UTM_coords theseUTMs = thisInst.UTM_conversions.LLtoUTM(zones[i].latitude, zones[i].longitude);

                if (theseUTMs.UTMEasting < minX)
                    minX = theseUTMs.UTMEasting;

                if (theseUTMs.UTMNorthing < minY)
                    minY = theseUTMs.UTMNorthing;

                if (theseUTMs.UTMEasting > maxX)
                    maxX = theseUTMs.UTMEasting;

                if (theseUTMs.UTMNorthing > maxY)
                    maxY = theseUTMs.UTMNorthing;
            }

            if (isIceMap == true)
            {
                if (thisInst.siteSuitability.yearlyIceHits.Length != 0)
                {
                    int yearToShow = thisInst.cboIcingYear.SelectedIndex;
                    YearlyHits theseHits = thisInst.siteSuitability.yearlyIceHits[yearToShow];

                    for (int i = 0; i < theseHits.iceHits.Length; i++)
                    {
                        if (theseHits.iceHits[i].thisX < minX)
                            minX = theseHits.iceHits[i].thisX;

                        if (theseHits.iceHits[i].thisZ < minY)
                            minY = theseHits.iceHits[i].thisZ;

                        if (theseHits.iceHits[i].thisX > maxX)
                            maxX = theseHits.iceHits[i].thisX;

                        if (theseHits.iceHits[i].thisZ > maxY)
                            maxY = theseHits.iceHits[i].thisZ;


                    }
                }
                
            }

            mapMinBounds.UTMX = minX - 500;
            mapMinBounds.UTMY = minY - 500;
            mapMaxBounds.UTMX = maxX + 500;
            mapMaxBounds.UTMY = maxY + 500;
        }

        /// <summary> Reads in lat/long coordinates and timestamp and returns true if sun is up. </summary>
        public bool isSunUp(DateTime dateTime, int UTC_offset, double thisLat, double thisLong)
        {
            bool sunIsUp = false;
            double julianDay = ((dateTime.Year - 1900) * 365.2422) + (dateTime.DayOfYear + 1) + 2415018.5 + dateTime.Hour / 24.0 + dateTime.Minute / (24.0 * 60) - UTC_offset / 24.0;
            double julianCentury = (julianDay - 2451545) / 36525;

            double time = ((dateTime.Year * 365.25) + dateTime.Day + (dateTime.Hour / 24) + (dateTime.Minute / 1440)) / 36525; // time since epoch as fraction of century
            double L = (280.460 + 36000.769 * julianCentury) % 360; // Mean Longitude - L - ref the Prime Meridian
            double M = 357.528 + julianCentury * (35999.050 - 0.0001537 * julianCentury); // Longitude Anomaly - M - due to a non circular orbit
            double lambda = (L + (1.915 - .005 * julianCentury) * Math.Sin(M * degsToRad) + .02 * Math.Sin(2 * M * degsToRad)) % 360; // True/ecliptic Longitude - in degrees
            double epsilon = 23.452 - .013 * julianCentury; // Obliquity - how much the earth's axis is tilted ref ecliptic = 47" per century

            double alpha = Math.Atan2(Math.Sin(lambda * degsToRad) * Math.Cos(epsilon * degsToRad), Math.Cos(lambda * degsToRad)) * radToDegs; // Solar right Ascension in degrees

            // put alpha in the same quadrant as lambda
            double quadrant = (int)(lambda / 90);
            while ((int)(alpha / 90) != quadrant) alpha += 90;

            double dec = Math.Asin(Math.Sin(lambda * degsToRad) * Math.Sin(epsilon * degsToRad)); //  Solar Declination in radians
            double hourtime = dateTime.Hour - UTC_offset + (dateTime.Minute / 60); // adjusts the UT to local time
            double HA = L - alpha - 180 + 15 * hourtime + thisLong; // Local Solar Hour Angle
                        
            // See if the sun is up
            double SRHA = Math.Acos(-1 * Math.Tan(thisLat * degsToRad) * Math.Tan(dec)) * radToDegs;
            if ((SRHA > -1 * HA) && (SRHA > HA))
                sunIsUp = true;
            else sunIsUp = false;

            return sunIsUp;
        }

        /// <summary> Reads in lat/long coordinates and timestamp and returns sun position (i.e. azimuth, altitude, and angle variance). </summary>
        public SunPosition GetSunPosition(DateTime dateTime, int UTC_offset, double thisLat, double thisLong)
        {
            // 4/24/2019 After translating to C#, the formulas in TAILS were not producing the correct solar altitude or azimuth values
            // These new equations are taken from "NOAA_Solar_Calculations_day.xls" which was downloaded from https://www.esrl.noaa.gov/gmd/grad/solcalc/calcdetails.html
            SunPosition sunPosition = new SunPosition();

      //      double excelDate = (dateTime.Year - 1900) * 365.2422 + dateTime.DayOfYear + 1;
            double julianDay = ((dateTime.Year - 1900) * 365.2422) + (dateTime.DayOfYear + 1) + 2415018.5 + dateTime.Hour / 24.0 + dateTime.Minute / (24.0 * 60) - UTC_offset / 24.0;
            double julianCentury = (julianDay - 2451545) / 36525;
            
       //     double time = ((dateTime.Year * 365.25) + dateTime.DayOfYear + (dateTime.Hour / 24.0) + (dateTime.Minute / 1440.0)) / 36525; // time since epoch as fraction of century THIS IS NOT RIGHT
            double L = (280.460 + 36000.769 * julianCentury) % 360; // Mean Longitude - L - ref the Prime Meridian
            double geomMeanAnom = 357.52911 + julianCentury * (35999.05029 - 0.0001537 * julianCentury); // Geom Mean Anom Sun (deg)
            double eccentricEarthOrbit = 0.016708634 - julianCentury * (0.000042037 + 0.0000001267 * julianCentury);
     //       double sunEqOfCtr = Math.Sin(degsToRad * geomMeanAnom) * (1.914602 - julianCentury * (0.004817 + 0.000014 * julianCentury)) + Math.Sin(degsToRad * 2 * geomMeanAnom)
      //          * (0.019993 - 0.000101 * julianCentury) + Math.Sin(degsToRad * (3 * geomMeanAnom)) * 0.000289; // 

            double M = (357.528 + 35999.050 * julianCentury) % 360; // Longitude Anomaly - M - due to a non circular orbit
            double lambda = (L + (1.915 - .005 * julianCentury) * Math.Sin(M * degsToRad) + .02 * Math.Sin(2 * M * degsToRad)) % 360; // True/ecliptic Longitude - in degrees
            double epsilon = 23.452 - .013 * julianCentury; // Obliquity - how much the earth's axis is tilted ref ecliptic = 47" per century
      //      double alpha = Math.Atan2(Math.Sin(lambda * degsToRad) * Math.Cos(epsilon * degsToRad), Math.Cos(lambda * degsToRad)) * radToDegs; // Solar right Ascension in degrees
            
            // put alpha in the same quadrant as lambda
      //      double quadrant = (int)(lambda / 90);
      //      while ((int)(alpha / 90) != quadrant) alpha += 90;

            double dec = Math.Asin(Math.Sin(lambda * degsToRad) * Math.Sin(epsilon * degsToRad)); //  Solar Declination in radians

            double varY = Math.Tan(epsilon / 2 * degsToRad) * Math.Tan(epsilon / 2 * degsToRad);
            double eqOfTime = 4 * radToDegs * (varY * Math.Sin(2 * degsToRad * (L)) - 2 * eccentricEarthOrbit * Math.Sin(degsToRad * geomMeanAnom) + 4 * eccentricEarthOrbit * varY
                * Math.Sin(degsToRad * geomMeanAnom) * Math.Cos(2 * degsToRad * L) - 0.5 * varY * varY * Math.Sin(4 * degsToRad * L) - 1.25 * eccentricEarthOrbit * eccentricEarthOrbit
                * Math.Sin(2 * degsToRad * geomMeanAnom));

            double trueSolarTime = ((dateTime.Hour / 24.0 + dateTime.Minute / (24.0 * 60)) * 1440 + eqOfTime + 4 * thisLong - 60 * UTC_offset) % 1440;

            double hourAngle = trueSolarTime / 4.0 - 180;
            if (hourAngle < -180) hourAngle = hourAngle + 360;                           

        //    double hourtime = dateTime.Hour - UTC_offset + (dateTime.Minute / 60.0); // adjusts the UT to local time
         //   double HA = L - alpha - 180 + 15 * hourtime + thisLong; // Local Solar Hour Angle

            // Calculate the Local Solar Altitude Angle in degrees
            sunPosition.altitude = Math.Asin(Math.Sin(thisLat * degsToRad) * Math.Sin(dec) + Math.Cos(thisLat * degsToRad) * Math.Cos(dec) * Math.Cos(hourAngle * degsToRad)) * radToDegs;

            // Calculate the Local Solar Azimuth Angle in degrees
       //     sunPosition.azimuth = Math.Atan2(Math.Sin(hourAngle * degsToRad), (Math.Cos(hourAngle * degsToRad) * Math.Sin(thisLat * degsToRad) - Math.Tan(dec) * Math.Cos(thisLat * degsToRad))) * radToDegs;

            // Solar zenith = 90 - altitude... update this
            double solarZenith = radToDegs * (Math.Acos(Math.Sin(degsToRad * thisLat) * Math.Sin(dec) + Math.Cos(degsToRad * thisLat) * Math.Cos(dec) * Math.Cos(degsToRad * hourAngle)));

            if (hourAngle > 0)
                sunPosition.azimuth = (radToDegs * (Math.Acos(((Math.Sin(degsToRad * thisLat) * Math.Cos(degsToRad * solarZenith)) - Math.Sin(dec)) / (Math.Cos(degsToRad * thisLat) *
                    Math.Sin(degsToRad * solarZenith)))) + 180) % 360;
            else
                sunPosition.azimuth = (540 - radToDegs * (Math.Acos(((Math.Sin(degsToRad * thisLat) * Math.Cos(degsToRad * solarZenith)) - Math.Sin(dec)) / 
                    (Math.Cos(degsToRad * thisLat) * Math.Sin(degsToRad * solarZenith))))) % 360;

            if (sunPosition.azimuth > 180)
                sunPosition.azimuth = sunPosition.azimuth - 360;

            // See if the sun is up
            double SRHA = Math.Acos(-1 * Math.Tan(thisLat * degsToRad) * Math.Tan(dec)) * radToDegs;
            if ((SRHA > -1 * hourAngle) && (SRHA > hourAngle))
                sunPosition.isSunUp = true;
            else sunPosition.isSunUp = false;

            return sunPosition;
        }

        /// <summary> Calculates and returns sunrise and sunset time at specified lat/long on specified day. In local time. </summary>
        public DateTime[] GetSunriseAndSunsetTimes(double thisLat, double thisLong, int offset, DateTime thisDate)
        {            
            DateTime[] sunTimes = new DateTime[2];

            double julianDay = ((thisDate.Year - 1900) * 365.2422) + (thisDate.DayOfYear + 1) + 2415018.5 + thisDate.Hour / 24.0 + thisDate.Minute / (24.0 * 60) - offset / 24.0;
            double julianCentury = (julianDay - 2451545) / 36525;

            double geomMeanLong = (280.460 + 36000.769 * julianCentury) % 360; // Mean Longitude - L - ref the Prime Meridian
            double geomMeanAnom = 357.52911 + julianCentury * (35999.05029 - 0.0001537 * julianCentury); // Geom Mean Anom Sun (deg)
            double eccentricEarthOrbit = 0.016708634 - julianCentury * (0.000042037 + 0.0000001267 * julianCentury);
            double M = (357.528 + 35999.050 * julianCentury) % 360; // Longitude Anomaly - M - due to a non circular orbit
            double lambda = (geomMeanLong + (1.915 - .005 * julianCentury) * Math.Sin(M * degsToRad) + .02 * Math.Sin(2 * M * degsToRad)) % 360; // True/ecliptic Longitude - in degrees
            double epsilon = 23.452 - .013 * julianCentury; // Obliquity - how much the earth's axis is tilted ref ecliptic = 47" per century
            double dec = Math.Asin(Math.Sin(lambda * degsToRad) * Math.Sin(epsilon * degsToRad)); //  Solar Declination in radians

            double varY = Math.Tan(epsilon / 2 * degsToRad) * Math.Tan(epsilon / 2 * degsToRad);
            double eqOfTime = 4 * radToDegs * (varY * Math.Sin(2 * degsToRad * (geomMeanLong)) - 2 * eccentricEarthOrbit * Math.Sin(degsToRad * geomMeanAnom) + 4 * eccentricEarthOrbit * varY
                * Math.Sin(degsToRad * geomMeanAnom) * Math.Cos(2 * degsToRad * geomMeanLong) - 0.5 * varY * varY * Math.Sin(4 * degsToRad * geomMeanLong) - 1.25 * eccentricEarthOrbit * eccentricEarthOrbit
                * Math.Sin(2 * degsToRad * geomMeanAnom));


            double HASunrise = radToDegs * Math.Acos(Math.Cos(degsToRad * (90.833)) / (Math.Cos(degsToRad * thisLat) * Math.Cos(dec)) 
                - Math.Tan(degsToRad * thisLat) * Math.Tan(dec));
            double solarNoon = (720 - 4 * thisLong - eqOfTime + offset * 60) / 1440;

            double sunRiseDouble = solarNoon - HASunrise * 4 / 1440;
            double sunSetDouble = solarNoon + HASunrise * 4 / 1440;                       

            sunTimes[0] = DateTime.Today.AddDays(sunRiseDouble);
            sunTimes[1] = DateTime.Today.AddDays(sunSetDouble);

            return sunTimes;
        }

        /// <summary> Returns the number of zones. </summary>
        public int GetNumZones()
        {
            int numZones = 0;

            try
            {
                numZones = zones.Length;
            }
            catch { }

            return numZones;
        }

        /// <summary> Finds and returns total number of ice hits at a specified zone for a specific year. </summary>
        public int GetTotalNumberOfIceHitsAtZone(int yearInd, Zone zone, Continuum thisInst)
        {
            int numberOfHits = 0;
            UTM_conversion.UTM_coords theseUTM = thisInst.UTM_conversions.LLtoUTM(zone.latitude, zone.longitude);
            double zoneMinX = theseUTM.UTMEasting - zone.xSize / 2.0;
            double zoneMaxX = theseUTM.UTMEasting + zone.xSize / 2.0;
            double zoneMinY = theseUTM.UTMNorthing - zone.ySize / 2.0;
            double zoneMaxY = theseUTM.UTMNorthing + zone.ySize / 2.0;
            
            for (int i = 0; i < yearlyIceHits[yearInd].iceHits.Length; i++)
            {
                if (yearlyIceHits[yearInd].iceHits[i].thisX > zoneMinX && yearlyIceHits[yearInd].iceHits[i].thisX < zoneMaxX
                    && yearlyIceHits[yearInd].iceHits[i].thisZ > zoneMinY && yearlyIceHits[yearInd].iceHits[i].thisZ < zoneMaxY)
                    numberOfHits++;
            }

            return numberOfHits;
        }

        /// <summary> Calculates and returns the final position of an ice piece thrown for a specified turbines with specified ice throw parameters. </summary>
        public FinalPosition ModelIceThrow(double degrees, double hubHeight, double elevDiff, double radius, double iceSpeed, double Cd, double iceArea, 
            double iceMass, double thisWS, double thisWD, Turbine thisTurb)
        {
            FinalPosition finalPosition = new FinalPosition();

          //  string fileName = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Ice Throw files\\Ball 2\\Ice Throw.txt";
         //   StreamWriter file = new StreamWriter(fileName);
         //   file.WriteLine("ax, ay, az, vx, vy, vz, x, y, z");

            double phi = degrees * Math.PI / 180.0; // Angle the ice came off the blade in the plane of the blade rotation in radians
            double launchY = hubHeight + Math.Sin(phi) * radius; // Y-axis component of point where ice left blade - reference to center of turbine base
            double launchX = -1 * Math.Cos(phi) * radius; // X-axis component of point where ice left blade
            double launchZ = 0.0; // Z-axis component of point where ice left blade

            double xVelocity = iceSpeed * Math.Sin(phi); // X-axis component of tip speed as ice leaves blade
            double yVelocity = iceSpeed * Math.Cos(phi); // Y-axis component of tip speed as ice leaves blade
            double zVelocity = 0.0; // Z-axis component is perpendicular to tip speed vector so always zero to start

            double Fd = (airDensity * Cd * iceArea) / 2.0; // constant portion of drag force equation
            double k = Fd / iceMass; // constant part of drag acceleration 

            // set up initial conditions for integration
            double vx_old = xVelocity;
            double vy_old = yVelocity;
            double vz_old = zVelocity;
            double x_old = launchX;
            double y_old = launchY;
            double z_old = 0;
            double time2Ground = 0.0;

            double delta_t = 0.1;
            double x_new;
            double y_new;
            double z_new;

          //  double windSpeedX = thisWS * Math.Cos((thisWD + 90) * Math.PI / 180);
          //  double windSpeedZ = -thisWS * Math.Sin((thisWD + 90) * Math.PI / 180);

            do
            { // integrate equation of motion
                double x_drag = k * Math.Pow(vx_old, 2);
                if (vx_old > 0) x_drag *= -1; // drag force is always in opposite direction from movement - wind resistance
                // double x_drag = k * Math.Pow(windSpeedX - vx_old, 2);                
                // if ((windSpeedX - vx_old) > 0) x_drag *= -1; // drag force is always in opposite direction from movement - wind resistance
                double ax_new = x_drag;

                double y_drag = k * Math.Pow(vy_old, 2);
                if (vy_old > 0) y_drag *= -1; // drag force is always in opposite direction from movement - wind resistance
                double ay_new = gravity + y_drag; // gravity is always working in y-axis, drag is from wind resistance

                //  double z_drag = k * Math.Pow((windSpeedZ - vz_old), 2); // Z axis "drag" force is the wind blowing the ice downwind based on diff between windspeed and speed of ice
                //  if ((windSpeedZ - vz_old) < 0) z_drag *= -1;
                //  double az_new = z_drag;
                double az_new = k * Math.Pow((thisWS - vz_old), 2); // Z axis "drag" force is the wind blowing the ice downwind based on diff between windspeed and speed of ice

                double vx_new = vx_old + ax_new * delta_t; // V = Vo + AT
                double vy_new = vy_old + ay_new * delta_t;
                double vz_new = vz_old + az_new * delta_t;

                x_new = x_old + vx_new * delta_t; // S = So + VT
                y_new = y_old + vy_new * delta_t;
                z_new = z_old + vz_new * delta_t;

             //   file.WriteLine(ax_new + "," + ay_new + "," + az_new + "," + vx_new + "," + vy_new + "," + vz_new + "," + x_new + "," + y_new + "," + z_new);

                x_old = x_new; // save values for next time tick calculations
                y_old = y_new;
                z_old = z_new;
                vx_old = vx_new;
                vy_old = vy_new;
                vz_old = vz_new;
                time2Ground += delta_t; // keep track of how long it has been since the ice left the blade - no longer used, but available
            } while (y_new > elevDiff); // stop integration when the piece hits the ground - assumes the ground at impact point is at same level as tower base

            // double directionRadians = thisWD * Math.PI / 180;
            double directionRadians = (thisWD - 180.0) * Math.PI / 180;
            finalPosition.thisX = thisTurb.UTMX + x_new * Math.Cos(directionRadians) + z_new * Math.Sin(directionRadians);
            finalPosition.thisZ = thisTurb.UTMY + z_new * Math.Cos(directionRadians) - x_new * Math.Sin(directionRadians);
            finalPosition.turbineName = thisTurb.name;

         //   file.Close();

            return finalPosition;
        }

        /// <summary> Calculates and returns the drag coefficient of ice piece based on ice piece shape. </summary>
        public double GetCd(double iceShape)
        {
            double Cd;

            if (iceShape <= 1.0)
                Cd = iceShape;
            else
                Cd = 0.055 * iceShape + 0.96;

            return Cd;
        }

        /// <summary> Calculates and returns the cross-sectional area of ice piece based on ice piece shape and mass. </summary>
        public double GetIceCrossSecArea(double iceMass, double iceShape)
        {
            double volume = iceMass / iceDensity;
            double iceCrossArea = volume / Math.Pow((volume / Math.Pow(iceShape, 2)), (1.0 / 3.0));
            return iceCrossArea;
        }

        /// <summary> Calculates and returns the shape of ice piece between the minimum and maximum shape and a random number. </summary>
        public double GetIceShape(double thisRand)
        {
            double thisShape = thisRand * (shapeHigh - shapeLow) + shapeLow;
            return thisShape;
        }

        /// <summary> Calculates and returns the mass of ice piece based on minimum and maximum ice mass and a random number. </summary>
        public double GetIceMass(double thisRand)
        {
            double thisMass = thisRand * (largestIce - smallestIce) + smallestIce;

            return thisMass;
        }

        /// <summary> Calculates and returns the angle the ice came off the blade in the plane of the blade rotation in radians. </summary>
        public int GetDegrees(double thisRand)
        {
            int randMax = 32767; // Max value of random number in C++ (which is language TAILS is written in)
            thisRand = thisRand * randMax;
            int degrees = 0;

            if (thisRand % 1000 < 700)
                degrees = Convert.ToInt32(thisRand % 360);
            else
            {
                double n = thisRand / randMax * 2 - 1;
                double angle = Math.Asin(n);
                degrees = (int)(angle * 180 / Math.PI + 270);
            }            

            return degrees;
        }

        /// <summary> Calculates and returns the Y-axis component of point where ice left blade - reference to center of turbine base. </summary>
        public double GetRandomRadius(double thisRand, TurbineCollection.PowerCurve powerCurve)
        {            
            double randomRadius = thisRand * powerCurve.RD / 2 + 1;

            return randomRadius;
        }

        /// <summary> Calculates and returns the tip speed as ice leaves blade. </summary>
        public double GetTipSpeed(TurbineCollection.PowerCurve powerCurve, double thisWS)
        {            
            TurbineCollection turbList = new TurbineCollection();
            
            double thisPower = turbList.GetInterpPowerOrThrust(thisWS, powerCurve, "Power");
            double tipSpeed = powerCurve.ratedRPM * 2 * Math.PI / 60 * powerCurve.RD / 2 * thisPower / powerCurve.ratedPower;

            return tipSpeed;
        }

        /// <summary> Calculates and returns the speed of the ice as it leaves blade. </summary>
        public double GetIceSpeed(double tipSpeed, double randRadius, TurbineCollection.PowerCurve powerCurve)
        {
            double iceSpeed = tipSpeed * randRadius / (powerCurve.RD / 2);
            return iceSpeed;
        }

        /// <summary> Returns the wind speed cumulative density function for specified wind direction. </summary>
        public double[] GetOneWS_CDF(double[,] theseCDFs, int WD_Ind)
        {
            int numWS = theseCDFs.GetUpperBound(1) + 1;
            double[] thisCDF = new double[numWS];

            for (int i = 0; i <= theseCDFs.GetUpperBound(1); i++)
                thisCDF[i] = theseCDFs[WD_Ind, i];

            return thisCDF;
        }

        /// <summary> Finds and returns random number. </summary>
        public Random GetRandomNumber()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            return rnd;
        }

        /// <summary> Returns wind speed corresponding to random number and CDF. </summary>
        public double FindCDF_WS(double[] thisCDF, double randomNum, MetCollection metList)
        {            
            double minDiff = 100;
            int minInd = 10000;           
            int numWS = thisCDF.Length;
            
            // find CDF index that most closely corresponds to random number
            for (int m = 0; m < numWS; m++)
            {
                double thisDiff = Math.Abs(thisCDF[m] - randomNum);
                if (thisDiff < minDiff)
                {
                    minInd = m;
                    minDiff = thisDiff;
                }
                else if (thisDiff > minDiff)
                    break;
            }

            if (minInd == 10000)
                minInd = numWS - 1;
                        
            double thisWS = metList.WS_FirstInt + minInd * metList.WS_IntSize - metList.WS_IntSize / 2;                

            return thisWS;
        }

        /// <summary> Generates and returns sectorwise wind speed cumulative density functions based on specified sectorwise wind speed distributions. </summary>
        public double[,] GenerateWS_CDFs(double[,] sectorDist)
        {
            int numWD = sectorDist.GetUpperBound(0) + 1;
            int numWS = sectorDist.GetUpperBound(1) + 1;
            double[,] theseCDFs = new double[numWD, numWS];

            for (int i = 0; i < numWD; i++)
            {
                theseCDFs[i, 0] = sectorDist[i, 0];

                for (int m = 1; m < numWS; m++)
                    theseCDFs[i, m] = theseCDFs[i, m - 1] + sectorDist[i, m];

            }

            return theseCDFs;
        }

        /// <summary> Deletes zone from list with specified name. </summary>
        public void DeleteZone(string name)
        {           
            int newCount = GetNumZones() - 1;

            if (newCount > 0)
            {
                Zone[] tempList = new Zone[newCount];                 
                int tempIndex = 0;

                for (int i = 0; i < GetNumZones(); i++)
                {
                    if (zones[i].name != name)
                    {
                        tempList[tempIndex] = zones[i];
                        tempIndex++;
                    }
                }

                zones = tempList;
            }
            else            
                zones = new Zone[0];                
            
        }

        /// <summary> Creates and returns grid array with total number of shadow flicker hours. </summary>
        public FlickerGrid[] GetTotalFlickerHoursByMonthAndHour(int monthInd, int hourInd)
        {                        
            FlickerGrid[] plotFlickerMap = new FlickerGrid[flickerMap.Length];

            for (int i = 0; i < flickerMap.Length; i++)
            {
                plotFlickerMap[i].UTMX = flickerMap[i].UTMX;
                plotFlickerMap[i].UTMY = flickerMap[i].UTMY;                              

                for (int m = 0; m < 12; m++)
                    for (int h = 0; h < 24; h++)
                        if ((monthInd == 100 || monthInd == m) && (hourInd == 100 || hourInd == h))
                            plotFlickerMap[i].flickerStats.totalShadowMinsPerYear = plotFlickerMap[i].flickerStats.totalShadowMinsPerYear + (int)Math.Round(flickerMap[i].flickerStats.shadowMins12x24[m, h] / 60.0, 0);
            }

            // Find max value
            int maxValue = 0;

            for (int i = 0; i < flickerMap.Length; i++)
                if (plotFlickerMap[i].flickerStats.totalShadowMinsPerYear > maxValue)
                    maxValue = plotFlickerMap[i].flickerStats.totalShadowMinsPerYear;

            return plotFlickerMap;
        }

        /// <summary> Calculates and returns the total number of shadow flicker hours at a zone for specified hour and month. All months : monthInd = 100; All hours : hourInd = 100. </summary>
        public double GetTotalFlickerHours(Zone thisZone, int monthInd, int hourInd)
        {            
            double totalHours = 0;

            if (thisZone.flickerStats.shadowMins12x24 == null)
                return totalHours;

            for (int m = 0; m < 12; m++)
                for (int h = 0; h < 24; h++)
                    if ((monthInd == 100 || monthInd == m) && (hourInd == 100 || hourInd == h))
                        totalHours = totalHours + thisZone.flickerStats.shadowMins12x24[m, h] / 60;

            
            return totalHours;

        }

        /// <summary> Calculates and returns the noise level at specified UTMX/Y caused by all turbines </summary>
        public double CalcNoiseLevel(int UTMX, int UTMY, Continuum thisInst)
        {                        
            double noiseAtZone = 0;
            double totalLps = 0;
           
            // calculate the noise level caused by each turbine
            for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
            {
                Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                double distance = thisInst.topo.CalcDistanceBetweenPoints(UTMX, UTMY, thisTurb.UTMX, thisTurb.UTMY);
                double thisLp = turbineSound - 10 * Math.Log10(2 * Math.PI * Math.Pow(distance, 2)) - noiseAlpha * distance;

                if (thisLp < 0)                
                    thisLp = 0;
                
                if (thisLp > 0)
                    totalLps = totalLps + Math.Pow(10, thisLp / 10);
            }

            if (totalLps == 0)            
                noiseAtZone = 0;            
            else            
                noiseAtZone = 10 * Math.Log10(totalLps);                     

            return noiseAtZone;
        }

        /// <summary> Creates turbine sound map. </summary>
        public void CreateSoundMap(Continuum thisInst)
        {            
            if (mapMinBounds.UTMX == 0)
                FindShadowMapBounds(thisInst, false);

            numXSound = Convert.ToInt32((mapMaxBounds.UTMX - mapMinBounds.UTMX) / soundGridReso) + 1;
            numYSound = Convert.ToInt32((mapMaxBounds.UTMY - mapMinBounds.UTMY) / soundGridReso) + 1;

            if (numXSound < 0)
                return;

            soundMap = new double[numXSound, numYSound];

            for (int i = 0; i < numXSound; i++)
                for (int j = 0; j < numYSound; j++)
                {
                    int thisUTMX = Convert.ToInt32(mapMinBounds.UTMX + i * soundGridReso);
                    int thisUTMY = Convert.ToInt32(mapMinBounds.UTMY + j * soundGridReso);
                    soundMap[i, j] = CalcNoiseLevel(thisUTMX, thisUTMY, thisInst);
                }
        }

        /// <summary> Calculates and returns probability of ice hit versus distance for specified WD. </summary>
        public double[] CalcIceHitVersusDistance(FinalPosition[] iceHits, int WD_Ind, string turbineName, Continuum thisInst)
        {            
            double[] probIceHit = new double[21];

            Turbine thisTurb = thisInst.turbineList.GetTurbine(turbineName);
            int totalHits = 0;                     
                
            for (int j = 0; j < iceHits.Length; j++)
            {
                if (iceHits[j].turbineName == turbineName)
                {
                    double thisDirection = thisInst.topo.GetDirection((iceHits[j].thisX - thisTurb.UTMX), (iceHits[j].thisZ - thisTurb.UTMY));
                    int thisWD_Ind = thisInst.metList.GetWD_Ind(thisDirection);

                    if (thisWD_Ind == WD_Ind || WD_Ind == thisInst.metList.numWD)
                    {
                        double thisDist = thisInst.topo.CalcDistanceBetweenPoints(iceHits[j].thisX, iceHits[j].thisZ, thisTurb.UTMX, thisTurb.UTMY);
                        int distInd = (int)Math.Round(thisDist / 50, 0);

                        if (distInd > 20)
                            distInd = 20;

                        if (distInd == 0)
                            distInd = 0;

                        probIceHit[distInd]++;                            
                    }

                    totalHits++;
                }
            }
                            

            return probIceHit;

        }

        /// <summary> Clears all ice hit estimates. </summary>
        public void ClearIceThrow()
        {            
            yearlyIceHits = new YearlyHits[0];            
        }

        /// <summary> Clears all shadow flicker estimates. </summary>
        public void ClearShadowFlicker()
        {            
            mapMinBounds = new TopoInfo.UTM_X_Y();
            mapMaxBounds = new TopoInfo.UTM_X_Y();
            numXFlicker = 0;
            numYFlicker = 0;
            flickerMap = new FlickerGrid[0];
            
            for (int i = 0; i < GetNumZones(); i++)
            {
                zones[i].flickerAngles = new FlickerAngles[0];
                zones[i].flickerStats = new ShadowFlickerStats();                
            }
        }

        /// <summary> Clears sound model. </summary>
        public void ClearSound()
        {            
            numXSound = 0;
            numYSound = 0;
            soundMap = new double[0, 0];
        }

        /// <summary> Clears all site suitability models. </summary>
        public void ClearAll()
        {
            ClearIceThrow();
            ClearShadowFlicker();
            ClearSound();
        }

        /// <summary> Clears all zones (sites of interest). </summary>
        public void ClearAllZones()
        {
            zones = new Zone[0];
        }

        /// <summary> Calculates and returns probability of more than minNumHits ice hits in a given year. </summary>
        public double CalcProbabilityOfHits(Zone zone, int minNumHits, Continuum thisInst)
        {            
            double prob = 0;

            // Calculate the histogram
            double[] hitHisto = new double[6]; // Bins are: 0, 1, 2, 3, 4, > 4

            for (int i = 0; i < numYearsToModel; i++)
            {
                int numHits = GetTotalNumberOfIceHitsAtZone(i, zone, thisInst);

                if (numHits > 4)
                    hitHisto[5]++;
                else
                    hitHisto[numHits]++;
            }

            for (int i = 5; i >= minNumHits; i--)
                prob = prob + hitHisto[i];

            if (prob > 0)
                prob = prob / numYearsToModel;

            return prob;
        }

        /// <summary> Calculates and returns either the Terrain Slope Index (TSI) and Terrain Variability Index (TVI) based on IEC 61400-1 Ed 4 terrain complexity. 
        /// The TSI is the average terrain slope measured over a specified radius from the point of interest within a specified wind direction sector.   The TVI is
        /// the avreage standard deviation of the variations in terrain from the best-fit line (divided by radius) within a specified direction sector. If direction
        /// sector is size less than 360, the average TSI or TVI is found using energy rose as the weight.  If direction sector is 360 degs, the average TSI is 
        /// multiplied by 5/3 and the average TVI is divided by 3.</summary>

        public double[] CalcTerrainSlopeAndVariationIndexPerIEC(double UTMX, double UTMY, double elev, double radius, TopoInfo topo, double[] energyRose = null)
        {
            // Calculates and returns either TSI or TVI at specified
            double[] avgSlopeAndVar = new double[2];
            double topoRes =  Math.Min(topo.topoNumXY.X.calcs.reso, topo.topoNumXY.Y.calcs.reso);
            bool inclIntercept = true;

            if (forceThruTurbBase)
                inclIntercept = false;

            if (energyRose == null)
            {
                // 360 degree terrain slope 
                // Loop through all WDs and gather all distance and elevation values in arrays

                int numDataPoints = Convert.ToInt32(360 * radius / topoRes); // Estimates approx number of total points so array resizing is reduced           
                int valInd = 0;

                //      Tuple<double, double>[] utmXandYs = new Tuple<double, double>[numDataPoints];
                double[][] utmXandYs = new double[numDataPoints][];
                double[] elevData = new double[numDataPoints];

          //      StreamWriter sw = new StreamWriter("C:\\Users\\OEE2021_03\\OneDrive - One Energy LLC\\Documents - Analytics\\General\\Renewable Energy Services\\R&D\\Terrain Complexity\\IEC Complexity calcs\\Full Grid output");
                                 
                for (int d = 0; d < 180; d++) // Only going to 180 since elevation profile is NOT just UW of POI, it goes to +/- specified radius
                {
                    TopoInfo.TopoGrid[] elevProfData = topo.GetElevationProfile(UTMX, UTMY, d, (int)radius, (int)topoRes);

                    for (int p = 0;  p < elevProfData.Length; p++)
                    {
                        if (valInd >= numDataPoints)
                        {
                            Array.Resize(ref utmXandYs, valInd + 1);
                            Array.Resize(ref elevData, valInd + 1);
                        }
                        
                        utmXandYs[valInd] = new double[2];
                        utmXandYs[valInd][0] = elevProfData[p].UTMX - UTMX;
                        utmXandYs[valInd][1] = elevProfData[p].UTMY - UTMY;
                        elevData[valInd] = elevProfData[p].elev - elev;
          //              sw.WriteLine(utmXandYs[valInd][0] + "," + utmXandYs[valInd][1] + "," + elevData[valInd]);
                        valInd++;
                        
                    }                    
                }

          //      sw.Close();
                
                // Find avg slope and standard deviation of elevation deviations across 360 degree plane. Multiply slope by 5/3 and divide st. dev. of 
                // elevation deviations by 3 and by radius

                // Find fitted plane by fitting a linear regression using UTM X and Ys and elevation data.  
                double[] regressionResults = MathNet.Numerics.LinearRegression.MultipleRegression.NormalEquations(utmXandYs, elevData, inclIntercept);

                // Calculate slope along centerline of each 30 deg sector based on slopes of fitted plane (i.e. along X and Y axes) and assign maximum (absolute) slope                
                double maxSlope = 0;

                for (int d = 0; d < 12; d++)
                {
                    double thisSlope = topo.CalcSlopeAlongCenterlineOfFittedPlane(regressionResults, radius, d * 30.0, UTMX, UTMY, elev, forceThruTurbBase);

                    if (Math.Abs(thisSlope) > maxSlope)
                        maxSlope = thisSlope;
                }               

                avgSlopeAndVar[0] = maxSlope;

                // Calculate variation of elevation from fitted plane
                avgSlopeAndVar[1] = topo.CalcElevVariationInFittedPlane(regressionResults, utmXandYs, elevData, elev, forceThruTurbBase);

                //      avgSlopeAndVar = topo.CalcSlopeAndVariation(allXVals, allYVals);
                avgSlopeAndVar[0] = avgSlopeAndVar[0] * 5.0 / 3.0;
                avgSlopeAndVar[1] = avgSlopeAndVar[1] / 3.0 / radius;

            }
            else
            {
                // Average TSI as average of sectorwise slope weighted by energy rose
                int numWDs = energyRose.Length;
                double[] sectorSlopes = new double[numWDs];
                double[] sectorVars = new double[numWDs];
                int degsPerSect = (int)360 / numWDs;
                int ptsPerSect = Convert.ToInt32(degsPerSect * radius / topoRes);  // Estimates approx number of total points so array resizing is reduced 
                         
                for (int i = 0; i < numWDs; i++)
                {
                     // Calculate average slope in sector
                    int valInd = 0;
                    int minWD = i * degsPerSect - degsPerSect / 2;
                    int maxWD = i * degsPerSect + degsPerSect / 2;
                    double midWD = i * degsPerSect;

                    double[][] utmXandYs = new double[ptsPerSect][];
                    double[] elevData = new double[ptsPerSect];

                    for (int d = minWD; d <= maxWD; d++) // Get distance and elevation data at every WD within sector (including min and max WD)
                    {
                        TopoInfo.TopoGrid[] elevProfData = topo.GetElevationProfile(UTMX, UTMY, d, (int)radius, (int)topoRes, true); // UW profile only
                        
                        for (int p = 0; p < elevProfData.Length; p++)
                        {
                            if (valInd >= ptsPerSect)
                            {
                                Array.Resize(ref utmXandYs, valInd + 1);
                                Array.Resize(ref elevData, valInd + 1);                                
                            }
                            
                            utmXandYs[valInd] = new double[2];
                            utmXandYs[valInd][0] = elevProfData[p].UTMX - UTMX;
                            utmXandYs[valInd][1] = elevProfData[p].UTMY - UTMY;
                            elevData[valInd] = elevProfData[p].elev - elev;

                            valInd++;                                                        
                        }
                    }                                       

                    // Find avg slope across all 360 degree plane 
                    
                    // Find fitted plane by fitting a linear regression using UTM X and Ys and elevation data.  
                    double[] regressionResults = MathNet.Numerics.LinearRegression.MultipleRegression.NormalEquations(utmXandYs, elevData, inclIntercept);

                    // Calculate slope along centerline based on slopes of fitted plane (i.e. along X and Y axes) 
                    sectorSlopes[i] = topo.CalcSlopeAlongCenterlineOfFittedPlane(regressionResults, radius, midWD, UTMX, UTMY, elev, forceThruTurbBase);

                    // Calculate variation of elevation from fitted plane
                    sectorVars[i] = topo.CalcElevVariationInFittedPlane(regressionResults, utmXandYs, elevData, elev, forceThruTurbBase) / radius;
                                  
                }

                // Combine sectorwise slopes (absolute values?) with energy rose
      //          StreamWriter sw = new StreamWriter("C:\\Users\\OEE2021_03\\OneDrive - One Energy LLC\\Documents - Analytics\\General\\Renewable Energy Services\\R&D\\Terrain Complexity\\IEC Complexity calcs\\5h30 Slopes and Vars.csv");


                for (int i = 0; i < numWDs; i++)
                {
      //             sw.WriteLine(sectorSlopes[i] + "," + sectorVars[i]);
                    avgSlopeAndVar[0] = avgSlopeAndVar[0] + Math.Abs(sectorSlopes[i]) * energyRose[i];
                    avgSlopeAndVar[1] = avgSlopeAndVar[1] + Math.Abs(sectorVars[i]) * energyRose[i];
                }

      //          sw.Close();

            }

            return avgSlopeAndVar;
        }

        /// <summary> Returns array of all turbines' terrain complexity values (for histogram) </summary>
        public double[] GetTerrainComplexityAtAllTurbines(TurbineCollection turbList, TopoInfo topo, string complexMetric, double[] energyRose, double hubHeight)
        {
            double[] turbVals = new double[turbList.TurbineCount];

            for (int t = 0; t < turbList.TurbineCount; t++)
            {
                Turbine thisTurb = turbList.turbineEsts[t];
                double UTMX = turbList.turbineEsts[t].UTMX;
                double UTMY = turbList.turbineEsts[t].UTMY;

                if (complexMetric == "5z 360 TSI")
                    turbVals[t] = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 5 * hubHeight, topo)[0];
                else if (complexMetric == "5z 360 TVI")
                    turbVals[t] = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 5 * hubHeight, topo)[1];
                else if (complexMetric == "5z 30 TSI")
                    turbVals[t] = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 5 * hubHeight, topo, energyRose)[0];
                else if (complexMetric == "5z 30 TVI")
                    turbVals[t] = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 5 * hubHeight, topo, energyRose)[1];
                else if (complexMetric == "10z 30 TSI")
                    turbVals[t] = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 10 * hubHeight, topo, energyRose)[0];
                else if (complexMetric == "10z 30 TVI")
                    turbVals[t] = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 10 * hubHeight, topo, energyRose)[1];
                else if (complexMetric == "20z 30 TSI")
                    turbVals[t] = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 20 * hubHeight, topo, energyRose)[0];
                else if (complexMetric == "20z 30 TVI")
                    turbVals[t] = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 20 * hubHeight, topo, energyRose)[1];
                else if (complexMetric == "P10 UW")
                    turbVals[t] = turbList.turbineEsts[t].gridStats.GetOverallP10(energyRose, 1, "UW");
                else if (complexMetric == "P10 DW")
                    turbVals[t] = turbList.turbineEsts[t].gridStats.GetOverallP10(energyRose, 1, "DW");
            }

            return turbVals;
        }

        /// <summary> Returns terrain complexity level based on IEC 61400-1 ed 4 "logic" </summary>
        
        public string CalcTerrainComplexityPerIEC(TurbineCollection turbList, TopoInfo topo, double hubH, double[] energyRose, string farmOrWTG, Turbine singleSite = null)
        {
            string terrainComplex = "Not Complex";

            // IEC thresholds
            double lowTSI = 10;
            double medTSI = 15;
            double highTSI = 20;

            double lowTVI = 0.02;
            double medTVI = 0.04;
            double highTVI = 0.06;

            double maxTSI = 0;
            double maxTVI = 0;

            // Loop through each turbine and calculate TSI and TVI at 5h over 360 degs and at 5h, 10h, and 20h in 30 deg sectors
            // If any TSI or TVI levels exceed above threshold values, the terrain complexity value is assigned based on highest value

            int numSites = 1;
            if (farmOrWTG == "Farm")
                numSites = turbList.TurbineCount;
                                   

            for (int t = 0; t < numSites; t++)
            {
                Turbine thisTurb = turbList.turbineEsts[t];

                if (farmOrWTG != "Farm")
                    thisTurb = singleSite;

                double UTMX = thisTurb.UTMX;
                double UTMY = thisTurb.UTMY;

                // TSI and TVI at 5h and 360 degs
                double[] slopeAndVarInds = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 5 * hubH, topo);

                if (slopeAndVarInds[0] > maxTSI)
                    maxTSI = slopeAndVarInds[0];

                if (slopeAndVarInds[1] > maxTVI)
                    maxTVI = slopeAndVarInds[1];

                // TSI and TVI at 5h and 30 degs (i.e. 12 sectors)
                slopeAndVarInds = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 5 * hubH, topo, energyRose);

                if (slopeAndVarInds[0] > maxTSI)
                    maxTSI = slopeAndVarInds[0];

                if (slopeAndVarInds[1] > maxTVI)
                    maxTVI = slopeAndVarInds[1];

                // TSI and TVI at 10h and 30 degs (i.e. 12 sectors)
                slopeAndVarInds = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 10 * hubH, topo, energyRose);

                if (slopeAndVarInds[0] > maxTSI)
                    maxTSI = slopeAndVarInds[0];

                if (slopeAndVarInds[1] > maxTVI)
                    maxTVI = slopeAndVarInds[1];

                // TSI and TVI at 20h and 30 degs (i.e. 12 sectors)
                slopeAndVarInds = CalcTerrainSlopeAndVariationIndexPerIEC(UTMX, UTMY, thisTurb.elev, 20 * hubH, topo, energyRose);

                if (slopeAndVarInds[0] > maxTSI)
                    maxTSI = slopeAndVarInds[0];

                if (slopeAndVarInds[1] > maxTVI)
                    maxTVI = slopeAndVarInds[1];
            }

            if (maxTSI > highTSI || maxTVI > highTVI)
                terrainComplex = "High Complex";
            else if (maxTSI > medTSI || maxTVI > medTVI)
                terrainComplex = "Mod. Complex";
            else if (maxTSI > lowTSI || maxTVI > lowTVI)
                terrainComplex = "Low Complex";
            
            return terrainComplex;
        }


    }
}
