using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using System.Reflection;

namespace ContinuumNS
{
    public partial class AboutContinuum : Form
    {
        public AboutContinuum()
        {

            InitializeComponent();
            AboutContinuumLoad();
        }


        private void AboutContinuumLoad()
        {
            Computer This_Computer = new Computer();
            
            // Set the title of the form.
            string ApplicationTitle = "Continuum";
            string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Assembly currentAssem = typeof(Continuum).Assembly;
            object[] attribs = currentAssem.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
            string company = "";
            string copyright = "";
            string appDescription = "";

            if (attribs.Length > 0)            
                company = ((AssemblyCompanyAttribute)attribs[0]).Company;            

            attribs = currentAssem.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);
            if (attribs.Length > 0)
                copyright = ((AssemblyCopyrightAttribute)attribs[0]).Copyright;

            attribs = currentAssem.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), true);
            if (attribs.Length > 0)
                appDescription = ((AssemblyDescriptionAttribute)attribs[0]).Description;

            Text = String.Format("About {0}", ApplicationTitle);
            // Initialize all of the text displayed on the About Box.
            // TODO: Customize the application's assembly information in the "Application" pane of the project 
            //    properties dialog (under the "Project" menu).
            LabelProductName.Text = ApplicationTitle;            
            LabelVersion.Text = String.Format("Version {0}", assemblyVersion);
            LabelCopyright.Text = copyright;
            LabelCompanyName.Text = company;
            TextBoxDescription.Text = appDescription;
        }

        private void OKButton_Click(object sender, EventArgs e)
        { 
            Close();
        }
                
    }
}
