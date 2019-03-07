namespace ContinuumNS
{
    partial class GenMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenMap));
            this.txtMap_FlowSep_Used = new System.Windows.Forms.TextBox();
            this.txtMap_LC_used = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.chkMetsToUse = new System.Windows.Forms.CheckedListBox();
            this.txtMapReso = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.cbo_GenMap_UWDW_mod = new System.Windows.Forms.ComboBox();
            this.cboPowerCrvs = new System.Windows.Forms.ComboBox();
            this.btnCoordsAllTurbs = new System.Windows.Forms.Button();
            this.btnMinMax = new System.Windows.Forms.Button();
            this.txtNumPoints = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtMaxUTMY = new System.Windows.Forms.TextBox();
            this.txtMinUTMY = new System.Windows.Forms.TextBox();
            this.txtMaxUTMX = new System.Windows.Forms.TextBox();
            this.txtMinUTMX = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGenMap = new System.Windows.Forms.Button();
            this.cboWhatToMap = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtMap_FlowSep_Used
            // 
            this.txtMap_FlowSep_Used.Location = new System.Drawing.Point(203, 192);
            this.txtMap_FlowSep_Used.Name = "txtMap_FlowSep_Used";
            this.txtMap_FlowSep_Used.ReadOnly = true;
            this.txtMap_FlowSep_Used.Size = new System.Drawing.Size(155, 20);
            this.txtMap_FlowSep_Used.TabIndex = 225;
            // 
            // txtMap_LC_used
            // 
            this.txtMap_LC_used.Location = new System.Drawing.Point(203, 166);
            this.txtMap_LC_used.Name = "txtMap_LC_used";
            this.txtMap_LC_used.ReadOnly = true;
            this.txtMap_LC_used.Size = new System.Drawing.Size(155, 20);
            this.txtMap_LC_used.TabIndex = 224;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(20, 89);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(133, 13);
            this.Label2.TabIndex = 223;
            this.Label2.Text = "Select Mets to use in Map:";
            // 
            // chkMetsToUse
            // 
            this.chkMetsToUse.FormattingEnabled = true;
            this.chkMetsToUse.Location = new System.Drawing.Point(16, 109);
            this.chkMetsToUse.Name = "chkMetsToUse";
            this.chkMetsToUse.Size = new System.Drawing.Size(169, 94);
            this.chkMetsToUse.TabIndex = 222;
            // 
            // txtMapReso
            // 
            this.txtMapReso.Location = new System.Drawing.Point(95, 231);
            this.txtMapReso.Name = "txtMapReso";
            this.txtMapReso.Size = new System.Drawing.Size(52, 20);
            this.txtMapReso.TabIndex = 221;
            this.txtMapReso.Text = "250";
            this.txtMapReso.TextChanged += new System.EventHandler(this.txtMapReso_TextChanged);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(205, 123);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(95, 13);
            this.Label9.TabIndex = 220;
            this.Label9.Text = "Continuum Model :";
            // 
            // cbo_GenMap_UWDW_mod
            // 
            this.cbo_GenMap_UWDW_mod.FormattingEnabled = true;
            this.cbo_GenMap_UWDW_mod.Items.AddRange(new object[] {
            "Upwind Exposure",
            "Downwind Exposure",
            "Wind Speed",
            "Gross AEP"});
            this.cbo_GenMap_UWDW_mod.Location = new System.Drawing.Point(203, 140);
            this.cbo_GenMap_UWDW_mod.Name = "cbo_GenMap_UWDW_mod";
            this.cbo_GenMap_UWDW_mod.Size = new System.Drawing.Size(160, 21);
            this.cbo_GenMap_UWDW_mod.TabIndex = 219;
            // 
            // cboPowerCrvs
            // 
            this.cboPowerCrvs.FormattingEnabled = true;
            this.cboPowerCrvs.Items.AddRange(new object[] {
            "Upwind Exposure",
            "Downwind Exposure",
            "Wind Speed",
            "Gross AEP"});
            this.cboPowerCrvs.Location = new System.Drawing.Point(25, 55);
            this.cboPowerCrvs.Name = "cboPowerCrvs";
            this.cboPowerCrvs.Size = new System.Drawing.Size(160, 21);
            this.cboPowerCrvs.TabIndex = 218;
            // 
            // btnCoordsAllTurbs
            // 
            this.btnCoordsAllTurbs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCoordsAllTurbs.Location = new System.Drawing.Point(213, 63);
            this.btnCoordsAllTurbs.Name = "btnCoordsAllTurbs";
            this.btnCoordsAllTurbs.Size = new System.Drawing.Size(157, 39);
            this.btnCoordsAllTurbs.TabIndex = 217;
            this.btnCoordsAllTurbs.Text = "Get Coords for Map that includes all turbine sites";
            this.btnCoordsAllTurbs.UseVisualStyleBackColor = true;
            this.btnCoordsAllTurbs.Click += new System.EventHandler(this.btnCoordsAllTurbs_Click);
            // 
            // btnMinMax
            // 
            this.btnMinMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinMax.Location = new System.Drawing.Point(213, 14);
            this.btnMinMax.Name = "btnMinMax";
            this.btnMinMax.Size = new System.Drawing.Size(157, 39);
            this.btnMinMax.TabIndex = 216;
            this.btnMinMax.Text = "Get Coords for Largest Possible Map";
            this.btnMinMax.UseVisualStyleBackColor = true;
            this.btnMinMax.Click += new System.EventHandler(this.btnMinMax_Click);
            // 
            // txtNumPoints
            // 
            this.txtNumPoints.Location = new System.Drawing.Point(104, 372);
            this.txtNumPoints.Name = "txtNumPoints";
            this.txtNumPoints.ReadOnly = true;
            this.txtNumPoints.Size = new System.Drawing.Size(81, 20);
            this.txtNumPoints.TabIndex = 215;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(20, 375);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(86, 13);
            this.Label8.TabIndex = 214;
            this.Label8.Text = "# of Grid Points: ";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(13, 236);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(85, 13);
            this.Label7.TabIndex = 213;
            this.Label7.Text = "Grid Resolution: ";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(20, 341);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(67, 13);
            this.Label6.TabIndex = 212;
            this.Label6.Text = "Max UTMY: ";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(23, 313);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(64, 13);
            this.Label5.TabIndex = 211;
            this.Label5.Text = "Min UTMY: ";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(20, 287);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(67, 13);
            this.Label4.TabIndex = 210;
            this.Label4.Text = "Max UTMX: ";
            // 
            // txtMaxUTMY
            // 
            this.txtMaxUTMY.Location = new System.Drawing.Point(93, 338);
            this.txtMaxUTMY.Name = "txtMaxUTMY";
            this.txtMaxUTMY.Size = new System.Drawing.Size(92, 20);
            this.txtMaxUTMY.TabIndex = 209;
            this.txtMaxUTMY.TextChanged += new System.EventHandler(this.txtMaxUTMY_TextChanged);
            // 
            // txtMinUTMY
            // 
            this.txtMinUTMY.Location = new System.Drawing.Point(93, 311);
            this.txtMinUTMY.Name = "txtMinUTMY";
            this.txtMinUTMY.Size = new System.Drawing.Size(92, 20);
            this.txtMinUTMY.TabIndex = 208;
            this.txtMinUTMY.TextChanged += new System.EventHandler(this.txtMinUTMY_TextChanged);
            // 
            // txtMaxUTMX
            // 
            this.txtMaxUTMX.Location = new System.Drawing.Point(93, 284);
            this.txtMaxUTMX.Name = "txtMaxUTMX";
            this.txtMaxUTMX.Size = new System.Drawing.Size(92, 20);
            this.txtMaxUTMX.TabIndex = 207;
            this.txtMaxUTMX.TextChanged += new System.EventHandler(this.txtMaxUTMX_TextChanged);
            // 
            // txtMinUTMX
            // 
            this.txtMinUTMX.Location = new System.Drawing.Point(93, 258);
            this.txtMinUTMX.Name = "txtMinUTMX";
            this.txtMinUTMX.Size = new System.Drawing.Size(92, 20);
            this.txtMinUTMX.TabIndex = 206;
            this.txtMinUTMX.TextChanged += new System.EventHandler(this.txtMinUTMX_TextChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(23, 261);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(64, 13);
            this.Label3.TabIndex = 205;
            this.Label3.Text = "Min UTMX: ";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(24, 11);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(127, 13);
            this.Label1.TabIndex = 204;
            this.Label1.Text = "Select Parameter to Map:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(222, 341);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 41);
            this.btnCancel.TabIndex = 203;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnGenMap
            // 
            this.btnGenMap.Location = new System.Drawing.Point(222, 284);
            this.btnGenMap.Name = "btnGenMap";
            this.btnGenMap.Size = new System.Drawing.Size(135, 41);
            this.btnGenMap.TabIndex = 202;
            this.btnGenMap.Text = "Generate Map";
            this.btnGenMap.UseVisualStyleBackColor = true;
            this.btnGenMap.Click += new System.EventHandler(this.btnGenMap_Click);
            // 
            // cboWhatToMap
            // 
            this.cboWhatToMap.FormattingEnabled = true;
            this.cboWhatToMap.Items.AddRange(new object[] {
            "Upwind Exposure",
            "Downwind Exposure",
            "Wind Speed",
            "Gross AEP"});
            this.cboWhatToMap.Location = new System.Drawing.Point(25, 28);
            this.cboWhatToMap.Name = "cboWhatToMap";
            this.cboWhatToMap.Size = new System.Drawing.Size(160, 21);
            this.cboWhatToMap.TabIndex = 201;
            this.cboWhatToMap.SelectedIndexChanged += new System.EventHandler(this.cboWhatToMap_SelectedIndexChanged);
            // 
            // GenMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 402);
            this.Controls.Add(this.txtMap_FlowSep_Used);
            this.Controls.Add(this.txtMap_LC_used);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.chkMetsToUse);
            this.Controls.Add(this.txtMapReso);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.cbo_GenMap_UWDW_mod);
            this.Controls.Add(this.cboPowerCrvs);
            this.Controls.Add(this.btnCoordsAllTurbs);
            this.Controls.Add(this.btnMinMax);
            this.Controls.Add(this.txtNumPoints);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.txtMaxUTMY);
            this.Controls.Add(this.txtMinUTMY);
            this.Controls.Add(this.txtMaxUTMX);
            this.Controls.Add(this.txtMinUTMX);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenMap);
            this.Controls.Add(this.cboWhatToMap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GenMap";
            this.Text = "Generate Map";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtMap_FlowSep_Used;
        internal System.Windows.Forms.TextBox txtMap_LC_used;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.CheckedListBox chkMetsToUse;
        internal System.Windows.Forms.TextBox txtMapReso;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.ComboBox cbo_GenMap_UWDW_mod;
        internal System.Windows.Forms.ComboBox cboPowerCrvs;
        internal System.Windows.Forms.Button btnCoordsAllTurbs;
        internal System.Windows.Forms.Button btnMinMax;
        internal System.Windows.Forms.TextBox txtNumPoints;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtMaxUTMY;
        internal System.Windows.Forms.TextBox txtMinUTMY;
        internal System.Windows.Forms.TextBox txtMaxUTMX;
        internal System.Windows.Forms.TextBox txtMinUTMX;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnGenMap;
        internal System.Windows.Forms.ComboBox cboWhatToMap;
    }
}