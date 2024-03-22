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
    public partial class FilterFlagLegend : Form
    {       

        /// <summary> Shows met data filter flag color legend </summary>
        public FilterFlagLegend(Continuum thisInst)
        {
            InitializeComponent();
            txtOutsideRange.BackColor = thisInst.updateThe.GetFilterFlagColor(Met_Data_Filter.Filter_Flags.outsideRange);
            txtMissing.BackColor = thisInst.updateThe.GetFilterFlagColor(Met_Data_Filter.Filter_Flags.missing);
            txtMinAnemSD.BackColor = thisInst.updateThe.GetFilterFlagColor(Met_Data_Filter.Filter_Flags.minAnemSD);
            txtMaxAnemSD.BackColor = thisInst.updateThe.GetFilterFlagColor(Met_Data_Filter.Filter_Flags.maxAnemSD);
            txtAnemMaxRange.BackColor = thisInst.updateThe.GetFilterFlagColor(Met_Data_Filter.Filter_Flags.maxAnemRange);
            txtIcing.BackColor = thisInst.updateThe.GetFilterFlagColor(Met_Data_Filter.Filter_Flags.Icing);
            txtTowerShadow.BackColor = thisInst.updateThe.GetFilterFlagColor(Met_Data_Filter.Filter_Flags.towerEffect);
        }        
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
