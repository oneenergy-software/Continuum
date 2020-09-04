using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    
    /// <summary> GUI Class that allows use to select modeled extrapolated height. </summary>    
    public partial class Pick_a_Height : Form
    {
        
        /// <summary>   Default constructor. </summary>      
        public Pick_a_Height()
        {
            InitializeComponent();
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {            
            try
            {
                double Selected_height = Convert.ToDouble(lstHeights.SelectedItems[0].Text);
            }
            catch
            {
                MessageBox.Show("Select a height to export.");
                return;
            }

            Close();
        }
    }
}
