using System.Windows.Forms;

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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Average");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("St. Dev.");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Minimum");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Maximum");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Alpha");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Extrap. WS");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Anems", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Average");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("St. Dev.");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Minimum");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Maximum");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Vanes", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Average");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("St. Dev.");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Minimum");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Maximum");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Temps.", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Average");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("St. Dev.");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Minimum");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Maximum");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Baros.", new System.Windows.Forms.TreeNode[] {
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Continuum));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabContinuum = new System.Windows.Forms.TabControl();
            this.pgeInput = new System.Windows.Forms.TabPage();
            this.chkUseValleyFlow = new System.Windows.Forms.CheckBox();
            this.chkUseElevModel = new System.Windows.Forms.CheckBox();
            this.btnShowMetTS_Info = new System.Windows.Forms.Button();
            this.cboInputLLorDD = new System.Windows.Forms.ComboBox();
            this.pnlInputMap = new System.Windows.Forms.Panel();
            this.plotTopo = new OxyPlot.WindowsForms.PlotView();
            this.pnlInputMapAndLegend = new System.Windows.Forms.Panel();
            this.lblMetLabels = new System.Windows.Forms.Label();
            this.cboTopo_Or_Roughness = new System.Windows.Forms.ComboBox();
            this.chkAllTurbLabels = new System.Windows.Forms.CheckBox();
            this.chkAllMetLabels = new System.Windows.Forms.CheckBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.chkTurbLabels = new System.Windows.Forms.CheckedListBox();
            this.chkMetLabels = new System.Windows.Forms.CheckedListBox();
            this.txtTopoSource = new System.Windows.Forms.TextBox();
            this.cboWindOrEnergy = new System.Windows.Forms.ComboBox();
            this.btnEditAirDensity = new System.Windows.Forms.Button();
            this.label222 = new System.Windows.Forms.Label();
            this.txtAirDensity = new System.Windows.Forms.TextBox();
            this.btnEditRotorDiam = new System.Windows.Forms.Button();
            this.label221 = new System.Windows.Forms.Label();
            this.txtRotorDiam = new System.Windows.Forms.TextBox();
            this.txtTopoNullValue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.plotDirectionalWS_Ratios = new OxyPlot.WindowsForms.PlotView();
            this.plotInputWindRose = new OxyPlot.WindowsForms.PlotView();
            this.chkCreateTurbTS = new System.Windows.Forms.CheckBox();
            this.btnEditModHeight = new System.Windows.Forms.Button();
            this.label168 = new System.Windows.Forms.Label();
            this.txtModeledHeight = new System.Windows.Forms.TextBox();
            this.btnImportMetTS = new System.Windows.Forms.Button();
            this.btnImportRoughness = new System.Windows.Forms.Button();
            this.chk_Use_Sep = new System.Windows.Forms.CheckBox();
            this.Label84 = new System.Windows.Forms.Label();
            this.txt_LC_Key_selected = new System.Windows.Forms.TextBox();
            this.chkUseSR = new System.Windows.Forms.CheckBox();
            this.btnViewModNLCD = new System.Windows.Forms.Button();
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
            this.btnDelTurb = new System.Windows.Forms.Button();
            this.btnEditTurb = new System.Windows.Forms.Button();
            this.btnAddTurb = new System.Windows.Forms.Button();
            this.lstTurbines = new System.Windows.Forms.ListView();
            this.ColumnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTurbineList = new System.Windows.Forms.Label();
            this.btnDelMet = new System.Windows.Forms.Button();
            this.lstMetTowers = new System.Windows.Forms.ListView();
            this.metName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lats = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Longs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblMetTable = new System.Windows.Forms.Label();
            this.btnTurbines = new System.Windows.Forms.Button();
            this.btnLoadXYZ = new System.Windows.Forms.Button();
            this.pgeMetDataTS = new System.Windows.Forms.TabPage();
            this.splContMetTS = new System.Windows.Forms.SplitContainer();
            this.dataMetTS = new System.Windows.Forms.DataGridView();
            this.chkTS_Params = new System.Windows.Forms.CheckedListBox();
            this.treeDataParams = new System.Windows.Forms.TreeView();
            this.chkMetsTS = new System.Windows.Forms.CheckedListBox();
            this.btnShowFilterFlags = new System.Windows.Forms.Button();
            this.label146 = new System.Windows.Forms.Label();
            this.chkShowFilteredData = new System.Windows.Forms.CheckBox();
            this.chkShowLegenMetDataTS = new System.Windows.Forms.CheckBox();
            this.plotTS_Baros = new OxyPlot.WindowsForms.PlotView();
            this.label213 = new System.Windows.Forms.Label();
            this.cboPlot4Type = new System.Windows.Forms.ComboBox();
            this.label212 = new System.Windows.Forms.Label();
            this.cboPlot3Type = new System.Windows.Forms.ComboBox();
            this.label211 = new System.Windows.Forms.Label();
            this.cboPlot2Type = new System.Windows.Forms.ComboBox();
            this.label210 = new System.Windows.Forms.Label();
            this.cboPlot1Type = new System.Windows.Forms.ComboBox();
            this.label209 = new System.Windows.Forms.Label();
            this.cboNumPlots = new System.Windows.Forms.ComboBox();
            this.lblMetDataTS_Inc = new System.Windows.Forms.Label();
            this.txtNumDaysTS = new System.Windows.Forms.TextBox();
            this.btnMetTS_Left = new System.Windows.Forms.Button();
            this.btnMetTS_Right = new System.Windows.Forms.Button();
            this.label208 = new System.Windows.Forms.Label();
            this.label149 = new System.Windows.Forms.Label();
            this.dateMetTS_End = new System.Windows.Forms.DateTimePicker();
            this.dateMetTS_Start = new System.Windows.Forms.DateTimePicker();
            this.plotTS_Temp = new OxyPlot.WindowsForms.PlotView();
            this.plotTS_Vanes = new OxyPlot.WindowsForms.PlotView();
            this.plotTS_Anems = new OxyPlot.WindowsForms.PlotView();
            this.pgeMetData = new System.Windows.Forms.TabPage();
            this.label102 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.pnlMetDatQC_WSDiff = new System.Windows.Forms.Panel();
            this.plotWSDiffByWS = new OxyPlot.WindowsForms.PlotView();
            this.pnlMetDataQC_Scatter = new System.Windows.Forms.Panel();
            this.plotAnemScatter = new OxyPlot.WindowsForms.PlotView();
            this.label70 = new System.Windows.Forms.Label();
            this.plotWSDiffByWD = new OxyPlot.WindowsForms.PlotView();
            this.txtShearCalcMethod = new System.Windows.Forms.TextBox();
            this.label227 = new System.Windows.Forms.Label();
            this.btnEditShearMethod = new System.Windows.Forms.Button();
            this.label226 = new System.Windows.Forms.Label();
            this.txtShearBestFitMaxHeight = new System.Windows.Forms.TextBox();
            this.txtShearBestFitMinHeight = new System.Windows.Forms.TextBox();
            this.label225 = new System.Windows.Forms.Label();
            this.btnResetMaxRecovDates = new System.Windows.Forms.Button();
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
            this.btnExportCloudCover = new System.Windows.Forms.Button();
            this.cboNumWDRefTab = new System.Windows.Forms.ComboBox();
            this.label223 = new System.Windows.Forms.Label();
            this.cboRefWindOrEnergy = new System.Windows.Forms.ComboBox();
            this.label217 = new System.Windows.Forms.Label();
            this.txtMaxLong = new System.Windows.Forms.TextBox();
            this.label218 = new System.Windows.Forms.Label();
            this.txtMinLong = new System.Windows.Forms.TextBox();
            this.label219 = new System.Windows.Forms.Label();
            this.txtMaxLat = new System.Windows.Forms.TextBox();
            this.label220 = new System.Windows.Forms.Label();
            this.txtMinLat = new System.Windows.Forms.TextBox();
            this.txtRefDataDownloadName = new System.Windows.Forms.TextBox();
            this.label216 = new System.Windows.Forms.Label();
            this.txtRefDataDownloadFolder = new System.Windows.Forms.TextBox();
            this.btnRefDataDownloads = new System.Windows.Forms.Button();
            this.btnDelRef = new System.Windows.Forms.Button();
            this.btnEditReference = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.label207 = new System.Windows.Forms.Label();
            this.cboLTReferences = new System.Windows.Forms.ComboBox();
            this.label205 = new System.Windows.Forms.Label();
            this.txtRefDataAvail = new System.Windows.Forms.TextBox();
            this.label204 = new System.Windows.Forms.Label();
            this.dateLTRefAvailEnd = new System.Windows.Forms.DateTimePicker();
            this.dateLTRefAvailStart = new System.Windows.Forms.DateTimePicker();
            this.label52 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.btnExplainMERRA2Tab = new System.Windows.Forms.Button();
            this.plotMERRA_WindRose = new OxyPlot.WindowsForms.PlotView();
            this.plotMERRA_Monthly = new OxyPlot.WindowsForms.PlotView();
            this.plotMERRA_Yearly = new OxyPlot.WindowsForms.PlotView();
            this.btnImportCRV_MERRA = new System.Windows.Forms.Button();
            this.chkYearsToDisplayAll = new System.Windows.Forms.CheckBox();
            this.label121 = new System.Windows.Forms.Label();
            this.cboReferenceMonth = new System.Windows.Forms.ComboBox();
            this.label120 = new System.Windows.Forms.Label();
            this.cboReferenceYear = new System.Windows.Forms.ComboBox();
            this.chkYearsToDisplay = new System.Windows.Forms.CheckedListBox();
            this.label162 = new System.Windows.Forms.Label();
            this.txtMERRA_WS_ScaleFact = new System.Windows.Forms.TextBox();
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
            this.label154 = new System.Windows.Forms.Label();
            this.label151 = new System.Windows.Forms.Label();
            this.pgeMCP = new System.Windows.Forms.TabPage();
            this.btnShowMCP_Info = new System.Windows.Forms.Button();
            this.btnClearMCP = new System.Windows.Forms.Button();
            this.label237 = new System.Windows.Forms.Label();
            this.cboMCP_Height = new System.Windows.Forms.ComboBox();
            this.label206 = new System.Windows.Forms.Label();
            this.cboMCP_Ref = new System.Windows.Forms.ComboBox();
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
            this.tabSiteConditions = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkTerrainSlope_UWOnly = new System.Windows.Forms.CheckBox();
            this.btnExportElevProfile = new System.Windows.Forms.Button();
            this.btnExportTerrainComplexSector = new System.Windows.Forms.Button();
            this.cboNumWDComplxTab = new System.Windows.Forms.ComboBox();
            this.label224 = new System.Windows.Forms.Label();
            this.chkForceThruBase = new System.Windows.Forms.CheckBox();
            this.btnCalcTerrainComplexity = new System.Windows.Forms.Button();
            this.label161 = new System.Windows.Forms.Label();
            this.cboTSIorTVIorP90 = new System.Windows.Forms.ComboBox();
            this.plotComplexHisto = new OxyPlot.WindowsForms.PlotView();
            this.btnShowIECThresh = new System.Windows.Forms.Button();
            this.lblIEC_Complexity = new System.Windows.Forms.Label();
            this.label147 = new System.Windows.Forms.Label();
            this.dataTerrainComplex = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label199 = new System.Windows.Forms.Label();
            this.label198 = new System.Windows.Forms.Label();
            this.label197 = new System.Windows.Forms.Label();
            this.label196 = new System.Windows.Forms.Label();
            this.plotInflowAngle = new OxyPlot.WindowsForms.PlotView();
            this.label200 = new System.Windows.Forms.Label();
            this.txtInflowAngle = new System.Windows.Forms.TextBox();
            this.cboInflowReso = new System.Windows.Forms.ComboBox();
            this.cboInflowRadius = new System.Windows.Forms.ComboBox();
            this.btnInflowAngles = new System.Windows.Forms.Button();
            this.cboInflowTurbine = new System.Windows.Forms.ComboBox();
            this.cboInflowWD = new System.Windows.Forms.ComboBox();
            this.label171 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cboTI_TerrainComplexCorr = new System.Windows.Forms.ComboBox();
            this.chkApplyTCCtoEffTI = new System.Windows.Forms.CheckBox();
            this.label215 = new System.Windows.Forms.Label();
            this.label214 = new System.Windows.Forms.Label();
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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnEditShearFromSiteConds = new System.Windows.Forms.Button();
            this.txtShearCalcMethodExtremeTab = new System.Windows.Forms.TextBox();
            this.txtMaxHeight = new System.Windows.Forms.TextBox();
            this.txtMinHeight = new System.Windows.Forms.TextBox();
            this.label228 = new System.Windows.Forms.Label();
            this.label229 = new System.Windows.Forms.Label();
            this.label230 = new System.Windows.Forms.Label();
            this.btnExportShearHisto = new System.Windows.Forms.Button();
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
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnExportExtremeWSTable = new System.Windows.Forms.Button();
            this.txtWMO_Desc = new System.Windows.Forms.TextBox();
            this.label242 = new System.Windows.Forms.Label();
            this.txtWMO_HourGust = new System.Windows.Forms.TextBox();
            this.label243 = new System.Windows.Forms.Label();
            this.txtWMO_HourTenMin = new System.Windows.Forms.TextBox();
            this.label241 = new System.Windows.Forms.Label();
            this.chkUseWMO_TenMin = new System.Windows.Forms.CheckBox();
            this.label240 = new System.Windows.Forms.Label();
            this.cboWMO_Class = new System.Windows.Forms.ComboBox();
            this.chkUseWMO_Gust = new System.Windows.Forms.CheckBox();
            this.chkExtremeWS_ShowLegend = new System.Windows.Forms.CheckBox();
            this.label239 = new System.Windows.Forms.Label();
            this.dateExtremeWS_End = new System.Windows.Forms.DateTimePicker();
            this.dateExtremeWS_Start = new System.Windows.Forms.DateTimePicker();
            this.lblGustExtremeWSUnavailable = new System.Windows.Forms.Label();
            this.label238 = new System.Windows.Forms.Label();
            this.cboExtremeWS_Height = new System.Windows.Forms.ComboBox();
            this.label235 = new System.Windows.Forms.Label();
            this.txtGumbelGustMu = new System.Windows.Forms.TextBox();
            this.label236 = new System.Windows.Forms.Label();
            this.txtGumbelGustBeta = new System.Windows.Forms.TextBox();
            this.label234 = new System.Windows.Forms.Label();
            this.label233 = new System.Windows.Forms.Label();
            this.txtGumbelTenMinMu = new System.Windows.Forms.TextBox();
            this.label232 = new System.Windows.Forms.Label();
            this.label231 = new System.Windows.Forms.Label();
            this.txtGumbelTenMinBeta = new System.Windows.Forms.TextBox();
            this.chkUseSimData = new System.Windows.Forms.CheckBox();
            this.plotExtremeWS_TS = new OxyPlot.WindowsForms.PlotView();
            this.dataExtremeWS = new System.Windows.Forms.DataGridView();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label195 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.cboExtremeWSRef = new System.Windows.Forms.ComboBox();
            this.plotExtremeWS = new OxyPlot.WindowsForms.PlotView();
            this.lblExtremeWS = new System.Windows.Forms.Label();
            this.btnExtremeWS = new System.Windows.Forms.Button();
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
            this.btnExportRR_Summary = new System.Windows.Forms.Button();
            this.btnDoAllRRs = new System.Windows.Forms.Button();
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
            this.spltNodePlotParams = new System.Windows.Forms.SplitContainer();
            this.plotPathAlongNodes = new OxyPlot.WindowsForms.PlotView();
            this.chkWeight_RMS = new System.Windows.Forms.CheckBox();
            this.txtSectRMS = new System.Windows.Forms.TextBox();
            this.Label48 = new System.Windows.Forms.Label();
            this.txtUWDWRMS = new System.Windows.Forms.TextBox();
            this.Label44 = new System.Windows.Forms.Label();
            this.chkAdvToShow = new System.Windows.Forms.CheckedListBox();
            this.spltModelCoeffs = new System.Windows.Forms.SplitContainer();
            this.plotUHModel = new OxyPlot.WindowsForms.PlotView();
            this.Label101 = new System.Windows.Forms.Label();
            this.txtSepCritWS = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.txtSepCrit = new System.Windows.Forms.TextBox();
            this.Label100 = new System.Windows.Forms.Label();
            this.cboDHplot = new System.Windows.Forms.ComboBox();
            this.plotDHModel = new OxyPlot.WindowsForms.PlotView();
            this.cboExpo_or_Stab = new System.Windows.Forms.ComboBox();
            this.Label82 = new System.Windows.Forms.Label();
            this.Label59 = new System.Windows.Forms.Label();
            this.txtUWCrit = new System.Windows.Forms.TextBox();
            this.Label58 = new System.Windows.Forms.Label();
            this.cboUphill_to_show = new System.Windows.Forms.ComboBox();
            this.spltAdvanced = new System.Windows.Forms.SplitContainer();
            this.lblTurbineTSNoAdvanced = new System.Windows.Forms.Label();
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
            this.spltAdvUWDW = new System.Windows.Forms.SplitContainer();
            this.Label8 = new System.Windows.Forms.Label();
            this.lstPathNodes_UW = new System.Windows.Forms.ListView();
            this.ColumnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader121 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader115 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader120 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader111 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label39 = new System.Windows.Forms.Label();
            this.lstPathNodes_DW = new System.Windows.Forms.ListView();
            this.ColumnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader110 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader108 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader109 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader112 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnExportRMS_Errors = new System.Windows.Forms.Button();
            this.txtImportedModel = new System.Windows.Forms.TextBox();
            this.btnImportModel = new System.Windows.Forms.Button();
            this.btnExportModel = new System.Windows.Forms.Button();
            this.plotAdvTopo = new OxyPlot.WindowsForms.PlotView();
            this.txtisMCPdAdv = new System.Windows.Forms.TextBox();
            this.cboSeasonAdvanced = new System.Windows.Forms.ComboBox();
            this.cboTODAdvanced = new System.Windows.Forms.ComboBox();
            this.txtAdv_FlowSep_Used = new System.Windows.Forms.TextBox();
            this.txtAdv_LC_used = new System.Windows.Forms.TextBox();
            this.cboAdvancedWD = new System.Windows.Forms.ComboBox();
            this.Label56 = new System.Windows.Forms.Label();
            this.btnExportCrossPreds = new System.Windows.Forms.Button();
            this.Label14 = new System.Windows.Forms.Label();
            this.cboAdvancedRad = new System.Windows.Forms.ComboBox();
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
            this.btnExportStepwise = new System.Windows.Forms.Button();
            this.lstModCrossPred = new System.Windows.Forms.ListView();
            this.ColumnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader30 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader31 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label11 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.pgeSuitability = new System.Windows.Forms.TabPage();
            this.btnExportSiteSuitMap = new System.Windows.Forms.Button();
            this.btnZoneFileFormatHelp = new System.Windows.Forms.Button();
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
            this.downloadReanalysisDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadTopographyDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mongoDBTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.txtMaxShadowDist = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tabContinuum.SuspendLayout();
            this.pgeInput.SuspendLayout();
            this.pnlInputMap.SuspendLayout();
            this.pnlInputMapAndLegend.SuspendLayout();
            this.pgeMetDataTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splContMetTS)).BeginInit();
            this.splContMetTS.Panel1.SuspendLayout();
            this.splContMetTS.Panel2.SuspendLayout();
            this.splContMetTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMetTS)).BeginInit();
            this.pgeMetData.SuspendLayout();
            this.pnlMetDatQC_WSDiff.SuspendLayout();
            this.pnlMetDataQC_Scatter.SuspendLayout();
            this.pgeMERRA.SuspendLayout();
            this.pgeMCP.SuspendLayout();
            this.pgeMetSumm.SuspendLayout();
            this.pgeGrossTurbs.SuspendLayout();
            this.pgeExceedance.SuspendLayout();
            this.pgeNetEsts.SuspendLayout();
            this.pgeSiteConditions.SuspendLayout();
            this.tabSiteConditions.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTerrainComplex)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataExtremeWS)).BeginInit();
            this.pgeMonthlyAnalysis.SuspendLayout();
            this.pgeMaps.SuspendLayout();
            this.pgeRound.SuspendLayout();
            this.pgeStepwise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltNodePlotParams)).BeginInit();
            this.spltNodePlotParams.Panel1.SuspendLayout();
            this.spltNodePlotParams.Panel2.SuspendLayout();
            this.spltNodePlotParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltModelCoeffs)).BeginInit();
            this.spltModelCoeffs.Panel1.SuspendLayout();
            this.spltModelCoeffs.Panel2.SuspendLayout();
            this.spltModelCoeffs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltAdvanced)).BeginInit();
            this.spltAdvanced.Panel1.SuspendLayout();
            this.spltAdvanced.Panel2.SuspendLayout();
            this.spltAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltAdvUWDW)).BeginInit();
            this.spltAdvUWDW.Panel1.SuspendLayout();
            this.spltAdvUWDW.Panel2.SuspendLayout();
            this.spltAdvUWDW.SuspendLayout();
            this.pgeSuitability.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabContinuum
            // 
            this.tabContinuum.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabContinuum.CausesValidation = false;
            this.tabContinuum.Controls.Add(this.pgeInput);
            this.tabContinuum.Controls.Add(this.pgeMetDataTS);
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
            this.tabContinuum.Size = new System.Drawing.Size(1496, 743);
            this.tabContinuum.TabIndex = 3;
            // 
            // pgeInput
            // 
            this.pgeInput.AutoScroll = true;
            this.pgeInput.Controls.Add(this.chkUseValleyFlow);
            this.pgeInput.Controls.Add(this.chkUseElevModel);
            this.pgeInput.Controls.Add(this.btnShowMetTS_Info);
            this.pgeInput.Controls.Add(this.cboInputLLorDD);
            this.pgeInput.Controls.Add(this.pnlInputMap);
            this.pgeInput.Controls.Add(this.pnlInputMapAndLegend);
            this.pgeInput.Controls.Add(this.cboWindOrEnergy);
            this.pgeInput.Controls.Add(this.btnEditAirDensity);
            this.pgeInput.Controls.Add(this.label222);
            this.pgeInput.Controls.Add(this.txtAirDensity);
            this.pgeInput.Controls.Add(this.btnEditRotorDiam);
            this.pgeInput.Controls.Add(this.label221);
            this.pgeInput.Controls.Add(this.txtRotorDiam);
            this.pgeInput.Controls.Add(this.txtTopoNullValue);
            this.pgeInput.Controls.Add(this.label5);
            this.pgeInput.Controls.Add(this.plotDirectionalWS_Ratios);
            this.pgeInput.Controls.Add(this.plotInputWindRose);
            this.pgeInput.Controls.Add(this.chkCreateTurbTS);
            this.pgeInput.Controls.Add(this.btnEditModHeight);
            this.pgeInput.Controls.Add(this.label168);
            this.pgeInput.Controls.Add(this.txtModeledHeight);
            this.pgeInput.Controls.Add(this.btnImportMetTS);
            this.pgeInput.Controls.Add(this.btnImportRoughness);
            this.pgeInput.Controls.Add(this.chk_Use_Sep);
            this.pgeInput.Controls.Add(this.Label84);
            this.pgeInput.Controls.Add(this.txt_LC_Key_selected);
            this.pgeInput.Controls.Add(this.chkUseSR);
            this.pgeInput.Controls.Add(this.btnViewModNLCD);
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
            this.pgeInput.Controls.Add(this.btnDelTurb);
            this.pgeInput.Controls.Add(this.btnEditTurb);
            this.pgeInput.Controls.Add(this.btnAddTurb);
            this.pgeInput.Controls.Add(this.lstTurbines);
            this.pgeInput.Controls.Add(this.lblTurbineList);
            this.pgeInput.Controls.Add(this.btnDelMet);
            this.pgeInput.Controls.Add(this.lstMetTowers);
            this.pgeInput.Controls.Add(this.lblMetTable);
            this.pgeInput.Controls.Add(this.btnTurbines);
            this.pgeInput.Controls.Add(this.btnLoadXYZ);
            this.pgeInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgeInput.Location = new System.Drawing.Point(4, 27);
            this.pgeInput.Margin = new System.Windows.Forms.Padding(0);
            this.pgeInput.Name = "pgeInput";
            this.pgeInput.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeInput.Size = new System.Drawing.Size(1488, 712);
            this.pgeInput.TabIndex = 0;
            this.pgeInput.Text = "Input";
            this.pgeInput.UseVisualStyleBackColor = true;
            // 
            // chkUseValleyFlow
            // 
            this.chkUseValleyFlow.AutoSize = true;
            this.chkUseValleyFlow.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseValleyFlow.Location = new System.Drawing.Point(330, 235);
            this.chkUseValleyFlow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkUseValleyFlow.Name = "chkUseValleyFlow";
            this.chkUseValleyFlow.Size = new System.Drawing.Size(141, 21);
            this.chkUseValleyFlow.TabIndex = 144;
            this.chkUseValleyFlow.Text = "Enable Valley Model";
            this.chkUseValleyFlow.UseVisualStyleBackColor = true;
            this.chkUseValleyFlow.CheckedChanged += new System.EventHandler(this.chkUseValleyFlow_CheckedChanged);
            // 
            // chkUseElevModel
            // 
            this.chkUseElevModel.AutoSize = true;
            this.chkUseElevModel.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseElevModel.Location = new System.Drawing.Point(330, 213);
            this.chkUseElevModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkUseElevModel.Name = "chkUseElevModel";
            this.chkUseElevModel.Size = new System.Drawing.Size(158, 21);
            this.chkUseElevModel.TabIndex = 143;
            this.chkUseElevModel.Text = "Enable Elevation Model";
            this.chkUseElevModel.UseVisualStyleBackColor = true;
            this.chkUseElevModel.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnShowMetTS_Info
            // 
            this.btnShowMetTS_Info.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnShowMetTS_Info.Location = new System.Drawing.Point(483, 68);
            this.btnShowMetTS_Info.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowMetTS_Info.Name = "btnShowMetTS_Info";
            this.btnShowMetTS_Info.Size = new System.Drawing.Size(31, 30);
            this.btnShowMetTS_Info.TabIndex = 142;
            this.btnShowMetTS_Info.Text = "?";
            this.btnShowMetTS_Info.UseVisualStyleBackColor = true;
            this.btnShowMetTS_Info.Click += new System.EventHandler(this.btnShowMetTS_Info_Click);
            // 
            // cboInputLLorDD
            // 
            this.cboInputLLorDD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cboInputLLorDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboInputLLorDD.FormattingEnabled = true;
            this.cboInputLLorDD.Items.AddRange(new object[] {
            "UTM",
            "DD"});
            this.cboInputLLorDD.Location = new System.Drawing.Point(697, 216);
            this.cboInputLLorDD.Name = "cboInputLLorDD";
            this.cboInputLLorDD.Size = new System.Drawing.Size(79, 24);
            this.cboInputLLorDD.TabIndex = 141;
            this.cboInputLLorDD.SelectedIndexChanged += new System.EventHandler(this.cboInputLLorDD_SelectedIndexChanged);
            // 
            // pnlInputMap
            // 
            this.pnlInputMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInputMap.Controls.Add(this.plotTopo);
            this.pnlInputMap.Location = new System.Drawing.Point(787, 7);
            this.pnlInputMap.Name = "pnlInputMap";
            this.pnlInputMap.Size = new System.Drawing.Size(695, 520);
            this.pnlInputMap.TabIndex = 140;
            this.pnlInputMap.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // plotTopo
            // 
            this.plotTopo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotTopo.Location = new System.Drawing.Point(0, 0);
            this.plotTopo.MaximumSize = new System.Drawing.Size(1000, 700);
            this.plotTopo.Name = "plotTopo";
            this.plotTopo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTopo.Size = new System.Drawing.Size(695, 520);
            this.plotTopo.TabIndex = 139;
            this.plotTopo.Text = "plotTopo";
            this.plotTopo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTopo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTopo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // pnlInputMapAndLegend
            // 
            this.pnlInputMapAndLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInputMapAndLegend.Controls.Add(this.lblMetLabels);
            this.pnlInputMapAndLegend.Controls.Add(this.cboTopo_Or_Roughness);
            this.pnlInputMapAndLegend.Controls.Add(this.chkAllTurbLabels);
            this.pnlInputMapAndLegend.Controls.Add(this.chkAllMetLabels);
            this.pnlInputMapAndLegend.Controls.Add(this.Label23);
            this.pnlInputMapAndLegend.Controls.Add(this.chkTurbLabels);
            this.pnlInputMapAndLegend.Controls.Add(this.chkMetLabels);
            this.pnlInputMapAndLegend.Controls.Add(this.txtTopoSource);
            this.pnlInputMapAndLegend.Location = new System.Drawing.Point(787, 533);
            this.pnlInputMapAndLegend.Name = "pnlInputMapAndLegend";
            this.pnlInputMapAndLegend.Size = new System.Drawing.Size(698, 172);
            this.pnlInputMapAndLegend.TabIndex = 138;
            this.pnlInputMapAndLegend.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblMetLabels
            // 
            this.lblMetLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMetLabels.AutoSize = true;
            this.lblMetLabels.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetLabels.Location = new System.Drawing.Point(10, 17);
            this.lblMetLabels.Name = "lblMetLabels";
            this.lblMetLabels.Size = new System.Drawing.Size(91, 23);
            this.lblMetLabels.TabIndex = 135;
            this.lblMetLabels.Text = "Met Sites";
            // 
            // cboTopo_Or_Roughness
            // 
            this.cboTopo_Or_Roughness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTopo_Or_Roughness.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTopo_Or_Roughness.FormattingEnabled = true;
            this.cboTopo_Or_Roughness.Items.AddRange(new object[] {
            "Topography",
            "Land Cover",
            "Surface Roughness",
            "Displacement height"});
            this.cboTopo_Or_Roughness.Location = new System.Drawing.Point(524, 6);
            this.cboTopo_Or_Roughness.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTopo_Or_Roughness.Name = "cboTopo_Or_Roughness";
            this.cboTopo_Or_Roughness.Size = new System.Drawing.Size(147, 26);
            this.cboTopo_Or_Roughness.TabIndex = 133;
            this.cboTopo_Or_Roughness.Text = "Topography";
            this.cboTopo_Or_Roughness.SelectedIndexChanged += new System.EventHandler(this.cboTopo_Or_Roughness_SelectedIndexChanged_1);
            // 
            // chkAllTurbLabels
            // 
            this.chkAllTurbLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAllTurbLabels.AutoSize = true;
            this.chkAllTurbLabels.Checked = true;
            this.chkAllTurbLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllTurbLabels.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllTurbLabels.Location = new System.Drawing.Point(388, 23);
            this.chkAllTurbLabels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAllTurbLabels.Name = "chkAllTurbLabels";
            this.chkAllTurbLabels.Size = new System.Drawing.Size(123, 20);
            this.chkAllTurbLabels.TabIndex = 132;
            this.chkAllTurbLabels.Text = "Select/Deselect All";
            this.chkAllTurbLabels.UseVisualStyleBackColor = true;
            this.chkAllTurbLabels.CheckedChanged += new System.EventHandler(this.chkAllTurbLabels_CheckedChanged_1);
            // 
            // chkAllMetLabels
            // 
            this.chkAllMetLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAllMetLabels.AutoSize = true;
            this.chkAllMetLabels.Checked = true;
            this.chkAllMetLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllMetLabels.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllMetLabels.Location = new System.Drawing.Point(128, 24);
            this.chkAllMetLabels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAllMetLabels.Name = "chkAllMetLabels";
            this.chkAllMetLabels.Size = new System.Drawing.Size(123, 20);
            this.chkAllMetLabels.TabIndex = 131;
            this.chkAllMetLabels.Text = "Select/Deselect All";
            this.chkAllMetLabels.UseVisualStyleBackColor = true;
            this.chkAllMetLabels.CheckedChanged += new System.EventHandler(this.chkAllMetLabels_CheckedChanged_1);
            // 
            // Label23
            // 
            this.Label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label23.AutoSize = true;
            this.Label23.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(256, 17);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(122, 23);
            this.Label23.TabIndex = 130;
            this.Label23.Text = "Turbine Sites";
            // 
            // chkTurbLabels
            // 
            this.chkTurbLabels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTurbLabels.CheckOnClick = true;
            this.chkTurbLabels.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTurbLabels.FormattingEnabled = true;
            this.chkTurbLabels.Location = new System.Drawing.Point(265, 47);
            this.chkTurbLabels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbLabels.MultiColumn = true;
            this.chkTurbLabels.Name = "chkTurbLabels";
            this.chkTurbLabels.Size = new System.Drawing.Size(420, 64);
            this.chkTurbLabels.TabIndex = 129;
            this.chkTurbLabels.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkTurbLabels_ItemCheck);
            this.chkTurbLabels.SelectedIndexChanged += new System.EventHandler(this.chkTurbLabels_SelectedIndexChanged_2);
            // 
            // chkMetLabels
            // 
            this.chkMetLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkMetLabels.CheckOnClick = true;
            this.chkMetLabels.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMetLabels.FormattingEnabled = true;
            this.chkMetLabels.Location = new System.Drawing.Point(14, 47);
            this.chkMetLabels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetLabels.Name = "chkMetLabels";
            this.chkMetLabels.Size = new System.Drawing.Size(242, 64);
            this.chkMetLabels.TabIndex = 128;
            this.chkMetLabels.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkMetLabels_ItemCheck);
            // 
            // txtTopoSource
            // 
            this.txtTopoSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTopoSource.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTopoSource.Location = new System.Drawing.Point(14, 139);
            this.txtTopoSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTopoSource.Name = "txtTopoSource";
            this.txtTopoSource.ReadOnly = true;
            this.txtTopoSource.Size = new System.Drawing.Size(671, 25);
            this.txtTopoSource.TabIndex = 127;
            this.txtTopoSource.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboWindOrEnergy
            // 
            this.cboWindOrEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboWindOrEnergy.FormattingEnabled = true;
            this.cboWindOrEnergy.Items.AddRange(new object[] {
            "Wind Rose",
            "Energy Rose"});
            this.cboWindOrEnergy.Location = new System.Drawing.Point(180, 430);
            this.cboWindOrEnergy.Name = "cboWindOrEnergy";
            this.cboWindOrEnergy.Size = new System.Drawing.Size(121, 21);
            this.cboWindOrEnergy.TabIndex = 137;
            this.cboWindOrEnergy.SelectedIndexChanged += new System.EventHandler(this.cboWindOrEnergy_SelectedIndexChanged);
            // 
            // btnEditAirDensity
            // 
            this.btnEditAirDensity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditAirDensity.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditAirDensity.Location = new System.Drawing.Point(679, 642);
            this.btnEditAirDensity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditAirDensity.Name = "btnEditAirDensity";
            this.btnEditAirDensity.Size = new System.Drawing.Size(50, 32);
            this.btnEditAirDensity.TabIndex = 136;
            this.btnEditAirDensity.Text = "Edit";
            this.btnEditAirDensity.UseVisualStyleBackColor = true;
            this.btnEditAirDensity.Click += new System.EventHandler(this.btnEditAirDensity_Click);
            // 
            // label222
            // 
            this.label222.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label222.AutoSize = true;
            this.label222.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label222.Location = new System.Drawing.Point(666, 564);
            this.label222.Name = "label222";
            this.label222.Size = new System.Drawing.Size(77, 36);
            this.label222.TabIndex = 135;
            this.label222.Text = "Air Density\r\n[kg/m3]:";
            this.label222.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtAirDensity
            // 
            this.txtAirDensity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAirDensity.Enabled = false;
            this.txtAirDensity.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAirDensity.Location = new System.Drawing.Point(679, 610);
            this.txtAirDensity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAirDensity.Name = "txtAirDensity";
            this.txtAirDensity.Size = new System.Drawing.Size(49, 25);
            this.txtAirDensity.TabIndex = 134;
            this.txtAirDensity.Text = "1.225";
            this.txtAirDensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnEditRotorDiam
            // 
            this.btnEditRotorDiam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditRotorDiam.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditRotorDiam.Location = new System.Drawing.Point(642, 519);
            this.btnEditRotorDiam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditRotorDiam.Name = "btnEditRotorDiam";
            this.btnEditRotorDiam.Size = new System.Drawing.Size(50, 32);
            this.btnEditRotorDiam.TabIndex = 133;
            this.btnEditRotorDiam.Text = "Edit";
            this.btnEditRotorDiam.UseVisualStyleBackColor = true;
            this.btnEditRotorDiam.Click += new System.EventHandler(this.btnRotorDiam_Click);
            // 
            // label221
            // 
            this.label221.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label221.AutoSize = true;
            this.label221.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label221.Location = new System.Drawing.Point(632, 441);
            this.label221.Name = "label221";
            this.label221.Size = new System.Drawing.Size(71, 36);
            this.label221.TabIndex = 132;
            this.label221.Text = "Rotor \r\nDiam. [m]:";
            this.label221.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtRotorDiam
            // 
            this.txtRotorDiam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRotorDiam.Enabled = false;
            this.txtRotorDiam.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRotorDiam.Location = new System.Drawing.Point(642, 487);
            this.txtRotorDiam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRotorDiam.Name = "txtRotorDiam";
            this.txtRotorDiam.Size = new System.Drawing.Size(49, 25);
            this.txtRotorDiam.TabIndex = 131;
            this.txtRotorDiam.Text = "100";
            this.txtRotorDiam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTopoNullValue
            // 
            this.txtTopoNullValue.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTopoNullValue.Location = new System.Drawing.Point(90, 104);
            this.txtTopoNullValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTopoNullValue.Name = "txtTopoNullValue";
            this.txtTopoNullValue.Size = new System.Drawing.Size(52, 22);
            this.txtTopoNullValue.TabIndex = 130;
            this.txtTopoNullValue.Text = "-99999";
            this.txtTopoNullValue.TextChanged += new System.EventHandler(this.txtTopoNullValue_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 129;
            this.label5.Text = "Null Value:";
            // 
            // plotDirectionalWS_Ratios
            // 
            this.plotDirectionalWS_Ratios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotDirectionalWS_Ratios.Location = new System.Drawing.Point(318, 450);
            this.plotDirectionalWS_Ratios.Name = "plotDirectionalWS_Ratios";
            this.plotDirectionalWS_Ratios.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDirectionalWS_Ratios.Size = new System.Drawing.Size(284, 235);
            this.plotDirectionalWS_Ratios.TabIndex = 128;
            this.plotDirectionalWS_Ratios.Text = "plotView2";
            this.plotDirectionalWS_Ratios.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDirectionalWS_Ratios.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDirectionalWS_Ratios.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotInputWindRose
            // 
            this.plotInputWindRose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotInputWindRose.Location = new System.Drawing.Point(17, 450);
            this.plotInputWindRose.Name = "plotInputWindRose";
            this.plotInputWindRose.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotInputWindRose.Size = new System.Drawing.Size(284, 235);
            this.plotInputWindRose.TabIndex = 127;
            this.plotInputWindRose.Text = "plotView1";
            this.plotInputWindRose.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotInputWindRose.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotInputWindRose.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // chkCreateTurbTS
            // 
            this.chkCreateTurbTS.AutoSize = true;
            this.chkCreateTurbTS.Checked = true;
            this.chkCreateTurbTS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateTurbTS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCreateTurbTS.Location = new System.Drawing.Point(591, 186);
            this.chkCreateTurbTS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkCreateTurbTS.Name = "chkCreateTurbTS";
            this.chkCreateTurbTS.Size = new System.Drawing.Size(137, 22);
            this.chkCreateTurbTS.TabIndex = 125;
            this.chkCreateTurbTS.Text = "Create Time Series";
            this.chkCreateTurbTS.UseVisualStyleBackColor = true;
            this.chkCreateTurbTS.CheckedChanged += new System.EventHandler(this.cboCreateTurbTS_CheckedChanged);
            // 
            // btnEditModHeight
            // 
            this.btnEditModHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditModHeight.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditModHeight.Location = new System.Drawing.Point(719, 519);
            this.btnEditModHeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditModHeight.Name = "btnEditModHeight";
            this.btnEditModHeight.Size = new System.Drawing.Size(50, 32);
            this.btnEditModHeight.TabIndex = 124;
            this.btnEditModHeight.Text = "Edit";
            this.btnEditModHeight.UseVisualStyleBackColor = true;
            this.btnEditModHeight.Click += new System.EventHandler(this.btnModHeight_Click);
            // 
            // label168
            // 
            this.label168.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label168.AutoSize = true;
            this.label168.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label168.Location = new System.Drawing.Point(714, 442);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(62, 36);
            this.label168.TabIndex = 123;
            this.label168.Text = "Modeled \r\nHeight:";
            this.label168.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtModeledHeight
            // 
            this.txtModeledHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtModeledHeight.Enabled = false;
            this.txtModeledHeight.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModeledHeight.Location = new System.Drawing.Point(719, 487);
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
            this.btnImportRoughness.Location = new System.Drawing.Point(17, 130);
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
            // txtUTMZone
            // 
            this.txtUTMZone.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUTMZone.Location = new System.Drawing.Point(230, 217);
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
            this.Label57.Location = new System.Drawing.Point(223, 195);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(74, 18);
            this.Label57.TabIndex = 104;
            this.Label57.Text = "UTM Zone:";
            // 
            // txtUTMDatum
            // 
            this.txtUTMDatum.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUTMDatum.Location = new System.Drawing.Point(97, 219);
            this.txtUTMDatum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUTMDatum.Name = "txtUTMDatum";
            this.txtUTMDatum.ReadOnly = true;
            this.txtUTMDatum.Size = new System.Drawing.Size(122, 22);
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
            this.btnGenTurbEsts.Location = new System.Drawing.Point(568, 132);
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
            this.lstTurbines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lstTurbines.Size = new System.Drawing.Size(292, 175);
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
            this.lstMetTowers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstMetTowers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.metName,
            this.Lats,
            this.Longs});
            this.lstMetTowers.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMetTowers.FullRowSelect = true;
            this.lstMetTowers.HideSelection = false;
            this.lstMetTowers.Location = new System.Drawing.Point(17, 264);
            this.lstMetTowers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstMetTowers.Name = "lstMetTowers";
            this.lstMetTowers.Size = new System.Drawing.Size(462, 159);
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
            // pgeMetDataTS
            // 
            this.pgeMetDataTS.Controls.Add(this.splContMetTS);
            this.pgeMetDataTS.Location = new System.Drawing.Point(4, 27);
            this.pgeMetDataTS.Name = "pgeMetDataTS";
            this.pgeMetDataTS.Size = new System.Drawing.Size(1488, 712);
            this.pgeMetDataTS.TabIndex = 21;
            this.pgeMetDataTS.Text = "Met Data Time Series";
            this.pgeMetDataTS.UseVisualStyleBackColor = true;
            // 
            // splContMetTS
            // 
            this.splContMetTS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splContMetTS.Location = new System.Drawing.Point(0, 0);
            this.splContMetTS.Name = "splContMetTS";
            // 
            // splContMetTS.Panel1
            // 
            this.splContMetTS.Panel1.AutoScroll = true;
            this.splContMetTS.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splContMetTS.Panel1.Controls.Add(this.dataMetTS);
            this.splContMetTS.Panel1.Controls.Add(this.chkTS_Params);
            this.splContMetTS.Panel1.Controls.Add(this.treeDataParams);
            this.splContMetTS.Panel1.Controls.Add(this.chkMetsTS);
            this.splContMetTS.Panel1.Controls.Add(this.btnShowFilterFlags);
            this.splContMetTS.Panel1.Controls.Add(this.label146);
            // 
            // splContMetTS.Panel2
            // 
            this.splContMetTS.Panel2.AutoScroll = true;
            this.splContMetTS.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splContMetTS.Panel2.Controls.Add(this.chkShowFilteredData);
            this.splContMetTS.Panel2.Controls.Add(this.chkShowLegenMetDataTS);
            this.splContMetTS.Panel2.Controls.Add(this.plotTS_Baros);
            this.splContMetTS.Panel2.Controls.Add(this.label213);
            this.splContMetTS.Panel2.Controls.Add(this.cboPlot4Type);
            this.splContMetTS.Panel2.Controls.Add(this.label212);
            this.splContMetTS.Panel2.Controls.Add(this.cboPlot3Type);
            this.splContMetTS.Panel2.Controls.Add(this.label211);
            this.splContMetTS.Panel2.Controls.Add(this.cboPlot2Type);
            this.splContMetTS.Panel2.Controls.Add(this.label210);
            this.splContMetTS.Panel2.Controls.Add(this.cboPlot1Type);
            this.splContMetTS.Panel2.Controls.Add(this.label209);
            this.splContMetTS.Panel2.Controls.Add(this.cboNumPlots);
            this.splContMetTS.Panel2.Controls.Add(this.lblMetDataTS_Inc);
            this.splContMetTS.Panel2.Controls.Add(this.txtNumDaysTS);
            this.splContMetTS.Panel2.Controls.Add(this.btnMetTS_Left);
            this.splContMetTS.Panel2.Controls.Add(this.btnMetTS_Right);
            this.splContMetTS.Panel2.Controls.Add(this.label208);
            this.splContMetTS.Panel2.Controls.Add(this.label149);
            this.splContMetTS.Panel2.Controls.Add(this.dateMetTS_End);
            this.splContMetTS.Panel2.Controls.Add(this.dateMetTS_Start);
            this.splContMetTS.Panel2.Controls.Add(this.plotTS_Temp);
            this.splContMetTS.Panel2.Controls.Add(this.plotTS_Vanes);
            this.splContMetTS.Panel2.Controls.Add(this.plotTS_Anems);
            this.splContMetTS.Size = new System.Drawing.Size(1488, 712);
            this.splContMetTS.SplitterDistance = 532;
            this.splContMetTS.SplitterWidth = 10;
            this.splContMetTS.TabIndex = 5;
            // 
            // dataMetTS
            // 
            this.dataMetTS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataMetTS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataMetTS.Location = new System.Drawing.Point(17, 182);
            this.dataMetTS.Name = "dataMetTS";
            this.dataMetTS.RowHeadersWidth = 51;
            this.dataMetTS.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataMetTS.Size = new System.Drawing.Size(497, 511);
            this.dataMetTS.TabIndex = 34;
            // 
            // chkTS_Params
            // 
            this.chkTS_Params.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTS_Params.CheckOnClick = true;
            this.chkTS_Params.ColumnWidth = 200;
            this.chkTS_Params.FormattingEnabled = true;
            this.chkTS_Params.HorizontalScrollbar = true;
            this.chkTS_Params.Location = new System.Drawing.Point(311, 72);
            this.chkTS_Params.MultiColumn = true;
            this.chkTS_Params.Name = "chkTS_Params";
            this.chkTS_Params.Size = new System.Drawing.Size(203, 104);
            this.chkTS_Params.TabIndex = 33;
            this.chkTS_Params.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkTS_Params_ItemCheck);
            // 
            // treeDataParams
            // 
            this.treeDataParams.CheckBoxes = true;
            this.treeDataParams.Location = new System.Drawing.Point(184, 72);
            this.treeDataParams.Name = "treeDataParams";
            treeNode1.Checked = true;
            treeNode1.Name = "nodeAnemAvg";
            treeNode1.Text = "Average";
            treeNode2.Name = "nodeAnemSD";
            treeNode2.Text = "St. Dev.";
            treeNode3.Name = "nodeAnemMin";
            treeNode3.Text = "Minimum";
            treeNode4.Name = "nodeAnemMax";
            treeNode4.Text = "Maximum";
            treeNode5.Name = "nodeAlpha";
            treeNode5.Text = "Alpha";
            treeNode6.Name = "nodeExtrapWS";
            treeNode6.Text = "Extrap. WS";
            treeNode7.Checked = true;
            treeNode7.Name = "nodeAnems";
            treeNode7.Text = "Anems";
            treeNode8.Name = "nodeVaneAvg";
            treeNode8.Text = "Average";
            treeNode9.Name = "nodeVaneSD";
            treeNode9.Text = "St. Dev.";
            treeNode10.Name = "nodeVaneMax";
            treeNode10.Text = "Minimum";
            treeNode11.Name = "nodeVaneMax";
            treeNode11.Text = "Maximum";
            treeNode12.Name = "nodeVanes";
            treeNode12.Text = "Vanes";
            treeNode13.Name = "Node0";
            treeNode13.Text = "Average";
            treeNode14.Name = "Node1";
            treeNode14.Text = "St. Dev.";
            treeNode15.Name = "Node2";
            treeNode15.Text = "Minimum";
            treeNode16.Name = "Node3";
            treeNode16.Text = "Maximum";
            treeNode17.Name = "nodeTemps";
            treeNode17.Text = "Temps.";
            treeNode18.Name = "Node4";
            treeNode18.Text = "Average";
            treeNode19.Name = "Node5";
            treeNode19.Text = "St. Dev.";
            treeNode20.Name = "Node6";
            treeNode20.Text = "Minimum";
            treeNode21.Name = "Node7";
            treeNode21.Text = "Maximum";
            treeNode22.Name = "nodeBaros";
            treeNode22.Text = "Baros.";
            this.treeDataParams.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode12,
            treeNode17,
            treeNode22});
            this.treeDataParams.Size = new System.Drawing.Size(121, 104);
            this.treeDataParams.TabIndex = 32;
            this.treeDataParams.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeDataParams_AfterCheck);
            // 
            // chkMetsTS
            // 
            this.chkMetsTS.CheckOnClick = true;
            this.chkMetsTS.FormattingEnabled = true;
            this.chkMetsTS.HorizontalScrollbar = true;
            this.chkMetsTS.Location = new System.Drawing.Point(17, 72);
            this.chkMetsTS.Name = "chkMetsTS";
            this.chkMetsTS.Size = new System.Drawing.Size(161, 104);
            this.chkMetsTS.TabIndex = 31;
            this.chkMetsTS.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkMetsTS_ItemCheck);
            // 
            // btnShowFilterFlags
            // 
            this.btnShowFilterFlags.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnShowFilterFlags.Location = new System.Drawing.Point(450, 10);
            this.btnShowFilterFlags.Name = "btnShowFilterFlags";
            this.btnShowFilterFlags.Size = new System.Drawing.Size(66, 43);
            this.btnShowFilterFlags.TabIndex = 8;
            this.btnShowFilterFlags.Text = "Flag Legend";
            this.btnShowFilterFlags.UseVisualStyleBackColor = true;
            this.btnShowFilterFlags.Click += new System.EventHandler(this.btnShowFilterFlags_Click);
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label146.Location = new System.Drawing.Point(12, 16);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(222, 25);
            this.label146.TabIndex = 6;
            this.label146.Text = "Met Data Time Series";
            // 
            // chkShowFilteredData
            // 
            this.chkShowFilteredData.AutoSize = true;
            this.chkShowFilteredData.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.chkShowFilteredData.Location = new System.Drawing.Point(142, 47);
            this.chkShowFilteredData.Name = "chkShowFilteredData";
            this.chkShowFilteredData.Size = new System.Drawing.Size(131, 21);
            this.chkShowFilteredData.TabIndex = 26;
            this.chkShowFilteredData.Text = "Show Filtered data";
            this.chkShowFilteredData.UseVisualStyleBackColor = true;
            this.chkShowFilteredData.CheckedChanged += new System.EventHandler(this.chkShowFilteredData_CheckedChanged);
            // 
            // chkShowLegenMetDataTS
            // 
            this.chkShowLegenMetDataTS.AutoSize = true;
            this.chkShowLegenMetDataTS.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.chkShowLegenMetDataTS.Location = new System.Drawing.Point(28, 47);
            this.chkShowLegenMetDataTS.Name = "chkShowLegenMetDataTS";
            this.chkShowLegenMetDataTS.Size = new System.Drawing.Size(108, 21);
            this.chkShowLegenMetDataTS.TabIndex = 25;
            this.chkShowLegenMetDataTS.Text = "Show Legends";
            this.chkShowLegenMetDataTS.UseVisualStyleBackColor = true;
            this.chkShowLegenMetDataTS.CheckedChanged += new System.EventHandler(this.chkShowLegenMetDataTS_CheckedChanged);
            // 
            // plotTS_Baros
            // 
            this.plotTS_Baros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotTS_Baros.BackColor = System.Drawing.Color.Gainsboro;
            this.plotTS_Baros.Location = new System.Drawing.Point(28, 537);
            this.plotTS_Baros.Name = "plotTS_Baros";
            this.plotTS_Baros.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTS_Baros.Size = new System.Drawing.Size(770, 155);
            this.plotTS_Baros.TabIndex = 24;
            this.plotTS_Baros.Text = "plotTS_Anems";
            this.plotTS_Baros.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTS_Baros.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTS_Baros.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // label213
            // 
            this.label213.AutoSize = true;
            this.label213.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label213.Location = new System.Drawing.Point(830, 44);
            this.label213.Name = "label213";
            this.label213.Size = new System.Drawing.Size(19, 18);
            this.label213.TabIndex = 23;
            this.label213.Text = "4)";
            this.label213.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboPlot4Type
            // 
            this.cboPlot4Type.FormattingEnabled = true;
            this.cboPlot4Type.Items.AddRange(new object[] {
            "WS",
            "WD",
            "Temp.",
            "Press."});
            this.cboPlot4Type.Location = new System.Drawing.Point(855, 40);
            this.cboPlot4Type.Name = "cboPlot4Type";
            this.cboPlot4Type.Size = new System.Drawing.Size(76, 26);
            this.cboPlot4Type.TabIndex = 22;
            this.cboPlot4Type.SelectedIndexChanged += new System.EventHandler(this.cboPlot4Type_SelectedIndexChanged);
            // 
            // label212
            // 
            this.label212.AutoSize = true;
            this.label212.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label212.Location = new System.Drawing.Point(833, 12);
            this.label212.Name = "label212";
            this.label212.Size = new System.Drawing.Size(19, 18);
            this.label212.TabIndex = 21;
            this.label212.Text = "3)";
            this.label212.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboPlot3Type
            // 
            this.cboPlot3Type.FormattingEnabled = true;
            this.cboPlot3Type.Items.AddRange(new object[] {
            "WS",
            "WD",
            "Temp.",
            "Press."});
            this.cboPlot3Type.Location = new System.Drawing.Point(854, 8);
            this.cboPlot3Type.Name = "cboPlot3Type";
            this.cboPlot3Type.Size = new System.Drawing.Size(76, 26);
            this.cboPlot3Type.TabIndex = 20;
            this.cboPlot3Type.SelectedIndexChanged += new System.EventHandler(this.cboPlot3Type_SelectedIndexChanged);
            // 
            // label211
            // 
            this.label211.AutoSize = true;
            this.label211.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label211.Location = new System.Drawing.Point(726, 44);
            this.label211.Name = "label211";
            this.label211.Size = new System.Drawing.Size(19, 18);
            this.label211.TabIndex = 19;
            this.label211.Text = "2)";
            this.label211.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboPlot2Type
            // 
            this.cboPlot2Type.FormattingEnabled = true;
            this.cboPlot2Type.Items.AddRange(new object[] {
            "WS",
            "WD",
            "Temp.",
            "Press."});
            this.cboPlot2Type.Location = new System.Drawing.Point(749, 40);
            this.cboPlot2Type.Name = "cboPlot2Type";
            this.cboPlot2Type.Size = new System.Drawing.Size(76, 26);
            this.cboPlot2Type.TabIndex = 18;
            this.cboPlot2Type.SelectedIndexChanged += new System.EventHandler(this.cboPlot2Type_SelectedIndexChanged);
            // 
            // label210
            // 
            this.label210.AutoSize = true;
            this.label210.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label210.Location = new System.Drawing.Point(726, 12);
            this.label210.Name = "label210";
            this.label210.Size = new System.Drawing.Size(19, 18);
            this.label210.TabIndex = 17;
            this.label210.Text = "1)";
            this.label210.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboPlot1Type
            // 
            this.cboPlot1Type.FormattingEnabled = true;
            this.cboPlot1Type.Items.AddRange(new object[] {
            "WS",
            "WD",
            "Temp.",
            "Press."});
            this.cboPlot1Type.Location = new System.Drawing.Point(749, 8);
            this.cboPlot1Type.Name = "cboPlot1Type";
            this.cboPlot1Type.Size = new System.Drawing.Size(76, 26);
            this.cboPlot1Type.TabIndex = 16;
            this.cboPlot1Type.SelectedIndexChanged += new System.EventHandler(this.cboPlot1Type_SelectedIndexChanged);
            // 
            // label209
            // 
            this.label209.AutoSize = true;
            this.label209.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label209.Location = new System.Drawing.Point(641, 15);
            this.label209.Name = "label209";
            this.label209.Size = new System.Drawing.Size(75, 18);
            this.label209.TabIndex = 15;
            this.label209.Text = "Num. Plots";
            this.label209.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboNumPlots
            // 
            this.cboNumPlots.FormattingEnabled = true;
            this.cboNumPlots.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cboNumPlots.Location = new System.Drawing.Point(644, 36);
            this.cboNumPlots.Name = "cboNumPlots";
            this.cboNumPlots.Size = new System.Drawing.Size(63, 26);
            this.cboNumPlots.TabIndex = 14;
            this.cboNumPlots.SelectedIndexChanged += new System.EventHandler(this.cboNumPlots_SelectedIndexChanged);
            // 
            // lblMetDataTS_Inc
            // 
            this.lblMetDataTS_Inc.AutoSize = true;
            this.lblMetDataTS_Inc.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetDataTS_Inc.Location = new System.Drawing.Point(523, 22);
            this.lblMetDataTS_Inc.Name = "lblMetDataTS_Inc";
            this.lblMetDataTS_Inc.Size = new System.Drawing.Size(38, 18);
            this.lblMetDataTS_Inc.TabIndex = 13;
            this.lblMetDataTS_Inc.Text = "Days";
            this.lblMetDataTS_Inc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtNumDaysTS
            // 
            this.txtNumDaysTS.Location = new System.Drawing.Point(458, 18);
            this.txtNumDaysTS.Name = "txtNumDaysTS";
            this.txtNumDaysTS.Size = new System.Drawing.Size(59, 25);
            this.txtNumDaysTS.TabIndex = 12;
            // 
            // btnMetTS_Left
            // 
            this.btnMetTS_Left.BackColor = System.Drawing.Color.LightCyan;
            this.btnMetTS_Left.Image = ((System.Drawing.Image)(resources.GetObject("btnMetTS_Left.Image")));
            this.btnMetTS_Left.Location = new System.Drawing.Point(397, 10);
            this.btnMetTS_Left.Name = "btnMetTS_Left";
            this.btnMetTS_Left.Size = new System.Drawing.Size(55, 40);
            this.btnMetTS_Left.TabIndex = 11;
            this.btnMetTS_Left.UseVisualStyleBackColor = false;
            this.btnMetTS_Left.Click += new System.EventHandler(this.btnMetTS_Left_Click);
            // 
            // btnMetTS_Right
            // 
            this.btnMetTS_Right.BackColor = System.Drawing.Color.LightCyan;
            this.btnMetTS_Right.Image = ((System.Drawing.Image)(resources.GetObject("btnMetTS_Right.Image")));
            this.btnMetTS_Right.Location = new System.Drawing.Point(567, 13);
            this.btnMetTS_Right.Name = "btnMetTS_Right";
            this.btnMetTS_Right.Size = new System.Drawing.Size(55, 40);
            this.btnMetTS_Right.TabIndex = 10;
            this.btnMetTS_Right.UseVisualStyleBackColor = false;
            this.btnMetTS_Right.Click += new System.EventHandler(this.btnMetTS_Right_Click);
            // 
            // label208
            // 
            this.label208.AutoSize = true;
            this.label208.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label208.Location = new System.Drawing.Point(214, 18);
            this.label208.Name = "label208";
            this.label208.Size = new System.Drawing.Size(23, 18);
            this.label208.TabIndex = 9;
            this.label208.Text = "To";
            this.label208.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label149.Location = new System.Drawing.Point(14, 18);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(40, 18);
            this.label149.TabIndex = 8;
            this.label149.Text = "From";
            this.label149.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dateMetTS_End
            // 
            this.dateMetTS_End.CustomFormat = "MM/dd/yy HH:mm";
            this.dateMetTS_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMetTS_End.Location = new System.Drawing.Point(244, 15);
            this.dateMetTS_End.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateMetTS_End.Name = "dateMetTS_End";
            this.dateMetTS_End.Size = new System.Drawing.Size(140, 25);
            this.dateMetTS_End.TabIndex = 5;
            this.dateMetTS_End.ValueChanged += new System.EventHandler(this.dateMetTS_End_ValueChanged);
            // 
            // dateMetTS_Start
            // 
            this.dateMetTS_Start.CustomFormat = "MM/dd/yy HH:mm";
            this.dateMetTS_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMetTS_Start.Location = new System.Drawing.Point(58, 15);
            this.dateMetTS_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateMetTS_Start.Name = "dateMetTS_Start";
            this.dateMetTS_Start.Size = new System.Drawing.Size(140, 25);
            this.dateMetTS_Start.TabIndex = 4;
            this.dateMetTS_Start.ValueChanged += new System.EventHandler(this.dateMetTS_Start_ValueChanged);
            // 
            // plotTS_Temp
            // 
            this.plotTS_Temp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotTS_Temp.BackColor = System.Drawing.Color.Gainsboro;
            this.plotTS_Temp.Location = new System.Drawing.Point(28, 382);
            this.plotTS_Temp.Name = "plotTS_Temp";
            this.plotTS_Temp.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTS_Temp.Size = new System.Drawing.Size(770, 155);
            this.plotTS_Temp.TabIndex = 2;
            this.plotTS_Temp.Text = "plotTS_Anems";
            this.plotTS_Temp.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTS_Temp.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTS_Temp.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotTS_Vanes
            // 
            this.plotTS_Vanes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotTS_Vanes.BackColor = System.Drawing.Color.Gainsboro;
            this.plotTS_Vanes.Location = new System.Drawing.Point(28, 227);
            this.plotTS_Vanes.Name = "plotTS_Vanes";
            this.plotTS_Vanes.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTS_Vanes.Size = new System.Drawing.Size(770, 155);
            this.plotTS_Vanes.TabIndex = 1;
            this.plotTS_Vanes.Text = "plotTS_Anems";
            this.plotTS_Vanes.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTS_Vanes.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTS_Vanes.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotTS_Anems
            // 
            this.plotTS_Anems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotTS_Anems.BackColor = System.Drawing.Color.Gainsboro;
            this.plotTS_Anems.Location = new System.Drawing.Point(28, 72);
            this.plotTS_Anems.Name = "plotTS_Anems";
            this.plotTS_Anems.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTS_Anems.Size = new System.Drawing.Size(770, 155);
            this.plotTS_Anems.TabIndex = 0;
            this.plotTS_Anems.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTS_Anems.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTS_Anems.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // pgeMetData
            // 
            this.pgeMetData.Controls.Add(this.label102);
            this.pgeMetData.Controls.Add(this.label69);
            this.pgeMetData.Controls.Add(this.pnlMetDatQC_WSDiff);
            this.pgeMetData.Controls.Add(this.pnlMetDataQC_Scatter);
            this.pgeMetData.Controls.Add(this.label70);
            this.pgeMetData.Controls.Add(this.plotWSDiffByWD);
            this.pgeMetData.Controls.Add(this.txtShearCalcMethod);
            this.pgeMetData.Controls.Add(this.label227);
            this.pgeMetData.Controls.Add(this.btnEditShearMethod);
            this.pgeMetData.Controls.Add(this.label226);
            this.pgeMetData.Controls.Add(this.txtShearBestFitMaxHeight);
            this.pgeMetData.Controls.Add(this.txtShearBestFitMinHeight);
            this.pgeMetData.Controls.Add(this.label225);
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
            this.pgeMetData.Size = new System.Drawing.Size(1488, 712);
            this.pgeMetData.TabIndex = 14;
            this.pgeMetData.Text = "Met Data QC";
            this.pgeMetData.UseVisualStyleBackColor = true;
            // 
            // label102
            // 
            this.label102.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label102.AutoSize = true;
            this.label102.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.Location = new System.Drawing.Point(897, 438);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(346, 23);
            this.label102.TabIndex = 255;
            this.label102.Text = "Anemometer WS Diff. vs. Wind Speed";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(26, 438);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(227, 23);
            this.label69.TabIndex = 254;
            this.label69.Text = "Wind Speed Scatterplot";
            // 
            // pnlMetDatQC_WSDiff
            // 
            this.pnlMetDatQC_WSDiff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMetDatQC_WSDiff.AutoSize = true;
            this.pnlMetDatQC_WSDiff.Controls.Add(this.plotWSDiffByWS);
            this.pnlMetDatQC_WSDiff.Location = new System.Drawing.Point(886, 466);
            this.pnlMetDatQC_WSDiff.MaximumSize = new System.Drawing.Size(700, 700);
            this.pnlMetDatQC_WSDiff.Name = "pnlMetDatQC_WSDiff";
            this.pnlMetDatQC_WSDiff.Size = new System.Drawing.Size(437, 237);
            this.pnlMetDatQC_WSDiff.TabIndex = 253;
            this.pnlMetDatQC_WSDiff.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMetDatQC_WSDiff_Paint);
            // 
            // plotWSDiffByWS
            // 
            this.plotWSDiffByWS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotWSDiffByWS.Location = new System.Drawing.Point(0, 0);
            this.plotWSDiffByWS.Margin = new System.Windows.Forms.Padding(5);
            this.plotWSDiffByWS.MaximumSize = new System.Drawing.Size(600, 500);
            this.plotWSDiffByWS.Name = "plotWSDiffByWS";
            this.plotWSDiffByWS.Padding = new System.Windows.Forms.Padding(1);
            this.plotWSDiffByWS.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotWSDiffByWS.Size = new System.Drawing.Size(437, 237);
            this.plotWSDiffByWS.TabIndex = 226;
            this.plotWSDiffByWS.Text = "plotView2";
            this.plotWSDiffByWS.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotWSDiffByWS.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotWSDiffByWS.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // pnlMetDataQC_Scatter
            // 
            this.pnlMetDataQC_Scatter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMetDataQC_Scatter.AutoSize = true;
            this.pnlMetDataQC_Scatter.Controls.Add(this.plotAnemScatter);
            this.pnlMetDataQC_Scatter.Location = new System.Drawing.Point(18, 466);
            this.pnlMetDataQC_Scatter.MaximumSize = new System.Drawing.Size(600, 700);
            this.pnlMetDataQC_Scatter.Name = "pnlMetDataQC_Scatter";
            this.pnlMetDataQC_Scatter.Size = new System.Drawing.Size(433, 242);
            this.pnlMetDataQC_Scatter.TabIndex = 252;
            // 
            // plotAnemScatter
            // 
            this.plotAnemScatter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotAnemScatter.Location = new System.Drawing.Point(0, 0);
            this.plotAnemScatter.Margin = new System.Windows.Forms.Padding(5);
            this.plotAnemScatter.Name = "plotAnemScatter";
            this.plotAnemScatter.Padding = new System.Windows.Forms.Padding(1);
            this.plotAnemScatter.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotAnemScatter.Size = new System.Drawing.Size(433, 242);
            this.plotAnemScatter.TabIndex = 250;
            this.plotAnemScatter.Text = "plotView1";
            this.plotAnemScatter.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotAnemScatter.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotAnemScatter.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotAnemScatter.Click += new System.EventHandler(this.plotAnemScatter_Click_1);
            // 
            // label70
            // 
            this.label70.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(477, 438);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(368, 23);
            this.label70.TabIndex = 251;
            this.label70.Text = "Anemometer WS Diff. vs. Wind Direction";
            // 
            // plotWSDiffByWD
            // 
            this.plotWSDiffByWD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.plotWSDiffByWD.Location = new System.Drawing.Point(456, 466);
            this.plotWSDiffByWD.Margin = new System.Windows.Forms.Padding(5);
            this.plotWSDiffByWD.MaximumSize = new System.Drawing.Size(600, 500);
            this.plotWSDiffByWD.Name = "plotWSDiffByWD";
            this.plotWSDiffByWD.Padding = new System.Windows.Forms.Padding(1);
            this.plotWSDiffByWD.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotWSDiffByWD.Size = new System.Drawing.Size(420, 237);
            this.plotWSDiffByWD.TabIndex = 250;
            this.plotWSDiffByWD.Text = "plotView1";
            this.plotWSDiffByWD.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotWSDiffByWD.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotWSDiffByWD.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // txtShearCalcMethod
            // 
            this.txtShearCalcMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShearCalcMethod.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.txtShearCalcMethod.Location = new System.Drawing.Point(827, 39);
            this.txtShearCalcMethod.Name = "txtShearCalcMethod";
            this.txtShearCalcMethod.ReadOnly = true;
            this.txtShearCalcMethod.Size = new System.Drawing.Size(161, 24);
            this.txtShearCalcMethod.TabIndex = 245;
            // 
            // label227
            // 
            this.label227.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label227.AutoSize = true;
            this.label227.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label227.Location = new System.Drawing.Point(825, 76);
            this.label227.Name = "label227";
            this.label227.Size = new System.Drawing.Size(38, 17);
            this.label227.TabIndex = 244;
            this.label227.Text = "From";
            // 
            // btnEditShearMethod
            // 
            this.btnEditShearMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditShearMethod.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnEditShearMethod.Location = new System.Drawing.Point(998, 39);
            this.btnEditShearMethod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditShearMethod.Name = "btnEditShearMethod";
            this.btnEditShearMethod.Size = new System.Drawing.Size(46, 25);
            this.btnEditShearMethod.TabIndex = 243;
            this.btnEditShearMethod.Text = "Edit";
            this.btnEditShearMethod.UseVisualStyleBackColor = true;
            this.btnEditShearMethod.Click += new System.EventHandler(this.btnEditShearMethod_Click);
            // 
            // label226
            // 
            this.label226.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label226.AutoSize = true;
            this.label226.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label226.Location = new System.Drawing.Point(920, 76);
            this.label226.Name = "label226";
            this.label226.Size = new System.Drawing.Size(19, 17);
            this.label226.TabIndex = 242;
            this.label226.Text = "to";
            // 
            // txtShearBestFitMaxHeight
            // 
            this.txtShearBestFitMaxHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShearBestFitMaxHeight.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.txtShearBestFitMaxHeight.Location = new System.Drawing.Point(941, 73);
            this.txtShearBestFitMaxHeight.Name = "txtShearBestFitMaxHeight";
            this.txtShearBestFitMaxHeight.ReadOnly = true;
            this.txtShearBestFitMaxHeight.Size = new System.Drawing.Size(47, 24);
            this.txtShearBestFitMaxHeight.TabIndex = 241;
            // 
            // txtShearBestFitMinHeight
            // 
            this.txtShearBestFitMinHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShearBestFitMinHeight.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.txtShearBestFitMinHeight.Location = new System.Drawing.Point(868, 73);
            this.txtShearBestFitMinHeight.Name = "txtShearBestFitMinHeight";
            this.txtShearBestFitMinHeight.ReadOnly = true;
            this.txtShearBestFitMinHeight.Size = new System.Drawing.Size(50, 24);
            this.txtShearBestFitMinHeight.TabIndex = 240;
            // 
            // label225
            // 
            this.label225.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label225.AutoSize = true;
            this.label225.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label225.Location = new System.Drawing.Point(825, 15);
            this.label225.Name = "label225";
            this.label225.Size = new System.Drawing.Size(151, 17);
            this.label225.TabIndex = 238;
            this.label225.Text = "Shear Calculation Method:";
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
            // chkMaxWS_Range
            // 
            this.chkMaxWS_Range.AutoSize = true;
            this.chkMaxWS_Range.Checked = true;
            this.chkMaxWS_Range.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaxWS_Range.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMaxWS_Range.Location = new System.Drawing.Point(705, 402);
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
            this.chkMaxWS_SD.Location = new System.Drawing.Point(705, 380);
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
            this.chkMinWS_SD.Location = new System.Drawing.Point(705, 359);
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
            this.chkMinWS.Location = new System.Drawing.Point(705, 337);
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
            this.chkIcing.Location = new System.Drawing.Point(705, 314);
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
            this.chkTowerShadow.Location = new System.Drawing.Point(705, 293);
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
            this.cboSelVane.Location = new System.Drawing.Point(400, 392);
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
            this.label184.Location = new System.Drawing.Point(355, 396);
            this.label184.Name = "label184";
            this.label184.Size = new System.Drawing.Size(38, 18);
            this.label184.TabIndex = 229;
            this.label184.Text = "Vane";
            // 
            // cboAnemB
            // 
            this.cboAnemB.FormattingEnabled = true;
            this.cboAnemB.Location = new System.Drawing.Point(254, 392);
            this.cboAnemB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboAnemB.Name = "cboAnemB";
            this.cboAnemB.Size = new System.Drawing.Size(79, 26);
            this.cboAnemB.TabIndex = 228;
            this.cboAnemB.SelectedIndexChanged += new System.EventHandler(this.cboAnemB_SelectedIndexChanged);
            // 
            // label155
            // 
            this.label155.AutoSize = true;
            this.label155.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label155.Location = new System.Drawing.Point(193, 397);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(55, 18);
            this.label155.TabIndex = 227;
            this.label155.Text = "Anem B";
            // 
            // cboAnemA
            // 
            this.cboAnemA.FormattingEnabled = true;
            this.cboAnemA.Location = new System.Drawing.Point(98, 392);
            this.cboAnemA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboAnemA.Name = "cboAnemA";
            this.cboAnemA.Size = new System.Drawing.Size(79, 26);
            this.cboAnemA.TabIndex = 226;
            this.cboAnemA.SelectedIndexChanged += new System.EventHandler(this.cboAnemA_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(36, 396);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(57, 18);
            this.label20.TabIndex = 225;
            this.label20.Text = "Anem A";
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // plotAlphaByWD
            // 
            this.plotAlphaByWD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.plotAlphaByWD.Location = new System.Drawing.Point(1050, 286);
            this.plotAlphaByWD.MaximumSize = new System.Drawing.Size(500, 200);
            this.plotAlphaByWD.Name = "plotAlphaByWD";
            this.plotAlphaByWD.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotAlphaByWD.Size = new System.Drawing.Size(424, 140);
            this.plotAlphaByWD.TabIndex = 221;
            this.plotAlphaByWD.Text = "plotView1";
            this.plotAlphaByWD.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotAlphaByWD.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotAlphaByWD.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotAlphaByWD.Click += new System.EventHandler(this.plotAlphaByWD_Click);
            // 
            // plotMetQC_WindRose
            // 
            this.plotMetQC_WindRose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.plotMetQC_WindRose.Location = new System.Drawing.Point(1249, 39);
            this.plotMetQC_WindRose.Name = "plotMetQC_WindRose";
            this.plotMetQC_WindRose.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMetQC_WindRose.Size = new System.Drawing.Size(229, 212);
            this.plotMetQC_WindRose.TabIndex = 220;
            this.plotMetQC_WindRose.Text = "plotView1";
            this.plotMetQC_WindRose.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMetQC_WindRose.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMetQC_WindRose.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotWS_vsHeight
            // 
            this.plotWS_vsHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.plotWS_vsHeight.Location = new System.Drawing.Point(1050, 39);
            this.plotWS_vsHeight.Name = "plotWS_vsHeight";
            this.plotWS_vsHeight.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotWS_vsHeight.Size = new System.Drawing.Size(193, 212);
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
            this.chkDisableFilter.Location = new System.Drawing.Point(608, 389);
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
            this.btnViewFilters.Location = new System.Drawing.Point(717, 257);
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
            this.Export_End.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Export_End.CustomFormat = "MM/dd/yy HH:mm";
            this.Export_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Export_End.Location = new System.Drawing.Point(1329, 494);
            this.Export_End.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Export_End.Name = "Export_End";
            this.Export_End.Size = new System.Drawing.Size(140, 25);
            this.Export_End.TabIndex = 214;
            // 
            // label105
            // 
            this.label105.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label105.AutoSize = true;
            this.label105.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.Location = new System.Drawing.Point(1326, 474);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(77, 18);
            this.label105.TabIndex = 213;
            this.label105.Text = "Export End:";
            // 
            // Export_Start
            // 
            this.Export_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Export_Start.CustomFormat = "MM/dd/yy HH:mm";
            this.Export_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Export_Start.Location = new System.Drawing.Point(1329, 448);
            this.Export_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Export_Start.Name = "Export_Start";
            this.Export_Start.Size = new System.Drawing.Size(140, 25);
            this.Export_Start.TabIndex = 212;
            // 
            // btnExportAnnualMax
            // 
            this.btnExportAnnualMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAnnualMax.Location = new System.Drawing.Point(1321, 656);
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
            this.btnExportExtrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportExtrap.Location = new System.Drawing.Point(1321, 614);
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
            this.btnExportAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAlpha.Location = new System.Drawing.Point(1321, 571);
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
            this.btnExportFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportFlags.Location = new System.Drawing.Point(1321, 529);
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
            this.label104.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label104.AutoSize = true;
            this.label104.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.Location = new System.Drawing.Point(1326, 428);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(84, 18);
            this.label104.TabIndex = 207;
            this.label104.Text = "Export Start:";
            // 
            // cboMetQC_SelectedMet
            // 
            this.cboMetQC_SelectedMet.FormattingEnabled = true;
            this.cboMetQC_SelectedMet.Location = new System.Drawing.Point(30, 38);
            this.cboMetQC_SelectedMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMetQC_SelectedMet.Name = "cboMetQC_SelectedMet";
            this.cboMetQC_SelectedMet.Size = new System.Drawing.Size(270, 26);
            this.cboMetQC_SelectedMet.TabIndex = 206;
            this.cboMetQC_SelectedMet.SelectedIndexChanged += new System.EventHandler(this.cboMetQC_SelectedMet_SelectedIndexChanged);
            // 
            // cboFilt_or_Not
            // 
            this.cboFilt_or_Not.FormattingEnabled = true;
            this.cboFilt_or_Not.Items.AddRange(new object[] {
            "Unfiltered",
            "Filtered"});
            this.cboFilt_or_Not.Location = new System.Drawing.Point(484, 398);
            this.cboFilt_or_Not.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboFilt_or_Not.Name = "cboFilt_or_Not";
            this.cboFilt_or_Not.Size = new System.Drawing.Size(101, 26);
            this.cboFilt_or_Not.TabIndex = 205;
            this.cboFilt_or_Not.Text = "Unfiltered";
            this.cboFilt_or_Not.SelectedIndexChanged += new System.EventHandler(this.cboFilt_or_Not_SelectedIndexChanged);
            // 
            // lstExtrapolated
            // 
            this.lstExtrapolated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader82,
            this.columnHeader84,
            this.columnHeader83});
            this.lstExtrapolated.HideSelection = false;
            this.lstExtrapolated.Location = new System.Drawing.Point(474, 286);
            this.lstExtrapolated.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstExtrapolated.Name = "lstExtrapolated";
            this.lstExtrapolated.Size = new System.Drawing.Size(225, 95);
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
            this.label68.Location = new System.Drawing.Point(477, 254);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(222, 23);
            this.label68.TabIndex = 195;
            this.label68.Text = "Extrapolated Summary";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(323, 257);
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
            this.lstTempSummary.Location = new System.Drawing.Point(325, 286);
            this.lstTempSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstTempSummary.Name = "lstTempSummary";
            this.lstTempSummary.Size = new System.Drawing.Size(133, 95);
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
            this.label66.Location = new System.Drawing.Point(26, 254);
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
            this.lstVaneSummary.Location = new System.Drawing.Point(29, 285);
            this.lstVaneSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstVaneSummary.Name = "lstVaneSummary";
            this.lstVaneSummary.Size = new System.Drawing.Size(283, 96);
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
            this.label65.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.cboMetWindRose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMetWindRose.FormattingEnabled = true;
            this.cboMetWindRose.Items.AddRange(new object[] {
            "Wind Rose",
            "WS by WD"});
            this.cboMetWindRose.Location = new System.Drawing.Point(1392, 7);
            this.cboMetWindRose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMetWindRose.Name = "cboMetWindRose";
            this.cboMetWindRose.Size = new System.Drawing.Size(86, 26);
            this.cboMetWindRose.TabIndex = 188;
            this.cboMetWindRose.Text = "Wind Rose";
            this.cboMetWindRose.SelectedIndexChanged += new System.EventHandler(this.cboMetWindRose_SelectedIndexChanged);
            // 
            // label64
            // 
            this.label64.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(1245, 9);
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
            this.lstAlphas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAlphas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader74,
            this.columnHeader86,
            this.columnHeader87,
            this.columnHeader114});
            this.lstAlphas.HideSelection = false;
            this.lstAlphas.Location = new System.Drawing.Point(825, 133);
            this.lstAlphas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstAlphas.MaximumSize = new System.Drawing.Size(250, 292);
            this.lstAlphas.Name = "lstAlphas";
            this.lstAlphas.Size = new System.Drawing.Size(215, 292);
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
            this.label61.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(821, 104);
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
            this.lstAnemSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lstAnemSummary.Size = new System.Drawing.Size(783, 160);
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
            this.pgeMERRA.AutoScroll = true;
            this.pgeMERRA.Controls.Add(this.btnExportCloudCover);
            this.pgeMERRA.Controls.Add(this.cboNumWDRefTab);
            this.pgeMERRA.Controls.Add(this.label223);
            this.pgeMERRA.Controls.Add(this.cboRefWindOrEnergy);
            this.pgeMERRA.Controls.Add(this.label217);
            this.pgeMERRA.Controls.Add(this.txtMaxLong);
            this.pgeMERRA.Controls.Add(this.label218);
            this.pgeMERRA.Controls.Add(this.txtMinLong);
            this.pgeMERRA.Controls.Add(this.label219);
            this.pgeMERRA.Controls.Add(this.txtMaxLat);
            this.pgeMERRA.Controls.Add(this.label220);
            this.pgeMERRA.Controls.Add(this.txtMinLat);
            this.pgeMERRA.Controls.Add(this.txtRefDataDownloadName);
            this.pgeMERRA.Controls.Add(this.label216);
            this.pgeMERRA.Controls.Add(this.txtRefDataDownloadFolder);
            this.pgeMERRA.Controls.Add(this.btnRefDataDownloads);
            this.pgeMERRA.Controls.Add(this.btnDelRef);
            this.pgeMERRA.Controls.Add(this.btnEditReference);
            this.pgeMERRA.Controls.Add(this.btnAddNew);
            this.pgeMERRA.Controls.Add(this.label207);
            this.pgeMERRA.Controls.Add(this.cboLTReferences);
            this.pgeMERRA.Controls.Add(this.label205);
            this.pgeMERRA.Controls.Add(this.txtRefDataAvail);
            this.pgeMERRA.Controls.Add(this.label204);
            this.pgeMERRA.Controls.Add(this.dateLTRefAvailEnd);
            this.pgeMERRA.Controls.Add(this.dateLTRefAvailStart);
            this.pgeMERRA.Controls.Add(this.label52);
            this.pgeMERRA.Controls.Add(this.label54);
            this.pgeMERRA.Controls.Add(this.btnExplainMERRA2Tab);
            this.pgeMERRA.Controls.Add(this.plotMERRA_WindRose);
            this.pgeMERRA.Controls.Add(this.plotMERRA_Monthly);
            this.pgeMERRA.Controls.Add(this.plotMERRA_Yearly);
            this.pgeMERRA.Controls.Add(this.btnImportCRV_MERRA);
            this.pgeMERRA.Controls.Add(this.chkYearsToDisplayAll);
            this.pgeMERRA.Controls.Add(this.label121);
            this.pgeMERRA.Controls.Add(this.cboReferenceMonth);
            this.pgeMERRA.Controls.Add(this.label120);
            this.pgeMERRA.Controls.Add(this.cboReferenceYear);
            this.pgeMERRA.Controls.Add(this.chkYearsToDisplay);
            this.pgeMERRA.Controls.Add(this.label162);
            this.pgeMERRA.Controls.Add(this.txtMERRA_WS_ScaleFact);
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
            this.pgeMERRA.Controls.Add(this.label154);
            this.pgeMERRA.Controls.Add(this.label151);
            this.pgeMERRA.Location = new System.Drawing.Point(4, 27);
            this.pgeMERRA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeMERRA.Name = "pgeMERRA";
            this.pgeMERRA.Size = new System.Drawing.Size(1488, 712);
            this.pgeMERRA.TabIndex = 16;
            this.pgeMERRA.Text = "LT Reference Data";
            this.pgeMERRA.UseVisualStyleBackColor = true;
            // 
            // btnExportCloudCover
            // 
            this.btnExportCloudCover.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCloudCover.Location = new System.Drawing.Point(19, 638);
            this.btnExportCloudCover.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportCloudCover.Name = "btnExportCloudCover";
            this.btnExportCloudCover.Size = new System.Drawing.Size(128, 48);
            this.btnExportCloudCover.TabIndex = 272;
            this.btnExportCloudCover.Text = "Export Cloud Cover (MERRA2)";
            this.btnExportCloudCover.UseVisualStyleBackColor = true;
            this.btnExportCloudCover.Click += new System.EventHandler(this.btnExportCloudCover_Click);
            // 
            // cboNumWDRefTab
            // 
            this.cboNumWDRefTab.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNumWDRefTab.FormattingEnabled = true;
            this.cboNumWDRefTab.Items.AddRange(new object[] {
            "1",
            "4",
            "8",
            "12",
            "16",
            "24"});
            this.cboNumWDRefTab.Location = new System.Drawing.Point(1427, 84);
            this.cboNumWDRefTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboNumWDRefTab.Name = "cboNumWDRefTab";
            this.cboNumWDRefTab.Size = new System.Drawing.Size(48, 26);
            this.cboNumWDRefTab.TabIndex = 271;
            this.cboNumWDRefTab.Text = "16";
            this.cboNumWDRefTab.SelectedIndexChanged += new System.EventHandler(this.cboNumWDRefTab_SelectedIndexChanged);
            // 
            // label223
            // 
            this.label223.AutoSize = true;
            this.label223.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label223.Location = new System.Drawing.Point(1318, 88);
            this.label223.Name = "label223";
            this.label223.Size = new System.Drawing.Size(103, 18);
            this.label223.TabIndex = 270;
            this.label223.Text = "Num. WD bins :";
            // 
            // cboRefWindOrEnergy
            // 
            this.cboRefWindOrEnergy.FormattingEnabled = true;
            this.cboRefWindOrEnergy.Items.AddRange(new object[] {
            "Wind Rose",
            "Energy Rose"});
            this.cboRefWindOrEnergy.Location = new System.Drawing.Point(1247, 117);
            this.cboRefWindOrEnergy.Name = "cboRefWindOrEnergy";
            this.cboRefWindOrEnergy.Size = new System.Drawing.Size(121, 26);
            this.cboRefWindOrEnergy.TabIndex = 269;
            this.cboRefWindOrEnergy.SelectedIndexChanged += new System.EventHandler(this.cboRefWindOrEnergy_SelectedIndexChanged);
            // 
            // label217
            // 
            this.label217.AutoSize = true;
            this.label217.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label217.Location = new System.Drawing.Point(361, 339);
            this.label217.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label217.Name = "label217";
            this.label217.Size = new System.Drawing.Size(20, 18);
            this.label217.TabIndex = 268;
            this.label217.Text = "to";
            // 
            // txtMaxLong
            // 
            this.txtMaxLong.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxLong.Location = new System.Drawing.Point(382, 336);
            this.txtMaxLong.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaxLong.Name = "txtMaxLong";
            this.txtMaxLong.ReadOnly = true;
            this.txtMaxLong.Size = new System.Drawing.Size(44, 25);
            this.txtMaxLong.TabIndex = 267;
            // 
            // label218
            // 
            this.label218.AutoSize = true;
            this.label218.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label218.Location = new System.Drawing.Point(202, 339);
            this.label218.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label218.Name = "label218";
            this.label218.Size = new System.Drawing.Size(109, 18);
            this.label218.TabIndex = 266;
            this.label218.Text = "Longitude range:";
            // 
            // txtMinLong
            // 
            this.txtMinLong.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinLong.Location = new System.Drawing.Point(314, 336);
            this.txtMinLong.Margin = new System.Windows.Forms.Padding(2);
            this.txtMinLong.Name = "txtMinLong";
            this.txtMinLong.ReadOnly = true;
            this.txtMinLong.Size = new System.Drawing.Size(44, 25);
            this.txtMinLong.TabIndex = 265;
            // 
            // label219
            // 
            this.label219.AutoSize = true;
            this.label219.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label219.Location = new System.Drawing.Point(361, 306);
            this.label219.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label219.Name = "label219";
            this.label219.Size = new System.Drawing.Size(20, 18);
            this.label219.TabIndex = 264;
            this.label219.Text = "to";
            // 
            // txtMaxLat
            // 
            this.txtMaxLat.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxLat.Location = new System.Drawing.Point(382, 303);
            this.txtMaxLat.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaxLat.Name = "txtMaxLat";
            this.txtMaxLat.ReadOnly = true;
            this.txtMaxLat.Size = new System.Drawing.Size(44, 25);
            this.txtMaxLat.TabIndex = 263;
            // 
            // label220
            // 
            this.label220.AutoSize = true;
            this.label220.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label220.Location = new System.Drawing.Point(210, 306);
            this.label220.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label220.Name = "label220";
            this.label220.Size = new System.Drawing.Size(99, 18);
            this.label220.TabIndex = 262;
            this.label220.Text = "Latitude range:";
            // 
            // txtMinLat
            // 
            this.txtMinLat.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinLat.Location = new System.Drawing.Point(314, 303);
            this.txtMinLat.Margin = new System.Windows.Forms.Padding(2);
            this.txtMinLat.Name = "txtMinLat";
            this.txtMinLat.ReadOnly = true;
            this.txtMinLat.Size = new System.Drawing.Size(44, 25);
            this.txtMinLat.TabIndex = 261;
            // 
            // txtRefDataDownloadName
            // 
            this.txtRefDataDownloadName.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefDataDownloadName.Location = new System.Drawing.Point(19, 220);
            this.txtRefDataDownloadName.Margin = new System.Windows.Forms.Padding(2);
            this.txtRefDataDownloadName.Name = "txtRefDataDownloadName";
            this.txtRefDataDownloadName.ReadOnly = true;
            this.txtRefDataDownloadName.Size = new System.Drawing.Size(434, 25);
            this.txtRefDataDownloadName.TabIndex = 260;
            // 
            // label216
            // 
            this.label216.AutoSize = true;
            this.label216.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.label216.Location = new System.Drawing.Point(21, 199);
            this.label216.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label216.Name = "label216";
            this.label216.Size = new System.Drawing.Size(202, 17);
            this.label216.TabIndex = 259;
            this.label216.Text = "Extracted from Reference Dataset:";
            // 
            // txtRefDataDownloadFolder
            // 
            this.txtRefDataDownloadFolder.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefDataDownloadFolder.Location = new System.Drawing.Point(19, 270);
            this.txtRefDataDownloadFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtRefDataDownloadFolder.Name = "txtRefDataDownloadFolder";
            this.txtRefDataDownloadFolder.ReadOnly = true;
            this.txtRefDataDownloadFolder.Size = new System.Drawing.Size(434, 25);
            this.txtRefDataDownloadFolder.TabIndex = 257;
            // 
            // btnRefDataDownloads
            // 
            this.btnRefDataDownloads.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.btnRefDataDownloads.Location = new System.Drawing.Point(352, 174);
            this.btnRefDataDownloads.Name = "btnRefDataDownloads";
            this.btnRefDataDownloads.Size = new System.Drawing.Size(103, 40);
            this.btnRefDataDownloads.TabIndex = 256;
            this.btnRefDataDownloads.Text = "View/Edit Ref. Data Downloads";
            this.btnRefDataDownloads.UseVisualStyleBackColor = true;
            this.btnRefDataDownloads.Click += new System.EventHandler(this.btnRefDataDownloads_Click);
            // 
            // btnDelRef
            // 
            this.btnDelRef.Font = new System.Drawing.Font("Palatino Linotype", 10F);
            this.btnDelRef.Location = new System.Drawing.Point(424, 49);
            this.btnDelRef.Name = "btnDelRef";
            this.btnDelRef.Size = new System.Drawing.Size(60, 31);
            this.btnDelRef.TabIndex = 254;
            this.btnDelRef.Text = "Delete";
            this.btnDelRef.UseVisualStyleBackColor = true;
            this.btnDelRef.Click += new System.EventHandler(this.btnDelRef_Click);
            // 
            // btnEditReference
            // 
            this.btnEditReference.Font = new System.Drawing.Font("Palatino Linotype", 10F);
            this.btnEditReference.Location = new System.Drawing.Point(358, 49);
            this.btnEditReference.Name = "btnEditReference";
            this.btnEditReference.Size = new System.Drawing.Size(60, 31);
            this.btnEditReference.TabIndex = 253;
            this.btnEditReference.Text = "Edit";
            this.btnEditReference.UseVisualStyleBackColor = true;
            this.btnEditReference.Click += new System.EventHandler(this.btnEditReference_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Font = new System.Drawing.Font("Palatino Linotype", 10F);
            this.btnAddNew.Location = new System.Drawing.Point(287, 49);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(60, 31);
            this.btnAddNew.TabIndex = 252;
            this.btnAddNew.Text = "New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // label207
            // 
            this.label207.AutoSize = true;
            this.label207.Location = new System.Drawing.Point(27, 68);
            this.label207.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label207.Name = "label207";
            this.label207.Size = new System.Drawing.Size(98, 19);
            this.label207.TabIndex = 251;
            this.label207.Text = "Reference Site:";
            // 
            // cboLTReferences
            // 
            this.cboLTReferences.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLTReferences.FormattingEnabled = true;
            this.cboLTReferences.Location = new System.Drawing.Point(19, 91);
            this.cboLTReferences.Margin = new System.Windows.Forms.Padding(2);
            this.cboLTReferences.Name = "cboLTReferences";
            this.cboLTReferences.Size = new System.Drawing.Size(459, 25);
            this.cboLTReferences.TabIndex = 250;
            this.cboLTReferences.SelectedIndexChanged += new System.EventHandler(this.cboLTReferences_SelectedIndexChanged);
            // 
            // label205
            // 
            this.label205.AutoSize = true;
            this.label205.Enabled = false;
            this.label205.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label205.Location = new System.Drawing.Point(245, 376);
            this.label205.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label205.Name = "label205";
            this.label205.Size = new System.Drawing.Size(95, 18);
            this.label205.TabIndex = 249;
            this.label205.Text = "Complete [%] :";
            // 
            // txtRefDataAvail
            // 
            this.txtRefDataAvail.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefDataAvail.Location = new System.Drawing.Point(343, 373);
            this.txtRefDataAvail.Margin = new System.Windows.Forms.Padding(2);
            this.txtRefDataAvail.Name = "txtRefDataAvail";
            this.txtRefDataAvail.ReadOnly = true;
            this.txtRefDataAvail.Size = new System.Drawing.Size(66, 25);
            this.txtRefDataAvail.TabIndex = 248;
            // 
            // label204
            // 
            this.label204.AutoSize = true;
            this.label204.Enabled = false;
            this.label204.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label204.Location = new System.Drawing.Point(20, 300);
            this.label204.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label204.Name = "label204";
            this.label204.Size = new System.Drawing.Size(175, 36);
            this.label204.TabIndex = 247;
            this.label204.Text = "Downloaded Data: \r\nDate Range and Complete %";
            // 
            // dateLTRefAvailEnd
            // 
            this.dateLTRefAvailEnd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLTRefAvailEnd.CausesValidation = false;
            this.dateLTRefAvailEnd.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateLTRefAvailEnd.Enabled = false;
            this.dateLTRefAvailEnd.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLTRefAvailEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateLTRefAvailEnd.Location = new System.Drawing.Point(23, 410);
            this.dateLTRefAvailEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dateLTRefAvailEnd.MaxDate = new System.DateTime(2050, 12, 1, 0, 0, 0, 0);
            this.dateLTRefAvailEnd.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dateLTRefAvailEnd.Name = "dateLTRefAvailEnd";
            this.dateLTRefAvailEnd.Size = new System.Drawing.Size(163, 25);
            this.dateLTRefAvailEnd.TabIndex = 245;
            this.dateLTRefAvailEnd.Value = new System.DateTime(2022, 12, 31, 23, 0, 0, 0);
            // 
            // dateLTRefAvailStart
            // 
            this.dateLTRefAvailStart.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLTRefAvailStart.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateLTRefAvailStart.Enabled = false;
            this.dateLTRefAvailStart.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLTRefAvailStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateLTRefAvailStart.Location = new System.Drawing.Point(22, 359);
            this.dateLTRefAvailStart.Margin = new System.Windows.Forms.Padding(2);
            this.dateLTRefAvailStart.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dateLTRefAvailStart.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dateLTRefAvailStart.Name = "dateLTRefAvailStart";
            this.dateLTRefAvailStart.Size = new System.Drawing.Size(163, 25);
            this.dateLTRefAvailStart.TabIndex = 244;
            this.dateLTRefAvailStart.Value = new System.DateTime(2002, 1, 1, 0, 0, 0, 0);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Enabled = false;
            this.label52.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(20, 390);
            this.label52.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(74, 18);
            this.label52.TabIndex = 243;
            this.label52.Text = "End (Local)";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Enabled = false;
            this.label54.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(21, 339);
            this.label54.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(81, 18);
            this.label54.TabIndex = 242;
            this.label54.Text = "Start (Local)";
            // 
            // btnExplainMERRA2Tab
            // 
            this.btnExplainMERRA2Tab.Font = new System.Drawing.Font("Palatino Linotype", 12F);
            this.btnExplainMERRA2Tab.Location = new System.Drawing.Point(519, 13);
            this.btnExplainMERRA2Tab.Name = "btnExplainMERRA2Tab";
            this.btnExplainMERRA2Tab.Size = new System.Drawing.Size(63, 31);
            this.btnExplainMERRA2Tab.TabIndex = 237;
            this.btnExplainMERRA2Tab.Text = "Help";
            this.btnExplainMERRA2Tab.UseVisualStyleBackColor = true;
            this.btnExplainMERRA2Tab.Click += new System.EventHandler(this.btnExplainMERRA2Tab_Click);
            // 
            // plotMERRA_WindRose
            // 
            this.plotMERRA_WindRose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotMERRA_WindRose.Location = new System.Drawing.Point(1233, 159);
            this.plotMERRA_WindRose.Name = "plotMERRA_WindRose";
            this.plotMERRA_WindRose.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMERRA_WindRose.Size = new System.Drawing.Size(242, 228);
            this.plotMERRA_WindRose.TabIndex = 226;
            this.plotMERRA_WindRose.Text = "plotView1";
            this.plotMERRA_WindRose.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMERRA_WindRose.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMERRA_WindRose.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotMERRA_WindRose.Click += new System.EventHandler(this.plotMERRA_WindRose_Click);
            // 
            // plotMERRA_Monthly
            // 
            this.plotMERRA_Monthly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotMERRA_Monthly.Location = new System.Drawing.Point(848, 402);
            this.plotMERRA_Monthly.MaximumSize = new System.Drawing.Size(680, 400);
            this.plotMERRA_Monthly.Name = "plotMERRA_Monthly";
            this.plotMERRA_Monthly.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMERRA_Monthly.Size = new System.Drawing.Size(620, 284);
            this.plotMERRA_Monthly.TabIndex = 225;
            this.plotMERRA_Monthly.Text = "plotView1";
            this.plotMERRA_Monthly.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMERRA_Monthly.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMERRA_Monthly.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotMERRA_Monthly.Click += new System.EventHandler(this.plotMERRA_Monthly_Click);
            // 
            // plotMERRA_Yearly
            // 
            this.plotMERRA_Yearly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotMERRA_Yearly.Location = new System.Drawing.Point(213, 403);
            this.plotMERRA_Yearly.MaximumSize = new System.Drawing.Size(680, 400);
            this.plotMERRA_Yearly.Name = "plotMERRA_Yearly";
            this.plotMERRA_Yearly.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMERRA_Yearly.Size = new System.Drawing.Size(620, 285);
            this.plotMERRA_Yearly.TabIndex = 224;
            this.plotMERRA_Yearly.Text = "plotView1";
            this.plotMERRA_Yearly.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMERRA_Yearly.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMERRA_Yearly.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnImportCRV_MERRA
            // 
            this.btnImportCRV_MERRA.BackColor = System.Drawing.Color.LightCoral;
            this.btnImportCRV_MERRA.Location = new System.Drawing.Point(955, 6);
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
            this.label121.Location = new System.Drawing.Point(1293, 51);
            this.label121.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(57, 18);
            this.label121.TabIndex = 211;
            this.label121.Text = "Month : ";
            // 
            // cboReferenceMonth
            // 
            this.cboReferenceMonth.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReferenceMonth.FormattingEnabled = true;
            this.cboReferenceMonth.Items.AddRange(new object[] {
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
            this.cboReferenceMonth.Location = new System.Drawing.Point(1352, 48);
            this.cboReferenceMonth.Margin = new System.Windows.Forms.Padding(2);
            this.cboReferenceMonth.Name = "cboReferenceMonth";
            this.cboReferenceMonth.Size = new System.Drawing.Size(123, 26);
            this.cboReferenceMonth.TabIndex = 210;
            this.cboReferenceMonth.Text = "All Months";
            this.cboReferenceMonth.SelectedIndexChanged += new System.EventHandler(this.cboMERRA_Month_SelectedIndexChanged);
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label120.Location = new System.Drawing.Point(1152, 51);
            this.label120.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(44, 18);
            this.label120.TabIndex = 209;
            this.label120.Text = "Year : ";
            // 
            // cboReferenceYear
            // 
            this.cboReferenceYear.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReferenceYear.FormattingEnabled = true;
            this.cboReferenceYear.Location = new System.Drawing.Point(1199, 48);
            this.cboReferenceYear.Margin = new System.Windows.Forms.Padding(2);
            this.cboReferenceYear.Name = "cboReferenceYear";
            this.cboReferenceYear.Size = new System.Drawing.Size(88, 26);
            this.cboReferenceYear.TabIndex = 208;
            this.cboReferenceYear.SelectedIndexChanged += new System.EventHandler(this.cboMERRAYear_SelectedIndexChanged);
            // 
            // chkYearsToDisplay
            // 
            this.chkYearsToDisplay.CheckOnClick = true;
            this.chkYearsToDisplay.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.chkYearsToDisplay.FormattingEnabled = true;
            this.chkYearsToDisplay.Location = new System.Drawing.Point(1139, 117);
            this.chkYearsToDisplay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkYearsToDisplay.Name = "chkYearsToDisplay";
            this.chkYearsToDisplay.Size = new System.Drawing.Size(88, 270);
            this.chkYearsToDisplay.TabIndex = 207;
            this.chkYearsToDisplay.SelectedIndexChanged += new System.EventHandler(this.chkYearsToDisplay_SelectedIndexChanged);
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Enabled = false;
            this.label162.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label162.Location = new System.Drawing.Point(344, 124);
            this.label162.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(109, 18);
            this.label162.TabIndex = 206;
            this.label162.Text = "WS Scale Factor :";
            // 
            // txtMERRA_WS_ScaleFact
            // 
            this.txtMERRA_WS_ScaleFact.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMERRA_WS_ScaleFact.Location = new System.Drawing.Point(358, 144);
            this.txtMERRA_WS_ScaleFact.Margin = new System.Windows.Forms.Padding(2);
            this.txtMERRA_WS_ScaleFact.Name = "txtMERRA_WS_ScaleFact";
            this.txtMERRA_WS_ScaleFact.ReadOnly = true;
            this.txtMERRA_WS_ScaleFact.Size = new System.Drawing.Size(66, 25);
            this.txtMERRA_WS_ScaleFact.TabIndex = 205;
            this.txtMERRA_WS_ScaleFact.Text = "1";
            // 
            // label160
            // 
            this.label160.AutoSize = true;
            this.label160.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label160.Location = new System.Drawing.Point(772, 53);
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
            this.cboMERRA_PowerCurves.Location = new System.Drawing.Point(874, 49);
            this.cboMERRA_PowerCurves.Margin = new System.Windows.Forms.Padding(2);
            this.cboMERRA_PowerCurves.MaxDropDownItems = 100;
            this.cboMERRA_PowerCurves.Name = "cboMERRA_PowerCurves";
            this.cboMERRA_PowerCurves.Size = new System.Drawing.Size(257, 26);
            this.cboMERRA_PowerCurves.TabIndex = 201;
            this.cboMERRA_PowerCurves.SelectedIndexChanged += new System.EventHandler(this.cboMERRA_PowerCurves_SelectedIndexChanged);
            // 
            // btn_ExportWR
            // 
            this.btn_ExportWR.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ExportWR.Location = new System.Drawing.Point(19, 575);
            this.btn_ExportWR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_ExportWR.Name = "btn_ExportWR";
            this.btn_ExportWR.Size = new System.Drawing.Size(128, 48);
            this.btn_ExportWR.TabIndex = 154;
            this.btn_ExportWR.Text = "Export Wind Rose";
            this.btn_ExportWR.UseVisualStyleBackColor = true;
            this.btn_ExportWR.Click += new System.EventHandler(this.btn_ExportWR_Click);
            // 
            // btn_Export_All_Months_All_Years
            // 
            this.btn_Export_All_Months_All_Years.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export_All_Months_All_Years.Location = new System.Drawing.Point(19, 512);
            this.btn_Export_All_Months_All_Years.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Export_All_Months_All_Years.Name = "btn_Export_All_Months_All_Years";
            this.btn_Export_All_Months_All_Years.Size = new System.Drawing.Size(128, 48);
            this.btn_Export_All_Months_All_Years.TabIndex = 153;
            this.btn_Export_All_Months_All_Years.Text = "Export All Months && All Years";
            this.btn_Export_All_Months_All_Years.UseVisualStyleBackColor = true;
            this.btn_Export_All_Months_All_Years.Click += new System.EventHandler(this.btn_Export_All_Months_All_Years_Click);
            // 
            // label159
            // 
            this.label159.AutoSize = true;
            this.label159.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label159.Location = new System.Drawing.Point(1176, 18);
            this.label159.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(273, 20);
            this.label159.TabIndex = 151;
            this.label159.Text = "LT Ref. Wind Rose for Selected Interval:";
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label156.Location = new System.Drawing.Point(510, 58);
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
            this.cboMERRA_PlotParam.Location = new System.Drawing.Point(623, 53);
            this.cboMERRA_PlotParam.Margin = new System.Windows.Forms.Padding(2);
            this.cboMERRA_PlotParam.Name = "cboMERRA_PlotParam";
            this.cboMERRA_PlotParam.Size = new System.Drawing.Size(134, 25);
            this.cboMERRA_PlotParam.TabIndex = 145;
            this.cboMERRA_PlotParam.Text = "CF (%)";
            this.cboMERRA_PlotParam.SelectedIndexChanged += new System.EventHandler(this.cboMERRA_PlotParam_SelectedIndexChanged);
            // 
            // lstMERRAAnnualProd
            // 
            this.lstMERRAAnnualProd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lstMERRAAnnualProd.Size = new System.Drawing.Size(287, 302);
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
            this.lstMERRA_MonthlyProd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lstMERRA_MonthlyProd.Size = new System.Drawing.Size(331, 302);
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
            this.btn_Export_Interp.Location = new System.Drawing.Point(19, 449);
            this.btn_Export_Interp.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Export_Interp.Name = "btn_Export_Interp";
            this.btn_Export_Interp.Size = new System.Drawing.Size(129, 48);
            this.btn_Export_Interp.TabIndex = 138;
            this.btn_Export_Interp.Text = "Export Interpolated Data";
            this.btn_Export_Interp.UseVisualStyleBackColor = true;
            this.btn_Export_Interp.Click += new System.EventHandler(this.btn_Export_Interp_Click);
            // 
            // dateMERRAEnd
            // 
            this.dateMERRAEnd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAEnd.CausesValidation = false;
            this.dateMERRAEnd.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateMERRAEnd.Enabled = false;
            this.dateMERRAEnd.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMERRAEnd.Location = new System.Drawing.Point(142, 156);
            this.dateMERRAEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dateMERRAEnd.MaxDate = new System.DateTime(2050, 12, 1, 0, 0, 0, 0);
            this.dateMERRAEnd.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dateMERRAEnd.Name = "dateMERRAEnd";
            this.dateMERRAEnd.Size = new System.Drawing.Size(163, 25);
            this.dateMERRAEnd.TabIndex = 124;
            this.dateMERRAEnd.Value = new System.DateTime(2022, 12, 31, 23, 0, 0, 0);
            // 
            // dateMERRAStart
            // 
            this.dateMERRAStart.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAStart.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateMERRAStart.Enabled = false;
            this.dateMERRAStart.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMERRAStart.Location = new System.Drawing.Point(142, 122);
            this.dateMERRAStart.Margin = new System.Windows.Forms.Padding(2);
            this.dateMERRAStart.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dateMERRAStart.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dateMERRAStart.Name = "dateMERRAStart";
            this.dateMERRAStart.Size = new System.Drawing.Size(163, 25);
            this.dateMERRAStart.TabIndex = 123;
            this.dateMERRAStart.Value = new System.DateTime(2002, 1, 1, 0, 0, 0, 0);
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Enabled = false;
            this.label152.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label152.Location = new System.Drawing.Point(29, 156);
            this.label152.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(74, 18);
            this.label152.TabIndex = 122;
            this.label152.Text = "End (Local)";
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Enabled = false;
            this.label153.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label153.Location = new System.Drawing.Point(28, 129);
            this.label153.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(81, 18);
            this.label153.TabIndex = 121;
            this.label153.Text = "Start (Local)";
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Enabled = false;
            this.label154.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label154.Location = new System.Drawing.Point(20, 248);
            this.label154.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(94, 17);
            this.label154.TabIndex = 109;
            this.label154.Text = "Folder location:";
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label151.Location = new System.Drawing.Point(22, 22);
            this.label151.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(283, 25);
            this.label151.TabIndex = 99;
            this.label151.Text = "Long-Term Reference Data";
            // 
            // pgeMCP
            // 
            this.pgeMCP.AutoScroll = true;
            this.pgeMCP.Controls.Add(this.btnShowMCP_Info);
            this.pgeMCP.Controls.Add(this.btnClearMCP);
            this.pgeMCP.Controls.Add(this.label237);
            this.pgeMCP.Controls.Add(this.cboMCP_Height);
            this.pgeMCP.Controls.Add(this.label206);
            this.pgeMCP.Controls.Add(this.cboMCP_Ref);
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
            this.pgeMCP.Size = new System.Drawing.Size(1488, 712);
            this.pgeMCP.TabIndex = 15;
            this.pgeMCP.Text = "MCP";
            this.pgeMCP.UseVisualStyleBackColor = true;
            // 
            // btnShowMCP_Info
            // 
            this.btnShowMCP_Info.Location = new System.Drawing.Point(200, 13);
            this.btnShowMCP_Info.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowMCP_Info.Name = "btnShowMCP_Info";
            this.btnShowMCP_Info.Size = new System.Drawing.Size(40, 30);
            this.btnShowMCP_Info.TabIndex = 304;
            this.btnShowMCP_Info.Text = "?";
            this.btnShowMCP_Info.UseVisualStyleBackColor = true;
            this.btnShowMCP_Info.Click += new System.EventHandler(this.btnShowMCP_Info_Click);
            // 
            // btnClearMCP
            // 
            this.btnClearMCP.Location = new System.Drawing.Point(646, 89);
            this.btnClearMCP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClearMCP.Name = "btnClearMCP";
            this.btnClearMCP.Size = new System.Drawing.Size(94, 30);
            this.btnClearMCP.TabIndex = 303;
            this.btnClearMCP.Text = "Clear MCP";
            this.btnClearMCP.UseVisualStyleBackColor = true;
            this.btnClearMCP.Click += new System.EventHandler(this.btnClearMCP_Click);
            // 
            // label237
            // 
            this.label237.AutoSize = true;
            this.label237.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label237.Location = new System.Drawing.Point(745, 243);
            this.label237.Name = "label237";
            this.label237.Size = new System.Drawing.Size(54, 18);
            this.label237.TabIndex = 302;
            this.label237.Text = "Height :";
            // 
            // cboMCP_Height
            // 
            this.cboMCP_Height.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMCP_Height.FormattingEnabled = true;
            this.cboMCP_Height.Location = new System.Drawing.Point(745, 261);
            this.cboMCP_Height.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCP_Height.Name = "cboMCP_Height";
            this.cboMCP_Height.Size = new System.Drawing.Size(110, 26);
            this.cboMCP_Height.TabIndex = 301;
            this.cboMCP_Height.SelectedIndexChanged += new System.EventHandler(this.cboMCP_Height_SelectedIndexChanged);
            // 
            // label206
            // 
            this.label206.AutoSize = true;
            this.label206.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label206.Location = new System.Drawing.Point(18, 97);
            this.label206.Name = "label206";
            this.label206.Size = new System.Drawing.Size(71, 18);
            this.label206.TabIndex = 300;
            this.label206.Text = "Reference :";
            // 
            // cboMCP_Ref
            // 
            this.cboMCP_Ref.FormattingEnabled = true;
            this.cboMCP_Ref.Location = new System.Drawing.Point(99, 93);
            this.cboMCP_Ref.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCP_Ref.Name = "cboMCP_Ref";
            this.cboMCP_Ref.Size = new System.Drawing.Size(349, 26);
            this.cboMCP_Ref.TabIndex = 299;
            this.cboMCP_Ref.SelectedIndexChanged += new System.EventHandler(this.cboMCP_Ref_SelectedIndexChanged);
            // 
            // btnExportTarget
            // 
            this.btnExportTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportTarget.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportTarget.Location = new System.Drawing.Point(822, 633);
            this.btnExportTarget.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportTarget.Name = "btnExportTarget";
            this.btnExportTarget.Size = new System.Drawing.Size(101, 64);
            this.btnExportTarget.TabIndex = 298;
            this.btnExportTarget.Text = "Export All Hourly Target Data";
            this.btnExportTarget.UseVisualStyleBackColor = true;
            this.btnExportTarget.Click += new System.EventHandler(this.btnExportTarget_Click);
            // 
            // plotMCP_Uncertainty
            // 
            this.plotMCP_Uncertainty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.plotMCP_Uncertainty.Location = new System.Drawing.Point(932, 367);
            this.plotMCP_Uncertainty.Name = "plotMCP_Uncertainty";
            this.plotMCP_Uncertainty.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMCP_Uncertainty.Size = new System.Drawing.Size(349, 264);
            this.plotMCP_Uncertainty.TabIndex = 297;
            this.plotMCP_Uncertainty.Text = "plotView1";
            this.plotMCP_Uncertainty.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMCP_Uncertainty.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMCP_Uncertainty.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotMCP
            // 
            this.plotMCP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotMCP.Location = new System.Drawing.Point(300, 295);
            this.plotMCP.Name = "plotMCP";
            this.plotMCP.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMCP.Size = new System.Drawing.Size(579, 326);
            this.plotMCP.TabIndex = 296;
            this.plotMCP.Text = "plotView1";
            this.plotMCP.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMCP.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMCP.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnShowMCPRanges
            // 
            this.btnShowMCPRanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowMCPRanges.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnShowMCPRanges.Location = new System.Drawing.Point(1294, 25);
            this.btnShowMCPRanges.Name = "btnShowMCPRanges";
            this.btnShowMCPRanges.Size = new System.Drawing.Size(116, 46);
            this.btnShowMCPRanges.TabIndex = 295;
            this.btnShowMCPRanges.Text = "View MCP Setting Valid Ranges";
            this.btnShowMCPRanges.UseVisualStyleBackColor = true;
            this.btnShowMCPRanges.Click += new System.EventHandler(this.btnShowMCPRanges_Click);
            // 
            // label51
            // 
            this.label51.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(1239, 160);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(121, 19);
            this.label51.TabIndex = 294;
            this.label51.Text = "Matrix Settings:";
            // 
            // btnDoMCPAllMets
            // 
            this.btnDoMCPAllMets.Location = new System.Drawing.Point(747, 41);
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
            this.btnDoMCP.Location = new System.Drawing.Point(646, 41);
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
            this.btnResetDates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetDates.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.btnResetDates.Location = new System.Drawing.Point(393, 628);
            this.btnResetDates.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnResetDates.Name = "btnResetDates";
            this.btnResetDates.Size = new System.Drawing.Size(45, 25);
            this.btnResetDates.TabIndex = 291;
            this.btnResetDates.Text = "Reset";
            this.btnResetDates.UseVisualStyleBackColor = true;
            this.btnResetDates.Click += new System.EventHandler(this.btnResetDates_Click);
            // 
            // dateMCPExportEnd
            // 
            this.dateMCPExportEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateMCPExportEnd.CustomFormat = "MM/dd/yy HH:mm";
            this.dateMCPExportEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMCPExportEnd.Location = new System.Drawing.Point(261, 683);
            this.dateMCPExportEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateMCPExportEnd.Name = "dateMCPExportEnd";
            this.dateMCPExportEnd.Size = new System.Drawing.Size(140, 25);
            this.dateMCPExportEnd.TabIndex = 290;
            this.dateMCPExportEnd.ValueChanged += new System.EventHandler(this.dateMCPExportEnd_ValueChanged);
            // 
            // label145
            // 
            this.label145.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label145.AutoSize = true;
            this.label145.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label145.Location = new System.Drawing.Point(261, 629);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(132, 17);
            this.label145.TabIndex = 289;
            this.label145.Text = "Export Data Range:";
            // 
            // dateMCPExportStart
            // 
            this.dateMCPExportStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateMCPExportStart.CustomFormat = "MM/dd/yy HH:mm";
            this.dateMCPExportStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMCPExportStart.Location = new System.Drawing.Point(262, 654);
            this.dateMCPExportStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateMCPExportStart.Name = "dateMCPExportStart";
            this.dateMCPExportStart.Size = new System.Drawing.Size(139, 25);
            this.dateMCPExportStart.TabIndex = 288;
            this.dateMCPExportStart.ValueChanged += new System.EventHandler(this.dateMCPExportStart_ValueChanged);
            // 
            // dateConcurrentEnd
            // 
            this.dateConcurrentEnd.CustomFormat = "MM/dd/yy HH:mm";
            this.dateConcurrentEnd.Enabled = false;
            this.dateConcurrentEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateConcurrentEnd.Location = new System.Drawing.Point(473, 90);
            this.dateConcurrentEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateConcurrentEnd.Name = "dateConcurrentEnd";
            this.dateConcurrentEnd.Size = new System.Drawing.Size(140, 25);
            this.dateConcurrentEnd.TabIndex = 287;
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label144.Location = new System.Drawing.Point(464, 25);
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
            this.dateConcurrentStart.Location = new System.Drawing.Point(473, 52);
            this.dateConcurrentStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateConcurrentStart.Name = "dateConcurrentStart";
            this.dateConcurrentStart.Size = new System.Drawing.Size(140, 25);
            this.dateConcurrentStart.TabIndex = 285;
            // 
            // txtTAB_WS_bin
            // 
            this.txtTAB_WS_bin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTAB_WS_bin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTAB_WS_bin.Location = new System.Drawing.Point(747, 674);
            this.txtTAB_WS_bin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTAB_WS_bin.Name = "txtTAB_WS_bin";
            this.txtTAB_WS_bin.Size = new System.Drawing.Size(35, 20);
            this.txtTAB_WS_bin.TabIndex = 284;
            this.txtTAB_WS_bin.Text = "1";
            this.txtTAB_WS_bin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label142
            // 
            this.label142.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label142.AutoSize = true;
            this.label142.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label142.Location = new System.Drawing.Point(661, 676);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(81, 15);
            this.label142.TabIndex = 283;
            this.label142.Text = "WS bin width:";
            // 
            // label143
            // 
            this.label143.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label143.AutoSize = true;
            this.label143.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label143.Location = new System.Drawing.Point(662, 639);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(56, 30);
            this.label143.TabIndex = 282;
            this.label143.Text = "Num. \r\nWD bins:";
            // 
            // cboTAB_bins
            // 
            this.cboTAB_bins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboTAB_bins.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTAB_bins.FormattingEnabled = true;
            this.cboTAB_bins.Items.AddRange(new object[] {
            "16",
            "24"});
            this.cboTAB_bins.Location = new System.Drawing.Point(736, 636);
            this.cboTAB_bins.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTAB_bins.Name = "cboTAB_bins";
            this.cboTAB_bins.Size = new System.Drawing.Size(48, 23);
            this.cboTAB_bins.TabIndex = 281;
            this.cboTAB_bins.Text = "16";
            // 
            // label141
            // 
            this.label141.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label141.AutoSize = true;
            this.label141.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label141.Location = new System.Drawing.Point(1220, 100);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(244, 19);
            this.label141.TabIndex = 280;
            this.label141.Text = "Method of Bins / Matrix Setting :";
            // 
            // label140
            // 
            this.label140.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label140.AutoSize = true;
            this.label140.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label140.Location = new System.Drawing.Point(968, 65);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(89, 18);
            this.label140.TabIndex = 279;
            this.label140.Text = "MCP Method:";
            // 
            // txtLast_WS_Wgt
            // 
            this.txtLast_WS_Wgt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLast_WS_Wgt.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLast_WS_Wgt.Location = new System.Drawing.Point(1368, 215);
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
            this.label134.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label134.AutoSize = true;
            this.label134.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label134.Location = new System.Drawing.Point(1253, 222);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(110, 18);
            this.label134.TabIndex = 277;
            this.label134.Text = "Last WS Weight :";
            // 
            // txtWS_PDF_Wgt
            // 
            this.txtWS_PDF_Wgt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWS_PDF_Wgt.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWS_PDF_Wgt.Location = new System.Drawing.Point(1368, 184);
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
            this.label135.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label135.AutoSize = true;
            this.label135.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label135.Location = new System.Drawing.Point(1254, 191);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(108, 18);
            this.label135.TabIndex = 275;
            this.label135.Text = "WS PDF Weight :";
            // 
            // cboMCPNumSeasons
            // 
            this.cboMCPNumSeasons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMCPNumSeasons.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMCPNumSeasons.FormattingEnabled = true;
            this.cboMCPNumSeasons.Items.AddRange(new object[] {
            "1",
            "4"});
            this.cboMCPNumSeasons.Location = new System.Drawing.Point(1095, 176);
            this.cboMCPNumSeasons.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCPNumSeasons.Name = "cboMCPNumSeasons";
            this.cboMCPNumSeasons.Size = new System.Drawing.Size(48, 26);
            this.cboMCPNumSeasons.TabIndex = 274;
            this.cboMCPNumSeasons.Text = "1";
            this.cboMCPNumSeasons.SelectedIndexChanged += new System.EventHandler(this.cboMCPNumSeasons_SelectedIndexChanged);
            // 
            // label136
            // 
            this.label136.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label136.AutoSize = true;
            this.label136.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label136.Location = new System.Drawing.Point(968, 184);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(121, 18);
            this.label136.TabIndex = 273;
            this.label136.Text = "Num. Season bins :";
            // 
            // cboMCPNumHours
            // 
            this.cboMCPNumHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMCPNumHours.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMCPNumHours.FormattingEnabled = true;
            this.cboMCPNumHours.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cboMCPNumHours.Location = new System.Drawing.Point(1125, 140);
            this.cboMCPNumHours.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCPNumHours.Name = "cboMCPNumHours";
            this.cboMCPNumHours.Size = new System.Drawing.Size(48, 26);
            this.cboMCPNumHours.TabIndex = 272;
            this.cboMCPNumHours.Text = "1";
            this.cboMCPNumHours.SelectedIndexChanged += new System.EventHandler(this.cboMCPNumHours_SelectedIndexChanged);
            // 
            // label137
            // 
            this.label137.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label137.AutoSize = true;
            this.label137.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label137.Location = new System.Drawing.Point(968, 148);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(151, 18);
            this.label137.TabIndex = 271;
            this.label137.Text = "Num. Time of Day bins :";
            // 
            // txtWS_bin_width
            // 
            this.txtWS_bin_width.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWS_bin_width.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWS_bin_width.Location = new System.Drawing.Point(1375, 123);
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
            this.label138.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label138.AutoSize = true;
            this.label138.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label138.Location = new System.Drawing.Point(1238, 126);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(131, 18);
            this.label138.TabIndex = 269;
            this.label138.Text = "WS bin width (m/s) :";
            // 
            // cboMCPNumWD
            // 
            this.cboMCPNumWD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMCPNumWD.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMCPNumWD.FormattingEnabled = true;
            this.cboMCPNumWD.Items.AddRange(new object[] {
            "1",
            "4",
            "8",
            "12",
            "16",
            "24"});
            this.cboMCPNumWD.Location = new System.Drawing.Point(1077, 109);
            this.cboMCPNumWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCPNumWD.Name = "cboMCPNumWD";
            this.cboMCPNumWD.Size = new System.Drawing.Size(48, 26);
            this.cboMCPNumWD.TabIndex = 268;
            this.cboMCPNumWD.Text = "16";
            this.cboMCPNumWD.SelectedIndexChanged += new System.EventHandler(this.cboMCPNumWD_SelectedIndexChanged);
            // 
            // label139
            // 
            this.label139.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label139.AutoSize = true;
            this.label139.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label139.Location = new System.Drawing.Point(968, 117);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(103, 18);
            this.label139.TabIndex = 267;
            this.label139.Text = "Num. WD bins :";
            // 
            // cboMCP_Type
            // 
            this.cboMCP_Type.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMCP_Type.FormattingEnabled = true;
            this.cboMCP_Type.Items.AddRange(new object[] {
            "Orth. Regression",
            "Method of Bins",
            "Variance Ratio",
            "Matrix"});
            this.cboMCP_Type.Location = new System.Drawing.Point(1063, 57);
            this.cboMCP_Type.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCP_Type.Name = "cboMCP_Type";
            this.cboMCP_Type.Size = new System.Drawing.Size(180, 26);
            this.cboMCP_Type.TabIndex = 266;
            this.cboMCP_Type.Text = "Orth. Regression";
            this.cboMCP_Type.SelectedIndexChanged += new System.EventHandler(this.cboMCP_Type_SelectedIndexChanged);
            // 
            // label133
            // 
            this.label133.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label133.AutoSize = true;
            this.label133.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label133.Location = new System.Drawing.Point(964, 25);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(129, 23);
            this.label133.TabIndex = 265;
            this.label133.Text = "MCP Settings";
            // 
            // txtNumYrsConc
            // 
            this.txtNumYrsConc.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumYrsConc.Location = new System.Drawing.Point(124, 273);
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
            this.label132.Location = new System.Drawing.Point(126, 251);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(41, 18);
            this.label132.TabIndex = 263;
            this.label132.Text = "Conc.";
            // 
            // txtRsq
            // 
            this.txtRsq.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRsq.Location = new System.Drawing.Point(160, 191);
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
            this.label130.Location = new System.Drawing.Point(172, 167);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(28, 18);
            this.label130.TabIndex = 259;
            this.label130.Text = "R² :";
            // 
            // btnExportBinRatios
            // 
            this.btnExportBinRatios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportBinRatios.Location = new System.Drawing.Point(69, 636);
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
            this.txtIntercept.Location = new System.Drawing.Point(98, 191);
            this.txtIntercept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtIntercept.Name = "txtIntercept";
            this.txtIntercept.ReadOnly = true;
            this.txtIntercept.Size = new System.Drawing.Size(54, 25);
            this.txtIntercept.TabIndex = 253;
            // 
            // txtSlope
            // 
            this.txtSlope.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlope.Location = new System.Drawing.Point(35, 191);
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
            this.label123.Location = new System.Drawing.Point(96, 167);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(65, 18);
            this.label123.TabIndex = 250;
            this.label123.Text = "Intercept:";
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label124.Location = new System.Drawing.Point(33, 167);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(43, 18);
            this.label124.TabIndex = 249;
            this.label124.Text = "Slope:";
            // 
            // label125
            // 
            this.label125.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label125.AutoSize = true;
            this.label125.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label125.Location = new System.Drawing.Point(30, 311);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(192, 19);
            this.label125.TabIndex = 248;
            this.label125.Text = "Avg && SD WS Ratios (T/R)";
            // 
            // lstMCP_Bins
            // 
            this.lstMCP_Bins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lstMCP_Bins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.WS,
            this.Mean,
            this.SD,
            this.Count});
            this.lstMCP_Bins.HideSelection = false;
            this.lstMCP_Bins.Location = new System.Drawing.Point(20, 336);
            this.lstMCP_Bins.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lstMCP_Bins.Name = "lstMCP_Bins";
            this.lstMCP_Bins.Size = new System.Drawing.Size(220, 295);
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
            this.lblRegStats.Location = new System.Drawing.Point(33, 133);
            this.lblRegStats.Name = "lblRegStats";
            this.lblRegStats.Size = new System.Drawing.Size(209, 23);
            this.lblRegStats.TabIndex = 246;
            this.lblRegStats.Text = "MCP Regression Stats.";
            // 
            // txtNumYrsTarg
            // 
            this.txtNumYrsTarg.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumYrsTarg.Location = new System.Drawing.Point(79, 273);
            this.txtNumYrsTarg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNumYrsTarg.Name = "txtNumYrsTarg";
            this.txtNumYrsTarg.ReadOnly = true;
            this.txtNumYrsTarg.Size = new System.Drawing.Size(38, 25);
            this.txtNumYrsTarg.TabIndex = 245;
            // 
            // txtNumYrsRef
            // 
            this.txtNumYrsRef.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumYrsRef.Location = new System.Drawing.Point(33, 273);
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
            this.label127.Location = new System.Drawing.Point(79, 251);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(38, 18);
            this.label127.TabIndex = 243;
            this.label127.Text = "Targ.";
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label128.Location = new System.Drawing.Point(35, 251);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(33, 18);
            this.label128.TabIndex = 242;
            this.label128.Text = "Ref. ";
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label129.Location = new System.Drawing.Point(38, 230);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(115, 16);
            this.label129.TabIndex = 241;
            this.label129.Text = "Number of Years";
            // 
            // btnExportMCP_TAB
            // 
            this.btnExportMCP_TAB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportMCP_TAB.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportMCP_TAB.Location = new System.Drawing.Point(551, 637);
            this.btnExportMCP_TAB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportMCP_TAB.Name = "btnExportMCP_TAB";
            this.btnExportMCP_TAB.Size = new System.Drawing.Size(104, 64);
            this.btnExportMCP_TAB.TabIndex = 240;
            this.btnExportMCP_TAB.Text = "Export Estimated data as TAB file\r\n";
            this.btnExportMCP_TAB.UseVisualStyleBackColor = true;
            this.btnExportMCP_TAB.Click += new System.EventHandler(this.btnExportMCP_TAB_Click);
            // 
            // btnExportMCP_TS
            // 
            this.btnExportMCP_TS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportMCP_TS.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportMCP_TS.Location = new System.Drawing.Point(445, 637);
            this.btnExportMCP_TS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportMCP_TS.Name = "btnExportMCP_TS";
            this.btnExportMCP_TS.Size = new System.Drawing.Size(102, 64);
            this.btnExportMCP_TS.TabIndex = 239;
            this.btnExportMCP_TS.Text = "Export Estimated data as Time Series";
            this.btnExportMCP_TS.UseVisualStyleBackColor = true;
            this.btnExportMCP_TS.Click += new System.EventHandler(this.btnExportMCP_TS_Click);
            // 
            // cboUncertStep
            // 
            this.cboUncertStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUncertStep.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUncertStep.FormattingEnabled = true;
            this.cboUncertStep.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cboUncertStep.Location = new System.Drawing.Point(1138, 311);
            this.cboUncertStep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboUncertStep.Name = "cboUncertStep";
            this.cboUncertStep.Size = new System.Drawing.Size(48, 26);
            this.cboUncertStep.TabIndex = 238;
            this.cboUncertStep.Text = "1";
            this.cboUncertStep.SelectedIndexChanged += new System.EventHandler(this.cboUncertStep_SelectedIndexChanged);
            // 
            // label119
            // 
            this.label119.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label119.AutoSize = true;
            this.label119.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label119.Location = new System.Drawing.Point(965, 313);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(140, 36);
            this.label119.TabIndex = 237;
            this.label119.Text = "Uncertainty Window \r\nStep (months):";
            // 
            // lstMCP_Uncert
            // 
            this.lstMCP_Uncert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMCP_Uncert.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Window,
            this.AVG,
            this.SDU});
            this.lstMCP_Uncert.HideSelection = false;
            this.lstMCP_Uncert.Location = new System.Drawing.Point(1302, 368);
            this.lstMCP_Uncert.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lstMCP_Uncert.Name = "lstMCP_Uncert";
            this.lstMCP_Uncert.Size = new System.Drawing.Size(161, 275);
            this.lstMCP_Uncert.TabIndex = 236;
            this.lstMCP_Uncert.UseCompatibleStateImageBehavior = false;
            this.lstMCP_Uncert.View = System.Windows.Forms.View.Details;
            // 
            // Window
            // 
            this.Window.Text = "Window";
            this.Window.Width = 73;
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
            this.btnExportMCPUncert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportMCPUncert.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportMCPUncert.Location = new System.Drawing.Point(1310, 652);
            this.btnExportMCPUncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportMCPUncert.Name = "btnExportMCPUncert";
            this.btnExportMCPUncert.Size = new System.Drawing.Size(134, 46);
            this.btnExportMCPUncert.TabIndex = 235;
            this.btnExportMCPUncert.Text = "Export Uncertainty Analysis\r\n\r\n";
            this.btnExportMCPUncert.UseVisualStyleBackColor = true;
            this.btnExportMCPUncert.Click += new System.EventHandler(this.btnExportMCPUncert_Click);
            // 
            // label118
            // 
            this.label118.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label118.AutoSize = true;
            this.label118.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label118.Location = new System.Drawing.Point(959, 267);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(244, 23);
            this.label118.TabIndex = 234;
            this.label118.Text = "MCP Uncertainty Analysis";
            // 
            // btnMCP_Uncert
            // 
            this.btnMCP_Uncert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMCP_Uncert.Location = new System.Drawing.Point(1230, 280);
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
            this.label108.Location = new System.Drawing.Point(609, 243);
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
            this.cboMCP_Season.Location = new System.Drawing.Point(609, 261);
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
            this.label109.Location = new System.Drawing.Point(328, 243);
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
            this.cboMCP_TOD.Location = new System.Drawing.Point(325, 262);
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
            this.label110.Location = new System.Drawing.Point(380, 132);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(435, 23);
            this.label110.TabIndex = 226;
            this.label110.Text = "Mean Concurrent and Long-term Wind Speeds";
            // 
            // txtLTratio
            // 
            this.txtLTratio.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLTratio.Location = new System.Drawing.Point(667, 206);
            this.txtLTratio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLTratio.Name = "txtLTratio";
            this.txtLTratio.ReadOnly = true;
            this.txtLTratio.Size = new System.Drawing.Size(70, 25);
            this.txtLTratio.TabIndex = 225;
            // 
            // txtAvgRatio
            // 
            this.txtAvgRatio.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvgRatio.Location = new System.Drawing.Point(667, 177);
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
            this.label111.Location = new System.Drawing.Point(668, 155);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(67, 18);
            this.label111.TabIndex = 223;
            this.label111.Text = "Ratio: T/R\r\n";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label112.Location = new System.Drawing.Point(759, 154);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(45, 18);
            this.label112.TabIndex = 222;
            this.label112.Text = "Count";
            // 
            // txtDataCount
            // 
            this.txtDataCount.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataCount.Location = new System.Drawing.Point(748, 177);
            this.txtDataCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDataCount.Name = "txtDataCount";
            this.txtDataCount.ReadOnly = true;
            this.txtDataCount.Size = new System.Drawing.Size(67, 25);
            this.txtDataCount.TabIndex = 221;
            // 
            // txtTarg_LT_WS
            // 
            this.txtTarg_LT_WS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTarg_LT_WS.Location = new System.Drawing.Point(581, 207);
            this.txtTarg_LT_WS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTarg_LT_WS.Name = "txtTarg_LT_WS";
            this.txtTarg_LT_WS.ReadOnly = true;
            this.txtTarg_LT_WS.Size = new System.Drawing.Size(70, 25);
            this.txtTarg_LT_WS.TabIndex = 220;
            // 
            // txtRef_LT_WS
            // 
            this.txtRef_LT_WS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRef_LT_WS.Location = new System.Drawing.Point(498, 207);
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
            this.label113.Location = new System.Drawing.Point(379, 209);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(110, 18);
            this.label113.TabIndex = 218;
            this.label113.Text = "Avg. LT WS (m/s)";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.Location = new System.Drawing.Point(463, 243);
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
            this.cboMCP_WD.Location = new System.Drawing.Point(467, 261);
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
            this.txtTargAvgWS.Location = new System.Drawing.Point(581, 177);
            this.txtTargAvgWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTargAvgWS.Name = "txtTargAvgWS";
            this.txtTargAvgWS.ReadOnly = true;
            this.txtTargAvgWS.Size = new System.Drawing.Size(70, 25);
            this.txtTargAvgWS.TabIndex = 215;
            // 
            // txtRefAvgWS
            // 
            this.txtRefAvgWS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefAvgWS.Location = new System.Drawing.Point(498, 177);
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
            this.label115.Location = new System.Drawing.Point(356, 177);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(128, 18);
            this.label115.TabIndex = 213;
            this.label115.Text = "Avg. Conc. WS (m/s)";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label116.Location = new System.Drawing.Point(580, 155);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(71, 18);
            this.label116.TabIndex = 212;
            this.label116.Text = "Target Site";
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.Location = new System.Drawing.Point(505, 155);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(55, 18);
            this.label117.TabIndex = 211;
            this.label117.Text = "Ref. Site";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(15, 64);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(78, 18);
            this.label107.TabIndex = 208;
            this.label107.Text = "Target Met :";
            // 
            // cboMCP_Met
            // 
            this.cboMCP_Met.FormattingEnabled = true;
            this.cboMCP_Met.Location = new System.Drawing.Point(99, 57);
            this.cboMCP_Met.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMCP_Met.Name = "cboMCP_Met";
            this.cboMCP_Met.Size = new System.Drawing.Size(349, 26);
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
            this.pgeMetSumm.AutoScroll = true;
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
            this.pgeMetSumm.Size = new System.Drawing.Size(1488, 712);
            this.pgeMetSumm.TabIndex = 12;
            this.pgeMetSumm.Text = "Met && Turbine Summary";
            this.pgeMetSumm.UseVisualStyleBackColor = true;
            // 
            // plotDW_DH
            // 
            this.plotDW_DH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotDW_DH.Location = new System.Drawing.Point(1114, 484);
            this.plotDW_DH.Name = "plotDW_DH";
            this.plotDW_DH.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDW_DH.Size = new System.Drawing.Size(360, 215);
            this.plotDW_DH.TabIndex = 221;
            this.plotDW_DH.Text = "plotView1";
            this.plotDW_DH.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDW_DH.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDW_DH.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotUW_DH
            // 
            this.plotUW_DH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotUW_DH.Location = new System.Drawing.Point(745, 484);
            this.plotUW_DH.Name = "plotUW_DH";
            this.plotUW_DH.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotUW_DH.Size = new System.Drawing.Size(360, 215);
            this.plotUW_DH.TabIndex = 220;
            this.plotUW_DH.Text = "plotView1";
            this.plotUW_DH.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotUW_DH.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotUW_DH.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotDW_SR
            // 
            this.plotDW_SR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotDW_SR.Location = new System.Drawing.Point(376, 484);
            this.plotDW_SR.Name = "plotDW_SR";
            this.plotDW_SR.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDW_SR.Size = new System.Drawing.Size(360, 215);
            this.plotDW_SR.TabIndex = 219;
            this.plotDW_SR.Text = "plotView1";
            this.plotDW_SR.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDW_SR.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDW_SR.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotUW_SR
            // 
            this.plotUW_SR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotUW_SR.Location = new System.Drawing.Point(10, 484);
            this.plotUW_SR.Name = "plotUW_SR";
            this.plotUW_SR.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotUW_SR.Size = new System.Drawing.Size(360, 215);
            this.plotUW_SR.TabIndex = 218;
            this.plotUW_SR.Text = "plotView1";
            this.plotUW_SR.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotUW_SR.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotUW_SR.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotElev
            // 
            this.plotElev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotElev.Location = new System.Drawing.Point(1117, 263);
            this.plotElev.Name = "plotElev";
            this.plotElev.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotElev.Size = new System.Drawing.Size(360, 215);
            this.plotElev.TabIndex = 217;
            this.plotElev.Text = "plotView1";
            this.plotElev.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotElev.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotElev.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotDWUWExpo
            // 
            this.plotDWUWExpo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotDWUWExpo.Location = new System.Drawing.Point(748, 263);
            this.plotDWUWExpo.Name = "plotDWUWExpo";
            this.plotDWUWExpo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDWUWExpo.Size = new System.Drawing.Size(360, 215);
            this.plotDWUWExpo.TabIndex = 216;
            this.plotDWUWExpo.Text = "plotView1";
            this.plotDWUWExpo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDWUWExpo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDWUWExpo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotDWExpo
            // 
            this.plotDWExpo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotDWExpo.Location = new System.Drawing.Point(379, 263);
            this.plotDWExpo.Name = "plotDWExpo";
            this.plotDWExpo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDWExpo.Size = new System.Drawing.Size(360, 215);
            this.plotDWExpo.TabIndex = 215;
            this.plotDWExpo.Text = "plotView1";
            this.plotDWExpo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDWExpo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDWExpo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotUWExpo
            // 
            this.plotUWExpo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotUWExpo.Location = new System.Drawing.Point(10, 263);
            this.plotUWExpo.Name = "plotUWExpo";
            this.plotUWExpo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotUWExpo.Size = new System.Drawing.Size(360, 215);
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
            this.Label80.Location = new System.Drawing.Point(1238, 8);
            this.Label80.Name = "Label80";
            this.Label80.Size = new System.Drawing.Size(242, 19);
            this.Label80.TabIndex = 175;
            this.Label80.Text = "Turbine Site Statistics (selected)";
            // 
            // lstTurbStats
            // 
            this.lstTurbStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstTurbStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader21,
            this.ColumnHeader34,
            this.ColumnHeader35,
            this.ColumnHeader22,
            this.ColumnHeader33});
            this.lstTurbStats.HideSelection = false;
            this.lstTurbStats.Location = new System.Drawing.Point(1242, 36);
            this.lstTurbStats.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstTurbStats.MultiSelect = false;
            this.lstTurbStats.Name = "lstTurbStats";
            this.lstTurbStats.Size = new System.Drawing.Size(238, 217);
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
            this.chkTurbSummAll.Location = new System.Drawing.Point(894, 28);
            this.chkTurbSummAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbSummAll.Name = "chkTurbSummAll";
            this.chkTurbSummAll.Size = new System.Drawing.Size(88, 36);
            this.chkTurbSummAll.TabIndex = 173;
            this.chkTurbSummAll.Text = "Select/\r\nDeselect All";
            this.chkTurbSummAll.UseVisualStyleBackColor = true;
            this.chkTurbSummAll.CheckedChanged += new System.EventHandler(this.chkTurbSummAll_CheckedChanged);
            // 
            // chkTurbSumm
            // 
            this.chkTurbSumm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTurbSumm.CheckOnClick = true;
            this.chkTurbSumm.FormattingEnabled = true;
            this.chkTurbSumm.HorizontalScrollbar = true;
            this.chkTurbSumm.Location = new System.Drawing.Point(891, 72);
            this.chkTurbSumm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbSumm.Name = "chkTurbSumm";
            this.chkTurbSumm.Size = new System.Drawing.Size(91, 144);
            this.chkTurbSumm.TabIndex = 172;
            this.chkTurbSumm.SelectedIndexChanged += new System.EventHandler(this.chkTurbSumm_SelectedIndexChanged);
            // 
            // Label78
            // 
            this.Label78.AutoSize = true;
            this.Label78.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label78.Location = new System.Drawing.Point(886, 7);
            this.Label78.Name = "Label78";
            this.Label78.Size = new System.Drawing.Size(88, 16);
            this.Label78.TabIndex = 171;
            this.Label78.Text = "Turbine Sites";
            // 
            // chkMetSummAll
            // 
            this.chkMetSummAll.AutoSize = true;
            this.chkMetSummAll.Checked = true;
            this.chkMetSummAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMetSummAll.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMetSummAll.Location = new System.Drawing.Point(789, 28);
            this.chkMetSummAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetSummAll.Name = "chkMetSummAll";
            this.chkMetSummAll.Size = new System.Drawing.Size(88, 36);
            this.chkMetSummAll.TabIndex = 170;
            this.chkMetSummAll.Text = "Select/\r\nDeselect All";
            this.chkMetSummAll.UseVisualStyleBackColor = true;
            this.chkMetSummAll.CheckedChanged += new System.EventHandler(this.chkMetSummAll_CheckedChanged);
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(788, 8);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(64, 16);
            this.Label12.TabIndex = 169;
            this.Label12.Text = "Met Sites";
            // 
            // chkMetSumm
            // 
            this.chkMetSumm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkMetSumm.CheckOnClick = true;
            this.chkMetSumm.FormattingEnabled = true;
            this.chkMetSumm.HorizontalScrollbar = true;
            this.chkMetSumm.Location = new System.Drawing.Point(779, 73);
            this.chkMetSumm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetSumm.Name = "chkMetSumm";
            this.chkMetSumm.Size = new System.Drawing.Size(101, 144);
            this.chkMetSumm.TabIndex = 168;
            this.chkMetSumm.SelectedIndexChanged += new System.EventHandler(this.chkMetSumm_SelectedIndexChanged);
            // 
            // Label76
            // 
            this.Label76.AutoSize = true;
            this.Label76.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label76.Location = new System.Drawing.Point(989, 10);
            this.Label76.Name = "Label76";
            this.Label76.Size = new System.Drawing.Size(213, 19);
            this.Label76.TabIndex = 166;
            this.Label76.Text = "Met Site Statistics (selected)";
            // 
            // lstMetStats
            // 
            this.lstMetStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstMetStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader42,
            this.ColumnHeader91,
            this.ColumnHeader93,
            this.ColumnHeader92,
            this.ColumnHeader52});
            this.lstMetStats.HideSelection = false;
            this.lstMetStats.Location = new System.Drawing.Point(992, 38);
            this.lstMetStats.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstMetStats.MultiSelect = false;
            this.lstMetStats.Name = "lstMetStats";
            this.lstMetStats.Size = new System.Drawing.Size(237, 217);
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
            this.btnExportExpoSRDH.Location = new System.Drawing.Point(668, 15);
            this.btnExportExpoSRDH.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportExpoSRDH.Name = "btnExportExpoSRDH";
            this.btnExportExpoSRDH.Size = new System.Drawing.Size(101, 43);
            this.btnExportExpoSRDH.TabIndex = 164;
            this.btnExportExpoSRDH.Text = "Export Values";
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
            this.lstMetSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lstMetSummary.Size = new System.Drawing.Size(759, 178);
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
            this.pgeGrossTurbs.AutoScroll = true;
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
            this.pgeGrossTurbs.Size = new System.Drawing.Size(1488, 712);
            this.pgeGrossTurbs.TabIndex = 3;
            this.pgeGrossTurbs.Text = "Gross Turbine Ests.";
            this.pgeGrossTurbs.UseVisualStyleBackColor = true;
            // 
            // plotGrossHisto
            // 
            this.plotGrossHisto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotGrossHisto.Location = new System.Drawing.Point(293, 478);
            this.plotGrossHisto.Name = "plotGrossHisto";
            this.plotGrossHisto.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGrossHisto.Size = new System.Drawing.Size(531, 210);
            this.plotGrossHisto.TabIndex = 219;
            this.plotGrossHisto.Text = "plotView1";
            this.plotGrossHisto.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotGrossHisto.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotGrossHisto.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotGross_PowerCrvs
            // 
            this.plotGross_PowerCrvs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotGross_PowerCrvs.Location = new System.Drawing.Point(1087, 355);
            this.plotGross_PowerCrvs.MaximumSize = new System.Drawing.Size(400, 350);
            this.plotGross_PowerCrvs.Name = "plotGross_PowerCrvs";
            this.plotGross_PowerCrvs.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGross_PowerCrvs.Size = new System.Drawing.Size(369, 333);
            this.plotGross_PowerCrvs.TabIndex = 218;
            this.plotGross_PowerCrvs.Text = "plotView1";
            this.plotGross_PowerCrvs.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotGross_PowerCrvs.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotGross_PowerCrvs.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotGrossWS_Dist
            // 
            this.plotGrossWS_Dist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotGrossWS_Dist.Location = new System.Drawing.Point(1087, 13);
            this.plotGrossWS_Dist.MaximumSize = new System.Drawing.Size(400, 350);
            this.plotGrossWS_Dist.Name = "plotGrossWS_Dist";
            this.plotGrossWS_Dist.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGrossWS_Dist.Size = new System.Drawing.Size(384, 312);
            this.plotGrossWS_Dist.TabIndex = 217;
            this.plotGrossWS_Dist.Text = "plotView1";
            this.plotGrossWS_Dist.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotGrossWS_Dist.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotGrossWS_Dist.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotGrossWindRose
            // 
            this.plotGrossWindRose.Location = new System.Drawing.Point(1087, 13);
            this.plotGrossWindRose.Name = "plotGrossWindRose";
            this.plotGrossWindRose.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGrossWindRose.Size = new System.Drawing.Size(369, 312);
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
            this.lstPowerCurveList.Location = new System.Drawing.Point(871, 140);
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
            this.txtisMCPdGross.Location = new System.Drawing.Point(477, 74);
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
            this.txtGross_FlowSepUsed.Location = new System.Drawing.Point(776, 74);
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
            this.Label96.Location = new System.Drawing.Point(891, 427);
            this.Label96.Name = "Label96";
            this.Label96.Size = new System.Drawing.Size(131, 18);
            this.Label96.TabIndex = 181;
            this.Label96.Text = "Histogram of Ests.";
            // 
            // lstGrossHisto
            // 
            this.lstGrossHisto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstGrossHisto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader103,
            this.ColumnHeader104,
            this.ColumnHeader105});
            this.lstGrossHisto.HideSelection = false;
            this.lstGrossHisto.Location = new System.Drawing.Point(871, 456);
            this.lstGrossHisto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstGrossHisto.Name = "lstGrossHisto";
            this.lstGrossHisto.Size = new System.Drawing.Size(182, 232);
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
            this.txtGross_LC_used.Location = new System.Drawing.Point(621, 74);
            this.txtGross_LC_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGross_LC_used.Name = "txtGross_LC_used";
            this.txtGross_LC_used.ReadOnly = true;
            this.txtGross_LC_used.Size = new System.Drawing.Size(146, 25);
            this.txtGross_LC_used.TabIndex = 179;
            // 
            // Label83
            // 
            this.Label83.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label83.AutoSize = true;
            this.Label83.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label83.Location = new System.Drawing.Point(25, 465);
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
            this.chkMetGrossAll.Location = new System.Drawing.Point(584, 132);
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
            this.Label50.Location = new System.Drawing.Point(607, 109);
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
            this.chkMetGross.Location = new System.Drawing.Point(587, 158);
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
            this.Label77.Location = new System.Drawing.Point(589, 408);
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
            this.cboGrossParam.Location = new System.Drawing.Point(589, 434);
            this.cboGrossParam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboGrossParam.Name = "cboGrossParam";
            this.cboGrossParam.Size = new System.Drawing.Size(126, 26);
            this.cboGrossParam.TabIndex = 160;
            this.cboGrossParam.Text = "Wind Speed";
            this.cboGrossParam.SelectedIndexChanged += new System.EventHandler(this.cboGrossParam_SelectedIndexChanged);
            // 
            // btnExportDirWSDists
            // 
            this.btnExportDirWSDists.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportDirWSDists.Location = new System.Drawing.Point(837, 11);
            this.btnExportDirWSDists.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportDirWSDists.Name = "btnExportDirWSDists";
            this.btnExportDirWSDists.Size = new System.Drawing.Size(117, 51);
            this.btnExportDirWSDists.TabIndex = 102;
            this.btnExportDirWSDists.Text = "Export Directional WS Dists";
            this.btnExportDirWSDists.UseVisualStyleBackColor = true;
            this.btnExportDirWSDists.Click += new System.EventHandler(this.btnExportDirWSDists_Click);
            // 
            // Label71
            // 
            this.Label71.AutoSize = true;
            this.Label71.Location = new System.Drawing.Point(590, 329);
            this.Label71.Name = "Label71";
            this.Label71.Size = new System.Drawing.Size(79, 19);
            this.Label71.TabIndex = 159;
            this.Label71.Text = "WD sector:";
            // 
            // cboGrossWD
            // 
            this.cboGrossWD.FormattingEnabled = true;
            this.cboGrossWD.Location = new System.Drawing.Point(592, 354);
            this.cboGrossWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboGrossWD.Name = "cboGrossWD";
            this.cboGrossWD.Size = new System.Drawing.Size(115, 26);
            this.cboGrossWD.TabIndex = 158;
            this.cboGrossWD.SelectedIndexChanged += new System.EventHandler(this.cboGrossTurbWD_SelectedIndexChanged);
            // 
            // btnExportDirWS
            // 
            this.btnExportDirWS.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.btnExportDirWS.Location = new System.Drawing.Point(712, 11);
            this.btnExportDirWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportDirWS.Name = "btnExportDirWS";
            this.btnExportDirWS.Size = new System.Drawing.Size(111, 51);
            this.btnExportDirWS.TabIndex = 157;
            this.btnExportDirWS.Text = "Export Sectorwise WS && Weibull";
            this.btnExportDirWS.UseVisualStyleBackColor = true;
            this.btnExportDirWS.Click += new System.EventHandler(this.btnExportDirWS_Click);
            // 
            // btnExportCRV
            // 
            this.btnExportCRV.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportCRV.Location = new System.Drawing.Point(886, 333);
            this.btnExportCRV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportCRV.Name = "btnExportCRV";
            this.btnExportCRV.Size = new System.Drawing.Size(140, 34);
            this.btnExportCRV.TabIndex = 153;
            this.btnExportCRV.Text = "Export Power Curve";
            this.btnExportCRV.UseVisualStyleBackColor = true;
            this.btnExportCRV.Click += new System.EventHandler(this.btnExportCRV_Click);
            // 
            // btnExportWSDists
            // 
            this.btnExportWSDists.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportWSDists.Location = new System.Drawing.Point(613, 11);
            this.btnExportWSDists.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportWSDists.Name = "btnExportWSDists";
            this.btnExportWSDists.Size = new System.Drawing.Size(85, 51);
            this.btnExportWSDists.TabIndex = 101;
            this.btnExportWSDists.Text = "Export WS Dists";
            this.btnExportWSDists.UseVisualStyleBackColor = true;
            this.btnExportWSDists.Click += new System.EventHandler(this.btnExportWSDists_Click);
            // 
            // btnWS_AEP_Exprt
            // 
            this.btnWS_AEP_Exprt.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnWS_AEP_Exprt.Location = new System.Drawing.Point(495, 11);
            this.btnWS_AEP_Exprt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnWS_AEP_Exprt.Name = "btnWS_AEP_Exprt";
            this.btnWS_AEP_Exprt.Size = new System.Drawing.Size(104, 51);
            this.btnWS_AEP_Exprt.TabIndex = 100;
            this.btnWS_AEP_Exprt.Text = "Export WS, Weibull && AEP";
            this.btnWS_AEP_Exprt.UseVisualStyleBackColor = true;
            this.btnWS_AEP_Exprt.Click += new System.EventHandler(this.btnWS_AEP_Exprt_Click);
            // 
            // txtAEPMax
            // 
            this.txtAEPMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAEPMax.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAEPMax.Location = new System.Drawing.Point(196, 639);
            this.txtAEPMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAEPMax.Name = "txtAEPMax";
            this.txtAEPMax.ReadOnly = true;
            this.txtAEPMax.Size = new System.Drawing.Size(68, 25);
            this.txtAEPMax.TabIndex = 98;
            // 
            // txtAEPMin
            // 
            this.txtAEPMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAEPMin.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAEPMin.Location = new System.Drawing.Point(196, 603);
            this.txtAEPMin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAEPMin.Name = "txtAEPMin";
            this.txtAEPMin.ReadOnly = true;
            this.txtAEPMin.Size = new System.Drawing.Size(68, 25);
            this.txtAEPMin.TabIndex = 97;
            // 
            // txtAEPSD
            // 
            this.txtAEPSD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAEPSD.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAEPSD.Location = new System.Drawing.Point(196, 565);
            this.txtAEPSD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAEPSD.Name = "txtAEPSD";
            this.txtAEPSD.ReadOnly = true;
            this.txtAEPSD.Size = new System.Drawing.Size(68, 25);
            this.txtAEPSD.TabIndex = 96;
            // 
            // txtAEPAvg
            // 
            this.txtAEPAvg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAEPAvg.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAEPAvg.Location = new System.Drawing.Point(196, 528);
            this.txtAEPAvg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAEPAvg.Name = "txtAEPAvg";
            this.txtAEPAvg.ReadOnly = true;
            this.txtAEPAvg.Size = new System.Drawing.Size(68, 25);
            this.txtAEPAvg.TabIndex = 95;
            // 
            // Label35
            // 
            this.Label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label35.AutoSize = true;
            this.Label35.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label35.Location = new System.Drawing.Point(212, 484);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(50, 36);
            this.Label35.TabIndex = 94;
            this.Label35.Text = "  AEP \r\n(MWh)";
            // 
            // Label34
            // 
            this.Label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label34.AutoSize = true;
            this.Label34.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label34.Location = new System.Drawing.Point(110, 484);
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
            this.chkTurbGrossAll.Location = new System.Drawing.Point(729, 129);
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
            this.Label9.Location = new System.Drawing.Point(752, 108);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(72, 19);
            this.Label9.TabIndex = 90;
            this.Label9.Text = "Turbines";
            // 
            // chkTurbGross
            // 
            this.chkTurbGross.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTurbGross.CheckOnClick = true;
            this.chkTurbGross.FormattingEnabled = true;
            this.chkTurbGross.HorizontalScrollbar = true;
            this.chkTurbGross.Location = new System.Drawing.Point(733, 158);
            this.chkTurbGross.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbGross.Name = "chkTurbGross";
            this.chkTurbGross.Size = new System.Drawing.Size(116, 284);
            this.chkTurbGross.TabIndex = 88;
            this.chkTurbGross.SelectedIndexChanged += new System.EventHandler(this.chkTurbGross_SelectedIndexChanged);
            // 
            // btnDelPowerCrv
            // 
            this.btnDelPowerCrv.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnDelPowerCrv.Location = new System.Drawing.Point(886, 375);
            this.btnDelPowerCrv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelPowerCrv.Name = "btnDelPowerCrv";
            this.btnDelPowerCrv.Size = new System.Drawing.Size(140, 34);
            this.btnDelPowerCrv.TabIndex = 85;
            this.btnDelPowerCrv.Text = "Delete Power Curve";
            this.btnDelPowerCrv.UseVisualStyleBackColor = true;
            this.btnDelPowerCrv.Click += new System.EventHandler(this.btnDelPowerCrv_Click);
            // 
            // btnImportCRV
            // 
            this.btnImportCRV.BackColor = System.Drawing.Color.LightCoral;
            this.btnImportCRV.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnImportCRV.Location = new System.Drawing.Point(886, 291);
            this.btnImportCRV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportCRV.Name = "btnImportCRV";
            this.btnImportCRV.Size = new System.Drawing.Size(140, 34);
            this.btnImportCRV.TabIndex = 34;
            this.btnImportCRV.Text = "Import Power Curve";
            this.btnImportCRV.UseVisualStyleBackColor = false;
            this.btnImportCRV.Click += new System.EventHandler(this.btnImportCRV_Click);
            // 
            // txtMax
            // 
            this.txtMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMax.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMax.Location = new System.Drawing.Point(108, 639);
            this.txtMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMax.Name = "txtMax";
            this.txtMax.ReadOnly = true;
            this.txtMax.Size = new System.Drawing.Size(68, 25);
            this.txtMax.TabIndex = 14;
            // 
            // txtMin
            // 
            this.txtMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMin.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMin.Location = new System.Drawing.Point(108, 602);
            this.txtMin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMin.Name = "txtMin";
            this.txtMin.ReadOnly = true;
            this.txtMin.Size = new System.Drawing.Size(68, 25);
            this.txtMin.TabIndex = 13;
            // 
            // txtCount
            // 
            this.txtCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCount.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCount.Location = new System.Drawing.Point(108, 675);
            this.txtCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(68, 25);
            this.txtCount.TabIndex = 10;
            // 
            // txtStDev
            // 
            this.txtStDev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtStDev.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStDev.Location = new System.Drawing.Point(108, 565);
            this.txtStDev.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStDev.Name = "txtStDev";
            this.txtStDev.ReadOnly = true;
            this.txtStDev.Size = new System.Drawing.Size(68, 25);
            this.txtStDev.TabIndex = 9;
            // 
            // txtAvg
            // 
            this.txtAvg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAvg.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvg.Location = new System.Drawing.Point(108, 528);
            this.txtAvg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAvg.Name = "txtAvg";
            this.txtAvg.ReadOnly = true;
            this.txtAvg.Size = new System.Drawing.Size(68, 25);
            this.txtAvg.TabIndex = 8;
            // 
            // lblMax
            // 
            this.lblMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMax.AutoSize = true;
            this.lblMax.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMax.Location = new System.Drawing.Point(26, 638);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(76, 18);
            this.lblMax.TabIndex = 12;
            this.lblMax.Text = "Maximum :";
            // 
            // lblMin
            // 
            this.lblMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMin.Location = new System.Drawing.Point(31, 605);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(74, 18);
            this.lblMin.TabIndex = 11;
            this.lblMin.Text = "Minimum :";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(45, 678);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(51, 18);
            this.lblCount.TabIndex = 7;
            this.lblCount.Text = "Count :";
            // 
            // lblStdev
            // 
            this.lblStdev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStdev.AutoSize = true;
            this.lblStdev.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStdev.Location = new System.Drawing.Point(38, 568);
            this.lblStdev.Name = "lblStdev";
            this.lblStdev.Size = new System.Drawing.Size(58, 18);
            this.lblStdev.TabIndex = 6;
            this.lblStdev.Text = "St. Dev. :";
            // 
            // lblAvg
            // 
            this.lblAvg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAvg.AutoSize = true;
            this.lblAvg.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvg.Location = new System.Drawing.Point(32, 531);
            this.lblAvg.Name = "lblAvg";
            this.lblAvg.Size = new System.Drawing.Size(63, 18);
            this.lblAvg.TabIndex = 5;
            this.lblAvg.Text = "Average :";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(878, 110);
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
            this.lstGrossTurbEst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lstGrossTurbEst.Size = new System.Drawing.Size(549, 339);
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
            this.pgeExceedance.AutoScroll = true;
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
            this.pgeExceedance.Size = new System.Drawing.Size(1488, 712);
            this.pgeExceedance.TabIndex = 19;
            this.pgeExceedance.Text = "Exceedance Modeling";
            this.pgeExceedance.UseVisualStyleBackColor = true;
            // 
            // plotCompositeExceed
            // 
            this.plotCompositeExceed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotCompositeExceed.Location = new System.Drawing.Point(1031, 452);
            this.plotCompositeExceed.Name = "plotCompositeExceed";
            this.plotCompositeExceed.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotCompositeExceed.Size = new System.Drawing.Size(437, 245);
            this.plotCompositeExceed.TabIndex = 237;
            this.plotCompositeExceed.Text = "plotView1";
            this.plotCompositeExceed.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotCompositeExceed.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotCompositeExceed.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotExceedCurves
            // 
            this.plotExceedCurves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotExceedCurves.Location = new System.Drawing.Point(948, 18);
            this.plotExceedCurves.Name = "plotExceedCurves";
            this.plotExceedCurves.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotExceedCurves.Size = new System.Drawing.Size(520, 395);
            this.plotExceedCurves.TabIndex = 236;
            this.plotExceedCurves.Text = "plotView1";
            this.plotExceedCurves.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotExceedCurves.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotExceedCurves.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnImportCurves
            // 
            this.btnImportCurves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportCurves.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportCurves.Location = new System.Drawing.Point(490, 418);
            this.btnImportCurves.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportCurves.Name = "btnImportCurves";
            this.btnImportCurves.Size = new System.Drawing.Size(140, 35);
            this.btnImportCurves.TabIndex = 235;
            this.btnImportCurves.Text = "Import Curves";
            this.btnImportCurves.UseVisualStyleBackColor = true;
            this.btnImportCurves.Click += new System.EventHandler(this.btnImportCurves_Click);
            // 
            // btnGetDefaultExceed
            // 
            this.btnGetDefaultExceed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetDefaultExceed.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetDefaultExceed.Location = new System.Drawing.Point(636, 418);
            this.btnGetDefaultExceed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGetDefaultExceed.Name = "btnGetDefaultExceed";
            this.btnGetDefaultExceed.Size = new System.Drawing.Size(140, 35);
            this.btnGetDefaultExceed.TabIndex = 234;
            this.btnGetDefaultExceed.Text = "Set to Default";
            this.btnGetDefaultExceed.UseVisualStyleBackColor = true;
            this.btnGetDefaultExceed.Click += new System.EventHandler(this.btnGetDefaultExceed_Click);
            // 
            // cboExceedWake
            // 
            this.cboExceedWake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboExceedWake.FormattingEnabled = true;
            this.cboExceedWake.Location = new System.Drawing.Point(529, 485);
            this.cboExceedWake.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExceedWake.Name = "cboExceedWake";
            this.cboExceedWake.Size = new System.Drawing.Size(312, 26);
            this.cboExceedWake.TabIndex = 231;
            this.cboExceedWake.SelectedIndexChanged += new System.EventHandler(this.cboExceedWake_SelectedIndexChanged);
            // 
            // label185
            // 
            this.label185.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label185.AutoSize = true;
            this.label185.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label185.Location = new System.Drawing.Point(531, 463);
            this.label185.Name = "label185";
            this.label185.Size = new System.Drawing.Size(128, 18);
            this.label185.TabIndex = 230;
            this.label185.Text = "Net Estimate Model:";
            // 
            // label183
            // 
            this.label183.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label183.AutoSize = true;
            this.label183.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label183.Location = new System.Drawing.Point(374, 463);
            this.label183.Name = "label183";
            this.label183.Size = new System.Drawing.Size(61, 18);
            this.label183.TabIndex = 220;
            this.label183.Text = "Turbine :";
            // 
            // cboExceedTurbine
            // 
            this.cboExceedTurbine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboExceedTurbine.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboExceedTurbine.FormattingEnabled = true;
            this.cboExceedTurbine.Location = new System.Drawing.Point(377, 485);
            this.cboExceedTurbine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExceedTurbine.Name = "cboExceedTurbine";
            this.cboExceedTurbine.Size = new System.Drawing.Size(134, 26);
            this.cboExceedTurbine.TabIndex = 219;
            this.cboExceedTurbine.SelectedIndexChanged += new System.EventHandler(this.cboExceedTurbine_SelectedIndexChanged);
            // 
            // btn_AddProj
            // 
            this.btn_AddProj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_AddProj.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddProj.Location = new System.Drawing.Point(29, 418);
            this.btn_AddProj.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddProj.Name = "btn_AddProj";
            this.btn_AddProj.Size = new System.Drawing.Size(129, 35);
            this.btn_AddProj.TabIndex = 44;
            this.btn_AddProj.Text = "Add Curve";
            this.btn_AddProj.UseVisualStyleBackColor = true;
            this.btn_AddProj.Click += new System.EventHandler(this.btn_AddProj_Click);
            // 
            // btnExportAllPVals
            // 
            this.btnExportAllPVals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportAllPVals.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportAllPVals.Location = new System.Drawing.Point(878, 617);
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
            this.btnDeleteExceed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteExceed.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteExceed.Location = new System.Drawing.Point(331, 418);
            this.btnDeleteExceed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDeleteExceed.Name = "btnDeleteExceed";
            this.btnDeleteExceed.Size = new System.Drawing.Size(152, 35);
            this.btnDeleteExceed.TabIndex = 38;
            this.btnDeleteExceed.Text = "Delete Curve(s)";
            this.btnDeleteExceed.UseVisualStyleBackColor = true;
            this.btnDeleteExceed.Click += new System.EventHandler(this.btnDeleteExceed_Click);
            // 
            // btnExportCurves
            // 
            this.btnExportCurves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportCurves.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCurves.Location = new System.Drawing.Point(782, 419);
            this.btnExportCurves.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportCurves.Name = "btnExportCurves";
            this.btnExportCurves.Size = new System.Drawing.Size(140, 35);
            this.btnExportCurves.TabIndex = 37;
            this.btnExportCurves.Text = "Export Curves";
            this.btnExportCurves.UseVisualStyleBackColor = true;
            this.btnExportCurves.Click += new System.EventHandler(this.btnExportCurves_Click);
            // 
            // btnExport_P_Vals
            // 
            this.btnExport_P_Vals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport_P_Vals.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport_P_Vals.Location = new System.Drawing.Point(877, 561);
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
            this.label180.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label180.AutoSize = true;
            this.label180.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label180.Location = new System.Drawing.Point(33, 483);
            this.label180.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(273, 23);
            this.label180.TabIndex = 33;
            this.label180.Text = "Table of Exceedance Values";
            // 
            // lstPvals
            // 
            this.lstPvals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lstPvals.Location = new System.Drawing.Point(31, 519);
            this.lstPvals.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.lstPvals.Name = "lstPvals";
            this.lstPvals.Size = new System.Drawing.Size(810, 178);
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
            this.chkShowCDFs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.chkShowPDF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnDoMonteCarlo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDoMonteCarlo.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoMonteCarlo.Location = new System.Drawing.Point(881, 473);
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
            this.label181.Location = new System.Drawing.Point(22, 18);
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
            this.label182.Location = new System.Drawing.Point(22, 55);
            this.label182.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label182.Name = "label182";
            this.label182.Size = new System.Drawing.Size(383, 23);
            this.label182.TabIndex = 27;
            this.label182.Text = "Summary of Defined Exceedance curves";
            // 
            // lstDefinedLosses
            // 
            this.lstDefinedLosses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lstDefinedLosses.Location = new System.Drawing.Point(27, 82);
            this.lstDefinedLosses.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.lstDefinedLosses.Name = "lstDefinedLosses";
            this.lstDefinedLosses.Size = new System.Drawing.Size(886, 322);
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
            this.btn_editloss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_editloss.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_editloss.Location = new System.Drawing.Point(164, 418);
            this.btn_editloss.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_editloss.Name = "btn_editloss";
            this.btn_editloss.Size = new System.Drawing.Size(160, 35);
            this.btn_editloss.TabIndex = 25;
            this.btn_editloss.Text = "Edit Curve";
            this.btn_editloss.UseVisualStyleBackColor = true;
            this.btn_editloss.Click += new System.EventHandler(this.btn_editloss_Click);
            // 
            // pgeNetEsts
            // 
            this.pgeNetEsts.AutoScroll = true;
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
            this.pgeNetEsts.Size = new System.Drawing.Size(1488, 712);
            this.pgeNetEsts.TabIndex = 13;
            this.pgeNetEsts.Text = "Net Turbine Ests";
            this.pgeNetEsts.UseVisualStyleBackColor = true;
            // 
            // plotWakeMap
            // 
            this.plotWakeMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotWakeMap.Location = new System.Drawing.Point(983, 202);
            this.plotWakeMap.Name = "plotWakeMap";
            this.plotWakeMap.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotWakeMap.Size = new System.Drawing.Size(492, 422);
            this.plotWakeMap.TabIndex = 240;
            this.plotWakeMap.Text = "plotView1";
            this.plotWakeMap.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotWakeMap.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotWakeMap.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotTurbsByString
            // 
            this.plotTurbsByString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotTurbsByString.Location = new System.Drawing.Point(499, 503);
            this.plotTurbsByString.Name = "plotTurbsByString";
            this.plotTurbsByString.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTurbsByString.Size = new System.Drawing.Size(445, 195);
            this.plotTurbsByString.TabIndex = 239;
            this.plotTurbsByString.Text = "plotView1";
            this.plotTurbsByString.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTurbsByString.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTurbsByString.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotWakedDists
            // 
            this.plotWakedDists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotWakedDists.Location = new System.Drawing.Point(21, 503);
            this.plotWakedDists.Name = "plotWakedDists";
            this.plotWakedDists.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotWakedDists.Size = new System.Drawing.Size(445, 195);
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
            this.btnExportNetDirWSDists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportNetDirWSDists.Location = new System.Drawing.Point(134, 446);
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
            this.btnExportNetDirWS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportNetDirWS.Location = new System.Drawing.Point(278, 446);
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
            this.btnExportNetWSDists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportNetWSDists.Location = new System.Drawing.Point(21, 446);
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
            this.btnRefreshWakeMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshWakeMap.Location = new System.Drawing.Point(1305, 637);
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
            this.chkWakeAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkWakeAuto.AutoSize = true;
            this.chkWakeAuto.Checked = true;
            this.chkWakeAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWakeAuto.Location = new System.Drawing.Point(1104, 674);
            this.chkWakeAuto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkWakeAuto.Name = "chkWakeAuto";
            this.chkWakeAuto.Size = new System.Drawing.Size(176, 23);
            this.chkWakeAuto.TabIndex = 187;
            this.chkWakeAuto.Text = "Use Auto Min and Max";
            this.chkWakeAuto.UseVisualStyleBackColor = true;
            // 
            // txtWakeInterval
            // 
            this.txtWakeInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWakeInterval.Location = new System.Drawing.Point(1037, 672);
            this.txtWakeInterval.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWakeInterval.Name = "txtWakeInterval";
            this.txtWakeInterval.Size = new System.Drawing.Size(56, 25);
            this.txtWakeInterval.TabIndex = 186;
            // 
            // txtWakeMax
            // 
            this.txtWakeMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWakeMax.Location = new System.Drawing.Point(1146, 641);
            this.txtWakeMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWakeMax.Name = "txtWakeMax";
            this.txtWakeMax.Size = new System.Drawing.Size(61, 25);
            this.txtWakeMax.TabIndex = 184;
            // 
            // txtWakeMin
            // 
            this.txtWakeMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWakeMin.Location = new System.Drawing.Point(1035, 641);
            this.txtWakeMin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWakeMin.Name = "txtWakeMin";
            this.txtWakeMin.Size = new System.Drawing.Size(61, 25);
            this.txtWakeMin.TabIndex = 182;
            // 
            // Label97
            // 
            this.Label97.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label97.AutoSize = true;
            this.Label97.Location = new System.Drawing.Point(974, 674);
            this.Label97.Name = "Label97";
            this.Label97.Size = new System.Drawing.Size(63, 19);
            this.Label97.TabIndex = 185;
            this.Label97.Text = "Interval:";
            // 
            // Label98
            // 
            this.Label98.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label98.AutoSize = true;
            this.Label98.Location = new System.Drawing.Point(1099, 639);
            this.Label98.Name = "Label98";
            this.Label98.Size = new System.Drawing.Size(41, 19);
            this.Label98.TabIndex = 183;
            this.Label98.Text = "Max:";
            // 
            // Label99
            // 
            this.Label99.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label99.AutoSize = true;
            this.Label99.Location = new System.Drawing.Point(989, 640);
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
            this.btnCreateWakeMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.chkTurbNet.Location = new System.Drawing.Point(661, 387);
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
            this.chkTurbNetAll.Location = new System.Drawing.Point(661, 360);
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
            this.Label87.Location = new System.Drawing.Point(658, 335);
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
            this.btnDelWakeModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnDelWakeGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelWakeGrid.Location = new System.Drawing.Point(1331, 12);
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
            this.cboWakePlot.Location = new System.Drawing.Point(815, 463);
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
            this.Label85.Location = new System.Drawing.Point(821, 439);
            this.Label85.Name = "Label85";
            this.Label85.Size = new System.Drawing.Size(130, 18);
            this.Label85.TabIndex = 10;
            this.Label85.Text = "Display in Plot/Map:";
            // 
            // btnExportNetEsts
            // 
            this.btnExportNetEsts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportNetEsts.Location = new System.Drawing.Point(426, 446);
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
            this.Label55.Location = new System.Drawing.Point(825, 367);
            this.Label55.Name = "Label55";
            this.Label55.Size = new System.Drawing.Size(124, 19);
            this.Label55.TabIndex = 8;
            this.Label55.Text = "Wind Direction:";
            // 
            // cboNetWD
            // 
            this.cboNetWD.FormattingEnabled = true;
            this.cboNetWD.Location = new System.Drawing.Point(822, 400);
            this.cboNetWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboNetWD.Name = "cboNetWD";
            this.cboNetWD.Size = new System.Drawing.Size(129, 26);
            this.cboNetWD.TabIndex = 7;
            this.cboNetWD.SelectedIndexChanged += new System.EventHandler(this.cboTurbEstWD_SelectedIndexChanged);
            // 
            // lstWakeModels
            // 
            this.lstWakeModels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lstWakeModels.Size = new System.Drawing.Size(812, 122);
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
            this.btnWakeLossCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.Label53.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lstWakedTurbs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lstWakedTurbs.Size = new System.Drawing.Size(621, 365);
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
            this.pgeSiteConditions.AutoScroll = true;
            this.pgeSiteConditions.Controls.Add(this.tabSiteConditions);
            this.pgeSiteConditions.Controls.Add(this.label92);
            this.pgeSiteConditions.Location = new System.Drawing.Point(4, 27);
            this.pgeSiteConditions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeSiteConditions.Name = "pgeSiteConditions";
            this.pgeSiteConditions.Size = new System.Drawing.Size(1488, 712);
            this.pgeSiteConditions.TabIndex = 20;
            this.pgeSiteConditions.Text = "Site Conditions";
            this.pgeSiteConditions.UseVisualStyleBackColor = true;
            // 
            // tabSiteConditions
            // 
            this.tabSiteConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSiteConditions.Controls.Add(this.tabPage2);
            this.tabSiteConditions.Controls.Add(this.tabPage1);
            this.tabSiteConditions.Controls.Add(this.tabPage3);
            this.tabSiteConditions.Controls.Add(this.tabPage4);
            this.tabSiteConditions.Location = new System.Drawing.Point(22, 48);
            this.tabSiteConditions.Name = "tabSiteConditions";
            this.tabSiteConditions.SelectedIndex = 0;
            this.tabSiteConditions.Size = new System.Drawing.Size(1461, 657);
            this.tabSiteConditions.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.chkTerrainSlope_UWOnly);
            this.tabPage2.Controls.Add(this.btnExportElevProfile);
            this.tabPage2.Controls.Add(this.btnExportTerrainComplexSector);
            this.tabPage2.Controls.Add(this.cboNumWDComplxTab);
            this.tabPage2.Controls.Add(this.label224);
            this.tabPage2.Controls.Add(this.chkForceThruBase);
            this.tabPage2.Controls.Add(this.btnCalcTerrainComplexity);
            this.tabPage2.Controls.Add(this.label161);
            this.tabPage2.Controls.Add(this.cboTSIorTVIorP90);
            this.tabPage2.Controls.Add(this.plotComplexHisto);
            this.tabPage2.Controls.Add(this.btnShowIECThresh);
            this.tabPage2.Controls.Add(this.lblIEC_Complexity);
            this.tabPage2.Controls.Add(this.label147);
            this.tabPage2.Controls.Add(this.dataTerrainComplex);
            this.tabPage2.Controls.Add(this.label199);
            this.tabPage2.Controls.Add(this.label198);
            this.tabPage2.Controls.Add(this.label197);
            this.tabPage2.Controls.Add(this.label196);
            this.tabPage2.Controls.Add(this.plotInflowAngle);
            this.tabPage2.Controls.Add(this.label200);
            this.tabPage2.Controls.Add(this.txtInflowAngle);
            this.tabPage2.Controls.Add(this.cboInflowReso);
            this.tabPage2.Controls.Add(this.cboInflowRadius);
            this.tabPage2.Controls.Add(this.btnInflowAngles);
            this.tabPage2.Controls.Add(this.cboInflowTurbine);
            this.tabPage2.Controls.Add(this.cboInflowWD);
            this.tabPage2.Controls.Add(this.label171);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1453, 626);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Terrain Complexity";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // chkTerrainSlope_UWOnly
            // 
            this.chkTerrainSlope_UWOnly.AutoSize = true;
            this.chkTerrainSlope_UWOnly.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.chkTerrainSlope_UWOnly.Location = new System.Drawing.Point(217, 348);
            this.chkTerrainSlope_UWOnly.Name = "chkTerrainSlope_UWOnly";
            this.chkTerrainSlope_UWOnly.Size = new System.Drawing.Size(79, 21);
            this.chkTerrainSlope_UWOnly.TabIndex = 306;
            this.chkTerrainSlope_UWOnly.Text = "UW Only";
            this.chkTerrainSlope_UWOnly.UseVisualStyleBackColor = true;
            this.chkTerrainSlope_UWOnly.CheckedChanged += new System.EventHandler(this.chkTerrainSlope_UWOnly_CheckedChanged);
            // 
            // btnExportElevProfile
            // 
            this.btnExportElevProfile.BackColor = System.Drawing.Color.Transparent;
            this.btnExportElevProfile.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnExportElevProfile.Location = new System.Drawing.Point(344, 308);
            this.btnExportElevProfile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportElevProfile.Name = "btnExportElevProfile";
            this.btnExportElevProfile.Size = new System.Drawing.Size(130, 29);
            this.btnExportElevProfile.TabIndex = 305;
            this.btnExportElevProfile.Text = "Export Elev. Profile";
            this.btnExportElevProfile.UseVisualStyleBackColor = false;
            this.btnExportElevProfile.Click += new System.EventHandler(this.btnExportElevProfile_Click);
            // 
            // btnExportTerrainComplexSector
            // 
            this.btnExportTerrainComplexSector.Location = new System.Drawing.Point(1195, 17);
            this.btnExportTerrainComplexSector.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportTerrainComplexSector.Name = "btnExportTerrainComplexSector";
            this.btnExportTerrainComplexSector.Size = new System.Drawing.Size(123, 47);
            this.btnExportTerrainComplexSector.TabIndex = 304;
            this.btnExportTerrainComplexSector.Text = "Export Values by Sector";
            this.btnExportTerrainComplexSector.UseVisualStyleBackColor = true;
            this.btnExportTerrainComplexSector.Click += new System.EventHandler(this.btnExportTerrainComplexSector_Click);
            // 
            // cboNumWDComplxTab
            // 
            this.cboNumWDComplxTab.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNumWDComplxTab.FormattingEnabled = true;
            this.cboNumWDComplxTab.Items.AddRange(new object[] {
            "1",
            "4",
            "8",
            "12",
            "16",
            "24"});
            this.cboNumWDComplxTab.Location = new System.Drawing.Point(479, 14);
            this.cboNumWDComplxTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboNumWDComplxTab.Name = "cboNumWDComplxTab";
            this.cboNumWDComplxTab.Size = new System.Drawing.Size(48, 26);
            this.cboNumWDComplxTab.TabIndex = 303;
            this.cboNumWDComplxTab.Text = "16";
            this.cboNumWDComplxTab.SelectedIndexChanged += new System.EventHandler(this.cboNumWDComplxTab_SelectedIndexChanged);
            // 
            // label224
            // 
            this.label224.AutoSize = true;
            this.label224.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label224.Location = new System.Drawing.Point(371, 17);
            this.label224.Name = "label224";
            this.label224.Size = new System.Drawing.Size(103, 18);
            this.label224.TabIndex = 302;
            this.label224.Text = "Num. WD bins :";
            // 
            // chkForceThruBase
            // 
            this.chkForceThruBase.AutoSize = true;
            this.chkForceThruBase.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.chkForceThruBase.Location = new System.Drawing.Point(526, 51);
            this.chkForceThruBase.Name = "chkForceThruBase";
            this.chkForceThruBase.Size = new System.Drawing.Size(253, 21);
            this.chkForceThruBase.TabIndex = 301;
            this.chkForceThruBase.Text = "Force Fitted Plane Through Turbine Base";
            this.chkForceThruBase.UseVisualStyleBackColor = true;
            this.chkForceThruBase.CheckedChanged += new System.EventHandler(this.chkForceThruBase_CheckedChanged);
            // 
            // btnCalcTerrainComplexity
            // 
            this.btnCalcTerrainComplexity.BackColor = System.Drawing.Color.Transparent;
            this.btnCalcTerrainComplexity.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnCalcTerrainComplexity.Location = new System.Drawing.Point(213, 9);
            this.btnCalcTerrainComplexity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCalcTerrainComplexity.Name = "btnCalcTerrainComplexity";
            this.btnCalcTerrainComplexity.Size = new System.Drawing.Size(146, 49);
            this.btnCalcTerrainComplexity.TabIndex = 300;
            this.btnCalcTerrainComplexity.Text = "Calculate Complexity at Turbine Sites";
            this.btnCalcTerrainComplexity.UseVisualStyleBackColor = false;
            this.btnCalcTerrainComplexity.Click += new System.EventHandler(this.btnCalcTerrainComplexity_Click);
            // 
            // label161
            // 
            this.label161.AutoSize = true;
            this.label161.Font = new System.Drawing.Font("Palatino Linotype", 12F);
            this.label161.Location = new System.Drawing.Point(42, 64);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(316, 22);
            this.label161.TabIndex = 299;
            this.label161.Text = "Histogram of Complexity Metrics at Turbines";
            // 
            // cboTSIorTVIorP90
            // 
            this.cboTSIorTVIorP90.FormattingEnabled = true;
            this.cboTSIorTVIorP90.Items.AddRange(new object[] {
            "5z 360 TSI",
            "5z 360 TVI",
            "5z 30 TSI",
            "5z 30 TVI",
            "10z 30 TSI",
            "10z 30 TVI",
            "20z 30 TSI",
            "20z 30 TVI",
            "P10 UW",
            "P10 DW"});
            this.cboTSIorTVIorP90.Location = new System.Drawing.Point(374, 60);
            this.cboTSIorTVIorP90.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTSIorTVIorP90.Name = "cboTSIorTVIorP90";
            this.cboTSIorTVIorP90.Size = new System.Drawing.Size(128, 26);
            this.cboTSIorTVIorP90.TabIndex = 298;
            this.cboTSIorTVIorP90.SelectedIndexChanged += new System.EventHandler(this.cboTSIorTVIorP90_SelectedIndexChanged);
            // 
            // plotComplexHisto
            // 
            this.plotComplexHisto.Location = new System.Drawing.Point(40, 92);
            this.plotComplexHisto.Name = "plotComplexHisto";
            this.plotComplexHisto.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotComplexHisto.Size = new System.Drawing.Size(458, 206);
            this.plotComplexHisto.TabIndex = 297;
            this.plotComplexHisto.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotComplexHisto.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotComplexHisto.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnShowIECThresh
            // 
            this.btnShowIECThresh.Location = new System.Drawing.Point(1348, 17);
            this.btnShowIECThresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowIECThresh.Name = "btnShowIECThresh";
            this.btnShowIECThresh.Size = new System.Drawing.Size(93, 47);
            this.btnShowIECThresh.TabIndex = 295;
            this.btnShowIECThresh.Text = "Show IEC Thresholds";
            this.btnShowIECThresh.UseVisualStyleBackColor = true;
            this.btnShowIECThresh.Click += new System.EventHandler(this.btnShowIECThresh_Click);
            // 
            // lblIEC_Complexity
            // 
            this.lblIEC_Complexity.AutoSize = true;
            this.lblIEC_Complexity.Font = new System.Drawing.Font("Palatino Linotype", 14F, System.Drawing.FontStyle.Bold);
            this.lblIEC_Complexity.ForeColor = System.Drawing.Color.IndianRed;
            this.lblIEC_Complexity.Location = new System.Drawing.Point(835, 32);
            this.lblIEC_Complexity.Name = "lblIEC_Complexity";
            this.lblIEC_Complexity.Size = new System.Drawing.Size(143, 26);
            this.lblIEC_Complexity.TabIndex = 294;
            this.lblIEC_Complexity.Text = "COMPLEXITY";
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Location = new System.Drawing.Point(779, 11);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(248, 19);
            this.label147.TabIndex = 293;
            this.label147.Text = "Complexity Level (IEC 61400-1 Ed.4) :";
            // 
            // dataTerrainComplex
            // 
            this.dataTerrainComplex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataTerrainComplex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTerrainComplex.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column15,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14});
            this.dataTerrainComplex.Location = new System.Drawing.Point(518, 86);
            this.dataTerrainComplex.Name = "dataTerrainComplex";
            this.dataTerrainComplex.ReadOnly = true;
            this.dataTerrainComplex.RowHeadersWidth = 51;
            this.dataTerrainComplex.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataTerrainComplex.Size = new System.Drawing.Size(929, 533);
            this.dataTerrainComplex.TabIndex = 292;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Turbine";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Elev. [m]";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 65;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Complexity";
            this.Column15.MinimumWidth = 6;
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 90;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "5h 360 TSI";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 65;
            // 
            // Column6
            // 
            dataGridViewCellStyle1.Format = "P";
            dataGridViewCellStyle1.NullValue = null;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column6.HeaderText = "5h 360 TVI";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 65;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "5h 30 TSI";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 65;
            // 
            // Column8
            // 
            dataGridViewCellStyle2.Format = "P";
            dataGridViewCellStyle2.NullValue = null;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column8.HeaderText = "5h 30 TVI";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 65;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "10h 30 TSI";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 65;
            // 
            // Column10
            // 
            dataGridViewCellStyle3.Format = "P";
            dataGridViewCellStyle3.NullValue = null;
            this.Column10.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column10.HeaderText = "10h 30 TVI";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 65;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "20h 30 TSI";
            this.Column11.MinimumWidth = 6;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 65;
            // 
            // Column12
            // 
            dataGridViewCellStyle4.Format = "P";
            dataGridViewCellStyle4.NullValue = null;
            this.Column12.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column12.HeaderText = "20h 30 TVI";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 65;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "P10 UW Expo.";
            this.Column13.MinimumWidth = 6;
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 75;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "P10 DW Expo";
            this.Column14.MinimumWidth = 6;
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 75;
            // 
            // label199
            // 
            this.label199.AutoSize = true;
            this.label199.Location = new System.Drawing.Point(317, 351);
            this.label199.Name = "label199";
            this.label199.Size = new System.Drawing.Size(85, 19);
            this.label199.TabIndex = 290;
            this.label199.Text = "Resolution :";
            // 
            // label198
            // 
            this.label198.AutoSize = true;
            this.label198.Location = new System.Drawing.Point(47, 383);
            this.label198.Name = "label198";
            this.label198.Size = new System.Drawing.Size(60, 19);
            this.label198.TabIndex = 289;
            this.label198.Text = "Radius :";
            // 
            // label197
            // 
            this.label197.AutoSize = true;
            this.label197.Location = new System.Drawing.Point(43, 311);
            this.label197.Name = "label197";
            this.label197.Size = new System.Drawing.Size(67, 19);
            this.label197.TabIndex = 288;
            this.label197.Text = "Turbine :";
            // 
            // label196
            // 
            this.label196.AutoSize = true;
            this.label196.Location = new System.Drawing.Point(62, 345);
            this.label196.Name = "label196";
            this.label196.Size = new System.Drawing.Size(42, 19);
            this.label196.TabIndex = 287;
            this.label196.Text = "WD :";
            // 
            // plotInflowAngle
            // 
            this.plotInflowAngle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.plotInflowAngle.Location = new System.Drawing.Point(40, 419);
            this.plotInflowAngle.Name = "plotInflowAngle";
            this.plotInflowAngle.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotInflowAngle.Size = new System.Drawing.Size(462, 195);
            this.plotInflowAngle.TabIndex = 286;
            this.plotInflowAngle.Text = "plotView1";
            this.plotInflowAngle.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotInflowAngle.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotInflowAngle.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // label200
            // 
            this.label200.AutoSize = true;
            this.label200.Location = new System.Drawing.Point(247, 387);
            this.label200.Name = "label200";
            this.label200.Size = new System.Drawing.Size(143, 19);
            this.label200.TabIndex = 285;
            this.label200.Text = "Inflow Angle (degs) :";
            // 
            // txtInflowAngle
            // 
            this.txtInflowAngle.Enabled = false;
            this.txtInflowAngle.Location = new System.Drawing.Point(394, 383);
            this.txtInflowAngle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtInflowAngle.Name = "txtInflowAngle";
            this.txtInflowAngle.Size = new System.Drawing.Size(96, 25);
            this.txtInflowAngle.TabIndex = 284;
            // 
            // cboInflowReso
            // 
            this.cboInflowReso.FormattingEnabled = true;
            this.cboInflowReso.Items.AddRange(new object[] {
            "25",
            "50",
            "75",
            "100"});
            this.cboInflowReso.Location = new System.Drawing.Point(408, 345);
            this.cboInflowReso.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboInflowReso.Name = "cboInflowReso";
            this.cboInflowReso.Size = new System.Drawing.Size(84, 26);
            this.cboInflowReso.TabIndex = 283;
            this.cboInflowReso.SelectedIndexChanged += new System.EventHandler(this.cboInflowReso_SelectedIndexChanged);
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
            this.cboInflowRadius.Location = new System.Drawing.Point(118, 380);
            this.cboInflowRadius.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboInflowRadius.Name = "cboInflowRadius";
            this.cboInflowRadius.Size = new System.Drawing.Size(84, 26);
            this.cboInflowRadius.TabIndex = 282;
            this.cboInflowRadius.SelectedIndexChanged += new System.EventHandler(this.cboInflowRadius_SelectedIndexChanged);
            // 
            // btnInflowAngles
            // 
            this.btnInflowAngles.Location = new System.Drawing.Point(1053, 17);
            this.btnInflowAngles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnInflowAngles.Name = "btnInflowAngles";
            this.btnInflowAngles.Size = new System.Drawing.Size(119, 47);
            this.btnInflowAngles.TabIndex = 281;
            this.btnInflowAngles.Text = "Export Turbine TSI && TVI";
            this.btnInflowAngles.UseVisualStyleBackColor = true;
            this.btnInflowAngles.Click += new System.EventHandler(this.btnInflowAngles_Click_1);
            // 
            // cboInflowTurbine
            // 
            this.cboInflowTurbine.FormattingEnabled = true;
            this.cboInflowTurbine.Items.AddRange(new object[] {
            "Average",
            "Representative",
            "Effective"});
            this.cboInflowTurbine.Location = new System.Drawing.Point(116, 308);
            this.cboInflowTurbine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboInflowTurbine.Name = "cboInflowTurbine";
            this.cboInflowTurbine.Size = new System.Drawing.Size(173, 26);
            this.cboInflowTurbine.TabIndex = 280;
            this.cboInflowTurbine.SelectedIndexChanged += new System.EventHandler(this.cboInflowTurbine_SelectedIndexChanged);
            // 
            // cboInflowWD
            // 
            this.cboInflowWD.FormattingEnabled = true;
            this.cboInflowWD.Location = new System.Drawing.Point(117, 343);
            this.cboInflowWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboInflowWD.Name = "cboInflowWD";
            this.cboInflowWD.Size = new System.Drawing.Size(84, 26);
            this.cboInflowWD.TabIndex = 279;
            this.cboInflowWD.SelectedIndexChanged += new System.EventHandler(this.cboInflowWD_SelectedIndexChanged);
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label171.Location = new System.Drawing.Point(18, 17);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(184, 23);
            this.label171.TabIndex = 278;
            this.label171.Text = "Terrain Complexity";
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.cboTI_TerrainComplexCorr);
            this.tabPage1.Controls.Add(this.chkApplyTCCtoEffTI);
            this.tabPage1.Controls.Add(this.label215);
            this.tabPage1.Controls.Add(this.label214);
            this.tabPage1.Controls.Add(this.plotTurbInt);
            this.tabPage1.Controls.Add(this.btnExportTI);
            this.tabPage1.Controls.Add(this.label188);
            this.tabPage1.Controls.Add(this.cboTurbPowerCurve);
            this.tabPage1.Controls.Add(this.dateTIStart);
            this.tabPage1.Controls.Add(this.label186);
            this.tabPage1.Controls.Add(this.label187);
            this.tabPage1.Controls.Add(this.dateTIEnd);
            this.tabPage1.Controls.Add(this.cboTurbineTI);
            this.tabPage1.Controls.Add(this.label179);
            this.tabPage1.Controls.Add(this.label178);
            this.tabPage1.Controls.Add(this.cboEffectiveTI_m);
            this.tabPage1.Controls.Add(this.cboTI_Type);
            this.tabPage1.Controls.Add(this.label177);
            this.tabPage1.Controls.Add(this.lstTurbulence);
            this.tabPage1.Controls.Add(this.label174);
            this.tabPage1.Controls.Add(this.cboTurbMet);
            this.tabPage1.Controls.Add(this.label172);
            this.tabPage1.Controls.Add(this.cboTurbWD);
            this.tabPage1.Controls.Add(this.label93);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1453, 626);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Turbulence Intensity";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cboTI_TerrainComplexCorr
            // 
            this.cboTI_TerrainComplexCorr.FormattingEnabled = true;
            this.cboTI_TerrainComplexCorr.Items.AddRange(new object[] {
            "1.00 - Not Complex ",
            "1.05 - Low Complex ",
            "1.10 - Moderate Complex ",
            "1.15 - High Complex "});
            this.cboTI_TerrainComplexCorr.Location = new System.Drawing.Point(252, 525);
            this.cboTI_TerrainComplexCorr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTI_TerrainComplexCorr.Name = "cboTI_TerrainComplexCorr";
            this.cboTI_TerrainComplexCorr.Size = new System.Drawing.Size(235, 26);
            this.cboTI_TerrainComplexCorr.TabIndex = 293;
            this.cboTI_TerrainComplexCorr.SelectedIndexChanged += new System.EventHandler(this.cboTI_TerrainComplexCorr_SelectedIndexChanged);
            // 
            // chkApplyTCCtoEffTI
            // 
            this.chkApplyTCCtoEffTI.AutoSize = true;
            this.chkApplyTCCtoEffTI.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.chkApplyTCCtoEffTI.Location = new System.Drawing.Point(250, 497);
            this.chkApplyTCCtoEffTI.Name = "chkApplyTCCtoEffTI";
            this.chkApplyTCCtoEffTI.Size = new System.Drawing.Size(238, 21);
            this.chkApplyTCCtoEffTI.TabIndex = 292;
            this.chkApplyTCCtoEffTI.Text = "Apply Terrain Complexity Adjustment";
            this.chkApplyTCCtoEffTI.UseVisualStyleBackColor = true;
            this.chkApplyTCCtoEffTI.CheckedChanged += new System.EventHandler(this.chkApplyTCCtoEffTI_CheckedChanged);
            // 
            // label215
            // 
            this.label215.AutoSize = true;
            this.label215.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label215.Location = new System.Drawing.Point(251, 31);
            this.label215.Name = "label215";
            this.label215.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label215.Size = new System.Drawing.Size(104, 22);
            this.label215.TabIndex = 291;
            this.label215.Text = "TI Parameters";
            // 
            // label214
            // 
            this.label214.AutoSize = true;
            this.label214.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label214.Location = new System.Drawing.Point(254, 302);
            this.label214.Name = "label214";
            this.label214.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label214.Size = new System.Drawing.Size(166, 22);
            this.label214.TabIndex = 290;
            this.label214.Text = "Effective TI Parameters";
            // 
            // plotTurbInt
            // 
            this.plotTurbInt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotTurbInt.Location = new System.Drawing.Point(520, 18);
            this.plotTurbInt.Name = "plotTurbInt";
            this.plotTurbInt.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTurbInt.Size = new System.Drawing.Size(911, 689);
            this.plotTurbInt.TabIndex = 289;
            this.plotTurbInt.Text = "plotView1";
            this.plotTurbInt.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTurbInt.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTurbInt.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnExportTI
            // 
            this.btnExportTI.Location = new System.Drawing.Point(253, 565);
            this.btnExportTI.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportTI.Name = "btnExportTI";
            this.btnExportTI.Size = new System.Drawing.Size(87, 42);
            this.btnExportTI.TabIndex = 288;
            this.btnExportTI.Text = "Export TI";
            this.btnExportTI.UseVisualStyleBackColor = true;
            this.btnExportTI.Click += new System.EventHandler(this.btnExportTI_Click);
            // 
            // label188
            // 
            this.label188.AutoSize = true;
            this.label188.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label188.Location = new System.Drawing.Point(250, 334);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(93, 18);
            this.label188.TabIndex = 287;
            this.label188.Text = "Power Curve :";
            // 
            // cboTurbPowerCurve
            // 
            this.cboTurbPowerCurve.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTurbPowerCurve.FormattingEnabled = true;
            this.cboTurbPowerCurve.Location = new System.Drawing.Point(253, 356);
            this.cboTurbPowerCurve.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTurbPowerCurve.Name = "cboTurbPowerCurve";
            this.cboTurbPowerCurve.Size = new System.Drawing.Size(234, 26);
            this.cboTurbPowerCurve.TabIndex = 286;
            this.cboTurbPowerCurve.SelectedIndexChanged += new System.EventHandler(this.cboTurbPowerCurve_SelectedIndexChanged);
            // 
            // dateTIStart
            // 
            this.dateTIStart.CustomFormat = "MM/dd/yy";
            this.dateTIStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTIStart.Location = new System.Drawing.Point(305, 222);
            this.dateTIStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTIStart.Name = "dateTIStart";
            this.dateTIStart.Size = new System.Drawing.Size(76, 25);
            this.dateTIStart.TabIndex = 285;
            this.dateTIStart.ValueChanged += new System.EventHandler(this.dateTIStart_ValueChanged);
            // 
            // label186
            // 
            this.label186.AutoSize = true;
            this.label186.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label186.Location = new System.Drawing.Point(265, 261);
            this.label186.Name = "label186";
            this.label186.Size = new System.Drawing.Size(34, 19);
            this.label186.TabIndex = 284;
            this.label186.Text = "To :";
            // 
            // label187
            // 
            this.label187.AutoSize = true;
            this.label187.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label187.Location = new System.Drawing.Point(248, 225);
            this.label187.Name = "label187";
            this.label187.Size = new System.Drawing.Size(51, 19);
            this.label187.TabIndex = 283;
            this.label187.Text = "From :";
            // 
            // dateTIEnd
            // 
            this.dateTIEnd.CustomFormat = "MM/dd/yy";
            this.dateTIEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTIEnd.Location = new System.Drawing.Point(305, 258);
            this.dateTIEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTIEnd.Name = "dateTIEnd";
            this.dateTIEnd.Size = new System.Drawing.Size(76, 25);
            this.dateTIEnd.TabIndex = 282;
            this.dateTIEnd.ValueChanged += new System.EventHandler(this.dateTIEnd_ValueChanged_1);
            // 
            // cboTurbineTI
            // 
            this.cboTurbineTI.FormattingEnabled = true;
            this.cboTurbineTI.Items.AddRange(new object[] {
            "Average",
            "Representative",
            "Effective"});
            this.cboTurbineTI.Location = new System.Drawing.Point(253, 419);
            this.cboTurbineTI.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTurbineTI.Name = "cboTurbineTI";
            this.cboTurbineTI.Size = new System.Drawing.Size(234, 26);
            this.cboTurbineTI.TabIndex = 281;
            this.cboTurbineTI.SelectedIndexChanged += new System.EventHandler(this.cboTurbineTI_SelectedIndexChanged);
            // 
            // label179
            // 
            this.label179.AutoSize = true;
            this.label179.Location = new System.Drawing.Point(249, 396);
            this.label179.Name = "label179";
            this.label179.Size = new System.Drawing.Size(67, 19);
            this.label179.TabIndex = 280;
            this.label179.Text = "Turbine :";
            // 
            // label178
            // 
            this.label178.AutoSize = true;
            this.label178.Location = new System.Drawing.Point(279, 458);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(147, 19);
            this.label178.TabIndex = 279;
            this.label178.Text = "Wohler exponent, m :";
            // 
            // cboEffectiveTI_m
            // 
            this.cboEffectiveTI_m.FormattingEnabled = true;
            this.cboEffectiveTI_m.Items.AddRange(new object[] {
            "1",
            "10"});
            this.cboEffectiveTI_m.Location = new System.Drawing.Point(432, 458);
            this.cboEffectiveTI_m.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboEffectiveTI_m.Name = "cboEffectiveTI_m";
            this.cboEffectiveTI_m.Size = new System.Drawing.Size(55, 26);
            this.cboEffectiveTI_m.TabIndex = 278;
            this.cboEffectiveTI_m.SelectedIndexChanged += new System.EventHandler(this.cboEffectiveTI_m_SelectedIndexChanged);
            // 
            // cboTI_Type
            // 
            this.cboTI_Type.FormattingEnabled = true;
            this.cboTI_Type.Items.AddRange(new object[] {
            "Average",
            "Representative",
            "Effective"});
            this.cboTI_Type.Location = new System.Drawing.Point(253, 135);
            this.cboTI_Type.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTI_Type.Name = "cboTI_Type";
            this.cboTI_Type.Size = new System.Drawing.Size(234, 26);
            this.cboTI_Type.TabIndex = 277;
            this.cboTI_Type.SelectedIndexChanged += new System.EventHandler(this.cboTI_Type_SelectedIndexChanged);
            // 
            // label177
            // 
            this.label177.AutoSize = true;
            this.label177.Location = new System.Drawing.Point(249, 112);
            this.label177.Name = "label177";
            this.label177.Size = new System.Drawing.Size(66, 19);
            this.label177.TabIndex = 276;
            this.label177.Text = "TI Type :";
            // 
            // lstTurbulence
            // 
            this.lstTurbulence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstTurbulence.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader177,
            this.columnHeader178,
            this.columnHeader179});
            this.lstTurbulence.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTurbulence.GridLines = true;
            this.lstTurbulence.HideSelection = false;
            this.lstTurbulence.Location = new System.Drawing.Point(13, 51);
            this.lstTurbulence.Margin = new System.Windows.Forms.Padding(2);
            this.lstTurbulence.Name = "lstTurbulence";
            this.lstTurbulence.Size = new System.Drawing.Size(217, 661);
            this.lstTurbulence.TabIndex = 275;
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
            this.label174.Location = new System.Drawing.Point(252, 60);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(64, 19);
            this.label174.TabIndex = 274;
            this.label174.Text = "Met Site:";
            // 
            // cboTurbMet
            // 
            this.cboTurbMet.FormattingEnabled = true;
            this.cboTurbMet.Location = new System.Drawing.Point(253, 83);
            this.cboTurbMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTurbMet.Name = "cboTurbMet";
            this.cboTurbMet.Size = new System.Drawing.Size(234, 26);
            this.cboTurbMet.TabIndex = 273;
            this.cboTurbMet.SelectedIndexChanged += new System.EventHandler(this.cboTurbMet_SelectedIndexChanged);
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.Location = new System.Drawing.Point(249, 183);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(42, 19);
            this.label172.TabIndex = 272;
            this.label172.Text = "WD :";
            // 
            // cboTurbWD
            // 
            this.cboTurbWD.FormattingEnabled = true;
            this.cboTurbWD.Location = new System.Drawing.Point(297, 180);
            this.cboTurbWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTurbWD.Name = "cboTurbWD";
            this.cboTurbWD.Size = new System.Drawing.Size(84, 26);
            this.cboTurbWD.TabIndex = 271;
            this.cboTurbWD.SelectedIndexChanged += new System.EventHandler(this.cboTurbWD_SelectedIndexChanged);
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.Location = new System.Drawing.Point(26, 18);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(191, 23);
            this.label93.TabIndex = 270;
            this.label93.Text = "Turbulence Intensity";
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.btnEditShearFromSiteConds);
            this.tabPage3.Controls.Add(this.txtShearCalcMethodExtremeTab);
            this.tabPage3.Controls.Add(this.txtMaxHeight);
            this.tabPage3.Controls.Add(this.txtMinHeight);
            this.tabPage3.Controls.Add(this.label228);
            this.tabPage3.Controls.Add(this.label229);
            this.tabPage3.Controls.Add(this.label230);
            this.tabPage3.Controls.Add(this.btnExportShearHisto);
            this.tabPage3.Controls.Add(this.plotExtremeShear);
            this.tabPage3.Controls.Add(this.dateTimeExtremeShearStart);
            this.tabPage3.Controls.Add(this.label202);
            this.tabPage3.Controls.Add(this.label203);
            this.tabPage3.Controls.Add(this.dateTimeExtremeShearEnd);
            this.tabPage3.Controls.Add(this.label201);
            this.tabPage3.Controls.Add(this.label190);
            this.tabPage3.Controls.Add(this.cboExtremeShearRange);
            this.tabPage3.Controls.Add(this.label189);
            this.tabPage3.Controls.Add(this.cboExtremeShearMet);
            this.tabPage3.Controls.Add(this.btnExportShearStats);
            this.tabPage3.Controls.Add(this.lstExtremeShear);
            this.tabPage3.Controls.Add(this.label95);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1453, 626);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Extreme Shear";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnEditShearFromSiteConds
            // 
            this.btnEditShearFromSiteConds.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnEditShearFromSiteConds.Location = new System.Drawing.Point(380, 148);
            this.btnEditShearFromSiteConds.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditShearFromSiteConds.Name = "btnEditShearFromSiteConds";
            this.btnEditShearFromSiteConds.Size = new System.Drawing.Size(46, 25);
            this.btnEditShearFromSiteConds.TabIndex = 301;
            this.btnEditShearFromSiteConds.Text = "Edit";
            this.btnEditShearFromSiteConds.UseVisualStyleBackColor = true;
            this.btnEditShearFromSiteConds.Click += new System.EventHandler(this.btnEditShearFromSiteConds_Click);
            // 
            // txtShearCalcMethodExtremeTab
            // 
            this.txtShearCalcMethodExtremeTab.Location = new System.Drawing.Point(38, 148);
            this.txtShearCalcMethodExtremeTab.Name = "txtShearCalcMethodExtremeTab";
            this.txtShearCalcMethodExtremeTab.ReadOnly = true;
            this.txtShearCalcMethodExtremeTab.Size = new System.Drawing.Size(327, 25);
            this.txtShearCalcMethodExtremeTab.TabIndex = 300;
            // 
            // txtMaxHeight
            // 
            this.txtMaxHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxHeight.Location = new System.Drawing.Point(312, 185);
            this.txtMaxHeight.Name = "txtMaxHeight";
            this.txtMaxHeight.ReadOnly = true;
            this.txtMaxHeight.Size = new System.Drawing.Size(53, 23);
            this.txtMaxHeight.TabIndex = 299;
            // 
            // txtMinHeight
            // 
            this.txtMinHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinHeight.Location = new System.Drawing.Point(136, 185);
            this.txtMinHeight.Name = "txtMinHeight";
            this.txtMinHeight.ReadOnly = true;
            this.txtMinHeight.Size = new System.Drawing.Size(53, 23);
            this.txtMinHeight.TabIndex = 298;
            // 
            // label228
            // 
            this.label228.AutoSize = true;
            this.label228.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label228.Location = new System.Drawing.Point(202, 188);
            this.label228.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label228.Name = "label228";
            this.label228.Size = new System.Drawing.Size(105, 17);
            this.label228.TabIndex = 297;
            this.label228.Text = "Max Height [m]:";
            // 
            // label229
            // 
            this.label229.AutoSize = true;
            this.label229.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label229.Location = new System.Drawing.Point(36, 188);
            this.label229.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label229.Name = "label229";
            this.label229.Size = new System.Drawing.Size(102, 17);
            this.label229.TabIndex = 296;
            this.label229.Text = "Min Height [m]:";
            // 
            // label230
            // 
            this.label230.AutoSize = true;
            this.label230.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label230.Location = new System.Drawing.Point(38, 126);
            this.label230.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label230.Name = "label230";
            this.label230.Size = new System.Drawing.Size(150, 16);
            this.label230.TabIndex = 295;
            this.label230.Text = "Shear Calculation Type:";
            // 
            // btnExportShearHisto
            // 
            this.btnExportShearHisto.Location = new System.Drawing.Point(370, 505);
            this.btnExportShearHisto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportShearHisto.Name = "btnExportShearHisto";
            this.btnExportShearHisto.Size = new System.Drawing.Size(101, 54);
            this.btnExportShearHisto.TabIndex = 293;
            this.btnExportShearHisto.Text = "Export Shear Histogram";
            this.btnExportShearHisto.UseVisualStyleBackColor = true;
            this.btnExportShearHisto.Click += new System.EventHandler(this.btnExportShearHisto_Click);
            // 
            // plotExtremeShear
            // 
            this.plotExtremeShear.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotExtremeShear.Location = new System.Drawing.Point(501, 18);
            this.plotExtremeShear.Name = "plotExtremeShear";
            this.plotExtremeShear.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotExtremeShear.Size = new System.Drawing.Size(914, 680);
            this.plotExtremeShear.TabIndex = 292;
            this.plotExtremeShear.Text = "plotView1";
            this.plotExtremeShear.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotExtremeShear.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotExtremeShear.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // dateTimeExtremeShearStart
            // 
            this.dateTimeExtremeShearStart.CustomFormat = "MM/dd/yy";
            this.dateTimeExtremeShearStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeExtremeShearStart.Location = new System.Drawing.Point(167, 293);
            this.dateTimeExtremeShearStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimeExtremeShearStart.Name = "dateTimeExtremeShearStart";
            this.dateTimeExtremeShearStart.Size = new System.Drawing.Size(76, 25);
            this.dateTimeExtremeShearStart.TabIndex = 291;
            this.dateTimeExtremeShearStart.ValueChanged += new System.EventHandler(this.dateTimeExtremeShearStart_ValueChanged);
            // 
            // label202
            // 
            this.label202.AutoSize = true;
            this.label202.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label202.Location = new System.Drawing.Point(123, 333);
            this.label202.Name = "label202";
            this.label202.Size = new System.Drawing.Size(34, 19);
            this.label202.TabIndex = 290;
            this.label202.Text = "To :";
            // 
            // label203
            // 
            this.label203.AutoSize = true;
            this.label203.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label203.Location = new System.Drawing.Point(109, 296);
            this.label203.Name = "label203";
            this.label203.Size = new System.Drawing.Size(51, 19);
            this.label203.TabIndex = 289;
            this.label203.Text = "From :";
            // 
            // dateTimeExtremeShearEnd
            // 
            this.dateTimeExtremeShearEnd.CustomFormat = "MM/dd/yy";
            this.dateTimeExtremeShearEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeExtremeShearEnd.Location = new System.Drawing.Point(167, 330);
            this.dateTimeExtremeShearEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimeExtremeShearEnd.Name = "dateTimeExtremeShearEnd";
            this.dateTimeExtremeShearEnd.Size = new System.Drawing.Size(76, 25);
            this.dateTimeExtremeShearEnd.TabIndex = 288;
            this.dateTimeExtremeShearEnd.ValueChanged += new System.EventHandler(this.dateTimeExtremeShearEnd_ValueChanged);
            // 
            // label201
            // 
            this.label201.AutoSize = true;
            this.label201.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label201.Location = new System.Drawing.Point(17, 382);
            this.label201.Name = "label201";
            this.label201.Size = new System.Drawing.Size(175, 19);
            this.label201.TabIndex = 287;
            this.label201.Text = "Extreme Shear Values";
            // 
            // label190
            // 
            this.label190.AutoSize = true;
            this.label190.Location = new System.Drawing.Point(31, 250);
            this.label190.Name = "label190";
            this.label190.Size = new System.Drawing.Size(135, 19);
            this.label190.TabIndex = 286;
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
            this.cboExtremeShearRange.Location = new System.Drawing.Point(172, 246);
            this.cboExtremeShearRange.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExtremeShearRange.Name = "cboExtremeShearRange";
            this.cboExtremeShearRange.Size = new System.Drawing.Size(132, 26);
            this.cboExtremeShearRange.TabIndex = 285;
            this.cboExtremeShearRange.SelectedIndexChanged += new System.EventHandler(this.cboExtremeShearRange_SelectedIndexChanged);
            // 
            // label189
            // 
            this.label189.AutoSize = true;
            this.label189.Location = new System.Drawing.Point(31, 56);
            this.label189.Name = "label189";
            this.label189.Size = new System.Drawing.Size(41, 19);
            this.label189.TabIndex = 284;
            this.label189.Text = "Met :";
            // 
            // cboExtremeShearMet
            // 
            this.cboExtremeShearMet.FormattingEnabled = true;
            this.cboExtremeShearMet.Location = new System.Drawing.Point(35, 79);
            this.cboExtremeShearMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExtremeShearMet.Name = "cboExtremeShearMet";
            this.cboExtremeShearMet.Size = new System.Drawing.Size(320, 26);
            this.cboExtremeShearMet.TabIndex = 283;
            this.cboExtremeShearMet.SelectedIndexChanged += new System.EventHandler(this.cboExtremeShearMet_SelectedIndexChanged);
            // 
            // btnExportShearStats
            // 
            this.btnExportShearStats.Location = new System.Drawing.Point(370, 443);
            this.btnExportShearStats.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportShearStats.Name = "btnExportShearStats";
            this.btnExportShearStats.Size = new System.Drawing.Size(101, 54);
            this.btnExportShearStats.TabIndex = 282;
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
            this.lstExtremeShear.Location = new System.Drawing.Point(21, 417);
            this.lstExtremeShear.Margin = new System.Windows.Forms.Padding(2);
            this.lstExtremeShear.Name = "lstExtremeShear";
            this.lstExtremeShear.Size = new System.Drawing.Size(334, 142);
            this.lstExtremeShear.TabIndex = 281;
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
            this.label95.Location = new System.Drawing.Point(17, 18);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(193, 23);
            this.label95.TabIndex = 280;
            this.label95.Text = "Extreme Wind Shear";
            // 
            // tabPage4
            // 
            this.tabPage4.AutoScroll = true;
            this.tabPage4.Controls.Add(this.btnExportExtremeWSTable);
            this.tabPage4.Controls.Add(this.txtWMO_Desc);
            this.tabPage4.Controls.Add(this.label242);
            this.tabPage4.Controls.Add(this.txtWMO_HourGust);
            this.tabPage4.Controls.Add(this.label243);
            this.tabPage4.Controls.Add(this.txtWMO_HourTenMin);
            this.tabPage4.Controls.Add(this.label241);
            this.tabPage4.Controls.Add(this.chkUseWMO_TenMin);
            this.tabPage4.Controls.Add(this.label240);
            this.tabPage4.Controls.Add(this.cboWMO_Class);
            this.tabPage4.Controls.Add(this.chkUseWMO_Gust);
            this.tabPage4.Controls.Add(this.chkExtremeWS_ShowLegend);
            this.tabPage4.Controls.Add(this.label239);
            this.tabPage4.Controls.Add(this.dateExtremeWS_End);
            this.tabPage4.Controls.Add(this.dateExtremeWS_Start);
            this.tabPage4.Controls.Add(this.lblGustExtremeWSUnavailable);
            this.tabPage4.Controls.Add(this.label238);
            this.tabPage4.Controls.Add(this.cboExtremeWS_Height);
            this.tabPage4.Controls.Add(this.label235);
            this.tabPage4.Controls.Add(this.txtGumbelGustMu);
            this.tabPage4.Controls.Add(this.label236);
            this.tabPage4.Controls.Add(this.txtGumbelGustBeta);
            this.tabPage4.Controls.Add(this.label234);
            this.tabPage4.Controls.Add(this.label233);
            this.tabPage4.Controls.Add(this.txtGumbelTenMinMu);
            this.tabPage4.Controls.Add(this.label232);
            this.tabPage4.Controls.Add(this.label231);
            this.tabPage4.Controls.Add(this.txtGumbelTenMinBeta);
            this.tabPage4.Controls.Add(this.chkUseSimData);
            this.tabPage4.Controls.Add(this.plotExtremeWS_TS);
            this.tabPage4.Controls.Add(this.dataExtremeWS);
            this.tabPage4.Controls.Add(this.label195);
            this.tabPage4.Controls.Add(this.label103);
            this.tabPage4.Controls.Add(this.cboExtremeWSRef);
            this.tabPage4.Controls.Add(this.plotExtremeWS);
            this.tabPage4.Controls.Add(this.lblExtremeWS);
            this.tabPage4.Controls.Add(this.btnExtremeWS);
            this.tabPage4.Controls.Add(this.cboExtremeWSMet);
            this.tabPage4.Controls.Add(this.txt1yrExtremeGust);
            this.tabPage4.Controls.Add(this.txt1yrExtreme10min);
            this.tabPage4.Controls.Add(this.txt50yrExtremeGust);
            this.tabPage4.Controls.Add(this.txt50yrExtreme10min);
            this.tabPage4.Controls.Add(this.label193);
            this.tabPage4.Controls.Add(this.label194);
            this.tabPage4.Controls.Add(this.label192);
            this.tabPage4.Controls.Add(this.label191);
            this.tabPage4.Controls.Add(this.label170);
            this.tabPage4.Location = new System.Drawing.Point(4, 27);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1453, 626);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Extreme WS";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnExportExtremeWSTable
            // 
            this.btnExportExtremeWSTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportExtremeWSTable.Location = new System.Drawing.Point(1042, 662);
            this.btnExportExtremeWSTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportExtremeWSTable.Name = "btnExportExtremeWSTable";
            this.btnExportExtremeWSTable.Size = new System.Drawing.Size(120, 46);
            this.btnExportExtremeWSTable.TabIndex = 324;
            this.btnExportExtremeWSTable.Text = "Export Extreme WS Table";
            this.btnExportExtremeWSTable.UseVisualStyleBackColor = true;
            this.btnExportExtremeWSTable.Click += new System.EventHandler(this.btnExportExtremeWSTable_Click);
            // 
            // txtWMO_Desc
            // 
            this.txtWMO_Desc.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.txtWMO_Desc.Location = new System.Drawing.Point(170, 240);
            this.txtWMO_Desc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWMO_Desc.Name = "txtWMO_Desc";
            this.txtWMO_Desc.ReadOnly = true;
            this.txtWMO_Desc.Size = new System.Drawing.Size(156, 22);
            this.txtWMO_Desc.TabIndex = 323;
            // 
            // label242
            // 
            this.label242.AutoSize = true;
            this.label242.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.label242.Location = new System.Drawing.Point(184, 273);
            this.label242.Name = "label242";
            this.label242.Size = new System.Drawing.Size(75, 16);
            this.label242.TabIndex = 322;
            this.label242.Text = "Hour-to-Gust:";
            // 
            // txtWMO_HourGust
            // 
            this.txtWMO_HourGust.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.txtWMO_HourGust.Location = new System.Drawing.Point(261, 270);
            this.txtWMO_HourGust.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWMO_HourGust.Name = "txtWMO_HourGust";
            this.txtWMO_HourGust.ReadOnly = true;
            this.txtWMO_HourGust.Size = new System.Drawing.Size(65, 22);
            this.txtWMO_HourGust.TabIndex = 321;
            // 
            // label243
            // 
            this.label243.AutoSize = true;
            this.label243.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.label243.Location = new System.Drawing.Point(20, 273);
            this.label243.Name = "label243";
            this.label243.Size = new System.Drawing.Size(82, 16);
            this.label243.TabIndex = 320;
            this.label243.Text = "Hour-to-10min:";
            // 
            // txtWMO_HourTenMin
            // 
            this.txtWMO_HourTenMin.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.txtWMO_HourTenMin.Location = new System.Drawing.Point(105, 270);
            this.txtWMO_HourTenMin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWMO_HourTenMin.Name = "txtWMO_HourTenMin";
            this.txtWMO_HourTenMin.ReadOnly = true;
            this.txtWMO_HourTenMin.Size = new System.Drawing.Size(65, 22);
            this.txtWMO_HourTenMin.TabIndex = 319;
            // 
            // label241
            // 
            this.label241.AutoSize = true;
            this.label241.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Underline);
            this.label241.Location = new System.Drawing.Point(41, 221);
            this.label241.Name = "label241";
            this.label241.Size = new System.Drawing.Size(147, 16);
            this.label241.TabIndex = 317;
            this.label241.Text = "WMO Gust Factor Estimates";
            // 
            // chkUseWMO_TenMin
            // 
            this.chkUseWMO_TenMin.AutoSize = true;
            this.chkUseWMO_TenMin.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.chkUseWMO_TenMin.Location = new System.Drawing.Point(34, 496);
            this.chkUseWMO_TenMin.Name = "chkUseWMO_TenMin";
            this.chkUseWMO_TenMin.Size = new System.Drawing.Size(189, 20);
            this.chkUseWMO_TenMin.TabIndex = 316;
            this.chkUseWMO_TenMin.Text = "Use WMO Estimated Gust Factor";
            this.chkUseWMO_TenMin.UseVisualStyleBackColor = true;
            this.chkUseWMO_TenMin.CheckedChanged += new System.EventHandler(this.chkUseWMO_TenMin_CheckedChanged);
            // 
            // label240
            // 
            this.label240.AutoSize = true;
            this.label240.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.label240.Location = new System.Drawing.Point(12, 244);
            this.label240.Name = "label240";
            this.label240.Size = new System.Drawing.Size(39, 16);
            this.label240.TabIndex = 315;
            this.label240.Text = "Class :";
            // 
            // cboWMO_Class
            // 
            this.cboWMO_Class.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.cboWMO_Class.FormattingEnabled = true;
            this.cboWMO_Class.Location = new System.Drawing.Point(55, 241);
            this.cboWMO_Class.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboWMO_Class.Name = "cboWMO_Class";
            this.cboWMO_Class.Size = new System.Drawing.Size(101, 24);
            this.cboWMO_Class.TabIndex = 314;
            this.cboWMO_Class.SelectedIndexChanged += new System.EventHandler(this.cboWMO_Class_SelectedIndexChanged);
            // 
            // chkUseWMO_Gust
            // 
            this.chkUseWMO_Gust.AutoSize = true;
            this.chkUseWMO_Gust.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.chkUseWMO_Gust.Location = new System.Drawing.Point(34, 335);
            this.chkUseWMO_Gust.Name = "chkUseWMO_Gust";
            this.chkUseWMO_Gust.Size = new System.Drawing.Size(189, 20);
            this.chkUseWMO_Gust.TabIndex = 313;
            this.chkUseWMO_Gust.Text = "Use WMO Estimated Gust Factor";
            this.chkUseWMO_Gust.UseVisualStyleBackColor = true;
            this.chkUseWMO_Gust.CheckedChanged += new System.EventHandler(this.chkUseWMO_Gust_CheckedChanged);
            // 
            // chkExtremeWS_ShowLegend
            // 
            this.chkExtremeWS_ShowLegend.AutoSize = true;
            this.chkExtremeWS_ShowLegend.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.chkExtremeWS_ShowLegend.Location = new System.Drawing.Point(1335, 8);
            this.chkExtremeWS_ShowLegend.Name = "chkExtremeWS_ShowLegend";
            this.chkExtremeWS_ShowLegend.Size = new System.Drawing.Size(96, 20);
            this.chkExtremeWS_ShowLegend.TabIndex = 312;
            this.chkExtremeWS_ShowLegend.Text = "Show Legend";
            this.chkExtremeWS_ShowLegend.UseVisualStyleBackColor = true;
            this.chkExtremeWS_ShowLegend.CheckedChanged += new System.EventHandler(this.chkExtremeWS_ShowLegend_CheckedChanged);
            // 
            // label239
            // 
            this.label239.AutoSize = true;
            this.label239.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label239.Location = new System.Drawing.Point(167, 88);
            this.label239.Name = "label239";
            this.label239.Size = new System.Drawing.Size(20, 18);
            this.label239.TabIndex = 311;
            this.label239.Text = "to";
            this.label239.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dateExtremeWS_End
            // 
            this.dateExtremeWS_End.CustomFormat = "MM/dd/yy HH:mm";
            this.dateExtremeWS_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateExtremeWS_End.Location = new System.Drawing.Point(190, 85);
            this.dateExtremeWS_End.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateExtremeWS_End.Name = "dateExtremeWS_End";
            this.dateExtremeWS_End.Size = new System.Drawing.Size(129, 25);
            this.dateExtremeWS_End.TabIndex = 310;
            this.dateExtremeWS_End.ValueChanged += new System.EventHandler(this.dateExtremeWS_End_ValueChanged);
            // 
            // dateExtremeWS_Start
            // 
            this.dateExtremeWS_Start.CustomFormat = "MM/dd/yy HH:mm";
            this.dateExtremeWS_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateExtremeWS_Start.Location = new System.Drawing.Point(34, 85);
            this.dateExtremeWS_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateExtremeWS_Start.Name = "dateExtremeWS_Start";
            this.dateExtremeWS_Start.Size = new System.Drawing.Size(129, 25);
            this.dateExtremeWS_Start.TabIndex = 308;
            this.dateExtremeWS_Start.ValueChanged += new System.EventHandler(this.dateExtremeWS_Start_ValueChanged);
            // 
            // lblGustExtremeWSUnavailable
            // 
            this.lblGustExtremeWSUnavailable.AutoSize = true;
            this.lblGustExtremeWSUnavailable.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.lblGustExtremeWSUnavailable.ForeColor = System.Drawing.Color.Red;
            this.lblGustExtremeWSUnavailable.Location = new System.Drawing.Point(20, 450);
            this.lblGustExtremeWSUnavailable.Name = "lblGustExtremeWSUnavailable";
            this.lblGustExtremeWSUnavailable.Size = new System.Drawing.Size(314, 17);
            this.lblGustExtremeWSUnavailable.TabIndex = 307;
            this.lblGustExtremeWSUnavailable.Text = "Cannot Estimate Extreme Gust at Extrapolated Heights";
            // 
            // label238
            // 
            this.label238.AutoSize = true;
            this.label238.Location = new System.Drawing.Point(41, 192);
            this.label238.Name = "label238";
            this.label238.Size = new System.Drawing.Size(169, 19);
            this.label238.TabIndex = 306;
            this.label238.Text = "Extreme WS Height [m] :";
            // 
            // cboExtremeWS_Height
            // 
            this.cboExtremeWS_Height.FormattingEnabled = true;
            this.cboExtremeWS_Height.Location = new System.Drawing.Point(216, 189);
            this.cboExtremeWS_Height.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExtremeWS_Height.Name = "cboExtremeWS_Height";
            this.cboExtremeWS_Height.Size = new System.Drawing.Size(92, 26);
            this.cboExtremeWS_Height.TabIndex = 305;
            this.cboExtremeWS_Height.SelectedIndexChanged += new System.EventHandler(this.cboExtremeWS_Height_SelectedIndexChanged);
            // 
            // label235
            // 
            this.label235.AutoSize = true;
            this.label235.Location = new System.Drawing.Point(172, 365);
            this.label235.Name = "label235";
            this.label235.Size = new System.Drawing.Size(26, 19);
            this.label235.TabIndex = 304;
            this.label235.Text = "μ :";
            // 
            // txtGumbelGustMu
            // 
            this.txtGumbelGustMu.Location = new System.Drawing.Point(210, 362);
            this.txtGumbelGustMu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGumbelGustMu.Name = "txtGumbelGustMu";
            this.txtGumbelGustMu.Size = new System.Drawing.Size(83, 25);
            this.txtGumbelGustMu.TabIndex = 303;
            // 
            // label236
            // 
            this.label236.AutoSize = true;
            this.label236.Location = new System.Drawing.Point(41, 365);
            this.label236.Name = "label236";
            this.label236.Size = new System.Drawing.Size(26, 19);
            this.label236.TabIndex = 302;
            this.label236.Text = "β :";
            // 
            // txtGumbelGustBeta
            // 
            this.txtGumbelGustBeta.Location = new System.Drawing.Point(73, 362);
            this.txtGumbelGustBeta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGumbelGustBeta.Name = "txtGumbelGustBeta";
            this.txtGumbelGustBeta.Size = new System.Drawing.Size(83, 25);
            this.txtGumbelGustBeta.TabIndex = 301;
            // 
            // label234
            // 
            this.label234.AutoSize = true;
            this.label234.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Underline);
            this.label234.Location = new System.Drawing.Point(42, 472);
            this.label234.Name = "label234";
            this.label234.Size = new System.Drawing.Size(221, 19);
            this.label234.TabIndex = 300;
            this.label234.Text = "10-Min. WS Gumbel Distribution";
            // 
            // label233
            // 
            this.label233.AutoSize = true;
            this.label233.Location = new System.Drawing.Point(169, 527);
            this.label233.Name = "label233";
            this.label233.Size = new System.Drawing.Size(26, 19);
            this.label233.TabIndex = 299;
            this.label233.Text = "μ :";
            // 
            // txtGumbelTenMinMu
            // 
            this.txtGumbelTenMinMu.Location = new System.Drawing.Point(210, 527);
            this.txtGumbelTenMinMu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGumbelTenMinMu.Name = "txtGumbelTenMinMu";
            this.txtGumbelTenMinMu.Size = new System.Drawing.Size(83, 25);
            this.txtGumbelTenMinMu.TabIndex = 298;
            // 
            // label232
            // 
            this.label232.AutoSize = true;
            this.label232.Location = new System.Drawing.Point(38, 527);
            this.label232.Name = "label232";
            this.label232.Size = new System.Drawing.Size(26, 19);
            this.label232.TabIndex = 297;
            this.label232.Text = "β :";
            // 
            // label231
            // 
            this.label231.AutoSize = true;
            this.label231.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Underline);
            this.label231.Location = new System.Drawing.Point(38, 308);
            this.label231.Name = "label231";
            this.label231.Size = new System.Drawing.Size(203, 19);
            this.label231.TabIndex = 296;
            this.label231.Text = "Gust WS Gumbel Distribution";
            // 
            // txtGumbelTenMinBeta
            // 
            this.txtGumbelTenMinBeta.Location = new System.Drawing.Point(73, 527);
            this.txtGumbelTenMinBeta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGumbelTenMinBeta.Name = "txtGumbelTenMinBeta";
            this.txtGumbelTenMinBeta.Size = new System.Drawing.Size(83, 25);
            this.txtGumbelTenMinBeta.TabIndex = 295;
            // 
            // chkUseSimData
            // 
            this.chkUseSimData.AutoSize = true;
            this.chkUseSimData.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.chkUseSimData.Location = new System.Drawing.Point(46, 122);
            this.chkUseSimData.Name = "chkUseSimData";
            this.chkUseSimData.Size = new System.Drawing.Size(288, 20);
            this.chkUseSimData.TabIndex = 294;
            this.chkUseSimData.Text = "Use Long-Term Simulated (MCP\'d) data as Reference";
            this.chkUseSimData.UseVisualStyleBackColor = true;
            this.chkUseSimData.CheckedChanged += new System.EventHandler(this.chkUseSimData_CheckedChanged);
            // 
            // plotExtremeWS_TS
            // 
            this.plotExtremeWS_TS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotExtremeWS_TS.Location = new System.Drawing.Point(921, 24);
            this.plotExtremeWS_TS.Name = "plotExtremeWS_TS";
            this.plotExtremeWS_TS.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotExtremeWS_TS.Size = new System.Drawing.Size(510, 350);
            this.plotExtremeWS_TS.TabIndex = 293;
            this.plotExtremeWS_TS.Text = "plotView1";
            this.plotExtremeWS_TS.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotExtremeWS_TS.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotExtremeWS_TS.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // dataExtremeWS
            // 
            this.dataExtremeWS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataExtremeWS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataExtremeWS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column17,
            this.Column1,
            this.Column20,
            this.Column18,
            this.Column2,
            this.Column19,
            this.Column16});
            this.dataExtremeWS.Location = new System.Drawing.Point(359, 17);
            this.dataExtremeWS.Name = "dataExtremeWS";
            this.dataExtremeWS.RowHeadersWidth = 51;
            this.dataExtremeWS.Size = new System.Drawing.Size(548, 694);
            this.dataExtremeWS.TabIndex = 292;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "Year";
            this.Column17.MinimumWidth = 6;
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Width = 50;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Max. Yearly Ref. Hourly WS [m/s]";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 70;
            // 
            // Column20
            // 
            this.Column20.HeaderText = "Max. Yearly Ref. Hourly WS Conc. With Met [m/s]";
            this.Column20.MinimumWidth = 6;
            this.Column20.Name = "Column20";
            this.Column20.Width = 95;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "Max. Yearly Actual 10-min WS [m/s]";
            this.Column18.MinimumWidth = 6;
            this.Column18.Name = "Column18";
            this.Column18.Width = 70;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Max. Yearly Est. 10-min WS [m/s]";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 70;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "Max. Yearly Actual Gust WS [m/s]";
            this.Column19.MinimumWidth = 6;
            this.Column19.Name = "Column19";
            this.Column19.Width = 70;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Max Yearly Est. Gust WS [m/s]";
            this.Column16.MinimumWidth = 6;
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Width = 70;
            // 
            // label195
            // 
            this.label195.AutoSize = true;
            this.label195.Location = new System.Drawing.Point(13, 49);
            this.label195.Name = "label195";
            this.label195.Size = new System.Drawing.Size(41, 19);
            this.label195.TabIndex = 290;
            this.label195.Text = "Met :";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(13, 155);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(75, 19);
            this.label103.TabIndex = 289;
            this.label103.Text = "Reference :";
            // 
            // cboExtremeWSRef
            // 
            this.cboExtremeWSRef.FormattingEnabled = true;
            this.cboExtremeWSRef.Location = new System.Drawing.Point(94, 152);
            this.cboExtremeWSRef.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExtremeWSRef.Name = "cboExtremeWSRef";
            this.cboExtremeWSRef.Size = new System.Drawing.Size(232, 26);
            this.cboExtremeWSRef.TabIndex = 288;
            this.cboExtremeWSRef.SelectedIndexChanged += new System.EventHandler(this.cboExtremeWSRef_SelectedIndexChanged);
            // 
            // plotExtremeWS
            // 
            this.plotExtremeWS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotExtremeWS.Location = new System.Drawing.Point(921, 298);
            this.plotExtremeWS.Name = "plotExtremeWS";
            this.plotExtremeWS.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotExtremeWS.Size = new System.Drawing.Size(510, 350);
            this.plotExtremeWS.TabIndex = 287;
            this.plotExtremeWS.Text = "plotView1";
            this.plotExtremeWS.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotExtremeWS.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotExtremeWS.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotExtremeWS.Click += new System.EventHandler(this.plotExtremeWS_Click);
            // 
            // lblExtremeWS
            // 
            this.lblExtremeWS.AutoSize = true;
            this.lblExtremeWS.Location = new System.Drawing.Point(167, 439);
            this.lblExtremeWS.Name = "lblExtremeWS";
            this.lblExtremeWS.Size = new System.Drawing.Size(0, 19);
            this.lblExtremeWS.TabIndex = 286;
            // 
            // btnExtremeWS
            // 
            this.btnExtremeWS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExtremeWS.Location = new System.Drawing.Point(916, 662);
            this.btnExtremeWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExtremeWS.Name = "btnExtremeWS";
            this.btnExtremeWS.Size = new System.Drawing.Size(120, 46);
            this.btnExtremeWS.TabIndex = 285;
            this.btnExtremeWS.Text = "Export Extreme WS Ests.";
            this.btnExtremeWS.UseVisualStyleBackColor = true;
            this.btnExtremeWS.Click += new System.EventHandler(this.btnExtremeWS_Click);
            // 
            // cboExtremeWSMet
            // 
            this.cboExtremeWSMet.FormattingEnabled = true;
            this.cboExtremeWSMet.Location = new System.Drawing.Point(60, 46);
            this.cboExtremeWSMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExtremeWSMet.Name = "cboExtremeWSMet";
            this.cboExtremeWSMet.Size = new System.Drawing.Size(266, 26);
            this.cboExtremeWSMet.TabIndex = 284;
            this.cboExtremeWSMet.SelectedIndexChanged += new System.EventHandler(this.cboExtremeWSMet_SelectedIndexChanged_1);
            // 
            // txt1yrExtremeGust
            // 
            this.txt1yrExtremeGust.Location = new System.Drawing.Point(210, 391);
            this.txt1yrExtremeGust.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt1yrExtremeGust.Name = "txt1yrExtremeGust";
            this.txt1yrExtremeGust.Size = new System.Drawing.Size(83, 25);
            this.txt1yrExtremeGust.TabIndex = 283;
            // 
            // txt1yrExtreme10min
            // 
            this.txt1yrExtreme10min.Location = new System.Drawing.Point(212, 561);
            this.txt1yrExtreme10min.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt1yrExtreme10min.Name = "txt1yrExtreme10min";
            this.txt1yrExtreme10min.Size = new System.Drawing.Size(83, 25);
            this.txt1yrExtreme10min.TabIndex = 282;
            // 
            // txt50yrExtremeGust
            // 
            this.txt50yrExtremeGust.Location = new System.Drawing.Point(210, 421);
            this.txt50yrExtremeGust.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt50yrExtremeGust.Name = "txt50yrExtremeGust";
            this.txt50yrExtremeGust.Size = new System.Drawing.Size(83, 25);
            this.txt50yrExtremeGust.TabIndex = 281;
            // 
            // txt50yrExtreme10min
            // 
            this.txt50yrExtreme10min.Location = new System.Drawing.Point(212, 591);
            this.txt50yrExtreme10min.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt50yrExtreme10min.Name = "txt50yrExtreme10min";
            this.txt50yrExtreme10min.Size = new System.Drawing.Size(83, 25);
            this.txt50yrExtreme10min.TabIndex = 280;
            // 
            // label193
            // 
            this.label193.AutoSize = true;
            this.label193.Location = new System.Drawing.Point(38, 391);
            this.label193.Name = "label193";
            this.label193.Size = new System.Drawing.Size(166, 19);
            this.label193.TabIndex = 279;
            this.label193.Text = "1 yr Extreme (3-sec gust)";
            // 
            // label194
            // 
            this.label194.AutoSize = true;
            this.label194.Location = new System.Drawing.Point(27, 563);
            this.label194.Name = "label194";
            this.label194.Size = new System.Drawing.Size(173, 19);
            this.label194.TabIndex = 278;
            this.label194.Text = "1 yr Extreme WS (10 min)";
            // 
            // label192
            // 
            this.label192.AutoSize = true;
            this.label192.Location = new System.Drawing.Point(38, 425);
            this.label192.Name = "label192";
            this.label192.Size = new System.Drawing.Size(173, 19);
            this.label192.TabIndex = 277;
            this.label192.Text = "50 yr Extreme (3-sec gust)";
            // 
            // label191
            // 
            this.label191.AutoSize = true;
            this.label191.Location = new System.Drawing.Point(26, 595);
            this.label191.Name = "label191";
            this.label191.Size = new System.Drawing.Size(180, 19);
            this.label191.TabIndex = 276;
            this.label191.Text = "50 yr Extreme WS (10 min)";
            // 
            // label170
            // 
            this.label170.AutoSize = true;
            this.label170.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label170.Location = new System.Drawing.Point(19, 16);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(201, 23);
            this.label170.TabIndex = 275;
            this.label170.Text = "Extreme Wind Speed";
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
            this.pgeMonthlyAnalysis.AutoScroll = true;
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
            this.pgeMonthlyAnalysis.Size = new System.Drawing.Size(1488, 712);
            this.pgeMonthlyAnalysis.TabIndex = 17;
            this.pgeMonthlyAnalysis.Text = "Time Series Analysis";
            this.pgeMonthlyAnalysis.UseVisualStyleBackColor = true;
            // 
            // plotMonthlyTS
            // 
            this.plotMonthlyTS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.plotMonthlyTS.Location = new System.Drawing.Point(760, 428);
            this.plotMonthlyTS.Name = "plotMonthlyTS";
            this.plotMonthlyTS.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMonthlyTS.Size = new System.Drawing.Size(712, 269);
            this.plotMonthlyTS.TabIndex = 279;
            this.plotMonthlyTS.Text = "plotView2";
            this.plotMonthlyTS.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMonthlyTS.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMonthlyTS.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotYearlyTS
            // 
            this.plotYearlyTS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotYearlyTS.Location = new System.Drawing.Point(14, 428);
            this.plotYearlyTS.Name = "plotYearlyTS";
            this.plotYearlyTS.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotYearlyTS.Size = new System.Drawing.Size(733, 269);
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
            this.btnExportHourlyTurbineValues.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportHourlyTurbineValues.Location = new System.Drawing.Point(648, 318);
            this.btnExportHourlyTurbineValues.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportHourlyTurbineValues.Name = "btnExportHourlyTurbineValues";
            this.btnExportHourlyTurbineValues.Size = new System.Drawing.Size(105, 30);
            this.btnExportHourlyTurbineValues.TabIndex = 227;
            this.btnExportHourlyTurbineValues.Text = "Export Hourly";
            this.btnExportHourlyTurbineValues.UseVisualStyleBackColor = true;
            this.btnExportHourlyTurbineValues.Click += new System.EventHandler(this.btnExportHourlyTurbineValues_Click);
            // 
            // btnExportMonthlyTurbineValues
            // 
            this.btnExportMonthlyTurbineValues.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportMonthlyTurbineValues.Location = new System.Drawing.Point(648, 352);
            this.btnExportMonthlyTurbineValues.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportMonthlyTurbineValues.Name = "btnExportMonthlyTurbineValues";
            this.btnExportMonthlyTurbineValues.Size = new System.Drawing.Size(105, 30);
            this.btnExportMonthlyTurbineValues.TabIndex = 226;
            this.btnExportMonthlyTurbineValues.Text = "Export Monthly";
            this.btnExportMonthlyTurbineValues.UseVisualStyleBackColor = true;
            this.btnExportMonthlyTurbineValues.Click += new System.EventHandler(this.btnExportMonthlyTurbineValues_Click);
            // 
            // btnExportYearlyTurbineValues
            // 
            this.btnExportYearlyTurbineValues.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportYearlyTurbineValues.Location = new System.Drawing.Point(648, 386);
            this.btnExportYearlyTurbineValues.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportYearlyTurbineValues.Name = "btnExportYearlyTurbineValues";
            this.btnExportYearlyTurbineValues.Size = new System.Drawing.Size(105, 30);
            this.btnExportYearlyTurbineValues.TabIndex = 225;
            this.btnExportYearlyTurbineValues.Text = "Export Annual";
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
            this.label148.Location = new System.Drawing.Point(649, 203);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(101, 17);
            this.label148.TabIndex = 220;
            this.label148.Text = "Plot Parameters:";
            // 
            // chkSelectedTurbineParam
            // 
            this.chkSelectedTurbineParam.CheckOnClick = true;
            this.chkSelectedTurbineParam.FormattingEnabled = true;
            this.chkSelectedTurbineParam.Location = new System.Drawing.Point(650, 226);
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
            this.chkSelectAllTurbineYears.Location = new System.Drawing.Point(649, 14);
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
            this.chkYears_Monthly.Location = new System.Drawing.Point(649, 49);
            this.chkYears_Monthly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkYears_Monthly.Name = "chkYears_Monthly";
            this.chkYears_Monthly.Size = new System.Drawing.Size(98, 144);
            this.chkYears_Monthly.TabIndex = 213;
            this.chkYears_Monthly.SelectedIndexChanged += new System.EventHandler(this.chkYears_Monthly_SelectedIndexChanged);
            // 
            // lstYearlyTurbine
            // 
            this.lstYearlyTurbine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lstMonthlyTurbine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lstMonthlyTurbine.Size = new System.Drawing.Size(695, 377);
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
            this.pgeMaps.AutoScroll = true;
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
            this.pgeMaps.Size = new System.Drawing.Size(1488, 712);
            this.pgeMaps.TabIndex = 8;
            this.pgeMaps.Text = "Maps";
            this.pgeMaps.UseVisualStyleBackColor = true;
            // 
            // plotGenMap
            // 
            this.plotGenMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotGenMap.Location = new System.Drawing.Point(675, 14);
            this.plotGenMap.Name = "plotGenMap";
            this.plotGenMap.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGenMap.Size = new System.Drawing.Size(791, 680);
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
            this.Label72.Location = new System.Drawing.Point(541, 14);
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
            this.cboMapWD.Location = new System.Drawing.Point(532, 38);
            this.cboMapWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMapWD.Name = "cboMapWD";
            this.cboMapWD.Size = new System.Drawing.Size(115, 24);
            this.cboMapWD.TabIndex = 193;
            this.cboMapWD.SelectedIndexChanged += new System.EventHandler(this.cboMapWD_SelectedIndexChanged);
            // 
            // txtMap_MetsUsed
            // 
            this.txtMap_MetsUsed.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMap_MetsUsed.Location = new System.Drawing.Point(114, 409);
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
            this.Label63.Location = new System.Drawing.Point(34, 412);
            this.Label63.Name = "Label63";
            this.Label63.Size = new System.Drawing.Size(73, 18);
            this.Label63.TabIndex = 93;
            this.Label63.Text = "Mets Used:";
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
            this.Label27.Location = new System.Drawing.Point(489, 456);
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
            this.chkAllTurbs_Maps.Location = new System.Drawing.Point(323, 583);
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
            this.chkAllMets_Maps.Location = new System.Drawing.Point(321, 451);
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
            this.Label28.Location = new System.Drawing.Point(227, 582);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(72, 19);
            this.Label28.TabIndex = 80;
            this.Label28.Text = "Turbines";
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label29.Location = new System.Drawing.Point(227, 449);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(73, 19);
            this.Label29.TabIndex = 79;
            this.Label29.Text = "Met Sites";
            // 
            // chkTurbLabels_Maps
            // 
            this.chkTurbLabels_Maps.CheckOnClick = true;
            this.chkTurbLabels_Maps.FormattingEnabled = true;
            this.chkTurbLabels_Maps.Location = new System.Drawing.Point(231, 612);
            this.chkTurbLabels_Maps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbLabels_Maps.Name = "chkTurbLabels_Maps";
            this.chkTurbLabels_Maps.Size = new System.Drawing.Size(200, 84);
            this.chkTurbLabels_Maps.TabIndex = 78;
            this.chkTurbLabels_Maps.SelectedIndexChanged += new System.EventHandler(this.chkTurbLabels_Maps_SelectedIndexChanged);
            // 
            // chkMetLabels_Maps
            // 
            this.chkMetLabels_Maps.CheckOnClick = true;
            this.chkMetLabels_Maps.FormattingEnabled = true;
            this.chkMetLabels_Maps.Location = new System.Drawing.Point(231, 479);
            this.chkMetLabels_Maps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetLabels_Maps.Name = "chkMetLabels_Maps";
            this.chkMetLabels_Maps.Size = new System.Drawing.Size(200, 84);
            this.chkMetLabels_Maps.TabIndex = 77;
            this.chkMetLabels_Maps.SelectedIndexChanged += new System.EventHandler(this.chkMetLabels_Maps_SelectedIndexChanged);
            // 
            // btnRefreshMap
            // 
            this.btnRefreshMap.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshMap.Location = new System.Drawing.Point(507, 608);
            this.btnRefreshMap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefreshMap.Name = "btnRefreshMap";
            this.btnRefreshMap.Size = new System.Drawing.Size(97, 38);
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
            this.chkAutoMinMax.Location = new System.Drawing.Point(467, 578);
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
            this.txtIntLevel.Location = new System.Drawing.Point(532, 542);
            this.txtIntLevel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtIntLevel.Name = "txtIntLevel";
            this.txtIntLevel.Size = new System.Drawing.Size(61, 25);
            this.txtIntLevel.TabIndex = 52;
            // 
            // txtMaxValue
            // 
            this.txtMaxValue.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxValue.Location = new System.Drawing.Point(532, 512);
            this.txtMaxValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaxValue.Name = "txtMaxValue";
            this.txtMaxValue.Size = new System.Drawing.Size(61, 25);
            this.txtMaxValue.TabIndex = 50;
            // 
            // txtMinValue
            // 
            this.txtMinValue.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinValue.Location = new System.Drawing.Point(532, 483);
            this.txtMinValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMinValue.Name = "txtMinValue";
            this.txtMinValue.Size = new System.Drawing.Size(61, 25);
            this.txtMinValue.TabIndex = 48;
            // 
            // txtMapMax
            // 
            this.txtMapMax.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapMax.Location = new System.Drawing.Point(104, 626);
            this.txtMapMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMapMax.Name = "txtMapMax";
            this.txtMapMax.ReadOnly = true;
            this.txtMapMax.Size = new System.Drawing.Size(80, 25);
            this.txtMapMax.TabIndex = 41;
            // 
            // txtMapMin
            // 
            this.txtMapMin.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapMin.Location = new System.Drawing.Point(104, 582);
            this.txtMapMin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMapMin.Name = "txtMapMin";
            this.txtMapMin.ReadOnly = true;
            this.txtMapMin.Size = new System.Drawing.Size(80, 25);
            this.txtMapMin.TabIndex = 40;
            // 
            // txtMapCount
            // 
            this.txtMapCount.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapCount.Location = new System.Drawing.Point(104, 670);
            this.txtMapCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMapCount.Name = "txtMapCount";
            this.txtMapCount.ReadOnly = true;
            this.txtMapCount.Size = new System.Drawing.Size(80, 25);
            this.txtMapCount.TabIndex = 37;
            // 
            // txtMapStDev
            // 
            this.txtMapStDev.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapStDev.Location = new System.Drawing.Point(104, 538);
            this.txtMapStDev.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMapStDev.Name = "txtMapStDev";
            this.txtMapStDev.ReadOnly = true;
            this.txtMapStDev.Size = new System.Drawing.Size(80, 25);
            this.txtMapStDev.TabIndex = 36;
            // 
            // txtMapAvg
            // 
            this.txtMapAvg.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapAvg.Location = new System.Drawing.Point(104, 494);
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
            this.lblInterval.Location = new System.Drawing.Point(471, 547);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(59, 18);
            this.lblInterval.TabIndex = 51;
            this.lblInterval.Text = "Interval:";
            // 
            // lblMaxVal
            // 
            this.lblMaxVal.AutoSize = true;
            this.lblMaxVal.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxVal.Location = new System.Drawing.Point(490, 515);
            this.lblMaxVal.Name = "lblMaxVal";
            this.lblMaxVal.Size = new System.Drawing.Size(37, 18);
            this.lblMaxVal.TabIndex = 49;
            this.lblMaxVal.Text = "Max:";
            // 
            // lblMinVal
            // 
            this.lblMinVal.AutoSize = true;
            this.lblMinVal.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinVal.Location = new System.Drawing.Point(492, 485);
            this.lblMinVal.Name = "lblMinVal";
            this.lblMinVal.Size = new System.Drawing.Size(35, 18);
            this.lblMinVal.TabIndex = 47;
            this.lblMinVal.Text = "Min:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.Location = new System.Drawing.Point(29, 629);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(76, 18);
            this.Label10.TabIndex = 39;
            this.Label10.Text = "Maximum :";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(34, 585);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(74, 18);
            this.Label15.TabIndex = 38;
            this.Label15.Text = "Minimum :";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(50, 672);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(51, 18);
            this.Label16.TabIndex = 34;
            this.Label16.Text = "Count :";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(43, 542);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(58, 18);
            this.Label17.TabIndex = 33;
            this.Label17.Text = "St. Dev. :";
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(38, 496);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(63, 18);
            this.Label18.TabIndex = 32;
            this.Label18.Text = "Average :";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(29, 459);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(137, 23);
            this.Label3.TabIndex = 27;
            this.Label3.Text = "Map Statistics";
            // 
            // btnDelMaps
            // 
            this.btnDelMaps.Location = new System.Drawing.Point(149, 64);
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
            this.btnMapExportCSV.Location = new System.Drawing.Point(258, 64);
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
            this.btnExportWRG.Location = new System.Drawing.Point(395, 64);
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
            this.btnGenMap.Location = new System.Drawing.Point(27, 64);
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
            this.lstMaps.Location = new System.Drawing.Point(27, 131);
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
            this.pgeRound.AutoScroll = true;
            this.pgeRound.Controls.Add(this.btnExportRR_Summary);
            this.pgeRound.Controls.Add(this.btnDoAllRRs);
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
            this.pgeRound.Size = new System.Drawing.Size(1488, 712);
            this.pgeRound.TabIndex = 11;
            this.pgeRound.Text = "Uncertainty Analysis";
            this.pgeRound.UseVisualStyleBackColor = true;
            this.pgeRound.Click += new System.EventHandler(this.pgeRound_Click);
            // 
            // btnExportRR_Summary
            // 
            this.btnExportRR_Summary.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportRR_Summary.Location = new System.Drawing.Point(15, 472);
            this.btnExportRR_Summary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportRR_Summary.Name = "btnExportRR_Summary";
            this.btnExportRR_Summary.Size = new System.Drawing.Size(127, 30);
            this.btnExportRR_Summary.TabIndex = 284;
            this.btnExportRR_Summary.Text = "Export Summary";
            this.btnExportRR_Summary.UseVisualStyleBackColor = true;
            this.btnExportRR_Summary.Click += new System.EventHandler(this.btnExportRR_Summary_Click);
            // 
            // btnDoAllRRs
            // 
            this.btnDoAllRRs.BackColor = System.Drawing.Color.LightCoral;
            this.btnDoAllRRs.Location = new System.Drawing.Point(15, 398);
            this.btnDoAllRRs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDoAllRRs.Name = "btnDoAllRRs";
            this.btnDoAllRRs.Size = new System.Drawing.Size(127, 29);
            this.btnDoAllRRs.TabIndex = 283;
            this.btnDoAllRRs.Text = "Do All RRs";
            this.btnDoAllRRs.UseVisualStyleBackColor = false;
            this.btnDoAllRRs.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // plotTurbUncert
            // 
            this.plotTurbUncert.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotTurbUncert.Location = new System.Drawing.Point(963, 507);
            this.plotTurbUncert.Name = "plotTurbUncert";
            this.plotTurbUncert.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTurbUncert.Size = new System.Drawing.Size(506, 192);
            this.plotTurbUncert.TabIndex = 282;
            this.plotTurbUncert.Text = "plotView1";
            this.plotTurbUncert.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTurbUncert.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTurbUncert.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotRR_Histo
            // 
            this.plotRR_Histo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.plotRR_Histo.Location = new System.Drawing.Point(15, 506);
            this.plotRR_Histo.Name = "plotRR_Histo";
            this.plotRR_Histo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotRR_Histo.Size = new System.Drawing.Size(421, 193);
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
            this.plotRRErrorByNumMets.Size = new System.Drawing.Size(421, 240);
            this.plotRRErrorByNumMets.TabIndex = 280;
            this.plotRRErrorByNumMets.Text = "plotView1";
            this.plotRRErrorByNumMets.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotRRErrorByNumMets.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotRRErrorByNumMets.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // txtisMCPdUncert
            // 
            this.txtisMCPdUncert.Location = new System.Drawing.Point(464, 85);
            this.txtisMCPdUncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtisMCPdUncert.Name = "txtisMCPdUncert";
            this.txtisMCPdUncert.ReadOnly = true;
            this.txtisMCPdUncert.Size = new System.Drawing.Size(154, 25);
            this.txtisMCPdUncert.TabIndex = 200;
            // 
            // txtRR_FlowSep_Used
            // 
            this.txtRR_FlowSep_Used.Location = new System.Drawing.Point(784, 85);
            this.txtRR_FlowSep_Used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRR_FlowSep_Used.Name = "txtRR_FlowSep_Used";
            this.txtRR_FlowSep_Used.ReadOnly = true;
            this.txtRR_FlowSep_Used.Size = new System.Drawing.Size(154, 25);
            this.txtRR_FlowSep_Used.TabIndex = 175;
            // 
            // cboRR_MinSize
            // 
            this.cboRR_MinSize.FormattingEnabled = true;
            this.cboRR_MinSize.Location = new System.Drawing.Point(159, 377);
            this.cboRR_MinSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboRR_MinSize.Name = "cboRR_MinSize";
            this.cboRR_MinSize.Size = new System.Drawing.Size(70, 26);
            this.cboRR_MinSize.TabIndex = 174;
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.Location = new System.Drawing.Point(154, 335);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(80, 38);
            this.Label31.TabIndex = 173;
            this.Label31.Text = "Min. Num.\r\nof Mets";
            this.Label31.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtRR_LC_used
            // 
            this.txtRR_LC_used.Location = new System.Drawing.Point(624, 85);
            this.txtRR_LC_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRR_LC_used.Name = "txtRR_LC_used";
            this.txtRR_LC_used.ReadOnly = true;
            this.txtRR_LC_used.Size = new System.Drawing.Size(154, 25);
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
            this.btnExportTurbUncert.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportTurbUncert.Location = new System.Drawing.Point(1359, 20);
            this.btnExportTurbUncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportTurbUncert.Name = "btnExportTurbUncert";
            this.btnExportTurbUncert.Size = new System.Drawing.Size(111, 54);
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
            this.cboUncert_WS_AEP.Location = new System.Drawing.Point(1061, 82);
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
            this.Label47.Location = new System.Drawing.Point(1017, 87);
            this.Label47.Name = "Label47";
            this.Label47.Size = new System.Drawing.Size(38, 18);
            this.Label47.TabIndex = 162;
            this.Label47.Text = "Plot :";
            // 
            // Label45
            // 
            this.Label45.AutoSize = true;
            this.Label45.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label45.Location = new System.Drawing.Point(954, 50);
            this.Label45.Name = "Label45";
            this.Label45.Size = new System.Drawing.Size(93, 18);
            this.Label45.TabIndex = 160;
            this.Label45.Text = "Power Curve :";
            // 
            // cboUncertPowerCrv
            // 
            this.cboUncertPowerCrv.FormattingEnabled = true;
            this.cboUncertPowerCrv.Location = new System.Drawing.Point(1061, 48);
            this.cboUncertPowerCrv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboUncertPowerCrv.Name = "cboUncertPowerCrv";
            this.cboUncertPowerCrv.Size = new System.Drawing.Size(238, 26);
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
            this.lstTurbUncert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lstTurbUncert.Size = new System.Drawing.Size(510, 382);
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
            this.btnExportRR.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportRR.Location = new System.Drawing.Point(15, 435);
            this.btnExportRR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportRR.Name = "btnExportRR";
            this.btnExportRR.Size = new System.Drawing.Size(127, 30);
            this.btnExportRR.TabIndex = 155;
            this.btnExportRR.Text = "Export All RR Ests.";
            this.btnExportRR.UseVisualStyleBackColor = true;
            this.btnExportRR.Click += new System.EventHandler(this.btnExportRR_Click);
            // 
            // lstRR_AllErr
            // 
            this.lstRR_AllErr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lstRR_AllErr.Size = new System.Drawing.Size(490, 582);
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
            this.ColumnHeader38.Width = 65;
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
            this.btnDoRRCalcs.Location = new System.Drawing.Point(15, 342);
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
            this.pgeStepwise.AutoScroll = true;
            this.pgeStepwise.Controls.Add(this.spltNodePlotParams);
            this.pgeStepwise.Controls.Add(this.spltModelCoeffs);
            this.pgeStepwise.Controls.Add(this.spltAdvanced);
            this.pgeStepwise.Controls.Add(this.btnExportRMS_Errors);
            this.pgeStepwise.Controls.Add(this.txtImportedModel);
            this.pgeStepwise.Controls.Add(this.btnImportModel);
            this.pgeStepwise.Controls.Add(this.btnExportModel);
            this.pgeStepwise.Controls.Add(this.plotAdvTopo);
            this.pgeStepwise.Controls.Add(this.txtisMCPdAdv);
            this.pgeStepwise.Controls.Add(this.cboSeasonAdvanced);
            this.pgeStepwise.Controls.Add(this.cboTODAdvanced);
            this.pgeStepwise.Controls.Add(this.txtAdv_FlowSep_Used);
            this.pgeStepwise.Controls.Add(this.txtAdv_LC_used);
            this.pgeStepwise.Controls.Add(this.cboAdvancedWD);
            this.pgeStepwise.Controls.Add(this.Label56);
            this.pgeStepwise.Controls.Add(this.btnExportCrossPreds);
            this.pgeStepwise.Controls.Add(this.Label14);
            this.pgeStepwise.Controls.Add(this.cboAdvancedRad);
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
            this.pgeStepwise.Controls.Add(this.btnExportStepwise);
            this.pgeStepwise.Controls.Add(this.lstModCrossPred);
            this.pgeStepwise.Controls.Add(this.Label11);
            this.pgeStepwise.Controls.Add(this.Label7);
            this.pgeStepwise.Location = new System.Drawing.Point(4, 27);
            this.pgeStepwise.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeStepwise.Name = "pgeStepwise";
            this.pgeStepwise.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeStepwise.Size = new System.Drawing.Size(1488, 712);
            this.pgeStepwise.TabIndex = 10;
            this.pgeStepwise.Text = "Advanced";
            this.pgeStepwise.UseVisualStyleBackColor = true;
            this.pgeStepwise.Click += new System.EventHandler(this.pgeStepwise_Click);
            // 
            // spltNodePlotParams
            // 
            this.spltNodePlotParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spltNodePlotParams.Location = new System.Drawing.Point(474, 310);
            this.spltNodePlotParams.Name = "spltNodePlotParams";
            // 
            // spltNodePlotParams.Panel1
            // 
            this.spltNodePlotParams.Panel1.Controls.Add(this.plotPathAlongNodes);
            this.spltNodePlotParams.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // spltNodePlotParams.Panel2
            // 
            this.spltNodePlotParams.Panel2.Controls.Add(this.chkWeight_RMS);
            this.spltNodePlotParams.Panel2.Controls.Add(this.txtSectRMS);
            this.spltNodePlotParams.Panel2.Controls.Add(this.Label48);
            this.spltNodePlotParams.Panel2.Controls.Add(this.txtUWDWRMS);
            this.spltNodePlotParams.Panel2.Controls.Add(this.Label44);
            this.spltNodePlotParams.Panel2.Controls.Add(this.chkAdvToShow);
            this.spltNodePlotParams.Size = new System.Drawing.Size(1008, 198);
            this.spltNodePlotParams.SplitterDistance = 672;
            this.spltNodePlotParams.TabIndex = 293;
            // 
            // plotPathAlongNodes
            // 
            this.plotPathAlongNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotPathAlongNodes.Location = new System.Drawing.Point(7, 10);
            this.plotPathAlongNodes.Name = "plotPathAlongNodes";
            this.plotPathAlongNodes.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotPathAlongNodes.Size = new System.Drawing.Size(662, 185);
            this.plotPathAlongNodes.TabIndex = 285;
            this.plotPathAlongNodes.Text = "plotView1";
            this.plotPathAlongNodes.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotPathAlongNodes.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotPathAlongNodes.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // chkWeight_RMS
            // 
            this.chkWeight_RMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkWeight_RMS.AutoSize = true;
            this.chkWeight_RMS.Checked = true;
            this.chkWeight_RMS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeight_RMS.Location = new System.Drawing.Point(141, 65);
            this.chkWeight_RMS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkWeight_RMS.Name = "chkWeight_RMS";
            this.chkWeight_RMS.Size = new System.Drawing.Size(181, 23);
            this.chkWeight_RMS.TabIndex = 299;
            this.chkWeight_RMS.Text = "Sect. RMS wgtd by Rose";
            this.chkWeight_RMS.UseVisualStyleBackColor = true;
            // 
            // txtSectRMS
            // 
            this.txtSectRMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSectRMS.Location = new System.Drawing.Point(239, 37);
            this.txtSectRMS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSectRMS.Name = "txtSectRMS";
            this.txtSectRMS.Size = new System.Drawing.Size(69, 25);
            this.txtSectRMS.TabIndex = 298;
            // 
            // Label48
            // 
            this.Label48.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label48.AutoSize = true;
            this.Label48.Location = new System.Drawing.Point(115, 41);
            this.Label48.Name = "Label48";
            this.Label48.Size = new System.Drawing.Size(120, 19);
            this.Label48.TabIndex = 297;
            this.Label48.Text = "RMS Sector Ests :";
            // 
            // txtUWDWRMS
            // 
            this.txtUWDWRMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUWDWRMS.Location = new System.Drawing.Point(239, 5);
            this.txtUWDWRMS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUWDWRMS.Name = "txtUWDWRMS";
            this.txtUWDWRMS.Size = new System.Drawing.Size(69, 25);
            this.txtUWDWRMS.TabIndex = 296;
            // 
            // Label44
            // 
            this.Label44.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label44.AutoSize = true;
            this.Label44.Location = new System.Drawing.Point(119, 10);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(103, 19);
            this.Label44.TabIndex = 295;
            this.Label44.Text = "RMS WS Ests :";
            // 
            // chkAdvToShow
            // 
            this.chkAdvToShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            "dWS Elev",
            "dWS UW Expo",
            "dWS DW Expo",
            "dWS UW SRDH",
            "dWS DW SRDH",
            "dWS Elev",
            "dWS Valley",
            "WS Est.",
            "Equiv WS",
            "Actual WS"});
            this.chkAdvToShow.Location = new System.Drawing.Point(7, 94);
            this.chkAdvToShow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAdvToShow.MultiColumn = true;
            this.chkAdvToShow.Name = "chkAdvToShow";
            this.chkAdvToShow.Size = new System.Drawing.Size(318, 104);
            this.chkAdvToShow.TabIndex = 294;
            this.chkAdvToShow.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkAdvToShow_ItemCheck);
            // 
            // spltModelCoeffs
            // 
            this.spltModelCoeffs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spltModelCoeffs.Location = new System.Drawing.Point(474, 514);
            this.spltModelCoeffs.Name = "spltModelCoeffs";
            // 
            // spltModelCoeffs.Panel1
            // 
            this.spltModelCoeffs.Panel1.Controls.Add(this.plotUHModel);
            this.spltModelCoeffs.Panel1.Controls.Add(this.Label101);
            this.spltModelCoeffs.Panel1.Controls.Add(this.txtSepCritWS);
            this.spltModelCoeffs.Panel1.Controls.Add(this.Label13);
            this.spltModelCoeffs.Panel1.Controls.Add(this.txtSepCrit);
            this.spltModelCoeffs.Panel1.Controls.Add(this.Label100);
            this.spltModelCoeffs.Panel1.Controls.Add(this.cboDHplot);
            // 
            // spltModelCoeffs.Panel2
            // 
            this.spltModelCoeffs.Panel2.Controls.Add(this.plotDHModel);
            this.spltModelCoeffs.Panel2.Controls.Add(this.cboExpo_or_Stab);
            this.spltModelCoeffs.Panel2.Controls.Add(this.Label82);
            this.spltModelCoeffs.Panel2.Controls.Add(this.Label59);
            this.spltModelCoeffs.Panel2.Controls.Add(this.txtUWCrit);
            this.spltModelCoeffs.Panel2.Controls.Add(this.Label58);
            this.spltModelCoeffs.Panel2.Controls.Add(this.cboUphill_to_show);
            this.spltModelCoeffs.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.spltModelCoeffs.Size = new System.Drawing.Size(995, 193);
            this.spltModelCoeffs.SplitterDistance = 494;
            this.spltModelCoeffs.TabIndex = 292;
            // 
            // plotUHModel
            // 
            this.plotUHModel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotUHModel.Location = new System.Drawing.Point(8, 10);
            this.plotUHModel.Name = "plotUHModel";
            this.plotUHModel.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotUHModel.Size = new System.Drawing.Size(355, 180);
            this.plotUHModel.TabIndex = 286;
            this.plotUHModel.Text = "plotView1";
            this.plotUHModel.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotUHModel.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotUHModel.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // Label101
            // 
            this.Label101.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label101.AutoSize = true;
            this.Label101.Location = new System.Drawing.Point(366, 25);
            this.Label101.Name = "Label101";
            this.Label101.Size = new System.Drawing.Size(122, 19);
            this.Label101.TabIndex = 203;
            this.Label101.Text = "Flow Sep. crit WS";
            // 
            // txtSepCritWS
            // 
            this.txtSepCritWS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSepCritWS.Location = new System.Drawing.Point(380, 47);
            this.txtSepCritWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSepCritWS.Name = "txtSepCritWS";
            this.txtSepCritWS.ReadOnly = true;
            this.txtSepCritWS.Size = new System.Drawing.Size(88, 25);
            this.txtSepCritWS.TabIndex = 202;
            // 
            // Label13
            // 
            this.Label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(376, 77);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(96, 19);
            this.Label13.TabIndex = 201;
            this.Label13.Text = "Flow Sep. crit";
            // 
            // txtSepCrit
            // 
            this.txtSepCrit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSepCrit.Location = new System.Drawing.Point(380, 97);
            this.txtSepCrit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSepCrit.Name = "txtSepCrit";
            this.txtSepCrit.ReadOnly = true;
            this.txtSepCrit.Size = new System.Drawing.Size(88, 25);
            this.txtSepCrit.TabIndex = 200;
            // 
            // Label100
            // 
            this.Label100.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label100.AutoSize = true;
            this.Label100.Location = new System.Drawing.Point(366, 134);
            this.Label100.Name = "Label100";
            this.Label100.Size = new System.Drawing.Size(117, 19);
            this.Label100.TabIndex = 199;
            this.Label100.Text = "DH plot to show";
            // 
            // cboDHplot
            // 
            this.cboDHplot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDHplot.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDHplot.FormattingEnabled = true;
            this.cboDHplot.Items.AddRange(new object[] {
            "Attached flow",
            "Separated flow"});
            this.cboDHplot.Location = new System.Drawing.Point(369, 158);
            this.cboDHplot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboDHplot.Name = "cboDHplot";
            this.cboDHplot.Size = new System.Drawing.Size(110, 26);
            this.cboDHplot.TabIndex = 198;
            this.cboDHplot.SelectedIndexChanged += new System.EventHandler(this.cboDHplot_SelectedIndexChanged_1);
            // 
            // plotDHModel
            // 
            this.plotDHModel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotDHModel.Location = new System.Drawing.Point(11, 10);
            this.plotDHModel.Name = "plotDHModel";
            this.plotDHModel.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDHModel.Size = new System.Drawing.Size(352, 170);
            this.plotDHModel.TabIndex = 287;
            this.plotDHModel.Text = "plotView1";
            this.plotDHModel.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDHModel.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDHModel.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // cboExpo_or_Stab
            // 
            this.cboExpo_or_Stab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboExpo_or_Stab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboExpo_or_Stab.FormattingEnabled = true;
            this.cboExpo_or_Stab.Items.AddRange(new object[] {
            "Exposure",
            "Roughness"});
            this.cboExpo_or_Stab.Location = new System.Drawing.Point(375, 51);
            this.cboExpo_or_Stab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExpo_or_Stab.Name = "cboExpo_or_Stab";
            this.cboExpo_or_Stab.Size = new System.Drawing.Size(101, 21);
            this.cboExpo_or_Stab.TabIndex = 188;
            this.cboExpo_or_Stab.SelectedIndexChanged += new System.EventHandler(this.cboExpo_or_Stab_SelectedIndexChanged_1);
            // 
            // Label82
            // 
            this.Label82.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label82.AutoSize = true;
            this.Label82.Location = new System.Drawing.Point(376, 31);
            this.Label82.Name = "Label82";
            this.Label82.Size = new System.Drawing.Size(95, 19);
            this.Label82.TabIndex = 187;
            this.Label82.Text = "Plot to show:";
            // 
            // Label59
            // 
            this.Label59.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label59.AutoSize = true;
            this.Label59.Location = new System.Drawing.Point(386, 82);
            this.Label59.Name = "Label59";
            this.Label59.Size = new System.Drawing.Size(80, 19);
            this.Label59.TabIndex = 186;
            this.Label59.Text = "UW critical";
            // 
            // txtUWCrit
            // 
            this.txtUWCrit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUWCrit.Location = new System.Drawing.Point(376, 101);
            this.txtUWCrit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUWCrit.Name = "txtUWCrit";
            this.txtUWCrit.ReadOnly = true;
            this.txtUWCrit.Size = new System.Drawing.Size(88, 25);
            this.txtUWCrit.TabIndex = 185;
            // 
            // Label58
            // 
            this.Label58.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label58.AutoSize = true;
            this.Label58.Location = new System.Drawing.Point(369, 138);
            this.Label58.Name = "Label58";
            this.Label58.Size = new System.Drawing.Size(117, 19);
            this.Label58.TabIndex = 184;
            this.Label58.Text = "UH plot to show";
            // 
            // cboUphill_to_show
            // 
            this.cboUphill_to_show.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUphill_to_show.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUphill_to_show.FormattingEnabled = true;
            this.cboUphill_to_show.Items.AddRange(new object[] {
            "UW > UW crit",
            "UW < UW crit"});
            this.cboUphill_to_show.Location = new System.Drawing.Point(373, 158);
            this.cboUphill_to_show.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboUphill_to_show.Name = "cboUphill_to_show";
            this.cboUphill_to_show.Size = new System.Drawing.Size(110, 21);
            this.cboUphill_to_show.TabIndex = 183;
            this.cboUphill_to_show.SelectedIndexChanged += new System.EventHandler(this.cboUphill_to_show_SelectedIndexChanged_1);
            // 
            // spltAdvanced
            // 
            this.spltAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spltAdvanced.Location = new System.Drawing.Point(474, 120);
            this.spltAdvanced.Name = "spltAdvanced";
            // 
            // spltAdvanced.Panel1
            // 
            this.spltAdvanced.Panel1.Controls.Add(this.lblTurbineTSNoAdvanced);
            this.spltAdvanced.Panel1.Controls.Add(this.lstPathNodes);
            // 
            // spltAdvanced.Panel2
            // 
            this.spltAdvanced.Panel2.Controls.Add(this.spltAdvUWDW);
            this.spltAdvanced.Size = new System.Drawing.Size(1011, 184);
            this.spltAdvanced.SplitterDistance = 450;
            this.spltAdvanced.TabIndex = 291;
            // 
            // lblTurbineTSNoAdvanced
            // 
            this.lblTurbineTSNoAdvanced.AutoSize = true;
            this.lblTurbineTSNoAdvanced.BackColor = System.Drawing.Color.White;
            this.lblTurbineTSNoAdvanced.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurbineTSNoAdvanced.Location = new System.Drawing.Point(20, 92);
            this.lblTurbineTSNoAdvanced.Name = "lblTurbineTSNoAdvanced";
            this.lblTurbineTSNoAdvanced.Size = new System.Drawing.Size(45, 18);
            this.lblTurbineTSNoAdvanced.TabIndex = 215;
            this.lblTurbineTSNoAdvanced.Text = "label5";
            // 
            // lstPathNodes
            // 
            this.lstPathNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lstPathNodes.Location = new System.Drawing.Point(12, 12);
            this.lstPathNodes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstPathNodes.Name = "lstPathNodes";
            this.lstPathNodes.Size = new System.Drawing.Size(429, 162);
            this.lstPathNodes.TabIndex = 214;
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
            // spltAdvUWDW
            // 
            this.spltAdvUWDW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spltAdvUWDW.Location = new System.Drawing.Point(5, 3);
            this.spltAdvUWDW.Name = "spltAdvUWDW";
            // 
            // spltAdvUWDW.Panel1
            // 
            this.spltAdvUWDW.Panel1.Controls.Add(this.Label8);
            this.spltAdvUWDW.Panel1.Controls.Add(this.lstPathNodes_UW);
            // 
            // spltAdvUWDW.Panel2
            // 
            this.spltAdvUWDW.Panel2.Controls.Add(this.Label39);
            this.spltAdvUWDW.Panel2.Controls.Add(this.lstPathNodes_DW);
            this.spltAdvUWDW.Size = new System.Drawing.Size(549, 172);
            this.spltAdvUWDW.SplitterDistance = 273;
            this.spltAdvUWDW.TabIndex = 205;
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label8.AutoSize = true;
            this.Label8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(53, 2);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(180, 19);
            this.Label8.TabIndex = 204;
            this.Label8.Text = "Upwind Flow Estimates";
            // 
            // lstPathNodes_UW
            // 
            this.lstPathNodes_UW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPathNodes_UW.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader14,
            this.ColumnHeader121,
            this.ColumnHeader115,
            this.ColumnHeader120,
            this.ColumnHeader111});
            this.lstPathNodes_UW.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPathNodes_UW.FullRowSelect = true;
            this.lstPathNodes_UW.HideSelection = false;
            this.lstPathNodes_UW.Location = new System.Drawing.Point(7, 25);
            this.lstPathNodes_UW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstPathNodes_UW.Name = "lstPathNodes_UW";
            this.lstPathNodes_UW.Size = new System.Drawing.Size(263, 143);
            this.lstPathNodes_UW.TabIndex = 203;
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
            // Label39
            // 
            this.Label39.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label39.AutoSize = true;
            this.Label39.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label39.Location = new System.Drawing.Point(36, 4);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(203, 19);
            this.Label39.TabIndex = 205;
            this.Label39.Text = "Downwind Flow Estimates";
            // 
            // lstPathNodes_DW
            // 
            this.lstPathNodes_DW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPathNodes_DW.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader15,
            this.ColumnHeader110,
            this.ColumnHeader108,
            this.ColumnHeader109,
            this.ColumnHeader112});
            this.lstPathNodes_DW.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPathNodes_DW.FullRowSelect = true;
            this.lstPathNodes_DW.HideSelection = false;
            this.lstPathNodes_DW.Location = new System.Drawing.Point(6, 27);
            this.lstPathNodes_DW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstPathNodes_DW.Name = "lstPathNodes_DW";
            this.lstPathNodes_DW.Size = new System.Drawing.Size(263, 141);
            this.lstPathNodes_DW.TabIndex = 204;
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
            // btnExportRMS_Errors
            // 
            this.btnExportRMS_Errors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportRMS_Errors.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportRMS_Errors.Location = new System.Drawing.Point(1346, 59);
            this.btnExportRMS_Errors.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportRMS_Errors.Name = "btnExportRMS_Errors";
            this.btnExportRMS_Errors.Size = new System.Drawing.Size(114, 45);
            this.btnExportRMS_Errors.TabIndex = 290;
            this.btnExportRMS_Errors.Text = "Export All Model RMS Errors";
            this.btnExportRMS_Errors.UseVisualStyleBackColor = true;
            this.btnExportRMS_Errors.Click += new System.EventHandler(this.btnExportRMS_Errors_Click);
            // 
            // txtImportedModel
            // 
            this.txtImportedModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImportedModel.Location = new System.Drawing.Point(741, 94);
            this.txtImportedModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtImportedModel.Name = "txtImportedModel";
            this.txtImportedModel.ReadOnly = true;
            this.txtImportedModel.Size = new System.Drawing.Size(180, 25);
            this.txtImportedModel.TabIndex = 289;
            // 
            // btnImportModel
            // 
            this.btnImportModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportModel.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnImportModel.Location = new System.Drawing.Point(1232, 10);
            this.btnImportModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportModel.Name = "btnImportModel";
            this.btnImportModel.Size = new System.Drawing.Size(114, 45);
            this.btnImportModel.TabIndex = 288;
            this.btnImportModel.Text = "Import Model Parameters";
            this.btnImportModel.UseVisualStyleBackColor = true;
            this.btnImportModel.Click += new System.EventHandler(this.btnImportModel_Click_1);
            // 
            // btnExportModel
            // 
            this.btnExportModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportModel.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportModel.Location = new System.Drawing.Point(1348, 10);
            this.btnExportModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportModel.Name = "btnExportModel";
            this.btnExportModel.Size = new System.Drawing.Size(114, 45);
            this.btnExportModel.TabIndex = 287;
            this.btnExportModel.Text = "Export Model Parameters";
            this.btnExportModel.UseVisualStyleBackColor = true;
            this.btnExportModel.Click += new System.EventHandler(this.btnExportModel_Click);
            // 
            // plotAdvTopo
            // 
            this.plotAdvTopo.Location = new System.Drawing.Point(25, 41);
            this.plotAdvTopo.Name = "plotAdvTopo";
            this.plotAdvTopo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotAdvTopo.Size = new System.Drawing.Size(430, 356);
            this.plotAdvTopo.TabIndex = 283;
            this.plotAdvTopo.Text = "plotView1";
            this.plotAdvTopo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotAdvTopo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotAdvTopo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // txtisMCPdAdv
            // 
            this.txtisMCPdAdv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtisMCPdAdv.Location = new System.Drawing.Point(741, 39);
            this.txtisMCPdAdv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtisMCPdAdv.Name = "txtisMCPdAdv";
            this.txtisMCPdAdv.ReadOnly = true;
            this.txtisMCPdAdv.Size = new System.Drawing.Size(180, 25);
            this.txtisMCPdAdv.TabIndex = 212;
            // 
            // cboSeasonAdvanced
            // 
            this.cboSeasonAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSeasonAdvanced.FormattingEnabled = true;
            this.cboSeasonAdvanced.Location = new System.Drawing.Point(937, 51);
            this.cboSeasonAdvanced.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSeasonAdvanced.Name = "cboSeasonAdvanced";
            this.cboSeasonAdvanced.Size = new System.Drawing.Size(91, 26);
            this.cboSeasonAdvanced.TabIndex = 211;
            this.cboSeasonAdvanced.Text = "All Seasons";
            this.cboSeasonAdvanced.SelectedIndexChanged += new System.EventHandler(this.cboSeasonAdvanced_SelectedIndexChanged);
            // 
            // cboTODAdvanced
            // 
            this.cboTODAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTODAdvanced.FormattingEnabled = true;
            this.cboTODAdvanced.Location = new System.Drawing.Point(937, 17);
            this.cboTODAdvanced.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTODAdvanced.Name = "cboTODAdvanced";
            this.cboTODAdvanced.Size = new System.Drawing.Size(91, 26);
            this.cboTODAdvanced.TabIndex = 209;
            this.cboTODAdvanced.Text = "All Hours";
            this.cboTODAdvanced.SelectedIndexChanged += new System.EventHandler(this.cboTODAdvanced_SelectedIndexChanged);
            // 
            // txtAdv_FlowSep_Used
            // 
            this.txtAdv_FlowSep_Used.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdv_FlowSep_Used.Location = new System.Drawing.Point(741, 67);
            this.txtAdv_FlowSep_Used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAdv_FlowSep_Used.Name = "txtAdv_FlowSep_Used";
            this.txtAdv_FlowSep_Used.ReadOnly = true;
            this.txtAdv_FlowSep_Used.Size = new System.Drawing.Size(180, 25);
            this.txtAdv_FlowSep_Used.TabIndex = 198;
            // 
            // txtAdv_LC_used
            // 
            this.txtAdv_LC_used.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdv_LC_used.Location = new System.Drawing.Point(741, 11);
            this.txtAdv_LC_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAdv_LC_used.Name = "txtAdv_LC_used";
            this.txtAdv_LC_used.ReadOnly = true;
            this.txtAdv_LC_used.Size = new System.Drawing.Size(180, 25);
            this.txtAdv_LC_used.TabIndex = 183;
            // 
            // cboAdvancedWD
            // 
            this.cboAdvancedWD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAdvancedWD.FormattingEnabled = true;
            this.cboAdvancedWD.Location = new System.Drawing.Point(1097, 51);
            this.cboAdvancedWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboAdvancedWD.Name = "cboAdvancedWD";
            this.cboAdvancedWD.Size = new System.Drawing.Size(81, 26);
            this.cboAdvancedWD.TabIndex = 168;
            this.cboAdvancedWD.SelectedIndexChanged += new System.EventHandler(this.cboWDsector_SelectedIndexChanged);
            // 
            // Label56
            // 
            this.Label56.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label56.AutoSize = true;
            this.Label56.Location = new System.Drawing.Point(1059, 53);
            this.Label56.Name = "Label56";
            this.Label56.Size = new System.Drawing.Size(38, 19);
            this.Label56.TabIndex = 167;
            this.Label56.Text = "WD:";
            // 
            // btnExportCrossPreds
            // 
            this.btnExportCrossPreds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportCrossPreds.Location = new System.Drawing.Point(295, 503);
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
            this.Label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(1041, 19);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(56, 19);
            this.Label14.TabIndex = 150;
            this.Label14.Text = "Radius:";
            // 
            // cboAdvancedRad
            // 
            this.cboAdvancedRad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAdvancedRad.FormattingEnabled = true;
            this.cboAdvancedRad.Location = new System.Drawing.Point(1097, 17);
            this.cboAdvancedRad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboAdvancedRad.Name = "cboAdvancedRad";
            this.cboAdvancedRad.Size = new System.Drawing.Size(115, 26);
            this.cboAdvancedRad.TabIndex = 149;
            this.cboAdvancedRad.SelectedIndexChanged += new System.EventHandler(this.cboRadDisplay_SelectedIndexChanged);
            // 
            // chkAllTurbsStep
            // 
            this.chkAllTurbsStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAllTurbsStep.AutoSize = true;
            this.chkAllTurbsStep.Checked = true;
            this.chkAllTurbsStep.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllTurbsStep.Location = new System.Drawing.Point(346, 407);
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
            this.Label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label38.AutoSize = true;
            this.Label38.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label38.Location = new System.Drawing.Point(243, 407);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(97, 18);
            this.Label38.TabIndex = 123;
            this.Label38.Text = "Turbine Sites";
            // 
            // chkTurbLabelStep
            // 
            this.chkTurbLabelStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTurbLabelStep.CheckOnClick = true;
            this.chkTurbLabelStep.FormattingEnabled = true;
            this.chkTurbLabelStep.Location = new System.Drawing.Point(246, 431);
            this.chkTurbLabelStep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbLabelStep.Name = "chkTurbLabelStep";
            this.chkTurbLabelStep.Size = new System.Drawing.Size(217, 64);
            this.chkTurbLabelStep.TabIndex = 122;
            this.chkTurbLabelStep.SelectedIndexChanged += new System.EventHandler(this.chkTurbLabelStep_SelectedIndexChanged);
            // 
            // chkAllMetLabelsStep
            // 
            this.chkAllMetLabelsStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAllMetLabelsStep.AutoSize = true;
            this.chkAllMetLabelsStep.Checked = true;
            this.chkAllMetLabelsStep.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllMetLabelsStep.Location = new System.Drawing.Point(103, 407);
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
            this.Label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label37.AutoSize = true;
            this.Label37.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label37.Location = new System.Drawing.Point(19, 407);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(72, 18);
            this.Label37.TabIndex = 120;
            this.Label37.Text = "Met Sites";
            // 
            // chkMetlabelsStep
            // 
            this.chkMetlabelsStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkMetlabelsStep.CheckOnClick = true;
            this.chkMetlabelsStep.FormattingEnabled = true;
            this.chkMetlabelsStep.Location = new System.Drawing.Point(17, 431);
            this.chkMetlabelsStep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetlabelsStep.Name = "chkMetlabelsStep";
            this.chkMetlabelsStep.Size = new System.Drawing.Size(217, 64);
            this.chkMetlabelsStep.TabIndex = 119;
            this.chkMetlabelsStep.SelectedIndexChanged += new System.EventHandler(this.chkMetlabelsStep_SelectedIndexChanged);
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.Location = new System.Drawing.Point(530, 20);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(75, 19);
            this.Label33.TabIndex = 107;
            this.Label33.Text = "From Met:";
            // 
            // cboStartMet
            // 
            this.cboStartMet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStartMet.FormattingEnabled = true;
            this.cboStartMet.Location = new System.Drawing.Point(610, 16);
            this.cboStartMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboStartMet.Name = "cboStartMet";
            this.cboStartMet.Size = new System.Drawing.Size(125, 26);
            this.cboStartMet.TabIndex = 106;
            this.cboStartMet.SelectedIndexChanged += new System.EventHandler(this.cboStartMet_SelectedIndexChanged);
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.Location = new System.Drawing.Point(477, 50);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(130, 19);
            this.Label32.TabIndex = 105;
            this.Label32.Text = "To Met or Turbine:";
            // 
            // cboEndMet
            // 
            this.cboEndMet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEndMet.FormattingEnabled = true;
            this.cboEndMet.Location = new System.Drawing.Point(610, 47);
            this.cboEndMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboEndMet.Name = "cboEndMet";
            this.cboEndMet.Size = new System.Drawing.Size(125, 26);
            this.cboEndMet.TabIndex = 104;
            this.cboEndMet.SelectedIndexChanged += new System.EventHandler(this.cboEndMet_SelectedIndexChanged);
            // 
            // btnExportStepwise
            // 
            this.btnExportStepwise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportStepwise.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.btnExportStepwise.Location = new System.Drawing.Point(1232, 59);
            this.btnExportStepwise.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportStepwise.Name = "btnExportStepwise";
            this.btnExportStepwise.Size = new System.Drawing.Size(114, 45);
            this.btnExportStepwise.TabIndex = 101;
            this.btnExportStepwise.Text = "Export Nodes and WS Estimates";
            this.btnExportStepwise.UseVisualStyleBackColor = true;
            this.btnExportStepwise.Click += new System.EventHandler(this.btnExportStepwise_Click);
            // 
            // lstModCrossPred
            // 
            this.lstModCrossPred.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lstModCrossPred.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader16,
            this.ColumnHeader26,
            this.ColumnHeader30,
            this.ColumnHeader31});
            this.lstModCrossPred.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstModCrossPred.FullRowSelect = true;
            this.lstModCrossPred.HideSelection = false;
            this.lstModCrossPred.Location = new System.Drawing.Point(22, 559);
            this.lstModCrossPred.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstModCrossPred.Name = "lstModCrossPred";
            this.lstModCrossPred.Size = new System.Drawing.Size(446, 138);
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
            this.Label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label11.AutoSize = true;
            this.Label11.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(30, 510);
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
            this.pgeSuitability.AutoScroll = true;
            this.pgeSuitability.Controls.Add(this.txtMaxShadowDist);
            this.pgeSuitability.Controls.Add(this.label21);
            this.pgeSuitability.Controls.Add(this.btnExportSiteSuitMap);
            this.pgeSuitability.Controls.Add(this.btnZoneFileFormatHelp);
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
            this.pgeSuitability.Size = new System.Drawing.Size(1488, 712);
            this.pgeSuitability.TabIndex = 18;
            this.pgeSuitability.Text = "Site Suitability";
            this.pgeSuitability.UseVisualStyleBackColor = true;
            // 
            // btnExportSiteSuitMap
            // 
            this.btnExportSiteSuitMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportSiteSuitMap.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportSiteSuitMap.Location = new System.Drawing.Point(772, 677);
            this.btnExportSiteSuitMap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportSiteSuitMap.Name = "btnExportSiteSuitMap";
            this.btnExportSiteSuitMap.Size = new System.Drawing.Size(92, 30);
            this.btnExportSiteSuitMap.TabIndex = 287;
            this.btnExportSiteSuitMap.Text = "Export Map";
            this.btnExportSiteSuitMap.UseVisualStyleBackColor = true;
            this.btnExportSiteSuitMap.Click += new System.EventHandler(this.btnExportSiteSuitMap_Click);
            // 
            // btnZoneFileFormatHelp
            // 
            this.btnZoneFileFormatHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZoneFileFormatHelp.Location = new System.Drawing.Point(152, 643);
            this.btnZoneFileFormatHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnZoneFileFormatHelp.Name = "btnZoneFileFormatHelp";
            this.btnZoneFileFormatHelp.Size = new System.Drawing.Size(34, 31);
            this.btnZoneFileFormatHelp.TabIndex = 286;
            this.btnZoneFileFormatHelp.Text = "?";
            this.btnZoneFileFormatHelp.UseVisualStyleBackColor = true;
            this.btnZoneFileFormatHelp.Click += new System.EventHandler(this.button1_Click);
            // 
            // plotIceVsDist
            // 
            this.plotIceVsDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.plotIceVsDist.Location = new System.Drawing.Point(902, 438);
            this.plotIceVsDist.Name = "plotIceVsDist";
            this.plotIceVsDist.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotIceVsDist.Size = new System.Drawing.Size(571, 253);
            this.plotIceVsDist.TabIndex = 285;
            this.plotIceVsDist.Text = "plotView1";
            this.plotIceVsDist.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotIceVsDist.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotIceVsDist.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotIceVsDist.Click += new System.EventHandler(this.plotIceVsDist_Click);
            // 
            // plotIceShadowSound
            // 
            this.plotIceShadowSound.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotIceShadowSound.Location = new System.Drawing.Point(344, 171);
            this.plotIceShadowSound.Name = "plotIceShadowSound";
            this.plotIceShadowSound.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotIceShadowSound.Size = new System.Drawing.Size(520, 500);
            this.plotIceShadowSound.TabIndex = 284;
            this.plotIceShadowSound.Text = "plotView1";
            this.plotIceShadowSound.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotIceShadowSound.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotIceShadowSound.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotIceShadowSound.Click += new System.EventHandler(this.plotIceShadowSound_Click);
            // 
            // lblIceDistOrHisto
            // 
            this.lblIceDistOrHisto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIceDistOrHisto.AutoSize = true;
            this.lblIceDistOrHisto.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIceDistOrHisto.Location = new System.Drawing.Point(1056, 404);
            this.lblIceDistOrHisto.Name = "lblIceDistOrHisto";
            this.lblIceDistOrHisto.Size = new System.Drawing.Size(75, 18);
            this.lblIceDistOrHisto.TabIndex = 278;
            this.lblIceDistOrHisto.Text = "Select Plot :";
            this.lblIceDistOrHisto.Click += new System.EventHandler(this.lblIceDistOrHisto_Click);
            // 
            // cboIceDistORIceHisto
            // 
            this.cboIceDistORIceHisto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboIceDistORIceHisto.FormattingEnabled = true;
            this.cboIceDistORIceHisto.Items.AddRange(new object[] {
            "Ice Hit vs. Distance",
            "Yearly Ice Hit Histogram"});
            this.cboIceDistORIceHisto.Location = new System.Drawing.Point(1137, 402);
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
            this.label91.Location = new System.Drawing.Point(384, 101);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(98, 18);
            this.label91.TabIndex = 276;
            this.label91.Text = "# Ice Days / Yr :";
            // 
            // txtNumIceThrowsPerDay
            // 
            this.txtNumIceThrowsPerDay.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumIceThrowsPerDay.Location = new System.Drawing.Point(485, 73);
            this.txtNumIceThrowsPerDay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNumIceThrowsPerDay.Name = "txtNumIceThrowsPerDay";
            this.txtNumIceThrowsPerDay.Size = new System.Drawing.Size(44, 25);
            this.txtNumIceThrowsPerDay.TabIndex = 275;
            this.txtNumIceThrowsPerDay.TextChanged += new System.EventHandler(this.txtNumIceDays_TextChanged);
            // 
            // txtMaxFlickerHours
            // 
            this.txtMaxFlickerHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxFlickerHours.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxFlickerHours.Location = new System.Drawing.Point(1387, 402);
            this.txtMaxFlickerHours.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaxFlickerHours.Name = "txtMaxFlickerHours";
            this.txtMaxFlickerHours.ReadOnly = true;
            this.txtMaxFlickerHours.Size = new System.Drawing.Size(65, 25);
            this.txtMaxFlickerHours.TabIndex = 274;
            this.txtMaxFlickerHours.TextChanged += new System.EventHandler(this.txtMaxFlickerHours_TextChanged);
            // 
            // dateMaxFlicker
            // 
            this.dateMaxFlicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateMaxFlicker.CustomFormat = "MM/dd/yy";
            this.dateMaxFlicker.Enabled = false;
            this.dateMaxFlicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMaxFlicker.Location = new System.Drawing.Point(1373, 370);
            this.dateMaxFlicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateMaxFlicker.Name = "dateMaxFlicker";
            this.dateMaxFlicker.Size = new System.Drawing.Size(108, 25);
            this.dateMaxFlicker.TabIndex = 273;
            this.dateMaxFlicker.ValueChanged += new System.EventHandler(this.dateMaxFlicker_ValueChanged);
            // 
            // lblMaxFlickerHours
            // 
            this.lblMaxFlickerHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxFlickerHours.AutoSize = true;
            this.lblMaxFlickerHours.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxFlickerHours.Location = new System.Drawing.Point(1255, 405);
            this.lblMaxFlickerHours.Name = "lblMaxFlickerHours";
            this.lblMaxFlickerHours.Size = new System.Drawing.Size(108, 18);
            this.lblMaxFlickerHours.TabIndex = 272;
            this.lblMaxFlickerHours.Text = "# of Flicker Mins:";
            this.lblMaxFlickerHours.Click += new System.EventHandler(this.lblMaxFlickerHours_Click);
            // 
            // lblMaxFlickerDay
            // 
            this.lblMaxFlickerDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxFlickerDay.AutoSize = true;
            this.lblMaxFlickerDay.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxFlickerDay.Location = new System.Drawing.Point(1260, 373);
            this.lblMaxFlickerDay.Name = "lblMaxFlickerDay";
            this.lblMaxFlickerDay.Size = new System.Drawing.Size(113, 18);
            this.lblMaxFlickerDay.TabIndex = 271;
            this.lblMaxFlickerDay.Text = "Max. Flicker Day :";
            this.lblMaxFlickerDay.Click += new System.EventHandler(this.lblMaxFlickerDay_Click);
            // 
            // txtTurbineNoise
            // 
            this.txtTurbineNoise.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTurbineNoise.Location = new System.Drawing.Point(787, 72);
            this.txtTurbineNoise.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTurbineNoise.Name = "txtTurbineNoise";
            this.txtTurbineNoise.Size = new System.Drawing.Size(44, 25);
            this.txtTurbineNoise.TabIndex = 270;
            this.txtTurbineNoise.TextChanged += new System.EventHandler(this.txtTurbineNoise_TextChanged);
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(701, 67);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(84, 36);
            this.label90.TabIndex = 269;
            this.label90.Text = "Turbine \r\nNoise (dBA) :";
            // 
            // txtNumIceDays
            // 
            this.txtNumIceDays.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumIceDays.Location = new System.Drawing.Point(485, 101);
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
            this.label89.Location = new System.Drawing.Point(384, 74);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(105, 18);
            this.label89.TabIndex = 267;
            this.label89.Text = "# Throws / Day :";
            // 
            // btnExportIceVsDist
            // 
            this.btnExportIceVsDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportIceVsDist.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportIceVsDist.Location = new System.Drawing.Point(1146, 301);
            this.btnExportIceVsDist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportIceVsDist.Name = "btnExportIceVsDist";
            this.btnExportIceVsDist.Size = new System.Drawing.Size(94, 50);
            this.btnExportIceVsDist.TabIndex = 266;
            this.btnExportIceVsDist.Text = "Export Throw vs. Dist.";
            this.btnExportIceVsDist.UseVisualStyleBackColor = true;
            this.btnExportIceVsDist.Click += new System.EventHandler(this.btnExportIceVsDist_Click);
            // 
            // btnExportSoundSummary
            // 
            this.btnExportSoundSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportSoundSummary.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportSoundSummary.Location = new System.Drawing.Point(1401, 301);
            this.btnExportSoundSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportSoundSummary.Name = "btnExportSoundSummary";
            this.btnExportSoundSummary.Size = new System.Drawing.Size(80, 50);
            this.btnExportSoundSummary.TabIndex = 264;
            this.btnExportSoundSummary.Text = "Export Summary";
            this.btnExportSoundSummary.UseVisualStyleBackColor = true;
            this.btnExportSoundSummary.Click += new System.EventHandler(this.btnExportSoundSummary_Click);
            // 
            // btnExportShadowFlicker
            // 
            this.btnExportShadowFlicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportShadowFlicker.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportShadowFlicker.Location = new System.Drawing.Point(945, 301);
            this.btnExportShadowFlicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportShadowFlicker.Name = "btnExportShadowFlicker";
            this.btnExportShadowFlicker.Size = new System.Drawing.Size(74, 50);
            this.btnExportShadowFlicker.TabIndex = 263;
            this.btnExportShadowFlicker.Text = "Export Summary";
            this.btnExportShadowFlicker.UseVisualStyleBackColor = true;
            this.btnExportShadowFlicker.Click += new System.EventHandler(this.btnExportShadowFlicker_Click);
            // 
            // btnExportIceSummary
            // 
            this.btnExportIceSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportIceSummary.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportIceSummary.Location = new System.Drawing.Point(1245, 301);
            this.btnExportIceSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportIceSummary.Name = "btnExportIceSummary";
            this.btnExportIceSummary.Size = new System.Drawing.Size(80, 50);
            this.btnExportIceSummary.TabIndex = 262;
            this.btnExportIceSummary.Text = "Export Summary";
            this.btnExportIceSummary.UseVisualStyleBackColor = true;
            this.btnExportIceSummary.Click += new System.EventHandler(this.btnExportIceSummary_Click);
            // 
            // label176
            // 
            this.label176.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label176.AutoSize = true;
            this.label176.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label176.Location = new System.Drawing.Point(1320, 15);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(156, 19);
            this.label176.TabIndex = 261;
            this.label176.Text = "Sound Levels (dBA)";
            // 
            // lstZoneSound
            // 
            this.lstZoneSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lstZoneSound.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader158,
            this.columnHeader159});
            this.lstZoneSound.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstZoneSound.GridLines = true;
            this.lstZoneSound.HideSelection = false;
            this.lstZoneSound.Location = new System.Drawing.Point(1333, 39);
            this.lstZoneSound.Margin = new System.Windows.Forms.Padding(2);
            this.lstZoneSound.Name = "lstZoneSound";
            this.lstZoneSound.Size = new System.Drawing.Size(147, 256);
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
            this.label175.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label175.AutoSize = true;
            this.label175.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label175.Location = new System.Drawing.Point(1027, 14);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(62, 19);
            this.label175.TabIndex = 259;
            this.label175.Text = "Ice Hits";
            // 
            // lblShadowByMonthOrIceByDist
            // 
            this.lblShadowByMonthOrIceByDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShadowByMonthOrIceByDist.AutoSize = true;
            this.lblShadowByMonthOrIceByDist.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShadowByMonthOrIceByDist.Location = new System.Drawing.Point(917, 370);
            this.lblShadowByMonthOrIceByDist.Name = "lblShadowByMonthOrIceByDist";
            this.lblShadowByMonthOrIceByDist.Size = new System.Drawing.Size(325, 19);
            this.lblShadowByMonthOrIceByDist.TabIndex = 258;
            this.lblShadowByMonthOrIceByDist.Text = "Hours of Shadow Flicker by Month / Hour :";
            this.lblShadowByMonthOrIceByDist.Click += new System.EventHandler(this.lblShadowByMonthOrIceByDist_Click);
            // 
            // lstZoneIceHits
            // 
            this.lstZoneIceHits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lstZoneIceHits.Location = new System.Drawing.Point(1027, 39);
            this.lstZoneIceHits.Margin = new System.Windows.Forms.Padding(2);
            this.lstZoneIceHits.Name = "lstZoneIceHits";
            this.lstZoneIceHits.Size = new System.Drawing.Size(298, 256);
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
            this.label173.Location = new System.Drawing.Point(769, 113);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(41, 18);
            this.label173.TabIndex = 256;
            this.label173.Text = "Year :";
            // 
            // cboIcingYear
            // 
            this.cboIcingYear.FormattingEnabled = true;
            this.cboIcingYear.Location = new System.Drawing.Point(810, 109);
            this.cboIcingYear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboIcingYear.Name = "cboIcingYear";
            this.cboIcingYear.Size = new System.Drawing.Size(54, 26);
            this.cboIcingYear.TabIndex = 255;
            this.cboIcingYear.Text = "1";
            this.cboIcingYear.SelectedIndexChanged += new System.EventHandler(this.cboIcingYear_SelectedIndexChanged);
            // 
            // lstShadowZoneSummary
            // 
            this.lstShadowZoneSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lstShadowZoneSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader152,
            this.columnHeader153});
            this.lstShadowZoneSummary.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstShadowZoneSummary.GridLines = true;
            this.lstShadowZoneSummary.HideSelection = false;
            this.lstShadowZoneSummary.Location = new System.Drawing.Point(872, 39);
            this.lstShadowZoneSummary.Margin = new System.Windows.Forms.Padding(2);
            this.lstShadowZoneSummary.Name = "lstShadowZoneSummary";
            this.lstShadowZoneSummary.Size = new System.Drawing.Size(147, 256);
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
            this.txtTotalShadow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalShadow.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalShadow.Location = new System.Drawing.Point(1175, 402);
            this.txtTotalShadow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalShadow.Name = "txtTotalShadow";
            this.txtTotalShadow.ReadOnly = true;
            this.txtTotalShadow.Size = new System.Drawing.Size(65, 25);
            this.txtTotalShadow.TabIndex = 253;
            this.txtTotalShadow.TextChanged += new System.EventHandler(this.txtTotalShadow_TextChanged);
            // 
            // lblTotalHoursPerYear
            // 
            this.lblTotalHoursPerYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalHoursPerYear.AutoSize = true;
            this.lblTotalHoursPerYear.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHoursPerYear.Location = new System.Drawing.Point(1037, 405);
            this.lblTotalHoursPerYear.Name = "lblTotalHoursPerYear";
            this.lblTotalHoursPerYear.Size = new System.Drawing.Size(138, 18);
            this.lblTotalHoursPerYear.TabIndex = 224;
            this.lblTotalHoursPerYear.Text = "Total Hours Per Year :";
            this.lblTotalHoursPerYear.Click += new System.EventHandler(this.lblTotalHoursPerYear_Click);
            // 
            // lblShadowOrIceByDist
            // 
            this.lblShadowOrIceByDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShadowOrIceByDist.AutoSize = true;
            this.lblShadowOrIceByDist.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShadowOrIceByDist.Location = new System.Drawing.Point(870, 16);
            this.lblShadowOrIceByDist.Name = "lblShadowOrIceByDist";
            this.lblShadowOrIceByDist.Size = new System.Drawing.Size(126, 19);
            this.lblShadowOrIceByDist.TabIndex = 223;
            this.lblShadowOrIceByDist.Text = "Shadow Flicker";
            // 
            // lblZoneOrDirection
            // 
            this.lblZoneOrDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblZoneOrDirection.AutoSize = true;
            this.lblZoneOrDirection.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoneOrDirection.Location = new System.Drawing.Point(902, 408);
            this.lblZoneOrDirection.Name = "lblZoneOrDirection";
            this.lblZoneOrDirection.Size = new System.Drawing.Size(44, 18);
            this.lblZoneOrDirection.TabIndex = 222;
            this.lblZoneOrDirection.Text = "Zone :";
            this.lblZoneOrDirection.Click += new System.EventHandler(this.lblZoneOrDirection_Click);
            // 
            // cboZoneList
            // 
            this.cboZoneList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboZoneList.FormattingEnabled = true;
            this.cboZoneList.Location = new System.Drawing.Point(945, 402);
            this.cboZoneList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboZoneList.Name = "cboZoneList";
            this.cboZoneList.Size = new System.Drawing.Size(74, 26);
            this.cboZoneList.TabIndex = 221;
            this.cboZoneList.SelectedIndexChanged += new System.EventHandler(this.cboZoneList_SelectedIndexChanged);
            // 
            // lstShadow12x24
            // 
            this.lstShadow12x24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lstShadow12x24.Location = new System.Drawing.Point(918, 435);
            this.lstShadow12x24.Margin = new System.Windows.Forms.Padding(2);
            this.lstShadow12x24.Name = "lstShadow12x24";
            this.lstShadow12x24.Size = new System.Drawing.Size(555, 260);
            this.lstShadow12x24.TabIndex = 219;
            this.lstShadow12x24.UseCompatibleStateImageBehavior = false;
            this.lstShadow12x24.View = System.Windows.Forms.View.Details;
            this.lstShadow12x24.SelectedIndexChanged += new System.EventHandler(this.lstShadow12x24_SelectedIndexChanged);
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
            this.label169.Location = new System.Drawing.Point(638, 112);
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
            this.cboSiteSuitHour.Location = new System.Drawing.Point(687, 109);
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
            this.label165.Location = new System.Drawing.Point(629, 145);
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
            this.cboSiteSuitMonth.Location = new System.Drawing.Point(687, 138);
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
            this.btnSiteSuitImportCRV.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnSiteSuitImportCRV.Location = new System.Drawing.Point(223, 49);
            this.btnSiteSuitImportCRV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSiteSuitImportCRV.Name = "btnSiteSuitImportCRV";
            this.btnSiteSuitImportCRV.Size = new System.Drawing.Size(140, 32);
            this.btnSiteSuitImportCRV.TabIndex = 214;
            this.btnSiteSuitImportCRV.Text = "Import Power Curve";
            this.btnSiteSuitImportCRV.UseVisualStyleBackColor = false;
            this.btnSiteSuitImportCRV.Click += new System.EventHandler(this.btnSiteSuitImportCRV_Click);
            // 
            // btnDelZones
            // 
            this.btnDelZones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelZones.Location = new System.Drawing.Point(199, 640);
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
            this.label164.Location = new System.Drawing.Point(342, 138);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(51, 18);
            this.label164.TabIndex = 200;
            this.label164.Text = "Model :";
            // 
            // cboSiteSuitabilitySelectPlot
            // 
            this.cboSiteSuitabilitySelectPlot.FormattingEnabled = true;
            this.cboSiteSuitabilitySelectPlot.Location = new System.Drawing.Point(393, 134);
            this.cboSiteSuitabilitySelectPlot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSiteSuitabilitySelectPlot.Name = "cboSiteSuitabilitySelectPlot";
            this.cboSiteSuitabilitySelectPlot.Size = new System.Drawing.Size(226, 26);
            this.cboSiteSuitabilitySelectPlot.TabIndex = 199;
            this.cboSiteSuitabilitySelectPlot.SelectedIndexChanged += new System.EventHandler(this.cboSiteSuitabilitySelectPlot_SelectedIndexChanged);
            // 
            // lstZones
            // 
            this.lstZones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstZones.CheckBoxes = true;
            this.lstZones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader133,
            this.columnHeader134,
            this.columnHeader135,
            this.columnHeader136,
            this.columnHeader137});
            this.lstZones.FullRowSelect = true;
            this.lstZones.HideSelection = false;
            this.lstZones.Location = new System.Drawing.Point(14, 144);
            this.lstZones.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstZones.Name = "lstZones";
            this.lstZones.Size = new System.Drawing.Size(307, 476);
            this.lstZones.TabIndex = 167;
            this.lstZones.UseCompatibleStateImageBehavior = false;
            this.lstZones.View = System.Windows.Forms.View.Details;
            this.lstZones.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstZones_ItemCheckChanged);
            this.lstZones.SelectedIndexChanged += new System.EventHandler(this.lstZones_SelectedIndexChanged);
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
            this.btnRunShadowFlicker.Location = new System.Drawing.Point(540, 10);
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
            this.label166.Location = new System.Drawing.Point(11, 92);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(93, 18);
            this.label166.TabIndex = 162;
            this.label166.Text = "Power Curve :";
            // 
            // cboSiteSuitPowerCurve
            // 
            this.cboSiteSuitPowerCurve.FormattingEnabled = true;
            this.cboSiteSuitPowerCurve.Location = new System.Drawing.Point(110, 89);
            this.cboSiteSuitPowerCurve.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSiteSuitPowerCurve.Name = "cboSiteSuitPowerCurve";
            this.cboSiteSuitPowerCurve.Size = new System.Drawing.Size(253, 26);
            this.cboSiteSuitPowerCurve.TabIndex = 161;
            // 
            // btnImportZones
            // 
            this.btnImportZones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportZones.Location = new System.Drawing.Point(20, 640);
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
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.aboutContinuumToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.mongoDBTestToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1520, 25);
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
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
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
            this.generateHeadersToolStripMenuItem,
            this.downloadReanalysisDataToolStripMenuItem,
            this.downloadTopographyDataToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(50, 21);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // generateHeadersToolStripMenuItem
            // 
            this.generateHeadersToolStripMenuItem.Name = "generateHeadersToolStripMenuItem";
            this.generateHeadersToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.generateHeadersToolStripMenuItem.Text = "Generate Headers";
            this.generateHeadersToolStripMenuItem.Click += new System.EventHandler(this.generateHeadersToolStripMenuItem_Click);
            // 
            // downloadReanalysisDataToolStripMenuItem
            // 
            this.downloadReanalysisDataToolStripMenuItem.Name = "downloadReanalysisDataToolStripMenuItem";
            this.downloadReanalysisDataToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.downloadReanalysisDataToolStripMenuItem.Text = "Download Reanalysis Data";
            this.downloadReanalysisDataToolStripMenuItem.Click += new System.EventHandler(this.downloadMERRA2DataToolStripMenuItem_Click);
            // 
            // downloadTopographyDataToolStripMenuItem
            // 
            this.downloadTopographyDataToolStripMenuItem.Name = "downloadTopographyDataToolStripMenuItem";
            this.downloadTopographyDataToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.downloadTopographyDataToolStripMenuItem.Text = "Download Topography Data";
            this.downloadTopographyDataToolStripMenuItem.Click += new System.EventHandler(this.downloadTopographyDataToolStripMenuItem_Click);
            // 
            // mongoDBTestToolStripMenuItem
            // 
            this.mongoDBTestToolStripMenuItem.Name = "mongoDBTestToolStripMenuItem";
            this.mongoDBTestToolStripMenuItem.Size = new System.Drawing.Size(12, 21);
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
            this.ofdLandCover.Filter = "TIF file|*.TIF|TIFF file|*.TIFF";
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
            // txtMaxShadowDist
            // 
            this.txtMaxShadowDist.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxShadowDist.Location = new System.Drawing.Point(626, 73);
            this.txtMaxShadowDist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaxShadowDist.Name = "txtMaxShadowDist";
            this.txtMaxShadowDist.Size = new System.Drawing.Size(57, 25);
            this.txtMaxShadowDist.TabIndex = 289;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(538, 67);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 36);
            this.label21.TabIndex = 288;
            this.label21.Text = "Max. Shadow\r\n Dist. [m] :";
            // 
            // Continuum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1520, 785);
            this.Controls.Add(this.tabContinuum);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Continuum";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Continuum Wind Flow Model";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Continuum_FormClosing);
            this.Load += new System.EventHandler(this.Continuum_Load);
            this.tabContinuum.ResumeLayout(false);
            this.pgeInput.ResumeLayout(false);
            this.pgeInput.PerformLayout();
            this.pnlInputMap.ResumeLayout(false);
            this.pnlInputMapAndLegend.ResumeLayout(false);
            this.pnlInputMapAndLegend.PerformLayout();
            this.pgeMetDataTS.ResumeLayout(false);
            this.splContMetTS.Panel1.ResumeLayout(false);
            this.splContMetTS.Panel1.PerformLayout();
            this.splContMetTS.Panel2.ResumeLayout(false);
            this.splContMetTS.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splContMetTS)).EndInit();
            this.splContMetTS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataMetTS)).EndInit();
            this.pgeMetData.ResumeLayout(false);
            this.pgeMetData.PerformLayout();
            this.pnlMetDatQC_WSDiff.ResumeLayout(false);
            this.pnlMetDataQC_Scatter.ResumeLayout(false);
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
            this.tabSiteConditions.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTerrainComplex)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataExtremeWS)).EndInit();
            this.pgeMonthlyAnalysis.ResumeLayout(false);
            this.pgeMonthlyAnalysis.PerformLayout();
            this.pgeMaps.ResumeLayout(false);
            this.pgeMaps.PerformLayout();
            this.pgeRound.ResumeLayout(false);
            this.pgeRound.PerformLayout();
            this.pgeStepwise.ResumeLayout(false);
            this.pgeStepwise.PerformLayout();
            this.spltNodePlotParams.Panel1.ResumeLayout(false);
            this.spltNodePlotParams.Panel2.ResumeLayout(false);
            this.spltNodePlotParams.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltNodePlotParams)).EndInit();
            this.spltNodePlotParams.ResumeLayout(false);
            this.spltModelCoeffs.Panel1.ResumeLayout(false);
            this.spltModelCoeffs.Panel1.PerformLayout();
            this.spltModelCoeffs.Panel2.ResumeLayout(false);
            this.spltModelCoeffs.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltModelCoeffs)).EndInit();
            this.spltModelCoeffs.ResumeLayout(false);
            this.spltAdvanced.Panel1.ResumeLayout(false);
            this.spltAdvanced.Panel1.PerformLayout();
            this.spltAdvanced.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltAdvanced)).EndInit();
            this.spltAdvanced.ResumeLayout(false);
            this.spltAdvUWDW.Panel1.ResumeLayout(false);
            this.spltAdvUWDW.Panel1.PerformLayout();
            this.spltAdvUWDW.Panel2.ResumeLayout(false);
            this.spltAdvUWDW.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltAdvUWDW)).EndInit();
            this.spltAdvUWDW.ResumeLayout(false);
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
        internal System.Windows.Forms.TextBox txtUTMZone;
        internal System.Windows.Forms.Label Label57;
        internal System.Windows.Forms.TextBox txtUTMDatum;
        internal System.Windows.Forms.Label Label49;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Button btnImportTAB;
        internal System.Windows.Forms.Button btnGenTurbEsts;
        internal System.Windows.Forms.Label Label30;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.Button btnDelTurb;
        internal System.Windows.Forms.Button btnEditTurb;
        internal System.Windows.Forms.Button btnAddTurb;
        internal System.Windows.Forms.ColumnHeader ColumnHeader17;
        internal System.Windows.Forms.ColumnHeader ColumnHeader18;
        internal System.Windows.Forms.ColumnHeader ColumnHeader19;
        internal System.Windows.Forms.Label lblTurbineList;
        internal System.Windows.Forms.Button btnDelMet;
        public System.Windows.Forms.ListView lstMetTowers;
        internal System.Windows.Forms.ColumnHeader metName;
        internal System.Windows.Forms.ColumnHeader Lats;
        internal System.Windows.Forms.ColumnHeader Longs;
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
        internal System.Windows.Forms.TextBox txtAdv_FlowSep_Used;
        internal System.Windows.Forms.TextBox txtAdv_LC_used;
        internal System.Windows.Forms.ComboBox cboAdvancedWD;
        internal System.Windows.Forms.Label Label56;
        internal System.Windows.Forms.Button btnExportCrossPreds;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.ComboBox cboAdvancedRad;
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
        private System.Windows.Forms.Label label154;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.Label label160;
        public System.Windows.Forms.ComboBox cboMERRA_PowerCurves;
        public System.Windows.Forms.FolderBrowserDialog fbd_MERRAData;
        private System.Windows.Forms.Label label162;
        public System.Windows.Forms.TextBox txtMERRA_WS_ScaleFact;        
        public System.Windows.Forms.ListView lstMERRAAnnualProd;
        public System.Windows.Forms.ListView lstMERRA_MonthlyProd;
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
        public System.Windows.Forms.ComboBox cboReferenceMonth;
        private System.Windows.Forms.Label label120;
        public System.Windows.Forms.ComboBox cboReferenceYear;
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
        internal System.Windows.Forms.Label label92;
        public System.Windows.Forms.Button btnDoMCP;
        private System.Windows.Forms.ColumnHeader columnHeader185;
        internal System.Windows.Forms.TextBox txtisMCPdGross;
        public System.Windows.Forms.Button btnDoMCPAllMets;
        internal System.Windows.Forms.TextBox txtisMCPdNet;
        internal System.Windows.Forms.TextBox txtisMCPdUncert;
        internal System.Windows.Forms.TextBox txtisMCPdAdv;
        private System.Windows.Forms.Button btnGetDefaultExceed;
        private System.Windows.Forms.Button btnImportCurves;
        internal System.Windows.Forms.OpenFileDialog ofdExceedCurves;
        internal System.Windows.Forms.Button btnEditModHeight;
        internal System.Windows.Forms.CheckBox chkDisableFilter;
        internal System.Windows.Forms.ColumnHeader columnHeader186;
        private System.Windows.Forms.ColumnHeader columnHeader193;
        private System.Windows.Forms.ColumnHeader columnHeader194;
        private System.Windows.Forms.Label label51;
        internal System.Windows.Forms.CheckBox chkCreateTurbTS;
        internal System.Windows.Forms.Button btnGenTurbGross;
        public System.Windows.Forms.ListView lstZones;
        private System.Windows.Forms.Button btnShowMCPRanges;
        public System.Windows.Forms.CheckBox chkUseSR;
        public System.Windows.Forms.CheckBox chk_Use_Sep;
        public System.Windows.Forms.OpenFileDialog ofdPowerCurve;
        public System.Windows.Forms.ListView lstTurbines;
        public System.Windows.Forms.ListView lstPowerCurveList;
        public System.Windows.Forms.SaveFileDialog sfdCFMfile;
        public System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        public System.Windows.Forms.TabControl tabContinuum;
        public OxyPlot.WindowsForms.PlotView plotUWExpo;
        public OxyPlot.WindowsForms.PlotView plotDirectionalWS_Ratios;
        public OxyPlot.WindowsForms.PlotView plotInputWindRose;
        public OxyPlot.WindowsForms.PlotView plotWS_vsHeight;
        private System.Windows.Forms.Label label62;
        public OxyPlot.WindowsForms.PlotView plotMetQC_WindRose;
        public OxyPlot.WindowsForms.PlotView plotAlphaByWD;
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
        public OxyPlot.WindowsForms.PlotView plotMonthlyTS;
        public OxyPlot.WindowsForms.PlotView plotYearlyTS;
        public OxyPlot.WindowsForms.PlotView plotGenMap;
        public OxyPlot.WindowsForms.PlotView plotRRErrorByNumMets;
        public OxyPlot.WindowsForms.PlotView plotRR_Histo;
        public OxyPlot.WindowsForms.PlotView plotTurbUncert;
        public OxyPlot.WindowsForms.PlotView plotAdvTopo;
        public OxyPlot.WindowsForms.PlotView plotIceShadowSound;
        public OxyPlot.WindowsForms.PlotView plotIceVsDist;
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
        private System.Windows.Forms.Button btnExplainMERRA2Tab;
        private System.Windows.Forms.ToolStripMenuItem downloadReanalysisDataToolStripMenuItem;
        public System.Windows.Forms.DateTimePicker dateLTRefAvailEnd;
        public System.Windows.Forms.DateTimePicker dateLTRefAvailStart;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label205;
        public System.Windows.Forms.TextBox txtRefDataAvail;
        private System.Windows.Forms.Label label204;
        private System.Windows.Forms.Label label206;
        public System.Windows.Forms.ComboBox cboMCP_Ref;
        private System.Windows.Forms.Label label207;
        public System.Windows.Forms.ComboBox cboLTReferences;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnEditReference;
        private System.Windows.Forms.Button btnDelRef;
        private System.Windows.Forms.TabControl tabSiteConditions;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        public OxyPlot.WindowsForms.PlotView plotTurbInt;
        private System.Windows.Forms.Button btnExportTI;
        internal System.Windows.Forms.Label label188;
        internal System.Windows.Forms.ComboBox cboTurbPowerCurve;
        public System.Windows.Forms.DateTimePicker dateTIStart;
        private System.Windows.Forms.Label label186;
        private System.Windows.Forms.Label label187;
        public System.Windows.Forms.DateTimePicker dateTIEnd;
        internal System.Windows.Forms.ComboBox cboTurbineTI;
        internal System.Windows.Forms.Label label179;
        internal System.Windows.Forms.Label label178;
        internal System.Windows.Forms.ComboBox cboEffectiveTI_m;
        internal System.Windows.Forms.ComboBox cboTI_Type;
        internal System.Windows.Forms.Label label177;
        public System.Windows.Forms.ListView lstTurbulence;
        private System.Windows.Forms.ColumnHeader columnHeader177;
        private System.Windows.Forms.ColumnHeader columnHeader178;
        private System.Windows.Forms.ColumnHeader columnHeader179;
        internal System.Windows.Forms.Label label174;
        public System.Windows.Forms.ComboBox cboTurbMet;
        internal System.Windows.Forms.Label label172;
        internal System.Windows.Forms.ComboBox cboTurbWD;
        internal System.Windows.Forms.Label label93;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        internal System.Windows.Forms.Label label199;
        internal System.Windows.Forms.Label label198;
        internal System.Windows.Forms.Label label197;
        internal System.Windows.Forms.Label label196;
        public OxyPlot.WindowsForms.PlotView plotInflowAngle;
        internal System.Windows.Forms.Label label200;
        public System.Windows.Forms.TextBox txtInflowAngle;
        internal System.Windows.Forms.ComboBox cboInflowReso;
        internal System.Windows.Forms.ComboBox cboInflowRadius;
        private System.Windows.Forms.Button btnInflowAngles;
        internal System.Windows.Forms.ComboBox cboInflowTurbine;
        internal System.Windows.Forms.ComboBox cboInflowWD;
        internal System.Windows.Forms.Label label171;
        public OxyPlot.WindowsForms.PlotView plotExtremeShear;
        public System.Windows.Forms.DateTimePicker dateTimeExtremeShearStart;
        private System.Windows.Forms.Label label202;
        private System.Windows.Forms.Label label203;
        public System.Windows.Forms.DateTimePicker dateTimeExtremeShearEnd;
        internal System.Windows.Forms.Label label201;
        internal System.Windows.Forms.Label label190;
        public System.Windows.Forms.ComboBox cboExtremeShearRange;
        internal System.Windows.Forms.Label label189;
        public System.Windows.Forms.ComboBox cboExtremeShearMet;
        private System.Windows.Forms.Button btnExportShearStats;
        public System.Windows.Forms.ListView lstExtremeShear;
        private System.Windows.Forms.ColumnHeader columnHeader180;
        private System.Windows.Forms.ColumnHeader columnHeader181;
        private System.Windows.Forms.ColumnHeader columnHeader182;
        private System.Windows.Forms.ColumnHeader columnHeader183;
        private System.Windows.Forms.ColumnHeader columnHeader184;
        internal System.Windows.Forms.Label label95;
        internal System.Windows.Forms.Label label195;
        internal System.Windows.Forms.Label label103;
        public System.Windows.Forms.ComboBox cboExtremeWSRef;
        public OxyPlot.WindowsForms.PlotView plotExtremeWS;
        private System.Windows.Forms.Label lblExtremeWS;
        private System.Windows.Forms.Button btnExtremeWS;
        public System.Windows.Forms.ComboBox cboExtremeWSMet;
        public System.Windows.Forms.TextBox txt1yrExtremeGust;
        public System.Windows.Forms.TextBox txt1yrExtreme10min;
        public System.Windows.Forms.TextBox txt50yrExtremeGust;
        public System.Windows.Forms.TextBox txt50yrExtreme10min;
        private System.Windows.Forms.Label label193;
        private System.Windows.Forms.Label label194;
        private System.Windows.Forms.Label label192;
        private System.Windows.Forms.Label label191;
        internal System.Windows.Forms.Label label170;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.Button btnShowIECThresh;
        public OxyPlot.WindowsForms.PlotView plotComplexHisto;
        private System.Windows.Forms.Label label161;
        public System.Windows.Forms.DataGridView dataTerrainComplex;
        public System.Windows.Forms.ComboBox cboTSIorTVIorP90;
        public System.Windows.Forms.Label lblIEC_Complexity;
        internal System.Windows.Forms.Button btnCalcTerrainComplexity;
        private System.Windows.Forms.TabPage pgeMetDataTS;
        private System.Windows.Forms.SplitContainer splContMetTS;
        private System.Windows.Forms.Label label146;
        private System.Windows.Forms.Label label149;
        public System.Windows.Forms.DateTimePicker dateMetTS_End;
        public System.Windows.Forms.DateTimePicker dateMetTS_Start;
        private System.Windows.Forms.Button btnMetTS_Right;
        private System.Windows.Forms.Label label208;
        private System.Windows.Forms.Label lblMetDataTS_Inc;
        private System.Windows.Forms.Button btnMetTS_Left;
        public OxyPlot.WindowsForms.PlotView plotTS_Anems;
        public OxyPlot.WindowsForms.PlotView plotTS_Temp;
        public OxyPlot.WindowsForms.PlotView plotTS_Vanes;
        private System.Windows.Forms.Label label209;
        private System.Windows.Forms.Label label211;
        private System.Windows.Forms.Label label210;
        private System.Windows.Forms.Label label213;
        private System.Windows.Forms.Label label212;
        public OxyPlot.WindowsForms.PlotView plotTS_Baros;
        public System.Windows.Forms.ComboBox cboNumPlots;
        public System.Windows.Forms.ComboBox cboPlot2Type;
        public System.Windows.Forms.ComboBox cboPlot1Type;
        public System.Windows.Forms.ComboBox cboPlot4Type;
        public System.Windows.Forms.ComboBox cboPlot3Type;
        public System.Windows.Forms.TextBox txtNumDaysTS;
        private System.Windows.Forms.Label label214;
        public System.Windows.Forms.CheckBox chkShowLegenMetDataTS;
        public System.Windows.Forms.CheckBox chkShowFilteredData;
        private System.Windows.Forms.Button btnRefDataDownloads;
        private System.Windows.Forms.Label label216;
        public System.Windows.Forms.TextBox txtRefDataDownloadFolder;
        public System.Windows.Forms.TextBox txtRefDataDownloadName;
        private System.Windows.Forms.Label label217;
        public System.Windows.Forms.TextBox txtMaxLong;
        private System.Windows.Forms.Label label218;
        public System.Windows.Forms.TextBox txtMinLong;
        private System.Windows.Forms.Label label219;
        public System.Windows.Forms.TextBox txtMaxLat;
        private System.Windows.Forms.Label label220;
        public System.Windows.Forms.TextBox txtMinLat;
        public System.Windows.Forms.ComboBox cboTI_TerrainComplexCorr;
        public System.Windows.Forms.CheckBox chkApplyTCCtoEffTI;
        private System.Windows.Forms.Button btnShowFilterFlags;
        public System.Windows.Forms.DataGridView dataMetTS;
        public System.Windows.Forms.CheckedListBox chkTS_Params;
        public System.Windows.Forms.TreeView treeDataParams;
        public System.Windows.Forms.CheckedListBox chkMetsTS;
        private System.Windows.Forms.Button btnZoneFileFormatHelp;
        internal TextBox txtTopoNullValue;
        internal Label label5;
        public CheckBox chkForceThruBase;
        internal Button btnEditRotorDiam;
        internal Label label221;
        public TextBox txtRotorDiam;
        internal Button btnEditAirDensity;
        internal Label label222;
        public TextBox txtAirDensity;
        public ComboBox cboWindOrEnergy;
        public ComboBox cboRefWindOrEnergy;
        public ComboBox cboNumWDRefTab;
        private Label label223;
        public ComboBox cboNumWDComplxTab;
        private Label label224;
        private Button btnExportShearHisto;
        private Label label225;
        private Label label226;
        private Label label227;
        private Button btnEditShearMethod;
        private Button btnEditShearFromSiteConds;
        private Label label228;
        private Label label229;
        private Label label230;
        public TextBox txtShearCalcMethod;
        public TextBox txtShearBestFitMinHeight;
        public TextBox txtShearBestFitMaxHeight;
        public TextBox txtShearCalcMethodExtremeTab;
        public TextBox txtMaxHeight;
        public TextBox txtMinHeight;
        public OxyPlot.WindowsForms.PlotView plotExtremeWS_TS;
        public DataGridView dataExtremeWS;
        public CheckBox chkUseSimData;
        public TextBox txtGumbelTenMinBeta;
        private Label label232;
        private Label label231;
        private Label label235;
        public TextBox txtGumbelGustMu;
        private Label label236;
        public TextBox txtGumbelGustBeta;
        private Label label234;
        private Label label233;
        public TextBox txtGumbelTenMinMu;
        private Label label237;
        public ComboBox cboMCP_Height;
        internal Label label238;
        public ComboBox cboExtremeWS_Height;
        public Label lblGustExtremeWSUnavailable;
        private Label label239;
        public DateTimePicker dateExtremeWS_End;
        public DateTimePicker dateExtremeWS_Start;
        public CheckBox chkExtremeWS_ShowLegend;
        public CheckBox chkUseWMO_Gust;
        internal Label label240;
        public ComboBox cboWMO_Class;
        internal Label label241;
        public CheckBox chkUseWMO_TenMin;
        private Label label243;
        public TextBox txtWMO_HourTenMin;
        private Label label242;
        public TextBox txtWMO_HourGust;
        public TextBox txtWMO_Desc;
        private Button btnExportExtremeWSTable;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private Label label215;
        internal Button btnImportModel;
        internal Button btnExportModel;
        private Panel pnlInputMapAndLegend;
        internal Label lblMetLabels;
        internal ComboBox cboTopo_Or_Roughness;
        internal CheckBox chkAllTurbLabels;
        internal CheckBox chkAllMetLabels;
        internal Label Label23;
        internal CheckedListBox chkTurbLabels;
        internal CheckedListBox chkMetLabels;
        internal TextBox txtTopoSource;
        private Panel pnlInputMap;
        public OxyPlot.WindowsForms.PlotView plotTopo;
        private Label label70;
        public OxyPlot.WindowsForms.PlotView plotWSDiffByWD;
        private Panel pnlMetDataQC_Scatter;
        public OxyPlot.WindowsForms.PlotView plotAnemScatter;
        private Panel pnlMetDatQC_WSDiff;
        public OxyPlot.WindowsForms.PlotView plotWSDiffByWS;
        private Label label102;
        private Label label69;
        private Button btnExportTerrainComplexSector;
        public ComboBox cboInputLLorDD;
        private DataGridViewTextBoxColumn Column17;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column20;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column19;
        private DataGridViewTextBoxColumn Column16;
        internal Button btnExportElevProfile;
        public CheckBox chkTerrainSlope_UWOnly;
        private ToolStripMenuItem mongoDBTestToolStripMenuItem;
        public Button btnClearMCP;
        public Button btnShowMCP_Info;
        internal Button btnShowMetTS_Info;
        private ToolStripMenuItem downloadTopographyDataToolStripMenuItem;
        public CheckBox chkUseElevModel;
        internal Button btnDoAllRRs;
        internal TextBox txtImportedModel;
        internal Button btnExportRMS_Errors;
        public CheckBox chkUseValleyFlow;
        public ComboBox cboTAB_bins;
        public Button btnExportCloudCover;
        internal Button btnExportRR_Summary;
        private SplitContainer spltAdvanced;
        public Label lblTurbineTSNoAdvanced;
        internal ListView lstPathNodes;
        internal ColumnHeader Site;
        internal ColumnHeader UTMX;
        internal ColumnHeader UTMY;
        internal ColumnHeader elev;
        internal ColumnHeader P10UW;
        internal ColumnHeader P10DW;
        internal ColumnHeader UW;
        internal ColumnHeader DW;
        internal ColumnHeader WSEst;
        internal ColumnHeader UW_SR;
        internal ColumnHeader DW_SR;
        internal ColumnHeader UW_DH;
        internal ColumnHeader DW_DH;
        private SplitContainer spltAdvUWDW;
        internal Label Label8;
        internal ListView lstPathNodes_UW;
        internal ColumnHeader ColumnHeader14;
        internal ColumnHeader ColumnHeader121;
        internal ColumnHeader ColumnHeader115;
        internal ColumnHeader ColumnHeader120;
        internal ColumnHeader ColumnHeader111;
        internal Label Label39;
        internal ListView lstPathNodes_DW;
        internal ColumnHeader ColumnHeader15;
        internal ColumnHeader ColumnHeader110;
        internal ColumnHeader ColumnHeader108;
        internal ColumnHeader ColumnHeader109;
        internal ColumnHeader ColumnHeader112;
        public Button btnExportSiteSuitMap;
        private SplitContainer spltModelCoeffs;
        public OxyPlot.WindowsForms.PlotView plotUHModel;
        internal Label Label101;
        internal TextBox txtSepCritWS;
        internal Label Label13;
        internal TextBox txtSepCrit;
        internal Label Label100;
        internal ComboBox cboDHplot;
        public OxyPlot.WindowsForms.PlotView plotDHModel;
        internal ComboBox cboExpo_or_Stab;
        internal Label Label82;
        internal Label Label59;
        internal TextBox txtUWCrit;
        internal Label Label58;
        internal ComboBox cboUphill_to_show;
        private SplitContainer spltNodePlotParams;
        public OxyPlot.WindowsForms.PlotView plotPathAlongNodes;
        internal CheckBox chkWeight_RMS;
        internal TextBox txtSectRMS;
        internal Label Label48;
        internal TextBox txtUWDWRMS;
        internal Label Label44;
        internal CheckedListBox chkAdvToShow;
        public TextBox txtMaxShadowDist;
        internal Label label21;
    }
}

