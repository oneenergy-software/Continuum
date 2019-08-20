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
            this.cboNorthOrSouth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(20, 43);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(52, 18);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "Datum:";
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OK.Location = new System.Drawing.Point(439, 75);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(108, 39);
            this.btn_OK.TabIndex = 6;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // cbo_Datums
            // 
            this.cbo_Datums.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.cbo_Datums.Location = new System.Drawing.Point(82, 40);
            this.cbo_Datums.MaxDropDownItems = 11;
            this.cbo_Datums.Name = "cbo_Datums";
            this.cbo_Datums.Size = new System.Drawing.Size(333, 26);
            this.cbo_Datums.TabIndex = 5;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(20, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(217, 18);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "Select UTM datum and hemisphere";
            // 
            // cboNorthOrSouth
            // 
            this.cboNorthOrSouth.AutoCompleteCustomSource.AddRange(new string[] {
            "-60",
            "-59",
            "-58",
            "-57",
            "-56",
            "-55",
            "-54",
            "-53",
            "-52",
            "-51",
            "-50",
            "-49",
            "-48",
            "-47",
            "-46",
            "-45",
            "-44",
            "-43",
            "-42",
            "-41",
            "-40",
            "-39",
            "-38",
            "-37",
            "-36",
            "-35",
            "-34",
            "-33",
            "-32",
            "-31",
            "-30",
            "-29",
            "-28",
            "-27",
            "-26",
            "-25",
            "-24",
            "-23",
            "-22",
            "-21",
            "-20",
            "-19",
            "-18",
            "-17",
            "-16",
            "-15",
            "-14",
            "-13",
            "-12",
            "-11",
            "-10",
            "-9",
            "-8",
            "-7",
            "-6",
            "-5",
            "-4",
            "-3",
            "-2",
            "-1",
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
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60"});
            this.cboNorthOrSouth.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboNorthOrSouth.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNorthOrSouth.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNorthOrSouth.FormattingEnabled = true;
            this.cboNorthOrSouth.Items.AddRange(new object[] {
            "Northern",
            "Southern"});
            this.cboNorthOrSouth.Location = new System.Drawing.Point(109, 81);
            this.cboNorthOrSouth.MaxDropDownItems = 11;
            this.cboNorthOrSouth.Name = "cboNorthOrSouth";
            this.cboNorthOrSouth.Size = new System.Drawing.Size(131, 26);
            this.cboNorthOrSouth.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 18);
            this.label4.TabIndex = 23;
            this.label4.Text = "Hemisphere:";
            // 
            // UTM_datum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 135);
            this.Controls.Add(this.cboNorthOrSouth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.cbo_Datums);
            this.Controls.Add(this.Label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UTM_datum";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btn_OK;
        internal System.Windows.Forms.ComboBox cbo_Datums;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ComboBox cboNorthOrSouth;
        internal System.Windows.Forms.Label label4;
    }
}