namespace ContinuumNS
{
    partial class Roughness_File_Format
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Roughness_File_Format));
            this.Label1 = new System.Windows.Forms.Label();
            this.cboRoughnessFile = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(23, 17);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(188, 16);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Select roughness File Format";
            // 
            // cboRoughnessFile
            // 
            this.cboRoughnessFile.FormattingEnabled = true;
            this.cboRoughnessFile.Items.AddRange(new object[] {
            "Land Cover codes (GeoTIFF)",
            "roughness contours (.MAP)"});
            this.cboRoughnessFile.Location = new System.Drawing.Point(26, 41);
            this.cboRoughnessFile.Name = "cboRoughnessFile";
            this.cboRoughnessFile.Size = new System.Drawing.Size(227, 21);
            this.cboRoughnessFile.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(176, 65);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Roughness_File_Format
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 105);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cboRoughnessFile);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Roughness_File_Format";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ComboBox cboRoughnessFile;
        internal System.Windows.Forms.Button btnOK;
    }
}