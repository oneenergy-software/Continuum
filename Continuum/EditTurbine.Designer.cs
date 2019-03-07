namespace ContinuumNS
{
    partial class EditTurbine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditTurbine));
            this.lblUTMY = new System.Windows.Forms.Label();
            this.txtUTMY = new System.Windows.Forms.TextBox();
            this.lblUTMX = new System.Windows.Forms.Label();
            this.txtUTMX = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUTMY
            // 
            this.lblUTMY.AutoSize = true;
            this.lblUTMY.Location = new System.Drawing.Point(42, 74);
            this.lblUTMY.Name = "lblUTMY";
            this.lblUTMY.Size = new System.Drawing.Size(47, 13);
            this.lblUTMY.TabIndex = 27;
            this.lblUTMY.Text = "UTM Y :";
            // 
            // txtUTMY
            // 
            this.txtUTMY.Location = new System.Drawing.Point(95, 71);
            this.txtUTMY.Name = "txtUTMY";
            this.txtUTMY.Size = new System.Drawing.Size(100, 20);
            this.txtUTMY.TabIndex = 26;
            // 
            // lblUTMX
            // 
            this.lblUTMX.AutoSize = true;
            this.lblUTMX.Location = new System.Drawing.Point(42, 44);
            this.lblUTMX.Name = "lblUTMX";
            this.lblUTMX.Size = new System.Drawing.Size(47, 13);
            this.lblUTMX.TabIndex = 25;
            this.lblUTMX.Text = "UTM X :";
            // 
            // txtUTMX
            // 
            this.txtUTMX.Location = new System.Drawing.Point(95, 43);
            this.txtUTMX.Name = "txtUTMX";
            this.txtUTMX.Size = new System.Drawing.Size(100, 20);
            this.txtUTMX.TabIndex = 24;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(95, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 23;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(15, 18);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(80, 13);
            this.lblName.TabIndex = 22;
            this.lblName.Text = "Turbine Name :";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(243, 62);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 37);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(243, 18);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(97, 38);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // EditTurbine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 114);
            this.Controls.Add(this.lblUTMY);
            this.Controls.Add(this.txtUTMY);
            this.Controls.Add(this.lblUTMX);
            this.Controls.Add(this.txtUTMX);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditTurbine";
            this.Text = "Edit Turbine";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblUTMY;
        internal System.Windows.Forms.TextBox txtUTMY;
        internal System.Windows.Forms.Label lblUTMX;
        internal System.Windows.Forms.TextBox txtUTMX;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.Label lblName;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnOK;
    }
}