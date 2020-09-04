using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary>
    /// Class that adds a mode to a defined exceedance curve
    /// </summary>
    public partial class Add_Mode : Form
    {
        /// <summary> Mean value of mode </summary>
        public float mean;
        /// <summary> Standard deviation of mode </summary>
        public float SD;
        /// <summary> Weight applied to mode (in multimodal distribution). Value range: 1 - 100 </summary>
        public float weight;
        /// <summary> True if mode is successfully defined and can be added to exceedance curve </summary>
        public bool okAdd;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Class initializer </summary>
        public Add_Mode()
        {
            InitializeComponent();
        }
               
        /// <summary> Cancel button closes form </summary>        
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            okAdd = false;
            Close();
        }

        /// <summary> Ok button adds mode to curve</summary>        
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
