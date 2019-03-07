using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicNP.CryptoLicensing;

namespace ContinuumNS
{
    public partial class Registration_key : Form
    {
        public Continuum thisInst;

        public Registration_key(Continuum continuum)
        {
            InitializeComponent();
            thisInst = continuum;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRegContinuum_Click(object sender, EventArgs e)
        {
            // Calls Check_Registration if a license key exists
            try {
                thisInst.usersKey = txtRegKey.Text;
            }
            catch  {
                txtMessage.Text = "Enter a License Key";
                return;
            }

            Check_Registration();

        }

        public void Check_Registration()
        {
            // Looks for user//s license key and validates license if it exists

            if (thisInst.usersKey == "")
                thisInst.usersKey = Check_for_license_file();

            if (thisInst.usersKey == "") {
                txtMessage.Text = "";
                btnOk.Enabled = false;
                this.ShowDialog();
            }
            else {
                txtRegKey.Text = thisInst.usersKey;
                Validate_License(ref thisInst.usersKey);

                if (thisInst.functionalitiesEnabled == false) {
                    try
                    {
                        this.ShowDialog();
                    }
                    catch 
                    {
                        return;
                    }
                }
            }
        }

        public string Check_for_license_file()
        { // Returns user//s license key
            string Users_Key = "";

            CryptoLicense license = new CryptoLicense();
            license.ValidationKey = thisInst.validationKey;
            license.StorageMode = LicenseStorageMode.ToRegistry;

            if (license.Load() == false)
                Users_Key = "";
            else
                Users_Key = license.LicenseCode;

            //   Dim Path_to_Lic  string = ""

            //  Path_to_Lic = Application.StartupPath

            // if ( Users_Key = "" ) {
            //  try {
            // FileOpen(1, Path_to_Lic & "\Continuum 2.3.license", OpenMode.Input)
            // Users_Key = LineInput(1)
            // FileClose(1)
            //  catch (Exception ex) {

            //        }
            //        }
            return Users_Key;
        }


        public void Validate_License(ref string usersKey)
        {
            // Validates user//s license key using license service running on www.continuumwindsoftware.com
            // Dim license  CryptoLicense = new CryptoLicense(usersKey, thisInst.validationKey)
            CryptoLicense license = new CryptoLicense();
            license.ValidationKey = thisInst.validationKey;
            license.StorageMode = LicenseStorageMode.ToRegistry;
            license.LicenseCode = usersKey;

            license.LicenseServiceURL = "http://www.continuumwindsoftware.com/LicenseService_2/Service.asmx";

            string Status = "";

            if (license.Status == LicenseStatus.ActivationFailed)
            {
                txtMessage.Text = "Maximum Activations (1) Exceeded";
                thisInst.functionalitiesEnabled = false;
            }
            else if (license.Status == LicenseStatus.Deactivated)
                txtMessage.Text = "License Key Deactivated";
            else if (license.Status == LicenseStatus.InstancesExceeded)
            {
                txtMessage.Text = "Maximum Number of Analyses Reached";
                thisInst.functionalitiesEnabled = false;
            }
            else if (license.Status == LicenseStatus.EvaluationExpired || license.Status == LicenseStatus.UniqueUsageDaysExceeded || license.Status == LicenseStatus.UsageDaysExceeded)
            {
                txtMessage.Text = "Trial Period Expired.";
                thisInst.functionalitiesEnabled = false;
            }
            else if (license.Status == LicenseStatus.Expired)
            {
                txtMessage.Text = "License key expired.";
                thisInst.functionalitiesEnabled = false;
            }
            else if (license.Status == LicenseStatus.GenericFailure || license.Status == LicenseStatus.NotValidated || license.Status == LicenseStatus.ServiceNotificationFailed)
            {
               // FOR UNIT TESTS
                // txtMessage.Text = "License key validation failed. Make sure that there is an internet connection and try again.";
              //  thisInst.functionalitiesEnabled = false;
            }
            else if (license.Status == LicenseStatus.InstancesExceeded)
            {
                txtMessage.Text = "Maximum Number of Instances (1) Exceeded";
                thisInst.functionalitiesEnabled = false;
            }
            else if (license.Status == LicenseStatus.CryptoLicensingModuleTampered || license.Status == LicenseStatus.DateRollbackDetected || license.Status == LicenseStatus.DebuggerDetected
               || license.Status == LicenseStatus.EvaluationlTampered || license.Status == LicenseStatus.InValid || license.Status == LicenseStatus.LicenseServerMachineCodeInvalid
               || license.Status == LicenseStatus.LocalTimeInvalid || license.Status == LicenseStatus.MachineCodeInvalid || license.Status == LicenseStatus.SerialCodeInvalid
               || license.Status == LicenseStatus.SignatureInvalid || license.Status == LicenseStatus.StrongNameVerificationFailed || license.Status == LicenseStatus.UsageModeInvalid)
            {
                txtMessage.Text = "Invalid license key. Please contact liz@cancalia.com to help resolve the issue.";
                thisInst.functionalitiesEnabled = false;
            }
            else if (license.Status == LicenseStatus.Valid)
            {
                txtMessage.Text = "Valid license";
                thisInst.functionalitiesEnabled = true;
                btnRegContinuum.Enabled = false;
                txtRegKey.Enabled = false;
                btnOk.Enabled = true;

                if (license.HasMaxUniqueUsageDays == true)
                {
                    int Num_days_left = license.RemainingUniqueUsageDays;
                    txtMessage.Text = Num_days_left.ToString() + " days of usage until this trial version expires.";

                    try
                    {
                        ShowDialog();
                    }
                    catch 
                    {
                        Close();
                    }
                }
            }
            else
            {
                txtMessage.Text = "License key validation failed. Make sure that there is an internet connection and try again.";
                thisInst.functionalitiesEnabled = false;
            }

                license.Save();
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Closes form
            this.Close();
        }

        private void btnDelLicense_Click(object sender, EventArgs e)
        {
            // Clears user//s license and allows user to enter a new one
            CryptoLicense license = new CryptoLicense();
            license.ValidationKey = thisInst.validationKey;
            license.StorageMode = LicenseStorageMode.ToRegistry;
            license.Remove();
            thisInst.usersKey = "";
            txtMessage.Text = "";
            txtRegKey.Text = "";
            txtRegKey.Enabled = true;
            btnOk.Enabled = false;
            btnRegContinuum.Enabled = true;
        }

        
    }
}
