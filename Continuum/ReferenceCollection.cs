using Microsoft.Research.Science.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Research.Science.Data.Imperative;
using System.Runtime.CompilerServices;
using Python.Runtime;

namespace ContinuumNS
{
    /// <summary> Reference object collection which contain interpolated (or closest if 1 node used) reanalysis time series data and site location. </summary>
    [Serializable()]
    public class ReferenceCollection
    {
        /// <summary> List of Reference objects which contain interpolated (or closest if 1 node used) reference time series data and site location. </summary>
        public Reference[] reference = new Reference[0];

        /// <summary> List of RefDataDownloads which contain info of all downloaded reference data </summary>
        public RefDataDownload[] refDataDownloads = new RefDataDownload[0];

        /// <summary> Struct to hold info related to reference data downloads </summary>
        [Serializable()]
        public struct RefDataDownload
        {
            /// <summary> Struct to hold info related to reference data downloads </summary>
            public string refType; // MERRA2 or ERA5
            /// <summary> File folder location of daily reference data files </summary>
            public string folderLocation;
            /// <summary> Username for download access </summary>
            public string userName;
            /// <summary> Password for download access </summary>
            public string userPassword;
            /// <summary> Specified start date of reference data (UTC-0) </summary>
            public DateTime startDate;
            /// <summary> Specified end date of reference data (UTC-0) </summary>
            public DateTime endDate;
            /// <summary> Download completion percent </summary>
            public double completion;
            /// <summary> Min latitude of downloaded nodes in files </summary>     
            public double minLat;
            /// <summary> Max latitude of downloaded nodes in files </summary>
            public double maxLat;
            /// <summary> Min longitude of downloaded nodes in files </summary>
            public double minLon;
            /// <summary> Max longitude of downloaded nodes in files </summary>
            public double maxLon;
            /// <summary> Flag specifying whether to download daily or monthly data </summary>
            public string monthlyOrDaily;

            public bool incl10mWS;

            public bool incl10mGust; // Applies to ERA5 only

            public bool inclCloud; // Applies to MERRA2 only

            /// <summary> Creates and returns name for specified reference data download </summary>
            public string GetName()
            {               
                string refDownName = refType + " Lat range: " + minLat.ToString() + " to " + maxLat.ToString() + ", Long range: " +
                    minLon.ToString() + " to " + maxLon.ToString();

                return refDownName;
            }
        }


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

        /// <summary> Returns number of reference data download objects in list. </summary>
        public int numRefDataDownloads
        {
            get
            {
                if (refDataDownloads == null)
                    return 0;
                else
                    return refDataDownloads.Length;
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

        /// <summary> Returns reference with specified latitude, longitude, and number of nodes </summary>
        
        public Reference GetRefAtLatLongNumNodes(double latitude, double longitude, int numNodes)
        {
            Reference thisRef = new Reference();
            
            for (int i = 0; i < numReferences; i++)
                if (reference[i].numNodes == numNodes && reference[i].interpData.Coords.latitude == Math.Round(latitude, 3) 
                    && reference[i].interpData.Coords.longitude == Math.Round(longitude, 3))
                {                    
                    thisRef = reference[i];
                    break;
                }

            return thisRef;
        }

        /*       /// <summary> Returns Reference object with specified type, latitude and longitude. </summary>
              public Reference GetReference(string refType, double latitude, double longitude, int numNodes)
               {
                   Reference thisRef = new Reference();

                   for (int i = 0; i < numReferences; i++)
                       if (reference[i].referenceType == refType && reference[i].interpData.Coords.latitude == Math.Round(latitude, 3) 
                           && reference[i].interpData.Coords.longitude == Math.Round(longitude, 3) && reference[i].numNodes == numNodes)
                           thisRef = reference[i];

                   return thisRef;
               }
        */
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

        /// <summary> Get Reference with closest lat and long  </summary>        
        public Reference GetReferenceByUTM(double thisUTMX, double thisUTMY, string refType)
        {
            Reference thisRef = new Reference();            
            double closestCoord = 1000;

            for (int i = 0; i < numReferences; i++)
            {
                if (refType != reference[i].refDataDownload.refType)
                    continue;

                double thisDist = Math.Abs(thisUTMX - reference[i].interpData.UTM.UTMEasting) + Math.Abs(thisUTMY - reference[i].interpData.UTM.UTMNorthing);

                if (thisDist < closestCoord)
                {                    
                    closestCoord = thisDist;
                    thisRef = reference[i];
                }
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

            if (ref1.isUserDefined == ref2.isUserDefined && ref1.interpData.Coords.latitude == ref2.interpData.Coords.latitude && ref1.interpData.Coords.longitude == ref2.interpData.Coords.longitude
                && ref1.numNodes == ref2.numNodes && IsSameRefDataDownload(ref1.refDataDownload, ref2.refDataDownload))
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

        /// <summary> Deletes specified reference data download from list. </summary>        
        public void DeleteRefDataDownload(RefDataDownload refDownToDelete)
        {
            int newCount = numRefDataDownloads - 1;

            if (newCount > 0)
            {
                RefDataDownload[] tempList = new RefDataDownload[newCount]; 
                int tempIndex = 0;

                for (int i = 0; i < numRefDataDownloads; i++)
                {
                    if (refDownToDelete.GetName() != refDataDownloads[i].GetName())
                    {
                        tempList[tempIndex] = refDataDownloads[i];
                        tempIndex++;
                    }
                }
                refDataDownloads = tempList;
            }
            else
            {
                refDataDownloads = new RefDataDownload[0];
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

        /// <summary> Clears all ERA5 data from DB. </summary> 
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

        /// <summary> Deletes ERA5 Node data with specified lat/long from DB. </summary> 
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

        /// <summary> Clears all reference node and interpolated data.  This is called when saving file (so the generated time series data is not saved to DB. </summary> 
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
                    IsSameRefDataDownload(reference[i].refDataDownload, newRef.refDataDownload) && reference[i].numNodes == newRef.numNodes && 
                    reference[i].startDate == newRef.startDate && reference[i].endDate == newRef.endDate && i != indexToIgnore)
                    gotIt = true;

            return gotIt;
        }

        /// <summary> Logs user into NASA's EarthData system and begins MERRA2 data download (in background worker). </summary> 
        public async Task NASA_LogInAsync(Continuum thisInst, ReferenceCollection.RefDataDownload merraDownload)
        {
            BackgroundWork.Vars_for_Reference_Download Vars_for_MERRA = new BackgroundWork.Vars_for_Reference_Download();
            Vars_for_MERRA.thisInst = thisInst;
            Vars_for_MERRA.thisRefDownload = merraDownload;

            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_MERRA2_Download(Vars_for_MERRA);
        }

        /// <summary> Calls ERA5 data download (in background worker). </summary> 
        public async Task DownloadERA5(Continuum thisInst, ReferenceCollection.RefDataDownload era5Download)
        {
            BackgroundWork.Vars_for_Reference_Download Vars_for_ERA5 = new BackgroundWork.Vars_for_Reference_Download();
            Vars_for_ERA5.thisInst = thisInst;
            Vars_for_ERA5.thisRefDownload = era5Download;

            thisInst.BW_worker = new BackgroundWork();
            thisInst.BW_worker.Call_BW_ERA5_Download(Vars_for_ERA5);
        }

        /// <summary> Returns latitude resolution of specified RefDataDownload. </summary> 
        public double GetLatRes(RefDataDownload thisRefData)
        {
            double latRes = 0;

            if (thisRefData.refType == "MERRA2")
                latRes = 0.5;
            else if (thisRefData.refType == "ERA5")
                latRes = 0.25;

            return latRes;
        }

        /// <summary> Returns longitude resolution of specified RefDataDownload. </summary> 
        public double GetLongRes(RefDataDownload thisRefData)
        {
            double lonRes = 0;

            if (thisRefData.refType == "MERRA2")
                lonRes = 0.625;
            else if (thisRefData.refType == "ERA5")
                lonRes = 0.25;

            return lonRes;
        }

        /// <summary> Gets and returns all latitudes or longitudes in specified RefDataDownload. </summary> 
        public double[] GetAllLatsOrLongs(RefDataDownload thisRefData, string latOrLong)
        {
            double[] allLLs = new double[0];

            if (latOrLong == "Lats")
            {
                double latRes = GetLatRes(thisRefData);
                int numLats = Convert.ToInt16((thisRefData.maxLat - thisRefData.minLat) / latRes) + 1;
                Array.Resize(ref allLLs, numLats);

                for (int l = 0; l < numLats; l++)
                    allLLs[l] = thisRefData.minLat + l * latRes;
            }
            else
            {
                double lonRes = GetLongRes(thisRefData);
                int numLongs = Convert.ToInt16((thisRefData.maxLon - thisRefData.minLon) / lonRes) + 1;
                Array.Resize(ref allLLs, numLongs);

                for (int l = 0; l < numLongs; l++)
                    allLLs[l] = thisRefData.minLon + l * lonRes;
            }

            return allLLs;
        }

        /// <summary> Returns index of specified latitude for specified RefDataDownload. </summary> 
        public int GetLatitudeIndex(RefDataDownload thisRefData, double thisLat)
        {
            double latRes = GetLatRes(thisRefData);
            int latInd = (int)(1 / latRes * thisLat + 180 / latRes / 2);
      //      int latInd = (int)(2 * thisLat + 180);

            return latInd;
        }

        /// <summary> Returns index of specified longitude for specified RefDataDownload. </summary> 
        public int GetLongitudeIndex(RefDataDownload thisRefData, double thisLong)
        {
            double lonRes = GetLongRes(thisRefData);
            //      int longInd = (int)(1.6 * thisLong + 288);
            int longInd = (int)(1 / lonRes * thisLong + 360 / lonRes / 2);

            return longInd;
        }

        /// <summary> Returns URL of MERRA2 data file to download. </summary> 
        public string GetMERRA2URL(DateTime thisDay, RefDataDownload merraDownload, string windOrCloud)
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

            int minLatInd = GetLatitudeIndex(merraDownload, merraDownload.minLat);
            int maxLatInd = GetLatitudeIndex(merraDownload, merraDownload.maxLat);
            int minLongInd = GetLongitudeIndex(merraDownload, merraDownload.minLon);
            int maxLongInd = GetLongitudeIndex(merraDownload, merraDownload.maxLon);
            string URL = "";

            if (windOrCloud == "Wind")
            {
                URL = "https://goldsmr4.gesdisc.eosdis.nasa.gov/opendap/MERRA2/M2T1NXSLV.5.12.4/";
                URL += thisYear + "/" + thisMonth + "/";
                URL += "MERRA2_" + dateNum.ToString() + ".tavg1_2d_slv_Nx." + thisYear + thisMonth + thisDayStr + ".nc4.ascii?";
                URL += "T10M[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
                URL += "U50M[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
                URL += "V50M[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
                URL += "SLP[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
                URL += "PS[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
                URL += "lat[" + minLatInd + ":" + maxLatInd + "]," + "time[0:23]," + "lon[" + minLongInd + ":" + maxLongInd + "]";
            }
            else
            {
                URL = "https://goldsmr4.gesdisc.eosdis.nasa.gov/opendap/MERRA2/M2T1NXCSP.5.12.4/";
                URL += thisYear + "/" + thisMonth + "/";
                URL += "MERRA2_" + dateNum.ToString() + ".tavg1_2d_csp_Nx." + thisYear + thisMonth + thisDayStr + ".nc4.ascii?";
                URL += "ISCCPCLDFRC[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
                URL += "MDSCLDFRCTTL[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";
                URL += "MDSOPTHCKTTL[0:23][" + minLatInd + ":" + maxLatInd + "][" + minLongInd + ":" + maxLongInd + "],";                
                URL += "lat[" + minLatInd + ":" + maxLatInd + "]," + "time[0:23]," + "lon[" + minLongInd + ":" + maxLongInd + "]";
            }

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
                     

        public int GetNumNodesInRange(double minDeg, double maxDeg, string latOrLong, RefDataDownload refDataDownload)
        {
            // Returns the number of nodes included between specified range (either "lat" or "lon")

            int numNodes = 0;
            int minInd = 0;
            int maxInd = 0;

            // Find lat/long index of min and max value
            if (latOrLong == "lat")
            {
                minInd = GetLatitudeIndex(refDataDownload, minDeg);
                maxInd = GetLatitudeIndex(refDataDownload, maxDeg);

            }
            else
            {
                minInd = GetLongitudeIndex(refDataDownload, minDeg);
                maxInd = GetLongitudeIndex(refDataDownload, maxDeg);
            }

            numNodes = maxInd - minInd;

            return numNodes;
        }

        /// <summary> Returns username and password of a reference object with specified refType (MERRA2 or ERA5) if it exists </summary>
        public string[] GetUserPasswordbyRefType(string refType)
        {
            // Returns the username and password of NASA earth data credentials if saved in a MERRA object
            string[] userPsd = new string[2];

            for (int r = 0; r < numRefDataDownloads; r++)
                if (refType == refDataDownloads[r].refType && refDataDownloads[r].userName != "")
                {
                    userPsd[0] = refDataDownloads[r].userName;
                    userPsd[1] = refDataDownloads[r].userPassword;
                }

            return userPsd;
        }

 /*       /// <summary> Returns folder location of reference of same type if it exists </summary>
        public string GetReferenceDataFolder(string thisRefType)
        {
            // Returns the folder location of another reference object of the same type
            string refLocation = "";

            for (int r = 0; r < numReferences; r++)
                if (reference[r].referenceType == thisRefType && reference[r].refDataFolder != "")
                    refLocation = reference[r].refDataFolder;                    

            return refLocation;
        }
 */
        

        /// <summary> Finds and returns the index for specified reference data download </summary>
        public int GetReferenceDownloadIndex(string refDownName)
        {
            int refDownInd = 0;

            for (int d = 0; d < numRefDataDownloads; d++)
                if (refDownName == refDataDownloads[d].GetName())
                {
                    refDownInd = d;
                    break;
                }

            return refDownInd;
        }

        /// <summary> Finds and returns the Reference Data Download object based on specified name </summary>
        public RefDataDownload GetRefDataDownloadByName(string refDownName)
        {
            RefDataDownload refDown = new RefDataDownload();

            for (int d = 0; d < numRefDataDownloads; d++)
                if (refDownName == refDataDownloads[d].GetName())
                {
                    refDown = refDataDownloads[d];
                    break;
                }

            return refDown;
        }

        /// <summary> Calculates and returns average wind rose </summary>
        public double[] CalcAvgWindRose(UTM_conversion utmConv, int numWD)
        {
            double[] avgRose = new double[numWD];

            if (reference.Length == 0)
                return avgRose;

            for (int r = 0; r < reference.Length; r++)
            {
                double[] thisRose = reference[r].CalcWindOrEnergyRose(100, 100, utmConv, numWD, "Wind Rose", 0, 0);

                for (int d = 0; d < thisRose.Length; d++)
                    avgRose[d] = avgRose[d] + thisRose[d];
            }

            for (int d = 0; d < numWD; d++)
                avgRose[d] = avgRose[d] / reference.Length;

            return avgRose;
        }

        /// <summary> Calculates and returns average wind rose </summary>
        public double[] CalcAvgEnergyRose(UTM_conversion utmConv, int numWD, double airDens, double rotorDiam)
        {
            double[] avgRose = new double[numWD];

            if (reference.Length == 0)
                return avgRose;

            for (int r = 0; r < reference.Length; r++)
            {
                double[] thisRose = reference[r].CalcWindOrEnergyRose(100, 100, utmConv, numWD, "Energy Rose", airDens, rotorDiam);

                for (int d = 0; d < thisRose.Length; d++)
                    avgRose[d] = avgRose[d] + thisRose[d];
            }

            for (int d = 0; d < numWD; d++)
                avgRose[d] = avgRose[d] / reference.Length;

            return avgRose;
        }

        /// <summary> Creates and returns ERA5 filename based on specified date and min/max lat/lon </summary>        
        public string CreateERA5Filename(DateTime thisDate, RefDataDownload thisRefData)
        {
            string filename = "ERA5_N" + thisRefData.minLat.ToString() + "_to_" + thisRefData.maxLat.ToString() + "_W" + thisRefData.minLon.ToString() 
                + "_to_" + thisRefData.maxLon.ToString() + "_" + thisDate.ToString("%yyyy_%MM_%dd") + ".nc";

            return filename;
        }

  /*      /// <summary> Returns true if reference daily datafile exists in folder. </summary>
        public bool ReferenceFileExists(DateTime thisDate, RefDataDownload thisRefData)
        {
            bool fileExists = false;
            string fileName = CreateERA5Filename(thisDate, thisRefData);

            try
            {
                string[] thisFile = Directory.GetFiles(thisRefData.folderLocation, fileName);
                if (thisFile.Length == 1)
                    fileExists = true;
                else
                    fileExists = false;
            }
            catch
            {
                return fileExists;
            }

            return fileExists;
        }
  */
        /// <summary> Adds a new reference data download </summary>
        public void AddRefDataDownload(RefDataDownload newDataDownload)
        {
            // Add Reference Data download object to list
            Array.Resize(ref refDataDownloads, numRefDataDownloads + 1);
            refDataDownloads[numRefDataDownloads - 1] = newDataDownload;
        }

        /// <summary> Returns true if there is already a reference data download folder with the same settings </summary>
        public bool HaveThisRefDataDownload(RefDataDownload thisDataDownload)
        {
            bool gotIt = false;

            if (thisDataDownload.minLat == 0) // Empty data download, don't add it yet
                return gotIt;

            for (int r = 0; r < numRefDataDownloads; r++)
            {
                if (thisDataDownload.refType == refDataDownloads[r].refType && thisDataDownload.minLat == refDataDownloads[r].minLat &&
                    thisDataDownload.maxLat == refDataDownloads[r].maxLat && thisDataDownload.minLon == refDataDownloads[r].minLon
                    && thisDataDownload.maxLon == refDataDownloads[r].maxLon)
                {
                    gotIt = true;
                    break;
                }
            }

            return gotIt;
        }

        public void UpdateRefDataDownload(RefDataDownload updatedRefData)
        {
            // Updates reference data download with new start/end dates
            for (int r = 0; r < numRefDataDownloads; r++)
            {
                if (updatedRefData.refType == refDataDownloads[r].refType && updatedRefData.minLat == refDataDownloads[r].minLat &&
                    updatedRefData.maxLat == refDataDownloads[r].maxLat && updatedRefData.minLon == refDataDownloads[r].minLon
                    && updatedRefData.maxLon == refDataDownloads[r].maxLon)
                {
                    refDataDownloads[r].startDate = updatedRefData.startDate;
                    refDataDownloads[r].endDate = updatedRefData.endDate;
                    break;
                }
            }
        }

        /// <summary> Returns true if specified RefDataDownload objects are the same </summary>        
        public bool IsSameRefDataDownload(RefDataDownload refData1, RefDataDownload refData2)
        {
            bool areSame = true;

            if (refData1.refType != refData2.refType || refData1.startDate != refData2.startDate || refData1.endDate != refData2.endDate
                || refData1.minLat != refData2.minLat || refData1.maxLat != refData2.maxLat || refData1.minLon != refData2.minLon || refData1.maxLon != refData2.maxLon
                || refData1.folderLocation != refData2.folderLocation) 
                areSame = false;

            return areSame;
        }

        /// <summary> Calculates % completion of downloaded data files </summary> 
        public double CalcDownloadedDataCompletion(RefDataDownload thisRefData)
        {
            double complete = 0;
            int numTotalDays = (int)Math.Round(thisRefData.endDate.Subtract(thisRefData.startDate).TotalDays,0);                                    
            int daysWithData = 0;

            if (Directory.GetFiles(thisRefData.folderLocation).Count() > 0)
            {
                for (DateTime thisDate = thisRefData.startDate; thisDate <= thisRefData.endDate; thisDate = thisDate.AddDays(1))
                {
                    if (ReferenceFileExists(thisDate, thisRefData))
                        daysWithData++;
                }
            }

            // If ERA5 data, figure out if folder has daily files or one big file
            if (thisRefData.refType == "ERA5" && daysWithData == 0)
            {
                string[] refDataFiles = Directory.GetFiles(thisRefData.folderLocation, "*.nc");
                DateTime baseTime = new DateTime(1900, 01, 01, 0, 0, 0); //time that all the ERA5 'time' variable values are relative to

                if (refDataFiles != null)
                {
                    try
                    {                       
                        for (int r = 0; r < refDataFiles.Length; r++)
                        {
                            DateTime thisContERA5 = IsThisAContinuumERA5File(refDataFiles[r]);

                            if (thisContERA5.Year != 1) // Daily file downloaded by Continuum                            
                                daysWithData++;                            
                            else
                            {
                                DataSet thisERA5Data = DataSet.Open(refDataFiles[r]);
                                Variable[] allVars = thisERA5Data.Variables.ToArray();

                                Int32[] allTime = thisERA5Data.GetData<Int32[]>("time");

                                DateTime firstDate = baseTime.AddHours(allTime[0]);
                                DateTime lastDate = baseTime.AddHours(allTime[allTime.Length - 1]);

                                daysWithData = daysWithData + Convert.ToInt32(lastDate.Subtract(firstDate).TotalDays);
                                thisERA5Data.Dispose();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                 //       MessageBox.Show("Error opening ERA5 data file.");                        
                    }
                }
            }
            
            if (numTotalDays > 0)
                complete = (double)daysWithData / numTotalDays;

            if (complete > 1.0)
                complete = 1.0;

            return complete;
        }
  

        /// <summary> Opens reference data file and finds min/max lat/long and start/end date  </summary>
        public RefDataDownload ReadFileAndDefineRefDataDownload(string refDataFolder)
        {
            RefDataDownload refDataDownload = new RefDataDownload();
            refDataDownload.folderLocation = refDataFolder;

            // Check to make sure that there are reference files in this folder
            // Make sure folder exists first
            if (Directory.Exists(refDataFolder) == false)
            {
                // See if folder exists under a different user name
                int usersFirstInd = refDataFolder.IndexOf("Users");
                
                int usersInd = refDataFolder.IndexOf('\\', refDataFolder.IndexOf("Users") + 7);
                string refFolderNoUser = refDataFolder.Substring(usersInd + 1, refDataFolder.Length - usersInd - 1);

                string thisUserName = Environment.GetEnvironmentVariable("USERPROFILE");
                string newRefFolderName = thisUserName + "\\" + refFolderNoUser;

                if (Directory.Exists(newRefFolderName) == false)
                {
                    MessageBox.Show("Could not find reference data folder: " + refFolderNoUser + ". Please select folder with reference data files.");
                    FolderBrowserDialog fbd = new FolderBrowserDialog();

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        // Read first file and figure out parameters for RefDataDownload and assign to dataToDownload

                        if (Directory.Exists(fbd.SelectedPath) == false) // user renamed 'new folder' so need to go grab new name
                        {
                            DirectoryInfo[] allFolders = new DirectoryInfo(fbd.SelectedPath).Parent.GetDirectories();
                            allFolders.OrderByDescending(d => d.LastWriteTimeUtc).First();
                            fbd.SelectedPath = allFolders[0].FullName;
                        }
                    }

                    refDataFolder = fbd.SelectedPath;
                    refDataDownload.folderLocation = fbd.SelectedPath;
                }
                else
                {
                    refDataFolder = newRefFolderName;
                    refDataDownload.folderLocation = newRefFolderName;
                }
            }

            // Figure out if it's MERRA2 or ERA5 data
            string fileRefType = "";

            try
            {
                string[] MERRAfiles = Directory.GetFiles(refDataFolder, "*.ascii");
                string[] ERA5Files = Directory.GetFiles(refDataFolder, "*.nc");

                if (MERRAfiles.Length > 0 && ERA5Files.Length > 0)
                {
                    if (MERRAfiles.Length > ERA5Files.Length)
                        fileRefType = "MERRA2";
                    else
                        fileRefType = "ERA5";

                    MessageBox.Show("There are " + MERRAfiles.Length.ToString() + " .ascii files and " + ERA5Files.Length.ToString() + " .nc files.  " +
                        "Assuming that reference is " + fileRefType);

                }
                else if (MERRAfiles.Length > 0)
                    fileRefType = "MERRA2";
                else if (ERA5Files.Length > 0)
                    fileRefType = "ERA5";
                else if (MERRAfiles.Length == 0 && ERA5Files.Length == 0)
                    return refDataDownload;

                refDataDownload.refType = fileRefType;

                double[] lats = new double[0];
                double[] longs = new double[0];


                if (refDataDownload.refType == "MERRA2")
                {
                    string line;

                    // Open one of the MERRA .ascii files and find the lat/lon closest TWO lats/lons to that of the input
                    StreamReader file;

                    try
                    {
                        file = new StreamReader(MERRAfiles[0]);
                    }
                    catch
                    {
                        MessageBox.Show("Error opening MERRA data file. Check that it's not open in another program.");
                        return refDataDownload;
                    }

                    while ((line = file.ReadLine()) != null)
                    {
                        char[] delims = { ',' };
                        string[] substrings = line.Split(delims);


                        if (substrings[0] == "lat") // read in all latitudes
                        {
                            Array.Resize(ref lats, substrings.Length - 1);

                            for (int i = 1; i < substrings.Length; i++)
                                lats[i - 1] = Convert.ToDouble(substrings[i]);
                        }

                        if (substrings[0] == "lon") // read in all latitudes
                        {
                            Array.Resize(ref longs, substrings.Length - 1);

                            for (int i = 1; i < substrings.Length; i++)
                                longs[i - 1] = Convert.ToDouble(substrings[i]);
                        }

                        if (substrings[0] == "U10M")
                            refDataDownload.incl10mWS = true;

                        
                    }

                    file.Close();
                }
                else
                {
                    // Read ERA5 file and get coords and indices and figure out if it's monthly or daily data                               

                    try
                    {
                        DataSet thisERA5Data = DataSet.Open(ERA5Files[0]);
                        Variable[] allVars = thisERA5Data.Variables.ToArray();

                        Single[] singlats = thisERA5Data.GetData<Single[]>("latitude");
                        Single[] singlons = thisERA5Data.GetData<Single[]>("longitude");

                        lats = new double[singlats.Length];
                        longs = new double[singlons.Length];

                        for (int l = 0; l < singlats.Length; l++)
                            lats[l] = Convert.ToDouble(singlats[l]);

                        for (int l = 0; l < singlons.Length; l++)
                            longs[l] = Convert.ToDouble(singlons[l]);

                        Array.Sort(lats);
                        Array.Sort(longs);

                        int slashInd = ERA5Files[0].LastIndexOf("\\");
                        string fileName = ERA5Files[0].Substring(slashInd + 1, ERA5Files[0].Length - slashInd - 1);
                        int numUnderscores = fileName.Count(c => c == '_');

                        if (numUnderscores == 8)
                            refDataDownload.monthlyOrDaily = "Monthly";
                        else
                            refDataDownload.monthlyOrDaily = "Daily";

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening ERA5 data file.");
                        MessageBox.Show(ex.Message);
                        return refDataDownload;
                    }
                }

                if (lats.Length > 0 && longs.Length > 0)
                {
                    refDataDownload.minLat = lats.Min();
                    refDataDownload.maxLat = lats.Max();
                    refDataDownload.minLon = longs.Min();
                    refDataDownload.maxLon = longs.Max();
                }

                // Check other downloads for crdentials
                for (int r = 0; r < numRefDataDownloads; r++)
                    if (refDataDownloads[r].refType == refDataDownload.refType)
                    {
                        refDataDownload.userName = refDataDownloads[r].userName;
                        refDataDownload.userPassword = refDataDownloads[r].userPassword;
                    }
            }
            catch
            {
                // Assume reference data is MERRA2
                refDataDownload.refType = "MERRA2";
            }            

            return refDataDownload;

        }

        public struct DateRangeAndCompletion
        {
            public DateTime[] startEnd;
            public double completion;
        }

        /// <summary> Finds and returns the start and end date of the downloaded reference data files. </summary>
        public DateRangeAndCompletion GetDataFileStartEndDateAndCompletion(string refDataFolder, string refType)
        {
            DateRangeAndCompletion dateRangeAndCount = new DateRangeAndCompletion();
            dateRangeAndCount.startEnd = new DateTime[2];

            bool folderExists = Directory.Exists(refDataFolder);
            if (folderExists == false)
                return dateRangeAndCount;

            if (refType == "MERRA2")
            {
                string[] MERRAfiles = Directory.GetFiles(refDataFolder, "*.ascii");
                
                for (int f = 0; f < MERRAfiles.Length; f++)
                {
                    // Get date from filename
                    int ncIndex = MERRAfiles[f].IndexOf("nc4");
                    string dateStr = MERRAfiles[f].Substring(ncIndex - 9, 8);
                    int thisYear = Convert.ToInt16(dateStr.Substring(0, 4));
                    int thisMonth = Convert.ToInt16(dateStr.Substring(4, 2));
                    int thisDay = Convert.ToInt16(dateStr.Substring(6, 2));

                    DateTime thisDate = new DateTime(thisYear, thisMonth, thisDay);

                    if (thisDate < dateRangeAndCount.startEnd[0] || dateRangeAndCount.startEnd[0].Year == 1)
                        dateRangeAndCount.startEnd[0] = thisDate;

                    if (thisDate > dateRangeAndCount.startEnd[1])
                        dateRangeAndCount.startEnd[1] = thisDate.AddHours(23); // End of day
                }

                // Now figure out data download completion
                int numDays = Convert.ToInt32(dateRangeAndCount.startEnd[1].Subtract(dateRangeAndCount.startEnd[0]).TotalDays);

                if (numDays > 0)
                    dateRangeAndCount.completion = (double)MERRAfiles.Length / numDays;
                
            }
            else if (refType == "ERA5")
            {
                // Either use file names to get start/end date or, if not downloaded through Continuum, read all netCDFs to get start and end dates 
                string[] refDataFiles = Directory.GetFiles(refDataFolder, "*.nc");
                DateTime baseTime = new DateTime(1900, 01, 01, 0, 0, 0); //time that all the ERA5 'time' variable values are relative to
                              
                if (refDataFiles == null)
                {
                    MessageBox.Show("Could not find netCDF file. Check specified folder path and try again.");
                    return dateRangeAndCount;
                }

                                             

                try
                {
                    double numDays = 0;

                    for (int r = 0; r < refDataFiles.Length; r++)
                    {
                        DateTime isContERA5 = IsThisAContinuumERA5File(refDataFiles[r]);

                        if (isContERA5.Year != 1)                                
                        {
                            if (isContERA5 < dateRangeAndCount.startEnd[0] || dateRangeAndCount.startEnd[0].Year == 1)
                                dateRangeAndCount.startEnd[0] = isContERA5;

                            if (isContERA5 > dateRangeAndCount.startEnd[1])
                                dateRangeAndCount.startEnd[1] = isContERA5.AddHours(23); // Make date end of day (i.e. 11 pm)

                            numDays++;
                        }
                        else //  Need to read netCDF files to figure out dates
                        {
                            DataSet thisERA5Data = DataSet.Open(refDataFiles[r]);
                            Variable[] allVars = thisERA5Data.Variables.ToArray();

                            Int32[] allTime = thisERA5Data.GetData<Int32[]>("time");

                            DateTime firstDate = baseTime.AddHours(allTime[0]);
                            DateTime lastDate = baseTime.AddHours(allTime[allTime.Length - 1]);

                            if (firstDate < dateRangeAndCount.startEnd[0] || dateRangeAndCount.startEnd[0].Year == 1)
                                dateRangeAndCount.startEnd[0] = firstDate;

                            if (lastDate > dateRangeAndCount.startEnd[1])
                                dateRangeAndCount.startEnd[1] = lastDate;

                            numDays = numDays + lastDate.Subtract(firstDate).TotalDays + 1.0 / 24;
                            thisERA5Data.Dispose();
                        } 
                    }

                    int totalNumDays = Convert.ToInt32(dateRangeAndCount.startEnd[1].Subtract(dateRangeAndCount.startEnd[0]).TotalDays);

                    if (totalNumDays > 0)
                        dateRangeAndCount.completion = (double)numDays / totalNumDays;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
              //      MessageBox.Show("Error opening ERA5 data file.");
                    return dateRangeAndCount;
                }
            }

            return dateRangeAndCount;
        }

    
        /// <summary> Returns true if file at specfied path is an ERA5 file that was downloaded by Continuum </summary>    
        public DateTime IsThisAContinuumERA5File(string refDataFile)
        {
            bool formattedFileName = false;
            int numUnderExpect = 9; // If the ERA5 datafile was downloaded through Continuum, it will have 9 underscores
                                
            int fileYear = 1;
            int fileMonth = 1;
            int fileDay = 1;
            DateTime fileDate = new DateTime(fileYear, fileMonth, fileDay);

            string justFileName = refDataFile.Substring(refDataFile.LastIndexOf('\\') + 1, refDataFile.Length - refDataFile.LastIndexOf('\\') - 1);
            int numUnderAct = justFileName.Count(t => t == '_');

            if (numUnderAct == numUnderExpect)
                formattedFileName = true;

            if (formattedFileName) // It has 9 underscores, now check to see if it contains the timestamp
            {
                int dateLength = 10;

                string dateStr = justFileName.Substring(justFileName.Length - dateLength - 3, dateLength);            
                int.TryParse(dateStr.Substring(0, 4), out fileYear);            
                int.TryParse(dateStr.Substring(5, 2), out fileMonth);            
                int.TryParse(dateStr.Substring(8, 2), out fileDay);
                fileDate = new DateTime(fileYear, fileMonth, fileDay);

                if (fileDate.Year == 1)
                    formattedFileName = false;
            }
                    
            return fileDate;      
    
        }

        /// <summary> Creates and returns the string format to use for specified coordinate (i.e. formatted depending on number of sig digs.) </summary>
        public string GetStringFormatForReferenceDataFilename(double thisCoord)
        {
            string strFormat = "#.0";
            int numDigs = 1;

            string[] latSplit = thisCoord.ToString().Split('.');

            if (latSplit.Length > 1)
                numDigs = latSplit[1].Length;
            
            if (numDigs == 2)
                strFormat = "#.00";
            else if (numDigs == 3)
                strFormat = "#.000";
            
            return strFormat;
        }

        /// <summary> Creates filename for exported MERRA2 or ERA5 data. </summary>
        public string CreateReferenceDatafilename(RefDataDownload refData, DateTime thisDate = new DateTime(), string windOrCloud = "")
        {
            string fileName = "";            

            if (refData.refType == "ERA5")
            {
                double minLat = Math.Round(refData.minLat / GetLatRes(refData), 0) * GetLatRes(refData);
                string minLatForm = GetStringFormatForReferenceDataFilename(refData.minLat);

                double maxLat = Math.Round(refData.maxLat / GetLatRes(refData), 0) * GetLatRes(refData);
                string maxLatForm = GetStringFormatForReferenceDataFilename(refData.maxLat);

                double minLon = Math.Round(refData.minLon / GetLongRes(refData)) * GetLongRes(refData);
                string minLonForm = GetStringFormatForReferenceDataFilename(refData.minLon);

                double maxLon = Math.Round(refData.maxLon / GetLongRes(refData)) * GetLongRes(refData);
                string maxLonForm = GetStringFormatForReferenceDataFilename(refData.maxLon);

                if (thisDate.Year != 1)
                {
                    if (refData.monthlyOrDaily == "Daily")
                        fileName = "ERA5_N" + minLat.ToString(minLatForm) + "_to_" + maxLat.ToString(maxLatForm) + "_W" + minLon.ToString(minLonForm)
                            + "_to_" + maxLon.ToString(maxLonForm) + "_" + thisDate.ToString("yyyy_MM_dd") + ".nc";
                    else
                        fileName = "ERA5_N" + minLat.ToString(minLatForm) + "_to_" + maxLat.ToString(maxLatForm) + "_W" + minLon.ToString(minLonForm)
                            + "_to_" + maxLon.ToString(maxLonForm) + "_" + thisDate.ToString("yyyy_MM") + ".nc";
                }
                else
                    fileName = "ERA5_N" + minLat.ToString(minLatForm) + "_to_" + maxLat.ToString(maxLatForm) + "_W" + minLon.ToString(minLonForm)
                        + "_to_" + maxLon.ToString(maxLonForm) + ".nc";
            }
            else if (refData.refType == "MERRA2")
            {
                int dateNum = 0;
                if (thisDate.Year <= 1991)
                    dateNum = 100;
                else if (thisDate.Year <= 2000)
                    dateNum = 200;
                else if (thisDate.Year <= 2010)
                    dateNum = 300;
                else
                    dateNum = 400;

                string thisMonth = thisDate.Month.ToString();
                if (thisDate.Month < 10)
                    thisMonth = "0" + thisMonth;

                string thisDay = thisDate.Day.ToString();
                if (thisDate.Day < 10)
                    thisDay = "0" + thisDay;

                if (windOrCloud == "Wind")
                    fileName = "MERRA2_" + dateNum.ToString() + ".tavg1_2d_slv_Nx." + thisDate.Year + thisMonth + thisDay + ".nc4.ascii";
                else if (windOrCloud == "Cloud")
                    fileName = "MERRA2_Cloud" + dateNum.ToString() + ".tavg1_2d_csp_Nx." + thisDate.Year + thisMonth + thisDay + ".nc4.ascii";
                                
            }

            return fileName;
        }               

        /// <summary> Returns true if reference daily datafile exists in folder. </summary>
        public bool ReferenceFileExists(DateTime thisDate, RefDataDownload refDataDown, string windOrCloud = "")
        {
            bool fileExists = false;
            string fileName = CreateReferenceDatafilename(refDataDown, thisDate, windOrCloud);

            try
            {
                string[] thisFile = Directory.GetFiles(refDataDown.folderLocation, fileName);
                if (thisFile.Length == 1)
                    fileExists = true;
                else
                    fileExists = false;
            }
            catch
            {
                return fileExists;
            }

            return fileExists;
        }

        /// <summary> Downloads and save MERRA2 datafile for specified day.     </summary>
        public bool SaveMERRA2DataFile(HttpWebResponse response, DateTime thisDate, RefDataDownload refDataDown, string windOrCloud)
        {
            // Now access the data            
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string MERRA2filename = CreateReferenceDatafilename(refDataDown, thisDate, windOrCloud);
            StreamWriter writer = new StreamWriter(refDataDown.folderLocation + "\\" + MERRA2filename);

            bool isDataset = false;
            bool isFirstLine = true;

            // Save to file
            while (reader.EndOfStream == false)
            {
                string thisLine = reader.ReadLine();
                writer.WriteLine(thisLine);

                if (isFirstLine)
                {
                    if (thisLine.Length > 8)
                        if (thisLine.Substring(0, 8) == "Dataset:")
                            isDataset = true;
                    isFirstLine = false;
                }
            }

            writer.Close();
            stream.Close();
            reader.Close();

            return isDataset;
        }

        /// <summary> Returns true if a Reference (other than refIndToIgnore) has a node with the specified lat and long </summary>        
        public bool IsThisReferenceNodeBeingUsed(string refType, double thisLat, double thisLong, int refIndToIgnore)
        {
            bool beingUsed = false;

            for (int r = 0; r < numReferences; r++)
                for (int n = 0; n < reference[r].numNodes; n++)
                    if (reference[r].nodes[n].XY_ind.Lat == thisLat && reference[r].nodes[n].XY_ind.Lon == thisLong && r != refIndToIgnore)
                    {
                        beingUsed = true;
                        break;
                    }

            return beingUsed;
        }

        /// <summary> Returns true if start/end dates are the same as the other references in the list </summary>        
        public bool IsSameLengthAsOtherRefs(Reference thisRef)
        {
            bool isSame = true;

            for (int r = 0; r < numReferences; r++)
            {
                if (thisRef.startDate != reference[r].startDate || thisRef.endDate != reference[r].endDate)
                {
                    isSame = false;
                    break;
                }
            }

            return isSame;
        }

    }
}

