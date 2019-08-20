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
    public partial class AddPFCurve : Form
    {
        public bool goodToGo = false;

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

        public void UpdateDropdown(Exceedance.ExceedanceCurve[] exceedanceCurves)
        {
            // Updates dropdown menu to only show curves that have yet to be defined
            cboCurves.Items.Clear();
            string exceedStr = "";

            for (int i = 0; i < 20; i++)
            {
                if (i == 0)
                    exceedStr = "Turbine Availability - Owner/Operator";
                else if (i == 1)
                    exceedStr = "Turbine Availability - OEM";
                else if (i == 2)
                    exceedStr = "Power Curve";
                else if (i == 3)
                    exceedStr = "Power Curve Degradation";
                else if (i == 4)
                    exceedStr = "Electrical";
                else if (i == 5)
                    exceedStr = "Wind Rose Sensitivity";
                else if (i == 6)
                    exceedStr = "Data Measurement";
                else if (i == 7)
                    exceedStr = "Balance of Plant";
                else if (i == 8)
                    exceedStr = "Annual Wind Variability";
                else if (i == 9)
                    exceedStr = "Extreme Wind";
                else if (i == 10)
                    exceedStr = "Extreme Temperatures";
                else if (i == 11)
                    exceedStr = "Icing";
                else if (i == 12)
                    exceedStr = "Legal/Curtailment";
                else if (i == 13)
                    exceedStr = "Grid";
                else if (i == 14)
                    exceedStr = "Catastrophic Event";
                else if (i == 15)
                    exceedStr = "Short-Term MCP";
                else if (i == 16)
                    exceedStr = "MCP Correlation";
                else if (i == 17)
                    exceedStr = "Shear Extrapolation";
                else if (i == 18)
                    exceedStr = "Wind Flow Model";
                else if (i == 19)
                    exceedStr = "Wake Loss Model";

                bool isDefined = CurveIsDefined(exceedanceCurves, exceedStr);

                if (isDefined == false)
                    cboCurves.Items.Add(exceedStr);
            }

            if (cboCurves.Items.Count > 0)
                cboCurves.SelectedIndex = 0;

        }

        public bool CurveIsDefined(Exceedance.ExceedanceCurve[] exceedanceCurves, string exceedStr)
        {
            // Returns true if exceedance curve is defined
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
