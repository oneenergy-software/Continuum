using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Scripting.Hosting;
using System.Net;
using System.Net.Sockets;
using System.Data.Entity.Core.Metadata.Edm;
using System.Runtime.CompilerServices;
using static ContinuumNS.ReferenceCollection;
using System.Threading;

namespace ContinuumNS
{
    /// <summary> Form to download ERA5 or MERRA2 reanalysis daily datafiles </summary>
    public partial class Reanalysis_Download : Form
    {
        Continuum thisInst;
        /// <summary> Reference data download object created to initiate new download </summary>
        public ReferenceCollection.RefDataDownload dataToDownload = new ReferenceCollection.RefDataDownload();
        
        /// <summary> Reanalysis download Constructor </summary>        
        public Reanalysis_Download(Continuum continuum)
        {
            InitializeComponent();
            thisInst = continuum;
            UpdateListOfRefDataDownloads();
        }

        private void btnDownloadMERRA2_Click(object sender, EventArgs e)
        {
            bool needToDownload = false;

            if (cboRefDataDownloads.SelectedItem == null)
            {
                MessageBox.Show("Select 'New' to create a new reference data download");
                return;
            }

            if (cboReanalysisType.SelectedItem.ToString() == "ERA5")
            {
                bool gotNetCDF = thisInst.IsNetCDF_Installed();

                if (gotNetCDF == false)
                {
                    MessageBox.Show("Continuum reads in ERA5 data as netCDF (.nc) files however the NetCDF libraries were not found on this PC. Please go to " +
                        "continuumwind.com and click 'Download Software' to access the installation file for NetCDF.");
                    return;
                }
            }

            if (cboReanalysisType.SelectedItem.ToString() == "ERA5" && dataToDownload.completion < 1)
            {
                // Check system for Python and for cdsapi
                bool pythonInstalled = thisInst.IsPythonInstalled();

                if (pythonInstalled == false)
                {
                    MessageBox.Show("ERA5 data is downloaded using Python however Python is not installed on this PC.  Go to 'https://www.python.org/downloads/' to download.");
                    return;
                }                               

                bool cdsapiInstall = thisInst.IsCDSAPI_Installed();
                
                if (cdsapiInstall == false)
                {
                    MessageBox.Show("To download ERA5 data, you must register with Climate Data Store (CDS) and install the CDS API.  Go to 'https://cds.climate.copernicus.eu/api-how-to' for more details");
                    return;
                }     
            }
            
            if (cboRefDataDownloads.SelectedItem.ToString() == "Define New")
            {
                // User is trying to start a new data download.
                // Make sure that a valid folder has been selected, end date is after start date, confirm number of nodes, check for username/password
                                
                dataToDownload.refType = cboReanalysisType.SelectedItem.ToString(); 
                
                if (dataToDownload.folderLocation == "")
                {
                    MessageBox.Show("You have to choose a folder to save the " + dataToDownload.refType + " datafiles.  Click 'Change Folder' button to select folder");
                    return;
                }                                

                int offset = thisInst.UTM_conversions.GetUTC_Offset(dataToDownload.minLat, dataToDownload.minLon);                
                dataToDownload.startDate = dateReferenceStart.Value.AddHours(-offset);                
                dataToDownload.endDate = dateReferenceEnd.Value.AddHours(-offset);

                if (dateReferenceEnd.Value > DateTime.Now)
                {
                    MessageBox.Show("End date cannot be in the future.");
                    return;
                }

                // Check to see if new files need to be downloaded
                if (dataToDownload.completion < 1.0)
                    needToDownload = true;

                // Make sure that the lat/long range includes at least one node, min < max, and show message displaying number of nodes and time range selected to download (give chance to cancel)
                if (txtMinLat.Text == "" || txtMaxLat.Text == "" || txtMinLong.Text == "" || txtMaxLong.Text == "")
                {
                    MessageBox.Show("Enter a latitude and longitude range to download " + dataToDownload.refType + " data.");
                    return;
                }

                double minLat = Convert.ToDouble(txtMinLat.Text);
                double maxLat = Convert.ToDouble(txtMaxLat.Text);
                double minLong = Convert.ToDouble(txtMinLong.Text);
                double maxLong = Convert.ToDouble(txtMaxLong.Text);

                double latRes = 0.25;
                double longRes = 0.25;

                if (dataToDownload.refType == "MERRA2")
                {
                    latRes = 0.5;
                    longRes = 0.625;
                }

                minLat = Math.Round(minLat / latRes, 0) * latRes;
                maxLat = Math.Round(maxLat / latRes, 0) * latRes;
                minLong = Math.Round(minLong / longRes) * longRes;
                maxLong = Math.Round(maxLong / longRes) * longRes;

                dataToDownload.minLat = minLat;
                dataToDownload.maxLat = maxLat;
                dataToDownload.minLon = minLong;
                dataToDownload.maxLon = maxLong;

                int numLat = thisInst.refList.GetNumNodesInRange(minLat, maxLat, "Lat", dataToDownload);
                int numLong = thisInst.refList.GetNumNodesInRange(minLong, maxLong, "Long", dataToDownload);
                int numNodes = numLat * numLong;

                if (minLat > maxLat)
                {
                    MessageBox.Show("Minimum latitude must be smaller than maximum latitude in range");
                    return;
                }

                if (minLong > maxLong)
                {
                    MessageBox.Show("Minimum longitude must be smaller than maximum longitude in range");
                    return;
                }

                if (numLat == 0)
                {
                    MessageBox.Show("There are no nodes that fall within specified latitude range: " + minLat.ToString() + " to " + maxLat.ToString());
                    return;
                }
                if (numLong == 0)
                {
                    MessageBox.Show("There are no nodes that fall within specified longitude range: " + minLong.ToString() + " to " + maxLong.ToString());
                    return;
                }

                DialogResult goodToGo = MessageBox.Show("With a latitude range of " + minLat.ToString() + " to " + maxLat.ToString() + " and a longitude range of " + minLong.ToString()
                    + " to " + maxLong.ToString() + ", a total of " + numNodes.ToString() + " nodes will be downloaded (" + numLat.ToString() + " along latitude and " + numLong
                    + " along longitude. Do you want to continue?", "Continuum", MessageBoxButtons.YesNo);

                if (goodToGo == DialogResult.No)
                    return;

                if (needToDownload && (dataToDownload.userName == "" || dataToDownload.userPassword == "" || dataToDownload.userName == null 
                    || dataToDownload.userPassword == null))
                {
                    // See if user credentials are saved in another MERRA2 object
                    string[] userPsd = thisInst.refList.GetUserPasswordbyRefType(dataToDownload.refType);

                    if ((userPsd[0] != "" && userPsd[1] != "") && (userPsd[0] != null && userPsd[1] != null))
                    {
                        dataToDownload.userName = userPsd[0];
                        dataToDownload.userPassword = userPsd[1];
                    }
                    else
                    {
                        if (dataToDownload.refType == "MERRA2")
                        {
                            NASA_LogIn nasaCreds = new NASA_LogIn();
                            nasaCreds.ShowDialog();

                            if (nasaCreds.goodToGo == false)
                                return;

                            try
                            {
                                dataToDownload.userName = nasaCreds.txtNASAUsername.Text;
                                dataToDownload.userPassword = nasaCreds.txtNASAPassword.Text;
                            }
                            catch
                            {
                                MessageBox.Show("Invalid Earthdata credentials");
                                return;
                            }
                        }
                        else if (dataToDownload.refType == "ERA5")
                        {
                            // Don't need to enter credentials since the download uses the CDS API program which includes the user's API key

                        }
                    }
                }

                if (thisInst.refList.HaveThisRefDataDownload(dataToDownload))
                {
                    MessageBox.Show("Reference data download with the same settings has already been created");
                    return;
                }                

                thisInst.refList.AddRefDataDownload(dataToDownload);

            }                    
            else
            {
                // Check to see if new files need to be downloaded
                
                if (dataToDownload.completion < 1.0)
                    needToDownload = true;
                
                if (dataToDownload.refType == "MERRA2" && (dataToDownload.userName == null || dataToDownload.userPassword == null))
                {
                    // See if user credentials are saved in another MERRA2 object
                    string[] userPsd = thisInst.refList.GetUserPasswordbyRefType(dataToDownload.refType);                                                            

                    if ((userPsd[0] != "" && userPsd[1] != "") && (userPsd[0] != null && userPsd[1] != null))
                    {
                        dataToDownload.userName = userPsd[0];
                        dataToDownload.userPassword = userPsd[1];
                    }
                    else if (needToDownload)
                    {                        
                        NASA_LogIn nasaCreds = new NASA_LogIn();
                        nasaCreds.ShowDialog();

                        if (nasaCreds.goodToGo == false)
                            return;

                        try
                        {
                            dataToDownload.userName = nasaCreds.txtNASAUsername.Text;
                            dataToDownload.userPassword = nasaCreds.txtNASAPassword.Text;
                        }
                        catch
                        {
                            MessageBox.Show("Invalid Earthdata credentials");
                            return;
                        }
                        
                    }
                }                

                thisInst.refList.UpdateRefDataDownload(dataToDownload);
            }

            if (needToDownload)
            {
                if (dataToDownload.refType == "MERRA2")
                    thisInst.refList.NASA_LogInAsync(thisInst, dataToDownload);
                else
                {
                    thisInst.refList.DownloadERA5(thisInst, dataToDownload);
                }
            }

            thisInst.ChangesMade();

            Close();
        }
        /// <summary> Updates textboxes, dropdowns, and dateTimePickers Enabled flag based on specified boolean </summary>        
        public void EnableDisableControls(bool enable)
        {
            cboReanalysisType.Enabled = enable;
            txtFolderLoc.Enabled = enable;
       //     dateReferenceStart.Enabled = enable;  // Always allow users to change start/end dates for downloading
       //     dateReferenceEnd.Enabled = enable;
            txtMinLat.Enabled = enable;
            txtMaxLat.Enabled = enable;
            txtMinLong.Enabled = enable;
            txtMaxLong.Enabled = enable;
        }

        /// <summary> Updates folder location, start/end dates, and textboxes showing Reference Download Data selected from list.  If it's
        /// new, then default start/end date, emtpy min max lat/long ranges, same data folder as last reference data of same type (if any). </summary>
        public void UpdateForm()
        {
            if (cboRefDataDownloads.SelectedItem == null && dataToDownload.minLat == 0) // No defined reference downloads yet
            {
                cboReanalysisType.SelectedIndex = 0;
                EnableDisableControls(false); // Disable all controls (ie user needs to click 'New')
                return;
            }
            else if (cboRefDataDownloads.SelectedItem == null && dataToDownload.minLat != 0) // Created RefDataDownload from selected folder
            {
                EnableDisableControls(false);

                txtFolderLoc.Text = dataToDownload.folderLocation;                

                txtMinLat.Text = dataToDownload.minLat.ToString();
                txtMaxLat.Text = dataToDownload.maxLat.ToString();
                txtMinLong.Text = dataToDownload.minLon.ToString();
                txtMaxLong.Text = dataToDownload.maxLon.ToString();

                cboReanalysisType.SelectedItem = dataToDownload.refType;

                dataToDownload.completion = thisInst.refList.CalcDownloadedDataCompletion(dataToDownload);
                txtPercComplete.Text = Math.Round(dataToDownload.completion * 100.0, 1).ToString();

                btnDownloadMERRA2.BackColor = Color.MediumSeaGreen;
            }
            else if (cboRefDataDownloads.SelectedItem.ToString() == "Define New")
            {                
                // Enable all controls to let user define new reference data download
                EnableDisableControls(true);
                txtFolderLoc.Text = dataToDownload.folderLocation;
                //return;                
            }
            else
            {
                dataToDownload = thisInst.refList.GetRefDataDownloadByName(cboRefDataDownloads.SelectedItem.ToString());
                EnableDisableControls(false);
            
                txtFolderLoc.Text = dataToDownload.folderLocation;

                // Check to see if Reference Data folder exists
                bool folderExists = Directory.Exists(dataToDownload.folderLocation);
                if (folderExists == false)
                {
                    btnDownloadMERRA2.Enabled = false;
                    btnDownloadMERRA2.BackColor = Color.LightCoral;
                    MessageBox.Show("Saved folder location not found");
                }

                txtMinLat.Text = dataToDownload.minLat.ToString();
                txtMaxLat.Text = dataToDownload.maxLat.ToString();
                txtMinLong.Text = dataToDownload.minLon.ToString();
                txtMaxLong.Text = dataToDownload.maxLon.ToString();

                cboReanalysisType.SelectedItem = dataToDownload.refType;
                int offset = thisInst.UTM_conversions.GetUTC_Offset(dataToDownload.minLat, dataToDownload.minLon);

                if (dataToDownload.startDate.Year != 1)
                {                    
                    dateReferenceStart.Value = dataToDownload.startDate.AddHours(offset); // dateReference are local time start/end                    
                    dateReferenceEnd.Value = dataToDownload.endDate.AddHours(offset); // dataToDownload start/end are in UTC-0
                }
                else
                {
                    dateReferenceStart.Enabled = true;
                    dateReferenceEnd.Enabled = true;
                    dataToDownload.startDate = dateReferenceStart.Value.AddHours(-offset);
                    dataToDownload.endDate = dateReferenceEnd.Value.AddHours(-offset);
                }

                dataToDownload.completion = thisInst.refList.CalcDownloadedDataCompletion(dataToDownload);
                txtPercComplete.Text = Math.Round(dataToDownload.completion * 100.0, 4).ToString();

                btnDownloadMERRA2.BackColor = Color.MediumSeaGreen;
            }
           
        }

        private void btnChangeFolder_Click(object sender, EventArgs e)
        {            
            MessageBox.Show("Please select folder containing reference data files or an empty folder for a new reference data download.");
            thisInst.fbd_MERRAData.ShowNewFolderButton = true;

            if (thisInst.fbd_MERRAData.ShowDialog() == DialogResult.OK)
            {
                // Read first file and figure out parameters for RefDataDownload and assign to dataToDownload
                
                if (Directory.Exists(thisInst.fbd_MERRAData.SelectedPath) == false) // user renamed 'new folder' so need to go grab new name
                {
                    DirectoryInfo[] allFolders = new DirectoryInfo(thisInst.fbd_MERRAData.SelectedPath).Parent.GetDirectories();
                    allFolders.OrderByDescending(d => d.LastWriteTimeUtc).First();
                    thisInst.fbd_MERRAData.SelectedPath = allFolders[0].FullName;
                }

                dataToDownload = thisInst.refList.ReadFileAndDefineRefDataDownload(thisInst.fbd_MERRAData.SelectedPath);

                if (dataToDownload.refType == null)
                    dataToDownload.refType = cboReanalysisType.SelectedItem.ToString();
                else
                {
                    ReferenceCollection.DateRangeAndCompletion refdataRangeAndComplete = thisInst.refList.GetDataFileStartEndDateAndCompletion(dataToDownload.folderLocation, dataToDownload.refType);
                    dataToDownload.startDate = refdataRangeAndComplete.startEnd[0];
                    dataToDownload.endDate = refdataRangeAndComplete.startEnd[1];
                    dataToDownload.completion = refdataRangeAndComplete.completion;
                }

                if (thisInst.refList.HaveThisRefDataDownload(dataToDownload) == false && dataToDownload.minLat != 0)
                {
                    thisInst.refList.AddRefDataDownload(dataToDownload);
                    UpdateListOfRefDataDownloads();
                }
                else
                    UpdateForm();
            }
            else
                return;
        }
                           

        /// <summary> Updates list of reference data downloads and selects last in list (which triggers form update) </summary>
        public void UpdateListOfRefDataDownloads()
        {
            cboRefDataDownloads.Items.Clear();
            cboRefDataDownloads.Text = "";

            for (int r = 0; r < thisInst.refList.numRefDataDownloads; r++)            
                cboRefDataDownloads.Items.Add(thisInst.refList.refDataDownloads[r].GetName());
            
            if (cboRefDataDownloads.Items.Count > 0 ) 
                cboRefDataDownloads.SelectedIndex = thisInst.refList.numRefDataDownloads - 1;
            else // No Ref Data downloads defined yet so set to default referency type
                cboReanalysisType.SelectedIndex = 0;
        }

        private void cboRefDataDownloads_SelectedIndexChanged(object sender, EventArgs e)
        {           
            UpdateForm();
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            // Creates and adds new ref data down

            // First see if 'Define New' has already been added
            for (int r = 0; r < cboRefDataDownloads.Items.Count; r++)
                if (cboRefDataDownloads.Items[r].ToString() == "Define New")
                {
                    cboRefDataDownloads.SelectedIndex = r;
                    return;
                }

            // Otherwise add "Define New" and select it
            cboRefDataDownloads.Items.Add("Define New");
            cboRefDataDownloads.SelectedIndex = cboRefDataDownloads.Items.Count - 1;            
            
        }

        private void dateReferenceStart_ValueChanged(object sender, EventArgs e)
        {
            // Update start date of selected RefDataDownload
            DateTime newStart = dateReferenceStart.Value;
            int offset = thisInst.UTM_conversions.GetUTC_Offset(dataToDownload.minLat, dataToDownload.minLon);
            dataToDownload.startDate = dateReferenceStart.Value.AddHours(-offset);
                        
            double percCompl = thisInst.refList.CalcDownloadedDataCompletion(dataToDownload);
            dataToDownload.completion = percCompl;
            txtPercComplete.Text = Math.Round(percCompl * 100.0, 1).ToString();
        }

        private void dateReferenceEnd_ValueChanged(object sender, EventArgs e)
        {
            // Update end date of selected RefDataDownload
            DateTime newEnd = dateReferenceEnd.Value;
            int offset = thisInst.UTM_conversions.GetUTC_Offset(dataToDownload.minLat, dataToDownload.minLon);
            dataToDownload.endDate = dateReferenceEnd.Value.AddHours(-offset);

            double percCompl = thisInst.refList.CalcDownloadedDataCompletion(dataToDownload);
            dataToDownload.completion = percCompl;
            txtPercComplete.Text = Math.Round(percCompl * 100.0, 1).ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Removes selected Reference Data Download from list
            string selRefData = cboRefDataDownloads.SelectedItem.ToString();
            RefDataDownload selRefDataDown = thisInst.refList.GetRefDataDownloadByName(selRefData);
            thisInst.refList.DeleteRefDataDownload(selRefDataDown);
            UpdateListOfRefDataDownloads();
        }

        private void cboReanalysisType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
