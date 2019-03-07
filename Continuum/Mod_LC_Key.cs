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
    public partial class Mod_LC_Key : Form
    {
        public Continuum thisInst;
        public LC_Key thisLC_Key; 

        public Mod_LC_Key(Continuum continuum, LC_Key lc_key)
        {
            InitializeComponent();
            thisInst = continuum;
            thisLC_Key = lc_key;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Checks for valid entries in each textbox. if ( all valid, update the referenced LC_Key with new description/SR/DH. Updates the land cover key table and closes form.
            int thisCode = 0;
            string newDesc = "";
            double newSR = 0;
            double newDH = 0;

            try {
                thisCode = Convert.ToInt16(txtCode.Text);
            }
            catch  {
                MessageBox.Show("Invalid land cover code.", "Continuum 2.3");
                return;
            }

            try {
                newDesc = txtDesc.Text;
            }
            catch  {
                MessageBox.Show("Invalid land cover description.", "Continuum 2.3");
                return;
            }

            try {
                newSR = Convert.ToSingle(txtSR.Text);
            }
            catch  {
                MessageBox.Show("Invalid surface roughness.", "Continuum 2.3");
                return;
            }

            try {
                newDH = Convert.ToSingle(txtDH.Text);
            }
            catch  {
                MessageBox.Show("Invalid displacement height.", "Continuum 2.3");
                return;
            }

            int numSR = 0;
            try {
                numSR = thisInst.topo.LC_Key.Length;
            }
            catch  {
                numSR = 0;
                return;
            }

            for (int i = 0; i < numSR; i++) { 
                if (thisLC_Key.LC_Key_New[i].code == thisCode) {
                    thisLC_Key.LC_Key_New[i].desc = newDesc;
                    thisLC_Key.LC_Key_New[i].SR = newSR;
                    thisLC_Key.LC_Key_New[i].DH = newDH;
                    break;
                }
            }

            thisInst.Populate_LC_Key_Form(thisLC_Key);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
                
    }
}
