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
        

        /// <summary> Constructor.  Selects MERRA2 by default </summary>
        public AddEditReference(MetCollection metCollection, ReferenceCollection referenceList, UTM_conversion utmConversion, Reference newOrEditRef = null)
        {
            InitializeComponent();
            metList = metCollection;
            refList = referenceList;
            utmConv = utmConversion;

            if (newOrEditRef == null) // If null then we're adding new reference.  
            {
                thisRef = new Reference();
                              
                // Set default values
                // Assign most recently downloaded reference data
                thisRef.refDataDownload = refList.refDataDownloads[refList.numRefDataDownloads - 1];
                thisRef.SetDefaultsForReferenceType();
                thisRef.numNodes = 1;

                int offset = utmConv.GetUTC_Offset(thisRef.refDataDownload.minLat, thisRef.refDataDownload.minLon);

                thisRef.startDate = thisRef.refDataDownload.startDate; // Make default starttime same as RefDataDownload start .AddHours(offset);                 
                thisRef.endDate = thisRef.refDataDownload.endDate; //.AddHours(offset); // Set end date to last day of month
                dateRefEnd.Value = thisRef.endDate;

                thisRef.isUserDefined = true; // Default to user-specified lat/lon

            }
            else // Editing reference
            {
                thisRef = newOrEditRef;
            }

            // Populate dropdown with all downloaded reference data
            UpdateDropdownReferenceDownloads();

            // Populate list of met sites (used to extract data at closest coordinates)
            PopulateMetList();

            UpdateForm();

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

     //       txtReferenceLat.Text = thisRef.interpData.Coords.latitude.ToString();
     //       txtReferenceLong.Text = thisRef.interpData.Coords.longitude.ToString();

            dateRefStart.Value = thisRef.startDate;
            dateRefEnd.Value = thisRef.endDate;

        }

        /// <summary> Populates dropdown list with met sites and selects first in list </summary>
        public void PopulateMetList()
        {
            cboTargetMet.Items.Clear();

            for (int m = 0; m < metList.ThisCount; m++)            
                cboTargetMet.Items.Add(metList.metItem[m].name);

            cboTargetMet.Items.Add("User-defined lat/long");
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

            DateTime[] startEndDates = refList.GetDataFileStartEndDate(thisRef.refDataDownload.folderLocation, thisRef.refDataDownload.refType);
            int offset = utmConv.GetUTC_Offset(refNodes[0].XY_ind.Lat, refNodes[0].XY_ind.Lon);

            if (startEndDates[0].Year != 1)
            {
                dateLTRefAvailStart.Value = startEndDates[0].AddHours(offset);
                dateLTRefAvailEnd.Value = startEndDates[1].AddHours(offset);

                double completePerc = refList.CalcDownloadedDataCompletion(thisRef.refDataDownload);

                if (completePerc > 100)
                    completePerc = 100;

                txtRefDataAvail.Text = Math.Round(completePerc, 1).ToString();
            }            
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
            }
            else
            {
                Met thisMet = metList.GetMet(cboTargetMet.SelectedItem.ToString());
                UTM_conversion.Lat_Long thisLL = utmConv.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                txtReferenceLat.Text = Math.Round(thisLL.latitude, 3).ToString();
                txtReferenceLong.Text = Math.Round(thisLL.longitude, 3).ToString();
                txtReferenceLat.Enabled = false;
                txtReferenceLong.Enabled = false;
                thisRef.isUserDefined = false;                
            }
        }

        private void dateMERRAStart_ValueChanged(object sender, EventArgs e)
        {
            thisRef.startDate = dateRefStart.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Get entered coordinates and make sure that they are within the RefDataDown area

            double thisLat = Convert.ToDouble(txtReferenceLat.Text);
            double thisLon = Convert.ToDouble(txtReferenceLong.Text);

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
    }
}
