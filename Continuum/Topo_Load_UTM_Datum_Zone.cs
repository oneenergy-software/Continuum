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
    public partial class Topo_Load_UTM_Datum_Zone : Form
    {
        public Topo_Load_UTM_Datum_Zone()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int datumInd = 0;
            int zoneNumber = 0;
            
            try
            {
                datumInd = cbo_Datums.SelectedIndex;
            }
            catch {
                MessageBox.Show("Please select a datum to use.", "Continuum 3");
                return;
            }

            try
            {
                zoneNumber = Convert.ToInt16(cboUTMZone.SelectedItem.ToString());
            }
            catch {
                MessageBox.Show("Please select the UTM Zone number.", "Continuum 3");
                return;
            }                       

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
                
    }
}
