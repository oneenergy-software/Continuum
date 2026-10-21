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
    public partial class Met_LatLong_Units : Form
    {
        public bool goodToGo = false;
        public Met_LatLong_Units()
        {
            InitializeComponent();
            ContinuumStyle.ApplyStyles(this);
            cbo_WS_Units.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            goodToGo = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            goodToGo = false;
            Close();
        }
    }
}
