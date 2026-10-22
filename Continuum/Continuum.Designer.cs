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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Continuum));
            this.tabContinuum = new System.Windows.Forms.TabControl();
            this.pgeInput = new System.Windows.Forms.TabPage();
            this.lblMetDataType = new System.Windows.Forms.Label();
            this.btnImportMetData = new System.Windows.Forms.Button();
            this.plotDirectionalWS_Ratios = new OxyPlot.WindowsForms.PlotView();
            this.label248 = new System.Windows.Forms.Label();
            this.label247 = new System.Windows.Forms.Label();
            this.dataMetTowers = new System.Windows.Forms.DataGridView();
            this.Column33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataTurbines = new System.Windows.Forms.DataGridView();
            this.colTurbineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEasting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNorthing = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.plotInputWindRose = new OxyPlot.WindowsForms.PlotView();
            this.chkCreateTurbTS = new System.Windows.Forms.CheckBox();
            this.btnEditModHeight = new System.Windows.Forms.Button();
            this.label168 = new System.Windows.Forms.Label();
            this.txtModeledHeight = new System.Windows.Forms.TextBox();
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
            this.btnGenTurbEsts = new System.Windows.Forms.Button();
            this.Label30 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.btnDelTurb = new System.Windows.Forms.Button();
            this.btnDelMet = new System.Windows.Forms.Button();
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
            this.dataAlphas = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataExtrapolated = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataTempSummary = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataVaneSummary = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataAnemSummary = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.label68 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.cboMetWindRose = new System.Windows.Forms.ComboBox();
            this.label64 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
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
            this.pgeMERRA = new System.Windows.Forms.TabPage();
            this.splotLTRef_1 = new System.Windows.Forms.SplitContainer();
            this.btnDelRef = new System.Windows.Forms.Button();
            this.btnEditReference = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.label207 = new System.Windows.Forms.Label();
            this.cboLTReferences = new System.Windows.Forms.ComboBox();
            this.dateMERRAEnd = new System.Windows.Forms.DateTimePicker();
            this.dateMERRAStart = new System.Windows.Forms.DateTimePicker();
            this.label152 = new System.Windows.Forms.Label();
            this.label153 = new System.Windows.Forms.Label();
            this.btnExportCloudCover = new System.Windows.Forms.Button();
            this.txtRefDataDownloadName = new System.Windows.Forms.TextBox();
            this.label216 = new System.Windows.Forms.Label();
            this.txtRefDataDownloadFolder = new System.Windows.Forms.TextBox();
            this.btn_ExportWR = new System.Windows.Forms.Button();
            this.btn_Export_All_Months_All_Years = new System.Windows.Forms.Button();
            this.btn_Export_Interp = new System.Windows.Forms.Button();
            this.label154 = new System.Windows.Forms.Label();
            this.txtLT_Avg_Prod = new System.Windows.Forms.TextBox();
            this.txtLT_Avg_CF = new System.Windows.Forms.TextBox();
            this.txtLT_Avg_WS = new System.Windows.Forms.TextBox();
            this.label162 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label217 = new System.Windows.Forms.Label();
            this.txtMaxLong = new System.Windows.Forms.TextBox();
            this.label218 = new System.Windows.Forms.Label();
            this.txtMinLong = new System.Windows.Forms.TextBox();
            this.label219 = new System.Windows.Forms.Label();
            this.txtMaxLat = new System.Windows.Forms.TextBox();
            this.label220 = new System.Windows.Forms.Label();
            this.txtMinLat = new System.Windows.Forms.TextBox();
            this.label205 = new System.Windows.Forms.Label();
            this.txtRefDataAvail = new System.Windows.Forms.TextBox();
            this.label204 = new System.Windows.Forms.Label();
            this.dateLTRefAvailEnd = new System.Windows.Forms.DateTimePicker();
            this.dateLTRefAvailStart = new System.Windows.Forms.DateTimePicker();
            this.label52 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dataRefAnnualProd = new System.Windows.Forms.DataGridView();
            this.Column44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column46 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.dataRefMonthlyProd = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column47 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboNumWDRefTab = new System.Windows.Forms.ComboBox();
            this.label223 = new System.Windows.Forms.Label();
            this.chkYearsToDisplayAll = new System.Windows.Forms.CheckBox();
            this.chkYearsToDisplay = new System.Windows.Forms.CheckedListBox();
            this.plotMERRA_WindRose = new OxyPlot.WindowsForms.PlotView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.plotMERRA_Yearly = new OxyPlot.WindowsForms.PlotView();
            this.plotMERRA_Monthly = new OxyPlot.WindowsForms.PlotView();
            this.cboRefWindOrEnergy = new System.Windows.Forms.ComboBox();
            this.btnRefDataDownloads = new System.Windows.Forms.Button();
            this.btnExplainMERRA2Tab = new System.Windows.Forms.Button();
            this.btnImportCRV_MERRA = new System.Windows.Forms.Button();
            this.label121 = new System.Windows.Forms.Label();
            this.cboReferenceMonth = new System.Windows.Forms.ComboBox();
            this.label120 = new System.Windows.Forms.Label();
            this.cboReferenceYear = new System.Windows.Forms.ComboBox();
            this.label160 = new System.Windows.Forms.Label();
            this.cboMERRA_PowerCurves = new System.Windows.Forms.ComboBox();
            this.label159 = new System.Windows.Forms.Label();
            this.label156 = new System.Windows.Forms.Label();
            this.cboMERRA_PlotParam = new System.Windows.Forms.ComboBox();
            this.label151 = new System.Windows.Forms.Label();
            this.pgeMCP = new System.Windows.Forms.TabPage();
            this.btnEditMetsUsedForDebias = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chkUsingMetsForDebias = new System.Windows.Forms.CheckBox();
            this.txtDebiasRSq = new System.Windows.Forms.TextBox();
            this.label254 = new System.Windows.Forms.Label();
            this.txtDebiasIntercept = new System.Windows.Forms.TextBox();
            this.label253 = new System.Windows.Forms.Label();
            this.txtDebiasSlope = new System.Windows.Forms.TextBox();
            this.label252 = new System.Windows.Forms.Label();
            this.dataDebias = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn103 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn104 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn105 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkUseDebiased = new System.Windows.Forms.CheckBox();
            this.chkUseMeasRose = new System.Windows.Forms.CheckBox();
            this.txtDebiasedWS = new System.Windows.Forms.TextBox();
            this.label251 = new System.Windows.Forms.Label();
            this.plotMCP_Debias = new OxyPlot.WindowsForms.PlotView();
            this.btnDebiasWS = new System.Windows.Forms.Button();
            this.label250 = new System.Windows.Forms.Label();
            this.dataMCP_Uncert = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataMCP_Bins = new System.Windows.Forms.DataGridView();
            this.Column48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column49 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column51 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.spltMetTurbSumm = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.chkMetSumm = new System.Windows.Forms.CheckedListBox();
            this.chkTurbSumm = new System.Windows.Forms.CheckedListBox();
            this.dataMetStats = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataTurbStats = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataMetSummary = new System.Windows.Forms.DataGridView();
            this.Column52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column53 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column55 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column56 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column57 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column58 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column59 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column60 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column61 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column62 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer10 = new System.Windows.Forms.SplitContainer();
            this.splitContainer11 = new System.Windows.Forms.SplitContainer();
            this.plotUWExpo = new OxyPlot.WindowsForms.PlotView();
            this.plotDWExpo = new OxyPlot.WindowsForms.PlotView();
            this.splitContainer12 = new System.Windows.Forms.SplitContainer();
            this.plotDWUWExpo = new OxyPlot.WindowsForms.PlotView();
            this.plotElev = new OxyPlot.WindowsForms.PlotView();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.plotUW_SR = new OxyPlot.WindowsForms.PlotView();
            this.plotDW_SR = new OxyPlot.WindowsForms.PlotView();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.plotUW_DH = new OxyPlot.WindowsForms.PlotView();
            this.plotDW_DH = new OxyPlot.WindowsForms.PlotView();
            this.cboSummSeason = new System.Windows.Forms.ComboBox();
            this.cboSummTOD = new System.Windows.Forms.ComboBox();
            this.Label80 = new System.Windows.Forms.Label();
            this.chkTurbSummAll = new System.Windows.Forms.CheckBox();
            this.Label78 = new System.Windows.Forms.Label();
            this.chkMetSummAll = new System.Windows.Forms.CheckBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label76 = new System.Windows.Forms.Label();
            this.btnExportExpoSRDH = new System.Windows.Forms.Button();
            this.Label74 = new System.Windows.Forms.Label();
            this.cboSummaryWD = new System.Windows.Forms.ComboBox();
            this.Label75 = new System.Windows.Forms.Label();
            this.cboMetSum_Rad = new System.Windows.Forms.ComboBox();
            this.Label73 = new System.Windows.Forms.Label();
            this.pgeGrossTurbs = new System.Windows.Forms.TabPage();
            this.txtGross_Valley_used = new System.Windows.Forms.TextBox();
            this.txtGross_Elev_used = new System.Windows.Forms.TextBox();
            this.lblModelsUsedGross = new System.Windows.Forms.Label();
            this.txtNameplateCapacity = new System.Windows.Forms.TextBox();
            this.label249 = new System.Windows.Forms.Label();
            this.btnDeletePowerCurve = new System.Windows.Forms.Button();
            this.btnExport_PowerCurve = new System.Windows.Forms.Button();
            this.txtTotalGrossCF = new System.Windows.Forms.TextBox();
            this.label245 = new System.Windows.Forms.Label();
            this.txtTotalGross = new System.Windows.Forms.TextBox();
            this.label246 = new System.Windows.Forms.Label();
            this.dataGrossHisto = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGrossTurbEst = new System.Windows.Forms.DataGridView();
            this.Column121 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column77 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column78 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column79 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column80 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataPowerCurveList = new System.Windows.Forms.DataGridView();
            this.Column74 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colPowerCrvName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column75 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column76 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plotGrossHisto = new OxyPlot.WindowsForms.PlotView();
            this.plotGross_PowerCrvs = new OxyPlot.WindowsForms.PlotView();
            this.plotGrossWS_Dist = new OxyPlot.WindowsForms.PlotView();
            this.plotGrossWindRose = new OxyPlot.WindowsForms.PlotView();
            this.btnGenTurbGross = new System.Windows.Forms.Button();
            this.txtisMCPdGross = new System.Windows.Forms.TextBox();
            this.Label40 = new System.Windows.Forms.Label();
            this.cboWS_or_WD = new System.Windows.Forms.ComboBox();
            this.txtGross_FlowSepUsed = new System.Windows.Forms.TextBox();
            this.Label96 = new System.Windows.Forms.Label();
            this.txtGross_LC_used = new System.Windows.Forms.TextBox();
            this.Label83 = new System.Windows.Forms.Label();
            this.Label79 = new System.Windows.Forms.Label();
            this.Label77 = new System.Windows.Forms.Label();
            this.cboGrossParam = new System.Windows.Forms.ComboBox();
            this.btnExportDirWSDists = new System.Windows.Forms.Button();
            this.Label71 = new System.Windows.Forms.Label();
            this.cboGrossWD = new System.Windows.Forms.ComboBox();
            this.btnExportDirWS = new System.Windows.Forms.Button();
            this.btnExportWSDists = new System.Windows.Forms.Button();
            this.btnWS_AEP_Exprt = new System.Windows.Forms.Button();
            this.txtAEPMax = new System.Windows.Forms.TextBox();
            this.txtAEPMin = new System.Windows.Forms.TextBox();
            this.txtAEPSD = new System.Windows.Forms.TextBox();
            this.txtAEPAvg = new System.Windows.Forms.TextBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label34 = new System.Windows.Forms.Label();
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
            this.pgeExceedance = new System.Windows.Forms.TabPage();
            this.dataPvals = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn46 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn47 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn49 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataDefinedLosses = new System.Windows.Forms.DataGridView();
            this.Column92 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column81 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column82 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column83 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column84 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column85 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSetToIEC_LossUncert = new System.Windows.Forms.Button();
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
            this.chkShowCDFs = new System.Windows.Forms.CheckBox();
            this.chkShowPDF = new System.Windows.Forms.CheckBox();
            this.btnDoMonteCarlo = new System.Windows.Forms.Button();
            this.label181 = new System.Windows.Forms.Label();
            this.label182 = new System.Windows.Forms.Label();
            this.btn_editloss = new System.Windows.Forms.Button();
            this.pgeNetEsts = new System.Windows.Forms.TabPage();
            this.txtValley_Net = new System.Windows.Forms.TextBox();
            this.txtElev_Net = new System.Windows.Forms.TextBox();
            this.txtOverallNetCF = new System.Windows.Forms.TextBox();
            this.label122 = new System.Windows.Forms.Label();
            this.txtOverallNetAEP = new System.Windows.Forms.TextBox();
            this.label86 = new System.Windows.Forms.Label();
            this.dataWakedTurbs = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn59 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn61 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn62 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn63 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn64 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn65 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn66 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn67 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataWakeModels = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn51 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn53 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn55 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn56 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn57 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn58 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column86 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column87 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column88 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column89 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column90 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column91 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.btnDelWakeModel = new System.Windows.Forms.Button();
            this.btnDelWakeGrid = new System.Windows.Forms.Button();
            this.cboWakePlot = new System.Windows.Forms.ComboBox();
            this.Label85 = new System.Windows.Forms.Label();
            this.btnExportNetEsts = new System.Windows.Forms.Button();
            this.Label55 = new System.Windows.Forms.Label();
            this.cboNetWD = new System.Windows.Forms.ComboBox();
            this.btnWakeLossCalc = new System.Windows.Forms.Button();
            this.Label53 = new System.Windows.Forms.Label();
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
            this.dataTurbulence = new System.Windows.Forms.DataGridView();
            this.Column93 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column94 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column95 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.label174 = new System.Windows.Forms.Label();
            this.cboTurbMet = new System.Windows.Forms.ComboBox();
            this.label172 = new System.Windows.Forms.Label();
            this.cboTurbWD = new System.Windows.Forms.ComboBox();
            this.label93 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataExtremeShear = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn60 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn68 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn69 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column96 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column97 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.label126 = new System.Windows.Forms.Label();
            this.txtTimeSeriesInterval = new System.Windows.Forms.TextBox();
            this.btnTimeSeriesLeft = new System.Windows.Forms.Button();
            this.btnTimeSeriesRight = new System.Windows.Forms.Button();
            this.label150 = new System.Windows.Forms.Label();
            this.label244 = new System.Windows.Forms.Label();
            this.dateTimeSeriesEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimeSeriesStart = new System.Windows.Forms.DateTimePicker();
            this.label22 = new System.Windows.Forms.Label();
            this.cboTimeSeriesAnalysis = new System.Windows.Forms.ComboBox();
            this.chkSelectAllWTGsToShow = new System.Windows.Forms.CheckBox();
            this.chkWTGsToShow = new System.Windows.Forms.CheckedListBox();
            this.chkSelectAllTurbineYears = new System.Windows.Forms.CheckBox();
            this.chkYears_Monthly = new System.Windows.Forms.CheckedListBox();
            this.label148 = new System.Windows.Forms.Label();
            this.chkSelectedTurbineParam = new System.Windows.Forms.CheckedListBox();
            this.btnExportTimeSeriesEsts = new System.Windows.Forms.Button();
            this.splitTimeSeries = new System.Windows.Forms.SplitContainer();
            this.dataWTG_TimeSeriesEsts = new System.Windows.Forms.DataGridView();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plotWTG_TimeSeries = new OxyPlot.WindowsForms.PlotView();
            this.label158 = new System.Windows.Forms.Label();
            this.cboMonthlyPowerCurve = new System.Windows.Forms.ComboBox();
            this.cboMonthlyWakeModel = new System.Windows.Forms.ComboBox();
            this.label157 = new System.Windows.Forms.Label();
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
            this.label9 = new System.Windows.Forms.Label();
            this.txtRR_Valley_used = new System.Windows.Forms.TextBox();
            this.txtRR_Elev_used = new System.Windows.Forms.TextBox();
            this.dataTurbUncert = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn76 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn77 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn78 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn79 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn80 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column100 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column101 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column102 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column103 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataRR_AllErr = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn70 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn74 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn75 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column98 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column99 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataRR_Results = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn71 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn72 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn73 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.Label41 = new System.Windows.Forms.Label();
            this.btnExportRR = new System.Windows.Forms.Button();
            this.cboRoundRobin = new System.Windows.Forms.ComboBox();
            this.btnDoRRCalcs = new System.Windows.Forms.Button();
            this.Label46 = new System.Windows.Forms.Label();
            this.pgeStepwise = new System.Windows.Forms.TabPage();
            this.label50 = new System.Windows.Forms.Label();
            this.txtAdv_Valley_used = new System.Windows.Forms.TextBox();
            this.txtAdv_Elev_used = new System.Windows.Forms.TextBox();
            this.dataModCrossPred = new System.Windows.Forms.DataGridView();
            this.Column116 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column117 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column118 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column119 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dataPathNodes = new System.Windows.Forms.DataGridView();
            this.lblTurbineTSNoAdvanced = new System.Windows.Forms.Label();
            this.spltAdvUWDW = new System.Windows.Forms.SplitContainer();
            this.dataPathNodes_UW = new System.Windows.Forms.DataGridView();
            this.Site = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Coeff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dWSExpo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dWSSRDH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label8 = new System.Windows.Forms.Label();
            this.dataPathNodes_DW = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn98 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn99 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn100 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn101 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn102 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label39 = new System.Windows.Forms.Label();
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
            this.Label11 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.pgeSuitability = new System.Windows.Forms.TabPage();
            this.dataZoneSound = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn90 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn91 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataZoneIceHits = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn88 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn89 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column104 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column105 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column106 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column107 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataShadowZoneSummary = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn86 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn87 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataZones = new System.Windows.Forms.DataGridView();
            this.Column120 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn81 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn82 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn83 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn84 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn85 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMaxShadowDist = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
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
            this.label175 = new System.Windows.Forms.Label();
            this.lblShadowByMonthOrIceByDist = new System.Windows.Forms.Label();
            this.label173 = new System.Windows.Forms.Label();
            this.cboIcingYear = new System.Windows.Forms.ComboBox();
            this.txtTotalShadow = new System.Windows.Forms.TextBox();
            this.lblTotalHoursPerYear = new System.Windows.Forms.Label();
            this.lblShadowOrIceByDist = new System.Windows.Forms.Label();
            this.lblZoneOrDirection = new System.Windows.Forms.Label();
            this.cboZoneList = new System.Windows.Forms.ComboBox();
            this.label169 = new System.Windows.Forms.Label();
            this.cboSiteSuitHour = new System.Windows.Forms.ComboBox();
            this.label165 = new System.Windows.Forms.Label();
            this.cboSiteSuitMonth = new System.Windows.Forms.ComboBox();
            this.btnSiteSuitImportCRV = new System.Windows.Forms.Button();
            this.btnDelZones = new System.Windows.Forms.Button();
            this.label164 = new System.Windows.Forms.Label();
            this.cboSiteSuitabilitySelectPlot = new System.Windows.Forms.ComboBox();
            this.btnRunSoundModel = new System.Windows.Forms.Button();
            this.btnRunShadowFlicker = new System.Windows.Forms.Button();
            this.btnRunIceThrow = new System.Windows.Forms.Button();
            this.label167 = new System.Windows.Forms.Label();
            this.label166 = new System.Windows.Forms.Label();
            this.cboSiteSuitPowerCurve = new System.Windows.Forms.ComboBox();
            this.btnImportZones = new System.Windows.Forms.Button();
            this.dataShadow12x24 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn92 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn93 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn94 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn95 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn96 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn97 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column108 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column109 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column110 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column111 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column112 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column113 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column114 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column115 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column63 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column64 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column65 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column66 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column67 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column68 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column69 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column70 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column71 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column72 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column73 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer9 = new System.Windows.Forms.SplitContainer();
            this.tabContinuum.SuspendLayout();
            this.pgeInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMetTowers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTurbines)).BeginInit();
            this.pnlInputMap.SuspendLayout();
            this.pnlInputMapAndLegend.SuspendLayout();
            this.pgeMetDataTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splContMetTS)).BeginInit();
            this.splContMetTS.Panel1.SuspendLayout();
            this.splContMetTS.Panel2.SuspendLayout();
            this.splContMetTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMetTS)).BeginInit();
            this.pgeMetData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataAlphas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataExtrapolated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTempSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataVaneSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataAnemSummary)).BeginInit();
            this.pnlMetDatQC_WSDiff.SuspendLayout();
            this.pnlMetDataQC_Scatter.SuspendLayout();
            this.pgeMERRA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splotLTRef_1)).BeginInit();
            this.splotLTRef_1.Panel1.SuspendLayout();
            this.splotLTRef_1.Panel2.SuspendLayout();
            this.splotLTRef_1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataRefAnnualProd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataRefMonthlyProd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pgeMCP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataDebias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataMCP_Uncert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataMCP_Bins)).BeginInit();
            this.pgeMetSumm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltMetTurbSumm)).BeginInit();
            this.spltMetTurbSumm.Panel1.SuspendLayout();
            this.spltMetTurbSumm.Panel2.SuspendLayout();
            this.spltMetTurbSumm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMetStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTurbStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataMetSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).BeginInit();
            this.splitContainer10.Panel1.SuspendLayout();
            this.splitContainer10.Panel2.SuspendLayout();
            this.splitContainer10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).BeginInit();
            this.splitContainer11.Panel1.SuspendLayout();
            this.splitContainer11.Panel2.SuspendLayout();
            this.splitContainer11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer12)).BeginInit();
            this.splitContainer12.Panel1.SuspendLayout();
            this.splitContainer12.Panel2.SuspendLayout();
            this.splitContainer12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            this.pgeGrossTurbs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrossHisto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrossTurbEst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPowerCurveList)).BeginInit();
            this.pgeExceedance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataPvals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataDefinedLosses)).BeginInit();
            this.pgeNetEsts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataWakedTurbs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataWakeModels)).BeginInit();
            this.pgeSiteConditions.SuspendLayout();
            this.tabSiteConditions.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTerrainComplex)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTurbulence)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataExtremeShear)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataExtremeWS)).BeginInit();
            this.pgeMonthlyAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTimeSeries)).BeginInit();
            this.splitTimeSeries.Panel1.SuspendLayout();
            this.splitTimeSeries.Panel2.SuspendLayout();
            this.splitTimeSeries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataWTG_TimeSeriesEsts)).BeginInit();
            this.pgeMaps.SuspendLayout();
            this.pgeRound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTurbUncert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataRR_AllErr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataRR_Results)).BeginInit();
            this.pgeStepwise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataModCrossPred)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataPathNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spltAdvUWDW)).BeginInit();
            this.spltAdvUWDW.Panel1.SuspendLayout();
            this.spltAdvUWDW.Panel2.SuspendLayout();
            this.spltAdvUWDW.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataPathNodes_UW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPathNodes_DW)).BeginInit();
            this.pgeSuitability.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataZoneSound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataZoneIceHits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataShadowZoneSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataZones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataShadow12x24)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).BeginInit();
            this.splitContainer9.SuspendLayout();
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
            this.pgeInput.Controls.Add(this.lblMetDataType);
            this.pgeInput.Controls.Add(this.btnImportMetData);
            this.pgeInput.Controls.Add(this.plotDirectionalWS_Ratios);
            this.pgeInput.Controls.Add(this.label248);
            this.pgeInput.Controls.Add(this.label247);
            this.pgeInput.Controls.Add(this.dataMetTowers);
            this.pgeInput.Controls.Add(this.dataTurbines);
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
            this.pgeInput.Controls.Add(this.plotInputWindRose);
            this.pgeInput.Controls.Add(this.chkCreateTurbTS);
            this.pgeInput.Controls.Add(this.btnEditModHeight);
            this.pgeInput.Controls.Add(this.label168);
            this.pgeInput.Controls.Add(this.txtModeledHeight);
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
            this.pgeInput.Controls.Add(this.btnGenTurbEsts);
            this.pgeInput.Controls.Add(this.Label30);
            this.pgeInput.Controls.Add(this.Label19);
            this.pgeInput.Controls.Add(this.btnDelTurb);
            this.pgeInput.Controls.Add(this.btnDelMet);
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
            // lblMetDataType
            // 
            this.lblMetDataType.AutoSize = true;
            this.lblMetDataType.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetDataType.Location = new System.Drawing.Point(132, 239);
            this.lblMetDataType.Name = "lblMetDataType";
            this.lblMetDataType.Size = new System.Drawing.Size(95, 18);
            this.lblMetDataType.TabIndex = 153;
            this.lblMetDataType.Text = "Met Data Type";
            // 
            // btnImportMetData
            // 
            this.btnImportMetData.BackColor = System.Drawing.Color.LightCoral;
            this.btnImportMetData.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.btnImportMetData.Location = new System.Drawing.Point(126, 202);
            this.btnImportMetData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportMetData.Name = "btnImportMetData";
            this.btnImportMetData.Size = new System.Drawing.Size(127, 29);
            this.btnImportMetData.TabIndex = 152;
            this.btnImportMetData.Text = "Import Met Data";
            this.btnImportMetData.UseVisualStyleBackColor = false;
            this.btnImportMetData.Click += new System.EventHandler(this.btnImportMetData_Click);
            // 
            // plotDirectionalWS_Ratios
            // 
            this.plotDirectionalWS_Ratios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotDirectionalWS_Ratios.Location = new System.Drawing.Point(255, 482);
            this.plotDirectionalWS_Ratios.Name = "plotDirectionalWS_Ratios";
            this.plotDirectionalWS_Ratios.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDirectionalWS_Ratios.Size = new System.Drawing.Size(223, 214);
            this.plotDirectionalWS_Ratios.TabIndex = 151;
            this.plotDirectionalWS_Ratios.Text = "plotView1";
            this.plotDirectionalWS_Ratios.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDirectionalWS_Ratios.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDirectionalWS_Ratios.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // label248
            // 
            this.label248.AutoSize = true;
            this.label248.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label248.Location = new System.Drawing.Point(545, 29);
            this.label248.Name = "label248";
            this.label248.Size = new System.Drawing.Size(84, 18);
            this.label248.TabIndex = 148;
            this.label248.Text = "Coordinates:";
            // 
            // label247
            // 
            this.label247.AutoSize = true;
            this.label247.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label247.Location = new System.Drawing.Point(322, 65);
            this.label247.Name = "label247";
            this.label247.Size = new System.Drawing.Size(200, 18);
            this.label247.TabIndex = 147;
            this.label247.Text = "Enable Wind Flow Components:";
            // 
            // dataMetTowers
            // 
            this.dataMetTowers.AllowUserToAddRows = false;
            this.dataMetTowers.AllowUserToDeleteRows = false;
            this.dataMetTowers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataMetTowers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataMetTowers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataMetTowers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column33,
            this.Column34,
            this.Column35});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataMetTowers.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataMetTowers.Location = new System.Drawing.Point(26, 296);
            this.dataMetTowers.Name = "dataMetTowers";
            this.dataMetTowers.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataMetTowers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataMetTowers.RowHeadersVisible = false;
            this.dataMetTowers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataMetTowers.Size = new System.Drawing.Size(451, 143);
            this.dataMetTowers.TabIndex = 146;
            // 
            // Column33
            // 
            this.Column33.HeaderText = "Met Name";
            this.Column33.Name = "Column33";
            this.Column33.ReadOnly = true;
            // 
            // Column34
            // 
            this.Column34.HeaderText = "UTMX [m]";
            this.Column34.Name = "Column34";
            this.Column34.ReadOnly = true;
            // 
            // Column35
            // 
            this.Column35.HeaderText = "UTMY [m]";
            this.Column35.Name = "Column35";
            this.Column35.ReadOnly = true;
            // 
            // dataTurbines
            // 
            this.dataTurbines.AllowUserToAddRows = false;
            this.dataTurbines.AllowUserToDeleteRows = false;
            this.dataTurbines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataTurbines.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataTurbines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTurbines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTurbineName,
            this.colEasting,
            this.colNorthing});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataTurbines.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataTurbines.Location = new System.Drawing.Point(487, 296);
            this.dataTurbines.Name = "dataTurbines";
            this.dataTurbines.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataTurbines.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataTurbines.RowHeadersVisible = false;
            this.dataTurbines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataTurbines.Size = new System.Drawing.Size(290, 401);
            this.dataTurbines.TabIndex = 145;
            // 
            // colTurbineName
            // 
            this.colTurbineName.HeaderText = "Name";
            this.colTurbineName.Name = "colTurbineName";
            this.colTurbineName.ReadOnly = true;
            this.colTurbineName.Width = 90;
            // 
            // colEasting
            // 
            this.colEasting.HeaderText = "UTMX [m]";
            this.colEasting.Name = "colEasting";
            this.colEasting.ReadOnly = true;
            // 
            // colNorthing
            // 
            this.colNorthing.HeaderText = "UTMY [m]";
            this.colNorthing.Name = "colNorthing";
            this.colNorthing.ReadOnly = true;
            // 
            // chkUseValleyFlow
            // 
            this.chkUseValleyFlow.AutoSize = true;
            this.chkUseValleyFlow.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseValleyFlow.Location = new System.Drawing.Point(350, 157);
            this.chkUseValleyFlow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkUseValleyFlow.Name = "chkUseValleyFlow";
            this.chkUseValleyFlow.Size = new System.Drawing.Size(100, 21);
            this.chkUseValleyFlow.TabIndex = 144;
            this.chkUseValleyFlow.Text = "Valley Model";
            this.chkUseValleyFlow.UseVisualStyleBackColor = true;
            this.chkUseValleyFlow.CheckedChanged += new System.EventHandler(this.chkUseValleyFlow_CheckedChanged);
            // 
            // chkUseElevModel
            // 
            this.chkUseElevModel.AutoSize = true;
            this.chkUseElevModel.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseElevModel.Location = new System.Drawing.Point(350, 135);
            this.chkUseElevModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkUseElevModel.Name = "chkUseElevModel";
            this.chkUseElevModel.Size = new System.Drawing.Size(117, 21);
            this.chkUseElevModel.TabIndex = 143;
            this.chkUseElevModel.Text = "Elevation Model";
            this.chkUseElevModel.UseVisualStyleBackColor = true;
            this.chkUseElevModel.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnShowMetTS_Info
            // 
            this.btnShowMetTS_Info.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnShowMetTS_Info.Location = new System.Drawing.Point(26, 239);
            this.btnShowMetTS_Info.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowMetTS_Info.Name = "btnShowMetTS_Info";
            this.btnShowMetTS_Info.Size = new System.Drawing.Size(30, 29);
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
            "DD",
            "UTM"});
            this.cboInputLLorDD.Location = new System.Drawing.Point(634, 27);
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
            this.cboWindOrEnergy.Location = new System.Drawing.Point(28, 451);
            this.cboWindOrEnergy.Name = "cboWindOrEnergy";
            this.cboWindOrEnergy.Size = new System.Drawing.Size(121, 21);
            this.cboWindOrEnergy.TabIndex = 137;
            this.cboWindOrEnergy.SelectedIndexChanged += new System.EventHandler(this.cboWindOrEnergy_SelectedIndexChanged);
            // 
            // btnEditAirDensity
            // 
            this.btnEditAirDensity.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditAirDensity.Location = new System.Drawing.Point(729, 138);
            this.btnEditAirDensity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditAirDensity.Name = "btnEditAirDensity";
            this.btnEditAirDensity.Size = new System.Drawing.Size(30, 29);
            this.btnEditAirDensity.TabIndex = 136;
            this.btnEditAirDensity.Text = "✎";
            this.btnEditAirDensity.UseVisualStyleBackColor = true;
            this.btnEditAirDensity.Click += new System.EventHandler(this.btnEditAirDensity_Click);
            // 
            // label222
            // 
            this.label222.AutoSize = true;
            this.label222.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label222.Location = new System.Drawing.Point(535, 143);
            this.label222.Name = "label222";
            this.label222.Size = new System.Drawing.Size(130, 18);
            this.label222.TabIndex = 135;
            this.label222.Text = "Air Density [kg/m3]:";
            this.label222.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtAirDensity
            // 
            this.txtAirDensity.Enabled = false;
            this.txtAirDensity.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAirDensity.Location = new System.Drawing.Point(671, 140);
            this.txtAirDensity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAirDensity.Name = "txtAirDensity";
            this.txtAirDensity.Size = new System.Drawing.Size(49, 25);
            this.txtAirDensity.TabIndex = 134;
            this.txtAirDensity.Text = "1.225";
            this.txtAirDensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnEditRotorDiam
            // 
            this.btnEditRotorDiam.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditRotorDiam.Location = new System.Drawing.Point(729, 107);
            this.btnEditRotorDiam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditRotorDiam.Name = "btnEditRotorDiam";
            this.btnEditRotorDiam.Size = new System.Drawing.Size(30, 29);
            this.btnEditRotorDiam.TabIndex = 133;
            this.btnEditRotorDiam.Text = "✎";
            this.btnEditRotorDiam.UseVisualStyleBackColor = true;
            this.btnEditRotorDiam.Click += new System.EventHandler(this.btnRotorDiam_Click);
            // 
            // label221
            // 
            this.label221.AutoSize = true;
            this.label221.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label221.Location = new System.Drawing.Point(557, 110);
            this.label221.Name = "label221";
            this.label221.Size = new System.Drawing.Size(108, 18);
            this.label221.TabIndex = 132;
            this.label221.Text = "Rotor Diam. [m]:";
            this.label221.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtRotorDiam
            // 
            this.txtRotorDiam.Enabled = false;
            this.txtRotorDiam.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRotorDiam.Location = new System.Drawing.Point(671, 107);
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
            this.txtTopoNullValue.Location = new System.Drawing.Point(90, 113);
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
            this.label5.Location = new System.Drawing.Point(25, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 129;
            this.label5.Text = "Null Value:";
            // 
            // plotInputWindRose
            // 
            this.plotInputWindRose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotInputWindRose.Location = new System.Drawing.Point(28, 481);
            this.plotInputWindRose.Name = "plotInputWindRose";
            this.plotInputWindRose.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotInputWindRose.Size = new System.Drawing.Size(223, 214);
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
            this.chkCreateTurbTS.Location = new System.Drawing.Point(497, 244);
            this.chkCreateTurbTS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkCreateTurbTS.Name = "chkCreateTurbTS";
            this.chkCreateTurbTS.Size = new System.Drawing.Size(95, 40);
            this.chkCreateTurbTS.TabIndex = 125;
            this.chkCreateTurbTS.Text = "Create \r\nTime Series";
            this.chkCreateTurbTS.UseVisualStyleBackColor = true;
            this.chkCreateTurbTS.CheckedChanged += new System.EventHandler(this.cboCreateTurbTS_CheckedChanged);
            // 
            // btnEditModHeight
            // 
            this.btnEditModHeight.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditModHeight.Location = new System.Drawing.Point(729, 76);
            this.btnEditModHeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditModHeight.Name = "btnEditModHeight";
            this.btnEditModHeight.Size = new System.Drawing.Size(30, 29);
            this.btnEditModHeight.TabIndex = 124;
            this.btnEditModHeight.Text = "✎";
            this.btnEditModHeight.UseVisualStyleBackColor = true;
            this.btnEditModHeight.Click += new System.EventHandler(this.btnModHeight_Click);
            // 
            // label168
            // 
            this.label168.AutoSize = true;
            this.label168.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label168.Location = new System.Drawing.Point(538, 81);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(130, 18);
            this.label168.TabIndex = 123;
            this.label168.Text = "Modeled Height [m]:";
            this.label168.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtModeledHeight
            // 
            this.txtModeledHeight.Enabled = false;
            this.txtModeledHeight.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModeledHeight.Location = new System.Drawing.Point(671, 78);
            this.txtModeledHeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtModeledHeight.Name = "txtModeledHeight";
            this.txtModeledHeight.Size = new System.Drawing.Size(49, 25);
            this.txtModeledHeight.TabIndex = 122;
            this.txtModeledHeight.Text = "80";
            this.txtModeledHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnImportRoughness
            // 
            this.btnImportRoughness.BackColor = System.Drawing.Color.LightCoral;
            this.btnImportRoughness.Font = new System.Drawing.Font("Palatino Linotype", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportRoughness.Location = new System.Drawing.Point(17, 139);
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
            this.chk_Use_Sep.Location = new System.Drawing.Point(350, 112);
            this.chk_Use_Sep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_Use_Sep.Name = "chk_Use_Sep";
            this.chk_Use_Sep.Size = new System.Drawing.Size(118, 21);
            this.chk_Use_Sep.TabIndex = 116;
            this.chk_Use_Sep.Text = "Flow Sep. Model";
            this.chk_Use_Sep.UseVisualStyleBackColor = true;
            this.chk_Use_Sep.CheckedChanged += new System.EventHandler(this.chk_Use_Sep_CheckedChanged);
            // 
            // Label84
            // 
            this.Label84.AutoSize = true;
            this.Label84.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label84.Location = new System.Drawing.Point(171, 119);
            this.Label84.Name = "Label84";
            this.Label84.Size = new System.Drawing.Size(152, 17);
            this.Label84.TabIndex = 114;
            this.Label84.Text = "Land Cover Key selected:";
            // 
            // txt_LC_Key_selected
            // 
            this.txt_LC_Key_selected.BackColor = System.Drawing.Color.LightCoral;
            this.txt_LC_Key_selected.Font = new System.Drawing.Font("Palatino Linotype", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LC_Key_selected.Location = new System.Drawing.Point(177, 141);
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
            this.chkUseSR.Location = new System.Drawing.Point(350, 89);
            this.chkUseSR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkUseSR.Name = "chkUseSR";
            this.chkUseSR.Size = new System.Drawing.Size(127, 21);
            this.chkUseSR.TabIndex = 112;
            this.chkUseSR.Text = "Roughness Model";
            this.chkUseSR.UseVisualStyleBackColor = true;
            this.chkUseSR.CheckedChanged += new System.EventHandler(this.chkUseSR_CheckedChanged);
            // 
            // btnViewModNLCD
            // 
            this.btnViewModNLCD.BackColor = System.Drawing.Color.LightCoral;
            this.btnViewModNLCD.Font = new System.Drawing.Font("Palatino Linotype", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewModNLCD.Location = new System.Drawing.Point(177, 59);
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
            this.txtUTMZone.Location = new System.Drawing.Point(477, 24);
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
            this.Label57.Location = new System.Drawing.Point(403, 28);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(74, 18);
            this.Label57.TabIndex = 104;
            this.Label57.Text = "UTM Zone:";
            // 
            // txtUTMDatum
            // 
            this.txtUTMDatum.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUTMDatum.Location = new System.Drawing.Point(262, 26);
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
            this.Label49.Location = new System.Drawing.Point(179, 29);
            this.Label49.Name = "Label49";
            this.Label49.Size = new System.Drawing.Size(85, 18);
            this.Label49.TabIndex = 102;
            this.Label49.Text = "UTM Datum:";
            // 
            // btnAnalyzeMets
            // 
            this.btnAnalyzeMets.BackColor = System.Drawing.Color.LightCoral;
            this.btnAnalyzeMets.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnAnalyzeMets.Location = new System.Drawing.Point(306, 238);
            this.btnAnalyzeMets.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAnalyzeMets.Name = "btnAnalyzeMets";
            this.btnAnalyzeMets.Size = new System.Drawing.Size(171, 49);
            this.btnAnalyzeMets.TabIndex = 101;
            this.btnAnalyzeMets.Text = "Create Site-Calibrated Wind Flow Model";
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
            // btnGenTurbEsts
            // 
            this.btnGenTurbEsts.BackColor = System.Drawing.Color.LightCoral;
            this.btnGenTurbEsts.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnGenTurbEsts.Location = new System.Drawing.Point(599, 238);
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
            this.Label30.Location = new System.Drawing.Point(27, 207);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(91, 23);
            this.Label30.TabIndex = 78;
            this.Label30.Text = "Met Sites";
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(483, 207);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(84, 23);
            this.Label19.TabIndex = 77;
            this.Label19.Text = "Turbines";
            // 
            // btnDelTurb
            // 
            this.btnDelTurb.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnDelTurb.Location = new System.Drawing.Point(745, 202);
            this.btnDelTurb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelTurb.Name = "btnDelTurb";
            this.btnDelTurb.Size = new System.Drawing.Size(30, 28);
            this.btnDelTurb.TabIndex = 30;
            this.btnDelTurb.Text = "✕";
            this.btnDelTurb.UseVisualStyleBackColor = true;
            this.btnDelTurb.Click += new System.EventHandler(this.btnDelTurb_Click);
            // 
            // btnDelMet
            // 
            this.btnDelMet.Font = new System.Drawing.Font("Palatino Linotype", 9.5F);
            this.btnDelMet.Location = new System.Drawing.Point(259, 202);
            this.btnDelMet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelMet.Name = "btnDelMet";
            this.btnDelMet.Size = new System.Drawing.Size(30, 28);
            this.btnDelMet.TabIndex = 19;
            this.btnDelMet.Text = "✕";
            this.btnDelMet.UseVisualStyleBackColor = true;
            this.btnDelMet.Click += new System.EventHandler(this.btnDelMet_Click);
            // 
            // btnTurbines
            // 
            this.btnTurbines.BackColor = System.Drawing.Color.LightCoral;
            this.btnTurbines.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.btnTurbines.Location = new System.Drawing.Point(599, 202);
            this.btnTurbines.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTurbines.Name = "btnTurbines";
            this.btnTurbines.Size = new System.Drawing.Size(141, 29);
            this.btnTurbines.TabIndex = 3;
            this.btnTurbines.Text = "Import Turbine Sites";
            this.btnTurbines.UseVisualStyleBackColor = false;
            this.btnTurbines.Click += new System.EventHandler(this.btnTurbines_Click);
            // 
            // btnLoadXYZ
            // 
            this.btnLoadXYZ.BackColor = System.Drawing.Color.LightCoral;
            this.btnLoadXYZ.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadXYZ.Location = new System.Drawing.Point(17, 59);
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
            this.dataMetTS.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataMetTS_CellFormatting);
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
            this.btnShowFilterFlags.Location = new System.Drawing.Point(442, 10);
            this.btnShowFilterFlags.Name = "btnShowFilterFlags";
            this.btnShowFilterFlags.Size = new System.Drawing.Size(74, 52);
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
            this.plotTS_Baros.Size = new System.Drawing.Size(854, 155);
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
            this.lblMetDataTS_Inc.Location = new System.Drawing.Point(523, 23);
            this.lblMetDataTS_Inc.Name = "lblMetDataTS_Inc";
            this.lblMetDataTS_Inc.Size = new System.Drawing.Size(38, 18);
            this.lblMetDataTS_Inc.TabIndex = 13;
            this.lblMetDataTS_Inc.Text = "Days";
            this.lblMetDataTS_Inc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtNumDaysTS
            // 
            this.txtNumDaysTS.Location = new System.Drawing.Point(458, 20);
            this.txtNumDaysTS.Name = "txtNumDaysTS";
            this.txtNumDaysTS.Size = new System.Drawing.Size(59, 25);
            this.txtNumDaysTS.TabIndex = 12;
            // 
            // btnMetTS_Left
            // 
            this.btnMetTS_Left.BackColor = System.Drawing.SystemColors.Control;
            this.btnMetTS_Left.Location = new System.Drawing.Point(406, 18);
            this.btnMetTS_Left.Name = "btnMetTS_Left";
            this.btnMetTS_Left.Size = new System.Drawing.Size(39, 28);
            this.btnMetTS_Left.TabIndex = 11;
            this.btnMetTS_Left.Text = "◀";
            this.btnMetTS_Left.UseVisualStyleBackColor = false;
            this.btnMetTS_Left.Click += new System.EventHandler(this.btnMetTS_Left_Click);
            // 
            // btnMetTS_Right
            // 
            this.btnMetTS_Right.BackColor = System.Drawing.SystemColors.Control;
            this.btnMetTS_Right.Location = new System.Drawing.Point(567, 18);
            this.btnMetTS_Right.Name = "btnMetTS_Right";
            this.btnMetTS_Right.Size = new System.Drawing.Size(39, 28);
            this.btnMetTS_Right.TabIndex = 10;
            this.btnMetTS_Right.Text = "▶";
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
            this.plotTS_Temp.Size = new System.Drawing.Size(854, 155);
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
            this.plotTS_Vanes.Size = new System.Drawing.Size(854, 155);
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
            this.plotTS_Anems.Size = new System.Drawing.Size(854, 155);
            this.plotTS_Anems.TabIndex = 0;
            this.plotTS_Anems.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotTS_Anems.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotTS_Anems.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // pgeMetData
            // 
            this.pgeMetData.Controls.Add(this.dataAlphas);
            this.pgeMetData.Controls.Add(this.dataExtrapolated);
            this.pgeMetData.Controls.Add(this.dataTempSummary);
            this.pgeMetData.Controls.Add(this.dataVaneSummary);
            this.pgeMetData.Controls.Add(this.dataAnemSummary);
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
            this.pgeMetData.Controls.Add(this.label68);
            this.pgeMetData.Controls.Add(this.label67);
            this.pgeMetData.Controls.Add(this.label66);
            this.pgeMetData.Controls.Add(this.label65);
            this.pgeMetData.Controls.Add(this.cboMetWindRose);
            this.pgeMetData.Controls.Add(this.label64);
            this.pgeMetData.Controls.Add(this.label62);
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
            this.pgeMetData.Location = new System.Drawing.Point(4, 27);
            this.pgeMetData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeMetData.Name = "pgeMetData";
            this.pgeMetData.Size = new System.Drawing.Size(1488, 712);
            this.pgeMetData.TabIndex = 14;
            this.pgeMetData.Text = "Met Data QC";
            this.pgeMetData.UseVisualStyleBackColor = true;
            // 
            // dataAlphas
            // 
            this.dataAlphas.AllowUserToAddRows = false;
            this.dataAlphas.AllowUserToDeleteRows = false;
            this.dataAlphas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataAlphas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataAlphas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.Column43});
            this.dataAlphas.Location = new System.Drawing.Point(825, 130);
            this.dataAlphas.Name = "dataAlphas";
            this.dataAlphas.ReadOnly = true;
            this.dataAlphas.RowHeadersVisible = false;
            this.dataAlphas.Size = new System.Drawing.Size(215, 296);
            this.dataAlphas.TabIndex = 260;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "WD";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 60;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "All Hrs";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 60;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "Day";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 50;
            // 
            // Column43
            // 
            this.Column43.HeaderText = "Night";
            this.Column43.Name = "Column43";
            this.Column43.ReadOnly = true;
            this.Column43.Width = 50;
            // 
            // dataExtrapolated
            // 
            this.dataExtrapolated.AllowUserToAddRows = false;
            this.dataExtrapolated.AllowUserToDeleteRows = false;
            this.dataExtrapolated.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataExtrapolated.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn10,
            this.Column42});
            this.dataExtrapolated.Location = new System.Drawing.Point(466, 286);
            this.dataExtrapolated.Name = "dataExtrapolated";
            this.dataExtrapolated.ReadOnly = true;
            this.dataExtrapolated.RowHeadersVisible = false;
            this.dataExtrapolated.Size = new System.Drawing.Size(225, 113);
            this.dataExtrapolated.TabIndex = 259;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Height [m]";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 60;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "% Rec.";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 60;
            // 
            // Column42
            // 
            this.Column42.HeaderText = "Avg. WS [m/s]";
            this.Column42.Name = "Column42";
            this.Column42.ReadOnly = true;
            // 
            // dataTempSummary
            // 
            this.dataTempSummary.AllowUserToAddRows = false;
            this.dataTempSummary.AllowUserToDeleteRows = false;
            this.dataTempSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTempSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dataTempSummary.Location = new System.Drawing.Point(327, 286);
            this.dataTempSummary.Name = "dataTempSummary";
            this.dataTempSummary.ReadOnly = true;
            this.dataTempSummary.RowHeadersVisible = false;
            this.dataTempSummary.Size = new System.Drawing.Size(133, 113);
            this.dataTempSummary.TabIndex = 258;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Height [m]";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 60;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "% Rec.";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 60;
            // 
            // dataVaneSummary
            // 
            this.dataVaneSummary.AllowUserToAddRows = false;
            this.dataVaneSummary.AllowUserToDeleteRows = false;
            this.dataVaneSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataVaneSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn14});
            this.dataVaneSummary.Location = new System.Drawing.Point(30, 286);
            this.dataVaneSummary.Name = "dataVaneSummary";
            this.dataVaneSummary.ReadOnly = true;
            this.dataVaneSummary.RowHeadersVisible = false;
            this.dataVaneSummary.Size = new System.Drawing.Size(289, 113);
            this.dataVaneSummary.TabIndex = 257;
            this.dataVaneSummary.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataVaneSummary_CellContentClick);
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Height [m]";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "% Rec.";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 60;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "% Iced";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 60;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "% Overall Rec";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            // 
            // dataAnemSummary
            // 
            this.dataAnemSummary.AllowUserToAddRows = false;
            this.dataAnemSummary.AllowUserToDeleteRows = false;
            this.dataAnemSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataAnemSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataAnemSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Column31,
            this.Column32,
            this.Column36,
            this.Column37,
            this.Column38,
            this.Column39,
            this.Column40,
            this.Column41});
            this.dataAnemSummary.Location = new System.Drawing.Point(28, 91);
            this.dataAnemSummary.Name = "dataAnemSummary";
            this.dataAnemSummary.ReadOnly = true;
            this.dataAnemSummary.RowHeadersVisible = false;
            this.dataAnemSummary.Size = new System.Drawing.Size(781, 160);
            this.dataAnemSummary.TabIndex = 256;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "H [m]";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Orient [degs]";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Unfilt. WS [m/s]";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // Column31
            // 
            this.Column31.HeaderText = "Filt. WS [m/s]";
            this.Column31.Name = "Column31";
            this.Column31.ReadOnly = true;
            // 
            // Column32
            // 
            this.Column32.HeaderText = "% Rec.";
            this.Column32.Name = "Column32";
            this.Column32.ReadOnly = true;
            this.Column32.Width = 50;
            // 
            // Column36
            // 
            this.Column36.HeaderText = "% Iced";
            this.Column36.Name = "Column36";
            this.Column36.ReadOnly = true;
            this.Column36.Width = 50;
            // 
            // Column37
            // 
            this.Column37.HeaderText = "% Twr Shadow";
            this.Column37.Name = "Column37";
            this.Column37.ReadOnly = true;
            this.Column37.Width = 60;
            // 
            // Column38
            // 
            this.Column38.HeaderText = "% Min WS";
            this.Column38.Name = "Column38";
            this.Column38.ReadOnly = true;
            this.Column38.Width = 80;
            // 
            // Column39
            // 
            this.Column39.HeaderText = "% SD";
            this.Column39.Name = "Column39";
            this.Column39.ReadOnly = true;
            this.Column39.Width = 50;
            // 
            // Column40
            // 
            this.Column40.HeaderText = "% Max Rng";
            this.Column40.Name = "Column40";
            this.Column40.ReadOnly = true;
            this.Column40.Width = 60;
            // 
            // Column41
            // 
            this.Column41.HeaderText = "% Overall Rec";
            this.Column41.Name = "Column41";
            this.Column41.ReadOnly = true;
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
            this.label69.Location = new System.Drawing.Point(21, 414);
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
            this.plotWSDiffByWD.Location = new System.Drawing.Point(456, 469);
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
            this.btnEditShearMethod.Location = new System.Drawing.Point(993, 34);
            this.btnEditShearMethod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditShearMethod.Name = "btnEditShearMethod";
            this.btnEditShearMethod.Size = new System.Drawing.Size(46, 35);
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
            this.chkMaxWS_Range.Location = new System.Drawing.Point(710, 383);
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
            this.chkMaxWS_SD.Location = new System.Drawing.Point(710, 363);
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
            this.chkMinWS_SD.Location = new System.Drawing.Point(710, 343);
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
            this.chkMinWS.Location = new System.Drawing.Point(710, 323);
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
            this.chkIcing.Location = new System.Drawing.Point(710, 303);
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
            this.chkTowerShadow.Location = new System.Drawing.Point(710, 283);
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
            this.cboSelVane.Location = new System.Drawing.Point(398, 440);
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
            this.label184.Location = new System.Drawing.Point(353, 444);
            this.label184.Name = "label184";
            this.label184.Size = new System.Drawing.Size(38, 18);
            this.label184.TabIndex = 229;
            this.label184.Text = "Vane";
            // 
            // cboAnemB
            // 
            this.cboAnemB.FormattingEnabled = true;
            this.cboAnemB.Location = new System.Drawing.Point(252, 440);
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
            this.label155.Location = new System.Drawing.Point(191, 445);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(55, 18);
            this.label155.TabIndex = 227;
            this.label155.Text = "Anem B";
            // 
            // cboAnemA
            // 
            this.cboAnemA.FormattingEnabled = true;
            this.cboAnemA.Location = new System.Drawing.Point(96, 440);
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
            this.label20.Location = new System.Drawing.Point(34, 444);
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
            this.chkDisableFilter.Location = new System.Drawing.Point(705, 259);
            this.chkDisableFilter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkDisableFilter.Name = "chkDisableFilter";
            this.chkDisableFilter.Size = new System.Drawing.Size(129, 22);
            this.chkDisableFilter.TabIndex = 217;
            this.chkDisableFilter.Text = "Filtering Enabled";
            this.chkDisableFilter.UseVisualStyleBackColor = true;
            this.chkDisableFilter.CheckedChanged += new System.EventHandler(this.chkDisableFilter_CheckedChanged);
            // 
            // btnViewFilters
            // 
            this.btnViewFilters.Location = new System.Drawing.Point(705, 407);
            this.btnViewFilters.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnViewFilters.Name = "btnViewFilters";
            this.btnViewFilters.Size = new System.Drawing.Size(108, 28);
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
            this.btnExportAnnualMax.Location = new System.Drawing.Point(1329, 656);
            this.btnExportAnnualMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportAnnualMax.Name = "btnExportAnnualMax";
            this.btnExportAnnualMax.Size = new System.Drawing.Size(153, 30);
            this.btnExportAnnualMax.TabIndex = 211;
            this.btnExportAnnualMax.Text = "Export Annual Max";
            this.btnExportAnnualMax.UseVisualStyleBackColor = true;
            this.btnExportAnnualMax.Click += new System.EventHandler(this.btnExportAnnualMax_Click);
            // 
            // btnExportExtrap
            // 
            this.btnExportExtrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportExtrap.Location = new System.Drawing.Point(1329, 614);
            this.btnExportExtrap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportExtrap.Name = "btnExportExtrap";
            this.btnExportExtrap.Size = new System.Drawing.Size(153, 30);
            this.btnExportExtrap.TabIndex = 210;
            this.btnExportExtrap.Text = "Export Extrap. Data";
            this.btnExportExtrap.UseVisualStyleBackColor = true;
            this.btnExportExtrap.Click += new System.EventHandler(this.btnExportExtrap_Click);
            // 
            // btnExportAlpha
            // 
            this.btnExportAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAlpha.Location = new System.Drawing.Point(1329, 571);
            this.btnExportAlpha.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportAlpha.Name = "btnExportAlpha";
            this.btnExportAlpha.Size = new System.Drawing.Size(153, 30);
            this.btnExportAlpha.TabIndex = 209;
            this.btnExportAlpha.Text = "Export Alpha";
            this.btnExportAlpha.UseVisualStyleBackColor = true;
            this.btnExportAlpha.Click += new System.EventHandler(this.btnExportAlpha_Click);
            // 
            // btnExportFlags
            // 
            this.btnExportFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportFlags.Location = new System.Drawing.Point(1329, 529);
            this.btnExportFlags.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportFlags.Name = "btnExportFlags";
            this.btnExportFlags.Size = new System.Drawing.Size(153, 30);
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
            this.cboFilt_or_Not.Location = new System.Drawing.Point(358, 411);
            this.cboFilt_or_Not.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboFilt_or_Not.Name = "cboFilt_or_Not";
            this.cboFilt_or_Not.Size = new System.Drawing.Size(101, 26);
            this.cboFilt_or_Not.TabIndex = 205;
            this.cboFilt_or_Not.Text = "Unfiltered";
            this.cboFilt_or_Not.SelectedIndexChanged += new System.EventHandler(this.cboFilt_or_Not_SelectedIndexChanged);
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
            // pgeMERRA
            // 
            this.pgeMERRA.AutoScroll = true;
            this.pgeMERRA.Controls.Add(this.splotLTRef_1);
            this.pgeMERRA.Controls.Add(this.cboRefWindOrEnergy);
            this.pgeMERRA.Controls.Add(this.btnRefDataDownloads);
            this.pgeMERRA.Controls.Add(this.btnExplainMERRA2Tab);
            this.pgeMERRA.Controls.Add(this.btnImportCRV_MERRA);
            this.pgeMERRA.Controls.Add(this.label121);
            this.pgeMERRA.Controls.Add(this.cboReferenceMonth);
            this.pgeMERRA.Controls.Add(this.label120);
            this.pgeMERRA.Controls.Add(this.cboReferenceYear);
            this.pgeMERRA.Controls.Add(this.label160);
            this.pgeMERRA.Controls.Add(this.cboMERRA_PowerCurves);
            this.pgeMERRA.Controls.Add(this.label159);
            this.pgeMERRA.Controls.Add(this.label156);
            this.pgeMERRA.Controls.Add(this.cboMERRA_PlotParam);
            this.pgeMERRA.Controls.Add(this.label151);
            this.pgeMERRA.Location = new System.Drawing.Point(4, 27);
            this.pgeMERRA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeMERRA.Name = "pgeMERRA";
            this.pgeMERRA.Size = new System.Drawing.Size(1488, 712);
            this.pgeMERRA.TabIndex = 16;
            this.pgeMERRA.Text = "LT Reference Data";
            this.pgeMERRA.UseVisualStyleBackColor = true;
            // 
            // splotLTRef_1
            // 
            this.splotLTRef_1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splotLTRef_1.Location = new System.Drawing.Point(11, 82);
            this.splotLTRef_1.Name = "splotLTRef_1";
            // 
            // splotLTRef_1.Panel1
            // 
            this.splotLTRef_1.Panel1.Controls.Add(this.btnDelRef);
            this.splotLTRef_1.Panel1.Controls.Add(this.btnEditReference);
            this.splotLTRef_1.Panel1.Controls.Add(this.btnAddNew);
            this.splotLTRef_1.Panel1.Controls.Add(this.label207);
            this.splotLTRef_1.Panel1.Controls.Add(this.cboLTReferences);
            this.splotLTRef_1.Panel1.Controls.Add(this.dateMERRAEnd);
            this.splotLTRef_1.Panel1.Controls.Add(this.dateMERRAStart);
            this.splotLTRef_1.Panel1.Controls.Add(this.label152);
            this.splotLTRef_1.Panel1.Controls.Add(this.label153);
            this.splotLTRef_1.Panel1.Controls.Add(this.btnExportCloudCover);
            this.splotLTRef_1.Panel1.Controls.Add(this.txtRefDataDownloadName);
            this.splotLTRef_1.Panel1.Controls.Add(this.label216);
            this.splotLTRef_1.Panel1.Controls.Add(this.txtRefDataDownloadFolder);
            this.splotLTRef_1.Panel1.Controls.Add(this.btn_ExportWR);
            this.splotLTRef_1.Panel1.Controls.Add(this.btn_Export_All_Months_All_Years);
            this.splotLTRef_1.Panel1.Controls.Add(this.btn_Export_Interp);
            this.splotLTRef_1.Panel1.Controls.Add(this.label154);
            this.splotLTRef_1.Panel1.Controls.Add(this.txtLT_Avg_Prod);
            this.splotLTRef_1.Panel1.Controls.Add(this.txtLT_Avg_CF);
            this.splotLTRef_1.Panel1.Controls.Add(this.txtLT_Avg_WS);
            this.splotLTRef_1.Panel1.Controls.Add(this.label162);
            this.splotLTRef_1.Panel1.Controls.Add(this.label131);
            this.splotLTRef_1.Panel1.Controls.Add(this.label87);
            this.splotLTRef_1.Panel1.Controls.Add(this.label217);
            this.splotLTRef_1.Panel1.Controls.Add(this.txtMaxLong);
            this.splotLTRef_1.Panel1.Controls.Add(this.label218);
            this.splotLTRef_1.Panel1.Controls.Add(this.txtMinLong);
            this.splotLTRef_1.Panel1.Controls.Add(this.label219);
            this.splotLTRef_1.Panel1.Controls.Add(this.txtMaxLat);
            this.splotLTRef_1.Panel1.Controls.Add(this.label220);
            this.splotLTRef_1.Panel1.Controls.Add(this.txtMinLat);
            this.splotLTRef_1.Panel1.Controls.Add(this.label205);
            this.splotLTRef_1.Panel1.Controls.Add(this.txtRefDataAvail);
            this.splotLTRef_1.Panel1.Controls.Add(this.label204);
            this.splotLTRef_1.Panel1.Controls.Add(this.dateLTRefAvailEnd);
            this.splotLTRef_1.Panel1.Controls.Add(this.dateLTRefAvailStart);
            this.splotLTRef_1.Panel1.Controls.Add(this.label52);
            this.splotLTRef_1.Panel1.Controls.Add(this.label54);
            // 
            // splotLTRef_1.Panel2
            // 
            this.splotLTRef_1.Panel2.Controls.Add(this.splitContainer1);
            this.splotLTRef_1.Size = new System.Drawing.Size(1465, 626);
            this.splotLTRef_1.SplitterDistance = 430;
            this.splotLTRef_1.TabIndex = 275;
            // 
            // btnDelRef
            // 
            this.btnDelRef.Font = new System.Drawing.Font("Palatino Linotype", 10F);
            this.btnDelRef.Location = new System.Drawing.Point(383, 9);
            this.btnDelRef.Name = "btnDelRef";
            this.btnDelRef.Size = new System.Drawing.Size(30, 29);
            this.btnDelRef.TabIndex = 306;
            this.btnDelRef.Text = "✕";
            this.btnDelRef.UseVisualStyleBackColor = true;
            this.btnDelRef.Click += new System.EventHandler(this.btnDelRef_Click);
            // 
            // btnEditReference
            // 
            this.btnEditReference.Font = new System.Drawing.Font("Palatino Linotype", 10F);
            this.btnEditReference.Location = new System.Drawing.Point(349, 9);
            this.btnEditReference.Name = "btnEditReference";
            this.btnEditReference.Size = new System.Drawing.Size(30, 29);
            this.btnEditReference.TabIndex = 305;
            this.btnEditReference.Text = "✎";
            this.btnEditReference.UseVisualStyleBackColor = true;
            this.btnEditReference.Click += new System.EventHandler(this.btnEditReference_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Font = new System.Drawing.Font("Palatino Linotype", 10F);
            this.btnAddNew.Location = new System.Drawing.Point(315, 9);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(30, 29);
            this.btnAddNew.TabIndex = 304;
            this.btnAddNew.Text = "+";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // label207
            // 
            this.label207.AutoSize = true;
            this.label207.Location = new System.Drawing.Point(23, 19);
            this.label207.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label207.Name = "label207";
            this.label207.Size = new System.Drawing.Size(98, 19);
            this.label207.TabIndex = 303;
            this.label207.Text = "Reference Site:";
            // 
            // cboLTReferences
            // 
            this.cboLTReferences.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLTReferences.FormattingEnabled = true;
            this.cboLTReferences.Location = new System.Drawing.Point(21, 45);
            this.cboLTReferences.Margin = new System.Windows.Forms.Padding(2);
            this.cboLTReferences.Name = "cboLTReferences";
            this.cboLTReferences.Size = new System.Drawing.Size(392, 25);
            this.cboLTReferences.TabIndex = 302;
            this.cboLTReferences.SelectedIndexChanged += new System.EventHandler(this.cboLTReferences_SelectedIndexChanged);
            // 
            // dateMERRAEnd
            // 
            this.dateMERRAEnd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAEnd.CausesValidation = false;
            this.dateMERRAEnd.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateMERRAEnd.Enabled = false;
            this.dateMERRAEnd.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMERRAEnd.Location = new System.Drawing.Point(119, 111);
            this.dateMERRAEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dateMERRAEnd.MaxDate = new System.DateTime(2050, 12, 1, 0, 0, 0, 0);
            this.dateMERRAEnd.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dateMERRAEnd.Name = "dateMERRAEnd";
            this.dateMERRAEnd.Size = new System.Drawing.Size(163, 25);
            this.dateMERRAEnd.TabIndex = 301;
            this.dateMERRAEnd.Value = new System.DateTime(2022, 12, 31, 23, 0, 0, 0);
            // 
            // dateMERRAStart
            // 
            this.dateMERRAStart.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAStart.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateMERRAStart.Enabled = false;
            this.dateMERRAStart.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMERRAStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMERRAStart.Location = new System.Drawing.Point(119, 77);
            this.dateMERRAStart.Margin = new System.Windows.Forms.Padding(2);
            this.dateMERRAStart.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dateMERRAStart.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dateMERRAStart.Name = "dateMERRAStart";
            this.dateMERRAStart.Size = new System.Drawing.Size(163, 25);
            this.dateMERRAStart.TabIndex = 300;
            this.dateMERRAStart.Value = new System.DateTime(2002, 1, 1, 0, 0, 0, 0);
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Enabled = false;
            this.label152.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label152.Location = new System.Drawing.Point(31, 110);
            this.label152.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(74, 18);
            this.label152.TabIndex = 299;
            this.label152.Text = "End (Local)";
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Enabled = false;
            this.label153.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label153.Location = new System.Drawing.Point(30, 83);
            this.label153.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(81, 18);
            this.label153.TabIndex = 298;
            this.label153.Text = "Start (Local)";
            // 
            // btnExportCloudCover
            // 
            this.btnExportCloudCover.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCloudCover.Location = new System.Drawing.Point(155, 319);
            this.btnExportCloudCover.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportCloudCover.Name = "btnExportCloudCover";
            this.btnExportCloudCover.Size = new System.Drawing.Size(128, 48);
            this.btnExportCloudCover.TabIndex = 297;
            this.btnExportCloudCover.Text = "Export Cloud Cover (MERRA2)";
            this.btnExportCloudCover.UseVisualStyleBackColor = true;
            this.btnExportCloudCover.Click += new System.EventHandler(this.btnExportCloudCover_Click);
            // 
            // txtRefDataDownloadName
            // 
            this.txtRefDataDownloadName.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefDataDownloadName.Location = new System.Drawing.Point(18, 402);
            this.txtRefDataDownloadName.Margin = new System.Windows.Forms.Padding(2);
            this.txtRefDataDownloadName.Name = "txtRefDataDownloadName";
            this.txtRefDataDownloadName.ReadOnly = true;
            this.txtRefDataDownloadName.Size = new System.Drawing.Size(399, 25);
            this.txtRefDataDownloadName.TabIndex = 296;
            // 
            // label216
            // 
            this.label216.AutoSize = true;
            this.label216.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.label216.Location = new System.Drawing.Point(12, 381);
            this.label216.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label216.Name = "label216";
            this.label216.Size = new System.Drawing.Size(202, 17);
            this.label216.TabIndex = 295;
            this.label216.Text = "Extracted from Reference Dataset:";
            // 
            // txtRefDataDownloadFolder
            // 
            this.txtRefDataDownloadFolder.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefDataDownloadFolder.Location = new System.Drawing.Point(18, 452);
            this.txtRefDataDownloadFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtRefDataDownloadFolder.Name = "txtRefDataDownloadFolder";
            this.txtRefDataDownloadFolder.ReadOnly = true;
            this.txtRefDataDownloadFolder.Size = new System.Drawing.Size(399, 25);
            this.txtRefDataDownloadFolder.TabIndex = 294;
            // 
            // btn_ExportWR
            // 
            this.btn_ExportWR.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ExportWR.Location = new System.Drawing.Point(23, 319);
            this.btn_ExportWR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_ExportWR.Name = "btn_ExportWR";
            this.btn_ExportWR.Size = new System.Drawing.Size(128, 48);
            this.btn_ExportWR.TabIndex = 293;
            this.btn_ExportWR.Text = "Export Wind Rose";
            this.btn_ExportWR.UseVisualStyleBackColor = true;
            this.btn_ExportWR.Click += new System.EventHandler(this.btn_ExportWR_Click);
            // 
            // btn_Export_All_Months_All_Years
            // 
            this.btn_Export_All_Months_All_Years.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export_All_Months_All_Years.Location = new System.Drawing.Point(155, 264);
            this.btn_Export_All_Months_All_Years.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Export_All_Months_All_Years.Name = "btn_Export_All_Months_All_Years";
            this.btn_Export_All_Months_All_Years.Size = new System.Drawing.Size(128, 48);
            this.btn_Export_All_Months_All_Years.TabIndex = 292;
            this.btn_Export_All_Months_All_Years.Text = "Export All Months && All Years";
            this.btn_Export_All_Months_All_Years.UseVisualStyleBackColor = true;
            this.btn_Export_All_Months_All_Years.Click += new System.EventHandler(this.btn_Export_All_Months_All_Years_Click);
            // 
            // btn_Export_Interp
            // 
            this.btn_Export_Interp.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export_Interp.Location = new System.Drawing.Point(23, 264);
            this.btn_Export_Interp.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Export_Interp.Name = "btn_Export_Interp";
            this.btn_Export_Interp.Size = new System.Drawing.Size(129, 48);
            this.btn_Export_Interp.TabIndex = 291;
            this.btn_Export_Interp.Text = "Export Hourly Data";
            this.btn_Export_Interp.UseVisualStyleBackColor = true;
            this.btn_Export_Interp.Click += new System.EventHandler(this.btn_Export_Interp_Click);
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Enabled = false;
            this.label154.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label154.Location = new System.Drawing.Point(19, 430);
            this.label154.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(94, 17);
            this.label154.TabIndex = 290;
            this.label154.Text = "Folder location:";
            // 
            // txtLT_Avg_Prod
            // 
            this.txtLT_Avg_Prod.Location = new System.Drawing.Point(206, 182);
            this.txtLT_Avg_Prod.Name = "txtLT_Avg_Prod";
            this.txtLT_Avg_Prod.Size = new System.Drawing.Size(76, 25);
            this.txtLT_Avg_Prod.TabIndex = 289;
            // 
            // txtLT_Avg_CF
            // 
            this.txtLT_Avg_CF.Location = new System.Drawing.Point(206, 219);
            this.txtLT_Avg_CF.Name = "txtLT_Avg_CF";
            this.txtLT_Avg_CF.Size = new System.Drawing.Size(76, 25);
            this.txtLT_Avg_CF.TabIndex = 288;
            // 
            // txtLT_Avg_WS
            // 
            this.txtLT_Avg_WS.Location = new System.Drawing.Point(206, 145);
            this.txtLT_Avg_WS.Name = "txtLT_Avg_WS";
            this.txtLT_Avg_WS.Size = new System.Drawing.Size(76, 25);
            this.txtLT_Avg_WS.TabIndex = 287;
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.label162.Location = new System.Drawing.Point(14, 182);
            this.label162.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(181, 22);
            this.label162.TabIndex = 286;
            this.label162.Text = "Avg Gross AEP [MWh]:";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.label131.Location = new System.Drawing.Point(52, 219);
            this.label131.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(143, 22);
            this.label131.TabIndex = 285;
            this.label131.Text = "Avg Gross CF [%]:";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.label87.Location = new System.Drawing.Point(23, 145);
            this.label87.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(172, 22);
            this.label87.TabIndex = 284;
            this.label87.Text = "Avg Annual WS [m/s]:";
            // 
            // label217
            // 
            this.label217.AutoSize = true;
            this.label217.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label217.Location = new System.Drawing.Point(325, 518);
            this.label217.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label217.Name = "label217";
            this.label217.Size = new System.Drawing.Size(20, 18);
            this.label217.TabIndex = 283;
            this.label217.Text = "to";
            // 
            // txtMaxLong
            // 
            this.txtMaxLong.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxLong.Location = new System.Drawing.Point(346, 515);
            this.txtMaxLong.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaxLong.Name = "txtMaxLong";
            this.txtMaxLong.ReadOnly = true;
            this.txtMaxLong.Size = new System.Drawing.Size(44, 25);
            this.txtMaxLong.TabIndex = 282;
            // 
            // label218
            // 
            this.label218.AutoSize = true;
            this.label218.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label218.Location = new System.Drawing.Point(198, 518);
            this.label218.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label218.Name = "label218";
            this.label218.Size = new System.Drawing.Size(78, 18);
            this.label218.TabIndex = 281;
            this.label218.Text = "Long range:";
            // 
            // txtMinLong
            // 
            this.txtMinLong.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinLong.Location = new System.Drawing.Point(278, 515);
            this.txtMinLong.Margin = new System.Windows.Forms.Padding(2);
            this.txtMinLong.Name = "txtMinLong";
            this.txtMinLong.ReadOnly = true;
            this.txtMinLong.Size = new System.Drawing.Size(44, 25);
            this.txtMinLong.TabIndex = 280;
            // 
            // label219
            // 
            this.label219.AutoSize = true;
            this.label219.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label219.Location = new System.Drawing.Point(325, 485);
            this.label219.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label219.Name = "label219";
            this.label219.Size = new System.Drawing.Size(20, 18);
            this.label219.TabIndex = 279;
            this.label219.Text = "to";
            // 
            // txtMaxLat
            // 
            this.txtMaxLat.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxLat.Location = new System.Drawing.Point(346, 482);
            this.txtMaxLat.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaxLat.Name = "txtMaxLat";
            this.txtMaxLat.ReadOnly = true;
            this.txtMaxLat.Size = new System.Drawing.Size(44, 25);
            this.txtMaxLat.TabIndex = 278;
            // 
            // label220
            // 
            this.label220.AutoSize = true;
            this.label220.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label220.Location = new System.Drawing.Point(206, 485);
            this.label220.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label220.Name = "label220";
            this.label220.Size = new System.Drawing.Size(68, 18);
            this.label220.TabIndex = 277;
            this.label220.Text = "Lat range:";
            // 
            // txtMinLat
            // 
            this.txtMinLat.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinLat.Location = new System.Drawing.Point(278, 482);
            this.txtMinLat.Margin = new System.Windows.Forms.Padding(2);
            this.txtMinLat.Name = "txtMinLat";
            this.txtMinLat.ReadOnly = true;
            this.txtMinLat.Size = new System.Drawing.Size(44, 25);
            this.txtMinLat.TabIndex = 276;
            // 
            // label205
            // 
            this.label205.AutoSize = true;
            this.label205.Enabled = false;
            this.label205.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label205.Location = new System.Drawing.Point(241, 555);
            this.label205.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label205.Name = "label205";
            this.label205.Size = new System.Drawing.Size(95, 18);
            this.label205.TabIndex = 275;
            this.label205.Text = "Complete [%] :";
            // 
            // txtRefDataAvail
            // 
            this.txtRefDataAvail.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefDataAvail.Location = new System.Drawing.Point(339, 552);
            this.txtRefDataAvail.Margin = new System.Windows.Forms.Padding(2);
            this.txtRefDataAvail.Name = "txtRefDataAvail";
            this.txtRefDataAvail.ReadOnly = true;
            this.txtRefDataAvail.Size = new System.Drawing.Size(66, 25);
            this.txtRefDataAvail.TabIndex = 274;
            // 
            // label204
            // 
            this.label204.AutoSize = true;
            this.label204.Enabled = false;
            this.label204.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label204.Location = new System.Drawing.Point(16, 479);
            this.label204.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label204.Name = "label204";
            this.label204.Size = new System.Drawing.Size(175, 36);
            this.label204.TabIndex = 273;
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
            this.dateLTRefAvailEnd.Location = new System.Drawing.Point(19, 589);
            this.dateLTRefAvailEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dateLTRefAvailEnd.MaxDate = new System.DateTime(2050, 12, 1, 0, 0, 0, 0);
            this.dateLTRefAvailEnd.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dateLTRefAvailEnd.Name = "dateLTRefAvailEnd";
            this.dateLTRefAvailEnd.Size = new System.Drawing.Size(163, 25);
            this.dateLTRefAvailEnd.TabIndex = 272;
            this.dateLTRefAvailEnd.Value = new System.DateTime(2022, 12, 31, 23, 0, 0, 0);
            // 
            // dateLTRefAvailStart
            // 
            this.dateLTRefAvailStart.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLTRefAvailStart.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateLTRefAvailStart.Enabled = false;
            this.dateLTRefAvailStart.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLTRefAvailStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateLTRefAvailStart.Location = new System.Drawing.Point(18, 538);
            this.dateLTRefAvailStart.Margin = new System.Windows.Forms.Padding(2);
            this.dateLTRefAvailStart.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dateLTRefAvailStart.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dateLTRefAvailStart.Name = "dateLTRefAvailStart";
            this.dateLTRefAvailStart.Size = new System.Drawing.Size(163, 25);
            this.dateLTRefAvailStart.TabIndex = 271;
            this.dateLTRefAvailStart.Value = new System.DateTime(2002, 1, 1, 0, 0, 0, 0);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Enabled = false;
            this.label52.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(16, 569);
            this.label52.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(74, 18);
            this.label52.TabIndex = 270;
            this.label52.Text = "End (Local)";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Enabled = false;
            this.label54.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(17, 518);
            this.label54.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(81, 18);
            this.label54.TabIndex = 269;
            this.label54.Text = "Start (Local)";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1031, 626);
            this.splitContainer1.SplitterDistance = 309;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dataRefAnnualProd);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(1031, 309);
            this.splitContainer3.SplitterDistance = 307;
            this.splitContainer3.TabIndex = 0;
            // 
            // dataRefAnnualProd
            // 
            this.dataRefAnnualProd.AllowUserToAddRows = false;
            this.dataRefAnnualProd.AllowUserToDeleteRows = false;
            this.dataRefAnnualProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataRefAnnualProd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column44,
            this.Column45,
            this.Column46});
            this.dataRefAnnualProd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataRefAnnualProd.Location = new System.Drawing.Point(0, 0);
            this.dataRefAnnualProd.Name = "dataRefAnnualProd";
            this.dataRefAnnualProd.ReadOnly = true;
            this.dataRefAnnualProd.RowHeadersVisible = false;
            this.dataRefAnnualProd.Size = new System.Drawing.Size(307, 309);
            this.dataRefAnnualProd.TabIndex = 274;
            // 
            // Column44
            // 
            this.Column44.HeaderText = "Year";
            this.Column44.Name = "Column44";
            this.Column44.ReadOnly = true;
            this.Column44.Width = 80;
            // 
            // Column45
            // 
            this.Column45.HeaderText = "WS [m/s]";
            this.Column45.Name = "Column45";
            this.Column45.ReadOnly = true;
            // 
            // Column46
            // 
            this.Column46.HeaderText = "Diff. from LT";
            this.Column46.Name = "Column46";
            this.Column46.ReadOnly = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.dataRefMonthlyProd);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.cboNumWDRefTab);
            this.splitContainer4.Panel2.Controls.Add(this.label223);
            this.splitContainer4.Panel2.Controls.Add(this.chkYearsToDisplayAll);
            this.splitContainer4.Panel2.Controls.Add(this.chkYearsToDisplay);
            this.splitContainer4.Panel2.Controls.Add(this.plotMERRA_WindRose);
            this.splitContainer4.Size = new System.Drawing.Size(720, 309);
            this.splitContainer4.SplitterDistance = 352;
            this.splitContainer4.TabIndex = 0;
            // 
            // dataRefMonthlyProd
            // 
            this.dataRefMonthlyProd.AllowUserToAddRows = false;
            this.dataRefMonthlyProd.AllowUserToDeleteRows = false;
            this.dataRefMonthlyProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataRefMonthlyProd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn21,
            this.Column47});
            this.dataRefMonthlyProd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataRefMonthlyProd.Location = new System.Drawing.Point(0, 0);
            this.dataRefMonthlyProd.Name = "dataRefMonthlyProd";
            this.dataRefMonthlyProd.ReadOnly = true;
            this.dataRefMonthlyProd.RowHeadersVisible = false;
            this.dataRefMonthlyProd.Size = new System.Drawing.Size(352, 309);
            this.dataRefMonthlyProd.TabIndex = 275;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.HeaderText = "Month";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Width = 70;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.HeaderText = "Year";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.Width = 70;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.HeaderText = "WS [m/s]";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.Width = 90;
            // 
            // Column47
            // 
            this.Column47.HeaderText = "% Diff. from LT";
            this.Column47.Name = "Column47";
            this.Column47.ReadOnly = true;
            this.Column47.Width = 90;
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
            this.cboNumWDRefTab.Location = new System.Drawing.Point(291, 7);
            this.cboNumWDRefTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboNumWDRefTab.Name = "cboNumWDRefTab";
            this.cboNumWDRefTab.Size = new System.Drawing.Size(48, 26);
            this.cboNumWDRefTab.TabIndex = 274;
            this.cboNumWDRefTab.Text = "16";
            this.cboNumWDRefTab.SelectedIndexChanged += new System.EventHandler(this.cboNumWDRefTab_SelectedIndexChanged);
            // 
            // label223
            // 
            this.label223.AutoSize = true;
            this.label223.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label223.Location = new System.Drawing.Point(182, 10);
            this.label223.Name = "label223";
            this.label223.Size = new System.Drawing.Size(103, 18);
            this.label223.TabIndex = 273;
            this.label223.Text = "Num. WD bins :";
            // 
            // chkYearsToDisplayAll
            // 
            this.chkYearsToDisplayAll.AutoSize = true;
            this.chkYearsToDisplayAll.Checked = true;
            this.chkYearsToDisplayAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYearsToDisplayAll.Location = new System.Drawing.Point(6, 6);
            this.chkYearsToDisplayAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkYearsToDisplayAll.Name = "chkYearsToDisplayAll";
            this.chkYearsToDisplayAll.Size = new System.Drawing.Size(139, 23);
            this.chkYearsToDisplayAll.TabIndex = 272;
            this.chkYearsToDisplayAll.Text = "Select/Deselect All";
            this.chkYearsToDisplayAll.UseVisualStyleBackColor = true;
            this.chkYearsToDisplayAll.CheckedChanged += new System.EventHandler(this.chkYearsToDisplayAll_CheckedChanged);
            // 
            // chkYearsToDisplay
            // 
            this.chkYearsToDisplay.CheckOnClick = true;
            this.chkYearsToDisplay.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.chkYearsToDisplay.FormattingEnabled = true;
            this.chkYearsToDisplay.Location = new System.Drawing.Point(6, 31);
            this.chkYearsToDisplay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkYearsToDisplay.Name = "chkYearsToDisplay";
            this.chkYearsToDisplay.Size = new System.Drawing.Size(88, 251);
            this.chkYearsToDisplay.TabIndex = 228;
            this.chkYearsToDisplay.SelectedIndexChanged += new System.EventHandler(this.chkYearsToDisplay_SelectedIndexChanged);
            // 
            // plotMERRA_WindRose
            // 
            this.plotMERRA_WindRose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotMERRA_WindRose.Location = new System.Drawing.Point(100, 45);
            this.plotMERRA_WindRose.MinimumSize = new System.Drawing.Size(229, 149);
            this.plotMERRA_WindRose.Name = "plotMERRA_WindRose";
            this.plotMERRA_WindRose.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMERRA_WindRose.Size = new System.Drawing.Size(251, 239);
            this.plotMERRA_WindRose.TabIndex = 227;
            this.plotMERRA_WindRose.Text = "plotView1";
            this.plotMERRA_WindRose.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMERRA_WindRose.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMERRA_WindRose.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.plotMERRA_Yearly);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.plotMERRA_Monthly);
            this.splitContainer2.Size = new System.Drawing.Size(1031, 313);
            this.splitContainer2.SplitterDistance = 532;
            this.splitContainer2.TabIndex = 0;
            // 
            // plotMERRA_Yearly
            // 
            this.plotMERRA_Yearly.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotMERRA_Yearly.Location = new System.Drawing.Point(0, 0);
            this.plotMERRA_Yearly.MaximumSize = new System.Drawing.Size(1680, 1400);
            this.plotMERRA_Yearly.Name = "plotMERRA_Yearly";
            this.plotMERRA_Yearly.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMERRA_Yearly.Size = new System.Drawing.Size(532, 313);
            this.plotMERRA_Yearly.TabIndex = 284;
            this.plotMERRA_Yearly.Text = "plotView1";
            this.plotMERRA_Yearly.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMERRA_Yearly.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMERRA_Yearly.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotMERRA_Monthly
            // 
            this.plotMERRA_Monthly.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotMERRA_Monthly.Location = new System.Drawing.Point(0, 0);
            this.plotMERRA_Monthly.MaximumSize = new System.Drawing.Size(1680, 1400);
            this.plotMERRA_Monthly.Name = "plotMERRA_Monthly";
            this.plotMERRA_Monthly.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMERRA_Monthly.Size = new System.Drawing.Size(495, 313);
            this.plotMERRA_Monthly.TabIndex = 226;
            this.plotMERRA_Monthly.Text = "plotView1";
            this.plotMERRA_Monthly.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMERRA_Monthly.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMERRA_Monthly.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
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
            // btnRefDataDownloads
            // 
            this.btnRefDataDownloads.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnRefDataDownloads.Location = new System.Drawing.Point(315, 13);
            this.btnRefDataDownloads.Name = "btnRefDataDownloads";
            this.btnRefDataDownloads.Size = new System.Drawing.Size(109, 56);
            this.btnRefDataDownloads.TabIndex = 256;
            this.btnRefDataDownloads.Text = "Manage Reference Data";
            this.btnRefDataDownloads.UseVisualStyleBackColor = true;
            this.btnRefDataDownloads.Click += new System.EventHandler(this.btnRefDataDownloads_Click);
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
            // label121
            // 
            this.label121.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.cboReferenceMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.label120.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.cboReferenceYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReferenceYear.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReferenceYear.FormattingEnabled = true;
            this.cboReferenceYear.Location = new System.Drawing.Point(1199, 48);
            this.cboReferenceYear.Margin = new System.Windows.Forms.Padding(2);
            this.cboReferenceYear.Name = "cboReferenceYear";
            this.cboReferenceYear.Size = new System.Drawing.Size(88, 26);
            this.cboReferenceYear.TabIndex = 208;
            this.cboReferenceYear.SelectedIndexChanged += new System.EventHandler(this.cboMERRAYear_SelectedIndexChanged);
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
            // label159
            // 
            this.label159.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label151.Location = new System.Drawing.Point(13, 14);
            this.label151.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(283, 25);
            this.label151.TabIndex = 99;
            this.label151.Text = "Long-Term Reference Data";
            // 
            // pgeMCP
            // 
            this.pgeMCP.AutoScroll = true;
            this.pgeMCP.Controls.Add(this.btnEditMetsUsedForDebias);
            this.pgeMCP.Controls.Add(this.textBox1);
            this.pgeMCP.Controls.Add(this.chkUsingMetsForDebias);
            this.pgeMCP.Controls.Add(this.txtDebiasRSq);
            this.pgeMCP.Controls.Add(this.label254);
            this.pgeMCP.Controls.Add(this.txtDebiasIntercept);
            this.pgeMCP.Controls.Add(this.label253);
            this.pgeMCP.Controls.Add(this.txtDebiasSlope);
            this.pgeMCP.Controls.Add(this.label252);
            this.pgeMCP.Controls.Add(this.dataDebias);
            this.pgeMCP.Controls.Add(this.chkUseDebiased);
            this.pgeMCP.Controls.Add(this.chkUseMeasRose);
            this.pgeMCP.Controls.Add(this.txtDebiasedWS);
            this.pgeMCP.Controls.Add(this.label251);
            this.pgeMCP.Controls.Add(this.plotMCP_Debias);
            this.pgeMCP.Controls.Add(this.btnDebiasWS);
            this.pgeMCP.Controls.Add(this.label250);
            this.pgeMCP.Controls.Add(this.dataMCP_Uncert);
            this.pgeMCP.Controls.Add(this.dataMCP_Bins);
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
            // btnEditMetsUsedForDebias
            // 
            this.btnEditMetsUsedForDebias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditMetsUsedForDebias.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnEditMetsUsedForDebias.Location = new System.Drawing.Point(1235, 353);
            this.btnEditMetsUsedForDebias.Name = "btnEditMetsUsedForDebias";
            this.btnEditMetsUsedForDebias.Size = new System.Drawing.Size(34, 33);
            this.btnEditMetsUsedForDebias.TabIndex = 324;
            this.btnEditMetsUsedForDebias.Text = "✎";
            this.btnEditMetsUsedForDebias.UseVisualStyleBackColor = true;
            this.btnEditMetsUsedForDebias.Click += new System.EventHandler(this.btnEditMetsUsedForDebias_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(942, 356);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(278, 22);
            this.textBox1.TabIndex = 323;
            // 
            // chkUsingMetsForDebias
            // 
            this.chkUsingMetsForDebias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUsingMetsForDebias.AutoSize = true;
            this.chkUsingMetsForDebias.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.chkUsingMetsForDebias.Location = new System.Drawing.Point(882, 350);
            this.chkUsingMetsForDebias.Name = "chkUsingMetsForDebias";
            this.chkUsingMetsForDebias.Size = new System.Drawing.Size(58, 36);
            this.chkUsingMetsForDebias.TabIndex = 322;
            this.chkUsingMetsForDebias.Text = "Using \r\n Mets:";
            this.chkUsingMetsForDebias.UseVisualStyleBackColor = true;
            this.chkUsingMetsForDebias.CheckedChanged += new System.EventHandler(this.chkUsingMetsForDebias_CheckedChanged);
            // 
            // txtDebiasRSq
            // 
            this.txtDebiasRSq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDebiasRSq.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDebiasRSq.Location = new System.Drawing.Point(942, 324);
            this.txtDebiasRSq.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDebiasRSq.Name = "txtDebiasRSq";
            this.txtDebiasRSq.ReadOnly = true;
            this.txtDebiasRSq.Size = new System.Drawing.Size(70, 22);
            this.txtDebiasRSq.TabIndex = 320;
            // 
            // label254
            // 
            this.label254.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label254.AutoSize = true;
            this.label254.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label254.Location = new System.Drawing.Point(916, 327);
            this.label254.Name = "label254";
            this.label254.Size = new System.Drawing.Size(24, 16);
            this.label254.TabIndex = 319;
            this.label254.Text = "R² :";
            // 
            // txtDebiasIntercept
            // 
            this.txtDebiasIntercept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDebiasIntercept.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDebiasIntercept.Location = new System.Drawing.Point(942, 296);
            this.txtDebiasIntercept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDebiasIntercept.Name = "txtDebiasIntercept";
            this.txtDebiasIntercept.ReadOnly = true;
            this.txtDebiasIntercept.Size = new System.Drawing.Size(70, 22);
            this.txtDebiasIntercept.TabIndex = 318;
            // 
            // label253
            // 
            this.label253.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label253.AutoSize = true;
            this.label253.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label253.Location = new System.Drawing.Point(884, 299);
            this.label253.Name = "label253";
            this.label253.Size = new System.Drawing.Size(56, 16);
            this.label253.TabIndex = 317;
            this.label253.Text = "Intercept :";
            // 
            // txtDebiasSlope
            // 
            this.txtDebiasSlope.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDebiasSlope.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDebiasSlope.Location = new System.Drawing.Point(942, 268);
            this.txtDebiasSlope.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDebiasSlope.Name = "txtDebiasSlope";
            this.txtDebiasSlope.ReadOnly = true;
            this.txtDebiasSlope.Size = new System.Drawing.Size(70, 22);
            this.txtDebiasSlope.TabIndex = 316;
            // 
            // label252
            // 
            this.label252.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label252.AutoSize = true;
            this.label252.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label252.Location = new System.Drawing.Point(898, 271);
            this.label252.Name = "label252";
            this.label252.Size = new System.Drawing.Size(42, 16);
            this.label252.TabIndex = 315;
            this.label252.Text = "Slope :";
            // 
            // dataDebias
            // 
            this.dataDebias.AllowUserToAddRows = false;
            this.dataDebias.AllowUserToDeleteRows = false;
            this.dataDebias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dataDebias.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataDebias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataDebias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn54,
            this.dataGridViewTextBoxColumn103,
            this.dataGridViewTextBoxColumn104,
            this.dataGridViewTextBoxColumn105});
            this.dataDebias.Location = new System.Drawing.Point(1278, 180);
            this.dataDebias.Name = "dataDebias";
            this.dataDebias.ReadOnly = true;
            this.dataDebias.RowHeadersVisible = false;
            this.dataDebias.Size = new System.Drawing.Size(192, 167);
            this.dataDebias.TabIndex = 314;
            // 
            // dataGridViewTextBoxColumn54
            // 
            this.dataGridViewTextBoxColumn54.HeaderText = "Start";
            this.dataGridViewTextBoxColumn54.Name = "dataGridViewTextBoxColumn54";
            this.dataGridViewTextBoxColumn54.ReadOnly = true;
            this.dataGridViewTextBoxColumn54.Width = 40;
            // 
            // dataGridViewTextBoxColumn103
            // 
            this.dataGridViewTextBoxColumn103.HeaderText = "End";
            this.dataGridViewTextBoxColumn103.Name = "dataGridViewTextBoxColumn103";
            this.dataGridViewTextBoxColumn103.ReadOnly = true;
            this.dataGridViewTextBoxColumn103.Width = 40;
            // 
            // dataGridViewTextBoxColumn104
            // 
            this.dataGridViewTextBoxColumn104.HeaderText = "LT Est.";
            this.dataGridViewTextBoxColumn104.Name = "dataGridViewTextBoxColumn104";
            this.dataGridViewTextBoxColumn104.ReadOnly = true;
            this.dataGridViewTextBoxColumn104.Width = 50;
            // 
            // dataGridViewTextBoxColumn105
            // 
            this.dataGridViewTextBoxColumn105.HeaderText = "Ref. WS";
            this.dataGridViewTextBoxColumn105.Name = "dataGridViewTextBoxColumn105";
            this.dataGridViewTextBoxColumn105.ReadOnly = true;
            this.dataGridViewTextBoxColumn105.Width = 50;
            // 
            // chkUseDebiased
            // 
            this.chkUseDebiased.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUseDebiased.AutoSize = true;
            this.chkUseDebiased.Checked = true;
            this.chkUseDebiased.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseDebiased.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.chkUseDebiased.Location = new System.Drawing.Point(637, 609);
            this.chkUseDebiased.Name = "chkUseDebiased";
            this.chkUseDebiased.Size = new System.Drawing.Size(94, 20);
            this.chkUseDebiased.TabIndex = 313;
            this.chkUseDebiased.Text = "Use Debiased";
            this.chkUseDebiased.UseVisualStyleBackColor = true;
            // 
            // chkUseMeasRose
            // 
            this.chkUseMeasRose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUseMeasRose.AutoSize = true;
            this.chkUseMeasRose.Checked = true;
            this.chkUseMeasRose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseMeasRose.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.chkUseMeasRose.Location = new System.Drawing.Point(523, 610);
            this.chkUseMeasRose.Name = "chkUseMeasRose";
            this.chkUseMeasRose.Size = new System.Drawing.Size(106, 20);
            this.chkUseMeasRose.TabIndex = 312;
            this.chkUseMeasRose.Text = "Use Meas. Rose";
            this.chkUseMeasRose.UseVisualStyleBackColor = true;
            // 
            // txtDebiasedWS
            // 
            this.txtDebiasedWS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDebiasedWS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDebiasedWS.Location = new System.Drawing.Point(942, 237);
            this.txtDebiasedWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDebiasedWS.Name = "txtDebiasedWS";
            this.txtDebiasedWS.ReadOnly = true;
            this.txtDebiasedWS.Size = new System.Drawing.Size(70, 25);
            this.txtDebiasedWS.TabIndex = 311;
            // 
            // label251
            // 
            this.label251.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label251.AutoSize = true;
            this.label251.Font = new System.Drawing.Font("Palatino Linotype", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label251.Location = new System.Drawing.Point(871, 232);
            this.label251.Name = "label251";
            this.label251.Size = new System.Drawing.Size(69, 32);
            this.label251.TabIndex = 310;
            this.label251.Text = "Debiased LT\r\n WS (m/s) :";
            // 
            // plotMCP_Debias
            // 
            this.plotMCP_Debias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.plotMCP_Debias.Location = new System.Drawing.Point(1026, 180);
            this.plotMCP_Debias.Name = "plotMCP_Debias";
            this.plotMCP_Debias.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMCP_Debias.Size = new System.Drawing.Size(243, 167);
            this.plotMCP_Debias.TabIndex = 309;
            this.plotMCP_Debias.Text = "plotView1";
            this.plotMCP_Debias.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotMCP_Debias.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotMCP_Debias.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnDebiasWS
            // 
            this.btnDebiasWS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDebiasWS.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnDebiasWS.Location = new System.Drawing.Point(901, 194);
            this.btnDebiasWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDebiasWS.Name = "btnDebiasWS";
            this.btnDebiasWS.Size = new System.Drawing.Size(91, 31);
            this.btnDebiasWS.TabIndex = 308;
            this.btnDebiasWS.Text = "Debias";
            this.btnDebiasWS.UseVisualStyleBackColor = true;
            this.btnDebiasWS.Click += new System.EventHandler(this.btnDebiasWS_Click);
            // 
            // label250
            // 
            this.label250.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label250.AutoSize = true;
            this.label250.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label250.Location = new System.Drawing.Point(869, 167);
            this.label250.Name = "label250";
            this.label250.Size = new System.Drawing.Size(151, 23);
            this.label250.TabIndex = 307;
            this.label250.Text = "MCP Debiasing";
            // 
            // dataMCP_Uncert
            // 
            this.dataMCP_Uncert.AllowUserToAddRows = false;
            this.dataMCP_Uncert.AllowUserToDeleteRows = false;
            this.dataMCP_Uncert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dataMCP_Uncert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataMCP_Uncert.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24});
            this.dataMCP_Uncert.Location = new System.Drawing.Point(1278, 432);
            this.dataMCP_Uncert.Name = "dataMCP_Uncert";
            this.dataMCP_Uncert.ReadOnly = true;
            this.dataMCP_Uncert.RowHeadersVisible = false;
            this.dataMCP_Uncert.Size = new System.Drawing.Size(192, 269);
            this.dataMCP_Uncert.TabIndex = 306;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.HeaderText = "Window";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            this.dataGridViewTextBoxColumn22.Width = 70;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.HeaderText = "Mean";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            this.dataGridViewTextBoxColumn23.Width = 50;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.HeaderText = "SD";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.Width = 50;
            // 
            // dataMCP_Bins
            // 
            this.dataMCP_Bins.AllowUserToAddRows = false;
            this.dataMCP_Bins.AllowUserToDeleteRows = false;
            this.dataMCP_Bins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataMCP_Bins.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column48,
            this.Column49,
            this.Column50,
            this.Column51});
            this.dataMCP_Bins.Location = new System.Drawing.Point(21, 338);
            this.dataMCP_Bins.Name = "dataMCP_Bins";
            this.dataMCP_Bins.ReadOnly = true;
            this.dataMCP_Bins.RowHeadersVisible = false;
            this.dataMCP_Bins.Size = new System.Drawing.Size(234, 283);
            this.dataMCP_Bins.TabIndex = 305;
            // 
            // Column48
            // 
            this.Column48.HeaderText = "WS";
            this.Column48.Name = "Column48";
            this.Column48.ReadOnly = true;
            this.Column48.Width = 50;
            // 
            // Column49
            // 
            this.Column49.HeaderText = "Mean";
            this.Column49.Name = "Column49";
            this.Column49.ReadOnly = true;
            this.Column49.Width = 60;
            // 
            // Column50
            // 
            this.Column50.HeaderText = "SD";
            this.Column50.Name = "Column50";
            this.Column50.ReadOnly = true;
            this.Column50.Width = 60;
            // 
            // Column51
            // 
            this.Column51.HeaderText = "Count";
            this.Column51.Name = "Column51";
            this.Column51.ReadOnly = true;
            this.Column51.Width = 60;
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
            this.label237.Location = new System.Drawing.Point(745, 238);
            this.label237.Name = "label237";
            this.label237.Size = new System.Drawing.Size(54, 18);
            this.label237.TabIndex = 302;
            this.label237.Text = "Height :";
            // 
            // cboMCP_Height
            // 
            this.cboMCP_Height.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMCP_Height.FormattingEnabled = true;
            this.cboMCP_Height.Location = new System.Drawing.Point(745, 256);
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
            this.btnExportTarget.Location = new System.Drawing.Point(754, 633);
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
            this.plotMCP_Uncertainty.Location = new System.Drawing.Point(874, 460);
            this.plotMCP_Uncertainty.Name = "plotMCP_Uncertainty";
            this.plotMCP_Uncertainty.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMCP_Uncertainty.Size = new System.Drawing.Size(395, 237);
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
            this.plotMCP.Location = new System.Drawing.Point(300, 290);
            this.plotMCP.Name = "plotMCP";
            this.plotMCP.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotMCP.Size = new System.Drawing.Size(555, 301);
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
            this.btnShowMCPRanges.Location = new System.Drawing.Point(1436, 116);
            this.btnShowMCPRanges.Name = "btnShowMCPRanges";
            this.btnShowMCPRanges.Size = new System.Drawing.Size(34, 25);
            this.btnShowMCPRanges.TabIndex = 295;
            this.btnShowMCPRanges.Text = "?";
            this.btnShowMCPRanges.UseVisualStyleBackColor = true;
            this.btnShowMCPRanges.Click += new System.EventHandler(this.btnShowMCPRanges_Click);
            // 
            // label51
            // 
            this.label51.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(1253, 86);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(109, 17);
            this.label51.TabIndex = 294;
            this.label51.Text = "Matrix Settings:";
            // 
            // btnDoMCPAllMets
            // 
            this.btnDoMCPAllMets.Location = new System.Drawing.Point(747, 41);
            this.btnDoMCPAllMets.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDoMCPAllMets.Name = "btnDoMCPAllMets";
            this.btnDoMCPAllMets.Size = new System.Drawing.Size(97, 78);
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
            this.btnResetDates.Location = new System.Drawing.Point(370, 622);
            this.btnResetDates.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnResetDates.Name = "btnResetDates";
            this.btnResetDates.Size = new System.Drawing.Size(30, 29);
            this.btnResetDates.TabIndex = 291;
            this.btnResetDates.Text = "↻";
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
            this.label145.Location = new System.Drawing.Point(257, 629);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(90, 17);
            this.label145.TabIndex = 289;
            this.label145.Text = "Export Data :";
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
            this.txtTAB_WS_bin.Location = new System.Drawing.Point(715, 674);
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
            this.label142.Location = new System.Drawing.Point(636, 676);
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
            this.label143.Location = new System.Drawing.Point(637, 639);
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
            "12",
            "16",
            "24"});
            this.cboTAB_bins.Location = new System.Drawing.Point(694, 640);
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
            this.label141.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label141.Location = new System.Drawing.Point(1234, 38);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(220, 17);
            this.label141.TabIndex = 280;
            this.label141.Text = "Method of Bins / Matrix Setting :";
            // 
            // label140
            // 
            this.label140.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label140.AutoSize = true;
            this.label140.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label140.Location = new System.Drawing.Point(923, 53);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(89, 18);
            this.label140.TabIndex = 279;
            this.label140.Text = "MCP Method:";
            // 
            // txtLast_WS_Wgt
            // 
            this.txtLast_WS_Wgt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLast_WS_Wgt.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLast_WS_Wgt.Location = new System.Drawing.Point(1389, 129);
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
            this.label134.Location = new System.Drawing.Point(1274, 136);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(110, 18);
            this.label134.TabIndex = 277;
            this.label134.Text = "Last WS Weight :";
            // 
            // txtWS_PDF_Wgt
            // 
            this.txtWS_PDF_Wgt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWS_PDF_Wgt.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWS_PDF_Wgt.Location = new System.Drawing.Point(1389, 101);
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
            this.label135.Location = new System.Drawing.Point(1275, 108);
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
            this.cboMCPNumSeasons.Location = new System.Drawing.Point(1150, 142);
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
            this.label136.Location = new System.Drawing.Point(1023, 145);
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
            this.cboMCPNumHours.Location = new System.Drawing.Point(1150, 109);
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
            this.label137.Location = new System.Drawing.Point(992, 112);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(151, 18);
            this.label137.TabIndex = 271;
            this.label137.Text = "Num. Time of Day bins :";
            // 
            // txtWS_bin_width
            // 
            this.txtWS_bin_width.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWS_bin_width.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWS_bin_width.Location = new System.Drawing.Point(1389, 58);
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
            this.label138.Location = new System.Drawing.Point(1252, 61);
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
            this.cboMCPNumWD.Location = new System.Drawing.Point(1150, 78);
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
            this.label139.Location = new System.Drawing.Point(1040, 81);
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
            this.cboMCP_Type.Location = new System.Drawing.Point(1018, 45);
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
            this.label133.Location = new System.Drawing.Point(922, 17);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(223, 23);
            this.label133.TabIndex = 265;
            this.label133.Text = "MCP Method && Settings";
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
            this.btnExportMCP_TAB.Location = new System.Drawing.Point(522, 637);
            this.btnExportMCP_TAB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportMCP_TAB.Name = "btnExportMCP_TAB";
            this.btnExportMCP_TAB.Size = new System.Drawing.Size(112, 64);
            this.btnExportMCP_TAB.TabIndex = 240;
            this.btnExportMCP_TAB.Text = "Export Estimated data as TAB file\r\n";
            this.btnExportMCP_TAB.UseVisualStyleBackColor = true;
            this.btnExportMCP_TAB.Click += new System.EventHandler(this.btnExportMCP_TAB_Click);
            // 
            // btnExportMCP_TS
            // 
            this.btnExportMCP_TS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportMCP_TS.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportMCP_TS.Location = new System.Drawing.Point(407, 637);
            this.btnExportMCP_TS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportMCP_TS.Name = "btnExportMCP_TS";
            this.btnExportMCP_TS.Size = new System.Drawing.Size(112, 64);
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
            this.cboUncertStep.Location = new System.Drawing.Point(1050, 428);
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
            this.label119.Location = new System.Drawing.Point(904, 418);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(140, 36);
            this.label119.TabIndex = 237;
            this.label119.Text = "Uncertainty Window \r\nStep (months):";
            // 
            // btnExportMCPUncert
            // 
            this.btnExportMCPUncert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportMCPUncert.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportMCPUncert.Location = new System.Drawing.Point(1389, 383);
            this.btnExportMCPUncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportMCPUncert.Name = "btnExportMCPUncert";
            this.btnExportMCPUncert.Size = new System.Drawing.Size(75, 36);
            this.btnExportMCPUncert.TabIndex = 235;
            this.btnExportMCPUncert.Text = "Export";
            this.btnExportMCPUncert.UseVisualStyleBackColor = true;
            this.btnExportMCPUncert.Click += new System.EventHandler(this.btnExportMCPUncert_Click);
            // 
            // label118
            // 
            this.label118.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label118.AutoSize = true;
            this.label118.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label118.Location = new System.Drawing.Point(867, 389);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(244, 23);
            this.label118.TabIndex = 234;
            this.label118.Text = "MCP Uncertainty Analysis";
            // 
            // btnMCP_Uncert
            // 
            this.btnMCP_Uncert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMCP_Uncert.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnMCP_Uncert.Location = new System.Drawing.Point(1117, 394);
            this.btnMCP_Uncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMCP_Uncert.Name = "btnMCP_Uncert";
            this.btnMCP_Uncert.Size = new System.Drawing.Size(103, 59);
            this.btnMCP_Uncert.TabIndex = 233;
            this.btnMCP_Uncert.Text = "Do MCP Uncertainty Analysis";
            this.btnMCP_Uncert.UseVisualStyleBackColor = true;
            this.btnMCP_Uncert.Click += new System.EventHandler(this.btnMCP_Uncert_Click);
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.Location = new System.Drawing.Point(609, 238);
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
            this.cboMCP_Season.Location = new System.Drawing.Point(609, 256);
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
            this.label109.Location = new System.Drawing.Point(328, 238);
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
            this.cboMCP_TOD.Location = new System.Drawing.Point(325, 257);
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
            this.txtTarg_LT_WS.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label114.Location = new System.Drawing.Point(463, 238);
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
            this.cboMCP_WD.Location = new System.Drawing.Point(467, 256);
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
            this.pgeMetSumm.Controls.Add(this.spltMetTurbSumm);
            this.pgeMetSumm.Controls.Add(this.cboSummSeason);
            this.pgeMetSumm.Controls.Add(this.cboSummTOD);
            this.pgeMetSumm.Controls.Add(this.Label80);
            this.pgeMetSumm.Controls.Add(this.chkTurbSummAll);
            this.pgeMetSumm.Controls.Add(this.Label78);
            this.pgeMetSumm.Controls.Add(this.chkMetSummAll);
            this.pgeMetSumm.Controls.Add(this.Label12);
            this.pgeMetSumm.Controls.Add(this.Label76);
            this.pgeMetSumm.Controls.Add(this.btnExportExpoSRDH);
            this.pgeMetSumm.Controls.Add(this.Label74);
            this.pgeMetSumm.Controls.Add(this.cboSummaryWD);
            this.pgeMetSumm.Controls.Add(this.Label75);
            this.pgeMetSumm.Controls.Add(this.cboMetSum_Rad);
            this.pgeMetSumm.Controls.Add(this.Label73);
            this.pgeMetSumm.Location = new System.Drawing.Point(4, 27);
            this.pgeMetSumm.Margin = new System.Windows.Forms.Padding(1);
            this.pgeMetSumm.Name = "pgeMetSumm";
            this.pgeMetSumm.Size = new System.Drawing.Size(1488, 712);
            this.pgeMetSumm.TabIndex = 12;
            this.pgeMetSumm.Text = "Met && Turbine Summary";
            this.pgeMetSumm.UseVisualStyleBackColor = true;
            // 
            // spltMetTurbSumm
            // 
            this.spltMetTurbSumm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spltMetTurbSumm.Location = new System.Drawing.Point(3, 94);
            this.spltMetTurbSumm.Name = "spltMetTurbSumm";
            this.spltMetTurbSumm.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltMetTurbSumm.Panel1
            // 
            this.spltMetTurbSumm.Panel1.Controls.Add(this.splitContainer5);
            // 
            // spltMetTurbSumm.Panel2
            // 
            this.spltMetTurbSumm.Panel2.Controls.Add(this.splitContainer6);
            this.spltMetTurbSumm.Size = new System.Drawing.Size(1477, 603);
            this.spltMetTurbSumm.SplitterDistance = 383;
            this.spltMetTurbSumm.TabIndex = 225;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.chkMetSumm);
            this.splitContainer5.Panel1.Controls.Add(this.chkTurbSumm);
            this.splitContainer5.Panel1.Controls.Add(this.dataMetStats);
            this.splitContainer5.Panel1.Controls.Add(this.dataTurbStats);
            this.splitContainer5.Panel1.Controls.Add(this.dataMetSummary);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer10);
            this.splitContainer5.Size = new System.Drawing.Size(1477, 383);
            this.splitContainer5.SplitterDistance = 164;
            this.splitContainer5.TabIndex = 0;
            // 
            // chkMetSumm
            // 
            this.chkMetSumm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkMetSumm.CheckOnClick = true;
            this.chkMetSumm.FormattingEnabled = true;
            this.chkMetSumm.HorizontalScrollbar = true;
            this.chkMetSumm.Location = new System.Drawing.Point(771, 10);
            this.chkMetSumm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMetSumm.Name = "chkMetSumm";
            this.chkMetSumm.Size = new System.Drawing.Size(86, 144);
            this.chkMetSumm.TabIndex = 228;
            this.chkMetSumm.SelectedIndexChanged += new System.EventHandler(this.chkMetSumm_SelectedIndexChanged);
            // 
            // chkTurbSumm
            // 
            this.chkTurbSumm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTurbSumm.CheckOnClick = true;
            this.chkTurbSumm.FormattingEnabled = true;
            this.chkTurbSumm.HorizontalScrollbar = true;
            this.chkTurbSumm.Location = new System.Drawing.Point(863, 10);
            this.chkTurbSumm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbSumm.Name = "chkTurbSumm";
            this.chkTurbSumm.Size = new System.Drawing.Size(91, 144);
            this.chkTurbSumm.TabIndex = 227;
            // 
            // dataMetStats
            // 
            this.dataMetStats.AllowUserToAddRows = false;
            this.dataMetStats.AllowUserToDeleteRows = false;
            this.dataMetStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataMetStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataMetStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27,
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn29});
            this.dataMetStats.Location = new System.Drawing.Point(966, 10);
            this.dataMetStats.Name = "dataMetStats";
            this.dataMetStats.ReadOnly = true;
            this.dataMetStats.RowHeadersVisible = false;
            this.dataMetStats.Size = new System.Drawing.Size(241, 147);
            this.dataMetStats.TabIndex = 226;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.HeaderText = "Param.";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.Width = 55;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.HeaderText = "Avg.";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            this.dataGridViewTextBoxColumn26.Width = 45;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.HeaderText = "SD";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            this.dataGridViewTextBoxColumn27.Width = 45;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.HeaderText = "Min.";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            this.dataGridViewTextBoxColumn28.Width = 45;
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.HeaderText = "Max.";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.ReadOnly = true;
            this.dataGridViewTextBoxColumn29.Width = 45;
            // 
            // dataTurbStats
            // 
            this.dataTurbStats.AllowUserToAddRows = false;
            this.dataTurbStats.AllowUserToDeleteRows = false;
            this.dataTurbStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataTurbStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTurbStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn30,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn34});
            this.dataTurbStats.Location = new System.Drawing.Point(1233, 10);
            this.dataTurbStats.Name = "dataTurbStats";
            this.dataTurbStats.ReadOnly = true;
            this.dataTurbStats.RowHeadersVisible = false;
            this.dataTurbStats.Size = new System.Drawing.Size(241, 147);
            this.dataTurbStats.TabIndex = 225;
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.HeaderText = "Param.";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.ReadOnly = true;
            this.dataGridViewTextBoxColumn30.Width = 55;
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.HeaderText = "Avg.";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.ReadOnly = true;
            this.dataGridViewTextBoxColumn31.Width = 45;
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.HeaderText = "SD";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.ReadOnly = true;
            this.dataGridViewTextBoxColumn32.Width = 45;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.HeaderText = "Min.";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            this.dataGridViewTextBoxColumn33.Width = 45;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.HeaderText = "Max.";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.ReadOnly = true;
            this.dataGridViewTextBoxColumn34.Width = 45;
            // 
            // dataMetSummary
            // 
            this.dataMetSummary.AllowUserToAddRows = false;
            this.dataMetSummary.AllowUserToDeleteRows = false;
            this.dataMetSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataMetSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataMetSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column52,
            this.Column53,
            this.Column54,
            this.Column55,
            this.Column56,
            this.Column57,
            this.Column58,
            this.Column59,
            this.Column60,
            this.Column61,
            this.Column62});
            this.dataMetSummary.Location = new System.Drawing.Point(6, 3);
            this.dataMetSummary.Name = "dataMetSummary";
            this.dataMetSummary.ReadOnly = true;
            this.dataMetSummary.RowHeadersVisible = false;
            this.dataMetSummary.Size = new System.Drawing.Size(759, 158);
            this.dataMetSummary.TabIndex = 223;
            // 
            // Column52
            // 
            this.Column52.HeaderText = "Name";
            this.Column52.Name = "Column52";
            this.Column52.ReadOnly = true;
            this.Column52.Width = 70;
            // 
            // Column53
            // 
            this.Column53.HeaderText = "Elev. [m]";
            this.Column53.Name = "Column53";
            this.Column53.ReadOnly = true;
            this.Column53.Width = 65;
            // 
            // Column54
            // 
            this.Column54.HeaderText = "WS [m/s]";
            this.Column54.Name = "Column54";
            this.Column54.ReadOnly = true;
            this.Column54.Width = 65;
            // 
            // Column55
            // 
            this.Column55.HeaderText = "Weibull k";
            this.Column55.Name = "Column55";
            this.Column55.ReadOnly = true;
            this.Column55.Width = 65;
            // 
            // Column56
            // 
            this.Column56.HeaderText = "Weibull A";
            this.Column56.Name = "Column56";
            this.Column56.ReadOnly = true;
            this.Column56.Width = 65;
            // 
            // Column57
            // 
            this.Column57.HeaderText = "UW Expo. [m]";
            this.Column57.Name = "Column57";
            this.Column57.ReadOnly = true;
            this.Column57.Width = 70;
            // 
            // Column58
            // 
            this.Column58.HeaderText = "DW Expo. [m]";
            this.Column58.Name = "Column58";
            this.Column58.ReadOnly = true;
            this.Column58.Width = 70;
            // 
            // Column59
            // 
            this.Column59.HeaderText = "UW Rough. [m]";
            this.Column59.Name = "Column59";
            this.Column59.ReadOnly = true;
            this.Column59.Width = 70;
            // 
            // Column60
            // 
            this.Column60.HeaderText = "DW Rough. [m] ";
            this.Column60.Name = "Column60";
            this.Column60.ReadOnly = true;
            this.Column60.Width = 70;
            // 
            // Column61
            // 
            this.Column61.HeaderText = "UW Disp. H [m]";
            this.Column61.Name = "Column61";
            this.Column61.ReadOnly = true;
            this.Column61.Width = 70;
            // 
            // Column62
            // 
            this.Column62.HeaderText = "DW Disp. H [m]";
            this.Column62.Name = "Column62";
            this.Column62.ReadOnly = true;
            this.Column62.Width = 70;
            // 
            // splitContainer10
            // 
            this.splitContainer10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer10.Location = new System.Drawing.Point(0, 0);
            this.splitContainer10.Name = "splitContainer10";
            // 
            // splitContainer10.Panel1
            // 
            this.splitContainer10.Panel1.Controls.Add(this.splitContainer11);
            // 
            // splitContainer10.Panel2
            // 
            this.splitContainer10.Panel2.Controls.Add(this.splitContainer12);
            this.splitContainer10.Size = new System.Drawing.Size(1477, 215);
            this.splitContainer10.SplitterDistance = 719;
            this.splitContainer10.TabIndex = 1;
            // 
            // splitContainer11
            // 
            this.splitContainer11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer11.Location = new System.Drawing.Point(0, 0);
            this.splitContainer11.Name = "splitContainer11";
            // 
            // splitContainer11.Panel1
            // 
            this.splitContainer11.Panel1.Controls.Add(this.plotUWExpo);
            // 
            // splitContainer11.Panel2
            // 
            this.splitContainer11.Panel2.Controls.Add(this.plotDWExpo);
            this.splitContainer11.Size = new System.Drawing.Size(719, 215);
            this.splitContainer11.SplitterDistance = 350;
            this.splitContainer11.TabIndex = 1;
            // 
            // plotUWExpo
            // 
            this.plotUWExpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotUWExpo.Location = new System.Drawing.Point(0, 0);
            this.plotUWExpo.Name = "plotUWExpo";
            this.plotUWExpo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotUWExpo.Size = new System.Drawing.Size(350, 215);
            this.plotUWExpo.TabIndex = 215;
            this.plotUWExpo.Text = "plotView1";
            this.plotUWExpo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotUWExpo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotUWExpo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotDWExpo
            // 
            this.plotDWExpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotDWExpo.Location = new System.Drawing.Point(0, 0);
            this.plotDWExpo.Name = "plotDWExpo";
            this.plotDWExpo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDWExpo.Size = new System.Drawing.Size(365, 215);
            this.plotDWExpo.TabIndex = 216;
            this.plotDWExpo.Text = "plotView1";
            this.plotDWExpo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDWExpo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDWExpo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // splitContainer12
            // 
            this.splitContainer12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer12.Location = new System.Drawing.Point(0, 0);
            this.splitContainer12.Name = "splitContainer12";
            // 
            // splitContainer12.Panel1
            // 
            this.splitContainer12.Panel1.Controls.Add(this.plotDWUWExpo);
            // 
            // splitContainer12.Panel2
            // 
            this.splitContainer12.Panel2.Controls.Add(this.plotElev);
            this.splitContainer12.Size = new System.Drawing.Size(754, 215);
            this.splitContainer12.SplitterDistance = 367;
            this.splitContainer12.TabIndex = 1;
            // 
            // plotDWUWExpo
            // 
            this.plotDWUWExpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotDWUWExpo.Location = new System.Drawing.Point(0, 0);
            this.plotDWUWExpo.Name = "plotDWUWExpo";
            this.plotDWUWExpo.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDWUWExpo.Size = new System.Drawing.Size(367, 215);
            this.plotDWUWExpo.TabIndex = 217;
            this.plotDWUWExpo.Text = "plotView1";
            this.plotDWUWExpo.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDWUWExpo.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDWUWExpo.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotElev
            // 
            this.plotElev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotElev.Location = new System.Drawing.Point(0, 0);
            this.plotElev.Name = "plotElev";
            this.plotElev.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotElev.Size = new System.Drawing.Size(383, 215);
            this.plotElev.TabIndex = 218;
            this.plotElev.Text = "plotView1";
            this.plotElev.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotElev.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotElev.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.splitContainer7);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.splitContainer8);
            this.splitContainer6.Size = new System.Drawing.Size(1477, 216);
            this.splitContainer6.SplitterDistance = 724;
            this.splitContainer6.TabIndex = 0;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.plotUW_SR);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.plotDW_SR);
            this.splitContainer7.Size = new System.Drawing.Size(724, 216);
            this.splitContainer7.SplitterDistance = 352;
            this.splitContainer7.TabIndex = 0;
            // 
            // plotUW_SR
            // 
            this.plotUW_SR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotUW_SR.Location = new System.Drawing.Point(0, 0);
            this.plotUW_SR.Name = "plotUW_SR";
            this.plotUW_SR.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotUW_SR.Size = new System.Drawing.Size(352, 216);
            this.plotUW_SR.TabIndex = 219;
            this.plotUW_SR.Text = "plotView1";
            this.plotUW_SR.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotUW_SR.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotUW_SR.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotDW_SR
            // 
            this.plotDW_SR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotDW_SR.Location = new System.Drawing.Point(0, 0);
            this.plotDW_SR.Name = "plotDW_SR";
            this.plotDW_SR.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDW_SR.Size = new System.Drawing.Size(368, 216);
            this.plotDW_SR.TabIndex = 220;
            this.plotDW_SR.Text = "plotView1";
            this.plotDW_SR.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDW_SR.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDW_SR.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // splitContainer8
            // 
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.plotUW_DH);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.plotDW_DH);
            this.splitContainer8.Size = new System.Drawing.Size(749, 216);
            this.splitContainer8.SplitterDistance = 362;
            this.splitContainer8.TabIndex = 0;
            // 
            // plotUW_DH
            // 
            this.plotUW_DH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotUW_DH.Location = new System.Drawing.Point(0, 0);
            this.plotUW_DH.Name = "plotUW_DH";
            this.plotUW_DH.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotUW_DH.Size = new System.Drawing.Size(362, 216);
            this.plotUW_DH.TabIndex = 221;
            this.plotUW_DH.Text = "plotView1";
            this.plotUW_DH.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotUW_DH.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotUW_DH.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotDW_DH
            // 
            this.plotDW_DH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotDW_DH.Location = new System.Drawing.Point(0, 0);
            this.plotDW_DH.Name = "plotDW_DH";
            this.plotDW_DH.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDW_DH.Size = new System.Drawing.Size(383, 216);
            this.plotDW_DH.TabIndex = 222;
            this.plotDW_DH.Text = "plotView1";
            this.plotDW_DH.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDW_DH.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDW_DH.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // cboSummSeason
            // 
            this.cboSummSeason.FormattingEnabled = true;
            this.cboSummSeason.Location = new System.Drawing.Point(309, 58);
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
            this.cboSummTOD.Location = new System.Drawing.Point(310, 25);
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
            this.Label80.Location = new System.Drawing.Point(1238, 70);
            this.Label80.Name = "Label80";
            this.Label80.Size = new System.Drawing.Size(242, 19);
            this.Label80.TabIndex = 175;
            this.Label80.Text = "Turbine Site Statistics (selected)";
            // 
            // chkTurbSummAll
            // 
            this.chkTurbSummAll.AutoSize = true;
            this.chkTurbSummAll.Checked = true;
            this.chkTurbSummAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTurbSummAll.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTurbSummAll.Location = new System.Drawing.Point(880, 56);
            this.chkTurbSummAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTurbSummAll.Name = "chkTurbSummAll";
            this.chkTurbSummAll.Size = new System.Drawing.Size(88, 36);
            this.chkTurbSummAll.TabIndex = 173;
            this.chkTurbSummAll.Text = "Select/\r\nDeselect All";
            this.chkTurbSummAll.UseVisualStyleBackColor = true;
            this.chkTurbSummAll.CheckedChanged += new System.EventHandler(this.chkTurbSummAll_CheckedChanged);
            // 
            // Label78
            // 
            this.Label78.AutoSize = true;
            this.Label78.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label78.Location = new System.Drawing.Point(872, 35);
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
            this.chkMetSummAll.Location = new System.Drawing.Point(775, 56);
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
            this.Label12.Location = new System.Drawing.Point(780, 36);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(64, 16);
            this.Label12.TabIndex = 169;
            this.Label12.Text = "Met Sites";
            // 
            // Label76
            // 
            this.Label76.AutoSize = true;
            this.Label76.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label76.Location = new System.Drawing.Point(989, 69);
            this.Label76.Name = "Label76";
            this.Label76.Size = new System.Drawing.Size(213, 19);
            this.Label76.TabIndex = 166;
            this.Label76.Text = "Met Site Statistics (selected)";
            // 
            // btnExportExpoSRDH
            // 
            this.btnExportExpoSRDH.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExpoSRDH.Location = new System.Drawing.Point(1353, 15);
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
            this.Label74.Location = new System.Drawing.Point(552, 25);
            this.Label74.Name = "Label74";
            this.Label74.Size = new System.Drawing.Size(79, 19);
            this.Label74.TabIndex = 163;
            this.Label74.Text = "WD sector:";
            // 
            // cboSummaryWD
            // 
            this.cboSummaryWD.FormattingEnabled = true;
            this.cboSummaryWD.Location = new System.Drawing.Point(546, 47);
            this.cboSummaryWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSummaryWD.Name = "cboSummaryWD";
            this.cboSummaryWD.Size = new System.Drawing.Size(115, 26);
            this.cboSummaryWD.TabIndex = 162;
            this.cboSummaryWD.SelectedIndexChanged += new System.EventHandler(this.cboMetSum_WD_SelectedIndexChanged);
            // 
            // Label75
            // 
            this.Label75.AutoSize = true;
            this.Label75.Location = new System.Drawing.Point(429, 25);
            this.Label75.Name = "Label75";
            this.Label75.Size = new System.Drawing.Size(60, 19);
            this.Label75.TabIndex = 161;
            this.Label75.Text = "Radius :";
            // 
            // cboMetSum_Rad
            // 
            this.cboMetSum_Rad.FormattingEnabled = true;
            this.cboMetSum_Rad.Location = new System.Drawing.Point(423, 47);
            this.cboMetSum_Rad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMetSum_Rad.Name = "cboMetSum_Rad";
            this.cboMetSum_Rad.Size = new System.Drawing.Size(115, 26);
            this.cboMetSum_Rad.TabIndex = 160;
            this.cboMetSum_Rad.SelectedIndexChanged += new System.EventHandler(this.cboMetSum_Rad_SelectedIndexChanged);
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
            this.pgeGrossTurbs.Controls.Add(this.txtGross_Valley_used);
            this.pgeGrossTurbs.Controls.Add(this.txtGross_Elev_used);
            this.pgeGrossTurbs.Controls.Add(this.lblModelsUsedGross);
            this.pgeGrossTurbs.Controls.Add(this.txtNameplateCapacity);
            this.pgeGrossTurbs.Controls.Add(this.label249);
            this.pgeGrossTurbs.Controls.Add(this.btnDeletePowerCurve);
            this.pgeGrossTurbs.Controls.Add(this.btnExport_PowerCurve);
            this.pgeGrossTurbs.Controls.Add(this.txtTotalGrossCF);
            this.pgeGrossTurbs.Controls.Add(this.label245);
            this.pgeGrossTurbs.Controls.Add(this.txtTotalGross);
            this.pgeGrossTurbs.Controls.Add(this.label246);
            this.pgeGrossTurbs.Controls.Add(this.dataGrossHisto);
            this.pgeGrossTurbs.Controls.Add(this.dataGrossTurbEst);
            this.pgeGrossTurbs.Controls.Add(this.dataPowerCurveList);
            this.pgeGrossTurbs.Controls.Add(this.plotGrossHisto);
            this.pgeGrossTurbs.Controls.Add(this.plotGross_PowerCrvs);
            this.pgeGrossTurbs.Controls.Add(this.plotGrossWS_Dist);
            this.pgeGrossTurbs.Controls.Add(this.plotGrossWindRose);
            this.pgeGrossTurbs.Controls.Add(this.btnGenTurbGross);
            this.pgeGrossTurbs.Controls.Add(this.txtisMCPdGross);
            this.pgeGrossTurbs.Controls.Add(this.Label40);
            this.pgeGrossTurbs.Controls.Add(this.cboWS_or_WD);
            this.pgeGrossTurbs.Controls.Add(this.txtGross_FlowSepUsed);
            this.pgeGrossTurbs.Controls.Add(this.Label96);
            this.pgeGrossTurbs.Controls.Add(this.txtGross_LC_used);
            this.pgeGrossTurbs.Controls.Add(this.Label83);
            this.pgeGrossTurbs.Controls.Add(this.Label79);
            this.pgeGrossTurbs.Controls.Add(this.Label77);
            this.pgeGrossTurbs.Controls.Add(this.cboGrossParam);
            this.pgeGrossTurbs.Controls.Add(this.btnExportDirWSDists);
            this.pgeGrossTurbs.Controls.Add(this.Label71);
            this.pgeGrossTurbs.Controls.Add(this.cboGrossWD);
            this.pgeGrossTurbs.Controls.Add(this.btnExportDirWS);
            this.pgeGrossTurbs.Controls.Add(this.btnExportWSDists);
            this.pgeGrossTurbs.Controls.Add(this.btnWS_AEP_Exprt);
            this.pgeGrossTurbs.Controls.Add(this.txtAEPMax);
            this.pgeGrossTurbs.Controls.Add(this.txtAEPMin);
            this.pgeGrossTurbs.Controls.Add(this.txtAEPSD);
            this.pgeGrossTurbs.Controls.Add(this.txtAEPAvg);
            this.pgeGrossTurbs.Controls.Add(this.Label35);
            this.pgeGrossTurbs.Controls.Add(this.Label34);
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
            this.pgeGrossTurbs.Location = new System.Drawing.Point(4, 27);
            this.pgeGrossTurbs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeGrossTurbs.Name = "pgeGrossTurbs";
            this.pgeGrossTurbs.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeGrossTurbs.Size = new System.Drawing.Size(1488, 712);
            this.pgeGrossTurbs.TabIndex = 3;
            this.pgeGrossTurbs.Text = "Gross Turbine Ests.";
            this.pgeGrossTurbs.UseVisualStyleBackColor = true;
            // 
            // txtGross_Valley_used
            // 
            this.txtGross_Valley_used.Location = new System.Drawing.Point(891, 19);
            this.txtGross_Valley_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGross_Valley_used.Name = "txtGross_Valley_used";
            this.txtGross_Valley_used.ReadOnly = true;
            this.txtGross_Valley_used.Size = new System.Drawing.Size(90, 25);
            this.txtGross_Valley_used.TabIndex = 313;
            this.txtGross_Valley_used.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGross_Elev_used
            // 
            this.txtGross_Elev_used.Location = new System.Drawing.Point(795, 53);
            this.txtGross_Elev_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGross_Elev_used.Name = "txtGross_Elev_used";
            this.txtGross_Elev_used.ReadOnly = true;
            this.txtGross_Elev_used.Size = new System.Drawing.Size(90, 25);
            this.txtGross_Elev_used.TabIndex = 312;
            this.txtGross_Elev_used.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblModelsUsedGross
            // 
            this.lblModelsUsedGross.AutoSize = true;
            this.lblModelsUsedGross.Location = new System.Drawing.Point(563, 22);
            this.lblModelsUsedGross.Name = "lblModelsUsedGross";
            this.lblModelsUsedGross.Size = new System.Drawing.Size(131, 19);
            this.lblModelsUsedGross.TabIndex = 311;
            this.lblModelsUsedGross.Text = "Models/Data Used :";
            // 
            // txtNameplateCapacity
            // 
            this.txtNameplateCapacity.Font = new System.Drawing.Font("Palatino Linotype", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameplateCapacity.Location = new System.Drawing.Point(895, 106);
            this.txtNameplateCapacity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNameplateCapacity.Name = "txtNameplateCapacity";
            this.txtNameplateCapacity.Size = new System.Drawing.Size(67, 27);
            this.txtNameplateCapacity.TabIndex = 310;
            // 
            // label249
            // 
            this.label249.AutoSize = true;
            this.label249.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label249.Location = new System.Drawing.Point(674, 109);
            this.label249.Name = "label249";
            this.label249.Size = new System.Drawing.Size(210, 18);
            this.label249.TabIndex = 309;
            this.label249.Text = "Nameplate Capacity [MW]:";
            // 
            // btnDeletePowerCurve
            // 
            this.btnDeletePowerCurve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePowerCurve.Font = new System.Drawing.Font("Palatino Linotype", 10F);
            this.btnDeletePowerCurve.Location = new System.Drawing.Point(1345, 34);
            this.btnDeletePowerCurve.Name = "btnDeletePowerCurve";
            this.btnDeletePowerCurve.Size = new System.Drawing.Size(30, 29);
            this.btnDeletePowerCurve.TabIndex = 308;
            this.btnDeletePowerCurve.Text = "✕";
            this.btnDeletePowerCurve.UseVisualStyleBackColor = true;
            this.btnDeletePowerCurve.Click += new System.EventHandler(this.btnDeletePowerCurve_Click);
            // 
            // btnExport_PowerCurve
            // 
            this.btnExport_PowerCurve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport_PowerCurve.Font = new System.Drawing.Font("Palatino Linotype", 12F);
            this.btnExport_PowerCurve.Location = new System.Drawing.Point(1311, 34);
            this.btnExport_PowerCurve.Name = "btnExport_PowerCurve";
            this.btnExport_PowerCurve.Size = new System.Drawing.Size(30, 29);
            this.btnExport_PowerCurve.TabIndex = 307;
            this.btnExport_PowerCurve.Text = "⭳";
            this.btnExport_PowerCurve.UseVisualStyleBackColor = true;
            this.btnExport_PowerCurve.Click += new System.EventHandler(this.btnExport_PowerCurve_Click);
            // 
            // txtTotalGrossCF
            // 
            this.txtTotalGrossCF.Font = new System.Drawing.Font("Palatino Linotype", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalGrossCF.Location = new System.Drawing.Point(586, 106);
            this.txtTotalGrossCF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalGrossCF.Name = "txtTotalGrossCF";
            this.txtTotalGrossCF.Size = new System.Drawing.Size(67, 27);
            this.txtTotalGrossCF.TabIndex = 250;
            this.txtTotalGrossCF.TextChanged += new System.EventHandler(this.txtTotalGrossCF_TextChanged);
            // 
            // label245
            // 
            this.label245.AutoSize = true;
            this.label245.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label245.Location = new System.Drawing.Point(475, 109);
            this.label245.Name = "label245";
            this.label245.Size = new System.Drawing.Size(102, 18);
            this.label245.TabIndex = 249;
            this.label245.Text = "Gross CF [%]:";
            this.label245.Click += new System.EventHandler(this.label245_Click);
            // 
            // txtTotalGross
            // 
            this.txtTotalGross.Font = new System.Drawing.Font("Palatino Linotype", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalGross.Location = new System.Drawing.Point(387, 107);
            this.txtTotalGross.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalGross.Name = "txtTotalGross";
            this.txtTotalGross.Size = new System.Drawing.Size(67, 27);
            this.txtTotalGross.TabIndex = 248;
            // 
            // label246
            // 
            this.label246.AutoSize = true;
            this.label246.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label246.Location = new System.Drawing.Point(214, 110);
            this.label246.Name = "label246";
            this.label246.Size = new System.Drawing.Size(171, 18);
            this.label246.TabIndex = 247;
            this.label246.Text = "Total Gross AEP [GWh]:";
            // 
            // dataGrossHisto
            // 
            this.dataGrossHisto.AllowUserToAddRows = false;
            this.dataGrossHisto.AllowUserToDeleteRows = false;
            this.dataGrossHisto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrossHisto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrossHisto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn40});
            this.dataGrossHisto.Location = new System.Drawing.Point(915, 242);
            this.dataGrossHisto.Name = "dataGrossHisto";
            this.dataGrossHisto.ReadOnly = true;
            this.dataGrossHisto.RowHeadersVisible = false;
            this.dataGrossHisto.Size = new System.Drawing.Size(169, 449);
            this.dataGrossHisto.TabIndex = 222;
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.HeaderText = "Max. Value";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.ReadOnly = true;
            this.dataGridViewTextBoxColumn38.Width = 60;
            // 
            // dataGridViewTextBoxColumn39
            // 
            this.dataGridViewTextBoxColumn39.HeaderText = "Mets";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            this.dataGridViewTextBoxColumn39.ReadOnly = true;
            this.dataGridViewTextBoxColumn39.Width = 50;
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.HeaderText = "WTGs";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.ReadOnly = true;
            this.dataGridViewTextBoxColumn40.Width = 55;
            // 
            // dataGrossTurbEst
            // 
            this.dataGrossTurbEst.AllowUserToAddRows = false;
            this.dataGrossTurbEst.AllowUserToDeleteRows = false;
            this.dataGrossTurbEst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGrossTurbEst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrossTurbEst.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column121,
            this.dataGridViewTextBoxColumn35,
            this.dataGridViewTextBoxColumn36,
            this.dataGridViewTextBoxColumn37,
            this.Column77,
            this.Column78,
            this.Column79,
            this.Column80});
            this.dataGrossTurbEst.Location = new System.Drawing.Point(28, 158);
            this.dataGrossTurbEst.Name = "dataGrossTurbEst";
            this.dataGrossTurbEst.RowHeadersVisible = false;
            this.dataGrossTurbEst.Size = new System.Drawing.Size(577, 295);
            this.dataGrossTurbEst.TabIndex = 221;
            this.dataGrossTurbEst.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrossTurbEst_CellValueChanged);
            this.dataGrossTurbEst.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGrossTurbEst_CurrentCellDirtyStateChanged);
            // 
            // Column121
            // 
            this.Column121.HeaderText = "";
            this.Column121.Name = "Column121";
            this.Column121.Width = 30;
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.HeaderText = "Name";
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.ReadOnly = true;
            this.dataGridViewTextBoxColumn35.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn35.Width = 60;
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.HeaderText = "Elev. [m]";
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.ReadOnly = true;
            this.dataGridViewTextBoxColumn36.Width = 80;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.HeaderText = "WS [m/s]";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.ReadOnly = true;
            this.dataGridViewTextBoxColumn37.Width = 80;
            // 
            // Column77
            // 
            this.Column77.HeaderText = "AEP [MWh]";
            this.Column77.Name = "Column77";
            this.Column77.ReadOnly = true;
            this.Column77.Width = 80;
            // 
            // Column78
            // 
            this.Column78.HeaderText = "CF [%]";
            this.Column78.Name = "Column78";
            this.Column78.ReadOnly = true;
            this.Column78.Width = 80;
            // 
            // Column79
            // 
            this.Column79.HeaderText = "Weibull k";
            this.Column79.Name = "Column79";
            this.Column79.ReadOnly = true;
            this.Column79.Width = 80;
            // 
            // Column80
            // 
            this.Column80.HeaderText = "Weibull A";
            this.Column80.Name = "Column80";
            this.Column80.ReadOnly = true;
            this.Column80.Width = 80;
            // 
            // dataPowerCurveList
            // 
            this.dataPowerCurveList.AllowUserToAddRows = false;
            this.dataPowerCurveList.AllowUserToDeleteRows = false;
            this.dataPowerCurveList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPowerCurveList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataPowerCurveList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column74,
            this.colPowerCrvName,
            this.Column75,
            this.Column76});
            this.dataPowerCurveList.Location = new System.Drawing.Point(1102, 75);
            this.dataPowerCurveList.Name = "dataPowerCurveList";
            this.dataPowerCurveList.RowHeadersVisible = false;
            this.dataPowerCurveList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataPowerCurveList.Size = new System.Drawing.Size(354, 149);
            this.dataPowerCurveList.TabIndex = 220;
            this.dataPowerCurveList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataPowerCurveList_CellValueChanged);
            // 
            // Column74
            // 
            this.Column74.HeaderText = "";
            this.Column74.Name = "Column74";
            this.Column74.Width = 20;
            // 
            // colPowerCrvName
            // 
            this.colPowerCrvName.HeaderText = "Name";
            this.colPowerCrvName.Name = "colPowerCrvName";
            this.colPowerCrvName.ReadOnly = true;
            this.colPowerCrvName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colPowerCrvName.Width = 200;
            // 
            // Column75
            // 
            this.Column75.HeaderText = "RD [m]";
            this.Column75.Name = "Column75";
            this.Column75.ReadOnly = true;
            this.Column75.Width = 70;
            // 
            // Column76
            // 
            this.Column76.HeaderText = "RPM";
            this.Column76.Name = "Column76";
            this.Column76.ReadOnly = true;
            this.Column76.Width = 50;
            // 
            // plotGrossHisto
            // 
            this.plotGrossHisto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plotGrossHisto.Location = new System.Drawing.Point(293, 478);
            this.plotGrossHisto.Name = "plotGrossHisto";
            this.plotGrossHisto.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGrossHisto.Size = new System.Drawing.Size(558, 210);
            this.plotGrossHisto.TabIndex = 219;
            this.plotGrossHisto.Text = "plotView1";
            this.plotGrossHisto.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotGrossHisto.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotGrossHisto.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotGross_PowerCrvs
            // 
            this.plotGross_PowerCrvs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotGross_PowerCrvs.Location = new System.Drawing.Point(1102, 242);
            this.plotGross_PowerCrvs.MaximumSize = new System.Drawing.Size(400, 350);
            this.plotGross_PowerCrvs.Name = "plotGross_PowerCrvs";
            this.plotGross_PowerCrvs.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGross_PowerCrvs.Size = new System.Drawing.Size(354, 309);
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
            this.plotGrossWS_Dist.Location = new System.Drawing.Point(621, 199);
            this.plotGrossWS_Dist.MaximumSize = new System.Drawing.Size(400, 350);
            this.plotGrossWS_Dist.Name = "plotGrossWS_Dist";
            this.plotGrossWS_Dist.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGrossWS_Dist.Size = new System.Drawing.Size(274, 249);
            this.plotGrossWS_Dist.TabIndex = 217;
            this.plotGrossWS_Dist.Text = "plotView1";
            this.plotGrossWS_Dist.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotGrossWS_Dist.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotGrossWS_Dist.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotGrossWindRose
            // 
            this.plotGrossWindRose.Location = new System.Drawing.Point(621, 199);
            this.plotGrossWindRose.Name = "plotGrossWindRose";
            this.plotGrossWindRose.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotGrossWindRose.Size = new System.Drawing.Size(250, 250);
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
            this.btnGenTurbGross.Location = new System.Drawing.Point(28, 97);
            this.btnGenTurbGross.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGenTurbGross.Name = "btnGenTurbGross";
            this.btnGenTurbGross.Size = new System.Drawing.Size(178, 49);
            this.btnGenTurbGross.TabIndex = 191;
            this.btnGenTurbGross.Text = "Generate Gross Energy Estimates at Turbine Sites";
            this.btnGenTurbGross.UseVisualStyleBackColor = false;
            this.btnGenTurbGross.Click += new System.EventHandler(this.btnGenTurbGross_Click);
            // 
            // txtisMCPdGross
            // 
            this.txtisMCPdGross.Location = new System.Drawing.Point(699, 19);
            this.txtisMCPdGross.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtisMCPdGross.Name = "txtisMCPdGross";
            this.txtisMCPdGross.ReadOnly = true;
            this.txtisMCPdGross.Size = new System.Drawing.Size(90, 25);
            this.txtisMCPdGross.TabIndex = 189;
            this.txtisMCPdGross.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label40
            // 
            this.Label40.AutoSize = true;
            this.Label40.Location = new System.Drawing.Point(622, 168);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(92, 19);
            this.Label40.TabIndex = 184;
            this.Label40.Text = "Distribution:";
            this.Label40.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboWS_or_WD
            // 
            this.cboWS_or_WD.FormattingEnabled = true;
            this.cboWS_or_WD.Items.AddRange(new object[] {
            "WS",
            "WD"});
            this.cboWS_or_WD.Location = new System.Drawing.Point(717, 164);
            this.cboWS_or_WD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboWS_or_WD.Name = "cboWS_or_WD";
            this.cboWS_or_WD.Size = new System.Drawing.Size(56, 26);
            this.cboWS_or_WD.TabIndex = 183;
            this.cboWS_or_WD.Text = "WS";
            this.cboWS_or_WD.SelectedIndexChanged += new System.EventHandler(this.cboWS_or_WD_SelectedIndexChanged_1);
            // 
            // txtGross_FlowSepUsed
            // 
            this.txtGross_FlowSepUsed.Location = new System.Drawing.Point(891, 53);
            this.txtGross_FlowSepUsed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGross_FlowSepUsed.Name = "txtGross_FlowSepUsed";
            this.txtGross_FlowSepUsed.ReadOnly = true;
            this.txtGross_FlowSepUsed.Size = new System.Drawing.Size(90, 25);
            this.txtGross_FlowSepUsed.TabIndex = 182;
            this.txtGross_FlowSepUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label96
            // 
            this.Label96.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label96.AutoSize = true;
            this.Label96.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label96.Location = new System.Drawing.Point(912, 172);
            this.Label96.Name = "Label96";
            this.Label96.Size = new System.Drawing.Size(169, 18);
            this.Label96.TabIndex = 181;
            this.Label96.Text = "Histogram of Estimates";
            // 
            // txtGross_LC_used
            // 
            this.txtGross_LC_used.Location = new System.Drawing.Point(795, 19);
            this.txtGross_LC_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGross_LC_used.Name = "txtGross_LC_used";
            this.txtGross_LC_used.ReadOnly = true;
            this.txtGross_LC_used.Size = new System.Drawing.Size(90, 25);
            this.txtGross_LC_used.TabIndex = 179;
            this.txtGross_LC_used.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.Label79.Location = new System.Drawing.Point(25, 49);
            this.Label79.Name = "Label79";
            this.Label79.Size = new System.Drawing.Size(51, 36);
            this.Label79.TabIndex = 176;
            this.Label79.Text = "Power \r\nCurve :";
            // 
            // Label77
            // 
            this.Label77.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label77.AutoSize = true;
            this.Label77.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label77.Location = new System.Drawing.Point(909, 206);
            this.Label77.Name = "Label77";
            this.Label77.Size = new System.Drawing.Size(57, 18);
            this.Label77.TabIndex = 161;
            this.Label77.Text = "Param. :";
            // 
            // cboGrossParam
            // 
            this.cboGrossParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboGrossParam.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGrossParam.FormattingEnabled = true;
            this.cboGrossParam.Items.AddRange(new object[] {
            "Wind Speed",
            "Gross AEP",
            "Elevation",
            "Weibull k",
            "Weibull A"});
            this.cboGrossParam.Location = new System.Drawing.Point(977, 201);
            this.cboGrossParam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboGrossParam.Name = "cboGrossParam";
            this.cboGrossParam.Size = new System.Drawing.Size(104, 26);
            this.cboGrossParam.TabIndex = 160;
            this.cboGrossParam.Text = "Wind Speed";
            this.cboGrossParam.SelectedIndexChanged += new System.EventHandler(this.cboGrossParam_SelectedIndexChanged);
            // 
            // btnExportDirWSDists
            // 
            this.btnExportDirWSDists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportDirWSDists.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportDirWSDists.Location = new System.Drawing.Point(1277, 640);
            this.btnExportDirWSDists.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportDirWSDists.Name = "btnExportDirWSDists";
            this.btnExportDirWSDists.Size = new System.Drawing.Size(120, 55);
            this.btnExportDirWSDists.TabIndex = 102;
            this.btnExportDirWSDists.Text = "Export Sector WS Distributions";
            this.btnExportDirWSDists.UseVisualStyleBackColor = true;
            this.btnExportDirWSDists.Click += new System.EventHandler(this.btnExportDirWSDists_Click);
            // 
            // Label71
            // 
            this.Label71.AutoSize = true;
            this.Label71.Location = new System.Drawing.Point(397, 57);
            this.Label71.Name = "Label71";
            this.Label71.Size = new System.Drawing.Size(42, 19);
            this.Label71.TabIndex = 159;
            this.Label71.Text = "WD :";
            // 
            // cboGrossWD
            // 
            this.cboGrossWD.FormattingEnabled = true;
            this.cboGrossWD.Location = new System.Drawing.Point(439, 54);
            this.cboGrossWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboGrossWD.Name = "cboGrossWD";
            this.cboGrossWD.Size = new System.Drawing.Size(75, 26);
            this.cboGrossWD.TabIndex = 158;
            this.cboGrossWD.SelectedIndexChanged += new System.EventHandler(this.cboGrossTurbWD_SelectedIndexChanged);
            // 
            // btnExportDirWS
            // 
            this.btnExportDirWS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportDirWS.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.btnExportDirWS.Location = new System.Drawing.Point(1152, 640);
            this.btnExportDirWS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportDirWS.Name = "btnExportDirWS";
            this.btnExportDirWS.Size = new System.Drawing.Size(120, 55);
            this.btnExportDirWS.TabIndex = 157;
            this.btnExportDirWS.Text = "Export Sector WS && Weibull";
            this.btnExportDirWS.UseVisualStyleBackColor = true;
            this.btnExportDirWS.Click += new System.EventHandler(this.btnExportDirWS_Click);
            // 
            // btnExportWSDists
            // 
            this.btnExportWSDists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportWSDists.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportWSDists.Location = new System.Drawing.Point(1277, 581);
            this.btnExportWSDists.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportWSDists.Name = "btnExportWSDists";
            this.btnExportWSDists.Size = new System.Drawing.Size(120, 55);
            this.btnExportWSDists.TabIndex = 101;
            this.btnExportWSDists.Text = "Export WS Distributions";
            this.btnExportWSDists.UseVisualStyleBackColor = true;
            this.btnExportWSDists.Click += new System.EventHandler(this.btnExportWSDists_Click);
            // 
            // btnWS_AEP_Exprt
            // 
            this.btnWS_AEP_Exprt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWS_AEP_Exprt.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnWS_AEP_Exprt.Location = new System.Drawing.Point(1152, 581);
            this.btnWS_AEP_Exprt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnWS_AEP_Exprt.Name = "btnWS_AEP_Exprt";
            this.btnWS_AEP_Exprt.Size = new System.Drawing.Size(120, 55);
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
            // btnImportCRV
            // 
            this.btnImportCRV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportCRV.BackColor = System.Drawing.Color.LightCoral;
            this.btnImportCRV.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnImportCRV.Location = new System.Drawing.Point(1277, 34);
            this.btnImportCRV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportCRV.Name = "btnImportCRV";
            this.btnImportCRV.Size = new System.Drawing.Size(30, 29);
            this.btnImportCRV.TabIndex = 34;
            this.btnImportCRV.Text = "+";
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
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(1098, 37);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(177, 19);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Power && Thrust Curves ";
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
            this.cboPowerCrvs.Location = new System.Drawing.Point(77, 53);
            this.cboPowerCrvs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboPowerCrvs.Name = "cboPowerCrvs";
            this.cboPowerCrvs.Size = new System.Drawing.Size(303, 26);
            this.cboPowerCrvs.TabIndex = 1;
            this.cboPowerCrvs.SelectedIndexChanged += new System.EventHandler(this.cboPowerCrvs_SelectedIndexChanged);
            // 
            // pgeExceedance
            // 
            this.pgeExceedance.AutoScroll = true;
            this.pgeExceedance.Controls.Add(this.dataPvals);
            this.pgeExceedance.Controls.Add(this.dataDefinedLosses);
            this.pgeExceedance.Controls.Add(this.btnSetToIEC_LossUncert);
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
            this.pgeExceedance.Controls.Add(this.chkShowCDFs);
            this.pgeExceedance.Controls.Add(this.chkShowPDF);
            this.pgeExceedance.Controls.Add(this.btnDoMonteCarlo);
            this.pgeExceedance.Controls.Add(this.label181);
            this.pgeExceedance.Controls.Add(this.label182);
            this.pgeExceedance.Controls.Add(this.btn_editloss);
            this.pgeExceedance.Location = new System.Drawing.Point(4, 27);
            this.pgeExceedance.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeExceedance.Name = "pgeExceedance";
            this.pgeExceedance.Size = new System.Drawing.Size(1488, 712);
            this.pgeExceedance.TabIndex = 19;
            this.pgeExceedance.Text = "Exceedance Modeling";
            this.pgeExceedance.UseVisualStyleBackColor = true;
            // 
            // dataPvals
            // 
            this.dataPvals.AllowUserToAddRows = false;
            this.dataPvals.AllowUserToDeleteRows = false;
            this.dataPvals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dataPvals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataPvals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn44,
            this.dataGridViewTextBoxColumn45,
            this.dataGridViewTextBoxColumn46,
            this.dataGridViewTextBoxColumn47,
            this.dataGridViewTextBoxColumn48,
            this.dataGridViewTextBoxColumn49,
            this.dataGridViewTextBoxColumn50});
            this.dataPvals.Location = new System.Drawing.Point(32, 521);
            this.dataPvals.Name = "dataPvals";
            this.dataPvals.ReadOnly = true;
            this.dataPvals.RowHeadersVisible = false;
            this.dataPvals.Size = new System.Drawing.Size(774, 179);
            this.dataPvals.TabIndex = 240;
            // 
            // dataGridViewTextBoxColumn44
            // 
            this.dataGridViewTextBoxColumn44.HeaderText = "P Value";
            this.dataGridViewTextBoxColumn44.Name = "dataGridViewTextBoxColumn44";
            this.dataGridViewTextBoxColumn44.ReadOnly = true;
            this.dataGridViewTextBoxColumn44.Width = 200;
            // 
            // dataGridViewTextBoxColumn45
            // 
            this.dataGridViewTextBoxColumn45.HeaderText = "1 yr PF";
            this.dataGridViewTextBoxColumn45.Name = "dataGridViewTextBoxColumn45";
            this.dataGridViewTextBoxColumn45.ReadOnly = true;
            this.dataGridViewTextBoxColumn45.Width = 90;
            // 
            // dataGridViewTextBoxColumn46
            // 
            this.dataGridViewTextBoxColumn46.HeaderText = "1 yr AEP";
            this.dataGridViewTextBoxColumn46.Name = "dataGridViewTextBoxColumn46";
            this.dataGridViewTextBoxColumn46.ReadOnly = true;
            this.dataGridViewTextBoxColumn46.Width = 90;
            // 
            // dataGridViewTextBoxColumn47
            // 
            this.dataGridViewTextBoxColumn47.HeaderText = "10 yr PF";
            this.dataGridViewTextBoxColumn47.Name = "dataGridViewTextBoxColumn47";
            this.dataGridViewTextBoxColumn47.ReadOnly = true;
            this.dataGridViewTextBoxColumn47.Width = 90;
            // 
            // dataGridViewTextBoxColumn48
            // 
            this.dataGridViewTextBoxColumn48.HeaderText = "10 yr AEP";
            this.dataGridViewTextBoxColumn48.Name = "dataGridViewTextBoxColumn48";
            this.dataGridViewTextBoxColumn48.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn49
            // 
            this.dataGridViewTextBoxColumn49.HeaderText = "20 yr PF";
            this.dataGridViewTextBoxColumn49.Name = "dataGridViewTextBoxColumn49";
            this.dataGridViewTextBoxColumn49.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn50
            // 
            this.dataGridViewTextBoxColumn50.HeaderText = "20 yr AEP";
            this.dataGridViewTextBoxColumn50.Name = "dataGridViewTextBoxColumn50";
            this.dataGridViewTextBoxColumn50.ReadOnly = true;
            // 
            // dataDefinedLosses
            // 
            this.dataDefinedLosses.AllowUserToAddRows = false;
            this.dataDefinedLosses.AllowUserToDeleteRows = false;
            this.dataDefinedLosses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataDefinedLosses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataDefinedLosses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column92,
            this.dataGridViewTextBoxColumn41,
            this.dataGridViewTextBoxColumn42,
            this.dataGridViewTextBoxColumn43,
            this.Column81,
            this.Column82,
            this.Column83,
            this.Column84,
            this.Column85});
            this.dataDefinedLosses.Location = new System.Drawing.Point(29, 88);
            this.dataDefinedLosses.Name = "dataDefinedLosses";
            this.dataDefinedLosses.RowHeadersVisible = false;
            this.dataDefinedLosses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataDefinedLosses.Size = new System.Drawing.Size(920, 318);
            this.dataDefinedLosses.TabIndex = 239;
            this.dataDefinedLosses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataDefinedLosses_CellContentClick);
            this.dataDefinedLosses.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataDefinedLosses_CellValueChanged);
            this.dataDefinedLosses.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataDefinedLosses_CurrentCellDirtyStateChanged);
            // 
            // Column92
            // 
            this.Column92.HeaderText = "Select";
            this.Column92.Name = "Column92";
            this.Column92.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column92.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column92.Width = 20;
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.HeaderText = "Name";
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            this.dataGridViewTextBoxColumn41.ReadOnly = true;
            this.dataGridViewTextBoxColumn41.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn41.Width = 200;
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.HeaderText = "P10";
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            this.dataGridViewTextBoxColumn42.ReadOnly = true;
            this.dataGridViewTextBoxColumn42.Width = 90;
            // 
            // dataGridViewTextBoxColumn43
            // 
            this.dataGridViewTextBoxColumn43.HeaderText = "P50";
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.ReadOnly = true;
            this.dataGridViewTextBoxColumn43.Width = 90;
            // 
            // Column81
            // 
            this.Column81.HeaderText = "P90";
            this.Column81.Name = "Column81";
            this.Column81.ReadOnly = true;
            this.Column81.Width = 90;
            // 
            // Column82
            // 
            this.Column82.HeaderText = "Lower";
            this.Column82.Name = "Column82";
            this.Column82.ReadOnly = true;
            // 
            // Column83
            // 
            this.Column83.HeaderText = "Upper";
            this.Column83.Name = "Column83";
            this.Column83.ReadOnly = true;
            // 
            // Column84
            // 
            this.Column84.HeaderText = "Mean";
            this.Column84.Name = "Column84";
            this.Column84.ReadOnly = true;
            // 
            // Column85
            // 
            this.Column85.HeaderText = "SD";
            this.Column85.Name = "Column85";
            this.Column85.ReadOnly = true;
            // 
            // btnSetToIEC_LossUncert
            // 
            this.btnSetToIEC_LossUncert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetToIEC_LossUncert.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetToIEC_LossUncert.Location = new System.Drawing.Point(679, 420);
            this.btnSetToIEC_LossUncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetToIEC_LossUncert.Name = "btnSetToIEC_LossUncert";
            this.btnSetToIEC_LossUncert.Size = new System.Drawing.Size(131, 35);
            this.btnSetToIEC_LossUncert.TabIndex = 238;
            this.btnSetToIEC_LossUncert.Text = "Set to IEC Default";
            this.btnSetToIEC_LossUncert.UseVisualStyleBackColor = true;
            this.btnSetToIEC_LossUncert.Click += new System.EventHandler(this.btnSetToIEC_LossUncert_Click);
            // 
            // plotCompositeExceed
            // 
            this.plotCompositeExceed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotCompositeExceed.Location = new System.Drawing.Point(994, 435);
            this.plotCompositeExceed.Name = "plotCompositeExceed";
            this.plotCompositeExceed.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotCompositeExceed.Size = new System.Drawing.Size(474, 262);
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
            this.plotExceedCurves.Location = new System.Drawing.Point(994, 18);
            this.plotExceedCurves.Name = "plotExceedCurves";
            this.plotExceedCurves.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotExceedCurves.Size = new System.Drawing.Size(474, 377);
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
            this.btnImportCurves.Location = new System.Drawing.Point(413, 420);
            this.btnImportCurves.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportCurves.Name = "btnImportCurves";
            this.btnImportCurves.Size = new System.Drawing.Size(124, 35);
            this.btnImportCurves.TabIndex = 235;
            this.btnImportCurves.Text = "Import Curves";
            this.btnImportCurves.UseVisualStyleBackColor = true;
            this.btnImportCurves.Click += new System.EventHandler(this.btnImportCurves_Click);
            // 
            // btnGetDefaultExceed
            // 
            this.btnGetDefaultExceed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetDefaultExceed.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetDefaultExceed.Location = new System.Drawing.Point(545, 420);
            this.btnGetDefaultExceed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGetDefaultExceed.Name = "btnGetDefaultExceed";
            this.btnGetDefaultExceed.Size = new System.Drawing.Size(126, 35);
            this.btnGetDefaultExceed.TabIndex = 234;
            this.btnGetDefaultExceed.Text = "Set to Default";
            this.btnGetDefaultExceed.UseVisualStyleBackColor = true;
            this.btnGetDefaultExceed.Click += new System.EventHandler(this.btnGetDefaultExceed_Click);
            // 
            // cboExceedWake
            // 
            this.cboExceedWake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboExceedWake.FormattingEnabled = true;
            this.cboExceedWake.Location = new System.Drawing.Point(482, 485);
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
            this.label185.Location = new System.Drawing.Point(484, 463);
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
            this.label183.Location = new System.Drawing.Point(340, 463);
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
            this.cboExceedTurbine.Location = new System.Drawing.Point(343, 485);
            this.cboExceedTurbine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboExceedTurbine.Name = "cboExceedTurbine";
            this.cboExceedTurbine.Size = new System.Drawing.Size(125, 26);
            this.cboExceedTurbine.TabIndex = 219;
            this.cboExceedTurbine.SelectedIndexChanged += new System.EventHandler(this.cboExceedTurbine_SelectedIndexChanged);
            // 
            // btn_AddProj
            // 
            this.btn_AddProj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_AddProj.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddProj.Location = new System.Drawing.Point(29, 420);
            this.btn_AddProj.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddProj.Name = "btn_AddProj";
            this.btn_AddProj.Size = new System.Drawing.Size(111, 35);
            this.btn_AddProj.TabIndex = 44;
            this.btn_AddProj.Text = "Add Curve";
            this.btn_AddProj.UseVisualStyleBackColor = true;
            this.btn_AddProj.Click += new System.EventHandler(this.btn_AddProj_Click);
            // 
            // btnExportAllPVals
            // 
            this.btnExportAllPVals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportAllPVals.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportAllPVals.Location = new System.Drawing.Point(820, 617);
            this.btnExportAllPVals.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportAllPVals.Name = "btnExportAllPVals";
            this.btnExportAllPVals.Size = new System.Drawing.Size(129, 59);
            this.btnExportAllPVals.TabIndex = 39;
            this.btnExportAllPVals.Text = "Export All P Values";
            this.btnExportAllPVals.UseVisualStyleBackColor = true;
            this.btnExportAllPVals.Click += new System.EventHandler(this.btnExportAllPVals_Click);
            // 
            // btnDeleteExceed
            // 
            this.btnDeleteExceed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteExceed.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteExceed.Location = new System.Drawing.Point(276, 420);
            this.btnDeleteExceed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDeleteExceed.Name = "btnDeleteExceed";
            this.btnDeleteExceed.Size = new System.Drawing.Size(129, 35);
            this.btnDeleteExceed.TabIndex = 38;
            this.btnDeleteExceed.Text = "Delete Curve(s)";
            this.btnDeleteExceed.UseVisualStyleBackColor = true;
            this.btnDeleteExceed.Click += new System.EventHandler(this.btnDeleteExceed_Click);
            // 
            // btnExportCurves
            // 
            this.btnExportCurves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportCurves.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCurves.Location = new System.Drawing.Point(818, 420);
            this.btnExportCurves.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportCurves.Name = "btnExportCurves";
            this.btnExportCurves.Size = new System.Drawing.Size(131, 35);
            this.btnExportCurves.TabIndex = 37;
            this.btnExportCurves.Text = "Export Curves";
            this.btnExportCurves.UseVisualStyleBackColor = true;
            this.btnExportCurves.Click += new System.EventHandler(this.btnExportCurves_Click);
            // 
            // btnExport_P_Vals
            // 
            this.btnExport_P_Vals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport_P_Vals.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport_P_Vals.Location = new System.Drawing.Point(820, 561);
            this.btnExport_P_Vals.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExport_P_Vals.Name = "btnExport_P_Vals";
            this.btnExport_P_Vals.Size = new System.Drawing.Size(129, 38);
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
            // chkShowCDFs
            // 
            this.chkShowCDFs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowCDFs.AutoSize = true;
            this.chkShowCDFs.Checked = true;
            this.chkShowCDFs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowCDFs.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowCDFs.Location = new System.Drawing.Point(1276, 402);
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
            this.chkShowPDF.Location = new System.Drawing.Point(1136, 402);
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
            this.btnDoMonteCarlo.Location = new System.Drawing.Point(820, 473);
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
            // btn_editloss
            // 
            this.btn_editloss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_editloss.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_editloss.Location = new System.Drawing.Point(148, 420);
            this.btn_editloss.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_editloss.Name = "btn_editloss";
            this.btn_editloss.Size = new System.Drawing.Size(120, 35);
            this.btn_editloss.TabIndex = 25;
            this.btn_editloss.Text = "Edit Curve";
            this.btn_editloss.UseVisualStyleBackColor = true;
            this.btn_editloss.Click += new System.EventHandler(this.btn_editloss_Click);
            // 
            // pgeNetEsts
            // 
            this.pgeNetEsts.AutoScroll = true;
            this.pgeNetEsts.Controls.Add(this.txtValley_Net);
            this.pgeNetEsts.Controls.Add(this.txtElev_Net);
            this.pgeNetEsts.Controls.Add(this.txtOverallNetCF);
            this.pgeNetEsts.Controls.Add(this.label122);
            this.pgeNetEsts.Controls.Add(this.txtOverallNetAEP);
            this.pgeNetEsts.Controls.Add(this.label86);
            this.pgeNetEsts.Controls.Add(this.dataWakedTurbs);
            this.pgeNetEsts.Controls.Add(this.dataWakeModels);
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
            this.pgeNetEsts.Controls.Add(this.btnDelWakeModel);
            this.pgeNetEsts.Controls.Add(this.btnDelWakeGrid);
            this.pgeNetEsts.Controls.Add(this.cboWakePlot);
            this.pgeNetEsts.Controls.Add(this.Label85);
            this.pgeNetEsts.Controls.Add(this.btnExportNetEsts);
            this.pgeNetEsts.Controls.Add(this.Label55);
            this.pgeNetEsts.Controls.Add(this.cboNetWD);
            this.pgeNetEsts.Controls.Add(this.btnWakeLossCalc);
            this.pgeNetEsts.Controls.Add(this.Label53);
            this.pgeNetEsts.Controls.Add(this.Label36);
            this.pgeNetEsts.Location = new System.Drawing.Point(4, 27);
            this.pgeNetEsts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeNetEsts.Name = "pgeNetEsts";
            this.pgeNetEsts.Size = new System.Drawing.Size(1488, 712);
            this.pgeNetEsts.TabIndex = 13;
            this.pgeNetEsts.Text = "Net Turbine Ests";
            this.pgeNetEsts.UseVisualStyleBackColor = true;
            // 
            // txtValley_Net
            // 
            this.txtValley_Net.Location = new System.Drawing.Point(392, 39);
            this.txtValley_Net.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtValley_Net.Name = "txtValley_Net";
            this.txtValley_Net.ReadOnly = true;
            this.txtValley_Net.Size = new System.Drawing.Size(110, 25);
            this.txtValley_Net.TabIndex = 248;
            this.txtValley_Net.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtElev_Net
            // 
            this.txtElev_Net.Location = new System.Drawing.Point(509, 6);
            this.txtElev_Net.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtElev_Net.Name = "txtElev_Net";
            this.txtElev_Net.ReadOnly = true;
            this.txtElev_Net.Size = new System.Drawing.Size(110, 25);
            this.txtElev_Net.TabIndex = 247;
            this.txtElev_Net.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOverallNetCF
            // 
            this.txtOverallNetCF.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOverallNetCF.Location = new System.Drawing.Point(867, 308);
            this.txtOverallNetCF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOverallNetCF.Name = "txtOverallNetCF";
            this.txtOverallNetCF.Size = new System.Drawing.Size(67, 29);
            this.txtOverallNetCF.TabIndex = 246;
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label122.Location = new System.Drawing.Point(756, 312);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(91, 19);
            this.label122.TabIndex = 245;
            this.label122.Text = "Net CF [%]:";
            // 
            // txtOverallNetAEP
            // 
            this.txtOverallNetAEP.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOverallNetAEP.Location = new System.Drawing.Point(867, 274);
            this.txtOverallNetAEP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOverallNetAEP.Name = "txtOverallNetAEP";
            this.txtOverallNetAEP.Size = new System.Drawing.Size(67, 29);
            this.txtOverallNetAEP.TabIndex = 244;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.Location = new System.Drawing.Point(684, 278);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(163, 19);
            this.label86.TabIndex = 243;
            this.label86.Text = "Total Net AEP [GWh]:";
            // 
            // dataWakedTurbs
            // 
            this.dataWakedTurbs.AllowUserToAddRows = false;
            this.dataWakedTurbs.AllowUserToDeleteRows = false;
            this.dataWakedTurbs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataWakedTurbs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataWakedTurbs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn59,
            this.dataGridViewTextBoxColumn61,
            this.dataGridViewTextBoxColumn62,
            this.dataGridViewTextBoxColumn63,
            this.dataGridViewTextBoxColumn64,
            this.dataGridViewTextBoxColumn65,
            this.dataGridViewTextBoxColumn66,
            this.dataGridViewTextBoxColumn67});
            this.dataWakedTurbs.Location = new System.Drawing.Point(15, 71);
            this.dataWakedTurbs.Name = "dataWakedTurbs";
            this.dataWakedTurbs.ReadOnly = true;
            this.dataWakedTurbs.RowHeadersVisible = false;
            this.dataWakedTurbs.Size = new System.Drawing.Size(620, 368);
            this.dataWakedTurbs.TabIndex = 242;
            // 
            // dataGridViewTextBoxColumn59
            // 
            this.dataGridViewTextBoxColumn59.HeaderText = "Site";
            this.dataGridViewTextBoxColumn59.Name = "dataGridViewTextBoxColumn59";
            this.dataGridViewTextBoxColumn59.ReadOnly = true;
            this.dataGridViewTextBoxColumn59.Width = 80;
            // 
            // dataGridViewTextBoxColumn61
            // 
            this.dataGridViewTextBoxColumn61.HeaderText = "Elev. [m]";
            this.dataGridViewTextBoxColumn61.Name = "dataGridViewTextBoxColumn61";
            this.dataGridViewTextBoxColumn61.ReadOnly = true;
            this.dataGridViewTextBoxColumn61.Width = 70;
            // 
            // dataGridViewTextBoxColumn62
            // 
            this.dataGridViewTextBoxColumn62.HeaderText = "WS [m/s]";
            this.dataGridViewTextBoxColumn62.Name = "dataGridViewTextBoxColumn62";
            this.dataGridViewTextBoxColumn62.ReadOnly = true;
            this.dataGridViewTextBoxColumn62.Width = 70;
            // 
            // dataGridViewTextBoxColumn63
            // 
            this.dataGridViewTextBoxColumn63.HeaderText = "Net AEP [MWh]";
            this.dataGridViewTextBoxColumn63.Name = "dataGridViewTextBoxColumn63";
            this.dataGridViewTextBoxColumn63.ReadOnly = true;
            this.dataGridViewTextBoxColumn63.Width = 70;
            // 
            // dataGridViewTextBoxColumn64
            // 
            this.dataGridViewTextBoxColumn64.HeaderText = "Net CF [%]";
            this.dataGridViewTextBoxColumn64.Name = "dataGridViewTextBoxColumn64";
            this.dataGridViewTextBoxColumn64.ReadOnly = true;
            this.dataGridViewTextBoxColumn64.Width = 80;
            // 
            // dataGridViewTextBoxColumn65
            // 
            this.dataGridViewTextBoxColumn65.HeaderText = "Wake Loss [%]";
            this.dataGridViewTextBoxColumn65.Name = "dataGridViewTextBoxColumn65";
            this.dataGridViewTextBoxColumn65.ReadOnly = true;
            this.dataGridViewTextBoxColumn65.Width = 80;
            // 
            // dataGridViewTextBoxColumn66
            // 
            this.dataGridViewTextBoxColumn66.HeaderText = "Weibull k";
            this.dataGridViewTextBoxColumn66.Name = "dataGridViewTextBoxColumn66";
            this.dataGridViewTextBoxColumn66.ReadOnly = true;
            this.dataGridViewTextBoxColumn66.Width = 70;
            // 
            // dataGridViewTextBoxColumn67
            // 
            this.dataGridViewTextBoxColumn67.HeaderText = "Weibull A";
            this.dataGridViewTextBoxColumn67.Name = "dataGridViewTextBoxColumn67";
            this.dataGridViewTextBoxColumn67.ReadOnly = true;
            this.dataGridViewTextBoxColumn67.Width = 70;
            // 
            // dataWakeModels
            // 
            this.dataWakeModels.AllowUserToAddRows = false;
            this.dataWakeModels.AllowUserToDeleteRows = false;
            this.dataWakeModels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataWakeModels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataWakeModels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn51,
            this.dataGridViewTextBoxColumn52,
            this.dataGridViewTextBoxColumn53,
            this.dataGridViewTextBoxColumn55,
            this.dataGridViewTextBoxColumn56,
            this.dataGridViewTextBoxColumn57,
            this.dataGridViewTextBoxColumn58,
            this.Column86,
            this.Column87,
            this.Column88,
            this.Column89,
            this.Column90,
            this.Column91});
            this.dataWakeModels.Location = new System.Drawing.Point(657, 71);
            this.dataWakeModels.Name = "dataWakeModels";
            this.dataWakeModels.ReadOnly = true;
            this.dataWakeModels.RowHeadersVisible = false;
            this.dataWakeModels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataWakeModels.Size = new System.Drawing.Size(817, 125);
            this.dataWakeModels.TabIndex = 241;
            this.dataWakeModels.SelectionChanged += new System.EventHandler(this.dataWakeModels_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn51
            // 
            this.dataGridViewTextBoxColumn51.HeaderText = "Wake Model";
            this.dataGridViewTextBoxColumn51.Name = "dataGridViewTextBoxColumn51";
            this.dataGridViewTextBoxColumn51.ReadOnly = true;
            this.dataGridViewTextBoxColumn51.Width = 200;
            // 
            // dataGridViewTextBoxColumn52
            // 
            this.dataGridViewTextBoxColumn52.HeaderText = "Combo";
            this.dataGridViewTextBoxColumn52.Name = "dataGridViewTextBoxColumn52";
            this.dataGridViewTextBoxColumn52.ReadOnly = true;
            this.dataGridViewTextBoxColumn52.Width = 90;
            // 
            // dataGridViewTextBoxColumn53
            // 
            this.dataGridViewTextBoxColumn53.HeaderText = "Turbine";
            this.dataGridViewTextBoxColumn53.Name = "dataGridViewTextBoxColumn53";
            this.dataGridViewTextBoxColumn53.ReadOnly = true;
            this.dataGridViewTextBoxColumn53.Width = 90;
            // 
            // dataGridViewTextBoxColumn55
            // 
            this.dataGridViewTextBoxColumn55.HeaderText = "Exp. Angle";
            this.dataGridViewTextBoxColumn55.Name = "dataGridViewTextBoxColumn55";
            this.dataGridViewTextBoxColumn55.ReadOnly = true;
            this.dataGridViewTextBoxColumn55.Width = 60;
            // 
            // dataGridViewTextBoxColumn56
            // 
            this.dataGridViewTextBoxColumn56.HeaderText = "Amb. TI";
            this.dataGridViewTextBoxColumn56.Name = "dataGridViewTextBoxColumn56";
            this.dataGridViewTextBoxColumn56.ReadOnly = true;
            this.dataGridViewTextBoxColumn56.Width = 60;
            // 
            // dataGridViewTextBoxColumn57
            // 
            this.dataGridViewTextBoxColumn57.HeaderText = "DW Spc";
            this.dataGridViewTextBoxColumn57.Name = "dataGridViewTextBoxColumn57";
            this.dataGridViewTextBoxColumn57.ReadOnly = true;
            this.dataGridViewTextBoxColumn57.Width = 60;
            // 
            // dataGridViewTextBoxColumn58
            // 
            this.dataGridViewTextBoxColumn58.HeaderText = "CW Spc";
            this.dataGridViewTextBoxColumn58.Name = "dataGridViewTextBoxColumn58";
            this.dataGridViewTextBoxColumn58.ReadOnly = true;
            this.dataGridViewTextBoxColumn58.Width = 60;
            // 
            // Column86
            // 
            this.Column86.HeaderText = "Roughness";
            this.Column86.Name = "Column86";
            this.Column86.ReadOnly = true;
            // 
            // Column87
            // 
            this.Column87.HeaderText = "Min X";
            this.Column87.Name = "Column87";
            this.Column87.ReadOnly = true;
            this.Column87.Width = 60;
            // 
            // Column88
            // 
            this.Column88.HeaderText = "Min Y";
            this.Column88.Name = "Column88";
            this.Column88.ReadOnly = true;
            this.Column88.Width = 60;
            // 
            // Column89
            // 
            this.Column89.HeaderText = "Max X";
            this.Column89.Name = "Column89";
            this.Column89.ReadOnly = true;
            this.Column89.Width = 60;
            // 
            // Column90
            // 
            this.Column90.HeaderText = "Max Y";
            this.Column90.Name = "Column90";
            this.Column90.ReadOnly = true;
            this.Column90.Width = 60;
            // 
            // Column91
            // 
            this.Column91.HeaderText = "Map Reso. [m]";
            this.Column91.Name = "Column91";
            this.Column91.ReadOnly = true;
            this.Column91.Width = 60;
            // 
            // plotWakeMap
            // 
            this.plotWakeMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotWakeMap.Location = new System.Drawing.Point(983, 207);
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
            this.txtisMCPdNet.Location = new System.Drawing.Point(256, 7);
            this.txtisMCPdNet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtisMCPdNet.Name = "txtisMCPdNet";
            this.txtisMCPdNet.ReadOnly = true;
            this.txtisMCPdNet.Size = new System.Drawing.Size(111, 25);
            this.txtisMCPdNet.TabIndex = 199;
            this.txtisMCPdNet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOtherLosses
            // 
            this.txtOtherLosses.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherLosses.Location = new System.Drawing.Point(867, 376);
            this.txtOtherLosses.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOtherLosses.Name = "txtOtherLosses";
            this.txtOtherLosses.Size = new System.Drawing.Size(67, 29);
            this.txtOtherLosses.TabIndex = 198;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(715, 380);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(132, 19);
            this.label88.TabIndex = 197;
            this.label88.Text = "Other Losses [%]:";
            // 
            // txtNet_FlowSep_Used
            // 
            this.txtNet_FlowSep_Used.Location = new System.Drawing.Point(509, 39);
            this.txtNet_FlowSep_Used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNet_FlowSep_Used.Name = "txtNet_FlowSep_Used";
            this.txtNet_FlowSep_Used.ReadOnly = true;
            this.txtNet_FlowSep_Used.Size = new System.Drawing.Size(110, 25);
            this.txtNet_FlowSep_Used.TabIndex = 193;
            this.txtNet_FlowSep_Used.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.txtLC_Net.Location = new System.Drawing.Point(392, 7);
            this.txtLC_Net.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLC_Net.Name = "txtLC_Net";
            this.txtLC_Net.ReadOnly = true;
            this.txtLC_Net.Size = new System.Drawing.Size(110, 25);
            this.txtLC_Net.TabIndex = 180;
            this.txtLC_Net.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCreateWakeMap
            // 
            this.btnCreateWakeMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateWakeMap.BackColor = System.Drawing.Color.LightCoral;
            this.btnCreateWakeMap.Location = new System.Drawing.Point(1189, 12);
            this.btnCreateWakeMap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCreateWakeMap.Name = "btnCreateWakeMap";
            this.btnCreateWakeMap.Size = new System.Drawing.Size(136, 51);
            this.btnCreateWakeMap.TabIndex = 68;
            this.btnCreateWakeMap.Text = "Create Wake Map";
            this.btnCreateWakeMap.UseVisualStyleBackColor = false;
            this.btnCreateWakeMap.Click += new System.EventHandler(this.btnCreateWakeMap_Click);
            // 
            // txtWakeLoss
            // 
            this.txtWakeLoss.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWakeLoss.Location = new System.Drawing.Point(867, 342);
            this.txtWakeLoss.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWakeLoss.Name = "txtWakeLoss";
            this.txtWakeLoss.Size = new System.Drawing.Size(67, 29);
            this.txtWakeLoss.TabIndex = 65;
            // 
            // Label94
            // 
            this.Label94.AutoSize = true;
            this.Label94.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label94.Location = new System.Drawing.Point(729, 346);
            this.Label94.Name = "Label94";
            this.Label94.Size = new System.Drawing.Size(118, 19);
            this.Label94.TabIndex = 64;
            this.Label94.Text = "Wake Loss [%]:";
            // 
            // btnDelWakeModel
            // 
            this.btnDelWakeModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelWakeModel.Location = new System.Drawing.Point(1069, 12);
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
            this.btnDelWakeGrid.Size = new System.Drawing.Size(113, 51);
            this.btnDelWakeGrid.TabIndex = 12;
            this.btnDelWakeGrid.Text = "Delete Wake Map";
            this.btnDelWakeGrid.UseVisualStyleBackColor = true;
            this.btnDelWakeGrid.Click += new System.EventHandler(this.btnDelWakeGrid_Click);
            // 
            // cboWakePlot
            // 
            this.cboWakePlot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.Label85.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.Label55.Location = new System.Drawing.Point(675, 223);
            this.Label55.Name = "Label55";
            this.Label55.Size = new System.Drawing.Size(124, 19);
            this.Label55.TabIndex = 8;
            this.Label55.Text = "Wind Direction:";
            // 
            // cboNetWD
            // 
            this.cboNetWD.FormattingEnabled = true;
            this.cboNetWD.Location = new System.Drawing.Point(805, 216);
            this.cboNetWD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboNetWD.Name = "cboNetWD";
            this.cboNetWD.Size = new System.Drawing.Size(129, 26);
            this.cboNetWD.TabIndex = 7;
            this.cboNetWD.SelectedIndexChanged += new System.EventHandler(this.cboTurbEstWD_SelectedIndexChanged);
            // 
            // btnWakeLossCalc
            // 
            this.btnWakeLossCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWakeLossCalc.BackColor = System.Drawing.Color.LightCoral;
            this.btnWakeLossCalc.Location = new System.Drawing.Point(822, 12);
            this.btnWakeLossCalc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnWakeLossCalc.Name = "btnWakeLossCalc";
            this.btnWakeLossCalc.Size = new System.Drawing.Size(238, 51);
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
            this.Label53.Location = new System.Drawing.Point(653, 37);
            this.Label53.Name = "Label53";
            this.Label53.Size = new System.Drawing.Size(135, 23);
            this.Label53.TabIndex = 2;
            this.Label53.Text = "Wake Models";
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
            dataGridViewCellStyle7.Format = "P";
            dataGridViewCellStyle7.NullValue = null;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle7;
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
            dataGridViewCellStyle8.Format = "P";
            dataGridViewCellStyle8.NullValue = null;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle8;
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
            dataGridViewCellStyle9.Format = "P";
            dataGridViewCellStyle9.NullValue = null;
            this.Column10.DefaultCellStyle = dataGridViewCellStyle9;
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
            dataGridViewCellStyle10.Format = "P";
            dataGridViewCellStyle10.NullValue = null;
            this.Column12.DefaultCellStyle = dataGridViewCellStyle10;
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
            "1000",
            "4000",
            "6000",
            "8000",
            "10000"});
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
            this.tabPage1.Controls.Add(this.dataTurbulence);
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
            // dataTurbulence
            // 
            this.dataTurbulence.AllowUserToAddRows = false;
            this.dataTurbulence.AllowUserToDeleteRows = false;
            this.dataTurbulence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataTurbulence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTurbulence.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column93,
            this.Column94,
            this.Column95});
            this.dataTurbulence.Location = new System.Drawing.Point(18, 51);
            this.dataTurbulence.Name = "dataTurbulence";
            this.dataTurbulence.ReadOnly = true;
            this.dataTurbulence.RowHeadersVisible = false;
            this.dataTurbulence.Size = new System.Drawing.Size(217, 565);
            this.dataTurbulence.TabIndex = 294;
            // 
            // Column93
            // 
            this.Column93.HeaderText = "WS [m/s]";
            this.Column93.Name = "Column93";
            this.Column93.ReadOnly = true;
            this.Column93.Width = 70;
            // 
            // Column94
            // 
            this.Column94.HeaderText = "TI";
            this.Column94.Name = "Column94";
            this.Column94.ReadOnly = true;
            this.Column94.Width = 70;
            // 
            // Column95
            // 
            this.Column95.HeaderText = "Freq.";
            this.Column95.Name = "Column95";
            this.Column95.ReadOnly = true;
            this.Column95.Width = 70;
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
            this.plotTurbInt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotTurbInt.Location = new System.Drawing.Point(520, 18);
            this.plotTurbInt.Name = "plotTurbInt";
            this.plotTurbInt.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTurbInt.Size = new System.Drawing.Size(911, 595);
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
            this.tabPage3.Controls.Add(this.dataExtremeShear);
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
            this.tabPage3.Controls.Add(this.label95);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1453, 626);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Extreme Shear";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataExtremeShear
            // 
            this.dataExtremeShear.AllowUserToAddRows = false;
            this.dataExtremeShear.AllowUserToDeleteRows = false;
            this.dataExtremeShear.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataExtremeShear.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn60,
            this.dataGridViewTextBoxColumn68,
            this.dataGridViewTextBoxColumn69,
            this.Column96,
            this.Column97});
            this.dataExtremeShear.Location = new System.Drawing.Point(21, 421);
            this.dataExtremeShear.Name = "dataExtremeShear";
            this.dataExtremeShear.ReadOnly = true;
            this.dataExtremeShear.RowHeadersVisible = false;
            this.dataExtremeShear.Size = new System.Drawing.Size(334, 185);
            this.dataExtremeShear.TabIndex = 302;
            // 
            // dataGridViewTextBoxColumn60
            // 
            this.dataGridViewTextBoxColumn60.HeaderText = "";
            this.dataGridViewTextBoxColumn60.Name = "dataGridViewTextBoxColumn60";
            this.dataGridViewTextBoxColumn60.ReadOnly = true;
            this.dataGridViewTextBoxColumn60.Width = 40;
            // 
            // dataGridViewTextBoxColumn68
            // 
            this.dataGridViewTextBoxColumn68.HeaderText = "5-10 m/s";
            this.dataGridViewTextBoxColumn68.Name = "dataGridViewTextBoxColumn68";
            this.dataGridViewTextBoxColumn68.ReadOnly = true;
            this.dataGridViewTextBoxColumn68.Width = 70;
            // 
            // dataGridViewTextBoxColumn69
            // 
            this.dataGridViewTextBoxColumn69.HeaderText = "10-15 m/s";
            this.dataGridViewTextBoxColumn69.Name = "dataGridViewTextBoxColumn69";
            this.dataGridViewTextBoxColumn69.ReadOnly = true;
            this.dataGridViewTextBoxColumn69.Width = 70;
            // 
            // Column96
            // 
            this.Column96.HeaderText = "15+ m/s";
            this.Column96.Name = "Column96";
            this.Column96.ReadOnly = true;
            this.Column96.Width = 70;
            // 
            // Column97
            // 
            this.Column97.HeaderText = "All > Cut-In";
            this.Column97.Name = "Column97";
            this.Column97.ReadOnly = true;
            this.Column97.Width = 70;
            // 
            // btnEditShearFromSiteConds
            // 
            this.btnEditShearFromSiteConds.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnEditShearFromSiteConds.Location = new System.Drawing.Point(377, 146);
            this.btnEditShearFromSiteConds.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditShearFromSiteConds.Name = "btnEditShearFromSiteConds";
            this.btnEditShearFromSiteConds.Size = new System.Drawing.Size(30, 29);
            this.btnEditShearFromSiteConds.TabIndex = 301;
            this.btnEditShearFromSiteConds.Text = "✎";
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
            this.plotExtremeShear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotExtremeShear.Location = new System.Drawing.Point(501, 18);
            this.plotExtremeShear.Name = "plotExtremeShear";
            this.plotExtremeShear.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotExtremeShear.Size = new System.Drawing.Size(914, 595);
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
            this.btnExportExtremeWSTable.Location = new System.Drawing.Point(842, 667);
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
            this.plotExtremeWS_TS.Location = new System.Drawing.Point(921, 17);
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
            this.dataExtremeWS.Size = new System.Drawing.Size(548, 650);
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
            this.plotExtremeWS.Location = new System.Drawing.Point(921, 317);
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
            this.btnExtremeWS.Location = new System.Drawing.Point(916, 967);
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
            this.pgeMonthlyAnalysis.Controls.Add(this.label126);
            this.pgeMonthlyAnalysis.Controls.Add(this.txtTimeSeriesInterval);
            this.pgeMonthlyAnalysis.Controls.Add(this.btnTimeSeriesLeft);
            this.pgeMonthlyAnalysis.Controls.Add(this.btnTimeSeriesRight);
            this.pgeMonthlyAnalysis.Controls.Add(this.label150);
            this.pgeMonthlyAnalysis.Controls.Add(this.label244);
            this.pgeMonthlyAnalysis.Controls.Add(this.dateTimeSeriesEnd);
            this.pgeMonthlyAnalysis.Controls.Add(this.dateTimeSeriesStart);
            this.pgeMonthlyAnalysis.Controls.Add(this.label22);
            this.pgeMonthlyAnalysis.Controls.Add(this.cboTimeSeriesAnalysis);
            this.pgeMonthlyAnalysis.Controls.Add(this.chkSelectAllWTGsToShow);
            this.pgeMonthlyAnalysis.Controls.Add(this.chkWTGsToShow);
            this.pgeMonthlyAnalysis.Controls.Add(this.chkSelectAllTurbineYears);
            this.pgeMonthlyAnalysis.Controls.Add(this.chkYears_Monthly);
            this.pgeMonthlyAnalysis.Controls.Add(this.label148);
            this.pgeMonthlyAnalysis.Controls.Add(this.chkSelectedTurbineParam);
            this.pgeMonthlyAnalysis.Controls.Add(this.btnExportTimeSeriesEsts);
            this.pgeMonthlyAnalysis.Controls.Add(this.splitTimeSeries);
            this.pgeMonthlyAnalysis.Controls.Add(this.label158);
            this.pgeMonthlyAnalysis.Controls.Add(this.cboMonthlyPowerCurve);
            this.pgeMonthlyAnalysis.Controls.Add(this.cboMonthlyWakeModel);
            this.pgeMonthlyAnalysis.Controls.Add(this.label157);
            this.pgeMonthlyAnalysis.Controls.Add(this.label163);
            this.pgeMonthlyAnalysis.Location = new System.Drawing.Point(4, 27);
            this.pgeMonthlyAnalysis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeMonthlyAnalysis.Name = "pgeMonthlyAnalysis";
            this.pgeMonthlyAnalysis.Size = new System.Drawing.Size(1488, 712);
            this.pgeMonthlyAnalysis.TabIndex = 17;
            this.pgeMonthlyAnalysis.Text = "Time Series Analysis";
            this.pgeMonthlyAnalysis.UseVisualStyleBackColor = true;
            this.pgeMonthlyAnalysis.Click += new System.EventHandler(this.pgeMonthlyAnalysis_Click);
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label126.Location = new System.Drawing.Point(1368, 66);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(38, 18);
            this.label126.TabIndex = 309;
            this.label126.Text = "Days";
            this.label126.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtTimeSeriesInterval
            // 
            this.txtTimeSeriesInterval.Location = new System.Drawing.Point(1303, 62);
            this.txtTimeSeriesInterval.Name = "txtTimeSeriesInterval";
            this.txtTimeSeriesInterval.Size = new System.Drawing.Size(59, 25);
            this.txtTimeSeriesInterval.TabIndex = 308;
            // 
            // btnTimeSeriesLeft
            // 
            this.btnTimeSeriesLeft.BackColor = System.Drawing.SystemColors.Control;
            this.btnTimeSeriesLeft.Location = new System.Drawing.Point(1248, 57);
            this.btnTimeSeriesLeft.Name = "btnTimeSeriesLeft";
            this.btnTimeSeriesLeft.Size = new System.Drawing.Size(45, 35);
            this.btnTimeSeriesLeft.TabIndex = 307;
            this.btnTimeSeriesLeft.Text = "◀";
            this.btnTimeSeriesLeft.UseVisualStyleBackColor = false;
            this.btnTimeSeriesLeft.Click += new System.EventHandler(this.btnTimeSeriesLeft_Click);
            // 
            // btnTimeSeriesRight
            // 
            this.btnTimeSeriesRight.BackColor = System.Drawing.SystemColors.Control;
            this.btnTimeSeriesRight.Location = new System.Drawing.Point(1409, 57);
            this.btnTimeSeriesRight.Name = "btnTimeSeriesRight";
            this.btnTimeSeriesRight.Size = new System.Drawing.Size(45, 35);
            this.btnTimeSeriesRight.TabIndex = 306;
            this.btnTimeSeriesRight.Text = "▶";
            this.btnTimeSeriesRight.UseVisualStyleBackColor = false;
            this.btnTimeSeriesRight.Click += new System.EventHandler(this.btnTimeSeriesRight_Click);
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label150.Location = new System.Drawing.Point(1059, 62);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(23, 18);
            this.label150.TabIndex = 305;
            this.label150.Text = "To";
            this.label150.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label244
            // 
            this.label244.AutoSize = true;
            this.label244.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label244.Location = new System.Drawing.Point(859, 62);
            this.label244.Name = "label244";
            this.label244.Size = new System.Drawing.Size(40, 18);
            this.label244.TabIndex = 304;
            this.label244.Text = "From";
            this.label244.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dateTimeSeriesEnd
            // 
            this.dateTimeSeriesEnd.CustomFormat = "MM/dd/yy HH:mm";
            this.dateTimeSeriesEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeSeriesEnd.Location = new System.Drawing.Point(1089, 59);
            this.dateTimeSeriesEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimeSeriesEnd.Name = "dateTimeSeriesEnd";
            this.dateTimeSeriesEnd.Size = new System.Drawing.Size(140, 25);
            this.dateTimeSeriesEnd.TabIndex = 303;
            this.dateTimeSeriesEnd.ValueChanged += new System.EventHandler(this.dateTimeSeriesEnd_ValueChanged);
            // 
            // dateTimeSeriesStart
            // 
            this.dateTimeSeriesStart.CustomFormat = "MM/dd/yy HH:mm";
            this.dateTimeSeriesStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeSeriesStart.Location = new System.Drawing.Point(903, 59);
            this.dateTimeSeriesStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimeSeriesStart.Name = "dateTimeSeriesStart";
            this.dateTimeSeriesStart.Size = new System.Drawing.Size(140, 25);
            this.dateTimeSeriesStart.TabIndex = 302;
            this.dateTimeSeriesStart.ValueChanged += new System.EventHandler(this.dateTimeSeriesStart_ValueChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(343, 27);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(67, 18);
            this.label22.TabIndex = 299;
            this.label22.Text = "Analysis :";
            // 
            // cboTimeSeriesAnalysis
            // 
            this.cboTimeSeriesAnalysis.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTimeSeriesAnalysis.FormattingEnabled = true;
            this.cboTimeSeriesAnalysis.Items.AddRange(new object[] {
            "Time Series - Hourly",
            "Time Series - Monthly",
            "Time Series - Yearly",
            "Trends - Monthly"});
            this.cboTimeSeriesAnalysis.Location = new System.Drawing.Point(410, 22);
            this.cboTimeSeriesAnalysis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTimeSeriesAnalysis.Name = "cboTimeSeriesAnalysis";
            this.cboTimeSeriesAnalysis.Size = new System.Drawing.Size(190, 26);
            this.cboTimeSeriesAnalysis.TabIndex = 298;
            this.cboTimeSeriesAnalysis.SelectedIndexChanged += new System.EventHandler(this.cboTimeSeriesAnalysis_SelectedIndexChanged);
            // 
            // chkSelectAllWTGsToShow
            // 
            this.chkSelectAllWTGsToShow.AutoSize = true;
            this.chkSelectAllWTGsToShow.Checked = true;
            this.chkSelectAllWTGsToShow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectAllWTGsToShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectAllWTGsToShow.Location = new System.Drawing.Point(25, 71);
            this.chkSelectAllWTGsToShow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkSelectAllWTGsToShow.Name = "chkSelectAllWTGsToShow";
            this.chkSelectAllWTGsToShow.Size = new System.Drawing.Size(81, 30);
            this.chkSelectAllWTGsToShow.TabIndex = 297;
            this.chkSelectAllWTGsToShow.Text = "Select/\r\nDeselect All";
            this.chkSelectAllWTGsToShow.UseVisualStyleBackColor = true;
            this.chkSelectAllWTGsToShow.CheckedChanged += new System.EventHandler(this.chkSelectAllWTGsToShow_CheckedChanged);
            // 
            // chkWTGsToShow
            // 
            this.chkWTGsToShow.CheckOnClick = true;
            this.chkWTGsToShow.FormattingEnabled = true;
            this.chkWTGsToShow.Location = new System.Drawing.Point(15, 105);
            this.chkWTGsToShow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkWTGsToShow.Name = "chkWTGsToShow";
            this.chkWTGsToShow.Size = new System.Drawing.Size(100, 244);
            this.chkWTGsToShow.TabIndex = 296;
            this.chkWTGsToShow.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkWTGsToShow_ItemCheck);
            this.chkWTGsToShow.SelectedIndexChanged += new System.EventHandler(this.chkWTGsToShow_SelectedIndexChanged);
            // 
            // chkSelectAllTurbineYears
            // 
            this.chkSelectAllTurbineYears.AutoSize = true;
            this.chkSelectAllTurbineYears.Checked = true;
            this.chkSelectAllTurbineYears.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectAllTurbineYears.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectAllTurbineYears.Location = new System.Drawing.Point(13, 486);
            this.chkSelectAllTurbineYears.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkSelectAllTurbineYears.Name = "chkSelectAllTurbineYears";
            this.chkSelectAllTurbineYears.Size = new System.Drawing.Size(81, 30);
            this.chkSelectAllTurbineYears.TabIndex = 295;
            this.chkSelectAllTurbineYears.Text = "Select/\r\nDeselect All";
            this.chkSelectAllTurbineYears.UseVisualStyleBackColor = true;
            this.chkSelectAllTurbineYears.CheckedChanged += new System.EventHandler(this.chkSelectAllTurbineYears_CheckedChanged_2);
            // 
            // chkYears_Monthly
            // 
            this.chkYears_Monthly.CheckOnClick = true;
            this.chkYears_Monthly.FormattingEnabled = true;
            this.chkYears_Monthly.Location = new System.Drawing.Point(13, 521);
            this.chkYears_Monthly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkYears_Monthly.Name = "chkYears_Monthly";
            this.chkYears_Monthly.Size = new System.Drawing.Size(98, 144);
            this.chkYears_Monthly.TabIndex = 294;
            this.chkYears_Monthly.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkYears_Monthly_ItemCheck);
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label148.Location = new System.Drawing.Point(12, 366);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(101, 17);
            this.label148.TabIndex = 293;
            this.label148.Text = "Plot Parameters:";
            // 
            // chkSelectedTurbineParam
            // 
            this.chkSelectedTurbineParam.CheckOnClick = true;
            this.chkSelectedTurbineParam.FormattingEnabled = true;
            this.chkSelectedTurbineParam.Location = new System.Drawing.Point(13, 389);
            this.chkSelectedTurbineParam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkSelectedTurbineParam.Name = "chkSelectedTurbineParam";
            this.chkSelectedTurbineParam.Size = new System.Drawing.Size(100, 84);
            this.chkSelectedTurbineParam.TabIndex = 292;
            this.chkSelectedTurbineParam.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkSelectedTurbineParam_ItemCheck);
            // 
            // btnExportTimeSeriesEsts
            // 
            this.btnExportTimeSeriesEsts.Font = new System.Drawing.Font("Palatino Linotype", 10F);
            this.btnExportTimeSeriesEsts.Location = new System.Drawing.Point(23, 673);
            this.btnExportTimeSeriesEsts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportTimeSeriesEsts.Name = "btnExportTimeSeriesEsts";
            this.btnExportTimeSeriesEsts.Size = new System.Drawing.Size(81, 30);
            this.btnExportTimeSeriesEsts.TabIndex = 291;
            this.btnExportTimeSeriesEsts.Text = "Export";
            this.btnExportTimeSeriesEsts.UseVisualStyleBackColor = true;
            this.btnExportTimeSeriesEsts.Click += new System.EventHandler(this.btnExportTimeSeriesEsts_Click);
            // 
            // splitTimeSeries
            // 
            this.splitTimeSeries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitTimeSeries.Location = new System.Drawing.Point(129, 105);
            this.splitTimeSeries.Name = "splitTimeSeries";
            this.splitTimeSeries.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitTimeSeries.Panel1
            // 
            this.splitTimeSeries.Panel1.Controls.Add(this.dataWTG_TimeSeriesEsts);
            // 
            // splitTimeSeries.Panel2
            // 
            this.splitTimeSeries.Panel2.Controls.Add(this.plotWTG_TimeSeries);
            this.splitTimeSeries.Size = new System.Drawing.Size(1339, 604);
            this.splitTimeSeries.SplitterDistance = 260;
            this.splitTimeSeries.TabIndex = 280;
            // 
            // dataWTG_TimeSeriesEsts
            // 
            this.dataWTG_TimeSeriesEsts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataWTG_TimeSeriesEsts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column21,
            this.Column22,
            this.Column23,
            this.Column24});
            this.dataWTG_TimeSeriesEsts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataWTG_TimeSeriesEsts.Location = new System.Drawing.Point(0, 0);
            this.dataWTG_TimeSeriesEsts.Name = "dataWTG_TimeSeriesEsts";
            this.dataWTG_TimeSeriesEsts.Size = new System.Drawing.Size(1339, 260);
            this.dataWTG_TimeSeriesEsts.TabIndex = 282;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "Year";
            this.Column21.Name = "Column21";
            // 
            // Column22
            // 
            this.Column22.HeaderText = "Avg. Free-Stream WS [m/s]";
            this.Column22.Name = "Column22";
            // 
            // Column23
            // 
            this.Column23.HeaderText = "Gross AEP [MWh]";
            this.Column23.Name = "Column23";
            // 
            // Column24
            // 
            this.Column24.HeaderText = "Net AEP [MWh]";
            this.Column24.Name = "Column24";
            // 
            // plotWTG_TimeSeries
            // 
            this.plotWTG_TimeSeries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotWTG_TimeSeries.Location = new System.Drawing.Point(0, 0);
            this.plotWTG_TimeSeries.Name = "plotWTG_TimeSeries";
            this.plotWTG_TimeSeries.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotWTG_TimeSeries.Size = new System.Drawing.Size(1339, 340);
            this.plotWTG_TimeSeries.TabIndex = 280;
            this.plotWTG_TimeSeries.Text = "plotView1";
            this.plotWTG_TimeSeries.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotWTG_TimeSeries.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotWTG_TimeSeries.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label158.Location = new System.Drawing.Point(606, 23);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(93, 18);
            this.label158.TabIndex = 229;
            this.label158.Text = "Power Curve :";
            // 
            // cboMonthlyPowerCurve
            // 
            this.cboMonthlyPowerCurve.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMonthlyPowerCurve.FormattingEnabled = true;
            this.cboMonthlyPowerCurve.Location = new System.Drawing.Point(705, 21);
            this.cboMonthlyPowerCurve.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMonthlyPowerCurve.Name = "cboMonthlyPowerCurve";
            this.cboMonthlyPowerCurve.Size = new System.Drawing.Size(321, 26);
            this.cboMonthlyPowerCurve.TabIndex = 228;
            this.cboMonthlyPowerCurve.SelectedIndexChanged += new System.EventHandler(this.cboMonthlyPowerCurve_SelectedIndexChanged);
            // 
            // cboMonthlyWakeModel
            // 
            this.cboMonthlyWakeModel.FormattingEnabled = true;
            this.cboMonthlyWakeModel.Location = new System.Drawing.Point(1147, 19);
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
            this.label157.Location = new System.Drawing.Point(1053, 23);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(84, 18);
            this.label157.TabIndex = 223;
            this.label157.Text = "Wake Model:";
            // 
            // label163
            // 
            this.label163.AutoSize = true;
            this.label163.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label163.Location = new System.Drawing.Point(20, 20);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(292, 25);
            this.label163.TabIndex = 1;
            this.label163.Text = "Turbine Time Series Analysis";
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
            this.chkTurbLabels_Maps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.pgeRound.Controls.Add(this.label9);
            this.pgeRound.Controls.Add(this.txtRR_Valley_used);
            this.pgeRound.Controls.Add(this.txtRR_Elev_used);
            this.pgeRound.Controls.Add(this.dataTurbUncert);
            this.pgeRound.Controls.Add(this.dataRR_AllErr);
            this.pgeRound.Controls.Add(this.dataRR_Results);
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
            this.pgeRound.Controls.Add(this.Label41);
            this.pgeRound.Controls.Add(this.btnExportRR);
            this.pgeRound.Controls.Add(this.cboRoundRobin);
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(483, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 18);
            this.label9.TabIndex = 308;
            this.label9.Text = "Models/Data Used:";
            // 
            // txtRR_Valley_used
            // 
            this.txtRR_Valley_used.Location = new System.Drawing.Point(799, 49);
            this.txtRR_Valley_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRR_Valley_used.Name = "txtRR_Valley_used";
            this.txtRR_Valley_used.ReadOnly = true;
            this.txtRR_Valley_used.Size = new System.Drawing.Size(90, 25);
            this.txtRR_Valley_used.TabIndex = 307;
            this.txtRR_Valley_used.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRR_Elev_used
            // 
            this.txtRR_Elev_used.Location = new System.Drawing.Point(703, 83);
            this.txtRR_Elev_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRR_Elev_used.Name = "txtRR_Elev_used";
            this.txtRR_Elev_used.ReadOnly = true;
            this.txtRR_Elev_used.Size = new System.Drawing.Size(90, 25);
            this.txtRR_Elev_used.TabIndex = 306;
            this.txtRR_Elev_used.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataTurbUncert
            // 
            this.dataTurbUncert.AllowUserToAddRows = false;
            this.dataTurbUncert.AllowUserToDeleteRows = false;
            this.dataTurbUncert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTurbUncert.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn76,
            this.dataGridViewTextBoxColumn77,
            this.dataGridViewTextBoxColumn78,
            this.dataGridViewTextBoxColumn79,
            this.dataGridViewTextBoxColumn80,
            this.Column100,
            this.Column101,
            this.Column102,
            this.Column103});
            this.dataTurbUncert.Location = new System.Drawing.Point(955, 117);
            this.dataTurbUncert.Name = "dataTurbUncert";
            this.dataTurbUncert.ReadOnly = true;
            this.dataTurbUncert.RowHeadersVisible = false;
            this.dataTurbUncert.Size = new System.Drawing.Size(514, 382);
            this.dataTurbUncert.TabIndex = 305;
            // 
            // dataGridViewTextBoxColumn76
            // 
            this.dataGridViewTextBoxColumn76.HeaderText = "#";
            this.dataGridViewTextBoxColumn76.Name = "dataGridViewTextBoxColumn76";
            this.dataGridViewTextBoxColumn76.ReadOnly = true;
            this.dataGridViewTextBoxColumn76.Width = 30;
            // 
            // dataGridViewTextBoxColumn77
            // 
            this.dataGridViewTextBoxColumn77.HeaderText = "Site";
            this.dataGridViewTextBoxColumn77.Name = "dataGridViewTextBoxColumn77";
            this.dataGridViewTextBoxColumn77.ReadOnly = true;
            this.dataGridViewTextBoxColumn77.Width = 60;
            // 
            // dataGridViewTextBoxColumn78
            // 
            this.dataGridViewTextBoxColumn78.HeaderText = "WS [m/s]";
            this.dataGridViewTextBoxColumn78.Name = "dataGridViewTextBoxColumn78";
            this.dataGridViewTextBoxColumn78.ReadOnly = true;
            this.dataGridViewTextBoxColumn78.Width = 60;
            // 
            // dataGridViewTextBoxColumn79
            // 
            this.dataGridViewTextBoxColumn79.HeaderText = "WS Error";
            this.dataGridViewTextBoxColumn79.Name = "dataGridViewTextBoxColumn79";
            this.dataGridViewTextBoxColumn79.ReadOnly = true;
            this.dataGridViewTextBoxColumn79.Width = 60;
            // 
            // dataGridViewTextBoxColumn80
            // 
            this.dataGridViewTextBoxColumn80.HeaderText = "WS P90";
            this.dataGridViewTextBoxColumn80.Name = "dataGridViewTextBoxColumn80";
            this.dataGridViewTextBoxColumn80.ReadOnly = true;
            this.dataGridViewTextBoxColumn80.Width = 60;
            // 
            // Column100
            // 
            this.Column100.HeaderText = "WS P99";
            this.Column100.Name = "Column100";
            this.Column100.ReadOnly = true;
            this.Column100.Width = 60;
            // 
            // Column101
            // 
            this.Column101.HeaderText = "AEP";
            this.Column101.Name = "Column101";
            this.Column101.ReadOnly = true;
            this.Column101.Width = 60;
            // 
            // Column102
            // 
            this.Column102.HeaderText = "AEP P90";
            this.Column102.Name = "Column102";
            this.Column102.ReadOnly = true;
            this.Column102.Width = 60;
            // 
            // Column103
            // 
            this.Column103.HeaderText = "AEP P99";
            this.Column103.Name = "Column103";
            this.Column103.ReadOnly = true;
            this.Column103.Width = 60;
            // 
            // dataRR_AllErr
            // 
            this.dataRR_AllErr.AllowUserToAddRows = false;
            this.dataRR_AllErr.AllowUserToDeleteRows = false;
            this.dataRR_AllErr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataRR_AllErr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn70,
            this.dataGridViewTextBoxColumn74,
            this.dataGridViewTextBoxColumn75,
            this.Column98,
            this.Column99});
            this.dataRR_AllErr.Location = new System.Drawing.Point(457, 117);
            this.dataRR_AllErr.Name = "dataRR_AllErr";
            this.dataRR_AllErr.ReadOnly = true;
            this.dataRR_AllErr.RowHeadersVisible = false;
            this.dataRR_AllErr.Size = new System.Drawing.Size(480, 582);
            this.dataRR_AllErr.TabIndex = 304;
            // 
            // dataGridViewTextBoxColumn70
            // 
            this.dataGridViewTextBoxColumn70.HeaderText = "#";
            this.dataGridViewTextBoxColumn70.Name = "dataGridViewTextBoxColumn70";
            this.dataGridViewTextBoxColumn70.ReadOnly = true;
            this.dataGridViewTextBoxColumn70.Width = 130;
            // 
            // dataGridViewTextBoxColumn74
            // 
            this.dataGridViewTextBoxColumn74.HeaderText = "Mets Used";
            this.dataGridViewTextBoxColumn74.Name = "dataGridViewTextBoxColumn74";
            this.dataGridViewTextBoxColumn74.ReadOnly = true;
            this.dataGridViewTextBoxColumn74.Width = 70;
            // 
            // dataGridViewTextBoxColumn75
            // 
            this.dataGridViewTextBoxColumn75.HeaderText = "Omitted";
            this.dataGridViewTextBoxColumn75.Name = "dataGridViewTextBoxColumn75";
            this.dataGridViewTextBoxColumn75.ReadOnly = true;
            this.dataGridViewTextBoxColumn75.Width = 70;
            // 
            // Column98
            // 
            this.Column98.HeaderText = "Error";
            this.Column98.Name = "Column98";
            this.Column98.ReadOnly = true;
            // 
            // Column99
            // 
            this.Column99.HeaderText = "RMSE";
            this.Column99.Name = "Column99";
            this.Column99.ReadOnly = true;
            // 
            // dataRR_Results
            // 
            this.dataRR_Results.AllowUserToAddRows = false;
            this.dataRR_Results.AllowUserToDeleteRows = false;
            this.dataRR_Results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataRR_Results.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn71,
            this.dataGridViewTextBoxColumn72,
            this.dataGridViewTextBoxColumn73});
            this.dataRR_Results.Location = new System.Drawing.Point(230, 344);
            this.dataRR_Results.Name = "dataRR_Results";
            this.dataRR_Results.ReadOnly = true;
            this.dataRR_Results.RowHeadersVisible = false;
            this.dataRR_Results.Size = new System.Drawing.Size(206, 155);
            this.dataRR_Results.TabIndex = 303;
            // 
            // dataGridViewTextBoxColumn71
            // 
            this.dataGridViewTextBoxColumn71.HeaderText = "# Mets";
            this.dataGridViewTextBoxColumn71.Name = "dataGridViewTextBoxColumn71";
            this.dataGridViewTextBoxColumn71.ReadOnly = true;
            this.dataGridViewTextBoxColumn71.Width = 60;
            // 
            // dataGridViewTextBoxColumn72
            // 
            this.dataGridViewTextBoxColumn72.HeaderText = "RMS Error";
            this.dataGridViewTextBoxColumn72.Name = "dataGridViewTextBoxColumn72";
            this.dataGridViewTextBoxColumn72.ReadOnly = true;
            this.dataGridViewTextBoxColumn72.Width = 70;
            // 
            // dataGridViewTextBoxColumn73
            // 
            this.dataGridViewTextBoxColumn73.HeaderText = "# Models";
            this.dataGridViewTextBoxColumn73.Name = "dataGridViewTextBoxColumn73";
            this.dataGridViewTextBoxColumn73.ReadOnly = true;
            this.dataGridViewTextBoxColumn73.Width = 70;
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
            this.plotTurbUncert.Location = new System.Drawing.Point(955, 507);
            this.plotTurbUncert.Name = "plotTurbUncert";
            this.plotTurbUncert.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotTurbUncert.Size = new System.Drawing.Size(514, 192);
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
            this.txtisMCPdUncert.Location = new System.Drawing.Point(607, 49);
            this.txtisMCPdUncert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtisMCPdUncert.Name = "txtisMCPdUncert";
            this.txtisMCPdUncert.ReadOnly = true;
            this.txtisMCPdUncert.Size = new System.Drawing.Size(90, 25);
            this.txtisMCPdUncert.TabIndex = 200;
            this.txtisMCPdUncert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRR_FlowSep_Used
            // 
            this.txtRR_FlowSep_Used.Location = new System.Drawing.Point(799, 83);
            this.txtRR_FlowSep_Used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRR_FlowSep_Used.Name = "txtRR_FlowSep_Used";
            this.txtRR_FlowSep_Used.ReadOnly = true;
            this.txtRR_FlowSep_Used.Size = new System.Drawing.Size(90, 25);
            this.txtRR_FlowSep_Used.TabIndex = 175;
            this.txtRR_FlowSep_Used.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cboRR_MinSize
            // 
            this.cboRR_MinSize.FormattingEnabled = true;
            this.cboRR_MinSize.Location = new System.Drawing.Point(150, 377);
            this.cboRR_MinSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboRR_MinSize.Name = "cboRR_MinSize";
            this.cboRR_MinSize.Size = new System.Drawing.Size(70, 26);
            this.cboRR_MinSize.TabIndex = 174;
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.Location = new System.Drawing.Point(145, 335);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(80, 38);
            this.Label31.TabIndex = 173;
            this.Label31.Text = "Min. Num.\r\nof Mets";
            this.Label31.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtRR_LC_used
            // 
            this.txtRR_LC_used.Location = new System.Drawing.Point(703, 49);
            this.txtRR_LC_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRR_LC_used.Name = "txtRR_LC_used";
            this.txtRR_LC_used.ReadOnly = true;
            this.txtRR_LC_used.Size = new System.Drawing.Size(90, 25);
            this.txtRR_LC_used.TabIndex = 171;
            this.txtRR_LC_used.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // Label41
            // 
            this.Label41.AutoSize = true;
            this.Label41.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label41.Location = new System.Drawing.Point(458, 18);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(143, 18);
            this.Label41.TabIndex = 156;
            this.Label41.Text = "Num. of Mets In Model";
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
            // cboRoundRobin
            // 
            this.cboRoundRobin.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRoundRobin.FormattingEnabled = true;
            this.cboRoundRobin.ItemHeight = 18;
            this.cboRoundRobin.Location = new System.Drawing.Point(607, 14);
            this.cboRoundRobin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboRoundRobin.Name = "cboRoundRobin";
            this.cboRoundRobin.Size = new System.Drawing.Size(330, 26);
            this.cboRoundRobin.TabIndex = 151;
            this.cboRoundRobin.SelectedIndexChanged += new System.EventHandler(this.cboRoundRobin_SelectedIndexChanged);
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
            this.pgeStepwise.Controls.Add(this.label50);
            this.pgeStepwise.Controls.Add(this.txtAdv_Valley_used);
            this.pgeStepwise.Controls.Add(this.txtAdv_Elev_used);
            this.pgeStepwise.Controls.Add(this.dataModCrossPred);
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
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(728, 13);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(92, 38);
            this.label50.TabIndex = 312;
            this.label50.Text = "Models/Data \r\nUsed";
            // 
            // txtAdv_Valley_used
            // 
            this.txtAdv_Valley_used.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdv_Valley_used.Location = new System.Drawing.Point(836, 65);
            this.txtAdv_Valley_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAdv_Valley_used.Name = "txtAdv_Valley_used";
            this.txtAdv_Valley_used.ReadOnly = true;
            this.txtAdv_Valley_used.Size = new System.Drawing.Size(102, 25);
            this.txtAdv_Valley_used.TabIndex = 311;
            // 
            // txtAdv_Elev_used
            // 
            this.txtAdv_Elev_used.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdv_Elev_used.Location = new System.Drawing.Point(837, 36);
            this.txtAdv_Elev_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAdv_Elev_used.Name = "txtAdv_Elev_used";
            this.txtAdv_Elev_used.ReadOnly = true;
            this.txtAdv_Elev_used.Size = new System.Drawing.Size(99, 25);
            this.txtAdv_Elev_used.TabIndex = 310;
            // 
            // dataModCrossPred
            // 
            this.dataModCrossPred.AllowUserToAddRows = false;
            this.dataModCrossPred.AllowUserToDeleteRows = false;
            this.dataModCrossPred.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataModCrossPred.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataModCrossPred.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column116,
            this.Column117,
            this.Column118,
            this.Column119});
            this.dataModCrossPred.Location = new System.Drawing.Point(34, 561);
            this.dataModCrossPred.Name = "dataModCrossPred";
            this.dataModCrossPred.ReadOnly = true;
            this.dataModCrossPred.RowHeadersVisible = false;
            this.dataModCrossPred.Size = new System.Drawing.Size(405, 137);
            this.dataModCrossPred.TabIndex = 309;
            // 
            // Column116
            // 
            this.Column116.HeaderText = "Predictor";
            this.Column116.Name = "Column116";
            this.Column116.ReadOnly = true;
            // 
            // Column117
            // 
            this.Column117.HeaderText = "Target";
            this.Column117.Name = "Column117";
            this.Column117.ReadOnly = true;
            // 
            // Column118
            // 
            this.Column118.HeaderText = "WS Est. [m/s]";
            this.Column118.Name = "Column118";
            this.Column118.ReadOnly = true;
            // 
            // Column119
            // 
            this.Column119.HeaderText = "% Error";
            this.Column119.Name = "Column119";
            this.Column119.ReadOnly = true;
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
            this.spltAdvanced.Panel1.Controls.Add(this.dataPathNodes);
            this.spltAdvanced.Panel1.Controls.Add(this.lblTurbineTSNoAdvanced);
            // 
            // spltAdvanced.Panel2
            // 
            this.spltAdvanced.Panel2.Controls.Add(this.spltAdvUWDW);
            this.spltAdvanced.Size = new System.Drawing.Size(1011, 184);
            this.spltAdvanced.SplitterDistance = 450;
            this.spltAdvanced.TabIndex = 291;
            // 
            // dataPathNodes
            // 
            this.dataPathNodes.AllowUserToAddRows = false;
            this.dataPathNodes.AllowUserToDeleteRows = false;
            this.dataPathNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPathNodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataPathNodes.Location = new System.Drawing.Point(8, 11);
            this.dataPathNodes.Name = "dataPathNodes";
            this.dataPathNodes.ReadOnly = true;
            this.dataPathNodes.RowHeadersVisible = false;
            this.dataPathNodes.Size = new System.Drawing.Size(429, 160);
            this.dataPathNodes.TabIndex = 308;
            // 
            // lblTurbineTSNoAdvanced
            // 
            this.lblTurbineTSNoAdvanced.AutoSize = true;
            this.lblTurbineTSNoAdvanced.BackColor = System.Drawing.Color.White;
            this.lblTurbineTSNoAdvanced.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurbineTSNoAdvanced.Location = new System.Drawing.Point(25, 94);
            this.lblTurbineTSNoAdvanced.Name = "lblTurbineTSNoAdvanced";
            this.lblTurbineTSNoAdvanced.Size = new System.Drawing.Size(45, 18);
            this.lblTurbineTSNoAdvanced.TabIndex = 215;
            this.lblTurbineTSNoAdvanced.Text = "label5";
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
            this.spltAdvUWDW.Panel1.Controls.Add(this.dataPathNodes_UW);
            this.spltAdvUWDW.Panel1.Controls.Add(this.Label8);
            // 
            // spltAdvUWDW.Panel2
            // 
            this.spltAdvUWDW.Panel2.Controls.Add(this.dataPathNodes_DW);
            this.spltAdvUWDW.Panel2.Controls.Add(this.Label39);
            this.spltAdvUWDW.Size = new System.Drawing.Size(549, 172);
            this.spltAdvUWDW.SplitterDistance = 273;
            this.spltAdvUWDW.TabIndex = 205;
            // 
            // dataPathNodes_UW
            // 
            this.dataPathNodes_UW.AllowUserToAddRows = false;
            this.dataPathNodes_UW.AllowUserToDeleteRows = false;
            this.dataPathNodes_UW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPathNodes_UW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataPathNodes_UW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Site,
            this.Flow,
            this.Coeff,
            this.dWSExpo,
            this.dWSSRDH});
            this.dataPathNodes_UW.Location = new System.Drawing.Point(7, 27);
            this.dataPathNodes_UW.Name = "dataPathNodes_UW";
            this.dataPathNodes_UW.ReadOnly = true;
            this.dataPathNodes_UW.RowHeadersVisible = false;
            this.dataPathNodes_UW.Size = new System.Drawing.Size(263, 141);
            this.dataPathNodes_UW.TabIndex = 309;
            // 
            // Site
            // 
            this.Site.HeaderText = "Site";
            this.Site.Name = "Site";
            this.Site.ReadOnly = true;
            this.Site.Width = 50;
            // 
            // Flow
            // 
            this.Flow.HeaderText = "Flow";
            this.Flow.Name = "Flow";
            this.Flow.ReadOnly = true;
            this.Flow.Width = 50;
            // 
            // Coeff
            // 
            this.Coeff.HeaderText = "Coeff";
            this.Coeff.Name = "Coeff";
            this.Coeff.ReadOnly = true;
            this.Coeff.Width = 50;
            // 
            // dWSExpo
            // 
            this.dWSExpo.HeaderText = "dWS Expo";
            this.dWSExpo.Name = "dWSExpo";
            this.dWSExpo.ReadOnly = true;
            this.dWSExpo.Width = 50;
            // 
            // dWSSRDH
            // 
            this.dWSSRDH.HeaderText = "dWS SRDH";
            this.dWSSRDH.Name = "dWSSRDH";
            this.dWSSRDH.ReadOnly = true;
            this.dWSSRDH.Width = 50;
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
            // dataPathNodes_DW
            // 
            this.dataPathNodes_DW.AllowUserToAddRows = false;
            this.dataPathNodes_DW.AllowUserToDeleteRows = false;
            this.dataPathNodes_DW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPathNodes_DW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataPathNodes_DW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn98,
            this.dataGridViewTextBoxColumn99,
            this.dataGridViewTextBoxColumn100,
            this.dataGridViewTextBoxColumn101,
            this.dataGridViewTextBoxColumn102});
            this.dataPathNodes_DW.Location = new System.Drawing.Point(6, 26);
            this.dataPathNodes_DW.Name = "dataPathNodes_DW";
            this.dataPathNodes_DW.ReadOnly = true;
            this.dataPathNodes_DW.RowHeadersVisible = false;
            this.dataPathNodes_DW.Size = new System.Drawing.Size(263, 141);
            this.dataPathNodes_DW.TabIndex = 310;
            // 
            // dataGridViewTextBoxColumn98
            // 
            this.dataGridViewTextBoxColumn98.HeaderText = "Site";
            this.dataGridViewTextBoxColumn98.Name = "dataGridViewTextBoxColumn98";
            this.dataGridViewTextBoxColumn98.ReadOnly = true;
            this.dataGridViewTextBoxColumn98.Width = 50;
            // 
            // dataGridViewTextBoxColumn99
            // 
            this.dataGridViewTextBoxColumn99.HeaderText = "Flow";
            this.dataGridViewTextBoxColumn99.Name = "dataGridViewTextBoxColumn99";
            this.dataGridViewTextBoxColumn99.ReadOnly = true;
            this.dataGridViewTextBoxColumn99.Width = 50;
            // 
            // dataGridViewTextBoxColumn100
            // 
            this.dataGridViewTextBoxColumn100.HeaderText = "Coeff";
            this.dataGridViewTextBoxColumn100.Name = "dataGridViewTextBoxColumn100";
            this.dataGridViewTextBoxColumn100.ReadOnly = true;
            this.dataGridViewTextBoxColumn100.Width = 50;
            // 
            // dataGridViewTextBoxColumn101
            // 
            this.dataGridViewTextBoxColumn101.HeaderText = "dWS Expo";
            this.dataGridViewTextBoxColumn101.Name = "dataGridViewTextBoxColumn101";
            this.dataGridViewTextBoxColumn101.ReadOnly = true;
            this.dataGridViewTextBoxColumn101.Width = 50;
            // 
            // dataGridViewTextBoxColumn102
            // 
            this.dataGridViewTextBoxColumn102.HeaderText = "dWS SRDH";
            this.dataGridViewTextBoxColumn102.Name = "dataGridViewTextBoxColumn102";
            this.dataGridViewTextBoxColumn102.ReadOnly = true;
            this.dataGridViewTextBoxColumn102.Width = 50;
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
            // btnExportRMS_Errors
            // 
            this.btnExportRMS_Errors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportRMS_Errors.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportRMS_Errors.Location = new System.Drawing.Point(1349, 59);
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
            this.txtImportedModel.Location = new System.Drawing.Point(714, 94);
            this.txtImportedModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtImportedModel.Name = "txtImportedModel";
            this.txtImportedModel.ReadOnly = true;
            this.txtImportedModel.Size = new System.Drawing.Size(114, 25);
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
            this.btnExportModel.Location = new System.Drawing.Point(1349, 10);
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
            this.txtisMCPdAdv.Location = new System.Drawing.Point(714, 61);
            this.txtisMCPdAdv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtisMCPdAdv.Name = "txtisMCPdAdv";
            this.txtisMCPdAdv.ReadOnly = true;
            this.txtisMCPdAdv.Size = new System.Drawing.Size(114, 25);
            this.txtisMCPdAdv.TabIndex = 212;
            // 
            // cboSeasonAdvanced
            // 
            this.cboSeasonAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSeasonAdvanced.FormattingEnabled = true;
            this.cboSeasonAdvanced.Location = new System.Drawing.Point(950, 51);
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
            this.cboTODAdvanced.Location = new System.Drawing.Point(950, 17);
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
            this.txtAdv_FlowSep_Used.Location = new System.Drawing.Point(836, 93);
            this.txtAdv_FlowSep_Used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAdv_FlowSep_Used.Name = "txtAdv_FlowSep_Used";
            this.txtAdv_FlowSep_Used.ReadOnly = true;
            this.txtAdv_FlowSep_Used.Size = new System.Drawing.Size(104, 25);
            this.txtAdv_FlowSep_Used.TabIndex = 198;
            // 
            // txtAdv_LC_used
            // 
            this.txtAdv_LC_used.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdv_LC_used.Location = new System.Drawing.Point(837, 8);
            this.txtAdv_LC_used.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAdv_LC_used.Name = "txtAdv_LC_used";
            this.txtAdv_LC_used.ReadOnly = true;
            this.txtAdv_LC_used.Size = new System.Drawing.Size(99, 25);
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
            this.btnExportCrossPreds.Location = new System.Drawing.Point(295, 503);
            this.btnExportCrossPreds.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportCrossPreds.Name = "btnExportCrossPreds";
            this.btnExportCrossPreds.Size = new System.Drawing.Size(144, 48);
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
            this.chkAllTurbsStep.Location = new System.Drawing.Point(346, 405);
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
            this.Label38.Location = new System.Drawing.Point(243, 407);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(97, 18);
            this.Label38.TabIndex = 123;
            this.Label38.Text = "Turbine Sites";
            // 
            // chkTurbLabelStep
            // 
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
            this.cboStartMet.Size = new System.Drawing.Size(102, 26);
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
            this.cboEndMet.Size = new System.Drawing.Size(102, 26);
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
            // Label11
            // 
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
            this.pgeSuitability.Controls.Add(this.dataZoneSound);
            this.pgeSuitability.Controls.Add(this.dataZoneIceHits);
            this.pgeSuitability.Controls.Add(this.dataShadowZoneSummary);
            this.pgeSuitability.Controls.Add(this.dataZones);
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
            this.pgeSuitability.Controls.Add(this.label175);
            this.pgeSuitability.Controls.Add(this.lblShadowByMonthOrIceByDist);
            this.pgeSuitability.Controls.Add(this.label173);
            this.pgeSuitability.Controls.Add(this.cboIcingYear);
            this.pgeSuitability.Controls.Add(this.txtTotalShadow);
            this.pgeSuitability.Controls.Add(this.lblTotalHoursPerYear);
            this.pgeSuitability.Controls.Add(this.lblShadowOrIceByDist);
            this.pgeSuitability.Controls.Add(this.lblZoneOrDirection);
            this.pgeSuitability.Controls.Add(this.cboZoneList);
            this.pgeSuitability.Controls.Add(this.label169);
            this.pgeSuitability.Controls.Add(this.cboSiteSuitHour);
            this.pgeSuitability.Controls.Add(this.label165);
            this.pgeSuitability.Controls.Add(this.cboSiteSuitMonth);
            this.pgeSuitability.Controls.Add(this.btnSiteSuitImportCRV);
            this.pgeSuitability.Controls.Add(this.btnDelZones);
            this.pgeSuitability.Controls.Add(this.label164);
            this.pgeSuitability.Controls.Add(this.cboSiteSuitabilitySelectPlot);
            this.pgeSuitability.Controls.Add(this.btnRunSoundModel);
            this.pgeSuitability.Controls.Add(this.btnRunShadowFlicker);
            this.pgeSuitability.Controls.Add(this.btnRunIceThrow);
            this.pgeSuitability.Controls.Add(this.label167);
            this.pgeSuitability.Controls.Add(this.label166);
            this.pgeSuitability.Controls.Add(this.cboSiteSuitPowerCurve);
            this.pgeSuitability.Controls.Add(this.btnImportZones);
            this.pgeSuitability.Controls.Add(this.dataShadow12x24);
            this.pgeSuitability.Location = new System.Drawing.Point(4, 27);
            this.pgeSuitability.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgeSuitability.Name = "pgeSuitability";
            this.pgeSuitability.Size = new System.Drawing.Size(1488, 712);
            this.pgeSuitability.TabIndex = 18;
            this.pgeSuitability.Text = "Site Suitability";
            this.pgeSuitability.UseVisualStyleBackColor = true;
            // 
            // dataZoneSound
            // 
            this.dataZoneSound.AllowUserToAddRows = false;
            this.dataZoneSound.AllowUserToDeleteRows = false;
            this.dataZoneSound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataZoneSound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataZoneSound.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn90,
            this.dataGridViewTextBoxColumn91});
            this.dataZoneSound.Location = new System.Drawing.Point(1335, 41);
            this.dataZoneSound.Name = "dataZoneSound";
            this.dataZoneSound.ReadOnly = true;
            this.dataZoneSound.RowHeadersVisible = false;
            this.dataZoneSound.Size = new System.Drawing.Size(130, 256);
            this.dataZoneSound.TabIndex = 309;
            // 
            // dataGridViewTextBoxColumn90
            // 
            this.dataGridViewTextBoxColumn90.HeaderText = "Zone";
            this.dataGridViewTextBoxColumn90.Name = "dataGridViewTextBoxColumn90";
            this.dataGridViewTextBoxColumn90.ReadOnly = true;
            this.dataGridViewTextBoxColumn90.Width = 60;
            // 
            // dataGridViewTextBoxColumn91
            // 
            this.dataGridViewTextBoxColumn91.HeaderText = "Sound [dBA]";
            this.dataGridViewTextBoxColumn91.Name = "dataGridViewTextBoxColumn91";
            this.dataGridViewTextBoxColumn91.ReadOnly = true;
            this.dataGridViewTextBoxColumn91.Width = 60;
            // 
            // dataZoneIceHits
            // 
            this.dataZoneIceHits.AllowUserToAddRows = false;
            this.dataZoneIceHits.AllowUserToDeleteRows = false;
            this.dataZoneIceHits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataZoneIceHits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataZoneIceHits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn88,
            this.dataGridViewTextBoxColumn89,
            this.Column104,
            this.Column105,
            this.Column106,
            this.Column107});
            this.dataZoneIceHits.Location = new System.Drawing.Point(1015, 41);
            this.dataZoneIceHits.Name = "dataZoneIceHits";
            this.dataZoneIceHits.ReadOnly = true;
            this.dataZoneIceHits.RowHeadersVisible = false;
            this.dataZoneIceHits.Size = new System.Drawing.Size(309, 256);
            this.dataZoneIceHits.TabIndex = 308;
            // 
            // dataGridViewTextBoxColumn88
            // 
            this.dataGridViewTextBoxColumn88.HeaderText = "Zone";
            this.dataGridViewTextBoxColumn88.Name = "dataGridViewTextBoxColumn88";
            this.dataGridViewTextBoxColumn88.ReadOnly = true;
            this.dataGridViewTextBoxColumn88.Width = 50;
            // 
            // dataGridViewTextBoxColumn89
            // 
            this.dataGridViewTextBoxColumn89.HeaderText = "Hits";
            this.dataGridViewTextBoxColumn89.Name = "dataGridViewTextBoxColumn89";
            this.dataGridViewTextBoxColumn89.ReadOnly = true;
            this.dataGridViewTextBoxColumn89.Width = 50;
            // 
            // Column104
            // 
            this.Column104.HeaderText = "Min";
            this.Column104.Name = "Column104";
            this.Column104.ReadOnly = true;
            this.Column104.Width = 50;
            // 
            // Column105
            // 
            this.Column105.HeaderText = "Max";
            this.Column105.Name = "Column105";
            this.Column105.ReadOnly = true;
            this.Column105.Width = 50;
            // 
            // Column106
            // 
            this.Column106.HeaderText = "% Prob > 0";
            this.Column106.Name = "Column106";
            this.Column106.ReadOnly = true;
            this.Column106.Width = 50;
            // 
            // Column107
            // 
            this.Column107.HeaderText = "% Prob > 1";
            this.Column107.Name = "Column107";
            this.Column107.ReadOnly = true;
            this.Column107.Width = 50;
            // 
            // dataShadowZoneSummary
            // 
            this.dataShadowZoneSummary.AllowUserToAddRows = false;
            this.dataShadowZoneSummary.AllowUserToDeleteRows = false;
            this.dataShadowZoneSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataShadowZoneSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataShadowZoneSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn86,
            this.dataGridViewTextBoxColumn87});
            this.dataShadowZoneSummary.Location = new System.Drawing.Point(874, 41);
            this.dataShadowZoneSummary.Name = "dataShadowZoneSummary";
            this.dataShadowZoneSummary.ReadOnly = true;
            this.dataShadowZoneSummary.RowHeadersVisible = false;
            this.dataShadowZoneSummary.Size = new System.Drawing.Size(130, 256);
            this.dataShadowZoneSummary.TabIndex = 307;
            // 
            // dataGridViewTextBoxColumn86
            // 
            this.dataGridViewTextBoxColumn86.HeaderText = "Zone";
            this.dataGridViewTextBoxColumn86.Name = "dataGridViewTextBoxColumn86";
            this.dataGridViewTextBoxColumn86.ReadOnly = true;
            this.dataGridViewTextBoxColumn86.Width = 60;
            // 
            // dataGridViewTextBoxColumn87
            // 
            this.dataGridViewTextBoxColumn87.HeaderText = "Total Hours";
            this.dataGridViewTextBoxColumn87.Name = "dataGridViewTextBoxColumn87";
            this.dataGridViewTextBoxColumn87.ReadOnly = true;
            this.dataGridViewTextBoxColumn87.Width = 60;
            // 
            // dataZones
            // 
            this.dataZones.AllowUserToAddRows = false;
            this.dataZones.AllowUserToDeleteRows = false;
            this.dataZones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataZones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataZones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column120,
            this.dataGridViewTextBoxColumn81,
            this.dataGridViewTextBoxColumn82,
            this.dataGridViewTextBoxColumn83,
            this.dataGridViewTextBoxColumn84,
            this.dataGridViewTextBoxColumn85});
            this.dataZones.Location = new System.Drawing.Point(14, 134);
            this.dataZones.Name = "dataZones";
            this.dataZones.RowHeadersVisible = false;
            this.dataZones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataZones.Size = new System.Drawing.Size(307, 499);
            this.dataZones.TabIndex = 306;
            this.dataZones.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataZones_CellValueChanged);
            this.dataZones.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataZones_CurrentCellDirtyStateChanged);
            // 
            // Column120
            // 
            this.Column120.HeaderText = "";
            this.Column120.Name = "Column120";
            this.Column120.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column120.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column120.Width = 20;
            // 
            // dataGridViewTextBoxColumn81
            // 
            this.dataGridViewTextBoxColumn81.HeaderText = "Name";
            this.dataGridViewTextBoxColumn81.Name = "dataGridViewTextBoxColumn81";
            this.dataGridViewTextBoxColumn81.ReadOnly = true;
            this.dataGridViewTextBoxColumn81.Width = 60;
            // 
            // dataGridViewTextBoxColumn82
            // 
            this.dataGridViewTextBoxColumn82.HeaderText = "Lat. [°]";
            this.dataGridViewTextBoxColumn82.Name = "dataGridViewTextBoxColumn82";
            this.dataGridViewTextBoxColumn82.ReadOnly = true;
            this.dataGridViewTextBoxColumn82.Width = 60;
            // 
            // dataGridViewTextBoxColumn83
            // 
            this.dataGridViewTextBoxColumn83.HeaderText = "Long. [°]";
            this.dataGridViewTextBoxColumn83.Name = "dataGridViewTextBoxColumn83";
            this.dataGridViewTextBoxColumn83.ReadOnly = true;
            this.dataGridViewTextBoxColumn83.Width = 60;
            // 
            // dataGridViewTextBoxColumn84
            // 
            this.dataGridViewTextBoxColumn84.HeaderText = "X Length [m]";
            this.dataGridViewTextBoxColumn84.Name = "dataGridViewTextBoxColumn84";
            this.dataGridViewTextBoxColumn84.ReadOnly = true;
            this.dataGridViewTextBoxColumn84.Width = 60;
            // 
            // dataGridViewTextBoxColumn85
            // 
            this.dataGridViewTextBoxColumn85.HeaderText = "Y Length [m]";
            this.dataGridViewTextBoxColumn85.Name = "dataGridViewTextBoxColumn85";
            this.dataGridViewTextBoxColumn85.ReadOnly = true;
            this.dataGridViewTextBoxColumn85.Width = 60;
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
            this.plotIceVsDist.Location = new System.Drawing.Point(896, 440);
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
            this.btnExportIceVsDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportIceVsDist.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportIceVsDist.Location = new System.Drawing.Point(1114, 304);
            this.btnExportIceVsDist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportIceVsDist.Name = "btnExportIceVsDist";
            this.btnExportIceVsDist.Size = new System.Drawing.Size(102, 50);
            this.btnExportIceVsDist.TabIndex = 266;
            this.btnExportIceVsDist.Text = "Export Throw vs. Dist.";
            this.btnExportIceVsDist.UseVisualStyleBackColor = true;
            this.btnExportIceVsDist.Click += new System.EventHandler(this.btnExportIceVsDist_Click);
            // 
            // btnExportSoundSummary
            // 
            this.btnExportSoundSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportSoundSummary.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportSoundSummary.Location = new System.Drawing.Point(1365, 304);
            this.btnExportSoundSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportSoundSummary.Name = "btnExportSoundSummary";
            this.btnExportSoundSummary.Size = new System.Drawing.Size(102, 50);
            this.btnExportSoundSummary.TabIndex = 264;
            this.btnExportSoundSummary.Text = "Export Summary";
            this.btnExportSoundSummary.UseVisualStyleBackColor = true;
            this.btnExportSoundSummary.Click += new System.EventHandler(this.btnExportSoundSummary_Click);
            // 
            // btnExportShadowFlicker
            // 
            this.btnExportShadowFlicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportShadowFlicker.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportShadowFlicker.Location = new System.Drawing.Point(905, 304);
            this.btnExportShadowFlicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportShadowFlicker.Name = "btnExportShadowFlicker";
            this.btnExportShadowFlicker.Size = new System.Drawing.Size(99, 50);
            this.btnExportShadowFlicker.TabIndex = 263;
            this.btnExportShadowFlicker.Text = "Export Summary";
            this.btnExportShadowFlicker.UseVisualStyleBackColor = true;
            this.btnExportShadowFlicker.Click += new System.EventHandler(this.btnExportShadowFlicker_Click);
            // 
            // btnExportIceSummary
            // 
            this.btnExportIceSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportIceSummary.Font = new System.Drawing.Font("Palatino Linotype", 9F);
            this.btnExportIceSummary.Location = new System.Drawing.Point(1222, 304);
            this.btnExportIceSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportIceSummary.Name = "btnExportIceSummary";
            this.btnExportIceSummary.Size = new System.Drawing.Size(102, 50);
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
            // label175
            // 
            this.label175.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label175.AutoSize = true;
            this.label175.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label175.Location = new System.Drawing.Point(1021, 18);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(128, 19);
            this.label175.TabIndex = 259;
            this.label175.Text = "Ice Hits by Zone";
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
            // btnRunSoundModel
            // 
            this.btnRunSoundModel.Location = new System.Drawing.Point(703, 10);
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
            // dataShadow12x24
            // 
            this.dataShadow12x24.AllowUserToAddRows = false;
            this.dataShadow12x24.AllowUserToDeleteRows = false;
            this.dataShadow12x24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dataShadow12x24.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataShadow12x24.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn92,
            this.dataGridViewTextBoxColumn93,
            this.dataGridViewTextBoxColumn94,
            this.dataGridViewTextBoxColumn95,
            this.dataGridViewTextBoxColumn96,
            this.dataGridViewTextBoxColumn97,
            this.Column108,
            this.Column109,
            this.Column110,
            this.Column111,
            this.Column112,
            this.Column113,
            this.Column114,
            this.Column115});
            this.dataShadow12x24.Location = new System.Drawing.Point(888, 446);
            this.dataShadow12x24.Name = "dataShadow12x24";
            this.dataShadow12x24.ReadOnly = true;
            this.dataShadow12x24.RowHeadersVisible = false;
            this.dataShadow12x24.Size = new System.Drawing.Size(577, 259);
            this.dataShadow12x24.TabIndex = 310;
            // 
            // dataGridViewTextBoxColumn92
            // 
            this.dataGridViewTextBoxColumn92.HeaderText = "";
            this.dataGridViewTextBoxColumn92.Name = "dataGridViewTextBoxColumn92";
            this.dataGridViewTextBoxColumn92.ReadOnly = true;
            this.dataGridViewTextBoxColumn92.Width = 40;
            // 
            // dataGridViewTextBoxColumn93
            // 
            this.dataGridViewTextBoxColumn93.HeaderText = "All";
            this.dataGridViewTextBoxColumn93.Name = "dataGridViewTextBoxColumn93";
            this.dataGridViewTextBoxColumn93.ReadOnly = true;
            this.dataGridViewTextBoxColumn93.Width = 40;
            // 
            // dataGridViewTextBoxColumn94
            // 
            this.dataGridViewTextBoxColumn94.HeaderText = "Jan";
            this.dataGridViewTextBoxColumn94.Name = "dataGridViewTextBoxColumn94";
            this.dataGridViewTextBoxColumn94.ReadOnly = true;
            this.dataGridViewTextBoxColumn94.Width = 40;
            // 
            // dataGridViewTextBoxColumn95
            // 
            this.dataGridViewTextBoxColumn95.HeaderText = "Feb";
            this.dataGridViewTextBoxColumn95.Name = "dataGridViewTextBoxColumn95";
            this.dataGridViewTextBoxColumn95.ReadOnly = true;
            this.dataGridViewTextBoxColumn95.Width = 40;
            // 
            // dataGridViewTextBoxColumn96
            // 
            this.dataGridViewTextBoxColumn96.HeaderText = "Mar";
            this.dataGridViewTextBoxColumn96.Name = "dataGridViewTextBoxColumn96";
            this.dataGridViewTextBoxColumn96.ReadOnly = true;
            this.dataGridViewTextBoxColumn96.Width = 40;
            // 
            // dataGridViewTextBoxColumn97
            // 
            this.dataGridViewTextBoxColumn97.HeaderText = "Apr";
            this.dataGridViewTextBoxColumn97.Name = "dataGridViewTextBoxColumn97";
            this.dataGridViewTextBoxColumn97.ReadOnly = true;
            this.dataGridViewTextBoxColumn97.Width = 40;
            // 
            // Column108
            // 
            this.Column108.HeaderText = "May";
            this.Column108.Name = "Column108";
            this.Column108.ReadOnly = true;
            this.Column108.Width = 40;
            // 
            // Column109
            // 
            this.Column109.HeaderText = "Jun";
            this.Column109.Name = "Column109";
            this.Column109.ReadOnly = true;
            this.Column109.Width = 40;
            // 
            // Column110
            // 
            this.Column110.HeaderText = "Jul";
            this.Column110.Name = "Column110";
            this.Column110.ReadOnly = true;
            this.Column110.Width = 40;
            // 
            // Column111
            // 
            this.Column111.HeaderText = "Aug";
            this.Column111.Name = "Column111";
            this.Column111.ReadOnly = true;
            this.Column111.Width = 40;
            // 
            // Column112
            // 
            this.Column112.HeaderText = "Sep";
            this.Column112.Name = "Column112";
            this.Column112.ReadOnly = true;
            this.Column112.Width = 40;
            // 
            // Column113
            // 
            this.Column113.HeaderText = "Oct";
            this.Column113.Name = "Column113";
            this.Column113.ReadOnly = true;
            this.Column113.Width = 40;
            // 
            // Column114
            // 
            this.Column114.HeaderText = "Nov";
            this.Column114.Name = "Column114";
            this.Column114.ReadOnly = true;
            this.Column114.Width = 40;
            // 
            // Column115
            // 
            this.Column115.HeaderText = "Dec";
            this.Column115.Name = "Column115";
            this.Column115.ReadOnly = true;
            this.Column115.Width = 40;
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
            // Column25
            // 
            this.Column25.HeaderText = "Name";
            this.Column25.Name = "Column25";
            // 
            // Column26
            // 
            this.Column26.HeaderText = "UTMX";
            this.Column26.Name = "Column26";
            // 
            // Column27
            // 
            this.Column27.HeaderText = "UTMY";
            this.Column27.Name = "Column27";
            // 
            // Column28
            // 
            this.Column28.HeaderText = "Name";
            this.Column28.Name = "Column28";
            this.Column28.Width = 70;
            // 
            // Column29
            // 
            this.Column29.HeaderText = "UTMX [m]";
            this.Column29.Name = "Column29";
            // 
            // Column30
            // 
            this.Column30.HeaderText = "UTMY [m]";
            this.Column30.Name = "Column30";
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "WD";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.Width = 60;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.HeaderText = "All Hrs";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.Width = 60;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.HeaderText = "Day";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.Width = 50;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.HeaderText = "Night";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.Width = 50;
            // 
            // Column63
            // 
            this.Column63.HeaderText = "Name";
            this.Column63.Name = "Column63";
            // 
            // Column64
            // 
            this.Column64.HeaderText = "RD [m]";
            this.Column64.Name = "Column64";
            // 
            // Column65
            // 
            this.Column65.HeaderText = "RPM";
            this.Column65.Name = "Column65";
            // 
            // Column66
            // 
            this.Column66.HeaderText = "Name";
            this.Column66.Name = "Column66";
            // 
            // Column67
            // 
            this.Column67.HeaderText = "Name";
            this.Column67.Name = "Column67";
            this.Column67.ReadOnly = true;
            this.Column67.Width = 70;
            // 
            // Column68
            // 
            this.Column68.HeaderText = "RD [m]";
            this.Column68.Name = "Column68";
            this.Column68.ReadOnly = true;
            // 
            // Column69
            // 
            this.Column69.HeaderText = "RPM";
            this.Column69.Name = "Column69";
            this.Column69.ReadOnly = true;
            // 
            // Column70
            // 
            this.Column70.HeaderText = "Name";
            this.Column70.Name = "Column70";
            this.Column70.ReadOnly = true;
            // 
            // colRD
            // 
            this.colRD.HeaderText = "RD [m]";
            this.colRD.Name = "colRD";
            this.colRD.ReadOnly = true;
            // 
            // Column71
            // 
            this.Column71.HeaderText = "RPM";
            this.Column71.Name = "Column71";
            this.Column71.ReadOnly = true;
            // 
            // Column72
            // 
            this.Column72.HeaderText = "Name";
            this.Column72.Name = "Column72";
            this.Column72.ReadOnly = true;
            // 
            // Column73
            // 
            this.Column73.HeaderText = "Model Name";
            this.Column73.Name = "Column73";
            // 
            // splitContainer9
            // 
            this.splitContainer9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer9.Location = new System.Drawing.Point(0, 0);
            this.splitContainer9.Name = "splitContainer9";
            this.splitContainer9.Size = new System.Drawing.Size(561, 183);
            this.splitContainer9.SplitterDistance = 273;
            this.splitContainer9.TabIndex = 1;
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
            this.Text = "Continuum Wind Energy Software";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Continuum_FormClosing);
            this.Load += new System.EventHandler(this.Continuum_Load);
            this.tabContinuum.ResumeLayout(false);
            this.pgeInput.ResumeLayout(false);
            this.pgeInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMetTowers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTurbines)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataAlphas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataExtrapolated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTempSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataVaneSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataAnemSummary)).EndInit();
            this.pnlMetDatQC_WSDiff.ResumeLayout(false);
            this.pnlMetDataQC_Scatter.ResumeLayout(false);
            this.pgeMERRA.ResumeLayout(false);
            this.pgeMERRA.PerformLayout();
            this.splotLTRef_1.Panel1.ResumeLayout(false);
            this.splotLTRef_1.Panel1.PerformLayout();
            this.splotLTRef_1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splotLTRef_1)).EndInit();
            this.splotLTRef_1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataRefAnnualProd)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataRefMonthlyProd)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.pgeMCP.ResumeLayout(false);
            this.pgeMCP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataDebias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataMCP_Uncert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataMCP_Bins)).EndInit();
            this.pgeMetSumm.ResumeLayout(false);
            this.pgeMetSumm.PerformLayout();
            this.spltMetTurbSumm.Panel1.ResumeLayout(false);
            this.spltMetTurbSumm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltMetTurbSumm)).EndInit();
            this.spltMetTurbSumm.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataMetStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTurbStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataMetSummary)).EndInit();
            this.splitContainer10.Panel1.ResumeLayout(false);
            this.splitContainer10.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).EndInit();
            this.splitContainer10.ResumeLayout(false);
            this.splitContainer11.Panel1.ResumeLayout(false);
            this.splitContainer11.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).EndInit();
            this.splitContainer11.ResumeLayout(false);
            this.splitContainer12.Panel1.ResumeLayout(false);
            this.splitContainer12.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer12)).EndInit();
            this.splitContainer12.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            this.pgeGrossTurbs.ResumeLayout(false);
            this.pgeGrossTurbs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrossHisto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrossTurbEst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPowerCurveList)).EndInit();
            this.pgeExceedance.ResumeLayout(false);
            this.pgeExceedance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataPvals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataDefinedLosses)).EndInit();
            this.pgeNetEsts.ResumeLayout(false);
            this.pgeNetEsts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataWakedTurbs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataWakeModels)).EndInit();
            this.pgeSiteConditions.ResumeLayout(false);
            this.pgeSiteConditions.PerformLayout();
            this.tabSiteConditions.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTerrainComplex)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTurbulence)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataExtremeShear)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataExtremeWS)).EndInit();
            this.pgeMonthlyAnalysis.ResumeLayout(false);
            this.pgeMonthlyAnalysis.PerformLayout();
            this.splitTimeSeries.Panel1.ResumeLayout(false);
            this.splitTimeSeries.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTimeSeries)).EndInit();
            this.splitTimeSeries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataWTG_TimeSeriesEsts)).EndInit();
            this.pgeMaps.ResumeLayout(false);
            this.pgeMaps.PerformLayout();
            this.pgeRound.ResumeLayout(false);
            this.pgeRound.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTurbUncert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataRR_AllErr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataRR_Results)).EndInit();
            this.pgeStepwise.ResumeLayout(false);
            this.pgeStepwise.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataModCrossPred)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataPathNodes)).EndInit();
            this.spltAdvUWDW.Panel1.ResumeLayout(false);
            this.spltAdvUWDW.Panel1.PerformLayout();
            this.spltAdvUWDW.Panel2.ResumeLayout(false);
            this.spltAdvUWDW.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltAdvUWDW)).EndInit();
            this.spltAdvUWDW.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataPathNodes_UW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPathNodes_DW)).EndInit();
            this.pgeSuitability.ResumeLayout(false);
            this.pgeSuitability.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataZoneSound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataZoneIceHits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataShadowZoneSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataZones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataShadow12x24)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).EndInit();
            this.splitContainer9.ResumeLayout(false);
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
        internal System.Windows.Forms.Button btnGenTurbEsts;
        internal System.Windows.Forms.Label Label30;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.Button btnDelTurb;
        internal System.Windows.Forms.Button btnDelMet;
        internal System.Windows.Forms.Button btnTurbines;
        internal System.Windows.Forms.TabPage pgeMetSumm;        
        internal System.Windows.Forms.Label Label80;
        internal System.Windows.Forms.CheckBox chkTurbSummAll;
        internal System.Windows.Forms.Label Label78;
        internal System.Windows.Forms.CheckBox chkMetSummAll;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label76;
        internal System.Windows.Forms.Button btnExportExpoSRDH;
        internal System.Windows.Forms.Label Label74;
        internal System.Windows.Forms.ComboBox cboSummaryWD;
        internal System.Windows.Forms.Label Label75;
        internal System.Windows.Forms.ComboBox cboMetSum_Rad;
        internal System.Windows.Forms.Label Label73;
        internal System.Windows.Forms.TabPage pgeGrossTurbs;
        internal System.Windows.Forms.Label Label40;
        internal System.Windows.Forms.ComboBox cboWS_or_WD;
        internal System.Windows.Forms.TextBox txtGross_FlowSepUsed;
        internal System.Windows.Forms.Label Label96;
        internal System.Windows.Forms.TextBox txtGross_LC_used;
        internal System.Windows.Forms.Label Label83;
        internal System.Windows.Forms.Label Label79;
        internal System.Windows.Forms.Label Label77;
        internal System.Windows.Forms.ComboBox cboGrossParam;
        internal System.Windows.Forms.Button btnExportDirWSDists;
        internal System.Windows.Forms.Label Label71;
        internal System.Windows.Forms.ComboBox cboGrossWD;
        internal System.Windows.Forms.Button btnExportDirWS;
        internal System.Windows.Forms.Button btnExportWSDists;
        internal System.Windows.Forms.Button btnWS_AEP_Exprt;
        internal System.Windows.Forms.TextBox txtAEPMax;
        internal System.Windows.Forms.TextBox txtAEPMin;
        internal System.Windows.Forms.TextBox txtAEPSD;
        internal System.Windows.Forms.TextBox txtAEPAvg;
        internal System.Windows.Forms.Label Label35;
        internal System.Windows.Forms.Label Label34;
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
        internal System.Windows.Forms.Button btnDelWakeModel;
        internal System.Windows.Forms.Button btnDelWakeGrid;
        internal System.Windows.Forms.ComboBox cboWakePlot;
        internal System.Windows.Forms.Label Label85;
        internal System.Windows.Forms.Button btnExportNetEsts;
        internal System.Windows.Forms.Label Label55;
        internal System.Windows.Forms.ComboBox cboNetWD;
        internal System.Windows.Forms.Button btnWakeLossCalc;
        internal System.Windows.Forms.Label Label53;
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
        internal System.Windows.Forms.Label Label41;
        internal System.Windows.Forms.Button btnExportRR;
        internal System.Windows.Forms.ComboBox cboRoundRobin;
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
        private System.Windows.Forms.TabPage pgeMetData;
        internal System.Windows.Forms.ComboBox cboFilt_or_Not;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label65;
        internal System.Windows.Forms.ComboBox cboMetWindRose;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.OpenFileDialog ofdMetData;
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
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.Label label125;
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
        public System.Windows.Forms.SaveFileDialog sfdSaveTAB;
        private System.Windows.Forms.TabPage pgeMERRA;        
        private System.Windows.Forms.Label label159;
        private System.Windows.Forms.Label label156;
        public System.Windows.Forms.ComboBox cboMERRA_PlotParam;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.Label label160;
        public System.Windows.Forms.ComboBox cboMERRA_PowerCurves;
        public System.Windows.Forms.FolderBrowserDialog fbd_MERRAData;
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
        internal System.Windows.Forms.Button btnImportCRV_MERRA;
        internal System.Windows.Forms.ComboBox cboMonthlyWakeModel;
        internal System.Windows.Forms.Label label157;
        internal System.Windows.Forms.Label label158;
        internal System.Windows.Forms.ComboBox cboMonthlyPowerCurve;
        private System.Windows.Forms.TabPage pgeExceedance;
        internal System.Windows.Forms.Label label164;
        internal System.Windows.Forms.ComboBox cboSiteSuitabilitySelectPlot;
        private System.Windows.Forms.Button btnDelZones;
        internal System.Windows.Forms.Button btnSiteSuitImportCRV;
        public System.Windows.Forms.Button btnRunIceThrow;
        internal System.Windows.Forms.Label label169;
        internal System.Windows.Forms.ComboBox cboSiteSuitHour;
        internal System.Windows.Forms.Label label165;
        internal System.Windows.Forms.ComboBox cboSiteSuitMonth;
        public System.Windows.Forms.Button btnRunShadowFlicker;
        internal System.Windows.Forms.Label lblShadowOrIceByDist;
        internal System.Windows.Forms.Label lblZoneOrDirection;
        internal System.Windows.Forms.ComboBox cboZoneList;
        public System.Windows.Forms.TextBox txtTotalShadow;
        internal System.Windows.Forms.Label lblTotalHoursPerYear;
        internal System.Windows.Forms.Label label173;
        internal System.Windows.Forms.ComboBox cboIcingYear;
        internal System.Windows.Forms.Label label175;
        internal System.Windows.Forms.Label lblShadowByMonthOrIceByDist;
        internal System.Windows.Forms.Label label176;
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
        private System.Windows.Forms.Label label181;
        private System.Windows.Forms.Label label182;
        private System.Windows.Forms.Button btn_editloss;
        internal System.Windows.Forms.Label label183;
        internal System.Windows.Forms.ComboBox cboExceedTurbine;
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
        private System.Windows.Forms.Label label51;
        internal System.Windows.Forms.CheckBox chkCreateTurbTS;
        internal System.Windows.Forms.Button btnGenTurbGross;
        private System.Windows.Forms.Button btnShowMCPRanges;
        public System.Windows.Forms.CheckBox chkUseSR;
        public System.Windows.Forms.CheckBox chk_Use_Sep;
        public System.Windows.Forms.OpenFileDialog ofdPowerCurve;
        public System.Windows.Forms.SaveFileDialog sfdCFMfile;
        public System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        public System.Windows.Forms.TabControl tabContinuum;
        public OxyPlot.WindowsForms.PlotView plotInputWindRose;
        public OxyPlot.WindowsForms.PlotView plotWS_vsHeight;
        private System.Windows.Forms.Label label62;
        public OxyPlot.WindowsForms.PlotView plotMetQC_WindRose;
        public OxyPlot.WindowsForms.PlotView plotAlphaByWD;
        public OxyPlot.WindowsForms.PlotView plotMCP;
        public OxyPlot.WindowsForms.PlotView plotMCP_Uncertainty;
        public OxyPlot.WindowsForms.PlotView plotGrossWindRose;
        public OxyPlot.WindowsForms.PlotView plotGrossWS_Dist;
        public OxyPlot.WindowsForms.PlotView plotGross_PowerCrvs;
        public OxyPlot.WindowsForms.PlotView plotGrossHisto;
        public OxyPlot.WindowsForms.PlotView plotExceedCurves;
        public OxyPlot.WindowsForms.PlotView plotCompositeExceed;
        public OxyPlot.WindowsForms.PlotView plotWakedDists;
        public OxyPlot.WindowsForms.PlotView plotTurbsByString;
        public OxyPlot.WindowsForms.PlotView plotWakeMap;
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
        private System.Windows.Forms.Label label206;
        public System.Windows.Forms.ComboBox cboMCP_Ref;
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
        internal Button btnExportRR_Summary;
        private SplitContainer spltAdvanced;
        public Label lblTurbineTSNoAdvanced;
        private SplitContainer spltAdvUWDW;
        internal Label Label8;
        internal Label Label39;
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
        private SplitContainer splitTimeSeries;
        private DataGridViewTextBoxColumn Column21;
        private DataGridViewTextBoxColumn Column22;
        private DataGridViewTextBoxColumn Column23;
        private DataGridViewTextBoxColumn Column24;
        public DataGridView dataWTG_TimeSeriesEsts;
        public OxyPlot.WindowsForms.PlotView plotWTG_TimeSeries;
        internal CheckBox chkSelectAllTurbineYears;
        public CheckedListBox chkYears_Monthly;
        internal Label label148;
        public CheckedListBox chkSelectedTurbineParam;
        internal Button btnExportTimeSeriesEsts;
        internal CheckBox chkSelectAllWTGsToShow;
        public CheckedListBox chkWTGsToShow;
        internal Label label22;
        internal ComboBox cboTimeSeriesAnalysis;
        private Label label126;
        public TextBox txtTimeSeriesInterval;
        private Button btnTimeSeriesLeft;
        private Button btnTimeSeriesRight;
        private Label label150;
        private Label label244;
        public DateTimePicker dateTimeSeriesEnd;
        public DateTimePicker dateTimeSeriesStart;
        private Button btnSetToIEC_LossUncert;
        private DataGridViewTextBoxColumn Column25;
        private DataGridViewTextBoxColumn Column26;
        private DataGridViewTextBoxColumn Column27;
        private DataGridViewTextBoxColumn Column28;
        private DataGridViewTextBoxColumn Column29;
        private DataGridViewTextBoxColumn Column30;
        private DataGridViewTextBoxColumn Column33;
        private DataGridViewTextBoxColumn Column34;
        private DataGridViewTextBoxColumn Column35;
        public DataGridView dataTurbines;
        public DataGridView dataMetTowers;
        private DataGridViewTextBoxColumn colTurbineName;
        private DataGridViewTextBoxColumn colEasting;
        private DataGridViewTextBoxColumn colNorthing;
        public DataGridView dataAnemSummary;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn Column31;
        private DataGridViewTextBoxColumn Column32;
        private DataGridViewTextBoxColumn Column36;
        private DataGridViewTextBoxColumn Column37;
        private DataGridViewTextBoxColumn Column38;
        private DataGridViewTextBoxColumn Column39;
        private DataGridViewTextBoxColumn Column40;
        private DataGridViewTextBoxColumn Column41;
        public DataGridView dataVaneSummary;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        public DataGridView dataExtrapolated;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn Column42;
        public DataGridView dataTempSummary;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        public DataGridView dataAlphas;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn Column43;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        public DataGridView dataMCP_Bins;
        public DataGridView dataMCP_Uncert;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private DataGridViewTextBoxColumn Column48;
        private DataGridViewTextBoxColumn Column49;
        private DataGridViewTextBoxColumn Column50;
        private DataGridViewTextBoxColumn Column51;
        private DataGridViewTextBoxColumn Column67;
        private DataGridViewTextBoxColumn Column68;
        private DataGridViewTextBoxColumn Column69;
        private DataGridViewTextBoxColumn Column63;
        private DataGridViewTextBoxColumn Column64;
        private DataGridViewTextBoxColumn Column65;
        private DataGridViewTextBoxColumn Column66;
        private DataGridViewTextBoxColumn Column70;
        private DataGridViewTextBoxColumn colRD;
        private DataGridViewTextBoxColumn Column71;
        private DataGridViewTextBoxColumn Column72;
        private DataGridViewTextBoxColumn Column73;
        public DataGridView dataPowerCurveList;
        public DataGridView dataGrossHisto;
        public DataGridView dataGrossTurbEst;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        public DataGridView dataPvals;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn46;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn47;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;
        public DataGridView dataDefinedLosses;
        public DataGridView dataWakeModels;
        public DataGridView dataWakedTurbs;
        internal TextBox txtOverallNetCF;
        internal Label label122;
        internal TextBox txtOverallNetAEP;
        internal Label label86;
        public DataGridView dataTurbulence;
        private DataGridViewTextBoxColumn Column93;
        private DataGridViewTextBoxColumn Column94;
        private DataGridViewTextBoxColumn Column95;
        public DataGridView dataExtremeShear;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn60;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn68;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn69;
        private DataGridViewTextBoxColumn Column96;
        private DataGridViewTextBoxColumn Column97;
        public DataGridView dataRR_Results;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn71;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn72;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn73;
        public DataGridView dataRR_AllErr;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn70;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn74;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn75;
        private DataGridViewTextBoxColumn Column98;
        private DataGridViewTextBoxColumn Column99;
        public DataGridView dataTurbUncert;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn76;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn77;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn78;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn79;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn80;
        private DataGridViewTextBoxColumn Column100;
        private DataGridViewTextBoxColumn Column101;
        private DataGridViewTextBoxColumn Column102;
        private DataGridViewTextBoxColumn Column103;
        public DataGridView dataZones;
        public DataGridView dataZoneIceHits;
        public DataGridView dataShadowZoneSummary;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn86;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn87;
        public DataGridView dataZoneSound;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn90;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn91;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn88;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn89;
        private DataGridViewTextBoxColumn Column104;
        private DataGridViewTextBoxColumn Column105;
        private DataGridViewTextBoxColumn Column106;
        private DataGridViewTextBoxColumn Column107;
        public DataGridView dataShadow12x24;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn92;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn93;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn94;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn95;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn96;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn97;
        private DataGridViewTextBoxColumn Column108;
        private DataGridViewTextBoxColumn Column109;
        private DataGridViewTextBoxColumn Column110;
        private DataGridViewTextBoxColumn Column111;
        private DataGridViewTextBoxColumn Column112;
        private DataGridViewTextBoxColumn Column113;
        private DataGridViewTextBoxColumn Column114;
        private DataGridViewTextBoxColumn Column115;
        public DataGridView dataPathNodes;
        public DataGridView dataModCrossPred;
        private DataGridViewTextBoxColumn Column116;
        private DataGridViewTextBoxColumn Column117;
        private DataGridViewTextBoxColumn Column118;
        private DataGridViewTextBoxColumn Column119;
        public DataGridView dataPathNodes_UW;
        private DataGridViewTextBoxColumn Site;
        private DataGridViewTextBoxColumn Flow;
        private DataGridViewTextBoxColumn Coeff;
        private DataGridViewTextBoxColumn dWSExpo;
        private DataGridViewTextBoxColumn dWSSRDH;
        public DataGridView dataPathNodes_DW;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn98;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn99;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn100;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn101;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn102;
        private SplitContainer splotLTRef_1;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer3;
        public DataGridView dataRefAnnualProd;
        private DataGridViewTextBoxColumn Column44;
        private DataGridViewTextBoxColumn Column45;
        private DataGridViewTextBoxColumn Column46;
        private SplitContainer splitContainer4;
        private SplitContainer splitContainer2;
        public OxyPlot.WindowsForms.PlotView plotMERRA_Yearly;
        public OxyPlot.WindowsForms.PlotView plotMERRA_Monthly;
        public Button btnExportCloudCover;
        public TextBox txtRefDataDownloadName;
        private Label label216;
        public TextBox txtRefDataDownloadFolder;
        private Button btn_ExportWR;
        private Button btn_Export_All_Months_All_Years;
        private Button btn_Export_Interp;
        private Label label154;
        public TextBox txtLT_Avg_Prod;
        public TextBox txtLT_Avg_CF;
        public TextBox txtLT_Avg_WS;
        private Label label162;
        private Label label131;
        private Label label87;
        private Label label217;
        public TextBox txtMaxLong;
        private Label label218;
        public TextBox txtMinLong;
        private Label label219;
        public TextBox txtMaxLat;
        private Label label220;
        public TextBox txtMinLat;
        private Label label205;
        public TextBox txtRefDataAvail;
        private Label label204;
        public DateTimePicker dateLTRefAvailEnd;
        public DateTimePicker dateLTRefAvailStart;
        private Label label52;
        private Label label54;
        public DataGridView dataRefMonthlyProd;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn Column47;
        public ComboBox cboNumWDRefTab;
        private Label label223;
        internal CheckBox chkYearsToDisplayAll;
        internal CheckedListBox chkYearsToDisplay;
        public OxyPlot.WindowsForms.PlotView plotMERRA_WindRose;
        private Button btnDelRef;
        private Button btnEditReference;
        private Button btnAddNew;
        private Label label207;
        public ComboBox cboLTReferences;
        public DateTimePicker dateMERRAEnd;
        public DateTimePicker dateMERRAStart;
        private Label label152;
        private Label label153;
        private SplitContainer spltMetTurbSumm;
        private SplitContainer splitContainer5;
        private SplitContainer splitContainer10;
        private SplitContainer splitContainer11;
        private SplitContainer splitContainer12;
        private SplitContainer splitContainer6;
        private SplitContainer splitContainer7;
        private SplitContainer splitContainer8;
        private SplitContainer splitContainer9;
        public DataGridView dataMetStats;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        public DataGridView dataTurbStats;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        public DataGridView dataMetSummary;
        private DataGridViewTextBoxColumn Column52;
        private DataGridViewTextBoxColumn Column53;
        private DataGridViewTextBoxColumn Column54;
        private DataGridViewTextBoxColumn Column55;
        private DataGridViewTextBoxColumn Column56;
        private DataGridViewTextBoxColumn Column57;
        private DataGridViewTextBoxColumn Column58;
        private DataGridViewTextBoxColumn Column59;
        private DataGridViewTextBoxColumn Column60;
        private DataGridViewTextBoxColumn Column61;
        private DataGridViewTextBoxColumn Column62;
        public OxyPlot.WindowsForms.PlotView plotUWExpo;
        public OxyPlot.WindowsForms.PlotView plotDWExpo;
        public OxyPlot.WindowsForms.PlotView plotDWUWExpo;
        public OxyPlot.WindowsForms.PlotView plotElev;
        public OxyPlot.WindowsForms.PlotView plotUW_SR;
        public OxyPlot.WindowsForms.PlotView plotDW_SR;
        public OxyPlot.WindowsForms.PlotView plotUW_DH;
        public OxyPlot.WindowsForms.PlotView plotDW_DH;
        internal CheckedListBox chkMetSumm;
        internal CheckedListBox chkTurbSumm;
        internal TextBox txtTotalGrossCF;
        internal Label label245;
        internal TextBox txtTotalGross;
        internal Label label246;
        private Button btnDeletePowerCurve;
        private Button btnExport_PowerCurve;
        internal Label label247;
        internal Label label248;
        public OxyPlot.WindowsForms.PlotView plotDirectionalWS_Ratios;
        internal Button btnImportMetData;
        internal Label lblMetDataType;
        internal TextBox txtNameplateCapacity;
        internal Label label249;
        private DataGridViewCheckBoxColumn Column74;
        private DataGridViewTextBoxColumn colPowerCrvName;
        private DataGridViewTextBoxColumn Column75;
        private DataGridViewTextBoxColumn Column76;
        private DataGridViewCheckBoxColumn Column121;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private DataGridViewTextBoxColumn Column77;
        private DataGridViewTextBoxColumn Column78;
        private DataGridViewTextBoxColumn Column79;
        private DataGridViewTextBoxColumn Column80;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn59;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn61;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn62;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn63;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn64;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn65;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn66;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn67;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn55;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn56;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn57;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn58;
        private DataGridViewTextBoxColumn Column86;
        private DataGridViewTextBoxColumn Column87;
        private DataGridViewTextBoxColumn Column88;
        private DataGridViewTextBoxColumn Column89;
        private DataGridViewTextBoxColumn Column90;
        private DataGridViewTextBoxColumn Column91;
        internal Label lblModelsUsedGross;
        internal TextBox txtGross_Valley_used;
        internal TextBox txtGross_Elev_used;
        internal TextBox txtElev_Net;
        internal TextBox txtValley_Net;
        internal Label label9;
        internal TextBox txtRR_Valley_used;
        internal TextBox txtRR_Elev_used;
        internal TextBox txtAdv_Valley_used;
        internal TextBox txtAdv_Elev_used;
        internal Label label50;
        private DataGridViewCheckBoxColumn Column92;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private DataGridViewTextBoxColumn Column81;
        private DataGridViewTextBoxColumn Column82;
        private DataGridViewTextBoxColumn Column83;
        private DataGridViewTextBoxColumn Column84;
        private DataGridViewTextBoxColumn Column85;
        private DataGridViewCheckBoxColumn Column120;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn81;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn82;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn83;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn84;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn85;
        public OxyPlot.WindowsForms.PlotView plotMCP_Debias;
        public Button btnDebiasWS;
        private Label label250;
        public TextBox txtDebiasedWS;
        private Label label251;
        public CheckBox chkUseDebiased;
        public CheckBox chkUseMeasRose;
        public DataGridView dataDebias;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn103;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn104;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn105;
        public TextBox txtDebiasIntercept;
        private Label label253;
        public TextBox txtDebiasSlope;
        private Label label252;
        public TextBox txtDebiasRSq;
        private Label label254;
        public TextBox textBox1;
        public CheckBox chkUsingMetsForDebias;
        private Button btnEditMetsUsedForDebias;
    }
}

