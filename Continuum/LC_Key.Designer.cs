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
            this.dataLC_SR_DH = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataLC_SR_DH)).BeginInit();
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
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            // dataLC_SR_DH
            // 
            this.dataLC_SR_DH.AllowUserToAddRows = false;
            this.dataLC_SR_DH.AllowUserToDeleteRows = false;
            this.dataLC_SR_DH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataLC_SR_DH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataLC_SR_DH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataLC_SR_DH.Location = new System.Drawing.Point(20, 80);
            this.dataLC_SR_DH.Name = "dataLC_SR_DH";
            this.dataLC_SR_DH.ReadOnly = true;
            this.dataLC_SR_DH.RowHeadersVisible = false;
            this.dataLC_SR_DH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataLC_SR_DH.Size = new System.Drawing.Size(442, 340);
            this.dataLC_SR_DH.TabIndex = 135;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Land Cover Code";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Land Cover Description";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 240;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Surface Roughness [m]";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Displacement Height [m]";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 75;
            // 
            // LC_Key
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 480);
            this.Controls.Add(this.dataLC_SR_DH);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cboLC_Key);
            this.Controls.Add(this.btnModKey);
            this.Controls.Add(this.btnNewKey);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LC_Key";
            ((System.ComponentModel.ISupportInitialize)(this.dataLC_SR_DH)).EndInit();
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
        public System.Windows.Forms.DataGridView dataLC_SR_DH;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}