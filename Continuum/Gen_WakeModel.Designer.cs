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
            this.cboWakeCombo = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtLC_Used = new System.Windows.Forms.TextBox();
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
            this.txtisMCPWakeModel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_FlowSep_Used
            // 
            this.txt_FlowSep_Used.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FlowSep_Used.Location = new System.Drawing.Point(29, 416);
            this.txt_FlowSep_Used.Name = "txt_FlowSep_Used";
            this.txt_FlowSep_Used.ReadOnly = true;
            this.txt_FlowSep_Used.Size = new System.Drawing.Size(159, 25);
            this.txt_FlowSep_Used.TabIndex = 275;
            // 
            // cboWakeCombo
            // 
            this.cboWakeCombo.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.cboWakeCombo.Location = new System.Drawing.Point(159, 123);
            this.cboWakeCombo.Name = "cboWakeCombo";
            this.cboWakeCombo.Size = new System.Drawing.Size(111, 26);
            this.cboWakeCombo.TabIndex = 270;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(21, 126);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(139, 18);
            this.Label4.TabIndex = 269;
            this.Label4.Text = "Wake combo. method:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(13, 235);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(206, 18);
            this.Label3.TabIndex = 268;
            this.Label3.Text = "Deep Array model settings:";
            // 
            // txtLC_Used
            // 
            this.txtLC_Used.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLC_Used.Location = new System.Drawing.Point(29, 383);
            this.txtLC_Used.Name = "txtLC_Used";
            this.txtLC_Used.ReadOnly = true;
            this.txtLC_Used.Size = new System.Drawing.Size(159, 25);
            this.txtLC_Used.TabIndex = 267;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(24, 320);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(143, 18);
            this.Label16.TabIndex = 264;
            this.Label16.Text = "Roughness length (m) :";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(23, 290);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(215, 18);
            this.Label15.TabIndex = 263;
            this.Label15.Text = "Crosswind Turbine Spacing (RDs) :";
            // 
            // txtCrossSpace
            // 
            this.txtCrossSpace.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCrossSpace.Location = new System.Drawing.Point(243, 289);
            this.txtCrossSpace.Name = "txtCrossSpace";
            this.txtCrossSpace.Size = new System.Drawing.Size(84, 25);
            this.txtCrossSpace.TabIndex = 262;
            // 
            // txtHorizWakeExp
            // 
            this.txtHorizWakeExp.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHorizWakeExp.Location = new System.Drawing.Point(177, 161);
            this.txtHorizWakeExp.Name = "txtHorizWakeExp";
            this.txtHorizWakeExp.Size = new System.Drawing.Size(73, 25);
            this.txtHorizWakeExp.TabIndex = 261;
            // 
            // cboPowerCrvs
            // 
            this.cboPowerCrvs.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPowerCrvs.FormattingEnabled = true;
            this.cboPowerCrvs.Location = new System.Drawing.Point(119, 92);
            this.cboPowerCrvs.Name = "cboPowerCrvs";
            this.cboPowerCrvs.Size = new System.Drawing.Size(208, 26);
            this.cboPowerCrvs.TabIndex = 260;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(21, 100);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(90, 18);
            this.Label14.TabIndex = 259;
            this.Label14.Text = "Power Curve:";
            // 
            // txtDownSpace
            // 
            this.txtDownSpace.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDownSpace.Location = new System.Drawing.Point(243, 258);
            this.txtDownSpace.Name = "txtDownSpace";
            this.txtDownSpace.Size = new System.Drawing.Size(84, 25);
            this.txtDownSpace.TabIndex = 258;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(24, 261);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(217, 18);
            this.Label13.TabIndex = 257;
            this.Label13.Text = "Downwind Turbine Spacing (RDs) :";
            // 
            // txtAmbTI
            // 
            this.txtAmbTI.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmbTI.Location = new System.Drawing.Point(176, 192);
            this.txtAmbTI.Name = "txtAmbTI";
            this.txtAmbTI.Size = new System.Drawing.Size(59, 25);
            this.txtAmbTI.TabIndex = 256;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(21, 198);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(104, 18);
            this.Label11.TabIndex = 255;
            this.Label11.Text = "Ambient TI (%) :";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(22, 152);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(144, 36);
            this.Label9.TabIndex = 254;
            this.Label9.Text = "Horizontal Wake\r\nExpansion Angle, degs:";
            // 
            // txtAmbRough
            // 
            this.txtAmbRough.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmbRough.Location = new System.Drawing.Point(170, 319);
            this.txtAmbRough.Name = "txtAmbRough";
            this.txtAmbRough.Size = new System.Drawing.Size(84, 25);
            this.txtAmbRough.TabIndex = 253;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(37, 455);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 54);
            this.btnCancel.TabIndex = 252;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboWakeModel
            // 
            this.cboWakeModel.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWakeModel.FormattingEnabled = true;
            this.cboWakeModel.Items.AddRange(new object[] {
            "Eddy Viscosity Wake Model",
            "Eddy Viscosity (Deep Array Wind Model)",
            "Jensen Model"});
            this.cboWakeModel.Location = new System.Drawing.Point(24, 63);
            this.cboWakeModel.Name = "cboWakeModel";
            this.cboWakeModel.Size = new System.Drawing.Size(303, 26);
            this.cboWakeModel.TabIndex = 251;
            this.cboWakeModel.SelectedIndexChanged += new System.EventHandler(this.cboWakeModel_SelectedIndexChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(12, 40);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(130, 20);
            this.Label2.TabIndex = 250;
            this.Label2.Text = "Wake Loss Model:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(10, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(203, 23);
            this.Label1.TabIndex = 249;
            this.Label1.Text = "Wake Model Settings";
            // 
            // btnGenMap
            // 
            this.btnGenMap.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenMap.Location = new System.Drawing.Point(130, 455);
            this.btnGenMap.Name = "btnGenMap";
            this.btnGenMap.Size = new System.Drawing.Size(166, 54);
            this.btnGenMap.TabIndex = 248;
            this.btnGenMap.Text = "Generate Wake Model and Net Estimates";
            this.btnGenMap.UseVisualStyleBackColor = true;
            this.btnGenMap.Click += new System.EventHandler(this.btnGenMap_Click);
            // 
            // txtisMCPWakeModel
            // 
            this.txtisMCPWakeModel.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtisMCPWakeModel.Location = new System.Drawing.Point(29, 350);
            this.txtisMCPWakeModel.Name = "txtisMCPWakeModel";
            this.txtisMCPWakeModel.ReadOnly = true;
            this.txtisMCPWakeModel.Size = new System.Drawing.Size(159, 25);
            this.txtisMCPWakeModel.TabIndex = 276;
            // 
            // Gen_WakeModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 527);
            this.Controls.Add(this.txtisMCPWakeModel);
            this.Controls.Add(this.txt_FlowSep_Used);
            this.Controls.Add(this.cboWakeCombo);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtLC_Used);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txt_FlowSep_Used;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtLC_Used;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnGenMap;
        internal System.Windows.Forms.TextBox txtisMCPWakeModel;
        public System.Windows.Forms.ComboBox cboWakeCombo;
        public System.Windows.Forms.TextBox txtCrossSpace;
        public System.Windows.Forms.TextBox txtHorizWakeExp;
        public System.Windows.Forms.ComboBox cboPowerCrvs;
        public System.Windows.Forms.TextBox txtDownSpace;
        public System.Windows.Forms.TextBox txtAmbTI;
        public System.Windows.Forms.TextBox txtAmbRough;
        public System.Windows.Forms.ComboBox cboWakeModel;
    }
}