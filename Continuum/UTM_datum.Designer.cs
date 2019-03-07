namespace ContinuumNS
{
    partial class UTM_datum
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UTM_datum));
            this.Label2 = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.cbo_Datums = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(20, 71);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(41, 13);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "Datum:";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(439, 58);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(108, 39);
            this.btn_OK.TabIndex = 6;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // cbo_Datums
            // 
            this.cbo_Datums.FormattingEnabled = true;
            this.cbo_Datums.Items.AddRange(new object[] {
            "NAD83/WGS84 (Global)",
            "GRS80 (US)",
            "WGS72 (NASA, DOD)",
            "Australian 1965",
            "Krasovsky 1940 (Russia)",
            "International 1924 (Europe)",
            "Clarke 1880 (France, Africa)",
            "NAD27/Clarke 1866 (North America)",
            "Airy 1830 (Great Britain)",
            "Bessel 1841 (Central Europe, Chile, Indonesia)",
            "Everest 1830 (South Asia)"});
            this.cbo_Datums.Location = new System.Drawing.Point(71, 68);
            this.cbo_Datums.MaxDropDownItems = 11;
            this.cbo_Datums.Name = "cbo_Datums";
            this.cbo_Datums.Size = new System.Drawing.Size(333, 21);
            this.cbo_Datums.TabIndex = 5;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(20, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(484, 36);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "The entered latitude and longitude will be converted to UTM coordinates. \r\nPlease" +
    " select the UTM datum and click Ok.";
            // 
            // UTM_datum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 110);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.cbo_Datums);
            this.Controls.Add(this.Label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UTM_datum";
            this.Text = "Converting to UTM coordinates";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btn_OK;
        internal System.Windows.Forms.ComboBox cbo_Datums;
        internal System.Windows.Forms.Label Label1;
    }
}