using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary> Class that holds list of Continuum models and functions to add/delete models, to generate average estimates based on model weights, and to create time series data. </summary>
    [Serializable()]
    public class ModelCollection
    {
        /// <summary> List of models; i = Model ind, j = radius ind  </summary>
        public Model[,] models;
        /// <summary> Maximum elevation difference between predictor and target site for wind speed estimate to be formed (not currently used)  </summary>
        public double maxElevAllowed = 300;
        /// <summary> Maximum P10 exposure difference between predictor and target site for wind speed estimate to be formed (not currently used)  </summary>
        public double maxP10ExpoAllowed = 200;

        public double rotorDiam = 100; // Wind turbine rotor diameter.  Default is 100 m until user imports a power curve (or edits from Input tab)
        public double airDens = 1.225; // Air density kg/m^3 (default of 1.225)


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Holds sectorwise wind speeds at site of interest and sectorwise wind speed estimates at each node in path of nodes  </summary>
        public struct WS_Est_Struct 
        {
            /// <summary> Sectorwise wind speed estimates </summary>
            public double[] sectorWS;
            /// <summary> Sectorwise wind speed estimates at each node in path of nodes </summary>
            public double[,] sectorWS_AtNodes;
        }

        /// <summary> Contains a model coefficient, flow type, estimated change in wind speed, and string specifying either exposure or roughness model </summary>
        public struct Coeff_Delta_WS 
        {
            /// <summary> Model coefficient </summary>
            public double coeff;
            /// <summary> Flow type: Downhill, uphill, speed-up, valley or turbulent </summary>
            public string flowType;
            /// <summary> Estimated change in wind speed </summary>
            public double deltaWS_Expo;
            /// <summary> specifies whether refers to exposure model or surface roughness model "Expo" or "SRDH" </summary>
            public string expoOrRough;
        }

        /// <summary> Contains model, predictor met, and wind speed weight </summary>
        public struct ModelWeights
        {
            /// <summary> Predictor met </summary>
            public Met met;
            /// <summary> Continuum model </summary>
            public Model model;
            /// <summary> Wind speed estimate weight to apply </summary>
            public double weight;
        }

        /// <summary> Holds list of WS/WD time series </summary>
        public struct MetLTEsts
        {
            /// <summary> List of WS/WD time series </summary>
            public MCP.Site_data[] estWS;
        }

        /// <summary> Holds list of Models (one for each radius of investigation) </summary>
        public struct Models
        {
            /// <summary> List of Models </summary>
            public Model[] modelsByRad;
        }

        /// <summary> Holds predictor met, wind flow model, and path of nodes to site of interest. </summary>
        public struct PathsOfNodes
        {
            /// <summary> Path of nodes between met and site of interest </summary>
            public Nodes[] path;
            /// <summary> Model to use to conduct estimate </summary>
            public Model model;
            /// <summary> Predicting met site </summary>
            public Met met;
        }

        /// <summary> Holds wind direction, wind speed (free-stream and waked), and energy production (gross and net) data for specified timestamp. </summary>
        [Serializable]
        public struct TimeSeries
        {
            /// <summary> Time stamp </summary>
            public DateTime dateTime;
            /// <summary> Free-stream wind speed </summary>
            public double freeStreamWS;
            /// <summary> Waked wind speed </summary>
            public double wakedWS;
            /// <summary> Wind direction </summary>
            public double WD;
            /// <summary> Gross energy production </summary>
            public double grossEnergy;
            /// <summary> Net energy production </summary>
            public double netEnergy;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Returns number of models in list </summary>
        public int ModelCount
        {
            get { if (models == null)
                    return 0;
                else return models.GetUpperBound(0) + 1; }
        }

        /// <summary> Returns number of Invest_Params (i.e. radius and exponent) </summary>
        public int RadiiCount
        {
            get
            {
                if (models == null)
                    return 0;
                else
                    return models.GetUpperBound(1) + 1;
            }
        }

        /// <summary> Clears all models from list. </summary>
        public void ClearAll()
        {                 
            models = null;                
        }

        /// <summary> Clears all models except imported models </summary>
        public void ClearAllExceptImported()
        {        
            if (ModelCount > 0)
            {
                Model[,] importedModel = null;
                int haveImported = GetImportedModelInd();

                if (haveImported != -1)               
                    importedModel = new Model[1, RadiiCount];                   
                                
                for (int i = 0; i < ModelCount; i++)
                {
                    if (models[i, 0].isImported)
                    {
                        for (int j = 0; j < RadiiCount; j++)
                            importedModel[0, j] = models[i, j]; 
                    }
                }

                models = importedModel;
            }            
        }

        /// <summary> Clears all imported models </summary>
        public void ClearImported()
        {            
            if (ModelCount > 0 ) {
                
                int modelInd = 0;

                for (int i = 0; i < ModelCount; i++)
                    if (models[i, 0].isImported == false)
                        modelInd++;

                Model[,] newModel = new Model[modelInd, RadiiCount];
                modelInd = 0;

                for (int i = 0; i < ModelCount; i++)
                { 
                    if (models[i, 0].isImported == false)
                    {
                        for (int j = 0; j < RadiiCount; j++)
                            newModel[modelInd, j] = models[i, j];

                        modelInd++;
                    }
                }

                models = newModel;
            }               
            
        }

        /// <summary> Adds a model to the list of models </summary>  
        public void AddModel(Model[] newModel)
        {             
            int numRadii = newModel.Length;
            Model[,] tempModels = new Model[ModelCount, numRadii];

            // Copy existing UW&DW models into temp array
            for (int i = 0; i < ModelCount; i++)
                for (int j = 0; j <= numRadii - 1; j++)
                    tempModels[i, j] = models[i, j];

            models = new Model[ModelCount + 1, numRadii];

            // Copy models back in
            for (int i = 0; i <= ModelCount - 2; i++)
                for (int j = 0; j <= numRadii - 1; j++)
                    models[i, j] = tempModels[i, j];               
            
            if (ModelCount == 0 )
            {
                for (int i = 0; i <= numRadii - 1; i++)
                    models[0, i] = newModel[i];
            }
            else {
                for (int i = 0; i < numRadii; i++)
                    models[ModelCount - 1, i] = newModel[i];                
            }

        }               

        /// <summary> Calculates and returns wind speed estimates weights at a target site for each predictor met and each model and TOD/Season bin </summary>
        public ModelWeights[] GetWS_EstWeights(Met[] predictorMets, Nodes targetNode, Model[] models, double[] windRose, InvestCollection radiiList)
        {             
            int numPredMets = predictorMets.Length;
            ModelWeights[] weights = new ModelWeights[0];          
            int slopeFactor = 1;
            
            double minRMS = 1000;
            double maxRMS = 0;
            double RMS_Weight = 0;
            
            int numWD = windRose.Length;                     

            double weightConst = 0.75f;
            double[] modelsRMS = new double[models.Length];

            // Finds the min and max met cross-prediction error of models()
            for (int i = 0; i < models.Length; i++)
            { 
                if (models[i].metsUsed.Length > 1)
                {
                    modelsRMS[i] = models[i].RMS_WS_Est;
                    if (modelsRMS[i] < minRMS)
                        minRMS = modelsRMS[i];

                    if (modelsRMS[i] > maxRMS)
                        maxRMS = modelsRMS[i];                    
                }
            }

            // slope and intercept for the RMS weight. weightConst = 0.75 so that minimum RMS weight = 0.25
            double slope = -weightConst / (maxRMS - minRMS);
            double intercept = 1 + weightConst * minRMS / (maxRMS - minRMS);

            if (numPredMets > 1)
            {
                weights = new ModelWeights[numPredMets * models.Length];
                int weightsInd = 0;
             
                for (int modelIndex = 0; modelIndex < models.Length; modelIndex++)
                {
                    int radius = models[modelIndex].radius;
                    int radiusIndex = radiiList.GetRadiusInd(radius);
                    double DW_Diff = 0;
                    double UW_Diff = 0;
                    // Calculates average difference in P10 exposure between predictor met and target site (weighted by wind rose) and finds weight where higher weight
                    // is applied to met sites with more similar terrain complexity (i.e. P10 exposure)

                    double sumUWDW_Diff = 0;
                    for (int i = 0; i < numPredMets; i++)
                    {
                        DW_Diff = 0;
                        UW_Diff = 0;
                        for (int WD = 0; WD < numWD; WD++)
                        {
                            DW_Diff = DW_Diff + Math.Abs(predictorMets[i].gridStats.stats[radiusIndex].P10_DW[WD] - targetNode.gridStats.stats[radiusIndex].P10_DW[WD]) * windRose[WD];
                            UW_Diff = UW_Diff + Math.Abs(predictorMets[i].gridStats.stats[radiusIndex].P10_UW[WD] - targetNode.gridStats.stats[radiusIndex].P10_UW[WD]) * windRose[WD];
                        }
                        sumUWDW_Diff = sumUWDW_Diff + DW_Diff + UW_Diff;
                    }

                    if (models[modelIndex].metsUsed.Length > 1 && minRMS != maxRMS)
                        RMS_Weight = slope * modelsRMS[modelIndex] + intercept;
                    else
                        RMS_Weight = 1;

                    for (int i = 0; i <= numPredMets - 1; i++)
                    {
                        weights[weightsInd].met = predictorMets[i];
                        weights[weightsInd].model = models[modelIndex];

                        DW_Diff = 0;
                        UW_Diff = 0;

                        for (int WD = 0; WD <= numWD - 1; WD++)
                        {
                            DW_Diff = DW_Diff + Math.Abs(predictorMets[i].gridStats.stats[radiusIndex].P10_DW[WD] - targetNode.gridStats.stats[radiusIndex].P10_DW[WD]) * windRose[WD];
                            UW_Diff = UW_Diff + Math.Abs(predictorMets[i].gridStats.stats[radiusIndex].P10_UW[WD] - targetNode.gridStats.stats[radiusIndex].P10_UW[WD]) * windRose[WD];
                        }

                        if (sumUWDW_Diff > 0)
                        {
                            weights[weightsInd].weight = 1 - slopeFactor * (DW_Diff + UW_Diff) / sumUWDW_Diff;
                            if (weights[weightsInd].weight < 0)
                                weights[weightsInd].weight = 0;
                        }
                        else
                            weights[weightsInd].weight = 1;

                        weights[weightsInd].weight = weights[weightsInd].weight * RMS_Weight;

                        if (models[modelIndex].timeOfDay != Met.TOD.All)
                            weights[weightsInd].weight = weights[weightsInd].weight * 0.5; // Day/Night conditions modeled so divide this weight by 2

                        if (models[modelIndex].season != Met.Season.All)
                            weights[weightsInd].weight = weights[weightsInd].weight * 0.25; // Four seasonal conditions modeled so divide this weight by 4

                        weightsInd++;
                    }
                }
            }
            else if (numPredMets == 1)
            {                
                weights = new ModelWeights[models.Length];
                for (int i = 0; i < models.Length; i++)
                {
                    weights[i].met = predictorMets[0];
                    weights[i].model = models[i];
                    weights[i].weight = 1;
                }
                    

            }

            return weights;
        }

        /// <summary> Finds and returns wind speed estimate weight for specified model and predictor met </summary>  
        public double GetWeightForMetAndModel(ModelWeights[] modelWeights, Met thisMet, Model thisModel)
        {                    
            if (modelWeights == null)
                return 0;

            int numWeights = modelWeights.Length;

            for (int i = 0; i < numWeights; i++)
                if (modelWeights[i].met.name == thisMet.name && IsSameModel(modelWeights[i].model, thisModel))                
                    return modelWeights[i].weight;                                   

            return 0;
        }

        /// <summary>  Returns false if site-calibration has not been performed yet for specified list of met sites and time of day and seasonal bins </summary> 
        public bool IterationAlreadyDone(Continuum thisInst, string[] metsUsed, Met.TOD thisTOD, Met.Season thisSeason)
        {            
            bool alreadyDone = false;
            
            for (int i = 0; i < ModelCount; i++)
            {
                bool sameMets = thisInst.metList.sameMets(metsUsed, models[i, 0].metsUsed);

                if (sameMets == true && thisTOD == models[i, 0].timeOfDay & thisSeason == models[i, 0].season) {
                    alreadyDone = true;
                    break;
                }
            }

            return alreadyDone;
        }

        /// <summary>  Returns index of imported model in list of models </summary> 
        public int GetImportedModelInd()
        {            
            int importedInd = -1;

            for (int i = 0; i < ModelCount; i++)
                if (models[i, 0] != null)
                {
                    if (models[i, 0].isImported == true)
                    {
                        importedInd = i;
                        break;
                    }
                }                

            return importedInd;
        }

        /// <summary> Returns models with specified mets, time of day bin, and season bin </summary> 
        public Model[] GetModels(Continuum thisInst, string[] metsUsed, Met.TOD thisTOD, Met.Season thisSeason, double thisHeight, bool createIfNeeded)
        {            
            Model[] thisModel = new Model[RadiiCount];
            bool sameMets = false;
            
            for (int i = 0; i < ModelCount; i++)
            {
                sameMets = thisInst.metList.sameMets(metsUsed, models[i, 0].metsUsed);
                if (sameMets == true && thisTOD == models[i, 0].timeOfDay & thisSeason == models[i, 0].season & thisHeight == models[i, 0].height)
                {
                    for (int j = 0; j < RadiiCount; j++)
                        thisModel[j] = models[i, j];

                    break;
                }
            }

            if (ModelCount == 0 || createIfNeeded)
            {
                CreateModel(metsUsed, thisInst, thisTOD, thisSeason, thisHeight);
                for (int i = 0; i < ModelCount; i++)
                {
                    sameMets = thisInst.metList.sameMets(metsUsed, models[i, 0].metsUsed);
                    if (sameMets == true && thisTOD == models[i, 0].timeOfDay & thisSeason == models[i, 0].season)
                    {
                        for (int j = 0; j < RadiiCount; j++)
                            thisModel[j] = models[i, j];

                        break;
                    }
                }
            }                            

            return thisModel;
        }

        /// <summary> Returns true if two models are identical </summary> 
        public bool IsSameModel(Model model1, Model model2)
        {            
            bool isSame = true;
            int numWD;
            try
            {
                numWD = model1.downhill_A.Length;
            }
            catch 
            {
                isSame = false;
                return isSame;
            }

            if (model1 != null && model2 != null)
            {
                bool isSameMets = sameMets(model1.metsUsed, model2.metsUsed);
                for (int WD = 0; WD < numWD; WD++)
                {
                    try
                    {
                        if (model1.height == model2.height && model1.timeOfDay == model2.timeOfDay && model1.season == model2.season && isSameMets == true 
                            && model1.downhill_A[WD] == model2.downhill_A[WD] && model1.downhill_B[WD] == model2.downhill_B[WD] && model1.uphill_A[WD] == model2.uphill_A[WD] && model1.radius == model2.radius
                            && model1.uphill_B[WD] == model2.uphill_B[WD] && model1.UW_crit[WD] == model2.UW_crit[WD] && model1.spdUp_A[WD] == model2.spdUp_A[WD] && model1.spdUp_B[WD] == model2.spdUp_B[WD]
                            && model1.DH_Stab_A[WD] == model2.DH_Stab_A[WD] && model1.UH_Stab_A[WD] == model2.UH_Stab_A[WD] && model1.SU_Stab_A[WD] == model2.SU_Stab_A[WD] && model1.stabB[WD] == model2.stabB[WD]
                            && model1.isImported == model2.isImported) 

                            isSame = true;
                        else
                        {
                            isSame = false;
                            break;
                        }
                    }
                    catch 
                    {
                        isSame = false;
                        return isSame;
                    }
                }
            }
            else
                isSame = false;


            return isSame;
        }

        /// <summary> Returns true if two lists of mets are identical or if both lists contain one met (i.e. default model) </summary> 
        public bool sameMets(string[] metsUsed1, string[] metsUsed2)
        {              
            string string1 = "";
            string string2 = "";
            bool sameMetSites = false;

            if (metsUsed1 != null && metsUsed2 != null)
            {
                if (metsUsed1.Length == metsUsed2.Length)
                {
                    if (metsUsed1.Length == 1 & metsUsed2.Length == 1)
                        sameMetSites = true;
                    else
                    {
                        for (int j = 0; j < metsUsed1.Length; j++)
                        {
                            string1 = string1 + metsUsed1[j].ToString();
                            string2 = string2 + metsUsed2[j].ToString();
                        }

                        if (string1 == string2)
                            sameMetSites = true;
                    }                    
                }
            }
            else
                sameMetSites = false;

            return sameMetSites;
        }

        /// <summary> With specified model settings and met list, creates site-calibrated model for each radius of investigation and adds to list of Models </summary>        
        public void FindSiteCalibratedModels(Continuum thisInst, Met.TOD thisTOD, Met.Season thisSeason, double thisHeight)
        {                       
            // need more than one met to create site-calibrate model
            if (thisInst.metList.ThisCount == 1)
                return;     
                    
            // Check to see if met exposures and cross-predictions have all been calculated
            if (thisInst.metList.ThisCount > 1)
                CreateModel(thisInst.metList.GetMetsUsed(), thisInst, thisTOD, thisSeason, thisHeight);                               
            
        }

        /// <summary> Creates default Continuum models (one for each radius of investigation and exponent) and adds them to list </summary>  
        public void CreateDefaultModels(Continuum thisInst)
        {
            int numRadii = thisInst.radiiList.ThisCount;
            int numWD = thisInst.metList.numWD;
            Model[] models = new Model[numRadii];

            for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
            {
                models[i] = new Model();
                models[i].SizeArrays(numWD);
                models[i].SetDefaultModelCoeffs(numWD);
                models[i].SetDefaultLimits();
                models[i].radius = thisInst.radiiList.investItem[i].radius;
                models[i].metsUsed = thisInst.metList.GetMetsUsed();
                
                models[i].season = Met.Season.All;
                models[i].timeOfDay = Met.TOD.All;
                models[i].height = thisInst.modeledHeight;
            }

            // Add new models to Model collection
            thisInst.modelList.AddModel(models);
            
        }

        /// <summary> Opens model coefficient file and creates model with imported coefficients </summary> 
        public void ImportModelsCSV(Continuum thisInst)
        {            
            int numRadii = thisInst.radiiList.ThisCount;
            Model[] importModel = new Model[numRadii];                       
            
            string[] theseParams;
            bool useSR = false;
            bool usesFlowSep = false;
            StreamReader sr;

            if (thisInst.ofdImportCoeffs.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(thisInst.ofdImportCoeffs.FileName) == false)
                {
                    MessageBox.Show("Error opening the file. Check that it's not open in another program.", "Continuum 2.2");
                    return;
                }

                try {                    
                    sr = new StreamReader(thisInst.ofdImportCoeffs.FileName);                }
                catch  {
                    MessageBox.Show("Error opening the file. Check that it's not open in another program.",  "Continuum 2.2");                    
                    return;
                }

                try {
                    string thisString = sr.ReadLine(); // Continuum 2.2 Model Parameters                    
                    thisString = sr.ReadLine(); // Date
                    thisString = sr.ReadLine(); // Site-calibrated Model using X mets

                    int firstParanth = thisString.IndexOf("(");
                    int lastParenth = thisString.IndexOf(")");
                    string[] theseMets = thisString.Substring(firstParanth + 1, lastParenth - firstParanth - 1).Split(Convert.ToChar(" "));

                    Model defaultModel = new Model();
                    defaultModel.SizeArrays(15); // it's okay that we're hardcoding numWD = 15 here since we're just doing this to set the default coefficients.The array is resized.
                    defaultModel.SetDefaultModelCoeffs(15);

                    for (int i = 0; i < numRadii; i++)
                    {
                        importModel[i] = new Model();
                        importModel[i].radius = thisInst.radiiList.investItem[i].radius;
                        importModel[i].isImported = true;                        
                        importModel[i].SetDefaultLimits(); // max diff in expo and elev
                        importModel[i].metsUsed = theseMets;

                        thisString = sr.ReadLine(); // radius
                        thisString = sr.ReadLine(); // RMS heading

                        thisString = sr.ReadLine(); // RMS error
                        string thisStr = thisString.Substring(0, thisString.Length - 1);
                        importModel[i].RMS_WS_Est = Convert.ToSingle(thisString.Substring(0, thisString.Length - 1)) / 100;

                        thisString = sr.ReadLine(); // Model Headings

                        string[] theseHeaders = thisString.Split(Convert.ToChar(","));

                        for (int head_ind = 0; head_ind <= theseHeaders.Length - 1; head_ind++)
                        {
                            if (theseHeaders[head_ind] == "DH Stability_A")
                                useSR = true;
                            else if (theseHeaders[head_ind] == "Sep_A_DW")
                                usesFlowSep = true;
                        }

                        int WD_Ind = 0;

                        while (thisString != "")
                        {
                            thisString = sr.ReadLine();
                            theseParams = thisString.Split(Convert.ToChar(","));

                            if (thisString != "")
                            {
                                Array.Resize(ref importModel[i].RMS_Sect_WS_Est, WD_Ind + 1);
                                Array.Resize(ref importModel[i].downhill_A, WD_Ind + 1);
                                Array.Resize(ref importModel[i].downhill_B, WD_Ind + 1);
                                Array.Resize(ref importModel[i].uphill_A, WD_Ind + 1);
                                Array.Resize(ref importModel[i].uphill_B, WD_Ind + 1);
                                Array.Resize(ref importModel[i].UW_crit, WD_Ind + 1);
                                Array.Resize(ref importModel[i].spdUp_A, WD_Ind + 1);
                                Array.Resize(ref importModel[i].spdUp_B, WD_Ind + 1);
                                Array.Resize(ref importModel[i].DH_Stab_A, WD_Ind + 1);
                                Array.Resize(ref importModel[i].UH_Stab_A, WD_Ind + 1);
                                Array.Resize(ref importModel[i].SU_Stab_A, WD_Ind + 1);
                                Array.Resize(ref importModel[i].stabB, WD_Ind + 1);
                                Array.Resize(ref importModel[i].sep_A_DW, WD_Ind + 1);
                                Array.Resize(ref importModel[i].sep_B_DW, WD_Ind + 1);
                                Array.Resize(ref importModel[i].turbWS_Fact, WD_Ind + 1);
                                Array.Resize(ref importModel[i].sepCrit, WD_Ind + 1);
                                Array.Resize(ref importModel[i].Sep_crit_WS, WD_Ind + 1);

                                importModel[i].RMS_Sect_WS_Est[WD_Ind] = Convert.ToSingle(theseParams[1].Substring(0, theseParams[1].Length - 1)) / 100;
                                importModel[i].downhill_A[WD_Ind] = Convert.ToSingle(theseParams[2]);
                                importModel[i].downhill_B[WD_Ind] = Convert.ToSingle(theseParams[3]);
                                importModel[i].uphill_A[WD_Ind] = Convert.ToSingle(theseParams[4]);
                                importModel[i].uphill_B[WD_Ind] = Convert.ToSingle(theseParams[5]);
                                importModel[i].UW_crit[WD_Ind] = Convert.ToSingle(theseParams[6]);
                                importModel[i].spdUp_A[WD_Ind] = Convert.ToSingle(theseParams[7]);
                                importModel[i].spdUp_B[WD_Ind] = Convert.ToSingle(theseParams[8]);

                                if (useSR == true && usesFlowSep == false) {
                                    importModel[i].DH_Stab_A[WD_Ind] = Convert.ToSingle(theseParams[9]);
                                    importModel[i].UH_Stab_A[WD_Ind] = Convert.ToSingle(theseParams[10]);
                                    importModel[i].SU_Stab_A[WD_Ind] = Convert.ToSingle(theseParams[11]);
                                    importModel[i].sep_A_DW[WD_Ind] = defaultModel.sep_A_DW[0];
                                    importModel[i].sep_B_DW[WD_Ind] = defaultModel.sep_B_DW[0];
                                    importModel[i].turbWS_Fact[WD_Ind] = defaultModel.turbWS_Fact[0];
                                    importModel[i].sepCrit[WD_Ind] = defaultModel.sepCrit[0];
                                    importModel[i].Sep_crit_WS[WD_Ind] = defaultModel.Sep_crit_WS[0];
                                }
                                else if (useSR == false && usesFlowSep == true) {
                                    importModel[i].DH_Stab_A[WD_Ind] = defaultModel.DH_Stab_A[0];
                                    importModel[i].UH_Stab_A[WD_Ind] = defaultModel.UH_Stab_A[0];
                                    importModel[i].SU_Stab_A[WD_Ind] = defaultModel.SU_Stab_A[0];
                                    importModel[i].sep_A_DW[WD_Ind] = Convert.ToSingle(theseParams[9]);
                                    importModel[i].sep_B_DW[WD_Ind] = Convert.ToSingle(theseParams[10]);
                                    importModel[i].turbWS_Fact[WD_Ind] = Convert.ToSingle(theseParams[11]);
                                    importModel[i].sepCrit[WD_Ind] = Convert.ToSingle(theseParams[12]);
                                    importModel[i].Sep_crit_WS[WD_Ind] = Convert.ToSingle(theseParams[13]);
                                }
                                else if (useSR == true && usesFlowSep == true) {
                                    importModel[i].DH_Stab_A[WD_Ind] = Convert.ToSingle(theseParams[9]);
                                    importModel[i].UH_Stab_A[WD_Ind] = Convert.ToSingle(theseParams[10]);
                                    importModel[i].SU_Stab_A[WD_Ind] = Convert.ToSingle(theseParams[11]);
                                    importModel[i].sep_A_DW[WD_Ind] = Convert.ToSingle(theseParams[12]);
                                    importModel[i].sep_B_DW[WD_Ind] = Convert.ToSingle(theseParams[13]);
                                    importModel[i].turbWS_Fact[WD_Ind] = Convert.ToSingle(theseParams[14]);
                                    importModel[i].sepCrit[WD_Ind] = Convert.ToSingle(theseParams[15]);
                                    importModel[i].Sep_crit_WS[WD_Ind] = Convert.ToSingle(theseParams[16]);
                                }
                                else {
                                    importModel[i].DH_Stab_A[WD_Ind] = defaultModel.DH_Stab_A[0];
                                    importModel[i].UH_Stab_A[WD_Ind] = defaultModel.UH_Stab_A[0];
                                    importModel[i].SU_Stab_A[WD_Ind] = defaultModel.SU_Stab_A[0];
                                    importModel[i].sep_A_DW[WD_Ind] = defaultModel.sep_A_DW[0];
                                    importModel[i].sep_B_DW[WD_Ind] = defaultModel.sep_B_DW[0];
                                    importModel[i].turbWS_Fact[WD_Ind] = defaultModel.turbWS_Fact[0];
                                    importModel[i].sepCrit[WD_Ind] = defaultModel.sepCrit[0];
                                    importModel[i].Sep_crit_WS[WD_Ind] = defaultModel.Sep_crit_WS[0];
                                }


                            }
                            WD_Ind++;
                        }

                        WD_Ind--;

                        // Check to see if the same length  wind rose entered
                        if (thisInst.metList.ThisCount > 0) {
                            if (thisInst.metList.numWD != WD_Ind) {
                                MessageBox.Show("The imported model has a different number of WD sectors than the entered met site. Check your inputs.", "Continuum 2.2");
                                sr.Close();
                                return;
                            }
                        }

                        thisInst.metList.numWD = WD_Ind;
                        thisInst.updateThe.WindDirectionToDisplay();                        

                    }
                }
                catch {
                    sr.Close();
                    return;
                }

                sr.Close();
                
                AddModel(importModel);
                thisInst.updateThe.AdvancedTAB();
                
            }
        }

        /// <summary> Creates and returns site-calibrated models using specified mets and model settings </summary>        
        public Model[] CreateModel(string[] metsUsed, Continuum thisInst, Met.TOD thisTOD, Met.Season thisSeason, double thisHeight)
        {             
            int numPairs = thisInst.metPairList.PairCount;
            int numRadii = thisInst.radiiList.ThisCount;
            NodeCollection nodeList = new NodeCollection();

            int numWD = thisInst.metList.numWD;                        
            Model[] models;
                        
            bool modelAlreadyCreated = IterationAlreadyDone(thisInst, metsUsed, thisTOD, thisSeason);

            if (modelAlreadyCreated == false)
            {
                models = new Model[numRadii];

                for (int i = 0; i < numRadii; i++)
                {
                    models[i] = new Model(); 
                    models[i].SizeArrays(numWD);
                    int thisRadius = thisInst.radiiList.investItem[i].radius;
                    models[i].SetDefaultLimits();
                    models[i].timeOfDay = thisTOD;
                    models[i].season = thisSeason;
                    models[i].height = thisHeight;
                    models[i].radius = thisRadius;
                    models[i].metsUsed = metsUsed;
                    models[i].SetDefaultModelCoeffs(numWD); // DW A and B, UW A and B, UW slope                    
                }

                AddModel(models);

                for (int j = 0; j < numPairs; j++)
                    thisInst.metPairList.metPairs[j].AddWS_Pred(models);

                for (int i = 0; i < numRadii; i++)
                {
                    for (int j = 0; j < numPairs; j++)
                    {
                        Nodes met1Node = nodeList.GetMetNode(thisInst.metPairList.metPairs[j].met1);
                        Nodes met2Node = nodeList.GetMetNode(thisInst.metPairList.metPairs[j].met2);

                        Nodes[] pathOfNodes = nodeList.FindPathOfNodes(met1Node, met2Node, models[i], thisInst);                        
                        thisInst.metPairList.metPairs[j].AddNodesToWS_Pred(models, pathOfNodes, models[i].radius, this);
                    }

                    if (metsUsed.Length > 1)
                        thisInst.metPairList.SweepFindMinError(models[i], thisInst);
                }

                // Do WS estimates with new models
                for (int j = 0; j < numPairs; j++)
                {
                    int WS_PredInd = thisInst.metPairList.metPairs[j].GetWS_PredInd(models, this);

                    for (int radiusIndex = 0; radiusIndex < numRadii; radiusIndex++)
                        thisInst.metPairList.metPairs[j].DoMetCrossPred(WS_PredInd, radiusIndex, thisInst);
                }
                                   
                // Calculate RMS with UW&DW models
                CalcRMS_Overall_and_Sectorwise(ref models, thisInst);
            }
            else
                models = GetModels(thisInst, metsUsed, thisTOD, thisSeason, thisHeight, false);                
            
            return models;
        }

        /// <summary> Calculates and returns the total estimated change in wind speed. </summary> 
        public double GetTotalWS(Coeff_Delta_WS[] coeffsDelta)
        {            
            double totalWS = 0;
            int numCoeffs = 0;

            if (coeffsDelta != null)
                numCoeffs = coeffsDelta.Length;


            if (numCoeffs > 0)
                totalWS = coeffsDelta[numCoeffs - 1].deltaWS_Expo;

            return totalWS;
        }

        /// <summary> Performs and returns wind speed estimate along path of nodes using specified model in one wind direction sector (WD_Ind) </summary>        
        public double DoWS_EstimateOneWDTimeSeries(double startWS, int WD_Ind, Met startMet, Nodes endNode, Nodes[] pathOfNodes, Model thisModel, Continuum thisInst)
        {           
            double thisWS_Est = 0;

            // Calculates wind speed from Met 1 to Met 2 along path of nodes 
            int numNodes = 0;
            
            //  double UW_CW_Grade = 0;
            //  double UW_PL_Grade = 0;

            double avgUW = 0;
            double avgDW = 0;
            double avgP10DW = 0;
            double avgP10UW = 0;

            double UW_1 = 0; // UW exposure at met or node 1
            double DW_1 = 0;
            double UW_2 = 0;
            double DW_2 = 0;

            double WS_1 = 0; // WS at met or node 1

            double UW_SR_1 = 0; // UW surface roughness at met or node 1
            double DW_SR_1 = 0;
            double UW_SR_2 = 0;
            double DW_SR_2 = 0;

            double UW_DH_1 = 0; // UW displacement height at met or node 1
            double DW_DH_1 = 0;
            double UW_DH_2 = 0;
            double DW_DH_2 = 0;

            Coeff_Delta_WS[] coeffsDelta;
            double deltaWS_UWExpo = 0; // change in wind speed due to change in UW exposure
            double deltaWS_DWExpo = 0; // change in wind speed due to change in DW exposure
            double deltaWS_UW_SR = 0; // change in wind speed due to change in UW surface roughness and displacement height
            double deltaWS_DW_SR = 0; // change in wind speed due to change in DW surface roughness and displacement height

            double UW_Stab_Corr_1 = 0;
            double UW_Stab_Corr_2 = 0;
            double DW_Stab_Corr_1 = 0;
            double DW_Stab_Corr_2 = 0;

      //      NodeCollection nodeList = new NodeCollection();
            Nodes thisNode;
            Nodes node1;
            Nodes node2;
            NodeCollection.Node_UTMs nodeUTM1 = new NodeCollection.Node_UTMs();
            nodeUTM1.UTMX = startMet.UTMX;
            nodeUTM1.UTMY = startMet.UTMY;
            NodeCollection.Node_UTMs nodeUTM2 = new NodeCollection.Node_UTMs();
                        
            int radInd = thisInst.radiiList.GetRadiusInd(thisModel.radius);                        

            if (pathOfNodes != null)
                numNodes = pathOfNodes.Length;                     
                                            
            double sectorWS = startWS;

            if (numNodes > 0)
            {
                double[] WS_AtNodes = new double[numNodes];
                thisNode = pathOfNodes[0];
                double[] nodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, thisNode.UTMX, thisNode.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);
                nodeUTM2.UTMX = thisNode.UTMX;
                nodeUTM2.UTMY = thisNode.UTMY;
                                
                // Met to first node                    
                avgP10DW = (startMet.gridStats.stats[radInd].P10_DW[WD_Ind] + thisNode.gridStats.stats[radInd].P10_DW[WD_Ind]) / 2;
                avgP10UW = (startMet.gridStats.stats[radInd].P10_UW[WD_Ind] + thisNode.gridStats.stats[radInd].P10_UW[WD_Ind]) / 2;

                UW_1 = startMet.expo[radInd].GetWgtAvg(startMet.GetWS_WD_Dist(thisInst.modeledHeight, thisModel.timeOfDay, thisModel.season).windRose, WD_Ind, "UW", "Expo");
                UW_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "Expo");
                avgUW = (UW_1 + UW_2) / 2;

                // to be used in flow around  vs. flow over hill algorithm
                //      UW_CW_Grade = (startMet.expo[radiusIndex].UW_P10CrossGrade[WD_Ind] + thisNode.expo[radiusIndex].UW_P10CrossGrade[WD_Ind]) / 2;
                //      UW_PL_Grade = (startMet.expo[radiusIndex].UW_ParallelGrade[WD_Ind] + thisNode.expo[radiusIndex].UW_ParallelGrade[WD_Ind]) / 2;

                WS_1 = sectorWS;
                Met.WSWD_Dist startMetDist = startMet.GetWS_WD_Dist(thisInst.modeledHeight, thisModel.timeOfDay, thisModel.season);

                if (thisInst.topo.useSR == true)
                {
                    UW_SR_1 = startMet.expo[radInd].GetWgtAvg(startMetDist.windRose, WD_Ind, "UW", "SR");
                    UW_SR_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "SR");
                    UW_DH_1 = startMet.expo[radInd].GetWgtAvg(startMetDist.windRose, WD_Ind, "UW", "DH");
                    UW_DH_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "DH");

                    DW_SR_1 = startMet.expo[radInd].GetWgtAvg(startMetDist.windRose, WD_Ind, "DW", "SR");
                    DW_SR_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "SR");
                    DW_DH_1 = startMet.expo[radInd].GetWgtAvg(startMetDist.windRose, WD_Ind, "DW", "DH");
                    DW_DH_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "DH");
                }

                DW_1 = startMet.expo[radInd].GetWgtAvg(startMetDist.windRose, WD_Ind, "DW", "Expo");
                DW_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "Expo");

                avgDW = (DW_1 + DW_2) / 2;                                

                coeffsDelta = Get_DeltaWS_UW_Expo(UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, radInd, startMet.flowSepNodes,
                                                        thisNode.flowSepNodes, WS_1, thisInst.topo.useSepMod, nodeUTM1, nodeUTM2);
                deltaWS_UWExpo = GetTotalWS(coeffsDelta);
                coeffsDelta = Get_DeltaWS_DW_Expo(WS_1, UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, thisInst.topo.useSepMod);
                deltaWS_DWExpo = GetTotalWS(coeffsDelta);

                if (thisInst.topo.useSR == true)
                {
                    UW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_1, thisInst.topo.useSepMod, "UW");
                    UW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_2, thisInst.topo.useSepMod, "UW");
                    deltaWS_UW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, UW_SR_1, UW_SR_2, UW_DH_1, UW_DH_2, UW_Stab_Corr_1, UW_Stab_Corr_2);

                    DW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_1, thisInst.topo.useSepMod, "DW");
                    DW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_2, thisInst.topo.useSepMod, "DW");
                    deltaWS_DW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, DW_SR_1, DW_SR_2, DW_DH_1, DW_DH_2, DW_Stab_Corr_1, DW_Stab_Corr_2);
                }

                // Avg WS Estimate at first node
                if ((WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR) > 0)
                    WS_AtNodes[0] = WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR;
                else
                    WS_AtNodes[0] = 0.05f; // no negative wind speed estimates!                    


                // Now do estimates up to met2
                WS_1 = WS_AtNodes[0];
                int nodeInd = 0;
                while (nodeInd + 1 < numNodes)
                {
                    node1 = pathOfNodes[nodeInd];
                    nodeUTM1.UTMX = pathOfNodes[nodeInd].UTMX;
                    nodeUTM1.UTMY = pathOfNodes[nodeInd].UTMY;
                    double[] node1WindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, node1.UTMX, node1.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);

                    node2 = pathOfNodes[nodeInd + 1];
                    nodeUTM2.UTMX = pathOfNodes[nodeInd + 1].UTMX;
                    nodeUTM2.UTMY = pathOfNodes[nodeInd + 1].UTMY;
                    double[] node2WindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, node2.UTMX, node2.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);
                                        
                    avgP10DW = (node1.gridStats.stats[radInd].P10_DW[WD_Ind] + node2.gridStats.stats[radInd].P10_DW[WD_Ind]) / 2;
                    avgP10UW = (node1.gridStats.stats[radInd].P10_UW[WD_Ind] + node2.gridStats.stats[radInd].P10_UW[WD_Ind]) / 2;

                    // for flow around vs. flow over hill algorithm
                    //      UW_CW_Grade = (node1.expo[radInd].UW_P10CrossGrade[WD_Ind] + node2.expo[radInd].UW_P10CrossGrade[WD_Ind]) / 2;
                    //      UW_PL_Grade = (node1.expo[radInd].UW_ParallelGrade[WD_Ind] + node2.expo[radInd].UW_ParallelGrade[WD_Ind]) / 2;

                    UW_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "UW", "Expo");
                    UW_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "UW", "Expo");

                    avgUW = (UW_1 + UW_2) / 2;
                                        
                    if (thisInst.topo.useSR == true)
                    {
                        UW_SR_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "UW", "SR");
                        UW_SR_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "UW", "SR");
                        UW_DH_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "UW", "DH");
                        UW_DH_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "UW", "DH");

                        DW_SR_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "DW", "SR");
                        DW_SR_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "DW", "SR");
                        DW_DH_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "DW", "DH");
                        DW_DH_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "DW", "DH");
                    }

                    DW_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "DW", "Expo");
                    DW_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "DW", "Expo");

                    avgDW = (DW_1 + DW_2) / 2;
                                        
                    coeffsDelta = Get_DeltaWS_UW_Expo(UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, radInd, node1.flowSepNodes, node2.flowSepNodes,
                                                        WS_1, thisInst.topo.useSepMod, nodeUTM1, nodeUTM2);
                    deltaWS_UWExpo = GetTotalWS(coeffsDelta);
                    coeffsDelta = Get_DeltaWS_DW_Expo(WS_1, UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, thisInst.topo.useSepMod);
                    deltaWS_DWExpo = GetTotalWS(coeffsDelta);

                    if (thisInst.topo.useSR == true)
                    {
                        UW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_1, thisInst.topo.useSepMod, "UW");
                        UW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_2, thisInst.topo.useSepMod, "UW");
                        deltaWS_UW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, UW_SR_1, UW_SR_2, UW_DH_1, UW_DH_2, UW_Stab_Corr_1, UW_Stab_Corr_2);

                        DW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_1, thisInst.topo.useSepMod, "DW");
                        DW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_2, thisInst.topo.useSepMod, "DW");
                        deltaWS_DW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, DW_SR_1, DW_SR_2, DW_DH_1, DW_DH_2, DW_Stab_Corr_1, DW_Stab_Corr_2);
                    }

                    if ((WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR) > 0)
                        WS_AtNodes[nodeInd + 1] = WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR;
                    else
                        WS_AtNodes[nodeInd + 1] = 0.05f; // no negative wind speed estimates!

                    WS_1 = WS_AtNodes[nodeInd + 1];
                    nodeInd++;
                }

                // lastNode to End Node
                thisNode = pathOfNodes[nodeInd];
                nodeUTM1.UTMX = pathOfNodes[nodeInd].UTMX;
                nodeUTM1.UTMY = pathOfNodes[nodeInd].UTMY;

                nodeUTM2.UTMX = endNode.UTMX;
                nodeUTM2.UTMY = endNode.UTMY;

                nodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, thisNode.UTMX, thisNode.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);
                double[] endNodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, endNode.UTMX, endNode.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);
                                                
                avgP10DW = (endNode.gridStats.stats[radInd].P10_DW[WD_Ind] + thisNode.gridStats.stats[radInd].P10_DW[WD_Ind]) / 2;
                avgP10UW = (endNode.gridStats.stats[radInd].P10_UW[WD_Ind] + thisNode.gridStats.stats[radInd].P10_UW[WD_Ind]) / 2;

                //     UW_CW_Grade = (thisNode.expo[radInd].UW_P10CrossGrade[WD_Ind] + endNode.expo[radInd].UW_P10CrossGrade[WD_Ind]) / 2;
                //     UW_PL_Grade = (thisNode.expo[radInd].UW_ParallelGrade[WD_Ind] + endNode.expo[radInd].UW_ParallelGrade[WD_Ind]) / 2;

                UW_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "Expo");
                UW_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "Expo");

                avgUW = (UW_1 + UW_2) / 2;
                                    
                if (thisInst.topo.useSR == true)
                {
                    UW_SR_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "SR");
                    UW_SR_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "SR");
                    UW_DH_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "DH");
                    UW_DH_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "DH");

                    DW_SR_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "SR");
                    DW_SR_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "SR");
                    DW_DH_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "DH");
                    DW_DH_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "DH");
                }

                DW_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "Expo");
                DW_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "Expo");

                avgDW = (DW_1 + DW_2) / 2;
                                
                coeffsDelta = Get_DeltaWS_UW_Expo(UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, radInd, thisNode.flowSepNodes, endNode.flowSepNodes,
                                                    WS_1, thisInst.topo.useSepMod, nodeUTM1, nodeUTM2);
                deltaWS_UWExpo = GetTotalWS(coeffsDelta);
                coeffsDelta = Get_DeltaWS_DW_Expo(WS_1, UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, thisInst.topo.useSepMod);
                deltaWS_DWExpo = GetTotalWS(coeffsDelta);

                if (thisInst.topo.useSR == true)
                {
                    UW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_1, thisInst.topo.useSepMod, "UW");
                    UW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_2, thisInst.topo.useSepMod, "UW");
                    deltaWS_UW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, UW_SR_1, UW_SR_2, UW_DH_1, UW_DH_2, UW_Stab_Corr_1, UW_Stab_Corr_2);

                    DW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_1, thisInst.topo.useSepMod, "DW");
                    DW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_2, thisInst.topo.useSepMod, "DW");
                    deltaWS_DW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, DW_SR_1, DW_SR_2, DW_DH_1, DW_DH_2, DW_Stab_Corr_1, DW_Stab_Corr_2);
                }

                if ((WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR) > 0)
                    thisWS_Est = WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR;
                else
                    thisWS_Est = 0.05f;
                                
            }
            else
            {
                // No nodes so just one step from Met 1 to Target
                WS_1 = sectorWS;

                double[] startWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, startMet.UTMX, startMet.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);
                double[] endNodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, endNode.UTMX, endNode.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);
                                
                nodeUTM2.UTMX = endNode.UTMX;
                nodeUTM2.UTMY = endNode.UTMY;
                                
                avgP10DW = (startMet.gridStats.stats[radInd].P10_DW[WD_Ind] + endNode.gridStats.stats[radInd].P10_DW[WD_Ind]) / 2;
                avgP10UW = (startMet.gridStats.stats[radInd].P10_UW[WD_Ind] + endNode.gridStats.stats[radInd].P10_UW[WD_Ind]) / 2;

                //     UW_CW_Grade = (endNode.expo[radInd].UW_P10CrossGrade[WD_Ind] + startMet.expo[radInd].UW_P10CrossGrade[WD_Ind]) / 2;
                //     UW_PL_Grade = (endNode.expo[radInd].UW_ParallelGrade[WD_Ind] + startMet.expo[radInd].UW_ParallelGrade[WD_Ind]) / 2;

                UW_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "UW", "Expo");
                UW_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "Expo");
                avgUW = (UW_1 + UW_2) / 2;
                                    
                if (thisInst.topo.useSR == true)
                {
                    UW_SR_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "UW", "SR");
                    UW_SR_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "SR");
                    UW_DH_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "UW", "DH");
                    UW_DH_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "DH");

                    DW_SR_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "DW", "SR");
                    DW_SR_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "SR");
                    DW_DH_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "DW", "DH");
                    DW_DH_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "DH");
                }

                DW_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "DW", "Expo");
                DW_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "Expo");

                avgDW = (DW_1 + DW_2) / 2;
                                
                coeffsDelta = Get_DeltaWS_UW_Expo(UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, radInd, startMet.flowSepNodes, endNode.flowSepNodes,
                                                    WS_1, thisInst.topo.useSepMod, nodeUTM1, nodeUTM2);
                deltaWS_UWExpo = GetTotalWS(coeffsDelta);
                coeffsDelta = Get_DeltaWS_DW_Expo(WS_1, UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, thisInst.topo.useSepMod);
                deltaWS_DWExpo = GetTotalWS(coeffsDelta);

                if (thisInst.topo.useSR == true)
                {
                    UW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_1, thisInst.topo.useSepMod, "UW");
                    UW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_2, thisInst.topo.useSepMod, "UW");
                    deltaWS_UW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, UW_SR_1, UW_SR_2, UW_DH_1, UW_DH_2, UW_Stab_Corr_1, UW_Stab_Corr_2);

                    DW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_1, thisInst.topo.useSepMod, "DW");
                    DW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_2, thisInst.topo.useSepMod, "DW");
                    deltaWS_DW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, DW_SR_1, DW_SR_2, DW_DH_1, DW_DH_2, DW_Stab_Corr_1, DW_Stab_Corr_2);
                }

                if ((WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR) > 0)
                    thisWS_Est = WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR;
                else
                    thisWS_Est = 0.05f;                            

            }
            
            return thisWS_Est;
        }

        /// <summary> Performs wind speed estimate along path of nodes using specified model and returns WS_Est_Struct object with results </summary>        
        public WS_Est_Struct DoWS_Estimate(Met startMet, Nodes endNode, Nodes[] pathOfNodes, Model thisModel, Continuum thisInst)
        {             
            WS_Est_Struct WS_Est_Return = new WS_Est_Struct();

            // Calculates wind speed from Met 1 to Met 2 along path of nodes 
            int numNodes = 0;
                 
          //  double UW_CW_Grade = 0;
          //  double UW_PL_Grade = 0;

            double avgUW = 0;
            double avgDW = 0;
            double avgP10DW = 0;
            double avgP10UW = 0;

            double UW_1 = 0; // UW exposure at met or node 1
            double DW_1 = 0;
            double UW_2 = 0;
            double DW_2 = 0;

            double WS_1 = 0; // WS at met or node 1
            
            double UW_SR_1 = 0; // UW surface roughness at met or node 1
            double DW_SR_1 = 0;
            double UW_SR_2 = 0;
            double DW_SR_2 = 0;

            double UW_DH_1 = 0; // UW displacement height at met or node 1
            double DW_DH_1 = 0;
            double UW_DH_2 = 0;
            double DW_DH_2 = 0;

            Coeff_Delta_WS[] coeffsDelta; 
            double deltaWS_UWExpo = 0; // change in wind speed due to change in UW exposure
            double deltaWS_DWExpo = 0; // change in wind speed due to change in DW exposure
            double deltaWS_UW_SR = 0; // change in wind speed due to change in UW surface roughness and displacement height
            double deltaWS_DW_SR = 0; // change in wind speed due to change in DW surface roughness and displacement height

            double UW_Stab_Corr_1 = 0;
            double UW_Stab_Corr_2 = 0;
            double DW_Stab_Corr_1 = 0;
            double DW_Stab_Corr_2 = 0;

            NodeCollection nodeList = new NodeCollection();
            Nodes thisNode;
            Nodes node1;
            Nodes node2;
            NodeCollection.Node_UTMs nodeUTM1 = new NodeCollection.Node_UTMs();
            nodeUTM1.UTMX = startMet.UTMX;
            nodeUTM1.UTMY = startMet.UTMY;
            NodeCollection.Node_UTMs nodeUTM2 = new NodeCollection.Node_UTMs();                      
            
            int numWD = thisInst.metList.numWD;
            int radInd = thisInst.radiiList.GetRadiusInd(thisModel.radius);            

            if (pathOfNodes != null)
                numNodes = pathOfNodes.Length;

            WS_Est_Return.sectorWS = new double[numWD];
            double[] sectorWS = new double[numWD];
            double[] WS_Equiv;
            Met.WSWD_Dist startMetDist = startMet.GetWS_WD_Dist(thisInst.modeledHeight, thisModel.timeOfDay, thisModel.season);

            for (int WD = 0; WD < numWD; WD++)
                sectorWS[WD] = startMetDist.WS * startMetDist.sectorWS_Ratio[WD];
            
            if (numNodes > 0)
            {
                WS_Est_Return.sectorWS_AtNodes = new double[numNodes, numWD];
                thisNode = pathOfNodes[0];
                double[] nodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, thisNode.UTMX, thisNode.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);
                nodeUTM2.UTMX = thisNode.UTMX;
                nodeUTM2.UTMY = thisNode.UTMY;
                WS_Equiv = GetWS_Equiv(startMetDist.windRose, nodeWindRose, sectorWS);

                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                {
                    // Met to first node                    
                    avgP10DW = (startMet.gridStats.stats[radInd].P10_DW[WD_Ind] + thisNode.gridStats.stats[radInd].P10_DW[WD_Ind]) / 2;
                    avgP10UW = (startMet.gridStats.stats[radInd].P10_UW[WD_Ind] + thisNode.gridStats.stats[radInd].P10_UW[WD_Ind]) / 2;

                    UW_1 = startMet.expo[radInd].GetWgtAvg(startMetDist.windRose, WD_Ind, "UW", "Expo");
                    UW_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "Expo");
                    avgUW = (UW_1 + UW_2) / 2;

                    // to be used in flow around  vs. flow over hill algorithm
              //      UW_CW_Grade = (startMet.expo[radiusIndex].UW_P10CrossGrade[WD_Ind] + thisNode.expo[radiusIndex].UW_P10CrossGrade[WD_Ind]) / 2;
              //      UW_PL_Grade = (startMet.expo[radiusIndex].UW_ParallelGrade[WD_Ind] + thisNode.expo[radiusIndex].UW_ParallelGrade[WD_Ind]) / 2;

                    WS_1 = WS_Equiv[WD_Ind];

                    if (thisInst.topo.useSR == true) {
                        UW_SR_1 = startMet.expo[radInd].GetWgtAvg(startMetDist.windRose, WD_Ind, "UW", "SR");
                        UW_SR_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "SR");
                        UW_DH_1 = startMet.expo[radInd].GetWgtAvg(startMetDist.windRose, WD_Ind, "UW", "DH");
                        UW_DH_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "DH");

                        DW_SR_1 = startMet.expo[radInd].GetWgtAvg(startMetDist.windRose, WD_Ind, "DW", "SR");
                        DW_SR_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "SR");
                        DW_DH_1 = startMet.expo[radInd].GetWgtAvg(startMetDist.windRose, WD_Ind, "DW", "DH");
                        DW_DH_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "DH");
                    }

                    DW_1 = startMet.expo[radInd].GetWgtAvg(startMetDist.windRose, WD_Ind, "DW", "Expo");
                    DW_2 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "Expo");

                    avgDW = (DW_1 + DW_2) / 2;                    

                    coeffsDelta = Get_DeltaWS_UW_Expo(UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, radInd, startMet.flowSepNodes,
                                                           thisNode.flowSepNodes, WS_1, thisInst.topo.useSepMod, nodeUTM1, nodeUTM2);
                    deltaWS_UWExpo = GetTotalWS(coeffsDelta);
                    coeffsDelta = Get_DeltaWS_DW_Expo(WS_1, UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, thisInst.topo.useSepMod);
                    deltaWS_DWExpo = GetTotalWS(coeffsDelta);

                    if (thisInst.topo.useSR == true) {
                        UW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_1, thisInst.topo.useSepMod, "UW");
                        UW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_2, thisInst.topo.useSepMod, "UW");
                        deltaWS_UW_SR = GetDeltaWS_SRDH(WS_1, startMetDist.height, UW_SR_1, UW_SR_2, UW_DH_1, UW_DH_2, UW_Stab_Corr_1, UW_Stab_Corr_2);

                        DW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_1, thisInst.topo.useSepMod, "DW");
                        DW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_2, thisInst.topo.useSepMod, "DW");
                        deltaWS_DW_SR = GetDeltaWS_SRDH(WS_1, startMetDist.height, DW_SR_1, DW_SR_2, DW_DH_1, DW_DH_2, DW_Stab_Corr_1, DW_Stab_Corr_2);
                    }

                    // Avg WS Estimate at first node
                    if ((WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR) > 0)
                        WS_Est_Return.sectorWS_AtNodes[0, WD_Ind] = WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR;
                    else
                        WS_Est_Return.sectorWS_AtNodes[0, WD_Ind] = 0.05f; // no negative wind speed estimates!                    
                }

                // Now do estimates up to met2
                int nodeInd = 0;
                while (nodeInd + 1 < numNodes)
                {
                    node1 = pathOfNodes[nodeInd];
                    nodeUTM1.UTMX = pathOfNodes[nodeInd].UTMX;
                    nodeUTM1.UTMY = pathOfNodes[nodeInd].UTMY;
                    double[] node1WindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, node1.UTMX, node1.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);
                    
                    node2 = pathOfNodes[nodeInd + 1];
                    nodeUTM2.UTMX = pathOfNodes[nodeInd + 1].UTMX;
                    nodeUTM2.UTMY = pathOfNodes[nodeInd + 1].UTMY;
                    double[] node2WindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, node2.UTMX, node2.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);
                                 
                    for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                        sectorWS[WD_Ind] = WS_Est_Return.sectorWS_AtNodes[nodeInd, WD_Ind];

                    WS_Equiv = GetWS_Equiv(node1WindRose, node2WindRose, sectorWS);

                    for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                    {
                        avgP10DW = (node1.gridStats.stats[radInd].P10_DW[WD_Ind] + node2.gridStats.stats[radInd].P10_DW[WD_Ind]) / 2;
                        avgP10UW = (node1.gridStats.stats[radInd].P10_UW[WD_Ind] + node2.gridStats.stats[radInd].P10_UW[WD_Ind]) / 2;

                        // for flow around vs. flow over hill algorithm
                  //      UW_CW_Grade = (node1.expo[radInd].UW_P10CrossGrade[WD_Ind] + node2.expo[radInd].UW_P10CrossGrade[WD_Ind]) / 2;
                  //      UW_PL_Grade = (node1.expo[radInd].UW_ParallelGrade[WD_Ind] + node2.expo[radInd].UW_ParallelGrade[WD_Ind]) / 2;

                        UW_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "UW", "Expo");
                        UW_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "UW", "Expo");

                        avgUW = (UW_1 + UW_2) / 2;

                        WS_1 = WS_Equiv[WD_Ind];

                        if (thisInst.topo.useSR == true) {
                            UW_SR_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "UW", "SR");
                            UW_SR_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "UW", "SR");
                            UW_DH_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "UW", "DH");
                            UW_DH_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "UW", "DH");

                            DW_SR_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "DW", "SR");
                            DW_SR_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "DW", "SR");
                            DW_DH_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "DW", "DH");
                            DW_DH_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "DW", "DH");
                        }

                        DW_1 = node1.expo[radInd].GetWgtAvg(node1WindRose, WD_Ind, "DW", "Expo");
                        DW_2 = node2.expo[radInd].GetWgtAvg(node2WindRose, WD_Ind, "DW", "Expo");

                        avgDW = (DW_1 + DW_2) / 2;                                                

                        coeffsDelta = Get_DeltaWS_UW_Expo(UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, radInd, node1.flowSepNodes, node2.flowSepNodes,
                                                            WS_1, thisInst.topo.useSepMod, nodeUTM1, nodeUTM2);
                        deltaWS_UWExpo = GetTotalWS(coeffsDelta);
                        coeffsDelta = Get_DeltaWS_DW_Expo(WS_1, UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, thisInst.topo.useSepMod);
                        deltaWS_DWExpo = GetTotalWS(coeffsDelta);

                        if (thisInst.topo.useSR == true) {
                            UW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_1, thisInst.topo.useSepMod, "UW");
                            UW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_2, thisInst.topo.useSepMod, "UW");
                            deltaWS_UW_SR = GetDeltaWS_SRDH(WS_1, startMetDist.height, UW_SR_1, UW_SR_2, UW_DH_1, UW_DH_2, UW_Stab_Corr_1, UW_Stab_Corr_2);

                            DW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_1, thisInst.topo.useSepMod, "DW");
                            DW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_2, thisInst.topo.useSepMod, "DW");
                            deltaWS_DW_SR = GetDeltaWS_SRDH(WS_1, startMetDist.height, DW_SR_1, DW_SR_2, DW_DH_1, DW_DH_2, DW_Stab_Corr_1, DW_Stab_Corr_2);
                        }

                        if ((WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR) > 0)
                            WS_Est_Return.sectorWS_AtNodes[nodeInd + 1, WD_Ind] = WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR;
                        else
                            WS_Est_Return.sectorWS_AtNodes[nodeInd + 1, WD_Ind] = 0.05f; // no negative wind speed estimates!

                    }
                    nodeInd++;
                }

                // lastNode to End Node
                thisNode = pathOfNodes[nodeInd];
                nodeUTM1.UTMX = pathOfNodes[nodeInd].UTMX;
                nodeUTM1.UTMY = pathOfNodes[nodeInd].UTMY;

                nodeUTM2.UTMX = endNode.UTMX;
                nodeUTM2.UTMY = endNode.UTMY;

                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                    sectorWS[WD_Ind] = WS_Est_Return.sectorWS_AtNodes[nodeInd, WD_Ind];
                
                nodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, thisNode.UTMX, thisNode.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);
                double[] endNodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, endNode.UTMX, endNode.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);

                WS_Equiv = GetWS_Equiv(nodeWindRose, endNodeWindRose, sectorWS);
                               

                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                {
                    avgP10DW = (endNode.gridStats.stats[radInd].P10_DW[WD_Ind] + thisNode.gridStats.stats[radInd].P10_DW[WD_Ind]) / 2;
                    avgP10UW = (endNode.gridStats.stats[radInd].P10_UW[WD_Ind] + thisNode.gridStats.stats[radInd].P10_UW[WD_Ind]) / 2;

               //     UW_CW_Grade = (thisNode.expo[radInd].UW_P10CrossGrade[WD_Ind] + endNode.expo[radInd].UW_P10CrossGrade[WD_Ind]) / 2;
               //     UW_PL_Grade = (thisNode.expo[radInd].UW_ParallelGrade[WD_Ind] + endNode.expo[radInd].UW_ParallelGrade[WD_Ind]) / 2;

                    UW_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "Expo");
                    UW_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "Expo");

                    avgUW = (UW_1 + UW_2) / 2;

                    WS_1 = WS_Equiv[WD_Ind];

                    if (thisInst.topo.useSR == true)
                    {
                        UW_SR_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "SR");
                        UW_SR_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "SR");
                        UW_DH_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "UW", "DH");
                        UW_DH_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "DH");

                        DW_SR_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "SR");
                        DW_SR_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "SR");
                        DW_DH_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "DH");
                        DW_DH_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "DH");
                    }

                    DW_1 = thisNode.expo[radInd].GetWgtAvg(nodeWindRose, WD_Ind, "DW", "Expo");
                    DW_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "Expo");

                    avgDW = (DW_1 + DW_2) / 2;                    

                    coeffsDelta = Get_DeltaWS_UW_Expo(UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, radInd, thisNode.flowSepNodes, endNode.flowSepNodes,
                                                        WS_1, thisInst.topo.useSepMod, nodeUTM1, nodeUTM2);
                    deltaWS_UWExpo = GetTotalWS(coeffsDelta);
                    coeffsDelta = Get_DeltaWS_DW_Expo(WS_1, UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, thisInst.topo.useSepMod);
                    deltaWS_DWExpo = GetTotalWS(coeffsDelta);

                    if (thisInst.topo.useSR == true)
                    {
                        UW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_1, thisInst.topo.useSepMod, "UW");
                        UW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_2, thisInst.topo.useSepMod, "UW");
                        deltaWS_UW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, UW_SR_1, UW_SR_2, UW_DH_1, UW_DH_2, UW_Stab_Corr_1, UW_Stab_Corr_2);

                        DW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_1, thisInst.topo.useSepMod, "DW");
                        DW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_2, thisInst.topo.useSepMod, "DW");
                        deltaWS_DW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, DW_SR_1, DW_SR_2, DW_DH_1, DW_DH_2, DW_Stab_Corr_1, DW_Stab_Corr_2);
                    }

                    if ((WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR) > 0)
                        WS_Est_Return.sectorWS[WD_Ind] = WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR;
                    else
                        WS_Est_Return.sectorWS[WD_Ind] = 0.05f;

                }
            }
            else {
                // No nodes so just one step from Met 1 to Target
                double[]  startWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, startMet.UTMX, startMet.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight); // should use start met wind rose and not interpolated
                double[] endNodeWindRose = thisInst.metList.GetInterpolatedWindRose(thisModel.metsUsed, endNode.UTMX, endNode.UTMY, thisModel.timeOfDay, thisModel.season, thisInst.modeledHeight);

                WS_Equiv = GetWS_Equiv(startWindRose, endNodeWindRose, sectorWS);
                nodeUTM2.UTMX = endNode.UTMX;
                nodeUTM2.UTMY = endNode.UTMY;

                for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                {
                    avgP10DW = (startMet.gridStats.stats[radInd].P10_DW[WD_Ind] + endNode.gridStats.stats[radInd].P10_DW[WD_Ind]) / 2;
                    avgP10UW = (startMet.gridStats.stats[radInd].P10_UW[WD_Ind] + endNode.gridStats.stats[radInd].P10_UW[WD_Ind]) / 2;

               //     UW_CW_Grade = (endNode.expo[radInd].UW_P10CrossGrade[WD_Ind] + startMet.expo[radInd].UW_P10CrossGrade[WD_Ind]) / 2;
               //     UW_PL_Grade = (endNode.expo[radInd].UW_ParallelGrade[WD_Ind] + startMet.expo[radInd].UW_ParallelGrade[WD_Ind]) / 2;

                    UW_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "UW", "Expo");
                    UW_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "Expo");
                    avgUW = (UW_1 + UW_2) / 2;

                    WS_1 = WS_Equiv[WD_Ind];

                    if (thisInst.topo.useSR == true) {
                        UW_SR_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "UW", "SR");
                        UW_SR_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "SR");
                        UW_DH_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "UW", "DH");
                        UW_DH_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "UW", "DH");

                        DW_SR_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "DW", "SR");
                        DW_SR_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "SR");
                        DW_DH_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "DW", "DH");
                        DW_DH_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "DH");
                    }

                    DW_1 = startMet.expo[radInd].GetWgtAvg(startWindRose, WD_Ind, "DW", "Expo");
                    DW_2 = endNode.expo[radInd].GetWgtAvg(endNodeWindRose, WD_Ind, "DW", "Expo");

                    avgDW = (DW_1 + DW_2) / 2;                    

                    coeffsDelta = Get_DeltaWS_UW_Expo(UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, radInd, startMet.flowSepNodes, endNode.flowSepNodes, 
                                                        WS_1, thisInst.topo.useSepMod, nodeUTM1, nodeUTM2);
                    deltaWS_UWExpo = GetTotalWS(coeffsDelta);
                    coeffsDelta = Get_DeltaWS_DW_Expo(WS_1, UW_1, UW_2, DW_1, DW_2, avgP10UW, avgP10DW, thisModel, WD_Ind, thisInst.topo.useSepMod);
                    deltaWS_DWExpo = GetTotalWS(coeffsDelta);

                    if (thisInst.topo.useSR == true)
                    {
                        UW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_1, thisInst.topo.useSepMod, "UW");
                        UW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, UW_SR_2, thisInst.topo.useSepMod, "UW");
                        deltaWS_UW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, UW_SR_1, UW_SR_2, UW_DH_1, UW_DH_2, UW_Stab_Corr_1, UW_Stab_Corr_2);

                        DW_Stab_Corr_1 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_1, thisInst.topo.useSepMod, "DW");
                        DW_Stab_Corr_2 = thisModel.GetStabilityCorrection(avgUW, avgDW, WD_Ind, DW_SR_2, thisInst.topo.useSepMod, "DW");
                        deltaWS_DW_SR = GetDeltaWS_SRDH(WS_1, thisInst.modeledHeight, DW_SR_1, DW_SR_2, DW_DH_1, DW_DH_2, DW_Stab_Corr_1, DW_Stab_Corr_2);
                    }

                    if ((WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR) > 0)
                        WS_Est_Return.sectorWS[WD_Ind] = WS_1 + deltaWS_UWExpo + deltaWS_DWExpo + deltaWS_UW_SR + deltaWS_DW_SR;
                    else
                        WS_Est_Return.sectorWS[WD_Ind] = 0.05f;

                }

            }

            return WS_Est_Return;

        }

        /// <summary> Calculates the RMS of all met cross-prediction errors (overall and sectorwise) for specified models. Only using met pairs where both mets are used in the models </summary>        
        public void CalcRMS_Overall_and_Sectorwise(ref Model[] models, Continuum thisInst)
        {                   
            int numRadii = models.Length;
            int numWD = models[0].downhill_A.Length;
            double RMS_WS_Est = 0;
            double RMS_WS_Est_count = 0;
            double[] RMS_Sect_WS = new double[numWD];
            double[] RMS_Sect_WS_count = new double[numWD];                                                      
            int pairCount = thisInst.metPairList.PairCount;
            
            if (pairCount > 0)
            {
                for (int radiusIndex = 0; radiusIndex < numRadii; radiusIndex++)
                {
                    Model model = models[radiusIndex];                                                                         

                    for (int j = 0; j < pairCount; j++)
                    {
                        if (model.IsMetUsedInModel(thisInst.metPairList.metPairs[j].met1.name) && model.IsMetUsedInModel(thisInst.metPairList.metPairs[j].met2.name))
                        { 
                            int WS_PredInd = GetWS_PredInd(thisInst.metPairList.metPairs[j], models);

                            if (WS_PredInd == -1)
                                return;

                            RMS_WS_Est = RMS_WS_Est + Math.Pow(thisInst.metPairList.metPairs[j].WS_Pred[WS_PredInd, radiusIndex].percErr[1], 2);
                            RMS_WS_Est_count++;

                            for (int WD = 0; WD < numWD; WD++)
                            {
                                RMS_Sect_WS[WD] = RMS_Sect_WS[WD] + Math.Pow(thisInst.metPairList.metPairs[j].WS_Pred[WS_PredInd, radiusIndex].percErrSector[1, WD], 2);
                                RMS_Sect_WS_count[WD]++;
                            }

                            RMS_WS_Est = RMS_WS_Est + Math.Pow(thisInst.metPairList.metPairs[j].WS_Pred[WS_PredInd, radiusIndex].percErr[0], 2);
                            RMS_WS_Est_count++;

                            for (int WD = 0; WD < numWD; WD++)
                            {
                                RMS_Sect_WS[WD] = RMS_Sect_WS[WD] + Math.Pow(thisInst.metPairList.metPairs[j].WS_Pred[WS_PredInd, radiusIndex].percErrSector[0, WD], 2);
                                RMS_Sect_WS_count[WD]++;
                            }
                        }
                    }

                    if (RMS_WS_Est_count > 0)
                        RMS_WS_Est = Math.Pow((RMS_WS_Est / RMS_WS_Est_count), 0.5);
                    else
                        RMS_WS_Est = 0;
                    

                    for (int WD = 0; WD < numWD; WD++)
                    {
                        if (RMS_Sect_WS_count[WD] > 0)
                            RMS_Sect_WS[WD] = Math.Pow((RMS_Sect_WS[WD] / RMS_Sect_WS_count[WD]), 0.5);
                        else
                            RMS_Sect_WS[WD] = 0;
                        
                    }

                    models[radiusIndex].RMS_WS_Est = RMS_WS_Est;
                    models[radiusIndex].RMS_Sect_WS_Est = RMS_Sect_WS;

                    RMS_WS_Est = 0;
                    RMS_WS_Est_count = 0;

                    RMS_Sect_WS = new double[numWD];
                    RMS_Sect_WS_count = new double[numWD];
                }
            }
        }

        /// <summary> Finds and returns the index of model in metPair's WS_Estimates. </summary>
        public int GetWS_PredInd(Pair_Of_Mets metPair, Model[] model)
        {           
            int WS_PredInd = -1;
            
            for (int i = 0; i < metPair.WS_PredCount; i++)
            {
                bool isSame = IsSameModel(model[0], metPair.WS_Pred[i, 0].model); // just comparing with first radii to find WS_PredInd
                if (isSame == true)
                {
                    WS_PredInd = i;
                    break;
                }
            }

            return WS_PredInd;
        }

        /// <summary> Calculates and returns the coefficients and change in wind speed due to changes in downwind exposure. </summary>        
        public Coeff_Delta_WS[] Get_DeltaWS_DW_Expo(double WS1, double UW1, double UW2, double DW1, double DW2, double P10_UW, double P10_DW, Model thisModel, int WD_Ind, bool useSepModel)
        {            
            Coeff_Delta_WS[] coeffsDelta = null;
            double deltaDH = 0;
            double deltaUH = 0;
            double deltaFS = 0;
            double deltaWS = 0;
            
            double coeff = 0;

            InvestCollection radiiList = new InvestCollection();
            radiiList.New();
            int radiusIndex = 0;
            for (int i = 0; i < radiiList.ThisCount; i++)
                if (radiiList.investItem[i].radius == thisModel.radius)                
                    radiusIndex = i;                    
                
            string flow1 = thisModel.GetFlowType(UW1, DW1, WD_Ind, "DW", null, WS1, useSepModel, radiusIndex);
            string flow2 = thisModel.GetFlowType(UW2, DW2, WD_Ind, "DW", null, WS1, useSepModel, radiusIndex);

            double avgUW = (UW1 + UW2) / 2;
            double avgDW = (DW1 + DW2) / 2;            

       /*     if (useSepModel == true && DW1 > 0 && UW1 > 0 && DW1 + UW1 > thisModel.sepCrit[WD_Ind] && WS1 > thisModel.Sep_crit_WS[WD_Ind])
                flow1 = "Turbulent";
            else if (DW1 > 0)
                flow1 = "Downhill";
            else
                flow1 = "Uphill";

            if (useSepModel = true && DW2 > 0 && UW2 > 0 && DW2 + UW2 > thisModel.sepCrit[WD_Ind] && WS1 > thisModel.Sep_crit_WS[WD_Ind])
                flow2 = "Turbulent";
            else if (DW2 > 0)
                flow2 = "Downhill";
            else
                flow2 = "Uphill";
                */

            if (flow1 == flow2) // 1
            {
                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow1);
                deltaWS = coeff * (DW2 - DW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaWS, flow1, "Expo");
            }
            else if (flow1 == "Downhill" && flow2 == "Uphill") // 2
            {
                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow1);
                deltaDH = coeff * (0 - DW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow1, "Expo");

                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow2);
                deltaUH = coeff * (DW2 - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow2, "Expo");


                deltaWS = deltaDH + deltaUH;
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Uphill" && flow2 == "Downhill") // 3 - this could be combined with Case 2, same math
            {
                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow1);
                deltaUH = coeff * (0 - DW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow1, "Expo");

                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow2);
                deltaDH = coeff * (DW2 - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow2, "Expo");

                deltaWS = deltaUH + deltaDH;
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Downhill" && flow2 == "Turbulent") // 4
            {
                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow1);
                deltaDH = coeff * ((thisModel.sepCrit[WD_Ind] - avgUW) - DW1);
                if (deltaDH < 0) deltaDH = 0; // WS should increase between DW1 and point of separation
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow1, "Expo");

                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow2);
                deltaFS = coeff * (DW2 - (thisModel.sepCrit[WD_Ind] - avgUW));
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaFS, flow2, "Expo");
                if (deltaFS > 0) deltaFS = 0; // WS should decrease between point of separation and turbulent site 2

                deltaWS = deltaDH + deltaFS;
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Turbulent" && flow2 == "Downhill") // 5
            {
                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow1);
                deltaFS = coeff * ((thisModel.sepCrit[WD_Ind] - avgUW) - DW1);
                if (deltaFS < 0) deltaFS = 0; // WS should increase between turbulent site 1 and point of separation 
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaFS, flow1, "Expo");

                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow2);
                deltaDH = coeff * (DW2 - (thisModel.sepCrit[WD_Ind] - avgUW));
                if (deltaDH > 0) deltaDH = 0; // WS should decrease between point of separation and DW2 
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow2, "Expo");

                deltaWS = deltaFS + deltaDH;
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Uphill" && flow2 == "Turbulent") // 6
            {
                // Uphill to flat
                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow1);
                deltaUH = coeff * (0 - DW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow1, "Expo");

                // Flat to POS
                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, "Downhill");
                deltaDH = coeff * ((thisModel.sepCrit[WD_Ind] - avgUW) - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, "Downhill", "Expo");

                // POS to Site 2
                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow2);
                deltaFS = coeff * (DW2 - (thisModel.sepCrit[WD_Ind] - avgUW));
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaFS, flow2, "Expo");
                if (deltaFS > 0) deltaDH = 0;// WS should decrease between POS and DW2 

                deltaWS = deltaUH + deltaDH + deltaFS;
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Turbulent" && flow2 == "Uphill") // 7
            {
                // Turbulent to point of separation
                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow1);
                deltaFS = coeff * ((thisModel.sepCrit[WD_Ind] - avgUW) - DW1);
                if (deltaFS < 0) deltaFS = 0; // WS should incree between turbulent site 1 and point of separation 
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaFS, flow1, "Expo");

                // POS to Flat
                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, "Downhill");
                deltaDH = coeff * (0 - (thisModel.sepCrit[WD_Ind] - avgUW));
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, "Downhill", "Expo");

                // Flat to Uphill
                coeff = thisModel.CalcDW_Coeff(P10_DW, P10_UW, WD_Ind, flow2);
                deltaUH = coeff * (DW2 - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow2, "Expo");

                deltaWS = deltaFS + deltaDH + deltaUH;
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaWS, "Total", "Expo");
            }

            return coeffsDelta;
        }

        /// <summary> Adds coefficients and delta WS to list of coeffsDelta. </summary>
        public void AddCoeffDeltaWS(ref Coeff_Delta_WS[] coeffsDelta, double coeff, double deltaWS, string flowType, string Expo_or_SRDH)
        {            
            int numCoeffDelt = 0;
            if (coeffsDelta != null)
                numCoeffDelt = coeffsDelta.Length;

            Array.Resize(ref coeffsDelta, numCoeffDelt + 1);

            coeffsDelta[numCoeffDelt].coeff = coeff;
            coeffsDelta[numCoeffDelt].deltaWS_Expo = deltaWS;
            coeffsDelta[numCoeffDelt].expoOrRough = Expo_or_SRDH;
            coeffsDelta[numCoeffDelt].flowType = flowType;

        }

        /// <summary> Returns flow separation nodes for specified WD sector if they exist. </summary>
        public NodeCollection.Sep_Nodes GetFlowSepNodes(NodeCollection.Sep_Nodes[] flowSepNodes, int WD_Ind)
        {            
            NodeCollection.Sep_Nodes thisFlowSep = new NodeCollection.Sep_Nodes();
            int numFS = 0;

            if (flowSepNodes != null) numFS = flowSepNodes.Length;
            
            for (int i = 0; i < numFS; i++)
            {
                if (flowSepNodes[i].flowSepWD == WD_Ind)
                {
                    thisFlowSep = flowSepNodes[i];
                    break;
                }
            }

            return thisFlowSep;
        }

        /// <summary> Calculates and returns the coefficients and change in wind speed due to changes in upwind exposure during flow separation. </summary>
        public Coeff_Delta_WS[] GetDeltaWS_UW_Turbulent(NodeCollection.Node_UTMs siteCoords, double siteUWExpo, NodeCollection.Sep_Nodes flowSepNodes, Model thisModel, 
                bool fromSiteToSepNode, int WD_Ind, int radiusIndex, double P10_UW, double P10_DW)
        {
            Coeff_Delta_WS[] coeffsDelta = null;
            TopoInfo topo = new TopoInfo();
            double deltaFS = 0;

            if (fromSiteToSepNode == true) // predicting change in wind speed from site to upwind point of separation
            {
                if (flowSepNodes.turbEndNode == null)
                { // Site 1 is in turb zone so go to Sep Pt 1                                                               
                  // Site 1 to Sep Pt 1
                    double distToHigh = topo.CalcDistanceBetweenPoints(flowSepNodes.highNode.UTMX, flowSepNodes.highNode.UTMY, siteCoords.UTMX, siteCoords.UTMY);
                    deltaFS = -thisModel.GetDeltaWS_TurbulentZone(distToHigh, WD_Ind);
                    AddCoeffDeltaWS(ref coeffsDelta, distToHigh, deltaFS, "Turbulent", "Expo");
                }
                else
                { // Site 1 is outside of turb so go to end of turb zone then go to Sep Pt 1
                  // Site 1 to Turb End 1
                    double coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "Downhill");
                    double deltaDH = coeff * (flowSepNodes.turbEndNode.expo[radiusIndex].expo[WD_Ind] - siteUWExpo);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, "Downhill", "Expo");

                    // Turb End 1 to Sep Pt 1
                    double distToHigh = topo.CalcDistanceBetweenPoints(flowSepNodes.highNode.UTMX, flowSepNodes.highNode.UTMY, flowSepNodes.turbEndNode.UTMX, flowSepNodes.turbEndNode.UTMY);
                    deltaFS = -thisModel.GetDeltaWS_TurbulentZone(distToHigh, WD_Ind);
                    AddCoeffDeltaWS(ref coeffsDelta, distToHigh, deltaFS, "Turbulent", "Expo");
                }
            }
            else // predicting change in wind speed from upwind point of separation to site
            {
                if (flowSepNodes.turbEndNode == null)
                { // Site 2 is in turb zone so go to Site 2                                                                
                  // Sep Pt 2 to Site 2
                    double distToHigh = topo.CalcDistanceBetweenPoints(siteCoords.UTMX, siteCoords.UTMY, flowSepNodes.highNode.UTMX, flowSepNodes.highNode.UTMY);
                    double deltaWS = thisModel.GetDeltaWS_TurbulentZone(distToHigh, WD_Ind);
                    AddCoeffDeltaWS(ref coeffsDelta, distToHigh, deltaWS, "Turbulent", "Expo");                    
                }
                else
                { // Sep Pt 2 to Turb End 2
                    double distToHigh = topo.CalcDistanceBetweenPoints(flowSepNodes.turbEndNode.UTMX, flowSepNodes.turbEndNode.UTMY, flowSepNodes.highNode.UTMX, flowSepNodes.highNode.UTMY);
                    double deltaWS = thisModel.GetDeltaWS_TurbulentZone(distToHigh, WD_Ind);
                    AddCoeffDeltaWS(ref coeffsDelta, distToHigh, deltaWS, "Turbulent", "Expo");
                    
                    // Turb End 2 to Site 2
                    double coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "Downhill");
                    deltaWS = coeff * (siteUWExpo - flowSepNodes.turbEndNode.expo[radiusIndex].expo[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaWS, "Downhill", "Expo");                    
                }
            }

            return coeffsDelta;
        }

        /// <summary> Calculates and returns the coeffs and change in wind speed for each flow type based on change in upwind exposure </summary>        
        public Coeff_Delta_WS[] Get_DeltaWS_UW_Expo(double UW1, double UW2, double DW1, double DW2, double P10_UW, double P10_DW, Model thisModel, int WD_Ind, int radiusIndex, NodeCollection.Sep_Nodes[] sepNodes1,
                                            NodeCollection.Sep_Nodes[] sepNodes2, double WS, bool useFlowSep, NodeCollection.Node_UTMs node1Coords, NodeCollection.Node_UTMs node2Coords)
        
        { 
            
            Coeff_Delta_WS[] coeffsDelta = null;
            double deltaDH = 0;
            double deltaSU = 0;
            double deltaUH = 0;            
            double deltaVL = 0;
            double deltaWS = 0;

            double coeff = 0;

            NodeCollection.Sep_Nodes flowSepNode1 = GetFlowSepNodes(sepNodes1, WD_Ind);
            NodeCollection.Sep_Nodes flowSepNode2 = GetFlowSepNodes(sepNodes2, WD_Ind); ;

            string flow1 = thisModel.GetFlowType(UW1, DW1, WD_Ind, "UW", sepNodes1, WS, useFlowSep, radiusIndex);
            string flow2 = thisModel.GetFlowType(UW2, DW2, WD_Ind, "UW", sepNodes2, WS, useFlowSep, radiusIndex);

            if (flow1 == "Turbulent" && flow2 == "Turbulent")
            { // Scenario 1             
                
                coeffsDelta = GetDeltaWS_UW_Turbulent(node1Coords, UW1, flowSepNode1, thisModel, true, WD_Ind, radiusIndex, P10_UW, P10_DW); // Site 1 to Sep Pt 1
           
                // Sep Pt 1 to Sep Pt 2 // for now assume that flow is uphill
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "Uphill");
                deltaUH = coeff * (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] - flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind]);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow1, "Expo");

                Coeff_Delta_WS[] turbCoeffDeltaWS = GetDeltaWS_UW_Turbulent(node2Coords, UW2, flowSepNode2, thisModel, false, WD_Ind, radiusIndex, P10_UW, P10_DW); // Sep Pt 2 to Site 2
                int coeffsDeltOldLength = coeffsDelta.Length;
                Array.Resize(ref coeffsDelta, coeffsDeltOldLength + turbCoeffDeltaWS.Length);
                Array.Copy(turbCoeffDeltaWS, 0, coeffsDelta, coeffsDeltOldLength, turbCoeffDeltaWS.Length);                              

                for (int i = 0; i < coeffsDelta.Length; i++)
                    deltaWS = deltaWS + coeffsDelta[i].deltaWS_Expo;
               
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == flow2) {  // Scenario 2

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaWS = coeff * (UW2 - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaWS, flow1, "Expo");
            }
            else if (flow1 == "Downhill" && flow2 == "Uphill")
            { // Scenario 3
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaDH = coeff * (0 - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow1, "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                deltaSU = coeff * (thisModel.UW_crit[WD_Ind] - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                deltaUH = coeff * (UW2 - thisModel.UW_crit[WD_Ind]);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow2, "Expo");

                deltaWS = deltaDH + deltaSU + deltaUH;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Downhill" && flow2 == "SpdUp")
            { // Scenario 4
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaDH = coeff * (0 - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow1, "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                deltaSU = coeff * (UW2 - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, flow2, "Expo");

                deltaWS = deltaDH + deltaSU;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Downhill" && flow2 == "Valley")
            { // Scenario 5// delta WS must be< 0. Can't accelerate if going from downhill flow into a valley.
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaVL = coeff * (UW2 - UW1);

                if (deltaVL < 0)
                    deltaWS = deltaVL;
                else
                    deltaWS = 0;

                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaWS, "Valley", "Expo");
            }
            else if (flow1 == "Downhill" && flow2 == "Turbulent")
            { // Scenario 6
                flowSepNode2 = GetFlowSepNodes(sepNodes2, WD_Ind);

                // Site 1 to Sep Pt 2
                if (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] > thisModel.UW_crit[WD_Ind])
                { // Sep Pt 2 is Uphill
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                    deltaDH = coeff * (0 - UW1);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow1, "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                    deltaSU = coeff * (thisModel.UW_crit[WD_Ind] - 0);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "Uphill");
                    deltaUH = coeff * (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] - thisModel.UW_crit[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, "Uphill", "Expo");
                }
                else { // Sep Pt 2 is Speed-up
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                    deltaDH = coeff * (0 - UW1);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow1, "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                    deltaSU = coeff * (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] - 0);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");
                }

                // Sep Pt 2 to Site 2
                Coeff_Delta_WS[] Coeffs_Delta_Turb = GetDeltaWS_UW_Turbulent(node2Coords, UW2, flowSepNode2, thisModel, false, WD_Ind, radiusIndex, P10_UW, P10_DW);
                int coeffsDeltOldLength = coeffsDelta.Length;
                Array.Resize(ref coeffsDelta, coeffsDeltOldLength + Coeffs_Delta_Turb.Length);
                Array.Copy(Coeffs_Delta_Turb, 0, coeffsDelta, coeffsDeltOldLength, Coeffs_Delta_Turb.Length);

                for (int i = 0; i < coeffsDelta.Length; i++)
                    deltaWS = deltaWS + coeffsDelta[i].deltaWS_Expo;

                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Uphill" && flow2 == "Downhill")
            { // Scenario 7
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaUH = coeff * (thisModel.UW_crit[WD_Ind] - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow1, "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                deltaSU = coeff * (0 - thisModel.UW_crit[WD_Ind]);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                deltaDH = coeff * (UW2 - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow2, "Expo");

                deltaWS = deltaUH + deltaSU + deltaDH;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Uphill" && flow2 == "SpdUp")
            { // Scenario 8
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaUH = coeff * (thisModel.UW_crit[WD_Ind] - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow1, "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                deltaSU = coeff * (UW2 - thisModel.UW_crit[WD_Ind]);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, flow2, "Expo");

                deltaWS = deltaUH + deltaSU;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Uphill" && flow2 == "Valley")
            { // Scenario 9// 
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaUH = coeff * (thisModel.UW_crit[WD_Ind] - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow1, "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                deltaSU = coeff * (0 - thisModel.UW_crit[WD_Ind]);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");

                // Flat - Valley
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                deltaVL = coeff * (UW2 - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaVL, flow2, "Expo");

                deltaWS = deltaUH + deltaSU + deltaVL;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Uphill" && flow2 == "Turbulent")
            { // Scenario 10
                flowSepNode2 = GetFlowSepNodes(sepNodes2, WD_Ind);

                // Site 1 to Sep Pt 2
                if (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] > thisModel.UW_crit[WD_Ind])
                { // Sep Pt 2 is Uphill
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                    deltaUH = coeff * (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] - UW1);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow1, "Expo");
                }
                else { // Sep Pt 2 is Speed-up
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                    deltaUH = coeff * (thisModel.UW_crit[WD_Ind] - UW1);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow1, "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                    deltaSU = coeff * (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] - thisModel.UW_crit[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");
                }

                // Sep Pt 2 to Site 2
                Coeff_Delta_WS[] Coeffs_Delta_Turb = GetDeltaWS_UW_Turbulent(node2Coords, UW2, flowSepNode2, thisModel, false, WD_Ind, radiusIndex, P10_UW, P10_DW);
                int coeffsDeltOldLength = coeffsDelta.Length;
                Array.Resize(ref coeffsDelta, coeffsDeltOldLength + Coeffs_Delta_Turb.Length);
                Array.Copy(Coeffs_Delta_Turb, 0, coeffsDelta, coeffsDeltOldLength, Coeffs_Delta_Turb.Length);

                for (int i = 0; i < coeffsDelta.Length; i++)
                    deltaWS = deltaWS + coeffsDelta[i].deltaWS_Expo;
                
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "SpdUp" && flow2 == "Uphill")
            { // Scenario 11
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaSU = coeff * (thisModel.UW_crit[WD_Ind] - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, flow1, "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                deltaUH = coeff * (UW2 - thisModel.UW_crit[WD_Ind]);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow2, "Expo");

                deltaWS = deltaSU + deltaUH;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "SpdUp" && flow2 == "Downhill")
            { // Scenario 12
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaSU = coeff * (0 - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, flow1, "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                deltaDH = coeff * (UW2 - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow2, "Expo");

                deltaWS = deltaSU + deltaDH;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "SpdUp" && flow2 == "Valley")
            { // Scenario 13
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaSU = coeff * (0 - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, flow1, "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                deltaVL = coeff * (UW2 - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaVL, flow2, "Expo");

                deltaWS = deltaSU + deltaVL;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "SpdUp" && flow2 == "Turbulent")
            { // Scenario 14
                flowSepNode2 = GetFlowSepNodes(sepNodes2, WD_Ind);

                // Site 1 to Sep Pt 2
                if (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] < thisModel.UW_crit[WD_Ind])
                { // Sep Pt 2 is Speed-up
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                    deltaSU = coeff * (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] - UW1);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, flow1, "Expo");
                }
                else { // Sep Pt 2 is Uphill
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                    deltaSU = coeff * (thisModel.UW_crit[WD_Ind] - UW1);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, flow1, "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "Uphill");
                    deltaUH = coeff * (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] - thisModel.UW_crit[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, "Uphill", "Expo");
                }

                // Sep Pt 2 to Site 2
                Coeff_Delta_WS[] Coeffs_Delta_Turb = GetDeltaWS_UW_Turbulent(node2Coords, UW2, flowSepNode2, thisModel, false, WD_Ind, radiusIndex, P10_UW, P10_DW);
                int coeffsDeltOldLength = coeffsDelta.Length;
                Array.Resize(ref coeffsDelta, coeffsDeltOldLength + Coeffs_Delta_Turb.Length);
                Array.Copy(Coeffs_Delta_Turb, 0, coeffsDelta, coeffsDeltOldLength, Coeffs_Delta_Turb.Length);

                for (int i = 0; i < coeffsDelta.Length; i++)
                    deltaWS = deltaWS + coeffsDelta[i].deltaWS_Expo;
                                
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Valley" && flow2 == "Uphill")
            { // Scenario 15                                                               
                // Valley - Flat 
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaVL = coeff * (0 - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaVL, flow1, "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                deltaSU = coeff * (thisModel.UW_crit[WD_Ind] - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                deltaUH = coeff * (UW2 - thisModel.UW_crit[WD_Ind]);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow2, "Expo");

                deltaWS = deltaUH + deltaSU + deltaVL;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Valley" && flow2 == "Downhill")
            { // Scenario 16
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                deltaDH = coeff * (UW2 - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow1, "Expo");

                deltaWS = deltaDH;
            }
            else if (flow1 == "Valley" && flow2 == "SpdUp")
            { // Scenario 17                                                              
                // Valley - Flat 
                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                deltaVL = coeff * (0 - UW1);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaVL, flow1, "Expo");

                coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                deltaSU = coeff * (UW2 - 0);
                AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, flow2, "Expo");

                deltaWS = deltaSU + deltaVL;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Valley" && flow2 == "Turbulent")
            { // Scenario 18
                flowSepNode2 = GetFlowSepNodes(sepNodes2, WD_Ind);
                // Site 1 to Sep Pt 2
                if (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] > thisModel.UW_crit[WD_Ind])
                { // Sep Pt 2 is Uphill
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                    deltaVL = coeff * (0 - UW1);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaVL, flow1, "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                    deltaSU = coeff * (thisModel.UW_crit[WD_Ind] - 0);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "Uphill");
                    deltaUH = coeff * (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] - thisModel.UW_crit[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, "Uphill", "Expo");
                }
                else { // Sep Pt 2 is Speed-up
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow1);
                    deltaVL = coeff * (0 - UW1);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaVL, flow1, "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                    deltaSU = coeff * (flowSepNode2.highNode.expo[radiusIndex].expo[WD_Ind] - 0);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");
                }

                // Sep Pt 2 to Site 2
                Coeff_Delta_WS[] Coeffs_Delta_Turb = GetDeltaWS_UW_Turbulent(node2Coords, UW2, flowSepNode2, thisModel, false, WD_Ind, radiusIndex, P10_UW, P10_DW);
                int coeffsDeltOldLength = coeffsDelta.Length;
                Array.Resize(ref coeffsDelta, coeffsDeltOldLength + Coeffs_Delta_Turb.Length);
                Array.Copy(Coeffs_Delta_Turb, 0, coeffsDelta, coeffsDeltOldLength, Coeffs_Delta_Turb.Length);

                for (int i = 0; i < coeffsDelta.Length; i++)
                    deltaWS = deltaWS + coeffsDelta[i].deltaWS_Expo;
                
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Turbulent" && flow2 == "Uphill")
            { // Scenario 19
                flowSepNode1 = GetFlowSepNodes(sepNodes1, WD_Ind);
                // Site 1 to Sep Pt 1
                coeffsDelta = GetDeltaWS_UW_Turbulent(node1Coords, UW1, flowSepNode1, thisModel, true, WD_Ind, radiusIndex, P10_UW, P10_DW);                            

                if (flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind] > thisModel.UW_crit[WD_Ind]) // Sep Pt 1 to Site 2
                { // Sep Pt 1 is uphill

                    // Sep Pt 1 to Site 2
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                    deltaUH = coeff * (UW2 - flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow2, "Expo");
                }
                else { // Sep Pt 1 is speed-up
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                    deltaSU = coeff * (thisModel.UW_crit[WD_Ind] - flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);

                    deltaUH = coeff * (UW2 - thisModel.UW_crit[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, flow2, "Expo");

                }

                for (int i = 0; i < coeffsDelta.Length; i++)
                    deltaWS = deltaWS + coeffsDelta[i].deltaWS_Expo;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Turbulent" && flow2 == "Downhill")
            { // Scenario 20
                flowSepNode1 = GetFlowSepNodes(sepNodes1, WD_Ind);

                // Site 1 to Sep Pt 1
                coeffsDelta = GetDeltaWS_UW_Turbulent(node1Coords, UW1, flowSepNode1, thisModel, true, WD_Ind, radiusIndex, P10_UW, P10_DW);                               

                if (flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind] > thisModel.UW_crit[WD_Ind])
                { // Sep Pt 1 is uphill

                    // Sep Pt 1 to Site 2
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "Uphill");
                    deltaUH = coeff * (thisModel.UW_crit[WD_Ind] - flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, "Uphill", "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                    deltaSU = coeff * (0 - thisModel.UW_crit[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                    deltaDH = deltaDH + coeff * (UW2 - 0);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow2, "Expo");
                }
                else
                { // Sep Pt 1 is speed-up
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                    deltaSU = coeff * (0 - flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                    deltaDH = coeff * (UW2 - 0);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaDH, flow2, "Expo");                    
                }

                for (int i = 0; i < coeffsDelta.Length; i++)
                    deltaWS = deltaWS + coeffsDelta[i].deltaWS_Expo;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Turbulent" && flow2 == "SpdUp")
            { // Scenario 21
                flowSepNode1 = GetFlowSepNodes(sepNodes1, WD_Ind);

                // Site 1 to Sep Pt 1
                coeffsDelta = GetDeltaWS_UW_Turbulent(node1Coords, UW1, flowSepNode1, thisModel, true, WD_Ind, radiusIndex, P10_UW, P10_DW);                               

                if (flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind] < thisModel.UW_crit[WD_Ind])
                { // Sep Pt 1 is speed-up, Sep Pt 1 to Site 2
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                    deltaSU = coeff * (UW2 - flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, flow2, "Expo");
                }
                else
                { // Sep Pt 1 is uphill
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "Uphill");
                    deltaUH = coeff * (thisModel.UW_crit[WD_Ind] - flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, "Uphill", "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                    deltaSU = coeff * (UW2 - thisModel.UW_crit[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, flow2, "Expo");
                }

                for (int i = 0; i < coeffsDelta.Length; i++)
                    deltaWS = deltaWS + coeffsDelta[i].deltaWS_Expo;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");
            }
            else if (flow1 == "Turbulent" && flow2 == "Valley")
            { // Scenario 22
                flowSepNode1 = GetFlowSepNodes(sepNodes1, WD_Ind);
                // Site 1 to Sep Pt 1
                coeffsDelta = GetDeltaWS_UW_Turbulent(node1Coords, UW1, flowSepNode1, thisModel, true, WD_Ind, radiusIndex, P10_UW, P10_DW);                              

                // Sep Pt 1 to Site 2 
                if (flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind] > thisModel.UW_crit[WD_Ind])
                { // Sep Pt 2 is Uphill
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "Uphill");
                    deltaUH = coeff * (thisModel.UW_crit[WD_Ind] - flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaUH, "Uphill", "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                    deltaSU = coeff * (0 - thisModel.UW_crit[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                    deltaVL = coeff * (UW2 - 0);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaVL, flow2, "Expo");
                }
                else { // Sep Pt 2 is Speed-up
                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, "SpdUp");
                    deltaSU = coeff * (0 - flowSepNode1.highNode.expo[radiusIndex].expo[WD_Ind]);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaSU, "SpdUp", "Expo");

                    coeff = thisModel.CalcUW_Coeff(P10_UW, P10_DW, WD_Ind, flow2);
                    deltaVL = coeff * (UW2 - 0);
                    AddCoeffDeltaWS(ref coeffsDelta, coeff, deltaVL, flow2, "Expo");
                }

                for (int i = 0; i < coeffsDelta.Length; i++)
                    deltaWS = deltaWS + coeffsDelta[i].deltaWS_Expo;
                AddCoeffDeltaWS(ref coeffsDelta, 0, deltaWS, "Total", "Expo");

            }
            
            return coeffsDelta;
        }

        /// <summary> Calculates and returns the change in WS based on change in surface roughness and displacement height. </summary>       
        public double GetDeltaWS_SRDH(double WS1, double HH, double SR1, double SR2, double DH1, double DH2, double Stab_1, double Stab_2)
        {             
            double deltaWS = 0;

            if (SR1 > 0 && SR2 > 0) 
                deltaWS = WS1 * ((Math.Log((HH - DH2) / SR2) + Stab_2) / (Math.Log((HH - DH1) / SR1) + Stab_1)) - WS1;

            return deltaWS;

        }

        /// <summary> Calculates and returns the equivalent sectorwise wind speeds at the target site based on the sectorwise wind speeds and wind rose at the predictor site. </summary>        
        public double[] GetWS_Equiv(double[] WR_Pred, double[] WR_Targ, double[] WS_Pred)
        {
            // Flow Rotation Model 4/20/16
            // Calculates the weighted average WS of predictor met using the target and predictor wind roses
            // Also calculates the change in the wind rose for each sector
            // Adj. factors are defined, one for sectors with positive WR change and one for sectors with negative delta WR with a default value of 0.01 
            // WS_Equiv is calculated for each sector = WS_Pred * Adj. factors * deltaWR
            // Wgt average wind speed is calculated with WS_Equiv. if Avg WS < Avg WS Pred then posAdj is increed using 0.02 increments until Avg WS = Avg WS Pred
            // Vice-versa if Avg WS > Avg WS Pred then Neg_adj incree
                        
            double posAdj = 0;
            double negAdj = 0;

            double avgWS_Pred = 0;
            double avgWS_Equiv = 0;
            
            int numWD = WR_Pred.Length;
            double[] WS_Equiv = new double[numWD];
            double[] deltaWR = new double[numWD];

            for (int i = 0; i < numWD; i++)
            {
                deltaWR[i] = WR_Targ[i] - WR_Pred[i];

                if (deltaWR[i] >= 0)
                    WS_Equiv[i] = WS_Pred[i] + deltaWR[i] * posAdj;
                else
                    WS_Equiv[i] = WS_Pred[i] + deltaWR[i] * negAdj;

                avgWS_Pred = avgWS_Pred + WS_Pred[i] * WR_Pred[i];
                avgWS_Equiv = avgWS_Equiv + WS_Equiv[i] * WR_Targ[i];
            }

            double WS_Diff = avgWS_Equiv - avgWS_Pred;
            double thisMin = 1000;

            while (Math.Abs(WS_Diff) > 0.01 && thisMin > 0.05)
            {
                if (WS_Diff > 0)
                {
                    negAdj = negAdj + Math.Abs(WS_Diff) * 3f;
                    posAdj = posAdj - Math.Abs(WS_Diff) * 1.5f;
                }
                else {
                    negAdj = negAdj - Math.Abs(WS_Diff) * 1.5f;
                    posAdj = posAdj + Math.Abs(WS_Diff) * 3f;
                }

                avgWS_Equiv = 0;

                for (int i = 0; i < numWD; i++)
                {
                    if (deltaWR[i] >= 0)
                        WS_Equiv[i] = WS_Pred[i] + deltaWR[i] * posAdj;
                    else
                        WS_Equiv[i] = WS_Pred[i] + deltaWR[i] * negAdj;

                    if (WS_Equiv[i] < 0) WS_Equiv[i] = 0.05f;
                    avgWS_Equiv = avgWS_Equiv + WS_Equiv[i] * WR_Targ[i];

                    if (WS_Equiv[i] < thisMin) thisMin = WS_Equiv[i];

                }

                WS_Diff = avgWS_Equiv - avgWS_Pred;
            }

            // return WS_Pred // NO FLOW ROTATION
            return WS_Equiv;

        }

        /// <summary> Finds and returns all models that use specfied met sites. </summary>
        public Model[] GetAllModels(Continuum thisInst, string[] metsUsed)
        {             
            Model[] models = new Model[0];

            int numTOD = thisInst.metList.numTOD;
            int numSeason = thisInst.metList.numSeason;
            Met.TOD thisTOD = new Met.TOD();
            Met.Season thisSeason = new Met.Season();
            int thisInd = 0;                       

            for (int tod = 0; tod < numTOD; tod++)
                for (int seas = 0; seas < numSeason; seas++)
                {
                    if (numTOD == 1)
                        thisTOD = Met.TOD.All;
                    else if (tod == 0)
                        thisTOD = Met.TOD.Day;
                    else
                        thisTOD = Met.TOD.Night;

                    if (numSeason == 1)
                        thisSeason = Met.Season.All;
                    else if (seas == 0)
                        thisSeason = Met.Season.Winter;
                    else if (seas == 1)
                        thisSeason = Met.Season.Spring;
                    else if (seas == 2)
                        thisSeason = Met.Season.Summer;
                    else if (seas == 3)
                        thisSeason = Met.Season.Fall;
                                        
                    Model[] theseModels = thisInst.modelList.GetModels(thisInst, metsUsed, thisTOD, thisSeason, thisInst.modeledHeight, true);
                    Array.Resize(ref models, models.Length + theseModels.Length);
                    for (int i = 0; i < theseModels.Length; i++)
                        models[thisInd + i] = theseModels[i];

                    thisInd = thisInd + theseModels.Length;

                }

            return models;
        }                

        
        /// <summary> Returns all models used in time series analysis. </summary>        
        public Models[] GetAllModelsUsed(Continuum thisInst)
        {
            Models[] models = new Models[1]; // Models struct holds array of Model called modelsByRad (one for each radius)
            int numModels = 1; // there will be at least one model (i.e. if default model is used)
            int numTODs = thisInst.metList.numTOD;
            int numSeasons = thisInst.metList.numSeason;
            string[] metsUsed = thisInst.metList.GetMetsUsed();
            
            if (thisInst.metList.ThisCount > 1)
            {
                numModels = numTODs * numSeasons;
                models = new Models[numModels];
            }

            // Gets models for all seasons/TOD if not using calibrated model otherwise gets models for each season/TOD
            if (thisInst.metList.ThisCount == 1)
                models[0].modelsByRad = thisInst.modelList.GetModels(thisInst, metsUsed, Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            else
            {
                int ind = 0;
                for (int t = 0; t < numTODs; t++)
                    for (int s = 0; s < numSeasons; s++)
                    {
                        Met.TOD thisTOD;
                        if (numTODs == 1)
                            thisTOD = Met.TOD.All;
                        else
                            thisTOD = (Met.TOD)t;

                        Met.Season thisSeason;
                        if (numSeasons == 1)
                            thisSeason = Met.Season.All;
                        else
                            thisSeason = (Met.Season)s;

                        models[ind].modelsByRad = thisInst.modelList.GetModels(thisInst, metsUsed, thisTOD, thisSeason, thisInst.modeledHeight, false);

                        ind++;
                    }
            }

            return models;
        }

        /// <summary> Generates a time series of either gross/net wind speed/energy. Uses seasonal and TOD models and LT estimates at each met site to estimate wind speed. These are combined using WS weights. Returns time series array.
        /// </summary>

        public TimeSeries[] GenerateTimeSeries(Continuum thisInst, string[] metsUsed, Nodes targetNode, TurbineCollection.PowerCurve powerCurve,
            Wake_Model wakeModel, WakeCollection.WakeLossCoeffs[] wakeCoeffs, string MCP_Method = "")
        {
            
            TimeSeries[] thisTS = new TimeSeries[0];
            int numMets = metsUsed.Length;
            MetLTEsts[] metLT_Ests = new MetLTEsts[numMets];
            int TS_Length = 0;

            if (metsUsed == null)
                return thisTS;

            if (numMets == 0)
                return thisTS;

            if (thisInst.topo.elevsForCalcs == null)
                thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            if (MCP_Method != null && MCP_Method != "")
            {
                // Size time series array to same length at met's long-term estimates
                Reference thisRef = thisInst.metList.GetReferenceUsedInMCP(metsUsed);

                if (thisRef.interpData.TS_Data.Length == 0)
                {
                    thisRef.GetReferenceDataFromDB(thisInst);
                    thisRef.GetInterpData(thisInst.UTM_conversions);
                }

                TS_Length = thisRef.interpData.TS_Data.Length;
                Array.Resize(ref thisTS, TS_Length);

                // Get LT Estimates at each met used in model. Save in MetLTEsts struct which holds an array of MCP.Site_Data struct           

                for (int i = 0; i < numMets; i++)
                {
                    for (int j = 0; j < thisInst.metList.ThisCount; j++)
                        if (metsUsed[i] == thisInst.metList.metItem[j].name)
                        {
                            if (thisInst.metList.metItem[j].mcpList[0].LT_WS_Ests.Length == 0) // mcpList[0] is MCP at modeled height
                                thisInst.metList.metItem[j].mcpList[0].LT_WS_Ests = thisInst.metList.metItem[j].mcpList[0].GenerateLT_WS_TS(thisInst, thisInst.metList.metItem[j], MCP_Method);

                            metLT_Ests[i].estWS = thisInst.metList.metItem[j].mcpList[0].LT_WS_Ests;
                        }
                }
            }
            else
            {
                // Using data period of all mets used in model (i.e. minimum allStartDate and maximum allEndDate of all mets)                
                for (int i = 0; i < numMets; i++)
                {
                    for (int j = 0; j < thisInst.metList.ThisCount; j++)
                        if (metsUsed[i] == thisInst.metList.metItem[j].name) 
                            metLT_Ests[i].estWS = thisInst.metList.GetMetDataTSOverEntireRange(metsUsed[i], thisInst.modeledHeight); 
                }
                                
                if (numMets > 0)
                    TS_Length = metLT_Ests[0].estWS.Length;                              

                Array.Resize(ref thisTS, TS_Length);
            }

            // Get all models used. 
         //   int numTODs = thisInst.metList.metItem[0].mcp.numTODs;
         //   int numSeasons = thisInst.metList.metItem[0].mcp.numSeasons;

            // Get all models used
            Models[] models = GetAllModelsUsed(thisInst);

            if (models == null) return thisTS;
            int numModels = models.Length;

            // Get WS Weights for each model
            ModelWeights[] weights = new ModelCollection.ModelWeights[0]; // ModelWeights struct holds predictor met, model, and weight
            NodeCollection nodeList = new NodeCollection();
            
            int numRad = thisInst.radiiList.ThisCount;

            for (int i = 0; i < numModels; i++)
            {
                ModelCollection.ModelWeights[] theseWeights = thisInst.modelList.GetWS_EstWeights(thisInst.metList.GetMets(metsUsed, null), targetNode,
                    models[i].modelsByRad, thisInst.metList.GetAvgWindRoseMetsUsed(metsUsed, models[i].modelsByRad[0].timeOfDay, models[0].modelsByRad[0].season, thisInst.modeledHeight), thisInst.radiiList);

                int oldSize = weights.Length;
                int newSize = weights.Length + theseWeights.Length;

                Array.Resize(ref weights, newSize);

                for (int j = 0; j < theseWeights.Length; j++)
                    weights[oldSize + j] = theseWeights[j];

            }

            // Get Paths of Nodes between turbine and each met for each model
            int numPaths = numMets * numModels * numRad;
            PathsOfNodes[] pathsOfNodes = new PathsOfNodes[numPaths];
            int pathInd = 0;

            for (int metInd = 0; metInd < numMets; metInd++)
                for (int modelInd = 0; modelInd < numModels; modelInd++)
                    for (int radInd = 0; radInd < numRad; radInd++)
                    {
                        pathsOfNodes[pathInd].met = thisInst.metList.GetMet(metsUsed[metInd]);
                        pathsOfNodes[pathInd].model = models[modelInd].modelsByRad[radInd];
                        Nodes metNode = nodeList.GetMetNode(pathsOfNodes[pathInd].met);
                        pathsOfNodes[pathInd].path = nodeList.FindPathOfNodes(metNode, targetNode, pathsOfNodes[pathInd].model, thisInst);
                        pathInd++;
                    }

            // Figure out order of mets by distance (for wind direction)
            int[] metOrderByDist = new int[numMets];
            
       //     int closestMetInd = 0;
            double[] distsToTarget = new double[numMets];
           
            int numWD = thisInst.metList.numWD;

            for (int m = 0; m < numMets; m++)
            {
                Met thisMet = thisInst.metList.GetMet(metsUsed[m]);
                distsToTarget[m] = thisInst.topo.CalcDistanceBetweenPoints(targetNode.UTMX, targetNode.UTMY, thisMet.UTMX, thisMet.UTMY);                
            }

            Array.Sort(distsToTarget);
            for (int m = 0; m < numMets; m++)
            {
                Met thisMet = thisInst.metList.GetMet(metsUsed[m]);

                for (int n = 0; n < numMets; n++)
                {
                    if (distsToTarget[n] == thisInst.topo.CalcDistanceBetweenPoints(targetNode.UTMX, targetNode.UTMY, thisMet.UTMX, thisMet.UTMY))
                        metOrderByDist[n] = m;
                }
            }

            // Figure out time series interval
            double timeInt = 1; // number of hours per time interval

            if (metLT_Ests[0].estWS.Length < 2)
                return thisTS;

            TimeSpan timeSpan = metLT_Ests[0].estWS[1].thisDate - metLT_Ests[0].estWS[0].thisDate;
            timeInt = timeSpan.TotalHours;
            Met_Data_Filter metDataFilter = new Met_Data_Filter();

            // For each time interval and for each predicting met and each model (radius), get path of nodes and do wind speed estimate along nodes
            // Then combine wind speed estimates using weights to form average wind speed

            List<int> integerList = Enumerable.Range(0, TS_Length).ToList();
            Parallel.ForEach(integerList, new ParallelOptions { MaxDegreeOfParallelism = 4 }, i =>
            {

       //     for (int i = 0; i < TS_Length; i++)
        //    {               
                DateTime dateTime = metLT_Ests[0].estWS[i].thisDate;
                Met.TOD thisTOD = thisInst.metList.GetTOD(dateTime);
                Met.Season season = thisInst.metList.GetSeason(dateTime);
                double thisAvg = 0;
                double sumWeights = 0;
                int closestMetInd = metOrderByDist[0];
                double thisWD = metLT_Ests[closestMetInd].estWS[i].thisWD;

                if (thisWD == 0 || thisWD == -999) // Check other mets for a valid WD
                {
                    for (int m = 0; m < numMets; m++)
                        if (metLT_Ests[metOrderByDist[m]].estWS[i].thisWD != 0 && metLT_Ests[metOrderByDist[m]].estWS[i].thisWD != -999)
                        {
                            closestMetInd = metOrderByDist[m];
                            break;
                        }

                    thisWD = metLT_Ests[closestMetInd].estWS[i].thisWD;

                    if (thisWD == 0 || thisWD == -999) // No valid WD at any sites so set closestMetInd back to zero
                    {
                        closestMetInd = metOrderByDist[0];
                        thisWD = metLT_Ests[metOrderByDist[closestMetInd]].estWS[i].thisWD;
                    }
                }

                if (thisWD == -999 || thisWD == 0)
                    thisWD = GetLastOrNextWDForTimeSeriesModeling(metLT_Ests[closestMetInd].estWS, i);

                int thisWD_Ind = metDataFilter.GetWD_Ind(thisWD, numWD);

                // Find average wind speed estimate using all mets with a valid WS
                for (int m = 0; m < numMets; m++)
                {
                    Met thisMet = thisInst.metList.GetMet(metsUsed[m]);

                    if (metLT_Ests[m].estWS[i].thisWS == 0 && metLT_Ests[m].estWS[i].thisWD == 0)
                        continue;

                    if (metLT_Ests[m].estWS[i].thisWS == -999)
                        continue;

                    for (int r = 0; r < numRad; r++)
                    {
                        Model thisModel = GetSeasonalModels(models, thisTOD, season, r);
                        Nodes[] thisPath = GetPathNodesForMetAndModel(pathsOfNodes, thisMet, thisModel, thisInst);
                        double thisEst = thisInst.modelList.DoWS_EstimateOneWDTimeSeries(metLT_Ests[m].estWS[i].thisWS, thisWD_Ind, thisMet, targetNode, thisPath, thisModel, thisInst);
                        double thisWeight = GetWeightForMetAndModel(weights, thisMet, thisModel);
                        thisAvg = thisAvg + thisWeight * thisEst;
                        sumWeights = sumWeights + thisWeight;
                    }
                }

                if (sumWeights > 0)
                    thisAvg = thisAvg / sumWeights;
                else
                    thisAvg = -999;

                thisTS[i].dateTime = metLT_Ests[closestMetInd].estWS[i].thisDate;
                thisTS[i].WD = thisWD;
                thisTS[i].freeStreamWS = thisAvg;

                if (powerCurve.name != null && thisAvg != -999)
                    thisTS[i].grossEnergy = thisInst.turbineList.GetInterpPowerOrThrust(thisTS[i].freeStreamWS, powerCurve, "Power") * timeInt;
                else
                    thisTS[i].grossEnergy = -999;

                if (wakeModel != null)
                {
                    if (wakeModel.comboMethod != null)
                    {
                        if (thisAvg != -999)
                        {
                            double[] wakedValues = thisInst.wakeModelList.CalcNetEnergyTimeSeries(wakeCoeffs, targetNode.UTMX, targetNode.UTMY, thisAvg, thisInst, wakeModel, thisTS[i].WD,
                                thisTS[i].grossEnergy, timeInt);

                            thisTS[i].wakedWS = wakedValues[0];
                            thisTS[i].netEnergy = wakedValues[1] * thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
                        }
                        else
                        {
                            thisTS[i].wakedWS = -999;
                            thisTS[i].netEnergy = -999;
                        }
                    }
                }

            });
          //  }

            return thisTS;
        }

        /// <summary> Find last (or next) timestamp with a valid WD and use that </summary>        
        public double GetLastOrNextWDForTimeSeriesModeling(MCP.Site_data[] estData, int i)
        {
            double thisWD = 0;   
            int numStepsToLastValid = 0;
            int lastValidInd = i;
            int numStepsToNextValid = 0;
            int nextValidInd = i;

            while (lastValidInd > 0 && (estData[lastValidInd].thisWD == -999 || estData[lastValidInd].thisWD == 0))
                lastValidInd--;

            if (estData[lastValidInd].thisWD != -999 && estData[lastValidInd].thisWD != 0)
                numStepsToLastValid = i - lastValidInd;

            while (nextValidInd < estData.Length - 1 && (estData[nextValidInd].thisWD == -999 || estData[nextValidInd].thisWD == 0))
                nextValidInd++;

            if (estData[nextValidInd].thisWD != -999 && estData[nextValidInd].thisWD != 0)
                numStepsToNextValid = i - nextValidInd;

            if (numStepsToLastValid != 0 && numStepsToNextValid != 0) // Found last and next valid WD so use WD closest to this timestamp
            {
                if (Math.Abs(numStepsToNextValid) < Math.Abs(numStepsToLastValid))
                    thisWD = estData[nextValidInd].thisWD;
                else
                    thisWD = estData[lastValidInd].thisWD;
            }
            else if (numStepsToNextValid != 0)
                thisWD = estData[nextValidInd].thisWD;
            else if (numStepsToLastValid != 0)
                thisWD = estData[lastValidInd].thisWD;

            return thisWD;
        }

        /// <summary> Finds and returns model with specified settings </summary>
        public Model GetSeasonalModels(Models[] models, Met.TOD thisTOD, Met.Season thisSeason, int radInd)
        {           

            for (int i = 0; i < models.Length; i++)
                if (models[i].modelsByRad[0].metsUsed.Length == 1 || (models[i].modelsByRad[0].timeOfDay == thisTOD && models[i].modelsByRad[0].season == thisSeason))
                    return models[i].modelsByRad[radInd];

            return new Model();
        }

        /// <summary> Finds and returns path of nodes between for specified met and model. </summary>        
        public Nodes[] GetPathNodesForMetAndModel(PathsOfNodes[] thesePaths, Met thisMet, Model thisModel, Continuum thisInst)
        {         
            if (thesePaths == null)
                return new Nodes[0];

            for (int i = 0; i < thesePaths.Length; i++)
                if (thesePaths[i].met.name == thisMet.name && thisInst.modelList.IsSameModel(thisModel, thesePaths[i].model))                
                    return thesePaths[i].path;                                   

            return new Nodes[0];
        }

        /// <summary> Calculates and returns WSWD distribution formed from TimeSeries data using either free-stream or waked wind speed. </summary>        
        public Met.WSWD_Dist CalcWSWD_Dist(TimeSeries[] thisTS, Continuum thisInst, string wakedOrFreestream)
        {
            // Flags are: Waked and Freestream
            //  Doesn't filter for season or TOD, just generates dist based on all TimeSeries data
            Met.WSWD_Dist thisDist = new Met.WSWD_Dist();
            thisDist.season = Met.Season.All;
            thisDist.timeOfDay = Met.TOD.All;
            thisDist.height = thisInst.modeledHeight;
            thisDist.WS_Dist = new double[thisInst.metList.numWS]; // All distributions add up to 1.0
            thisDist.windRose = new double[thisInst.metList.numWD];
            thisDist.sectorWS_Ratio = new double[thisInst.metList.numWD];
            thisDist.sectorWS_Dist = new double[thisInst.metList.numWD, thisInst.metList.numWS];

            MCP mcp = new MCP(); // For Get_WS_ind and Get_WD_ind functions
            mcp.New_MCP(true, true, thisInst);

            if (thisInst.metList.ThisCount == 0) return thisDist;
            double WS_FirstInt = thisInst.metList.WS_FirstInt; // for creating distributions
            double WS_IntSize = thisInst.metList.WS_IntSize;

            if (thisTS == null)
                return thisDist;

            for (int i = 0; i < thisTS.Length; i++)
            {
                double thisWS = 0;

                if (thisTS[i].WD == -999 || thisTS[i].freeStreamWS == -999)
                    continue;

                if (wakedOrFreestream == "Freestream")
                    thisWS = thisTS[i].freeStreamWS;
                else if (wakedOrFreestream == "Waked")
                    thisWS = thisTS[i].wakedWS;
                else
                {
                    MessageBox.Show("Invalid flag in CalcWSWD_Dist");
                    return thisDist;
                }

                int WS_ind = mcp.Get_WS_ind(thisWS, 1);
                int WD_ind = mcp.Get_WD_ind(thisTS[i].WD);

                if (WS_ind >= thisInst.metList.numWS) WS_ind = thisInst.metList.numWS - 1;

                thisDist.windRose[WD_ind]++;
                thisDist.WS_Dist[WS_ind]++;
                thisDist.sectorWS_Dist[WD_ind, WS_ind]++;
            }

            if (thisTS.Length > 0)
                for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                    thisDist.WS_Dist[WS_ind] = thisDist.WS_Dist[WS_ind] / thisTS.Length;

            // Calculate wind speed overall and sectorwise distributions
            double sumWD = 0;
            for (int i = 0; i < thisInst.metList.numWD; i++)
                sumWD = sumWD + thisDist.windRose[i];

            for (int i = 0; i < thisInst.metList.numWD; i++)
                thisDist.windRose[i] = thisDist.windRose[i] / sumWD;

            for (int WD_ind = 0; WD_ind < thisInst.metList.numWD; WD_ind++)
            {
                double sumWS = 0;
                for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                    sumWS = sumWS + thisDist.sectorWS_Dist[WD_ind, WS_ind];

                for (int WS_ind = 0; WS_ind < thisInst.metList.numWS; WS_ind++)
                    thisDist.sectorWS_Dist[WD_ind, WS_ind] = thisDist.sectorWS_Dist[WD_ind, WS_ind] / sumWS;
            }

            double sumDist = 0;
            double[] sumSectorDist = new double[thisInst.metList.numWD];

            // Calculate sectorwise and overall wind speeds
            for (int i = 0; i < thisInst.metList.numWS; i++)
            {
                thisDist.WS = thisDist.WS + thisDist.WS_Dist[i] * (WS_FirstInt + i * WS_IntSize - WS_IntSize / 2);
                sumDist = sumDist + thisDist.WS_Dist[i];

                for (int j = 0; j < thisInst.metList.numWD; j++)
                {
                    thisDist.sectorWS_Ratio[j] = thisDist.sectorWS_Ratio[j] + thisDist.sectorWS_Dist[j, i] * (WS_FirstInt + i * WS_IntSize - WS_IntSize / 2);
                    sumSectorDist[j] = sumSectorDist[j] + thisDist.sectorWS_Dist[j, i];
                }
            }

            if (sumDist > 0)
                thisDist.WS = thisDist.WS / sumDist;

            for (int i = 0; i < thisInst.metList.numWD; i++)
            {
                if (sumSectorDist[i] > 0)
                    thisDist.sectorWS_Ratio[i] = thisDist.sectorWS_Ratio[i] / sumSectorDist[i] / thisDist.WS;
            }

            return thisDist;
        }

        /// <summary> Reads in a time series dataset and calculates long-term (LT) Gross AEP, LT CF, sectorwise energy, and monthly values and saves to referenced gross estimates. </summary>        
        public void CalcGrossAEP_AndMonthlyEnergy(ref Turbine.Gross_Energy_Est grossEst, ModelCollection.TimeSeries[] thisTS, Continuum thisInst)
        {     

            if (thisTS == null) return;
            if (thisTS.Length < 2) return;

            int TS_Length = thisTS.Length;

            // Figure out time interval (in hours)
            TimeSpan timeSpan = thisTS[1].dateTime - thisTS[0].dateTime;
            double timeInt = timeSpan.TotalMinutes / 60.0;

            // Start on first hour of month
            int TS_Ind = 0;
            int thisDay = thisTS[TS_Ind].dateTime.Day;
            int thisHour = thisTS[TS_Ind].dateTime.Hour;
                        
            MCP mcp = new MCP(); // Created to use GetWD_Ind function
            mcp.numWD = thisInst.metList.numWD;

            // Go to first day of month
            bool atFirstDay = false;

            if (thisDay == 1 && thisHour == 0)
                atFirstDay = true;

            while (atFirstDay == false && TS_Ind < thisTS.Length - 1)
            {
                TS_Ind++;
                thisDay = thisTS[TS_Ind].dateTime.Day;
                thisHour = thisTS[TS_Ind].dateTime.Hour;
                
                if (thisDay == 1 && thisHour == 0)
                    atFirstDay = true;
            }
                                                
            double LT_AEP = 0;
            int yearCount = 0;
            double thisAEP = 0;
            double[] LT_SectorEnergy = new double[thisInst.metList.numWD];
            double[] thisSectorEnergy = new double[thisInst.metList.numWD];
            grossEst.sectorEnergy = new double[thisInst.metList.numWD];

            if (atFirstDay == false)
                return;

            int lastMonth = thisTS[TS_Ind].dateTime.Month;
            int lastYear = thisTS[TS_Ind].dateTime.Year;

            double monthEnergy = 0;
            int monthInd = 0;
                                    
            // Calculates gross energy production for every year which is used to find the LT average gross AEP
            // Also calculates the monthly energy production for each month in the time series
            // And calculates the average LT sectorwise energy production
            for (int t = TS_Ind; t < TS_Length; t++)
            {
                int month = thisTS[t].dateTime.Month;
                int year = thisTS[t].dateTime.Year;

                int WD_ind = mcp.Get_WD_ind(thisTS[t].WD);

                if (year == lastYear)
                {
                    if (thisTS[t].grossEnergy != -999)
                    {
                        thisAEP = thisAEP + thisTS[t].grossEnergy;
                        thisSectorEnergy[WD_ind] = thisSectorEnergy[WD_ind] + thisTS[t].grossEnergy;
                    }
                }
                else
                {
                    LT_AEP = LT_AEP + thisAEP;
                    for (int WD = 0; WD < thisInst.metList.numWD; WD++)
                        LT_SectorEnergy[WD] = LT_SectorEnergy[WD] + thisSectorEnergy[WD];

                    yearCount++;

                    if (thisTS[t].grossEnergy != -999)
                        thisAEP = thisTS[t].grossEnergy;
                    else
                        thisAEP = 0;

                    thisSectorEnergy = new double[thisInst.metList.numWD];
                    thisSectorEnergy[WD_ind] = thisTS[t].grossEnergy;
                }

                if (month == lastMonth && year == lastYear)
                {
                    if (thisTS[t].grossEnergy != -999)
                        monthEnergy = monthEnergy + thisTS[t].grossEnergy;
                }
                else
                {
                    Array.Resize(ref grossEst.monthlyVals, monthInd + 1);
                    grossEst.monthlyVals[monthInd].month = lastMonth;
                    grossEst.monthlyVals[monthInd].year = lastYear;
                    grossEst.monthlyVals[monthInd].energyProd = monthEnergy / 1000; // Save in MWh

                    if (thisTS[t].grossEnergy != -999)
                        monthEnergy = thisTS[t].grossEnergy;
                    else
                        monthEnergy = 0;

                    monthInd++;

                    lastMonth = month;
                    lastYear = year;                                        
                }
            }

            // Add last year and last month of last year
            if (thisTS[thisTS.Length - 1].dateTime.Month == 12 && thisTS[thisTS.Length - 1].dateTime.Day == 31) // have another full year to add
            {
                LT_AEP = LT_AEP + thisAEP;
                for (int WD = 0; WD < thisInst.metList.numWD; WD++)
                    LT_SectorEnergy[WD] = LT_SectorEnergy[WD] + thisSectorEnergy[WD];

                yearCount++;

                Array.Resize(ref grossEst.monthlyVals, monthInd + 1);
                grossEst.monthlyVals[monthInd].month = lastMonth;
                grossEst.monthlyVals[monthInd].year = lastYear;
                grossEst.monthlyVals[monthInd].energyProd = monthEnergy / 1000; // Save in MWh
            }
            else
            {
                // Add last month (if it's a full month)
                DateTime nextTS = thisTS[thisTS.Length - 1].dateTime.AddHours(timeInt);

                if (nextTS.Day == 1 && nextTS.Hour == 0)
                {
                    Array.Resize(ref grossEst.monthlyVals, monthInd + 1);
                    grossEst.monthlyVals[monthInd].month = lastMonth;
                    grossEst.monthlyVals[monthInd].year = lastYear;
                    grossEst.monthlyVals[monthInd].energyProd = monthEnergy / 1000; // Save in MWh
                }
            }

            if (yearCount > 0)
            {
                grossEst.AEP = LT_AEP / yearCount / 1000; // Save in MWh

                for (int i = 0; i < thisInst.metList.numWD; i++)
                    grossEst.sectorEnergy[i] = LT_SectorEnergy[i] / yearCount / 1000; // save in MWh
            }

            grossEst.CF = grossEst.AEP / (grossEst.powerCurve.ratedPower * 8.76);
                       

        }

        /// <summary> Reads in a timeseries dataset and calculates LT net AEP, LT CF, sectorwise energy, and monthly values and saves to referenced net energy estimate </summary>        
        public void CalcNetAEP_AndMonthlyEnergy(ref Turbine.Net_Energy_Est netEst, ModelCollection.TimeSeries[] thisTS, Continuum thisInst)
        {
            if (thisTS == null) return;
            if (thisTS.Length < 1) return;

            int TS_Length = thisTS.Length;

            // Figure out time interval (in hours)
            TimeSpan timeSpan = thisTS[1].dateTime - thisTS[0].dateTime;
            double timeInt = timeSpan.TotalMinutes / 60.0;

            // Start on first hour on first of month
            int TS_Ind = 0;
            int thisDay = thisTS[TS_Ind].dateTime.Day;
            int thisHour = thisTS[TS_Ind].dateTime.Hour;
                        
            MCP mcp = new MCP(); // Created to use Get_WD_Ind function
            mcp.numWD = thisInst.metList.numWD;

            // Go to first day of month
            bool atFirstDay = false;

            if (thisDay == 1 && thisHour == 0)
                atFirstDay = true;

            while (atFirstDay == false && TS_Ind < thisTS.Length - 1)
            {
                TS_Ind++;
                thisDay = thisTS[TS_Ind].dateTime.Day;
                thisHour = thisTS[TS_Ind].dateTime.Hour;

                if (thisDay == 1 && thisHour == 0)
                    atFirstDay = true;
            }                        

            double LT_AEP = 0;
            int yearCount = 0;
            double thisAEP = 0;
            double[] LT_SectorEnergy = new double[thisInst.metList.numWD];
            double[] thisSectorEnergy = new double[thisInst.metList.numWD];
            netEst.sectorEnergy = new double[thisInst.metList.numWD];

            if (atFirstDay == false)
                return;

            int lastMonth = thisTS[TS_Ind].dateTime.Month;
            int lastYear = thisTS[TS_Ind].dateTime.Year;

            double monthEnergy = 0;
            int monthInd = 0;                                    

            for (int t = TS_Ind; t < TS_Length; t++)
            {
                int month = thisTS[t].dateTime.Month;
                int year = thisTS[t].dateTime.Year;
                int WD_ind = mcp.Get_WD_ind(thisTS[t].WD);

                if (year == lastYear)
                {
                    if (thisTS[t].netEnergy != -999)
                    {
                        thisAEP = thisAEP + thisTS[t].netEnergy;
                        thisSectorEnergy[WD_ind] = thisSectorEnergy[WD_ind] + thisTS[t].netEnergy;
                    }
                }
                else
                {
                    LT_AEP = LT_AEP + thisAEP;
                    for (int WD = 0; WD < thisInst.metList.numWD; WD++)
                        LT_SectorEnergy[WD] = LT_SectorEnergy[WD] + thisSectorEnergy[WD];

                    yearCount++;                    
                    thisSectorEnergy = new double[thisInst.metList.numWD];

                    if (thisTS[t].netEnergy != -999)
                        thisAEP = thisTS[t].netEnergy;
                    else
                        thisAEP = 0;
                }

                if (month == lastMonth && year == lastYear)
                {
                    if (thisTS[t].netEnergy != -999)
                        monthEnergy = monthEnergy + thisTS[t].netEnergy;
                }
                else
                {
                    Array.Resize(ref netEst.monthlyVals, monthInd + 1);
                    netEst.monthlyVals[monthInd].month = lastMonth;
                    netEst.monthlyVals[monthInd].year = lastYear;
                    netEst.monthlyVals[monthInd].energyProd = monthEnergy / 1000; // Save in MWh

                    if (thisTS[t].netEnergy != -999)
                        monthEnergy = thisTS[t].netEnergy;
                    else
                        monthEnergy = 0;
                    
                    monthInd++;

                    lastMonth = month;
                    lastYear = year;                                        
                }

            }

            // Add last year and last month of last year
            if (thisTS[thisTS.Length - 1].dateTime.Month == 12 && thisTS[thisTS.Length - 1].dateTime.Day == 31) // have another full year to add
            {
                LT_AEP = LT_AEP + thisAEP;
                for (int WD = 0; WD < thisInst.metList.numWD; WD++)
                    LT_SectorEnergy[WD] = LT_SectorEnergy[WD] + thisSectorEnergy[WD];

                yearCount++;

                Array.Resize(ref netEst.monthlyVals, monthInd + 1);
                netEst.monthlyVals[monthInd].month = lastMonth;
                netEst.monthlyVals[monthInd].year = lastYear;
                netEst.monthlyVals[monthInd].energyProd = monthEnergy / 1000; // Save in MWh
            }
            else
            {
                // Add last month (if it's a full month)
                DateTime nextTS = thisTS[thisTS.Length - 1].dateTime.AddHours(timeInt);

                if (nextTS.Day == 1 && nextTS.Hour == 0)
                {
                    Array.Resize(ref netEst.monthlyVals, monthInd + 1);
                    netEst.monthlyVals[monthInd].month = lastMonth;
                    netEst.monthlyVals[monthInd].year = lastYear;
                    netEst.monthlyVals[monthInd].energyProd = monthEnergy / 1000; // Save in MWh
                }
            }

            if (yearCount > 0)
            {
                netEst.AEP = LT_AEP / yearCount / 1000; // Save in MWh

                for (int i = 0; i < thisInst.metList.numWD; i++)
                    netEst.sectorEnergy[i] = LT_SectorEnergy[i] / yearCount / 1000; // Save in MWh
            }

            netEst.CF = netEst.AEP / (netEst.wakeModel.powerCurve.ratedPower * 8.76);

        }

        /// <summary> Returns true if model collection contains required models for specified number of time of day and seasonal bins. </summary>        
        public bool HaveRequiredModels(MetCollection metList)
        {
            bool haveModels = false;
            
            if (metList.numTOD == 1 && metList.numSeason == 1)
            {                
                for (int i = 0; i < ModelCount; i++)
                    if (models[i, 0].season == Met.Season.All && models[i, 0].timeOfDay == Met.TOD.All && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        haveModels = true;

            }
            else if (metList.numTOD == 2 && metList.numSeason == 1)
            {
                bool gotDay = false;
                bool gotNight = false;

                for (int i = 0; i < ModelCount; i++)
                {
                    if (models[i, 0].season == Met.Season.All && models[i, 0].timeOfDay == Met.TOD.Day && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotDay = true;
                    else if (models[i, 0].season == Met.Season.All && models[i, 0].timeOfDay == Met.TOD.Night && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotNight = true;
                }

                if (gotDay == true && gotNight == true)
                    haveModels = true;
            }
            else if (metList.numTOD == 1 & metList.numSeason == 4)
            {
                bool gotWinter = false;
                bool gotSpring = false;
                bool gotSummer = false;
                bool gotFall = false;

                for (int i = 0; i < ModelCount; i++)
                {
                    if (models[i, 0].season == Met.Season.Winter && models[i, 0].timeOfDay == Met.TOD.All && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotWinter = true;
                    else if (models[i, 0].season == Met.Season.Spring && models[i, 0].timeOfDay == Met.TOD.All && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotSpring = true;
                    else if (models[i, 0].season == Met.Season.Summer && models[i, 0].timeOfDay == Met.TOD.All && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotSummer = true;
                    else if (models[i, 0].season == Met.Season.Fall && models[i, 0].timeOfDay == Met.TOD.All && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotFall = true;
                }

                if (gotWinter && gotSpring && gotSummer && gotFall)
                    haveModels = true;
            }
            else if (metList.numTOD == 2 && metList.numSeason == 4)
            {
                bool gotWinterDay = false;
                bool gotWinterNight = false;
                bool gotSpringDay = false;
                bool gotSpringNight = false;
                bool gotSummerDay = false;
                bool gotSummerNight = false;
                bool gotFallDay = false;
                bool gotFallNight = false;

                for (int i = 0; i < ModelCount; i++)
                {
                    if (models[i, 0].season == Met.Season.Winter && models[i, 0].timeOfDay == Met.TOD.Day && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotWinterDay = true;
                    else if (models[i, 0].season == Met.Season.Winter && models[i, 0].timeOfDay == Met.TOD.Night && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotWinterNight = true;
                    else if (models[i, 0].season == Met.Season.Spring && models[i, 0].timeOfDay == Met.TOD.Day && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotSpringDay = true;
                    else if (models[i, 0].season == Met.Season.Spring && models[i, 0].timeOfDay == Met.TOD.Night && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotSpringNight = true;
                    else if (models[i, 0].season == Met.Season.Summer && models[i, 0].timeOfDay == Met.TOD.Day && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotSummerDay = true;
                    else if (models[i, 0].season == Met.Season.Summer && models[i, 0].timeOfDay == Met.TOD.Night && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotSummerNight = true;
                    else if (models[i, 0].season == Met.Season.Fall && models[i, 0].timeOfDay == Met.TOD.Day && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotFallDay = true;
                    else if (models[i, 0].season == Met.Season.Fall && models[i, 0].timeOfDay == Met.TOD.Night && sameMets(metList.GetMetsUsed(), models[i, 0].metsUsed))
                        gotFallNight = true;
                }

                if (gotWinterDay && gotWinterNight && gotSpringDay && gotSpringNight && gotSummerDay && gotSummerNight && gotFallDay && gotFallNight)
                    haveModels = true;
            }


            return haveModels;
        }

    }

}
