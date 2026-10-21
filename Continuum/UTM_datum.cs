using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary> GUI class that allows user to select UTM datum to use. <summary>
    public partial class UTM_datum : Form
    {
        /// <summary> Class initializer. </summary>
        public UTM_datum()
        {
            InitializeComponent();
            ContinuumStyle.ApplyStyles(this);
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            int datumInd = 0;

            try
            {
                datumInd = cbo_Datums.SelectedIndex;
            }
            catch {
                MessageBox.Show("Please select a datum to use.", "Continuum Wind Energy Software");
                return;
            }

            string hemisphere = "";

            try
            {
                hemisphere = cboNorthOrSouth.SelectedItem.ToString();
            }
            catch
            {
                MessageBox.Show("Please select either northern or southern hemisphere.", "Continuum Wind Energy Software");
                return;
            }

            Close();
        }

        
    }
}
