namespace ContinuumNS
{
    partial class LC_Key
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LC_Key));
            this.Label1 = new System.Windows.Forms.Label();
            this.cboLC_Key = new System.Windows.Forms.ComboBox();
            this.btnModKey = new System.Windows.Forms.Button();
            this.btnNewKey = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lstLC_SR_DH = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(21, 21);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(129, 18);
            this.Label1.TabIndex = 13;
            this.Label1.Text = "Land Cover key:";
            // 
            // cboLC_Key
            // 
            this.cboLC_Key.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLC_Key.FormattingEnabled = true;
            this.cboLC_Key.Items.AddRange(new object[] {
            "US NLCD",
            "North America NALCMS",
            "EU Corine 2006 LC",
            "South African National Land Cover (SANLC)",
            "User-Defined"});
            this.cboLC_Key.Location = new System.Drawing.Point(20, 48);
            this.cboLC_Key.Name = "cboLC_Key";
            this.cboLC_Key.Size = new System.Drawing.Size(194, 26);
            this.cboLC_Key.TabIndex = 12;
            this.cboLC_Key.Text = "US NLCD";
            this.cboLC_Key.SelectedIndexChanged += new System.EventHandler(this.cboLC_Key_SelectedIndexChanged);
            // 
            // btnModKey
            // 
            this.btnModKey.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModKey.Location = new System.Drawing.Point(230, 16);
            this.btnModKey.Margin = new System.Windows.Forms.Padding(2);
            this.btnModKey.Name = "btnModKey";
            this.btnModKey.Size = new System.Drawing.Size(109, 53);
            this.btnModKey.TabIndex = 11;
            this.btnModKey.Text = "Modify LC/SR/DH key";
            this.btnModKey.UseVisualStyleBackColor = true;
            this.btnModKey.Click += new System.EventHandler(this.btnModKey_Click);
            // 
            // btnNewKey
            // 
            this.btnNewKey.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewKey.Location = new System.Drawing.Point(352, 16);
            this.btnNewKey.Margin = new System.Windows.Forms.Padding(2);
            this.btnNewKey.Name = "btnNewKey";
            this.btnNewKey.Size = new System.Drawing.Size(109, 53);
            this.btnNewKey.TabIndex = 10;
            this.btnNewKey.Text = "Import LC/SR/DH key";
            this.btnNewKey.UseVisualStyleBackColor = true;
            this.btnNewKey.Click += new System.EventHandler(this.btnNewKey_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(352, 425);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 39);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(230, 425);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(109, 39);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstLC_SR_DH
            // 
            this.lstLC_SR_DH.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3,
            this.ColumnHeader4});
            this.lstLC_SR_DH.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLC_SR_DH.FullRowSelect = true;
            this.lstLC_SR_DH.HideSelection = false;
            this.lstLC_SR_DH.Location = new System.Drawing.Point(20, 88);
            this.lstLC_SR_DH.Margin = new System.Windows.Forms.Padding(2);
            this.lstLC_SR_DH.MultiSelect = false;
            this.lstLC_SR_DH.Name = "lstLC_SR_DH";
            this.lstLC_SR_DH.Size = new System.Drawing.Size(442, 303);
            this.lstLC_SR_DH.TabIndex = 7;
            this.lstLC_SR_DH.UseCompatibleStateImageBehavior = false;
            this.lstLC_SR_DH.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Land Cover Code";
            this.ColumnHeader1.Width = 121;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Land Cover Desc.";
            this.ColumnHeader2.Width = 131;
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "Surface Roughness, m";
            this.ColumnHeader3.Width = 156;
            // 
            // ColumnHeader4
            // 
            this.ColumnHeader4.Text = "Displacement height, m";
            this.ColumnHeader4.Width = 168;
            // 
            // LC_Key
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 480);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cboLC_Key);
            this.Controls.Add(this.btnModKey);
            this.Controls.Add(this.btnNewKey);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstLC_SR_DH);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LC_Key";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ComboBox cboLC_Key;
        internal System.Windows.Forms.Button btnModKey;
        internal System.Windows.Forms.Button btnNewKey;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.ListView lstLC_SR_DH;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        internal System.Windows.Forms.ColumnHeader ColumnHeader3;
        internal System.Windows.Forms.ColumnHeader ColumnHeader4;
    }
}