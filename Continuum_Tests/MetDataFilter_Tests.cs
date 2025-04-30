using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;
using System.Threading;
using System.Windows.Forms.VisualStyles;

namespace Continuum_Tests
{

    [TestClass]
    public class MetDataFilter_Tests
    {
        Globals globals = new Globals();
        string testingFolder;

        public MetDataFilter_Tests()
        {
            testingFolder = globals.testingFolder + "MetDataFilter";
        }

        [TestMethod]
        public void ConvertToMPS_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter New Bremen testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            Met_Data_Filter thisQC = thisInst.metList.metItem[0].metData;

            Assert.AreEqual(thisQC.anems[0].windData[20].avg, 1.56459544, 0.001, "Wind speed not converted to m/s correctly");
            Assert.AreEqual(thisQC.anems[0].windData[20].SD, 0.447027269, 0.001, "Wind speed not converted to m/s correctly");
            Assert.AreEqual(thisQC.anems[0].windData[20].max, 2.637460885, 0.001, "Wind speed not converted to m/s correctly");
            Assert.AreEqual(thisQC.anems[0].windData[20].min, 0.357621815, 0.001, "Wind speed not converted to m/s correctly");

        }

        [TestMethod]
        public void GetAvgAlpha_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];
            double[] theseAvgAlphas = thisMet.metData.GetAvgAlpha(0, 24, 1);
            Assert.AreEqual(theseAvgAlphas[0], 0.297582647, 0.001, "Wrong average alpha for Test 1");

            thisMet.metData.ClearFilterFlagsAndEstimatedData();
            string[] filtsToApply = new string[1];
            filtsToApply[0] = "All";
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.startDate = Convert.ToDateTime("8/1/2008");
            thisMet.metData.endDate = Convert.ToDateTime("10/31/2008");
            thisMet.metData.EstimateAlpha();
            theseAvgAlphas = thisMet.metData.GetAvgAlpha(0, 24, 1);
            Assert.AreEqual(theseAvgAlphas[0], 0.320117958, 0.001, "Wrong average alpha for Test 2");

            thisMet.metData.ClearFilterFlagsAndEstimatedData();
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.startDate = Convert.ToDateTime("6/25/2008");
            thisMet.metData.endDate = Convert.ToDateTime("6/24/2009 23:50");
            thisMet.metData.EstimateAlpha();
            theseAvgAlphas = thisMet.metData.GetAvgAlpha(7, 9, 1);
            Assert.AreEqual(theseAvgAlphas[0], 0.33187482, 0.001, "Wrong average alpha for Test 3");

            thisMet.metData.ClearFilterFlagsAndEstimatedData();
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.startDate = Convert.ToDateTime("6/25/2008");
            thisMet.metData.endDate = Convert.ToDateTime("6/24/2009 23:50");
            thisMet.metData.EstimateAlpha();
            theseAvgAlphas = thisMet.metData.GetAvgAlpha(12, 23, 16);
            Assert.AreEqual(theseAvgAlphas[2], 0.160170555, 0.001, "Wrong average alpha for Test 4");

            thisMet.metData.ClearFilterFlagsAndEstimatedData();
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.startDate = Convert.ToDateTime("1/1/2009");
            thisMet.metData.endDate = Convert.ToDateTime("3/15/2009");
            thisMet.metData.EstimateAlpha();
            theseAvgAlphas = thisMet.metData.GetAvgAlpha(0, 2, 1);
            Assert.AreEqual(theseAvgAlphas[0], 0.347682384, 0.001, "Wrong average alpha for Test 5");

            thisMet.metData.ClearFilterFlagsAndEstimatedData();
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.startDate = Convert.ToDateTime("12/30/2008");
            thisMet.metData.endDate = Convert.ToDateTime("2/1/2009");
            thisMet.metData.EstimateAlpha();
            theseAvgAlphas = thisMet.metData.GetAvgAlpha(3, 3, 16);
            Assert.AreEqual(theseAvgAlphas[15], 0.400452263, 0.001, "Wrong average alpha for Test 6");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcAvgWS_by_WD_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];
            Met_Data_Filter.Anem_Data anem = thisMet.metData.anems[0];
            Met_Data_Filter.Vane_Data vane = thisMet.metData.vanes[0];
            double[] thisWS = thisMet.metData.Calc_Avg_WS_by_WD(Convert.ToDateTime("6/24/2008 15:00"), Convert.ToDateTime("6/30/2009 23:50"), anem, vane, "Filtered");
            Assert.AreEqual(thisWS[12], 4.09828434, 0.0001, "Wrong avg WS Test 1");

            thisWS = thisMet.metData.Calc_Avg_WS_by_WD(Convert.ToDateTime("10/28/2008 19:20"), Convert.ToDateTime("4/15/2009 2:50"), anem, vane, "Unfiltered");
            Assert.AreEqual(thisWS[63], 4.670447715, 0.0001, "Wrong avg WS Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Wind_Rose_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            Met_Data_Filter.Vane_Data vane = thisMet.metData.vanes[0];
            double[] thisWR = thisMet.metData.Calc_Wind_Rose(Convert.ToDateTime("6/24/2008 15:00"), Convert.ToDateTime("6/30/2009 23:50"), vane, "Filtered");
            Assert.AreEqual(thisWR[4], 0.066466684, 0.0001, "Wrong wind rose Test 1");

            thisWR = thisMet.metData.Calc_Wind_Rose(Convert.ToDateTime("11/2/2008 2:40"), Convert.ToDateTime("2/2/2009 13:00"), vane, "Filtered");
            Assert.AreEqual(thisWR[10], 0.143694678, 0.0001, "Wrong wind rose Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void Get_WS_Ratio_or_Diff_and_WD_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            Met_Data_Filter.WS_Ratio_WD_Data theseCalcs = new Met_Data_Filter.WS_Ratio_WD_Data();

            // 30m Anem tests
            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWD(30, "Unfiltered", "Ratio", true);
            Assert.AreEqual(theseCalcs.WS_Ratio[2468], 0.96742, 0.001, "Wrong value for Test 1");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWD(30, "Unfiltered", "Diff", true);
            Assert.AreEqual(theseCalcs.WS_Ratio[49], 0, 0.001, "Wrong value for Test 2");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWD(30, "Filtered", "Ratio", true);
            Assert.AreEqual(theseCalcs.WD[1567], 199.6, 0.001, "Wrong value for Test 3");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWD(30, "Filtered", "Diff", true);
            Assert.AreEqual(theseCalcs.WS_Ratio[15559], -0.492, 0.001, "Wrong value for Test 4");

            thisMet.metData.startDate = Convert.ToDateTime("1/1/2009 22:00");
            thisMet.metData.endDate = Convert.ToDateTime("4/3/2009 8:00");

            // 50m Anem Tests
            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWD(50, "Unfiltered", "Ratio", false);
            Assert.AreEqual(theseCalcs.WD[1687], 315, 0.001, "Wrong value for Test 5");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWD(50, "Unfiltered", "Diff", false);
            Assert.AreEqual(theseCalcs.WS_Ratio[6874], 0.134, 0.001, "Wrong value for Test 6");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWD(50, "Filtered", "Ratio", false);
            Assert.AreEqual(theseCalcs.WS_Ratio[2198], 1.020134, 0.001, "Wrong value for Test 7");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWD(50, "Filtered", "Diff", false);
            Assert.AreEqual(theseCalcs.WD[357], 253, 0.001, "Wrong value for Test 8");

            thisInst.Close();
        }

        [TestMethod]
        public void Get_WS_Ratio_or_Diff_and_WS_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            Met_Data_Filter.WS_Ratio_WS_Data theseCalcs = new Met_Data_Filter.WS_Ratio_WS_Data();

            // 30m Anem tests
            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWS(30, "Unfiltered", "Ratio");
            Assert.AreEqual(theseCalcs.WS_Ratio[2468], 0.96742, 0.001, "Wrong value for Test 1");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWS(30, "Unfiltered", "Diff");
            Assert.AreEqual(theseCalcs.WS_Ratio[49], 0, 0.001, "Wrong value for Test 2");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWS(30, "Filtered", "Ratio");
            Assert.AreEqual(theseCalcs.WS[1567], 2.816, 0.001, "Wrong value for Test 3");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWS(30, "Filtered", "Diff");
            Assert.AreEqual(theseCalcs.WS_Ratio[15559], -0.582, 0.001, "Wrong value for Test 4");

            thisMet.metData.startDate = Convert.ToDateTime("1/1/2009 22:00");
            thisMet.metData.endDate = Convert.ToDateTime("4/3/2009 8:00");

            // 50m Anem Tests
            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWS(50, "Unfiltered", "Ratio");
            Assert.AreEqual(theseCalcs.WS[1687], 8.896, 0.001, "Wrong value for Test 5");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWS(50, "Unfiltered", "Diff");
            Assert.AreEqual(theseCalcs.WS_Ratio[6874], 0.134, 0.001, "Wrong value for Test 6");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWS(50, "Filtered", "Ratio");
            Assert.AreEqual(theseCalcs.WS_Ratio[2198], 0.983128, 0.001, "Wrong value for Test 7");

            theseCalcs = thisMet.metData.GetWS_RatioOrDiffAndWS(50, "Filtered", "Diff");
            Assert.AreEqual(theseCalcs.WS[357], 4.873, 0.001, "Wrong value for Test 8");

            thisInst.Close();
        }

        [TestMethod]
        public void Get_Conc_WS_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];
            Met_Data_Filter.Anem_Data anemA = new Met_Data_Filter.Anem_Data();
            Met_Data_Filter.Anem_Data anemB = new Met_Data_Filter.Anem_Data();

            thisMet.metData.startDate = Convert.ToDateTime("1/6/2009 3:00");
            thisMet.metData.endDate = Convert.ToDateTime("4/15/2009 16:00");

            Met_Data_Filter.Conc_WS_Data thisConcData = thisMet.metData.GetConcWS(30, "Filtered", thisInst, thisMet.name, anemA, anemB);
            Assert.AreEqual(thisConcData.anemA_WS.Length, 11166, 0, "Wrong number in Test 1");

            thisMet.metData.startDate = Convert.ToDateTime("12/15/2008 18:00");
            thisMet.metData.endDate = Convert.ToDateTime("2/16/2009 4:00");

            thisConcData = thisMet.metData.GetConcWS(40, "Filtered", thisInst, thisMet.name, anemA, anemB);
            Assert.AreEqual(thisConcData.anemA_WS.Length, 7369, 0, "Wrong number in Test 2");

            thisMet.metData.startDate = Convert.ToDateTime("3/3/2009 22:00");
            thisMet.metData.endDate = Convert.ToDateTime("3/31/2009 1:00");

            thisConcData = thisMet.metData.GetConcWS(50, "Filtered", thisInst, thisMet.name, anemA, anemB);
            Assert.AreEqual(thisConcData.anemA_WS.Length, 2895, 0, "Wrong number in Test 3");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcAvgExtrapolatedWS_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];
            string[] filtsToApply = new string[1];
            filtsToApply[0] = "All";

            thisMet.metData.startDate = Convert.ToDateTime("6/24/2008 15:00");
            thisMet.metData.endDate = Convert.ToDateTime("1/22/2009 18:30");
            thisMet.metData.EstimateAlpha();

            // Test 1
            thisMet.metData.ExtrapolateData(80);
            double thisAvgWS = thisMet.metData.CalcAvgExtrapolatedWS(thisMet.metData.GetSimulatedTimeSeries(80));
            Assert.AreEqual(thisAvgWS, 5.67062, 0.001, "Wrong WS Test 1");

            // Test 2
            thisMet.metData.ClearFilterFlagsAndEstimatedData();
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.startDate = Convert.ToDateTime("10/13/2008 21:30");
            thisMet.metData.endDate = Convert.ToDateTime("6/30/2009 23:50");
            thisMet.metData.EstimateAlpha();
            thisMet.metData.ExtrapolateData(100);
            thisAvgWS = thisMet.metData.CalcAvgExtrapolatedWS(thisMet.metData.GetSimulatedTimeSeries(100));
            Assert.AreEqual(thisAvgWS, 6.69379, 0.001, "Wrong WS Test 2");

            // Test 3
            thisMet.metData.ClearFilterFlagsAndEstimatedData();
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.startDate = Convert.ToDateTime("1/6/2009");
            thisMet.metData.endDate = Convert.ToDateTime("6/1/2009");
            thisMet.metData.EstimateAlpha();
            thisMet.metData.ExtrapolateData(20);
            thisAvgWS = thisMet.metData.CalcAvgExtrapolatedWS(thisMet.metData.GetSimulatedTimeSeries(20));
            Assert.AreEqual(thisAvgWS, 4.6989779, 0.001, "Wrong WS Test 3");

            // Test 4
            thisMet.metData.ClearFilterFlagsAndEstimatedData();
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.startDate = Convert.ToDateTime("1/1/2009 13:00");
            thisMet.metData.endDate = Convert.ToDateTime("1/2/2009 12:00");
            thisMet.metData.EstimateAlpha();
            thisMet.metData.ExtrapolateData(43);
            thisAvgWS = thisMet.metData.CalcAvgExtrapolatedWS(thisMet.metData.GetSimulatedTimeSeries(43));
            Assert.AreEqual(thisAvgWS, 6.422621015, 0.001, "Wrong WS Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcAvgWS_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            Met_Data_Filter.Anem_Data thisAnem = new Met_Data_Filter.Anem_Data();

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == 30 && thisMet.metData.anems[i].ID == 'B')
                {
                    thisAnem = thisMet.metData.anems[i];
                    break;
                }

            double thisAvg = thisMet.metData.CalcAvgWS(thisAnem, false);
            Assert.AreEqual(thisAvg, 4.39565913, 0.001, "Wrong avg 30m unfilt WS Test 1");

            thisAvg = thisMet.metData.CalcAvgWS(thisAnem, true);
            Assert.AreEqual(thisAvg, 4.5700187, 0.001, "Wrong avg 30m filt WS Test 2");

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == 40 && thisMet.metData.anems[i].ID == 'B')
                {
                    thisAnem = thisMet.metData.anems[i];
                    break;
                }

            thisAvg = thisMet.metData.CalcAvgWS(thisAnem, false);
            Assert.AreEqual(thisAvg, 4.68654658, 0.001, "Wrong avg 40m unfilt WS Test 3");

            thisAvg = thisMet.metData.CalcAvgWS(thisAnem, true);
            Assert.AreEqual(thisAvg, 4.85966632, 0.001, "Wrong avg 40m filt WS Test 4");

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == 50 && thisMet.metData.anems[i].ID == 'B')
                {
                    thisAnem = thisMet.metData.anems[i];
                    break;
                }

            thisAvg = thisMet.metData.CalcAvgWS(thisAnem, false);
            Assert.AreEqual(thisAvg, 5.02178275, 0.001, "Wrong avg 50m unfilt WS Test 5");

            thisAvg = thisMet.metData.CalcAvgWS(thisAnem, true);
            Assert.AreEqual(thisAvg, 5.20731192, 0.001, "Wrong avg 50m filt WS Test 6");

            thisMet.metData.startDate = Convert.ToDateTime("10/31/2008");
            thisMet.metData.endDate = Convert.ToDateTime("4/14/2009");

            thisAvg = thisMet.metData.CalcAvgWS(thisMet.metData.anems[0], true);
            Assert.AreEqual(thisAvg, 5.28296651, 0.001, "Wrong avg 30m filt WS Test 7");

            thisAvg = thisMet.metData.CalcAvgWS(thisMet.metData.anems[2], true);
            Assert.AreEqual(thisAvg, 5.540579327, 0.001, "Wrong avg 40m filt WS Test 8");

            thisAvg = thisMet.metData.CalcAvgWS(thisMet.metData.anems[4], true);
            Assert.AreEqual(thisAvg, 5.877799401, 0.001, "Wrong avg 50m filt WS Test 9");

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Anem_Data_Recovery_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
           
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            // Test 1
            double thisRec = thisMet.metData.CalcAnemDataRecovery(thisMet.metData.anems[0], false);
            Assert.AreEqual(thisRec, 0.93586377, 0.001, "Wrong 30m unfiltered data recovery Test 1");

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].ID == 'B' && thisMet.metData.anems[i].height == 30)
                {
                    thisRec = thisMet.metData.CalcAnemDataRecovery(thisMet.metData.anems[i], true);
                    break;
                }

            Assert.AreEqual(thisRec, 0.80359589, 0.001, "Wrong 30m filtered data recovery Test 2");

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].ID == 'B' && thisMet.metData.anems[i].height == 40)
                {
                    thisRec = thisMet.metData.CalcAnemDataRecovery(thisMet.metData.anems[i], true);
                    break;
                }

            Assert.AreEqual(thisRec, 0.816799848, 0.001, "Wrong 40m filtered data recovery Test 3");

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].ID == 'B' && thisMet.metData.anems[i].height == 50)
                {
                    thisRec = thisMet.metData.CalcAnemDataRecovery(thisMet.metData.anems[i], true);
                    break;
                }

            Assert.AreEqual(thisRec, 0.818493151, 0.0001, "Wrong 50m filtered data recovery Test 4");

            thisMet.metData.startDate = Convert.ToDateTime("1/1/2009");
            thisMet.metData.endDate = Convert.ToDateTime("6/14/2009 16:10");

            thisRec = thisMet.metData.CalcAnemDataRecovery(thisMet.metData.anems[4], true);
            Assert.AreEqual(thisRec, 0.926327331, 0.0001, "Wrong 50m filtered data recovery Test 5");

            thisRec = thisMet.metData.CalcAnemDataRecovery(thisMet.metData.anems[3], true);
            Assert.AreEqual(thisRec, 0.878294606, 0.0001, "Wrong 40m filtered data recovery Test 6");

            thisInst.Close();

        }

        [TestMethod]
        public void Calc_Vane_Data_Recovery_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];
            double thisRec = 0;

            for (int i = 0; i < thisMet.metData.GetNumVanes(); i++)
                if (thisMet.metData.vanes[i].height == 41)
                {
                    thisRec = thisMet.metData.CalcVaneDataRecovery(thisMet.metData.vanes[i], true);
                    break;
                }

            Assert.AreEqual(thisRec, 0.908561644, 0.001, "Wrong 41m filtered data recovery Test 1");

            for (int i = 0; i < thisMet.metData.GetNumVanes(); i++)
                if (thisMet.metData.vanes[i].height == 49)
                {
                    thisRec = thisMet.metData.CalcVaneDataRecovery(thisMet.metData.vanes[i], true);
                    break;
                }

            Assert.AreEqual(thisRec, 0.911320396, 0.001, "Wrong 49m filtered data recovery Test 2");

            thisMet.metData.startDate = Convert.ToDateTime("9/24/2008");
            thisMet.metData.endDate = Convert.ToDateTime("5/4/2009 5:00");
            thisRec = thisMet.metData.CalcVaneDataRecovery(thisMet.metData.vanes[0], true);
            Assert.AreEqual(thisRec, 0.849803113, 0.001, "Wrong 41m filtered data recovery Test 3");

            thisRec = thisMet.metData.CalcVaneDataRecovery(thisMet.metData.vanes[1], true);
            Assert.AreEqual(thisRec, 0.854334646, 0.001, "Wrong 49m filtered data recovery Test 4");

            thisInst.Close();

        }

        [TestMethod]
        public void Calc_Temp_Data_Recovery_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            double thisRec = thisMet.metData.CalcTempDataRecovery(thisMet.metData.temps[0]);

            Assert.AreEqual(thisRec, 0.935863775, 0.001, "Wrong temperature data recovery Test 1");

            thisMet.metData.startDate = Convert.ToDateTime("9/24/2008");
            thisMet.metData.endDate = Convert.ToDateTime("5/4/2009 5:00");
            thisRec = thisMet.metData.CalcTempDataRecovery(thisMet.metData.temps[0]);
            Assert.AreEqual(thisRec, 0.89465, 0.001, "Wrong temperature data recovery Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcPercentAnemFiltered_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];
            Met_Data_Filter.Anem_Data thisAnem = new Met_Data_Filter.Anem_Data();

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == 30 && thisMet.metData.anems[i].ID == 'A')
                {
                    thisAnem = thisMet.metData.anems[i];
                    break;
                }


            double thisPercFilt = thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.minAnemSD) + thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.maxAnemSD);
            Assert.AreEqual(thisPercFilt, 0.000913, 0.00001, "Wrong SD Filt at 30m Test 1");

            thisPercFilt = thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.Icing);
            Assert.AreEqual(thisPercFilt, 0.0161339, 0.00001, "Wrong Icing Filt at 30m Test 2");

            thisPercFilt = thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.maxAnemRange);
            Assert.AreEqual(thisPercFilt, 0.00000, 0.00001, "Wrong max Range Filt at 30m Test 3");

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == 40 && thisMet.metData.anems[i].ID == 'B')
                {
                    thisAnem = thisMet.metData.anems[i];
                    break;
                }

            thisPercFilt = thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.minAnemSD) + thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.maxAnemSD);
            Assert.AreEqual(thisPercFilt, 0.0006088, 0.00001, "Wrong SD Filt at 40m Test 4");

            thisPercFilt = thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.Icing);
            Assert.AreEqual(thisPercFilt, 0.0145167, 0.00001, "Wrong Icing Filt at 40m Test 5");

            thisPercFilt = thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.maxAnemRange);
            Assert.AreEqual(thisPercFilt, 0.00000, 0.00001, "Wrong max Range Filt at 40m Test 6");

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == 50 && thisMet.metData.anems[i].ID == 'B')
                {
                    thisAnem = thisMet.metData.anems[i];
                    break;
                }

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Percent_Vane_Filtered_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);

            Thread.Sleep(4000);

            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];
            Met_Data_Filter.Vane_Data This_Vane = new Met_Data_Filter.Vane_Data();

            for (int i = 0; i < thisMet.metData.GetNumVanes(); i++)
                if (thisMet.metData.vanes[i].height == 41)
                    This_Vane = thisMet.metData.vanes[i];

            double thisPercFilt = thisMet.metData.CalcPercentVaneFiltered(This_Vane, Met_Data_Filter.Filter_Flags.Icing);
            Assert.AreEqual(thisPercFilt, 0.027302, 0.00001, "Wrong Icing Filt at 41m Test 1");

            for (int i = 0; i < thisMet.metData.GetNumVanes(); i++)
                if (thisMet.metData.vanes[i].height == 49)
                    This_Vane = thisMet.metData.vanes[i];

            thisPercFilt = thisMet.metData.CalcPercentVaneFiltered(This_Vane, Met_Data_Filter.Filter_Flags.Icing);
            Assert.AreEqual(thisPercFilt, 0.024543, 0.00001, "Wrong Icing Filt at 49m Test 2");

            thisMet.metData.startDate = Convert.ToDateTime("1/27/2009");
            thisMet.metData.endDate = Convert.ToDateTime("4/3/2009");

            thisPercFilt = thisMet.metData.CalcPercentVaneFiltered(thisMet.metData.vanes[0], Met_Data_Filter.Filter_Flags.Icing);
            Assert.AreEqual(thisPercFilt, 0.016729798, 0.00001, "Wrong Icing Filt at 41m");

            thisPercFilt = thisMet.metData.CalcPercentVaneFiltered(thisMet.metData.vanes[1], Met_Data_Filter.Filter_Flags.Icing);
            Assert.AreEqual(thisPercFilt, 0.018097643, 0.00001, "Wrong Icing Filt at 49m");

            thisInst.Close();
        }

        [TestMethod]
        public void GetMaxWS_atHeight_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            double thisMax = thisMet.metData.GetMaxWS_atHeight(30, 0);
            Assert.AreEqual(thisMax, 0.581, 0.001, "Wrong max WS at ind 0");

            thisMax = thisMet.metData.GetMaxWS_atHeight(30, 10);
            Assert.AreEqual(thisMax, 1.475, 0.001, "Wrong max WS at ind 10");

            thisInst.Close();
        }

        [TestMethod]
        public void FilterData_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];
            thisMet.metData.ClearFilterFlagsAndEstimatedData();
            string[] filtsToApply = new string[1];
            filtsToApply[0] = "All";
            thisMet.metData.FilterData(filtsToApply);

            int SD_Flag_Count = 0;
            int Icing_Flag_Count = 0;
            int max_Rng_Count = 0;
            int Tower_Shadow_Count = 0;
            int min_WS_count = 0;

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].orientation == 180 && thisMet.metData.anems[i].height == 30)
                    for (int j = 0; j < thisMet.metData.anems[i].windData.Length; j++)
                    {
                        if ((thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.maxAnemSD) ||
                            (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.minAnemSD))
                            SD_Flag_Count++;

                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.minWS) min_WS_count++;
                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.Icing) Icing_Flag_Count++;
                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.maxAnemRange) max_Rng_Count++;
                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.towerEffect) Tower_Shadow_Count++;
                    }

            Assert.AreEqual(min_WS_count, 445, 0, "Wrong number of data flagged for min WS Test 1");
            Assert.AreEqual(Icing_Flag_Count, 848, 0, "Wrong number of data flagged for Icing Test 2");
            Assert.AreEqual(SD_Flag_Count, 48, 0, "Wrong number of data flagged for WS SD Test 3");
            Assert.AreEqual(max_Rng_Count, 0, 0, "Wrong number of data flagged for max WS Rng Test 4");
            Assert.AreEqual(Tower_Shadow_Count, 4321, 0, "Wrong number of data flagged for tower shadow Test 5");

            SD_Flag_Count = 0;
            Icing_Flag_Count = 0;
            max_Rng_Count = 0;
            Tower_Shadow_Count = 0;
            min_WS_count = 0;

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].orientation == 270 && thisMet.metData.anems[i].height == 40)
                    for (int j = 0; j < thisMet.metData.anems[i].windData.Length; j++)
                    {
                        if ((thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.maxAnemSD) ||
                            (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.minAnemSD))
                            SD_Flag_Count++;

                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.minWS) min_WS_count++;
                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.Icing) Icing_Flag_Count++;
                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.maxAnemRange) max_Rng_Count++;
                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.towerEffect) Tower_Shadow_Count++;
                    }

            Assert.AreEqual(min_WS_count, 0, 0, "Wrong number of data flagged for min WS Test 6");
            Assert.AreEqual(Icing_Flag_Count, 763, 0, "Wrong number of data flagged for Icing Test 7");
            Assert.AreEqual(SD_Flag_Count, 32, 0, "Wrong number of data flagged for WS SD Test 8");
            Assert.AreEqual(max_Rng_Count, 0, 0, "Wrong number of data flagged for max WS Rng Test 9");
            Assert.AreEqual(Tower_Shadow_Count, 5516, 0, "Wrong number of data flagged for tower shadow Test 10");

            SD_Flag_Count = 0;
            Icing_Flag_Count = 0;
            max_Rng_Count = 0;
            Tower_Shadow_Count = 0;
            min_WS_count = 0;

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].orientation == 180 && thisMet.metData.anems[i].height == 50)
                    for (int j = 0; j < thisMet.metData.anems[i].windData.Length; j++)
                    {
                        if ((thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.maxAnemSD) ||
                            (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.minAnemSD))
                            SD_Flag_Count++;

                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.minWS) min_WS_count++;
                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.Icing) Icing_Flag_Count++;
                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.maxAnemRange) max_Rng_Count++;
                        if (thisMet.metData.anems[i].windData[j].filterFlag == Met_Data_Filter.Filter_Flags.towerEffect) Tower_Shadow_Count++;
                    }

            Assert.AreEqual(min_WS_count, 3, 0, "Wrong number of data flagged for min WS Test 11");
            Assert.AreEqual(Icing_Flag_Count, 764, 0, "Wrong number of data flagged for Icing Test 12");
            Assert.AreEqual(SD_Flag_Count, 35, 0, "Wrong number of data flagged for WS SD Test 13");
            Assert.AreEqual(max_Rng_Count, 0, 0, "Wrong number of data flagged for max WS Rng Test 14");
            Assert.AreEqual(Tower_Shadow_Count, 3594, 0, "Wrong number of data flagged for tower shadow Test 15");

            Icing_Flag_Count = 0;

            for (int i = 0; i < thisMet.metData.vanes[0].dirData.Length; i++)
                if (thisMet.metData.vanes[0].dirData[i].filterFlag == Met_Data_Filter.Filter_Flags.Icing) Icing_Flag_Count++;

            Assert.AreEqual(Icing_Flag_Count, 1435, 0, "Wrong number of data flagged for icing Test 16");

            Icing_Flag_Count = 0;

            for (int i = 0; i < thisMet.metData.vanes[1].dirData.Length; i++)
                if (thisMet.metData.vanes[1].dirData[i].filterFlag == Met_Data_Filter.Filter_Flags.Icing) Icing_Flag_Count++;

            Assert.AreEqual(Icing_Flag_Count, 1290, 0, "Wrong number of data flagged for icing Test 17");

            thisInst.Close();
        }

        [TestMethod]
        public void GetClosestWS_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            double thisWS = thisMet.metData.GetClosestWS(1, 13256);
            Assert.AreEqual(thisWS, 2.771643406, 0.0001, "Wrong closest wind speed Test 1");
        }

        [TestMethod]
        public void IsAffectedByTower_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            bool Is_Affected = thisMet.metData.IsAffectedByTower(45, 0);
            Assert.AreEqual(Is_Affected, false, "Wrong result Test 1");

            Is_Affected = thisMet.metData.IsAffectedByTower(359, 0);
            Assert.AreEqual(Is_Affected, true, "Wrong result Test 2");

            Is_Affected = thisMet.metData.IsAffectedByTower(100, 1);
            Assert.AreEqual(Is_Affected, true, "Wrong result Test 3");

            Is_Affected = thisMet.metData.IsAffectedByTower(345, 1);
            Assert.AreEqual(Is_Affected, false, "Wrong result Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void DebiasAvgWS_Diff_Test()
        {
            double[] avg_WS = new double[10];
            avg_WS[0] = 0.088;
            avg_WS[1] = 0.621;
            avg_WS[2] = 0.01;
            avg_WS[3] = 0.251;
            avg_WS[4] = 0.105;
            avg_WS[5] = 0.616;
            avg_WS[6] = 0.276;
            avg_WS[7] = 0.116;
            avg_WS[8] = 0.988;
            avg_WS[9] = 0.804;

            Met thisMet = new Met();
            thisMet.metData = new Met_Data_Filter();
            double[] Debiased = thisMet.metData.DebiasAvgWS_Diff(avg_WS);

            Assert.AreEqual(Debiased[3], -0.1365, 0.001, "Wrong debiased WS");

        }

        [TestMethod]
        public void FindShadowMinMax_Test()
        {
            double[] avg_WS = new double[10];
            avg_WS[0] = 0.03;
            avg_WS[1] = 0.029;
            avg_WS[2] = 0.250;
            avg_WS[3] = 0.052;
            avg_WS[4] = 0.036;
            avg_WS[5] = 0.092;
            avg_WS[6] = 0.046;
            avg_WS[7] = 0.084;
            avg_WS[8] = 0.039;
            avg_WS[9] = 0.044;

            Met_Data_Filter thisMetData = new Met_Data_Filter();
            int[] Shadow_inds = thisMetData.FindShadowMinMax(avg_WS, 72);

            Assert.AreEqual(36, Shadow_inds[0]);
            Assert.AreEqual(108, Shadow_inds[1]);
        }

        [TestMethod]
        public void FindMinMaxWS_Diff_Test()
        {
            double[] avg_WS = new double[10];
            avg_WS[0] = 0.03;
            avg_WS[1] = 0.029;
            avg_WS[2] = 0.250;
            avg_WS[3] = 0.052;
            avg_WS[4] = 0.036;
            avg_WS[5] = 0.092;
            avg_WS[6] = 0.046;
            avg_WS[7] = 0.084;
            avg_WS[8] = -0.539;
            avg_WS[9] = 0.044;

            Met_Data_Filter thisMetData = new Met_Data_Filter();
            int thisMin = thisMetData.FindMinMaxWS_Diff(avg_WS, "min");
            int thisMax = thisMetData.FindMinMaxWS_Diff(avg_WS, "max");
            Assert.AreEqual(thisMin, 288, 0);
            Assert.AreEqual(thisMax, 72, 0);
        }

        [TestMethod]
        public void CalcAvgWS_Diffs_by_WD_Test()
        {

            string filename = testingFolder + "\\CalcAvgWS_DiffsByWD\\WS Diffs and WD.csv";

            StreamReader file = new StreamReader(filename);
            string line;
            char[] delims = { ',' };

            Met_Data_Filter.WS_Ratio_WD_Data theseDiffs = new Met_Data_Filter.WS_Ratio_WD_Data();
            int Diff_Count = 0;

            while ((line = file.ReadLine()) != null)
            {
                String[] Data = line.Split(delims);

                if (Data[0] != "" & Data[1] != "")
                {
                    Diff_Count++;
                    Array.Resize(ref theseDiffs.WS_Ratio, Diff_Count);
                    Array.Resize(ref theseDiffs.WD, Diff_Count);
                    theseDiffs.WS_Ratio[Diff_Count - 1] = Convert.ToSingle(Data[0]);
                    theseDiffs.WD[Diff_Count - 1] = Convert.ToDouble(Data[1]);
                }
            }

            Met_Data_Filter thisMetData = new Met_Data_Filter();
            double[] theseAvgDiffs = thisMetData.CalcAvgWS_DiffsByWD(theseDiffs);

            Assert.AreEqual(theseAvgDiffs[0], -0.075307018, 0.0001, "Wrong avg Diff Test 1");
            Assert.AreEqual(theseAvgDiffs[13], 0.367984064, 0.0001, "Wrong avg Diff Test 2");
            Assert.AreEqual(theseAvgDiffs[27], 0.376872093, 0.0001, "Wrong avg Diff Test 3");
            Assert.AreEqual(theseAvgDiffs[179], -0.051792271, 0.0001, "Wrong avg Diff Test 4");

        }

        [TestMethod]
        public void GetVaneClosestToHH_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            double HH = 30;
            int thisInd = thisMet.metData.GetVaneClosestToHH(HH);
            Assert.AreEqual(thisInd, 0);

            HH = 50;
            thisInd = thisMet.metData.GetVaneClosestToHH(HH);
            Assert.AreEqual(thisInd, 1);

            HH = 60;
            thisInd = thisMet.metData.GetVaneClosestToHH(HH);
            Assert.AreEqual(thisInd, 1);

            thisInst.Close();

        }

        [TestMethod]
        public void GetAnemsClosestToHH_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            int actInd = 0;
            double HH = 30;

            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == 30 && thisMet.metData.anems[i].ID == 'B')
                    actInd = i;

            int[] thisInd = thisMet.metData.GetAnemsClosestToHH(HH);
            Assert.AreEqual(thisInd[1], actInd, 0, "Wrong anem index");

            HH = 55;
            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == 50 && thisMet.metData.anems[i].ID == 'B')
                    actInd = i;

            thisInd = thisMet.metData.GetAnemsClosestToHH(HH);
            Assert.AreEqual(thisInd[1], actInd, 0, "Wrong anem index");

            HH = 42;
            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == 40 && thisMet.metData.anems[i].ID == 'B')
                    actInd = i;

            thisInd = thisMet.metData.GetAnemsClosestToHH(HH);
            Assert.AreEqual(thisInd[1], actInd, 0, "Wrong anem index");

            thisInst.Close();

        }

        [TestMethod]
        public void GetHeightClosestToHH_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            double thisHH = 10;
            int thisHeightInd = thisMet.metData.GetHeightClosestToHH(thisHH);
            Assert.AreEqual(thisHeightInd, 0, 0, "Wrong Test 1");

            thisHH = 100;
            thisHeightInd = thisMet.metData.GetHeightClosestToHH(thisHH);
            Assert.AreEqual(thisHeightInd, 2, 0, "Wrong Test 2");

            thisInst.Close();
        }

        [TestMethod]
        public void ExtrapolateData_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];
            string[] filtsToApply = new string[1];
            filtsToApply[0] = "All";
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.EstimateAlpha();

            thisMet.metData.ExtrapolateData(80);
            thisMet.metData.ExtrapolateData(45);
            thisMet.metData.ExtrapolateData(20);

            Assert.AreEqual(thisMet.metData.simData[2].WS_WD_data[253].WS, 3.797445309, 0.001, "Wrong extrapolated wind speed Test 1");
            Assert.AreEqual(thisMet.metData.simData[1].WS_WD_data[12679].WS, 3.847447028, 0.001, "Wrong extrapolated wind speed Test 2");
            Assert.AreEqual(thisMet.metData.simData[0].WS_WD_data[5559].WS, 4.201222192, 0.001, "Wrong extrapolated wind speed Test 3");
            Assert.AreEqual(thisMet.metData.simData[2].WS_WD_data[44444].WS, 5.077628706, 0.001, "Wrong extrapolated wind speed Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void EstimateAlpha_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];
            string[] filtsToApply = new string[1];
            filtsToApply[0] = "All";
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.EstimateAlpha();

            Assert.AreEqual(thisMet.metData.alpha[0].alpha, 2.231940771, 0.001, "Wrong alpha Test 1");
            Assert.AreEqual(thisMet.metData.alpha[6579].alpha, 0.762375729, 0.001, "Wrong alpha Test 2");
            Assert.AreEqual(thisMet.metData.alpha[11689].alpha, 0.283838437, 0.001, "Wrong alpha Test 3");
            Assert.AreEqual(thisMet.metData.alpha[44466].alpha, 0.010483427, 0.001, "Wrong alpha Test 4");

            thisInst.Close();
        }

        [TestMethod]
        public void GetAvgAlpha_from_Valid_WS_Test()
        {
            double[] avg_WS = new double[] { 0.447, 0.8495, 2.4585 };
            double[] heights = new double[] { 30, 40, 50 };

            Met_Data_Filter thisMetData = new Met_Data_Filter();
            double avg_Alpha = thisMetData.GetAvgAlphaFromValidWS(avg_WS, heights);

            Assert.AreEqual(avg_Alpha, 2.231941, 0.001, "Wrong avg Alpha Test 1");

            avg_WS = new double[] { 2.146, 2.146, 2.28 };
            avg_Alpha = thisMetData.GetAvgAlphaFromValidWS(avg_WS, heights);
            Assert.AreEqual(avg_Alpha, 0.1300, 0.001, "Wrong avg Alpha Test 2");

            avg_WS = new double[] { 1.9, 1.8105, 1.833 };
            avg_Alpha = thisMetData.GetAvgAlphaFromValidWS(avg_WS, heights);
            Assert.AreEqual(avg_Alpha, -0.06088, 0.001, "Wrong avg Alpha Test 3");

            avg_WS = new double[] { 3.263, 3.2635, 3.487 };
            avg_Alpha = thisMetData.GetAvgAlphaFromValidWS(avg_WS, heights);
            Assert.AreEqual(avg_Alpha, 0.142455, 0.001, "Wrong avg Alpha Test 4");

        }

        [TestMethod]
        public void GetMaxWS_AndGust_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            Met_Data_Filter.Yearly_Maxes thisMax = thisMet.metData.GetMaxWS_AndGust(2008, 10);
            Assert.AreEqual(thisMax.maxWS, 18.1945, 0.001, "Wrong 30m max WS");
            Assert.AreEqual(thisMax.maxGust, 27.00117, 0.001, "Wrong 30m max Gust");

            thisMax = thisMet.metData.GetMaxWS_AndGust(2008, 52);
            Assert.AreEqual(thisMax.maxWS, 19.49091169, 0.001, "Wrong 50m max WS");
            Assert.AreEqual(thisMax.maxGust, 26.55418, 0.001, "Wrong 50m max Gust");

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Extrap_Recovery_Test()
        {
            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\MetDataFilter Archbold testing.cfm";
            thisInst.Open(Filename);

            Thread.Sleep(4000);

            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            bool workDon = thisInst.BW_worker.DoWorkDone;
            Met thisMet = thisInst.metList.metItem[0];
            thisMet.metData.ClearFilterFlagsAndEstimatedData();
            thisMet.metData.startDate = Convert.ToDateTime("8/1/2008 0:00");
            thisMet.metData.endDate = Convert.ToDateTime("1/28/2009 0:00");
            string[] filtsToApply = new string[1];
            filtsToApply[0] = "All";
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.EstimateAlpha();
            thisMet.metData.ExtrapolateData(80);

            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            double thisRec = thisMet.metData.CalcExtrapRecovery(thisMet.metData.simData[0]);
            Assert.AreEqual(thisRec, 0.823225309, 0.001, "Wrong extrapolated data recovery");

            thisMet.metData.ClearFilterFlagsAndEstimatedData();
            thisMet.metData.startDate = Convert.ToDateTime("6/24/2008 0:00");
            thisMet.metData.endDate = Convert.ToDateTime("12/2/2008 0:00");
            thisMet.metData.FilterData(filtsToApply);
            thisMet.metData.EstimateAlpha();
            thisMet.metData.ExtrapolateData(80);

            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            thisRec = thisMet.metData.CalcExtrapRecovery(thisMet.metData.simData[0]);
            Assert.AreEqual(thisRec, 0.84269323, 0.001, "Wrong extrapolated data recovery");

        }

        [TestMethod]
        public void GetTS_Index()
        {

            Continuum thisInst = new Continuum("", false);
            thisInst.isTest = true;
            string Filename = testingFolder + "\\GetTS_Index\\GetTS_Index_Test.cfm";
            thisInst.Open(Filename);
            Thread.Sleep(4000);
            while (thisInst.metList.metItem[0].metData.simDataCalcComplete == false) // RunWorkerCompleted isn't getting called (?) so killing BW_Worker once it reaches end of DoWork
                Thread.Sleep(1000);

            Met thisMet = thisInst.metList.metItem[0];

            // Test 1 - 3: Anem_Data
            int testInd1 = thisMet.metData.anems[0].GetTS_Index(Convert.ToDateTime("08/05/2008 4:00 AM"));
            int testInd2 = thisMet.metData.anems[0].GetTS_Index(Convert.ToDateTime("01/09/2009 15:50"));
            int testInd3 = thisMet.metData.anems[0].GetTS_Index(Convert.ToDateTime("10/23/2009 3:30 AM"));

            Assert.AreEqual(testInd1, 7841, 0, "Wrong anem TS index Test 1");
            Assert.AreEqual(testInd2, 19986, 0, "Wrong anem TS index Test 2");
            Assert.AreEqual(testInd3, 61240, 0, "Wrong anem TS index Test 3");

            // Test 4 - 6: Vane_Data
            int testInd4 = thisMet.metData.vanes[0].GetTS_Index(Convert.ToDateTime("08/05/2008 4:00 AM"));
            int testInd5 = thisMet.metData.vanes[0].GetTS_Index(Convert.ToDateTime("01/09/2009 15:50"));
            int testInd6 = thisMet.metData.vanes[0].GetTS_Index(Convert.ToDateTime("10/23/2009 3:30 AM"));

            Assert.AreEqual(testInd4, 7841, 0, "Wrong anem TS index Test 4");
            Assert.AreEqual(testInd5, 19986, 0, "Wrong anem TS index Test 5");
            Assert.AreEqual(testInd6, 61240, 0, "Wrong anem TS index Test 6");

            // Test 7 - 9: Temp_Data
            int testInd7 = thisMet.metData.temps[0].GetTS_Index(Convert.ToDateTime("08/05/2008 4:00 AM"));
            int testInd8 = thisMet.metData.temps[0].GetTS_Index(Convert.ToDateTime("01/09/2009 15:50"));
            int testInd9 = thisMet.metData.temps[0].GetTS_Index(Convert.ToDateTime("10/23/2009 3:30 AM"));

            Assert.AreEqual(testInd7, 7841, 0, "Wrong temps TS index Test 7");
            Assert.AreEqual(testInd8, 19986, 0, "Wrong temps TS index Test 8");
            Assert.AreEqual(testInd9, 61240, 0, "Wrong temps TS index Test 9");

            // Test 10 - 12: Baro_Data
            int testInd10 = thisMet.metData.baros[0].GetTS_Index(Convert.ToDateTime("08/05/2008 4:00 AM"));
            int testInd11 = thisMet.metData.baros[0].GetTS_Index(Convert.ToDateTime("01/09/2009 15:50"));
            int testInd12 = thisMet.metData.baros[0].GetTS_Index(Convert.ToDateTime("10/23/2009 3:30 AM"));

            Assert.AreEqual(testInd10, 7841, 0, "Wrong anem TS index Test 10");
            Assert.AreEqual(testInd11, 19986, 0, "Wrong anem TS index Test 11");
            Assert.AreEqual(testInd12, 61240, 0, "Wrong anem TS index Test 12");

            thisInst.Close();

        }

        [TestMethod]
        public void CalcBestFitAlpha_Test()
        {
            double[] avg_WS = new double[] { 4.2, 5.1, 5.5, 5.7, 5.9 };
            double[] heights = new double[] { 30, 45, 60, 80, 100 };

            Met_Data_Filter thisMetData = new Met_Data_Filter();
            thisMetData.shearSettings.shearCalcType = Met_Data_Filter.ShearCalculationTypes.bestFit;

            // Test 1: Height range = 30 - 100
            thisMetData.shearSettings.minHeight = 30;
            thisMetData.shearSettings.maxHeight = 100;
            double avg_Alpha = thisMetData.CalcBestFitAlpha(avg_WS, heights);

            Assert.AreEqual(avg_Alpha, 0.2733, 0.001, "Wrong avg Alpha Test 1");

            // Test 2: Height range = 30 - 60
            thisMetData.shearSettings.minHeight = 30;
            thisMetData.shearSettings.maxHeight = 60;
            avg_Alpha = thisMetData.CalcBestFitAlpha(avg_WS, heights);

            Assert.AreEqual(avg_Alpha, 0.3949, 0.001, "Wrong avg Alpha Test 2");

            // Test 2: Height range = 45 - 80
            thisMetData.shearSettings.minHeight = 45;
            thisMetData.shearSettings.maxHeight = 80;
            avg_Alpha = thisMetData.CalcBestFitAlpha(avg_WS, heights);

            Assert.AreEqual(avg_Alpha, 0.1933, 0.001, "Wrong avg Alpha Test 3");

        }
    }
}
