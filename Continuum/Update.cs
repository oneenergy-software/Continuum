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

namespace ContinuumNS
{
    public class Update
    {

        ListViewItem objListItem = new ListViewItem();

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
            LossTextboxes(thisInst);            
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

            // btnViewModNLCD is modified in Update.LC_KeySelected sub

            if (thisInst.metList.ThisCount > 0)
                thisInst.btnImportTAB.BackColor = Color.MediumSeaGreen;
            else
                thisInst.btnImportTAB.BackColor = Color.LightCoral;

            if ((thisInst.modelList.ModelCount > 1 && thisInst.metList.ThisCount > 1) || (thisInst.metList.ThisCount == 1 && thisInst.metList.expoIsCalc && thisInst.modelList.ModelCount > 0)) {
                thisInst.btnAnalyzeMets.BackColor = Color.MediumSeaGreen;
                thisInst.btnAnalyzeMets.Invoke(new Action(() => thisInst.btnAnalyzeMets.Enabled = false));
            //    thisInst.btnAnalyzeMets.Enabled = false;
            }
            else {
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

            if (thisInst.turbineList.turbineCalcsDone == true)
                thisInst.btnGenTurbEsts.BackColor = Color.MediumSeaGreen;
            else
                thisInst.btnGenTurbEsts.BackColor = Color.LightCoral;

            // Gross Turb Ests tab

            if (thisInst.turbineList.PowerCurveCount > 0)
                thisInst.btnImportCRV.BackColor = Color.MediumSeaGreen;
            else
                thisInst.btnImportCRV.BackColor = Color.LightCoral;

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

        }

        public void LossTextboxes(Continuum thisInst) // Updates losses textboxes on Net Turb Ests tab
        {

            Wake_Model thisWakeModel = null;

            if (thisInst.wakeModelList.NumWakeModels > 0)
                thisWakeModel = thisInst.GetSelectedWakeModel();
            else
                thisWakeModel = null;

            if (thisInst.metList.ThisCount == 0) return;
            int numWD = thisInst.GetNumWD();

            if (numWD == 0) return;
            bool isCalibrated = thisInst.GetSelectedModel("Net");

            double overallWakeLoss = 0;

            if (thisWakeModel != null)
                overallWakeLoss = thisInst.turbineList.GetOverallWakeLoss(thisWakeModel, numWD, isCalibrated);

            thisInst.txtWakeLoss.Text = Math.Round(overallWakeLoss * 100, 2).ToString() + " %";

            double overallAvailLoss = 100 * (1 - (1 - thisInst.otherLosses.Turb_Avail_Loss) * (1 - thisInst.otherLosses.BOP_Avail_Loss));
            thisInst.txtAvailLoss.Text = Math.Round(overallAvailLoss, 2).ToString() + " %";

            double overallElecLoss = 100 - 100 * (1 - thisInst.otherLosses.Electrical_Loss);
            thisInst.txtElecLoss.Text = Math.Round(overallElecLoss, 2).ToString() + " %";

            double overallEnviroLoss = 100 - 100 * (1 - thisInst.otherLosses.Icing_Loss) * (1 - thisInst.otherLosses.Blade_Soil_Loss) *
                                        (1 - thisInst.otherLosses.Blade_Degrade_Loss) * (1 - thisInst.otherLosses.HighLowTemp_Loss);
            thisInst.txtEnviroLoss.Text = Math.Round(overallEnviroLoss, 2).ToString() + " %";


            double overallTurbPerfLoss = 100 - 100 * (1 - thisInst.otherLosses.Power_Crv_Loss) * (1 - thisInst.otherLosses.Turbulence_Loss);
            thisInst.txtTurbPerfLoss.Text = Math.Round(overallTurbPerfLoss, 2).ToString() + " %";

            double overallCurtailLoss = 100 - 100 * (1 - thisInst.otherLosses.Grid_Curtail_Loss) * (1 - thisInst.otherLosses.Environ_Curtail_Loss)
                                                 * (1 - thisInst.otherLosses.Wake_Sect_Curtail_Loss);
            thisInst.txtCurtailLoss.Text = Math.Round(overallCurtailLoss, 2) + " %";

            double totalLoss = thisInst.otherLosses.Get_Total_Losses();
            totalLoss = 1 - (1 - totalLoss) * (1 - overallWakeLoss);
            thisInst.txtTotalLosses.Text = Math.Round(totalLoss * 100, 2).ToString() + " %";

        }

        public void TurbsByString(Continuum thisInst) // Updates net turbine  plot that shows estimates for each specified string
        {
            int dist;
            double maxVal = 0;
            double minVal = 10000000;
            Turbine lastTurb = null;

            TopoInfo topo = new TopoInfo();           
            
            int WD_Ind = thisInst.GetWD_ind("Net");
            if (thisInst.metList.ThisCount == 0) return;
            int numWD = thisInst.GetNumWD();
            bool isCalibrated = thisInst.GetSelectedModel("Net");
            Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();

            NChartControl turbsByString = thisInst.chtTurbByString_Nev;
            turbsByString.Charts[0].Series.Clear();
            turbsByString.Labels.Clear();
            turbsByString.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            turbsByString.Controller.Tools.Add(tooltip);

            if (thisWakeModel == null || numWD == 0 || WD_Ind == -1) {
                turbsByString.Refresh();
                return;
            }
                        
            int allStringCount = thisInst.turbineList.NumStrings;
            int plotStringCount = thisInst.chkStrings.CheckedItems.Count;

            int whatToPlot;
            try {
                whatToPlot = thisInst.cboWakePlot.SelectedIndex;
                if (whatToPlot == -1) {
                    turbsByString.Refresh();
                    return;
                }
            }
            catch {
                turbsByString.Refresh();
                return;
            }

            if (plotStringCount > 0) {

                double[] stringXDist = new double[plotStringCount];
                double[] turbParam = new double[plotStringCount];

                int plotInd = 0;

                if (thisInst.turbineList.turbineCalcsDone == true)
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

                        for (int j = 0; j < plotStringCount; j++) {
                            if (thisInst.chkStrings.CheckedItems[j].ToString() == (i + 1).ToString()) {
                                isChecked = true;
                                break;
                            }
                        }

                        if (isChecked == true)
                        {
                            for (int j = 0; j < thisInst.turbineList.TurbineCount; j++)
                            {
                                Turbine thisTurb = thisInst.turbineList.turbineEsts[j];
                                Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(isCalibrated, thisWakeModel);
                                Turbine.Net_Energy_Est netEst = thisTurb.GetNetEnergyEst(isCalibrated, thisWakeModel);                                                

                                if (thisTurb.stringNum == i + 1)
                                {
                                    if (turbInd == 0) {
                                        dist = 0;
                                        lastTurb = thisTurb;
                                        stringXDist[turbInd] = 0;
                                    }
                                    else {
                                        dist = (int)topo.CalcDistanceBetweenPoints(thisTurb.UTMX, thisTurb.UTMY, lastTurb.UTMX, lastTurb.UTMY);
                                        stringXDist[turbInd] = dist + stringXDist[turbInd - 1];
                                        lastTurb = thisTurb;
                                    }

                                    if (whatToPlot == 0) {
                                        if (WD_Ind == numWD)
                                            turbParam[turbInd] = avgEst.WS;
                                        else
                                            turbParam[turbInd] = avgEst.sectorWS[WD_Ind];
                                    }
                                    else if (whatToPlot == 1) {
                                        if (WD_Ind == numWD)
                                            turbParam[turbInd] = netEst.wakeLoss;
                                        else
                                            turbParam[turbInd] = netEst.sectorWakeLoss[WD_Ind];
                                    }
                                    else {
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

                            for (int j = 0; j < Num_turbs_in_str; j++) {
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
            turbsByString.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "distance, m";
            string yLabel1 = "";

            if (whatToPlot == 0) {
                header = "Net WS Estimates along Turbine Strings";
                yLabel1 = "Wind Speed, m/s";
            }
            else if (whatToPlot == 1) {
                header = "Wake Loss Estimate along Turbine Strings";
                yLabel1 = "Wake Loss, %";
            }
            else {
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

                try {
                    plotInd = thisInst.cboWakePlot.SelectedIndex;
                    if (plotInd == -1)
                        thisInst.cboWakePlot.SelectedIndex = 0;
                }
                catch  {
                    return;
                }

                if (thisInst.wakeModelList.NumWakeGridMaps == 0)
                    return;

                thisWakeModel = thisInst.GetSelectedWakeModel();
                thisGrid = thisInst.GetSelectedWakeGrid();

                //  thisInst.cht3DWakeLoss.ChartGroups.Group0.ChartData.SetGrid.ColumnCount = thisGrid.numX
                //  thisInst.cht3DWakeLoss.ChartGroups.Group0.ChartData.SetGrid.RowCount = thisGrid.numY

                if (thisGrid.isComplete == false)
                    return;

                wakeSurface.Data.SetGridSize(thisGrid.numX, thisGrid.numY);

                int WD_Ind = thisInst.GetWD_ind("Net");
                int numWD = thisInst.GetNumWD();
                double thisMin = 0;
                double thisMax = 0;
                int newNumLevels = 10;
                double intWidth = 0;

                try {
                    thisMin = Convert.ToSingle(thisInst.txtWakeMin.Text);
                }
                catch  {
                    thisMin = thisInst.wakeModelList.FindMin(thisGrid, WD_Ind, plotInd) * 0.99f;
                    thisInst.txtWakeMin.Text = Math.Round(thisMin, 3).ToString();
                }

                try {
                    thisMax = Convert.ToSingle(thisInst.txtWakeMax.Text);
                }
                catch {
                    thisMax = thisInst.wakeModelList.FindMax(thisGrid, WD_Ind, plotInd) * 1.01f;
                    thisInst.txtWakeMax.Text = Math.Round(thisMax, 3).ToString();
                }

                if (thisMin == thisMax) {
                    thisMin = thisMin - thisMin * 0.1f;
                    thisMax = thisMax + thisMax * 0.1f;
                }

                try {
                    intWidth = Convert.ToSingle(thisInst.txtWakeInterval.Text);
                }
                catch  {
                    intWidth = (thisMax - thisMin) / (newNumLevels - 1);
                    thisInst.txtWakeInterval.Text = Math.Round(intWidth, 2).ToString();
                }

                if (thisInst.chkWakeAuto.Checked == true) {
                    thisMin = thisInst.wakeModelList.FindMin(thisGrid, WD_Ind, plotInd) * 0.99f;
                    thisMax = thisInst.wakeModelList.FindMax(thisGrid, WD_Ind, plotInd) * 1.01f;

                    if (plotInd == 0) {
                        thisMin = Math.Round(thisMin, 2);
                        thisMax = Math.Round(thisMax, 2);

                        if (thisMin == thisMax) {
                            thisMin = thisMin - thisMin * 0.02f;
                            thisMax = thisMax + thisMax * 0.02f;
                        }
                        intWidth = (thisMax - thisMin) / (newNumLevels - 1);

                        intWidth = Math.Round(intWidth, 2);
                    }
                    else if (plotInd == 1) {
                        thisMin = Math.Round(thisMin, 1);
                        thisMax = Math.Round(thisMax, 1);

                        if (thisMin == thisMax) {
                            thisMin = thisMin - thisMin * 0.02f;
                            thisMax = thisMax + thisMax * 0.02f;
                        }
                        intWidth = (thisMax - thisMin) / (newNumLevels - 1);

                        intWidth = Math.Round(intWidth, 2);
                    }
                    else {
                        thisMin = Math.Round(thisMin, 0);
                        thisMax = Math.Round(thisMax, 0);

                        if (thisMin == thisMax) {
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
                thisInst.cht3DWakeLoss_Nev.Charts[0].DisplayOnLegend = wakeLegend;
                thisInst.cht3DWakeLoss_Nev.Panels.Add(wakeLegend);
                wakeLegend.DockMode = PanelDockMode.Right;

                WakeMapLabels(thisInst, thisGrid, plotInd, WD_Ind);
            }
            else {
                thisInst.cht3DWakeLoss_Nev.Charts[0].Series.Clear();
            }

            thisInst.cht3DWakeLoss_Nev.Refresh();
            wakedMap.Refresh();

        }

        public void WakeModelList(Continuum thisInst) // Updates list on //Net Turbine Ests// tab that lists the wake models and wake maps that have been created
        {   
            thisInst.lstWakeModels.Items.Clear();
                        
            if (thisInst.wakeModelList == null) return;            

            
            
            for (int i = 0; i < thisInst.wakeModelList.NumWakeModels; i++)
            {
                Wake_Model thisWakeModel = thisInst.wakeModelList.wakeModels[i];

                string wakeModelStr = "";

                if (thisWakeModel.wakeModelType == 0)
                    wakeModelStr = "Eddy Viscosity";
                else if (thisWakeModel.wakeModelType == 1)
                    wakeModelStr = "DAWM with EV";
                else if (thisWakeModel.wakeModelType == 2)
                    wakeModelStr = "Continuum Wake";
                    
                ListViewItem objListItem = thisInst.lstWakeModels.Items.Add(wakeModelStr);
                objListItem.SubItems.Add(thisWakeModel.comboMethod);
                objListItem.SubItems.Add(thisWakeModel.powerCurve.name);
                objListItem.SubItems.Add(thisWakeModel.wakeRechargeRate.ToString());
                objListItem.SubItems.Add(thisWakeModel.horizWakeExp.ToString());
                objListItem.SubItems.Add(thisWakeModel.ambTI.ToString());
                objListItem.SubItems.Add(thisWakeModel.DW_Spacing.ToString());
                objListItem.SubItems.Add(thisWakeModel.CW_Spacing.ToString());
                objListItem.SubItems.Add(Math.Round(thisWakeModel.ambRough,3).ToString());

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

            try {
                thisInst.lstWakeModels.Items[0].Selected = true;
            }
            catch  {
                return;
            }

            

            ColoredButtons(thisInst);
        }

        public void WakedTurbList(Continuum thisInst)
        {
            // Updates the table on Net Turbine Ests tab which lists the net turbine estimates
            thisInst.lstWakedTurbs.Items.Clear();
            int turbCount = thisInst.turbineList.TurbineCount;

            if (thisInst.turbineList.turbineCalcsDone == false || turbCount == 0)
                return;
            
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Net");
            int checkedTurbCount = 0;

            if (checkedTurbines != null)                
                checkedTurbCount = checkedTurbines.Length;                   

            Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();

            if (thisWakeModel == null)          
                return;            

            if (thisInst.metList.ThisCount == 0) return;

            int numWD = thisInst.GetNumWD();
            int WD_Ind = thisInst.GetWD_ind("Net");

            if (WD_Ind == -1) return;

            bool isCalibrated = thisInst.GetSelectedModel("Net");                       
            
            for (int i = 0; i < checkedTurbCount; i++)
            {
                Turbine thisTurb = checkedTurbines[i];
                Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(isCalibrated, thisWakeModel);
                
                if (avgEst.WS != 0)
                {
                    ListViewItem objListItem = new ListViewItem();
                    objListItem = thisInst.lstWakedTurbs.Items.Add(thisTurb.name);
                    objListItem.SubItems.Add(thisTurb.stringNum.ToString());
                    objListItem.SubItems.Add(Math.Round(thisTurb.elev, 1).ToString());

                    if (WD_Ind == numWD)
                        objListItem.SubItems.Add(Math.Round(avgEst.WS, 3).ToString());
                    else
                        objListItem.SubItems.Add(Math.Round(avgEst.sectorWS[WD_Ind], 3).ToString());

                    objListItem.SubItems.Add(Math.Round(thisTurb.GetNetAEP(thisWakeModel, isCalibrated, WD_Ind), 0).ToString());
                    objListItem.SubItems.Add(Math.Round(thisTurb.GetNetCF(thisWakeModel, isCalibrated, WD_Ind), 4).ToString("P"));
                    objListItem.SubItems.Add((thisTurb.GetWakeLoss(thisWakeModel, isCalibrated, WD_Ind)).ToString("P"));

                    if (WD_Ind == numWD)
                    {
                        objListItem.SubItems.Add(Math.Round(avgEst.weibull_k, 2).ToString());
                        objListItem.SubItems.Add(Math.Round(avgEst.weibull_A, 2).ToString());
                    }
                    else {
                        objListItem.SubItems.Add(Math.Round(avgEst.sectWeibull_k[WD_Ind], 2).ToString());
                        objListItem.SubItems.Add(Math.Round(avgEst.sectWeibull_A[WD_Ind], 2).ToString());
                    }

                }
            }
        }     

        public void MetList(Continuum thisInst) // Updates all plots and tables on all tabs that have the mets to reflect new metList
        {
            thisInst.lstMetTowers.Items.Clear(); // First clear table
            thisInst.chkMetLabels.Items.Clear(); // Clear met label table
            thisInst.chkMetlabelsStep.Items.Clear(); // Clear met label table for stepwise tab
            thisInst.chkMetLabels_Maps.Items.Clear();
            thisInst.chkMetSumm.Items.Clear(); // Clear met list in turb est tab
            thisInst.chkMetGross.Items.Clear(); // Clear met list in Met and Turb summary page                      

            int numMets = thisInst.metList.ThisCount;
            
            if (numMets > 0)
            {
                Met thisMet = thisInst.metList.metItem[0];
                thisInst.lstMetTowers.Columns[3].Text = thisMet.height + " m Wind Speed";

                for (int j = 0; j < numMets; j++) // Now repopulate it with met towers
                {
                    thisMet = thisInst.metList.metItem[j];

                    ListViewItem objListItem = thisInst.lstMetTowers.Items.Add(thisMet.name);
                    objListItem.SubItems.Add(Math.Round(thisMet.UTMX,0).ToString());
                    objListItem.SubItems.Add(Math.Round(thisMet.UTMY,0).ToString());
                    objListItem.SubItems.Add(Math.Round(thisMet.WS, 3).ToString());

                    thisInst.chkMetLabels.Items.Add(thisMet.name, true);
                    thisInst.chkMetlabelsStep.Items.Add(thisMet.name, true);
                    thisInst.chkMetLabels_Maps.Items.Add(thisMet.name, true);
                    if (thisMet.ExposureCount == 4) thisInst.chkMetSumm.Items.Add(thisMet.name, true);
                    if (thisMet.ExposureCount == 4) thisInst.chkMetGross.Items.Add(thisMet.name, true);                    
                }
            }

            thisInst.cboRR_MinSize.Items.Clear();
            thisInst.cboRR_MinSize.Text = "";
            for (int i = numMets - 1; i >= 1; i--)
                thisInst.cboRR_MinSize.Items.Add(i + " mets");

            try {
                thisInst.cboRR_MinSize.SelectedIndex = 0;
            }
            catch { }

            StartMet_Dropdown(thisInst);
            EndMet_Dropdown(thisInst);
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

            if (numPairs == 0 || WD_Ind == -1 || radiusInd == -1) {
                thisInst.lstModCrossPred.Items.Clear();
                return;
            }
            else
                thisInst.lstModCrossPred.Items.Clear();

            int numModels = thisInst.modelList.ModelCount;
            int numRadii = thisInst.radiiList.ThisCount;
            bool isCalibrated = thisInst.GetSelectedModel("Advanced");
            string[] metsUsed = thisInst.metList.GetMetsUsed();

            if (thisInst.modelList.ModelCount > 1)
                if (thisInst.modelList.models[1,0].isImported == true)
                    return;

            if (thisInst.cboAdvancedModel.SelectedItem != null)
            {
                if (numPairs > 0 && numModels > 1)
                {
                    Model[] models = thisInst.modelList.GetModels(thisInst, metsUsed, thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(), isCalibrated);

                    for (int i = 0; i < numPairs; i++)
                    {
                        thisPair = thisInst.metPairList.metPairs[i];
                        Pair_Of_Mets.WS_CrossPreds thisWS_CrossPred = thisPair.GetWS_CrossPred(models[radiusInd]);

                        ListViewItem objListItem = thisInst.lstModCrossPred.Items.Add(thisPair.met1.name);
                        objListItem.SubItems.Add(thisPair.met2.name);

                        if (WD_Ind == numWD) {
                            objListItem.SubItems.Add(Math.Round(thisWS_CrossPred.WS_Ests[0], 2).ToString());
                            objListItem.SubItems.Add((Math.Round(thisWS_CrossPred.percErr[0], 4)).ToString("P"));
                        }
                        else {
                            objListItem.SubItems.Add(Math.Round(thisWS_CrossPred.sectorWS_Ests[0, WD_Ind], 2).ToString());
                            thisErr = (thisWS_CrossPred.sectorWS_Ests[0, WD_Ind] - thisPair.met2.WS * thisPair.met2.sectorWS_Ratio[WD_Ind]) / (thisPair.met2.WS * thisPair.met2.sectorWS_Ratio[WD_Ind]);
                            objListItem.SubItems.Add((Math.Round(thisErr, 4)).ToString("P"));
                        }

                        objListItem = thisInst.lstModCrossPred.Items.Add(thisPair.met2.name);
                        objListItem.SubItems.Add(thisPair.met1.name);

                        if (WD_Ind == numWD) {
                            objListItem.SubItems.Add(Math.Round(thisWS_CrossPred.WS_Ests[1], 2).ToString());
                            objListItem.SubItems.Add((Math.Round(thisWS_CrossPred.percErr[1], 4)).ToString("P"));
                        }
                        else {
                            objListItem.SubItems.Add(Math.Round(thisWS_CrossPred.sectorWS_Ests[1, WD_Ind], 2).ToString());
                            thisErr = (thisWS_CrossPred.sectorWS_Ests[1, WD_Ind] - thisPair.met1.WS * thisPair.met1.sectorWS_Ratio[WD_Ind]) / (thisPair.met1.WS * thisPair.met1.sectorWS_Ratio[WD_Ind]);
                            objListItem.SubItems.Add((Math.Round(thisErr, 4)).ToString("P"));
                        }
                    }

                }
            }
        }

        public void modelDropdown(Continuum thisInst)
        {
            // Lists site-calibrated models on dropdown lists on Net Turb, Uncertainty and Advanced tabs
            int numModels = thisInst.modelList.ModelCount - 1; // not counting default model
            
            thisInst.cboAdvancedModel.Items.Clear();
            thisInst.cboAdvancedModel.Text = "";

            thisInst.cboGrossModel.Items.Clear();
            thisInst.cboGrossModel.Text = "";

            thisInst.cboNetModel.Items.Clear();
            thisInst.cboNetModel.Text = "";

            thisInst.cboUncertModel.Items.Clear();
            thisInst.cboUncertModel.Text = "";

            if (numModels == -1)
                return;
            
            string modelStr = "Default Model";
            thisInst.cboAdvancedModel.Items.Add(modelStr);
            thisInst.cboGrossModel.Items.Add(modelStr);
            thisInst.cboNetModel.Items.Add(modelStr);
            thisInst.cboUncertModel.Items.Add(modelStr);                
            
            if (numModels > 0)
            {
                if (thisInst.modelList.models[1,0].isImported == false)
                {
                    modelStr = "Site-Calibated Model";
                    thisInst.cboGrossModel.Items.Add(modelStr);
                    thisInst.cboNetModel.Items.Add(modelStr);
                    thisInst.cboUncertModel.Items.Add(modelStr);
                    thisInst.cboAdvancedModel.Items.Add(modelStr);                                       
                }
                else {
                    modelStr = "Imported Model";
                    thisInst.cboAdvancedModel.Items.Add(modelStr);
                    thisInst.cboGrossModel.Items.Add(modelStr);
                    thisInst.cboNetModel.Items.Add(modelStr);
                    thisInst.cboUncertModel.Items.Add(modelStr);                    
                }
            }

            if (thisInst.cboAdvancedModel.Items.Count > 1)
            {
                thisInst.cboAdvancedModel.SelectedIndex = 1;
                thisInst.cboGrossModel.SelectedIndex = 1;
                thisInst.cboNetModel.SelectedIndex = 1;
                thisInst.cboUncertModel.SelectedIndex = 1;                
            }
            else if (thisInst.cboAdvancedModel.Items.Count > 0)
            {
                thisInst.cboAdvancedModel.SelectedIndex = 0;
                thisInst.cboGrossModel.SelectedIndex = 0;
                thisInst.cboNetModel.SelectedIndex = 0;
                thisInst.cboUncertModel.SelectedIndex = 0;                
            }
        }

        public void SetDefaultCheckAdvanced(Continuum thisInst) // Specifies the default parameters shown on Path Node plot on Advanced tab
        {
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
            bool isCalibrated = thisInst.GetSelectedModel("Advanced");

            if (radiusIndex == -1) return;      
            
            int numModels = thisInst.modelList.ModelCount;
            
            int numRadii = thisInst.radiiList.ThisCount;
            int radius = thisInst.radiiList.investItem[radiusIndex].radius;

            int numMets = thisInst.metList.ThisCount;
            int numTurbines = thisInst.turbineList.TurbineCount;

            if (thisInst.modelList.ModelCount == 0 || radiusIndex == -1 || startMetStr == "" || endMetStr == "" || thisInst.metList.expoIsCalc == false)
            {
                thisInst.lstPathNodes.Items.Clear();
                return;
            }

            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.GetMaxRadius(), isCalibrated);
            Model thisModel = models[radiusIndex];

            int WD_Ind = thisInst.GetWD_ind("Advanced");
            if (thisInst.metList.ThisCount == 0) return;
            int numWD = thisInst.GetNumWD();

            if (WD_Ind == -1 || thisModel == null) {
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

            if (thisInst.cboAdvancedModel.SelectedItem == null)
                modelDropdown(thisInst);

            if (thisInst.cboAdvancedModel.SelectedItem == null)
                thisInst.cboAdvancedModel.SelectedIndex = 0;

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
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "UW roughness")
                    paramsToShow.showUWRough = true;
                else if (thisInst.chkAdvToShow.CheckedItems[i].ToString() == "DW roughness")
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

            if (paramsToShow.showUTMY == true) {
                objList.Columns.Add("UTMY");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showElevation == true) {
                objList.Columns.Add("Elev");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showP10UW == true) {
                objList.Columns.Add("P10 UW");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showP10DW == true) {
                objList.Columns.Add("P10 DW");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showUWExpo == true) {
                objList.Columns.Add("UW Expo");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showDWExpo == true) {
                objList.Columns.Add("DW Expo");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showUWRough == true) {
                objList.Columns.Add("UW roughness");
                objList.Columns[colNum].Width = 70;
                colNum++;
            }

            if (paramsToShow.showDWRough == true) {
                objList.Columns.Add("DW roughness");
                objList.Columns[colNum].Width = 70;
                colNum++;
            }

            if (paramsToShow.showUWDispH == true) {
                objList.Columns.Add("UW Disp H");
                objList.Columns[colNum].Width = 75;
                colNum++;
            }

            if (paramsToShow.showDWDispH == true) {
                objList.Columns.Add("DW Disp H");
                objList.Columns[colNum].Width = 75;
                colNum++;
            }

            if (paramsToShow.show_dWS_UWExpo == true) {
                objList.Columns.Add("dWS UW Expo");
                objList.Columns[colNum].Width = 85;
                colNum++;
            }

            if (paramsToShow.show_dWS_DWExpo == true) {
                objList.Columns.Add("dWS DW Expo");
                objList.Columns[colNum].Width = 85;
                colNum++;
            }

            if (paramsToShow.show_dWS_UW_SRDH == true) {
                objList.Columns.Add("dWS UW SRDH");
                objList.Columns[colNum].Width = 95;
                colNum++;
            }

            if (paramsToShow.show_dWS_DW_SRDH == true) {
                objList.Columns.Add("dWS DW SRDH");
                objList.Columns[colNum].Width = 95;
                colNum++;
            }

            if (paramsToShow.showWS_Est == true) {
                objList.Columns.Add("WS Est.");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showEquivWS == true) {
                objList.Columns.Add("Equiv WS");
                objList.Columns[colNum].Width = 60;
                colNum++;
            }

            if (paramsToShow.showActualWS == true) {
                objList.Columns.Add("Actual WS");
                objList.Columns[colNum].Width = 70;
                colNum++;
            }

            NodeCollection nodeList = new NodeCollection();

            for (int i = 0; i < numMets; i++)
            {
                thisMet = thisInst.metList.metItem[i];
                if (thisMet.name == startMetStr)
                {
                    startNode = nodeList.GetMetNode(thisMet);
                    startWS = new double[numWD];

                    for (int j = 0; j < numWD; j++)
                        startWS[j] = thisMet.WS * thisMet.sectorWS_Ratio[j];

                    startAvgWS = thisMet.WS;
                }
                else if (thisMet.name == endMetStr)
                {
                    endNode = nodeList.GetMetNode(thisMet);

                    if (WD_Ind == numWD)
                        actWS = thisMet.WS;
                    else
                        actWS = thisMet.WS * thisMet.sectorWS_Ratio[WD_Ind];
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
            else {
                // End is a turbine             
                for (int i = 0; i < numTurbines; i++)
                {
                    Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                    if (thisTurb.name == endMetStr)
                    {
                        endNode = nodeList.GetTurbNode(thisTurb);

                        for (int j = 0; j < thisTurb.WSEst_Count; j++)
                        {
                            if (thisTurb.WS_Estimate[j].predictorMetName == startMetStr && thisTurb.WS_Estimate[j].radius == radius && thisInst.modelList.IsSameModel(thisModel, thisTurb.WS_Estimate[j].model))
                            {
                                try {
                                    numNodes = thisTurb.WS_Estimate[j].pathOfNodesUTMs.Length;
                                    Nodes[] Blank = null;
                                    if (numNodes > 0) pathOfNodes = nodeList.GetPathOfNodes(thisTurb.WS_Estimate[j].pathOfNodesUTMs, thisInst, ref Blank);
                                }
                                catch  {
                                    numNodes = 0;
                                }

                                nodesSectorWS = thisTurb.WS_Estimate[j].sectorWS_at_nodes;
                                nodeWS = thisTurb.WS_Estimate[j].WS_at_nodes;

                                estSectorWS = thisTurb.WS_Estimate[j].sectorWS;
                                estWS = thisTurb.WS_Estimate[j].WS;
                                break;
                            }
                        }
                    }
                }
            }

            if (startNode.UTMX == 0 || estSectorWS == null)
                return;

            PathNodeListUpdate(startNode, startMetStr, startWS, endNode, endMetStr, pathOfNodes, nodesSectorWS, nodeWS, WD_Ind,
                                  radiusIndex, numWD, thisModel, thisInst, paramsToShow, thisMet.height, estSectorWS, estWS, actWS);

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

            if (pathOfNodes != null)
                numNodes = pathOfNodes.Length;
            else
                numNodes = 0;

            bool gotSR = thisInst.topo.gotSR;
            bool useFlowSep = thisInst.topo.useSepMod;

            objListItem = thisInst.lstPathNodes.Items.Add(startMetStr);

            if (WD_Ind != numWD) {
                objList_UW = thisInst.lstPathNodes_UW.Items.Add(startMetStr);
                objList_DW = thisInst.lstPathNodes_DW.Items.Add(startMetStr);
            }

            if (WD_Ind == numWD)
            {
                P10_DW_1 = startNode.gridStats.GetOverallP10(startNode.windRose, radiusIndex, "DW");
                P10_UW_1 = startNode.gridStats.GetOverallP10(startNode.windRose, radiusIndex, "UW");

                UWExpo_1 = startNode.expo[radiusIndex].GetOverallValue(startNode.windRose, "Expo", "UW");
                DWExpo_1 = startNode.expo[radiusIndex].GetOverallValue(startNode.windRose, "Expo", "DW");
            }
            else {
                P10_DW_1 = startNode.gridStats.stats[radiusIndex].P10_DW[WD_Ind];
                P10_UW_1 = startNode.gridStats.stats[radiusIndex].P10_UW[WD_Ind];

                UWExpo_1 = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD_Ind, "UW", "Expo");
                DWExpo_1 = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD_Ind, "DW", "Expo");
            }

            AddUTMsP10sExposToPathNodeList(paramsToShow, startNode.UTMX, startNode.UTMY, startNode.elev, P10_UW_1, P10_DW_1, UWExpo_1, DWExpo_1);

            if (gotSR == true)
            {
                if (WD_Ind == numWD) {
                    UW_SR_1 = startNode.expo[radiusIndex].GetOverallValue(startNode.windRose, "SR", "UW");
                    UW_DH_1 = startNode.expo[radiusIndex].GetOverallValue(startNode.windRose, "DH", "UW");
                    DW_SR_1 = startNode.expo[radiusIndex].GetOverallValue(startNode.windRose, "SR", "DW");
                    DW_DH_1 = startNode.expo[radiusIndex].GetOverallValue(startNode.windRose, "DH", "DW");
                }
                else {
                    UW_SR_1 = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD_Ind, "UW", "SR");
                    UW_DH_1 = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD_Ind, "UW", "DH");
                    DW_SR_1 = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD_Ind, "DW", "SR");
                    DW_DH_1 = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD_Ind, "DW", "DH");
                }
            }

            for (int WD = 0; WD < numWD; WD++)
            {
                sectWS_1[WD] = startWS[WD];
                P10_DW_All[WD] = startNode.gridStats.stats[radiusIndex].P10_DW[WD];
                P10_UW_All[WD] = startNode.gridStats.stats[radiusIndex].P10_UW[WD];
                UWExpo_1_All[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD, "UW", "Expo");
                DWExpo_1_All[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD, "DW", "Expo");

                if (gotSR == true)
                {
                    UW_SR_All_1[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD, "UW", "SR");
                    UW_DH_All_1[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD, "UW", "DH");
                    DW_SR_All_1[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD, "DW", "SR");
                    DW_DH_All_1[WD] = startNode.expo[radiusIndex].GetWgtAvg(startNode.windRose, WD, "DW", "DH");
                }
            }

            WR_Pred = startNode.windRose;

            if (numNodes == 0)
            {
                WS_Equiv = thisInst.modelList.GetWS_Equiv(startNode.windRose, endNode.windRose, sectWS_1);
                node2Coords.UTMX = endNode.UTMX;
                node2Coords.UTMY = endNode.UTMY;
            }
            else {
                WS_Equiv = thisInst.modelList.GetWS_Equiv(startNode.windRose, pathOfNodes[0].windRose, sectWS_1);
                node2Coords.UTMX = pathOfNodes[0].UTMX;
                node2Coords.UTMY = pathOfNodes[0].UTMY;
            }

            if (WD_Ind == numWD) {
                WS_1 = startNode.CalcAvgWS(sectWS_1);
                WS_Equiv_1 = startNode.CalcAvgWS(WS_Equiv);
            }
            else {
                WS_1 = sectWS_1[WD_Ind];
                WS_Equiv_1 = WS_Equiv[WD_Ind];
            }

            node1.flowSepNodes = startNode.flowSepNodes;
            node1 = startNode;

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

                // Calculate equivalent wind speed at predictor based on target wind rose
                WS_Equiv = thisInst.modelList.GetWS_Equiv(WR_Pred, node2.windRose, sectWS_1);

                if (WD_Ind == numWD)
                {
                    WS_1 = node1.CalcAvgWS(sectWS_1);
                    WS_Equiv_1 = startNode.CalcAvgWS(WS_Equiv);
                }
                else {
                    WS_1 = sectWS_1[WD_Ind];
                    WS_Equiv_1 = WS_Equiv[WD_Ind];
                }

                if (paramsToShow.showEquivWS) objListItem.SubItems.Add(Math.Round(WS_Equiv_1, 2).ToString());
                if (WD_Ind != numWD) WS_1 = WS_Equiv[WD_Ind];

                for (int WD = 0; WD < numWD; WD++)
                {
                    P10_DW_All[WD] = (P10_DW_All[WD] + node2.gridStats.stats[radiusIndex].P10_DW[WD]) / 2;
                    P10_UW_All[WD] = (P10_UW_All[WD] + node2.gridStats.stats[radiusIndex].P10_UW[WD]) / 2;
                    UWExpo_2_All[WD] = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD, "UW", "Expo");
                    DWExpo_2_All[WD] = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD, "DW", "Expo");

                    avgUW[WD] = (UWExpo_1_All[WD] + UWExpo_2_All[WD]) / 2;
                    avgDW[WD] = (DWExpo_1_All[WD] + DWExpo_2_All[WD]) / 2;
                }

                if (WD_Ind == numWD)
                {
                    P10_UW_2 = node2.gridStats.GetOverallP10(node2.windRose, radiusIndex, "UW");
                    P10_DW_2 = node2.gridStats.GetOverallP10(node2.windRose, radiusIndex, "DW");
                    UWExpo_2 = node2.expo[radiusIndex].GetOverallValue(node2.windRose, "Expo", "UW");
                    DWExpo_2 = node2.expo[radiusIndex].GetOverallValue(node2.windRose, "Expo", "DW");
                }
                else
                {
                    P10_DW_2 = node2.gridStats.stats[radiusIndex].P10_DW[WD_Ind];
                    P10_UW_2 = node2.gridStats.stats[radiusIndex].P10_UW[WD_Ind];
                    UWExpo_2 = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD_Ind, "UW", "Expo");
                    DWExpo_2 = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD_Ind, "DW", "Expo");
                }

                objListItem = thisInst.lstPathNodes.Items.Add("Node " + (i + 1));
                AddUTMsP10sExposToPathNodeList(paramsToShow, node2.UTMX, node2.UTMY, node2.elev, P10_UW_2, P10_DW_2, UWExpo_2, DWExpo_2);

                if (gotSR == true)
                {
                    if (WD_Ind == numWD) {
                        UW_SR_2 = node2.expo[radiusIndex].GetOverallValue(node2.windRose, "SR", "UW");
                        UW_DH_2 = node2.expo[radiusIndex].GetOverallValue(node2.windRose, "DH", "UW");
                        DW_SR_2 = node2.expo[radiusIndex].GetOverallValue(node2.windRose, "SR", "DW");
                        DW_DH_2 = node2.expo[radiusIndex].GetOverallValue(node2.windRose, "DH", "DW");
                    }
                    else {
                        UW_SR_2 = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD_Ind, "UW", "SR");
                        UW_DH_2 = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD_Ind, "UW", "DH");
                        DW_SR_2 = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD_Ind, "DW", "SR");
                        DW_DH_2 = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD_Ind, "DW", "DH");
                    }

                    if (paramsToShow.showUWRough) objListItem.SubItems.Add(Math.Round(UW_SR_2, 2).ToString());
                    if (paramsToShow.showDWRough) objListItem.SubItems.Add(Math.Round(DW_SR_2, 2).ToString());
                    if (paramsToShow.showUWDispH) objListItem.SubItems.Add(Math.Round(UW_DH_2, 2).ToString());
                    if (paramsToShow.showDWDispH) objListItem.SubItems.Add(Math.Round(DW_DH_2, 2).ToString());

                    deltaWS_UW_SRDH = 0;
                    deltaWS_DW_SRDH = 0;
                }
                else {
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

                        deltaWS_UWExpo = deltaWS_UWExpo + UW_CoeffsDeltas[numUW_CoeffsDeltas - 1].deltaWS_Expo * node2.windRose[WD];
                        deltaWS_DWExpo = deltaWS_DWExpo + DW_CoeffsDeltas[numDW_CoeffsDeltas - 1].deltaWS_Expo * node2.windRose[WD];
                    }
                }
                else {
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
                        UW_SR_All_2[WD] = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD, "UW", "SR");
                        UW_DH_All_2[WD] = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD, "UW", "DH");
                        UW_Stab_Corr_2[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, UW_SR_All_2[WD], useFlowSep, "UW");

                        DW_Stab_Corr_1[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, DW_SR_All_1[WD], useFlowSep, "DW");
                        DW_SR_All_2[WD] = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD, "DW", "SR");
                        DW_DH_All_2[WD] = node2.expo[radiusIndex].GetWgtAvg(node2.windRose, WD, "DW", "DH");
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
                else {
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

                    if (gotSR == true) {
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
                WR_Pred = node2.windRose;
                node1Coords = node2Coords;

            }

            WS_Equiv = thisInst.modelList.GetWS_Equiv(WR_Pred, endNode.windRose, sectWS_1);
            node2Coords.UTMX = endNode.UTMX;
            node2Coords.UTMY = endNode.UTMY;
            node2 = endNode;

            if (WD_Ind == numWD) {
                WS_1 = node1.CalcAvgWS(sectWS_1);
                WS_Equiv_1 = endNode.CalcAvgWS(WS_Equiv);
            }
            else {
                WS_1 = sectWS_1[WD_Ind];
                WS_Equiv_1 = WS_Equiv[WD_Ind];
            }

            if (paramsToShow.showEquivWS) objListItem.SubItems.Add(Math.Round(WS_Equiv_1, 2).ToString());
            if (WD_Ind != numWD) WS_1 = WS_Equiv[WD_Ind];

            if (WD_Ind == numWD)
            {
                P10_UW_2 = endNode.gridStats.GetOverallP10(endNode.windRose, radiusIndex, "UW");
                P10_DW_2 = endNode.gridStats.GetOverallP10(endNode.windRose, radiusIndex, "DW");
                UWExpo_2 = endNode.expo[radiusIndex].GetOverallValue(endNode.windRose, "Expo", "UW");
                DWExpo_2 = endNode.expo[radiusIndex].GetOverallValue(endNode.windRose, "Expo", "DW");
            }
            else {
                P10_DW_2 = endNode.gridStats.stats[radiusIndex].P10_DW[WD_Ind];
                P10_UW_2 = endNode.gridStats.stats[radiusIndex].P10_UW[WD_Ind];
                UWExpo_2 = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD_Ind, "UW", "Expo");
                DWExpo_2 = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD_Ind, "DW", "Expo");
            }

            objListItem = thisInst.lstPathNodes.Items.Add(endStr);
            AddUTMsP10sExposToPathNodeList(paramsToShow, endNode.UTMX, endNode.UTMY, endNode.elev, P10_UW_2, P10_DW_2, UWExpo_2, DWExpo_2);

            for (int WD = 0; WD < numWD; WD++)
            {
                P10_DW_All[WD] = (P10_DW_All[WD] + endNode.gridStats.stats[radiusIndex].P10_DW[WD]) / 2;
                P10_UW_All[WD] = (P10_UW_All[WD] + endNode.gridStats.stats[radiusIndex].P10_UW[WD]) / 2;
                UWExpo_2_All[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD, "UW", "Expo");
                DWExpo_2_All[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD, "DW", "Expo");

                avgUW[WD] = (UWExpo_1_All[WD] + UWExpo_2_All[WD]) / 2;
                avgDW[WD] = (DWExpo_1_All[WD] + DWExpo_2_All[WD]) / 2;
            }

            if (gotSR == true)
            {
                if (WD_Ind == numWD)
                {
                    UW_SR_2 = endNode.expo[radiusIndex].GetOverallValue(endNode.windRose, "SR", "UW");
                    UW_DH_2 = endNode.expo[radiusIndex].GetOverallValue(endNode.windRose, "DH", "UW");
                    DW_SR_2 = endNode.expo[radiusIndex].GetOverallValue(endNode.windRose, "SR", "DW");
                    DW_DH_2 = endNode.expo[radiusIndex].GetOverallValue(endNode.windRose, "DH", "DW");
                }
                else {
                    UW_SR_2 = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD_Ind, "UW", "SR");
                    UW_DH_2 = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD_Ind, "UW", "DH");
                    DW_SR_2 = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD_Ind, "DW", "SR");
                    DW_DH_2 = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD_Ind, "DW", "DH");
                }

                if (paramsToShow.showUWRough) objListItem.SubItems.Add(Math.Round(UW_SR_2, 2).ToString());
                if (paramsToShow.showDWRough) objListItem.SubItems.Add(Math.Round(DW_SR_2, 2).ToString());
                if (paramsToShow.showUWDispH) objListItem.SubItems.Add(Math.Round(UW_DH_2, 2).ToString());
                if (paramsToShow.showDWDispH) objListItem.SubItems.Add(Math.Round(DW_DH_2, 2).ToString());

            }
            else {
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
                                                            WD, radiusIndex, node1.flowSepNodes, endNode.flowSepNodes, WS_Equiv[WD], useFlowSep,  node1Coords, node2Coords);

                    if (UW_CoeffsDeltas != null)
                        numUW_CoeffsDeltas = UW_CoeffsDeltas.Length;

                    DW_CoeffsDeltas = thisInst.modelList.Get_DeltaWS_DW_Expo(sectWS_1[WD], UWExpo_1_All[WD], UWExpo_2_All[WD], DWExpo_1_All[WD], DWExpo_2_All[WD], P10_UW_All[WD], P10_DW_All[WD], thisModel, WD, useFlowSep);

                    if (DW_CoeffsDeltas != null)
                        numDW_CoeffsDeltas = DW_CoeffsDeltas.Length;

                    deltaWS_UWExpo = deltaWS_UWExpo + UW_CoeffsDeltas[numUW_CoeffsDeltas - 1].deltaWS_Expo * endNode.windRose[WD];
                    deltaWS_DWExpo = deltaWS_DWExpo + DW_CoeffsDeltas[numDW_CoeffsDeltas - 1].deltaWS_Expo * endNode.windRose[WD];

                }
            }
            else {
                WS_1 = WS_Equiv[WD_Ind];
                UW_CoeffsDeltas = thisInst.modelList.Get_DeltaWS_UW_Expo(UWExpo_1, UWExpo_2, DWExpo_1, DWExpo_2, (P10_UW_1 + P10_UW_2) / 2, (P10_DW_1 + P10_DW_2) / 2, thisModel,
                                                            WD_Ind, radiusIndex, node1.flowSepNodes, node2.flowSepNodes, WS_1, useFlowSep, node1Coords, node2Coords);

                if (UW_CoeffsDeltas != null)
                    numUW_CoeffsDeltas = UW_CoeffsDeltas.Length;

                for (int Coeff_ind = 0; Coeff_ind < numUW_CoeffsDeltas; Coeff_ind++)
                {
                    if (UW_CoeffsDeltas[Coeff_ind].flowType != "Total") {
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
                        UW_SR_All_2[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD, "UW", "SR");
                        UW_DH_All_2[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD, "UW", "DH");
                        UW_Stab_Corr_2[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, UW_SR_All_2[WD], useFlowSep, "UW");

                        deltaWS_UW_SRDH = deltaWS_UW_SRDH + thisInst.modelList.GetDeltaWS_SRDH(sectWS_1[WD], HH, UW_SR_All_1[WD], UW_SR_All_2[WD], UW_DH_All_1[WD],
                            UW_DH_All_2[WD], UW_Stab_Corr_1[WD], UW_Stab_Corr_2[WD]) * endNode.windRose[WD];

                        DW_SR_All_2[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD, "DW", "SR");
                        DW_DH_All_2[WD] = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD, "DW", "DH");

                        DW_Stab_Corr_1[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, DW_SR_All_1[WD], useFlowSep, "UW");
                        DW_Stab_Corr_2[WD] = thisModel.GetStabilityCorrection(avgUW[WD], avgDW[WD], WD, DW_SR_All_2[WD], useFlowSep, "UW");

                        deltaWS_DW_SRDH = deltaWS_DW_SRDH + thisInst.modelList.GetDeltaWS_SRDH(sectWS_1[WD], HH, DW_SR_All_1[WD], DW_SR_All_2[WD], DW_DH_All_1[WD],
                            DW_DH_All_2[WD], DW_Stab_Corr_1[WD], DW_Stab_Corr_2[WD]) * endNode.windRose[WD];
                    }
                }
                else {
                    UW_Stab_Corr_1[WD_Ind] = thisModel.GetStabilityCorrection(avgUW[WD_Ind], avgDW[WD_Ind], WD_Ind, UW_SR_All_1[WD_Ind], useFlowSep, "UW");
                    UW_SR_All_2[WD_Ind] = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD_Ind, "UW", "SR");
                    UW_DH_All_2[WD_Ind] = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD_Ind, "UW", "DH");
                    UW_Stab_Corr_2[WD_Ind] = thisModel.GetStabilityCorrection(avgUW[WD_Ind], avgDW[WD_Ind], WD_Ind, UW_SR_All_2[WD_Ind], useFlowSep, "UW");

                    deltaWS_UW_SRDH = thisInst.modelList.GetDeltaWS_SRDH(sectWS_1[WD_Ind], HH, UW_SR_All_1[WD_Ind], UW_SR_All_2[WD_Ind], UW_DH_All_1[WD_Ind],
                        UW_DH_All_2[WD_Ind], UW_Stab_Corr_1[WD_Ind], UW_Stab_Corr_2[WD_Ind]);

                    DW_SR_All_2[WD_Ind] = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD_Ind, "DW", "SR");
                    DW_DH_All_2[WD_Ind] = endNode.expo[radiusIndex].GetWgtAvg(endNode.windRose, WD_Ind, "DW", "DH");

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
            else {
                if (paramsToShow.show_dWS_UW_SRDH) objListItem.SubItems.Add("");
                if (paramsToShow.show_dWS_DW_SRDH) objListItem.SubItems.Add("");
            }

            if (WD_Ind == numWD) {
                if (paramsToShow.showWS_Est) objListItem.SubItems.Add(Math.Round(estWS, 2).ToString());
            }
            else {
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
                            if (thisInst.metPairList.roundRobinEsts[j].metSubSize < lastSize) {
                                lastSize = thisInst.metPairList.roundRobinEsts[j].metSubSize;
                                dropInd = j;
                            }
                        }
                    }
                    else {
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

            try {
                thisInst.cboRoundRobin.SelectedIndex = 0;
            }
            catch  {
                return;
            }

        }

        public void RoundRobinIndivResults(Continuum thisInst)
        {
            // Updates table on Uncertainty tab showing the results of Round Robin estimates
            int RR_ind;
            string dropText;
            
            int numMetsinRR;
            thisInst.lstRR_AllErr.Items.Clear();

            if (thisInst.metPairList.RoundRobinCount > 0) {
                try {
                    RR_ind = thisInst.cboRoundRobin.SelectedIndex;
                    dropText = thisInst.cboRoundRobin.SelectedItem.ToString();
                }
                catch  {
                    return;
                }

                string[] textSplit = dropText.Split(Convert.ToChar(" "));
                try {
                    numMetsinRR = Convert.ToInt16(textSplit[1]);
                }
                catch {
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

            if (thisInst.turbineList.turbineCalcsDone == false) {
                ChtCtrl.Refresh();
                return;
            }

            // Find whether plotting AEP or WS
            bool plotWS = false;
            string powerCurve = "";
            int numWD = thisInst.GetNumWD();
            int Sel_Ind;
                        
            try {
                Sel_Ind = thisInst.cboUncert_WS_AEP.SelectedIndex;
            }
            catch  {
                thisInst.cboUncert_WS_AEP.SelectedIndex = 0;
            }

            bool isCalibrated = thisInst.GetSelectedModel("Uncertainty");

            if (thisInst.cboUncert_WS_AEP.SelectedIndex == 0)
                plotWS = true;
            else
                plotWS = false;                       

            int numTurbines = thisInst.turbineList.TurbineCount;

            if (plotWS == false)
            {                
                try {
                    powerCurve = thisInst.cboUncertPowerCrv.SelectedItem.ToString();
                }
                catch  {
                    thisInst.cboUncertPowerCrv.SelectedIndex = 0;
                    powerCurve = thisInst.cboUncertPowerCrv.SelectedItem.ToString();
                }

                if (powerCurve == "No power Curves Imported")
                    return;
            }

            if (numTurbines > 0) {
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
                            Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(isCalibrated, null);
                            Turbine.Gross_Energy_Est grossEnergy = thisTurb.GetGrossEnergyEst(isCalibrated);

                            if (plotWS == true) {
                                P50_Series.Values.Add(avgEst.WS);
                                P90_Series.Values.Add(avgEst.WS - avgEst.WS * avgEst.uncert * 1.28155);
                                P99_Series.Values.Add(avgEst.WS - avgEst.WS * avgEst.uncert * 2.326);
                            }
                            else {
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

                    if (plotWS == true) {
                        string yLabel1 = "Wind Speed, m/s";
                        ChtCtrl.Labels.AddHeader("Turbine Wind Speed Ests: P50, P90 & P99");
                        ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = yLabel1;
                    }
                    else {
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

            if (numMetsUsed > 0) {
                metsUsed = new string[numMetsUsed];
                for (int i = 0; i <= numMetsUsed - 1; i++)
                    metsUsed[i] = thisInst.metList.metItem[i].name;
            }
           
            // List with overall RMS
            int lastSize = 0;
            int thisSize = 0;

            // List from small to large
            for (int i = 0; i < numRR; i++) {
                if (lastSize == 0) { // Find smallest RR size
                    lastSize = thisInst.metPairList.roundRobinEsts[0].metSubSize;
                    thisRR = thisInst.metPairList.roundRobinEsts[0];
                    for (int j = 1; j < numRR; j++) {
                        if (thisInst.metPairList.roundRobinEsts[j].metSubSize < lastSize) {
                            lastSize = thisInst.metPairList.roundRobinEsts[j].metSubSize;
                            thisRR = thisInst.metPairList.roundRobinEsts[j];
                        }
                    }
                }
                else {
                    thisSize = 100;
                    for (int j = 0; j < numRR; j++) {
                        if (thisInst.metPairList.roundRobinEsts[j].metSubSize > lastSize && thisInst.metPairList.roundRobinEsts[j].metSubSize < thisSize) {
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

            for (int i = 0; i < numRR; i++) {
                thisRR = thisInst.metPairList.roundRobinEsts[i];
                bool sameMets = thisInst.metList.sameMets(thisRR.metsUsed, metsUsed);
                if (sameMets == true) {
                    numMets[RR_ind] = thisInst.metPairList.roundRobinEsts[i].metSubSize;
                    overallRMS[RR_ind] = 100 * thisInst.metPairList.roundRobinEsts[i].RMS_All;
                    RR_ind++;
                }
            }
                        
            for (int i = 0; i < numRR; i++) {
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
            bool isCalibrated = thisInst.GetSelectedModel("Advanced");
            int WD_Ind = thisInst.GetWD_ind("Advanced");
            thisInst.txtUWDWRMS.Text = "";
            thisInst.txtSectRMS.Text = "";
            int numModels = thisInst.modelList.ModelCount;
            string[] metsUsed = thisInst.metList.GetMetsUsed();

            if (thisInst.metList.ThisCount == 0 || numModels == 0 || radiusIndex == -1)
                return;
            
            int numWD = thisInst.metList.numWD;
                        
            if (thisInst.modelList.models[0, 0].RMS_Sect_WS_Est == null || thisInst.metList.expoIsCalc == false)
                return;
            
            Model[] theseModels = thisInst.modelList.GetModels(thisInst, metsUsed, thisInst.radiiList.investItem[radiusIndex].radius, thisInst.radiiList.investItem[radiusIndex].radius, isCalibrated);
            Model thisModel = theseModels[0];

            if (thisModel == null)
                return;

            string metStr;
            if (thisModel.isCalibrated == false)
                metStr = "Default Model";
            else if (thisModel.isImported == true)
                metStr = "Imported Model";
            else
                metStr = thisInst.metList.CreateMetString(thisModel.metsUsed, false);
                        
            bool Wgt_or_No;

            if (thisInst.chkWeight_RMS.Checked == true)
                Wgt_or_No = true;
            else
                Wgt_or_No = false;

            thisInst.txtUWDWRMS.Text = Math.Round(thisModel.RMS_WS_Est, 5).ToString("P");

            if (thisInst.metList.ThisCount > 1 && thisModel.RMS_Sect_WS_Est != null) {
                if (WD_Ind == numWD) {
                    double thisErr = thisModel.CalcRMS_SectorsEstError(Wgt_or_No, thisInst.metList.GetAvgWindRose());
                    thisInst.txtSectRMS.Text = Math.Round(thisErr, 5).ToString("P");
                }
                else {
                    double thisErr = thisModel.RMS_Sect_WS_Est[WD_Ind];
                    thisInst.txtSectRMS.Text = Math.Round(thisErr, 5).ToString("P");
                }
            }
        }

        public void RadiusToDisplay(string summ_or_Adv, Continuum thisInst) // Updates radius of investigation dropdown menu on Met & Turb Summary tab and Advanced tab
        {
            if (thisInst.radiiList.ThisCount > 0) {
                int numRadii = thisInst.radiiList.ThisCount;

                if (summ_or_Adv == "Summary") {
                    thisInst.cboMetSum_Rad.Items.Clear();

                    for (int i = 0; i < numRadii; i++)
                        thisInst.cboMetSum_Rad.Items.Add(thisInst.radiiList.investItem[i].radius);

                    thisInst.cboMetSum_Rad.SelectedIndex = 0;
                }
                else {
                    thisInst.cboAdvancedRad.Items.Clear();
                    for (int i = 0; i < numRadii; i++)
                        thisInst.cboAdvancedRad.Items.Add(thisInst.radiiList.investItem[i].radius);

                    thisInst.cboAdvancedRad.SelectedIndex = 0;
                }
            }
            else {
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
        }

        public void WindDirectionToDisplay(Continuum thisInst) // Updates all wind direction dropdown menus
        {
            if (thisInst.metList.ThisCount == 0) return;
            int numWD = thisInst.metList.metItem[0].windRose.Length;

            thisInst.cboAdvancedWD.Items.Clear();
            thisInst.cboSummaryWD.Items.Clear();
            thisInst.cboGrossWD.Items.Clear();
            thisInst.cboNetWD.Items.Clear();
            thisInst.cboMapWD.Items.Clear();

            for (int WD = 0; WD < numWD; WD++) {
                thisInst.cboAdvancedWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
                thisInst.cboSummaryWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
                thisInst.cboGrossWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
                thisInst.cboNetWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
                thisInst.cboMapWD.Items.Add((Math.Round((double)WD * 360 / numWD, 1) + " degs").ToString());
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

        }

        public void StartMet_Dropdown(Continuum thisInst) // Updates "Start Met" dropdown menu on Advanced tab
        {
            
            thisInst.cboStartMet.Items.Clear();

            string[] metsUsed = thisInst.metList.GetMetsUsed();
            int numMets = metsUsed.Length;

            for (int i = 0; i < numMets; i++)
                thisInst.cboStartMet.Items.Add(metsUsed[i]);

            try {
                thisInst.cboStartMet.SelectedIndex = 0;
            }
            catch {
                return;
            }
        }

        public void EndMet_Dropdown(Continuum thisInst)  // Updates "End Site" dropdown menu on Advanced tab
        {                                
            
            string Start_met_name = thisInst.GetStartMetAdvanced();
            thisInst.cboEndMet.Items.Clear();

            for (int i = 0; i < thisInst.metList.ThisCount; i++) {
                Met thisMet = thisInst.metList.metItem[i];
                if (thisMet.name != Start_met_name)
                    thisInst.cboEndMet.Items.Add(thisMet.name);
            }

            if (thisInst.turbineList.turbineCalcsDone == true) {
                for (int i = 0; i < thisInst.turbineList.TurbineCount; i++) {
                    Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                    thisInst.cboEndMet.Items.Add(thisTurb.name);
                }
            }

            if (thisInst.cboEndMet.Items.Count > 0) thisInst.cboEndMet.SelectedIndex = 0;
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

            //  Dim isChecked  bool = false
            // Dim thisMet  Met

            string Selected_str;

            try {
                Selected_str = thisInst.cboAdvancedModel.SelectedItem.ToString();
            }
            catch  {
                return;
            }

            int metStrInd = Selected_str.LastIndexOf(",");
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
                if (checkedTurbines[i].AvgWSEst_Count > 0) {
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
            Nodes[] pathOfNodes = null;

            // Figure out if turbine or met end
            string met1Str = thisInst.GetStartMetAdvanced();
            Met startMet = null;
            string endStr = thisInst.GetEndSiteAdvanced();

            for (int i = 0; i < thisInst.metList.ThisCount; i++) {
                if (thisInst.metList.metItem[i].name == met1Str) {
                    startMet = thisInst.metList.metItem[i];
                    break;
                }
            }

            bool isMetPair = false;

            for (int i = 0; i < metPairCount; i++) {
                thisPair = thisInst.metPairList.metPairs[i];
                numNodes = thisInst.metPairList.metPairs[i].WS_Pred[0, radiusIndex].nodePath.Length;
                isMetPair = false;
                if ((met1Str == thisPair.met1.name && endStr == thisPair.met2.name) || (met1Str == thisPair.met2.name && endStr == thisPair.met1.name)) {
                    isMetPair = true;
                    break;
                }
            }

            if (isMetPair == true) {
                bool isCalibrated = thisInst.GetSelectedModel("Advanced");

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
            else {
                // met to a turbine
                // Find turbine
                bool foundTurb = false;
                Turbine endTurb = null;
                for (int i = 0; i < checkedTurbCount; i++)
                {
                    //  thisTurb = thisInst.turbineList.turbineEsts[i]
                    if (checkedTurbines[i].AvgWSEst_Count > 0) {
                        if (checkedTurbines[i].name == endStr) {
                            for (int j = 0; j < checkedTurbines[i].WSEst_Count; j++)
                            {
                                if (checkedTurbines[i].WS_Estimate[j].predictorMetName == met1Str && checkedTurbines[i].WS_Estimate[j].radius == radius) {
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

                for (int i = 0; i <= endTurb.WSEst_Count - 1; i++) {
                    if (endTurb.WS_Estimate[i].predictorMetName == startMet.name && endTurb.WS_Estimate[i].radius == radius) {
                        WS_PredInd = i;
                        break;
                    }
                }

                try {
                    numNodes = endTurb.WS_Estimate[WS_PredInd].pathOfNodesUTMs.Length;
                }
                catch {
                    numNodes = 0;
                }

                if (numNodes > 0) {
                    pathOfNodes = new Nodes[numNodes];
                    Nodes[] Blank = null;
                    pathOfNodes = nodeList.GetPathOfNodes(endTurb.WS_Estimate[WS_PredInd].pathOfNodesUTMs, thisInst, ref Blank);
                }

                NPointSeries[] label_Nodes = new NPointSeries[numNodes];

                for (int j = 0; j < numNodes; j++)
                {
                    Nodes thisNode = pathOfNodes[j];
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

                for (int j = 0; j < thisInst.mapList.ThisCount; j++) {
                    if (mapToPlot == thisInst.mapList.mapItem[j].mapName) {
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

        public void LandCoverFlowSepUsedTexts(Continuum thisInst) // Updates roughness model and Flow Separation model textbox colors and text to indicate whether or not model was used
        {
            if (thisInst.topo.useSR == true) {
                thisInst.txtGross_LC_used.Text = "Roughness model used";
                thisInst.txtGross_LC_used.BackColor = Color.MediumSeaGreen;

                thisInst.txtLC_Net.Text = "Roughness model used";
                thisInst.txtLC_Net.BackColor = Color.MediumSeaGreen;                       
               
                thisInst.txtRR_LC_used.Text = "Roughness model used";
                thisInst.txtRR_LC_used.BackColor = Color.MediumSeaGreen;

                thisInst.txtUncert_LC_used.Text = "Roughness model used";
                thisInst.txtUncert_LC_used.BackColor = Color.MediumSeaGreen;

                thisInst.txtAdv_LC_used.Text = "Roughness model used";
                thisInst.txtAdv_LC_used.BackColor = Color.MediumSeaGreen;
            }
            else {
                thisInst.txtGross_LC_used.Text = "Roughness model NOT used";
                thisInst.txtGross_LC_used.BackColor = Color.LightCoral;

                thisInst.txtLC_Net.Text = "Roughness model NOT used";
                thisInst.txtLC_Net.BackColor = Color.LightCoral;

                thisInst.txtRR_LC_used.Text = "Roughness model NOT used";
                thisInst.txtRR_LC_used.BackColor = Color.LightCoral;

                thisInst.txtUncert_LC_used.Text = "Roughness model NOT used";
                thisInst.txtUncert_LC_used.BackColor = Color.LightCoral;

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
                           
                thisInst.txtUncert_FlowSep_Used.Text = "Flow Sep. model used";
                thisInst.txtUncert_FlowSep_Used.BackColor = Color.LightBlue;

                thisInst.txtAdv_FlowSep_Used.Text = "Flow Sep. model used";
                thisInst.txtAdv_FlowSep_Used.BackColor = Color.LightBlue;
            }
            else {
                thisInst.txtGross_FlowSepUsed.Text = "Flow Sep. model NOT used";
                thisInst.txtGross_FlowSepUsed.BackColor = Color.LightCoral;

                thisInst.txtNet_FlowSep_Used.Text = "Flow Sep. model NOT used";
                thisInst.txtNet_FlowSep_Used.BackColor = Color.LightCoral;

                thisInst.txtRR_FlowSep_Used.Text = "Flow Sep. model NOT used";
                thisInst.txtRR_FlowSep_Used.BackColor = Color.LightCoral;                    
                                 
                thisInst.txtUncert_FlowSep_Used.Text = "Flow Sep. model NOT used";
                thisInst.txtUncert_FlowSep_Used.BackColor = Color.LightCoral;

                thisInst.txtAdv_FlowSep_Used.Text = "Flow Sep. model NOT used";
                thisInst.txtAdv_FlowSep_Used.BackColor = Color.LightCoral;
            }

        }

        public void LC_KeySelected(Continuum thisInst) // Updates Land Cover textbox to indicate what LC key has been selected
        {
            bool isNLCD = thisInst.topo.LC_IsDefaultNLCD(thisInst.topo.LC_Key);
            bool isNALC = thisInst.topo.LC_IsDefaultNALC(thisInst.topo.LC_Key);
            bool isEULC = thisInst.topo.LC_IsDefaultEU_Corine(thisInst.topo.LC_Key);

            int numSR = 0;

            try {
                numSR = thisInst.topo.LC_Key.Length;
                thisInst.btnViewModNLCD.BackColor = Color.MediumSeaGreen;
            }
            catch  {
                thisInst.txt_LC_Key_selected.Text = "Not Selected";
                thisInst.txt_LC_Key_selected.BackColor = Color.LightCoral;
                thisInst.btnViewModNLCD.BackColor = Color.LightCoral;
                return;
            }

            if (isNLCD == true) {
                thisInst.txt_LC_Key_selected.Text = "US NLCD";
                thisInst.txt_LC_Key_selected.BackColor = Color.MediumSeaGreen;
            }
            else if (isNALC == true) {
                thisInst.txt_LC_Key_selected.Text = "North America LC";
                thisInst.txt_LC_Key_selected.BackColor = Color.MediumSeaGreen;
            }
            else if (isEULC == true) {
                thisInst.txt_LC_Key_selected.Text = "EU Corine LC";
                thisInst.txt_LC_Key_selected.BackColor = Color.MediumSeaGreen;
            }
            else {
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
            bool isCalibrated = false;
            if (thisInst.modelList.ModelCount > 1)
                isCalibrated = true;

            if (WD_Ind > numWD) return;
                       
            Met[] checkedMets = thisInst.GetCheckedMets("Summary");
            int metCount = checkedMets.Length;
            
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Summary");
            int turbCount = checkedTurbines.Length;

            for (int i = 0; i < checkedMets.Length; i++)
            {
                Met thisMet = checkedMets[i];
                objListItem = thisInst.lstMetSummary.Items.Add(thisMet.name);
                objListItem.SubItems.Add(Math.Round(thisMet.elev, 1).ToString());

                if (WD_Ind == numWD)
                    objListItem.SubItems.Add(Math.Round(thisMet.WS, 3).ToString());
                else
                    objListItem.SubItems.Add(Math.Round(thisMet.sectorWS_Ratio[WD_Ind] * thisMet.WS, 3).ToString());

                MetCollection.Weibull_params weibull = thisInst.metList.CalcWeibullParams(thisMet.WS_Dist, thisMet.sectorWS_Dist, thisMet.WS);

                if (WD_Ind == numWD)
                {
                    objListItem.SubItems.Add(Math.Round(weibull.overall_k, 3).ToString()); // weibull k
                    objListItem.SubItems.Add(Math.Round(weibull.overall_A, 3).ToString()); // weibull A

                    objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisMet.windRose, "Expo", "UW"), 3).ToString());
                    objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisMet.windRose, "Expo", "DW"), 3).ToString());

                    if (thisInst.metList.SRDH_IsCalc == true) {
                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisMet.windRose, "SR", "UW"), 3).ToString());
                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisMet.windRose, "SR", "DW"), 3).ToString());

                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisMet.windRose, "DH", "UW"), 3).ToString());
                        objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetOverallValue(thisMet.windRose, "DH", "DW"), 3).ToString());
                    }
                }
                else {
                    objListItem.SubItems.Add(Math.Round(weibull.sector_k[WD_Ind], 3).ToString()); // weibull k
                    objListItem.SubItems.Add(Math.Round(weibull.sector_A[WD_Ind], 3).ToString()); // weibull C

                    objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].expo[WD_Ind], 3).ToString());
                    objListItem.SubItems.Add(Math.Round(thisMet.expo[radiusInd].GetDW_Param(WD_Ind, "Expo"), 3).ToString());

                    if (thisInst.metList.SRDH_IsCalc == true) {
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
                if (WD_Ind < numWD)
                {
                    // Sector statistics
                    Met_WS[i] = checkedMets[i].WS * checkedMets[i].sectorWS_Ratio[WD_Ind];
                    Met_UW_Expos[i] = checkedMets[i].expo[radiusInd].expo[WD_Ind];
                    Met_DW_Expos[i] = checkedMets[i].expo[radiusInd].GetDW_Param(WD_Ind, "Expo");
                    Met_DW_min_UW_Expos[i] = Met_DW_Expos[i] - checkedMets[i].expo[radiusInd].expo[WD_Ind];
                    Met_Elevs[i] = checkedMets[i].elev;

                    if (thisInst.topo.gotSR == true) {
                        Met_UW_SRs[i] = checkedMets[i].expo[radiusInd].SR[WD_Ind];
                        Met_UW_DHs[i] = checkedMets[i].expo[radiusInd].dispH[WD_Ind];                            
                        Met_DW_SRs[i] = checkedMets[i].expo[radiusInd].GetDW_Param(WD_Ind, "SR");
                        Met_DW_DHs[i] = checkedMets[i].expo[radiusInd].GetDW_Param(WD_Ind, "DH");
                    }
                }
                else {
                    // Overall statistics
                    Met_WS[i] = checkedMets[i].WS;
                    Met_UW_Expos[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisMet.windRose, "Expo", "UW");
                    Met_DW_Expos[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisMet.windRose, "Expo", "DW");
                    Met_DW_min_UW_Expos[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisMet.windRose, "Expo", "DW") - checkedMets[i].expo[radiusInd].GetOverallValue(thisMet.windRose, "Expo", "UW");
                    Met_Elevs[i] = checkedMets[i].elev;

                    if (thisInst.topo.gotSR == true) {
                        Met_UW_SRs[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisMet.windRose, "SR", "UW");
                        Met_DW_SRs[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisMet.windRose, "SR", "DW");
                        Met_UW_DHs[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisMet.windRose, "DH", "UW");
                        Met_DW_DHs[i] = checkedMets[i].expo[radiusInd].GetOverallValue(thisMet.windRose, "DH", "DW");
                    }
                }
            }

            if (metCount > 0) {
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

            if (thisInst.turbineList.turbineCalcsDone == true && thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false)

            {
                for (int i = 0; i < turbCount; i++)
                {
                    Turbine thisTurb = checkedTurbines[i];
                    objListItem = thisInst.lstMetSummary.Items.Add(thisTurb.name);
                    objListItem.SubItems.Add(Math.Round(thisTurb.elev, 1).ToString());

                    if (thisTurb.AvgWSEst_Count == 0)
                        return;

                    Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(isCalibrated, null);

                    if (WD_Ind == numWD)
                        objListItem.SubItems.Add(Math.Round(avgEst.WS, 3).ToString());
                    else
                        objListItem.SubItems.Add(Math.Round(avgEst.sectorWS[WD_Ind], 3).ToString());

                    if (WD_Ind == numWD)
                    {
                        objListItem.SubItems.Add(Math.Round(avgEst.weibull_k, 3).ToString()); // weibull k
                        objListItem.SubItems.Add(Math.Round(avgEst.weibull_A, 3).ToString()); // weibull A

                        objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(thisTurb.windRose, "Expo", "UW"), 3).ToString());
                        objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(thisTurb.windRose, "Expo", "DW"), 3).ToString());

                        if (thisInst.topo.gotSR == true)
                        {
                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(thisTurb.windRose, "SR", "UW"), 3).ToString());
                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(thisTurb.windRose, "SR", "DW"), 3).ToString());

                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(thisTurb.windRose, "DH", "UW"), 3).ToString());
                            objListItem.SubItems.Add(Math.Round(thisTurb.expo[radiusInd].GetOverallValue(thisTurb.windRose, "DH", "DW"), 3).ToString());
                        }
                    }
                    else
                    {
                        objListItem.SubItems.Add(Math.Round(avgEst.sectWeibull_k[WD_Ind], 3).ToString()); // weibull k
                        objListItem.SubItems.Add(Math.Round(avgEst.sectWeibull_A[WD_Ind], 3).ToString()); // weibull C

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
                    Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(isCalibrated, null);

                    if (WD_Ind < numWD)
                    {
                        // Sectorwise statistics
                        turbineWS[i] = avgEst.sectorWS[WD_Ind];
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
                        turbineWS[i] = avgEst.WS;
                        Turb_UW_Expos[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(checkedTurbines[i].windRose, "Expo", "UW");
                        Turb_DW_Expos[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(checkedTurbines[i].windRose, "Expo", "DW");
                        Turb_DW_min_UW_Expos[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(checkedTurbines[i].windRose, "Expo", "DW") - checkedTurbines[i].expo[radiusInd].GetOverallValue(checkedTurbines[i].windRose, "Expo", "UW");
                        Turb_Elevs[i] = checkedTurbines[i].elev;

                        if (thisInst.topo.gotSR == true)
                        {
                            Turb_UW_SRs[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(checkedTurbines[i].windRose, "SR", "UW");
                            Turb_DW_SRs[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(checkedTurbines[i].windRose, "SR", "DW");
                            Turb_UW_DHs[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(checkedTurbines[i].windRose, "DH", "UW");
                            Turb_DW_DHs[i] = checkedTurbines[i].expo[radiusInd].GetOverallValue(checkedTurbines[i].windRose, "DH", "DW");
                        }
                    }
                }

                if (turbCount > 0) {
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


                    if (thisInst.topo.gotSR == true) {
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

            bool isCalibrated = thisInst.GetSelectedModel("Gross");   
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
            double metAEP = 0;
            double metCF = 0;

            try {
                if (thisInst.cboPowerCrvs.SelectedItem.ToString() != "No power Curves Imported")
                    AEP_calc = true;
                else
                    AEP_calc = false;

                powerCurve = thisInst.cboPowerCrvs.SelectedItem.ToString();
            }
            catch  {
                if (thisInst.cboPowerCrvs.Items.Count > 0)
                {
                    thisInst.cboPowerCrvs.SelectedIndex = 0;
                    if (thisInst.cboPowerCrvs.SelectedItem.ToString() != "No power Curves Imported")
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
                metAEP = 0;
                weibull = thisInst.metList.CalcWeibullParams(checkedMets[i].WS_Dist, checkedMets[i].sectorWS_Dist, checkedMets[i].WS);

                if (AEP_calc == true)
                {
                    if (WD_Ind == numWD)
                        metAEP = thisInst.turbineList.CalcAndReturnGrossAEP(checkedMets[i].WS_Dist, powerCurve);
                    else
                    {
                        int numWS = checkedMets[i].WS_Dist.Length;
                        double[] sectDist = new double[numWS];
                        for (int k = 0; k <= numWS - 1; k++)
                            sectDist[k] = checkedMets[i].sectorWS_Dist[WD_Ind, k];

                        metAEP = thisInst.turbineList.CalcAndReturnGrossAEP(sectDist, powerCurve);
                        metAEP = metAEP / 1000;
                        metAEP = metAEP * checkedMets[i].windRose[WD_Ind];
                    }
                    metCF = thisInst.turbineList.CalcCapacityFactor(metAEP, thisPowerCurve.ratedPower);
                    if (WD_Ind != numWD) metCF = metCF * numWD;
                }

                objListItem = thisInst.lstGrossTurbEst.Items.Add(checkedMets[i].name);
                objListItem.SubItems.Add(Math.Round(checkedMets[i].elev, 1).ToString());
                if (WD_Ind == numWD)
                    objListItem.SubItems.Add(Math.Round(checkedMets[i].WS, 3).ToString());
                else
                    objListItem.SubItems.Add(Math.Round(checkedMets[i].sectorWS_Ratio[WD_Ind] * checkedMets[i].WS, 3).ToString());

                if (metAEP != 0)
                    objListItem.SubItems.Add(Math.Round(metAEP, 0).ToString());
                else
                    objListItem.SubItems.Add("");

                if (metCF != 0)
                    objListItem.SubItems.Add(Math.Round(metCF,3).ToString("P"));
                else
                    objListItem.SubItems.Add("");

                if (WD_Ind == numWD) {
                    objListItem.SubItems.Add(Math.Round(weibull.overall_k, 2).ToString()); // weibull k
                    objListItem.SubItems.Add(Math.Round(weibull.overall_A, 2).ToString()); // weibull C
                }
                else {
                    objListItem.SubItems.Add(Math.Round(weibull.sector_k[WD_Ind], 2).ToString()); // weibull k
                    objListItem.SubItems.Add(Math.Round(weibull.sector_A[WD_Ind], 2).ToString()); // weibull C
                }
            }

            if (thisInst.turbineList.turbineCalcsDone == true && thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false)
            {
                double turbAEP = 0;
                double turbCF = 0;

                for (int i = 0; i < checkedTurbCount; i++)
                {
                    Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(isCalibrated, null);

                    if (avgEst.WS != 0)
                    {
                        objListItem = thisInst.lstGrossTurbEst.Items.Add(checkedTurbines[i].name);
                        objListItem.SubItems.Add(Math.Round(checkedTurbines[i].elev, 1).ToString());
                        if (WD_Ind == numWD)
                            objListItem.SubItems.Add(Math.Round(avgEst.WS, 3).ToString());
                        else
                            objListItem.SubItems.Add(Math.Round(avgEst.sectorWS[WD_Ind], 3).ToString());

                        if (AEP_calc == true && WD_Ind == numWD) {
                            turbAEP = checkedTurbines[i].GetGrossAEP(powerCurve, isCalibrated, WD_Ind);
                            objListItem.SubItems.Add(Math.Round(turbAEP, 0).ToString());
                        }
                        else if (AEP_calc == true && WD_Ind < numWD) {
                            int numWS = avgEst.WS_Dist.Length;
                            double[] sectDist = new double[numWS];

                            for (int k = 0; k < numWS; k++)
                                sectDist[k] = avgEst.sectorWS_Dist[WD_Ind, k];

                            turbAEP = thisInst.turbineList.CalcAndReturnGrossAEP(sectDist, powerCurve);
                            turbAEP = turbAEP / 1000;
                            turbAEP = turbAEP * checkedTurbines[i].windRose[WD_Ind];
                            objListItem.SubItems.Add(Math.Round(turbAEP, 0).ToString());
                        }
                        else
                            objListItem.SubItems.Add("");

                        if (turbAEP != 0) {
                            turbCF = thisInst.turbineList.CalcCapacityFactor(turbAEP, thisPowerCurve.ratedPower);
                            if (WD_Ind != numWD) turbCF = turbCF * numWD;
                            objListItem.SubItems.Add(Math.Round(turbCF, 4).ToString("P"));
                        }
                        else
                            objListItem.SubItems.Add("");

                        if (WD_Ind == numWD)
                        {
                            objListItem.SubItems.Add(Math.Round(avgEst.weibull_k, 2).ToString());
                            objListItem.SubItems.Add(Math.Round(avgEst.weibull_A, 2).ToString());
                        }
                        else
                        {
                            objListItem.SubItems.Add(Math.Round(avgEst.sectWeibull_k[WD_Ind], 2).ToString());
                            objListItem.SubItems.Add(Math.Round(avgEst.sectWeibull_A[WD_Ind], 2).ToString());
                        }

                    }

                }
            }
            

        }

        public void TurbUncertEstList(Continuum thisInst) // Updates the table of turbine uncertainty estimates on Uncertainty tab
        {
            thisInst.lstTurbUncert.Items.Clear();

            string powerCurve = "";
            bool isCalibrated = thisInst.GetSelectedModel("Uncertainty");           

            int turbCount = thisInst.turbineList.TurbineCount;

            if (thisInst.turbineList.turbineCalcsDone == true && thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false)
            {
                try {
                    powerCurve = thisInst.cboUncertPowerCrv.SelectedItem.ToString();
                }
                catch  {
                    thisInst.cboUncertPowerCrv.SelectedIndex = 0;
                    powerCurve = thisInst.cboUncertPowerCrv.SelectedItem.ToString();
                }

                for (int i = 0; i < turbCount; i++) // Now repopulate it with turbines and modeled parameters
                {
                    Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                    Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(isCalibrated, null);

                    objListItem = thisInst.lstTurbUncert.Items.Add((i + 1).ToString());
                    objListItem.SubItems.Add(thisTurb.name);

                    if (thisTurb.avgWS_Est != null) {
                        objListItem.SubItems.Add(Math.Round(avgEst.WS, 3).ToString());
                        objListItem.SubItems.Add(Math.Round(avgEst.uncert,5).ToString("P"));
                        objListItem.SubItems.Add(Math.Round(avgEst.WS - avgEst.WS * avgEst.uncert * 1.28155, 3).ToString());
                        objListItem.SubItems.Add(Math.Round(avgEst.WS - avgEst.WS * avgEst.uncert * 2.326, 3).ToString());
                    }
                    if (thisTurb.grossAEP != null && powerCurve != "No power Curves Imported")
                    {
                        Turbine.Gross_Energy_Est grossEst = thisTurb.GetGrossEnergyEst(isCalibrated);

                        objListItem.SubItems.Add(Math.Round(grossEst.AEP, 0).ToString());
                        objListItem.SubItems.Add(Math.Round(grossEst.P90, 0).ToString());
                        objListItem.SubItems.Add(Math.Round(grossEst.P99, 0).ToString());
                    }
                }
            }
        }

        public void PowerCurveList(Continuum thisInst) // Updates the list of turbines on Gross Est tab and dropdown lists on Uncertainty tab and Wake Model generator
        {
            thisInst.cboPowerCrvs.Items.Clear();
            thisInst.cboPowerCrvs.Text = "";
            thisInst.cboUncertPowerCrv.Items.Clear();
            thisInst.chkPowerCurveList.Items.Clear();
            
            if (thisInst.turbineList.PowerCurveCount > 0) {
                for (int i = 0; i < thisInst.turbineList.PowerCurveCount; i++)
                {
                    thisInst.cboPowerCrvs.Items.Add(thisInst.turbineList.powerCurves[i].name);
                    thisInst.cboUncertPowerCrv.Items.Add(thisInst.turbineList.powerCurves[i].name);
                    thisInst.chkPowerCurveList.Items.Add(thisInst.turbineList.powerCurves[i].name, true);                    
                }
            }
            else {
                thisInst.cboPowerCrvs.Items.Add("No power Curves Imported");
                thisInst.cboUncertPowerCrv.Items.Add("No power Curves Imported");
            }

            thisInst.okToUpdate = false;
            if (thisInst.cboPowerCrvs.Items.Count > 0) thisInst.cboPowerCrvs.SelectedIndex = 0;
            if (thisInst.cboUncertPowerCrv.Items.Count > 0) thisInst.cboUncertPowerCrv.SelectedIndex = 0;
            thisInst.okToUpdate = true;
            
        }

        public void TurbStats(Continuum thisInst) // Reads turbines that are checked on gross tab and then calls function to calculate stats and fill textboxes on gross tab
        {
            if (thisInst.turbineList.turbineCalcsDone == false) {
                ClearStats(thisInst);
                return;
            }

            bool isCalibrated = thisInst.GetSelectedModel("Gross");

            int WD_Ind = thisInst.GetWD_ind("Gross");
            if (thisInst.metList.ThisCount == 0) return;
            int numWD = thisInst.GetNumWD();

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Gross");
            if (checkedTurbines == null) {
                ClearStats(thisInst);
                return;
            }
            thisInst.turbineList.FindParamStats(thisInst, checkedTurbines, isCalibrated, WD_Ind, numWD);
        }

        public double FindMin(double[] theseParams) // Returns minimum of theseParams()
        {
            double min = 1000;
            int numParams;

            try {
                numParams = theseParams.Length;
            }
            catch  {
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

            try {
                numParams = theseParams.Length;
            }
            catch {
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

            try {
                selectParam = thisInst.cboGrossParam.SelectedItem.ToString();
            }
            catch {
                return;
            }

            Met[] checkedMets = thisInst.GetCheckedMets("Gross");
            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Gross");

            int checkedMetCount = checkedMets.Length;
            int checkedTurbCount = checkedTurbines.Length;              

            NChartControl histoCtrl = thisInst.chtHistogram_Nev;
            histoCtrl.Charts[0].Series.Clear();
            histoCtrl.Labels.Clear();

            histoCtrl.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            histoCtrl.Controller.Tools.Add(tooltip);

            if (metList.ThisCount == 0 || (checkedMetCount == 0 && checkedTurbCount == 0)) {
                histoCtrl.Refresh();
                return;
            }

            if (thisInst.metList.ThisCount == 0) return;
            int WD_Ind = thisInst.GetWD_ind("Gross");
            int numWD = thisInst.GetNumWD();

            bool isCalibrated = thisInst.GetSelectedModel("Gross");

            MetCollection.Weibull_params weibull = new MetCollection.Weibull_params();
            TurbineCollection turbineList = thisInst.turbineList;
            double metAEP = 0;
            bool AEP_calc = false;
            string powerCurve = "";

            double hubHeight = thisInst.metList.metItem[0].height;
            double[] sectDist;
            int numWS;

            try {
                if (thisInst.cboPowerCrvs.SelectedItem.ToString() != "No power Curves Imported")
                    AEP_calc = true;
                else
                    AEP_calc = false;

                powerCurve = thisInst.cboPowerCrvs.SelectedItem.ToString();
            }
            catch  {
                if (thisInst.cboPowerCrvs.Items.Count > 0) {
                    thisInst.cboPowerCrvs.SelectedIndex = 0;
                    if (thisInst.cboPowerCrvs.SelectedItem.ToString() != "No power Curves Imported")
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
                if (selectParam == "Wind Speed") {
                    if (WD_Ind == numWD)
                        metVals[i] = checkedMets[i].WS;
                    else
                        metVals[i] = checkedMets[i].WS * checkedMets[i].sectorWS_Ratio[WD_Ind];
                }
                else if (selectParam == "Gross AEP")
                {
                    if (WD_Ind == numWD) {
                        metAEP = turbineList.CalcAndReturnGrossAEP(checkedMets[i].WS_Dist, powerCurve);
                        metVals[i] = metAEP;
                    }
                    else {
                        numWS = checkedMets[i].WS_Dist.Length;
                        sectDist = new double[numWS];
                        for (int k = 0; k <= numWS - 1; k++)
                            sectDist[k] = checkedMets[i].sectorWS_Dist[WD_Ind, k];

                        metAEP = turbineList.CalcAndReturnGrossAEP(sectDist, powerCurve);
                        metAEP = metAEP * checkedMets[i].windRose[WD_Ind] / 1000;
                        metVals[i] = metAEP;
                    }
                }
                else if (selectParam == "Elevation")
                    metVals[i] = checkedMets[i].elev;
                else if (selectParam == "weibull k")
                {
                    weibull = metList.CalcWeibullParams(checkedMets[i].WS_Dist, checkedMets[i].sectorWS_Dist, checkedMets[i].WS);
                    if (WD_Ind == numWD)
                        metVals[i] = weibull.overall_k;
                    else
                        metVals[i] = weibull.sector_k[WD_Ind];
                }
                else if (selectParam == "weibull A")
                {
                    weibull = metList.CalcWeibullParams(checkedMets[i].WS_Dist, checkedMets[i].sectorWS_Dist, checkedMets[i].WS);
                    if (WD_Ind == numWD)
                        metVals[i] = weibull.overall_A;
                    else
                        metVals[i] = weibull.sector_A[WD_Ind];
                }
            }

            double[] turbVals = new double[checkedTurbCount];
            
            for (int i = 0; i <= checkedTurbCount - 1; i++)
            {
                Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(isCalibrated, null);

                // Get values at turbine sites
                if (avgEst.WS != 0)
                {
                    if (selectParam == "Wind Speed") {
                        if (WD_Ind == numWD)
                            turbVals[i] = avgEst.WS;
                        else
                            turbVals[i] = avgEst.sectorWS[WD_Ind];
                    }
                    else if (selectParam == "Gross AEP")
                        turbVals[i] = checkedTurbines[i].GetGrossAEP(powerCurve, isCalibrated, WD_Ind);
                    else if (selectParam == "Elevation")
                        turbVals[i] = checkedTurbines[i].elev;
                    else if (selectParam == "weibull k")
                    {
                        if (WD_Ind == numWD)
                            turbVals[i] = avgEst.weibull_k;
                        else
                            turbVals[i] = avgEst.sectWeibull_k[WD_Ind];
                    }
                    else if (selectParam == "weibull A")
                    {
                        if (WD_Ind == numWD)
                            turbVals[i] = avgEst.weibull_A;
                        else
                            turbVals[i] = avgEst.sectWeibull_A[WD_Ind];
                    }
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
                if (turbVals[i] < histoMin)
                    histoMin = turbVals[i];

                if (turbVals[i] > histoMax)
                    histoMax = turbVals[i];

                Val_avg = Val_avg + turbVals[i];
            }

            if (checkedTurbCount + checkedMetCount > 0)
                Val_avg = Val_avg / (checkedTurbCount + checkedMetCount);

            histoMin = histoMin - histoMin * 0.02f;
            histoMax = histoMax + histoMax * 0.02f;

            if (selectParam == "Wind Speed" || selectParam == "weibull k" || selectParam == "weibull A")
            {
                histoMin = (double)Math.Round(histoMin, 1);
                histoMax = (double)Math.Round(histoMax, 1);
                Val_avg = (double)Math.Round(Val_avg, 1);
                histoInt = 0.05f;
                histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                while (histoSize > 20) {
                    histoInt = histoInt + 0.05f;
                    histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                }
            }
            else if (selectParam == "Elevation") {
                histoMin = (double)Math.Round(histoMin, 0);
                histoMax = (double)Math.Round(histoMax, 0);
                Val_avg = (double)Math.Round(Val_avg, 0);
                histoInt = 1;
                histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                while (histoSize > 20) {
                    histoInt = histoInt + 1;
                    histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                }
            }
            else if (selectParam == "Gross AEP") {
                histoMin = histoMin / 100;
                histoMin = (double)Math.Round(histoMin, 0);
                histoMin = histoMin * 100;

                histoMax = histoMax / 100;
                histoMax = (double)Math.Round(histoMax, 0);
                histoMax = histoMax * 100;

                Val_avg = Val_avg / 100;
                Val_avg = (double)Math.Round(Val_avg, 0);
                Val_avg = Val_avg * 100;

                histoInt = 100;
                histoSize = Convert.ToInt16(1 + (histoMax - histoMin) / histoInt);
                while (histoSize > 20) {
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
                Turb_Histo.XValues.Add(histoVals[i]);
                Turb_Histo.Values.Add(turbHistoVals[i]);
            }

            NLinearScaleConfigurator linearScaleX = new NLinearScaleConfigurator();
            linearScaleX.MinorTickCount = 0;

            if (selectParam == "Wind Speed") {
                linearScaleX.MajorTickMode = MajorTickMode.CustomStep;
                linearScaleX.CustomStep = 0.1f;
            }
            else {
                linearScaleX.MajorTickMode = MajorTickMode.AutoMaxCount;
                linearScaleX.MaxTickCount = 10;
            }

            histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator = linearScaleX;

            NLinearScaleConfigurator linearScaleY = (NLinearScaleConfigurator)histoCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator;
            linearScaleY.MinorTickCount = 0;
            linearScaleY.MajorTickMode = MajorTickMode.CustomStep;
            linearScaleY.CustomStep = 1;

            if (selectParam == "Wind Speed") {
                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "Wind Speed, m/s";
                histoCtrl.Labels.AddHeader("Histogram of Met and Turbine " + hubHeight + " m Wind Speeds");
            }
            else if (selectParam == "Gross AEP") {
                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "Gross AEP, MWh";
                histoCtrl.Labels.AddHeader("Histogram of Met and Turbine Gross AEP");
            }
            else if (selectParam == "Elevation") {
                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "Elevation, m";
                histoCtrl.Labels.AddHeader("Histogram of Met and Turbine Elevations");
            }
            else if (selectParam == "weibull k") {
                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "weibull k";
                histoCtrl.Labels.AddHeader("Histogram of Met and Turbine " + hubHeight + " m weibull k");
            }
            else if (selectParam == "weibull A") {
                histoCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = "weibull A";
                histoCtrl.Labels.AddHeader("Histogram of Met and Turbine " + hubHeight + " m weibull A");
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
            histoCtrl.Labels.AddHeader("Histogram of Round Robin Estimates");

            int RR_ind;
            string dropText;
            int numMetsUsed;

            if (thisInst.metPairList.RoundRobinCount > 0)
            {
                try {
                    RR_ind = thisInst.cboRoundRobin.SelectedIndex;
                    dropText = thisInst.cboRoundRobin.SelectedItem.ToString();
                }
                catch {
                    return;
                }

                string[] textSplit = dropText.Split(Convert.ToChar(" "));

                try {
                    numMetsUsed = Convert.ToInt16(textSplit[1]);
                }
                catch  {
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

                    if ((sameMets == true && thisRR.metSubSize == numMetsUsed)) {
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

                while (histoSize > 20) {
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
            int turbCount = thisInst.turbineList.TurbineCount;
            thisInst.lstTurbines.Items.Clear(); // First clear table
            thisInst.chkTurbLabels.Items.Clear(); // Clear table of turbine labels
            thisInst.chkTurbLabelStep.Items.Clear(); // Clear table of turbine labels
            thisInst.chkTurbLabels_Maps.Items.Clear(); // Clear table of turbine labels
            thisInst.chkTurbSumm.Items.Clear(); // Clear table of turbines in met & turbine est tab
            thisInst.chkTurbGross.Items.Clear(); // Clear table of turbines in turbine est tab
            thisInst.chkTurbNet.Items.Clear(); // net est ab
            thisInst.chkStrings.Items.Clear(); // list of turbine strings
                         
            string[] stringNames = null;
            int numStrings = 0;
            bool haveString = false;

            for (int i = 0; i < turbCount; i++)
            {
                Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                objListItem = thisInst.lstTurbines.Items.Add(thisTurb.name);
                objListItem.SubItems.Add(Math.Round(thisTurb.UTMX,0).ToString());
                objListItem.SubItems.Add(Math.Round(thisTurb.UTMY, 0).ToString());

                thisInst.chkTurbLabels.Items.Add(thisTurb.name, true);
                thisInst.chkTurbLabels_Maps.Items.Add(thisTurb.name, true);
                thisInst.chkTurbLabelStep.Items.Add(thisTurb.name, true);
                thisInst.chkTurbGross.Items.Add(thisTurb.name, true);
                thisInst.chkTurbNet.Items.Add(thisTurb.name, true);
                thisInst.chkTurbSumm.Items.Add(thisTurb.name, true);

                if (numStrings > 0)
                {
                    haveString = false;
                    for (int j = 0; j < numStrings; j++)
                    {
                        if (thisTurb.stringNum == Convert.ToInt16(stringNames[j])) {
                            haveString = true;
                            break;
                        }
                    }

                    if (haveString == false) {
                        Array.Resize(ref stringNames, numStrings + 1);
                        stringNames[numStrings] = thisTurb.stringNum.ToString();
                        numStrings++;
                    }
                }
                else {
                    stringNames = new string[numStrings + 1];
                    stringNames[numStrings] = thisTurb.stringNum.ToString();
                    numStrings++;
                }
            }

            for (int i = 0; i < numStrings; i++)
                thisInst.chkStrings.Items.Add(stringNames[i], true);            

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

            if (plotName == "UW Expo")
                expoSR_ChtCtrl = thisInst.chtUWExpo_Nev;
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
            int hubHeight = (int)thisInst.metList.metItem[0].height;
            
            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)(expoSR_ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator);
            scaleConfiguratorX.Title.Text = GetExpoSR_AxisTitle(plotName);

            NStandardScaleConfigurator scaleConfiguratorY = (NStandardScaleConfigurator)(expoSR_ChtCtrl.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator);
            scaleConfiguratorY.Title.Text = "Wind Speed, m/s";

            NLabel chartTitle = expoSR_ChtCtrl.Labels.AddHeader(GetExpoSR_ChartTitle(plotName, hubHeight));

            Turbine[] checkedTurbines = thisInst.GetCheckedTurbs("Summary");
            Met[] checkedMets = thisInst.GetCheckedMets("Summary");

            bool plotTurbs = false;
            if (thisInst.turbineList.turbineCalcsDone == true && thisInst.BW_worker.BackgroundWorker_TurbCalcs.IsBusy == false)
                plotTurbs = true;

            if (checkedMets == null && checkedTurbines == null)
                return;

            if (checkedMets != null)
            {
                for (int i = 0; i < checkedMets.Length; i++)
                {
                    NPointSeries metDataSeries = new NPointSeries();
                    metDataSeries.DataLabelStyle.Visible = false;
                    metDataSeries.UseXValues = true;
                    metDataSeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
                    expoSR_ChtCtrl.Charts[0].Series.Add(metDataSeries);

                    if (checkedMets[i].expo != null)
                    {
                        metDataSeries.XValues.Add(GetXValForExpoSR_Plot(plotName, WD_Ind, checkedMets[i].expo[radiusIndex], checkedMets[i].elev, checkedMets[i].windRose));

                        if (WD_Ind == numWD)
                            metDataSeries.Values.Add(checkedMets[i].WS);
                        else
                            metDataSeries.Values.Add(checkedMets[i].WS * checkedMets[i].sectorWS_Ratio[WD_Ind]);
                    }

                    metDataSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(checkedMets[i].name);
                }
            }

            if (plotTurbs == true && checkedTurbines != null) {

                for (int i = 0; i < checkedTurbines.Length; i++)
                {
                    NPointSeries turbDataSeries = new NPointSeries();
                    turbDataSeries.DataLabelStyle.Visible = false;
                    turbDataSeries.UseXValues = true;
                    turbDataSeries.FillStyle = new NColorFillStyle(Color.Red);
                    turbDataSeries.Size = new NLength(2, NRelativeUnit.ParentPercentage);
                    expoSR_ChtCtrl.Charts[0].Series.Add(turbDataSeries);

                    if (checkedTurbines[i].expo != null)
                    {
                        turbDataSeries.XValues.Add(GetXValForExpoSR_Plot(plotName, WD_Ind, checkedTurbines[i].expo[radiusIndex], checkedTurbines[i].elev, checkedTurbines[i].windRose));
                        turbDataSeries.Values.Add(checkedTurbines[i].GetAvgOrSectorWS_Est(true, null, WD_Ind, "WS"));
                        
                    }
                    turbDataSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(checkedTurbines[i].name);
                }
            }

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
                chartTitle = thisHeight + " m WS vs Upwind roughness";
            else if (plotName == "DW SR")
                chartTitle = thisHeight + " m WS vs Downwind roughness";
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
                axisTitle = "Upwind Surface roughness, m";
            else if (plotName == "DW SR")
                axisTitle = "Downwind Surface roughness, m";
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

            try {
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
            catch {
                XVal = -999;
            }

            return XVal;

        }


        public double[] CalcDistAlongNodes(TopoInfo.TopoGrid site1, TopoInfo.TopoGrid site2, NodeCollection.Node_UTMs[] pathOfNodes)
        {
            // Returns array of cumulative distances along path of nodes from site1 to site2                       
            int numNodes = 0;

            try {
                numNodes = pathOfNodes.Length;
            }
            catch  {
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
            nodePthChtCtrl.Controller.Tools.Clear();

            if (thisInst.modelList.ModelCount == 0) return;
            NTooltipTool toolTip = new NTooltipTool();
            nodePthChtCtrl.Controller.Tools.Add(toolTip);

            if (thisInst.lstPathNodes.Items.Count == 0)
            {
                nodePthChtCtrl.Refresh();
                return;
            }

            int radiusInd = thisInst.GetRadiusInd("Advanced");
            int radius = thisInst.radiiList.investItem[radiusInd].radius;
            if (thisInst.metList.ThisCount == 0) return;

            int numWD = thisInst.GetNumWD();
            int WD_Ind = thisInst.GetWD_ind("Advanced");
            if (WD_Ind == -1) return;

            bool isCalibrated = thisInst.GetSelectedModel("Advanced");
            Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), radius, radius, isCalibrated);
            Model thisModel = theseModels[0];
           
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

            NodeCollection.Node_UTMs[] pathOfNodes = null;            
            int Y_ind = 0;

            if (site1.UTMX != 0 && site2.UTMX != 0)
            { // met pair
                for (int i = 0; i < thisInst.metPairList.PairCount; i++)
                {
                    Pair_Of_Mets.WS_CrossPreds thisCrossPred = thisInst.metPairList.metPairs[i].GetWS_CrossPred(theseModels[radiusInd]);
                    if (thisInst.metPairList.metPairs[i].met1.name == startMetStr && thisInst.metPairList.metPairs[i].met2.name == endMetStr)
                    {                        
                        Nodes[] Pair_path_nodes = thisCrossPred.nodePath;

                        if (Pair_path_nodes != null)
                        {
                            pathOfNodes = new NodeCollection.Node_UTMs[Pair_path_nodes.Length];

                            for (int j = 0; j < Pair_path_nodes.Length; j++)
                            {
                                pathOfNodes[j].UTMX = Pair_path_nodes[j].UTMX;
                                pathOfNodes[j].UTMY = Pair_path_nodes[j].UTMY;
                            }
                            break;
                        }
                    }
                    else if ((thisInst.metPairList.metPairs[i].met2.name == startMetStr && thisInst.metPairList.metPairs[i].met1.name == endMetStr))
                    {
                        Nodes[] Pair_path_nodes = thisCrossPred.nodePath;

                        if (Pair_path_nodes != null)
                        {
                            pathOfNodes = new NodeCollection.Node_UTMs[Pair_path_nodes.Length];
                            Y_ind = 0;

                            for (int j = Pair_path_nodes.Length - 1; j >= 0; j--)
                            {
                                pathOfNodes[Y_ind].UTMX = Pair_path_nodes[j].UTMX;
                                pathOfNodes[Y_ind].UTMY = Pair_path_nodes[j].UTMY;
                                Y_ind = Y_ind++;
                            }

                            break;
                        }
                    }
                }
            }
            else {
                for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
                {
                    if (thisInst.turbineList.turbineEsts[i].name == endMetStr)
                    {
                        site2.UTMX = thisInst.turbineList.turbineEsts[i].UTMX;
                        site2.UTMY = thisInst.turbineList.turbineEsts[i].UTMY;

                        for (int j = 0; j < thisInst.turbineList.turbineEsts[i].WSEst_Count; j++)
                            if (thisInst.turbineList.turbineEsts[i].WS_Estimate[j].predictorMetName == startMetStr && thisInst.turbineList.turbineEsts[i].WS_Estimate[j].radius == thisInst.radiiList.investItem[radiusInd].radius &&
                            thisInst.modelList.IsSameModel(thisModel, thisInst.turbineList.turbineEsts[i].WS_Estimate[j].model))
                                pathOfNodes = thisInst.turbineList.turbineEsts[i].WS_Estimate[j].pathOfNodesUTMs;

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
                if (thisInst.lstPathNodes.Columns[i].Text == "UW roughness") paramToShow.showUWRough = true;
                if (thisInst.lstPathNodes.Columns[i].Text == "DW roughness") paramToShow.showDWRough = true;
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
                        else {
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
            else {
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
                        else {

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
                    Y_label_2 = "roughness (m)";


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
            else {

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
            else {
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
            if (thisSeries.Name == "UW roughness") thisColor = Color.Maroon;
            if (thisSeries.Name == "DW roughness") thisColor = Color.Brown;
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
            thisInst.chtEst_Roses_Nev.Charts[0].Series.Clear();
            thisInst.chtEst_Roses_Nev.Refresh();

            try {
                WS_or_WR = thisInst.cboWS_or_WD.SelectedItem.ToString();
            }
            catch { 
                thisInst.cboWS_or_WD.SelectedIndex = 0;
                return;
            }

            if (WS_or_WR == "WS")
            {
                thisInst.chtWSDist_Nev.Visible = true;
                thisInst.chtEst_Roses_Nev.Visible = false;
                WSDist_Plot(thisInst);
            }
            else {
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

            AddAxisToRadarPlot(ref WR_Chart, numWD);

            for (int j = 0; j < checkedMetCount; j++)
                AddSeriesToRadarPlot(ref WR_Chart, checkedMets[j].windRose, checkedMets[j].name, GetMetOrTurbColor(j), true, false);

            for (int j = 0; j < checkedTurbCount; j++)
                AddSeriesToRadarPlot(ref WR_Chart, checkedTurbines[j].windRose, checkedTurbines[j].name, GetMetOrTurbColor(checkedMetCount + j), true, true);

            chtWindRose.Refresh();
        }


        public void WSDist_Plot(Continuum thisInst) // Updates wind speed distribution plot on Gross Turbine Ests tab 
        {

            NChartControl WS_DistCtl = thisInst.chtWSDist_Nev;
            WS_DistCtl.Charts[0].Series.Clear();
            WS_DistCtl.Labels.Clear();
            WS_DistCtl.Controller.Tools.Clear();

            bool isCalibrated = thisInst.GetSelectedModel("Gross");
            if (thisInst.metList.ThisCount == 0) return;
            int numWD = thisInst.GetNumWD();
            
            if (numWD <= 1) {
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
                double WS_FirstInt = thisMet.WS_FirstInt;
                double WS_IntSize = thisMet.WS_IntSize;
                int numWS = thisMet.WS_Dist.Length;

                for (int i = 0; i <= checkedMetCount - 1; i++)
                {
                    NLineSeries WS_DistSeries = new NLineSeries();
                    WS_DistSeries.DataLabelStyle.Visible = false;
                    WS_DistSeries.Name = checkedMets[i].name;
                    WS_DistCtl.Charts[0].Series.Add(WS_DistSeries);
                    WS_DistSeries.BorderStyle.Color = GetMetOrTurbColor(i);
                    WS_DistSeries.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);
                    WS_DistSeries.InteractivityStyle.Tooltip = new NTooltipAttribute(checkedMets[i].name);

                    for (int WS_ind = 0; WS_ind < checkedMets[i].WS_Dist.Length; WS_ind++)
                    {
                        WS_DistSeries.XValues.Add(WS_FirstInt + WS_ind * WS_IntSize - WS_IntSize / 2);

                        if (WD_Ind == numWD)
                            WS_DistSeries.Values.Add(checkedMets[i].WS_Dist[WS_ind]);
                        else
                            WS_DistSeries.Values.Add(checkedMets[i].sectorWS_Dist[WD_Ind, WS_ind]);
                    }
                }

                if (thisInst.turbineList.turbineCalcsDone == true)
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

                        Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(isCalibrated, null);

                        for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
                        {
                            WS_DistSeries.XValues.Add(WS_FirstInt + WS_ind * WS_IntSize - WS_IntSize / 2);

                            if (WD_Ind == numWD)
                                WS_DistSeries.Values.Add(avgEst.WS_Dist[WS_ind]);
                            else
                                WS_DistSeries.Values.Add(avgEst.sectorWS_Dist[WD_Ind, WS_ind]);
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
            wakedDist.Labels.Clear();
            wakedDist.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            wakedDist.Controller.Tools.Add(tooltip);                     

            double maxFreq = 0;
            bool isCalibrated = thisInst.GetSelectedModel("Net");
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
            if (WD_Ind == -1) {
                wakedDist.Refresh();
                return;
            }    

            Met thisMet = new Met();

            if (thisInst.metList.ThisCount == 0)
                return;
            else
                thisMet = thisInst.metList.metItem[0];

            double WS_FirstInt = thisMet.WS_FirstInt;
            double WS_IntSize = thisMet.WS_IntSize;

            int numWS = thisMet.WS_Dist.Length;

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
                Turbine.Avg_Est avgEst = checkedTurbines[i].GetAvgWS_Est(isCalibrated, thisWakeModel);

                if (avgEst.WS != 0)
                {
                    for (int j = 0; j < numWS; j++)
                    {
                        Turb_X_WS[j] = WS_FirstInt + j * WS_IntSize - WS_IntSize / 2;

                        if (WD_Ind == numWD)
                        {
                            Turb_Y_Freq[j] = avgEst.WS_Dist[j];
                            if (avgEst.WS_Dist[j] > maxFreq)
                                maxFreq = avgEst.WS_Dist[j];
                        }
                        else {

                            Turb_Y_Freq[j] = avgEst.sectorWS_Dist[WD_Ind, j];

                            if (avgEst.sectorWS_Dist[WD_Ind, j] > maxFreq)
                                maxFreq = avgEst.sectorWS_Dist[WD_Ind, j];
                        }
                    }

                    NLineSeries thisSeries = new NLineSeries();
                    thisSeries.DataLabelStyle.Visible = false;
                    thisSeries.BorderStyle.Color = GetMetOrTurbColor((int)(100*(float)plotInd / checkedTurbines.Length));
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

            double height = thisInst.metList.metItem[0].height;

            wakedDist.Labels.AddHeader(height + " m Waked WS Distribution");
            wakedDist.Charts[0].Axis(StandardAxis.PrimaryX).ScaleConfigurator.Title.Text = Xlabel1;
            wakedDist.Charts[0].Axis(StandardAxis.PrimaryY).ScaleConfigurator.Title.Text = yLabel1;

            wakedDist.Refresh();            
        }

        public void PowerCrvPlot(Continuum thisInst) // Updates plot of power and thrust curves on Gross Turb Ests tab
        {

            int powerCurveCount = thisInst.chkPowerCurveList.CheckedItems.Count;            
            int numMets = thisInst.metList.ThisCount;
            
            TurbineCollection.PowerCurve thisPowerCurve = new TurbineCollection.PowerCurve();

            NChartControl crvCtrl = thisInst.chtPowerCrv_Nev;
            crvCtrl.Charts[0].Series.Clear();
            crvCtrl.Labels.Clear();
            crvCtrl.Labels.AddHeader("Power and Thrust Curves");

            crvCtrl.Controller.Tools.Clear();
            NTooltipTool tooltip = new NTooltipTool();
            crvCtrl.Controller.Tools.Add(tooltip);

            NLineSeries powerCurve = new NLineSeries();
            powerCurve.DataLabelStyle.Visible = false;
            powerCurve.BorderStyle.Width = new NLength(0.5f, NRelativeUnit.ParentPercentage);

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

            if (powerCurveCount > 0 && thisInst.metList.ThisCount > 0)
            {
                Met thisMet = thisInst.metList.metItem[0];
                int numWS = thisMet.WS_Dist.Length;

                double[,] metX_WS = new double[powerCurveCount, numWS];
                double[,] metY_Power = new double[powerCurveCount, numWS];
                double[,] metY_Thrust = new double[powerCurveCount, numWS];

                for (int i = 0; i < powerCurveCount; i++)
                {
                    thisMet = thisInst.metList.metItem[0];
                    for (int j = 0; j < thisInst.turbineList.PowerCurveCount; j++)
                    {
                        if (thisInst.chkPowerCurveList.CheckedItems[i].ToString() == thisInst.turbineList.powerCurves[j].name) {
                            thisPowerCurve = thisInst.turbineList.powerCurves[j];
                            break;
                        }
                    }

                    if (thisPowerCurve.power != null)
                    {
                        for (int j = 0; j < numWS; j++)
                        {
                            powerCurve.XValues.Add(thisMet.WS_FirstInt + j * thisMet.WS_IntSize - thisMet.WS_IntSize / 2);
                            powerCurve.Values.Add(thisPowerCurve.power[j]);
                            thrustCurve.XValues.Add(thisMet.WS_FirstInt + j * thisMet.WS_IntSize - thisMet.WS_IntSize / 2);
                            thrustCurve.Values.Add(thisPowerCurve.thrustCoeff[j]);
                        }

                        powerCurve.Name = "power: " + thisPowerCurve.name;
                        thrustCurve.Name = "thrustCoeff: " + thisPowerCurve.name;

                        powerCurve.InteractivityStyle.Tooltip = new NTooltipAttribute(powerCurve.Name);
                        thrustCurve.InteractivityStyle.Tooltip = new NTooltipAttribute(thrustCurve.Name);

                        if (i == 0) {
                            powerCurve.BorderStyle.Color = Color.Red;
                            thrustCurve.BorderStyle.Color = Color.CornflowerBlue;
                        }
                        else if (i == 1) {
                            powerCurve.BorderStyle.Color = Color.BlueViolet;
                            thrustCurve.BorderStyle.Color = Color.BlueViolet;
                        }
                        else if (i == 2) {
                            powerCurve.BorderStyle.Color = Color.Coral;
                            thrustCurve.BorderStyle.Color = Color.Coral;
                        }
                        else if (i == 3) {
                            powerCurve.BorderStyle.Color = Color.DarkGreen;
                            thrustCurve.BorderStyle.Color = Color.DarkGreen;
                        }
                        else if (i == 4) {
                            powerCurve.BorderStyle.Color = Color.Firebrick;
                            thrustCurve.BorderStyle.Color = Color.Firebrick;
                        }
                        else {
                            powerCurve.BorderStyle.Color = Color.Gold;
                            thrustCurve.BorderStyle.Color = Color.Gold;
                        }
                    }
                }

                double height = thisInst.metList.metItem[0].height;

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
            origModel.setDefaultModelCoeffs(numWD);
            NodeCollection nodeList = new NodeCollection();

            if (DH_plot_to_show == "Separated flow") {
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
                else {
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

                if (WD_Ind < numWD) {
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
            else {
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
                else {

                    if (WD_Ind < numWD) {
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
                    else {
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

                if (thisModel.isImported == false) {
                    posDW_ScatterSeries.Name = "DW > 0";
                    posDW_ScatterSeries.InteractivityStyle.Tooltip = new NTooltipAttribute("DW > 0");
                }
                else {
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

            }
        }

        public void DownhillRoughPlot(Continuum thisInst, Model thisModel)
        {
            // Updates plot on Advanced tab showing DW Stability factor (used in the surface roughness model) radar plot 
            int numWD = thisInst.GetNumWD();
            double[] DH_Stab = new double[numWD];   // DW Stability factor by WD sector

            Model origModel = new Model();
            origModel.SizeArrays(numWD);
            origModel.setDefaultModelCoeffs(numWD);

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
                if (UW_to_show == "UW > UW crit") {
                    if (thisModel.UH_Stab_A[i] < 5)
                        UH_Stab[i] = thisModel.UH_Stab_A[i];
                }
                else { // UW < UW crit, induced speed-up
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
            origModel.setDefaultModelCoeffs(numWD);
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
                    else {
                        P10_Expo_NegDW[0] = 1;
                        P10_Expo_NegDW[1] = thisModel.UW_crit[WD_Ind] / 4;
                        P10_Expo_NegDW[2] = thisModel.UW_crit[WD_Ind] / 2;
                        P10_Expo_NegDW[3] = thisModel.UW_crit[WD_Ind] * 3 / 4;
                        P10_Expo_NegDW[4] = thisModel.UW_crit[WD_Ind];

                        for (int i = 0; i <= 4; i++)
                            negDW_Coeffs[i] = thisModel.spdUp_A[WD_Ind] * (double)Math.Pow(P10_Expo_NegDW[i], thisModel.spdUp_B[WD_Ind]);
                    }
                }
                else {
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
                            else {
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

                if (thisModel.isImported == false) {
                    UH_DW0_Scatter_Series.Name = "DW < 0";
                    UH_UWcrit_Scatter_Series.Name = "UW > UW crit";
                }
                else {
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

            DH_Ctl.Controller.Tools.Clear();
            UH_Ctl.Controller.Tools.Clear();

            NTooltipTool DH_ToolTip = new NTooltipTool();
            DH_Ctl.Controller.Tools.Add(DH_ToolTip);
            NTooltipTool UH_ToolTip = new NTooltipTool();
            UH_Ctl.Controller.Tools.Add(UH_ToolTip);

            int numModels = thisInst.modelList.ModelCount;

            if (numModels == 0 || thisInst.metList.expoIsCalc == false)
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

            bool isCalibrated = thisInst.GetSelectedModel("Advanced");
            int radiusInd = thisInst.GetRadiusInd("Advanced");
            int radius = thisInst.radiiList.investItem[radiusInd].radius;
            
            Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), radius, radius, isCalibrated);
            Model thisModel = theseModels[0];

            if (thisModel == null)
                return;

            string UH_Plot_to_show = "";

            try {
                UH_Plot_to_show = thisInst.cboUphill_to_show.SelectedItem.ToString();
            }
            catch  {
                thisInst.cboUphill_to_show.SelectedIndex = 0;
                UH_Plot_to_show = thisInst.cboUphill_to_show.SelectedItem.ToString();
            }

            string DH_Plot_to_show = "";

            try {
                DH_Plot_to_show = thisInst.cboDHplot.SelectedItem.ToString();
            }
            catch {
                thisInst.cboDHplot.SelectedIndex = 0;
                DH_Plot_to_show = thisInst.cboDHplot.SelectedItem.ToString();
            }

            if (DH_Plot_to_show == "Separated Flow" && thisInst.topo.useSepMod == false)
            {
                thisInst.cboDHplot.SelectedIndex = 0;
                DH_Plot_to_show = thisInst.cboDHplot.SelectedItem.ToString();
            }

            string Expo_or_Rough = "";

            try {
                Expo_or_Rough = thisInst.cboExpo_or_Stab.SelectedItem.ToString();
            }
            catch {
                thisInst.cboExpo_or_Stab.SelectedIndex = 0;
                return;
            }

            if (Expo_or_Rough == "Exposure")
            {
                DownhillLogLogPlot(thisInst, thisModel, DH_Plot_to_show); // updates scatter and log-log fit of Uphill plot
                UphillLogLogPlot(thisInst, thisModel, UH_Plot_to_show);
            }
            else {
                DownhillRoughPlot(thisInst, thisModel);
                UphillRoughPlot(thisInst, thisModel, UH_Plot_to_show);
            }

            double UW_critical = 0;
            double sepCrit;
            double sepCritWS;

            if (WD_Ind < numWD)
            {
                if (thisModel.UW_crit[WD_Ind] != 4 && thisModel.UW_crit[WD_Ind] != 500)
                    UW_critical = thisModel.UW_crit[WD_Ind];

                sepCrit = thisModel.sepCrit[WD_Ind];
                sepCritWS = thisModel.Sep_crit_WS[WD_Ind];
            }
            else {
                UW_critical = thisModel.CalcOverallUWCrit(thisInst.metList.GetAvgWindRose());
                sepCrit = thisModel.CalcOverallSepCrit(thisInst.metList.GetAvgWindRose());
                sepCritWS = thisModel.CalcOverallSepCritWS(thisInst.metList.GetAvgWindRose());
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

            try {
                topoOrRough = thisInst.cboTopo_Or_Roughness.SelectedItem.ToString();
            }
            catch {
                topoOrRough = "Topography";
            }

            if (topoOrRough == "Topography" && thisInst.topo.gotTopo == false) {
                // no topo data yet
                if (thisInst.topo.gotSR == true) {
                    thisInst.cboTopo_Or_Roughness.SelectedIndex = 1;
                    topoOrRough = thisInst.cboTopo_Or_Roughness.SelectedItem.ToString();
                }
            }

            if ((topoOrRough == "Surface roughness" || topoOrRough == "Displacement height" || topoOrRough == "Land Cover") && thisInst.topo.gotSR == false)
            {
                // no topo data yet
                if (thisInst.topo.gotTopo == true) {
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
            } else {
                try {
                    thisMin = Convert.ToSingle(thisInst.txtMainMin.Text);
                }
                catch  {

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
            else {
                try {
                    thisMax = Convert.ToSingle(thisInst.txtMainMax.Text);
                    double thisMin = GetTopoMapMin(thisInst, paramToPlot);
                    if (thisMax <= thisMin)
                    {
                        thisMax = thisMax + 1;
                        thisInst.txtMainMax.Text = thisMax.ToString();
                    }
                }
                catch {
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
            else {
                try {
                    intWidth = Convert.ToSingle(thisInst.txtMainInt.Text);

                    if (intWidth <= 0)
                    {
                        intWidth = (thisMax - thisMin) / (newNumLevels - 1);
                        thisInst.txtMainInt.Text = intWidth.ToString();
                    }
                }
                catch  {
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
                NChart topoMap = thisInst.cht_NevTopo.Charts[0];
                topoMap.Projection.SetPredefinedProjection(PredefinedProjection.OrthogonalTop);
                topoMap.Series.Clear();
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

                if ((topoOrRough == "Surface roughness" || topoOrRough == "Displacement height" || topoOrRough == "Land Cover") && thisInst.topo.gotSR == true)
                {
                    // Plot land cover, surface roughness, or displacement height
                    if (topoOrRough == "Surface roughness") paramToPlot = thisInst.topo.GetLC_ParamToPlot("Surface roughness");
                    if (topoOrRough == "Displacement height") paramToPlot = thisInst.topo.GetLC_ParamToPlot("Displacement height");
                    if (topoOrRough == "Land Cover") paramToPlot = thisInst.topo.GetLC_ParamToPlot("Land Cover");

                    if (paramToPlot == null)
                        return;

                    numX_Plot = paramToPlot.GetUpperBound(0);
                    numY_Plot = paramToPlot.GetUpperBound(1);
                }
                else if ((topoOrRough == "Topography" && thisInst.topo.gotTopo == true)) { // Topography plot
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

                if ((topoOrRough == "Surface roughness"))
                    topoSurface.ValueFormatter.FormatSpecifier = "0.000";
                else
                    topoSurface.ValueFormatter.FormatSpecifier = "0";

                topoSurface.Palette.HasCustomMin = true;
                topoSurface.Palette.CustomMin = thisMin - thisMin * 0.05;
                topoSurface.Palette.Mode = PaletteMode.Custom;
                topoSurface.Palette.PaletteSteps = newNumLevels;

                for (int i = 0; i < newNumLevels; i++)
                {
                    if (topoOrRough == "Surface roughness")
                        topoSurface.Palette.Add(thisMin + i * intWidth, GetRGB_Values((double)i / newNumLevels));
                    else
                        topoSurface.Palette.Add(Math.Round(thisMin + i * intWidth, 0), GetRGB_Values((double)i / newNumLevels));
                }

                topoSurface.Legend.Mode = SeriesLegendMode.SeriesLogic;
                topoSurface.Legend.Format = "<zone_begin> - <zone_end>";

                NLegend Topo_Legend = new NLegend();
                thisInst.cht_NevTopo.Charts[0].DisplayOnLegend = Topo_Legend;
                thisInst.cht_NevTopo.Panels.Add(Topo_Legend);
                Topo_Legend.DockMode = PanelDockMode.Right;

                for (int i = 0; i < numX_Plot; i++)
                {
                    for (int j = 0; j < numY_Plot; j++)
                    {
                        if ((topoOrRough == "Surface roughness" || topoOrRough == "Displacement height" || topoOrRough == "Land Cover"))
                        {
                            int thisX = Convert.ToInt32(thisInst.topo.LC_NumXY.X.plot.min + i * thisInst.topo.LC_NumXY.X.plot.reso);
                            int thisY = (int)(thisInst.topo.LC_NumXY.Y.plot.min + j * thisInst.topo.LC_NumXY.Y.plot.reso);
                            topoSurface.Data.SetValue(i, j, paramToPlot[i, j], thisX, thisY);
                        }
                        else {
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

                if (thisMin == thisMax) {
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
                NodeCollection.Node_UTMs This_node_UTM;
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

                if (isMetPair == true) {
                    if (thisPair.met1.UTMX < thisPair.met2.UTMX)
                    {
                        minX = thisPair.met1.UTMX;
                        maxX = thisPair.met2.UTMX;
                    }
                    else {
                        minX = thisPair.met2.UTMX;
                        maxX = thisPair.met1.UTMX;
                    }

                    if (thisPair.met1.UTMY < thisPair.met2.UTMY)
                    {
                        minY = thisPair.met1.UTMY;
                        maxY = thisPair.met2.UTMY;
                    }
                    else {
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
                                if (thisTurb.WS_Estimate[j].predictorMetName == met1Str) {
                                    foundTurb = true;
                                    endTurb = thisTurb;
                                    break;
                                }
                            }
                        }

                        if (foundTurb == true)
                            break;
                    }

                    if (thisMet.UTMX < thisTurb.UTMX) {
                        minX = thisMet.UTMX;
                        maxX = thisTurb.UTMX;
                    }
                    else {
                        minX = thisTurb.UTMX;
                        maxX = thisMet.UTMX;
                    }

                    if (thisMet.UTMY < thisTurb.UTMY) {
                        minY = thisMet.UTMY;
                        maxY = thisTurb.UTMY;
                    }
                    else {
                        minY = thisTurb.UTMY;
                        maxY = thisMet.UTMY;
                    }

                    int WS_PredInd = 0;

                    for (int i = 0; i < thisTurb.WSEst_Count; i++)
                        if (thisTurb.WS_Estimate[i].predictorMetName == thisMet.name && thisTurb.WS_Estimate[i].radius == radius)
                        {
                            WS_PredInd = i;
                            break;
                        }

                    try {
                        numNodes = thisTurb.WS_Estimate[WS_PredInd].pathOfNodesUTMs.Length;
                    } catch  {
                        numNodes = 0;
                    }

                    for (int j = 0; j < numNodes; j++)
                    {
                        This_node_UTM = thisTurb.WS_Estimate[WS_PredInd].pathOfNodesUTMs[j];
                        if (This_node_UTM.UTMX < minX) minX = This_node_UTM.UTMX;
                        if (This_node_UTM.UTMX > maxX) maxX = This_node_UTM.UTMX;
                        if (This_node_UTM.UTMY < minY) minY = This_node_UTM.UTMY;
                        if (This_node_UTM.UTMY > maxY) maxY = This_node_UTM.UTMY;
                    }
                }
                else { // single met and no turbines
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
                int numY = (int)((maxY - minY) / (topo.topoNumXY.Y.plot.reso));
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
            thisInst.metList.ClearAllMets();
            thisInst.metPairList.ClearAll(); // clears met pairs and round robin ests
            thisInst.turbineList.ClearAllTurbines();
            thisInst.turbineList.ClearAllPowerCurves();
            thisInst.mapList.ClearAllMaps();
            thisInst.modelList.ClearAll();
            thisInst.wakeModelList.ClearAll();
            thisInst.savedParams.ClearAll();
            thisInst.UTM_conversions.ResetDefaults();
            thisInst.otherLosses.Set_Defaults();
                        
            thisInst.txtTopoSource.Clear();
            thisInst.txtUTMDatum.Clear();
            thisInst.txtUTMZone.Clear();                      

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
            thisInst.cboAdvancedModel.Items.Clear();
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
                AddAxisToRadarPlot(ref WR_Chart, numWD);

                for (int i = 0; i < metCount; i++)
                    AddSeriesToRadarPlot(ref WR_Chart, checkedMets[i].windRose, checkedMets[i].name, GetMetOrTurbColor(100 * i / metCount), true, false);
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

        public void AddAxisToRadarPlot(ref NRadarChart thisRadar, int numWD)
        {
            // Adds axis to referenced Radar chart
            for (int i = 0; i <= numWD - 1; i++)
            {
                NRadarAxis Axis = new NRadarAxis();

                if ((i <= numWD / 2))
                    Axis.Title = Convert.ToString((numWD / 2 - i) * (double)360 / numWD);
                else
                    Axis.Title = Convert.ToString((numWD / 2 - i) * (double)360 / numWD + 360);


                NLinearScaleConfigurator linearScale = (NLinearScaleConfigurator)(Axis.ScaleConfigurator);
                linearScale.RulerStyle.BorderStyle.Color = Color.Silver;
                linearScale.InnerMajorTickStyle.LineStyle.Color = Color.Silver;
                linearScale.OuterMajorTickStyle.LineStyle.Color = Color.Silver;
                linearScale.InnerMajorTickStyle.Length = new NLength(2, NGraphicsUnit.Point);
                linearScale.OuterMajorTickStyle.Length = new NLength(2, NGraphicsUnit.Point);

                if ((thisRadar.Axes.Count == 0)) {
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
                else {
                    // hide labels
                    linearScale.AutoLabels = false;
                }

                Axis.Visible = true;
                Axis.TitleTextStyle.FontStyle.EmSize = new NLength(10);

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

            if (checkedMets == null) {
                Dir_cht_Nev.Refresh();
                return;
            }

            if (checkedMets.Length > 0)
            {
                int numWD = thisInst.GetNumWD();
                AddAxisToRadarPlot(ref Dir_Chart, numWD);

                for (int i = 0; i < checkedMets.Length; i++)
                {
                    Color lineColor = GetMetOrTurbColor(i);
                    AddSeriesToRadarPlot(ref Dir_Chart, checkedMets[i].sectorWS_Ratio, checkedMets[i].name, lineColor, true, false);
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
                    if (mapToPlot == thisInst.mapList.mapItem[j].mapName) {
                        thisMap = thisInst.mapList.mapItem[j];
                        break;
                    }
                }

                NChart genMap = thisInst.c3DMaps_Nev.Charts[0];
                genMap.Projection.SetPredefinedProjection(PredefinedProjection.OrthogonalTop);
                genMap.Series.Clear();
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

                try {
                    thisMin = Convert.ToSingle(thisInst.txtMinValue.Text);
                }
                catch  {
                    thisMin = thisMap.FindMin(WD_Ind, numWD);
                    thisInst.txtMinValue.Text = Math.Round(thisMin, 3).ToString();
                }

                try {
                    thisMax = Convert.ToSingle(thisInst.txtMaxValue.Text);
                }
                catch  {
                    thisMax = thisMap.FindMax(WD_Ind, numWD);
                    thisInst.txtMaxValue.Text = Math.Round(thisMax, 3).ToString();
                }

                if (thisMin == thisMax)
                {
                    thisMin = thisMin - thisMin * 0.1f;
                    thisMax = thisMax + thisMax * 0.1f;
                }

                try {
                    intWidth = Convert.ToSingle(thisInst.txtIntLevel.Text);
                }
                catch  {
                    intWidth = (thisMax - thisMin) / (newNumLevels - 1);
                    thisInst.txtIntLevel.Text = Math.Round(intWidth, 2).ToString();
                }

                if (thisInst.chkAutoMinMax.Checked == true)
                {
                    thisMin = thisMap.FindMin(WD_Ind, numWD);
                    thisMax = thisMap.FindMax(WD_Ind, numWD);

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
                    else if (thisMap.modelType <= 1) {
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
                    else {
                        thisMin = Math.Round(thisMin, 0);
                        thisMax = Math.Round(thisMax, 0);

                        if (thisMin == thisMax) {
                            thisMin = thisMin - thisMin * 0.02f;
                            thisMax = thisMax + thisMax * 0.02f;
                        }
                        intWidth = (thisMax - thisMin) / (newNumLevels - 1);

                        //  intWidth = Math.Round(intWidth, 0)
                    }

                    thisInst.txtMinValue.Text = thisMin.ToString();
                    thisInst.txtMaxValue.Text = thisMax.ToString();
                    thisInst.txtIntLevel.Text = Math.Round(intWidth,3).ToString();
                }

                if (thisMap.modelType == 2 || thisMap.modelType == 4 || thisMap.isWaked)
                {
                    thisMin = Math.Round(thisMin, 2);
                    thisMax = Math.Round(thisMax, 2);
                }
                else if (thisMap.modelType <= 1) {
                    thisMin = Math.Round(thisMin, 1);
                    thisMax = Math.Round(thisMax, 1);
                }
                else {
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

                if (WD_Ind == numWD) {
                    singPar = thisMap.parameterToMap;
                }
                else if (thisMap.modelType != 2 && thisMap.modelType != 4 && thisMap.isWaked == false)
                {
                    try {                       
                        singPar = thisMap.parameterToMap;                        
                        thisInst.cboMapWD.SelectedIndex = numWD;
                        MessageBox.Show("Sectorwise exposure maps not currently calculated. Resetting to Overall WD", "Continuum 2.3");
                    }
                    catch  {
                        return;
                    }
                }                
                else {
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
            else {
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

            try {
                min = Convert.ToSingle(thisInst.txtMainMin.Text);
                max = Convert.ToSingle(thisInst.txtMainMax.Text);
                interval = Convert.ToSingle(thisInst.txtMainInt.Text);
            }
            catch  {
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
            else {
                thisInst.txtMainMin.Text = thisInst.savedParams.topoMapMin.ToString();
                thisInst.txtMainMax.Text = thisInst.savedParams.topoMapMax.ToString();
                thisInst.txtMainInt.Text = Math.Round(thisInst.savedParams.topoMapInterval,1).ToString();
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
            WindDirectionToDisplay(thisInst);
            RadiusToDisplay("Summary", thisInst);
            RadiusToDisplay("Adv", thisInst);
            modelDropdown(thisInst);

            ColoredButtons(thisInst);
            LandCoverFlowSepUsedTexts(thisInst);

            thisInst.okToUpdate = true;
            InputTAB(thisInst);
            Met_Turbine_Summary_TAB(thisInst);
            GrossTurbineEstsTAB(thisInst);
            NetTurbineEstsTAB(thisInst);
            MapsTAB(thisInst);
            Uncertainty_TAB_Round_Robin(thisInst);
            Uncertainty_TAB_Turbine_Ests(thisInst);
            SetDefaultCheckAdvanced(thisInst);
            AdvancedTAB(thisInst);
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
                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE Topo_table");
                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE LandCover_table");
                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE GridStat_table");
                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE Node_table");
                    ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE Expo_table");
                    
                    ctx.SaveChanges();
                }
            }
            catch 
            { }
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
    }

}
