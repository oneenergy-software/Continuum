using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Linq.Expressions;

namespace ContinuumNS
{
    /// <summary> Class with functions to open and save CFM files and update GUI plots and tables on all tabs. </summary>
    public class Update
    {
        /// <summary> List view item used to update tables. </summary>
        ListViewItem objListItem = new ListViewItem();

        /// <summary> Holds booleans to determine which parameters to plot on Advanced tab "Path of Nodes" plot and table. </summary>
        public struct Advanced_to_show
        {
            /// <summary> Show UTM X coordinates if true. </summary>
            public bool showUTMX;
            /// <summary> Show UTM Y coordinates if true. </summary>
            public bool showUTMY;
            /// <summary> Show elevation if true. </summary>
            public bool showElevation;
            /// <summary> Show P10 upwind exposure if true. </summary>
            public bool showP10UW;
            /// <summary> Show P10 downwind exposure if true. </summary>
            public bool showP10DW;
            /// <summary> Show upwind exposure if true. </summary>
            public bool showUWExpo;
            /// <summary> Show downwind exposure if true. </summary>
            public bool showDWExpo;
            /// <summary> Show upwind surface roughness if true. </summary>
            public bool showUWRough;
            /// <summary> Show downwind surface roughness if true. </summary>
            public bool showDWRough;
            /// <summary> Show upwind displacement height if true. </summary>
            public bool showUWDispH;
            /// <summary> Show downwind displacement height if true. </summary>
            public bool showDWDispH;
            /// <summary> Show change in wind speed due to change in upwind exposure if true. </summary>
            public bool show_dWS_UWExpo;
            /// <summary> Show change in wind speed due to change in downwind exposure if true. </summary>
            public bool show_dWS_DWExpo;
            /// <summary> Show change in wind speed due to change in upwind SR/DH if true. </summary>
            public bool show_dWS_UW_SRDH;
            /// <summary> Show change in wind speed due to change in downwind SR/DH if true. </summary>
            public bool show_dWS_DW_SRDH;
            /// <summary> Show wind speed estimate if true. </summary>
            public bool showWS_Est;
            /// <summary> Show equivalent wind speed estimate (adjusted for flow rotation) if true. </summary>
            public bool showEquivWS;
            /// <summary> Show actual wind speed if true. </summary>
            public bool showActualWS;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Updates all plots, tables and textboxes on Net Turb Ests tab. </summary>
        public void NetTurbineEstsTAB(Continuum thisInst)
        {
            WakeModelList(thisInst);
            WakedTurbList(thisInst);
            WakedWSDistPlot(thisInst);
            TurbsByString(thisInst);
            WakeLossMap(thisInst);
        }

        /// <summary> Updates all buttons that are color-coded to be either red or green depending on state of analysis. </summary>
        public void ColoredButtons(Continuum thisInst) // 
        {
            if (thisInst.IsHandleCreated == false)
                return;

            // Input tab buttons
            if (thisInst.topo.gotTopo == true)
                thisInst.btnLoadXYZ.BackColor = Color.MediumSeaGreen;
            else
                thisInst.btnLoadXYZ.BackColor = Color.LightCoral;

            if (thisInst.topo.gotSR == true)
                thisInst.btnImportRoughness.BackColor = Color.MediumSeaGreen;
            else
                thisInst.btnImportRoughness.BackColor = Color.LightCoral;

            if (thisInst.metList.isTimeSeries || thisInst.metList.ThisCount == 0)
                thisInst.btnModHeight.Enabled = true;
            else
                thisInst.btnModHeight.Enabled = false;

            // btnViewModNLCD is modified in Update.LC_KeySelected sub

            if (thisInst.metList.ThisCount > 0 && thisInst.metList.isTimeSeries == false)
                thisInst.btnImportTAB.BackColor = Color.MediumSeaGreen;
            else if (thisInst.metList.isTimeSeries == false)
                thisInst.btnImportTAB.BackColor = Color.LightCoral;
            else if (thisInst.metList.ThisCount > 0 && thisInst.metList.isTimeSeries == true)
                thisInst.btnImportTAB.BackColor = Color.Gray;
            else if (thisInst.metList.ThisCount == 0)
                thisInst.btnImportTAB.BackColor = Color.LightCoral;

            if (thisInst.metList.ThisCount > 0 && thisInst.metList.isTimeSeries == true)
                thisInst.btnImportMetTS.BackColor = Color.MediumSeaGreen;
            else if (thisInst.metList.isTimeSeries == true)
                thisInst.btnImportMetTS.BackColor = Color.LightCoral;
            else if (thisInst.metList.ThisCount > 0 && thisInst.metList.isTimeSeries == false)
                thisInst.btnImportMetTS.BackColor = Color.Gray;
            else if (thisInst.metList.ThisCount == 0)
                thisInst.btnImportMetTS.BackColor = Color.LightCoral;

            if (thisInst.modelList.HaveRequiredModels(thisInst.metList))
            {
                thisInst.btnAnalyzeMets.BackColor = Color.MediumSeaGreen;
                thisInst.btnAnalyzeMets.Invoke(new Action(() => thisInst.btnAnalyzeMets.Enabled = false));
            }
            else
            {
                thisInst.btnAnalyzeMets.BackColor = Color.LightCoral;

                if (thisInst.topo.gotTopo)
                    thisInst.btnAnalyzeMets.Invoke(new Action(() => thisInst.btnAnalyzeMets.Enabled = true));
                else
                    thisInst.btnAnalyzeMets.Invoke(new Action(() => thisInst.btnAnalyzeMets.Enabled = false));
            }

            if (thisInst.turbineList.TurbineCount > 0)
                thisInst.btnTurbines.BackColor = Color.MediumSeaGreen;
            else
                thisInst.btnTurbines.BackColor = Color.LightCoral;

            thisInst.turbineList.AreTurbCalcsDone(thisInst);
            if (thisInst.turbineList.turbineCalcsDone == true)
            {
                thisInst.btnGenTurbEsts.BackColor = Color.MediumSeaGreen;
                thisInst.btnGenTurbGross.BackColor = Color.MediumSeaGreen;
            }
            else
            {
                thisInst.btnGenTurbEsts.BackColor = Color.LightCoral;
                thisInst.btnGenTurbGross.BackColor = Color.LightCoral;
            }

            if (thisInst.metList.isTimeSeries == false || thisInst.metList.allMCPd == false)
            {
                thisInst.chkCreateTurbTS.Enabled = false;
                thisInst.chkCreateTurbTS.CheckState = CheckState.Unchecked;
            }
            else
            {
                thisInst.chkCreateTurbTS.Enabled = true;
                if (thisInst.turbineList.genTimeSeries)
                    thisInst.chkCreateTurbTS.CheckState = CheckState.Checked;
                else
                    thisInst.chkCreateTurbTS.CheckState = CheckState.Unchecked;
            }

            // MERRA2 tab
            MERRA thisMERRA = thisInst.GetSelectedMERRA();
            if (thisMERRA.GotWindTS(thisInst.UTM_conversions))
                thisInst.btn_Import_MERRA.BackColor = Color.MediumSeaGreen;
            else if (thisMERRA.GotWindTS(thisInst.UTM_conversions) == false)
                thisInst.btn_Import_MERRA.BackColor = Color.LightCoral;
            else
                thisInst.btn_Import_MERRA.BackColor = Color.Gray;

            // Met data QC tab
            if (thisInst.metList.ThisCount == 0)
                thisInst.chkDisableFilter.Enabled = true;
            else if (thisInst.metList.ThisCount > 0 && thisInst.metList.isTimeSeries == false)
                thisInst.chkDisableFilter.Enabled = false;

            // MCP tab
            Met thisMet = thisInst.GetSelectedMet("MCP");
            bool gotMERRA = false;

            // Check to see if have MERRA data
            if (thisMet.name != null)
            {
                UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                int offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
                gotMERRA = thisInst.merraList.GotMERRA(theseLL.latitude, theseLL.longitude);
            }

            if (thisMet.name == null)
            {
                thisInst.btnDoMCP.BackColor = Color.LightCoral;
                thisInst.btnDoMCP.Enabled = false;
            }
            else if (thisMet.mcp == null && gotMERRA == false)
            {
                thisInst.btnDoMCP.BackColor = Color.LightCoral;
                thisInst.btnDoMCP.Enabled = false;
            }
            else if (thisMet.mcp == null && gotMERRA == true)
            {
                thisInst.btnDoMCP.BackColor = Color.LightCoral;
                thisInst.btnDoMCP.Enabled = true;
            }
            else if (thisMet.mcp.HaveMCP_Estimate("Any") == false && gotMERRA == true)
            {
                thisInst.btnDoMCP.BackColor = Color.LightCoral;
                thisInst.btnDoMCP.Enabled = true;
            }
            else if (thisMet.mcp.HaveMCP_Estimate("Any") == true)
            {
                thisInst.btnDoMCP.BackColor = Color.MediumSeaGreen;
                thisInst.btnDoMCP.Enabled = false;
            }

            // Check to see if all mets have MCP
            thisInst.metList.AreAllMetsMCPd();
            bool haveMERRAForAll = false;

            for (int i = 0; i < thisInst.metList.ThisCount; i++)
            {
                UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisInst.metList.metItem[i].UTMX, thisInst.metList.metItem[i].UTMY);
                haveMERRAForAll = thisInst.merraList.GotMERRA(theseLL.latitude, theseLL.longitude);

                if (haveMERRAForAll == false)
                    break;
            }

            if (thisInst.metList.allMCPd)
            {
                thisInst.btnDoMCPAllMets.BackColor = Color.MediumSeaGreen;
                thisInst.btnDoMCPAllMets.Enabled = false;
            }
            else if (haveMERRAForAll)
            {
                thisInst.btnDoMCPAllMets.BackColor = Color.LightCoral;
                thisInst.btnDoMCPAllMets.Enabled = true;
            }
            else
            {
                thisInst.btnDoMCPAllMets.BackColor = Color.LightCoral;
                thisInst.btnDoMCPAllMets.Enabled = false;
            }

            // Gross Turb Ests and MERRA2 tab

            if (thisInst.turbineList.PowerCurveCount > 0)
            {
                thisInst.btnImportCRV.BackColor = Color.MediumSeaGreen;
                thisInst.btnImportCRV_MERRA.BackColor = Color.MediumSeaGreen;
                thisInst.btnSiteSuitImportCRV.BackColor = Color.MediumSeaGreen;
            }
            else
            {
                thisInst.btnImportCRV.BackColor = Color.LightCoral;
                thisInst.btnImportCRV_MERRA.BackColor = Color.LightCoral;
                thisInst.btnSiteSuitImportCRV.BackColor = Color.LightCoral;
            }

            // Net Turb Ests tab
            if (thisInst.wakeModelList.NumWakeModels > 0)
                thisInst.btnWakeLossCalc.BackColor = Color.MediumSeaGreen;
            else
                thisInst.btnWakeLossCalc.BackColor = Color.LightCoral;

            if (thisInst.wakeModelList.NumCompleteWakeGridMaps > 0)
                thisInst.btnCreateWakeMap.BackColor = Color.MediumSeaGreen;
            else
                thisInst.btnCreateWakeMap.BackColor = Color.LightCoral;

            // Uncertainty Tab

            if (thisInst.metPairList.RoundRobinCount > 0)
                thisInst.btnDoRRCalcs.BackColor = Color.MediumSeaGreen;
            else
                thisInst.btnDoRRCalcs.BackColor = Color.LightCoral;

            if (thisInst.metList.isTimeSeries && thisInst.metList.ThisCount <= 1)
                thisInst.btnDoRRCalcs.Enabled = false;
            else
                thisInst.btnDoRRCalcs.Enabled = true;

            // Site Suitability Tab
            if (thisInst.turbineList.PowerCurveCount == 0 || thisInst.metList.ThisCount == 0)
            {
                thisInst.btnRunIceThrow.BackColor = Color.LightCoral;
                thisInst.btnRunIceThrow.Invoke(new Action(() => thisInst.btnRunIceThrow.Enabled = false));

                thisInst.btnRunShadowFlicker.BackColor = Color.LightCoral;
                thisInst.btnRunShadowFlicker.Invoke(new Action(() => thisInst.btnRunShadowFlicker.Enabled = false));
            }

            if (thisInst.siteSuitability.flickerMap.Length == 0 && thisInst.turbineList.PowerCurveCount > 0)
            {
                thisInst.btnRunShadowFlicker.BackColor = Color.LightCoral;
                thisInst.btnRunShadowFlicker.Invoke(new Action(() => thisInst.btnRunShadowFlicker.Enabled = true));
            }
            else if (thisInst.siteSuitability.flickerMap.Length > 0)
            {
                thisInst.btnRunShadowFlicker.BackColor = Color.MediumSeaGreen;
                thisInst.btnRunShadowFlicker.Invoke(new Action(() => thisInst.btnRunShadowFlicker.Enabled = true));
            }

            if (thisInst.siteSuitability.yearlyIceHits.Length == 0 && thisInst.turbineList.PowerCurveCount > 0 && thisInst.metList.ThisCount > 0)
            {
                thisInst.btnRunIceThrow.BackColor = Color.LightCoral;
                thisInst.btnRunIceThrow.Invoke(new Action(() => thisInst.btnRunIceThrow.Enabled = true));
            }
            else if (thisInst.siteSuitability.yearlyIceHits.Length != 0)
            {
                thisInst.btnRunIceThrow.BackColor = Color.MediumSeaGreen;
                thisInst.btnRunIceThrow.Invoke(new Action(() => thisInst.btnRunIceThrow.Enabled = true));
            }

            if (thisInst.siteSuitability.soundMap.Length == 0 && thisInst.turbineList.PowerCurveCount == 0)
            {
                thisInst.btnRunSoundModel.BackColor = Color.LightCoral;
                thisInst.btnRunSoundModel.Invoke(new Action(() => thisInst.btnRunSoundModel.Enabled = false));
            }
            else if (thisInst.siteSuitability.soundMap.Length == 0)
            {
                thisInst.btnRunSoundModel.BackColor = Color.LightCoral;
                thisInst.btnRunSoundModel.Invoke(new Action(() => thisInst.btnRunSoundModel.Enabled = true));
            }
            else
            {
                thisInst.btnRunSoundModel.BackColor = Color.MediumSeaGreen;
                thisInst.btnRunSoundModel.Invoke(new Action(() => thisInst.btnRunSoundModel.Enabled = true));
            }

            // Exceedance Tab
            if (thisInst.turbineList.exceed == null)
                thisInst.btnDoMonteCarlo.BackColor = Color.LightCoral;
            else if (thisInst.turbineList.exceed.compositeLoss.isComplete == false)
                thisInst.btnDoMonteCarlo.BackColor = Color.LightCoral;
            else
                thisInst.btnDoMonteCarlo.BackColor = Color.MediumSeaGreen;

            // Advanced Tab
            if (thisInst.metList.isTimeSeries == false || thisInst.metList.allMCPd == false)
            {
                thisInst.cboSeasonAdvanced.Enabled = false;
                thisInst.cboTODAdvanced.Enabled = false;
            }
            else if (thisInst.metList.isTimeSeries == true && thisInst.metList.allMCPd == true)
            {
                if (thisInst.metList.numSeason == 1)
                    thisInst.cboSeasonAdvanced.Enabled = false;
                else
                    thisInst.cboSeasonAdvanced.Enabled = true;

                if (thisInst.metList.numTOD == 1)
                    thisInst.cboTODAdvanced.Enabled = false;
                else
                    thisInst.cboTODAdvanced.Enabled = true;
            }
        }

        /// <summary> Updates net turbine  plot that shows estimates for each specified string. </summary>        
        public void TurbsByString(Continuum thisInst)
        {
            double maxVal = 0;
            double minVal = 10000000;
            Turbine lastTurb = null;

            TopoInfo topo = new TopoInfo();

            int WD_Ind = thisInst.GetWD_ind("Net");

            int numWD = thisInst.GetNumWD();
            Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();

            thisInst.plotTurbsByString.Model = new PlotModel();
            var model = thisInst.plotTurbsByString.Model;
            model.IsLegendVisible = false;

            if (thisInst.metList.ThisCount == 0 || thisWakeModel == null || numWD == 0 || WD_Ind == -1)
            {
                thisInst.plotTurbsByString.Refresh();
                return;
            }

            int allStringCount = thisInst.turbineList.NumStrings;
            int plotStringCount = thisInst.chkStrings.CheckedItems.Count;

            int whatToPlot;
            try
            {
                whatToPlot = thisInst.cboWakePlot.SelectedIndex;
                if (whatToPlot == -1)
                {
                    thisInst.plotTurbsByString.Refresh();
                    return;
                }
            }
            catch
            {
                thisInst.plotTurbsByString.Refresh();
                return;
            }

            if (plotStringCount > 0)
            {
                int plotInd = 0;

                if (thisInst.turbineList.GotEst("Net", thisWakeModel.powerCurve, thisWakeModel) == true)
                {
                    for (int i = 0; i <= allStringCount - 1; i++)
                    {
                        int Num_turbs_in_str = 0;
                        for (int j = 0; j <= thisInst.turbineList.TurbineCount - 1; j++)
                            if (thisInst.turbineList.turbineEsts[j].stringNum == i + 1)
                                Num_turbs_in_str++;

                        double[] stringXDist = new double[Num_turbs_in_str];
                        double[] turbParam = new double[Num_turbs_in_str];

                        int turbInd = 0;
                        bool isChecked = false;

                        for (int j = 0; j < plotStringCount; j++)
                        {
                            if (thisInst.chkStrings.CheckedItems[j].ToString() == (i + 1).ToString())
                            {
                                isChecked = true;
                                break;
                            }
                        }

                        if (isChecked == true)
                        {
                            for (int j = 0; j < thisInst.turbineList.TurbineCount; j++)
                            {
                                Turbine thisTurb = thisInst.turbineList.turbineEsts[j];
                                Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(thisWakeModel);
                                Turbine.Net_Energy_Est netEst = thisTurb.GetNetEnergyEst(thisWakeModel);

                                if (thisTurb.stringNum == i + 1)
                                {
                                    if (turbInd == 0)
                                    {
                                        lastTurb = thisTurb;
                                        stringXDist[turbInd] = 0;
                                    }
                                    else
                                    {
                                        int dist = (int)topo.CalcDistanceBetweenPoints(thisTurb.UTMX, thisTurb.UTMY, lastTurb.UTMX, lastTurb.UTMY);
                                        stringXDist[turbInd] = dist + stringXDist[turbInd - 1];
                                        lastTurb = thisTurb;
                                    }

                                    if (whatToPlot == 0)
                                    {
                                        if (WD_Ind == numWD)
                                            turbParam[turbInd] = avgEst.waked.WS;
                                        else
                                            turbParam[turbInd] = avgEst.waked.sectorWS[WD_Ind];
                                    }
                                    else if (whatToPlot == 1)
                                    {
                                        if (WD_Ind == numWD)
                                            turbParam[turbInd] = netEst.wakeLoss;
                                        else
                                            turbParam[turbInd] = netEst.sectorWakeLoss[WD_Ind];
                                    }
                                    else
                                    {
                                        if (WD_Ind == numWD)
                                            turbParam[turbInd] = netEst.AEP;
                                        else
                                            turbParam[turbInd] = netEst.sectorEnergy[WD_Ind];
                                    }

                                    if (turbParam[turbInd] > maxVal)
                                        maxVal = turbParam[turbInd];

                                    if (turbParam[turbInd] < minVal)
                                        minVal = turbParam[turbInd];

                                    turbInd++;
                                }
                            }

                            LineSeries thisSeries = new LineSeries();
                            thisSeries.Title = "string " + i + 1;

                            for (int j = 0; j < Num_turbs_in_str; j++)
                                thisSeries.Points.Add(new DataPoint(Math.Round(stringXDist[j], 3), Math.Round(turbParam[j], 3)));

                            model.Series.Add(thisSeries);

                            plotInd++;
                        }
                    }
                }
            }

            string header = "";
            string yLabel1 = "";

            if (whatToPlot == 0)
            {
                header = "Net WS Estimates along Turbine Strings";
                yLabel1 = "Wind Speed, m/s";
            }
            else if (whatToPlot == 1)
            {
                header = "Wake Loss Estimate along Turbine Strings";
                yLabel1 = "Wake Loss, %";
            }
            else
            {
                header = "Net AEP Estimate along Turbine Strings";
                yLabel1 = "Net AEP, MWh";
            }

            model.Title = header;
            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Distance, m"; ;
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = yLabel1;

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            thisInst.plotTurbsByString.Refresh();

        }

        /// <summary> Updates waked wind speed map on Net Turbine Ests tab </summary>        
        public void WakeLossMap(Continuum thisInst)
        {

            int numWakeModels = thisInst.wakeModelList.NumWakeModels;

            thisInst.plotWakeMap.Model = new PlotModel();
            var model = thisInst.plotWakeMap.Model;

            if (numWakeModels > 0 && thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false && thisInst.BW_worker.BackgroundWorker_Map.IsBusy == false)
            {
                int plotInd = 0;

                try
                {
                    plotInd = thisInst.cboWakePlot.SelectedIndex;
                    if (plotInd == -1)
                        thisInst.cboWakePlot.SelectedIndex = 0;
                }
                catch
                {
                    thisInst.plotWakeMap.Refresh();
                    return;
                }

                if (thisInst.wakeModelList.NumWakeGridMaps == 0)
                {
                    thisInst.plotWakeMap.Refresh();
                    return;
                }

                WakeCollection.WakeGridMap thisGrid = thisInst.GetSelectedWakeGrid();

                if (thisGrid.isComplete == false)
                {
                    thisInst.plotWakeMap.Refresh();
                    return;
                }

                int WD_Ind = thisInst.GetWD_ind("Net");
                int numWD = thisInst.GetNumWD();
                double thisMin = 0;
                double thisMax = 0;
                int newNumLevels = 10;
                double intWidth = 0;

                if (thisInst.txtWakeMin.Text != "")
                {
                    try
                    {
                        thisMin = Convert.ToSingle(thisInst.txtWakeMin.Text);
                    }
                    catch
                    {
                        thisMin = thisInst.wakeModelList.FindMin(thisGrid, WD_Ind, plotInd) * 0.99f;
                        thisInst.txtWakeMin.Text = Math.Round(thisMin, 3).ToString();
                    }
                }
                else
                {
                    thisMin = thisInst.wakeModelList.FindMin(thisGrid, WD_Ind, plotInd) * 0.99f;
                    thisInst.txtWakeMin.Text = Math.Round(thisMin, 3).ToString();
                }

                if (thisInst.txtWakeMax.Text != "")
                {
                    try
                    {
                        thisMax = Convert.ToSingle(thisInst.txtWakeMax.Text);
                    }
                    catch
                    {
                        thisMax = thisInst.wakeModelList.FindMax(thisGrid, WD_Ind, plotInd) * 1.01f;
                        thisInst.txtWakeMax.Text = Math.Round(thisMax, 3).ToString();
                    }
                }
                else
                {
                    thisMax = thisInst.wakeModelList.FindMax(thisGrid, WD_Ind, plotInd) * 1.01f;
                    thisInst.txtWakeMax.Text = Math.Round(thisMax, 3).ToString();
                }

                if (thisMin == thisMax)
                {
                    thisMin = thisMin - thisMin * 0.1f;
                    thisMax = thisMax + thisMax * 0.1f;
                }

                try
                {
                    intWidth = Convert.ToSingle(thisInst.txtWakeInterval.Text);
                }
                catch
                {
                    intWidth = (thisMax - thisMin) / (newNumLevels - 1);
                    thisInst.txtWakeInterval.Text = Math.Round(intWidth, 2).ToString();
                }

                if (thisInst.chkWakeAuto.Checked == true)
                {
                    thisMin = thisInst.wakeModelList.FindMin(thisGrid, WD_Ind, plotInd) * 0.99f;
                    thisMax = thisInst.wakeModelList.FindMax(thisGrid, WD_Ind, plotInd) * 1.01f;

                    if (plotInd == 0)
                    {
                        thisMin = Math.Round(thisMin, 2);
                        thisMax = Math.Round(thisMax, 2);

                        if (thisMin == thisMax)
                        {
                            thisMin = thisMin - thisMin * 0.02f;
                            thisMax = thisMax + thisMax * 0.02f;
                        }
                        intWidth = (thisMax - thisMin) / (newNumLevels - 1);

                        intWidth = Math.Round(intWidth, 2);
                    }
                    else if (plotInd == 1)
                    {
                        thisMin = Math.Round(thisMin, 1);
                        thisMax = Math.Round(thisMax, 1);

                        if (thisMin == thisMax)
                        {
                            thisMin = thisMin - thisMin * 0.02f;
                            thisMax = thisMax + thisMax * 0.02f;
                        }
                        intWidth = (thisMax - thisMin) / (newNumLevels - 1);

                        intWidth = Math.Round(intWidth, 2);
                    }
                    else
                    {
                        thisMin = Math.Round(thisMin, 0);
                        thisMax = Math.Round(thisMax, 0);

                        if (thisMin == thisMax)
                        {
                            thisMin = thisMin - thisMin * 0.02f;
                            thisMax = thisMax + thisMax * 0.02f;
                        }
                        intWidth = (thisMax - thisMin) / (newNumLevels - 1);

                        intWidth = Math.Round(intWidth, 0);
                    }

                    thisInst.txtWakeMin.Text = thisMin.ToString();
                    thisInst.txtWakeMax.Text = thisMax.ToString();
                    thisInst.txtWakeInterval.Text = intWidth.ToString();
                }

                int numX = thisGrid.numX;
                int numY = thisGrid.numY;

                double[,] param = new double[numX, numY];

                for (int i = 0; i < numX; i++)
                {
                    for (int j = 0; j < numY; j++)
                    {
                        if (plotInd == 0)
                        {
                            if (WD_Ind == numWD)
                                param[i, j] = Math.Round(thisGrid.wakeGrids[i, j].wakedWS, 3);
                            else
                                param[i, j] = Math.Round(thisGrid.wakeGrids[i, j].sectorWakedWS[WD_Ind], 3);
                        }
                        else if (plotInd == 1)
                        {
                            if (WD_Ind == numWD)
                                param[i, j] = Math.Round(thisGrid.wakeGrids[i, j].wakeLoss * 100, 1);
                            else
                                param[i, j] = Math.Round(thisGrid.wakeGrids[i, j].sectorWakeLoss[WD_Ind] * 100, 1);
                        }
                        else
                        {
                            if (WD_Ind == numWD)
                                param[i, j] = Math.Round(thisGrid.wakeGrids[i, j].netEnergy, 1);
                            else
                                param[i, j] = Math.Round(thisGrid.wakeGrids[i, j].sectorNetEnergy[WD_Ind], 1);

                        }
                    }
                }

                var wakeMapSeries = new HeatMapSeries
                {
                    X0 = thisGrid.minUTMX,
                    Y0 = thisGrid.minUTMY,
                    X1 = thisGrid.minUTMX + thisGrid.wakeGridReso * thisGrid.numX,
                    Y1 = thisGrid.minUTMY + thisGrid.wakeGridReso * thisGrid.numY,
                    Data = param
                };

                model.Series.Add(wakeMapSeries);

                if (plotInd == 0)
                    model.LegendTitle = "Wind Speed [m/s]";
                else if (plotInd == 1)
                    model.LegendTitle = "Wake Loss [%]";
                else if (plotInd == 2)
                    model.LegendTitle = "Net Energy Prod [MWh]";

                model.Axes.Add(new LinearColorAxis
                {
                    Position = AxisPosition.Right,
                    Palette = OxyPalettes.Jet(20),
                    HighColor = OxyColors.Red,
                    LowColor = OxyColors.Gray,
                    Minimum = thisMin,
                    Maximum = thisMax
                });

                WakeMapLabels(thisInst);
            }

            thisInst.plotWakeMap.Refresh();

        }

        /// <summary> Updates list on Net Turbine Ests tab that lists the wake models and wake maps that have been created </summary> 
        public void WakeModelList(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            thisInst.lstWakeModels.Items.Clear();
            thisInst.cboMonthlyWakeModel.Items.Clear();
            thisInst.cboMonthlyWakeModel.Text = "";
            thisInst.cboExceedWake.Items.Clear();
            thisInst.cboExceedWake.Text = "";

            if (thisInst.wakeModelList == null)
            {
                thisInst.okToUpdate = true;
                return;
            }

            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Monthly");

            for (int i = 0; i < thisInst.wakeModelList.NumWakeModels; i++)
            {
                Wake_Model thisWakeModel = thisInst.wakeModelList.wakeModels[i];

                string wakeModelStr = "";

                if (thisWakeModel.wakeModelType == 0)
                    wakeModelStr = "Eddy Viscosity";
                else if (thisWakeModel.wakeModelType == 1)
                    wakeModelStr = "DAWM with EV";
                else if (thisWakeModel.wakeModelType == 2)
                    wakeModelStr = "Jensen";

                ListViewItem objListItem = thisInst.lstWakeModels.Items.Add(wakeModelStr);
                objListItem.SubItems.Add(thisWakeModel.comboMethod);
                objListItem.SubItems.Add(thisWakeModel.powerCurve.name);
                objListItem.SubItems.Add(thisWakeModel.wakeRechargeRate.ToString());
                objListItem.SubItems.Add(thisWakeModel.horizWakeExp.ToString());
                objListItem.SubItems.Add(thisWakeModel.ambTI.ToString());
                objListItem.SubItems.Add(thisWakeModel.DW_Spacing.ToString());
                objListItem.SubItems.Add(thisWakeModel.CW_Spacing.ToString());
                objListItem.SubItems.Add(Math.Round(thisWakeModel.ambRough, 3).ToString());

                if (thisWakeModel.powerCurve.name == powerCurve.name)
                {
                    string wakeModelFullString = thisInst.wakeModelList.CreateWakeModelString(thisWakeModel);
                    thisInst.cboMonthlyWakeModel.Items.Add(wakeModelFullString);
                    thisInst.cboExceedWake.Items.Add(wakeModelFullString);
                }

                // See if there is a wake grid map that uses this wake model (table of wake models shows each wake grid map that is created)
                int numWakeGridMaps = thisInst.wakeModelList.NumWakeGridMaps;
                for (int j = 0; j < numWakeGridMaps; j++)
                {
                    if (thisInst.wakeModelList.wakeGridMaps[j].isComplete == true &&
                        thisInst.wakeModelList.IsSameWakeModel(thisInst.wakeModelList.wakeGridMaps[j].wakeModel, thisWakeModel))
                    {
                        WakeCollection.WakeGridMap thisWakeGrid = thisInst.wakeModelList.wakeGridMaps[j];
                        objListItem.SubItems.Add(thisWakeGrid.minUTMX.ToString());
                        objListItem.SubItems.Add(thisWakeGrid.minUTMY.ToString());
                        int gridReso = thisWakeGrid.wakeGridReso;
                        double maxX = thisWakeGrid.minUTMX + thisWakeGrid.numX * gridReso;
                        double maxY = thisWakeGrid.minUTMY + thisWakeGrid.numY * gridReso;
                        objListItem.SubItems.Add(maxX.ToString());
                        objListItem.SubItems.Add(maxY.ToString());
                        objListItem.SubItems.Add(gridReso.ToString());

                        // Adds any additional (complete) Wake Grid Maps that used the same Wake Model
                        for (int k = j + 1; k < numWakeGridMaps; k++)
                        {
                            if (thisInst.wakeModelList.wakeGridMaps[k].isComplete == true &&
                                thisInst.wakeModelList.IsSameWakeModel(thisInst.wakeModelList.wakeGridMaps[k].wakeModel, thisWakeModel))
                            {
                                objListItem = thisInst.lstWakeModels.Items.Add(wakeModelStr);
                                objListItem.SubItems.Add(thisWakeModel.comboMethod);
                                objListItem.SubItems.Add(thisWakeModel.powerCurve.name);
                                objListItem.SubItems.Add(thisWakeModel.wakeRechargeRate.ToString());
                                objListItem.SubItems.Add(thisWakeModel.horizWakeExp.ToString());
                                objListItem.SubItems.Add(thisWakeModel.ambTI.ToString());
                                objListItem.SubItems.Add(thisWakeModel.DW_Spacing.ToString());
                                objListItem.SubItems.Add(thisWakeModel.CW_Spacing.ToString());
                                objListItem.SubItems.Add(thisWakeModel.ambRough.ToString());

                                objListItem.SubItems.Add(thisInst.wakeModelList.wakeGridMaps[k].minUTMX.ToString());
                                objListItem.SubItems.Add(thisInst.wakeModelList.wakeGridMaps[k].minUTMY.ToString());
                                gridReso = thisInst.wakeModelList.wakeGridMaps[k].wakeGridReso;
                                maxX = thisInst.wakeModelList.wakeGridMaps[k].minUTMX + thisInst.wakeModelList.wakeGridMaps[k].numX * gridReso;
                                maxY = thisInst.wakeModelList.wakeGridMaps[k].minUTMY + thisInst.wakeModelList.wakeGridMaps[k].numY * gridReso;
                                objListItem.SubItems.Add(maxX.ToString());
                                objListItem.SubItems.Add(maxY.ToString());
                                objListItem.SubItems.Add(gridReso.ToString());
                            }
                        }
                        break;

                    }
                }

            }

            if (thisInst.lstWakeModels.Items.Count > 0)
            {
                try
                {
                    thisInst.lstWakeModels.Items[0].Selected = true;
                    thisInst.cboMonthlyWakeModel.SelectedIndex = 0;
                    thisInst.cboExceedWake.SelectedIndex = 0;
                }
                catch
                {
                    thisInst.okToUpdate = true;
                    return;
                }
            }

            ColoredButtons(thisInst);
            thisInst.okToUpdate = true;
        }

        /// <summary> Updates the table on Net Turbine Ests tab which lists the net turbine estimates </summary> 
        public void WakedTurbList(Continuum thisInst)
        {
            thisInst.lstWakedTurbs.Items.Clear();

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Net");
            int checkedTurbCount = 0;

            if (checkedTurbines != null)
                checkedTurbCount = checkedTurbines.Length;
            else
                return;

            Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();

            if (thisWakeModel == null)
            {
                thisInst.txtWakeLoss.Text = "";
                thisInst.txtOtherLosses.Text = "";
                return;
            }

            if (thisInst.metList.ThisCount == 0) return;

            int numWD = thisInst.GetNumWD();
            int WD_Ind = thisInst.GetWD_ind("Net");
            double avgWakeLoss = 0;

            if (WD_Ind == -1) return;

            for (int i = 0; i < checkedTurbCount; i++)
            {
                Turbine thisTurb = checkedTurbines[i];
                Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(thisWakeModel);

                if (avgEst.waked.WS != 0)
                {
                    objListItem = thisInst.lstWakedTurbs.Items.Add(thisTurb.name);
                    objListItem.SubItems.Add(thisTurb.stringNum.ToString());
                    objListItem.SubItems.Add(Math.Round(thisTurb.elev, 1).ToString());

                    if (WD_Ind == numWD)
                        objListItem.SubItems.Add(Math.Round(avgEst.waked.WS, 3).ToString());
                    else
                        objListItem.SubItems.Add(Math.Round(avgEst.waked.sectorWS[WD_Ind], 3).ToString());

                    double thisNet = thisTurb.GetNetAEP(thisWakeModel, WD_Ind);

                    if (thisNet != 0)
                    {
                        objListItem.SubItems.Add(Math.Round(thisTurb.GetNetAEP(thisWakeModel, WD_Ind), 0).ToString());

                        if (WD_Ind == numWD)
                            objListItem.SubItems.Add(Math.Round(thisTurb.GetNetCF(thisWakeModel, WD_Ind), 4).ToString("P"));
                        else
                            objListItem.SubItems.Add(Math.Round(thisTurb.GetNetCF(thisWakeModel, WD_Ind) / avgEst.freeStream.windRose[WD_Ind], 4).ToString("P"));

                    }
                    else
                    {
                        objListItem.SubItems.Add("");
                        objListItem.SubItems.Add("");
                    }

                    double thisWakeLoss = thisTurb.GetWakeLoss(thisWakeModel, WD_Ind);
                    objListItem.SubItems.Add(thisWakeLoss.ToString("P"));
                    avgWakeLoss = avgWakeLoss + thisWakeLoss;

                    if (WD_Ind == numWD)
                    {
                        objListItem.SubItems.Add(Math.Round(avgEst.waked.weibullParams.overall_k, 2).ToString());
                        objListItem.SubItems.Add(Math.Round(avgEst.waked.weibullParams.overall_A, 2).ToString());
                    }
                    else
                    {
                        objListItem.SubItems.Add(Math.Round(avgEst.waked.weibullParams.sector_k[WD_Ind], 2).ToString());
                        objListItem.SubItems.Add(Math.Round(avgEst.waked.weibullParams.sector_A[WD_Ind], 2).ToString());
                    }

                }
            }

            if (checkedTurbCount > 0)
                avgWakeLoss = avgWakeLoss / checkedTurbCount;

            thisInst.txtWakeLoss.Text = Math.Round(avgWakeLoss, 4).ToString("P");

            double otherLoss = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
            thisInst.txtOtherLosses.Text = Math.Round(1 - otherLoss, 4).ToString("P");

        }

        /// <summary> Updates all plots and tables on all tabs that have the mets to reflect new metList. </summary>        
        public void MetList(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            thisInst.lstMetTowers.Items.Clear(); // First clear table
            thisInst.chkMetLabels.Items.Clear(); // Clear met label table
            thisInst.chkMetlabelsStep.Items.Clear(); // Clear met label table for stepwise tab
            thisInst.chkMetLabels_Maps.Items.Clear();
            thisInst.chkMetSumm.Items.Clear(); // Clear met list in turb est tab
            thisInst.chkMetGross.Items.Clear(); // Clear met list in Met and Turb summary page    
            thisInst.cboMetQC_SelectedMet.Items.Clear(); // Clear met dropdown list on Met Data QC page
            thisInst.cboMCP_Met.Items.Clear(); // Clear met dropdown list on MCP page
            thisInst.cboMERRASelectedMet.Items.Clear(); // Clear met dropdown list on MERRA (LT Ref) page
            thisInst.cboTurbMet.Items.Clear();
            thisInst.cboExtremeShearMet.Items.Clear();
            thisInst.cboExtremeWSMet.Items.Clear();

            int numMets = thisInst.metList.ThisCount;

            if (numMets > 0)
            {
                Met thisMet = thisInst.metList.metItem[0];
                thisInst.lstMetTowers.Columns[3].Text = thisInst.modeledHeight + " m Wind Speed";

                for (int j = 0; j < numMets; j++) // Now repopulate it with met towers
                {
                    thisMet = thisInst.metList.metItem[j];
                    Met.WSWD_Dist thisDist = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                    ListViewItem objListItem = thisInst.lstMetTowers.Items.Add(thisMet.name);
                    objListItem.SubItems.Add(Math.Round(thisMet.UTMX, 0).ToString());
                    objListItem.SubItems.Add(Math.Round(thisMet.UTMY, 0).ToString());
                    if (thisDist.WS != 0)
                        objListItem.SubItems.Add(Math.Round(thisDist.WS, 3).ToString());

                    thisInst.chkMetLabels.Items.Add(thisMet.name, true);
                    thisInst.chkMetlabelsStep.Items.Add(thisMet.name, true);
                    thisInst.chkMetLabels_Maps.Items.Add(thisMet.name, true);
                    if (thisMet.ExposureCount == 4) thisInst.chkMetSumm.Items.Add(thisMet.name, true);
                    if (thisMet.ExposureCount == 4) thisInst.chkMetGross.Items.Add(thisMet.name, true);
                    thisInst.cboMetQC_SelectedMet.Items.Add(thisMet.name);
                    thisInst.cboMCP_Met.Items.Add(thisMet.name);
                    thisInst.cboMERRASelectedMet.Items.Add(thisMet.name);
                    thisInst.cboTurbMet.Items.Add(thisMet.name);
                    thisInst.cboExtremeShearMet.Items.Add(thisMet.name);
                    thisInst.cboExtremeWSMet.Items.Add(thisMet.name);
                }

                thisInst.cboMetQC_SelectedMet.SelectedIndex = 0;
                thisInst.cboMCP_Met.SelectedIndex = 0;
                thisInst.cboMERRASelectedMet.SelectedIndex = 0;
                thisInst.cboTurbMet.SelectedIndex = 0;
                thisInst.cboExtremeShearMet.SelectedIndex = 0;
                thisInst.cboExtremeWSMet.SelectedIndex = 0;
                SiteConditionsMetDates(thisInst);

                if (thisMet.mcp == null && thisInst.cboMCP_Type.SelectedItem == null)
                    thisInst.cboMCP_Type.SelectedIndex = 0;
                else if (thisMet.mcp != null)
                {
                    if (thisMet.mcp.MCP_Ortho.allR_Sq != 0)
                        thisInst.cboMCP_Type.SelectedIndex = 0; // Orthogonal                    
                    else if (thisMet.mcp.MCP_Bins.binAvgSD_Cnt != null)
                        thisInst.cboMCP_Type.SelectedIndex = 1; // Method of Bins
                    else if (thisMet.mcp.MCP_Varrat.allR_Sq != 0)
                        thisInst.cboMCP_Type.SelectedIndex = 2; // Variance
                    else if (thisMet.mcp.MCP_Matrix.WS_CDFs != null)
                        thisInst.cboMCP_Type.SelectedIndex = 3; // Matrix
                    else
                        thisInst.cboMCP_Type.SelectedIndex = 0;

                }
            }
            else // Clear all met text
            {
                thisInst.cboMetQC_SelectedMet.Text = "";
                thisInst.cboMCP_Met.Text = "";
                thisInst.cboMERRASelectedMet.Text = "";
                thisInst.cboTurbMet.Text = "";
                thisInst.cboExtremeShearMet.Text = "";
                thisInst.cboExtremeWSMet.Text = "";
                thisInst.cboTurbMet.Text = "";
                thisInst.cboStartMet.Text = "";
                thisInst.cboEndMet.Text = "";

            }

            // Add 'user defined' on MERRA page so user can enter lat/long for MERRA2 data extraction
            thisInst.cboMERRASelectedMet.Items.Add("User-Defined Lat/Long");

            // Add all user-defined MERRA data extracted
            for (int i = 0; i < thisInst.merraList.numMERRA_Data; i++)
                if (thisInst.merraList.merraData[i].isUserDefined == true)
                    if (thisInst.merraList.merraData[i].numMERRA_Nodes > 0)
                        thisInst.cboMERRASelectedMet.Items.Add("Lat: " + Math.Round(thisInst.merraList.merraData[i].MERRA_Nodes[0].XY_ind.Lat, 3)
                            + " Long: " + Math.Round(thisInst.merraList.merraData[i].MERRA_Nodes[0].XY_ind.Lon, 3));

            thisInst.cboMERRASelectedMet.SelectedIndex = 0;

            thisInst.cboRR_MinSize.Items.Clear();
            thisInst.cboRR_MinSize.Text = "";
            for (int i = numMets - 1; i >= 1; i--)
                thisInst.cboRR_MinSize.Items.Add(i + " mets");

            if (thisInst.cboRR_MinSize.Items.Count > 0)
                thisInst.cboRR_MinSize.SelectedIndex = 0;

            StartMet_Dropdown(thisInst);
            EndMet_Dropdown(thisInst);

            thisInst.okToUpdate = true;
        }

        /// <summary> Updates table on Advanced tab that shows the met cross prediction of site-calibrated model with specified radius and WD sector. </summary>       
        public void ModCrossPredictions(Continuum thisInst)
        {
            int numPairs = thisInst.metPairList.PairCount;
            int radiusInd = thisInst.GetRadiusInd("Advanced");
            int WD_Ind = thisInst.GetWD_ind("Advanced");

            if (thisInst.metList.ThisCount == 0 || thisInst.metList.expoIsCalc == false)
            {
                thisInst.lstModCrossPred.Items.Clear();
                return;
            }

            int numWD = thisInst.GetNumWD();

            if (numPairs == 0 || WD_Ind == -1 || radiusInd == -1)
            {
                thisInst.lstModCrossPred.Items.Clear();
                return;
            }
            else
                thisInst.lstModCrossPred.Items.Clear();

            Met.TOD thisTOD = thisInst.GetSelectedTOD("Advanced");
            Met.Season thisSeason = thisInst.GetSelectedSeason("Advanced");
            string[] metsUsed = thisInst.metList.GetMetsUsed();

            if (thisInst.modelList.ModelCount > 1)
                if (thisInst.modelList.models[1, 0].isImported == true)
                    return;

            if (thisInst.modelList.ModelCount > 0)
            {
                if (numPairs > 0)
                {
                    Model[] models = thisInst.modelList.GetModels(thisInst, metsUsed, thisTOD, thisSeason, thisInst.modeledHeight, false);

                    for (int i = 0; i < numPairs; i++)
                    {
                        Pair_Of_Mets thisPair = thisInst.metPairList.metPairs[i];
                        Pair_Of_Mets.WS_CrossPreds thisWS_CrossPred = thisPair.GetWS_CrossPred(models[radiusInd]);

                        ListViewItem objListItem = thisInst.lstModCrossPred.Items.Add(thisPair.met1.name);
                        objListItem.SubItems.Add(thisPair.met2.name);
                        Met.WSWD_Dist dist2 = thisPair.met2.GetWS_WD_Dist(thisInst.modeledHeight, thisTOD, thisSeason);

                        if (WD_Ind == numWD)
                        {
                            objListItem.SubItems.Add(Math.Round(thisWS_CrossPred.WS_Ests[0], 2).ToString());
                            objListItem.SubItems.Add((Math.Round(thisWS_CrossPred.percErr[0], 4)).ToString("P"));
                        }
                        else
                        {
                            objListItem.SubItems.Add(Math.Round(thisWS_CrossPred.sectorWS_Ests[0, WD_Ind], 2).ToString());
                            double thisErr = (thisWS_CrossPred.sectorWS_Ests[0, WD_Ind] - dist2.WS * dist2.sectorWS_Ratio[WD_Ind]) / (dist2.WS * dist2.sectorWS_Ratio[WD_Ind]);
                            objListItem.SubItems.Add((Math.Round(thisErr, 4)).ToString("P"));
                        }

                        objListItem = thisInst.lstModCrossPred.Items.Add(thisPair.met2.name);
                        objListItem.SubItems.Add(thisPair.met1.name);
                        Met.WSWD_Dist dist1 = thisPair.met1.GetWS_WD_Dist(thisInst.modeledHeight, thisTOD, thisSeason);

                        if (WD_Ind == numWD)
                        {
                            objListItem.SubItems.Add(Math.Round(thisWS_CrossPred.WS_Ests[1], 2).ToString());
                            objListItem.SubItems.Add((Math.Round(thisWS_CrossPred.percErr[1], 4)).ToString("P"));
                        }
                        else
                        {
                            objListItem.SubItems.Add(Math.Round(thisWS_CrossPred.sectorWS_Ests[1, WD_Ind], 2).ToString());
                            double thisErr = (thisWS_CrossPred.sectorWS_Ests[1, WD_Ind] - dist1.WS * dist1.sectorWS_Ratio[WD_Ind]) / (dist1.WS * dist1.sectorWS_Ratio[WD_Ind]);
                            objListItem.SubItems.Add((Math.Round(thisErr, 4)).ToString("P"));
                        }
                    }

                }
            }
        }

        /// <summary> Specifies the default parameters shown on Path Node plot on Advanced tab. </summary>   
        public void SetDefaultCheckAdvanced(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            thisInst.chkAdvToShow.SetItemChecked(0, false); // UTMX
            thisInst.chkAdvToShow.SetItemChecked(1, false); // UTMY
            thisInst.chkAdvToShow.SetItemChecked(2, false); // Elevation
            thisInst.chkAdvToShow.SetItemChecked(3, false); // P10 UW
            thisInst.chkAdvToShow.SetItemChecked(4, false); // P10 DW
            thisInst.chkAdvToShow.SetItemChecked(5, true); // UW Expo
            thisInst.chkAdvToShow.SetItemChecked(6, true); // DW Expo
            thisInst.chkAdvToShow.SetItemChecked(7, true); // UW roughness
            thisInst.chkAdvToShow.SetItemChecked(8, true); // DW roughness
            thisInst.chkAdvToShow.SetItemChecked(9, false); // UW Disp H
            thisInst.chkAdvToShow.SetItemChecked(10, false); // DW Disp H
            thisInst.chkAdvToShow.SetItemChecked(11, false); // Delta WS UW Expo
            thisInst.chkAdvToShow.SetItemChecked(12, false); // Delta WS DW Expo
            thisInst.chkAdvToShow.SetItemChecked(13, false); // Delta WS UW SRDH
            thisInst.chkAdvToShow.SetItemChecked(14, false); // Delta WS DW SRDH
            thisInst.chkAdvToShow.SetItemChecked(15, true); // WS Est
            thisInst.chkAdvToShow.SetItemChecked(16, true); // WS Equiv
            thisInst.chkAdvToShow.SetItemChecked(17, true); // Act WS
            thisInst.okToUpdate = true;
        }

        /// <summary> Updates path of nodes tables on Advanced tab. </summary>   
        public void PathNodesList(Continuum thisInst)
        {
            if (thisInst.modelList.ModelCount == 0)
            {
                thisInst.lstPathNodes.Items.Clear();
                thisInst.lstPathNodes_UW.Items.Clear();
                thisInst.lstPathNodes_DW.Items.Clear();
                return;
            }

            string startMetStr = thisInst.GetStartMetAdvanced();
            string endMetStr = thisInst.GetEndSiteAdvanced();
            int numPairs = thisInst.metPairList.PairCount;

            thisInst.lstPathNodes.Items.Clear();
            thisInst.lstPathNodes_UW.Items.Clear();
            thisInst.lstPathNodes_DW.Items.Clear();

            int radiusIndex = thisInst.GetRadiusInd("Advanced");
            Met.TOD thisTOD = thisInst.GetSelectedTOD("Advanced");
            Met.Season thisSeason = thisInst.GetSelectedSeason("Advanced");

            if (radiusIndex == -1) return;

            int numMets = thisInst.metList.ThisCount;

            if (thisInst.modelList.ModelCount == 0 || radiusIndex == -1 || startMetStr == "" || endMetStr == "" || thisInst.metList.expoIsCalc == false)
            {
                thisInst.lstPathNodes.Items.Clear();
                return;
            }

            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisTOD, thisSeason, thisInst.modeledHeight, false);
            Model thisModel = models[radiusIndex];

            int WD_Ind = thisInst.GetWD_ind("Advanced");
            if (thisInst.metList.ThisCount == 0) return;
            int numWD = thisInst.GetNumWD();

            if (WD_Ind == -1 || thisModel == null)
            {
                thisInst.lstPathNodes.Items.Clear();
                return;
            }

            Nodes startNode = new Nodes();
            Nodes endNode = new Nodes();
            double[] startWS = null;
            double startAvgWS;
            Nodes[] pathOfNodes = null;
            double[,] nodesSectorWS = null;
            double[] nodeWS = null;
            double[] estSectorWS = null;
            double estWS = 0;
            double actWS = 0;

            // Need to figure out if end is a met or a turbine
            bool endIsMet = false;
            //  Met thisMet = new Met();

            for (int i = 0; i < numMets; i++)
            {
                Met thisMet = thisInst.metList.metItem[i];
                if (thisMet.name == endMetStr)
                {
                    endIsMet = true;
                    break;
                }
            }

            // Read what parameters to show
            Advanced_to_show paramsToShow = new Advanced_to_show();

            if (thisInst.chkAdvToShow.CheckedItems.Count == 0)
                return;

            for (int i = 0; i < thisInst.chkAdvToShow.CheckedItems.Count; i++)
            {
                if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "UTMX")
                    paramsToShow.showUTMX = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "UTMY")
                    paramsToShow.showUTMY = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "Elevation")
                    paramsToShow.showElevation = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "P10 UW")
                    paramsToShow.showP10UW = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "P10 DW")
                    paramsToShow.showP10DW = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "UW Expo")
                    paramsToShow.showUWExpo = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "DW Expo")
                    paramsToShow.showDWExpo = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "UW Roughness")
                    paramsToShow.showUWRough = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "DW Roughness")
                    paramsToShow.showDWRough = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "UW Disp H")
                    paramsToShow.showUWDispH = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "DW Disp H")
                    paramsToShow.showDWDispH = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "dWS UW Expo")
                    paramsToShow.show_dWS_UWExpo = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "dWS DW Expo")
                    paramsToShow.show_dWS_DWExpo = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "dWS UW SRDH")
                    paramsToShow.show_dWS_UW_SRDH = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "dWS DW SRDH")
                    paramsToShow.show_dWS_DW_SRDH = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "WS Est.")
                    paramsToShow.showWS_Est = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "Equiv WS")
                    paramsToShow.showEquivWS = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "Actual WS")
                    paramsToShow.showActualWS = true;

            }

            int colNum = 0;
            ListView objList = thisInst.lstPathNodes;
            objList.Columns.Clear();
            objList.Columns.Add("Site");
            objList.Columns[colNum].Width = 60;
            colNum++;

            if (paramsToShow.showUTMX == true)
            {
                objList.Columns.Add("UTMX");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showUTMY == true)
            {
                objList.Columns.Add("UTMY");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showElevation == true)
            {
                objList.Columns.Add("Elev");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showP10UW == true)
            {
                objList.Columns.Add("P10 UW");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showP10DW == true)
            {
                objList.Columns.Add("P10 DW");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showUWExpo == true)
            {
                objList.Columns.Add("UW Expo");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showDWExpo == true)
            {
                objList.Columns.Add("DW Expo");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showUWRough == true)
            {
                objList.Columns.Add("UW Roughness");
                objList.Columns[colNum].Width = 70;
                colNum++;
            }

            if (paramsToShow.showDWRough == true)
            {
                objList.Columns.Add("DW Roughness");
                objList.Columns[colNum].Width = 70;
                colNum++;
            }

            if (paramsToShow.showUWDispH == true)
            {
                objList.Columns.Add("UW Disp H");
                objList.Columns[colNum].Width = 75;
                colNum++;
            }

            if (paramsToShow.showDWDispH == true)
            {
                objList.Columns.Add("DW Disp H");
                objList.Columns[colNum].Width = 75;
                colNum++;
            }

            if (paramsToShow.show_dWS_UWExpo == true)
            {
                objList.Columns.Add("dWS UW Expo");
                objList.Columns[colNum].Width = 85;
                colNum++;
            }

            if (paramsToShow.show_dWS_DWExpo == true)
            {
                objList.Columns.Add("dWS DW Expo");
                objList.Columns[colNum].Width = 85;
                colNum++;
            }

            if (paramsToShow.show_dWS_UW_SRDH == true)
            {
                objList.Columns.Add("dWS UW SRDH");
                objList.Columns[colNum].Width = 95;
                colNum++;
            }

            if (paramsToShow.show_dWS_DW_SRDH == true)
            {
                objList.Columns.Add("dWS DW SRDH");
                objList.Columns[colNum].Width = 95;
                colNum++;
            }

            if (paramsToShow.showWS_Est == true)
            {
                objList.Columns.Add("WS Est.");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showEquivWS == true)
            {
                objList.Columns.Add("Equiv WS");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showActualWS == true)
            {
                objList.Columns.Add("Actual WS");
                objList.Columns[colNum].Width = 70;
            }

            NodeCollection nodeList = new NodeCollection();
            thisInst.lblTurbineTSNoAdvanced.Text = "";

            for (int i = 0; i < numMets; i++)
            {
                Met thisMet = thisInst.metList.metItem[i];
                Met.WSWD_Dist thisDist = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, thisTOD, thisSeason);

                if (thisMet.name == startMetStr)
                {
                    startNode = nodeList.GetMetNode(thisMet);
                    startWS = new double[numWD];

                    for (int j = 0; j < numWD; j++)
                        startWS[j] = thisDist.WS * thisDist.sectorWS_Ratio[j];

                    startAvgWS = thisDist.WS;
                }
                else if (thisMet.name == endMetStr)
                {
                    endNode = nodeList.GetMetNode(thisMet);

                    if (WD_Ind == numWD)
                        actWS = thisDist.WS;
                    else
                        actWS = thisDist.WS * thisDist.sectorWS_Ratio[WD_Ind];
                }
            }

            if (endIsMet)
            {
                for (int i = 0; i < numPairs; i++)
                {
                    Pair_Of_Mets thisPair = thisInst.metPairList.metPairs[i];
                    if (startMetStr == thisPair.met1.name && endMetStr == thisPair.met2.name)
                    {
                        Pair_Of_Mets.WS_CrossPreds thisCrossPred = thisPair.GetWS_CrossPred(models[radiusIndex]);

                        pathOfNodes = thisCrossPred.nodePath;
                        nodesSectorWS = thisCrossPred.nodeSectorWS_Ests1to2;
                        nodeWS = thisCrossPred.nodeWS_Ests1to2;

                        estSectorWS = new double[numWD];

                        for (int j = 0; j <= numWD - 1; j++)
                            estSectorWS[j] = thisCrossPred.sectorWS_Ests[0, j];

                        estWS = thisCrossPred.WS_Ests[0];
                        break;
                    }
                    else if (startMetStr == thisPair.met2.name && endMetStr == thisPair.met1.name)
                    {
                        Pair_Of_Mets.WS_CrossPreds thisCrossPred = thisPair.GetWS_CrossPred(models[radiusIndex]);
                        pathOfNodes = thisCrossPred.nodePath;

                        if (pathOfNodes != null)
                        {
                            int numNodes = pathOfNodes.Length;
                            Nodes[] tempNodes = new Nodes[numNodes];
                            int Ind = 0;

                            for (int j = numNodes - 1; j >= 0; j--)
                            {
                                tempNodes[Ind] = pathOfNodes[j];
                                Ind++;
                            }
                            pathOfNodes = tempNodes;
                        }

                        nodesSectorWS = thisCrossPred.nodeSectorWS_Ests2to1;
                        nodeWS = thisCrossPred.nodeWS_Ests2to1;
                        estSectorWS = new double[numWD];

                        for (int j = 0; j <= numWD - 1; j++)
                            estSectorWS[j] = thisCrossPred.sectorWS_Ests[1, j];

                        estWS = thisCrossPred.WS_Ests[1];
                    }
                }
            }
            else
            {
                // End is a turbine             

                Turbine thisTurb = thisInst.turbineList.GetTurbine(endMetStr);
                if (thisTurb.name == endMetStr)
                {
                    endNode = nodeList.GetTurbNode(thisTurb);

                    for (int j = 0; j < thisTurb.WSEst_Count; j++)
                    {
                        if (thisTurb.WS_Estimate[j].predictorMetName == startMetStr && thisInst.modelList.IsSameModel(thisModel, thisTurb.WS_Estimate[j].model))
                        {
                            nodesSectorWS = thisTurb.WS_Estimate[j].sectorWS_at_nodes;
                            nodeWS = thisTurb.WS_Estimate[j].WS_at_nodes;
                            pathOfNodes = thisTurb.WS_Estimate[j].pathOfNodes;
                            estSectorWS = thisTurb.WS_Estimate[j].sectorWS;
                            estWS = thisTurb.WS_Estimate[j].WS;
                            break;
                        }
                    }

                    if (thisInst.turbineList.genTimeSeries == true)
                        thisInst.lblTurbineTSNoAdvanced.Text = "Turbine estimates generated using time series. No advanced analysis currently available.";

                }

            }

            if (startNode.UTMX == 0 || estSectorWS == null)
                return;

            PathNodeListUpdate(startNode, startMetStr, startWS, endNode, endMetStr, pathOfNodes, nodesSectorWS, nodeWS, WD_Ind,
                                  radiusIndex, numWD, thisModel, thisInst, paramsToShow, thisInst.modeledHeight, estSectorWS, estWS, actWS);

        }

        /// <summary> Updates the three tables on Advanced tab to show selected parameters from selected met to selected met or turbine site for specified radius and wind direction sector. </summary>
        public void PathNodeListUpdate(Nodes startNode, string startMetStr, double[] startWS, Nodes endNode, string endStr, Nodes[] pathOfNodes,
                                         double[,] pathNodeSectWS, double[] pathNodeWS, int WD_Ind, int radiusIndex, int numWD, Model thisModel,
                                         Continuum thisInst, Advanced_to_show paramsToShow, double HH, double[] estSectorWS, double estWS, double actWS)
        {

            ListViewItem objList_UW = null;
            ListViewItem objList_DW = null;

            double P10_DW_1 = 0;
            double P10_UW_1 = 0;
            double UWExpo_1 = 0;
            double DWExpo_1 = 0;

            double UW_SR_1 = 0;
            double UW_DH_1 = 0;
            double DW_SR_1 = 0;
            double DW_DH_1 = 0;

            double P10_DW_2 = 0;
            double P10_UW_2 = 0;
            double UWExpo_2 = 0;
            double DWExpo_2 = 0;

            double UW_SR_2 = 0;
            double UW_DH_2 = 0;
            double DW_SR_2 = 0;
            double DW_DH_2 = 0;

            double[] P10_DW_All = new double[numWD];
            double[] P10_UW_All = new double[numWD];
            double[] UWExpo_1_All = new double[numWD];
            double[] DWExpo_1_All = new double[numWD];
            double[] UWExpo_2_All = new double[numWD];
            double[] DWExpo_2_All = new double[numWD];

            double[] UW_SR_All_1 = new double[numWD];
            double[] UW_SR_All_2 = new double[numWD];
            double[] DW_SR_All_1 = new double[numWD];
            double[] DW_SR_All_2 = new double[numWD];

            double[] UW_DH_All_1 = new double[numWD];
            double[] UW_DH_All_2 = new double[numWD];
            double[] DW_DH_All_1 = new double[numWD];
            double[] DW_DH_All_2 = new double[numWD];

            double[] avgUW = new double[numWD];
            double[] avgDW = new double[numWD];

            double[] UW_Stab_Corr_1 = new double[numWD];
            double[] UW_Stab_Corr_2 = new double[numWD];
            double[] DW_Stab_Corr_1 = new double[numWD];
            double[] DW_Stab_Corr_2 = new double[numWD];

            double[] sectWS_1 = new double[numWD];
            double[] Sect_WS_2 = new double[numWD];
            double[] WS_Equiv = new double[numWD];
            double[] WR_Pred = new double[numWD];

            int numNodes = 0;

            double WS_Equiv_1 = 0;
            double WS_1 = 0;

            double deltaWS_UWExpo = 0;
            double deltaWS_DWExpo = 0;
            double deltaWS_UW_SRDH = 0;
            double deltaWS_DW_SRDH = 0;

            NodeCollection.Sep_Nodes[] flowSepNodes = new NodeCollection.Sep_Nodes[1];
            Nodes node1 = new Nodes();
            Nodes node2 = new Nodes();

            NodeCollection.Node_UTMs node1Coords = new NodeCollection.Node_UTMs();
            node1Coords.UTMX = startNode.UTMX;
            node1Coords.UTMY = startNode.UTMY;
            NodeCollection.Node_UTMs node2Coords = new NodeCollection.Node_UTMs();

            Met.TOD thisTOD = thisInst.GetSelectedTOD("Advanced");
            Met.Season thisSeason = thisInst.GetSelectedSeason("Advanced");
            double[] startNodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), startNode.UTMX, startNode.UTMY, thisTOD, thisSeason, thisInst.modeledHeight);
            double[] endNodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), endNode.UTMX, endNode.UTMY, thisTOD, thisSeason, thisInst.modeledHeight);

            if (pathOfNodes != null)
                numNodes = pathOfNodes.Length;
            else
                numNodes = 0;

            bool gotSR = thisInst.topo.gotSR;
            bool useFlowSep = thisInst.topo.useSepMod;

            objListItem = thisInst.lstPathNodes.Items.Add(startMetStr);

            if (WD_Ind != numWD)
            {
                objList_UW = thisInst.lstPathNodes_UW.Items.Add(startMetStr);
                objList_DW = thisInst.lstPathNodes_DW.Items.Add(startMetStr);
            }

            if (WD_Ind == numWD)
            {
                P10_DW_1 = startNode.gridStats.GetOverallP10(startNodeWindRose, radiusIndex, "DW");
                P10_UW_1 = startNode.gridStats.GetOverallP10(startNodeWindRose, radiusIndex, "UW");

                UWExpo_1 = startNode.expo[radiusIndex].GetOverallValue(startNodeWindRose, "Expo", "UW");
                DWExpo_1 = startNode.expo[radiusIndex].GetOverallValue(startNodeWindRose, "Expo", "DW");
            }
            else
            {
                P10_DW_1 = startNode.gridStats.stats[radiusIndex].P10_DW[WD_Ind];
                P10_UW_1 = startNode.gridStats.stats[radiusIndex].P10_UW[WD_Ind];

                UWExpo_1 = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD_Ind, "UW", "Expo");
                DWExpo_1 = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD_Ind, "DW", "Expo");
            }

            AddUTMsP10sExposToPathNodeList(paramsToShow, startNode.UTMX, startNode.UTMY, startNode.elev, P10_UW_1, P10_DW_1, UWExpo_1, DWExpo_1);

            if (gotSR == true)
            {
                if (WD_Ind == numWD)
                {
                    UW_SR_1 = startNode.expo[radiusIndex].GetOverallValue(startNodeWindRose, "SR", "UW");
                    UW_DH_1 = startNode.expo[radiusIndex].GetOverallValue(startNodeWindRose, "DH", "UW");
                    DW_SR_1 = startNode.expo[radiusIndex].GetOverallValue(startNodeWindRose, "SR", "DW");
                    DW_DH_1 = startNode.expo[radiusIndex].GetOverallValue(startNodeWindRose, "DH", "DW");
                }
                else
                {
                    UW_SR_1 = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD_Ind, "UW", "SR");
                    UW_DH_1 = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD_Ind, "UW", "DH");
                    DW_SR_1 = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD_Ind, "DW", "SR");
                    DW_DH_1 = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD_Ind, "DW", "DH");
                }
            }

            for (int WD = 0; WD < numWD; WD++)
            {
                sectWS_1[WD] = startWS[WD];
                P10_DW_All[WD] = startNode.gridStats.stats[radiusIndex].P10_DW[WD];
                P10_UW_All[WD] = startNode.gridStats.stats[radiusIndex].P10_UW[WD];
                UWExpo_1_All[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD, "UW", "Expo");
                DWExpo_1_All[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD, "DW", "Expo");

                if (gotSR == true)
                {
                    UW_SR_All_1[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD, "UW", "SR");
                    UW_DH_All_1[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD, "UW", "DH");
                    DW_SR_All_1[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD, "DW", "SR");
                    DW_DH_All_1[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNodeWindRose, WD, "DW", "DH");
                }
            }

            WR_Pred = startNodeWindRose;

            if (numNodes == 0)
            {
                WS_Equiv = thisInst.modelList.GetWS_Equiv(startNodeWindRose, endNodeWindRose, sectWS_1);
                node2Coords.UTMX = endNode.UTMX;
                node2Coords.UTMY = endNode.UTMY;
            }
            else
            {
                double[] pathOfNodesWindRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), pathOfNodes[0].UTMX, pathOfNodes[0].UTMY, thisTOD, thisSeason, thisInst.modeledHeight);
                WS_Equiv = thisInst.modelList.GetWS_Equiv(startNodeWindRose, pathOfNodesWindRose, sectWS_1);
                node2Coords.UTMX = pathOfNodes[0].UTMX;
                node2Coords.UTMY = pathOfNodes[0].UTMY;
            }

            if (WD_Ind == numWD)
            {
                WS_1 = startNode.CalcAvgWS(sectWS_1, startNodeWindRose);
                WS_Equiv_1 = startNode.CalcAvgWS(WS_Equiv, startNodeWindRose);
            }
            else
            {
                WS_1 = sectWS_1[WD_Ind];
                WS_Equiv_1 = WS_Equiv[WD_Ind];
            }

            node1.flowSepNodes = startNode.flowSepNodes;
            node1 = startNode;
            double[] node1WindRose = startNodeWindRose;

            ModelCollection.Coeff_Delta_WS[] UW_CoeffsDeltas;
            int numUW_CoeffsDeltas = 0;
            ModelCollection.Coeff_Delta_WS[] DW_CoeffsDeltas;
            int numDW_CoeffsDeltas = 0;

            if (gotSR == true)
            {
                if (paramsToShow.showUWRough) objListItem.SubItems.Add(Math.Round(UW_SR_1, 2).ToString());
                if (paramsToShow.showDWRough) objListItem.SubItems.Add(Math.Round(DW_SR_1, 2).ToString());
                if (paramsToShow.showUWDispH) objListItem.SubItems.Add(Math.Round(UW_DH_1, 2).ToString());
                if (paramsToShow.showDWDispH) objListItem.SubItems.Add(Math.Round(DW_DH_1, 2).ToString());
            }
            else
            {
                if (paramsToShow.showUWRough) objListItem.SubItems.Add("");
                if (paramsToShow.showDWRough) objListItem.SubItems.Add("");
                if (paramsToShow.showUWDispH) objListItem.SubItems.Add("");
                if (paramsToShow.showDWDispH) objListItem.SubItems.Add("");
            }

            if (paramsToShow.show_dWS_UWExpo) objListItem.SubItems.Add(""); // Delta WS UW expo col
            if (paramsToShow.show_dWS_DWExpo) objListItem.SubItems.Add(""); // Delta WS DW expo col
            if (paramsToShow.show_dWS_UW_SRDH) objListItem.SubItems.Add(""); // Delta WS UW SRDH col
            if (paramsToShow.show_dWS_DW_SRDH) objListItem.SubItems.Add(""); // Delta WS DW SRDH col
            if (paramsToShow.showWS_Est) objListItem.SubItems.Add(Math.Round(WS_1, 2).ToString());

            for (int i = 0; i < numNodes; i++)
            {
                node2 = pathOfNodes[i];
                node2Coords.UTMX = pathOfNodes[i].UTMX;
                node2Coords.UTMY = pathOfNodes[i].UTMY;
                double[] node2WindRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), node2.UTMX, node2.UTMY, thisTOD, thisSeason, thisInst.modeledHeight);

                // Calculate equivalent wind speed at predictor based on target wind rose
                WS_Equiv = thisInst.modelList.GetWS_Equiv(WR_Pred, node2WindRose, sectWS_1);

                if (WD_Ind == numWD)
                {
                    WS_1 = node1.CalcAvgWS(sectWS_1, node1WindRose);
                    WS_Equiv_1 = startNode.CalcAvgWS(WS_Equiv, node1WindRose);
                }
                else
                {
                    WS_1 = sectWS_1[WD_Ind];
                    WS_Equiv_1 = WS_Equiv[WD_Ind];
                }

                if (paramsToShow.showEquivWS) objListItem.SubItems.Add(Math.Round(WS_Equiv_1, 2).ToString());
                if (WD_Ind != numWD) WS_1 = WS_Equiv[WD_Ind];

                for (int WD = 0; WD < numWD; WD++)
                {
                    P10_DW_All[WD] = (P10_DW_All[WD] + node2.gridStats.stats[radiusIndex].P10_DW[WD]) / 2;
                    P10_UW_All[WD] = (P10_UW_All[WD] + node2.gridStats.stats[radiusIndex].P10_UW[WD]) / 2;
                    UWExpo_2_All[WD] = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD, "UW", "Expo");
                    DWExpo_2_All[WD] = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD, "DW", "Expo");

                    avgUW[WD] = (UWExpo_1_All[WD] + UWExpo_2_All[WD]) / 2;
                    avgDW[WD] = (DWExpo_1_All[WD] + DWExpo_2_All[WD]) / 2;
                }

                if (WD_Ind == numWD)
                {
                    P10_UW_2 = node2.gridStats.GetOverallP10(node2WindRose, radiusIndex, "UW");
                    P10_DW_2 = node2.gridStats.GetOverallP10(node2WindRose, radiusIndex, "DW");
                    UWExpo_2 = node2.expo[radiusIndex].GetOverallValue(node2WindRose, "Expo", "UW");
                    DWExpo_2 = node2.expo[radiusIndex].GetOverallValue(node2WindRose, "Expo", "DW");
                }
                else
                {
                    P10_DW_2 = node2.gridStats.stats[radiusIndex].P10_DW[WD_Ind];
                    P10_UW_2 = node2.gridStats.stats[radiusIndex].P10_UW[WD_Ind];
                    UWExpo_2 = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD_Ind, "UW", "Expo");
                    DWExpo_2 = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD_Ind, "DW", "Expo");
                }

                objListItem = thisInst.lstPathNodes.Items.Add("Node " + (i + 1).ToString());
                AddUTMsP10sExposToPathNodeList(paramsToShow, node2.UTMX, node2.UTMY, node2.elev, P10_UW_2, P10_DW_2, UWExpo_2, DWExpo_2);

                if (gotSR == true)
                {
                    if (WD_Ind == numWD)
                    {
                        UW_SR_2 = node2.expo[radiusIndex].GetOverallValue(node2WindRose, "SR", "UW");
                        UW_DH_2 = node2.expo[radiusIndex].GetOverallValue(node2WindRose, "DH", "UW");
                        DW_SR_2 = node2.expo[radiusIndex].GetOverallValue(node2WindRose, "SR", "DW");
                        DW_DH_2 = node2.expo[radiusIndex].GetOverallValue(node2WindRose, "DH", "DW");
                    }
                    else
                    {
                        UW_SR_2 = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD_Ind, "UW", "SR");
                        UW_DH_2 = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD_Ind, "UW", "DH");
                        DW_SR_2 = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD_Ind, "DW", "SR");
                        DW_DH_2 = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD_Ind, "DW", "DH");
                    }

                    if (paramsToShow.showUWRough) objListItem.SubItems.Add(Math.Round(UW_SR_2, 2).ToString());
                    if (paramsToShow.showDWRough) objListItem.SubItems.Add(Math.Round(DW_SR_2, 2).ToString());
                    if (paramsToShow.showUWDispH) objListItem.SubItems.Add(Math.Round(UW_DH_2, 2).ToString());
                    if (paramsToShow.showDWDispH) objListItem.SubItems.Add(Math.Round(DW_DH_2, 2).ToString());

                    deltaWS_UW_SRDH = 0;
                    deltaWS_DW_SRDH = 0;
                }
                else
                {
                    if (paramsToShow.showUWRough) objListItem.SubItems.Add("");
                    if (paramsToShow.showDWRough) objListItem.SubItems.Add("");
                    if (paramsToShow.showUWDispH) objListItem.SubItems.Add("");
                    if (paramsToShow.showDWDispH) objListItem.SubItems.Add("");

                }

                deltaWS_UWExpo = 0;
                deltaWS_DWExpo = 0;

                if (WD_Ind == numWD)
                {
                    deltaWS_DWExpo = 0;
                    deltaWS_UWExpo = 0;

                    for (int WD = 0; WD < numWD; WD++)
                    {
                        UW_CoeffsDeltas = thisInst.modelList.Get_DeltaWS_UW_Expo(UWExpo_1_All[WD], UWExpo_2_All[WD], DWExpo_1_All[WD], DWExpo_2_All[WD], P10_UW_All[WD], P10_DW_All[WD], thisModel,
                                                                WD, radiusIndex, node1.flowSepNodes, node2.flowSepNodes, WS_Equiv[WD], useFlowSep, node1Coords, node2Coords);

                        if (UW_CoeffsDeltas != null)
                            numUW_CoeffsDeltas = UW_CoeffsDeltas.Length;

                        DW_CoeffsDeltas = thisInst.modelList.Get_DeltaWS_DW_Expo(WS_Equiv[WD], UWExpo_1_All[WD], UWExpo_2_All[WD], DWExpo_1_All[WD], DWExpo_2_All[WD], P10_UW_All[WD],
                            P10_DW_All[WD], thisModel, WD, useFlowSep);

                        if (DW_CoeffsDeltas != null)
                            numDW_CoeffsDeltas = DW_CoeffsDeltas.Length;

                        deltaWS_UWExpo = deltaWS_UWExpo + UW_CoeffsDeltas[numUW_CoeffsDeltas - 1].deltaWS_Expo * node2WindRose[WD];
                        deltaWS_DWExpo = deltaWS_DWExpo + DW_CoeffsDeltas[numDW_CoeffsDeltas - 1].deltaWS_Expo * node2WindRose[WD];
                    }
                }
                else
                {
                    UW_CoeffsDeltas = thisInst.modelList.Get_DeltaWS_UW_Expo(UWExpo_1, UWExpo_2, DWExpo_1, DWExpo_2, (P10_UW_1 + P10_UW_2) / 2, (P10_DW_1 + P10_DW_2) / 2, thisModel, WD_Ind,
                        radiusIndex, node1.flowSepNodes, node2.flowSepNodes, WS_1, useFlowSep, node1Coords, node2Coords);

                    if (UW_CoeffsDeltas != null)
                        numUW_CoeffsDeltas = UW_CoeffsDeltas.Length;

                    for (int Coeff_ind = 0; Coeff_ind < numUW_CoeffsDeltas; Coeff_ind++)
                    {
                        if (UW_CoeffsDeltas[Coeff_ind].flowType != "Total")
                        {
                            if (Coeff_ind > 0)
                                objList_UW = thisInst.lstPathNodes_UW.Items.Add((Coeff_ind + 1).ToString());
                            else
                                objList_UW = thisInst.lstPathNodes_UW.Items.Add("Node " + (i + 1));

                            objList_UW.SubItems.Add(UW_CoeffsDeltas[Coeff_ind].flowType);
                            objList_UW.SubItems.Add(Math.Round(UW_CoeffsDeltas[Coeff_ind].coeff, 4).ToString());
                            objList_UW.SubItems.Add(Math.Round(UW_CoeffsDeltas[Coeff_ind].deltaWS_Expo, 2).ToString());
                        }
                    }

                    DW_CoeffsDeltas = thisInst.modelList.Get_DeltaWS_DW_Expo(WS_1, UWExpo_1, UWExpo_2, DWExpo_1, DWExpo_2, (P10_UW_1 + P10_UW_2) / 2, (P10_DW_1 + P10_DW_2) / 2, thisModel, WD_Ind, useFlowSep);

                    if (DW_CoeffsDeltas != null)
                        numDW_CoeffsDeltas = DW_CoeffsDeltas.Length;

                    for (int Coeff_ind = 0; Coeff_ind < numDW_CoeffsDeltas; Coeff_ind++)
                    {
                        if (DW_CoeffsDeltas[Coeff_ind].flowType != "Total")
                        {
                            if (Coeff_ind > 0)
                                objList_DW = thisInst.lstPathNodes_DW.Items.Add((Coeff_ind + 1).ToString());
                            else
                                objList_DW = thisInst.lstPathNodes_DW.Items.Add("Node " + (i + 1));

                            objList_DW.SubItems.Add(DW_CoeffsDeltas[Coeff_ind].flowType);
                            objList_DW.SubItems.Add(Math.Round(DW_CoeffsDeltas[Coeff_ind].coeff, 4).ToString());
                            objList_DW.SubItems.Add(Math.Round(DW_CoeffsDeltas[Coeff_ind].deltaWS_Expo, 2).ToString());
                        }

                    }

                    deltaWS_UWExpo = UW_CoeffsDeltas[numUW_CoeffsDeltas - 1].deltaWS_Expo;
                    deltaWS_DWExpo = DW_CoeffsDeltas[numDW_CoeffsDeltas - 1].deltaWS_Expo;

                }

                if (paramsToShow.show_dWS_UWExpo) objListItem.SubItems.Add(Math.Round(deltaWS_UWExpo, 2).ToString());
                if (paramsToShow.show_dWS_DWExpo) objListItem.SubItems.Add(Math.Round(deltaWS_DWExpo, 2).ToString());

                if (gotSR == true)
                {
                    deltaWS_UW_SRDH = 0;
                    deltaWS_DW_SRDH = 0;

                    for (int WD = 0; WD < numWD; WD++)
                    {
                        UW_Stab_Corr_1[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, UW_SR_All_1[WD], useFlowSep, "UW");
                        UW_SR_All_2[WD] = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD, "UW", "SR");
                        UW_DH_All_2[WD] = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD, "UW", "DH");
                        UW_Stab_Corr_2[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, UW_SR_All_2[WD], useFlowSep, "UW");

                        DW_Stab_Corr_1[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, DW_SR_All_1[WD], useFlowSep, "DW");
                        DW_SR_All_2[WD] = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD, "DW", "SR");
                        DW_DH_All_2[WD] = node2.expo[radiusIndex].GetWgtAvg(node2WindRose, WD, "DW", "DH");
                        DW_Stab_Corr_2[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, DW_SR_All_2[WD], useFlowSep, "DW");
                    }

                    if (WD_Ind != numWD)
                    {

                        deltaWS_UW_SRDH = thisInst.modelList.GetDeltaWS_SRDH(WS_1, HH, UW_SR_All_1[WD_Ind], UW_SR_All_2[WD_Ind], UW_DH_All_1[WD_Ind], UW_DH_All_2[WD_Ind], UW_Stab_Corr_1[WD_Ind], UW_Stab_Corr_2[WD_Ind]);
                        deltaWS_DW_SRDH = thisInst.modelList.GetDeltaWS_SRDH(WS_1, HH, DW_SR_All_1[WD_Ind], DW_SR_All_2[WD_Ind], DW_DH_All_1[WD_Ind], DW_DH_All_2[WD_Ind], DW_Stab_Corr_1[WD_Ind], DW_Stab_Corr_2[WD_Ind]);
                        objList_UW.SubItems.Add(Math.Round(deltaWS_UW_SRDH, 2).ToString());
                        objList_DW.SubItems.Add(Math.Round(deltaWS_DW_SRDH, 2).ToString());

                    }

                    if (paramsToShow.show_dWS_UW_SRDH) objListItem.SubItems.Add(Math.Round(deltaWS_UW_SRDH, 2).ToString());
                    if (paramsToShow.show_dWS_DW_SRDH) objListItem.SubItems.Add(Math.Round(deltaWS_DW_SRDH, 2).ToString());
                }
                else
                {
                    if (paramsToShow.show_dWS_UW_SRDH) objListItem.SubItems.Add("");
                    if (paramsToShow.show_dWS_DW_SRDH) objListItem.SubItems.Add("");
                }

                if (WD_Ind == numWD)
                {
                    if (paramsToShow.showWS_Est) objListItem.SubItems.Add(Math.Round(pathNodeWS[i], 2).ToString());
                }
                else
                {
                    if (paramsToShow.showWS_Est) objListItem.SubItems.Add(Math.Round(pathNodeSectWS[i, WD_Ind], 2).ToString());
                }

                for (int WD = 0; WD < numWD; WD++)
                {
                    sectWS_1[WD] = pathNodeSectWS[i, WD];
                    P10_DW_All[WD] = node2.gridStats.stats[radiusIndex].P10_DW[WD];
                    P10_UW_All[WD] = node2.gridStats.stats[radiusIndex].P10_UW[WD];
                    UWExpo_1_All[WD] = UWExpo_2_All[WD];
                    DWExpo_1_All[WD] = DWExpo_2_All[WD];

                    if (gotSR == true)
                    {
                        UW_SR_All_1[WD] = UW_SR_All_2[WD];
                        UW_DH_All_1[WD] = UW_DH_All_2[WD];
                        UW_Stab_Corr_1[WD] = UW_Stab_Corr_2[WD];
                        DW_SR_All_1[WD] = DW_SR_All_2[WD];
                        DW_DH_All_1[WD] = DW_DH_All_2[WD];
                        DW_Stab_Corr_1[WD] = DW_Stab_Corr_2[WD];
                    }
                }

                node1 = node2;
                UWExpo_1 = UWExpo_2;
                DWExpo_1 = DWExpo_2;
                P10_UW_1 = P10_UW_2;
                P10_DW_1 = P10_DW_2;
                WR_Pred = node2WindRose;
                node1Coords = node2Coords;

            }

            WS_Equiv = thisInst.modelList.GetWS_Equiv(WR_Pred, endNodeWindRose, sectWS_1);
            node2Coords.UTMX = endNode.UTMX;
            node2Coords.UTMY = endNode.UTMY;
            node2 = endNode;

            if (WD_Ind == numWD)
            {
                WS_1 = node1.CalcAvgWS(sectWS_1, node1WindRose);
                WS_Equiv_1 = endNode.CalcAvgWS(WS_Equiv, node1WindRose);
            }
            else
            {
                WS_1 = sectWS_1[WD_Ind];
                WS_Equiv_1 = WS_Equiv[WD_Ind];
            }

            if (paramsToShow.showEquivWS) objListItem.SubItems.Add(Math.Round(WS_Equiv_1, 2).ToString());
            if (WD_Ind != numWD) WS_1 = WS_Equiv[WD_Ind];

            if (WD_Ind == numWD)
            {
                P10_UW_2 = endNode.gridStats.GetOverallP10(endNodeWindRose, radiusIndex, "UW");
                P10_DW_2 = endNode.gridStats.GetOverallP10(endNodeWindRose, radiusIndex, "DW");
                UWExpo_2 = endNode.expo[radiusIndex].GetOverallValue(endNodeWindRose, "Expo", "UW");
                DWExpo_2 = endNode.expo[radiusIndex].GetOverallValue(endNodeWindRose, "Expo", "DW");
            }
            else
            {
                P10_DW_2 = endNode.gridStats.stats[radiusIndex].P10_DW[WD_Ind];
                P10_UW_2 = endNode.gridStats.stats[radiusIndex].P10_UW[WD_Ind];
                UWExpo_2 = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "Expo");
                DWExpo_2 = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "Expo");
            }

            objListItem = thisInst.lstPathNodes.Items.Add(endStr);
            AddUTMsP10sExposToPathNodeList(paramsToShow, endNode.UTMX, endNode.UTMY, endNode.elev, P10_UW_2, P10_DW_2, UWExpo_2, DWExpo_2);

            for (int WD = 0; WD < numWD; WD++)
            {
                P10_DW_All[WD] = (P10_DW_All[WD] + endNode.gridStats.stats[radiusIndex].P10_DW[WD]) / 2;
                P10_UW_All[WD] = (P10_UW_All[WD] + endNode.gridStats.stats[radiusIndex].P10_UW[WD]) / 2;
                UWExpo_2_All[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD, "UW", "Expo");
                DWExpo_2_All[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD, "DW", "Expo");

                avgUW[WD] = (UWExpo_1_All[WD] + UWExpo_2_All[WD]) / 2;
                avgDW[WD] = (DWExpo_1_All[WD] + DWExpo_2_All[WD]) / 2;
            }

            if (gotSR == true)
            {
                if (WD_Ind == numWD)
                {
                    UW_SR_2 = endNode.expo[radiusIndex].GetOverallValue(endNodeWindRose, "SR", "UW");
                    UW_DH_2 = endNode.expo[radiusIndex].GetOverallValue(endNodeWindRose, "DH", "UW");
                    DW_SR_2 = endNode.expo[radiusIndex].GetOverallValue(endNodeWindRose, "SR", "DW");
                    DW_DH_2 = endNode.expo[radiusIndex].GetOverallValue(endNodeWindRose, "DH", "DW");
                }
                else
                {
                    UW_SR_2 = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "SR");
                    UW_DH_2 = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "DH");
                    DW_SR_2 = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "SR");
                    DW_DH_2 = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "DH");
                }

                if (paramsToShow.showUWRough) objListItem.SubItems.Add(Math.Round(UW_SR_2, 2).ToString());
                if (paramsToShow.showDWRough) objListItem.SubItems.Add(Math.Round(DW_SR_2, 2).ToString());
                if (paramsToShow.showUWDispH) objListItem.SubItems.Add(Math.Round(UW_DH_2, 2).ToString());
                if (paramsToShow.showDWDispH) objListItem.SubItems.Add(Math.Round(DW_DH_2, 2).ToString());

            }
            else
            {
                if (paramsToShow.showUWRough) objListItem.SubItems.Add("");
                if (paramsToShow.showDWRough) objListItem.SubItems.Add("");
                if (paramsToShow.showUWDispH) objListItem.SubItems.Add("");
                if (paramsToShow.showDWDispH) objListItem.SubItems.Add("");
            }

            if (WD_Ind == numWD)
            {
                deltaWS_DWExpo = 0;
                deltaWS_UWExpo = 0;

                for (int WD = 0; WD < numWD; WD++)
                {
                    UW_CoeffsDeltas = thisInst.modelList.Get_DeltaWS_UW_Expo(UWExpo_1_All[WD], UWExpo_2_All[WD], DWExpo_1_All[WD], DWExpo_2_All[WD], P10_UW_All[WD], P10_DW_All[WD], thisModel,
                                                            WD, radiusIndex, node1.flowSepNodes, endNode.flowSepNodes, WS_Equiv[WD], useFlowSep, node1Coords, node2Coords);

                    if (UW_CoeffsDeltas != null)
                        numUW_CoeffsDeltas = UW_CoeffsDeltas.Length;

                    DW_CoeffsDeltas = thisInst.modelList.Get_DeltaWS_DW_Expo(sectWS_1[WD], UWExpo_1_All[WD], UWExpo_2_All[WD], DWExpo_1_All[WD], DWExpo_2_All[WD], P10_UW_All[WD], P10_DW_All[WD], thisModel, WD, useFlowSep);

                    if (DW_CoeffsDeltas != null)
                        numDW_CoeffsDeltas = DW_CoeffsDeltas.Length;

                    deltaWS_UWExpo = deltaWS_UWExpo + UW_CoeffsDeltas[numUW_CoeffsDeltas - 1].deltaWS_Expo * endNodeWindRose[WD];
                    deltaWS_DWExpo = deltaWS_DWExpo + DW_CoeffsDeltas[numDW_CoeffsDeltas - 1].deltaWS_Expo * endNodeWindRose[WD];

                }
            }
            else
            {
                WS_1 = WS_Equiv[WD_Ind];
                UW_CoeffsDeltas = thisInst.modelList.Get_DeltaWS_UW_Expo(UWExpo_1, UWExpo_2, DWExpo_1, DWExpo_2, (P10_UW_1 + P10_UW_2) / 2, (P10_DW_1 + P10_DW_2) / 2, thisModel,
                                                            WD_Ind, radiusIndex, node1.flowSepNodes, node2.flowSepNodes, WS_1, useFlowSep, node1Coords, node2Coords);

                if (UW_CoeffsDeltas != null)
                    numUW_CoeffsDeltas = UW_CoeffsDeltas.Length;

                for (int Coeff_ind = 0; Coeff_ind < numUW_CoeffsDeltas; Coeff_ind++)
                {
                    if (UW_CoeffsDeltas[Coeff_ind].flowType != "Total")
                    {
                        if (Coeff_ind > 0)
                            objList_UW = thisInst.lstPathNodes_UW.Items.Add((Coeff_ind + 1).ToString());
                        else
                            objList_UW = thisInst.lstPathNodes_UW.Items.Add(endStr);

                        objList_UW.SubItems.Add(UW_CoeffsDeltas[Coeff_ind].flowType);
                        objList_UW.SubItems.Add(Math.Round(UW_CoeffsDeltas[Coeff_ind].coeff, 4).ToString());
                        objList_UW.SubItems.Add(Math.Round(UW_CoeffsDeltas[Coeff_ind].deltaWS_Expo, 2).ToString());
                    }
                }

                DW_CoeffsDeltas = thisInst.modelList.Get_DeltaWS_DW_Expo(WS_1, UWExpo_1, UWExpo_2, DWExpo_1, DWExpo_2, (P10_UW_1 + P10_UW_2) / 2, (P10_DW_1 + P10_DW_2) / 2, thisModel, WD_Ind, useFlowSep);

                if (DW_CoeffsDeltas != null)
                    numDW_CoeffsDeltas = DW_CoeffsDeltas.Length;

                for (int Coeff_ind = 0; Coeff_ind < numDW_CoeffsDeltas; Coeff_ind++)
                {
                    if (DW_CoeffsDeltas[Coeff_ind].flowType != "Total")
                    {
                        if (Coeff_ind > 0)
                            objList_DW = thisInst.lstPathNodes_DW.Items.Add((Coeff_ind + 1).ToString());
                        else
                            objList_DW = thisInst.lstPathNodes_DW.Items.Add(endStr);

                        objList_DW.SubItems.Add(DW_CoeffsDeltas[Coeff_ind].flowType);
                        objList_DW.SubItems.Add(Math.Round(DW_CoeffsDeltas[Coeff_ind].coeff, 4).ToString());
                        objList_DW.SubItems.Add(Math.Round(DW_CoeffsDeltas[Coeff_ind].deltaWS_Expo, 2).ToString());
                    }
                }

                deltaWS_UWExpo = UW_CoeffsDeltas[numUW_CoeffsDeltas - 1].deltaWS_Expo;
                deltaWS_DWExpo = DW_CoeffsDeltas[numDW_CoeffsDeltas - 1].deltaWS_Expo;

            }

            if (paramsToShow.show_dWS_UWExpo) objListItem.SubItems.Add(Math.Round(deltaWS_UWExpo, 2).ToString());
            if (paramsToShow.show_dWS_DWExpo) objListItem.SubItems.Add(Math.Round(deltaWS_DWExpo, 2).ToString());

            if (gotSR == true)
            {
                deltaWS_UW_SRDH = 0;
                deltaWS_DW_SRDH = 0;

                if (WD_Ind == numWD)
                {
                    for (int WD = 0; WD < numWD; WD++)
                    {
                        UW_Stab_Corr_1[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, UW_SR_All_1[WD], useFlowSep, "UW");
                        UW_SR_All_2[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD, "UW", "SR");
                        UW_DH_All_2[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD, "UW", "DH");
                        UW_Stab_Corr_2[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, UW_SR_All_2[WD], useFlowSep, "UW");

                        deltaWS_UW_SRDH = deltaWS_UW_SRDH + thisInst.modelList.GetDeltaWS_SRDH(sectWS_1[WD], HH, UW_SR_All_1[WD], UW_SR_All_2[WD], UW_DH_All_1[WD],
                            UW_DH_All_2[WD], UW_Stab_Corr_1[WD], UW_Stab_Corr_2[WD]) * endNodeWindRose[WD];

                        DW_SR_All_2[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD, "DW", "SR");
                        DW_DH_All_2[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD, "DW", "DH");

                        DW_Stab_Corr_1[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, DW_SR_All_1[WD], useFlowSep, "UW");
                        DW_Stab_Corr_2[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, DW_SR_All_2[WD], useFlowSep, "UW");

                        deltaWS_DW_SRDH = deltaWS_DW_SRDH + thisInst.modelList.GetDeltaWS_SRDH(sectWS_1[WD], HH, DW_SR_All_1[WD], DW_SR_All_2[WD], DW_DH_All_1[WD],
                            DW_DH_All_2[WD], DW_Stab_Corr_1[WD], DW_Stab_Corr_2[WD]) * endNodeWindRose[WD];
                    }
                }
                else
                {
                    UW_Stab_Corr_1[WD_Ind] = thisModel.GetStabilityCorrection(avgUW[WD_Ind], avgDW[WD_Ind], WD_Ind, UW_SR_All_1[WD_Ind], useFlowSep, "UW");
                    UW_SR_All_2[WD_Ind] = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "SR");
                    UW_DH_All_2[WD_Ind] = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "DH");
                    UW_Stab_Corr_2[WD_Ind] = thisModel.GetStabilityCorrection(avgUW[WD_Ind], avgDW[WD_Ind], WD_Ind, UW_SR_All_2[WD_Ind], useFlowSep, "UW");

                    deltaWS_UW_SRDH = thisInst.modelList.GetDeltaWS_SRDH(sectWS_1[WD_Ind], HH, UW_SR_All_1[WD_Ind], UW_SR_All_2[WD_Ind], UW_DH_All_1[WD_Ind],
                        UW_DH_All_2[WD_Ind], UW_Stab_Corr_1[WD_Ind], UW_Stab_Corr_2[WD_Ind]);

                    DW_SR_All_2[WD_Ind] = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "SR");
                    DW_DH_All_2[WD_Ind] = endNode.expo[radiusIndex].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "DH");

                    DW_Stab_Corr_1[WD_Ind] = thisModel.GetStabilityCorrection(avgUW[WD_Ind], avgDW[WD_Ind], WD_Ind, DW_SR_All_1[WD_Ind], useFlowSep, "DW");
                    DW_Stab_Corr_2[WD_Ind] = thisModel.GetStabilityCorrection(avgUW[WD_Ind], avgDW[WD_Ind], WD_Ind, DW_SR_All_2[WD_Ind], useFlowSep, "DW");

                    deltaWS_DW_SRDH = thisInst.modelList.GetDeltaWS_SRDH(sectWS_1[WD_Ind], HH, DW_SR_All_1[WD_Ind], DW_SR_All_2[WD_Ind], DW_DH_All_1[WD_Ind], DW_DH_All_2[WD_Ind],
                                                                DW_Stab_Corr_1[WD_Ind], DW_Stab_Corr_2[WD_Ind]);

                    objList_UW.SubItems.Add(Math.Round(deltaWS_UW_SRDH, 2).ToString());
                    objList_DW.SubItems.Add(Math.Round(deltaWS_DW_SRDH, 2).ToString());

                }

                if (paramsToShow.show_dWS_UW_SRDH) objListItem.SubItems.Add(Math.Round(deltaWS_UW_SRDH, 2).ToString());
                if (paramsToShow.show_dWS_DW_SRDH) objListItem.SubItems.Add(Math.Round(deltaWS_DW_SRDH, 2).ToString());
            }
            else
            {
                if (paramsToShow.show_dWS_UW_SRDH) objListItem.SubItems.Add("");
                if (paramsToShow.show_dWS_DW_SRDH) objListItem.SubItems.Add("");
            }

            if (WD_Ind == numWD)
            {
                if (paramsToShow.showWS_Est) objListItem.SubItems.Add(Math.Round(estWS, 2).ToString());
            }
            else
            {
                if (paramsToShow.showWS_Est) objListItem.SubItems.Add(Math.Round(estSectorWS[WD_Ind], 2).ToString());
            }

            if (paramsToShow.showEquivWS) objListItem.SubItems.Add("");
            if (paramsToShow.showActualWS && actWS != 0) objListItem.SubItems.Add(Math.Round(actWS, 2).ToString());

        }

        /// <summary> Adds UTMs, elevation, P10 UW/DW, or UW/DW exposure to Path Node List on Advanced tab. </summary>
        public void AddUTMsP10sExposToPathNodeList(Advanced_to_show paramsToShow, double UTMX, double UTMY, double elev, double P10_UW_1, double P10_DW_1, double UWExpo_1, double DWExpo_1)
        {
            if (paramsToShow.showUTMX) objListItem.SubItems.Add(UTMX.ToString());
            if (paramsToShow.showUTMY) objListItem.SubItems.Add(UTMY.ToString());
            if (paramsToShow.showElevation) objListItem.SubItems.Add(Math.Round(elev, 1).ToString());
            if (paramsToShow.showP10UW) objListItem.SubItems.Add(Math.Round(P10_UW_1, 2).ToString());
            if (paramsToShow.showP10DW) objListItem.SubItems.Add(Math.Round(P10_DW_1, 2).ToString());
            if (paramsToShow.showUWExpo) objListItem.SubItems.Add(Math.Round(UWExpo_1, 2).ToString());
            if (paramsToShow.showDWExpo) objListItem.SubItems.Add(Math.Round(DWExpo_1, 2).ToString());
        }

        /// <summary> Updates dropdown menu on Uncertainty tab (Round Robin ) specifying number of mets used in Round Robin. </summary>
        public void RoundRobinDropdown(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            thisInst.cboRoundRobin.Items.Clear();
            thisInst.cboRoundRobin.Text = "";

            string[] dropdownStr = null;
            int numRR = 0;
            int numMetsUsed;

            if (thisInst.metPairList.RoundRobinCount > 0)
            {
                for (int i = 0; i <= thisInst.metPairList.RoundRobinCount - 1; i++)
                {
                    numMetsUsed = thisInst.metPairList.roundRobinEsts[i].metSubSize;
                    Array.Resize(ref dropdownStr, numRR + 1);
                    dropdownStr[numRR] = "Using " + numMetsUsed + " mets in model";
                    numRR++;
                }

                int lastSize = 0;
                int dropInd = 0;

                // List from small to large
                for (int i = 0; i < numRR; i++)
                {
                    if (lastSize == 0)
                    { // Find smallest RR size
                        lastSize = thisInst.metPairList.roundRobinEsts[0].metSubSize;

                        for (int j = 1; j < numRR; j++)
                        {
                            if (thisInst.metPairList.roundRobinEsts[j].metSubSize < lastSize)
                            {
                                lastSize = thisInst.metPairList.roundRobinEsts[j].metSubSize;
                                dropInd = j;
                            }
                        }
                    }
                    else
                    {
                        int thisSize = 100;
                        for (int j = 0; j < numRR; j++)
                        {
                            if (thisInst.metPairList.roundRobinEsts[j].metSubSize > lastSize && thisInst.metPairList.roundRobinEsts[j].metSubSize < thisSize)
                            {
                                thisSize = thisInst.metPairList.roundRobinEsts[j].metSubSize;
                                dropInd = j;
                            }
                        }
                    }

                    thisInst.cboRoundRobin.Items.Add(dropdownStr[dropInd]);
                    lastSize = thisInst.metPairList.roundRobinEsts[dropInd].metSubSize;
                }
            }

            if (thisInst.cboRoundRobin.Items.Count > 0)
                thisInst.cboRoundRobin.SelectedIndex = 0;

            thisInst.okToUpdate = true;

        }

        /// <summary> Updates table on Uncertainty tab showing the results of Round Robin estimates. </summary>       
        public void RoundRobinIndivResults(Continuum thisInst)
        {
            thisInst.lstRR_AllErr.Items.Clear();

            if (thisInst.metPairList.RoundRobinCount > 0 && thisInst.cboRoundRobin.SelectedItem != null)
            {
                int RR_ind = thisInst.cboRoundRobin.SelectedIndex;
                string dropText = thisInst.cboRoundRobin.SelectedItem.ToString();

                string[] textSplit = dropText.Split(Convert.ToChar(" "));
                int numMetsinRR = Convert.ToInt16(textSplit[1]);
                string[] metsUsed = thisInst.metList.GetMetsUsed();

                for (int i = 0; i < thisInst.metPairList.RoundRobinCount; i++)
                {
                    MetPairCollection.RR_WS_Ests thisRR = thisInst.metPairList.roundRobinEsts[i];
                    bool sameMets = thisInst.metList.sameMets(metsUsed, thisRR.metsUsed);
                    if ((sameMets == true && thisRR.metSubSize == numMetsinRR))
                    {
                        RR_ind = i;
                        break;
                    }
                }

                if (RR_ind >= 0)
                {
                    // List WS Estimate Errors                    
                    thisInst.lstRR_AllErr.Items.Clear();
                    MetPairCollection.RR_WS_Ests thisRR = thisInst.metPairList.roundRobinEsts[RR_ind];
                    int numErrs = thisRR.RMS_Err.Length;
                    int numToPredict = thisRR.avgWS_Ests.GetUpperBound(0) + 1;

                    // List with overall RMS for specified UW&DW model
                    string[] metsInModel = new string[thisRR.metsInModel.GetUpperBound(0) + 1];

                    for (int i = 0; i < numErrs; i++)
                    {
                        for (int n = 0; n <= thisRR.metsInModel.GetUpperBound(0); n++)
                            metsInModel[n] = thisRR.metsInModel[n, i];

                        string metStr = metsInModel[0];
                        for (int n = 1; n <= metsInModel.Length - 1; n++)
                            metStr = metStr + ", " + metsInModel[n];

                        int counter = 1;
                        for (int j = 0; j < numToPredict; j++)
                        {
                            string metPredStr = thisRR.avgWS_Ests[j, i].predictee;
                            objListItem = thisInst.lstRR_AllErr.Items.Add(counter.ToString());
                            objListItem.SubItems.Add(metStr);
                            objListItem.SubItems.Add(metPredStr);
                            objListItem.SubItems.Add((thisRR.avgWS_Ests[j, i].estError).ToString("P"));

                            counter++;
                        }
                        objListItem = thisInst.lstRR_AllErr.Items.Add("RMS of WS Ests");
                        objListItem.SubItems.Add(metStr);
                        objListItem.SubItems.Add("Avg of All");
                        objListItem.SubItems.Add("");
                        objListItem.SubItems.Add(thisRR.RMS_Err[i].ToString("P"));

                    }

                    objListItem = thisInst.lstRR_AllErr.Items.Add("RMS of All WS Ests");
                    objListItem.SubItems.Add("All Combos");
                    objListItem.SubItems.Add("Avg of All");
                    objListItem.SubItems.Add("");
                    objListItem.SubItems.Add(thisRR.RMS_All.ToString("P"));

                }
            }
        }

        /// <summary> Updates plot on Uncertainty tab showing turbine uncertainty estimates. </summary>  
        public void TurbineUncertPlot(Continuum thisInst)
        {
            thisInst.plotTurbUncert.Model = new PlotModel();
            var model = thisInst.plotTurbUncert.Model;
            model.IsLegendVisible = false;

            if (thisInst.turbineList.GotEst("WS", new TurbineCollection.PowerCurve(), null) == false)
            {
                thisInst.plotTurbUncert.Refresh();
                return;
            }

            // Find whether plotting AEP or WS
            bool plotWS = false;

            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Uncertainty");
            int Sel_Ind;

            try
            {
                Sel_Ind = thisInst.cboUncert_WS_AEP.SelectedIndex;
            }
            catch
            {
                thisInst.cboUncert_WS_AEP.SelectedIndex = 0;
            }

            if (thisInst.cboUncert_WS_AEP.SelectedIndex == 0)
                plotWS = true;
            else
                plotWS = false;

            int numTurbines = thisInst.turbineList.TurbineCount;

            if (plotWS == false)
            {
                if (powerCurve.name == "")
                {
                    thisInst.plotTurbUncert.Refresh();
                    return;
                }
            }

            if (numTurbines > 0)
            {
                if ((thisInst.turbineList.turbineEsts[0].avgWS_Est != null && plotWS == true) ||
                    (thisInst.turbineList.turbineEsts[0].grossAEP != null && plotWS == false))
                {
                    ScatterSeries P50_Series = new ScatterSeries();
                    P50_Series.MarkerStroke = OxyColors.Blue;
                    P50_Series.Title = "P50";

                    ScatterSeries P90_Series = new ScatterSeries();
                    P90_Series.MarkerStroke = OxyColors.Red;
                    P90_Series.Title = "P90";

                    ScatterSeries P99_Series = new ScatterSeries();
                    P99_Series.MarkerStroke = OxyColors.Green;
                    P99_Series.Title = "P99";

                    for (int i = 0; i <= numTurbines - 1; i++)
                    {
                        Turbine thisTurb = thisInst.turbineList.turbineEsts[i];

                        if (thisTurb.AvgWSEst_Count > 0)
                        {
                            Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(null);
                            Turbine.Gross_Energy_Est grossEnergy = thisTurb.GetGrossEnergyEst(powerCurve);

                            if (plotWS == true)
                            {
                                P50_Series.Points.Add(new ScatterPoint(i + 1, Math.Round(avgEst.freeStream.WS, 3)));
                                P90_Series.Points.Add(new ScatterPoint(i + 1, Math.Round(avgEst.freeStream.WS - avgEst.freeStream.WS * avgEst.uncert * 1.28155, 3)));
                                P99_Series.Points.Add(new ScatterPoint(i + 1, Math.Round(avgEst.freeStream.WS - avgEst.freeStream.WS * avgEst.uncert * 2.326, 3)));
                            }
                            else
                            {
                                P50_Series.Points.Add(new ScatterPoint(i + 1, Math.Round(grossEnergy.AEP, 0)));
                                P90_Series.Points.Add(new ScatterPoint(i + 1, Math.Round(grossEnergy.P90, 0)));
                                P99_Series.Points.Add(new ScatterPoint(i + 1, Math.Round(grossEnergy.P99, 0)));
                            }
                        }
                    }

                    model.Series.Add(P50_Series);
                    model.Series.Add(P90_Series);
                    model.Series.Add(P99_Series);

                    // Specify axes
                    LinearAxis xAxis = new LinearAxis();
                    xAxis.Position = AxisPosition.Bottom;
                    xAxis.Title = "Turbine Site #";
                    xAxis.Minimum = xAxis.Minimum * 0.99;
                    xAxis.Maximum = xAxis.Maximum * 1.01;
                    LinearAxis yAxis = new LinearAxis();
                    yAxis.Position = AxisPosition.Left;
                    yAxis.Minimum = yAxis.Minimum * 0.99;
                    yAxis.Maximum = yAxis.Maximum * 1.01;

                    model.Axes.Add(xAxis);
                    model.Axes.Add(yAxis);

                    if (plotWS == true)
                    {
                        model.Title = "Turbine Wind Speed Ests: P50, P90 & P99";
                        yAxis.Title = "Wind Speed, m/s";
                    }
                    else
                    {
                        model.Title = "Turbine Gross AEP Ests: P50, P90 & P99";
                        yAxis.Title = "Gross AEP, MWh";
                    }

                    if (thisInst.chkP99.Checked == false)
                        P99_Series.IsVisible = false;

                    if (thisInst.chkP50.Checked == false)
                        P50_Series.IsVisible = false;

                    if (thisInst.chkP90.Checked == false)
                        P90_Series.IsVisible = false;

                    thisInst.plotTurbUncert.Refresh();
                }
            }

        }

        /// <summary> Updates table on Uncertainty tab showing RMS error of Round Robin analyses. </summary>        
        public void RoundRobinResults(Continuum thisInst)
        {
            int numRR = thisInst.metPairList.RoundRobinCount;
            MetPairCollection.RR_WS_Ests thisRR = new MetPairCollection.RR_WS_Ests();

            thisInst.lstRR_Results.Items.Clear();

            thisInst.plotRRErrorByNumMets.Model = new PlotModel();
            var model = thisInst.plotRRErrorByNumMets.Model;
            model.IsLegendVisible = false;

            LineSeries RR_RMS = new LineSeries();
            RR_RMS.MarkerType = MarkerType.Square;
            RR_RMS.Title = "Round Robin RMS Error";
            model.Series.Add(RR_RMS);

            if (thisInst.modelList.ModelCount == 0)
            {
                thisInst.plotRRErrorByNumMets.Refresh();
                return;
            }

            string[] metsUsed = null;
            int numMetsUsed = thisInst.metList.ThisCount;

            if (numMetsUsed > 0)
            {
                metsUsed = new string[numMetsUsed];
                for (int i = 0; i <= numMetsUsed - 1; i++)
                    metsUsed[i] = thisInst.metList.metItem[i].name;
            }

            // List with overall RMS
            int lastSize = 0;

            // List from small to large
            for (int i = 0; i < numRR; i++)
            {
                if (lastSize == 0)
                { // Find smallest RR size
                    lastSize = thisInst.metPairList.roundRobinEsts[0].metSubSize;
                    thisRR = thisInst.metPairList.roundRobinEsts[0];
                    for (int j = 1; j < numRR; j++)
                    {
                        if (thisInst.metPairList.roundRobinEsts[j].metSubSize < lastSize)
                        {
                            lastSize = thisInst.metPairList.roundRobinEsts[j].metSubSize;
                            thisRR = thisInst.metPairList.roundRobinEsts[j];
                        }
                    }
                }
                else
                {
                    int thisSize = 100;
                    for (int j = 0; j < numRR; j++)
                    {
                        if (thisInst.metPairList.roundRobinEsts[j].metSubSize > lastSize && thisInst.metPairList.roundRobinEsts[j].metSubSize < thisSize)
                        {
                            thisSize = thisInst.metPairList.roundRobinEsts[j].metSubSize;
                            thisRR = thisInst.metPairList.roundRobinEsts[j];
                        }
                    }
                }

                objListItem = thisInst.lstRR_Results.Items.Add(thisRR.metSubSize.ToString());
                objListItem.SubItems.Add(thisRR.RMS_All.ToString("P"));
                objListItem.SubItems.Add(thisRR.RMS_Err.Length.ToString());
                lastSize = thisRR.metSubSize;
            }

            // Plot with overall RMS            
            for (int i = 0; i < numRR; i++)
            {
                thisRR = thisInst.metPairList.roundRobinEsts[i];
                bool sameMets = thisInst.metList.sameMets(thisRR.metsUsed, metsUsed);
                if (sameMets == true)
                {
                    int numMets = thisInst.metPairList.roundRobinEsts[i].metSubSize;
                    double thisRMS = 100 * thisInst.metPairList.roundRobinEsts[i].RMS_All;
                    RR_RMS.Points.Add(new DataPoint(numMets, Math.Round(thisRMS, 3)));
                }
            }

            string Xlabel1 = "Num Mets Used in Model";
            string yLabel1 = "% Error ";

            model.Title = yLabel1 + " vs " + Xlabel1;

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = Xlabel1;
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = yLabel1;

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            thisInst.plotRRErrorByNumMets.Refresh();

        }

        /// <summary> Updates four textboxes on Advanced tab: Mets used, RMS error (mets used), RMS error (all mets), sect error. </summary>
        public void ModelParams(Continuum thisInst)
        {
            int radiusIndex = thisInst.GetRadiusInd("Advanced");

            int WD_Ind = thisInst.GetWD_ind("Advanced");
            Met.TOD thisTOD = thisInst.GetSelectedTOD("Advanced");
            Met.Season thisSeason = thisInst.GetSelectedSeason("Advanced");

            thisInst.txtUWDWRMS.Text = "";
            thisInst.txtSectRMS.Text = "";
            int numModels = thisInst.modelList.ModelCount;
            string[] metsUsed = thisInst.metList.GetMetsUsed();

            bool isImported = false;
            if (numModels > 0)
                if (thisInst.modelList.models[0, 0].isImported)
                    isImported = true;

            if (isImported == false && (thisInst.metList.ThisCount == 0 || numModels == 0 || radiusIndex == -1))
                return;

            int numWD = thisInst.metList.numWD;

            if (isImported == false && (thisInst.modelList.models[0, 0].RMS_Sect_WS_Est == null || thisInst.metList.expoIsCalc == false))
                return;

            Model[] theseModels = thisInst.modelList.GetModels(thisInst, metsUsed, thisTOD, thisSeason, thisInst.modeledHeight, false);
            Model thisModel = theseModels[0];

            if (thisModel == null)
                return;

            thisModel = theseModels[radiusIndex];

            bool Wgt_or_No;

            if (thisInst.chkWeight_RMS.Checked == true)
                Wgt_or_No = true;
            else
                Wgt_or_No = false;

            thisInst.txtUWDWRMS.Text = Math.Round(thisModel.RMS_WS_Est, 5).ToString("P");

            if (thisModel.RMS_Sect_WS_Est != null)
            {
                if (thisInst.metList.ThisCount > 1 && WD_Ind == numWD)
                {
                    double thisErr = thisModel.CalcRMS_SectorsEstError(Wgt_or_No, thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, thisTOD, thisSeason));
                    thisInst.txtSectRMS.Text = Math.Round(thisErr, 5).ToString("P");
                }
                else if (WD_Ind != numWD)
                {
                    double thisErr = thisModel.RMS_Sect_WS_Est[WD_Ind];
                    thisInst.txtSectRMS.Text = Math.Round(thisErr, 5).ToString("P");
                }
            }
        }

        /// <summary> Updates radius of investigation dropdown menu on Met and Turb Summary tab and Advanced tab. </summary>
        public void RadiusToDisplay(string summ_or_Adv, Continuum thisInst) // 
        {
            thisInst.okToUpdate = false;

            if (thisInst.radiiList.ThisCount > 0)
            {
                int numRadii = thisInst.radiiList.ThisCount;

                if (summ_or_Adv == "Summary")
                {
                    thisInst.cboMetSum_Rad.Items.Clear();

                    for (int i = 0; i < numRadii; i++)
                        thisInst.cboMetSum_Rad.Items.Add(thisInst.radiiList.investItem[i].radius);

                    thisInst.cboMetSum_Rad.SelectedIndex = 0;
                }
                else
                {
                    thisInst.cboAdvancedRad.Items.Clear();
                    for (int i = 0; i < numRadii; i++)
                        thisInst.cboAdvancedRad.Items.Add(thisInst.radiiList.investItem[i].radius);

                    thisInst.cboAdvancedRad.SelectedIndex = 0;
                }
            }
            else
            {
                thisInst.cboMetSum_Rad.Items.Clear();
                thisInst.cboAdvancedRad.Items.Clear();
                thisInst.lstPathNodes.Items.Clear();
                thisInst.lstPathNodes_DW.Items.Clear();
                thisInst.lstPathNodes_UW.Items.Clear();
                ModelPlots(thisInst);
                ModelParams(thisInst);
                ModCrossPredictions(thisInst);
                StepTopoMap(thisInst);
            }

            thisInst.okToUpdate = true;
        }

        /// <summary> Updates all wind direction dropdown menus. </summary>
        public void WindDirectionToDisplay(Continuum thisInst)
        {
            thisInst.okToUpdate = false;

            int numWD = thisInst.GetNumWD();

            // Update WD dropdown on MCP tab            
            if (numWD == 4)
                thisInst.cboMCPNumWD.SelectedIndex = 0;
            else if (numWD == 8)
                thisInst.cboMCPNumWD.SelectedIndex = 1;
            else if (numWD == 12)
                thisInst.cboMCPNumWD.SelectedIndex = 2;
            else if (numWD == 16)
                thisInst.cboMCPNumWD.SelectedIndex = 3;
            else if (numWD == 24)
                thisInst.cboMCPNumWD.SelectedIndex = 4;

            thisInst.cboAdvancedWD.Items.Clear();
            thisInst.cboSummaryWD.Items.Clear();
            thisInst.cboGrossWD.Items.Clear();
            thisInst.cboNetWD.Items.Clear();
            thisInst.cboMapWD.Items.Clear();
            thisInst.cboMCP_WD.Items.Clear();
            thisInst.cboTurbWD.Items.Clear();
            thisInst.cboInflowWD.Items.Clear();

            for (int WD = 0; WD < numWD; WD++)
            {
                thisInst.cboAdvancedWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
                thisInst.cboSummaryWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
                thisInst.cboGrossWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
                thisInst.cboNetWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
                thisInst.cboMapWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
                thisInst.cboMCP_WD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
                thisInst.cboTurbWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
                thisInst.cboInflowWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1)).ToString());
            }

            thisInst.cboAdvancedWD.Items.Add("Overall");
            thisInst.cboAdvancedWD.SelectedIndex = numWD;

            thisInst.cboSummaryWD.Items.Add("Overall");
            thisInst.cboSummaryWD.SelectedIndex = numWD;

            thisInst.cboGrossWD.Items.Add("Overall");
            thisInst.cboGrossWD.SelectedIndex = numWD;

            thisInst.cboNetWD.Items.Add("Overall");
            thisInst.cboNetWD.SelectedIndex = numWD;

            thisInst.cboMapWD.Items.Add("Overall");
            thisInst.cboMapWD.SelectedIndex = numWD;

            thisInst.cboMCP_WD.Items.Add("Overall");
            thisInst.cboMCP_WD.SelectedIndex = numWD;

            thisInst.cboTurbWD.Items.Add("Overall");
            thisInst.cboTurbWD.SelectedIndex = numWD;

            thisInst.cboInflowWD.SelectedIndex = 0;
            thisInst.okToUpdate = true;
        }

        /// <summary> Updates all season dropdown menus. </summary>
        public void SeasonDropdown(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            thisInst.cboSummSeason.Items.Clear();
            thisInst.cboMCP_Season.Items.Clear();
            thisInst.cboSeasonAdvanced.Items.Clear();

            if (thisInst.metList.numSeason > 1)
            {
                thisInst.cboSummSeason.Items.Add("Winter");
                thisInst.cboSummSeason.Items.Add("Spring");
                thisInst.cboSummSeason.Items.Add("Summer");
                thisInst.cboSummSeason.Items.Add("Fall");

                thisInst.cboMCP_Season.Items.Add("Winter");
                thisInst.cboMCP_Season.Items.Add("Spring");
                thisInst.cboMCP_Season.Items.Add("Summer");
                thisInst.cboMCP_Season.Items.Add("Fall");

                thisInst.cboSeasonAdvanced.Items.Add("Winter");
                thisInst.cboSeasonAdvanced.Items.Add("Spring");
                thisInst.cboSeasonAdvanced.Items.Add("Summer");
                thisInst.cboSeasonAdvanced.Items.Add("Fall");
            }

            thisInst.cboSummSeason.Items.Add("All Seasons");
            thisInst.cboMCP_Season.Items.Add("All Seasons");
            thisInst.cboSeasonAdvanced.Items.Add("All Seasons");

            if (thisInst.metList.numSeason > 1)
            {
                thisInst.cboSummSeason.SelectedIndex = thisInst.metList.numSeason;
                thisInst.cboMCP_Season.SelectedIndex = thisInst.metList.numSeason;
                thisInst.cboSeasonAdvanced.SelectedIndex = thisInst.metList.numSeason;
            }
            else
            {
                thisInst.cboSummSeason.SelectedIndex = 0;
                thisInst.cboMCP_Season.SelectedIndex = 0;
                thisInst.cboSeasonAdvanced.SelectedIndex = 0;
            }

            thisInst.okToUpdate = true;
        }

        /// <summary> Updates all time of day dropdown menus. </summary>
        public void TOD_Dropdown(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            thisInst.cboMCP_TOD.Items.Clear();
            thisInst.cboTODAdvanced.Items.Clear();
            thisInst.cboSummTOD.Items.Clear();

            if (thisInst.metList.numTOD > 1)
            {
                thisInst.cboMCP_TOD.Items.Add("Day");
                thisInst.cboTODAdvanced.Items.Add("Day");
                thisInst.cboSummTOD.Items.Add("Day");

                thisInst.cboMCP_TOD.Items.Add("Night");
                thisInst.cboTODAdvanced.Items.Add("Night");
                thisInst.cboSummTOD.Items.Add("Night");
            }

            thisInst.cboMCP_TOD.Items.Add("All Hours");
            thisInst.cboTODAdvanced.Items.Add("All Hours");
            thisInst.cboSummTOD.Items.Add("All Hours");

            if (thisInst.metList.numTOD > 1)
            {
                thisInst.cboMCP_TOD.SelectedIndex = thisInst.metList.numTOD;
                thisInst.cboTODAdvanced.SelectedIndex = thisInst.metList.numTOD;
                thisInst.cboSummTOD.SelectedIndex = thisInst.metList.numTOD;
                thisInst.cboSummTOD.Enabled = true;
            }
            else
            {
                thisInst.cboMCP_TOD.SelectedIndex = 0;
                thisInst.cboTODAdvanced.SelectedIndex = 0;
                thisInst.cboSummTOD.SelectedIndex = 0;
                thisInst.cboSummTOD.Enabled = false;
            }
            thisInst.okToUpdate = true;
        }

        /// <summary> Updates "Start Met" dropdown menu on Advanced tab. </summary>
        public void StartMet_Dropdown(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            thisInst.cboStartMet.Items.Clear();

            string[] metsUsed = thisInst.metList.GetMetsUsed();
            int numMets = metsUsed.Length;

            for (int i = 0; i < numMets; i++)
                thisInst.cboStartMet.Items.Add(metsUsed[i]);

            if (thisInst.cboStartMet.Items.Count > 0)
                thisInst.cboStartMet.SelectedIndex = 0;

            thisInst.okToUpdate = true;
        }

        /// <summary> Updates "End Met" dropdown menu on Advanced tab. </summary>
        public void EndMet_Dropdown(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            string Start_met_name = thisInst.GetStartMetAdvanced();
            thisInst.cboEndMet.Items.Clear();

            for (int i = 0; i < thisInst.metList.ThisCount; i++)
            {
                Met thisMet = thisInst.metList.metItem[i];
                if (thisMet.name != Start_met_name)
                    thisInst.cboEndMet.Items.Add(thisMet.name);
            }

            if (thisInst.turbineList.GotEst("WS", new TurbineCollection.PowerCurve(), null) == true)
            {
                for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
                {
                    Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                    thisInst.cboEndMet.Items.Add(thisTurb.name);
                }
            }

            if (thisInst.cboEndMet.Items.Count > 0) thisInst.cboEndMet.SelectedIndex = 0;
            thisInst.okToUpdate = true;
        }

        /// <summary> Updates Met and Turbine labels on thisInst.topo/SR/DH/LC map on Input tab. </summary>
        public void Labels(Continuum thisInst)
        {
            Met[] checkedMets = thisInst.GetCheckedMets("Input");
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Input");
            int numMets = 0;
            int turbCount = 0;

            if (checkedMets == null)
                numMets = 0;
            else
                numMets = checkedMets.Length;

            // Create label series for met sites                                 
            for (int i = 0; i < numMets; i++)
            {
                var metSeries = new ScatterSeries();
                metSeries.MarkerSize = 5;
                metSeries.MarkerStrokeThickness = 1;
                metSeries.MarkerType = MarkerType.Circle;
                metSeries.MarkerFill = OxyColors.Black;
                metSeries.MarkerStroke = OxyColors.Red;
                metSeries.RenderInLegend = false;
                metSeries.Title = checkedMets[i].name;

                metSeries.Points.Add(new ScatterPoint(checkedMets[i].UTMX, checkedMets[i].UTMY, 5, checkedMets[i].elev));
                thisInst.plotTopo.Model.Series.Add(metSeries);
            }

            if (checkedTurbines == null)
                turbCount = 0;
            else
                turbCount = checkedTurbines.Length;

            // Create label series for turbine sites
            for (int i = 0; i < turbCount; i++)
            {
                var turbineSeries = new ScatterSeries();
                turbineSeries.MarkerSize = 5;
                turbineSeries.MarkerStrokeThickness = 1;
                turbineSeries.MarkerType = MarkerType.Diamond;
                turbineSeries.MarkerFill = OxyColors.White;
                turbineSeries.MarkerStroke = OxyColors.Black;
                turbineSeries.RenderInLegend = false;
                turbineSeries.Points.Add(new ScatterPoint(checkedTurbines[i].UTMX, checkedTurbines[i].UTMY, 5, checkedTurbines[i].elev));
                turbineSeries.Title = checkedTurbines[i].name;
                thisInst.plotTopo.Model.Series.Add(turbineSeries);
            }

            thisInst.plotTopo.Refresh();

        }

        /// <summary> Updates Met, turbine and path of nodes labels on map on Advanced tab. </summary>
        public void StepLabels(Continuum thisInst, double minX, double maxX, double minY, double maxY)
        {
            Met[] checkedMets = thisInst.GetCheckedMets("Advanced");
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Advanced");

            int checkedMetCount = 0;
            int checkedTurbCount = 0;

            if (checkedMets == null)
                checkedMetCount = 0;
            else
                checkedMetCount = checkedMets.Length;

            if (checkedTurbines == null)
                checkedTurbCount = 0;
            else
                checkedTurbCount = checkedTurbines.Length;

            ScatterSeries[] labelTurbs = new ScatterSeries[checkedTurbCount];
            ScatterSeries[] labelMets = new ScatterSeries[checkedMetCount];

            int metPairCount = thisInst.metPairList.PairCount;
            int radiusIndex = thisInst.GetRadiusInd("Advanced");

            if (radiusIndex == -1) return;

            int radius = thisInst.radiiList.investItem[radiusIndex].radius;
            var model = thisInst.plotAdvTopo.Model;

            for (int i = 0; i < checkedMetCount; i++)
            {
                if (checkedMets[i].UTMX > minX && checkedMets[i].UTMX < maxX && checkedMets[i].UTMY > minY && checkedMets[i].UTMY < maxY)
                {
                    labelMets[i] = new ScatterSeries();
                    labelMets[i].MarkerType = MarkerType.Circle;
                    labelMets[i].MarkerFill = OxyColors.Black;
                    labelMets[i].MarkerStroke = OxyColors.Red;
                    labelMets[i].Title = checkedMets[i].name;
                    labelMets[i].Points.Add(new ScatterPoint(checkedMets[i].UTMX, checkedMets[i].UTMY, 5, checkedMets[i].elev));

                    model.Series.Add(labelMets[i]);
                }
            }

            for (int i = 0; i < checkedTurbCount; i++)
            {
                if (checkedTurbines[i].AvgWSEst_Count > 0)
                {
                    if (checkedTurbines[i].UTMX > minX && checkedTurbines[i].UTMX < maxX && checkedTurbines[i].UTMY > minY && checkedTurbines[i].UTMY < maxY)
                    {
                        labelTurbs[i] = new ScatterSeries();
                        labelTurbs[i].MarkerType = MarkerType.Diamond;
                        labelTurbs[i].MarkerFill = OxyColors.White;
                        labelTurbs[i].MarkerStroke = OxyColors.White;
                        labelTurbs[i].Title = checkedTurbines[i].name;
                        labelTurbs[i].Points.Add(new ScatterPoint(checkedTurbines[i].UTMX, checkedTurbines[i].UTMY, 5, checkedTurbines[i].elev));

                        model.Series.Add(labelTurbs[i]);
                    }
                }
            }


            int numNodes = 0;

            // Figure out if turbine or met end
            string met1Str = thisInst.GetStartMetAdvanced();
            Met startMet = null;
            string endStr = thisInst.GetEndSiteAdvanced();

            for (int i = 0; i < thisInst.metList.ThisCount; i++)
            {
                if (thisInst.metList.metItem[i].name == met1Str)
                {
                    startMet = thisInst.metList.metItem[i];
                    break;
                }
            }

            bool isMetPair = false;
            Pair_Of_Mets thisPair = new Pair_Of_Mets();

            for (int i = 0; i < metPairCount; i++)
            {
                thisPair = thisInst.metPairList.metPairs[i];
                numNodes = thisInst.metPairList.metPairs[i].WS_Pred[0, radiusIndex].nodePath.Length;
                isMetPair = false;
                if ((met1Str == thisPair.met1.name && endStr == thisPair.met2.name) || (met1Str == thisPair.met2.name && endStr == thisPair.met1.name))
                {
                    isMetPair = true;
                    break;
                }
            }

            if (isMetPair == true)
            {
                ScatterSeries[] label_Nodes = new ScatterSeries[numNodes];

                for (int j = 0; j < numNodes; j++)
                {
                    Nodes thisNode = thisPair.WS_Pred[0, radiusIndex].nodePath[j]; // path of nodes is same for all UW&DW models (only varies by radius)
                    if (thisNode.UTMX > minX && thisNode.UTMX < maxX && thisNode.UTMY > minY && thisNode.UTMY < maxY)
                    {
                        label_Nodes[j] = new ScatterSeries();
                        label_Nodes[j].MarkerType = MarkerType.Square;
                        label_Nodes[j].MarkerFill = OxyColors.Purple;
                        label_Nodes[j].MarkerStroke = OxyColors.Purple;
                        label_Nodes[j].Title = "Node " + (j + 1).ToString();
                        label_Nodes[j].Points.Add(new ScatterPoint(thisNode.UTMX, thisNode.UTMY, 5, thisNode.elev));

                        model.Series.Add(label_Nodes[j]);
                    }
                }
            }
            else
            {
                // met to a turbine
                // Find turbine
                bool foundTurb = false;
                Turbine endTurb = null;
                for (int i = 0; i < checkedTurbCount; i++)
                {
                    //  thisTurb = thisInst.turbineList.turbineEsts[i]
                    if (checkedTurbines[i].AvgWSEst_Count > 0)
                    {
                        if (checkedTurbines[i].name == endStr)
                        {
                            for (int j = 0; j < checkedTurbines[i].WSEst_Count; j++)
                            {
                                if (checkedTurbines[i].WS_Estimate[j].predictorMetName == met1Str && checkedTurbines[i].WS_Estimate[j].model.radius == radius)
                                {
                                    foundTurb = true;
                                    endTurb = checkedTurbines[i];
                                    break;
                                }
                            }
                        }

                        if (foundTurb == true)
                            break;
                    }
                }

                int WS_PredInd = 0;

                if (endTurb == null) return;
                if (endTurb.WSEst_Count == 0) return;
                if (endTurb.WS_Estimate[WS_PredInd].WS == 0) return;

                for (int i = 0; i <= endTurb.WSEst_Count - 1; i++)
                {
                    if (endTurb.WS_Estimate[i].predictorMetName == startMet.name && endTurb.WS_Estimate[i].model.radius == radius)
                    {
                        WS_PredInd = i;
                        break;
                    }
                }

                try
                {
                    numNodes = endTurb.WS_Estimate[WS_PredInd].pathOfNodes.Length;
                }
                catch
                {
                    numNodes = 0;
                }

                ScatterSeries[] label_Nodes = new ScatterSeries[numNodes];

                for (int j = 0; j < numNodes; j++)
                {
                    Nodes thisNode = endTurb.WS_Estimate[WS_PredInd].pathOfNodes[j];
                    if (thisNode.UTMX > minX && thisNode.UTMX < maxX && thisNode.UTMY > minY && thisNode.UTMY < maxY)
                    {
                        label_Nodes[j] = new ScatterSeries();
                        label_Nodes[j].MarkerType = MarkerType.Square;
                        label_Nodes[j].MarkerFill = OxyColors.Purple;
                        label_Nodes[j].MarkerStroke = OxyColors.Purple;
                        label_Nodes[j].Title = "Node " + (j + 1).ToString();
                        label_Nodes[j].Points.Add(new ScatterPoint(thisNode.UTMX, thisNode.UTMY, 5, thisNode.elev));

                        model.Series.Add(label_Nodes[j]);
                    }
                }
            }
        }

        /// <summary> Updates met and turbine labels on map on Maps tab. </summary>        
        public void MapLabels(Continuum thisInst)
        {
            if (thisInst.lstMaps.SelectedItems.Count > 0)
            {
                string mapToPlot = thisInst.lstMaps.SelectedItems[0].Text;
                Map thisMap = new Map();

                for (int j = 0; j < thisInst.mapList.ThisCount; j++)
                {
                    if (mapToPlot == thisInst.mapList.mapItem[j].mapName)
                    {
                        thisMap = thisInst.mapList.mapItem[j];
                        break;
                    }
                }

                int WD_Ind = thisInst.GetWD_ind("Maps");

                if (thisInst.metList.ThisCount == 0) return;
                int numWD = thisInst.GetNumWD();

                if (WD_Ind == -1 || numWD == 0)
                    return;

                Met[] checkedMets = thisInst.GetCheckedMets("Map");
                Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Map");

                int checkedMetCount = 0;
                int checkedTurbCount = 0;

                if (checkedMets == null)
                    checkedMetCount = 0;
                else
                    checkedMetCount = checkedMets.Length;

                if (checkedTurbines == null)
                    checkedTurbCount = 0;
                else
                    checkedTurbCount = checkedTurbines.Length;

                ScatterSeries[] labelMets = new ScatterSeries[checkedMetCount];
                ScatterSeries[] labelTurbs = new ScatterSeries[checkedTurbCount];

                var model = thisInst.plotGenMap.Model;

                for (int i = 0; i < checkedMetCount; i++)
                {
                    if (checkedMets[i].UTMX > thisMap.minUTMX && checkedMets[i].UTMX < (thisMap.minUTMX + thisMap.reso * thisMap.numX) &&
                            checkedMets[i].UTMY > thisMap.minUTMY && checkedMets[i].UTMY < (thisMap.minUTMY + thisMap.reso * thisMap.numY))
                    {
                        labelMets[i] = new ScatterSeries();
                        labelMets[i].MarkerType = MarkerType.Circle;
                        labelMets[i].MarkerFill = OxyColors.Black;
                        labelMets[i].MarkerStroke = OxyColors.Red;
                        labelMets[i].Points.Add(new ScatterPoint(checkedMets[i].UTMX, checkedMets[i].UTMY, 5, checkedMets[i].elev));
                        model.Series.Add(labelMets[i]);
                    }
                }

                for (int i = 0; i < checkedTurbCount; i++)
                {
                    if (checkedTurbines[i].UTMX > thisMap.minUTMX && checkedTurbines[i].UTMX < (thisMap.minUTMX + thisMap.reso * thisMap.numX) &&
                            checkedTurbines[i].UTMY > thisMap.minUTMY && checkedTurbines[i].UTMY < (thisMap.minUTMY + thisMap.reso * thisMap.numY))
                    {
                        labelTurbs[i] = new ScatterSeries();
                        labelTurbs[i].MarkerType = MarkerType.Diamond;
                        labelTurbs[i].MarkerFill = OxyColors.White;
                        labelTurbs[i].MarkerStroke = OxyColors.White;
                        labelTurbs[i].Points.Add(new ScatterPoint(checkedTurbines[i].UTMX, checkedTurbines[i].UTMY, 5, checkedTurbines[i].elev));
                        model.Series.Add(labelTurbs[i]);
                    }

                }
                thisInst.plotGenMap.Refresh();
            }

        }

        /// <summary> Updates met and turbine labels on map on Net Turbine Ests. tab. </summary>  
        public void WakeMapLabels(Continuum thisInst)
        {
            if (thisInst.metList.ThisCount == 0) return;
            var model = thisInst.plotWakeMap.Model;
                 
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Net");
            int checkedMetCount = 0;
            int checkedTurbCount = 0;                        

            if (checkedTurbines == null)
                checkedTurbCount = 0;
            else
                checkedTurbCount = checkedTurbines.Length;
           
            ScatterSeries[] labelTurbs = new ScatterSeries[checkedTurbCount];                        

            for (int i = 0; i < checkedTurbCount; i++)
            {
                labelTurbs[i] = new ScatterSeries();
                labelTurbs[i].Points.Add(new ScatterPoint(Math.Round(checkedTurbines[i].UTMX, 0), Math.Round(checkedTurbines[i].UTMY, 0), 5, checkedTurbines[i].elev));
                labelTurbs[i].MarkerSize = 5;
                labelTurbs[i].MarkerStrokeThickness = 1;
                labelTurbs[i].MarkerType = MarkerType.Diamond;
                labelTurbs[i].MarkerFill = OxyColors.White;
                labelTurbs[i].MarkerStroke = OxyColors.Black;
                labelTurbs[i].RenderInLegend = false;
                model.Series.Add(labelTurbs[i]);
            }

            thisInst.plotWakeMap.Refresh();

        }

        /// <summary> Updates roughness model and Flow Separation model textbox colors and text to indicate whether or not model was used. </summary> 
        public void ColoredTextBoxes(Continuum thisInst)
        {
            if (thisInst.topo.useSR == true)
            {
                thisInst.txtGross_LC_used.Text = "Roughness model used";
                thisInst.txtGross_LC_used.BackColor = Color.MediumSeaGreen;

                thisInst.txtLC_Net.Text = "Roughness model used";
                thisInst.txtLC_Net.BackColor = Color.MediumSeaGreen;

                thisInst.txtRR_LC_used.Text = "Roughness model used";
                thisInst.txtRR_LC_used.BackColor = Color.MediumSeaGreen;

                thisInst.txtAdv_LC_used.Text = "Roughness model used";
                thisInst.txtAdv_LC_used.BackColor = Color.MediumSeaGreen;
            }
            else
            {
                thisInst.txtGross_LC_used.Text = "Roughness model NOT used";
                thisInst.txtGross_LC_used.BackColor = Color.LightCoral;

                thisInst.txtLC_Net.Text = "Roughness model NOT used";
                thisInst.txtLC_Net.BackColor = Color.LightCoral;

                thisInst.txtRR_LC_used.Text = "Roughness model NOT used";
                thisInst.txtRR_LC_used.BackColor = Color.LightCoral;

                thisInst.txtAdv_LC_used.Text = "Roughness model NOT used";
                thisInst.txtAdv_LC_used.BackColor = Color.LightCoral;
            }

            if (thisInst.topo.useSepMod == true)
            {
                thisInst.txtGross_FlowSepUsed.Text = "Flow Sep. model used";
                thisInst.txtGross_FlowSepUsed.BackColor = Color.LightBlue;

                thisInst.txtNet_FlowSep_Used.Text = "Flow Sep. model used";
                thisInst.txtNet_FlowSep_Used.BackColor = Color.LightBlue;

                thisInst.txtRR_FlowSep_Used.Text = "Flow Sep. model used";
                thisInst.txtRR_FlowSep_Used.BackColor = Color.LightBlue;

                thisInst.txtAdv_FlowSep_Used.Text = "Flow Sep. model used";
                thisInst.txtAdv_FlowSep_Used.BackColor = Color.LightBlue;
            }
            else
            {
                thisInst.txtGross_FlowSepUsed.Text = "Flow Sep. model NOT used";
                thisInst.txtGross_FlowSepUsed.BackColor = Color.LightCoral;

                thisInst.txtNet_FlowSep_Used.Text = "Flow Sep. model NOT used";
                thisInst.txtNet_FlowSep_Used.BackColor = Color.LightCoral;

                thisInst.txtRR_FlowSep_Used.Text = "Flow Sep. model NOT used";
                thisInst.txtRR_FlowSep_Used.BackColor = Color.LightCoral;

                thisInst.txtAdv_FlowSep_Used.Text = "Flow Sep. model NOT used";
                thisInst.txtAdv_FlowSep_Used.BackColor = Color.LightCoral;
            }

            thisInst.metList.AreAllMetsMCPd();
            if (thisInst.metList.allMCPd)
            {
                thisInst.txtisMCPdGross.Text = "MCP'd Met data used";
                thisInst.txtisMCPdGross.BackColor = Color.MediumOrchid;

                thisInst.txtisMCPdNet.Text = "MCP'd Met data used";
                thisInst.txtisMCPdNet.BackColor = Color.MediumOrchid;

                thisInst.txtisMCPdUncert.Text = "MCP'd Met data used";
                thisInst.txtisMCPdUncert.BackColor = Color.MediumOrchid;

                thisInst.txtisMCPdAdv.Text = "MCP'd Met data used";
                thisInst.txtisMCPdAdv.BackColor = Color.MediumOrchid;
            }
            else
            {
                thisInst.txtisMCPdGross.Text = "Meas. Met data used";
                thisInst.txtisMCPdGross.BackColor = Color.LightCoral;

                thisInst.txtisMCPdNet.Text = "Meas. Met data used";
                thisInst.txtisMCPdNet.BackColor = Color.LightCoral;

                thisInst.txtisMCPdUncert.Text = "Meas. Met data used";
                thisInst.txtisMCPdUncert.BackColor = Color.LightCoral;

                thisInst.txtisMCPdAdv.Text = "Meas. Met data used";
                thisInst.txtisMCPdAdv.BackColor = Color.LightCoral;
            }

        }

        /// <summary> Updates Land Cover textbox to indicate which LC key has been selected. </summary> 
        public void LC_KeySelected(Continuum thisInst)
        {
            bool isNLCD = thisInst.topo.LC_IsDefaultNLCD(thisInst.topo.LC_Key);
            bool isNALC = thisInst.topo.LC_IsDefaultNALC(thisInst.topo.LC_Key);
            bool isEULC = thisInst.topo.LC_IsDefaultEU_Corine(thisInst.topo.LC_Key);

            if (thisInst.topo.LC_Key != null)
                thisInst.btnViewModNLCD.BackColor = Color.MediumSeaGreen;
            else
            {
                thisInst.txt_LC_Key_selected.Text = "Not Selected";
                thisInst.txt_LC_Key_selected.BackColor = Color.LightCoral;
                thisInst.btnViewModNLCD.BackColor = Color.LightCoral;
                return;
            }

            if (isNLCD == true)
            {
                thisInst.txt_LC_Key_selected.Text = "US NLCD";
                thisInst.txt_LC_Key_selected.BackColor = Color.MediumSeaGreen;
            }
            else if (isNALC == true)
            {
                thisInst.txt_LC_Key_selected.Text = "North America LC";
                thisInst.txt_LC_Key_selected.BackColor = Color.MediumSeaGreen;
            }
            else if (isEULC == true)
            {
                thisInst.txt_LC_Key_selected.Text = "EU Corine LC";
                thisInst.txt_LC_Key_selected.BackColor = Color.MediumSeaGreen;
            }
            else
            {
                thisInst.txt_LC_Key_selected.Text = "User-defined";
                thisInst.txt_LC_Key_selected.BackColor = Color.MediumSeaGreen;
            }

        }

        /// <summary> Updates the met and turbine expo, SRDH and statistics shown in tables on Met and Turbine Summary tab </summary>        
        public void MetTurbSummaryAndStatsTable(Continuum thisInst)
        {
            thisInst.lstMetSummary.Items.Clear();
            thisInst.lstMetStats.Items.Clear();
            thisInst.lstTurbStats.Items.Clear();

            if (thisInst.metList.expoIsCalc == false) return;
            if (thisInst.metList.ThisCount == 0) return;
            int WD_Ind = thisInst.GetWD_ind("Summary");
            int numWD = thisInst.GetNumWD();
            int radiusInd = thisInst.GetRadiusInd("Summary");
            Met.TOD thisTOD = thisInst.GetSelectedTOD("Summary");
            Met.Season thisSeason = thisInst.GetSelectedSeason("Summary");

            if (WD_Ind > numWD) return;

            Met[] checkedMets = thisInst.GetCheckedMets("Summary");
            int metCount = checkedMets.Length;

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Summary");
            int turbCount = checkedTurbines.Length;

            for (int i = 0; i < checkedMets.Length; i++)
            {
                Met thisMet = checkedMets[i];
                Met.WSWD_Dist thisDist = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, thisTOD, thisSeason);

                objListItem = thisInst.lstMetSummary.Items.Add(thisMet.name);
                objListItem.SubItems.Add(Math.Round(thisMet.elev, 1).ToString());

                if (WD_Ind == numWD)
                    objListItem.SubItems.Add(Math.Round(thisDist.WS, 3).ToString());
                else
                    objListItem.SubItems.Add(Math.Round(thisDist.sectorWS_Ratio[WD_Ind] * thisDist.WS, 3).ToString());

                MetCollection.Weibull_params weibull = thisInst.metList.CalcWeibullParams(thisDist.WS_Dist, thisDist.sectorWS_Dist, thisDist.WS);

                if (WD_Ind == numWD)
                {
                    objListItem.SubItems.Add(Math.Round(weibull.overall_k, 3).ToString()); // weibull k
                    objListItem.SubItems.Add(Math.Round(weibull.overall_A, 3).ToString()); // weibull A

                    objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisDist.windRose, "Expo", "UW"), 3).ToString());
                    objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisDist.windRose, "Expo", "DW"), 3).ToString());

                    if (thisInst.metList.SRDH_IsCalc == true)
                    {
                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisDist.windRose, "SR", "UW"), 3).ToString());
                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisDist.windRose, "SR", "DW"), 3).ToString());

                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisDist.windRose, "DH", "UW"), 3).ToString());
                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisDist.windRose, "DH", "DW"), 3).ToString());
                    }
                }
                else
                {
                    objListItem.SubItems.Add(Math.Round(weibull.sector_k[WD_Ind], 3).ToString()); // weibull k
                    objListItem.SubItems.Add(Math.Round(weibull.sector_A[WD_Ind], 3).ToString()); // weibull C

                    objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].expo[WD_Ind], 3).ToString());
                    objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetDW_Param(WD_Ind, "Expo"), 3).ToString());

                    if (thisInst.metList.SRDH_IsCalc == true)
                    {
                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].SR[WD_Ind], 3).ToString());
                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetDW_Param(WD_Ind, "SR"), 3).ToString());

                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].dispH[WD_Ind], 3).ToString());
                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetDW_Param(WD_Ind, "DH"), 3).ToString());
                    }
                }
            }

            double[] Met_WS = new double[metCount];
            double[] Met_UW_Expos = new double[metCount];
            double[] Met_DW_Expos = new double[metCount];
            double[] Met_DW_min_UW_Expos = new double[metCount];
            double[] Met_Elevs = new double[metCount];
            double[] Met_UW_SRs = new double[metCount];
            double[] Met_DW_SRs = new double[metCount];
            double[] Met_UW_DHs = new double[metCount];
            double[] Met_DW_DHs = new double[metCount];

            // Find statistics of at Met sites
            for (int i = 0; i < metCount; i++)
            {
                Met thisMet = checkedMets[i];
                Met.WSWD_Dist thisDist = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, thisTOD, thisSeason);

                if (WD_Ind < numWD)
                {
                    // Sector statistics
                    Met_WS[i] = thisDist.WS * thisDist.sectorWS_Ratio[WD_Ind];
                    Met_UW_Expos[i] = checkedMets[i].expo[radiusInd].expo[WD_Ind];
                    Met_DW_Expos[i] = checkedMets[i].expo[radiusInd].GetDW_Param(WD_Ind, "Expo");
                    Met_DW_min_UW_Expos[i] = Met_DW_Expos[i] - checkedMets[i].expo[radiusInd].expo[WD_Ind];
                    Met_Elevs[i] = checkedMets[i].elev;

                    if (thisInst.topo.gotSR == true)
                    {
                        Met_UW_SRs[i] = checkedMets[i].expo[radiusInd].SR[WD_Ind];
                        Met_UW_DHs[i] = checkedMets[i].expo[radiusInd].dispH[WD_Ind];
                        Met_DW_SRs[i] = checkedMets[i].expo[radiusInd].GetDW_Param(WD_Ind, "SR");
                        Met_DW_DHs[i] = checkedMets[i].expo[radiusInd].GetDW_Param(WD_Ind, "DH");
                    }
                }
                else
                {
                    // Overall statistics
                    Met_WS[i] = thisDist.WS;
                    Met_UW_Expos[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisDist.windRose, "Expo", "UW");
                    Met_DW_Expos[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisDist.windRose, "Expo", "DW");
                    Met_DW_min_UW_Expos[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisDist.windRose, "Expo", "DW") - checkedMets[i].expo[radiusInd].GetOverallValue(thisDist.windRose, "Expo", "UW");
                    Met_Elevs[i] = checkedMets[i].elev;

                    if (thisInst.topo.gotSR == true)
                    {
                        Met_UW_SRs[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisDist.windRose, "SR", "UW");
                        Met_DW_SRs[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisDist.windRose, "SR", "DW");
                        Met_UW_DHs[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisDist.windRose, "DH", "UW");
                        Met_DW_DHs[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisDist.windRose, "DH", "DW");
                    }
                }
            }

            if (metCount > 0)
            {
                objListItem = thisInst.lstMetStats.Items.Add("WS");

                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Met_WS), 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Met_WS), 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Met_WS), 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Met_WS), 2).ToString());

                double thisMin = thisInst.topo.FindMin(Met_UW_Expos);
                double thisMax = thisInst.topo.FindMax(Met_UW_Expos);
                double thisAvg = thisInst.topo.FindAvg(Met_UW_Expos);
                double thisSD = thisInst.topo.FindSD(Met_UW_Expos);

                objListItem = thisInst.lstMetStats.Items.Add("UW Expo");
                objListItem.SubItems.Add(Math.Round(thisAvg, 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisSD, 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisMin, 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisMax, 2).ToString());

                objListItem = thisInst.lstMetStats.Items.Add("DW Expo");
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Met_DW_Expos), 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Met_DW_Expos), 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Met_DW_Expos), 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Met_DW_Expos), 2).ToString());

                objListItem = thisInst.lstMetStats.Items.Add("DW-UW");
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Met_DW_min_UW_Expos), 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Met_DW_min_UW_Expos), 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Met_DW_min_UW_Expos), 2).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Met_DW_min_UW_Expos), 2).ToString());

                objListItem = thisInst.lstMetStats.Items.Add("Elev");
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Met_Elevs), 1).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Met_Elevs), 1).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Met_Elevs), 1).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Met_Elevs), 1).ToString());


                if (thisInst.topo.gotSR == true)
                {
                    objListItem = thisInst.lstMetStats.Items.Add("UW SR");
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Met_UW_SRs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Met_UW_SRs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Met_UW_SRs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Met_UW_SRs), 2).ToString());

                    objListItem = thisInst.lstMetStats.Items.Add("DW SR");
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Met_DW_SRs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Met_DW_SRs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Met_DW_SRs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Met_DW_SRs), 2).ToString());

                    objListItem = thisInst.lstMetStats.Items.Add("UW DH");
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Met_UW_DHs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Met_UW_DHs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Met_UW_DHs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Met_UW_DHs), 2).ToString());

                    objListItem = thisInst.lstMetStats.Items.Add("DW DH");
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Met_DW_DHs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Met_DW_DHs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Met_DW_DHs), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Met_DW_DHs), 2).ToString());

                }
            }

            if (thisInst.turbineList.GotEst("WS", new TurbineCollection.PowerCurve(), null) == true && thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false)

            {
                for (int i = 0; i < turbCount; i++)
                {
                    Turbine thisTurb = checkedTurbines[i];
                    objListItem = thisInst.lstMetSummary.Items.Add(thisTurb.name);
                    objListItem.SubItems.Add(Math.Round(thisTurb.elev, 1).ToString());

                    if (thisTurb.AvgWSEst_Count == 0)
                        return;

                    Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(null);
                    Turbine.EstWS_Data estData = avgEst.freeStream;

                    if (WD_Ind == numWD)
                        objListItem.SubItems.Add(Math.Round(estData.WS, 3).ToString());
                    else
                        objListItem.SubItems.Add(Math.Round(estData.sectorWS[WD_Ind], 3).ToString());

                    if (WD_Ind == numWD)
                    {
                        objListItem.SubItems.Add(Math.Round(estData.weibullParams.overall_k, 3).ToString()); // weibull k
                        objListItem.SubItems.Add(Math.Round(estData.weibullParams.overall_A, 3).ToString()); // weibull A

                        objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(estData.windRose, "Expo", "UW"), 3).ToString());
                        objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(estData.windRose, "Expo", "DW"), 3).ToString());

                        if (thisInst.topo.gotSR == true)
                        {
                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(estData.windRose, "SR", "UW"), 3).ToString());
                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(estData.windRose, "SR", "DW"), 3).ToString());

                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(estData.windRose, "DH", "UW"), 3).ToString());
                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(estData.windRose, "DH", "DW"), 3).ToString());
                        }
                    }
                    else
                    {
                        objListItem.SubItems.Add(Math.Round(estData.weibullParams.sector_k[WD_Ind], 3).ToString()); // weibull k
                        objListItem.SubItems.Add(Math.Round(estData.weibullParams.sector_A[WD_Ind], 3).ToString()); // weibull C

                        objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].expo[WD_Ind], 3).ToString());
                        objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetDW_Param(WD_Ind, "Expo"), 3).ToString());

                        if (thisInst.topo.gotSR == true)
                        {
                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].SR[WD_Ind], 3).ToString());
                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetDW_Param(WD_Ind, "SR"), 3).ToString());
                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].dispH[WD_Ind], 3).ToString());
                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetDW_Param(WD_Ind, "DH"), 3).ToString());
                        }
                    }


                }

                double[] turbineWS = new double[turbCount];
                double[] Turb_UW_Expos = new double[turbCount];
                double[] Turb_DW_Expos = new double[turbCount];
                double[] Turb_DW_min_UW_Expos = new double[turbCount];
                double[] Turb_Elevs = new double[turbCount];
                double[] Turb_UW_SRs = new double[turbCount];
                double[] Turb_DW_SRs = new double[turbCount];
                double[] Turb_UW_DHs = new double[turbCount];
                double[] Turb_DW_DHs = new double[turbCount];

                // Find statistics of at Turbine sites
                for (int i = 0; i <= turbCount - 1; i++)
                {
                    Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(null);

                    if (WD_Ind < numWD)
                    {
                        // Sectorwise statistics
                        turbineWS[i] = avgEst.freeStream.sectorWS[WD_Ind];
                        Turb_UW_Expos[i] = checkedTurbines[i].expo[radiusInd].expo[WD_Ind];
                        Turb_DW_Expos[i] = checkedTurbines[i].expo[radiusInd].GetDW_Param(WD_Ind, "Expo");
                        Turb_DW_min_UW_Expos[i] = Turb_DW_Expos[i] - checkedTurbines[i].expo[radiusInd].expo[WD_Ind];
                        Turb_Elevs[i] = checkedTurbines[i].elev;

                        if (thisInst.topo.gotSR == true)
                        {
                            Turb_UW_SRs[i] = checkedTurbines[i].expo[radiusInd].SR[WD_Ind];
                            Turb_UW_DHs[i] = checkedTurbines[i].expo[radiusInd].dispH[WD_Ind];
                            Turb_DW_SRs[i] = checkedTurbines[i].expo[radiusInd].GetDW_Param(WD_Ind, "SR");
                            Turb_DW_DHs[i] = checkedTurbines[i].expo[radiusInd].GetDW_Param(WD_Ind, "DH");
                        }
                    }
                    else
                    {
                        // Overall statistics
                        turbineWS[i] = avgEst.freeStream.WS;
                        Turb_UW_Expos[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(avgEst.freeStream.windRose, "Expo", "UW");
                        Turb_DW_Expos[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(avgEst.freeStream.windRose, "Expo", "DW");
                        Turb_DW_min_UW_Expos[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(avgEst.freeStream.windRose, "Expo", "DW") - checkedTurbines[i].expo[radiusInd].GetOverallValue(avgEst.freeStream.windRose, "Expo", "UW");
                        Turb_Elevs[i] = checkedTurbines[i].elev;

                        if (thisInst.topo.gotSR == true)
                        {
                            Turb_UW_SRs[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(avgEst.freeStream.windRose, "SR", "UW");
                            Turb_DW_SRs[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(avgEst.freeStream.windRose, "SR", "DW");
                            Turb_UW_DHs[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(avgEst.freeStream.windRose, "DH", "UW");
                            Turb_DW_DHs[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(avgEst.freeStream.windRose, "DH", "DW");
                        }
                    }
                }

                if (turbCount > 0)
                {
                    objListItem = thisInst.lstTurbStats.Items.Add("WS");
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(turbineWS), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(turbineWS), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(turbineWS), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(turbineWS), 2).ToString());

                    objListItem = thisInst.lstTurbStats.Items.Add("UW Expo");
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Turb_UW_Expos), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Turb_UW_Expos), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Turb_UW_Expos), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Turb_UW_Expos), 2).ToString());

                    objListItem = thisInst.lstTurbStats.Items.Add("DW Expo");
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Turb_DW_Expos), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Turb_DW_Expos), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Turb_DW_Expos), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Turb_DW_Expos), 2).ToString());

                    objListItem = thisInst.lstTurbStats.Items.Add("DW-UW");
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Turb_DW_min_UW_Expos), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Turb_DW_min_UW_Expos), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Turb_DW_min_UW_Expos), 2).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Turb_DW_min_UW_Expos), 2).ToString());

                    objListItem = thisInst.lstTurbStats.Items.Add("Elev");
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Turb_Elevs), 1).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Turb_Elevs), 1).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Turb_Elevs), 1).ToString());
                    objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Turb_Elevs), 1).ToString());


                    if (thisInst.topo.gotSR == true)
                    {
                        objListItem = thisInst.lstTurbStats.Items.Add("UW SR");
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Turb_UW_SRs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Turb_UW_SRs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Turb_UW_SRs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Turb_UW_SRs), 2).ToString());

                        objListItem = thisInst.lstTurbStats.Items.Add("DW SR");
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Turb_DW_SRs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Turb_DW_SRs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Turb_DW_SRs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Turb_DW_SRs), 2).ToString());

                        objListItem = thisInst.lstTurbStats.Items.Add("UW DH");
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Turb_UW_DHs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Turb_UW_DHs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Turb_UW_DHs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Turb_UW_DHs), 2).ToString());

                        objListItem = thisInst.lstTurbStats.Items.Add("DW DH");
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindAvg(Turb_DW_DHs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindSD(Turb_DW_DHs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMin(Turb_DW_DHs), 2).ToString());
                        objListItem.SubItems.Add(Math.Round(thisInst.topo.FindMax(Turb_DW_DHs), 2).ToString());
                    }
                }
            }
        }

        /// <summary> Updates the table on Gross Turbines Ests tab showing gross turbine estimates. </summary> 
        public void GrossTurbEstList(Continuum thisInst)
        {
            thisInst.lstGrossTurbEst.Items.Clear();

            string powerCurve = "";
            bool AEP_calc = false;

            if (thisInst.metList.ThisCount == 0) return;
            int numWD = thisInst.GetNumWD();
            int WD_Ind = thisInst.GetWD_ind("Gross");
            if (WD_Ind > numWD || WD_Ind == -1)
                return;

            Met[] checkedMets = thisInst.GetCheckedMets("Gross");
            int checkedMetCount = checkedMets.Length;

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Gross");
            int checkedTurbCount = checkedTurbines.Length;

            if (thisInst.metList.ThisCount == 0) return;

            double metCF = 0;

            try
            {
                if (thisInst.cboPowerCrvs.SelectedItem.ToString() != "No Power Curves Imported")
                    AEP_calc = true;
                else
                    AEP_calc = false;

                powerCurve = thisInst.cboPowerCrvs.SelectedItem.ToString();
            }
            catch
            {
                if (thisInst.cboPowerCrvs.Items.Count > 0)
                {
                    thisInst.cboPowerCrvs.SelectedIndex = 0;
                    if (thisInst.cboPowerCrvs.SelectedItem.ToString() != "No Power Curves Imported")
                        AEP_calc = true;
                    else
                        AEP_calc = false;
                }
            }

            TurbineCollection.PowerCurve thisPowerCurve = new TurbineCollection.PowerCurve();

            for (int i = 0; i <= thisInst.turbineList.PowerCurveCount - 1; i++)
            {
                thisPowerCurve = thisInst.turbineList.powerCurves[i];
                if (thisPowerCurve.name == powerCurve)
                    break;
            }

            for (int i = 0; i <= checkedMetCount - 1; i++)
            {
                double metAEP = 0;
                Met.WSWD_Dist thisDist = checkedMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                MetCollection.Weibull_params weibull = thisInst.metList.CalcWeibullParams(thisDist.WS_Dist, thisDist.sectorWS_Dist, thisDist.WS);

                if (AEP_calc == true)
                {
                    if (WD_Ind == numWD)
                        metAEP = thisInst.turbineList.CalcAndReturnGrossAEP(thisDist.WS_Dist, thisInst.metList, powerCurve);
                    else
                    {
                        int numWS = thisDist.WS_Dist.Length;
                        double[] sectDist = new double[numWS];
                        for (int k = 0; k <= numWS - 1; k++)
                            sectDist[k] = thisDist.sectorWS_Dist[WD_Ind, k];

                        metAEP = thisInst.turbineList.CalcAndReturnGrossAEP(sectDist, thisInst.metList, powerCurve);
                        metAEP = metAEP * thisDist.windRose[WD_Ind];
                    }
                    metCF = thisInst.turbineList.CalcCapacityFactor(metAEP, thisPowerCurve.ratedPower);
                    if (WD_Ind != numWD) metCF = metCF * numWD;
                }

                objListItem = thisInst.lstGrossTurbEst.Items.Add(checkedMets[i].name);
                objListItem.SubItems.Add(Math.Round(checkedMets[i].elev, 1).ToString());
                if (WD_Ind == numWD)
                    objListItem.SubItems.Add(Math.Round(thisDist.WS, 3).ToString());
                else
                    objListItem.SubItems.Add(Math.Round(thisDist.sectorWS_Ratio[WD_Ind] * thisDist.WS, 3).ToString());

                if (metAEP != 0)
                    objListItem.SubItems.Add(Math.Round(metAEP, 0).ToString());
                else
                    objListItem.SubItems.Add("");

                if (metCF != 0)
                    objListItem.SubItems.Add(Math.Round(metCF, 3).ToString("P"));
                else
                    objListItem.SubItems.Add("");

                if (WD_Ind == numWD)
                {
                    objListItem.SubItems.Add(Math.Round(weibull.overall_k, 2).ToString()); // weibull k
                    objListItem.SubItems.Add(Math.Round(weibull.overall_A, 2).ToString()); // weibull C
                }
                else
                {
                    objListItem.SubItems.Add(Math.Round(weibull.sector_k[WD_Ind], 2).ToString()); // weibull k
                    objListItem.SubItems.Add(Math.Round(weibull.sector_A[WD_Ind], 2).ToString()); // weibull C
                }
            }

            if (thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false)
            {
                double turbAEP = 0;

                for (int i = 0; i < checkedTurbCount; i++)
                {
                    Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(null); // Just want FS WS

                    if (avgEst.freeStream.WS != 0)
                    {
                        objListItem = thisInst.lstGrossTurbEst.Items.Add(checkedTurbines[i].name);
                        objListItem.SubItems.Add(Math.Round(checkedTurbines[i].elev, 1).ToString());
                        if (WD_Ind == numWD)
                            objListItem.SubItems.Add(Math.Round(avgEst.freeStream.WS, 3).ToString());
                        else
                            objListItem.SubItems.Add(Math.Round(avgEst.freeStream.sectorWS[WD_Ind], 3).ToString());

                        if (AEP_calc == true && WD_Ind == numWD)
                            turbAEP = checkedTurbines[i].GetGrossAEP(powerCurve, WD_Ind);
                        else if (AEP_calc == true && WD_Ind < numWD)
                        {
                            int numWS = avgEst.freeStream.WS_Dist.Length;
                            double[] sectDist = new double[numWS];

                            for (int k = 0; k < numWS; k++)
                                sectDist[k] = avgEst.freeStream.sectorWS_Dist[WD_Ind, k];

                            turbAEP = thisInst.turbineList.CalcAndReturnGrossAEP(sectDist, thisInst.metList, powerCurve);
                            turbAEP = turbAEP * avgEst.freeStream.windRose[WD_Ind];
                        }

                        if (turbAEP != 0)
                        {
                            objListItem.SubItems.Add(Math.Round(turbAEP, 0).ToString());
                            double turbCF = thisInst.turbineList.CalcCapacityFactor(turbAEP, thisPowerCurve.ratedPower);
                            if (WD_Ind != numWD) turbCF = turbCF * numWD;
                            objListItem.SubItems.Add(Math.Round(turbCF, 4).ToString("P"));
                        }
                        else
                        {
                            objListItem.SubItems.Add("");
                            objListItem.SubItems.Add("");
                        }

                        if (WD_Ind == numWD)
                        {
                            objListItem.SubItems.Add(Math.Round(avgEst.freeStream.weibullParams.overall_k, 2).ToString());
                            objListItem.SubItems.Add(Math.Round(avgEst.freeStream.weibullParams.overall_A, 2).ToString());
                        }
                        else
                        {
                            objListItem.SubItems.Add(Math.Round(avgEst.freeStream.weibullParams.sector_k[WD_Ind], 2).ToString());
                            objListItem.SubItems.Add(Math.Round(avgEst.freeStream.weibullParams.sector_A[WD_Ind], 2).ToString());
                        }
                    }

                }
            }
        }

        /// <summary> Updates the table of turbine uncertainty estimates on Uncertainty tab. </summary>        
        public void TurbUncertEstList(Continuum thisInst)
        {
            thisInst.lstTurbUncert.Items.Clear();
            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Uncertainty");
            int turbCount = thisInst.turbineList.TurbineCount;

            if (thisInst.turbineList.GotEst("WS", powerCurve, null) == true && thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false)
            {
                for (int i = 0; i < turbCount; i++) // Now repopulate it with turbines and modeled parameters
                {
                    Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                    Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(null); // just want the WS

                    objListItem = thisInst.lstTurbUncert.Items.Add((i + 1).ToString());
                    objListItem.SubItems.Add(thisTurb.name);

                    if (thisTurb.avgWS_Est != null)
                    {
                        objListItem.SubItems.Add(Math.Round(avgEst.freeStream.WS, 3).ToString());
                        objListItem.SubItems.Add(Math.Round(avgEst.uncert, 5).ToString("P"));
                        objListItem.SubItems.Add(Math.Round(avgEst.freeStream.WS - avgEst.freeStream.WS * avgEst.uncert * 1.28155, 3).ToString());
                        objListItem.SubItems.Add(Math.Round(avgEst.freeStream.WS - avgEst.freeStream.WS * avgEst.uncert * 2.326, 3).ToString());
                    }
                    if (thisTurb.grossAEP != null && powerCurve.name != "")
                    {
                        Turbine.Gross_Energy_Est grossEst = thisTurb.GetGrossEnergyEst(powerCurve);

                        objListItem.SubItems.Add(Math.Round(grossEst.AEP, 0).ToString());
                        objListItem.SubItems.Add(Math.Round(grossEst.P90, 0).ToString());
                        objListItem.SubItems.Add(Math.Round(grossEst.P99, 0).ToString());
                    }
                }
            }
        }

        /// <summary> Updates the list of turbines on Gross Est tab and dropdown lists on Time Series tab, Uncertainty tab and Wake Model generator. </summary>
        public void PowerCurveList(Continuum thisInst)
        {
            thisInst.okToUpdate = false;

            thisInst.cboPowerCrvs.Items.Clear();
            thisInst.cboPowerCrvs.Text = "";
            thisInst.cboUncertPowerCrv.Items.Clear();
            thisInst.lstPowerCurveList.Items.Clear();
            thisInst.cboMERRA_PowerCurves.Items.Clear();
            thisInst.cboSiteSuitPowerCurve.Items.Clear();
            thisInst.cboMonthlyPowerCurve.Items.Clear();
            thisInst.cboTurbPowerCurve.Items.Clear();

            if (thisInst.turbineList.PowerCurveCount > 0)
            {
                for (int i = 0; i < thisInst.turbineList.PowerCurveCount; i++)
                {
                    thisInst.cboPowerCrvs.Items.Add(thisInst.turbineList.powerCurves[i].name);
                    thisInst.cboUncertPowerCrv.Items.Add(thisInst.turbineList.powerCurves[i].name);

                    ListViewItem objlist = thisInst.lstPowerCurveList.Items.Add(thisInst.turbineList.powerCurves[i].name);
                    objlist.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.powerCurves[i].RD, 0)));
                    objlist.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.powerCurves[i].ratedRPM, 1)));

                    thisInst.cboMERRA_PowerCurves.Items.Add(thisInst.turbineList.powerCurves[i].name);
                    thisInst.cboSiteSuitPowerCurve.Items.Add(thisInst.turbineList.powerCurves[i].name);
                    thisInst.cboMonthlyPowerCurve.Items.Add(thisInst.turbineList.powerCurves[i].name);
                    thisInst.cboTurbPowerCurve.Items.Add(thisInst.turbineList.powerCurves[i].name);
                }

                thisInst.lstPowerCurveList.Items[thisInst.lstPowerCurveList.Items.Count - 1].Checked = true;
                thisInst.lstPowerCurveList.Items[thisInst.lstPowerCurveList.Items.Count - 1].Selected = true;

            }
            else
            {
                thisInst.cboPowerCrvs.Items.Add("No Power Curves Imported");
                thisInst.cboMonthlyPowerCurve.Items.Add("No Power Curves Imported");
                thisInst.cboMERRA_PowerCurves.Items.Add("No Power Curves Imported");
                thisInst.cboUncertPowerCrv.Items.Add("No Power Curves Imported");
                thisInst.cboTurbPowerCurve.Items.Add("No Power Curves Imported");
                thisInst.cboSiteSuitPowerCurve.Items.Add("No Power Curves Imported");
            }

            thisInst.cboPowerCrvs.SelectedIndex = 0;
            thisInst.cboUncertPowerCrv.SelectedIndex = 0;
            thisInst.cboMERRA_PowerCurves.SelectedIndex = 0;
            thisInst.cboMERRA_PowerCurves.Text = "";
            thisInst.cboSiteSuitPowerCurve.SelectedIndex = 0;
            thisInst.cboMonthlyPowerCurve.SelectedIndex = 0;
            thisInst.cboTurbPowerCurve.SelectedIndex = 0;

            thisInst.okToUpdate = true;
        }

        /// <summary> Reads turbines that are checked on gross tab and then calls function to calculate stats and fill textboxes on gross tab. </summary>
        public void TurbStats(Continuum thisInst)
        {
            if (thisInst.turbineList.GotEst("WS", new TurbineCollection.PowerCurve(), null) == false)
            {
                ClearStats(thisInst);
                return;
            }

            int WD_Ind = thisInst.GetWD_ind("Gross");
            if (thisInst.metList.ThisCount == 0) return;
            int numWD = thisInst.GetNumWD();

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Gross");
            if (checkedTurbines == null)
            {
                ClearStats(thisInst);
                return;
            }
            thisInst.turbineList.FindParamStats(thisInst, checkedTurbines, WD_Ind);
        }

        /// <summary> Creates a histogram of specified (gross) parameter (elev, WS, AEP, weibull k, weibull A) of selected met and turbines on Gross Turb Ests tab. </summary>
        public void GrossHistogram(Continuum thisInst)
        {
            thisInst.lstGrossHisto.Items.Clear();
            thisInst.lstGrossHisto.Columns.Clear();

            // Get selected parameter            
            MetCollection metList = thisInst.metList;
            string selectParam = thisInst.cboGrossParam.SelectedItem.ToString();

            Met[] checkedMets = thisInst.GetCheckedMets("Gross");
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Gross");

            int checkedMetCount = checkedMets.Length;
            int checkedTurbCount = checkedTurbines.Length;

            thisInst.plotGrossHisto.Model = new PlotModel();
            var model = thisInst.plotGrossHisto.Model;
            model.IsLegendVisible = false;

            if (metList.ThisCount == 0 || (checkedMetCount == 0 && checkedTurbCount == 0))
            {
                thisInst.plotGrossHisto.Refresh();
                return;
            }

            // Specify axes
            CategoryAxis xAxis = new CategoryAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = selectParam;
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Met Count";
            yAxis.Key = "Met";
            LinearAxis secYAxis = new LinearAxis();
            secYAxis.Position = AxisPosition.Right;
            secYAxis.Title = "Turbine Count";
            secYAxis.Key = "Turbine";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);
            model.Axes.Add(secYAxis);

            if (thisInst.metList.ThisCount == 0) return;
            int WD_Ind = thisInst.GetWD_ind("Gross");
            int numWD = thisInst.GetNumWD();

            TurbineCollection turbineList = thisInst.turbineList;
            bool AEP_calc = false;
            string powerCurve = "";

            double[] sectDist;
            int numWS;

            try
            {
                if (thisInst.cboPowerCrvs.SelectedItem.ToString() != "No Power Curves Imported")
                    AEP_calc = true;
                else
                    AEP_calc = false;

                powerCurve = thisInst.cboPowerCrvs.SelectedItem.ToString();
            }
            catch
            {
                if (thisInst.cboPowerCrvs.Items.Count > 0)
                {
                    thisInst.cboPowerCrvs.SelectedIndex = 0;
                    if (thisInst.cboPowerCrvs.SelectedItem.ToString() != "No Power Curves Imported")
                        AEP_calc = true;
                    else
                        AEP_calc = false;
                }
            }

            if (AEP_calc == false && selectParam == "Gross AEP") return;

            // Get values at met sites
            double[] metVals = new double[checkedMetCount];

            for (int i = 0; i <= checkedMetCount - 1; i++)
            {
                Met.WSWD_Dist thisDist = checkedMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                if (selectParam == "Wind Speed")
                {
                    if (WD_Ind == numWD)
                        metVals[i] = thisDist.WS;
                    else
                        metVals[i] = thisDist.WS * thisDist.sectorWS_Ratio[WD_Ind];
                }
                else if (selectParam == "Gross AEP")
                {
                    if (WD_Ind == numWD)
                    {
                        double metAEP = turbineList.CalcAndReturnGrossAEP(thisDist.WS_Dist, thisInst.metList, powerCurve);
                        metVals[i] = metAEP;
                    }
                    else
                    {
                        numWS = thisDist.WS_Dist.Length;
                        sectDist = new double[numWS];
                        for (int k = 0; k <= numWS - 1; k++)
                            sectDist[k] = thisDist.sectorWS_Dist[WD_Ind, k];

                        double metAEP = turbineList.CalcAndReturnGrossAEP(sectDist, thisInst.metList, powerCurve);
                        metAEP = metAEP * thisDist.windRose[WD_Ind];
                        metVals[i] = metAEP;
                    }
                }
                else if (selectParam == "Elevation")
                    metVals[i] = checkedMets[i].elev;
                else if (selectParam == "Weibull k")
                {
                    MetCollection.Weibull_params weibull = metList.CalcWeibullParams(thisDist.WS_Dist, thisDist.sectorWS_Dist, thisDist.WS);
                    if (WD_Ind == numWD)
                        metVals[i] = weibull.overall_k;
                    else
                        metVals[i] = weibull.sector_k[WD_Ind];
                }
                else if (selectParam == "Weibull A")
                {
                    MetCollection.Weibull_params weibull = metList.CalcWeibullParams(thisDist.WS_Dist, thisDist.sectorWS_Dist, thisDist.WS);
                    if (WD_Ind == numWD)
                        metVals[i] = weibull.overall_A;
                    else
                        metVals[i] = weibull.sector_A[WD_Ind];
                }
            }

            double[] turbVals = new double[checkedTurbCount];

            if (thisInst.turbineList.turbineCalcsDone)
            {
                for (int i = 0; i <= checkedTurbCount - 1; i++)
                {
                    Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(null);

                    // Get values at turbine sites
                    if (avgEst.freeStream.WS != 0)
                    {
                        if (selectParam == "Wind Speed")
                        {
                            if (WD_Ind == numWD)
                                turbVals[i] = avgEst.freeStream.WS;
                            else
                                turbVals[i] = avgEst.freeStream.sectorWS[WD_Ind];
                        }
                        else if (selectParam == "Gross AEP")
                            turbVals[i] = checkedTurbines[i].GetGrossAEP(powerCurve, WD_Ind);
                        else if (selectParam == "Elevation")
                            turbVals[i] = checkedTurbines[i].elev;
                        else if (selectParam == "Weibull k")
                        {
                            if (WD_Ind == numWD)
                                turbVals[i] = avgEst.freeStream.weibullParams.overall_k;
                            else
                                turbVals[i] = avgEst.freeStream.weibullParams.sector_k[WD_Ind];
                        }
                        else if (selectParam == "Weibull A")
                        {
                            if (WD_Ind == numWD)
                                turbVals[i] = avgEst.freeStream.weibullParams.overall_A;
                            else
                                turbVals[i] = avgEst.freeStream.weibullParams.sector_A[WD_Ind];
                        }
                    }
                }
            }
            else
            {
                checkedTurbCount = 0;

                if (checkedMetCount == 0)
                {
                    thisInst.plotGrossHisto.Refresh();
                    return;
                }
            }

            // Find min / max and set interval bins
            int histoSize = 1;
            double histoMin = 1000000;
            double histoMax = 0;
            double histoInt = 1;
            double[] histoVals;

            for (int i = 0; i <= checkedMetCount - 1; i++)
            {
                if (metVals[i] < histoMin)
                    histoMin = metVals[i];

                if (metVals[i] > histoMax)
                    histoMax = metVals[i];
            }

            for (int i = 0; i <= checkedTurbCount - 1; i++)
            {
                if (turbVals[i] < histoMin && turbVals[i] != 0)
                    histoMin = turbVals[i];

                if (turbVals[i] > histoMax)
                    histoMax = turbVals[i];
            }

            histoMin = histoMin - histoMin * 0.02f;
            histoMax = histoMax + histoMax * 0.02f;

            if (selectParam == "Wind Speed" || selectParam == "Weibull k" || selectParam == "Weibull A")
            {
                histoMin = Math.Round(histoMin, 1);
                histoMax = Math.Round(histoMax, 1);
                histoInt = 0.05f;
                histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                while (histoSize > 20)
                {
                    histoInt = histoInt + 0.05f;
                    histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                }
            }
            else if (selectParam == "Elevation")
            {
                histoMin = Math.Round(histoMin, 0);
                histoMax = Math.Round(histoMax, 0);
                histoInt = 1;
                histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                while (histoSize > 20)
                {
                    histoInt = histoInt + 1;
                    histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                }
            }
            else if (selectParam == "Gross AEP")
            {
                histoMin = histoMin / 100;
                histoMin = Math.Round(histoMin, 0);
                histoMin = histoMin * 100;

                histoMax = histoMax / 100;
                histoMax = Math.Round(histoMax, 0);
                histoMax = histoMax * 100;

                histoInt = 100;
                histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                while (histoSize > 20)
                {
                    histoInt = histoInt + 100;
                    histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                }
            }

            ColumnSeries metHisto = new ColumnSeries();
            metHisto.Title = "Met sites";
            metHisto.YAxisKey = "Met";
            model.Series.Add(metHisto);

            ColumnSeries turbineHisto = new ColumnSeries();
            turbineHisto.Title = "Turbine sites";
            turbineHisto.YAxisKey = "Turbine";
            model.Series.Add(turbineHisto);

            histoVals = new double[histoSize];
            int[] metHistoVals = new int[histoSize];
            int[] turbHistoVals = new int[histoSize];

            for (int i = 0; i < checkedMetCount; i++)
            {
                for (int j = 0; j < histoSize; j++)
                {
                    histoVals[j] = Math.Round(histoMin + j * histoInt, 2);
                    if (j == 0 && metVals[i] < histoVals[j])
                        metHistoVals[j]++;
                    else if (j > 0)
                    {
                        if (metVals[i] > histoVals[j - 1] && metVals[i] <= histoVals[j])
                            metHistoVals[j] = metHistoVals[j] + 1;
                    }
                    else if (j == histoSize - 1 && metVals[i] > histoVals[j])  // More than max value in last bin
                        metHistoVals[histoSize]++;
                }
            }

            for (int i = 0; i < checkedTurbCount; i++)
            {
                for (int j = 0; j < histoSize; j++)
                {
                    histoVals[j] = Math.Round(histoMin + j * histoInt, 2);
                    if (j == 0 && turbVals[i] < histoVals[j])
                        turbHistoVals[j]++;
                    else if (j > 0)
                    {
                        if (turbVals[i] > histoVals[j - 1] && turbVals[i] <= histoVals[j])
                            turbHistoVals[j]++;
                    }
                    else if (j == histoSize - 1 && turbVals[i] > histoVals[j])  // More than max value in last bin
                        turbHistoVals[histoSize]++;
                }
            }

            List<double> histoLabels = new List<double>();
            for (int i = 0; i < histoSize; i++)
                histoLabels.Add(histoVals[i]);

            xAxis.ItemsSource = histoLabels;

            for (int i = 0; i < histoSize; i++)
            {
                metHisto.Items.Add(new ColumnItem { Value = metHistoVals[i] });

                if (thisInst.turbineList.turbineCalcsDone)
                    turbineHisto.Items.Add(new ColumnItem { Value = turbHistoVals[i] });
            }

            if (selectParam == "Wind Speed")
            {
                xAxis.Title = "Wind Speed, m/s";
                model.Title = "Histogram of Met and Turbine " + thisInst.modeledHeight + " m Wind Speeds";
            }
            else if (selectParam == "Gross AEP")
            {
                xAxis.Title = "Gross AEP, MWh";
                model.Title = "Histogram of Met and Turbine Gross AEP";
            }
            else if (selectParam == "Elevation")
            {
                xAxis.Title = "Elevation, m";
                model.Title = "Histogram of Met and Turbine Elevations";
            }
            else if (selectParam == "Weibull k")
            {
                xAxis.Title = "Weibull k";
                model.Title = "Histogram of Met and Turbine " + thisInst.modeledHeight + " m Weibull k";
            }
            else if (selectParam == "Weibull A")
            {
                xAxis.Title = "Weibull A";
                model.Title = "Histogram of Met and Turbine " + thisInst.modeledHeight + " m Weibull A";
            }

            // Fill list with max value and met and turbine counts
            if (selectParam == "Wind Speed")
                thisInst.lstGrossHisto.Columns.Add("WS");
            else if (selectParam == "Gross AEP")
                thisInst.lstGrossHisto.Columns.Add("AEP");
            else if (selectParam == "Elevation")
                thisInst.lstGrossHisto.Columns.Add("Elev");
            else if (selectParam == "Weibull k")
                thisInst.lstGrossHisto.Columns.Add("k");
            else if (selectParam == "Weibull A")
                thisInst.lstGrossHisto.Columns.Add("A");

            thisInst.lstGrossHisto.Columns.Add("Mets");
            thisInst.lstGrossHisto.Columns.Add("Turbs");

            thisInst.lstGrossHisto.Columns[0].Width = 60;
            thisInst.lstGrossHisto.Columns[1].Width = 45;
            thisInst.lstGrossHisto.Columns[2].Width = 45;

            for (int i = 0; i < histoSize; i++)
            {
                objListItem = thisInst.lstGrossHisto.Items.Add(histoVals[i].ToString());
                objListItem.SubItems.Add(metHistoVals[i].ToString());
                objListItem.SubItems.Add(turbHistoVals[i].ToString());
            }

            thisInst.plotGrossHisto.Refresh();

        }

        /// <summary> Creates a histogram of Round Robin errors on Uncertainty tab. </summary>        
        public void RoundRobinHistogram(Continuum thisInst)
        {
            thisInst.plotRR_Histo.Model = new PlotModel();
            var model = thisInst.plotRR_Histo.Model;
            model.IsLegendVisible = false;
            model.Title = "Histogram of Round Robin Estimates";

            if (thisInst.metPairList.RoundRobinCount > 0 && thisInst.cboRoundRobin.SelectedItem != null)
            {
                string dropText = thisInst.cboRoundRobin.SelectedItem.ToString();
                string[] textSplit = dropText.Split(Convert.ToChar(" "));
                int numMetsUsed = Convert.ToInt16(textSplit[1]);

                MetPairCollection.RR_WS_Ests thisRR = new MetPairCollection.RR_WS_Ests();
                int numMets = thisInst.metList.ThisCount;
                string[] metsUsed = new string[numMets];

                for (int i = 0; i <= numMets - 1; i++)
                    metsUsed[i] = thisInst.metList.metItem[i].name;

                for (int i = 0; i <= thisInst.metPairList.RoundRobinCount - 1; i++)
                {
                    thisRR = thisInst.metPairList.roundRobinEsts[i];
                    bool sameMets = thisInst.metList.sameMets(metsUsed, thisRR.metsUsed);

                    if (sameMets == true && thisRR.metSubSize == numMetsUsed)
                        break;
                }

                int numErrs = (thisRR.avgWS_Ests.GetUpperBound(0) + 1) * (thisRR.avgWS_Ests.GetUpperBound(1) + 1);
                // Find min / max and set interval bins
                double histoMin = 1000000;
                double histoMax = 0;

                // Get values at met sites
                double[] Err_vals = new double[numErrs];
                int Err_ind = 0;

                for (int i = 0; i <= thisRR.avgWS_Ests.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= thisRR.avgWS_Ests.GetUpperBound(1); j++)
                    {
                        Err_vals[Err_ind] = thisRR.avgWS_Ests[i, j].estError;
                        Err_ind++;
                    }
                }

                for (int i = 0; i <= numErrs - 1; i++)
                {
                    if (Err_vals[i] < histoMin)
                        histoMin = Err_vals[i];

                    if (Err_vals[i] > histoMax)
                        histoMax = Err_vals[i];
                }

                histoMin = histoMin - histoMin * 0.1f;
                histoMax = histoMax + histoMax * 0.1f;

                double histoInt = 0.001f;
                int histoSize = Convert.ToInt16(Math.Round((histoMax - histoMin) / histoInt, 0)) + 1;

                while (histoSize > 20)
                {
                    histoInt = histoInt + 0.0005f;
                    histoSize = 1 + (int)((histoMax - histoMin) / histoInt);
                }

                // Specify axes
                CategoryAxis xAxis = new CategoryAxis();
                xAxis.Position = AxisPosition.Bottom;
                xAxis.Title = "WS Est Error, %";
                LinearAxis yAxis = new LinearAxis();
                yAxis.Position = AxisPosition.Left;
                yAxis.Title = "Data Count";

                model.Axes.Add(xAxis);
                model.Axes.Add(yAxis);

                ColumnSeries histoSeries = new ColumnSeries();
                histoSeries.FillColor = OxyColors.Red;
                model.Series.Add(histoSeries);

                List<double> histoLabels = new List<double>();
                double[] histoVals = new double[histoSize];

                for (int i = 0; i < histoSize; i++)
                {
                    histoVals[i] = Math.Round(histoMin + i * histoInt, 3);
                    histoLabels.Add(histoVals[i] * 100);
                }

                xAxis.ItemsSource = histoLabels;
                xAxis.MajorStep = 2;

                int[] errHistoVals = new int[histoSize];

                for (int i = 0; i < numErrs; i++)
                {
                    for (int j = 0; j < histoSize; j++)
                    {
                        if (j == 0 && Err_vals[i] <= histoVals[j])
                            errHistoVals[j]++;
                        else if (j > 0)
                        {
                            if (Err_vals[i] > histoVals[j - 1] && Err_vals[i] <= histoVals[j])
                                errHistoVals[j]++;
                        }
                        else if (j == histoSize - 1 && Err_vals[i] < histoVals[j])  // Last bin
                            errHistoVals[histoSize - 1] = errHistoVals[histoSize] + 1;
                    }
                }

                for (int i = 0; i < histoSize; i++)
                    histoSeries.Items.Add(new ColumnItem { Value = errHistoVals[i] });
            }

            thisInst.plotRR_Histo.Refresh();

        }

        /// <summary> Updates all plots and tables on all tabs that have the turbine sites to reflect new list. </summary>        
        public void TurbineList(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            int turbCount = thisInst.turbineList.TurbineCount;
            thisInst.lstTurbines.Items.Clear(); // First clear table
            thisInst.chkTurbLabels.Items.Clear(); // Clear table of turbine labels
            thisInst.chkTurbLabelStep.Items.Clear(); // Clear table of turbine labels
            thisInst.chkTurbLabels_Maps.Items.Clear(); // Clear table of turbine labels
            thisInst.chkTurbSumm.Items.Clear(); // Clear table of turbines in met & turbine est tab
            thisInst.chkTurbGross.Items.Clear(); // Clear table of turbines in turbine est tab
            thisInst.chkTurbNet.Items.Clear(); // net est tab
            thisInst.chkStrings.Items.Clear(); // list of turbine strings
            thisInst.cboSelectedTurbine.Items.Clear(); // Dropdown of turbine sites on Monthly analysis tab
            thisInst.cboExceedTurbine.Items.Clear(); // Dropdown of turbine sites on Exceedance tab
            thisInst.cboTurbineTI.Items.Clear(); // Dropdown of turbine sites under Turbulence Intensity on Site Condition tab
            thisInst.cboInflowTurbine.Items.Clear(); // Dropdown of turbine sites under Inflow Angle on Site Conditions tab

            string[] stringNames = null;
            int numStrings = 0;

            for (int i = 0; i < turbCount; i++)
            {
                Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                objListItem = thisInst.lstTurbines.Items.Add(thisTurb.name);
                objListItem.SubItems.Add(Math.Round(thisTurb.UTMX, 0).ToString());
                objListItem.SubItems.Add(Math.Round(thisTurb.UTMY, 0).ToString());

                thisInst.chkTurbLabels.Items.Add(thisTurb.name, true);
                thisInst.chkTurbLabels_Maps.Items.Add(thisTurb.name, true);
                thisInst.chkTurbLabelStep.Items.Add(thisTurb.name, true);
                thisInst.chkTurbGross.Items.Add(thisTurb.name, true);
                thisInst.chkTurbNet.Items.Add(thisTurb.name, true);
                thisInst.chkTurbSumm.Items.Add(thisTurb.name, true);
                thisInst.cboSelectedTurbine.Items.Add(thisTurb.name);
                thisInst.cboExceedTurbine.Items.Add(thisTurb.name);
                thisInst.cboTurbineTI.Items.Add(thisTurb.name);
                thisInst.cboInflowTurbine.Items.Add(thisTurb.name);

                if (numStrings > 0)
                {
                    bool haveString = false;
                    for (int j = 0; j < numStrings; j++)
                    {
                        if (thisTurb.stringNum == Convert.ToInt16(stringNames[j]))
                        {
                            haveString = true;
                            break;
                        }
                    }

                    if (haveString == false)
                    {
                        Array.Resize(ref stringNames, numStrings + 1);
                        stringNames[numStrings] = thisTurb.stringNum.ToString();
                        numStrings++;
                    }
                }
                else
                {
                    stringNames = new string[numStrings + 1];
                    stringNames[numStrings] = thisTurb.stringNum.ToString();
                    numStrings++;
                }
            }

            if (turbCount > 0)
            {
                thisInst.cboSelectedTurbine.SelectedIndex = 0;
                thisInst.cboExceedTurbine.SelectedIndex = 0;
                thisInst.cboTurbineTI.SelectedIndex = 0;
                thisInst.cboInflowTurbine.SelectedIndex = 0;
            }
            else
            {
                thisInst.cboSelectedTurbine.Text = "";
                thisInst.cboExceedTurbine.Text = "";
                thisInst.cboTurbineTI.Text = "";
                thisInst.cboInflowTurbine.Text = "";
            }

            for (int i = 0; i < numStrings; i++)
                thisInst.chkStrings.Items.Add(stringNames[i], true);

            thisInst.okToUpdate = true;

        }

        /// <summary> Update the plots and tables on Met and Turbine Summary tab. </summary>        
        public void Met_Turbine_Summary_TAB(Continuum thisInst)
        {
            MetAndTurbExpoPlots(thisInst);
            MetTurbSummaryAndStatsTable(thisInst);
        }

        /// <summary> Calls thisExpoPlot to update each of the 8 plots on the 'Met and Turbine Summary' tab. </summary> 
        public void MetAndTurbExpoPlots(Continuum thisInst) // 
        {
            int radiusIndex = thisInst.GetRadiusInd("Summary");
            int WD_Ind = thisInst.GetWD_ind("Summary");
            thisExpoPlot(thisInst, "UW Expo", radiusIndex, WD_Ind); // UW Expo plot
            thisExpoPlot(thisInst, "DW Expo", radiusIndex, WD_Ind); // DW Expo plot
            thisExpoPlot(thisInst, "DW-UW Expo", radiusIndex, WD_Ind); // DW-UW Expo plot
            thisExpoPlot(thisInst, "Elev", radiusIndex, WD_Ind); // Elevation plot

            thisExpoPlot(thisInst, "UW SR", radiusIndex, WD_Ind); // UW Surface roughness plot
            thisExpoPlot(thisInst, "DW SR", radiusIndex, WD_Ind);// DW Surface roughness plot
            thisExpoPlot(thisInst, "UW DH", radiusIndex, WD_Ind);// UW Displacement height plot
            thisExpoPlot(thisInst, "DW DH", radiusIndex, WD_Ind);// DW Displacement height plot

        }

        /// <summary> Updates plot on 'Met and Turbine Summary' tab based on plotName. </summary>        
        public void thisExpoPlot(Continuum thisInst, string plotName, int radiusIndex, int WD_Ind)
        {
            OxyPlot.WindowsForms.PlotView thisPlot = new OxyPlot.WindowsForms.PlotView();
            var model = new PlotModel(); ;

            if (plotName == "UW Expo")
            {
                thisPlot = thisInst.plotUWExpo;
                thisInst.plotUWExpo.Model = new PlotModel();
                model = thisInst.plotUWExpo.Model;
            }
            else if (plotName == "DW Expo")
            {
                thisPlot = thisInst.plotDWExpo;
                thisInst.plotDWExpo.Model = new PlotModel();
                model = thisInst.plotDWExpo.Model;
            }
            else if (plotName == "DW-UW Expo")
            {
                thisPlot = thisInst.plotDWUWExpo;
                thisInst.plotDWUWExpo.Model = new PlotModel();
                model = thisInst.plotDWUWExpo.Model;
            }
            else if (plotName == "Elev")
            {
                thisPlot = thisInst.plotElev;
                thisInst.plotElev.Model = new PlotModel();
                model = thisInst.plotElev.Model;
            }
            else if (plotName == "UW SR")
            {
                thisPlot = thisInst.plotUW_SR;
                thisInst.plotUW_SR.Model = new PlotModel();
                model = thisInst.plotUW_SR.Model;
            }
            else if (plotName == "DW SR")
            {
                thisPlot = thisInst.plotUW_SR;
                thisInst.plotDW_SR.Model = new PlotModel();
                model = thisInst.plotDW_SR.Model;
            }
            else if (plotName == "UW DH")
            {
                thisPlot = thisInst.plotUW_DH;
                thisInst.plotUW_DH.Model = new PlotModel();
                model = thisInst.plotUW_DH.Model;
            }
            else if (plotName == "DW DH")
            {
                thisPlot = thisInst.plotDW_DH;
                thisInst.plotDW_DH.Model = new PlotModel();
                model = thisInst.plotDW_DH.Model;
            }

            if ((plotName == "UW SR" || plotName == "DW SR" || plotName == "UW DH" || plotName == "DW DH") && thisInst.topo.gotSR == false)
            {
                thisPlot.Refresh();
                return;
            }

            if (thisInst.metList.ThisCount == 0)
            {
                thisPlot.Refresh();
                return;
            }

            model.IsLegendVisible = false;
            model.Title = GetExpoSR_ChartTitle(plotName, Convert.ToInt16(thisInst.modeledHeight));

            int numWD = thisInst.metList.numWD;
            Met.TOD thisTOD = thisInst.GetSelectedTOD("Summary");
            Met.Season thisSeason = thisInst.GetSelectedSeason("Summary");

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Summary");
            Met[] checkedMets = thisInst.GetCheckedMets("Summary");

            bool plotTurbs = false;
            if (thisInst.turbineList.GotEst("WS", new TurbineCollection.PowerCurve(), null) == true && thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false)
                plotTurbs = true;

            if (checkedMets == null && checkedTurbines == null)
            {
                thisPlot.Refresh();
                return;
            }

            if (checkedMets.Length == 0 && checkedTurbines.Length == 0)
            {
                thisPlot.Refresh();
                return;
            }

            double minXValue = 10000;
            double maxXValue = -10000;
            double minWS = 100;
            double maxWS = -100;

            if (checkedMets != null)
            {
                for (int i = 0; i < checkedMets.Length; i++)
                {
                    // Create label series
                    var metDataSeries = new ScatterSeries();
                    metDataSeries.MarkerSize = 5;
                    metDataSeries.Title = checkedMets[i].name;
                    metDataSeries.MarkerFill = OxyColors.Red;
                    model.Series.Add(metDataSeries);

                    Met.WSWD_Dist thisDist = checkedMets[i].GetWS_WD_Dist(thisInst.modeledHeight, thisTOD, thisSeason);

                    if (checkedMets[i].expo != null)
                    {
                        double thisXVal = Math.Round(GetXValForExpoSR_Plot(plotName, WD_Ind, checkedMets[i].expo[radiusIndex], checkedMets[i].elev, thisDist.windRose), 3);

                        if (thisXVal < minXValue)
                            minXValue = thisXVal;

                        if (thisXVal > maxXValue)
                            maxXValue = thisXVal;

                        double thisWS = 0;

                        if (WD_Ind == numWD)
                            thisWS = Math.Round(thisDist.WS, 3);
                        else
                            thisWS = Math.Round(thisDist.WS * thisDist.sectorWS_Ratio[WD_Ind], 3);

                        if (thisWS < minWS)
                            minWS = thisWS;

                        if (thisWS > maxWS)
                            maxWS = thisWS;

                        metDataSeries.Points.Add(new ScatterPoint(thisXVal, thisWS));
                    }
                }
            }

            if (plotTurbs == true && checkedTurbines != null)
            {
                for (int i = 0; i < checkedTurbines.Length; i++)
                {
                    var turbineDataSeries = new ScatterSeries();
                    turbineDataSeries.MarkerSize = 5;
                    turbineDataSeries.Title = checkedTurbines[i].name;
                    turbineDataSeries.MarkerFill = OxyColors.Blue;
                    model.Series.Add(turbineDataSeries);

                    Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(null);

                    if (checkedTurbines[i].expo != null)
                    {
                        double thisXVal = Math.Round(GetXValForExpoSR_Plot(plotName, WD_Ind, checkedTurbines[i].expo[radiusIndex], checkedTurbines[i].elev, avgEst.freeStream.windRose), 3);
                        double thisWS = Math.Round(checkedTurbines[i].GetAvgOrSectorWS_Est(null, WD_Ind, "WS", new TurbineCollection.PowerCurve()), 3);
                        turbineDataSeries.Points.Add(new ScatterPoint(thisXVal, thisWS));

                        if (thisXVal < minXValue)
                            minXValue = thisXVal;

                        if (thisXVal > maxXValue)
                            maxXValue = thisXVal;

                        if (thisWS < minWS)
                            minWS = thisWS;

                        if (thisWS > maxWS)
                            maxWS = thisWS;
                    }
                }
            }

            // Customize axis            

            minXValue = minXValue - Math.Abs(minXValue) * 0.02;
            maxXValue = maxXValue + Math.Abs(maxXValue) * 0.02;

            minWS = minWS - Math.Abs(minWS) * 0.01;
            maxWS = maxWS + Math.Abs(maxWS) * 0.01;

            double customStep = (maxXValue - minXValue) / 4;
            double wsStep = (maxWS - minWS) / 4;

            if (Math.Round(wsStep, 1) != 0)
                wsStep = Math.Round(wsStep, 1);
            else if (Math.Round(wsStep, 2) != 0)
                wsStep = Math.Round(wsStep, 2);
            else if (Math.Round(wsStep, 3) != 0)
                wsStep = Math.Round(wsStep, 3);
            else
                wsStep = 0.001;

            if (Math.Round(customStep, 1) != 0)
                customStep = Math.Round(customStep, 1);
            else if (Math.Round(customStep, 2) != 0)
                customStep = Math.Round(customStep, 2);
            else if (Math.Round(customStep, 3) != 0)
                customStep = Math.Round(customStep, 3);
            else
                customStep = 0.001;

            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = GetExpoSR_AxisTitle(plotName);
            xAxis.MajorStep = customStep;
            xAxis.Minimum = minXValue;
            xAxis.Maximum = maxXValue;
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Wind Speed, m/s";
            yAxis.MajorStep = wsStep;
            yAxis.Minimum = minWS;
            yAxis.Maximum = maxWS;

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            thisPlot.Refresh();

        }

        /// <summary> Returns chart title of specified plot. </summary> 
        public string GetExpoSR_ChartTitle(string plotName, int thisHeight)
        {
            string chartTitle = "";

            if (plotName == "UW Expo")
                chartTitle = thisHeight + " m WS vs Upwind Exposure";
            else if (plotName == "DW Expo")
                chartTitle = thisHeight + " m WS vs Downwind Exposure";
            else if (plotName == "DW-UW Expo")
                chartTitle = thisHeight + " m WS vs DW-UW Exposure";
            else if (plotName == "Elev")
                chartTitle = thisHeight + " m WS vs Elevation";
            else if (plotName == "UW SR")
                chartTitle = thisHeight + " m WS vs Upwind Roughness";
            else if (plotName == "DW SR")
                chartTitle = thisHeight + " m WS vs Downwind Roughness";
            else if (plotName == "UW DH")
                chartTitle = thisHeight + " m WS vs Upwind Disp. height";
            else if (plotName == "DW DH")
                chartTitle = thisHeight + " m WS vs Downwind Disp. height";

            return chartTitle;

        }

        /// <summary> Returns x-axis title of specified plot. </summary> 
        public string GetExpoSR_AxisTitle(string plotName)
        {
            string axisTitle = "";

            if (plotName == "UW Expo")
                axisTitle = "Upwind Exposure, m";
            else if (plotName == "DW Expo")
                axisTitle = "Downwind Exposure, m";
            else if (plotName == "DW-UW Expo")
                axisTitle = "DW-UW Exposure, m";
            else if (plotName == "Elev")
                axisTitle = "Elevation, m";
            else if (plotName == "UW SR")
                axisTitle = "Upwind Surface Roughness, m";
            else if (plotName == "DW SR")
                axisTitle = "Downwind Surface Roughness, m";
            else if (plotName == "UW DH")
                axisTitle = "Upwind Displacement height, m";
            else if (plotName == "DW DH")
                axisTitle = "Downwind Displacement height, m";

            return axisTitle;
        }

        /// <summary> Returns X value for specified plot on Met and Turbine Summary. </summary> 
        public double GetXValForExpoSR_Plot(string plotName, int WD_Ind, Exposure thisExpoSR, double elev, double[] windRose)
        {
            double XVal = 0;
            int numWD = 0;

            if (windRose == null)
                return XVal;
            else
                numWD = windRose.Length;

            try
            {
                if (plotName == "UW Expo")
                {
                    if (WD_Ind == numWD)
                        XVal = thisExpoSR.GetOverallValue(windRose, "Expo", "UW");
                    else
                        XVal = thisExpoSR.expo[WD_Ind];

                }
                else if (plotName == "DW Expo")
                {
                    if (WD_Ind == numWD)
                        XVal = thisExpoSR.GetOverallValue(windRose, "Expo", "DW");
                    else
                        XVal = thisExpoSR.GetDW_Param(WD_Ind, "Expo");
                }
                else if (plotName == "Elev")
                    XVal = elev;
                else if (plotName == "DW-UW Expo")
                {
                    if (WD_Ind == numWD)
                        XVal = thisExpoSR.GetOverallValue(windRose, "Expo", "DW") - thisExpoSR.GetOverallValue(windRose, "Expo", "UW");
                    else
                        XVal = thisExpoSR.GetDW_Param(WD_Ind, "Expo") - thisExpoSR.expo[WD_Ind];
                }
                else if (plotName == "UW SR")
                {
                    if (WD_Ind == numWD)
                        XVal = thisExpoSR.GetOverallValue(windRose, "SR", "UW");
                    else
                        XVal = thisExpoSR.SR[WD_Ind];
                }
                else if (plotName == "DW SR")
                {
                    if (WD_Ind == numWD)
                        XVal = thisExpoSR.GetOverallValue(windRose, "SR", "DW");
                    else
                        XVal = thisExpoSR.GetDW_Param(WD_Ind, "SR");
                }
                else if (plotName == "UW DH")
                {
                    if (WD_Ind == numWD)
                        XVal = thisExpoSR.GetOverallValue(windRose, "DH", "UW");
                    else
                        XVal = thisExpoSR.dispH[WD_Ind];
                }
                else if (plotName == "DW DH")
                {
                    if (WD_Ind == numWD)
                        XVal = thisExpoSR.GetOverallValue(windRose, "DH", "DW");
                    else
                        XVal = thisExpoSR.GetDW_Param(WD_Ind, "DH");
                }

            }
            catch
            {
                XVal = -999;
            }

            return XVal;

        }

        /// <summary> Returns array of cumulative distances along path of nodes from site1 to site2. </summary> 
        public double[] CalcDistAlongNodes(TopoInfo.TopoGrid site1, TopoInfo.TopoGrid site2, Nodes[] pathOfNodes)
        {
            int numNodes = 0;

            if (pathOfNodes != null)
                numNodes = pathOfNodes.Length;

            int numPoints = numNodes + 2; // (Start and finish nodes)

            double[] distArr = new double[numPoints];
            distArr[0] = 0;
            TopoInfo topo = new TopoInfo();

            if (numNodes > 0)
            {
                double lastX = site1.UTMX;
                double lastY = site1.UTMY;
                for (int i = 0; i < numNodes; i++)
                {
                    distArr[i + 1] = distArr[i] + topo.CalcDistanceBetweenPoints(lastX, lastY, pathOfNodes[i].UTMX, pathOfNodes[i].UTMY);
                    lastX = pathOfNodes[i].UTMX;
                    lastY = pathOfNodes[i].UTMY;
                }
                distArr[numPoints - 1] = distArr[numPoints - 2] + topo.CalcDistanceBetweenPoints(lastX, lastY, site2.UTMX, site2.UTMY);
            }
            else
                distArr[1] = topo.CalcDistanceBetweenPoints(site1.UTMX, site1.UTMY, site2.UTMX, site2.UTMY);

            return distArr;
        }

        /// <summary> Updates the path of nodes plot on Advanced tab to show selected parameters. </summary> 
        public void PlotAdvancedTable(Continuum thisInst)
        {
            thisInst.plotPathAlongNodes.Model = new PlotModel();
            var model = thisInst.plotPathAlongNodes.Model;
            model.IsLegendVisible = false;

            if (thisInst.modelList.ModelCount == 0) return;

            if (thisInst.lstPathNodes.Items.Count == 0)
            {
                thisInst.plotPathAlongNodes.Refresh();
                return;
            }

            int radiusInd = thisInst.GetRadiusInd("Advanced");
            if (thisInst.metList.ThisCount == 0) return;

            int WD_Ind = thisInst.GetWD_ind("Advanced");
            if (WD_Ind == -1) return;

            Met.TOD thisTOD = thisInst.GetSelectedTOD("Advanced");
            Met.Season thisSeason = thisInst.GetSelectedSeason("Advanced");

            Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisTOD, thisSeason,
                thisInst.modeledHeight, false);
            Model thisModel = theseModels[radiusInd];

            bool useSecondYAxis = false;
            int numSeries = 0;
            int numCols = thisInst.lstPathNodes.Columns.Count;
            string startMetStr = thisInst.GetStartMetAdvanced();
            string endMetStr = thisInst.GetEndSiteAdvanced();

            for (int i = 1; i < numCols; i++) // skip first column which has Site name            
                if (thisInst.lstPathNodes.Columns[i].Name != "UTMX" && thisInst.lstPathNodes.Columns[i].Name != "UTMY")
                    numSeries++;

            TopoInfo.TopoGrid site1 = new TopoInfo.TopoGrid();
            TopoInfo.TopoGrid site2 = new TopoInfo.TopoGrid();

            if (startMetStr == "" || endMetStr == "")
                return;

            for (int i = 0; i < thisInst.metList.ThisCount; i++)
            {
                if (thisInst.metList.metItem[i].name == startMetStr)
                {
                    site1.UTMX = thisInst.metList.metItem[i].UTMX;
                    site1.UTMY = thisInst.metList.metItem[i].UTMY;
                }
                else if (thisInst.metList.metItem[i].name == endMetStr)
                {
                    site2.UTMX = thisInst.metList.metItem[i].UTMX;
                    site2.UTMY = thisInst.metList.metItem[i].UTMY;
                }
            }

            Nodes[] pathOfNodes = null;

            if (site1.UTMX != 0 && site2.UTMX != 0)
            { // met pair
                for (int i = 0; i < thisInst.metPairList.PairCount; i++)
                {
                    Pair_Of_Mets.WS_CrossPreds thisCrossPred = thisInst.metPairList.metPairs[i].GetWS_CrossPred(theseModels[radiusInd]);
                    if ((thisInst.metPairList.metPairs[i].met1.name == startMetStr && thisInst.metPairList.metPairs[i].met2.name == endMetStr) ||
                        ((thisInst.metPairList.metPairs[i].met2.name == startMetStr && thisInst.metPairList.metPairs[i].met1.name == endMetStr)))
                    {
                        pathOfNodes = thisCrossPred.nodePath;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
                {
                    if (thisInst.turbineList.turbineEsts[i].name == endMetStr)
                    {
                        site2.UTMX = thisInst.turbineList.turbineEsts[i].UTMX;
                        site2.UTMY = thisInst.turbineList.turbineEsts[i].UTMY;

                        for (int j = 0; j < thisInst.turbineList.turbineEsts[i].WSEst_Count; j++)
                            if (thisInst.turbineList.turbineEsts[i].WS_Estimate[j].predictorMetName == startMetStr &&
                            thisInst.modelList.IsSameModel(thisModel, thisInst.turbineList.turbineEsts[i].WS_Estimate[j].model))
                                pathOfNodes = thisInst.turbineList.turbineEsts[i].WS_Estimate[j].pathOfNodes;

                        break;
                    }
                }
            }

            // Loop through column names to determine if secondary Y axis is needed
            Advanced_to_show paramToShow = new Advanced_to_show();

            for (int i = 0; i < numCols; i++)
            {
                if (thisInst.lstPathNodes.Columns[i].Text == "UTMX") paramToShow.showUTMX = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "UTMY") paramToShow.showUTMY = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "Elevation") paramToShow.showElevation = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "P10 UW") paramToShow.showP10UW = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "P10 DW") paramToShow.showP10DW = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "UW Expo") paramToShow.showUWExpo = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "DW Expo") paramToShow.showDWExpo = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "UW Roughness") paramToShow.showUWRough = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "DW Roughness") paramToShow.showDWRough = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "UW Disp H") paramToShow.showUWDispH = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "DW Disp H") paramToShow.showDWDispH = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "dWS UW Expo") paramToShow.show_dWS_UWExpo = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "dWS DW Expo") paramToShow.show_dWS_DWExpo = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "dWS UW SRDH") paramToShow.show_dWS_UW_SRDH = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "dWS DW SRDH") paramToShow.show_dWS_DW_SRDH = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "WS Est.") paramToShow.showWS_Est = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "Equiv WS") paramToShow.showEquivWS = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "Actual WS") paramToShow.showActualWS = true;
            }

            if (paramToShow.showUWExpo && (paramToShow.showUWRough || paramToShow.showDWRough || paramToShow.showUWDispH || paramToShow.showDWDispH ||
                                                paramToShow.show_dWS_UWExpo || paramToShow.show_dWS_DWExpo || paramToShow.show_dWS_UW_SRDH || paramToShow.show_dWS_DW_SRDH))
                useSecondYAxis = true;
            else if (paramToShow.showDWExpo && (paramToShow.showUWRough || paramToShow.showDWRough || paramToShow.showUWDispH || paramToShow.showDWDispH ||
                                                paramToShow.show_dWS_UWExpo || paramToShow.show_dWS_DWExpo || paramToShow.show_dWS_UW_SRDH || paramToShow.show_dWS_DW_SRDH))
                useSecondYAxis = true;
            else if (paramToShow.showP10UW && (paramToShow.showUWRough || paramToShow.showDWRough || paramToShow.showUWDispH || paramToShow.showDWDispH ||
                                                paramToShow.show_dWS_UWExpo || paramToShow.show_dWS_DWExpo || paramToShow.show_dWS_UW_SRDH || paramToShow.show_dWS_DW_SRDH))
                useSecondYAxis = true;
            else if (paramToShow.showP10DW && (paramToShow.showUWRough || paramToShow.showDWRough || paramToShow.showUWDispH || paramToShow.showDWDispH ||
                                                paramToShow.show_dWS_UWExpo || paramToShow.show_dWS_DWExpo || paramToShow.show_dWS_UW_SRDH || paramToShow.show_dWS_DW_SRDH))
                useSecondYAxis = true;
            else if (paramToShow.showWS_Est && (paramToShow.showUWRough || paramToShow.showDWRough || paramToShow.showUWDispH || paramToShow.showDWDispH ||
                                                paramToShow.show_dWS_UWExpo || paramToShow.show_dWS_DWExpo || paramToShow.show_dWS_UW_SRDH || paramToShow.show_dWS_DW_SRDH))
                useSecondYAxis = true;
            else if (paramToShow.showEquivWS && (paramToShow.showUWRough || paramToShow.showDWRough || paramToShow.showUWDispH || paramToShow.showDWDispH ||
                                                paramToShow.show_dWS_UWExpo || paramToShow.show_dWS_DWExpo || paramToShow.show_dWS_UW_SRDH || paramToShow.show_dWS_DW_SRDH))
                useSecondYAxis = true;
            else if (paramToShow.showActualWS && (paramToShow.showUWRough || paramToShow.showDWRough || paramToShow.showUWDispH || paramToShow.showDWDispH ||
                                                paramToShow.show_dWS_UWExpo || paramToShow.show_dWS_DWExpo || paramToShow.show_dWS_UW_SRDH || paramToShow.show_dWS_DW_SRDH))
                useSecondYAxis = true;
            else if (paramToShow.showElevation && (paramToShow.showUWRough || paramToShow.showDWRough || paramToShow.showUWDispH || paramToShow.showDWDispH ||
                                                paramToShow.show_dWS_UWExpo || paramToShow.show_dWS_DWExpo || paramToShow.show_dWS_UW_SRDH || paramToShow.show_dWS_DW_SRDH))
                useSecondYAxis = true;

            string titleLabel = "Estimated and Calculated Values along Node Path";
            model.Title = titleLabel;

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Distance, m";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Key = "Primary";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            LinearAxis secYAxis = new LinearAxis();
            if (useSecondYAxis)
            {
                secYAxis.Position = AxisPosition.Right;
                secYAxis.Key = "Secondary";
                model.Axes.Add(secYAxis);
            }

            double[] distArr = CalcDistAlongNodes(site1, site2, pathOfNodes);
            int numPoints = distArr.Length;

            int series1Ind = 0;
            int series2Ind = 0;
            double thisTableVal = 0;

            if (useSecondYAxis == false)
            {
                for (int i = 1; i < numCols; i++)
                {
                    string colName = thisInst.lstPathNodes.Columns[i].Text;

                    if (colName != "UTMX" && colName != "UTMY")
                    {
                        LineSeries dataSeries = new LineSeries();
                        dataSeries.Title = colName;
                        model.Series.Add(dataSeries);

                        if (colName == "Actual WS")
                        {
                            thisTableVal = Convert.ToSingle(thisInst.lstPathNodes.Items[numPoints - 1].SubItems[i].Text);
                            dataSeries.Points.Add(new DataPoint(distArr[distArr.Length - 1], thisTableVal));
                        }
                        else
                        {
                            int Y_ind = 0;

                            foreach (ListViewItem item in thisInst.lstPathNodes.Items)
                            {
                                if (item.SubItems[i].Text != "")
                                    thisTableVal = Convert.ToSingle(item.SubItems[i].Text);
                                else
                                    thisTableVal = 0;

                                dataSeries.Points.Add(new DataPoint(distArr[Y_ind], thisTableVal));
                                Y_ind++;
                            }
                        }
                        series1Ind++;
                    }
                }

            }
            else
            {
                for (int i = 1; i < numCols; i++)
                {
                    string colName = thisInst.lstPathNodes.Columns[i].Text;

                    if (colName != "UTMX" && colName != "UTMY")
                    {
                        if (colName == "UW Expo" || colName == "DW Expo" || colName == "P10 UW" || colName == "P10 DW" || colName == "WS Est." || colName == "Equiv WS"
                                    || colName == "Actual WS" || colName == "Elevation")
                        {

                            LineSeries dataSeries = new LineSeries();
                            dataSeries.Title = colName;
                            dataSeries.YAxisKey = "Primary";
                            model.Series.Add(dataSeries);

                            if (colName == "Actual WS")
                            {
                                try
                                {
                                    thisTableVal = Convert.ToSingle(thisInst.lstPathNodes.Items[numPoints - 1].SubItems[i].Text);
                                }
                                catch
                                {
                                    thisTableVal = 0;
                                }

                                dataSeries.Points.Add(new DataPoint(distArr[distArr.Length - 1], thisTableVal));
                            }
                            else
                            {
                                int Y_ind = 0;
                                foreach (ListViewItem item in thisInst.lstPathNodes.Items)
                                {
                                    if (item.SubItems[i].Text != "")
                                        thisTableVal = Convert.ToSingle(item.SubItems[i].Text);
                                    else
                                        thisTableVal = 0;

                                    dataSeries.Points.Add(new DataPoint(distArr[Y_ind], thisTableVal));
                                    Y_ind++;

                                }
                            }
                            series1Ind++;
                        }
                        else
                        {

                            LineSeries dataSeries = new LineSeries();
                            dataSeries.Title = colName;
                            dataSeries.YAxisKey = "Secondary";
                            model.Series.Add(dataSeries);

                            int Y_ind = 0;
                            foreach (ListViewItem item in thisInst.lstPathNodes.Items)
                            {
                                if (item.SubItems[i].Text != "")
                                    thisTableVal = Convert.ToSingle(item.SubItems[i].Text);
                                else
                                    thisTableVal = 0;

                                dataSeries.Points.Add(new DataPoint(distArr[Y_ind], thisTableVal));
                                Y_ind++;
                            }
                            series2Ind++;
                        }

                    }

                }

            }

            string Y_label_1 = "";
            string Y_label_2 = "";

            if (useSecondYAxis == true)
            {
                if (paramToShow.showUWExpo || paramToShow.showDWExpo || paramToShow.showP10UW || paramToShow.showP10UW)
                    Y_label_1 = "Expo. (m)";

                if (paramToShow.showWS_Est || paramToShow.showActualWS || paramToShow.showEquivWS)
                {
                    if (Y_label_1 == "")
                        Y_label_1 = "WS (m/s)";
                    else
                        Y_label_1 = Y_label_1 + ", WS (m/s)";
                }

                if (paramToShow.showElevation)
                {
                    if (Y_label_1 == "")
                        Y_label_1 = "Elev. (m)";
                    else
                        Y_label_1 = Y_label_1 + ", Elev. (m)";
                }

                if (paramToShow.showUWRough || paramToShow.showDWRough)
                    Y_label_2 = "Roughness (m)";


                if (paramToShow.showUWDispH || paramToShow.showDWDispH)
                {
                    if (Y_label_2 == "")
                        Y_label_2 = "Disp. H (m)";
                    else
                        Y_label_2 = Y_label_2 + ", Disp. H (m)";
                }

                if (paramToShow.show_dWS_UWExpo || paramToShow.show_dWS_DWExpo || paramToShow.show_dWS_UW_SRDH || paramToShow.show_dWS_DW_SRDH)
                {
                    if (Y_label_2 == "")
                        Y_label_2 = "Delta WS (m/s)";
                    else
                        Y_label_2 = Y_label_2 + ", Delta WS (m/s)";
                }
            }
            else
            {

                if (paramToShow.showUWExpo || paramToShow.showDWExpo || paramToShow.showP10UW || paramToShow.showP10UW)
                    Y_label_1 = "Expo. (m)";

                if (paramToShow.showWS_Est || paramToShow.showActualWS || paramToShow.showEquivWS)
                {
                    if (Y_label_1 == "")
                        Y_label_1 = "WS (m/s)";
                    else
                        Y_label_1 = Y_label_1 + ", WS (m/s)";
                }

                if (paramToShow.showElevation)
                {
                    if (Y_label_1 == "")
                        Y_label_1 = "Elev. (m)";
                    else
                        Y_label_1 = Y_label_1 + ", Elev. (m)";
                }

                if (paramToShow.showUWRough || paramToShow.showDWRough)
                    Y_label_1 = "Roughness (m)";

                if (paramToShow.showUWDispH || paramToShow.showDWDispH)
                {
                    if (Y_label_1 == "")
                        Y_label_1 = "Disp. H (m)";
                    else
                        Y_label_1 = Y_label_1 + ", Disp. H (m)";
                }

                if (paramToShow.show_dWS_UWExpo || paramToShow.show_dWS_DWExpo || paramToShow.show_dWS_UW_SRDH || paramToShow.show_dWS_DW_SRDH)
                {
                    if (Y_label_1 == "")
                        Y_label_1 = "Delta WS (m/s)";
                    else
                        Y_label_1 = Y_label_1 + ", Delta WS (m/s)";
                }
            }

            yAxis.Title = Y_label_1;

            if (Y_label_2 != "")
                secYAxis.Title = Y_label_2;

            thisInst.plotPathAlongNodes.Refresh();

        }

        /// <summary> Updates either the wind speed distribution or wind rose plot on Gross Turb Est tab (depending on which is selected from dropdown box). </summary>        
        public void WS_or_WR_Plot(Continuum thisInst)
        {
            string WS_or_WR = thisInst.cboWS_or_WD.SelectedItem.ToString();

            if (WS_or_WR == "WS")
            {
                thisInst.plotGrossWS_Dist.Visible = true;
                thisInst.plotGrossWindRose.Visible = false;
                WSDist_Plot(thisInst);
            }
            else
            {
                thisInst.plotGrossWS_Dist.Visible = false;
                thisInst.plotGrossWindRose.Visible = true;
                WR_Plot(thisInst);
            }
        }

        /// <summary> Updates wind rose plot on 'Gross Turbine Ests' tab. </summary>        
        public void WR_Plot(Continuum thisInst)
        {
            thisInst.plotGrossWindRose.Model = new PlotModel();
            var model = thisInst.plotGrossWindRose.Model;
            model.Title = "Wind Roses at Met and Turbine Sites";
            model.PlotType = PlotType.Polar;
            model.IsLegendVisible = false;

            Met[] checkedMets = thisInst.GetCheckedMets("Gross");
            int checkedMetCount = checkedMets.Length;

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Gross");
            int checkedTurbCount = checkedTurbines.Length;

            int metCount = thisInst.metList.ThisCount;
            if (metCount == 0) return;
            int numWD = thisInst.GetNumWD();

            model.Axes.Add(new AngleAxis
            {
                StartAngle = 90,
                EndAngle = -270,
                Minimum = 0,
                Maximum = 360,
                MajorStep = 15
            });

            for (int j = 0; j < checkedMetCount; j++)
            {
                Met.WSWD_Dist thisDist = checkedMets[j].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                var metSeries = new LineSeries();
                SortedList<double, double> ii = new SortedList<double, double>();

                for (int WDind = 0; WDind < numWD; WDind++)
                    ii.Add(Math.Round((double)(WDind * 360 / numWD), 1), Math.Round(thisDist.windRose[WDind], 4));

                ii.Add(360, thisDist.windRose[0]);

                metSeries.ItemsSource = ii;
                metSeries.LineStyle = LineStyle.Solid;

                metSeries.DataFieldX = "Value";
                metSeries.DataFieldY = "Key";
                metSeries.Title = checkedMets[j].name;

                model.Series.Add(metSeries);
            }

            for (int j = 0; j < checkedTurbCount; j++)
            {
                Turbine.Avg_Est avgEst = checkedTurbines[j].GetAvgWS_Est(null);

                if (avgEst.freeStream.windRose == null)
                    return;

                var turbineSeries = new LineSeries();
                SortedList<double, double> ii = new SortedList<double, double>();

                for (int WDind = 0; WDind < numWD; WDind++)
                    ii.Add(Math.Round((double)(WDind * 360 / numWD), 1), Math.Round(avgEst.freeStream.windRose[WDind], 4));

                ii.Add(360, avgEst.freeStream.windRose[0]);

                turbineSeries.ItemsSource = ii;
                turbineSeries.LineStyle = LineStyle.Solid;

                turbineSeries.DataFieldX = "Value";
                turbineSeries.DataFieldY = "Key";
                turbineSeries.Title = checkedTurbines[j].name;

                model.Series.Add(turbineSeries);
            }

            thisInst.plotGrossWindRose.Refresh();

        }

        /// <summary> Updates wind speed distribution plot on Gross Turbine Ests tab. </summary>
        public void WSDist_Plot(Continuum thisInst)
        {
            thisInst.plotGrossWS_Dist.Model = new PlotModel();
            var model = thisInst.plotGrossWS_Dist.Model;
            model.IsLegendVisible = false;
            model.Title = "Wind Speed Distributions";

            if (thisInst.metList.ThisCount == 0) return;
            int numWD = thisInst.GetNumWD();

            if (numWD <= 1)
            {
                thisInst.plotGrossWS_Dist.Refresh();
                return;
            }

            int WD_Ind = thisInst.GetWD_ind("Gross");

            if (WD_Ind == -1)
                return;

            Met[] checkedMets = thisInst.GetCheckedMets("Gross");
            int checkedMetCount = checkedMets.Length;

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Gross");
            int checkedTurbCount = checkedTurbines.Length;

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Wind Speed, m/s";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Freq. of Occurrence";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            if ((thisInst.metList.ThisCount > 0) && (checkedMetCount > 0 || checkedTurbCount > 0))
            {
                double WS_FirstInt = thisInst.metList.WS_FirstInt;
                double WS_IntSize = thisInst.metList.WS_IntSize;
                int numWS = thisInst.metList.numWS;

                for (int i = 0; i <= checkedMetCount - 1; i++)
                {
                    var metSeries = new LineSeries();
                    metSeries.LineStyle = LineStyle.Solid;
                    metSeries.Title = checkedMets[i].name;
                    model.Series.Add(metSeries);

                    Met.WSWD_Dist thisDist = checkedMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                    for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
                    {
                        double thisX = Math.Round((WS_FirstInt + WS_ind * WS_IntSize - WS_IntSize / 2), 1);

                        if (WD_Ind == numWD)
                            metSeries.Points.Add(new DataPoint(thisX, Math.Round(thisDist.WS_Dist[WS_ind], 4)));
                        else
                            metSeries.Points.Add(new DataPoint(thisX, Math.Round(thisDist.sectorWS_Dist[WD_Ind, WS_ind], 4)));
                    }
                }

                if (thisInst.turbineList.GotEst("WS", new TurbineCollection.PowerCurve(), null) == true)
                {
                    for (int i = 0; i < checkedTurbCount; i++)
                    {
                        if (checkedTurbines[i].AvgWSEst_Count == 0)
                        {
                            thisInst.plotGrossWS_Dist.Refresh();
                            return;
                        }

                        var turbineSeries = new LineSeries();
                        turbineSeries.LineStyle = LineStyle.Dash;
                        turbineSeries.Title = checkedTurbines[i].name;
                        model.Series.Add(turbineSeries);

                        Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(null);

                        for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
                        {
                            double thisX = Math.Round((WS_FirstInt + WS_ind * WS_IntSize - WS_IntSize / 2), 2);

                            if (WD_Ind == numWD)
                                turbineSeries.Points.Add(new DataPoint(thisX, Math.Round(avgEst.freeStream.WS_Dist[WS_ind], 4)));
                            else
                                turbineSeries.Points.Add(new DataPoint(thisX, Math.Round(avgEst.freeStream.sectorWS_Dist[WD_Ind, WS_ind], 4)));
                        }
                    }
                }
            }

            thisInst.plotGrossWS_Dist.Refresh();

        }

        /// <summary> Returns color (RGB values) based on index of met or turbine for series formatting. </summary>
        public Color GetMetOrTurbColor(int Met_Turb_Ind) // 
        {
            Color thisColor = new Color();
            int lastDigit = Met_Turb_Ind % 10;

            if (lastDigit == 0)
                thisColor = Color.FromArgb(192, 0, 0);
            else if (lastDigit == 1)
                thisColor = Color.FromArgb(112, 48, 160);
            else if (lastDigit == 2)
                thisColor = Color.FromArgb(255, 0, 0);
            else if (lastDigit == 3)
                thisColor = Color.FromArgb(0, 32, 96);
            else if (lastDigit == 4)
                thisColor = Color.FromArgb(255, 192, 0);
            else if (lastDigit == 5)
                thisColor = Color.FromArgb(0, 112, 192);
            else if (lastDigit == 6)
                thisColor = Color.FromArgb(146, 208, 80);
            else if (lastDigit == 7)
                thisColor = Color.FromArgb(0, 176, 240);
            else if (lastDigit == 8)
                thisColor = Color.FromArgb(0, 176, 80);
            else
                thisColor = Color.FromArgb(255, 255, 0);

            return thisColor;
        }

        /// <summary> Updates (net/waked) wind speed distribution plot on Net Turb Ests tab. </summary>        
        public void WakedWSDistPlot(Continuum thisInst)
        {
            if (thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy || thisInst.BW_worker.BackgroundWorker_Map.IsBusy)
                return;

            thisInst.plotWakedDists.Model = new PlotModel();
            var model = thisInst.plotWakedDists.Model;
            model.IsLegendVisible = false;

            double maxFreq = 0;
            Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();

            if (thisWakeModel == null)
            {
                thisInst.plotWakedDists.Refresh();
                return;
            }

            int numWD = thisInst.GetNumWD();

            if (numWD <= 1)
            {
                thisInst.plotWakedDists.Refresh();
                return;
            }

            int WD_Ind = thisInst.GetWD_ind("Net");
            if (WD_Ind == -1)
            {
                thisInst.plotWakedDists.Refresh();
                return;
            }

            double WS_FirstInt = thisInst.metList.WS_FirstInt;
            double WS_IntSize = thisInst.metList.WS_IntSize;

            int numWS = thisInst.metList.numWS;

            double[] Turb_X_WS = new double[numWS];
            double[] Turb_Y_Freq = new double[numWS];

            int plotInd = 0;
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Net");

            if (checkedTurbines == null)
            {
                thisInst.plotWakedDists.Refresh();
                return;
            }

            for (int i = 0; i < checkedTurbines.Length; i++)
            {
                Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(thisWakeModel);

                if (avgEst.waked.WS != 0)
                {
                    for (int j = 0; j < numWS; j++)
                    {
                        Turb_X_WS[j] = WS_FirstInt + j * WS_IntSize - WS_IntSize / 2;

                        if (WD_Ind == numWD)
                        {
                            Turb_Y_Freq[j] = avgEst.waked.WS_Dist[j];
                            if (avgEst.waked.WS_Dist[j] > maxFreq)
                                maxFreq = avgEst.waked.WS_Dist[j];
                        }
                        else
                        {

                            Turb_Y_Freq[j] = avgEst.waked.sectorWS_Dist[WD_Ind, j];

                            if (avgEst.waked.sectorWS_Dist[WD_Ind, j] > maxFreq)
                                maxFreq = avgEst.waked.sectorWS_Dist[WD_Ind, j];
                        }
                    }

                    LineSeries thisSeries = new LineSeries();
                    thisSeries.Title = checkedTurbines[i].name;
                    model.Series.Add(thisSeries);

                    for (int j = 0; j < numWS; j++)
                        thisSeries.Points.Add(new DataPoint(Math.Round(Turb_X_WS[j], 2), Math.Round(Turb_Y_Freq[j], 4)));

                    plotInd++;
                }

            }

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Wind Speed, m/s";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Freq. of Occurrence";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            double height = thisInst.modeledHeight;
            model.Title = height + " m Waked WS Distribution";

            thisInst.plotWakedDists.Refresh();

        }

        /// <summary> Updates plot of power and thrust curves on Gross Turb Ests tab. </summary>        
        public void PowerCrvPlot(Continuum thisInst)
        {
            int chkPowerCurveCount = thisInst.lstPowerCurveList.CheckedItems.Count;
            int powerCurveCount = thisInst.turbineList.PowerCurveCount;

            thisInst.plotGross_PowerCrvs.Model = new PlotModel();
            var model = thisInst.plotGross_PowerCrvs.Model;
            model.IsLegendVisible = false;
            model.Title = "Power and Thrust Curves";

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Wind Speed, m/s";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Power, kW";
            yAxis.Key = "Power";
            LinearAxis secYAxis = new LinearAxis();
            secYAxis.Position = AxisPosition.Right;
            secYAxis.Title = "Thrust Coeff.";
            secYAxis.Key = "Thrust";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);
            model.Axes.Add(secYAxis);

            if (powerCurveCount == 0)
            {
                thisInst.plotGross_PowerCrvs.Refresh();
                return;
            }

            if (chkPowerCurveCount > 0)
            {
                for (int i = 0; i < chkPowerCurveCount; i++)
                {
                    TurbineCollection.PowerCurve thisPowerCurve = thisInst.turbineList.GetPowerCurve(thisInst.lstPowerCurveList.CheckedItems[i].Text);

                    if (thisPowerCurve.power != null)
                    {
                        var powerCurve = new LineSeries();
                        powerCurve.Title = "Power: " + thisPowerCurve.name;
                        powerCurve.YAxisKey = "Power";
                        model.Series.Add(powerCurve);

                        var thrustCurve = new LineSeries();
                        thrustCurve.Title = "Thrust Coeff: " + thisPowerCurve.name;
                        thrustCurve.YAxisKey = "Thrust";
                        model.Series.Add(thrustCurve);

                        for (int j = 0; j < thisPowerCurve.power.Length; j++)
                        {
                            double thisX = Math.Round(thisPowerCurve.firstWS + j * thisPowerCurve.wsInt, 2);
                            powerCurve.Points.Add(new DataPoint(thisX, Math.Round(thisPowerCurve.power[j], 0)));
                            thrustCurve.Points.Add(new DataPoint(thisX, Math.Round(thisPowerCurve.thrustCoeff[j], 4)));
                        }
                    }
                }
            }

            thisInst.plotGross_PowerCrvs.Refresh();

        }

        /// <summary> Updates Log-Log plot : Downhill coeff vs P10 Expo on Advanced tab. </summary>        
        public void DownhillLogLogPlot(Continuum thisInst, Model thisModel, string DH_plot_to_show)
        {
            Model origModel = new Model();
            int numWD = thisInst.GetNumWD();
            int WD_Ind = thisInst.GetWD_ind("Advanced");
            origModel.SizeArrays(numWD);
            origModel.SetDefaultModelCoeffs(numWD);
            NodeCollection nodeList = new NodeCollection();

            thisInst.plotDHModel.Model = new PlotModel();
            var model = thisInst.plotDHModel.Model;
            model.IsLegendVisible = false;

            if (DH_plot_to_show == "Separated flow")
            {
                double[] negDW_Coeffs;
                int negDW_CoeffCount = 0;
                double[] P10_Expo;
                double[,] negDW_Coeffs_and_P10_Expos;   // Col 1: DW coeff; Col 2: P10 Expo for DW Expo > 0

                if (thisInst.metPairList.PairCount > 0 && thisModel.metsUsed.Length > 1 && thisModel.isImported == false)
                {
                    negDW_Coeffs_and_P10_Expos = thisInst.metPairList.GetCoeffsForLogLogPlot(thisModel, thisInst, nodeList, WD_Ind, "DW", "Turbulent");
                    negDW_CoeffCount = negDW_Coeffs_and_P10_Expos.GetUpperBound(1) + 1;

                    negDW_Coeffs = new double[negDW_CoeffCount];
                    P10_Expo = new double[negDW_CoeffCount];

                    for (int i = 0; i < negDW_CoeffCount; i++)
                    {
                        negDW_Coeffs[i] = negDW_Coeffs_and_P10_Expos[0, i];
                        P10_Expo[i] = negDW_Coeffs_and_P10_Expos[1, i];
                    }

                    if (negDW_CoeffCount == 1)
                        if (negDW_Coeffs[0] == 0 && P10_Expo[0] == 0)
                            negDW_CoeffCount = 0;
                }
                else
                {
                    if (WD_Ind < numWD)
                    {
                        negDW_CoeffCount = 5;

                        negDW_Coeffs = new double[negDW_CoeffCount];
                        P10_Expo = new double[negDW_CoeffCount];

                        P10_Expo[0] = 80;
                        P10_Expo[1] = 100;
                        P10_Expo[2] = 120;
                        P10_Expo[3] = 160;
                        P10_Expo[4] = 200;

                        for (int i = 0; i <= 4; i++)
                            negDW_Coeffs[i] = thisModel.sep_A_DW[WD_Ind] * Math.Pow(P10_Expo[i], thisModel.sep_B_DW[WD_Ind]);
                    }
                    else
                    {
                        negDW_CoeffCount = 5 * numWD;
                        negDW_Coeffs = new double[negDW_CoeffCount];
                        P10_Expo = new double[negDW_CoeffCount];

                        int P10_ind = 0;

                        for (int i = 0; i <= 4; i++)
                        {
                            for (int j = 0; j < numWD; j++)
                            {
                                if (i == 0) P10_Expo[P10_ind] = 80;
                                if (i == 1) P10_Expo[P10_ind] = 100;
                                if (i == 2) P10_Expo[P10_ind] = 120;
                                if (i == 3) P10_Expo[P10_ind] = 160;
                                if (i == 4) P10_Expo[P10_ind] = 200;

                                negDW_Coeffs[P10_ind] = thisModel.sep_A_DW[j] * Math.Pow(P10_Expo[P10_ind], thisModel.sep_B_DW[j]);
                                P10_ind++;
                            }
                        }
                    }
                }

                // NegDW Flow Separated Plot
                ScatterSeries DH_Scatter_Series = new ScatterSeries(); // scatter of negative DW coeffs and P10 expo

                DH_Scatter_Series.MarkerFill = OxyColors.OrangeRed;
                model.Series.Add(DH_Scatter_Series);

                LineSeries DH_Default_Series = new LineSeries();  // line of default model                
                DH_Default_Series.MarkerStroke = OxyColors.Red;
                DH_Default_Series.LineStyle = LineStyle.Solid;

                model.Series.Add(DH_Default_Series);

                if (WD_Ind < numWD)
                {
                    LineSeries DH_Calib_Series = new LineSeries(); // line of site-calibrated model

                    DH_Calib_Series.MarkerStroke = OxyColors.Blue;
                    DH_Calib_Series.LineStyle = LineStyle.Solid;
                    DH_Calib_Series.Title = "Site-Calibrated model";

                    DH_Calib_Series.Points.Add(new DataPoint(50, Math.Round(thisModel.sep_A_DW[WD_Ind] * Math.Pow(50, thisModel.sep_B_DW[WD_Ind]), 4)));
                    DH_Calib_Series.Points.Add(new DataPoint(200, Math.Round(thisModel.sep_A_DW[WD_Ind] * Math.Pow(200, thisModel.sep_B_DW[WD_Ind]), 4)));

                    model.Series.Add(DH_Calib_Series);
                }

                if (thisModel.isImported == false)
                    DH_Scatter_Series.Title = "DW < 0";
                else
                    DH_Scatter_Series.Title = "Separated Imported";

                for (int i = 0; i < negDW_CoeffCount; i++)
                    DH_Scatter_Series.Points.Add(new ScatterPoint(Math.Round(P10_Expo[i], 3), Math.Round(Math.Abs(negDW_Coeffs[i]), 4)));

                DH_Default_Series.Title = "Flow Sep Orig";
                DH_Default_Series.Points.Add(new DataPoint(50, Math.Round(origModel.sep_A_DW[WD_Ind] * Math.Pow(50, origModel.sep_B_DW[WD_Ind]), 4)));
                DH_Default_Series.Points.Add(new DataPoint(200, Math.Round(origModel.sep_A_DW[WD_Ind] * Math.Pow(200, origModel.sep_B_DW[WD_Ind]), 4)));

                string Xlabel1 = "ln P10 Exposure";
                string yLabel1 = "ln Coeff";

                // Specify axes
                LogarithmicAxis xAxis = new LogarithmicAxis();
                xAxis.Position = AxisPosition.Bottom;
                xAxis.Title = Xlabel1;

                LogarithmicAxis yAxis = new LogarithmicAxis();
                yAxis.Position = AxisPosition.Left;
                yAxis.Title = yLabel1;

                model.Axes.Add(xAxis);
                model.Axes.Add(yAxis);

                model.Title = yLabel1 + " vs " + Xlabel1 + " for Downhill Flow";
            }
            else
            {
                // Attached downhill flow: Mdw > 0 and Muw < 0
                double[] posDW_Coeffs = null;
                double[] negUW_Coeffs = null;
                int posDW_CoeffCount = 0;
                int negUW_CoeffCount = 0;

                double[] P10_Expo_PosDW = null;
                double[] P10_Expo_NegUW = null;

                double[,] posDW_Coeffs_and_P10_Expos;  // Col 1: DW coeff; Col 2: P10 Expo for DW Expo > 0
                double[,] negUW_Coeffs_and_P10_Expos;  // Col 1: UW coeff; Col 2: P10 Expo for UW Expo < 0 and CW Grade < max Grade

                if (thisInst.metPairList.PairCount > 0 && thisModel.metsUsed.Length > 1 && thisModel.isImported == false)
                {
                    posDW_Coeffs_and_P10_Expos = thisInst.metPairList.GetCoeffsForLogLogPlot(thisModel, thisInst, nodeList, WD_Ind, "DW", "Downhill");
                    negUW_Coeffs_and_P10_Expos = thisInst.metPairList.GetCoeffsForLogLogPlot(thisModel, thisInst, nodeList, WD_Ind, "UW", "Downhill");

                    posDW_CoeffCount = posDW_Coeffs_and_P10_Expos.GetUpperBound(1) + 1;
                    negUW_CoeffCount = negUW_Coeffs_and_P10_Expos.GetUpperBound(1) + 1;

                    posDW_Coeffs = new double[posDW_CoeffCount];
                    P10_Expo_PosDW = new double[posDW_CoeffCount];

                    negUW_Coeffs = new double[negUW_CoeffCount];
                    P10_Expo_NegUW = new double[negUW_CoeffCount];

                    for (int i = 0; i <= posDW_CoeffCount - 1; i++)
                    {
                        posDW_Coeffs[i] = posDW_Coeffs_and_P10_Expos[0, i];
                        P10_Expo_PosDW[i] = posDW_Coeffs_and_P10_Expos[1, i];
                    }

                    for (int i = 0; i <= negUW_CoeffCount - 1; i++)
                    {
                        negUW_Coeffs[i] = Math.Abs(negUW_Coeffs_and_P10_Expos[0, i]);
                        P10_Expo_NegUW[i] = negUW_Coeffs_and_P10_Expos[1, i];
                    }

                    if (posDW_CoeffCount == 1)
                        if (posDW_Coeffs[0] == 0 && P10_Expo_PosDW[0] == 0)
                            posDW_CoeffCount = 0;

                    if (negUW_CoeffCount == 1)
                        if (negUW_Coeffs[0] == 0 && P10_Expo_NegUW[0] == 0)
                            negUW_CoeffCount = 0;

                }
                else
                {

                    if (WD_Ind < numWD)
                    {
                        posDW_CoeffCount = 5;
                        posDW_Coeffs = new double[posDW_CoeffCount];
                        P10_Expo_PosDW = new double[posDW_CoeffCount];

                        P10_Expo_PosDW[0] = 10;
                        P10_Expo_PosDW[1] = 20;
                        P10_Expo_PosDW[2] = 40;
                        P10_Expo_PosDW[3] = 80;
                        P10_Expo_PosDW[4] = 100;

                        for (int i = 0; i <= 4; i++)
                            posDW_Coeffs[i] = thisModel.downhill_A[WD_Ind] * Math.Pow(P10_Expo_PosDW[i], thisModel.downhill_B[WD_Ind]);
                    }
                    else
                    {
                        posDW_CoeffCount = 5 * numWD;
                        posDW_Coeffs = new double[posDW_CoeffCount];
                        P10_Expo_PosDW = new double[posDW_CoeffCount];

                        int P10_ind = 0;

                        for (int i = 0; i <= 4; i++)
                        {
                            for (int j = 0; j < numWD; j++)
                            {
                                if (i == 0) P10_Expo_PosDW[P10_ind] = 10;
                                if (i == 1) P10_Expo_PosDW[P10_ind] = 20;
                                if (i == 2) P10_Expo_PosDW[P10_ind] = 40;
                                if (i == 3) P10_Expo_PosDW[P10_ind] = 80;
                                if (i == 4) P10_Expo_PosDW[P10_ind] = 100;

                                posDW_Coeffs[P10_ind] = thisModel.downhill_A[j] * Math.Pow(P10_Expo_PosDW[P10_ind], thisModel.downhill_B[j]);
                                P10_ind++;
                            }
                        }
                    }
                }

                // PosDW & NegUW Plot
                ScatterSeries posDW_ScatterSeries = new ScatterSeries(); // scatter of negative DW>0 coeffs and P10 expo                
                posDW_ScatterSeries.MarkerFill = OxyColors.OrangeRed;
                model.Series.Add(posDW_ScatterSeries);

                ScatterSeries negUW_ScatterSeries = new ScatterSeries(); // scatter of UW<0 coeffs and P10 expo                
                negUW_ScatterSeries.MarkerFill = OxyColors.BlueViolet;
                model.Series.Add(negUW_ScatterSeries);

                LineSeries DH_Default_Series = new LineSeries(); // line of default model                
                DH_Default_Series.MarkerStroke = OxyColors.Red;
                model.Series.Add(DH_Default_Series);

                if (WD_Ind < numWD)
                {
                    LineSeries DH_Calib_Series = new LineSeries(); // line of site-calibrated model                    
                    DH_Calib_Series.MarkerStroke = OxyColors.Blue;
                    DH_Calib_Series.Title = "Downhill Site-Calibrated model";
                    DH_Calib_Series.Points.Add(new DataPoint(1, Math.Round(thisModel.downhill_A[WD_Ind] * Math.Pow(1, thisModel.downhill_B[WD_Ind]), 4)));
                    DH_Calib_Series.Points.Add(new DataPoint(100, Math.Round(thisModel.downhill_A[WD_Ind] * Math.Pow(100, thisModel.downhill_B[WD_Ind]), 4)));
                    model.Series.Add(DH_Calib_Series);
                }

                if (thisModel.isImported == false)
                    posDW_ScatterSeries.Title = "DW > 0";
                else
                    posDW_ScatterSeries.Title = "Downhill Imported";

                for (int i = 0; i < posDW_CoeffCount; i++)
                    posDW_ScatterSeries.Points.Add(new ScatterPoint(Math.Round(P10_Expo_PosDW[i], 3), Math.Round(posDW_Coeffs[i], 3)));

                if (thisModel.isImported == false)
                {
                    negUW_ScatterSeries.Title = "UW < 0";

                    for (int i = 0; i < negUW_CoeffCount; i++)
                        negUW_ScatterSeries.Points.Add(new ScatterPoint(Math.Round(P10_Expo_NegUW[i], 3), Math.Round(negUW_Coeffs[i], 3)));
                }

                DH_Default_Series.Title = "Downhill Default Model";
                DH_Default_Series.Points.Add(new DataPoint(1, Math.Round(origModel.downhill_A[0] * Math.Pow(1, origModel.downhill_B[0]), 4)));
                DH_Default_Series.Points.Add(new DataPoint(100, Math.Round(origModel.downhill_A[0] * Math.Pow(100, origModel.downhill_B[0]), 4)));

                string Xlabel1 = "ln P10 Exposure";
                string yLabel1 = "ln Coeff";

                LogarithmicAxis xAxis = new LogarithmicAxis();
                xAxis.Position = AxisPosition.Bottom;
                xAxis.Title = Xlabel1;
                LogarithmicAxis yAxis = new LogarithmicAxis();
                yAxis.Position = AxisPosition.Left;
                yAxis.Title = yLabel1;

                model.Axes.Add(xAxis);
                model.Axes.Add(yAxis);

                model.Title = yLabel1 + " vs " + Xlabel1 + " for Downhill Flow";

            }

            thisInst.plotDHModel.Refresh();
        }

        /// <summary> Updates plot on Advanced tab showing DW Stability factor (used in the surface roughness model) radar plot. </summary>  
        public void DownhillRoughPlot(Continuum thisInst, Model thisModel)
        {
            int numWD = thisInst.GetNumWD();
            double[] DH_Stab = new double[numWD];   // DW Stability factor by WD sector

            Model origModel = new Model();
            origModel.SizeArrays(numWD);
            origModel.SetDefaultModelCoeffs(numWD);

            thisInst.plotDHModel.Model = new PlotModel();
            var model = thisInst.plotDHModel.Model;

            LineSeries DH_Stab_Series = new LineSeries();
            DH_Stab_Series.MarkerStroke = OxyColors.Blue;
            DH_Stab_Series.Title = "Downhill Stability Factor";
            model.Series.Add(DH_Stab_Series);

            for (int i = 0; i < numWD; i++)
            {
                DH_Stab[i] = thisModel.DH_Stab_A[i];
                DH_Stab_Series.Points.Add(new DataPoint(Math.Round(i * ((double)360 / numWD), 1), Math.Round(DH_Stab[i], 2)));
            }

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Wind direction, degs";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Stability factor";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            model.Title = "Downhill Stability Factor vs. WD";
            model.IsLegendVisible = false;

            thisInst.plotDHModel.Refresh();
        }

        /// <summary> Updates plot on Advanced tab showing UW Stability factor (used in the surface roughness model) radar plot. </summary>
        public void UphillRoughPlot(Continuum thisInst, Model thisModel, string UW_to_show)
        {
            int numWD = thisInst.GetNumWD();
            double[] UH_Stab = new double[numWD];  // UW Stability factor by WD sector                      

            thisInst.plotUHModel.Model = new PlotModel();
            var model = thisInst.plotUHModel.Model;
            model.IsLegendVisible = false;

            LineSeries UH_Stab_Series = new LineSeries();
            UH_Stab_Series.MarkerFill = OxyColors.Blue;
            UH_Stab_Series.Title = "Uphill Stability Factor";
            model.Series.Add(UH_Stab_Series);

            for (int i = 0; i < numWD; i++)
            {
                if (UW_to_show == "UW > UW crit")
                {
                    if (thisModel.UH_Stab_A[i] < 5)
                        UH_Stab[i] = thisModel.UH_Stab_A[i];
                }
                else
                { // UW < UW crit, induced speed-up
                    if (thisModel.SU_Stab_A[i] < 5)
                    {
                        UH_Stab[i] = thisModel.SU_Stab_A[i];
                    }
                }

                UH_Stab_Series.Points.Add(new DataPoint(Math.Round(i * ((double)360 / numWD), 1), Math.Round(UH_Stab[i], 2)));
            }

            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Wind direction, degs";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Stability factor";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            model.Title = "Uphill Stability Factor vs. WD";

            thisInst.plotUHModel.Refresh();
        }

        /// <summary> Updates Log-Log plot : Uphill coeff vs P10 Expo on Advanced tab. </summary>
        public void UphillLogLogPlot(Continuum thisInst, Model thisModel, string UW_plot_to_show)
        {
            int numWD = thisInst.GetNumWD();
            int WD_Ind = thisInst.GetWD_ind("Advanced");
            double[] negDW_Coeffs = null;
            double[] posUW_Coeffs = null;
            double[] P10_Expo_NegDW = null;
            double[] P10_Expo_PosUW = null;

            int negDW_CoeffCount = 0;
            int posUW_CoeffCount = 0;

            Model origModel = new Model();
            origModel.SizeArrays(numWD);
            origModel.SetDefaultModelCoeffs(numWD);
            NodeCollection nodeList = new NodeCollection();

            double[,] negDW_Coeffs_and_P10_Expos;   // Col 1: DW coeff; Col 2: P10 Expo for DW Expo < 0
            double[,] posUW_Coeffs_and_P10_Expos;   // Col 1: UW coeff; Col 2: P10 Expo for UW Expo > 0 and UW > UW crit

            thisInst.plotUHModel.Model = new PlotModel();
            var model = thisInst.plotUHModel.Model;
            model.IsLegendVisible = false;

            if (thisInst.metPairList.PairCount > 0 && thisModel.metsUsed.Length > 1 && thisModel.isImported == false)
            {
                if (UW_plot_to_show == "UW > UW crit")
                {
                    negDW_Coeffs_and_P10_Expos = thisInst.metPairList.GetCoeffsForLogLogPlot(thisModel, thisInst, nodeList, WD_Ind, "DW", "Uphill");
                    posUW_Coeffs_and_P10_Expos = thisInst.metPairList.GetCoeffsForLogLogPlot(thisModel, thisInst, nodeList, WD_Ind, "UW", "Uphill");

                    negDW_CoeffCount = negDW_Coeffs_and_P10_Expos.GetUpperBound(1) + 1;
                    posUW_CoeffCount = posUW_Coeffs_and_P10_Expos.GetUpperBound(1) + 1;

                    negDW_Coeffs = new double[negDW_CoeffCount];
                    P10_Expo_NegDW = new double[negDW_CoeffCount];

                    posUW_Coeffs = new double[posUW_CoeffCount];
                    P10_Expo_PosUW = new double[posUW_CoeffCount];

                    for (int i = 0; i < negDW_CoeffCount; i++)
                    {
                        negDW_Coeffs[i] = negDW_Coeffs_and_P10_Expos[0, i];
                        P10_Expo_NegDW[i] = negDW_Coeffs_and_P10_Expos[1, i];
                    }

                    for (int i = 0; i < posUW_CoeffCount; i++)
                    {
                        posUW_Coeffs[i] = Math.Abs(posUW_Coeffs_and_P10_Expos[0, i]);
                        P10_Expo_PosUW[i] = posUW_Coeffs_and_P10_Expos[1, i];
                    }

                    if (negDW_CoeffCount == 1)
                    {
                        if (negDW_Coeffs[0] == 0 && P10_Expo_NegDW[0] == 0)
                            negDW_CoeffCount = 0;
                    }

                    if (posUW_CoeffCount == 1)
                    {
                        if (posUW_Coeffs[0] == 0 && P10_Expo_PosUW[0] == 0)
                            posUW_CoeffCount = 0;
                    }
                }
                else if (UW_plot_to_show == "UW < UW crit")
                {  // UW_Plot_to_show UW < UW crit
                    posUW_Coeffs_and_P10_Expos = thisInst.metPairList.GetCoeffsForLogLogPlot(thisModel, thisInst, nodeList, WD_Ind, "UW", "SpdUp");
                    posUW_CoeffCount = posUW_Coeffs_and_P10_Expos.GetUpperBound(1) + 1;

                    posUW_Coeffs = new double[posUW_CoeffCount];
                    P10_Expo_PosUW = new double[posUW_CoeffCount];

                    for (int i = 0; i < posUW_CoeffCount; i++)
                    {
                        posUW_Coeffs[i] = posUW_Coeffs_and_P10_Expos[0, i];
                        P10_Expo_PosUW[i] = posUW_Coeffs_and_P10_Expos[1, i];
                    }

                    if (posUW_CoeffCount == 1)
                    {
                        if (posUW_Coeffs[0] == 0 && P10_Expo_PosUW[0] == 0)
                            posUW_CoeffCount = 0;
                    }
                }
            }
            else
            {
                if (WD_Ind < numWD)
                {
                    negDW_CoeffCount = 5;
                    negDW_Coeffs = new double[negDW_CoeffCount];
                    P10_Expo_NegDW = new double[negDW_CoeffCount];

                    if (UW_plot_to_show == "UW > UW crit")
                    {
                        P10_Expo_NegDW[0] = thisModel.UW_crit[WD_Ind];
                        P10_Expo_NegDW[1] = thisModel.UW_crit[WD_Ind] + 10;
                        P10_Expo_NegDW[2] = thisModel.UW_crit[WD_Ind] + 20;
                        P10_Expo_NegDW[3] = thisModel.UW_crit[WD_Ind] + 30;
                        P10_Expo_NegDW[4] = thisModel.UW_crit[WD_Ind] + 50;

                        for (int i = 0; i <= 4; i++)
                            negDW_Coeffs[i] = thisModel.uphill_A[WD_Ind] * Math.Pow(P10_Expo_NegDW[i], thisModel.uphill_B[WD_Ind]);
                    }
                    else
                    {
                        P10_Expo_NegDW[0] = 1;
                        P10_Expo_NegDW[1] = thisModel.UW_crit[WD_Ind] / 4;
                        P10_Expo_NegDW[2] = thisModel.UW_crit[WD_Ind] / 2;
                        P10_Expo_NegDW[3] = thisModel.UW_crit[WD_Ind] * 3 / 4;
                        P10_Expo_NegDW[4] = thisModel.UW_crit[WD_Ind];

                        for (int i = 0; i <= 4; i++)
                            negDW_Coeffs[i] = thisModel.spdUp_A[WD_Ind] * Math.Pow(P10_Expo_NegDW[i], thisModel.spdUp_B[WD_Ind]);
                    }
                }
                else
                {
                    negDW_CoeffCount = 5 * numWD;
                    negDW_Coeffs = new double[negDW_CoeffCount];
                    P10_Expo_NegDW = new double[negDW_CoeffCount];

                    int P10_ind = 0;

                    for (int i = 0; i <= 4; i++)
                    {
                        for (int j = 0; j < numWD; j++)
                        {
                            if (UW_plot_to_show == "UW > UW crit")
                            {
                                if (i == 0) P10_Expo_NegDW[P10_ind] = thisModel.UW_crit[j];
                                if (i == 1) P10_Expo_NegDW[P10_ind] = thisModel.UW_crit[j] + 10;
                                if (i == 2) P10_Expo_NegDW[P10_ind] = thisModel.UW_crit[j] + 20;
                                if (i == 3) P10_Expo_NegDW[P10_ind] = thisModel.UW_crit[j] + 30;
                                if (i == 4) P10_Expo_NegDW[P10_ind] = thisModel.UW_crit[j] + 50;

                                negDW_Coeffs[P10_ind] = thisModel.uphill_A[j] * Math.Pow(P10_Expo_NegDW[P10_ind], thisModel.uphill_B[j]);
                            }
                            else
                            {
                                if (i == 0) P10_Expo_NegDW[P10_ind] = 1;
                                if (i == 1) P10_Expo_NegDW[P10_ind] = thisModel.UW_crit[j] / 4;
                                if (i == 2) P10_Expo_NegDW[P10_ind] = thisModel.UW_crit[j] / 2;
                                if (i == 3) P10_Expo_NegDW[P10_ind] = thisModel.UW_crit[j] * 3 / 4;
                                if (i == 4) P10_Expo_NegDW[P10_ind] = thisModel.UW_crit[j];

                                negDW_Coeffs[P10_ind] = thisModel.spdUp_A[j] * Math.Pow(P10_Expo_NegDW[P10_ind], thisModel.spdUp_B[j]);
                            }

                            P10_ind++;
                        }
                    }
                }
            }

            if (UW_plot_to_show == "UW > UW crit")
            {

                ScatterSeries UH_UWcrit_Scatter_Series = new ScatterSeries(); // scatter of UW>UWcrit coeffs and P10 expo                
                UH_UWcrit_Scatter_Series.MarkerFill = OxyColors.BlueViolet;
                model.Series.Add(UH_UWcrit_Scatter_Series);

                ScatterSeries UH_DW0_Scatter_Series = new ScatterSeries(); // scatter of DW<0 coeffs and P10 expo                
                UH_DW0_Scatter_Series.MarkerFill = OxyColors.Red;
                model.Series.Add(UH_DW0_Scatter_Series);

                LineSeries UH_Default_Series = new LineSeries(); // line of default model               
                UH_Default_Series.MarkerStroke = OxyColors.Red;
                UH_Default_Series.Title = "Default Uphill model";
                model.Series.Add(UH_Default_Series);

                if (thisModel.isImported == false)
                {
                    UH_DW0_Scatter_Series.Title = "DW < 0";
                    UH_UWcrit_Scatter_Series.Title = "UW > UW crit";
                }
                else
                {
                    UH_DW0_Scatter_Series.Title = "DW < 0 Imported";
                    UH_UWcrit_Scatter_Series.Title = "UW > UW crit Imported";
                }

                if (WD_Ind < numWD)
                {
                    LineSeries UH_Calib_Series = new LineSeries(); // line of default model                    
                    UH_Calib_Series.MarkerStroke = OxyColors.Blue;

                    UH_Calib_Series.Points.Add(new DataPoint(0.1, Math.Round(thisModel.uphill_A[WD_Ind] * Math.Pow(0.1, thisModel.uphill_B[WD_Ind]), 4)));
                    UH_Calib_Series.Points.Add(new DataPoint(100, Math.Round(thisModel.uphill_A[WD_Ind] * Math.Pow(100, thisModel.uphill_B[WD_Ind]), 4)));

                    UH_Calib_Series.Title = "Uphill Site-Calibrated Model";
                    model.Series.Add(UH_Calib_Series);
                }

                for (int i = 0; i < posUW_CoeffCount; i++)
                    UH_UWcrit_Scatter_Series.Points.Add(new ScatterPoint(Math.Round(P10_Expo_PosUW[i], 3), Math.Round(posUW_Coeffs[i], 4)));

                for (int i = 0; i < negDW_CoeffCount; i++)
                    UH_DW0_Scatter_Series.Points.Add(new ScatterPoint(Math.Round(P10_Expo_NegDW[i], 3), Math.Round(negDW_Coeffs[i], 4)));

                UH_Default_Series.Points.Add(new DataPoint(0.1, Math.Round(origModel.uphill_A[0] * Math.Pow(0.1, origModel.uphill_B[0]), 4)));
                UH_Default_Series.Points.Add(new DataPoint(100, Math.Round(origModel.uphill_A[0] * Math.Pow(100, origModel.uphill_B[0]), 4)));

                // Specify axes
                LogarithmicAxis xAxis = new LogarithmicAxis();
                xAxis.Position = AxisPosition.Bottom;
                xAxis.Title = "ln P10 Exposure";
                LogarithmicAxis yAxis = new LogarithmicAxis();
                yAxis.Position = AxisPosition.Left;
                yAxis.Title = "ln Coeff";

                model.Axes.Add(xAxis);
                model.Axes.Add(yAxis);

                model.Title = "ln Coeff vs P10 Exposure for Uphill Flow";
            }
            else if (UW_plot_to_show == "UW < UW crit")
            {
                // Pos UW expo, UW < UW Critical
                ScatterSeries UH_Scatter_Series = new ScatterSeries(); // scatter of UW<UWcrit coeffs and P10 expo

                UH_Scatter_Series.MarkerFill = OxyColors.BlueViolet;
                UH_Scatter_Series.Title = "UW < UW crit";
                model.Series.Add(UH_Scatter_Series);

                LineSeries UH_Default_Series = new LineSeries(); // line of default model                
                UH_Default_Series.MarkerStroke = OxyColors.Red;
                UH_Default_Series.Title = "Default Speed-Up Model";

                UH_Default_Series.Points.Add(new DataPoint(0.1, Math.Round(origModel.spdUp_A[0] * Math.Pow(0.1, origModel.spdUp_B[0]), 4)));
                UH_Default_Series.Points.Add(new DataPoint(100, Math.Round(origModel.spdUp_A[0] * Math.Pow(100, origModel.spdUp_B[0]), 4)));
                model.Series.Add(UH_Default_Series);

                if (WD_Ind < numWD)
                {
                    LineSeries UH_Calib_Series = new LineSeries(); // line of site-calibrated speed-up model                    
                    UH_Calib_Series.MarkerStroke = OxyColors.Blue;
                    UH_Calib_Series.Title = "Site-Calibrated Speed-Up Model";
                    UH_Calib_Series.Points.Add(new DataPoint(0.1, Math.Round(thisModel.spdUp_A[WD_Ind] * Math.Pow(0.1, thisModel.spdUp_B[WD_Ind]), 4)));
                    UH_Calib_Series.Points.Add(new DataPoint(100, Math.Round(thisModel.spdUp_A[WD_Ind] * Math.Pow(100, thisModel.spdUp_B[WD_Ind]), 4)));
                    model.Series.Add(UH_Calib_Series);
                }

                for (int i = 0; i < posUW_CoeffCount; i++)
                    UH_Scatter_Series.Points.Add(new ScatterPoint(Math.Round(P10_Expo_PosUW[i], 3), Math.Round(posUW_Coeffs[i], 3)));

                string Xlabel2 = "ln P10 Exposure";
                string Ylabel2 = "ln Coeff";

                // Specify axes
                LogarithmicAxis xAxis = new LogarithmicAxis();
                xAxis.Position = AxisPosition.Bottom;
                xAxis.Title = Xlabel2;
                LogarithmicAxis yAxis = new LogarithmicAxis();
                yAxis.Position = AxisPosition.Left;
                yAxis.Title = Ylabel2;

                model.Axes.Add(xAxis);
                model.Axes.Add(yAxis);

                model.Title = "Coeff vs. P10 Exposure for Speed-up";

            }

            thisInst.plotUHModel.Refresh();

        }

        /// <summary> Reads the selected radius, WD, Expo/SRDH, UW critical and updates the plots on the Advanced tab to show either the log-log P10 exposure or stability factors on a radar plot. </summary>        
        public void ModelPlots(Continuum thisInst)
        {
            int numModels = thisInst.modelList.ModelCount;
            bool isImported = false;

            if (numModels > 0)
            {
                if (thisInst.modelList.models[0, 0].isImported)
                    isImported = true;
            }
            else if (numModels == 0)
            {
                thisInst.plotDHModel.Model = new PlotModel();
                thisInst.plotUHModel.Model = new PlotModel();
                return;
            }

            int numWD = thisInst.GetNumWD();
            int WD_Ind = thisInst.GetWD_ind("Advanced");
            if (WD_Ind == -1 || numWD == 0)
                return;

            int radiusInd = thisInst.GetRadiusInd("Advanced");
            Met.TOD thisTOD = thisInst.GetSelectedTOD("Advanced");
            Met.Season thisSeason = thisInst.GetSelectedSeason("Advanced");

            Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisTOD, thisSeason,
                thisInst.modeledHeight, false);
            Model thisModel = theseModels[radiusInd];

            string UH_Plot_to_show = "";

            if (thisInst.cboUphill_to_show.SelectedItem != null)
                UH_Plot_to_show = thisInst.cboUphill_to_show.SelectedItem.ToString();
            else
            {
                thisInst.cboUphill_to_show.SelectedIndex = 0;
                UH_Plot_to_show = thisInst.cboUphill_to_show.SelectedItem.ToString();
            }

            string DH_Plot_to_show = "";

            if (thisInst.cboDHplot.SelectedItem != null)
                DH_Plot_to_show = thisInst.cboDHplot.SelectedItem.ToString();
            else
            {
                thisInst.cboDHplot.SelectedIndex = 0;
                DH_Plot_to_show = thisInst.cboDHplot.SelectedItem.ToString();
            }

            if (DH_Plot_to_show == "Separated Flow" && thisInst.topo.useSepMod == false)
            {
                thisInst.cboDHplot.SelectedIndex = 0;
                DH_Plot_to_show = thisInst.cboDHplot.SelectedItem.ToString();
            }

            string Expo_or_Rough = "";

            if (thisInst.cboExpo_or_Stab.SelectedItem != null)
                Expo_or_Rough = thisInst.cboExpo_or_Stab.SelectedItem.ToString();
            else
            {
                thisInst.cboExpo_or_Stab.SelectedIndex = 0;
                return;
            }

            if (Expo_or_Rough == "Exposure")
            {
                DownhillLogLogPlot(thisInst, thisModel, DH_Plot_to_show); // updates scatter and log-log fit of Uphill plot
                UphillLogLogPlot(thisInst, thisModel, UH_Plot_to_show);
            }
            else
            {
                DownhillRoughPlot(thisInst, thisModel);
                UphillRoughPlot(thisInst, thisModel, UH_Plot_to_show);
            }

            double UW_critical = 0;
            double sepCrit = 0;
            double sepCritWS = 0;

            if (WD_Ind < numWD)
            {
                if (thisModel.UW_crit[WD_Ind] != 4 && thisModel.UW_crit[WD_Ind] != 500)
                    UW_critical = thisModel.UW_crit[WD_Ind];

                sepCrit = thisModel.sepCrit[WD_Ind];
                sepCritWS = thisModel.Sep_crit_WS[WD_Ind];
            }
            else if (thisInst.metList.ThisCount > 0)
            {
                UW_critical = thisModel.CalcOverallUWCrit(thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, thisTOD, thisSeason));
                sepCrit = thisModel.CalcOverallSepCrit(thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, thisTOD, thisSeason));
                sepCritWS = thisModel.CalcOverallSepCritWS(thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, thisTOD, thisSeason));
            }

            thisInst.txtUWCrit.Text = "";
            if (UW_critical != 0) thisInst.txtUWCrit.Text = Math.Round(UW_critical, 2).ToString();

            if (thisInst.topo.useSepMod == true) thisInst.txtSepCrit.Text = Math.Round(sepCrit, 1).ToString();
            if (thisInst.topo.useSepMod == true) thisInst.txtSepCritWS.Text = Math.Round(sepCritWS, 1).ToString();

        }

        /// <summary> Returns either ‘Topography’ or ‘Surface roughness’ depending what is selected on ‘Input’ tab. </summary>        
        public string GetSelectedTopoMapParam(Continuum thisInst) // 
        {
            string topoOrRough = thisInst.cboTopo_Or_Roughness.SelectedItem.ToString();

            if (topoOrRough == "Topography" && thisInst.topo.gotTopo == false)
            {
                // no topo data yet
                if (thisInst.topo.gotSR == true)
                {
                    thisInst.cboTopo_Or_Roughness.SelectedIndex = 1;
                    topoOrRough = thisInst.cboTopo_Or_Roughness.SelectedItem.ToString();
                }
            }

            if ((topoOrRough == "Surface Roughness" || topoOrRough == "Displacement height" || topoOrRough == "Land Cover") && thisInst.topo.gotSR == false)
            {
                // no topo data yet
                if (thisInst.topo.gotTopo == true)
                {
                    thisInst.cboTopo_Or_Roughness.SelectedIndex = 0;
                    topoOrRough = thisInst.cboTopo_Or_Roughness.SelectedItem.ToString();
                }
            }

            return topoOrRough;
        }

        /// <summary> Updates map on Input tab to show either elevation, surface roughness, displacement height, or land cover codes. </summary>   
        public void TopoMap(Continuum thisInst)
        {
            string topoOrRough = GetSelectedTopoMapParam(thisInst);
            thisInst.plotTopo.Model = new PlotModel();
            var model = thisInst.plotTopo.Model;

            if (thisInst.topo.gotTopo || thisInst.topo.gotSR == true)
            {
                // Create plot model                
                model.LegendPosition = LegendPosition.RightMiddle;
                model.IsLegendVisible = true;

                double[,] paramToPlot = null;
                // Plot land cover, surface roughness, or displacement height
                if (topoOrRough == "Surface Roughness") paramToPlot = thisInst.topo.GetLC_ParamToPlot("Surface Roughness");
                if (topoOrRough == "Displacement height") paramToPlot = thisInst.topo.GetLC_ParamToPlot("Displacement height");
                if (topoOrRough == "Land Cover") paramToPlot = thisInst.topo.GetLC_ParamToPlot("Land Cover");
                if (topoOrRough == "Topography") paramToPlot = thisInst.topo.topoElevs;

                if (paramToPlot == null)
                    return;

                TopoInfo.Min_Max_Num_XYs theseXYs = new TopoInfo.Min_Max_Num_XYs();

                if (topoOrRough == "Topography")
                {
                    theseXYs = thisInst.topo.topoNumXY;
                    thisInst.txtTopoSource.Text = thisInst.savedParams.topoFileName;
                }
                else
                {
                    theseXYs = thisInst.topo.LC_NumXY;
                    thisInst.txtTopoSource.Text = thisInst.savedParams.landCoverFileName;
                }

                // Create heat map series
                var cs = new HeatMapSeries
                {
                    X0 = theseXYs.X.plot.min,
                    X1 = theseXYs.X.plot.max,
                    Y0 = theseXYs.Y.plot.min,
                    Y1 = theseXYs.Y.plot.max,

                    Data = paramToPlot
                };

                model.Axes.Add(new LinearColorAxis
                {
                    Position = AxisPosition.Right,
                    Palette = OxyPalettes.Jet(500),
                    HighColor = OxyColors.Red,
                    LowColor = OxyColors.Gray,
                    Minimum = thisInst.topo.GetMin(paramToPlot, false),
                    Maximum = thisInst.topo.GetMax(paramToPlot)
                });

                thisInst.plotTopo.Model.Series.Add(cs);

                Labels(thisInst);

                // Refresh plot
                thisInst.plotTopo.Refresh();

            }

        }

        /// <summary> Updates the map on Advanced tab to show the selected start and end met and the nodes in between (if any). </summary> 
        public void StepTopoMap(Continuum thisInst)
        {
            thisInst.plotAdvTopo.Model = new PlotModel();
            var model = thisInst.plotAdvTopo.Model;
            model.IsLegendVisible = false;

            if (thisInst.turbineList.genTimeSeries == true)
            {
                thisInst.plotAdvTopo.Refresh();
                return;
            }

            if (thisInst.topo.gotTopo == true)
            {
                TopoInfo topo = thisInst.topo;
                double thisMin = topo.GetMin(thisInst.topo.topoElevs, true);
                double thisMax = topo.GetMax(thisInst.topo.topoElevs);

                if (thisMin == thisMax)
                {
                    thisMin = thisMin * 0.98f;
                    thisMax = thisMax * 1.02f;
                }

                double minX = 1000000;
                double minY = 1000000;
                double maxX = 0;
                double maxY = 0;

                // Figure out if turbine or met end
                string met1Str = thisInst.GetStartMetAdvanced();
                string endStr = thisInst.GetEndSiteAdvanced();

                Met thisMet = null;
                int metPairCount = thisInst.metPairList.PairCount;
                Pair_Of_Mets thisPair = null;
                int numNodes = 0;
                Nodes thisNode;
                int radiusIndex = thisInst.GetRadiusInd("Advanced");

                if (radiusIndex == -1 || met1Str == "")
                    return;

                int radius = thisInst.radiiList.investItem[radiusIndex].radius;

                for (int i = 0; i <= thisInst.metList.ThisCount - 1; i++)
                {
                    if (thisInst.metList.metItem[i].name == met1Str)
                    {
                        thisMet = thisInst.metList.metItem[i];
                        break;
                    }
                }

                if (thisMet == null)
                    return;

                bool isMetPair = false;

                for (int i = 0; i <= metPairCount - 1; i++)
                {
                    thisPair = thisInst.metPairList.metPairs[i];
                    isMetPair = false;
                    if ((met1Str == thisPair.met1.name && endStr == thisPair.met2.name) || (met1Str == thisPair.met2.name && endStr == thisPair.met1.name))
                    {
                        isMetPair = true;
                        break;
                    }
                }

                if (isMetPair == true)
                {
                    if (thisPair.met1.UTMX < thisPair.met2.UTMX)
                    {
                        minX = thisPair.met1.UTMX;
                        maxX = thisPair.met2.UTMX;
                    }
                    else
                    {
                        minX = thisPair.met2.UTMX;
                        maxX = thisPair.met1.UTMX;
                    }

                    if (thisPair.met1.UTMY < thisPair.met2.UTMY)
                    {
                        minY = thisPair.met1.UTMY;
                        maxY = thisPair.met2.UTMY;
                    }
                    else
                    {
                        minY = thisPair.met2.UTMY;
                        maxY = thisPair.met1.UTMY;
                    }

                    numNodes = thisPair.WS_Pred[0, radiusIndex].nodePath.Length;

                    for (int j = 0; j <= numNodes - 1; j++)
                    {
                        thisNode = thisPair.WS_Pred[0, radiusIndex].nodePath[j]; // path of nodes is same for all UW&DW models (only varies by radius)

                        if (thisNode.UTMX < minX) minX = thisNode.UTMX;
                        if (thisNode.UTMX > maxX) maxX = thisNode.UTMX;
                        if (thisNode.UTMY < minY) minY = thisNode.UTMY;
                        if (thisNode.UTMY > maxY) maxY = thisNode.UTMY;
                    }
                }
                else if (thisInst.turbineList.TurbineCount > 0 && thisInst.turbineList.turbineCalcsDone)
                { // goes to a turbine

                    int turbCount = thisInst.turbineList.TurbineCount;
                    Turbine thisTurb = null;
                    bool foundTurb = false;
                    Turbine endTurb = null;

                    for (int i = 0; i <= turbCount - 1; i++)
                    {
                        thisTurb = thisInst.turbineList.turbineEsts[i];
                        if (thisTurb.name == endStr)
                        {
                            for (int j = 0; j <= thisTurb.WSEst_Count - 1; j++)
                            {
                                if (thisTurb.WS_Estimate[j].predictorMetName == met1Str)
                                {
                                    foundTurb = true;
                                    endTurb = thisTurb;
                                    break;
                                }
                            }
                        }

                        if (foundTurb == true)
                            break;
                    }

                    if (thisMet.UTMX < thisTurb.UTMX)
                    {
                        minX = thisMet.UTMX;
                        maxX = thisTurb.UTMX;
                    }
                    else
                    {
                        minX = thisTurb.UTMX;
                        maxX = thisMet.UTMX;
                    }

                    if (thisMet.UTMY < thisTurb.UTMY)
                    {
                        minY = thisMet.UTMY;
                        maxY = thisTurb.UTMY;
                    }
                    else
                    {
                        minY = thisTurb.UTMY;
                        maxY = thisMet.UTMY;
                    }

                    int WS_PredInd = 0;

                    for (int i = 0; i < thisTurb.WSEst_Count; i++)
                        if (thisTurb.WS_Estimate[i].predictorMetName == thisMet.name && thisTurb.WS_Estimate[i].model.radius == radius)
                        {
                            WS_PredInd = i;
                            break;
                        }

                    if (thisTurb.WS_Estimate[WS_PredInd].pathOfNodes != null)
                    {
                        try
                        {
                            numNodes = thisTurb.WS_Estimate[WS_PredInd].pathOfNodes.Length;
                        }
                        catch
                        {
                            numNodes = 0;
                        }
                    }

                    for (int j = 0; j < numNodes; j++)
                    {
                        thisNode = thisTurb.WS_Estimate[WS_PredInd].pathOfNodes[j];
                        if (thisNode.UTMX < minX) minX = thisNode.UTMX;
                        if (thisNode.UTMX > maxX) maxX = thisNode.UTMX;
                        if (thisNode.UTMY < minY) minY = thisNode.UTMY;
                        if (thisNode.UTMY > maxY) maxY = thisNode.UTMY;
                    }
                }
                else
                { // single met and no turbines
                    minX = thisMet.UTMX;
                    maxX = thisMet.UTMX;
                    minY = thisMet.UTMY;
                    maxY = thisMet.UTMY;
                }

                // Need to figure out closest line of data to change axes  // couldn//t figure out how to get axis to change consistently :(
                minX = (int)(minX - minX * 0.005);
                minY = (int)(minY - minY * 0.0003);
                maxX = (int)(maxX + maxX * 0.005);
                maxY = (int)(maxY + maxY * 0.0003);
                TopoInfo.TopoGrid closestNode = thisInst.topo.GetClosestNode(minX, minY, "Topography");
                minX = closestNode.UTMX;
                minY = closestNode.UTMY;

                closestNode = thisInst.topo.GetClosestNode(maxX, maxY, "Topography");
                maxX = closestNode.UTMX;
                maxY = closestNode.UTMY;

                int numX = (int)((maxX - minX) / (topo.topoNumXY.X.plot.reso));

                if (numX <= 1)
                {
                    numX = 2;
                    maxX = minX + 2 * topo.topoNumXY.X.plot.reso;
                }

                int numY = (int)((maxY - minY) / (topo.topoNumXY.Y.plot.reso));

                if (numY <= 1)
                {
                    numY = 2;
                    maxY = minY + 2 * topo.topoNumXY.Y.plot.reso;
                }

                double[,] elevsToPlot = new double[numX, numY];

                int plot_X_ind = 0;
                int plot_Y_ind = 0;

                for (double i = minX; i <= maxX; i = i + (int)topo.topoNumXY.X.plot.reso)
                {
                    for (double j = minY; j <= maxY; j = j + (int)topo.topoNumXY.Y.plot.reso)
                    {
                        int x_ind = (int)((i - topo.topoNumXY.X.plot.min) / (topo.topoNumXY.X.plot.reso));
                        int y_ind = (int)((j - topo.topoNumXY.Y.plot.min) / (topo.topoNumXY.Y.plot.reso));
                        if (y_ind > topo.topoNumXY.Y.plot.num - 1) y_ind = numY - 1;
                        elevsToPlot[plot_X_ind, plot_Y_ind] = topo.topoElevs[x_ind, y_ind];
                        plot_Y_ind++;
                        if (plot_Y_ind > numY - 1) plot_Y_ind = numY - 1;
                    }

                    if (plot_Y_ind <= numY - 1)
                    {
                        for (int j = plot_Y_ind; j <= numY - 1; j++)
                            elevsToPlot[plot_X_ind, j] = elevsToPlot[plot_X_ind, plot_Y_ind - 1];
                    }

                    plot_X_ind++;
                    if (plot_X_ind > numX - 1) plot_X_ind = numX - 1;
                    plot_Y_ind = 0;
                }

                var topoSurface = new HeatMapSeries
                {
                    X0 = minX,
                    X1 = maxX,
                    Y0 = minY,
                    Y1 = maxY,

                    Data = elevsToPlot
                };

                model.Series.Add(topoSurface);

                model.Axes.Add(new LinearColorAxis
                {
                    Position = AxisPosition.Right,
                    Palette = OxyPalettes.Jet(500),
                    HighColor = OxyColors.Red,
                    LowColor = OxyColors.Gray,
                    Minimum = thisMin,
                    Maximum = thisMax
                });

                StepLabels(thisInst, minX, maxX, minY, maxY);
            }

            thisInst.plotAdvTopo.Refresh();

        }

        /// <summary> Clears all parameters and resets model for a new project. </summary>        
        public void NewProject(Continuum thisInst)
        {
            thisInst.topo.ClearAll(ref thisInst);
            thisInst.metList.ClearAllMets(thisInst, false);
            thisInst.metPairList.ClearAll(); // clears met pairs and round robin ests
            thisInst.turbineList.ClearAllTurbines();
            thisInst.turbineList.ClearAllPowerCurves();
            thisInst.turbineList.ClearAllExceedance();
            thisInst.turbineList.SetExceedCurves();
            thisInst.mapList.ClearAllMaps();
            thisInst.modelList.ClearAll();
            thisInst.wakeModelList.ClearAll();
            thisInst.savedParams.ClearAll();
            thisInst.UTM_conversions.ResetDefaults();
            thisInst.merraList.ClearMERRA();
            thisInst.siteSuitability.ClearAll();
            thisInst.siteSuitability.ClearAllZones();

            thisInst.txtTopoSource.Clear();
            thisInst.txtUTMDatum.Clear();
            thisInst.txtUTMZone.Clear();

            thisInst.modeledHeight = 80;
            thisInst.txtModeledHeight.Text = "80";
            thisInst.metList.isTimeSeries = false;
            thisInst.metList.filteringEnabled = true;
            thisInst.chkDisableFilter.CheckState = CheckState.Checked;

            AllTABs(thisInst);

            ClearSavedParameters(thisInst);

        }

        /// <summary> Clears everything except for Maps and met and turbine sites (clear WS calcs). </summary>
        public void NewModel(Continuum thisInst)
        {
            thisInst.topo.ClearAll(ref thisInst);
            thisInst.metList.ClearAllExposuresAndGridStats();
            thisInst.metPairList.ClearAll(); // clears met pairs and round robin ests
            thisInst.turbineList.ClearAllCalcs();
            thisInst.modelList.ClearAll();
            thisInst.savedParams.ClearAll();

            // Input tab
            TopoMap(thisInst);

            // Turb Est tab
            WS_or_WR_Plot(thisInst);
            PowerCrvPlot(thisInst);
            TurbStats(thisInst);
            GrossTurbEstList(thisInst);

            // Maps tab
            ClearMapsPlotsAndTables(thisInst);
            thisInst.cboStartMet.Text = "";
            thisInst.cboEndMet.Text = "";

            // Uncertainty tab
            thisInst.cboUncertPowerCrv.Items.Clear();
            thisInst.lstTurbUncert.Items.Clear();
            thisInst.lstRR_AllErr.Items.Clear();
            thisInst.lstRR_Results.Items.Clear();
            thisInst.cboRoundRobin.Items.Clear();

            // Advanced tab
            thisInst.lstModCrossPred.Items.Clear();
            thisInst.lstPathNodes.Items.Clear();
            thisInst.chkMetlabelsStep.Items.Clear();
            thisInst.chkTurbLabelStep.Items.Clear();
            thisInst.cboStartMet.Items.Clear();
            thisInst.cboEndMet.Items.Clear();
            thisInst.cboAdvancedWD.Items.Clear();
            StepTopoMap(thisInst);
            PlotAdvancedTable(thisInst);
            ModelPlots(thisInst);
            ModelParams(thisInst);

            ClearSavedParameters(thisInst);

        }

        /// <summary> Clears all saved parameters. </summary>        
        public void ClearSavedParameters(Continuum thisInst)
        {
            thisInst.savedParams.ClearAll();
        }

        /// <summary> Clears textboxes on Gross Turbine Ests tab showing the stats of the estimated WS and AEP. </summary>
        public void ClearStats(Continuum thisInst) // 
        {
            thisInst.txtAvg.Clear();
            thisInst.txtCount.Clear();
            thisInst.txtMax.Clear();
            thisInst.txtMin.Clear();
            thisInst.txtStDev.Clear();
            thisInst.txtAEPAvg.Clear();
            thisInst.txtAEPMax.Clear();
            thisInst.txtAEPMin.Clear();
            thisInst.txtAEPSD.Clear();
        }

        /// <summary> Clears all plots and table on Map tab. </summary>
        public void ClearMapsPlotsAndTables(Continuum thisInst)
        {
            thisInst.lstMaps.Items.Clear();

            thisInst.txtMapAvg.Clear();
            thisInst.txtMapCount.Clear();
            thisInst.txtMapMax.Clear();
            thisInst.txtMapMin.Clear();
            thisInst.txtMapStDev.Clear();

            Generated2DMap(thisInst);

            thisInst.chkMetLabels_Maps.Items.Clear();
            thisInst.chkTurbLabels_Maps.Items.Clear();

        }

        /// <summary> Updates wind rose plot on Input tab. </summary>        
        public void WindRose(Continuum thisInst)
        {
            thisInst.plotInputWindRose.Model = new PlotModel();
            var model = thisInst.plotInputWindRose.Model;
            model.Title = "Wind Rose at Met Sites";
            model.PlotType = PlotType.Polar;
            model.IsLegendVisible = false;
            model.PlotAreaBorderThickness = new OxyThickness(0);

            int numWD = thisInst.GetNumWD();
            Met[] checkedMets = thisInst.GetCheckedMets("Input");
            int metCount = checkedMets.Length;

            if (metCount > 0)
            {
                model.Axes.Add(new AngleAxis
                {
                    StartAngle = 90,
                    EndAngle = -270,
                    Minimum = 0,
                    Maximum = 360,
                    MajorStep = 15
                });

                for (int i = 0; i < metCount; i++)
                {
                    Met.WSWD_Dist thisDist = checkedMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                    var metSeries = new LineSeries();
                    SortedList<double, double> ii = new SortedList<double, double>();

                    for (int WDind = 0; WDind < numWD; WDind++)
                        ii.Add(WDind * 360 / numWD, Math.Round(thisDist.windRose[WDind], 5));

                    ii.Add(360, thisDist.windRose[0]);

                    metSeries.ItemsSource = ii;
                    metSeries.LineStyle = LineStyle.Solid;

                    metSeries.DataFieldX = "Value";
                    metSeries.DataFieldY = "Key";
                    metSeries.Title = checkedMets[i].name;

                    model.Series.Add(metSeries);
                }
            }

            thisInst.plotInputWindRose.Refresh();

        }

        /// <summary> Updates directional WS ratio plot on Input tab. </summary>
        public void DirectionalWS_Ratios(Continuum thisInst)
        {
            thisInst.plotDirectionalWS_Ratios.Model = new PlotModel();
            var model = thisInst.plotDirectionalWS_Ratios.Model;
            model.Title = "Directional WS Ratios at Met Sites";
            model.PlotType = PlotType.Polar;
            model.IsLegendVisible = false;
            model.PlotAreaBorderThickness = new OxyThickness(0);

            Met[] checkedMets = thisInst.GetCheckedMets("Input");

            if (checkedMets == null)
            {
                thisInst.plotDirectionalWS_Ratios.Refresh();
                return;
            }

            if (checkedMets.Length > 0)
            {
                int numWD = thisInst.GetNumWD();
                model.Axes.Add(new AngleAxis
                {
                    StartAngle = 90,
                    EndAngle = -270,
                    Minimum = 0,
                    Maximum = 360,
                    MajorStep = 15
                });

                for (int i = 0; i < checkedMets.Length; i++)
                {
                    Color lineColor = GetMetOrTurbColor(i);
                    Met.WSWD_Dist thisDist = checkedMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                    var metSeries = new LineSeries();
                    SortedList<double, double> ii = new SortedList<double, double>();

                    for (int WDind = 0; WDind < numWD; WDind++)
                        ii.Add(WDind * 360 / numWD, Math.Round(thisDist.sectorWS_Ratio[WDind], 5));

                    ii.Add(360, thisDist.sectorWS_Ratio[0]);

                    metSeries.ItemsSource = ii;
                    metSeries.LineStyle = LineStyle.Solid;

                    metSeries.DataFieldX = "Value";
                    metSeries.DataFieldY = "Key";
                    metSeries.Title = checkedMets[i].name;

                    model.Series.Add(metSeries);
                }

                // Add 1:1 ratio
                var onesSeries = new LineSeries();
                SortedList<double, double> ones = new SortedList<double, double>();

                for (int WDind = 0; WDind < numWD; WDind++)
                    ones.Add(WDind * 360 / numWD, 1.0);

                onesSeries.ItemsSource = ones;
                onesSeries.LineStyle = LineStyle.Dash;

                model.Series.Add(onesSeries);

            }

            thisInst.plotDirectionalWS_Ratios.Refresh();

        }

        /// <summary> Updates tables and plot showing results of Round Robin </summary>        
        public void Uncertainty_TAB_Round_Robin(Continuum thisInst) //  
        {
            thisInst.okToUpdate = false;
            RoundRobinDropdown(thisInst);
            thisInst.okToUpdate = true;
            RoundRobinResults(thisInst);
            RoundRobinIndivResults(thisInst);
            RoundRobinHistogram(thisInst);
        }

        /// <summary>  Updates tables, plots, and textboxes on Map tab. </summary>
        public void MapTAB(Continuum thisInst)
        {
            MapList(thisInst);
            Generated2DMap(thisInst);
            MapStats(thisInst);
        }

        /// <summary> Updates the list on Map tab. </summary>
        public void MapList(Continuum thisInst) // 
        {
            thisInst.lstMaps.Items.Clear();

            if (thisInst.mapList.ThisCount > 0)
            {
                for (int i = 0; i < thisInst.mapList.ThisCount; i++)
                {
                    // if ( map generation was cancelled then remove it from list
                    Map thisMap = thisInst.mapList.mapItem[i];

                    if (thisMap.isComplete == true)
                    {
                        objListItem = thisInst.lstMaps.Items.Add(thisMap.mapName);
                        objListItem.SubItems.Add(thisMap.minUTMX.ToString());
                        objListItem.SubItems.Add(thisMap.minUTMY.ToString());
                        objListItem.SubItems.Add((thisMap.minUTMX + thisMap.numX * thisMap.reso).ToString());
                        objListItem.SubItems.Add((thisMap.minUTMY + thisMap.numY * thisMap.reso).ToString());
                        objListItem.SubItems.Add(thisMap.reso.ToString());
                        objListItem.SubItems.Add(thisMap.useSR.ToString());
                        objListItem.SubItems.Add(thisMap.useFlowSep.ToString());
                        objListItem.SubItems.Add(thisMap.useTimeSeries.ToString());
                    }
                }
                if (thisInst.lstMaps.Items.Count > 0)
                    thisInst.lstMaps.Items[0].Selected = true;
            }
        }

        /// <summary> Updates map on Maps tab </summary>        
        public void Generated2DMap(Continuum thisInst)
        {
            if (thisInst.lstMaps.SelectedItems.Count == 1)
            {
                string mapToPlot = thisInst.lstMaps.SelectedItems[0].Text;
                Map thisMap = new Map();

                for (int j = 0; j < thisInst.mapList.ThisCount; j++)
                {
                    if (mapToPlot == thisInst.mapList.mapItem[j].mapName)
                    {
                        thisMap = thisInst.mapList.mapItem[j];
                        break;
                    }
                }

                thisInst.plotGenMap.Model = new PlotModel();
                var model = thisInst.plotGenMap.Model;
                if (thisMap.modelType == 0 || thisMap.modelType == 1)
                    model.LegendTitle = "Exposure [m]";
                else if (thisMap.modelType == 2 || thisMap.isWaked)
                    model.LegendTitle = "Wind Speed [m/s]";
                else if (thisMap.modelType == 3)
                    model.LegendTitle = "Energy Prod [MWh]";

                double intWidth = 0.0f;
                double thisMin = 0;
                double thisMax = 0;
                int WD_Ind = thisInst.GetWD_ind("Maps");

                int numWD = thisInst.GetNumWD();

                if (thisInst.txtMinValue.Text != "")
                {
                    try
                    {
                        thisMin = Convert.ToSingle(thisInst.txtMinValue.Text);
                    }
                    catch
                    {
                        thisMin = thisMap.FindMin(WD_Ind, numWD);
                        thisInst.txtMinValue.Text = Math.Round(thisMin, 3).ToString();
                    }
                }
                else
                {
                    thisMin = thisMap.FindMin(WD_Ind, numWD);
                    thisInst.txtMinValue.Text = Math.Round(thisMin, 3).ToString();
                }

                if (thisInst.txtMaxValue.Text != "")
                {
                    try
                    {
                        thisMax = Convert.ToSingle(thisInst.txtMaxValue.Text);
                    }
                    catch
                    {
                        thisMax = thisMap.FindMax(WD_Ind, numWD);
                        thisInst.txtMaxValue.Text = Math.Round(thisMax, 3).ToString();
                    }
                }
                else
                {
                    thisMax = thisMap.FindMax(WD_Ind, numWD);
                    thisInst.txtMaxValue.Text = Math.Round(thisMax, 3).ToString();
                }

                if (thisMin == thisMax)
                {
                    thisMin = thisMin - thisMin * 0.1f;
                    thisMax = thisMax + thisMax * 0.1f;
                }

                try
                {
                    intWidth = Convert.ToSingle(thisInst.txtIntLevel.Text);
                }
                catch
                {
                    intWidth = (thisMax - thisMin) / 9;
                    thisInst.txtIntLevel.Text = Math.Round(intWidth, 2).ToString();
                }

                if (thisInst.chkAutoMinMax.Checked == true)
                {
                    thisMin = thisMap.FindMin(WD_Ind, numWD);
                    thisMin = thisMin - thisMin * 0.005f;
                    thisMax = thisMap.FindMax(WD_Ind, numWD);
                    thisMax = thisMax + thisMax * 0.005f;

                    if (thisMap.modelType == 2 || thisMap.modelType == 4 || thisMap.isWaked)
                    {
                        thisMin = Math.Round(thisMin, 2);
                        thisMax = Math.Round(thisMax, 2);

                        if (thisMin == thisMax)
                        {
                            thisMin = thisMin - thisMin * 0.02f;
                            thisMax = thisMax + thisMax * 0.02f;
                        }
                        intWidth = (thisMax - thisMin) / 9;

                    }
                    else if (thisMap.modelType <= 1)
                    {
                        thisMin = Math.Round(thisMin, 1);
                        thisMax = Math.Round(thisMax, 1);

                        if (thisMin == thisMax)
                        {
                            thisMin = thisMin - thisMin * 0.02f;
                            thisMax = thisMax + thisMax * 0.02f;
                        }
                        intWidth = (thisMax - thisMin) / 9;
                    }
                    else
                    {
                        thisMin = Math.Round(thisMin, 0);
                        thisMax = Math.Round(thisMax, 0);

                        if (thisMin == thisMax)
                        {
                            thisMin = thisMin - thisMin * 0.02f;
                            thisMax = thisMax + thisMax * 0.02f;
                        }
                        intWidth = (thisMax - thisMin) / 9;
                    }

                    thisInst.txtMinValue.Text = thisMin.ToString();
                    thisInst.txtMaxValue.Text = thisMax.ToString();
                    thisInst.txtIntLevel.Text = Math.Round(intWidth, 3).ToString();
                }

                if (thisMap.modelType == 2 || thisMap.modelType == 4 || thisMap.isWaked)
                {
                    thisMin = Math.Round(thisMin, 2);
                    thisMax = Math.Round(thisMax, 2);
                }
                else if (thisMap.modelType <= 1)
                {
                    thisMin = Math.Round(thisMin, 1);
                    thisMax = Math.Round(thisMax, 1);
                }
                else
                {
                    thisMin = Math.Round(thisMin, 0);
                    thisMax = Math.Round(thisMax, 0);
                }

                model.Axes.Add(new LinearColorAxis
                {
                    Position = AxisPosition.Right,
                    Palette = OxyPalettes.Jet(50),
                    HighColor = OxyColors.Red,
                    LowColor = OxyColors.Gray,
                    MajorStep = intWidth,
                    Minimum = thisMin,
                    Maximum = thisMax
                });

                int numX = thisMap.numX;
                int numY = thisMap.numY;

                double[,] paramToMap = new double[numX, numY];

                if (WD_Ind == numWD)
                {
                    paramToMap = thisMap.parameterToMap;
                }
                else
                {
                    for (int i = 0; i < numX; i++)
                        for (int j = 0; j < numY; j++)
                            paramToMap[i, j] = thisMap.sectorParamToMap[i, j, WD_Ind];
                }

                var cs = new HeatMapSeries
                {
                    X0 = thisMap.minUTMX,
                    Y0 = thisMap.minUTMY,
                    X1 = thisMap.minUTMX + thisMap.reso * thisMap.numX,
                    Y1 = thisMap.minUTMY + thisMap.reso * thisMap.numY,

                    Data = paramToMap
                };

                model.Series.Add(cs);


                MapLabels(thisInst);
            }

            thisInst.plotGenMap.Refresh();

        }

        /// <summary> Updates textboxes on Map tab showing statistics of selected map. </summary>
        public void MapStats(Continuum thisInst)
        {
            if (thisInst.lstMaps.SelectedItems.Count == 1)
            {
                string Selected_Map = thisInst.lstMaps.SelectedItems[0].Text;

                for (int i = 0; i <= thisInst.mapList.ThisCount - 1; i++)
                {
                    if (Selected_Map == thisInst.mapList.mapItem[i].mapName)
                    {
                        FindMapStats(thisInst, thisInst.mapList.mapItem[i]);
                        break;
                    }
                }
            }
            else if (thisInst.lstMaps.Items.Count == 0)
            {
                thisInst.txtMapAvg.Text = "";
                thisInst.txtMapStDev.Text = "";
                thisInst.txtMapMin.Text = "";
                thisInst.txtMapMax.Text = "";
                thisInst.txtMapCount.Text = "";
                thisInst.txtMap_MetsUsed.Text = "";
            }

        }

        /// <summary> Updates all plots and tables on Advanced tab. </summary>
        public void AdvancedTAB(Continuum thisInst)
        {
            PathNodesList(thisInst); // calls PathNodeListUpdate
            PlotAdvancedTable(thisInst);
            ModelPlots(thisInst);
            ModelParams(thisInst);
            ModCrossPredictions(thisInst);
            StepTopoMap(thisInst);
        }

        /// <summary> Updates everything on Input_tab (except for MetLists and TurbineLists which is done separately). </summary>
        public void InputTAB(Continuum thisInst)
        {
            WindRose(thisInst);
            DirectionalWS_Ratios(thisInst);
            LC_KeySelected(thisInst);
            TopoMap(thisInst);
        }

        /// <summary> Updates everything on Gross Turbine Estimates tab. </summary>
        public void GrossTurbineEstsTAB(Continuum thisInst)
        {
            PowerCurveList(thisInst);
            PowerCrvPlot(thisInst);
            GrossTurbEstList(thisInst);
            GrossHistogram(thisInst);
            TurbStats(thisInst);
            WS_or_WR_Plot(thisInst);
        }

        /// <summary> Updates turbine estimates on Uncertainty tab. </summary>
        public void Uncertainty_TAB_Turbine_Ests(Continuum thisInst)
        {
            TurbUncertEstList(thisInst);
            TurbineUncertPlot(thisInst);
        }

        /// <summary> Updates Maps tab. </summary>
        public void MapsTAB(Continuum thisInst)
        {
            MapList(thisInst);
            MapStats(thisInst);
            Generated2DMap(thisInst);
        }

        /// <summary> Updates all Continuum tabs. </summary>
        public void AllTABs(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            MetList(thisInst);
            TurbineList(thisInst);
            ZoneList(thisInst);
            thisInst.txtTurbineNoise.Text = thisInst.siteSuitability.turbineSound.ToString();
            WindDirectionToDisplay(thisInst);
            RadiusToDisplay("Summary", thisInst);
            RadiusToDisplay("Adv", thisInst);
            MERRA_Dropdowns(thisInst);
            SeasonDropdown(thisInst);
            TOD_Dropdown(thisInst);
            MCP_Settings(thisInst);
            MERRA_Settings(thisInst);
            SetDefaultCheckAdvanced(thisInst);

            ColoredButtons(thisInst);
            ColoredTextBoxes(thisInst);

            thisInst.dateMERRAStart.Value = thisInst.merraList.startDate;
            thisInst.dateMERRAEnd.Value = thisInst.merraList.endDate;

            thisInst.okToUpdate = true;
            InputTAB(thisInst);
            MetDataQC_TAB(thisInst);
            MCP_TAB(thisInst);
            MERRA_TAB(thisInst);
            Met_Turbine_Summary_TAB(thisInst);
            GrossTurbineEstsTAB(thisInst);
            NetTurbineEstsTAB(thisInst);
            Monthly_TAB(thisInst);
            MapsTAB(thisInst);
            Uncertainty_TAB_Round_Robin(thisInst);
            Uncertainty_TAB_Turbine_Ests(thisInst);
            AdvancedTAB(thisInst);
            SiteSuitabilityTAB(thisInst);
            Exceedance_TAB(thisInst);
            TurbulenceIntensityPlotAndTable(thisInst);
            SiteConditionsAlpha(thisInst);
            ExtremeWindSpeed(thisInst);
            InflowAnglePlotAndTable(thisInst);
        }


        /// <summary> Clears land cover database tables. </summary>        
        public void Clear_LandCover_DB(string savedFileName)
        {
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(savedFileName);

            try
            {
                using (var ctx = new Continuum_EDMContainer(connString))
                {
                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE LandCover_table");
                    ctx.SaveChanges();
                }
            }
            catch
            { }
        }

        /// <summary> Clears toporaphy database tables. </summary>   
        public void Clear_Topo_DB(string savedFileName)
        {
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(savedFileName);

            try
            {
                using (var ctx = new Continuum_EDMContainer(connString))
                {
                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE Topo_table");
                    ctx.SaveChanges();
                }
            }
            catch
            { }
        }

        /// <summary> Updates Met Data QC tab. </summary> 
        public void MetDataQC_TAB(Continuum thisInst)
        {
            Met thisMet = thisInst.GetSelectedMet("Met Data QC");

            if (thisMet.metData != null)
            {
                if (thisMet.metData.GetNumAnems() > 0)
                {
                    if (thisMet.metData.anems[0].windData == null)
                        thisMet.metData.GetSensorDataFromDB(thisInst, thisMet.name);

                    if (thisMet.metData.alpha.Length == 0)
                        thisMet.metData.EstimateAlpha();

                    if (thisMet.metData.simData.Length == 0)
                        thisMet.metData.ExtrapolateData(thisInst.modeledHeight);
                }
            }

       //     MetQC_AnemDropdown(thisInst, thisMet);
            MetQCAnemVaneDropdown(thisInst);
            MetQCDates(thisInst, thisMet);
            AlphaVsWD_PlotAndTable(thisInst, thisMet);
            WS_vsHeightPlot(thisInst, thisMet);
            AnemometerSummary(thisInst, thisMet);
            TempSummary(thisInst, thisMet);
            VaneSummary(thisInst, thisMet);
            ExtrapolatedSummary(thisInst, thisMet);
            MetAnemScatterplot(thisInst, thisMet);
            MetWS_DiffvsWD(thisInst, thisMet);
            MetWS_DiffvsWindSpeed(thisInst, thisMet);
            MetWindRoseOrWS_byWDPlot(thisInst, thisMet);
        }

        /// <summary> Updates the Shear alpha vs Wind Direction Plot and Table.  Plots overall, day, and night shear exponents as function of WD. </summary> 
        public void AlphaVsWD_PlotAndTable(Continuum thisInst, Met thisMet)
        {
            Met_Data_Filter thisData = thisMet.metData;
            thisInst.plotAlphaByWD.Model = new PlotModel();
            var model = thisInst.plotAlphaByWD.Model;
            model.IsLegendVisible = false;

            thisInst.lstAlphas.Items.Clear();

            if (thisData == null)
            {
                thisInst.plotAlphaByWD.Refresh();
                return;
            }

            if (thisData.alpha.Length == 0)
            {
                thisInst.plotAlphaByWD.Refresh();
                return;
            }

            double[] Avg_Alpha_WD = thisData.GetAvgAlpha(0, 24, 16);
            double[] Day_Alpha_WD = thisData.GetAvgAlpha(7, 18, 16);
            double[] Night_Alpha_WD = thisData.GetAvgAlpha(19, 6, 16);

            double[] Avg_Alpha = thisData.GetAvgAlpha(0, 24, 1);
            double[] Day_Alpha = thisData.GetAvgAlpha(7, 18, 1);
            double[] Night_Alpha = thisData.GetAvgAlpha(19, 6, 1);

            double[] WD_vals = new double[16];

            for (int i = 0; i < 16; i++)
                WD_vals[i] = i * 22.5;

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Wind Direction, degs";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Shear Exponent";
            yAxis.MajorStep = 0.1;

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            var allHoursSeries = new LineSeries();
            allHoursSeries.MarkerSize = 3;
            allHoursSeries.MarkerStrokeThickness = 1;
            allHoursSeries.MarkerType = MarkerType.Circle;
            allHoursSeries.MarkerFill = OxyColors.Black;
            allHoursSeries.MarkerStroke = OxyColors.Blue;
            allHoursSeries.Title = "All hours";
            model.Series.Add(allHoursSeries);

            var daySeries = new LineSeries();
            daySeries.MarkerSize = 3;
            daySeries.MarkerStrokeThickness = 1;
            daySeries.MarkerType = MarkerType.Circle;
            daySeries.MarkerFill = OxyColors.Black;
            daySeries.MarkerStroke = OxyColors.Green;
            daySeries.Title = "Day";
            model.Series.Add(daySeries);

            var nightSeries = new LineSeries();
            nightSeries.MarkerSize = 3;
            nightSeries.MarkerStrokeThickness = 1;
            nightSeries.MarkerType = MarkerType.Circle;
            nightSeries.MarkerFill = OxyColors.Black;
            nightSeries.MarkerStroke = OxyColors.Red;
            nightSeries.Title = "Night";
            model.Series.Add(nightSeries);

            for (int i = 0; i < 16; i++)
            {
                allHoursSeries.Points.Add(new DataPoint(WD_vals[i], Math.Round(Avg_Alpha_WD[i], 5)));
                daySeries.Points.Add(new DataPoint(WD_vals[i], Math.Round(Day_Alpha_WD[i], 5)));
                nightSeries.Points.Add(new DataPoint(WD_vals[i], Math.Round(Night_Alpha_WD[i], 5)));
            }

            thisInst.lstAlphas.Items.Clear();
            ListViewItem objListItem = new ListViewItem();

            objListItem = thisInst.lstAlphas.Items.Add(Convert.ToString("All"));
            objListItem.SubItems.Add(Convert.ToString(Math.Round(Avg_Alpha[0], 3)));
            objListItem.SubItems.Add(Convert.ToString(Math.Round(Day_Alpha[0], 3)));
            objListItem.SubItems.Add(Convert.ToString(Math.Round(Night_Alpha[0], 3)));

            for (int i = 0; i < 16; i++)
            {
                objListItem = thisInst.lstAlphas.Items.Add(Convert.ToString(Math.Round(WD_vals[i], 1)));
                objListItem.SubItems.Add(Convert.ToString(Math.Round(Avg_Alpha_WD[i], 3)));
                objListItem.SubItems.Add(Convert.ToString(Math.Round(Day_Alpha_WD[i], 3)));
                objListItem.SubItems.Add(Convert.ToString(Math.Round(Night_Alpha_WD[i], 3)));
            }

            thisInst.lstAlphas.Columns[0].Width = 40;
            thisInst.lstAlphas.Columns[1].Width = 40;
            thisInst.lstAlphas.Columns[2].Width = 60;
            thisInst.lstAlphas.Columns[3].Width = 60;

            thisInst.plotAlphaByWD.Refresh();

        }


        /// <summary> Updates the wind speed vs height plot pn Met Data QC tab. </summary>        
        public void WS_vsHeightPlot(Continuum thisInst, Met thisMet)
        {
            Met_Data_Filter thisData = thisMet.metData;

            thisInst.plotWS_vsHeight.Model = new PlotModel();
            var model = thisInst.plotWS_vsHeight.Model;
            model.IsLegendVisible = false;

            if (thisData == null)
            {
                thisInst.plotWS_vsHeight.Refresh();
                return;
            }

            int numAnems = thisData.GetNumAnems();
            int numSim = thisData.GetNumSimData();

            double[] heights = new double[numAnems];
            double[] unfiltWS = new double[numAnems];
            double[] filtWS = new double[numAnems];

            double[] extrapH = new double[numSim];
            double[] extrapWS = new double[numSim];

            int minHeight = 10000;
            int maxHeight = 0;

            int minWS = 100;
            int maxWS = 0;

            for (int i = 0; i < numAnems; i++)
            {
                heights[i] = thisData.anems[i].height;
                unfiltWS[i] = Math.Round(thisData.CalcAvgWS(thisData.anems[i], false), 5);
                filtWS[i] = Math.Round(thisData.CalcAvgWS(thisData.anems[i], true), 5);

                if (heights[i] < minHeight) minHeight = (int)heights[i];
                if (heights[i] > maxHeight) maxHeight = (int)heights[i];

                if (unfiltWS[i] < minWS) minWS = (int)unfiltWS[i];
                if (filtWS[i] < minWS) minWS = (int)filtWS[i];

                if (unfiltWS[i] > maxWS) maxWS = (int)unfiltWS[i];
                if (filtWS[i] > maxWS) maxWS = (int)filtWS[i];
            }

            for (int i = 0; i < numSim; i++)
            {
                extrapH[i] = thisData.simData[i].height;
                extrapWS[i] = Math.Round(thisData.CalcAvgExtrapolatedWS(thisData.simData[i]), 5);

                if (extrapH[i] < minHeight) minHeight = (int)extrapH[i];
                if (extrapH[i] > maxHeight) maxHeight = (int)extrapH[i];
                if (extrapWS[i] < minWS) minWS = (int)extrapWS[i];
                if (extrapWS[i] > maxWS) maxWS = (int)extrapWS[i];
            }

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Wind Speed, m/s";
            xAxis.Minimum = minWS - 1;
            xAxis.Maximum = maxWS + 1;
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Height, m";
            yAxis.MajorStep = 20;
            yAxis.Minimum = minHeight - 10;
            yAxis.Maximum = maxHeight + 10;

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            // Create unfiltered WS series
            var unfilteredSeries = new LineSeries();
            unfilteredSeries.MarkerSize = 3;
            unfilteredSeries.MarkerStrokeThickness = 1;
            unfilteredSeries.MarkerType = MarkerType.Circle;
            unfilteredSeries.MarkerFill = OxyColors.Black;
            unfilteredSeries.MarkerStroke = OxyColors.Red;
            unfilteredSeries.Title = "Unfiltered";

            model.Series.Add(unfilteredSeries);

            if (thisData.filteringDone == true)
            {
                var filteredSeries = new LineSeries();
                filteredSeries.MarkerSize = 3;
                filteredSeries.MarkerStrokeThickness = 1;
                filteredSeries.MarkerType = MarkerType.Circle;
                filteredSeries.MarkerFill = OxyColors.Black;
                filteredSeries.MarkerStroke = OxyColors.Blue;
                filteredSeries.Title = "Filtered";

                for (int i = 0; i < numAnems; i++)
                    filteredSeries.Points.Add(new DataPoint(filtWS[i], heights[i]));

                model.Series.Add(filteredSeries);
            }

            for (int i = 0; i < numAnems; i++)
                unfilteredSeries.Points.Add(new DataPoint(unfiltWS[i], heights[i]));

            if (numSim > 0)
            {
                var extrapolatedSeries = new LineSeries();
                extrapolatedSeries.MarkerSize = 3;
                extrapolatedSeries.MarkerStrokeThickness = 1;
                extrapolatedSeries.MarkerType = MarkerType.Circle;
                extrapolatedSeries.MarkerFill = OxyColors.Black;
                extrapolatedSeries.MarkerStroke = OxyColors.Green;
                extrapolatedSeries.Title = "Extrapolated";

                for (int i = 0; i < numSim; i++)
                    extrapolatedSeries.Points.Add(new DataPoint(extrapWS[i], extrapH[i]));

                model.Series.Add(extrapolatedSeries);
            }

            thisInst.plotWS_vsHeight.Refresh();

        }


        /// <summary> Updates the temperature sensor height and recovery table on Met Data QC tab. </summary>
        public void TempSummary(Continuum thisInst, Met thisMet)
        {
            thisInst.lstTempSummary.Items.Clear();

            if (thisMet.metData == null)
                return;

            foreach (Met_Data_Filter.Temp_Data thisTemp in thisMet.metData.temps)
            {
                objListItem = thisInst.lstTempSummary.Items.Add(Convert.ToString(thisTemp.height));
                objListItem.SubItems.Add(thisMet.metData.CalcTempDataRecovery(thisTemp).ToString("P"));
            }

            thisInst.lstTempSummary.Columns[0].Width = 60;
            thisInst.lstTempSummary.Columns[1].Width = 60;
        }


        /// <summary> Updates the vane summary table including height, icing, and recovery on Met Data QC tab. </summary>
        public void VaneSummary(Continuum thisInst, Met thisMet)
        {
            thisInst.lstVaneSummary.Items.Clear();

            if (thisMet.metData == null)
                return;

            foreach (Met_Data_Filter.Vane_Data thisVane in thisMet.metData.vanes)
            {
                objListItem = thisInst.lstVaneSummary.Items.Add(Convert.ToString(thisVane.height));
                objListItem.SubItems.Add(thisMet.metData.CalcVaneDataRecovery(thisVane, false).ToString("P"));
                if (thisMet.metData.filteringDone == true)
                    objListItem.SubItems.Add(thisMet.metData.CalcPercentVaneFiltered(thisVane, Met_Data_Filter.Filter_Flags.Icing).ToString("P"));

                else
                    objListItem.SubItems.Add(Convert.ToString(""));

                objListItem.SubItems.Add(thisMet.metData.CalcVaneDataRecovery(thisVane, true).ToString("P"));
            }

            thisInst.lstVaneSummary.Columns[0].Width = 40;
            thisInst.lstVaneSummary.Columns[1].Width = 60;
            thisInst.lstVaneSummary.Columns[2].Width = 60;
            thisInst.lstVaneSummary.Columns[3].Width = 60;
        }


        /// <summary> Updates the extrapolated wind speed summary on Met Data QC tab. </summary>        
        public void ExtrapolatedSummary(Continuum thisInst, Met thisMet)
        {
            thisInst.lstExtrapolated.Items.Clear();

            if (thisMet.metData == null)
                return;

            foreach (Met_Data_Filter.Sim_TS thisSim in thisMet.metData.simData)
            {
                objListItem = thisInst.lstExtrapolated.Items.Add(Convert.ToString(thisSim.height));
                objListItem.SubItems.Add(Math.Round(thisMet.metData.CalcAvgExtrapolatedWS(thisSim), 3).ToString());
                objListItem.SubItems.Add(Math.Round(thisMet.metData.CalcExtrapRecovery(thisSim), 4).ToString("P"));
            }

            thisInst.lstExtrapolated.Columns[0].Width = 60;
            thisInst.lstExtrapolated.Columns[1].Width = 55;
        }


        /// <summary> Updates Anemometer summary table with mean WS, % recovery, % flagged (if filtering has been done) on Met Data QC tab. </summary>        
        public void AnemometerSummary(Continuum thisInst, Met thisMet)
        {
            thisInst.lstAnemSummary.Items.Clear();

            if (thisMet.metData == null)
                return;

            foreach (Met_Data_Filter.Anem_Data thisAnem in thisMet.metData.anems)
            {
                objListItem = thisInst.lstAnemSummary.Items.Add(Convert.ToString(thisAnem.height));
                objListItem.SubItems.Add(thisAnem.orientation.ToString());

                objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMet.metData.CalcAvgWS(thisAnem, false), 2)));
                if (thisMet.metData.filteringDone == true)
                    objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMet.metData.CalcAvgWS(thisAnem, true), 2)));
                else
                    objListItem.SubItems.Add("");

                // Data recovery before filtering

                objListItem.SubItems.Add(thisMet.metData.CalcAnemDataRecovery(thisAnem, false).ToString("P"));

                if (thisMet.metData.filteringDone == true)
                {
                    double SD_Filtered = thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.minAnemSD) + thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.maxAnemSD);

                    objListItem.SubItems.Add(thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.Icing).ToString("P"));
                    objListItem.SubItems.Add(thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.towerEffect).ToString("P"));
                    objListItem.SubItems.Add(thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.minWS).ToString("P"));
                    objListItem.SubItems.Add(SD_Filtered.ToString("P"));
                    objListItem.SubItems.Add(thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.maxAnemRange).ToString("P"));
                }
                else
                {
                    objListItem.SubItems.Add(Convert.ToString(""));
                    objListItem.SubItems.Add(Convert.ToString(""));
                    objListItem.SubItems.Add(Convert.ToString(""));
                    objListItem.SubItems.Add(Convert.ToString(""));
                    objListItem.SubItems.Add(Convert.ToString(""));
                }

                // data recovery after filtering
                objListItem.SubItems.Add(thisMet.metData.CalcAnemDataRecovery(thisAnem, true).ToString("P"));
            }

            thisInst.lstAnemSummary.Columns[0].Width = 50;
            thisInst.lstAnemSummary.Columns[1].Width = 30;
            thisInst.lstAnemSummary.Columns[2].Width = 65;
            thisInst.lstAnemSummary.Columns[3].Width = 55;
            thisInst.lstAnemSummary.Columns[4].Width = 50; // % Rec
            thisInst.lstAnemSummary.Columns[5].Width = 50;
            thisInst.lstAnemSummary.Columns[6].Width = 65;
            thisInst.lstAnemSummary.Columns[7].Width = 70;
            thisInst.lstAnemSummary.Columns[8].Width = 72;
            thisInst.lstAnemSummary.Columns[9].Width = 60;
            thisInst.lstAnemSummary.Columns[10].Width = 65;


        }

        /// <summary> Updates the start/end dates of imported dataset and export start/end dates on Met Data QC tab. </summary>
        public void MetQCDates(Continuum thisInst, Met thisMet)
        {
            if (thisMet.metData == null)
                return;

            thisInst.okToUpdate = false;

            if (thisMet.metData.GetNumAnems() > 0)
            {
                thisInst.All_Start_Time.Value = thisMet.metData.allStartDate;
                thisInst.All_End_Time.Value = thisMet.metData.allEndDate;

                thisInst.Start_Time.Value = thisMet.metData.startDate;
                thisInst.End_Time.Value = thisMet.metData.endDate;

                thisInst.Start_Time.Enabled = true;
                thisInst.End_Time.Enabled = true;

                thisInst.Export_Start.Value = thisMet.metData.startDate;
                thisInst.Export_End.Value = thisMet.metData.endDate;
            }

            thisInst.okToUpdate = true;
        } 

        /// <summary> Updates the WS diff vs WD. Plots Anem A - Anem B (filtered or unfiltered depending on dropdown) </summary>
        public void MetWS_DiffvsWD(Continuum thisInst, Met thisMet)
        {
            thisInst.plotWSDiffByWD.Model = new PlotModel();
            var model = thisInst.plotWSDiffByWD.Model;

            // Create label series
            var ms = new ScatterSeries();
            ms.MarkerSize = 2;
            ms.MarkerStrokeThickness = 1;
            ms.MarkerType = MarkerType.Circle;
            ms.MarkerFill = OxyColors.Blue;

            if (thisMet.metData == null)
            {
                thisInst.plotWSDiffByWD.Refresh();
                return;
            }

            if (thisMet.metData.GetNumAnems() == 0)
            {
                thisInst.plotWSDiffByWD.Refresh();
                return;
            }

            string filtOrUnfilt = thisInst.cboFilt_or_Not.SelectedItem.ToString();
       //     int anemHeight = Convert.ToInt16(thisInst.cboSensorHeight.SelectedItem.ToString());
            Met_Data_Filter.Anem_Data anemA = thisInst.GetAnemAorB("A");
            Met_Data_Filter.Anem_Data anemB = thisInst.GetAnemAorB("B");
            Met_Data_Filter.Vane_Data selVane = thisInst.GetSelectedVane();

            // if there isn't a redundant anem then return
            /*     for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                     if (thisMet.metData.anems[i].height == anemHeight)
                         if (thisMet.metData.anems[i].isOnlyMet == true)
                         {
                             thisInst.plotWSDiffByWD.Refresh();
                             return;
                         }
     */
            // get WS ratios and WD
            //      Met_Data_Filter.WS_Ratio_WD_Data Ratios_and_WD = thisMet.metData.GetWS_RatioOrDiffAndWD(anemHeight, filtOrUnfilt, "Diff", false);
            Met_Data_Filter.WS_Ratio_WD_Data Ratios_and_WD = thisMet.metData.GetWS_DiffbyWD_TwoAnems(anemA, anemB, selVane, filtOrUnfilt, false);

            if (Ratios_and_WD.WD == null)
            {
                thisInst.plotWSDiffByWD.Refresh();
                return;
            }

            int numWD = Ratios_and_WD.WD.Length;

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Wind Direction, degs";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "A - B WS Diff, m/s";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            for (int i = 0; i < numWD; i++)
                ms.Points.Add(new ScatterPoint(Math.Round(Ratios_and_WD.WD[i], 3), Math.Round(Ratios_and_WD.WS_Ratio[i], 3)));

            model.Series.Add(ms);

            thisInst.plotWSDiffByWD.Refresh();

        }

        /// <summary> Updates the WS diff vs WS. Plots Anem A - Anem B (filtered or unfiltered depending on dropdown) </summary>
        public void MetWS_DiffvsWindSpeed(Continuum thisInst, Met thisMet)
        {
            thisInst.plotWSDiffByWS.Model = new PlotModel();
            var model = thisInst.plotWSDiffByWS.Model;

            // Create label series
            var ms = new ScatterSeries();
            ms.MarkerSize = 2;
            ms.MarkerStrokeThickness = 1;
            ms.MarkerType = MarkerType.Circle;
            ms.MarkerFill = OxyColors.Blue;

            if (thisMet.metData == null)
            {
                thisInst.plotWSDiffByWS.Refresh();
                return;
            }

            if (thisMet.metData.GetNumAnems() == 0)
            {
                thisInst.plotWSDiffByWS.Refresh();
                return;
            }

            string filtOrUnfilt = thisInst.cboFilt_or_Not.SelectedItem.ToString();
      //      int anemHeight = Convert.ToInt16(thisInst.cboSensorHeight.SelectedItem.ToString());
            Met_Data_Filter.Anem_Data anemA = thisInst.GetAnemAorB("A");
            Met_Data_Filter.Anem_Data anemB = thisInst.GetAnemAorB("B");
           
            // if there isn't a redundant anem then return
            /*      for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                      if (thisMet.metData.anems[i].height == anemHeight)
                          if (thisMet.metData.anems[i].isOnlyMet == true)
                          {
                              thisInst.plotWSDiffByWS.Refresh();
                              return;
                          }
            */
            // get WS ratios and WS
            //     Met_Data_Filter.WS_Ratio_WS_Data Ratios_and_WS = thisMet.metData.GetWS_RatioOrDiffAndWS(anemHeight, filtOrUnfilt, "Diff");
            Met_Data_Filter.WS_Ratio_WS_Data Ratios_and_WS = thisMet.metData.GetWS_DiffAndWS_TwoAnems(anemA, anemB, filtOrUnfilt);

            if (Ratios_and_WS.WS != null && Ratios_and_WS.WS_Ratio != null)
            {
                // Specify axes
                LinearAxis xAxis = new LinearAxis();
                xAxis.Position = AxisPosition.Bottom;
                xAxis.Title = "Avg. WS, m/s";
                LinearAxis yAxis = new LinearAxis();
                yAxis.Position = AxisPosition.Left;
                yAxis.Title = "A - B WS Diff, m/s";

                model.Axes.Add(xAxis);
                model.Axes.Add(yAxis);


                for (int i = 0; i < Ratios_and_WS.WS.Length; i++)
                    ms.Points.Add(new ScatterPoint(Math.Round(Ratios_and_WS.WS[i], 3), Math.Round(Ratios_and_WS.WS_Ratio[i], 3)));

                model.Series.Add(ms);

            }

            thisInst.plotWSDiffByWS.Refresh();

        }

        /// <summary> Updates the wind speed scatterplot. Plots Anem A vs. Anem B (filtered or unfiltered depending on dropdown) on Met Data QC tab. </summary>        
        public void MetAnemScatterplot(Continuum thisInst, Met thisMet)
        {
            thisInst.plotAnemScatter.Model = new PlotModel();
            var model = thisInst.plotAnemScatter.Model;

            if (thisMet.metData == null)
            {
                thisInst.plotAnemScatter.Refresh();
                return;
            }

            if (thisMet.metData.GetNumAnems() == 0)
            {
                thisInst.plotAnemScatter.Refresh();
                return;
            }

            string filtOrUnfilt = thisInst.cboFilt_or_Not.Text;

      /*      if (thisInst.cboSensorHeight.Items.Count == 0)
            {
                thisInst.plotAnemScatter.Refresh();
                return;
            }
*/
     //       int anemHeight = Convert.ToInt16(thisInst.cboSensorHeight.SelectedItem.ToString());
            Met_Data_Filter.Anem_Data anemA = thisInst.GetAnemAorB("A");
            Met_Data_Filter.Anem_Data anemB = thisInst.GetAnemAorB("B");

            if (anemA.height == 0 || anemB.height == 0)
                return;

      /*      // if there isn't a redundant anem then return
            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == anemHeight)
                    if (thisMet.metData.anems[i].isOnlyMet == true)
                    {
                        thisInst.plotAnemScatter.Refresh();
                        return;
                    }
      */
            // get concurrent wind speed data
            Met_Data_Filter.Conc_WS_Data concWS = thisMet.metData.GetConcWS(0, filtOrUnfilt, thisInst, thisMet.name, anemA, anemB);

            if (concWS.anemA_WS != null && concWS.anemB_WS != null)
            {
                // Specify axes
                LinearAxis xAxis = new LinearAxis();
                xAxis.Position = AxisPosition.Bottom;
                xAxis.Title = anemA.height + " m A WS, m/s";
                LinearAxis yAxis = new LinearAxis();
                yAxis.Position = AxisPosition.Left;
                yAxis.Title = anemB.height + " m B WS, m/s";

                model.Axes.Add(xAxis);
                model.Axes.Add(yAxis);

                var ms = new ScatterSeries();
                ms.MarkerSize = 2;
                ms.MarkerStrokeThickness = 1;
                ms.MarkerType = MarkerType.Circle;
                ms.MarkerFill = OxyColors.Blue;
                ms.MarkerStroke = OxyColors.Blue;

                for (int i = 0; i < concWS.anemA_WS.Length; i++)
                    ms.Points.Add(new ScatterPoint(Math.Round(concWS.anemA_WS[i], 3), Math.Round(concWS.anemB_WS[i], 3)));

                model.Series.Add(ms);
            }

            thisInst.plotAnemScatter.Refresh();

        }


        /// <summary> Updates the wind rose or wind speed by wind direction plot on Met Data QC tab. </summary>        
        public void MetWindRoseOrWS_byWDPlot(Continuum thisInst, Met thisMet)
        {
            thisInst.plotMetQC_WindRose.Model = new PlotModel();
            var model = thisInst.plotMetQC_WindRose.Model;
            model.PlotType = PlotType.Polar;
            model.IsLegendVisible = false;
            model.PlotAreaBorderThickness = new OxyThickness(0);

            model.Axes.Add(new AngleAxis
            {
                StartAngle = 90,
                EndAngle = -270,
                Minimum = 0,
                Maximum = 360,
                MajorStep = 15
            });

            Met_Data_Filter thisData = thisMet.metData;
            if (thisMet.metData == null)
            {
                thisInst.plotMetQC_WindRose.Refresh();
                return;
            }

            if (thisData.GetNumAnems() == 0)
            {
                thisInst.plotMetQC_WindRose.Refresh();
                return;
            }

      //      int anemHeight = Convert.ToInt16(thisInst.cboSensorHeight.SelectedItem.ToString());
      //      int vaneInd = thisData.GetVaneClosestToHH(anemHeight);
            Met_Data_Filter.Vane_Data selVane = thisInst.GetSelectedVane();
            Met_Data_Filter.Anem_Data anemA = thisInst.GetAnemAorB("A");

            double[] thisWR_orWS;

            string filtOrNotFilt = thisInst.cboFilt_or_Not.SelectedItem.ToString();

            if (thisInst.cboMetWindRose.SelectedItem.ToString() == "Wind Rose")
            {
                thisWR_orWS = thisData.Calc_Wind_Rose(thisData.startDate, thisData.endDate, selVane, filtOrNotFilt);
                int numWD = thisWR_orWS.Length;

                var metSeries = new LineSeries();
                SortedList<double, double> ii = new SortedList<double, double>();

                for (int WDind = 0; WDind < numWD; WDind++)
                    ii.Add(WDind * 360 / numWD, Math.Round(thisWR_orWS[WDind], 3));

                ii.Add(360, Math.Round(thisWR_orWS[0], 3));

                metSeries.ItemsSource = ii;
                metSeries.LineStyle = LineStyle.Solid;

                metSeries.DataFieldX = "Value";
                metSeries.DataFieldY = "Key";

                model.Series.Add(metSeries);
            }
            else
            {
           //     for (int i = 0; i < thisData.GetNumAnems(); i++)
           //     {
           //         if (thisData.anems[i].height == anemHeight)
           //         {
                        thisWR_orWS = thisData.Calc_Avg_WS_by_WD(thisData.startDate, thisData.endDate, anemA, selVane, filtOrNotFilt);
                        int numWD = thisWR_orWS.Length;
                        string Series_Name = "Anem " + anemA.height + " " + anemA.orientation;

                        var metSeries = new LineSeries();
                        SortedList<double, double> ii = new SortedList<double, double>();

                        for (int WDind = 0; WDind < numWD; WDind++)
                            ii.Add(WDind * 360 / numWD, Math.Round(thisWR_orWS[WDind], 4));

                        ii.Add(360, Math.Round(thisWR_orWS[0], 5));

                        metSeries.ItemsSource = ii;
                        metSeries.LineStyle = LineStyle.Solid;
                        metSeries.Title = Series_Name;
                        metSeries.DataFieldX = "Value";
                        metSeries.DataFieldY = "Key";

                        model.Series.Add(metSeries);
           //             break;
           //         }
           //     }
            }

            thisInst.plotMetQC_WindRose.Refresh();

        }

        /// <summary> Updates the text boxes on MCP tab. </summary>        
        public void MCP_TextBoxes(Continuum thisInst)
        {
            int WD_Ind = thisInst.GetWD_ind("MCP");
            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;

            if (thisMCP == null)
            {
                thisInst.txtRefAvgWS.Text = "";
                thisInst.txtTargAvgWS.Text = "";
                thisInst.txtAvgRatio.Text = "";
                thisInst.txtRef_LT_WS.Text = "";
                thisInst.txtTarg_LT_WS.Text = "";
                thisInst.txtLTratio.Text = "";
                thisInst.txtDataCount.Text = "";
                thisInst.txtSlope.Text = "";
                thisInst.txtIntercept.Text = "";
                thisInst.txtRsq.Text = "";
                thisInst.txtNumYrsRef.Text = "";
                thisInst.txtNumYrsTarg.Text = "";
                thisInst.txtNumYrsConc.Text = "";
                return;
            }

            if (thisMCP.HaveMCP_Estimate("Any") == false)
            {
                thisInst.txtRefAvgWS.Text = "";
                thisInst.txtTargAvgWS.Text = "";
                thisInst.txtAvgRatio.Text = "";
                thisInst.txtRef_LT_WS.Text = "";
                thisInst.txtTarg_LT_WS.Text = "";
                thisInst.txtLTratio.Text = "";
                thisInst.txtDataCount.Text = "";
                thisInst.txtSlope.Text = "";
                thisInst.txtIntercept.Text = "";
                thisInst.txtRsq.Text = "";
                thisInst.txtNumYrsRef.Text = "";
                thisInst.txtNumYrsTarg.Text = "";
                thisInst.txtNumYrsConc.Text = "";
                return;
            }

            if (thisMet.mcp.refData == null)
            {
                UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                MERRA thisMERRA = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
                thisMet.mcp.refData = thisMet.mcp.GetRefData(thisMERRA, ref thisMet, thisInst);
            }

            if (thisMet.mcp.targetData == null)
                thisMet.mcp.targetData = thisMet.mcp.GetTargetData(thisInst.modeledHeight, thisMet);

            string MCP_method = thisInst.Get_MCP_Method();

            if (thisMet.mcp.concData.Length == 0)
                thisMet.mcp.FindConcurrentData(thisMet.mcp.GetStartOrEndDate("Concurrent", "Start"), thisMet.mcp.GetStartOrEndDate("Concurrent", "End"));

            int Num_WD = thisInst.metList.numWD;
            Met.TOD thisTOD = thisInst.GetSelectedTOD("MCP");
            Met.Season thisSeason = thisInst.GetSelectedSeason("MCP");

            double avgRef = 0;
            double avgTarg = 0;

            // Update Num. Yrs text boxes 
            if (thisMCP.refData.Length > 0)
            {
                double num_yrs = thisMet.mcp.refData.Length / 365.0 / 24.0;
                thisInst.txtNumYrsRef.Text = Convert.ToString(Math.Round(num_yrs, 2));
            }
            else
                thisInst.txtNumYrsRef.Text = "";

            if (thisMCP.targetData.Length > 0)
            {
                int This_length = thisMet.mcp.targetData.Length;
                double num_yrs = This_length / 8760.0;
                thisInst.txtNumYrsTarg.Text = Convert.ToString(Math.Round(num_yrs, 2));
            }
            else
                thisInst.txtNumYrsTarg.Text = "";

            if (thisMCP.concData.Length > 0)
            {
                int This_length = thisMCP.concData.Length;
                double num_yrs = thisMCP.concData.Length / 8760.0;
                thisInst.txtNumYrsConc.Text = Convert.ToString(Math.Round(num_yrs, 2));
            }
            else
                thisInst.txtNumYrsConc.Text = "";

            // Update WS and WS Ratio text boxes
            double minWD;
            double maxWD;

            if (WD_Ind == Num_WD || (WD_Ind == 0 && Num_WD == 1))
            {
                minWD = 0;
                maxWD = 360;
            }
            else
            {
                minWD = WD_Ind * 360.0 / Num_WD - 360.0 / Num_WD / 2;
                if (minWD < 0)
                    minWD = minWD + 360;
                maxWD = WD_Ind * 360.0 / Num_WD + 360.0 / Num_WD / 2;
                if (maxWD > 360)
                    maxWD = maxWD - 360;
            }

            Stats stat = new Stats();
            if (thisMCP.refData.Length > 0)
            {
                avgRef = stat.CalcAvgWS(thisMet.mcp.refData, thisMCP.GetStartOrEndDate("Reference", "Start"), thisMCP.GetStartOrEndDate("Reference", "End"), minWD, maxWD, thisTOD, thisSeason, thisInst.metList);
                thisInst.txtRef_LT_WS.Text = Convert.ToString(Math.Round(avgRef, 2));
            }
            else
                thisInst.txtRef_LT_WS.Text = "";

            if (thisMCP.concData.Length > 0)
            {
                double[] thisConc = thisMCP.GetConcAvgsCount(WD_Ind, thisTOD, thisSeason);

                avgTarg = thisConc[0];
                avgRef = thisConc[1];
                double Avg_Ratio = avgTarg / avgRef;

                thisInst.txtRefAvgWS.Text = Convert.ToString(Math.Round(avgRef, 2));
                thisInst.txtTargAvgWS.Text = Convert.ToString(Math.Round(avgTarg, 2));
                thisInst.txtAvgRatio.Text = Convert.ToString(Math.Round(Avg_Ratio, 2));
                thisInst.txtDataCount.Text = Convert.ToString(thisConc[2]);
            }
            else
            {
                thisInst.txtRefAvgWS.Text = "";
                thisInst.txtTargAvgWS.Text = "";
                thisInst.txtAvgRatio.Text = "";
                thisInst.txtDataCount.Text = "";
            }

            if (thisMCP.MCP_Ortho.allR_Sq != 0)
            {
                double slope = 0;
                double intercept = 0;
                double Rsq = 0;
                if (WD_Ind == Num_WD && Num_WD != 1)
                {
                    slope = thisMCP.MCP_Ortho.allSlope;
                    intercept = thisMCP.MCP_Ortho.allIntercept;
                    Rsq = thisMCP.MCP_Ortho.allR_Sq;
                }
                else
                {
                    int TODind = thisMet.GetTOD_Ind(thisMCP.numTODs, thisTOD);
                    int seasonInd = thisMet.GetSeasonInd(thisMCP.numSeasons, thisSeason);
                    int thisWDInd = WD_Ind;
                    if (Num_WD == 1) thisWDInd = 0;
                    slope = thisMCP.MCP_Ortho.slope[thisWDInd, TODind, seasonInd];
                    intercept = thisMCP.MCP_Ortho.intercept[thisWDInd, TODind, seasonInd];
                    Rsq = thisMCP.MCP_Ortho.R_sq[thisWDInd, TODind, seasonInd];
                }

                thisInst.txtSlope.Text = Convert.ToString(Math.Round(slope, 3));
                thisInst.txtIntercept.Text = Convert.ToString(Math.Round(intercept, 3));
                thisInst.txtRsq.Text = Convert.ToString(Math.Round(Rsq, 3));
            }
            else
            {
                thisInst.txtSlope.Text = "";
                thisInst.txtIntercept.Text = "";
                thisInst.txtRsq.Text = "";
            }

            if (thisMCP.MCP_Varrat.allR_Sq != 0)
            {
                double slope = 0;
                double intercept = 0;
                double Rsq = 0;
                if (WD_Ind == Num_WD && Num_WD != 1)
                {
                    slope = thisMCP.MCP_Varrat.allSlope;
                    intercept = thisMCP.MCP_Varrat.allIntercept;
                    Rsq = thisMCP.MCP_Varrat.allR_Sq;
                }
                else
                {
                    int TODind = thisMet.GetTOD_Ind(thisMCP.numTODs, thisTOD);
                    int seasonInd = thisMet.GetSeasonInd(thisMCP.numSeasons, thisSeason);
                    int thisWDInd = WD_Ind;
                    if (Num_WD == 1) thisWDInd = 0;
                    slope = thisMCP.MCP_Varrat.slope[thisWDInd, TODind, seasonInd];
                    intercept = thisMCP.MCP_Varrat.intercept[thisWDInd, TODind, seasonInd];
                    Rsq = thisMCP.MCP_Varrat.R_sq[thisWDInd, TODind, seasonInd];
                }

                thisInst.txtSlope.Text = Convert.ToString(Math.Round(slope, 3));
                thisInst.txtIntercept.Text = Convert.ToString(Math.Round(intercept, 3));
                thisInst.txtRsq.Text = Convert.ToString(Math.Round(Rsq, 3));
            }
            else if (thisMCP.MCP_Ortho.allR_Sq == 0)
            {
                thisInst.txtSlope.Text = "";
                thisInst.txtIntercept.Text = "";
                thisInst.txtRsq.Text = "";
            }

            if (thisMet.mcp.LT_WS_Ests.Length == 0)
                thisMet.mcp.LT_WS_Ests = thisMet.mcp.GenerateLT_WS_TS(thisInst, thisMet, MCP_method);

            if (thisMet.mcp.LT_WS_Ests.Length != 0)
            {
                avgRef = stat.CalcAvgWS(thisMet.mcp.refData, thisMCP.GetStartOrEndDate("Reference", "Start"), thisMCP.GetStartOrEndDate("Reference", "End"), minWD, maxWD, thisTOD, thisSeason, thisInst.metList);
                double Avg_Target_LT = stat.CalcAvgWS(thisMet.mcp.LT_WS_Ests, thisMCP.GetStartOrEndDate("Reference", "Start"), thisMCP.GetStartOrEndDate("Reference", "End"), minWD, maxWD, thisTOD, thisSeason, thisInst.metList);
                double Avg_Ratio = Avg_Target_LT / avgRef;
                thisInst.txtTarg_LT_WS.Text = Convert.ToString(Math.Round(Avg_Target_LT, 2));
                thisInst.txtLTratio.Text = Convert.ToString(Math.Round(Avg_Ratio, 2));
            }
            else
            {
                thisInst.txtTarg_LT_WS.Text = "";
                thisInst.txtLTratio.Text = "";
            }

        }


        /// <summary> Updates dates for concurrent period used in MCP and dates used in export. </summary>        
        public void MCPDates(Continuum thisInst)
        {
            thisInst.okToUpdate = false;

            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;

            if (thisMCP == null)
            {
                thisInst.okToUpdate = true;
                return;
            }

            DateTime concStart = thisMCP.GetStartOrEndDate("Concurrent", "Start");
            DateTime concEnd = thisMCP.GetStartOrEndDate("Concurrent", "End");

            if (thisMCP.concData.Length > 0 && (thisInst.dateConcurrentStart.Value != concStart))
                thisInst.dateConcurrentStart.Value = concStart;
            else
                thisInst.dateConcurrentStart.Value = thisMet.metData.startDate;

            if (thisMCP.concData.Length > 0 && (thisInst.dateConcurrentEnd.Value != concEnd))
                thisInst.dateConcurrentEnd.Value = concEnd;
            else
                thisInst.dateConcurrentEnd.Value = thisMet.metData.endDate;

            if (thisMCP.refData.Length > 0)
            {
                thisInst.dateMCPExportStart.Value = thisMCP.GetStartOrEndDate("Reference", "Start");
                thisInst.dateMCPExportEnd.Value = thisMCP.GetStartOrEndDate("Reference", "End");
            }

            thisInst.okToUpdate = true;
        }

        /// <summary> Updates the MCP settings on MCP tab. </summary>  
        public void MCP_Settings(Continuum thisInst)
        {
            thisInst.okToUpdate = false;

            if (thisInst.metList.numTOD == 1)
                thisInst.cboMCPNumHours.SelectedIndex = 0;
            else
                thisInst.cboMCPNumHours.SelectedIndex = 1;

            if (thisInst.metList.numSeason == 1)
                thisInst.cboMCPNumSeasons.SelectedIndex = 0;
            else
                thisInst.cboMCPNumSeasons.SelectedIndex = 1;

            thisInst.txtWS_bin_width.Text = Math.Round(thisInst.metList.mcpWS_BinWidth, 2).ToString();
            thisInst.txtWS_PDF_Wgt.Text = Math.Round(thisInst.metList.mcpMatrixWgt, 2).ToString();
            thisInst.txtLast_WS_Wgt.Text = Math.Round(thisInst.metList.mcpLastWS_Wgt, 2).ToString();

            if (thisInst.metList.isTimeSeries == false && thisInst.metList.ThisCount > 0)
            {
                thisInst.cboMCPNumWD.Enabled = false;
                thisInst.cboMCPNumSeasons.Enabled = false;
                thisInst.cboMCPNumHours.Enabled = false;

                for (int i = 0; i < thisInst.cboMCPNumWD.Items.Count; i++)
                    if (thisInst.cboMCPNumWD.Items[i].ToString() == thisInst.metList.numWD.ToString())
                    {
                        thisInst.cboMCPNumWD.SelectedIndex = i;
                        break;
                    }

                thisInst.cboMCPNumSeasons.SelectedIndex = 0;
                thisInst.cboMCPNumHours.SelectedIndex = 0;
            }
            else
            {
                thisInst.cboMCPNumWD.Enabled = true;
                thisInst.cboMCPNumSeasons.Enabled = true;
                thisInst.cboMCPNumHours.Enabled = true;
            }

            if (thisInst.Get_MCP_Method() == "Orth. Regression" || thisInst.Get_MCP_Method() == "Variance Ratio")
            {
                thisInst.txtWS_bin_width.Enabled = false;
                thisInst.txtWS_PDF_Wgt.Enabled = false;
                thisInst.txtLast_WS_Wgt.Enabled = false;
            }
            else if (thisInst.Get_MCP_Method() == "Method of Bins")
            {
                thisInst.txtWS_bin_width.Enabled = true;
                thisInst.txtWS_PDF_Wgt.Enabled = false;
                thisInst.txtLast_WS_Wgt.Enabled = false;
            }
            else // Matrix method
            {
                thisInst.txtWS_bin_width.Enabled = true;
                thisInst.txtWS_PDF_Wgt.Enabled = true;
                thisInst.txtLast_WS_Wgt.Enabled = true;
            }

            thisInst.okToUpdate = true;
        }


        /// <summary> Enables or disables MCP buttons based on what analysis has been done. </summary>        
        public void MCP_Buttons(Continuum thisInst)
        {
            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;
            string selectedMethod = thisInst.Get_MCP_Method();

            if (thisMCP == null)
            {
                thisInst.btnExportMCP_TS.Enabled = false;
                thisInst.btnExportMCP_TAB.Enabled = false;
                thisInst.btnExportBinRatios.Enabled = false;
                thisInst.btnExportMCPUncert.Enabled = false;
                thisInst.btnMCP_Uncert.Enabled = false;

                bool gotThisMERRA = false;
                // Check to see if have MERRA data
                if (thisMet.name != null)
                {
                    UTM_conversion.Lat_Long metLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                    gotThisMERRA = thisInst.merraList.GotMERRA(metLL.latitude, metLL.longitude);
                }

                if (gotThisMERRA == false)
                {
                    thisInst.btnDoMCP.BackColor = Color.LightCoral;
                    thisInst.btnDoMCP.Enabled = false;
                }
                else if (gotThisMERRA == true)
                {
                    thisInst.btnDoMCP.BackColor = Color.LightCoral;
                    thisInst.btnDoMCP.Enabled = true;
                }
                return;
            }

            if (thisMCP.HaveMCP_Estimate(selectedMethod) == true)
            {
                thisInst.btnExportMCP_TS.Enabled = true;
                thisInst.btnExportMCP_TAB.Enabled = true;
            }
            else
            {
                thisInst.btnExportMCP_TS.Enabled = false;
                thisInst.btnExportMCP_TAB.Enabled = false;
            }

            if (selectedMethod == "Method of Bins" && thisMCP.HaveMCP_Estimate(selectedMethod) == true)
                thisInst.btnExportBinRatios.Enabled = true;
            else
                thisInst.btnExportBinRatios.Enabled = false;

            if ((selectedMethod == "Orth. Regression" && thisMCP.uncertOrtho.Length > 0) ||
                (selectedMethod == "Method of Bins" && thisMCP.uncertBins.Length > 0) ||
                (selectedMethod == "Variance Ratio" && thisMCP.uncertVarrat.Length > 0) ||
                (selectedMethod == "Matrix" && thisMCP.uncertMatrix.Length > 0))
                thisInst.btnExportMCPUncert.Enabled = true;
            else
                thisInst.btnExportMCPUncert.Enabled = false;

            if ((selectedMethod == "Orth. Regression" && thisMCP.uncertOrtho.Length > 0) || (selectedMethod == "Method of Bins" && thisMCP.uncertBins.Length > 0)
                || (selectedMethod == "Variance Ratio" && thisMCP.uncertVarrat.Length > 0) || (selectedMethod == "Matrix" && thisMCP.uncertMatrix.Length > 0))
                thisInst.btnMCP_Uncert.Enabled = false;
            else
                thisInst.btnMCP_Uncert.Enabled = true;

            bool gotMERRA = false;
            // Check to see if have MERRA data
            if (thisMet.name != null)
            {
                UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                gotMERRA = thisInst.merraList.GotMERRA(theseLL.latitude, theseLL.longitude);
            }

            if (thisMet.name == "")
            {
                thisInst.btnDoMCP.BackColor = Color.LightCoral;
                thisInst.btnDoMCP.Enabled = false;
            }
            else if (thisMet.mcp == null && gotMERRA == false)
            {
                thisInst.btnDoMCP.BackColor = Color.LightCoral;
                thisInst.btnDoMCP.Enabled = false;
            }
            else if (thisMet.mcp == null && gotMERRA == true)
            {
                thisInst.btnDoMCP.BackColor = Color.LightCoral;
                thisInst.btnDoMCP.Enabled = true;
            }
            else if (thisMet.mcp.HaveMCP_Estimate(selectedMethod) == true)
            {
                thisInst.btnDoMCP.BackColor = Color.MediumSeaGreen;
                thisInst.btnDoMCP.Enabled = false;
            }

        }


        /// <summary> Updates the scatterplot showing target versus reference wind speed on the MCP tab. </summary>        
        public void MCP_Scatterplot(Continuum thisInst)
        {
            Met thisMet = thisInst.GetSelectedMet("MCP");

            if (thisMet.metData == null)
                return;

            if (thisMet.metData.GetNumAnems() == 0)
                return;

            if (thisMet.metData.anems[0].windData == null)
                thisMet.metData.GetSensorDataFromDB(thisInst, thisMet.name);

            MCP thisMCP = thisMet.mcp;

            int WD_Ind = thisInst.GetWD_ind("MCP");
            Met.TOD thisTOD = thisInst.GetSelectedTOD("MCP");
            Met.Season thisSeason = thisInst.GetSelectedSeason("MCP");

            thisInst.plotMCP.Model = new PlotModel();
            var model = thisInst.plotMCP.Model;
            model.IsLegendVisible = false;

            if (thisMCP == null)
            {
                thisInst.plotMCP.Refresh();
                return;
            }

            if (thisMCP.HaveMCP_Estimate("Any") == false)
            {
                thisInst.plotMCP.Refresh();
                return;
            }

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "MERRA2 50 m Wind Speed, m/s";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = thisMet.name + " " + thisInst.modeledHeight + " Wind Speed, m/s";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            if (thisMCP.refData.Length == 0)
            {
                UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                MERRA thisMERRA = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
                thisMCP.refData = thisMCP.GetRefData(thisMERRA, ref thisMet, thisInst);
            }

            if (thisMCP.targetData.Length == 0)
                thisMCP.targetData = thisMCP.GetTargetData(thisInst.modeledHeight, thisMet);

            if (thisMCP.concData.Length == 0) thisMCP.FindConcurrentData(thisMCP.GetStartOrEndDate("Concurrent", "Start"), thisMCP.GetStartOrEndDate("Concurrent", "End"));

            if (thisMCP.concData.Length > 0)
            {
                // Create label series
                var MCP_DataSeries = new ScatterSeries();
                MCP_DataSeries.MarkerSize = 3;
                model.Series.Add(MCP_DataSeries);

                double[] thisRefWS = thisMCP.GetConcWS_Array("Ref", WD_Ind, thisTOD, thisSeason, 0, 30, false);
                double[] thisTargWS = thisMCP.GetConcWS_Array("Target", WD_Ind, thisTOD, thisSeason, 0, 30, false);

                if (thisRefWS != null)
                {
                    for (int i = 0; i < thisRefWS.Length; i++)
                        MCP_DataSeries.Points.Add(new ScatterPoint(Math.Round(thisRefWS[i], 3), Math.Round(thisTargWS[i], 3)));
                }

                if ((thisMCP.MCP_Ortho.slope != null) && (thisInst.Get_MCP_Method() == "Orth. Regression"))
                {
                    var SlopeSeries = new LineSeries();
                    SlopeSeries.Title = "Ortho. Reg.";
                    model.Series.Add(SlopeSeries);

                    int maxWS = 0;

                    for (int i = 0; i < thisRefWS.Length; i++)
                        if (thisRefWS[i] > maxWS) maxWS = Convert.ToInt32(thisRefWS[i]);

                    double[] Ortho_Y = null;
                    double[] Ortho_X = null;

                    Array.Resize(ref Ortho_Y, maxWS + 5);
                    Array.Resize(ref Ortho_X, maxWS + 5);

                    for (int i = 0; i < maxWS + 5; i++)
                    {
                        Ortho_X[i] = i;
                        double slope;
                        double intercept;

                        if (WD_Ind == thisMCP.numWD || (WD_Ind == 0 && thisMCP.numWD == 1))
                        {
                            slope = thisMCP.MCP_Ortho.allSlope;
                            intercept = thisMCP.MCP_Ortho.allIntercept;
                        }
                        else
                        {
                            int TODind = thisMet.GetTOD_Ind(thisMCP.numTODs, thisTOD);
                            int seasonInd = thisMet.GetSeasonInd(thisMCP.numSeasons, thisSeason);
                            slope = thisMCP.MCP_Ortho.slope[WD_Ind, TODind, seasonInd];
                            intercept = thisMCP.MCP_Ortho.intercept[WD_Ind, TODind, seasonInd];
                        }

                        Ortho_Y[i] = slope * i + intercept;
                        SlopeSeries.Points.Add(new DataPoint(Ortho_X[i], Ortho_Y[i]));

                    }
                }

                else if ((thisMCP.MCP_Varrat.slope != null) && (thisInst.Get_MCP_Method() == "Variance Ratio"))
                {
                    var SlopeSeries = new LineSeries();
                    SlopeSeries.Title = "Variance Ratio";
                    model.Series.Add(SlopeSeries);

                    int maxWS = 0;

                    for (int i = 0; i < thisRefWS.Length; i++)
                        if (thisRefWS[i] > maxWS) maxWS = Convert.ToInt32(thisRefWS[i]);

                    double[] Varrat_Y = null;
                    double[] Varrat_X = null;

                    Array.Resize(ref Varrat_Y, maxWS + 5);
                    Array.Resize(ref Varrat_X, maxWS + 5);

                    for (int i = 0; i < maxWS + 5; i++)
                    {
                        Varrat_X[i] = i;

                        double slope;
                        double intercept;

                        if (WD_Ind == thisMCP.numWD || (WD_Ind == 0 && thisMCP.numWD == 1))
                        {
                            slope = thisMCP.MCP_Varrat.allSlope;
                            intercept = thisMCP.MCP_Varrat.allIntercept;
                        }
                        else
                        {
                            int TODind = thisMet.GetTOD_Ind(thisMCP.numTODs, thisTOD);
                            int seasonInd = thisMet.GetSeasonInd(thisMCP.numSeasons, thisSeason);
                            slope = thisMCP.MCP_Varrat.slope[WD_Ind, TODind, seasonInd];
                            intercept = thisMCP.MCP_Varrat.intercept[WD_Ind, TODind, seasonInd];
                        }

                        Varrat_Y[i] = slope * i + intercept;
                        SlopeSeries.Points.Add(new DataPoint(Varrat_X[i], Varrat_Y[i]));
                    }
                }
                else if ((thisMCP.MCP_Bins.binAvgSD_Cnt != null) && (thisInst.Get_MCP_Method() == "Method of Bins"))
                {
                    var binSeries = new ScatterSeries();
                    model.Series.Add(binSeries);

                    for (int i = 0; i < thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(0); i++)
                    {
                        if (thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].avgWS_Ratio > 0)
                            binSeries.Points.Add(new ScatterPoint(i * thisMCP.Get_WS_width_for_MCP(), thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].avgWS_Ratio));
                    }
                }

                MCP_TextBoxes(thisInst);
                thisInst.plotMCP.Refresh();
            }

        }

        /// <summary> Update list with mean and standard deviation of WS ratios on MCP tab. </summary>        
        public void MCP_BinList(Continuum thisInst)
        {
            thisInst.lstMCP_Bins.Items.Clear();
            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;

            if (thisMCP == null)
                return;

            if (thisMCP.MCP_Bins.binAvgSD_Cnt != null)
            {
                int numWS = thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(0);

                int WD_Ind = thisInst.GetWD_ind("MCP");
                double WS_Width = thisMCP.Get_WS_width_for_MCP();

                for (int i = 0; i < numWS; i++)
                {
                    double thisWS = i * WS_Width;
                    if (thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].avgWS_Ratio > 0)
                    {
                        objListItem = thisInst.lstMCP_Bins.Items.Add(Convert.ToString(thisWS));
                        objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].avgWS_Ratio, 2)));
                        objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].SD_WS_Ratio, 2)));
                        objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].count, 2)));
                    }
                }
            }
        }


        /// <summary> Update table with results of uncertainty analysis showing window size, mean LT estimate and standard deviation of LT estimate on MCP tab. </summary>
        public void MCP_UncertList(Continuum thisInst)
        {
            thisInst.lstMCP_Uncert.Items.Clear();

            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;

            if (thisMCP == null)
                return;

            string active_method = thisInst.Get_MCP_Method();

            if ((active_method == "Orth. Regression") && (thisMCP.uncertOrtho.Length > 0))
            {
                for (int u = 0; u < thisMCP.uncertOrtho.Length; u++)
                {
                    if ((thisMCP.uncertOrtho[u].avg != 0) && (thisMCP.uncertOrtho[u].stDev != 0))
                    {
                        objListItem = thisInst.lstMCP_Uncert.Items.Add(Convert.ToString(thisMCP.uncertOrtho[u].WSize));
                        objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertOrtho[u].avg, 2)));
                        objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertOrtho[u].stDev, 2)));
                    }
                }
            }
            if ((active_method == "Method of Bins") && (thisMCP.uncertBins.Length > 0))
            {
                for (int u = 0; u < thisMCP.uncertBins.Length; u++)
                {
                    if ((thisMCP.uncertBins[u].avg != 0) && (thisMCP.uncertBins[u].stDev != 0))
                    {
                        objListItem = thisInst.lstMCP_Uncert.Items.Add(Convert.ToString(thisMCP.uncertBins[u].WSize));
                        objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertBins[u].avg, 2)));
                        objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertBins[u].stDev, 2)));
                    }
                }
            }
            if ((active_method == "Variance Ratio") && (thisMCP.uncertVarrat.Length > 0))
            {
                for (int u = 0; u < thisMCP.uncertVarrat.Length; u++)
                {
                    if ((thisMCP.uncertVarrat[u].avg != 0) && (thisMCP.uncertVarrat[u].stDev != 0))
                    {
                        objListItem = thisInst.lstMCP_Uncert.Items.Add(Convert.ToString(thisMCP.uncertVarrat[u].WSize));
                        objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertVarrat[u].avg, 2)));
                        objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertVarrat[u].stDev, 2)));
                    }
                }
            }
            if ((active_method == "Matrix") && (thisMCP.uncertMatrix.Length > 0))
            {
                for (int u = 0; u < thisMCP.uncertMatrix.Length; u++)
                {
                    if ((thisMCP.uncertMatrix[u].avg != 0) && (thisMCP.uncertMatrix[u].stDev != 0))
                    {
                        objListItem = thisInst.lstMCP_Uncert.Items.Add(Convert.ToString(thisMCP.uncertMatrix[u].WSize));
                        objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertMatrix[u].avg, 2)));
                        objListItem.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertMatrix[u].stDev, 2)));
                    }
                }
            }
        }

        /// <summary> Update all tables and plots on MCP tab. </summary>
        public void MCP_TAB(Continuum thisInst)
        {
            MCPDates(thisInst);
            MCP_Buttons(thisInst);
            MCP_BinList(thisInst);
            MCP_Scatterplot(thisInst);
            MCP_TextBoxes(thisInst);
            MCP_UncertList(thisInst);
            MCP_UncertPlot(thisInst);
        }

        /// <summary> Update MCP uncertainty plot on MCP tab. </summary>
        public void MCP_UncertPlot(Continuum thisInst)
        {
            thisInst.plotMCP_Uncertainty.Model = new PlotModel();
            var model = thisInst.plotMCP_Uncertainty.Model;
            model.IsLegendVisible = false;

            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Window Size, months";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "LT Est. Target WS [m/s]";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            var LTDataSeries = new ScatterSeries();
            LTDataSeries.Title = "Long-term WS Estimate";
            model.Series.Add(LTDataSeries);

            var AvgDataSeries = new ScatterSeries();
            AvgDataSeries.Title = "Average Long-term WS Estimate";
            model.Series.Add(AvgDataSeries);

            // Get Active MCP type
            string selectedMethod = thisInst.Get_MCP_Method();
            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;

            if (thisMCP == null)
            {
                thisInst.plotMCP_Uncertainty.Refresh();
                return;
            }

            if (selectedMethod == "Orth. Regression" && thisMCP.uncertOrtho.Length > 0)
            {
                for (int u = 0; u < thisMCP.uncertOrtho.Length; u++)
                {
                    if (thisMCP.uncertOrtho[u].LT_Ests != null)
                    {
                        for (int i = 0; i < thisMCP.uncertOrtho[u].LT_Ests.Length; i++)
                            LTDataSeries.Points.Add(new ScatterPoint(thisMCP.uncertOrtho[u].WSize, Math.Round(thisMCP.uncertOrtho[u].LT_Ests[i], 3)));

                    }
                    // Assign LT Avg series = Avg of Uncert obj
                    if (thisMCP.uncertOrtho[u].avg != 0)
                        AvgDataSeries.Points.Add(new ScatterPoint(thisMCP.uncertOrtho[u].WSize, Math.Round(thisMCP.uncertOrtho[u].avg, 3)));

                }
            }
            if (selectedMethod == "Method of Bins" && thisMCP.uncertBins.Length > 0)
            {
                for (int u = 0; u < thisMCP.uncertBins.Length; u++)
                {
                    if (thisMCP.uncertBins[u].LT_Ests != null)
                    {
                        for (int i = 0; i < thisMCP.uncertBins[u].LT_Ests.Length; i++)
                            LTDataSeries.Points.Add(new ScatterPoint(thisMCP.uncertBins[u].WSize, Math.Round(thisMCP.uncertBins[u].LT_Ests[i], 3)));

                    }
                    // Assign LT Avg series = Avg of Uncert obj
                    if (thisMCP.uncertBins[u].avg != 0)
                        AvgDataSeries.Points.Add(new ScatterPoint(thisMCP.uncertBins[u].WSize, Math.Round(thisMCP.uncertBins[u].avg, 3)));

                }
            }
            if (selectedMethod == "Variance Ratio" && thisMCP.uncertVarrat.Length > 0)
            {
                for (int u = 0; u < thisMCP.uncertVarrat.Length; u++)
                {
                    if (thisMCP.uncertVarrat[u].LT_Ests != null)
                    {
                        for (int i = 0; i < thisMCP.uncertVarrat[u].LT_Ests.Length; i++)
                            LTDataSeries.Points.Add(new ScatterPoint(thisMCP.uncertVarrat[u].WSize, Math.Round(thisMCP.uncertVarrat[u].LT_Ests[i], 3)));

                    }
                    // Assign LT Avg series = Avg of Uncert obj
                    if (thisMCP.uncertVarrat[u].avg != 0)
                        AvgDataSeries.Points.Add(new ScatterPoint(thisMCP.uncertVarrat[u].WSize, Math.Round(thisMCP.uncertVarrat[u].avg, 3)));

                }
            }
            if (selectedMethod == "Matrix" && thisMCP.uncertMatrix.Length > 0)
            {
                for (int u = 0; u < thisMCP.uncertMatrix.Length; u++)
                {
                    if (thisMCP.uncertMatrix[u].LT_Ests != null)
                    {
                        for (int i = 0; i < thisMCP.uncertMatrix[u].LT_Ests.Length; i++)
                            LTDataSeries.Points.Add(new ScatterPoint(thisMCP.uncertMatrix[u].WSize, Math.Round(thisMCP.uncertMatrix[u].LT_Ests[i], 3)));

                    }
                    // Assign LT Avg series = Avg of Uncert obj
                    if (thisMCP.uncertMatrix[u].avg != 0)
                        AvgDataSeries.Points.Add(new ScatterPoint(thisMCP.uncertMatrix[u].WSize, Math.Round(thisMCP.uncertMatrix[u].avg, 3)));

                }
            }

            thisInst.plotMCP_Uncertainty.Refresh();

        }

        /// <summary> Updates yearly summary table on MERRA2 tab based on selected parameter and creates plot of yearly values. </summary>
        public void MERRA_AnnualTableAndPlot(Continuum thisInst)
        {
            thisInst.lstMERRAAnnualProd.Items.Clear();
            MERRA thisMERRA = thisInst.GetSelectedMERRA();

            thisInst.plotMERRA_Yearly.Model = new PlotModel();
            var model = thisInst.plotMERRA_Yearly.Model;
            model.IsLegendVisible = false;

            if (thisMERRA.interpData.TS_Data == null)
            {
                thisInst.plotMERRA_Yearly.Refresh();
                return;
            }

            if (thisMERRA.interpData.TS_Data.Length == 0)
            {
                thisMERRA.GetMERRADataFromDB(thisInst);
                thisMERRA.GetInterpData(thisInst.UTM_conversions);
            }

            string selectedParam = thisInst.GetMERRA_SelectedPlotParameter();
            thisInst.lstMERRAAnnualProd.Columns[1].Text = selectedParam;
            thisInst.lstMERRAAnnualProd.Columns[2].Text = "% Diff. from LT";

            if (thisInst.okToUpdate == false || thisMERRA.GotWindTS(thisInst.UTM_conversions) == false || (thisMERRA.powerCurve.name == null &&
                (selectedParam == "CF (%)" || selectedParam == "Energy Prod.")))
            {
                thisInst.plotMERRA_Yearly.Refresh();
                return;
            }

            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("MERRA");

            if (powerCurve.name != thisMERRA.powerCurve.name)
            {
                thisMERRA.Reset_MonthProdStats();
                thisMERRA.Reset_AnnualProdStats();
                thisMERRA.powerCurve = powerCurve;
                thisMERRA.ApplyPC(ref thisMERRA.interpData.TS_Data);
            }

            int firstYear = thisInst.merraList.startDate.Year;
            int lastYear = thisInst.merraList.endDate.Year;

            if (thisMERRA.interpData.Annual_Prod.LT_Avg == 0)
            {
                thisMERRA.Calc_MonthProdStats(thisInst.UTM_conversions);
                thisMERRA.CalcAnnualProd(ref thisMERRA.interpData.Annual_Prod, thisMERRA.interpData.Monthly_Prod, thisInst.UTM_conversions);
            }

            MERRA.YearlyProdAndLTAvg thisAnnual = thisMERRA.interpData.Annual_Prod;
            MERRA.MonthlyProdByYearAndLTAvg[] thisMonthly = thisMERRA.interpData.Monthly_Prod;
            MERRA.Wind_TS_with_Prod[] thisTS = thisMERRA.interpData.TS_Data;
            double diff = 0;
            double LT_Val = 0;

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Year";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = selectedParam;

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            if (selectedParam == "Energy Prod.")
            {
                model.Title = "Yearly Energy Prod. [MWh]";
                yAxis.Title = "Energy Prod. [MWh]";
            }
            else if (selectedParam.Substring(selectedParam.Length - 2, 2) == "WS")
            {
                model.Title = "Yearly WS [m/s]";
                yAxis.Title = "WS [m/s]";
            }
            else
                model.Title = "Yearly " + selectedParam;

            var paramSeries = new LineSeries();

            for (int i = firstYear; i <= lastYear; i++)
            {
                if (thisMERRA.Have_Full_Year(thisTS, i))
                {
                    ListViewItem objListItem = new ListViewItem(i.ToString()); // Adds year to table

                    int yearInd = thisMERRA.Get_Year_Ind(i, thisAnnual);

                    if (selectedParam == "CF (%)" || selectedParam == "Energy Prod.")
                    {
                        double prod = thisMERRA.Get_Energy_Prod(thisAnnual, thisMonthly, 100, i);
                        double thisCF = thisMERRA.Calc_CF(prod, 100, i, powerCurve);

                        if (selectedParam == "Energy Prod.")
                        {
                            LT_Val = thisMERRA.Get_Energy_Prod(thisAnnual, thisMonthly, 100, 100);
                            diff = thisMERRA.Calc_Dev_from_LT(thisMonthly, thisAnnual, i, 100);
                            objListItem.SubItems.Add(Math.Round(prod, 1).ToString());
                            paramSeries.Points.Add(new DataPoint(i, Math.Round(prod, 1)));
                        }
                        else
                        {
                            double LT_Prod = thisMERRA.Get_Energy_Prod(thisAnnual, thisMonthly, 100, 100);
                            LT_Val = thisMERRA.Calc_CF(LT_Prod, 100, 100, powerCurve);
                            diff = (thisCF - LT_Val) / LT_Val;
                            objListItem.SubItems.Add(Math.Round(thisCF, 4).ToString("P"));
                            paramSeries.Points.Add(new DataPoint(i, Math.Round(thisCF * 100, 2)));
                        }
                    }
                    else
                    {
                        double avg = thisMERRA.Calc_Avg_or_LT(thisTS, 100, i, selectedParam);
                        LT_Val = thisMERRA.Calc_Avg_or_LT(thisTS, 100, 100, selectedParam);
                        diff = (avg - LT_Val) / LT_Val;
                        objListItem.SubItems.Add(Math.Round(avg, 2).ToString());
                        paramSeries.Points.Add(new DataPoint(i, Math.Round(avg, 1)));
                    }

                    objListItem.SubItems.Add((Math.Round(diff, 4)).ToString("P")); // adds % diff from the average
                    thisInst.lstMERRAAnnualProd.Items.Add(objListItem);

                }
            }

            model.Series.Add(paramSeries);

            ListViewItem objListItem2 = new ListViewItem("LT Avg"); // Adds year to table
            objListItem2.SubItems.Add(Math.Round(LT_Val, 2).ToString());
            thisInst.plotMERRA_Yearly.Refresh();
        }



        /// <summary> Updates MERRA2 monthly table and plot on MERRA2 tab. </summary>        
        public void MERRA_MonthlyTableAndPlot(Continuum thisInst)
        {
            thisInst.lstMERRA_MonthlyProd.Items.Clear();
            MERRA thisMERRA = thisInst.GetSelectedMERRA();

            thisInst.plotMERRA_Monthly.Model = new PlotModel();
            var model = thisInst.plotMERRA_Monthly.Model;
            model.IsLegendVisible = false;

            if (thisInst.okToUpdate == false || thisMERRA.GotWindTS(thisInst.UTM_conversions) == false)
            {
                thisInst.plotMERRA_Monthly.Refresh();
                return;
            }

            // Calculate monthly energy production by year and average monthly energy production

            if (thisMERRA.GotMonthlyProd() == false)
                thisMERRA.Calc_MonthProdStats(thisInst.UTM_conversions);

            MERRA.MonthlyProdByYearAndLTAvg[] thisMonthly = thisMERRA.interpData.Monthly_Prod;
            MERRA.YearlyProdAndLTAvg thisAnnual = thisMERRA.interpData.Annual_Prod;
            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("MERRA");

            MERRA.Wind_TS_with_Prod[] thisTS = thisMERRA.interpData.TS_Data;
            string selectedParam = thisInst.GetMERRA_SelectedPlotParameter();
            thisInst.lstMERRA_MonthlyProd.Columns[2].Text = selectedParam;

            if (thisMERRA.powerCurve.name == null && (selectedParam == "CF (%)" || selectedParam == "Energy Prod."))
            {
                thisInst.plotMERRA_Monthly.Refresh();
                return;
            }

            int numYears = thisInst.chkYearsToDisplay.CheckedItems.Count;
            int thisYear = 0;
            double diffFromLT = 0;

            // Configure plot 
            model.Title = "Monthly " + selectedParam;

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Month";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = selectedParam;

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            if (selectedParam == "Energy Prod.")
            {
                model.Title = "Monthly Energy Prod. [MWh]";
                yAxis.Title = "Energy Prod. [MWh]";
            }
            else if (selectedParam.Substring(selectedParam.Length - 2, 2) == "WS")
            {
                model.Title = "Monthly WS [m/s]";
                yAxis.Title = "WS [m/s]";
            }

            for (int n = 0; n < numYears; n++)
            {
                var monthlySeries = new LineSeries();

                if (thisInst.chkYearsToDisplay.CheckedItems[n].ToString() == "LT Avg")
                {
                    thisYear = 100;
                    monthlySeries.Title = "LT Avg";
                    monthlySeries.LineStyle = LineStyle.Dash;
                    monthlySeries.Color = OxyColors.Black;
                }
                else
                {
                    thisYear = Convert.ToInt16(thisInst.chkYearsToDisplay.CheckedItems[n].ToString());
                    monthlySeries.Title = thisYear.ToString();
                }

                for (int i = 1; i <= 12; i++)
                {
                    string monthStr = new DateTime(1999, i, 1).ToString("MMM", CultureInfo.InvariantCulture);

                    if ((thisYear == 100 || thisMERRA.Have_Full_Month(thisTS, i, thisYear)))
                    {
                        ListViewItem objListItem = new ListViewItem(monthStr);
                        if (thisYear != 100)
                            objListItem.SubItems.Add(thisYear.ToString());
                        else
                            objListItem.SubItems.Add("LT Avg");

                        if (selectedParam == "CF (%)" || selectedParam == "Energy Prod.")
                        {
                            double prod = thisMERRA.Get_Energy_Prod(thisAnnual, thisMonthly, i, thisYear);
                            double LT_Prod = thisMERRA.Get_Energy_Prod(thisAnnual, thisMonthly, i, 100);
                            double thisCF = thisMERRA.Calc_CF(prod, i, thisYear, powerCurve);

                            if (selectedParam == "CF (%)")
                            {
                                objListItem.SubItems.Add((Math.Round(thisCF, 4)).ToString("P"));
                                double LT_CF = thisMERRA.Calc_CF(LT_Prod, i, thisYear, powerCurve);
                                diffFromLT = (thisCF - LT_CF) / LT_CF;
                                monthlySeries.Points.Add(new DataPoint(i, Math.Round(thisCF * 100, 4)));

                            }
                            else
                            {
                                objListItem.SubItems.Add((Math.Round(prod, 1)).ToString());
                                diffFromLT = (prod - LT_Prod) / LT_Prod;
                                monthlySeries.Points.Add(new DataPoint(i, Math.Round(prod, 1)));
                            }
                        }
                        else
                        {
                            double LT_Val = thisMERRA.Calc_Avg_or_LT(thisTS, i, 100, selectedParam);
                            double monthVal = thisMERRA.Calc_Avg_or_LT(thisTS, i, thisYear, selectedParam);
                            objListItem.SubItems.Add((Math.Round(monthVal, 2)).ToString()); // adds the monthly average 
                            monthlySeries.Points.Add(new DataPoint(i, Math.Round(monthVal, 1)));
                            diffFromLT = (monthVal - LT_Val) / LT_Val;
                        }

                        objListItem.SubItems.Add(diffFromLT.ToString("P"));
                        thisInst.lstMERRA_MonthlyProd.Items.Add(objListItem);

                    }
                }

                model.Series.Add(monthlySeries);
            }

            thisInst.lstMERRA_MonthlyProd.Columns[0].Width = 40;
            thisInst.lstMERRA_MonthlyProd.Columns[1].Width = 50;
            thisInst.lstMERRA_MonthlyProd.Columns[2].Width = 60;
            thisInst.lstMERRA_MonthlyProd.Columns[3].Width = 90;

            thisInst.plotMERRA_Monthly.Refresh();
        }

        /// <summary> Updates MERRA2 dropdown menu based on what datasets have been imported on MERRA2 tab. </summary> 
        public void MERRA_Dropdowns(Continuum thisInst)
        {
            thisInst.okToUpdate = false;

            thisInst.cboMERRAYear.Items.Clear();
            thisInst.chkYearsToDisplay.Items.Clear();
            thisInst.chkYears_Monthly.Items.Clear();
            thisInst.cboMERRAYear.Text = "";

            if (thisInst.merraList.numMERRA_Data == 0)
            {
                thisInst.okToUpdate = true;
                return;
            }

            MERRA thisMERRA = thisInst.GetSelectedMERRA();

            if (thisMERRA.interpData.TS_Data == null)
            {
                thisInst.okToUpdate = true;
                return;
            }

            if (thisMERRA.interpData.TS_Data.Length == 0)
            {
                thisMERRA.GetMERRADataFromDB(thisInst);
                thisMERRA.GetInterpData(thisInst.UTM_conversions);
            }

            if (thisMERRA.GotWindTS(thisInst.UTM_conversions)) // By default only show LT Avg (to speed up AllTAB update)
            {
                thisInst.cboMERRAYear.Items.Add("LT Avg");
                thisInst.chkYearsToDisplay.Items.Add("LT Avg", true);
                thisInst.chkYears_Monthly.Items.Add("LT Avg", true);

                for (DateTime thisDate = thisInst.merraList.startDate; thisDate <= thisInst.merraList.endDate; thisDate = thisDate.AddYears(1))
                {
                    thisInst.cboMERRAYear.Items.Add(Convert.ToString(thisDate.Year));
                    thisInst.chkYearsToDisplay.Items.Add(Convert.ToString(thisDate.Year), false);
                    thisInst.chkYears_Monthly.Items.Add(Convert.ToString(thisDate.Year), false);
                }
            }

            if (thisInst.cboMERRAYear.Items.Count > 0) thisInst.cboMERRAYear.SelectedIndex = 0;

            thisInst.cboMERRA_PlotParam.Items.Clear();

            thisInst.cboMERRA_PlotParam.Items.Add("50 m WS");
            thisInst.cboMERRA_PlotParam.Items.Add("CF (%)");
            thisInst.cboMERRA_PlotParam.Items.Add("Energy Prod.");
            thisInst.cboMERRA_PlotParam.Items.Add("10 m Temp");
            thisInst.cboMERRA_PlotParam.Items.Add("Surface Pressure");
            thisInst.cboMERRA_PlotParam.Items.Add("Sea Level Pressure");

            if (thisInst.cboMERRA_PlotParam.Items.Count > 0)
                thisInst.cboMERRA_PlotParam.SelectedIndex = 0;

            thisInst.okToUpdate = true;
        }

        /// <summary> Updates textboxes on MERRA2 tab. </summary> 
        public void MERRA_Textboxes(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            MERRA thisMERRA = thisInst.GetSelectedMERRA();
            Met thisMet = thisInst.GetSelectedMet("MERRA");

            thisInst.txt_MERRA2_folder.Text = thisInst.merraList.MERRAfolder;
            string thisLLStr = thisInst.cboMERRASelectedMet.SelectedItem.ToString();

            if (thisMet.name != null) // it is associated with a met
            {
                UTM_conversion.Lat_Long thisLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                thisInst.txtMERRA_SelectedLat.Text = Math.Round(thisLL.latitude, 3).ToString();
                thisInst.txtMERRA_SelectedLong.Text = Math.Round(thisLL.longitude, 3).ToString();

                thisInst.txtMERRA_SelectedLat.Enabled = false;
                thisInst.txtMERRA_SelectedLong.Enabled = false;
            }
            else if (thisLLStr != "User-Defined Lat/Long" && thisLLStr != "") // not associated with a met
            {
                int firstColon = thisLLStr.IndexOf(':');
                int secColon = thisLLStr.LastIndexOf(':');

                double thisLat = Convert.ToDouble(thisLLStr.Substring(firstColon + 2, secColon - firstColon - 6));
                double thisLong = Convert.ToDouble(thisLLStr.Substring(secColon + 2, thisLLStr.Length - secColon - 2));

                thisInst.txtMERRA_SelectedLat.Text = Math.Round(thisLat, 3).ToString();
                thisInst.txtMERRA_SelectedLong.Text = Math.Round(thisLong, 3).ToString();

                thisInst.txtMERRA_SelectedLat.Enabled = false;
                thisInst.txtMERRA_SelectedLong.Enabled = false;
            }
            else
            {
                thisInst.txtMERRA_SelectedLat.Enabled = true;
                thisInst.txtMERRA_SelectedLong.Enabled = true;
            }

            thisInst.txtMERRA_WS_ScaleFact.Text = thisMERRA.WS_ScaleFactor.ToString();
            thisInst.okToUpdate = true;
        }

        /// <summary> Updates MERRA2 settings (start/end dates, number of nodes, and scale factor) on MERRA2 tab. </summary> 
        public void MERRA_Settings(Continuum thisInst)
        {
            thisInst.okToUpdate = false;

            thisInst.dateMERRAStart.Value = thisInst.merraList.startDate;
            thisInst.dateMERRAEnd.Value = thisInst.merraList.endDate;

            if (thisInst.merraList.numMERRA_Nodes == 1)
                thisInst.cboNumMERRA_Nodes.SelectedIndex = 0;
            else if (thisInst.merraList.numMERRA_Nodes == 4)
                thisInst.cboNumMERRA_Nodes.SelectedIndex = 1;
            else if (thisInst.merraList.numMERRA_Nodes == 16)
                thisInst.cboNumMERRA_Nodes.SelectedIndex = 2;

            thisInst.okToUpdate = true;

        }

        /// <summary> Updates MERRA2 wind rose plot on MERRA2 tab. </summary> 
        public void MERRA_WindRosePlot(Continuum thisInst)
        {
            MERRA thisMERRA = thisInst.GetSelectedMERRA();
            thisInst.plotMERRA_WindRose.Model = new PlotModel();
            var model = thisInst.plotMERRA_WindRose.Model;
            model.PlotType = PlotType.Polar;
            model.IsLegendVisible = false;
            model.PlotAreaBorderThickness = new OxyThickness(0);

            if (thisMERRA.interpData.TS_Data == null)
            {
                thisInst.plotMERRA_WindRose.Refresh();
                return;
            }

            if (thisMERRA.interpData.TS_Data.Length == 0)
            {
                thisMERRA.GetMERRADataFromDB(thisInst);
                thisMERRA.GetInterpData(thisInst.UTM_conversions);
            }

            if (thisInst.cboMERRAYear.Items.Count == 0)
            {
                thisInst.plotMERRA_WindRose.Refresh();
                return;
            }

            int thisMonth = 100;
            if (thisInst.cboMERRA_Month.SelectedItem.ToString() != "All Months")
                thisMonth = thisInst.cboMERRA_Month.SelectedIndex + 1;

            int thisYear = 100;
            if (thisInst.cboMERRAYear.SelectedItem.ToString() != "LT Avg")
                thisYear = Convert.ToInt16(thisInst.cboMERRAYear.SelectedItem.ToString());

            if (thisMonth == -1 || thisYear == -1)
                return;

            // Specify axes
            model.Axes.Add(new AngleAxis
            {
                StartAngle = 90,
                EndAngle = -270,
                Minimum = 0,
                Maximum = 360
            });

            double[] interpWR = thisMERRA.Calc_Wind_Rose(thisMonth, thisYear, thisInst.UTM_conversions);


            if (thisMERRA.GotWindTS(thisInst.UTM_conversions) == false)
                return;

            var WR_Series = new LineSeries();
            SortedList<double, double> ii = new SortedList<double, double>();

            for (int i = 0; i < interpWR.Length; i++)
            {

                ii.Add(i * 360 / interpWR.Length, interpWR[i]);
            }

            ii.Add(360, interpWR[0]);

            WR_Series.ItemsSource = ii;
            WR_Series.LineStyle = LineStyle.Solid;

            WR_Series.DataFieldX = "Value";
            WR_Series.DataFieldY = "Key";

            model.Series.Add(WR_Series);

            thisInst.plotMERRA_WindRose.Refresh();
        }

        /// <summary> Updates all tables and plots on MERRA2 tab. </summary> 
        public void MERRA_TAB(Continuum thisInst)
        {
            MERRA_AnnualTableAndPlot(thisInst);
            MERRA_MonthlyTableAndPlot(thisInst);
            MERRA_Textboxes(thisInst);
            MERRA_WindRosePlot(thisInst);

            // MERRA2 tab
            MERRA thisMERRA = thisInst.GetSelectedMERRA();
            if (thisMERRA.GotWindTS(thisInst.UTM_conversions))
                thisInst.btn_Import_MERRA.BackColor = Color.MediumSeaGreen;
            else if (thisMERRA.GotWindTS(thisInst.UTM_conversions) == false)
                thisInst.btn_Import_MERRA.BackColor = Color.LightCoral;
            else
                thisInst.btn_Import_MERRA.BackColor = Color.Gray;
        }

        /// <summary> Updates all tables and plots on Site Suitability tab. </summary> 
        public void SiteSuitabilityTAB(Continuum thisInst)
        {

            if (thisInst.cboIcingYear.Items.Count == 0)
                IcingYearsDropDown(thisInst);

            if (thisInst.cboSiteSuitabilitySelectPlot.Items.Count == 0)
                SiteSuitabilityDropdown(thisInst, null);

            SiteSuitabilityVisibility(thisInst); // updates dropdown menus and plot/table visibility based on selected model

            thisInst.okToUpdate = false;
            thisInst.txtNumIceThrowsPerDay.Text = thisInst.siteSuitability.iceThrowsPerIceDay.ToString();
            thisInst.txtNumIceDays.Text = thisInst.siteSuitability.numIceDaysPerYear.ToString();
            thisInst.txtTurbineNoise.Text = Math.Round(thisInst.siteSuitability.turbineSound, 1).ToString();

            if (thisInst.cboIceDistORIceHisto.SelectedItem == null)
                thisInst.cboIceDistORIceHisto.SelectedIndex = 0;

            thisInst.okToUpdate = true;

            if (thisInst.cboSiteSuitabilitySelectPlot.SelectedItem != null)
            {
                string selectedSiteSuitability = thisInst.cboSiteSuitabilitySelectPlot.SelectedItem.ToString();

                if (selectedSiteSuitability == "Ice Throw")
                {
                    IceThrowPlotsAndTables(thisInst);
                }
                else if (selectedSiteSuitability == "Shadow Flicker")
                {
                    ShadowFlickerSurfacePlot(thisInst);
                    ShadowFlicker12x24(thisInst);
                    ZoneShadowSummary(thisInst);
                    ShadowFlickerMaxDay(thisInst);
                }
                else if (selectedSiteSuitability == "Sound")
                {
                    SoundMap(thisInst);

                    if (thisInst.siteSuitability.yearlyIceHits.Length != 0) // Update the ice throw by zone table too in case zone list changed                
                        IceHitsByZone(thisInst);
                }

                SoundAtZones(thisInst);

            }

        }

        /// <summary> Updates the plots and tables on Time Series Analysis tab. </summary> 
        public void Monthly_TAB(Continuum thisInst)
        {
            TurbineYearlyPlotAndTable(thisInst);
            TurbineMonthlyTable(thisInst);
            TurbineMonthlyPlot(thisInst);
        }

        /// <summary> Updates the yearly table and plot on Time Series Analysis tab. </summary> 
        public void TurbineYearlyPlotAndTable(Continuum thisInst)
        {
            thisInst.lstYearlyTurbine.Items.Clear();
            Turbine thisTurb = thisInst.GetSelectedTurbine("Monthly");

            thisInst.plotYearlyTS.Model = new PlotModel();
            var model = thisInst.plotYearlyTS.Model;
            model.IsLegendVisible = false;

            if (thisTurb.AvgWSEst_Count == 0 || thisInst.turbineList.genTimeSeries == false)
            {
                thisInst.plotYearlyTS.Refresh();
                return;
            }

            CheckedListBox.CheckedItemCollection theseParams = thisInst.chkSelectedTurbineParam.CheckedItems;
            bool plotWS = false;
            LineSeries WS_Series = new LineSeries();
            WS_Series.Title = "Avg WS";
            bool plotGross = false;
            LineSeries Gross_Series = new LineSeries();
            Gross_Series.Title = "Gross AEP";
            bool plotNet = false;
            LineSeries Net_Series = new LineSeries();
            Net_Series.Title = "Net AEP";
            bool plotWake = false;
            LineSeries Wake_Series = new LineSeries();
            Wake_Series.Title = "Wake Loss";
            bool plotDiff = false;
            LineSeries Diff_Series = new LineSeries();
            Diff_Series.Title = "% Diff";

            foreach (var n in theseParams)
            {
                if (n.ToString() == "Avg WS")
                {
                    plotWS = true;
                    model.Series.Add(WS_Series);
                }

                if (n.ToString() == "Gross AEP")
                {
                    plotGross = true;
                    model.Series.Add(Gross_Series);
                }

                if (n.ToString() == "Net AEP")
                {
                    plotNet = true;
                    model.Series.Add(Net_Series);
                }

                if (n.ToString() == "Wake Loss")
                {
                    plotWake = true;
                    model.Series.Add(Wake_Series);
                }

                if (n.ToString() == "% Diff")
                {
                    plotDiff = true;
                    model.Series.Add(Diff_Series);
                }
            }

            bool needSecondYAxis = false;
            string YAxisTitle = "";

            if (theseParams.Count == 0)
            {
                thisInst.plotYearlyTS.Refresh();
                return;
            }
            else if (theseParams.Count > 0)
            {
                for (int i = 0; i < theseParams.Count; i++)
                {
                    if (theseParams[i].ToString() == "Net AEP" || theseParams[i].ToString() == "Gross AEP")
                        needSecondYAxis = true;
                    else if (i == 0)
                        YAxisTitle = theseParams[i].ToString();
                    else
                        YAxisTitle = YAxisTitle + " / " + theseParams[i].ToString();
                }
            }

            model.Title = "Yearly Trends at " + thisTurb.name;

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Year";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = YAxisTitle;
            yAxis.Key = "Primary";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            if (needSecondYAxis == true)
            {
                LinearAxis secYAxis = new LinearAxis();
                secYAxis.Position = AxisPosition.Right;
                secYAxis.Title = "Net AEP";
                secYAxis.Key = "Secondary";
            }

            Wake_Model thisWakeModel = null;
            if (thisInst.wakeModelList.NumWakeModels > 0 && thisInst.cboMonthlyWakeModel.Items.Count != 0)
            {
                string wakeModelString = thisInst.cboMonthlyWakeModel.SelectedItem.ToString();
                thisWakeModel = thisInst.wakeModelList.GetWakeModelFromString(wakeModelString);
            }

            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Monthly");

            int firstYear = thisInst.merraList.startDate.Year;
            int lastYear = thisInst.merraList.endDate.Year;

            for (int i = firstYear; i <= lastYear; i++)
            {
                double avgWS = Math.Round(thisTurb.CalcYearlyValue(i, "Avg WS", null, new TurbineCollection.PowerCurve()), 2);
                double grossEnergy = Math.Round(thisTurb.CalcYearlyValue(i, "Gross AEP", thisWakeModel, powerCurve), 0);
                double netEnergy = Math.Round(thisTurb.CalcYearlyValue(i, "Net AEP", thisWakeModel, powerCurve), 0);

                // Add values to list
                objListItem = thisInst.lstYearlyTurbine.Items.Add(i.ToString());

                if (avgWS != 0)
                {
                    objListItem.SubItems.Add(avgWS.ToString());

                    if (plotWS == true)  // Add values to plot series                        
                        WS_Series.Points.Add(new DataPoint(i, avgWS));

                }

                if (grossEnergy != 0)
                {
                    objListItem.SubItems.Add(grossEnergy.ToString());

                    if (plotGross == true)
                    {
                        Gross_Series.Points.Add(new DataPoint(i, grossEnergy));

                        if (needSecondYAxis)
                            Gross_Series.YAxisKey = "Secondary";
                        else
                            Gross_Series.YAxisKey = "Primary";
                    }
                }

                if (netEnergy != 0)
                {
                    objListItem.SubItems.Add(netEnergy.ToString());

                    if (plotNet == true)
                    {
                        Net_Series.Points.Add(new DataPoint(i, netEnergy));

                        if (needSecondYAxis)
                            Net_Series.YAxisKey = "Secondary";
                        else
                            Net_Series.YAxisKey = "Primary";
                    }
                }
                else
                    objListItem.SubItems.Add("");

                if (grossEnergy != 0 && netEnergy != 0)
                {
                    double otherLoss = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
                    double thisWakeLoss = Math.Round((grossEnergy - netEnergy / otherLoss) / grossEnergy, 4);
                    objListItem.SubItems.Add(thisWakeLoss.ToString("P"));

                    if (plotWake == true)
                        Wake_Series.Points.Add(new DataPoint(i, 100 * thisWakeLoss));
                }
                else
                    objListItem.SubItems.Add("");

                if (netEnergy == 0 && grossEnergy != 0) // no net estimates so calculate % diff in gross energy
                {
                    Turbine.Gross_Energy_Est thisGross = thisTurb.GetGrossEnergyEst(powerCurve);
                    double percDiff = Math.Round((grossEnergy - thisGross.AEP) / thisGross.AEP, 4);
                    objListItem.SubItems.Add(percDiff.ToString("P"));

                    if (plotDiff == true)
                        Diff_Series.Points.Add(new DataPoint(i, percDiff));
                }
                else if (netEnergy != 0) // Calculate % Diff in net energy
                {
                    Turbine.Net_Energy_Est thisNet = thisTurb.GetNetEnergyEst(thisWakeModel);
                    double percDiff = Math.Round((netEnergy - thisNet.AEP) / thisNet.AEP, 4);
                    objListItem.SubItems.Add(percDiff.ToString("P"));

                    if (plotDiff == true)
                        Diff_Series.Points.Add(new DataPoint(i, percDiff));

                }
            }

            thisInst.plotYearlyTS.Refresh();
        }

        /// <summary> Updates the yearly table on Time Series Analysis tab </summary>        
        public void TurbineMonthlyTable(Continuum thisInst)
        {
            thisInst.lstMonthlyTurbine.Items.Clear();
            Turbine thisTurb = thisInst.GetSelectedTurbine("Monthly");

            Wake_Model thisWakeModel = null;
            if (thisInst.wakeModelList.NumWakeModels > 0 && thisInst.cboMonthlyWakeModel.Items.Count != 0)
            {
                string wakeModelString = thisInst.cboMonthlyWakeModel.SelectedItem.ToString();
                thisWakeModel = thisInst.wakeModelList.GetWakeModelFromString(wakeModelString);
            }

            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Monthly");

            Turbine.Avg_Est thisAvgEst = thisTurb.GetAvgWS_Est(null); // Want free-stream wind speeds
            Turbine.Gross_Energy_Est thisGrossEst = thisTurb.GetGrossEnergyEst(powerCurve);
            Turbine.Net_Energy_Est thisNetEst = thisTurb.GetNetEnergyEst(thisWakeModel);

            if (thisAvgEst.FS_MonthlyVals == null)
                return;

            int numMonths = thisAvgEst.FS_MonthlyVals.Length;

            for (int i = 0; i < numMonths; i++)
            {
                objListItem = thisInst.lstMonthlyTurbine.Items.Add(thisAvgEst.FS_MonthlyVals[i].month.ToString());
                objListItem.SubItems.Add(thisAvgEst.FS_MonthlyVals[i].year.ToString());
                objListItem.SubItems.Add(Math.Round(thisAvgEst.FS_MonthlyVals[i].avgWS, 2).ToString());

                if (thisGrossEst.monthlyVals != null)
                    if (thisGrossEst.monthlyVals[i].energyProd != 0)
                        objListItem.SubItems.Add(Math.Round(thisGrossEst.monthlyVals[i].energyProd, 1).ToString());

                if (thisNetEst.monthlyVals != null)
                {
                    if (thisNetEst.monthlyVals[i].energyProd != 0)
                        objListItem.SubItems.Add(Math.Round(thisNetEst.monthlyVals[i].energyProd, 1).ToString());

                    if (thisGrossEst.monthlyVals[i].energyProd != 0 && thisNetEst.monthlyVals[i].energyProd != 0)
                    {
                        double otherLoss = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
                        double thisWakeLoss = (thisGrossEst.monthlyVals[i].energyProd - thisNetEst.monthlyVals[i].energyProd / otherLoss) / thisGrossEst.monthlyVals[i].energyProd;
                        objListItem.SubItems.Add(Math.Round(100 * thisWakeLoss, 2).ToString());
                    }
                }

                if (thisGrossEst.monthlyVals != null)
                    if (thisGrossEst.monthlyVals[i].energyProd != 0)
                    {
                        double thisLTValue = thisTurb.CalcLT_MonthlyValue("Gross AEP", thisAvgEst.FS_MonthlyVals[i].month, null, powerCurve);
                        double percDiff = (thisGrossEst.monthlyVals[i].energyProd - thisLTValue) / thisLTValue;
                        objListItem.SubItems.Add(Math.Round(100 * percDiff, 2).ToString());
                    }

            }

            // Add LT Estimates to list and plot
            for (int i = 1; i <= 12; i++)
            {
                objListItem = thisInst.lstMonthlyTurbine.Items.Add(i.ToString());
                objListItem.SubItems.Add("LT Avg");

                double LT_AvgWS = thisTurb.CalcLT_MonthlyValue("Avg WS", i, null, powerCurve);
                objListItem.SubItems.Add(Math.Round(LT_AvgWS, 2).ToString());

                if (thisGrossEst.AEP != 0)
                {
                    double LT_GrossAEP = thisTurb.CalcLT_MonthlyValue("Gross AEP", i, null, powerCurve);
                    objListItem.SubItems.Add(Math.Round(LT_GrossAEP, 1).ToString());

                    if (thisNetEst.AEP != 0)
                    {
                        double LT_NetAEP = thisTurb.CalcLT_MonthlyValue("Net AEP", i, thisWakeModel, powerCurve);
                        objListItem.SubItems.Add(Math.Round(LT_NetAEP, 1).ToString());

                        double otherLoss = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);

                        if (LT_NetAEP == 0) return;
                        double thisWakeLoss = (LT_GrossAEP - LT_NetAEP / otherLoss) / LT_GrossAEP;
                        objListItem.SubItems.Add(Math.Round(100 * thisWakeLoss, 2).ToString());
                    }
                }
            }
        }

        /// <summary> Updates the monthly plot on Time Series Analysis tab </summary> 
        public void TurbineMonthlyPlot(Continuum thisInst)
        {
            Turbine thisTurb = thisInst.GetSelectedTurbine("Monthly");

            thisInst.plotMonthlyTS.Model = new PlotModel();
            var model = thisInst.plotMonthlyTS.Model;
            model.IsLegendVisible = false;

            if (thisTurb.AvgWSEst_Count == 0 || thisInst.turbineList.genTimeSeries == false)
            {
                thisInst.plotMonthlyTS.Refresh();
                return;
            }

            CheckedListBox.CheckedItemCollection theseParams = thisInst.chkSelectedTurbineParam.CheckedItems;
            bool plotWS = false;
            LineSeries WS_Series = new LineSeries();
            bool plotGross = false;
            LineSeries Gross_Series = new LineSeries();
            bool plotNet = false;
            LineSeries Net_Series = new LineSeries();
            bool plotWake = false;
            LineSeries Wake_Series = new LineSeries();
            bool plotDiff = false;
            LineSeries Diff_Series = new LineSeries();

            foreach (var n in theseParams)
            {
                if (n.ToString() == "Avg WS")
                    plotWS = true;

                if (n.ToString() == "Gross AEP")
                    plotGross = true;

                if (n.ToString() == "Net AEP")
                    plotNet = true;

                if (n.ToString() == "Wake Loss")
                    plotWake = true;

                if (n.ToString() == "% Diff")
                    plotWake = true;
            }

            bool needSecondYAxis = false;
            string YAxisTitle = "";

            if (theseParams.Count == 0)
            {
                thisInst.plotMonthlyTS.Refresh();
                return;
            }
            else if (theseParams.Count >= 1)
            {
                for (int i = 0; i < theseParams.Count; i++)
                {
                    if (theseParams[i].ToString() == "Net AEP" || theseParams[i].ToString() == "Gross AEP")
                        needSecondYAxis = true;
                    else if (i == 0)
                        YAxisTitle = theseParams[i].ToString();
                    else
                        YAxisTitle = YAxisTitle + " / " + theseParams[i].ToString();
                }
            }

            model.Title = "Monthly Trends at " + thisTurb.name;

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Month";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = YAxisTitle;
            yAxis.Key = "Primary";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            if (needSecondYAxis == true)
            {
                LinearAxis secYAxis = new LinearAxis();
                secYAxis.Position = AxisPosition.Right;
                secYAxis.Title = "AEP, MWh";
                secYAxis.Key = "Secondary";

                model.Axes.Add(secYAxis);
            }

            Wake_Model thisWakeModel = null;
            if (thisInst.wakeModelList.NumWakeModels > 0 && thisInst.cboMonthlyWakeModel.Items.Count != 0)
            {
                string wakeModelString = thisInst.cboMonthlyWakeModel.SelectedItem.ToString();
                thisWakeModel = thisInst.wakeModelList.GetWakeModelFromString(wakeModelString);
            }

            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Monthly");

            Turbine.Avg_Est thisAvgEst = thisTurb.GetAvgWS_Est(null); // Want free-stream wind speeds
            Turbine.Gross_Energy_Est thisGrossEst = thisTurb.GetGrossEnergyEst(powerCurve);
            Turbine.Net_Energy_Est thisNetEst = thisTurb.GetNetEnergyEst(thisWakeModel);

            if (thisAvgEst.FS_MonthlyVals == null)
                return;

            int numYears = thisInst.chkYears_Monthly.CheckedItems.Count;

            for (int i = 0; i < numYears; i++)
            {
                if (thisInst.chkYears_Monthly.CheckedItems[i].ToString() != "LT Avg")
                {
                    int thisYear = Convert.ToInt16(thisInst.chkYears_Monthly.CheckedItems[i].ToString());

                    if (plotWS == true)
                    {
                        LineSeries thisSeries = new LineSeries();
                        string seriesName = "Avg WS " + thisYear.ToString();
                        thisSeries.Title = seriesName;

                        for (int j = 0; j < thisAvgEst.FS_MonthlyVals.Length; j++)
                            if (thisAvgEst.FS_MonthlyVals[j].year == thisYear)
                                thisSeries.Points.Add(new DataPoint(thisAvgEst.FS_MonthlyVals[j].month, Math.Round(thisAvgEst.FS_MonthlyVals[j].avgWS, 3)));

                        model.Series.Add(thisSeries);
                    }

                    if (plotGross == true && thisGrossEst.AEP != 0)
                    {
                        LineSeries thisSeries = new LineSeries();
                        string seriesName = "Gross Energy " + thisYear.ToString();
                        thisSeries.Title = seriesName;

                        for (int j = 0; j < thisGrossEst.monthlyVals.Length; j++)
                            if (thisGrossEst.monthlyVals[j].year == thisYear)
                                thisSeries.Points.Add(new DataPoint(thisGrossEst.monthlyVals[j].month, Math.Round(thisGrossEst.monthlyVals[j].energyProd, 0)));

                        if (needSecondYAxis)
                            thisSeries.YAxisKey = "Secondary";

                        model.Series.Add(thisSeries);
                    }

                    if (plotNet == true && thisNetEst.AEP != 0)
                    {
                        LineSeries thisSeries = new LineSeries();
                        string seriesName = "Net Energy " + thisYear.ToString();
                        thisSeries.Title = seriesName;

                        for (int j = 0; j < thisNetEst.monthlyVals.Length; j++)
                            if (thisNetEst.monthlyVals[j].year == thisYear)
                                thisSeries.Points.Add(new DataPoint(thisNetEst.monthlyVals[j].month, Math.Round(thisNetEst.monthlyVals[j].energyProd, 0)));

                        if (needSecondYAxis)
                            thisSeries.YAxisKey = "Secondary";

                        model.Series.Add(thisSeries);
                    }

                    if (plotWake == true && thisGrossEst.AEP != 0 && thisNetEst.AEP != 0)
                    {
                        LineSeries thisSeries = new LineSeries();
                        string seriesName = "Wake Loss " + thisYear.ToString();
                        thisSeries.Title = seriesName;

                        for (int j = 0; j < thisNetEst.monthlyVals.Length; j++)
                            if (thisNetEst.monthlyVals[j].year == thisYear)
                            {
                                double otherLoss = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
                                double thisWakeLoss = (thisGrossEst.monthlyVals[j].energyProd - thisNetEst.monthlyVals[j].energyProd / otherLoss) / thisGrossEst.monthlyVals[j].energyProd;
                                thisSeries.Points.Add(new DataPoint(thisNetEst.monthlyVals[j].month, Math.Round(thisWakeLoss, 4)));
                            }

                        model.Series.Add(thisSeries);
                    }

                    if (plotDiff == true && thisGrossEst.AEP != 0 && thisNetEst.AEP == 0) // no net estimates so calculate % diff in gross energy
                    {
                        LineSeries thisSeries = new LineSeries();
                        string seriesName = "% Diff Gross " + thisYear.ToString();
                        thisSeries.Title = seriesName;

                        for (int j = 0; j < thisNetEst.monthlyVals.Length; j++)
                            if (thisNetEst.monthlyVals[j].year == thisYear)
                            {
                                double thisLTValue = thisTurb.CalcLT_MonthlyValue("Gross AEP", thisAvgEst.FS_MonthlyVals[j].month, null, powerCurve);
                                double percDiff = (thisGrossEst.monthlyVals[j].energyProd - thisLTValue) / thisLTValue;
                                thisSeries.Points.Add(new DataPoint(thisNetEst.monthlyVals[j].month, Math.Round(percDiff, 4)));
                            }

                        model.Series.Add(thisSeries);
                    }
                    else if (plotDiff == true && thisGrossEst.AEP != 0 && thisNetEst.AEP != 0) // Calculate % Diff in net energy
                    {
                        LineSeries thisSeries = new LineSeries();
                        string seriesName = "% Diff Net " + thisYear.ToString();
                        thisSeries.Title = seriesName;

                        for (int j = 0; j < thisNetEst.monthlyVals.Length; j++)
                            if (thisNetEst.monthlyVals[j].year == thisYear)
                            {
                                double thisLTValue = thisTurb.CalcLT_MonthlyValue("Net AEP", thisAvgEst.FS_MonthlyVals[j].month, null, powerCurve);
                                double percDiff = (thisGrossEst.monthlyVals[j].energyProd - thisLTValue) / thisLTValue;
                                thisSeries.Points.Add(new DataPoint(thisNetEst.monthlyVals[j].month, Math.Round(percDiff, 4)));
                            }

                        model.Series.Add(thisSeries);
                    }
                }
            }


            // Now add LT Ests (if checked)
            bool showLT = false;
            for (int i = 0; i < thisInst.chkYears_Monthly.CheckedItems.Count; i++)
                if (thisInst.chkYears_Monthly.CheckedItems[i].ToString() == "LT Avg")
                    showLT = true;

            if (showLT == true)
            {

                if (plotWS)
                {
                    WS_Series.Title = "LT Avg WS";
                    WS_Series.LineStyle = LineStyle.Dash;
                    WS_Series.Color = OxyColors.Black;
                    model.Series.Add(WS_Series);
                }

                if (plotGross)
                {
                    Gross_Series.Title = "LT Gross Energy";
                    Gross_Series.LineStyle = LineStyle.Dash;
                    Gross_Series.Color = OxyColors.Black;
                    model.Series.Add(Gross_Series);
                }

                if (plotNet)
                {
                    Net_Series.Title = "LT Net Energy";
                    Net_Series.LineStyle = LineStyle.Dash;
                    Net_Series.Color = OxyColors.Black;
                    model.Series.Add(Net_Series);
                }

                if (plotWake)
                {
                    Wake_Series.Title = "LT Wake Loss";
                    Wake_Series.LineStyle = LineStyle.Dash;
                    Wake_Series.Color = OxyColors.Black;
                    model.Series.Add(Wake_Series);
                }

                // Add LT Estimates to list and plot
                for (int i = 1; i <= 12; i++)
                {
                    double LT_AvgWS = thisTurb.CalcLT_MonthlyValue("Avg WS", i, null, powerCurve);

                    if (plotWS == true) // Add values to plot series                        
                        WS_Series.Points.Add(new DataPoint(i, Math.Round(LT_AvgWS, 3)));

                    if (plotGross && thisGrossEst.AEP != 0)
                    {
                        double LT_GrossAEP = thisTurb.CalcLT_MonthlyValue("Gross AEP", i, null, powerCurve);
                        Gross_Series.Points.Add(new DataPoint(i, Math.Round(LT_GrossAEP, 0)));

                        if (needSecondYAxis)
                            Gross_Series.YAxisKey = "Secondary";
                        else
                            Gross_Series.YAxisKey = "Primary";

                    }

                    if (plotNet && thisNetEst.AEP != 0)
                    {
                        double LT_NetAEP = thisTurb.CalcLT_MonthlyValue("Net AEP", i, thisWakeModel, powerCurve);
                        objListItem.SubItems.Add(Math.Round(LT_NetAEP, 0).ToString());

                        Net_Series.Points.Add(new DataPoint(i, Math.Round(LT_NetAEP, 0)));

                        if (needSecondYAxis)
                            Net_Series.YAxisKey = "Secondary";
                        else
                            Net_Series.YAxisKey = "Primary";
                    }

                    if (plotWake == true && thisGrossEst.AEP != 0 && thisNetEst.AEP != 0)
                    {
                        double otherLoss = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
                        double LT_GrossAEP = thisTurb.CalcLT_MonthlyValue("Gross AEP", i, null, powerCurve);
                        double LT_NetAEP = thisTurb.CalcLT_MonthlyValue("Net AEP", i, thisWakeModel, powerCurve);
                        if (LT_NetAEP == 0) return;
                        double thisWakeLoss = (LT_GrossAEP - LT_NetAEP / otherLoss) / LT_GrossAEP;

                        Wake_Series.Points.Add(new DataPoint(i, Math.Round(100 * thisWakeLoss, 3)));
                    }

                }
            }

            thisInst.plotMonthlyTS.Refresh();
        }

        /// <summary> Updates zone list and zone dropdown on Site Suitability tab </summary> 
        public void ZoneList(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            thisInst.lstZones.Items.Clear();
            thisInst.cboZoneList.Items.Clear();

            if (thisInst.siteSuitability.zones == null)
            {
                thisInst.okToUpdate = true;
                return;
            }

            for (int i = 0; i < thisInst.siteSuitability.zones.Length; i++)
            {
                ListViewItem objListItem = thisInst.lstZones.Items.Add(thisInst.siteSuitability.zones[i].name);
                thisInst.lstZones.Items[i].Checked = true;
                objListItem.SubItems.Add(Math.Round(thisInst.siteSuitability.zones[i].latitude, 3).ToString());
                objListItem.SubItems.Add(Math.Round(thisInst.siteSuitability.zones[i].longitude, 3).ToString());
                objListItem.SubItems.Add(thisInst.siteSuitability.zones[i].xSize.ToString());
                objListItem.SubItems.Add(thisInst.siteSuitability.zones[i].ySize.ToString());

                thisInst.cboZoneList.Items.Add(thisInst.siteSuitability.zones[i].name);
            }

            if (thisInst.siteSuitability.zones != null)
                if (thisInst.siteSuitability.zones.Length > 0)
                    thisInst.cboZoneList.SelectedIndex = 0;

            thisInst.okToUpdate = true;

        }

        /// <summary> Creates a surface plot showing of ice throw surrounding turbines and specified zones. </summary>        
        public void IceThrowSurfacePlot(Continuum thisInst)
        {
            thisInst.plotIceShadowSound.Model = new PlotModel();
            var model = thisInst.plotIceShadowSound.Model;
            model.Title = "Ice Hits over Annual Period";
            model.IsLegendVisible = false;

            if (thisInst.siteSuitability.yearlyIceHits.Length == 0)
            {
                thisInst.plotIceShadowSound.Refresh();
                return;
            }

            // Create grid that covers turbines +/- 3 km
            HeatMapSeries zoneSurface = new HeatMapSeries();

            if (thisInst.siteSuitability.mapMinBounds.UTMX == 0)
                thisInst.siteSuitability.FindShadowMapBounds(thisInst, true);

            TopoInfo.UTM_X_Y mapMinBounds = thisInst.siteSuitability.mapMinBounds;
            TopoInfo.UTM_X_Y mapMaxBounds = thisInst.siteSuitability.mapMaxBounds;

            zoneSurface.X0 = mapMinBounds.UTMX;
            zoneSurface.Y0 = mapMinBounds.UTMY;
            zoneSurface.X1 = mapMaxBounds.UTMX;
            zoneSurface.Y1 = mapMaxBounds.UTMY;

            model.Axes.Add(new OxyPlot.Axes.LinearColorAxis
            {
                Palette = OxyPalettes.Jet(500),
                HighColor = OxyColors.Gray,
                LowColor = OxyColors.Gray,
                Minimum = 1
            });

            int surfaceReso = 5;
            int numX = ((int)mapMaxBounds.UTMX - (int)mapMinBounds.UTMX) / surfaceReso + 1;
            int numY = ((int)mapMaxBounds.UTMY - (int)mapMinBounds.UTMY) / surfaceReso + 1;

            double[,] background = new double[numX, numY];
            for (int i = 0; i < numX; i++)
                for (int j = 0; j < numY; j++)
                    background[i, j] = 1;

            zoneSurface.Data = background;

            model.Series.Add(zoneSurface);

            ScatterSeries iceThrowSeries = new ScatterSeries();
            iceThrowSeries.Title = "Ice";
            iceThrowSeries.MarkerFill = OxyColors.Silver;
            model.Series.Add(iceThrowSeries);

            if (thisInst.turbineList.TurbineCount == 0)
            {
                thisInst.plotIceShadowSound.Refresh();
                return;
            }

            int yearToShow = Convert.ToInt16(thisInst.cboIcingYear.SelectedItem.ToString());

            for (int i = 0; i < thisInst.siteSuitability.yearlyIceHits[yearToShow - 1].iceHits.Length; i++)
            {
                double thisX = thisInst.siteSuitability.yearlyIceHits[yearToShow - 1].iceHits[i].thisX;
                double thisY = thisInst.siteSuitability.yearlyIceHits[yearToShow - 1].iceHits[i].thisZ;
                iceThrowSeries.Points.Add(new ScatterPoint(thisX, thisY, 2, 2));
            }

            SiteSuitabilitySurfacePlotLabels(thisInst, -999, false);
            thisInst.plotIceShadowSound.Refresh();

        }

        /// <summary> Updates labels on model surface plot on Site Suitability tab. </summary>        
        public void SiteSuitabilitySurfacePlotLabels(Continuum thisInst, int changedIndex, bool changedItemChecked)
        {
            if (thisInst.plotIceShadowSound.Model == null)
                return;

            int numZones = thisInst.siteSuitability.GetNumZones();
            ScatterSeries[] zones = new ScatterSeries[numZones];
            var model = thisInst.plotIceShadowSound.Model;

            for (int i = 0; i < numZones; i++)
            {
                bool isChecked = false;
                SiteSuitability.Zone thisZone = thisInst.siteSuitability.zones[i];

                for (int j = 0; j < thisInst.lstZones.CheckedItems.Count; j++)
                    if (thisInst.lstZones.CheckedItems[j].Text == thisZone.name)
                        isChecked = true;

                if (i == changedIndex)
                    isChecked = changedItemChecked;

                if (isChecked == true)
                {
                    zones[i] = new ScatterSeries();
                    zones[i].MarkerFill = OxyColors.Blue;
                    zones[i].Title = thisZone.name;
                    zones[i].RenderInLegend = false;

                    // Convert lat/long to UTM
                    UTM_conversion.UTM_coords theseUTM = thisInst.UTM_conversions.LLtoUTM(thisZone.latitude, thisZone.longitude);
                    zones[i].Points.Add(new ScatterPoint(theseUTM.UTMEasting, theseUTM.UTMNorthing, 5, 2));
                    model.Series.Add(zones[i]);
                }
            }

            // Create point series for turbines
            int numTurbs = thisInst.turbineList.TurbineCount;
            ScatterSeries[] turbines = new ScatterSeries[numTurbs];

            for (int i = 0; i < numTurbs; i++)
            {
                Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                turbines[i] = new ScatterSeries();
                turbines[i].MarkerFill = OxyColors.White;
                turbines[i].MarkerType = MarkerType.Diamond;
                turbines[i].Title = thisTurb.name;
                turbines[i].RenderInLegend = false;

                turbines[i].Points.Add(new ScatterPoint(thisTurb.UTMX, thisTurb.UTMY, 5, 2));
                model.Series.Add(turbines[i]);
            }

            thisInst.plotIceShadowSound.Refresh();

        }

        /// <summary> Updates dropdown menu on Site Suitability tab. </summary>
        public void SiteSuitabilityDropdown(Continuum thisInst, string modelToSelect)
        {
            thisInst.okToUpdate = false;

            thisInst.cboSiteSuitabilitySelectPlot.Items.Clear();

            if (thisInst.siteSuitability.yearlyIceHits.Length != 0)
                thisInst.cboSiteSuitabilitySelectPlot.Items.Add("Ice Throw");

            if (thisInst.siteSuitability.flickerMap.Length > 0)
                thisInst.cboSiteSuitabilitySelectPlot.Items.Add("Shadow Flicker");

            if (thisInst.siteSuitability.soundMap.Length != 0)
                thisInst.cboSiteSuitabilitySelectPlot.Items.Add("Sound");

            if (thisInst.cboSiteSuitabilitySelectPlot.Items.Count > 0)
            {
                if (modelToSelect == null)
                    thisInst.cboSiteSuitabilitySelectPlot.SelectedIndex = 0;
                else
                {
                    for (int i = 0; i < thisInst.cboSiteSuitabilitySelectPlot.Items.Count; i++)
                    {
                        if (modelToSelect == thisInst.cboSiteSuitabilitySelectPlot.Items[i].ToString())
                        {
                            thisInst.cboSiteSuitabilitySelectPlot.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }

            thisInst.okToUpdate = true;

        }

        /// <summary> Updates map to show total shadow flicker of selected month and hour at each grid node. </summary>        
        public void ShadowFlickerSurfacePlot(Continuum thisInst)
        {
            string thisMonthStr = thisInst.cboSiteSuitMonth.SelectedItem.ToString();
            string thisHourStr = thisInst.cboSiteSuitHour.SelectedItem.ToString();

            int monthInd = GetMonthInd(thisMonthStr);
            int hourInd = 100;

            if (thisHourStr != "All")
                hourInd = GetHourFromHourString(thisHourStr);

            // Create grid array with total number of shadow flicker hours            
            SiteSuitability.FlickerGrid[] plotFlicker = thisInst.siteSuitability.GetTotalFlickerHoursByMonthAndHour(monthInd, hourInd);

            thisInst.plotIceShadowSound.Model = new PlotModel();
            var model = thisInst.plotIceShadowSound.Model;
            model.Title = "Total Number of Shadow Flicker Hours";

            if (plotFlicker.Length == 0)
            {
                thisInst.plotIceShadowSound.Refresh();
                return;
            }

            // Create grid that covers turbines +/- 3 km
            HeatMapSeries flickerSurface = new HeatMapSeries();

            int maxValue = 0;

            for (int i = 0; i < plotFlicker.Length; i++)
                if (plotFlicker[i].flickerStats.totalShadowMinsPerYear > maxValue)
                    maxValue = plotFlicker[i].flickerStats.totalShadowMinsPerYear;

            maxValue = maxValue + 5;

            model.Axes.Add(new LinearColorAxis
            {
                Position = AxisPosition.Right,
                Palette = OxyPalettes.Jet(15),
                HighColor = OxyColors.Red,
                LowColor = OxyColors.Gray,
                Minimum = 0,
                Maximum = maxValue
            });

            flickerSurface.X0 = thisInst.siteSuitability.mapMinBounds.UTMX;
            flickerSurface.X1 = thisInst.siteSuitability.mapMaxBounds.UTMX;
            flickerSurface.Y0 = thisInst.siteSuitability.mapMinBounds.UTMY;
            flickerSurface.Y1 = thisInst.siteSuitability.mapMaxBounds.UTMY;

            double[,] flickerGrid = new double[thisInst.siteSuitability.numXFlicker, thisInst.siteSuitability.numYFlicker];

            int flickerMapInd = 0;

            for (int i = 0; i < thisInst.siteSuitability.numXFlicker; i++)
                for (int j = 0; j < thisInst.siteSuitability.numYFlicker; j++)
                {
                    flickerGrid[i, j] = plotFlicker[flickerMapInd].flickerStats.totalShadowMinsPerYear;
                    flickerMapInd++;
                }

            flickerSurface.Data = flickerGrid;
            model.Series.Add(flickerSurface);

            SiteSuitabilitySurfacePlotLabels(thisInst, -999, true);
            thisInst.plotIceShadowSound.Refresh();

        }

        /// <summary> Returns hour as integer based on string from dropdown on Site Suitability. </summary>  
        public int GetHourFromHourString(string hourStr)
        {
            int thisHour = 0;

            if (hourStr == "4 am")
                thisHour = 4;
            else if (hourStr == "5 am")
                thisHour = 5;
            else if (hourStr == "6 am")
                thisHour = 6;
            else if (hourStr == "7 am")
                thisHour = 7;
            else if (hourStr == "8 am")
                thisHour = 8;
            else if (hourStr == "9 am")
                thisHour = 9;
            else if (hourStr == "10 am")
                thisHour = 10;
            else if (hourStr == "11 am")
                thisHour = 11;
            else if (hourStr == "12 pm")
                thisHour = 12;
            else if (hourStr == "1 pm")
                thisHour = 13;
            else if (hourStr == "2 pm")
                thisHour = 14;
            else if (hourStr == "3 pm")
                thisHour = 15;
            else if (hourStr == "4 pm")
                thisHour = 16;
            else if (hourStr == "5 pm")
                thisHour = 17;
            else if (hourStr == "6 pm")
                thisHour = 18;
            else if (hourStr == "7 pm")
                thisHour = 19;
            else if (hourStr == "8 pm")
                thisHour = 20;
            else if (hourStr == "9 pm")
                thisHour = 21;
            else if (hourStr == "10 pm")
                thisHour = 22;

            return thisHour;
        }

        /// <summary> Returns month as integer based on month string. </summary>        
        public int GetMonthInd(string monthString)
        {
            int monthInd = 0;

            if (monthString == "Jan")
                monthInd = 0;
            else if (monthString == "Feb")
                monthInd = 1;
            else if (monthString == "Mar")
                monthInd = 2;
            else if (monthString == "Apr")
                monthInd = 3;
            else if (monthString == "May")
                monthInd = 4;
            else if (monthString == "Jun")
                monthInd = 5;
            else if (monthString == "Jul")
                monthInd = 6;
            else if (monthString == "Aug")
                monthInd = 7;
            else if (monthString == "Sep")
                monthInd = 8;
            else if (monthString == "Oct")
                monthInd = 9;
            else if (monthString == "Nov")
                monthInd = 10;
            else if (monthString == "Dec")
                monthInd = 11;
            else
                monthInd = 100;

            return monthInd;
        }

        /// <summary> Returns month string based on month as integer. </summary>    
        public string GetMonthString(int monthInd)
        {
            string thisMonth = "";

            if (monthInd == 1)
                thisMonth = "JAN";
            else if (monthInd == 2)
                thisMonth = "FEB";
            else if (monthInd == 3)
                thisMonth = "MAR";
            else if (monthInd == 4)
                thisMonth = "APR";
            else if (monthInd == 5)
                thisMonth = "MAY";
            else if (monthInd == 6)
                thisMonth = "JUN";
            else if (monthInd == 7)
                thisMonth = "JUL";
            else if (monthInd == 8)
                thisMonth = "AUG";
            else if (monthInd == 9)
                thisMonth = "SEP";
            else if (monthInd == 10)
                thisMonth = "OCT";
            else if (monthInd == 11)
                thisMonth = "NOV";
            else if (monthInd == 12)
                thisMonth = "DEC";

            return thisMonth;
        }

        /// <summary>  Updates the textbox showing the daily max number of flicker hours and date of occurrence on Site Suitability tab. </summary> 
        public void ShadowFlickerMaxDay(Continuum thisInst)
        {
            SiteSuitability.Zone zone = GetSelectedZone(thisInst);

            if (zone.flickerStats.maxDailyShadowMins > 0)
            {
                thisInst.dateMaxFlicker.Value = zone.flickerStats.maxShadowDay;
                thisInst.txtMaxFlickerHours.Text = zone.flickerStats.maxDailyShadowMins.ToString();
            }
            else
            {
                thisInst.dateMaxFlicker.Value = DateTime.Today;
                thisInst.txtMaxFlickerHours.Text = "0";
            }

        }

        /// <summary> Returns zone selected on list on Site Suitabiity tab. </summary> 
        public SiteSuitability.Zone GetSelectedZone(Continuum thisInst)
        {
            string zoneStr = "";
            SiteSuitability.Zone zone = new SiteSuitability.Zone();

            try
            {
                zoneStr = thisInst.cboZoneList.SelectedItem.ToString();
            }
            catch
            {
                return zone;
            }

            for (int i = 0; i < thisInst.siteSuitability.GetNumZones(); i++)
                if (thisInst.siteSuitability.zones[i].name == zoneStr)
                    return thisInst.siteSuitability.zones[i];

            return zone;
        }

        /// <summary> Updates in Shadow Flicker 12x24 table and textbox on Site Suitability tab. </summary> 
        public void ShadowFlicker12x24(Continuum thisInst)
        {
            thisInst.lstShadow12x24.Items.Clear();
            thisInst.txtTotalShadow.Text = "";

            SiteSuitability.Zone thisZone = GetSelectedZone(thisInst);

            if (thisZone.flickerStats.shadowMins12x24 == null)
                return;

            double totalShadowHours = thisInst.siteSuitability.GetTotalFlickerHours(thisZone, 100, 100);
            thisInst.txtTotalShadow.Text = Math.Round(totalShadowHours, 1).ToString();

            for (int h = 4; h < 22; h++)
                for (int m = 0; m < 12; m++)
                {
                    if (m == 0) // add "All Months" and Hour name
                    {
                        string hourStr = GetHourString(h);
                        objListItem = thisInst.lstShadow12x24.Items.Add(hourStr);
                        totalShadowHours = thisInst.siteSuitability.GetTotalFlickerHours(thisZone, 100, h);
                        objListItem.SubItems.Add(Math.Round(totalShadowHours, 1).ToString());
                        totalShadowHours = thisInst.siteSuitability.GetTotalFlickerHours(thisZone, m, h);
                        objListItem.SubItems.Add(Math.Round(totalShadowHours, 1).ToString());
                    }
                    else
                    {
                        totalShadowHours = thisInst.siteSuitability.GetTotalFlickerHours(thisZone, m, h);
                        objListItem.SubItems.Add(Math.Round(totalShadowHours, 1).ToString());
                    }

                }

        }

        /// <summary> Updates the list showing total number of shadow flicker hours at each zone on Site Suitability tab . </summary> 
        public void ZoneShadowSummary(Continuum thisInst)
        {
            thisInst.lstShadowZoneSummary.Items.Clear();
            thisInst.lstShadowZoneSummary.Columns[0].Text = "Zone";
            thisInst.lstShadowZoneSummary.Columns[1].Text = "Total Hours";

            if (thisInst.siteSuitability.GetNumZones() > 0)
                if (thisInst.siteSuitability.zones[0].flickerStats.shadowMins12x24 == null)
                    return;

            for (int i = 0; i < thisInst.siteSuitability.GetNumZones(); i++)
            {
                double totalNumberShadow = thisInst.siteSuitability.GetTotalFlickerHours(thisInst.siteSuitability.zones[i], 100, 100);
                objListItem = thisInst.lstShadowZoneSummary.Items.Add(thisInst.siteSuitability.zones[i].name);
                objListItem.SubItems.Add(Math.Round(totalNumberShadow, 1).ToString());
            }
        }

        /// <summary> Returns a string corresponding to an integer hour. </summary> 
        public string GetHourString(int thisHourInd)
        {
            string hourStr = "12 am";

            if (thisHourInd > 0 && thisHourInd < 13)
                hourStr = thisHourInd + " am";
            else if (thisHourInd > 0)
                hourStr = thisHourInd - 12 + " pm";

            return hourStr;
        }

        /// <summary> Update the yearly ice hits table on Site Suitability tab. </summary> 
        public void IceHitsByZone(Continuum thisInst)
        {
            thisInst.lstZoneIceHits.Items.Clear();

            if (thisInst.siteSuitability.yearlyIceHits.Length == 0)
                return;

            int yearToShow = Convert.ToInt16(thisInst.cboIcingYear.SelectedItem.ToString());

            for (int i = 0; i < thisInst.siteSuitability.GetNumZones(); i++)
            {
                SiteSuitability.Zone zone = thisInst.siteSuitability.zones[i];
                double avgHits = 0;
                double minHits = 100000;
                double maxHits = 0;

                double hitsThisYear = thisInst.siteSuitability.GetTotalNumberOfIceHitsAtZone(yearToShow - 1, zone, thisInst);

                for (int y = 0; y < thisInst.siteSuitability.numYearsToModel; y++)
                {
                    int numHits = thisInst.siteSuitability.GetTotalNumberOfIceHitsAtZone(y, zone, thisInst);
                    avgHits = avgHits + numHits;

                    if (numHits < minHits)
                        minHits = numHits;

                    if (numHits > maxHits)
                        maxHits = numHits;

                }

                if (thisInst.siteSuitability.numYearsToModel > 0)
                    avgHits = avgHits / thisInst.siteSuitability.numYearsToModel;

                double probOneOrMore = thisInst.siteSuitability.CalcProbabilityOfHits(zone, 1, thisInst);
                double probTwoOrMore = thisInst.siteSuitability.CalcProbabilityOfHits(zone, 2, thisInst);

                objListItem = thisInst.lstZoneIceHits.Items.Add(thisInst.siteSuitability.zones[i].name);
                objListItem.SubItems.Add(hitsThisYear.ToString());
                objListItem.SubItems.Add(minHits.ToString());
                objListItem.SubItems.Add(maxHits.ToString());
                objListItem.SubItems.Add(probOneOrMore.ToString("P"));
                objListItem.SubItems.Add(probTwoOrMore.ToString("P"));
            }
        }

        /// <summary> Creates a surface plot of turbine sound model on Site Suitability tab. </summary>        
        public void SoundMap(Continuum thisInst)
        {
            thisInst.plotIceShadowSound.Model = new PlotModel();
            var model = thisInst.plotIceShadowSound.Model;
            model.Title = "Estimated Sound Levels (dBA)";

            // Create grid that covers turbines +/- 3 km
            HeatMapSeries soundSurface = new HeatMapSeries();
            soundSurface.Data = thisInst.siteSuitability.soundMap;

            model.Axes.Add(new OxyPlot.Axes.LinearColorAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Right,
                Palette = OxyPalettes.Jet(15),
                HighColor = OxyColors.Red,
                LowColor = OxyColors.Gray,
                Minimum = 0,
                Maximum = thisInst.topo.GetMax(thisInst.siteSuitability.soundMap)
            });

            thisInst.siteSuitability.FindShadowMapBounds(thisInst, false);
            soundSurface.X0 = thisInst.siteSuitability.mapMinBounds.UTMX;
            soundSurface.X1 = thisInst.siteSuitability.mapMaxBounds.UTMX;
            soundSurface.Y0 = thisInst.siteSuitability.mapMinBounds.UTMY;
            soundSurface.Y1 = thisInst.siteSuitability.mapMaxBounds.UTMY;

            model.Series.Add(soundSurface);

            SiteSuitabilitySurfacePlotLabels(thisInst, -999, true);
            thisInst.plotIceShadowSound.Refresh();

        }

        /// <summary> Updates estimated sound level at turbine sites on Site Suitability tab. </summary>
        public void SoundAtZones(Continuum thisInst)
        {
            thisInst.lstZoneSound.Items.Clear();

            if (thisInst.siteSuitability.soundMap.GetUpperBound(0) == -1)
                return;

            int thisBound = thisInst.siteSuitability.soundMap.GetUpperBound(0);

            for (int i = 0; i < thisInst.siteSuitability.GetNumZones(); i++)
            {
                SiteSuitability.Zone zone = thisInst.siteSuitability.zones[i];
                UTM_conversion.UTM_coords theseUTM = thisInst.UTM_conversions.LLtoUTM(zone.latitude, zone.longitude);
                double thisSound = thisInst.siteSuitability.CalcNoiseLevel((int)theseUTM.UTMEasting, (int)theseUTM.UTMNorthing, thisInst);
                objListItem = thisInst.lstZoneSound.Items.Add(thisInst.siteSuitability.zones[i].name);
                objListItem.SubItems.Add(Math.Round(thisSound, 2).ToString());
            }
        }

        /// <summary> Updates all tables and plots on Exceedance tab. </summary>
        public void Exceedance_TAB(Continuum thisInst)
        {
            PerformanceFactorList(thisInst);
            PerformanceFactorsPlot(thisInst);
            PValsTable(thisInst);

        }

        /// <summary> Updates Performance Curves table on Exceedance tab. </summary>
        public void PerformanceFactorList(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            thisInst.lstDefinedLosses.Items.Clear();

            if (thisInst.turbineList.exceed == null)
            {
                thisInst.okToUpdate = true;
                return;
            }

            // Table columns: Name, P10, P50, P90  
            for (int i = 0; i < thisInst.turbineList.exceed.Num_Exceed(); i++)
            {
                objListItem = new ListViewItem(thisInst.turbineList.exceed.exceedCurves[i].exceedStr);
                objListItem.Checked = true;
                Exceedance.ExceedanceCurve exceedCurve = thisInst.turbineList.exceed.exceedCurves[i];

                // P10
                double pVal = 0.9;
                double perfFact = Math.Round(thisInst.turbineList.exceed.Get_PF_Value(pVal, exceedCurve), 3);
                objListItem.SubItems.Add(perfFact.ToString("P"));

                // P50
                pVal = 0.5;
                perfFact = Math.Round(thisInst.turbineList.exceed.Get_PF_Value(pVal, exceedCurve), 3);
                objListItem.SubItems.Add(perfFact.ToString("P"));

                // P90
                pVal = 0.1;
                perfFact = Math.Round(thisInst.turbineList.exceed.Get_PF_Value(pVal, exceedCurve), 3);
                objListItem.SubItems.Add(perfFact.ToString("P"));

                // Lower and Upper bounds
                objListItem.SubItems.Add(Math.Round(exceedCurve.lowerBound, 4).ToString("P"));
                objListItem.SubItems.Add(Math.Round(exceedCurve.upperBound, 4).ToString("P"));

                if (exceedCurve.modes != null)
                {
                    if (exceedCurve.modes.Length == 1) // normal standard deviation
                    {
                        objListItem.SubItems.Add(exceedCurve.modes[0].mean.ToString("P"));
                        objListItem.SubItems.Add(exceedCurve.modes[0].SD.ToString("P"));
                    }
                }

                thisInst.lstDefinedLosses.Items.Add(objListItem);
            }

            thisInst.lstDefinedLosses.Columns[0].Width = 290; // width of first columns: name of performance factor
            thisInst.okToUpdate = true;
        }

        /// <summary> Plots probability and cumulative density function of all (checked) defined exceedance on Exceedance tab. </summary>
        public void PerformanceFactorsPlot(Continuum thisInst)
        {
            thisInst.plotExceedCurves.Model = new PlotModel();
            var model = thisInst.plotExceedCurves.Model;
            model.IsLegendVisible = false;

            if (thisInst.turbineList.exceed == null)
                return;

            int numDefined = thisInst.turbineList.exceed.Num_Exceed();

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Performance Factor";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Probability";
            yAxis.Key = "PDF";
            LinearAxis secYAxis = new LinearAxis();
            secYAxis.Position = AxisPosition.Right;
            secYAxis.Title = "Cumulative Probability";
            secYAxis.Key = "CDF";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);
            model.Axes.Add(secYAxis);

            for (int i = 0; i < numDefined; i++)
            {
                if (thisInst.lstDefinedLosses.Items[i].Checked == true)
                {
                    if (thisInst.chkShowPDF.Checked)
                    {
                        string PDF_name = "PDF " + thisInst.turbineList.exceed.exceedCurves[i].exceedStr;

                        var pdfSeries = new LineSeries();
                        pdfSeries.Title = PDF_name;
                        model.Series.Add(pdfSeries);
                        pdfSeries.YAxisKey = "PDF";

                        for (int j = 0; j < thisInst.turbineList.exceed.exceedCurves[i].distSize; j++)
                            pdfSeries.Points.Add(new DataPoint(Math.Round(thisInst.turbineList.exceed.exceedCurves[i].xVals[j] * 100, 4),
                                Math.Round(thisInst.turbineList.exceed.exceedCurves[i].probDist[j], 4)));

                    }

                    if (thisInst.chkShowCDFs.Checked)
                    {
                        string CDF_name = "CDF " + thisInst.turbineList.exceed.exceedCurves[i].exceedStr;

                        var cdfSeries = new LineSeries();
                        cdfSeries.Title = CDF_name;
                        model.Series.Add(cdfSeries);
                        cdfSeries.YAxisKey = "CDF";

                        for (int j = 0; j < thisInst.turbineList.exceed.exceedCurves[i].distSize; j++)
                            cdfSeries.Points.Add(new DataPoint(Math.Round(thisInst.turbineList.exceed.exceedCurves[i].xVals[j] * 100, 4),
                                Math.Round(thisInst.turbineList.exceed.exceedCurves[i].cumulDist[j], 4)));
                    }

                }
            }

            thisInst.plotExceedCurves.Refresh();

        }

        /// <summary> Updates the table and plot of P values on Exceedance tab. </summary>
        public void PValsTable(Continuum thisInst)
        {
            thisInst.plotCompositeExceed.Model = new PlotModel();
            var model = thisInst.plotCompositeExceed.Model;
            model.IsLegendVisible = false;

            thisInst.lstPvals.Items.Clear();

            if (thisInst.turbineList.exceed == null)
            {
                thisInst.plotCompositeExceed.Refresh();
                return;
            }

            model.Title = "Composite P-Value Distribution";

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Overall Performance Factor";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "P-Value";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            if (thisInst.turbineList.exceed.compositeLoss.isComplete == false)
            {
                thisInst.plotCompositeExceed.Refresh();
                return;
            }

            Wake_Model thisWakeModel = new Wake_Model();

            if (thisInst.cboExceedWake.Items.Count > 0)
            {
                string wakeModelString = thisInst.cboExceedWake.SelectedItem.ToString();
                thisWakeModel = thisInst.wakeModelList.GetWakeModelFromString(wakeModelString);
            }

            Turbine thisTurb = thisInst.GetSelectedTurbine("Exceedance");
            Turbine.Net_Energy_Est netEst = thisTurb.GetNetEnergyEst(thisWakeModel);
            double overallP50 = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
            double AEP = netEst.AEP / overallP50;

            var pVal1yr = new LineSeries();
            pVal1yr.Title = "1-year P-Values";
            model.Series.Add(pVal1yr);

            var pVal10yrs = new LineSeries();
            pVal10yrs.Title = "10-year P-Values";
            model.Series.Add(pVal10yrs);

            var pVal20yrs = new LineSeries();
            pVal20yrs.Title = "20-year P-Values";
            model.Series.Add(pVal20yrs);

            for (int i = 0; i < 99; i++)
            {
                pVal1yr.Points.Add(new DataPoint(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[98 - i], 4), i + 1));
                pVal10yrs.Points.Add(new DataPoint(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[98 - i], 4), i + 1));
                pVal20yrs.Points.Add(new DataPoint(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[98 - i], 4), i + 1));
            }

            // P1
            ListViewItem objListItem_1 = new ListViewItem("P1");
            // 1 year values
            objListItem_1.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[0], 3).ToString());
            if (AEP != 0)
                objListItem_1.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[0] * AEP, 2)));
            else
                objListItem_1.SubItems.Add("");
            // 10 year values
            objListItem_1.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[0], 3).ToString());
            if (AEP != 0)
                objListItem_1.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[0] * AEP, 2)));
            else
                objListItem_1.SubItems.Add("");
            // 20 year values
            objListItem_1.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[0], 3).ToString());
            if (AEP != 0)
                objListItem_1.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[0] * AEP, 2)));
            else
                objListItem_1.SubItems.Add("");

            thisInst.lstPvals.Items.Add(objListItem_1);

            // P10
            ListViewItem objListItem_10 = new ListViewItem("P10");

            // 1 year values
            objListItem_10.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[9], 3).ToString());
            if (AEP != 0)
                objListItem_10.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[9] * AEP, 2)));
            else
                objListItem_10.SubItems.Add("");
            // 10 year values
            objListItem_10.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[9], 3).ToString());
            if (AEP != 0)
                objListItem_10.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[9] * AEP, 2)));
            else
                objListItem_10.SubItems.Add("");
            // 20 year values
            objListItem_10.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[9], 3).ToString());
            if (AEP != 0)
                objListItem_10.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[9] * AEP, 2)));
            else
                objListItem_10.SubItems.Add("");

            thisInst.lstPvals.Items.Add(objListItem_10);

            // P50
            ListViewItem objListItem_50 = new ListViewItem("P50");

            // 1 year values
            objListItem_50.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[49], 3).ToString());
            if (AEP != 0)
                objListItem_50.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[49] * AEP, 2)));
            else
                objListItem_50.SubItems.Add("");
            // 10 year values
            objListItem_50.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[49], 3).ToString());
            if (AEP != 0)
                objListItem_50.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[49] * AEP, 2)));
            else
                objListItem_50.SubItems.Add("");
            // 20 year values
            objListItem_50.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[49], 3).ToString());
            if (AEP != 0)
                objListItem_50.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[49] * AEP, 2)));
            else
                objListItem_50.SubItems.Add("");

            thisInst.lstPvals.Items.Add(objListItem_50);

            // P90
            ListViewItem objListItem_90 = new ListViewItem("P90");
            // 1 year values
            objListItem_90.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[89], 3).ToString());
            if (AEP != 0)
                objListItem_90.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[89] * AEP, 2)));
            else
                objListItem_90.SubItems.Add("");
            // 10 year values
            objListItem_90.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[89], 3).ToString());
            if (AEP != 0)
                objListItem_90.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[89] * AEP, 2)));
            else
                objListItem_90.SubItems.Add("");
            // 20 year values
            objListItem_90.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[89], 3).ToString());
            if (AEP != 0)
                objListItem_90.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[89] * AEP, 2)));
            else
                objListItem_90.SubItems.Add("");

            thisInst.lstPvals.Items.Add(objListItem_90);

            // P99
            ListViewItem objListItem_99 = new ListViewItem("P99");
            // 1 year values
            objListItem_99.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[98], 3).ToString());
            if (AEP != 0)
                objListItem_99.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[98] * AEP, 2)));
            else
                objListItem_99.SubItems.Add("");
            // 10 year values
            objListItem_99.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[98], 3).ToString());
            if (AEP != 0)
                objListItem_99.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[98] * AEP, 2)));
            else
                objListItem_99.SubItems.Add("");
            // 20 year values
            objListItem_99.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[98], 3).ToString());
            if (AEP != 0)
                objListItem_99.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[98] * AEP, 2)));
            else
                objListItem_99.SubItems.Add("");

            thisInst.lstPvals.Items.Add(objListItem_99);

            thisInst.lstPvals.Columns[0].Width = 70;
            thisInst.lstPvals.Columns[1].Width = 60;
            thisInst.lstPvals.Columns[2].Width = 70;
            thisInst.lstPvals.Columns[3].Width = 60;
            thisInst.lstPvals.Columns[4].Width = 70;
            thisInst.lstPvals.Columns[5].Width = 60;
            thisInst.lstPvals.Columns[6].Width = 70;

            thisInst.plotCompositeExceed.Refresh();
        }

        /// <summary> Updates visibility of plots and tables on Site Suitability tab based on selected model. </summary>
        public void SiteSuitabilityVisibility(Continuum thisInst)
        {
            thisInst.okToUpdate = false;
            string selectedModel = thisInst.GetSelectedSiteSuitabilityModel();

            if (selectedModel == "Ice Throw")
            {
                thisInst.lstShadow12x24.Visible = false;
                thisInst.plotIceVsDist.Visible = true;

                thisInst.lblShadowByMonthOrIceByDist.Text = "Ice Hits by Distance";
                thisInst.lblZoneOrDirection.Text = "WD";
                thisInst.lblShadowOrIceByDist.Text = "Ice Hits by Dist.";

                thisInst.cboZoneList.Items.Clear();
                thisInst.lblIceDistOrHisto.Visible = true;
                thisInst.cboIceDistORIceHisto.Visible = true;

                if (thisInst.cboIceDistORIceHisto.SelectedItem == null)
                    thisInst.cboIceDistORIceHisto.SelectedIndex = 0;

                if (thisInst.siteSuitability.GetNumZones() == 0)
                    thisInst.cboIceDistORIceHisto.Enabled = false;
                else
                    thisInst.cboIceDistORIceHisto.Enabled = true;

                string plotSelected = thisInst.cboIceDistORIceHisto.SelectedItem.ToString();

                if (plotSelected == "Ice Hit vs. Distance" || thisInst.siteSuitability.GetNumZones() > 0)
                {
                    for (int i = 0; i < thisInst.metList.numWD; i++)
                        thisInst.cboZoneList.Items.Add(Math.Round((double)i * 360 / thisInst.metList.numWD, 1));

                    thisInst.cboZoneList.Items.Add("All");
                    thisInst.cboZoneList.SelectedIndex = thisInst.metList.numWD;
                }
                else if (thisInst.siteSuitability.GetNumZones() > 0)
                {
                    for (int i = 0; i < thisInst.siteSuitability.GetNumZones(); i++)
                        thisInst.cboZoneList.Items.Add(thisInst.siteSuitability.zones[i].name);

                    thisInst.cboZoneList.SelectedIndex = 0;
                }

                thisInst.lblTotalHoursPerYear.Visible = false;
                thisInst.txtTotalShadow.Visible = false;

                thisInst.cboSiteSuitHour.Enabled = false;
                thisInst.cboSiteSuitMonth.Enabled = false;
                thisInst.cboIcingYear.Enabled = true;

                thisInst.lblMaxFlickerDay.Visible = false;
                thisInst.dateMaxFlicker.Visible = false;
                thisInst.lblMaxFlickerHours.Visible = false;
                thisInst.txtMaxFlickerHours.Visible = false;

            }
            else if (selectedModel == "Shadow Flicker")
            {
                thisInst.lstShadow12x24.Visible = true;
                thisInst.plotIceVsDist.Visible = false;

                thisInst.lblShadowByMonthOrIceByDist.Text = "Hours of Shadow Flicker by Month / Hour :";

                thisInst.lblZoneOrDirection.Text = "Zone";
                thisInst.lblShadowOrIceByDist.Text = "Shadow Flicker";

                thisInst.lblIceDistOrHisto.Visible = false;
                thisInst.cboIceDistORIceHisto.Visible = false;

                // Check to see if dropdown already has zones
                bool hasFirstAndLastZone = false;
                string firstInDrop = "";
                string lastInDrop = "";
                int dropCount = thisInst.cboZoneList.Items.Count;
                int numZones = thisInst.siteSuitability.GetNumZones();

                if (dropCount > 0 && numZones > 0)
                {
                    firstInDrop = thisInst.cboZoneList.Items[0].ToString();
                    lastInDrop = thisInst.cboZoneList.Items[dropCount - 1].ToString();

                    if (firstInDrop == thisInst.siteSuitability.zones[0].name && lastInDrop == thisInst.siteSuitability.zones[numZones - 1].name)
                        hasFirstAndLastZone = true;
                }

                if (hasFirstAndLastZone == false && numZones > 0)
                {
                    thisInst.cboZoneList.Items.Clear();

                    for (int i = 0; i < numZones; i++)
                        thisInst.cboZoneList.Items.Add(thisInst.siteSuitability.zones[i].name);

                    thisInst.cboZoneList.SelectedIndex = 0;
                }

                thisInst.lblTotalHoursPerYear.Visible = true;
                thisInst.txtTotalShadow.Visible = true;

                thisInst.cboSiteSuitHour.Enabled = true;
                thisInst.cboSiteSuitMonth.Enabled = true;
                thisInst.cboIcingYear.Enabled = false;

                thisInst.lblMaxFlickerDay.Visible = true;
                thisInst.dateMaxFlicker.Visible = true;
                thisInst.lblMaxFlickerHours.Visible = true;
                thisInst.txtMaxFlickerHours.Visible = true;
            }
            else if (selectedModel == "Sound")
            {
                thisInst.cboSiteSuitHour.Enabled = false;
                thisInst.cboSiteSuitMonth.Enabled = false;
                thisInst.cboIcingYear.Enabled = false;

                thisInst.lblMaxFlickerDay.Visible = false;
                thisInst.dateMaxFlicker.Visible = false;
                thisInst.lblMaxFlickerHours.Visible = false;
                thisInst.txtMaxFlickerHours.Visible = false;

                thisInst.lblIceDistOrHisto.Visible = false;
                thisInst.cboIceDistORIceHisto.Visible = false;
            }
            else
            {
                thisInst.lblIceDistOrHisto.Visible = false;
                thisInst.cboIceDistORIceHisto.Visible = false;
            }

            thisInst.okToUpdate = true;
        }

        /// <summary> Updates probability vs. distance plot on Site Suitability tab. </summary>        
        public void IceHitsVsDistancePlot(Continuum thisInst)
        {
            thisInst.plotIceVsDist.Model = new PlotModel();
            var model = thisInst.plotIceVsDist.Model;
            model.IsLegendVisible = false;

            if (thisInst.turbineList.TurbineCount == 0 || thisInst.siteSuitability.yearlyIceHits.Length == 0)
            {
                thisInst.plotIceVsDist.Refresh();
                return;
            }

            model.Title = "Number of Ice Impacts over Turbine Lifetime";

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Distance from Turbine, m";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Number of Ice Impacts";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            LineSeries iceSeries = new LineSeries();
            iceSeries.Title = "Probability of Ice Hit";
            model.Series.Add(iceSeries);

            int yearToShow = Convert.ToInt16(thisInst.cboIcingYear.SelectedItem.ToString());
            int WD_Ind = Convert.ToInt16(thisInst.cboZoneList.SelectedIndex); // Index 16 (Num WD) = All WD

            double[] probIceVsDist = thisInst.siteSuitability.CalcIceHitVersusDistance(thisInst.siteSuitability.yearlyIceHits[yearToShow - 1].iceHits, WD_Ind,
                thisInst.turbineList.turbineEsts[0].name, thisInst);

            for (int i = 0; i < 21; i++)
                iceSeries.Points.Add(new DataPoint(i * 50, Math.Round(probIceVsDist[i], 3)));

            thisInst.plotIceVsDist.Refresh();

        }

        /// <summary> Updates Ice hit count versus Distance table on Site Suitability tab. </summary>
        public void IceHitVsDistTable(Continuum thisInst)
        {
            thisInst.lstShadowZoneSummary.Items.Clear();

            if (thisInst.siteSuitability.yearlyIceHits.Length != 0)
            {
                int yearToShow = Convert.ToInt16(thisInst.cboIcingYear.SelectedItem.ToString());
                int WD_Ind = Convert.ToInt16(thisInst.cboZoneList.SelectedIndex); // Index 16 (Num WD) = All WD
                string turbineName = thisInst.turbineList.turbineEsts[0].name;

                double[] iceHitsVsDist = thisInst.siteSuitability.CalcIceHitVersusDistance(thisInst.siteSuitability.yearlyIceHits[yearToShow - 1].iceHits, WD_Ind, turbineName, thisInst);

                thisInst.lstShadowZoneSummary.Columns[0].Text = "Distance [m]";
                thisInst.lstShadowZoneSummary.Columns[1].Text = "Num. Hits";

                for (int i = 0; i < iceHitsVsDist.Length; i++)
                {
                    objListItem = new ListViewItem((i * 50).ToString());
                    objListItem.SubItems.Add(iceHitsVsDist[i].ToString());

                    thisInst.lstShadowZoneSummary.Items.Add(objListItem);
                }
            }
        }

        /// <summary> Updates year dropdown menu on Site Suitability tab (used for ice throw model). </summary>        
        public void IcingYearsDropDown(Continuum thisInst)
        {
            thisInst.okToUpdate = false;

            thisInst.cboIcingYear.Items.Clear();

            if (thisInst.siteSuitability.yearlyIceHits.Length != 0)
            {
                for (int i = 0; i < thisInst.siteSuitability.numYearsToModel; i++)
                    thisInst.cboIcingYear.Items.Add((i + 1).ToString());

                thisInst.cboIcingYear.SelectedIndex = 0;

            }

            thisInst.okToUpdate = true;
        }

        /// <summary> Updates all plots and tables on Site Suitability tab related to ice throw. </summary>  
        public void IceThrowPlotsAndTables(Continuum thisInst)
        {
            SiteSuitabilityVisibility(thisInst);
            IceHitsByZone(thisInst);
            IceHitVsDistTable(thisInst);

            string plotSelected = thisInst.cboIceDistORIceHisto.SelectedItem.ToString();

            if (plotSelected == "Ice Hit vs. Distance")
                IceHitsVsDistancePlot(thisInst);
            else
                IceHitHistogram(thisInst);

            IceThrowSurfacePlot(thisInst);
        }

        /// <summary> Updates yearly ice hit histogram on Site Suitability tab. </summary> 
        public void IceHitHistogram(Continuum thisInst)
        {
            thisInst.plotIceVsDist.Model = new PlotModel();
            var model = thisInst.plotIceVsDist.Model;
            model.IsLegendVisible = false;

            SiteSuitability.Zone zone = GetSelectedZone(thisInst);

            if (zone.latitude == 0)
            {
                thisInst.plotIceVsDist.Refresh();
                return;
            }

            // Specify axes
            CategoryAxis xAxis = new CategoryAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Number of Ice Hits Per Year";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Freq. of Occurrence";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            double[] hitHisto = new double[6]; // Bins are: 0, 1, 2, 3, 4, > 4

            for (int i = 0; i < thisInst.siteSuitability.numYearsToModel; i++)
            {
                int numHits = thisInst.siteSuitability.GetTotalNumberOfIceHitsAtZone(i, zone, thisInst);

                if (numHits > 4)
                    hitHisto[5]++;
                else
                    hitHisto[numHits]++;
            }

            ColumnSeries hitBars = new ColumnSeries();
            model.Series.Add(hitBars);

            List<double> histoLabels = new List<double>();
            for (int i = 0; i < 6; i++)
                histoLabels.Add(i);

            xAxis.ItemsSource = histoLabels;

            for (int i = 0; i < 6; i++)
                hitBars.Items.Add(new ColumnItem { Value = hitHisto[i] });

            model.Title = "Yearly Ice Hit Histogram";

            thisInst.plotIceVsDist.Refresh();

        }

        /// <summary> Updates the turbulence intensity plot and table on Site Conditions tab. </summary> 
        public void TurbulenceIntensityPlotAndTable(Continuum thisInst)
        {
            thisInst.plotTurbInt.Model = new PlotModel();
            var model = thisInst.plotTurbInt.Model;

            if (thisInst.metList.isTimeSeries == false)
            {
                thisInst.plotTurbInt.Refresh();
                return;
            }

            Met thisMet = thisInst.GetSelectedMet("Site Conditions TI");
            if (thisMet.name == null) return;
            DateTime startTime = thisInst.dateTIStart.Value;
            DateTime endTime = thisInst.dateTIEnd.Value;
            int WD_Ind = thisInst.GetWD_ind("Site Conditions TI");

            if (thisMet.turbulence.avgWS == null)
                thisMet.CalcTurbulenceIntensity(startTime, endTime, thisInst.modeledHeight, thisInst);
            else if (thisMet.turbulence.startTime != startTime || thisMet.turbulence.endTime != endTime)
                thisMet.CalcTurbulenceIntensity(startTime, endTime, thisInst.modeledHeight, thisInst);

            thisInst.lstTurbulence.Items.Clear();

            string turbType = thisInst.cboTI_Type.SelectedItem.ToString();
            double[] effectiveTI = new double[thisInst.metList.numWD];

            if (turbType == "Effective")
            {
                Turbine thisTurb = thisInst.GetSelectedTurbine("Turbulence");
                double wohler = Convert.ToDouble(thisInst.cboEffectiveTI_m.SelectedItem.ToString());
                TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Turbulence");
                effectiveTI = thisTurb.CalcEffectiveTI(thisMet, wohler, thisInst, powerCurve, WD_Ind);
            }

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Wind Speed, m/s";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "TI";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);
            model.Title = turbType + " TI vs. WS";

            LineSeries TISeries = new LineSeries();

            TISeries.MarkerFill = OxyColors.Red;
            TISeries.MarkerStroke = OxyColors.Red;
            TISeries.Title = turbType + "TI";
            model.Series.Add(TISeries);

            Met.TIandCount[] overallTI = new Met.TIandCount[0];
            if (WD_Ind == thisInst.metList.numWD) // Calculate overall values
            {
                if (turbType == "Effective")
                    overallTI = thisMet.CalcOverallTurbulenceIntensity("Representative", thisInst); // Just to get counts
                else
                    overallTI = thisMet.CalcOverallTurbulenceIntensity(turbType, thisInst);
            }

            for (int i = 0; i < thisInst.metList.numWS; i++)
            {
                if (WD_Ind == thisInst.metList.numWD) // Calculate overall values
                {
                    ListViewItem objListItem = new ListViewItem(i.ToString());

                    if (turbType == "Effective")
                        objListItem.SubItems.Add(Math.Round(effectiveTI[i], 4).ToString("P"));
                    else
                        objListItem.SubItems.Add(Math.Round(overallTI[i].overallTI, 4).ToString("P"));

                    objListItem.SubItems.Add(overallTI[i].count.ToString());
                    thisInst.lstTurbulence.Items.Add(objListItem);

                    if (turbType == "Effective")
                    {
                        if (effectiveTI[i] != 0)
                            TISeries.Points.Add(new DataPoint(i, Math.Round(effectiveTI[i], 4)));
                    }
                    else
                    {
                        if (overallTI[i].overallTI != 0)
                            TISeries.Points.Add(new DataPoint(i, Math.Round(overallTI[i].overallTI, 4)));
                    }
                }
                else
                {
                    if (turbType == "Average")
                    {
                        if (thisMet.turbulence.count[i, WD_Ind] > 0)
                        {
                            ListViewItem objListItem = new ListViewItem(i.ToString());
                            objListItem.SubItems.Add(Math.Round((thisMet.turbulence.avgSD[i, WD_Ind] / thisMet.turbulence.avgWS[i, WD_Ind]), 4).ToString("P"));
                            objListItem.SubItems.Add(thisMet.turbulence.count[i, WD_Ind].ToString());
                            thisInst.lstTurbulence.Items.Add(objListItem);

                            if (thisMet.turbulence.avgSD[i, WD_Ind] != 0) TISeries.Points.Add(new DataPoint(i, Math.Round(thisMet.turbulence.avgSD[i, WD_Ind] / thisMet.turbulence.avgWS[i, WD_Ind], 4)));
                        }
                    }
                    else if (turbType == "Representative")
                    {
                        if (thisMet.turbulence.count[i, WD_Ind] > 2)
                        {
                            ListViewItem objListItem = new ListViewItem(i.ToString());
                            objListItem.SubItems.Add(Math.Round((thisMet.turbulence.p90SD[i, WD_Ind] / thisMet.turbulence.avgWS[i, WD_Ind]), 4).ToString("P"));
                            objListItem.SubItems.Add(thisMet.turbulence.count[i, WD_Ind].ToString());
                            thisInst.lstTurbulence.Items.Add(objListItem);

                            if (thisMet.turbulence.p90SD[i, WD_Ind] != 0) TISeries.Points.Add(new DataPoint(i, Math.Round(thisMet.turbulence.p90SD[i, WD_Ind] / thisMet.turbulence.avgWS[i, WD_Ind], 4)));
                        }

                    }
                    else if (turbType == "Effective")
                    {
                        if (thisMet.turbulence.count[i, WD_Ind] > 2)
                        {
                            ListViewItem objListItem = new ListViewItem(i.ToString());
                            objListItem.SubItems.Add(Math.Round(effectiveTI[i], 4).ToString("P"));
                            objListItem.SubItems.Add(thisMet.turbulence.count[i, WD_Ind].ToString());
                            thisInst.lstTurbulence.Items.Add(objListItem);

                            if (effectiveTI[i] != 0) TISeries.Points.Add(new DataPoint(i, Math.Round(effectiveTI[i], 4)));
                        }
                    }
                }

            }

            LineSeries iecA = new LineSeries();
            LineSeries iecB = new LineSeries();
            LineSeries iecC = new LineSeries();

            iecA.MarkerStroke = OxyColors.Blue;
            iecA.Title = "IEC A";

            iecB.MarkerStroke = OxyColors.Green;
            iecB.Title = "IEC B";

            iecC.MarkerStroke = OxyColors.Orange;
            iecC.Title = "IEC C";

            model.Series.Add(iecA);
            model.Series.Add(iecB);
            model.Series.Add(iecC);

            for (int i = 3; i < thisInst.metList.numWS; i++)
            {
                iecA.Points.Add(new DataPoint(i, 0.16 * (0.75 * i + 5.6) / i));
                iecB.Points.Add(new DataPoint(i, 0.14 * (0.75 * i + 5.6) / i));
                iecC.Points.Add(new DataPoint(i, 0.12 * (0.75 * i + 5.6) / i));
            }

            thisInst.plotTurbInt.Refresh();

        }

        /// <summary> Updates start/end dates on Site Conditions tab. </summary>
        public void SiteConditionsMetDates(Continuum thisInst)
        {
            // It's called in All_Tabs with okToUpdate sent to false so need to check if it's already false before setting/resetting it.

            bool alreadyOff = false;

            if (thisInst.okToUpdate == false)
                alreadyOff = true;

            if (thisInst.metList.isTimeSeries == false)
                return;

            if (alreadyOff == false)
                thisInst.okToUpdate = false;

            Met thisMet = thisInst.GetSelectedMet("Site Conditions TI");

            thisInst.dateTIStart.Value = thisMet.metData.startDate;
            thisInst.dateTIEnd.Value = thisMet.metData.endDate;
            thisMet = thisInst.GetSelectedMet("Site Conditions Shear");
            thisInst.dateTimeExtremeShearStart.Value = thisMet.metData.startDate;
            thisInst.dateTimeExtremeShearEnd.Value = thisMet.metData.endDate;

            if (alreadyOff == false)
                thisInst.okToUpdate = true;
        }

        /// <summary> Updates extreme shear statistics on Site Conditions tab. </summary>
        public void SiteConditionsAlpha(Continuum thisInst)
        {
            AlphaHistogram(thisInst);
            ExtremeShearTable(thisInst);
        }

        /// <summary> Updates extreme shear histogram on Site Conditions tab. </summary>
        public void AlphaHistogram(Continuum thisInst)
        {
            Met thisMet = thisInst.GetSelectedMet("Site Conditions Shear");

            if (thisInst.cboExtremeShearRange.SelectedIndex == -1)
                thisInst.cboExtremeShearRange.SelectedIndex = 0;

            thisInst.plotExtremeShear.Model = new PlotModel();
            var model = thisInst.plotExtremeShear.Model;
            model.IsLegendVisible = false;

            ColumnSeries alphaHisto = new ColumnSeries();
            alphaHisto.Title = "Shear Alpha Histogram";
            model.Series.Add(alphaHisto);

            if (thisMet.name == null)
            {
                thisInst.plotExtremeShear.Refresh();
                return;
            }

            if (thisMet.metData == null)
            {
                thisInst.plotExtremeShear.Refresh();
                return;
            }

            // Specify axes
            CategoryAxis xAxis = new CategoryAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Shear Alpha Exponent";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Freq. of Occurrence";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            DateTime startTime = thisInst.dateTimeExtremeShearStart.Value;
            DateTime endTime = thisInst.dateTimeExtremeShearEnd.Value;

            string rangeAlpha = thisInst.cboExtremeShearRange.SelectedItem.ToString();
            double[] thisHisto = thisMet.GetAlphaHistogram(rangeAlpha, thisInst, startTime, endTime);

            List<double> histoLabels = new List<double>();

            for (int i = 0; i < thisHisto.Length; i++)
                histoLabels.Add(-0.5 + i * 0.02);

            xAxis.ItemsSource = histoLabels;
            xAxis.MajorStep = 10;

            for (int i = 0; i < thisHisto.Length; i++)
                alphaHisto.Items.Add(new ColumnItem { Value = thisHisto[i] });

            model.Title = "Shear Alpha Histogram: " + thisInst.cboExtremeShearRange.SelectedItem.ToString();

            thisInst.plotExtremeShear.Refresh();

        }

        /// <summary> Updates extreme shear P table (P1, P10, and P50) for wind speed ranges: 5 - 10, 10 - 15, 15+, All WS on Site Conditions tab. </summary>
        public void ExtremeShearTable(Continuum thisInst)
        {
            Met thisMet = thisInst.GetSelectedMet("Site Conditions Shear");
            thisInst.lstExtremeShear.Items.Clear();

            if (thisMet.metData == null)
                return;

            double[] anemHeights = thisMet.metData.GetHeightsOfAnems();
            if (anemHeights.Length == 1)
                return;

            // Get start and end dates
            DateTime startTime = thisInst.dateTimeExtremeShearStart.Value;
            DateTime endTime = thisInst.dateTimeExtremeShearEnd.Value;

            double alphaP1_5_to_10 = thisMet.GetAlphaPValue(5, 10, 1, thisInst, startTime, endTime);
            double alphaP10_5_to_10 = thisMet.GetAlphaPValue(5, 10, 10, thisInst, startTime, endTime);
            double alphaP50_5_to_10 = thisMet.GetAlphaPValue(5, 10, 50, thisInst, startTime, endTime);

            double alphaP1_10_to_15 = thisMet.GetAlphaPValue(10, 15, 1, thisInst, startTime, endTime);
            double alphaP10_10_to_15 = thisMet.GetAlphaPValue(10, 15, 10, thisInst, startTime, endTime);
            double alphaP50_10_to_15 = thisMet.GetAlphaPValue(10, 15, 50, thisInst, startTime, endTime);

            double alphaP1_15plus = thisMet.GetAlphaPValue(15, 30, 1, thisInst, startTime, endTime);
            double alphaP10_15plus = thisMet.GetAlphaPValue(15, 30, 10, thisInst, startTime, endTime);
            double alphaP50_15plus = thisMet.GetAlphaPValue(15, 30, 50, thisInst, startTime, endTime);

            double alphaP1_All = thisMet.GetAlphaPValue(3, 30, 1, thisInst, startTime, endTime);
            double alphaP10_All = thisMet.GetAlphaPValue(3, 30, 10, thisInst, startTime, endTime);
            double alphaP50_All = thisMet.GetAlphaPValue(3, 30, 50, thisInst, startTime, endTime);

            objListItem = new ListViewItem("P1");
            objListItem.SubItems.Add(Math.Round(alphaP1_5_to_10, 3).ToString());
            objListItem.SubItems.Add(Math.Round(alphaP1_10_to_15, 3).ToString());
            objListItem.SubItems.Add(Math.Round(alphaP1_15plus, 3).ToString());
            objListItem.SubItems.Add(Math.Round(alphaP1_All, 3).ToString());
            thisInst.lstExtremeShear.Items.Add(objListItem);

            objListItem = new ListViewItem("P10");
            objListItem.SubItems.Add(Math.Round(alphaP10_5_to_10, 3).ToString());
            objListItem.SubItems.Add(Math.Round(alphaP10_10_to_15, 3).ToString());
            objListItem.SubItems.Add(Math.Round(alphaP10_15plus, 3).ToString());
            objListItem.SubItems.Add(Math.Round(alphaP10_All, 3).ToString());
            thisInst.lstExtremeShear.Items.Add(objListItem);

            objListItem = new ListViewItem("P50");
            objListItem.SubItems.Add(Math.Round(alphaP50_5_to_10, 3).ToString());
            objListItem.SubItems.Add(Math.Round(alphaP50_10_to_15, 3).ToString());
            objListItem.SubItems.Add(Math.Round(alphaP50_15plus, 3).ToString());
            objListItem.SubItems.Add(Math.Round(alphaP50_All, 3).ToString());
            thisInst.lstExtremeShear.Items.Add(objListItem);
        }

        /// <summary> Updates extreme wind speed textboxes and plots on Site Conditions tab. </summary>        
        public void ExtremeWindSpeed(Continuum thisInst)
        {
            Met thisMet = thisInst.GetSelectedMet("Site Conditions Extreme WS");
            Met.Extreme_WindSpeed extremeWS = thisMet.CalcExtremeWindSpeeds(thisInst);

            thisInst.plotExtremeWS.Model = new PlotModel();
            var model = thisInst.plotExtremeWS.Model;
            model.IsLegendVisible = false;

            thisInst.lblNoExtremeWS.Text = "";

            if (extremeWS.tenMin1yr == 0)
            {
                if (thisInst.merraList.numMERRA_Data == 0)
                    thisInst.lblNoExtremeWS.Text = "MERRA2 data not loaded. MERRA2 required for extreme WS calculations.";
                else
                    thisInst.lblNoExtremeWS.Text = "Met data does not cover a full year (Jan. - Dec.) required for extreme WS calculations.";

                thisInst.txt50yrExtreme10min.Text = "";
                thisInst.txt50yrExtremeGust.Text = "";
                thisInst.txt1yrExtreme10min.Text = "";
                thisInst.txt1yrExtremeGust.Text = "";

                return;
            }

            if (extremeWS.gust1yr != 0)
            {
                thisInst.txt50yrExtreme10min.Text = Math.Round(extremeWS.tenMin50yr, 2).ToString();
                thisInst.txt50yrExtremeGust.Text = Math.Round(extremeWS.gust50yr, 2).ToString();
                thisInst.txt1yrExtreme10min.Text = Math.Round(extremeWS.tenMin1yr, 2).ToString();
                thisInst.txt1yrExtremeGust.Text = Math.Round(extremeWS.gust1yr, 2).ToString();
            }

            model.Title = "Extreme Wind Speeds by Years of Recurrence";

            // Specify axes
            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Years of Recurrence";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Max. Wind Speed, m/s";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            LineSeries maxTenMinSeries = new LineSeries();
            maxTenMinSeries.Title = "Max Ten-Min WS";
            model.Series.Add(maxTenMinSeries);
            maxTenMinSeries.MarkerStroke = OxyColors.Red;

            LineSeries maxGustSeries = new LineSeries();
            maxGustSeries.Title = "Max Gust WS";
            model.Series.Add(maxGustSeries);
            maxGustSeries.MarkerStroke = OxyColors.Blue;

            for (int i = 0; i < extremeWS.yearsOfOcc.Length; i++)
            {
                maxTenMinSeries.Points.Add(new DataPoint(1.5 + i * 0.5, extremeWS.maxTenMin[i]));
                maxGustSeries.Points.Add(new DataPoint(1.5 + i * 0.5, extremeWS.maxGust[i]));
            }

            thisInst.plotExtremeWS.Refresh();
        }

        /// <summary> Updates the inflow angle plot and textboxes on Site Conditions tab. </summary>
        public void InflowAnglePlotAndTable(Continuum thisInst)
        {
            thisInst.plotInflowAngle.Model = new PlotModel();
            var model = thisInst.plotInflowAngle.Model;
            model.IsLegendVisible = false;

            Turbine thisTurb = thisInst.GetSelectedTurbine("Inflow Angle");

            if (thisInst.cboInflowWD.Items.Count == 0 || thisTurb.UTMX == 0 || thisInst.topo.gotTopo == false)
            {
                thisInst.plotInflowAngle.Refresh();
                return;
            }

            if (thisInst.topo.topoNumXY.X.calcs.min == 0)
                thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            if (thisInst.cboInflowWD.SelectedItem == null)
                thisInst.cboInflowWD.SelectedIndex = 0;

            double thisWD = Convert.ToDouble(thisInst.cboInflowWD.SelectedItem.ToString());

            if (thisInst.cboInflowRadius.SelectedItem == null)
                thisInst.cboInflowRadius.SelectedIndex = 0;

            int radius = Convert.ToInt16(thisInst.cboInflowRadius.SelectedItem.ToString());

            if (thisInst.cboInflowReso.SelectedItem == null)
                thisInst.cboInflowReso.SelectedIndex = 0;

            int reso = Convert.ToInt16(thisInst.cboInflowReso.SelectedItem.ToString());

            if (thisInst.topo.elevsForCalcs == null)
                thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            // Find elevation profile along WD
            TopoInfo.TopoGrid[] elevProfile = thisInst.topo.GetElevationProfile(thisTurb.UTMX, thisTurb.UTMY, thisWD, radius, reso);

            // Calculate slope along inflow (not downwind)
            int numPtsInflow = radius / reso;
            double[] xVals = new double[numPtsInflow];
            double[] yVals = new double[numPtsInflow];

            for (int i = 0; i < numPtsInflow; i++)
            {
                xVals[i] = i * reso;
                yVals[i] = elevProfile[i].elev;
            }

            double slope = thisInst.topo.CalcSlope(xVals, yVals);
            double inflowAngle = Math.Atan(slope) * 180 / Math.PI;
            thisInst.txtInflowAngle.Text = Math.Round(inflowAngle, 2).ToString();

            // Update plot
            model.Title = "Elevation Profile along WD = " + thisWD.ToString();

            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "Distance";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Elevation, m";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            LineSeries elevSeries = new LineSeries();
            elevSeries.Title = "Elevation Profile";
            model.Series.Add(elevSeries);
            elevSeries.MarkerStroke = OxyColors.Blue;

            for (int i = 0; i < elevProfile.Length; i++)
                elevSeries.Points.Add(new DataPoint(Math.Round((double)(i * reso), 1), Math.Round(elevProfile[i].elev, 2)));

            LineSeries slopeSeries = new LineSeries();
            slopeSeries.Title = "Best-Fit Slope";
            model.Series.Add(slopeSeries);
            slopeSeries.MarkerStroke = OxyColors.Red;
            slopeSeries.LineStyle = LineStyle.Dash;

            slopeSeries.Points.Add(new DataPoint(0, elevProfile[0].elev));
            slopeSeries.Points.Add(new DataPoint(radius, elevProfile[0].elev + radius * slope));

            // Add point for turbine location
            ScatterSeries turbineSite = new ScatterSeries();
            turbineSite.Title = "Turbine Site";
            model.Series.Add(turbineSite);
            turbineSite.Points.Add(new ScatterPoint(radius, thisTurb.elev));

            thisInst.plotInflowAngle.Refresh();
        }

        /// <summary> Calculates the statistics (avg, min, max, SD) of map and updates the textboxes on Maps tab. </summary> 
        public void FindMapStats(Continuum thisInst, Map thisMap)
        {
            double avg = 0;
            double stdev = 0;
            double min = 1000;
            double max = 0;
            double param;
            int dataCount = 0;

            for (int i = 0; i < thisMap.numX; i++)
            {
                for (int j = 0; j < thisMap.numY; j++)
                {
                    param = thisMap.parameterToMap[i, j];
                    avg = avg + param;
                    stdev = stdev + Math.Pow(param, 2);
                    if (param < min) min = param;
                    if (param > max) max = param;
                    dataCount++;
                }
            }

            if (dataCount > 0)
            {
                avg = avg / dataCount;
                stdev = (Math.Pow((stdev / dataCount - Math.Pow(avg, 2)), 0.5));
            }

            if (thisMap.modelType == 3 || thisMap.modelType == 5)
            {
                thisInst.txtMapAvg.Text = Math.Round(avg, 1).ToString();
                thisInst.txtMapStDev.Text = Math.Round(stdev, 1).ToString();
                thisInst.txtMapMin.Text = Math.Round(min, 1).ToString();
                thisInst.txtMapMax.Text = Math.Round(max, 1).ToString();
                thisInst.txtMapCount.Text = dataCount.ToString();
            }
            else
            {
                thisInst.txtMapAvg.Text = Math.Round(avg, 3).ToString();
                thisInst.txtMapStDev.Text = Math.Round(stdev, 3).ToString();
                thisInst.txtMapMin.Text = Math.Round(min, 3).ToString();
                thisInst.txtMapMax.Text = Math.Round(max, 3).ToString();
                thisInst.txtMapCount.Text = dataCount.ToString();
            }

            string theseMetsUsed = thisInst.metList.CreateMetString(thisMap.metsUsed, true);
            thisInst.txtMap_MetsUsed.Text = theseMetsUsed;

        }

        /// <summary> Updates all plots and tables on Time Series tab </summary>        
        public void TimeSeries_TAB(Continuum thisInst)
        {
            MetDataQC_TAB(thisInst);
            MetTurbSummaryAndStatsTable(thisInst);
            GrossTurbineEstsTAB(thisInst);
            Exceedance_TAB(thisInst);
            NetTurbineEstsTAB(thisInst);
            Monthly_TAB(thisInst);
            MapsTAB(thisInst);
            Uncertainty_TAB_Round_Robin(thisInst);
            Uncertainty_TAB_Turbine_Ests(thisInst);
            AdvancedTAB(thisInst);
        }

        /// <summary> Updates anemometer A and B dropdown on Met Data QC tab </summary>  
        public void MetQCAnemVaneDropdown(Continuum thisInst)
        {            
            Met selectedMet = thisInst.GetSelectedMet("Met Data QC");
            if (selectedMet.name == null)
                return;

            if (selectedMet.metData == null)
                return;

            thisInst.okToUpdate = false;
            thisInst.cboAnemA.Items.Clear();
            thisInst.cboAnemB.Items.Clear();
            thisInst.cboSelVane.Items.Clear();
            
            int numAnems = selectedMet.metData.GetNumAnems();
            int numVanes = selectedMet.metData.GetNumVanes();

            for (int i = 0; i < numAnems; i++)
            {
                thisInst.cboAnemA.Items.Add(selectedMet.metData.anems[i].height + " " + selectedMet.metData.anems[i].orientation);
                thisInst.cboAnemB.Items.Add(selectedMet.metData.anems[i].height + " " + selectedMet.metData.anems[i].orientation);
            }

            if (numAnems > 1)
            {
                thisInst.cboAnemA.SelectedIndex = numAnems - 2;
                thisInst.cboAnemB.SelectedIndex = numAnems - 1;
            }
            else if (numAnems > 0)
            {
                thisInst.cboAnemA.SelectedIndex = numAnems - 1;
                thisInst.cboAnemB.SelectedIndex = numAnems - 1;
            }

            for (int i = 0; i < numVanes; i++)
                thisInst.cboSelVane.Items.Add(selectedMet.metData.vanes[i].height);

            if (numVanes > 0)
                thisInst.cboSelVane.SelectedIndex = 0;

            thisInst.okToUpdate = true;
        }

    }

}
