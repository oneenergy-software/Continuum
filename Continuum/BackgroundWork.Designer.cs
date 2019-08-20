namespace ContinuumNS
{
    partial class BackgroundWork
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackgroundWork));
            this.progbar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.BackgroundWorker_Map = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_MetCalcs = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_TurbCalcs = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_RoundRobin = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_Topo = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_SaveAs = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_WRG = new System.ComponentModel.BackgroundWorker();
            this.sfd_Background = new System.Windows.Forms.SaveFileDialog();
            this.BackgroundWorker_LandCover = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_WAsP_Map = new System.ComponentModel.BackgroundWorker();
            this.lblprogbar = new System.Windows.Forms.Label();
            this.BackgroundWorker_Node_SR_Recalc = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_MERRA = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_IceThrow = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_Shadow = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_Exceed = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // progbar
            // 
            this.progbar.Location = new System.Drawing.Point(20, 50);
            this.progbar.Name = "progbar";
            this.progbar.Size = new System.Drawing.Size(587, 25);
            this.progbar.TabIndex = 3;
            this.progbar.UseWaitCursor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(526, 89);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.UseWaitCursor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BackgroundWorker_Map
            // 
            this.BackgroundWorker_Map.WorkerReportsProgress = true;
            this.BackgroundWorker_Map.WorkerSupportsCancellation = true;
            this.BackgroundWorker_Map.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Map_DoWork);
            this.BackgroundWorker_Map.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Map_ProgressChanged);
            this.BackgroundWorker_Map.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_Map_RunWorkerCompleted);
            // 
            // BackgroundWorker_MetCalcs
            // 
            this.BackgroundWorker_MetCalcs.WorkerReportsProgress = true;
            this.BackgroundWorker_MetCalcs.WorkerSupportsCancellation = true;
            this.BackgroundWorker_MetCalcs.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_MetCalcs_DoWork);
            this.BackgroundWorker_MetCalcs.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_MetCalcs_ProgressChanged);
            this.BackgroundWorker_MetCalcs.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_MetCalcs_RunWorkerCompleted);
            // 
            // BackgroundWorker_TurbCalcs
            // 
            this.BackgroundWorker_TurbCalcs.WorkerReportsProgress = true;
            this.BackgroundWorker_TurbCalcs.WorkerSupportsCancellation = true;
            this.BackgroundWorker_TurbCalcs.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_TurbCalcs_DoWork);
            this.BackgroundWorker_TurbCalcs.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_TurbCalcs_ProgressChanged);
            this.BackgroundWorker_TurbCalcs.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_TurbCalcs_RunWorkerCompleted);
            // 
            // BackgroundWorker_RoundRobin
            // 
            this.BackgroundWorker_RoundRobin.WorkerReportsProgress = true;
            this.BackgroundWorker_RoundRobin.WorkerSupportsCancellation = true;
            this.BackgroundWorker_RoundRobin.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_RoundRobin_DoWork);
            this.BackgroundWorker_RoundRobin.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_RoundRobin_ProgressChanged);
            this.BackgroundWorker_RoundRobin.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RoundRobin_RunWorkerCompleted);
            // 
            // BackgroundWorker_Topo
            // 
            this.BackgroundWorker_Topo.WorkerReportsProgress = true;
            this.BackgroundWorker_Topo.WorkerSupportsCancellation = true;
            this.BackgroundWorker_Topo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Topo_DoWork);
            this.BackgroundWorker_Topo.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Topo_ProgressChanged);
            this.BackgroundWorker_Topo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_Topo_RunWorkerCompleted);
            // 
            // BackgroundWorker_SaveAs
            // 
            this.BackgroundWorker_SaveAs.WorkerReportsProgress = true;
            this.BackgroundWorker_SaveAs.WorkerSupportsCancellation = true;
            this.BackgroundWorker_SaveAs.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_SaveAs_DoWork);
            this.BackgroundWorker_SaveAs.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_SaveAs_ProgressChanged);
            this.BackgroundWorker_SaveAs.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_SaveAs_RunWorkerCompleted);
            // 
            // BackgroundWorker_WRG
            // 
            this.BackgroundWorker_WRG.WorkerReportsProgress = true;
            this.BackgroundWorker_WRG.WorkerSupportsCancellation = true;
            this.BackgroundWorker_WRG.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_WRG_DoWork);
            this.BackgroundWorker_WRG.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_WRG_ProgressChanged);
            this.BackgroundWorker_WRG.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_WRG_RunWorkerCompleted);
            // 
            // sfd_Background
            // 
            this.sfd_Background.DefaultExt = "wrg";
            this.sfd_Background.Filter = "WRG |*wrg";
            // 
            // BackgroundWorker_LandCover
            // 
            this.BackgroundWorker_LandCover.WorkerReportsProgress = true;
            this.BackgroundWorker_LandCover.WorkerSupportsCancellation = true;
            this.BackgroundWorker_LandCover.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_LandCover_DoWork);
            this.BackgroundWorker_LandCover.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_LandCover_ProgressChanged);
            this.BackgroundWorker_LandCover.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_LandCover_RunWorkerCompleted);
            // 
            // BackgroundWorker_WAsP_Map
            // 
            this.BackgroundWorker_WAsP_Map.WorkerReportsProgress = true;
            this.BackgroundWorker_WAsP_Map.WorkerSupportsCancellation = true;
            this.BackgroundWorker_WAsP_Map.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_WAsP_Map_DoWork);
            this.BackgroundWorker_WAsP_Map.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_WAsP_Map_ProgressChanged);
            this.BackgroundWorker_WAsP_Map.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_WAsP_Map_RunWorkerCompleted);
            // 
            // lblprogbar
            // 
            this.lblprogbar.AutoSize = true;
            this.lblprogbar.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprogbar.Location = new System.Drawing.Point(22, 16);
            this.lblprogbar.Name = "lblprogbar";
            this.lblprogbar.Size = new System.Drawing.Size(0, 22);
            this.lblprogbar.TabIndex = 4;
            this.lblprogbar.UseWaitCursor = true;
            // 
            // BackgroundWorker_Node_SR_Recalc
            // 
            this.BackgroundWorker_Node_SR_Recalc.WorkerReportsProgress = true;
            this.BackgroundWorker_Node_SR_Recalc.WorkerSupportsCancellation = true;
            this.BackgroundWorker_Node_SR_Recalc.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Node_SR_Recalc_DoWork);
            this.BackgroundWorker_Node_SR_Recalc.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Node_SR_Recalc_ProgressChanged);
            this.BackgroundWorker_Node_SR_Recalc.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_Node_SR_Recalc_RunWorkerCompleted);
            // 
            // BackgroundWorker_MERRA
            // 
            this.BackgroundWorker_MERRA.WorkerReportsProgress = true;
            this.BackgroundWorker_MERRA.WorkerSupportsCancellation = true;
            this.BackgroundWorker_MERRA.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_MERRA_DoWork);
            this.BackgroundWorker_MERRA.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_MERRA_ProgressChanged);
            this.BackgroundWorker_MERRA.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_MERRA_RunWorkerCompleted);
            // 
            // BackgroundWorker_IceThrow
            // 
            this.BackgroundWorker_IceThrow.WorkerReportsProgress = true;
            this.BackgroundWorker_IceThrow.WorkerSupportsCancellation = true;
            this.BackgroundWorker_IceThrow.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_IceThrow_DoWork);
            this.BackgroundWorker_IceThrow.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_IceThrow_ProgressChanged);
            this.BackgroundWorker_IceThrow.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_IceThrow_RunWorkerCompleted);
            // 
            // BackgroundWorker_Shadow
            // 
            this.BackgroundWorker_Shadow.WorkerReportsProgress = true;
            this.BackgroundWorker_Shadow.WorkerSupportsCancellation = true;
            this.BackgroundWorker_Shadow.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Shadow_DoWork);
            this.BackgroundWorker_Shadow.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Shadow_ProgressChanged);
            this.BackgroundWorker_Shadow.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_Shadow_RunWorkerCompleted);
            // 
            // BackgroundWorker_Exceed
            // 
            this.BackgroundWorker_Exceed.WorkerReportsProgress = true;
            this.BackgroundWorker_Exceed.WorkerSupportsCancellation = true;
            this.BackgroundWorker_Exceed.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Exceed_DoWork);
            this.BackgroundWorker_Exceed.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Exceed_ProgressChanged);
            this.BackgroundWorker_Exceed.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_Exceed_RunWorkerCompleted);
            // 
            // BackgroundWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 135);
            this.Controls.Add(this.lblprogbar);
            this.Controls.Add(this.progbar);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BackgroundWork";
            this.UseWaitCursor = true;            
            this.ResumeLayout(false);
            this.PerformLayout();
         //   this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(BackgroundWorker_Closed);
        }

        #endregion

        internal System.Windows.Forms.ProgressBar progbar;
        internal System.Windows.Forms.Button btnCancel;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_Map;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_MetCalcs;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_TurbCalcs;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_RoundRobin;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_Topo;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_SaveAs;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_WRG;
        internal System.Windows.Forms.SaveFileDialog sfd_Background;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_LandCover;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_WAsP_Map;
        private System.Windows.Forms.Label lblprogbar;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_Node_SR_Recalc;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_MERRA;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_IceThrow;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_Shadow;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker_Exceed;
    }
}