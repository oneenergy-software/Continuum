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
    public partial class UTM_datum : Form
    {
        public UTM_datum()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            int datumInd = 0;

            try
            {
                datumInd = cbo_Datums.SelectedIndex;
            }
            catch {
                MessageBox.Show("Please select a datum to use.", "Continuum 3");
                return;
            }

            string hemisphere = "";

            try
            {
                hemisphere = cboNorthOrSouth.SelectedItem.ToString();
            }
            catch
            {
                MessageBox.Show("Please select either northern or southern hemisphere.", "Continuum 3");
                return;
            }

            Close();
        }

        
    }
}
