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
    public partial class CloudNodeToDownload : Form
    {
        public ReferenceCollection.RefDataDownload cloudRefDown;
        public bool goodToGo;

        public CloudNodeToDownload()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            goodToGo = false;
            Close();
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            // Check entered lat/long and make sure that they fall within lat/long ranges
            double thisLat = 0;
            double thisLong = 0;

            double.TryParse(txtLat.Text, out thisLat);
            double.TryParse(txtLon.Text, out thisLong);

            if (thisLat == 0)
            {
                MessageBox.Show("Invalid latitude.");
                goodToGo = false;
                return;
            }
            else if (thisLat < cloudRefDown.minLat)
            {
                MessageBox.Show("Latitude must be greater than the minimum value of " + cloudRefDown.minLat.ToString());
                goodToGo = false;
                return;
            }
            else if (thisLat > cloudRefDown.maxLat)
            {
                MessageBox.Show("Latitude must be less than the maximum value of " + cloudRefDown.maxLat.ToString());
                goodToGo = false;
                return;
            }

            if (thisLong == 0)
            {
                MessageBox.Show("Invalid longitude.");
                goodToGo = false;
                return;
            }
            else if (thisLong < cloudRefDown.minLon)
            {
                MessageBox.Show("Longitude must be greater than the minimum value of " + cloudRefDown.minLon.ToString());
                goodToGo = false;
                return;
            }
            else if (thisLong > cloudRefDown.maxLon)
            {
                MessageBox.Show("Longitude must be less than the maximum value of " + cloudRefDown.maxLon.ToString());
                goodToGo = false;
                return;
            }

            goodToGo = true;
            Close();
        }
    }
}
