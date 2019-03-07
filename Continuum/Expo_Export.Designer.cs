namespace ContinuumNS
{
    partial class Expo_Export
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Expo_Export));
            this.chkBulkSRDH = new System.Windows.Forms.CheckBox();
            this.chkSectorSRDH = new System.Windows.Forms.CheckBox();
            this.chkBulkExpos = new System.Windows.Forms.CheckBox();
            this.chkSector = new System.Windows.Forms.CheckBox();
            this.chkMets = new System.Windows.Forms.CheckedListBox();
            this.lblMets = new System.Windows.Forms.Label();
            this.chkboxAllMets = new System.Windows.Forms.CheckBox();
            this.lblTurbs = new System.Windows.Forms.Label();
            this.chkTurbs = new System.Windows.Forms.CheckedListBox();
            this.chkboxAllTurbs = new System.Windows.Forms.CheckBox();
            this.chkboxAllRadii = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblSelectRadii = new System.Windows.Forms.Label();
            this.chkRadii = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // chkBulkSRDH
            // 
            this.chkBulkSRDH.AutoSize = true;
            this.chkBulkSRDH.Checked = true;
            this.chkBulkSRDH.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBulkSRDH.Location = new System.Drawing.Point(21, 268);
            this.chkBulkSRDH.Name = "chkBulkSRDH";
            this.chkBulkSRDH.Size = new System.Drawing.Size(140, 30);
            this.chkBulkSRDH.TabIndex = 29;
            this.chkBulkSRDH.Text = "Export Bulk Upwind and\r\nDownwind SR && DH";
            this.chkBulkSRDH.UseVisualStyleBackColor = true;
            // 
            // chkSectorSRDH
            // 
            this.chkSectorSRDH.AutoSize = true;
            this.chkSectorSRDH.Checked = true;
            this.chkSectorSRDH.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSectorSRDH.Location = new System.Drawing.Point(21, 232);
            this.chkSectorSRDH.Name = "chkSectorSRDH";
            this.chkSectorSRDH.Size = new System.Drawing.Size(154, 30);
            this.chkSectorSRDH.TabIndex = 28;
            this.chkSectorSRDH.Text = "Export Sectorwise Surface \r\nRoughness && Disp. height";
            this.chkSectorSRDH.UseVisualStyleBackColor = true;
            // 
            // chkBulkExpos
            // 
            this.chkBulkExpos.AutoSize = true;
            this.chkBulkExpos.Checked = true;
            this.chkBulkExpos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBulkExpos.Location = new System.Drawing.Point(21, 196);
            this.chkBulkExpos.Name = "chkBulkExpos";
            this.chkBulkExpos.Size = new System.Drawing.Size(140, 30);
            this.chkBulkExpos.TabIndex = 27;
            this.chkBulkExpos.Text = "Export Bulk Upwind and\r\nDownwind Exposures";
            this.chkBulkExpos.UseVisualStyleBackColor = true;
            // 
            // chkSector
            // 
            this.chkSector.AutoSize = true;
            this.chkSector.Checked = true;
            this.chkSector.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSector.Location = new System.Drawing.Point(22, 160);
            this.chkSector.Name = "chkSector";
            this.chkSector.Size = new System.Drawing.Size(114, 30);
            this.chkSector.TabIndex = 26;
            this.chkSector.Text = "Export Sectorwise \r\nExposures";
            this.chkSector.UseVisualStyleBackColor = true;
            // 
            // chkMets
            // 
            this.chkMets.CheckOnClick = true;
            this.chkMets.FormattingEnabled = true;
            this.chkMets.HorizontalScrollbar = true;
            this.chkMets.Location = new System.Drawing.Point(299, 59);
            this.chkMets.Name = "chkMets";
            this.chkMets.Size = new System.Drawing.Size(102, 124);
            this.chkMets.TabIndex = 25;
            // 
            // lblMets
            // 
            this.lblMets.AutoSize = true;
            this.lblMets.Location = new System.Drawing.Point(320, 43);
            this.lblMets.Name = "lblMets";
            this.lblMets.Size = new System.Drawing.Size(51, 13);
            this.lblMets.TabIndex = 24;
            this.lblMets.Text = "Met Sites";
            // 
            // chkboxAllMets
            // 
            this.chkboxAllMets.AutoSize = true;
            this.chkboxAllMets.Checked = true;
            this.chkboxAllMets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkboxAllMets.Location = new System.Drawing.Point(311, 23);
            this.chkboxAllMets.Name = "chkboxAllMets";
            this.chkboxAllMets.Size = new System.Drawing.Size(84, 17);
            this.chkboxAllMets.TabIndex = 23;
            this.chkboxAllMets.Text = "All Met Sites";
            this.chkboxAllMets.UseVisualStyleBackColor = true;
            this.chkboxAllMets.CheckedChanged += new System.EventHandler(this.chkboxAllMets_CheckedChanged);
            // 
            // lblTurbs
            // 
            this.lblTurbs.AutoSize = true;
            this.lblTurbs.Location = new System.Drawing.Point(184, 43);
            this.lblTurbs.Name = "lblTurbs";
            this.lblTurbs.Size = new System.Drawing.Size(69, 13);
            this.lblTurbs.TabIndex = 22;
            this.lblTurbs.Text = "Turbine Sites";
            // 
            // chkTurbs
            // 
            this.chkTurbs.CheckOnClick = true;
            this.chkTurbs.FormattingEnabled = true;
            this.chkTurbs.HorizontalScrollbar = true;
            this.chkTurbs.Location = new System.Drawing.Point(181, 59);
            this.chkTurbs.Name = "chkTurbs";
            this.chkTurbs.Size = new System.Drawing.Size(96, 214);
            this.chkTurbs.TabIndex = 21;
            // 
            // chkboxAllTurbs
            // 
            this.chkboxAllTurbs.AutoSize = true;
            this.chkboxAllTurbs.Checked = true;
            this.chkboxAllTurbs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkboxAllTurbs.Location = new System.Drawing.Point(181, 23);
            this.chkboxAllTurbs.Name = "chkboxAllTurbs";
            this.chkboxAllTurbs.Size = new System.Drawing.Size(102, 17);
            this.chkboxAllTurbs.TabIndex = 20;
            this.chkboxAllTurbs.Text = "All Turbine Sites";
            this.chkboxAllTurbs.UseVisualStyleBackColor = true;
            this.chkboxAllTurbs.CheckedChanged += new System.EventHandler(this.chkboxAllTurbs_CheckedChanged);
            // 
            // chkboxAllRadii
            // 
            this.chkboxAllRadii.AutoSize = true;
            this.chkboxAllRadii.Checked = true;
            this.chkboxAllRadii.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkboxAllRadii.Location = new System.Drawing.Point(21, 13);
            this.chkboxAllRadii.Name = "chkboxAllRadii";
            this.chkboxAllRadii.Size = new System.Drawing.Size(64, 17);
            this.chkboxAllRadii.TabIndex = 19;
            this.chkboxAllRadii.Text = "All Radii";
            this.chkboxAllRadii.UseVisualStyleBackColor = true;
            this.chkboxAllRadii.CheckedChanged += new System.EventHandler(this.chkboxAllRadii_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(299, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 33);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(299, 196);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(102, 33);
            this.btnExport.TabIndex = 17;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblSelectRadii
            // 
            this.lblSelectRadii.AutoSize = true;
            this.lblSelectRadii.Location = new System.Drawing.Point(30, 43);
            this.lblSelectRadii.Name = "lblSelectRadii";
            this.lblSelectRadii.Size = new System.Drawing.Size(120, 13);
            this.lblSelectRadii.TabIndex = 16;
            this.lblSelectRadii.Text = "radius of Investigations";
            // 
            // chkRadii
            // 
            this.chkRadii.CheckOnClick = true;
            this.chkRadii.FormattingEnabled = true;
            this.chkRadii.HorizontalScrollbar = true;
            this.chkRadii.Location = new System.Drawing.Point(33, 59);
            this.chkRadii.Name = "chkRadii";
            this.chkRadii.Size = new System.Drawing.Size(126, 94);
            this.chkRadii.TabIndex = 15;
            // 
            // Expo_Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 310);
            this.Controls.Add(this.chkBulkSRDH);
            this.Controls.Add(this.chkSectorSRDH);
            this.Controls.Add(this.chkBulkExpos);
            this.Controls.Add(this.chkSector);
            this.Controls.Add(this.chkMets);
            this.Controls.Add(this.lblMets);
            this.Controls.Add(this.chkboxAllMets);
            this.Controls.Add(this.lblTurbs);
            this.Controls.Add(this.chkTurbs);
            this.Controls.Add(this.chkboxAllTurbs);
            this.Controls.Add(this.chkboxAllRadii);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblSelectRadii);
            this.Controls.Add(this.chkRadii);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Expo_Export";
            this.Text = "Export Exposure & roughness Values";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox chkBulkSRDH;
        internal System.Windows.Forms.CheckBox chkSectorSRDH;
        internal System.Windows.Forms.CheckBox chkBulkExpos;
        internal System.Windows.Forms.CheckBox chkSector;
        internal System.Windows.Forms.CheckedListBox chkMets;
        internal System.Windows.Forms.Label lblMets;
        internal System.Windows.Forms.CheckBox chkboxAllMets;
        internal System.Windows.Forms.Label lblTurbs;
        internal System.Windows.Forms.CheckedListBox chkTurbs;
        internal System.Windows.Forms.CheckBox chkboxAllTurbs;
        internal System.Windows.Forms.CheckBox chkboxAllRadii;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnExport;
        internal System.Windows.Forms.Label lblSelectRadii;
        internal System.Windows.Forms.CheckedListBox chkRadii;
    }
}