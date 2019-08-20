using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSGeo.GDAL;
using OSGeo.OGR;
using OSGeo.OSR;

namespace ContinuumNS
{
    [Serializable()]
    public class MERRACollection
    {
        public MERRA[] merraData = new MERRA[0];
        public int numMERRA_Nodes = 1; // OE Methodology 2017.1 uses closest MERRA2 node (future methodology to incorporate interpolated data)
        public DateTime startDate = new DateTime(1989, 1, 1, 0, 0, 0);
        public DateTime endDate = new DateTime(2018, 12, 31, 23, 0, 0);
        
        public struct minMaxReq
        {
            public double minReqLat;
            public double maxReqLat;
            public double minReqLong;
            public double maxReqLong;
        }

        public int numMERRA_Data
        {
            get
            {
                if (merraData == null)
                    return 0;
                else
                    return merraData.Length;
            }            
        }

        public void ClearMERRA()
        {
            merraData = new MERRA[0];
        }

        public void Set_Num_MERRA_Nodes(Continuum thisInst)
        {            
            if (merraData == null)
                numMERRA_Nodes = Convert.ToInt16(thisInst.cboNumMERRA_Nodes.SelectedItem.ToString());
            else if (numMERRA_Data != 0)
            {
                if (((thisInst.cboNumMERRA_Nodes.SelectedIndex == 0 && merraData[0].numMERRA_Nodes != 1) ||
                    (thisInst.cboNumMERRA_Nodes.SelectedIndex == 1 && merraData[0].numMERRA_Nodes != 4) ||
                    (thisInst.cboNumMERRA_Nodes.SelectedIndex == 2 && merraData[0].numMERRA_Nodes != 16)) && thisInst.okToUpdate == true && merraData[0].GotWindTS(thisInst.UTM_conversions))

                {                    
                    numMERRA_Nodes = Convert.ToInt16(thisInst.cboNumMERRA_Nodes.SelectedItem.ToString());
                    for (int i = 0; i < numMERRA_Data; i++)
                    {
                        Array.Resize(ref merraData[i].MERRA_Nodes, numMERRA_Nodes);
                        merraData[i].ClearAll(true);
                        // TO DO: Reload MERRA data
                    }                    
                                          

                }
                else
                    numMERRA_Nodes = Convert.ToInt16(thisInst.cboNumMERRA_Nodes.SelectedItem.ToString());
            }
            else
                numMERRA_Nodes = Convert.ToInt16(thisInst.cboNumMERRA_Nodes.SelectedItem.ToString());

            thisInst.ChangesMade();
        }        
        
        public MERRA.DecimalCoords[] GetRequiredMERRACoords(double latitude, double longitude)
        {
            MERRA.DecimalCoords[] theseReqCoords = new MERRA.DecimalCoords[numMERRA_Nodes];

            double minReqLat = Math.Round(latitude / 0.5, 0) * 0.5;
            double  minReqLong = Math.Round(longitude / 0.625) * 0.625;
            double maxReqLat = minReqLat;
            double maxReqLong = minReqLong;

            if (numMERRA_Nodes == 1)
            {                
                theseReqCoords[0].Lat = minReqLat;
                theseReqCoords[0].Lon = minReqLong;
            }
            else if (numMERRA_Nodes == 4)
            {                
                if (latitude > minReqLat)
                    maxReqLat = minReqLat + 0.5;
                else
                {
                    minReqLat = minReqLat - 0.5;
                    maxReqLat = minReqLat + 0.5;
                }
                
                if (longitude > minReqLong)
                    maxReqLong = minReqLong + 0.625;
                else
                {
                    minReqLong = minReqLong - 0.625;
                    maxReqLong = minReqLong + 0.625;
                }

                theseReqCoords[0].Lat = minReqLat;
                theseReqCoords[0].Lon = minReqLong;

                theseReqCoords[1].Lat = minReqLat;
                theseReqCoords[1].Lon = maxReqLong;

                theseReqCoords[2].Lat = maxReqLat;
                theseReqCoords[2].Lon = minReqLong;

                theseReqCoords[3].Lat = maxReqLat;
                theseReqCoords[3].Lon = maxReqLong;
            }
            else if (numMERRA_Nodes == 16)
            {
                if (latitude > minReqLat)
                {
                    minReqLat = minReqLat - 0.5;
                    maxReqLat = minReqLat + 3 * 0.5;
                }
                else
                {
                    minReqLat = minReqLat - 1;
                    maxReqLat = minReqLat + 3 * 0.5;
                }

                if (longitude > minReqLong)
                {
                    minReqLong = minReqLong - 0.625;
                    maxReqLong = minReqLong + 3 * 0.625;
                }
                else
                {
                    minReqLong = minReqLong - 2 * 0.625;
                    maxReqLong = minReqLong + 3 * 0.625;
                }

                int latInd = 0;
                int longInd = 0;

                for (int i = 0; i < numMERRA_Nodes; i++)
                {
                    theseReqCoords[i].Lat = minReqLat + 0.5 * latInd;
                    theseReqCoords[i].Lon = minReqLong + 0.625 * longInd;

                    latInd++;
                    if (latInd >= 4)
                    {
                        latInd = 0;
                        longInd++;
                    }
                }

            }

            return theseReqCoords;
        }

        public MERRA.DecimalCoords[] GetRequiredNewMERRANodeCoords(double latitude, double longitude, int offset, Continuum thisInst)
        {
            // Finds the min/max lat/long of MERRA nodes needed for specified latitude and longitude. Loops through existing MERRA data to see
            // if additional MERRA data is needed
            MERRA.DecimalCoords[] newRequiredMERRANodes = new MERRA.DecimalCoords[0];
            int numNewReqNodes = 0;

            MERRA.DecimalCoords[] theseRequiredNodes = GetRequiredMERRACoords(latitude, longitude);
            MERRA.DecimalCoords[] existingNodes = new MERRA.DecimalCoords[0];
            int numExistingNodes = 0;

            // Loop through required nodes and see what additional nodes are needed
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            using (var context = new Continuum_EDMContainer(connString))
            {
                var theseNodes = from N in context.MERRA_Node_table select N;

                foreach (var N in theseNodes)
                {
                    numExistingNodes++;
                    Array.Resize(ref existingNodes, numExistingNodes);
                    existingNodes[numExistingNodes - 1].Lat = N.latitude;
                    existingNodes[numExistingNodes - 1].Lon = N.longitude;
                }
            }

            for (int i = 0; i < numMERRA_Nodes; i++)
            {
                bool gotIt = false;
                for (int j = 0; j < numExistingNodes; j++)
                {
                    if (existingNodes[j].Lat == theseRequiredNodes[i].Lat && existingNodes[j].Lon == theseRequiredNodes[i].Lon)
                    {
                        gotIt = true;
                        break;
                    }
                }

                if (gotIt == false)
                {
                    numNewReqNodes++;
                    Array.Resize(ref newRequiredMERRANodes, numNewReqNodes);
                    newRequiredMERRANodes[numNewReqNodes - 1].Lat = theseRequiredNodes[i].Lat;
                    newRequiredMERRANodes[numNewReqNodes - 1].Lon = theseRequiredNodes[i].Lon;
                }

            }                                       

            return newRequiredMERRANodes;
        }

        public bool areSameMERRAData(MERRA merra1, MERRA merra2)
        {
            // Compares the two MERRA objects and returns true if they are identical
            bool areSame = true;

            if (merra1.MERRA_Nodes == null && merra2.MERRA_Nodes == null)
                return areSame;
            else if ((merra1.MERRA_Nodes == null && merra2.MERRA_Nodes != null) || (merra1.MERRA_Nodes != null && merra2.MERRA_Nodes == null))
                areSame = false;
            else if (merra1.MERRA_Nodes.Length != merra2.MERRA_Nodes.Length)
                areSame = false;

            return areSame;
        }        
        
        public MERRA GetMERRA(double latitude, double longitude)
        {
            // Returns MERRA object with specified latitude and longitude
            MERRA thisMERRA = new MERRA();

            for (int i = 0; i < numMERRA_Data; i++)
                if (merraData[i].interpData.Coords.Lat == Math.Round(latitude, 3) && merraData[i].interpData.Coords.Lon == Math.Round(longitude, 3))
                    thisMERRA = merraData[i];

            return thisMERRA;
        }

        public void AddMERRA_GetDataFromTextFiles(double thisLat, double thisLong, int offset, Continuum thisInst, Met thisMet)
        {
            // Adds new MERRA object to list. Figures out if additional MERRA nodes need to be uploaded from textfiles.
            // Runs MCP at Met site (if thisMet not null) if have all MERRA node data. Calls BW worker to uploaded additional data if needed.

            // Create new MERRA object and assign lat, long, and node lat/long            
            MERRA thisMERRA = new MERRA();
            thisMERRA.Set_Interp_LatLon_Dates_Offset(thisLat, thisLong, offset, thisInst);
            thisMERRA.numMERRA_Nodes = numMERRA_Nodes;
            thisMERRA.MERRA_Nodes = new MERRA.MERRA_Node_Data[numMERRA_Nodes];

            if (thisMet.name == null)
                thisMERRA.isUserDefined = true;

            if (numMERRA_Data > 0)
                thisMERRA.MERRAfolder = merraData[0].MERRAfolder;

            if (thisMERRA.MERRAfolder == "")
            {
                try
                {
                    MessageBox.Show("Please select folder containing MERRA2 data .ascii files.");
                    if (thisInst.fbd_MERRAData.ShowDialog() == DialogResult.OK)
                        thisMERRA.MERRAfolder = thisInst.fbd_MERRAData.SelectedPath;
                    else
                        return;
                }
                catch
                {
                    MessageBox.Show("Folder path not valid.", "", MessageBoxButtons.OK);
                    return;
                }
            }

            // Figure out if MERRA textfile has the necessary lat/long range and get MERRA node coordinates
            bool gotCoords = thisMERRA.Find_MERRA_Coords(); 

            if (gotCoords == false)
                return;                                                        

            thisMERRA.Get_Export_Params(thisInst);
                                   
            DialogResult doMCP = DialogResult.No;

            if (thisMet.name != null && (thisInst.metList.ThisCount == 1 || thisInst.metList.isMCPd == false))
                doMCP = MessageBox.Show("Do you want to conduct MCP at selected met?", "Continuum 3.0", MessageBoxButtons.YesNo);
            else if (thisMet.name != "" && thisInst.metList.ThisCount > 1 && thisInst.metList.isMCPd == true)
                doMCP = DialogResult.Yes;

            if (doMCP == DialogResult.Yes)
            {                
                thisInst.metList.isMCPd = true;
                thisInst.modelList.ClearAllExceptImported();
                thisInst.turbineList.ClearAllWSEsts();
                thisInst.turbineList.ClearAllGrossEsts();
                thisInst.turbineList.ClearAllNetEsts();
                thisInst.mapList.ClearAllMaps();
                thisInst.metPairList.ClearAll();
            }            
            
            // Figure out what MERRA nodes need to be downloaded
            MERRA.DecimalCoords[] requiredMERRANode = GetRequiredNewMERRANodeCoords(thisLat, thisLong, offset, thisInst);

            if (requiredMERRANode.Length != 0)
            {              

                MERRA.MERRA_Pull[] nodesToPull = new MERRA.MERRA_Pull[requiredMERRANode.Length];

                for (int i = 0; i < requiredMERRANode.Length; i++)
                {
                    nodesToPull[i].Coords.Lat = requiredMERRANode[i].Lat;
                    nodesToPull[i].Coords.Lon = requiredMERRANode[i].Lon;
                    nodesToPull[i].UTM = thisInst.UTM_conversions.LLtoUTM(nodesToPull[i].Coords.Lat, nodesToPull[i].Coords.Lon);
                }

                // Check to see that MERRA data files have required lat/long and assign XInd and YInd
                bool gotIndices = thisMERRA.GetMERRAPullXYIndices(ref nodesToPull);

                if (gotIndices == false)
                    return;

                BackgroundWork.Vars_for_MERRA Vars_for_MERRA = new BackgroundWork.Vars_for_MERRA();
                Vars_for_MERRA.thisInst = thisInst;
                Vars_for_MERRA.thisMERRA = thisMERRA;
                Vars_for_MERRA.MCP_type = thisInst.Get_MCP_Method();
                Vars_for_MERRA.thisMet = thisMet;
                Vars_for_MERRA.nodesToPull = nodesToPull;

                thisInst.BW_worker = new BackgroundWork();
                thisInst.BW_worker.Call_BW_MERRA2_Import(Vars_for_MERRA);
            }
            else
            {
                // Have all necessary MERRA nodes and user wants to do MCP so.... Run MCP!

                // Add MERRA object to list
                Array.Resize(ref merraData, numMERRA_Data + 1);
                merraData[numMERRA_Data - 1] = thisMERRA;
                thisMERRA.GetMERRADataFromDB(thisInst);
                thisMERRA.GetInterpData(thisInst.UTM_conversions);

                if (doMCP == DialogResult.Yes)
                {
                    thisMet.WSWD_Dists = new Met.WSWD_Dist[0];
                    thisInst.metList.RunMCP(ref thisMet, thisMERRA, thisInst, thisInst.Get_MCP_Method());
                    thisMet.CalcAllLT_WSWD_Dists(thisInst, thisMet.mcp.LT_WS_Ests); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)
                    thisInst.updateThe.AllTABs(thisInst);
                }
                else
                    thisInst.updateThe.MERRA_TAB(thisInst);
            }
        }
        
        public void deleteMERRA(double latitude, double longitude, Continuum thisInst)
        {
            // Deletes MERRA data from list
            int newCount = numMERRA_Data - 1;

            if (newCount > 0)
            {
                MERRA[] tempList = new MERRA[newCount]; // Create list of met towers that you//re keeping(so size one less than before)
                int tempIndex = 0;

                for (int i = 0; i < numMERRA_Data; i++)
                {
                    if (merraData[i].interpData.Coords.Lat != Math.Round(latitude, 3) && merraData[i].interpData.Coords.Lon != Math.Round(longitude, 3))
                    {
                        tempList[tempIndex] = merraData[i];
                        tempIndex++;
                    }
                }
                merraData = tempList;
            }
            else
            {
                merraData = new MERRA[0];
            }
        }

        public void DeleteAllMERRADataFromDB(Continuum thisInst)
        {
            // clears all MERRA data from DB
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            try
            {
                using (var ctx = new Continuum_EDMContainer(connString))
                {
                    ctx.Database.ExecuteSqlCommand("DELETE FROM MERRA_Node_table");
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void DeleteMERRANodeDataFromDB(double latitude, double longitude, Continuum thisInst)
        { 
            // Deletes MERRA Node data from DB
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);
            try
            {
                using (var context = new Continuum_EDMContainer(connString))
                {
                    var merra_db = from N in context.MERRA_Node_table where N.latitude == latitude & N.longitude == longitude select N;

                    foreach (var N in merra_db)
                        context.MERRA_Node_table.Remove(N);                                     

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
                return;
            }

        }

        public int GetLTRefLength(Continuum thisInst)
        {
            // returns number of hours in long-term ref data
            int thisLength = 0;

            TimeSpan timeSpan = endDate - startDate;
            thisLength = timeSpan.Days * 24 + timeSpan.Hours + 1;

            return thisLength;
        }

        public void ClearMERRA_Data(Continuum thisInst)
        {
            for (int i = 0; i < numMERRA_Data; i++)
            {
                merraData[i].Clear_MERRA2_Node_Data();
                merraData[i].interpData.TS_Data = new MERRA.Wind_TS_with_Prod[0];
            }
        }
        
        public void ApplyPC_ToAllMERRA(Continuum thisInst, TurbineCollection.PowerCurve powerCurve)
        {
            // Calculates MERRA2 energy production using power curve at each MERRA2 interpolated node
            
            for (int i = 0; i < numMERRA_Data; i++)
            {
                merraData[i].powerCurve = powerCurve;
                merraData[i].ApplyPC(ref merraData[i].interpData.TS_Data);
            }
        }

        public void ClearMERRA_ProdStats()
        {
            // Clears monthly and annual energy production estimates. Called after a power curve is deleted
            
            for (int i = 0; i < numMERRA_Data; i++)
            {
                merraData[i].Reset_MonthProdStats();
                merraData[i].Reset_AnnualProdStats();
                merraData[i].powerCurve.Clear();
            }
        }

        public bool GotMERRA(double latitude, double longitude)
        {
            // Returns true if have MERRA with specified latitude and longitude in list
            bool gotIt = false;

            for (int i = 0; i < numMERRA_Data; i++)
                if (merraData[i].interpData.Coords.Lat == Math.Round(latitude, 3) && merraData[i].interpData.Coords.Lon == Math.Round(longitude, 3))
                    gotIt = true;

            return gotIt;
        }
    }
}
