using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary>
    /// Class that adds an exceedance curve (i.e. performance factor) curve
    /// </summary>
    public partial class AddPFCurve : Form
    {
        /// <summary> True if exceedance curve is successfully defined </summary>
        public bool goodToGo = false;

        /// <summary> Class initializer </summary>
        public AddPFCurve()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            goodToGo = true;
            Close();
        }

        /// <summary> Updates dropdown menu to show curves that have yet to be defined </summary>        
        public void UpdateDropdown(Exceedance.ExceedanceCurve[] exceedanceCurves)
        {            
            cboCurves.Items.Clear();
            string exceedStr = "";

            for (int i = 0; i < 21; i++)
            {
                if (i == 0)
                    exceedStr = "Annual Wind Variability";
                else if (i == 1)
                    exceedStr = "Balance of Plant";
                else if (i == 2)
                    exceedStr = "Catastrophic Event";
                else if (i == 3)
                    exceedStr = "Data Measurement";
                else if (i == 4)
                    exceedStr = "Electrical";
                else if (i == 5)
                    exceedStr = "Extreme Temperatures";
                else if (i == 6)
                    exceedStr = "Extreme Wind";
                else if (i == 7)
                    exceedStr = "Grid";
                else if (i == 8)
                    exceedStr = "Icing";
                else if (i == 9)
                    exceedStr = "Legal/Curtailment";
                else if (i == 10)
                    exceedStr = "MCP Correlation";
                else if (i == 11)
                    exceedStr = "Parasitic";
                else if (i == 12)
                    exceedStr = "Power Curve Degradation";
                else if (i == 13)
                    exceedStr = "Power Curve";
                else if (i == 14)
                    exceedStr = "Shear Extrapolation";
                else if (i == 15)
                    exceedStr = "Short-Term MCP";
                else if (i == 16)
                    exceedStr = "Turbine Availability - OEM";
                else if (i == 17)
                    exceedStr = "Turbine Availability - Owner/Operator";
                else if (i == 18)
                    exceedStr = "Wake Loss Model";
                else if (i == 19)
                    exceedStr = "Wind Flow Model";
                else if (i == 20)
                    exceedStr = "Wind Rose Sensitivity";

                bool isDefined = CurveIsDefined(exceedanceCurves, exceedStr);

                if (isDefined == false)
                    cboCurves.Items.Add(exceedStr);
            }

            if (cboCurves.Items.Count > 0)
                cboCurves.SelectedIndex = 0;

        }

        /// <summary> Checks list of curves to see if exceedance curve is defined </summary>        
        /// <returns> True if specified exceedance curve is defined </returns>
        public bool CurveIsDefined(Exceedance.ExceedanceCurve[] exceedanceCurves, string exceedStr)
        {            
            bool isDefined = false;

            if (exceedanceCurves == null)
                return isDefined;

            for (int i = 0; i < exceedanceCurves.Length; i++)
            {
                if (exceedanceCurves[i].exceedStr == exceedStr)
                    isDefined = true;
            }
            
            return isDefined;
        }
    }
}
