namespace ContinuumNS
{
    partial class TopoDataDownload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopoDataDownload));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOpenTopoAPI = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.sfdTopoFile = new System.Windows.Forms.SaveFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMinLat = new System.Windows.Forms.TextBox();
            this.txtMaxLat = new System.Windows.Forms.TextBox();
            this.txtMaxLon = new System.Windows.Forms.TextBox();
            this.txtMinLon = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnFindBoundBox = new System.Windows.Forms.Button();
            this.txtBoundBoxBuffer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblOpenTopoLink = new System.Windows.Forms.Label();
            this.cboOpenTopoData = new System.Windows.Forms.ComboBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtGridReso = new System.Windows.Forms.TextBox();
            this.fbdTopoFile = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(471, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Download Topography Data (.geoTiff) from OpenTopo*\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(452, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "* Registration with OpenTopo required to access API (https://portal.opentopograph" +
    "y.org/login)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "OpenTopo API Key:";
            // 
            // txtOpenTopoAPI
            // 
            this.txtOpenTopoAPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpenTopoAPI.Location = new System.Drawing.Point(141, 99);
            this.txtOpenTopoAPI.Name = "txtOpenTopoAPI";
            this.txtOpenTopoAPI.Size = new System.Drawing.Size(348, 21);
            this.txtOpenTopoAPI.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(404, 319);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 32);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(312, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // sfdTopoFile
            // 
            this.sfdTopoFile.Filter = "GeoTiff files|*.tif";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Min. Latitude [degs]:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Max. Latitude [degs]:";
            // 
            // txtMinLat
            // 
            this.txtMinLat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinLat.Location = new System.Drawing.Point(133, 236);
            this.txtMinLat.Name = "txtMinLat";
            this.txtMinLat.Size = new System.Drawing.Size(70, 21);
            this.txtMinLat.TabIndex = 8;
            // 
            // txtMaxLat
            // 
            this.txtMaxLat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxLat.Location = new System.Drawing.Point(133, 266);
            this.txtMaxLat.Name = "txtMaxLat";
            this.txtMaxLat.Size = new System.Drawing.Size(70, 21);
            this.txtMaxLat.TabIndex = 9;
            // 
            // txtMaxLon
            // 
            this.txtMaxLon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxLon.Location = new System.Drawing.Point(346, 268);
            this.txtMaxLon.Name = "txtMaxLon";
            this.txtMaxLon.Size = new System.Drawing.Size(70, 21);
            this.txtMaxLon.TabIndex = 13;
            // 
            // txtMinLon
            // 
            this.txtMinLon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinLon.Location = new System.Drawing.Point(346, 238);
            this.txtMinLon.Name = "txtMinLon";
            this.txtMinLon.Size = new System.Drawing.Size(70, 21);
            this.txtMinLon.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Max. Longitude [degs]:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(228, 239);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Min. Longitude [degs]:";
            // 
            // btnFindBoundBox
            // 
            this.btnFindBoundBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindBoundBox.Location = new System.Drawing.Point(25, 312);
            this.btnFindBoundBox.Name = "btnFindBoundBox";
            this.btnFindBoundBox.Size = new System.Drawing.Size(131, 38);
            this.btnFindBoundBox.TabIndex = 14;
            this.btnFindBoundBox.Text = "Find Bounding Box around Mets && Turbines";
            this.btnFindBoundBox.UseVisualStyleBackColor = true;
            this.btnFindBoundBox.Click += new System.EventHandler(this.btnFindBoundBox_Click);
            // 
            // txtBoundBoxBuffer
            // 
            this.txtBoundBoxBuffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoundBoxBuffer.Location = new System.Drawing.Point(177, 339);
            this.txtBoundBoxBuffer.Name = "txtBoundBoxBuffer";
            this.txtBoundBoxBuffer.Size = new System.Drawing.Size(55, 23);
            this.txtBoundBoxBuffer.TabIndex = 16;
            this.txtBoundBoxBuffer.Text = "15";
            this.txtBoundBoxBuffer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(162, 307);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 26);
            this.label8.TabIndex = 15;
            this.label8.Text = "Buffer around Mets && \r\nTurbines (Min. 12 km):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(28, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 15);
            this.label9.TabIndex = 17;
            this.label9.Text = "Dataset:";
            // 
            // lblOpenTopoLink
            // 
            this.lblOpenTopoLink.AutoSize = true;
            this.lblOpenTopoLink.Location = new System.Drawing.Point(28, 192);
            this.lblOpenTopoLink.Name = "lblOpenTopoLink";
            this.lblOpenTopoLink.Size = new System.Drawing.Size(0, 13);
            this.lblOpenTopoLink.TabIndex = 18;
            // 
            // cboOpenTopoData
            // 
            this.cboOpenTopoData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOpenTopoData.FormattingEnabled = true;
            this.cboOpenTopoData.Items.AddRange(new object[] {
            "NASADEM",
            "AW3D30",
            "SRTMGL1",
            "SRTMGL3",
            "COP30",
            "COP90",
            "EU_DTM"});
            this.cboOpenTopoData.Location = new System.Drawing.Point(86, 134);
            this.cboOpenTopoData.Name = "cboOpenTopoData";
            this.cboOpenTopoData.Size = new System.Drawing.Size(110, 23);
            this.cboOpenTopoData.TabIndex = 20;
            this.cboOpenTopoData.SelectedIndexChanged += new System.EventHandler(this.cboOpenTopoData_SelectedIndexChanged);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.Location = new System.Drawing.Point(35, 163);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(0, 15);
            this.lblDesc.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(221, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 15);
            this.label10.TabIndex = 22;
            this.label10.Text = "Grid Resolution:";
            // 
            // txtGridReso
            // 
            this.txtGridReso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGridReso.Location = new System.Drawing.Point(322, 135);
            this.txtGridReso.Name = "txtGridReso";
            this.txtGridReso.ReadOnly = true;
            this.txtGridReso.Size = new System.Drawing.Size(65, 21);
            this.txtGridReso.TabIndex = 23;
            this.txtGridReso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TopoDataDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(511, 374);
            this.Controls.Add(this.txtGridReso);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.cboOpenTopoData);
            this.Controls.Add(this.lblOpenTopoLink);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtBoundBoxBuffer);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnFindBoundBox);
            this.Controls.Add(this.txtMaxLon);
            this.Controls.Add(this.txtMinLon);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMaxLat);
            this.Controls.Add(this.txtMinLat);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtOpenTopoAPI);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TopoDataDownload";
            this.Text = "Download Digital Elevation Data from OpenTopo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOpenTopoAPI;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SaveFileDialog sfdTopoFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMinLat;
        private System.Windows.Forms.TextBox txtMaxLat;
        private System.Windows.Forms.TextBox txtMaxLon;
        private System.Windows.Forms.TextBox txtMinLon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnFindBoundBox;
        private System.Windows.Forms.TextBox txtBoundBoxBuffer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblOpenTopoLink;
        private System.Windows.Forms.ComboBox cboOpenTopoData;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtGridReso;
        private System.Windows.Forms.FolderBrowserDialog fbdTopoFile;
    }
}