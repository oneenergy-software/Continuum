namespace ContinuumNS
{
    partial class Registration_key
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registration_key));
            this.btnDelLicense = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRegContinuum = new System.Windows.Forms.Button();
            this.txtRegKey = new System.Windows.Forms.TextBox();
            this.lblRegKey = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDelLicense
            // 
            this.btnDelLicense.Location = new System.Drawing.Point(14, 118);
            this.btnDelLicense.Name = "btnDelLicense";
            this.btnDelLicense.Size = new System.Drawing.Size(109, 28);
            this.btnDelLicense.TabIndex = 25;
            this.btnDelLicense.Text = "Enter New License";
            this.btnDelLicense.UseVisualStyleBackColor = true;
            this.btnDelLicense.Click += new System.EventHandler(this.btnDelLicense_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.SystemColors.Info;
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.ForeColor = System.Drawing.Color.Red;
            this.txtMessage.Location = new System.Drawing.Point(11, 61);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(412, 41);
            this.txtMessage.TabIndex = 24;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(260, 118);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 28);
            this.btnOk.TabIndex = 23;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(346, 118);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 28);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRegContinuum
            // 
            this.btnRegContinuum.Location = new System.Drawing.Point(129, 118);
            this.btnRegContinuum.Name = "btnRegContinuum";
            this.btnRegContinuum.Size = new System.Drawing.Size(120, 28);
            this.btnRegContinuum.TabIndex = 21;
            this.btnRegContinuum.Text = "Register Continuum";
            this.btnRegContinuum.UseVisualStyleBackColor = true;
            this.btnRegContinuum.Click += new System.EventHandler(this.btnRegContinuum_Click);
            // 
            // txtRegKey
            // 
            this.txtRegKey.Location = new System.Drawing.Point(105, 14);
            this.txtRegKey.Multiline = true;
            this.txtRegKey.Name = "txtRegKey";
            this.txtRegKey.Size = new System.Drawing.Size(321, 41);
            this.txtRegKey.TabIndex = 20;
            // 
            // lblRegKey
            // 
            this.lblRegKey.AutoSize = true;
            this.lblRegKey.Location = new System.Drawing.Point(11, 11);
            this.lblRegKey.Name = "lblRegKey";
            this.lblRegKey.Size = new System.Drawing.Size(87, 13);
            this.lblRegKey.TabIndex = 19;
            this.lblRegKey.Text = "Registration Key:";
            // 
            // Registration_key
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 156);
            this.Controls.Add(this.btnDelLicense);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRegContinuum);
            this.Controls.Add(this.txtRegKey);
            this.Controls.Add(this.lblRegKey);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Registration_key";
            this.Text = "Enter Registration Key for Continuum";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnDelLicense;
        internal System.Windows.Forms.TextBox txtMessage;
        internal System.Windows.Forms.Button btnOk;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnRegContinuum;
        internal System.Windows.Forms.TextBox txtRegKey;
        internal System.Windows.Forms.Label lblRegKey;
    }
}