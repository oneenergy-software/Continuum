
using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using OSGeo.GDAL;
using OSGeo.OGR;
using OSGeo.OSR;
using System.Runtime.Serialization.Formatters.Binary;
 
namespace ContinuumNS
{
    
    /// <summary> Class that contains all information and functions regarding the digital elevation (topography) and land cover datasets. Also it contains functions for calculating terrain
    /// exposure, average surface roughness and average displacement height. </summary>
    
    [Serializable()]
    public class TopoInfo
    {
        /// <summary> Elevation data used for plotting only. </summary>
        public double[,] topoElevs;
        /// <summary> Elevation data used for calculations. </summary>
        public double[,] elevsForCalcs;
        /// <summary> Surface roughness data used for calculations. </summary>
        public double[,] SR_ForCalcs;
        /// <summary> Displacement height data used for calculations. </summary>
        public double[,] DH_ForCalcs;

        /// <summary> True when surface roughness or land cover data has been loaded. </summary>
        public bool gotSR;
        /// <summary> If true then surface roughness model is used in wind speed estimates. </summary>
        public bool useSR;
        /// <summary> If true then flow separation model is used in wind speed estimates. </summary>
        public bool useSepMod;
        /// <summary> True when topo data has been loaded. </summary>
        public bool gotTopo;

        /// <summary> Holds Num X/Y All/Plot/Calcs of elevation data. </summary>
        public Min_Max_Num_XYs topoNumXY = new Min_Max_Num_XYs();
        /// <summary> Holds Num X/Y All/Plot/Calcs of land cover data. </summary>
        public Min_Max_Num_XYs LC_NumXY;

        /// <summary>  Land cover data used for plotting only. </summary>
        public int[,] landCover;
        /// <summary>  Land cover key (converts land cover code to surface roughness and displacement height). </summary>
        public LC_SR_DH[] LC_Key;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>   Struct containing two Dec_Type structs which contain Min_Max_Num structs for each grid type (i.e. all, calcs, or plot). </summary>        
        [Serializable()]
        public struct Min_Max_Num_XYs
        {
            /// <summary>   X Min_Max_Num structs for each grid type. </summary>
            public Dec_Type X;
            /// <summary>   Y Min_Max_Num structs for each grid type. </summary>
            public Dec_Type Y;
        }
       
        /// <summary> Contains Min_Max_Num structs (min, max, num, and reso). One for each topo grid type (i.e. all, calcs, or plot) </summary>
        [Serializable()]
        public struct Dec_Type
        {
            /// <summary> All data in database. </summary>
            public Min_Max_Num all;
            /// <summary> Decimated for plotting. </summary>
            public Min_Max_Num plot;
            /// <summary> Data extracted for calculations. </summary>
            public Min_Max_Num calcs;
        }

        /// <summary> Contains grid min, max, count, and resolution. </summary>        
        [Serializable()]
        public struct Min_Max_Num
        {
            /// <summary> Grid minimum. </summary>
            public double min;
            /// <summary> Grid maximum. </summary>
            public double max;
            /// <summary> Grid count. </summary>
            public int num;
            /// <summary> Grid resolution. </summary>
            public double reso;
        }
                
        /// <summary> Contains UTMX/Y, elevation, and exposure info for a grid point. </summary>  
        [Serializable()]
        public struct TopoGrid
        {
            /// <summary> UTM X coordinate. </summary>
            public double UTMX;
            /// <summary> UTM Y coordinate. </summary>
            public double UTMY;
            /// <summary> Elevation. </summary>
            public double elev;
            /// <summary> Exposure calculated with a smaller radius of investigation. </summary>
            public Exposure smallerR_Exposure;
            /// <summary> Smaller radius. </summary>
            public int smallerRadius;
        }
                
        /// <summary> Contains UTMX/Y and Land cover code for a grid point. </summary>    
        public struct LandCoverGrid
        {
            /// <summary> UTM X coordinate. </summary>
            public double UTMX;
            /// <summary> UTM Y coordinate. </summary>
            public double UTMY;
            /// <summary> Land cover code. </summary>
            public int LC_code;
        }
        
        /// <summary>  Contains land cover code, description, surface roughness and displacement height. </summary> 
        [Serializable()]
        public struct LC_SR_DH
        {
            /// <summary> Land cover code. </summary>
            public int code;
            /// <summary> Land cover description. </summary>
            public string desc;
            /// <summary> Surface roughness [m]. </summary>
            public double SR;
            /// <summary> Displacement height [m]. </summary>
            public double DH;
        }
                
        /// <summary>   Struct containing UTMX/Y. </summary>        
        [Serializable()]
        public struct UTM_X_Y
        {
            /// <summary> UTM X coordinate. </summary>
            public double UTMX;
            /// <summary> UTM Y coordinate. </summary>
            public double UTMY;
        }          
               
        /// <summary> Contains info related to a shape read in from roughness maps (.MAP). </summary>        
        public struct Roughness_Map_Struct
        {
            // Used when reading .map roughness file
            /// <summary> Minimum X coordinate. </summary>
            public double minX;
            /// <summary> Maximum X coordinate. </summary>
            public double maxX;
            /// <summary> Minimum Y coordinate. </summary>
            public double minY;
            /// <summary> Maximum Y coordinate. </summary>
            public double maxY;

            /// <summary> Surface roughness on left side of line. </summary>
            public double leftRough;
            /// <summary> Surface roughness on right side of line. </summary>
            public double rightRough;

            /// <summary> Number of points. </summary>
            public int numPoints;
            /// <summary> Array of struct UTM_X_Y containing coordinates of points. </summary>
            public UTM_X_Y[] points;

            /// <summary> True if is closed, false if not. </summary>
            public bool isClosed;            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Returns param array based on specified parameter ("Land Cover", "Surface Roughness" or "Displacement Height"). </summary>
        public double[,] GetLC_ParamToPlot(string paramType)
        {
            int numX_Plot = LC_NumXY.X.plot.num;
            int numY_Plot = LC_NumXY.Y.plot.num;

            double[,] param = new double[numX_Plot, numY_Plot];

            if (LC_Key == null)
            {
                MessageBox.Show("No land cover / surface roughness info entered.", "Continuum 2.2");
                return param;
            }

            int numSR = LC_Key.Length;                       

            int X_Ind = 0;
            int Y_Ind = 0;

            for (int i = 0; i < numX_Plot; i++)
            {
                for (int j = 0; j < numY_Plot; j++)
                {
                    for (int k = 0; k < numSR; k++)
                    {
                        if (landCover[i, j] == LC_Key[k].code)
                        {
                            if (paramType == "Surface Roughness")
                                param[X_Ind, Y_Ind] = LC_Key[k].SR;
                            else if (paramType == "Displacement Height")
                                param[X_Ind, Y_Ind] = LC_Key[k].DH;
                            else
                                param[X_Ind, Y_Ind] = LC_Key[k].code;

                            break;
                        }
                    }

                    Y_Ind = Y_Ind + 1;

                    if (Y_Ind >= numY_Plot)
                        break;

                }
                X_Ind = X_Ind + 1;
                if (X_Ind >= numX_Plot)
                    break;

                Y_Ind = 0;
            }

            return param;
        }


        /// <summary>  Returns minimum value of two-dimensional param array. </summary> 
        public double GetMin(double[,] param, bool ignoreZeros)
        {            
            double thisMin = 100000;

            for (int i = 0; i <= param.GetUpperBound(0); i++)
                for (int j = 0; j <= param.GetUpperBound(1); j++)
                    if (param[i, j] < thisMin && param[i,j] != -999 && ((ignoreZeros == true && param[i, j] != 0) || (ignoreZeros == false)))
                        thisMin = param[i, j];

            return thisMin;
        }

        /// <summary> Returns maximum value of two-dimensional param array. </summary>     

        public double GetMax(double[,] param)
        {            
            double thisMax = 0;

            for (int i = 0; i <= param.GetUpperBound(0); i++)
                for (int j = 0; j <= param.GetUpperBound(1); j++)
                    if (param[i, j] > thisMax)
                        thisMax = param[i, j];

            return thisMax;
        }


        /// <summary> Returns minimum value of one-dimensional theseParams array. </summary>        

        public double FindMin(double[] theseParams)
        {            
            double thisMin = 1000000;
            int numParams;

            try
            {
                numParams = theseParams.Length;
            }
            catch
            {
                numParams = 0;
            }

            for (int i = 0; i < numParams; i++)
                if (theseParams[i] < thisMin)
                    thisMin = theseParams[i];

            return thisMin;
        }


        /// <summary> Returns maximum value of one-dimensional param array. </summary>

        public double FindMax(double[] theseParams)
        {            
            double thisMax = -1000000;
            int numParams;

            try
            {
                numParams = theseParams.Length;
            }
            catch {
                numParams = 0;
            }

            for (int i = 0; i < numParams; i++)
                if (theseParams[i] > thisMax)
                    thisMax = theseParams[i];

            return thisMax;
        }

        /// <summary> Returns average of one-dimensional theseParams array. </summary>        
        public double FindAvg(double[] theseParams)
        {            
            double avg = 0;
            int numParams;

            try
            {
                numParams = theseParams.Length;
            }
            catch
            {
                numParams = 0;
            }

            for (int i = 0; i < numParams; i++)
                avg = avg + theseParams[i];

            if (numParams > 0)
                avg = avg / numParams;
            else
                avg = 0;


            return avg;
        }

        /// <summary> Returns standard deviation of one-dimensional theseParams array. </summary>        
        public double FindSD(double[] theseParams)
        {            
            double stDev = 0;
            double avg = 0;
            int numParams = 0;

            try
            {
                numParams = theseParams.Length;
            }
            catch {
                numParams = 0;
            }

            for (int i = 0; i < numParams; i++)
            {
                avg = avg + theseParams[i];
                stDev = stDev + Convert.ToSingle(Math.Pow(theseParams[i], 2));
            }

            if (numParams > 0)
            {
                avg = avg / numParams;
                stDev = Convert.ToSingle(Math.Pow((stDev / numParams - Math.Pow(avg, 2)), 0.5));
            }

            return stDev;

        }
        
        /// <summary> Returns 1 if gotTopo is false; returns 6 if gotTopo is true and user wants to load new data; returns 7 if gotTopo is true but user does not want to load new data. </summary>
        public int OkToReload()
        {            
            int goodToGo = 1;

            if (gotTopo == true) // Already have topo data loaded, need to clear it first before loading new stuff
                goodToGo = Convert.ToInt16(MessageBox.Show("You already have topo data loaded.  Do you want to load new data?  If you proceed, all calculated exposures, " +
                    "models and parameters will be lost.", "Continuum 3", MessageBoxButtons.YesNo));

            return goodToGo;
        }

        /// <summary> If user agrees to load new LC data, it clears the calculated SRDH at mets and estimates at turbine sites and at nodes in the database. </summary>       
        public bool LC_OkToReload(string savedFileName, Continuum continuum)
        {            
            int goodToGo;
            bool okBool = false;
            int numRadii = 0;
            NodeCollection nodeList = new NodeCollection();

            if (gotSR == true) // Already have surface roughness data loaded
            {
                goodToGo = Convert.ToInt16(MessageBox.Show("You already have land cover data loaded.  Do you want to load new data?", "Continuum 2.2", MessageBoxButtons.YesNo));
                if (goodToGo == 6)
                {
                    okBool = true;
                    landCover = null;
                    // Clear met SR/DH
                    for (int met_ind = 0; met_ind < continuum.metList.ThisCount; met_ind++)
                    {
                        if (continuum.metList.metItem[met_ind].expo != null)
                        {
                            numRadii = continuum.metList.metItem[met_ind].ExposureCount;

                            for (int rad_ind = 0; rad_ind < numRadii; rad_ind++)
                            {
                                if (continuum.metList.metItem[met_ind].expo[rad_ind].SR != null)
                                {
                                    continuum.metList.metItem[met_ind].expo[rad_ind].dispH = null;
                                    continuum.metList.metItem[met_ind].expo[rad_ind].SR = null;
                                    continuum.metList.metItem[met_ind].expo[rad_ind].SR_Dist = null;                                    
                                }
                            }
                        }
                    }
                    
                    // Clear turbine SR/DH
                    if (useSR == true)
                    {
                        continuum.turbineList.ClearAllWSEsts();
                        continuum.turbineList.ClearAllGrossEsts();
                        continuum.turbineList.ClearAllNetEsts();
                    }
                    
                    for (int turb_ind = 0; turb_ind < continuum.turbineList.TurbineCount; turb_ind++)
                    {
                        if (continuum.turbineList.turbineEsts[turb_ind].expo != null)
                        {
                            numRadii = continuum.turbineList.turbineEsts[turb_ind].ExposureCount;

                            for (int rad_ind = 0; rad_ind < numRadii; rad_ind++)
                            {
                                if (continuum.turbineList.turbineEsts[turb_ind].expo[rad_ind].SR != null)
                                {
                                    continuum.turbineList.turbineEsts[turb_ind].expo[rad_ind].dispH = null;
                                    continuum.turbineList.turbineEsts[turb_ind].expo[rad_ind].SR = null;
                                    continuum.turbineList.turbineEsts[turb_ind].expo[rad_ind].SR_Dist = null;                                    
                                }
                            }
                        }
                    }
                    
                    // Clear DB node.expo SR/DH
                    string connString = nodeList.GetDB_ConnectionString(savedFileName);                    
                    Exposure thisExpo = new Exposure();

                    try
                    {
                        using (var context = new Continuum_EDMContainer(connString))
                        {
                            var expo_db = from N in context.Expo_table select N;

                            foreach (var N in expo_db)
                            {
                                N.SR_Array = null;
                                N.DH_Array = null;
                            }

                            context.SaveChanges();                            
                        }
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.InnerException.ToString());
                    }


                    // Clear DB Map_nodes that use SR/DH

                }
                else
                    okBool = false;
            }
            else
                okBool = true;

            return okBool;
        }

        /// <summary> Returns interpolated elevation at specified UTMX and UTMY. </summary>        
        public double CalcElevs(double UTMX, double UTMY)
        {            
            double sumDist;
            double elev = 0;

            int thisX_Ind = Convert.ToInt16((UTMX - topoNumXY.X.calcs.min) / topoNumXY.X.calcs.reso);
            int thisY_Ind = Convert.ToInt16((UTMY - topoNumXY.Y.calcs.min) / topoNumXY.Y.calcs.reso);                       

            int xInd1 = thisX_Ind;
            int xInd2 = thisX_Ind + 1;
            int yInd1 = thisY_Ind;
            int yInd2 = thisY_Ind + 1;

            double targetEastMin = xInd1 * topoNumXY.X.calcs.reso + topoNumXY.X.calcs.min;
            double targetEastMax = xInd2 * topoNumXY.X.calcs.reso + topoNumXY.X.calcs.min;
            double targetNorthMin = yInd1 * topoNumXY.Y.calcs.reso + topoNumXY.Y.calcs.min;
            double targetNorthMax = yInd2 * topoNumXY.Y.calcs.reso + topoNumXY.Y.calcs.min;                        

            if (elevsForCalcs == null)
                return elev;

            double distanceSqrd = (targetEastMin - UTMX) * (targetEastMin - UTMX) + (targetNorthMin - UTMY) * (targetNorthMin - UTMY);
            if (distanceSqrd > 0)
            {
                sumDist = 1 / distanceSqrd;                
                elev = elevsForCalcs[xInd1, yInd1] / distanceSqrd;                
            }
            else
            {
                elev = elevsForCalcs[xInd1, yInd1];
                return elev;
            }
                        
            distanceSqrd = (targetEastMin - UTMX) * (targetEastMin - UTMX) + (targetNorthMax - UTMY) * (targetNorthMax - UTMY);
            if (distanceSqrd > 0)
            {
                sumDist = sumDist + 1 / distanceSqrd;                
                elev = elev + elevsForCalcs[xInd1, yInd2] / distanceSqrd;
                
            }
            else
            {
                elev = elevsForCalcs[xInd1, yInd2];
                return elev;
            }
                        
            distanceSqrd = (targetEastMax - UTMX) * (targetEastMax - UTMX) + (targetNorthMin - UTMY) * (targetNorthMin - UTMY);
            if (distanceSqrd > 0)
            {
                sumDist = sumDist + 1 / distanceSqrd;                
                elev = elev + elevsForCalcs[xInd2, yInd1] / distanceSqrd;                
            }
            else
            {
                elev = elevsForCalcs[xInd2, yInd1];
                return elev;
            }
                                    
            distanceSqrd = (targetEastMax - UTMX) * (targetEastMax - UTMX) + (targetNorthMax - UTMY) * (targetNorthMax - UTMY);
            if (distanceSqrd > 0)
            {
                sumDist = sumDist + 1 / distanceSqrd;                
                elev = elev + elevsForCalcs[xInd2, yInd2] / distanceSqrd;                
            }
            else
            {
                elev = elevsForCalcs[xInd2, yInd2];
                return elev;
            }
            
            if (sumDist > 0) elev = elev / sumDist;

            return elev;
        }

        /// <summary> Calculates wind direction corresponding to thisX and thisY. </summary>        
        public double GetDirection(double thisX, double thisY)
        {            
            double direction = Convert.ToSingle(Math.Atan2(thisY, thisX) * 180 / Math.PI);

            direction = 90 - direction; // moves 0 degree from east (90 degs) To north (0 degs)                                                              

            if (direction < 0) direction = direction + 360;

            return direction;
        }

        /// <summary> Returns wind direction sector corresponding to deltaX, deltaY, and bin size (degrees). </summary>        
        public int CalcDirInd(double deltaX, double deltaY, double dirBinSize)
        {            
            double direction = GetDirection(deltaX, deltaY);
            int dirInd;

            if (direction >= (360 - dirBinSize / 2) || direction <= dirBinSize / 2)
                dirInd = 0;
            else
                dirInd = Convert.ToInt16(Math.Round(direction / dirBinSize, 0));
                        
            return dirInd;
        }

        /// <summary> Returns node coordinates of data point that is closest to UTMX and UTMY. </summary>        
        public TopoGrid GetClosestNode(double UTMX, double UTMY, string Topo_or_LC)
        {            
            double targetEastMin = 0;
            double targetEastMax = 0;
            double targetNorthMin = 0;
            double targetNorthMax = 0;
            TopoGrid closestNode = new TopoGrid();

            int thisX_Ind = 0;
            int thisY_Ind = 0;

            int xInd1;
            int xInd2;
            int yInd1;
            int yInd2;

            if (Topo_or_LC == "Topography")
            {
                // Find four closest data points
                thisX_Ind = Convert.ToInt32((UTMX - topoNumXY.X.all.min) / topoNumXY.X.all.reso);
                thisY_Ind = Convert.ToInt32((UTMY - topoNumXY.Y.all.min) / topoNumXY.Y.all.reso);

                xInd1 = thisX_Ind;
                xInd2 = thisX_Ind + 1;
                yInd1 = thisY_Ind;
                yInd2 = thisY_Ind + 1;

                targetEastMin = xInd1 * topoNumXY.X.all.reso + topoNumXY.X.all.min;
                targetEastMax = xInd2 * topoNumXY.X.all.reso + topoNumXY.X.all.min;
                targetNorthMin = yInd1 * topoNumXY.Y.all.reso + topoNumXY.Y.all.min;
                targetNorthMax = yInd2 * topoNumXY.Y.all.reso + topoNumXY.Y.all.min;
            }
            else if (Topo_or_LC == "Land Cover")
            {
                // Find four closest data points
                thisX_Ind = Convert.ToInt32((UTMX - LC_NumXY.X.all.min) / LC_NumXY.X.all.reso);
                thisY_Ind = Convert.ToInt32((UTMY - LC_NumXY.Y.all.min) / LC_NumXY.Y.all.reso);

                xInd1 = thisX_Ind;
                xInd2 = thisX_Ind + 1;
                yInd1 = thisY_Ind;
                yInd2 = thisY_Ind + 1;

                targetEastMin = xInd1 * LC_NumXY.X.all.reso + LC_NumXY.X.all.min;
                targetEastMax = xInd2 * LC_NumXY.X.all.reso + LC_NumXY.X.all.min;
                targetNorthMin = yInd1 * LC_NumXY.Y.all.reso + LC_NumXY.Y.all.min;
                targetNorthMax = yInd2 * LC_NumXY.Y.all.reso + LC_NumXY.Y.all.min;

            }

            // Find closest node
            double minDist = 1000;

            // Using East Min and North Min          
            double thisDist = CalcDistanceBetweenPoints(UTMX, UTMY, targetEastMin, targetNorthMin);

            if (thisDist < minDist)
            {
                minDist = thisDist;
                closestNode.UTMX = targetEastMin;
                closestNode.UTMY = targetNorthMin;
            }

            // Using East Min and North Max         
            thisDist = CalcDistanceBetweenPoints(UTMX, UTMY, targetEastMin, targetNorthMax);
            if (thisDist < minDist)
            {
                minDist = thisDist;
                closestNode.UTMX = targetEastMin;
                closestNode.UTMY = targetNorthMax;
            }

            // Using East Max and North Min          
            thisDist = CalcDistanceBetweenPoints(UTMX, UTMY, targetEastMax, targetNorthMin);
            if (thisDist < minDist)
            {
                minDist = thisDist;
                closestNode.UTMX = targetEastMax;
                closestNode.UTMY = targetNorthMin;
            }

            // Using East Max and North Max            
            thisDist = CalcDistanceBetweenPoints(UTMX, UTMY, targetEastMax, targetNorthMax);
            if (thisDist < minDist)
            {
                minDist = thisDist;
                closestNode.UTMX = targetEastMax;
                closestNode.UTMY = targetNorthMax;
            }

            return closestNode;

        }

        /// <summary> Returns a node on grid closest to UTMX/Y but a minimum distance away from edge of topo. </summary>        
        public TopoGrid GetClosestNodeFixedGrid(double UTMX, double UTMY, int gridReso, int minDist)
        {            
            TopoGrid closestNode = new TopoGrid();
                        
            int X_Ind = (int)Math.Round((UTMX - topoNumXY.X.all.min) / gridReso, 0);
            int Y_Ind = (int)Math.Round((UTMY - topoNumXY.Y.all.min) / gridReso, 0);

            double xFixed = topoNumXY.X.all.min + X_Ind * gridReso;
            double yFixed = topoNumXY.Y.all.min + Y_Ind * gridReso;

            // Check that location has elevation
            Check_class check = new Check_class();
            int goodToGo = check.NewNodeCheck(this, xFixed, yFixed, minDist, "Plot");

            if (goodToGo == 100)
            {
                closestNode.UTMX = X_Ind * gridReso + topoNumXY.X.all.min;
                closestNode.UTMY = Y_Ind * gridReso + topoNumXY.Y.all.min;
            }
            else if (goodToGo != -999)
            {
                while (goodToGo != 100)
                {
                    if (goodToGo == 0) // too far north
                        Y_Ind--;
                    else if (goodToGo == 1) // too far northeast
                    {
                        X_Ind--;
                        Y_Ind--;
                    }
                    else if (goodToGo == 2) // too far east                    
                        X_Ind--;
                    else if (goodToGo == 3) // too far southeast
                    {
                        X_Ind--;
                        Y_Ind++;
                    }
                    else if (goodToGo == 4) // too far south
                        Y_Ind++;
                    else if (goodToGo == 5) // too far southwest
                    {
                        X_Ind++;
                        Y_Ind++;
                    }
                    else if (goodToGo == 6) // too far west
                        X_Ind++;
                    else if (goodToGo == 7) // too far northwest
                    {
                        X_Ind++;
                        Y_Ind--;
                    }

                    xFixed = topoNumXY.X.all.min + X_Ind * gridReso;
                    yFixed = topoNumXY.Y.all.min + Y_Ind * gridReso;                                       

                    goodToGo = check.NewNodeCheck(this, xFixed, yFixed, minDist, "Plot");
                }

                closestNode.UTMX = X_Ind * gridReso + topoNumXY.X.all.min;
                closestNode.UTMY = Y_Ind * gridReso + topoNumXY.Y.all.min;
            }
            
            return closestNode;
        }

        /// <summary> Returns P10 upwind crosswind grade. Not used right now but will be when flow around hills algorithm is revisited. </summary>        
        public double CalcP10_UW_CrosswindGrade(double UTMX, double UTMY, int WD_sec, int numWD)
        {            
            double[] UWGrade = new double[4];
            double[] avgUWGrade = new double[6];
            double highestUWGrade;
            TopoGrid[] nodesCross;

            double[,] thetaMin = new double[6, 4];  // i = crosswind slice index, j = 400m chunk of crosswind slope
            double[,] thetaMax = new double[6, 4];
            double[,] X_min = new double[6, 4];
            double[,] X_max = new double[6, 4];
            double[,] Y_min = new double[6, 4];
            double[,] Y_max = new double[6, 4];                       

            if (numWD == 0)
                return 0;

            double WDbin = (double)360 / numWD;            
            int gradeInd = 0;
            double radialDist500 = 0;
            double radialDist300 = 0;
            double radialDist100 = 0;
            double WD_theta = WD_sec * (double)360 / numWD;

            //   minRadius = CInt(Min_Length * 1.1 / 2 / Tan(WDbin / 2 * PI / 180)) ' multiplied by 1.1 to make first radius slightly larger than 400 m across
            int thisRadius;

            // Takes seven slices of 1000 m and calculates the average slope in 4 - 400 m windows along each slice.
            // First slice is a radius of 0 m and increments by 500 m to 3000 m.
            //  The average slope is calculated at each slice and the highest of the six slices is found and used as the UW crosswind grade.
            for (int r = 0; r <= 5; r++)
            {
                thisRadius = (r + 1) * 500;
                thetaMin[r, 0] = 450 - WD_theta + Convert.ToSingle(Math.Atan(500 / thisRadius) * 180 / Math.PI);
                thetaMax[r, 0] = 450 - WD_theta + Convert.ToSingle(Math.Atan(100 / thisRadius) * 180 / Math.PI);

                thetaMin[r, 1] = 450 - WD_theta + Convert.ToSingle(Math.Atan(300 / thisRadius) * 180 / Math.PI);
                thetaMax[r, 1] = 450 - WD_theta - Convert.ToSingle(Math.Atan(100 / thisRadius) * 180 / Math.PI);

                thetaMin[r, 2] = 450 - WD_theta + Convert.ToSingle(Math.Atan(100 / thisRadius) * 180 / Math.PI);
                thetaMax[r, 2] = 450 - WD_theta - Convert.ToSingle(Math.Atan(300 / thisRadius) * 180 / Math.PI);

                thetaMin[r, 3] = 450 - WD_theta - Convert.ToSingle(Math.Atan(100 / thisRadius) * 180 / Math.PI);
                thetaMax[r, 3] = 450 - WD_theta - Convert.ToSingle(Math.Atan(500 / thisRadius) * 180 / Math.PI);

                if (thetaMin[r, 0] > 360) thetaMin[r, 0] = thetaMin[r, 0] - 360;
                if (thetaMax[r, 0] > 360) thetaMax[r, 0] = thetaMax[r, 0] - 360;
                if (thetaMin[r, 1] > 360) thetaMin[r, 1] = thetaMin[r, 1] - 360;
                if (thetaMax[r, 1] > 360) thetaMax[r, 1] = thetaMax[r, 1] - 360;
                if (thetaMin[r, 2] > 360) thetaMin[r, 2] = thetaMin[r, 2] - 360;
                if (thetaMax[r, 2] > 360) thetaMax[r, 2] = thetaMax[r, 2] - 360;
                if (thetaMin[r, 3] > 360) thetaMin[r, 3] = thetaMin[r, 3] - 360;
                if (thetaMax[r, 3] > 360) thetaMax[r, 3] = thetaMax[r, 3] - 360;

                radialDist500 = Convert.ToSingle(Math.Pow((Math.Pow(thisRadius, 2) + Math.Pow(500, 2)), 0.5));
                radialDist300 = Convert.ToSingle(Math.Pow((Math.Pow(thisRadius, 2) + Math.Pow(300, 2)), 0.5));
                radialDist100 = Convert.ToSingle(Math.Pow((Math.Pow(thisRadius, 2) + Math.Pow(100, 2)), 0.5));

                X_min[r, 0] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[r, 0] * Math.PI / 180)) * radialDist500);
                X_max[r, 0] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[r, 0] * Math.PI / 180)) * radialDist100);

                Y_min[r, 0] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[r, 0] * Math.PI / 180)) * radialDist500);
                Y_max[r, 0] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[r, 0] * Math.PI / 180)) * radialDist100);

                X_min[r, 1] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[r, 1] * Math.PI / 180)) * radialDist300);
                X_max[r, 1] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[r, 1] * Math.PI / 180)) * radialDist100);

                Y_min[r, 1] = UTMY + Convert.ToInt32((Math.Sin(thetaMin[r, 1] * Math.PI / 180)) * radialDist300);
                Y_max[r, 1] = UTMY + Convert.ToInt32((Math.Sin(thetaMax[r, 1] * Math.PI / 180)) * radialDist100);

                X_min[r, 2] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[r, 2] * Math.PI / 180)) * radialDist100);
                X_max[r, 2] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[r, 2] * Math.PI / 180)) * radialDist300);

                Y_min[r, 2] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[r, 2] * Math.PI / 180)) * radialDist100);
                Y_max[r, 2] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[r, 2] * Math.PI / 180)) * radialDist300);

                X_min[r, 3] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[r, 3] * Math.PI / 180)) * radialDist100);
                X_max[r, 3] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[r, 3] * Math.PI / 180)) * radialDist500);

                Y_min[r, 3] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[r, 3] * Math.PI / 180)) * radialDist100);
                Y_max[r, 3] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[r, 3] * Math.PI / 180)) * radialDist500);
            }

            int nodeCount = 9;

            for (int r = 0; r <= 5; r++)
            {
                gradeInd = 0;
                for (int c = 0; c <= 3; c++)
                {
                    double thisX_Min;
                    double thisX_Max;
                    double thisY_Min;
                    double thisY_Max;

                    if (X_min[r, c] < X_max[r, c])
                    {
                        thisX_Min = X_min[r, c];
                        thisX_Max = X_max[r, c];
                    }
                    else
                    {
                        thisX_Min = X_max[r, c];
                        thisX_Max = X_min[r, c];
                    }

                    if (Y_min[r, c] < Y_max[r, c])
                    {
                        thisY_Min = Y_min[r, c];
                        thisY_Max = Y_max[r, c];
                    }
                    else
                    {
                        thisY_Min = Y_max[r, c];
                        thisY_Max = Y_min[r, c];
                    }

                    if (thisY_Max - thisY_Min < 10)
                    {
                        thisY_Max = thisY_Max + 40;
                        thisY_Min = thisY_Min - 40;
                    }

                    if (thisX_Max - thisX_Min < 10)
                    {
                        thisX_Max = thisX_Max + 40;
                        thisX_Min = thisX_Min - 40;
                    }

                    nodesCross = new TopoGrid[nodeCount]; // resets array

                    // Calc slope between Min and Max chunk limits using 10 grid points 
                    for (int N = 0; N <= nodeCount - 1; N++)
                    {
                        nodesCross[N].UTMX = X_min[r, c] + N * (X_max[r, c] - X_min[r, c]) / 9;
                        nodesCross[N].UTMY = Y_min[r, c] + N * (Y_max[r, c] - Y_min[r, c]) / 9;
                        nodesCross[N].elev = CalcElevs(nodesCross[N].UTMX, nodesCross[N].UTMY);
                    }

                    // now calculate average slope of terrain
                    double Sx = 0;
                    double Sy = 0;
                    double Sxy = 0;
                    double Sx2 = 0;

                    for (int i = 0; i < nodeCount; i++)
                    {
                        double thisDist = CalcDistanceBetweenPoints(nodesCross[i].UTMX, nodesCross[i].UTMY, X_min[r, c], Y_min[r, c]);
                        Sx = Sx + thisDist; 
                        Sy = Sy + nodesCross[i].elev;                        
                        Sxy = Sxy + thisDist * nodesCross[i].elev;
                        Sx2 = Sx2 + Convert.ToSingle(Math.Pow(thisDist, 2));
                    }

                    Array.Resize(ref UWGrade, gradeInd + 1);
                    UWGrade[gradeInd] = Math.Abs((nodeCount * Sxy - Sx * Sy) / (nodeCount * Sx2 - Sx * Sx));

                    gradeInd = gradeInd + 1;
                }

                avgUWGrade[r] = (UWGrade[0] + UWGrade[1] + UWGrade[2] + UWGrade[3]) / 4;

            }

            highestUWGrade = 0;
            for (int r = 0; r <= 5; r++)
                if (avgUWGrade[r] > highestUWGrade)
                    highestUWGrade = avgUWGrade[r];

            return highestUWGrade;
        }


        /// <summary> Returns P10 upwind parallel grade. Not used right now but will be when flow around hills algorithm is revisited. </summary>        
        public double CalcP10_UW_ParallelGrade(double UTMX, double UTMY, int WD_sec, int numWD)
        {            
            double[] UWGrade = new double[11];
            double[] maxUWGrade = new double[5];            
            double WD_theta;
            TopoGrid[] nodesCross;

            double[,] thetaMin = new double[5, 11]; // i = crosswind slice index, j = 500m chunk of parallel slope
            double[,] thetaMax = new double[5, 11];
            double[,] X_min = new double[5, 11];
            double[,] X_max = new double[5, 11];
            double[,] Y_min = new double[5, 11];
            double[,] Y_max = new double[5, 11];                      

            if (numWD == 0) return 0;

          //  double WDbin = Convert.ToSingle(360.0 / numWD);                   
         
                        
            int[] isPositiveSlope = new int[5]; // 0 = not sure, 1 = positive, 2 = negative
            int[] hillSlopeChangeInd = new int[5]; // Chunk index where slope changes from + to - and is more than |0.02|
            int[] hillStartInd = new int[5]; // Chunk index where slope greater than 0.02 is first identified
            WD_theta = Convert.ToSingle(WD_sec * 360.0 / numWD);
            
            // Takes five slices of 3000 m and calculates the average slope in 11 - 500 m windows along each slice.
            // Slices are +/- 500 m and increments by 250 m.
            //  The maximum slope is calculated at each slice and the average of the five slices' max slope is found and used as the UW parallel grade.
            for (int d = 0; d <= 4; d++)
            {
                int thisDist = -500 + d * 250;
                if (thisDist < 0)
                {
                    thetaMin[d, 0] = 450 - WD_theta;
                    thetaMax[d, 0] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 500) * 180 / Math.PI);

                    thetaMin[d, 1] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 250) * 180 / Math.PI);
                    thetaMax[d, 1] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 750) * 180 / Math.PI);

                    thetaMin[d, 2] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 500) * 180 / Math.PI);
                    thetaMax[d, 2] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1000) * 180 / Math.PI);

                    thetaMin[d, 3] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 750) * 180 / Math.PI);
                    thetaMax[d, 3] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1250) * 180 / Math.PI);

                    thetaMin[d, 4] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1000) * 180 / Math.PI);
                    thetaMax[d, 4] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1500) * 180 / Math.PI);

                    thetaMin[d, 5] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1250) * 180 / Math.PI);
                    thetaMax[d, 5] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1750) * 180 / Math.PI);

                    thetaMin[d, 6] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1500) * 180 / Math.PI);
                    thetaMax[d, 6] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2000) * 180 / Math.PI);

                    thetaMin[d, 7] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1750) * 180 / Math.PI);
                    thetaMax[d, 7] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2250) * 180 / Math.PI);

                    thetaMin[d, 8] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2000) * 180 / Math.PI);
                    thetaMax[d, 8] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2500) * 180 / Math.PI);

                    thetaMin[d, 9] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2250) * 180 / Math.PI);
                    thetaMax[d, 9] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2750) * 180 / Math.PI);

                    thetaMin[d, 10] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2500) * 180 / Math.PI);
                    thetaMax[d, 10] = 450 - WD_theta + Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 3000) * 180 / Math.PI);

                }
                else
                {
                    thetaMin[d, 0] = 450 - WD_theta;
                    thetaMax[d, 0] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 500) * 180 / Math.PI);

                    thetaMin[d, 1] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 250) * 180 / Math.PI);
                    thetaMax[d, 1] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 750) * 180 / Math.PI);

                    thetaMin[d, 2] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 500) * 180 / Math.PI);
                    thetaMax[d, 2] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1000) * 180 / Math.PI);

                    thetaMin[d, 3] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 750) * 180 / Math.PI);
                    thetaMax[d, 3] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1250) * 180 / Math.PI);

                    thetaMin[d, 4] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1000) * 180 / Math.PI);
                    thetaMax[d, 4] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1500) * 180 / Math.PI);

                    thetaMin[d, 5] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1250) * 180 / Math.PI);
                    thetaMax[d, 5] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1750) * 180 / Math.PI);

                    thetaMin[d, 6] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1500) * 180 / Math.PI);
                    thetaMax[d, 6] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2000) * 180 / Math.PI);

                    thetaMin[d, 7] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 1750) * 180 / Math.PI);
                    thetaMax[d, 7] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2250) * 180 / Math.PI);

                    thetaMin[d, 8] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2000) * 180 / Math.PI);
                    thetaMax[d, 8] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2500) * 180 / Math.PI);

                    thetaMin[d, 9] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2250) * 180 / Math.PI);
                    thetaMax[d, 9] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2750) * 180 / Math.PI);

                    thetaMin[d, 10] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 2500) * 180 / Math.PI);
                    thetaMax[d, 10] = 450 - WD_theta - Convert.ToSingle(Math.Atan(Math.Abs(thisDist) / 3000) * 180 / Math.PI);
                }

                if (thetaMin[d, 0] > 360) thetaMin[d, 0] = thetaMin[d, 0] - 360;
                if (thetaMax[d, 0] > 360) thetaMax[d, 0] = thetaMax[d, 0] - 360;
                if (thetaMin[d, 1] > 360) thetaMin[d, 1] = thetaMin[d, 1] - 360;
                if (thetaMax[d, 1] > 360) thetaMax[d, 1] = thetaMax[d, 1] - 360;
                if (thetaMin[d, 2] > 360) thetaMin[d, 2] = thetaMin[d, 2] - 360;
                if (thetaMax[d, 2] > 360) thetaMax[d, 2] = thetaMax[d, 2] - 360;
                if (thetaMin[d, 3] > 360) thetaMin[d, 3] = thetaMin[d, 3] - 360;
                if (thetaMax[d, 3] > 360) thetaMax[d, 3] = thetaMax[d, 3] - 360;
                if (thetaMin[d, 4] > 360) thetaMin[d, 4] = thetaMin[d, 4] - 360;
                if (thetaMax[d, 4] > 360) thetaMax[d, 4] = thetaMax[d, 4] - 360;
                if (thetaMin[d, 5] > 360) thetaMin[d, 5] = thetaMin[d, 5] - 360;
                if (thetaMax[d, 5] > 360) thetaMax[d, 5] = thetaMax[d, 5] - 360;
                if (thetaMin[d, 6] > 360) thetaMin[d, 6] = thetaMin[d, 6] - 360;
                if (thetaMax[d, 6] > 360) thetaMax[d, 6] = thetaMax[d, 6] - 360;
                if (thetaMin[d, 7] > 360) thetaMin[d, 7] = thetaMin[d, 7] - 360;
                if (thetaMax[d, 7] > 360) thetaMax[d, 7] = thetaMax[d, 7] - 360;
                if (thetaMin[d, 8] > 360) thetaMin[d, 8] = thetaMin[d, 8] - 360;
                if (thetaMax[d, 8] > 360) thetaMax[d, 8] = thetaMax[d, 8] - 360;
                if (thetaMin[d, 9] > 360) thetaMin[d, 9] = thetaMin[d, 9] - 360;
                if (thetaMax[d, 9] > 360) thetaMax[d, 9] = thetaMax[d, 9] - 360;
                if (thetaMin[d, 10] > 360) thetaMin[d, 10] = thetaMin[d, 10] - 360;
                if (thetaMax[d, 10] > 360) thetaMax[d, 10] = thetaMax[d, 10] - 360;

                double radialDist0 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2)), 0.5));
                double radialDist250 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(250, 2)), 0.5));
                double radialDist500 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(500, 2)), 0.5));
                double radialDist750 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(750, 2)), 0.5));
                double radialDist1000 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(1000, 2)), 0.5));
                double radialDist1250 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(1250, 2)), 0.5));
                double radialDist1500 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(1500, 2)), 0.5));
                double radialDist1750 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(1750, 2)), 0.5));
                double radialDist2000 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(2000, 2)), 0.5));
                double radialDist2250 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(2250, 2)), 0.5));
                double radialDist2500 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(2500, 2)), 0.5));
                double radialDist2750 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(2750, 2)), 0.5));
                double radialDist3000 = Convert.ToSingle(Math.Pow((Math.Pow(thisDist, 2) + Math.Pow(3000, 2)), 0.5));

                X_min[d, 0] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[d, 0] * Math.PI / 180)) * radialDist0);
                X_max[d, 0] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[d, 0] * Math.PI / 180)) * radialDist500);

                Y_min[d, 0] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[d, 0] * Math.PI / 180)) * radialDist0);
                Y_max[d, 0] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[d, 0] * Math.PI / 180)) * radialDist500);

                X_min[d, 1] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[d, 1] * Math.PI / 180)) * radialDist250);
                X_max[d, 1] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[d, 1] * Math.PI / 180)) * radialDist750);

                Y_min[d, 1] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[d, 1] * Math.PI / 180)) * radialDist250);
                Y_max[d, 1] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[d, 1] * Math.PI / 180)) * radialDist750);

                X_min[d, 2] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[d, 2] * Math.PI / 180)) * radialDist500);
                X_max[d, 2] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[d, 2] * Math.PI / 180)) * radialDist1000);

                Y_min[d, 2] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[d, 2] * Math.PI / 180)) * radialDist500);
                Y_max[d, 2] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[d, 2] * Math.PI / 180)) * radialDist1000);

                X_min[d, 3] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[d, 3] * Math.PI / 180)) * radialDist750);
                X_max[d, 3] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[d, 3] * Math.PI / 180)) * radialDist1250);

                Y_min[d, 3] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[d, 3] * Math.PI / 180)) * radialDist750);
                Y_max[d, 3] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[d, 3] * Math.PI / 180)) * radialDist1250);

                X_min[d, 4] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[d, 4] * Math.PI / 180)) * radialDist1000);
                X_max[d, 4] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[d, 4] * Math.PI / 180)) * radialDist1500);

                Y_min[d, 4] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[d, 4] * Math.PI / 180)) * radialDist1000);
                Y_max[d, 4] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[d, 4] * Math.PI / 180)) * radialDist1500);

                X_min[d, 5] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[d, 5] * Math.PI / 180)) * radialDist1250);
                X_max[d, 5] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[d, 5] * Math.PI / 180)) * radialDist1750);

                Y_min[d, 5] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[d, 5] * Math.PI / 180)) * radialDist1250);
                Y_max[d, 5] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[d, 5] * Math.PI / 180)) * radialDist1750);

                X_min[d, 6] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[d, 6] * Math.PI / 180)) * radialDist1500);
                X_max[d, 6] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[d, 6] * Math.PI / 180)) * radialDist2000);

                Y_min[d, 6] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[d, 6] * Math.PI / 180)) * radialDist1500);
                Y_max[d, 6] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[d, 6] * Math.PI / 180)) * radialDist2000);

                X_min[d, 7] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[d, 7] * Math.PI / 180)) * radialDist1750);
                X_max[d, 7] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[d, 7] * Math.PI / 180)) * radialDist2250);

                Y_min[d, 7] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[d, 7] * Math.PI / 180)) * radialDist1750);
                Y_max[d, 7] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[d, 7] * Math.PI / 180)) * radialDist2250);

                X_min[d, 8] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[d, 8] * Math.PI / 180)) * radialDist2000);
                X_max[d, 8] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[d, 8] * Math.PI / 180)) * radialDist2500);

                Y_min[d, 8] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[d, 8] * Math.PI / 180)) * radialDist2000);
                Y_max[d, 8] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[d, 8] * Math.PI / 180)) * radialDist2500);

                X_min[d, 9] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[d, 9] * Math.PI / 180)) * radialDist2250);
                X_max[d, 9] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[d, 9] * Math.PI / 180)) * radialDist2750);

                Y_min[d, 9] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[d, 9] * Math.PI / 180)) * radialDist2250);
                Y_max[d, 9] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[d, 9] * Math.PI / 180)) * radialDist2750);

                X_min[d, 10] = UTMX + Convert.ToSingle((Math.Cos(thetaMin[d, 10] * Math.PI / 180)) * radialDist2500);
                X_max[d, 10] = UTMX + Convert.ToSingle((Math.Cos(thetaMax[d, 10] * Math.PI / 180)) * radialDist3000);

                Y_min[d, 10] = UTMY + Convert.ToSingle((Math.Sin(thetaMin[d, 10] * Math.PI / 180)) * radialDist2500);
                Y_max[d, 10] = UTMY + Convert.ToSingle((Math.Sin(thetaMax[d, 10] * Math.PI / 180)) * radialDist3000);
            }

            int nodeCount = 10;

            for (int d = 0; d <= 4; d++)
            {
                int gradeInd = 0;
                isPositiveSlope[d] = 0; // not sure

                for (int c = 0; c <= 10; c++)
                {
                    double thisX_Min;
                    double thisX_Max;
                    double thisY_Min;
                    double thisY_Max;

                    if (X_min[d, c] < X_max[d, c])
                    {
                        thisX_Min = X_min[d, c];
                        thisX_Max = X_max[d, c];
                    }
                    else
                    {
                        thisX_Min = X_max[d, c];
                        thisX_Max = X_min[d, c];
                    }

                    if (Y_min[d, c] < Y_max[d, c])
                    {
                        thisY_Min = Y_min[d, c];
                        thisY_Max = Y_max[d, c];
                    }
                    else
                    {
                        thisY_Min = Y_max[d, c];
                        thisY_Max = Y_min[d, c];
                    }

                    if (thisY_Max - thisY_Min < 10)
                    {
                        thisY_Max = thisY_Max + 40;
                        thisY_Min = thisY_Min - 40;
                    }

                    if (thisX_Max - thisX_Min < 10)
                    {
                        thisX_Max = thisX_Max + 40;
                        thisX_Min = thisX_Min - 40;
                    }

                    nodesCross = new TopoGrid[nodeCount];  // resets array       
                                                                 // Calc slope between Min and Max chunk limits using 10 grid points (spaced by 50 m)
                    for (int N = 0; N <= nodeCount - 1; N++)
                    {
                        nodesCross[N].UTMX = X_min[d, c] + N * (X_max[d, c] - X_min[d, c]) / 9;
                        nodesCross[N].UTMY = Y_min[d, c] + N * (Y_max[d, c] - Y_min[d, c]) / 9;
                        nodesCross[N].elev = CalcElevs(nodesCross[N].UTMX, nodesCross[N].UTMY);
                    }

                    // now calculate average slope of terrain
                    double Sx = 0;
                    double Sy = 0;
                    double Sxy = 0;
                    double Sx2 = 0;

                    for (int i = 0; i <= nodeCount - 1; i++)
                    {
                        Sx = Sx + Convert.ToSingle(Math.Pow((Math.Pow((nodesCross[i].UTMX - X_min[d, c]), 2) + Math.Pow((nodesCross[i].UTMY - Y_min[d, c]), 2)), 0.5));
                        Sy = Sy + nodesCross[i].elev;
                        Sxy = Sxy + Convert.ToSingle(Math.Pow((Math.Pow((nodesCross[i].UTMX - X_min[d, c]), 2) + Math.Pow((nodesCross[i].UTMY - Y_min[d, c]), 2)), 0.5) * nodesCross[i].elev);
                        Sx2 = Sx2 + Convert.ToSingle(Math.Pow((nodesCross[i].UTMX - X_min[d, c]), 2) + Math.Pow((nodesCross[i].UTMY - Y_min[d, c]), 2));
                    }

                    Array.Resize(ref UWGrade, gradeInd + 1);

                    UWGrade[gradeInd] = (nodeCount * Sxy - Sx * Sy) / (nodeCount * Sx2 - Sx * Sx);

                    if (isPositiveSlope[d] == 0 && Math.Abs(UWGrade[gradeInd]) > 0.02)   // 0 = not sure, 1 = positive, 2 = negative
                    {
                        hillStartInd[d] = gradeInd;
                        if (UWGrade[gradeInd] > 0)
                            isPositiveSlope[d] = 1;
                        else
                            isPositiveSlope[d] = 2;
                    }
                    else if (isPositiveSlope[d] != 0)
                    {
                        if ((UWGrade[gradeInd] > 0 && isPositiveSlope[d] == 2 && Math.Abs(UWGrade[gradeInd]) > 0.02) || (UWGrade[gradeInd] < 0 && isPositiveSlope[d] == 1 &&
                            Math.Abs(UWGrade[gradeInd]) > 0.02))
                        {
                            hillSlopeChangeInd[d] = gradeInd;
                            break;
                        }
                    }

                    gradeInd = gradeInd + 1;
                }

                if (hillSlopeChangeInd[d] == 0) hillSlopeChangeInd[d] = 10; // i.e. didn't find a change in slope
                maxUWGrade[d] = 0;

                if (isPositiveSlope[d] != 0)
                {
                    maxUWGrade[d] = UWGrade[hillStartInd[d]];
                    for (int i = hillStartInd[d]; i <= hillSlopeChangeInd[d] - 1; i++)
                        if (Math.Abs(UWGrade[i]) > Math.Abs(maxUWGrade[d])) maxUWGrade[d] = UWGrade[i];

                }

            }
                                  
            double highestGrade = 0;
            double secHighestGrade = 0;

            for (int s = 0; s <= 4; s++)
                if (Math.Abs(maxUWGrade[s]) > Math.Abs(highestGrade) && maxUWGrade[s] != 0) highestGrade = maxUWGrade[s];

            for (int s = 0; s <= 4; s++)
                if (Math.Abs(maxUWGrade[s]) > Math.Abs(secHighestGrade) && maxUWGrade[s] != highestGrade && maxUWGrade[s] != 0) secHighestGrade = maxUWGrade[s];

            double avgHighestUWGrade = (highestGrade + secHighestGrade) / 2;

            return avgHighestUWGrade;
        }


        /// <summary> Calculates and returns terrain exposure at specified UTMX/Y and using specified radius and exponent. </summary>
        public Exposure CalcExposures(double UTMX, double UTMY, double elev, int radius, double exponent, int numSectors, TopoInfo topo, int numWD)
        {                        
            Exposure expoReturn = new Exposure();

            if (numWD == 0)
            {
                MessageBox.Show("You need to import met files first.", "Continuum 2.2");
                return expoReturn;
            }
                        
            double dirBin = (double)360 / numWD;

            double[] exposure = new double[numWD];
            double[] exposureDist = new double[numWD];

            // Find square that contains radius of interest
            int minX_Ind = (int)Math.Round(((UTMX - radius - topo.topoNumXY.X.calcs.min) / topoNumXY.X.all.reso), 0);
            int maxX_Ind = (int)Math.Round(((UTMX + radius - topo.topoNumXY.X.calcs.min) / topoNumXY.X.all.reso), 0);
            int minY_Ind = (int)Math.Round(((UTMY - radius - topo.topoNumXY.Y.calcs.min) / topoNumXY.Y.all.reso), 0);
            int maxY_Ind = (int)Math.Round(((UTMY + radius - topo.topoNumXY.Y.calcs.min) / topoNumXY.Y.all.reso), 0);

            double maxDistance = Convert.ToSingle(radius * 1.414);

            int stepInt;

            // DEBUGGING
          //  StreamWriter sw = new StreamWriter("C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\New DB Structure\\Testing Calcs w Diff TopoCalcs\\Elevs & DirInd MapNode1 with Met 474.csv");

            if (topoNumXY.X.all.reso < 30)
                stepInt = (int)(30 / topoNumXY.X.all.reso);
            else
                stepInt = 1;                       

            for (int j = minX_Ind; j <= maxX_Ind; j = j + stepInt)
            {
                for (int k = minY_Ind; k <= maxY_Ind; k = k + stepInt)
                {
                    double gridUTMX = (j * topoNumXY.X.all.reso) + topo.topoNumXY.X.calcs.min;
                    double gridUTMY = (k * topoNumXY.Y.all.reso) + topo.topoNumXY.Y.calcs.min;                                        

                    double deltaX = gridUTMX - UTMX;
                    double deltaY = gridUTMY - UTMY;
                    if (deltaX < maxDistance && deltaY < maxDistance)
                    {
                        double deltaZ = elev - topo.elevsForCalcs[j, k];  
                        double distance = CalcDistanceBetweenPoints(gridUTMX, gridUTMY, UTMX, UTMY);                                        

                        if (distance <= radius && distance >= 0) // since exposures are calculated using an inverse distance, using grid point that is too close
                            // to the site can cause singularities in the calculations. Only using grid points that are at least 10 m from the site of interest
                        {
                            int dirInd = CalcDirInd(deltaX, deltaY, dirBin);
                            if (exponent != 1.0)
                                distance = Math.Pow(distance, exponent);                                                     

                            exposure[dirInd] = exposure[dirInd] + deltaZ / distance;
                            exposureDist[dirInd] = exposureDist[dirInd] + 1 / distance;

                     /*       if (dirInd >= 0)
                            {
                                sw.Write(gridUTMX + ",");
                                sw.Write(gridUTMY + ",");
                                sw.Write(topo.elevsForCalcs[j, k] + ",");
                                sw.Write(dirInd + ",");
                                sw.WriteLine();
                            }
*/
                        }
                    }
                }
            }

     //      sw.Close();

            for (int m = 0; m < numWD; m++)
                if (exposureDist[m] != 0) exposure[m] = exposure[m] / exposureDist[m];

            expoReturn.exponent = exponent;
            expoReturn.numSectors = numSectors;
            expoReturn.radius = radius;
            expoReturn.expoDist = exposureDist;
            expoReturn.expo = exposure;

            return expoReturn;
        }


        /// <summary> Returns radius one level lower in list of exposures. </summary>
        public int GetSmallerRadius(Exposure[] expo, int radius, double exponent, int numSectors)
        {            
            int smallerRadius = 0;
            int thisR;
            int thisCount = 0;

            try
            {
                thisCount = expo.Length;
            }
            catch 
            { }

            if (thisCount == 0)
                smallerRadius = 0;
            else
            {
                for (int i = 0; i < thisCount; i++)
                {
                    if (expo[i] == null)
                        break;

                    thisR = expo[i].radius;
                    if (expo[i].exponent == exponent && thisR < radius && thisR > smallerRadius && expo[i].numSectors == numSectors)
                        smallerRadius = thisR;

                }
            }

            return smallerRadius;
        }
        
        /// <summary> Returns exposure calculated with specified radius, exponent and numSectors. </summary>
        public Exposure GetSmallerRadiusExpo(Exposure[] expo, int smaller_radius, double exponent, int numSectors)
        {          
            Exposure thisExpo = null;
            int thisCount = 0;

            if (expo != null)            
                thisCount = expo.Length;            

            for (int i = 0; i < thisCount; i++)
            {                
                if (expo[i].exponent == exponent && expo[i].radius == smaller_radius && expo[i].numSectors == numSectors)
                {
                    thisExpo = expo[i];
                    break;
                }
            }

            return thisExpo;
        }


        /// <summary> Calculates the surface roughness and displacement height at specified UTMX/Y and updates referenced Exposure object. </summary>
        public void CalcSRDH(ref Exposure expo, double UTMX, double UTMY, int radius, double exponent, int numWD)
        {                                    
            if (numWD == 0)
            {
                MessageBox.Show("You need to import met files first.", "Continuum 3");
                return;
            }                        
                        
            double dirBin = (double)360 / numWD;

            expo.SR = new double[numWD];
            expo.SR_Dist = new double[numWD];
            expo.dispH = new double[numWD];

            // Find square that contains radius of interest
            int minX_Ind = Convert.ToInt16((UTMX - radius - LC_NumXY.X.calcs.min) / LC_NumXY.X.all.reso);
            int maxX_Ind = Convert.ToInt16((UTMX + radius - LC_NumXY.X.calcs.min) / LC_NumXY.X.all.reso);
            int minY_Ind = Convert.ToInt16((UTMY - radius - LC_NumXY.Y.calcs.min) / LC_NumXY.Y.all.reso);
            int maxY_Ind = Convert.ToInt16((UTMY + radius - LC_NumXY.Y.calcs.min) / LC_NumXY.Y.all.reso);                      

            double maxDistance = Convert.ToSingle(radius * 1.414);

            for (int j = minX_Ind; j <= maxX_Ind; j++)
            {
                for (int k = minY_Ind; k <= maxY_Ind; k++)
                {
                    double gridUTMX = Convert.ToInt32((j * LC_NumXY.X.all.reso) + LC_NumXY.X.calcs.min);
                    double gridUTMY = Convert.ToInt32((k * LC_NumXY.Y.all.reso) + LC_NumXY.Y.calcs.min);

                    double deltaX = gridUTMX - UTMX;
                    double deltaY = gridUTMY - UTMY;

                    if (deltaX < maxDistance && deltaY < maxDistance && j >= 0 && j < LC_NumXY.X.calcs.num && k >= 0 && k < LC_NumXY.Y.calcs.num)
                    {                        
                        double distance = CalcDistanceBetweenPoints(gridUTMX, gridUTMY, UTMX, UTMY);

                        if (distance <= radius && distance != 0)
                        {
                            int dirInd = CalcDirInd(deltaX, deltaY, dirBin);
                            double thisSR = SR_ForCalcs[j, k];
                            double This_Disp = DH_ForCalcs[j, k];                                                       

                            if (thisSR > 0)
                            {
                                if (exponent != 1.0)
                                    distance = Math.Pow(distance, exponent);

                                expo.SR[dirInd] = expo.SR[dirInd] + thisSR / distance;
                                expo.dispH[dirInd] = expo.dispH[dirInd] + This_Disp / distance;
                                expo.SR_Dist[dirInd] = expo.SR_Dist[dirInd] + 1 / distance;
                            }
                        }
                    }
                }
            }

            for (int m = 0; m < numWD; m++)
            {
                if (gotSR == true && expo.SR_Dist[m] != 0)
                {
                    expo.SR[m] = expo.SR[m] / expo.SR_Dist[m];
                    expo.dispH[m] = expo.dispH[m] / expo.SR_Dist[m];
                }
                else if (gotSR == true)
                {
                    expo.SR[m] = 0;
                    expo.dispH[m] = 0;
                }
            }
        }


        /// <summary> Calculates the surface roughness and displacement height at specified UTMX/Y using SR/DH from smaller radius and updates referenced Exposure object. </summary>
        public void CalcSRDHwithSmallerRadius(ref Exposure expo, double UTMX, double UTMY, int radius, double exponent, int numSectors, int smallerRadius, Exposure smallerExposure, int numWD)
        {            
            // TESTING EXPONENT = 1
            exponent = 1;
            
            if (numWD == 0)
            {
                MessageBox.Show("You need to import met files first.", "Continuum 2.2");
                return;
            }
                        
            double dirBin = (double)360 / numWD;
            expo.SR_Dist = new double[numWD];
            expo.SR = new double[numWD];
            expo.dispH = new double[numWD];                       

            // Find square that contains radius of interest
            int minX_Ind = Convert.ToInt16((UTMX - radius - LC_NumXY.X.calcs.min) / LC_NumXY.X.all.reso);
            int maxX_Ind = Convert.ToInt16((UTMX + radius - LC_NumXY.X.calcs.min) / LC_NumXY.X.all.reso);
            int minY_Ind = Convert.ToInt16((UTMY - radius - LC_NumXY.Y.calcs.min) / LC_NumXY.Y.all.reso);
            int maxY_Ind = Convert.ToInt16((UTMY + radius - LC_NumXY.Y.calcs.min) / LC_NumXY.Y.all.reso);

            // Find inner square that fits just inside the smaller radius
            int minX_IndInner = Convert.ToInt16((UTMX - (smallerRadius / 1.414) - LC_NumXY.X.calcs.min) / LC_NumXY.X.all.reso);
            int maxX_IndInner = Convert.ToInt16((UTMX + (smallerRadius / 1.414) - LC_NumXY.X.calcs.min) / LC_NumXY.X.all.reso);

            int minY_IndInner = Convert.ToInt16((UTMY - (smallerRadius / 1.414) - LC_NumXY.Y.calcs.min) / LC_NumXY.Y.all.reso);
            int maxY_IndInner = Convert.ToInt16((UTMY + (smallerRadius / 1.414) - LC_NumXY.Y.calcs.min) / LC_NumXY.Y.all.reso);

            double maxDistance = Convert.ToSingle(radius * 1.414);                    

            // First do calcs for left side of box
            for (int j = minX_Ind; j <= minX_IndInner; j++)
            {
                for (int k = minY_Ind; k <= maxY_Ind; k++)
                {
                    double gridUTMX = j * LC_NumXY.X.all.reso + LC_NumXY.X.calcs.min;
                    double gridUTMY = k * LC_NumXY.Y.all.reso + LC_NumXY.Y.calcs.min;

                    double deltaX = gridUTMX - UTMX;
                    double deltaY = gridUTMY - UTMY;

                    if (deltaX < maxDistance && deltaY < maxDistance && j >= 0 && j < LC_NumXY.X.calcs.num && k >= 0 && k < LC_NumXY.Y.calcs.num)
                    {
                        double thisSR = SR_ForCalcs[j, k];
                        double thisDH = DH_ForCalcs[j, k];                        
                      
                        double distance = CalcDistanceBetweenPoints(gridUTMX, gridUTMY, UTMX, UTMY);

                        if (distance <= radius && distance > smallerRadius && distance != 0)  // Zero values on edges can skew calculated values
                        {
                            int dirInd = CalcDirInd(deltaX, deltaY, dirBin);
                            if (exponent != 1.0)
                                distance = (double)Math.Pow(distance, exponent);

                            expo.SR[dirInd] = expo.SR[dirInd] + thisSR / distance;
                            expo.dispH[dirInd] = expo.dispH[dirInd] + thisDH / distance;
                            expo.SR_Dist[dirInd] = expo.SR_Dist[dirInd] + 1 / distance;

                        }
                    }
                }
            }

            // Now do calcs for right side of box
            for (int j = maxX_IndInner; j <= maxX_Ind; j++)
            {
                for (int k = minY_Ind; k <= maxY_Ind; k++)
                {
                    double gridUTMX = j * LC_NumXY.X.all.reso + LC_NumXY.X.calcs.min;
                    double gridUTMY = k * LC_NumXY.Y.all.reso + LC_NumXY.Y.calcs.min;

                    double deltaX = gridUTMX - UTMX;
                    double deltaY = gridUTMY - UTMY;

                    if (deltaX < maxDistance && deltaY < maxDistance && j >= 0 && j < LC_NumXY.X.calcs.num && k >= 0 && k < LC_NumXY.Y.calcs.num)
                    {
                        double thisSR = SR_ForCalcs[j, k];
                        double thisDH = DH_ForCalcs[j, k];                     
                        double distance = CalcDistanceBetweenPoints(gridUTMX, gridUTMY, UTMX, UTMY);                                               

                        if (distance <= radius && distance > smallerRadius && distance != 0)
                        {
                            int dirInd = CalcDirInd(deltaX, deltaY, dirBin);
                            if (exponent != 1.0)
                                distance = (double)Math.Pow(distance, exponent);

                            expo.SR[dirInd] = expo.SR[dirInd] + thisSR / distance;
                            expo.dispH[dirInd] = expo.dispH[dirInd] + thisDH / distance;
                            expo.SR_Dist[dirInd] = expo.SR_Dist[dirInd] + 1 / distance;

                        }
                    }
                }
            }

            // Now do calcs for bottom of box
            for (int j = minX_IndInner + 1; j <= maxX_IndInner - 1; j++)
            {
                for (int k = minY_Ind; k <= minY_IndInner; k++)
                {
                    double gridUTMX = j * LC_NumXY.X.all.reso + LC_NumXY.X.calcs.min;
                    double gridUTMY = k * LC_NumXY.Y.all.reso + LC_NumXY.Y.calcs.min;

                    double deltaX = gridUTMX - UTMX;
                    double deltaY = gridUTMY - UTMY;

                    if (deltaX < maxDistance && deltaY < maxDistance && j >= 0 && j < LC_NumXY.X.calcs.num && k >= 0 && k < LC_NumXY.Y.calcs.num)
                    {
                        double thisSR = SR_ForCalcs[j, k];
                        double thisDH = DH_ForCalcs[j, k];
                      
                        double distance = CalcDistanceBetweenPoints(gridUTMX, gridUTMY, UTMX, UTMY);                                               

                        if (distance <= radius && distance > smallerRadius && distance != 0)
                        {
                            int dirInd = CalcDirInd(deltaX, deltaY, dirBin);
                            if (exponent != 1.0)
                                distance = (double)Math.Pow(distance, exponent);

                            expo.SR[dirInd] = expo.SR[dirInd] + thisSR / distance;
                            expo.dispH[dirInd] = expo.dispH[dirInd] + thisDH / distance;
                            expo.SR_Dist[dirInd] = expo.SR_Dist[dirInd] + 1 / distance;
                        }
                    }
                }
            }

            // Now do calcs for top of box
            for (int j = minX_IndInner + 1; j <= maxX_IndInner - 1; j++)
            {
                for (int k = maxY_IndInner; k <= maxY_Ind; k++)
                {
                    double gridUTMX = j * LC_NumXY.X.all.reso + LC_NumXY.X.calcs.min;
                    double gridUTMY = k * LC_NumXY.Y.all.reso + LC_NumXY.Y.calcs.min;

                    double deltaX = gridUTMX - UTMX;
                    double deltaY = gridUTMY - UTMY;

                    if (deltaX < maxDistance && deltaY < maxDistance && j >= 0 && j < LC_NumXY.X.calcs.num && k >= 0 && k < LC_NumXY.Y.calcs.num)
                    {
                        double thisSR = SR_ForCalcs[j, k];
                        double thisDH = DH_ForCalcs[j, k];
                        
                        double distance = CalcDistanceBetweenPoints(gridUTMX, gridUTMY, UTMX, UTMY);                                               

                        if (distance <= radius && distance > smallerRadius && distance != 0)
                        {
                            int dirInd = CalcDirInd(deltaX, deltaY, dirBin);
                            if (exponent != 1.0)
                                distance = (double)Math.Pow(distance, exponent);

                            expo.SR[dirInd] = expo.SR[dirInd] + thisSR / distance;
                            expo.dispH[dirInd] = expo.dispH[dirInd] + thisDH / distance;
                            expo.SR_Dist[dirInd] = expo.SR_Dist[dirInd] + 1 / distance;

                        }
                    }
                }
            }

            // Now add surface roughness and displacement height and SR_Dist from smaller radius
            for (int m = 0; m < numWD; m++)
            {
                expo.SR[m] = expo.SR[m] + smallerExposure.SR[m] * smallerExposure.SR_Dist[m];
                expo.dispH[m] = expo.dispH[m] + smallerExposure.dispH[m] * smallerExposure.SR_Dist[m];
                expo.SR_Dist[m] = expo.SR_Dist[m] + smallerExposure.SR_Dist[m];
            }

            for (int m = 0; m < numWD; m++)
            {
                if (expo.SR_Dist[m] != 0)
                {
                    expo.SR[m] = expo.SR[m] / expo.SR_Dist[m];
                    expo.dispH[m] = expo.dispH[m] / expo.SR_Dist[m];
                }
                else
                {
                    expo.SR[m] = 0;
                    expo.dispH[m] = 0;
                }
            }
        }

        /// <summary> Calculates the exposure at specified UTMX/Y using exposure from smaller radius and returns Exposure object. </summary>        
        public Exposure CalcExposuresWithSmallerRadius(double UTMX, double UTMY, double elev, int radius, double exponent, int numSectors, int smallerRadius, Exposure smallerExposure, int numWD)
        {              
            Exposure expoReturn = new Exposure();

            if (numWD == 0)
            {
                MessageBox.Show("You need to import met files first.", "Continuum 2.2");
                return expoReturn;
            }
                        
            double dirBin = (double)360 / numWD;

            double[] exposure = new double[numWD];
            double[] exposureDist = new double[numWD];

            // Find square that contains radius of interest
            int minX_Ind = Convert.ToInt16((UTMX - radius - topoNumXY.X.calcs.min) / topoNumXY.X.all.reso);
            int maxX_Ind = Convert.ToInt16((UTMX + radius - topoNumXY.X.calcs.min) / topoNumXY.X.all.reso);
            int minY_Ind = Convert.ToInt16((UTMY - radius - topoNumXY.Y.calcs.min) / topoNumXY.Y.all.reso);
            int maxY_Ind = Convert.ToInt16((UTMY + radius - topoNumXY.Y.calcs.min) / topoNumXY.Y.all.reso);

            // Find inner square that fits just inside the smaller radius
            int minX_IndInner = Convert.ToInt16((UTMX - (smallerRadius / 1.414) - topoNumXY.X.calcs.min) / topoNumXY.X.all.reso);
            int maxX_IndInner = Convert.ToInt16((UTMX + (smallerRadius / 1.414) - topoNumXY.X.calcs.min) / topoNumXY.X.all.reso);

            int minY_IndInner = Convert.ToInt16((UTMY - (smallerRadius / 1.414) - topoNumXY.Y.calcs.min) / topoNumXY.Y.all.reso);
            int maxY_IndInner = Convert.ToInt16((UTMY + (smallerRadius / 1.414) - topoNumXY.Y.calcs.min) / topoNumXY.Y.all.reso);

            double maxDistance = Convert.ToSingle(radius * 1.414);

            int stepInt;
            if (topoNumXY.X.all.reso < 30)
                stepInt = (int)(30 / topoNumXY.X.all.reso);
            else
                stepInt = 1;

            if (minX_Ind < 0) minX_Ind = 0;
            if (minY_Ind < 0) minY_Ind = 0;

            // Only doing calculations for square with radius of interest minus the inner square that first inside smaller radius

            // First do calcs for left side of box
            for (int j = minX_Ind; j < minX_IndInner; j = j + stepInt)
            {
                for (int k = minY_Ind; k <= maxY_Ind; k = k + stepInt)
                {
                    double gridUTMX = j * topoNumXY.X.all.reso + topoNumXY.X.calcs.min;
                    double gridUTMY = k * topoNumXY.Y.all.reso + topoNumXY.Y.calcs.min;
                    double deltaX = gridUTMX - UTMX;
                    double deltaY = gridUTMY - UTMY;

                    if (deltaX < maxDistance && deltaY < maxDistance)
                    {
                        double deltaZ = elev - elevsForCalcs[j, k];
                        double distance = CalcDistanceBetweenPoints(gridUTMX, gridUTMY, UTMX, UTMY);

                        if (distance <= radius && distance > smallerRadius && distance != 0)
                        {
                            if (exponent != 1.0)
                                distance = Math.Pow(distance, exponent);

                            int dirInd = CalcDirInd(deltaX, deltaY, dirBin);
                            exposure[dirInd] = exposure[dirInd] + deltaZ / distance;
                            exposureDist[dirInd] = exposureDist[dirInd] + 1 / distance;
                        }
                    }
                }
            }

            // Now do calcs for(int right side of box
            for (int j = maxX_IndInner; j < maxX_Ind; j = j + stepInt)
            {
                for (int k = minY_Ind; k <= maxY_Ind; k = k + stepInt)
                {
                    double  gridUTMX = j * topoNumXY.X.all.reso + topoNumXY.X.calcs.min;
                    double gridUTMY = k * topoNumXY.Y.all.reso + topoNumXY.Y.calcs.min;
                    double deltaX = gridUTMX - UTMX;
                    double deltaY = gridUTMY - UTMY;

                    if (deltaX < maxDistance && deltaY < maxDistance)
                    {
                        double deltaZ = elev - elevsForCalcs[j, k];
                        double distance = CalcDistanceBetweenPoints(gridUTMX, gridUTMY, UTMX, UTMY);

                        if (distance <= radius && distance > smallerRadius && distance != 0)
                        {
                            if (exponent != 1.0)
                                distance = Math.Pow(distance, exponent);

                            int dirInd = CalcDirInd(deltaX, deltaY, dirBin);
                            exposure[dirInd] = exposure[dirInd] + deltaZ / distance;
                            exposureDist[dirInd] = exposureDist[dirInd] + 1 / distance;
                        }
                    }
                }
            }

            // Now do calcs for bottom of box
            for (int j = minX_IndInner + 1; j < maxX_IndInner - 1; j = j + stepInt)  // no Single-dipping
            {
                for (int k = minY_Ind; k <= minY_IndInner; k = k + stepInt)
                {
                    double gridUTMX = j * topoNumXY.X.all.reso + topoNumXY.X.calcs.min;
                    double gridUTMY = k * topoNumXY.Y.all.reso + topoNumXY.Y.calcs.min;
                    double deltaX = gridUTMX - UTMX;
                    double deltaY = gridUTMY - UTMY;

                    if (deltaX < maxDistance && deltaY < maxDistance)
                    {
                        double deltaZ = elev - elevsForCalcs[j, k];
                        double distance = CalcDistanceBetweenPoints(gridUTMX, gridUTMY, UTMX, UTMY);

                        if (distance <= radius && distance > smallerRadius && distance != 0)
                        {
                            if (exponent != 1.0)
                                distance = Math.Pow(distance, exponent);

                            int dirInd = CalcDirInd(deltaX, deltaY, dirBin);
                            exposure[dirInd] = exposure[dirInd] + deltaZ / distance;
                            exposureDist[dirInd] = exposureDist[dirInd] + 1 / distance;
                        }
                    }
                }
            }

            // Now do calcs for(int top of box
            for (int j = minX_IndInner + 1; j < maxX_IndInner - 1; j = j + stepInt)
            {
                for (int k = maxY_IndInner; k <= maxY_Ind; k = k + stepInt)
                {
                    double gridUTMX = j * topoNumXY.X.all.reso + topoNumXY.X.calcs.min;
                    double gridUTMY = k * topoNumXY.Y.all.reso + topoNumXY.Y.calcs.min;
                    double deltaX = gridUTMX - UTMX;
                    double deltaY = gridUTMY - UTMY;

                    if (deltaX < maxDistance && deltaY < maxDistance)
                    {
                        double deltaZ = elev - elevsForCalcs[j, k];
                        double distance = CalcDistanceBetweenPoints(gridUTMX, gridUTMY, UTMX, UTMY);

                        if (distance <= radius && distance > smallerRadius && distance != 0)
                        {
                            if (exponent != 1.0)
                                distance = (double)Math.Pow(distance, exponent);

                            int dirInd = CalcDirInd(deltaX, deltaY, dirBin);
                            exposure[dirInd] = exposure[dirInd] + deltaZ / distance;
                            exposureDist[dirInd] = exposureDist[dirInd] + 1 / distance;
                        }
                    }
                }
            }

            // Now add Expo && ExpoDist from smaller radius
            for (int m = 0; m <= numWD - 1; m++)
            {
                exposure[m] = exposure[m] + smallerExposure.expo[m] * smallerExposure.expoDist[m];
                exposureDist[m] = exposureDist[m] + smallerExposure.expoDist[m];
            }

            for (int m = 0; m <= numWD - 1; m++)
                if (exposureDist[m] != 0)
                    exposure[m] = exposure[m] / exposureDist[m];
            
            expoReturn.exponent = exponent;
            expoReturn.numSectors = numSectors;
            expoReturn.radius = radius;
            expoReturn.expoDist = exposureDist;
            expoReturn.expo = exposure;

            return expoReturn;

        }

        /// <summary> Clears the topo and LC All/Plot/Calcs X/Y Min/Max/Num/reso values. </summary>        
        public void ClearXY_Info(ref Min_Max_Num_XYs thisXY_Info)
        {            
            thisXY_Info.X.all.min = 0;
            thisXY_Info.X.all.max = 0;
            thisXY_Info.X.all.num = 0;
            thisXY_Info.X.all.reso = 0;

            thisXY_Info.Y.all.min = 0;
            thisXY_Info.Y.all.max = 0;
            thisXY_Info.Y.all.num = 0;
            thisXY_Info.Y.all.reso = 0;

            thisXY_Info.X.calcs.min = 0;
            thisXY_Info.X.calcs.max = 0;
            thisXY_Info.X.calcs.num = 0;
            thisXY_Info.X.calcs.reso = 0;

            thisXY_Info.Y.calcs.min = 0;
            thisXY_Info.Y.calcs.max = 0;
            thisXY_Info.Y.calcs.num = 0;
            thisXY_Info.Y.calcs.reso = 0;

            thisXY_Info.X.plot.min = 0;
            thisXY_Info.X.plot.max = 0;
            thisXY_Info.X.plot.num = 0;
            thisXY_Info.X.plot.reso = 0;

            thisXY_Info.Y.plot.min = 0;
            thisXY_Info.Y.plot.max = 0;
            thisXY_Info.Y.plot.num = 0;
            thisXY_Info.Y.plot.reso = 0;
        }

        
        /// <summary> Clears TopoInfo object. </summary>        
        public void ClearAll(ref Continuum continuum)
        {            
            ClearXY_Info(ref topoNumXY);
            ClearXY_Info(ref LC_NumXY);

            gotTopo = false;
            gotSR = false;
            useSR = false;
            topoElevs = null;
            elevsForCalcs = null;
            SR_ForCalcs = null;
            DH_ForCalcs = null;
            landCover = null;

            continuum.turbineList.ClearAllCalcs();
            continuum.ChangesMade();

            Update update = new Update();
            update.TopoMap(continuum);
            update.ClearMapsPlotsAndTables(continuum);
            update.ClearStats(continuum);

        }

        /// <summary> If more than 500,000 data points, the topo/LC data is decimated for the plot on Input and Advanced tab. </summary>        
        public void DecimateForPlot(string Topo_or_LC)
        {             
            int decInd = 1;

            if (Topo_or_LC == "topo")
            {
                topoNumXY.X.plot.num = topoElevs.GetUpperBound(0);
                topoNumXY.Y.plot.num = topoElevs.GetUpperBound(1);
                topoNumXY.X.plot.min = topoNumXY.X.all.min;
                topoNumXY.X.plot.max = topoNumXY.X.all.max;
                topoNumXY.Y.plot.min = topoNumXY.Y.all.min;
                topoNumXY.Y.plot.max = topoNumXY.Y.all.max;

                int numX_Before = topoNumXY.X.plot.num;
                int numY_Before = topoNumXY.Y.plot.num;

                int totalPoints = topoElevs.Length;
                int maxPoints = 500000;

                if (totalPoints > maxPoints) {
                    // need to decimate for plotting or won't have enough memory
                    decInd = decInd + 1;
                    totalPoints = numX_Before / decInd * numY_Before / decInd;

                    while (totalPoints > maxPoints) {
                        decInd = decInd + 1;
                        totalPoints = numX_Before / decInd * numY_Before / decInd;
                    }

                    topoNumXY.X.plot.num = numX_Before / decInd;
                    topoNumXY.Y.plot.num = numY_Before / decInd;
                }

                double[,] topoElevsDec = new double[topoNumXY.X.plot.num, topoNumXY.Y.plot.num];
                topoNumXY.X.plot.reso = topoNumXY.X.all.reso * decInd;
                topoNumXY.Y.plot.reso = topoNumXY.Y.all.reso * decInd;

                int X_Ind = 0;
                int Y_Ind = 0;

                for (int i = 0; i < numX_Before; i = i + decInd)
                {
                    for (int j = 0; j < numY_Before; j = j + decInd)
                    {
                        topoElevsDec[X_Ind, Y_Ind] = topoElevs[i, j];

                        Y_Ind = Y_Ind + 1;
                        if (Y_Ind >= topoNumXY.Y.plot.num) break;

                    }

                    X_Ind = X_Ind + 1;
                    if (X_Ind >= topoNumXY.X.plot.num) break;

                    Y_Ind = 0;
                }

                topoElevs = topoElevsDec;
            }
            else {
                
                LC_NumXY.X.plot.num = landCover.GetUpperBound(0);
                LC_NumXY.Y.plot.num = landCover.GetUpperBound(1);
                LC_NumXY.X.plot.min = LC_NumXY.X.all.min;
                LC_NumXY.X.plot.max = LC_NumXY.X.all.max;
                LC_NumXY.Y.plot.min = LC_NumXY.Y.all.min;
                LC_NumXY.Y.plot.max = LC_NumXY.Y.all.max;

                int numX_Before = LC_NumXY.X.plot.num;
                int numY_Before = LC_NumXY.Y.plot.num;

                int totalPoints = landCover.Length;
                int maxPoints = 500000;

                if (totalPoints > maxPoints)
                {
                    // need to decimate for plotting or won't have enough memory
                    decInd = decInd + 1;
                    totalPoints = numX_Before / decInd * numY_Before / decInd;

                    while (totalPoints > maxPoints)
                    {
                        decInd = decInd + 1;
                        totalPoints = numX_Before / decInd * numY_Before / decInd;
                    }

                    LC_NumXY.X.plot.num = numX_Before / decInd;
                    LC_NumXY.Y.plot.num = numY_Before / decInd;
                }

                int[,] landCoverDec = new int[LC_NumXY.X.plot.num, LC_NumXY.Y.plot.num];
                LC_NumXY.X.plot.reso = LC_NumXY.X.all.reso * decInd;
                LC_NumXY.Y.plot.reso = LC_NumXY.Y.all.reso * decInd;

                int X_Ind = 0;
                int Y_Ind = 0;

                for (int i = 0; i < numX_Before; i = i + decInd)
                {
                    for (int j = 0; j < numY_Before; j = j + decInd)
                    {
                        landCoverDec[X_Ind, Y_Ind] = landCover[i, j];

                        Y_Ind = Y_Ind + 1;
                        if (Y_Ind >= LC_NumXY.Y.plot.num) break;

                    }

                    X_Ind = X_Ind + 1;
                    if (X_Ind >= LC_NumXY.X.plot.num) break;

                    Y_Ind = 0;
                }

                landCover = landCoverDec;
            }
        }


        /// <summary> Calculates the min and max UTMX and UTMY needed for calculations. </summary>
        public void GetMinMaxUTM_forCalcs(Continuum thisInst, Map thisMap, bool allNodesInDB)
        { 
            // thisMap should be null if a map is not being generated.

            Min_Max_Num_XYs bounds = new Min_Max_Num_XYs();
            bounds.X.calcs.min = 10000000;
            bounds.X.calcs.max = 0;
            bounds.Y.calcs.min = 10000000;
            bounds.Y.calcs.max = 0;
            bounds.X.calcs.reso = bounds.X.all.reso;
            bounds.Y.calcs.reso = bounds.Y.all.reso;

            // Find the min / max UTM X/Y of met sites
            if (thisInst.metList.ThisCount > 0)
            {
                for (int i = 0; i < thisInst.metList.ThisCount; i++)
                {
                    Met thisMet = thisInst.metList.metItem[i];
                    if (thisMet.UTMX < bounds.X.calcs.min) bounds.X.calcs.min = thisMet.UTMX;
                    if (thisMet.UTMX > bounds.X.calcs.max) bounds.X.calcs.max = thisMet.UTMX;
                    if (thisMet.UTMY < bounds.Y.calcs.min) bounds.Y.calcs.min = thisMet.UTMY;
                    if (thisMet.UTMY > bounds.Y.calcs.max) bounds.Y.calcs.max = thisMet.UTMY;
                }
            }

            // Find the min / max UTM X/Y of turbine sites
            if (thisInst.turbineList.TurbineCount > 0 && thisMap == null) // only consider turbine locations if a map is not being generated
            {
                for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
                {
                    Turbine thisTurbine = thisInst.turbineList.turbineEsts[i];
                    if (thisTurbine.UTMX < bounds.X.calcs.min) bounds.X.calcs.min = thisTurbine.UTMX;
                    if (thisTurbine.UTMX > bounds.X.calcs.max) bounds.X.calcs.max = thisTurbine.UTMX;
                    if (thisTurbine.UTMY < bounds.Y.calcs.min) bounds.Y.calcs.min = thisTurbine.UTMY;
                    if (thisTurbine.UTMY > bounds.Y.calcs.max) bounds.Y.calcs.max = thisTurbine.UTMY;
                }
            }

            // Find the min / max UTM X/Y of map
            if (thisMap != null)
            {
                if (thisMap.minUTMX < bounds.X.calcs.min) bounds.X.calcs.min = thisMap.minUTMX;
                if ((thisMap.minUTMX + thisMap.numX * thisMap.reso) > bounds.X.calcs.max) bounds.X.calcs.max = (thisMap.minUTMX + thisMap.numX * thisMap.reso);
                if (thisMap.minUTMY < bounds.Y.calcs.min) bounds.Y.calcs.min = thisMap.minUTMY;
                if ((thisMap.minUTMY + thisMap.numY * thisMap.reso) > bounds.Y.calcs.max) bounds.Y.calcs.max = (thisMap.minUTMY + thisMap.numY * thisMap.reso);
            }

            // Find min/max UTMX/Y stored in database if allNodesInDB is true
            if (allNodesInDB == true)
            {
                Min_Max_Num_XYs DB_MinMax = GetMinMaxXY_InDB(thisInst);
                if (DB_MinMax.X.all.min < bounds.X.calcs.min) bounds.X.calcs.min = DB_MinMax.X.all.min;
                if (DB_MinMax.X.all.max > bounds.X.calcs.max) bounds.X.calcs.max = DB_MinMax.X.all.max;
                if (DB_MinMax.Y.all.min < bounds.Y.calcs.min) bounds.Y.calcs.min = DB_MinMax.Y.all.min;
                if (DB_MinMax.Y.all.max > bounds.Y.calcs.max) bounds.Y.calcs.max = DB_MinMax.Y.all.max;
            }

            // Find min/max of zone sites
            for (int i = 0; i < thisInst.siteSuitability.zones.Length; i++)
            {
                SiteSuitability.Zone thisZone = thisInst.siteSuitability.zones[i];
                UTM_conversion.UTM_coords theseUTM = thisInst.UTM_conversions.LLtoUTM(thisZone.latitude, thisZone.longitude);

                if (theseUTM.UTMEasting < bounds.X.calcs.min) bounds.X.calcs.min = theseUTM.UTMEasting;
                if (theseUTM.UTMEasting > bounds.X.calcs.max) bounds.X.calcs.max = theseUTM.UTMEasting;
                if (theseUTM.UTMNorthing < bounds.Y.calcs.min) bounds.Y.calcs.min = theseUTM.UTMNorthing;
                if (theseUTM.UTMNorthing > bounds.Y.calcs.max) bounds.Y.calcs.max = theseUTM.UTMNorthing;
            }

            Grid_Info gridStat = new Grid_Info();
            int gridRadius = gridStat.gridRadius;
            int maxRadius = thisInst.radiiList.GetMaxRadius();                     

            // Adding an additional 4000 m for path of nodes to be able to curve out
            bounds.X.calcs.min = bounds.X.calcs.min - maxRadius - gridRadius - 4000; 
            bounds.Y.calcs.min = bounds.Y.calcs.min - maxRadius - gridRadius - 4000;
            bounds.X.calcs.max = bounds.X.calcs.max + maxRadius + gridRadius + 4000;
            bounds.Y.calcs.max = bounds.Y.calcs.max + maxRadius + gridRadius + 4000;
                       
            // Make sure the bounds are within topo bounds and assign to topo Calcs
            if (bounds.X.calcs.min < topoNumXY.X.all.min) bounds.X.calcs.min = topoNumXY.X.all.min;
            topoNumXY.X.calcs.min = bounds.X.calcs.min;

            if (bounds.X.calcs.max > topoNumXY.X.all.max) bounds.X.calcs.max = topoNumXY.X.all.max;
            topoNumXY.X.calcs.max = bounds.X.calcs.max;

            if (bounds.Y.calcs.min < topoNumXY.Y.all.min) bounds.Y.calcs.min = topoNumXY.Y.all.min;
            topoNumXY.Y.calcs.min = bounds.Y.calcs.min;

            if (bounds.Y.calcs.max > topoNumXY.Y.all.max) bounds.Y.calcs.max = topoNumXY.Y.all.max;
            topoNumXY.Y.calcs.max = bounds.Y.calcs.max;

            topoNumXY.X.calcs.reso = topoNumXY.X.all.reso;
            topoNumXY.Y.calcs.reso = topoNumXY.Y.all.reso;

            if (gotSR == true)
            {
                LC_NumXY.X.calcs.min = bounds.X.calcs.min;
                LC_NumXY.Y.calcs.min = bounds.Y.calcs.min;
                LC_NumXY.X.calcs.max = bounds.X.calcs.max;
                LC_NumXY.Y.calcs.max = bounds.Y.calcs.max;
                LC_NumXY.X.calcs.reso = LC_NumXY.X.all.reso;
                LC_NumXY.Y.calcs.reso = LC_NumXY.Y.all.reso;
            }

            TopoGrid minXY = new TopoGrid();
            TopoGrid maxXY = new TopoGrid();

            // Min / Max UTM for topography data
            minXY = GetClosestNode(topoNumXY.X.calcs.min, topoNumXY.Y.calcs.min, "Topography");
            topoNumXY.X.calcs.min = minXY.UTMX;
            topoNumXY.Y.calcs.min = minXY.UTMY;

            if (topoNumXY.X.calcs.min < topoNumXY.X.all.min) topoNumXY.X.calcs.min = topoNumXY.X.all.min;
            if (topoNumXY.Y.calcs.min < topoNumXY.Y.all.min) topoNumXY.Y.calcs.min = topoNumXY.Y.all.min;

            maxXY = GetClosestNode(topoNumXY.X.calcs.max, topoNumXY.Y.calcs.max, "Topography");

            topoNumXY.X.calcs.max = maxXY.UTMX;
            topoNumXY.Y.calcs.max = maxXY.UTMY;

            if (topoNumXY.X.calcs.max > topoNumXY.X.all.max) topoNumXY.X.calcs.max = topoNumXY.X.all.max;
            if (topoNumXY.Y.calcs.max > topoNumXY.Y.all.max) topoNumXY.Y.calcs.max = topoNumXY.Y.all.max;

            topoNumXY.X.calcs.num = (int)((topoNumXY.X.calcs.max - topoNumXY.X.calcs.min) / topoNumXY.X.calcs.reso + 1);
            topoNumXY.Y.calcs.num = (int)((topoNumXY.Y.calcs.max - topoNumXY.Y.calcs.min) / topoNumXY.Y.calcs.reso + 1);

            if (gotSR == true)
            {
                // Min / Max UTM for(int  land cover data
                minXY = GetClosestNode(LC_NumXY.X.calcs.min, LC_NumXY.Y.calcs.min, "Land Cover");
                LC_NumXY.X.calcs.min = minXY.UTMX;
                LC_NumXY.Y.calcs.min = minXY.UTMY;

                if (LC_NumXY.X.calcs.min < LC_NumXY.X.all.min) LC_NumXY.X.calcs.min = LC_NumXY.X.all.min;
                if (LC_NumXY.Y.calcs.min < LC_NumXY.Y.all.min) LC_NumXY.Y.calcs.min = LC_NumXY.Y.all.min;

                maxXY = GetClosestNode(LC_NumXY.X.calcs.max, LC_NumXY.Y.calcs.max, "Land Cover");
                LC_NumXY.X.calcs.max = maxXY.UTMX;
                LC_NumXY.Y.calcs.max = maxXY.UTMY;

                if (LC_NumXY.X.calcs.max > LC_NumXY.X.all.max) LC_NumXY.X.calcs.max = LC_NumXY.X.all.max;
                if (LC_NumXY.Y.calcs.max > LC_NumXY.Y.all.max) LC_NumXY.Y.calcs.max = LC_NumXY.Y.all.max;

                LC_NumXY.X.calcs.num = (int)((LC_NumXY.X.calcs.max - LC_NumXY.X.calcs.min) / LC_NumXY.X.calcs.reso + 1);
                LC_NumXY.Y.calcs.num = (int)((LC_NumXY.Y.calcs.max - LC_NumXY.Y.calcs.min) / LC_NumXY.Y.calcs.reso + 1);
            }
            
        }
        
        /// <summary> Gets minimum and maximum UTMX/Y of nodes in database. </summary>        
        public Min_Max_Num_XYs GetMinMaxXY_InDB(Continuum thisInst)
        {
            Min_Max_Num_XYs theseMinMax = new Min_Max_Num_XYs();
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            try
            {
                using (var context = new Continuum_EDMContainer(connString))
                {
                    var node_db = from N in context.Node_table.Include("expo") select N;

                    foreach (var N in node_db)
                    {
                        if (theseMinMax.X.all.min == 0 || N.UTMX < theseMinMax.X.all.min)
                            theseMinMax.X.all.min = N.UTMX;

                        if (theseMinMax.X.all.max == 0 || N.UTMX > theseMinMax.X.all.max)
                            theseMinMax.X.all.max = N.UTMX;

                        if (theseMinMax.Y.all.min == 0 || N.UTMY < theseMinMax.Y.all.min)
                            theseMinMax.Y.all.min = N.UTMY;

                        if (theseMinMax.Y.all.max == 0 || N.UTMY < theseMinMax.Y.all.max)
                            theseMinMax.Y.all.max = N.UTMY;
                        
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return theseMinMax;
            }        

            return theseMinMax;
        }

        /// <summary> Returns X/Y indices associated with specified UTMX/Y and either topography or land cover data and using either 'all' data or decimated 'plot' data. </summary>
        public int[] GetXYIndices(string topoOrLC, double UTMX, double UTMY, string allPlotOrCalcs)
        {            
            // Returns indices = -888 if error thrown
            int[] indices = new int[2]; // 0: X, 1: Y
                       
            if (topoOrLC == "Topo")
            {
                if (allPlotOrCalcs == "All")
                {
                    if (topoNumXY.X.all.reso == 0)
                    {
                        indices[0] = -999;
                        indices[1] = -999;
                        return indices;
                    }

                    try
                    {
                        indices[0] = Convert.ToInt16((UTMX - topoNumXY.X.all.min) / topoNumXY.X.all.reso);
                        indices[1] = Convert.ToInt16((UTMY - topoNumXY.Y.all.min) / topoNumXY.Y.all.reso);
                    }
                    catch
                    {
                        indices[0] = -888;
                        indices[1] = -888;
                        return indices;
                    }
                }
                else if (allPlotOrCalcs == "Calcs")
                {
                    if (topoNumXY.X.calcs.reso == 0)
                    {
                        indices[0] = -999;
                        indices[1] = -999;
                        return indices;
                    }

                    try
                    {
                        indices[0] = Convert.ToInt16((UTMX - topoNumXY.X.calcs.min) / topoNumXY.X.calcs.reso);
                        indices[1] = Convert.ToInt16((UTMY - topoNumXY.Y.calcs.min) / topoNumXY.Y.calcs.reso);
                    }
                    catch
                    {
                        indices[0] = -888;
                        indices[1] = -888;
                        return indices;
                    }
                    
                }
                else if (allPlotOrCalcs == "Plot")
                {
                    if (topoNumXY.X.plot.reso == 0)
                    {
                        indices[0] = -999;
                        indices[1] = -999;
                        return indices;
                    }

                    try
                    {
                        indices[0] = Convert.ToInt16((UTMX - topoNumXY.X.plot.min) / topoNumXY.X.plot.reso);
                        indices[1] = Convert.ToInt16((UTMY - topoNumXY.Y.plot.min) / topoNumXY.Y.plot.reso);
                    }
                    catch
                    {
                        indices[0] = -888;
                        indices[1] = -888;
                        return indices;
                    }

                }
            }
            else
            {
                if (allPlotOrCalcs == "All")
                {
                    if (LC_NumXY.X.all.reso == 0)
                    {
                        indices[0] = -999;
                        indices[1] = -999;
                        return indices;
                    }

                    try
                    {
                        indices[0] = Convert.ToInt16((UTMX - LC_NumXY.X.all.min) / LC_NumXY.X.all.reso);
                        indices[1] = Convert.ToInt16((UTMY - LC_NumXY.Y.all.min) / LC_NumXY.Y.all.reso);
                    }
                    catch
                    {
                        indices[0] = -888;
                        indices[1] = -888;
                        return indices;
                    }

                }
                else if (allPlotOrCalcs == "Calcs")
                {
                    if (LC_NumXY.X.calcs.reso == 0)
                    {
                        indices[0] = -999;
                        indices[1] = -999;
                        return indices;
                    }

                    try
                    {
                        indices[0] = Convert.ToInt16((UTMX - LC_NumXY.X.calcs.min) / LC_NumXY.X.calcs.reso);
                        indices[1] = Convert.ToInt16((UTMY - LC_NumXY.Y.calcs.min) / LC_NumXY.Y.calcs.reso);
                    }
                    catch
                    {
                        indices[0] = -888;
                        indices[1] = -888;
                        return indices;
                    }

                }
                else if (allPlotOrCalcs == "Plot")
                {
                    if (LC_NumXY.X.plot.reso == 0)
                    {
                        indices[0] = -999;
                        indices[1] = -999;
                        return indices;
                    }

                    try
                    {
                        indices[0] = Convert.ToInt16((UTMX - LC_NumXY.X.plot.min) / LC_NumXY.X.plot.reso);
                        indices[1] = Convert.ToInt16((UTMY - LC_NumXY.Y.plot.min) / LC_NumXY.Y.plot.reso);
                    }
                    catch
                    {
                        indices[0] = -888;
                        indices[1] = -888;
                        return indices;
                    }
                    
                }
            }

            return indices;

        }

        /// <summary> Retrieves elevations from database and gets surface roughness and displacement height for calcs using LC key. </summary>        
        public void GetElevsAndSRDH_ForCalcs(Continuum thisInst, Map thisMap, bool allNodesInDB)
        {            
            if (gotTopo == true && (thisInst.metList.ThisCount > 0 || thisInst.turbineList.TurbineCount > 0 || thisInst.siteSuitability.GetNumZones() > 0))
            {                
                GetMinMaxUTM_forCalcs(thisInst, thisMap, allNodesInDB);                

                NodeCollection nodeList = new NodeCollection();                
                string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

                topoNumXY.X.calcs.num = Convert.ToInt32((topoNumXY.X.calcs.max - topoNumXY.X.calcs.min) / topoNumXY.X.all.reso) + 1;
                topoNumXY.Y.calcs.num = Convert.ToInt32((topoNumXY.Y.calcs.max - topoNumXY.Y.calcs.min) / topoNumXY.Y.all.reso) + 1;
                topoNumXY.X.calcs.reso = topoNumXY.X.all.reso;
                topoNumXY.Y.calcs.reso = topoNumXY.Y.all.reso;

                if (gotSR == true)
                {
                    LC_NumXY.X.calcs.num = Convert.ToInt32((LC_NumXY.X.calcs.max - LC_NumXY.X.calcs.min) / LC_NumXY.X.all.reso) + 1;
                    LC_NumXY.Y.calcs.num = Convert.ToInt32((LC_NumXY.Y.calcs.max - LC_NumXY.Y.calcs.min) / LC_NumXY.Y.all.reso) + 1;
                    LC_NumXY.X.calcs.reso = LC_NumXY.X.all.reso;
                    LC_NumXY.Y.calcs.reso = LC_NumXY.Y.all.reso;
                }

                if (topoNumXY.X.calcs.num < 0 || topoNumXY.Y.calcs.num < 0 || LC_NumXY.X.calcs.num < 0 || LC_NumXY.Y.calcs.num < 0)
                {
                    MessageBox.Show("Error reading in data. Check that the UTM datum for the elevation and land cover/surface roughness are the same.", "Continuum 2.2");
                    return;
                }

                elevsForCalcs = new double[topoNumXY.X.calcs.num, topoNumXY.Y.calcs.num];                
                BinaryFormatter bin = new BinaryFormatter();
                // First grab elevation data
                try
                {
                    using (var ctx = new Continuum_EDMContainer(connString))
                    {  
                        int minX_Id = (int)Math.Round(((topoNumXY.X.calcs.min - topoNumXY.X.all.min) / topoNumXY.X.all.reso), 0) + 1; // Id starts at 1
                        int maxX_Id = (int)Math.Round(((topoNumXY.X.calcs.max - topoNumXY.X.all.min) / topoNumXY.X.all.reso), 0) + 1;
                                                
                        var topoDB_Query = from N in ctx.Topo_table where N.Id >= minX_Id && N.Id <= maxX_Id select N;
                            
                        foreach (var N in topoDB_Query)
                        {
                            MemoryStream MS1 = new MemoryStream(N.Elevs);                            
                            float[] theseElevs = (float[])bin.Deserialize(MS1);
                            
                            int X_IndAll = N.Id - 1;
                            int X_IndCalcs = X_IndAll + (int)Math.Round(((topoNumXY.X.all.min - topoNumXY.X.calcs.min) / topoNumXY.X.all.reso),0);
                            int Y_IndStart = (int)Math.Round((topoNumXY.Y.calcs.min - topoNumXY.Y.all.min) / topoNumXY.Y.all.reso,0);
                            int Y_IndStop = (int)Math.Round((topoNumXY.Y.calcs.max - topoNumXY.Y.all.min) / topoNumXY.Y.all.reso, 0);

                            for (int j = Y_IndStart; j <= Y_IndStop; j++)
                            {
                                int Y_IndAll = j;
                                int Y_IndCalcs = Y_IndAll + (int)Math.Round((topoNumXY.Y.all.min - topoNumXY.Y.calcs.min) / topoNumXY.Y.all.reso, 0);

                                elevsForCalcs[X_IndCalcs, Y_IndCalcs] = theseElevs[j];
                            }

                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }

                // DEBUGGING
                /*         StreamWriter sw = new StreamWriter("C:\\Users\\OEE2017_32\\Dropbox (OEE)\\Software - Development\\Continuum\\v3.0\\QA Backup files\\Testing 3.0\\RR Calcs debugging\\Elevs_for_Calcs_Mets_475_3350_no_turbs.csv");
                         for (int i = 0; i < elevsForCalcs.GetUpperBound(0); i++)
                             for (int j = 0; j < elevsForCalcs.GetUpperBound(1); j++)
                             {
                                 double thisX = topoNumXY.X.calcs.min + i * topoNumXY.X.calcs.reso;
                                 double thisY = topoNumXY.Y.calcs.min + j * topoNumXY.Y.calcs.reso;
                                 sw.Write(thisX + ",");
                                 sw.Write(thisY + ",");
                                 sw.Write(elevsForCalcs[i, j]);
                                 sw.WriteLine();
                             }

                         sw.Close();
                         */
                if (gotSR == true)
                {   
                    SR_ForCalcs = new double[LC_NumXY.X.calcs.num, LC_NumXY.Y.calcs.num];
                    DH_ForCalcs = new double[LC_NumXY.X.calcs.num, LC_NumXY.Y.calcs.num];
                    int[,] landCoverForCalcs = new int[LC_NumXY.X.calcs.num, LC_NumXY.Y.calcs.num];
                    
                    // Grab land cover data from DB
                    try
                    {
                        using (var ctx = new Continuum_EDMContainer(connString))
                        {
                            int minX_Id = (int)Math.Round(((LC_NumXY.X.calcs.min - LC_NumXY.X.all.min) / LC_NumXY.X.all.reso),0) + 1;
                            int maxX_Id = (int)Math.Round(((LC_NumXY.X.calcs.max - LC_NumXY.X.all.min) / LC_NumXY.X.all.reso),0) + 1;                                                      

                            var landCoverDB_Query = from N in ctx.LandCover_table where N.Id >= minX_Id && N.Id <= maxX_Id select N;

                            foreach (var N in landCoverDB_Query)
                            {
                                MemoryStream MS1 = new MemoryStream(N.LandCover);
                                int[] these_LCs = (int[])bin.Deserialize(MS1);

                                int X_IndAll = N.Id - 1;
                                int X_IndCalcs = X_IndAll + (int)Math.Round(((LC_NumXY.X.all.min - LC_NumXY.X.calcs.min) / LC_NumXY.X.all.reso),0);
                                int Y_IndStart = (int)Math.Round((LC_NumXY.Y.calcs.min - LC_NumXY.Y.all.min) / LC_NumXY.Y.all.reso, 0);
                                int Y_IndStop = (int)Math.Round((LC_NumXY.Y.calcs.max - LC_NumXY.Y.all.min) / LC_NumXY.Y.all.reso, 0);

                                for (int j = Y_IndStart; j <= Y_IndStop; j++)
                                {
                                    int Y_IndAll = j;
                                    int Y_IndCalcs = Y_IndAll + (int)Math.Round((LC_NumXY.Y.all.min - LC_NumXY.Y.calcs.min) / LC_NumXY.Y.all.reso, 0);                                                                      

                                    landCoverForCalcs[X_IndCalcs, Y_IndCalcs] = these_LCs[j];
                                    
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                        return;
                    }
                    
                    // Fill Surface roughness and displacement height arrays
                    int numSR = 0;

                    try
                    {
                        numSR = LC_Key.Length;
                    }
                    catch 
                    {
                        numSR = 0;
                    }

                    for (int i = 0; i < LC_NumXY.X.calcs.num; i++)
                    {
                        for (int j = 0; j < LC_NumXY.Y.calcs.num; j++)
                        {
                            for (int k = 0; k < numSR; k++)
                            {
                                if (landCoverForCalcs[i, j] == LC_Key[k].code)
                                {                                    
                                    SR_ForCalcs[i, j] = LC_Key[k].SR;
                                    DH_ForCalcs[i, j] = LC_Key[k].DH;
                                    break;
                                }
                            }
                        }
                    }
                }

            }
        }
                
        /// <summary>  Gets land cover code.  Only used in TopoInfo tests (ReadGeoTiffLandCover) and not for any SRDH calcs.  </summary>        
        public int GetLC_Code(double UTMX, double UTMY)
        {
            int thisLC_Code = 0;

            int X_Ind = (int)((UTMX - LC_NumXY.X.plot.min) / LC_NumXY.X.plot.reso);
            int Y_Ind = (int)((UTMY - LC_NumXY.Y.plot.min) / LC_NumXY.Y.plot.reso);

            if (X_Ind >= 0 && X_Ind < LC_NumXY.X.plot.num && Y_Ind >= 0 && Y_Ind < LC_NumXY.Y.plot.num)
                thisLC_Code = landCover[X_Ind, Y_Ind];

            return thisLC_Code;
        }
               
        /// <summary> Reads digital elevation data from GeoTIFF file. </summary>        
        public bool ReadGeoTiffTopo(string wholePath, Continuum thisInst)
        {                        
            GdalConfiguration.ConfigureGdal();
            Gdal.AllRegister();            
            Dataset GDAL_obj;
                        
            try {
                GDAL_obj = Gdal.Open(wholePath, Access.GA_ReadOnly);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Unable to open file. Make sure that it's not open in another program.", "Continuum 2.2");
                return false;
            }

            int width = GDAL_obj.RasterXSize;
            int height = GDAL_obj.RasterYSize;

            topoNumXY.X.all.num = width;
            topoNumXY.Y.all.num = height;

            double[] geoTrans = new double[6];

            string projection = GDAL_obj.GetProjection();
            GDAL_obj.GetGeoTransform(geoTrans);

            double minX = geoTrans[0];
            double minY = geoTrans[3] + width * geoTrans[4] + height * geoTrans[5];
            double X_Reso = geoTrans[1];
            double Y_Reso = -geoTrans[5];
            double maxX = minX + X_Reso * (width - 1);
            double maxY = minY + Y_Reso * (height - 1);

            SpatialReference src = new SpatialReference(projection);
            string datumString = thisInst.UTM_conversions.GetDatumString(thisInst.UTM_conversions.savedDatumIndex);

            if (datumString == "NAD83/WGS84")
            {
                datumString = "NAD83";
            }
            else if (datumString == "NAD27/Clarke 1866")
                datumString = "NAD27";

            int zoneNumber = thisInst.UTM_conversions.UTMZoneNumber;
            
            int isNorth = 0; // If point is in southern hemisphere
            if (thisInst.UTM_conversions.hemisphere == "Northern")
                isNorth = 1; // If point is in northern hemisphere

            SpatialReference dst = new SpatialReference("");
            dst.SetProjCS("UTM Proj");  
            dst.SetWellKnownGeogCS(datumString);
            dst.SetUTM(zoneNumber, isNorth);

            double[] UTM_Point = new double[3];
            CoordinateTransformation ct = new CoordinateTransformation(src, dst);

            UTM_Point[0] = minX;
            UTM_Point[1] = minY;
            ct.TransformPoint(UTM_Point);
            
            UTM_Point[0] = maxX;
            UTM_Point[1] = maxY;
            ct.TransformPoint(UTM_Point);                           

            // Figure out grid resolution

            if (X_Reso > 1) { // in meters
                topoNumXY.X.all.reso = X_Reso;
                topoNumXY.Y.all.reso = Y_Reso;
            }
            else  // in lat/long so convert to UTM to find x and y resolution
            {
                UTM_conversion.UTM_coords thisMinXY = thisInst.UTM_conversions.LLtoUTM(minY, minX);
                UTM_conversion.UTM_coords minPlus1 = thisInst.UTM_conversions.LLtoUTM(minY, minX + X_Reso);
                double xres = (minPlus1.UTMEasting - thisMinXY.UTMEasting);
                                
                minPlus1 = thisInst.UTM_conversions.LLtoUTM(minY + Y_Reso, minX);
                double yres = (minPlus1.UTMNorthing - thisMinXY.UTMNorthing);

                UTM_conversion.UTM_coords thisMaxXY = thisInst.UTM_conversions.LLtoUTM(maxY, maxX);
                UTM_conversion.UTM_coords maxMinus1 = thisInst.UTM_conversions.LLtoUTM(maxY, maxX - X_Reso);
                double xres2 = (thisMaxXY.UTMEasting - maxMinus1.UTMEasting);
                                
                maxMinus1 = thisInst.UTM_conversions.LLtoUTM(maxY - Y_Reso, maxX);
                double yres2 = (thisMaxXY.UTMNorthing - maxMinus1.UTMNorthing);

                topoNumXY.X.all.reso = (xres + xres2) / 2;
                topoNumXY.Y.all.reso = (yres + yres2) / 2;
            }

            Band GD_Raster = GDAL_obj.GetRasterBand(1);

            double[] buff = new double[width * height];        
            GD_Raster.ReadRaster(0, 0, width, height, buff, width, height, 0, 0);

            // Check to see if file contains elevation or color RGB codes
            Check_class check = new Check_class();
            bool isGeoTiff = check.IsGeoTIFF(buff);

            if (isGeoTiff == false)
            {
               MessageBox.Show("The elevation TIF file appears to contain color RGB raster values and not elevation data. Aborting import.");                
               return false;
            }

            // Create array of TopoGrid containing raw GeoTiff data
            TopoGrid[] rawGeoTiff = new TopoGrid[width * height];

            double new_MinX = 0;
            double new_MinY = 0;
            double new_MaxX = 0;
            double new_MaxY = 0;
                        
            int X_Ind = 0;
            int Y_Ind = height - 1;                        

            // Fill in rawGeoTiff array
            for (int i = 0; i <= width * height - 1; i++)
            {
                double origX = minX + X_Ind * X_Reso;
                double origY = minY + Y_Ind * Y_Reso;
                UTM_Point[0] = origX;
                UTM_Point[1] = origY;

                ct.TransformPoint(UTM_Point);
                rawGeoTiff[i].UTMX = UTM_Point[0];
                rawGeoTiff[i].UTMY = UTM_Point[1];
                rawGeoTiff[i].elev = buff[i];                               

                if (i == 0) {
                    new_MinX = UTM_Point[0];
                    new_MaxX = UTM_Point[0];
                }
                else if (UTM_Point[0] < new_MinX) 
                    new_MinX = UTM_Point[0];
                else if (UTM_Point[0] > new_MaxX) 
                    new_MaxX = UTM_Point[0];

                if (i == 0) {
                    new_MinY = UTM_Point[1];
                    new_MaxY = UTM_Point[1];
                }
                else if (UTM_Point[1] < new_MinY) 
                    new_MinY = UTM_Point[1];
                else if (UTM_Point[1] > new_MaxY) 
                    new_MaxY = UTM_Point[1];

                if (X_Ind < width - 1)
                    X_Ind = X_Ind + 1;
                else {
                    X_Ind = 0;
                    Y_Ind = Y_Ind - 1;
                }
            }
                        
            int newWidth = Convert.ToInt16((new_MaxX - new_MinX) / topoNumXY.X.all.reso) + 1;
            int newHeight = Convert.ToInt16((new_MaxY - new_MinY) / topoNumXY.Y.all.reso) + 1;

            double[,] Proj_Elev = new double[newWidth, newHeight];          
            CoordinateTransformation ct_to_LL = new CoordinateTransformation(dst, src);             

            // Fill in Proj_Elevs by interpolating Raw GeoTiff data to create a uniform grid
            for (int i = 0; i < newWidth; i++)
            {
                for (int j = 0; j < newHeight; j++)
                {
                    double thisX = new_MinX + i * topoNumXY.X.all.reso;
                    double thisY = new_MinY + j * topoNumXY.Y.all.reso;
                    double Interp_Elev = -999;                                       

                    UTM_Point[0] = thisX; // convert desired UTMX/Y to lat/long
                    UTM_Point[1] = thisY; 
                    ct_to_LL.TransformPoint(UTM_Point);

                    double longX = UTM_Point[0];
                    double latY = UTM_Point[1];

                    if (longX >= minX && longX <= maxX && latY >= minY && latY <= maxY)
                    {
                        int X_IndMin = (int)Math.Floor((longX - minX) / X_Reso);
                        int X_IndMax = X_IndMin + 1;                        

                        int Y_IndMin = (int)Math.Floor((maxY - latY) / Y_Reso);
                        int Y_IndMax = Y_IndMin + 1;                       

                        int index1 = Y_IndMin * width + X_IndMin;
                        int index2 = Y_IndMax * width + X_IndMin;
                        int index3 = Y_IndMax * width + X_IndMax;
                        int index4 = Y_IndMin * width + X_IndMax;

                        if (index1 > 0 && index1 < rawGeoTiff.Length && index2 > 0 && index2 < rawGeoTiff.Length
                            && index3 > 0 && index3 < rawGeoTiff.Length && index4 > 0 && index4 < rawGeoTiff.Length) // didn't find four closest points
                        {
                            double dist1 = CalcDistanceBetweenPoints(rawGeoTiff[index1].UTMX, rawGeoTiff[index1].UTMY, thisX, thisY);
                            double dist2 = CalcDistanceBetweenPoints(rawGeoTiff[index2].UTMX, rawGeoTiff[index2].UTMY, thisX, thisY);
                            double dist3 = CalcDistanceBetweenPoints(rawGeoTiff[index3].UTMX, rawGeoTiff[index3].UTMY, thisX, thisY);
                            double dist4 = CalcDistanceBetweenPoints(rawGeoTiff[index4].UTMX, rawGeoTiff[index4].UTMY, thisX, thisY);

                            if (dist1 == 0)
                                Interp_Elev = rawGeoTiff[index1].elev;
                            else if (dist2 == 0)
                                Interp_Elev = rawGeoTiff[index2].elev;
                            else if (dist3 == 0)
                                Interp_Elev = rawGeoTiff[index3].elev;
                            else if (dist4 == 0)
                                Interp_Elev = rawGeoTiff[index4].elev;
                            else
                            {
                                Interp_Elev = 0;
                                double sumDist = 0;
                                if (rawGeoTiff[index1].elev > 0)
                                {
                                    Interp_Elev = rawGeoTiff[index1].elev / dist1;
                                    sumDist = 1 / dist1;
                                }
                                
                                if (rawGeoTiff[index2].elev > 0)
                                {
                                    Interp_Elev = Interp_Elev + rawGeoTiff[index2].elev / dist2;
                                    sumDist = sumDist + 1 / dist2;
                                }

                                if (rawGeoTiff[index3].elev > 0)
                                {
                                    Interp_Elev = Interp_Elev + rawGeoTiff[index3].elev / dist3;
                                    sumDist = sumDist + 1 / dist3;
                                }

                                if (rawGeoTiff[index4].elev > 0)
                                {
                                    Interp_Elev = Interp_Elev + rawGeoTiff[index4].elev / dist4;
                                    sumDist = sumDist + 1 / dist4;
                                }

                                if (Interp_Elev > 0 && sumDist > 0)
                                    Interp_Elev = Interp_Elev / sumDist;
                                else
                                    Interp_Elev = -999;
                           
                            }

                            if (Interp_Elev < 0 && Interp_Elev != -999)
                                Interp_Elev = Interp_Elev;
                        }
                    }                
                    Proj_Elev[i, j] = Interp_Elev;         
                    
                }
            }

            topoNumXY.X.all.num = newWidth;
            topoNumXY.X.all.min = new_MinX;
            topoNumXY.X.all.max = new_MinX + (newWidth - 1) * topoNumXY.X.all.reso;
            topoNumXY.Y.all.num = newHeight;
            topoNumXY.Y.all.min = new_MinY;
            topoNumXY.Y.all.max = new_MinY + (newHeight - 1) * topoNumXY.Y.all.reso;
            topoElevs = Proj_Elev;

            return true;
        }

        /// <summary> Reads land cover data as a GeoTiff file. </summary> 
        public bool ReadGeoTiffLandCover(string wholePath, Continuum thisInst)
        {            
            GdalConfiguration.ConfigureGdal();
            Gdal.AllRegister();
            Dataset GDAL_obj; // GDal Dataset object which is used to open the GeoTiff file

            try
            {
                GDAL_obj = Gdal.Open(wholePath, Access.GA_ReadOnly);
            }
            catch {
                MessageBox.Show("Unable to open file. Make sure that it's not open in another program.", "Continuum 2.2");
                return false;
            }
            
            int width = GDAL_obj.RasterXSize; // Number of grid points along X in raster file
            int height = GDAL_obj.RasterYSize; // Number of grid points along Y in raster file

            LC_NumXY.X.all.num = width;
            LC_NumXY.Y.all.num = height;

            double[] geoTrans = new double[6]; // Array to hold geographic transformation information

            string projection = GDAL_obj.GetProjection(); // Datum projection string
            GDAL_obj.GetGeoTransform(geoTrans);

            double LC_MinX = geoTrans[0]; // Minimum X coordinate in GeoTiff
            double LC_MinY = geoTrans[3] + width * geoTrans[4] + height * geoTrans[5]; // Minimum Y coordinate in GeoTiff
            double LC_X_Reso = geoTrans[1]; // X Grid Resolution in GeoTiff
            double LC_Y_Reso = -geoTrans[5]; // Y Grid Resolution in GeoTiff
            double LC_MaxX = LC_MinX + LC_X_Reso * (width - 1); // Maximum X coordinate in GeoTiff
            double LC_MaxY = LC_MinY + LC_Y_Reso * (height - 1); // Maximum Y coordinate in GeoTiff

            SpatialReference src = new SpatialReference(projection); // GDal SpatialReference object created using GeoTiff projection. Used to convert data to UTM
            string datumString = thisInst.UTM_conversions.GetDatumString(thisInst.UTM_conversions.savedDatumIndex); // UTM datum selected to be used in Continuum
            if (datumString == "" || datumString == "NAD83/WGS84")
                datumString = "WGS84";
            else if (datumString == "NAD27/Clarke 1866")
                datumString = "NAD27";

            int zoneNumber = thisInst.UTM_conversions.UTMZoneNumber; 
            
            int isNorth = 0; //point is in southern hemisphere
            if (thisInst.UTM_conversions.hemisphere == "Northern")
                isNorth = 1; //point is in northern hemisphere

            SpatialReference dst = new SpatialReference(""); // GDal SpatialReference object created using UTM datum and zone selected in Continuum 
            dst.SetProjCS("UTM Proj");
            dst.SetWellKnownGeogCS(datumString);
            dst.SetUTM(zoneNumber, isNorth);                      
            
            // Figure out grid resolution to use to define UTM grid
            if (LC_X_Reso > 1)
            { // in meters
                LC_NumXY.X.all.reso = LC_X_Reso;
                LC_NumXY.Y.all.reso = LC_Y_Reso;
            }
            else
            {
                UTM_conversion.UTM_coords thisMinXY = thisInst.UTM_conversions.LLtoUTM(LC_MinY, LC_MinX); // Min X/Y coordinate converted to UTM 
                UTM_conversion.UTM_coords minPlus1 = thisInst.UTM_conversions.LLtoUTM(LC_MinY, LC_MinX + LC_X_Reso); // Min X/Y plus 1 grid point along X
                double xres = (minPlus1.UTMEasting - thisMinXY.UTMEasting); // X grid resolution based on lower left grid points

                minPlus1 = thisInst.UTM_conversions.LLtoUTM(LC_MinY + LC_Y_Reso, LC_MinX);
                double yres = (minPlus1.UTMNorthing - thisMinXY.UTMNorthing); // Y grid resolution based on lower left grid point

                UTM_conversion.UTM_coords thisMaxXY = thisInst.UTM_conversions.LLtoUTM(LC_MaxY, LC_MaxX); // Max X/Y coordinate converted to UTM
                UTM_conversion.UTM_coords maxMinus1 = thisInst.UTM_conversions.LLtoUTM(LC_MaxY, LC_MaxX - LC_X_Reso); // Max X/Y minus 1 grid point along X
                double xres2 = (thisMaxXY.UTMEasting - maxMinus1.UTMEasting); // X grid resolution based on upper right grid points

                maxMinus1 = thisInst.UTM_conversions.LLtoUTM(LC_MaxY - LC_Y_Reso, LC_MaxX);
                double yres2 = (thisMaxXY.UTMNorthing - maxMinus1.UTMNorthing); // Y grid resolution based on upper right grid points

                LC_NumXY.X.all.reso = (xres + xres2) / 2;
                LC_NumXY.Y.all.reso = (yres + yres2) / 2;
            }

            int[] buff = new int[width * height]; // Array to hold all land cover data in GeoTiff file
            Band GD_Raster = GDAL_obj.GetRasterBand(1);  // OSGeo.GDal.Band object used to read landcover data in GeoTiff          
            GD_Raster.GetRasterCategoryNames();
            GD_Raster.ReadRaster(0, 0, width, height, buff, width, height, 0, 0); // Populates buff with GeoTiff raster data

            // Create array of LandCoverGrid containing raw GeoTiff data
            LandCoverGrid[] rawGeoTiff = new LandCoverGrid[width * height];

            double newLC_MinX = 0; // Min UTM X of new forced uniform grid
            double newLC_MinY = 0; // Min UTM X of new forced uniform grid
            double newLC_MaxX = 0; // Min UTM X of new forced uniform grid
            double newLC_MaxY = 0;       // Min UTM X of new forced uniform grid               

            int X_Ind = 0;            
            int Y_Ind = height - 1;

            CoordinateTransformation ct = new CoordinateTransformation(src, dst); // GDal CoordinateTransformation object used to convert data to UTM 
            double[] UTM_Point = new double[2]; // Array to hold coordinates to be transformed

            // Read through raster data to find min/max values of new forced uniform UTM grid
            for (int i = 0; i < width * height; i++)
            {
                UTM_Point[0] = LC_MinX + X_Ind * LC_X_Reso;
                UTM_Point[1] = LC_MinY + Y_Ind * LC_Y_Reso;
                
                ct.TransformPoint(UTM_Point);
                rawGeoTiff[i].UTMX = UTM_Point[0];
                rawGeoTiff[i].UTMY = UTM_Point[1];
                rawGeoTiff[i].LC_code = buff[i];

                if (i == 0)
                {
                    newLC_MinX = UTM_Point[0];
                    newLC_MaxX = UTM_Point[0];
                }
                else if (UTM_Point[0] < newLC_MinX)
                    newLC_MinX = UTM_Point[0];
                else if (UTM_Point[0] > newLC_MaxX)
                    newLC_MaxX = UTM_Point[0];

                if (i == 0)
                {
                    newLC_MinY = UTM_Point[1];
                    newLC_MaxY = UTM_Point[1];
                }
                else if (UTM_Point[1] < newLC_MinY)
                    newLC_MinY = UTM_Point[1];
                else if (UTM_Point[1] > newLC_MaxY)
                    newLC_MaxY = UTM_Point[1];

                if (buff[i] >= 0)
                {
                    bool LC_Code_In_Key = CheckKeyForLC_Code(buff[i]);

                    if (LC_Code_In_Key == false)
                    {
                        MessageBox.Show("Read in land cover code that is not defined in the selected key. Land Cover Code: " + buff[i].ToString());
                        return false;
                    }                                        
                }

                if (X_Ind <= width - 2)
                    X_Ind = X_Ind + 1;
                else {
                    X_Ind = 0;
                    Y_Ind = Y_Ind - 1;
                }
            }

            int newWidth = Convert.ToInt16((newLC_MaxX - newLC_MinX) / LC_NumXY.X.all.reso + 1); // X Grid size of UTM grid
            int newHeight = Convert.ToInt16((newLC_MaxY - newLC_MinY) / LC_NumXY.Y.all.reso + 1); // Y Grid size of UTM grid

            int[,] projLandCover = new int[newWidth, newHeight]; // Holds land cover at each grid point

            CoordinateTransformation ct_to_LL = new CoordinateTransformation(dst, src); // GDal CoordinateTransformation object used to convert UTM to LL

            // Fill in projLandCover by finding closest Raw GeoTiff data to create a uniform grid
            for (int i = 0; i < newWidth; i++)
            {
                for (int j = 0; j < newHeight; j++)
                {
                    double thisX = newLC_MinX + i * LC_NumXY.X.all.reso;
                    double thisY = newLC_MinY + j * LC_NumXY.Y.all.reso;
                    int closestLC = -999; // Land cover code of grid point closest to thisX, thisY

                    UTM_Point[0] = thisX; // convert desired UTMX/Y to lat/long
                    UTM_Point[1] = thisY;
                    ct_to_LL.TransformPoint(UTM_Point);

                    double longX = UTM_Point[0];
                    double latY = UTM_Point[1];

                    if (longX >= LC_MinX && longX <= LC_MaxX && latY >= LC_MinY && latY <= LC_MaxY)
                    { // Finds four closest grid points from GeoTiff data
                        int X_IndMin = (int)Math.Floor((longX - LC_MinX) / LC_X_Reso);
                        int X_IndMax = X_IndMin + 1;

                        int Y_IndMin = (int)Math.Floor((LC_MaxY - latY) / LC_Y_Reso);
                        int Y_IndMax = Y_IndMin + 1;

                        int index1 = Y_IndMin * width + X_IndMin;
                        int index2 = Y_IndMax * width + X_IndMin;
                        int index3 = Y_IndMax * width + X_IndMax;
                        int index4 = Y_IndMin * width + X_IndMax;

                        if (index1 > 0 && index1 < rawGeoTiff.Length && index2 > 0 && index2 < rawGeoTiff.Length
                            && index3 > 0 && index3 < rawGeoTiff.Length && index4 > 0 && index4 < rawGeoTiff.Length) // didn't find four closest points
                        {
                            double dist1 = CalcDistanceBetweenPoints(rawGeoTiff[index1].UTMX, rawGeoTiff[index1].UTMY, thisX, thisY);
                            double dist2 = CalcDistanceBetweenPoints(rawGeoTiff[index2].UTMX, rawGeoTiff[index2].UTMY, thisX, thisY);
                            double dist3 = CalcDistanceBetweenPoints(rawGeoTiff[index3].UTMX, rawGeoTiff[index3].UTMY, thisX, thisY);
                            double dist4 = CalcDistanceBetweenPoints(rawGeoTiff[index4].UTMX, rawGeoTiff[index4].UTMY, thisX, thisY);

                            if (dist1 <= dist2 && dist1 <= dist3 && dist1 <= dist4)
                                closestLC = rawGeoTiff[index1].LC_code;
                            else if (dist2 <= dist1 && dist2 <= dist3 && dist2 <= dist4)
                                closestLC = rawGeoTiff[index2].LC_code;
                            else if (dist3 <= dist1 && dist3 <= dist2 && dist3 <= dist4)
                                closestLC = rawGeoTiff[index3].LC_code;
                            else if (dist4 <= dist1 && dist4 <= dist2 && dist4 <= dist3)
                                closestLC = rawGeoTiff[index4].LC_code;                                                                                       
                            
                        }
                    }
                    projLandCover[i, j] = closestLC;

                }
            }

            LC_NumXY.X.all.min = newLC_MinX;
            LC_NumXY.X.all.num = newWidth;
            LC_NumXY.X.all.max = newLC_MinX + (newWidth - 1) * LC_NumXY.X.all.reso;
            LC_NumXY.Y.all.min = newLC_MinY;
            LC_NumXY.Y.all.num = newHeight;
            LC_NumXY.Y.all.max = newLC_MinY + (newHeight - 1) * LC_NumXY.Y.all.reso;
            landCover = projLandCover;
                        
            useSR = true;

            return true;
        }

        /// <summary> Returns true if This_LC_Code is defined in the Land cover key. </summary>        
        public bool CheckKeyForLC_Code(int This_LC_Code)
        {            
            bool LC_in_Key = false;

            if (LC_Key != null)
            {
                for (int i = 0; i < LC_Key.Length; i++)
                    if (This_LC_Code == LC_Key[i].code)
                    {
                        LC_in_Key = true;
                        break;
                    }

                if (This_LC_Code == 0)
                    LC_in_Key = true; // Zeros are ignored in SRDH calcs and a warning is given but can still import land cover with empty values
            }

            return LC_in_Key;
        }

        /// <summary> Sets the land cover key to be USGS National Land Cover data. </summary>        
        public void SetUS_NLCD_Key()
        {            
            // Land cover type as defined by USGS:
            
            LC_Key = new LC_SR_DH[20];
            LC_Key[0].code = 11;
            LC_Key[0].desc = "Open Water";
            LC_Key[0].SR = 0.0002f;
            LC_Key[0].DH = 0;

            LC_Key[1].code = 12;
            LC_Key[1].desc = "Perennial Ice/Snow";
            LC_Key[1].SR = 0.0024f;
            LC_Key[1].DH = 0;

            LC_Key[2].code = 31;
            LC_Key[2].desc = "Barren Bare Rock/Sand/Clay";
            LC_Key[2].SR = 0.005f;
            LC_Key[2].DH = 0f;

            LC_Key[3].code = 71;
            LC_Key[3].desc = "Grassland/Herbaceous";
            LC_Key[3].SR = 0.03f;
            LC_Key[3].DH = 0.1f;

            LC_Key[4].code = 72;
            LC_Key[4].desc = "Sedge/Herbaceous";
            LC_Key[4].SR = 0.03f;
            LC_Key[4].DH = 0.3f;

            LC_Key[5].code = 73;
            LC_Key[5].desc = "Lichens";
            LC_Key[5].SR = 0.03f;
            LC_Key[5].DH = 0.1f;

            LC_Key[6].code = 74;
            LC_Key[6].desc = "Moss";
            LC_Key[6].SR = 0.03f;
            LC_Key[6].DH = 0.1f;

            LC_Key[7].code = 81;
            LC_Key[7].desc = "Pasture/Hay";
            LC_Key[7].SR = 0.03f;
            LC_Key[7].DH = 0.1f;

            LC_Key[8].code = 21;
            LC_Key[8].desc = "Developed Open Space";
            LC_Key[8].SR = 0.2f;
            LC_Key[8].DH = 0.7f;

            LC_Key[9].code = 51;
            LC_Key[9].desc = "Dwarf Shrubland";
            LC_Key[9].SR = 0.2f;
            LC_Key[9].DH = 0.1f;

            LC_Key[10].code = 52;
            LC_Key[10].desc = "Shrub/scrub";
            LC_Key[10].SR = 0.2f;
            LC_Key[10].DH = 1.3f;

            LC_Key[11].code = 82;
            LC_Key[11].desc = "Row Crops";
            LC_Key[11].SR = 0.25f;
            LC_Key[11].DH = 1.3f;

            LC_Key[12].code = 22;
            LC_Key[12].desc = "Developed Low-intensity residential";
            LC_Key[12].SR = 0.4f;
            LC_Key[12].DH = 1.3f;

            LC_Key[13].code = 95;
            LC_Key[13].desc = "Emergent Herbaceous Wetlands";
            LC_Key[13].SR = 0.4f;
            LC_Key[13].DH = 0.7f;

            LC_Key[14].code = 90;
            LC_Key[14].desc = "Woody Wetlands";
            LC_Key[14].SR = 0.5f;
            LC_Key[14].DH = 3.3f;

            LC_Key[15].code = 23;
            LC_Key[15].desc = "Developed Med-intensity residential";
            LC_Key[15].SR = 0.8f;
            LC_Key[15].DH = 3.3f;

            LC_Key[16].code = 41;
            LC_Key[16].desc = "Deciduous forest";
            LC_Key[16].SR = 1f;
            LC_Key[16].DH = 16.7f;

            LC_Key[17].code = 43;
            LC_Key[17].desc = "Mixed forest";
            LC_Key[17].SR = 1.1f;
            LC_Key[17].DH = 16.7f;

            LC_Key[18].code = 42;
            LC_Key[18].desc = "Evergreen forest";
            LC_Key[18].SR = 1.2f;
            LC_Key[18].DH = 13.3f;

            LC_Key[19].code = 24;
            LC_Key[19].desc = "Developed High-intensity residential";
            LC_Key[19].SR = 1.6f;
            LC_Key[19].DH = 13.3f;

        }

        /// <summary> Returns true if Land Cover key is the USGS NLCD. </summary>
        public bool LC_IsDefaultNLCD(LC_SR_DH[] thisLC_Key)
        {
            bool isNLCD = true;
            int numCodes = 0;

            if (thisLC_Key != null)            
                numCodes = thisLC_Key.Length;            
            else 
            { 
                numCodes = 0;
                isNLCD = false;
            }
            
            if (numCodes == 20)
            {
                TopoInfo topoNLCD_Key = new TopoInfo();
                topoNLCD_Key.SetUS_NLCD_Key();

                for (int i = 0; i < numCodes; i++)
                    if (topoNLCD_Key.LC_Key[i].code != thisLC_Key[i].code || (topoNLCD_Key.LC_Key[i].code == thisLC_Key[i].code && (topoNLCD_Key.LC_Key[i].SR != thisLC_Key[i].SR
                        || topoNLCD_Key.LC_Key[i].DH != thisLC_Key[i].DH)))
                    {
                        isNLCD = false;
                        break;
                    }
            }
            else
                isNLCD = false;

            return isNLCD;

        }

        /// <summary> Returns true if Land Cover key is North America Land Cover data. </summary>        
        public bool LC_IsDefaultNALC(LC_SR_DH[] thisLC_Key)
        {            
            bool isNALC = true;
            int numCodes = 0;
            
            if (thisLC_Key != null)            
                numCodes = thisLC_Key.Length;
           else 
            { 
                numCodes = 0;
                isNALC = false;
            }

            if (numCodes == 19)
            {
                TopoInfo topoNALC_Key = new TopoInfo();
                topoNALC_Key.SetNA_LC_Key();

                for (int i = 0; i < numCodes; i++)
                    if (topoNALC_Key.LC_Key[i].code != thisLC_Key[i].code || (topoNALC_Key.LC_Key[i].code == thisLC_Key[i].code && (topoNALC_Key.LC_Key[i].SR != thisLC_Key[i].SR
                            || topoNALC_Key.LC_Key[i].DH != thisLC_Key[i].DH)))
                    {
                        isNALC = false;
                        break;
                    }
            }
            else
                isNALC = false;

            return isNALC;

        }

        /// <summary> Returns true if Land Cover key is EU Corine 2006 Land Cover data. </summary>        
        public bool LC_IsDefaultEU_Corine(LC_SR_DH[] thisLC_Key)
        {
            bool isEU_LC = true;
            int numCodes = 0;

            if (thisLC_Key != null)
                numCodes = thisLC_Key.Length;
            else 
            {
                numCodes = 0;
                isEU_LC = false;
            }

            if (numCodes == 44)
            {
                TopoInfo topoEU_Key = new TopoInfo();
                topoEU_Key.SetEU_Corine_LC_Key();

                for (int i = 0; i < numCodes; i++)
                    if (topoEU_Key.LC_Key[i].code != thisLC_Key[i].code || (topoEU_Key.LC_Key[i].code == thisLC_Key[i].code && (topoEU_Key.LC_Key[i].SR != thisLC_Key[i].SR
                            || topoEU_Key.LC_Key[i].DH != thisLC_Key[i].DH)))
                    {
                        isEU_LC = false;
                        break;
                    }
            }

            else
                isEU_LC = false;

            return isEU_LC;

        }

        /// <summary> Sets the land cover key to be North America Land Cover DB. </summary>        
        public void SetNA_LC_Key()
        {             
            LC_Key = new LC_SR_DH[19];
            LC_Key[0].code = 1;
            LC_Key[0].desc = "Temperate or sub-polar needleleaf forest";
            LC_Key[0].SR = 1.2f;
            LC_Key[0].DH = 13.33f;

            LC_Key[1].code = 2;
            LC_Key[1].desc = "Sub-polar taiga needleleaf forest";
            LC_Key[1].SR = 1.2f;
            LC_Key[1].DH = 13.33f;

            LC_Key[2].code = 3;
            LC_Key[2].desc = "Tropical or sub-tropical broadleaf evergreen forest";
            LC_Key[2].SR = 1.2f;
            LC_Key[2].DH = 13.33f;

            LC_Key[3].code = 4;
            LC_Key[3].desc = "Tropical or sub-tropical broadleaf deciduous forest";
            LC_Key[3].SR = 1f;
            LC_Key[3].DH = 16.67f;

            LC_Key[4].code = 5;
            LC_Key[4].desc = "Temperate or sub-polar broadleaf deciduous forest";
            LC_Key[4].SR = 1f;
            LC_Key[4].DH = 16.67f;

            LC_Key[5].code = 6;
            LC_Key[5].desc = "Mixed forest";
            LC_Key[5].SR = 1.1f;
            LC_Key[5].DH = 16.67f;

            LC_Key[6].code = 7;
            LC_Key[6].desc = "Tropical or sub-tropical shrubland";
            LC_Key[6].SR = 0.2f;
            LC_Key[6].DH = 1.33f;

            LC_Key[7].code = 8;
            LC_Key[7].desc = "Temperate or Sub-polar shrubland";
            LC_Key[7].SR = 0.2f;
            LC_Key[7].DH = 1.33f;

            LC_Key[8].code = 9;
            LC_Key[8].desc = "Tropical or Sub-tropical grassland";
            LC_Key[8].SR = 0.03f;
            LC_Key[8].DH = 0.13f;

            LC_Key[9].code = 10;
            LC_Key[9].desc = "Temperate or Sub-polar grassland";
            LC_Key[9].SR = 0.03f;
            LC_Key[9].DH = 0.13f;

            LC_Key[10].code = 11;
            LC_Key[10].desc = "Sub-polar or polar shrubland-lichen-moss";
            LC_Key[10].SR = 0.03f;
            LC_Key[10].DH = 0.07f;

            LC_Key[11].code = 12;
            LC_Key[11].desc = "Sub-polar or polar grassland-lichen-moss";
            LC_Key[11].SR = 0.03f;
            LC_Key[11].DH = 0.07f;

            LC_Key[12].code = 13;
            LC_Key[12].desc = "Sub-polar or polar barren-lichen-moss";
            LC_Key[12].SR = 0.03f;
            LC_Key[12].DH = 0.07f;

            LC_Key[13].code = 14;
            LC_Key[13].desc = "Wetland";
            LC_Key[13].SR = 0.5f;
            LC_Key[13].DH = 3.33f;

            LC_Key[14].code = 15;
            LC_Key[14].desc = "Cropland";
            LC_Key[14].SR = 0.25f;
            LC_Key[14].DH = 1.33f;

            LC_Key[15].code = 16;
            LC_Key[15].desc = "Barren land";
            LC_Key[15].SR = 0.005f;
            LC_Key[15].DH = 0.0f;

            LC_Key[16].code = 17;
            LC_Key[16].desc = "Urban and built-up";
            LC_Key[16].SR = 1.6f;
            LC_Key[16].DH = 13.3f;

            LC_Key[17].code = 18;
            LC_Key[17].desc = "Water";
            LC_Key[17].SR = 0.0002f;
            LC_Key[17].DH = 0.0f;

            LC_Key[18].code = 19;
            LC_Key[18].desc = "Snow and ice";
            LC_Key[18].SR = 0.0024f;
            LC_Key[18].DH = 0.0f;

        }

        /// <summary> Sets the land cover key to be EU Corine 2006 Land Cover DB. </summary>
        public void SetEU_Corine_LC_Key()
        {   
            LC_Key = new LC_SR_DH[44];
            LC_Key[0].code = 1;
            LC_Key[0].desc = "Continuous urban fabric";
            LC_Key[0].SR = 0.8f;
            LC_Key[0].DH = 3.3f;

            LC_Key[1].code = 2;
            LC_Key[1].desc = "Discontinuous urban fabric";
            LC_Key[1].SR = 0.4f;
            LC_Key[1].DH = 1.3f;

            LC_Key[2].code = 3;
            LC_Key[2].desc = "Industrial as commercial units";
            LC_Key[2].SR = 1.6f;
            LC_Key[2].DH = 13.3f;

            LC_Key[3].code = 4;
            LC_Key[3].desc = "Road and rail networks and associated land";
            LC_Key[3].SR = 0.4f;
            LC_Key[3].DH = 1.3f;

            LC_Key[4].code = 5;
            LC_Key[4].desc = "Port areas";
            LC_Key[4].SR = 1.6f;
            LC_Key[4].DH = 13.3f;

            LC_Key[5].code = 6;
            LC_Key[5].desc = "Airports";
            LC_Key[5].SR = 1.6f;
            LC_Key[5].DH = 13.3f;

            LC_Key[6].code = 7;
            LC_Key[6].desc = "Mineral extraction sites";
            LC_Key[6].SR = 1.6f;
            LC_Key[6].DH = 13.3f;

            LC_Key[7].code = 8;
            LC_Key[7].desc = "Dump sites";
            LC_Key[7].SR = 0.4f;
            LC_Key[7].DH = 1.3f;

            LC_Key[8].code = 9;
            LC_Key[8].desc = "Construction sites";
            LC_Key[8].SR = 1.6f;
            LC_Key[8].DH = 13.3f;

            LC_Key[9].code = 10;
            LC_Key[9].desc = "green urban areas";
            LC_Key[9].SR = 0.8f;
            LC_Key[9].DH = 3.3f;

            LC_Key[10].code = 11;
            LC_Key[10].desc = "Sport and leisure facilities";
            LC_Key[10].SR = 1.6f;
            LC_Key[10].DH = 13.3f;

            LC_Key[11].code = 12;
            LC_Key[11].desc = "Non-irrigated arable land";
            LC_Key[11].SR = 0.2f;
            LC_Key[11].DH = 0.1f;

            LC_Key[12].code = 13;
            LC_Key[12].desc = "Permanently irrigated land";
            LC_Key[12].SR = 0.03f;
            LC_Key[12].DH = 0.1f;

            LC_Key[13].code = 14;
            LC_Key[13].desc = "Rice fields";
            LC_Key[13].SR = 0.25f;
            LC_Key[13].DH = 1.3f;

            LC_Key[14].code = 15;
            LC_Key[14].desc = "Vineyards";
            LC_Key[14].SR = 0.25f;
            LC_Key[14].DH = 1.3f;

            LC_Key[15].code = 16;
            LC_Key[15].desc = "Fruit trees and berry plantations";
            LC_Key[15].SR = 0.25f;
            LC_Key[15].DH = 1.3f;

            LC_Key[16].code = 17;
            LC_Key[16].desc = "Olive groves";
            LC_Key[16].SR = 0.25f;
            LC_Key[16].DH = 1.3f;

            LC_Key[17].code = 18;
            LC_Key[17].desc = "Pastures";
            LC_Key[17].SR = 0.03f;
            LC_Key[17].DH = 0.1f;

            LC_Key[18].code = 19;
            LC_Key[18].desc = "Annual crops associated with permanent crops";
            LC_Key[18].SR = 0.25f;
            LC_Key[18].DH = 1.3f;

            LC_Key[19].code = 20;
            LC_Key[19].desc = "Complex cultivation patterns";
            LC_Key[19].SR = 0.25f;
            LC_Key[19].DH = 1.3f;

            LC_Key[20].code = 21;
            LC_Key[20].desc = "Land principally occupied by agriculture, with signif[icant are= of natural vegetation";
            LC_Key[20].SR = 0.25f;
            LC_Key[20].DH = 1.3f;

            LC_Key[21].code = 22;
            LC_Key[21].desc = "Agro-forestry areas";
            LC_Key[21].SR = 0.4f;
            LC_Key[21].DH = 0.7f;

            LC_Key[22].code = 23;
            LC_Key[22].desc = "Broad-leaved forest";
            LC_Key[22].SR = 1f;
            LC_Key[22].DH = 16.7f;

            LC_Key[23].code = 24;
            LC_Key[23].desc = "Coniferous forest";
            LC_Key[23].SR = 1f;
            LC_Key[23].DH = 16.7f;

            LC_Key[24].code = 25;
            LC_Key[24].desc = "Mixed forest";
            LC_Key[24].SR = 1.1f;
            LC_Key[24].DH = 16.7f;

            LC_Key[25].code = 26;
            LC_Key[25].desc = "Natural grasslands";
            LC_Key[25].SR = 0.03f;
            LC_Key[25].DH = 0.1f;

            LC_Key[26].code = 27;
            LC_Key[26].desc = "Moors and heathland";
            LC_Key[26].SR = 0.03f;
            LC_Key[26].DH = 0.1f;

            LC_Key[27].code = 28;
            LC_Key[27].desc = "Sclerophyllous vegetation";
            LC_Key[27].SR = 0.03f;
            LC_Key[27].DH = 0.1f;

            LC_Key[28].code = 29;
            LC_Key[28].desc = "Transitional woodland-shrub";
            LC_Key[28].SR = 0.5f;
            LC_Key[28].DH = 3.3f;

            LC_Key[29].code = 30;
            LC_Key[29].desc = "Beaches, dunes, sands";
            LC_Key[29].SR = 0.005f;
            LC_Key[29].DH = 0f;

            LC_Key[30].code = 31;
            LC_Key[30].desc = "Bare rocks";
            LC_Key[30].SR = 0.005f;
            LC_Key[30].DH = 0f;

            LC_Key[31].code = 32;
            LC_Key[31].desc = "Sparsely vegetated areas";
            LC_Key[31].SR = 0.2f;
            LC_Key[31].DH = 0.1f;

            LC_Key[32].code = 33;
            LC_Key[32].desc = "Burnt areas";
            LC_Key[32].SR = 0.005f;
            LC_Key[32].DH = 0f;

            LC_Key[33].code = 34;
            LC_Key[33].desc = "Glaciers and perpetual snow";
            LC_Key[33].SR = 0.0024f;
            LC_Key[33].DH = 0f;

            LC_Key[34].code = 35;
            LC_Key[34].desc = "Inland marshes";
            LC_Key[34].SR = 0.03f;
            LC_Key[34].DH = 0.1f;

            LC_Key[35].code = 36;
            LC_Key[35].desc = "Peat bogs";
            LC_Key[35].SR = 0.03f;
            LC_Key[35].DH = 0.1f;

            LC_Key[36].code = 37;
            LC_Key[36].desc = "Salt marshes";
            LC_Key[36].SR = 0.03f;
            LC_Key[36].DH = 0.1f;

            LC_Key[37].code = 38;
            LC_Key[37].desc = "Salines";
            LC_Key[37].SR = 0.03f;
            LC_Key[37].DH = 0.1f;

            LC_Key[38].code = 39;
            LC_Key[38].desc = "Intertidal flats";
            LC_Key[38].SR = 0.03f;
            LC_Key[38].DH = 0.1f;

            LC_Key[39].code = 40;
            LC_Key[39].desc = "Water courses";
            LC_Key[39].SR = 0.0002f;
            LC_Key[39].DH = 0f;

            LC_Key[40].code = 41;
            LC_Key[40].desc = "Water bodies";
            LC_Key[40].SR = 0.0002f;
            LC_Key[40].DH = 0f;

            LC_Key[41].code = 42;
            LC_Key[41].desc = "Coastal lagoons";
            LC_Key[41].SR = 0.0002f;
            LC_Key[41].DH = 0f;

            LC_Key[42].code = 43;
            LC_Key[42].desc = "Estuaries";
            LC_Key[42].SR = 0.0002f;
            LC_Key[42].DH = 0f;

            LC_Key[43].code = 44;
            LC_Key[43].desc = "Sea and ocean";
            LC_Key[43].SR = 0.0002f;
            LC_Key[43].DH = 0f;

        }

        /// <summary> Finds min/max X/Y of shape and determines whether it is a line or a closed contour . </summary>        
        public void FindShapeMinMaxAndIsClosed(ref Roughness_Map_Struct thisShape)
        {            
            double thisX_Min = 10000000;
            double thisX_Max = 0;
            double thisY_Min = 10000000;
            double thisY_Max = 0;                        

            for (int j = 0; j < thisShape.numPoints; j++)
            {
                if (thisShape.points[j].UTMX < thisX_Min) thisX_Min = thisShape.points[j].UTMX;
                if (thisShape.points[j].UTMX > thisX_Max) thisX_Max = thisShape.points[j].UTMX;
                if (thisShape.points[j].UTMY < thisY_Min) thisY_Min = thisShape.points[j].UTMY;
                if (thisShape.points[j].UTMY > thisY_Max) thisY_Max = thisShape.points[j].UTMY;

            }

            // Polygon is not closed if( the first and l=t point are not the same

            if (thisShape.points[0].UTMX != thisShape.points[thisShape.numPoints - 1].UTMX || thisShape.points[0].UTMY != thisShape.points[thisShape.numPoints - 1].UTMY)
                thisShape.isClosed = false;

            thisShape.minX = thisX_Min;
            thisShape.maxX = thisX_Max;
            thisShape.minY = thisY_Min;
            thisShape.maxY = thisY_Max;

        }


        /// <summary> Reads in shape objects from a .MAP contour file and creates a land cover key based on the roughness values contained in the file and assigns a default displacement height to each land cover code. </summary>
        public void CreateLC_KeyUsingMAP_Shapes(Roughness_Map_Struct[] theseShapes)
        {
            // reset the LC_Key            
            LC_Key = new LC_SR_DH[1];
            int LC_count = 0;
            
            if (theseShapes == null)
                return;

            for (int i = 0; i < theseShapes.Length; i++)
            {
                bool isNewLeftRough = true;
                bool isNewRightRough = true;

                for (int j = 0; j < LC_count; j++)
                {
                    if (LC_Key[j].SR == theseShapes[i].leftRough) isNewLeftRough = false;
                    if (LC_Key[j].SR == theseShapes[i].rightRough) isNewRightRough = false;
                }

                if (isNewLeftRough == true)
                {
                    Array.Resize(ref LC_Key, LC_count + 1);

                    LC_Key[LC_count].code = LC_count + 1;
                    LC_Key[LC_count].desc = ".Map Roughness " + (LC_count + 1);
                    LC_Key[LC_count].SR = theseShapes[i].leftRough;
                    LC_Key[LC_count].DH = GetDefaultDispHeight(LC_Key[LC_count].SR);
                    LC_count = LC_count + 1;
                }

                if (isNewRightRough == true)
                {
                    Array.Resize(ref LC_Key, LC_count + 1);
                    LC_Key[LC_count].code = LC_count + 1;
                    LC_Key[LC_count].desc = ".Map Roughness " + (LC_count + 1);
                    LC_Key[LC_count].SR = theseShapes[i].rightRough;
                    LC_Key[LC_count].DH = GetDefaultDispHeight(LC_Key[LC_count].SR);
                    LC_count = LC_count + 1;
                }
            }

        }

        /// <summary> Returns true if there is another line between thisY and Y_interp. </summary>   
        public bool DoesThisY_CrossAnotherLine(Roughness_Map_Struct thisShape, int pointInd, double Y_interp, double thisX, double thisY)
        {            
            bool crossesLine = false;
            
            double Y_int_prime = 0;
            double diffY_Int = thisY - Y_interp;
            
            for (int i = 0; i <= thisShape.numPoints - 2; i++)
            {
                if (i != pointInd)
                { // ignore the line that you//re currently working with
                    double X1 = thisShape.points[i].UTMX;
                    double Y1 = thisShape.points[i].UTMY;

                    double X2 = thisShape.points[i + 1].UTMX;
                    double Y2 = thisShape.points[i + 1].UTMY;

                    if (thisX > X1 && thisX < X2)
                    { // thisX falls in range of this other line
                      // calculate Y_interp between point and this other line

                        if (thisX != X1 & thisX != X2 && X1 != X2)
                            Y_int_prime = Y1 + (thisX - X1) * (Y2 - Y1) / (X2 - X1);
                        else if (thisX == X1)
                            Y_int_prime = Y1;
                        else if (thisX == X2)
                            Y_int_prime = Y2;
                        else
                            Y_int_prime = thisY;

                        double diffY_IntPrime = thisY - Y_int_prime;

                        if ((thisY < Y_interp && diffY_IntPrime < 0 && diffY_IntPrime > diffY_Int) ||
                          (thisY > Y_interp && diffY_IntPrime > 0 && diffY_IntPrime < diffY_Int))
                        {
                            crossesLine = true;
                            break;

                        }

                    }
                }
            }

            return crossesLine;
        }

        
        /// <summary> Returns true if data point is outside either the X or the Y bounds. </summary>
        public bool IsOutsideOneOfBounds(int thisX, int thisY, int minX, int maxX, int minY, int maxY)
        {            
            bool isOutside = false;

            if (thisX < minX || thisX > maxX || thisY < minY || thisY > maxY)
                isOutside = true;

            return isOutside;

        }

        /// <summary> Finds interpolated Y value at thisX value. </summary>
        public double GetY_Interp(double thisX, double thisY, double X_1, double X_2, double Y_1, double Y_2)
        {            
            double Y_interp = 0;
            
            if (thisX != X_1 && thisX != X_2 && X_1 != X_2)
                Y_interp = Y_1 + (thisX - X_1) * (Y_2 - Y_1) / (X_2 - X_1);
            else if (thisX == X_1)
                Y_interp = Y_1;
            else if (thisX == X_2)
                Y_interp = Y_2;
            else
                Y_interp = thisY;


            return Y_interp;
        }


        /// <summary> Returns true is thisX/thisY is left of line and false if on right. </summary>
        public bool Is_Left_or_Right(double thisX, double thisY, double X_1, double X_2, double Y_1, double Y_2)
        {            
            bool isLeft = true;
            double deltaX = X_2 - X_1;
            double deltaY = Y_2 - Y_1;
            double Y_interp = GetY_Interp(thisX, thisY, X_1, X_2, Y_1, Y_2);

            if (deltaX == 0)
            { // vertical line

                if (Y_2 > Y_1)
                {
                    if (thisX < X_1)
                        isLeft = true;
                    else
                        isLeft = false;
                }
                else
                {
                    if (thisX > X_1)
                        isLeft = true;
                    else
                        isLeft = false;
                }
            }
            else if (deltaY == 0)
            { // horizontal line

                if (X_2 > X_1)
                {
                    if (thisY > Y_1)
                        isLeft = true;
                    else
                        isLeft = false;
                }
                else
                {
                    if (thisY < Y_1)
                        isLeft = true;
                    else
                        isLeft = false;
                }
            }
            else if (deltaX != 0 || deltaY != 0)
            {
                if (deltaX >= 0 && deltaY >= 0)
                { // First quadrant

                    if (thisY > Y_interp)
                        isLeft = true;
                    else
                        isLeft = false;
                }
                else if (deltaX <= 0 && deltaY >= 0)
                { // Second quad

                    if (thisY > Y_interp)
                        isLeft = false;
                    else
                        isLeft = true;
                }
                else if (deltaX <= 0 && deltaY <= 0)
                { // Third quad

                    if (thisY > Y_interp)
                        isLeft = false;
                    else
                        isLeft = true;
                }
                else if (deltaX >= 0 && deltaY <= 0)
                { // Fourth

                    if (thisY > Y_interp)
                        isLeft = true;
                    else
                        isLeft = false;
                }
            }

            return isLeft;

        }


        /// <summary> Traces each contour/line of each shape in theseShapes() array and assigns left/right Land cover code to +/- 90m from contour/line. </summary>        
        public void AssignLC_CodesToShapeContours(Roughness_Map_Struct[] theseShapes)
        {
            UTM_X_Y gridPoint = new TopoInfo.UTM_X_Y();
            UTM_X_Y nextPoint = new TopoInfo.UTM_X_Y();
            UTM_X_Y lastPoint = new TopoInfo.UTM_X_Y();
            UTM_X_Y startPoint = new TopoInfo.UTM_X_Y();
            UTM_X_Y endPoint = new TopoInfo.UTM_X_Y();

            int leftLC = 1;
            int rightLC = 1;            
            bool nextIsLeft = false;

            double angleLastToPt1 = 0;
            double anglePt1toPt2 = 0;
            double anglePt2ToNext = 0;
            double angleDiff = 0;

            if ((theseShapes == null) || (LC_Key == null))
                return;

            for (int shpInd = 0; shpInd <= theseShapes.Length - 1; shpInd++)
            {
                for (int ptInd = 0; ptInd <= theseShapes[shpInd].numPoints - 2; ptInd++)
                {
                    startPoint.UTMX = theseShapes[shpInd].points[ptInd].UTMX;
                    startPoint.UTMY = theseShapes[shpInd].points[ptInd].UTMY;

                    endPoint.UTMX = theseShapes[shpInd].points[ptInd + 1].UTMX;
                    endPoint.UTMY = theseShapes[shpInd].points[ptInd + 1].UTMY;

                    anglePt1toPt2 = Convert.ToSingle(Math.Atan2((endPoint.UTMY - startPoint.UTMY), (endPoint.UTMX - startPoint.UTMX)) * 180 / Math.PI);
                    if (anglePt1toPt2 < 0) anglePt1toPt2 = anglePt1toPt2 + 360;

                    if (ptInd < theseShapes[shpInd].numPoints - 2)
                    {
                        nextPoint.UTMX = theseShapes[shpInd].points[ptInd + 2].UTMX;
                        nextPoint.UTMY = theseShapes[shpInd].points[ptInd + 2].UTMY;
                    }
                    else if (theseShapes[shpInd].isClosed == true) {
                        nextPoint.UTMX = theseShapes[shpInd].points[1].UTMX;
                        nextPoint.UTMY = theseShapes[shpInd].points[1].UTMY;
                    }
                    else {
                        nextPoint.UTMX = 0;
                        nextPoint.UTMY = 0;
                    }

                    if (nextPoint.UTMX != 0)
                        anglePt2ToNext = Convert.ToSingle(Math.Atan2((nextPoint.UTMY - endPoint.UTMY), (nextPoint.UTMX - endPoint.UTMX)) * 180 / Math.PI);
                    else
                        anglePt2ToNext = 0;

                    if (anglePt2ToNext < 0) anglePt2ToNext = anglePt2ToNext + 360;

                    if (ptInd > 0) {
                        lastPoint.UTMX = theseShapes[shpInd].points[ptInd - 1].UTMX;
                        lastPoint.UTMY = theseShapes[shpInd].points[ptInd - 1].UTMY;
                    }
                    else if (theseShapes[shpInd].isClosed == true) {
                        lastPoint.UTMX = theseShapes[shpInd].points[theseShapes[shpInd].numPoints - 2].UTMX;
                        lastPoint.UTMY = theseShapes[shpInd].points[theseShapes[shpInd].numPoints - 2].UTMY;
                    }
                    else {
                        lastPoint.UTMX = 0;
                        lastPoint.UTMY = 0;
                    }

                    if (lastPoint.UTMX != 0)
                        angleLastToPt1 = Convert.ToSingle(Math.Atan2((startPoint.UTMY - lastPoint.UTMY), (startPoint.UTMX - lastPoint.UTMX)) * 180 / Math.PI);
                    else
                        angleLastToPt1 = 0;

                    if (angleLastToPt1 < 0) angleLastToPt1 = angleLastToPt1 + 360;

                    // Define min/max X and Y to define bounding box to fill with LC codes
                    int X_IndMin = (int)Math.Round((startPoint.UTMX - LC_NumXY.X.all.min) / LC_NumXY.X.all.reso, 0); // Start of line
                    int X_IndMax = (int)Math.Round((endPoint.UTMX - LC_NumXY.X.all.min) / LC_NumXY.X.all.reso, 0); // End of line

                    if (X_IndMin > X_IndMax)
                    {
                        int tempX = X_IndMin;
                        X_IndMin = X_IndMax;
                        X_IndMax = tempX;
                    }

                    int X_ind_min_box = X_IndMin - 2;
                    if (X_ind_min_box < 0) X_ind_min_box = 0;

                    int X_ind_max_box = X_IndMax + 2;
                    if (X_ind_max_box > LC_NumXY.X.all.num - 1) X_ind_max_box = LC_NumXY.X.all.num - 1;
                    //
                    int Y_IndMin = (int)Math.Round((startPoint.UTMY - LC_NumXY.Y.all.min) / LC_NumXY.Y.all.reso, 0);
                    int Y_IndMax = (int)Math.Round((endPoint.UTMY - LC_NumXY.Y.all.min) / LC_NumXY.Y.all.reso, 0);

                    if (Y_IndMin > Y_IndMax)
                    {
                        int tempY = Y_IndMin;
                        Y_IndMin = Y_IndMax;
                        Y_IndMax = tempY;
                    }

                    int Y_ind_min_box = Y_IndMin - 2;
                    if (Y_ind_min_box < 0) Y_ind_min_box = 0;

                    int Y_ind_max_box = Y_IndMax + 2;
                    if (Y_ind_max_box > LC_NumXY.Y.all.num - 1) Y_ind_max_box = LC_NumXY.Y.all.num - 1;

                    for (int LC_ind = 0; LC_ind <= LC_Key.Length - 1; LC_ind++)
                    {
                        if (LC_Key[LC_ind].SR == theseShapes[shpInd].leftRough) leftLC = LC_Key[LC_ind].code;
                        if (LC_Key[LC_ind].SR == theseShapes[shpInd].rightRough) rightLC = LC_Key[LC_ind].code;
                    }

                    for (int X_Ind = X_ind_min_box; X_Ind <= X_ind_max_box; X_Ind++)
                    {
                        for (int Y_Ind = Y_ind_min_box; Y_Ind <= Y_ind_max_box; Y_Ind++)
                        {
                            gridPoint.UTMX = LC_NumXY.X.all.min + X_Ind * LC_NumXY.X.all.reso;
                            gridPoint.UTMY = LC_NumXY.Y.all.min + Y_Ind * LC_NumXY.Y.all.reso;                                                       

                            // Check to see if there is another line between thisY and Y_interp
                            // if there is else don't assign a LC code
                            double Y_interp = GetY_Interp(gridPoint.UTMX, gridPoint.UTMY, startPoint.UTMX, endPoint.UTMX, startPoint.UTMY, endPoint.UTMY);
                            bool crossesAnother = DoesThisY_CrossAnotherLine(theseShapes[shpInd], ptInd, Y_interp, gridPoint.UTMX, gridPoint.UTMY);
                            bool outsideOneBound = IsOutsideOneOfBounds(X_Ind, Y_Ind, X_IndMin, X_IndMax, Y_IndMin, Y_IndMax);                                                     

                            if (landCover[X_Ind, Y_Ind] == 0 && Math.Abs(gridPoint.UTMY - Y_interp) < 90 && crossesAnother == false)
                            {
                                bool isLeft = Is_Left_or_Right(gridPoint.UTMX, gridPoint.UTMY, startPoint.UTMX, endPoint.UTMX, startPoint.UTMY, endPoint.UTMY);

                                if (outsideOneBound == true)
                                {
                                    // if( grid point is outside the min/max X/Y of line segment then check either the last or point to figure out what LC code to assign
                                    // calc distance to each end of line
                                    double distToStart = CalcDistanceBetweenPoints(gridPoint.UTMX, gridPoint.UTMY, startPoint.UTMX, startPoint.UTMY);
                                    double distToEnd = CalcDistanceBetweenPoints(gridPoint.UTMX, gridPoint.UTMY, endPoint.UTMX, endPoint.UTMY);

                                    // figure out if( grid point is closer to start or end of line, then figure out if the grid point is expected to be on the
                                    // left or right side of the last or line 
                                    // Calculate change in angle between line and either last || } point
                                    if (distToStart < distToEnd && lastPoint.UTMX != 0)
                                    { // closer to start of line
                                        nextIsLeft = Is_Left_or_Right(gridPoint.UTMX, gridPoint.UTMY, lastPoint.UTMX, startPoint.UTMX, lastPoint.UTMY, startPoint.UTMY);
                                        angleDiff = anglePt1toPt2 - angleLastToPt1;
                                    }
                                    else if (distToEnd < distToStart && nextPoint.UTMX != 0)
                                    {
                                        nextIsLeft = Is_Left_or_Right(gridPoint.UTMX, gridPoint.UTMY, endPoint.UTMX, nextPoint.UTMX, endPoint.UTMY, nextPoint.UTMY);
                                        angleDiff = anglePt2ToNext - anglePt1toPt2;
                                    }
                                    else
                                    {
                                        // either at start || end of unclosed polygon so don//t =sign anything.Set nextIsLeft to opposite of isLeft
                                        if (isLeft == true)
                                            nextIsLeft = false;
                                        else
                                            nextIsLeft = true;

                                    }

                                    // make sure that angle Difference is +/- 180 degrees
                                    if (angleDiff > 180) angleDiff = angleDiff - 360;
                                    if (angleDiff < -180) angleDiff = angleDiff + 360;

                                    // check to see if( this line and } (or Last) both expect grid point to be on left || right of line
                                    if (isLeft == nextIsLeft)
                                    {
                                        if (isLeft == true)
                                            landCover[X_Ind, Y_Ind] = leftLC;
                                        else
                                            landCover[X_Ind, Y_Ind] = rightLC;
                                    }
                                    else if (isLeft == true && angleDiff < 0)  // if( this line expects point to be on left but either Last || } line expects it to be on the right 
                                        landCover[X_Ind, Y_Ind] = leftLC;   //BUT angle_Diff < 0 (the line is turning clockwise) ) { ignore Last || } line and =sign left LC code
                                    else if (isLeft == false && angleDiff > 0)  // if( this line expects point to be on right but Last || } line expects it to be on left 
                                        landCover[X_Ind, Y_Ind] = rightLC; // BUT angle Diff > 0 (line is turning counter-clockwise) ) { ignore Last || } line and =sign right LC code
                                }
                                else
                                {

                                    if (isLeft == true)
                                        landCover[X_Ind, Y_Ind] = leftLC;
                                    else
                                        landCover[X_Ind, Y_Ind] = rightLC;
                                }
                            }                        

                        }
                    }
                }
            }
        }

        /// <summary>  Fills in the landCover array with land cover codes based on the LC codes that were assigned to the left and right of each line. </summary>

        public void FillInLC_Array()
        {
            // This is called after the void routine 'AssignLC_CodesToShapeContours' has been called.
            // First move along rows and read from bottom-up of each column
            for (int i = 0; i < LC_NumXY.X.all.num; i++)
            {
                for (int j = 0; j < LC_NumXY.Y.all.num; j++)
                {
                    if (landCover[i, j] == 0 && j == 0)
                    { // At start of column, move up unti LC code is not a zero                                              

                        int thisInd = j;
                        int nextLC = landCover[i, thisInd];

                        while (nextLC == 0 && thisInd < LC_NumXY.Y.all.num - 1)
                        {
                            thisInd = thisInd + 1;
                            nextLC = landCover[i, thisInd];
                        }

                        if (nextLC != 0)
                        { // fill in LC codes with from start of column up to where the first LC code was found
                            for (int Y_Ind = j; Y_Ind <= thisInd - 1; Y_Ind++)
                                landCover[i, Y_Ind] = nextLC;
                        }

                        // move 'j' index up to where the first LC code w= found
                        j = thisInd;
                    }
                    else if (landCover[i, j] == 0 && j > 0)
                    { // not at start of column but empty landCover entry next
                        int lastLC = landCover[i, j - 1];
                        int thisInd = j;
                        int nextLC = landCover[i, thisInd];

                        while (nextLC == 0 && thisInd < LC_NumXY.Y.all.num - 1) // fill in landCover codes with //lastLC// until a non-empty landCover entry { is found
                        {
                            landCover[i, thisInd] = lastLC;
                            thisInd = thisInd + 1;
                            nextLC = landCover[i, thisInd];
                        }

                        if (thisInd == LC_NumXY.Y.all.num - 1 && landCover[i, thisInd] == 0)
                            landCover[i, thisInd] = lastLC;

                        j = thisInd;
                    }
                }
            }

            // now move along columns and read rows from left-right
            for (int j = 0; j < LC_NumXY.Y.all.num; j++)
            {
                for (int i = 0; i < LC_NumXY.X.all.num; i++)
                {                    

                    if (landCover[i, j] == 0 && i == 0)
                    { // At start of row, move to right unti LC code is not a zero
                        int thisInd = i;
                        int nextLC = landCover[thisInd, j];

                        while (nextLC == 0 && thisInd < LC_NumXY.X.all.num - 1)
                        {
                            thisInd = thisInd + 1;
                            nextLC = landCover[thisInd, j];
                        }

                        for (int X_Ind = i; X_Ind < thisInd; X_Ind++)  // fill in LC codes with from start of row up to where the first LC code was found
                            landCover[X_Ind, j] = nextLC;

                        i = thisInd; // move 'i' up to where first LC code was found
                    }
                    else if (landCover[i, j] == 0 && i > 0)
                    {
                        int thisInd = i;
                        int lastLC = landCover[i - 1, j];
                        int nextLC = landCover[thisInd, j];
                        
                        while (nextLC == 0 && thisInd < LC_NumXY.X.all.num - 1)
                        { // fill in landCover codes with //lastLC// until a non-empty landCover entry { is found
                            landCover[thisInd, j] = lastLC;
                            thisInd = thisInd + 1;
                            nextLC = landCover[thisInd, j];
                        }


                        if (thisInd == LC_NumXY.X.all.num - 1 && landCover[thisInd, j] == 0)
                            landCover[thisInd, j] = lastLC;

                        i = thisInd; // move //i// up to where } LC code w= found

                    }
                }
            }

            // Fill in along columns one more time to get anything that w= missed
            for (int i = 0; i < LC_NumXY.X.all.num; i++)
            {
                for (int j = 0; j < LC_NumXY.Y.all.num; j++)
                {                    

                    if (landCover[i, j] == 0 && j == 0)
                    { // At start of column, move up unti LC code is not a zero

                        int thisInd = j;
                        int nextLC = landCover[i, thisInd];

                        while (nextLC == 0 && thisInd < LC_NumXY.Y.all.num - 1)
                        {
                            thisInd = thisInd + 1;
                            nextLC = landCover[i, thisInd];
                        }

                        if (nextLC != 0)  // fill in LC codes with from start of column up to where the first LC code was found
                            for (int Y_Ind = j; Y_Ind <= thisInd - 1; Y_Ind++)
                                landCover[i, Y_Ind] = nextLC;

                        // move 'j' index up to where the first LC code was found
                        j = thisInd;
                    }
                    else if (landCover[i, j] == 0 && j > 0)
                    { // not at start of column but empty landCover entry {
                        int lastLC = landCover[i, j - 1];
                        int thisInd = j;
                        int nextLC = landCover[i, thisInd];

                        while (nextLC == 0 && thisInd < LC_NumXY.Y.all.num - 1)
                        { // fill in landCover codes with 'lastLC' until a non-empty landCover entry is found
                            landCover[i, thisInd] = lastLC;
                            thisInd = thisInd + 1;
                            nextLC = landCover[i, thisInd];
                        }

                        if (thisInd == LC_NumXY.Y.all.num - 1 && landCover[i, thisInd] == 0)
                            landCover[i, thisInd] = lastLC;


                        j = thisInd;
                    }
                }
            }
        }

        
        /// <summary> Calculates and erturns the distance between two points. </summary>        
        public double CalcDistanceBetweenPoints(double X_1, double Y_1, double X_2, double Y_2)
        {            
            double thisDist = Math.Sqrt((X_1 - X_2) * (X_1 - X_2) + (Y_1 - Y_2) * (Y_1 - Y_2));
            return thisDist;
        }

        /// <summary> Returns the default displacement height associated with thisSR. </summary>        
        public double GetDefaultDispHeight(double thisSR)
        {             
            double defaultDH = 0;

            if (thisSR < 0.02)
                defaultDH = 0;
            else if (thisSR < 0.25)
                defaultDH = 0.1f;
            else if (thisSR < 0.5)
                defaultDH = 1.3f;
            else if (thisSR < 0.8)
                defaultDH = 2;
            else if (thisSR < 1)
                defaultDH = 3.3f;
            else
                defaultDH = 16.7f;

            return defaultDH;

        }

        /// <summary> Creates array of elevation data along line specified by WD and radius (+/- distance from site) using elevReso as spacing between points. </summary>        
        public TopoGrid[] GetElevationProfile(double UTMX, double UTMY, double WD, int radius, int elevReso)
        {             
            TopoGrid[] elevProfile = new TopoGrid[0];

            if (elevReso == 0)
                return elevProfile;

            int numPoints = 2 * radius / elevReso + 1;
            elevProfile = new TopoGrid[numPoints];
                        
            for (int i = 0; i < numPoints; i++)
            {
                int relDist = -radius + i * elevReso;
                elevProfile[i].UTMX = UTMX + Math.Cos(Math.PI / 180.0 * (90.0 - WD)) * relDist;
                elevProfile[i].UTMY = UTMY + Math.Sin(Math.PI / 180.0 * (90.0 - WD)) * relDist;
                elevProfile[i].elev = CalcElevs(elevProfile[i].UTMX, elevProfile[i].UTMY);
            }
            
            return elevProfile;
        }

        /// <summary> Calculates and returns best-fit slope (using least squares regression) along X and Y values. </summary>        
        public double CalcSlope(double[] xVals, double[] yVals)
        {             
            double slope = 0;

            if (xVals == null || yVals == null)
                return slope;

            if (xVals.Length != yVals.Length)
                return slope;

            int numPts = xVals.Length;

            if (numPts == 0)
                return slope;

            // Calculate mean of X and Y
            double meanX = 0;
            double meanY = 0;

            for (int i = 0; i < numPts; i++)
            {
                meanX = meanX + xVals[i];
                meanY = meanY + yVals[i];
            }

            meanX = meanX / numPts;
            meanY = meanY / numPts;

            // Calculate covariance of X and Y and variance of X
            double sXY = 0;
            double sXX = 0;

            for (int i = 0; i < numPts; i++)
            {
                sXY = sXY + (xVals[i] - meanX) * (yVals[i] - meanY);
                sXX = sXX + Math.Pow(xVals[i] - meanX, 2);
            }

            if (sXX != 0)
                slope = sXY / sXX;        
                     
            return slope;
        }
        
    }
     
}
