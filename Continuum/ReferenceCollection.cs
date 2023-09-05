using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary> Reference object collection which contain interpolated (or closest if 1 node used) reanalysis time series data and site location. </summary>
    [Serializable()]
    public class ReferenceCollection
    {
        /// <summary> List of Reference objects which contain interpolated (or closest if 1 node used) reference time series data and site location. </summary>
        public Reference[] reference = new Reference[0];
        
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public int numReferences
        {
            get
            {
                if (reference == null)
                    return 0;
                else
                    return reference.Length;
            }
        }

        /// <summary> Clears all reference data. </summary>
        public void ClearReferences()
        {
            reference = new Reference[0];
        }

        /// <summary> Returns Reference object with specified type, latitude and longitude. </summary>
        public Reference[] GetAllRefsAtLatLong(double latitude, double longitude)
        {
            Reference[] theseRefs = new Reference[0];
            int numRefs = 0;

            for (int i = 0; i < numReferences; i++)
                if (reference[i].interpData.Coords.latitude == Math.Round(latitude, 3) && reference[i].interpData.Coords.longitude == Math.Round(longitude, 3))
                {
                    numRefs++;
                    Array.Resize(ref theseRefs, numRefs);
                    theseRefs[numRefs - 1] = reference[i];
                }

            return theseRefs;
        }

        /// <summary> Returns Reference object with specified type, latitude and longitude. </summary>
        public Reference GetReference(string refType, double latitude, double longitude, int numNodes)
        {
            Reference thisRef = new Reference();

            for (int i = 0; i < numReferences; i++)
                if (reference[i].referenceType == refType && reference[i].interpData.Coords.latitude == Math.Round(latitude, 3) 
                    && reference[i].interpData.Coords.longitude == Math.Round(longitude, 3) && reference[i].numNodes == numNodes)
                    thisRef = reference[i];

            return thisRef;
        }

        /// <summary> Returns Reference object with name. </summary>
        public Reference GetReferenceByName(string thisName, MetCollection metList, UTM_conversion utmConv)
        {
            Reference thisRef = new Reference();

            for (int i = 0; i < numReferences; i++)
                if (reference[i].GetName(metList, utmConv) == thisName)
                {
                    thisRef = reference[i];
                    break;
                }

            return thisRef;
        }

        /// <summary> Returns index of Reference object with name. </summary>
        public int GetReferenceIndexByName(string thisName, MetCollection metList, UTM_conversion utmConv)
        {
            int refInd = 0;

            for (int i = 0; i < numReferences; i++)
                if (reference[i].GetName(metList, utmConv) == thisName)
                {
                    refInd = i;
                    break;
                }

            return refInd;
        }

        /// <summary> Adds new Reference to list </summary>
        public void AddReference(Reference newRef)
        {
            // Add Reference object to list
            Array.Resize(ref reference, numReferences + 1);
            reference[numReferences - 1] = newRef;            
        }

        /// <summary> Gets reference data from locally downloaded textfiles </summary>
        public void GetDataFromTextFiles(Reference thisRef, Continuum thisInst)
        {
            // Figure out what additional nodes need to be downloaded
            UTM_conversion.Lat_Long[] requiredNodes = thisRef.GetRequiredNewNodeCoords(thisInst);

            if (requiredNodes.Length != 0)
            {
                Reference.RefData_Pull[] nodesToPull = new Reference.RefData_Pull[requiredNodes.Length];

                for (int i = 0; i < requiredNodes.Length; i++)
                {
                    nodesToPull[i].Coords.latitude = requiredNodes[i].latitude;
                    nodesToPull[i].Coords.longitude = requiredNodes[i].longitude;
                    nodesToPull[i].UTM = thisInst.UTM_conversions.LLtoUTM(nodesToPull[i].Coords.latitude, nodesToPull[i].Coords.longitude);
                }

                // Check to see that MERRA data files have required lat/long and assign XInd and YInd
                bool gotIndices = thisRef.GetPullXYIndices(ref nodesToPull);

                if (gotIndices == false)
                    return;

                BackgroundWork.Vars_for_ReferenceData_Extract Vars_for_RefData = new BackgroundWork.Vars_for_ReferenceData_Extract();
                Vars_for_RefData.thisInst = thisInst;
                Vars_for_RefData.thisRef = thisRef;
                Vars_for_RefData.nodesToPull = nodesToPull;

                thisInst.BW_worker = new BackgroundWork();
                thisInst.BW_worker.Call_BW_RefDataImport(Vars_for_RefData);
            }           
            
        }

        // Taking this out and broke into two functions: AddReference and GetDataFromTextFiles
        /// <summary> Adds new Reference object to list. Figures out if additional nodes need to be extracted from textfiles.
        ///    Runs MCP at Met site (if thisMet not null) if have all node data. Calls BW worker to upload additional data if needed. </summary>
  /*      public void AddReference_GetDataFromTextFiles(string refType, double thisLat, double thisLong, int numNodes, DateTime thisStart, DateTime thisEnd, int offset, 
            Continuum thisInst, Met thisMet, bool isTest)
        {
            // Create new Reference object and assign lat, long, and node lat/long            
            Reference thisRef = new Reference();
            thisRef.referenceType = refType;
            thisRef.Set_Interp_LatLon_Dates_Offset(thisLat, thisLong, offset, thisInst);
            thisRef.numNodes = numNodes;
            thisRef.nodes = new Reference.Node_Data[numNodes];
            thisRef.startDate = thisStart;
            thisRef.endDate = thisEnd;

            if (thisMet.name == null)
                thisRef.isUserDefined = true;

            if (thisRef.refDataFolder == "")
            {
                try
                {
                    if (thisRef.referenceType == "MERRA2")
                        MessageBox.Show("Please select folder containing MERRA2 data .ascii files.");
                    else if (thisRef.referenceType == "ERA5")
                        MessageBox.Show("Please select folder containing ERA5 data .nc files.");

                    if (thisInst.fbd_MERRAData.ShowDialog() == DialogResult.OK)
                        thisRef.refDataFolder = thisInst.fbd_MERRAData.SelectedPath;
                    else
                        return;

                    //SetMERRA2LatLong(thisInst);
                    thisInst.updateThe.LT_RefNodesAndCompleteness(thisInst);
                }
                catch
                {
                    MessageBox.Show("Folder path not valid.", "", MessageBoxButtons.OK);
                    return;
                }
            }

            // Figure out if MERRA textfile has the necessary lat/long range and get MERRA node coordinates
            bool gotCoords = thisRef.FindCoords();

            if (gotCoords == false)
                return;

            DialogResult doMCP = DialogResult.No;

            if (thisMet.name != null && isTest == false && (thisInst.metList.ThisCount == 1 || thisInst.metList.isMCPd == false))
                doMCP = MessageBox.Show("Do you want to conduct MCP at selected met?", "Continuum 3.0", MessageBoxButtons.YesNo);
            else if (thisMet.name != "" && isTest == false && thisInst.metList.ThisCount > 1 && thisInst.metList.isMCPd == true)
                doMCP = DialogResult.Yes;
            else if (isTest == true)
                doMCP = DialogResult.No;

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

            // Figure out what additional nodes need to be downloaded
            UTM_conversion.Lat_Long[] requiredNodes = thisRef.GetRequiredNewNodeCoords(thisLat, thisLong, thisInst);

            if (requiredNodes.Length != 0 && thisRef.referenceType == "MERRA2")
            {
                Reference.RefData_Pull[] nodesToPull = new Reference.RefData_Pull[requiredNodes.Length];

                for (int i = 0; i < requiredNodes.Length; i++)
                {
                    nodesToPull[i].Coords.latitude = requiredNodes[i].latitude;
                    nodesToPull[i].Coords.longitude = requiredNodes[i].longitude;
                    nodesToPull[i].UTM = thisInst.UTM_conversions.LLtoUTM(nodesToPull[i].Coords.latitude, nodesToPull[i].Coords.longitude);
                }

                // Check to see that MERRA data files have required lat/long and assign XInd and YInd
                bool gotIndices = thisRef.GetPullXYIndices(ref nodesToPull);

                if (gotIndices == false)
                    return;

                BackgroundWork.Vars_for_MERRA Vars_for_MERRA = new BackgroundWork.Vars_for_MERRA();
                Vars_for_MERRA.thisInst = thisInst;
                Vars_for_MERRA.thisMERRA = thisRef;
                Vars_for_MERRA.MCP_type = thisInst.Get_MCP_Method();
                Vars_for_MERRA.thisMet = thisMet;
                Vars_for_MERRA.nodesToPull = nodesToPull;

                thisInst.BW_worker = new BackgroundWork();
                thisInst.BW_worker.Call_BW_MERRA2_Import(Vars_for_MERRA);
            }
            else if (requiredNodes.Length != 0 && thisRef.referenceType == "ERA5")
            {
                MessageBox.Show("The downloaded ERA5 file does not contain all required nodes.  Download another file that includes all necessary " +
                    "nodes.");
                return;
            }
            else
            {
                // Have all necessary MERRA nodes and user wants to do MCP so.... Run MCP!

                // Add Reference object to list
                Array.Resize(ref reference, numReferences + 1);
                reference[numReferences - 1] = thisRef;
                thisRef.GetReferenceDataFromDB(thisInst);
                thisRef.GetInterpData(thisInst.UTM_conversions);

                if (doMCP == DialogResult.Yes)
                {
                    thisMet.WSWD_Dists = new Met.WSWD_Dist[0];
                    thisInst.metList.RunMCP(ref thisMet, thisRef, thisInst, thisInst.Get_MCP_Method());
                    thisMet.CalcAllLT_WSWD_Dists(thisInst, thisMet.mcp.LT_WS_Ests); // Calculates LT wind speed / wind direction distributions for using all day and using each season and each time of day (Day vs. Night)
                    thisInst.updateThe.AllTABs(thisInst);
                }
                else
                {
                    thisInst.updateThe.MERRA_Dropdowns(thisInst);
                    thisInst.updateThe.MERRA_TAB(thisInst);
                }

            }
        }
  */

        /// <summary> Returns true if two specified Reference objects are identical </summary>
        public bool AreSameReference(Reference ref1, Reference ref2)
        {
            bool areSameRef = false;

            if (ref1.interpData.Coords.latitude == ref2.interpData.Coords.latitude && ref1.interpData.Coords.longitude == ref2.interpData.Coords.longitude
                && ref1.numNodes == ref2.numNodes && ref1.referenceType == ref2.referenceType)
                areSameRef = true;

            return areSameRef;

        }


        /// <summary> Deletes interpolated reference data from list. </summary>        
        public void DeleteReference(Reference refToDelete)
        {
            int newCount = numReferences - 1;

            if (newCount > 0)
            {
                Reference[] tempList = new Reference[newCount]; // Create list of met towers that you//re keeping(so size one less than before)
                int tempIndex = 0;

                for (int i = 0; i < numReferences; i++)
                {
                    if (AreSameReference(refToDelete, reference[i]) == false)
                    {
                        tempList[tempIndex] = reference[i];
                        tempIndex++;
                    }
                }
                reference = tempList;
            }
            else
            {
                reference = new Reference[0];
            }
        }

        /// <summary> Clears all MERRA data from DB. </summary> 
        public void DeleteAllMERRADataFromDB(Continuum thisInst)
        {
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

        /// <summary> Deletes MERRA Node data with specified lat/long from DB. </summary> 
        public void DeleteMERRANodeDataFromDB(double latitude, double longitude, Continuum thisInst)
        {
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

        /// <summary> Clears all MERRA data from DB. </summary> 
        public void DeleteAllERADataFromDB(Continuum thisInst)
        {
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            try
            {
                using (var ctx = new Continuum_EDMContainer(connString))
                {
                    ctx.Database.ExecuteSqlCommand("DELETE FROM ERA_Node_table");
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary> Deletes MERRA Node data with specified lat/long from DB. </summary> 
        public void DeleteERANodeDataFromDB(double latitude, double longitude, Continuum thisInst)
        {
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);
            try
            {
                using (var context = new Continuum_EDMContainer(connString))
                {
                    var merra_db = from N in context.ERA_Node_table where N.latitude == latitude & N.longitude == longitude select N;

                    foreach (var N in merra_db)
                        context.ERA_Node_table.Remove(N);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
                return;
            }

        }              

        /// <summary> Clears all MERRA2 node and interpolated data. </summary> 
        public void ClearReferenceData()
        {
            for (int i = 0; i < numReferences; i++)
            {
                reference[i].Clear_Node_Data();
                reference[i].interpData.TS_Data = new Reference.Wind_TS_with_Prod[0];
            }
        }

        /// <summary> Clears monthly and annual energy production estimates. Called after a power curve is deleted. </summary> 
        public void ClearReferenceProdStats()
        {
            for (int i = 0; i < numReferences; i++)
            {
                reference[i].Reset_MonthProdStats();
                reference[i].Reset_AnnualProdStats();
                reference[i].powerCurve.Clear();
            }
        }

        /// <summary> Returns true if have refrence with specified latitude, longitude, reference type, num nodes in list. </summary> 
        public bool GotReference(Reference newRef, int indexToIgnore = -999)
        {
            bool gotIt = false;

            for (int i = 0; i < numReferences; i++)
                if (reference[i].interpData.Coords.latitude == newRef.interpData.Coords.latitude && 
                    reference[i].interpData.Coords.longitude == newRef.interpData.Coords.longitude && 
                    reference[i].referenceType == newRef.referenceType && reference[i].numNodes == newRef.numNodes && 
                    reference[i].startDate == newRef.startDate && reference[i].endDate == newRef.endDate && i != indexToIgnore)
                    gotIt = true;

            return gotIt;
        }

        /// <summary> Logs user into NASA's EarthData system and begins MERRA2 data download (in background worker). </summary> 
        public async Task NASA_LogInAsync(Continuum thisInst, MERRA2_Download merraDownload)
        {
            BackgroundWork.Vars_for_MERRA_Download Vars_for_MERRA = new BackgroundWork.Vars_for_MERRA_Download();
            Vars_for_MERRA.thisInst = thisInst;
            Vars_for_MERRA.thisMERRA = merraDownload;

            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_MERRA2_Download(Vars_for_MERRA);
        }

        /// <summary> Returns MERRA2 data file latitude index. </summary> 
        public int GetLatitudeIndex(double thisLat)
        {
            int latInd = (int)(2 * thisLat + 180);

            return latInd;
        }

        /// <summary> Returns MERRA2 data file longitude index. </summary> 
        public int GetLongitudeIndex(double thisLong)
        {
            int longInd = (int)(1.6 * thisLong + 288);

            return longInd;
        }

        /// <summary> Returns URL of MERRA2 data file to download. </summary> 
        public string GetMERRA2URL(DateTime thisDay, double minLat, double maxLat, double minLong, double maxLong)
        {
            int dateNum = 0;

            string thisYear = thisDay.Year.ToString();
            string thisMonth = thisDay.Month.ToString();

            if (thisDay.Month < 10)
                thisMonth = "0" + thisMonth;

            string thisDayStr = thisDay.Day.ToString();

            if (thisDay.Day < 10)
                thisDayStr = "0" + thisDayStr;

            if (thisDay.Year <= 1991)
                dateNum = 100;
            else if (thisDay.Year <= 2000)
                dateNum = 200;
            else if (thisDay.Year <= 2010)
                dateNum = 300;
            else if ((thisDay.Year == 2021 && thisDay.Month >= 6 && thisDay.Month <= 9) ||
                (thisDay.Year == 2020 && thisDay.Month == 9))
                dateNum = 401;
            else
                dateNum = 400;

            int minLatInd = GetLatitudeIndex(minLat);
            int maxLatInd = GetLatitudeIndex(maxLat);
            int minLongInd = GetLongitudeIndex(minLong);
            int maxLongInd = GetLongitudeIndex(maxLong);

            string URL = "https://goldsmr4.gesdisc.eosdis.nasa.gov/opendap/MERRA2/M2T1NXSLV.5.12.4/";
            URL += thisYear + "/" + thisMonth + "/";
            URL += "MERRA2_" + dateNum.ToString() + ".tavg1_2d_slv_Nx." + thisYear + thisMonth + thisDayStr + ".nc4.ascii?";
            URL += "T10M[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
            URL += "U50M[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
            URL += "V50M[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
            URL += "SLP[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
            URL += "PS[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
            URL += "lat[" + minLatInd + ":" + maxLatInd + "]," + "time[0:23]," + "lon[" + minLongInd + ":" + maxLongInd + "]";

            return URL;

        }      
              

        /// <summary> Updates textboxes showing MERRA2 data bounds on GUI. </summary>
   /*     public void SetMERRA2LatLong (Continuum thisInst)
        {
            if (MERRAfolder == null || MERRAfolder == "")
                return;

            // Check to see if MERRA folder exists
            bool folderExists = Directory.Exists(MERRAfolder);
            if (folderExists == false)
            {
                MERRAfolder = "";
                thisInst.txtMinLat.Text = "";
                thisInst.txtMaxLat.Text = "";
                thisInst.txtMinLong.Text = "";
                thisInst.txtMaxLong.Text = "";

                thisInst.txtMinLat.Enabled = true;
                thisInst.txtMaxLat.Enabled = true;
                thisInst.txtMinLong.Enabled = true;
                thisInst.txtMaxLong.Enabled = true;

                thisInst.btnDownloadMERRA2.BackColor = System.Drawing.Color.LightCoral;
                return;
            }

            // If there are files, check one and get min/max lat/long
            string[] MERRAfiles = Directory.GetFiles(MERRAfolder, "*.ascii");
            string line;

            if (MERRAfiles == null)
            {
                thisInst.txtMinLat.Text = "";
                thisInst.txtMaxLat.Text = "";
                thisInst.txtMinLong.Text = "";
                thisInst.txtMaxLong.Text = "";

                thisInst.txtMinLat.Enabled = true;
                thisInst.txtMaxLat.Enabled = true;
                thisInst.txtMinLong.Enabled = true;
                thisInst.txtMaxLong.Enabled = true;

                thisInst.btnDownloadMERRA2.BackColor = System.Drawing.Color.LightCoral;
                return;
            }


            if (MERRAfiles.Length == 0)
            {
                thisInst.txtMinLat.Text = "";
                thisInst.txtMaxLat.Text = "";
                thisInst.txtMinLong.Text = "";
                thisInst.txtMaxLong.Text = "";

                thisInst.txtMinLat.Enabled = true;
                thisInst.txtMaxLat.Enabled = true;
                thisInst.txtMinLong.Enabled = true;
                thisInst.txtMaxLong.Enabled = true;

                thisInst.btnDownloadMERRA2.BackColor = System.Drawing.Color.LightCoral;
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

            thisInst.txtMinLat.Text = lats[0].ToString();
            thisInst.txtMaxLat.Text = lats[numLats - 1].ToString();
            thisInst.txtMinLong.Text = longs[0].ToString();
            thisInst.txtMaxLong.Text = longs[numLongs - 1].ToString();

            thisInst.txtMinLat.Enabled = false;
            thisInst.txtMaxLat.Enabled = false;
            thisInst.txtMinLong.Enabled = false;
            thisInst.txtMaxLong.Enabled = false;

            thisInst.btnDownloadMERRA2.BackColor = System.Drawing.Color.MediumSeaGreen;

            file.Close();
        }
   */
              

        

        public int GetNumNodesInRange(double minDeg, double maxDeg, string latOrLong)
        {
            // Returns the number of nodes included between specified range (either "lat" or "lon")

            int numNodes = 0;
            int minInd = 0;
            int maxInd = 0;

            // Find lat/long index of min and max value
            if (latOrLong == "lat")
            {
                minInd = GetLatitudeIndex(minDeg);
                maxInd = GetLatitudeIndex(maxDeg);

            }
            else
            {
                minInd = GetLongitudeIndex(minDeg);
                maxInd = GetLongitudeIndex(maxDeg);
            }

            numNodes = maxInd - minInd;

            return numNodes;
        }

        /// <summary> Returns EarthData username and password of a MERRA2 object if it exists </summary>
        public string[] GetEarthDataUserPsd()
        {
            // Returns the username and password of NASA earth data credentials if saved in a MERRA object
            string[] userPsd = new string[2];

            for (int r = 0; r < numReferences; r++)
                if (reference[r].referenceType == "MERRA2" && reference[r].earthdataUser != "")
                {
                    userPsd[0] = reference[r].earthdataUser;
                    userPsd[1] = reference[r].earthdataPwd;
                }

            return userPsd;
        }

        /// <summary> Returns folder location of reference of same type if it exists </summary>
        public string GetReferenceDataFolder(string thisRefType)
        {
            // Returns the folder location of another reference object of the same type
            string refLocation = "";

            for (int r = 0; r < numReferences; r++)
                if (reference[r].referenceType == thisRefType && reference[r].refDataFolder != "")
                    refLocation = reference[r].refDataFolder;                    

            return refLocation;
        }

        /// <summary> Calculates and returns average wind rose </summary>
        public double[] CalcAvgWindRose(UTM_conversion utmConv, int numWD)
        {
            double[] avgRose = new double[numWD];

            if (reference.Length == 0)
                return avgRose;

            for (int r = 0; r < reference.Length; r++)
            {
                double[] thisRose = reference[r].Calc_Wind_Rose(100, 100, utmConv, numWD);

                for (int d = 0; d < thisRose.Length; d++)
                    avgRose[d] = avgRose[d] + thisRose[d];
            }

            for (int d = 0; d < numWD; d++)
                avgRose[d] = avgRose[d] / reference.Length;

            return avgRose;
        }

    }
}

