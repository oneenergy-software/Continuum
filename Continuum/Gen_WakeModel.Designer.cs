namespace ContinuumNS
{
    partial class Gen_WakeModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gen_WakeModel));
            this.txt_FlowSep_Used = new System.Windows.Forms.TextBox();
            this.numWakeExp = new System.Windows.Forms.NumericUpDown();
            this.Label6 = new System.Windows.Forms.Label();
            this.numWRR = new System.Windows.Forms.NumericUpDown();
            this.Label5 = new System.Windows.Forms.Label();
            this.cboWakeCombo = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtLC_Used = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.cboUWDW = new System.Windows.Forms.ComboBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.txtCrossSpace = new System.Windows.Forms.TextBox();
            this.txtHorizWakeExp = new System.Windows.Forms.TextBox();
            this.cboPowerCrvs = new System.Windows.Forms.ComboBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.txtDownSpace = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.txtAmbTI = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.txtAmbRough = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboWakeModel = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnGenMap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numWakeExp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWRR)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_FlowSep_Used
            // 
            this.txt_FlowSep_Used.Location = new System.Drawing.Point(15, 449);
            this.txt_FlowSep_Used.Name = "txt_FlowSep_Used";
            this.txt_FlowSep_Used.ReadOnly = true;
            this.txt_FlowSep_Used.Size = new System.Drawing.Size(159, 20);
            this.txt_FlowSep_Used.TabIndex = 275;
            // 
            // numWakeExp
            // 
            this.numWakeExp.DecimalPlaces = 3;
            this.numWakeExp.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.numWakeExp.Location = new System.Drawing.Point(146, 199);
            this.numWakeExp.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWakeExp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numWakeExp.Name = "numWakeExp";
            this.numWakeExp.Size = new System.Drawing.Size(73, 20);
            this.numWakeExp.TabIndex = 274;
            this.numWakeExp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(12, 201);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(132, 13);
            this.Label6.TabIndex = 273;
            this.Label6.Text = "Wake distance exponent:";
            // 
            // numWRR
            // 
            this.numWRR.DecimalPlaces = 4;
            this.numWRR.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numWRR.Location = new System.Drawing.Point(146, 172);
            this.numWRR.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWRR.Name = "numWRR";
            this.numWRR.Size = new System.Drawing.Size(73, 20);
            this.numWRR.TabIndex = 272;
            this.numWRR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(25, 174);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(115, 13);
            this.Label5.TabIndex = 271;
            this.Label5.Text = "Wake Recharge Rate:";
            // 
            // cboWakeCombo
            // 
            this.cboWakeCombo.FormattingEnabled = true;
            this.cboWakeCombo.Items.AddRange(new object[] {
            "Linear",
            "RSS",
            "Avg Lin&RSS",
            "Max",
            "Geometric",
            "Avg Lin&Max",
            "Avg Lin&Geo",
            "Avg RSS&Max",
            "Avg RSS&Geo",
            "Avg Max&Geo"});
            this.cboWakeCombo.Location = new System.Drawing.Point(129, 87);
            this.cboWakeCombo.Name = "cboWakeCombo";
            this.cboWakeCombo.Size = new System.Drawing.Size(111, 21);
            this.cboWakeCombo.TabIndex = 270;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(14, 91);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(116, 13);
            this.Label4.TabIndex = 269;
            this.Label4.Text = "Wake combo. method:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(13, 224);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(133, 13);
            this.Label3.TabIndex = 268;
            this.Label3.Text = "Deep Array model settings:";
            // 
            // txtLC_Used
            // 
            this.txtLC_Used.Location = new System.Drawing.Point(15, 423);
            this.txtLC_Used.Name = "txtLC_Used";
            this.txtLC_Used.ReadOnly = true;
            this.txtLC_Used.Size = new System.Drawing.Size(159, 20);
            this.txtLC_Used.TabIndex = 267;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(24, 380);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(92, 13);
            this.Label12.TabIndex = 266;
            this.Label12.Text = "Continuum Model:";
            // 
            // cboUWDW
            // 
            this.cboUWDW.FormattingEnabled = true;
            this.cboUWDW.Location = new System.Drawing.Point(14, 396);
            this.cboUWDW.Name = "cboUWDW";
            this.cboUWDW.Size = new System.Drawing.Size(172, 21);
            this.cboUWDW.TabIndex = 265;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(17, 314);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(116, 13);
            this.Label16.TabIndex = 264;
            this.Label16.Text = "roughness length (m) :";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(26, 274);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(94, 26);
            this.Label15.TabIndex = 263;
            this.Label15.Text = "Crosswind Turbine\r\nSpacing (in RDs) :";
            // 
            // txtCrossSpace
            // 
            this.txtCrossSpace.Location = new System.Drawing.Point(137, 274);
            this.txtCrossSpace.Name = "txtCrossSpace";
            this.txtCrossSpace.Size = new System.Drawing.Size(84, 20);
            this.txtCrossSpace.TabIndex = 262;
            // 
            // txtHorizWakeExp
            // 
            this.txtHorizWakeExp.Location = new System.Drawing.Point(146, 113);
            this.txtHorizWakeExp.Name = "txtHorizWakeExp";
            this.txtHorizWakeExp.Size = new System.Drawing.Size(73, 20);
            this.txtHorizWakeExp.TabIndex = 261;
            // 
            // cboPowerCrvs
            // 
            this.cboPowerCrvs.FormattingEnabled = true;
            this.cboPowerCrvs.Location = new System.Drawing.Point(13, 352);
            this.cboPowerCrvs.Name = "cboPowerCrvs";
            this.cboPowerCrvs.Size = new System.Drawing.Size(208, 21);
            this.cboPowerCrvs.TabIndex = 260;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(13, 336);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(71, 13);
            this.Label14.TabIndex = 259;
            this.Label14.Text = "power Curve:";
            // 
            // txtDownSpace
            // 
            this.txtDownSpace.Location = new System.Drawing.Point(137, 240);
            this.txtDownSpace.Name = "txtDownSpace";
            this.txtDownSpace.Size = new System.Drawing.Size(84, 20);
            this.txtDownSpace.TabIndex = 258;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(24, 240);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(96, 26);
            this.Label13.TabIndex = 257;
            this.Label13.Text = "Downwind Turbine\r\nSpacing (in RDs) :";
            // 
            // txtAmbTI
            // 
            this.txtAmbTI.Location = new System.Drawing.Point(112, 147);
            this.txtAmbTI.Name = "txtAmbTI";
            this.txtAmbTI.Size = new System.Drawing.Size(59, 20);
            this.txtAmbTI.TabIndex = 256;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(25, 147);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(81, 13);
            this.Label11.TabIndex = 255;
            this.Label11.Text = "Ambient TI (%) :";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(21, 113);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(118, 26);
            this.Label9.TabIndex = 254;
            this.Label9.Text = "Horizontal Wake\r\nExpansion Angle, degs:";
            // 
            // txtAmbRough
            // 
            this.txtAmbRough.Location = new System.Drawing.Point(137, 311);
            this.txtAmbRough.Name = "txtAmbRough";
            this.txtAmbRough.Size = new System.Drawing.Size(84, 20);
            this.txtAmbRough.TabIndex = 253;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(8, 475);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 45);
            this.btnCancel.TabIndex = 252;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboWakeModel
            // 
            this.cboWakeModel.FormattingEnabled = true;
            this.cboWakeModel.Items.AddRange(new object[] {
            "Eddy Viscosity Wake Model",
            "Eddy Viscosity (Deep Array Wind Model)"});
            this.cboWakeModel.Location = new System.Drawing.Point(11, 56);
            this.cboWakeModel.Name = "cboWakeModel";
            this.cboWakeModel.Size = new System.Drawing.Size(230, 21);
            this.cboWakeModel.TabIndex = 251;
            this.cboWakeModel.SelectedIndexChanged += new System.EventHandler(this.cboWakeModel_SelectedIndexChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(12, 40);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(96, 13);
            this.Label2.TabIndex = 250;
            this.Label2.Text = "Wake Loss Model:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(10, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(192, 24);
            this.Label1.TabIndex = 249;
            this.Label1.Text = "Wake Model Settings:";
            // 
            // btnGenMap
            // 
            this.btnGenMap.Location = new System.Drawing.Point(101, 475);
            this.btnGenMap.Name = "btnGenMap";
            this.btnGenMap.Size = new System.Drawing.Size(126, 45);
            this.btnGenMap.TabIndex = 248;
            this.btnGenMap.Text = "Generate Wake Model and Estimates";
            this.btnGenMap.UseVisualStyleBackColor = true;
            this.btnGenMap.Click += new System.EventHandler(this.btnGenMap_Click);
            // 
            // Gen_WakeModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 529);
            this.Controls.Add(this.txt_FlowSep_Used);
            this.Controls.Add(this.numWakeExp);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.numWRR);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.cboWakeCombo);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtLC_Used);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.cboUWDW);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.txtCrossSpace);
            this.Controls.Add(this.txtHorizWakeExp);
            this.Controls.Add(this.cboPowerCrvs);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.txtDownSpace);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.txtAmbTI);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.txtAmbRough);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboWakeModel);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnGenMap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Gen_WakeModel";
            ((System.ComponentModel.ISupportInitialize)(this.numWakeExp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWRR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txt_FlowSep_Used;
        internal System.Windows.Forms.NumericUpDown numWakeExp;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.NumericUpDown numWRR;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox cboWakeCombo;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtLC_Used;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.ComboBox cboUWDW;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.TextBox txtCrossSpace;
        internal System.Windows.Forms.TextBox txtHorizWakeExp;
        internal System.Windows.Forms.ComboBox cboPowerCrvs;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox txtDownSpace;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox txtAmbTI;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.TextBox txtAmbRough;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.ComboBox cboWakeModel;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnGenMap;
    }
}