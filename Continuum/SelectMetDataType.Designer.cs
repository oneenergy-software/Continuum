namespace ContinuumNS
{
    partial class SelectMetDataType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectMetDataType));
            this.radioTAB = new System.Windows.Forms.RadioButton();
            this.radioTimeSeries = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioTAB
            // 
            this.radioTAB.AutoSize = true;
            this.radioTAB.Location = new System.Drawing.Point(44, 66);
            this.radioTAB.Name = "radioTAB";
            this.radioTAB.Size = new System.Drawing.Size(110, 17);
            this.radioTAB.TabIndex = 0;
            this.radioTAB.TabStop = true;
            this.radioTAB.Text = "Use Met TAB files";
            this.radioTAB.UseVisualStyleBackColor = true;
            // 
            // radioTimeSeries
            // 
            this.radioTimeSeries.AutoSize = true;
            this.radioTimeSeries.Location = new System.Drawing.Point(44, 93);
            this.radioTimeSeries.Name = "radioTimeSeries";
            this.radioTimeSeries.Size = new System.Drawing.Size(144, 17);
            this.radioTimeSeries.TabIndex = 1;
            this.radioTimeSeries.TabStop = true;
            this.radioTimeSeries.Text = "Use Met Time Series files";
            this.radioTimeSeries.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Met Data Type:";
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(167, 133);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(86, 29);
            this.btnContinue.TabIndex = 3;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(85, 133);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 29);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SelectMetDataType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 174);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioTimeSeries);
            this.Controls.Add(this.radioTAB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectMetDataType";
            this.Text = "Select Met Data Type";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnContinue;
        public System.Windows.Forms.RadioButton radioTAB;
        public System.Windows.Forms.RadioButton radioTimeSeries;
        public System.Windows.Forms.Button btnCancel;
    }
}