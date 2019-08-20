using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ContinuumNS
{
    [Serializable()]
    public class SiteSuitability
    {
        // Ice Throw Model constants
        double gravity = -9.80665;
        double airDensity = 1.225;
        double iceDensity = 917.9;
        double timeUnit = 0.001;
        double largestIce = 2.5;
        double smallestIce = 0.025;
        double shapeLow = 0.4;
        double shapeHigh = 6;
        public int iceThrowsPerIceDay = 300; // Number of ice throws per turbine per year. Default is 300, user-defined.
        public int numIceDaysPerYear = 5; // Default is 5, User-defined
        public int numYearsToModel = 20; // Constant, not user-defined
        public YearlyHits[] yearlyIceHits = new YearlyHits[0];

        // Shadow Flicker constants
        public TopoInfo.UTM_X_Y mapMinBounds = new TopoInfo.UTM_X_Y();
        public TopoInfo.UTM_X_Y mapMaxBounds = new TopoInfo.UTM_X_Y();
        public int flickerGridReso = 250;
        public int flickerGridDimension = 20; // Assume zone is 20 m x 20 m in flicker map (i.e. close to zone dimensions) 
        public FlickerGrid[] flickerMap = new FlickerGrid[0];
        public int numXFlicker;
        public int numYFlicker;

        // Sound Model constants
        public double turbineSound = 105;
        public int soundGridReso = 50;
        public int numXSound;
        public int numYSound;
        public double noiseAlpha = 0.005;
        public double[,] soundMap = new double[0,0];

        // Angle converters
        double degsToRad = Math.PI / 180;
        double radToDegs = 180 / Math.PI;

        double sunVariation = 0.2725;       // angle from center of sun to its edge based on minimum distance from sun (in degrees)

        public Zone[] zones = new Zone[0];

        [Serializable()]
        public struct YearlyHits
        {
            public FinalPosition[] iceHits; 
        }

        [Serializable()]
        public struct FinalPosition
        {
            public double thisX;
            public double thisZ;
            public string turbineName; // Name of turbine that threw the ice
        }

        [Serializable()]
        public struct Zone
        {
            public string name;
            public double latitude;
            public double longitude;
            public double elev;
            public int xSize;
            public int ySize;
            public ShadowFlickerStats flickerStats; 
            public FlickerAngles[] flickerAngles; // azimuth, altitude, and angle variance between zone and each turbine
        }

        [Serializable()]
        public struct FlickerGrid
        {
            public double UTMX;
            public double UTMY;
            public double latitude;
            public double longitude;
            public double elev;
            public ShadowFlickerStats flickerStats;
            public FlickerAngles[] flickerAngles; // azimuth, altitude, and angle variance between grid point and each turbine
        }

        [Serializable()]
        public struct ShadowFlickerStats
        {
            public int totalShadowMinsPerYear;
            public int[] totalShadowMinsPerMonth;
            public int[,] shadowMins12x24; // total number of shadow flicker minutes, i = month index; j = hour index
            public int maxDailyShadowMins;
            public DateTime maxShadowDay;            
        }

        [Serializable()]
        public struct FlickerAngles
        {
            public double targetAzimuthAngle;
            public double targetAltitudeAngle;
            public double angleVariance;
        }

        public struct SunPosition
        {
            public double altitude;
            public double azimuth;
            public bool isSunUp;
        }

        public void RunShadowFlickerModel(Continuum thisInst)
        {
            // Define bounds of map to calculate shadow flicker and size flickerMap
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

        public FlickerAngles FindMinSolarAngles(Continuum thisInst)
        {
            // Finds and returns min target altitude and azimuth and max angle variance of flicker map
            FlickerAngles minFlickerAngles = new FlickerAngles();
            minFlickerAngles.targetAltitudeAngle = 360; // Finds min azimuth and altitude
            minFlickerAngles.targetAzimuthAngle = 360; 
            minFlickerAngles.angleVariance = 0; // Finding max angle variance for shadow flicker

            int flickerInd = 0;

            if (flickerMap == null)
                return minFlickerAngles;

            for (int i = 0; i < thisInst.siteSuitability.numXFlicker; i++)
                for (int j = 0; j < thisInst.siteSuitability.numYFlicker; j++)
                {
                    FlickerAngles[] theseAngles = thisInst.siteSuitability.flickerMap[flickerInd].flickerAngles;

                    for (int t = 0; t < thisInst.turbineList.TurbineCount; t++)
                    {
                        if (theseAngles[t].targetAltitudeAngle < minFlickerAngles.targetAltitudeAngle)
                            minFlickerAngles.targetAltitudeAngle = theseAngles[t].targetAltitudeAngle;

                        if (theseAngles[t].targetAzimuthAngle < minFlickerAngles.targetAzimuthAngle)
                            minFlickerAngles.targetAzimuthAngle = theseAngles[t].targetAzimuthAngle;

                        if (theseAngles[t].angleVariance > minFlickerAngles.angleVariance)
                            minFlickerAngles.angleVariance = theseAngles[t].angleVariance;

                    }

                    flickerInd++;
                }

            return minFlickerAngles;
        }

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

        public DateTime[] GetSunriseAndSunsetTimes(double thisLat, double thisLong, int offset, DateTime thisDate)
        {
            // Calculates and returns sunrise and sunset time at specified lat/long on specified day. In local time
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


            double HASunrise = radToDegs * (Math.Acos(Math.Cos(degsToRad * (90.833)) / (Math.Cos(degsToRad * (thisLat)) * Math.Cos(dec)) - Math.Tan(degsToRad * (thisLat)) * Math.Tan(dec)));
            double solarNoon = (720 - 4 * thisLong - eqOfTime + offset * 60) / 1440;

            double sunRiseDouble = solarNoon - HASunrise * 4 / 1440;
            double sunSetDouble = solarNoon + HASunrise * 4 / 1440;                       

            sunTimes[0] = DateTime.Today.AddDays(sunRiseDouble);
            sunTimes[1] = DateTime.Today.AddDays(sunSetDouble);

            return sunTimes;
        }
                       

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

            double delta_t = 0.001;
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

        public double GetCd(double iceShape)
        {
            double Cd;

            if (iceShape <= 1.0)
                Cd = iceShape;
            else
                Cd = 0.055 * iceShape + 0.96;

            return Cd;
        }

        public double GetIceCrossSecArea(double iceMass, double iceShape)
        {
            double volume = iceMass / iceDensity;
            double iceCrossArea = volume / Math.Pow((volume / Math.Pow(iceShape, 2)), (1.0 / 3.0));
            return iceCrossArea;
        }

        public double GetIceShape(double thisRand)
        {
            double thisShape = thisRand * (shapeHigh - shapeLow) + shapeLow;
            return thisShape;
        }

        public double GetIceMass(double thisRand)
        {
            double thisMass = thisRand * (largestIce - smallestIce) + smallestIce;

            return thisMass;
        }

        public int GetDegrees(double thisRand)
        {
            // Angle the ice came off the blade in the plane of the blade rotation in radians
            double n = thisRand * 2 - 1;
            double angle = Math.Asin(n);
            int degrees = (int)(angle * 180 / Math.PI + 270);

            return degrees;
        }

        public double GetRandomRadius(double thisRand, TurbineCollection.PowerCurve powerCurve)
        {
            //  Y-axis component of point where ice left blade - reference to center of turbine base
            double randomRadius = thisRand * powerCurve.RD / 2 + 1;

            return randomRadius;
        }

        public double GetTipSpeed(TurbineCollection.PowerCurve powerCurve, double thisWS)
        {
            // tip speed as ice leaves blade
            TurbineCollection turbList = new TurbineCollection();
            
            double thisPower = turbList.GetInterpPowerOrThrust(thisWS, powerCurve, "Power");
            double tipSpeed = powerCurve.ratedRPM * 2 * Math.PI / 60 * powerCurve.RD / 2 * thisPower / powerCurve.ratedPower;

            return tipSpeed;
        }

        public double GetIceSpeed(double tipSpeed, double randRadius, TurbineCollection.PowerCurve powerCurve)
        {
            double iceSpeed = tipSpeed * randRadius / (powerCurve.RD / 2);
            return iceSpeed;
        }

        public double[] GetOneWS_CDF(double[,] theseCDFs, int WD_Ind)
        {
            int numWS = theseCDFs.GetUpperBound(1) + 1;
            double[] thisCDF = new double[numWS];

            for (int i = 0; i <= theseCDFs.GetUpperBound(1); i++)
                thisCDF[i] = theseCDFs[WD_Ind, i];

            return thisCDF;
        }

        public Random GetRandomNumber()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            return rnd;
        }

        public double FindCDF_WS(double[] thisCDF, double randomNum, MetCollection metList)
        {
            // Returns wind speed corresponding to random number on CDF

            double minDiff = 100;
            int minInd = 10000;
            double thisWS = 0;
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
                        
            thisWS = metList.WS_FirstInt + minInd * metList.WS_IntSize - metList.WS_IntSize / 2;                

            return thisWS;
        }

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

        public void DeleteZone(string name)
        {
            // Deletes turbine object with specified name
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

        public FlickerGrid[] GetTotalFlickerHoursByMonthAndHour(int monthInd, int hourInd)
        {
            // Creates and returns grid array with total number of shadow flicker hours            
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

        public double GetTotalFlickerHours(Zone thisZone, int monthInd, int hourInd)
        {
            // Calculates and returns the total number of shadow flicker hours at a zone for specified hour and month
            // All months : monthInd = 100; All hours : hourInd = 100

            double totalHours = 0;

            if (thisZone.flickerStats.shadowMins12x24 == null)
                return totalHours;

            for (int m = 0; m < 12; m++)
                for (int h = 0; h < 24; h++)
                    if ((monthInd == 100 || monthInd == m) && (hourInd == 100 || hourInd == h))
                        totalHours = totalHours + thisZone.flickerStats.shadowMins12x24[m, h] / 60;

            
            return totalHours;

        }

        public double CalcNoiseLevel(int UTMX, int UTMY, Continuum thisInst)
        {
            // Calculates and returns the noise level at specified UTMX/Y caused by all turbines
            
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
            {
                noiseAtZone = 0;
            }
            else
            {
                noiseAtZone = 10 * Math.Log10(totalLps);
            }
            

            return noiseAtZone;
        }

        public void CreateSoundMap(Continuum thisInst)
        {
            // Creates sound map

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

        public double[] CalcIceHitVersusDistance(FinalPosition[] iceHits, int WD_Ind, string turbineName, Continuum thisInst)
        {
            // Calculates and returns probability of ice hit versus distance for specified WD

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

        public void ClearIceThrow()
        {
            // Clears all ice hit estimates
            yearlyIceHits = new YearlyHits[0];            
        }

        public void ClearShadowFlicker()
        {
            // Clears all shadow flicker estimates
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

        public void ClearSound()
        {
            // Clears sound model
            numXSound = 0;
            numYSound = 0;
            soundMap = new double[0, 0];
        }
        
        public void ClearAll()
        {
            ClearIceThrow();
            ClearShadowFlicker();
            ClearSound();
        }

        public void ClearAllZones()
        {
            zones = new Zone[0];
        }

        public double CalcProbabilityOfHits(Zone zone, int minNumHits, Continuum thisInst)
        {
            // Calculates and returns probability of more than minNumHits ice hits in a given year
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

    }
}
