using System;

namespace ContinuumNS
{
    /// <summary> Class that holds list of wake models and list of wake maps. Contains functions for calculating wake losses and wake loss coefficients. </summary>
    [Serializable()]
    public class WakeCollection
    {
        /// <summary> List of wake models. </summary>
        public Wake_Model[] wakeModels;
        /// <summary> List of wake maps. </summary>
        public WakeGridMap[] wakeGridMaps;
        /// <summary> von Karman constant = 0.4. </summary>
        public double vonKarman = 0.4f;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Holds all information related to a waked map. </summary>
        [Serializable()] public struct WakeGridMap
        {
            /// <summary> Wake map name. </summary>
            public string name;
            /// <summary> Minimum UTMX of map. </summary>
            public int minUTMX;
            /// <summary> Minimum UTMY of map. </summary>
            public int minUTMY;
            /// <summary> Number of grid points along X. </summary>
            public int numX;
            /// <summary> Number of grid points along Y. </summary>
            public int numY;
            /// <summary> Grid resolution. </summary>
            public int wakeGridReso;
            /// <summary> Wake model used. </summary>
            public Wake_Model wakeModel;
            /// <summary> Wake map nodes. </summary>
            public WakeGridObj[,] wakeGrids;
            /// <summary> True if map generation was completed. </summary>
            public bool isComplete;
            
        }

        /// <summary> Holds all information related to a waked map node. </summary>
        [Serializable()] public struct WakeGridObj
        {
            /// <summary> Overall waked wind speed. </summary>
            public double wakedWS;
            /// <summary> Overall wake loss. </summary>
            public double wakeLoss;
            /// <summary> Net energy production. </summary>
            public double netEnergy;

            /// <summary> Sectorwise waked wind speed. </summary>
            public double[] sectorWakedWS;
            /// <summary> Sectorwise wake loss. </summary>
            public double[] sectorWakeLoss;
            /// <summary> Sectorwise net energy production. </summary>
            public double[] sectorNetEnergy;
        }

        /// <summary> Holds estimated overall and sectorwise wind speed and energy production found from wake loss calculation. </summary>
        public struct WakeCalcResults
        {
            /// <summary> Overall wind speed distribution. </summary>
            public double[] WS_Dist;
            /// <summary> Sectorwise wind speed distribution. </summary>
            public double[,] sectorDist;
            /// <summary> Overall wind speed. </summary>
            public double wakedWS;
            /// <summary> Overall wake loss. </summary>
            public double wakeLoss;
            /// <summary> Overall net energy production. </summary>
            public double netEnergy;
            /// <summary> Sectorwise wind speed. </summary>
            public double[] sectorWakedWS;
            /// <summary> Sectorwise wake loss. </summary>
            public double[] sectorWakeLoss;
            /// <summary> Sectorwise net energy production. </summary>
            public double[] sectorNetEnergy;
        }

        /// <summary> Holds wake loss profile polynomial coefficients for a specific free-stream wind speed and distance, X, downwind of turbine. </summary>
        public struct WakeLossCoeffs
        {
            /// <summary> Free-stream wind speed. </summary>
            public double freeStream;
            /// <summary> Distance downwind of turbine in rotor diameters [RDs]. </summary>
            public double X_LengthRD;
            /// <summary> Regression coefficient 1. </summary>
            public double linCoeff1;
            /// <summary> Regression coefficient 2. </summary>
            public double linCoeff2;
            /// <summary> Regression coefficient 3. </summary>
            public double linCoeff3;
            /// <summary> Regression coefficient 4. </summary>
            public double linCoeff4;
            /// <summary> Regression intercept. </summary>
            public double linRegInt;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Returns number of defined wake models in list. </summary>
        public int NumWakeModels {
            get {
                if (wakeModels == null)
                    return 0;
                else
                    return wakeModels.Length;
            }
        }

        /// <summary> Returns number of wake maps in list. </summary>
        public int NumWakeGridMaps
        {
            get
            {
                if (wakeGridMaps == null)
                    return 0;
                else
                    return wakeGridMaps.Length;
            }
        }

        /// <summary> Returns number of wake maps in list. </summary>
        public int NumCompleteWakeGridMaps
        {
            get
            {
                if (wakeGridMaps == null)
                    return 0;
                else
                {
                    int Num_Complete = 0;
                    for (int i = 0; i < NumWakeGridMaps; i++)
                    {
                        if (wakeGridMaps[i].isComplete == true)
                            Num_Complete++;
                    }
                    return Num_Complete;
                }                    
            }
        }

        /// <summary> Clears all wake models and wake maps. </summary>
        public void ClearAll()
        {           
            wakeModels = null;
            wakeGridMaps = null;
        }

        /// <summary> Clears all wake maps. </summary>
        public void ClearWakeMaps()
        {            
            wakeGridMaps = null;
        }

        /// <summary> Returns true if two wake models are identical. </summary>
        public bool IsSameWakeModel(Wake_Model wakeModel1, Wake_Model wakeModel2)
        {           
            bool isSame = false;

            if (wakeModel1 == null && wakeModel2 == null)
                return true;

            if ((wakeModel1 == null && wakeModel2 != null) || (wakeModel1 != null && wakeModel2 == null))
                return false;
        
            if (wakeModel1.ambTI == wakeModel2.ambTI && wakeModel1.horizWakeExp == wakeModel2.horizWakeExp && wakeModel1.wakeModelType == wakeModel2.wakeModelType && 
                wakeModel1.powerCurve.name == wakeModel2.powerCurve.name && wakeModel1.comboMethod == wakeModel2.comboMethod && wakeModel1.wakeRechargeRate == wakeModel2.wakeRechargeRate)
                {
                    if (wakeModel1.wakeModelType == 1)
                    {
                        if (wakeModel1.DW_Spacing == wakeModel2.DW_Spacing && wakeModel1.CW_Spacing == wakeModel2.CW_Spacing && wakeModel1.ambRough == wakeModel2.ambRough)
                            isSame = true;
                    }
                    else
                        isSame = true;
                }

                return isSame;
        }

        /// <summary> Returns true if two wake maps are identical. </summary>
        public bool IsSameWakeGrid(WakeGridMap wakeGrid1, WakeGridMap wakeGrid2)
        {            
            bool isSame = false;

        if (wakeGrid1.minUTMX == wakeGrid2.minUTMX && wakeGrid1.minUTMY == wakeGrid2.minUTMY && wakeGrid1.numX == wakeGrid2.numX && wakeGrid1.numY == wakeGrid2.numY &&
            wakeGrid1.wakeGrids.Length == wakeGrid2.wakeGrids.Length && IsSameWakeModel(wakeGrid1.wakeModel, wakeGrid2.wakeModel) && wakeGrid1.wakeGridReso == wakeGrid2.wakeGridReso)            
                isSame = true;

            return isSame;
        }

        /// <summary> Returns minimum value of wake loss map for specified WD sector and selected parameter. </summary>
        public double FindMin(WakeGridMap thisGrid, int WD_Ind, int plotInd)
        {            
            double thisMin = 100000;
            int numWD = 0;

            try {
                numWD = thisGrid.wakeGrids[0, 0].sectorWakedWS.Length;
            }
            catch {
                return thisMin;
            }
            
            for (int i = 0; i < thisGrid.numX; i++)
            { 
                for (int j = 0; j < thisGrid.numY; j++)
                {
                    if (plotInd == 0)
                    {
                        if (WD_Ind == numWD)
                        {
                            if (thisGrid.wakeGrids[i, j].wakedWS < thisMin)
                                thisMin = thisGrid.wakeGrids[i, j].wakedWS;
                        }
                        else {
                            if (thisGrid.wakeGrids[i, j].sectorWakedWS[WD_Ind] < thisMin)
                                thisMin = thisGrid.wakeGrids[i, j].sectorWakedWS[WD_Ind];
                        }
                    }
                    else if (plotInd == 1)
                    {
                        if (WD_Ind == numWD)
                        {
                            if (thisGrid.wakeGrids[i, j].wakeLoss * 100 < thisMin)
                                thisMin = thisGrid.wakeGrids[i, j].wakeLoss * 100;
                        }
                        else {
                            if (thisGrid.wakeGrids[i, j].sectorWakeLoss[WD_Ind] * 100 < thisMin)
                                thisMin = thisGrid.wakeGrids[i, j].sectorWakeLoss[WD_Ind] * 100;
                        }
                    }
                    else {
                        if (WD_Ind == numWD)
                        {
                            if (thisGrid.wakeGrids[i, j].netEnergy < thisMin)
                                thisMin = thisGrid.wakeGrids[i, j].netEnergy;
                        }
                        else {
                            if (thisGrid.wakeGrids[i, j].sectorNetEnergy[WD_Ind] < thisMin)
                                thisMin = thisGrid.wakeGrids[i, j].sectorNetEnergy[WD_Ind];
                        }

                    }

                }

            }

            return thisMin;
        }

        /// <summary> Returns maximum value of wake loss map for specified WD sector and selected parameter. </summary>
        public double FindMax(WakeGridMap thisGrid, int WD_Ind, int plotInd)
        {            
            double thisMax = 0;
            int numWD = 0;

            try {
                numWD = thisGrid.wakeGrids[0, 0].sectorWakedWS.Length;
            }
            catch {
                return thisMax;
            }

            for (int i = 0; i < thisGrid.numX; i++)
            { 
                for (int j = 0; j < thisGrid.numY; j++)
                {
                    if (plotInd == 0)
                    {
                        if (WD_Ind == numWD)
                        {
                            if (thisGrid.wakeGrids[i, j].wakedWS > thisMax)
                                thisMax = thisGrid.wakeGrids[i, j].wakedWS;
                        }
                        else
                        {
                            if (thisGrid.wakeGrids[i, j].sectorWakedWS[WD_Ind] > thisMax)
                                thisMax = thisGrid.wakeGrids[i, j].sectorWakedWS[WD_Ind];
                        }

                    }
                    else if (plotInd == 1)
                    {
                        if (WD_Ind == numWD)
                        {
                            if (thisGrid.wakeGrids[i, j].wakeLoss * 100 > thisMax)
                                thisMax = thisGrid.wakeGrids[i, j].wakeLoss * 100;
                        }
                        else {
                            if (thisGrid.wakeGrids[i, j].sectorWakeLoss[WD_Ind] * 100 > thisMax)
                                thisMax = thisGrid.wakeGrids[i, j].sectorWakeLoss[WD_Ind] * 100;
                        }
                    }
                    else {
                        if (WD_Ind == numWD)
                        {
                            if (thisGrid.wakeGrids[i, j].netEnergy > thisMax)                            
                                thisMax = thisGrid.wakeGrids[i, j].netEnergy;
                        }
                        else {
                            if (thisGrid.wakeGrids[i, j].sectorNetEnergy[WD_Ind] > thisMax)
                                thisMax = thisGrid.wakeGrids[i, j].sectorNetEnergy[WD_Ind];
                        }

                    }
                }
            }

            return thisMax;
        }

        /// <summary> If the calculations get cancelled, there could be wake grids with no wake model so this compares the wake grids and wake models and deletes wake grids with no wake model. </summary>
        public void CleanUpWakeModelsAndGrid()
        {             
            if (NumWakeGridMaps > 0 || NumWakeModels > 0)
            {                
                for (int i = 0; i < NumWakeGridMaps; i++)
                {
                    bool wakeGridHasModel = false;
                    for (int j = 0; j < NumWakeModels; j++)
                    { 
                        if (IsSameWakeModel(wakeGridMaps[i].wakeModel, wakeModels[j]))
                        {
                            wakeGridHasModel = true;
                            break;
                        }
                    }

                    if (wakeGridHasModel == false)
                    {
                        RemoveWakeGridMap(wakeGridMaps[i]);
                        if (i >= NumWakeGridMaps) 
                            break;                        
                    }
                }
            }
        }

        /// <summary> Returns true if Wake Map has already been created. </summary>
        public bool WakeGridExists(WakeGridMap wakeGrid)
        {           
            bool existsAlready = false;

            for (int i = 0; i < NumWakeGridMaps; i++)
            {
                if (wakeGridMaps[i].minUTMX == wakeGrid.minUTMX && wakeGridMaps[i].minUTMY == wakeGrid.minUTMY && wakeGridMaps[i].numX == wakeGrid.numX && wakeGridMaps[i].numY == wakeGrid.numY &&
                   wakeGridMaps[i].wakeGrids.Length == wakeGridMaps[i].wakeGrids.Length && IsSameWakeModel(wakeGridMaps[i].wakeModel, wakeGrid.wakeModel))
                    existsAlready = true;                
            }

            return existsAlready;
        }

        /// <summary> Returns wake model name based on model_type index. </summary>
        public string GetWakeModelName(int modelType)
        {           
            string modelName = "";

            if (modelType == 0)
                modelName = "Eddy Viscosity";
            else if (modelType == 1)
                modelName = "DAWM Eddy Viscosity";
            else if (modelType == 2)
                modelName = "Jensen";

            return modelName;
        }

        /// <summary> Returns wake model with specified settings. </summary>
        public Wake_Model GetWakeModel(int wakeModelType, double horizExp, double ambTI, double DW_Spacing, double CW_Spacing, double ambRough, string powerCurveName, string comboMethod)
        {            
            Wake_Model thisWakeModel = null;

            for (int i = 0; i < NumWakeModels; i++)
            { 
                if (wakeModels[i].wakeModelType == wakeModelType && wakeModels[i].ambTI == ambTI && wakeModels[i].horizWakeExp == horizExp && wakeModels[i].DW_Spacing == DW_Spacing && 
                    wakeModels[i].powerCurve.name == powerCurveName && wakeModels[i].CW_Spacing == CW_Spacing && Math.Round(wakeModels[i].ambRough, 4) == Math.Round(ambRough, 4)
                    && wakeModels[i].comboMethod == comboMethod) {
                    thisWakeModel = wakeModels[i];
                    break;
                }
            }

            return thisWakeModel;
        }

        /// <summary> Returns wake model with same string. </summary>
        public Wake_Model GetWakeModelFromString(string wakeModelString)
        {            
            Wake_Model thisWakeModel = null;

            for (int i = 0; i < NumWakeModels; i++)
            {
                string thisModelString = CreateWakeModelString(wakeModels[i]);
                if (thisModelString == wakeModelString)
                    thisWakeModel = wakeModels[i];
            }
            
            return thisWakeModel;
        }

        /// <summary> Returns wake map with specified settings. </summary>
        public WakeGridMap GetWakeGrid(Wake_Model thisWakeModel, int minX, int minY, int numX, int numY, int reso)
        {            
            WakeGridMap thisWakeGrid = new WakeGridMap();

            for (int i = 0; i < NumWakeGridMaps; i++)
            { 
                if (IsSameWakeModel(wakeGridMaps[i].wakeModel, thisWakeModel) && wakeGridMaps[i].minUTMX == minX && wakeGridMaps[i].minUTMY == minY && wakeGridMaps[i].numX == numX 
                    && wakeGridMaps[i].numY == numY && wakeGridMaps[i].wakeGridReso == reso)
                {
                    thisWakeGrid = wakeGridMaps[i];
                    break;
                }
            }

            return thisWakeGrid;
        }

        /// <summary> Returns index of wake map with specified settings. </summary>
        public int GetWakeGridInd(Wake_Model thisWakeModel, int minX, int minY, int numX, int numY, int reso)
        {            
            int thisInd = 0;

            for (int i = 0; i < NumWakeGridMaps; i++)
            {
                if (IsSameWakeModel(wakeGridMaps[i].wakeModel, thisWakeModel) && wakeGridMaps[i].minUTMX == minX && wakeGridMaps[i].minUTMY == minY && wakeGridMaps[i].numX == numX
                    && wakeGridMaps[i].numY == numY && wakeGridMaps[i].wakeGridReso == reso)
                {
                    thisInd = i;
                    break;
                }
            }

            return thisInd;
        }

        /// <summary> Creates a new wake map with specified X/Y bounds. </summary>
        public WakeGridMap CreateNewWakeGrid(int minX, int minY, int numX, int numY, Wake_Model thisWakeModel)
        {            
            WakeGridMap thisWakeGrid = new WakeGridMap();

            thisWakeGrid.minUTMX = minX;
            thisWakeGrid.minUTMY = minY;
            thisWakeGrid.numX = numX;
            thisWakeGrid.numY = numY;
            thisWakeGrid.wakeModel = thisWakeModel;

            thisWakeGrid.wakeGrids = new WakeGridObj[numX, numY];

            return thisWakeGrid;
        }

        /// <summary> Returns true a wake model with same specified parameters already exists. </summary>
        public bool WakeModelExists(int wakeModelType, double horizExp, double ambTI, string powerCurveName, double DW_Spacing, double CW_Spacing,
                                          double ambRough, string Wake_Combo)
        {           
            bool existsAlready = false;

            for (int i = 0; i < NumWakeModels; i++)
            { 
                if (wakeModels[i].wakeModelType == wakeModelType && wakeModels[i].horizWakeExp == horizExp && wakeModels[i].ambTI == ambTI && 
                    wakeModels[i].powerCurve.name == powerCurveName && wakeModels[i].comboMethod == Wake_Combo)
                {
                    if (wakeModels[i].wakeModelType == 1)
                    {
                        if (wakeModels[i].DW_Spacing == DW_Spacing && wakeModels[i].CW_Spacing == CW_Spacing && wakeModels[i].ambRough == ambRough)
                        {
                            existsAlready = true;
                            break;
                        }
                    }
                    else {
                        existsAlready = true;
                        break;
                    }

                }
            }

            return existsAlready;
        }

        /// <summary> Add wake model to list. </summary>
        public void AddWakeModel(int wakeModelType, double horizExp, double ambTI, TurbineCollection.PowerCurve powerCurve,
                                  double DW_Spacing, double CW_Spacing, double ambRough, string combo)
        {            
            int numExistingModels = NumWakeModels;

            Array.Resize(ref wakeModels, numExistingModels + 1);
            wakeModels[numExistingModels] = new Wake_Model();
            wakeModels[numExistingModels].wakeModelType = wakeModelType;
            wakeModels[numExistingModels].horizWakeExp = horizExp;
            wakeModels[numExistingModels].ambTI = ambTI;
            wakeModels[numExistingModels].powerCurve = powerCurve;
            wakeModels[numExistingModels].comboMethod = combo;
         //   wakeModels[numExistingModels].wakeRechargeRate = wakeRechargeRate;
         //   wakeModels[numExistingModels].wakeRechargeExp = wakeRechargeExp;

            if (wakeModelType == 1)
            { // DAWM
                wakeModels[numExistingModels].DW_Spacing = DW_Spacing;
                wakeModels[numExistingModels].CW_Spacing = CW_Spacing;
                wakeModels[numExistingModels].ambRough = ambRough;
            }

        }

        /// <summary> Add wake map to list. </summary>
        public void AddWakeGridMap(string mapName, int minX, int minY, int NumX, int NumY, int gridReso, Wake_Model thisWakeModel)
        {            
            int numGridMaps = NumWakeGridMaps;

            Array.Resize(ref wakeGridMaps, numGridMaps + 1);
            wakeGridMaps[numGridMaps].name = mapName;
            wakeGridMaps[numGridMaps].minUTMX = minX;
            wakeGridMaps[numGridMaps].minUTMY = minY;
            wakeGridMaps[numGridMaps].numX = NumX;
            wakeGridMaps[numGridMaps].numY = NumY;
            wakeGridMaps[numGridMaps].wakeModel = thisWakeModel;
            wakeGridMaps[numGridMaps].wakeGridReso = gridReso;

            wakeGridMaps[numGridMaps].wakeGrids = new WakeGridObj[NumX, NumY];

        }

        /// <summary> Removes wake map from list. </summary>
        public void RemoveWakeGridMap(WakeGridMap thisGridMap)
        {           
            int newCount = NumWakeGridMaps - 1;

            if (newCount > 0) {
                WakeGridMap[] tempList = new WakeGridMap[newCount];
                int tempIndex = 0;

                for (int i = 0; i < NumWakeGridMaps; i++)
                {
                    if (IsSameWakeGrid(wakeGridMaps[i], thisGridMap) == false) {
                        tempList[tempIndex] = wakeGridMaps[i];
                        tempIndex++;
                    }
                }

                wakeGridMaps = tempList;
            }
            else {
                wakeGridMaps = null;
            }

        }

        /// <summary> Deletes waked map(s) from collection. </summary>
        public void RemoveWakeGridMapByName(string[] mapnames)
        {           
            int numMapsToDelete = mapnames.Length;
            int newCount = NumWakeGridMaps - numMapsToDelete;

            if (newCount <= 0)
                wakeGridMaps = null;
            else
            {
                WakeGridMap[] tempList = new WakeGridMap[newCount];
                int counter = 0;

                for (int i = 0; i < NumWakeGridMaps; i++)
                {
                    bool keepIt = true;
                    for (int j = 0; j < numMapsToDelete; j++)
                    {
                        if (wakeGridMaps[i].name == mapnames[j])
                        {
                            keepIt = false;
                            break;
                        }
                    }

                    if (keepIt == true)
                    {
                        tempList[counter] = wakeGridMaps[i];
                        counter++;
                    }
                }

                wakeGridMaps = tempList;
            }
        }

        /// <summary> Removes wake model from list and removes turbine estimates and maps that used the deleted wake model. </summary>
        public void RemoveWakeModel(TurbineCollection turbineList, MapCollection mapList, Wake_Model thisWakeModel)
        {            
            int newCount = NumWakeModels - 1;

            if (newCount > 0) {
                Wake_Model[] tempList = new Wake_Model[newCount];
                int tempIndex = 0;

                for (int i = 0; i < NumWakeModels; i++)
                {
                    if (IsSameWakeModel(wakeModels[i], thisWakeModel) == false) {
                        tempList[tempIndex] = wakeModels[i];
                        tempIndex++;
                    }
                }

                wakeModels = tempList;
            }
            else {
                wakeModels = null;
            }

            // Remove any wake grids that were created with this wake model
            int newGridCount = 0; 
            for (int i = 0; i < NumWakeGridMaps; i++)
                if (IsSameWakeModel(wakeGridMaps[i].wakeModel, thisWakeModel) == false) 
                    newGridCount++;

            if (newGridCount > 0)
            {
                WakeGridMap[] tempList = new WakeGridMap[newGridCount];
                int tempIndex = 0;

                for (int i = 0; i < NumWakeGridMaps; i++) {
                    if (IsSameWakeModel(wakeGridMaps[i].wakeModel, thisWakeModel) == false) {
                        tempList[tempIndex] = wakeGridMaps[i];
                        tempIndex++;
                    }
                }

                wakeGridMaps = tempList;
            }
            else {
                wakeGridMaps = null;
            }

            // Now remove Net estimates from turbine list that used this wake model
            for (int i = 0; i < turbineList.TurbineCount; i++) {
                turbineList.turbineEsts[i].ClearNetEstsFromAvgWS(thisWakeModel, this);
                turbineList.turbineEsts[i].RemoveNetAEP_byWakeModel(thisWakeModel, this);
            }                       

            // Remove all maps that used this wake model            
            for (int i = 0; i < mapList.ThisCount; i++)
                mapList.RemoveMapByWakeModel(thisWakeModel, this);
            

        }

        /// <summary> Remove wake models based on power curve and delete turbine estimates and maps which used the wake model. </summary>
        public void RemoveWakeModelByPowerCurve(TurbineCollection turbineList, MapCollection mapList, string powerCurve)
        {            
            int newCount = 0;

            for (int i = 0; i < NumWakeModels; i++) 
                if (wakeModels[i].powerCurve.name != powerCurve) 
                    newCount++;

            if (newCount > 0) {
                Wake_Model[] tempList = new Wake_Model[newCount];
                int tempIndex = 0;

                for (int i = 0; i < NumWakeModels; i++) {
                    if (wakeModels[i].powerCurve.name != powerCurve) {
                        tempList[tempIndex] = wakeModels[i];
                        tempIndex++;
                    }
                    else
                    {
                        for (int j = 0; j < turbineList.TurbineCount; j++)
                            turbineList.turbineEsts[j].ClearNetEstsFromAvgWS(wakeModels[i], this);
                    }
                }

                wakeModels = tempList;
            }
            else {
                wakeModels = null;
            }

            // Remove any wake grids that were created with this wake model
            int newGridCount = 0;
            for (int i = 0; i < NumWakeGridMaps; i++)
                if (wakeGridMaps[i].wakeModel.powerCurve.name != powerCurve)
                    newGridCount++;

            if (newGridCount > 0)
            {
                WakeGridMap[] tempList = new WakeGridMap[newGridCount];
                int tempIndex = 0;

                for (int i = 0; i < NumWakeGridMaps; i++) {
                    if (wakeGridMaps[i].wakeModel.powerCurve.name != powerCurve) {
                        tempList[tempIndex] = wakeGridMaps[i];
                        tempIndex++;
                    }
                }

                wakeGridMaps = tempList;
            }
            else {
                wakeGridMaps = null;
            }

            // Now remove Net estimates from turbine list that used this power curve
            for (int i = 0; i < turbineList.TurbineCount; i++) {                                
                turbineList.turbineEsts[i].RemoveNetAEP_byPowerCurve(powerCurve);
            }

            // Remove all maps that used this power curve

            for (int i = 0; i < mapList.ThisCount; i++)
                mapList.RemoveMapByPowerCurve(powerCurve);
            
        }

        /// <summary> Populates wake grid with calculated wind speed, wake loss, and energy production. </summary>
        public void PopulateWakeGrid(Map.MapNode mapNode, int wakeGridInd)
        {            
            int xind = (int)(mapNode.UTMX - wakeGridMaps[wakeGridInd].minUTMX) / wakeGridMaps[wakeGridInd].wakeGridReso;
            int yind = (int)(mapNode.UTMY - wakeGridMaps[wakeGridInd].minUTMY) / wakeGridMaps[wakeGridInd].wakeGridReso;

            // Overall estimates
            wakeGridMaps[wakeGridInd].wakeGrids[xind, yind].wakedWS = mapNode.avgWS_Est;
            wakeGridMaps[wakeGridInd].wakeGrids[xind, yind].netEnergy = mapNode.netEnergyEsts.est;
            wakeGridMaps[wakeGridInd].wakeGrids[xind, yind].wakeLoss = mapNode.netEnergyEsts.wakeLoss;

            // Directional estimates
            wakeGridMaps[wakeGridInd].wakeGrids[xind, yind].sectorWakedWS = mapNode.sectorWS;
            wakeGridMaps[wakeGridInd].wakeGrids[xind, yind].sectorNetEnergy = mapNode.netEnergyEsts.sectorEnergy;
            wakeGridMaps[wakeGridInd].wakeGrids[xind, yind].sectorWakeLoss = mapNode.netEnergyEsts.sectorWakeLoss;

        }

        /// <summary> Creates string containing wake model parameters for wake model list. </summary>
        public string CreateWakeModelString(Wake_Model thisWakeModel)
        {            
            string wakeString = "";

            if (thisWakeModel.wakeModelType == 0)
                wakeString = "Eddy Visc., ";
            else if (thisWakeModel.wakeModelType == 1)
                wakeString = "DAWM, ";
            else if (thisWakeModel.wakeModelType == 2)
                wakeString = "Jensen, ";

            if (thisWakeModel.powerCurve.name.Length > 9)
                wakeString = wakeString + thisWakeModel.powerCurve.name.Substring(0, 10).Trim();
            else
                wakeString = wakeString + thisWakeModel.powerCurve.name.Trim();

            wakeString = wakeString + ", Exp.: " + Math.Round(thisWakeModel.horizWakeExp, 1) + " degs,";
            wakeString = wakeString + " TI: " + Math.Round(thisWakeModel.ambTI, 1) + " %, ";
            wakeString = wakeString + " WRR: " + Math.Round(thisWakeModel.wakeRechargeRate, 3) + ", ";
            wakeString = wakeString + thisWakeModel.comboMethod;

            if (thisWakeModel.wakeModelType == 1)  // DAWM
                wakeString = wakeString + " rough: " + Math.Round(thisWakeModel.ambRough, 2).ToString();

            return wakeString;
        }

        /// <summary> Fits a 4th order polynomial to velocity deficit profile for each X_LengthRD (distance downwind of turbine) and returns 4th order poly coefficients. </summary>
        public double[] CalcWakeProfileFit(double[] velDeficit, double[] R_RD)
        {            
            int numParams = 4;

            double[,] xMatrix = new double[numParams + 1, numParams + 2];
            double[] coeffVector = new double[numParams + 1];

            double[] sumX = new double[numParams * 2 + 1];
            double[] sumYX = new double[numParams + 1];
            double timesX = 1;

            int numPts = velDeficit.Length;

            for (int i = 0; i < numPts; i++)
            {
                timesX = 1;
                sumX[0] = sumX[0] + 1;
                sumYX[0] = sumYX[0] + velDeficit[i];

                for (int j = 1; j <= numParams; j++) {
                    timesX = timesX * R_RD[i];
                    sumX[j] = sumX[j] + timesX;
                    sumYX[j] = sumYX[j] + velDeficit[i] * timesX;
                }

                for (int j = numParams + 1; j <= numParams * 2; j++) {
                    timesX = timesX * R_RD[i];
                    sumX[j] = sumX[j] + timesX;
                }
            }

            for (int i = 0; i <= 3; i++)
                coeffVector[i] = 0;
            
            // Build matrix
            for (int i = 0; i <= numParams; i++) { 
                for (int k = 0; k <= numParams; k++) {
                    xMatrix[i, k] = sumX[i + k];
                }
                xMatrix[i, numParams + 1] = sumYX[i];
            }

            // Triangulate matrix
            int iMax = 0;
            double t = 0;

            //first triangulize the matrix
            for (int i = 0; i <= numParams; i++) {
                iMax = i;
                t = Math.Abs(xMatrix[iMax, i]);
                for (int j = i + 1; j <= numParams; j++) { //find the line with the largest absvalue in this row
                    if (t < Math.Abs(xMatrix[j, i]) ) {
                        iMax = j;
                        t = Math.Abs(xMatrix[iMax, i]);
                    }
                } 
                if (i < iMax) { //exchange the two lines
                    for (int k = i; k <= numParams + 1; k++) {
                        t = xMatrix[i, k];
                        xMatrix[i, k] = xMatrix[iMax, k];
                        xMatrix[iMax, k] = t;
                    } 
                }
                for (int j = i + 1; j <= numParams; j++) { //scale all following lines to have a leading zero
                    t = xMatrix[j, i] / xMatrix[i, i];
                    xMatrix[j, i] = 0.0f;
                    for (int k = i + 1; k <= numParams + 1; k++) {
                        xMatrix[j, k] = xMatrix[j, k] - xMatrix[i, k] * t;
                    } 
                } 
            } 
            //then substitute the coefficients
            for (int j = numParams; j >= 0; j--) {
                t = xMatrix[j, numParams + 1];
                for (int k = j + 1; k <= numParams; k++) {
                    t = t - xMatrix[j, k] * coeffVector[k];
                }
                coeffVector[j] = t / xMatrix[j, j];
            }

            return coeffVector;

        }

        /// <summary> Returns wake loss profile 4th order poly for specified eddy viscosity wake loss model and distance range. </summary>
        public WakeLossCoeffs[] GetWakeLossesCoeffs(int minDistance, int maxDistance, Wake_Model thisWakeModel, MetCollection metList)
        {            
            WakeLossCoeffs[] wakeCoeffs = null;
            int numWakeCoeffs = 0;
            double[,] velDef;
            double freeStream;
            int numWS = metList.numWS;
            double WS_bin_width;
            double WS_dist_first;
            
            if (metList.ThisCount > 0) {
                WS_bin_width = metList.WS_IntSize;
                WS_dist_first = metList.WS_FirstInt;
            }
            else {
                return wakeCoeffs;
            }

            for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
            {
                freeStream = WS_dist_first + WS_ind * WS_bin_width - WS_bin_width / 2;
                if (freeStream > 0 && freeStream >= thisWakeModel.powerCurve.cutInWS && freeStream <= thisWakeModel.powerCurve.cutOutWS) {
                    if (minDistance / thisWakeModel.powerCurve.RD < 2)
                        minDistance = (int)thisWakeModel.powerCurve.RD * 2;

                    velDef = CalcWS_DeficitEddyViscosityGrid((int)Math.Round(minDistance / thisWakeModel.powerCurve.RD,0), (int)Math.Round(maxDistance / thisWakeModel.powerCurve.RD,0), 0.1, 0.025, freeStream, thisWakeModel, metList);

                    double[] R_RD = new double[21];  

                    for (int i = 0; i <= 20; i++) {
                        R_RD[i] = i * 0.025f;
                    }
                    double[] coeffs;

                    int numPts = velDef.GetUpperBound(0) + 1;
                    double[] velDefRad = new double[21];   // Velocity Deficit profile at X_LengthRD

                    for (int i = 0; i < numPts; i++)
                    {
                        for (int radiusInd = 0; radiusInd <= 19; radiusInd++)
                            velDefRad[radiusInd] = velDef[i, radiusInd];

                        velDefRad[20] = 0;
                        coeffs = CalcWakeProfileFit(velDefRad, R_RD); // 
                        Array.Resize(ref wakeCoeffs, numWakeCoeffs + 1);
                        wakeCoeffs[numWakeCoeffs].freeStream = freeStream;
                        wakeCoeffs[numWakeCoeffs].linRegInt = coeffs[0];
                        wakeCoeffs[numWakeCoeffs].linCoeff4 = coeffs[1];
                        wakeCoeffs[numWakeCoeffs].linCoeff3 = coeffs[2];
                        wakeCoeffs[numWakeCoeffs].linCoeff2 = coeffs[3];
                        wakeCoeffs[numWakeCoeffs].linCoeff1 = coeffs[4];
                        wakeCoeffs[numWakeCoeffs].X_LengthRD = (minDistance + i * thisWakeModel.powerCurve.RD * 0.1) / thisWakeModel.powerCurve.RD;

                        numWakeCoeffs++;
                                                
                    }
                }
            }

            return wakeCoeffs;

        }

        /// <summary> Calculates and returns waked WS and net energy for time series estimates. Time int is time interval in hours.
        /// Finds distance to all UW turbines and wake widths and calculates wind speed deficit. Combines wind speed deficits of all UW turbines based on combo method. </summary>
        public double[] CalcNetEnergyTimeSeries(WakeLossCoeffs[] wakeCoeffs, double thisX, double thisY, double freeStream,  
                                         Continuum thisInst, Wake_Model thisWakeModel, double WD, double grossEnergy, double timeInt)        
        {
            double[] wakedResults = new double[2]; // 0 = Waked WS, 1 = Net Energy 
            double netEnergy = 0;
            double wakedWS = 0;
            int numUW_turbs;
            int numWD = thisInst.metList.numWD;
            
            double WS_bin_width;
            double WS_dist_first;

            int freeStreamInt = (int)Math.Round(freeStream, 0);

            if (thisInst.metList.ThisCount > 0)
            {
                WS_bin_width = thisInst.metList.WS_IntSize;
                WS_dist_first = thisInst.metList.WS_FirstInt;
            }
            else
                return wakedResults;
                       
            double DAWM_Def = 0;
            
            Turbine[] UW_turbs = FindAllUW_Turbines(WD, thisX, thisY, thisInst.turbineList);

            if (UW_turbs == null)
                numUW_turbs = 0;
            else
                numUW_turbs = UW_turbs.Length;

            // If there is at least one upwind turbine, calculate wake loss
            if (numUW_turbs > 0)
            {
                double[] WS_Deficit = new double[numUW_turbs];

                double thrustCoeff = thisInst.turbineList.GetInterpPowerOrThrust(freeStream, thisWakeModel.powerCurve, "Thrust");
                double power = thisInst.turbineList.GetInterpPowerOrThrust(freeStream, thisWakeModel.powerCurve, "Power");

                // Ainslie's initial deficit
                double initVelDef = (thrustCoeff - 0.05 - (16 * thrustCoeff - 0.5) * thisWakeModel.ambTI / 1000);
                if (initVelDef < 0) initVelDef = 0.0001f;

                double initWakeWidth = Math.Pow((3.56 * thrustCoeff) / (8 * initVelDef * (1 - 0.5 * initVelDef)), 0.5); // Full width of wake
                initWakeWidth = initWakeWidth / 2; // Half width of wake (which is what is modeled)

                if (freeStream > 0 && power > 0)
                {
                    for (int UW_turb_ind = 0; UW_turb_ind < numUW_turbs; UW_turb_ind++)
                    {
                        double[] DW_andLatDists = CalcDownwindAndLateralDistanceFromUW_Turb(UW_turbs[UW_turb_ind].UTMX, UW_turbs[UW_turb_ind].UTMY, thisX, thisY, thisWakeModel.powerCurve.RD, WD);
                        double DW_Dist = DW_andLatDists[0];
                        double latDist = DW_andLatDists[1];

                        double wakeWidth = CalcWakeWidth(initWakeWidth, thisWakeModel, DW_Dist);
                        double R_ind = latDist / wakeWidth / 2; // Divided by 2 since R_ind = 0 to 0.5 (modeling half of wake)

                        if (DW_Dist > 2 && latDist < wakeWidth)
                        {
                            if (thisWakeModel.wakeModelType == 0)
                            { // Eddy Viscosity Wake Model" 
                                for (int coeffInd = 0; coeffInd <= wakeCoeffs.Length - 1; coeffInd++)
                                {
                                    if (DW_Dist >= 2 && Math.Abs(DW_Dist - wakeCoeffs[coeffInd].X_LengthRD) <= 0.05 && wakeCoeffs[coeffInd].freeStream == freeStreamInt)
                                    {
                                        WS_Deficit[UW_turb_ind] = wakeCoeffs[coeffInd].linCoeff1 * Math.Pow(R_ind, 4) + wakeCoeffs[coeffInd].linCoeff2 * Math.Pow(R_ind, 3)
                                                + wakeCoeffs[coeffInd].linCoeff3 * Math.Pow(R_ind, 2) + wakeCoeffs[coeffInd].linCoeff4 * (R_ind) + wakeCoeffs[coeffInd].linRegInt;

                                        if (WS_Deficit[UW_turb_ind] < 0) WS_Deficit[UW_turb_ind] = 0;
                                        break;
                                    }
                                }
                            }
                            else if (thisWakeModel.wakeModelType == 1)
                            { // "Eddy Viscosity (Deep Array Wind Model)" 
                              // Calculate Eddy viscosity and Deep Array Wake Model and use the maximum deficit
                                for (int coeffInd = 0; coeffInd <= wakeCoeffs.Length - 1; coeffInd++)
                                {
                                    if ((Math.Abs(DW_Dist - wakeCoeffs[coeffInd].X_LengthRD) <= 0.1) && wakeCoeffs[coeffInd].freeStream == freeStreamInt)
                                    {
                                        WS_Deficit[UW_turb_ind] = wakeCoeffs[coeffInd].linCoeff1 * Math.Pow(R_ind, 4) + wakeCoeffs[coeffInd].linCoeff2 * Math.Pow(R_ind, 3)
                                            + wakeCoeffs[coeffInd].linCoeff3 * Math.Pow(R_ind, 2) + wakeCoeffs[coeffInd].linCoeff4 * R_ind + wakeCoeffs[coeffInd].linRegInt;

                                        break;
                                    }
                                }

                                DAWM_Def = Calc_DAWM_Deficit(UW_turbs, thisX, thisY, WD, freeStream, thisWakeModel, thisInst.metList, thisInst.modeledHeight);
                            }
                            else if (thisWakeModel.wakeModelType == 2)
                            {
                                WS_Deficit[UW_turb_ind] = 1 - (1 - Math.Sqrt(1 - thrustCoeff) / Math.Pow(1 + 2 * thisWakeModel.wakeDecayConst * DW_Dist, 2));
                            }
                        }
                    }
                    
                    // Now need to combine WS_Deficit
                    double avgWS_Def = 0;
                    double linearWS_Def = 0;
                    double RSS_WS_Def = 0;
                    double maxWS_Def = 0;
                    double geoWS_Def = 0;

                    if (thisWakeModel.comboMethod == "Linear" || thisWakeModel.comboMethod == "Avg Lin&RSS" || thisWakeModel.comboMethod == "Avg Lin&Max" ||
                    thisWakeModel.comboMethod == "Avg Lin&Geo")
                    {
                        // Using a linear approach which is the sum of the velocity deficits
                        linearWS_Def = 0;
                        for (int UW_turb_ind = 0; UW_turb_ind < numUW_turbs; UW_turb_ind++)                        
                            if (WS_Deficit[UW_turb_ind] > 0)                            
                                linearWS_Def = linearWS_Def + WS_Deficit[UW_turb_ind];                                                   

                        if (linearWS_Def > 1)
                            linearWS_Def = 1;
                    }

                    if (thisWakeModel.comboMethod == "RSS" || thisWakeModel.comboMethod == "Avg Lin&RSS" || thisWakeModel.comboMethod == "Avg RSS&Max" ||
                        thisWakeModel.comboMethod == "Avg RSS&Geo")
                    {
                        // Using a Root Sum Square to find overal velocity deficit
                        RSS_WS_Def = 0;
                        for (int UW_turb_ind = 0; UW_turb_ind < numUW_turbs; UW_turb_ind++)                        
                            if (WS_Deficit[UW_turb_ind] > 0)
                                RSS_WS_Def = RSS_WS_Def + Math.Pow(WS_Deficit[UW_turb_ind], 2);
                        
                        if (RSS_WS_Def > 0)
                            RSS_WS_Def = Math.Pow(RSS_WS_Def, 0.5);

                    }

                    if (thisWakeModel.comboMethod == "Max" || thisWakeModel.comboMethod == "Avg Lin&Max" || thisWakeModel.comboMethod == "Avg RSS&Max" ||
                    thisWakeModel.comboMethod == "Avg Max&Geo")
                    {
                        // Find Max velocity deficit
                        maxWS_Def = 0;
                        for (int UW_turb_ind = 0; UW_turb_ind < numUW_turbs; UW_turb_ind++)
                            if (WS_Deficit[UW_turb_ind] > maxWS_Def)
                                maxWS_Def = WS_Deficit[UW_turb_ind];

                    }

                    if (thisWakeModel.comboMethod == "Geometric" || thisWakeModel.comboMethod == "Avg Lin&Geo" || thisWakeModel.comboMethod == "Avg RSS&Geo" ||
                    thisWakeModel.comboMethod == "Avg Max&Geo")
                    {

                        // Find Geometric avg velocity deficit
                        geoWS_Def = 1;
                        int geoCount = 0;
                        for (int UW_turb_ind = 0; UW_turb_ind < numUW_turbs; UW_turb_ind++)
                        {
                            if (WS_Deficit[UW_turb_ind] > 0)
                            {
                                geoWS_Def = geoWS_Def * (1 - WS_Deficit[UW_turb_ind]);
                                geoCount++;
                            }
                        }

                        if (geoCount > 0)
                            geoWS_Def = 1 - Math.Pow(geoWS_Def, (1 / geoCount));
                        else
                            geoWS_Def = 0;

                    }

                    if (thisWakeModel.comboMethod == "Linear")
                        avgWS_Def = linearWS_Def;
                    else if (thisWakeModel.comboMethod == "RSS")
                        avgWS_Def = RSS_WS_Def;
                    else if (thisWakeModel.comboMethod == "Max")
                        avgWS_Def = maxWS_Def;
                    else if (thisWakeModel.comboMethod == "Geometric")
                        avgWS_Def = geoWS_Def;
                    else if (thisWakeModel.comboMethod == "Avg Lin&RSS")
                        avgWS_Def = (linearWS_Def + RSS_WS_Def) / 2;
                    else if (thisWakeModel.comboMethod == "Avg Lin&Max")
                        avgWS_Def = (linearWS_Def + maxWS_Def) / 2;
                    else if (thisWakeModel.comboMethod == "Avg Lin&Geo")
                        avgWS_Def = (linearWS_Def + geoWS_Def) / 2;
                    else if (thisWakeModel.comboMethod == "Avg RSS&Max")
                        avgWS_Def = (RSS_WS_Def + maxWS_Def) / 2;
                    else if (thisWakeModel.comboMethod == "Avg RSS&Geo")
                        avgWS_Def = (RSS_WS_Def + geoWS_Def) / 2;
                    else if (thisWakeModel.comboMethod == "Avg Max&Geo")
                        avgWS_Def = (maxWS_Def + geoWS_Def) / 2;

                    if (thisWakeModel.wakeModelType == 1)
                    { // compare DAWM deficit to Eddy Viscosity deficit and take the max
                        if (avgWS_Def < DAWM_Def)
                        {
                            avgWS_Def = DAWM_Def;
                        }
                    }
                    if (avgWS_Def >= freeStream)
                        wakedWS = 0.01f;
                    else
                        wakedWS = freeStream * (1 - avgWS_Def);

                    if (wakedWS == 0)
                        wakedWS = 0.05; // no zero wind speeds

                    power = thisInst.turbineList.GetInterpPowerOrThrust(wakedWS, thisWakeModel.powerCurve, "Power");
                }
                
                netEnergy = power * timeInt;
            }

            else
            {
                // No turbines creating wakes
                wakedWS = freeStream;
                netEnergy = grossEnergy;

            }                        

            wakedResults[0] = wakedWS;
            wakedResults[1] = netEnergy;

            return wakedResults;

        }

        /// <summary> For each WD sector, finds distance to all UW turbines and wake widths and calculates wind speed deficit. Combines wind speed deficits of all UW turbines based on combo method. </summary>        
        public WakeCalcResults CalcWakeLosses(WakeLossCoeffs[] wakeCoeffs, double thisX, double thisY, double[,] freeSectorWS_Dist, double grossAEP, double[] grossAEPSect,
                                         Continuum thisInst, Wake_Model thisWakeModel, double[] windRose)         
        {
            WakeCalcResults wakeResults = new WakeCalcResults();
            
            int numUW_turbs;
            int numWD = thisInst.metList.numWD;
            if (numWD == 0) return wakeResults;
            int numWS = thisInst.metList.numWS;            
                      
            double WS_bin_width;
            double WS_dist_first;

            if (thisInst.metList.ThisCount > 0) {
                WS_bin_width = thisInst.metList.WS_IntSize;
                WS_dist_first = thisInst.metList.WS_FirstInt;
            }
            else
                return wakeResults;

            wakeResults.sectorDist = new double[numWD, numWS];                       

            double DAWM_Def = 0;

            for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
            {
                double WD = (double)WD_Ind / numWD * 360.0f;
                Turbine[] UW_turbs = FindAllUW_Turbines(WD, thisX, thisY, thisInst.turbineList);

                if (UW_turbs == null)
                    numUW_turbs = 0;
                else
                    numUW_turbs = UW_turbs.Length;               

                if (numUW_turbs > 0) {                    
                    double[] avgWakedDist = new double[numWS];                     
                    
                    for (int subWD_ind = 0; subWD_ind <= 3; subWD_ind++)
                    {
                        double subWD = WD - (360.0f / numWD) / 2 + (360.0f / numWD) / 4 / 2 + subWD_ind * (360.0f / numWD) / 4;
                        double[] WS_Deficit = new double[numUW_turbs];
                        double[] wakedDistWS = new double[numWS];
                        double WD_sector = subWD;

                        for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
                        {
                            double freeStream = WS_dist_first + WS_ind * WS_bin_width - WS_bin_width / 2;
                            double thrustCoeff = thisInst.turbineList.GetInterpPowerOrThrust(freeStream, thisWakeModel.powerCurve, "Thrust");

                            // Ainslie's initial deficit
                            double initVelDef = (thrustCoeff - 0.05 - (16 * thrustCoeff - 0.5) * thisWakeModel.ambTI / 1000);
                            if (initVelDef < 0) initVelDef = 0.0001f;

                            double initWakeWidth = Math.Pow((3.56 * thrustCoeff) / (8 * initVelDef * (1 - 0.5 * initVelDef)), 0.5); // Full width of wake
                            initWakeWidth = initWakeWidth / 2; // Half width of wake (which is what is modeled)

                            double thisPower = thisInst.turbineList.GetInterpPowerOrThrust(freeStream, thisWakeModel.powerCurve, "Power");

                            if (freeStream > 0 && thisPower > 0)
                            {
                                for (int UW_turb_ind = 0; UW_turb_ind < numUW_turbs; UW_turb_ind++)
                                {
                                    double[] DW_andLatDists = CalcDownwindAndLateralDistanceFromUW_Turb(UW_turbs[UW_turb_ind].UTMX, UW_turbs[UW_turb_ind].UTMY, thisX, thisY, thisWakeModel.powerCurve.RD, WD_sector);
                                    double DW_Dist = DW_andLatDists[0];
                                    double latDist = DW_andLatDists[1];

                                    double wakeWidth = CalcWakeWidth(initWakeWidth, thisWakeModel, DW_Dist);
                                    double R_ind = latDist / wakeWidth / 2; // Divided by 2 since R_ind = 0 to 0.5 (modeling half of wake)

                                    if (DW_Dist > 0.2 && latDist < wakeWidth) {
                                        if (thisWakeModel.wakeModelType == 0)
                                        { // Eddy Viscosity Wake Model" 
                                            for (int coeffInd = 0; coeffInd <= wakeCoeffs.Length - 1; coeffInd++)
                                            {
                                                if (DW_Dist >= 0.2 && Math.Abs(DW_Dist - wakeCoeffs[coeffInd].X_LengthRD) <= 0.05 && wakeCoeffs[coeffInd].freeStream == freeStream)
                                                {
                                                    WS_Deficit[UW_turb_ind] = wakeCoeffs[coeffInd].linCoeff1 * Math.Pow(R_ind, 4) + wakeCoeffs[coeffInd].linCoeff2 * Math.Pow(R_ind, 3)
                                                         + wakeCoeffs[coeffInd].linCoeff3 * Math.Pow(R_ind, 2) + wakeCoeffs[coeffInd].linCoeff4 * (R_ind) + wakeCoeffs[coeffInd].linRegInt;

                                                    if (WS_Deficit[UW_turb_ind] < 0) WS_Deficit[UW_turb_ind] = 0;
                                                    break;
                                                }
                                            }
                                        }
                                        else if (thisWakeModel.wakeModelType == 1)
                                        { // "Eddy Viscosity (Deep Array Wind Model)" 
                                            // Calculate Eddy viscosity and Deep Array Wake Model and use the maximum deficit
                                            for (int coeffInd = 0; coeffInd <= wakeCoeffs.Length - 1; coeffInd++)
                                            {
                                                if ((Math.Abs(DW_Dist - wakeCoeffs[coeffInd].X_LengthRD) <= 0.1) && wakeCoeffs[coeffInd].freeStream == freeStream)
                                                {
                                                    WS_Deficit[UW_turb_ind] = wakeCoeffs[coeffInd].linCoeff1 * Math.Pow(R_ind, 4) + wakeCoeffs[coeffInd].linCoeff2 * Math.Pow(R_ind, 3)
                                                        + wakeCoeffs[coeffInd].linCoeff3 * Math.Pow(R_ind, 2) + wakeCoeffs[coeffInd].linCoeff4 * R_ind + wakeCoeffs[coeffInd].linRegInt;

                                                    break;
                                                }
                                            }

                                            DAWM_Def = Calc_DAWM_Deficit(UW_turbs, thisX, thisY, subWD, freeStream, thisWakeModel, thisInst.metList, thisInst.modeledHeight);
                                        }
                                        else if (thisWakeModel.wakeModelType == 2)
                                        {
                                            WS_Deficit[UW_turb_ind] = 1 - (1 - Math.Sqrt(1 - thrustCoeff) / Math.Pow(1 + 2 * thisWakeModel.wakeDecayConst * DW_Dist, 2));
                                        }
                                    }

                                    WD_sector = subWD;
                                }

                                // Now need to combine WS_Deficit
                                double avgWS_Def = 0;
                                double linearWS_Def = 0;
                                double RSS_WS_Def = 0;
                                double maxWS_Def = 0;
                                double geoWS_Def = 0;

                                if (thisWakeModel.comboMethod == "Linear" || thisWakeModel.comboMethod == "Avg Lin&RSS" || thisWakeModel.comboMethod == "Avg Lin&Max" ||
                                thisWakeModel.comboMethod == "Avg Lin&Geo") {
                                    // Using a linear approach which is the sum of the velocity deficits
                                    linearWS_Def = 0;
                                    for (int UW_turb_ind = 0; UW_turb_ind < numUW_turbs; UW_turb_ind++) {
                                        if (WS_Deficit[UW_turb_ind] > 0) {
                                            linearWS_Def = linearWS_Def + WS_Deficit[UW_turb_ind];                                            
                                        }
                                    }
                                }

                                if (thisWakeModel.comboMethod == "RSS" || thisWakeModel.comboMethod == "Avg Lin&RSS" || thisWakeModel.comboMethod == "Avg RSS&Max" ||
                                    thisWakeModel.comboMethod == "Avg RSS&Geo") {
                                    // Using a Root Sum Square to find overal velocity deficit
                                    RSS_WS_Def = 0;
                                    for (int UW_turb_ind = 0; UW_turb_ind < numUW_turbs; UW_turb_ind++) {
                                        if (WS_Deficit[UW_turb_ind] > 0)
                                            RSS_WS_Def = RSS_WS_Def + Math.Pow(WS_Deficit[UW_turb_ind], 2);

                                    }
                                    if (RSS_WS_Def > 0)
                                        RSS_WS_Def = Math.Pow(RSS_WS_Def, 0.5);

                                }

                                if (thisWakeModel.comboMethod == "Max" || thisWakeModel.comboMethod == "Avg Lin&Max" || thisWakeModel.comboMethod == "Avg RSS&Max" ||
                                thisWakeModel.comboMethod == "Avg Max&Geo") {
                                    // Find Max velocity deficit
                                    maxWS_Def = 0;
                                    for (int UW_turb_ind = 0; UW_turb_ind < numUW_turbs; UW_turb_ind++)
                                        if (WS_Deficit[UW_turb_ind] > maxWS_Def)
                                            maxWS_Def = WS_Deficit[UW_turb_ind];

                                }

                                if (thisWakeModel.comboMethod == "Geometric" || thisWakeModel.comboMethod == "Avg Lin&Geo" || thisWakeModel.comboMethod == "Avg RSS&Geo" ||
                                thisWakeModel.comboMethod == "Avg Max&Geo") {

                                    // Find Geometric avg velocity deficit
                                    geoWS_Def = 1;
                                    int geoCount = 0;
                                    for (int UW_turb_ind = 0; UW_turb_ind < numUW_turbs; UW_turb_ind++) {
                                        if (WS_Deficit[UW_turb_ind] > 0) {
                                            geoWS_Def = geoWS_Def * (1 - WS_Deficit[UW_turb_ind]);
                                            geoCount++;
                                        }
                                    }

                                    if (geoCount > 0)
                                        geoWS_Def = 1 - Math.Pow(geoWS_Def, (1 / geoCount));
                                    else
                                        geoWS_Def = 0;

                                }

                                if (thisWakeModel.comboMethod == "Linear")
                                    avgWS_Def = linearWS_Def;
                                else if (thisWakeModel.comboMethod == "RSS")
                                    avgWS_Def = RSS_WS_Def;
                                else if (thisWakeModel.comboMethod == "Max")
                                    avgWS_Def = maxWS_Def;
                                else if (thisWakeModel.comboMethod == "Geometric")
                                    avgWS_Def = geoWS_Def;
                                else if (thisWakeModel.comboMethod == "Avg Lin&RSS")
                                    avgWS_Def = (linearWS_Def + RSS_WS_Def) / 2;
                                else if (thisWakeModel.comboMethod == "Avg Lin&Max")
                                    avgWS_Def = (linearWS_Def + maxWS_Def) / 2;
                                else if (thisWakeModel.comboMethod == "Avg Lin&Geo")
                                    avgWS_Def = (linearWS_Def + geoWS_Def) / 2;
                                else if (thisWakeModel.comboMethod == "Avg RSS&Max")
                                    avgWS_Def = (RSS_WS_Def + maxWS_Def) / 2;
                                else if (thisWakeModel.comboMethod == "Avg RSS&Geo")
                                    avgWS_Def = (RSS_WS_Def + geoWS_Def) / 2;
                                else if (thisWakeModel.comboMethod == "Avg Max&Geo")
                                    avgWS_Def = (maxWS_Def + geoWS_Def) / 2;

                                if (thisWakeModel.wakeModelType == 1) { // compare DAWM deficit to Eddy Viscosity deficit and take the max
                                    if (avgWS_Def != 0 || DAWM_Def != 0)
                                        avgWS_Def = avgWS_Def;

                                    if (avgWS_Def < DAWM_Def) {
                                        avgWS_Def = DAWM_Def;
                                    }
                                }
                                if (avgWS_Def >= freeStream)
                                    wakedDistWS[WS_ind] = 0.01f;
                                else
                                    wakedDistWS[WS_ind] = freeStream * (1 - avgWS_Def);

                            }
                            else {
                                wakedDistWS[WS_ind] = freeStream;
                            }

                        }

                        // Now interpolate to find waked WS distribution
                        double[] thisDist = new double[numWS];
                        for (int i = 0; i < numWS; i++)
                            thisDist[i] = freeSectorWS_Dist[WD_Ind, i];

                        double[] WS_DistWaked = InterpolateFindWakedDist(wakedDistWS, thisDist, thisInst.metList);

                        for (int i = 0; i < numWS; i++)
                            avgWakedDist[i] = avgWakedDist[i] + WS_DistWaked[i];
                        
                    }

                    for (int i = 0; i < numWS; i++)
                        wakeResults.sectorDist[WD_Ind, i] = avgWakedDist[i] / 4;

                }
                else {
                    // No turbines creating wakes in this WD sector
                    for (int i = 0; i < numWS; i++)
                        wakeResults.sectorDist[WD_Ind, i] = freeSectorWS_Dist[WD_Ind, i];
                    
                }
            }

            // Now calculate Waked overall WS dist, sector WS and overall avg WS

            wakeResults.sectorWakedWS = CalcAvgSectorWaked(wakeResults.sectorDist, thisInst.metList);
            wakeResults.WS_Dist = CalcAvgWakedWS_Dists(wakeResults.sectorDist, windRose);
            wakeResults.wakedWS = CalcAvgWakedWS(wakeResults.WS_Dist, thisInst.metList);

            double otherLoss = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);

            wakeResults.netEnergy = CalcNetEnergy(thisWakeModel, wakeResults.WS_Dist, thisInst, otherLoss);
            wakeResults.sectorNetEnergy = CalcNetSectEnergy(thisWakeModel, wakeResults.sectorDist, windRose, thisInst, otherLoss);            
            wakeResults.sectorWakeLoss = CalcThisSectWakeLoss(wakeResults.sectorNetEnergy, grossAEPSect, otherLoss);
            wakeResults.wakeLoss = CalcThisWakeLoss(wakeResults.netEnergy, grossAEP, otherLoss);

            return wakeResults;

        }

        /// <summary> Calculates and returns P50 net annual energy production (wake and other losses included) </summary>
        public double CalcNetEnergy(Wake_Model thisWakeModel, double[] WS_Dist, Continuum thisInst, double otherLoss)
        {            
            int numWS = 0;
                        
            try {
                numWS = WS_Dist.Length;
            }
            catch  { 
                return 0;
            }

            double P50_AEP = 0;
            double sumWS = 0;

            for (int k = 0; k < numWS; k++) {
                double thisWS = thisInst.metList.GetWS_atWS_Ind(k);
                double thisPower = thisInst.turbineList.GetInterpPowerOrThrust(thisWS, thisWakeModel.powerCurve, "Power");
                P50_AEP = P50_AEP + WS_Dist[k] * thisPower;
                sumWS = sumWS + WS_Dist[k];
            }

            P50_AEP = otherLoss * P50_AEP / sumWS * 365 * 24 / 1000;

            return P50_AEP;
        }

        /// <summary> Calculates and returns P50 net AEP by wind direction sector (wake and other losses included) </summary>
        public double[] CalcNetSectEnergy(Wake_Model thisWakeModel, double[,] sectorWS_Dist, double[] windRose, Continuum thisInst, double otherLoss)
        {            
            int numWS = 0;
            int numWD = 0;
            TurbineCollection.PowerCurve thisPowerCurve = thisWakeModel.powerCurve;

            try {
                numWS = sectorWS_Dist.GetUpperBound(1) + 1;
            }
            catch  {
                return null;
            }

            try {
                numWD = sectorWS_Dist.GetUpperBound(0) + 1;
            }
            catch  {
                return null;
            }

            double[] P50_AEP = new double[numWD];
                        
            for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++) {
                double sumWS = 0;
                for (int k = 0; k <= numWS - 1; k++) {
                    double thisWS = thisInst.metList.GetWS_atWS_Ind(k);
                    double thisPower = thisInst.turbineList.GetInterpPowerOrThrust(thisWS, thisPowerCurve, "Power");
                    P50_AEP[WD_Ind] = P50_AEP[WD_Ind] + sectorWS_Dist[WD_Ind, k] * thisPower;
                    sumWS = sumWS + sectorWS_Dist[WD_Ind, k];
                }
                P50_AEP[WD_Ind] = otherLoss * P50_AEP[WD_Ind] / sumWS * 365 * 24 * windRose[WD_Ind] / 1000;                
            }

            return P50_AEP;
        }

        /// <summary> Calculates and returns the wake loss (%) </summary>
        public double CalcThisWakeLoss(double P50_AEP, double grossAEP, double otherLoss)
        {            
            double wakeLoss = 1 - P50_AEP / otherLoss / grossAEP;

            if (wakeLoss < 0) wakeLoss = 0;

            return wakeLoss;
        }

        /// <summary> Calculates and returns sectorwise wake loss </summary>
        public double[] CalcThisSectWakeLoss(double[] P50_AEP, double[] grossAEP, double otherLoss)
        {            
            double[] wakeLoss = null;
            int numWD = 0;

            try {
                numWD = P50_AEP.Length;
                wakeLoss = new double[numWD];
            }
            catch {
                return wakeLoss;
            }

            for (int i = 0; i <= numWD - 1; i++) {
                wakeLoss[i] = 1 - (P50_AEP[i] / otherLoss) / grossAEP[i];
                if (wakeLoss[i] < 0) wakeLoss[i] = 0;
            }

            return wakeLoss;

        }

        /// <summary> Calculates and returns the average waked wind speed in each sector </summary>
        public double[] CalcAvgSectorWaked(double[,] sectorWS_Dist, MetCollection metList)
        {            
            int numWD = 0;
            int numWS = 0;
            double WS_First;
            double WS_Int;

            try {
                WS_First = metList.WS_FirstInt;
                WS_Int = metList.WS_IntSize;
            }
            catch {
                return null;
            }
            try {
                numWD = sectorWS_Dist.GetUpperBound(0) + 1;
                numWS = sectorWS_Dist.GetUpperBound(1) + 1;
            }
            catch {
                return null;
            }
                       
            double[] sectorWS = new double[numWD];  

            //Calculate sectorwise wind speeds
            for (int i = 0; i < numWD; i++) {
                double sumWS_Freq = 0;
                double sumFreq = 0;
                for (int j = 0; j <= numWS - 1; j++) {
                    double thisWS = WS_First + j * WS_Int - WS_Int / 2;
                    double thisFreq = sectorWS_Dist[i, j];
                    sumFreq = sumFreq + thisFreq;
                    sumWS_Freq = sumWS_Freq + thisWS * thisFreq;
                }

                sectorWS[i] = sumWS_Freq / sumFreq;
            }

            return sectorWS;
        }

        /// <summary> Calculate and returns overall waked WS distribution </summary>
        public double[] CalcAvgWakedWS_Dists(double[,] sectorWS_Dist, double[] windRose)
        {            
            int numWD = 0;
            int numWS = 0;

            try {
                numWS = sectorWS_Dist.GetUpperBound(1) + 1;
                numWD = sectorWS_Dist.GetUpperBound(0) + 1;
            }
            catch  {

            }
                        
            double[] WS_Dist = new double[numWS];
            
            for (int i = 0; i <= numWS - 1; i++) {
                double sumRose = 0;
                double thisFreq = 0;
                for (int j = 0; j <= numWD - 1; j++) {
                    thisFreq = thisFreq + sectorWS_Dist[j, i] * windRose[j];
                    sumRose = sumRose + windRose[j];
                }
                WS_Dist[i] = thisFreq / sumRose;
            }

            return WS_Dist;
        }

        /// <summary> Calculates and returns average overall waked wind speed. </summary>
        public double CalcAvgWakedWS(double[] WS_Dist, MetCollection metList)
        {            
            int numWS = 0;
            double WS_First = 0;
            double WS_Int = 0;

            try {
                WS_First = metList.WS_FirstInt;
                WS_Int = metList.WS_IntSize;
            }
            catch  {
                return 0;
            }
            try {
                numWS = WS_Dist.Length;
            }
            catch {
                return 0;
            }

            double thisWS = 0;
            double sumDist = 0;

            for (int i = 0; i < numWS; i++) {
                thisWS = thisWS + WS_Dist[i] * (WS_First + i * WS_Int - WS_Int / 2);
                sumDist = sumDist + WS_Dist[i];
            }

            thisWS = thisWS / sumDist;

            return thisWS;
        }

        /// <summary> Takes wake deficit distribution for each wind speed and calculates and returns resulting wind speed distribution. </summary>
        public double[] InterpolateFindWakedDist(double[] wakedDistWS, double[] unwakedDist, MetCollection metList)
        {          
            int numWS = 0;            
            double WS_int = 0;
            double WS_first = 0;
            double[] interpDist = null;

            try {
                numWS = wakedDistWS.Length;
            }
            catch  {
                return interpDist;
            }

            try {
                WS_int = metList.WS_IntSize;
                WS_first = metList.WS_FirstInt;
            }
            catch  {
                return interpDist;
            }
                                    
            interpDist = new double[numWS];

            for (int i = 0; i < numWS; i++) {
                double thisWS = WS_first + WS_int * i - WS_int / 2;
                                
                // Loop through wakedDistWS and find all with same WS at thisWS
                for (int j = 0; j <= numWS - 1; j++) {
                    if (thisWS > (wakedDistWS[j] - WS_int / 2) && thisWS < (wakedDistWS[j] + WS_int / 2))
                        interpDist[i] = interpDist[i] + unwakedDist[j];                     
                }
            }

            // Normalize to sum to 1.000
            double distSum = 0;
            for (int i = 0; i < numWS; i++)
                distSum = distSum + interpDist[i];

            for (int i = 0; i < numWS; i++)
                interpDist[i] = interpDist[i] / distSum;

            return interpDist;

        }

        /// <summary> Calculate and return "equivalent" roughness used in DAWM. </summary>
        public double CalcEquivRoughness(MetCollection metList, double freeStream, Wake_Model thisWakeModel, double hubHeight)
        {            
            double WS_First = 0;
            double WS_Int = 0;
            int numWS = metList.numWS;
            double thrustCoeff = 0;            
            
            double DW_Spacing = thisWakeModel.DW_Spacing;
            double CW_Spacing = thisWakeModel.CW_Spacing;
            double z0 = thisWakeModel.ambRough;                     

            try {
                WS_First = metList.WS_FirstInt;
                WS_Int = metList.WS_IntSize;                
            }
            catch  {
                return 0;
            }

            TurbineCollection turbList = new TurbineCollection();
            
            for (int i = 0; i < numWS; i++) { 
                if (freeStream == WS_First + i * WS_Int - WS_Int / 2) {
                    thrustCoeff = turbList.GetInterpPowerOrThrust(freeStream, thisWakeModel.powerCurve, "Thrust");
                    break;
                }
            }

            double distThrust = (thrustCoeff * Math.PI / (8 * DW_Spacing * CW_Spacing));
            double equivRough = hubHeight * Math.Exp(-vonKarman / Math.Pow((distThrust + Math.Pow((vonKarman / Math.Log(hubHeight / z0)), 2)), 0.5));

            return equivRough;
        }

        /// <summary> Used in DAWM, this is the height of the first internal boundary layer (IBL) calculated based on distance, distributed thrust coeff,
        /// and turbine roughness. Initial IBL height equal to top of rotor. </summary>
        public double Calc_IBL_H1(Turbine UW_turb, double UTMX, double UTMY, Wake_Model thisWakeModel, double WD_sector, double equivRough, double hubHeight)
        {
            double H1 = 0;
            double RD = thisWakeModel.powerCurve.RD;                        

            double[] DW_andLatDists = CalcDownwindAndLateralDistanceFromUW_Turb(UW_turb.UTMX, UW_turb.UTMY, UTMX, UTMY, RD, WD_sector);            
            double DW_Dist = DW_andLatDists[0]; 
            double latDist = DW_andLatDists[1];

            if (DW_Dist > 0)
            {
                // Calculate height of IBL               
                double initWakeWidth = 0.8f;
                double wakeWidth = CalcWakeWidth(initWakeWidth, thisWakeModel, DW_Dist);

                if (latDist < wakeWidth)
                { // it's in the IBL created by UW turbine 
                  // Initial height is top of rotor
                    
                    int counter = 0;

                    H1 = hubHeight + RD / 2;
                    double leftSide = (H1 * (Math.Log(H1 / equivRough) - 1));
                    double rightSide = equivRough * (DW_Dist * RD / equivRough - 1);
                    double diff = leftSide - rightSide;

                    while (Math.Abs(diff) > 0.5 && counter < 1000)
                    {
                        if (diff < 0)  // increase H1
                            H1 = H1 + Math.Abs(diff) / 10;
                        else  // decrease H1
                            H1 = H1 - Math.Abs(diff) / 10;

                        leftSide = (H1 * (Math.Log(H1 / equivRough) - 1));
                        rightSide = equivRough * (DW_Dist * RD / equivRough - 1);
                        diff = leftSide - rightSide;
                        counter++;
                    }

                    H1 = H1 + hubHeight + RD / 2; 
                }
            }
            return H1;
        }

        /// <summary> Calculate and return downwind and lateral distance from specified upwind turbine. </summary>
        public double[] CalcDownwindAndLateralDistanceFromUW_Turb(double UW_UTMX, double UW_UTMY, double Site_UTMX, double Site_UTMY, double RD, double WD)
        {
            TopoInfo topo = new TopoInfo();
            WD = 90 - WD;
            // Calculate distance and angle between target site and (potentially upwind) turbine
            double Theta_Site_to_Turb = 180.0f / Math.PI * Math.Atan2(UW_UTMY - Site_UTMY, UW_UTMX - Site_UTMX);
            double Dist_Site_to_Turb = topo.CalcDistanceBetweenPoints(UW_UTMX, UW_UTMY, Site_UTMX, Site_UTMY);

            // Calculate downwind distance (in RD) from turbine to target site. If it's negative then the target site is on the other side of turbine (i.e. not downwind)
            double DW_Dist = Dist_Site_to_Turb / RD * Math.Cos(Math.PI / 180.0f * (Theta_Site_to_Turb - WD));

            // Calculate lateral distance (in RD) from turbine to target site. Finding absolute distance since sign indicates which side of the centerline it is (which doesn't matter for this)
            double latDist = Math.Abs(Dist_Site_to_Turb / RD * Math.Sin(Math.PI / 180.0f * (Theta_Site_to_Turb - WD)));

            double[] Results = new double[2];
            Results[0] = DW_Dist;
            Results[1] = latDist;

            return Results;
        }

        /// <summary> Used in DAWM, this is the height of the second internal boundary layer (IBL) calculated based on distance and ambient (background) roughness. </summary>
        public double Calc_IBL_H2(Turbine UW_turb, double UTMX, double UTMY, Wake_Model thisWakeModel, double WD_sector, MetCollection metList, double hubHeight)
        {            
            // Initial IBL height equal to bottom of rotor
            double H2 = 1000000;
            double RD = thisWakeModel.powerCurve.RD;
            double WS_First = 0;
            double WS_Int = 0;

            try
            {
                WS_First = metList.WS_FirstInt;
                WS_Int = metList.WS_IntSize;
            }
            catch 
            {
                return 0;
            }                       

            double[] DW_andLatDists = CalcDownwindAndLateralDistanceFromUW_Turb(UW_turb.UTMX, UW_turb.UTMY, UTMX, UTMY, RD, WD_sector);
            double DW_Dist = DW_andLatDists[0];
            double latDist = DW_andLatDists[1];

            if (DW_Dist > 0)
            {
                // Calculate height of IBL                 
                double initWakeWidth = 0.8f;
                double wakeWidth = CalcWakeWidth(initWakeWidth, thisWakeModel, DW_Dist);

                if (latDist < wakeWidth)
                { // it's in the IBL created by UW turbine 
                    // Initial height is bottom of rotor
                    
                    int counter = 0;
                    H2 = hubHeight - RD / 2;
                    double leftSide = H2 * (Math.Log(H2 / thisWakeModel.ambRough) - 1);
                    double rightSide = thisWakeModel.ambRough * (DW_Dist * RD / thisWakeModel.ambRough - 1);
                    double diff = leftSide - rightSide;

                    while (Math.Abs(diff) > 0.5 && counter < 1000)
                    {
                        if (diff < 0)  // increase H1
                            H2 = H2 + Math.Abs(diff) / 10;
                        else  // decrease H1
                            H2 = H2 - Math.Abs(diff) / 10;

                        leftSide = H2 * (Math.Log(H2 / thisWakeModel.ambRough) - 1);
                        rightSide = thisWakeModel.ambRough * (DW_Dist * RD / thisWakeModel.ambRough - 1);
                        diff = leftSide - rightSide;
                        counter++;
                    }

                    H2 = H2 + hubHeight - RD / 2;

                }
            }
            return H2;
        }

        /// <summary> Calculate and return the wind speed deficit based on DAWM. </summary>
        public double Calc_DAWM_Deficit(Turbine[] UW_turbs, double UTMX, double UTMY, double WD_sector, double freeStream, Wake_Model thisWakeModel, MetCollection metList, double hubHeight)
        {            
            double velDeficit = 0;                       
            double IBL_H1 = 0;
            double IBL_H2 = 1000000;
            int numUW_turbs = 0;
            
            try {
                numUW_turbs = UW_turbs.Length;
            }
            catch {
                return velDeficit;
            }

            double equivRough = CalcEquivRoughness(metList, freeStream, thisWakeModel, hubHeight);

            // Find the maximum IBL H1 and the minimum IBL H2 (greater than hub height)
            for (int i = 0; i < numUW_turbs; i++) {
                double This_H1 = Calc_IBL_H1(UW_turbs[i], UTMX, UTMY, thisWakeModel, WD_sector, equivRough, hubHeight);
                double This_H2 = Calc_IBL_H2(UW_turbs[i], UTMX, UTMY, thisWakeModel, WD_sector, metList, hubHeight);

                if (This_H1 > IBL_H1)
                    IBL_H1 = This_H1;

                if (This_H2 >= hubHeight && This_H2 < IBL_H2)
                    IBL_H2 = This_H2;               

            }

            if (IBL_H1!= 0 && IBL_H2!= 1000000 ) {
                double WS_Ratio = (Math.Log(IBL_H1 / thisWakeModel.ambRough) * Math.Log(IBL_H2 / equivRough) / (Math.Log(IBL_H2 / thisWakeModel.ambRough) * Math.Log(IBL_H1 / equivRough)));
                //  velDeficit = freeStream - WS_Ratio * freeStream
                velDeficit = 1 - WS_Ratio;
            }

            return velDeficit;
        }

        /// <summary> Calculate and return grid of wind speed deficit based on eddy viscosity model. i = distance behind rotor, j = distance away from centerline. </summary>
        public double[,] CalcWS_DeficitEddyViscosityGrid(double minDistRD, double maxDistRD, double X_Reso_RD, double R_Reso_RD, double freeStream, Wake_Model thisWakeModel, MetCollection metList)
        {            
            double[,] velDeficit = null;
            double k1 = 0.015f;

            double RD = thisWakeModel.powerCurve.RD;

            double WS_First = 0;
            double WS_Int = 0;

            try {
                WS_First = metList.WS_FirstInt;
                WS_Int = metList.WS_IntSize;
            }
            catch {
                return velDeficit;
            }

            int numWS = 0;            

            try {
                numWS = thisWakeModel.powerCurve.thrustCoeff.Length;
            }
            catch  {
                return velDeficit;
            }            

            TurbineCollection turbList = new TurbineCollection();
            double thrustCoeff = turbList.GetInterpPowerOrThrust(freeStream, thisWakeModel.powerCurve, "Thrust");
           
            double k = X_Reso_RD;
            double h = R_Reso_RD;
            double r = 0;
            double Km = Math.Pow(vonKarman, 2) * thisWakeModel.ambTI / 100;
            
            // Ainslie's initial deficit
            double initVelDef = (thrustCoeff - 0.05 - (16 * thrustCoeff - 0.5) * thisWakeModel.ambTI / 1000);

            // Liz's initial deficit(Horns Rev validation)
            //initVelDef = thrustCoeff - (thrustCoeff - 0.5) * thisWakeModel.ambTI / 1000

            if (initVelDef < 0) initVelDef = 0.0001f;

            double initWakeWidth = Math.Pow((3.56 * thrustCoeff) / (8 * initVelDef * (1 - 0.5 * initVelDef)), 0.5); // Full width of wake
            initWakeWidth = initWakeWidth / 2; // Half width of wake (which is what is modeled)            
            int Num_Rs = Convert.ToInt16(1 / R_Reso_RD / 2); // why is this divided by 2??
            if (minDistRD < 2) minDistRD = 2;

            int X_ind = 1 + Convert.ToInt16((maxDistRD - minDistRD) / X_Reso_RD);
            velDeficit = new double[X_ind, Num_Rs];

            double[] U_spd = new double[Num_Rs];  
            double[] V_spd = new double[Num_Rs];  
            double[] U_spd_last = new double[Num_Rs];  
            double[] V_spd_last = new double[Num_Rs];  
            double[] a_mtrx = new double[Num_Rs];  
            double[] b_mtrx = new double[Num_Rs];  
            double[] c_mtrx = new double[Num_Rs];  
            double[] r_mtrx = new double[Num_Rs];  
            double[] c_prime = new double[Num_Rs];  
            double[] r_prime = new double[Num_Rs];
                              
            int Vel_Def_Count = 0;

            // First calculate the U_spd at RD = 2
            U_spd_last[0] = freeStream - freeStream * initVelDef;

            if (minDistRD == 2) {
                velDeficit[0, 0] = 1 - U_spd_last[0] / freeStream;
                Vel_Def_Count++;
            }

            for (int r_ind = 1; r_ind <= Num_Rs - 1; r_ind++) {
                r = (double)r_ind / Num_Rs / 2; // divided by 2 since we//re modeling half the wake
                U_spd_last[r_ind] = (freeStream * (1 - (freeStream - U_spd_last[0]) / freeStream * Math.Exp(-3.56 * Math.Pow(r, 2) / Math.Pow(initWakeWidth, 2))));
                V_spd_last[r_ind] = 0;
                if (minDistRD == 2) velDeficit[0, r_ind] = 1 - U_spd_last[r_ind] / freeStream;
            }

            double F = (0.65 - Math.Pow((-(2 - 4.5) / 23.32), 1f/3));
            double b = initWakeWidth;
            double Eps = F * (k1 * b * (freeStream - U_spd_last[0]) + Km);

            if (maxDistRD > 2) {
                // Steps along axial direction from UW turbine to waked turbine
                for (double i = 2 + X_Reso_RD; i <= maxDistRD; i = i + X_Reso_RD) {

                    double X_Length = (2 + i) * RD;
                    a_mtrx[0] = -k * Eps;
                    b_mtrx[0] = 2 * (Math.Pow(h, 2) * U_spd_last[0] + k * Eps);
                    c_mtrx[0] = -2 * k * Eps;
                    r_mtrx[0] = (k * Eps * (2 * U_spd_last[1] - 2 * U_spd_last[0]) + 2 * Math.Pow(h, 2) * Math.Pow(U_spd_last[0], 2));
                    c_prime[0] = c_mtrx[0] / b_mtrx[0];
                    r_prime[0] = r_mtrx[0] / b_mtrx[0];

                    for (int r_ind = 1; r_ind <= Num_Rs - 1; r_ind++) {
                        r = (double)r_ind / Num_Rs / 2; // divided by 2 since we//re modeling half the wake

                        a_mtrx[r_ind] = k * (h * Eps - r * h * V_spd_last[r_ind] - 2 * r * Eps);
                        b_mtrx[r_ind] = 4 * r * (Math.Pow(h, 2) * U_spd_last[r_ind] + k * Eps);
                        c_mtrx[r_ind] = k * (r * h * V_spd_last[r_ind] - 2 * r * Eps - h * Eps);
                        c_prime[r_ind] = c_mtrx[r_ind] / (b_mtrx[r_ind] - a_mtrx[r_ind] * c_prime[r_ind - 1]);

                        if (r_ind == Num_Rs - 1) {
                            r_mtrx[r_ind] = (h * k * Eps * (freeStream - U_spd_last[r_ind - 1]) + 2 * r * k * Eps * (freeStream - 2 * U_spd_last[r_ind] + U_spd_last[r_ind - 1])
                                - r * h * k * V_spd_last[r_ind] * (freeStream - U_spd_last[r_ind - 1]) + 4 * r * Math.Pow(h, 2) * Math.Pow(U_spd_last[r_ind], 2) - c_mtrx[r_ind] * freeStream);
                        }
                        else {
                            r_mtrx[r_ind] = (h * k * Eps * (U_spd_last[r_ind + 1] - U_spd_last[r_ind - 1]) + 2 * r * k * Eps * (U_spd_last[r_ind + 1] - 2 * U_spd_last[r_ind] + U_spd_last[r_ind - 1])
                                - r * h * k * V_spd_last[r_ind] * (U_spd_last[r_ind + 1] - U_spd_last[r_ind - 1]) + 4 * r * Math.Pow(h, 2) * Math.Pow(U_spd_last[r_ind], 2));
                        }

                        r_prime[r_ind] = (r_mtrx[r_ind] - a_mtrx[r_ind] * r_prime[r_ind - 1]) / (b_mtrx[r_ind] - a_mtrx[r_ind] * c_prime[r_ind - 1]);
                    }

                    // Now solve for U spd at r
                    // Goes from r = b to r = 0
                    U_spd[Num_Rs - 1] = r_prime[Num_Rs - 1];

                    for (int r_ind = Num_Rs - 2; r_ind >= 0; r_ind--)
                        U_spd[r_ind] = r_prime[r_ind] - c_prime[r_ind] * U_spd[r_ind + 1];

                    // Now calculate radial WS
                    for (int r_ind = 1; r_ind <= Num_Rs - 1; r_ind++) {
                        r = (double)r_ind / Num_Rs / 2; // divided by 2 since we//re modeling half the wake
                        V_spd[r_ind] = r / (r + h) * (V_spd[r_ind - 1] - h / k * (U_spd[r_ind] - U_spd_last[r_ind]));
                    }

                    // && save last U and V wind speeds
                    for (int r_ind = 0; r_ind <= Num_Rs - 1; r_ind++) {
                        U_spd_last[r_ind] = U_spd[r_ind];
                        V_spd_last[r_ind] = V_spd[r_ind];
                    }

                    // Calculate F, Wake width and Epsilon (eddy viscosity) for next row//s calculations
                    if (i < 5.5) {
                        if (i < 4.5)
                            F = 0.65f - Math.Pow((-(i - 4.5) / 23.32), (1f / 3));
                        else
                            F = 0.65f + Math.Pow((i - 4.5) / 23.32, (1f / 3));
                    }
                    else {
                        F = 1;
                    }

                    b = initWakeWidth + (i - 2) * Math.Atan(thisWakeModel.horizWakeExp * Math.PI / 180); // Minus 2 since this is initial RD for initial wake width
                    Eps = F * (k1 * b * (freeStream - U_spd_last[0]) + Km);

                    for (int r_ind = 0; r_ind <= Num_Rs - 1; r_ind++) {

                        double WS_Recharge = (Math.Pow(thisWakeModel.wakeRechargeRate, 0.5) * Math.Pow(X_Length, thisWakeModel.wakeRechargeExp * freeStream));

                        if (U_spd[r_ind] + WS_Recharge < freeStream)
                            U_spd[r_ind] = U_spd[r_ind] + WS_Recharge;
                        else
                            U_spd[r_ind] = freeStream;


                        //  velDeficit = freeStream - U_spd(Waked_Turb_r_ind)
                        if (Math.Round(i, 2) >= minDistRD && Math.Round(i, 2) <= maxDistRD) {
                            velDeficit[Vel_Def_Count, r_ind] = 1 - U_spd[r_ind] / freeStream;
                            if (r_ind == Num_Rs - 1) Vel_Def_Count = Vel_Def_Count + 1;
                        }

                    }
                }
            }
            else {
                U_spd = U_spd_last;
            }

            return velDeficit;
        }

        /// <summary> Calculates and returns the width of wake based on horizontal expansion angle, distance from rotor, and initial wake width. </summary>
        public double CalcWakeWidth(double initWakeWidth, Wake_Model thisWakeModel, double X_Length)
        {            
            double wakeWidth = initWakeWidth + (X_Length - 2) * Math.Atan(thisWakeModel.horizWakeExp * Math.PI / 180); // Subtract 2 since initial wake width is at RD = 2

            return wakeWidth;
        }

        /// <summary> For given WD sector and turbine, returns list of all upwind turbines sorted with furthest away first. Calculates dot product of wind direction sector vector and turbine coordinate vector. </summary>
        public Turbine[] FindAllUW_Turbines(double WD_Sector, double gridX, double gridY, TurbineCollection turbineList)
        {
            Turbine[] UW_Turbines = null;

            int numTurbs = turbineList.TurbineCount;            
            double firstVectLen   = -100000000.0f;
            WD_Sector = 90 - WD_Sector;
                        
            double thisLength;
            double thisDot;
            Turbine thisTurbine = new Turbine();
            Turbine firstTurbine = new Turbine();
            int turbInd = 0;
            int numUW_turbs = 0;

            // first find Vector_length to waked grid node            
            double thisTheta = WD_Sector - ((Math.Atan2(gridY, gridX)) * 180 / Math.PI);
            double wakedVectLen = (Math.Pow((Math.Pow(gridX, 2) + Math.Pow(gridY, 2)), 0.5) * Math.Cos(thisTheta * Math.PI / 180));

            // find first turbine and count how many are upwind
            for (int i = 0; i < numTurbs; i++) {
                thisTurbine = turbineList.turbineEsts[i];
                thisTheta = WD_Sector - ((Math.Atan2(thisTurbine.UTMY, thisTurbine.UTMX)) * 180 / Math.PI);

                thisLength = Math.Pow((Math.Pow(thisTurbine.UTMX, 2) + Math.Pow(thisTurbine.UTMY, 2)), 0.5);
                thisDot = (thisLength * Math.Cos(thisTheta * Math.PI / 180));

                if (thisDot > wakedVectLen &&(gridX!= thisTurbine.UTMX || gridY!= thisTurbine.UTMY))
                {
                    numUW_turbs++;
                    if ( thisDot > firstVectLen ) {
                        firstTurbine = thisTurbine;
                        firstVectLen = thisDot;
                    }
                }
            }

            if (numUW_turbs == 0)
                return UW_Turbines;

            UW_Turbines = new Turbine[numUW_turbs];
            UW_Turbines[0] = firstTurbine;
            turbInd = 1;

            // Find next turbine           
            while (turbInd < numUW_turbs) {
                firstVectLen = -100000000.0f;
                for (int i = 0; i < numTurbs; i++) {
                    thisTurbine = turbineList.turbineEsts[i];
                    thisTheta = WD_Sector - ((Math.Atan2(thisTurbine.UTMY, thisTurbine.UTMX)) * 180 / Math.PI);

                    thisLength = (Math.Pow((Math.Pow(thisTurbine.UTMX, 2) + Math.Pow(thisTurbine.UTMY, 2)), 0.5));
                    thisDot = (thisLength * Math.Cos(thisTheta * Math.PI / 180));

                    if (thisDot > firstVectLen) {
                        bool gotIt = false;
                        for (int j = 0; j <= turbInd - 1; j++) { 
                            if (UW_Turbines[j].name == thisTurbine.name) {
                                gotIt = true;
                                break;
                            }
                        }

                        if (gotIt == false) {
                            firstTurbine = thisTurbine;
                            firstVectLen = thisDot;
                        }
                    }
                }

                UW_Turbines[turbInd] = firstTurbine;
                turbInd++;

            }

            return UW_Turbines;
        }

        /// <summary> Checks to see if there is a defined wake loss model that uses specified power curve. </summary>
        public bool HaveWakeModelWithThisCrv(TurbineCollection.PowerCurve powerCurve)
        {            
            bool haveModelWithCrv = false;

            for (int i = 0; i < NumWakeModels; i++)
                if (wakeModels[i].powerCurve.name == powerCurve.name)
                    haveModelWithCrv = true;

            return haveModelWithCrv;
        }
    }
}
