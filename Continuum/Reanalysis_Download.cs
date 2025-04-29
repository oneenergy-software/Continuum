﻿using System;
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
        /// <summary> If set to false, form is not updated </summary>
        public bool okToUpdate;

        /// <summary> Reanalysis download Constructor </summary>        
        public Reanalysis_Download(Continuum continuum)
        {
            InitializeComponent();
            thisInst = continuum;
            okToUpdate = true;
            UpdateListOfRefDataDownloads();
            UpdateShowTimeDropdown();

            if (cboDailyOrMonthly.SelectedIndex == -1)
                cboDailyOrMonthly.SelectedIndex = 0;
        }

        private void btnDownloadMERRA2_Click(object sender, EventArgs e)
        {
            bool needToDownload = false;
            string selRefDataDownload = "";

            if (cboRefDataDownloads.SelectedItem != null)
                selRefDataDownload = cboRefDataDownloads.SelectedItem.ToString();
            else if (cboRefDataDownloads.Text != null)
                selRefDataDownload = cboRefDataDownloads.Text;

            if (selRefDataDownload == "")
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
            
            if (selRefDataDownload == "Define New")
            {
                // User is trying to start a new data download.
                // Make sure that a valid folder has been selected, end date is after start date, confirm number of nodes, check for username/password
                                
                dataToDownload.refType = cboReanalysisType.SelectedItem.ToString(); 
                dataToDownload.monthlyOrDaily = cboDailyOrMonthly.SelectedItem.ToString();
                
                if (dataToDownload.refType != "MERRA2 Cloud")
                    dataToDownload.incl10mWS = chkInclOpParam1.Checked;

                if (dataToDownload.refType == "ERA5" && chkInclOpParam2.Checked)
                    dataToDownload.incl10mGust = true;
                
                if (dataToDownload.folderLocation == "")
                {
                    MessageBox.Show("You have to choose a folder to save the " + dataToDownload.refType + " datafiles.  Click 'Change Folder' button to select folder");
                    return;
                }

                // Make sure that the lat/long range includes at least one node, min < max, and show message displaying number of nodes and time range selected to download (give chance to cancel)
                if (txtMinLat.Text == "" || txtMaxLat.Text == "" || txtMinLong.Text == "" || txtMaxLong.Text == "")
                {
                    MessageBox.Show("Enter a latitude and longitude range to download " + dataToDownload.refType + " data.");
                    return;
                }

                ReadLatLongs();
                
                double latRes = 0.25;
                double longRes = 0.25;

                if (dataToDownload.refType.Contains("MERRA2"))
                {
                    latRes = 0.5;
                    longRes = 0.625;
                }

                dataToDownload.minLat = Math.Round(dataToDownload.minLat / latRes, 0) * latRes;
                dataToDownload.maxLat = Math.Round(dataToDownload.maxLat / latRes, 0) * latRes;
                dataToDownload.minLon = Math.Round(dataToDownload.minLon / longRes) * longRes;
                dataToDownload.maxLon = Math.Round(dataToDownload.maxLon / longRes) * longRes;                               

                int numLat = thisInst.refList.GetNumNodesInRange(dataToDownload.minLat, dataToDownload.maxLat, "Lat", dataToDownload);
                int numLong = thisInst.refList.GetNumNodesInRange(dataToDownload.minLon, dataToDownload.maxLon, "Long", dataToDownload);
                int numNodes = numLat * numLong;

                if (dataToDownload.minLat > dataToDownload.maxLat)
                {
                    MessageBox.Show("Minimum latitude must be smaller than maximum latitude in range");
                    return;
                }

                if (dataToDownload.minLon > dataToDownload.maxLon)
                {
                    MessageBox.Show("Minimum longitude must be smaller than maximum longitude in range");
                    return;
                }

                if (numLat == 0)
                {
                    MessageBox.Show("There are no nodes that fall within specified latitude range: " + dataToDownload.minLat.ToString() + " to " + dataToDownload.maxLat.ToString());
                    return;
                }
                if (numLong == 0)
                {
                    MessageBox.Show("There are no nodes that fall within specified longitude range: " + dataToDownload.minLon.ToString() + " to " + dataToDownload.maxLon.ToString());
                    return;
                }

                dataToDownload.startDate = dateReferenceStart.Value;
                dataToDownload.endDate = dateReferenceEnd.Value;

                if (cboDailyOrMonthly.SelectedItem.ToString() == "Monthly")
                    dataToDownload.endDate = dataToDownload.endDate.AddMonths(1).AddHours(-1);

                if (cboShowTime.SelectedItem.ToString() == "Local")
                {
                    int offset = thisInst.UTM_conversions.GetUTC_Offset(dataToDownload.minLat, dataToDownload.minLon);
                    dataToDownload.startDate = dataToDownload.startDate.AddHours(-offset);
                    dataToDownload.endDate = dataToDownload.endDate.AddHours(-offset);
                }
                
                if (dateReferenceEnd.Value > DateTime.Now)
                {
                    MessageBox.Show("End date cannot be in the future.");
                    return;
                }

                // Check to see if new files need to be downloaded
                if (dataToDownload.completion < 1.0)
                    needToDownload = true;
                                
                DialogResult goodToGo = MessageBox.Show("With a latitude range of " + dataToDownload.minLat.ToString() + " to " + dataToDownload.maxLat.ToString() + " and a longitude range of " + dataToDownload.minLon.ToString()
                    + " to " + dataToDownload.maxLon.ToString() + ", a total of " + numNodes.ToString() + " nodes will be downloaded (" + numLat.ToString() + " along latitude and " + numLong
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
                        if (dataToDownload.refType.Contains("MERRA2"))
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
                
                if (dataToDownload.refType.Contains("MERRA2") && (dataToDownload.userName == null || dataToDownload.userPassword == null))
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

                if (thisInst.refList.HaveThisRefDataDownload(dataToDownload) == false)
                    thisInst.refList.AddRefDataDownload(dataToDownload);
                
            }

            if (needToDownload)
            {
                if (dataToDownload.refType.Contains("MERRA2"))
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
            cboDailyOrMonthly.Enabled = enable;

            if (enable && cboReanalysisType.SelectedItem.ToString() == "MERRA2")
            {
                cboDailyOrMonthly.SelectedIndex = 0;
                cboDailyOrMonthly.Enabled = false;
            }

            txtFolderLoc.Enabled = enable;       
            txtMinLat.Enabled = enable;
            txtMaxLat.Enabled = enable;
            txtMinLong.Enabled = enable;
            txtMaxLong.Enabled = enable;
        }

        /// <summary> Updates folder location, start/end dates, and textboxes showing Reference Download Data selected from list.  If it's
        /// new, then default start/end date, emtpy min max lat/long ranges, same data folder as last reference data of same type (if any). </summary>
        public void UpdateForm()
        {
            string selRefDataDownload = "";

            if (cboRefDataDownloads.SelectedItem != null)
                selRefDataDownload = cboRefDataDownloads.SelectedItem.ToString();

            if (selRefDataDownload == "" && dataToDownload.minLat == 0) // No defined reference downloads yet
            {
                cboReanalysisType.SelectedIndex = 0;
                EnableDisableControls(false); // Disable all controls (ie user needs to click 'New')
                return;
            }
            else if (selRefDataDownload == "Define New" && dataToDownload.minLat == 0)
            {
                // Enable all controls to let user define new reference data download
                EnableDisableControls(true);
                txtFolderLoc.Text = dataToDownload.folderLocation;
                dataToDownload.completion = 0;
                txtPercComplete.Text = Math.Round(dataToDownload.completion * 100.0, 1).ToString();
            }
            else if ((selRefDataDownload == "" && dataToDownload.minLat != 0) || (selRefDataDownload == "Define New" && dataToDownload.minLat != 0) 
                || (selRefDataDownload != "" && selRefDataDownload != "Define New"))
            {
                // Created RefDataDownload from selected folder or user selected ref data download from list

                if (selRefDataDownload != "" && selRefDataDownload != "Define New")
                    dataToDownload = thisInst.refList.GetRefDataDownloadByName(cboRefDataDownloads.SelectedItem.ToString());
                else
                    cboRefDataDownloads.Text = dataToDownload.GetName();
                                
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

                okToUpdate = false;
                txtMinLat.Text = dataToDownload.minLat.ToString();
                txtMaxLat.Text = dataToDownload.maxLat.ToString();
                txtMinLong.Text = dataToDownload.minLon.ToString();
                txtMaxLong.Text = dataToDownload.maxLon.ToString();

                cboReanalysisType.SelectedItem = dataToDownload.refType;                               
                
                dateReferenceStart.Value = dataToDownload.startDate;
                dateReferenceEnd.Value = dataToDownload.endDate;

                if (cboShowTime.SelectedItem.ToString() == "Local")
                {
                    int offset = thisInst.UTM_conversions.GetUTC_Offset(dataToDownload.minLat, dataToDownload.minLon);
                    dateReferenceStart.Value = dateReferenceStart.Value.AddHours(offset);
                    dateReferenceEnd.Value = dateReferenceEnd.Value.AddHours(offset);
                }

                okToUpdate = true;
                dataToDownload.completion = thisInst.refList.CalcDownloadedDataCompletion(dataToDownload);
                txtPercComplete.Text = Math.Round(dataToDownload.completion * 100.0, 4).ToString();

                if (dataToDownload.refType != "MERRA2 Cloud")
                {
                    chkInclOpParam1.Checked = dataToDownload.incl10mWS;
                    chkInclOpParam1.Visible = true;
                }
                else
                    chkInclOpParam1.Visible = false;

                if (dataToDownload.refType == "ERA5")
                {
                    chkInclOpParam2.Visible = true;
                    chkInclOpParam2.Checked = dataToDownload.incl10mGust;
                }
                else
                    chkInclOpParam2.Visible = false;

                btnDownloadMERRA2.BackColor = Color.MediumSeaGreen;

                if (dataToDownload.monthlyOrDaily == "Daily")
                    cboDailyOrMonthly.SelectedIndex = 0;
                else
                    cboDailyOrMonthly.SelectedIndex = 1;

                if (cboDailyOrMonthly.SelectedItem.ToString() == "Monthly")
                {
                    dateReferenceStart.CustomFormat = "MM/yyyy";
                    dateReferenceEnd.CustomFormat = "MM/yyyy";
                    okToUpdate = false; // By setting okToUpdate to false, this modification will not affect the actual start/end time of the refDataDownload
                    dateReferenceStart.Value = new DateTime(dateReferenceStart.Value.Year, dateReferenceStart.Value.Month, 1);
                    dateReferenceEnd.Value = new DateTime(dateReferenceEnd.Value.Year, dateReferenceEnd.Value.Month, 1);
                    okToUpdate = true;
                }
                else
                {
                    dateReferenceStart.CustomFormat = "MM/dd/yyyy HH:mm";
                    dateReferenceEnd.CustomFormat = "MM/dd/yyyy HH:mm";
                }

                if (dataToDownload.minLat != 0)
                    cboDailyOrMonthly.Enabled = false;
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

                if (dataToDownload.refType != null)
                {
                    DateRangeAndCompletion dateRngCompl = thisInst.refList.GetDataFileStartEndDateAndCompletion(thisInst.fbd_MERRAData.SelectedPath, dataToDownload.refType);

                    dataToDownload.startDate = dateRngCompl.startEnd[0];
                    dataToDownload.endDate = dateRngCompl.startEnd[1];
                }

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
                    dataToDownload = new RefDataDownload();
                    dataToDownload.refType = cboReanalysisType.SelectedItem.ToString();
                    cboRefDataDownloads.SelectedIndex = r;
                    return;
                }

            // Otherwise add "Define New" and select it
            cboRefDataDownloads.Items.Add("Define New");
            dataToDownload = new RefDataDownload();
            dataToDownload.refType = cboReanalysisType.SelectedItem.ToString();
            cboRefDataDownloads.SelectedIndex = cboRefDataDownloads.Items.Count - 1;           
            
        }

        private void dateReferenceStart_ValueChanged(object sender, EventArgs e)
        {
            // Update start date of selected RefDataDownload
            if (okToUpdate == false)
                return;                        

            if (cboShowTime.SelectedItem.ToString() == "Local")
            {
                int offset = thisInst.UTM_conversions.GetUTC_Offset(dataToDownload.minLat, dataToDownload.minLon);
                dataToDownload.startDate = dateReferenceStart.Value.AddHours(-offset);
            }
            else
                dataToDownload.startDate = dateReferenceStart.Value;

            double percCompl = thisInst.refList.CalcDownloadedDataCompletion(dataToDownload);
            dataToDownload.completion = percCompl;
            txtPercComplete.Text = Math.Round(percCompl * 100.0, 1).ToString();
        }

        private void dateReferenceEnd_ValueChanged(object sender, EventArgs e)
        {
            // Update end date of selected RefDataDownload. If it is a monthly download, update dataToDownload.endDate to end of month
            if (okToUpdate == false)
                return;

            if (cboDailyOrMonthly.SelectedItem.ToString() == "Daily")            
                dataToDownload.endDate = dateReferenceEnd.Value;  
            else // Monthly            
                dataToDownload.endDate = dateReferenceEnd.Value.AddMonths(1).AddHours(-1); // End date of a monthly download will be at last hour of month (UTC-0)
              
            if (cboShowTime.SelectedItem.ToString() == "Local")
            {
                int offset = thisInst.UTM_conversions.GetUTC_Offset(dataToDownload.minLat, dataToDownload.minLon);
                dataToDownload.endDate = dataToDownload.endDate.AddHours(-offset);
            }

            if (dataToDownload.startDate.Year != 1)
            {
                double percCompl = thisInst.refList.CalcDownloadedDataCompletion(dataToDownload);
                dataToDownload.completion = percCompl;
                txtPercComplete.Text = Math.Round(percCompl * 100.0, 1).ToString();
            }
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

            if (cboReanalysisType.SelectedItem.ToString().Contains("MERRA2"))
            {
                cboDailyOrMonthly.Text = "Daily";
                cboDailyOrMonthly.Enabled = false;
            }
            else            
                cboDailyOrMonthly.Enabled = true;

            UpdateOptionalParams();
        }

        private void cboDailyOrMonthly_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (okToUpdate == false)
                return;

            if (cboDailyOrMonthly.SelectedItem.ToString() == "Monthly")
            {                
                dateReferenceStart.CustomFormat = "MM/yyyy";
                dateReferenceEnd.CustomFormat = "MM/yyyy";
                okToUpdate = false; // By setting okToUpdate to false, this modification will not affect the actual start/end time of the refDataDownload
                dateReferenceStart.Value = new DateTime(dateReferenceStart.Value.Year, dateReferenceStart.Value.Month, 1);
                dateReferenceEnd.Value = new DateTime(dateReferenceEnd.Value.Year, dateReferenceEnd.Value.Month, 1);
                okToUpdate = true;
            }
            else
            {
                dateReferenceStart.CustomFormat = "MM/dd/yyyy HH:mm";
                dateReferenceEnd.CustomFormat = "MM/dd/yyyy HH:mm";
            }
        }

        /// <summary> Updates checkboxes on form to show optional downloads (if any) </summary>
        public void UpdateOptionalParams()
        {
            if (cboReanalysisType.SelectedItem.ToString() == "MERRA2")
            {
                chkInclOpParam1.Text = "10m WS";
                chkInclOpParam1.Visible = true;
                chkInclOpParam2.Visible = false; 
            }
            else if (cboReanalysisType.SelectedItem.ToString() == "ERA5")
            {
                chkInclOpParam1.Text = "10m WS";
                chkInclOpParam1.Visible = true;
                chkInclOpParam2.Text = "10m Wind Gust since previous post processing";
                chkInclOpParam2.Visible = true;
            }
            else if (cboReanalysisType.SelectedItem.ToString() == "MERRA2 Cloud")
            {
                chkInclOpParam1.Visible = false;
                chkInclOpParam2.Visible = false;
            }
        }

        private void cboShowTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            okToUpdate = false;
            dateReferenceStart.Value = dataToDownload.startDate;
            dateReferenceEnd.Value = dataToDownload.endDate;
            
            if (cboShowTime.SelectedItem.ToString() == "Local")
            {
                if (dataToDownload.minLat == 0)                
                    ReadLatLongs();
                                    
                int offset = thisInst.UTM_conversions.GetUTC_Offset(dataToDownload.minLat, dataToDownload.minLon);
                dateReferenceStart.Value = dateReferenceStart.Value.AddHours(offset);
                dateReferenceEnd.Value = dateReferenceEnd.Value.AddHours(offset);
            }

            okToUpdate = true;            
        }

        /// <summary> Tries to read min/max lat/long entries </summary>
        public void ReadLatLongs()
        {
            if (txtMinLat.Text != "")
                double.TryParse(txtMinLat.Text, out dataToDownload.minLat);

            if (txtMaxLat.Text != "")
                double.TryParse(txtMaxLat.Text, out dataToDownload.maxLat);

            if (txtMinLong.Text != "")
                double.TryParse(txtMinLong.Text, out dataToDownload.minLon);

            if (txtMaxLong.Text != "")
                double.TryParse(txtMaxLong.Text, out dataToDownload.maxLon);
        }

        /// <summary> Updates 'Show Time' dropdown so that Local time option only allowed after lat/long range has been defined </summary>
        public void UpdateShowTimeDropdown()
        {            
            if (dataToDownload.minLat == 0 || dataToDownload.minLon == 0)
            {
                cboShowTime.Enabled = false;
                cboShowTime.Text = "UTC-0";
            }
            else
                cboShowTime.Enabled = true;
        }

        private void txtMinLat_TextChanged(object sender, EventArgs e)
        {
            if (okToUpdate)
            {
                ReadLatLongs();
                UpdateShowTimeDropdown();
            }
        }

        private void txtMinLong_TextChanged(object sender, EventArgs e)
        {
            if (okToUpdate)
            {
                ReadLatLongs();
                UpdateShowTimeDropdown();
            }
        }
    }
}
