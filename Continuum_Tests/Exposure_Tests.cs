using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    [TestClass]
    public class Exposure_Tests
    {
        [TestMethod]
        public void Get_DW_Param_Test()
        {
            Exposure thisExpo = new Exposure();
            thisExpo.expo = new double[16];
            thisExpo.SR = new double[16];
            thisExpo.dispH = new double[16];

            for (int i = 0; i < 16; i++)
            {
                thisExpo.expo[i] = i;
                thisExpo.SR[i] = 0.1 + (double)i / 10;
                thisExpo.dispH[i] = 10 + 10 * i;
            }

            Assert.AreEqual(thisExpo.GetDW_Param(0, "Expo"), 8, 1e-6, "Wrong DW Expo Test 1");
            Assert.AreEqual(thisExpo.GetDW_Param(3, "Expo"), 11, 1e-6, "Wrong DW Expo Test 2");
            Assert.AreEqual(thisExpo.GetDW_Param(12, "Expo"), 4, 1e-6, "Wrong DW Expo Test 3");
            Assert.AreEqual(thisExpo.GetDW_Param(4, "SR"), 1.3, 1e-6, "Wrong DW SR Test 4");
            Assert.AreEqual(thisExpo.GetDW_Param(9, "SR"), 0.2, 1e-6, "Wrong DW SR Test 5");
            Assert.AreEqual(thisExpo.GetDW_Param(15, "SR"), 0.8, 1e-6, "Wrong DW SR Test 6");
            Assert.AreEqual(thisExpo.GetDW_Param(1, "DH"), 100, 1e-6, "Wrong DW DH Test 7");
            Assert.AreEqual(thisExpo.GetDW_Param(8, "DH"), 10, 1e-6, "Wrong DW DH Test 8");
            Assert.AreEqual(thisExpo.GetDW_Param(14, "DH"), 70, 1e-6, "Wrong DW DH Test 9");
        }
    }
}
