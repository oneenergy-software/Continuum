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
    public partial class EditShearCalcSettings : Form
    {
        Met thisMet;
        public bool goodToGo;


        public EditShearCalcSettings(Met thisMetToEdit)
        {
            InitializeComponent();
            thisMet = thisMetToEdit;
            UpdateForm();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // Get shear settings and set goodToGo to true

            // Check that max height is great than min if best-fit selected
            if (cboShearCalcTypes.SelectedItem.ToString() == thisMet.metData.GetShearCalcNameFromEnum(Met_Data_Filter.ShearCalculationTypes.bestFit))
            {
                double selMinHeight = Convert.ToDouble(cboMinHeight.SelectedItem.ToString());
                double selMaxHeight = Convert.ToDouble(cboMaxHeight.SelectedItem.ToString());

                if (selMaxHeight <= selMinHeight)
                {
                    MessageBox.Show("When using the best-fit shear calculation, the minimum height must be below maximum height.");
                    return;
                }
            }

            int numH = thisMet.metData.GetHeightsOfAnems().Length;

            if (numH <= 2 && cboShearCalcTypes.SelectedItem.ToString() == "Best-Fit Alpha")
            {
                MessageBox.Show("A minimum of 3 wind speed measurement heights are needed to use the best-fit shear calculation");
                goodToGo = false;
                return;
            }

            goodToGo = true;
            Close();
        }

        public void UpdateForm()
        {
            txtMetSite.Text = thisMet.name;

            cboShearCalcTypes.SelectedItem = thisMet.metData.GetShearCalcNameFromEnum(thisMet.metData.shearSettings.shearCalcType);

            double[] wsHeights = thisMet.metData.GetHeightsOfAnems();

            // Populate min valid heights dropdown
            cboMinValidHeights.Items.Clear();

            for (int i = 3; i <= wsHeights.Length; i++)
                cboMinValidHeights.Items.Add(i.ToString());

            if (thisMet.metData.shearSettings.minNumHs == 0)
                cboMinValidHeights.SelectedIndex = 0; // Min of 3 heights by default
            else
                cboMinValidHeights.SelectedItem = thisMet.metData.shearSettings.minNumHs.ToString();

            // Populate min/max height dropdowns
            cboMinHeight.Items.Clear();
            cboMaxHeight.Items.Clear();
                      
            for (int h = 0; h < wsHeights.Length; h++)
            {
                cboMinHeight.Items.Add(wsHeights[h]);
                cboMaxHeight.Items.Add(wsHeights[h]);
            }

            cboMinHeight.SelectedItem = thisMet.metData.shearSettings.minHeight;
            cboMaxHeight.SelectedItem = thisMet.metData.shearSettings.maxHeight;

            if (thisMet.metData.shearSettings.shearCalcType == Met_Data_Filter.ShearCalculationTypes.bestFit)
            {   
                cboMinHeight.Enabled = true;
                cboMaxHeight.Enabled = true;
            }
            else if (thisMet.metData.shearSettings.shearCalcType == Met_Data_Filter.ShearCalculationTypes.avgAllPairs)
            {              

                cboMinHeight.Enabled = false;
                cboMaxHeight.Enabled = false;
            }
        }

        private void cboShearCalcTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Different shear calculation selected
            if (thisMet.metData.shearSettings.shearCalcType == Met_Data_Filter.ShearCalculationTypes.bestFit && cboShearCalcTypes.SelectedItem.ToString() == thisMet.metData.GetShearCalcNameFromEnum(Met_Data_Filter.ShearCalculationTypes.bestFit))
            {
                // Best-fit shear calculation selected (same as setting currently set so set specified min/max height)
                cboMinHeight.SelectedItem = thisMet.metData.shearSettings.minHeight.ToString();
                cboMaxHeight.SelectedItem = thisMet.metData.shearSettings.maxHeight.ToString();
            }
            else if (cboShearCalcTypes.SelectedItem.ToString() == thisMet.metData.GetShearCalcNameFromEnum(Met_Data_Filter.ShearCalculationTypes.bestFit))
            {
                cboMinHeight.Enabled = true;
                cboMaxHeight.Enabled = true;
                cboMinHeight.Enabled = true;
            }
            else if (cboShearCalcTypes.SelectedItem.ToString() == thisMet.metData.GetShearCalcNameFromEnum(Met_Data_Filter.ShearCalculationTypes.avgAllPairs))
            {
                cboMinHeight.Enabled = false;
                cboMaxHeight.Enabled = false;
                cboMinHeight.Enabled = false;   
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            goodToGo = false;

            Close();
        }
    }
}
