using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuumNS
{
    public partial class TopoDataDownload : Form
    {
        
        public string openTopoAPI_Key;
        public Continuum thisInst;
        public OpenTopoDataset[] openTopoDatasets;

        public struct OpenTopoDataset
        {
            public string dataName;
            public string description;
            public string website;
            public int gridReso;
        }

        public TopoDataDownload(Continuum continuum)
        {
            InitializeComponent();
            CreateOpenTopoDatasets();
            thisInst = continuum;
            FindMinMaxCoords();
        }    
        
        public void CreateOpenTopoDatasets()
        {
            openTopoDatasets = new OpenTopoDataset[7];

            openTopoDatasets[0] = new OpenTopoDataset();
            openTopoDatasets[0].dataName = "SRTMGL3";
            openTopoDatasets[0].description = "Shuttle Radar Topography Mission (SRTM) Global";
            openTopoDatasets[0].website = "https://portal.opentopography.org/raster?opentopoID=OTSRTM.042013.4326.1";
            openTopoDatasets[0].gridReso = 90;

            openTopoDatasets[1] = new OpenTopoDataset();
            openTopoDatasets[1].dataName = "SRTMGL1";
            openTopoDatasets[1].description = "Shuttle Radar Topography Mission (SRTM) Global";
            openTopoDatasets[1].website = "https://portal.opentopography.org/raster?opentopoID=OTSRTM.082015.4326.1";
            openTopoDatasets[1].gridReso = 30;

            openTopoDatasets[2] = new OpenTopoDataset();
            openTopoDatasets[2].dataName = "AW3D30";
            openTopoDatasets[2].description = "ALOS World 3D";
            openTopoDatasets[2].website = "https://portal.opentopography.org/raster?opentopoID=OTALOS.112016.4326.2";
            openTopoDatasets[2].gridReso = 30;

            openTopoDatasets[3] = new OpenTopoDataset();
            openTopoDatasets[3].dataName = "NASADEM";
            openTopoDatasets[3].description = "Modernization of DEM model generated from SRTM";
            openTopoDatasets[3].website = "https://portal.opentopography.org/raster?opentopoID=OTSDEM.032021.4326.2";
            openTopoDatasets[3].gridReso = 30;

            openTopoDatasets[4] = new OpenTopoDataset();
            openTopoDatasets[4].dataName = "COP30";
            openTopoDatasets[4].description = "Copernicus DEM is Digital Surface Model which includes buildings and vegetation.";
            openTopoDatasets[4].website = "https://portal.opentopography.org/raster?opentopoID=OTSDEM.032021.4326.3";
            openTopoDatasets[4].gridReso = 30;

            openTopoDatasets[5] = new OpenTopoDataset();
            openTopoDatasets[5].dataName = "COP90";
            openTopoDatasets[5].description = "Copernicus DEM is Digital Surface Model which includes buildings and vegetation.";
            openTopoDatasets[5].website = "https://portal.opentopography.org/raster?opentopoID=OTSDEM.032021.4326.1";
            openTopoDatasets[5].gridReso = 90;

            openTopoDatasets[6] = new OpenTopoDataset();
            openTopoDatasets[6].dataName = "EU_DTM";
            openTopoDatasets[6].description = "European Digital Terrain Model";
            openTopoDatasets[6].website = "https://portal.opentopography.org/raster?opentopoID=OTSRTM.042013.4326.1";
            openTopoDatasets[6].gridReso = 30;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {            
            if (fbdTopoFile.ShowDialog() != DialogResult.OK)
                return;

            // If filename is still default, ask user if they want to specify
            if (txtTopoDownloadName.Text == "Topo Data Download")
            {
                DialogResult yesOrNo = MessageBox.Show("Do you want to use the default file name 'Topo Data Download'?", "", MessageBoxButtons.YesNo);

                if (yesOrNo == DialogResult.No)
                    return;
            }

            string fileName = txtTopoDownloadName.Text;
            if (fileName == "")
            {
                MessageBox.Show("Enter a filename for downloaded topography data");
                return;
            }

            openTopoAPI_Key = txtOpenTopoAPI.Text;
            if (openTopoAPI_Key.Length == 0)
            { 
                MessageBox.Show("OpenTopo API Key required");
                return;
            }

            double minLat = double.Parse(txtMinLat.Text);
            double maxLat = double.Parse(txtMaxLat.Text);
            double minLon = double.Parse(txtMinLon.Text);
            double maxLon = double.Parse(txtMaxLon.Text);

            if (minLat == 0)
            {
                MessageBox.Show("Enter minimum latitude of bounding box.");
                return;
            }

            if (minLon == 0)
            {
                MessageBox.Show("Enter minimum longitude of bounding box.");
                return;
            }

            if (maxLat == 0)
            {
                MessageBox.Show("Enter maximum latitude of bounding box.");
                return;
            }

            if (maxLon == 0)
            {
                MessageBox.Show("Enter maximum longitude of bounding box.");
                return;
            }

            string dataType = cboOpenTopoData.SelectedItem.ToString();
           
            string apiURL = "https://portal.opentopography.org/API/globaldem";
      //      string apiURL = "https://portal.opentopography.org/API/usgsdem";
      //      dataType = "USGS10m";
            string outputFormat = "GTiff";
            string queryUrl = $"{apiURL}?demtype={dataType}&south={minLat}&north={maxLat}&west={minLon}&east={maxLon}&outputFormat={outputFormat}&API_Key={openTopoAPI_Key}";

            using (HttpClient client = new HttpClient())
            {
                try
                {                    
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {openTopoAPI_Key}");                        
                    HttpResponseMessage response = await client.GetAsync(queryUrl);
                                        
                    if (response.IsSuccessStatusCode)
                    {
                        // Read content as byte array
                        byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                        // Check if response has a filename header
                        if (response.Content.Headers.ContentDisposition?.FileName != null)
                        {
                      //      string downloadedFileName = response.Content.Headers.ContentDisposition.FileName;
                            string filePath = System.IO.Path.Combine(fbdTopoFile.SelectedPath, fileName + ".tif");
                            System.IO.File.WriteAllBytes(filePath, fileBytes);
                            MessageBox.Show($"File downloaded and saved to: {filePath}");                          
                            

                        }
                        else
                        {
                            MessageBox.Show("File name not provided in response headers.");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Failed to download data. Status code: {response.Content.ReadAsStringAsync().Result}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

        }        

        private void btnFindBoundBox_Click(object sender, EventArgs e)
        {
            FindMinMaxCoords();

        }

        public void FindMinMaxCoords()
        {
            // Calculates the min/max latitude/longitude range based on mets and turbines and specified buffer (minimum 12 km)

            // Define bounding box parameters
            double minUTMX = 0;
            double maxUTMX = 0;
            double minUTMY = 0;
            double maxUTMY = 0;

            if (txtBoundBoxBuffer.Text == "")
                txtBoundBoxBuffer.Text = "12";

            int topoBuffer = Convert.ToInt16(txtBoundBoxBuffer.Text);

            // Loop through all met sites and turbine sites and find min/max UTMX/Y
            for (int m = 0; m < thisInst.metList.ThisCount; m++)
            {
                Met thisMet = thisInst.metList.metItem[m];

                if (minUTMX == 0 || thisMet.UTMX < minUTMX)
                    minUTMX = thisMet.UTMX;

                if (minUTMY == 0 || thisMet.UTMY < minUTMY)
                    minUTMY = thisMet.UTMY;

                if (maxUTMX == 0 || thisMet.UTMX > maxUTMX)
                    maxUTMX = thisMet.UTMX;

                if (maxUTMY == 0 || thisMet.UTMY > maxUTMY)
                    maxUTMY = thisMet.UTMY;
            }

            for (int t = 0; t < thisInst.turbineList.TurbineCount; t++)
            {
                Turbine thisTurb = thisInst.turbineList.turbineEsts[t];

                if (minUTMX == 0 || thisTurb.UTMX < minUTMX)
                    minUTMX = thisTurb.UTMX;

                if (minUTMY == 0 || thisTurb.UTMY < minUTMY)
                    minUTMY = thisTurb.UTMY;

                if (maxUTMX == 0 || thisTurb.UTMX > maxUTMX)
                    maxUTMX = thisTurb.UTMX;

                if (maxUTMY == 0 || thisTurb.UTMY > maxUTMY)
                    maxUTMY = thisTurb.UTMY;
            }

            minUTMX = minUTMX - topoBuffer * 1000;
            maxUTMX = maxUTMX + topoBuffer * 1000;
            minUTMY = minUTMY - topoBuffer * 1000;
            maxUTMY = maxUTMY + topoBuffer * 1000;

            UTM_conversion.Lat_Long minLL = thisInst.UTM_conversions.UTMtoLL(minUTMX, minUTMY);
            UTM_conversion.Lat_Long maxLL = thisInst.UTM_conversions.UTMtoLL(maxUTMX, maxUTMY);

            txtMinLat.Text = Math.Round(minLL.latitude, 4).ToString();
            txtMinLon.Text = Math.Round(minLL.longitude, 4).ToString();
            txtMaxLat.Text = Math.Round(maxLL.latitude, 4).ToString();
            txtMaxLon.Text = Math.Round(maxLL.longitude, 4).ToString();
        }

        public OpenTopoDataset GetOponTopoData(string dataName)
        {
            OpenTopoDataset openTopoData = new OpenTopoDataset();
            
            for (int i = 0; i < openTopoDatasets.Length; i++)
                if (openTopoDatasets[i].dataName == dataName)
                {
                    openTopoData = openTopoDatasets[i];
                    break;
                }

            return openTopoData;
        }

        private void cboOpenTopoData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOpenTopoData.SelectedItem == null)
            {
                cboOpenTopoData.SelectedIndex = 0;
                return;
            }

            OpenTopoDataset selOpenTopo = GetOponTopoData(cboOpenTopoData.SelectedItem.ToString());
            lblDesc.Text = selOpenTopo.description;
            lblOpenTopoLink.Text = selOpenTopo.website;
            txtGridReso.Text = selOpenTopo.gridReso.ToString();

        }

        private void TopoDataDownload_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
