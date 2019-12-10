using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class MERRA_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\MERRA";
        string merraFolder = "C:\\Users\\Liz\\Desktop\\MERRA2";

        [TestMethod]
        public void GetMaxHourlyWindSpeeds_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\GetMaxHourlyWindSpeeds\\GetMaxHourlyWindSpeeds test.cfm";
            thisInst.Open(Filename);

            double thisLat = 41.0;
            double thisLong = -83.75;

            MERRA thisMERRA = thisInst.merraList.GetMERRA(thisLat, thisLong);
            thisMERRA.GetMERRADataFromDB(thisInst);
            thisMERRA.GetInterpData(thisInst.UTM_conversions);

            Met.MaxYearlyWind[] theseMaxWS = thisMERRA.GetMaxHourlyWindSpeeds();

            Assert.AreEqual(theseMaxWS[0].maxWS, 19.948, 0.001, "Wrong Max WS Year 2000");
            Assert.AreEqual(theseMaxWS[2].maxWS, 23.496, 0.001, "Wrong Max WS Year 2002");
            Assert.AreEqual(theseMaxWS[6].maxWS, 21.704, 0.001, "Wrong Max WS Year 2006");
            Assert.AreEqual(theseMaxWS[10].maxWS, 20.305, 0.001, "Wrong Max WS Year 2010");

            thisInst.Close();
        }
               

        [TestMethod]
        public void Calc_Avg_or_LT_Test()
        {       

            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MERRA_Testing.cfm";
            thisInst.Open(Filename);

            Met thisMet = thisInst.metList.metItem[0];
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisInst.metList.metItem[0].UTMX, thisInst.metList.metItem[0].UTMY);
            MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);

            double thisAvg = merra.Calc_Avg_or_LT(merra.interpData.TS_Data, 100, 100, "50m WS");
            Assert.AreEqual(6.631771175, thisAvg, 0.001, "Wrong 50m WS all data");
            thisAvg = merra.Calc_Avg_or_LT(merra.interpData.TS_Data, 100, 100, "Surface Pressure");
            Assert.AreEqual(98.78617712, thisAvg, 0.001, "Wrong Pressure");
            thisAvg = merra.Calc_Avg_or_LT(merra.interpData.TS_Data, 100, 100, "10m WS");
            Assert.AreEqual(4.569342609, thisAvg, 0.001, "Wrong 10m WS");
            thisAvg = merra.Calc_Avg_or_LT(merra.interpData.TS_Data, 100, 100, "10m Temp"); // all data
            Assert.AreEqual(11.32908569, thisAvg, 0.001, "Wrong 10m Temp");

            thisAvg = merra.Calc_Avg_or_LT(merra.interpData.TS_Data, 1, 2012, "50m WS"); // jan 2012
            Assert.AreEqual(8.293981183, thisAvg, 0.001, "Wrong 50m WS");

            thisAvg = merra.Calc_Avg_or_LT(merra.interpData.TS_Data, 100, 2012, "50m WS"); // avg 2012
            Assert.AreEqual(6.696877846, thisAvg, 0.001, "Wrong 50 m WS");

            thisAvg = merra.Calc_Avg_or_LT(merra.interpData.TS_Data, 1, 100, "50m WS"); // avg all january's
            Assert.AreEqual(7.182093638, thisAvg, 0.001, "Wrong 50m WS");

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Wind_Rose_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MERRA_Testing.cfm";
            thisInst.Open(Filename);

            Met thisMet = thisInst.metList.metItem[0];
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisInst.metList.metItem[0].UTMX, thisInst.metList.metItem[0].UTMY);
            MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);

            double[] thisWR = merra.Calc_Wind_Rose(100, 100, thisInst.UTM_conversions); // all years all months
            Assert.AreEqual(0.063526, thisWR[0], 0.001);
            Assert.AreEqual(0.054934611, thisWR[1], 0.001);
            Assert.AreEqual(0.04584854, thisWR[2], 0.001);
            Assert.AreEqual(0.042617092, thisWR[3], 0.001);
            Assert.AreEqual(0.041742701, thisWR[4], 0.001);
            Assert.AreEqual(0.043909672, thisWR[5], 0.001);
            Assert.AreEqual(0.048737835, thisWR[6], 0.001);
            Assert.AreEqual(0.048015511, thisWR[7], 0.001);
            Assert.AreEqual(0.074931569, thisWR[8], 0.001);
            Assert.AreEqual(0.111732056, thisWR[9], 0.001);
            Assert.AreEqual(0.08607056, thisWR[10], 0.001);
            Assert.AreEqual(0.072004258, thisWR[11], 0.001);
            Assert.AreEqual(0.077706813, thisWR[12], 0.001);
            Assert.AreEqual(0.075958029, thisWR[13], 0.001);
            Assert.AreEqual(0.060028893, thisWR[14], 0.001);
            Assert.AreEqual(0.052235401, thisWR[15], 0.001);

            thisWR = merra.Calc_Wind_Rose(3, 2012, thisInst.UTM_conversions); // all years all months
            Assert.AreEqual(0.040322581, thisWR[0], 0.001);
            Assert.AreEqual(0.053763441, thisWR[5], 0.001);
            Assert.AreEqual(0.053763441, thisWR[10], 0.001);
            Assert.AreEqual(0.037634409, thisWR[15], 0.001);

            thisWR = merra.Calc_Wind_Rose(6, 100, thisInst.UTM_conversions); // all years all Junes
            Assert.AreEqual(0.064351852, thisWR[0], 0.001);
            Assert.AreEqual(0.049537037, thisWR[5], 0.001);
            Assert.AreEqual(0.111574074, thisWR[10], 0.001);
            Assert.AreEqual(0.050462963, thisWR[15], 0.001);

            thisWR = merra.Calc_Wind_Rose(100, 2011, thisInst.UTM_conversions); // Avg 2011 all months
            Assert.AreEqual(0.06130137, thisWR[0], 0.001);
            Assert.AreEqual(0.044406393, thisWR[5], 0.001);
            Assert.AreEqual(0.095091324, thisWR[10], 0.001);
            Assert.AreEqual(0.047488584, thisWR[15], 0.001);

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_LT_Avg_Prod_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MERRA_Testing.cfm";
            thisInst.Open(Filename);

            Met thisMet = thisInst.metList.metItem[0];
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisInst.metList.metItem[0].UTMX, thisInst.metList.metItem[0].UTMY);
            MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
            merra.Calc_LT_Avg_Prod(ref merra.interpData.Annual_Prod);            

            // Test 1
            double thisProd = merra.interpData.Annual_Prod.LT_Avg;
            Assert.AreEqual(3577940.0, thisProd, 20, "Wrong LT Avg Prod");

            // Test 2
            thisProd = merra.interpData.Annual_Prod.Yearly_Prod[0].prod;
            Assert.AreEqual(3427649.1, thisProd, 20, "Wrong 2010 Prod");

            // Test 3
            Assert.AreEqual(3, merra.interpData.Annual_Prod.Yearly_Prod.Length, "Wrong number of years");
            
            thisInst.Close();
        }

        [TestMethod]
        public void Calc_MERRA2_WS_WD_Test()
        {
            string UVfile = testingFolder + "\\U50_V50_WS.csv";
            StreamReader srUVs = new StreamReader(UVfile);
                                    
            MERRA.East_North_WSs[] theseUVs = new MERRA.East_North_WSs[3];
            MERRA thisMERRA = new MERRA();
            thisMERRA.Size_East_North_WS_Data(ref theseUVs, 2);

            for (int i = 0; i < 3; i++)
            {                
                for (int j = 0; j < 2; j++)
                {
                    string theseUVstr = srUVs.ReadLine();
                    string[] UVs = theseUVstr.Split(',');

                    theseUVs[i].U50[j] = Convert.ToDouble(UVs[0]);
                    theseUVs[i].V50[j] = Convert.ToDouble(UVs[1]);
                }
            }
            
            MERRA.MERRA_Pull[] theseMERRA = new MERRA.MERRA_Pull[3];
            thisMERRA.Calc_MERRA2_WS_WD(ref theseMERRA, theseUVs);                 

            Assert.AreEqual(4.07736, theseMERRA[0].Data[0].WS50m, 0.01, "Wrong WS Node 1 Hour 1");
            Assert.AreEqual(284.9041, theseMERRA[0].Data[0].WD50m, 0.01, "Wrong WD Node 1 Hour 1");

            Assert.AreEqual(5.18769, theseMERRA[0].Data[1].WS50m, 0.01, "Wrong WS Node 1 Hour 2");
            Assert.AreEqual(237.5902, theseMERRA[0].Data[1].WD50m, 0.01, "Wrong WD Node 1 Hour 2");

            Assert.AreEqual(4.7727, theseMERRA[1].Data[0].WS50m, 0.01, "Wrong WS Node 2 Hour 1");
            Assert.AreEqual(81.1243, theseMERRA[1].Data[0].WD50m, 0.01, "Wrong WD Node 2 Hour 1");

            Assert.AreEqual(5.70873, theseMERRA[1].Data[1].WS50m, 0.01, "Wrong WS Node 2 Hour 2");
            Assert.AreEqual(99.0641, theseMERRA[1].Data[1].WD50m, 0.01, "Wrong WD Node 2 Hour 2");

            Assert.AreEqual(1.24520, theseMERRA[2].Data[0].WS50m, 0.01, "Wrong WS Node 3 Hour 1");
            Assert.AreEqual(342.719, theseMERRA[2].Data[0].WD50m, 0.01, "Wrong WD Node 3 Hour 1");

            Assert.AreEqual(5.0064, theseMERRA[2].Data[1].WS50m, 0.01, "Wrong WS Node 3 Hour 2");
            Assert.AreEqual(272.743, theseMERRA[2].Data[1].WD50m, 0.01, "Wrong WD Node 3 Hour 2");

            srUVs.Close();
        }

        [TestMethod]
        public void Calc_MonthProdStats_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MERRA_Testing.cfm";
            thisInst.Open(Filename);

            Met thisMet = thisInst.metList.metItem[0];
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisInst.metList.metItem[0].UTMX, thisInst.metList.metItem[0].UTMY);
            MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
            merra.Calc_MonthProdStats(thisInst.UTM_conversions);

            Assert.AreEqual(350134.7, merra.interpData.Monthly_Prod[0].YearProd[0].prod, 10, "Wrong Prod Jan 2010");
            Assert.AreEqual(515097.7174, merra.interpData.Monthly_Prod[3].YearProd[1].prod, 10, "Wrong Prod April 2011");
            Assert.AreEqual(433854.31, merra.interpData.Monthly_Prod[9].YearProd[2].prod, 10, "Wrong Prod Oct 2012");
            Assert.AreEqual(310891.82, merra.interpData.Monthly_Prod[1].LT_Avg, 10, "Wrong Prod Feb LT Avg");
            Assert.AreEqual(335950.897, merra.interpData.Monthly_Prod[11].LT_Avg, 10, "Wrong Prod Dec LT Avg");
            
            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Perc_Diff_from_LT_Monthly_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MERRA_Testing.cfm";
            thisInst.Open(Filename);

            Met thisMet = thisInst.metList.metItem[0];
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisInst.metList.metItem[0].UTMX, thisInst.metList.metItem[0].UTMY);
            MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);

            double thisDiff = merra.Calc_Perc_Diff_from_LT_Monthly(merra.interpData.Monthly_Prod[0], 2011);
            Assert.AreEqual(-0.2984, thisDiff, 0.001, "Wrong Perc Diff Jan 2011");

            thisDiff = merra.Calc_Perc_Diff_from_LT_Monthly(merra.interpData.Monthly_Prod[4], 2010);
            Assert.AreEqual(0.0783, thisDiff, 0.001, "Wrong Perc Diff May 2010");

            thisDiff = merra.Calc_Perc_Diff_from_LT_Monthly(merra.interpData.Monthly_Prod[11], 2011);
            Assert.AreEqual(-0.0480, thisDiff, 0.001, "Wrong Perc Diff Dec 2012");

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Perc_Diff_from_LT_Yearly_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MERRA_Testing.cfm";
            thisInst.Open(Filename);

            Met thisMet = thisInst.metList.metItem[0];
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisInst.metList.metItem[0].UTMX, thisInst.metList.metItem[0].UTMY);
            MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);

            double This_Diff = merra.Calc_Perc_Diff_from_LT_Yearly(merra.interpData.Annual_Prod, 2010);
            Assert.AreEqual(-0.0420051, This_Diff, 0.001, "Wrong Perc Diff 2010");

            This_Diff = merra.Calc_Perc_Diff_from_LT_Yearly(merra.interpData.Annual_Prod, 2011);
            Assert.AreEqual(0.0282090, This_Diff, 0.001, "Wrong Perc Diff 2011");

            This_Diff = merra.Calc_Perc_Diff_from_LT_Yearly(merra.interpData.Annual_Prod, 2012);
            Assert.AreEqual(0.0137960, This_Diff, 0.001, "Wrong Perc Diff 2012");

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Dev_from_LT_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MERRA_Testing.cfm";
            thisInst.Open(Filename);

            Met thisMet = thisInst.metList.metItem[0];
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisInst.metList.metItem[0].UTMX, thisInst.metList.metItem[0].UTMY);
            MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);

            double thisDev = merra.Calc_Dev_from_LT(merra.interpData.Monthly_Prod, merra.interpData.Annual_Prod, 2010, 11);
            Assert.AreEqual(-0.0412, thisDev, 0.001, "Wrong deviation in Nov 2010");

            thisDev = merra.Calc_Dev_from_LT(merra.interpData.Monthly_Prod, merra.interpData.Annual_Prod, 2010, 100);
            Assert.AreEqual(-0.0420051, thisDev, 0.001, "Wrong deviation in 2010");
                        
            thisInst.Close();
        }

        [TestMethod]
        public void Calc_CF_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MERRA_Testing.cfm";
            thisInst.Open(Filename);

            Met thisMet = thisInst.metList.metItem[0];
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisInst.metList.metItem[0].UTMX, thisInst.metList.metItem[0].UTMY);
            MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0];

            Assert.AreEqual(0.40860, merra.Calc_CF(456, 1, 1999, powerCurve), 0.001, "Wrong CF 31 days");
            Assert.AreEqual(0.45238, merra.Calc_CF(456, 2, 1999, powerCurve), 0.001, "Wrong CF 28 days");
            Assert.AreEqual(0.43678, merra.Calc_CF(456, 2, 2000, powerCurve), 0.001, "Wrong CF 29 days");
            Assert.AreEqual(0.42222, merra.Calc_CF(456, 4, 2000, powerCurve), 0.001, "Wrong CF 30 days");
            Assert.AreEqual(0.34703, merra.Calc_CF(4560, 100, 1999, powerCurve), 0.001, "Wrong CF normal year");
            Assert.AreEqual(0.34608, merra.Calc_CF(4560, 100, 2000, powerCurve), 0.001, "Wrong CF leap year");

            thisInst.Close();
        }
                
        [TestMethod]
        public void Find_MERRA_Coords_Test()
        {
            Continuum thisInst = new Continuum();
            
            string Filename = testingFolder + "\\MERRA_Testing.cfm";
            thisInst.Open(Filename);

            Met thisMet = thisInst.metList.metItem[0];
            UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisInst.metList.metItem[0].UTMX, thisInst.metList.metItem[0].UTMY);
            MERRA merra = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);

            bool Get_MERRA_Coords = merra.Find_MERRA_Coords(merraFolder);            
            Assert.AreEqual(6, merra.MERRA_Nodes[0].XY_ind.X_ind, "Wrong x ind");
            Assert.AreEqual(1, merra.MERRA_Nodes[0].XY_ind.Y_ind, "Wrong y ind");

            merra.numMERRA_Nodes = 4;            
            Array.Resize(ref merra.MERRA_Nodes, merra.numMERRA_Nodes);
            merra.ClearAll();            
            merra.Set_Interp_LatLon_Dates_Offset(41.1, -85.4, thisInst.UTM_conversions.GetUTC_Offset(41.1, -85.4), thisInst);
            
            Get_MERRA_Coords = merra.Find_MERRA_Coords(merraFolder);
            Assert.AreEqual(6, merra.MERRA_Nodes[0].XY_ind.X_ind, "Wrong x ind 0");
            Assert.AreEqual(1, merra.MERRA_Nodes[0].XY_ind.Y_ind, "Wrong y ind 0");
            Assert.AreEqual(7, merra.MERRA_Nodes[1].XY_ind.X_ind, "Wrong x ind 1");
            Assert.AreEqual(1, merra.MERRA_Nodes[1].XY_ind.Y_ind, "Wrong y ind 1");
            Assert.AreEqual(6, merra.MERRA_Nodes[2].XY_ind.X_ind, "Wrong x ind 2");
            Assert.AreEqual(2, merra.MERRA_Nodes[2].XY_ind.Y_ind, "Wrong y ind 2");
            Assert.AreEqual(7, merra.MERRA_Nodes[3].XY_ind.X_ind, "Wrong x ind 3");
            Assert.AreEqual(2, merra.MERRA_Nodes[3].XY_ind.Y_ind, "Wrong y ind 3");

            merra.numMERRA_Nodes = 16;            
            Array.Resize(ref merra.MERRA_Nodes, merra.numMERRA_Nodes);
            merra.ClearAll();
            merra.Set_Interp_LatLon_Dates_Offset(41.1, -85.4, thisInst.UTM_conversions.GetUTC_Offset(41.1, -85.4), thisInst);            
            Get_MERRA_Coords = merra.Find_MERRA_Coords(merraFolder);
            Assert.AreEqual(5, merra.MERRA_Nodes[0].XY_ind.X_ind, "Wrong x ind 0");
            Assert.AreEqual(0, merra.MERRA_Nodes[0].XY_ind.Y_ind, "Wrong y ind 0");
            Assert.AreEqual(6, merra.MERRA_Nodes[9].XY_ind.X_ind, "Wrong x ind 1");
            Assert.AreEqual(2, merra.MERRA_Nodes[9].XY_ind.Y_ind, "Wrong y ind 1");
            Assert.AreEqual(7, merra.MERRA_Nodes[14].XY_ind.X_ind, "Wrong x ind 2");
            Assert.AreEqual(3, merra.MERRA_Nodes[14].XY_ind.Y_ind, "Wrong y ind 2");

            merra.numMERRA_Nodes = 16;            
            Array.Resize(ref merra.MERRA_Nodes, merra.numMERRA_Nodes);
            merra.ClearAll();            
            merra.Set_Interp_LatLon_Dates_Offset(41.1, -85.4, thisInst.UTM_conversions.GetUTC_Offset(41.1, -85.4), thisInst);
            Get_MERRA_Coords = merra.Find_MERRA_Coords(merraFolder);
            Assert.AreEqual(5, merra.MERRA_Nodes[0].XY_ind.X_ind, "Wrong x ind 0 last");
            Assert.AreEqual(7, merra.MERRA_Nodes[0].XY_ind.Y_ind, "Wrong y ind 0");

            thisInst.Close();
        }
                
 /*       [TestMethod]
        public void InterpMERRA_Test()
        {
            MERRA merra = new MERRA();
            merra.numMERRA_Nodes = 4;

            merra.interpData.Coords.latitude = 41.1;
            merra.interpData.Coords.longitude = -83.64;

            UTM_conversion utm = new UTM_conversion();
            utm.savedDatumIndex = 0;

            UTM_conversion.UTM_coords theseUTM = utm.LLtoUTM(41.1, -83.64);
            merra.interpData.UTM.UTMEasting  = theseUTM.UTMEasting;
            merra.interpData.UTM.UTMNorthing = theseUTM.UTMNorthing;
            merra.interpData.UTM.UTMZoneNumber = theseUTM.UTMZoneNumber;

            MERRA.East_North_WSs[] theseUVs = new MERRA.East_North_WSs[4];

            for (int i = 0; i < 4; i++)
            {
                Array.Resize(ref theseUVs[i].U10, 1);
                Array.Resize(ref theseUVs[i].V10, 1);
                Array.Resize(ref theseUVs[i].U50, 1);
                Array.Resize(ref theseUVs[i].V50, 1);
            }

            theseUVs[0].U10[0] = -1.67534f;
            theseUVs[0].V10[0] = -1.6873f;
            theseUVs[0].U50[0] = -3.16597f;
            theseUVs[0].V50[0] = -3.19527f;

            theseUVs[1].U10[0] = -1.1563f;
            theseUVs[1].V10[0] = -1.12871f;
            theseUVs[1].U50[0] = -2.10933f;
            theseUVs[1].V50[0] = -2.10347f;

            theseUVs[2].U10[0] = -2.03374f;
            theseUVs[2].V10[0] = -2.02129f;
            theseUVs[2].U50[0] = -3.26656f;
            theseUVs[2].V50[0] = -3.20504f;

            theseUVs[3].U10[0] = -1.25811f;
            theseUVs[3].V10[0] = -1.30937f;
            theseUVs[3].U50[0] = -2.0522f;
            theseUVs[3].V50[0] = -2.12105f;

            MERRA.MERRA_Pull[] This_MERRA = new MERRA.MERRA_Pull[4];

            This_MERRA[0].Coords.latitude = 41;
            This_MERRA[0].Coords.longitude = -83.75;
            theseUTM = utm.LLtoUTM(41, -83.75);
            This_MERRA[0].UTM.UTMEasting = theseUTM.UTMEasting;
            This_MERRA[0].UTM.UTMNorthing = theseUTM.UTMNorthing;
            This_MERRA[0].UTM.UTMZoneNumber = theseUTM.UTMZoneNumber;
            Array.Resize(ref This_MERRA[0].Data, 1);
            This_MERRA[0].Data[0].Temp10m = 271.094f;
            This_MERRA[0].Data[0].SurfPress = 98659;

            This_MERRA[1].Coords.latitude = 41.5;
            This_MERRA[1].Coords.longitude = -83.75;
            theseUTM = utm.LLtoUTM(41.5, -83.75);
            This_MERRA[1].UTM.UTMEasting = theseUTM.UTMEasting;
            This_MERRA[1].UTM.UTMNorthing = theseUTM.UTMNorthing;
            This_MERRA[1].UTM.UTMZoneNumber = theseUTM.UTMZoneNumber;
            Array.Resize(ref This_MERRA[1].Data, 1);
            This_MERRA[1].Data[0].Temp10m = 271.598f;
            This_MERRA[1].Data[0].SurfPress = 99147;

            This_MERRA[2].Coords.latitude = 41;
            This_MERRA[2].Coords.longitude = -83.125;
            theseUTM = utm.LLtoUTM(41, -83.125);
            This_MERRA[2].UTM.UTMEasting = theseUTM.UTMEasting;
            This_MERRA[2].UTM.UTMNorthing = theseUTM.UTMNorthing;
            This_MERRA[2].UTM.UTMZoneNumber = theseUTM.UTMZoneNumber;
            
            Array.Resize(ref This_MERRA[2].Data, 1);
            This_MERRA[2].Data[0].Temp10m = 270.492f;
            This_MERRA[2].Data[0].SurfPress = 98331;

            This_MERRA[3].Coords.latitude = 41.5;
            This_MERRA[3].Coords.longitude = -83.125;
            theseUTM = utm.LLtoUTM(41.5, -83.125);
            This_MERRA[3].UTM.UTMEasting = theseUTM.UTMEasting;
            This_MERRA[3].UTM.UTMNorthing = theseUTM.UTMNorthing;
            This_MERRA[3].UTM.UTMZoneNumber = theseUTM.UTMZoneNumber;
            Array.Resize(ref This_MERRA[3].Data, 1);
            This_MERRA[3].Data[0].Temp10m = 271.48f;
            This_MERRA[3].Data[0].SurfPress = 99331;
                        
            MERRA.Wind_TS[] This_Wind_TS = merra.InterpMERRA(theseUVs, This_MERRA);

            Assert.AreEqual(This_Wind_TS[0].SurfPress, 98768.98, 0.01, "Wrong interpolated pressure");
            Assert.AreEqual(This_Wind_TS[0].Temp10m, 271.1238, 0.01, "Wrong interpolated temp");            
            Assert.AreEqual(This_Wind_TS[0].WS50m, 4.060930, 0.01, "Wrong interpolated 50m WS");
                       
        }
    */
    }
    
}
