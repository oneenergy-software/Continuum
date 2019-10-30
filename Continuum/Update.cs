using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Nevron.Chart;
using Nevron.Chart.WinForm;
using Nevron.GraphicsCore;
using Nevron.Chart.Windows;
using Nevron.Dom;
using System.Globalization;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace ContinuumNS
{
    public class Update
    {

        ListViewItem objListItem = new ListViewItem();
        DateTime startTime;

        public struct Advanced_to_show
        {
            // Based on checkbox on Advanced tab which specifies which parameter to show in "Path of Nodes" plot
            public bool showUTMX;
            public bool showUTMY;
            public bool showElevation;
            public bool showP10UW;
            public bool showP10DW;
            public bool showUWExpo;
            public bool showDWExpo;
            public bool showUWRough;
            public bool showDWRough;
            public bool showUWDispH;
            public bool showDWDispH;
            public bool show_dWS_UWExpo;
            public bool show_dWS_DWExpo;
            public bool show_dWS_UW_SRDH;
            public bool show_dWS_DW_SRDH;
            public bool showWS_Est;
            public bool showEquivWS;
            public bool showActualWS;
        }

        public void NetTurbineEstsTAB(Continuum thisInst) // Updates all plots, tables and textboxes on Net Turb Ests tab
        {
            WakeModelList(thisInst);
            WakedTurbList(thisInst);
            WakedWSDistPlot(thisInst);
            TurbsByString(thisInst);
            WakeLossMap(thisInst);
        }

        public void ColoredButtons(Continuum thisInst) // Updates all buttons that are color-coded to be either red or green depending on state of analysis
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
                //    thisInst.btnAnalyzeMets.Enabled = false;
            }
            else
            {
                thisInst.btnAnalyzeMets.BackColor = Color.LightCoral;
                //     thisInst.btnAnalyzeMets.Enabled = true;
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
                thisInst.btn_Import_MERRA.BackColor = Color.Green;
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
            else if (thisMet.mcp.gotMCP_Est == false && gotMERRA == true)
            {
                thisInst.btnDoMCP.BackColor = Color.LightCoral;
                thisInst.btnDoMCP.Enabled = true;
            }
            else if (thisMet.mcp.gotMCP_Est == true)
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

        public void TurbsByString(Continuum thisInst) // Updates net turbine  plot that shows estimates for each specified string
        {
            int dist;
            double maxVal = 0;
            double minVal = 10000000;
            Turbine lastTurb = null;

            TopoInfo topo = new TopoInfo();

            int WD_Ind = thisInst.GetWD_ind("Net");
           
            int numWD = thisInst.GetNumWD();            
            Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();

            NChartControl turbsByString = thisInst.chtTurbByString_Nev;
            turbsByString.Charts[0].Series.Clear();
            turbsByString.Charts[0].Width = 200;
            turbsByString.Charts[0].Height = 125;
            turbsByString.Labels.Clear();
            turbsByString.Controller.Tools.Clear();
            turbsByString.Legends[0].Visible = false;
            NTooltipTool tooltip = new NTooltipTool();
            turbsByString.Controller.Tools.Add(tooltip);
                        
            if (thisInst.metList.ThisCount == 0 || thisWakeModel == null || numWD == 0 || WD_Ind == -1)
            {
                turbsByString.Refresh();
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
                    turbsByString.Refresh();
                    return;
                }
            }
            catch
            {
                turbsByString.Refresh();
                return;
            }

            if (plotStringCount > 0)
            {

                double[] stringXDist = new double[plotStringCount];
                double[] turbParam = new double[plotStringCount];

                int plotInd = 0;

                if (thisInst.turbineList.GotEst("Net", thisWakeModel.powerCurve, thisWakeModel) == true)
                {
                    for (int i = 0; i <= allStringCount - 1; i++)
                    {
                        int Num_turbs_in_str = 0;
                        for (int j = 0; j <= thisInst.turbineList.TurbineCount - 1; j++)
                            if (thisInst.turbineList.turbineEsts[j].stringNum == i + 1)
                                Num_turbs_in_str++;

                        stringXDist = new double[Num_turbs_in_str];
                        turbParam = new double[Num_turbs_in_str];

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
                                Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(thisWakeModel, thisWakeModel.powerCurve);
                                Turbine.Net_Energy_Est netEst = thisTurb.GetNetEnergyEst(thisWakeModel);

                                if (thisTurb.stringNum == i + 1)
                                {
                                    if (turbInd == 0)
                                    {
                                        dist = 0;
                                        lastTurb = thisTurb;
                                        stringXDist[turbInd] = 0;
                                    }
                                    else
                                    {
                                        dist = (int)topo.CalcDistanceBetweenPoints(thisTurb.UTMX, thisTurb.UTMY, lastTurb.UTMX, lastTurb.UTMY);
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

                            NLineSeries thisSeries = new NLineSeries();                            
                            thisSeries.DataLabelStyle.Visible = false;
                            thisSeries.BorderStyle.Color = GetMetOrTurbColor(plotInd);
                            thisSeries.Name = "string " + i + 1;
                            thisSeries.UseXValues = true;

                            for (int j = 0; j < Num_turbs_in_str; j++)
                            {
                                thisSeries.XValues.Add(stringXDist[j]);
                                thisSeries.Values.Add(turbParam[j]);
                            }

                            thisSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(thisSeries.Name);
                            turbsByString.Charts[0].Series.Add(thisSeries);

                            plotInd++;
                        }
                    }
                }
            }

            string header = "";
            turbsByString.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "Distance, m";
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

            turbsByString.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = yLabel1;
            turbsByString.Labels.AddHeader(header);

            turbsByString.Refresh();

        }

        public void WakeLossMap(Continuum thisInst) // Updates waked WS map on Net Turbine Ests tab
        {
            //  thisInst.cht3DWakeLoss.ChartLabels.LabelsCollection.Clear()
            int numWakeModels = thisInst.wakeModelList.NumWakeModels;

            NChart wakedMap = thisInst.cht3DWakeLoss_Nev.Charts[0];
            wakedMap.Projection.SetPredefinedProjection(PredefinedProjection.OrthogonalTop);
            wakedMap.Series.Clear();
            wakedMap.Enable3D = true;
            wakedMap.Width = 70f;
            wakedMap.Depth = 70f;
            wakedMap.Height = 0.001f;
            thisInst.cht3DWakeLoss_Nev.Legends.Clear();
            NMeshSurfaceSeries wakeSurface = new NMeshSurfaceSeries();
            wakeSurface.FillMode = SurfaceFillMode.Zone;
            wakeSurface.FrameMode = SurfaceFrameMode.Contour;
            wakeSurface.DrawFlat = true;
            wakeSurface.ValueFormatter.FormatSpecifier = "0.00";

            if (numWakeModels > 0 && thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false && thisInst.BW_worker.BackgroundWorker_Map.IsBusy == false)
            {
                // Reset map
                //  thisInst.cht3DWakeLoss.ChartGroups.Group0.ChartData.SetGrid.ColumnCount = 1
                //  thisInst.cht3DWakeLoss.ChartGroups.Group0.ChartData.SetGrid.RowCount = 1
                //  Dim Z(0, 0)  double
                //  z(0, 0) = 1
                //  Dim reset_grid  Chart3DDataSetGrid = new Chart3DDataSetGrid(0, 0, 1, 1, Z)
                //  thisInst.cht3DWakeLoss.ChartGroups.Group0.ChartData.SetGrid = reset_grid
                //  thisInst.cht3DWakeLoss.Legend.Visible = false

                WakeCollection.WakeGridMap thisGrid = new WakeCollection.WakeGridMap();
                Wake_Model thisWakeModel;

                int plotInd = 0;

                try
                {
                    plotInd = thisInst.cboWakePlot.SelectedIndex;
                    if (plotInd == -1)
                        thisInst.cboWakePlot.SelectedIndex = 0;
                }
                catch
                {
                    return;
                }

                if (thisInst.wakeModelList.NumWakeGridMaps == 0)
                {
                    thisInst.cht3DWakeLoss_Nev.Refresh();
                    return;
                }
                    

                thisWakeModel = thisInst.GetSelectedWakeModel();
                thisGrid = thisInst.GetSelectedWakeGrid();

                //  thisInst.cht3DWakeLoss.ChartGroups.Group0.ChartData.SetGrid.ColumnCount = thisGrid.numX
                //  thisInst.cht3DWakeLoss.ChartGroups.Group0.ChartData.SetGrid.RowCount = thisGrid.numY

                if (thisGrid.isComplete == false)
                {
                    thisInst.cht3DWakeLoss_Nev.Refresh();
                    return;
                }

                wakeSurface.Data.SetGridSize(thisGrid.numX, thisGrid.numY);

                int WD_Ind = thisInst.GetWD_ind("Net");
                int numWD = thisInst.GetNumWD();
                double thisMin = 0;
                double thisMax = 0;
                int newNumLevels = 10;
                double intWidth = 0;

                try
                {
                    thisMin = Convert.ToSingle(thisInst.txtWakeMin.Text);
                }
                catch
                {
                    thisMin = thisInst.wakeModelList.FindMin(thisGrid, WD_Ind, plotInd) * 0.99f;
                    thisInst.txtWakeMin.Text = Math.Round(thisMin, 3).ToString();
                }

                try
                {
                    thisMax = Convert.ToSingle(thisInst.txtWakeMax.Text);
                }
                catch
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

                        intWidth = (double)Math.Round(intWidth, 0);
                    }

                    thisInst.txtWakeMin.Text = thisMin.ToString();
                    thisInst.txtWakeMax.Text = thisMax.ToString();
                    thisInst.txtWakeInterval.Text = intWidth.ToString();
                }

                newNumLevels = Convert.ToInt16((thisMax - thisMin) / intWidth + 1);

                if (newNumLevels <= 0)
                    newNumLevels = 1;

                double[] newLevelArray = new double[newNumLevels];

                //  for (int i = 0; i <= newNumLevels - 1; i++) {
                //     newLevel = thisMin + intWidth * i;
                //     newLevelArray[i] = newLevel;
                // }

                wakeSurface.Palette.Clear();
                wakeSurface.Palette.SmoothPalette = false;
                wakeSurface.Palette.HasCustomMin = true;
                wakeSurface.Palette.CustomMin = thisMin - thisMin * 0.05f;
                wakeSurface.Palette.Mode = PaletteMode.Custom;
                wakeSurface.Palette.PaletteSteps = newNumLevels;

                for (int i = 0; i < newNumLevels; i++)
                    wakeSurface.Palette.Add(Math.Round(thisMin + i * intWidth, 3), GetRGB_Values((double)i / newNumLevels));

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
                                param[i, j] = thisGrid.wakeGrids[i, j].wakedWS;
                            else
                                param[i, j] = thisGrid.wakeGrids[i, j].sectorWakedWS[WD_Ind];
                        }
                        else if (plotInd == 1)
                        {
                            if (WD_Ind == numWD)
                                param[i, j] = thisGrid.wakeGrids[i, j].wakeLoss * 100;
                            else
                                param[i, j] = thisGrid.wakeGrids[i, j].sectorWakeLoss[WD_Ind] * 100;
                        }
                        else
                        {
                            if (WD_Ind == numWD)
                                param[i, j] = thisGrid.wakeGrids[i, j].netEnergy;
                            else
                                param[i, j] = thisGrid.wakeGrids[i, j].sectorNetEnergy[WD_Ind];

                        }

                        wakeSurface.Data.SetValue(i, j, param[i, j], thisGrid.minUTMX + i * thisGrid.wakeGridReso, thisGrid.minUTMY + j * thisGrid.wakeGridReso);
                    }
                }

                wakedMap.Series.Add(wakeSurface);

                wakeSurface.Legend.Mode = SeriesLegendMode.SeriesLogic;
                wakeSurface.Legend.Format = "<zone_begin> - <zone_end>";
                

                if ((plotInd <= 1))
                    wakeSurface.ValueFormatter.FormatSpecifier = "0.000";
                else
                    wakeSurface.ValueFormatter.FormatSpecifier = "0";

                NLegend wakeLegend = new NLegend();
                wakeLegend.UseAutomaticSize = true;
                thisInst.cht3DWakeLoss_Nev.Charts[0].DisplayOnLegend = wakeLegend;
                thisInst.cht3DWakeLoss_Nev.Panels.Add(wakeLegend);
                wakeLegend.DockMode = PanelDockMode.Right;

                WakeMapLabels(thisInst, thisGrid, plotInd, WD_Ind);
            }
            else
            {
                thisInst.cht3DWakeLoss_Nev.Charts[0].Series.Clear();
            }

            thisInst.cht3DWakeLoss_Nev.Refresh();
            wakedMap.Refresh();

        }

        public void WakeModelList(Continuum thisInst) // Updates list on //Net Turbine Ests// tab that lists the wake models and wake maps that have been created
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

            ColoredButtons(thisInst);
            thisInst.okToUpdate = true;
        }

        public void WakedTurbList(Continuum thisInst)
        {
            // Updates the table on Net Turbine Ests tab which lists the net turbine estimates
            thisInst.lstWakedTurbs.Items.Clear();
            int turbCount = thisInst.turbineList.TurbineCount;                     

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Net");
            int checkedTurbCount = 0;

            if (checkedTurbines != null)
                checkedTurbCount = checkedTurbines.Length;

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
                Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(thisWakeModel, thisWakeModel.powerCurve);

                if (avgEst.waked.WS != 0)
                {
                    ListViewItem objListItem = new ListViewItem();
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
                        objListItem.SubItems.Add(Math.Round(thisTurb.GetNetCF(thisWakeModel, WD_Ind), 4).ToString("P"));
                        
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

        public void MetList(Continuum thisInst) // Updates all plots and tables on all tabs that have the mets to reflect new metList
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

            try
            {
                thisInst.cboRR_MinSize.SelectedIndex = 0;
            }
            catch { }

            StartMet_Dropdown(thisInst);
            EndMet_Dropdown(thisInst);

            thisInst.okToUpdate = true;
        }

        public void ModCrossPredictions(Continuum thisInst)
        {
            // Updates table on Advanced tab that shows the met cross prediction of site-calibrated model with specified radius and WD sector
            Pair_Of_Mets thisPair;
            int numPairs = thisInst.metPairList.PairCount;

            int radiusInd = thisInst.GetRadiusInd("Advanced");
            int WD_Ind = thisInst.GetWD_ind("Advanced");

            if (thisInst.metList.ThisCount == 0 || thisInst.metList.expoIsCalc == false)
            {
                thisInst.lstModCrossPred.Items.Clear();
                return;
            }

            int numWD = thisInst.GetNumWD();
            double thisErr = 0;

            if (numPairs == 0 || WD_Ind == -1 || radiusInd == -1)
            {
                thisInst.lstModCrossPred.Items.Clear();
                return;
            }
            else
                thisInst.lstModCrossPred.Items.Clear();

            int numModels = thisInst.modelList.ModelCount;
            int numRadii = thisInst.radiiList.ThisCount;
            
            
            Met.TOD thisTOD = thisInst.GetSelectedTOD("Advanced");
            Met.Season thisSeason = thisInst.GetSelectedSeason("Advanced");
            string[] metsUsed = thisInst.metList.GetMetsUsed();

            if (thisInst.modelList.ModelCount > 1)
                if (thisInst.modelList.models[1, 0].isImported == true)
                    return;

            if (thisInst.modelList.ModelCount > 0)
            {
                if (numPairs > 0 && numModels > 1)
                {
                    Model[] models = thisInst.modelList.GetModels(thisInst, metsUsed, thisTOD, thisSeason, thisInst.modeledHeight, false);

                    for (int i = 0; i < numPairs; i++)
                    {
                        thisPair = thisInst.metPairList.metPairs[i];
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
                            thisErr = (thisWS_CrossPred.sectorWS_Ests[0, WD_Ind] - dist2.WS * dist2.sectorWS_Ratio[WD_Ind]) / (dist2.WS * dist2.sectorWS_Ratio[WD_Ind]);
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
                            thisErr = (thisWS_CrossPred.sectorWS_Ests[1, WD_Ind] - dist1.WS * dist1.sectorWS_Ratio[WD_Ind]) / (dist1.WS * dist1.sectorWS_Ratio[WD_Ind]);
                            objListItem.SubItems.Add((Math.Round(thisErr, 4)).ToString("P"));
                        }
                    }

                }
            }
        }                

        public void SetDefaultCheckAdvanced(Continuum thisInst) // Specifies the default parameters shown on Path Node plot on Advanced tab
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

        public void PathNodesList(Continuum thisInst)
        {
            // Sets up table on Advanced tab to show selected parameters and reads selected start met and end site, radius and WD sector
            //  then calls void "PathNodes_List_Update" to update the three tables on Advanced tab
            if (thisInst.modelList.ModelCount == 0)
            {
                thisInst.lstPathNodes.Items.Clear();
                thisInst.lstPathNodes_UW.Items.Clear();
                thisInst.lstPathNodes_DW.Items.Clear();
                return;
            }

            Pair_Of_Mets thisPair;
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
            int radius = thisInst.radiiList.investItem[radiusIndex].radius;
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

            int metPairInd = thisInst.cboEndMet.SelectedIndex;

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
            Met thisMet = new Met();

            for (int i = 0; i < numMets; i++)
            {
                thisMet = thisInst.metList.metItem[i];
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
                colNum++;
            }

            NodeCollection nodeList = new NodeCollection();
            thisInst.lblTurbineTSNoAdvanced.Text = "";

            for (int i = 0; i < numMets; i++)
            {
                thisMet = thisInst.metList.metItem[i];
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

            int numNodes = 0;

            if (endIsMet)
            {
                for (int i = 0; i < numPairs; i++)
                {
                    thisPair = thisInst.metPairList.metPairs[i];
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
                            numNodes = pathOfNodes.Length;
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

        public void PathNodeListUpdate(Nodes startNode, string startMetStr, double[] startWS, Nodes endNode, string endStr, Nodes[] pathOfNodes,
                                         double[,] pathNodeSectWS, double[] pathNodeWS, int WD_Ind, int radiusIndex, int numWD, Model thisModel,
                                         Continuum thisInst, Advanced_to_show paramsToShow, double HH, double[] estSectorWS, double estWS, double actWS)
        {
            // Updates the three tables on Advanced tab to show selected parameters from selected met to selected met or turbine site for specified radius and wind direction sector

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

                objListItem = thisInst.lstPathNodes.Items.Add("Node " + (i + 1));
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
                                objList_UW = thisInst.lstPathNodes_UW.Items.Add("Node " + i + 1);

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
                                objList_DW = thisInst.lstPathNodes_DW.Items.Add("Node " + i + 1);

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

        public void AddUTMsP10sExposToPathNodeList(Advanced_to_show paramsToShow, double UTMX, double UTMY, double elev, double P10_UW_1, double P10_DW_1, double UWExpo_1, double DWExpo_1)
        {
            // Adds UTMs, elevation, P10 UW/DW, or UW/DW exposure to Path Node List on //Advanced// tab
            if (paramsToShow.showUTMX) objListItem.SubItems.Add(UTMX.ToString());
            if (paramsToShow.showUTMY) objListItem.SubItems.Add(UTMY.ToString());
            if (paramsToShow.showElevation) objListItem.SubItems.Add(Math.Round(elev, 1).ToString());
            if (paramsToShow.showP10UW) objListItem.SubItems.Add(Math.Round(P10_UW_1, 2).ToString());
            if (paramsToShow.showP10DW) objListItem.SubItems.Add(Math.Round(P10_DW_1, 2).ToString());
            if (paramsToShow.showUWExpo) objListItem.SubItems.Add(Math.Round(UWExpo_1, 2).ToString());
            if (paramsToShow.showDWExpo) objListItem.SubItems.Add(Math.Round(DWExpo_1, 2).ToString());
        }

        public void RoundRobinDropdown(Continuum thisInst) // Updates dropdown menu on Uncertainty tab (Round Robin ) specifying number of mets used in Round Robin
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
                int thisSize = 0;
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
                        thisSize = 100;
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

            try
            {
                thisInst.cboRoundRobin.SelectedIndex = 0;
            }
            catch
            {
            }

            thisInst.okToUpdate = true;

        }

        public void RoundRobinIndivResults(Continuum thisInst)
        {
            // Updates table on Uncertainty tab showing the results of Round Robin estimates
            int RR_ind;
            string dropText;

            int numMetsinRR;
            thisInst.lstRR_AllErr.Items.Clear();

            if (thisInst.metPairList.RoundRobinCount > 0)
            {
                try
                {
                    RR_ind = thisInst.cboRoundRobin.SelectedIndex;
                    dropText = thisInst.cboRoundRobin.SelectedItem.ToString();
                }
                catch
                {
                    return;
                }

                string[] textSplit = dropText.Split(Convert.ToChar(" "));
                try
                {
                    numMetsinRR = Convert.ToInt16(textSplit[1]);
                }
                catch
                {
                    return;
                }

                int numMets = thisInst.metList.ThisCount;
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
                    string metStr;

                    thisInst.lstRR_AllErr.Items.Clear();
                    MetPairCollection.RR_WS_Ests thisRR = thisInst.metPairList.roundRobinEsts[RR_ind];
                    int numErrs = thisRR.RMS_Err.Length;
                    int numToPredict = thisRR.avgWS_Ests.GetUpperBound(0) + 1;

                    // List with overall RMS for specified UW&DW model
                    string[] metsInModel = new string[thisRR.metsInModel.GetUpperBound(0) + 1];

                    for (int i = 0; i < numErrs; i++)
                    {
                        for (int n = 0; n <= thisRR.metsInModel.GetUpperBound(0); n++)
                        {
                            metsInModel[n] = thisRR.metsInModel[n, i];
                            metStr = thisInst.metList.CreateMetString(metsInModel, true);
                        }

                        metStr = metsInModel[0];
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

        public void TurbineUncertPlot(Continuum thisInst) // Updates plot on Uncertainty tab showing turbine uncertainty estimates
        {
            NChartControl ChtCtrl = thisInst.chtTurbUncert_Nev;
            ChtCtrl.Charts[0].Series.Clear();
            ChtCtrl.Labels.Clear();

            if (thisInst.turbineList.GotEst("WS", new TurbineCollection.PowerCurve(), null) == false)
            {
                ChtCtrl.Refresh();
                return;
            }

            // Find whether plotting AEP or WS
            bool plotWS = false;

            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Uncertainty");
            int numWD = thisInst.GetNumWD();
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
                    return;
            }

            if (numTurbines > 0)
            {
                if ((thisInst.turbineList.turbineEsts[0].avgWS_Est != null && plotWS == true) ||
                    (thisInst.turbineList.turbineEsts[0].grossAEP != null && plotWS == false))
                {

                    NPointSeries P50_Series = new NPointSeries();
                    P50_Series.FillStyle = new NColorFillStyle(Color.Blue);
                    P50_Series.UseXValues = true;
                    P50_Series.Name = "P50";
                    P50_Series.Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                    P50_Series.DataLabelStyle.Visible = false;

                    NPointSeries P90_Series = new NPointSeries();
                    P90_Series.FillStyle = new NColorFillStyle(Color.Red);
                    P90_Series.UseXValues = true;
                    P90_Series.Name = "P90";
                    P90_Series.Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                    P90_Series.DataLabelStyle.Visible = false;

                    NPointSeries P99_Series = new NPointSeries();
                    P99_Series.FillStyle = new NColorFillStyle(Color.Green);
                    P99_Series.UseXValues = true;
                    P99_Series.Name = "P99";
                    P99_Series.Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                    P99_Series.DataLabelStyle.Visible = false;

                    ChtCtrl.Legends.Add();
                    ChtCtrl.Legends[0].DockMode = PanelDockMode.Right;

                    for (int i = 0; i <= numTurbines - 1; i++)
                    {
                        Turbine thisTurb = thisInst.turbineList.turbineEsts[i];

                        if (thisTurb.AvgWSEst_Count > 0)
                        {
                            Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(null, powerCurve);
                            Turbine.Gross_Energy_Est grossEnergy = thisTurb.GetGrossEnergyEst(powerCurve);

                            if (plotWS == true)
                            {
                                P50_Series.Values.Add(avgEst.freeStream.WS);
                                P90_Series.Values.Add(avgEst.freeStream.WS - avgEst.freeStream.WS * avgEst.uncert * 1.28155);
                                P99_Series.Values.Add(avgEst.freeStream.WS - avgEst.freeStream.WS * avgEst.uncert * 2.326);
                            }
                            else
                            {
                                P50_Series.Values.Add(grossEnergy.AEP);
                                P90_Series.Values.Add(grossEnergy.P90);
                                P99_Series.Values.Add(grossEnergy.P99);
                            }

                            P50_Series.XValues.Add(i + 1);
                            P90_Series.XValues.Add(i + 1);
                            P99_Series.XValues.Add(i + 1);
                        }
                    }

                    ChtCtrl.Charts[0].Series.Add(P50_Series);
                    ChtCtrl.Charts[0].Series.Add(P90_Series);
                    ChtCtrl.Charts[0].Series.Add(P99_Series);

                    if (plotWS == true)
                    {
                        string yLabel1 = "Wind Speed, m/s";
                        ChtCtrl.Labels.AddHeader("Turbine Wind Speed Ests: P50, P90 & P99");
                        ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = yLabel1;
                    }
                    else
                    {
                        string yLabel1 = "Gross AEP, MWh";
                        ChtCtrl.Labels.AddHeader("Turbine Gross AEP Ests: P50, P90 & P99");
                        ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = yLabel1;
                    }

                    string Xlabel1 = "Turbine Site #";
                    ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = Xlabel1;

                    if (thisInst.chkP99.Checked == false)
                        P99_Series.Visible = false;

                    if (thisInst.chkP50.Checked == false)
                        P50_Series.Visible = false;

                    if (thisInst.chkP90.Checked == false)
                        P90_Series.Visible = false;

                    ChtCtrl.Legends[0].Visible = true;
                    ChtCtrl.Refresh();
                }
            }
        }

        public void RoundRobinResults(Continuum thisInst) // Updates table on Uncertainty tab showing RMS error of Round Robin analyses
        {
            int numRR = thisInst.metPairList.RoundRobinCount;
            MetPairCollection.RR_WS_Ests thisRR = new MetPairCollection.RR_WS_Ests();

            thisInst.lstRR_Results.Items.Clear();

            NChartControl RR_RMSAll_Ctrl = thisInst.chtRR_RMSAll_Nev;
            RR_RMSAll_Ctrl.Charts[0].Series.Clear();
            RR_RMSAll_Ctrl.Labels.Clear();
            RR_RMSAll_Ctrl.Legends[0].Visible = false;

            NPointSeries RR_RMS = new NPointSeries();
            RR_RMS.DataLabelStyle.Visible = false;
            RR_RMS.UseXValues = true;
            RR_RMS.PercentValueFormatter = new NNumericValueFormatter(Nevron.NumericValueFormat.Percentage);
            RR_RMSAll_Ctrl.Charts[0].Series.Add(RR_RMS);

            if (thisInst.modelList.ModelCount <= 1)
            {
                RR_RMSAll_Ctrl.Refresh();
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
            int thisSize = 0;

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
                    thisSize = 100;
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
            double[] numMets = new double[numRR];
            double[] overallRMS = new double[numRR];
            int RR_ind = 0;

            for (int i = 0; i < numRR; i++)
            {
                thisRR = thisInst.metPairList.roundRobinEsts[i];
                bool sameMets = thisInst.metList.sameMets(thisRR.metsUsed, metsUsed);
                if (sameMets == true)
                {
                    numMets[RR_ind] = thisInst.metPairList.roundRobinEsts[i].metSubSize;
                    overallRMS[RR_ind] = 100 * thisInst.metPairList.roundRobinEsts[i].RMS_All;
                    RR_ind++;
                }
            }

            for (int i = 0; i < numRR; i++)
            {
                RR_RMS.XValues.Add(numMets[i]);
                RR_RMS.Values.Add(overallRMS[i]);
            }

            string Xlabel1 = "Num Mets Used in Model";
            string yLabel1 = "% Error ";

            RR_RMSAll_Ctrl.Labels.AddHeader(yLabel1 + " vs " + Xlabel1);

            NLinearScaleConfigurator linearScaleX = new NLinearScaleConfigurator();
            linearScaleX.MajorTickMode = MajorTickMode.CustomStep;
            linearScaleX.CustomStep = 1;
            linearScaleX.Title.Text = Xlabel1;
            RR_RMSAll_Ctrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator = linearScaleX;

            NLinearScaleConfigurator linearScaleY = new NLinearScaleConfigurator();

            RR_RMS.PercentValueFormatter = new NNumericValueFormatter(Nevron.NumericValueFormat.Percentage);

            RR_RMSAll_Ctrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator = linearScaleY;

            RR_RMSAll_Ctrl.Refresh();

        }

        public void ModelParams(Continuum thisInst)
        {
            // Updates four textboxes on Advanced tab: Mets used, RMS error (mets used), RMS error (all mets), sect error                         
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

        public void RadiusToDisplay(string summ_or_Adv, Continuum thisInst) // Updates radius of investigation dropdown menu on Met & Turb Summary tab and Advanced tab
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

        public void WindDirectionToDisplay(Continuum thisInst) // Updates all wind direction dropdown menus
        {
            thisInst.okToUpdate = false;
      //      if (thisInst.metList.ThisCount == 0) return;
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

        public void StartMet_Dropdown(Continuum thisInst) // Updates "Start Met" dropdown menu on Advanced tab
        {
            thisInst.okToUpdate = false;
            thisInst.cboStartMet.Items.Clear();

            string[] metsUsed = thisInst.metList.GetMetsUsed();
            int numMets = metsUsed.Length;

            for (int i = 0; i < numMets; i++)
                thisInst.cboStartMet.Items.Add(metsUsed[i]);

            try
            {
                thisInst.cboStartMet.SelectedIndex = 0;
            }
            catch
            {        
            }

            thisInst.okToUpdate = true;
        }

        public void EndMet_Dropdown(Continuum thisInst)  // Updates "End Site" dropdown menu on Advanced tab
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

        public void Labels(Continuum thisInst) // Updates Met and Turbine labels on thisInst.topo/SR/DH/LC map on Input tab
        {
            Met[] checkedMets = thisInst.GetCheckedMets("Input");
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Input");
            int numMets = 0;
            int turbCount = 0;

            if (checkedMets == null)
                numMets = 0;
            else
                numMets = checkedMets.Length;

            NPointSeries[] label = new NPointSeries[numMets];

            for (int i = 0; i < numMets; i++)
            {
                label[i] = new NPointSeries();
                label[i].UseXValues = true;
                label[i].UseZValues = true;
                label[i].Values.Add(0);
                label[i].XValues.Add(checkedMets[i].UTMX);
                label[i].ZValues.Add(checkedMets[i].UTMY);
                label[i].Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                label[i].Labels.Clear();
                label[i].Name = checkedMets[i].name;
                label[i].DataLabelStyle.Visible = false;
                label[i].InteractivityStyle.Tooltip = new NTooltipAttribute(checkedMets[i].name);

                thisInst.cht_NevTopo.Charts[0].Series.Add(label[i]);
            }

            if (checkedTurbines == null)
                turbCount = 0;
            else
                turbCount = checkedTurbines.Length;

            NPointSeries[] labelTurbs = new NPointSeries[turbCount];

            for (int i = 0; i < turbCount; i++)
            {
                labelTurbs[i] = new NPointSeries();
                labelTurbs[i].UseXValues = true;
                labelTurbs[i].UseZValues = true;
                labelTurbs[i].Values.Add(0);
                labelTurbs[i].XValues.Add(checkedTurbines[i].UTMX);
                labelTurbs[i].ZValues.Add(checkedTurbines[i].UTMY);
                labelTurbs[i].Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                labelTurbs[i].Name = checkedTurbines[i].name;
                labelTurbs[i].DataLabelStyle.Visible = false;
                labelTurbs[i].FillStyle = new NColorFillStyle(Color.Red);
                labelTurbs[i].Legend.Mode = SeriesLegendMode.None;

                thisInst.cht_NevTopo.Charts[0].Series.Add(labelTurbs[i]);
                labelTurbs[i].InteractivityStyle.Tooltip = new NTooltipAttribute(checkedTurbines[i].name);

            }

            thisInst.cht_NevTopo.Refresh();

        }

        public void StepLabels(Continuum thisInst, double minX, double maxX, double minY, double maxY)
        {
            // Updates Met, turbine and path of nodes labels on map on Advanced tab
            //   Dim numMets = thisInst.metList.ThisCount()

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

            NPointSeries[] labelTurbs = new NPointSeries[checkedTurbCount];
            NPointSeries[] labelMets = new NPointSeries[checkedMetCount];

            int metPairCount = thisInst.metPairList.PairCount;
            int metListCount = thisInst.chkMetlabelsStep.Items.Count;
            int turbListCount = thisInst.chkTurbLabels.Items.Count;
                        
            int radiusIndex = thisInst.GetRadiusInd("Advanced");

            NodeCollection nodeList = new NodeCollection();

            if (radiusIndex == -1) return;

            int radius = thisInst.radiiList.investItem[radiusIndex].radius;

            for (int i = 0; i < checkedMetCount; i++)
            {
                //   thisMet = thisInst.metList.metItem[i]
                if (checkedMets[i].UTMX > minX && checkedMets[i].UTMX < maxX && checkedMets[i].UTMY > minY && checkedMets[i].UTMY < maxY)
                {
                    labelMets[i] = new NPointSeries();
                    labelMets[i].UseXValues = true;
                    labelMets[i].UseZValues = true;
                    labelMets[i].Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                    labelMets[i].FillStyle = new NColorFillStyle(Color.Blue);
                    labelMets[i].DataLabelStyle.Visible = false;
                    labelMets[i].Name = checkedMets[i].name;

                    labelMets[i].XValues.Add(checkedMets[i].UTMX);
                    labelMets[i].ZValues.Add(checkedMets[i].UTMY);
                    labelMets[i].Values.Add(checkedMets[i].elev);
                    labelMets[i].InteractivityStyle.Tooltip = new NTooltipAttribute(checkedMets[i].name);

                    thisInst.chtTopoStep_Nev.Charts[0].Series.Add(labelMets[i]);
                }
            }

            //  Dim turbCount = thisInst.turbineList.TurbineCount()
            // Dim thisTurb  Turbine = null

            for (int i = 0; i < checkedTurbCount; i++)
            {
                //  thisTurb = thisInst.turbineList.turbineEsts[i]
                if (checkedTurbines[i].AvgWSEst_Count > 0)
                {
                    if (checkedTurbines[i].UTMX > minX && checkedTurbines[i].UTMX < maxX && checkedTurbines[i].UTMY > minY && checkedTurbines[i].UTMY < maxY)
                    {
                        labelTurbs[i] = new NPointSeries();
                        labelTurbs[i].UseXValues = true;
                        labelTurbs[i].UseZValues = true;
                        labelTurbs[i].Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                        labelTurbs[i].FillStyle = new NColorFillStyle(Color.Red);
                        labelTurbs[i].DataLabelStyle.Visible = false;
                        labelTurbs[i].Name = checkedTurbines[i].name;

                        labelTurbs[i].XValues.Add(checkedTurbines[i].UTMX);
                        labelTurbs[i].ZValues.Add(checkedTurbines[i].UTMY);
                        labelTurbs[i].Values.Add(checkedTurbines[i].elev);
                        labelTurbs[i].InteractivityStyle.Tooltip = new NTooltipAttribute(checkedTurbines[i].name);

                        thisInst.chtTopoStep_Nev.Charts[0].Series.Add(labelTurbs[i]);
                    }
                }
            }

            Pair_Of_Mets thisPair = null;
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
                NPointSeries[] label_Nodes = new NPointSeries[numNodes];

                for (int j = 0; j < numNodes; j++)
                {
                    Nodes thisNode = thisPair.WS_Pred[0, radiusIndex].nodePath[j]; // path of nodes is same for all UW&DW models (only varies by radius)
                    if (thisNode.UTMX > minX && thisNode.UTMX < maxX && thisNode.UTMY > minY && thisNode.UTMY < maxY)
                    {
                        label_Nodes[j] = new NPointSeries();
                        label_Nodes[j].UseXValues = true;
                        label_Nodes[j].UseZValues = true;
                        label_Nodes[j].Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                        label_Nodes[j].FillStyle = new NColorFillStyle(Color.Purple);
                        label_Nodes[j].DataLabelStyle.Visible = false;
                        label_Nodes[j].Name = "Node " + (j + 1).ToString();

                        label_Nodes[j].XValues.Add(thisNode.UTMX);
                        label_Nodes[j].ZValues.Add(thisNode.UTMY);
                        label_Nodes[j].Values.Add(thisNode.elev);
                        label_Nodes[j].InteractivityStyle.Tooltip = new NTooltipAttribute("Node " + (j + 1).ToString());

                        thisInst.chtTopoStep_Nev.Charts[0].Series.Add(label_Nodes[j]);
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

                NPointSeries[] label_Nodes = new NPointSeries[numNodes];

                for (int j = 0; j < numNodes; j++)
                {
                    Nodes thisNode = endTurb.WS_Estimate[WS_PredInd].pathOfNodes[j];
                    if (thisNode.UTMX > minX && thisNode.UTMX < maxX && thisNode.UTMY > minY && thisNode.UTMY < maxY)
                    {
                        label_Nodes[j] = new NPointSeries();
                        label_Nodes[j].UseXValues = true;
                        label_Nodes[j].UseZValues = true;
                        label_Nodes[j].Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                        label_Nodes[j].FillStyle = new NColorFillStyle(Color.Purple);
                        label_Nodes[j].DataLabelStyle.Visible = false;
                        label_Nodes[j].Name = "Node " + j + 1;

                        label_Nodes[j].XValues.Add(thisNode.UTMX);
                        label_Nodes[j].ZValues.Add(thisNode.UTMY);
                        label_Nodes[j].Values.Add(thisNode.elev);
                        label_Nodes[j].InteractivityStyle.Tooltip = new NTooltipAttribute("Node " + j + 1);

                        thisInst.chtTopoStep_Nev.Charts[0].Series.Add(label_Nodes[j]);
                    }
                }
            }
        }

        public void MapLabels(Continuum thisInst) //Updates met and turbine labels on map on Maps tab
        {
            string mapToPlot = "";
            if (thisInst.lstMaps.SelectedItems.Count > 0)
            {
                mapToPlot = thisInst.lstMaps.SelectedItems[0].Text;
                Map thisMap = new Map();
                int WD_Ind = 0;

                for (int j = 0; j < thisInst.mapList.ThisCount; j++)
                {
                    if (mapToPlot == thisInst.mapList.mapItem[j].mapName)
                    {
                        thisMap = thisInst.mapList.mapItem[j];
                        break;
                    }
                }

                WD_Ind = thisInst.GetWD_ind("Maps");

                if (thisInst.metList.ThisCount == 0) return;
                int numWD = thisInst.GetNumWD();

                if (WD_Ind == -1 || numWD == 0)
                    return;

                double mapMax = thisMap.FindMax(WD_Ind, numWD);
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

                NPointSeries[] labelMets = new NPointSeries[checkedMetCount];
                NPointSeries[] labelTurbs = new NPointSeries[checkedTurbCount];

                //  Dim metListCount  int = thisInst.chkMetLabels_Maps.Items.Count
                //  Dim turbListCount  int = thisInst.chkTurbLabels_Maps.Items.Count
                //  Dim isChecked  bool = false

                // Dim thisMet  Met

                for (int i = 0; i < checkedMetCount; i++)
                {
                    if (checkedMets[i].UTMX > thisMap.minUTMX && checkedMets[i].UTMX < (thisMap.minUTMX + thisMap.reso * thisMap.numX) &&
                            checkedMets[i].UTMY > thisMap.minUTMY && checkedMets[i].UTMY < (thisMap.minUTMY + thisMap.reso * thisMap.numY))
                    {
                        labelMets[i] = new NPointSeries();
                        labelMets[i].UseXValues = true;
                        labelMets[i].UseZValues = true;
                        labelMets[i].Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                        labelMets[i].FillStyle = new NColorFillStyle(Color.Aqua);
                        labelMets[i].DataLabelStyle.Visible = false;
                        labelMets[i].Name = checkedMets[i].name;
                        labelMets[i].Legend.Mode = SeriesLegendMode.None;
                        labelMets[i].XValues.Add(checkedMets[i].UTMX);
                        labelMets[i].ZValues.Add(checkedMets[i].UTMY);
                        labelMets[i].Values.Add(mapMax);
                        thisInst.c3DMaps_Nev.Charts[0].Series.Add(labelMets[i]);
                    }
                }

                //   Dim turbCount = thisInst.turbineList.TurbineCount()
                //   Dim thisTurb  Turbine

                for (int i = 0; i < checkedTurbCount; i++)
                {
                    if (checkedTurbines[i].UTMX > thisMap.minUTMX && checkedTurbines[i].UTMX < (thisMap.minUTMX + thisMap.reso * thisMap.numX) &&
                            checkedTurbines[i].UTMY > thisMap.minUTMY && checkedTurbines[i].UTMY < (thisMap.minUTMY + thisMap.reso * thisMap.numY))
                    {
                        labelTurbs[i] = new NPointSeries();
                        labelTurbs[i].UseXValues = true;
                        labelTurbs[i].UseZValues = true;
                        labelTurbs[i].Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                        labelTurbs[i].FillStyle = new NColorFillStyle(Color.Red);
                        labelTurbs[i].DataLabelStyle.Visible = false;
                        labelTurbs[i].Name = checkedTurbines[i].name;
                        labelTurbs[i].Legend.Mode = SeriesLegendMode.None;
                        labelTurbs[i].XValues.Add(checkedTurbines[i].UTMX);
                        labelTurbs[i].ZValues.Add(checkedTurbines[i].UTMY);
                        labelTurbs[i].Values.Add(mapMax);
                        thisInst.c3DMaps_Nev.Charts[0].Series.Add(labelTurbs[i]);
                    }

                }
            }
        }

        public void WakeMapLabels(Continuum thisInst, WakeCollection.WakeGridMap thisGrid, int plotInd, int WD_Ind)
        {
            // Updates met and turbine labels on map on Net Turbine Ests. tab

            if (thisInst.metList.ThisCount == 0) return;

            Met[] checkedMets = thisInst.GetCheckedMets("Net");
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Net");
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

            NPointSeries[] labelMets = new NPointSeries[checkedMetCount];
            NPointSeries[] labelTurbs = new NPointSeries[checkedTurbCount];

            int numWD = thisInst.GetNumWD();
            double mapMax = thisInst.wakeModelList.FindMax(thisGrid, WD_Ind, plotInd);

            for (int i = 0; i < checkedMetCount; i++)
            {
                //  thisMet = thisInst.metList.metItem[i]
                labelMets[i].UseXValues = true;
                labelMets[i].UseZValues = true;

                labelMets[i].Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                labelMets[i].FillStyle = new NColorFillStyle(Color.Red);
                labelMets[i].DataLabelStyle.Visible = false;
                labelMets[i].Name = checkedMets[i].name;
                labelMets[i].XValues.Add(checkedMets[i].UTMX);
                labelMets[i].ZValues.Add(checkedMets[i].UTMY);
                labelMets[i].Values.Add(mapMax);

                thisInst.cht3DWakeLoss_Nev.Charts[0].Series.Add(labelMets[i]);
            }

            for (int i = 0; i < checkedTurbCount; i++)
            {
                labelTurbs[i] = new NPointSeries();
                labelTurbs[i].UseXValues = true;
                labelTurbs[i].UseZValues = true;

                labelTurbs[i].Size = new NLength(1.5f, NRelativeUnit.ParentPercentage);
                labelTurbs[i].FillStyle = new NColorFillStyle(Color.Red);
                labelTurbs[i].DataLabelStyle.Visible = false;
                labelTurbs[i].Name = checkedTurbines[i].name;
                labelTurbs[i].XValues.Add(checkedTurbines[i].UTMX);
                labelTurbs[i].ZValues.Add(checkedTurbines[i].UTMY);
                labelTurbs[i].Values.Add(mapMax);
                labelTurbs[i].Legend.Mode = SeriesLegendMode.None;

                thisInst.cht3DWakeLoss_Nev.Charts[0].Series.Add(labelTurbs[i]);
            }
        }

        public void ColoredTextBoxes(Continuum thisInst) // Updates roughness model and Flow Separation model textbox colors and text to indicate whether or not model was used
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

        public void LC_KeySelected(Continuum thisInst) // Updates Land Cover textbox to indicate what LC key has been selected
        {
            bool isNLCD = thisInst.topo.LC_IsDefaultNLCD(thisInst.topo.LC_Key);
            bool isNALC = thisInst.topo.LC_IsDefaultNALC(thisInst.topo.LC_Key);
            bool isEULC = thisInst.topo.LC_IsDefaultEU_Corine(thisInst.topo.LC_Key);

            int numSR = 0;

            try
            {
                numSR = thisInst.topo.LC_Key.Length;
                thisInst.btnViewModNLCD.BackColor = Color.MediumSeaGreen;
            }
            catch
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

        public void MetTurbSummaryAndStatsTable(Continuum thisInst)
        {
            // Updates the met and turbine expo, SRDH and statistics shown in tables on Met & Turbine Summary tab
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

                    Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(null, new TurbineCollection.PowerCurve());
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
                    Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(null, new TurbineCollection.PowerCurve());

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

        public void GrossTurbEstList(Continuum thisInst)
        {
            // Updates the table on Gross Turbines Ests tab showing gross turbine estimates
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

            MetCollection.Weibull_params weibull = new MetCollection.Weibull_params();
            
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
                weibull = thisInst.metList.CalcWeibullParams(thisDist.WS_Dist, thisDist.sectorWS_Dist, thisDist.WS);

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
                    Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(null, new TurbineCollection.PowerCurve()); // Just want FS WS

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

        public void TurbUncertEstList(Continuum thisInst) // Updates the table of turbine uncertainty estimates on Uncertainty tab
        {
            thisInst.lstTurbUncert.Items.Clear();

            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Uncertainty");
            
            int turbCount = thisInst.turbineList.TurbineCount;

            if (thisInst.turbineList.GotEst("WS", powerCurve, null) == true && thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false)
            {

                for (int i = 0; i < turbCount; i++) // Now repopulate it with turbines and modeled parameters
                {
                    Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                    Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(null, new TurbineCollection.PowerCurve()); // just want the WS

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

        public void PowerCurveList(Continuum thisInst) // Updates the list of turbines on Gross Est tab and dropdown lists on Time Series tab, Uncertainty tab and Wake Model generator
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

        public void TurbStats(Continuum thisInst) // Reads turbines that are checked on gross tab and then calls function to calculate stats and fill textboxes on gross tab
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
            thisInst.turbineList.FindParamStats(thisInst, checkedTurbines, WD_Ind, numWD);
        }

        public double FindMin(double[] theseParams) // Returns minimum of theseParams()
        {
            double min = 1000;
            int numParams;

            try
            {
                numParams = theseParams.Length;
            }
            catch
            {
                numParams = 0;
            }

            for (int i = 0; i < numParams; i++)
                if (theseParams[i] < min)
                    min = theseParams[i];

            return min;
        }

        public double FindMax(double[] theseParams)  // Returns maximum of theseParams()
        {
            double max = 0;
            int numParams;

            try
            {
                numParams = theseParams.Length;
            }
            catch
            {
                numParams = 0;
            }

            for (int i = 0; i <= numParams - 1; i++)
                if (theseParams[i] > max)
                    max = theseParams[i];

            return max;

        }

        public void GrossHistogram(Continuum thisInst)
        {
            // Creates a histogram of specified (gross) parameter (elev, WS, AEP, weibull k, weibull A) of selected met and turbines on Gross Turb Ests tab
            thisInst.lstGrossHisto.Items.Clear();
            thisInst.lstGrossHisto.Columns.Clear();

            // Get selected parameter
            string selectParam = "";
            MetCollection metList = thisInst.metList;

            try
            {
                selectParam = thisInst.cboGrossParam.SelectedItem.ToString();
            }
            catch
            {
                return;
            }

            Met[] checkedMets = thisInst.GetCheckedMets("Gross");
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Gross");

            int checkedMetCount = checkedMets.Length;
            int checkedTurbCount = checkedTurbines.Length;

            NChartControl histoCtrl = thisInst.chtHistogram_Nev;
            histoCtrl.Charts[0].Series.Clear();
            histoCtrl.Charts[0].Width = 350;
            histoCtrl.Charts[0].Height = 200;
            histoCtrl.Labels.Clear();
            histoCtrl.Legends[0].Visible = false;

            histoCtrl.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            histoCtrl.Controller.Tools.Add(tooltip);

            if (metList.ThisCount == 0 || (checkedMetCount == 0 && checkedTurbCount == 0))
            {
                histoCtrl.Refresh();
                return;
            }

            if (thisInst.metList.ThisCount == 0) return;
            int WD_Ind = thisInst.GetWD_ind("Gross");
            int numWD = thisInst.GetNumWD();                        

            MetCollection.Weibull_params weibull = new MetCollection.Weibull_params();
            TurbineCollection turbineList = thisInst.turbineList;
            double metAEP = 0;
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
                        metAEP = turbineList.CalcAndReturnGrossAEP(thisDist.WS_Dist, thisInst.metList, powerCurve);
                        metVals[i] = metAEP;
                    }
                    else
                    {
                        numWS = thisDist.WS_Dist.Length;
                        sectDist = new double[numWS];
                        for (int k = 0; k <= numWS - 1; k++)
                            sectDist[k] = thisDist.sectorWS_Dist[WD_Ind, k];

                        metAEP = turbineList.CalcAndReturnGrossAEP(sectDist, thisInst.metList, powerCurve);
                        metAEP = metAEP * thisDist.windRose[WD_Ind];
                        metVals[i] = metAEP;
                    }
                }
                else if (selectParam == "Elevation")
                    metVals[i] = checkedMets[i].elev;
                else if (selectParam == "Weibull k")
                {
                    weibull = metList.CalcWeibullParams(thisDist.WS_Dist, thisDist.sectorWS_Dist, thisDist.WS);
                    if (WD_Ind == numWD)
                        metVals[i] = weibull.overall_k;
                    else
                        metVals[i] = weibull.sector_k[WD_Ind];
                }
                else if (selectParam == "Weibull A")
                {
                    weibull = metList.CalcWeibullParams(thisDist.WS_Dist, thisDist.sectorWS_Dist, thisDist.WS);
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
                    Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(null, new TurbineCollection.PowerCurve());

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
                    histoCtrl.Refresh();
                    return;
                }
            }

            // Find min / max and set interval bins
            int histoSize = 1;
            double histoMin = 1000000;
            double histoMax = 0;
            double histoInt = 1;
            double[] histoVals;
            double Val_avg = 0;

            for (int i = 0; i <= checkedMetCount - 1; i++)
            {
                if (metVals[i] < histoMin)
                    histoMin = metVals[i];

                if (metVals[i] > histoMax)
                    histoMax = metVals[i];

                Val_avg = Val_avg + metVals[i];
            }

            for (int i = 0; i <= checkedTurbCount - 1; i++)
            {
                if (turbVals[i] < histoMin && turbVals[i] != 0)
                    histoMin = turbVals[i];

                if (turbVals[i] > histoMax)
                    histoMax = turbVals[i];

                Val_avg = Val_avg + turbVals[i];
            }

            if (checkedTurbCount + checkedMetCount > 0)
                Val_avg = Val_avg / (checkedTurbCount + checkedMetCount);

            histoMin = histoMin - histoMin * 0.02f;
            histoMax = histoMax + histoMax * 0.02f;

            if (selectParam == "Wind Speed" || selectParam == "Weibull k" || selectParam == "Weibull A")
            {
                histoMin = Math.Round(histoMin, 1);
                histoMax = Math.Round(histoMax, 1);
                Val_avg = Math.Round(Val_avg, 1);
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
                Val_avg = Math.Round(Val_avg, 0);
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

                Val_avg = Val_avg / 100;
                Val_avg = Math.Round(Val_avg, 0);
                Val_avg = Val_avg * 100;

                histoInt = 100;
                histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                while (histoSize > 20)
                {
                    histoInt = histoInt + 100;
                    histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                }
            }

            NBarSeries Met_Histo = new NBarSeries();
            histoCtrl.Charts[0].Series.Add(Met_Histo);
            Met_Histo.FillStyle = new NColorFillStyle(Color.Red);
            Met_Histo.DataLabelStyle.Visible = false;
            Met_Histo.MultiBarMode = MultiBarMode.Series;
            Met_Histo.UseXValues = true;
            Met_Histo.InteractivityStyle.Tooltip = new NTooltipAttribute("Mets");

            NBarSeries Turb_Histo = new NBarSeries();
            histoCtrl.Charts[0].Series.Add(Turb_Histo);
            Turb_Histo.DataLabelStyle.Visible = false;
            Turb_Histo.MultiBarMode = MultiBarMode.Clustered;
            Turb_Histo.UseXValues = true;
            Turb_Histo.InteractivityStyle.Tooltip = new NTooltipAttribute("Turbines");

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

            for (int i = 0; i < histoSize; i++)
            {
                Met_Histo.XValues.Add(histoVals[i]);
                Met_Histo.Values.Add(metHistoVals[i]);
                if (thisInst.turbineList.turbineCalcsDone)
                {
                    Turb_Histo.XValues.Add(histoVals[i]);
                    Turb_Histo.Values.Add(turbHistoVals[i]);
                }                
            }

            NLinearScaleConfigurator linearScaleX = new NLinearScaleConfigurator();
            linearScaleX.MinorTickCount = 0;

            if (selectParam == "Wind Speed")
            {
                linearScaleX.MajorTickMode = MajorTickMode.CustomStep;
                linearScaleX.CustomStep = 0.1f;
            }
            else
            {
                linearScaleX.MajorTickMode = MajorTickMode.AutoMaxCount;
                linearScaleX.MaxTickCount = 10;
            }

            histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator = linearScaleX;

            NLinearScaleConfigurator linearScaleY = (NLinearScaleConfigurator)histoCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator;
            linearScaleY.MinorTickCount = 0;
            linearScaleY.MajorTickMode = MajorTickMode.CustomStep;
            linearScaleY.CustomStep = 1;

            if (selectParam == "Wind Speed")
            {
                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "Wind Speed, m/s";
                histoCtrl.Labels.AddHeader("Histogram of Met and Turbine " + thisInst.modeledHeight + " m Wind Speeds");
            }
            else if (selectParam == "Gross AEP")
            {
                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "Gross AEP, MWh";
                histoCtrl.Labels.AddHeader("Histogram of Met and Turbine Gross AEP");
            }
            else if (selectParam == "Elevation")
            {
                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "Elevation, m";
                histoCtrl.Labels.AddHeader("Histogram of Met and Turbine Elevations");
            }
            else if (selectParam == "Weibull k")
            {
                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "weibull k";
                histoCtrl.Labels.AddHeader("Histogram of Met and Turbine " + thisInst.modeledHeight + " m weibull k");
            }
            else if (selectParam == "Weibull A")
            {
                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "weibull A";
                histoCtrl.Labels.AddHeader("Histogram of Met and Turbine " + thisInst.modeledHeight + " m weibull A");
            }

            histoCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = "Data Count";            
            histoCtrl.Refresh();

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
        }

        public void RoundRobinHistogram(Continuum thisInst)
        {
            // Creates a histogram of Math.Round Robin errors on Uncertainty tab
            NChartControl histoCtrl = thisInst.chtHistoRoundRobin_Nev;
            histoCtrl.Charts[0].Series.Clear();
            histoCtrl.Labels.Clear();
            histoCtrl.Legends[0].Visible = false;
            histoCtrl.Labels.AddHeader("Histogram of Round Robin Estimates");

            int RR_ind;
            string dropText;
            int numMetsUsed;

            if (thisInst.metPairList.RoundRobinCount > 0)
            {
                try
                {
                    RR_ind = thisInst.cboRoundRobin.SelectedIndex;
                    dropText = thisInst.cboRoundRobin.SelectedItem.ToString();
                }
                catch
                {
                    return;
                }

                string[] textSplit = dropText.Split(Convert.ToChar(" "));

                try
                {
                    numMetsUsed = Convert.ToInt16(textSplit[1]);
                }
                catch
                {
                    return;
                }

                MetPairCollection.RR_WS_Ests thisRR = new MetPairCollection.RR_WS_Ests();
                int numMets = thisInst.metList.ThisCount;
                string[] metsUsed = new string[numMets];

                for (int i = 0; i <= numMets - 1; i++)
                    metsUsed[i] = thisInst.metList.metItem[i].name;

                for (int i = 0; i <= thisInst.metPairList.RoundRobinCount - 1; i++)
                {
                    thisRR = thisInst.metPairList.roundRobinEsts[i];
                    bool sameMets = thisInst.metList.sameMets(metsUsed, thisRR.metsUsed);

                    if ((sameMets == true && thisRR.metSubSize == numMetsUsed))
                    {
                        RR_ind = i;
                        break;
                    }
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

                NBarSeries histoSeries = new NBarSeries();
                histoSeries.UseXValues = true;
                histoSeries.DataLabelStyle.Visible = false;
                histoSeries.FillStyle = new NColorFillStyle(Color.Red);
                histoSeries.PercentValueFormatter = new NNumericValueFormatter(Nevron.NumericValueFormat.Percentage);

                double[] histoVals = new double[histoSize];

                for (int i = 0; i < histoSize; i++)
                    histoVals[i] = histoMin + i * histoInt;

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
                {
                    histoSeries.XValues.Add(histoVals[i] * 100);
                    histoSeries.Values.Add(errHistoVals[i]);
                }

                histoCtrl.Charts[0].Series.Add(histoSeries);
                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "WS Est Error, %";

                //  Dim linearScaleY  NLinearScaleConfigurator = CType(histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator, NLinearScaleConfigurator)
                //  linearScaleY.MajorTickMode = MajorTickMode.CustomStep
                //  linearScaleY.CustomStep = 1

                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = "Data Count";

            }

            histoCtrl.Refresh();

        }

        public void TurbineList(Continuum thisInst) // Updates all plots and tables on all tabs that have the turbine sites to reflect new list
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
            bool haveString = false;

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
                    haveString = false;
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

        /*     public int Get_UWDW_ind_Gross_Turb(Continuum thisInst) // Returns selected model index (0 is default, 1 is Site-calibrated) on Gross Turb Ests tab
             {
                 int modelInd = 0;
                 try {
                     modelInd = thisInst.cboGrossModel.SelectedIndex;
                     if (modelInd == -1) {
                         if (thisInst.cboGrossModel.Items.Count > 0)
                             modelInd = thisInst.cboGrossModel.Items.Count - 1;
                         else
                             modelInd = 0;

                     }
                 }
                 catch  {
                     if (thisInst.cboGrossModel.Items.Count > 0)
                         modelInd = thisInst.cboGrossModel.Items.Count - 1;
                     else
                         modelInd = 0;
                 }

                 return modelInd;
             }

         */
        public void Met_Turbine_Summary_TAB(Continuum thisInst)
        {
            // Update the plots and tables on Met & Turbine Summary tab            
            MetAndTurbExpoPlots(thisInst);
            MetTurbSummaryAndStatsTable(thisInst);
        }

        public void MetAndTurbExpoPlots(Continuum thisInst) // Calls thisExpoPlot to update each of the 8 plots on the 'Met and Turbine Summary' tab
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

        public void thisExpoPlot(Continuum thisInst, string plotName, int radiusIndex, int WD_Ind)
        {
            // Updates plot on //Met and Turbine Summary// tab based on plotName
            NChartControl expoSR_ChtCtrl = new NChartControl();
            
            var model = thisInst.chtUWExpo.Model;

            if (plotName == "UW Expo")
            {
                thisInst.chtUWExpo.Model = new PlotModel();
                model = thisInst.chtUWExpo.Model;
                model.Title = GetExpoSR_ChartTitle(plotName, Convert.ToInt16(thisInst.modeledHeight));
                expoSR_ChtCtrl = thisInst.chtUWExpo_Nev;
            }
                
            else if (plotName == "DW Expo")
                expoSR_ChtCtrl = thisInst.chtDWExpo_Nev;
            else if (plotName == "DW-UW Expo")
                expoSR_ChtCtrl = thisInst.chtDWUWExpo_Nev;
            else if (plotName == "Elev")
                expoSR_ChtCtrl = thisInst.chtElev_Nev;
            else if (plotName == "UW SR")
                expoSR_ChtCtrl = thisInst.chtUW_SR_Nev;
            else if (plotName == "DW SR")
                expoSR_ChtCtrl = thisInst.chtDW_SR_Nev;
            else if (plotName == "UW DH")
                expoSR_ChtCtrl = thisInst.chtUW_DH_Nev;
            else if (plotName == "DW DH")
                expoSR_ChtCtrl = thisInst.chtDW_DH_Nev;

            

            expoSR_ChtCtrl.Charts[0].Series.Clear();
            expoSR_ChtCtrl.Labels.Clear();
            expoSR_ChtCtrl.Controller.Tools.Clear();
            expoSR_ChtCtrl.Legends[0].Visible = false;

            if ((plotName == "UW SR" || plotName == "DW SR" || plotName == "UW DH" || plotName == "DW DH") && thisInst.topo.gotSR == false)
            {
                expoSR_ChtCtrl.Refresh();
                return;
            }

            NTooltipTool tooltip = new NTooltipTool();
            expoSR_ChtCtrl.Controller.Tools.Add(tooltip);

            if (thisInst.metList.ThisCount == 0)
            {
                expoSR_ChtCtrl.Refresh();
                return;
            }

            int numWD = thisInst.metList.numWD;
            int hubHeight = (int)thisInst.modeledHeight;
            Met.TOD thisTOD = thisInst.GetSelectedTOD("Summary");
            Met.Season thisSeason = thisInst.GetSelectedSeason("Summary");

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(expoSR_ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);            
            scaleConfiguratorX.Title.Text = GetExpoSR_AxisTitle(plotName);
                       

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(expoSR_ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "Wind Speed, m/s";

            NLabel chartTitle = expoSR_ChtCtrl.Labels.AddHeader(GetExpoSR_ChartTitle(plotName, hubHeight));

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Summary");
            Met[] checkedMets = thisInst.GetCheckedMets("Summary");

            bool plotTurbs = false;
            if (thisInst.turbineList.GotEst("WS", new TurbineCollection.PowerCurve(), null) == true && thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false)
                plotTurbs = true;

            if (checkedMets == null && checkedTurbines == null)
                return;

            double minXValue = 10000;
            double maxXValue = -10000;

            // Create label series
            var ms = new ScatterSeries();
            ms.BinSize = 8;
            ms.MarkerSize = 5;
            ms.MarkerStrokeThickness = 1;
            ms.MarkerType = MarkerType.Circle;
            ms.MarkerFill = OxyColors.Black;
            ms.MarkerStroke = OxyColors.Red;

            LinearAxis xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Bottom;
            xAxis.Title = "UW Exposure, m";
            LinearAxis yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Left;
            yAxis.Title = "Wind Speed, m/s";

            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            if (checkedMets != null)
            {
                for (int i = 0; i < checkedMets.Length; i++)
                {                    
                    NPointSeries metDataSeries = new NPointSeries();
                    metDataSeries.DataLabelStyle.Visible = false;
                    metDataSeries.UseXValues = true;
                    metDataSeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
                    expoSR_ChtCtrl.Charts[0].Series.Add(metDataSeries);
                    Met.WSWD_Dist thisDist = checkedMets[i].GetWS_WD_Dist(thisInst.modeledHeight, thisTOD, thisSeason);

                    if (checkedMets[i].expo != null)
                    {
                        double thisXVal = GetXValForExpoSR_Plot(plotName, WD_Ind, checkedMets[i].expo[radiusIndex], checkedMets[i].elev, thisDist.windRose);
                        metDataSeries.XValues.Add(thisXVal);

                        if (thisXVal < minXValue)
                            minXValue = thisXVal;

                        if (thisXVal > maxXValue)
                            maxXValue = thisXVal;

                        if (WD_Ind == numWD)
                        {
                            metDataSeries.Values.Add(thisDist.WS);
                            ms.Points.Add(new ScatterPoint(thisXVal, thisDist.WS));
                        }
                            
                        else
                        {
                            metDataSeries.Values.Add(thisDist.WS * thisDist.sectorWS_Ratio[WD_Ind]);
                            ms.Points.Add(new ScatterPoint(thisXVal, thisDist.WS * thisDist.sectorWS_Ratio[WD_Ind]));
                        }
                            
                    }

                    metDataSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(checkedMets[i].name);
                }
            }

            model.Series.Add(ms);

            // Refresh plot
            thisInst.chtUWExpo.Refresh();

            if (plotTurbs == true && checkedTurbines != null)
            {

                for (int i = 0; i < checkedTurbines.Length; i++)
                {
                    NPointSeries turbDataSeries = new NPointSeries();
                    turbDataSeries.DataLabelStyle.Visible = false;
                    turbDataSeries.UseXValues = true;
                    turbDataSeries.FillStyle = new NColorFillStyle(Color.Red);
                    turbDataSeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
                    expoSR_ChtCtrl.Charts[0].Series.Add(turbDataSeries);
                    
                    Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(null, new TurbineCollection.PowerCurve());

                    if (checkedTurbines[i].expo != null)
                    {
                        double thisXVal = GetXValForExpoSR_Plot(plotName, WD_Ind, checkedTurbines[i].expo[radiusIndex], checkedTurbines[i].elev, avgEst.freeStream.windRose);
                        turbDataSeries.XValues.Add(thisXVal);
                        turbDataSeries.Values.Add(checkedTurbines[i].GetAvgOrSectorWS_Est(null, WD_Ind, "WS", new TurbineCollection.PowerCurve()));

                        if (thisXVal < minXValue)
                            minXValue = thisXVal;

                        if (thisXVal > maxXValue)
                           maxXValue = thisXVal;

                    }
                    turbDataSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(checkedTurbines[i].name);
                }
            }

            // Customize axis

            expoSR_ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator = new NLinearScaleConfigurator();
            NLinearScaleConfigurator linearConfiguratorX = (NLinearScaleConfigurator)(expoSR_ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            linearConfiguratorX.MajorTickMode = MajorTickMode.CustomStep;
            double customStep = (maxXValue - minXValue) / 4;
            
            if (plotName == "Elev")
                customStep = Math.Round(customStep, 0);
            else if (plotName == "UW Expo" || plotName == "DW Expo" || plotName == "DW-UW Expo")
                customStep = Math.Round(customStep, 1);
            else
            {
                if (Math.Round(customStep, 2) != 0)
                    customStep = Math.Round(customStep, 2);
                else if (Math.Round(customStep, 3) != 0)
                    customStep = Math.Round(customStep, 3);
                else
                    customStep = 0.001;
            }
                

            linearConfiguratorX.CustomStep = customStep;         
            
            expoSR_ChtCtrl.Refresh();
        }

        public string GetExpoSR_ChartTitle(string plotName, int thisHeight) // Returns chart title of specified plot
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

        public string GetExpoSR_AxisTitle(string plotName) // Returns x-axis title of specified plot
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


        public double GetXValForExpoSR_Plot(string plotName, int WD_Ind, Exposure thisExpoSR, double elev, double[] windRose)
        {
            // Returns X value for specified plot on //Met and Turbine Summary//
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


        public double[] CalcDistAlongNodes(TopoInfo.TopoGrid site1, TopoInfo.TopoGrid site2, Nodes[] pathOfNodes)
        {
            // Returns array of cumulative distances along path of nodes from site1 to site2                       
            int numNodes = 0;

            try
            {
                numNodes = pathOfNodes.Length;
            }
            catch
            {
                numNodes = 0;
            }

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

        public void PlotAdvancedTable(Continuum thisInst)
        {
            // Updates the path of nodes plot on Advanced tab to show selected parameters

            NChartControl nodePthChtCtrl = thisInst.chtNodeWS_Nev;
            nodePthChtCtrl.Charts[0].Series.Clear();
            nodePthChtCtrl.Charts[0].Width = 300;
            nodePthChtCtrl.Charts[0].Height = 100;
            nodePthChtCtrl.Controller.Tools.Clear();
            nodePthChtCtrl.Legends[0].Visible = false;

            if (thisInst.modelList.ModelCount == 0) return;
            NTooltipTool toolTip = new NTooltipTool();
            nodePthChtCtrl.Controller.Tools.Add(toolTip);

            if (thisInst.lstPathNodes.Items.Count == 0)
            {
                nodePthChtCtrl.Refresh();
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
            int Y_ind = 0;

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
            string X_label = "distance, m";

            nodePthChtCtrl.Labels.AddHeader(titleLabel);
            nodePthChtCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = X_label;
            double[] distArr = CalcDistAlongNodes(site1, site2, pathOfNodes);
            int numPoints = distArr.Length;

            Y_ind = 0;
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
                        NLineSeries dataSeries = new NLineSeries();
                        dataSeries.DataLabelStyle.Visible = false;
                        dataSeries.UseXValues = true;
                        dataSeries.Name = colName;

                        dataSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(colName);
                        nodePthChtCtrl.Charts[0].Series.Add(dataSeries);

                        if (colName == "Actual WS")
                        {
                            try
                            {
                                thisTableVal = Convert.ToSingle(thisInst.lstPathNodes.Items[numPoints - 1].SubItems[i].Text);
                                dataSeries.XValues.Add(distArr[distArr.Length - 1]);
                                dataSeries.Values.Add(thisTableVal);
                            }
                            catch { }
                        }
                        else
                        {
                            Y_ind = 0;

                            foreach (ListViewItem item in thisInst.lstPathNodes.Items)
                            {
                                if (item.SubItems[i].Text != "")
                                    thisTableVal = Convert.ToSingle(item.SubItems[i].Text);
                                else
                                    thisTableVal = 0;

                                dataSeries.XValues.Add(distArr[Y_ind]);
                                dataSeries.Values.Add(thisTableVal);

                                Y_ind++;
                            }
                        }
                        series1Ind++;
                    }
                }

                for (int i = 0; i < series1Ind; i++)
                {
                    NLineSeries thisSeries = (NLineSeries)nodePthChtCtrl.Charts[0].Series[i];
                    FormatPathToNodeLines(ref thisSeries);
                    nodePthChtCtrl.Charts[0].Series[i] = thisSeries;
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

                            NLineSeries dataSeries = new NLineSeries();
                            dataSeries.DataLabelStyle.Visible = false;
                            dataSeries.UseXValues = true;
                            dataSeries.Name = colName;
                            dataSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(colName);
                            nodePthChtCtrl.Charts[0].Series.Add(dataSeries);
                            dataSeries.DisplayOnAxis(StandardAxis.PrimaryY, true);
                            dataSeries.DisplayOnAxis(StandardAxis.SecondaryY, false);

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

                                dataSeries.XValues.Add(distArr[distArr.Length - 1]);
                                dataSeries.Values.Add(thisTableVal);

                            }
                            else
                            {
                                Y_ind = 0;
                                foreach (ListViewItem item in thisInst.lstPathNodes.Items)
                                {
                                    if (item.SubItems[i].Text != "")
                                        thisTableVal = Convert.ToSingle(item.SubItems[i].Text);
                                    else
                                        thisTableVal = 0;

                                    dataSeries.XValues.Add(distArr[Y_ind]);
                                    dataSeries.Values.Add(thisTableVal);
                                    Y_ind++;

                                }
                            }
                            series1Ind++;
                        }
                        else
                        {

                            NLineSeries dataSeries = new NLineSeries();
                            dataSeries.DataLabelStyle.Visible = false;
                            dataSeries.UseXValues = true;
                            dataSeries.Name = colName;
                            dataSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(colName);
                            nodePthChtCtrl.Charts[0].Series.Add(dataSeries);

                            dataSeries.DisplayOnAxis(StandardAxis.PrimaryY, false);
                            dataSeries.DisplayOnAxis(StandardAxis.SecondaryY, true);

                            Y_ind = 0;
                            foreach (ListViewItem item in thisInst.lstPathNodes.Items)
                            {
                                if (item.SubItems[i].Text != "")
                                    thisTableVal = Convert.ToSingle(item.SubItems[i].Text);
                                else
                                    thisTableVal = 0;

                                dataSeries.XValues.Add(distArr[Y_ind]);
                                dataSeries.Values.Add(thisTableVal);
                                Y_ind++;
                            }
                            series2Ind++;
                        }

                    }

                }

                for (int i = 0; i < nodePthChtCtrl.Charts[0].Series.Count; i++)
                {
                    NLineSeries thisSeries = (NLineSeries)nodePthChtCtrl.Charts[0].Series[i];
                    FormatPathToNodeLines(ref thisSeries);
                    nodePthChtCtrl.Charts[0].Series[i] = thisSeries;
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

            nodePthChtCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = Y_label_1;

            if (Y_label_2 != "")
            {
                nodePthChtCtrl.Charts[0].Axis(StandardAxis.SecondaryY).ScaleConfigurator.Title.Text = Y_label_2;
                nodePthChtCtrl.Charts[0].Axis(StandardAxis.SecondaryY).Visible = true;
            }
            else
            {
                nodePthChtCtrl.Charts[0].Axis(StandardAxis.SecondaryY).ScaleConfigurator.Title.Text = Y_label_2;
                nodePthChtCtrl.Charts[0].Axis(StandardAxis.SecondaryY).Visible = false;
            }

            nodePthChtCtrl.Refresh();

        }

        public void FormatPathToNodeLines(ref NLineSeries thisSeries)
        {
            // Formats series of Path to Node plot
            Color thisColor = new Color();

            if (thisSeries.Name == "Elevation") thisColor = Color.Black;
            if (thisSeries.Name == "P10 UW") thisColor = Color.LightBlue;
            if (thisSeries.Name == "P10 DW") thisColor = Color.LightGreen;
            if (thisSeries.Name == "UW Expo") thisColor = Color.Blue;
            if (thisSeries.Name == "DW Expo") thisColor = Color.Green;
            if (thisSeries.Name == "UW Roughness") thisColor = Color.Maroon;
            if (thisSeries.Name == "DW Roughness") thisColor = Color.Brown;
            if (thisSeries.Name == "UW Disp H") thisColor = Color.Maroon;
            if (thisSeries.Name == "DW Disp H") thisColor = Color.Brown;
            if (thisSeries.Name == "UW coeff") thisColor = Color.SteelBlue;
            if (thisSeries.Name == "DW coeff") thisColor = Color.SpringGreen;
            if (thisSeries.Name == "dWS UW Expo") thisColor = Color.DodgerBlue;
            if (thisSeries.Name == "dWS DW Expo") thisColor = Color.ForestGreen;
            if (thisSeries.Name == "dWS UW SRDH") thisColor = Color.Firebrick;
            if (thisSeries.Name == "dWS DW SRDH") thisColor = Color.Sienna;
            if (thisSeries.Name == "WS Est.") thisColor = Color.DarkOrchid;
            if (thisSeries.Name == "Equiv WS") thisColor = Color.SpringGreen;

            if (thisSeries.Name == "UTMX" || thisSeries.Name == "UTMY" || thisSeries.Name == "Elevation" || thisSeries.Name == "P10 UW" || thisSeries.Name == "P10 DW" ||
                    thisSeries.Name == "UW Disp H" || thisSeries.Name == "DW Disp H" || thisSeries.Name == "DW Disp H" || thisSeries.Name == "DW Disp H")
                thisSeries.BorderStyle.Pattern = LinePattern.Dash;
            else
                thisSeries.BorderStyle.Pattern = LinePattern.Solid;

            thisSeries.BorderStyle.Color = thisColor;
            thisSeries.MarkerStyle.FillStyle = new NColorFillStyle(thisColor);
            thisSeries.MarkerStyle.Width = new NLength(1, NRelativeUnit.ParentPercentage);
            thisSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            thisSeries.DataLabelStyle.Visible = false;
            thisSeries.MarkerStyle.Visible = true;

            if (thisSeries.Name == "Actual WS")
            {
                thisColor = Color.Crimson;
                thisSeries.MarkerStyle.Width = new NLength(2, NRelativeUnit.ParentPercentage);
                thisSeries.MarkerStyle.Height = new NLength(2, NRelativeUnit.ParentPercentage);
                thisSeries.BorderStyle.Color = Color.White;
                thisSeries.BorderStyle.Width = new NLength(0.05f, NRelativeUnit.ParentPercentage);
            }

        }

        public void WS_or_WR_Plot(Continuum thisInst) // Updates either the wind speed distribution or wind rose plot on Gross Turb Est tab (depending on which is selected from dropdown box)
        {

            string WS_or_WR = "";
            thisInst.chtWSDist_Nev.Charts[0].Series.Clear();
            thisInst.chtWSDist_Nev.Refresh();
            thisInst.chtWSDist_Nev.Legends[0].Visible = false;
            thisInst.chtEst_Roses_Nev.Charts[0].Series.Clear();
            thisInst.chtEst_Roses_Nev.Refresh();
            thisInst.chtEst_Roses_Nev.Legends[0].Visible = false;

            try
            {
                WS_or_WR = thisInst.cboWS_or_WD.SelectedItem.ToString();
            }
            catch
            {
                thisInst.cboWS_or_WD.SelectedIndex = 0;
                return;
            }

            if (WS_or_WR == "WS")
            {
                thisInst.chtWSDist_Nev.Visible = true;
                thisInst.chtEst_Roses_Nev.Visible = false;
                WSDist_Plot(thisInst);
            }
            else
            {
                thisInst.chtWSDist_Nev.Visible = false;
                thisInst.chtEst_Roses_Nev.Visible = true;
                WR_Plot(thisInst);
            }
        }

        public void WR_Plot(Continuum thisInst) // Updates wind rose plot on 'Gross Turbine Ests' tab
        {

            NChartControl chtWindRose = thisInst.chtEst_Roses_Nev;
            NRadarChart WR_Chart = new NRadarChart();
            chtWindRose.Clear();
            chtWindRose.Labels.AddHeader("Wind Roses at Met and Turbine Sites");
            chtWindRose.Charts.Add(WR_Chart);
            chtWindRose.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            chtWindRose.Controller.Tools.Add(tooltip);

            Met[] checkedMets = thisInst.GetCheckedMets("Gross");
            int checkedMetCount = checkedMets.Length;

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Gross");
            int checkedTurbCount = checkedTurbines.Length;

            int metCount = thisInst.metList.ThisCount;
            if (metCount == 0) return;
            int turbCount = thisInst.turbineList.TurbineCount;
            int numWD = thisInst.GetNumWD();            
            
            AddAxisToRadarPlot(ref WR_Chart, numWD, true, 10);

            for (int j = 0; j < checkedMetCount; j++)
            {
                Met.WSWD_Dist thisDist = checkedMets[j].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                AddSeriesToRadarPlot(ref WR_Chart, thisDist.windRose, checkedMets[j].name, GetMetOrTurbColor(j), true, false);
            }
            
            for (int j = 0; j < checkedTurbCount; j++)
            {
                Turbine.Avg_Est avgEst = checkedTurbines[j].GetAvgWS_Est(null, new TurbineCollection.PowerCurve());

                if (avgEst.freeStream.windRose == null)
                    return;

                AddSeriesToRadarPlot(ref WR_Chart, avgEst.freeStream.windRose, checkedTurbines[j].name, GetMetOrTurbColor(checkedMetCount + j), true, true);
            }

            chtWindRose.Refresh();
        }

        public void WSDist_Plot(Continuum thisInst) // Updates wind speed distribution plot on Gross Turbine Ests tab 
        {

            NChartControl WS_DistCtl = thisInst.chtWSDist_Nev;
            WS_DistCtl.Charts[0].Series.Clear();
            WS_DistCtl.Labels.Clear();
            WS_DistCtl.Controller.Tools.Clear();
            WS_DistCtl.Legends[0].Visible = false;
                        
            if (thisInst.metList.ThisCount == 0) return;
            int numWD = thisInst.GetNumWD();

            if (numWD <= 1)
            {
                WS_DistCtl.Refresh();
                return;
            }

            int WD_Ind = thisInst.GetWD_ind("Gross");

            if (WD_Ind == -1)
                return;

            Met[] checkedMets = thisInst.GetCheckedMets("Gross");
            int checkedMetCount = checkedMets.Length;

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Gross");
            int checkedTurbCount = checkedTurbines.Length;

            NTooltipTool tooltip = new NTooltipTool();
            WS_DistCtl.Controller.Tools.Add(tooltip);
            WS_DistCtl.Labels.AddHeader("Wind Speed Distributions");

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(WS_DistCtl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Wind Speed, m/s";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(WS_DistCtl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "Freq. of Occurrence";

            if ((thisInst.metList.ThisCount > 0) && (checkedMetCount > 0 || checkedTurbCount > 0))
            {
                Met thisMet = thisInst.metList.metItem[0];
                double WS_FirstInt = thisInst.metList.WS_FirstInt;
                double WS_IntSize = thisInst.metList.WS_IntSize;
                int numWS = thisInst.metList.numWS;

                for (int i = 0; i <= checkedMetCount - 1; i++)
                {
                    NLineSeries WS_DistSeries = new NLineSeries();
                    WS_DistSeries.DataLabelStyle.Visible = false;
                    WS_DistSeries.Name = checkedMets[i].name;
                    WS_DistCtl.Charts[0].Series.Add(WS_DistSeries);
                    WS_DistSeries.BorderStyle.Color = GetMetOrTurbColor(i);
                    WS_DistSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                    WS_DistSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(checkedMets[i].name);
                    Met.WSWD_Dist thisDist = checkedMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                    for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
                    {
                        WS_DistSeries.XValues.Add(WS_FirstInt + WS_ind * WS_IntSize - WS_IntSize / 2);

                        if (WD_Ind == numWD)
                            WS_DistSeries.Values.Add(thisDist.WS_Dist[WS_ind]);
                        else
                            WS_DistSeries.Values.Add(thisDist.sectorWS_Dist[WD_Ind, WS_ind]);
                    }
                }

                if (thisInst.turbineList.GotEst("WS", new TurbineCollection.PowerCurve(), null) == true)
                {
                    for (int i = 0; i < checkedTurbCount; i++)
                    {
                        NLineSeries WS_DistSeries = new NLineSeries();
                        WS_DistSeries.DataLabelStyle.Visible = false;
                        WS_DistSeries.Name = checkedTurbines[i].name;
                        WS_DistCtl.Charts[0].Series.Add(WS_DistSeries);
                        WS_DistSeries.BorderStyle.Color = GetMetOrTurbColor(checkedMetCount + i);
                        WS_DistSeries.BorderStyle.Pattern = LinePattern.Dash;
                        WS_DistSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                        WS_DistSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(checkedTurbines[i].name);

                        if (checkedTurbines[i].AvgWSEst_Count == 0)
                            return;

                        Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(null, new TurbineCollection.PowerCurve());

                        for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
                        {
                            WS_DistSeries.XValues.Add(WS_FirstInt + WS_ind * WS_IntSize - WS_IntSize / 2);

                            if (WD_Ind == numWD)
                                WS_DistSeries.Values.Add(avgEst.freeStream.WS_Dist[WS_ind]);
                            else
                                WS_DistSeries.Values.Add(avgEst.freeStream.sectorWS_Dist[WD_Ind, WS_ind]);
                        }
                    }
                }
            }

            WS_DistCtl.Refresh();
        }

        public Color GetMetOrTurbColor(int Met_Turb_Ind) // Returns color (RGB values) based on index of met or turbine for series formatting
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


        public void WakedWSDistPlot(Continuum thisInst)
        {
            // Updates (net/waked) wind speed distribution plot on Net Turb Ests tab

            if (thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy || thisInst.BW_worker.BackgroundWorker_Map.IsBusy)
                return;

            NChartControl wakedDist = thisInst.chtWakedWSDists_Nev;
            wakedDist.Charts[0].Series.Clear();
            wakedDist.Charts[0].Width = 200;
            wakedDist.Charts[0].Height = 125;
            wakedDist.Labels.Clear();
            wakedDist.Legends[0].Visible = false;
            wakedDist.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            wakedDist.Controller.Tools.Add(tooltip);

            double maxFreq = 0;            
            Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();

            if (thisWakeModel == null)
            {
                wakedDist.Refresh();
                return;
            }

            int numWD = thisInst.GetNumWD();

            if (numWD <= 1)
            {
                wakedDist.Refresh();
                return;
            }

            int WD_Ind = thisInst.GetWD_ind("Net");
            if (WD_Ind == -1)
            {
                wakedDist.Refresh();
                return;
            }

            Met thisMet = new Met();

            if (thisInst.metList.ThisCount == 0)
                return;
            else
                thisMet = thisInst.metList.metItem[0];

            double WS_FirstInt = thisInst.metList.WS_FirstInt;
            double WS_IntSize = thisInst.metList.WS_IntSize;

            int numWS = thisInst.metList.numWS;

            double[] Turb_X_WS = new double[numWS];
            double[] Turb_Y_Freq = new double[numWS];

            int plotInd = 0;
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Net");

            if (checkedTurbines == null)
            {
                wakedDist.Refresh();
                return;
            }

            for (int i = 0; i < checkedTurbines.Length; i++)
            {
                Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(thisWakeModel, thisWakeModel.powerCurve);

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

                    NLineSeries thisSeries = new NLineSeries();
                    thisSeries.DataLabelStyle.Visible = false;
                    thisSeries.BorderStyle.Color = GetMetOrTurbColor((int)(100 * (float)plotInd / checkedTurbines.Length));
                    thisSeries.Name = checkedTurbines[i].name;

                    for (int j = 0; j < numWS; j++)
                    {
                        thisSeries.XValues.Add(Turb_X_WS[j]);
                        thisSeries.Values.Add(Turb_Y_Freq[j]);
                    }

                    thisSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(thisSeries.Name);
                    wakedDist.Charts[0].Series.Add(thisSeries);

                    plotInd++;
                }

            }

            string Xlabel1 = "Wind Speed, m/s";
            string yLabel1 = "Freq. of Occurrence";

            double height = thisInst.modeledHeight;

            wakedDist.Labels.AddHeader(height + " m Waked WS Distribution");
            wakedDist.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = Xlabel1;
            wakedDist.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = yLabel1;

            wakedDist.Refresh();
        }

        public void PowerCrvPlot(Continuum thisInst) // Updates plot of power and thrust curves on Gross Turb Ests tab
        {

            int chkPowerCurveCount = thisInst.lstPowerCurveList.CheckedItems.Count;
            int powerCurveCount = thisInst.turbineList.PowerCurveCount;
            int numMets = thisInst.metList.ThisCount;

            NChartControl crvCtrl = thisInst.chtPowerCrv_Nev;
            crvCtrl.Legends[0].Visible = false;
            crvCtrl.Charts[0].Series.Clear();

            if (powerCurveCount == 0)
            {
                crvCtrl.Refresh();
                return;
            }

            TurbineCollection.PowerCurve thisPowerCurve = new TurbineCollection.PowerCurve();            
            crvCtrl.Labels.Clear();
            crvCtrl.Labels.AddHeader("Power and Thrust Curves");

            crvCtrl.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            crvCtrl.Controller.Tools.Add(tooltip);
            
            if (chkPowerCurveCount > 0)
            {                
                for (int i = 0; i < chkPowerCurveCount; i++)
                {                    
                    thisPowerCurve = thisInst.turbineList.GetPowerCurve(thisInst.lstPowerCurveList.CheckedItems[i].Text);
                                        
                    if (thisPowerCurve.power != null)
                    {
                        int numWS = thisPowerCurve.power.Length;
                        double[,] metX_WS = new double[powerCurveCount, numWS];
                        double[,] metY_Power = new double[powerCurveCount, numWS];
                        double[,] metY_Thrust = new double[powerCurveCount, numWS];

                        NLineSeries powerCurve = new NLineSeries();
                        powerCurve.DataLabelStyle.Visible = false;
                        powerCurve.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                        powerCurve.UseXValues = true;

                        crvCtrl.Charts[0].Series.Add(powerCurve);
                        powerCurve.DisplayOnAxis(StandardAxis.PrimaryY, true);
                        powerCurve.DisplayOnAxis(StandardAxis.SecondaryY, false);

                        NLineSeries thrustCurve = new NLineSeries();
                        crvCtrl.Charts[0].Series.Add(thrustCurve);
                        thrustCurve.DataLabelStyle.Visible = false;
                        thrustCurve.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                        thrustCurve.BorderStyle.Pattern = LinePattern.Dash;
                        thrustCurve.DisplayOnAxis(StandardAxis.PrimaryY, false);
                        thrustCurve.DisplayOnAxis(StandardAxis.SecondaryY, true);
                        thrustCurve.UseXValues = true;

                        for (int j = 0; j < thisPowerCurve.power.Length; j++)
                        {
                            powerCurve.XValues.Add(thisPowerCurve.firstWS + j * thisPowerCurve.wsInt);
                            powerCurve.Values.Add(thisPowerCurve.power[j]);
                            thrustCurve.XValues.Add(thisPowerCurve.firstWS + j * thisPowerCurve.wsInt);
                            thrustCurve.Values.Add(thisPowerCurve.thrustCoeff[j]);
                        }

                        powerCurve.Name = "Power: " + thisPowerCurve.name;
                        thrustCurve.Name = "Thrust Coeff: " + thisPowerCurve.name;

                        powerCurve.InteractivityStyle.Tooltip = new NTooltipAttribute(powerCurve.Name);
                        thrustCurve.InteractivityStyle.Tooltip = new NTooltipAttribute(thrustCurve.Name);

                        if (i == 0)
                        {
                            powerCurve.BorderStyle.Color = Color.Red;
                            thrustCurve.BorderStyle.Color = Color.CornflowerBlue;
                        }
                        else if (i == 1)
                        {
                            powerCurve.BorderStyle.Color = Color.BlueViolet;
                            thrustCurve.BorderStyle.Color = Color.BlueViolet;
                        }
                        else if (i == 2)
                        {
                            powerCurve.BorderStyle.Color = Color.Coral;
                            thrustCurve.BorderStyle.Color = Color.Coral;
                        }
                        else if (i == 3)
                        {
                            powerCurve.BorderStyle.Color = Color.DarkGreen;
                            thrustCurve.BorderStyle.Color = Color.DarkGreen;
                        }
                        else if (i == 4)
                        {
                            powerCurve.BorderStyle.Color = Color.Firebrick;
                            thrustCurve.BorderStyle.Color = Color.Firebrick;
                        }
                        else
                        {
                            powerCurve.BorderStyle.Color = Color.Gold;
                            thrustCurve.BorderStyle.Color = Color.Gold;
                        }
                    }
                }

                double height = thisInst.modeledHeight;

                string Xlabel1 = "Wind Speed, m/s";
                string yLabel1 = "Power, kW";
                string Ylabel2 = "Thrust coeff.";

                crvCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = Xlabel1;
                crvCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = yLabel1;
                crvCtrl.Charts[0].Axis(StandardAxis.SecondaryY).ScaleConfigurator.Title.Text = Ylabel2;

            }

            crvCtrl.Refresh();

        }

        public void DownhillLogLogPlot(Continuum thisInst, Model thisModel, string DH_plot_to_show)
        {
            // Updates Log-Log plot : Downhill coeff vs P10 Expo on Advanced tab
            Model origModel = new Model();
            int numWD = thisInst.GetNumWD();
            int WD_Ind = thisInst.GetWD_ind("Advanced");
            origModel.SizeArrays(numWD);
            origModel.SetDefaultModelCoeffs(numWD);
            NodeCollection nodeList = new NodeCollection();

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
                            negDW_Coeffs[i] = thisModel.sep_A_DW[WD_Ind] * (double)Math.Pow(P10_Expo[i], thisModel.sep_B_DW[WD_Ind]);
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

                                negDW_Coeffs[P10_ind] = thisModel.sep_A_DW[j] * (double)Math.Pow(P10_Expo[P10_ind], thisModel.sep_B_DW[j]);
                                P10_ind++;
                            }
                        }
                    }
                }

                // NegDW Flow Separated Plot
                NPointSeries DH_Scatter_Series = new NPointSeries(); // scatter of negative DW coeffs and P10 expo
                DH_Scatter_Series.DataLabelStyle.Visible = false;
                DH_Scatter_Series.BorderStyle.Color = Color.OrangeRed;
                DH_Scatter_Series.BorderStyle.Pattern = LinePattern.Dash;
                DH_Scatter_Series.Size = new NLength(2, NRelativeUnit.ParentPercentage);
                DH_Scatter_Series.UseXValues = true;
                thisInst.cht_Downhill_Nev.Charts[0].Series.Add(DH_Scatter_Series);

                NLineSeries DH_Default_Series = new NLineSeries();  // line of default model
                DH_Default_Series.DataLabelStyle.Visible = false;
                DH_Default_Series.BorderStyle.Color = Color.Red;
                DH_Default_Series.BorderStyle.Pattern = LinePattern.Solid;
                DH_Default_Series.MarkerStyle.Visible = false;
                DH_Default_Series.UseXValues = true;
                thisInst.cht_Downhill_Nev.Charts[0].Series.Add(DH_Default_Series);

                if (WD_Ind < numWD)
                {
                    NLineSeries DH_Calib_Series = new NLineSeries(); // line of site-calibrated model
                    DH_Calib_Series.DataLabelStyle.Visible = false;
                    DH_Calib_Series.BorderStyle.Color = Color.Blue;
                    DH_Calib_Series.BorderStyle.Pattern = LinePattern.Solid;
                    DH_Calib_Series.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                    DH_Calib_Series.Name = "Site-Calibrated model";
                    DH_Calib_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(DH_Calib_Series.Name);
                    DH_Calib_Series.UseXValues = true;
                    DH_Calib_Series.MarkerStyle.Visible = false;

                    DH_Calib_Series.UseXValues = true;
                    DH_Calib_Series.XValues.Add(5);
                    DH_Calib_Series.Values.Add(thisModel.sep_A_DW[WD_Ind] * (double)Math.Pow(50, thisModel.sep_B_DW[WD_Ind]));
                    DH_Calib_Series.XValues.Add(200);
                    DH_Calib_Series.Values.Add(thisModel.sep_A_DW[WD_Ind] * (double)Math.Pow(200, thisModel.sep_B_DW[WD_Ind]));
                    thisInst.cht_Downhill_Nev.Charts[0].Series.Add(DH_Calib_Series);
                }

                if (thisModel.isImported == false)
                    DH_Scatter_Series.Name = "DW < 0";
                else
                    DH_Scatter_Series.Name = "Separated Imported";

                DH_Scatter_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(DH_Scatter_Series.Name);

                for (int i = 0; i < negDW_CoeffCount; i++)
                {
                    DH_Scatter_Series.XValues.Add(P10_Expo[i]);
                    DH_Scatter_Series.Values.Add(Math.Abs(negDW_Coeffs[i]));
                }

                DH_Default_Series.Name = "Flow Sep Orig";
                DH_Default_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(DH_Default_Series.Name);
                DH_Default_Series.XValues.Add(50);
                DH_Default_Series.Values.Add(origModel.sep_A_DW[0] * Math.Pow(50, origModel.sep_B_DW[0]));
                DH_Default_Series.XValues.Add(200);
                DH_Default_Series.Values.Add(origModel.sep_A_DW[0] * Math.Pow(200, origModel.sep_B_DW[0]));

                string Xlabel1 = "ln P10 Exposure";
                string yLabel1 = "ln coeff";

                thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator = new NLogarithmicScaleConfigurator();
                thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "ln P10 Exposure";
                thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator = new NLogarithmicScaleConfigurator();
                thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = "ln coeff";
                thisInst.cht_Downhill_Nev.Labels.AddHeader(yLabel1 + " vs " + Xlabel1 + " for Downhill Flow");
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
                NPointSeries posDW_ScatterSeries = new NPointSeries(); // scatter of negative DW>0 coeffs and P10 expo
                posDW_ScatterSeries.DataLabelStyle.Visible = false;
                posDW_ScatterSeries.MarkerStyle.FillStyle = new NColorFillStyle(Color.OrangeRed);
                posDW_ScatterSeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
                posDW_ScatterSeries.FillStyle = new NColorFillStyle(Color.OrangeRed);
                posDW_ScatterSeries.BorderStyle.Color = Color.Black;
                posDW_ScatterSeries.UseXValues = true;
                thisInst.cht_Downhill_Nev.Charts[0].Series.Add(posDW_ScatterSeries);

                NPointSeries negUW_ScatterSeries = new NPointSeries(); // scatter of UW<0 coeffs and P10 expo
                negUW_ScatterSeries.DataLabelStyle.Visible = false;
                negUW_ScatterSeries.FillStyle = new NColorFillStyle(Color.BlueViolet);
                negUW_ScatterSeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
                negUW_ScatterSeries.BorderStyle.Width = new NLength(0.3f, NRelativeUnit.ParentPercentage);
                negUW_ScatterSeries.UseXValues = true;
                thisInst.cht_Downhill_Nev.Charts[0].Series.Add(negUW_ScatterSeries);

                NLineSeries DH_Default_Series = new NLineSeries(); // line of default model
                DH_Default_Series.DataLabelStyle.Visible = false;
                DH_Default_Series.BorderStyle.Color = Color.Red;
                DH_Default_Series.BorderStyle.Pattern = LinePattern.Solid;
                DH_Default_Series.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                DH_Default_Series.MarkerStyle.Visible = false;
                DH_Default_Series.UseXValues = true;
                thisInst.cht_Downhill_Nev.Charts[0].Series.Add(DH_Default_Series);

                if (WD_Ind < numWD)
                {
                    NLineSeries DH_Calib_Series = new NLineSeries(); // line of site-calibrated model
                    DH_Calib_Series.DataLabelStyle.Visible = false;
                    DH_Calib_Series.BorderStyle.Color = Color.Blue;
                    DH_Calib_Series.BorderStyle.Pattern = LinePattern.Solid;
                    DH_Calib_Series.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                    DH_Calib_Series.Name = "Downhill Site-Calibrated model";
                    DH_Calib_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(DH_Calib_Series.Name);
                    DH_Calib_Series.UseXValues = true;
                    DH_Calib_Series.MarkerStyle.Visible = false;

                    DH_Calib_Series.UseXValues = true;
                    DH_Calib_Series.XValues.Add(1);
                    DH_Calib_Series.Values.Add(thisModel.downhill_A[WD_Ind] * Math.Pow(1, thisModel.downhill_B[WD_Ind]));
                    DH_Calib_Series.XValues.Add(100);
                    DH_Calib_Series.Values.Add(thisModel.downhill_A[WD_Ind] * Math.Pow(100, thisModel.downhill_B[WD_Ind]));
                    thisInst.cht_Downhill_Nev.Charts[0].Series.Add(DH_Calib_Series);
                }

                if (thisModel.isImported == false)
                {
                    posDW_ScatterSeries.Name = "DW > 0";
                    posDW_ScatterSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("DW > 0");
                }
                else
                {
                    posDW_ScatterSeries.Name = "Downhill Imported";
                    posDW_ScatterSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("Downhill Imported");
                }

                for (int i = 0; i < posDW_CoeffCount; i++)
                {
                    posDW_ScatterSeries.XValues.Add(P10_Expo_PosDW[i]);
                    posDW_ScatterSeries.Values.Add(posDW_Coeffs[i]);
                }

                if (thisModel.isImported == false)
                {
                    negUW_ScatterSeries.Name = "UW < 0";
                    negUW_ScatterSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("UW < 0");

                    for (int i = 0; i < negUW_CoeffCount; i++)
                    {
                        negUW_ScatterSeries.XValues.Add(P10_Expo_NegUW[i]);
                        negUW_ScatterSeries.Values.Add(negUW_Coeffs[i]);
                    }
                }

                DH_Default_Series.Name = "Downhill Default Model";
                DH_Default_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(DH_Default_Series.Name);
                DH_Default_Series.XValues.Add(1);
                DH_Default_Series.Values.Add(origModel.downhill_A[0] * Math.Pow(1, origModel.downhill_B[0]));
                DH_Default_Series.XValues.Add(100);
                DH_Default_Series.Values.Add(origModel.downhill_A[0] * Math.Pow(100, origModel.downhill_B[0]));

                string Xlabel1 = "ln P10 Exposure";
                string yLabel1 = "ln coeff";

                thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator = new NLogarithmicScaleConfigurator();
                thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "ln P10 Exposure";
                thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator = new NLogarithmicScaleConfigurator();
                thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = "ln coeff";
                thisInst.cht_Downhill_Nev.Labels.AddHeader(yLabel1 + " vs " + Xlabel1 + " for Downhill Flow");
                thisInst.cht_Downhill_Nev.Legends[0].Visible = false;

            }
        }

        public void DownhillRoughPlot(Continuum thisInst, Model thisModel)
        {
            // Updates plot on Advanced tab showing DW Stability factor (used in the surface roughness model) radar plot 
            int numWD = thisInst.GetNumWD();
            double[] DH_Stab = new double[numWD];   // DW Stability factor by WD sector

            Model origModel = new Model();
            origModel.SizeArrays(numWD);
            origModel.SetDefaultModelCoeffs(numWD);

            //  Dim DW_series  ChartDataSeries = series_coll1.AddNewSeries() // scatter of DW>0 coeffs and P10 expo
            //  DW_series.PointData.Length = numWD

            NLineSeries DH_Stab_Series = new NLineSeries();
            DH_Stab_Series.DataLabelStyle.Visible = false;
            DH_Stab_Series.BorderStyle.Color = Color.Blue;
            DH_Stab_Series.BorderStyle.Pattern = LinePattern.Solid;
            DH_Stab_Series.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            DH_Stab_Series.Name = "Downhill Stability Factor";
            DH_Stab_Series.UseXValues = true;
            thisInst.cht_Downhill_Nev.Charts[0].Series.Add(DH_Stab_Series);

            for (int i = 0; i < numWD; i++)
            {
                DH_Stab[i] = thisModel.DH_Stab_A[i];
                //  DW_series.X[i] = i * (360 / numWD)
                // if ( DH_Stab[i] < 5 ) { DW_series.Y[i] = DH_Stab[i]
                DH_Stab_Series.XValues.Add(i * ((double)360 / numWD));
                DH_Stab_Series.Values.Add(DH_Stab[i]);
            }

            // Rough_plot.ChartArea.Visible = true
            // Rough_plot.ChartGroups.Group0.Radar.Filled = true
            // DW_series.SymbolStyle.Shape = C1.Win.C1Chart.SymbolShapeEnum.None
            // DW_series.LineStyle.Pattern = C1.Win.C1Chart.LinePatternEnum.Solid

            string Xlabel1 = "Wind direction, degs";
            string yLabel1 = "Stability factor";

            thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator = new NLinearScaleConfigurator();
            thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator = new NLinearScaleConfigurator();
            thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = Xlabel1;
            thisInst.cht_Downhill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = yLabel1;
            thisInst.cht_Downhill_Nev.Labels.AddHeader("Downhill Stability Factor vs. WD");
            thisInst.cht_Downhill_Nev.Legends[0].Visible = false;

            // Rough_plot.header.Text = yLabel1 & " for Downhill Flow"
            // Rough_plot.header.Visible = true

            // Dim ax1  Axis = Rough_plot.ChartArea.AxisX
            // ax1.Text = ""
            // ax1.IsLogarithmic = false
            // ax1.AnnoFormat = C1.Win.C1Chart.FormatEnum.NumericManual
            // ax1.AnnoFormatString = "#.0"
            // ax1.GridMajor.Visible = true

            //  Dim ay1  Axis = Rough_plot.ChartArea.AxisY
            //  ay1.IsLogarithmic = false
            //  ay1.Text = yLabel1
            //  ay1.AnnoFormatString = "#.0"
            //  ay1.GridMajor.Visible = true
            //// ay1.AutoMin = true
            // ay1.AutoMax = true

            //  Rough_plot.Legend.Visible = false
            //  Rough_plot.ChartGroups.Group0.Radar.Degrees = true
            //  Rough_plot.ChartArea.AxisX.Reversed = true

        }

        public void UphillRoughPlot(Continuum thisInst, Model thisModel, string UW_to_show)
        {
            // Updates plot on Advanced tab showing UW Stability factor (used in the surface roughness model) radar plot
            int numWD = thisInst.GetNumWD();
            double[] UH_Stab = new double[numWD];  // UW Stability factor by WD sector

            //Dim UW_series  ChartDataSeries = series_coll1.AddNewSeries() // scatter of UW<0 coeffs and P10 expo
            //  UW_series.PointData.Length = numWD

            NLineSeries UH_Stab_Series = new NLineSeries();
            UH_Stab_Series.DataLabelStyle.Visible = false;
            UH_Stab_Series.BorderStyle.Color = Color.Blue;
            UH_Stab_Series.BorderStyle.Pattern = LinePattern.Solid;
            UH_Stab_Series.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            UH_Stab_Series.Name = "Uphill Stability Factor";
            UH_Stab_Series.UseXValues = true;
            thisInst.cht_Uphill_Nev.Charts[0].Series.Add(UH_Stab_Series);

            for (int i = 0; i < numWD; i++)
            {
                //   UW_series.X[i] = i * (360 / numWD)
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

                //   UW_series.Y[i] = UH_Stab[i]
                UH_Stab_Series.XValues.Add(i * ((double)360 / numWD));
                UH_Stab_Series.Values.Add(UH_Stab[i]);
            }

            //  UW_series.SymbolStyle.Shape = C1.Win.C1Chart.SymbolShapeEnum.None
            //  UW_series.LineStyle.Pattern = C1.Win.C1Chart.LinePatternEnum.Solid

            string Xlabel1 = "Stability factor";
            string yLabel1 = "Wind direction, degs";

            thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator = new NLinearScaleConfigurator();
            thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator = new NLinearScaleConfigurator();
            thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = yLabel1;
            thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = Xlabel1;
            thisInst.cht_Uphill_Nev.Legends[0].Visible = false;
            thisInst.cht_Uphill_Nev.Labels.AddHeader("Uphill Stability Factor vs. WD");

            //  thisInst.cht_Uphill.header.Visible = true

            //  Dim ax1  Axis = thisInst.cht_Uphill.ChartArea.AxisX
            //  ax1.Text = ""
            //  ax1.IsLogarithmic = false
            //   ax1.AnnoFormat = C1.Win.C1Chart.FormatEnum.NumericManual
            //  ax1.AnnoFormatString = "#.0"
            //  ax1.GridMajor.Visible = true

            //  Dim ay1  Axis = thisInst.cht_Uphill.ChartArea.AxisY
            //  ay1.IsLogarithmic = false
            //  ay1.AnnoFormatString = "#.0"
            // ay1.GridMajor.Visible = true
            // ay1.AutoMax = true
            // ay1.AutoMin = true
            //  ay1.Visible = true

            ////   thisInst.cht_Uphill.Legend.Visible = false
            //   thisInst.cht_Uphill.ChartGroups.Group0.Radar.Degrees = true
            //   thisInst.cht_Uphill.ChartArea.AxisX.Reversed = true

        }

        public void UphillLogLogPlot(Continuum thisInst, Model thisModel, string UW_plot_to_show)
        {
            //  Updates Log-Log plot : Uphill coeff vs P10 Expo on Advanced tab
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
                            negDW_Coeffs[i] = thisModel.uphill_A[WD_Ind] * (double)Math.Pow(P10_Expo_NegDW[i], thisModel.uphill_B[WD_Ind]);
                    }
                    else
                    {
                        P10_Expo_NegDW[0] = 1;
                        P10_Expo_NegDW[1] = thisModel.UW_crit[WD_Ind] / 4;
                        P10_Expo_NegDW[2] = thisModel.UW_crit[WD_Ind] / 2;
                        P10_Expo_NegDW[3] = thisModel.UW_crit[WD_Ind] * 3 / 4;
                        P10_Expo_NegDW[4] = thisModel.UW_crit[WD_Ind];

                        for (int i = 0; i <= 4; i++)
                            negDW_Coeffs[i] = thisModel.spdUp_A[WD_Ind] * (double)Math.Pow(P10_Expo_NegDW[i], thisModel.spdUp_B[WD_Ind]);
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

                                negDW_Coeffs[P10_ind] = thisModel.uphill_A[j] * (double)Math.Pow(P10_Expo_NegDW[P10_ind], thisModel.uphill_B[j]);
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

                NPointSeries UH_UWcrit_Scatter_Series = new NPointSeries(); // scatter of UW>UWcrit coeffs and P10 expo
                UH_UWcrit_Scatter_Series.DataLabelStyle.Visible = false;
                UH_UWcrit_Scatter_Series.FillStyle = new NColorFillStyle(Color.BlueViolet);
                UH_UWcrit_Scatter_Series.UseXValues = true;
                UH_UWcrit_Scatter_Series.Size = new NLength(2, NRelativeUnit.ParentPercentage);
                thisInst.cht_Uphill_Nev.Charts[0].Series.Add(UH_UWcrit_Scatter_Series);

                NPointSeries UH_DW0_Scatter_Series = new NPointSeries(); // scatter of DW<0 coeffs and P10 expo
                UH_DW0_Scatter_Series.DataLabelStyle.Visible = false;
                UH_DW0_Scatter_Series.FillStyle = new NColorFillStyle(Color.Red);
                UH_DW0_Scatter_Series.Size = new NLength(2, NRelativeUnit.ParentPercentage);
                UH_DW0_Scatter_Series.UseXValues = true;

                thisInst.cht_Uphill_Nev.Charts[0].Series.Add(UH_DW0_Scatter_Series);

                NLineSeries UH_Default_Series = new NLineSeries(); // line of default model
                UH_Default_Series.DataLabelStyle.Visible = false;
                UH_Default_Series.BorderStyle.Color = Color.Red;
                UH_Default_Series.BorderStyle.Pattern = LinePattern.Solid;
                UH_Default_Series.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                UH_Default_Series.MarkerStyle.Visible = false;
                UH_Default_Series.UseXValues = true;
                UH_Default_Series.Name = "Default Uphill model";
                UH_Default_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(UH_Default_Series.Name);
                thisInst.cht_Uphill_Nev.Charts[0].Series.Add(UH_Default_Series);

                if (thisModel.isImported == false)
                {
                    UH_DW0_Scatter_Series.Name = "DW < 0";
                    UH_UWcrit_Scatter_Series.Name = "UW > UW crit";
                }
                else
                {
                    UH_DW0_Scatter_Series.Name = "DW < 0 Imported";
                    UH_UWcrit_Scatter_Series.Name = "UW > UW crit Imported";
                }

                UH_UWcrit_Scatter_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(UH_UWcrit_Scatter_Series.Name);
                UH_DW0_Scatter_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(UH_DW0_Scatter_Series.Name);

                if (WD_Ind < numWD)
                {
                    NLineSeries UH_Calib_Series = new NLineSeries(); // line of default model
                    UH_Calib_Series.DataLabelStyle.Visible = false;
                    UH_Calib_Series.BorderStyle.Color = Color.Blue;
                    UH_Calib_Series.BorderStyle.Pattern = LinePattern.Solid;
                    UH_Calib_Series.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                    UH_Calib_Series.MarkerStyle.Visible = false;
                    UH_Calib_Series.UseXValues = true;
                    UH_Calib_Series.XValues.Add(0.1);
                    UH_Calib_Series.Values.Add(thisModel.uphill_A[WD_Ind] * Math.Pow(0.1, thisModel.uphill_B[WD_Ind]));
                    UH_Calib_Series.XValues.Add(100);
                    UH_Calib_Series.Values.Add(thisModel.uphill_A[WD_Ind] * Math.Pow(100, thisModel.uphill_B[WD_Ind]));
                    UH_Calib_Series.Name = "Uphill Site-Calibrated Model";
                    UH_Calib_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(UH_Calib_Series.Name);
                    thisInst.cht_Uphill_Nev.Charts[0].Series.Add(UH_Calib_Series);
                }

                for (int i = 0; i < posUW_CoeffCount; i++)
                {
                    UH_UWcrit_Scatter_Series.XValues.Add(P10_Expo_PosUW[i]);
                    UH_UWcrit_Scatter_Series.Values.Add(posUW_Coeffs[i]);
                }

                for (int i = 0; i < negDW_CoeffCount; i++)
                {
                    UH_DW0_Scatter_Series.XValues.Add(P10_Expo_NegDW[i]);
                    UH_DW0_Scatter_Series.Values.Add(negDW_Coeffs[i]);
                }

                UH_Default_Series.XValues.Add(0.1);
                UH_Default_Series.Values.Add(origModel.uphill_A[0] * Math.Pow(0.1, origModel.uphill_B[0]));
                UH_Default_Series.XValues.Add(100);
                UH_Default_Series.Values.Add(origModel.uphill_A[0] * Math.Pow(100, origModel.uphill_B[0]));

                string Xlabel2 = "ln P10 Exposure";
                string Ylabel2 = "ln coeff";

                thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator = new NLogarithmicScaleConfigurator();
                thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "ln P10 Exposure";
                thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator = new NLogarithmicScaleConfigurator();
                thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = "ln coeff";
                thisInst.cht_Uphill_Nev.Labels.AddHeader(Ylabel2 + " vs " + Xlabel2 + " for Uphill Flow");
            }
            else if (UW_plot_to_show == "UW < UW crit")
            {
                // Pos UW expo, UW < UW Critical
                NPointSeries UH_Scatter_Series = new NPointSeries(); // scatter of UW<UWcrit coeffs and P10 expo
                UH_Scatter_Series.DataLabelStyle.Visible = false;
                UH_Scatter_Series.FillStyle = new NColorFillStyle(Color.BlueViolet);
                UH_Scatter_Series.Size = new NLength(2, NRelativeUnit.ParentPercentage);
                UH_Scatter_Series.UseXValues = true;
                UH_Scatter_Series.Name = "UW < UW crit";
                UH_Scatter_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(UH_Scatter_Series.Name);
                thisInst.cht_Uphill_Nev.Charts[0].Series.Add(UH_Scatter_Series);

                NLineSeries UH_Default_Series = new NLineSeries(); // line of default model
                UH_Default_Series.DataLabelStyle.Visible = false;
                UH_Default_Series.BorderStyle.Color = Color.Red;
                UH_Default_Series.BorderStyle.Pattern = LinePattern.Solid;
                UH_Default_Series.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                UH_Default_Series.MarkerStyle.Visible = false;
                UH_Default_Series.UseXValues = true;
                UH_Default_Series.Name = "Default Speed-Up Model";
                UH_Default_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(UH_Default_Series.Name);
                UH_Default_Series.XValues.Add(0.1);
                UH_Default_Series.Values.Add(origModel.spdUp_A[0] * Math.Pow(0.1, origModel.spdUp_B[0]));
                UH_Default_Series.XValues.Add(100);
                UH_Default_Series.Values.Add(origModel.spdUp_A[0] * Math.Pow(100, origModel.spdUp_B[0]));
                thisInst.cht_Uphill_Nev.Charts[0].Series.Add(UH_Default_Series);

                if (WD_Ind < numWD)
                {
                    NLineSeries UH_Calib_Series = new NLineSeries(); // line of site-calibrated speed-up model
                    UH_Calib_Series.DataLabelStyle.Visible = false;
                    UH_Calib_Series.BorderStyle.Color = Color.Blue;
                    UH_Calib_Series.BorderStyle.Pattern = LinePattern.Solid;
                    UH_Calib_Series.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                    UH_Calib_Series.MarkerStyle.Visible = false;
                    UH_Calib_Series.UseXValues = true;
                    UH_Calib_Series.Name = "Site-Calibrated Speed-Up Model";
                    UH_Calib_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(UH_Calib_Series.Name);
                    UH_Calib_Series.XValues.Add(0.1);
                    UH_Calib_Series.Values.Add(thisModel.spdUp_A[WD_Ind] * Math.Pow(0.1, thisModel.spdUp_B[WD_Ind]));
                    UH_Calib_Series.XValues.Add(100);
                    UH_Calib_Series.Values.Add(thisModel.spdUp_A[WD_Ind] * Math.Pow(100, thisModel.spdUp_B[WD_Ind]));
                    thisInst.cht_Uphill_Nev.Charts[0].Series.Add(UH_Calib_Series);
                }

                for (int i = 0; i < posUW_CoeffCount; i++)
                {
                    UH_Scatter_Series.XValues.Add(P10_Expo_PosUW[i]);
                    UH_Scatter_Series.Values.Add(posUW_Coeffs[i]);
                }

                string Xlabel2 = "ln P10 Exposure";
                string Ylabel2 = "ln coeff";

                thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator = new NLogarithmicScaleConfigurator();
                thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = Xlabel2;
                thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator = new NLogarithmicScaleConfigurator();
                thisInst.cht_Uphill_Nev.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = Ylabel2;
                thisInst.cht_Uphill_Nev.Labels.AddHeader(Ylabel2 + " vs " + Xlabel2 + " for Speed-up");
                thisInst.cht_Uphill_Nev.Labels[0].Size = new NSizeL(1, 1);

            }

            thisInst.cht_Uphill_Nev.Legends[0].Visible = false;
        }

        public void ModelPlots(Continuum thisInst)
        {
            // Reads the selected radius, WD, Expo/SRDH, UW critical and updates the plots on the Advanced tab to show either the log-log P10 exposure or stability factors on a radar plot             
            int numRadii = thisInst.radiiList.ThisCount;

            NChartControl DH_Ctl = thisInst.cht_Downhill_Nev;
            NChartControl UH_Ctl = thisInst.cht_Uphill_Nev;
            DH_Ctl.Charts[0].Series.Clear();
            UH_Ctl.Charts[0].Series.Clear();
            DH_Ctl.Labels.Clear();
            UH_Ctl.Labels.Clear();
            DH_Ctl.Legends[0].Visible = false;
            UH_Ctl.Legends[0].Visible = false;

            DH_Ctl.Controller.Tools.Clear();
            UH_Ctl.Controller.Tools.Clear();

            NTooltipTool DH_ToolTip = new NTooltipTool();
            DH_Ctl.Controller.Tools.Add(DH_ToolTip);
            NTooltipTool UH_ToolTip = new NTooltipTool();
            UH_Ctl.Controller.Tools.Add(UH_ToolTip);

            int numModels = thisInst.modelList.ModelCount;
            bool isImported = false;

            if (numModels > 0)
                if (thisInst.modelList.models[0,0].isImported)
                    isImported = true;

            if (isImported == false && (numModels == 0 || thisInst.metList.expoIsCalc == false))
            {
                DH_Ctl.Refresh();
                UH_Ctl.Refresh();
                thisInst.txtUWCrit.Text = "";
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

            if (thisModel == null)
                return;

            string UH_Plot_to_show = "";

            try
            {
                UH_Plot_to_show = thisInst.cboUphill_to_show.SelectedItem.ToString();
            }
            catch
            {
                thisInst.cboUphill_to_show.SelectedIndex = 0;
                UH_Plot_to_show = thisInst.cboUphill_to_show.SelectedItem.ToString();
            }

            string DH_Plot_to_show = "";

            try
            {
                DH_Plot_to_show = thisInst.cboDHplot.SelectedItem.ToString();
            }
            catch
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

            try
            {
                Expo_or_Rough = thisInst.cboExpo_or_Stab.SelectedItem.ToString();
            }
            catch
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

            DH_Ctl.Refresh();
            UH_Ctl.Refresh();

        }

        public string GetSelectedTopoMapParam(Continuum thisInst) // Returns either ‘Topography’ or ‘Surface roughness’ depending what is selected on ‘Input’ tab
        {
            string topoOrRough = "";

            try
            {
                topoOrRough = thisInst.cboTopo_Or_Roughness.SelectedItem.ToString();
            }
            catch
            {
                topoOrRough = "Topography";
            }

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

        public double GetTopoMapMin(Continuum thisInst, double[,] paramToPlot)
        {
            // Returns minimum of paramToPlot[,] or reads and returns minimum elevation value entered on Input tab

            double thisMin = 0;
            if (thisInst.txtMainMin.Text == "" || thisInst.chkMainAuto.Checked == true)
            {
                if (paramToPlot == null)
                    thisMin = thisInst.topo.GetMin(thisInst.topo.topoElevs, true);
                else
                    thisMin = thisInst.topo.GetMin(paramToPlot, true);

                thisInst.txtMainMin.Text = Math.Round(thisMin, 0).ToString();
            }
            else
            {
                try
                {
                    thisMin = Convert.ToSingle(thisInst.txtMainMin.Text);
                }
                catch
                {

                    if (paramToPlot == null)
                        thisMin = thisInst.topo.GetMin(thisInst.topo.topoElevs, true);
                    else
                        thisMin = thisInst.topo.GetMin(paramToPlot, true);

                    thisInst.txtMainMin.Text = Math.Round(thisMin, 0).ToString();
                }
            }

            return thisMin;
        }

        public double GetTopoMapMax(Continuum thisInst, double[,] paramToPlot)
        {
            // Returns maximum of paramToPlot[,] or reads and returns maximum elevation value entered on Input tab
            double thisMax = 0;
            if (thisInst.txtMainMax.Text == "" || thisInst.chkMainAuto.Checked == true)
            {
                if (paramToPlot == null)
                    thisMax = thisInst.topo.GetMax(thisInst.topo.topoElevs);
                else
                    thisMax = thisInst.topo.GetMax(paramToPlot);

                thisInst.txtMainMax.Text = Math.Round(thisMax, 0).ToString();
            }
            else
            {
                try
                {
                    thisMax = Convert.ToSingle(thisInst.txtMainMax.Text);
                    double thisMin = GetTopoMapMin(thisInst, paramToPlot);
                    if (thisMax <= thisMin)
                    {
                        thisMax = thisMax + 1;
                        thisInst.txtMainMax.Text = thisMax.ToString();
                    }
                }
                catch
                {
                    if (paramToPlot == null)
                        thisMax = thisInst.topo.GetMax(thisInst.topo.topoElevs);
                    else
                        thisMax = thisInst.topo.GetMax(paramToPlot);

                    thisInst.txtMainMax.Text = Math.Round(thisMax, 4).ToString();
                }
            }

            return thisMax;
        }

        public double GetTopoMapInt(Continuum thisInst, double thisMin, double thisMax)
        {
            // Returns contour interval value entered on Input tab
            double intWidth = 1;
            int newNumLevels = 10;

            if (thisInst.txtMainInt.Text == "" || thisInst.chkMainAuto.Checked == true)
            {
                intWidth = (thisMax - thisMin) / (newNumLevels - 1);
                thisInst.txtMainInt.Text = Math.Round(intWidth, 2).ToString();
            }
            else
            {
                try
                {
                    intWidth = Convert.ToSingle(thisInst.txtMainInt.Text);

                    if (intWidth <= 0)
                    {
                        intWidth = (thisMax - thisMin) / (newNumLevels - 1);
                        thisInst.txtMainInt.Text = intWidth.ToString();
                    }
                }
                catch
                {
                    intWidth = (thisMax - thisMin) / (newNumLevels - 1);
                    thisInst.txtMainInt.Text = intWidth.ToString();
                }
            }

            if (intWidth == 0 || thisMin == thisMax)
            {
                thisMin = thisMin * 0.98f;
                thisMax = thisMax * 1.02f;
                intWidth = (thisMax - thisMin) / (newNumLevels - 1);
            }

            return intWidth;
        }

        public Color GetRGB_Values(double frac)
        {
            // Returns Color (RGB) based on index for contour plot
            int red = 0;
            int green = 0;
            int blue = 0;

            if (frac < 0.2)
                red = 255;
            else if (frac >= 0.2 && frac < 0.4)
                red = (int)(-720 * frac + 393);
            else if (frac >= 0.4 && frac < 0.75)
                red = 96;
            else
                red = (int)(637.2 * frac - 383.8);

            if (frac < 0.25)
                green = (int)(616.8 * frac + 106.9);
            else if (frac >= 0.25 && frac < 0.6)
                green = 252;
            else if (frac >= 0.6 && frac < 0.8)
                green = (int)(-640.8 * frac + 619.9);
            else
                green = 97;

            if (frac < 0.4)
                blue = 94;
            else if (frac >= 0.4 && frac < 0.6)
                blue = (int)(852 * frac - 250.67);
            else
                blue = 251;

            if (red < 0) red = 0;
            if (green < 0) green = 0;
            if (blue < 0) blue = 0;

            if (red > 255) red = 255;
            if (green > 255) green = 255;
            if (blue > 255) blue = 255;

            Color theseRGB = Color.FromArgb(red, green, blue);

            return theseRGB;

        }

        public void TopoMap(Continuum thisInst)
        {
            // Updates map on Input tab to show either elevation, SR, DH or LC
            string topoOrRough = GetSelectedTopoMapParam(thisInst);
            if (thisInst.cht_NevTopo.Charts.Count > 0)
                thisInst.cht_NevTopo.Charts[0].Series.Clear();

            if (thisInst.topo.gotTopo || thisInst.topo.gotSR == true)
            {
                // Create plot model
                thisInst.plotTopo.Model = new PlotModel();
                var model = thisInst.plotTopo.Model;
                model.LegendPosition = LegendPosition.RightMiddle;
                model.IsLegendVisible = true;                

                // Create heat map series
                var cs = new HeatMapSeries
                {
                    X0 = thisInst.topo.topoNumXY.X.plot.min,
                    X1 = thisInst.topo.topoNumXY.X.plot.max,
                    Y0 = thisInst.topo.topoNumXY.Y.plot.min,
                    Y1 = thisInst.topo.topoNumXY.Y.plot.max,   
                    
                    Data = thisInst.topo.topoElevs
                };

                model.Axes.Add(new OxyPlot.Axes.LinearColorAxis
                {
                    Position = OxyPlot.Axes.AxisPosition.Right,
                    Palette = OxyPalettes.Jet(500),
                    HighColor = OxyColors.Red,
                    LowColor = OxyColors.Gray,
                    Minimum = thisInst.topo.GetMin(thisInst.topo.topoElevs, false),
                    MinimumPadding = 10
                }) ;
                
                thisInst.plotTopo.Model.Series.Add(cs);
                
                // Create label series
                var ms = new ScatterSeries();
                ms.BinSize = 8;
                ms.MarkerSize = 5;
                ms.MarkerStrokeThickness = 1;
                ms.MarkerType = MarkerType.Circle;
                ms.MarkerFill = OxyColors.Black;
                ms.MarkerStroke = OxyColors.Red;
                ms.Title = "Met 1";
                
                ms.Points.Add(new ScatterPoint(thisInst.metList.metItem[0].UTMX, thisInst.metList.metItem[0].UTMY, 5, thisInst.metList.metItem[0].elev));
                
                model.Series.Add(ms);

                // Refresh plot
                thisInst.plotTopo.Refresh();

                NChart topoMap = thisInst.cht_NevTopo.Charts[0];
                topoMap.Projection.SetPredefinedProjection(PredefinedProjection.OrthogonalTop);
                topoMap.Series.Clear();
                topoMap.Enable3D = true;
                topoMap.Width = 85f;
                topoMap.Depth = 85f;
                topoMap.Height = 0.001f;
                
                thisInst.cht_NevTopo.Legends.Clear();
                thisInst.cht_NevTopo.Controller.Tools.Clear();
                NTooltipTool tooltip = new NTooltipTool();
                thisInst.cht_NevTopo.Controller.Tools.Add(tooltip);
                thisInst.cht_NevTopo.Controller.Tools.Add(new NSelectorTool());
                // thisInst.cht_NevTopo.Controller.Tools.Add(new NTrackballTool())
                //thisInst.cht_NevTopo.Controller.Tools.Add(new NCustomTooltipTool());

                NMeshSurfaceSeries topoSurface = new NMeshSurfaceSeries();
                double[,] paramToPlot = null;
                int numX_Plot;
                int numY_Plot;

                if ((topoOrRough == "Surface Roughness" || topoOrRough == "Displacement height" || topoOrRough == "Land Cover") && thisInst.topo.gotSR == true)
                {
                    // Plot land cover, surface roughness, or displacement height
                    if (topoOrRough == "Surface Roughness") paramToPlot = thisInst.topo.GetLC_ParamToPlot("Surface Roughness");
                    if (topoOrRough == "Displacement height") paramToPlot = thisInst.topo.GetLC_ParamToPlot("Displacement height");
                    if (topoOrRough == "Land Cover") paramToPlot = thisInst.topo.GetLC_ParamToPlot("Land Cover");

                    if (paramToPlot == null)
                        return;

                    numX_Plot = paramToPlot.GetUpperBound(0);
                    numY_Plot = paramToPlot.GetUpperBound(1);
                }
                else if ((topoOrRough == "Topography" && thisInst.topo.gotTopo == true))
                { // Topography plot
                    numX_Plot = thisInst.topo.topoNumXY.X.plot.num;
                    numY_Plot = thisInst.topo.topoNumXY.Y.plot.num;
                }
                else
                    return;

                topoSurface.Data.SetGridSize(numX_Plot, numY_Plot);

                int newNumLevels = 10;
                double thisMin = GetTopoMapMin(thisInst, paramToPlot);
                double thisMax = GetTopoMapMax(thisInst, paramToPlot);
                double intWidth = GetTopoMapInt(thisInst, thisMin, thisMax);

                if (intWidth == 0)
                    newNumLevels = 1;
                else
                    newNumLevels = Convert.ToInt16((thisMax - thisMin) / intWidth + 1);

                topoSurface.FillMode = SurfaceFillMode.Zone;
                topoSurface.FrameMode = SurfaceFrameMode.Contour;
                topoSurface.DrawFlat = true;

                topoSurface.Palette.Clear();
                topoSurface.Palette.SmoothPalette = false;

                if ((topoOrRough == "Surface Roughness"))
                    topoSurface.ValueFormatter.FormatSpecifier = "0.000";
                else
                    topoSurface.ValueFormatter.FormatSpecifier = "0";

                topoSurface.Palette.HasCustomMin = true;
                topoSurface.Palette.CustomMin = thisMin - thisMin * 0.05;
                topoSurface.Palette.Mode = PaletteMode.Custom;
                topoSurface.Palette.PaletteSteps = newNumLevels;

                for (int i = 0; i < newNumLevels; i++)
                {
                    if (topoOrRough == "Surface Roughness")
                        topoSurface.Palette.Add(thisMin + i * intWidth, GetRGB_Values((double)i / newNumLevels));
                    else
                        topoSurface.Palette.Add(Math.Round(thisMin + i * intWidth, 0), GetRGB_Values((double)i / newNumLevels));
                }

                topoSurface.Legend.Mode = SeriesLegendMode.SeriesLogic;
                topoSurface.Legend.Format = "<zone_begin> - <zone_end>";
                
                
                NLegend Topo_Legend = new NLegend();
                Topo_Legend.UseAutomaticSize = true;
                thisInst.cht_NevTopo.Charts[0].DisplayOnLegend = Topo_Legend;
                thisInst.cht_NevTopo.Panels.Add(Topo_Legend);
                
                Topo_Legend.DockMode = PanelDockMode.Right;

                for (int i = 0; i < numX_Plot; i++)
                {
                    for (int j = 0; j < numY_Plot; j++)
                    {
                        if ((topoOrRough == "Surface Roughness" || topoOrRough == "Displacement height" || topoOrRough == "Land Cover"))
                        {
                            int thisX = Convert.ToInt32(thisInst.topo.LC_NumXY.X.plot.min + i * thisInst.topo.LC_NumXY.X.plot.reso);
                            int thisY = (int)(thisInst.topo.LC_NumXY.Y.plot.min + j * thisInst.topo.LC_NumXY.Y.plot.reso);
                            topoSurface.Data.SetValue(i, j, paramToPlot[i, j], thisX, thisY);                            
                        }
                        else
                        {
                            int thisX = Convert.ToInt32(thisInst.topo.topoNumXY.X.all.min + i * thisInst.topo.topoNumXY.X.plot.reso);
                            int thisY = (int)(thisInst.topo.topoNumXY.Y.all.min + j * thisInst.topo.topoNumXY.Y.plot.reso);
                            topoSurface.Data.SetValue(i, j, Math.Round(thisInst.topo.topoElevs[i, j], 1), thisX, thisY);
                        }
                    }
                }

                topoMap.Series.Add(topoSurface);
                Labels(thisInst);
                //  thisInst.cht_NevTopo.Controller.Tools.Add(new NCustomTooltipTool);
            }
            else
                thisInst.cht_NevTopo.Charts[0].Series.Clear();

            thisInst.cht_NevTopo.Refresh();
        }

        public void StepTopoMap(Continuum thisInst)
        {
            // Updates the map on Advanced tab to show the selected start and end met and the nodes in between (if any)
            NChart stepTopoMap = thisInst.chtTopoStep_Nev.Charts[0];
            stepTopoMap.Projection.SetPredefinedProjection(PredefinedProjection.OrthogonalTop);
            stepTopoMap.Series.Clear();
            stepTopoMap.Enable3D = true;
            stepTopoMap.Width = 70f;
            stepTopoMap.Depth = 70f;
            stepTopoMap.Height = 0.001f;
            
            thisInst.chtTopoStep_Nev.Labels.Clear();
            thisInst.chtTopoStep_Nev.Legends.Clear();

            thisInst.chtTopoStep_Nev.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            thisInst.chtTopoStep_Nev.Controller.Tools.Add(tooltip);

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

                int newNumLevels = 10;
                double intWidth = (thisMax - thisMin) / (newNumLevels - 1);

                NMeshSurfaceSeries topoSurface = new NMeshSurfaceSeries();
                topoSurface.Palette.Clear();
                topoSurface.Palette.SmoothPalette = false;
                topoSurface.Palette.HasCustomMin = true;
                topoSurface.Palette.CustomMin = thisMin - thisMin * 0.05;
                topoSurface.Palette.Mode = PaletteMode.Custom;
                topoSurface.Palette.PaletteSteps = newNumLevels;

                for (int i = 0; i < newNumLevels; i++)
                    topoSurface.Palette.Add(Math.Round(thisMin + i * intWidth, 0), GetRGB_Values((double)i / newNumLevels));

                topoSurface.FillMode = SurfaceFillMode.Zone;
                topoSurface.FrameMode = SurfaceFrameMode.Contour;
                topoSurface.DrawFlat = true;
                topoSurface.ValueFormatter.FormatSpecifier = "0.00";

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
                int numNodes;
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
                else if (thisInst.turbineList.TurbineCount > 0)
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

                    try
                    {
                        numNodes = thisTurb.WS_Estimate[WS_PredInd].pathOfNodes.Length;
                    }
                    catch
                    {
                        numNodes = 0;
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
                    maxX = minX + 2* topo.topoNumXY.X.plot.reso;
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

                topoSurface.Data.SetGridSize(numX, numY);

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


                for (int i = 0; i < numX; i++)
                    for (int j = 0; j < numY; j++)
                        topoSurface.Data.SetValue(i, j, elevsToPlot[i, j], minX + i * topo.topoNumXY.X.plot.reso, minY + j * topo.topoNumXY.Y.plot.reso);

                stepTopoMap.Series.Add(topoSurface);
                StepLabels(thisInst, minX, maxX, minY, maxY);
            }

            thisInst.chtTopoStep_Nev.Refresh();
        }

        public void NewProject(Continuum thisInst)
        {
            // Clears all parameters and resets model for a new project
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

        public void NewModel(Continuum thisInst)
        {
            // Clears everything except for Maps and met & turbine sites (clear WS calcs)
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
            thisInst.chtRR_RMSAll_Nev.Charts[0].Series.Clear();
            thisInst.chtRR_RMSAll_Nev.Refresh();
            thisInst.chtHistoRoundRobin_Nev.Charts[0].Series.Clear();
            thisInst.chtHistoRoundRobin_Nev.Refresh();
            thisInst.chtTurbUncert_Nev.Charts[0].Series.Clear();
            thisInst.chtTurbUncert_Nev.Refresh();
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

        public void ClearSavedParameters(Continuum thisInst) // Clears all saved parameters (Map settings)
        {
            thisInst.savedParams.ClearAll();
        }

        public void ClearStats(Continuum thisInst) // Clears textboxes on Gross Turbine Ests tab showing the stats of the estimated WS & AEP
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

        public void ClearMapsPlotsAndTables(Continuum thisInst) // Clears all plots and table on Map tab
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

        public void WindRose(Continuum thisInst) // Updates wind rose plot on Input tab
        {           

            NChartControl chtWindRose = thisInst.chtWindRose_Nev;
            NTooltipTool tooltip = new NTooltipTool();

            chtWindRose.Controller.Tools.Clear();
            chtWindRose.Controller.Tools.Add(tooltip);

            NRadarChart WR_Chart = new NRadarChart();

            chtWindRose.Charts.Clear();
            chtWindRose.Labels.AddHeader("Wind Rose");
            chtWindRose.Charts.Add(WR_Chart);

            int numWD = thisInst.GetNumWD();
            Met[] checkedMets = thisInst.GetCheckedMets("Input");
            int metCount = checkedMets.Length;

            if (metCount > 0)
            {
                AddAxisToRadarPlot(ref WR_Chart, numWD, true, 10);

                for (int i = 0; i < metCount; i++)
                {
                    Met.WSWD_Dist thisDist = checkedMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                    AddSeriesToRadarPlot(ref WR_Chart, thisDist.windRose, checkedMets[i].name, GetMetOrTurbColor(100 * i / metCount), true, false);
                }

            }

            chtWindRose.Refresh();
        }

        public void AddSeriesToRadarPlot(ref NRadarChart thisRadar, double[] dataToAdd, string seriesName, Color lineColor, bool showMarkers, bool isDashed)
        {
            // Adds a data series to referenced Radar chart
            NRadarLineSeries WR_Series = new NRadarLineSeries();
            thisRadar.Series.Add(WR_Series);
            thisRadar.Name = seriesName;

            for (int i = 0; i < dataToAdd.Length; i++)
            {
                int thisIndex = 0;
                if ((i <= dataToAdd.Length / 2))
                    thisIndex = dataToAdd.Length / 2 - i;
                else
                    thisIndex = dataToAdd.Length + dataToAdd.Length / 2 - i;

                WR_Series.Values.InsertValue(i, dataToAdd[thisIndex]);
            }

            WR_Series.DataLabelStyle.Visible = false;
            WR_Series.DataLabelStyle.Format = "0.0";
            if (showMarkers == true)
                WR_Series.MarkerStyle.Visible = true;
            else
                WR_Series.MarkerStyle.Visible = false;

            WR_Series.MarkerStyle.Width = new NLength(1.5F, NRelativeUnit.ParentPercentage);
            WR_Series.MarkerStyle.Height = new NLength(1.5F, NRelativeUnit.ParentPercentage);
            WR_Series.MarkerStyle.FillStyle = new NColorFillStyle(lineColor);
            WR_Series.FillStyle = new NColorFillStyle(lineColor);
            if (isDashed == true) WR_Series.BorderStyle.Pattern = LinePattern.Dash;
            WR_Series.InteractivityStyle.Tooltip = new NTooltipAttribute(seriesName);

        }

        public void AddAxisToRadarPlot(ref NRadarChart thisRadar, int numWD, bool isVisible, int fontSize)
        {
            // Adds axis to referenced Radar chart
            for (int i = 0; i <= numWD - 1; i++)
            {
                NRadarAxis Axis = new NRadarAxis();

                if (isVisible == true)
                {
                    if ((i <= numWD / 2))
                        Axis.Title = Convert.ToString((numWD / 2 - i) * (double)360 / numWD);
                    else
                        Axis.Title = Convert.ToString((numWD / 2 - i) * (double)360 / numWD + 360);
                }
                else
                    Axis.Title = "";

                NLinearScaleConfigurator linearScale = (NLinearScaleConfigurator)(Axis.ScaleConfigurator);
                linearScale.RulerStyle.BorderStyle.Color = Color.Silver;
                linearScale.InnerMajorTickStyle.LineStyle.Color = Color.Silver;
                linearScale.OuterMajorTickStyle.LineStyle.Color = Color.Silver;
                linearScale.InnerMajorTickStyle.Length = new NLength(2, NGraphicsUnit.Point);
                linearScale.OuterMajorTickStyle.Length = new NLength(2, NGraphicsUnit.Point);

                if ((thisRadar.Axes.Count == 0))
                {
                    // if the first axis then configure grid style && stripes
                    linearScale.MajorGridStyle.LineStyle.Color = Color.Gainsboro;
                    linearScale.MajorGridStyle.LineStyle.Pattern = LinePattern.Dot;
                    linearScale.MajorGridStyle.SetShowAtWall(ChartWallType.Radar, true);

                    // add interlaced stripe
                    NScaleStripStyle strip = new NScaleStripStyle();
                    strip.FillStyle = new NColorFillStyle(Color.FromArgb(64, 200, 200, 200));
                    strip.Interlaced = true;
                    strip.SetShowAtWall(ChartWallType.Radar, true);
                    linearScale.StripStyles.Add(strip);
                }
                else
                {
                    // hide labels
                    linearScale.AutoLabels = false;
                }

                if (isVisible)
                    Axis.Visible = true;
                else
                    Axis.Visible = false;

                Axis.TitleTextStyle.FontStyle.EmSize = new NLength(fontSize);

                thisRadar.Axes.Add(Axis);

            }

        }

        public void DirectionalWS_Ratios(Continuum thisInst) // Updates directional WS ratio plot on Input tab
        {

            NChartControl Dir_cht_Nev = thisInst.chtDirectionalWSRatios_Nev;
            NTooltipTool tooltip = new NTooltipTool();
            Dir_cht_Nev.Controller.Tools.Clear();
            Dir_cht_Nev.Controller.Tools.Add(tooltip);

            NRadarChart Dir_Chart = new NRadarChart();
            Dir_cht_Nev.Charts.Clear();
            Dir_cht_Nev.Legends.Clear();
            Dir_cht_Nev.Labels.AddHeader("Directional WS Ratios");
            Dir_cht_Nev.Charts.Add(Dir_Chart);

            NLegend Dir_Legend = new NLegend();
            Dir_cht_Nev.Legends.Add();
            Dir_Legend = Dir_cht_Nev.Legends[0];

            Met[] checkedMets = thisInst.GetCheckedMets("Input");

            if (checkedMets == null)
            {
                Dir_cht_Nev.Refresh();
                return;
            }

            if (checkedMets.Length > 0)
            {
                int numWD = thisInst.GetNumWD();
                AddAxisToRadarPlot(ref Dir_Chart, numWD, true, 10);

                for (int i = 0; i < checkedMets.Length; i++)
                {
                    Color lineColor = GetMetOrTurbColor(i);
                    Met.WSWD_Dist thisDist = checkedMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                    AddSeriesToRadarPlot(ref Dir_Chart, thisDist.sectorWS_Ratio, checkedMets[i].name, lineColor, true, false);
                }

                double[] ones = new double[numWD];    // Add 1:1 ratio

                for (int i = 0; i < numWD; i++)
                    ones[i] = 1;

                AddSeriesToRadarPlot(ref Dir_Chart, ones, "1:1", Color.Black, false, true);
            }

            Dir_cht_Nev.Refresh();
        }


        public void Uncertainty_TAB_Round_Robin(Continuum thisInst) // Updates tables and plot showing results of Math.Round Robin 
        {
            thisInst.okToUpdate = false;
            RoundRobinDropdown(thisInst);
            thisInst.okToUpdate = true;
            RoundRobinResults(thisInst);
            RoundRobinIndivResults(thisInst);
            RoundRobinHistogram(thisInst);
        }

        public void MapTAB(Continuum thisInst) // Updates tables, plots, and textboxes on Map tab
        {
            MapList(thisInst);
            Generated2DMap(thisInst);
            MapStats(thisInst);
        }


        public void MapList(Continuum thisInst) // Updates the list on Map tab
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

        public void Generated2DMap(Continuum thisInst) //  Updates map on Maps tab
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

                NChart genMap = thisInst.c3DMaps_Nev.Charts[0];
                genMap.Projection.SetPredefinedProjection(PredefinedProjection.OrthogonalTop);
                genMap.Series.Clear();
                genMap.Enable3D = true;
                genMap.Width = 85f;
                genMap.Depth = 85f;
                genMap.Height = 0.001f;
                thisInst.c3DMaps_Nev.Legends.Clear();
                NMeshSurfaceSeries mapSurface = new NMeshSurfaceSeries();
                mapSurface.Data.SetGridSize(thisMap.numX, thisMap.numY);
                mapSurface.FillMode = SurfaceFillMode.Zone;
                mapSurface.FrameMode = SurfaceFrameMode.Contour;
                mapSurface.DrawFlat = true;
                mapSurface.ValueFormatter.FormatSpecifier = "0.00";
                
                mapSurface.Legend.Mode = SeriesLegendMode.SeriesLogic;
                mapSurface.Legend.Format = "<zone_begin> - <zone_end>";

                int newNumLevels = 10;
                double newLevel;
                double intWidth = 0.0f;

                double[] This_Level = new double[newNumLevels];
                double thisMin = 0;
                double thisMax = 0;
                int WD_Ind = thisInst.GetWD_ind("Maps");

                int numWD = thisInst.GetNumWD();

                try
                {
                    thisMin = Convert.ToSingle(thisInst.txtMinValue.Text);
                }
                catch
                {
                    thisMin = thisMap.FindMin(WD_Ind, numWD);
                    thisInst.txtMinValue.Text = Math.Round(thisMin, 3).ToString();
                }

                try
                {
                    thisMax = Convert.ToSingle(thisInst.txtMaxValue.Text);
                }
                catch
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
                    intWidth = (thisMax - thisMin) / (newNumLevels - 1);
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
                        intWidth = (thisMax - thisMin) / (newNumLevels - 1);

                        //  intWidth = Math.Round(intWidth, 2)
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
                        intWidth = (thisMax - thisMin) / (newNumLevels - 1);

                        //  intWidth = Math.Round(intWidth, 1)
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

                        //  intWidth = Math.Round(intWidth, 0)
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

                newNumLevels = Convert.ToInt16((thisMax - thisMin) / intWidth + 1);

                if (newNumLevels <= 0)
                    newNumLevels = 1;

                double[] newLevelArray = new double[newNumLevels];

                for (int i = 0; i < newNumLevels; i++)
                {
                    newLevel = thisMin + intWidth * i;
                    newLevelArray[i] = newLevel;
                }

                mapSurface.Palette.Clear();
                mapSurface.Palette.SmoothPalette = false;
                mapSurface.Palette.HasCustomMin = true;
                mapSurface.Palette.CustomMin = thisMin - thisMin * 0.05f;
                mapSurface.Palette.Mode = PaletteMode.Custom;
                mapSurface.Palette.PaletteSteps = newNumLevels;

                for (int i = 0; i < newNumLevels; i++)
                    mapSurface.Palette.Add(Math.Round(thisMin + i * intWidth, 2), GetRGB_Values((double)i / newNumLevels));

                mapSurface.Legend.Mode = SeriesLegendMode.SeriesLogic;
                mapSurface.Legend.Format = "<zone_begin> - <zone_end>";
                NLegend Map_Legend = new NLegend();
                thisInst.c3DMaps_Nev.Charts[0].DisplayOnLegend = Map_Legend;
                thisInst.c3DMaps_Nev.Panels.Add(Map_Legend);
                Map_Legend.DockMode = PanelDockMode.Right;

                int numX = thisMap.numX;
                int numY = thisMap.numY;

                double[,] param = new double[numX, numY];
                double[,] singPar;

                if (WD_Ind == numWD)
                {
                    singPar = thisMap.parameterToMap;
                }
                else if (thisMap.modelType != 2 && thisMap.modelType != 4 && thisMap.isWaked == false)
                {
                    try
                    {
                        singPar = thisMap.parameterToMap;
                        thisInst.cboMapWD.SelectedIndex = numWD;
                        MessageBox.Show("Sectorwise exposure maps not currently calculated. Resetting to Overall WD", "Continuum 3");
                    }
                    catch
                    {
                        return;
                    }
                }
                else
                {
                    singPar = new double[numX, numY];

                    for (int i = 0; i < numX; i++)
                        for (int j = 0; j < numY; j++)
                            singPar[i, j] = thisMap.sectorParamToMap[i, j, WD_Ind];
                }

                for (int i = 0; i < numX; i++)
                {
                    for (int j = 0; j < numY; j++)
                    {
                        param[i, j] = Convert.ToDouble(singPar[i, j]);
                        mapSurface.Data.SetValue(i, j, param[i, j], thisMap.minUTMX + i * thisMap.reso, thisMap.minUTMY + j * thisMap.reso);
                    }
                }

                genMap.Series.Add(mapSurface);
                MapLabels(thisInst);
            }
            else
            {
                thisInst.c3DMaps_Nev.Charts[0].Series.Clear();
            }

            thisInst.c3DMaps_Nev.Refresh();
        }

        public void MapStats(Continuum thisInst) // Updates textboxes on Map tab showing statistics of selected map
        {
            string Selected_Map = "";

            if (thisInst.lstMaps.SelectedItems.Count == 1)
            {
                Selected_Map = thisInst.lstMaps.SelectedItems[0].Text;

                for (int i = 0; i <= thisInst.mapList.ThisCount - 1; i++)
                {
                    if (Selected_Map == thisInst.mapList.mapItem[i].mapName)
                    {
                        thisInst.mapList.mapItem[i].FindMapStats(thisInst);
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

        public void AdvancedTAB(Continuum thisInst) // Updates all plots and tables on Advanced tab
        {            
            PathNodesList(thisInst); // calls PathNodeListUpdate
            PlotAdvancedTable(thisInst);
            ModelPlots(thisInst);
            ModelParams(thisInst);
            ModCrossPredictions(thisInst);
            StepTopoMap(thisInst);
        }

        public void GetMapSettings(Continuum thisInst) // Reads selected settings on Maps tab
        {
            int colorInd = 0;
            bool auto = false;

            double min;
            double max;
            double interval;

            if (thisInst.chkMainAuto.CheckState == CheckState.Checked)
                auto = true;

            try
            {
                min = Convert.ToSingle(thisInst.txtMainMin.Text);
                max = Convert.ToSingle(thisInst.txtMainMax.Text);
                interval = Convert.ToSingle(thisInst.txtMainInt.Text);
            }
            catch
            {
                return;
            }

            thisInst.savedParams.topoMapColor = colorInd;
            thisInst.savedParams.topoMapAuto = auto;
            thisInst.savedParams.topoMapMin = min;
            thisInst.savedParams.topoMapMax = max;
            thisInst.savedParams.topoMapInterval = interval;

        }

        public void MapSettings(Continuum thisInst) // Updates the textboxes on the Maps tab
        {

            if (thisInst.savedParams.topoMapMin == 0 && thisInst.savedParams.topoMapMax == 0)  // no saved map settings
                thisInst.chkMainAuto.CheckState = CheckState.Checked;
            else
            {
                thisInst.txtMainMin.Text = thisInst.savedParams.topoMapMin.ToString();
                thisInst.txtMainMax.Text = thisInst.savedParams.topoMapMax.ToString();
                thisInst.txtMainInt.Text = Math.Round(thisInst.savedParams.topoMapInterval, 1).ToString();
                if (thisInst.savedParams.topoMapAuto == true)
                    thisInst.chkMainAuto.CheckState = CheckState.Checked;
                else
                    thisInst.chkMainAuto.CheckState = CheckState.Unchecked;
            }
        }

        public void InputTAB(Continuum thisInst)
        {
            // Updates everything on Input_tab (except for MetLists and TurbineLists which is done separately)
            WindRose(thisInst);
            DirectionalWS_Ratios(thisInst);
            LC_KeySelected(thisInst);
            TopoMap(thisInst);
            thisInst.txtTopoSource.Text = thisInst.savedParams.topoText;
        }

        public void GrossTurbineEstsTAB(Continuum thisInst)
        {
            // Updates everything on Gross Turbine Estimates tab
            PowerCurveList(thisInst);
            PowerCrvPlot(thisInst);
            GrossTurbEstList(thisInst);
            GrossHistogram(thisInst);
            TurbStats(thisInst);
            WS_or_WR_Plot(thisInst);
        }

        public void Uncertainty_TAB_Turbine_Ests(Continuum thisInst)
        {
            TurbUncertEstList(thisInst);
            TurbineUncertPlot(thisInst);
        }

        public void MapsTAB(Continuum thisInst)
        {
            MapList(thisInst);
            MapStats(thisInst);
            MapSettings(thisInst);
            Generated2DMap(thisInst);
        }

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

        public void ClearDB(string savedFileName)
        {
            // clears all database tables
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(savedFileName);

            try
            {
                using (var ctx = new Continuum_EDMContainer(connString))
                {
                    ctx.Database.ExecuteSqlCommand("DELETE FROM Topo_table");
                    ctx.Database.ExecuteSqlCommand("DELETE FROM LandCover_table");
                    ctx.Database.ExecuteSqlCommand("DELETE FROM GridStat_table");
                    ctx.Database.ExecuteSqlCommand("DELETE FROM Expo_table");
                    ctx.Database.ExecuteSqlCommand("DELETE FROM Node_table");

                    ctx.SaveChanges();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        public void Clear_LandCover_DB(string savedFileName)
        {
            // clears land cover database tables
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

        public void Clear_Topo_DB(string savedFileName)
        {
            // clears toporaphy database tables
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

            MetQC_AnemDropdown(thisInst, thisMet);
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Updates the Shear alpha vs Wind Direction Plot and Table.  Plots overall, day, and night
        /// shear exponents as function of WD.
        /// </summary>
        ///
        /// <remarks>   Liz, 6/21/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AlphaVsWD_PlotAndTable(Continuum thisInst, Met thisMet)
        {
            Met_Data_Filter thisData = thisMet.metData;


            NChartControl alphaWDPlot = thisInst.chtAlphabyWD;
            //   alphaWDPlot.Charts[0].PlotArea. = new NSizeL(new NLength(160, NGraphicsUnit.Point), new NLength(100, NGraphicsUnit.Point));
            alphaWDPlot.Charts[0].Series.Clear();
            alphaWDPlot.Labels.Clear();
            alphaWDPlot.Controller.Tools.Clear();
            alphaWDPlot.Legends[0].Visible = false;
            NTooltipTool tooltip = new NTooltipTool();
            alphaWDPlot.Controller.Tools.Add(tooltip);

            // Size chart area
            alphaWDPlot.Charts[0].Width = 175;
            alphaWDPlot.Charts[0].Height = 125;
            
            thisInst.lstAlphas.Items.Clear();

            if (thisData == null)
            {
                alphaWDPlot.Refresh();
                return;
            }

            if (thisData.alpha.Length == 0)
            {
                alphaWDPlot.Refresh();
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

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(alphaWDPlot.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Wind Direction, degs";
            scaleConfiguratorX.AutoMinorTicks = false;
            scaleConfiguratorX.MinorTickCount = 15;

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(alphaWDPlot.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "Shear Exponent";

            NLabel chartTitle = alphaWDPlot.Labels.AddHeader("Alpha vs. Wind Direction");

            NLineSeries allHoursSeries = new NLineSeries();
            allHoursSeries.DataLabelStyle.Visible = false;
            allHoursSeries.UseXValues = true;
            //allHoursSeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
            allHoursSeries.BorderStyle.Color = Color.Red;
            allHoursSeries.Name = "All hours";
            alphaWDPlot.Charts[0].Series.Add(allHoursSeries);

            allHoursSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("All hours");

            NLineSeries daySeries = new NLineSeries();
            daySeries.DataLabelStyle.Visible = false;
            daySeries.UseXValues = true;
            // daySeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
            daySeries.BorderStyle.Color = Color.Green;
            daySeries.Name = "Day";
            alphaWDPlot.Charts[0].Series.Add(daySeries);
            daySeries.InteractivityStyle.Tooltip = new NTooltipAttribute("Day");

            NLineSeries nightSeries = new NLineSeries();
            nightSeries.DataLabelStyle.Visible = false;
            nightSeries.UseXValues = true;
            //nightSeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
            nightSeries.BorderStyle.Color = Color.Blue;
            nightSeries.Name = "Night";
            alphaWDPlot.Charts[0].Series.Add(nightSeries);
            nightSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("Night");

            for (int i = 0; i < 16; i++)
            {
                allHoursSeries.XValues.Add(WD_vals[i]);
                allHoursSeries.Values.Add(Avg_Alpha_WD[i]);

                daySeries.XValues.Add(WD_vals[i]);
                daySeries.Values.Add(Day_Alpha_WD[i]);

                nightSeries.XValues.Add(WD_vals[i]);
                nightSeries.Values.Add(Night_Alpha_WD[i]);
            }

            alphaWDPlot.Legends[0].DockMode = PanelDockMode.Right;
            alphaWDPlot.Legends[0].Visible = true;

            thisInst.lstAlphas.Items.Clear();
            ListViewItem lstView = new ListViewItem();

            lstView = thisInst.lstAlphas.Items.Add(Convert.ToString("All"));
            lstView.SubItems.Add(Convert.ToString(Math.Round(Avg_Alpha[0], 3)));
            lstView.SubItems.Add(Convert.ToString(Math.Round(Day_Alpha[0], 3)));
            lstView.SubItems.Add(Convert.ToString(Math.Round(Night_Alpha[0], 3)));

            for (int i = 0; i < 16; i++)
            {
                lstView = thisInst.lstAlphas.Items.Add(Convert.ToString(Math.Round(WD_vals[i], 1)));
                lstView.SubItems.Add(Convert.ToString(Math.Round(Avg_Alpha_WD[i], 3)));
                lstView.SubItems.Add(Convert.ToString(Math.Round(Day_Alpha_WD[i], 3)));
                lstView.SubItems.Add(Convert.ToString(Math.Round(Night_Alpha_WD[i], 3)));
            }

            thisInst.lstAlphas.Columns[0].Width = 40;
            thisInst.lstAlphas.Columns[1].Width = 40;
            thisInst.lstAlphas.Columns[2].Width = 60;
            thisInst.lstAlphas.Columns[3].Width = 60;

            alphaWDPlot.Refresh();

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the wind speed vs height plot. </summary>
        ///
        /// <remarks>   Liz, 10/16/2017. Plots filtered and unfiltered average WS vs. height. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void WS_vsHeightPlot(Continuum thisInst, Met thisMet)
        {
            Met_Data_Filter thisData = thisMet.metData;

            NChartControl WS_ShearCtl = thisInst.chtWS_by_H;
            WS_ShearCtl.Charts[0].Series.Clear();
            WS_ShearCtl.Labels.Clear();
            WS_ShearCtl.Controller.Tools.Clear();
            WS_ShearCtl.Legends[0].Visible = false;

            if (thisData == null)
            {
                WS_ShearCtl.Refresh();
                return;
            }

            int numAnems = thisData.GetNumAnems();
            int numSim = thisData.GetNumSimData();

            double[] heights = new double[numAnems];
            double[] unfiltWS = new double[numAnems];
            double[] filtWS = new double[numAnems];

            double[] extrapH = new double[numSim];
            double[] extrapWS = new double[numSim];

            int minWS = 100;

            for (int i = 0; i < numAnems; i++)
            {
                heights[i] = thisData.anems[i].height;
                unfiltWS[i] = thisData.CalcAvgWS(thisData.anems[i], false);
                filtWS[i] = thisData.CalcAvgWS(thisData.anems[i], true);

                if (unfiltWS[i] < minWS) minWS = (int)unfiltWS[i];
                if (filtWS[i] < minWS) minWS = (int)filtWS[i];
            }

            for (int i = 0; i < numSim; i++)
            {
                extrapH[i] = thisData.simData[i].height;
                extrapWS[i] = thisData.CalcAvgExtrapolatedWS(thisData.simData[i]);
                if (extrapWS[i] < minWS) minWS = (int)extrapWS[i];
            }

            WS_ShearCtl.Legends[0].DockMode = PanelDockMode.Bottom;

            NTooltipTool tooltip = new NTooltipTool();
            WS_ShearCtl.Controller.Tools.Add(tooltip);
            WS_ShearCtl.Labels.AddHeader("WS vs. Height");

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(WS_ShearCtl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Wind Speed, m/s";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(WS_ShearCtl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "Height, m";

            NLineSeries unfilteredSeries = new NLineSeries();
            unfilteredSeries.UseXValues = true;
            unfilteredSeries.DataLabelStyle.Visible = false;
            unfilteredSeries.Name = "Unfiltered";
            WS_ShearCtl.Charts[0].Series.Add(unfilteredSeries);
            unfilteredSeries.MarkerStyle.Visible = true;
            unfilteredSeries.MarkerStyle.FillStyle = new NColorFillStyle(Color.Blue);
            unfilteredSeries.BorderStyle.Color = Color.Blue;
            unfilteredSeries.BorderStyle.Width = new NLength(0.1f, NRelativeUnit.ParentPercentage);
            unfilteredSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("Unfiltered");

            if (thisData.filteringDone == true)
            {
                NLineSeries filteredSeries = new NLineSeries();
                filteredSeries.DataLabelStyle.Visible = false;
                filteredSeries.UseXValues = true;
                filteredSeries.Name = "Filtered";
                WS_ShearCtl.Charts[0].Series.Add(filteredSeries);
                filteredSeries.MarkerStyle.Visible = true;
                filteredSeries.MarkerStyle.FillStyle = new NColorFillStyle(Color.Red);
                filteredSeries.BorderStyle.Color = Color.Red;
                filteredSeries.BorderStyle.Width = new NLength(0.05f, NRelativeUnit.ParentPercentage);
                filteredSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("Filtered");

                for (int i = 0; i < numAnems; i++)
                {
                    filteredSeries.XValues.Add(filtWS[i]);
                    filteredSeries.Values.Add(heights[i]);
                }
            }

            for (int i = 0; i < numAnems; i++)
            {
                unfilteredSeries.XValues.Add(unfiltWS[i]);
                unfilteredSeries.Values.Add(heights[i]);
            }

            if (numSim > 0)
            {
                NPointSeries extrapolatedSeries = new NPointSeries();
                extrapolatedSeries.DataLabelStyle.Visible = false;
                extrapolatedSeries.UseXValues = true;
                extrapolatedSeries.Name = "Extrapolated";
                WS_ShearCtl.Charts[0].Series.Add(extrapolatedSeries);
                extrapolatedSeries.BorderStyle.Color = Color.Green;
                extrapolatedSeries.BorderStyle.Width = new NLength(0.05f, NRelativeUnit.ParentPercentage);
                extrapolatedSeries.MarkerStyle.FillStyle = new NColorFillStyle(Color.Green);
                extrapolatedSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("Extrapolated");

                for (int i = 0; i < numSim; i++)
                {
                    extrapolatedSeries.XValues.Add(extrapWS[i]);
                    extrapolatedSeries.Values.Add(extrapH[i]);
                }
            }

            thisInst.chtWS_by_H.Refresh();


        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the temperature sensor height and recovery table. </summary>
        ///
        /// <remarks>   Liz, 6/21/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void TempSummary(Continuum thisInst, Met thisMet)
        {

            ListViewItem lstView = new ListViewItem();
            thisInst.lstTempSummary.Items.Clear();

            if (thisMet.metData == null)
                return;

            foreach (Met_Data_Filter.Temp_Data thisTemp in thisMet.metData.temps)
            {
                lstView = thisInst.lstTempSummary.Items.Add(Convert.ToString(thisTemp.height));
                lstView.SubItems.Add(thisMet.metData.CalcTempDataRecovery(thisTemp).ToString("P"));
            }

            thisInst.lstTempSummary.Columns[0].Width = 60;
            thisInst.lstTempSummary.Columns[1].Width = 60;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the vane summary table including height, icing, and recovery. </summary>
        ///
        /// <remarks>   Liz, 6/21/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void VaneSummary(Continuum thisInst, Met thisMet)
        {
            ListViewItem lstView = new ListViewItem();
            thisInst.lstVaneSummary.Items.Clear();

            if (thisMet.metData == null)
                return;

            foreach (Met_Data_Filter.Vane_Data thisVane in thisMet.metData.vanes)
            {
                lstView = thisInst.lstVaneSummary.Items.Add(Convert.ToString(thisVane.height));
                lstView.SubItems.Add(thisMet.metData.CalcVaneDataRecovery(thisVane, false).ToString("P"));
                if (thisMet.metData.filteringDone == true)
                    lstView.SubItems.Add(thisMet.metData.CalcPercentVaneFiltered(thisVane, Met_Data_Filter.Filter_Flags.Icing).ToString("P"));

                else
                    lstView.SubItems.Add(Convert.ToString(""));

                lstView.SubItems.Add(thisMet.metData.CalcVaneDataRecovery(thisVane, true).ToString("P"));
            }

            thisInst.lstVaneSummary.Columns[0].Width = 40;
            thisInst.lstVaneSummary.Columns[1].Width = 60;
            thisInst.lstVaneSummary.Columns[2].Width = 60;
            thisInst.lstVaneSummary.Columns[3].Width = 60;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the extrapolated wind speed summary. </summary>
        ///
        /// <remarks>   Liz, 10/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ExtrapolatedSummary(Continuum thisInst, Met thisMet)
        {
            ListViewItem lstView = new ListViewItem();
            thisInst.lstExtrapolated.Items.Clear();

            if (thisMet.metData == null)
                return;

            foreach (Met_Data_Filter.Sim_TS thisSim in thisMet.metData.simData)
            {
                lstView = thisInst.lstExtrapolated.Items.Add(Convert.ToString(thisSim.height));
                lstView.SubItems.Add(Math.Round(thisMet.metData.CalcAvgExtrapolatedWS(thisSim), 3).ToString());
                lstView.SubItems.Add(Math.Round(thisMet.metData.CalcExtrapRecovery(thisSim), 4).ToString("P"));
            }

            thisInst.lstExtrapolated.Columns[0].Width = 60;
            thisInst.lstExtrapolated.Columns[1].Width = 55;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Updates Anemometer summary table with mean WS, % recovery, % flagged (if filtering has been
        /// done)
        /// </summary>
        ///
        /// <remarks>   Liz, 10/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AnemometerSummary(Continuum thisInst, Met thisMet)
        {
            ListViewItem lstView = new ListViewItem();
            thisInst.lstAnemSummary.Items.Clear();

            if (thisMet.metData == null)
                return;

            foreach (Met_Data_Filter.Anem_Data thisAnem in thisMet.metData.anems)
            {
                lstView = thisInst.lstAnemSummary.Items.Add(Convert.ToString(thisAnem.height));
                lstView.SubItems.Add(thisAnem.orientation.ToString());

                lstView.SubItems.Add(Convert.ToString(Math.Round(thisMet.metData.CalcAvgWS(thisAnem, false), 2)));
                if (thisMet.metData.filteringDone == true)
                    lstView.SubItems.Add(Convert.ToString(Math.Round(thisMet.metData.CalcAvgWS(thisAnem, true), 2)));
                else
                    lstView.SubItems.Add("");

                // data recovery before filtering

                lstView.SubItems.Add(thisMet.metData.CalcAnemDataRecovery(thisAnem, false).ToString("P"));

                if (thisMet.metData.filteringDone == true)
                {
                    double SD_Filtered = thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.minAnemSD) + thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.maxAnemSD);

                    lstView.SubItems.Add(thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.Icing).ToString("P"));
                    lstView.SubItems.Add(thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.towerEffect).ToString("P"));
                    lstView.SubItems.Add(thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.minWS).ToString("P"));
                    lstView.SubItems.Add(SD_Filtered.ToString("P"));
                    lstView.SubItems.Add(thisMet.metData.CalcPercentAnemFiltered(thisAnem, Met_Data_Filter.Filter_Flags.maxAnemRange).ToString("P"));
                }
                else
                {
                    lstView.SubItems.Add(Convert.ToString(""));
                    lstView.SubItems.Add(Convert.ToString(""));
                    lstView.SubItems.Add(Convert.ToString(""));
                    lstView.SubItems.Add(Convert.ToString(""));
                    lstView.SubItems.Add(Convert.ToString(""));
                }

                // data recovery after filtering
                lstView.SubItems.Add(thisMet.metData.CalcAnemDataRecovery(thisAnem, true).ToString("P"));
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Updates the start/end dates of imported dataset and export start/end dates.
        /// </summary>
        ///
        /// <remarks>   Liz, 10/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates dropdown list showing heights of anemometers. </summary>
        ///
        /// <remarks>   Liz, 10/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MetQC_AnemDropdown(Continuum thisInst, Met thisMet)
        {
            thisInst.okToUpdate = false;
            thisInst.cboSensorHeight.Items.Clear();
            thisInst.cboSensorHeight.SelectedText = "";
            double Last_Height = 0.0;

            if (thisMet.metData == null)
            {
                thisInst.okToUpdate = true;
                return;
            }

            foreach (Met_Data_Filter.Anem_Data thisAnem in thisMet.metData.anems)
            {
                if (Math.Abs(thisAnem.height - Last_Height) > 2)
                {
                    thisInst.cboSensorHeight.Items.Add(thisAnem.height);
                    Last_Height = thisAnem.height;
                }
            }

            if (thisMet.metData.GetNumAnems() > 0) thisInst.cboSensorHeight.SelectedIndex = 0;
            thisInst.okToUpdate = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Updates the WS diff vs WD. Plots Anem A - Anem B (filtered or unfiltered depending on
        /// dropdown)
        /// </summary>
        ///
        /// <remarks>   Liz, 10/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MetWS_DiffvsWD(Continuum thisInst, Met thisMet)
        {
            NChartControl metWS_DiffChtCtrl = thisInst.chtRatio_vs_WD;
            metWS_DiffChtCtrl.Charts[0].Series.Clear();
            metWS_DiffChtCtrl.Charts[0].Width = 220;
            metWS_DiffChtCtrl.Charts[0].Height = 125;
            metWS_DiffChtCtrl.Labels.Clear();
            metWS_DiffChtCtrl.Legends[0].Visible = false;

            if (thisMet.metData == null)
            {
                metWS_DiffChtCtrl.Refresh();
                return;
            }

            if (thisMet.metData.GetNumAnems() == 0)
            {
                metWS_DiffChtCtrl.Refresh();
                return;
            }

            string filtOrUnfilt = thisInst.cboFilt_or_Not.SelectedItem.ToString();
            int anemHeight = Convert.ToInt16(thisInst.cboSensorHeight.SelectedItem.ToString());

            // if there isn't a redundant anem then return
            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == anemHeight)
                    if (thisMet.metData.anems[i].isOnlyMet == true)
                    {
                        metWS_DiffChtCtrl.Refresh();
                        return;
                    }

            // get WS ratios and WD
            Met_Data_Filter.WS_Ratio_WD_Data Ratios_and_WD = thisMet.metData.GetWS_RatioOrDiffAndWD(anemHeight, filtOrUnfilt, "Diff", false);

            if (Ratios_and_WD.WD == null)
            {
                metWS_DiffChtCtrl.Refresh();
                return;
            }

            int numWD = Ratios_and_WD.WD.Length;

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(metWS_DiffChtCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Wind Direction, degs";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(metWS_DiffChtCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = anemHeight + " m A - B WS Diff, m/s";

            NPointSeries metDataSeries = new NPointSeries();
            metDataSeries.DataLabelStyle.Visible = false;
            metDataSeries.UseXValues = true;
            metDataSeries.Size = new NLength(1, NRelativeUnit.ParentPercentage);
            metDataSeries.BorderStyle = new NStrokeStyle(Color.DarkBlue);
            metDataSeries.BorderStyle.Width = new NLength(0.1f);
            metDataSeries.FillStyle = new NColorFillStyle(Color.Blue);
            metWS_DiffChtCtrl.Charts[0].Series.Add(metDataSeries);

            for (int i = 0; i < numWD; i++)
            {
                metDataSeries.XValues.Add(Ratios_and_WD.WD[i]);
                metDataSeries.Values.Add(Ratios_and_WD.WS_Ratio[i]);
            }

            metWS_DiffChtCtrl.Refresh();

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Updates the WS diff vs WS. Plots Anem A - Anem B (filtered or unfiltered depending on
        /// dropdown)
        /// </summary>
        ///
        /// <remarks>   Liz, 10/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MetWS_DiffvsWindSpeed(Continuum thisInst, Met thisMet)
        {
            NChartControl metWS_DiffChtCtrl = thisInst.chtWSRatiobyWS;
            metWS_DiffChtCtrl.Charts[0].Series.Clear();
            metWS_DiffChtCtrl.Charts[0].Width = 220;
            metWS_DiffChtCtrl.Charts[0].Height = 125;
            metWS_DiffChtCtrl.Labels.Clear();
            metWS_DiffChtCtrl.Legends[0].Visible = false;

            if (thisMet.metData == null)
            {
                metWS_DiffChtCtrl.Refresh();
                return;
            }

            if (thisMet.metData.GetNumAnems() == 0)
            {
                metWS_DiffChtCtrl.Refresh();
                return;
            }

            string filtOrUnfilt = thisInst.cboFilt_or_Not.SelectedItem.ToString();
            int anemHeight = Convert.ToInt16(thisInst.cboSensorHeight.SelectedItem.ToString());

            // if there isn't a redundant anem then return
            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == anemHeight)
                    if (thisMet.metData.anems[i].isOnlyMet == true)
                    {
                        metWS_DiffChtCtrl.Refresh();
                        return;
                    }

            // get WS ratios and WS
            Met_Data_Filter.WS_Ratio_WS_Data Ratios_and_WS = thisMet.metData.GetWS_RatioOrDiffAndWS(anemHeight, filtOrUnfilt, "Diff");

            if (Ratios_and_WS.WS != null && Ratios_and_WS.WS_Ratio != null)
            {

                NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(metWS_DiffChtCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
                scaleConfiguratorX.Title.Text = "Avg. WS, m/s";

                NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(metWS_DiffChtCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
                scaleConfiguratorY.Title.Text = anemHeight + " m A - B WS Diff, m/s";

                NPointSeries metDataSeries = new NPointSeries();
                metDataSeries.DataLabelStyle.Visible = false;
                metDataSeries.UseXValues = true;
                metDataSeries.Size = new NLength(1, NRelativeUnit.ParentPercentage);
                metDataSeries.BorderStyle = new NStrokeStyle(Color.DarkBlue);
                metDataSeries.BorderStyle.Width = new NLength(0.1f);
                metDataSeries.FillStyle = new NColorFillStyle(Color.Blue);
                metWS_DiffChtCtrl.Charts[0].Series.Add(metDataSeries);

                for (int i = 0; i < Ratios_and_WS.WS.Length; i++)
                {
                    metDataSeries.XValues.Add(Ratios_and_WS.WS[i]);
                    metDataSeries.Values.Add(Ratios_and_WS.WS_Ratio[i]);
                }

            }

            metWS_DiffChtCtrl.Refresh();

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Updates the wind speed scatterplot. Plots Anem A vs. Anem B (filtered or unfiltered depending
        /// on dropdown)
        /// </summary>
        ///
        /// <remarks>   Liz, 10/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MetAnemScatterplot(Continuum thisInst, Met thisMet)
        {
            NChartControl metScatterChtCtrl = thisInst.chtWS_Scatter;
            metScatterChtCtrl.Charts[0].Series.Clear();
            metScatterChtCtrl.Labels.Clear();
            metScatterChtCtrl.Charts[0].Width = 200;
            metScatterChtCtrl.Charts[0].Height = 125;
            metScatterChtCtrl.Legends[0].Visible = false;

            if (thisMet.metData == null)
            {
                metScatterChtCtrl.Refresh();
                return;
            }

            if (thisMet.metData.GetNumAnems() == 0)
            {
                metScatterChtCtrl.Refresh();
                return;
            }

            string filtOrUnfilt = thisInst.cboFilt_or_Not.Text;

            if (thisInst.cboSensorHeight.Items.Count == 0)
            {
                metScatterChtCtrl.Refresh();
                return;
            }

            int anemHeight = Convert.ToInt16(thisInst.cboSensorHeight.SelectedItem.ToString());

            // if there isn't a redundant anem then return
            for (int i = 0; i < thisMet.metData.GetNumAnems(); i++)
                if (thisMet.metData.anems[i].height == anemHeight)
                    if (thisMet.metData.anems[i].isOnlyMet == true)
                    {
                        metScatterChtCtrl.Refresh();
                        return;
                    }

            // get concurrent wind speed data
            Met_Data_Filter.Conc_WS_Data concWS = thisMet.metData.GetConcWS(anemHeight, filtOrUnfilt, thisInst, thisMet.name);

            if (concWS.anemA_WS != null && concWS.anemB_WS != null)
            {
                NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(metScatterChtCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
                scaleConfiguratorX.Title.Text = anemHeight + " m A WS, m/s";

                NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(metScatterChtCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
                scaleConfiguratorY.Title.Text = anemHeight + " m B WS, m/s";

                NPointSeries metDataSeries = new NPointSeries();
                metDataSeries.DataLabelStyle.Visible = false;
                metDataSeries.UseXValues = true;
                metDataSeries.Size = new NLength(1, NRelativeUnit.ParentPercentage);
                metDataSeries.BorderStyle = new NStrokeStyle(Color.DarkBlue);
                metDataSeries.BorderStyle.Width = new NLength(0.1f);
                metDataSeries.FillStyle = new NColorFillStyle(Color.Blue);
                metScatterChtCtrl.Charts[0].Series.Add(metDataSeries);

                for (int i = 0; i < concWS.anemA_WS.Length; i++)
                {
                    metDataSeries.XValues.Add(concWS.anemA_WS[i]);
                    metDataSeries.Values.Add(concWS.anemB_WS[i]);
                }
            }

            metScatterChtCtrl.Refresh();

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the wind rose or ws by wd plot. </summary>
        ///
        /// <remarks>   Liz, 7/6/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MetWindRoseOrWS_byWDPlot(Continuum thisInst, Met thisMet)
        {
            NChartControl metRoseChtCtrl = thisInst.chtWindRoseMetQC;            
            metRoseChtCtrl.Charts.Clear();
            metRoseChtCtrl.Labels.Clear();

            Met_Data_Filter thisData = thisMet.metData;
            if (thisMet.metData == null)
            {
                metRoseChtCtrl.Refresh();
                return;
            }

            if (thisData.GetNumAnems() == 0)
            {
                metRoseChtCtrl.Refresh();
                return;
            }

            int anemHeight = Convert.ToInt16(thisInst.cboSensorHeight.SelectedItem.ToString());
            int vaneInd = thisData.GetVaneClosestToHH(anemHeight);

            double[] thisWR_orWS;
            NRadarChart WR_Chart = new NRadarChart();            
            metRoseChtCtrl.Charts.Add(WR_Chart);
            metRoseChtCtrl.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            metRoseChtCtrl.Controller.Tools.Add(tooltip);
            AddAxisToRadarPlot(ref WR_Chart, thisInst.metList.numWD, true, 7);
            string filtOrNotFilt = thisInst.cboFilt_or_Not.SelectedItem.ToString();

            if (thisInst.cboMetWindRose.SelectedItem.ToString() == "Wind Rose")
            {
                thisWR_orWS = thisData.Calc_Wind_Rose(thisData.startDate, thisData.endDate, vaneInd, filtOrNotFilt);
                AddSeriesToRadarPlot(ref WR_Chart, thisWR_orWS, "Vane " + thisData.vanes[vaneInd].height, GetMetOrTurbColor(0), true, true);
            }
            else
            {
                for (int i = 0; i < thisData.GetNumAnems(); i++)
                {
                    if (thisData.anems[i].height == anemHeight)
                    {
                        thisWR_orWS = thisData.Calc_Avg_WS_by_WD(thisData.startDate, thisData.endDate, i, filtOrNotFilt);
                        string Series_Name = "Anem " + thisData.anems[i].height + " " + thisData.anems[i].orientation;

                        AddSeriesToRadarPlot(ref WR_Chart, thisWR_orWS, Series_Name, GetMetOrTurbColor(5), true, true);
                        break;
                    }
                }
            }

            metRoseChtCtrl.Refresh();

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets WR or WS selected dropdown. </summary>
        ///
        /// <remarks>   Liz, 7/6/2018. </remarks>
        ///
        /// <returns>   Selected Wind Rose or Directional WS. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Get_Wind_Rose_or_WS_by_WD(Continuum thisInst)
        {
            string This_Plot = thisInst.cboWS_or_WD.SelectedItem.ToString();
            return This_Plot;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the text boxes on MCP form. </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

            if (thisMCP.gotMCP_Est == false)
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

            if (thisMet.mcp.gotConc == false)
                thisMet.mcp.FindConcurrentData(thisMet.mcp.concStart, thisMet.mcp.concEnd);

         //   MCP.Site_data[] LT_WS_Est = thisMet.mcp.GenerateLT_WS_TS(thisInst, thisMet, MCP_method);

            int Num_WD = thisInst.metList.numWD;
            Met.TOD thisTOD = thisInst.GetSelectedTOD("MCP");
            Met.Season thisSeason = thisInst.GetSelectedSeason("MCP");

            double avgRef = 0;
            double avgTarg = 0;

            // Update Num. Yrs text boxes 
            if (thisMCP.gotRef)
            {
                double num_yrs = thisMet.mcp.refData.Length / 365.0 / 24.0;
                thisInst.txtNumYrsRef.Text = Convert.ToString(Math.Round(num_yrs, 2));
            }
            else
                thisInst.txtNumYrsRef.Text = "";

            if (thisMCP.gotTarg)
            {
                int This_length = thisMet.mcp.targetData.Length;
                double num_yrs = This_length / 8760.0;
                thisInst.txtNumYrsTarg.Text = Convert.ToString(Math.Round(num_yrs, 2));
            }
            else
                thisInst.txtNumYrsTarg.Text = "";

            if (thisMCP.gotConc)
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
                minWD = WD_Ind * 360 / Num_WD - 360 / Num_WD / 2;
                if (minWD < 0)
                    minWD = minWD + 360;
                maxWD = WD_Ind * 360 / Num_WD + 360 / Num_WD / 2;
                if (maxWD > 360)
                    maxWD = maxWD - 360;
            }

            Stats stat = new Stats();
            if (thisMCP.gotRef)
            {
                avgRef = stat.CalcAvgWS(thisMet.mcp.refData, thisMCP.refStart, thisMCP.refEnd, minWD, maxWD, thisTOD, thisSeason, thisInst.metList);
                thisInst.txtRef_LT_WS.Text = Convert.ToString(Math.Round(avgRef, 2));
            }
            else
                thisInst.txtRef_LT_WS.Text = "";

            if (thisMCP.gotConc)
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
                avgRef = stat.CalcAvgWS(thisMet.mcp.refData, thisMCP.refStart, thisMCP.refEnd, minWD, maxWD, thisTOD, thisSeason, thisInst.metList);
                double Avg_Target_LT = stat.CalcAvgWS(thisMet.mcp.LT_WS_Ests, thisMCP.refStart, thisMCP.refEnd, minWD, maxWD, thisTOD, thisSeason, thisInst.metList);
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Updates calendar dates for concurrent period used in MCP and dates used in export.
        /// </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

            if (thisMCP.gotConc && (thisInst.dateConcurrentStart.Value != thisMCP.concStart))
                thisInst.dateConcurrentStart.Value = thisMCP.concStart;
            else
                thisInst.dateConcurrentStart.Value = thisMet.metData.startDate;

            if (thisMCP.gotConc && (thisInst.dateConcurrentEnd.Value != thisMCP.concEnd))
                thisInst.dateConcurrentEnd.Value = thisMCP.concEnd;
            else
                thisInst.dateConcurrentEnd.Value = thisMet.metData.endDate;            

            if (thisMCP.gotRef)
            {
                thisInst.dateMCPExportStart.Value = thisMCP.exportStart;
                thisInst.dateMCPExportEnd.Value = thisMCP.exportEnd;
            }

            thisInst.okToUpdate = true;
        }

        public void MCP_Settings(Continuum thisInst)
        {
            // Updates the MCP settings on MCP tab. Called when a file is opened
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Enables or disables export buttons based on what analysis has been done. </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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
                    int thisOffset = thisInst.UTM_conversions.GetUTC_Offset(metLL.latitude, metLL.longitude);
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
            
            if (thisMCP.gotMCP_Est == true)
            {
                thisInst.btnExportMCP_TS.Enabled = true;
                thisInst.btnExportMCP_TAB.Enabled = true;
            }
            else
            {
                thisInst.btnExportMCP_TS.Enabled = false;
                thisInst.btnExportMCP_TAB.Enabled = false;
            }

            if (selectedMethod == "Method of Bins" && thisMCP.gotMCP_Est == true)
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
                int offset = thisInst.UTM_conversions.GetUTC_Offset(theseLL.latitude, theseLL.longitude);
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
            else if (thisMet.mcp.gotMCP_Est == true)
            {
                thisInst.btnDoMCP.BackColor = Color.MediumSeaGreen;
                thisInst.btnDoMCP.Enabled = false;
            }

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the scatterplot showing target versus reference wind speed. </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MCP_Scatterplot(Continuum thisInst)
        {
            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;                     

            int WD_Ind = thisInst.GetWD_ind("MCP");
            Met.TOD thisTOD = thisInst.GetSelectedTOD("MCP");
            Met.Season thisSeason = thisInst.GetSelectedSeason("MCP");

            NChartControl MCP_ChtCtrl = thisInst.chtMCP_Scatter;
            MCP_ChtCtrl.Charts[0].Series.Clear();
            MCP_ChtCtrl.Labels.Clear();
            MCP_ChtCtrl.Legends[0].Visible = false;

            if (thisMCP == null)
            {
                MCP_ChtCtrl.Refresh();
                return;
            }

            if (thisMCP.gotMCP_Est == false)
            {
                MCP_ChtCtrl.Refresh();
                return;
            }
                
            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(MCP_ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "MERRA2 50 m Wind Speed, m/s";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(MCP_ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = thisMet.name + " " + thisInst.modeledHeight + " Wind Speed, m/s";

            if (thisMCP.refData.Length == 0)
            {
                UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                MERRA thisMERRA = thisInst.merraList.GetMERRA(theseLL.latitude, theseLL.longitude);
                thisMCP.refData = thisMCP.GetRefData(thisMERRA, ref thisMet, thisInst);
            }

            if (thisMCP.targetData.Length == 0)
                thisMCP.targetData = thisMCP.GetTargetData(thisInst.modeledHeight, thisMet);

            if (thisMCP.gotConc == false) thisMCP.FindConcurrentData(thisMCP.concStart, thisMCP.concEnd);

            if (thisMCP.gotConc)
            {
                NPointSeries MCP_DataSeries = new NPointSeries();
                MCP_DataSeries.DataLabelStyle.Visible = false;
                MCP_DataSeries.UseXValues = true;
                MCP_DataSeries.Size = new NLength(1.0f, NRelativeUnit.ParentPercentage);
                MCP_DataSeries.BorderStyle = new NStrokeStyle(Color.White);
                MCP_ChtCtrl.Charts[0].Series.Add(MCP_DataSeries);

                double[] thisRefWS = new double[0];
                double[] thisTargWS = new double[0];

          //      if ((WD_Ind == thisMCP.numWD) || ((WD_Ind == 0) && (thisMCP.numWD == 1)))
           //     {
           //         thisRefWS = thisMCP.GetConcWS_Array("Ref", WD_Ind, thisTOD, thisSeason, 0, 30, true);
          //          thisTargWS = thisMCP.GetConcWS_Array("Target", WD_Ind, thisTOD, thisSeason, 0, 30, true);
           //     }
          //      else
           //     {
                    thisRefWS = thisMCP.GetConcWS_Array("Ref", WD_Ind, thisTOD, thisSeason, 0, 30, false);
                    thisTargWS = thisMCP.GetConcWS_Array("Target", WD_Ind, thisTOD, thisSeason, 0, 30, false);
          //      }

                if (thisRefWS != null)
                {
                    for (int i = 0; i < thisRefWS.Length; i++)
                    {
                        MCP_DataSeries.XValues.Add(thisRefWS[i]);
                        MCP_DataSeries.Values.Add(thisTargWS[i]);
                    }
                }

                if ((thisMCP.MCP_Ortho.slope != null) && (thisInst.Get_MCP_Method() == "Orth. Regression"))
                {
                    NLineSeries SlopeSeries = new NLineSeries();
                    SlopeSeries.DataLabelStyle.Visible = false;
                    SlopeSeries.Name = "Ortho. Reg.";
                    MCP_ChtCtrl.Charts[0].Series.Add(SlopeSeries);

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
                        SlopeSeries.XValues.Add(Ortho_X[i]);
                        SlopeSeries.Values.Add(Ortho_Y[i]);
                    }
                }

                else if ((thisMCP.MCP_Varrat.slope != null) && (thisInst.Get_MCP_Method() == "Variance Ratio"))
                {
                    NLineSeries SlopeSeries = new NLineSeries();
                    SlopeSeries.DataLabelStyle.Visible = false;
                    SlopeSeries.Name = "Variance Ratio";
                    MCP_ChtCtrl.Charts[0].Series.Add(SlopeSeries);

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
                        SlopeSeries.XValues.Add(Varrat_X[i]);
                        SlopeSeries.Values.Add(Varrat_Y[i]);
                    }
                }
                else if ((thisMCP.MCP_Bins.binAvgSD_Cnt != null) && (thisInst.Get_MCP_Method() == "Method of Bins"))
                {
                    NPointSeries binSeries = new NPointSeries();
                    binSeries.DataLabelStyle.Visible = false;
                    binSeries.UseXValues = true;
                    binSeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
                    // TO DO: Put on secondary y-axis
                    MCP_ChtCtrl.Charts[0].Series.Add(binSeries);

                    for (int i = 0; i < thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(0); i++)
                    {
                        if (thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].avgWS_Ratio > 0)
                        {
                            binSeries.XValues.Add(i * thisMCP.Get_WS_width_for_MCP());
                            binSeries.Values.Add(thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].avgWS_Ratio);
                        }
                    }
                }

                MCP_TextBoxes(thisInst);
                MCP_ChtCtrl.Refresh();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Update list with mean and standard deviation of WS ratios. </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MCP_BinList(Continuum thisInst)
        {
            thisInst.lstMCP_Bins.Items.Clear();
            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;

            if (thisMCP == null)
                return;

            if (thisMCP.MCP_Bins.binAvgSD_Cnt != null)
            {
                ListViewItem objlist = new ListViewItem();
                int numWS = thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(0);
                int numWD = thisMCP.Get_Num_WD();
                int WD_Ind = thisInst.GetWD_ind("MCP");
                double WS_Width = thisMCP.Get_WS_width_for_MCP();

                for (int i = 0; i < numWS; i++)
                {
                    double thisWS = i * WS_Width;
                    if (thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].avgWS_Ratio > 0)
                    {
                        objlist = thisInst.lstMCP_Bins.Items.Add(Convert.ToString(thisWS));
                        objlist.SubItems.Add(Convert.ToString(Math.Round(thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].avgWS_Ratio, 2)));
                        objlist.SubItems.Add(Convert.ToString(Math.Round(thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].SD_WS_Ratio, 2)));
                        objlist.SubItems.Add(Convert.ToString(Math.Round(thisMCP.MCP_Bins.binAvgSD_Cnt[i, WD_Ind].count, 2)));
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update table with results of uncertainty analysis showing window size, mean LT estimate and
        /// standard deviation of LT estimate.
        /// </summary>
        ///
        /// <remarks>   Liz, 5/16/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MCP_UncertList(Continuum thisInst)
        {
            thisInst.lstMCP_Uncert.Items.Clear();
            ListViewItem objlist = new ListViewItem();
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
                        objlist = thisInst.lstMCP_Uncert.Items.Add(Convert.ToString(thisMCP.uncertOrtho[u].WSize));
                        objlist.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertOrtho[u].avg, 2)));
                        objlist.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertOrtho[u].stDev, 2)));
                    }
                }
            }
            if ((active_method == "Method of Bins") && (thisMCP.uncertBins.Length > 0))
            {
                for (int u = 0; u < thisMCP.uncertBins.Length; u++)
                {
                    if ((thisMCP.uncertBins[u].avg != 0) && (thisMCP.uncertBins[u].stDev != 0))
                    {
                        objlist = thisInst.lstMCP_Uncert.Items.Add(Convert.ToString(thisMCP.uncertBins[u].WSize));
                        objlist.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertBins[u].avg, 2)));
                        objlist.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertBins[u].stDev, 2)));
                    }
                }
            }
            if ((active_method == "Variance Ratio") && (thisMCP.uncertVarrat.Length > 0))
            {
                for (int u = 0; u < thisMCP.uncertVarrat.Length; u++)
                {
                    if ((thisMCP.uncertVarrat[u].avg != 0) && (thisMCP.uncertVarrat[u].stDev != 0))
                    {
                        objlist = thisInst.lstMCP_Uncert.Items.Add(Convert.ToString(thisMCP.uncertVarrat[u].WSize));
                        objlist.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertVarrat[u].avg, 2)));
                        objlist.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertVarrat[u].stDev, 2)));
                    }
                }
            }
            if ((active_method == "Matrix") && (thisMCP.uncertMatrix.Length > 0))
            {
                for (int u = 0; u < thisMCP.uncertMatrix.Length; u++)
                {
                    if ((thisMCP.uncertMatrix[u].avg != 0) && (thisMCP.uncertMatrix[u].stDev != 0))
                    {
                        objlist = thisInst.lstMCP_Uncert.Items.Add(Convert.ToString(thisMCP.uncertMatrix[u].WSize));
                        objlist.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertMatrix[u].avg, 2)));
                        objlist.SubItems.Add(Convert.ToString(Math.Round(thisMCP.uncertMatrix[u].stDev, 2)));
                    }
                }
            }
        }

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

        public void MCP_UncertPlot(Continuum thisInst)
        {
            NChartControl MCP_Uncert_ChtCtrl = thisInst.chtMCP_Uncert;
            MCP_Uncert_ChtCtrl.Charts[0].Series.Clear();
            MCP_Uncert_ChtCtrl.Labels.Clear();
            MCP_Uncert_ChtCtrl.Controller.Tools.Clear();
            MCP_Uncert_ChtCtrl.Legends[0].Visible = false;
            NTooltipTool tooltip = new NTooltipTool();
            MCP_Uncert_ChtCtrl.Controller.Tools.Add(tooltip);

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(MCP_Uncert_ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Window Size, months";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(MCP_Uncert_ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "LT Est. Target WS, m/s";

            NPointSeries LTDataSeries = new NPointSeries();
            LTDataSeries.UseXValues = true;
            LTDataSeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
            LTDataSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("LT Est. Data");
            LTDataSeries.PointShape = PointShape.Diamond;
            LTDataSeries.DataLabelStyle.Visible = false;
            MCP_Uncert_ChtCtrl.Charts[0].Series.Add(LTDataSeries);

            NPointSeries AvgDataSeries = new NPointSeries();
            AvgDataSeries.UseXValues = true;
            AvgDataSeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
            AvgDataSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("LT Est. Avg.");
            AvgDataSeries.DataLabelStyle.Visible = false;
            AvgDataSeries.FillStyle = new NColorFillStyle(Color.Red);
            MCP_Uncert_ChtCtrl.Charts[0].Series.Add(AvgDataSeries);

            // Get Active MCP type
            string selectedMethod = thisInst.Get_MCP_Method();
            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisMet.mcp;

            if (thisMCP == null)
            {
                MCP_Uncert_ChtCtrl.Refresh();
                return;
            }
                
            if (selectedMethod == "Orth. Regression" && thisMCP.uncertOrtho.Length > 0)
            {
                for (int u = 0; u < thisMCP.uncertOrtho.Length; u++)
                {
                    if (thisMCP.uncertOrtho[u].LT_Ests != null)
                    {
                        for (int i = 0; i < thisMCP.uncertOrtho[u].LT_Ests.Length; i++)
                        {
                            LTDataSeries.XValues.Add(thisMCP.uncertOrtho[u].WSize);
                            LTDataSeries.Values.Add(thisMCP.uncertOrtho[u].LT_Ests[i]);
                        }

                    }
                    // Assign LT Avg series = Avg of Uncert obj
                    if (thisMCP.uncertOrtho[u].avg != 0)
                    {
                        AvgDataSeries.XValues.Add(thisMCP.uncertOrtho[u].WSize);
                        AvgDataSeries.Values.Add(thisMCP.uncertOrtho[u].avg);
                    }
                }
            }
            if (selectedMethod == "Method of Bins" && thisMCP.uncertBins.Length > 0)
            {
                for (int u = 0; u < thisMCP.uncertBins.Length; u++)
                {
                    if (thisMCP.uncertBins[u].LT_Ests != null)
                    {
                        for (int i = 0; i < thisMCP.uncertBins[u].LT_Ests.Length; i++)
                        {
                            LTDataSeries.XValues.Add(thisMCP.uncertBins[u].WSize);
                            LTDataSeries.Values.Add(thisMCP.uncertBins[u].LT_Ests[i]);
                        }
                    }
                    // Assign LT Avg series = Avg of Uncert obj
                    if (thisMCP.uncertBins[u].avg != 0)
                    {
                        AvgDataSeries.XValues.Add(thisMCP.uncertBins[u].WSize);
                        AvgDataSeries.Values.Add(thisMCP.uncertBins[u].avg);
                    }
                }
            }
            if (selectedMethod == "Variance Ratio" && thisMCP.uncertVarrat.Length > 0)
            {
                for (int u = 0; u < thisMCP.uncertVarrat.Length; u++)
                {
                    if (thisMCP.uncertVarrat[u].LT_Ests != null)
                    {
                        for (int i = 0; i < thisMCP.uncertVarrat[u].LT_Ests.Length; i++)
                        {
                            LTDataSeries.XValues.Add(thisMCP.uncertVarrat[u].WSize);
                            LTDataSeries.Values.Add(thisMCP.uncertVarrat[u].LT_Ests[i]);
                        }

                    }
                    // Assign LT Avg series = Avg of Uncert obj
                    if (thisMCP.uncertVarrat[u].avg != 0)
                    {
                        AvgDataSeries.XValues.Add(thisMCP.uncertVarrat[u].WSize);
                        AvgDataSeries.Values.Add(thisMCP.uncertVarrat[u].avg);
                    }
                }
            }
            if (selectedMethod == "Matrix" && thisMCP.uncertMatrix.Length > 0)
            {
                for (int u = 0; u < thisMCP.uncertMatrix.Length; u++)
                {
                    if (thisMCP.uncertMatrix[u].LT_Ests != null)
                    {
                        for (int i = 0; i < thisMCP.uncertMatrix[u].LT_Ests.Length; i++)
                        {
                            LTDataSeries.XValues.Add(thisMCP.uncertMatrix[u].WSize);
                            LTDataSeries.Values.Add(thisMCP.uncertMatrix[u].LT_Ests[i]);
                        }
                    }
                    // Assign LT Avg series = Avg of Uncert obj
                    if (thisMCP.uncertMatrix[u].avg != 0)
                    {
                        AvgDataSeries.XValues.Add(thisMCP.uncertMatrix[u].WSize);
                        AvgDataSeries.Values.Add(thisMCP.uncertMatrix[u].avg);
                    }
                }
            }

            MCP_Uncert_ChtCtrl.Refresh();

        }

        public void MERRA_AnnualTableAndPlot(Continuum thisInst)
        {
            // Fills out yearly summary table on MERRA2 tab based on selected parameter and creates plot of yearly values

            thisInst.lstMERRAAnnualProd.Items.Clear();
            MERRA thisMERRA = thisInst.GetSelectedMERRA();

            if (thisMERRA.interpData.TS_Data == null)
            {
                thisInst.chtMERRA_Yearly.Charts[0].Series.Clear();
                thisInst.chtMERRA_Yearly.Refresh();
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
                thisInst.chtMERRA_Yearly.Charts[0].Series.Clear();
                thisInst.chtMERRA_Yearly.Refresh();
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

            NChartControl chtMERRA_Year = thisInst.chtMERRA_Yearly;
            chtMERRA_Year.Charts[0].Series.Clear();
            chtMERRA_Year.Labels.Clear();
            chtMERRA_Year.Labels.AddHeader("Yearly " + selectedParam);
            chtMERRA_Year.Legends[0].Visible = false;

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(chtMERRA_Year.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Year";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(chtMERRA_Year.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = selectedParam;

            if (selectedParam == "Energy Prod.")
                scaleConfiguratorY.Title.Text = "Energy Prod. [MWh]";
            else if (selectedParam == "Avg WS")
                scaleConfiguratorY.Title.Text = "WS [m/s]";

            NLineSeries paramSeries = new NLineSeries();
            paramSeries.DataLabelStyle.Visible = false;
            paramSeries.Name = selectedParam;
            chtMERRA_Year.Charts[0].Series.Add(paramSeries);
            paramSeries.BorderStyle.Color = NColor.ColorFromString("Blue");
            paramSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);

            for (int i = firstYear; i <= lastYear; i++)
            {
                if (thisMERRA.Have_Full_Year(thisTS, i))
                {
                    ListViewItem lvi = new ListViewItem(i.ToString()); // Adds year to table
                    paramSeries.UseXValues = true;
                    paramSeries.XValues.Add(i);

                    int yearInd = thisMERRA.Get_Year_Ind(i, thisAnnual);

                    if (selectedParam == "CF (%)" || selectedParam == "Energy Prod.")
                    {
                        double prod = thisMERRA.Get_Energy_Prod(thisAnnual, thisMonthly, 100, i);
                        double thisCF = thisMERRA.Calc_CF(prod, 100, i, powerCurve);

                        if (selectedParam == "Energy Prod.")
                        {
                            LT_Val = thisMERRA.Get_Energy_Prod(thisAnnual, thisMonthly, 100, 100);
                            diff = thisMERRA.Calc_Dev_from_LT(thisMonthly, thisAnnual, i, 100);
                            lvi.SubItems.Add(Math.Round(prod, 1).ToString());
                            paramSeries.Values.Add(prod);
                        }
                        else
                        {
                            double LT_Prod = thisMERRA.Get_Energy_Prod(thisAnnual, thisMonthly, 100, 100);
                            LT_Val = thisMERRA.Calc_CF(LT_Prod, 100, 100, powerCurve);
                            diff = (thisCF - LT_Val) / LT_Val;
                            lvi.SubItems.Add(Math.Round(thisCF, 4).ToString("P"));
                            paramSeries.Values.Add(thisCF * 100);
                        }
                    }
                    else
                    {
                        double avg = thisMERRA.Calc_Avg_or_LT(thisTS, 100, i, selectedParam);
                        LT_Val = thisMERRA.Calc_Avg_or_LT(thisTS, 100, 100, selectedParam);
                        diff = (avg - LT_Val) / LT_Val;
                        lvi.SubItems.Add(Math.Round(avg, 1).ToString());
                        paramSeries.Values.Add(avg);
                    }

                    lvi.SubItems.Add((Math.Round(diff, 4)).ToString("P")); // adds % diff from the average
                    thisInst.lstMERRAAnnualProd.Items.Add(lvi);

                }

            }

            ListViewItem lvi2 = new ListViewItem("LT Avg"); // Adds year to table
            lvi2.SubItems.Add(Math.Round(LT_Val, 1).ToString());
            chtMERRA_Year.Refresh();
        }

        public void MERRA_Param_Checkboxes(Continuum thisInst, MERRA thisMERRA)
        {
            thisInst.okToUpdate = false;

            if (thisMERRA.MERRA_Params.Get_50mWSWD)
                thisInst.chkWSWD50m.Checked = true;
            else
                thisInst.chkWSWD50m.Checked = false;

            if (thisMERRA.MERRA_Params.Get_10mTemp)
                thisInst.chkTemp10m.Checked = true;
            else
                thisInst.chkTemp10m.Checked = false;

            if (thisMERRA.MERRA_Params.Get_SurfPress)
                thisInst.chkPressure.Checked = true;
            else
                thisInst.chkPressure.Checked = false;

            if (thisMERRA.MERRA_Params.Get_SeaPress)
                thisInst.chkSeaLevel.Checked = true;
            else
                thisInst.chkSeaLevel.Checked = false;

            /*     if (thisMERRA.MERRA_Params.Get_10mWSWD)
                     thisInst.chkWSWD10m.Checked = true;
                 else
                     thisInst.chkWSWD10m.Checked = false;

                 if (thisMERRA.MERRA_Params.Get_FracMean)
                     thisInst.chkCloudFracMean.Checked = true;
                 else
                     thisInst.chkCloudFracMean.Checked = false;

                 if (thisMERRA.MERRA_Params.Get_OpticalThick)
                     thisInst.chkOptThick.Checked = true;
                 else
                     thisInst.chkOptThick.Checked = false;

                 if (thisMERRA.MERRA_Params.Get_TotalFrac)
                     thisInst.chkTotalFrac.Checked = true;
                 else
                     thisInst.chkTotalFrac.Checked = false;

                 if (thisMERRA.MERRA_Params.Get_Precip)
                     thisInst.chk_Precip.Checked = true;
                 else
                     thisInst.chk_Precip.Checked = false;

                 if (thisMERRA.MERRA_Params.Get_Corr_Precip)
                     thisInst.chk_Precip_Corr.Checked = true;
                 else
                     thisInst.chk_Precip_Corr.Checked = false;
             */

            thisInst.okToUpdate = true;
        }

        /*      public void MERRA_EnergyProd(Continuum thisInst)
              {
                  thisInst.txtMERRA_EnergyProd.Text = "";
                  MERRA thisMERRA = thisInst.GetSelectedMERRA();

                  if (thisInst.okToUpdate == false || thisMERRA.GotWindTS(thisInst.UTM_conversions) == false)
                      return;

                  string monthlyOrYearly = thisInst.cboMERRA_By_Year_or_Month.SelectedItem.ToString();
                  int selectedYear = thisInst.GetMERRA_SelectedYear();
                  int selectedMonth = 100;
                  double thisValue = 0;
                  string param = thisInst.GetMERRA_SelectedPlotParameter();
                  TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("MERRA");

                  if (monthlyOrYearly == "Monthly")
                      selectedMonth = thisInst.GetMERRA_SelectedMonth();

                  if (selectedMonth == -1)
                      return;

                  MERRA.MonthlyProdByYearAndLTAvg[]  thisMonthly = thisMERRA.interpData.Monthly_Prod;            
                  MERRA.YearlyProdAndLTAvg thisYearly = thisMERRA.interpData.Annual_Prod;
                  MERRA.Wind_TS[] thisTS = thisMERRA.interpData.TS_Data;

                  if (param == "Energy prod.")
                      thisValue = thisMERRA.Get_Energy_Prod(thisYearly, thisMonthly, selectedMonth, selectedYear);
                  else if (param == "CF (%)")
                  {
                      double thisProd = thisMERRA.Get_Energy_Prod(thisYearly, thisMonthly, selectedMonth, selectedYear);
                      thisValue = thisMERRA.Calc_CF(thisProd, selectedMonth, selectedYear, powerCurve);
                  }
                  else if (param == "% diff from LT")
                      thisValue = thisMERRA.Calc_Dev_from_LT(thisMonthly, thisYearly, selectedYear, selectedMonth);
                  else
                      thisValue = thisMERRA.Calc_Avg_or_LT(thisTS, selectedMonth, selectedYear, param);

                  if (param == "Energy prod.")
                      thisInst.txtMERRA_EnergyProd.Text = Math.Round(thisValue, 1).ToString();
                  else if (param == "CF (%)" || param == "% diff from LT")
                      thisInst.txtMERRA_EnergyProd.Text = Math.Round(thisValue, 4).ToString("0.00%");


              }
      */
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Fill monthly table. </summary>
        ///
        /// <remarks>   OEE, 1/16/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MERRA_MonthlyTableAndPlot(Continuum thisInst)
        {
            // Fills table and plot with monthly values (only for years that are checked in list)
            thisInst.lstMERRA_MonthlyProd.Items.Clear();
            MERRA thisMERRA = thisInst.GetSelectedMERRA();

            if (thisInst.okToUpdate == false || thisMERRA.GotWindTS(thisInst.UTM_conversions) == false)
            {
                thisInst.chtMERRA_Monthly.Charts[0].Series.Clear();
                thisInst.chtMERRA_Monthly.Refresh();
                return;
            }
                

            // Calculate monthly energy production by year and average monthly energy production

            if (thisMERRA.GotMonthlyProd() == false)
            {
                thisMERRA.Calc_MonthProdStats(thisInst.UTM_conversions);
                thisInst.chtMERRA_Monthly.Charts[0].Series.Clear();
                thisInst.chtMERRA_Monthly.Refresh();
            }


            MERRA.MonthlyProdByYearAndLTAvg[] thisMonthly = thisMERRA.interpData.Monthly_Prod;
            MERRA.YearlyProdAndLTAvg thisAnnual = thisMERRA.interpData.Annual_Prod;
            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("MERRA");

            MERRA.Wind_TS_with_Prod[] thisTS = thisMERRA.interpData.TS_Data;
            string selectedParam = thisInst.GetMERRA_SelectedPlotParameter();
            thisInst.lstMERRA_MonthlyProd.Columns[2].Text = selectedParam;

            if (thisMERRA.powerCurve.name == null && (selectedParam == "CF (%)" || selectedParam == "Energy Prod."))
            {
                thisInst.chtMERRA_Monthly.Charts[0].Series.Clear();
                thisInst.chtMERRA_Monthly.Refresh();
                return;
            }

            int numYears = thisInst.chkYearsToDisplay.CheckedItems.Count;
            int thisYear = 0;
            double diffFromLT = 0;

            // Configure plot
            NChartControl monthPlot = thisInst.chtMERRA_Monthly;
            monthPlot.Charts[0].Series.Clear();
            monthPlot.Labels.Clear();
            monthPlot.Controller.Tools.Clear();
            monthPlot.Legends[0].Visible = false;

            NTooltipTool tooltip = new NTooltipTool();
            monthPlot.Controller.Tools.Add(tooltip);
            monthPlot.Labels.AddHeader("Monthly " + selectedParam);

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(monthPlot.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Month";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(monthPlot.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = selectedParam;

            if (selectedParam == "Energy Prod.")
                scaleConfiguratorY.Title.Text = "Energy Prod. [MWh]";
            else if (selectedParam == "Avg WS")
                scaleConfiguratorY.Title.Text = "WS [m/s]";

            for (int n = 0; n < numYears; n++)
            {
                if (thisInst.chkYearsToDisplay.CheckedItems[n].ToString() == "LT Avg")
                    thisYear = 100;
                else
                    thisYear = Convert.ToInt16(thisInst.chkYearsToDisplay.CheckedItems[n].ToString());

                NLineSeries monthlySeries = new NLineSeries();
                monthlySeries.DataLabelStyle.Visible = false;
                monthlySeries.Name = thisYear.ToString();
                monthPlot.Charts[0].Series.Add(monthlySeries);
                monthlySeries.BorderStyle.Color = GetMetOrTurbColor(n);
                monthlySeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);

                if (thisYear != 100)
                    monthlySeries.InteractivityStyle.Tooltip = new NTooltipAttribute(thisYear.ToString());
                else
                {
                    monthlySeries.InteractivityStyle.Tooltip = new NTooltipAttribute("LT Avg");
                    // FIGURE OUT HOW TO MAKE LINE THICKER FOR LT AVG monthlySeries.MarkerStyle = new NMarkerStyle(PointShape.Diamond, NFillStyle.c_szDefaultSize);
                }

                for (int i = 1; i <= 12; i++)
                {
                    string monthStr = new DateTime(1999, i, 1).ToString("MMM", CultureInfo.InvariantCulture);

                    if ((thisYear == 100 || thisMERRA.Have_Full_Month(thisTS, i, thisYear)) && thisMERRA.MERRA_Params.Get_50mWSWD == true)
                    {
                        ListViewItem lvi = new ListViewItem(monthStr);
                        if (thisYear != 100)
                            lvi.SubItems.Add(thisYear.ToString());
                        else
                            lvi.SubItems.Add("LT Avg");

                        monthlySeries.XValues.Add(i);

                        if (selectedParam == "CF (%)" || selectedParam == "Energy Prod.")
                        {
                            double prod = thisMERRA.Get_Energy_Prod(thisAnnual, thisMonthly, i, thisYear);
                            double LT_Prod = thisMERRA.Get_Energy_Prod(thisAnnual, thisMonthly, i, 100);
                            double thisCF = thisMERRA.Calc_CF(prod, i, thisYear, powerCurve);

                            if (selectedParam == "CF (%)")
                            {
                                lvi.SubItems.Add((Math.Round(thisCF, 4)).ToString("P"));
                                double LT_CF = thisMERRA.Calc_CF(LT_Prod, i, thisYear, powerCurve);
                                diffFromLT = (thisCF - LT_CF) / LT_CF;
                                monthlySeries.Values.Add(thisCF * 100);
                            }
                            else
                            {
                                lvi.SubItems.Add((Math.Round(prod, 1)).ToString());
                                diffFromLT = (prod - LT_Prod) / LT_Prod;
                                monthlySeries.Values.Add(prod);
                            }

                        }
                        else
                        {
                            double LT_Val = thisMERRA.Calc_Avg_or_LT(thisTS, i, 100, selectedParam);
                            double monthVal = thisMERRA.Calc_Avg_or_LT(thisTS, i, thisYear, selectedParam);
                            lvi.SubItems.Add((Math.Round(monthVal, 2)).ToString()); // adds the monthly average 
                            monthlySeries.Values.Add(monthVal);
                            diffFromLT = (monthVal - LT_Val) / LT_Val;

                        }

                        lvi.SubItems.Add(diffFromLT.ToString("P"));
                        thisInst.lstMERRA_MonthlyProd.Items.Add(lvi);
                    }
                }
            }

            thisInst.lstMERRA_MonthlyProd.Columns[0].Width = 40;
            thisInst.lstMERRA_MonthlyProd.Columns[1].Width = 50;
            thisInst.lstMERRA_MonthlyProd.Columns[2].Width = 60;
            thisInst.lstMERRA_MonthlyProd.Columns[3].Width = 90;

            monthPlot.Refresh();
        }

        public void MERRA_Dropdowns(Continuum thisInst)
        {
            // Check to see what datasets have been imported
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

            if (thisMERRA.MERRA_Params.Get_50mWSWD == true)
            {
                thisInst.cboMERRA_PlotParam.Items.Add("50 m WS");
                thisInst.cboMERRA_PlotParam.Items.Add("CF (%)");
                thisInst.cboMERRA_PlotParam.Items.Add("Energy Prod.");
            }

            if (thisMERRA.MERRA_Params.Get_10mWSWD == true)
                thisInst.cboMERRA_PlotParam.Items.Add("10 m WS");

            if (thisMERRA.MERRA_Params.Get_10mTemp == true)
                thisInst.cboMERRA_PlotParam.Items.Add("10 m Temp");

            if (thisMERRA.MERRA_Params.Get_SurfPress == true)
                thisInst.cboMERRA_PlotParam.Items.Add("Surface Pressure");

            if (thisMERRA.MERRA_Params.Get_SeaPress == true)
                thisInst.cboMERRA_PlotParam.Items.Add("Sea Level Pressure");

            if (thisMERRA.MERRA_Params.Get_FracMean == true)
                thisInst.cboMERRA_PlotParam.Items.Add("MODIS Cloud Fraction");

            if (thisMERRA.MERRA_Params.Get_OpticalThick == true)
                thisInst.cboMERRA_PlotParam.Items.Add("MODIS Cloud Thickness");

            if (thisMERRA.MERRA_Params.Get_TotalFrac == true)
                thisInst.cboMERRA_PlotParam.Items.Add("ISCCP Cloud Fraction");

            if (thisMERRA.MERRA_Params.Get_Precip == true)
                thisInst.cboMERRA_PlotParam.Items.Add("Total Precipitation");

            if (thisMERRA.MERRA_Params.Get_Corr_Precip == true)
                thisInst.cboMERRA_PlotParam.Items.Add("Corrected Precipitation");

            if (thisInst.cboMERRA_PlotParam.Items.Count > 0)
                thisInst.cboMERRA_PlotParam.SelectedIndex = 0;

            thisInst.okToUpdate = true;
        }

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
                thisInst.txtMERRA_SelectedLat.Text = "";
                thisInst.txtMERRA_SelectedLat.Enabled = true;
                thisInst.txtMERRA_SelectedLong.Text = "";
                thisInst.txtMERRA_SelectedLong.Enabled = true;
            }

            thisInst.txtMERRA_WS_ScaleFact.Text = thisMERRA.WS_ScaleFactor.ToString();
            thisInst.okToUpdate = true;
        }

        public void MERRA_Settings(Continuum thisInst)
        {
            // When opening a file, this is called to set MERRA start/end dates, number of MERRA nodes, and WS scale factor
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

        public void MERRA_WindRosePlot(Continuum thisInst)
        {
            MERRA thisMERRA = thisInst.GetSelectedMERRA();

            if (thisMERRA.interpData.TS_Data == null)
            {
                thisInst.chtMERRA_WR.Charts[0].Series.Clear();
                thisInst.chtMERRA_WR.Refresh();
                return;
            }

            if (thisMERRA.interpData.TS_Data.Length == 0)
            {
                thisMERRA.GetMERRADataFromDB(thisInst);
                thisMERRA.GetInterpData(thisInst.UTM_conversions);
            }                                       

            if (thisInst.cboMERRAYear.Items.Count == 0)
                return;

            int thisMonth = 100;
            if (thisInst.cboMERRA_Month.SelectedItem.ToString() != "All Months")
                thisMonth = thisInst.cboMERRA_Month.SelectedIndex + 1;

            int thisYear = 100;
            if (thisInst.cboMERRAYear.SelectedItem.ToString() != "LT Avg")
                thisYear = Convert.ToInt16(thisInst.cboMERRAYear.SelectedItem.ToString());

            if (thisMonth == -1 || thisYear == -1)
                return;

            double[] interpWR = thisMERRA.Calc_Wind_Rose(thisMonth, thisYear, thisInst.UTM_conversions);

            NRadarChart WR_Chart = new NRadarChart();
            NLabel Chart_Title = new NLabel();
            Chart_Title.DockMode = PanelDockMode.Top;

            thisInst.chtMERRA_WR.Panels.Clear();
            thisInst.chtMERRA_WR.Charts.Clear();

            if (thisMERRA.GotWindTS(thisInst.UTM_conversions) == false)
                return;

            // Chart_Title.Text = "Wind Rose at Lat: " + Math.Round(thisMERRA.interpData.Coords.Lat, 3).ToString() + ", Long: " + Math.Round(thisMERRA.interpData.Coords.Lon, 3).ToString();
            // thisInst.chtMERRA_WR.Panels.Add(Chart_Title);
            thisInst.chtMERRA_WR.Charts.Add(WR_Chart);

            WR_Chart.Wall(ChartWallType.Radar).BorderStyle.Color = Color.White;
            WR_Chart.Projection.SetPredefinedProjection(PredefinedProjection.Orthogonal);            
            AddAxisToRadarPlot(ref WR_Chart, thisInst.metList.numWD, true, 10);                

            NRadarAreaSeries WR_Series = new NRadarAreaSeries();
            WR_Chart.Series.Add(WR_Series);
            WR_Series.Name = "windRose";

            for (int i = 0; i < interpWR.Length; i++)
            {
                int This_Index = 0;

                if (i <= interpWR.Length / 2)
                    This_Index = 8 - i;
                else
                    This_Index = 24 - i;

                WR_Series.Values.InsertValue(i, interpWR[This_Index]);
            }

            WR_Series.DataLabelStyle.Visible = false;

            WR_Series.MarkerStyle.AutoDepth = true;
            WR_Series.MarkerStyle.Width = new NLength(1.5f, NRelativeUnit.ParentPercentage);
            WR_Series.MarkerStyle.Height = new NLength(1.5f, NRelativeUnit.ParentPercentage);

            thisInst.chtMERRA_WR.Refresh();

        }

        public void MERRA_TAB(Continuum thisInst)
        {
            MERRA_AnnualTableAndPlot(thisInst);
            MERRA_MonthlyTableAndPlot(thisInst);
            MERRA_Textboxes(thisInst);
            MERRA_WindRosePlot(thisInst);

            // MERRA2 tab
            MERRA thisMERRA = thisInst.GetSelectedMERRA();
            if (thisMERRA.GotWindTS(thisInst.UTM_conversions))
                thisInst.btn_Import_MERRA.BackColor = Color.Green;
            else if (thisMERRA.GotWindTS(thisInst.UTM_conversions) == false)
                thisInst.btn_Import_MERRA.BackColor = Color.LightCoral;
            else
                thisInst.btn_Import_MERRA.BackColor = Color.Gray;
        }

        public void SiteSuitabilityTAB(Continuum thisInst)
        {
            // Updates the site suitability tab
            
            if (thisInst.cboIcingYear.Items.Count == 0)
                IcingYearsDropDown(thisInst);                           

            if (thisInst.cboSiteSuitabilitySelectPlot.Items.Count == 0) 
                SiteSuitabilityDropdown(thisInst, null);                

            string selectedSiteSuitability = "";
            SiteSuitabilityVisibility(thisInst); // updates dropdown menus and plot/table visibility based on selected model

            thisInst.okToUpdate = false;
            thisInst.txtNumIceThrowsPerDay.Text = thisInst.siteSuitability.iceThrowsPerIceDay.ToString();
            thisInst.txtNumIceDays.Text = thisInst.siteSuitability.numIceDaysPerYear.ToString();
            thisInst.txtTurbineNoise.Text = Math.Round(thisInst.siteSuitability.turbineSound, 1).ToString();
            
            if (thisInst.cboIceDistORIceHisto.SelectedItem == null)
                thisInst.cboIceDistORIceHisto.SelectedIndex = 0;

            thisInst.okToUpdate = true;
            
            thisInst.chtSiteSuitability.Charts[0].Enable3D = true;
            thisInst.chtSiteSuitability.Charts[0].Width = 70f;
            thisInst.chtSiteSuitability.Charts[0].Depth = 70f;
            thisInst.chtSiteSuitability.Charts[0].Height = 0.001f;

            try
            {
                selectedSiteSuitability = thisInst.cboSiteSuitabilitySelectPlot.SelectedItem.ToString();
            }
            catch
            {
                thisInst.chtSiteSuitability.Refresh();
                return;
            }

            if (selectedSiteSuitability == "" && thisInst.cboSiteSuitabilitySelectPlot.Items.Count == 0)
            {
                thisInst.chtSiteSuitability.Refresh();
                return;
            }
            else if ((selectedSiteSuitability == "" && thisInst.cboSiteSuitabilitySelectPlot.Items.Count > 0))
            {
                thisInst.okToUpdate = false;
                thisInst.cboSiteSuitabilitySelectPlot.SelectedIndex = 0;
                selectedSiteSuitability = thisInst.cboSiteSuitabilitySelectPlot.SelectedItem.ToString();
                thisInst.okToUpdate = true;
            }

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

        public void Monthly_TAB(Continuum thisInst)
        {
            // Updates the plots and tables on the Turbine Monthly Analysis tab 
            TurbineYearlyPlotAndTable(thisInst);
            TurbineMonthlyPlotAndTable(thisInst);
        }

        public void TurbineYearlyPlotAndTable(Continuum thisInst)
        {
            // Updates the yearly table and plot on Monthly Analysis tab
            thisInst.lstYearlyTurbine.Items.Clear();

            Turbine thisTurb = thisInst.GetSelectedTurbine("Monthly");

            NChartControl yearlyChart = thisInst.chtTurbineYearly;
            yearlyChart.Charts[0].Series.Clear();
            yearlyChart.Charts[0].Width = 400;
            yearlyChart.Charts[0].Height = 200;
            yearlyChart.Labels.Clear();
            yearlyChart.Controller.Tools.Clear();
            yearlyChart.Legends[0].Visible = false;

            if (thisTurb.AvgWSEst_Count == 0 || thisInst.turbineList.genTimeSeries == false)
            {
                yearlyChart.Refresh();
                return;
            }

            CheckedListBox.CheckedItemCollection theseParams = thisInst.chkSelectedTurbineParam.CheckedItems;
            bool plotWS = false;
            NLineSeries WS_Series = new NLineSeries();
            bool plotGross = false;
            NLineSeries Gross_Series = new NLineSeries();
            bool plotNet = false;
            NLineSeries Net_Series = new NLineSeries();
            bool plotWake = false;
            NLineSeries Wake_Series = new NLineSeries();
            bool plotDiff = false;
            NLineSeries Diff_Series = new NLineSeries();

            foreach (var n in theseParams)
            {
                if (n.ToString() == "Avg WS")
                {
                    plotWS = true;
                    AddSeriesToLinePlot(ref yearlyChart, "Avg WS", 0);
                }

                if (n.ToString() == "Gross AEP")
                {
                    plotGross = true;
                    AddSeriesToLinePlot(ref yearlyChart, "Gross AEP", 25);
                }

                if (n.ToString() == "Net AEP")
                {
                    plotNet = true;
                    AddSeriesToLinePlot(ref yearlyChart, "Net AEP", 0);
                }

                if (n.ToString() == "Wake Loss")
                {
                    plotWake = true;
                    AddSeriesToLinePlot(ref yearlyChart, "Wake Loss", 0);
                }

                if (n.ToString() == "% Diff")
                {
                    plotDiff = true;
                    AddSeriesToLinePlot(ref yearlyChart, "% Diff", 0);
                }
            }

            bool needSecondYAxis = false;
            string YAxisTitle = "";

            if (theseParams.Count == 0)
            {
                yearlyChart.Refresh();
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

            NTooltipTool tooltip = new NTooltipTool();
            yearlyChart.Controller.Tools.Add(tooltip);
            yearlyChart.Labels.AddHeader("Yearly Trends at " + thisTurb.name);

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(yearlyChart.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Year";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(yearlyChart.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = YAxisTitle;

            if (needSecondYAxis == true)
            {
                yearlyChart.Charts[0].Axis(StandardAxis.SecondaryY).Visible = true;
                NStandardScaleConfigurator scaleConfiguratorSecondY = (NStandardScaleConfigurator)(yearlyChart.Charts[0].Axis(StandardAxis.SecondaryY).ScaleConfigurator);
                scaleConfiguratorSecondY.Title.Text = "Net AEP";
            }

            Wake_Model thisWakeModel = null;
            if (thisInst.wakeModelList.NumWakeModels > 0 && thisInst.cboMonthlyWakeModel.Items.Count != 0)
            {
                string wakeModelString = thisInst.cboMonthlyWakeModel.SelectedItem.ToString();
                thisWakeModel = thisInst.wakeModelList.GetWakeModelFromString(wakeModelString);
            }
       /*     else
            {
                yearlyChart.Refresh();
                return;
            }
          */      
            
            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Monthly");

            int firstYear = thisInst.merraList.startDate.Year;
            int lastYear = thisInst.merraList.endDate.Year;

            for (int i = firstYear; i <= lastYear; i++)
            {
                double avgWS = thisTurb.CalcYearlyValue(i, "Avg WS", null, new TurbineCollection.PowerCurve());
                double grossEnergy = thisTurb.CalcYearlyValue(i, "Gross AEP", thisWakeModel, powerCurve);
                double netEnergy = thisTurb.CalcYearlyValue(i, "Net AEP", thisWakeModel, powerCurve);
                double thisWakeLoss = 0;

                // Add values to list
                objListItem = thisInst.lstYearlyTurbine.Items.Add(i.ToString());

                if (avgWS != 0)
                {
                    objListItem.SubItems.Add(Math.Round(avgWS, 2).ToString());

                    if (plotWS == true)
                    {  // Add values to plot series
                        int seriesInd = GetSeriesIndex(yearlyChart, "Avg WS");
                        WS_Series = (NLineSeries)yearlyChart.Charts[0].Series[seriesInd];

                        WS_Series.XValues.Add(i);
                        WS_Series.Values.Add(avgWS);
                    }
                }

                if (grossEnergy != 0)
                {
                    objListItem.SubItems.Add(Math.Round(grossEnergy, 0).ToString());

                    if (plotGross == true)
                    {
                        int seriesInd = GetSeriesIndex(yearlyChart, "Gross AEP");
                        Gross_Series = (NLineSeries)yearlyChart.Charts[0].Series[seriesInd];
                        Gross_Series.XValues.Add(i);
                        Gross_Series.Values.Add(grossEnergy);

                        if (needSecondYAxis)
                        {
                            Gross_Series.DisplayOnAxis(StandardAxis.PrimaryY, false);
                            Gross_Series.DisplayOnAxis(StandardAxis.SecondaryY, true);
                        }
                        else
                        {
                            Gross_Series.DisplayOnAxis(StandardAxis.PrimaryY, true);
                            Gross_Series.DisplayOnAxis(StandardAxis.SecondaryY, false);
                        }
                    }
                }

                if (netEnergy != 0)
                {
                    objListItem.SubItems.Add(Math.Round(netEnergy, 0).ToString());

                    if (plotNet == true)
                    {
                        int seriesInd = GetSeriesIndex(yearlyChart, "Net AEP");
                        Net_Series = (NLineSeries)yearlyChart.Charts[0].Series[seriesInd];
                        Net_Series.XValues.Add(i);
                        Net_Series.Values.Add(netEnergy);

                        if (needSecondYAxis)
                        {
                            Net_Series.DisplayOnAxis(StandardAxis.PrimaryY, false);
                            Net_Series.DisplayOnAxis(StandardAxis.SecondaryY, true);
                        }
                        else
                        {
                            Net_Series.DisplayOnAxis(StandardAxis.PrimaryY, true);
                            Net_Series.DisplayOnAxis(StandardAxis.SecondaryY, false);
                        }
                    }
                }
                else
                    objListItem.SubItems.Add("");

                if (grossEnergy != 0 && netEnergy != 0)
                {
                    double otherLoss = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
                    thisWakeLoss = (grossEnergy - netEnergy / otherLoss) / grossEnergy;
                    objListItem.SubItems.Add(Math.Round(thisWakeLoss, 4).ToString("P"));

                    if (plotWake == true)
                    {
                        int seriesInd = GetSeriesIndex(yearlyChart, "Wake Loss");
                        Wake_Series = (NLineSeries)yearlyChart.Charts[0].Series[seriesInd];
                        Wake_Series.XValues.Add(i);
                        Wake_Series.Values.Add(100 * thisWakeLoss);
                    }
                }
                else
                    objListItem.SubItems.Add("");

                if (netEnergy == 0 && grossEnergy != 0) // no net estimates so calculate % diff in gross energy
                {
                    Turbine.Gross_Energy_Est thisGross = thisTurb.GetGrossEnergyEst(powerCurve);
                    double percDiff = (grossEnergy - thisGross.AEP) / thisGross.AEP;
                    objListItem.SubItems.Add(Math.Round(percDiff, 4).ToString("P"));

                    if (plotDiff == true)
                    {
                        int seriesInd = GetSeriesIndex(yearlyChart, "% Diff");
                        Diff_Series = (NLineSeries)yearlyChart.Charts[0].Series[seriesInd];
                        Diff_Series.XValues.Add(i);
                        Diff_Series.Values.Add(percDiff);
                    }
                }
                else if (netEnergy != 0) // Calculate % Diff in net energy
                {
                    Turbine.Net_Energy_Est thisNet = thisTurb.GetNetEnergyEst(thisWakeModel);
                    double percDiff = (netEnergy - thisNet.AEP) / thisNet.AEP;
                    objListItem.SubItems.Add(Math.Round(percDiff, 4).ToString("P"));

                    if (plotDiff == true)
                    {
                        int seriesInd = GetSeriesIndex(yearlyChart, "% Diff");
                        Diff_Series = (NLineSeries)yearlyChart.Charts[0].Series[seriesInd];
                        Diff_Series.XValues.Add(i);
                        Diff_Series.Values.Add(percDiff);
                    }
                }


            }

            yearlyChart.Refresh();
        }

        public void TurbineMonthlyPlotAndTable(Continuum thisInst)
        {
            // Updates the yearly table and plot on Monthly Analysis tab
            thisInst.lstMonthlyTurbine.Items.Clear();

            Turbine thisTurb = thisInst.GetSelectedTurbine("Monthly");

            NChartControl monthlyChart = thisInst.chtTurbineMonthly;
            monthlyChart.Charts[0].Series.Clear();
            monthlyChart.Charts[0].Width = 400;
            monthlyChart.Charts[0].Height = 200;
            monthlyChart.Labels.Clear();
            monthlyChart.Controller.Tools.Clear();
            monthlyChart.Legends[0].Visible = false;

            if (thisTurb.AvgWSEst_Count == 0 || thisInst.turbineList.genTimeSeries == false)
            {
                monthlyChart.Refresh();
                return;
            }

            CheckedListBox.CheckedItemCollection theseParams = thisInst.chkSelectedTurbineParam.CheckedItems;
            bool plotWS = false;
            NLineSeries WS_Series = new NLineSeries();
            bool plotGross = false;
            NLineSeries Gross_Series = new NLineSeries();
            bool plotNet = false;
            NLineSeries Net_Series = new NLineSeries();
            bool plotWake = false;
            NLineSeries Wake_Series = new NLineSeries();
            bool plotDiff = false;
            NLineSeries Diff_Series = new NLineSeries();

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
                monthlyChart.Refresh();
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

            NTooltipTool tooltip = new NTooltipTool();
            monthlyChart.Controller.Tools.Add(tooltip);
            monthlyChart.Labels.AddHeader("Monthly Trends at " + thisTurb.name);

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(monthlyChart.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Month";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(monthlyChart.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = YAxisTitle;

            if (needSecondYAxis == true)
            {
                monthlyChart.Charts[0].Axis(StandardAxis.SecondaryY).Visible = true;                
                NStandardScaleConfigurator scaleConfiguratorSecondY = (NStandardScaleConfigurator)(monthlyChart.Charts[0].Axis(StandardAxis.SecondaryY).ScaleConfigurator);
                scaleConfiguratorSecondY.Title.Text = "AEP, MWh";
            }

            Wake_Model thisWakeModel = null;
            if (thisInst.wakeModelList.NumWakeModels > 0 && thisInst.cboMonthlyWakeModel.Items.Count != 0)
            {
                string wakeModelString = thisInst.cboMonthlyWakeModel.SelectedItem.ToString();
                thisWakeModel = thisInst.wakeModelList.GetWakeModelFromString(wakeModelString);
            }
                       
            TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Monthly");

            Turbine.Avg_Est thisAvgEst = thisTurb.GetAvgWS_Est(null, new TurbineCollection.PowerCurve()); // Want free-stream wind speeds
            Turbine.Gross_Energy_Est thisGrossEst = thisTurb.GetGrossEnergyEst(powerCurve);
            Turbine.Net_Energy_Est thisNetEst = thisTurb.GetNetEnergyEst(thisWakeModel);

            if (thisAvgEst.FS_MonthlyVals == null)
                return;

            int numYears = thisInst.chkYears_Monthly.CheckedItems.Count;

            int lastYear = 0;

            for (int i = 0; i < thisAvgEst.FS_MonthlyVals.Length; i++)
            {
                int seriesInd = 0;
                bool showThisYear = false;

                for (int j = 0; j < numYears; j++)
                    if (thisInst.chkYears_Monthly.CheckedItems[j].ToString() == thisAvgEst.FS_MonthlyVals[i].year.ToString())
                        showThisYear = true;

                if (showThisYear == true)
                {
                    // Add values to list
                    objListItem = thisInst.lstMonthlyTurbine.Items.Add(thisAvgEst.FS_MonthlyVals[i].month.ToString());
                    objListItem.SubItems.Add(thisAvgEst.FS_MonthlyVals[i].year.ToString());

                    objListItem.SubItems.Add(Math.Round(thisAvgEst.FS_MonthlyVals[i].avgWS, 2).ToString());

                    if (plotWS == true)
                    {  // Add values to plot series
                        string seriesName = "Avg WS " + thisAvgEst.FS_MonthlyVals[i].year.ToString();
                        
                        if (lastYear != thisAvgEst.FS_MonthlyVals[i].year)
                            seriesInd = AddSeriesToLinePlot(ref monthlyChart, seriesName, i);

                        seriesInd = GetSeriesIndex(monthlyChart, seriesName);
                        WS_Series = (NLineSeries)monthlyChart.Charts[0].Series[seriesInd];
                        WS_Series.XValues.Add(thisAvgEst.FS_MonthlyVals[i].month);
                        WS_Series.Values.Add(thisAvgEst.FS_MonthlyVals[i].avgWS);
                    }


                    if (thisGrossEst.AEP != 0)
                    {
                        objListItem.SubItems.Add(Math.Round(thisGrossEst.monthlyVals[i].energyProd, 0).ToString());

                        if (plotGross == true)
                        {
                            string seriesName = "Gross Energy " + thisAvgEst.FS_MonthlyVals[i].year.ToString();

                            if (lastYear != thisAvgEst.FS_MonthlyVals[i].year)
                                seriesInd = AddSeriesToLinePlot(ref monthlyChart, seriesName, i);

                            seriesInd = GetSeriesIndex(monthlyChart, seriesName);
                            Gross_Series = (NLineSeries)monthlyChart.Charts[0].Series[seriesInd];
                            Gross_Series.XValues.Add(thisAvgEst.FS_MonthlyVals[i].month);
                            Gross_Series.Values.Add(thisGrossEst.monthlyVals[i].energyProd);

                            if (needSecondYAxis)
                            {
                                Gross_Series.DisplayOnAxis(StandardAxis.PrimaryY, false);
                                Gross_Series.DisplayOnAxis(StandardAxis.SecondaryY, true);
                            }
                            else
                            {
                                Gross_Series.DisplayOnAxis(StandardAxis.PrimaryY, true);
                                Gross_Series.DisplayOnAxis(StandardAxis.SecondaryY, false);
                            }
                        }
                    }

                    if (thisNetEst.AEP != 0)
                    {
                        objListItem.SubItems.Add(Math.Round(thisNetEst.monthlyVals[i].energyProd, 0).ToString());

                        if (plotNet == true)
                        {
                            string seriesName = "Net Energy " + thisAvgEst.FS_MonthlyVals[i].year.ToString();

                            if (lastYear != thisAvgEst.FS_MonthlyVals[i].year)
                                seriesInd = AddSeriesToLinePlot(ref monthlyChart, seriesName, i);

                            seriesInd = GetSeriesIndex(monthlyChart, seriesName);
                            Net_Series = (NLineSeries)monthlyChart.Charts[0].Series[seriesInd];
                            Net_Series.XValues.Add(thisAvgEst.FS_MonthlyVals[i].month);
                            Net_Series.Values.Add(thisNetEst.monthlyVals[i].energyProd);

                            if (needSecondYAxis)
                            {
                                Net_Series.DisplayOnAxis(StandardAxis.PrimaryY, false);
                                Net_Series.DisplayOnAxis(StandardAxis.SecondaryY, true);
                            }
                            else
                            {
                                Net_Series.DisplayOnAxis(StandardAxis.PrimaryY, true);
                                Net_Series.DisplayOnAxis(StandardAxis.SecondaryY, false);
                            }
                        }
                    }
                    else
                        objListItem.SubItems.Add("");

                    if (thisGrossEst.AEP != 0 && thisNetEst.AEP != 0)
                    {
                        if (thisGrossEst.monthlyVals[i].energyProd != 0)
                        {
                            double otherLoss = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
                            double thisWakeLoss = (thisGrossEst.monthlyVals[i].energyProd - thisNetEst.monthlyVals[i].energyProd / otherLoss) / thisGrossEst.monthlyVals[i].energyProd;
                            objListItem.SubItems.Add(Math.Round(thisWakeLoss, 4).ToString("P"));

                            if (plotWake == true)
                            {
                                string seriesName = "Wake Loss " + thisAvgEst.FS_MonthlyVals[i].year.ToString();

                                if (lastYear != thisAvgEst.FS_MonthlyVals[i].year)
                                    seriesInd = AddSeriesToLinePlot(ref monthlyChart, seriesName, i);

                                seriesInd = GetSeriesIndex(monthlyChart, seriesName);
                                Wake_Series = (NLineSeries)monthlyChart.Charts[0].Series[seriesInd];
                                Wake_Series.XValues.Add(thisAvgEst.FS_MonthlyVals[i].month);
                                Wake_Series.Values.Add(100 * thisWakeLoss);
                            }
                        }
                        else
                            objListItem.SubItems.Add("");

                    }
                    else
                        objListItem.SubItems.Add("");

                    if (thisGrossEst.AEP != 0 && thisNetEst.AEP == 0) // no net estimates so calculate % diff in gross energy
                    {
                        double thisLTValue = thisTurb.CalcLT_MonthlyValue("Gross AEP", thisAvgEst.FS_MonthlyVals[i].month, null, powerCurve);
                        double percDiff = (thisGrossEst.monthlyVals[i].energyProd - thisLTValue) / thisLTValue;
                        objListItem.SubItems.Add(Math.Round(percDiff, 4).ToString("P"));

                        if (plotDiff == true)
                        {
                            string seriesName = "% Diff " + thisAvgEst.FS_MonthlyVals[i].year.ToString();

                            if (lastYear != thisAvgEst.FS_MonthlyVals[i].year)
                                seriesInd = AddSeriesToLinePlot(ref monthlyChart, seriesName, i);

                            seriesInd = GetSeriesIndex(monthlyChart, seriesName);
                            Diff_Series = (NLineSeries)monthlyChart.Charts[0].Series[seriesInd];
                            Diff_Series.XValues.Add(thisAvgEst.FS_MonthlyVals[i].month);
                            Diff_Series.Values.Add(percDiff);
                        }
                    }
                    else if (thisNetEst.AEP != 0) // Calculate % Diff in net energy
                    {
                        double thisLTValue = thisTurb.CalcLT_MonthlyValue("Gross AEP", thisAvgEst.FS_MonthlyVals[i].month, thisWakeModel, powerCurve);
                        double percDiff = (thisNetEst.monthlyVals[i].energyProd - thisLTValue) / thisLTValue;
                        objListItem.SubItems.Add(Math.Round(percDiff, 4).ToString("P"));

                        if (plotDiff == true)
                        {
                            string seriesName = "% Diff " + thisAvgEst.FS_MonthlyVals[i].year.ToString();

                            if (lastYear != thisAvgEst.FS_MonthlyVals[i].year)
                                seriesInd = AddSeriesToLinePlot(ref monthlyChart, seriesName, i);

                            seriesInd = GetSeriesIndex(monthlyChart, seriesName);
                            Diff_Series = (NLineSeries)monthlyChart.Charts[0].Series[seriesInd];
                            Diff_Series.XValues.Add(thisAvgEst.FS_MonthlyVals[i].month);
                            Diff_Series.Values.Add(percDiff);
                        }
                    }
                }
                lastYear = thisAvgEst.FS_MonthlyVals[i].year;
            }

            // Now add LT Ests (if checked)
            bool showLT = false;
            for (int i = 0; i < thisInst.chkYears_Monthly.CheckedItems.Count; i++)
                if (thisInst.chkYears_Monthly.CheckedItems[i].ToString() == "LT Avg")
                    showLT = true;

            if (showLT == true)
            {
                // Add LT Estimates to list and plot
                for (int i = 1; i <= 12; i++)
                {
                    objListItem = thisInst.lstMonthlyTurbine.Items.Add(i.ToString());
                    objListItem.SubItems.Add("LT Avg");

                    double LT_AvgWS = thisTurb.CalcLT_MonthlyValue("Avg WS", i, null, powerCurve);
                    objListItem.SubItems.Add(Math.Round(LT_AvgWS, 2).ToString());

                    if (plotWS == true)
                    {  // Add values to plot series
                        string seriesName = "LT Avg WS";

                        if (i == 1)
                            AddSeriesToLinePlot(ref monthlyChart, seriesName, i);

                        int seriesInd = GetSeriesIndex(monthlyChart, seriesName);
                        WS_Series = (NLineSeries)monthlyChart.Charts[0].Series[seriesInd];
                        WS_Series.XValues.Add(i);
                        WS_Series.Values.Add(LT_AvgWS);
                    }

                    if (thisGrossEst.AEP != 0)
                    {
                        double LT_GrossAEP = thisTurb.CalcLT_MonthlyValue("Gross AEP", i, null, powerCurve);
                        objListItem.SubItems.Add(Math.Round(LT_GrossAEP, 0).ToString());

                        if (plotGross == true)
                        {
                            string seriesName = "LT Gross Energy";

                            if (i == 1)
                                AddSeriesToLinePlot(ref monthlyChart, seriesName, i);

                            int seriesInd = GetSeriesIndex(monthlyChart, seriesName);
                            Gross_Series = (NLineSeries)monthlyChart.Charts[0].Series[seriesInd];
                            Gross_Series.XValues.Add(i);
                            Gross_Series.Values.Add(LT_GrossAEP);

                            if (needSecondYAxis)
                            {
                                Gross_Series.DisplayOnAxis(StandardAxis.PrimaryY, false);
                                Gross_Series.DisplayOnAxis(StandardAxis.SecondaryY, true);
                            }
                            else
                            {
                                Gross_Series.DisplayOnAxis(StandardAxis.PrimaryY, true);
                                Gross_Series.DisplayOnAxis(StandardAxis.SecondaryY, false);
                            }
                        }
                    }

                    if (thisNetEst.AEP != 0)
                    {
                        double LT_NetAEP = thisTurb.CalcLT_MonthlyValue("Net AEP", i, thisWakeModel, powerCurve);
                        objListItem.SubItems.Add(Math.Round(LT_NetAEP, 0).ToString());

                        if (plotNet == true)
                        {
                            string seriesName = "LT Net Energy";

                            if (i == 1)
                                AddSeriesToLinePlot(ref monthlyChart, seriesName, i);

                            int seriesInd = GetSeriesIndex(monthlyChart, seriesName);
                            Net_Series = (NLineSeries)monthlyChart.Charts[0].Series[seriesInd];
                            Net_Series.XValues.Add(i);
                            Net_Series.Values.Add(LT_NetAEP);

                            if (needSecondYAxis)
                            {
                                Net_Series.DisplayOnAxis(StandardAxis.PrimaryY, false);
                                Net_Series.DisplayOnAxis(StandardAxis.SecondaryY, true);
                            }
                            else
                            {
                                Net_Series.DisplayOnAxis(StandardAxis.PrimaryY, true);
                                Net_Series.DisplayOnAxis(StandardAxis.SecondaryY, false);
                            }
                        }
                    }
                    else
                        objListItem.SubItems.Add("");

                    if (thisGrossEst.AEP != 0 && thisNetEst.AEP != 0)
                    {
                        double otherLoss = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
                        double LT_GrossAEP = thisTurb.CalcLT_MonthlyValue("Gross AEP", i, null, powerCurve);
                        double LT_NetAEP = thisTurb.CalcLT_MonthlyValue("Net AEP", i, thisWakeModel, powerCurve);
                        if (LT_NetAEP == 0) return;
                        double thisWakeLoss = (LT_GrossAEP - LT_NetAEP / otherLoss) / LT_GrossAEP;
                        objListItem.SubItems.Add(Math.Round(thisWakeLoss, 4).ToString("P"));

                        if (plotWake == true)
                        {
                            string seriesName = "LT Wake Loss";

                            if (lastYear != thisAvgEst.FS_MonthlyVals[i].year)
                                AddSeriesToLinePlot(ref monthlyChart, seriesName, i);

                            int seriesInd = GetSeriesIndex(monthlyChart, seriesName);
                            Wake_Series = (NLineSeries)monthlyChart.Charts[0].Series[seriesInd];
                            Wake_Series.XValues.Add(i);
                            Wake_Series.Values.Add(100 * thisWakeLoss);
                        }

                    }
                    else
                        objListItem.SubItems.Add("");
                }
            }

            monthlyChart.Refresh();
        }

        public int GetSeriesIndex(NChartControl thisPlot, string seriesName)
        {
            int thisInd = 0;

            for (int i = 0; i < thisPlot.Charts[0].Series.Count; i++)
            {
                if (thisPlot.Charts[0].Series[i].Name == seriesName)
                {
                    thisInd = i;
                    break;
                }
            }

            return thisInd;
        }

        public int AddSeriesToLinePlot(ref NChartControl thisPlot, string seriesName, int ColorInt)
        {
            // Adds a series to a line plot and returns the index
            NLineSeries thisSeries = new NLineSeries();
            thisSeries.DataLabelStyle.Visible = false;
            thisSeries.Name = seriesName;
            thisPlot.Charts[0].Series.Add(thisSeries);
            thisSeries.BorderStyle.Color = GetMetOrTurbColor(ColorInt);
            thisSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            thisSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(seriesName);
            thisSeries.UseXValues = true;

            int thisInd = thisPlot.Charts[0].Series.Count - 1;

            return thisInd;
        }

        public void ZoneList(Continuum thisInst)
        {
            thisInst.okToUpdate = false;

            // Updates zone list and zone dropdown on Site Suitability tab            
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

        public void IceThrowSurfacePlot(Continuum thisInst)
        {
            // Based on selected plot parameter, creates a surface plot showing either ice throw, shadow flicker, or sound surrounding turbines and specified zones

            NChart siteMap = thisInst.chtSiteSuitability.Charts[0];
            siteMap.Projection.SetPredefinedProjection(PredefinedProjection.OrthogonalTop);
            siteMap.Series.Clear();
            siteMap.Enable3D = true;
            siteMap.Width = 70f;
            siteMap.Depth = 70f;
            siteMap.Height = 0.001f;
            thisInst.chtSiteSuitability.Legends.Clear();
            thisInst.chtSiteSuitability.Labels.Clear();
            thisInst.chtSiteSuitability.Labels.AddHeader("Ice Hits over Annual Period");

            if (thisInst.siteSuitability.yearlyIceHits.Length == 0)
            {
                thisInst.chtSiteSuitability.Refresh();
                return;
            }

            // Create grid that covers turbines +/- 3 km
            NMeshSurfaceSeries zoneSurface = new NMeshSurfaceSeries();
            zoneSurface.FillMode = SurfaceFillMode.Zone;
            zoneSurface.FrameMode = SurfaceFrameMode.None;
            zoneSurface.DrawFlat = true;
            zoneSurface.ValueFormatter.FormatSpecifier = "0.00";

            thisInst.chtSiteSuitability.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            thisInst.chtSiteSuitability.Controller.Tools.Add(tooltip);

            zoneSurface.Palette.Clear();
            zoneSurface.Palette.SmoothPalette = false;
            zoneSurface.Palette.HasCustomMin = true;
            zoneSurface.Palette.CustomMin = 1;
            zoneSurface.Palette.Mode = PaletteMode.Custom;
            zoneSurface.Palette.PaletteSteps = 3;
            zoneSurface.Palette.Add(1, Color.Red);
            zoneSurface.Palette.Add(2, Color.Blue);
            zoneSurface.Palette.Add(3, Color.Black);
            

            if (thisInst.siteSuitability.mapMinBounds.UTMX == 0)
                thisInst.siteSuitability.FindShadowMapBounds(thisInst, true);

            TopoInfo.UTM_X_Y mapMinBounds = thisInst.siteSuitability.mapMinBounds;
            TopoInfo.UTM_X_Y mapMaxBounds = thisInst.siteSuitability.mapMaxBounds;

            int surfaceReso = 5;
            int numX = ((int)mapMaxBounds.UTMX - (int)mapMinBounds.UTMX) / surfaceReso + 1;
            int numY = ((int)mapMaxBounds.UTMY - (int)mapMinBounds.UTMY) / surfaceReso + 1;

            zoneSurface.Data.SetGridSize(numX, numY);

            for (int i = 0; i < numX; i++)
                for (int j = 0; j < numY; j++)
                    zoneSurface.Data.SetValue(i, j, 1, (int)(mapMinBounds.UTMX + i * surfaceReso), (int)(mapMinBounds.UTMY + j * surfaceReso));

            siteMap.Series.Add(zoneSurface);

            NPointSeries iceThrowSeries = new NPointSeries();
            iceThrowSeries.Name = "Ice";
            iceThrowSeries.DataLabelStyle.Visible = false;
            iceThrowSeries.UseXValues = true;
            iceThrowSeries.UseZValues = true;
            iceThrowSeries.Visible = true;
            
            iceThrowSeries.FillStyle = new NColorFillStyle(Color.Silver);
            iceThrowSeries.Size = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            siteMap.Series.Add(iceThrowSeries);

            if (thisInst.turbineList.TurbineCount == 0)
                return;

            int yearToShow = Convert.ToInt16(thisInst.cboIcingYear.SelectedItem.ToString());

            for (int i = 0; i < thisInst.siteSuitability.yearlyIceHits[yearToShow - 1].iceHits.Length; i++)
            {
                iceThrowSeries.XValues.Add(thisInst.siteSuitability.yearlyIceHits[yearToShow - 1].iceHits[i].thisX);
                iceThrowSeries.ZValues.Add(thisInst.siteSuitability.yearlyIceHits[yearToShow - 1].iceHits[i].thisZ);                            

                iceThrowSeries.Values.Add(2);
            }

            SiteSuitabilitySurfacePlotLabels(thisInst, -999, false);
            thisInst.chtSiteSuitability.Refresh();
            
        }                

        public void SiteSuitabilitySurfacePlotLabels(Continuum thisInst, int changedIndex, bool changedItemChecked)
        {
            // Create point series for zones and change zoneSurface to show zone dimensions
            int numZones = thisInst.siteSuitability.GetNumZones();
            NPointSeries[] zones = new NPointSeries[numZones];
            int numSeries = thisInst.chtSiteSuitability.Charts[0].Series.Count;
            thisInst.chtSiteSuitability.Charts[0].Enable3D = true;
            thisInst.chtSiteSuitability.Charts[0].Projection.SetPredefinedProjection(PredefinedProjection.OrthogonalTop);

            for (int i = 0; i < numSeries; i++)
            {
                SeriesType seriesType = thisInst.chtSiteSuitability.Charts[0].Series[i].GetSeriesType();
                string seriesName = thisInst.chtSiteSuitability.Charts[0].Series[i].Name;

                if (seriesType != SeriesType.MeshSurface && seriesName != "Ice")
                {
                    thisInst.chtSiteSuitability.Charts[0].Series.Remove(thisInst.chtSiteSuitability.Charts[0].Series[i]);
                    i--;
                }

                numSeries = thisInst.chtSiteSuitability.Charts[0].Series.Count;
            }

            for (int i = 0; i < numZones; i++)
            {
                bool isChecked = false;
                                
                for (int j = 0; j < thisInst.lstZones.CheckedItems.Count; j++)
                    if (thisInst.lstZones.CheckedItems[j].Text == thisInst.siteSuitability.zones[i].name)
                        isChecked = true;

                if (i == changedIndex)
                    isChecked = changedItemChecked;

                if (isChecked == true)
                {
                    zones[i] = new NPointSeries();
                    zones[i].UseXValues = true;
                    zones[i].UseZValues = true;
                    zones[i].Values.Add(2);
                    zones[i].DataLabelStyle.Visible = false;
                    zones[i].Visible = true;
                    zones[i].FillStyle = new NColorFillStyle(Color.Blue);
                    zones[i].Size = new NLength(1f, NRelativeUnit.ParentPercentage);                    
                    zones[i].Legend.Mode = SeriesLegendMode.None;
                    zones[i].InteractivityStyle.Tooltip = new NTooltipAttribute("Zone " + (i + 1));

                    // Convert lat/long to UTM
                    UTM_conversion.UTM_coords theseUTM = thisInst.UTM_conversions.LLtoUTM(thisInst.siteSuitability.zones[i].latitude, thisInst.siteSuitability.zones[i].longitude);
                    zones[i].XValues.Add(theseUTM.UTMEasting);
                    zones[i].ZValues.Add(theseUTM.UTMNorthing);
                    thisInst.chtSiteSuitability.Charts[0].Series.Add(zones[i]);

                    /*        // Change color of zoneSurface TO DO: DECIDE WHETHER TO USE THIS OR JUST A POINT SERIES FOR ZONES
                            int zoneMinX = Convert.ToInt32(theseUTM.UTMEasting - thisInst.siteSuitability.zones[i].xSize / 2);
                            int zoneMaxX = zoneMinX + thisInst.siteSuitability.zones[i].xSize;
                            int zoneMinY = Convert.ToInt32(theseUTM.UTMNorthing - thisInst.siteSuitability.zones[i].ySize / 2);
                            int zoneMaxY = zoneMinY + thisInst.siteSuitability.zones[i].ySize;

                            int zoneMinXInd = (int)Math.Round((double)(zoneMinX - minX) / surfaceReso, 0);
                            int zoneMaxXInd = (int)Math.Round((double)(zoneMaxX - minX) / surfaceReso, 0);
                            int zoneMinYInd = (int)Math.Round((double)(zoneMinY - minY) / surfaceReso, 0);
                            int zoneMaxYInd = (int)Math.Round((double)(zoneMaxY - minY) / surfaceReso, 0);

                            for (int j = zoneMinXInd; j <= zoneMaxXInd; j++)
                                for (int k = zoneMinYInd; k <= zoneMaxYInd; k++)
                                    zoneSurface.Data.SetValue(j, k, 3, j * surfaceReso + minX, k * surfaceReso + minY);
                                    */
                }
            }

            // Create point series for turbines
            int numTurbs = thisInst.turbineList.TurbineCount;
            NPointSeries[] turbines = new NPointSeries[numTurbs];

            for (int i = 0; i < numTurbs; i++)
            {
                Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                turbines[i] = new NPointSeries();
                turbines[i].UseXValues = true;
                turbines[i].UseZValues = true;
                turbines[i].Values.Add(2);
                turbines[i].DataLabelStyle.Visible = false;
                turbines[i].Visible = true;
                turbines[i].FillStyle = new NColorFillStyle(Color.White);
                turbines[i].BorderStyle = new NStrokeStyle(Color.Black);
                turbines[i].PointShape = PointShape.Sphere;
                turbines[i].Size = new NLength(1, NRelativeUnit.ParentPercentage);                
                turbines[i].Legend.Mode = SeriesLegendMode.None;
                turbines[i].InteractivityStyle.Tooltip = new NTooltipAttribute(thisTurb.name);

                turbines[i].XValues.Add(thisTurb.UTMX);
                turbines[i].ZValues.Add(thisTurb.UTMY);
                thisInst.chtSiteSuitability.Charts[0].Series.Add(turbines[i]);
            }

            thisInst.chtSiteSuitability.Refresh();
            numSeries = thisInst.chtSiteSuitability.Charts[0].Series.Count;
        }

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

        public void ShadowFlickerSurfacePlot(Continuum thisInst)
        {
            // Plots total shadow flicker of selected month and hour at each grid node

            string thisMonthStr = thisInst.cboSiteSuitMonth.SelectedItem.ToString();
            string thisHourStr = thisInst.cboSiteSuitHour.SelectedItem.ToString();

            int monthInd = GetMonthInd(thisMonthStr);
            int hourInd = 100;

            if (thisHourStr != "All")
                hourInd = GetHourFromHourString(thisHourStr);

            // Create grid array with total number of shadow flicker hours            
            SiteSuitability.FlickerGrid[] plotFlicker = thisInst.siteSuitability.GetTotalFlickerHoursByMonthAndHour(monthInd, hourInd);
                       
            NChart siteMap = thisInst.chtSiteSuitability.Charts[0];
            siteMap.Projection.SetPredefinedProjection(PredefinedProjection.OrthogonalTop);
            siteMap.Series.Clear();
            thisInst.chtSiteSuitability.Legends.Clear();
            thisInst.chtSiteSuitability.Labels.Clear();
            thisInst.chtSiteSuitability.Labels.AddHeader("Total Number of Shadow Flicker Hours");

            if (plotFlicker.Length == 0)
            {
                thisInst.chtSiteSuitability.Refresh();                
                return;
            }

            // Create grid that covers turbines +/- 3 km
            NMeshSurfaceSeries flickerSurface = new NMeshSurfaceSeries();
            flickerSurface.FillMode = SurfaceFillMode.Zone;
            flickerSurface.FrameMode = SurfaceFrameMode.None;
            flickerSurface.DrawFlat = true;
            flickerSurface.ValueFormatter.FormatSpecifier = "0";

            flickerSurface.Palette.Clear();
            flickerSurface.Palette.SmoothPalette = false;
            flickerSurface.Palette.HasCustomMin = true;
            flickerSurface.Palette.CustomMin = 0;
            flickerSurface.Palette.Mode = PaletteMode.Custom;
            flickerSurface.Palette.PaletteSteps = 50;

            flickerSurface.Legend.Mode = SeriesLegendMode.SeriesLogic;            
            flickerSurface.Legend.Format = "<zone_begin> - <zone_end>";
            NLegend Map_Legend = new NLegend();
          //  Map_Legend.Size.Width = MeasurementUnits.Scale[1];
            thisInst.chtSiteSuitability.Charts[0].DisplayOnLegend = Map_Legend;
            thisInst.chtSiteSuitability.Panels.Add(Map_Legend);
            Map_Legend.DockMode = PanelDockMode.Right;

            int maxValue = 0;

            for (int i = 0; i < plotFlicker.Length; i++)
                if (plotFlicker[i].flickerStats.totalShadowMinsPerYear > maxValue)
                    maxValue = plotFlicker[i].flickerStats.totalShadowMinsPerYear;

            maxValue = maxValue + 5;

            double intWidth = Math.Round(maxValue / 14.0, 0);

            if (intWidth * 14 < maxValue)
                intWidth++;

            for (int i = 0; i < 15; i++)
                flickerSurface.Palette.Add(Math.Round(i * intWidth, 2), GetRGB_Values((double)i / 15));

            /*      flickerSurface.Legend.Mode = SeriesLegendMode.SeriesLogic;
                  flickerSurface.Legend.Format = "<zone_begin> - <zone_end>";
                  NLegend Map_Legend = new NLegend();
                  thisInst.chtSiteSuitability.Charts[0].DisplayOnLegend = Map_Legend;
                  thisInst.chtSiteSuitability.Panels.Add(Map_Legend);
                  Map_Legend.DockMode = PanelDockMode.Right;
      */
            thisInst.chtSiteSuitability.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            thisInst.chtSiteSuitability.Controller.Tools.Add(tooltip);

            flickerSurface.Data.SetGridSize(thisInst.siteSuitability.numXFlicker, thisInst.siteSuitability.numYFlicker);
            int flickerMapInd = 0;

            for (int i = 0; i < thisInst.siteSuitability.numXFlicker; i++)
                for (int j = 0; j < thisInst.siteSuitability.numYFlicker; j++)
                {
                    flickerSurface.Data.SetValue(i, j, plotFlicker[flickerMapInd].flickerStats.totalShadowMinsPerYear, thisInst.siteSuitability.mapMinBounds.UTMX + thisInst.siteSuitability.flickerGridReso * i,
                        thisInst.siteSuitability.mapMinBounds.UTMY + thisInst.siteSuitability.flickerGridReso * j);
                    flickerMapInd++;
                }

            siteMap.Series.Add(flickerSurface);

            SiteSuitabilitySurfacePlotLabels(thisInst, -999, true);
            thisInst.chtSiteSuitability.Refresh();

            // For debugging and unit tests
      /*      string fileName = "C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\Site Suitability\\Shadow Flicker\\Flicker Map vals.csv";

            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName);
            sw.Write("UTMX, UTMY,");
            for (int i = 1; i <= 12; i++)
                for (int j = 0; j <= 23; j++)
                    sw.Write("Month " + i + " Hour " + j + ",");

            sw.WriteLine();
            int totalNum = thisInst.siteSuitability.numXFlicker * thisInst.siteSuitability.numYFlicker;

            for (int i = 0; i < totalNum; i++)
            {
                sw.Write(thisInst.siteSuitability.flickerMap[i].UTMX.ToString() + "," + thisInst.siteSuitability.flickerMap[i].UTMY.ToString() + ",");

                for (int m = 0; m < 12; m++)
                    for (int h = 0; h < 24; h++)
                        sw.Write(thisInst.siteSuitability.flickerMap[i].flickerStats.shadowMins12x24[m, h].ToString() + ",");

                sw.WriteLine();

            }

            sw.Close();
         */   
        }

        public int GetHourFromHourString(string hourStr)
        {
            int thisHour = 0;

            if (hourStr == "7 am")
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

            return thisHour;
        }

        public struct FlickerGridGroup
        {
            public SiteSuitability.FlickerGrid[] flickerGrid;
        }

        public void ShadowFlickerSurfaceMovie(Continuum thisInst)
        {
            // Plots total shadow flicker of selected month and hour at each grid node

            string thisMonthStr = thisInst.cboSiteSuitMonth.SelectedItem.ToString();
            string thisHourStr = thisInst.cboSiteSuitHour.SelectedItem.ToString();

            int monthInd = GetMonthInd(thisMonthStr);
            int hourInd = 100;

            if (thisHourStr != "All")
                hourInd = Convert.ToInt16(thisHourStr);

            // Create grid array with all total number of shadow flicker hours
            FlickerGridGroup[] plotFlicker = new FlickerGridGroup[288];
            int thisInd = 0;
            for (int m = 0; m < 12; m++)
                for (int h = 0; h < 24; h++)
                {
                    plotFlicker[thisInd].flickerGrid = thisInst.siteSuitability.GetTotalFlickerHoursByMonthAndHour(m, h);
                    thisInd++;
                }

            NChart siteMap = thisInst.chtSiteSuitability.Charts[0];
            siteMap.Projection.SetPredefinedProjection(PredefinedProjection.OrthogonalTop);
            siteMap.Series.Clear();

            // Create grid that covers turbines +/- 3 km
            NMeshSurfaceSeries flickerSurface = new NMeshSurfaceSeries();
            flickerSurface.FillMode = SurfaceFillMode.Zone;
            flickerSurface.FrameMode = SurfaceFrameMode.None;
            flickerSurface.DrawFlat = true;
            flickerSurface.ValueFormatter.FormatSpecifier = "0.00";

            thisInst.chtSiteSuitability.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            thisInst.chtSiteSuitability.Controller.Tools.Add(tooltip);

            SiteSuitabilitySurfacePlotLabels(thisInst, -999, true);
            flickerSurface.Data.SetGridSize(thisInst.siteSuitability.numXFlicker, thisInst.siteSuitability.numYFlicker);

            int maxValue = 0;

            for (int i = 0; i < plotFlicker.Length; i++)
                for (int j = 0; j < plotFlicker[i].flickerGrid.Length; j++)
                    if (plotFlicker[i].flickerGrid[j].flickerStats.totalShadowMinsPerYear > maxValue)
                        maxValue = plotFlicker[i].flickerGrid[j].flickerStats.totalShadowMinsPerYear;

            flickerSurface.Palette.Clear();
            flickerSurface.Palette.SmoothPalette = false;
            flickerSurface.Palette.HasCustomMin = true;
            flickerSurface.Palette.CustomMin = 0;
            flickerSurface.Palette.Mode = PaletteMode.Custom;
            flickerSurface.Palette.PaletteSteps = 20;

            double intWidth = maxValue / 20.0;

            for (int i = 0; i < 20; i++)
                flickerSurface.Palette.Add(Math.Round(i * intWidth, 2), GetRGB_Values((double)i / 20));

            // Loop through each flicker grid
            thisInd = 0;

            startTime = DateTime.Now;
            DateTime lastTime = startTime;

            int plotInd = 0;
            UpdateShadowPlot(thisInst, plotFlicker[plotInd].flickerGrid);

            for (int m = 0; m < 12; m++)
                for (int h = 0; h < 24; h++)
                {
                    UpdateShadowPlot(thisInst, plotFlicker[plotInd].flickerGrid);
                    thisInst.chtSiteSuitability.Refresh();
                    TimeSpan timeSpan = DateTime.Now - lastTime;

                    while (timeSpan.Seconds < 5)
                        timeSpan = DateTime.Now - lastTime;

                    lastTime = DateTime.Now;
                }

        }

        public void UpdateShadowPlot(Continuum thisInst, SiteSuitability.FlickerGrid[] plotFlicker)
        {
            NMeshSurfaceSeries flickerSurface = (NMeshSurfaceSeries)thisInst.chtSiteSuitability.Charts[0].Series[1];

            TimeSpan span = startTime - DateTime.Now;
            double t = 0.002 * span.TotalMilliseconds;
            int flickerMapInd = 0;

            for (int i = 0; i < thisInst.siteSuitability.numXFlicker; i++)
                for (int j = 0; j < thisInst.siteSuitability.numYFlicker; j++)
                {
                    flickerSurface.Data.SetValue(i, j, plotFlicker[flickerMapInd].flickerStats.totalShadowMinsPerYear, thisInst.siteSuitability.mapMinBounds.UTMX + thisInst.siteSuitability.flickerGridReso * i,
                        thisInst.siteSuitability.mapMinBounds.UTMY + thisInst.siteSuitability.flickerGridReso * j);

                    flickerMapInd++;
                }

        }

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

        public void ShadowFlickerMaxDay(Continuum thisInst)
        {
            // Updates the max day and max number of flicker hours

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


        public void ShadowFlicker12x24(Continuum thisInst)
        {
            // Fills in Shadow Flicker table and textbox on Site Suitability tab

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

        public void ZoneShadowSummary(Continuum thisInst)
        {
            // Updates the list of Site Suitability tab showing total number of shadow flicker hours at each zone

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

        public string GetHourString(int thisHourInd)
        {
            // Returns a string representing integer hour
            string hourStr = "12 am";

            if (thisHourInd > 0 && thisHourInd < 13)
                hourStr = thisHourInd + " am";
            else if (thisHourInd > 0)
                hourStr = thisHourInd - 12 + " pm";

            return hourStr;
        }

        public void IceHitsByZone(Continuum thisInst)
        {
            // Update the yearly ice hits table
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

        public void SoundMap(Continuum thisInst)
        {
            // Creates a surface plot of sound model

            NChart siteMap = thisInst.chtSiteSuitability.Charts[0];
            siteMap.Projection.SetPredefinedProjection(PredefinedProjection.OrthogonalTop);
            siteMap.Series.Clear();
            siteMap.Enable3D = true;
            siteMap.Width = 70f;
            siteMap.Depth = 70f;
            siteMap.Height = 0.001f;
            thisInst.chtSiteSuitability.Legends.Clear();
            thisInst.chtSiteSuitability.Labels.Clear();
            thisInst.chtSiteSuitability.Labels.AddHeader("Estimated Sound Levels (dBA)");

            // Create grid that covers turbines +/- 3 km
            NMeshSurfaceSeries soundSurface = new NMeshSurfaceSeries();
            soundSurface.FillMode = SurfaceFillMode.Zone;
            soundSurface.FrameMode = SurfaceFrameMode.None;
            soundSurface.DrawFlat = true;
            soundSurface.ValueFormatter.FormatSpecifier = "0";
            
            soundSurface.Palette.Clear();
            soundSurface.Palette.SmoothPalette = false;
            soundSurface.Palette.HasCustomMin = true;
            soundSurface.Palette.CustomMin = 0;
            soundSurface.Palette.Mode = PaletteMode.Custom;
            soundSurface.Palette.PaletteSteps = 10;

            soundSurface.Legend.Mode = SeriesLegendMode.SeriesLogic;
            soundSurface.Legend.Format = "<zone_begin> - <zone_end>";
            NLegend Map_Legend = new NLegend();
            thisInst.chtSiteSuitability.Charts[0].DisplayOnLegend = Map_Legend;
            thisInst.chtSiteSuitability.Panels.Add(Map_Legend);
            Map_Legend.DockMode = PanelDockMode.Right;

            double intWidth = thisInst.siteSuitability.turbineSound / 15.0;

            for (int i = 0; i < 15; i++)
                soundSurface.Palette.Add(Math.Round(i * intWidth, 2), GetRGB_Values((double)i / 15));

            /*      flickerSurface.Legend.Mode = SeriesLegendMode.SeriesLogic;
                  flickerSurface.Legend.Format = "<zone_begin> - <zone_end>";
                  NLegend Map_Legend = new NLegend();
                  thisInst.chtSiteSuitability.Charts[0].DisplayOnLegend = Map_Legend;
                  thisInst.chtSiteSuitability.Panels.Add(Map_Legend);
                  Map_Legend.DockMode = PanelDockMode.Right;
      */

            soundSurface.Data.SetGridSize(thisInst.siteSuitability.numXSound, thisInst.siteSuitability.numYSound);
            thisInst.siteSuitability.FindShadowMapBounds(thisInst, false);

            for (int i = 0; i < thisInst.siteSuitability.numXSound; i++)
                for (int j = 0; j < thisInst.siteSuitability.numYSound; j++)
                {
                    soundSurface.Data.SetValue(i, j, thisInst.siteSuitability.soundMap[i, j], thisInst.siteSuitability.mapMinBounds.UTMX + thisInst.siteSuitability.soundGridReso * i,
                        thisInst.siteSuitability.mapMinBounds.UTMY + thisInst.siteSuitability.soundGridReso * j);

                }

            siteMap.Series.Add(soundSurface);                       

            SiteSuitabilitySurfacePlotLabels(thisInst, -999, true);
            thisInst.chtSiteSuitability.Refresh();
        }

        public void SoundAtZones(Continuum thisInst)
        {
            // Updates estimated sound level at turbine sites
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

        public void Exceedance_TAB(Continuum thisInst)
        {
            PerformanceFactorList(thisInst);
            PerformanceFactorsPlot(thisInst);
            PValsTable(thisInst);

        }

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
            double perfFact = 0;
            double pVal = 0;

            for (int i = 0; i < thisInst.turbineList.exceed.Num_Exceed(); i++)
            {
                ListViewItem lvi = new ListViewItem(thisInst.turbineList.exceed.exceedCurves[i].exceedStr);
                lvi.Checked = true;
                Exceedance.ExceedanceCurve exceedCurve = thisInst.turbineList.exceed.exceedCurves[i];

                // P10
                pVal = 0.9;
                perfFact = Math.Round(thisInst.turbineList.exceed.Get_PF_Value(pVal, exceedCurve), 3);
                lvi.SubItems.Add(perfFact.ToString("P"));

                // P50
                pVal = 0.5;
                perfFact = Math.Round(thisInst.turbineList.exceed.Get_PF_Value(pVal, exceedCurve), 3);
                lvi.SubItems.Add(perfFact.ToString("P"));

                // P90
                pVal = 0.1;
                perfFact = Math.Round(thisInst.turbineList.exceed.Get_PF_Value(pVal, exceedCurve), 3);
                lvi.SubItems.Add(perfFact.ToString("P"));

                // Lower and Upper bounds
                lvi.SubItems.Add(Math.Round(exceedCurve.lowerBound, 4).ToString("P"));
                lvi.SubItems.Add(Math.Round(exceedCurve.upperBound, 4).ToString("P"));

                if (exceedCurve.modes != null)
                {
                    if (exceedCurve.modes.Length == 1) // normal standard deviation
                    {
                        lvi.SubItems.Add(exceedCurve.modes[0].mean.ToString("P"));
                        lvi.SubItems.Add(exceedCurve.modes[0].SD.ToString("P"));
                    }
                }

                thisInst.lstDefinedLosses.Items.Add(lvi);
            }

            thisInst.lstDefinedLosses.Columns[0].Width = 290; // width of first columns: name of performance factor
            thisInst.okToUpdate = true;
        }

        public void PerformanceFactorsPlot(Continuum thisInst)
        {
            // Plots probability and cumulative density function of all (checked) defined exceedance
            
            NChartControl perfFactChart = thisInst.chtExceedDists;
            perfFactChart.Charts[0].Series.Clear();
            perfFactChart.Labels.Clear();
            perfFactChart.Controller.Tools.Clear();
            perfFactChart.Legends[0].Visible = false;

            NTooltipTool tooltip = new NTooltipTool();
            perfFactChart.Controller.Tools.Add(tooltip);

            string PDF_name = "";
            int PDF_Ind = 0;
            string CDF_name = "";
            int CDF_Ind = 0;

            // see which are checked in list
            ListView lv = thisInst.lstDefinedLosses;

            if (thisInst.turbineList.exceed == null)
                return;

            int numDefined = thisInst.turbineList.exceed.Num_Exceed();

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(perfFactChart.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Performance Factor";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(perfFactChart.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "Probability";

            NStandardScaleConfigurator scaleConfiguratorYSec = (NStandardScaleConfigurator)(perfFactChart.Charts[0].Axis(StandardAxis.SecondaryY).ScaleConfigurator);
            scaleConfiguratorYSec.Title.Text = "Cumulative Probability";

            for (int i = 0; i < numDefined; i++)
            {
                if (thisInst.lstDefinedLosses.Items[i].Checked == true)
                {
                    if (thisInst.chkShowPDF.Checked)
                    {
                        PDF_name = "PDF " + thisInst.turbineList.exceed.exceedCurves[i].exceedStr;

                        NLineSeries pdfSeries = new NLineSeries();
                        pdfSeries.DataLabelStyle.Visible = false;
                        pdfSeries.Name = PDF_name;
                        pdfSeries.UseXValues = true;
                        perfFactChart.Charts[0].Series.Add(pdfSeries);
                        pdfSeries.BorderStyle.Color = GetMetOrTurbColor(i);
                        pdfSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                        pdfSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(PDF_name);
                        PDF_Ind = perfFactChart.Charts[0].Series.Count - 1;

                        for (int j = 0; j < thisInst.turbineList.exceed.exceedCurves[i].distSize; j++)
                        {
                            pdfSeries.XValues.Add(thisInst.turbineList.exceed.exceedCurves[i].xVals[j] * 100);
                            pdfSeries.Values.Add(thisInst.turbineList.exceed.exceedCurves[i].probDist[j]);
                        }
                    }

                    if (thisInst.chkShowCDFs.Checked)
                    {
                        CDF_name = "CDF " + thisInst.turbineList.exceed.exceedCurves[i].exceedStr;

                        NLineSeries cdfSeries = new NLineSeries();
                        cdfSeries.DataLabelStyle.Visible = false;
                        cdfSeries.Name = CDF_name;
                        cdfSeries.UseXValues = true;
                        perfFactChart.Charts[0].Series.Add(cdfSeries);
                        cdfSeries.BorderStyle.Color = GetMetOrTurbColor(i);
                        cdfSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                        cdfSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(CDF_name);
                        CDF_Ind = perfFactChart.Charts[0].Series.Count - 1;

                        for (int j = 0; j < thisInst.turbineList.exceed.exceedCurves[i].distSize; j++)
                        {
                            cdfSeries.XValues.Add(thisInst.turbineList.exceed.exceedCurves[i].xVals[j] * 100);
                            cdfSeries.Values.Add(thisInst.turbineList.exceed.exceedCurves[i].cumulDist[j]);
                        }
                    }

                    if (thisInst.chkShowPDF.Checked && thisInst.chkShowCDFs.Checked)
                    {
                        perfFactChart.Charts[0].Series[CDF_Ind].DisplayOnAxis(StandardAxis.PrimaryY, true);
                        perfFactChart.Charts[0].Series[PDF_Ind].DisplayOnAxis(StandardAxis.SecondaryY, true);
                    }
                    else if (thisInst.chkShowPDF.Checked && thisInst.chkShowCDFs.Checked == false)
                    {
                        perfFactChart.Charts[0].Series[PDF_Ind].DisplayOnAxis(StandardAxis.PrimaryY, true);
                    }
                    else if (thisInst.chkShowCDFs.Checked && thisInst.chkShowPDF.Checked == false)
                    {
                        perfFactChart.Charts[0].Series[CDF_Ind].DisplayOnAxis(StandardAxis.PrimaryY, true);
                    }
                }
            }

            perfFactChart.Refresh();
        }

        public void PValsTable(Continuum thisInst)
        {
            // updates the table and plot of P values

            thisInst.lstPvals.Items.Clear();
            NChartControl pValPlot = thisInst.chtPVals;
            pValPlot.Charts[0].Series.Clear();
            pValPlot.Labels.Clear();
            pValPlot.Controller.Tools.Clear();
            pValPlot.Legends[0].Visible = false;
            NTooltipTool tooltip = new NTooltipTool();
            pValPlot.Controller.Tools.Add(tooltip);

            if (thisInst.turbineList.exceed == null)
            {
                pValPlot.Refresh();
                return;
            }

            pValPlot.Labels.AddHeader("Composite P-Value Distribution");

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(pValPlot.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Overall Performance Factor";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(pValPlot.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "P-Value";

            if (thisInst.turbineList.exceed.compositeLoss.isComplete == false)
            {
                pValPlot.Refresh();
                return;
            }

            double AEP = 0;           

            string wakeModelString = "";
            Wake_Model thisWakeModel = new Wake_Model();

            if (thisInst.cboExceedWake.Items.Count > 0)
            {
                wakeModelString = thisInst.cboExceedWake.SelectedItem.ToString();
                thisWakeModel = thisInst.wakeModelList.GetWakeModelFromString(wakeModelString);
            }                        

            Turbine thisTurb = thisInst.GetSelectedTurbine("Exceedance");
            Turbine.Net_Energy_Est netEst = thisTurb.GetNetEnergyEst(thisWakeModel);
            double overallP50 = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
            AEP = netEst.AEP / overallP50;

            NLineSeries pVal1yr = new NLineSeries();
            pVal1yr.DataLabelStyle.Visible = false;
            pVal1yr.Name = "1-year P-Values";
            pVal1yr.UseXValues = true;
            pValPlot.Charts[0].Series.Add(pVal1yr);
            pVal1yr.BorderStyle.Color = GetMetOrTurbColor(0);
            pVal1yr.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            pVal1yr.InteractivityStyle.Tooltip = new NTooltipAttribute("1-year P-Values");

            NLineSeries pVal10yrs = new NLineSeries();
            pVal10yrs.DataLabelStyle.Visible = false;
            pVal10yrs.Name = "10-year P-Values";
            pVal10yrs.UseXValues = true;
            pValPlot.Charts[0].Series.Add(pVal10yrs);
            pVal10yrs.BorderStyle.Color = GetMetOrTurbColor(2);
            pVal10yrs.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            pVal10yrs.InteractivityStyle.Tooltip = new NTooltipAttribute("10-years P-Values");

            NLineSeries pVal20yrs = new NLineSeries();
            pVal20yrs.DataLabelStyle.Visible = false;
            pVal20yrs.Name = "20-year P-Values";
            pVal20yrs.UseXValues = true;
            pValPlot.Charts[0].Series.Add(pVal20yrs);
            pVal20yrs.BorderStyle.Color = GetMetOrTurbColor(4);
            pVal20yrs.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            pVal20yrs.InteractivityStyle.Tooltip = new NTooltipAttribute("20-years P-Values");

            for (int i = 0; i < 99; i++)
            {
                pVal1yr.XValues.Add(thisInst.turbineList.exceed.compositeLoss.pVals1yr[98 - i]);
                pVal1yr.Values.Add((i + 1));

                pVal10yrs.XValues.Add(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[98 - i]);
                pVal10yrs.Values.Add((i + 1));

                pVal20yrs.XValues.Add(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[98 - i]);
                pVal20yrs.Values.Add((i + 1));
            }

            // P1
            ListViewItem lvi_1 = new ListViewItem("P1");
            // 1 year values
            lvi_1.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[0], 3).ToString());
            if (AEP != 0)
                lvi_1.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[0] * AEP, 2)));
            else
                lvi_1.SubItems.Add("");
            // 10 year values
            lvi_1.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[0], 3).ToString());
            if (AEP != 0)
                lvi_1.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[0] * AEP, 2)));
            else
                lvi_1.SubItems.Add("");
            // 20 year values
            lvi_1.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[0], 3).ToString());
            if (AEP != 0)
                lvi_1.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[0] * AEP, 2)));
            else
                lvi_1.SubItems.Add("");

            thisInst.lstPvals.Items.Add(lvi_1);

            // P10
            ListViewItem lvi_10 = new ListViewItem("P10");

            // 1 year values
            lvi_10.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[9], 3).ToString());
            if (AEP != 0)
                lvi_10.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[9] * AEP, 2)));
            else
                lvi_10.SubItems.Add("");
            // 10 year values
            lvi_10.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[9], 3).ToString());
            if (AEP != 0)
                lvi_10.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[9] * AEP, 2)));
            else
                lvi_10.SubItems.Add("");
            // 20 year values
            lvi_10.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[9], 3).ToString());
            if (AEP != 0)
                lvi_10.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[9] * AEP, 2)));
            else
                lvi_10.SubItems.Add("");

            thisInst.lstPvals.Items.Add(lvi_10);

            // P50
            ListViewItem lvi_50 = new ListViewItem("P50");

            // 1 year values
            lvi_50.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[49], 3).ToString());
            if (AEP != 0)
                lvi_50.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[49] * AEP, 2)));
            else
                lvi_50.SubItems.Add("");
            // 10 year values
            lvi_50.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[49], 3).ToString());
            if (AEP != 0)
                lvi_50.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[49] * AEP, 2)));
            else
                lvi_50.SubItems.Add("");
            // 20 year values
            lvi_50.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[49], 3).ToString());
            if (AEP != 0)
                lvi_50.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[49] * AEP, 2)));
            else
                lvi_50.SubItems.Add("");

            thisInst.lstPvals.Items.Add(lvi_50);

            // P90
            ListViewItem lvi_90 = new ListViewItem("P90");
            // 1 year values
            lvi_90.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[89], 3).ToString());
            if (AEP != 0)
                lvi_90.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[89] * AEP, 2)));
            else
                lvi_90.SubItems.Add("");
            // 10 year values
            lvi_90.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[89], 3).ToString());
            if (AEP != 0)
                lvi_90.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[89] * AEP, 2)));
            else
                lvi_90.SubItems.Add("");
            // 20 year values
            lvi_90.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[89], 3).ToString());
            if (AEP != 0)
                lvi_90.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[89] * AEP, 2)));
            else
                lvi_90.SubItems.Add("");

            thisInst.lstPvals.Items.Add(lvi_90);

            // P99
            ListViewItem lvi_99 = new ListViewItem("P99");
            // 1 year values
            lvi_99.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[98], 3).ToString());
            if (AEP != 0)
                lvi_99.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals1yr[98] * AEP, 2)));
            else
                lvi_99.SubItems.Add("");
            // 10 year values
            lvi_99.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[98], 3).ToString());
            if (AEP != 0)
                lvi_99.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals10yrs[98] * AEP, 2)));
            else
                lvi_99.SubItems.Add("");
            // 20 year values
            lvi_99.SubItems.Add(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[98], 3).ToString());
            if (AEP != 0)
                lvi_99.SubItems.Add(Convert.ToString(Math.Round(thisInst.turbineList.exceed.compositeLoss.pVals20yrs[98] * AEP, 2)));
            else
                lvi_99.SubItems.Add("");

            thisInst.lstPvals.Items.Add(lvi_99);

            thisInst.lstPvals.Columns[0].Width = 70;
            thisInst.lstPvals.Columns[1].Width = 60;
            thisInst.lstPvals.Columns[2].Width = 70;
            thisInst.lstPvals.Columns[3].Width = 60;
            thisInst.lstPvals.Columns[4].Width = 70;
            thisInst.lstPvals.Columns[5].Width = 60;
            thisInst.lstPvals.Columns[6].Width = 70;

            pValPlot.Refresh();

        }

        public void SiteSuitabilityVisibility(Continuum thisInst)
        {
            // Updates visibility of plots and tables on Site Suitability tab based on selected model
            thisInst.okToUpdate = false;
            string selectedModel = thisInst.GetSelectedSiteSuitabilityModel();

            if (selectedModel == "Ice Throw")
            {
                thisInst.lstShadow12x24.Visible = false;
                thisInst.chtIceVsDistOrHisto.Visible = true;
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
                thisInst.chtIceVsDistOrHisto.Visible = false;

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
                    try
                    {
                        firstInDrop = thisInst.cboZoneList.Items[0].ToString();
                        lastInDrop = thisInst.cboZoneList.Items[dropCount - 1].ToString();
                    }
                    catch { }

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
           
            thisInst.chtIceVsDistOrHisto.Refresh();
            thisInst.okToUpdate = true;
        }

        public void IceHitsVsDistancePlot(Continuum thisInst)
        {
            // Updates probability vs. distance plot on Site Suitability tab
            NChartControl iceChart = thisInst.chtIceVsDistOrHisto;
            iceChart.Charts[0].Series.Clear();
            iceChart.Charts[0].Width = 200;
            iceChart.Charts[0].Height = 125;
            iceChart.Legends[0].Visible = false;
            iceChart.Labels.Clear();
            iceChart.Controller.Tools.Clear();

            if (thisInst.turbineList.TurbineCount == 0 || thisInst.siteSuitability.yearlyIceHits.Length == 0)
            {
                iceChart.Refresh();
                return;
            }

            NTooltipTool tooltip = new NTooltipTool();
            iceChart.Controller.Tools.Add(tooltip);
            iceChart.Labels.AddHeader("Number of Ice Impacts over Turbine Lifetime");

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(iceChart.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Distance from Turbine, m";
            scaleConfiguratorX.MajorTickMode = MajorTickMode.AutoMaxCount;
            scaleConfiguratorX.MaxTickCount = 10;
            scaleConfiguratorX.MinorTickCount = 0;

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(iceChart.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.MajorTickMode = MajorTickMode.AutoMaxCount;
            scaleConfiguratorY.MinorTickCount = 0;
            scaleConfiguratorY.Title.Text = "Number of Ice Impacts";

            NLineSeries iceSeries = new NLineSeries();
            iceSeries.DataLabelStyle.Visible = false;
            iceSeries.Name = "Probability of Ice Hit";
            iceChart.Charts[0].Series.Add(iceSeries);
            iceSeries.BorderStyle.Color = GetMetOrTurbColor(50);
            iceSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            iceSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("Probability of Ice Hit");
            iceSeries.UseXValues = true;

            int yearToShow = Convert.ToInt16(thisInst.cboIcingYear.SelectedItem.ToString());
            int WD_Ind = Convert.ToInt16(thisInst.cboZoneList.SelectedIndex); // Index 16 (Num WD) = All WD

            double[] probIceVsDist = thisInst.siteSuitability.CalcIceHitVersusDistance(thisInst.siteSuitability.yearlyIceHits[yearToShow - 1].iceHits, WD_Ind,
                thisInst.turbineList.turbineEsts[0].name, thisInst);


            for (int i = 0; i < 21; i++)
            {
                iceSeries.XValues.Add(i * 50);
                iceSeries.Values.Add(probIceVsDist[i]);
            }

            iceChart.Refresh();

        }

        public void IceHitVsDistTable(Continuum thisInst)
        {
            // Updates Ice hit count versus Distance table on Site Suitability tab

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
                    ListViewItem lvi = new ListViewItem((i * 50).ToString());
                    lvi.SubItems.Add(iceHitsVsDist[i].ToString());

                    thisInst.lstShadowZoneSummary.Items.Add(lvi);
                }
            }
        }

        public void IcingYearsDropDown(Continuum thisInst)
        {
            // Updates year dropdown menu on Site Suitability tab (used for ice throw model)
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

        public void IceThrowPlotsAndTables(Continuum thisInst)
        {
            // Updates all plots and tables on Site Suitability tab related to ice throw
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

        public void IceHitHistogram(Continuum thisInst)
        {
            // Updates yearly ice hit histogram on Site Suitability tab

            NChart iceChart = thisInst.chtIceVsDistOrHisto.Charts[0];
            NChartControl iceChartControl = thisInst.chtIceVsDistOrHisto;
            iceChartControl.Legends[0].Visible = false;
            iceChart.Series.Clear();
            thisInst.chtIceVsDistOrHisto.Labels.Clear();

            SiteSuitability.Zone zone = GetSelectedZone(thisInst);

            if (zone.latitude == 0)
            {
                iceChart.Refresh();
                return;
            }

            double[] hitHisto = new double[6]; // Bins are: 0, 1, 2, 3, 4, > 4

            for (int i = 0; i < thisInst.siteSuitability.numYearsToModel; i++)
            {
                int numHits = thisInst.siteSuitability.GetTotalNumberOfIceHitsAtZone(i, zone, thisInst);

                if (numHits > 4)
                    hitHisto[5]++;
                else
                    hitHisto[numHits]++;
            }

            NBarSeries hitBars = new NBarSeries();
            iceChart.Series.Add(hitBars);
            hitBars.DataLabelStyle.Visible = false;
            hitBars.MultiBarMode = MultiBarMode.Series;
            hitBars.UseXValues = true;

            for (int i = 0; i < 6; i++)
            {
                hitBars.XValues.Add(i);
                hitBars.Values.Add(hitHisto[i]);
            }

            iceChartControl.Labels.AddHeader("Yearly Ice Hit Histogram");

            NLinearScaleConfigurator scaleConfiguratorX = (NLinearScaleConfigurator)(iceChart.Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.MinorTickCount = 0;
            scaleConfiguratorX.MajorTickMode = MajorTickMode.CustomStep;
            scaleConfiguratorX.CustomStep = 1;
            scaleConfiguratorX.Title.Text = "Number of Ice Hits Per Year";

            NLinearScaleConfigurator scaleConfiguratorY = (NLinearScaleConfigurator)(iceChart.Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.MajorTickMode = MajorTickMode.AutoMaxCount;
            scaleConfiguratorY.Title.Text = "Freq. of Occurrence";

            iceChart.Refresh();
            thisInst.chtIceVsDistOrHisto.Refresh();

        }
                
        public void TurbulenceIntensityPlotAndTable(Continuum thisInst)
        {
            // Updates the turbulence intensity plot and table on Site Conditions tab
            if (thisInst.metList.isTimeSeries == false)
                return;

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

            NChartControl turbChart = thisInst.chtTurbulence;
            turbChart.Charts[0].Series.Clear();
            turbChart.Labels.Clear();
            turbChart.Controller.Tools.Clear();
            turbChart.Legends[0].Visible = false;
            NTooltipTool tooltip = new NTooltipTool();
            turbChart.Controller.Tools.Add(tooltip);

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(turbChart.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.MajorTickMode = MajorTickMode.AutoMaxCount;
            //  scaleConfiguratorX.CustomStep = 2;
            scaleConfiguratorX.MaxTickCount = 15;
            scaleConfiguratorX.Title.Text = "Wind Speed, m/s";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(turbChart.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "TI";

            NLabel chartTitle = turbChart.Labels.AddHeader(turbType + " TI vs. WS");

            NLineSeries TISeries = new NLineSeries();
            TISeries.DataLabelStyle.Visible = false;
            TISeries.UseXValues = true;
            TISeries.MarkerStyle.FillStyle = new NColorFillStyle(Color.Red);
            TISeries.BorderStyle.Color = Color.Red;
            TISeries.InteractivityStyle.Tooltip = new NTooltipAttribute(turbType + "TI");

            turbChart.Charts[0].Series.Add(TISeries);

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
                    ListViewItem lvi = new ListViewItem(i.ToString());

                    if (turbType == "Effective")
                        lvi.SubItems.Add(Math.Round(effectiveTI[i], 4).ToString("P"));
                    else
                        lvi.SubItems.Add(Math.Round(overallTI[i].overallTI, 4).ToString("P"));

                    lvi.SubItems.Add(overallTI[i].count.ToString());
                    thisInst.lstTurbulence.Items.Add(lvi);

                    TISeries.XValues.Add(i);

                    if (turbType == "Effective")
                        TISeries.Values.Add(effectiveTI[i]);
                    else
                        TISeries.Values.Add(overallTI[i].overallTI);                    
                }
                else
                {
                    if (turbType == "Average")
                    {
                        if (thisMet.turbulence.count[i, WD_Ind] > 0)
                        {
                            ListViewItem lvi = new ListViewItem(i.ToString());
                            lvi.SubItems.Add(Math.Round((thisMet.turbulence.avgSD[i, WD_Ind] / thisMet.turbulence.avgWS[i, WD_Ind]), 4).ToString("P"));
                            lvi.SubItems.Add(thisMet.turbulence.count[i, WD_Ind].ToString());
                            thisInst.lstTurbulence.Items.Add(lvi);

                            TISeries.XValues.Add(i);
                            TISeries.Values.Add(thisMet.turbulence.avgSD[i, WD_Ind] / thisMet.turbulence.avgWS[i, WD_Ind]);

                        }
                    }
                    else if (turbType == "Representative")
                    {
                        if (thisMet.turbulence.count[i, WD_Ind] > 2)
                        {
                            ListViewItem lvi = new ListViewItem(i.ToString());
                            lvi.SubItems.Add(Math.Round((thisMet.turbulence.p90SD[i, WD_Ind] / thisMet.turbulence.avgWS[i, WD_Ind]), 4).ToString("P"));
                            lvi.SubItems.Add(thisMet.turbulence.count[i, WD_Ind].ToString());
                            thisInst.lstTurbulence.Items.Add(lvi);

                            TISeries.XValues.Add(i);
                            TISeries.Values.Add(thisMet.turbulence.p90SD[i, WD_Ind] / thisMet.turbulence.avgWS[i, WD_Ind]);

                        }

                    }
                    else if (turbType == "Effective")
                    {
                        if (thisMet.turbulence.count[i, WD_Ind] > 2)
                        {
                            ListViewItem lvi = new ListViewItem(i.ToString());
                            lvi.SubItems.Add(Math.Round(effectiveTI[i], 4).ToString("P"));
                            lvi.SubItems.Add(thisMet.turbulence.count[i, WD_Ind].ToString());
                            thisInst.lstTurbulence.Items.Add(lvi);

                            TISeries.XValues.Add(i);
                            TISeries.Values.Add(effectiveTI[i]);
                        }
                    }
                }

            }

            thisInst.chtTurbulence.Refresh();
            PlotIEC_TurbModels(thisInst);

        }

        public void PlotIEC_TurbModels(Continuum thisInst)
        {
            NChartControl turbChart = thisInst.chtTurbulence;
            NLineSeries iecA = new NLineSeries();
            NLineSeries iecB = new NLineSeries();
            NLineSeries iecC = new NLineSeries();

            iecA.DataLabelStyle.Visible = false;
            iecA.UseXValues = true;
            iecA.FillStyle = new NColorFillStyle(Color.Blue);
            iecA.BorderStyle.Color = Color.Blue;
            iecA.InteractivityStyle.Tooltip = new NTooltipAttribute("IEC A");

            iecB.DataLabelStyle.Visible = false;
            iecB.UseXValues = true;
            iecB.FillStyle = new NColorFillStyle(Color.Green);
            iecB.BorderStyle.Color = Color.Green;
            iecB.InteractivityStyle.Tooltip = new NTooltipAttribute("IEC B");

            iecC.DataLabelStyle.Visible = false;
            iecC.UseXValues = true;
            iecC.FillStyle = new NColorFillStyle(Color.Orange);
            iecC.BorderStyle.Color = Color.Orange;
            iecC.InteractivityStyle.Tooltip = new NTooltipAttribute("IEC C");

            turbChart.Charts[0].Series.Add(iecA);
            turbChart.Charts[0].Series.Add(iecB);
            turbChart.Charts[0].Series.Add(iecC);

            for (int i = 3; i < thisInst.metList.numWS; i++)
            {
                iecA.XValues.Add(i);
                iecA.Values.Add(0.16 * (0.75 * i + 5.6) / i);

                iecB.XValues.Add(i);
                iecB.Values.Add(0.14 * (0.75 * i + 5.6) / i);

                iecC.XValues.Add(i);
                iecC.Values.Add(0.12 * (0.75 * i + 5.6) / i);
            }

            thisInst.chtTurbulence.Refresh();
        }

        public void SiteConditionsMetDates(Continuum thisInst)
        {
            // Updates start/end dates on Site Conditions tab. It's called in All_Tabs with okToUpdate sent to false so need to check if it's already false before setting/resetting it.

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

        public void SiteConditionsAlpha(Continuum thisInst)
        {
            AlphaHistogram(thisInst);
            ExtremeShearTable(thisInst);
        }

        public void AlphaHistogram(Continuum thisInst)
        {
            Met thisMet = thisInst.GetSelectedMet("Site Conditions Shear");

            if (thisInst.cboExtremeShearRange.SelectedIndex == -1)
                thisInst.cboExtremeShearRange.SelectedIndex = 0;

            NBarSeries alphaHisto = new NBarSeries();
            thisInst.chtAlpha_Histo.Charts[0].Series.Clear();
            thisInst.chtAlpha_Histo.Labels.Clear();
            thisInst.chtAlpha_Histo.Legends[0].Visible = false;
            thisInst.chtAlpha_Histo.Charts[0].Series.Add(alphaHisto);
            alphaHisto.FillStyle = new NColorFillStyle(Color.Red);
            alphaHisto.DataLabelStyle.Visible = false;
            alphaHisto.MultiBarMode = MultiBarMode.Series;
            alphaHisto.UseXValues = true;

            if (thisMet.name == null)
            {
                thisInst.chtAlpha_Histo.Refresh();
                return;
            }


            if (thisMet.metData == null)
            {
                thisInst.chtAlpha_Histo.Refresh();
                return;
            }

            DateTime startTime = thisInst.dateTimeExtremeShearStart.Value;
            DateTime endTime = thisInst.dateTimeExtremeShearEnd.Value;

            string rangeAlpha = thisInst.cboExtremeShearRange.SelectedItem.ToString();
            double[] thisHisto = thisMet.GetAlphaHistogram(rangeAlpha, thisInst, startTime, endTime);

            for (int i = 0; i < thisHisto.Length; i++)
            {
                alphaHisto.XValues.Add(-0.5 + i * 0.02);
                alphaHisto.Values.Add(thisHisto[i]);
            }

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(thisInst.chtAlpha_Histo.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Shear Alpha Exponent";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(thisInst.chtAlpha_Histo.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "Freq. of Occurrence";

            thisInst.chtAlpha_Histo.Labels.AddHeader("Shear Alpha Histogram: " + thisInst.cboExtremeShearRange.SelectedItem.ToString());

            thisInst.chtAlpha_Histo.Refresh();
        }

        public void ExtremeShearTable(Continuum thisInst)
        {
            // Updates extreme shear P table (P1, P10, and P50) for wind speed ranges: 5 - 10, 10 - 15, 15+, All WS

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

            ListViewItem lvi = new ListViewItem("P1");
            lvi.SubItems.Add(Math.Round(alphaP1_5_to_10, 3).ToString());
            lvi.SubItems.Add(Math.Round(alphaP1_10_to_15, 3).ToString());
            lvi.SubItems.Add(Math.Round(alphaP1_15plus, 3).ToString());
            lvi.SubItems.Add(Math.Round(alphaP1_All, 3).ToString());
            thisInst.lstExtremeShear.Items.Add(lvi);

            lvi = new ListViewItem("P10");
            lvi.SubItems.Add(Math.Round(alphaP10_5_to_10, 3).ToString());
            lvi.SubItems.Add(Math.Round(alphaP10_10_to_15, 3).ToString());
            lvi.SubItems.Add(Math.Round(alphaP10_15plus, 3).ToString());
            lvi.SubItems.Add(Math.Round(alphaP10_All, 3).ToString());
            thisInst.lstExtremeShear.Items.Add(lvi);

            lvi = new ListViewItem("P50");
            lvi.SubItems.Add(Math.Round(alphaP50_5_to_10, 3).ToString());
            lvi.SubItems.Add(Math.Round(alphaP50_10_to_15, 3).ToString());
            lvi.SubItems.Add(Math.Round(alphaP50_15plus, 3).ToString());
            lvi.SubItems.Add(Math.Round(alphaP50_All, 3).ToString());
            thisInst.lstExtremeShear.Items.Add(lvi);
        }

        public void ExtremeWindSpeed(Continuum thisInst)
        {
            // Updates extreme wind speed textboxes and plots
            Met thisMet = thisInst.GetSelectedMet("Site Conditions Extreme WS");
            Met.Extreme_WindSpeed extremeWS = thisMet.CalcExtremeWindSpeeds(thisInst);

            NChartControl extremeWSCht = thisInst.chtExtremeWSDist;
            extremeWSCht.Charts[0].Series.Clear();
            extremeWSCht.Labels.Clear();
            extremeWSCht.Controller.Tools.Clear();
            extremeWSCht.Legends[0].Visible = false;

            thisInst.lblNoExtremeWS.Text = "";

            if (extremeWS.tenMin1yr == 0)
            {
                thisInst.lblNoExtremeWS.Text = "Met data does not cover a full year (Jan. - Dec.) required for extreme WS calculations.";
                extremeWSCht.Refresh();

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

            NTooltipTool tooltip = new NTooltipTool();
            extremeWSCht.Controller.Tools.Add(tooltip);
            extremeWSCht.Labels.AddHeader("Extreme Wind Speeds by Years of Recurrence");

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(extremeWSCht.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Years of Recurrence";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(extremeWSCht.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "Max. Wind Speed, m/s";

            NLineSeries maxTenMinSeries = new NLineSeries();
            maxTenMinSeries.DataLabelStyle.Visible = false;
            maxTenMinSeries.Name = "Max Ten-Min WS";
            extremeWSCht.Charts[0].Series.Add(maxTenMinSeries);
            maxTenMinSeries.BorderStyle.Color = Color.Red;
            maxTenMinSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            maxTenMinSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("Max Ten-Min WS");

            NLineSeries maxGustSeries = new NLineSeries();
            maxGustSeries.DataLabelStyle.Visible = false;
            maxGustSeries.Name = "Max Gust WS";
            extremeWSCht.Charts[0].Series.Add(maxGustSeries);
            maxGustSeries.BorderStyle.Color = Color.Blue;
            maxGustSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            maxGustSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("Max Ten-Min WS");

            for (int i = 0; i < extremeWS.yearsOfOcc.Length; i++)
            {
                maxTenMinSeries.XValues.Add(1.5 + i * 0.5);
                maxTenMinSeries.Values.Add(extremeWS.maxTenMin[i]);

                maxGustSeries.XValues.Add(1.5 + i * 0.5);
                maxGustSeries.Values.Add(extremeWS.maxGust[i]);
            }

            extremeWSCht.Refresh();
        }
        
        public void InflowAnglePlotAndTable(Continuum thisInst)
        {
            // Updates the inflow angle plot and textboxes on Site Conditions tab

            thisInst.chtInflowAngle.Charts[0].Series.Clear();
            thisInst.chtInflowAngle.Legends[0].Visible = false;

            Turbine thisTurb = thisInst.GetSelectedTurbine("Inflow Angle");

            if (thisInst.cboInflowWD.Items.Count == 0 || thisTurb.UTMX == 0 || thisInst.topo.gotTopo == false)
            {
                thisInst.chtInflowAngle.Refresh();
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
            double inflowAngle = Math.Atan(slope)* 180 / Math.PI;
            thisInst.txtInflowAngle.Text = Math.Round(inflowAngle, 2).ToString();

            // Update plot
            NChartControl inflowCht = thisInst.chtInflowAngle;
            inflowCht.Charts[0].Series.Clear();
            inflowCht.Labels.Clear();
            inflowCht.Controller.Tools.Clear();

            NTooltipTool tooltip = new NTooltipTool();
            inflowCht.Controller.Tools.Add(tooltip);
            inflowCht.Labels.AddHeader("Elevation Profile along WD = " + thisWD.ToString());

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(inflowCht.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = "Distance";

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(inflowCht.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "Elevation, m";

            NLineSeries elevSeries = new NLineSeries();
            elevSeries.DataLabelStyle.Visible = false;
            elevSeries.Name = "Elevation";
            elevSeries.UseXValues = true;
            inflowCht.Charts[0].Series.Add(elevSeries);
            elevSeries.BorderStyle.Color = Color.Blue;
            elevSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            elevSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("Elevation Profile");

            for (int i = 0; i < elevProfile.Length; i++)
            {
                elevSeries.XValues.Add(i * reso);
                elevSeries.Values.Add(elevProfile[i].elev);
            }

            NLineSeries slopeSeries = new NLineSeries();
            slopeSeries.DataLabelStyle.Visible = false;
            slopeSeries.UseXValues = true;
            slopeSeries.Name = "Best-Fit Slope";
            inflowCht.Charts[0].Series.Add(slopeSeries);
            elevSeries.BorderStyle.Color = Color.Red;
            elevSeries.BorderStyle.Pattern = LinePattern.Dash;
            elevSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
            elevSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("Best-Fit Slope");

            slopeSeries.XValues.Add(0);
            slopeSeries.Values.Add(elevProfile[0].elev);
            slopeSeries.XValues.Add(radius);
            slopeSeries.Values.Add(elevProfile[0].elev + radius * slope);

            // Add point for turbine location
            NPointSeries turbineSite = new NPointSeries();
            turbineSite.DataLabelStyle.Visible = false;
            turbineSite.UseXValues = true;
            turbineSite.Name = "Turbine Site";
            inflowCht.Charts[0].Series.Add(turbineSite);
            turbineSite.InteractivityStyle.Tooltip = new NTooltipAttribute("Turbine site");
            turbineSite.XValues.Add(radius);
            turbineSite.Values.Add(thisTurb.elev);

            inflowCht.Refresh();

        }
    }

}
