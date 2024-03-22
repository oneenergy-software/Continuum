using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Continuum_Tests.GUI_Tests
{
    [TestClass]
    public class SiteSuitability_Tests
    {
        string testFolder;
        string saveFolder;

        Globals globals = new Globals();
        
        public SiteSuitability_Tests()
        {
            testFolder = globals.testFolder;
            saveFolder = globals.saveFolder;
        }

        [TestMethod]
        public void ShadowFlicker_Test()
        {
            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_123";

            thisInst.Open(fileName + ".cfm");
            thisInst.siteSuitability.RunShadowFlickerModel(thisInst);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();
            
            // Called in RunWorkerCompleted
            thisInst.updateThe.SiteSuitabilityDropdown("Shadow Flicker");
            thisInst.updateThe.SiteSuitabilityTAB();
            thisInst.updateThe.ColoredButtons();

            if (thisInst.siteSuitability.numXFlicker != 0)
                thisInst.SaveFile();

            Assert.AreNotEqual(thisInst.siteSuitability.numXFlicker, 0, "Didn't calculate shadow map");
            Assert.AreNotEqual(thisInst.siteSuitability.zones[0].flickerStats.shadowMins12x24, null, "Didn't calculate shadow at zones");

            thisInst.BW_worker.Close();
            thisInst.Close();

        }

        [TestMethod]
        public void IceThrow_Test()
        {
            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_123";

            thisInst.Open(fileName + ".cfm");

            int numIceThrowsPerDay = thisInst.GetNumIceThrowsPerDay();
            int numIceDaysPerYear = thisInst.GetNumIcingDays();

            thisInst.siteSuitability.numIceDaysPerYear = numIceDaysPerYear;
            thisInst.siteSuitability.iceThrowsPerIceDay = numIceThrowsPerDay;

            BackgroundWork.Vars_for_BW varsForBW = new BackgroundWork.Vars_for_BW();
            varsForBW.thisInst = thisInst;
            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_IceThrow(varsForBW);

            while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                Thread.Sleep(1000);

            if (thisInst.BW_worker.WasReturned)
                Assert.Fail();

            thisInst.BW_worker.Close();

            // Called in RunCompleted
            thisInst.updateThe.SiteSuitabilityDropdown("Ice Throw");
            thisInst.updateThe.IcingYearsDropDown();
            thisInst.updateThe.SiteSuitabilityTAB();
            thisInst.ChangesMade();

            thisInst.updateThe.ColoredButtons();

            Assert.AreNotEqual(thisInst.siteSuitability.yearlyIceHits.Length, 0, "Didn't calculate ice throw");
            Assert.AreNotEqual(thisInst.siteSuitability.yearlyIceHits[0].iceHits.Length, 0, "Didn't calculate ice throw");
            thisInst.Close();
        }

        [TestMethod]
        public void SoundModel_Test()
        {
            Continuum thisInst = new Continuum("");

            string fileName = saveFolder + "\\OneMetTABAndGrossNet_123";

            thisInst.Open(fileName + ".cfm");
            double turbineSound = thisInst.GetTurbineNoise();

            if (turbineSound != -999)
            {
                thisInst.siteSuitability.turbineSound = turbineSound;
                thisInst.siteSuitability.CreateSoundMap(thisInst);
                thisInst.updateThe.SoundMap();
                thisInst.updateThe.SoundAtZones();
                thisInst.updateThe.SiteSuitabilityDropdown("Sound");
                thisInst.updateThe.ColoredButtons();
                thisInst.updateThe.SiteSuitabilityVisibility();
            }

            Assert.AreNotEqual(thisInst.siteSuitability.soundMap, null, "Didn't calculate turbine noise");

            thisInst.Close();
        }

    }
}
