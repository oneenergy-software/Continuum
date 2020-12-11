using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    /// <summary>
    /// Summary description for Grid_Info_Tests
    /// </summary>
    [TestClass]
    public class Grid_Info_Tests
    {
        string testingFolder = "C:\\Users\\liz_w\\Dropbox\\Continuum 3 Source code\\Critical Unit Test Docs\\Grid_Info";

        [TestMethod]
        public void FindSectorsForGrid_Test()
        {
            Continuum thisInst = new Continuum("");
                     
            StreamReader sr = new StreamReader(testingFolder + "\\WindRose 1.txt");
            double[] windRose = new double[16];
            
            for (int i = 0; i < 16; i++)
            {
                string thisData = sr.ReadLine();
                windRose[i] = Convert.ToDouble(thisData);
            }

            sr.Close();
            Grid_Info gridStats = new Grid_Info();
            bool[] Sectors_to_use = gridStats.FindSectorsForGrid(windRose);

            // All sectors should be included (true)
            for (int i = 0; i < 16; i++)
                Assert.AreEqual(true, Sectors_to_use[i], "Wrong Sector " + i);

            // Test 2
            sr = new StreamReader(testingFolder + "\\WindRose 2.txt");
            
            for (int i = 0; i < 16; i++)
            {
                string thisData = sr.ReadLine();
                windRose[i] = Convert.ToDouble(thisData);
            }
            sr.Close();
                        
            Sectors_to_use = gridStats.FindSectorsForGrid(windRose);

            Assert.AreEqual(true, Sectors_to_use[0], "Wrong Sector 0");
            Assert.AreEqual(true, Sectors_to_use[1], "Wrong Sector 1");
            Assert.AreEqual(true, Sectors_to_use[2], "Wrong Sector 2");
            Assert.AreEqual(false, Sectors_to_use[3], "Wrong Sector 3");
            Assert.AreEqual(false, Sectors_to_use[4], "Wrong Sector 4");
            Assert.AreEqual(false, Sectors_to_use[5], "Wrong Sector 5");
            Assert.AreEqual(true, Sectors_to_use[6], "Wrong Sector 6");
            Assert.AreEqual(true, Sectors_to_use[7], "Wrong Sector 7");
            Assert.AreEqual(true, Sectors_to_use[8], "Wrong Sector 8");
            Assert.AreEqual(true, Sectors_to_use[9], "Wrong Sector 9");
            Assert.AreEqual(true, Sectors_to_use[10], "Wrong Sector 10");
            Assert.AreEqual(false, Sectors_to_use[11], "Wrong Sector 11");
            Assert.AreEqual(false, Sectors_to_use[12], "Wrong Sector 12");
            Assert.AreEqual(false, Sectors_to_use[13], "Wrong Sector 13");
            Assert.AreEqual(true, Sectors_to_use[14], "Wrong Sector 14");
            Assert.AreEqual(true, Sectors_to_use[15], "Wrong Sector 15");
            
            thisInst.Close();

        }

        public void ExportGridArray()
        {
            Continuum thisInst = new Continuum("");
            

            string Filename = testingFolder + "\\Grid_Info testing.cfm";

            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            Export export = new Export();

            Met thisMet = thisInst.metList.metItem[0]; // Paulding met
            TopoInfo.TopoGrid[] thisGridArray = thisMet.gridStats.GetGridArray(thisMet.UTMX, thisMet.UTMY, thisInst);
            export.ExportGridArray(thisGridArray, testingFolder, "Paulding_Grid.csv");

            thisInst.Close();
        }

        [TestMethod]
        public void Get_Grid_Array_Test()
        {
            Continuum thisInst = new Continuum("");
            

            string Filename = testingFolder + "\\Grid_Info testing Paulding 0206.cfm";
            
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);                             

            // Test 1 Paulding model                        
            Met thisMet = thisInst.metList.metItem[0];
            TopoInfo.TopoGrid[] Grid_Array = thisMet.gridStats.GetGridArray(thisMet.UTMX, thisMet.UTMY, thisInst);

            TopoInfo topo = thisInst.topo;
            // Write grid array to csv
            Export export = new Export();
            export.ExportGridArray(Grid_Array, testingFolder, "Paulding Grid Array.csv");

            StreamReader srCont = new StreamReader(testingFolder + "\\Paulding Grid Array.csv");
            StreamReader srExcel = new StreamReader(testingFolder + "\\Paulding Grid Array Excel.csv");

            for (int i = 0; i < Grid_Array.Length; i++)
            {
                string contString = srCont.ReadLine();
                string[] contData = contString.Split(',');
                
                string excelString = srExcel.ReadLine();
                string[] excelData = excelString.Split(',');

                Assert.AreEqual(Math.Round(Convert.ToDouble(contData[0]), 0), Math.Round(Convert.ToDouble(excelData[0]),0), 0, "Wrong UTMX in Grid num " + i);
                Assert.AreEqual(Math.Round(Convert.ToDouble(contData[1]), 0), Math.Round(Convert.ToDouble(excelData[1]),0), 0, "Wrong UTMY in Grid num " + i);
            }

            thisInst.Close();
            srCont.Close();
            srExcel.Close();

            // Test 2
            thisInst = new Continuum("");
            Filename = testingFolder + "\\Grid_Info testing Great Western 0206.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            thisMet = thisInst.metList.metItem[0];
            Grid_Array = thisInst.metList.metItem[0].gridStats.GetGridArray(thisMet.UTMX, thisMet.UTMY, thisInst);

            // Write grid array to csv            
            export.ExportGridArray(Grid_Array, testingFolder, "Great Western Grid Array.csv");

            srCont = new StreamReader(testingFolder + "\\Great Western Grid Array.csv");
            srExcel = new StreamReader(testingFolder + "\\GreatWestern Grid Array Excel.csv");

            for (int i = 0; i < Grid_Array.Length; i++)
            {
                string contString = srCont.ReadLine();
                string[] contData = contString.Split(',');
                string excelString = srExcel.ReadLine();
                string[] excelData = excelString.Split(',');

                Assert.AreEqual(Math.Round(Convert.ToDouble(contData[0]), 0), Math.Round(Convert.ToDouble(excelData[0]),0), 0, "Wrong UTMX in Grid num " + i);
                Assert.AreEqual(Math.Round(Convert.ToDouble(contData[1]), 0), Math.Round(Convert.ToDouble(excelData[1]), 0), 0, "Wrong UTMY in Grid num " + i);
            }

            thisInst.Close();
            srCont.Close();
            srExcel.Close();
        }

        [TestMethod]
        public void Get_Closest_Node_Fixed_Grid()
        {
            Continuum thisInst = new Continuum("");
            

            string Filename = testingFolder + "\\Grid_Info testing Paulding 0206.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            double UTMX = 710541;
            double UTMY = 4552553;

            TopoInfo.TopoGrid closestNode = thisInst.topo.GetClosestNodeFixedGrid(UTMX, UTMY, 250, 12000);
            
            Assert.AreEqual(closestNode.UTMX, 710482, 1, "Wrong UTMX");
            Assert.AreEqual(closestNode.UTMY, 4552458, 1, "Wrong UTMY");

            thisInst.Close();
        }
                
        public void ExportGridExpos()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\Grid_Info testing Paulding 0206.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            int thisX = 710541;
            int thisY = 4552553;
            TopoInfo.TopoGrid[] Grid_Array = thisInst.metList.metItem[0].gridStats.GetGridArray(thisX, thisY, thisInst);

            for (int i = 0; i < Grid_Array.Length; i++)
                Grid_Array[i].elev = thisInst.topo.CalcElevs(Grid_Array[i].UTMX, Grid_Array[i].UTMY);

            Grid_Info thisGridInfo = new Grid_Info();
            Node_table[] dummyNulls = null;
            thisGridInfo.CalcGridStats(0, ref Grid_Array, ref dummyNulls, null, thisInst, true, testingFolder + "\\Paulding");

            thisInst.Close();
        }

        [TestMethod]
        public void Calc_Grid_Stats_Test()
        {
            Continuum thisInst = new Continuum("");
            

            string Filename = testingFolder + "\\Grid_Info testing Paulding 0206.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Test 1
            int thisX = 710541;
            int thisY = 4552553;
            
            TopoInfo.TopoGrid[] Grid_Array = thisInst.metList.metItem[0].gridStats.GetGridArray(thisX, thisY, thisInst);

            for (int i = 0; i < Grid_Array.Length; i++)
                Grid_Array[i].elev = thisInst.topo.CalcElevs(Grid_Array[i].UTMX, Grid_Array[i].UTMY);

            Grid_Info thisGridInfo = new Grid_Info();
            Node_table[] dummyNulls = null;
            thisGridInfo.CalcGridStats(0, ref Grid_Array, ref dummyNulls, null, thisInst, false, "");

            Assert.AreEqual(1.1504, thisGridInfo.stats[0].P10_UW[4], 0.01, "Wrong P10 UW Expo for Test 1");
            Assert.AreEqual(-0.43314, thisGridInfo.stats[0].P10_DW[4], 0.01, "Wrong P10 DW Expo for Test 1");

            thisInst.Close();
        }
    }
    
}
