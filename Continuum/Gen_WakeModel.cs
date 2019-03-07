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
    public partial class Gen_WakeModel : Form
    {
        public Gen_WakeModel(Continuum continuum)
        {
            InitializeComponent();
            thisInst = continuum;

            if (thisInst.topo.useSepMod)
            {
                txt_FlowSep_Used.Text = "Flow Sep. model used";
                txt_FlowSep_Used.BackColor = Color.LightBlue;
            }
            else
            {
                txt_FlowSep_Used.Text = "Flow Sep. model NOT used";
                txt_FlowSep_Used.BackColor = Color.LightCoral;
            }

            if (thisInst.topo.useSR)
            {
                txtLC_Used.Text = "roughness model used";
                txtLC_Used.BackColor = Color.MediumSeaGreen;
            }
            else
            {
                txtLC_Used.Text = "roughness model NOT used";
                txtLC_Used.BackColor = Color.LightCoral;
            }

            cboUWDW.Items.Clear();
            cboUWDW.Text = "";
                                                
            cboUWDW.Items.Add("Default Model");                
            
            if (thisInst.modelList.ModelCount > 1)
            {
                if (thisInst.modelList.models[1,0].isImported == false) 
                    cboUWDW.Items.Add("Site-Calibated Model"); 
                else                                      
                    cboUWDW.Items.Add("Imported Model");  
            }

            if (cboUWDW.Items.Count > 1)                            
                cboUWDW.SelectedIndex = 1;               
            else if (cboUWDW.Items.Count > 0)                           
                cboUWDW.SelectedIndex = 0;

            cboPowerCrvs.Items.Clear();

            if (thisInst.turbineList.PowerCurveCount > 0)
            {
                for (int i = 0; i < thisInst.turbineList.PowerCurveCount; i++)                   
                    cboPowerCrvs.Items.Add(thisInst.turbineList.powerCurves[i].name);                
            }
                        
            if (cboPowerCrvs.Items.Count > 0) cboPowerCrvs.SelectedIndex = 0;

        }

        public Continuum thisInst;

        private void btnGenMap_Click(object sender, EventArgs e)
        {
            // Reads wake loss model settings from form and adds wake model to list and calls background_worker to conduct turbine calcs                     
            
            bool isCalibrated = GetIsCalibrated();
            int wakeModelType = ReadWakeModelType();
            double horizExp = ReadHorizExp();
            TurbineCollection.PowerCurve thisPowerCurve = GetPowerCurve();
            double WRR = ReadWRR();
            double WRR_Exp = ReadWRR_Exp();
            double avgTI = ReadTI();                        

            string wakeCombo = ReadWakeCombo();

            double DW_Spacing = 0;
            double CW_Spacing = 0;
            double ambRough = 0;
                        
            if (wakeModelType == 1) { // read in inputs for DAWM
                DW_Spacing = ReadDW_Spacing();
                CW_Spacing = ReadCW_Spacing();
                ambRough = ReadAmbRough();
            }

            // Check to see if wake model has been added        
            bool wakeModelExists = thisInst.wakeModelList.WakeModelExists(wakeModelType, horizExp, avgTI, thisPowerCurve.name, DW_Spacing, CW_Spacing, ambRough, wakeCombo, WRR, WRR_Exp);

            if (wakeModelExists == false)
                thisInst.wakeModelList.AddWakeModel(wakeModelType, horizExp, avgTI, thisPowerCurve, DW_Spacing, CW_Spacing, ambRough, wakeCombo, WRR, WRR_Exp);

            int wakeModelInd = 0;

            for (int i = 0; i < thisInst.wakeModelList.NumWakeModels; i++) {
                if (thisInst.wakeModelList.wakeModels[i].wakeModelType == wakeModelType && thisInst.wakeModelList.wakeModels[i].horizWakeExp == horizExp &&
                    thisInst.wakeModelList.wakeModels[i].powerCurve.name == thisPowerCurve.name && thisInst.wakeModelList.wakeModels[i].ambTI == avgTI &&
                    thisInst.wakeModelList.wakeModels[i].comboMethod == wakeCombo && thisInst.wakeModelList.wakeModels[i].wakeRechargeRate == WRR &&
                    thisInst.wakeModelList.wakeModels[i].wakeRechargeExp == WRR_Exp) {
                    if (wakeModelType == 1) {
                        if (thisInst.wakeModelList.wakeModels[i].DW_Spacing == DW_Spacing && thisInst.wakeModelList.wakeModels[i].CW_Spacing == CW_Spacing &&
                            thisInst.wakeModelList.wakeModels[i].ambRough == ambRough) {
                            wakeModelInd = i;
                            break;
                        }
                    }
                    else {
                        wakeModelInd = i;
                        break;
                    }

                }
            }

            if (thisInst.turbineList.TurbineCount > 0) {
                if (thisInst.turbineList.turbineEsts[0].EstsExistForWakeModel(thisInst.wakeModelList.wakeModels[wakeModelInd], isCalibrated, thisInst.wakeModelList, thisInst.topo.useSR,
                    thisInst.topo.useSepMod)) {
                    MessageBox.Show("Wake losses have already been calculated with this model and settings.", "Continuum 2.3");
                    return;
                }
            }
            else {
                Close();
                return;
            }

            if (thisInst.turbineList.turbineEsts[0].EstsExistForWakeModel(thisInst.wakeModelList.wakeModels[wakeModelInd], isCalibrated, thisInst.wakeModelList,
                thisInst.topo.useSR, thisInst.topo.useSepMod) == false) {

                BackgroundWork.Vars_for_Turbine_and_Node_Calcs argsForBW = new BackgroundWork.Vars_for_Turbine_and_Node_Calcs();

                if (thisInst.metList.ThisCount > 0) {
                    argsForBW.thisInst = thisInst;
                    argsForBW.isCalibrated = isCalibrated;
                    argsForBW.thisWakeModel = thisInst.wakeModelList.wakeModels[wakeModelInd];

                    // Call background worker to run calculations
                    thisInst.BW_worker = new BackgroundWork();
                    thisInst.BW_worker.Call_BW_TurbCalcs(argsForBW);
                    thisInst.ChangesMade();
                }
            }
            else {
                // Update Net turb tab
                Update updateThe = new Update();                
                updateThe.NetTurbineEstsTAB(thisInst);
            }

            Close();
        }

        public bool GetIsCalibrated()
        {
            bool isCalibrated = false;

            int modelInd = cboUWDW.SelectedIndex;
            if (modelInd == 1)
                isCalibrated = true;

            return isCalibrated;
        }

        public TurbineCollection.PowerCurve GetPowerCurve()
        {
            // Reads selected turbine power curve from form and gets power curve object 
            TurbineCollection.PowerCurve thisPowerCurve = new TurbineCollection.PowerCurve();
            string powerCurve = "";

            try {
                if (cboPowerCrvs.SelectedItem.ToString() == "No power Curves Imported") {
                    MessageBox.Show("Import a power curve to calculate wake losses. On Gross Energy Estimates tab, click Import power Curve", "Continuum 2.3");
                    return thisPowerCurve;
                }
                powerCurve = cboPowerCrvs.SelectedItem.ToString();
            }
            catch {
                return thisPowerCurve;
            }

            for (int i = 0; i < thisInst.turbineList.PowerCurveCount; i++) {
                if (thisInst.turbineList.powerCurves[i].name == powerCurve) {
                    thisPowerCurve = thisInst.turbineList.powerCurves[i];
                }
            }

            return thisPowerCurve;
        }

        public double ReadTI()
        {
            // Reads and returns ambient TI
            double thisTI = 10;

            try {
                thisTI = Convert.ToSingle(txtAmbTI.Text);
            }
            catch {
                thisTI = 10;
                txtAmbTI.Text = "10";
            }

            return thisTI;
        }

        public string ReadWakeCombo()
        {
            // Reads and returns selected wake combination method
            string thisCombo = "";

            try {
                thisCombo = cboWakeCombo.SelectedItem.ToString();
            }
            catch {
                cboWakeCombo.SelectedIndex = 0;
                thisCombo = cboWakeCombo.SelectedItem.ToString();
            }

            return thisCombo;
        }

        public double ReadHorizExp()
        {
            // Reads and returns wake horizontal expansion angle
            double horizExp = 5;

            try {
                horizExp = Convert.ToSingle(txtHorizWakeExp.Text);
            }
            catch {
                horizExp = 5;
                txtHorizWakeExp.Text = "5";
            }

            return horizExp;
        }

        public double ReadWRR()
        {
            // Reads and returns wake recharge rate
            double WRR = 0;

            try {
                WRR = Convert.ToSingle(numWRR.Value);
            }
            catch {
                WRR = 0;
            }

            return WRR;
        }

        public double ReadWRR_Exp()
        { 
            // Reads and returns wake recharge distance weight exponent
            double WRR_Exp = 0;

            try {
                WRR_Exp = Convert.ToSingle(numWakeExp.Value);
            }
            catch {
                WRR_Exp = 0;
            }

            return WRR_Exp;
        }

        public double ReadDW_Spacing()
        { 
            // Reads and returns the downwind spacing
            double DW_Spacing = 10;

            try {
                DW_Spacing = Convert.ToSingle(txtDownSpace.Text);
            }
            catch  {
                DW_Spacing = 10;
                txtDownSpace.Text = "10";
            }

            return DW_Spacing;
        }

        public double ReadCW_Spacing()
        {
            // Reads and returns the crosswind spacing
            double CW_Spacing = 3.5f;

            try {
                CW_Spacing = Convert.ToSingle(txtCrossSpace.Text);
            }
            catch {
                CW_Spacing = 3.5f;
                txtCrossSpace.Text = "3.5";
            }

            return CW_Spacing;
        }

        public double ReadAmbRough()
        { 
            // Reads and returns ambient surface roughness
            double ambRough = 0.03f;

            try {
                ambRough = Convert.ToSingle(txtAmbRough.Text);
            }
            catch {
                ambRough = 0.03f;
                txtAmbRough.Text = "0.03";
            }

            return ambRough;
        }

        public int ReadWakeModelType()
        {
            // Reads and returns the selected wake model
            int modelType = 0;
            string wakeModel = "";

            try {
                wakeModel = cboWakeModel.SelectedItem.ToString();
                if (wakeModel == "Eddy Viscosity Wake Model")
                    modelType = 0;
                else if (wakeModel == "Eddy Viscosity (Deep Array Wind Model)")
                    modelType = 1;
                else if (wakeModel == "Continuum Wake Model")
                    modelType = 2;
            }
            catch {
                return modelType;
            }

            return modelType;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboWakeModel_SelectedIndexChanged(object sender, EventArgs e)
        { 
        // Update downwind and crosswind spacing and ambient roughness textboxes depending on whether DAWM model is selected or not
            if (Created) {
                if (cboWakeModel.SelectedIndex != 1)
                { // DAWM
                    txtCrossSpace.Clear();
                    txtDownSpace.Clear();
                    txtAmbRough.Clear();
                    txtCrossSpace.Enabled = false;
                    txtDownSpace.Enabled = false;
                    txtAmbRough.Enabled = false;
                }
                else
                {
                    txtCrossSpace.Text = "3.5";
                    txtDownSpace.Text = "11";
                    txtAmbRough.Text = "0.03";
                    txtCrossSpace.Enabled = true;
                    txtDownSpace.Enabled = true;
                    txtAmbRough.Enabled = true;
                }
            }

        }

        
    }
}
