namespace ContinuumNS
{
    partial class FilterFlagLegend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterFlagLegend));
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOutsideRange = new System.Windows.Forms.TextBox();
            this.txtMissing = new System.Windows.Forms.TextBox();
            this.txtMinAnemSD = new System.Windows.Forms.TextBox();
            this.txtMaxAnemSD = new System.Windows.Forms.TextBox();
            this.txtIcing = new System.Windows.Forms.TextBox();
            this.txtTowerShadow = new System.Windows.Forms.TextBox();
            this.txtAnemMaxRange = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(98, 324);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 29);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter Flag Legend";
            // 
            // txtOutsideRange
            // 
            this.txtOutsideRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutsideRange.Location = new System.Drawing.Point(36, 56);
            this.txtOutsideRange.Name = "txtOutsideRange";
            this.txtOutsideRange.Size = new System.Drawing.Size(199, 23);
            this.txtOutsideRange.TabIndex = 2;
            this.txtOutsideRange.Text = "Outside Sensor Range";
            this.txtOutsideRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMissing
            // 
            this.txtMissing.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMissing.Location = new System.Drawing.Point(36, 94);
            this.txtMissing.Name = "txtMissing";
            this.txtMissing.Size = new System.Drawing.Size(199, 23);
            this.txtMissing.TabIndex = 3;
            this.txtMissing.Text = "Missing Data";
            this.txtMissing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMinAnemSD
            // 
            this.txtMinAnemSD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinAnemSD.Location = new System.Drawing.Point(36, 132);
            this.txtMinAnemSD.Name = "txtMinAnemSD";
            this.txtMinAnemSD.Size = new System.Drawing.Size(199, 23);
            this.txtMinAnemSD.TabIndex = 4;
            this.txtMinAnemSD.Text = "Anem SD Too Low";
            this.txtMinAnemSD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMaxAnemSD
            // 
            this.txtMaxAnemSD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxAnemSD.Location = new System.Drawing.Point(36, 170);
            this.txtMaxAnemSD.Name = "txtMaxAnemSD";
            this.txtMaxAnemSD.Size = new System.Drawing.Size(199, 23);
            this.txtMaxAnemSD.TabIndex = 5;
            this.txtMaxAnemSD.Text = "Anem SD Too High";
            this.txtMaxAnemSD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIcing
            // 
            this.txtIcing.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIcing.Location = new System.Drawing.Point(36, 246);
            this.txtIcing.Name = "txtIcing";
            this.txtIcing.Size = new System.Drawing.Size(199, 23);
            this.txtIcing.TabIndex = 6;
            this.txtIcing.Text = "Icing Detected";
            this.txtIcing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTowerShadow
            // 
            this.txtTowerShadow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTowerShadow.Location = new System.Drawing.Point(36, 284);
            this.txtTowerShadow.Name = "txtTowerShadow";
            this.txtTowerShadow.Size = new System.Drawing.Size(199, 23);
            this.txtTowerShadow.TabIndex = 7;
            this.txtTowerShadow.Text = "Tower Shadow Filter";
            this.txtTowerShadow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtAnemMaxRange
            // 
            this.txtAnemMaxRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnemMaxRange.Location = new System.Drawing.Point(36, 208);
            this.txtAnemMaxRange.Name = "txtAnemMaxRange";
            this.txtAnemMaxRange.Size = new System.Drawing.Size(199, 23);
            this.txtAnemMaxRange.TabIndex = 8;
            this.txtAnemMaxRange.Text = "Anem WS Range Too High";
            this.txtAnemMaxRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAnemMaxRange.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // FilterFlagLegend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(272, 369);
            this.Controls.Add(this.txtAnemMaxRange);
            this.Controls.Add(this.txtTowerShadow);
            this.Controls.Add(this.txtIcing);
            this.Controls.Add(this.txtMaxAnemSD);
            this.Controls.Add(this.txtMinAnemSD);
            this.Controls.Add(this.txtMissing);
            this.Controls.Add(this.txtOutsideRange);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FilterFlagLegend";
            this.Text = "Met Data QC Filter Flag Legend";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOutsideRange;
        private System.Windows.Forms.TextBox txtMissing;
        private System.Windows.Forms.TextBox txtMinAnemSD;
        private System.Windows.Forms.TextBox txtMaxAnemSD;
        private System.Windows.Forms.TextBox txtIcing;
        private System.Windows.Forms.TextBox txtTowerShadow;
        private System.Windows.Forms.TextBox txtAnemMaxRange;
    }
}