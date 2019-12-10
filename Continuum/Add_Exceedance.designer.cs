namespace ContinuumNS
{
    partial class Add_Exceedance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_Exceedance));
            this.label2 = new System.Windows.Forms.Label();
            this.txt_LowerBound = new System.Windows.Forms.TextBox();
            this.txt_UpperBound = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_AddMode = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lstModes = new System.Windows.Forms.ListView();
            this.Mean = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StDev = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Weight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_EditMode = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExceed = new System.Windows.Forms.TextBox();
            this.btnImportCDF = new System.Windows.Forms.Button();
            this.ofdImportCFD = new System.Windows.Forms.OpenFileDialog();
            this.plotExceed = new OxyPlot.WindowsForms.PlotView();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lower Bound:";
            // 
            // txt_LowerBound
            // 
            this.txt_LowerBound.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LowerBound.Location = new System.Drawing.Point(146, 84);
            this.txt_LowerBound.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_LowerBound.Name = "txt_LowerBound";
            this.txt_LowerBound.Size = new System.Drawing.Size(116, 29);
            this.txt_LowerBound.TabIndex = 3;
            // 
            // txt_UpperBound
            // 
            this.txt_UpperBound.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_UpperBound.Location = new System.Drawing.Point(146, 138);
            this.txt_UpperBound.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_UpperBound.Name = "txt_UpperBound";
            this.txt_UpperBound.Size = new System.Drawing.Size(116, 29);
            this.txt_UpperBound.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Upper Bound:";
            // 
            // btn_AddMode
            // 
            this.btn_AddMode.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddMode.Location = new System.Drawing.Point(565, 215);
            this.btn_AddMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_AddMode.Name = "btn_AddMode";
            this.btn_AddMode.Size = new System.Drawing.Size(115, 44);
            this.btn_AddMode.TabIndex = 8;
            this.btn_AddMode.Text = "Add Mode";
            this.btn_AddMode.UseVisualStyleBackColor = true;
            this.btn_AddMode.Click += new System.EventHandler(this.btn_AddMode_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(565, 427);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(119, 46);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(565, 492);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 46);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lstModes
            // 
            this.lstModes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Mean,
            this.StDev,
            this.Weight});
            this.lstModes.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstModes.HideSelection = false;
            this.lstModes.Location = new System.Drawing.Point(345, 69);
            this.lstModes.Margin = new System.Windows.Forms.Padding(2);
            this.lstModes.Name = "lstModes";
            this.lstModes.Size = new System.Drawing.Size(328, 128);
            this.lstModes.TabIndex = 11;
            this.lstModes.UseCompatibleStateImageBehavior = false;
            this.lstModes.View = System.Windows.Forms.View.Details;
            // 
            // Mean
            // 
            this.Mean.Text = "Mean";
            this.Mean.Width = 127;
            // 
            // StDev
            // 
            this.StDev.Text = "St. Dev.";
            this.StDev.Width = 148;
            // 
            // Weight
            // 
            this.Weight.Text = "Weight";
            this.Weight.Width = 95;
            // 
            // btn_EditMode
            // 
            this.btn_EditMode.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EditMode.Location = new System.Drawing.Point(565, 281);
            this.btn_EditMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_EditMode.Name = "btn_EditMode";
            this.btn_EditMode.Size = new System.Drawing.Size(115, 44);
            this.btn_EditMode.TabIndex = 13;
            this.btn_EditMode.Text = "Edit Mode";
            this.btn_EditMode.UseVisualStyleBackColor = true;
            this.btn_EditMode.Click += new System.EventHandler(this.btn_EditMode_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 22);
            this.label1.TabIndex = 14;
            this.label1.Text = "Exceedance:";
            // 
            // txtExceed
            // 
            this.txtExceed.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExceed.Location = new System.Drawing.Point(146, 25);
            this.txtExceed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtExceed.Name = "txtExceed";
            this.txtExceed.ReadOnly = true;
            this.txtExceed.Size = new System.Drawing.Size(272, 29);
            this.txtExceed.TabIndex = 15;
            // 
            // btnImportCDF
            // 
            this.btnImportCDF.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportCDF.Location = new System.Drawing.Point(565, 346);
            this.btnImportCDF.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportCDF.Name = "btnImportCDF";
            this.btnImportCDF.Size = new System.Drawing.Size(119, 54);
            this.btnImportCDF.TabIndex = 16;
            this.btnImportCDF.Text = "Import CDF (.csv)";
            this.btnImportCDF.UseVisualStyleBackColor = true;
            this.btnImportCDF.Click += new System.EventHandler(this.btnImportCDF_Click);
            // 
            // ofdImportCFD
            // 
            this.ofdImportCFD.DefaultExt = "txt";
            this.ofdImportCFD.FileName = "openFileDialog1";
            // 
            // plotExceed
            // 
            this.plotExceed.Location = new System.Drawing.Point(36, 228);
            this.plotExceed.Name = "plotExceed";
            this.plotExceed.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotExceed.Size = new System.Drawing.Size(485, 324);
            this.plotExceed.TabIndex = 238;
            this.plotExceed.Text = "plotView1";
            this.plotExceed.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotExceed.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotExceed.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // Add_Exceedance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 590);
            this.Controls.Add(this.plotExceed);
            this.Controls.Add(this.btnImportCDF);
            this.Controls.Add(this.txtExceed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_EditMode);
            this.Controls.Add(this.lstModes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btn_AddMode);
            this.Controls.Add(this.txt_UpperBound);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_LowerBound);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Add_Exceedance";
            this.Text = "Defining Exceedance Distribution";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_AddMode;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColumnHeader Mean;
        private System.Windows.Forms.ColumnHeader StDev;
        private System.Windows.Forms.ColumnHeader Weight;
        private System.Windows.Forms.Button btn_EditMode;
        public System.Windows.Forms.TextBox txt_LowerBound;
        public System.Windows.Forms.TextBox txt_UpperBound;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtExceed;
        public System.Windows.Forms.ListView lstModes;        
        private System.Windows.Forms.Button btnImportCDF;
        private System.Windows.Forms.OpenFileDialog ofdImportCFD;
        public OxyPlot.WindowsForms.PlotView plotExceed;
    }
}