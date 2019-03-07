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
    public partial class Select_UTM_zone : Form
    {
        public Select_UTM_zone()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            int longZone = 0;
            char latBand;

            try
            {
                longZone = cbo_UTMNumber.SelectedIndex + 1;
            }
            catch 
            {
                MessageBox.Show("Please specify the longitude UTM zone to use.", "Continuum 2.3");
                return;
            }

            try
            {
                latBand = Convert.ToChar(cboHemisphere.SelectedItem.ToString());
            }
            catch 
            {
                MessageBox.Show("Please specify the latitude UTM band to use.", "Continuum 2.3");
                return;
            }
            Close();
        }

        
    }
}
