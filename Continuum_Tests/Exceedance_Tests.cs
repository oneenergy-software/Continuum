using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class Exceedance_Tests
    {
        string testingFolder = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\Unit tests & Documentation\\Exceedance";
        
        [TestMethod]
        public void Get_PF_Value_Test()
        {
            Exceedance exceedance = new Exceedance();
            exceedance.CreateDefaultCurve();

            double thisPF = exceedance.Get_PF_Value(0.2, exceedance.exceedCurves[0]);
            Assert.AreEqual(0.971577, thisPF, 0.001, "Wrong PF Test 1");

            thisPF = exceedance.Get_PF_Value(0.445, exceedance.exceedCurves[0]);
            Assert.AreEqual(0.980636, thisPF, 0.001, "Wrong PF Test 2");

            thisPF = exceedance.Get_PF_Value(0.86, exceedance.exceedCurves[0]);
            Assert.AreEqual(0.991497, thisPF, 0.001, "Wrong PF Test 3");

            thisPF = exceedance.Get_PF_Value(0.98, exceedance.exceedCurves[0]);
            Assert.AreEqual(0.9945, thisPF, 0.001, "Wrong PF Test 4");

        }

        [TestMethod]
        public void FindPValues_Test()
        {

            string Filename = testingFolder + "\\FindPValues\\totalPF_1yr.txt";
            StreamReader sr = new StreamReader(Filename);

            double[] totalPF_1yr = new double[100000];
            int count = 0;

            while (sr.EndOfStream == false)
            {
                totalPF_1yr[count] = Convert.ToDouble(sr.ReadLine());
                count++;
            }

            sr.Close();

            Exceedance exceedance = new Exceedance();
            exceedance.FindPValues(totalPF_1yr, 1);

            Assert.AreEqual(exceedance.compositeLoss.pVals1yr[0], 1.101147, 0.0001, "Wrong 1yr P1 Value");
            Assert.AreEqual(exceedance.compositeLoss.pVals1yr[20], 0.973731, 0.0001, "Wrong 1yr P21 Value");
            Assert.AreEqual(exceedance.compositeLoss.pVals1yr[42], 0.923149, 0.0001, "Wrong 1yr P43 Value");
            Assert.AreEqual(exceedance.compositeLoss.pVals1yr[63], 0.881307, 0.0001, "Wrong 1yr P64 Value");
            Assert.AreEqual(exceedance.compositeLoss.pVals1yr[87], 0.819363, 0.0001, "Wrong 1yr P88 Value");
            Assert.AreEqual(exceedance.compositeLoss.pVals1yr[99], 0.599363, 0.0001, "Wrong 1yr P100 Value");


        }

        [TestMethod]
        public void CalculateProbDist_Test()
        {
            Exceedance exceedance = new Exceedance();
            Exceedance.ExceedanceCurve exceedanceCurve = new Exceedance.ExceedanceCurve();
            exceedanceCurve.lowerBound = 80;
            exceedanceCurve.upperBound = 120;
            exceedanceCurve.distSize = 100;
            exceedanceCurve.modes = new Exceedance.Mode_Def[1];
            exceedanceCurve.modes[0].mean = 103;
            exceedanceCurve.modes[0].SD = 5.3f;
            exceedanceCurve.modes[0].weight = 1.0f;
            exceedanceCurve.probDist = new double[100];
            exceedanceCurve.xVals = new double[100];
            exceedance.CalculateProbDist(ref exceedanceCurve);

            Assert.AreEqual(exceedanceCurve.probDist[0], 0, 0.0001, "Wrong PDF at index 0");
            Assert.AreEqual(exceedanceCurve.probDist[12], 0.0002136, 0.00001, "Wrong PDF at index 12");
            Assert.AreEqual(exceedanceCurve.probDist[49], 0.062715488, 0.00001, "Wrong PDF at index 49");
            Assert.AreEqual(exceedanceCurve.probDist[73], 0.035524804, 0.00001, "Wrong PDF at index 73");
            Assert.AreEqual(exceedanceCurve.probDist[99], 0.0004390, 0.00001, "Wrong PDF at index 99");

        }

        [TestMethod]
        public void Normalize_Dists_Test()
        {
            Exceedance exceedance = new Exceedance();
            Exceedance.ExceedanceCurve exceedanceCurve = new Exceedance.ExceedanceCurve();
            exceedanceCurve.lowerBound = 90;
            exceedanceCurve.upperBound = 100;
            exceedanceCurve.distSize = 100;
            exceedanceCurve.modes = new Exceedance.Mode_Def[1];
            exceedanceCurve.modes[0].mean = 97;
            exceedanceCurve.modes[0].SD = 2.1f;
            exceedanceCurve.modes[0].weight = 1.0f;
            exceedanceCurve.probDist = new double[100];
            exceedanceCurve.cumulDist = new double[100];
            exceedanceCurve.xVals = new double[100];
            exceedance.CalculateProbDist(ref exceedanceCurve);
            exceedance.Normalize_Dists(ref exceedanceCurve);

            Assert.AreEqual(exceedanceCurve.probDist[0], 0.000793, 0.00001, "Wrong normalized PDF index 0");
            Assert.AreEqual(exceedanceCurve.probDist[28], 0.028506, 0.00001, "Wrong normalized PDF index 28");
            Assert.AreEqual(exceedanceCurve.probDist[49], 0.127301, 0.00001, "Wrong normalized PDF index 49");
            Assert.AreEqual(exceedanceCurve.probDist[61], 0.189345, 0.00001, "Wrong normalized PDF index 61");
            Assert.AreEqual(exceedanceCurve.probDist[79], 0.183905, 0.00001, "Wrong normalized PDF index 79");
            Assert.AreEqual(exceedanceCurve.probDist[88], 0.13683, 0.00001, "Wrong normalized PDF index 88");
            Assert.AreEqual(exceedanceCurve.probDist[99], 0.07391, 0.00001, "Wrong normalized PDF index 99");

            Assert.AreEqual(exceedanceCurve.cumulDist[0], 0.000080, 0.00001, "Wrong normalized CDF index 0");
            Assert.AreEqual(exceedanceCurve.cumulDist[28], 0.0263899, 0.00001, "Wrong normalized CDF index 28");
            Assert.AreEqual(exceedanceCurve.cumulDist[49], 0.18353226, 0.00001, "Wrong normalized CDF index 49");
            Assert.AreEqual(exceedanceCurve.cumulDist[61], 0.38140445, 0.00001, "Wrong normalized CDF index 61");
            Assert.AreEqual(exceedanceCurve.cumulDist[79], 0.74236859, 0.00001, "Wrong normalized CDF index 79");
            Assert.AreEqual(exceedanceCurve.cumulDist[88], 0.88698974, 0.00001, "Wrong normalized CDF index 88");
            Assert.AreEqual(exceedanceCurve.cumulDist[99], 1, 0.00001, "Wrong normalized CDF index 99");
        }

        [TestMethod]
        public void Interpolate_CDF_Test()
        {
            Exceedance exceed = new Exceedance();
            Add_Exceedance addExceed = new Add_Exceedance();
            Exceedance.ExceedanceCurve exceedCurve = new Exceedance.ExceedanceCurve();
            exceedCurve.distSize = 1000;
            exceedCurve.xVals = new double[exceedCurve.distSize];
            exceedCurve.cumulDist = new double[exceedCurve.distSize];

            string filename = testingFolder + "\\Interpolate_CDF\\Imported_CDF.csv";
            double[,] importedCDF = addExceed.Read_CDF_file(filename);
            exceed.Interpolate_CDF(ref exceedCurve, importedCDF);

            Assert.AreEqual(exceedCurve.xVals[0], 0.853452, 0.001, "Wrong XVal Index 0");
            Assert.AreEqual(exceedCurve.cumulDist[0], 0, 0.001, "Wrong CDF Index 0");

            Assert.AreEqual(exceedCurve.xVals[224], 0.9246206, 0.001, "Wrong XVal Index 224");
            Assert.AreEqual(exceedCurve.cumulDist[224], 0.067422, 0.001, "Wrong CDF Index 224");

            Assert.AreEqual(exceedCurve.xVals[368], 0.9703719, 0.001, "Wrong XVal Index 368");
            Assert.AreEqual(exceedCurve.cumulDist[368], 0.307178, 0.001, "Wrong CDF Index 368");

            Assert.AreEqual(exceedCurve.xVals[479], 1.005639, 0.001, "Wrong XVal Index 479");
            Assert.AreEqual(exceedCurve.cumulDist[479], 0.565243, 0.001, "Wrong CDF Index 479");

            Assert.AreEqual(exceedCurve.xVals[602], 1.044718, 0.001, "Wrong XVal Index 602");
            Assert.AreEqual(exceedCurve.cumulDist[602], 0.796571, 0.001, "Wrong CDF Index 602");

            Assert.AreEqual(exceedCurve.xVals[788], 1.103813, 0.001, "Wrong XVal Index 788");
            Assert.AreEqual(exceedCurve.cumulDist[788], 0.909619, 0.001, "Wrong CDF Index 788");

            Assert.AreEqual(exceedCurve.xVals[899], 1.13908, 0.001, "Wrong XVal Index 899");
            Assert.AreEqual(exceedCurve.cumulDist[899], 0.970939, 0.001, "Wrong CDF Index 899");

            Assert.AreEqual(exceedCurve.xVals[903], 1.140351, 0.001, "Wrong XVal Index 903");
            Assert.AreEqual(exceedCurve.cumulDist[903], 0.972101, 0.001, "Wrong CDF Index 903");
        }

        [TestMethod]
        public void Calc_PDF_from_CDF_Test()
        {
            Exceedance exceed = new Exceedance();
            Add_Exceedance addExceed = new Add_Exceedance();
            Exceedance.ExceedanceCurve exceedCurve = new Exceedance.ExceedanceCurve();
            exceedCurve.distSize = 1000;
            exceedCurve.xVals = new double[exceedCurve.distSize];
            exceedCurve.cumulDist = new double[exceedCurve.distSize];
            exceedCurve.probDist = new double[exceedCurve.distSize];

            string filename = testingFolder + "\\Interpolate_CDF\\Imported_CDF.csv";
            double[,] importedCDF = addExceed.Read_CDF_file(filename);
            exceed.Interpolate_CDF(ref exceedCurve, importedCDF);
            exceed.Calc_PDF_from_CDF(ref exceedCurve);

            Assert.AreEqual(exceedCurve.probDist[145], 1.8294, 0.0001, "Wrong PDF at Index 145");
            Assert.AreEqual(exceedCurve.probDist[369], 7.3175, 0.0001, "Wrong PDF at Index 369");
            Assert.AreEqual(exceedCurve.probDist[599], 3.6588, 0.0001, "Wrong PDF at Index 599");
            Assert.AreEqual(exceedCurve.probDist[703], 0.9147, 0.0001, "Wrong PDF at Index 703");
            Assert.AreEqual(exceedCurve.probDist[850], 1.8294, 0.0001, "Wrong PDF at Index 850");
        }
    }
}
