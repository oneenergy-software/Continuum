using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    public partial class Filter_Settings : Form
    {
        /// <summary>
        /// GUI form which displays the Met data QC filter settings.
        /// </summary>
        public Filter_Settings()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}
