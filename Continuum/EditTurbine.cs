﻿using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    public partial class EditTurbine : Form
    {
        /// <summary> Continuum instance that calls EditTurbine </summary>
        public Continuum thisInst;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Class initializer </summary>
        public EditTurbine(Continuum continuum)
        {
            InitializeComponent();
            thisInst = continuum;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Edits the coordinates of selected turbine and calls background worker to perform turbine calcs (if they were done before)
                        
            string name = txtName.Text;
            double UTMX = Convert.ToDouble(txtUTMX.Text);
            double UTMY = Convert.ToDouble(txtUTMY.Text);

            if ( name == "" || UTMX == 0 || UTMY == 0 ) {
                MessageBox.Show("Need valid entries for all fields", "Continuum 3");
                return;
            }

            Check_class Check = new Check_class();
            bool inputTurbine = Check.NewTurbOrMet(thisInst.topo, name, UTMX, UTMY, true);

            if (inputTurbine == true) 
                thisInst.turbineList.EditTurbine(name, UTMX, UTMY); 
            
            Close();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Closes form
            Close();
        }
                
    }
}
