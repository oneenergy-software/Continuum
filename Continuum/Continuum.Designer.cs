namespace ContinuumNS
{
    partial class Continuum
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Continuum));
            this.tabContinuum = new System.Windows.Forms.TabControl();
            this.pgeInput = new System.Windows.Forms.TabPage();
            this.plotDirectionalWS_Ratios = new OxyPlot.WindowsForms.PlotView();
            this.plotInputWindRose = new OxyPlot.WindowsForms.PlotView();
            this.plotTopo = new OxyPlot.WindowsForms.PlotView();
            this.chkCreateTurbTS = new System.Windows.Forms.CheckBox();
            this.btnModHeight = new System.Windows.Forms.Button();
            this.label168 = new System.Windows.Forms.Label();
            this.txtModeledHeight = new System.Windows.Forms.TextBox();
            this.btnImportMetTS = new System.Windows.Forms.Button();
            this.btnImportRoughness = new System.Windows.Forms.Button();
            this.chk_Use_Sep = new System.Windows.Forms.CheckBox();
            this.Label84 = new System.Windows.Forms.Label();
            this.txt_LC_Key_selected = new System.Windows.Forms.TextBox();
            this.chkUseSR = new System.Windows.Forms.CheckBox();
            this.btnViewModNLCD = new System.Windows.Forms.Button();
            this.cboTopo_Or_Roughness = new System.Windows.Forms.ComboBox();
            this.txtUTMZone = new System.Windows.Forms.TextBox();
            this.Label57 = new System.Windows.Forms.Label();
            this.txtUTMDatum = new System.Windows.Forms.TextBox();
            this.Label49 = new System.Windows.Forms.Label();
            this.btnAnalyzeMets = new System.Windows.Forms.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.btnImportTAB = new System.Windows.Forms.Button();
            this.btnGenTurbEsts = new System.Windows.Forms.Button();
            this.Label30 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.chkAllTurbLabels = new System.Windows.Forms.CheckBox();
            this.chkAllMetLabels = new System.Windows.Forms.CheckBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.lblMetLabels = new System.Windows.Forms.Label();
            this.chkTurbLabels = new System.Windows.Forms.CheckedListBox();
            this.chkMetLabels = new System.Windows.Forms.CheckedListBox();
            this.txtMainMax = new System.Windows.Forms.TextBox();
            this.txtMainMin = new System.Windows.Forms.TextBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.btnDelTurb = new System.Windows.Forms.Button();
            this.btnEditTurb = new System.Windows.Forms.Button();
            this.btnAddTurb = new System.Windows.Forms.Button();
            this.lstTurbines = new System.Windows.Forms.ListView();
            this.ColumnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtTopoSource = new System.Windows.Forms.TextBox();
            this.lblTurbineList = new System.Windows.Forms.Label();
            this.btnDelMet = new System.Windows.Forms.Button();
            this.lstMetTowers = new System.Windows.Forms.ListView();
            this.metName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lats = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Longs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WindSpd1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblMetTable = new System.Windows.Forms.Label();
            this.btnTurbines = new System.Windows.Forms.Button();
            this.btnLoadXYZ = new System.Windows.Forms.Button();
            this.pgeMetData = new System.Windows.Forms.TabPage();
            this.chkMaxWS_Range = new System.Windows.Forms.CheckBox();
            this.chkMaxWS_SD = new System.Windows.Forms.CheckBox();
            this.chkMinWS_SD = new System.Windows.Forms.CheckBox();
            this.chkMinWS = new System.Windows.Forms.CheckBox();
            this.chkIcing = new System.Windows.Forms.CheckBox();
            this.chkTowerShadow = new System.Windows.Forms.CheckBox();
            this.cboSelVane = new System.Windows.Forms.ComboBox();
            this.label184 = new System.Windows.Forms.Label();
            this.cboAnemB = new System.Windows.Forms.ComboBox();
            this.label155 = new System.Windows.Forms.Label();
            this.cboAnemA = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.plotWSDiffByWS = new OxyPlot.WindowsForms.PlotView();
            this.plotWSDiffByWD = new OxyPlot.WindowsForms.PlotView();
            this.plotAnemScatter = new OxyPlot.WindowsForms.PlotView();
            this.plotAlphaByWD = new OxyPlot.WindowsForms.PlotView();
            this.plotMetQC_WindRose = new OxyPlot.WindowsForms.PlotView();
            this.plotWS_vsHeight = new OxyPlot.WindowsForms.PlotView();
            this.chkDisableFilter = new System.Windows.Forms.CheckBox();
            this.btnViewFilters = new System.Windows.Forms.Button();
            this.Export_End = new System.Windows.Forms.DateTimePicker();
            this.label105 = new System.Windows.Forms.Label();
            this.Export_Start = new System.Windows.Forms.DateTimePicker();
            this.btnExportAnnualMax = new System.Windows.Forms.Button();
            this.btnExportExtrap = new System.Windows.Forms.Button();
            this.btnExportAlpha = new System.Windows.Forms.Button();
            this.btnExportFlags = new System.Windows.Forms.Button();
            this.label104 = new System.Windows.Forms.Label();
            this.cboMetQC_SelectedMet = new System.Windows.Forms.ComboBox();
            this.cboFilt_or_Not = new System.Windows.Forms.ComboBox();
            this.label102 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.lstExtrapolated = new System.Windows.Forms.ListView();
            this.columnHeader82 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader84 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader83 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label68 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.lstTempSummary = new System.Windows.Forms.ListView();
            this.columnHeader80 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader81 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label66 = new System.Windows.Forms.Label();
            this.lstVaneSummary = new System.Windows.Forms.ListView();
            this.columnHeader75 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader76 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader78 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader79 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label65 = new System.Windows.Forms.Label();
            this.cboMetWindRose = new System.Windows.Forms.ComboBox();
            this.label64 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.lstAlphas = new System.Windows.Forms.ListView();
            this.columnHeader74 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader86 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader87 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader114 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label61 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.End_Time = new System.Windows.Forms.DateTimePicker();
            this.All_End_Time = new System.Windows.Forms.DateTimePicker();
            this.label43 = new System.Windows.Forms.Label();
            this.Start_Time = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.All_Start_Time = new System.Windows.Forms.DateTimePicker();
            this.label24 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstAnemSummary = new System.Windows.Forms.ListView();
            this.height = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader64 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader65 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader66 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader67 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader68 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader69 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader70 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader71 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader72 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader73 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pgeMERRA = new System.Windows.Forms.TabPage();
            this.label54 = new System.Windows.Forms.Label();
            this.txtMaxLong = new System.Windows.Forms.TextBox();
            this.label146 = new System.Windows.Forms.Label();
            this.txtMinLong = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.txtMaxLat = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMinLat = new System.Windows.Forms.TextBox();
            this.btnChangeFolder = new System.Windows.Forms.Button();
            this.btnDownloadMERRA2 = new System.Windows.Forms.Button();
            this.plotMERRA_WindRose = new OxyPlot.WindowsForms.PlotView();
            this.plotMERRA_Monthly = new OxyPlot.WindowsForms.PlotView();
            this.plotMERRA_Yearly = new OxyPlot.WindowsForms.PlotView();
            this.btnImportCRV_MERRA = new System.Windows.Forms.Button();
            this.chkYearsToDisplayAll = new System.Windows.Forms.CheckBox();
            this.label121 = new System.Windows.Forms.Label();
            this.cboMERRA_Month = new System.Windows.Forms.ComboBox();
            this.label120 = new System.Windows.Forms.Label();
            this.cboMERRAYear = new System.Windows.Forms.ComboBox();
            this.chkYearsToDisplay = new System.Windows.Forms.CheckedListBox();
            this.label162 = new System.Windows.Forms.Label();
            this.txtMERRA_WS_ScaleFact = new System.Windows.Forms.TextBox();
            this.cboNumMERRA_Nodes = new System.Windows.Forms.ComboBox();
            this.label161 = new System.Windows.Forms.Label();
            this.label160 = new System.Windows.Forms.Label();
            this.cboMERRA_PowerCurves = new System.Windows.Forms.ComboBox();
            this.btn_ExportWR = new System.Windows.Forms.Button();
            this.btn_Export_All_Months_All_Years = new System.Windows.Forms.Button();
            this.label159 = new System.Windows.Forms.Label();
            this.label156 = new System.Windows.Forms.Label();
            this.cboMERRA_PlotParam = new System.Windows.Forms.ComboBox();
            this.lstMERRAAnnualProd = new System.Windows.Forms.ListView();
            this.columnHeader85 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader116 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader117 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstMERRA_MonthlyProd = new System.Windows.Forms.ListView();
            this.Month = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ThisYear = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Diff = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Average = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_Export_Interp = new System.Windows.Forms.Button();
            this.dateMERRAEnd = new System.Windows.Forms.DateTimePicker();
            this.dateMERRAStart = new System.Windows.Forms.DateTimePicker();
            this.label152 = new System.Windows.Forms.Label();
            this.label153 = new System.Windows.Forms.Label();
            this.label147 = new System.Windows.Forms.Label();
            this.cboMERRASelectedMet = new System.Windows.Forms.ComboBox();
            this.txtMERRA_SelectedLong = new System.Windows.Forms.TextBox();
            this.label149 = new System.Windows.Forms.Label();
            this.label150 = new System.Windows.Forms.Label();
            this.txtMERRA_SelectedLat = new System.Windows.Forms.TextBox();
            this.label154 = new System.Windows.Forms.Label();
            this.txt_MERRA2_folder = new System.Windows.Forms.TextBox();
            this.btn_Import_MERRA = new System.Windows.Forms.Button();
            this.label151 = new System.Windows.Forms.Label();
            this.pgeMCP = new System.Windows.Forms.TabPage();
            this.btnExportTarget = new System.Windows.Forms.Button();
            this.plotMCP_Uncertainty = new OxyPlot.WindowsForms.PlotView();
            this.plotMCP = new OxyPlot.WindowsForms.PlotView();
            this.btnShowMCPRanges = new System.Windows.Forms.Button();
            this.label51 = new System.Windows.Forms.Label();
            this.btnDoMCPAllMets = new System.Windows.Forms.Button();
            this.btnDoMCP = new System.Windows.Forms.Button();
            this.btnResetDates = new System.Windows.Forms.Button();
            this.dateMCPExportEnd = new System.Windows.Forms.DateTimePicker();
            this.label145 = new System.Windows.Forms.Label();
            this.dateMCPExportStart = new System.Windows.Forms.DateTimePicker();
            this.dateConcurrentEnd = new System.Windows.Forms.DateTimePicker();
            this.label144 = new System.Windows.Forms.Label();
            this.dateConcurrentStart = new System.Windows.Forms.DateTimePicker();
            this.txtTAB_WS_bin = new System.Windows.Forms.TextBox();
            this.label142 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.cboTAB_bins = new System.Windows.Forms.ComboBox();
            this.label141 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.txtLast_WS_Wgt = new System.Windows.Forms.TextBox();
            this.label134 = new System.Windows.Forms.Label();
            this.txtWS_PDF_Wgt = new System.Windows.Forms.TextBox();
            this.label135 = new System.Windows.Forms.Label();
            this.cboMCPNumSeasons = new System.Windows.Forms.ComboBox();
            this.label136 = new System.Windows.Forms.Label();
            this.cboMCPNumHours = new System.Windows.Forms.ComboBox();
            this.label137 = new System.Windows.Forms.Label();
            this.txtWS_bin_width = new System.Windows.Forms.TextBox();
            this.label138 = new System.Windows.Forms.Label();
            this.cboMCPNumWD = new System.Windows.Forms.ComboBox();
            this.label139 = new System.Windows.Forms.Label();
            this.cboMCP_Type = new System.Windows.Forms.ComboBox();
            this.label133 = new System.Windows.Forms.Label();
            this.txtNumYrsConc = new System.Windows.Forms.TextBox();
            this.label132 = new System.Windows.Forms.Label();
            this.txtRsq = new System.Windows.Forms.TextBox();
            this.label130 = new System.Windows.Forms.Label();
            this.btnExportBinRatios = new System.Windows.Forms.Button();
            this.txtIntercept = new System.Windows.Forms.TextBox();
            this.txtSlope = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.lstMCP_Bins = new System.Windows.Forms.ListView();
            this.WS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Mean = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblRegStats = new System.Windows.Forms.Label();
            this.txtNumYrsTarg = new System.Windows.Forms.TextBox();
            this.txtNumYrsRef = new System.Windows.Forms.TextBox();
            this.label127 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.btnExportMCP_TAB = new System.Windows.Forms.Button();
            this.btnExportMCP_TS = new System.Windows.Forms.Button();
            this.cboUncertStep = new System.Windows.Forms.ComboBox();
            this.label119 = new System.Windows.Forms.Label();
            this.lstMCP_Uncert = new System.Windows.Forms.ListView();
            this.Window = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AVG = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SDU = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnExportMCPUncert = new System.Windows.Forms.Button();
            this.label118 = new System.Windows.Forms.Label();
            this.btnMCP_Uncert = new System.Windows.Forms.Button();
            this.label108 = new System.Windows.Forms.Label();
            this.cboMCP_Season = new System.Windows.Forms.ComboBox();
            this.label109 = new System.Windows.Forms.Label();
            this.cboMCP_TOD = new System.Windows.Forms.ComboBox();
            this.label110 = new System.Windows.Forms.Label();
            this.txtLTratio = new System.Windows.Forms.TextBox();
            this.txtAvgRatio = new System.Windows.Forms.TextBox();
            this.label111 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.txtDataCount = new System.Windows.Forms.TextBox();
            this.txtTarg_LT_WS = new System.Windows.Forms.TextBox();
            this.txtRef_LT_WS = new System.Windows.Forms.TextBox();
            this.label113 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.cboMCP_WD = new System.Windows.Forms.ComboBox();
            this.txtTargAvgWS = new System.Windows.Forms.TextBox();
            this.txtRefAvgWS = new System.Windows.Forms.TextBox();
            this.label115 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.cboMCP_Met = new System.Windows.Forms.ComboBox();
            this.label106 = new System.Windows.Forms.Label();
            this.pgeMetSumm = new System.Windows.Forms.TabPage();
            this.plotDW_DH = new OxyPlot.WindowsForms.PlotView();
            this.plotUW_DH = new OxyPlot.WindowsForms.PlotView();
            this.plotDW_SR = new OxyPlot.WindowsForms.PlotView();
            this.plotUW_SR = new OxyPlot.WindowsForms.PlotView();
            this.plotElev = new OxyPlot.WindowsForms.PlotView();
            this.plotDWUWExpo = new OxyPlot.WindowsForms.PlotView();
            this.plotDWExpo = new OxyPlot.WindowsForms.PlotView();
            this.plotUWExpo = new OxyPlot.WindowsForms.PlotView();
            this.cboSummSeason = new System.Windows.Forms.ComboBox();
            this.cboSummTOD = new System.Windows.Forms.ComboBox();
            this.Label80 = new System.Windows.Forms.Label();
            this.lstTurbStats = new System.Windows.Forms.ListView();
            this.ColumnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader34 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader35 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader33 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkTurbSummAll = new System.Windows.Forms.CheckBox();
            this.chkTurbSumm = new System.Windows.Forms.CheckedListBox();
            this.Label78 = new System.Windows.Forms.Label();
            this.chkMetSummAll = new System.Windows.Forms.CheckBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.chkMetSumm = new System.Windows.Forms.CheckedListBox();
            this.Label76 = new System.Windows.Forms.Label();
            this.lstMetStats = new System.Windows.Forms.ListView();
            this.ColumnHeader42 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader91 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader93 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader92 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader52 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnExportExpoSRDH = new System.Windows.Forms.Button();
            this.Label74 = new System.Windows.Forms.Label();
            this.cboSummaryWD = new System.Windows.Forms.ComboBox();
            this.Label75 = new System.Windows.Forms.Label();
            this.cboMetSum_Rad = new System.Windows.Forms.ComboBox();
            this.lstMetSummary = new System.Windows.Forms.ListView();
            this.ColumnHeader37 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader40 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader41 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader43 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader44 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader46 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader47 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader48 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader49 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader50 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader51 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label73 = new System.Windows.Forms.Label();
            this.pgeGrossTurbs = new System.Windows.Forms.TabPage();
            this.plotGrossHisto = new OxyPlot.WindowsForms.PlotView();
            this.plotGross_PowerCrvs = new OxyPlot.WindowsForms.PlotView();
            this.plotGrossWS_Dist = new OxyPlot.WindowsForms.PlotView();
            this.plotGrossWindRose = new OxyPlot.WindowsForms.PlotView();
            this.btnGenTurbGross = new System.Windows.Forms.Button();
            this.lstPowerCurveList = new System.Windows.Forms.ListView();
            this.columnHeader186 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader193 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader194 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtisMCPdGross = new System.Windows.Forms.TextBox();
            this.Label40 = new System.Windows.Forms.Label();
            this.cboWS_or_WD = new System.Windows.Forms.ComboBox();
            this.txtGross_FlowSepUsed = new System.Windows.Forms.TextBox();
            this.Label96 = new System.Windows.Forms.Label();
            this.lstGrossHisto = new System.Windows.Forms.ListView();
            this.ColumnHeader103 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader104 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader105 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtGross_LC_used = new System.Windows.Forms.TextBox();
            this.Label83 = new System.Windows.Forms.Label();
            this.Label79 = new System.Windows.Forms.Label();
            this.chkMetGrossAll = new System.Windows.Forms.CheckBox();
            this.Label50 = new System.Windows.Forms.Label();
            this.chkMetGross = new System.Windows.Forms.CheckedListBox();
            this.Label77 = new System.Windows.Forms.Label();
            this.cboGrossParam = new System.Windows.Forms.ComboBox();
            this.btnExportDirWSDists = new System.Windows.Forms.Button();
            this.Label71 = new System.Windows.Forms.Label();
            this.cboGrossWD = new System.Windows.Forms.ComboBox();
            this.btnExportDirWS = new System.Windows.Forms.Button();
            this.btnExportCRV = new System.Windows.Forms.Button();
            this.btnExportWSDists = new System.Windows.Forms.Button();
            this.btnWS_AEP_Exprt = new System.Windows.Forms.Button();
            this.txtAEPMax = new System.Windows.Forms.TextBox();
            this.txtAEPMin = new System.Windows.Forms.TextBox();
            this.txtAEPSD = new System.Windows.Forms.TextBox();
            this.txtAEPAvg = new System.Windows.Forms.TextBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label34 = new System.Windows.Forms.Label();
            this.chkTurbGrossAll = new System.Windows.Forms.CheckBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.chkTurbGross = new System.Windows.Forms.CheckedListBox();
            this.btnDelPowerCrv = new System.Windows.Forms.Button();
            this.btnImportCRV = new System.Windows.Forms.Button();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.txtStDev = new System.Windows.Forms.TextBox();
            this.txtAvg = new System.Windows.Forms.TextBox();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblStdev = new System.Windows.Forms.Label();
            this.lblAvg = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblTurbEsts = new System.Windows.Forms.Label();
            this.cboPowerCrvs = new System.Windows.Forms.ComboBox();
            this.lstGrossTurbEst = new System.Windows.Forms.ListView();
            this.ColumnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Header25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader32 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pgeExceedance = new System.Windows.Forms.TabPage();
            this.plotCompositeExceed = new OxyPlot.WindowsForms.PlotView();
            this.plotExceedCurves = new OxyPlot.WindowsForms.PlotView();
            this.btnImportCurves = new System.Windows.Forms.Button();
            this.btnGetDefaultExceed = new System.Windows.Forms.Button();
            this.cboExceedWake = new System.Windows.Forms.ComboBox();
            this.label185 = new System.Windows.Forms.Label();
            this.label183 = new System.Windows.Forms.Label();
            this.cboExceedTurbine = new System.Windows.Forms.ComboBox();
            this.btn_AddProj = new System.Windows.Forms.Button();
            this.btnExportAllPVals = new System.Windows.Forms.Button();
            this.btnDeleteExceed = new System.Windows.Forms.Button();
            this.btnExportCurves = new System.Windows.Forms.Button();
            this.btnExport_P_Vals = new System.Windows.Forms.Button();
            this.label180 = new System.Windows.Forms.Label();
            this.lstPvals = new System.Windows.Forms.ListView();
            this.columnHeader160 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader161 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader162 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader163 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader164 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader165 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader166 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkShowCDFs = new System.Windows.Forms.CheckBox();
            this.chkShowPDF = new System.Windows.Forms.CheckBox();
            this.btnDoMonteCarlo = new System.Windows.Forms.Button();
            this.label181 = new System.Windows.Forms.Label();
            this.label182 = new System.Windows.Forms.Label();
            this.lstDefinedLosses = new System.Windows.Forms.ListView();
            this.columnHeader167 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader168 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader169 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader170 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader171 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader172 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader173 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader174 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_editloss = new System.Windows.Forms.Button();
            this.pgeNetEsts = new System.Windows.Forms.TabPage();
            this.plotWakeMap = new OxyPlot.WindowsForms.PlotView();
            this.plotTurbsByString = new OxyPlot.WindowsForms.PlotView();
            this.plotWakedDists = new OxyPlot.WindowsForms.PlotView();
            this.txtisMCPdNet = new System.Windows.Forms.TextBox();
            this.txtOtherLosses = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.txtNet_FlowSep_Used = new System.Windows.Forms.TextBox();
            this.btnExportNetDirWSDists = new System.Windows.Forms.Button();
            this.btnExportNetDirWS = new System.Windows.Forms.Button();
            this.btnExportNetWSDists = new System.Windows.Forms.Button();
            this.btnRefreshWakeMap = new System.Windows.Forms.Button();
            this.chkWakeAuto = new System.Windows.Forms.CheckBox();
            this.txtWakeInterval = new System.Windows.Forms.TextBox();
            this.txtWakeMax = new System.Windows.Forms.TextBox();
            this.txtWakeMin = new System.Windows.Forms.TextBox();
            this.Label97 = new System.Windows.Forms.Label();
            this.Label98 = new System.Windows.Forms.Label();
            this.Label99 = new System.Windows.Forms.Label();
            this.txtLC_Net = new System.Windows.Forms.TextBox();
            this.btnCreateWakeMap = new System.Windows.Forms.Button();
            this.txtWakeLoss = new System.Windows.Forms.TextBox();
            this.Label94 = new System.Windows.Forms.Label();
            this.chkTurbNet = new System.Windows.Forms.CheckedListBox();
            this.chkTurbNetAll = new System.Windows.Forms.CheckBox();
            this.Label87 = new System.Windows.Forms.Label();
            this.chkStrings = new System.Windows.Forms.CheckedListBox();
            this.chkStringAll = new System.Windows.Forms.CheckBox();
            this.Label86 = new System.Windows.Forms.Label();
            this.btnDelWakeModel = new System.Windows.Forms.Button();
            this.btnDelWakeGrid = new System.Windows.Forms.Button();
            this.cboWakePlot = new System.Windows.Forms.ComboBox();
            this.Label85 = new System.Windows.Forms.Label();
            this.btnExportNetEsts = new System.Windows.Forms.Button();
            this.Label55 = new System.Windows.Forms.Label();
            this.cboNetWD = new System.Windows.Forms.ComboBox();
            this.lstWakeModels = new System.Windows.Forms.ListView();
            this.ColumnHeader88 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader106 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader89 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader107 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader90 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader94 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader95 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader96 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader97 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader98 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader99 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader100 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader101 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader102 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnWakeLossCalc = new System.Windows.Forms.Button();
            this.Label53 = new System.Windows.Forms.Label();
            this.lstWakedTurbs = new System.Windows.Forms.ListView();
            this.SiteName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StringNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader53 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader60 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader61 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader113 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader62 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader63 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader77 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label36 = new System.Windows.Forms.Label();
            this.pgeSiteConditions = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.plotInflowAngle = new OxyPlot.WindowsForms.PlotView();
            this.label200 = new System.Windows.Forms.Label();
            this.txtInflowAngle = new System.Windows.Forms.TextBox();
            this.label199 = new System.Windows.Forms.Label();
            this.cboInflowReso = new System.Windows.Forms.ComboBox();
            this.label198 = new System.Windows.Forms.Label();
            this.cboInflowRadius = new System.Windows.Forms.ComboBox();
            this.btnInflowAngles = new System.Windows.Forms.Button();
            this.cboInflowTurbine = new System.Windows.Forms.ComboBox();
            this.label197 = new System.Windows.Forms.Label();
            this.label196 = new System.Windows.Forms.Label();
            this.cboInflowWD = new System.Windows.Forms.ComboBox();
            this.label171 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.plotExtremeWS = new OxyPlot.WindowsForms.PlotView();
            this.lblNoExtremeWS = new System.Windows.Forms.Label();
            this.lblExtremeWS = new System.Windows.Forms.Label();
            this.btnExtremeWS = new System.Windows.Forms.Button();
            this.label195 = new System.Windows.Forms.Label();
            this.cboExtremeWSMet = new System.Windows.Forms.ComboBox();
            this.txt1yrExtremeGust = new System.Windows.Forms.TextBox();
            this.txt1yrExtreme10min = new System.Windows.Forms.TextBox();
            this.txt50yrExtremeGust = new System.Windows.Forms.TextBox();
            this.txt50yrExtreme10min = new System.Windows.Forms.TextBox();
            this.label193 = new System.Windows.Forms.Label();
            this.label194 = new System.Windows.Forms.Label();
            this.label192 = new System.Windows.Forms.Label();
            this.label191 = new System.Windows.Forms.Label();
            this.label170 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.plotExtremeShear = new OxyPlot.WindowsForms.PlotView();
            this.dateTimeExtremeShearStart = new System.Windows.Forms.DateTimePicker();
            this.label202 = new System.Windows.Forms.Label();
            this.label203 = new System.Windows.Forms.Label();
            this.dateTimeExtremeShearEnd = new System.Windows.Forms.DateTimePicker();
            this.label201 = new System.Windows.Forms.Label();
            this.label190 = new System.Windows.Forms.Label();
            this.cboExtremeShearRange = new System.Windows.Forms.ComboBox();
            this.label189 = new System.Windows.Forms.Label();
            this.cboExtremeShearMet = new System.Windows.Forms.ComboBox();
            this.btnExportShearStats = new System.Windows.Forms.Button();
            this.lstExtremeShear = new System.Windows.Forms.ListView();
            this.columnHeader180 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader181 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader182 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader183 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader184 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label95 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plotTurbInt = new OxyPlot.WindowsForms.PlotView();
            this.btnExportTI = new System.Windows.Forms.Button();
            this.label188 = new System.Windows.Forms.Label();
            this.cboTurbPowerCurve = new System.Windows.Forms.ComboBox();
            this.dateTIStart = new System.Windows.Forms.DateTimePicker();
            this.label186 = new System.Windows.Forms.Label();
            this.label187 = new System.Windows.Forms.Label();
            this.dateTIEnd = new System.Windows.Forms.DateTimePicker();
            this.cboTurbineTI = new System.Windows.Forms.ComboBox();
            this.label179 = new System.Windows.Forms.Label();
            this.label178 = new System.Windows.Forms.Label();
            this.cboEffectiveTI_m = new System.Windows.Forms.ComboBox();
            this.cboTI_Type = new System.Windows.Forms.ComboBox();
            this.label177 = new System.Windows.Forms.Label();
            this.lstTurbulence = new System.Windows.Forms.ListView();
            this.columnHeader177 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader178 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader179 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label174 = new System.Windows.Forms.Label();
            this.cboTurbMet = new System.Windows.Forms.ComboBox();
            this.label172 = new System.Windows.Forms.Label();
            this.cboTurbWD = new System.Windows.Forms.ComboBox();
            this.label93 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.pgeMonthlyAnalysis = new System.Windows.Forms.TabPage();
            this.plotMonthlyTS = new OxyPlot.WindowsForms.PlotView();
            this.plotYearlyTS = new OxyPlot.WindowsForms.PlotView();
            this.label158 = new System.Windows.Forms.Label();
            this.cboMonthlyPowerCurve = new System.Windows.Forms.ComboBox();
            this.btnExportHourlyTurbineValues = new System.Windows.Forms.Button();
            this.btnExportMonthlyTurbineValues = new System.Windows.Forms.Button();
            this.btnExportYearlyTurbineValues = new System.Windows.Forms.Button();
            this.cboMonthlyWakeModel = new System.Windows.Forms.ComboBox();
            this.label157 = new System.Windows.Forms.Label();
            this.label148 = new System.Windows.Forms.Label();
            this.chkSelectedTurbineParam = new System.Windows.Forms.CheckedListBox();
            this.label131 = new System.Windows.Forms.Label();
            this.cboSelectedTurbine = new System.Windows.Forms.ComboBox();
            this.label126 = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.chkSelectAllTurbineYears = new System.Windows.Forms.CheckBox();
            this.chkYears_Monthly = new System.Windows.Forms.CheckedListBox();
            this.lstYearlyTurbine = new System.Windows.Forms.ListView();
            this.columnHeader118 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader119 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader131 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader122 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader127 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader128 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstMonthlyTurbine = new System.Windows.Forms.ListView();
            this.columnHeader123 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader124 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader125 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader132 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader126 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader129 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader130 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label163 = new System.Windows.Forms.Label();
            this.pgeMaps = new System.Windows.Forms.TabPage();
            this.plotGenMap = new OxyPlot.WindowsForms.PlotView();
            this.Label72 = new System.Windows.Forms.Label();
            this.cboMapWD = new System.Windows.Forms.ComboBox();
            this.txtMap_MetsUsed = new System.Windows.Forms.TextBox();
            this.Label63 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.chkAllTurbs_Maps = new System.Windows.Forms.CheckBox();
            this.chkAllMets_Maps = new System.Windows.Forms.CheckBox();
            this.Label28 = new System.Windows.Forms.Label();
            this.Label29 = new System.Windows.Forms.Label();
            this.chkTurbLabels_Maps = new System.Windows.Forms.CheckedListBox();
            this.chkMetLabels_Maps = new System.Windows.Forms.CheckedListBox();
            this.btnRefreshMap = new System.Windows.Forms.Button();
            this.chkAutoMinMax = new System.Windows.Forms.CheckBox();
            this.txtIntLevel = new System.Windows.Forms.TextBox();
            this.txtMaxValue = new System.Windows.Forms.TextBox();
            this.txtMinValue = new System.Windows.Forms.TextBox();
            this.txtMapMax = new System.Windows.Forms.TextBox();
            this.txtMapMin = new System.Windows.Forms.TextBox();
            this.txtMapCount = new System.Windows.Forms.TextBox();
            this.txtMapStDev = new System.Windows.Forms.TextBox();
            this.txtMapAvg = new System.Windows.Forms.TextBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.lblMaxVal = new System.Windows.Forms.Label();
            this.lblMinVal = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.btnDelMaps = new System.Windows.Forms.Button();
            this.btnMapExportCSV = new System.Windows.Forms.Button();
            this.btnExportWRG = new System.Windows.Forms.Button();
            this.btnGenMap = new System.Windows.Forms.Button();
            this.lstMaps = new System.Windows.Forms.ListView();
            this.ColumnHeader54 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader55 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader56 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader57 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader58 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader59 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader36 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader185 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pgeRound = new System.Windows.Forms.TabPage();
            this.plotTurbUncert = new OxyPlot.WindowsForms.PlotView();
            this.plotRR_Histo = new OxyPlot.WindowsForms.PlotView();
            this.plotRRErrorByNumMets = new OxyPlot.WindowsForms.PlotView();
            this.txtisMCPdUncert = new System.Windows.Forms.TextBox();
            this.txtRR_FlowSep_Used = new System.Windows.Forms.TextBox();
            this.cboRR_MinSize = new System.Windows.Forms.ComboBox();
            this.Label31 = new System.Windows.Forms.Label();
            this.txtRR_LC_used = new System.Windows.Forms.TextBox();
            this.Label81 = new System.Windows.Forms.Label();
            this.btnExportTurbUncert = new System.Windows.Forms.Button();
            this.chkP50 = new System.Windows.Forms.CheckBox();
            this.chkP90 = new System.Windows.Forms.CheckBox();
            this.chkP99 = new System.Windows.Forms.CheckBox();
            this.cboUncert_WS_AEP = new System.Windows.Forms.ComboBox();
            this.Label47 = new System.Windows.Forms.Label();
            this.Label45 = new System.Windows.Forms.Label();
            this.cboUncertPowerCrv = new System.Windows.Forms.ComboBox();
            this.Label42 = new System.Windows.Forms.Label();
            this.lstTurbUncert = new System.Windows.Forms.ListView();
            this.ColumnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label41 = new System.Windows.Forms.Label();
            this.btnExportRR = new System.Windows.Forms.Button();
            this.lstRR_AllErr = new System.Windows.Forms.ListView();
            this.ColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cboRoundRobin = new System.Windows.Forms.ComboBox();
            this.lstRR_Results = new System.Windows.Forms.ListView();
            this.ColumnHeader38 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader39 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader45 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDoRRCalcs = new System.Windows.Forms.Button();
            this.Label46 = new System.Windows.Forms.Label();
            this.pgeStepwise = new System.Windows.Forms.TabPage();
            this.plotDHModel = new OxyPlot.WindowsForms.PlotView();
            this.plotUHModel = new OxyPlot.WindowsForms.PlotView();
            this.plotPathAlongNodes = new OxyPlot.WindowsForms.PlotView();
            this.plotAdvTopo = new OxyPlot.WindowsForms.PlotView();
            this.lblTurbineTSNoAdvanced = new System.Windows.Forms.Label();
            this.txtisMCPdAdv = new System.Windows.Forms.TextBox();
            this.cboSeasonAdvanced = new System.Windows.Forms.ComboBox();
            this.cboTODAdvanced = new System.Windows.Forms.ComboBox();
            this.Label39 = new System.Windows.Forms.Label();
            this.lstPathNodes_DW = new System.Windows.Forms.ListView();
            this.ColumnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader110 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader108 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader109 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader112 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label8 = new System.Windows.Forms.Label();
            this.lstPathNodes_UW = new System.Windows.Forms.ListView();
            this.ColumnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader121 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader115 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader120 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader111 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtAdv_FlowSep_Used = new System.Windows.Forms.TextBox();
            this.Label101 = new System.Windows.Forms.Label();
            this.txtSepCritWS = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.txtSepCrit = new System.Windows.Forms.TextBox();
            this.Label100 = new System.Windows.Forms.Label();
            this.cboDHplot = new System.Windows.Forms.ComboBox();
            this.btnImportModel = new System.Windows.Forms.Button();
            this.chkAdvToShow = new System.Windows.Forms.CheckedListBox();
            this.chkWeight_RMS = new System.Windows.Forms.CheckBox();
            this.txtSectRMS = new System.Windows.Forms.TextBox();
            this.Label48 = new System.Windows.Forms.Label();
            this.txtAdv_LC_used = new System.Windows.Forms.TextBox();
            this.cboExpo_or_Stab = new System.Windows.Forms.ComboBox();
            this.Label82 = new System.Windows.Forms.Label();
            this.btnExportModel = new System.Windows.Forms.Button();
            this.Label59 = new System.Windows.Forms.Label();
            this.txtUWCrit = new System.Windows.Forms.TextBox();
            this.Label58 = new System.Windows.Forms.Label();
            this.cboUphill_to_show = new System.Windows.Forms.ComboBox();
            this.cboAdvancedWD = new System.Windows.Forms.ComboBox();
            this.Label56 = new System.Windows.Forms.Label();
            this.btnExportCrossPreds = new System.Windows.Forms.Button();
            this.Label14 = new System.Windows.Forms.Label();
            this.cboAdvancedRad = new System.Windows.Forms.ComboBox();
            this.txtUWDWRMS = new System.Windows.Forms.TextBox();
            this.Label44 = new System.Windows.Forms.Label();
            this.chkAllTurbsStep = new System.Windows.Forms.CheckBox();
            this.Label38 = new System.Windows.Forms.Label();
            this.chkTurbLabelStep = new System.Windows.Forms.CheckedListBox();
            this.chkAllMetLabelsStep = new System.Windows.Forms.CheckBox();
            this.Label37 = new System.Windows.Forms.Label();
            this.chkMetlabelsStep = new System.Windows.Forms.CheckedListBox();
            this.Label33 = new System.Windows.Forms.Label();
            this.cboStartMet = new System.Windows.Forms.ComboBox();
            this.Label32 = new System.Windows.Forms.Label();
            this.cboEndMet = new System.Windows.Forms.ComboBox();
            this.lstPathNodes = new System.Windows.Forms.ListView();
            this.Site = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UTMX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UTMY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.elev = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.P10UW = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.P10DW = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UW = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DW = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WSEst = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UW_SR = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DW_SR = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UW_DH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DW_DH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnExportStepwise = new System.Windows.Forms.Button();
            this.lstModCrossPred = new System.Windows.Forms.ListView();
            this.ColumnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader30 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader31 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label11 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.pgeSuitability = new System.Windows.Forms.TabPage();
            this.plotIceVsDist = new OxyPlot.WindowsForms.PlotView();
            this.plotIceShadowSound = new OxyPlot.WindowsForms.PlotView();
            this.lblIceDistOrHisto = new System.Windows.Forms.Label();
            this.cboIceDistORIceHisto = new System.Windows.Forms.ComboBox();
            this.label91 = new System.Windows.Forms.Label();
            this.txtNumIceThrowsPerDay = new System.Windows.Forms.TextBox();
            this.txtMaxFlickerHours = new System.Windows.Forms.TextBox();
            this.dateMaxFlicker = new System.Windows.Forms.DateTimePicker();
            this.lblMaxFlickerHours = new System.Windows.Forms.Label();
            this.lblMaxFlickerDay = new System.Windows.Forms.Label();
            this.txtTurbineNoise = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.txtNumIceDays = new System.Windows.Forms.TextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.btnExportIceVsDist = new System.Windows.Forms.Button();
            this.btnExportSoundSummary = new System.Windows.Forms.Button();
            this.btnExportShadowFlicker = new System.Windows.Forms.Button();
            this.btnExportIceSummary = new System.Windows.Forms.Button();
            this.label176 = new System.Windows.Forms.Label();
            this.lstZoneSound = new System.Windows.Forms.ListView();
            this.columnHeader158 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader159 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label175 = new System.Windows.Forms.Label();
            this.lblShadowByMonthOrIceByDist = new System.Windows.Forms.Label();
            this.lstZoneIceHits = new System.Windows.Forms.ListView();
            this.columnHeader154 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader155 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader156 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader157 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader175 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader176 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label173 = new System.Windows.Forms.Label();
            this.cboIcingYear = new System.Windows.Forms.ComboBox();
            this.lstShadowZoneSummary = new System.Windows.Forms.ListView();
            this.columnHeader152 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader153 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtTotalShadow = new System.Windows.Forms.TextBox();
            this.lblTotalHoursPerYear = new System.Windows.Forms.Label();
            this.lblShadowOrIceByDist = new System.Windows.Forms.Label();
            this.lblZoneOrDirection = new System.Windows.Forms.Label();
            this.cboZoneList = new System.Windows.Forms.ComboBox();
            this.lstShadow12x24 = new System.Windows.Forms.ListView();
            this.columnHeader138 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader139 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader140 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader151 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader141 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader142 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader143 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader144 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader145 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader146 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader147 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader148 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader149 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader150 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label169 = new System.Windows.Forms.Label();
            this.cboSiteSuitHour = new System.Windows.Forms.ComboBox();
            this.label165 = new System.Windows.Forms.Label();
            this.cboSiteSuitMonth = new System.Windows.Forms.ComboBox();
            this.btnSiteSuitImportCRV = new System.Windows.Forms.Button();
            this.btnDelZones = new System.Windows.Forms.Button();
            this.label164 = new System.Windows.Forms.Label();
            this.cboSiteSuitabilitySelectPlot = new System.Windows.Forms.ComboBox();
            this.lstZones = new System.Windows.Forms.ListView();
            this.columnHeader133 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader134 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader135 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader136 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader137 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRunSoundModel = new System.Windows.Forms.Button();
            this.btnRunShadowFlicker = new System.Windows.Forms.Button();
            this.btnRunIceThrow = new System.Windows.Forms.Button();
            this.label167 = new System.Windows.Forms.Label();
            this.label166 = new System.Windows.Forms.Label();
            this.cboSiteSuitPowerCurve = new System.Windows.Forms.ComboBox();
            this.btnImportZones = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutContinuumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateHeadersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdXYZfile = new System.Windows.Forms.OpenFileDialog();
            this.ofdPowerCurve = new System.Windows.Forms.OpenFileDialog();
            this.sfdrsf = new System.Windows.Forms.SaveFileDialog();
            this.sfdWRG = new System.Windows.Forms.SaveFileDialog();
            this.ofdCFMfile = new System.Windows.Forms.OpenFileDialog();
            this.ofdLandCover = new System.Windows.Forms.OpenFileDialog();
            this.sfdCFMfile = new System.Windows.Forms.SaveFileDialog();
            this.ofdLC_Key = new System.Windows.Forms.OpenFileDialog();
            this.ofdMets = new System.Windows.Forms.OpenFileDialog();
            this.ofdTurbines = new System.Windows.Forms.OpenFileDialog();
            this.ofdImportMap = new System.Windows.Forms.OpenFileDialog();
            this.ofdImportCoeffs = new System.Windows.Forms.OpenFileDialog();
            this.sfd60mWS = new System.Windows.Forms.SaveFileDialog();
            this.sfdExpos = new System.Windows.Forms.SaveFileDialog();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ofdMetData = new System.Windows.Forms.OpenFileDialog();
            this.sfdEstimateWS = new System.Windows.Forms.SaveFileDialog();
            this.sfdSaveTAB = new System.Windows.Forms.SaveFileDialog();
            this.fbd_MERRAData = new System.Windows.Forms.FolderBrowserDialog();
            this.fbd_Export = new System.Windows.Forms.FolderBrowserDialog();
            this.ofdZones = new System.Windows.Forms.OpenFileDialog();
            this.ofdExceedCurves = new System.Windows.Forms.OpenFileDialog();
            this.btnResetMaxRecovDates = new System.Windows.Forms.Button();
            this.tabContinuum.SuspendLayout();
            this.pgeInput.SuspendLayout();
            this.pgeMetData.SuspendLayout();
            this.pgeMERRA.SuspendLayout();
            this.pgeMCP.SuspendLayout();
            this.pgeMetSumm.SuspendLayout();
            this.pgeGrossTurbs.SuspendLayout();
            this.pgeExceedance.SuspendLayout();
            this.pgeNetEsts.SuspendLayout();
            this.pgeSiteConditions.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pgeMonthlyAnalysis.SuspendLayout();
            this.pgeMaps.SuspendLayout();
            this.pgeRound.SuspendLayout();
            this.pgeStepwise.SuspendLayout();
            this.pgeSuitability.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabContinuum
            // 
            this.tabContinuum.CausesValidation = false;
            this.tabContinuum.Controls.Add(this.pgeInput);
            this.tabContinuum.Controls.Add(this.pgeMetData);
            this.tabContinuum.Controls.Add(this.pgeMERRA);
            this.tabContinuum.Controls.Add(this.pgeMCP);
            this.tabContinuum.Controls.Add(this.pgeMetSumm);
            this.tabContinuum.Controls.Add(this.pgeGrossTurbs);
            this.tabContinuum.Controls.Add(this.pgeExceedance);
            this.tabContinuum.Controls.Add(this.pgeNetEsts);
            this.tabContinuum.Controls.Add(this.pgeSiteConditions);
            this.tabContinuum.Controls.Add(this.pgeMonthlyAnalysis);
            this.tabContinuum.Controls.Add(this.pgeMaps);
            this.tabContinuum.Controls.Add(this.pgeRound);
            this.tabContinuum.Controls.Add(this.pgeStepwise);
            this.tabContinuum.Controls.Add(this.pgeSuitability);
            this.tabContinuum.Font = new System.Drawing.Font("Palatino Linotype", 10F);
            this.tabContinuum.Location = new System.Drawing.Point(12, 29);
            this.tabContinuum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabContinuum.Name = "tabContinuum";
            this.tabContinuum.SelectedIndex = 0;
            this.tabContinuum.Size = new System.Drawing.Size(1635, 880);
            this.tabContinuum.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabContinuum.TabIndex = 3;
            // 
            // pgeInput
            // 
            this.pgeInput.Controls.Add(this.plotDirectionalWS_Ratios);
            this.pgeInput.Controls.Add(this.plotInputWindRose);
            this.pgeInput.Controls.Add(this.plotTopo);
            this.pgeInput.Controls.Add(this.chkCreateTurbTS);
            this.pgeInput.Controls.Add(this.btnModHeight);
            this.pgeInput.Controls.Add(this.label168);
            this.pgeInput.Controls.Add(this.txtModeledHeight);
            this.pgeInput.Controls.Add(this.btnImportMetTS);
            this.pgeInput.Controls.Add(this.btnImportRoughness);
            this.pgeInput.Controls.Add(this.chk_Use_Sep);
            this.pgeInput.Controls.Add(this.Label84);
            this.pgeInput.Controls.Add(this.txt_LC_Key_selected);
            this.pgeInput.Controls.Add(this.chkUseSR);
            this.pgeInput.Controls.Add(this.btnViewModNLCD);
            this.pgeInput.Controls.Add(this.cboTopo_Or_Roughness);
            this.pgeInput.Controls.Add(this.txtUTMZone);
            this.pgeInput.Controls.Add(this.Label57);
            this.pgeInput.Controls.Add(this.txtUTMDatum);
            this.pgeInput.Controls.Add(this.Label49);
            this.pgeInput.Controls.Add(this.btnAnalyzeMets);
            this.pgeInput.Controls.Add(this.Label6);
            this.pgeInput.Controls.Add(this.btnImportTAB);
            this.pgeInput.Controls.Add(this.btnGenTurbEsts);
            this.pgeInput.Controls.Add(this.Label30);
            this.pgeInput.Controls.Add(this.Label19);
            this.pgeInput.Controls.Add(this.chkAllTurbLabels);
            this.pgeInput.Controls.Add(this.chkAllMetLabels);
            this.pgeInput.Controls.Add(this.Label23);
            this.pgeInput.Controls.Add(this.lblMetLabels);
            this.pgeInput.Controls.Add(this.chkTurbLabels);
            this.pgeInput.Controls.Add(this.chkMetLabels);
            this.pgeInput.Controls.Add(this.txtMainMax);
            this.pgeInput.Controls.Add(this.txtMainMin);
            this.pgeInput.Controls.Add(this.Label21);
            this.pgeInput.Controls.Add(this.Label22);
            this.pgeInput.Controls.Add(this.btnDelTurb);
            this.pgeInput.Controls.Add(this.btnEditTurb);
            this.pgeInput.Controls.Add(this.btnAddTurb);
            this.pgeInput.Controls.Add(this.lstTurbines);
            this.pgeInput.Controls.Add(this.txtTopoSource);
            this.pgeInput.Controls.Add(this.lblTurbineList);
            this.pgeInput.Controls.Add(this.btnDelMet);
            this.pgeInput.Controls.Add(this.lstMetTowers);
            this.pgeInput.Controls.Add(this.lblMetTable);
            this.pgeInput.Controls.Add(this.btnTurbines);
            this.pgeInput.Controls.Add(this.btnLoadXYZ);
            this.pgeInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgeInput.Location = new System.Drawing.Point(4, 27);
            this.pgeInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeInput.Name = "pgeInput";
            this.pgeInput.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeInput.Size = new System.Drawing.Size(1627, 849);
            this.pgeInput.TabIndex = 0;
            this.pgeInput.Text = "Input";
            this.pgeInput.UseVisualStyleBackColor = true;
            // 
            // plotDirectionalWS_Ratios
            // 
            this.plotDirectionalWS_Ratios.Location = new System.Drawing.Point(405, 505);
            this.plotDirectionalWS_Ratios.Name = "plotDirectionalWS_Ratios";
            this.plotDirectionalWS_Ratios.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDirectionalWS_Ratios.Size = new System.Drawing.Size(376, 327);
            this.plotDirectionalWS_Ratios.TabIndex = 128;
            this.plotDirectionalWS_Ratios.Text = "plotView2";
            this.plotDirectionalWS_Ratios.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDirectionalWS_Ratios.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDirectionalWS_Ratios.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotInputWindRose
            // 
            this.plotInputWindRose.Location = new System.Drawing.Point(17, 505);
            this.plotInputWindRose.Name = "plotInputWindRose";
            this.plotInputWindRose.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotInputWindRose.Size = new System.Drawing.Size(376, 327);
            this.plotInputWindRose.TabIndex = 127;
            this.plotInputWindRose.Text = "plotView1";
            this.plotInputWindRose.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotInputWindRose.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotInputWindRose.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotTopo
            // 
            this.plotTopo.Location = new System.Drawing.Point(824, 26);
            this.plotTopo.Name = "plotTopo";
            this.plotTopo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTopo.Size = new System.Drawing.Size(780, 608);
            this.plotTopo.TabIndex = 126;
            this.plotTopo.Text = "plotTopo";
            this.plotTopo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTopo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTopo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // chkCreateTurbTS
            // 
            this.chkCreateTurbTS.AutoSize = true;
            this.chkCreateTurbTS.Checked = true;
            this.chkCreateTurbTS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateTurbTS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCreateTurbTS.Location = new System.Drawing.Point(622, 198);
            this.chkCreateTurbTS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkCreateTurbTS.Name = "chkCreateTurbTS";
            this.chkCreateTurbTS.Size = new System.Drawing.Size(137, 22);
            this.chkCreateTurbTS.TabIndex = 125;
            this.chkCreateTurbTS.Text = "Create Time Series";
            this.chkCreateTurbTS.UseVisualStyleBackColor = true;
            this.chkCreateTurbTS.CheckedChanged += new System.EventHandler(this.cboCreateTurbTS_CheckedChanged);
            // 
            // btnModHeight
            // 
            this.btnModHeight.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModHeight.Location = new System.Drawing.Point(493, 103);
            this.btnModHeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnModHeight.Name = "btnModHeight";
            this.btnModHeight.Size = new System.Drawing.Size(50, 32);
            this.btnModHeight.TabIndex = 124;
            this.btnModHeight.Text = "Edit";
            this.btnModHeight.UseVisualStyleBackColor = true;
            this.btnModHeight.Click += new System.EventHandler(this.btnModHeight_Click);
            // 
            // label168
            // 
            this.label168.AutoSize = true;
            this.label168.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label168.Location = new System.Drawing.Point(488, 26);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(62, 36);
            this.label168.TabIndex = 123;
            this.label168.Text = "Modeled \r\nHeight:";
            this.label168.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtModeledHeight
            // 
            this.txtModeledHeight.Enabled = false;
            this.txtModeledHeight.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModeledHeight.Location = new System.Drawing.Point(493, 71);
            this.txtModeledHeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtModeledHeight.Name = "txtModeledHeight";
            this.txtModeledHeight.Size = new System.Drawing.Size(49, 25);
            this.txtModeledHeight.TabIndex = 122;
            this.txtModeledHeight.Text = "80";
            this.txtModeledHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnImportMetTS
            // 
            this.btnImportMetTS.BackColor = System.Drawing.Color.LightCoral;
            this.btnImportMetTS.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.btnImportMetTS.Location = new System.Drawing.Point(350, 66);
            this.btnImportMetTS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportMetTS.Name = "btnImportMetTS";
            this.btnImportMetTS.Size = new System.Drawing.Size(127, 32);
            this.btnImportMetTS.TabIndex = 121;
            this.btnImportMetTS.Text = "Import Met TS file(s)";
            this.btnImportMetTS.UseVisualStyleBackColor = false;
            this.btnImportMetTS.Click += new System.EventHandler(this.btnMetData_Click);
            // 
            // btnImportRoughness
            // 
            this.btnImportRoughness.BackColor = System.Drawing.Color.LightCoral;
            this.btnImportRoughness.Font = new System.Drawing.Font("Palatino Linotype", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportRoughness.Location = new System.Drawing.Point(17, 114);
            this.btnImportRoughness.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportRoughness.Name = "btnImportRoughness";
            this.btnImportRoughness.Size = new System.Drawing.Size(150, 50);
            this.btnImportRoughness.TabIndex = 117;
            this.btnImportRoughness.Text = "Import Land Cover / Roughness data";
            this.btnImportRoughness.UseVisualStyleBackColor = false;
            this.btnImportRoughness.Click += new System.EventHandler(this.btnImportRoughness_Click);
            // 
            // chk_Use_Sep
            // 
            this.chk_Use_Sep.AutoSize = true;
            this.chk_Use_Sep.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Use_Sep.Location = new System.Drawing.Point(330, 190);
            this.chk_Use_Sep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_Use_Sep.Name = "chk_Use_Sep";
            this.chk_Use_Sep.Size = new System.Drawing.Size(196, 21);
            this.chk_Use_Sep.TabIndex = 116;
            this.chk_Use_Sep.Text = "Enable Flow Separation Model";
            this.chk_Use_Sep.UseVisualStyleBackColor = true;
            this.chk_Use_Sep.CheckedChanged += new System.EventHandler(this.chk_Use_Sep_CheckedChanged);
            // 
            // Label84
            // 
            this.Label84.AutoSize = true;
            this.Label84.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label84.Location = new System.Drawing.Point(171, 110);
            this.Label84.Name = "Label84";
            this.Label84.Size = new System.Drawing.Size(152, 17);
            this.Label84.TabIndex = 114;
            this.Label84.Text = "Land Cover Key selected:";
            // 
            // txt_LC_Key_selected
            // 
            this.txt_LC_Key_selected.BackColor = System.Drawing.Color.LightCoral;
            this.txt_LC_Key_selected.Font = new System.Drawing.Font("Palatino Linotype", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LC_Key_selected.Location = new System.Drawing.Point(177, 132);
            this.txt_LC_Key_selected.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_LC_Key_selected.Name = "txt_LC_Key_selected";
            this.txt_LC_Key_selected.ReadOnly = true;
            this.txt_LC_Key_selected.Size = new System.Drawing.Size(140, 25);
            this.txt_LC_Key_selected.TabIndex = 113;
            this.txt_LC_Key_selected.Text = "Not Selected";
            // 
            // chkUseSR
            // 
            this.chkUseSR.AutoSize = true;
            this.chkUseSR.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseSR.Location = new System.Drawing.Point(330, 167);
            this.chkUseSR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkUseSR.Name = "chkUseSR";
            this.chkUseSR.Size = new System.Drawing.Size(168, 21);
            this.chkUseSR.TabIndex = 112;
            this.chkUseSR.Text = "Enable Roughness Model";
            this.chkUseSR.UseVisualStyleBackColor = true;
            this.chkUseSR.CheckedChanged += new System.EventHandler(this.chkUseSR_CheckedChanged);
            // 
            // btnViewModNLCD
            // 
            this.btnViewModNLCD.BackColor = System.Drawing.Color.LightCoral;
            this.btnViewModNLCD.Font = new System.Drawing.Font("Palatino Linotype", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewModNLCD.Location = new System.Drawing.Point(177, 50);
            this.btnViewModNLCD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnViewModNLCD.Name = "btnViewModNLCD";
            this.btnViewModNLCD.Size = new System.Drawing.Size(139, 50);
            this.btnViewModNLCD.TabIndex = 111;
            this.btnViewModNLCD.Text = "Select/View/Modify Land Cover Key";
            this.btnViewModNLCD.UseVisualStyleBackColor = false;
            this.btnViewModNLCD.Click += new System.EventHandler(this.btnViewModNLCD_Click);
            // 
            // cboTopo_Or_Roughness
            // 
            this.cboTopo_Or_Roughness.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTopo_Or_Roughness.FormattingEnabled = true;
            this.cboTopo_Or_Roughness.Items.AddRange(new object[] {
            "Topography",
            "Land Cover",
            "Surface Roughness",
            "Displacement height"});
            this.cboTopo_Or_Roughness.Location = new System.Drawing.Point(1444, 650);
            this.cboTopo_Or_Roughness.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTopo_Or_Roughness.Name = "cboTopo_Or_Roughness";
            this.cboTopo_Or_Roughness.Size = new System.Drawing.Size(147, 26);
            this.cboTopo_Or_Roughness.TabIndex = 107;
            this.cboTopo_Or_Roughness.Text = "Topography";
            this.cboTopo_Or_Roughness.SelectedIndexChanged += new System.EventHandler(this.cboTopo_Or_Roughness_SelectedIndexChanged);
            // 
            // txtUTMZone
            // 
            this.txtUTMZone.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUTMZone.Location = new System.Drawing.Point(355, 216);
            this.txtUTMZone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUTMZone.Name = "txtUTMZone";
            this.txtUTMZone.ReadOnly = true;
            this.txtUTMZone.Size = new System.Drawing.Size(52, 25);
            this.txtUTMZone.TabIndex = 105;
            // 
            // Label57
            // 
            this.Label57.AutoSize = true;
            this.Label57.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label57.Location = new System.Drawing.Point(271, 221);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(74, 18);
            this.Label57.TabIndex = 104;
            this.Label57.Text = "UTM Zone:";
            // 
            // txtUTMDatum
            // 
            this.txtUTMDatum.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUTMDatum.Location = new System.Drawing.Point(107, 217);
            this.txtUTMDatum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUTMDatum.Name = "txtUTMDatum";
            this.txtUTMDatum.ReadOnly = true;
            this.txtUTMDatum.Size = new System.Drawing.Size(147, 25);
            this.txtUTMDatum.TabIndex = 103;
            // 
            // Label49
            // 
            this.Label49.AutoSize = true;
            this.Label49.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label49.Location = new System.Drawing.Point(14, 222);
            this.Label49.Name = "Label49";
            this.Label49.Size = new System.Drawing.Size(85, 18);
            this.Label49.TabIndex = 102;
            this.Label49.Text = "UTM Datum:";
            // 
            // btnAnalyzeMets
            // 
            this.btnAnalyzeMets.BackColor = System.Drawing.Color.LightCoral;
            this.btnAnalyzeMets.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnAnalyzeMets.Location = new System.Drawing.Point(350, 98);
            this.btnAnalyzeMets.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAnalyzeMets.Name = "btnAnalyzeMets";
            this.btnAnalyzeMets.Size = new System.Drawing.Size(127, 34);
            this.btnAnalyzeMets.TabIndex = 101;
            this.btnAnalyzeMets.Text = "Analyze Met(s)";
            this.btnAnalyzeMets.UseVisualStyleBackColor = false;
            this.btnAnalyzeMets.Click += new System.EventHandler(this.btnAnalyzeMets_Click);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(10, 15);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(150, 26);
            this.Label6.TabIndex = 90;
            this.Label6.Text = "Model Inputs";
            // 
            // btnImportTAB
            // 
            this.btnImportTAB.BackColor = System.Drawing.Color.LightCoral;
            this.btnImportTAB.Font = new System.Drawing.Font("Palatino Linotype", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportTAB.Location = new System.Drawing.Point(350, 33);
            this.btnImportTAB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportTAB.Name = "btnImportTAB";
            this.btnImportTAB.Size = new System.Drawing.Size(127, 36);
            this.btnImportTAB.TabIndex = 82;
            this.btnImportTAB.Text = "Import TAB file(s)";
            this.btnImportTAB.UseVisualStyleBackColor = false;
            this.btnImportTAB.Click += new System.EventHandler(this.btnImportTAB_Click);
            // 
            // btnGenTurbEsts
            // 
            this.btnGenTurbEsts.BackColor = System.Drawing.Color.LightCoral;
            this.btnGenTurbEsts.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnGenTurbEsts.Location = new System.Drawing.Point(568, 142);
            this.btnGenTurbEsts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGenTurbEsts.Name = "btnGenTurbEsts";
            this.btnGenTurbEsts.Size = new System.Drawing.Size(178, 49);
            this.btnGenTurbEsts.TabIndex = 80;
            this.btnGenTurbEsts.Text = "Generate Wind Speed Estimates at Turbine Sites";
            this.btnGenTurbEsts.UseVisualStyleBackColor = false;
            this.btnGenTurbEsts.Click += new System.EventHandler(this.btnGenTurbEsts_Click);
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label30.Location = new System.Drawing.Point(369, 5);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(91, 23);
            this.Label30.TabIndex = 78;
            this.Label30.Text = "Met Sites";
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(597, 15);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(122, 23);
            this.Label19.TabIndex = 77;
            this.Label19.Text = "Turbine Sites";
            // 
            // chkAllTurbLabels
            // 
            this.chkAllTurbLabels.AutoSize = true;
            this.chkAllTurbLabels.Checked = true;
            this.chkAllTurbLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllTurbLabels.Location = new System.Drawing.Point(1260, 656);
            this.chkAllTurbLabels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAllTurbLabels.Name = "chkAllTurbLabels";
            this.chkAllTurbLabels.Size = new System.Drawing.Size(117, 17);
            this.chkAllTurbLabels.TabIndex = 68;
            this.chkAllTurbLabels.Text = "Select/Deselect All";
            this.chkAllTurbLabels.UseVisualStyleBackColor = true;
            this.chkAllTurbLabels.CheckedChanged += new System.EventHandler(this.chkAllTurbLabels_CheckedChanged);
            // 
            // chkAllMetLabels
            // 
            this.chkAllMetLabels.AutoSize = true;
            this.chkAllMetLabels.Checked = true;
            this.chkAllMetLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllMetLabels.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllMetLabels.Location = new System.Drawing.Point(930, 656);
            this.chkAllMetLabels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAllMetLabels.Name = "chkAllMetLabels";
            this.chkAllMetLabels.Size = new System.Drawing.Size(134, 22);
            this.chkAllMetLabels.TabIndex = 67;
            this.chkAllMetLabels.Text = "Select/Deselect All";
            this.chkAllMetLabels.UseVisualStyleBackColor = true;
            this.chkAllMetLabels.CheckedChanged += new System.EventHandler(this.chkAllMetLabels_CheckedChanged);
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(1095, 651);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(168, 23);
            this.Label23.TabIndex = 66;
            this.Label23.Text = "Turbine Locations";
            // 
            // lblMetLabels
            // 
            this.lblMetLabels.AutoSize = true;
            this.lblMetLabels.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetLabels.Location = new System.Drawing.Point(820, 651);
            this.lblMetLabels.Name = "lblMetLabels";
            this.lblMetLabels.Size = new System.Drawing.Size(91, 23);
            this.lblMetLabels.TabIndex = 65;
            this.lblMetLabels.Text = "Met Sites";
            // 
            // chkTurbLabels
            // 
            this.chkTurbLabels.CheckOnClick = true;
            this.chkTurbLabels.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTurbLabels.FormattingEnabled = true;
            this.chkTurbLabels.Location = new System.Drawing.Point(1095, 681);
            this.chkTurbLabels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbLabels.Name = "chkTurbLabels";
            this.chkTurbLabels.Size = new System.Drawing.Size(268, 104);
            this.chkTurbLabels.TabIndex = 64;
            this.chkTurbLabels.SelectedIndexChanged += new System.EventHandler(this.chkTurbLabels_SelectedIndexChanged);
            // 
            // chkMetLabels
            // 
            this.chkMetLabels.CheckOnClick = true;
            this.chkMetLabels.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMetLabels.FormattingEnabled = true;
            this.chkMetLabels.Location = new System.Drawing.Point(822, 681);
            this.chkMetLabels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetLabels.Name = "chkMetLabels";
            this.chkMetLabels.Size = new System.Drawing.Size(264, 104);
            this.chkMetLabels.TabIndex = 63;
            this.chkMetLabels.SelectedIndexChanged += new System.EventHandler(this.chkMetLabels_SelectedIndexChanged);
            // 
            // txtMainMax
            // 
            this.txtMainMax.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMainMax.Location = new System.Drawing.Point(1483, 721);
            this.txtMainMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMainMax.Name = "txtMainMax";
            this.txtMainMax.Size = new System.Drawing.Size(61, 25);
            this.txtMainMax.TabIndex = 58;
            // 
            // txtMainMin
            // 
            this.txtMainMin.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMainMin.Location = new System.Drawing.Point(1483, 692);
            this.txtMainMin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMainMin.Name = "txtMainMin";
            this.txtMainMin.Size = new System.Drawing.Size(61, 25);
            this.txtMainMin.TabIndex = 56;
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label21.Location = new System.Drawing.Point(1439, 725);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(37, 18);
            this.Label21.TabIndex = 57;
            this.Label21.Text = "Max:";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label22.Location = new System.Drawing.Point(1441, 693);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(35, 18);
            this.Label22.TabIndex = 55;
            this.Label22.Text = "Min:";
            // 
            // btnDelTurb
            // 
            this.btnDelTurb.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnDelTurb.Location = new System.Drawing.Point(659, 90);
            this.btnDelTurb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelTurb.Name = "btnDelTurb";
            this.btnDelTurb.Size = new System.Drawing.Size(92, 38);
            this.btnDelTurb.TabIndex = 30;
            this.btnDelTurb.Text = "Delete";
            this.btnDelTurb.UseVisualStyleBackColor = true;
            this.btnDelTurb.Click += new System.EventHandler(this.btnDelTurb_Click);
            // 
            // btnEditTurb
            // 
            this.btnEditTurb.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnEditTurb.Location = new System.Drawing.Point(561, 90);
            this.btnEditTurb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditTurb.Name = "btnEditTurb";
            this.btnEditTurb.Size = new System.Drawing.Size(87, 38);
            this.btnEditTurb.TabIndex = 29;
            this.btnEditTurb.Text = "Edit";
            this.btnEditTurb.UseVisualStyleBackColor = true;
            this.btnEditTurb.Click += new System.EventHandler(this.btnEditTurb_Click);
            // 
            // btnAddTurb
            // 
            this.btnAddTurb.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnAddTurb.Location = new System.Drawing.Point(659, 44);
            this.btnAddTurb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddTurb.Name = "btnAddTurb";
            this.btnAddTurb.Size = new System.Drawing.Size(92, 38);
            this.btnAddTurb.TabIndex = 28;
            this.btnAddTurb.Text = "Add";
            this.btnAddTurb.UseVisualStyleBackColor = true;
            this.btnAddTurb.Click += new System.EventHandler(this.btnAddTurb_Click);
            // 
            // lstTurbines
            // 
            this.lstTurbines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader17,
            this.ColumnHeader18,
            this.ColumnHeader19});
            this.lstTurbines.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTurbines.FullRowSelect = true;
            this.lstTurbines.HideSelection = false;
            this.lstTurbines.Location = new System.Drawing.Point(489, 248);
            this.lstTurbines.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstTurbines.Name = "lstTurbines";
            this.lstTurbines.Size = new System.Drawing.Size(292, 246);
            this.lstTurbines.TabIndex = 27;
            this.lstTurbines.UseCompatibleStateImageBehavior = false;
            this.lstTurbines.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader17
            // 
            this.ColumnHeader17.Text = "Name";
            this.ColumnHeader17.Width = 79;
            // 
            // ColumnHeader18
            // 
            this.ColumnHeader18.Text = "UTMX";
            this.ColumnHeader18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnHeader18.Width = 85;
            // 
            // ColumnHeader19
            // 
            this.ColumnHeader19.Text = "UTMY";
            this.ColumnHeader19.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnHeader19.Width = 82;
            // 
            // txtTopoSource
            // 
            this.txtTopoSource.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTopoSource.Location = new System.Drawing.Point(822, 799);
            this.txtTopoSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTopoSource.Name = "txtTopoSource";
            this.txtTopoSource.Size = new System.Drawing.Size(782, 25);
            this.txtTopoSource.TabIndex = 12;
            // 
            // lblTurbineList
            // 
            this.lblTurbineList.AutoSize = true;
            this.lblTurbineList.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurbineList.Location = new System.Drawing.Point(490, 217);
            this.lblTurbineList.Name = "lblTurbineList";
            this.lblTurbineList.Size = new System.Drawing.Size(138, 23);
            this.lblTurbineList.TabIndex = 26;
            this.lblTurbineList.Text = "List of Turbines";
            // 
            // btnDelMet
            // 
            this.btnDelMet.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnDelMet.Location = new System.Drawing.Point(350, 135);
            this.btnDelMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelMet.Name = "btnDelMet";
            this.btnDelMet.Size = new System.Drawing.Size(127, 30);
            this.btnDelMet.TabIndex = 19;
            this.btnDelMet.Text = "Delete Met(s)";
            this.btnDelMet.UseVisualStyleBackColor = true;
            this.btnDelMet.Click += new System.EventHandler(this.btnDelMet_Click);
            // 
            // lstMetTowers
            // 
            this.lstMetTowers.AllowColumnReorder = true;
            this.lstMetTowers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.metName,
            this.Lats,
            this.Longs,
            this.WindSpd1});
            this.lstMetTowers.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMetTowers.FullRowSelect = true;
            this.lstMetTowers.HideSelection = false;
            this.lstMetTowers.Location = new System.Drawing.Point(17, 248);
            this.lstMetTowers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstMetTowers.Name = "lstMetTowers";
            this.lstMetTowers.Size = new System.Drawing.Size(462, 246);
            this.lstMetTowers.TabIndex = 17;
            this.lstMetTowers.UseCompatibleStateImageBehavior = false;
            this.lstMetTowers.View = System.Windows.Forms.View.Details;
            // 
            // metName
            // 
            this.metName.Text = "Met Name";
            this.metName.Width = 100;
            // 
            // Lats
            // 
            this.Lats.Text = "UTMX";
            this.Lats.Width = 79;
            // 
            // Longs
            // 
            this.Longs.Text = "UTMY";
            this.Longs.Width = 77;
            // 
            // WindSpd1
            // 
            this.WindSpd1.Text = "Wind Spd @ H1";
            this.WindSpd1.Width = 102;
            // 
            // lblMetTable
            // 
            this.lblMetTable.AutoSize = true;
            this.lblMetTable.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetTable.Location = new System.Drawing.Point(22, 188);
            this.lblMetTable.Name = "lblMetTable";
            this.lblMetTable.Size = new System.Drawing.Size(175, 23);
            this.lblMetTable.TabIndex = 7;
            this.lblMetTable.Text = "Met Site Summary";
            // 
            // btnTurbines
            // 
            this.btnTurbines.BackColor = System.Drawing.Color.LightCoral;
            this.btnTurbines.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnTurbines.Location = new System.Drawing.Point(561, 44);
            this.btnTurbines.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTurbines.Name = "btnTurbines";
            this.btnTurbines.Size = new System.Drawing.Size(87, 38);
            this.btnTurbines.TabIndex = 3;
            this.btnTurbines.Text = "Import";
            this.btnTurbines.UseVisualStyleBackColor = false;
            this.btnTurbines.Click += new System.EventHandler(this.btnTurbines_Click);
            // 
            // btnLoadXYZ
            // 
            this.btnLoadXYZ.BackColor = System.Drawing.Color.LightCoral;
            this.btnLoadXYZ.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadXYZ.Location = new System.Drawing.Point(17, 50);
            this.btnLoadXYZ.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLoadXYZ.Name = "btnLoadXYZ";
            this.btnLoadXYZ.Size = new System.Drawing.Size(150, 50);
            this.btnLoadXYZ.TabIndex = 0;
            this.btnLoadXYZ.Text = "Import Elevation data (GeoTIFF or ADF)";
            this.btnLoadXYZ.UseVisualStyleBackColor = false;
            this.btnLoadXYZ.Click += new System.EventHandler(this.btnLoadXYZ_Click);
            // 
            // pgeMetData
            // 
            this.pgeMetData.Controls.Add(this.btnResetMaxRecovDates);
            this.pgeMetData.Controls.Add(this.chkMaxWS_Range);
            this.pgeMetData.Controls.Add(this.chkMaxWS_SD);
            this.pgeMetData.Controls.Add(this.chkMinWS_SD);
            this.pgeMetData.Controls.Add(this.chkMinWS);
            this.pgeMetData.Controls.Add(this.chkIcing);
            this.pgeMetData.Controls.Add(this.chkTowerShadow);
            this.pgeMetData.Controls.Add(this.cboSelVane);
            this.pgeMetData.Controls.Add(this.label184);
            this.pgeMetData.Controls.Add(this.cboAnemB);
            this.pgeMetData.Controls.Add(this.label155);
            this.pgeMetData.Controls.Add(this.cboAnemA);
            this.pgeMetData.Controls.Add(this.label20);
            this.pgeMetData.Controls.Add(this.plotWSDiffByWS);
            this.pgeMetData.Controls.Add(this.plotWSDiffByWD);
            this.pgeMetData.Controls.Add(this.plotAnemScatter);
            this.pgeMetData.Controls.Add(this.plotAlphaByWD);
            this.pgeMetData.Controls.Add(this.plotMetQC_WindRose);
            this.pgeMetData.Controls.Add(this.plotWS_vsHeight);
            this.pgeMetData.Controls.Add(this.chkDisableFilter);
            this.pgeMetData.Controls.Add(this.btnViewFilters);
            this.pgeMetData.Controls.Add(this.Export_End);
            this.pgeMetData.Controls.Add(this.label105);
            this.pgeMetData.Controls.Add(this.Export_Start);
            this.pgeMetData.Controls.Add(this.btnExportAnnualMax);
            this.pgeMetData.Controls.Add(this.btnExportExtrap);
            this.pgeMetData.Controls.Add(this.btnExportAlpha);
            this.pgeMetData.Controls.Add(this.btnExportFlags);
            this.pgeMetData.Controls.Add(this.label104);
            this.pgeMetData.Controls.Add(this.cboMetQC_SelectedMet);
            this.pgeMetData.Controls.Add(this.cboFilt_or_Not);
            this.pgeMetData.Controls.Add(this.label102);
            this.pgeMetData.Controls.Add(this.label70);
            this.pgeMetData.Controls.Add(this.label69);
            this.pgeMetData.Controls.Add(this.lstExtrapolated);
            this.pgeMetData.Controls.Add(this.label68);
            this.pgeMetData.Controls.Add(this.label67);
            this.pgeMetData.Controls.Add(this.lstTempSummary);
            this.pgeMetData.Controls.Add(this.label66);
            this.pgeMetData.Controls.Add(this.lstVaneSummary);
            this.pgeMetData.Controls.Add(this.label65);
            this.pgeMetData.Controls.Add(this.cboMetWindRose);
            this.pgeMetData.Controls.Add(this.label64);
            this.pgeMetData.Controls.Add(this.label62);
            this.pgeMetData.Controls.Add(this.lstAlphas);
            this.pgeMetData.Controls.Add(this.label61);
            this.pgeMetData.Controls.Add(this.label60);
            this.pgeMetData.Controls.Add(this.End_Time);
            this.pgeMetData.Controls.Add(this.All_End_Time);
            this.pgeMetData.Controls.Add(this.label43);
            this.pgeMetData.Controls.Add(this.Start_Time);
            this.pgeMetData.Controls.Add(this.label26);
            this.pgeMetData.Controls.Add(this.label25);
            this.pgeMetData.Controls.Add(this.All_Start_Time);
            this.pgeMetData.Controls.Add(this.label24);
            this.pgeMetData.Controls.Add(this.label2);
            this.pgeMetData.Controls.Add(this.lstAnemSummary);
            this.pgeMetData.Location = new System.Drawing.Point(4, 27);
            this.pgeMetData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeMetData.Name = "pgeMetData";
            this.pgeMetData.Size = new System.Drawing.Size(1627, 849);
            this.pgeMetData.TabIndex = 14;
            this.pgeMetData.Text = "Met Data QC";
            this.pgeMetData.UseVisualStyleBackColor = true;
            // 
            // chkMaxWS_Range
            // 
            this.chkMaxWS_Range.AutoSize = true;
            this.chkMaxWS_Range.Checked = true;
            this.chkMaxWS_Range.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaxWS_Range.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMaxWS_Range.Location = new System.Drawing.Point(705, 431);
            this.chkMaxWS_Range.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMaxWS_Range.Name = "chkMaxWS_Range";
            this.chkMaxWS_Range.Size = new System.Drawing.Size(120, 22);
            this.chkMaxWS_Range.TabIndex = 236;
            this.chkMaxWS_Range.Text = "Max. WS Range";
            this.chkMaxWS_Range.UseVisualStyleBackColor = true;
            this.chkMaxWS_Range.CheckedChanged += new System.EventHandler(this.chkMaxWS_Range_CheckedChanged);
            // 
            // chkMaxWS_SD
            // 
            this.chkMaxWS_SD.AutoSize = true;
            this.chkMaxWS_SD.Checked = true;
            this.chkMaxWS_SD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaxWS_SD.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMaxWS_SD.Location = new System.Drawing.Point(705, 409);
            this.chkMaxWS_SD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMaxWS_SD.Name = "chkMaxWS_SD";
            this.chkMaxWS_SD.Size = new System.Drawing.Size(99, 22);
            this.chkMaxWS_SD.TabIndex = 235;
            this.chkMaxWS_SD.Text = "Max. WS SD";
            this.chkMaxWS_SD.UseVisualStyleBackColor = true;
            this.chkMaxWS_SD.CheckedChanged += new System.EventHandler(this.chkMaxWS_SD_CheckedChanged);
            // 
            // chkMinWS_SD
            // 
            this.chkMinWS_SD.AutoSize = true;
            this.chkMinWS_SD.Checked = true;
            this.chkMinWS_SD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMinWS_SD.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMinWS_SD.Location = new System.Drawing.Point(705, 388);
            this.chkMinWS_SD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMinWS_SD.Name = "chkMinWS_SD";
            this.chkMinWS_SD.Size = new System.Drawing.Size(97, 22);
            this.chkMinWS_SD.TabIndex = 234;
            this.chkMinWS_SD.Text = "Min. WS SD";
            this.chkMinWS_SD.UseVisualStyleBackColor = true;
            this.chkMinWS_SD.CheckedChanged += new System.EventHandler(this.chkMinWS_SD_CheckedChanged);
            // 
            // chkMinWS
            // 
            this.chkMinWS.AutoSize = true;
            this.chkMinWS.Checked = true;
            this.chkMinWS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMinWS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMinWS.Location = new System.Drawing.Point(705, 366);
            this.chkMinWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMinWS.Name = "chkMinWS";
            this.chkMinWS.Size = new System.Drawing.Size(78, 22);
            this.chkMinWS.TabIndex = 233;
            this.chkMinWS.Text = "Min. WS";
            this.chkMinWS.UseVisualStyleBackColor = true;
            this.chkMinWS.CheckedChanged += new System.EventHandler(this.chkMinWS_CheckedChanged);
            // 
            // chkIcing
            // 
            this.chkIcing.AutoSize = true;
            this.chkIcing.Checked = true;
            this.chkIcing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIcing.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIcing.Location = new System.Drawing.Point(705, 343);
            this.chkIcing.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkIcing.Name = "chkIcing";
            this.chkIcing.Size = new System.Drawing.Size(56, 22);
            this.chkIcing.TabIndex = 232;
            this.chkIcing.Text = "Icing";
            this.chkIcing.UseVisualStyleBackColor = true;
            this.chkIcing.CheckedChanged += new System.EventHandler(this.chkIcing_CheckedChanged);
            // 
            // chkTowerShadow
            // 
            this.chkTowerShadow.AutoSize = true;
            this.chkTowerShadow.Checked = true;
            this.chkTowerShadow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTowerShadow.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTowerShadow.Location = new System.Drawing.Point(705, 322);
            this.chkTowerShadow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTowerShadow.Name = "chkTowerShadow";
            this.chkTowerShadow.Size = new System.Drawing.Size(118, 22);
            this.chkTowerShadow.TabIndex = 231;
            this.chkTowerShadow.Text = "Tower Shadow";
            this.chkTowerShadow.UseVisualStyleBackColor = true;
            this.chkTowerShadow.CheckedChanged += new System.EventHandler(this.chkTowerShadow_CheckedChanged);
            // 
            // cboSelVane
            // 
            this.cboSelVane.FormattingEnabled = true;
            this.cboSelVane.Location = new System.Drawing.Point(509, 465);
            this.cboSelVane.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSelVane.Name = "cboSelVane";
            this.cboSelVane.Size = new System.Drawing.Size(61, 26);
            this.cboSelVane.TabIndex = 230;
            this.cboSelVane.SelectedIndexChanged += new System.EventHandler(this.cboSelVane_SelectedIndexChanged);
            // 
            // label184
            // 
            this.label184.AutoSize = true;
            this.label184.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label184.Location = new System.Drawing.Point(454, 469);
            this.label184.Name = "label184";
            this.label184.Size = new System.Drawing.Size(38, 18);
            this.label184.TabIndex = 229;
            this.label184.Text = "Vane";
            // 
            // cboAnemB
            // 
            this.cboAnemB.FormattingEnabled = true;
            this.cboAnemB.Location = new System.Drawing.Point(367, 466);
            this.cboAnemB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboAnemB.Name = "cboAnemB";
            this.cboAnemB.Size = new System.Drawing.Size(61, 26);
            this.cboAnemB.TabIndex = 228;
            this.cboAnemB.SelectedIndexChanged += new System.EventHandler(this.cboAnemB_SelectedIndexChanged);
            // 
            // label155
            // 
            this.label155.AutoSize = true;
            this.label155.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label155.Location = new System.Drawing.Point(312, 470);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(55, 18);
            this.label155.TabIndex = 227;
            this.label155.Text = "Anem B";
            // 
            // cboAnemA
            // 
            this.cboAnemA.FormattingEnabled = true;
            this.cboAnemA.Location = new System.Drawing.Point(233, 467);
            this.cboAnemA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboAnemA.Name = "cboAnemA";
            this.cboAnemA.Size = new System.Drawing.Size(61, 26);
            this.cboAnemA.TabIndex = 226;
            this.cboAnemA.SelectedIndexChanged += new System.EventHandler(this.cboAnemA_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(171, 471);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(57, 18);
            this.label20.TabIndex = 225;
            this.label20.Text = "Anem A";
            // 
            // plotWSDiffByWS
            // 
            this.plotWSDiffByWS.Location = new System.Drawing.Point(1079, 529);
            this.plotWSDiffByWS.Name = "plotWSDiffByWS";
            this.plotWSDiffByWS.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotWSDiffByWS.Size = new System.Drawing.Size(510, 313);
            this.plotWSDiffByWS.TabIndex = 224;
            this.plotWSDiffByWS.Text = "plotView2";
            this.plotWSDiffByWS.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotWSDiffByWS.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotWSDiffByWS.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotWSDiffByWD
            // 
            this.plotWSDiffByWD.Location = new System.Drawing.Point(541, 529);
            this.plotWSDiffByWD.Name = "plotWSDiffByWD";
            this.plotWSDiffByWD.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotWSDiffByWD.Size = new System.Drawing.Size(510, 313);
            this.plotWSDiffByWD.TabIndex = 223;
            this.plotWSDiffByWD.Text = "plotView1";
            this.plotWSDiffByWD.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotWSDiffByWD.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotWSDiffByWD.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotAnemScatter
            // 
            this.plotAnemScatter.Location = new System.Drawing.Point(18, 529);
            this.plotAnemScatter.Name = "plotAnemScatter";
            this.plotAnemScatter.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotAnemScatter.Size = new System.Drawing.Size(500, 313);
            this.plotAnemScatter.TabIndex = 222;
            this.plotAnemScatter.Text = "plotView1";
            this.plotAnemScatter.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotAnemScatter.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotAnemScatter.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotAlphaByWD
            // 
            this.plotAlphaByWD.Location = new System.Drawing.Point(1050, 286);
            this.plotAlphaByWD.Name = "plotAlphaByWD";
            this.plotAlphaByWD.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotAlphaByWD.Size = new System.Drawing.Size(406, 207);
            this.plotAlphaByWD.TabIndex = 221;
            this.plotAlphaByWD.Text = "plotView1";
            this.plotAlphaByWD.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotAlphaByWD.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotAlphaByWD.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotMetQC_WindRose
            // 
            this.plotMetQC_WindRose.Location = new System.Drawing.Point(1348, 39);
            this.plotMetQC_WindRose.Name = "plotMetQC_WindRose";
            this.plotMetQC_WindRose.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMetQC_WindRose.Size = new System.Drawing.Size(269, 212);
            this.plotMetQC_WindRose.TabIndex = 220;
            this.plotMetQC_WindRose.Text = "plotView1";
            this.plotMetQC_WindRose.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMetQC_WindRose.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMetQC_WindRose.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotWS_vsHeight
            // 
            this.plotWS_vsHeight.Location = new System.Drawing.Point(1050, 39);
            this.plotWS_vsHeight.Name = "plotWS_vsHeight";
            this.plotWS_vsHeight.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotWS_vsHeight.Size = new System.Drawing.Size(292, 212);
            this.plotWS_vsHeight.TabIndex = 219;
            this.plotWS_vsHeight.Text = "plotView1";
            this.plotWS_vsHeight.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotWS_vsHeight.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotWS_vsHeight.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // chkDisableFilter
            // 
            this.chkDisableFilter.AutoSize = true;
            this.chkDisableFilter.Checked = true;
            this.chkDisableFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisableFilter.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableFilter.Location = new System.Drawing.Point(717, 456);
            this.chkDisableFilter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkDisableFilter.Name = "chkDisableFilter";
            this.chkDisableFilter.Size = new System.Drawing.Size(78, 40);
            this.chkDisableFilter.TabIndex = 217;
            this.chkDisableFilter.Text = "Filtering\r\nEnabled";
            this.chkDisableFilter.UseVisualStyleBackColor = true;
            this.chkDisableFilter.CheckedChanged += new System.EventHandler(this.chkDisableFilter_CheckedChanged);
            // 
            // btnViewFilters
            // 
            this.btnViewFilters.Location = new System.Drawing.Point(717, 286);
            this.btnViewFilters.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnViewFilters.Name = "btnViewFilters";
            this.btnViewFilters.Size = new System.Drawing.Size(92, 28);
            this.btnViewFilters.TabIndex = 215;
            this.btnViewFilters.Text = "View Filters";
            this.btnViewFilters.UseVisualStyleBackColor = true;
            this.btnViewFilters.Click += new System.EventHandler(this.btnViewFilters_Click);
            // 
            // Export_End
            // 
            this.Export_End.CustomFormat = "MM/dd/yy HH:mm";
            this.Export_End.Enabled = false;
            this.Export_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Export_End.Location = new System.Drawing.Point(1465, 329);
            this.Export_End.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Export_End.Name = "Export_End";
            this.Export_End.Size = new System.Drawing.Size(140, 25);
            this.Export_End.TabIndex = 214;
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.Location = new System.Drawing.Point(1462, 309);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(77, 18);
            this.label105.TabIndex = 213;
            this.label105.Text = "Export End:";
            // 
            // Export_Start
            // 
            this.Export_Start.CustomFormat = "MM/dd/yy HH:mm";
            this.Export_Start.Enabled = false;
            this.Export_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Export_Start.Location = new System.Drawing.Point(1465, 276);
            this.Export_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Export_Start.Name = "Export_Start";
            this.Export_Start.Size = new System.Drawing.Size(140, 25);
            this.Export_Start.TabIndex = 212;
            // 
            // btnExportAnnualMax
            // 
            this.btnExportAnnualMax.Location = new System.Drawing.Point(1465, 486);
            this.btnExportAnnualMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportAnnualMax.Name = "btnExportAnnualMax";
            this.btnExportAnnualMax.Size = new System.Drawing.Size(153, 38);
            this.btnExportAnnualMax.TabIndex = 211;
            this.btnExportAnnualMax.Text = "Export Annual Max";
            this.btnExportAnnualMax.UseVisualStyleBackColor = true;
            this.btnExportAnnualMax.Click += new System.EventHandler(this.btnExportAnnualMax_Click);
            // 
            // btnExportExtrap
            // 
            this.btnExportExtrap.Location = new System.Drawing.Point(1465, 444);
            this.btnExportExtrap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportExtrap.Name = "btnExportExtrap";
            this.btnExportExtrap.Size = new System.Drawing.Size(153, 38);
            this.btnExportExtrap.TabIndex = 210;
            this.btnExportExtrap.Text = "Export Extrap. Data";
            this.btnExportExtrap.UseVisualStyleBackColor = true;
            this.btnExportExtrap.Click += new System.EventHandler(this.btnExportExtrap_Click);
            // 
            // btnExportAlpha
            // 
            this.btnExportAlpha.Location = new System.Drawing.Point(1465, 401);
            this.btnExportAlpha.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportAlpha.Name = "btnExportAlpha";
            this.btnExportAlpha.Size = new System.Drawing.Size(153, 38);
            this.btnExportAlpha.TabIndex = 209;
            this.btnExportAlpha.Text = "Export Alpha";
            this.btnExportAlpha.UseVisualStyleBackColor = true;
            this.btnExportAlpha.Click += new System.EventHandler(this.btnExportAlpha_Click);
            // 
            // btnExportFlags
            // 
            this.btnExportFlags.Location = new System.Drawing.Point(1465, 359);
            this.btnExportFlags.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportFlags.Name = "btnExportFlags";
            this.btnExportFlags.Size = new System.Drawing.Size(153, 38);
            this.btnExportFlags.TabIndex = 208;
            this.btnExportFlags.Text = "Export Flagged Data";
            this.btnExportFlags.UseVisualStyleBackColor = true;
            this.btnExportFlags.Click += new System.EventHandler(this.btnExportFlags_Click);
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.Location = new System.Drawing.Point(1462, 256);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(84, 18);
            this.label104.TabIndex = 207;
            this.label104.Text = "Export Start:";
            // 
            // cboMetQC_SelectedMet
            // 
            this.cboMetQC_SelectedMet.FormattingEnabled = true;
            this.cboMetQC_SelectedMet.Location = new System.Drawing.Point(108, 38);
            this.cboMetQC_SelectedMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMetQC_SelectedMet.Name = "cboMetQC_SelectedMet";
            this.cboMetQC_SelectedMet.Size = new System.Drawing.Size(192, 26);
            this.cboMetQC_SelectedMet.TabIndex = 206;
            this.cboMetQC_SelectedMet.SelectedIndexChanged += new System.EventHandler(this.cboMetQC_SelectedMet_SelectedIndexChanged);
            // 
            // cboFilt_or_Not
            // 
            this.cboFilt_or_Not.FormattingEnabled = true;
            this.cboFilt_or_Not.Items.AddRange(new object[] {
            "Unfiltered",
            "Filtered"});
            this.cboFilt_or_Not.Location = new System.Drawing.Point(593, 465);
            this.cboFilt_or_Not.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboFilt_or_Not.Name = "cboFilt_or_Not";
            this.cboFilt_or_Not.Size = new System.Drawing.Size(101, 26);
            this.cboFilt_or_Not.TabIndex = 205;
            this.cboFilt_or_Not.Text = "Unfiltered";
            this.cboFilt_or_Not.SelectedIndexChanged += new System.EventHandler(this.cboFilt_or_Not_SelectedIndexChanged);
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.Location = new System.Drawing.Point(1087, 500);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(346, 23);
            this.label102.TabIndex = 202;
            this.label102.Text = "Anemometer WS Diff. vs. Wind Speed";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(551, 498);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(368, 23);
            this.label70.TabIndex = 199;
            this.label70.Text = "Anemometer WS Diff. vs. Wind Direction";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(25, 498);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(227, 23);
            this.label69.TabIndex = 197;
            this.label69.Text = "Wind Speed Scatterplot";
            // 
            // lstExtrapolated
            // 
            this.lstExtrapolated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader82,
            this.columnHeader84,
            this.columnHeader83});
            this.lstExtrapolated.HideSelection = false;
            this.lstExtrapolated.Location = new System.Drawing.Point(474, 315);
            this.lstExtrapolated.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstExtrapolated.Name = "lstExtrapolated";
            this.lstExtrapolated.Size = new System.Drawing.Size(225, 145);
            this.lstExtrapolated.TabIndex = 196;
            this.lstExtrapolated.UseCompatibleStateImageBehavior = false;
            this.lstExtrapolated.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader82
            // 
            this.columnHeader82.Text = "Height, m";
            this.columnHeader82.Width = 69;
            // 
            // columnHeader84
            // 
            this.columnHeader84.DisplayIndex = 2;
            this.columnHeader84.Text = "Avg. WS";
            this.columnHeader84.Width = 73;
            // 
            // columnHeader83
            // 
            this.columnHeader83.DisplayIndex = 1;
            this.columnHeader83.Text = "% Rec.";
            this.columnHeader83.Width = 58;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(477, 283);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(222, 23);
            this.label68.TabIndex = 195;
            this.label68.Text = "Extrapolated Summary";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(323, 286);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(136, 19);
            this.label67.TabIndex = 194;
            this.label67.Text = "Temp. Summary";
            // 
            // lstTempSummary
            // 
            this.lstTempSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader80,
            this.columnHeader81});
            this.lstTempSummary.HideSelection = false;
            this.lstTempSummary.Location = new System.Drawing.Point(325, 315);
            this.lstTempSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstTempSummary.Name = "lstTempSummary";
            this.lstTempSummary.Size = new System.Drawing.Size(133, 145);
            this.lstTempSummary.TabIndex = 193;
            this.lstTempSummary.UseCompatibleStateImageBehavior = false;
            this.lstTempSummary.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader80
            // 
            this.columnHeader80.Text = "Height, m";
            this.columnHeader80.Width = 54;
            // 
            // columnHeader81
            // 
            this.columnHeader81.Text = "% Rec.";
            this.columnHeader81.Width = 48;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(26, 283);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(151, 23);
            this.label66.TabIndex = 192;
            this.label66.Text = "Vane Summary";
            // 
            // lstVaneSummary
            // 
            this.lstVaneSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader75,
            this.columnHeader76,
            this.columnHeader78,
            this.columnHeader79});
            this.lstVaneSummary.HideSelection = false;
            this.lstVaneSummary.Location = new System.Drawing.Point(29, 314);
            this.lstVaneSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstVaneSummary.Name = "lstVaneSummary";
            this.lstVaneSummary.Size = new System.Drawing.Size(283, 146);
            this.lstVaneSummary.TabIndex = 191;
            this.lstVaneSummary.UseCompatibleStateImageBehavior = false;
            this.lstVaneSummary.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader75
            // 
            this.columnHeader75.Text = "Height, m";
            this.columnHeader75.Width = 59;
            // 
            // columnHeader76
            // 
            this.columnHeader76.Text = "% Rec.";
            this.columnHeader76.Width = 53;
            // 
            // columnHeader78
            // 
            this.columnHeader78.Text = "% Iced";
            this.columnHeader78.Width = 44;
            // 
            // columnHeader79
            // 
            this.columnHeader79.Text = "% Overall Rec";
            this.columnHeader79.Width = 81;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(1046, 257);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(277, 23);
            this.label65.TabIndex = 189;
            this.label65.Text = "Alpha vs. WD (All/Day/Night)";
            // 
            // cboMetWindRose
            // 
            this.cboMetWindRose.FormattingEnabled = true;
            this.cboMetWindRose.Items.AddRange(new object[] {
            "Wind Rose",
            "WS by WD"});
            this.cboMetWindRose.Location = new System.Drawing.Point(1531, 7);
            this.cboMetWindRose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMetWindRose.Name = "cboMetWindRose";
            this.cboMetWindRose.Size = new System.Drawing.Size(86, 26);
            this.cboMetWindRose.TabIndex = 188;
            this.cboMetWindRose.Text = "Wind Rose";
            this.cboMetWindRose.SelectedIndexChanged += new System.EventHandler(this.cboMetWindRose_SelectedIndexChanged);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(1384, 9);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(140, 23);
            this.label64.TabIndex = 178;
            this.label64.Text = "WS/WD vs. WD";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(1048, 9);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(131, 23);
            this.label62.TabIndex = 13;
            this.label62.Text = "WS vs. Height";
            // 
            // lstAlphas
            // 
            this.lstAlphas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader74,
            this.columnHeader86,
            this.columnHeader87,
            this.columnHeader114});
            this.lstAlphas.HideSelection = false;
            this.lstAlphas.Location = new System.Drawing.Point(825, 62);
            this.lstAlphas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstAlphas.Name = "lstAlphas";
            this.lstAlphas.Size = new System.Drawing.Size(210, 425);
            this.lstAlphas.TabIndex = 12;
            this.lstAlphas.UseCompatibleStateImageBehavior = false;
            this.lstAlphas.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader74
            // 
            this.columnHeader74.Text = "WD";
            this.columnHeader74.Width = 49;
            // 
            // columnHeader86
            // 
            this.columnHeader86.Text = "All Hrs";
            // 
            // columnHeader87
            // 
            this.columnHeader87.Text = "Day";
            // 
            // columnHeader114
            // 
            this.columnHeader114.Text = "Night";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(827, 21);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(130, 23);
            this.label61.TabIndex = 11;
            this.label61.Text = "Alpha vs. WD";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(528, 49);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(64, 36);
            this.label60.TabIndex = 10;
            this.label60.Text = "Analysis \r\nEnd";
            this.label60.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // End_Time
            // 
            this.End_Time.CustomFormat = "MM/dd/yy HH:mm";
            this.End_Time.Enabled = false;
            this.End_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.End_Time.Location = new System.Drawing.Point(593, 58);
            this.End_Time.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.End_Time.Name = "End_Time";
            this.End_Time.Size = new System.Drawing.Size(137, 25);
            this.End_Time.TabIndex = 9;
            this.End_Time.ValueChanged += new System.EventHandler(this.End_Time_ValueChanged);
            // 
            // All_End_Time
            // 
            this.All_End_Time.CustomFormat = "MM/dd/yy HH:mm";
            this.All_End_Time.Enabled = false;
            this.All_End_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.All_End_Time.Location = new System.Drawing.Point(593, 17);
            this.All_End_Time.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.All_End_Time.Name = "All_End_Time";
            this.All_End_Time.Size = new System.Drawing.Size(137, 25);
            this.All_End_Time.TabIndex = 8;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(309, 49);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(64, 36);
            this.label43.TabIndex = 7;
            this.label43.Text = "Analysis \r\nStart";
            this.label43.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Start_Time
            // 
            this.Start_Time.CustomFormat = "MM/dd/yy HH:mm";
            this.Start_Time.Enabled = false;
            this.Start_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Start_Time.Location = new System.Drawing.Point(378, 58);
            this.Start_Time.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Start_Time.Name = "Start_Time";
            this.Start_Time.Size = new System.Drawing.Size(140, 25);
            this.Start_Time.TabIndex = 6;
            this.Start_Time.ValueChanged += new System.EventHandler(this.Start_Time_ValueChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(547, 12);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(39, 36);
            this.label26.TabIndex = 5;
            this.label26.Text = "Data \r\nEnd";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(330, 10);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(39, 36);
            this.label25.TabIndex = 4;
            this.label25.Text = "Data \r\nStart";
            // 
            // All_Start_Time
            // 
            this.All_Start_Time.CustomFormat = "MM/dd/yy HH:mm";
            this.All_Start_Time.Enabled = false;
            this.All_Start_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.All_Start_Time.Location = new System.Drawing.Point(378, 16);
            this.All_Start_Time.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.All_Start_Time.Name = "All_Start_Time";
            this.All_Start_Time.Size = new System.Drawing.Size(140, 25);
            this.All_Start_Time.TabIndex = 3;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(26, 62);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(223, 23);
            this.label24.TabIndex = 2;
            this.label24.Text = "Anemometer Summary";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Filtered Met Data Summary";
            // 
            // lstAnemSummary
            // 
            this.lstAnemSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.height,
            this.columnHeader64,
            this.columnHeader65,
            this.columnHeader66,
            this.columnHeader67,
            this.columnHeader68,
            this.columnHeader69,
            this.columnHeader70,
            this.columnHeader71,
            this.columnHeader72,
            this.columnHeader73});
            this.lstAnemSummary.HideSelection = false;
            this.lstAnemSummary.Location = new System.Drawing.Point(28, 91);
            this.lstAnemSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstAnemSummary.Name = "lstAnemSummary";
            this.lstAnemSummary.Size = new System.Drawing.Size(783, 185);
            this.lstAnemSummary.TabIndex = 0;
            this.lstAnemSummary.UseCompatibleStateImageBehavior = false;
            this.lstAnemSummary.View = System.Windows.Forms.View.Details;
            // 
            // height
            // 
            this.height.Text = "H [m]";
            this.height.Width = 49;
            // 
            // columnHeader64
            // 
            this.columnHeader64.Text = "Orient";
            this.columnHeader64.Width = 42;
            // 
            // columnHeader65
            // 
            this.columnHeader65.Text = "WS (Unfilt)";
            this.columnHeader65.Width = 71;
            // 
            // columnHeader66
            // 
            this.columnHeader66.Text = "WS (Filt)";
            this.columnHeader66.Width = 52;
            // 
            // columnHeader67
            // 
            this.columnHeader67.Text = "% Rec.";
            this.columnHeader67.Width = 54;
            // 
            // columnHeader68
            // 
            this.columnHeader68.Text = "% Iced";
            this.columnHeader68.Width = 46;
            // 
            // columnHeader69
            // 
            this.columnHeader69.Text = "% Twr Shadow";
            this.columnHeader69.Width = 85;
            // 
            // columnHeader70
            // 
            this.columnHeader70.Text = "% Min WS";
            this.columnHeader70.Width = 67;
            // 
            // columnHeader71
            // 
            this.columnHeader71.Text = "% SD";
            this.columnHeader71.Width = 39;
            // 
            // columnHeader72
            // 
            this.columnHeader72.Text = "% Max Rng";
            this.columnHeader72.Width = 73;
            // 
            // columnHeader73
            // 
            this.columnHeader73.Text = "% Overall Rec";
            this.columnHeader73.Width = 87;
            // 
            // pgeMERRA
            // 
            this.pgeMERRA.Controls.Add(this.label54);
            this.pgeMERRA.Controls.Add(this.txtMaxLong);
            this.pgeMERRA.Controls.Add(this.label146);
            this.pgeMERRA.Controls.Add(this.txtMinLong);
            this.pgeMERRA.Controls.Add(this.label52);
            this.pgeMERRA.Controls.Add(this.txtMaxLat);
            this.pgeMERRA.Controls.Add(this.label5);
            this.pgeMERRA.Controls.Add(this.txtMinLat);
            this.pgeMERRA.Controls.Add(this.btnChangeFolder);
            this.pgeMERRA.Controls.Add(this.btnDownloadMERRA2);
            this.pgeMERRA.Controls.Add(this.plotMERRA_WindRose);
            this.pgeMERRA.Controls.Add(this.plotMERRA_Monthly);
            this.pgeMERRA.Controls.Add(this.plotMERRA_Yearly);
            this.pgeMERRA.Controls.Add(this.btnImportCRV_MERRA);
            this.pgeMERRA.Controls.Add(this.chkYearsToDisplayAll);
            this.pgeMERRA.Controls.Add(this.label121);
            this.pgeMERRA.Controls.Add(this.cboMERRA_Month);
            this.pgeMERRA.Controls.Add(this.label120);
            this.pgeMERRA.Controls.Add(this.cboMERRAYear);
            this.pgeMERRA.Controls.Add(this.chkYearsToDisplay);
            this.pgeMERRA.Controls.Add(this.label162);
            this.pgeMERRA.Controls.Add(this.txtMERRA_WS_ScaleFact);
            this.pgeMERRA.Controls.Add(this.cboNumMERRA_Nodes);
            this.pgeMERRA.Controls.Add(this.label161);
            this.pgeMERRA.Controls.Add(this.label160);
            this.pgeMERRA.Controls.Add(this.cboMERRA_PowerCurves);
            this.pgeMERRA.Controls.Add(this.btn_ExportWR);
            this.pgeMERRA.Controls.Add(this.btn_Export_All_Months_All_Years);
            this.pgeMERRA.Controls.Add(this.label159);
            this.pgeMERRA.Controls.Add(this.label156);
            this.pgeMERRA.Controls.Add(this.cboMERRA_PlotParam);
            this.pgeMERRA.Controls.Add(this.lstMERRAAnnualProd);
            this.pgeMERRA.Controls.Add(this.lstMERRA_MonthlyProd);
            this.pgeMERRA.Controls.Add(this.btn_Export_Interp);
            this.pgeMERRA.Controls.Add(this.dateMERRAEnd);
            this.pgeMERRA.Controls.Add(this.dateMERRAStart);
            this.pgeMERRA.Controls.Add(this.label152);
            this.pgeMERRA.Controls.Add(this.label153);
            this.pgeMERRA.Controls.Add(this.label147);
            this.pgeMERRA.Controls.Add(this.cboMERRASelectedMet);
            this.pgeMERRA.Controls.Add(this.txtMERRA_SelectedLong);
            this.pgeMERRA.Controls.Add(this.label149);
            this.pgeMERRA.Controls.Add(this.label150);
            this.pgeMERRA.Controls.Add(this.txtMERRA_SelectedLat);
            this.pgeMERRA.Controls.Add(this.label154);
            this.pgeMERRA.Controls.Add(this.txt_MERRA2_folder);
            this.pgeMERRA.Controls.Add(this.btn_Import_MERRA);
            this.pgeMERRA.Controls.Add(this.label151);
            this.pgeMERRA.Location = new System.Drawing.Point(4, 27);
            this.pgeMERRA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeMERRA.Name = "pgeMERRA";
            this.pgeMERRA.Size = new System.Drawing.Size(1627, 849);
            this.pgeMERRA.TabIndex = 16;
            this.pgeMERRA.Text = "MERRA2 Data";
            this.pgeMERRA.UseVisualStyleBackColor = true;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(400, 455);
            this.label54.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(20, 18);
            this.label54.TabIndex = 236;
            this.label54.Text = "to";
            // 
            // txtMaxLong
            // 
            this.txtMaxLong.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxLong.Location = new System.Drawing.Point(422, 452);
            this.txtMaxLong.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaxLong.Name = "txtMaxLong";
            this.txtMaxLong.Size = new System.Drawing.Size(47, 25);
            this.txtMaxLong.TabIndex = 235;
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label146.Location = new System.Drawing.Point(238, 455);
            this.label146.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(109, 18);
            this.label146.TabIndex = 234;
            this.label146.Text = "Longitude range:";
            // 
            // txtMinLong
            // 
            this.txtMinLong.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinLong.Location = new System.Drawing.Point(350, 452);
            this.txtMinLong.Margin = new System.Windows.Forms.Padding(2);
            this.txtMinLong.Name = "txtMinLong";
            this.txtMinLong.Size = new System.Drawing.Size(47, 25);
            this.txtMinLong.TabIndex = 233;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(400, 422);
            this.label52.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(20, 18);
            this.label52.TabIndex = 232;
            this.label52.Text = "to";
            // 
            // txtMaxLat
            // 
            this.txtMaxLat.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxLat.Location = new System.Drawing.Point(422, 419);
            this.txtMaxLat.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaxLat.Name = "txtMaxLat";
            this.txtMaxLat.Size = new System.Drawing.Size(47, 25);
            this.txtMaxLat.TabIndex = 231;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(247, 422);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 18);
            this.label5.TabIndex = 230;
            this.label5.Text = "Latitude range:";
            // 
            // txtMinLat
            // 
            this.txtMinLat.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinLat.Location = new System.Drawing.Point(350, 419);
            this.txtMinLat.Margin = new System.Windows.Forms.Padding(2);
            this.txtMinLat.Name = "txtMinLat";
            this.txtMinLat.Size = new System.Drawing.Size(47, 25);
            this.txtMinLat.TabIndex = 229;
            // 
            // btnChangeFolder
            // 
            this.btnChangeFolder.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.btnChangeFolder.Location = new System.Drawing.Point(381, 76);
            this.btnChangeFolder.Name = "btnChangeFolder";
            this.btnChangeFolder.Size = new System.Drawing.Size(104, 31);
            this.btnChangeFolder.TabIndex = 228;
            this.btnChangeFolder.Text = "Change Folder";
            this.btnChangeFolder.UseVisualStyleBackColor = true;
            this.btnChangeFolder.Click += new System.EventHandler(this.btnChangeFolder_Click);
            // 
            // btnDownloadMERRA2
            // 
            this.btnDownloadMERRA2.BackColor = System.Drawing.Color.LightCoral;
            this.btnDownloadMERRA2.Location = new System.Drawing.Point(49, 421);
            this.btnDownloadMERRA2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDownloadMERRA2.Name = "btnDownloadMERRA2";
            this.btnDownloadMERRA2.Size = new System.Drawing.Size(184, 52);
            this.btnDownloadMERRA2.TabIndex = 227;
            this.btnDownloadMERRA2.Text = "Download MERRA2 data";
            this.btnDownloadMERRA2.UseVisualStyleBackColor = false;
            this.btnDownloadMERRA2.Click += new System.EventHandler(this.btnDownloadMERRA2_Click);
            // 
            // plotMERRA_WindRose
            // 
            this.plotMERRA_WindRose.Location = new System.Drawing.Point(1290, 91);
            this.plotMERRA_WindRose.Name = "plotMERRA_WindRose";
            this.plotMERRA_WindRose.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMERRA_WindRose.Size = new System.Drawing.Size(320, 320);
            this.plotMERRA_WindRose.TabIndex = 226;
            this.plotMERRA_WindRose.Text = "plotView1";
            this.plotMERRA_WindRose.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMERRA_WindRose.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMERRA_WindRose.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotMERRA_Monthly
            // 
            this.plotMERRA_Monthly.Location = new System.Drawing.Point(1050, 421);
            this.plotMERRA_Monthly.Name = "plotMERRA_Monthly";
            this.plotMERRA_Monthly.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMERRA_Monthly.Size = new System.Drawing.Size(560, 405);
            this.plotMERRA_Monthly.TabIndex = 225;
            this.plotMERRA_Monthly.Text = "plotView1";
            this.plotMERRA_Monthly.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMERRA_Monthly.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMERRA_Monthly.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotMERRA_Yearly
            // 
            this.plotMERRA_Yearly.Location = new System.Drawing.Point(474, 421);
            this.plotMERRA_Yearly.Name = "plotMERRA_Yearly";
            this.plotMERRA_Yearly.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMERRA_Yearly.Size = new System.Drawing.Size(560, 405);
            this.plotMERRA_Yearly.TabIndex = 224;
            this.plotMERRA_Yearly.Text = "plotView1";
            this.plotMERRA_Yearly.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMERRA_Yearly.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMERRA_Yearly.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnImportCRV_MERRA
            // 
            this.btnImportCRV_MERRA.BackColor = System.Drawing.Color.LightCoral;
            this.btnImportCRV_MERRA.Location = new System.Drawing.Point(999, 6);
            this.btnImportCRV_MERRA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportCRV_MERRA.Name = "btnImportCRV_MERRA";
            this.btnImportCRV_MERRA.Size = new System.Drawing.Size(160, 36);
            this.btnImportCRV_MERRA.TabIndex = 213;
            this.btnImportCRV_MERRA.Text = "Import Power Curve";
            this.btnImportCRV_MERRA.UseVisualStyleBackColor = false;
            this.btnImportCRV_MERRA.Click += new System.EventHandler(this.btnImportCRV_MERRA_Click);
            // 
            // chkYearsToDisplayAll
            // 
            this.chkYearsToDisplayAll.AutoSize = true;
            this.chkYearsToDisplayAll.Checked = true;
            this.chkYearsToDisplayAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYearsToDisplayAll.Location = new System.Drawing.Point(1142, 84);
            this.chkYearsToDisplayAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkYearsToDisplayAll.Name = "chkYearsToDisplayAll";
            this.chkYearsToDisplayAll.Size = new System.Drawing.Size(139, 23);
            this.chkYearsToDisplayAll.TabIndex = 212;
            this.chkYearsToDisplayAll.Text = "Select/Deselect All";
            this.chkYearsToDisplayAll.UseVisualStyleBackColor = true;
            this.chkYearsToDisplayAll.CheckedChanged += new System.EventHandler(this.chkYearsToDisplayAll_CheckedChanged);
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label121.Location = new System.Drawing.Point(1429, 50);
            this.label121.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(57, 18);
            this.label121.TabIndex = 211;
            this.label121.Text = "Month : ";
            // 
            // cboMERRA_Month
            // 
            this.cboMERRA_Month.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMERRA_Month.FormattingEnabled = true;
            this.cboMERRA_Month.Items.AddRange(new object[] {
            "All Months",
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sept",
            "Oct",
            "Nov",
            "Dec"});
            this.cboMERRA_Month.Location = new System.Drawing.Point(1490, 47);
            this.cboMERRA_Month.Margin = new System.Windows.Forms.Padding(2);
            this.cboMERRA_Month.Name = "cboMERRA_Month";
            this.cboMERRA_Month.Size = new System.Drawing.Size(123, 26);
            this.cboMERRA_Month.TabIndex = 210;
            this.cboMERRA_Month.Text = "All Months";
            this.cboMERRA_Month.SelectedIndexChanged += new System.EventHandler(this.cboMERRA_Month_SelectedIndexChanged);
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label120.Location = new System.Drawing.Point(1244, 50);
            this.label120.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(44, 18);
            this.label120.TabIndex = 209;
            this.label120.Text = "Year : ";
            // 
            // cboMERRAYear
            // 
            this.cboMERRAYear.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMERRAYear.FormattingEnabled = true;
            this.cboMERRAYear.Items.AddRange(new object[] {
            "CF, %",
            "Energy Prod.",
            "Dev. from LT"});
            this.cboMERRAYear.Location = new System.Drawing.Point(1291, 47);
            this.cboMERRAYear.Margin = new System.Windows.Forms.Padding(2);
            this.cboMERRAYear.Name = "cboMERRAYear";
            this.cboMERRAYear.Size = new System.Drawing.Size(123, 26);
            this.cboMERRAYear.TabIndex = 208;
            this.cboMERRAYear.SelectedIndexChanged += new System.EventHandler(this.cboMERRAYear_SelectedIndexChanged);
            // 
            // chkYearsToDisplay
            // 
            this.chkYearsToDisplay.CheckOnClick = true;
            this.chkYearsToDisplay.FormattingEnabled = true;
            this.chkYearsToDisplay.Location = new System.Drawing.Point(1161, 117);
            this.chkYearsToDisplay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkYearsToDisplay.Name = "chkYearsToDisplay";
            this.chkYearsToDisplay.Size = new System.Drawing.Size(108, 264);
            this.chkYearsToDisplay.TabIndex = 207;
            this.chkYearsToDisplay.SelectedIndexChanged += new System.EventHandler(this.chkYearsToDisplay_SelectedIndexChanged);
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label162.Location = new System.Drawing.Point(343, 159);
            this.label162.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(109, 18);
            this.label162.TabIndex = 206;
            this.label162.Text = "WS Scale Factor :";
            // 
            // txtMERRA_WS_ScaleFact
            // 
            this.txtMERRA_WS_ScaleFact.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMERRA_WS_ScaleFact.Location = new System.Drawing.Point(374, 183);
            this.txtMERRA_WS_ScaleFact.Margin = new System.Windows.Forms.Padding(2);
            this.txtMERRA_WS_ScaleFact.Name = "txtMERRA_WS_ScaleFact";
            this.txtMERRA_WS_ScaleFact.Size = new System.Drawing.Size(66, 25);
            this.txtMERRA_WS_ScaleFact.TabIndex = 205;
            this.txtMERRA_WS_ScaleFact.Text = "0.85";
            this.txtMERRA_WS_ScaleFact.TextChanged += new System.EventHandler(this.txtMERRA_WS_ScaleFact_TextChanged);
            // 
            // cboNumMERRA_Nodes
            // 
            this.cboNumMERRA_Nodes.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNumMERRA_Nodes.FormattingEnabled = true;
            this.cboNumMERRA_Nodes.Items.AddRange(new object[] {
            "1",
            "4",
            "16"});
            this.cboNumMERRA_Nodes.Location = new System.Drawing.Point(265, 183);
            this.cboNumMERRA_Nodes.Margin = new System.Windows.Forms.Padding(2);
            this.cboNumMERRA_Nodes.Name = "cboNumMERRA_Nodes";
            this.cboNumMERRA_Nodes.Size = new System.Drawing.Size(72, 26);
            this.cboNumMERRA_Nodes.TabIndex = 204;
            this.cboNumMERRA_Nodes.Text = "1";
            this.cboNumMERRA_Nodes.SelectedIndexChanged += new System.EventHandler(this.cboNumMERRA_Nodes_SelectedIndexChanged);
            // 
            // label161
            // 
            this.label161.AutoSize = true;
            this.label161.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label161.Location = new System.Drawing.Point(15, 188);
            this.label161.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(195, 18);
            this.label161.TabIndex = 203;
            this.label161.Text = "Num. of MERRA2 Nodes to use:";
            // 
            // label160
            // 
            this.label160.AutoSize = true;
            this.label160.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label160.Location = new System.Drawing.Point(850, 53);
            this.label160.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(96, 18);
            this.label160.TabIndex = 202;
            this.label160.Text = "Power Curve : ";
            // 
            // cboMERRA_PowerCurves
            // 
            this.cboMERRA_PowerCurves.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMERRA_PowerCurves.FormattingEnabled = true;
            this.cboMERRA_PowerCurves.Location = new System.Drawing.Point(952, 49);
            this.cboMERRA_PowerCurves.Margin = new System.Windows.Forms.Padding(2);
            this.cboMERRA_PowerCurves.MaxDropDownItems = 100;
            this.cboMERRA_PowerCurves.Name = "cboMERRA_PowerCurves";
            this.cboMERRA_PowerCurves.Size = new System.Drawing.Size(279, 26);
            this.cboMERRA_PowerCurves.TabIndex = 201;
            this.cboMERRA_PowerCurves.SelectedIndexChanged += new System.EventHandler(this.cboMERRA_PowerCurves_SelectedIndexChanged);
            // 
            // btn_ExportWR
            // 
            this.btn_ExportWR.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ExportWR.Location = new System.Drawing.Point(70, 734);
            this.btn_ExportWR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_ExportWR.Name = "btn_ExportWR";
            this.btn_ExportWR.Size = new System.Drawing.Size(128, 59);
            this.btn_ExportWR.TabIndex = 154;
            this.btn_ExportWR.Text = "Export Wind Rose";
            this.btn_ExportWR.UseVisualStyleBackColor = true;
            this.btn_ExportWR.Click += new System.EventHandler(this.btn_ExportWR_Click);
            // 
            // btn_Export_All_Months_All_Years
            // 
            this.btn_Export_All_Months_All_Years.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export_All_Months_All_Years.Location = new System.Drawing.Point(70, 658);
            this.btn_Export_All_Months_All_Years.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Export_All_Months_All_Years.Name = "btn_Export_All_Months_All_Years";
            this.btn_Export_All_Months_All_Years.Size = new System.Drawing.Size(128, 58);
            this.btn_Export_All_Months_All_Years.TabIndex = 153;
            this.btn_Export_All_Months_All_Years.Text = "Export All Months && All Years";
            this.btn_Export_All_Months_All_Years.UseVisualStyleBackColor = true;
            this.btn_Export_All_Months_All_Years.Click += new System.EventHandler(this.btn_Export_All_Months_All_Years_Click);
            // 
            // label159
            // 
            this.label159.AutoSize = true;
            this.label159.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label159.Location = new System.Drawing.Point(1305, 17);
            this.label159.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(289, 20);
            this.label159.TabIndex = 151;
            this.label159.Text = "MERRA2 Wind Rose for Selected Interval:";
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label156.Location = new System.Drawing.Point(544, 55);
            this.label156.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(101, 18);
            this.label156.TabIndex = 146;
            this.label156.Text = "Plot Parameter:";
            // 
            // cboMERRA_PlotParam
            // 
            this.cboMERRA_PlotParam.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMERRA_PlotParam.FormattingEnabled = true;
            this.cboMERRA_PlotParam.Items.AddRange(new object[] {
            "CF, %",
            "Energy Prod.",
            "Dev. from LT"});
            this.cboMERRA_PlotParam.Location = new System.Drawing.Point(657, 50);
            this.cboMERRA_PlotParam.Margin = new System.Windows.Forms.Padding(2);
            this.cboMERRA_PlotParam.Name = "cboMERRA_PlotParam";
            this.cboMERRA_PlotParam.Size = new System.Drawing.Size(166, 25);
            this.cboMERRA_PlotParam.TabIndex = 145;
            this.cboMERRA_PlotParam.Text = "CF (%)";
            this.cboMERRA_PlotParam.SelectedIndexChanged += new System.EventHandler(this.cboMERRA_PlotParam_SelectedIndexChanged);
            // 
            // lstMERRAAnnualProd
            // 
            this.lstMERRAAnnualProd.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader85,
            this.columnHeader116,
            this.columnHeader117});
            this.lstMERRAAnnualProd.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMERRAAnnualProd.GridLines = true;
            this.lstMERRAAnnualProd.HideSelection = false;
            this.lstMERRAAnnualProd.Location = new System.Drawing.Point(504, 90);
            this.lstMERRAAnnualProd.Margin = new System.Windows.Forms.Padding(2);
            this.lstMERRAAnnualProd.Name = "lstMERRAAnnualProd";
            this.lstMERRAAnnualProd.Size = new System.Drawing.Size(287, 313);
            this.lstMERRAAnnualProd.TabIndex = 141;
            this.lstMERRAAnnualProd.UseCompatibleStateImageBehavior = false;
            this.lstMERRAAnnualProd.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader85
            // 
            this.columnHeader85.Text = "Year";
            this.columnHeader85.Width = 40;
            // 
            // columnHeader116
            // 
            this.columnHeader116.Text = "WS (m/s)";
            this.columnHeader116.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader116.Width = 63;
            // 
            // columnHeader117
            // 
            this.columnHeader117.Text = "% Diff from LT";
            this.columnHeader117.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader117.Width = 85;
            // 
            // lstMERRA_MonthlyProd
            // 
            this.lstMERRA_MonthlyProd.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Month,
            this.ThisYear,
            this.Diff,
            this.Average});
            this.lstMERRA_MonthlyProd.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMERRA_MonthlyProd.GridLines = true;
            this.lstMERRA_MonthlyProd.HideSelection = false;
            this.lstMERRA_MonthlyProd.Location = new System.Drawing.Point(800, 90);
            this.lstMERRA_MonthlyProd.Margin = new System.Windows.Forms.Padding(2);
            this.lstMERRA_MonthlyProd.Name = "lstMERRA_MonthlyProd";
            this.lstMERRA_MonthlyProd.Size = new System.Drawing.Size(331, 313);
            this.lstMERRA_MonthlyProd.TabIndex = 139;
            this.lstMERRA_MonthlyProd.UseCompatibleStateImageBehavior = false;
            this.lstMERRA_MonthlyProd.View = System.Windows.Forms.View.Details;
            // 
            // Month
            // 
            this.Month.Text = "Month";
            this.Month.Width = 44;
            // 
            // ThisYear
            // 
            this.ThisYear.Text = "Year";
            this.ThisYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ThisYear.Width = 48;
            // 
            // Diff
            // 
            this.Diff.Text = "WS (m/s)";
            this.Diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Diff.Width = 59;
            // 
            // Average
            // 
            this.Average.Text = "% Diff from LT";
            this.Average.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Average.Width = 83;
            // 
            // btn_Export_Interp
            // 
            this.btn_Export_Interp.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export_Interp.Location = new System.Drawing.Point(37, 582);
            this.btn_Export_Interp.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Export_Interp.Name = "btn_Export_Interp";
            this.btn_Export_Interp.Size = new System.Drawing.Size(203, 62);
            this.btn_Export_Interp.TabIndex = 138;
            this.btn_Export_Interp.Text = "Export Interpolated OE-Extracted MERRA2 Data";
            this.btn_Export_Interp.UseVisualStyleBackColor = true;
            this.btn_Export_Interp.Click += new System.EventHandler(this.btn_Export_Interp_Click);
            // 
            // dateMERRAEnd
            // 
            this.dateMERRAEnd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAEnd.CausesValidation = false;
            this.dateMERRAEnd.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateMERRAEnd.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMERRAEnd.Location = new System.Drawing.Point(136, 354);
            this.dateMERRAEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dateMERRAEnd.MaxDate = new System.DateTime(2050, 12, 1, 0, 0, 0, 0);
            this.dateMERRAEnd.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
            this.dateMERRAEnd.Name = "dateMERRAEnd";
            this.dateMERRAEnd.Size = new System.Drawing.Size(163, 25);
            this.dateMERRAEnd.TabIndex = 124;
            this.dateMERRAEnd.Value = new System.DateTime(2018, 12, 31, 23, 0, 0, 0);
            this.dateMERRAEnd.ValueChanged += new System.EventHandler(this.dateMERRAEnd_ValueChanged);
            // 
            // dateMERRAStart
            // 
            this.dateMERRAStart.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAStart.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateMERRAStart.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMERRAStart.Location = new System.Drawing.Point(136, 320);
            this.dateMERRAStart.Margin = new System.Windows.Forms.Padding(2);
            this.dateMERRAStart.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dateMERRAStart.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
            this.dateMERRAStart.Name = "dateMERRAStart";
            this.dateMERRAStart.Size = new System.Drawing.Size(163, 25);
            this.dateMERRAStart.TabIndex = 123;
            this.dateMERRAStart.Value = new System.DateTime(1989, 1, 1, 0, 0, 0, 0);
            this.dateMERRAStart.ValueChanged += new System.EventHandler(this.dateMERRAStart_ValueChanged);
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label152.Location = new System.Drawing.Point(23, 354);
            this.label152.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(74, 18);
            this.label152.TabIndex = 122;
            this.label152.Text = "End (Local)";
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label153.Location = new System.Drawing.Point(22, 326);
            this.label153.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(81, 18);
            this.label153.TabIndex = 121;
            this.label153.Text = "Start (Local)";
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label147.Location = new System.Drawing.Point(31, 228);
            this.label147.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(88, 18);
            this.label147.TabIndex = 115;
            this.label147.Text = "Selected Met :";
            // 
            // cboMERRASelectedMet
            // 
            this.cboMERRASelectedMet.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMERRASelectedMet.FormattingEnabled = true;
            this.cboMERRASelectedMet.Items.AddRange(new object[] {
            "User-Defined Lat/Long"});
            this.cboMERRASelectedMet.Location = new System.Drawing.Point(135, 226);
            this.cboMERRASelectedMet.Margin = new System.Windows.Forms.Padding(2);
            this.cboMERRASelectedMet.Name = "cboMERRASelectedMet";
            this.cboMERRASelectedMet.Size = new System.Drawing.Size(320, 26);
            this.cboMERRASelectedMet.TabIndex = 114;
            this.cboMERRASelectedMet.SelectedIndexChanged += new System.EventHandler(this.cboMERRASelectedMet_SelectedIndexChanged);
            // 
            // txtMERRA_SelectedLong
            // 
            this.txtMERRA_SelectedLong.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMERRA_SelectedLong.Location = new System.Drawing.Point(317, 278);
            this.txtMERRA_SelectedLong.Margin = new System.Windows.Forms.Padding(2);
            this.txtMERRA_SelectedLong.Name = "txtMERRA_SelectedLong";
            this.txtMERRA_SelectedLong.Size = new System.Drawing.Size(110, 25);
            this.txtMERRA_SelectedLong.TabIndex = 113;
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label149.Location = new System.Drawing.Point(222, 278);
            this.label149.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(72, 18);
            this.label149.TabIndex = 112;
            this.label149.Text = "Longitude:";
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label150.Location = new System.Drawing.Point(19, 279);
            this.label150.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(62, 18);
            this.label150.TabIndex = 111;
            this.label150.Text = "Latitude:";
            // 
            // txtMERRA_SelectedLat
            // 
            this.txtMERRA_SelectedLat.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMERRA_SelectedLat.Location = new System.Drawing.Point(93, 277);
            this.txtMERRA_SelectedLat.Margin = new System.Windows.Forms.Padding(2);
            this.txtMERRA_SelectedLat.Name = "txtMERRA_SelectedLat";
            this.txtMERRA_SelectedLat.Size = new System.Drawing.Size(110, 25);
            this.txtMERRA_SelectedLat.TabIndex = 110;
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Font = new System.Drawing.Font("Palatino Linotype", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label154.Location = new System.Drawing.Point(22, 91);
            this.label154.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(178, 20);
            this.label154.TabIndex = 109;
            this.label154.Text = "MERRA2 Folder location:";
            // 
            // txt_MERRA2_folder
            // 
            this.txt_MERRA2_folder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MERRA2_folder.Location = new System.Drawing.Point(26, 117);
            this.txt_MERRA2_folder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_MERRA2_folder.Name = "txt_MERRA2_folder";
            this.txt_MERRA2_folder.ReadOnly = true;
            this.txt_MERRA2_folder.Size = new System.Drawing.Size(459, 21);
            this.txt_MERRA2_folder.TabIndex = 108;
            // 
            // btn_Import_MERRA
            // 
            this.btn_Import_MERRA.BackColor = System.Drawing.Color.LightCoral;
            this.btn_Import_MERRA.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Import_MERRA.Location = new System.Drawing.Point(49, 497);
            this.btn_Import_MERRA.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Import_MERRA.Name = "btn_Import_MERRA";
            this.btn_Import_MERRA.Size = new System.Drawing.Size(184, 71);
            this.btn_Import_MERRA.TabIndex = 107;
            this.btn_Import_MERRA.Text = "Extract MERRA2 Data for Selected Met or Lat/Long";
            this.btn_Import_MERRA.UseVisualStyleBackColor = false;
            this.btn_Import_MERRA.Click += new System.EventHandler(this.btn_Import_MERRA_Click);
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label151.Location = new System.Drawing.Point(22, 22);
            this.label151.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(371, 25);
            this.label151.TabIndex = 99;
            this.label151.Text = "Long-Term Reference MERRA2 Data";
            // 
            // pgeMCP
            // 
            this.pgeMCP.Controls.Add(this.btnExportTarget);
            this.pgeMCP.Controls.Add(this.plotMCP_Uncertainty);
            this.pgeMCP.Controls.Add(this.plotMCP);
            this.pgeMCP.Controls.Add(this.btnShowMCPRanges);
            this.pgeMCP.Controls.Add(this.label51);
            this.pgeMCP.Controls.Add(this.btnDoMCPAllMets);
            this.pgeMCP.Controls.Add(this.btnDoMCP);
            this.pgeMCP.Controls.Add(this.btnResetDates);
            this.pgeMCP.Controls.Add(this.dateMCPExportEnd);
            this.pgeMCP.Controls.Add(this.label145);
            this.pgeMCP.Controls.Add(this.dateMCPExportStart);
            this.pgeMCP.Controls.Add(this.dateConcurrentEnd);
            this.pgeMCP.Controls.Add(this.label144);
            this.pgeMCP.Controls.Add(this.dateConcurrentStart);
            this.pgeMCP.Controls.Add(this.txtTAB_WS_bin);
            this.pgeMCP.Controls.Add(this.label142);
            this.pgeMCP.Controls.Add(this.label143);
            this.pgeMCP.Controls.Add(this.cboTAB_bins);
            this.pgeMCP.Controls.Add(this.label141);
            this.pgeMCP.Controls.Add(this.label140);
            this.pgeMCP.Controls.Add(this.txtLast_WS_Wgt);
            this.pgeMCP.Controls.Add(this.label134);
            this.pgeMCP.Controls.Add(this.txtWS_PDF_Wgt);
            this.pgeMCP.Controls.Add(this.label135);
            this.pgeMCP.Controls.Add(this.cboMCPNumSeasons);
            this.pgeMCP.Controls.Add(this.label136);
            this.pgeMCP.Controls.Add(this.cboMCPNumHours);
            this.pgeMCP.Controls.Add(this.label137);
            this.pgeMCP.Controls.Add(this.txtWS_bin_width);
            this.pgeMCP.Controls.Add(this.label138);
            this.pgeMCP.Controls.Add(this.cboMCPNumWD);
            this.pgeMCP.Controls.Add(this.label139);
            this.pgeMCP.Controls.Add(this.cboMCP_Type);
            this.pgeMCP.Controls.Add(this.label133);
            this.pgeMCP.Controls.Add(this.txtNumYrsConc);
            this.pgeMCP.Controls.Add(this.label132);
            this.pgeMCP.Controls.Add(this.txtRsq);
            this.pgeMCP.Controls.Add(this.label130);
            this.pgeMCP.Controls.Add(this.btnExportBinRatios);
            this.pgeMCP.Controls.Add(this.txtIntercept);
            this.pgeMCP.Controls.Add(this.txtSlope);
            this.pgeMCP.Controls.Add(this.label123);
            this.pgeMCP.Controls.Add(this.label124);
            this.pgeMCP.Controls.Add(this.label125);
            this.pgeMCP.Controls.Add(this.lstMCP_Bins);
            this.pgeMCP.Controls.Add(this.lblRegStats);
            this.pgeMCP.Controls.Add(this.txtNumYrsTarg);
            this.pgeMCP.Controls.Add(this.txtNumYrsRef);
            this.pgeMCP.Controls.Add(this.label127);
            this.pgeMCP.Controls.Add(this.label128);
            this.pgeMCP.Controls.Add(this.label129);
            this.pgeMCP.Controls.Add(this.btnExportMCP_TAB);
            this.pgeMCP.Controls.Add(this.btnExportMCP_TS);
            this.pgeMCP.Controls.Add(this.cboUncertStep);
            this.pgeMCP.Controls.Add(this.label119);
            this.pgeMCP.Controls.Add(this.lstMCP_Uncert);
            this.pgeMCP.Controls.Add(this.btnExportMCPUncert);
            this.pgeMCP.Controls.Add(this.label118);
            this.pgeMCP.Controls.Add(this.btnMCP_Uncert);
            this.pgeMCP.Controls.Add(this.label108);
            this.pgeMCP.Controls.Add(this.cboMCP_Season);
            this.pgeMCP.Controls.Add(this.label109);
            this.pgeMCP.Controls.Add(this.cboMCP_TOD);
            this.pgeMCP.Controls.Add(this.label110);
            this.pgeMCP.Controls.Add(this.txtLTratio);
            this.pgeMCP.Controls.Add(this.txtAvgRatio);
            this.pgeMCP.Controls.Add(this.label111);
            this.pgeMCP.Controls.Add(this.label112);
            this.pgeMCP.Controls.Add(this.txtDataCount);
            this.pgeMCP.Controls.Add(this.txtTarg_LT_WS);
            this.pgeMCP.Controls.Add(this.txtRef_LT_WS);
            this.pgeMCP.Controls.Add(this.label113);
            this.pgeMCP.Controls.Add(this.label114);
            this.pgeMCP.Controls.Add(this.cboMCP_WD);
            this.pgeMCP.Controls.Add(this.txtTargAvgWS);
            this.pgeMCP.Controls.Add(this.txtRefAvgWS);
            this.pgeMCP.Controls.Add(this.label115);
            this.pgeMCP.Controls.Add(this.label116);
            this.pgeMCP.Controls.Add(this.label117);
            this.pgeMCP.Controls.Add(this.label107);
            this.pgeMCP.Controls.Add(this.cboMCP_Met);
            this.pgeMCP.Controls.Add(this.label106);
            this.pgeMCP.Location = new System.Drawing.Point(4, 27);
            this.pgeMCP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeMCP.Name = "pgeMCP";
            this.pgeMCP.Size = new System.Drawing.Size(1627, 849);
            this.pgeMCP.TabIndex = 15;
            this.pgeMCP.Text = "MCP";
            this.pgeMCP.UseVisualStyleBackColor = true;
            // 
            // btnExportTarget
            // 
            this.btnExportTarget.Location = new System.Drawing.Point(979, 766);
            this.btnExportTarget.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportTarget.Name = "btnExportTarget";
            this.btnExportTarget.Size = new System.Drawing.Size(113, 58);
            this.btnExportTarget.TabIndex = 298;
            this.btnExportTarget.Text = "Export Hourly Target Data";
            this.btnExportTarget.UseVisualStyleBackColor = true;
            this.btnExportTarget.Click += new System.EventHandler(this.btnExportTarget_Click);
            // 
            // plotMCP_Uncertainty
            // 
            this.plotMCP_Uncertainty.Location = new System.Drawing.Point(1008, 400);
            this.plotMCP_Uncertainty.Name = "plotMCP_Uncertainty";
            this.plotMCP_Uncertainty.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMCP_Uncertainty.Size = new System.Drawing.Size(423, 311);
            this.plotMCP_Uncertainty.TabIndex = 297;
            this.plotMCP_Uncertainty.Text = "plotView1";
            this.plotMCP_Uncertainty.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMCP_Uncertainty.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMCP_Uncertainty.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotMCP
            // 
            this.plotMCP.Location = new System.Drawing.Point(304, 286);
            this.plotMCP.Name = "plotMCP";
            this.plotMCP.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMCP.Size = new System.Drawing.Size(678, 425);
            this.plotMCP.TabIndex = 296;
            this.plotMCP.Text = "plotView1";
            this.plotMCP.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMCP.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMCP.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnShowMCPRanges
            // 
            this.btnShowMCPRanges.Location = new System.Drawing.Point(1463, 161);
            this.btnShowMCPRanges.Name = "btnShowMCPRanges";
            this.btnShowMCPRanges.Size = new System.Drawing.Size(138, 56);
            this.btnShowMCPRanges.TabIndex = 295;
            this.btnShowMCPRanges.Text = "View MCP Setting Valid Ranges";
            this.btnShowMCPRanges.UseVisualStyleBackColor = true;
            this.btnShowMCPRanges.Click += new System.EventHandler(this.btnShowMCPRanges_Click);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(1260, 191);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(121, 19);
            this.label51.TabIndex = 294;
            this.label51.Text = "Matrix Settings:";
            // 
            // btnDoMCPAllMets
            // 
            this.btnDoMCPAllMets.Location = new System.Drawing.Point(492, 25);
            this.btnDoMCPAllMets.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDoMCPAllMets.Name = "btnDoMCPAllMets";
            this.btnDoMCPAllMets.Size = new System.Drawing.Size(148, 46);
            this.btnDoMCPAllMets.TabIndex = 293;
            this.btnDoMCPAllMets.Text = "Do MCP at All Mets";
            this.btnDoMCPAllMets.UseVisualStyleBackColor = true;
            this.btnDoMCPAllMets.Click += new System.EventHandler(this.btnDoMCPAllMets_Click);
            // 
            // btnDoMCP
            // 
            this.btnDoMCP.Location = new System.Drawing.Point(391, 25);
            this.btnDoMCP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDoMCP.Name = "btnDoMCP";
            this.btnDoMCP.Size = new System.Drawing.Size(94, 46);
            this.btnDoMCP.TabIndex = 292;
            this.btnDoMCP.Text = "Do MCP";
            this.btnDoMCP.UseVisualStyleBackColor = true;
            this.btnDoMCP.Click += new System.EventHandler(this.btnDoMCP_Click);
            // 
            // btnResetDates
            // 
            this.btnResetDates.Location = new System.Drawing.Point(539, 725);
            this.btnResetDates.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnResetDates.Name = "btnResetDates";
            this.btnResetDates.Size = new System.Drawing.Size(112, 32);
            this.btnResetDates.TabIndex = 291;
            this.btnResetDates.Text = "Reset Dates";
            this.btnResetDates.UseVisualStyleBackColor = true;
            this.btnResetDates.Click += new System.EventHandler(this.btnResetDates_Click);
            // 
            // dateMCPExportEnd
            // 
            this.dateMCPExportEnd.CustomFormat = "MM/dd/yy HH:mm";
            this.dateMCPExportEnd.Enabled = false;
            this.dateMCPExportEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMCPExportEnd.Location = new System.Drawing.Point(379, 796);
            this.dateMCPExportEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateMCPExportEnd.Name = "dateMCPExportEnd";
            this.dateMCPExportEnd.Size = new System.Drawing.Size(140, 25);
            this.dateMCPExportEnd.TabIndex = 290;
            this.dateMCPExportEnd.ValueChanged += new System.EventHandler(this.dateMCPExportEnd_ValueChanged);
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label145.Location = new System.Drawing.Point(366, 730);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(132, 17);
            this.label145.TabIndex = 289;
            this.label145.Text = "Export Data Range:";
            // 
            // dateMCPExportStart
            // 
            this.dateMCPExportStart.CustomFormat = "MM/dd/yy HH:mm";
            this.dateMCPExportStart.Enabled = false;
            this.dateMCPExportStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMCPExportStart.Location = new System.Drawing.Point(379, 762);
            this.dateMCPExportStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateMCPExportStart.Name = "dateMCPExportStart";
            this.dateMCPExportStart.Size = new System.Drawing.Size(140, 25);
            this.dateMCPExportStart.TabIndex = 288;
            this.dateMCPExportStart.ValueChanged += new System.EventHandler(this.dateMCPExportStart_ValueChanged);
            // 
            // dateConcurrentEnd
            // 
            this.dateConcurrentEnd.CustomFormat = "MM/dd/yy HH:mm";
            this.dateConcurrentEnd.Enabled = false;
            this.dateConcurrentEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateConcurrentEnd.Location = new System.Drawing.Point(259, 193);
            this.dateConcurrentEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateConcurrentEnd.Name = "dateConcurrentEnd";
            this.dateConcurrentEnd.Size = new System.Drawing.Size(140, 25);
            this.dateConcurrentEnd.TabIndex = 287;
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label144.Location = new System.Drawing.Point(250, 128);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(151, 18);
            this.label144.TabIndex = 286;
            this.label144.Text = "Concurrent Data Range:";
            // 
            // dateConcurrentStart
            // 
            this.dateConcurrentStart.CustomFormat = "MM/dd/yy HH:mm";
            this.dateConcurrentStart.Enabled = false;
            this.dateConcurrentStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateConcurrentStart.Location = new System.Drawing.Point(259, 155);
            this.dateConcurrentStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateConcurrentStart.Name = "dateConcurrentStart";
            this.dateConcurrentStart.Size = new System.Drawing.Size(140, 25);
            this.dateConcurrentStart.TabIndex = 285;
            // 
            // txtTAB_WS_bin
            // 
            this.txtTAB_WS_bin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTAB_WS_bin.Location = new System.Drawing.Point(895, 793);
            this.txtTAB_WS_bin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTAB_WS_bin.Name = "txtTAB_WS_bin";
            this.txtTAB_WS_bin.Size = new System.Drawing.Size(35, 20);
            this.txtTAB_WS_bin.TabIndex = 284;
            this.txtTAB_WS_bin.Text = "1";
            this.txtTAB_WS_bin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label142.Location = new System.Drawing.Point(791, 796);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(81, 15);
            this.label142.TabIndex = 283;
            this.label142.Text = "WS bin width:";
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label143.Location = new System.Drawing.Point(791, 766);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(86, 15);
            this.label143.TabIndex = 282;
            this.label143.Text = "Num WD bins:";
            // 
            // cboTAB_bins
            // 
            this.cboTAB_bins.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTAB_bins.FormattingEnabled = true;
            this.cboTAB_bins.Items.AddRange(new object[] {
            "16",
            "24"});
            this.cboTAB_bins.Location = new System.Drawing.Point(895, 759);
            this.cboTAB_bins.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTAB_bins.Name = "cboTAB_bins";
            this.cboTAB_bins.Size = new System.Drawing.Size(48, 23);
            this.cboTAB_bins.TabIndex = 281;
            this.cboTAB_bins.Text = "16";
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label141.Location = new System.Drawing.Point(1241, 100);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(244, 19);
            this.label141.TabIndex = 280;
            this.label141.Text = "Method of Bins / Matrix Setting :";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label140.Location = new System.Drawing.Point(989, 65);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(89, 18);
            this.label140.TabIndex = 279;
            this.label140.Text = "MCP Method:";
            // 
            // txtLast_WS_Wgt
            // 
            this.txtLast_WS_Wgt.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLast_WS_Wgt.Location = new System.Drawing.Point(1389, 254);
            this.txtLast_WS_Wgt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLast_WS_Wgt.Name = "txtLast_WS_Wgt";
            this.txtLast_WS_Wgt.Size = new System.Drawing.Size(35, 25);
            this.txtLast_WS_Wgt.TabIndex = 278;
            this.txtLast_WS_Wgt.Text = "0.5";
            this.txtLast_WS_Wgt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLast_WS_Wgt.TextChanged += new System.EventHandler(this.txtLast_WS_Wgt_TextChanged);
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label134.Location = new System.Drawing.Point(1274, 261);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(110, 18);
            this.label134.TabIndex = 277;
            this.label134.Text = "Last WS Weight :";
            // 
            // txtWS_PDF_Wgt
            // 
            this.txtWS_PDF_Wgt.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWS_PDF_Wgt.Location = new System.Drawing.Point(1389, 215);
            this.txtWS_PDF_Wgt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWS_PDF_Wgt.Name = "txtWS_PDF_Wgt";
            this.txtWS_PDF_Wgt.Size = new System.Drawing.Size(35, 25);
            this.txtWS_PDF_Wgt.TabIndex = 276;
            this.txtWS_PDF_Wgt.Text = "1";
            this.txtWS_PDF_Wgt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWS_PDF_Wgt.TextChanged += new System.EventHandler(this.txtWS_PDF_Wgt_TextChanged);
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label135.Location = new System.Drawing.Point(1275, 222);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(108, 18);
            this.label135.TabIndex = 275;
            this.label135.Text = "WS PDF Weight :";
            // 
            // cboMCPNumSeasons
            // 
            this.cboMCPNumSeasons.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMCPNumSeasons.FormattingEnabled = true;
            this.cboMCPNumSeasons.Items.AddRange(new object[] {
            "1",
            "4"});
            this.cboMCPNumSeasons.Location = new System.Drawing.Point(1116, 201);
            this.cboMCPNumSeasons.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCPNumSeasons.Name = "cboMCPNumSeasons";
            this.cboMCPNumSeasons.Size = new System.Drawing.Size(48, 26);
            this.cboMCPNumSeasons.TabIndex = 274;
            this.cboMCPNumSeasons.Text = "1";
            this.cboMCPNumSeasons.SelectedIndexChanged += new System.EventHandler(this.cboMCPNumSeasons_SelectedIndexChanged);
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label136.Location = new System.Drawing.Point(989, 209);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(121, 18);
            this.label136.TabIndex = 273;
            this.label136.Text = "Num. Season bins :";
            // 
            // cboMCPNumHours
            // 
            this.cboMCPNumHours.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMCPNumHours.FormattingEnabled = true;
            this.cboMCPNumHours.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cboMCPNumHours.Location = new System.Drawing.Point(1146, 153);
            this.cboMCPNumHours.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCPNumHours.Name = "cboMCPNumHours";
            this.cboMCPNumHours.Size = new System.Drawing.Size(48, 26);
            this.cboMCPNumHours.TabIndex = 272;
            this.cboMCPNumHours.Text = "1";
            this.cboMCPNumHours.SelectedIndexChanged += new System.EventHandler(this.cboMCPNumHours_SelectedIndexChanged);
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label137.Location = new System.Drawing.Point(989, 161);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(151, 18);
            this.label137.TabIndex = 271;
            this.label137.Text = "Num. Time of Day bins :";
            // 
            // txtWS_bin_width
            // 
            this.txtWS_bin_width.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWS_bin_width.Location = new System.Drawing.Point(1396, 133);
            this.txtWS_bin_width.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWS_bin_width.Name = "txtWS_bin_width";
            this.txtWS_bin_width.Size = new System.Drawing.Size(35, 25);
            this.txtWS_bin_width.TabIndex = 270;
            this.txtWS_bin_width.Text = "1";
            this.txtWS_bin_width.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWS_bin_width.TextChanged += new System.EventHandler(this.txtWS_bin_width_TextChanged);
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label138.Location = new System.Drawing.Point(1259, 136);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(131, 18);
            this.label138.TabIndex = 269;
            this.label138.Text = "WS bin width (m/s) :";
            // 
            // cboMCPNumWD
            // 
            this.cboMCPNumWD.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMCPNumWD.FormattingEnabled = true;
            this.cboMCPNumWD.Items.AddRange(new object[] {
            "4",
            "8",
            "12",
            "16",
            "24"});
            this.cboMCPNumWD.Location = new System.Drawing.Point(1098, 109);
            this.cboMCPNumWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCPNumWD.Name = "cboMCPNumWD";
            this.cboMCPNumWD.Size = new System.Drawing.Size(48, 26);
            this.cboMCPNumWD.TabIndex = 268;
            this.cboMCPNumWD.Text = "16";
            this.cboMCPNumWD.SelectedIndexChanged += new System.EventHandler(this.cboMCPNumWD_SelectedIndexChanged);
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label139.Location = new System.Drawing.Point(989, 117);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(103, 18);
            this.label139.TabIndex = 267;
            this.label139.Text = "Num. WD bins :";
            // 
            // cboMCP_Type
            // 
            this.cboMCP_Type.FormattingEnabled = true;
            this.cboMCP_Type.Items.AddRange(new object[] {
            "Orth. Regression",
            "Method of Bins",
            "Variance Ratio",
            "Matrix"});
            this.cboMCP_Type.Location = new System.Drawing.Point(1084, 57);
            this.cboMCP_Type.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCP_Type.Name = "cboMCP_Type";
            this.cboMCP_Type.Size = new System.Drawing.Size(180, 26);
            this.cboMCP_Type.TabIndex = 266;
            this.cboMCP_Type.Text = "Orth. Regression";
            this.cboMCP_Type.SelectedIndexChanged += new System.EventHandler(this.cboMCP_Type_SelectedIndexChanged);
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label133.Location = new System.Drawing.Point(985, 25);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(129, 23);
            this.label133.TabIndex = 265;
            this.label133.Text = "MCP Settings";
            // 
            // txtNumYrsConc
            // 
            this.txtNumYrsConc.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumYrsConc.Location = new System.Drawing.Point(128, 258);
            this.txtNumYrsConc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNumYrsConc.Name = "txtNumYrsConc";
            this.txtNumYrsConc.ReadOnly = true;
            this.txtNumYrsConc.Size = new System.Drawing.Size(38, 25);
            this.txtNumYrsConc.TabIndex = 264;
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label132.Location = new System.Drawing.Point(130, 236);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(41, 18);
            this.label132.TabIndex = 263;
            this.label132.Text = "Conc.";
            // 
            // txtRsq
            // 
            this.txtRsq.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRsq.Location = new System.Drawing.Point(160, 156);
            this.txtRsq.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRsq.Name = "txtRsq";
            this.txtRsq.ReadOnly = true;
            this.txtRsq.Size = new System.Drawing.Size(54, 25);
            this.txtRsq.TabIndex = 260;
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label130.Location = new System.Drawing.Point(172, 132);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(28, 18);
            this.label130.TabIndex = 259;
            this.label130.Text = "R² :";
            // 
            // btnExportBinRatios
            // 
            this.btnExportBinRatios.Location = new System.Drawing.Point(68, 763);
            this.btnExportBinRatios.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportBinRatios.Name = "btnExportBinRatios";
            this.btnExportBinRatios.Size = new System.Drawing.Size(113, 58);
            this.btnExportBinRatios.TabIndex = 255;
            this.btnExportBinRatios.Text = "Export WS Bin Ratios";
            this.btnExportBinRatios.UseVisualStyleBackColor = true;
            this.btnExportBinRatios.Click += new System.EventHandler(this.btnExportBinRatios_Click);
            // 
            // txtIntercept
            // 
            this.txtIntercept.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntercept.Location = new System.Drawing.Point(98, 156);
            this.txtIntercept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtIntercept.Name = "txtIntercept";
            this.txtIntercept.ReadOnly = true;
            this.txtIntercept.Size = new System.Drawing.Size(54, 25);
            this.txtIntercept.TabIndex = 253;
            // 
            // txtSlope
            // 
            this.txtSlope.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlope.Location = new System.Drawing.Point(35, 156);
            this.txtSlope.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSlope.Name = "txtSlope";
            this.txtSlope.ReadOnly = true;
            this.txtSlope.Size = new System.Drawing.Size(54, 25);
            this.txtSlope.TabIndex = 252;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label123.Location = new System.Drawing.Point(96, 132);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(65, 18);
            this.label123.TabIndex = 250;
            this.label123.Text = "Intercept:";
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label124.Location = new System.Drawing.Point(33, 132);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(43, 18);
            this.label124.TabIndex = 249;
            this.label124.Text = "Slope:";
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label125.Location = new System.Drawing.Point(34, 331);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(192, 19);
            this.label125.TabIndex = 248;
            this.label125.Text = "Avg && SD WS Ratios (T/R)";
            // 
            // lstMCP_Bins
            // 
            this.lstMCP_Bins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.WS,
            this.Mean,
            this.SD,
            this.Count});
            this.lstMCP_Bins.HideSelection = false;
            this.lstMCP_Bins.Location = new System.Drawing.Point(24, 356);
            this.lstMCP_Bins.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lstMCP_Bins.Name = "lstMCP_Bins";
            this.lstMCP_Bins.Size = new System.Drawing.Size(220, 355);
            this.lstMCP_Bins.TabIndex = 247;
            this.lstMCP_Bins.UseCompatibleStateImageBehavior = false;
            this.lstMCP_Bins.View = System.Windows.Forms.View.Details;
            // 
            // WS
            // 
            this.WS.Text = "WS";
            this.WS.Width = 44;
            // 
            // Mean
            // 
            this.Mean.Text = "Mean";
            this.Mean.Width = 50;
            // 
            // SD
            // 
            this.SD.Text = "SD";
            this.SD.Width = 44;
            // 
            // Count
            // 
            this.Count.Text = "Count";
            this.Count.Width = 66;
            // 
            // lblRegStats
            // 
            this.lblRegStats.AutoSize = true;
            this.lblRegStats.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegStats.Location = new System.Drawing.Point(33, 98);
            this.lblRegStats.Name = "lblRegStats";
            this.lblRegStats.Size = new System.Drawing.Size(209, 23);
            this.lblRegStats.TabIndex = 246;
            this.lblRegStats.Text = "MCP Regression Stats.";
            // 
            // txtNumYrsTarg
            // 
            this.txtNumYrsTarg.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumYrsTarg.Location = new System.Drawing.Point(83, 258);
            this.txtNumYrsTarg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNumYrsTarg.Name = "txtNumYrsTarg";
            this.txtNumYrsTarg.ReadOnly = true;
            this.txtNumYrsTarg.Size = new System.Drawing.Size(38, 25);
            this.txtNumYrsTarg.TabIndex = 245;
            // 
            // txtNumYrsRef
            // 
            this.txtNumYrsRef.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumYrsRef.Location = new System.Drawing.Point(37, 258);
            this.txtNumYrsRef.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNumYrsRef.Name = "txtNumYrsRef";
            this.txtNumYrsRef.ReadOnly = true;
            this.txtNumYrsRef.Size = new System.Drawing.Size(38, 25);
            this.txtNumYrsRef.TabIndex = 244;
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label127.Location = new System.Drawing.Point(86, 236);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(38, 18);
            this.label127.TabIndex = 243;
            this.label127.Text = "Targ.";
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label128.Location = new System.Drawing.Point(37, 236);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(33, 18);
            this.label128.TabIndex = 242;
            this.label128.Text = "Ref. ";
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label129.Location = new System.Drawing.Point(42, 215);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(116, 16);
            this.label129.TabIndex = 241;
            this.label129.Text = "Number of Years";
            // 
            // btnExportMCP_TAB
            // 
            this.btnExportMCP_TAB.Location = new System.Drawing.Point(666, 764);
            this.btnExportMCP_TAB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportMCP_TAB.Name = "btnExportMCP_TAB";
            this.btnExportMCP_TAB.Size = new System.Drawing.Size(113, 66);
            this.btnExportMCP_TAB.TabIndex = 240;
            this.btnExportMCP_TAB.Text = "Export Estimated data as TAB file\r\n";
            this.btnExportMCP_TAB.UseVisualStyleBackColor = true;
            this.btnExportMCP_TAB.Click += new System.EventHandler(this.btnExportMCP_TAB_Click);
            // 
            // btnExportMCP_TS
            // 
            this.btnExportMCP_TS.Location = new System.Drawing.Point(539, 764);
            this.btnExportMCP_TS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportMCP_TS.Name = "btnExportMCP_TS";
            this.btnExportMCP_TS.Size = new System.Drawing.Size(113, 66);
            this.btnExportMCP_TS.TabIndex = 239;
            this.btnExportMCP_TS.Text = "Export Estimated data as Time Series";
            this.btnExportMCP_TS.UseVisualStyleBackColor = true;
            this.btnExportMCP_TS.Click += new System.EventHandler(this.btnExportMCP_TS_Click);
            // 
            // cboUncertStep
            // 
            this.cboUncertStep.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUncertStep.FormattingEnabled = true;
            this.cboUncertStep.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cboUncertStep.Location = new System.Drawing.Point(1148, 340);
            this.cboUncertStep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboUncertStep.Name = "cboUncertStep";
            this.cboUncertStep.Size = new System.Drawing.Size(48, 26);
            this.cboUncertStep.TabIndex = 238;
            this.cboUncertStep.Text = "1";
            this.cboUncertStep.SelectedIndexChanged += new System.EventHandler(this.cboUncertStep_SelectedIndexChanged);
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label119.Location = new System.Drawing.Point(1004, 332);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(140, 36);
            this.label119.TabIndex = 237;
            this.label119.Text = "Uncertainty Window \r\nStep (months):";
            // 
            // lstMCP_Uncert
            // 
            this.lstMCP_Uncert.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Window,
            this.AVG,
            this.SDU});
            this.lstMCP_Uncert.HideSelection = false;
            this.lstMCP_Uncert.Location = new System.Drawing.Point(1451, 401);
            this.lstMCP_Uncert.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lstMCP_Uncert.Name = "lstMCP_Uncert";
            this.lstMCP_Uncert.Size = new System.Drawing.Size(161, 288);
            this.lstMCP_Uncert.TabIndex = 236;
            this.lstMCP_Uncert.UseCompatibleStateImageBehavior = false;
            this.lstMCP_Uncert.View = System.Windows.Forms.View.Details;
            // 
            // Window
            // 
            this.Window.Text = "Window";
            this.Window.Width = 51;
            // 
            // AVG
            // 
            this.AVG.Text = "Mean";
            this.AVG.Width = 44;
            // 
            // SDU
            // 
            this.SDU.Text = "SD";
            this.SDU.Width = 40;
            // 
            // btnExportMCPUncert
            // 
            this.btnExportMCPUncert.Location = new System.Drawing.Point(1479, 721);
            this.btnExportMCPUncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportMCPUncert.Name = "btnExportMCPUncert";
            this.btnExportMCPUncert.Size = new System.Drawing.Size(113, 66);
            this.btnExportMCPUncert.TabIndex = 235;
            this.btnExportMCPUncert.Text = "Export Uncertainty Analysis\r\n\r\n";
            this.btnExportMCPUncert.UseVisualStyleBackColor = true;
            this.btnExportMCPUncert.Click += new System.EventHandler(this.btnExportMCPUncert_Click);
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label118.Location = new System.Drawing.Point(988, 286);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(244, 23);
            this.label118.TabIndex = 234;
            this.label118.Text = "MCP Uncertainty Analysis";
            // 
            // btnMCP_Uncert
            // 
            this.btnMCP_Uncert.Location = new System.Drawing.Point(1245, 318);
            this.btnMCP_Uncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMCP_Uncert.Name = "btnMCP_Uncert";
            this.btnMCP_Uncert.Size = new System.Drawing.Size(113, 69);
            this.btnMCP_Uncert.TabIndex = 233;
            this.btnMCP_Uncert.Text = "Do MCP Uncertainty Analysis";
            this.btnMCP_Uncert.UseVisualStyleBackColor = true;
            this.btnMCP_Uncert.Click += new System.EventHandler(this.btnMCP_Uncert_Click);
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.Location = new System.Drawing.Point(743, 214);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(78, 18);
            this.label108.TabIndex = 232;
            this.label108.Text = "Season Bin :";
            // 
            // cboMCP_Season
            // 
            this.cboMCP_Season.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMCP_Season.FormattingEnabled = true;
            this.cboMCP_Season.Items.AddRange(new object[] {
            "All"});
            this.cboMCP_Season.Location = new System.Drawing.Point(743, 240);
            this.cboMCP_Season.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCP_Season.Name = "cboMCP_Season";
            this.cboMCP_Season.Size = new System.Drawing.Size(110, 26);
            this.cboMCP_Season.TabIndex = 231;
            this.cboMCP_Season.Text = "All";
            this.cboMCP_Season.SelectedIndexChanged += new System.EventHandler(this.cboMCP_Season_SelectedIndexChanged);
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label109.Location = new System.Drawing.Point(453, 214);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(85, 18);
            this.label109.TabIndex = 230;
            this.label109.Text = "Time of Day :";
            // 
            // cboMCP_TOD
            // 
            this.cboMCP_TOD.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMCP_TOD.FormattingEnabled = true;
            this.cboMCP_TOD.Items.AddRange(new object[] {
            "All"});
            this.cboMCP_TOD.Location = new System.Drawing.Point(450, 241);
            this.cboMCP_TOD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCP_TOD.Name = "cboMCP_TOD";
            this.cboMCP_TOD.Size = new System.Drawing.Size(115, 26);
            this.cboMCP_TOD.TabIndex = 229;
            this.cboMCP_TOD.Text = "All";
            this.cboMCP_TOD.SelectedIndexChanged += new System.EventHandler(this.cboMCP_TOD_SelectedIndexChanged);
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label110.Location = new System.Drawing.Point(429, 98);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(435, 23);
            this.label110.TabIndex = 226;
            this.label110.Text = "Mean Concurrent and Long-term Wind Speeds";
            // 
            // txtLTratio
            // 
            this.txtLTratio.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLTratio.Location = new System.Drawing.Point(748, 177);
            this.txtLTratio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLTratio.Name = "txtLTratio";
            this.txtLTratio.ReadOnly = true;
            this.txtLTratio.Size = new System.Drawing.Size(70, 25);
            this.txtLTratio.TabIndex = 225;
            // 
            // txtAvgRatio
            // 
            this.txtAvgRatio.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvgRatio.Location = new System.Drawing.Point(748, 148);
            this.txtAvgRatio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAvgRatio.Name = "txtAvgRatio";
            this.txtAvgRatio.ReadOnly = true;
            this.txtAvgRatio.Size = new System.Drawing.Size(70, 25);
            this.txtAvgRatio.TabIndex = 224;
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.Location = new System.Drawing.Point(749, 126);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(67, 18);
            this.label111.TabIndex = 223;
            this.label111.Text = "Ratio: T/R\r\n";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label112.Location = new System.Drawing.Point(840, 125);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(45, 18);
            this.label112.TabIndex = 222;
            this.label112.Text = "Count";
            // 
            // txtDataCount
            // 
            this.txtDataCount.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataCount.Location = new System.Drawing.Point(829, 148);
            this.txtDataCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDataCount.Name = "txtDataCount";
            this.txtDataCount.ReadOnly = true;
            this.txtDataCount.Size = new System.Drawing.Size(67, 25);
            this.txtDataCount.TabIndex = 221;
            // 
            // txtTarg_LT_WS
            // 
            this.txtTarg_LT_WS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTarg_LT_WS.Location = new System.Drawing.Point(662, 178);
            this.txtTarg_LT_WS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTarg_LT_WS.Name = "txtTarg_LT_WS";
            this.txtTarg_LT_WS.ReadOnly = true;
            this.txtTarg_LT_WS.Size = new System.Drawing.Size(70, 25);
            this.txtTarg_LT_WS.TabIndex = 220;
            // 
            // txtRef_LT_WS
            // 
            this.txtRef_LT_WS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRef_LT_WS.Location = new System.Drawing.Point(579, 178);
            this.txtRef_LT_WS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRef_LT_WS.Name = "txtRef_LT_WS";
            this.txtRef_LT_WS.ReadOnly = true;
            this.txtRef_LT_WS.Size = new System.Drawing.Size(70, 25);
            this.txtRef_LT_WS.TabIndex = 219;
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label113.Location = new System.Drawing.Point(460, 180);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(110, 18);
            this.label113.TabIndex = 218;
            this.label113.Text = "Avg. LT WS (m/s)";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.Location = new System.Drawing.Point(597, 214);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(77, 18);
            this.label114.TabIndex = 217;
            this.label114.Text = "WD Sector :";
            // 
            // cboMCP_WD
            // 
            this.cboMCP_WD.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMCP_WD.FormattingEnabled = true;
            this.cboMCP_WD.Items.AddRange(new object[] {
            "All"});
            this.cboMCP_WD.Location = new System.Drawing.Point(601, 240);
            this.cboMCP_WD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCP_WD.Name = "cboMCP_WD";
            this.cboMCP_WD.Size = new System.Drawing.Size(118, 26);
            this.cboMCP_WD.TabIndex = 216;
            this.cboMCP_WD.Text = "All";
            this.cboMCP_WD.SelectedIndexChanged += new System.EventHandler(this.cboMCP_WD_SelectedIndexChanged);
            // 
            // txtTargAvgWS
            // 
            this.txtTargAvgWS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTargAvgWS.Location = new System.Drawing.Point(662, 148);
            this.txtTargAvgWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTargAvgWS.Name = "txtTargAvgWS";
            this.txtTargAvgWS.ReadOnly = true;
            this.txtTargAvgWS.Size = new System.Drawing.Size(70, 25);
            this.txtTargAvgWS.TabIndex = 215;
            // 
            // txtRefAvgWS
            // 
            this.txtRefAvgWS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefAvgWS.Location = new System.Drawing.Point(579, 148);
            this.txtRefAvgWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRefAvgWS.Name = "txtRefAvgWS";
            this.txtRefAvgWS.ReadOnly = true;
            this.txtRefAvgWS.Size = new System.Drawing.Size(70, 25);
            this.txtRefAvgWS.TabIndex = 214;
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label115.Location = new System.Drawing.Point(437, 148);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(128, 18);
            this.label115.TabIndex = 213;
            this.label115.Text = "Avg. Conc. WS (m/s)";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label116.Location = new System.Drawing.Point(661, 126);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(71, 18);
            this.label116.TabIndex = 212;
            this.label116.Text = "Target Site";
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.Location = new System.Drawing.Point(586, 126);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(55, 18);
            this.label117.TabIndex = 211;
            this.label117.Text = "Ref. Site";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(31, 62);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(88, 18);
            this.label107.TabIndex = 208;
            this.label107.Text = "Selected Met :";
            // 
            // cboMCP_Met
            // 
            this.cboMCP_Met.FormattingEnabled = true;
            this.cboMCP_Met.Location = new System.Drawing.Point(127, 58);
            this.cboMCP_Met.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCP_Met.Name = "cboMCP_Met";
            this.cboMCP_Met.Size = new System.Drawing.Size(233, 26);
            this.cboMCP_Met.TabIndex = 207;
            this.cboMCP_Met.SelectedIndexChanged += new System.EventHandler(this.cboMCP_Met_SelectedIndexChanged);
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label106.Location = new System.Drawing.Point(20, 17);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(162, 25);
            this.label106.TabIndex = 2;
            this.label106.Text = "MCP Summary";
            // 
            // pgeMetSumm
            // 
            this.pgeMetSumm.Controls.Add(this.plotDW_DH);
            this.pgeMetSumm.Controls.Add(this.plotUW_DH);
            this.pgeMetSumm.Controls.Add(this.plotDW_SR);
            this.pgeMetSumm.Controls.Add(this.plotUW_SR);
            this.pgeMetSumm.Controls.Add(this.plotElev);
            this.pgeMetSumm.Controls.Add(this.plotDWUWExpo);
            this.pgeMetSumm.Controls.Add(this.plotDWExpo);
            this.pgeMetSumm.Controls.Add(this.plotUWExpo);
            this.pgeMetSumm.Controls.Add(this.cboSummSeason);
            this.pgeMetSumm.Controls.Add(this.cboSummTOD);
            this.pgeMetSumm.Controls.Add(this.Label80);
            this.pgeMetSumm.Controls.Add(this.lstTurbStats);
            this.pgeMetSumm.Controls.Add(this.chkTurbSummAll);
            this.pgeMetSumm.Controls.Add(this.chkTurbSumm);
            this.pgeMetSumm.Controls.Add(this.Label78);
            this.pgeMetSumm.Controls.Add(this.chkMetSummAll);
            this.pgeMetSumm.Controls.Add(this.Label12);
            this.pgeMetSumm.Controls.Add(this.chkMetSumm);
            this.pgeMetSumm.Controls.Add(this.Label76);
            this.pgeMetSumm.Controls.Add(this.lstMetStats);
            this.pgeMetSumm.Controls.Add(this.btnExportExpoSRDH);
            this.pgeMetSumm.Controls.Add(this.Label74);
            this.pgeMetSumm.Controls.Add(this.cboSummaryWD);
            this.pgeMetSumm.Controls.Add(this.Label75);
            this.pgeMetSumm.Controls.Add(this.cboMetSum_Rad);
            this.pgeMetSumm.Controls.Add(this.lstMetSummary);
            this.pgeMetSumm.Controls.Add(this.Label73);
            this.pgeMetSumm.Location = new System.Drawing.Point(4, 27);
            this.pgeMetSumm.Margin = new System.Windows.Forms.Padding(1);
            this.pgeMetSumm.Name = "pgeMetSumm";
            this.pgeMetSumm.Size = new System.Drawing.Size(1627, 849);
            this.pgeMetSumm.TabIndex = 12;
            this.pgeMetSumm.Text = "Met && Turbine Summary";
            this.pgeMetSumm.UseVisualStyleBackColor = true;
            // 
            // plotDW_DH
            // 
            this.plotDW_DH.Location = new System.Drawing.Point(1228, 552);
            this.plotDW_DH.Name = "plotDW_DH";
            this.plotDW_DH.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDW_DH.Size = new System.Drawing.Size(387, 258);
            this.plotDW_DH.TabIndex = 221;
            this.plotDW_DH.Text = "plotView1";
            this.plotDW_DH.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDW_DH.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDW_DH.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotUW_DH
            // 
            this.plotUW_DH.Location = new System.Drawing.Point(826, 552);
            this.plotUW_DH.Name = "plotUW_DH";
            this.plotUW_DH.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotUW_DH.Size = new System.Drawing.Size(387, 258);
            this.plotUW_DH.TabIndex = 220;
            this.plotUW_DH.Text = "plotView1";
            this.plotUW_DH.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotUW_DH.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotUW_DH.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotDW_SR
            // 
            this.plotDW_SR.Location = new System.Drawing.Point(423, 552);
            this.plotDW_SR.Name = "plotDW_SR";
            this.plotDW_SR.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDW_SR.Size = new System.Drawing.Size(387, 258);
            this.plotDW_SR.TabIndex = 219;
            this.plotDW_SR.Text = "plotView1";
            this.plotDW_SR.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDW_SR.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDW_SR.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotUW_SR
            // 
            this.plotUW_SR.Location = new System.Drawing.Point(20, 552);
            this.plotUW_SR.Name = "plotUW_SR";
            this.plotUW_SR.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotUW_SR.Size = new System.Drawing.Size(387, 258);
            this.plotUW_SR.TabIndex = 218;
            this.plotUW_SR.Text = "plotView1";
            this.plotUW_SR.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotUW_SR.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotUW_SR.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotElev
            // 
            this.plotElev.Location = new System.Drawing.Point(1228, 263);
            this.plotElev.Name = "plotElev";
            this.plotElev.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotElev.Size = new System.Drawing.Size(387, 258);
            this.plotElev.TabIndex = 217;
            this.plotElev.Text = "plotView1";
            this.plotElev.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotElev.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotElev.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotDWUWExpo
            // 
            this.plotDWUWExpo.Location = new System.Drawing.Point(826, 263);
            this.plotDWUWExpo.Name = "plotDWUWExpo";
            this.plotDWUWExpo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDWUWExpo.Size = new System.Drawing.Size(387, 258);
            this.plotDWUWExpo.TabIndex = 216;
            this.plotDWUWExpo.Text = "plotView1";
            this.plotDWUWExpo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDWUWExpo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDWUWExpo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotDWExpo
            // 
            this.plotDWExpo.Location = new System.Drawing.Point(423, 264);
            this.plotDWExpo.Name = "plotDWExpo";
            this.plotDWExpo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDWExpo.Size = new System.Drawing.Size(387, 258);
            this.plotDWExpo.TabIndex = 215;
            this.plotDWExpo.Text = "plotView1";
            this.plotDWExpo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDWExpo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDWExpo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotUWExpo
            // 
            this.plotUWExpo.Location = new System.Drawing.Point(20, 264);
            this.plotUWExpo.Name = "plotUWExpo";
            this.plotUWExpo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotUWExpo.Size = new System.Drawing.Size(387, 258);
            this.plotUWExpo.TabIndex = 214;
            this.plotUWExpo.Text = "plotView1";
            this.plotUWExpo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotUWExpo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotUWExpo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // cboSummSeason
            // 
            this.cboSummSeason.FormattingEnabled = true;
            this.cboSummSeason.Location = new System.Drawing.Point(309, 42);
            this.cboSummSeason.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSummSeason.Name = "cboSummSeason";
            this.cboSummSeason.Size = new System.Drawing.Size(91, 26);
            this.cboSummSeason.TabIndex = 213;
            this.cboSummSeason.Text = "All Seasons";
            this.cboSummSeason.SelectedIndexChanged += new System.EventHandler(this.cboSummSeason_SelectedIndexChanged);
            // 
            // cboSummTOD
            // 
            this.cboSummTOD.FormattingEnabled = true;
            this.cboSummTOD.Location = new System.Drawing.Point(310, 9);
            this.cboSummTOD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSummTOD.Name = "cboSummTOD";
            this.cboSummTOD.Size = new System.Drawing.Size(91, 26);
            this.cboSummTOD.TabIndex = 212;
            this.cboSummTOD.Text = "All Hours";
            this.cboSummTOD.SelectedIndexChanged += new System.EventHandler(this.cboSummTOD_SelectedIndexChanged);
            // 
            // Label80
            // 
            this.Label80.AutoSize = true;
            this.Label80.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label80.Location = new System.Drawing.Point(1308, 10);
            this.Label80.Name = "Label80";
            this.Label80.Size = new System.Drawing.Size(242, 19);
            this.Label80.TabIndex = 175;
            this.Label80.Text = "Turbine Site Statistics (selected)";
            // 
            // lstTurbStats
            // 
            this.lstTurbStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader21,
            this.ColumnHeader34,
            this.ColumnHeader35,
            this.ColumnHeader22,
            this.ColumnHeader33});
            this.lstTurbStats.HideSelection = false;
            this.lstTurbStats.Location = new System.Drawing.Point(1312, 38);
            this.lstTurbStats.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstTurbStats.MultiSelect = false;
            this.lstTurbStats.Name = "lstTurbStats";
            this.lstTurbStats.Size = new System.Drawing.Size(283, 221);
            this.lstTurbStats.TabIndex = 174;
            this.lstTurbStats.UseCompatibleStateImageBehavior = false;
            this.lstTurbStats.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader21
            // 
            this.ColumnHeader21.Text = "";
            this.ColumnHeader21.Width = 57;
            // 
            // ColumnHeader34
            // 
            this.ColumnHeader34.Text = "Avg.";
            this.ColumnHeader34.Width = 45;
            // 
            // ColumnHeader35
            // 
            this.ColumnHeader35.Text = "SD";
            this.ColumnHeader35.Width = 45;
            // 
            // ColumnHeader22
            // 
            this.ColumnHeader22.Text = "Min.";
            this.ColumnHeader22.Width = 47;
            // 
            // ColumnHeader33
            // 
            this.ColumnHeader33.Text = "Max.";
            this.ColumnHeader33.Width = 38;
            // 
            // chkTurbSummAll
            // 
            this.chkTurbSummAll.AutoSize = true;
            this.chkTurbSummAll.Checked = true;
            this.chkTurbSummAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTurbSummAll.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTurbSummAll.Location = new System.Drawing.Point(918, 28);
            this.chkTurbSummAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbSummAll.Name = "chkTurbSummAll";
            this.chkTurbSummAll.Size = new System.Drawing.Size(89, 36);
            this.chkTurbSummAll.TabIndex = 173;
            this.chkTurbSummAll.Text = "Select/\r\nDeselect All";
            this.chkTurbSummAll.UseVisualStyleBackColor = true;
            this.chkTurbSummAll.CheckedChanged += new System.EventHandler(this.chkTurbSummAll_CheckedChanged);
            // 
            // chkTurbSumm
            // 
            this.chkTurbSumm.CheckOnClick = true;
            this.chkTurbSumm.FormattingEnabled = true;
            this.chkTurbSumm.HorizontalScrollbar = true;
            this.chkTurbSumm.Location = new System.Drawing.Point(915, 72);
            this.chkTurbSumm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbSumm.Name = "chkTurbSumm";
            this.chkTurbSumm.Size = new System.Drawing.Size(91, 184);
            this.chkTurbSumm.TabIndex = 172;
            this.chkTurbSumm.SelectedIndexChanged += new System.EventHandler(this.chkTurbSumm_SelectedIndexChanged);
            // 
            // Label78
            // 
            this.Label78.AutoSize = true;
            this.Label78.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label78.Location = new System.Drawing.Point(910, 7);
            this.Label78.Name = "Label78";
            this.Label78.Size = new System.Drawing.Size(89, 16);
            this.Label78.TabIndex = 171;
            this.Label78.Text = "Turbine Sites";
            // 
            // chkMetSummAll
            // 
            this.chkMetSummAll.AutoSize = true;
            this.chkMetSummAll.Checked = true;
            this.chkMetSummAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMetSummAll.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMetSummAll.Location = new System.Drawing.Point(813, 28);
            this.chkMetSummAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetSummAll.Name = "chkMetSummAll";
            this.chkMetSummAll.Size = new System.Drawing.Size(89, 36);
            this.chkMetSummAll.TabIndex = 170;
            this.chkMetSummAll.Text = "Select/\r\nDeselect All";
            this.chkMetSummAll.UseVisualStyleBackColor = true;
            this.chkMetSummAll.CheckedChanged += new System.EventHandler(this.chkMetSummAll_CheckedChanged);
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(812, 8);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(65, 16);
            this.Label12.TabIndex = 169;
            this.Label12.Text = "Met Sites";
            // 
            // chkMetSumm
            // 
            this.chkMetSumm.CheckOnClick = true;
            this.chkMetSumm.FormattingEnabled = true;
            this.chkMetSumm.HorizontalScrollbar = true;
            this.chkMetSumm.Location = new System.Drawing.Point(803, 73);
            this.chkMetSumm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetSumm.Name = "chkMetSumm";
            this.chkMetSumm.Size = new System.Drawing.Size(101, 184);
            this.chkMetSumm.TabIndex = 168;
            this.chkMetSumm.SelectedIndexChanged += new System.EventHandler(this.chkMetSumm_SelectedIndexChanged);
            // 
            // Label76
            // 
            this.Label76.AutoSize = true;
            this.Label76.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label76.Location = new System.Drawing.Point(1013, 10);
            this.Label76.Name = "Label76";
            this.Label76.Size = new System.Drawing.Size(213, 19);
            this.Label76.TabIndex = 166;
            this.Label76.Text = "Met Site Statistics (selected)";
            // 
            // lstMetStats
            // 
            this.lstMetStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader42,
            this.ColumnHeader91,
            this.ColumnHeader93,
            this.ColumnHeader92,
            this.ColumnHeader52});
            this.lstMetStats.HideSelection = false;
            this.lstMetStats.Location = new System.Drawing.Point(1016, 38);
            this.lstMetStats.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstMetStats.MultiSelect = false;
            this.lstMetStats.Name = "lstMetStats";
            this.lstMetStats.Size = new System.Drawing.Size(290, 221);
            this.lstMetStats.TabIndex = 165;
            this.lstMetStats.UseCompatibleStateImageBehavior = false;
            this.lstMetStats.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader42
            // 
            this.ColumnHeader42.Text = "";
            this.ColumnHeader42.Width = 54;
            // 
            // ColumnHeader91
            // 
            this.ColumnHeader91.Text = "Avg.";
            this.ColumnHeader91.Width = 46;
            // 
            // ColumnHeader93
            // 
            this.ColumnHeader93.Text = "SD";
            this.ColumnHeader93.Width = 42;
            // 
            // ColumnHeader92
            // 
            this.ColumnHeader92.Text = "Min.";
            this.ColumnHeader92.Width = 42;
            // 
            // ColumnHeader52
            // 
            this.ColumnHeader52.Text = "Max.";
            this.ColumnHeader52.Width = 42;
            // 
            // btnExportExpoSRDH
            // 
            this.btnExportExpoSRDH.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExpoSRDH.Location = new System.Drawing.Point(668, 9);
            this.btnExportExpoSRDH.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportExpoSRDH.Name = "btnExportExpoSRDH";
            this.btnExportExpoSRDH.Size = new System.Drawing.Size(129, 49);
            this.btnExportExpoSRDH.TabIndex = 164;
            this.btnExportExpoSRDH.Text = "Export Calculated Values";
            this.btnExportExpoSRDH.UseVisualStyleBackColor = true;
            this.btnExportExpoSRDH.Click += new System.EventHandler(this.btnExportExpoSRDH_Click);
            // 
            // Label74
            // 
            this.Label74.AutoSize = true;
            this.Label74.Location = new System.Drawing.Point(552, 9);
            this.Label74.Name = "Label74";
            this.Label74.Size = new System.Drawing.Size(79, 19);
            this.Label74.TabIndex = 163;
            this.Label74.Text = "WD sector:";
            // 
            // cboSummaryWD
            // 
            this.cboSummaryWD.FormattingEnabled = true;
            this.cboSummaryWD.Location = new System.Drawing.Point(546, 31);
            this.cboSummaryWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSummaryWD.Name = "cboSummaryWD";
            this.cboSummaryWD.Size = new System.Drawing.Size(115, 26);
            this.cboSummaryWD.TabIndex = 162;
            this.cboSummaryWD.SelectedIndexChanged += new System.EventHandler(this.cboMetSum_WD_SelectedIndexChanged);
            // 
            // Label75
            // 
            this.Label75.AutoSize = true;
            this.Label75.Location = new System.Drawing.Point(429, 9);
            this.Label75.Name = "Label75";
            this.Label75.Size = new System.Drawing.Size(60, 19);
            this.Label75.TabIndex = 161;
            this.Label75.Text = "Radius :";
            // 
            // cboMetSum_Rad
            // 
            this.cboMetSum_Rad.FormattingEnabled = true;
            this.cboMetSum_Rad.Location = new System.Drawing.Point(423, 31);
            this.cboMetSum_Rad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMetSum_Rad.Name = "cboMetSum_Rad";
            this.cboMetSum_Rad.Size = new System.Drawing.Size(115, 26);
            this.cboMetSum_Rad.TabIndex = 160;
            this.cboMetSum_Rad.SelectedIndexChanged += new System.EventHandler(this.cboMetSum_Rad_SelectedIndexChanged);
            // 
            // lstMetSummary
            // 
            this.lstMetSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader37,
            this.ColumnHeader40,
            this.ColumnHeader41,
            this.ColumnHeader43,
            this.ColumnHeader44,
            this.ColumnHeader46,
            this.ColumnHeader47,
            this.ColumnHeader48,
            this.ColumnHeader49,
            this.ColumnHeader50,
            this.ColumnHeader51});
            this.lstMetSummary.HideSelection = false;
            this.lstMetSummary.Location = new System.Drawing.Point(10, 75);
            this.lstMetSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstMetSummary.MultiSelect = false;
            this.lstMetSummary.Name = "lstMetSummary";
            this.lstMetSummary.Size = new System.Drawing.Size(787, 182);
            this.lstMetSummary.TabIndex = 31;
            this.lstMetSummary.UseCompatibleStateImageBehavior = false;
            this.lstMetSummary.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader37
            // 
            this.ColumnHeader37.Text = "Name";
            this.ColumnHeader37.Width = 62;
            // 
            // ColumnHeader40
            // 
            this.ColumnHeader40.Text = "Elev., m";
            this.ColumnHeader40.Width = 40;
            // 
            // ColumnHeader41
            // 
            this.ColumnHeader41.Text = "WS, m/s";
            // 
            // ColumnHeader43
            // 
            this.ColumnHeader43.Text = "Weibull k";
            this.ColumnHeader43.Width = 57;
            // 
            // ColumnHeader44
            // 
            this.ColumnHeader44.Text = "Weibull A";
            this.ColumnHeader44.Width = 59;
            // 
            // ColumnHeader46
            // 
            this.ColumnHeader46.Text = "UW Expo";
            this.ColumnHeader46.Width = 62;
            // 
            // ColumnHeader47
            // 
            this.ColumnHeader47.Text = "DW Expo";
            this.ColumnHeader47.Width = 66;
            // 
            // ColumnHeader48
            // 
            this.ColumnHeader48.Text = "UW Rough";
            this.ColumnHeader48.Width = 72;
            // 
            // ColumnHeader49
            // 
            this.ColumnHeader49.Text = "DW Rough";
            this.ColumnHeader49.Width = 74;
            // 
            // ColumnHeader50
            // 
            this.ColumnHeader50.Text = "UW Disp H";
            this.ColumnHeader50.Width = 83;
            // 
            // ColumnHeader51
            // 
            this.ColumnHeader51.Text = "DW Disp H";
            this.ColumnHeader51.Width = 71;
            // 
            // Label73
            // 
            this.Label73.AutoSize = true;
            this.Label73.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label73.Location = new System.Drawing.Point(15, 15);
            this.Label73.Name = "Label73";
            this.Label73.Size = new System.Drawing.Size(280, 25);
            this.Label73.TabIndex = 30;
            this.Label73.Text = "Met and Turbine Summary";
            // 
            // pgeGrossTurbs
            // 
            this.pgeGrossTurbs.Controls.Add(this.plotGrossHisto);
            this.pgeGrossTurbs.Controls.Add(this.plotGross_PowerCrvs);
            this.pgeGrossTurbs.Controls.Add(this.plotGrossWS_Dist);
            this.pgeGrossTurbs.Controls.Add(this.plotGrossWindRose);
            this.pgeGrossTurbs.Controls.Add(this.btnGenTurbGross);
            this.pgeGrossTurbs.Controls.Add(this.lstPowerCurveList);
            this.pgeGrossTurbs.Controls.Add(this.txtisMCPdGross);
            this.pgeGrossTurbs.Controls.Add(this.Label40);
            this.pgeGrossTurbs.Controls.Add(this.cboWS_or_WD);
            this.pgeGrossTurbs.Controls.Add(this.txtGross_FlowSepUsed);
            this.pgeGrossTurbs.Controls.Add(this.Label96);
            this.pgeGrossTurbs.Controls.Add(this.lstGrossHisto);
            this.pgeGrossTurbs.Controls.Add(this.txtGross_LC_used);
            this.pgeGrossTurbs.Controls.Add(this.Label83);
            this.pgeGrossTurbs.Controls.Add(this.Label79);
            this.pgeGrossTurbs.Controls.Add(this.chkMetGrossAll);
            this.pgeGrossTurbs.Controls.Add(this.Label50);
            this.pgeGrossTurbs.Controls.Add(this.chkMetGross);
            this.pgeGrossTurbs.Controls.Add(this.Label77);
            this.pgeGrossTurbs.Controls.Add(this.cboGrossParam);
            this.pgeGrossTurbs.Controls.Add(this.btnExportDirWSDists);
            this.pgeGrossTurbs.Controls.Add(this.Label71);
            this.pgeGrossTurbs.Controls.Add(this.cboGrossWD);
            this.pgeGrossTurbs.Controls.Add(this.btnExportDirWS);
            this.pgeGrossTurbs.Controls.Add(this.btnExportCRV);
            this.pgeGrossTurbs.Controls.Add(this.btnExportWSDists);
            this.pgeGrossTurbs.Controls.Add(this.btnWS_AEP_Exprt);
            this.pgeGrossTurbs.Controls.Add(this.txtAEPMax);
            this.pgeGrossTurbs.Controls.Add(this.txtAEPMin);
            this.pgeGrossTurbs.Controls.Add(this.txtAEPSD);
            this.pgeGrossTurbs.Controls.Add(this.txtAEPAvg);
            this.pgeGrossTurbs.Controls.Add(this.Label35);
            this.pgeGrossTurbs.Controls.Add(this.Label34);
            this.pgeGrossTurbs.Controls.Add(this.chkTurbGrossAll);
            this.pgeGrossTurbs.Controls.Add(this.Label9);
            this.pgeGrossTurbs.Controls.Add(this.chkTurbGross);
            this.pgeGrossTurbs.Controls.Add(this.btnDelPowerCrv);
            this.pgeGrossTurbs.Controls.Add(this.btnImportCRV);
            this.pgeGrossTurbs.Controls.Add(this.txtMax);
            this.pgeGrossTurbs.Controls.Add(this.txtMin);
            this.pgeGrossTurbs.Controls.Add(this.txtCount);
            this.pgeGrossTurbs.Controls.Add(this.txtStDev);
            this.pgeGrossTurbs.Controls.Add(this.txtAvg);
            this.pgeGrossTurbs.Controls.Add(this.lblMax);
            this.pgeGrossTurbs.Controls.Add(this.lblMin);
            this.pgeGrossTurbs.Controls.Add(this.lblCount);
            this.pgeGrossTurbs.Controls.Add(this.lblStdev);
            this.pgeGrossTurbs.Controls.Add(this.lblAvg);
            this.pgeGrossTurbs.Controls.Add(this.Label1);
            this.pgeGrossTurbs.Controls.Add(this.lblTurbEsts);
            this.pgeGrossTurbs.Controls.Add(this.cboPowerCrvs);
            this.pgeGrossTurbs.Controls.Add(this.lstGrossTurbEst);
            this.pgeGrossTurbs.Location = new System.Drawing.Point(4, 27);
            this.pgeGrossTurbs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeGrossTurbs.Name = "pgeGrossTurbs";
            this.pgeGrossTurbs.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeGrossTurbs.Size = new System.Drawing.Size(1627, 849);
            this.pgeGrossTurbs.TabIndex = 3;
            this.pgeGrossTurbs.Text = "Gross Turbine Ests.";
            this.pgeGrossTurbs.UseVisualStyleBackColor = true;
            // 
            // plotGrossHisto
            // 
            this.plotGrossHisto.Location = new System.Drawing.Point(292, 527);
            this.plotGrossHisto.Name = "plotGrossHisto";
            this.plotGrossHisto.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGrossHisto.Size = new System.Drawing.Size(531, 299);
            this.plotGrossHisto.TabIndex = 219;
            this.plotGrossHisto.Text = "plotView1";
            this.plotGrossHisto.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotGrossHisto.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotGrossHisto.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotGross_PowerCrvs
            // 
            this.plotGross_PowerCrvs.Location = new System.Drawing.Point(1111, 440);
            this.plotGross_PowerCrvs.Name = "plotGross_PowerCrvs";
            this.plotGross_PowerCrvs.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGross_PowerCrvs.Size = new System.Drawing.Size(497, 386);
            this.plotGross_PowerCrvs.TabIndex = 218;
            this.plotGross_PowerCrvs.Text = "plotView1";
            this.plotGross_PowerCrvs.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotGross_PowerCrvs.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotGross_PowerCrvs.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotGrossWS_Dist
            // 
            this.plotGrossWS_Dist.Location = new System.Drawing.Point(1111, 39);
            this.plotGrossWS_Dist.Name = "plotGrossWS_Dist";
            this.plotGrossWS_Dist.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGrossWS_Dist.Size = new System.Drawing.Size(497, 386);
            this.plotGrossWS_Dist.TabIndex = 217;
            this.plotGrossWS_Dist.Text = "plotView1";
            this.plotGrossWS_Dist.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotGrossWS_Dist.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotGrossWS_Dist.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotGrossWindRose
            // 
            this.plotGrossWindRose.Location = new System.Drawing.Point(1111, 39);
            this.plotGrossWindRose.Name = "plotGrossWindRose";
            this.plotGrossWindRose.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGrossWindRose.Size = new System.Drawing.Size(497, 386);
            this.plotGrossWindRose.TabIndex = 216;
            this.plotGrossWindRose.Text = "plotView1";
            this.plotGrossWindRose.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotGrossWindRose.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotGrossWindRose.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnGenTurbGross
            // 
            this.btnGenTurbGross.BackColor = System.Drawing.Color.LightCoral;
            this.btnGenTurbGross.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnGenTurbGross.Location = new System.Drawing.Point(289, 13);
            this.btnGenTurbGross.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGenTurbGross.Name = "btnGenTurbGross";
            this.btnGenTurbGross.Size = new System.Drawing.Size(178, 49);
            this.btnGenTurbGross.TabIndex = 191;
            this.btnGenTurbGross.Text = "Generate Gross Energy Estimates at Turbine Sites";
            this.btnGenTurbGross.UseVisualStyleBackColor = false;
            this.btnGenTurbGross.Click += new System.EventHandler(this.btnGenTurbGross_Click);
            // 
            // lstPowerCurveList
            // 
            this.lstPowerCurveList.CheckBoxes = true;
            this.lstPowerCurveList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader186,
            this.columnHeader193,
            this.columnHeader194});
            this.lstPowerCurveList.FullRowSelect = true;
            this.lstPowerCurveList.HideSelection = false;
            this.lstPowerCurveList.Location = new System.Drawing.Point(871, 154);
            this.lstPowerCurveList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstPowerCurveList.MultiSelect = false;
            this.lstPowerCurveList.Name = "lstPowerCurveList";
            this.lstPowerCurveList.Size = new System.Drawing.Size(199, 143);
            this.lstPowerCurveList.TabIndex = 190;
            this.lstPowerCurveList.UseCompatibleStateImageBehavior = false;
            this.lstPowerCurveList.View = System.Windows.Forms.View.Details;
            this.lstPowerCurveList.SelectedIndexChanged += new System.EventHandler(this.lstPowerCurveList_SelectedIndexChanged);
            // 
            // columnHeader186
            // 
            this.columnHeader186.Text = "Name";
            this.columnHeader186.Width = 85;
            // 
            // columnHeader193
            // 
            this.columnHeader193.Text = "RD, m";
            // 
            // columnHeader194
            // 
            this.columnHeader194.Text = "RPM";
            // 
            // txtisMCPdGross
            // 
            this.txtisMCPdGross.Location = new System.Drawing.Point(477, 87);
            this.txtisMCPdGross.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtisMCPdGross.Name = "txtisMCPdGross";
            this.txtisMCPdGross.ReadOnly = true;
            this.txtisMCPdGross.Size = new System.Drawing.Size(133, 25);
            this.txtisMCPdGross.TabIndex = 189;
            // 
            // Label40
            // 
            this.Label40.AutoSize = true;
            this.Label40.Location = new System.Drawing.Point(960, 24);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(121, 38);
            this.Label40.TabIndex = 184;
            this.Label40.Text = "Select WS or WD \r\nDist. to show:";
            this.Label40.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboWS_or_WD
            // 
            this.cboWS_or_WD.FormattingEnabled = true;
            this.cboWS_or_WD.Items.AddRange(new object[] {
            "WS",
            "WD"});
            this.cboWS_or_WD.Location = new System.Drawing.Point(985, 68);
            this.cboWS_or_WD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboWS_or_WD.Name = "cboWS_or_WD";
            this.cboWS_or_WD.Size = new System.Drawing.Size(56, 26);
            this.cboWS_or_WD.TabIndex = 183;
            this.cboWS_or_WD.Text = "WS";
            this.cboWS_or_WD.SelectedIndexChanged += new System.EventHandler(this.cboWS_or_WD_SelectedIndexChanged_1);
            // 
            // txtGross_FlowSepUsed
            // 
            this.txtGross_FlowSepUsed.Location = new System.Drawing.Point(776, 87);
            this.txtGross_FlowSepUsed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGross_FlowSepUsed.Name = "txtGross_FlowSepUsed";
            this.txtGross_FlowSepUsed.ReadOnly = true;
            this.txtGross_FlowSepUsed.Size = new System.Drawing.Size(137, 25);
            this.txtGross_FlowSepUsed.TabIndex = 182;
            // 
            // Label96
            // 
            this.Label96.AutoSize = true;
            this.Label96.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label96.Location = new System.Drawing.Point(877, 479);
            this.Label96.Name = "Label96";
            this.Label96.Size = new System.Drawing.Size(131, 18);
            this.Label96.TabIndex = 181;
            this.Label96.Text = "Histogram of Ests.";
            // 
            // lstGrossHisto
            // 
            this.lstGrossHisto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader103,
            this.ColumnHeader104,
            this.ColumnHeader105});
            this.lstGrossHisto.HideSelection = false;
            this.lstGrossHisto.Location = new System.Drawing.Point(857, 508);
            this.lstGrossHisto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstGrossHisto.Name = "lstGrossHisto";
            this.lstGrossHisto.Size = new System.Drawing.Size(182, 318);
            this.lstGrossHisto.TabIndex = 180;
            this.lstGrossHisto.UseCompatibleStateImageBehavior = false;
            this.lstGrossHisto.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader103
            // 
            this.ColumnHeader103.Text = "Max Val";
            // 
            // ColumnHeader104
            // 
            this.ColumnHeader104.Text = "Mets";
            this.ColumnHeader104.Width = 36;
            // 
            // ColumnHeader105
            // 
            this.ColumnHeader105.Text = "Turbines";
            this.ColumnHeader105.Width = 55;
            // 
            // txtGross_LC_used
            // 
            this.txtGross_LC_used.Location = new System.Drawing.Point(621, 87);
            this.txtGross_LC_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGross_LC_used.Name = "txtGross_LC_used";
            this.txtGross_LC_used.ReadOnly = true;
            this.txtGross_LC_used.Size = new System.Drawing.Size(146, 25);
            this.txtGross_LC_used.TabIndex = 179;
            // 
            // Label83
            // 
            this.Label83.AutoSize = true;
            this.Label83.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label83.Location = new System.Drawing.Point(42, 527);
            this.Label83.Name = "Label83";
            this.Label83.Size = new System.Drawing.Size(202, 19);
            this.Label83.TabIndex = 178;
            this.Label83.Text = "Selected Turbine Statistics";
            // 
            // Label79
            // 
            this.Label79.AutoSize = true;
            this.Label79.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label79.Location = new System.Drawing.Point(27, 78);
            this.Label79.Name = "Label79";
            this.Label79.Size = new System.Drawing.Size(99, 18);
            this.Label79.TabIndex = 176;
            this.Label79.Text = "Power Curves :";
            // 
            // chkMetGrossAll
            // 
            this.chkMetGrossAll.AutoSize = true;
            this.chkMetGrossAll.Checked = true;
            this.chkMetGrossAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMetGrossAll.Location = new System.Drawing.Point(584, 146);
            this.chkMetGrossAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetGrossAll.Name = "chkMetGrossAll";
            this.chkMetGrossAll.Size = new System.Drawing.Size(139, 23);
            this.chkMetGrossAll.TabIndex = 173;
            this.chkMetGrossAll.Text = "Select/Deselect All";
            this.chkMetGrossAll.UseVisualStyleBackColor = true;
            this.chkMetGrossAll.CheckedChanged += new System.EventHandler(this.chkMetGrossAll_CheckedChanged);
            // 
            // Label50
            // 
            this.Label50.AutoSize = true;
            this.Label50.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label50.Location = new System.Drawing.Point(607, 123);
            this.Label50.Name = "Label50";
            this.Label50.Size = new System.Drawing.Size(73, 19);
            this.Label50.TabIndex = 172;
            this.Label50.Text = "Met Sites";
            // 
            // chkMetGross
            // 
            this.chkMetGross.CheckOnClick = true;
            this.chkMetGross.FormattingEnabled = true;
            this.chkMetGross.HorizontalScrollbar = true;
            this.chkMetGross.Location = new System.Drawing.Point(587, 172);
            this.chkMetGross.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetGross.Name = "chkMetGross";
            this.chkMetGross.Size = new System.Drawing.Size(131, 164);
            this.chkMetGross.TabIndex = 171;
            this.chkMetGross.SelectedIndexChanged += new System.EventHandler(this.chkMetGross_SelectedIndexChanged);
            // 
            // Label77
            // 
            this.Label77.AutoSize = true;
            this.Label77.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label77.Location = new System.Drawing.Point(589, 432);
            this.Label77.Name = "Label77";
            this.Label77.Size = new System.Drawing.Size(116, 18);
            this.Label77.TabIndex = 161;
            this.Label77.Text = "Parameter to plot:";
            // 
            // cboGrossParam
            // 
            this.cboGrossParam.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGrossParam.FormattingEnabled = true;
            this.cboGrossParam.Items.AddRange(new object[] {
            "Wind Speed",
            "Gross AEP",
            "Elevation",
            "Weibull k",
            "Weibull A"});
            this.cboGrossParam.Location = new System.Drawing.Point(589, 457);
            this.cboGrossParam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboGrossParam.Name = "cboGrossParam";
            this.cboGrossParam.Size = new System.Drawing.Size(126, 26);
            this.cboGrossParam.TabIndex = 160;
            this.cboGrossParam.Text = "Wind Speed";
            this.cboGrossParam.SelectedIndexChanged += new System.EventHandler(this.cboGrossParam_SelectedIndexChanged);
            // 
            // btnExportDirWSDists
            // 
            this.btnExportDirWSDists.Location = new System.Drawing.Point(837, 11);
            this.btnExportDirWSDists.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportDirWSDists.Name = "btnExportDirWSDists";
            this.btnExportDirWSDists.Size = new System.Drawing.Size(101, 68);
            this.btnExportDirWSDists.TabIndex = 102;
            this.btnExportDirWSDists.Text = "Export Directional WS Dists";
            this.btnExportDirWSDists.UseVisualStyleBackColor = true;
            this.btnExportDirWSDists.Click += new System.EventHandler(this.btnExportDirWSDists_Click);
            // 
            // Label71
            // 
            this.Label71.AutoSize = true;
            this.Label71.Location = new System.Drawing.Point(590, 366);
            this.Label71.Name = "Label71";
            this.Label71.Size = new System.Drawing.Size(79, 19);
            this.Label71.TabIndex = 159;
            this.Label71.Text = "WD sector:";
            // 
            // cboGrossWD
            // 
            this.cboGrossWD.FormattingEnabled = true;
            this.cboGrossWD.Location = new System.Drawing.Point(592, 391);
            this.cboGrossWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboGrossWD.Name = "cboGrossWD";
            this.cboGrossWD.Size = new System.Drawing.Size(115, 26);
            this.cboGrossWD.TabIndex = 158;
            this.cboGrossWD.SelectedIndexChanged += new System.EventHandler(this.cboGrossTurbWD_SelectedIndexChanged);
            // 
            // btnExportDirWS
            // 
            this.btnExportDirWS.Location = new System.Drawing.Point(712, 11);
            this.btnExportDirWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportDirWS.Name = "btnExportDirWS";
            this.btnExportDirWS.Size = new System.Drawing.Size(111, 68);
            this.btnExportDirWS.TabIndex = 157;
            this.btnExportDirWS.Text = "Export Directional WS && Weibull";
            this.btnExportDirWS.UseVisualStyleBackColor = true;
            this.btnExportDirWS.Click += new System.EventHandler(this.btnExportDirWS_Click);
            // 
            // btnExportCRV
            // 
            this.btnExportCRV.Location = new System.Drawing.Point(886, 359);
            this.btnExportCRV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportCRV.Name = "btnExportCRV";
            this.btnExportCRV.Size = new System.Drawing.Size(129, 46);
            this.btnExportCRV.TabIndex = 153;
            this.btnExportCRV.Text = "Export Power Curve";
            this.btnExportCRV.UseVisualStyleBackColor = true;
            this.btnExportCRV.Click += new System.EventHandler(this.btnExportCRV_Click);
            // 
            // btnExportWSDists
            // 
            this.btnExportWSDists.Location = new System.Drawing.Point(613, 11);
            this.btnExportWSDists.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportWSDists.Name = "btnExportWSDists";
            this.btnExportWSDists.Size = new System.Drawing.Size(85, 68);
            this.btnExportWSDists.TabIndex = 101;
            this.btnExportWSDists.Text = "Export WS Dists";
            this.btnExportWSDists.UseVisualStyleBackColor = true;
            this.btnExportWSDists.Click += new System.EventHandler(this.btnExportWSDists_Click);
            // 
            // btnWS_AEP_Exprt
            // 
            this.btnWS_AEP_Exprt.Location = new System.Drawing.Point(495, 11);
            this.btnWS_AEP_Exprt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnWS_AEP_Exprt.Name = "btnWS_AEP_Exprt";
            this.btnWS_AEP_Exprt.Size = new System.Drawing.Size(104, 68);
            this.btnWS_AEP_Exprt.TabIndex = 100;
            this.btnWS_AEP_Exprt.Text = "Export WS, Weibull && AEP";
            this.btnWS_AEP_Exprt.UseVisualStyleBackColor = true;
            this.btnWS_AEP_Exprt.Click += new System.EventHandler(this.btnWS_AEP_Exprt_Click);
            // 
            // txtAEPMax
            // 
            this.txtAEPMax.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAEPMax.Location = new System.Drawing.Point(190, 720);
            this.txtAEPMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAEPMax.Name = "txtAEPMax";
            this.txtAEPMax.ReadOnly = true;
            this.txtAEPMax.Size = new System.Drawing.Size(68, 25);
            this.txtAEPMax.TabIndex = 98;
            // 
            // txtAEPMin
            // 
            this.txtAEPMin.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAEPMin.Location = new System.Drawing.Point(190, 684);
            this.txtAEPMin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAEPMin.Name = "txtAEPMin";
            this.txtAEPMin.ReadOnly = true;
            this.txtAEPMin.Size = new System.Drawing.Size(68, 25);
            this.txtAEPMin.TabIndex = 97;
            // 
            // txtAEPSD
            // 
            this.txtAEPSD.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAEPSD.Location = new System.Drawing.Point(190, 646);
            this.txtAEPSD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAEPSD.Name = "txtAEPSD";
            this.txtAEPSD.ReadOnly = true;
            this.txtAEPSD.Size = new System.Drawing.Size(68, 25);
            this.txtAEPSD.TabIndex = 96;
            // 
            // txtAEPAvg
            // 
            this.txtAEPAvg.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAEPAvg.Location = new System.Drawing.Point(190, 609);
            this.txtAEPAvg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAEPAvg.Name = "txtAEPAvg";
            this.txtAEPAvg.ReadOnly = true;
            this.txtAEPAvg.Size = new System.Drawing.Size(68, 25);
            this.txtAEPAvg.TabIndex = 95;
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label35.Location = new System.Drawing.Point(206, 565);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(50, 36);
            this.Label35.TabIndex = 94;
            this.Label35.Text = "  AEP \r\n(MWh)";
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label34.Location = new System.Drawing.Point(104, 565);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(80, 36);
            this.Label34.TabIndex = 93;
            this.Label34.Text = "Wind Speed\r\n     (m/s)";
            // 
            // chkTurbGrossAll
            // 
            this.chkTurbGrossAll.AutoSize = true;
            this.chkTurbGrossAll.Checked = true;
            this.chkTurbGrossAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTurbGrossAll.Location = new System.Drawing.Point(729, 143);
            this.chkTurbGrossAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbGrossAll.Name = "chkTurbGrossAll";
            this.chkTurbGrossAll.Size = new System.Drawing.Size(139, 23);
            this.chkTurbGrossAll.TabIndex = 92;
            this.chkTurbGrossAll.Text = "Select/Deselect All";
            this.chkTurbGrossAll.UseVisualStyleBackColor = true;
            this.chkTurbGrossAll.CheckedChanged += new System.EventHandler(this.chkTurbGrossAll_CheckedChanged);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(752, 122);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(72, 19);
            this.Label9.TabIndex = 90;
            this.Label9.Text = "Turbines";
            // 
            // chkTurbGross
            // 
            this.chkTurbGross.CheckOnClick = true;
            this.chkTurbGross.FormattingEnabled = true;
            this.chkTurbGross.HorizontalScrollbar = true;
            this.chkTurbGross.Location = new System.Drawing.Point(733, 172);
            this.chkTurbGross.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbGross.Name = "chkTurbGross";
            this.chkTurbGross.Size = new System.Drawing.Size(116, 284);
            this.chkTurbGross.TabIndex = 88;
            this.chkTurbGross.SelectedIndexChanged += new System.EventHandler(this.chkTurbGross_SelectedIndexChanged);
            // 
            // btnDelPowerCrv
            // 
            this.btnDelPowerCrv.Location = new System.Drawing.Point(886, 413);
            this.btnDelPowerCrv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelPowerCrv.Name = "btnDelPowerCrv";
            this.btnDelPowerCrv.Size = new System.Drawing.Size(129, 46);
            this.btnDelPowerCrv.TabIndex = 85;
            this.btnDelPowerCrv.Text = "Delete Power Curve";
            this.btnDelPowerCrv.UseVisualStyleBackColor = true;
            this.btnDelPowerCrv.Click += new System.EventHandler(this.btnDelPowerCrv_Click);
            // 
            // btnImportCRV
            // 
            this.btnImportCRV.BackColor = System.Drawing.Color.LightCoral;
            this.btnImportCRV.Location = new System.Drawing.Point(886, 305);
            this.btnImportCRV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportCRV.Name = "btnImportCRV";
            this.btnImportCRV.Size = new System.Drawing.Size(129, 47);
            this.btnImportCRV.TabIndex = 34;
            this.btnImportCRV.Text = "Import Power Curve";
            this.btnImportCRV.UseVisualStyleBackColor = false;
            this.btnImportCRV.Click += new System.EventHandler(this.btnImportCRV_Click);
            // 
            // txtMax
            // 
            this.txtMax.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMax.Location = new System.Drawing.Point(102, 720);
            this.txtMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMax.Name = "txtMax";
            this.txtMax.ReadOnly = true;
            this.txtMax.Size = new System.Drawing.Size(68, 25);
            this.txtMax.TabIndex = 14;
            // 
            // txtMin
            // 
            this.txtMin.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMin.Location = new System.Drawing.Point(102, 683);
            this.txtMin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMin.Name = "txtMin";
            this.txtMin.ReadOnly = true;
            this.txtMin.Size = new System.Drawing.Size(68, 25);
            this.txtMin.TabIndex = 13;
            // 
            // txtCount
            // 
            this.txtCount.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCount.Location = new System.Drawing.Point(102, 756);
            this.txtCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(68, 25);
            this.txtCount.TabIndex = 10;
            // 
            // txtStDev
            // 
            this.txtStDev.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStDev.Location = new System.Drawing.Point(102, 646);
            this.txtStDev.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStDev.Name = "txtStDev";
            this.txtStDev.ReadOnly = true;
            this.txtStDev.Size = new System.Drawing.Size(68, 25);
            this.txtStDev.TabIndex = 9;
            // 
            // txtAvg
            // 
            this.txtAvg.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvg.Location = new System.Drawing.Point(102, 609);
            this.txtAvg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAvg.Name = "txtAvg";
            this.txtAvg.ReadOnly = true;
            this.txtAvg.Size = new System.Drawing.Size(68, 25);
            this.txtAvg.TabIndex = 8;
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMax.Location = new System.Drawing.Point(20, 719);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(76, 18);
            this.lblMax.TabIndex = 12;
            this.lblMax.Text = "Maximum :";
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMin.Location = new System.Drawing.Point(25, 686);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(74, 18);
            this.lblMin.TabIndex = 11;
            this.lblMin.Text = "Minimum :";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(39, 759);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(51, 18);
            this.lblCount.TabIndex = 7;
            this.lblCount.Text = "Count :";
            // 
            // lblStdev
            // 
            this.lblStdev.AutoSize = true;
            this.lblStdev.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStdev.Location = new System.Drawing.Point(32, 649);
            this.lblStdev.Name = "lblStdev";
            this.lblStdev.Size = new System.Drawing.Size(58, 18);
            this.lblStdev.TabIndex = 6;
            this.lblStdev.Text = "St. Dev. :";
            // 
            // lblAvg
            // 
            this.lblAvg.AutoSize = true;
            this.lblAvg.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvg.Location = new System.Drawing.Point(26, 612);
            this.lblAvg.Name = "lblAvg";
            this.lblAvg.Size = new System.Drawing.Size(63, 18);
            this.lblAvg.TabIndex = 5;
            this.lblAvg.Text = "Average :";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(878, 124);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(116, 19);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Power Curves ";
            // 
            // lblTurbEsts
            // 
            this.lblTurbEsts.AutoSize = true;
            this.lblTurbEsts.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurbEsts.Location = new System.Drawing.Point(11, 16);
            this.lblTurbEsts.Name = "lblTurbEsts";
            this.lblTurbEsts.Size = new System.Drawing.Size(247, 25);
            this.lblTurbEsts.TabIndex = 2;
            this.lblTurbEsts.Text = "Gross Turbine Estimates";
            // 
            // cboPowerCrvs
            // 
            this.cboPowerCrvs.FormattingEnabled = true;
            this.cboPowerCrvs.Items.AddRange(new object[] {
            "No Power Curves Imported"});
            this.cboPowerCrvs.Location = new System.Drawing.Point(130, 74);
            this.cboPowerCrvs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboPowerCrvs.Name = "cboPowerCrvs";
            this.cboPowerCrvs.Size = new System.Drawing.Size(303, 26);
            this.cboPowerCrvs.TabIndex = 1;
            this.cboPowerCrvs.SelectedIndexChanged += new System.EventHandler(this.cboPowerCrvs_SelectedIndexChanged);
            // 
            // lstGrossTurbEst
            // 
            this.lstGrossTurbEst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader20,
            this.ColumnHeader23,
            this.ColumnHeader24,
            this.Col_Header25,
            this.ColumnHeader13,
            this.ColumnHeader29,
            this.ColumnHeader32});
            this.lstGrossTurbEst.HideSelection = false;
            this.lstGrossTurbEst.Location = new System.Drawing.Point(24, 121);
            this.lstGrossTurbEst.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstGrossTurbEst.MultiSelect = false;
            this.lstGrossTurbEst.Name = "lstGrossTurbEst";
            this.lstGrossTurbEst.Size = new System.Drawing.Size(549, 379);
            this.lstGrossTurbEst.TabIndex = 0;
            this.lstGrossTurbEst.UseCompatibleStateImageBehavior = false;
            this.lstGrossTurbEst.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader20
            // 
            this.ColumnHeader20.Text = "Name";
            this.ColumnHeader20.Width = 85;
            // 
            // ColumnHeader23
            // 
            this.ColumnHeader23.Text = "Elev";
            // 
            // ColumnHeader24
            // 
            this.ColumnHeader24.Text = "WS, m/s";
            this.ColumnHeader24.Width = 72;
            // 
            // Col_Header25
            // 
            this.Col_Header25.Text = "AEP, MWh";
            this.Col_Header25.Width = 93;
            // 
            // ColumnHeader13
            // 
            this.ColumnHeader13.Text = "CF";
            this.ColumnHeader13.Width = 45;
            // 
            // ColumnHeader29
            // 
            this.ColumnHeader29.Text = "Weibull k";
            this.ColumnHeader29.Width = 79;
            // 
            // ColumnHeader32
            // 
            this.ColumnHeader32.Text = "Weibull A";
            this.ColumnHeader32.Width = 78;
            // 
            // pgeExceedance
            // 
            this.pgeExceedance.Controls.Add(this.plotCompositeExceed);
            this.pgeExceedance.Controls.Add(this.plotExceedCurves);
            this.pgeExceedance.Controls.Add(this.btnImportCurves);
            this.pgeExceedance.Controls.Add(this.btnGetDefaultExceed);
            this.pgeExceedance.Controls.Add(this.cboExceedWake);
            this.pgeExceedance.Controls.Add(this.label185);
            this.pgeExceedance.Controls.Add(this.label183);
            this.pgeExceedance.Controls.Add(this.cboExceedTurbine);
            this.pgeExceedance.Controls.Add(this.btn_AddProj);
            this.pgeExceedance.Controls.Add(this.btnExportAllPVals);
            this.pgeExceedance.Controls.Add(this.btnDeleteExceed);
            this.pgeExceedance.Controls.Add(this.btnExportCurves);
            this.pgeExceedance.Controls.Add(this.btnExport_P_Vals);
            this.pgeExceedance.Controls.Add(this.label180);
            this.pgeExceedance.Controls.Add(this.lstPvals);
            this.pgeExceedance.Controls.Add(this.chkShowCDFs);
            this.pgeExceedance.Controls.Add(this.chkShowPDF);
            this.pgeExceedance.Controls.Add(this.btnDoMonteCarlo);
            this.pgeExceedance.Controls.Add(this.label181);
            this.pgeExceedance.Controls.Add(this.label182);
            this.pgeExceedance.Controls.Add(this.lstDefinedLosses);
            this.pgeExceedance.Controls.Add(this.btn_editloss);
            this.pgeExceedance.Location = new System.Drawing.Point(4, 27);
            this.pgeExceedance.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeExceedance.Name = "pgeExceedance";
            this.pgeExceedance.Size = new System.Drawing.Size(1627, 849);
            this.pgeExceedance.TabIndex = 19;
            this.pgeExceedance.Text = "Exceedance Modeling";
            this.pgeExceedance.UseVisualStyleBackColor = true;
            // 
            // plotCompositeExceed
            // 
            this.plotCompositeExceed.Location = new System.Drawing.Point(1033, 485);
            this.plotCompositeExceed.Name = "plotCompositeExceed";
            this.plotCompositeExceed.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotCompositeExceed.Size = new System.Drawing.Size(567, 343);
            this.plotCompositeExceed.TabIndex = 237;
            this.plotCompositeExceed.Text = "plotView1";
            this.plotCompositeExceed.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotCompositeExceed.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotCompositeExceed.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotExceedCurves
            // 
            this.plotExceedCurves.Location = new System.Drawing.Point(970, 27);
            this.plotExceedCurves.Name = "plotExceedCurves";
            this.plotExceedCurves.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotExceedCurves.Size = new System.Drawing.Size(630, 386);
            this.plotExceedCurves.TabIndex = 236;
            this.plotExceedCurves.Text = "plotView1";
            this.plotExceedCurves.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotExceedCurves.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotExceedCurves.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnImportCurves
            // 
            this.btnImportCurves.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportCurves.Location = new System.Drawing.Point(511, 442);
            this.btnImportCurves.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportCurves.Name = "btnImportCurves";
            this.btnImportCurves.Size = new System.Drawing.Size(140, 47);
            this.btnImportCurves.TabIndex = 235;
            this.btnImportCurves.Text = "Import Curves";
            this.btnImportCurves.UseVisualStyleBackColor = true;
            this.btnImportCurves.Click += new System.EventHandler(this.btnImportCurves_Click);
            // 
            // btnGetDefaultExceed
            // 
            this.btnGetDefaultExceed.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetDefaultExceed.Location = new System.Drawing.Point(657, 442);
            this.btnGetDefaultExceed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGetDefaultExceed.Name = "btnGetDefaultExceed";
            this.btnGetDefaultExceed.Size = new System.Drawing.Size(140, 47);
            this.btnGetDefaultExceed.TabIndex = 234;
            this.btnGetDefaultExceed.Text = "Set to Default";
            this.btnGetDefaultExceed.UseVisualStyleBackColor = true;
            this.btnGetDefaultExceed.Click += new System.EventHandler(this.btnGetDefaultExceed_Click);
            // 
            // cboExceedWake
            // 
            this.cboExceedWake.FormattingEnabled = true;
            this.cboExceedWake.Location = new System.Drawing.Point(532, 530);
            this.cboExceedWake.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExceedWake.Name = "cboExceedWake";
            this.cboExceedWake.Size = new System.Drawing.Size(312, 26);
            this.cboExceedWake.TabIndex = 231;
            this.cboExceedWake.SelectedIndexChanged += new System.EventHandler(this.cboExceedWake_SelectedIndexChanged);
            // 
            // label185
            // 
            this.label185.AutoSize = true;
            this.label185.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label185.Location = new System.Drawing.Point(534, 508);
            this.label185.Name = "label185";
            this.label185.Size = new System.Drawing.Size(128, 18);
            this.label185.TabIndex = 230;
            this.label185.Text = "Net Estimate Model:";
            // 
            // label183
            // 
            this.label183.AutoSize = true;
            this.label183.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label183.Location = new System.Drawing.Point(377, 508);
            this.label183.Name = "label183";
            this.label183.Size = new System.Drawing.Size(61, 18);
            this.label183.TabIndex = 220;
            this.label183.Text = "Turbine :";
            // 
            // cboExceedTurbine
            // 
            this.cboExceedTurbine.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboExceedTurbine.FormattingEnabled = true;
            this.cboExceedTurbine.Location = new System.Drawing.Point(380, 530);
            this.cboExceedTurbine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExceedTurbine.Name = "cboExceedTurbine";
            this.cboExceedTurbine.Size = new System.Drawing.Size(134, 26);
            this.cboExceedTurbine.TabIndex = 219;
            this.cboExceedTurbine.SelectedIndexChanged += new System.EventHandler(this.cboExceedTurbine_SelectedIndexChanged);
            // 
            // btn_AddProj
            // 
            this.btn_AddProj.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddProj.Location = new System.Drawing.Point(50, 442);
            this.btn_AddProj.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddProj.Name = "btn_AddProj";
            this.btn_AddProj.Size = new System.Drawing.Size(129, 47);
            this.btn_AddProj.TabIndex = 44;
            this.btn_AddProj.Text = "Add Curve";
            this.btn_AddProj.UseVisualStyleBackColor = true;
            this.btn_AddProj.Click += new System.EventHandler(this.btn_AddProj_Click);
            // 
            // btnExportAllPVals
            // 
            this.btnExportAllPVals.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportAllPVals.Location = new System.Drawing.Point(878, 698);
            this.btnExportAllPVals.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportAllPVals.Name = "btnExportAllPVals";
            this.btnExportAllPVals.Size = new System.Drawing.Size(139, 59);
            this.btnExportAllPVals.TabIndex = 39;
            this.btnExportAllPVals.Text = "Export All P Values";
            this.btnExportAllPVals.UseVisualStyleBackColor = true;
            this.btnExportAllPVals.Click += new System.EventHandler(this.btnExportAllPVals_Click);
            // 
            // btnDeleteExceed
            // 
            this.btnDeleteExceed.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteExceed.Location = new System.Drawing.Point(352, 442);
            this.btnDeleteExceed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDeleteExceed.Name = "btnDeleteExceed";
            this.btnDeleteExceed.Size = new System.Drawing.Size(152, 47);
            this.btnDeleteExceed.TabIndex = 38;
            this.btnDeleteExceed.Text = "Delete Curve(s)";
            this.btnDeleteExceed.UseVisualStyleBackColor = true;
            this.btnDeleteExceed.Click += new System.EventHandler(this.btnDeleteExceed_Click);
            // 
            // btnExportCurves
            // 
            this.btnExportCurves.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCurves.Location = new System.Drawing.Point(804, 442);
            this.btnExportCurves.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportCurves.Name = "btnExportCurves";
            this.btnExportCurves.Size = new System.Drawing.Size(140, 47);
            this.btnExportCurves.TabIndex = 37;
            this.btnExportCurves.Text = "Export Curves";
            this.btnExportCurves.UseVisualStyleBackColor = true;
            this.btnExportCurves.Click += new System.EventHandler(this.btnExportCurves_Click);
            // 
            // btnExport_P_Vals
            // 
            this.btnExport_P_Vals.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport_P_Vals.Location = new System.Drawing.Point(877, 642);
            this.btnExport_P_Vals.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExport_P_Vals.Name = "btnExport_P_Vals";
            this.btnExport_P_Vals.Size = new System.Drawing.Size(139, 38);
            this.btnExport_P_Vals.TabIndex = 34;
            this.btnExport_P_Vals.Text = "Export P table";
            this.btnExport_P_Vals.UseVisualStyleBackColor = true;
            this.btnExport_P_Vals.Click += new System.EventHandler(this.btnExport_P_Vals_Click);
            // 
            // label180
            // 
            this.label180.AutoSize = true;
            this.label180.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label180.Location = new System.Drawing.Point(36, 528);
            this.label180.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(273, 23);
            this.label180.TabIndex = 33;
            this.label180.Text = "Table of Exceedance Values";
            // 
            // lstPvals
            // 
            this.lstPvals.CheckBoxes = true;
            this.lstPvals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader160,
            this.columnHeader161,
            this.columnHeader162,
            this.columnHeader163,
            this.columnHeader164,
            this.columnHeader165,
            this.columnHeader166});
            this.lstPvals.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPvals.HideSelection = false;
            this.lstPvals.Location = new System.Drawing.Point(34, 564);
            this.lstPvals.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.lstPvals.Name = "lstPvals";
            this.lstPvals.Size = new System.Drawing.Size(810, 224);
            this.lstPvals.TabIndex = 32;
            this.lstPvals.UseCompatibleStateImageBehavior = false;
            this.lstPvals.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader160
            // 
            this.columnHeader160.Text = "P Value";
            this.columnHeader160.Width = 96;
            // 
            // columnHeader161
            // 
            this.columnHeader161.Text = "1 yr PF";
            this.columnHeader161.Width = 107;
            // 
            // columnHeader162
            // 
            this.columnHeader162.Text = "1yr AEP";
            this.columnHeader162.Width = 98;
            // 
            // columnHeader163
            // 
            this.columnHeader163.Text = "10 yr PF";
            this.columnHeader163.Width = 92;
            // 
            // columnHeader164
            // 
            this.columnHeader164.Text = "10yr AEP";
            this.columnHeader164.Width = 110;
            // 
            // columnHeader165
            // 
            this.columnHeader165.Text = "20 yr PF";
            this.columnHeader165.Width = 93;
            // 
            // columnHeader166
            // 
            this.columnHeader166.Text = "20yr AEP";
            this.columnHeader166.Width = 94;
            // 
            // chkShowCDFs
            // 
            this.chkShowCDFs.AutoSize = true;
            this.chkShowCDFs.Checked = true;
            this.chkShowCDFs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowCDFs.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowCDFs.Location = new System.Drawing.Point(1276, 425);
            this.chkShowCDFs.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.chkShowCDFs.Name = "chkShowCDFs";
            this.chkShowCDFs.Size = new System.Drawing.Size(109, 26);
            this.chkShowCDFs.TabIndex = 31;
            this.chkShowCDFs.Text = "Show CDFs";
            this.chkShowCDFs.UseVisualStyleBackColor = true;
            this.chkShowCDFs.CheckedChanged += new System.EventHandler(this.chkShowCDFs_CheckedChanged);
            // 
            // chkShowPDF
            // 
            this.chkShowPDF.AutoSize = true;
            this.chkShowPDF.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowPDF.Location = new System.Drawing.Point(1136, 425);
            this.chkShowPDF.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.chkShowPDF.Name = "chkShowPDF";
            this.chkShowPDF.Size = new System.Drawing.Size(108, 26);
            this.chkShowPDF.TabIndex = 30;
            this.chkShowPDF.Text = "Show PDFs";
            this.chkShowPDF.UseVisualStyleBackColor = true;
            this.chkShowPDF.CheckedChanged += new System.EventHandler(this.chkShowPDF_CheckedChanged);
            // 
            // btnDoMonteCarlo
            // 
            this.btnDoMonteCarlo.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoMonteCarlo.Location = new System.Drawing.Point(881, 554);
            this.btnDoMonteCarlo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDoMonteCarlo.Name = "btnDoMonteCarlo";
            this.btnDoMonteCarlo.Size = new System.Drawing.Size(129, 64);
            this.btnDoMonteCarlo.TabIndex = 29;
            this.btnDoMonteCarlo.Text = "Do Monte Carlo";
            this.btnDoMonteCarlo.UseVisualStyleBackColor = true;
            this.btnDoMonteCarlo.Click += new System.EventHandler(this.btnDoMonteCarlo_Click);
            // 
            // label181
            // 
            this.label181.AutoSize = true;
            this.label181.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label181.Location = new System.Drawing.Point(43, 27);
            this.label181.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label181.Name = "label181";
            this.label181.Size = new System.Drawing.Size(243, 25);
            this.label181.TabIndex = 28;
            this.label181.Text = "Exceedance Modeling";
            // 
            // label182
            // 
            this.label182.AutoSize = true;
            this.label182.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label182.Location = new System.Drawing.Point(43, 64);
            this.label182.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label182.Name = "label182";
            this.label182.Size = new System.Drawing.Size(383, 23);
            this.label182.TabIndex = 27;
            this.label182.Text = "Summary of Defined Exceedance curves";
            // 
            // lstDefinedLosses
            // 
            this.lstDefinedLosses.CheckBoxes = true;
            this.lstDefinedLosses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader167,
            this.columnHeader168,
            this.columnHeader169,
            this.columnHeader170,
            this.columnHeader171,
            this.columnHeader172,
            this.columnHeader173,
            this.columnHeader174});
            this.lstDefinedLosses.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDefinedLosses.HideSelection = false;
            this.lstDefinedLosses.Location = new System.Drawing.Point(48, 91);
            this.lstDefinedLosses.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.lstDefinedLosses.Name = "lstDefinedLosses";
            this.lstDefinedLosses.Size = new System.Drawing.Size(886, 339);
            this.lstDefinedLosses.TabIndex = 26;
            this.lstDefinedLosses.UseCompatibleStateImageBehavior = false;
            this.lstDefinedLosses.View = System.Windows.Forms.View.Details;
            this.lstDefinedLosses.SelectedIndexChanged += new System.EventHandler(this.lstDefinedLosses_SelectedIndexChanged);
            // 
            // columnHeader167
            // 
            this.columnHeader167.Text = "Name";
            this.columnHeader167.Width = 287;
            // 
            // columnHeader168
            // 
            this.columnHeader168.Text = "P10";
            this.columnHeader168.Width = 70;
            // 
            // columnHeader169
            // 
            this.columnHeader169.Text = "P50";
            this.columnHeader169.Width = 80;
            // 
            // columnHeader170
            // 
            this.columnHeader170.Text = "P90";
            this.columnHeader170.Width = 65;
            // 
            // columnHeader171
            // 
            this.columnHeader171.Text = "Lower";
            // 
            // columnHeader172
            // 
            this.columnHeader172.Text = "Upper";
            // 
            // columnHeader173
            // 
            this.columnHeader173.Text = "Mean";
            // 
            // columnHeader174
            // 
            this.columnHeader174.Text = "St. Dev.";
            // 
            // btn_editloss
            // 
            this.btn_editloss.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_editloss.Location = new System.Drawing.Point(185, 442);
            this.btn_editloss.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_editloss.Name = "btn_editloss";
            this.btn_editloss.Size = new System.Drawing.Size(160, 47);
            this.btn_editloss.TabIndex = 25;
            this.btn_editloss.Text = "Edit Curve";
            this.btn_editloss.UseVisualStyleBackColor = true;
            this.btn_editloss.Click += new System.EventHandler(this.btn_editloss_Click);
            // 
            // pgeNetEsts
            // 
            this.pgeNetEsts.Controls.Add(this.plotWakeMap);
            this.pgeNetEsts.Controls.Add(this.plotTurbsByString);
            this.pgeNetEsts.Controls.Add(this.plotWakedDists);
            this.pgeNetEsts.Controls.Add(this.txtisMCPdNet);
            this.pgeNetEsts.Controls.Add(this.txtOtherLosses);
            this.pgeNetEsts.Controls.Add(this.label88);
            this.pgeNetEsts.Controls.Add(this.txtNet_FlowSep_Used);
            this.pgeNetEsts.Controls.Add(this.btnExportNetDirWSDists);
            this.pgeNetEsts.Controls.Add(this.btnExportNetDirWS);
            this.pgeNetEsts.Controls.Add(this.btnExportNetWSDists);
            this.pgeNetEsts.Controls.Add(this.btnRefreshWakeMap);
            this.pgeNetEsts.Controls.Add(this.chkWakeAuto);
            this.pgeNetEsts.Controls.Add(this.txtWakeInterval);
            this.pgeNetEsts.Controls.Add(this.txtWakeMax);
            this.pgeNetEsts.Controls.Add(this.txtWakeMin);
            this.pgeNetEsts.Controls.Add(this.Label97);
            this.pgeNetEsts.Controls.Add(this.Label98);
            this.pgeNetEsts.Controls.Add(this.Label99);
            this.pgeNetEsts.Controls.Add(this.txtLC_Net);
            this.pgeNetEsts.Controls.Add(this.btnCreateWakeMap);
            this.pgeNetEsts.Controls.Add(this.txtWakeLoss);
            this.pgeNetEsts.Controls.Add(this.Label94);
            this.pgeNetEsts.Controls.Add(this.chkTurbNet);
            this.pgeNetEsts.Controls.Add(this.chkTurbNetAll);
            this.pgeNetEsts.Controls.Add(this.Label87);
            this.pgeNetEsts.Controls.Add(this.chkStrings);
            this.pgeNetEsts.Controls.Add(this.chkStringAll);
            this.pgeNetEsts.Controls.Add(this.Label86);
            this.pgeNetEsts.Controls.Add(this.btnDelWakeModel);
            this.pgeNetEsts.Controls.Add(this.btnDelWakeGrid);
            this.pgeNetEsts.Controls.Add(this.cboWakePlot);
            this.pgeNetEsts.Controls.Add(this.Label85);
            this.pgeNetEsts.Controls.Add(this.btnExportNetEsts);
            this.pgeNetEsts.Controls.Add(this.Label55);
            this.pgeNetEsts.Controls.Add(this.cboNetWD);
            this.pgeNetEsts.Controls.Add(this.lstWakeModels);
            this.pgeNetEsts.Controls.Add(this.btnWakeLossCalc);
            this.pgeNetEsts.Controls.Add(this.Label53);
            this.pgeNetEsts.Controls.Add(this.lstWakedTurbs);
            this.pgeNetEsts.Controls.Add(this.Label36);
            this.pgeNetEsts.Location = new System.Drawing.Point(4, 27);
            this.pgeNetEsts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeNetEsts.Name = "pgeNetEsts";
            this.pgeNetEsts.Size = new System.Drawing.Size(1627, 849);
            this.pgeNetEsts.TabIndex = 13;
            this.pgeNetEsts.Text = "Net Turbine Ests";
            this.pgeNetEsts.UseVisualStyleBackColor = true;
            // 
            // plotWakeMap
            // 
            this.plotWakeMap.Location = new System.Drawing.Point(989, 238);
            this.plotWakeMap.Name = "plotWakeMap";
            this.plotWakeMap.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotWakeMap.Size = new System.Drawing.Size(610, 499);
            this.plotWakeMap.TabIndex = 240;
            this.plotWakeMap.Text = "plotView1";
            this.plotWakeMap.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotWakeMap.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotWakeMap.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotTurbsByString
            // 
            this.plotTurbsByString.Location = new System.Drawing.Point(499, 534);
            this.plotTurbsByString.Name = "plotTurbsByString";
            this.plotTurbsByString.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTurbsByString.Size = new System.Drawing.Size(445, 296);
            this.plotTurbsByString.TabIndex = 239;
            this.plotTurbsByString.Text = "plotView1";
            this.plotTurbsByString.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTurbsByString.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTurbsByString.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotWakedDists
            // 
            this.plotWakedDists.Location = new System.Drawing.Point(21, 534);
            this.plotWakedDists.Name = "plotWakedDists";
            this.plotWakedDists.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotWakedDists.Size = new System.Drawing.Size(445, 296);
            this.plotWakedDists.TabIndex = 238;
            this.plotWakedDists.Text = "plotView1";
            this.plotWakedDists.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotWakedDists.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotWakedDists.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // txtisMCPdNet
            // 
            this.txtisMCPdNet.Location = new System.Drawing.Point(302, 23);
            this.txtisMCPdNet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtisMCPdNet.Name = "txtisMCPdNet";
            this.txtisMCPdNet.ReadOnly = true;
            this.txtisMCPdNet.Size = new System.Drawing.Size(164, 25);
            this.txtisMCPdNet.TabIndex = 199;
            // 
            // txtOtherLosses
            // 
            this.txtOtherLosses.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherLosses.Location = new System.Drawing.Point(856, 327);
            this.txtOtherLosses.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOtherLosses.Name = "txtOtherLosses";
            this.txtOtherLosses.Size = new System.Drawing.Size(67, 25);
            this.txtOtherLosses.TabIndex = 198;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(811, 301);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(164, 19);
            this.label88.TabIndex = 197;
            this.label88.Text = "Overall Other Losses:";
            // 
            // txtNet_FlowSep_Used
            // 
            this.txtNet_FlowSep_Used.Location = new System.Drawing.Point(475, 39);
            this.txtNet_FlowSep_Used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNet_FlowSep_Used.Name = "txtNet_FlowSep_Used";
            this.txtNet_FlowSep_Used.ReadOnly = true;
            this.txtNet_FlowSep_Used.Size = new System.Drawing.Size(164, 25);
            this.txtNet_FlowSep_Used.TabIndex = 193;
            // 
            // btnExportNetDirWSDists
            // 
            this.btnExportNetDirWSDists.Location = new System.Drawing.Point(134, 464);
            this.btnExportNetDirWSDists.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportNetDirWSDists.Name = "btnExportNetDirWSDists";
            this.btnExportNetDirWSDists.Size = new System.Drawing.Size(138, 49);
            this.btnExportNetDirWSDists.TabIndex = 191;
            this.btnExportNetDirWSDists.Text = "Export Directional WS Dists";
            this.btnExportNetDirWSDists.UseVisualStyleBackColor = true;
            this.btnExportNetDirWSDists.Click += new System.EventHandler(this.btnExportNetDirWSDists_Click);
            // 
            // btnExportNetDirWS
            // 
            this.btnExportNetDirWS.Location = new System.Drawing.Point(278, 464);
            this.btnExportNetDirWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportNetDirWS.Name = "btnExportNetDirWS";
            this.btnExportNetDirWS.Size = new System.Drawing.Size(142, 50);
            this.btnExportNetDirWS.TabIndex = 192;
            this.btnExportNetDirWS.Text = "Export Directional WS && Weibull";
            this.btnExportNetDirWS.UseVisualStyleBackColor = true;
            this.btnExportNetDirWS.Click += new System.EventHandler(this.btnExportNetDirWS_Click);
            // 
            // btnExportNetWSDists
            // 
            this.btnExportNetWSDists.Location = new System.Drawing.Point(21, 464);
            this.btnExportNetWSDists.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportNetWSDists.Name = "btnExportNetWSDists";
            this.btnExportNetWSDists.Size = new System.Drawing.Size(107, 49);
            this.btnExportNetWSDists.TabIndex = 190;
            this.btnExportNetWSDists.Text = "Export WS Distributions";
            this.btnExportNetWSDists.UseVisualStyleBackColor = true;
            this.btnExportNetWSDists.Click += new System.EventHandler(this.btnExportNetWSDists_Click);
            // 
            // btnRefreshWakeMap
            // 
            this.btnRefreshWakeMap.Location = new System.Drawing.Point(1513, 782);
            this.btnRefreshWakeMap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefreshWakeMap.Name = "btnRefreshWakeMap";
            this.btnRefreshWakeMap.Size = new System.Drawing.Size(101, 33);
            this.btnRefreshWakeMap.TabIndex = 188;
            this.btnRefreshWakeMap.Text = "Refresh Map";
            this.btnRefreshWakeMap.UseVisualStyleBackColor = true;
            this.btnRefreshWakeMap.Click += new System.EventHandler(this.btnRefreshWakeMap_Click);
            // 
            // chkWakeAuto
            // 
            this.chkWakeAuto.AutoSize = true;
            this.chkWakeAuto.Checked = true;
            this.chkWakeAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWakeAuto.Location = new System.Drawing.Point(1331, 791);
            this.chkWakeAuto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkWakeAuto.Name = "chkWakeAuto";
            this.chkWakeAuto.Size = new System.Drawing.Size(176, 23);
            this.chkWakeAuto.TabIndex = 187;
            this.chkWakeAuto.Text = "Use Auto Min and Max";
            this.chkWakeAuto.UseVisualStyleBackColor = true;
            // 
            // txtWakeInterval
            // 
            this.txtWakeInterval.Location = new System.Drawing.Point(1263, 788);
            this.txtWakeInterval.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWakeInterval.Name = "txtWakeInterval";
            this.txtWakeInterval.Size = new System.Drawing.Size(56, 25);
            this.txtWakeInterval.TabIndex = 186;
            // 
            // txtWakeMax
            // 
            this.txtWakeMax.Location = new System.Drawing.Point(1136, 788);
            this.txtWakeMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWakeMax.Name = "txtWakeMax";
            this.txtWakeMax.Size = new System.Drawing.Size(61, 25);
            this.txtWakeMax.TabIndex = 184;
            // 
            // txtWakeMin
            // 
            this.txtWakeMin.Location = new System.Drawing.Point(1025, 788);
            this.txtWakeMin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWakeMin.Name = "txtWakeMin";
            this.txtWakeMin.Size = new System.Drawing.Size(61, 25);
            this.txtWakeMin.TabIndex = 182;
            // 
            // Label97
            // 
            this.Label97.AutoSize = true;
            this.Label97.Location = new System.Drawing.Point(1201, 791);
            this.Label97.Name = "Label97";
            this.Label97.Size = new System.Drawing.Size(63, 19);
            this.Label97.TabIndex = 185;
            this.Label97.Text = "Interval:";
            // 
            // Label98
            // 
            this.Label98.AutoSize = true;
            this.Label98.Location = new System.Drawing.Point(1095, 791);
            this.Label98.Name = "Label98";
            this.Label98.Size = new System.Drawing.Size(41, 19);
            this.Label98.TabIndex = 183;
            this.Label98.Text = "Max:";
            // 
            // Label99
            // 
            this.Label99.AutoSize = true;
            this.Label99.Location = new System.Drawing.Point(985, 792);
            this.Label99.Name = "Label99";
            this.Label99.Size = new System.Drawing.Size(39, 19);
            this.Label99.TabIndex = 181;
            this.Label99.Text = "Min:";
            // 
            // txtLC_Net
            // 
            this.txtLC_Net.Location = new System.Drawing.Point(475, 7);
            this.txtLC_Net.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLC_Net.Name = "txtLC_Net";
            this.txtLC_Net.ReadOnly = true;
            this.txtLC_Net.Size = new System.Drawing.Size(164, 25);
            this.txtLC_Net.TabIndex = 180;
            // 
            // btnCreateWakeMap
            // 
            this.btnCreateWakeMap.BackColor = System.Drawing.Color.LightCoral;
            this.btnCreateWakeMap.Location = new System.Drawing.Point(1189, 11);
            this.btnCreateWakeMap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCreateWakeMap.Name = "btnCreateWakeMap";
            this.btnCreateWakeMap.Size = new System.Drawing.Size(136, 53);
            this.btnCreateWakeMap.TabIndex = 68;
            this.btnCreateWakeMap.Text = "Create Wake Map";
            this.btnCreateWakeMap.UseVisualStyleBackColor = false;
            this.btnCreateWakeMap.Click += new System.EventHandler(this.btnCreateWakeMap_Click);
            // 
            // txtWakeLoss
            // 
            this.txtWakeLoss.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWakeLoss.Location = new System.Drawing.Point(856, 256);
            this.txtWakeLoss.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWakeLoss.Name = "txtWakeLoss";
            this.txtWakeLoss.Size = new System.Drawing.Size(67, 25);
            this.txtWakeLoss.TabIndex = 65;
            // 
            // Label94
            // 
            this.Label94.AutoSize = true;
            this.Label94.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label94.Location = new System.Drawing.Point(812, 229);
            this.Label94.Name = "Label94";
            this.Label94.Size = new System.Drawing.Size(150, 19);
            this.Label94.TabIndex = 64;
            this.Label94.Text = "Overall Wake Loss:";
            // 
            // chkTurbNet
            // 
            this.chkTurbNet.CheckOnClick = true;
            this.chkTurbNet.FormattingEnabled = true;
            this.chkTurbNet.HorizontalScrollbar = true;
            this.chkTurbNet.Location = new System.Drawing.Point(661, 400);
            this.chkTurbNet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbNet.Name = "chkTurbNet";
            this.chkTurbNet.Size = new System.Drawing.Size(139, 104);
            this.chkTurbNet.TabIndex = 19;
            this.chkTurbNet.SelectedIndexChanged += new System.EventHandler(this.chkTurbNet_SelectedIndexChanged);
            // 
            // chkTurbNetAll
            // 
            this.chkTurbNetAll.AutoSize = true;
            this.chkTurbNetAll.Checked = true;
            this.chkTurbNetAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTurbNetAll.Location = new System.Drawing.Point(661, 373);
            this.chkTurbNetAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbNetAll.Name = "chkTurbNetAll";
            this.chkTurbNetAll.Size = new System.Drawing.Size(117, 23);
            this.chkTurbNetAll.TabIndex = 18;
            this.chkTurbNetAll.Text = "Select/Deselect";
            this.chkTurbNetAll.UseVisualStyleBackColor = true;
            this.chkTurbNetAll.CheckedChanged += new System.EventHandler(this.chkTurbNetAll_CheckedChanged);
            // 
            // Label87
            // 
            this.Label87.AutoSize = true;
            this.Label87.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label87.Location = new System.Drawing.Point(658, 348);
            this.Label87.Name = "Label87";
            this.Label87.Size = new System.Drawing.Size(67, 18);
            this.Label87.TabIndex = 17;
            this.Label87.Text = "Turbines";
            // 
            // chkStrings
            // 
            this.chkStrings.CheckOnClick = true;
            this.chkStrings.FormattingEnabled = true;
            this.chkStrings.Location = new System.Drawing.Point(661, 262);
            this.chkStrings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkStrings.Name = "chkStrings";
            this.chkStrings.Size = new System.Drawing.Size(139, 64);
            this.chkStrings.TabIndex = 16;
            this.chkStrings.SelectedIndexChanged += new System.EventHandler(this.chkStrings_SelectedIndexChanged);
            // 
            // chkStringAll
            // 
            this.chkStringAll.AutoSize = true;
            this.chkStringAll.Checked = true;
            this.chkStringAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStringAll.Location = new System.Drawing.Point(663, 238);
            this.chkStringAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkStringAll.Name = "chkStringAll";
            this.chkStringAll.Size = new System.Drawing.Size(117, 23);
            this.chkStringAll.TabIndex = 15;
            this.chkStringAll.Text = "Select/Deselect";
            this.chkStringAll.UseVisualStyleBackColor = true;
            this.chkStringAll.CheckedChanged += new System.EventHandler(this.chkStringAll_CheckedChanged);
            // 
            // Label86
            // 
            this.Label86.AutoSize = true;
            this.Label86.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label86.Location = new System.Drawing.Point(658, 217);
            this.Label86.Name = "Label86";
            this.Label86.Size = new System.Drawing.Size(57, 19);
            this.Label86.TabIndex = 14;
            this.Label86.Text = "Strings";
            // 
            // btnDelWakeModel
            // 
            this.btnDelWakeModel.Location = new System.Drawing.Point(1069, 11);
            this.btnDelWakeModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelWakeModel.Name = "btnDelWakeModel";
            this.btnDelWakeModel.Size = new System.Drawing.Size(113, 51);
            this.btnDelWakeModel.TabIndex = 13;
            this.btnDelWakeModel.Text = "Delete Model";
            this.btnDelWakeModel.UseVisualStyleBackColor = true;
            this.btnDelWakeModel.Click += new System.EventHandler(this.btnDelWakeModel_Click);
            // 
            // btnDelWakeGrid
            // 
            this.btnDelWakeGrid.Location = new System.Drawing.Point(1486, 14);
            this.btnDelWakeGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelWakeGrid.Name = "btnDelWakeGrid";
            this.btnDelWakeGrid.Size = new System.Drawing.Size(113, 48);
            this.btnDelWakeGrid.TabIndex = 12;
            this.btnDelWakeGrid.Text = "Delete Wake Map";
            this.btnDelWakeGrid.UseVisualStyleBackColor = true;
            this.btnDelWakeGrid.Click += new System.EventHandler(this.btnDelWakeGrid_Click);
            // 
            // cboWakePlot
            // 
            this.cboWakePlot.FormattingEnabled = true;
            this.cboWakePlot.Items.AddRange(new object[] {
            "Wind Speed",
            "Wake loss, %",
            "Net Energy"});
            this.cboWakePlot.Location = new System.Drawing.Point(1331, 38);
            this.cboWakePlot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboWakePlot.Name = "cboWakePlot";
            this.cboWakePlot.Size = new System.Drawing.Size(147, 26);
            this.cboWakePlot.TabIndex = 11;
            this.cboWakePlot.Text = "Wind Speed";
            this.cboWakePlot.SelectedIndexChanged += new System.EventHandler(this.cboWakePlot_SelectedIndexChanged);
            // 
            // Label85
            // 
            this.Label85.AutoSize = true;
            this.Label85.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label85.Location = new System.Drawing.Point(1337, 14);
            this.Label85.Name = "Label85";
            this.Label85.Size = new System.Drawing.Size(130, 18);
            this.Label85.TabIndex = 10;
            this.Label85.Text = "Display in Plot/Map:";
            // 
            // btnExportNetEsts
            // 
            this.btnExportNetEsts.Location = new System.Drawing.Point(426, 464);
            this.btnExportNetEsts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportNetEsts.Name = "btnExportNetEsts";
            this.btnExportNetEsts.Size = new System.Drawing.Size(209, 49);
            this.btnExportNetEsts.TabIndex = 9;
            this.btnExportNetEsts.Text = "Export Net Turbine Estimates && Summary of Losses";
            this.btnExportNetEsts.UseVisualStyleBackColor = true;
            this.btnExportNetEsts.Click += new System.EventHandler(this.btnExportNetEsts_Click);
            // 
            // Label55
            // 
            this.Label55.AutoSize = true;
            this.Label55.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label55.Location = new System.Drawing.Point(834, 415);
            this.Label55.Name = "Label55";
            this.Label55.Size = new System.Drawing.Size(124, 19);
            this.Label55.TabIndex = 8;
            this.Label55.Text = "Wind Direction:";
            // 
            // cboNetWD
            // 
            this.cboNetWD.FormattingEnabled = true;
            this.cboNetWD.Location = new System.Drawing.Point(831, 448);
            this.cboNetWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboNetWD.Name = "cboNetWD";
            this.cboNetWD.Size = new System.Drawing.Size(129, 26);
            this.cboNetWD.TabIndex = 7;
            this.cboNetWD.SelectedIndexChanged += new System.EventHandler(this.cboTurbEstWD_SelectedIndexChanged);
            // 
            // lstWakeModels
            // 
            this.lstWakeModels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader88,
            this.ColumnHeader106,
            this.ColumnHeader89,
            this.ColumnHeader107,
            this.ColumnHeader90,
            this.ColumnHeader94,
            this.ColumnHeader95,
            this.ColumnHeader96,
            this.ColumnHeader97,
            this.ColumnHeader98,
            this.ColumnHeader99,
            this.ColumnHeader100,
            this.ColumnHeader101,
            this.ColumnHeader102});
            this.lstWakeModels.FullRowSelect = true;
            this.lstWakeModels.HideSelection = false;
            this.lstWakeModels.Location = new System.Drawing.Point(663, 73);
            this.lstWakeModels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstWakeModels.Name = "lstWakeModels";
            this.lstWakeModels.Size = new System.Drawing.Size(951, 122);
            this.lstWakeModels.TabIndex = 4;
            this.lstWakeModels.UseCompatibleStateImageBehavior = false;
            this.lstWakeModels.View = System.Windows.Forms.View.Details;
            this.lstWakeModels.SelectedIndexChanged += new System.EventHandler(this.lstWakeModels_SelectedIndexChanged);
            // 
            // ColumnHeader88
            // 
            this.ColumnHeader88.Text = "Wake Model";
            this.ColumnHeader88.Width = 115;
            // 
            // ColumnHeader106
            // 
            this.ColumnHeader106.Text = "Combo";
            // 
            // ColumnHeader89
            // 
            this.ColumnHeader89.Text = "Turbine";
            this.ColumnHeader89.Width = 119;
            // 
            // ColumnHeader107
            // 
            this.ColumnHeader107.Text = "WRR";
            // 
            // ColumnHeader90
            // 
            this.ColumnHeader90.Text = "Exp. Angle";
            this.ColumnHeader90.Width = 76;
            // 
            // ColumnHeader94
            // 
            this.ColumnHeader94.Text = "Amb. TI";
            // 
            // ColumnHeader95
            // 
            this.ColumnHeader95.Text = "DW Spc";
            // 
            // ColumnHeader96
            // 
            this.ColumnHeader96.Text = "CW Spc";
            // 
            // ColumnHeader97
            // 
            this.ColumnHeader97.Text = "Roughness";
            this.ColumnHeader97.Width = 81;
            // 
            // ColumnHeader98
            // 
            this.ColumnHeader98.Text = "Min X";
            // 
            // ColumnHeader99
            // 
            this.ColumnHeader99.Text = "Min Y";
            // 
            // ColumnHeader100
            // 
            this.ColumnHeader100.Text = "Max X";
            // 
            // ColumnHeader101
            // 
            this.ColumnHeader101.Text = "Max Y";
            // 
            // ColumnHeader102
            // 
            this.ColumnHeader102.Text = "Map Reso";
            this.ColumnHeader102.Width = 78;
            // 
            // btnWakeLossCalc
            // 
            this.btnWakeLossCalc.BackColor = System.Drawing.Color.LightCoral;
            this.btnWakeLossCalc.Location = new System.Drawing.Point(822, 9);
            this.btnWakeLossCalc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnWakeLossCalc.Name = "btnWakeLossCalc";
            this.btnWakeLossCalc.Size = new System.Drawing.Size(238, 53);
            this.btnWakeLossCalc.TabIndex = 3;
            this.btnWakeLossCalc.Text = "Create Wake Model && Calculate Turbine Wake Losses";
            this.btnWakeLossCalc.UseVisualStyleBackColor = false;
            this.btnWakeLossCalc.Click += new System.EventHandler(this.btnWakeLossCalc_Click);
            // 
            // Label53
            // 
            this.Label53.AutoSize = true;
            this.Label53.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label53.Location = new System.Drawing.Point(665, 36);
            this.Label53.Name = "Label53";
            this.Label53.Size = new System.Drawing.Size(135, 23);
            this.Label53.TabIndex = 2;
            this.Label53.Text = "Wake Models";
            // 
            // lstWakedTurbs
            // 
            this.lstWakedTurbs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SiteName,
            this.StringNum,
            this.ColumnHeader53,
            this.ColumnHeader60,
            this.ColumnHeader61,
            this.ColumnHeader113,
            this.ColumnHeader62,
            this.ColumnHeader63,
            this.ColumnHeader77});
            this.lstWakedTurbs.FullRowSelect = true;
            this.lstWakedTurbs.HideSelection = false;
            this.lstWakedTurbs.Location = new System.Drawing.Point(17, 73);
            this.lstWakedTurbs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstWakedTurbs.Name = "lstWakedTurbs";
            this.lstWakedTurbs.Size = new System.Drawing.Size(621, 386);
            this.lstWakedTurbs.TabIndex = 1;
            this.lstWakedTurbs.UseCompatibleStateImageBehavior = false;
            this.lstWakedTurbs.View = System.Windows.Forms.View.Details;
            // 
            // SiteName
            // 
            this.SiteName.Text = "Site";
            this.SiteName.Width = 61;
            // 
            // StringNum
            // 
            this.StringNum.Text = "String #";
            // 
            // ColumnHeader53
            // 
            this.ColumnHeader53.Text = "Elev., m";
            // 
            // ColumnHeader60
            // 
            this.ColumnHeader60.Text = "WS, m/s";
            // 
            // ColumnHeader61
            // 
            this.ColumnHeader61.Text = "Net AEP, MWh";
            this.ColumnHeader61.Width = 94;
            // 
            // ColumnHeader113
            // 
            this.ColumnHeader113.Text = "Net CF";
            // 
            // ColumnHeader62
            // 
            this.ColumnHeader62.Text = "Wake Loss";
            this.ColumnHeader62.Width = 77;
            // 
            // ColumnHeader63
            // 
            this.ColumnHeader63.Text = "Weibull k";
            // 
            // ColumnHeader77
            // 
            this.ColumnHeader77.Text = "Weibull A";
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label36.Location = new System.Drawing.Point(10, 18);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(226, 25);
            this.Label36.TabIndex = 0;
            this.Label36.Text = "Turbine Net Estimates";
            // 
            // pgeSiteConditions
            // 
            this.pgeSiteConditions.Controls.Add(this.panel4);
            this.pgeSiteConditions.Controls.Add(this.panel3);
            this.pgeSiteConditions.Controls.Add(this.panel2);
            this.pgeSiteConditions.Controls.Add(this.panel1);
            this.pgeSiteConditions.Controls.Add(this.label92);
            this.pgeSiteConditions.Location = new System.Drawing.Point(4, 27);
            this.pgeSiteConditions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeSiteConditions.Name = "pgeSiteConditions";
            this.pgeSiteConditions.Size = new System.Drawing.Size(1627, 849);
            this.pgeSiteConditions.TabIndex = 20;
            this.pgeSiteConditions.Text = "Site Conditions";
            this.pgeSiteConditions.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.plotInflowAngle);
            this.panel4.Controls.Add(this.label200);
            this.panel4.Controls.Add(this.txtInflowAngle);
            this.panel4.Controls.Add(this.label199);
            this.panel4.Controls.Add(this.cboInflowReso);
            this.panel4.Controls.Add(this.label198);
            this.panel4.Controls.Add(this.cboInflowRadius);
            this.panel4.Controls.Add(this.btnInflowAngles);
            this.panel4.Controls.Add(this.cboInflowTurbine);
            this.panel4.Controls.Add(this.label197);
            this.panel4.Controls.Add(this.label196);
            this.panel4.Controls.Add(this.cboInflowWD);
            this.panel4.Controls.Add(this.label171);
            this.panel4.Location = new System.Drawing.Point(821, 442);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(806, 377);
            this.panel4.TabIndex = 6;
            // 
            // plotInflowAngle
            // 
            this.plotInflowAngle.Location = new System.Drawing.Point(326, 33);
            this.plotInflowAngle.Name = "plotInflowAngle";
            this.plotInflowAngle.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotInflowAngle.Size = new System.Drawing.Size(444, 294);
            this.plotInflowAngle.TabIndex = 277;
            this.plotInflowAngle.Text = "plotView1";
            this.plotInflowAngle.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotInflowAngle.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotInflowAngle.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // label200
            // 
            this.label200.AutoSize = true;
            this.label200.Location = new System.Drawing.Point(19, 207);
            this.label200.Name = "label200";
            this.label200.Size = new System.Drawing.Size(143, 19);
            this.label200.TabIndex = 276;
            this.label200.Text = "Inflow Angle (degs) :";
            // 
            // txtInflowAngle
            // 
            this.txtInflowAngle.Enabled = false;
            this.txtInflowAngle.Location = new System.Drawing.Point(166, 203);
            this.txtInflowAngle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtInflowAngle.Name = "txtInflowAngle";
            this.txtInflowAngle.Size = new System.Drawing.Size(96, 25);
            this.txtInflowAngle.TabIndex = 275;
            // 
            // label199
            // 
            this.label199.AutoSize = true;
            this.label199.Location = new System.Drawing.Point(8, 171);
            this.label199.Name = "label199";
            this.label199.Size = new System.Drawing.Size(85, 19);
            this.label199.TabIndex = 274;
            this.label199.Text = "Resolution :";
            // 
            // cboInflowReso
            // 
            this.cboInflowReso.FormattingEnabled = true;
            this.cboInflowReso.Items.AddRange(new object[] {
            "25",
            "50",
            "75",
            "100"});
            this.cboInflowReso.Location = new System.Drawing.Point(98, 165);
            this.cboInflowReso.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboInflowReso.Name = "cboInflowReso";
            this.cboInflowReso.Size = new System.Drawing.Size(84, 26);
            this.cboInflowReso.TabIndex = 273;
            this.cboInflowReso.SelectedIndexChanged += new System.EventHandler(this.cboInflowReso_SelectedIndexChanged);
            // 
            // label198
            // 
            this.label198.AutoSize = true;
            this.label198.Location = new System.Drawing.Point(28, 130);
            this.label198.Name = "label198";
            this.label198.Size = new System.Drawing.Size(60, 19);
            this.label198.TabIndex = 272;
            this.label198.Text = "Radius :";
            // 
            // cboInflowRadius
            // 
            this.cboInflowRadius.FormattingEnabled = true;
            this.cboInflowRadius.Items.AddRange(new object[] {
            "100",
            "200",
            "300",
            "400",
            "500",
            "600",
            "700",
            "800",
            "900",
            "1000"});
            this.cboInflowRadius.Location = new System.Drawing.Point(98, 127);
            this.cboInflowRadius.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboInflowRadius.Name = "cboInflowRadius";
            this.cboInflowRadius.Size = new System.Drawing.Size(84, 26);
            this.cboInflowRadius.TabIndex = 271;
            this.cboInflowRadius.SelectedIndexChanged += new System.EventHandler(this.cboInflowRadius_SelectedIndexChanged);
            // 
            // btnInflowAngles
            // 
            this.btnInflowAngles.Location = new System.Drawing.Point(26, 284);
            this.btnInflowAngles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnInflowAngles.Name = "btnInflowAngles";
            this.btnInflowAngles.Size = new System.Drawing.Size(93, 77);
            this.btnInflowAngles.TabIndex = 270;
            this.btnInflowAngles.Text = "Export Inflow Angles";
            this.btnInflowAngles.UseVisualStyleBackColor = true;
            this.btnInflowAngles.Click += new System.EventHandler(this.btnInflowAngles_Click);
            // 
            // cboInflowTurbine
            // 
            this.cboInflowTurbine.FormattingEnabled = true;
            this.cboInflowTurbine.Items.AddRange(new object[] {
            "Average",
            "Representative",
            "Effective"});
            this.cboInflowTurbine.Location = new System.Drawing.Point(96, 55);
            this.cboInflowTurbine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboInflowTurbine.Name = "cboInflowTurbine";
            this.cboInflowTurbine.Size = new System.Drawing.Size(173, 26);
            this.cboInflowTurbine.TabIndex = 263;
            this.cboInflowTurbine.SelectedIndexChanged += new System.EventHandler(this.cboInflowTurbine_SelectedIndexChanged);
            // 
            // label197
            // 
            this.label197.AutoSize = true;
            this.label197.Location = new System.Drawing.Point(24, 58);
            this.label197.Name = "label197";
            this.label197.Size = new System.Drawing.Size(67, 19);
            this.label197.TabIndex = 262;
            this.label197.Text = "Turbine :";
            // 
            // label196
            // 
            this.label196.AutoSize = true;
            this.label196.Location = new System.Drawing.Point(43, 92);
            this.label196.Name = "label196";
            this.label196.Size = new System.Drawing.Size(42, 19);
            this.label196.TabIndex = 199;
            this.label196.Text = "WD :";
            // 
            // cboInflowWD
            // 
            this.cboInflowWD.FormattingEnabled = true;
            this.cboInflowWD.Location = new System.Drawing.Point(97, 90);
            this.cboInflowWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboInflowWD.Name = "cboInflowWD";
            this.cboInflowWD.Size = new System.Drawing.Size(84, 26);
            this.cboInflowWD.TabIndex = 198;
            this.cboInflowWD.SelectedIndexChanged += new System.EventHandler(this.cboInflowWD_SelectedIndexChanged);
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label171.Location = new System.Drawing.Point(22, 20);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(123, 23);
            this.label171.TabIndex = 3;
            this.label171.Text = "Inflow Angle";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.plotExtremeWS);
            this.panel3.Controls.Add(this.lblNoExtremeWS);
            this.panel3.Controls.Add(this.lblExtremeWS);
            this.panel3.Controls.Add(this.btnExtremeWS);
            this.panel3.Controls.Add(this.label195);
            this.panel3.Controls.Add(this.cboExtremeWSMet);
            this.panel3.Controls.Add(this.txt1yrExtremeGust);
            this.panel3.Controls.Add(this.txt1yrExtreme10min);
            this.panel3.Controls.Add(this.txt50yrExtremeGust);
            this.panel3.Controls.Add(this.txt50yrExtreme10min);
            this.panel3.Controls.Add(this.label193);
            this.panel3.Controls.Add(this.label194);
            this.panel3.Controls.Add(this.label192);
            this.panel3.Controls.Add(this.label191);
            this.panel3.Controls.Add(this.label170);
            this.panel3.Location = new System.Drawing.Point(9, 442);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(797, 377);
            this.panel3.TabIndex = 5;
            // 
            // plotExtremeWS
            // 
            this.plotExtremeWS.Location = new System.Drawing.Point(328, 33);
            this.plotExtremeWS.Name = "plotExtremeWS";
            this.plotExtremeWS.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotExtremeWS.Size = new System.Drawing.Size(444, 251);
            this.plotExtremeWS.TabIndex = 272;
            this.plotExtremeWS.Text = "plotView1";
            this.plotExtremeWS.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotExtremeWS.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotExtremeWS.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // lblNoExtremeWS
            // 
            this.lblNoExtremeWS.AutoSize = true;
            this.lblNoExtremeWS.Location = new System.Drawing.Point(151, 347);
            this.lblNoExtremeWS.Name = "lblNoExtremeWS";
            this.lblNoExtremeWS.Size = new System.Drawing.Size(0, 19);
            this.lblNoExtremeWS.TabIndex = 271;
            // 
            // lblExtremeWS
            // 
            this.lblExtremeWS.AutoSize = true;
            this.lblExtremeWS.Location = new System.Drawing.Point(150, 284);
            this.lblExtremeWS.Name = "lblExtremeWS";
            this.lblExtremeWS.Size = new System.Drawing.Size(0, 19);
            this.lblExtremeWS.TabIndex = 270;
            // 
            // btnExtremeWS
            // 
            this.btnExtremeWS.Location = new System.Drawing.Point(26, 284);
            this.btnExtremeWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExtremeWS.Name = "btnExtremeWS";
            this.btnExtremeWS.Size = new System.Drawing.Size(100, 63);
            this.btnExtremeWS.TabIndex = 269;
            this.btnExtremeWS.Text = "Export Extreme WS";
            this.btnExtremeWS.UseVisualStyleBackColor = true;
            this.btnExtremeWS.Click += new System.EventHandler(this.btnExtremeWS_Click);
            // 
            // label195
            // 
            this.label195.AutoSize = true;
            this.label195.Location = new System.Drawing.Point(19, 65);
            this.label195.Name = "label195";
            this.label195.Size = new System.Drawing.Size(41, 19);
            this.label195.TabIndex = 210;
            this.label195.Text = "Met :";
            // 
            // cboExtremeWSMet
            // 
            this.cboExtremeWSMet.FormattingEnabled = true;
            this.cboExtremeWSMet.Location = new System.Drawing.Point(66, 61);
            this.cboExtremeWSMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExtremeWSMet.Name = "cboExtremeWSMet";
            this.cboExtremeWSMet.Size = new System.Drawing.Size(227, 26);
            this.cboExtremeWSMet.TabIndex = 209;
            this.cboExtremeWSMet.SelectedIndexChanged += new System.EventHandler(this.cboExtremeWSMet_SelectedIndexChanged);
            // 
            // txt1yrExtremeGust
            // 
            this.txt1yrExtremeGust.Location = new System.Drawing.Point(193, 236);
            this.txt1yrExtremeGust.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt1yrExtremeGust.Name = "txt1yrExtremeGust";
            this.txt1yrExtremeGust.Size = new System.Drawing.Size(96, 25);
            this.txt1yrExtremeGust.TabIndex = 11;
            // 
            // txt1yrExtreme10min
            // 
            this.txt1yrExtreme10min.Location = new System.Drawing.Point(193, 201);
            this.txt1yrExtreme10min.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt1yrExtreme10min.Name = "txt1yrExtreme10min";
            this.txt1yrExtreme10min.Size = new System.Drawing.Size(96, 25);
            this.txt1yrExtreme10min.TabIndex = 10;
            // 
            // txt50yrExtremeGust
            // 
            this.txt50yrExtremeGust.Location = new System.Drawing.Point(194, 167);
            this.txt50yrExtremeGust.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt50yrExtremeGust.Name = "txt50yrExtremeGust";
            this.txt50yrExtremeGust.Size = new System.Drawing.Size(96, 25);
            this.txt50yrExtremeGust.TabIndex = 9;
            // 
            // txt50yrExtreme10min
            // 
            this.txt50yrExtreme10min.Location = new System.Drawing.Point(195, 133);
            this.txt50yrExtreme10min.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt50yrExtreme10min.Name = "txt50yrExtreme10min";
            this.txt50yrExtreme10min.Size = new System.Drawing.Size(96, 25);
            this.txt50yrExtreme10min.TabIndex = 8;
            // 
            // label193
            // 
            this.label193.AutoSize = true;
            this.label193.Location = new System.Drawing.Point(21, 236);
            this.label193.Name = "label193";
            this.label193.Size = new System.Drawing.Size(166, 19);
            this.label193.TabIndex = 7;
            this.label193.Text = "1 yr Extreme (3-sec gust)";
            // 
            // label194
            // 
            this.label194.AutoSize = true;
            this.label194.Location = new System.Drawing.Point(21, 203);
            this.label194.Name = "label194";
            this.label194.Size = new System.Drawing.Size(147, 19);
            this.label194.TabIndex = 6;
            this.label194.Text = "1 yr Extreme (10 min)";
            // 
            // label192
            // 
            this.label192.AutoSize = true;
            this.label192.Location = new System.Drawing.Point(22, 171);
            this.label192.Name = "label192";
            this.label192.Size = new System.Drawing.Size(173, 19);
            this.label192.TabIndex = 5;
            this.label192.Text = "50 yr Extreme (3-sec gust)";
            // 
            // label191
            // 
            this.label191.AutoSize = true;
            this.label191.Location = new System.Drawing.Point(22, 137);
            this.label191.Name = "label191";
            this.label191.Size = new System.Drawing.Size(154, 19);
            this.label191.TabIndex = 4;
            this.label191.Text = "50 yr Extreme (10 min)";
            // 
            // label170
            // 
            this.label170.AutoSize = true;
            this.label170.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label170.Location = new System.Drawing.Point(22, 20);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(201, 23);
            this.label170.TabIndex = 3;
            this.label170.Text = "Extreme Wind Speed";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.plotExtremeShear);
            this.panel2.Controls.Add(this.dateTimeExtremeShearStart);
            this.panel2.Controls.Add(this.label202);
            this.panel2.Controls.Add(this.label203);
            this.panel2.Controls.Add(this.dateTimeExtremeShearEnd);
            this.panel2.Controls.Add(this.label201);
            this.panel2.Controls.Add(this.label190);
            this.panel2.Controls.Add(this.cboExtremeShearRange);
            this.panel2.Controls.Add(this.label189);
            this.panel2.Controls.Add(this.cboExtremeShearMet);
            this.panel2.Controls.Add(this.btnExportShearStats);
            this.panel2.Controls.Add(this.lstExtremeShear);
            this.panel2.Controls.Add(this.label95);
            this.panel2.Location = new System.Drawing.Point(821, 47);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(806, 388);
            this.panel2.TabIndex = 4;
            // 
            // plotExtremeShear
            // 
            this.plotExtremeShear.Location = new System.Drawing.Point(32, 113);
            this.plotExtremeShear.Name = "plotExtremeShear";
            this.plotExtremeShear.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotExtremeShear.Size = new System.Drawing.Size(394, 251);
            this.plotExtremeShear.TabIndex = 279;
            this.plotExtremeShear.Text = "plotView1";
            this.plotExtremeShear.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotExtremeShear.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotExtremeShear.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // dateTimeExtremeShearStart
            // 
            this.dateTimeExtremeShearStart.CustomFormat = "MM/dd/yy";
            this.dateTimeExtremeShearStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeExtremeShearStart.Location = new System.Drawing.Point(553, 41);
            this.dateTimeExtremeShearStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimeExtremeShearStart.Name = "dateTimeExtremeShearStart";
            this.dateTimeExtremeShearStart.Size = new System.Drawing.Size(76, 25);
            this.dateTimeExtremeShearStart.TabIndex = 278;
            this.dateTimeExtremeShearStart.ValueChanged += new System.EventHandler(this.dateTimeExtremeShearStart_ValueChanged);
            // 
            // label202
            // 
            this.label202.AutoSize = true;
            this.label202.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label202.Location = new System.Drawing.Point(644, 45);
            this.label202.Name = "label202";
            this.label202.Size = new System.Drawing.Size(29, 18);
            this.label202.TabIndex = 277;
            this.label202.Text = "To :";
            // 
            // label203
            // 
            this.label203.AutoSize = true;
            this.label203.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label203.Location = new System.Drawing.Point(506, 45);
            this.label203.Name = "label203";
            this.label203.Size = new System.Drawing.Size(46, 18);
            this.label203.TabIndex = 276;
            this.label203.Text = "From :";
            // 
            // dateTimeExtremeShearEnd
            // 
            this.dateTimeExtremeShearEnd.CustomFormat = "MM/dd/yy";
            this.dateTimeExtremeShearEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeExtremeShearEnd.Location = new System.Drawing.Point(678, 41);
            this.dateTimeExtremeShearEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimeExtremeShearEnd.Name = "dateTimeExtremeShearEnd";
            this.dateTimeExtremeShearEnd.Size = new System.Drawing.Size(76, 25);
            this.dateTimeExtremeShearEnd.TabIndex = 275;
            this.dateTimeExtremeShearEnd.ValueChanged += new System.EventHandler(this.dateTimeExtremeShearEnd_ValueChanged);
            // 
            // label201
            // 
            this.label201.AutoSize = true;
            this.label201.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label201.Location = new System.Drawing.Point(450, 113);
            this.label201.Name = "label201";
            this.label201.Size = new System.Drawing.Size(175, 19);
            this.label201.TabIndex = 274;
            this.label201.Text = "Extreme Shear Values";
            // 
            // label190
            // 
            this.label190.AutoSize = true;
            this.label190.Location = new System.Drawing.Point(223, 46);
            this.label190.Name = "label190";
            this.label190.Size = new System.Drawing.Size(135, 19);
            this.label190.TabIndex = 273;
            this.label190.Text = "Wind Speed Range :";
            // 
            // cboExtremeShearRange
            // 
            this.cboExtremeShearRange.FormattingEnabled = true;
            this.cboExtremeShearRange.Items.AddRange(new object[] {
            "5 - 10 m/s",
            "10 - 15 m/s",
            "15+ m/s",
            "All > Cut-In"});
            this.cboExtremeShearRange.Location = new System.Drawing.Point(359, 42);
            this.cboExtremeShearRange.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExtremeShearRange.Name = "cboExtremeShearRange";
            this.cboExtremeShearRange.Size = new System.Drawing.Size(104, 26);
            this.cboExtremeShearRange.TabIndex = 272;
            this.cboExtremeShearRange.SelectedIndexChanged += new System.EventHandler(this.cboExtremeShearRange_SelectedIndexChanged);
            // 
            // label189
            // 
            this.label189.AutoSize = true;
            this.label189.Location = new System.Drawing.Point(233, 15);
            this.label189.Name = "label189";
            this.label189.Size = new System.Drawing.Size(41, 19);
            this.label189.TabIndex = 271;
            this.label189.Text = "Met :";
            // 
            // cboExtremeShearMet
            // 
            this.cboExtremeShearMet.FormattingEnabled = true;
            this.cboExtremeShearMet.Location = new System.Drawing.Point(276, 9);
            this.cboExtremeShearMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExtremeShearMet.Name = "cboExtremeShearMet";
            this.cboExtremeShearMet.Size = new System.Drawing.Size(187, 26);
            this.cboExtremeShearMet.TabIndex = 270;
            this.cboExtremeShearMet.SelectedIndexChanged += new System.EventHandler(this.cboExtremeShearMet_SelectedIndexChanged);
            // 
            // btnExportShearStats
            // 
            this.btnExportShearStats.Location = new System.Drawing.Point(687, 303);
            this.btnExportShearStats.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportShearStats.Name = "btnExportShearStats";
            this.btnExportShearStats.Size = new System.Drawing.Size(101, 66);
            this.btnExportShearStats.TabIndex = 269;
            this.btnExportShearStats.Text = "Export Shear Stats";
            this.btnExportShearStats.UseVisualStyleBackColor = true;
            this.btnExportShearStats.Click += new System.EventHandler(this.btnExportShearStats_Click);
            // 
            // lstExtremeShear
            // 
            this.lstExtremeShear.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader180,
            this.columnHeader181,
            this.columnHeader182,
            this.columnHeader183,
            this.columnHeader184});
            this.lstExtremeShear.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstExtremeShear.GridLines = true;
            this.lstExtremeShear.HideSelection = false;
            this.lstExtremeShear.Location = new System.Drawing.Point(454, 148);
            this.lstExtremeShear.Margin = new System.Windows.Forms.Padding(2);
            this.lstExtremeShear.Name = "lstExtremeShear";
            this.lstExtremeShear.Size = new System.Drawing.Size(334, 142);
            this.lstExtremeShear.TabIndex = 267;
            this.lstExtremeShear.UseCompatibleStateImageBehavior = false;
            this.lstExtremeShear.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader180
            // 
            this.columnHeader180.Text = "";
            this.columnHeader180.Width = 32;
            // 
            // columnHeader181
            // 
            this.columnHeader181.Text = "5-10 m/s";
            this.columnHeader181.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader181.Width = 56;
            // 
            // columnHeader182
            // 
            this.columnHeader182.Text = "10-15 m/s";
            this.columnHeader182.Width = 59;
            // 
            // columnHeader183
            // 
            this.columnHeader183.Text = "15+ m/s";
            // 
            // columnHeader184
            // 
            this.columnHeader184.Text = "All > Cut-In";
            this.columnHeader184.Width = 72;
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label95.Location = new System.Drawing.Point(22, 20);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(193, 23);
            this.label95.TabIndex = 3;
            this.label95.Text = "Extreme Wind Shear";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.plotTurbInt);
            this.panel1.Controls.Add(this.btnExportTI);
            this.panel1.Controls.Add(this.label188);
            this.panel1.Controls.Add(this.cboTurbPowerCurve);
            this.panel1.Controls.Add(this.dateTIStart);
            this.panel1.Controls.Add(this.label186);
            this.panel1.Controls.Add(this.label187);
            this.panel1.Controls.Add(this.dateTIEnd);
            this.panel1.Controls.Add(this.cboTurbineTI);
            this.panel1.Controls.Add(this.label179);
            this.panel1.Controls.Add(this.label178);
            this.panel1.Controls.Add(this.cboEffectiveTI_m);
            this.panel1.Controls.Add(this.cboTI_Type);
            this.panel1.Controls.Add(this.label177);
            this.panel1.Controls.Add(this.lstTurbulence);
            this.panel1.Controls.Add(this.label174);
            this.panel1.Controls.Add(this.cboTurbMet);
            this.panel1.Controls.Add(this.label172);
            this.panel1.Controls.Add(this.cboTurbWD);
            this.panel1.Controls.Add(this.label93);
            this.panel1.Location = new System.Drawing.Point(9, 47);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 388);
            this.panel1.TabIndex = 3;
            // 
            // plotTurbInt
            // 
            this.plotTurbInt.Location = new System.Drawing.Point(259, 113);
            this.plotTurbInt.Name = "plotTurbInt";
            this.plotTurbInt.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTurbInt.Size = new System.Drawing.Size(444, 251);
            this.plotTurbInt.TabIndex = 269;
            this.plotTurbInt.Text = "plotView1";
            this.plotTurbInt.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTurbInt.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTurbInt.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnExportTI
            // 
            this.btnExportTI.Location = new System.Drawing.Point(709, 113);
            this.btnExportTI.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportTI.Name = "btnExportTI";
            this.btnExportTI.Size = new System.Drawing.Size(87, 62);
            this.btnExportTI.TabIndex = 268;
            this.btnExportTI.Text = "Export TI";
            this.btnExportTI.UseVisualStyleBackColor = true;
            this.btnExportTI.Click += new System.EventHandler(this.btnExportTI_Click);
            // 
            // label188
            // 
            this.label188.AutoSize = true;
            this.label188.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label188.Location = new System.Drawing.Point(461, 18);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(93, 18);
            this.label188.TabIndex = 267;
            this.label188.Text = "Power Curve :";
            // 
            // cboTurbPowerCurve
            // 
            this.cboTurbPowerCurve.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTurbPowerCurve.FormattingEnabled = true;
            this.cboTurbPowerCurve.Location = new System.Drawing.Point(559, 15);
            this.cboTurbPowerCurve.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTurbPowerCurve.Name = "cboTurbPowerCurve";
            this.cboTurbPowerCurve.Size = new System.Drawing.Size(229, 26);
            this.cboTurbPowerCurve.TabIndex = 266;
            // 
            // dateTIStart
            // 
            this.dateTIStart.CustomFormat = "MM/dd/yy";
            this.dateTIStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTIStart.Location = new System.Drawing.Point(503, 48);
            this.dateTIStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTIStart.Name = "dateTIStart";
            this.dateTIStart.Size = new System.Drawing.Size(76, 25);
            this.dateTIStart.TabIndex = 265;
            this.dateTIStart.ValueChanged += new System.EventHandler(this.dateTIStart_ValueChanged);
            // 
            // label186
            // 
            this.label186.AutoSize = true;
            this.label186.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label186.Location = new System.Drawing.Point(594, 52);
            this.label186.Name = "label186";
            this.label186.Size = new System.Drawing.Size(29, 18);
            this.label186.TabIndex = 264;
            this.label186.Text = "To :";
            // 
            // label187
            // 
            this.label187.AutoSize = true;
            this.label187.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label187.Location = new System.Drawing.Point(456, 52);
            this.label187.Name = "label187";
            this.label187.Size = new System.Drawing.Size(46, 18);
            this.label187.TabIndex = 263;
            this.label187.Text = "From :";
            // 
            // dateTIEnd
            // 
            this.dateTIEnd.CustomFormat = "MM/dd/yy";
            this.dateTIEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTIEnd.Location = new System.Drawing.Point(628, 48);
            this.dateTIEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTIEnd.Name = "dateTIEnd";
            this.dateTIEnd.Size = new System.Drawing.Size(76, 25);
            this.dateTIEnd.TabIndex = 262;
            this.dateTIEnd.ValueChanged += new System.EventHandler(this.dateTIEnd_ValueChanged);
            // 
            // cboTurbineTI
            // 
            this.cboTurbineTI.FormattingEnabled = true;
            this.cboTurbineTI.Items.AddRange(new object[] {
            "Average",
            "Representative",
            "Effective"});
            this.cboTurbineTI.Location = new System.Drawing.Point(532, 80);
            this.cboTurbineTI.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTurbineTI.Name = "cboTurbineTI";
            this.cboTurbineTI.Size = new System.Drawing.Size(173, 26);
            this.cboTurbineTI.TabIndex = 261;
            this.cboTurbineTI.SelectedIndexChanged += new System.EventHandler(this.cboTurbineTI_SelectedIndexChanged);
            // 
            // label179
            // 
            this.label179.AutoSize = true;
            this.label179.Location = new System.Drawing.Point(467, 82);
            this.label179.Name = "label179";
            this.label179.Size = new System.Drawing.Size(67, 19);
            this.label179.TabIndex = 260;
            this.label179.Text = "Turbine :";
            // 
            // label178
            // 
            this.label178.AutoSize = true;
            this.label178.Location = new System.Drawing.Point(374, 84);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(29, 19);
            this.label178.TabIndex = 259;
            this.label178.Text = "m :";
            // 
            // cboEffectiveTI_m
            // 
            this.cboEffectiveTI_m.FormattingEnabled = true;
            this.cboEffectiveTI_m.Items.AddRange(new object[] {
            "1",
            "10"});
            this.cboEffectiveTI_m.Location = new System.Drawing.Point(403, 79);
            this.cboEffectiveTI_m.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboEffectiveTI_m.Name = "cboEffectiveTI_m";
            this.cboEffectiveTI_m.Size = new System.Drawing.Size(55, 26);
            this.cboEffectiveTI_m.TabIndex = 258;
            this.cboEffectiveTI_m.SelectedIndexChanged += new System.EventHandler(this.cboEffectiveTI_m_SelectedIndexChanged);
            // 
            // cboTI_Type
            // 
            this.cboTI_Type.FormattingEnabled = true;
            this.cboTI_Type.Items.AddRange(new object[] {
            "Average",
            "Representative",
            "Effective"});
            this.cboTI_Type.Location = new System.Drawing.Point(304, 48);
            this.cboTI_Type.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTI_Type.Name = "cboTI_Type";
            this.cboTI_Type.Size = new System.Drawing.Size(132, 26);
            this.cboTI_Type.TabIndex = 257;
            this.cboTI_Type.SelectedIndexChanged += new System.EventHandler(this.cboTI_Type_SelectedIndexChanged);
            // 
            // label177
            // 
            this.label177.AutoSize = true;
            this.label177.Location = new System.Drawing.Point(238, 50);
            this.label177.Name = "label177";
            this.label177.Size = new System.Drawing.Size(66, 19);
            this.label177.TabIndex = 256;
            this.label177.Text = "TI Type :";
            // 
            // lstTurbulence
            // 
            this.lstTurbulence.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader177,
            this.columnHeader178,
            this.columnHeader179});
            this.lstTurbulence.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTurbulence.GridLines = true;
            this.lstTurbulence.HideSelection = false;
            this.lstTurbulence.Location = new System.Drawing.Point(15, 68);
            this.lstTurbulence.Margin = new System.Windows.Forms.Padding(2);
            this.lstTurbulence.Name = "lstTurbulence";
            this.lstTurbulence.Size = new System.Drawing.Size(217, 290);
            this.lstTurbulence.TabIndex = 255;
            this.lstTurbulence.UseCompatibleStateImageBehavior = false;
            this.lstTurbulence.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader177
            // 
            this.columnHeader177.Text = "WS [m/s]";
            this.columnHeader177.Width = 62;
            // 
            // columnHeader178
            // 
            this.columnHeader178.Text = "TI";
            this.columnHeader178.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader178.Width = 56;
            // 
            // columnHeader179
            // 
            this.columnHeader179.Text = "Freq.";
            this.columnHeader179.Width = 59;
            // 
            // label174
            // 
            this.label174.AutoSize = true;
            this.label174.Location = new System.Drawing.Point(232, 18);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(41, 19);
            this.label174.TabIndex = 208;
            this.label174.Text = "Met :";
            // 
            // cboTurbMet
            // 
            this.cboTurbMet.FormattingEnabled = true;
            this.cboTurbMet.Location = new System.Drawing.Point(276, 16);
            this.cboTurbMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTurbMet.Name = "cboTurbMet";
            this.cboTurbMet.Size = new System.Drawing.Size(177, 26);
            this.cboTurbMet.TabIndex = 207;
            this.cboTurbMet.SelectedIndexChanged += new System.EventHandler(this.cboTurbMet_SelectedIndexChanged);
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.Location = new System.Drawing.Point(239, 82);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(42, 19);
            this.label172.TabIndex = 197;
            this.label172.Text = "WD :";
            // 
            // cboTurbWD
            // 
            this.cboTurbWD.FormattingEnabled = true;
            this.cboTurbWD.Location = new System.Drawing.Point(282, 79);
            this.cboTurbWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTurbWD.Name = "cboTurbWD";
            this.cboTurbWD.Size = new System.Drawing.Size(84, 26);
            this.cboTurbWD.TabIndex = 196;
            this.cboTurbWD.SelectedIndexChanged += new System.EventHandler(this.cboTurbWD_SelectedIndexChanged);
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.Location = new System.Drawing.Point(22, 20);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(191, 23);
            this.label93.TabIndex = 3;
            this.label93.Text = "Turbulence Intensity";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.Location = new System.Drawing.Point(17, 11);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(288, 25);
            this.label92.TabIndex = 1;
            this.label92.Text = "Summary of Site Conditions";
            // 
            // pgeMonthlyAnalysis
            // 
            this.pgeMonthlyAnalysis.Controls.Add(this.plotMonthlyTS);
            this.pgeMonthlyAnalysis.Controls.Add(this.plotYearlyTS);
            this.pgeMonthlyAnalysis.Controls.Add(this.label158);
            this.pgeMonthlyAnalysis.Controls.Add(this.cboMonthlyPowerCurve);
            this.pgeMonthlyAnalysis.Controls.Add(this.btnExportHourlyTurbineValues);
            this.pgeMonthlyAnalysis.Controls.Add(this.btnExportMonthlyTurbineValues);
            this.pgeMonthlyAnalysis.Controls.Add(this.btnExportYearlyTurbineValues);
            this.pgeMonthlyAnalysis.Controls.Add(this.cboMonthlyWakeModel);
            this.pgeMonthlyAnalysis.Controls.Add(this.label157);
            this.pgeMonthlyAnalysis.Controls.Add(this.label148);
            this.pgeMonthlyAnalysis.Controls.Add(this.chkSelectedTurbineParam);
            this.pgeMonthlyAnalysis.Controls.Add(this.label131);
            this.pgeMonthlyAnalysis.Controls.Add(this.cboSelectedTurbine);
            this.pgeMonthlyAnalysis.Controls.Add(this.label126);
            this.pgeMonthlyAnalysis.Controls.Add(this.label122);
            this.pgeMonthlyAnalysis.Controls.Add(this.chkSelectAllTurbineYears);
            this.pgeMonthlyAnalysis.Controls.Add(this.chkYears_Monthly);
            this.pgeMonthlyAnalysis.Controls.Add(this.lstYearlyTurbine);
            this.pgeMonthlyAnalysis.Controls.Add(this.lstMonthlyTurbine);
            this.pgeMonthlyAnalysis.Controls.Add(this.label163);
            this.pgeMonthlyAnalysis.Location = new System.Drawing.Point(4, 27);
            this.pgeMonthlyAnalysis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeMonthlyAnalysis.Name = "pgeMonthlyAnalysis";
            this.pgeMonthlyAnalysis.Size = new System.Drawing.Size(1627, 849);
            this.pgeMonthlyAnalysis.TabIndex = 17;
            this.pgeMonthlyAnalysis.Text = "Time Series Analysis";
            this.pgeMonthlyAnalysis.UseVisualStyleBackColor = true;
            // 
            // plotMonthlyTS
            // 
            this.plotMonthlyTS.Location = new System.Drawing.Point(820, 437);
            this.plotMonthlyTS.Name = "plotMonthlyTS";
            this.plotMonthlyTS.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMonthlyTS.Size = new System.Drawing.Size(750, 391);
            this.plotMonthlyTS.TabIndex = 279;
            this.plotMonthlyTS.Text = "plotView2";
            this.plotMonthlyTS.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMonthlyTS.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMonthlyTS.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotYearlyTS
            // 
            this.plotYearlyTS.Location = new System.Drawing.Point(15, 437);
            this.plotYearlyTS.Name = "plotYearlyTS";
            this.plotYearlyTS.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotYearlyTS.Size = new System.Drawing.Size(750, 391);
            this.plotYearlyTS.TabIndex = 278;
            this.plotYearlyTS.Text = "plotView1";
            this.plotYearlyTS.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotYearlyTS.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotYearlyTS.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label158.Location = new System.Drawing.Point(180, 17);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(93, 18);
            this.label158.TabIndex = 229;
            this.label158.Text = "Power Curve :";
            // 
            // cboMonthlyPowerCurve
            // 
            this.cboMonthlyPowerCurve.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMonthlyPowerCurve.FormattingEnabled = true;
            this.cboMonthlyPowerCurve.Location = new System.Drawing.Point(279, 15);
            this.cboMonthlyPowerCurve.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMonthlyPowerCurve.Name = "cboMonthlyPowerCurve";
            this.cboMonthlyPowerCurve.Size = new System.Drawing.Size(321, 26);
            this.cboMonthlyPowerCurve.TabIndex = 228;
            this.cboMonthlyPowerCurve.SelectedIndexChanged += new System.EventHandler(this.cboMonthlyPowerCurve_SelectedIndexChanged);
            // 
            // btnExportHourlyTurbineValues
            // 
            this.btnExportHourlyTurbineValues.Location = new System.Drawing.Point(647, 49);
            this.btnExportHourlyTurbineValues.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportHourlyTurbineValues.Name = "btnExportHourlyTurbineValues";
            this.btnExportHourlyTurbineValues.Size = new System.Drawing.Size(100, 70);
            this.btnExportHourlyTurbineValues.TabIndex = 227;
            this.btnExportHourlyTurbineValues.Text = "Export Hourly Values";
            this.btnExportHourlyTurbineValues.UseVisualStyleBackColor = true;
            this.btnExportHourlyTurbineValues.Click += new System.EventHandler(this.btnExportHourlyTurbineValues_Click);
            // 
            // btnExportMonthlyTurbineValues
            // 
            this.btnExportMonthlyTurbineValues.Location = new System.Drawing.Point(647, 127);
            this.btnExportMonthlyTurbineValues.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportMonthlyTurbineValues.Name = "btnExportMonthlyTurbineValues";
            this.btnExportMonthlyTurbineValues.Size = new System.Drawing.Size(100, 72);
            this.btnExportMonthlyTurbineValues.TabIndex = 226;
            this.btnExportMonthlyTurbineValues.Text = "Export Monthly Values";
            this.btnExportMonthlyTurbineValues.UseVisualStyleBackColor = true;
            this.btnExportMonthlyTurbineValues.Click += new System.EventHandler(this.btnExportMonthlyTurbineValues_Click);
            // 
            // btnExportYearlyTurbineValues
            // 
            this.btnExportYearlyTurbineValues.Location = new System.Drawing.Point(647, 207);
            this.btnExportYearlyTurbineValues.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportYearlyTurbineValues.Name = "btnExportYearlyTurbineValues";
            this.btnExportYearlyTurbineValues.Size = new System.Drawing.Size(100, 67);
            this.btnExportYearlyTurbineValues.TabIndex = 225;
            this.btnExportYearlyTurbineValues.Text = "Export Annual Values";
            this.btnExportYearlyTurbineValues.UseVisualStyleBackColor = true;
            this.btnExportYearlyTurbineValues.Click += new System.EventHandler(this.btnExportYearlyTurbineValues_Click);
            // 
            // cboMonthlyWakeModel
            // 
            this.cboMonthlyWakeModel.FormattingEnabled = true;
            this.cboMonthlyWakeModel.Location = new System.Drawing.Point(279, 48);
            this.cboMonthlyWakeModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMonthlyWakeModel.Name = "cboMonthlyWakeModel";
            this.cboMonthlyWakeModel.Size = new System.Drawing.Size(321, 26);
            this.cboMonthlyWakeModel.TabIndex = 224;
            this.cboMonthlyWakeModel.SelectedIndexChanged += new System.EventHandler(this.cboMonthlyWakeModel_SelectedIndexChanged);
            // 
            // label157
            // 
            this.label157.AutoSize = true;
            this.label157.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label157.Location = new System.Drawing.Point(180, 49);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(84, 18);
            this.label157.TabIndex = 223;
            this.label157.Text = "Wake Model:";
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label148.Location = new System.Drawing.Point(646, 293);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(101, 17);
            this.label148.TabIndex = 220;
            this.label148.Text = "Plot Parameters:";
            // 
            // chkSelectedTurbineParam
            // 
            this.chkSelectedTurbineParam.CheckOnClick = true;
            this.chkSelectedTurbineParam.FormattingEnabled = true;
            this.chkSelectedTurbineParam.Location = new System.Drawing.Point(647, 316);
            this.chkSelectedTurbineParam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkSelectedTurbineParam.Name = "chkSelectedTurbineParam";
            this.chkSelectedTurbineParam.Size = new System.Drawing.Size(100, 84);
            this.chkSelectedTurbineParam.TabIndex = 219;
            this.chkSelectedTurbineParam.SelectedIndexChanged += new System.EventHandler(this.chkSelectedTurbineParam_SelectedIndexChanged);
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label131.Location = new System.Drawing.Point(343, 86);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(61, 18);
            this.label131.TabIndex = 218;
            this.label131.Text = "Turbine :";
            // 
            // cboSelectedTurbine
            // 
            this.cboSelectedTurbine.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSelectedTurbine.FormattingEnabled = true;
            this.cboSelectedTurbine.Location = new System.Drawing.Point(410, 81);
            this.cboSelectedTurbine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSelectedTurbine.Name = "cboSelectedTurbine";
            this.cboSelectedTurbine.Size = new System.Drawing.Size(190, 26);
            this.cboSelectedTurbine.TabIndex = 217;
            this.cboSelectedTurbine.SelectedIndexChanged += new System.EventHandler(this.cboSelectedTurbine_SelectedIndexChanged);
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label126.Location = new System.Drawing.Point(779, 16);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(149, 19);
            this.label126.TabIndex = 216;
            this.label126.Text = "Monthly Summary";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label122.Location = new System.Drawing.Point(20, 90);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(137, 19);
            this.label122.TabIndex = 215;
            this.label122.Text = "Yearly Summary";
            // 
            // chkSelectAllTurbineYears
            // 
            this.chkSelectAllTurbineYears.AutoSize = true;
            this.chkSelectAllTurbineYears.Checked = true;
            this.chkSelectAllTurbineYears.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectAllTurbineYears.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectAllTurbineYears.Location = new System.Drawing.Point(1515, 47);
            this.chkSelectAllTurbineYears.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkSelectAllTurbineYears.Name = "chkSelectAllTurbineYears";
            this.chkSelectAllTurbineYears.Size = new System.Drawing.Size(81, 30);
            this.chkSelectAllTurbineYears.TabIndex = 214;
            this.chkSelectAllTurbineYears.Text = "Select/\r\nDeselect All";
            this.chkSelectAllTurbineYears.UseVisualStyleBackColor = true;
            this.chkSelectAllTurbineYears.CheckedChanged += new System.EventHandler(this.chkSelectAllTurbineYears_CheckedChanged);
            // 
            // chkYears_Monthly
            // 
            this.chkYears_Monthly.CheckOnClick = true;
            this.chkYears_Monthly.FormattingEnabled = true;
            this.chkYears_Monthly.Location = new System.Drawing.Point(1504, 94);
            this.chkYears_Monthly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkYears_Monthly.Name = "chkYears_Monthly";
            this.chkYears_Monthly.Size = new System.Drawing.Size(109, 284);
            this.chkYears_Monthly.TabIndex = 213;
            this.chkYears_Monthly.SelectedIndexChanged += new System.EventHandler(this.chkYears_Monthly_SelectedIndexChanged);
            // 
            // lstYearlyTurbine
            // 
            this.lstYearlyTurbine.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader118,
            this.columnHeader119,
            this.columnHeader131,
            this.columnHeader122,
            this.columnHeader127,
            this.columnHeader128});
            this.lstYearlyTurbine.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstYearlyTurbine.GridLines = true;
            this.lstYearlyTurbine.HideSelection = false;
            this.lstYearlyTurbine.Location = new System.Drawing.Point(15, 124);
            this.lstYearlyTurbine.Margin = new System.Windows.Forms.Padding(2);
            this.lstYearlyTurbine.Name = "lstYearlyTurbine";
            this.lstYearlyTurbine.Size = new System.Drawing.Size(611, 299);
            this.lstYearlyTurbine.TabIndex = 201;
            this.lstYearlyTurbine.UseCompatibleStateImageBehavior = false;
            this.lstYearlyTurbine.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader118
            // 
            this.columnHeader118.Text = "Year";
            this.columnHeader118.Width = 70;
            // 
            // columnHeader119
            // 
            this.columnHeader119.Text = "Avg WS (m/s)";
            this.columnHeader119.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader119.Width = 83;
            // 
            // columnHeader131
            // 
            this.columnHeader131.Text = "Gross AEP (MWh)";
            this.columnHeader131.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader131.Width = 100;
            // 
            // columnHeader122
            // 
            this.columnHeader122.Text = "Net AEP (MWh)";
            this.columnHeader122.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader122.Width = 85;
            // 
            // columnHeader127
            // 
            this.columnHeader127.Text = "Wake Loss (%)";
            this.columnHeader127.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader127.Width = 86;
            // 
            // columnHeader128
            // 
            this.columnHeader128.Text = "% Energy Diff. from LT";
            this.columnHeader128.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader128.Width = 118;
            // 
            // lstMonthlyTurbine
            // 
            this.lstMonthlyTurbine.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader123,
            this.columnHeader124,
            this.columnHeader125,
            this.columnHeader132,
            this.columnHeader126,
            this.columnHeader129,
            this.columnHeader130});
            this.lstMonthlyTurbine.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMonthlyTurbine.GridLines = true;
            this.lstMonthlyTurbine.HideSelection = false;
            this.lstMonthlyTurbine.Location = new System.Drawing.Point(765, 47);
            this.lstMonthlyTurbine.Margin = new System.Windows.Forms.Padding(2);
            this.lstMonthlyTurbine.Name = "lstMonthlyTurbine";
            this.lstMonthlyTurbine.Size = new System.Drawing.Size(719, 377);
            this.lstMonthlyTurbine.TabIndex = 200;
            this.lstMonthlyTurbine.UseCompatibleStateImageBehavior = false;
            this.lstMonthlyTurbine.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader123
            // 
            this.columnHeader123.Text = "Month";
            this.columnHeader123.Width = 44;
            // 
            // columnHeader124
            // 
            this.columnHeader124.Text = "Year";
            this.columnHeader124.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader124.Width = 48;
            // 
            // columnHeader125
            // 
            this.columnHeader125.Text = "Avg WS (m/s)";
            this.columnHeader125.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader125.Width = 110;
            // 
            // columnHeader132
            // 
            this.columnHeader132.Text = "Gross Energy (MWh)";
            this.columnHeader132.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader132.Width = 116;
            // 
            // columnHeader126
            // 
            this.columnHeader126.Text = "Net Energy (MWh)";
            this.columnHeader126.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader126.Width = 112;
            // 
            // columnHeader129
            // 
            this.columnHeader129.Text = "Wake Loss (%)";
            this.columnHeader129.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader129.Width = 89;
            // 
            // columnHeader130
            // 
            this.columnHeader130.Text = "% Energy Diff. from LT";
            this.columnHeader130.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader130.Width = 123;
            // 
            // label163
            // 
            this.label163.AutoSize = true;
            this.label163.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label163.Location = new System.Drawing.Point(20, 20);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(159, 50);
            this.label163.TabIndex = 1;
            this.label163.Text = "Turbine Time \r\nSeries Analysis";
            // 
            // pgeMaps
            // 
            this.pgeMaps.Controls.Add(this.plotGenMap);
            this.pgeMaps.Controls.Add(this.Label72);
            this.pgeMaps.Controls.Add(this.cboMapWD);
            this.pgeMaps.Controls.Add(this.txtMap_MetsUsed);
            this.pgeMaps.Controls.Add(this.Label63);
            this.pgeMaps.Controls.Add(this.Label4);
            this.pgeMaps.Controls.Add(this.Label27);
            this.pgeMaps.Controls.Add(this.chkAllTurbs_Maps);
            this.pgeMaps.Controls.Add(this.chkAllMets_Maps);
            this.pgeMaps.Controls.Add(this.Label28);
            this.pgeMaps.Controls.Add(this.Label29);
            this.pgeMaps.Controls.Add(this.chkTurbLabels_Maps);
            this.pgeMaps.Controls.Add(this.chkMetLabels_Maps);
            this.pgeMaps.Controls.Add(this.btnRefreshMap);
            this.pgeMaps.Controls.Add(this.chkAutoMinMax);
            this.pgeMaps.Controls.Add(this.txtIntLevel);
            this.pgeMaps.Controls.Add(this.txtMaxValue);
            this.pgeMaps.Controls.Add(this.txtMinValue);
            this.pgeMaps.Controls.Add(this.txtMapMax);
            this.pgeMaps.Controls.Add(this.txtMapMin);
            this.pgeMaps.Controls.Add(this.txtMapCount);
            this.pgeMaps.Controls.Add(this.txtMapStDev);
            this.pgeMaps.Controls.Add(this.txtMapAvg);
            this.pgeMaps.Controls.Add(this.lblInterval);
            this.pgeMaps.Controls.Add(this.lblMaxVal);
            this.pgeMaps.Controls.Add(this.lblMinVal);
            this.pgeMaps.Controls.Add(this.Label10);
            this.pgeMaps.Controls.Add(this.Label15);
            this.pgeMaps.Controls.Add(this.Label16);
            this.pgeMaps.Controls.Add(this.Label17);
            this.pgeMaps.Controls.Add(this.Label18);
            this.pgeMaps.Controls.Add(this.Label3);
            this.pgeMaps.Controls.Add(this.btnDelMaps);
            this.pgeMaps.Controls.Add(this.btnMapExportCSV);
            this.pgeMaps.Controls.Add(this.btnExportWRG);
            this.pgeMaps.Controls.Add(this.btnGenMap);
            this.pgeMaps.Controls.Add(this.lstMaps);
            this.pgeMaps.Location = new System.Drawing.Point(4, 27);
            this.pgeMaps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeMaps.Name = "pgeMaps";
            this.pgeMaps.Size = new System.Drawing.Size(1627, 849);
            this.pgeMaps.TabIndex = 8;
            this.pgeMaps.Text = "Maps";
            this.pgeMaps.UseVisualStyleBackColor = true;
            // 
            // plotGenMap
            // 
            this.plotGenMap.Location = new System.Drawing.Point(646, 32);
            this.plotGenMap.Name = "plotGenMap";
            this.plotGenMap.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGenMap.Size = new System.Drawing.Size(978, 800);
            this.plotGenMap.TabIndex = 280;
            this.plotGenMap.Text = "plotView2";
            this.plotGenMap.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotGenMap.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotGenMap.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // Label72
            // 
            this.Label72.AutoSize = true;
            this.Label72.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label72.Location = new System.Drawing.Point(512, 14);
            this.Label72.Name = "Label72";
            this.Label72.Size = new System.Drawing.Size(103, 18);
            this.Label72.TabIndex = 194;
            this.Label72.Text = "Wind Direction:";
            this.Label72.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboMapWD
            // 
            this.cboMapWD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMapWD.FormattingEnabled = true;
            this.cboMapWD.Location = new System.Drawing.Point(503, 38);
            this.cboMapWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMapWD.Name = "cboMapWD";
            this.cboMapWD.Size = new System.Drawing.Size(115, 24);
            this.cboMapWD.TabIndex = 193;
            this.cboMapWD.SelectedIndexChanged += new System.EventHandler(this.cboMapWD_SelectedIndexChanged);
            // 
            // txtMap_MetsUsed
            // 
            this.txtMap_MetsUsed.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMap_MetsUsed.Location = new System.Drawing.Point(120, 434);
            this.txtMap_MetsUsed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMap_MetsUsed.Name = "txtMap_MetsUsed";
            this.txtMap_MetsUsed.ReadOnly = true;
            this.txtMap_MetsUsed.Size = new System.Drawing.Size(497, 25);
            this.txtMap_MetsUsed.TabIndex = 94;
            // 
            // Label63
            // 
            this.Label63.AutoSize = true;
            this.Label63.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label63.Location = new System.Drawing.Point(24, 439);
            this.Label63.Name = "Label63";
            this.Label63.Size = new System.Drawing.Size(80, 18);
            this.Label63.TabIndex = 93;
            this.Label63.Text = "Mets Used =";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(22, 21);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(275, 25);
            this.Label4.TabIndex = 89;
            this.Label4.Text = "Maps && WRG File Creation";
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label27.Location = new System.Drawing.Point(473, 515);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(127, 19);
            this.Label27.TabIndex = 85;
            this.Label27.Text = "Display Options";
            // 
            // chkAllTurbs_Maps
            // 
            this.chkAllTurbs_Maps.AutoSize = true;
            this.chkAllTurbs_Maps.Checked = true;
            this.chkAllTurbs_Maps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllTurbs_Maps.Location = new System.Drawing.Point(313, 655);
            this.chkAllTurbs_Maps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAllTurbs_Maps.Name = "chkAllTurbs_Maps";
            this.chkAllTurbs_Maps.Size = new System.Drawing.Size(139, 23);
            this.chkAllTurbs_Maps.TabIndex = 82;
            this.chkAllTurbs_Maps.Text = "Select/Deselect All";
            this.chkAllTurbs_Maps.UseVisualStyleBackColor = true;
            this.chkAllTurbs_Maps.CheckedChanged += new System.EventHandler(this.chkAllTurbs_Maps_CheckedChanged);
            // 
            // chkAllMets_Maps
            // 
            this.chkAllMets_Maps.AutoSize = true;
            this.chkAllMets_Maps.Checked = true;
            this.chkAllMets_Maps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllMets_Maps.Location = new System.Drawing.Point(311, 486);
            this.chkAllMets_Maps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAllMets_Maps.Name = "chkAllMets_Maps";
            this.chkAllMets_Maps.Size = new System.Drawing.Size(139, 23);
            this.chkAllMets_Maps.TabIndex = 81;
            this.chkAllMets_Maps.Text = "Select/Deselect All";
            this.chkAllMets_Maps.UseVisualStyleBackColor = true;
            this.chkAllMets_Maps.CheckedChanged += new System.EventHandler(this.chkAllMets_Maps_CheckedChanged);
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label28.Location = new System.Drawing.Point(217, 654);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(72, 19);
            this.Label28.TabIndex = 80;
            this.Label28.Text = "Turbines";
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label29.Location = new System.Drawing.Point(217, 484);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(73, 19);
            this.Label29.TabIndex = 79;
            this.Label29.Text = "Met Sites";
            // 
            // chkTurbLabels_Maps
            // 
            this.chkTurbLabels_Maps.CheckOnClick = true;
            this.chkTurbLabels_Maps.FormattingEnabled = true;
            this.chkTurbLabels_Maps.Location = new System.Drawing.Point(221, 684);
            this.chkTurbLabels_Maps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbLabels_Maps.Name = "chkTurbLabels_Maps";
            this.chkTurbLabels_Maps.Size = new System.Drawing.Size(200, 104);
            this.chkTurbLabels_Maps.TabIndex = 78;
            this.chkTurbLabels_Maps.SelectedIndexChanged += new System.EventHandler(this.chkTurbLabels_Maps_SelectedIndexChanged);
            // 
            // chkMetLabels_Maps
            // 
            this.chkMetLabels_Maps.CheckOnClick = true;
            this.chkMetLabels_Maps.FormattingEnabled = true;
            this.chkMetLabels_Maps.Location = new System.Drawing.Point(221, 514);
            this.chkMetLabels_Maps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetLabels_Maps.Name = "chkMetLabels_Maps";
            this.chkMetLabels_Maps.Size = new System.Drawing.Size(200, 104);
            this.chkMetLabels_Maps.TabIndex = 77;
            this.chkMetLabels_Maps.SelectedIndexChanged += new System.EventHandler(this.chkMetLabels_Maps_SelectedIndexChanged);
            // 
            // btnRefreshMap
            // 
            this.btnRefreshMap.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshMap.Location = new System.Drawing.Point(486, 684);
            this.btnRefreshMap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefreshMap.Name = "btnRefreshMap";
            this.btnRefreshMap.Size = new System.Drawing.Size(97, 70);
            this.btnRefreshMap.TabIndex = 54;
            this.btnRefreshMap.Text = "Refresh Map";
            this.btnRefreshMap.UseVisualStyleBackColor = true;
            this.btnRefreshMap.Click += new System.EventHandler(this.btnRefreshMap_Click);
            // 
            // chkAutoMinMax
            // 
            this.chkAutoMinMax.AutoSize = true;
            this.chkAutoMinMax.Checked = true;
            this.chkAutoMinMax.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoMinMax.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoMinMax.Location = new System.Drawing.Point(451, 637);
            this.chkAutoMinMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAutoMinMax.Name = "chkAutoMinMax";
            this.chkAutoMinMax.Size = new System.Drawing.Size(163, 22);
            this.chkAutoMinMax.TabIndex = 53;
            this.chkAutoMinMax.Text = "Use Auto Min and Max";
            this.chkAutoMinMax.UseVisualStyleBackColor = true;
            // 
            // txtIntLevel
            // 
            this.txtIntLevel.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntLevel.Location = new System.Drawing.Point(516, 601);
            this.txtIntLevel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtIntLevel.Name = "txtIntLevel";
            this.txtIntLevel.Size = new System.Drawing.Size(61, 25);
            this.txtIntLevel.TabIndex = 52;
            // 
            // txtMaxValue
            // 
            this.txtMaxValue.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxValue.Location = new System.Drawing.Point(516, 571);
            this.txtMaxValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaxValue.Name = "txtMaxValue";
            this.txtMaxValue.Size = new System.Drawing.Size(61, 25);
            this.txtMaxValue.TabIndex = 50;
            // 
            // txtMinValue
            // 
            this.txtMinValue.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinValue.Location = new System.Drawing.Point(516, 542);
            this.txtMinValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMinValue.Name = "txtMinValue";
            this.txtMinValue.Size = new System.Drawing.Size(61, 25);
            this.txtMinValue.TabIndex = 48;
            // 
            // txtMapMax
            // 
            this.txtMapMax.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapMax.Location = new System.Drawing.Point(94, 653);
            this.txtMapMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMapMax.Name = "txtMapMax";
            this.txtMapMax.ReadOnly = true;
            this.txtMapMax.Size = new System.Drawing.Size(80, 25);
            this.txtMapMax.TabIndex = 41;
            // 
            // txtMapMin
            // 
            this.txtMapMin.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapMin.Location = new System.Drawing.Point(94, 609);
            this.txtMapMin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMapMin.Name = "txtMapMin";
            this.txtMapMin.ReadOnly = true;
            this.txtMapMin.Size = new System.Drawing.Size(80, 25);
            this.txtMapMin.TabIndex = 40;
            // 
            // txtMapCount
            // 
            this.txtMapCount.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapCount.Location = new System.Drawing.Point(94, 697);
            this.txtMapCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMapCount.Name = "txtMapCount";
            this.txtMapCount.ReadOnly = true;
            this.txtMapCount.Size = new System.Drawing.Size(80, 25);
            this.txtMapCount.TabIndex = 37;
            // 
            // txtMapStDev
            // 
            this.txtMapStDev.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapStDev.Location = new System.Drawing.Point(94, 565);
            this.txtMapStDev.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMapStDev.Name = "txtMapStDev";
            this.txtMapStDev.ReadOnly = true;
            this.txtMapStDev.Size = new System.Drawing.Size(80, 25);
            this.txtMapStDev.TabIndex = 36;
            // 
            // txtMapAvg
            // 
            this.txtMapAvg.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapAvg.Location = new System.Drawing.Point(94, 521);
            this.txtMapAvg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMapAvg.Name = "txtMapAvg";
            this.txtMapAvg.ReadOnly = true;
            this.txtMapAvg.Size = new System.Drawing.Size(80, 25);
            this.txtMapAvg.TabIndex = 35;
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterval.Location = new System.Drawing.Point(455, 606);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(59, 18);
            this.lblInterval.TabIndex = 51;
            this.lblInterval.Text = "Interval:";
            // 
            // lblMaxVal
            // 
            this.lblMaxVal.AutoSize = true;
            this.lblMaxVal.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxVal.Location = new System.Drawing.Point(474, 574);
            this.lblMaxVal.Name = "lblMaxVal";
            this.lblMaxVal.Size = new System.Drawing.Size(37, 18);
            this.lblMaxVal.TabIndex = 49;
            this.lblMaxVal.Text = "Max:";
            // 
            // lblMinVal
            // 
            this.lblMinVal.AutoSize = true;
            this.lblMinVal.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinVal.Location = new System.Drawing.Point(476, 544);
            this.lblMinVal.Name = "lblMinVal";
            this.lblMinVal.Size = new System.Drawing.Size(35, 18);
            this.lblMinVal.TabIndex = 47;
            this.lblMinVal.Text = "Min:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.Location = new System.Drawing.Point(19, 656);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(76, 18);
            this.Label10.TabIndex = 39;
            this.Label10.Text = "Maximum :";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(24, 612);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(74, 18);
            this.Label15.TabIndex = 38;
            this.Label15.Text = "Minimum :";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(40, 699);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(51, 18);
            this.Label16.TabIndex = 34;
            this.Label16.Text = "Count :";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(33, 569);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(58, 18);
            this.Label17.TabIndex = 33;
            this.Label17.Text = "St. Dev. :";
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(28, 523);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(63, 18);
            this.Label18.TabIndex = 32;
            this.Label18.Text = "Average :";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(19, 486);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(137, 23);
            this.Label3.TabIndex = 27;
            this.Label3.Text = "Map Statistics";
            // 
            // btnDelMaps
            // 
            this.btnDelMaps.Location = new System.Drawing.Point(139, 78);
            this.btnDelMaps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelMaps.Name = "btnDelMaps";
            this.btnDelMaps.Size = new System.Drawing.Size(101, 50);
            this.btnDelMaps.TabIndex = 20;
            this.btnDelMaps.Text = "Delete Maps";
            this.btnDelMaps.UseVisualStyleBackColor = true;
            this.btnDelMaps.Click += new System.EventHandler(this.btnDelMaps_Click);
            // 
            // btnMapExportCSV
            // 
            this.btnMapExportCSV.Location = new System.Drawing.Point(248, 78);
            this.btnMapExportCSV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMapExportCSV.Name = "btnMapExportCSV";
            this.btnMapExportCSV.Size = new System.Drawing.Size(125, 50);
            this.btnMapExportCSV.TabIndex = 19;
            this.btnMapExportCSV.Text = "Export Map as CSV";
            this.btnMapExportCSV.UseVisualStyleBackColor = true;
            this.btnMapExportCSV.Click += new System.EventHandler(this.btnMapExportCSV_Click);
            // 
            // btnExportWRG
            // 
            this.btnExportWRG.Location = new System.Drawing.Point(385, 78);
            this.btnExportWRG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportWRG.Name = "btnExportWRG";
            this.btnExportWRG.Size = new System.Drawing.Size(125, 50);
            this.btnExportWRG.TabIndex = 18;
            this.btnExportWRG.Text = "Export WS Map as WRG file";
            this.btnExportWRG.UseVisualStyleBackColor = true;
            this.btnExportWRG.Click += new System.EventHandler(this.btnExportWRG_Click);
            // 
            // btnGenMap
            // 
            this.btnGenMap.Location = new System.Drawing.Point(17, 78);
            this.btnGenMap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGenMap.Name = "btnGenMap";
            this.btnGenMap.Size = new System.Drawing.Size(114, 50);
            this.btnGenMap.TabIndex = 17;
            this.btnGenMap.Text = "Generate Map";
            this.btnGenMap.UseVisualStyleBackColor = true;
            this.btnGenMap.Click += new System.EventHandler(this.btnGenMap_Click);
            // 
            // lstMaps
            // 
            this.lstMaps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader54,
            this.ColumnHeader55,
            this.ColumnHeader56,
            this.ColumnHeader57,
            this.ColumnHeader58,
            this.ColumnHeader59,
            this.ColumnHeader36,
            this.ColumnHeader12,
            this.columnHeader185});
            this.lstMaps.FullRowSelect = true;
            this.lstMaps.HideSelection = false;
            this.lstMaps.Location = new System.Drawing.Point(17, 158);
            this.lstMaps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstMaps.Name = "lstMaps";
            this.lstMaps.Size = new System.Drawing.Size(600, 265);
            this.lstMaps.TabIndex = 15;
            this.lstMaps.UseCompatibleStateImageBehavior = false;
            this.lstMaps.View = System.Windows.Forms.View.Details;
            this.lstMaps.SelectedIndexChanged += new System.EventHandler(this.lstMaps_SelectedIndexChanged);
            // 
            // ColumnHeader54
            // 
            this.ColumnHeader54.Text = "Map Name";
            this.ColumnHeader54.Width = 139;
            // 
            // ColumnHeader55
            // 
            this.ColumnHeader55.Text = "Min UTMX";
            this.ColumnHeader55.Width = 70;
            // 
            // ColumnHeader56
            // 
            this.ColumnHeader56.Text = "Min UTMY";
            this.ColumnHeader56.Width = 71;
            // 
            // ColumnHeader57
            // 
            this.ColumnHeader57.Text = "Max UTMX";
            this.ColumnHeader57.Width = 75;
            // 
            // ColumnHeader58
            // 
            this.ColumnHeader58.Text = "Max UTMY";
            this.ColumnHeader58.Width = 74;
            // 
            // ColumnHeader59
            // 
            this.ColumnHeader59.Text = "Resolution";
            this.ColumnHeader59.Width = 65;
            // 
            // ColumnHeader36
            // 
            this.ColumnHeader36.Text = "Used SR/DH";
            this.ColumnHeader36.Width = 76;
            // 
            // ColumnHeader12
            // 
            this.ColumnHeader12.Text = "Used FS";
            // 
            // columnHeader185
            // 
            this.columnHeader185.Text = "uses TS";
            // 
            // pgeRound
            // 
            this.pgeRound.Controls.Add(this.plotTurbUncert);
            this.pgeRound.Controls.Add(this.plotRR_Histo);
            this.pgeRound.Controls.Add(this.plotRRErrorByNumMets);
            this.pgeRound.Controls.Add(this.txtisMCPdUncert);
            this.pgeRound.Controls.Add(this.txtRR_FlowSep_Used);
            this.pgeRound.Controls.Add(this.cboRR_MinSize);
            this.pgeRound.Controls.Add(this.Label31);
            this.pgeRound.Controls.Add(this.txtRR_LC_used);
            this.pgeRound.Controls.Add(this.Label81);
            this.pgeRound.Controls.Add(this.btnExportTurbUncert);
            this.pgeRound.Controls.Add(this.chkP50);
            this.pgeRound.Controls.Add(this.chkP90);
            this.pgeRound.Controls.Add(this.chkP99);
            this.pgeRound.Controls.Add(this.cboUncert_WS_AEP);
            this.pgeRound.Controls.Add(this.Label47);
            this.pgeRound.Controls.Add(this.Label45);
            this.pgeRound.Controls.Add(this.cboUncertPowerCrv);
            this.pgeRound.Controls.Add(this.Label42);
            this.pgeRound.Controls.Add(this.lstTurbUncert);
            this.pgeRound.Controls.Add(this.Label41);
            this.pgeRound.Controls.Add(this.btnExportRR);
            this.pgeRound.Controls.Add(this.lstRR_AllErr);
            this.pgeRound.Controls.Add(this.cboRoundRobin);
            this.pgeRound.Controls.Add(this.lstRR_Results);
            this.pgeRound.Controls.Add(this.btnDoRRCalcs);
            this.pgeRound.Controls.Add(this.Label46);
            this.pgeRound.Location = new System.Drawing.Point(4, 27);
            this.pgeRound.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeRound.Name = "pgeRound";
            this.pgeRound.Size = new System.Drawing.Size(1627, 849);
            this.pgeRound.TabIndex = 11;
            this.pgeRound.Text = "Uncertainty Analysis";
            this.pgeRound.UseVisualStyleBackColor = true;
            // 
            // plotTurbUncert
            // 
            this.plotTurbUncert.Location = new System.Drawing.Point(964, 517);
            this.plotTurbUncert.Name = "plotTurbUncert";
            this.plotTurbUncert.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTurbUncert.Size = new System.Drawing.Size(626, 302);
            this.plotTurbUncert.TabIndex = 282;
            this.plotTurbUncert.Text = "plotView1";
            this.plotTurbUncert.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTurbUncert.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTurbUncert.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotRR_Histo
            // 
            this.plotRR_Histo.Location = new System.Drawing.Point(15, 506);
            this.plotRR_Histo.Name = "plotRR_Histo";
            this.plotRR_Histo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotRR_Histo.Size = new System.Drawing.Size(427, 313);
            this.plotRR_Histo.TabIndex = 281;
            this.plotRR_Histo.Text = "plotView1";
            this.plotRR_Histo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotRR_Histo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotRR_Histo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotRRErrorByNumMets
            // 
            this.plotRRErrorByNumMets.Location = new System.Drawing.Point(15, 95);
            this.plotRRErrorByNumMets.Name = "plotRRErrorByNumMets";
            this.plotRRErrorByNumMets.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotRRErrorByNumMets.Size = new System.Drawing.Size(427, 240);
            this.plotRRErrorByNumMets.TabIndex = 280;
            this.plotRRErrorByNumMets.Text = "plotView1";
            this.plotRRErrorByNumMets.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotRRErrorByNumMets.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotRRErrorByNumMets.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // txtisMCPdUncert
            // 
            this.txtisMCPdUncert.Location = new System.Drawing.Point(464, 82);
            this.txtisMCPdUncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtisMCPdUncert.Name = "txtisMCPdUncert";
            this.txtisMCPdUncert.ReadOnly = true;
            this.txtisMCPdUncert.Size = new System.Drawing.Size(180, 25);
            this.txtisMCPdUncert.TabIndex = 200;
            // 
            // txtRR_FlowSep_Used
            // 
            this.txtRR_FlowSep_Used.Location = new System.Drawing.Point(827, 82);
            this.txtRR_FlowSep_Used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRR_FlowSep_Used.Name = "txtRR_FlowSep_Used";
            this.txtRR_FlowSep_Used.ReadOnly = true;
            this.txtRR_FlowSep_Used.Size = new System.Drawing.Size(172, 25);
            this.txtRR_FlowSep_Used.TabIndex = 175;
            // 
            // cboRR_MinSize
            // 
            this.cboRR_MinSize.FormattingEnabled = true;
            this.cboRR_MinSize.Location = new System.Drawing.Point(159, 406);
            this.cboRR_MinSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboRR_MinSize.Name = "cboRR_MinSize";
            this.cboRR_MinSize.Size = new System.Drawing.Size(70, 26);
            this.cboRR_MinSize.TabIndex = 174;
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.Location = new System.Drawing.Point(154, 364);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(80, 38);
            this.Label31.TabIndex = 173;
            this.Label31.Text = "Min. Num.\r\nof Mets";
            this.Label31.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtRR_LC_used
            // 
            this.txtRR_LC_used.Location = new System.Drawing.Point(652, 82);
            this.txtRR_LC_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRR_LC_used.Name = "txtRR_LC_used";
            this.txtRR_LC_used.ReadOnly = true;
            this.txtRR_LC_used.Size = new System.Drawing.Size(168, 25);
            this.txtRR_LC_used.TabIndex = 171;
            // 
            // Label81
            // 
            this.Label81.AutoSize = true;
            this.Label81.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label81.Location = new System.Drawing.Point(22, 58);
            this.Label81.Name = "Label81";
            this.Label81.Size = new System.Drawing.Size(362, 23);
            this.Label81.TabIndex = 170;
            this.Label81.Text = "Round Robin (Leave One Out) Analysis";
            // 
            // btnExportTurbUncert
            // 
            this.btnExportTurbUncert.Location = new System.Drawing.Point(1434, 20);
            this.btnExportTurbUncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportTurbUncert.Name = "btnExportTurbUncert";
            this.btnExportTurbUncert.Size = new System.Drawing.Size(149, 54);
            this.btnExportTurbUncert.TabIndex = 167;
            this.btnExportTurbUncert.Text = "Export Turbine Estimates";
            this.btnExportTurbUncert.UseVisualStyleBackColor = true;
            this.btnExportTurbUncert.Click += new System.EventHandler(this.btnExportTurbUncert_Click);
            // 
            // chkP50
            // 
            this.chkP50.AutoSize = true;
            this.chkP50.Checked = true;
            this.chkP50.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkP50.Location = new System.Drawing.Point(1279, 87);
            this.chkP50.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkP50.Name = "chkP50";
            this.chkP50.Size = new System.Drawing.Size(51, 23);
            this.chkP50.TabIndex = 166;
            this.chkP50.Text = "P50";
            this.chkP50.UseVisualStyleBackColor = true;
            this.chkP50.CheckedChanged += new System.EventHandler(this.chkP50_CheckedChanged);
            // 
            // chkP90
            // 
            this.chkP90.AutoSize = true;
            this.chkP90.Checked = true;
            this.chkP90.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkP90.Location = new System.Drawing.Point(1338, 87);
            this.chkP90.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkP90.Name = "chkP90";
            this.chkP90.Size = new System.Drawing.Size(51, 23);
            this.chkP90.TabIndex = 165;
            this.chkP90.Text = "P90";
            this.chkP90.UseVisualStyleBackColor = true;
            this.chkP90.CheckedChanged += new System.EventHandler(this.chkP90_CheckedChanged);
            // 
            // chkP99
            // 
            this.chkP99.AutoSize = true;
            this.chkP99.Checked = true;
            this.chkP99.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkP99.Location = new System.Drawing.Point(1394, 87);
            this.chkP99.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkP99.Name = "chkP99";
            this.chkP99.Size = new System.Drawing.Size(51, 23);
            this.chkP99.TabIndex = 164;
            this.chkP99.Text = "P99";
            this.chkP99.UseVisualStyleBackColor = true;
            this.chkP99.CheckedChanged += new System.EventHandler(this.chkP99_CheckedChanged);
            // 
            // cboUncert_WS_AEP
            // 
            this.cboUncert_WS_AEP.FormattingEnabled = true;
            this.cboUncert_WS_AEP.Items.AddRange(new object[] {
            "Wind Speed",
            "Gross AEP"});
            this.cboUncert_WS_AEP.Location = new System.Drawing.Point(1097, 82);
            this.cboUncert_WS_AEP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboUncert_WS_AEP.Name = "cboUncert_WS_AEP";
            this.cboUncert_WS_AEP.Size = new System.Drawing.Size(149, 26);
            this.cboUncert_WS_AEP.TabIndex = 163;
            this.cboUncert_WS_AEP.SelectedIndexChanged += new System.EventHandler(this.cboUncert_WS_AEP_SelectedIndexChanged);
            // 
            // Label47
            // 
            this.Label47.AutoSize = true;
            this.Label47.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label47.Location = new System.Drawing.Point(1053, 87);
            this.Label47.Name = "Label47";
            this.Label47.Size = new System.Drawing.Size(38, 18);
            this.Label47.TabIndex = 162;
            this.Label47.Text = "Plot :";
            // 
            // Label45
            // 
            this.Label45.AutoSize = true;
            this.Label45.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label45.Location = new System.Drawing.Point(990, 50);
            this.Label45.Name = "Label45";
            this.Label45.Size = new System.Drawing.Size(93, 18);
            this.Label45.TabIndex = 160;
            this.Label45.Text = "Power Curve :";
            // 
            // cboUncertPowerCrv
            // 
            this.cboUncertPowerCrv.FormattingEnabled = true;
            this.cboUncertPowerCrv.Location = new System.Drawing.Point(1097, 48);
            this.cboUncertPowerCrv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboUncertPowerCrv.Name = "cboUncertPowerCrv";
            this.cboUncertPowerCrv.Size = new System.Drawing.Size(294, 26);
            this.cboUncertPowerCrv.TabIndex = 159;
            this.cboUncertPowerCrv.SelectedIndexChanged += new System.EventHandler(this.cboUncertPowerCrv_SelectedIndexChanged);
            // 
            // Label42
            // 
            this.Label42.AutoSize = true;
            this.Label42.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label42.Location = new System.Drawing.Point(955, 14);
            this.Label42.Name = "Label42";
            this.Label42.Size = new System.Drawing.Size(398, 23);
            this.Label42.TabIndex = 158;
            this.Label42.Text = "Turbine WS and Gross Energy Uncertainties";
            // 
            // lstTurbUncert
            // 
            this.lstTurbUncert.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader28,
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3,
            this.ColumnHeader11,
            this.ColumnHeader25,
            this.ColumnHeader7,
            this.ColumnHeader10,
            this.ColumnHeader27});
            this.lstTurbUncert.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTurbUncert.FullRowSelect = true;
            this.lstTurbUncert.HideSelection = false;
            this.lstTurbUncert.Location = new System.Drawing.Point(960, 117);
            this.lstTurbUncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstTurbUncert.Name = "lstTurbUncert";
            this.lstTurbUncert.Size = new System.Drawing.Size(630, 393);
            this.lstTurbUncert.TabIndex = 157;
            this.lstTurbUncert.UseCompatibleStateImageBehavior = false;
            this.lstTurbUncert.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader28
            // 
            this.ColumnHeader28.Text = "#";
            this.ColumnHeader28.Width = 30;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Site";
            this.ColumnHeader1.Width = 41;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "WS, m/s";
            this.ColumnHeader2.Width = 58;
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "WS Err.";
            this.ColumnHeader3.Width = 56;
            // 
            // ColumnHeader11
            // 
            this.ColumnHeader11.Text = "WS P90";
            this.ColumnHeader11.Width = 59;
            // 
            // ColumnHeader25
            // 
            this.ColumnHeader25.Text = "WS P99";
            // 
            // ColumnHeader7
            // 
            this.ColumnHeader7.Text = "AEP";
            this.ColumnHeader7.Width = 63;
            // 
            // ColumnHeader10
            // 
            this.ColumnHeader10.Text = "AEP P90";
            this.ColumnHeader10.Width = 67;
            // 
            // ColumnHeader27
            // 
            this.ColumnHeader27.Text = "AEP P99";
            this.ColumnHeader27.Width = 71;
            // 
            // Label41
            // 
            this.Label41.AutoSize = true;
            this.Label41.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label41.Location = new System.Drawing.Point(458, 18);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(160, 18);
            this.Label41.TabIndex = 156;
            this.Label41.Text = "Number of Mets In Model";
            // 
            // btnExportRR
            // 
            this.btnExportRR.Location = new System.Drawing.Point(15, 416);
            this.btnExportRR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportRR.Name = "btnExportRR";
            this.btnExportRR.Size = new System.Drawing.Size(127, 65);
            this.btnExportRR.TabIndex = 155;
            this.btnExportRR.Text = "Export All Round Robin Estimates";
            this.btnExportRR.UseVisualStyleBackColor = true;
            this.btnExportRR.Click += new System.EventHandler(this.btnExportRR_Click);
            // 
            // lstRR_AllErr
            // 
            this.lstRR_AllErr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader4,
            this.ColumnHeader5,
            this.ColumnHeader8,
            this.ColumnHeader6,
            this.ColumnHeader9});
            this.lstRR_AllErr.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRR_AllErr.FullRowSelect = true;
            this.lstRR_AllErr.HideSelection = false;
            this.lstRR_AllErr.Location = new System.Drawing.Point(461, 117);
            this.lstRR_AllErr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstRR_AllErr.Name = "lstRR_AllErr";
            this.lstRR_AllErr.Size = new System.Drawing.Size(490, 702);
            this.lstRR_AllErr.TabIndex = 154;
            this.lstRR_AllErr.UseCompatibleStateImageBehavior = false;
            this.lstRR_AllErr.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader4
            // 
            this.ColumnHeader4.Text = "#";
            this.ColumnHeader4.Width = 134;
            // 
            // ColumnHeader5
            // 
            this.ColumnHeader5.Text = "Mets Used";
            this.ColumnHeader5.Width = 78;
            // 
            // ColumnHeader8
            // 
            this.ColumnHeader8.Text = "Omitted Met";
            this.ColumnHeader8.Width = 83;
            // 
            // ColumnHeader6
            // 
            this.ColumnHeader6.Text = "Error, %";
            this.ColumnHeader6.Width = 58;
            // 
            // ColumnHeader9
            // 
            this.ColumnHeader9.Text = "RMSE";
            this.ColumnHeader9.Width = 74;
            // 
            // cboRoundRobin
            // 
            this.cboRoundRobin.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRoundRobin.FormattingEnabled = true;
            this.cboRoundRobin.ItemHeight = 18;
            this.cboRoundRobin.Location = new System.Drawing.Point(461, 44);
            this.cboRoundRobin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboRoundRobin.Name = "cboRoundRobin";
            this.cboRoundRobin.Size = new System.Drawing.Size(346, 26);
            this.cboRoundRobin.TabIndex = 151;
            this.cboRoundRobin.SelectedIndexChanged += new System.EventHandler(this.cboRoundRobin_SelectedIndexChanged);
            // 
            // lstRR_Results
            // 
            this.lstRR_Results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader38,
            this.ColumnHeader39,
            this.ColumnHeader45});
            this.lstRR_Results.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRR_Results.FullRowSelect = true;
            this.lstRR_Results.HideSelection = false;
            this.lstRR_Results.Location = new System.Drawing.Point(244, 353);
            this.lstRR_Results.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstRR_Results.Name = "lstRR_Results";
            this.lstRR_Results.Size = new System.Drawing.Size(209, 127);
            this.lstRR_Results.TabIndex = 149;
            this.lstRR_Results.UseCompatibleStateImageBehavior = false;
            this.lstRR_Results.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader38
            // 
            this.ColumnHeader38.Text = "# Mets";
            this.ColumnHeader38.Width = 84;
            // 
            // ColumnHeader39
            // 
            this.ColumnHeader39.Text = "RMS Error";
            this.ColumnHeader39.Width = 73;
            // 
            // ColumnHeader45
            // 
            this.ColumnHeader45.Text = "# Models";
            this.ColumnHeader45.Width = 68;
            // 
            // btnDoRRCalcs
            // 
            this.btnDoRRCalcs.BackColor = System.Drawing.Color.LightCoral;
            this.btnDoRRCalcs.Location = new System.Drawing.Point(15, 361);
            this.btnDoRRCalcs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDoRRCalcs.Name = "btnDoRRCalcs";
            this.btnDoRRCalcs.Size = new System.Drawing.Size(127, 48);
            this.btnDoRRCalcs.TabIndex = 86;
            this.btnDoRRCalcs.Text = "Do Round Robin Analysis";
            this.btnDoRRCalcs.UseVisualStyleBackColor = false;
            this.btnDoRRCalcs.Click += new System.EventHandler(this.btnDoRRCalcs_Click);
            // 
            // Label46
            // 
            this.Label46.AutoSize = true;
            this.Label46.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label46.Location = new System.Drawing.Point(18, 16);
            this.Label46.Name = "Label46";
            this.Label46.Size = new System.Drawing.Size(217, 25);
            this.Label46.TabIndex = 85;
            this.Label46.Text = "Uncertainty Analysis";
            // 
            // pgeStepwise
            // 
            this.pgeStepwise.Controls.Add(this.plotDHModel);
            this.pgeStepwise.Controls.Add(this.plotUHModel);
            this.pgeStepwise.Controls.Add(this.plotPathAlongNodes);
            this.pgeStepwise.Controls.Add(this.plotAdvTopo);
            this.pgeStepwise.Controls.Add(this.lblTurbineTSNoAdvanced);
            this.pgeStepwise.Controls.Add(this.txtisMCPdAdv);
            this.pgeStepwise.Controls.Add(this.cboSeasonAdvanced);
            this.pgeStepwise.Controls.Add(this.cboTODAdvanced);
            this.pgeStepwise.Controls.Add(this.Label39);
            this.pgeStepwise.Controls.Add(this.lstPathNodes_DW);
            this.pgeStepwise.Controls.Add(this.Label8);
            this.pgeStepwise.Controls.Add(this.lstPathNodes_UW);
            this.pgeStepwise.Controls.Add(this.txtAdv_FlowSep_Used);
            this.pgeStepwise.Controls.Add(this.Label101);
            this.pgeStepwise.Controls.Add(this.txtSepCritWS);
            this.pgeStepwise.Controls.Add(this.Label13);
            this.pgeStepwise.Controls.Add(this.txtSepCrit);
            this.pgeStepwise.Controls.Add(this.Label100);
            this.pgeStepwise.Controls.Add(this.cboDHplot);
            this.pgeStepwise.Controls.Add(this.btnImportModel);
            this.pgeStepwise.Controls.Add(this.chkAdvToShow);
            this.pgeStepwise.Controls.Add(this.chkWeight_RMS);
            this.pgeStepwise.Controls.Add(this.txtSectRMS);
            this.pgeStepwise.Controls.Add(this.Label48);
            this.pgeStepwise.Controls.Add(this.txtAdv_LC_used);
            this.pgeStepwise.Controls.Add(this.cboExpo_or_Stab);
            this.pgeStepwise.Controls.Add(this.Label82);
            this.pgeStepwise.Controls.Add(this.btnExportModel);
            this.pgeStepwise.Controls.Add(this.Label59);
            this.pgeStepwise.Controls.Add(this.txtUWCrit);
            this.pgeStepwise.Controls.Add(this.Label58);
            this.pgeStepwise.Controls.Add(this.cboUphill_to_show);
            this.pgeStepwise.Controls.Add(this.cboAdvancedWD);
            this.pgeStepwise.Controls.Add(this.Label56);
            this.pgeStepwise.Controls.Add(this.btnExportCrossPreds);
            this.pgeStepwise.Controls.Add(this.Label14);
            this.pgeStepwise.Controls.Add(this.cboAdvancedRad);
            this.pgeStepwise.Controls.Add(this.txtUWDWRMS);
            this.pgeStepwise.Controls.Add(this.Label44);
            this.pgeStepwise.Controls.Add(this.chkAllTurbsStep);
            this.pgeStepwise.Controls.Add(this.Label38);
            this.pgeStepwise.Controls.Add(this.chkTurbLabelStep);
            this.pgeStepwise.Controls.Add(this.chkAllMetLabelsStep);
            this.pgeStepwise.Controls.Add(this.Label37);
            this.pgeStepwise.Controls.Add(this.chkMetlabelsStep);
            this.pgeStepwise.Controls.Add(this.Label33);
            this.pgeStepwise.Controls.Add(this.cboStartMet);
            this.pgeStepwise.Controls.Add(this.Label32);
            this.pgeStepwise.Controls.Add(this.cboEndMet);
            this.pgeStepwise.Controls.Add(this.lstPathNodes);
            this.pgeStepwise.Controls.Add(this.btnExportStepwise);
            this.pgeStepwise.Controls.Add(this.lstModCrossPred);
            this.pgeStepwise.Controls.Add(this.Label11);
            this.pgeStepwise.Controls.Add(this.Label7);
            this.pgeStepwise.Location = new System.Drawing.Point(4, 27);
            this.pgeStepwise.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeStepwise.Name = "pgeStepwise";
            this.pgeStepwise.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeStepwise.Size = new System.Drawing.Size(1627, 849);
            this.pgeStepwise.TabIndex = 10;
            this.pgeStepwise.Text = "Advanced";
            this.pgeStepwise.UseVisualStyleBackColor = true;
            // 
            // plotDHModel
            // 
            this.plotDHModel.Location = new System.Drawing.Point(1060, 559);
            this.plotDHModel.Name = "plotDHModel";
            this.plotDHModel.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDHModel.Size = new System.Drawing.Size(429, 279);
            this.plotDHModel.TabIndex = 286;
            this.plotDHModel.Text = "plotView1";
            this.plotDHModel.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDHModel.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDHModel.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotUHModel
            // 
            this.plotUHModel.Location = new System.Drawing.Point(484, 554);
            this.plotUHModel.Name = "plotUHModel";
            this.plotUHModel.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotUHModel.Size = new System.Drawing.Size(429, 279);
            this.plotUHModel.TabIndex = 285;
            this.plotUHModel.Text = "plotView1";
            this.plotUHModel.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotUHModel.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotUHModel.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotPathAlongNodes
            // 
            this.plotPathAlongNodes.Location = new System.Drawing.Point(484, 250);
            this.plotPathAlongNodes.Name = "plotPathAlongNodes";
            this.plotPathAlongNodes.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotPathAlongNodes.Size = new System.Drawing.Size(846, 292);
            this.plotPathAlongNodes.TabIndex = 284;
            this.plotPathAlongNodes.Text = "plotView1";
            this.plotPathAlongNodes.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotPathAlongNodes.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotPathAlongNodes.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotAdvTopo
            // 
            this.plotAdvTopo.Location = new System.Drawing.Point(25, 51);
            this.plotAdvTopo.Name = "plotAdvTopo";
            this.plotAdvTopo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotAdvTopo.Size = new System.Drawing.Size(438, 397);
            this.plotAdvTopo.TabIndex = 283;
            this.plotAdvTopo.Text = "plotView1";
            this.plotAdvTopo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotAdvTopo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotAdvTopo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // lblTurbineTSNoAdvanced
            // 
            this.lblTurbineTSNoAdvanced.AutoSize = true;
            this.lblTurbineTSNoAdvanced.BackColor = System.Drawing.Color.White;
            this.lblTurbineTSNoAdvanced.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurbineTSNoAdvanced.Location = new System.Drawing.Point(501, 114);
            this.lblTurbineTSNoAdvanced.Name = "lblTurbineTSNoAdvanced";
            this.lblTurbineTSNoAdvanced.Size = new System.Drawing.Size(45, 18);
            this.lblTurbineTSNoAdvanced.TabIndex = 213;
            this.lblTurbineTSNoAdvanced.Text = "label5";
            // 
            // txtisMCPdAdv
            // 
            this.txtisMCPdAdv.Location = new System.Drawing.Point(810, 43);
            this.txtisMCPdAdv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtisMCPdAdv.Name = "txtisMCPdAdv";
            this.txtisMCPdAdv.ReadOnly = true;
            this.txtisMCPdAdv.Size = new System.Drawing.Size(180, 25);
            this.txtisMCPdAdv.TabIndex = 212;
            // 
            // cboSeasonAdvanced
            // 
            this.cboSeasonAdvanced.FormattingEnabled = true;
            this.cboSeasonAdvanced.Location = new System.Drawing.Point(1218, 41);
            this.cboSeasonAdvanced.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSeasonAdvanced.Name = "cboSeasonAdvanced";
            this.cboSeasonAdvanced.Size = new System.Drawing.Size(91, 26);
            this.cboSeasonAdvanced.TabIndex = 211;
            this.cboSeasonAdvanced.Text = "All Seasons";
            this.cboSeasonAdvanced.SelectedIndexChanged += new System.EventHandler(this.cboSeasonAdvanced_SelectedIndexChanged);
            // 
            // cboTODAdvanced
            // 
            this.cboTODAdvanced.FormattingEnabled = true;
            this.cboTODAdvanced.Location = new System.Drawing.Point(1218, 7);
            this.cboTODAdvanced.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTODAdvanced.Name = "cboTODAdvanced";
            this.cboTODAdvanced.Size = new System.Drawing.Size(91, 26);
            this.cboTODAdvanced.TabIndex = 209;
            this.cboTODAdvanced.Text = "All Hours";
            this.cboTODAdvanced.SelectedIndexChanged += new System.EventHandler(this.cboTODAdvanced_SelectedIndexChanged);
            // 
            // Label39
            // 
            this.Label39.AutoSize = true;
            this.Label39.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label39.Location = new System.Drawing.Point(1369, 51);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(203, 19);
            this.Label39.TabIndex = 202;
            this.Label39.Text = "Downwind Flow Estimates";
            // 
            // lstPathNodes_DW
            // 
            this.lstPathNodes_DW.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader15,
            this.ColumnHeader110,
            this.ColumnHeader108,
            this.ColumnHeader109,
            this.ColumnHeader112});
            this.lstPathNodes_DW.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPathNodes_DW.FullRowSelect = true;
            this.lstPathNodes_DW.HideSelection = false;
            this.lstPathNodes_DW.Location = new System.Drawing.Point(1305, 75);
            this.lstPathNodes_DW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstPathNodes_DW.Name = "lstPathNodes_DW";
            this.lstPathNodes_DW.Size = new System.Drawing.Size(322, 160);
            this.lstPathNodes_DW.TabIndex = 201;
            this.lstPathNodes_DW.UseCompatibleStateImageBehavior = false;
            this.lstPathNodes_DW.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader15
            // 
            this.ColumnHeader15.Text = "Site";
            this.ColumnHeader15.Width = 40;
            // 
            // ColumnHeader110
            // 
            this.ColumnHeader110.Text = "Flow";
            this.ColumnHeader110.Width = 56;
            // 
            // ColumnHeader108
            // 
            this.ColumnHeader108.Text = "Coeff";
            this.ColumnHeader108.Width = 49;
            // 
            // ColumnHeader109
            // 
            this.ColumnHeader109.Text = "dWS Expo";
            this.ColumnHeader109.Width = 64;
            // 
            // ColumnHeader112
            // 
            this.ColumnHeader112.Text = "dWS SRDH";
            this.ColumnHeader112.Width = 72;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(1030, 53);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(180, 19);
            this.Label8.TabIndex = 200;
            this.Label8.Text = "Upwind Flow Estimates";
            // 
            // lstPathNodes_UW
            // 
            this.lstPathNodes_UW.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader14,
            this.ColumnHeader121,
            this.ColumnHeader115,
            this.ColumnHeader120,
            this.ColumnHeader111});
            this.lstPathNodes_UW.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPathNodes_UW.FullRowSelect = true;
            this.lstPathNodes_UW.HideSelection = false;
            this.lstPathNodes_UW.Location = new System.Drawing.Point(966, 75);
            this.lstPathNodes_UW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstPathNodes_UW.Name = "lstPathNodes_UW";
            this.lstPathNodes_UW.Size = new System.Drawing.Size(322, 160);
            this.lstPathNodes_UW.TabIndex = 199;
            this.lstPathNodes_UW.UseCompatibleStateImageBehavior = false;
            this.lstPathNodes_UW.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader14
            // 
            this.ColumnHeader14.Text = "Site";
            this.ColumnHeader14.Width = 40;
            // 
            // ColumnHeader121
            // 
            this.ColumnHeader121.Text = "Flow";
            this.ColumnHeader121.Width = 54;
            // 
            // ColumnHeader115
            // 
            this.ColumnHeader115.Text = "Coeff";
            this.ColumnHeader115.Width = 49;
            // 
            // ColumnHeader120
            // 
            this.ColumnHeader120.Text = "dWS Expo";
            this.ColumnHeader120.Width = 63;
            // 
            // ColumnHeader111
            // 
            this.ColumnHeader111.Text = "dWS SRDH";
            this.ColumnHeader111.Width = 73;
            // 
            // txtAdv_FlowSep_Used
            // 
            this.txtAdv_FlowSep_Used.Location = new System.Drawing.Point(999, 11);
            this.txtAdv_FlowSep_Used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAdv_FlowSep_Used.Name = "txtAdv_FlowSep_Used";
            this.txtAdv_FlowSep_Used.ReadOnly = true;
            this.txtAdv_FlowSep_Used.Size = new System.Drawing.Size(180, 25);
            this.txtAdv_FlowSep_Used.TabIndex = 198;
            // 
            // Label101
            // 
            this.Label101.AutoSize = true;
            this.Label101.Location = new System.Drawing.Point(932, 643);
            this.Label101.Name = "Label101";
            this.Label101.Size = new System.Drawing.Size(122, 19);
            this.Label101.TabIndex = 197;
            this.Label101.Text = "Flow Sep. crit WS";
            // 
            // txtSepCritWS
            // 
            this.txtSepCritWS.Location = new System.Drawing.Point(946, 665);
            this.txtSepCritWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSepCritWS.Name = "txtSepCritWS";
            this.txtSepCritWS.ReadOnly = true;
            this.txtSepCritWS.Size = new System.Drawing.Size(88, 25);
            this.txtSepCritWS.TabIndex = 196;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(942, 695);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(96, 19);
            this.Label13.TabIndex = 195;
            this.Label13.Text = "Flow Sep. crit";
            // 
            // txtSepCrit
            // 
            this.txtSepCrit.Location = new System.Drawing.Point(946, 715);
            this.txtSepCrit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSepCrit.Name = "txtSepCrit";
            this.txtSepCrit.ReadOnly = true;
            this.txtSepCrit.Size = new System.Drawing.Size(88, 25);
            this.txtSepCrit.TabIndex = 194;
            // 
            // Label100
            // 
            this.Label100.AutoSize = true;
            this.Label100.Location = new System.Drawing.Point(932, 752);
            this.Label100.Name = "Label100";
            this.Label100.Size = new System.Drawing.Size(117, 19);
            this.Label100.TabIndex = 193;
            this.Label100.Text = "DH plot to show";
            // 
            // cboDHplot
            // 
            this.cboDHplot.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDHplot.FormattingEnabled = true;
            this.cboDHplot.Items.AddRange(new object[] {
            "Attached flow",
            "Separated flow"});
            this.cboDHplot.Location = new System.Drawing.Point(935, 776);
            this.cboDHplot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboDHplot.Name = "cboDHplot";
            this.cboDHplot.Size = new System.Drawing.Size(110, 26);
            this.cboDHplot.TabIndex = 192;
            this.cboDHplot.SelectedIndexChanged += new System.EventHandler(this.cboDHplot_SelectedIndexChanged);
            // 
            // btnImportModel
            // 
            this.btnImportModel.Location = new System.Drawing.Point(1507, 718);
            this.btnImportModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportModel.Name = "btnImportModel";
            this.btnImportModel.Size = new System.Drawing.Size(114, 50);
            this.btnImportModel.TabIndex = 191;
            this.btnImportModel.Text = "Import Model Parameters";
            this.btnImportModel.UseVisualStyleBackColor = true;
            this.btnImportModel.Click += new System.EventHandler(this.btnImportModel_Click);
            // 
            // chkAdvToShow
            // 
            this.chkAdvToShow.CheckOnClick = true;
            this.chkAdvToShow.ColumnWidth = 120;
            this.chkAdvToShow.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAdvToShow.FormattingEnabled = true;
            this.chkAdvToShow.Items.AddRange(new object[] {
            "UTMX",
            "UTMY",
            "Elevation",
            "P10 UW",
            "P10 DW",
            "UW Expo",
            "DW Expo",
            "UW Roughness",
            "DW Roughness",
            "UW Disp H",
            "DW Disp H",
            "dWS UW Expo",
            "dWS DW Expo",
            "dWS UW SRDH",
            "dWS DW SRDH",
            "WS Est.",
            "Equiv WS",
            "Actual WS"});
            this.chkAdvToShow.Location = new System.Drawing.Point(1373, 340);
            this.chkAdvToShow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAdvToShow.MultiColumn = true;
            this.chkAdvToShow.Name = "chkAdvToShow";
            this.chkAdvToShow.Size = new System.Drawing.Size(254, 184);
            this.chkAdvToShow.TabIndex = 187;
            this.chkAdvToShow.SelectedIndexChanged += new System.EventHandler(this.chkAdvToShow_SelectedIndexChanged);
            // 
            // chkWeight_RMS
            // 
            this.chkWeight_RMS.AutoSize = true;
            this.chkWeight_RMS.Checked = true;
            this.chkWeight_RMS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeight_RMS.Location = new System.Drawing.Point(1429, 310);
            this.chkWeight_RMS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkWeight_RMS.Name = "chkWeight_RMS";
            this.chkWeight_RMS.Size = new System.Drawing.Size(181, 23);
            this.chkWeight_RMS.TabIndex = 186;
            this.chkWeight_RMS.Text = "Sect. RMS wgtd by Rose";
            this.chkWeight_RMS.UseVisualStyleBackColor = true;
            this.chkWeight_RMS.CheckedChanged += new System.EventHandler(this.chkWeight_RMS_CheckedChanged);
            // 
            // txtSectRMS
            // 
            this.txtSectRMS.Location = new System.Drawing.Point(1527, 282);
            this.txtSectRMS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSectRMS.Name = "txtSectRMS";
            this.txtSectRMS.Size = new System.Drawing.Size(69, 25);
            this.txtSectRMS.TabIndex = 185;
            // 
            // Label48
            // 
            this.Label48.AutoSize = true;
            this.Label48.Location = new System.Drawing.Point(1403, 286);
            this.Label48.Name = "Label48";
            this.Label48.Size = new System.Drawing.Size(120, 19);
            this.Label48.TabIndex = 184;
            this.Label48.Text = "RMS Sector Ests :";
            // 
            // txtAdv_LC_used
            // 
            this.txtAdv_LC_used.Location = new System.Drawing.Point(810, 12);
            this.txtAdv_LC_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAdv_LC_used.Name = "txtAdv_LC_used";
            this.txtAdv_LC_used.ReadOnly = true;
            this.txtAdv_LC_used.Size = new System.Drawing.Size(180, 25);
            this.txtAdv_LC_used.TabIndex = 183;
            // 
            // cboExpo_or_Stab
            // 
            this.cboExpo_or_Stab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboExpo_or_Stab.FormattingEnabled = true;
            this.cboExpo_or_Stab.Items.AddRange(new object[] {
            "Exposure",
            "Roughness"});
            this.cboExpo_or_Stab.Location = new System.Drawing.Point(1510, 581);
            this.cboExpo_or_Stab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExpo_or_Stab.Name = "cboExpo_or_Stab";
            this.cboExpo_or_Stab.Size = new System.Drawing.Size(101, 21);
            this.cboExpo_or_Stab.TabIndex = 182;
            this.cboExpo_or_Stab.SelectedIndexChanged += new System.EventHandler(this.cboExpo_or_Stab_SelectedIndexChanged);
            // 
            // Label82
            // 
            this.Label82.AutoSize = true;
            this.Label82.Location = new System.Drawing.Point(1511, 561);
            this.Label82.Name = "Label82";
            this.Label82.Size = new System.Drawing.Size(95, 19);
            this.Label82.TabIndex = 181;
            this.Label82.Text = "Plot to show:";
            // 
            // btnExportModel
            // 
            this.btnExportModel.Location = new System.Drawing.Point(1507, 773);
            this.btnExportModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportModel.Name = "btnExportModel";
            this.btnExportModel.Size = new System.Drawing.Size(114, 50);
            this.btnExportModel.TabIndex = 180;
            this.btnExportModel.Text = "Export Model Parameters";
            this.btnExportModel.UseVisualStyleBackColor = true;
            this.btnExportModel.Click += new System.EventHandler(this.btnExportModel_Click);
            // 
            // Label59
            // 
            this.Label59.AutoSize = true;
            this.Label59.Location = new System.Drawing.Point(1521, 612);
            this.Label59.Name = "Label59";
            this.Label59.Size = new System.Drawing.Size(80, 19);
            this.Label59.TabIndex = 174;
            this.Label59.Text = "UW critical";
            // 
            // txtUWCrit
            // 
            this.txtUWCrit.Location = new System.Drawing.Point(1511, 631);
            this.txtUWCrit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUWCrit.Name = "txtUWCrit";
            this.txtUWCrit.ReadOnly = true;
            this.txtUWCrit.Size = new System.Drawing.Size(88, 25);
            this.txtUWCrit.TabIndex = 173;
            // 
            // Label58
            // 
            this.Label58.AutoSize = true;
            this.Label58.Location = new System.Drawing.Point(1504, 668);
            this.Label58.Name = "Label58";
            this.Label58.Size = new System.Drawing.Size(117, 19);
            this.Label58.TabIndex = 172;
            this.Label58.Text = "UH plot to show";
            // 
            // cboUphill_to_show
            // 
            this.cboUphill_to_show.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUphill_to_show.FormattingEnabled = true;
            this.cboUphill_to_show.Items.AddRange(new object[] {
            "UW > UW crit",
            "UW < UW crit"});
            this.cboUphill_to_show.Location = new System.Drawing.Point(1508, 688);
            this.cboUphill_to_show.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboUphill_to_show.Name = "cboUphill_to_show";
            this.cboUphill_to_show.Size = new System.Drawing.Size(110, 21);
            this.cboUphill_to_show.TabIndex = 169;
            this.cboUphill_to_show.SelectedIndexChanged += new System.EventHandler(this.cboUphill_to_show_SelectedIndexChanged);
            // 
            // cboAdvancedWD
            // 
            this.cboAdvancedWD.FormattingEnabled = true;
            this.cboAdvancedWD.Location = new System.Drawing.Point(1541, 16);
            this.cboAdvancedWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboAdvancedWD.Name = "cboAdvancedWD";
            this.cboAdvancedWD.Size = new System.Drawing.Size(81, 26);
            this.cboAdvancedWD.TabIndex = 168;
            this.cboAdvancedWD.SelectedIndexChanged += new System.EventHandler(this.cboWDsector_SelectedIndexChanged);
            // 
            // Label56
            // 
            this.Label56.AutoSize = true;
            this.Label56.Location = new System.Drawing.Point(1503, 18);
            this.Label56.Name = "Label56";
            this.Label56.Size = new System.Drawing.Size(38, 19);
            this.Label56.TabIndex = 167;
            this.Label56.Text = "WD:";
            // 
            // btnExportCrossPreds
            // 
            this.btnExportCrossPreds.Location = new System.Drawing.Point(295, 579);
            this.btnExportCrossPreds.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportCrossPreds.Name = "btnExportCrossPreds";
            this.btnExportCrossPreds.Size = new System.Drawing.Size(160, 48);
            this.btnExportCrossPreds.TabIndex = 159;
            this.btnExportCrossPreds.Text = "Export Met Cross-Predictions";
            this.btnExportCrossPreds.UseVisualStyleBackColor = true;
            this.btnExportCrossPreds.Click += new System.EventHandler(this.btnExportCrossPreds_Click);
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(1325, 18);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(56, 19);
            this.Label14.TabIndex = 150;
            this.Label14.Text = "Radius:";
            // 
            // cboAdvancedRad
            // 
            this.cboAdvancedRad.FormattingEnabled = true;
            this.cboAdvancedRad.Location = new System.Drawing.Point(1381, 16);
            this.cboAdvancedRad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboAdvancedRad.Name = "cboAdvancedRad";
            this.cboAdvancedRad.Size = new System.Drawing.Size(115, 26);
            this.cboAdvancedRad.TabIndex = 149;
            this.cboAdvancedRad.SelectedIndexChanged += new System.EventHandler(this.cboRadDisplay_SelectedIndexChanged);
            // 
            // txtUWDWRMS
            // 
            this.txtUWDWRMS.Location = new System.Drawing.Point(1527, 250);
            this.txtUWDWRMS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUWDWRMS.Name = "txtUWDWRMS";
            this.txtUWDWRMS.Size = new System.Drawing.Size(69, 25);
            this.txtUWDWRMS.TabIndex = 138;
            // 
            // Label44
            // 
            this.Label44.AutoSize = true;
            this.Label44.Location = new System.Drawing.Point(1407, 255);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(103, 19);
            this.Label44.TabIndex = 137;
            this.Label44.Text = "RMS WS Ests :";
            // 
            // chkAllTurbsStep
            // 
            this.chkAllTurbsStep.AutoSize = true;
            this.chkAllTurbsStep.Checked = true;
            this.chkAllTurbsStep.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllTurbsStep.Location = new System.Drawing.Point(346, 474);
            this.chkAllTurbsStep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAllTurbsStep.Name = "chkAllTurbsStep";
            this.chkAllTurbsStep.Size = new System.Drawing.Size(139, 23);
            this.chkAllTurbsStep.TabIndex = 124;
            this.chkAllTurbsStep.Text = "Select/Deselect All";
            this.chkAllTurbsStep.UseVisualStyleBackColor = true;
            this.chkAllTurbsStep.CheckedChanged += new System.EventHandler(this.chkAllTurbsStep_CheckedChanged);
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label38.Location = new System.Drawing.Point(243, 474);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(97, 18);
            this.Label38.TabIndex = 123;
            this.Label38.Text = "Turbine Sites";
            // 
            // chkTurbLabelStep
            // 
            this.chkTurbLabelStep.CheckOnClick = true;
            this.chkTurbLabelStep.FormattingEnabled = true;
            this.chkTurbLabelStep.Location = new System.Drawing.Point(246, 498);
            this.chkTurbLabelStep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbLabelStep.Name = "chkTurbLabelStep";
            this.chkTurbLabelStep.Size = new System.Drawing.Size(217, 64);
            this.chkTurbLabelStep.TabIndex = 122;
            this.chkTurbLabelStep.SelectedIndexChanged += new System.EventHandler(this.chkTurbLabelStep_SelectedIndexChanged);
            // 
            // chkAllMetLabelsStep
            // 
            this.chkAllMetLabelsStep.AutoSize = true;
            this.chkAllMetLabelsStep.Checked = true;
            this.chkAllMetLabelsStep.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllMetLabelsStep.Location = new System.Drawing.Point(103, 474);
            this.chkAllMetLabelsStep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAllMetLabelsStep.Name = "chkAllMetLabelsStep";
            this.chkAllMetLabelsStep.Size = new System.Drawing.Size(139, 23);
            this.chkAllMetLabelsStep.TabIndex = 121;
            this.chkAllMetLabelsStep.Text = "Select/Deselect All";
            this.chkAllMetLabelsStep.UseVisualStyleBackColor = true;
            this.chkAllMetLabelsStep.CheckedChanged += new System.EventHandler(this.chkAllMetLabelsStep_CheckedChanged);
            // 
            // Label37
            // 
            this.Label37.AutoSize = true;
            this.Label37.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label37.Location = new System.Drawing.Point(19, 474);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(72, 18);
            this.Label37.TabIndex = 120;
            this.Label37.Text = "Met Sites";
            // 
            // chkMetlabelsStep
            // 
            this.chkMetlabelsStep.CheckOnClick = true;
            this.chkMetlabelsStep.FormattingEnabled = true;
            this.chkMetlabelsStep.Location = new System.Drawing.Point(17, 498);
            this.chkMetlabelsStep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetlabelsStep.Name = "chkMetlabelsStep";
            this.chkMetlabelsStep.Size = new System.Drawing.Size(217, 64);
            this.chkMetlabelsStep.TabIndex = 119;
            this.chkMetlabelsStep.SelectedIndexChanged += new System.EventHandler(this.chkMetlabelsStep_SelectedIndexChanged);
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.Location = new System.Drawing.Point(574, 14);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(75, 19);
            this.Label33.TabIndex = 107;
            this.Label33.Text = "From Met:";
            // 
            // cboStartMet
            // 
            this.cboStartMet.FormattingEnabled = true;
            this.cboStartMet.Location = new System.Drawing.Point(654, 10);
            this.cboStartMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboStartMet.Name = "cboStartMet";
            this.cboStartMet.Size = new System.Drawing.Size(125, 26);
            this.cboStartMet.TabIndex = 106;
            this.cboStartMet.SelectedIndexChanged += new System.EventHandler(this.cboStartMet_SelectedIndexChanged);
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.Location = new System.Drawing.Point(521, 44);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(130, 19);
            this.Label32.TabIndex = 105;
            this.Label32.Text = "To Met or Turbine:";
            // 
            // cboEndMet
            // 
            this.cboEndMet.FormattingEnabled = true;
            this.cboEndMet.Location = new System.Drawing.Point(654, 41);
            this.cboEndMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboEndMet.Name = "cboEndMet";
            this.cboEndMet.Size = new System.Drawing.Size(125, 26);
            this.cboEndMet.TabIndex = 104;
            this.cboEndMet.SelectedIndexChanged += new System.EventHandler(this.cboEndMet_SelectedIndexChanged);
            // 
            // lstPathNodes
            // 
            this.lstPathNodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Site,
            this.UTMX,
            this.UTMY,
            this.elev,
            this.P10UW,
            this.P10DW,
            this.UW,
            this.DW,
            this.WSEst,
            this.UW_SR,
            this.DW_SR,
            this.UW_DH,
            this.DW_DH});
            this.lstPathNodes.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPathNodes.FullRowSelect = true;
            this.lstPathNodes.HideSelection = false;
            this.lstPathNodes.Location = new System.Drawing.Point(484, 75);
            this.lstPathNodes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstPathNodes.Name = "lstPathNodes";
            this.lstPathNodes.Size = new System.Drawing.Size(474, 160);
            this.lstPathNodes.TabIndex = 102;
            this.lstPathNodes.UseCompatibleStateImageBehavior = false;
            this.lstPathNodes.View = System.Windows.Forms.View.Details;
            // 
            // Site
            // 
            this.Site.Text = "Site";
            this.Site.Width = 61;
            // 
            // UTMX
            // 
            this.UTMX.Text = "UTMX";
            this.UTMX.Width = 56;
            // 
            // UTMY
            // 
            this.UTMY.Text = "UTMY";
            this.UTMY.Width = 63;
            // 
            // elev
            // 
            this.elev.Text = "elev";
            // 
            // P10UW
            // 
            this.P10UW.Text = "P10 UW";
            this.P10UW.Width = 67;
            // 
            // P10DW
            // 
            this.P10DW.Text = "P10 DW";
            this.P10DW.Width = 73;
            // 
            // UW
            // 
            this.UW.Text = "UW";
            // 
            // DW
            // 
            this.DW.Text = "DW";
            // 
            // WSEst
            // 
            this.WSEst.Text = "WS Est";
            this.WSEst.Width = 69;
            // 
            // UW_SR
            // 
            this.UW_SR.Text = "UW SR";
            // 
            // DW_SR
            // 
            this.DW_SR.Text = "DW SR";
            // 
            // UW_DH
            // 
            this.UW_DH.Text = "UW DH";
            // 
            // DW_DH
            // 
            this.DW_DH.Text = "DW DH";
            // 
            // btnExportStepwise
            // 
            this.btnExportStepwise.Location = new System.Drawing.Point(934, 559);
            this.btnExportStepwise.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportStepwise.Name = "btnExportStepwise";
            this.btnExportStepwise.Size = new System.Drawing.Size(111, 66);
            this.btnExportStepwise.TabIndex = 101;
            this.btnExportStepwise.Text = "Export Nodes and WS Estimates";
            this.btnExportStepwise.UseVisualStyleBackColor = true;
            this.btnExportStepwise.Click += new System.EventHandler(this.btnExportStepwise_Click);
            // 
            // lstModCrossPred
            // 
            this.lstModCrossPred.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader16,
            this.ColumnHeader26,
            this.ColumnHeader30,
            this.ColumnHeader31});
            this.lstModCrossPred.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstModCrossPred.FullRowSelect = true;
            this.lstModCrossPred.HideSelection = false;
            this.lstModCrossPred.Location = new System.Drawing.Point(17, 636);
            this.lstModCrossPred.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstModCrossPred.Name = "lstModCrossPred";
            this.lstModCrossPred.Size = new System.Drawing.Size(446, 175);
            this.lstModCrossPred.TabIndex = 91;
            this.lstModCrossPred.UseCompatibleStateImageBehavior = false;
            this.lstModCrossPred.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader16
            // 
            this.ColumnHeader16.Text = "Predictor";
            this.ColumnHeader16.Width = 96;
            // 
            // ColumnHeader26
            // 
            this.ColumnHeader26.Text = "Target";
            this.ColumnHeader26.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnHeader26.Width = 142;
            // 
            // ColumnHeader30
            // 
            this.ColumnHeader30.Text = "WS Est.";
            this.ColumnHeader30.Width = 69;
            // 
            // ColumnHeader31
            // 
            this.ColumnHeader31.Text = "% Error";
            this.ColumnHeader31.Width = 64;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(30, 586);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(175, 38);
            this.Label11.TabIndex = 87;
            this.Label11.Text = "Summary of Met \r\nCross-Prediction Errors";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(9, 9);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(512, 25);
            this.Label7.TabIndex = 84;
            this.Label7.Text = "Continuum Wind Flow Model: Advanced Analysis";
            // 
            // pgeSuitability
            // 
            this.pgeSuitability.Controls.Add(this.plotIceVsDist);
            this.pgeSuitability.Controls.Add(this.plotIceShadowSound);
            this.pgeSuitability.Controls.Add(this.lblIceDistOrHisto);
            this.pgeSuitability.Controls.Add(this.cboIceDistORIceHisto);
            this.pgeSuitability.Controls.Add(this.label91);
            this.pgeSuitability.Controls.Add(this.txtNumIceThrowsPerDay);
            this.pgeSuitability.Controls.Add(this.txtMaxFlickerHours);
            this.pgeSuitability.Controls.Add(this.dateMaxFlicker);
            this.pgeSuitability.Controls.Add(this.lblMaxFlickerHours);
            this.pgeSuitability.Controls.Add(this.lblMaxFlickerDay);
            this.pgeSuitability.Controls.Add(this.txtTurbineNoise);
            this.pgeSuitability.Controls.Add(this.label90);
            this.pgeSuitability.Controls.Add(this.txtNumIceDays);
            this.pgeSuitability.Controls.Add(this.label89);
            this.pgeSuitability.Controls.Add(this.btnExportIceVsDist);
            this.pgeSuitability.Controls.Add(this.btnExportSoundSummary);
            this.pgeSuitability.Controls.Add(this.btnExportShadowFlicker);
            this.pgeSuitability.Controls.Add(this.btnExportIceSummary);
            this.pgeSuitability.Controls.Add(this.label176);
            this.pgeSuitability.Controls.Add(this.lstZoneSound);
            this.pgeSuitability.Controls.Add(this.label175);
            this.pgeSuitability.Controls.Add(this.lblShadowByMonthOrIceByDist);
            this.pgeSuitability.Controls.Add(this.lstZoneIceHits);
            this.pgeSuitability.Controls.Add(this.label173);
            this.pgeSuitability.Controls.Add(this.cboIcingYear);
            this.pgeSuitability.Controls.Add(this.lstShadowZoneSummary);
            this.pgeSuitability.Controls.Add(this.txtTotalShadow);
            this.pgeSuitability.Controls.Add(this.lblTotalHoursPerYear);
            this.pgeSuitability.Controls.Add(this.lblShadowOrIceByDist);
            this.pgeSuitability.Controls.Add(this.lblZoneOrDirection);
            this.pgeSuitability.Controls.Add(this.cboZoneList);
            this.pgeSuitability.Controls.Add(this.lstShadow12x24);
            this.pgeSuitability.Controls.Add(this.label169);
            this.pgeSuitability.Controls.Add(this.cboSiteSuitHour);
            this.pgeSuitability.Controls.Add(this.label165);
            this.pgeSuitability.Controls.Add(this.cboSiteSuitMonth);
            this.pgeSuitability.Controls.Add(this.btnSiteSuitImportCRV);
            this.pgeSuitability.Controls.Add(this.btnDelZones);
            this.pgeSuitability.Controls.Add(this.label164);
            this.pgeSuitability.Controls.Add(this.cboSiteSuitabilitySelectPlot);
            this.pgeSuitability.Controls.Add(this.lstZones);
            this.pgeSuitability.Controls.Add(this.btnRunSoundModel);
            this.pgeSuitability.Controls.Add(this.btnRunShadowFlicker);
            this.pgeSuitability.Controls.Add(this.btnRunIceThrow);
            this.pgeSuitability.Controls.Add(this.label167);
            this.pgeSuitability.Controls.Add(this.label166);
            this.pgeSuitability.Controls.Add(this.cboSiteSuitPowerCurve);
            this.pgeSuitability.Controls.Add(this.btnImportZones);
            this.pgeSuitability.Location = new System.Drawing.Point(4, 27);
            this.pgeSuitability.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeSuitability.Name = "pgeSuitability";
            this.pgeSuitability.Size = new System.Drawing.Size(1627, 849);
            this.pgeSuitability.TabIndex = 18;
            this.pgeSuitability.Text = "Site Suitability";
            this.pgeSuitability.UseVisualStyleBackColor = true;
            // 
            // plotIceVsDist
            // 
            this.plotIceVsDist.Location = new System.Drawing.Point(1003, 443);
            this.plotIceVsDist.Name = "plotIceVsDist";
            this.plotIceVsDist.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotIceVsDist.Size = new System.Drawing.Size(619, 371);
            this.plotIceVsDist.TabIndex = 285;
            this.plotIceVsDist.Text = "plotView1";
            this.plotIceVsDist.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotIceVsDist.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotIceVsDist.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotIceShadowSound
            // 
            this.plotIceShadowSound.Location = new System.Drawing.Point(345, 171);
            this.plotIceShadowSound.Name = "plotIceShadowSound";
            this.plotIceShadowSound.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotIceShadowSound.Size = new System.Drawing.Size(623, 564);
            this.plotIceShadowSound.TabIndex = 284;
            this.plotIceShadowSound.Text = "plotView1";
            this.plotIceShadowSound.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotIceShadowSound.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotIceShadowSound.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // lblIceDistOrHisto
            // 
            this.lblIceDistOrHisto.AutoSize = true;
            this.lblIceDistOrHisto.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIceDistOrHisto.Location = new System.Drawing.Point(1168, 401);
            this.lblIceDistOrHisto.Name = "lblIceDistOrHisto";
            this.lblIceDistOrHisto.Size = new System.Drawing.Size(75, 18);
            this.lblIceDistOrHisto.TabIndex = 278;
            this.lblIceDistOrHisto.Text = "Select Plot :";
            // 
            // cboIceDistORIceHisto
            // 
            this.cboIceDistORIceHisto.FormattingEnabled = true;
            this.cboIceDistORIceHisto.Items.AddRange(new object[] {
            "Ice Hit vs. Distance",
            "Yearly Ice Hit Histogram"});
            this.cboIceDistORIceHisto.Location = new System.Drawing.Point(1249, 399);
            this.cboIceDistORIceHisto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboIceDistORIceHisto.Name = "cboIceDistORIceHisto";
            this.cboIceDistORIceHisto.Size = new System.Drawing.Size(258, 26);
            this.cboIceDistORIceHisto.TabIndex = 277;
            this.cboIceDistORIceHisto.SelectedIndexChanged += new System.EventHandler(this.cboIceDistORIceHisto_SelectedIndexChanged);
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(369, 97);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(110, 18);
            this.label91.TabIndex = 276;
            this.label91.Text = "# Ice Days / Year :";
            // 
            // txtNumIceThrowsPerDay
            // 
            this.txtNumIceThrowsPerDay.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumIceThrowsPerDay.Location = new System.Drawing.Point(499, 69);
            this.txtNumIceThrowsPerDay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNumIceThrowsPerDay.Name = "txtNumIceThrowsPerDay";
            this.txtNumIceThrowsPerDay.Size = new System.Drawing.Size(44, 25);
            this.txtNumIceThrowsPerDay.TabIndex = 275;
            this.txtNumIceThrowsPerDay.TextChanged += new System.EventHandler(this.txtNumIceDays_TextChanged);
            // 
            // txtMaxFlickerHours
            // 
            this.txtMaxFlickerHours.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxFlickerHours.Location = new System.Drawing.Point(1526, 399);
            this.txtMaxFlickerHours.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaxFlickerHours.Name = "txtMaxFlickerHours";
            this.txtMaxFlickerHours.ReadOnly = true;
            this.txtMaxFlickerHours.Size = new System.Drawing.Size(65, 25);
            this.txtMaxFlickerHours.TabIndex = 274;
            // 
            // dateMaxFlicker
            // 
            this.dateMaxFlicker.CustomFormat = "MM/dd/yy";
            this.dateMaxFlicker.Enabled = false;
            this.dateMaxFlicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMaxFlicker.Location = new System.Drawing.Point(1501, 367);
            this.dateMaxFlicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateMaxFlicker.Name = "dateMaxFlicker";
            this.dateMaxFlicker.Size = new System.Drawing.Size(108, 25);
            this.dateMaxFlicker.TabIndex = 273;
            // 
            // lblMaxFlickerHours
            // 
            this.lblMaxFlickerHours.AutoSize = true;
            this.lblMaxFlickerHours.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxFlickerHours.Location = new System.Drawing.Point(1394, 402);
            this.lblMaxFlickerHours.Name = "lblMaxFlickerHours";
            this.lblMaxFlickerHours.Size = new System.Drawing.Size(108, 18);
            this.lblMaxFlickerHours.TabIndex = 272;
            this.lblMaxFlickerHours.Text = "# of Flicker Mins:";
            // 
            // lblMaxFlickerDay
            // 
            this.lblMaxFlickerDay.AutoSize = true;
            this.lblMaxFlickerDay.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxFlickerDay.Location = new System.Drawing.Point(1388, 370);
            this.lblMaxFlickerDay.Name = "lblMaxFlickerDay";
            this.lblMaxFlickerDay.Size = new System.Drawing.Size(113, 18);
            this.lblMaxFlickerDay.TabIndex = 271;
            this.lblMaxFlickerDay.Text = "Max. Flicker Day :";
            // 
            // txtTurbineNoise
            // 
            this.txtTurbineNoise.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTurbineNoise.Location = new System.Drawing.Point(783, 70);
            this.txtTurbineNoise.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTurbineNoise.Name = "txtTurbineNoise";
            this.txtTurbineNoise.Size = new System.Drawing.Size(44, 25);
            this.txtTurbineNoise.TabIndex = 270;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(648, 73);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(134, 18);
            this.label90.TabIndex = 269;
            this.label90.Text = "Turbine Noise (dBA) :";
            // 
            // txtNumIceDays
            // 
            this.txtNumIceDays.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumIceDays.Location = new System.Drawing.Point(499, 97);
            this.txtNumIceDays.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNumIceDays.Name = "txtNumIceDays";
            this.txtNumIceDays.Size = new System.Drawing.Size(44, 25);
            this.txtNumIceDays.TabIndex = 268;
            this.txtNumIceDays.TextChanged += new System.EventHandler(this.txtNumIceDays_TextChanged);
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(369, 70);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(124, 18);
            this.label89.TabIndex = 267;
            this.label89.Text = "# Ice Throws / Day :";
            // 
            // btnExportIceVsDist
            // 
            this.btnExportIceVsDist.Location = new System.Drawing.Point(526, 760);
            this.btnExportIceVsDist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportIceVsDist.Name = "btnExportIceVsDist";
            this.btnExportIceVsDist.Size = new System.Drawing.Size(122, 68);
            this.btnExportIceVsDist.TabIndex = 266;
            this.btnExportIceVsDist.Text = "Export Ice Throw vs. Distance";
            this.btnExportIceVsDist.UseVisualStyleBackColor = true;
            this.btnExportIceVsDist.Click += new System.EventHandler(this.btnExportIceVsDist_Click);
            // 
            // btnExportSoundSummary
            // 
            this.btnExportSoundSummary.Location = new System.Drawing.Point(796, 760);
            this.btnExportSoundSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportSoundSummary.Name = "btnExportSoundSummary";
            this.btnExportSoundSummary.Size = new System.Drawing.Size(122, 68);
            this.btnExportSoundSummary.TabIndex = 264;
            this.btnExportSoundSummary.Text = "Export Sound Model Summary";
            this.btnExportSoundSummary.UseVisualStyleBackColor = true;
            this.btnExportSoundSummary.Click += new System.EventHandler(this.btnExportSoundSummary_Click);
            // 
            // btnExportShadowFlicker
            // 
            this.btnExportShadowFlicker.Location = new System.Drawing.Point(661, 760);
            this.btnExportShadowFlicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportShadowFlicker.Name = "btnExportShadowFlicker";
            this.btnExportShadowFlicker.Size = new System.Drawing.Size(122, 68);
            this.btnExportShadowFlicker.TabIndex = 263;
            this.btnExportShadowFlicker.Text = "Export Shadow Flicker Summary";
            this.btnExportShadowFlicker.UseVisualStyleBackColor = true;
            this.btnExportShadowFlicker.Click += new System.EventHandler(this.btnExportShadowFlicker_Click);
            // 
            // btnExportIceSummary
            // 
            this.btnExportIceSummary.Location = new System.Drawing.Point(391, 760);
            this.btnExportIceSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportIceSummary.Name = "btnExportIceSummary";
            this.btnExportIceSummary.Size = new System.Drawing.Size(122, 68);
            this.btnExportIceSummary.TabIndex = 262;
            this.btnExportIceSummary.Text = "Export Ice Throw Summary";
            this.btnExportIceSummary.UseVisualStyleBackColor = true;
            this.btnExportIceSummary.Click += new System.EventHandler(this.btnExportIceSummary_Click);
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label176.Location = new System.Drawing.Point(1466, 27);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(156, 19);
            this.label176.TabIndex = 261;
            this.label176.Text = "Sound Levels (dBA)";
            // 
            // lstZoneSound
            // 
            this.lstZoneSound.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader158,
            this.columnHeader159});
            this.lstZoneSound.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstZoneSound.GridLines = true;
            this.lstZoneSound.HideSelection = false;
            this.lstZoneSound.Location = new System.Drawing.Point(1475, 59);
            this.lstZoneSound.Margin = new System.Windows.Forms.Padding(2);
            this.lstZoneSound.Name = "lstZoneSound";
            this.lstZoneSound.Size = new System.Drawing.Size(147, 290);
            this.lstZoneSound.TabIndex = 260;
            this.lstZoneSound.UseCompatibleStateImageBehavior = false;
            this.lstZoneSound.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader158
            // 
            this.columnHeader158.Text = "Zone";
            this.columnHeader158.Width = 40;
            // 
            // columnHeader159
            // 
            this.columnHeader159.Text = "Sound Level";
            this.columnHeader159.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader159.Width = 82;
            // 
            // label175
            // 
            this.label175.AutoSize = true;
            this.label175.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label175.Location = new System.Drawing.Point(1158, 26);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(62, 19);
            this.label175.TabIndex = 259;
            this.label175.Text = "Ice Hits";
            // 
            // lblShadowByMonthOrIceByDist
            // 
            this.lblShadowByMonthOrIceByDist.AutoSize = true;
            this.lblShadowByMonthOrIceByDist.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShadowByMonthOrIceByDist.Location = new System.Drawing.Point(1000, 367);
            this.lblShadowByMonthOrIceByDist.Name = "lblShadowByMonthOrIceByDist";
            this.lblShadowByMonthOrIceByDist.Size = new System.Drawing.Size(325, 19);
            this.lblShadowByMonthOrIceByDist.TabIndex = 258;
            this.lblShadowByMonthOrIceByDist.Text = "Hours of Shadow Flicker by Month / Hour :";
            // 
            // lstZoneIceHits
            // 
            this.lstZoneIceHits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader154,
            this.columnHeader155,
            this.columnHeader156,
            this.columnHeader157,
            this.columnHeader175,
            this.columnHeader176});
            this.lstZoneIceHits.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstZoneIceHits.GridLines = true;
            this.lstZoneIceHits.HideSelection = false;
            this.lstZoneIceHits.Location = new System.Drawing.Point(1158, 59);
            this.lstZoneIceHits.Margin = new System.Windows.Forms.Padding(2);
            this.lstZoneIceHits.Name = "lstZoneIceHits";
            this.lstZoneIceHits.Size = new System.Drawing.Size(298, 290);
            this.lstZoneIceHits.TabIndex = 257;
            this.lstZoneIceHits.UseCompatibleStateImageBehavior = false;
            this.lstZoneIceHits.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader154
            // 
            this.columnHeader154.Text = "Zone";
            this.columnHeader154.Width = 40;
            // 
            // columnHeader155
            // 
            this.columnHeader155.Text = "Hits";
            this.columnHeader155.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader155.Width = 40;
            // 
            // columnHeader156
            // 
            this.columnHeader156.Text = "Min";
            this.columnHeader156.Width = 41;
            // 
            // columnHeader157
            // 
            this.columnHeader157.Text = "Max";
            this.columnHeader157.Width = 40;
            // 
            // columnHeader175
            // 
            this.columnHeader175.Text = "% Prob > 0";
            this.columnHeader175.Width = 69;
            // 
            // columnHeader176
            // 
            this.columnHeader176.Text = "% Prob > 1";
            // 
            // label173
            // 
            this.label173.AutoSize = true;
            this.label173.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label173.Location = new System.Drawing.Point(839, 79);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(41, 18);
            this.label173.TabIndex = 256;
            this.label173.Text = "Year :";
            // 
            // cboIcingYear
            // 
            this.cboIcingYear.FormattingEnabled = true;
            this.cboIcingYear.Location = new System.Drawing.Point(894, 75);
            this.cboIcingYear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboIcingYear.Name = "cboIcingYear";
            this.cboIcingYear.Size = new System.Drawing.Size(74, 26);
            this.cboIcingYear.TabIndex = 255;
            this.cboIcingYear.Text = "1";
            this.cboIcingYear.SelectedIndexChanged += new System.EventHandler(this.cboIcingYear_SelectedIndexChanged);
            // 
            // lstShadowZoneSummary
            // 
            this.lstShadowZoneSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader152,
            this.columnHeader153});
            this.lstShadowZoneSummary.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstShadowZoneSummary.GridLines = true;
            this.lstShadowZoneSummary.HideSelection = false;
            this.lstShadowZoneSummary.Location = new System.Drawing.Point(993, 59);
            this.lstShadowZoneSummary.Margin = new System.Windows.Forms.Padding(2);
            this.lstShadowZoneSummary.Name = "lstShadowZoneSummary";
            this.lstShadowZoneSummary.Size = new System.Drawing.Size(147, 290);
            this.lstShadowZoneSummary.TabIndex = 254;
            this.lstShadowZoneSummary.UseCompatibleStateImageBehavior = false;
            this.lstShadowZoneSummary.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader152
            // 
            this.columnHeader152.Text = "Zone";
            this.columnHeader152.Width = 40;
            // 
            // columnHeader153
            // 
            this.columnHeader153.Text = "Total Hours";
            this.columnHeader153.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader153.Width = 82;
            // 
            // txtTotalShadow
            // 
            this.txtTotalShadow.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalShadow.Location = new System.Drawing.Point(1314, 399);
            this.txtTotalShadow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalShadow.Name = "txtTotalShadow";
            this.txtTotalShadow.ReadOnly = true;
            this.txtTotalShadow.Size = new System.Drawing.Size(65, 25);
            this.txtTotalShadow.TabIndex = 253;
            // 
            // lblTotalHoursPerYear
            // 
            this.lblTotalHoursPerYear.AutoSize = true;
            this.lblTotalHoursPerYear.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHoursPerYear.Location = new System.Drawing.Point(1176, 402);
            this.lblTotalHoursPerYear.Name = "lblTotalHoursPerYear";
            this.lblTotalHoursPerYear.Size = new System.Drawing.Size(138, 18);
            this.lblTotalHoursPerYear.TabIndex = 224;
            this.lblTotalHoursPerYear.Text = "Total Hours Per Year :";
            // 
            // lblShadowOrIceByDist
            // 
            this.lblShadowOrIceByDist.AutoSize = true;
            this.lblShadowOrIceByDist.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShadowOrIceByDist.Location = new System.Drawing.Point(991, 28);
            this.lblShadowOrIceByDist.Name = "lblShadowOrIceByDist";
            this.lblShadowOrIceByDist.Size = new System.Drawing.Size(126, 19);
            this.lblShadowOrIceByDist.TabIndex = 223;
            this.lblShadowOrIceByDist.Text = "Shadow Flicker";
            // 
            // lblZoneOrDirection
            // 
            this.lblZoneOrDirection.AutoSize = true;
            this.lblZoneOrDirection.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoneOrDirection.Location = new System.Drawing.Point(1009, 405);
            this.lblZoneOrDirection.Name = "lblZoneOrDirection";
            this.lblZoneOrDirection.Size = new System.Drawing.Size(44, 18);
            this.lblZoneOrDirection.TabIndex = 222;
            this.lblZoneOrDirection.Text = "Zone :";
            // 
            // cboZoneList
            // 
            this.cboZoneList.FormattingEnabled = true;
            this.cboZoneList.Location = new System.Drawing.Point(1066, 398);
            this.cboZoneList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboZoneList.Name = "cboZoneList";
            this.cboZoneList.Size = new System.Drawing.Size(74, 26);
            this.cboZoneList.TabIndex = 221;
            this.cboZoneList.SelectedIndexChanged += new System.EventHandler(this.cboZoneList_SelectedIndexChanged);
            // 
            // lstShadow12x24
            // 
            this.lstShadow12x24.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader138,
            this.columnHeader139,
            this.columnHeader140,
            this.columnHeader151,
            this.columnHeader141,
            this.columnHeader142,
            this.columnHeader143,
            this.columnHeader144,
            this.columnHeader145,
            this.columnHeader146,
            this.columnHeader147,
            this.columnHeader148,
            this.columnHeader149,
            this.columnHeader150});
            this.lstShadow12x24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstShadow12x24.GridLines = true;
            this.lstShadow12x24.HideSelection = false;
            this.lstShadow12x24.Location = new System.Drawing.Point(1003, 443);
            this.lstShadow12x24.Margin = new System.Windows.Forms.Padding(2);
            this.lstShadow12x24.Name = "lstShadow12x24";
            this.lstShadow12x24.Size = new System.Drawing.Size(619, 371);
            this.lstShadow12x24.TabIndex = 219;
            this.lstShadow12x24.UseCompatibleStateImageBehavior = false;
            this.lstShadow12x24.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader138
            // 
            this.columnHeader138.Text = "";
            this.columnHeader138.Width = 40;
            // 
            // columnHeader139
            // 
            this.columnHeader139.Text = "All";
            this.columnHeader139.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader139.Width = 41;
            // 
            // columnHeader140
            // 
            this.columnHeader140.Text = "Jan";
            this.columnHeader140.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader140.Width = 38;
            // 
            // columnHeader151
            // 
            this.columnHeader151.Text = "Feb";
            this.columnHeader151.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader151.Width = 36;
            // 
            // columnHeader141
            // 
            this.columnHeader141.Text = "Mar";
            this.columnHeader141.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader141.Width = 37;
            // 
            // columnHeader142
            // 
            this.columnHeader142.Text = "Apr";
            this.columnHeader142.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader142.Width = 36;
            // 
            // columnHeader143
            // 
            this.columnHeader143.Text = "May";
            this.columnHeader143.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader143.Width = 39;
            // 
            // columnHeader144
            // 
            this.columnHeader144.Text = "Jun";
            this.columnHeader144.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader144.Width = 39;
            // 
            // columnHeader145
            // 
            this.columnHeader145.Text = "Jul";
            this.columnHeader145.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader145.Width = 33;
            // 
            // columnHeader146
            // 
            this.columnHeader146.Text = "Aug";
            this.columnHeader146.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader146.Width = 38;
            // 
            // columnHeader147
            // 
            this.columnHeader147.Text = "Sep";
            this.columnHeader147.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader147.Width = 39;
            // 
            // columnHeader148
            // 
            this.columnHeader148.Text = "Oct";
            this.columnHeader148.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader148.Width = 37;
            // 
            // columnHeader149
            // 
            this.columnHeader149.Text = "Nov";
            this.columnHeader149.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader149.Width = 35;
            // 
            // columnHeader150
            // 
            this.columnHeader150.Text = "Dec";
            this.columnHeader150.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader150.Width = 38;
            // 
            // label169
            // 
            this.label169.AutoSize = true;
            this.label169.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label169.Location = new System.Drawing.Point(836, 18);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(45, 18);
            this.label169.TabIndex = 218;
            this.label169.Text = "Hour :";
            // 
            // cboSiteSuitHour
            // 
            this.cboSiteSuitHour.FormattingEnabled = true;
            this.cboSiteSuitHour.Items.AddRange(new object[] {
            "All",
            "4 am",
            "5 am",
            "6 am",
            "7 am",
            "8 am",
            "9 am",
            "10 am",
            "11 am",
            "12 pm",
            "1 pm",
            "2 pm",
            "3 pm",
            "4 pm",
            "5 pm",
            "6 pm",
            "7 pm",
            "8 pm",
            "9 pm",
            "10 pm"});
            this.cboSiteSuitHour.Location = new System.Drawing.Point(894, 11);
            this.cboSiteSuitHour.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSiteSuitHour.Name = "cboSiteSuitHour";
            this.cboSiteSuitHour.Size = new System.Drawing.Size(74, 26);
            this.cboSiteSuitHour.TabIndex = 217;
            this.cboSiteSuitHour.Text = "All";
            this.cboSiteSuitHour.SelectedIndexChanged += new System.EventHandler(this.cboSiteSuitHour_SelectedIndexChanged);
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label165.Location = new System.Drawing.Point(836, 49);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(54, 18);
            this.label165.TabIndex = 216;
            this.label165.Text = "Month :";
            // 
            // cboSiteSuitMonth
            // 
            this.cboSiteSuitMonth.FormattingEnabled = true;
            this.cboSiteSuitMonth.Items.AddRange(new object[] {
            "All",
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec"});
            this.cboSiteSuitMonth.Location = new System.Drawing.Point(894, 42);
            this.cboSiteSuitMonth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSiteSuitMonth.Name = "cboSiteSuitMonth";
            this.cboSiteSuitMonth.Size = new System.Drawing.Size(74, 26);
            this.cboSiteSuitMonth.TabIndex = 215;
            this.cboSiteSuitMonth.Text = "All";
            this.cboSiteSuitMonth.SelectedIndexChanged += new System.EventHandler(this.cboSiteSuitMonth_SelectedIndexChanged);
            // 
            // btnSiteSuitImportCRV
            // 
            this.btnSiteSuitImportCRV.BackColor = System.Drawing.Color.LightCoral;
            this.btnSiteSuitImportCRV.Location = new System.Drawing.Point(171, 62);
            this.btnSiteSuitImportCRV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSiteSuitImportCRV.Name = "btnSiteSuitImportCRV";
            this.btnSiteSuitImportCRV.Size = new System.Drawing.Size(129, 55);
            this.btnSiteSuitImportCRV.TabIndex = 214;
            this.btnSiteSuitImportCRV.Text = "Import Power Curve";
            this.btnSiteSuitImportCRV.UseVisualStyleBackColor = false;
            this.btnSiteSuitImportCRV.Click += new System.EventHandler(this.btnSiteSuitImportCRV_Click);
            // 
            // btnDelZones
            // 
            this.btnDelZones.Location = new System.Drawing.Point(178, 711);
            this.btnDelZones.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelZones.Name = "btnDelZones";
            this.btnDelZones.Size = new System.Drawing.Size(122, 53);
            this.btnDelZones.TabIndex = 201;
            this.btnDelZones.Text = "Delete Zone Location(s)";
            this.btnDelZones.UseVisualStyleBackColor = true;
            this.btnDelZones.Click += new System.EventHandler(this.btnDelZones_Click);
            // 
            // label164
            // 
            this.label164.AutoSize = true;
            this.label164.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label164.Location = new System.Drawing.Point(420, 135);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(143, 18);
            this.label164.TabIndex = 200;
            this.label164.Text = "Site Suitability Model :";
            // 
            // cboSiteSuitabilitySelectPlot
            // 
            this.cboSiteSuitabilitySelectPlot.FormattingEnabled = true;
            this.cboSiteSuitabilitySelectPlot.Location = new System.Drawing.Point(566, 131);
            this.cboSiteSuitabilitySelectPlot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSiteSuitabilitySelectPlot.Name = "cboSiteSuitabilitySelectPlot";
            this.cboSiteSuitabilitySelectPlot.Size = new System.Drawing.Size(243, 26);
            this.cboSiteSuitabilitySelectPlot.TabIndex = 199;
            this.cboSiteSuitabilitySelectPlot.SelectedIndexChanged += new System.EventHandler(this.cboSiteSuitabilitySelectPlot_SelectedIndexChanged);
            // 
            // lstZones
            // 
            this.lstZones.CheckBoxes = true;
            this.lstZones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader133,
            this.columnHeader134,
            this.columnHeader135,
            this.columnHeader136,
            this.columnHeader137});
            this.lstZones.FullRowSelect = true;
            this.lstZones.HideSelection = false;
            this.lstZones.Location = new System.Drawing.Point(14, 171);
            this.lstZones.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstZones.Name = "lstZones";
            this.lstZones.Size = new System.Drawing.Size(307, 522);
            this.lstZones.TabIndex = 167;
            this.lstZones.UseCompatibleStateImageBehavior = false;
            this.lstZones.View = System.Windows.Forms.View.Details;
            this.lstZones.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstZones_ItemCheckChanged);
            // 
            // columnHeader133
            // 
            this.columnHeader133.Text = "Name";
            this.columnHeader133.Width = 58;
            // 
            // columnHeader134
            // 
            this.columnHeader134.Text = "Latitude";
            this.columnHeader134.Width = 50;
            // 
            // columnHeader135
            // 
            this.columnHeader135.Text = "Longitude";
            this.columnHeader135.Width = 61;
            // 
            // columnHeader136
            // 
            this.columnHeader136.Text = "X Length [m]";
            this.columnHeader136.Width = 68;
            // 
            // columnHeader137
            // 
            this.columnHeader137.Text = "Y Length [m]";
            this.columnHeader137.Width = 77;
            // 
            // btnRunSoundModel
            // 
            this.btnRunSoundModel.Location = new System.Drawing.Point(692, 10);
            this.btnRunSoundModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRunSoundModel.Name = "btnRunSoundModel";
            this.btnRunSoundModel.Size = new System.Drawing.Size(122, 53);
            this.btnRunSoundModel.TabIndex = 166;
            this.btnRunSoundModel.Text = "Run Sound Model";
            this.btnRunSoundModel.UseVisualStyleBackColor = true;
            this.btnRunSoundModel.Click += new System.EventHandler(this.btnRunSoundModel_Click);
            // 
            // btnRunShadowFlicker
            // 
            this.btnRunShadowFlicker.Location = new System.Drawing.Point(548, 10);
            this.btnRunShadowFlicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRunShadowFlicker.Name = "btnRunShadowFlicker";
            this.btnRunShadowFlicker.Size = new System.Drawing.Size(122, 53);
            this.btnRunShadowFlicker.TabIndex = 165;
            this.btnRunShadowFlicker.Text = "Run Shadow Flicker Model";
            this.btnRunShadowFlicker.UseVisualStyleBackColor = true;
            this.btnRunShadowFlicker.Click += new System.EventHandler(this.btnRunShadowFlicker_Click);
            // 
            // btnRunIceThrow
            // 
            this.btnRunIceThrow.Location = new System.Drawing.Point(393, 10);
            this.btnRunIceThrow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRunIceThrow.Name = "btnRunIceThrow";
            this.btnRunIceThrow.Size = new System.Drawing.Size(122, 55);
            this.btnRunIceThrow.TabIndex = 164;
            this.btnRunIceThrow.Text = "Run Ice Throw Model";
            this.btnRunIceThrow.UseVisualStyleBackColor = true;
            this.btnRunIceThrow.Click += new System.EventHandler(this.btnRunIceThrow_Click);
            // 
            // label167
            // 
            this.label167.AutoSize = true;
            this.label167.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label167.Location = new System.Drawing.Point(19, 18);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(246, 25);
            this.label167.TabIndex = 163;
            this.label167.Text = "Site Suitability Analyses";
            // 
            // label166
            // 
            this.label166.AutoSize = true;
            this.label166.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label166.Location = new System.Drawing.Point(10, 135);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(93, 18);
            this.label166.TabIndex = 162;
            this.label166.Text = "Power Curve :";
            // 
            // cboSiteSuitPowerCurve
            // 
            this.cboSiteSuitPowerCurve.FormattingEnabled = true;
            this.cboSiteSuitPowerCurve.Location = new System.Drawing.Point(104, 132);
            this.cboSiteSuitPowerCurve.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSiteSuitPowerCurve.Name = "cboSiteSuitPowerCurve";
            this.cboSiteSuitPowerCurve.Size = new System.Drawing.Size(294, 26);
            this.cboSiteSuitPowerCurve.TabIndex = 161;
            // 
            // btnImportZones
            // 
            this.btnImportZones.Location = new System.Drawing.Point(38, 711);
            this.btnImportZones.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportZones.Name = "btnImportZones";
            this.btnImportZones.Size = new System.Drawing.Size(122, 53);
            this.btnImportZones.TabIndex = 0;
            this.btnImportZones.Text = "Import Zone Locations";
            this.btnImportZones.UseVisualStyleBackColor = true;
            this.btnImportZones.Click += new System.EventHandler(this.btnImportZones_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.aboutContinuumToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1671, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHelpToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(46, 21);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // showHelpToolStripMenuItem
            // 
            this.showHelpToolStripMenuItem.Name = "showHelpToolStripMenuItem";
            this.showHelpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.showHelpToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.showHelpToolStripMenuItem.Text = "Show Help";
            this.showHelpToolStripMenuItem.Click += new System.EventHandler(this.showHelpToolStripMenuItem_Click);
            // 
            // aboutContinuumToolStripMenuItem
            // 
            this.aboutContinuumToolStripMenuItem.Name = "aboutContinuumToolStripMenuItem";
            this.aboutContinuumToolStripMenuItem.Size = new System.Drawing.Size(119, 21);
            this.aboutContinuumToolStripMenuItem.Text = "About Continuum";
            this.aboutContinuumToolStripMenuItem.Click += new System.EventHandler(this.AboutContinuumToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateHeadersToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(50, 21);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // generateHeadersToolStripMenuItem
            // 
            this.generateHeadersToolStripMenuItem.Name = "generateHeadersToolStripMenuItem";
            this.generateHeadersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.generateHeadersToolStripMenuItem.Text = "Generate Headers";
            this.generateHeadersToolStripMenuItem.Click += new System.EventHandler(this.generateHeadersToolStripMenuItem_Click);
            // 
            // ofdXYZfile
            // 
            this.ofdXYZfile.Filter = "GeoTIFF file|*.TIF|ADF file|*.ADF";
            this.ofdXYZfile.InitialDirectory = "C:\\";
            this.ofdXYZfile.Title = "Load GeoTIFF or ArcGrid file";
            // 
            // ofdPowerCurve
            // 
            this.ofdPowerCurve.Filter = "CSV files|*csv";
            // 
            // sfdrsf
            // 
            this.sfdrsf.DefaultExt = "rsf";
            this.sfdrsf.Filter = "RSF |*rsf";
            // 
            // sfdWRG
            // 
            this.sfdWRG.DefaultExt = "wrg";
            this.sfdWRG.Filter = "WRG |*wrg";
            // 
            // ofdCFMfile
            // 
            this.ofdCFMfile.Filter = "CFM file|*.CFM";
            this.ofdCFMfile.InitialDirectory = "C:\\";
            this.ofdCFMfile.Title = "Open .CFM file";
            // 
            // ofdLandCover
            // 
            this.ofdLandCover.Filter = "TIFF file|*.TIF";
            this.ofdLandCover.InitialDirectory = "C:\\";
            this.ofdLandCover.Title = "Load GeoTIFF file";
            // 
            // sfdCFMfile
            // 
            this.sfdCFMfile.DefaultExt = "cfm";
            this.sfdCFMfile.Filter = "CFM file|*.CFM";
            this.sfdCFMfile.Title = "Save .CFM file";
            // 
            // ofdLC_Key
            // 
            this.ofdLC_Key.Filter = "CSV file|*.CSV|TXT file|*.TXT";
            this.ofdLC_Key.InitialDirectory = "C:\\";
            this.ofdLC_Key.Title = "Import Land Cover key";
            // 
            // ofdMets
            // 
            this.ofdMets.Filter = "TAB file|*.TAB|CSV file|*.CSV|TXT file|*.TXT";
            this.ofdMets.InitialDirectory = "C:\\";
            this.ofdMets.Multiselect = true;
            this.ofdMets.Title = "Import Met Data from a file";
            // 
            // ofdTurbines
            // 
            this.ofdTurbines.Filter = "CSV file|*.CSV|TXT file|*.TXT";
            this.ofdTurbines.InitialDirectory = "C:\\";
            this.ofdTurbines.Title = "Import Turbine Coords from a file";
            // 
            // ofdImportMap
            // 
            this.ofdImportMap.Filter = "MAP file|*.MAP";
            this.ofdImportMap.InitialDirectory = "C:\\";
            this.ofdImportMap.Title = "Load Surface Roughness file";
            // 
            // ofdImportCoeffs
            // 
            this.ofdImportCoeffs.Filter = "CSV file|*.CSV";
            this.ofdImportCoeffs.InitialDirectory = "C:\\";
            this.ofdImportCoeffs.Title = "Import Continuum Coefficients from a file";
            // 
            // sfd60mWS
            // 
            this.sfd60mWS.DefaultExt = "csv";
            this.sfd60mWS.Filter = "CSV files|*csv";
            // 
            // sfdExpos
            // 
            this.sfdExpos.DefaultExt = "csv";
            this.sfdExpos.Filter = "CSV files|*csv";
            // 
            // ofdMetData
            // 
            this.ofdMetData.Filter = "CSV file|*.CSV|TXT file|*.TXT";
            this.ofdMetData.InitialDirectory = "C:\\";
            this.ofdMetData.Title = "Import Met Data from a file";
            // 
            // sfdEstimateWS
            // 
            this.sfdEstimateWS.DefaultExt = "csv";
            this.sfdEstimateWS.Filter = "CSV files|*csv";
            // 
            // sfdSaveTAB
            // 
            this.sfdSaveTAB.Filter = "TAB files|*.TAB";
            // 
            // ofdZones
            // 
            this.ofdZones.Filter = "CSV file|*.CSV|TXT file|*.TXT";
            this.ofdZones.InitialDirectory = "C:\\";
            this.ofdZones.Title = "Import Turbine Coords from a file";
            // 
            // ofdExceedCurves
            // 
            this.ofdExceedCurves.Filter = "CSV file|*.CSV";
            this.ofdExceedCurves.InitialDirectory = "C:\\";
            this.ofdExceedCurves.Title = "Import Turbine Coords from a file";
            // 
            // btnResetMaxRecovDates
            // 
            this.btnResetMaxRecovDates.Location = new System.Drawing.Point(744, 36);
            this.btnResetMaxRecovDates.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnResetMaxRecovDates.Name = "btnResetMaxRecovDates";
            this.btnResetMaxRecovDates.Size = new System.Drawing.Size(65, 47);
            this.btnResetMaxRecovDates.TabIndex = 237;
            this.btnResetMaxRecovDates.Text = "Reset Dates";
            this.btnResetMaxRecovDates.UseVisualStyleBackColor = true;
            this.btnResetMaxRecovDates.Click += new System.EventHandler(this.btnResetMaxRecovDates_Click);
            // 
            // Continuum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1671, 910);
            this.Controls.Add(this.tabContinuum);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Continuum";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Continuum Wind Flow Model";
            this.tabContinuum.ResumeLayout(false);
            this.pgeInput.ResumeLayout(false);
            this.pgeInput.PerformLayout();
            this.pgeMetData.ResumeLayout(false);
            this.pgeMetData.PerformLayout();
            this.pgeMERRA.ResumeLayout(false);
            this.pgeMERRA.PerformLayout();
            this.pgeMCP.ResumeLayout(false);
            this.pgeMCP.PerformLayout();
            this.pgeMetSumm.ResumeLayout(false);
            this.pgeMetSumm.PerformLayout();
            this.pgeGrossTurbs.ResumeLayout(false);
            this.pgeGrossTurbs.PerformLayout();
            this.pgeExceedance.ResumeLayout(false);
            this.pgeExceedance.PerformLayout();
            this.pgeNetEsts.ResumeLayout(false);
            this.pgeNetEsts.PerformLayout();
            this.pgeSiteConditions.ResumeLayout(false);
            this.pgeSiteConditions.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pgeMonthlyAnalysis.ResumeLayout(false);
            this.pgeMonthlyAnalysis.PerformLayout();
            this.pgeMaps.ResumeLayout(false);
            this.pgeMaps.PerformLayout();
            this.pgeRound.ResumeLayout(false);
            this.pgeRound.PerformLayout();
            this.pgeStepwise.ResumeLayout(false);
            this.pgeStepwise.PerformLayout();
            this.pgeSuitability.ResumeLayout(false);
            this.pgeSuitability.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.TabPage pgeInput;        
        internal System.Windows.Forms.Button btnImportRoughness;
        internal System.Windows.Forms.Label Label84;
        internal System.Windows.Forms.TextBox txt_LC_Key_selected;
        internal System.Windows.Forms.Button btnViewModNLCD;
        internal System.Windows.Forms.ComboBox cboTopo_Or_Roughness;
        internal System.Windows.Forms.TextBox txtUTMZone;
        internal System.Windows.Forms.Label Label57;
        internal System.Windows.Forms.TextBox txtUTMDatum;
        internal System.Windows.Forms.Label Label49;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Button btnImportTAB;
        internal System.Windows.Forms.Button btnGenTurbEsts;
        internal System.Windows.Forms.Label Label30;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.CheckBox chkAllTurbLabels;
        internal System.Windows.Forms.CheckBox chkAllMetLabels;
        internal System.Windows.Forms.Label Label23;
        internal System.Windows.Forms.Label lblMetLabels;
        internal System.Windows.Forms.CheckedListBox chkTurbLabels;
        internal System.Windows.Forms.CheckedListBox chkMetLabels;
        internal System.Windows.Forms.TextBox txtMainMax;
        internal System.Windows.Forms.TextBox txtMainMin;
        internal System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.Label Label22;
        internal System.Windows.Forms.Button btnDelTurb;
        internal System.Windows.Forms.Button btnEditTurb;
        internal System.Windows.Forms.Button btnAddTurb;
        internal System.Windows.Forms.ColumnHeader ColumnHeader17;
        internal System.Windows.Forms.ColumnHeader ColumnHeader18;
        internal System.Windows.Forms.ColumnHeader ColumnHeader19;
        internal System.Windows.Forms.TextBox txtTopoSource;
        internal System.Windows.Forms.Label lblTurbineList;
        internal System.Windows.Forms.Button btnDelMet;
        public System.Windows.Forms.ListView lstMetTowers;
        internal System.Windows.Forms.ColumnHeader metName;
        internal System.Windows.Forms.ColumnHeader Lats;
        internal System.Windows.Forms.ColumnHeader Longs;
        internal System.Windows.Forms.ColumnHeader WindSpd1;
        internal System.Windows.Forms.Label lblMetTable;
        internal System.Windows.Forms.Button btnTurbines;
        internal System.Windows.Forms.TabPage pgeMetSumm;        
        internal System.Windows.Forms.Label Label80;
        internal System.Windows.Forms.ListView lstTurbStats;
        internal System.Windows.Forms.ColumnHeader ColumnHeader21;
        internal System.Windows.Forms.ColumnHeader ColumnHeader34;
        internal System.Windows.Forms.ColumnHeader ColumnHeader35;
        internal System.Windows.Forms.ColumnHeader ColumnHeader22;
        internal System.Windows.Forms.ColumnHeader ColumnHeader33;
        internal System.Windows.Forms.CheckBox chkTurbSummAll;
        internal System.Windows.Forms.CheckedListBox chkTurbSumm;
        internal System.Windows.Forms.Label Label78;
        internal System.Windows.Forms.CheckBox chkMetSummAll;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.CheckedListBox chkMetSumm;
        internal System.Windows.Forms.Label Label76;
        internal System.Windows.Forms.ListView lstMetStats;
        internal System.Windows.Forms.ColumnHeader ColumnHeader42;
        internal System.Windows.Forms.ColumnHeader ColumnHeader91;
        internal System.Windows.Forms.ColumnHeader ColumnHeader93;
        internal System.Windows.Forms.ColumnHeader ColumnHeader92;
        internal System.Windows.Forms.ColumnHeader ColumnHeader52;
        internal System.Windows.Forms.Button btnExportExpoSRDH;
        internal System.Windows.Forms.Label Label74;
        internal System.Windows.Forms.ComboBox cboSummaryWD;
        internal System.Windows.Forms.Label Label75;
        internal System.Windows.Forms.ComboBox cboMetSum_Rad;
        internal System.Windows.Forms.ListView lstMetSummary;
        internal System.Windows.Forms.ColumnHeader ColumnHeader37;
        internal System.Windows.Forms.ColumnHeader ColumnHeader40;
        internal System.Windows.Forms.ColumnHeader ColumnHeader41;
        internal System.Windows.Forms.ColumnHeader ColumnHeader43;
        internal System.Windows.Forms.ColumnHeader ColumnHeader44;
        internal System.Windows.Forms.ColumnHeader ColumnHeader46;
        internal System.Windows.Forms.ColumnHeader ColumnHeader47;
        internal System.Windows.Forms.ColumnHeader ColumnHeader48;
        internal System.Windows.Forms.ColumnHeader ColumnHeader49;
        internal System.Windows.Forms.ColumnHeader ColumnHeader50;
        internal System.Windows.Forms.ColumnHeader ColumnHeader51;
        internal System.Windows.Forms.Label Label73;
        internal System.Windows.Forms.TabPage pgeGrossTurbs;
        internal System.Windows.Forms.Label Label40;
        internal System.Windows.Forms.ComboBox cboWS_or_WD;
        internal System.Windows.Forms.TextBox txtGross_FlowSepUsed;
        internal System.Windows.Forms.Label Label96;
        internal System.Windows.Forms.ListView lstGrossHisto;
        internal System.Windows.Forms.ColumnHeader ColumnHeader103;
        internal System.Windows.Forms.ColumnHeader ColumnHeader104;
        internal System.Windows.Forms.ColumnHeader ColumnHeader105;
        internal System.Windows.Forms.TextBox txtGross_LC_used;
        internal System.Windows.Forms.Label Label83;
        internal System.Windows.Forms.Label Label79;
        internal System.Windows.Forms.CheckBox chkMetGrossAll;
        internal System.Windows.Forms.Label Label50;
        internal System.Windows.Forms.CheckedListBox chkMetGross;
        internal System.Windows.Forms.Label Label77;
        internal System.Windows.Forms.ComboBox cboGrossParam;
        internal System.Windows.Forms.Button btnExportDirWSDists;
        internal System.Windows.Forms.Label Label71;
        internal System.Windows.Forms.ComboBox cboGrossWD;
        internal System.Windows.Forms.Button btnExportDirWS;
        internal System.Windows.Forms.Button btnExportCRV;
        internal System.Windows.Forms.Button btnExportWSDists;
        internal System.Windows.Forms.Button btnWS_AEP_Exprt;
        internal System.Windows.Forms.TextBox txtAEPMax;
        internal System.Windows.Forms.TextBox txtAEPMin;
        internal System.Windows.Forms.TextBox txtAEPSD;
        internal System.Windows.Forms.TextBox txtAEPAvg;
        internal System.Windows.Forms.Label Label35;
        internal System.Windows.Forms.Label Label34;
        internal System.Windows.Forms.CheckBox chkTurbGrossAll;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.CheckedListBox chkTurbGross;
        internal System.Windows.Forms.Button btnDelPowerCrv;
        internal System.Windows.Forms.Button btnImportCRV;
        internal System.Windows.Forms.TextBox txtMax;
        internal System.Windows.Forms.TextBox txtMin;
        internal System.Windows.Forms.TextBox txtCount;
        internal System.Windows.Forms.TextBox txtStDev;
        internal System.Windows.Forms.TextBox txtAvg;
        internal System.Windows.Forms.Label lblMax;
        internal System.Windows.Forms.Label lblMin;
        internal System.Windows.Forms.Label lblCount;
        internal System.Windows.Forms.Label lblStdev;
        internal System.Windows.Forms.Label lblAvg;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label lblTurbEsts;
        internal System.Windows.Forms.ComboBox cboPowerCrvs;
        internal System.Windows.Forms.ListView lstGrossTurbEst;
        internal System.Windows.Forms.ColumnHeader ColumnHeader20;
        internal System.Windows.Forms.ColumnHeader ColumnHeader23;
        internal System.Windows.Forms.ColumnHeader ColumnHeader24;
        internal System.Windows.Forms.ColumnHeader Col_Header25;
        internal System.Windows.Forms.ColumnHeader ColumnHeader13;
        internal System.Windows.Forms.ColumnHeader ColumnHeader29;
        internal System.Windows.Forms.ColumnHeader ColumnHeader32;
        internal System.Windows.Forms.TabPage pgeNetEsts;
        internal System.Windows.Forms.TextBox txtNet_FlowSep_Used;
        internal System.Windows.Forms.Button btnExportNetDirWSDists;
        internal System.Windows.Forms.Button btnExportNetDirWS;
        internal System.Windows.Forms.Button btnExportNetWSDists;
        internal System.Windows.Forms.Button btnRefreshWakeMap;
        internal System.Windows.Forms.CheckBox chkWakeAuto;
        internal System.Windows.Forms.TextBox txtWakeInterval;
        internal System.Windows.Forms.TextBox txtWakeMax;
        internal System.Windows.Forms.TextBox txtWakeMin;
        internal System.Windows.Forms.Label Label97;
        internal System.Windows.Forms.Label Label98;
        internal System.Windows.Forms.Label Label99;
        internal System.Windows.Forms.TextBox txtLC_Net;
        internal System.Windows.Forms.Button btnCreateWakeMap;
        internal System.Windows.Forms.TextBox txtWakeLoss;
        internal System.Windows.Forms.Label Label94;
        internal System.Windows.Forms.CheckedListBox chkTurbNet;
        internal System.Windows.Forms.CheckBox chkTurbNetAll;
        internal System.Windows.Forms.Label Label87;
        internal System.Windows.Forms.CheckedListBox chkStrings;
        internal System.Windows.Forms.CheckBox chkStringAll;
        internal System.Windows.Forms.Label Label86;
        internal System.Windows.Forms.Button btnDelWakeModel;
        internal System.Windows.Forms.Button btnDelWakeGrid;
        internal System.Windows.Forms.ComboBox cboWakePlot;
        internal System.Windows.Forms.Label Label85;
        internal System.Windows.Forms.Button btnExportNetEsts;
        internal System.Windows.Forms.Label Label55;
        internal System.Windows.Forms.ComboBox cboNetWD;
        internal System.Windows.Forms.ListView lstWakeModels;
        internal System.Windows.Forms.ColumnHeader ColumnHeader88;
        internal System.Windows.Forms.ColumnHeader ColumnHeader106;
        internal System.Windows.Forms.ColumnHeader ColumnHeader89;
        internal System.Windows.Forms.ColumnHeader ColumnHeader107;
        internal System.Windows.Forms.ColumnHeader ColumnHeader90;
        internal System.Windows.Forms.ColumnHeader ColumnHeader94;
        internal System.Windows.Forms.ColumnHeader ColumnHeader95;
        internal System.Windows.Forms.ColumnHeader ColumnHeader96;
        internal System.Windows.Forms.ColumnHeader ColumnHeader97;
        internal System.Windows.Forms.ColumnHeader ColumnHeader98;
        internal System.Windows.Forms.ColumnHeader ColumnHeader99;
        internal System.Windows.Forms.ColumnHeader ColumnHeader100;
        internal System.Windows.Forms.ColumnHeader ColumnHeader101;
        internal System.Windows.Forms.ColumnHeader ColumnHeader102;
        internal System.Windows.Forms.Button btnWakeLossCalc;
        internal System.Windows.Forms.Label Label53;
        internal System.Windows.Forms.ListView lstWakedTurbs;
        internal System.Windows.Forms.ColumnHeader SiteName;
        internal System.Windows.Forms.ColumnHeader StringNum;
        internal System.Windows.Forms.ColumnHeader ColumnHeader53;
        internal System.Windows.Forms.ColumnHeader ColumnHeader60;
        internal System.Windows.Forms.ColumnHeader ColumnHeader61;
        internal System.Windows.Forms.ColumnHeader ColumnHeader113;
        internal System.Windows.Forms.ColumnHeader ColumnHeader62;
        internal System.Windows.Forms.ColumnHeader ColumnHeader63;
        internal System.Windows.Forms.ColumnHeader ColumnHeader77;
        internal System.Windows.Forms.Label Label36;
        internal System.Windows.Forms.TabPage pgeMaps;
        internal System.Windows.Forms.Label Label72;
        internal System.Windows.Forms.ComboBox cboMapWD;
        internal System.Windows.Forms.TextBox txtMap_MetsUsed;
        internal System.Windows.Forms.Label Label63;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label27;
        internal System.Windows.Forms.CheckBox chkAllTurbs_Maps;
        internal System.Windows.Forms.CheckBox chkAllMets_Maps;
        internal System.Windows.Forms.Label Label28;
        internal System.Windows.Forms.Label Label29;
        internal System.Windows.Forms.CheckedListBox chkTurbLabels_Maps;
        internal System.Windows.Forms.CheckedListBox chkMetLabels_Maps;
        internal System.Windows.Forms.Button btnRefreshMap;
        internal System.Windows.Forms.CheckBox chkAutoMinMax;
        internal System.Windows.Forms.TextBox txtIntLevel;
        internal System.Windows.Forms.TextBox txtMaxValue;
        internal System.Windows.Forms.TextBox txtMinValue;
        internal System.Windows.Forms.TextBox txtMapMax;
        internal System.Windows.Forms.TextBox txtMapMin;
        internal System.Windows.Forms.TextBox txtMapCount;
        internal System.Windows.Forms.TextBox txtMapStDev;
        internal System.Windows.Forms.TextBox txtMapAvg;
        internal System.Windows.Forms.Label lblInterval;
        internal System.Windows.Forms.Label lblMaxVal;
        internal System.Windows.Forms.Label lblMinVal;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button btnDelMaps;
        internal System.Windows.Forms.Button btnMapExportCSV;
        internal System.Windows.Forms.Button btnExportWRG;
        internal System.Windows.Forms.Button btnGenMap;
        internal System.Windows.Forms.ListView lstMaps;
        internal System.Windows.Forms.ColumnHeader ColumnHeader54;
        internal System.Windows.Forms.ColumnHeader ColumnHeader55;
        internal System.Windows.Forms.ColumnHeader ColumnHeader56;
        internal System.Windows.Forms.ColumnHeader ColumnHeader57;
        internal System.Windows.Forms.ColumnHeader ColumnHeader58;
        internal System.Windows.Forms.ColumnHeader ColumnHeader59;
        internal System.Windows.Forms.ColumnHeader ColumnHeader36;
        internal System.Windows.Forms.ColumnHeader ColumnHeader12;
        internal System.Windows.Forms.TabPage pgeRound;        
        internal System.Windows.Forms.TextBox txtRR_FlowSep_Used;
        internal System.Windows.Forms.ComboBox cboRR_MinSize;
        internal System.Windows.Forms.Label Label31;
        internal System.Windows.Forms.TextBox txtRR_LC_used;
        internal System.Windows.Forms.Label Label81;
        internal System.Windows.Forms.Button btnExportTurbUncert;
        internal System.Windows.Forms.CheckBox chkP50;
        internal System.Windows.Forms.CheckBox chkP90;
        internal System.Windows.Forms.CheckBox chkP99;
        internal System.Windows.Forms.ComboBox cboUncert_WS_AEP;
        internal System.Windows.Forms.Label Label47;
        internal System.Windows.Forms.Label Label45;
        internal System.Windows.Forms.ComboBox cboUncertPowerCrv;
        internal System.Windows.Forms.Label Label42;
        internal System.Windows.Forms.ListView lstTurbUncert;
        internal System.Windows.Forms.ColumnHeader ColumnHeader28;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        internal System.Windows.Forms.ColumnHeader ColumnHeader3;
        internal System.Windows.Forms.ColumnHeader ColumnHeader11;
        internal System.Windows.Forms.ColumnHeader ColumnHeader25;
        internal System.Windows.Forms.ColumnHeader ColumnHeader7;
        internal System.Windows.Forms.ColumnHeader ColumnHeader10;
        internal System.Windows.Forms.ColumnHeader ColumnHeader27;
        internal System.Windows.Forms.Label Label41;
        internal System.Windows.Forms.Button btnExportRR;
        internal System.Windows.Forms.ListView lstRR_AllErr;
        internal System.Windows.Forms.ColumnHeader ColumnHeader4;
        internal System.Windows.Forms.ColumnHeader ColumnHeader5;
        internal System.Windows.Forms.ColumnHeader ColumnHeader8;
        internal System.Windows.Forms.ColumnHeader ColumnHeader6;
        internal System.Windows.Forms.ColumnHeader ColumnHeader9;
        internal System.Windows.Forms.ComboBox cboRoundRobin;
        internal System.Windows.Forms.ListView lstRR_Results;
        internal System.Windows.Forms.ColumnHeader ColumnHeader38;
        internal System.Windows.Forms.ColumnHeader ColumnHeader39;
        internal System.Windows.Forms.ColumnHeader ColumnHeader45;
        internal System.Windows.Forms.Button btnDoRRCalcs;
        internal System.Windows.Forms.Label Label46;
        internal System.Windows.Forms.TabPage pgeStepwise;
        internal System.Windows.Forms.Label Label39;
        internal System.Windows.Forms.ListView lstPathNodes_DW;
        internal System.Windows.Forms.ColumnHeader ColumnHeader15;
        internal System.Windows.Forms.ColumnHeader ColumnHeader110;
        internal System.Windows.Forms.ColumnHeader ColumnHeader108;
        internal System.Windows.Forms.ColumnHeader ColumnHeader109;
        internal System.Windows.Forms.ColumnHeader ColumnHeader112;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.ListView lstPathNodes_UW;
        internal System.Windows.Forms.ColumnHeader ColumnHeader14;
        internal System.Windows.Forms.ColumnHeader ColumnHeader121;
        internal System.Windows.Forms.ColumnHeader ColumnHeader115;
        internal System.Windows.Forms.ColumnHeader ColumnHeader120;
        internal System.Windows.Forms.ColumnHeader ColumnHeader111;
        internal System.Windows.Forms.TextBox txtAdv_FlowSep_Used;
        internal System.Windows.Forms.Label Label101;
        internal System.Windows.Forms.TextBox txtSepCritWS;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox txtSepCrit;
        internal System.Windows.Forms.Label Label100;
        internal System.Windows.Forms.ComboBox cboDHplot;
        internal System.Windows.Forms.Button btnImportModel;
        internal System.Windows.Forms.CheckedListBox chkAdvToShow;
        internal System.Windows.Forms.CheckBox chkWeight_RMS;
        internal System.Windows.Forms.TextBox txtSectRMS;
        internal System.Windows.Forms.Label Label48;
        internal System.Windows.Forms.TextBox txtAdv_LC_used;
        internal System.Windows.Forms.ComboBox cboExpo_or_Stab;
        internal System.Windows.Forms.Label Label82;
        internal System.Windows.Forms.Button btnExportModel;
        internal System.Windows.Forms.Label Label59;
        internal System.Windows.Forms.TextBox txtUWCrit;
        internal System.Windows.Forms.Label Label58;
        internal System.Windows.Forms.ComboBox cboUphill_to_show;
        internal System.Windows.Forms.ComboBox cboAdvancedWD;
        internal System.Windows.Forms.Label Label56;
        internal System.Windows.Forms.Button btnExportCrossPreds;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.ComboBox cboAdvancedRad;
        internal System.Windows.Forms.TextBox txtUWDWRMS;
        internal System.Windows.Forms.Label Label44;
        internal System.Windows.Forms.CheckBox chkAllTurbsStep;
        internal System.Windows.Forms.Label Label38;
        internal System.Windows.Forms.CheckedListBox chkTurbLabelStep;
        internal System.Windows.Forms.CheckBox chkAllMetLabelsStep;
        internal System.Windows.Forms.Label Label37;
        internal System.Windows.Forms.CheckedListBox chkMetlabelsStep;
        internal System.Windows.Forms.Label Label33;
        internal System.Windows.Forms.ComboBox cboStartMet;
        internal System.Windows.Forms.Label Label32;
        internal System.Windows.Forms.ComboBox cboEndMet;
        internal System.Windows.Forms.ListView lstPathNodes;
        internal System.Windows.Forms.ColumnHeader Site;
        internal System.Windows.Forms.ColumnHeader UTMX;
        internal System.Windows.Forms.ColumnHeader UTMY;
        internal System.Windows.Forms.ColumnHeader elev;
        internal System.Windows.Forms.ColumnHeader P10UW;
        internal System.Windows.Forms.ColumnHeader P10DW;
        internal System.Windows.Forms.ColumnHeader UW;
        internal System.Windows.Forms.ColumnHeader DW;
        internal System.Windows.Forms.ColumnHeader WSEst;
        internal System.Windows.Forms.ColumnHeader UW_SR;
        internal System.Windows.Forms.ColumnHeader DW_SR;
        internal System.Windows.Forms.ColumnHeader UW_DH;
        internal System.Windows.Forms.ColumnHeader DW_DH;
        internal System.Windows.Forms.Button btnExportStepwise;
        internal System.Windows.Forms.ListView lstModCrossPred;
        internal System.Windows.Forms.ColumnHeader ColumnHeader16;
        internal System.Windows.Forms.ColumnHeader ColumnHeader26;
        internal System.Windows.Forms.ColumnHeader ColumnHeader30;
        internal System.Windows.Forms.ColumnHeader ColumnHeader31;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label7;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        internal System.Windows.Forms.SaveFileDialog sfdrsf;
        internal System.Windows.Forms.SaveFileDialog sfdWRG;
        internal System.Windows.Forms.OpenFileDialog ofdCFMfile;
        internal System.Windows.Forms.OpenFileDialog ofdLandCover;
        internal System.Windows.Forms.OpenFileDialog ofdLC_Key;
        internal System.Windows.Forms.OpenFileDialog ofdTurbines;
        internal System.Windows.Forms.OpenFileDialog ofdImportMap;
        internal System.Windows.Forms.OpenFileDialog ofdImportCoeffs;
        internal System.Windows.Forms.SaveFileDialog sfd60mWS;
        internal System.Windows.Forms.SaveFileDialog sfdExpos;
        internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutContinuumToolStripMenuItem;
        public System.Windows.Forms.Button btnLoadXYZ;
        public System.Windows.Forms.Button btnAnalyzeMets;
        public System.Windows.Forms.OpenFileDialog ofdMets;
        public System.Windows.Forms.OpenFileDialog ofdXYZfile;
        internal System.Windows.Forms.Button btnImportMetTS;
        private System.Windows.Forms.TabPage pgeMetData;
        internal System.Windows.Forms.ComboBox cboFilt_or_Not;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.ColumnHeader columnHeader82;
        private System.Windows.Forms.ColumnHeader columnHeader84;
        private System.Windows.Forms.ColumnHeader columnHeader83;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.ColumnHeader columnHeader80;
        private System.Windows.Forms.ColumnHeader columnHeader81;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.ColumnHeader columnHeader75;
        private System.Windows.Forms.ColumnHeader columnHeader76;
        private System.Windows.Forms.ColumnHeader columnHeader78;
        private System.Windows.Forms.ColumnHeader columnHeader79;
        private System.Windows.Forms.Label label65;
        internal System.Windows.Forms.ComboBox cboMetWindRose;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.ColumnHeader columnHeader74;
        private System.Windows.Forms.ColumnHeader columnHeader86;
        private System.Windows.Forms.ColumnHeader columnHeader87;
        private System.Windows.Forms.ColumnHeader columnHeader114;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader height;
        private System.Windows.Forms.ColumnHeader columnHeader64;
        private System.Windows.Forms.ColumnHeader columnHeader65;
        private System.Windows.Forms.ColumnHeader columnHeader66;
        private System.Windows.Forms.ColumnHeader columnHeader67;
        private System.Windows.Forms.ColumnHeader columnHeader68;
        private System.Windows.Forms.ColumnHeader columnHeader69;
        private System.Windows.Forms.ColumnHeader columnHeader70;
        private System.Windows.Forms.ColumnHeader columnHeader71;
        private System.Windows.Forms.ColumnHeader columnHeader72;
        private System.Windows.Forms.ColumnHeader columnHeader73;
        public System.Windows.Forms.OpenFileDialog ofdMetData;
        public System.Windows.Forms.ListView lstAlphas;
        public System.Windows.Forms.ListView lstExtrapolated;
        public System.Windows.Forms.ListView lstTempSummary;
        public System.Windows.Forms.ListView lstVaneSummary;
        public System.Windows.Forms.ListView lstAnemSummary;
        public System.Windows.Forms.DateTimePicker End_Time;
        public System.Windows.Forms.DateTimePicker All_End_Time;
        public System.Windows.Forms.DateTimePicker Start_Time;
        public System.Windows.Forms.DateTimePicker All_Start_Time;
        public System.Windows.Forms.DateTimePicker Export_End;
        private System.Windows.Forms.Label label105;
        public System.Windows.Forms.DateTimePicker Export_Start;
        private System.Windows.Forms.Button btnExportAnnualMax;
        private System.Windows.Forms.Button btnExportExtrap;
        private System.Windows.Forms.Button btnExportAlpha;
        private System.Windows.Forms.Button btnExportFlags;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Button btnViewFilters;
        public System.Windows.Forms.ComboBox cboMetQC_SelectedMet;
        internal System.Windows.Forms.ComboBox cboSeasonAdvanced;
        internal System.Windows.Forms.ComboBox cboTODAdvanced;
        internal System.Windows.Forms.ComboBox cboSummSeason;
        internal System.Windows.Forms.ComboBox cboSummTOD;
        public System.Windows.Forms.SaveFileDialog sfdEstimateWS;
        private System.Windows.Forms.TabPage pgeMCP;
        public System.Windows.Forms.ComboBox cboMCP_Met;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label108;
        public System.Windows.Forms.ComboBox cboMCP_Season;
        private System.Windows.Forms.Label label109;
        public System.Windows.Forms.ComboBox cboMCP_TOD;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label114;
        public System.Windows.Forms.ComboBox cboMCP_WD;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label label118;
        public System.Windows.Forms.Button btnMCP_Uncert;
        public System.Windows.Forms.ComboBox cboUncertStep;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.ColumnHeader Window;
        private System.Windows.Forms.ColumnHeader AVG;
        private System.Windows.Forms.ColumnHeader SDU;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.ColumnHeader WS;
        private System.Windows.Forms.ColumnHeader Mean;
        private System.Windows.Forms.ColumnHeader SD;
        private System.Windows.Forms.ColumnHeader Count;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Label label140;
        public System.Windows.Forms.TextBox txtLast_WS_Wgt;
        private System.Windows.Forms.Label label134;
        public System.Windows.Forms.TextBox txtWS_PDF_Wgt;
        private System.Windows.Forms.Label label135;
        public System.Windows.Forms.ComboBox cboMCPNumSeasons;
        private System.Windows.Forms.Label label136;
        public System.Windows.Forms.ComboBox cboMCPNumHours;
        private System.Windows.Forms.Label label137;
        public System.Windows.Forms.TextBox txtWS_bin_width;
        private System.Windows.Forms.Label label138;
        public System.Windows.Forms.ComboBox cboMCPNumWD;
        private System.Windows.Forms.Label label139;
        public System.Windows.Forms.ComboBox cboMCP_Type;
        private System.Windows.Forms.Label label133;
        public System.Windows.Forms.TextBox txtTAB_WS_bin;
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.ComboBox cboTAB_bins;
        public System.Windows.Forms.TextBox txtNumYrsConc;
        public System.Windows.Forms.TextBox txtRsq;
        public System.Windows.Forms.TextBox txtIntercept;
        public System.Windows.Forms.TextBox txtSlope;
        public System.Windows.Forms.TextBox txtNumYrsTarg;
        public System.Windows.Forms.TextBox txtNumYrsRef;
        public System.Windows.Forms.TextBox txtLTratio;
        public System.Windows.Forms.TextBox txtAvgRatio;
        public System.Windows.Forms.TextBox txtDataCount;
        public System.Windows.Forms.TextBox txtTarg_LT_WS;
        public System.Windows.Forms.TextBox txtRef_LT_WS;
        public System.Windows.Forms.TextBox txtTargAvgWS;
        public System.Windows.Forms.TextBox txtRefAvgWS;
        public System.Windows.Forms.DateTimePicker dateConcurrentEnd;
        private System.Windows.Forms.Label label144;
        public System.Windows.Forms.DateTimePicker dateConcurrentStart;
        public System.Windows.Forms.DateTimePicker dateMCPExportEnd;
        private System.Windows.Forms.Label label145;
        public System.Windows.Forms.DateTimePicker dateMCPExportStart;
        public System.Windows.Forms.Button btnExportBinRatios;
        public System.Windows.Forms.Button btnExportMCP_TAB;
        public System.Windows.Forms.Button btnExportMCP_TS;
        public System.Windows.Forms.Button btnResetDates;
        public System.Windows.Forms.Button btnExportMCPUncert;
        public System.Windows.Forms.ListView lstMCP_Bins;
        public System.Windows.Forms.ListView lstMCP_Uncert;
        public System.Windows.Forms.SaveFileDialog sfdSaveTAB;
        private System.Windows.Forms.TabPage pgeMERRA;        
        private System.Windows.Forms.Button btn_ExportWR;
        private System.Windows.Forms.Button btn_Export_All_Months_All_Years;
        private System.Windows.Forms.Label label159;
        private System.Windows.Forms.Label label156;
        public System.Windows.Forms.ComboBox cboMERRA_PlotParam;
        private System.Windows.Forms.ColumnHeader columnHeader85;
        private System.Windows.Forms.ColumnHeader columnHeader116;
        private System.Windows.Forms.ColumnHeader columnHeader117;
        private System.Windows.Forms.ColumnHeader Month;
        private System.Windows.Forms.ColumnHeader ThisYear;
        private System.Windows.Forms.ColumnHeader Diff;
        private System.Windows.Forms.ColumnHeader Average;
        private System.Windows.Forms.Button btn_Export_Interp;
        public System.Windows.Forms.DateTimePicker dateMERRAEnd;
        public System.Windows.Forms.DateTimePicker dateMERRAStart;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.Label label147;
        public System.Windows.Forms.ComboBox cboMERRASelectedMet;
        public System.Windows.Forms.TextBox txtMERRA_SelectedLong;
        private System.Windows.Forms.Label label149;
        private System.Windows.Forms.Label label150;
        public System.Windows.Forms.TextBox txtMERRA_SelectedLat;
        private System.Windows.Forms.Label label154;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.Label label160;
        public System.Windows.Forms.ComboBox cboMERRA_PowerCurves;
        public System.Windows.Forms.FolderBrowserDialog fbd_MERRAData;
        public System.Windows.Forms.ComboBox cboNumMERRA_Nodes;
        private System.Windows.Forms.Label label161;
        private System.Windows.Forms.Label label162;
        public System.Windows.Forms.TextBox txtMERRA_WS_ScaleFact;        
        public System.Windows.Forms.ListView lstMERRAAnnualProd;
        public System.Windows.Forms.ListView lstMERRA_MonthlyProd;
        public System.Windows.Forms.Button btn_Import_MERRA;
        public System.Windows.Forms.TextBox txt_MERRA2_folder;
        public System.Windows.Forms.FolderBrowserDialog fbd_Export;
        private System.Windows.Forms.TabPage pgeMonthlyAnalysis;
        internal System.Windows.Forms.Label label163;
        private System.Windows.Forms.TabPage pgeSuitability;
        private System.Windows.Forms.Button btnImportZones;
        internal System.Windows.Forms.OpenFileDialog ofdZones;
        internal System.Windows.Forms.Label label166;
        internal System.Windows.Forms.ComboBox cboSiteSuitPowerCurve;
        internal System.Windows.Forms.Label label167;
        public System.Windows.Forms.TextBox txtModeledHeight;
        internal System.Windows.Forms.Label label168;
        public System.Windows.Forms.Label lblRegStats;
        private System.Windows.Forms.Label label121;
        public System.Windows.Forms.ComboBox cboMERRA_Month;
        private System.Windows.Forms.Label label120;
        public System.Windows.Forms.ComboBox cboMERRAYear;
        internal System.Windows.Forms.CheckedListBox chkYearsToDisplay;
        internal System.Windows.Forms.CheckBox chkYearsToDisplayAll;
        internal System.Windows.Forms.Button btnImportCRV_MERRA;
        public System.Windows.Forms.ListView lstYearlyTurbine;
        private System.Windows.Forms.ColumnHeader columnHeader118;
        private System.Windows.Forms.ColumnHeader columnHeader119;
        private System.Windows.Forms.ColumnHeader columnHeader122;
        public System.Windows.Forms.ListView lstMonthlyTurbine;
        private System.Windows.Forms.ColumnHeader columnHeader123;
        private System.Windows.Forms.ColumnHeader columnHeader124;
        private System.Windows.Forms.ColumnHeader columnHeader125;
        private System.Windows.Forms.ColumnHeader columnHeader126;
        internal System.Windows.Forms.CheckBox chkSelectAllTurbineYears;
        internal System.Windows.Forms.CheckedListBox chkYears_Monthly;
        internal System.Windows.Forms.Label label131;
        internal System.Windows.Forms.ComboBox cboSelectedTurbine;
        internal System.Windows.Forms.Label label126;
        internal System.Windows.Forms.Label label122;
        private System.Windows.Forms.ColumnHeader columnHeader127;
        private System.Windows.Forms.ColumnHeader columnHeader128;
        private System.Windows.Forms.ColumnHeader columnHeader129;
        private System.Windows.Forms.ColumnHeader columnHeader130;
        internal System.Windows.Forms.CheckedListBox chkSelectedTurbineParam;
        internal System.Windows.Forms.Label label148;
        internal System.Windows.Forms.ComboBox cboMonthlyWakeModel;
        internal System.Windows.Forms.Label label157;
        private System.Windows.Forms.ColumnHeader columnHeader131;
        private System.Windows.Forms.ColumnHeader columnHeader132;
        internal System.Windows.Forms.Button btnExportMonthlyTurbineValues;
        internal System.Windows.Forms.Button btnExportYearlyTurbineValues;
        internal System.Windows.Forms.Button btnExportHourlyTurbineValues;
        internal System.Windows.Forms.Label label158;
        internal System.Windows.Forms.ComboBox cboMonthlyPowerCurve;
        internal System.Windows.Forms.ColumnHeader columnHeader133;
        internal System.Windows.Forms.ColumnHeader columnHeader134;
        internal System.Windows.Forms.ColumnHeader columnHeader135;
        private System.Windows.Forms.TabPage pgeExceedance;
        internal System.Windows.Forms.Label label164;
        internal System.Windows.Forms.ComboBox cboSiteSuitabilitySelectPlot;
        private System.Windows.Forms.Button btnDelZones;
        private System.Windows.Forms.ColumnHeader columnHeader136;
        private System.Windows.Forms.ColumnHeader columnHeader137;
        internal System.Windows.Forms.Button btnSiteSuitImportCRV;
        public System.Windows.Forms.Button btnRunIceThrow;
        internal System.Windows.Forms.Label label169;
        internal System.Windows.Forms.ComboBox cboSiteSuitHour;
        internal System.Windows.Forms.Label label165;
        internal System.Windows.Forms.ComboBox cboSiteSuitMonth;
        public System.Windows.Forms.Button btnRunShadowFlicker;
        public System.Windows.Forms.ListView lstShadow12x24;
        private System.Windows.Forms.ColumnHeader columnHeader138;
        private System.Windows.Forms.ColumnHeader columnHeader139;
        private System.Windows.Forms.ColumnHeader columnHeader140;
        private System.Windows.Forms.ColumnHeader columnHeader141;
        private System.Windows.Forms.ColumnHeader columnHeader142;
        private System.Windows.Forms.ColumnHeader columnHeader143;
        internal System.Windows.Forms.Label lblShadowOrIceByDist;
        internal System.Windows.Forms.Label lblZoneOrDirection;
        internal System.Windows.Forms.ComboBox cboZoneList;
        private System.Windows.Forms.ColumnHeader columnHeader151;
        private System.Windows.Forms.ColumnHeader columnHeader144;
        private System.Windows.Forms.ColumnHeader columnHeader145;
        private System.Windows.Forms.ColumnHeader columnHeader146;
        private System.Windows.Forms.ColumnHeader columnHeader147;
        private System.Windows.Forms.ColumnHeader columnHeader148;
        private System.Windows.Forms.ColumnHeader columnHeader149;
        private System.Windows.Forms.ColumnHeader columnHeader150;
        public System.Windows.Forms.TextBox txtTotalShadow;
        internal System.Windows.Forms.Label lblTotalHoursPerYear;
        public System.Windows.Forms.ListView lstShadowZoneSummary;
        private System.Windows.Forms.ColumnHeader columnHeader152;
        private System.Windows.Forms.ColumnHeader columnHeader153;
        internal System.Windows.Forms.Label label173;
        internal System.Windows.Forms.ComboBox cboIcingYear;
        public System.Windows.Forms.ListView lstZoneIceHits;
        private System.Windows.Forms.ColumnHeader columnHeader154;
        private System.Windows.Forms.ColumnHeader columnHeader155;
        internal System.Windows.Forms.Label label175;
        internal System.Windows.Forms.Label lblShadowByMonthOrIceByDist;
        private System.Windows.Forms.ColumnHeader columnHeader156;
        private System.Windows.Forms.ColumnHeader columnHeader157;
        internal System.Windows.Forms.Label label176;
        public System.Windows.Forms.ListView lstZoneSound;
        private System.Windows.Forms.ColumnHeader columnHeader158;
        private System.Windows.Forms.ColumnHeader columnHeader159;
        public System.Windows.Forms.Button btnRunSoundModel;
        private System.Windows.Forms.Button btnExportSoundSummary;
        private System.Windows.Forms.Button btnExportShadowFlicker;
        private System.Windows.Forms.Button btnExportIceSummary;
        private System.Windows.Forms.Button btn_AddProj;
        private System.Windows.Forms.Button btnExportAllPVals;
        private System.Windows.Forms.Button btnDeleteExceed;
        private System.Windows.Forms.Button btnExportCurves;
        private System.Windows.Forms.Button btnExport_P_Vals;
        private System.Windows.Forms.Label label180;
        private System.Windows.Forms.ColumnHeader columnHeader160;
        private System.Windows.Forms.ColumnHeader columnHeader161;
        private System.Windows.Forms.ColumnHeader columnHeader162;
        private System.Windows.Forms.ColumnHeader columnHeader163;
        private System.Windows.Forms.ColumnHeader columnHeader164;
        private System.Windows.Forms.ColumnHeader columnHeader165;
        private System.Windows.Forms.ColumnHeader columnHeader166;
        private System.Windows.Forms.Label label181;
        private System.Windows.Forms.Label label182;
        private System.Windows.Forms.ColumnHeader columnHeader167;
        private System.Windows.Forms.ColumnHeader columnHeader168;
        private System.Windows.Forms.ColumnHeader columnHeader169;
        private System.Windows.Forms.ColumnHeader columnHeader170;
        private System.Windows.Forms.Button btn_editloss;
        internal System.Windows.Forms.Label label183;
        internal System.Windows.Forms.ComboBox cboExceedTurbine;
        public System.Windows.Forms.ListView lstDefinedLosses;
        private System.Windows.Forms.ColumnHeader columnHeader171;
        private System.Windows.Forms.ColumnHeader columnHeader172;
        private System.Windows.Forms.ColumnHeader columnHeader173;
        private System.Windows.Forms.ColumnHeader columnHeader174;
        public System.Windows.Forms.ListView lstPvals;
        internal System.Windows.Forms.ComboBox cboExceedWake;
        internal System.Windows.Forms.Label label185;
        public System.Windows.Forms.CheckBox chkShowCDFs;
        public System.Windows.Forms.CheckBox chkShowPDF;
        public System.Windows.Forms.Button btnDoMonteCarlo;
        internal System.Windows.Forms.TextBox txtOtherLosses;
        internal System.Windows.Forms.Label label88;
        private System.Windows.Forms.Button btnExportIceVsDist;
        public System.Windows.Forms.TextBox txtNumIceDays;
        internal System.Windows.Forms.Label label89;
        public System.Windows.Forms.TextBox txtTurbineNoise;
        internal System.Windows.Forms.Label label90;
        internal System.Windows.Forms.Label lblMaxFlickerHours;
        internal System.Windows.Forms.Label lblMaxFlickerDay;
        public System.Windows.Forms.TextBox txtMaxFlickerHours;
        public System.Windows.Forms.DateTimePicker dateMaxFlicker;
        internal System.Windows.Forms.Label label91;
        public System.Windows.Forms.TextBox txtNumIceThrowsPerDay;
        internal System.Windows.Forms.Label lblIceDistOrHisto;
        internal System.Windows.Forms.ComboBox cboIceDistORIceHisto;
        private System.Windows.Forms.ColumnHeader columnHeader175;
        private System.Windows.Forms.ColumnHeader columnHeader176;
        private System.Windows.Forms.TabPage pgeSiteConditions;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Label label170;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Label label95;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label93;
        internal System.Windows.Forms.Label label92;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Label label171;
        internal System.Windows.Forms.Label label172;
        internal System.Windows.Forms.ComboBox cboTurbWD;
        internal System.Windows.Forms.Label label174;
        public System.Windows.Forms.ComboBox cboTurbMet;
        public System.Windows.Forms.ListView lstTurbulence;
        private System.Windows.Forms.ColumnHeader columnHeader177;
        private System.Windows.Forms.ColumnHeader columnHeader178;
        internal System.Windows.Forms.ComboBox cboTurbineTI;
        internal System.Windows.Forms.Label label179;
        internal System.Windows.Forms.Label label178;
        internal System.Windows.Forms.ComboBox cboEffectiveTI_m;
        internal System.Windows.Forms.ComboBox cboTI_Type;
        internal System.Windows.Forms.Label label177;
        private System.Windows.Forms.ColumnHeader columnHeader179;
        public System.Windows.Forms.DateTimePicker dateTIStart;
        private System.Windows.Forms.Label label186;
        private System.Windows.Forms.Label label187;
        public System.Windows.Forms.DateTimePicker dateTIEnd;
        internal System.Windows.Forms.Label label188;
        internal System.Windows.Forms.ComboBox cboTurbPowerCurve;
        private System.Windows.Forms.Button btnExportTI;
        internal System.Windows.Forms.Label label189;
        public System.Windows.Forms.ComboBox cboExtremeShearMet;
        private System.Windows.Forms.Button btnExportShearStats;
        public System.Windows.Forms.ListView lstExtremeShear;
        private System.Windows.Forms.ColumnHeader columnHeader180;
        private System.Windows.Forms.ColumnHeader columnHeader181;
        private System.Windows.Forms.ColumnHeader columnHeader182;
        private System.Windows.Forms.ColumnHeader columnHeader183;
        private System.Windows.Forms.ColumnHeader columnHeader184;
        internal System.Windows.Forms.Label label190;
        public System.Windows.Forms.ComboBox cboExtremeShearRange;
        private System.Windows.Forms.Button btnInflowAngles;
        internal System.Windows.Forms.ComboBox cboInflowTurbine;
        internal System.Windows.Forms.Label label197;
        internal System.Windows.Forms.Label label196;
        internal System.Windows.Forms.ComboBox cboInflowWD;
        private System.Windows.Forms.Button btnExtremeWS;
        internal System.Windows.Forms.Label label195;
        public System.Windows.Forms.ComboBox cboExtremeWSMet;
        private System.Windows.Forms.Label label193;
        private System.Windows.Forms.Label label194;
        private System.Windows.Forms.Label label192;
        private System.Windows.Forms.Label label191;
        public System.Windows.Forms.TextBox txt1yrExtremeGust;
        public System.Windows.Forms.TextBox txt1yrExtreme10min;
        public System.Windows.Forms.TextBox txt50yrExtremeGust;
        public System.Windows.Forms.TextBox txt50yrExtreme10min;
        internal System.Windows.Forms.Label label199;
        internal System.Windows.Forms.ComboBox cboInflowReso;
        internal System.Windows.Forms.Label label198;
        internal System.Windows.Forms.ComboBox cboInflowRadius;
        internal System.Windows.Forms.Label label200;
        public System.Windows.Forms.TextBox txtInflowAngle;
        internal System.Windows.Forms.Label label201;
        public System.Windows.Forms.Button btnDoMCP;
        public System.Windows.Forms.DateTimePicker dateTimeExtremeShearStart;
        private System.Windows.Forms.Label label202;
        private System.Windows.Forms.Label label203;
        public System.Windows.Forms.DateTimePicker dateTimeExtremeShearEnd;
        private System.Windows.Forms.ColumnHeader columnHeader185;
        internal System.Windows.Forms.TextBox txtisMCPdGross;
        public System.Windows.Forms.Button btnDoMCPAllMets;
        internal System.Windows.Forms.TextBox txtisMCPdNet;
        internal System.Windows.Forms.TextBox txtisMCPdUncert;
        internal System.Windows.Forms.TextBox txtisMCPdAdv;
        private System.Windows.Forms.Button btnGetDefaultExceed;
        private System.Windows.Forms.Button btnImportCurves;
        internal System.Windows.Forms.OpenFileDialog ofdExceedCurves;
        internal System.Windows.Forms.Button btnModHeight;
        internal System.Windows.Forms.CheckBox chkDisableFilter;
        internal System.Windows.Forms.ColumnHeader columnHeader186;
        private System.Windows.Forms.ColumnHeader columnHeader193;
        private System.Windows.Forms.ColumnHeader columnHeader194;
        private System.Windows.Forms.Label label51;
        internal System.Windows.Forms.CheckBox chkCreateTurbTS;
        internal System.Windows.Forms.Button btnGenTurbGross;
        private System.Windows.Forms.Label lblExtremeWS;
        public System.Windows.Forms.Label lblNoExtremeWS;
        public System.Windows.Forms.ListView lstZones;
        public System.Windows.Forms.Label lblTurbineTSNoAdvanced;
        private System.Windows.Forms.Button btnShowMCPRanges;
        public System.Windows.Forms.CheckBox chkUseSR;
        public System.Windows.Forms.CheckBox chk_Use_Sep;
        public System.Windows.Forms.OpenFileDialog ofdPowerCurve;
        public System.Windows.Forms.ListView lstTurbines;
        public System.Windows.Forms.ListView lstPowerCurveList;
        public System.Windows.Forms.SaveFileDialog sfdCFMfile;
        public System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        public System.Windows.Forms.TabControl tabContinuum;
        public MatplotlibCS.Figure matPlotTopo;
        public OxyPlot.WindowsForms.PlotView plotTopo;
        public OxyPlot.WindowsForms.PlotView plotUWExpo;
        public OxyPlot.WindowsForms.PlotView plotDirectionalWS_Ratios;
        public OxyPlot.WindowsForms.PlotView plotInputWindRose;
        public OxyPlot.WindowsForms.PlotView plotWS_vsHeight;
        private System.Windows.Forms.Label label62;
        public OxyPlot.WindowsForms.PlotView plotMetQC_WindRose;
        public OxyPlot.WindowsForms.PlotView plotAlphaByWD;
        public OxyPlot.WindowsForms.PlotView plotAnemScatter;
        public OxyPlot.WindowsForms.PlotView plotWSDiffByWS;
        public OxyPlot.WindowsForms.PlotView plotWSDiffByWD;
        public OxyPlot.WindowsForms.PlotView plotMERRA_Monthly;
        public OxyPlot.WindowsForms.PlotView plotMERRA_Yearly;
        public OxyPlot.WindowsForms.PlotView plotMERRA_WindRose;
        public OxyPlot.WindowsForms.PlotView plotMCP;
        public OxyPlot.WindowsForms.PlotView plotMCP_Uncertainty;
        public OxyPlot.WindowsForms.PlotView plotDW_DH;
        public OxyPlot.WindowsForms.PlotView plotUW_DH;
        public OxyPlot.WindowsForms.PlotView plotDW_SR;
        public OxyPlot.WindowsForms.PlotView plotUW_SR;
        public OxyPlot.WindowsForms.PlotView plotElev;
        public OxyPlot.WindowsForms.PlotView plotDWUWExpo;
        public OxyPlot.WindowsForms.PlotView plotDWExpo;
        public OxyPlot.WindowsForms.PlotView plotGrossWindRose;
        public OxyPlot.WindowsForms.PlotView plotGrossWS_Dist;
        public OxyPlot.WindowsForms.PlotView plotGross_PowerCrvs;
        public OxyPlot.WindowsForms.PlotView plotGrossHisto;
        public OxyPlot.WindowsForms.PlotView plotExceedCurves;
        public OxyPlot.WindowsForms.PlotView plotCompositeExceed;
        public OxyPlot.WindowsForms.PlotView plotWakedDists;
        public OxyPlot.WindowsForms.PlotView plotTurbsByString;
        public OxyPlot.WindowsForms.PlotView plotWakeMap;
        public OxyPlot.WindowsForms.PlotView plotTurbInt;
        public OxyPlot.WindowsForms.PlotView plotExtremeShear;
        public OxyPlot.WindowsForms.PlotView plotExtremeWS;
        public OxyPlot.WindowsForms.PlotView plotInflowAngle;
        public OxyPlot.WindowsForms.PlotView plotMonthlyTS;
        public OxyPlot.WindowsForms.PlotView plotYearlyTS;
        public OxyPlot.WindowsForms.PlotView plotGenMap;
        public OxyPlot.WindowsForms.PlotView plotRRErrorByNumMets;
        public OxyPlot.WindowsForms.PlotView plotRR_Histo;
        public OxyPlot.WindowsForms.PlotView plotTurbUncert;
        public OxyPlot.WindowsForms.PlotView plotAdvTopo;
        public OxyPlot.WindowsForms.PlotView plotPathAlongNodes;
        public OxyPlot.WindowsForms.PlotView plotDHModel;
        public OxyPlot.WindowsForms.PlotView plotUHModel;
        public OxyPlot.WindowsForms.PlotView plotIceShadowSound;
        public OxyPlot.WindowsForms.PlotView plotIceVsDist;
        internal System.Windows.Forms.Button btnDownloadMERRA2;
        private System.Windows.Forms.Button btnChangeFolder;
        private System.Windows.Forms.Label label54;
        public System.Windows.Forms.TextBox txtMaxLong;
        private System.Windows.Forms.Label label146;
        public System.Windows.Forms.TextBox txtMinLong;
        private System.Windows.Forms.Label label52;
        public System.Windows.Forms.TextBox txtMaxLat;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtMinLat;
        public System.Windows.Forms.Button btnExportTarget;
        internal System.Windows.Forms.ComboBox cboSelVane;
        private System.Windows.Forms.Label label184;
        internal System.Windows.Forms.ComboBox cboAnemB;
        private System.Windows.Forms.Label label155;
        internal System.Windows.Forms.ComboBox cboAnemA;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateHeadersToolStripMenuItem;
        internal System.Windows.Forms.CheckBox chkMaxWS_Range;
        internal System.Windows.Forms.CheckBox chkMaxWS_SD;
        internal System.Windows.Forms.CheckBox chkMinWS_SD;
        internal System.Windows.Forms.CheckBox chkMinWS;
        internal System.Windows.Forms.CheckBox chkIcing;
        internal System.Windows.Forms.CheckBox chkTowerShadow;
        private System.Windows.Forms.Button btnResetMaxRecovDates;
    }
}

