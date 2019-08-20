////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Pick_a_Height.cs
//
// summary:	Implements the pick a height class
////////////////////////////////////////////////////////////////////////////////////////////////////

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
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A pick a height. </summary>
    ///
    /// <remarks>   OEE, 6/21/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class Pick_a_Height : Form
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   OEE, 6/21/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Pick_a_Height()
        {
            InitializeComponent();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by btnOK for click events. </summary>
        ///
        /// <remarks>   OEE, 6/21/2017. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnOK_Click(object sender, EventArgs e)
        {
            // check to see if height is selected

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
