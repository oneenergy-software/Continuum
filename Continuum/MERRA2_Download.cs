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

namespace ContinuumNS
{
    public partial class MERRA2_Download : Form
    {
        Continuum thisInst;
        public Reference merraToDownload = new Reference();

        public MERRA2_Download(Continuum continuum)
        {
            InitializeComponent();
            thisInst = continuum;
            UpdateTextboxes();
        }

        private void btnDownloadMERRA2_Click(object sender, EventArgs e)
        {
            // Make sure that a folder has been selected
           
            if (merraToDownload.refDataFolder == "")
            {
                MessageBox.Show("You have to choose a folder to save the daily MERRA2 datafiles.  Click 'Change Folder' button to select folder");
                return;
            }

            if (dateMERRAEnd.Value > DateTime.Now)
            {
                MessageBox.Show("End date cannot be in the future.");
                return;
            }

            // Make sure that the lat/long range includes at least one node, min < max, and show message displaying number of nodes and time range selected to download (give chance to cancel)
            if (txtMinLat.Text == "" || txtMaxLat.Text == "" || txtMinLong.Text == "" || txtMaxLong.Text == "")
            {
                MessageBox.Show("Enter a latitude and longitude range to download MERRA2 data.");
                return;
            }

            double minLat = Convert.ToDouble(txtMinLat.Text);
            double maxLat = Convert.ToDouble(txtMaxLat.Text);
            double minLong = Convert.ToDouble(txtMinLong.Text);
            double maxLong = Convert.ToDouble(txtMaxLong.Text);

            minLat = Math.Round(minLat / 0.5, 0) * 0.5;
            maxLat = Math.Round(maxLat / 0.5, 0) * 0.5;
            minLong = Math.Round(minLong / 0.625) * 0.625;
            maxLong = Math.Round(maxLong / 0.625) * 0.625;

            int numLat = thisInst.refList.GetNumNodesInRange(minLat, maxLat, "Lat");
            int numLong = thisInst.refList.GetNumNodesInRange(minLong, maxLong, "Long");
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
                MessageBox.Show("There are no MERRA2 nodes that fall within specified latitude range: " + minLat.ToString() + " to " + maxLat.ToString());
                return;
            }
            if (numLong == 0)
            {
                MessageBox.Show("There are no MERRA2 nodes that fall within specified longitude range: " + minLong.ToString() + " to " + maxLong.ToString());
                return;
            }

            DialogResult goodToGo = MessageBox.Show("With a latitude range of " + minLat.ToString() + " to " + maxLat.ToString() + " and a longitude range of " + minLong.ToString()
                + " to " + maxLong.ToString() + ", a total of " + numNodes.ToString() + " nodes will be downloaded (" + numLat.ToString() + " along latitude and " + numLong
                + " along longitude. Do you want to continue?", "Continuum", MessageBoxButtons.YesNo);

            if (goodToGo == DialogResult.No)
                return;

            if (merraToDownload.earthdataUser == "" || merraToDownload.earthdataPwd == "")
            {
                // See if user credentials are saved in another MERRA2 object
                string[] userPsd = thisInst.refList.GetEarthDataUserPsd();

                if ((userPsd[0] != "" && userPsd[1] != "") && (userPsd[0] != null && userPsd[1] != null))
                {
                    merraToDownload.earthdataUser = userPsd[0];
                    merraToDownload.earthdataPwd = userPsd[1];
                }
                else
                {
                    NASA_LogIn nasaCreds = new NASA_LogIn();
                    nasaCreds.ShowDialog();

                    if (nasaCreds.goodToGo == false)
                        return;

                    try
                    {
                        merraToDownload.earthdataUser = nasaCreds.txtNASAUsername.Text;
                        merraToDownload.earthdataPwd = nasaCreds.txtNASAPassword.Text;
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Earthdata credentials");
                        return;
                    }
                }
            }

            thisInst.refList.NASA_LogInAsync(thisInst, this);
        }

        /// <summary> Updates textboxes showing MERRA2 data bounds on GUI. </summary>
        public void SetMERRA2LatLong(Continuum thisInst)
        {
            string MERRAfolder = thisInst.refList.GetReferenceDataFolder("MERRA2");

            if (MERRAfolder == null || MERRAfolder == "")
                return;

            // Check to see if MERRA folder exists
            bool folderExists = Directory.Exists(MERRAfolder);
            if (folderExists == false)
            {
                MERRAfolder = "";
                txtMinLat.Text = "";
                txtMaxLat.Text = "";
                txtMinLong.Text = "";
                txtMaxLong.Text = "";

                txtMinLat.Enabled = true;
                txtMaxLat.Enabled = true;
                txtMinLong.Enabled = true;
                txtMaxLong.Enabled = true;

                btnDownloadMERRA2.BackColor = Color.LightCoral;
                return;
            }

            // If there are files, check one and get min/max lat/long
            string[] MERRAfiles = Directory.GetFiles(MERRAfolder, "*.ascii");
            string line;

            if (MERRAfiles == null)
            {
                txtMinLat.Text = "";
                txtMaxLat.Text = "";
                txtMinLong.Text = "";
                txtMaxLong.Text = "";

                txtMinLat.Enabled = true;
                txtMaxLat.Enabled = true;
                txtMinLong.Enabled = true;
                txtMaxLong.Enabled = true;

                btnDownloadMERRA2.BackColor = Color.LightCoral;
                return;
            }


            if (MERRAfiles.Length == 0)
            {
                txtMinLat.Text = "";
                txtMaxLat.Text = "";
                txtMinLong.Text = "";
                txtMaxLong.Text = "";

                txtMinLat.Enabled = true;
                txtMaxLat.Enabled = true;
                txtMinLong.Enabled = true;
                txtMaxLong.Enabled = true;

                btnDownloadMERRA2.BackColor = System.Drawing.Color.LightCoral;
                return;
            }

            StreamReader file = new StreamReader(MERRAfiles[0]);

            char[] delims = { ',' };
            int numLats = 0;
            int numLongs = 0;
            double[] lats = new double[numLats];
            double[] longs = new double[numLongs];

            while ((line = file.ReadLine()) != null)
            {
                string[] substrings = line.Split(delims);

                if (substrings[0] == "lat") // read in all latitudes
                {
                    numLats = substrings.Length - 1;
                    Array.Resize(ref lats, numLats);

                    for (int i = 0; i < numLats; i++)
                        lats[i] = Convert.ToDouble(substrings[i + 1]);
                }

                if (substrings[0] == "lon") // read in all longitudes
                {
                    numLongs = substrings.Length - 1;
                    Array.Resize(ref longs, numLongs);

                    for (int i = 0; i < numLongs; i++)
                        longs[i] = Convert.ToDouble(substrings[i + 1]);
                }
            }

            txtMinLat.Text = lats[0].ToString();
            txtMaxLat.Text = lats[numLats - 1].ToString();
            txtMinLong.Text = longs[0].ToString();
            txtMaxLong.Text = longs[numLongs - 1].ToString();

            txtMinLat.Enabled = false;
            txtMaxLat.Enabled = false;
            txtMinLong.Enabled = false;
            txtMaxLong.Enabled = false;

            btnDownloadMERRA2.BackColor = Color.MediumSeaGreen;

            file.Close();
        }

        private void btnChangeFolder_Click(object sender, EventArgs e)
        {            
            MessageBox.Show("Please select folder containing reference data files.");

            if (thisInst.fbd_MERRAData.ShowDialog() == DialogResult.OK)
                merraToDownload.refDataFolder = thisInst.fbd_MERRAData.SelectedPath;
            else
                return;

            UpdateTextboxes();
            SetMERRA2LatLong(thisInst);
        }

        /// <summary> Updates textboxes showing the start and end date of downloaded data and folder location of data </summary>
        public void UpdateTextboxes()
        {
            // Update start/end dates and lat/long range

            DateTime[] startEndDates = merraToDownload.GetDataFileStartEndDate();

            if (startEndDates[0].Year > 1900)
            {
                dateMERRAStart.Value = startEndDates[0];
                dateMERRAEnd.Value = startEndDates[1];
            }

            txt_MERRA2_folder.Text = merraToDownload.refDataFolder;
            SetMERRA2LatLong(thisInst);
        }
    }
}
