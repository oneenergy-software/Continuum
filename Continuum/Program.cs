using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuumNS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>      
        
        [STAThread]
        static void Main(string[] args)
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                if (args.Length == 0)
                    Application.Run(new Continuum("", true));
                else if (args.Length == 1)
                    Application.Run(new Continuum(args[0], true));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatal startup error:\n" + ex.Message + "\n" + ex.StackTrace);
            }
            
            
        }
    }
}
