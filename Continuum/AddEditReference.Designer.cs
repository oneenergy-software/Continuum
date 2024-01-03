namespace ContinuumNS
{
    partial class AddEditReference
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditReference));
            this.label205 = new System.Windows.Forms.Label();
            this.txtRefDataAvail = new System.Windows.Forms.TextBox();
            this.label204 = new System.Windows.Forms.Label();
            this.label146 = new System.Windows.Forms.Label();
            this.dateLTRefAvailEnd = new System.Windows.Forms.DateTimePicker();
            this.dateLTRefAvailStart = new System.Windows.Forms.DateTimePicker();
            this.label52 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataLTRefNodes = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label162 = new System.Windows.Forms.Label();
            this.txtWS_ScaleFact = new System.Windows.Forms.TextBox();
            this.cboNumNodes = new System.Windows.Forms.ComboBox();
            this.label161 = new System.Windows.Forms.Label();
            this.dateRefEnd = new System.Windows.Forms.DateTimePicker();
            this.dateRefStart = new System.Windows.Forms.DateTimePicker();
            this.label152 = new System.Windows.Forms.Label();
            this.label153 = new System.Windows.Forms.Label();
            this.label147 = new System.Windows.Forms.Label();
            this.cboTargetMet = new System.Windows.Forms.ComboBox();
            this.txtReferenceLong = new System.Windows.Forms.TextBox();
            this.label149 = new System.Windows.Forms.Label();
            this.label150 = new System.Windows.Forms.Label();
            this.txtReferenceLat = new System.Windows.Forms.TextBox();
            this.label154 = new System.Windows.Forms.Label();
            this.lalAddEditTitle = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.fbdRefDataFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboRefDataDownloads = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRefDataFolder = new System.Windows.Forms.TextBox();
            this.btnDownloadHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataLTRefNodes)).BeginInit();
            this.SuspendLayout();
            // 
            // label205
            // 
            this.label205.AutoSize = true;
            this.label205.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label205.Location = new System.Drawing.Point(32, 371);
            this.label205.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label205.Name = "label205";
            this.label205.Size = new System.Drawing.Size(95, 18);
            this.label205.TabIndex = 307;
            this.label205.Text = "Complete [%] :";
            // 
            // txtRefDataAvail
            // 
            this.txtRefDataAvail.Enabled = false;
            this.txtRefDataAvail.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefDataAvail.Location = new System.Drawing.Point(147, 371);
            this.txtRefDataAvail.Margin = new System.Windows.Forms.Padding(2);
            this.txtRefDataAvail.Name = "txtRefDataAvail";
            this.txtRefDataAvail.Size = new System.Drawing.Size(66, 25);
            this.txtRefDataAvail.TabIndex = 306;
            // 
            // label204
            // 
            this.label204.AutoSize = true;
            this.label204.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label204.Location = new System.Drawing.Point(31, 201);
            this.label204.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label204.Name = "label204";
            this.label204.Size = new System.Drawing.Size(188, 38);
            this.label204.TabIndex = 305;
            this.label204.Text = "Downloaded Data: \r\nDate Range and Complete %";
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Font = new System.Drawing.Font("Palatino Linotype", 11F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label146.Location = new System.Drawing.Point(25, 456);
            this.label146.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(262, 20);
            this.label146.TabIndex = 304;
            this.label146.Text = "Extracting Reference Data for Analysis";
            // 
            // dateLTRefAvailEnd
            // 
            this.dateLTRefAvailEnd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLTRefAvailEnd.CausesValidation = false;
            this.dateLTRefAvailEnd.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateLTRefAvailEnd.Enabled = false;
            this.dateLTRefAvailEnd.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLTRefAvailEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateLTRefAvailEnd.Location = new System.Drawing.Point(34, 327);
            this.dateLTRefAvailEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dateLTRefAvailEnd.MaxDate = new System.DateTime(2050, 12, 1, 0, 0, 0, 0);
            this.dateLTRefAvailEnd.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateLTRefAvailEnd.Name = "dateLTRefAvailEnd";
            this.dateLTRefAvailEnd.Size = new System.Drawing.Size(163, 25);
            this.dateLTRefAvailEnd.TabIndex = 303;
            this.dateLTRefAvailEnd.Value = new System.DateTime(2022, 12, 31, 23, 0, 0, 0);
            // 
            // dateLTRefAvailStart
            // 
            this.dateLTRefAvailStart.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLTRefAvailStart.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateLTRefAvailStart.Enabled = false;
            this.dateLTRefAvailStart.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLTRefAvailStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateLTRefAvailStart.Location = new System.Drawing.Point(33, 268);
            this.dateLTRefAvailStart.Margin = new System.Windows.Forms.Padding(2);
            this.dateLTRefAvailStart.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dateLTRefAvailStart.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateLTRefAvailStart.Name = "dateLTRefAvailStart";
            this.dateLTRefAvailStart.Size = new System.Drawing.Size(163, 25);
            this.dateLTRefAvailStart.TabIndex = 302;
            this.dateLTRefAvailStart.Value = new System.DateTime(2002, 1, 1, 0, 0, 0, 0);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(31, 307);
            this.label52.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(74, 18);
            this.label52.TabIndex = 301;
            this.label52.Text = "End (Local)";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(32, 248);
            this.label54.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(81, 18);
            this.label54.TabIndex = 300;
            this.label54.Text = "Start (Local)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(235, 182);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(248, 18);
            this.label5.TabIndex = 299;
            this.label5.Text = "Node Coordinates in Reference data files";
            // 
            // dataLTRefNodes
            // 
            this.dataLTRefNodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataLTRefNodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataLTRefNodes.Location = new System.Drawing.Point(235, 202);
            this.dataLTRefNodes.Margin = new System.Windows.Forms.Padding(2);
            this.dataLTRefNodes.Name = "dataLTRefNodes";
            this.dataLTRefNodes.ReadOnly = true;
            this.dataLTRefNodes.RowHeadersWidth = 51;
            this.dataLTRefNodes.RowTemplate.Height = 24;
            this.dataLTRefNodes.Size = new System.Drawing.Size(244, 248);
            this.dataLTRefNodes.TabIndex = 298;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Latitude [degs]";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 90;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Longitude [degs]";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 90;
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label162.Location = new System.Drawing.Point(279, 489);
            this.label162.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(109, 18);
            this.label162.TabIndex = 294;
            this.label162.Text = "WS Scale Factor :";
            // 
            // txtWS_ScaleFact
            // 
            this.txtWS_ScaleFact.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWS_ScaleFact.Location = new System.Drawing.Point(396, 487);
            this.txtWS_ScaleFact.Margin = new System.Windows.Forms.Padding(2);
            this.txtWS_ScaleFact.Name = "txtWS_ScaleFact";
            this.txtWS_ScaleFact.Size = new System.Drawing.Size(66, 25);
            this.txtWS_ScaleFact.TabIndex = 293;
            this.txtWS_ScaleFact.Text = "0.85";
            this.txtWS_ScaleFact.TextChanged += new System.EventHandler(this.txtWS_ScaleFact_TextChanged);
            // 
            // cboNumNodes
            // 
            this.cboNumNodes.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNumNodes.FormattingEnabled = true;
            this.cboNumNodes.Items.AddRange(new object[] {
            "1",
            "4",
            "16"});
            this.cboNumNodes.Location = new System.Drawing.Point(180, 489);
            this.cboNumNodes.Margin = new System.Windows.Forms.Padding(2);
            this.cboNumNodes.Name = "cboNumNodes";
            this.cboNumNodes.Size = new System.Drawing.Size(72, 26);
            this.cboNumNodes.TabIndex = 292;
            this.cboNumNodes.Text = "1";
            this.cboNumNodes.SelectedIndexChanged += new System.EventHandler(this.cboNumNodes_SelectedIndexChanged);
            // 
            // label161
            // 
            this.label161.AutoSize = true;
            this.label161.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label161.Location = new System.Drawing.Point(25, 489);
            this.label161.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(138, 18);
            this.label161.TabIndex = 291;
            this.label161.Text = "Num. of Nodes to use:";
            // 
            // dateRefEnd
            // 
            this.dateRefEnd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateRefEnd.CausesValidation = false;
            this.dateRefEnd.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateRefEnd.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateRefEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateRefEnd.Location = new System.Drawing.Point(146, 655);
            this.dateRefEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dateRefEnd.MaxDate = new System.DateTime(2050, 12, 1, 0, 0, 0, 0);
            this.dateRefEnd.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateRefEnd.Name = "dateRefEnd";
            this.dateRefEnd.Size = new System.Drawing.Size(163, 25);
            this.dateRefEnd.TabIndex = 290;
            this.dateRefEnd.Value = new System.DateTime(2022, 12, 31, 23, 0, 0, 0);
            this.dateRefEnd.ValueChanged += new System.EventHandler(this.dateRefEnd_ValueChanged);
            // 
            // dateRefStart
            // 
            this.dateRefStart.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateRefStart.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dateRefStart.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateRefStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateRefStart.Location = new System.Drawing.Point(146, 621);
            this.dateRefStart.Margin = new System.Windows.Forms.Padding(2);
            this.dateRefStart.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dateRefStart.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateRefStart.Name = "dateRefStart";
            this.dateRefStart.Size = new System.Drawing.Size(163, 25);
            this.dateRefStart.TabIndex = 289;
            this.dateRefStart.Value = new System.DateTime(2002, 1, 1, 0, 0, 0, 0);
            this.dateRefStart.ValueChanged += new System.EventHandler(this.dateMERRAStart_ValueChanged);
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label152.Location = new System.Drawing.Point(33, 655);
            this.label152.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(74, 18);
            this.label152.TabIndex = 288;
            this.label152.Text = "End (Local)";
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label153.Location = new System.Drawing.Point(32, 628);
            this.label153.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(81, 18);
            this.label153.TabIndex = 287;
            this.label153.Text = "Start (Local)";
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label147.Location = new System.Drawing.Point(41, 529);
            this.label147.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(88, 18);
            this.label147.TabIndex = 286;
            this.label147.Text = "Selected Met :";
            // 
            // cboTargetMet
            // 
            this.cboTargetMet.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTargetMet.FormattingEnabled = true;
            this.cboTargetMet.Items.AddRange(new object[] {
            "User-Defined Lat/Long"});
            this.cboTargetMet.Location = new System.Drawing.Point(145, 527);
            this.cboTargetMet.Margin = new System.Windows.Forms.Padding(2);
            this.cboTargetMet.Name = "cboTargetMet";
            this.cboTargetMet.Size = new System.Drawing.Size(320, 26);
            this.cboTargetMet.TabIndex = 285;
            this.cboTargetMet.SelectedIndexChanged += new System.EventHandler(this.cboTargetMet_SelectedIndexChanged);
            // 
            // txtReferenceLong
            // 
            this.txtReferenceLong.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferenceLong.Location = new System.Drawing.Point(327, 580);
            this.txtReferenceLong.Margin = new System.Windows.Forms.Padding(2);
            this.txtReferenceLong.Name = "txtReferenceLong";
            this.txtReferenceLong.Size = new System.Drawing.Size(110, 25);
            this.txtReferenceLong.TabIndex = 284;
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label149.Location = new System.Drawing.Point(232, 580);
            this.label149.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(72, 18);
            this.label149.TabIndex = 283;
            this.label149.Text = "Longitude:";
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label150.Location = new System.Drawing.Point(29, 581);
            this.label150.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(62, 18);
            this.label150.TabIndex = 282;
            this.label150.Text = "Latitude:";
            // 
            // txtReferenceLat
            // 
            this.txtReferenceLat.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferenceLat.Location = new System.Drawing.Point(103, 578);
            this.txtReferenceLat.Margin = new System.Windows.Forms.Padding(2);
            this.txtReferenceLat.Name = "txtReferenceLat";
            this.txtReferenceLat.Size = new System.Drawing.Size(110, 25);
            this.txtReferenceLat.TabIndex = 281;
            this.txtReferenceLat.TextChanged += new System.EventHandler(this.txtReferenceLat_TextChanged);
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label154.Location = new System.Drawing.Point(34, 117);
            this.label154.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(191, 18);
            this.label154.TabIndex = 280;
            this.label154.Text = "Reference Data Folder location:";
            // 
            // lalAddEditTitle
            // 
            this.lalAddEditTitle.AutoSize = true;
            this.lalAddEditTitle.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lalAddEditTitle.Location = new System.Drawing.Point(24, 23);
            this.lalAddEditTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lalAddEditTitle.Name = "lalAddEditTitle";
            this.lalAddEditTitle.Size = new System.Drawing.Size(333, 25);
            this.lalAddEditTitle.TabIndex = 309;
            this.lalAddEditTitle.Text = "Add Long-Term Reference Data";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.btnOK.Location = new System.Drawing.Point(430, 628);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 31);
            this.btnOK.TabIndex = 310;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Palatino Linotype", 8F);
            this.btnCancel.Location = new System.Drawing.Point(430, 673);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 31);
            this.btnCancel.TabIndex = 311;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboRefDataDownloads
            // 
            this.cboRefDataDownloads.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRefDataDownloads.FormattingEnabled = true;
            this.cboRefDataDownloads.Location = new System.Drawing.Point(31, 78);
            this.cboRefDataDownloads.Margin = new System.Windows.Forms.Padding(2);
            this.cboRefDataDownloads.Name = "cboRefDataDownloads";
            this.cboRefDataDownloads.Size = new System.Drawing.Size(445, 25);
            this.cboRefDataDownloads.TabIndex = 314;
            this.cboRefDataDownloads.SelectedIndexChanged += new System.EventHandler(this.cboRefDataDownloads_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 20);
            this.label1.TabIndex = 313;
            this.label1.Text = "Reference Data Downloads:";
            // 
            // txtRefDataFolder
            // 
            this.txtRefDataFolder.Enabled = false;
            this.txtRefDataFolder.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefDataFolder.Location = new System.Drawing.Point(31, 139);
            this.txtRefDataFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtRefDataFolder.Name = "txtRefDataFolder";
            this.txtRefDataFolder.Size = new System.Drawing.Size(445, 25);
            this.txtRefDataFolder.TabIndex = 315;
            // 
            // btnDownloadHelp
            // 
            this.btnDownloadHelp.Location = new System.Drawing.Point(229, 53);
            this.btnDownloadHelp.Name = "btnDownloadHelp";
            this.btnDownloadHelp.Size = new System.Drawing.Size(23, 23);
            this.btnDownloadHelp.TabIndex = 316;
            this.btnDownloadHelp.Text = "?";
            this.btnDownloadHelp.UseVisualStyleBackColor = true;
            this.btnDownloadHelp.Click += new System.EventHandler(this.btnDownloadHelp_Click);
            // 
            // AddEditReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(531, 717);
            this.Controls.Add(this.btnDownloadHelp);
            this.Controls.Add(this.txtRefDataFolder);
            this.Controls.Add(this.cboRefDataDownloads);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lalAddEditTitle);
            this.Controls.Add(this.label205);
            this.Controls.Add(this.txtRefDataAvail);
            this.Controls.Add(this.label204);
            this.Controls.Add(this.label146);
            this.Controls.Add(this.dateLTRefAvailEnd);
            this.Controls.Add(this.dateLTRefAvailStart);
            this.Controls.Add(this.label52);
            this.Controls.Add(this.label54);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataLTRefNodes);
            this.Controls.Add(this.label162);
            this.Controls.Add(this.txtWS_ScaleFact);
            this.Controls.Add(this.cboNumNodes);
            this.Controls.Add(this.label161);
            this.Controls.Add(this.dateRefEnd);
            this.Controls.Add(this.dateRefStart);
            this.Controls.Add(this.label152);
            this.Controls.Add(this.label153);
            this.Controls.Add(this.label147);
            this.Controls.Add(this.cboTargetMet);
            this.Controls.Add(this.txtReferenceLong);
            this.Controls.Add(this.label149);
            this.Controls.Add(this.label150);
            this.Controls.Add(this.txtReferenceLat);
            this.Controls.Add(this.label154);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddEditReference";
            this.Text = "Add/Edit New Long-Term Reference";
            ((System.ComponentModel.ISupportInitialize)(this.dataLTRefNodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label205;
        public System.Windows.Forms.TextBox txtRefDataAvail;
        private System.Windows.Forms.Label label204;
        private System.Windows.Forms.Label label146;
        public System.Windows.Forms.DateTimePicker dateLTRefAvailEnd;
        public System.Windows.Forms.DateTimePicker dateLTRefAvailStart;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.DataGridView dataLTRefNodes;
        private System.Windows.Forms.Label label162;
        public System.Windows.Forms.TextBox txtWS_ScaleFact;
        public System.Windows.Forms.ComboBox cboNumNodes;
        private System.Windows.Forms.Label label161;
        public System.Windows.Forms.DateTimePicker dateRefEnd;
        public System.Windows.Forms.DateTimePicker dateRefStart;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.Label label147;
        public System.Windows.Forms.ComboBox cboTargetMet;
        public System.Windows.Forms.TextBox txtReferenceLong;
        private System.Windows.Forms.Label label149;
        private System.Windows.Forms.Label label150;
        public System.Windows.Forms.TextBox txtReferenceLat;
        private System.Windows.Forms.Label label154;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.FolderBrowserDialog fbdRefDataFolder;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Label lalAddEditTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        public System.Windows.Forms.ComboBox cboRefDataDownloads;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtRefDataFolder;
        private System.Windows.Forms.Button btnDownloadHelp;
    }
}