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
    public partial class Export_Degs_or_UTM : Form
    {
        public Export_Degs_or_UTM()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sende, EventArgs e)
        {
            int latsUTMS = 0;
            try
            {
                latsUTMS = cbo_Lats_UTMs.SelectedIndex;
            }
            catch
            {
                MessageBox.Show("Please select either Lats/Longs or UTM coordinates from drop-down box.", "Continuum 3");
                return;
            }

            Close();
        }

        
    }
}
