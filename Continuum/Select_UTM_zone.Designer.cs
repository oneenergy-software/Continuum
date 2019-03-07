namespace ContinuumNS
{
    partial class Select_UTM_zone
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Select_UTM_zone));
            this.Label3 = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.cbo_UTMNumber = new System.Windows.Forms.ComboBox();
            this.cboHemisphere = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(15, 17);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(95, 13);
            this.Label3.TabIndex = 11;
            this.Label3.Text = "Select UTM Zone:";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(278, 46);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 8;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // cbo_UTMNumber
            // 
            this.cbo_UTMNumber.FormattingEnabled = true;
            this.cbo_UTMNumber.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cbo_UTMNumber.Location = new System.Drawing.Point(120, 14);
            this.cbo_UTMNumber.Name = "cbo_UTMNumber";
            this.cbo_UTMNumber.Size = new System.Drawing.Size(73, 21);
            this.cbo_UTMNumber.TabIndex = 7;
            // 
            // cboHemisphere
            // 
            this.cboHemisphere.FormattingEnabled = true;
            this.cboHemisphere.Items.AddRange(new object[] {
            "Northern",
            "Southern"});
            this.cboHemisphere.Location = new System.Drawing.Point(120, 48);
            this.cboHemisphere.Name = "cboHemisphere";
            this.cboHemisphere.Size = new System.Drawing.Size(117, 21);
            this.cboHemisphere.TabIndex = 6;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(15, 51);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(99, 13);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Select hemisphere:";
            // 
            // Select_UTM_zone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 86);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.cbo_UTMNumber);
            this.Controls.Add(this.cboHemisphere);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Select_UTM_zone";
            this.Text = "Specify UTM zone for conversion to Lat/Long";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button btn_OK;
        internal System.Windows.Forms.ComboBox cbo_UTMNumber;
        internal System.Windows.Forms.ComboBox cboHemisphere;
        internal System.Windows.Forms.Label Label2;
    }
}