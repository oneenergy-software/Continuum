using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Continuum_Tests
{
    [TestClass]
    public class LC_Key_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;

        public LC_Key_Tests()
        {
            testingFolder = globals.testingFolder + "LC_Key";
        }

        [TestMethod]
        public void LCKeyOK_Test()
        {
            // Tests changing LC key after a met has been imported and a map has been created
            
            string DB_Change_LC_Before_Met = "Test_Change_LCKey_Before_Met.cfm";
            string DB_Change_LC_After_Met = "Test_Change_LCKey_After_Met.cfm";

            NodeCollection nodeList = new NodeCollection();
            BinaryFormatter bin = new BinaryFormatter();
            string connBefore = nodeList.GetDB_ConnectionString(testingFolder + "\\" + DB_Change_LC_Before_Met);
            string connAfter = nodeList.GetDB_ConnectionString(testingFolder + "\\" + DB_Change_LC_After_Met);

            Nodes[] LC_Change_Before_Met = new Nodes[0]; // array of Nodes with SRDH calcs from model where Land Cover key was changed before the met import and map generation
            Nodes[] LC_Change_After_Met = new Nodes[0]; // same as above but from model where Land Cover key was changed after the met import and map generation

            // Grab all Nodes with SRDH calcs from 'LC Key Change Before' model
            using (var context = new Continuum_EDMContainer(connBefore))
            {
                var node_db = from N in context.Node_table.Include("expo") select N;

                foreach (var N in node_db)
                {
                    int numRadii = N.expo.Count;

                    for (int i = 0; i < numRadii; i++)
                    {
                        if (N.expo.ElementAt(i).SR_Array != null)
                        {
                            int numNodes = LC_Change_Before_Met.Length;
                            Array.Resize(ref LC_Change_Before_Met, numNodes + 1);
                            LC_Change_Before_Met[numNodes] = new Nodes();
                            LC_Change_Before_Met[numNodes].UTMX = N.UTMX;
                            LC_Change_Before_Met[numNodes].UTMY = N.UTMY;
                            LC_Change_Before_Met[numNodes].elev = N.elev;

                            MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).SR_Array);
                            LC_Change_Before_Met[numNodes].AddExposure(N.expo.ElementAt(i).radius, N.expo.ElementAt(i).exponent, 1, 24);
                            LC_Change_Before_Met[numNodes].expo[0].SR = new double[24];
                            LC_Change_Before_Met[numNodes].expo[0].SR = (double[])bin.Deserialize(MS3);

                            MS3 = new MemoryStream(N.expo.ElementAt(i).DH_Array);
                            LC_Change_Before_Met[numNodes].expo[0].dispH = new double[24];
                            LC_Change_Before_Met[numNodes].expo[0].dispH = (double[])bin.Deserialize(MS3);
                        }
                    }
                }
            }

            // Grab all Nodes with SRDH calcs from 'LC Key Change After' model
            using (var context = new Continuum_EDMContainer(connAfter))
            {
                var node_db = from N in context.Node_table.Include("expo") select N;

                foreach (var N in node_db)
                {
                    int numRadii = N.expo.Count;

                    for (int i = 0; i < numRadii; i++)
                    {
                        if (N.expo.ElementAt(i).SR_Array != null)
                        {
                            int numNodes = LC_Change_After_Met.Length;
                            Array.Resize(ref LC_Change_After_Met, numNodes + 1);
                            LC_Change_After_Met[numNodes] = new Nodes();
                            LC_Change_After_Met[numNodes].UTMX = N.UTMX;
                            LC_Change_After_Met[numNodes].UTMY = N.UTMY;
                            LC_Change_After_Met[numNodes].elev = N.elev;

                            MemoryStream MS3 = new MemoryStream(N.expo.ElementAt(i).SR_Array);
                            LC_Change_After_Met[numNodes].AddExposure(N.expo.ElementAt(i).radius, N.expo.ElementAt(i).exponent, 1, 24);
                            LC_Change_After_Met[numNodes].expo[0].SR = new double[24];
                            LC_Change_After_Met[numNodes].expo[0].SR = (double[])bin.Deserialize(MS3);

                            MS3 = new MemoryStream(N.expo.ElementAt(i).DH_Array);
                            LC_Change_After_Met[numNodes].expo[0].dispH = new double[24];
                            LC_Change_After_Met[numNodes].expo[0].dispH = (double[])bin.Deserialize(MS3);
                        }
                    }
                }
            }

            // Loop through nodes LC_Change_Before_Met and find same coords in LC_Change_After_Met and compare SR/DH
            for (int i = 0; i < LC_Change_Before_Met.Length; i++)
            {
                for (int j = 0; j < LC_Change_After_Met.Length; j++)
                {
                    if (LC_Change_Before_Met[i].UTMX == LC_Change_After_Met[j].UTMX && LC_Change_Before_Met[i].UTMY == LC_Change_After_Met[j].UTMY && LC_Change_Before_Met[i].expo[0].radius == LC_Change_After_Met[j].expo[0].radius)
                    {
                        for (int k = 0; k < 24; k++)
                        {
                            Assert.AreEqual(LC_Change_Before_Met[i].expo[0].SR[k], LC_Change_After_Met[j].expo[0].SR[k], 0.00001, "Different SR" + LC_Change_Before_Met[i].UTMX.ToString() + "," + LC_Change_Before_Met[i].UTMY.ToString());
                            Assert.AreEqual(LC_Change_Before_Met[i].expo[0].dispH[k], LC_Change_After_Met[j].expo[0].dispH[k], 0.00001, "Different SR" + LC_Change_Before_Met[i].UTMX.ToString() + "," + LC_Change_Before_Met[i].UTMY.ToString());
                        }
                        break;
                    }
                }
            }

            string filename_before = testingFolder + "\\Test_Change_LCKey_Before_Met.cfm";
            string filename_after = testingFolder + "\\Test_Change_LCKey_After_Met.cfm";
            Continuum thisInstBefore = new Continuum("", false);
            Continuum thisInstAfter = new Continuum("", false);
            thisInstBefore.Open(filename_before);
            thisInstAfter.Open(filename_after);
            // Loop through met and turbine sites and compare SR/DH
            Met metBefore = thisInstBefore.metList.metItem[0];
            Met metAfter = thisInstAfter.metList.metItem[0];

            for (int k = 0; k < 24; k++)
            {
                Assert.AreEqual(metBefore.expo[0].SR[k], metAfter.expo[0].SR[k], 0.00001, "Different SR");
                Assert.AreEqual(metBefore.expo[0].dispH[k], metAfter.expo[0].dispH[k], 0.00001, "Different displacement height");
            }

            for (int i = 0; i < thisInstAfter.turbineList.TurbineCount; i++)
            {
                Turbine turbineBefore = thisInstBefore.turbineList.turbineEsts[i];
                Turbine turbineAfter = thisInstAfter.turbineList.turbineEsts[i];

                for (int k = 0; k < 24; k++)
                {
                    Assert.AreEqual(turbineBefore.expo[0].SR[k], turbineAfter.expo[0].SR[k], 0.00001, "Different SR");
                    Assert.AreEqual(turbineBefore.expo[0].dispH[k], turbineAfter.expo[0].dispH[k], 0.00001, "Different displacement height");
                }
            }

        }
    }
}
