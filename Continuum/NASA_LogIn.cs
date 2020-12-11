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
    public partial class NASA_LogIn : Form
    {
        public bool goodToGo = false;
        public NASA_LogIn()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtNASAUsername.Text != "" && txtNASAPassword.Text != "")
                goodToGo = true;
            else
                return;

            Close();
        }
    }
}
