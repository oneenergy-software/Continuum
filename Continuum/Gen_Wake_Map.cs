using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuumNS
{
    public partial class GenWakeMap : Form
    {
        public GenWakeMap(Continuum continuum)
        {
            InitializeComponent();
            thisInst = continuum;

            if (thisInst.topo.useSepMod)
            {
                txt_FlowSep_Used.Text = "Flow Sep. model used";
                txt_FlowSep_Used.BackColor = Color.LightBlue;
            }
            else
            {
                txt_FlowSep_Used.Text = "Flow Sep. model NOT used";
                txt_FlowSep_Used.BackColor = Color.LightCoral;
            }

            if (thisInst.topo.useSR)
            {
                txtLC_Used.Text = "Roughness model used";
                txtLC_Used.BackColor = Color.MediumSeaGreen;
            }
            else
            {
                txtLC_Used.Text = "Roughness model NOT used";
                txtLC_Used.BackColor = Color.LightCoral;
            }

            thisInst.metList.AreAllMetsMCPd();
            if (thisInst.metList.allMCPd)
            {
                txtisMCPGenWakeMap.Text = "MCP'd Met data used";
                txtisMCPGenWakeMap.BackColor = Color.MediumOrchid;
            }
            else
            {
                txtisMCPGenWakeMap.Text = "Meas. Met data used";
                txtisMCPGenWakeMap.BackColor = Color.LightCoral;
            }

            chkMetsToUse.Items.Clear(); // Met list on Wake model dialog
            int metCount = thisInst.metList.ThisCount;
            
            if (metCount > 0)
            {
                Met thisMet = thisInst.metList.metItem[0];                
                for (int j = 0; j < metCount; j++) // Now repopulate it with met towers
                {
                    thisMet = thisInst.metList.metItem[j];                    
                    chkMetsToUse.Items.Add(thisMet.name, true);
                }
            }                       

        }

        public int minUTMX = 0; // Minimum UTM X coordinate of map
        public int maxUTMX = 0; // Maximum UTM X coordinate of map
        public int minUTMY = 0; // Minimum UTM Y coordinate of map
        public int maxUTMY = 0; // Maximum UTM X coordinate of map
        public int gridReso = 0; // Map grid resolution
        int numGrid = 0; // Total number of grid points
        public int numX = 0; // Grid size along X
        public int numY = 0; // Grid size along Y
        string mapName = "";
        Met[] metsUsed; // Mets used to create map
        int whatToMap = 3; // 3 for site-calibrated model; 5 for default model

        public int minUTMX_Limit; // Minimum allowable X
        public int minUTMY_Limit; // Minimum allowable Y
        public int maxUTMX_Limit; // Maximum allowable X
        public int maxUTMY_Limit; // Maximum allowable Y

        Model[] models; // Wind flow model parameters
        Wake_Model thisWakeModel = null;
        bool useTimeSeries; 

        Continuum thisInst;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void UpdateTextboxes()
        {
            txtMinUTMX.Text = minUTMX.ToString();
            txtMinUTMY.Text = minUTMY.ToString();
            txtMaxUTMX.Text = maxUTMX.ToString();
            txtMaxUTMY.Text = maxUTMY.ToString();
            txtNumPoints.Text = numGrid.ToString();
        }

        public void UpdateLimits()
        {
            if (gridReso == 0)
                UpdateGridReso();

            int minX_Lim = (int)thisInst.topo.topoNumXY.X.all.min + 12000;
            int minY_Lim = (int)thisInst.topo.topoNumXY.Y.all.min + 12000;
            int maxX_Lim = (int)thisInst.topo.topoNumXY.X.all.max - 12000;
            int maxY_Lim = (int)thisInst.topo.topoNumXY.Y.all.max - 12000;

            int minX_LimInd = (int)Math.Ceiling((minX_Lim - thisInst.topo.topoNumXY.X.all.min) / gridReso);
            int minY_LimInd = (int)Math.Ceiling((minY_Lim - thisInst.topo.topoNumXY.Y.all.min) / gridReso);
            int maxX_LimInd = (int)Math.Floor((maxX_Lim - thisInst.topo.topoNumXY.X.all.min) / gridReso);
            int maxY_LimInd = (int)Math.Floor((maxY_Lim - thisInst.topo.topoNumXY.Y.all.min) / gridReso);

            minUTMX_Limit = (int)thisInst.topo.topoNumXY.X.all.min + minX_LimInd * gridReso;
            minUTMY_Limit = (int)thisInst.topo.topoNumXY.Y.all.min + minY_LimInd * gridReso;
            maxUTMX_Limit = (int)thisInst.topo.topoNumXY.X.all.min + maxX_LimInd * gridReso;
            maxUTMY_Limit = (int)thisInst.topo.topoNumXY.Y.all.min + maxY_LimInd * gridReso;

            if (minUTMX < minUTMX_Limit || minUTMX == 0) minUTMX = minUTMX_Limit;
            if (minUTMY < minUTMY_Limit || minUTMY == 0) minUTMY = minUTMY_Limit;
            if (maxUTMX > maxUTMX_Limit || maxUTMX == 0) maxUTMX = maxUTMX_Limit;
            if (maxUTMY > maxUTMY_Limit || maxUTMY == 0) maxUTMY = maxUTMY_Limit;

            UpdateNumPoints();
        }

        public void GetBiggestArea()
        {
            minUTMX = minUTMX_Limit;
            maxUTMX = maxUTMX_Limit;
            minUTMY = minUTMY_Limit;
            maxUTMY = maxUTMY_Limit;
            UpdateNumPoints();
        }

        public void UpdateGridReso()
        {
            try
            {
                gridReso = Convert.ToInt16(txtMapReso.Text);
            }
            catch
            {
                MessageBox.Show("Invalid grid resolution.", "Continuum 3");
                txtMapReso.Text = gridReso.ToString();
                return;
            }
        }

        public void UpdateNumPoints()
        {
            if (gridReso != 0)
            {
                numX = (maxUTMX - minUTMX) / gridReso + 1;
                numY = (maxUTMY - minUTMY) / gridReso + 1;
                numGrid = numX * numY;
            }
            else
                numGrid = 0;

            txtNumPoints.Text = numGrid.ToString();
        }

        private void GetMapSettings()
        {
            // Reads in map settings from form
            
            // Check which model is selected (i.e. site-calibrated or default model)
            bool isCalibrated = false;
            whatToMap = 5; // default model
            
            if (thisInst.metList.ThisCount > 1)
            {
                isCalibrated = true;
                whatToMap = 3; // site-calibrated model
            }               

            // Get selected wake model
            
            try
            {
                thisWakeModel = thisInst.wakeModelList.wakeModels[cboWakeModels.SelectedIndex];
            }
            catch 
            {
                MessageBox.Show("Select a wake model to use.", "Continuum 3");
            }

            // Get mets to use in map generation
            int numMets = chkMetsToUse.CheckedItems.Count;
            metsUsed = new Met[numMets];
            
            for (int n = 0; n < numMets; n++)
            {
                for (int j = 0; j <= thisInst.metList.ThisCount - 1; j++)
                {
                    if (chkMetsToUse.CheckedItems[n].ToString() == thisInst.metList.metItem[j].name)
                    {
                        metsUsed[n] = thisInst.metList.metItem[j];                        
                        break;
                    }
                }
            }

            if (cboUseTimeSeries.SelectedItem.ToString() == "Use Time Series")
                useTimeSeries = true;
            else
                useTimeSeries = false;

            // Get wind flow models            
            if (thisInst.metList.isTimeSeries == false || thisInst.metList.isMCPd == false || useTimeSeries == false)
                models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisInst.radiiList.investItem[0].radius, thisInst.radiiList.investItem[3].radius,
                    isCalibrated, Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            else
                models = thisInst.modelList.GetAllModels(thisInst, thisInst.metList.GetMetsUsed());
                
            
            int Waked_map_ind = 0;

            for (int j = 0; j < thisInst.mapList.ThisCount; j++)
            {
                if (thisInst.mapList.mapItem[j].mapName.Substring(0, 5) == "Waked")
                    Waked_map_ind++;
            }

            mapName = "Waked Map " + (Waked_map_ind + 1);

            
        }

        private void btnGenMap_Click(object sender, EventArgs e)
        {
            GenerateWakeMap();
        }

        public void GenerateWakeMap()
        {
            // Gets map settings then adds map to list (which calls background worker to generate map)
            GetMapSettings();

            // Check to see if this wake loss has already been done
            WakeCollection.WakeGridMap This_Wake_grid = thisInst.wakeModelList.CreateNewWakeGrid(minUTMX, minUTMY, numX, numY, thisWakeModel);

            if (thisInst.wakeModelList.WakeGridExists(This_Wake_grid) == false)
                thisInst.wakeModelList.AddWakeGridMap(mapName, minUTMX, minUTMY, numX, numY, gridReso, thisWakeModel);

            thisInst.mapList.AddMap(mapName, minUTMX, minUTMY, gridReso, numX, numY, whatToMap, thisWakeModel.powerCurve.name,
                                thisInst, true, thisWakeModel, metsUsed, models, useTimeSeries);

            Close();
        }

        

        public void GetCoordsAroundTurbs()
        {
            // Finds Min/Max XY that contain all turbine sites and updates textboxes
            double turbMinX = 0;
            double turbMinY = 0;
            double turbMaxX = 0;
            double turbMaxY = 0;
            
            int numTurbines = thisInst.turbineList.TurbineCount;

            if (numTurbines == 0)
            {
                MessageBox.Show("No Turbine sites have been loaded.", "Continuum 3");
                return;
            }

            for (int i = 0; i < numTurbines; i++)
            {
                Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                if (i == 0)
                {
                    turbMinX = thisTurb.UTMX;
                    turbMinY = thisTurb.UTMY;
                    turbMaxX = thisTurb.UTMX;
                    turbMaxY = thisTurb.UTMY;
                }

                if (thisTurb.UTMX < turbMinX) turbMinX = thisTurb.UTMX;
                if (thisTurb.UTMY < turbMinY) turbMinY = thisTurb.UTMY;
                if (thisTurb.UTMX > turbMaxX) turbMaxX = thisTurb.UTMX;
                if (thisTurb.UTMY > turbMaxY) turbMaxY = thisTurb.UTMY;
            }

            // Make a buffer around turbines (maximum of 500 m or grid resolution 
            turbMinX = turbMinX - Math.Max(500, gridReso);
            turbMinY = turbMinY - Math.Max(500, gridReso);
            turbMaxX = turbMaxX + Math.Max(500, gridReso);
            turbMaxY = turbMaxY + Math.Max(500, gridReso);

            minUTMX = (int)Math.Round(turbMinX, 0);
            minUTMY = (int)Math.Round(turbMinY, 0);
            maxUTMX = (int)Math.Round(turbMaxX, 0);
            maxUTMY = (int)Math.Round(turbMaxY, 0);

            if (minUTMX < minUTMX_Limit) minUTMX = minUTMX_Limit;
            if (minUTMY < minUTMY_Limit) minUTMY = minUTMY_Limit;
            if (maxUTMX > maxUTMX_Limit) maxUTMX = maxUTMX_Limit;
            if (maxUTMY > maxUTMY_Limit) maxUTMY = maxUTMY_Limit;

            UpdateNumPoints();
            UpdateTextboxes();

        }


        private void btnCoordsAllTurbs_Click(object sender, EventArgs e)
        {
            // Finds Min/Max XY that cover all turbine sites
            GetCoordsAroundTurbs();
        }

        private void txtMapReso_TextChanged(object sender, EventArgs e)
        {
            // Updates number of grid points textbox
            // Recalculates number of grid points and updates txtNumPoints
            UpdateGridReso();
            UpdateLimits();
            UpdateNumPoints();
            UpdateTextboxes();
        }

        private void txtMinUTMX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMinUTMX.TextLength < 6)
                    return;

                minUTMX = Convert.ToInt32(txtMinUTMX.Text);
                if (minUTMX < minUTMX_Limit)
                {
                    MessageBox.Show("Entered Min UTMX is less than minimum allowed value. Resetting to minimum.", "Continuum 3");
                    minUTMX = minUTMX_Limit;
                }

                UpdateNumPoints();
                UpdateTextboxes();

            }
            catch { }
        }

        private void txtMaxUTMX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMaxUTMX.TextLength < 6)
                    return;

                maxUTMX = Convert.ToInt32(txtMaxUTMX.Text);
                if (maxUTMX > maxUTMX_Limit)
                {
                    MessageBox.Show("Entered Max UTMX is more than maximum allowed value. Resetting to maximum.", "Continuum 3");
                    maxUTMX = maxUTMX_Limit;
                }

                UpdateNumPoints();
                UpdateTextboxes();
            }
            catch { }
        }

        private void txtMinUTMY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMinUTMY.TextLength < 7)
                    return;

                minUTMY = Convert.ToInt32(txtMinUTMY.Text);
                if (minUTMY < minUTMY_Limit)
                {
                    MessageBox.Show("Entered Min UTMY is less than minimum allowed value. Resetting to minimum.", "Continuum 3");
                    minUTMY = minUTMY_Limit;
                }

                UpdateNumPoints();
                UpdateTextboxes();
            }
            catch { }
        }

        private void txtMaxUTMY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMaxUTMY.TextLength < 7)
                    return;

                maxUTMY = Convert.ToInt32(txtMaxUTMY.Text);
                if (maxUTMY > maxUTMY_Limit)
                {
                    MessageBox.Show("Entered Max UTMY is more than maximum allowed value. Resetting to maximum.", "Continuum 3");
                    maxUTMY = maxUTMY_Limit;
                }

                UpdateNumPoints();
                UpdateTextboxes();
            }
            catch { }
        }

        
    }
}
