using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    public partial class Copernicus_LogIn : Form
    {
        /// <summary> Boolean flag showing whether user clicked Ok or Cancel </summary>
        public bool goodToGo = false;

        /// <summary> Form constructor </summary>
        public Copernicus_LogIn()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Text != "")
                goodToGo = true;
            else
                return;

            Close();
        }
    }
}
