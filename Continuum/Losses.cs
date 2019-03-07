using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuumNS
{
    [Serializable()]
    public partial class Losses : Form
    {
        Continuum thisInst;
        public Losses(Continuum continuum)
        {
            InitializeComponent();
            thisInst = continuum;
        }

        public double Previous_Other_Losses;  // when the other losses are altered, this keeps the previous value so that the turbine ests
                                             // can be updated to reflect new loss factors
                                             
        private void btnRestore_Click(object sender, EventArgs e)
        {
            // Restores default //other// losses and updates textboxes on forms
            thisInst.otherLosses.Set_Defaults();

            txtTurbAvail.Text = Math.Round(100 * thisInst.otherLosses.Turb_Avail_Loss, 2).ToString() + " %";
            txtBOPAvail.Text = Math.Round(100 * thisInst.otherLosses.BOP_Avail_Loss, 2).ToString() + " %";
            txtElecLoss.Text = Math.Round(100 * thisInst.otherLosses.Electrical_Loss, 2).ToString() + " %";
            txtIcingLoss.Text = Math.Round(100 * thisInst.otherLosses.Icing_Loss, 2).ToString() + " %";
            txtBladeSoilLoss.Text = Math.Round(100 * thisInst.otherLosses.Blade_Soil_Loss, 2).ToString() + " %";
            txtBladeDegradeLoss.Text = Math.Round(100 * thisInst.otherLosses.Blade_Degrade_Loss, 2).ToString() + " %";
            txtHighLowTempLoss.Text = Math.Round(100 * thisInst.otherLosses.HighLowTemp_Loss, 2).ToString() + " %";
            txtPowerCrvLoss.Text = Math.Round(100 * thisInst.otherLosses.Power_Crv_Loss, 2).ToString() + " %";
            txtTurbulenceLoss.Text = Math.Round(100 * thisInst.otherLosses.Turbulence_Loss, 2).ToString() + " %";
            txtGrid_Curt.Text = Math.Round(100 * thisInst.otherLosses.Grid_Curtail_Loss, 2).ToString() + " %";
            txtWakeSM_Loss.Text = Math.Round(100 * thisInst.otherLosses.Wake_Sect_Curtail_Loss, 2).ToString() + " %";
            txtEnviro_Curt.Text = Math.Round(100 * thisInst.otherLosses.Environ_Curtail_Loss, 2).ToString() + " %";

            Update updateThe = new Update();
            updateThe.LossTextboxes(thisInst);
        }

    private void txtTurbAvail_TextChanged(object sender, EventArgs e)
        {
            // Updates Turbine Availability loss and updates forms

            double New_Turb_Avail = 0;

            try
            {
                if (txtTurbAvail.Text.Substring(txtTurbAvail.Text.Length - 1, 1) == "%")
                    New_Turb_Avail = Convert.ToSingle(txtTurbAvail.Text.Substring(0, txtTurbAvail.Text.Length - 2)) / 100;
                else
                {
                    New_Turb_Avail = Convert.ToSingle(txtTurbAvail.Text);
                    txtTurbAvail.Text = Math.Round(New_Turb_Avail, 2) + " %";
                    return;
                }
            }
            catch 
            {
                txtTurbAvail.Text = Math.Round(thisInst.otherLosses.Turb_Avail_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no = 0;

            if (New_Turb_Avail > 0.1)
            {
                yes_or_no = MessageBox.Show("Turbine Availability loss entered as :" + Math.Round(New_Turb_Avail * 100, 2) +
                                            "%. The default value is 3.5%. Do you want to use the entered value?", "", MessageBoxButtons.YesNo);
                if (yes_or_no == DialogResult.Yes)

                    thisInst.otherLosses.Turb_Avail_Loss = New_Turb_Avail;
                else {
                    txtTurbAvail.Text = Math.Round(100 * thisInst.otherLosses.Turb_Avail_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_Turb_Avail < 0)
            {
                txtTurbAvail.Text = Math.Round(thisInst.otherLosses.Turb_Avail_Loss, 2) + " %";
                return;
            }
            else 
                thisInst.otherLosses.Turb_Avail_Loss = New_Turb_Avail;

            double Overall_Avail_Eff = 100 * (1 - thisInst.otherLosses.Turb_Avail_Loss) * (1 - thisInst.otherLosses.BOP_Avail_Loss);
            double overallAvailLoss = 100 - Overall_Avail_Eff;

            txtAvailLossOverall.Text = Math.Round(overallAvailLoss, 2) + " %";
            txtAvailEff.Text = Math.Round(Overall_Avail_Eff, 2) + " %";
            Update_Overall_Loss_Eff();

        }
        
        private void txtBOPAvail_TextChanged(object sender, EventArgs e)
        {
            // Updates BOP Availability loss and updates forms
            double New_BOP_Avail = 0;

            try
            {

                if (txtBOPAvail.Text.Substring(txtBOPAvail.Text.Length - 1, 1) == "%")
                    New_BOP_Avail = Convert.ToSingle(txtBOPAvail.Text.Substring(0, txtBOPAvail.Text.Length - 2)) / 100;
                else
                {
                    New_BOP_Avail = Convert.ToSingle(txtBOPAvail.Text);
                    txtBOPAvail.Text = Math.Round(New_BOP_Avail, 2) + " %";
                    return;
                }
            }
            catch 
            {
                txtBOPAvail.Text = Math.Round(thisInst.otherLosses.BOP_Avail_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no;

            if (New_BOP_Avail > 0.1)
            {
                yes_or_no = MessageBox.Show("Balance of Plant (BOP) Availability loss entered as :" + Math.Round(New_BOP_Avail * 100, 2) +
                                            "%. The default value is 1%. Do you want to use the entered value?", "", MessageBoxButtons.YesNo);

                if (yes_or_no == DialogResult.Yes)
                    thisInst.otherLosses.BOP_Avail_Loss = New_BOP_Avail;
                else {
                    txtTurbAvail.Text = Math.Round(thisInst.otherLosses.BOP_Avail_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_BOP_Avail < 0)
            {
                txtBOPAvail.Text = Math.Round(thisInst.otherLosses.BOP_Avail_Loss, 2) + " %";
                return;
            }
            else
                thisInst.otherLosses.BOP_Avail_Loss = New_BOP_Avail;

            double Overall_Avail_Eff = 100 * (1 - thisInst.otherLosses.Turb_Avail_Loss) * (1 - thisInst.otherLosses.BOP_Avail_Loss);
            double overallAvailLoss = 100 - Overall_Avail_Eff;

            txtAvailLossOverall.Text = Math.Round(overallAvailLoss, 2) + " %";
            txtAvailEff.Text = Math.Round(Overall_Avail_Eff, 2) + " %";
            Update_Overall_Loss_Eff();
        }


        private void txtElecLoss_TextChanged(object sender, EventArgs e)
        {
            // Updates Electricity loss and updates forms
            double New_Elec_Loss = 0;

            try
            {
                if (txtElecLoss.Text.Substring(txtElecLoss.Text.Length - 1, 1) == "%")

                    New_Elec_Loss = Convert.ToSingle(txtElecLoss.Text.Substring(0, txtElecLoss.Text.Length - 2)) / 100;
                else
                {
                    New_Elec_Loss = Convert.ToSingle(txtElecLoss.Text);
                    txtElecLoss.Text = Math.Round(New_Elec_Loss, 2) + " %";
                    return;
                }
            }
            catch 
            {
                txtElecLoss.Text = Math.Round(thisInst.otherLosses.Electrical_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no;

            if (New_Elec_Loss > 0.1)
            {
                yes_or_no = MessageBox.Show("Electical loss entered as :" + Math.Round(New_Elec_Loss * 100, 2) + "%. The default value is 2.5%. " +
                                            "Do you want to use the entered value?","", MessageBoxButtons.YesNo);
                if (yes_or_no == DialogResult.Yes)
                    thisInst.otherLosses.Electrical_Loss = New_Elec_Loss;
                else {
                    txtElecLoss.Text = Math.Round(thisInst.otherLosses.Electrical_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_Elec_Loss < 0)
            {
                txtElecLoss.Text = Math.Round(thisInst.otherLosses.Electrical_Loss, 2) + " %";
                return;
            }
            else
                thisInst.otherLosses.Electrical_Loss = New_Elec_Loss;

            double Overall_Elec_Eff = 100 * (1 - thisInst.otherLosses.Electrical_Loss);
            double overallElecLoss = 100 - Overall_Elec_Eff;

            txtElecLossAll.Text = Math.Round(overallElecLoss, 2) + " %";
            txtElecEffAll.Text = Math.Round(Overall_Elec_Eff, 2) + " %";
            Update_Overall_Loss_Eff();
        }


        private void txtIcingLoss_TextChanged(object sender, EventArgs e)
        {
            // Updates Icing loss and updates forms

            double New_Icing_Loss = 0;

            try
            {
                if (txtIcingLoss.Text.Substring(txtIcingLoss.Text.Length - 1, 1) == "%")
                    New_Icing_Loss = Convert.ToSingle(txtIcingLoss.Text.Substring(0, txtIcingLoss.Text.Length - 2)) / 100;
                else
                {
                    New_Icing_Loss = Convert.ToSingle(txtIcingLoss.Text);
                    txtIcingLoss.Text = Math.Round(New_Icing_Loss, 2) + " %";
                    return;
                }
            }
            catch 
            {
                txtIcingLoss.Text = Math.Round(thisInst.otherLosses.Icing_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no;

            if (New_Icing_Loss > 0.1)
            {
                yes_or_no = MessageBox.Show("Icing loss entered as :" + Math.Round(New_Icing_Loss * 100, 2) + "%. " +
                    "The default value is 0%. Do you want to use the entered value?", "", MessageBoxButtons.YesNo);
                if (yes_or_no == DialogResult.Yes)
                    thisInst.otherLosses.Icing_Loss = New_Icing_Loss;
                else {
                    txtIcingLoss.Text = Math.Round(thisInst.otherLosses.Icing_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_Icing_Loss < 0)
            {
                txtIcingLoss.Text = Math.Round(thisInst.otherLosses.Icing_Loss, 2) + " %";
                return;
            }
            else
                thisInst.otherLosses.Icing_Loss = New_Icing_Loss;

            double Overall_Enviro_Eff = 100 * (1 - thisInst.otherLosses.Icing_Loss) * (1 - thisInst.otherLosses.Blade_Soil_Loss) * (1 - thisInst.otherLosses.Blade_Degrade_Loss) * (1 - thisInst.otherLosses.HighLowTemp_Loss);
            double overallEnviroLoss = 100 - Overall_Enviro_Eff;

            txtEnviroLossAll.Text = Math.Round(overallEnviroLoss, 2) + " %";
            txtEnviroEffAll.Text = Math.Round(Overall_Enviro_Eff, 2) + " %";
            Update_Overall_Loss_Eff();
        }

        private void txtBladeDegradeLoss_TextChanged(object sender, EventArgs e)
        {
            //  Updates Blade degradation loss and updates forms

            double New_Blade_Degrade_Loss = 0;

            try
            {
                if (txtBladeDegradeLoss.Text.Substring(txtBladeDegradeLoss.Text.Length - 1, 1) == "%")
                    New_Blade_Degrade_Loss = Convert.ToSingle(txtBladeDegradeLoss.Text.Substring(0, txtBladeDegradeLoss.Text.Length - 2)) / 100;
                else
                {
                    New_Blade_Degrade_Loss = Convert.ToSingle(txtBladeDegradeLoss.Text);
                    txtBladeDegradeLoss.Text = Math.Round(New_Blade_Degrade_Loss, 2) + " %";
                    return;
                }
            }
            catch 
            {
                txtBladeDegradeLoss.Text = Math.Round(thisInst.otherLosses.Blade_Degrade_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no;

            if (New_Blade_Degrade_Loss > 0.1)
            {
                yes_or_no = MessageBox.Show("Blade degradation loss entered as :" + Math.Round(New_Blade_Degrade_Loss * 100, 2) +
                    "%. The default value is 1%. Do you want to use the entered value?", "", MessageBoxButtons.YesNo);

                if (yes_or_no == DialogResult.Yes)
                    thisInst.otherLosses.Blade_Degrade_Loss = New_Blade_Degrade_Loss;
                else {
                    txtBladeDegradeLoss.Text = Math.Round(thisInst.otherLosses.Blade_Degrade_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_Blade_Degrade_Loss < 0)
            {
                txtBladeDegradeLoss.Text = Math.Round(thisInst.otherLosses.Blade_Degrade_Loss, 2) + " %";
                return;
            }
            else
                thisInst.otherLosses.Blade_Degrade_Loss = New_Blade_Degrade_Loss;

            double Overall_Enviro_Eff = 100 * (1 - thisInst.otherLosses.Icing_Loss) * (1 - thisInst.otherLosses.Blade_Soil_Loss) * (1 - thisInst.otherLosses.Blade_Degrade_Loss) * (1 - thisInst.otherLosses.HighLowTemp_Loss);
            double overallEnviroLoss = 100 - Overall_Enviro_Eff;

            txtEnviroLossAll.Text = Math.Round(overallEnviroLoss, 2) + " %";
            txtEnviroEffAll.Text = Math.Round(Overall_Enviro_Eff, 2) + " %";
            Update_Overall_Loss_Eff();
        }


        private void txtBladeSoilLoss_TextChanged(object sender, EventArgs e)
        {
            // Updates Blade soiling loss and updates forms
            //
            double New_Blade_Soil_Loss = 0;

            try
            {
                if (txtBladeSoilLoss.Text.Substring(txtBladeSoilLoss.Text.Length - 1, 1) == "%")
                    New_Blade_Soil_Loss = Convert.ToSingle(txtBladeSoilLoss.Text.Substring(0, txtBladeSoilLoss.Text.Length - 2)) / 100;
                else
                {
                    New_Blade_Soil_Loss = Convert.ToSingle(txtBladeSoilLoss.Text);
                    txtBladeSoilLoss.Text = Math.Round(New_Blade_Soil_Loss, 2) + " %";
                    return;
                }
            }
            catch 
            {
                txtBladeSoilLoss.Text = Math.Round(thisInst.otherLosses.Blade_Soil_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no;

            if (New_Blade_Soil_Loss > 0.1)
            {
                yes_or_no = MessageBox.Show("Blade soiling loss entered as :" + Math.Round(New_Blade_Soil_Loss * 100, 2) + "%. " +
                    "The default value is 1%. Do you want to use the entered value?", "", MessageBoxButtons.YesNo);

                if (yes_or_no == DialogResult.Yes)
                    thisInst.otherLosses.Blade_Soil_Loss = New_Blade_Soil_Loss;
                else {
                    txtBladeSoilLoss.Text = Math.Round(thisInst.otherLosses.Blade_Soil_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_Blade_Soil_Loss < 0)
            {
                txtBladeSoilLoss.Text = Math.Round(thisInst.otherLosses.Blade_Soil_Loss, 2) + " %";
                return;
            }
            else
                thisInst.otherLosses.Blade_Soil_Loss = New_Blade_Soil_Loss;

            double Overall_Enviro_Eff = 100 * (1 - thisInst.otherLosses.Icing_Loss) * (1 - thisInst.otherLosses.Blade_Soil_Loss) * (1 - thisInst.otherLosses.Blade_Degrade_Loss) * (1 - thisInst.otherLosses.HighLowTemp_Loss);
            double overallEnviroLoss = 100 - Overall_Enviro_Eff;

            txtEnviroLossAll.Text = Math.Round(overallEnviroLoss, 2) + " %";
            txtEnviroEffAll.Text = Math.Round(Overall_Enviro_Eff, 2) + " %";
            Update_Overall_Loss_Eff();
        }
        
        private void txtHighLowTempLoss_TextChanged(object sender, EventArgs e)
        {
            // Updates Extreme (Hi/Lo) temperature loss and updates forms
            double New_HighLowTemp_Loss = 0;

            try
            {
                if (txtHighLowTempLoss.Text.Substring(txtHighLowTempLoss.Text.Length - 1, 1) == "%")
                    New_HighLowTemp_Loss = Convert.ToSingle(txtHighLowTempLoss.Text.Substring(0, txtHighLowTempLoss.Text.Length - 2)) / 100;
                else
                {
                    New_HighLowTemp_Loss = Convert.ToSingle(txtHighLowTempLoss.Text);
                    txtHighLowTempLoss.Text = Math.Round(New_HighLowTemp_Loss, 2) + " %";
                    return;
                }
            }
            catch 
            {
                txtHighLowTempLoss.Text = Math.Round(thisInst.otherLosses.HighLowTemp_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no;

            if (New_HighLowTemp_Loss > 0.1)
            {
                yes_or_no = MessageBox.Show("High/Low Temperature shut-down loss entered as :" + Math.Round(New_HighLowTemp_Loss * 100, 2) +
                    "%. The default value is 0%. Do you want to use the entered value?", "", MessageBoxButtons.YesNo);

                if (yes_or_no == DialogResult.Yes)
                    thisInst.otherLosses.HighLowTemp_Loss = New_HighLowTemp_Loss;
                else {
                    txtHighLowTempLoss.Text = Math.Round(thisInst.otherLosses.HighLowTemp_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_HighLowTemp_Loss < 0)
            {
                txtHighLowTempLoss.Text = Math.Round(thisInst.otherLosses.HighLowTemp_Loss, 2) + " %";
                return;
            }
            else
                thisInst.otherLosses.HighLowTemp_Loss = New_HighLowTemp_Loss;

            double Overall_Enviro_Eff = 100 * (1 - thisInst.otherLosses.Icing_Loss) * (1 - thisInst.otherLosses.Blade_Soil_Loss) * (1 - thisInst.otherLosses.Blade_Degrade_Loss) * (1 - thisInst.otherLosses.HighLowTemp_Loss);
            double overallEnviroLoss = 100 - Overall_Enviro_Eff;

            txtEnviroLossAll.Text = Math.Round(overallEnviroLoss, 2) + " %";
            txtEnviroEffAll.Text = Math.Round(Overall_Enviro_Eff, 2) + " %";
            Update_Overall_Loss_Eff();
        }
        
        private void txtPowerCrvLoss_TextChanged(object sender, EventArgs e)
        {
            // Updates power curve loss and updates forms
            // 
            double New_Power_Crv_Loss = 0;

            try
            {
                if (txtPowerCrvLoss.Text.Substring(txtPowerCrvLoss.Text.Length - 1, 1) == "%")
                    New_Power_Crv_Loss = Convert.ToSingle(txtPowerCrvLoss.Text.Substring(0, txtPowerCrvLoss.Text.Length - 2)) / 100;
                else
                {
                    New_Power_Crv_Loss = Convert.ToSingle(txtPowerCrvLoss.Text);
                    txtPowerCrvLoss.Text = Math.Round(New_Power_Crv_Loss, 2) + " %";
                    return;
                }
            }
            catch 
            {
                txtPowerCrvLoss.Text = Math.Round(thisInst.otherLosses.Power_Crv_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no;

            if (New_Power_Crv_Loss > 0.1)
            {
                yes_or_no = MessageBox.Show("power curve loss entered as :" + Math.Round(New_Power_Crv_Loss * 100, 2) +
                    "%. The default value is 1.5%. Do you want to use the entered value?", "", MessageBoxButtons.YesNo);
                if (yes_or_no == DialogResult.Yes)
                    thisInst.otherLosses.Power_Crv_Loss = New_Power_Crv_Loss;
                else {
                    txtPowerCrvLoss.Text = Math.Round(thisInst.otherLosses.Power_Crv_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_Power_Crv_Loss < 0)
            {
                txtPowerCrvLoss.Text = Math.Round(thisInst.otherLosses.Power_Crv_Loss, 2) + " %";
                return;
            }
            else
                thisInst.otherLosses.Power_Crv_Loss = New_Power_Crv_Loss;

            double Overall_Turb_Perf_Eff = 100 * (1 - thisInst.otherLosses.Power_Crv_Loss) * (1 - thisInst.otherLosses.Turbulence_Loss);
            double overallTurbPerfLoss = 100 - Overall_Turb_Perf_Eff;

            txtTurbPerfLossAll.Text = Math.Round(overallTurbPerfLoss, 2) + " %";
            txtTurbPerfEffAll.Text = Math.Round(Overall_Turb_Perf_Eff, 2) + " %";
            Update_Overall_Loss_Eff();
        }
        
        private void txtTurbulenceLoss_TextChanged(object sender, EventArgs e)
        {
            // Updates turbulence loss and updates forms
            //
            double New_Turbulence_Loss = 0;

            try
            {
                if (txtTurbulenceLoss.Text.Substring(txtTurbulenceLoss.Text.Length - 1, 1) == "%")
                    New_Turbulence_Loss = Convert.ToSingle(txtTurbulenceLoss.Text.Substring(0, txtTurbulenceLoss.Text.Length - 2)) / 100;
                else
                {
                    New_Turbulence_Loss = Convert.ToSingle(txtTurbulenceLoss.Text);
                    txtTurbulenceLoss.Text = Convert.ToSingle(Math.Round(New_Turbulence_Loss, 2)) + " %";
                    return;
                }
            }
            catch 
            {
                txtTurbulenceLoss.Text = Math.Round(thisInst.otherLosses.Turbulence_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no;

            if (New_Turbulence_Loss > 0.1)
            {
                yes_or_no = MessageBox.Show("Turbuence loss entered as :" + Math.Round(New_Turbulence_Loss * 100, 2) +
                    "%. The default value is 0.5%. Do you want to use the entered value?", "", MessageBoxButtons.YesNo);
                if (yes_or_no == DialogResult.Yes)
                    thisInst.otherLosses.Turbulence_Loss = New_Turbulence_Loss;
                else {
                    txtTurbulenceLoss.Text = Math.Round(thisInst.otherLosses.Turbulence_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_Turbulence_Loss < 0)
            {
                txtTurbulenceLoss.Text = Math.Round(thisInst.otherLosses.Turbulence_Loss, 2) + " %";
                return;
            }
            else
                thisInst.otherLosses.Turbulence_Loss = New_Turbulence_Loss;

            double Overall_Turb_Perf_Eff = 100 * (1 - thisInst.otherLosses.Power_Crv_Loss) * (1 - thisInst.otherLosses.Turbulence_Loss);
            double overallTurbPerfLoss = 100 - Overall_Turb_Perf_Eff;

            txtTurbPerfLossAll.Text = Math.Round(overallTurbPerfLoss, 2) + " %";
            txtTurbPerfEffAll.Text = Math.Round(Overall_Turb_Perf_Eff, 2) + " %";
            Update_Overall_Loss_Eff();
        }

        private void txtGrid_Curt_TextChanged(object sender, EventArgs e)
        {
            // Updates grid curtailment loss and updates forms
            double New_Grid_Curtail_Loss = 0;

            try { 
                if (txtGrid_Curt.Text.Substring(txtGrid_Curt.Text.Length - 1, 1) == "%")
                New_Grid_Curtail_Loss = Convert.ToSingle(txtGrid_Curt.Text.Substring(0, txtGrid_Curt.Text.Length - 2)) / 100;
            else
            {
                New_Grid_Curtail_Loss = Convert.ToSingle(txtGrid_Curt.Text);
                txtGrid_Curt.Text = Math.Round(New_Grid_Curtail_Loss, 2) + " %";
                return;
            }
            }
            catch 
            {
                txtGrid_Curt.Text = Math.Round(thisInst.otherLosses.Grid_Curtail_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no;

            if (New_Grid_Curtail_Loss > 0.1)
            {
                yes_or_no = MessageBox.Show("Grid curtail loss entered as :" + Math.Round(New_Grid_Curtail_Loss * 100, 2) +
                    "%. The default value is 0%. Do you want to use the entered value?", "", MessageBoxButtons.YesNo);
                if (yes_or_no == DialogResult.Yes)
                    thisInst.otherLosses.Grid_Curtail_Loss = New_Grid_Curtail_Loss;
                else {
                    txtGrid_Curt.Text = Math.Round(thisInst.otherLosses.Grid_Curtail_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_Grid_Curtail_Loss < 0)
            {
                txtGrid_Curt.Text = Math.Round(thisInst.otherLosses.Grid_Curtail_Loss, 2) + " %";
                return;
            }
            else
                thisInst.otherLosses.Grid_Curtail_Loss = New_Grid_Curtail_Loss;

            double Overall_Curtail_Eff = 100 * (1 - thisInst.otherLosses.Grid_Curtail_Loss) * (1 - thisInst.otherLosses.Environ_Curtail_Loss) * (1 - thisInst.otherLosses.Wake_Sect_Curtail_Loss);
            double overallCurtailLoss = 100 - Overall_Curtail_Eff;

            txtCurtLossAll.Text = Math.Round(overallCurtailLoss, 2) + " %";
            txtCurtEffAll.Text = Math.Round(Overall_Curtail_Eff, 2) + " %";
            Update_Overall_Loss_Eff();
        }
        
        private void txtEnviro_Curt_TextChanged(object sender, EventArgs e)
        {
            // Updates environmental curtailment loss and updates forms
            double New_Environ_Curtail_Loss = 0;

            try
            {
                if (txtEnviro_Curt.Text.Substring(txtEnviro_Curt.Text.Length - 1, 1) == "%")
                    New_Environ_Curtail_Loss = Convert.ToSingle(txtEnviro_Curt.Text.Substring(0, txtEnviro_Curt.Text.Length - 2)) / 100;
                else
                {
                    New_Environ_Curtail_Loss = Convert.ToSingle(txtEnviro_Curt.Text);
                    txtEnviro_Curt.Text = Math.Round(New_Environ_Curtail_Loss, 2) + " %";
                    return;
                }
            }
            catch 
            {
                txtEnviro_Curt.Text = Math.Round(thisInst.otherLosses.Environ_Curtail_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no;

            if (New_Environ_Curtail_Loss > 0.1)
            {
                yes_or_no = MessageBox.Show("Grid curtail loss entered as :" + Math.Round(New_Environ_Curtail_Loss * 100, 2) +
                    "%. The default value is 0%. Do you want to use the entered value?", "", MessageBoxButtons.YesNo);

                if (yes_or_no == DialogResult.Yes)
                    thisInst.otherLosses.Environ_Curtail_Loss = New_Environ_Curtail_Loss;
                else {
                    txtEnviro_Curt.Text = Math.Round(thisInst.otherLosses.Environ_Curtail_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_Environ_Curtail_Loss < 0)
            {
                txtEnviro_Curt.Text = Math.Round(thisInst.otherLosses.Environ_Curtail_Loss, 2) + " %";
                return;
            }
            else
                thisInst.otherLosses.Environ_Curtail_Loss = New_Environ_Curtail_Loss;

            double Overall_Curtail_Eff = 100 * (1 - thisInst.otherLosses.Grid_Curtail_Loss) * (1 - thisInst.otherLosses.Environ_Curtail_Loss) * (1 - thisInst.otherLosses.Wake_Sect_Curtail_Loss);
            double overallCurtailLoss = 100 - Overall_Curtail_Eff;

            txtCurtLossAll.Text = Math.Round(overallCurtailLoss, 2) + " %";
            txtCurtEffAll.Text = Math.Round(Overall_Curtail_Eff, 2) + " %";
            Update_Overall_Loss_Eff();
        }
        
        private void txtWakeSM_Loss_TextChanged(object sender, EventArgs e)
        {
            // Updates wake sector management curtailment loss and updates forms
            double New_WakeSM_Curtail_Loss = 0;

            try
            {
                if (txtWakeSM_Loss.Text.Substring(txtWakeSM_Loss.Text.Length - 1, 1) == "%")
                    New_WakeSM_Curtail_Loss = Convert.ToSingle(txtWakeSM_Loss.Text.Substring(0, txtWakeSM_Loss.Text.Length - 2)) / 100;
                else
                {
                    New_WakeSM_Curtail_Loss = Convert.ToSingle(txtWakeSM_Loss.Text);
                    txtWakeSM_Loss.Text = Math.Round(New_WakeSM_Curtail_Loss, 2) + " %";
                    return;
                }
            }
            catch 
            {
                txtWakeSM_Loss.Text = Math.Round(thisInst.otherLosses.Wake_Sect_Curtail_Loss, 2) + " %";
                return;
            }

            DialogResult yes_or_no;

            if (New_WakeSM_Curtail_Loss > 0.1)
            {
                yes_or_no = MessageBox.Show("Grid curtail loss entered as :" + Math.Round(New_WakeSM_Curtail_Loss * 100, 2) +
                    "%. The default value is 0%. Do you want to use the entered value?", "", MessageBoxButtons.YesNo);

                if (yes_or_no == DialogResult.Yes)
                    thisInst.otherLosses.Wake_Sect_Curtail_Loss = New_WakeSM_Curtail_Loss;
                else {
                    txtWakeSM_Loss.Text = Math.Round(thisInst.otherLosses.Wake_Sect_Curtail_Loss, 2) + " %";
                    return;
                }
            }
            else if (New_WakeSM_Curtail_Loss < 0)
            {
                txtWakeSM_Loss.Text = Math.Round(thisInst.otherLosses.Wake_Sect_Curtail_Loss, 2) + " %";
                return;
            }
            else
                thisInst.otherLosses.Wake_Sect_Curtail_Loss = New_WakeSM_Curtail_Loss;

            double Overall_Curtail_Eff = 100 * (1 - thisInst.otherLosses.Grid_Curtail_Loss) * (1 - thisInst.otherLosses.Environ_Curtail_Loss) * (1 - thisInst.otherLosses.Wake_Sect_Curtail_Loss);
            double overallCurtailLoss = 100 - Overall_Curtail_Eff;

            txtCurtLossAll.Text = Math.Round(overallCurtailLoss, 2) + " %";
            txtCurtEffAll.Text = Math.Round(Overall_Curtail_Eff, 2) + " %";
            Update_Overall_Loss_Eff();
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Updates turbine net energy estimates with updated losses and updates Net Energy tab table and textboxes
            Update updateThe = new Update();            
            double Total_Other_Loss = thisInst.otherLosses.Get_Total_Losses();
            thisInst.turbineList.ReCalcNetEnergy(Previous_Other_Losses, Total_Other_Loss);
            updateThe.NetTurbineEstsTAB(thisInst);
            Close();
        }


        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }

        public void Update_Overall_Loss_Eff()
        {

            // Calculates the overall loss and efficiency factor and updates the textboxes

            double Overall_Wake_Eff = 0;

            try
            {
                Overall_Wake_Eff = Convert.ToSingle(txtWakeEffAll.Text.Substring(0, txtWakeEffAll.Text.Length - 2)) / 100;
            }
            catch 
            {
                Overall_Wake_Eff = 1;
            }

            double Overall_Avail_Eff = 0;
            try {
                Overall_Avail_Eff = Convert.ToSingle(txtAvailEff.Text.Substring(0, txtAvailEff.Text.Length - 2)) / 100;
            }
            catch 
            {
                Overall_Avail_Eff = 1;
            }

            double Overall_Elec_Eff = 0;
            try {
                Overall_Elec_Eff = Convert.ToSingle(txtElecEffAll.Text.Substring(0, txtElecEffAll.Text.Length - 2)) / 100;
            }
            catch 
            {
                Overall_Elec_Eff = 1;
            }

            double Overall_Enviro_Eff = 0;
            try {
                Overall_Enviro_Eff = Convert.ToSingle(txtEnviroEffAll.Text.Substring(0, txtEnviroEffAll.Text.Length - 2)) / 100;
            }
            catch
            {
                Overall_Enviro_Eff = 1;
            }

            double Overall_Turb_Perf_Eff = 0;
            try {
                Overall_Turb_Perf_Eff = Convert.ToSingle(txtTurbPerfEffAll.Text.Substring(0, txtTurbPerfEffAll.Text.Length - 2)) / 100;
            }
            catch
            {
                Overall_Turb_Perf_Eff = 1;
            }

            double Overall_Curtail_Eff = 0;
            try { 
                Overall_Curtail_Eff = Convert.ToSingle(txtCurtEffAll.Text.Substring(0, txtCurtEffAll.Text.Length - 2)) / 100;
            }
            catch 
            {
                Overall_Curtail_Eff = 1;
            }

            double Total_Eff = 100 * (Overall_Wake_Eff * Overall_Avail_Eff * Overall_Elec_Eff * Overall_Enviro_Eff * Overall_Turb_Perf_Eff * Overall_Curtail_Eff);
            double totalLoss = (double)Math.Round(100 - Total_Eff, 4);

            txtOverallLoss.Text = Math.Round(totalLoss, 2) + " %";
            txtOverallEff.Text = Math.Round(Total_Eff, 2) + " %";
        }
                
    }

}
