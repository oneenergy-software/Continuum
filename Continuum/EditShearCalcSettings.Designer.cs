namespace ContinuumNS
{
    partial class EditShearCalcSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditShearCalcSettings));
            this.label151 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboShearCalcTypes = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboMinHeight = new System.Windows.Forms.ComboBox();
            this.cboMaxHeight = new System.Windows.Forms.ComboBox();
            this.txtMetSite = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label151.Location = new System.Drawing.Point(11, 12);
            this.label151.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(285, 22);
            this.label151.TabIndex = 251;
            this.label151.Text = "Wind Shear Calculation Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 20);
            this.label2.TabIndex = 259;
            this.label2.Text = "Shear Calculation Type:";
            // 
            // cboShearCalcTypes
            // 
            this.cboShearCalcTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboShearCalcTypes.FormattingEnabled = true;
            this.cboShearCalcTypes.Items.AddRange(new object[] {
            "Avg Alpha over all pairs",
            "Best-Fit Alpha"});
            this.cboShearCalcTypes.Location = new System.Drawing.Point(28, 123);
            this.cboShearCalcTypes.Name = "cboShearCalcTypes";
            this.cboShearCalcTypes.Size = new System.Drawing.Size(445, 26);
            this.cboShearCalcTypes.TabIndex = 258;
            this.cboShearCalcTypes.SelectedIndexChanged += new System.EventHandler(this.cboShearCalcTypes_SelectedIndexChanged);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(397, 228);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 35);
            this.btnApply.TabIndex = 260;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(306, 228);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 35);
            this.btnCancel.TabIndex = 261;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 180);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 262;
            this.label1.Text = "Min Height [m]:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(262, 179);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 263;
            this.label3.Text = "Max Height [m]:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 265;
            this.label4.Text = "Met Site:";
            // 
            // cboMinHeight
            // 
            this.cboMinHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMinHeight.FormattingEnabled = true;
            this.cboMinHeight.Location = new System.Drawing.Point(152, 177);
            this.cboMinHeight.Name = "cboMinHeight";
            this.cboMinHeight.Size = new System.Drawing.Size(86, 26);
            this.cboMinHeight.TabIndex = 266;
            // 
            // cboMaxHeight
            // 
            this.cboMaxHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaxHeight.FormattingEnabled = true;
            this.cboMaxHeight.Location = new System.Drawing.Point(387, 178);
            this.cboMaxHeight.Name = "cboMaxHeight";
            this.cboMaxHeight.Size = new System.Drawing.Size(86, 26);
            this.cboMaxHeight.TabIndex = 267;
            // 
            // txtMetSite
            // 
            this.txtMetSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMetSite.Location = new System.Drawing.Point(28, 66);
            this.txtMetSite.Name = "txtMetSite";
            this.txtMetSite.ReadOnly = true;
            this.txtMetSite.Size = new System.Drawing.Size(445, 24);
            this.txtMetSite.TabIndex = 268;
            // 
            // EditShearCalcSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(502, 285);
            this.Controls.Add(this.txtMetSite);
            this.Controls.Add(this.cboMaxHeight);
            this.Controls.Add(this.cboMinHeight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboShearCalcTypes);
            this.Controls.Add(this.label151);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditShearCalcSettings";
            this.Text = "Edit Shear Calculation Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cboShearCalcTypes;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cboMinHeight;
        public System.Windows.Forms.ComboBox cboMaxHeight;
        private System.Windows.Forms.TextBox txtMetSite;
    }
}