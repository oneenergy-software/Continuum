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
    public class MCP_Tests
    {
        string testingFolder = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\TestFolder";
        string saveFolder = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder";
        Stats stat = new Stats();

        [TestMethod]
        public void Orthogonal_Regression_Test()
        {
            Continuum thisInst = new Continuum("");                      

            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\OneMetTSNotFiltWithMERRAAndMCP_1";

            thisInst.Open(fileName + ".cfm");
            thisInst.isTest = true;
            thisInst.cboMCP_Type.SelectedIndex = 0;

            // Test orthogonal regression method
            Met thisMet = thisInst.GetSelectedMet("MCP");
            Met.WSWD_Dist thisEst = new Met.WSWD_Dist();
            Met.WSWD_Dist lastEst = new Met.WSWD_Dist();

            for (int WD_ind = 0; WD_ind <= 4; WD_ind++)
                for (int TOD_ind = 0; TOD_ind <= 1; TOD_ind++)
                    for (int seasonInd = 0; seasonInd <= 1; seasonInd++)
                    {
                        thisInst.cboMCPNumHours.SelectedIndex = TOD_ind;
                        thisInst.cboMCPNumSeasons.SelectedIndex = seasonInd;
                        thisInst.cboMCPNumWD.SelectedIndex = WD_ind;
                        thisInst.DoMCP();
                        thisEst = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                                                
                        Assert.AreNotEqual(thisEst.WS, lastEst.WS, "LT Estimate did not change with setting change");
                        lastEst = thisEst;
                    }

            thisInst.Close();

        }

        [TestMethod]
        public void Variance_Ratio_Test()
        {
            // Test Variance Ratio method

            Continuum thisInst = new Continuum("");
            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\OneMetTSNotFiltWithMERRAAndMCP_1";
            thisInst.Open(fileName + ".cfm");
            thisInst.isTest = true;
            thisInst.cboMCP_Type.SelectedIndex = 2;

            Met thisMet = thisInst.GetSelectedMet("MCP");
            Met.WSWD_Dist thisEst = new Met.WSWD_Dist();
            Met.WSWD_Dist lastEst = new Met.WSWD_Dist();

            for (int WD_ind = 0; WD_ind <= 4; WD_ind++)
                for (int TOD_ind = 0; TOD_ind <= 1; TOD_ind++)
                    for (int seasonInd = 0; seasonInd <= 1; seasonInd++)
                    {
                        thisInst.cboMCPNumHours.SelectedIndex = TOD_ind;
                        thisInst.cboMCPNumSeasons.SelectedIndex = seasonInd;
                        thisInst.cboMCPNumWD.SelectedIndex = WD_ind;
                        thisInst.DoMCP();
                        thisEst = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                        Assert.AreNotEqual(thisEst.WS, lastEst.WS, "LT Estimate did not change with setting change");
                        lastEst = thisEst;
                    }

            thisInst.Close();
        }

        [TestMethod]
        public void Method_Of_Bins_Test()
        {
            // Test Method of Bins method
            // Loop through WD, TOD, Season, and WS width (0.5 and 1.0 m/s)
            Continuum thisInst = new Continuum("");
            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\OneMetTSNotFiltWithMERRAAndMCP_1";
            thisInst.Open(fileName + ".cfm");
            thisInst.isTest = true;
            thisInst.cboMCP_Type.SelectedIndex = 1;

            Met thisMet = thisInst.GetSelectedMet("MCP");
            Met.WSWD_Dist thisEst = new Met.WSWD_Dist();
            Met.WSWD_Dist lastEst = new Met.WSWD_Dist();

            for (int WD_ind = 0; WD_ind <= 4; WD_ind++)
                for (int TOD_ind = 0; TOD_ind <= 1; TOD_ind++)
                    for (int seasonInd = 0; seasonInd <= 1; seasonInd++)
                        for (int WS_width_ind = 0; WS_width_ind <= 1; WS_width_ind++)
                        {
                            if (WS_width_ind == 0)
                                thisInst.txtWS_bin_width.Text = "0.5";
                            else
                                thisInst.txtWS_bin_width.Text = "1.0";

                            thisInst.cboMCPNumHours.SelectedIndex = TOD_ind;
                            thisInst.cboMCPNumSeasons.SelectedIndex = seasonInd;
                            thisInst.cboMCPNumWD.SelectedIndex = WD_ind;
                            thisInst.DoMCP();
                            thisEst = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                            Assert.AreNotEqual(thisEst.WS, lastEst.WS, "LT Estimate did not change with setting change");
                            lastEst = thisEst;
                        }

            thisInst.Close();
        }

        [TestMethod]
        public void Matrix_Test()
        {
            // Test Matrix method
            // Loop through WD, TOD, Season, WS width (0.5 and 1.0 m/s), WS PDF Weight (0.5, 1, 2, 10), Last WS Weight (0, 1, 2)

            Continuum thisInst = new Continuum("");
            string fileName = "C:\\Users\\liz_w\\Desktop\\Continuum 3 GUI Testing\\SaveFolder\\OneMetTSNotFiltWithMERRAAndMCP_1";
            thisInst.Open(fileName + ".cfm");
            thisInst.isTest = true;
            thisInst.cboMCP_Type.SelectedIndex = 3;

            Met thisMet = thisInst.GetSelectedMet("MCP");
            Met.WSWD_Dist thisEst = new Met.WSWD_Dist();
            Met.WSWD_Dist lastEst = new Met.WSWD_Dist();

            for (int WD_ind = 0; WD_ind <= 4; WD_ind++)
                for (int TOD_ind = 0; TOD_ind <= 1; TOD_ind++)
                    for (int seasonInd = 0; seasonInd <= 1; seasonInd++)
                        for (int WS_width_ind = 0; WS_width_ind <= 1; WS_width_ind++)
                            for (int WS_PDF_ind = 0; WS_PDF_ind <= 1; WS_PDF_ind++)
                                for (int LastWS_PDF_ind = 0; LastWS_PDF_ind <= 1; LastWS_PDF_ind++)
                                {
                                    if (WS_width_ind == 0)
                                        thisInst.txtWS_bin_width.Text = "0.5";
                                    else
                                        thisInst.txtWS_bin_width.Text = "1.0";

                                    if (WS_PDF_ind == 0)
                                        thisInst.txtWS_PDF_Wgt.Text = "0.5";
                                    else if (WS_PDF_ind == 1)
                                        thisInst.txtWS_PDF_Wgt.Text = "1";
                                    
                                    if (LastWS_PDF_ind == 0)
                                        thisInst.txtLast_WS_Wgt.Text = "0";
                                    else if (LastWS_PDF_ind == 1)
                                        thisInst.txtLast_WS_Wgt.Text = "1";
                                    
                                    thisInst.cboMCPNumHours.SelectedIndex = TOD_ind;
                                    thisInst.cboMCPNumSeasons.SelectedIndex = seasonInd;
                                    thisInst.cboMCPNumWD.SelectedIndex = WD_ind;
                                    thisInst.DoMCP();
                                    thisEst = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                                    Assert.AreNotEqual(thisEst.WS, lastEst.WS, "LT Estimate did not change with setting change");
                                    lastEst = thisEst;
                                }


            thisInst.Close();
        }

            
        
    }
}
