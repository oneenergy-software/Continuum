using System;
using System.Drawing;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary> GUI class that allows user to define map extent and grid resolution. Calls background worker to generate map. </summary>
    public partial class GenMap : Form
    {
        /// <summary> Minimum UTM X coordinate. </summary>
        public int minUTMX;
        /// <summary> Maximum UTM X coordinate. </summary>
        public int maxUTMX;
        /// <summary> Minimum UTM Y coordinate. </summary>
        public int minUTMY;
        /// <summary> Maximum UTM Y coordinate. </summary>
        public int maxUTMY;
        /// <summary> Grid resolution. </summary>
        public int gridReso;
        /// <summary> Number of grid points. </summary>
        public int numGrid;
        /// <summary> Map name. </summary>
        public string mapName = "";
        /// <summary> Number of grid points along X. </summary>
        public int numX;
        /// <summary> Number of grid points along Y. </summary>
        public int numY;
        /// <summary> Minimum allowable X. </summary>
        public int minUTMX_Limit;
        /// <summary> Minimum allowable Y. </summary>
        public int minUTMY_Limit;
        /// <summary> Maximum allowable X. </summary>
        public int maxUTMX_Limit;
        /// <summary> Maximum allowable Y. </summary>
        public int maxUTMY_Limit;
        /// <summary> Index of selected map type. </summary>
        public int whatToMap = 0;
        /// <summary> Name of power curve used in map (if AEP map). </summary>
        public string powerCurve = "";
        /// <summary> Mets used to generate the map. </summary>
        public Met[] metsUsed;
        /// <summary> Wind flow model parameters. </summary>
        public Model[] models;
        /// <summary> Continuum instance that called GenMap. </summary>
        public Continuum thisInst;
        /// <summary> True if map generated from time series data otherwise WS/WD frequency distributions(i.e.TAB files) are used to create map. </summary>
        public bool useTimeSeries;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Class initializer. </summary>
        public GenMap(Continuum continuum)
        {
            InitializeComponent();
            thisInst = continuum;

            if (thisInst.topo.useSepMod)
            {
                txtMap_FlowSep_Used.Text = "Flow Sep. model used";
                txtMap_FlowSep_Used.BackColor = Color.LightBlue;
            }
            else
            {
                txtMap_FlowSep_Used.Text = "Flow Sep. model NOT used";
                txtMap_FlowSep_Used.BackColor = Color.LightCoral;
            }

            if (thisInst.topo.useSR)
            {
                txtMap_LC_used.Text = "Roughness model used";
                txtMap_LC_used.BackColor = Color.MediumSeaGreen;
            }
            else
            {
                txtMap_LC_used.Text = "Roughness model NOT used";
                txtMap_LC_used.BackColor = Color.LightCoral;
            }

            thisInst.metList.AreAllMetsMCPd();
            if (thisInst.metList.allMCPd)
            {
                txtisMCPdGenMap.Text = "MCP'd Met data used";
                txtisMCPdGenMap.BackColor = Color.MediumOrchid;
            }
            else
            {
                txtisMCPdGenMap.Text = "Meas Met data used";
                txtisMCPdGenMap.BackColor = Color.LightCoral;
            }

        }        
        
        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }

        /// <summary> Updates textboxes with min/max UTMX/Y </summary>
        public void UpdateTextboxes()
        {
            txtMinUTMX.Text = minUTMX.ToString();
            txtMinUTMY.Text = minUTMY.ToString();
            txtMaxUTMX.Text = maxUTMX.ToString();
            txtMaxUTMY.Text = maxUTMY.ToString();
            txtNumPoints.Text = numGrid.ToString();
        }

        /*      public void UpdateLimits()
              {
                  if (gridReso == 0)
                      UpdateGridReso();

                  TopoInfo.Min_Max_Num minMaxX = new TopoInfo.Min_Max_Num();
                  minMaxX.min = thisInst.topo.topoNumXY.X.all.min;
                  minMaxX.max = thisInst.topo.topoNumXY.X.all.max;

                  TopoInfo.Min_Max_Num minMaxY = new TopoInfo.Min_Max_Num();
                  minMaxY.min = thisInst.topo.topoNumXY.Y.all.min;
                  minMaxY.max = thisInst.topo.topoNumXY.Y.all.max;

                  TopoInfo.TopoGrid minXY = thisInst.topo.GetClosestNodeFixedGrid(minMaxX.min, minMaxY.min, 250, 12000);
                  minUTMX_Limit = (int)minXY.UTMX;
                  minUTMY_Limit = (int)minXY.UTMY;

                  TopoInfo.TopoGrid maxXY = thisInst.topo.GetClosestNodeFixedGrid(minMaxX.max, minMaxY.max, 250, 12000);
                  maxUTMX_Limit = (int)maxXY.UTMX;
                  maxUTMY_Limit = (int)maxXY.UTMY;

                  if (minUTMX == 0)
                      minUTMX = minUTMX_Limit;

                  if (minUTMY == 0)
                      minUTMY = minUTMY_Limit;

                  if (maxUTMX == 0)
                      maxUTMX = maxUTMX_Limit;

                  if (maxUTMY == 0)
                      maxUTMY = maxUTMY_Limit;

                  UpdateNumPoints();
              }
      */

        /// <summary> Sets min/max UTMX/Y parameters to min/max UTMX/Y limits. </summary>
        public void GetBiggestArea()
        {
            minUTMX = minUTMX_Limit;
            maxUTMX = maxUTMX_Limit;
            minUTMY = minUTMY_Limit;
            maxUTMY = maxUTMY_Limit;
            UpdateNumPoints();
        }

        /// <summary> Finds largest allowable mapping area (min/max UTMX/Y limits) based on topo data and grid resolution. </summary>
        public void FindLargestArea()
        {
            // Find largest allowable mapping area
            int plotNumX = thisInst.topo.topoNumXY.X.plot.num;
            int plotNumY = thisInst.topo.topoNumXY.Y.plot.num;

            // First find min X ind
            int numValid = 0;
            int lastNumValid = 0;
            int minXInd = 0;

            while (minXInd < plotNumX)
            {
                for (int j = 0; j < plotNumY; j++)
                    if (thisInst.topo.topoElevs[minXInd, j] != -999)
                        numValid++;

                if (numValid <= lastNumValid && numValid > 0)
                {
                    minXInd = minXInd - 1;
                    break;
                }
                else
                {
                    lastNumValid = numValid;
                    minXInd++;
                }

                numValid = 0;
            }

            // Now find max X ind
            numValid = 0;
            lastNumValid = 0;
            int maxXInd = plotNumX - 1;

            while (maxXInd > 0)
            {
                for (int j = 0; j < plotNumY; j++)
                    if (thisInst.topo.topoElevs[maxXInd, j] != -999)
                        numValid++;

                if (numValid <= lastNumValid && numValid > 0)
                {
                    maxXInd = maxXInd + 1;
                    break;
                }
                else
                {
                    lastNumValid = numValid;
                    maxXInd--;
                }

                numValid = 0;
            }

            // Now find min Y ind
            numValid = 0;
            lastNumValid = 0;
            int minYInd = 0;

            while (minYInd < plotNumY)
            {
                for (int i = 0; i < plotNumX; i++)
                    if (thisInst.topo.topoElevs[i, minYInd] != -999)
                        numValid++;

                if (numValid <= lastNumValid && numValid > 0)
                {
                    minYInd = minYInd - 1;
                    break;
                }
                else
                {
                    lastNumValid = numValid;
                    minYInd++;
                }

                numValid = 0;
            }

            // Now find max Y ind
            numValid = 0;
            lastNumValid = 0;
            int maxYInd = plotNumY - 1;

            while (maxYInd > 0)
            {
                for (int i = 0; i < plotNumX; i++)
                    if (thisInst.topo.topoElevs[i, maxYInd] != -999)
                        numValid++;

                if (numValid <= lastNumValid && numValid > 0)
                {
                    maxYInd = maxYInd + 1;
                    break;
                }
                else
                {
                    lastNumValid = numValid;
                    maxYInd--;
                }

                numValid = 0;
            }

            minUTMX_Limit = (int)(thisInst.topo.topoNumXY.X.plot.min + minXInd * thisInst.topo.topoNumXY.X.plot.reso) + 12000;
            minUTMY_Limit = (int)(thisInst.topo.topoNumXY.Y.plot.min + minYInd * thisInst.topo.topoNumXY.Y.plot.reso) + 12000;
            maxUTMX_Limit = (int)(thisInst.topo.topoNumXY.X.plot.min + maxXInd * thisInst.topo.topoNumXY.X.plot.reso) - 12000;
            maxUTMY_Limit = (int)(thisInst.topo.topoNumXY.Y.plot.min + maxYInd * thisInst.topo.topoNumXY.Y.plot.reso) - 12000;

            TopoInfo.TopoGrid thisXY = thisInst.topo.GetClosestNodeFixedGrid(minUTMX_Limit, minUTMY_Limit, gridReso, 12000);
            minUTMX_Limit = (int)thisXY.UTMX;
            minUTMY_Limit = (int)thisXY.UTMY;
                        
            thisXY = thisInst.topo.GetClosestNodeFixedGrid(maxUTMX_Limit, maxUTMY_Limit, gridReso, 12000);
            maxUTMX_Limit = (int)thisXY.UTMX;
            maxUTMY_Limit = (int)thisXY.UTMY;

            numX = (int)Math.Round((double)(maxUTMX_Limit - minUTMX_Limit) / gridReso, 0);
            numY = (int)Math.Round((double)(maxUTMY_Limit - minUTMY_Limit) / gridReso, 0);

            maxUTMX_Limit = minUTMX_Limit + numX * gridReso;
            maxUTMY_Limit = minUTMY_Limit + numY * gridReso;
                        

        }

        /// <summary> Updates grid resolution as entered by user. </summary>
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

        /// <summary> Updates textbox to show total number of grid points. </summary>
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

        /// <summary> Reads selected model type (0 = UW map, 1 = DW map, 2 = WS map (site-calibrated), 3 = Gross AEP map (site-calibrated), 4 = WS map (default),
        /// 5 = Gross AEP map (default)) and selected mets to use in creating map. Gets models used to create map. </summary>
        public void GetMapSettings()
        {            
            double height = thisInst.modeledHeight;
            
            try
            {
                whatToMap = cboWhatToMap.SelectedIndex;
            }
            catch 
            {
                MessageBox.Show("Select parameter to map.", "Continuum 3");
                return;
            }          

            Map thisMap = null;
            // 0 = UW map, 1 = DW map, 2 = WS map (site-calibrated), 3 = Gross AEP map (site-calibrated), 4 = WS map (default), 5 = Gross AEP map (default)
            if (whatToMap == 0)
                mapName = "Upwind Exposure";
            else if (whatToMap == 1)
                mapName = "Downwind Exposure";
            else if (whatToMap == 2)
            {
                mapName = height + " m Wind Speed";                
            }
            else if (whatToMap == 3)
            {
                try
                {
                    powerCurve = cboPowerCrvs.SelectedItem.ToString();
                }
                catch 
                {
                    MessageBox.Show("No power Curves imported.  Cannot create map of Gross AEP.", "Continuum 3");
                    return;
                }
                mapName = "Gross AEP with " + powerCurve;                
            }

            int mapInd = 1;

            // Check to see if there are other maps of same type
            for (int i = 0; i < thisInst.mapList.ThisCount; i++)
            {
                thisMap = thisInst.mapList.mapItem[i];

                if (thisMap.modelType == whatToMap)
                    mapInd++;
            }

            if (mapInd > 1)
            {                                 
                mapName = mapName + " " + mapInd;                
            }
            
            // Check that map name hasn't been taken
            bool mapNameTaken = false;
            for (int i = 0; i < thisInst.mapList.ThisCount; i++)
                if (thisInst.mapList.mapItem[i].mapName == mapName)
                    mapNameTaken = true;

            while (mapNameTaken == true)
            {
                mapInd++;
                if (whatToMap >= 2)                                    
                    mapName = height + " m Wind Speed " + mapInd;                
                else                                    
                    mapName = mapName + " " + mapInd;                

                mapNameTaken = false;
                for (int i = 0; i < thisInst.mapList.ThisCount; i++)
                    if (thisInst.mapList.mapItem[i].mapName == mapName)
                        mapNameTaken = true;

            }

            if (metsUsed == null)
                metsUsed = new Met[0];

            for(int i = 0; i < chkMetsToUse.CheckedItems.Count; i++)
            {
                for (int j = 0; j < thisInst.metList.ThisCount; j++)
                {
                    if (chkMetsToUse.CheckedItems[i].ToString() == thisInst.metList.metItem[j].name)
                    {
                        int newCount = metsUsed.Length + 1;
                        Array.Resize(ref metsUsed, newCount);
                        metsUsed[newCount - 1] = thisInst.metList.metItem[j];
                    }
                }
            }

            if (cboUseTimeSeries.SelectedItem.ToString() == "Use Time Series")
                useTimeSeries = true;
            else
                useTimeSeries = false;
            
            if (thisInst.metList.isTimeSeries == false || thisInst.metList.allMCPd == false || useTimeSeries == false)
                models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight,false);                     
            
        }
                
        
        private void btnMinMax_Click(object sender, EventArgs e)
        {
            // Finds the largest area that can be mapped based on loaded topo data and updates Min/Max X/Y

            FindLargestArea();
            GetBiggestArea();
            UpdateTextboxes(); 
            
        }
        
        private void btnCoordsAllTurbs_Click(object sender, EventArgs e)
        {
            FindAreaAroundTurbines();
        }

        /// <summary> Finds map area that covers all loaded turbine sites and updates Min/Max X/Y </summary>
        public void FindAreaAroundTurbines()
        {            
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

            if (turbMinX > minUTMX) minUTMX = (int)Math.Round(turbMinX, 0);
            if (turbMinY > minUTMY) minUTMY = (int)Math.Round(turbMinY, 0);
            if (turbMaxX < maxUTMX || maxUTMX == 0) maxUTMX = (int)Math.Round(turbMaxX, 0);
            if (turbMaxY < maxUTMY || maxUTMY == 0) maxUTMY = (int)Math.Round(turbMaxY, 0);

            UpdateNumPoints();
            UpdateTextboxes();
        }
        
        private void cboWhatToMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if ( a Gross AEP map is selected from dropbown, the power curve dropdown is updated
            int selectedInd = 0;
             
            try {
                selectedInd = cboWhatToMap.SelectedIndex;
            }
            catch  {
                return;
            }

            if (selectedInd == 3) { // fill power curves
                int numPowerCurves = thisInst.turbineList.PowerCurveCount;

                if (numPowerCurves == 0)
                    cboPowerCrvs.Items.Clear();
                else {

                    for (int i = 0; i < numPowerCurves; i++) {
                        TurbineCollection.PowerCurve This_Pwr_Crv = thisInst.turbineList.powerCurves[i];
                        cboPowerCrvs.Items.Add(This_Pwr_Crv.name);
                    }
                }

                try {
                    cboPowerCrvs.SelectedIndex = 0;
                }
                catch  {
                    return;
                }
            }
            else {
                cboPowerCrvs.Text = "";
                cboPowerCrvs.Items.Clear();
            }
        }
        
        private void txtMapReso_TextChanged(object sender, EventArgs e)
        {
            // Recalculates number of grid points and updates txtNumPoints
            UpdateGridReso();
            FindLargestArea();
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

        private void btnGenMap_Click(object sender, EventArgs e)
        {
            GetMapSettings();
            thisInst.mapList.AddMap(mapName, minUTMX, minUTMY, gridReso, numX, numY, whatToMap, powerCurve, thisInst, false, new Wake_Model(), metsUsed, models, useTimeSeries);
            Close();
        }
    }
}
