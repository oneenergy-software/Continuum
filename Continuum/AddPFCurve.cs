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
            ContinuumStyle.ApplyStyles(this);            
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
        public void UpdateDropdownContinuumDefault(Exceedance.ExceedanceCurve[] exceedanceCurves)
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

        /// <summary> Updates dropdown menu to show curves that have yet to be defined </summary>        
        public void UpdateDropdownIECDefault(Exceedance.ExceedanceCurve[] exceedanceCurves)
        {
            cboCurves.Items.Clear();
            string exceedStr = "";

            for (int i = 0; i < 30; i++)
            {
                if (i == 0)
                    exceedStr = "Historical Wind - Rep. of LT";
                else if (i == 1)
                    exceedStr = "Historical Wind - Ref. Data Consistency";
                else if (i == 2)
                    exceedStr = "Reference Data - Measurements";
                else if (i == 3)
                    exceedStr = "Reference Data - LT Adjust (MCP)";
                else if (i == 4)
                    exceedStr = "Reference Data - WS Dist. Uncertainty";
                else if (i == 5)
                    exceedStr = "Project Variability - WS Variability (IAV)";
                else if (i == 6)
                    exceedStr = "Project Variability - Climate Change";
                else if (i == 7)
                    exceedStr = "Measurement Uncertainty - WS";
                else if (i == 8)
                    exceedStr = "Horizontal Extrapolation";
                else if (i == 9)
                    exceedStr = "Vertical Extrapolation";
                else if (i == 10)
                    exceedStr = "Turbine Interaction - Internal";
                else if (i == 11)
                    exceedStr = "Turbine Interaction - External";
                else if (i == 12)
                    exceedStr = "Turbine Interaction - Future";
                else if (i == 13)
                    exceedStr = "Availability - Turbine";
                else if (i == 14)
                    exceedStr = "Availability - BOP";
                else if (i == 15)
                    exceedStr = "Availability - Grid";
                else if (i == 16)
                    exceedStr = "Electrical - Efficiency";
                else if (i == 17)
                    exceedStr = "Electrical - Parasitic";
                else if (i == 18)
                    exceedStr = "Turbine Performance - Sub-Optimal";
                else if (i == 19)
                    exceedStr = "Turbine Performance - Generic Adj.";
                else if (i == 20)
                    exceedStr = "Turbine Performance - Site-spec. Adj.";
                else if (i == 21)
                    exceedStr = "Turbine Performance - High WS Hysteresis";
                else if (i == 22)
                    exceedStr = "Environmental - Icing";
                else if (i == 23)
                    exceedStr = "Environmental - Degradation";
                else if (i == 24)
                    exceedStr = "Environmental - Ext. Conditions";
                else if (i == 25)
                    exceedStr = "Environmental - Exposure Changes";
                else if (i == 26)
                    exceedStr = "Curtailment - Load";
                else if (i == 27)
                    exceedStr = "Curtailment - Grid";
                else if (i == 28)
                    exceedStr = "Curtailment - Environmental";
                else if (i == 29)
                    exceedStr = "Operational Strategies";

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
