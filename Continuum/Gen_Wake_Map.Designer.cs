namespace ContinuumNS
{
    partial class GenWakeMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenWakeMap));
            this.txt_FlowSep_Used = new System.Windows.Forms.TextBox();
            this.txtLC_Used = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.cboUWDW = new System.Windows.Forms.ComboBox();
            this.cboWakeModels = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.chkMetsToUse = new System.Windows.Forms.CheckedListBox();
            this.btnCoordsAllTurbs = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtMapReso = new System.Windows.Forms.TextBox();
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
            this.btnGenMap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_FlowSep_Used
            // 
            this.txt_FlowSep_Used.Location = new System.Drawing.Point(17, 126);
            this.txt_FlowSep_Used.Name = "txt_FlowSep_Used";
            this.txt_FlowSep_Used.ReadOnly = true;
            this.txt_FlowSep_Used.Size = new System.Drawing.Size(141, 20);
            this.txt_FlowSep_Used.TabIndex = 260;
            // 
            // txtLC_Used
            // 
            this.txtLC_Used.Location = new System.Drawing.Point(17, 100);
            this.txtLC_Used.Name = "txtLC_Used";
            this.txtLC_Used.ReadOnly = true;
            this.txtLC_Used.Size = new System.Drawing.Size(141, 20);
            this.txtLC_Used.TabIndex = 259;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(19, 54);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(92, 13);
            this.Label12.TabIndex = 258;
            this.Label12.Text = "Continuum Model:";
            // 
            // cboUWDW
            // 
            this.cboUWDW.FormattingEnabled = true;
            this.cboUWDW.Location = new System.Drawing.Point(17, 70);
            this.cboUWDW.Name = "cboUWDW";
            this.cboUWDW.Size = new System.Drawing.Size(142, 21);
            this.cboUWDW.TabIndex = 257;
            // 
            // cboWakeModels
            // 
            this.cboWakeModels.FormattingEnabled = true;
            this.cboWakeModels.Location = new System.Drawing.Point(17, 28);
            this.cboWakeModels.Name = "cboWakeModels";
            this.cboWakeModels.Size = new System.Drawing.Size(351, 21);
            this.cboWakeModels.TabIndex = 256;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(24, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(136, 13);
            this.Label1.TabIndex = 255;
            this.Label1.Text = "Select Wake Model to use:";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(197, 59);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(133, 13);
            this.Label17.TabIndex = 254;
            this.Label17.Text = "Select Mets to use in Map:";
            // 
            // chkMetsToUse
            // 
            this.chkMetsToUse.FormattingEnabled = true;
            this.chkMetsToUse.Location = new System.Drawing.Point(182, 76);
            this.chkMetsToUse.Name = "chkMetsToUse";
            this.chkMetsToUse.Size = new System.Drawing.Size(184, 94);
            this.chkMetsToUse.TabIndex = 253;
            // 
            // btnCoordsAllTurbs
            // 
            this.btnCoordsAllTurbs.Location = new System.Drawing.Point(208, 184);
            this.btnCoordsAllTurbs.Name = "btnCoordsAllTurbs";
            this.btnCoordsAllTurbs.Size = new System.Drawing.Size(138, 45);
            this.btnCoordsAllTurbs.TabIndex = 252;
            this.btnCoordsAllTurbs.Text = "Get Coords for Map that include all turbine sites";
            this.btnCoordsAllTurbs.UseVisualStyleBackColor = true;
            this.btnCoordsAllTurbs.Click += new System.EventHandler(this.btnCoordsAllTurbs_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(182, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 45);
            this.btnCancel.TabIndex = 251;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtMapReso
            // 
            this.txtMapReso.Location = new System.Drawing.Point(97, 150);
            this.txtMapReso.Name = "txtMapReso";
            this.txtMapReso.Size = new System.Drawing.Size(52, 20);
            this.txtMapReso.TabIndex = 250;
            this.txtMapReso.Text = "250";
            this.txtMapReso.TextChanged += new System.EventHandler(this.txtMapReso_TextChanged);
            // 
            // txtNumPoints
            // 
            this.txtNumPoints.Location = new System.Drawing.Point(97, 285);
            this.txtNumPoints.Name = "txtNumPoints";
            this.txtNumPoints.ReadOnly = true;
            this.txtNumPoints.Size = new System.Drawing.Size(66, 20);
            this.txtNumPoints.TabIndex = 249;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(11, 288);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(86, 13);
            this.Label8.TabIndex = 248;
            this.Label8.Text = "# of Grid Points: ";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(15, 155);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(85, 13);
            this.Label7.TabIndex = 247;
            this.Label7.Text = "Grid Resolution: ";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(13, 260);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(67, 13);
            this.Label6.TabIndex = 246;
            this.Label6.Text = "Max UTMY: ";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(16, 232);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(64, 13);
            this.Label5.TabIndex = 245;
            this.Label5.Text = "Min UTMY: ";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(13, 206);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(67, 13);
            this.Label4.TabIndex = 244;
            this.Label4.Text = "Max UTMX: ";
            // 
            // txtMaxUTMY
            // 
            this.txtMaxUTMY.Location = new System.Drawing.Point(81, 257);
            this.txtMaxUTMY.Name = "txtMaxUTMY";
            this.txtMaxUTMY.Size = new System.Drawing.Size(82, 20);
            this.txtMaxUTMY.TabIndex = 243;
            this.txtMaxUTMY.TextChanged += new System.EventHandler(this.txtMaxUTMY_TextChanged);
            // 
            // txtMinUTMY
            // 
            this.txtMinUTMY.Location = new System.Drawing.Point(81, 230);
            this.txtMinUTMY.Name = "txtMinUTMY";
            this.txtMinUTMY.Size = new System.Drawing.Size(82, 20);
            this.txtMinUTMY.TabIndex = 242;
            this.txtMinUTMY.TextChanged += new System.EventHandler(this.txtMinUTMY_TextChanged);
            // 
            // txtMaxUTMX
            // 
            this.txtMaxUTMX.Location = new System.Drawing.Point(81, 203);
            this.txtMaxUTMX.Name = "txtMaxUTMX";
            this.txtMaxUTMX.Size = new System.Drawing.Size(82, 20);
            this.txtMaxUTMX.TabIndex = 241;
            this.txtMaxUTMX.TextChanged += new System.EventHandler(this.txtMaxUTMX_TextChanged);
            // 
            // txtMinUTMX
            // 
            this.txtMinUTMX.Location = new System.Drawing.Point(81, 177);
            this.txtMinUTMX.Name = "txtMinUTMX";
            this.txtMinUTMX.Size = new System.Drawing.Size(82, 20);
            this.txtMinUTMX.TabIndex = 240;
            this.txtMinUTMX.TextChanged += new System.EventHandler(this.txtMinUTMX_TextChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(16, 180);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(64, 13);
            this.Label3.TabIndex = 239;
            this.Label3.Text = "Min UTMX: ";
            // 
            // btnGenMap
            // 
            this.btnGenMap.Location = new System.Drawing.Point(278, 238);
            this.btnGenMap.Name = "btnGenMap";
            this.btnGenMap.Size = new System.Drawing.Size(90, 45);
            this.btnGenMap.TabIndex = 238;
            this.btnGenMap.Text = "Generate Wake Map";
            this.btnGenMap.UseVisualStyleBackColor = true;
            this.btnGenMap.Click += new System.EventHandler(this.btnGenMap_Click);
            // 
            // GenWakeMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 316);
            this.Controls.Add(this.txt_FlowSep_Used);
            this.Controls.Add(this.txtLC_Used);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.cboUWDW);
            this.Controls.Add(this.cboWakeModels);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.chkMetsToUse);
            this.Controls.Add(this.btnCoordsAllTurbs);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtMapReso);
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
            this.Controls.Add(this.btnGenMap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GenWakeMap";
            this.Text = "Generate Wake Map";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txt_FlowSep_Used;
        internal System.Windows.Forms.TextBox txtLC_Used;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.ComboBox cboUWDW;
        internal System.Windows.Forms.ComboBox cboWakeModels;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.CheckedListBox chkMetsToUse;
        internal System.Windows.Forms.Button btnCoordsAllTurbs;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.TextBox txtMapReso;
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
        internal System.Windows.Forms.Button btnGenMap;
    }
}