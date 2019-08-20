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
            this.Label1.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(17, 102);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(61, 18);
            this.Label1.TabIndex = 19;
            this.Label1.Text = "String # :";
            // 
            // txtStrNum
            // 
            this.txtStrNum.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStrNum.Location = new System.Drawing.Point(79, 99);
            this.txtStrNum.Name = "txtStrNum";
            this.txtStrNum.Size = new System.Drawing.Size(48, 25);
            this.txtStrNum.TabIndex = 18;
            // 
            // lblUTMY
            // 
            this.lblUTMY.AutoSize = true;
            this.lblUTMY.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUTMY.Location = new System.Drawing.Point(15, 71);
            this.lblUTMY.Name = "lblUTMY";
            this.lblUTMY.Size = new System.Drawing.Size(120, 18);
            this.lblUTMY.TabIndex = 17;
            this.lblUTMY.Text = "Northing (UTMY) :";
            // 
            // txtUTMY
            // 
            this.txtUTMY.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUTMY.Location = new System.Drawing.Point(137, 68);
            this.txtUTMY.Name = "txtUTMY";
            this.txtUTMY.Size = new System.Drawing.Size(99, 25);
            this.txtUTMY.TabIndex = 16;
            // 
            // lblUTMX
            // 
            this.lblUTMX.AutoSize = true;
            this.lblUTMX.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUTMX.Location = new System.Drawing.Point(15, 44);
            this.lblUTMX.Name = "lblUTMX";
            this.lblUTMX.Size = new System.Drawing.Size(108, 18);
            this.lblUTMX.TabIndex = 15;
            this.lblUTMX.Text = "Easting (UTMX) :";
            // 
            // txtUTMX
            // 
            this.txtUTMX.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUTMX.Location = new System.Drawing.Point(137, 40);
            this.txtUTMX.Name = "txtUTMX";
            this.txtUTMX.Size = new System.Drawing.Size(99, 25);
            this.txtUTMX.TabIndex = 14;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(137, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(99, 25);
            this.txtName.TabIndex = 13;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(15, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 18);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "Turbine Name :";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(253, 91);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 37);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(253, 47);
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
            this.ClientSize = new System.Drawing.Size(361, 142);
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