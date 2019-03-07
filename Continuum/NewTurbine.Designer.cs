namespace ContinuumNS
{
    partial class NewTurbine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTurbine));
            this.Label1 = new System.Windows.Forms.Label();
            this.txtStrNum = new System.Windows.Forms.TextBox();
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
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(53, 97);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(50, 13);
            this.Label1.TabIndex = 19;
            this.Label1.Text = "String # :";
            // 
            // txtStrNum
            // 
            this.txtStrNum.Location = new System.Drawing.Point(115, 94);
            this.txtStrNum.Name = "txtStrNum";
            this.txtStrNum.Size = new System.Drawing.Size(48, 20);
            this.txtStrNum.TabIndex = 18;
            // 
            // lblUTMY
            // 
            this.lblUTMY.AutoSize = true;
            this.lblUTMY.Location = new System.Drawing.Point(15, 70);
            this.lblUTMY.Name = "lblUTMY";
            this.lblUTMY.Size = new System.Drawing.Size(93, 13);
            this.lblUTMY.TabIndex = 17;
            this.lblUTMY.Text = "Northing (UTMY) :";
            // 
            // txtUTMY
            // 
            this.txtUTMY.Location = new System.Drawing.Point(115, 67);
            this.txtUTMY.Name = "txtUTMY";
            this.txtUTMY.Size = new System.Drawing.Size(100, 20);
            this.txtUTMY.TabIndex = 16;
            // 
            // lblUTMX
            // 
            this.lblUTMX.AutoSize = true;
            this.lblUTMX.Location = new System.Drawing.Point(15, 42);
            this.lblUTMX.Name = "lblUTMX";
            this.lblUTMX.Size = new System.Drawing.Size(88, 13);
            this.lblUTMX.TabIndex = 15;
            this.lblUTMX.Text = "Easting (UTMX) :";
            // 
            // txtUTMX
            // 
            this.txtUTMX.Location = new System.Drawing.Point(115, 39);
            this.txtUTMX.Name = "txtUTMX";
            this.txtUTMX.Size = new System.Drawing.Size(100, 20);
            this.txtUTMX.TabIndex = 14;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(115, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 13;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(15, 14);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(80, 13);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "Turbine Name :";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(243, 58);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 37);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(243, 14);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(97, 38);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // NewTurbine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 124);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtStrNum);
            this.Controls.Add(this.lblUTMY);
            this.Controls.Add(this.txtUTMY);
            this.Controls.Add(this.lblUTMX);
            this.Controls.Add(this.txtUTMX);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewTurbine";
            this.Text = "Add New Turbine Location";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtStrNum;
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