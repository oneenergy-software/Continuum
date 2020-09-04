using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    public partial class MCP_ValidSettings : Form
    {
        /// <summary> Form showing the allowable (i.e. valid) ranges of Matrix MCP settings </summary>
        public MCP_ValidSettings()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
