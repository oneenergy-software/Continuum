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
    public partial class Add_Mode : Form
    {
        public float mean;
        public float SD;
        public float weight;
        public bool okAdd;

        public Add_Mode()
        {
            InitializeComponent();
        }
                
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            okAdd = false;
            Close();
        }

        public void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                mean = Convert.ToSingle(txtMean.Text);
            }
            catch
            {
                MessageBox.Show("Invalid entry for mean.");
                return;
            }

            try
            {
                SD = Convert.ToSingle(txtSD.Text);
                if (SD < 0.001) SD = (float)0.001;
            }
            catch
            {
                MessageBox.Show("Invalid entry for SD.");
                return;
            }

            try
            {
                weight = Convert.ToSingle(txtWeight.Text);
            }
            catch 
            {
                MessageBox.Show("Invalid entry for weight.");
                return;
            }

            okAdd = true;
            Close();
        }
    }
}
