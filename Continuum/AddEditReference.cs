using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary>Form used to define a new long-term reference site  </summary>
    public partial class AddEditReference : Form
    {
        public Reference thisRef;
        MetCollection metList;
        UTM_conversion utmConv = new UTM_conversion();
        public bool goodToGo = false;

        /// <summary> Constructor.  Selects MERRA2 by default </summary>
        public AddEditReference(MetCollection metCollection, ReferenceCollection refList, Reference newOrEditRef = null)
        {
            InitializeComponent();
            metList = metCollection;

            if (newOrEditRef == null) // Adding new reference.  Set to default settings and use folder location and
                                      // Earthdata username and password for last MERRA2 reference (if any)
            {
                thisRef = new Reference();

                if (refList.numReferences > 0)
                {
                    // Use same folder as MERRA2 reference
                    for (int r = refList.numReferences - 1; r >= 0; r--)
                    {
                        if (refList.reference[r].referenceType == "MERRA2")
                        {
                            thisRef.refDataFolder = refList.reference[r].refDataFolder;
                            thisRef.earthdataUser = refList.reference[r].earthdataUser;
                            thisRef.earthdataPwd = refList.reference[r].earthdataPwd;
                            break;
                        }    
                    }                    
                }

                if (thisRef.refDataFolder == "") // Didn't find another MERRA2 so set it to Desktop folder
                    thisRef.refDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                thisRef.referenceType = "MERRA2";
                thisRef.SetDefaultsForReferenceType();
                thisRef.numNodes = 1;
                thisRef.startDate = new DateTime(2002, 1, 1);
                DateTime lastmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 23, 0, 0);
                thisRef.endDate = lastmonth.AddMonths(-1).AddDays(-1); // Set end date to last day of month
                dateRefEnd.Value = thisRef.endDate;

                thisRef.isUserDefined = true; // Default to user-specified lat/lon

            }
            else // Editing reference
            {
                thisRef = newOrEditRef;
            }

            txtRefFolder.Text = thisRef.refDataFolder;
            UpdateLT_RefNodesAndCompleteness();

            if (thisRef.referenceType == "MERRA2")
                cboLTRefType.SelectedIndex = 0; 
            else
                cboLTRefType.SelectedIndex = 1;                        

            PopulateMetList();
        }

        /// <summary> Updates textboxes and dropdown lists </summary>
        public void UpdateForm()
        {
            txtRefFolder.Text = thisRef.refDataFolder;
            cboNumNodes.SelectedItem = thisRef.numNodes;
            txtWS_ScaleFact.Text = thisRef.WS_ScaleFactor.ToString();

            cboTargetMet.Items.Clear();

            for (int m = 0; m < metList.ThisCount; m++)
                cboTargetMet.Items.Add(metList.metItem[m].name);

            cboTargetMet.Items.Add("User-defined");

            if (thisRef.isUserDefined)
                cboTargetMet.SelectedIndex = cboTargetMet.Items.Count - 1;

            txtReferenceLat.Text = thisRef.interpData.Coords.latitude.ToString();
            txtReferenceLong.Text = thisRef.interpData.Coords.longitude.ToString();

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
                        
            Reference.Node_Data[] refNodes = thisRef.GetAllNodesInFile();

            for (int m = 0; m < refNodes.Length; m++)
            {
                int rowInd = dataLTRefNodes.Rows.Add(refNodes[m].XY_ind.Lat.ToString());
                dataLTRefNodes.Rows[rowInd].Cells[1].Value = refNodes[m].XY_ind.Lon.ToString();
            }

            if (refNodes.Length == 0)
                return;

            DateTime[] startEndDates = thisRef.GetDataFileStartEndDate();
            int offset = utmConv.GetUTC_Offset(refNodes[0].XY_ind.Lat, refNodes[0].XY_ind.Lon);

            if (startEndDates[0].Year != 1)
            {
                dateLTRefAvailStart.Value = startEndDates[0].AddHours(offset);
                dateLTRefAvailEnd.Value = startEndDates[1].AddHours(offset);

                double completePerc = thisRef.CalcDownloadedDataCompletion();

                if (completePerc > 100)
                    completePerc = 100;

                txtRefDataAvail.Text = Math.Round(completePerc, 1).ToString();
            }            
        }

        private void cboLTRefType_SelectedIndexChanged(object sender, EventArgs e)
        {            
            thisRef.referenceType = cboLTRefType.SelectedItem.ToString();

            thisRef.SetDefaultsForReferenceType();

        }

        private void btnChangeFolder_Click(object sender, EventArgs e)
        {
            if (fbdRefDataFolder.ShowDialog() == DialogResult.OK)
            {
                thisRef.refDataFolder = fbdRefDataFolder.SelectedPath;
                txtRefFolder.Text = thisRef.refDataFolder;
                UpdateLT_RefNodesAndCompleteness();

                if (dateRefStart.Value < dateLTRefAvailStart.Value)
                {
                    thisRef.startDate = dateLTRefAvailStart.Value;
                    dateRefStart.Value = dateLTRefAvailStart.Value;
                }

                if (dateRefEnd.Value > dateLTRefAvailEnd.Value)
                {
                    thisRef.endDate = dateLTRefAvailEnd.Value;
                    dateRefEnd.Value = dateLTRefAvailEnd.Value;
                }
            }
        }

        private void cboNumNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisRef.numNodes = Convert.ToInt16(cboNumNodes.SelectedItem.ToString());
        }

        private void cboTargetMet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTargetMet.SelectedItem.ToString() == "User-defined lat/long")
            {
                txtReferenceLat.Enabled = true;
                txtReferenceLong.Enabled = true;
            }
            else
            {
                Met thisMet = metList.GetMet(cboTargetMet.SelectedItem.ToString());
                UTM_conversion.Lat_Long thisLL = utmConv.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                txtReferenceLat.Text = thisLL.latitude.ToString();
                txtReferenceLong.Text = thisLL.longitude.ToString();
                txtReferenceLat.Enabled = false;
                txtReferenceLong.Enabled = false;
            }
        }

        private void dateMERRAStart_ValueChanged(object sender, EventArgs e)
        {
            thisRef.startDate = dateRefStart.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Check to see if data files have necessary nodes for interpolation

            // Figure out if reference textfile has the necessary lat/long range and get reference node coordinates

            double thisLat = Convert.ToDouble(txtReferenceLat.Text);
            double thisLon = Convert.ToDouble(txtReferenceLong.Text);

            int offset = utmConv.GetUTC_Offset(thisLat, thisLon);
            thisRef.Set_Interp_LatLon_Dates_Offset(thisLat, thisLon, offset, utmConv);
            bool gotCoords = thisRef.FindCoords();

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
    }
}
