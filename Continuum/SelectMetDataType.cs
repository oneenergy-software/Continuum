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
    public partial class SelectMetDataType : Form
    {
        public bool goodToGo = false;
        public SelectMetDataType()
        {
            InitializeComponent();
            ContinuumStyle.ApplyStyles(this);
        }

        private void btnContinue_Click(object sender, EventArgs e)
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
