using System;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary>Form used to define a new long-term reference site  </summary>
    public partial class AddEditReference : Form
    {
        /// <summary> New or Existing Reference object to be created or editted </summary>
        public Reference thisRef;
        /// <summary> Reference Collection </summary>
        public ReferenceCollection refList;
        MetCollection metList; // Used to extract reference data at nodes closest to met site lat/long coordinates
        UTM_conversion utmConv = new UTM_conversion();
        /// <summary> Set to true if all user inputs are ok </summary>
        public bool goodToGo = false;
        /// <summary> Set to false if form should not be updated </summary>
        public bool okToUpdate = false;
        
        /// <summary> Constructor.  Selects MERRA2 by default </summary>
        public AddEditReference(MetCollection metCollection, ReferenceCollection referenceList, UTM_conversion utmConversion, Reference newOrEditRef = null)
        {
            InitializeComponent();
            metList = metCollection;
            refList = referenceList;
            utmConv = utmConversion;
            okToUpdate = true;

            if (newOrEditRef == null) // If null then we're adding new reference.  
            {
                thisRef = new Reference();
                              
                // Set default values
                // Assign most recently downloaded reference data
                thisRef.refDataDownload = refList.refDataDownloads[refList.numRefDataDownloads - 1];
                thisRef.SetDefaultsForReferenceType();
                thisRef.numNodes = 1;

           //     int offset = utmConv.GetUTC_Offset(thisRef.refDataDownload.minLat, thisRef.refDataDownload.minLon);

                if (refList.numReferences > 0)
                {
                    // Set start and end dates to same as other references
                    thisRef.startDate = refList.reference[0].startDate;
                    thisRef.endDate = refList.reference[0].endDate;
              //      dateRefEnd.Value = thisRef.endDate;
                }
                else
                {
                    // Don't assign a start/end date since we don't know the coords and could get the wrong offset
          //          thisRef.startDate = thisRef.refDataDownload.startDate.AddHours(offset); // Make default starttime same as RefDataDownload start (stored in local time)                 
          //          thisRef.endDate = thisRef.refDataDownload.endDate.AddHours(offset);  // Set end date to last day of month
           //         dateRefEnd.Value = thisRef.endDate;
                }

                thisRef.isUserDefined = true; // Default to user-specified lat/lon

            }
            else // Editing reference
            {
                thisRef = newOrEditRef;
                cboShowTime.SelectedIndex = 0; // Reference start/end time stored as local
            }

            // Populate dropdown with all downloaded reference data
            UpdateDropdownReferenceDownloads();

            // Populate list of met sites (used to extract data at closest coordinates)
            if (newOrEditRef == null)
            {
                PopulateMetList();
                UpdateForm();
            }           

        }

        /// <summary> Updates list of refernce data downloads to choose from </summary>
        public void UpdateDropdownReferenceDownloads()
        {
            // Updates dropdown list showing all defined reference downloads
            cboRefDataDownloads.Items.Clear();

            for (int d = 0; d < refList.numRefDataDownloads; d++)
                cboRefDataDownloads.Items.Add(refList.refDataDownloads[d].GetName());

            if (thisRef.refDataDownload.minLat == 0)
                cboRefDataDownloads.SelectedIndex = cboRefDataDownloads.Items.Count - 1;
            else            
                cboRefDataDownloads.SelectedIndex = refList.GetReferenceDownloadIndex(thisRef.refDataDownload.GetName());
            
        }

        /// <summary> Updates textboxes and dropdown lists based on selected 'Reference Data Downloads' </summary>
        public void UpdateForm()
        {
            if (thisRef.numNodes == 1)
                cboNumNodes.SelectedIndex = 0;
            else if (thisRef.numNodes == 4)
                cboNumNodes.SelectedIndex = 1;
            else if (thisRef.numNodes == 16)
                cboNumNodes.SelectedIndex = 2;
                        
            txtWS_ScaleFact.Text = thisRef.WS_ScaleFactor.ToString();

            cboTargetMet.Items.Clear();

            for (int m = 0; m < metList.ThisCount; m++)
                cboTargetMet.Items.Add(metList.metItem[m].name);

            cboTargetMet.Items.Add("User-defined");

            if (thisRef.isUserDefined)
                cboTargetMet.SelectedIndex = cboTargetMet.Items.Count - 1;
            else if (thisRef.interpData.Coords.latitude != 0)
            {
                Met thisMet = metList.GetMetAtLatLon(thisRef.interpData.Coords.latitude, thisRef.interpData.Coords.longitude, utmConv);
                for (int m = 0; m < cboTargetMet.Items.Count; m++)
                    if (cboTargetMet.Items[m].ToString() == thisMet.name)
                    {
                        cboTargetMet.SelectedIndex = m;
                        break;
                    }
            }

            okToUpdate = false;
            dateRefStart.Value = thisRef.startDate; // Reference start/end stored in local time
            dateRefEnd.Value = thisRef.endDate;

            if (cboShowTime.SelectedItem.ToString() == "UTC-0" && thisRef.interpData.Coords.latitude != 0)
            {
                int offset = utmConv.GetUTC_Offset(thisRef.interpData.Coords.latitude, thisRef.interpData.Coords.longitude);
                
                dateRefStart.Value = dateRefStart.Value.AddHours(-offset);
                dateRefEnd.Value = dateRefEnd.Value.AddHours(-offset);
            }
            okToUpdate = true;
            
        }

        /// <summary> Populates dropdown list with met sites and selects first in list </summary>
        public void PopulateMetList()
        {
            cboTargetMet.Items.Clear();

            for (int m = 0; m < metList.ThisCount; m++)            
                cboTargetMet.Items.Add(metList.metItem[m].name);

            cboTargetMet.Items.Add("User-defined");
            cboTargetMet.SelectedIndex = 0;
            
        }

        /// <summary> Updates table with LT reference lat/longs and % download completion on LT Reference tab. </summary> 
        public void UpdateLT_RefNodesAndCompleteness()
        {
            dataLTRefNodes.Rows.Clear();
            txtRefDataFolder.Text = thisRef.refDataDownload.folderLocation;

            Reference.Node_Data[] refNodes = thisRef.GetAllNodesInFile();

            for (int m = 0; m < refNodes.Length; m++)
            {
                int rowInd = dataLTRefNodes.Rows.Add(refNodes[m].XY_ind.Lat.ToString());
                dataLTRefNodes.Rows[rowInd].Cells[1].Value = refNodes[m].XY_ind.Lon.ToString();
            }

            if (refNodes.Length == 0)
                return;

            dateLTRefAvailStart.Value = thisRef.refDataDownload.startDate;
            dateLTRefAvailEnd.Value = thisRef.refDataDownload.endDate;

            if (cboShowTime.SelectedItem.ToString() == "Local")
            {
                int offset = utmConv.GetUTC_Offset(thisRef.interpData.Coords.latitude, thisRef.interpData.Coords.longitude);

                dateLTRefAvailStart.Value = dateLTRefAvailStart.Value.AddHours(-offset);
                dateLTRefAvailEnd.Value = dateLTRefAvailEnd.Value.AddHours(-offset);
            }
            
            txtRefDataAvail.Text = Math.Round(thisRef.refDataDownload.completion * 100.0, 1).ToString();
                      
        }               
        
        private void cboNumNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisRef.numNodes = Convert.ToInt16(cboNumNodes.SelectedItem.ToString());
        }

        private void cboTargetMet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTargetMet.SelectedItem.ToString() == "User-defined")
            {
                txtReferenceLat.Enabled = true;
                txtReferenceLong.Enabled = true;
                thisRef.isUserDefined = true;

                if (thisRef.interpData.Coords.latitude != 0)
                {
                    txtReferenceLat.Text = thisRef.interpData.Coords.latitude.ToString();
                    txtReferenceLong.Text = thisRef.interpData.Coords.longitude.ToString();
                }
            }
            else
            {
                if (thisRef.interpData.Coords.latitude != 0)
                {
                    txtReferenceLat.Text = thisRef.interpData.Coords.latitude.ToString();
                    txtReferenceLong.Text = thisRef.interpData.Coords.longitude.ToString();
                }
                else
                {
                    Met thisMet = metList.GetMet(cboTargetMet.SelectedItem.ToString());
                    UTM_conversion.Lat_Long thisLL = utmConv.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                    txtReferenceLat.Text = Math.Round(thisLL.latitude, 3).ToString();
                    txtReferenceLong.Text = Math.Round(thisLL.longitude, 3).ToString();

                    if (thisRef.startDate.Year == 1989 && thisRef.endDate.Year == 2018) // defaults so update to refDataDownload.addhours
                    {
                        int offset = utmConv.GetUTC_Offset(thisLL.latitude, thisLL.longitude);
                        thisRef.startDate = thisRef.refDataDownload.startDate.AddHours(offset); // Make default starttime same as RefDataDownload start (stored in local time)                 
                        thisRef.endDate = thisRef.refDataDownload.endDate.AddHours(offset);  // Set end date to last day of month
                    }

                }

                txtReferenceLat.Enabled = false;
                txtReferenceLong.Enabled = false;
                thisRef.isUserDefined = false;                
            }
        }

        private void dateMERRAStart_ValueChanged(object sender, EventArgs e)
        {
            if (okToUpdate == false)
                return;

            if (cboShowTime.SelectedItem.ToString() == "UTC-0")
            {
                // Reference start date stored in local time. Reference data download starts in UTC-0. 
                int offset = utmConv.GetUTC_Offset(thisRef.refDataDownload.minLat, thisRef.refDataDownload.minLon);                                
                thisRef.startDate = dateRefStart.Value.AddHours(offset);
            }
            else            
                thisRef.startDate = dateRefStart.Value;
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Get entered coordinates and make sure that they are within the RefDataDown area

            double thisLat = 0;
            double thisLon = 0;

            double.TryParse(txtReferenceLat.Text, out thisLat);
            double.TryParse(txtReferenceLong.Text, out thisLon);

            if (thisLat == 0 || thisLon == 0)
            {
                goodToGo = false;
                MessageBox.Show("Enter a latitude and longitude for the reference site");
                return;
            }

            if (thisLat < thisRef.refDataDownload.minLat || thisLat > thisRef.refDataDownload.maxLat)
            {
                goodToGo = false;
                MessageBox.Show("Requested latitude is outside the bounds of the downloaded data.");
                return;
            }
            else if (thisLon < thisRef.refDataDownload.minLon || thisLon > thisRef.refDataDownload.maxLon)
            {
                goodToGo = false;
                MessageBox.Show("Requested longitude is outside the bounds of the downloaded data.");
                return;
            }                        

            int offset = utmConv.GetUTC_Offset(thisLat, thisLon);
            thisRef.Set_Interp_LatLon_Dates_Offset(thisLat, thisLon, offset, utmConv);
            thisRef.nodes = new Reference.Node_Data[thisRef.numNodes];
            bool gotCoords = thisRef.FindCoords(refList);

            if (gotCoords == false)
                return;

            // Compare requested start/end dates to downloaded start/end dates
            if (thisRef.startDate < thisRef.refDataDownload.startDate.AddHours(thisRef.interpData.UTC_offset))
            {
                MessageBox.Show("Requested start date cannot be before the start of the downloaded data range.");
                return;
            }

            if (thisRef.endDate > thisRef.refDataDownload.endDate.AddHours(thisRef.interpData.UTC_offset))
            {
                MessageBox.Show("Requested end date cannot be after the end of the downloaded data range.");
                return;
            }

            // Compare requested start/end dates to others in list
            bool sameLength = refList.IsSameLengthAsOtherRefs(thisRef);
            if (sameLength == false)
            {
                int diffRefInd = 0;

                for (int r = 0; r < refList.numReferences; r++)
                {
                    if (refList.reference[r].startDate != thisRef.startDate)
                    {
                        diffRefInd = r;
                        break;
                    }
                }

                DialogResult diffsAreOk = MessageBox.Show("The requested start/end dates are not the same as the other long-term references in the list which are " +
                    refList.reference[diffRefInd].startDate.ToString() + " - " + refList.reference[diffRefInd].endDate.ToString() + ".  Do you want to continue?", "Continuum 3", MessageBoxButtons.YesNo);

                if (diffsAreOk == DialogResult.No)
                {
                    goodToGo = false;
                    return;
                }
            }

            goodToGo = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            goodToGo = false;
            Close();
        }

        private void txtWS_ScaleFact_TextChanged(object sender, EventArgs e)
        {
            double newScaleFactor = 0;
            double.TryParse(txtWS_ScaleFact.Text, out newScaleFactor);

            if (newScaleFactor != 0 && newScaleFactor != thisRef.WS_ScaleFactor) 
                thisRef.WS_ScaleFactor = newScaleFactor;
        }

        private void dateRefEnd_ValueChanged(object sender, EventArgs e)
        {
            if (okToUpdate == false)
                return;

            if (cboShowTime.SelectedItem.ToString() == "UTC-0")
            {
                int offset = utmConv.GetUTC_Offset(thisRef.refDataDownload.minLat, thisRef.refDataDownload.minLon);
                thisRef.endDate = dateRefEnd.Value.AddHours(offset);
            }
            else
                thisRef.endDate = dateRefEnd.Value;
        }               

        private void cboRefDataDownloads_SelectedIndexChanged_1(object sender, EventArgs e)
        {            
            
            thisRef.refDataDownload = refList.GetRefDataDownloadByName(cboRefDataDownloads.SelectedItem.ToString());
            UpdateLT_RefNodesAndCompleteness();
            UpdateForm();
        }                

        private void btnDownloadHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click Tools -> Download Reanalysis Data to download reference data or to link project to folder with previously-downloaded " +
                "reference data");

        }

        private void txtReferenceLat_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddEditReference_Load(object sender, EventArgs e)
        {

        }

        private void cboShowTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateRefStart.Value = thisRef.startDate; // Reference dates stored in Local
            dateRefEnd.Value = thisRef.endDate;

            dateLTRefAvailStart.Value = thisRef.refDataDownload.startDate; // Reference Data Download stored in UTC-0
            dateLTRefAvailEnd.Value = thisRef.refDataDownload.endDate;

            int offset = utmConv.GetUTC_Offset(thisRef.interpData.Coords.latitude, thisRef.interpData.Coords.longitude);

            if (cboShowTime.SelectedItem.ToString() == "Local")
            {
                // Show Reference Data Download available data range in Local time
                dateLTRefAvailStart.Value = dateLTRefAvailStart.Value.AddHours(offset); 
                dateLTRefAvailEnd.Value = dateLTRefAvailEnd.Value.AddHours(offset); 
            }
            else // Show in UTC-0
            {                
                dateRefStart.Value = dateRefStart.Value.AddHours(-offset);
                dateRefEnd.Value = dateRefEnd.Value.AddHours(-offset);
            }
        }

        /// <summary> Updates 'Show Time' dropdown so that Local time option only allowed after lat/long range has been defined </summary>
        public void UpdateShowTimeDropdown()
        {
            if (thisRef.interpData.Coords.latitude == 0 || thisRef.interpData.Coords.longitude == 0)
            {
                cboShowTime.Enabled = false;
                cboShowTime.Text = "UTC-0";
            }
            else
                cboShowTime.Enabled = true;
        }

    }
}
