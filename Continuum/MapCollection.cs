using System;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary> Class that holds list of Map objects. Contains functions to add and delete maps from list. </summary>
    [Serializable()]
    public class MapCollection
    {
        /// <summary> List of Maps </summary>
        public Map[] mapItem;
                
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       
        public int ThisCount {
            get {
                if (mapItem == null)
                    return 0;
                else
                    return mapItem.Length;
            }
        }

        /// <summary> Adds a map to the collection and calls Background_worker to start map calculations </summary>
        public void AddMap(string mapname, int minUTMX, int minUTMY, int reso, int numX, int numY, int whatToMap, string powerCurve, Continuum thisInst, bool isWaked,
                           Wake_Model wakeModel, Met[] metsUsed, Model[] models, bool useTimeSeries)
        {
            int thisCount = ThisCount;
            int numMets = metsUsed.Length;
                        
            string[] metsUsedName = new string[numMets];
            for (int i = 0; i <= numMets - 1; i++)
                metsUsedName[i] = metsUsed[i].name;

            Map thisMap = new Map();
            thisMap.mapName = mapname;
            thisMap.minUTMX = minUTMX;
            thisMap.minUTMY = minUTMY;
            thisMap.reso = reso;
            thisMap.numX = numX;
            thisMap.numY = numY;
            thisMap.modelType = whatToMap;
            thisMap.powerCurve = powerCurve;
            thisMap.isWaked = isWaked;
            thisMap.wakeModel = wakeModel;
            thisMap.metsUsed = metsUsedName;
            thisMap.model = models;
            thisMap.useSR = thisInst.topo.useSR;
            thisMap.useFlowSep = thisInst.topo.useSepMod;
            thisMap.useTimeSeries = useTimeSeries;                       

            // Check to see if map was started but not finished
            int existingInd = GetIncompleteMapInd(thisMap);
            if (existingInd != -999)
                thisMap = mapItem[existingInd];

            if (existingInd == -999)
            {
                // First check that the model doesn//t already exist
                bool mapAlreadyCreated = CheckForDuplicate(metsUsedName, minUTMX, minUTMY, numX, numY, reso, whatToMap, powerCurve, thisInst.topo.useSR, 
                    thisInst.topo.useSepMod, wakeModel, useTimeSeries);
                if (mapAlreadyCreated == true)
                    return;

                Array.Resize(ref mapItem, thisCount + 1);
                mapItem[thisCount] = thisMap;
            }
                        
            BackgroundWork.Vars_for_Gen_Map args = new BackgroundWork.Vars_for_Gen_Map();
            args.thisInst = thisInst;
            args.thisMap = thisMap;
            args.MCP_Method = thisInst.Get_MCP_Method();

            try {
                thisInst.BW_worker = new BackgroundWork();
                thisInst.BW_worker.Call_BW_GenMap(args);
            }
            catch {
                // Background worker is still working
            }

        }

        /// <summary> Deletes specified map(s) from list </summary>
        public void RemoveMap(string[] mapnames) 
        {            
            int numMapsToDelete = mapnames.Length;
            int newCount = ThisCount - numMapsToDelete;

            if (newCount == 0)
                mapItem = null;
            else
            {
                Map[] tempList = new Map[newCount];
                int counter = 0;

                for (int i = 0; i < ThisCount; i++)
                {
                    bool keepIt = true;
                    for (int j = 0; j < numMapsToDelete; j++)
                    {
                        if (mapItem[i].mapName == mapnames[j])
                        {
                            keepIt = false;
                            break;
                        }
                    }

                    if (keepIt == true)
                    {
                        tempList[counter] = mapItem[i];
                        counter++;
                    }
                }

                mapItem = tempList;
            }
        }

        /// <summary> Delete all maps that were generated with thisWakeModel </summary>
        public void RemoveMapByWakeModel(Wake_Model thisWakeModel, WakeCollection wakeList)
        {             
            int newCount = 0;
            for (int i = 0; i < ThisCount; i++)
                if (mapItem[i].isWaked == false || (mapItem[i].isWaked == true && wakeList.IsSameWakeModel(mapItem[i].wakeModel, thisWakeModel) == false))
                    newCount++;

            if (newCount > 0)
            {
                Map[] tempList = new Map[newCount];
                int tempIndex = 0;

                for (int i = 0; i < ThisCount; i++)
                {
                    if (mapItem[i].isWaked == false || (mapItem[i].isWaked == true && wakeList.IsSameWakeModel(mapItem[i].wakeModel, thisWakeModel) == false))
                    {
                        tempList[tempIndex] = mapItem[i];
                        tempIndex++;
                    }
                }

                mapItem = new Map[newCount];
                mapItem = tempList;
            }
            else
                mapItem = null;

        }

        /// <summary> Delete waked wind speed map </summary>
        public void RemoveMapByWakeGridMap(WakeCollection.WakeGridMap thisWakeGrid, WakeCollection wakeList)
        {            
            int newCount = 0;
            for (int i = 0; i < ThisCount; i++)
                if (mapItem[i].isWaked == false || (mapItem[i].isWaked == true && wakeList.IsSameWakeModel(mapItem[i].wakeModel, thisWakeGrid.wakeModel) == false &&
                   mapItem[i].minUTMX != thisWakeGrid.minUTMX && mapItem[i].minUTMY != thisWakeGrid.minUTMY && mapItem[i].numX != thisWakeGrid.numX &&
                   mapItem[i].numY != thisWakeGrid.numY && mapItem[i].reso != thisWakeGrid.wakeGridReso))
                    newCount++;

            if (newCount > 0) {
                Map[] tempList = new Map[newCount];
                int tempIndex = 0;

                for (int i = 0; i < ThisCount; i++) {
                    if (mapItem[i].isWaked == false || (mapItem[i].isWaked == true && wakeList.IsSameWakeModel(mapItem[i].wakeModel, thisWakeGrid.wakeModel) == false &&
                   mapItem[i].minUTMX != thisWakeGrid.minUTMX && mapItem[i].minUTMY != thisWakeGrid.minUTMY && mapItem[i].numX != thisWakeGrid.numX &&
                   mapItem[i].numY != thisWakeGrid.numY && mapItem[i].reso != thisWakeGrid.wakeGridReso)) {
                        tempList[tempIndex] = mapItem[i];
                        tempIndex++;
                    }
                }

                mapItem = new Map[newCount];
                mapItem = tempList;
            }
            else {
                mapItem = null;
            }

        }

        /// <summary> Delete all maps that use specified power curve </summary>
        public void RemoveMapByPowerCurve(string powerCurve)
        {            
            int newCount = 0;
            for (int i = 0; i < ThisCount; i++) {
                if (mapItem[i].powerCurve != powerCurve) {
                    newCount++;
                }
            }

            if (newCount > 0)
            {
                Map[] tempList = new Map[newCount];
                int tempIndex = 0;

                for (int i = 0; i < ThisCount; i++)
                {
                    if (mapItem[i].powerCurve != powerCurve)
                    {
                        tempList[tempIndex] = mapItem[i];
                        tempIndex++;
                    }
                }

                mapItem = new Map[newCount];
                mapItem = tempList;
            }
            else
                mapItem = null;

        }

        /// <summary> Checks to see if map has already been created. Returns false if has not been created. </summary>
        public bool CheckForDuplicate(string[] metsUsed, double minUTMX, double minUTMY, int numX, int numY, int reso,
                                            int whatToMap, string powerCurve, bool useSR, bool useSepModel, Wake_Model thisWakeModel, bool useTimeSeries)
        {           
            bool alreadyExists = false;
            WakeCollection wakeList = new WakeCollection();
            MetCollection metList = new MetCollection();

            if (ThisCount > 0) {
                for (int i = 0; i < ThisCount; i++) {
                    if (mapItem[i].minUTMX == minUTMX && mapItem[i].minUTMY == minUTMY && mapItem[i].numX == numX
                        && mapItem[i].numY == numY && mapItem[i].reso == reso && mapItem[i].modelType == whatToMap
                        && metList.sameMets(metsUsed, mapItem[i].metsUsed) && mapItem[i].powerCurve == powerCurve && mapItem[i].useSR == useSR
                        && mapItem[i].useFlowSep == useSepModel && wakeList.IsSameWakeModel(mapItem[i].wakeModel, thisWakeModel) 
                        && mapItem[i].useTimeSeries == useTimeSeries) {

                        MessageBox.Show("An identical map has already been created.", "Continuum 3");
                        alreadyExists = true;
                        break;
                    }
                }
            }

            return alreadyExists;
        }

        /// <summary> Return Map index of incomplete map if it exists, return -999 if not.  </summary>
        public int GetIncompleteMapInd(Map thisMap)
        {                 
            WakeCollection wakeList = new WakeCollection();
            MetCollection metList = new MetCollection();
            int mapInd = -999;

            if (ThisCount > 0)
            {
                for (int i = 0; i < ThisCount; i++)
                {
                    if (mapItem[i].isComplete == false && mapItem[i].minUTMX == thisMap.minUTMX && mapItem[i].minUTMY == thisMap.minUTMY && mapItem[i].numX == thisMap.numX
                        && mapItem[i].numY == thisMap.numY && mapItem[i].reso == thisMap.reso && mapItem[i].modelType == thisMap.modelType
                        && metList.sameMets(thisMap.metsUsed, mapItem[i].metsUsed) && mapItem[i].powerCurve == thisMap.powerCurve && mapItem[i].useSR == thisMap.useSR
                        && mapItem[i].useFlowSep == thisMap.useFlowSep && wakeList.IsSameWakeModel(mapItem[i].wakeModel, thisMap.wakeModel) && mapItem[i].useTimeSeries == thisMap.useTimeSeries)
                    {

                        mapInd = i;
                        break;
                    }
                }
            }

            return mapInd;
        }

        /// <summary> Remove all maps.  </summary>
        public void ClearAllMaps()
        {             
            mapItem = null;
        }

        /// <summary> Remove all waked maps.  </summary>
        public void ClearAllWakedMaps()
        {            
            Map[] unwakedMaps = null;
            int unwakedCount = 0;

            for (int i = 0; i < ThisCount; i++) {
                if (mapItem[i].isWaked == false) {
                    unwakedCount++;
                    Array.Resize(ref unwakedMaps, unwakedCount);
                    unwakedMaps[unwakedCount - 1] = mapItem[i];
                }
            }

            mapItem = unwakedMaps;

        }

        /// <summary> Deletes all maps which used a met site that has been deleted from the analysis.  </summary>  
        public void DeleteMapsUsingDeletedMets(string[] deletedMets)
        {             
            if (deletedMets == null)
                return;
            string[] mapsToDelete = null;
            int numMapsToDelete = 0;
            
            for (int i = 0; i < ThisCount; i++)
            {
                bool isGettingDeleted = false;
                for (int j = 0; j < mapItem[i].metsUsed.Length; j++)
                {
                    for (int k = 0; k < deletedMets.Length; k++)
                    {
                        if (deletedMets[k] == mapItem[i].metsUsed[j])
                        {
                            numMapsToDelete++;
                            Array.Resize(ref mapsToDelete, numMapsToDelete);
                            mapsToDelete[numMapsToDelete - 1] = mapItem[i].mapName;
                            isGettingDeleted = true;
                            break;
                        }
                    }
                    if (isGettingDeleted)
                        break;
                }
            }

            if (mapsToDelete == null)
                return;

            RemoveMap(mapsToDelete);
                 
        }
    }
}
