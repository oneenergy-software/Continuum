using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary> GUI class that allows user to export exposure and SRDH at met and turbine sites for selected radii. </summary>
    public partial class Expo_Export : Form
    {
        Continuum thisInst;

        /// <summary> Class initializer</summary>        
        public Expo_Export(Continuum continuum)
        {
            InitializeComponent();
            thisInst = continuum;
        }

        private void chkboxAllRadii_CheckedChanged(object sender, EventArgs e)
        {
            // Selects or Deselects all radii from list                        
            int numRadii = thisInst.radiiList.ThisCount;
            int numChecked = chkRadii.CheckedItems.Count;

            if (chkboxAllRadii.Checked == true && numRadii != numChecked) {
                chkRadii.Items.Clear();

                for (int i = 0; i < numRadii; i++) {
                    string invest_str = "R= " + thisInst.radiiList.investItem[i].radius;
                    chkRadii.Items.Add(invest_str, true);
                }            
            }
            else if (chkboxAllRadii.Checked == false && numRadii == numChecked)
            {
                chkRadii.Items.Clear();
            for (int i = 0; i < numRadii; i++) {
                    string invest_str = "R= " + thisInst.radiiList.investItem[i].radius;
                    chkRadii.Items.Add(invest_str, false);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) // Closes form
        {
            Close();
        }

        private void chkboxAllTurbs_CheckedChanged(object sender, EventArgs e)
        {
            // Selects or deselects all turbines from list
            int numTurbs = thisInst.turbineList.TurbineCount;
            if (numTurbs > 0) {
                if (chkboxAllTurbs.Checked == true) {
                    chkTurbs.Items.Clear();
                    for (int i = 0; i < numTurbs; i++) {
                        chkTurbs.Items.Add(thisInst.turbineList.turbineEsts[i].name, true);
                    }
                }
                else
                {
                    chkTurbs.Items.Clear();
                    for (int i = 0; i < numTurbs; i++)
                        chkTurbs.Items.Add(thisInst.turbineList.turbineEsts[i].name, false);                    
                }
            }
        }

        private void chkboxAllMets_CheckedChanged(object sender, EventArgs e)
            {
            // Selects or deselects all mets from list            
            int numMets = thisInst.metList.ThisCount;

            if (numMets > 0)
            {
                if (chkboxAllMets.Checked == true)
                {
                    chkMets.Items.Clear();
                    for (int i = 0; i < numMets; i++) {
                        chkMets.Items.Add(thisInst.metList.metItem[i].name, true);
                    }
                }
                else {
                    chkMets.Items.Clear();
                    for (int i = 0; i < numMets; i++) {
                        chkMets.Items.Add(thisInst.metList.metItem[i].name, false);
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Calls Export.ExportExpos to export specified mets and turbines and radii exposure and SR/DH to .CSV
            int numRadii = chkRadii.CheckedItems.Count;
            int numMets = chkMets.CheckedItems.Count;
            int numTurbs = chkTurbs.CheckedItems.Count;

            bool sectorOut = false;
            bool bulkOut = false;            
            bool sectorSRDH_Out = false;
            bool bulkSRDH_Out = false;

            if (numRadii == 0) {
                MessageBox.Show("You need to select at least one radius and exponent to export.", "Continuum 3");
                return;
            }

            if (numMets == 0 && numTurbs == 0) {
                MessageBox.Show("You have to select at least one met or turbine site to export.", "Continuum 3");
                return;
            }

            string[] radii = new string[numRadii]; 
            for (int i = 0; i < numRadii; i++) {
                radii[i] = chkRadii.CheckedItems[i].ToString();
            }

            string[] mets = { "" };
            if (numMets > 0) {
                mets = new string[numMets];
                for (int i = 0; i < numMets; i++) 
                    mets[i] = chkMets.CheckedItems[i].ToString();                
            }
            else 
                mets = null;        

            string[] turbs  = { "" };
            if (numTurbs > 0) {
                turbs = new string[numTurbs];
                for (int i = 0; i < numTurbs; i++) {
                    turbs[i] = chkTurbs.CheckedItems[i].ToString();
                }
            }
            else {
                turbs = null;
            }

            if (chkSector.Checked == true)
                sectorOut = true;

            if (chkBulkExpos.Checked == true)
                bulkOut = true;

            if (chkSectorSRDH.Checked == true)
                sectorSRDH_Out = true;

            if (chkBulkSRDH.Checked == true)
                bulkSRDH_Out = true;

            //  if ( chkP10UW_CrossGrade.Checked = true ) {
            // P10_UW_Cross_out = true
            // }

            Export thisExport = new Export();
            thisExport.ExportExpos(thisInst, radii, mets, turbs, sectorOut, bulkOut, 1, thisInst.topo.gotSR, sectorSRDH_Out, bulkSRDH_Out);
            Close();
        }               
        
    }
}
