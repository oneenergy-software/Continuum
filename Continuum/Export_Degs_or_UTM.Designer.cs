namespace ContinuumNS
{
    partial class Export_Degs_or_UTM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Export_Degs_or_UTM));
            this.Label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbo_Lats_UTMs = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(23, 52);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(61, 13);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "Select one:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(276, 43);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 31);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbo_Lats_UTMs
            // 
            this.cbo_Lats_UTMs.FormattingEnabled = true;
            this.cbo_Lats_UTMs.Items.AddRange(new object[] {
            "Lats/Longs",
            "UTM coords"});
            this.cbo_Lats_UTMs.Location = new System.Drawing.Point(87, 49);
            this.cbo_Lats_UTMs.Name = "cbo_Lats_UTMs";
            this.cbo_Lats_UTMs.Size = new System.Drawing.Size(145, 21);
            this.cbo_Lats_UTMs.TabIndex = 5;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(23, 16);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(349, 13);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "Would you like the turbine coordinates in Lat/Long or UTM coordinates?";
            // 
            // Export_Degs_or_UTM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 90);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbo_Lats_UTMs);
            this.Controls.Add(this.Label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Export_Degs_or_UTM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.ComboBox cbo_Lats_UTMs;
        internal System.Windows.Forms.Label Label1;
    }
}