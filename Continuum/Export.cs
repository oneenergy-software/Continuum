using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;
using MathNet.Numerics.Statistics;

namespace ContinuumNS
{
    /// <summary> Export class contains functions to export data and calculated results to CSV files. </summary>
    public class Export
    {
        /// <summary> Exports the met cross-prediction error for selected radius and WD sector </summary>        
        public void ExportMetCrossPreds(Continuum thisInst)
        {            
            string headerString = "Continuum 3: Met Cross-Predictions";
            int numPairs = thisInst.metPairList.PairCount;
            StreamWriter sw;

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                }
                catch
                {
                    MessageBox.Show("Error writing to file.  Make sure the file is closed.", "Continuum 3");
                    return;
                }

                sw.WriteLine(headerString);
                sw.WriteLine(DateTime.Now);
                sw.WriteLine("");
                                
                Model[] theseModels = thisInst.GetModels("Advanced");

                if (thisInst.metList.ThisCount > 1)
                    headerString = "Using Site-Calibrated Model";
                else
                    headerString = "Using Default Model";

                sw.WriteLine(headerString);

                if (thisInst.topo.useSR == true)
                    headerString = "Surface Roughness model USED";
                else
                    headerString = "Surface Roughness model NOT USED";

                sw.WriteLine(headerString);

                if (thisInst.topo.useElev == true)
                    headerString = "Elevation model USED";
                else
                    headerString = "Elevation model NOT USED";

                sw.WriteLine(headerString);

                if (thisInst.topo.useValley == true)
                    headerString = "Valley flow model USED";
                else
                    headerString = "Valley flow model NOT USED";

                sw.WriteLine(headerString);

                if (thisInst.topo.useSepMod == true)
                    headerString = "Flow Separation model USED";
                else
                    headerString = "Flow Separation model NOT USED";

                sw.WriteLine(headerString);
                sw.WriteLine();

                for (int r = 0; r < thisInst.radiiList.ThisCount; r++)
                {                    
                    int radius = thisInst.radiiList.investItem[r].radius;

                    sw.Write("Radius = " + radius.ToString());
                    sw.WriteLine();

                    int WD_Ind = thisInst.GetWD_ind("Advanced");
                    int numWD = thisInst.GetNumWD();

                    if (WD_Ind == numWD)
                        sw.Write("Overall WD");
                    else
                        sw.Write("WD sector =" + (WD_Ind * (double)360 / numWD).ToString());

                    sw.WriteLine();

                    // Data_String = new string[3];
                    sw.Write("Predictor, ");
                    sw.Write("Target, ");
                    sw.Write("% Error");

                    sw.WriteLine();

                    for (int j = 0; j < numPairs; j++)
                    {
                        Pair_Of_Mets thisPair = thisInst.metPairList.metPairs[j];

                        sw.Write(thisPair.met1.name + ",");
                        sw.Write(thisPair.met2.name + ",");
                        int WS_PredInd = thisPair.GetWS_PredInd(theseModels, thisInst.modelList);

                        if (WD_Ind == numWD)
                            sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, r].percErr[0], 4).ToString("P"));
                        else
                            sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, r].percErrSector[0, WD_Ind], 4).ToString("P"));

                        sw.WriteLine();

                        sw.Write(thisPair.met2.name + ",");
                        sw.Write(thisPair.met1.name + ",");
                        if (WD_Ind == numWD)
                            sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, r].percErr[1], 4).ToString("P"));
                        else
                            sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, r].percErrSector[1, WD_Ind], 4).ToString("P"));

                        sw.WriteLine();

                    }
                }

                sw.Close();
            }

        }

        /// <summary> Exports calculations and estimates along path of nodes between selected start and end site </summary>   
        public void ExportNodesAndWS(Continuum thisInst)
        { 
            int numPairs = thisInst.metPairList.PairCount;
     //       if (numPairs == 0)
      //      {
      //          MessageBox.Show("Need to create met pairs and cross-predict first.  Click button on top left.", "Continuum 3");
      //          return;
      //      }

            Pair_Of_Mets thisPair = null;            
            int numWD = thisInst.GetNumWD();

            int radiusIndex = thisInst.GetRadiusInd("Advanced");
            int radius = thisInst.radiiList.investItem[radiusIndex].radius;
            int WD_Ind = thisInst.GetWD_ind("Advanced");
            
            Met.TOD thisTOD = thisInst.GetSelectedTOD("Advanced");
            Met.Season thisSeason = thisInst.GetSelectedSeason("Advanced");

            Model[] thisModel = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisTOD, thisSeason,
                thisInst.modeledHeight, false);

            string startStr = thisInst.GetStartMetAdvanced();
            string endStr = thisInst.GetEndSiteAdvanced();
            string headerString = "Continuum 3: Estimated and Calculated Values from " + startStr + " to " + endStr;

            int numMets = thisInst.metList.ThisCount;            
            StreamWriter sw;

            bool gotSR = thisInst.topo.gotSR;

            // Figure out if end is a met or a turbine
            bool endIsTurb = true;

            for (int i = 0; i < numMets; i++)
            {
                Met thisMet = thisInst.metList.metItem[i];
                if (thisMet.name == endStr)
                {
                    endIsTurb = false;
                    break;
                }
            }

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                }
                catch
                {
                    MessageBox.Show("Error writing to file.  Make sure the file is closed.", "Continuum 3");
                    return;
                }

                sw.WriteLine(headerString);
                sw.WriteLine(DateTime.Now.ToString());

                sw.WriteLine("radius, WD sector, Model");
                sw.Write(radius + ",");

                if (WD_Ind == numWD)
                    sw.Write("Overall,");
                else
                    sw.Write(WD_Ind.ToString() + ",");

                if (numMets == 1)
                    sw.Write("Default,");
                else
                    sw.Write("Site-Calibrated,");

                sw.WriteLine();
                sw.WriteLine();

                sw.Write("Met/Node, UTMX, UTMY, elev, P10 UW, P10 DW, UW Expo, DW Expo,");

                if (gotSR == true)
                    sw.Write("UW Roughness, DW Roughness, UW Disp H, DW Disp H,");

                sw.Write("WS Est,");
                if (endIsTurb == false)
                    sw.Write("WS Actual");

                sw.WriteLine();

                if (endIsTurb == false)
                {
                    bool is1to2 = false;
                    for (int i = 0; i < numPairs; i++)
                    {
                        if (startStr == thisInst.metPairList.metPairs[i].met1.name && endStr == thisInst.metPairList.metPairs[i].met2.name)
                        {
                            thisPair = thisInst.metPairList.metPairs[i];
                            is1to2 = true;
                            break;
                        }
                        else if (startStr == thisInst.metPairList.metPairs[i].met2.name && endStr == thisInst.metPairList.metPairs[i].met1.name)
                        {
                            thisPair = thisInst.metPairList.metPairs[i];
                            break;
                        }
                    }

                    int WS_PredInd = thisPair.GetWS_PredInd(thisModel, thisInst.modelList);
                    Met met1 = null;
                    Met met2 = null;

                    if (is1to2)
                    {
                        met1 = thisPair.met1;
                        met2 = thisPair.met2;
                    }
                    else
                    {
                        met1 = thisPair.met2;
                        met2 = thisPair.met1;
                    }

                    sw.Write(met1.name + "," + Math.Round(met1.UTMX, 0).ToString() + "," + Math.Round(met1.UTMY, 0).ToString() + "," + Math.Round(met1.elev, 1).ToString() + ",");
                    Met.WSWD_Dist met1Dist = met1.GetWS_WD_Dist(thisInst.modeledHeight, thisTOD, thisSeason);
                    Met.WSWD_Dist met2Dist = met2.GetWS_WD_Dist(thisInst.modeledHeight, thisTOD, thisSeason);

                    // Output Met 1 values
                    if (WD_Ind == numWD)
                    {
                        sw.Write(Math.Round(met1.gridStats.GetOverallP10(met1Dist.windRose, radiusIndex, "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(met1.gridStats.GetOverallP10(met1Dist.windRose, radiusIndex, "DW"), 3).ToString() + ",");
                        sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1Dist.windRose, "Expo", "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1Dist.windRose, "Expo", "DW"), 3).ToString() + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1Dist.windRose, "SR", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1Dist.windRose, "SR", "DW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1Dist.windRose, "DH", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1Dist.windRose, "DH", "DW"), 3).ToString() + ",");
                        }
                        sw.Write(met1Dist.WS.ToString() + ",");
                    }
                    else
                    {
                        sw.Write(Math.Round(met1.gridStats.stats[radiusIndex].P10_UW[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(met1.gridStats.stats[radiusIndex].P10_DW[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(met1.expo[radiusIndex].expo[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(met1.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo"), 3).ToString() + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(met1.expo[radiusIndex].SR[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(met1.expo[radiusIndex].GetDW_Param(WD_Ind, "SR"), 3).ToString() + ",");
                            sw.Write(Math.Round(met1.expo[radiusIndex].dispH[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(met1.expo[radiusIndex].GetDW_Param(WD_Ind, "DH"), 3).ToString() + ",");
                        }
                        sw.Write(Math.Round(met1Dist.WS * met1Dist.sectorWS_Ratio[WD_Ind], 3).ToString() + ",");
                    }

                    sw.WriteLine();

                    // Output values along nodes
                    int numNodes = thisPair.WS_Pred[WS_PredInd, radiusIndex].nodePath.Length;

                    for (int j = 0; j < numNodes; j++)
                    {
                        sw.Write("Node " + (j + 1) + ",");
                        Nodes this_Node = null;
                        if (is1to2)
                            this_Node = thisPair.WS_Pred[WS_PredInd, radiusIndex].nodePath[j];
                        else
                            this_Node = thisPair.WS_Pred[WS_PredInd, radiusIndex].nodePath[numNodes - j - 1];

                        sw.Write(Math.Round(this_Node.UTMX, 0).ToString() + "," + Math.Round(this_Node.UTMY, 0).ToString() + "," + Math.Round(this_Node.elev, 1).ToString() + ",");

                        if (WD_Ind == numWD)
                        {
                            double[] thisWindRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), this_Node.UTMX, this_Node.UTMY, thisTOD, thisSeason, thisInst.modeledHeight);
                            sw.Write(Math.Round(this_Node.gridStats.GetOverallP10(thisWindRose, radiusIndex, "UW"), 3) + ",");
                            sw.Write(Math.Round(this_Node.gridStats.GetOverallP10(thisWindRose, radiusIndex, "DW"), 3) + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "Expo", "UW"), 3) + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "Expo", "DW"), 3) + ",");
                            if (gotSR == true)
                            {
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "SR", "UW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "SR", "DW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "DH", "UW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "DH", "DW"), 3).ToString() + ",");
                            }
                            if (is1to2)
                                sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].nodeWS_Ests1to2[j], 3) + ",");
                            else
                                sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].nodeWS_Ests2to1[j], 3) + ",");
                        }
                        else
                        {
                            sw.Write(Math.Round(this_Node.gridStats.stats[radiusIndex].P10_UW[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.gridStats.stats[radiusIndex].P10_DW[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].expo[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo"), 3).ToString() + ",");
                            if (gotSR == true)
                            {
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].SR[WD_Ind], 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetDW_Param(WD_Ind, "SR"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].dispH[WD_Ind], 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetDW_Param(WD_Ind, "DH"), 3).ToString() + ",");
                            }
                            if (is1to2)
                                sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].nodeSectorWS_Ests1to2[j, WD_Ind], 3).ToString() + ",");
                            else
                                sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].nodeSectorWS_Ests2to1[j, WD_Ind], 3).ToString() + ",");
                        }

                        sw.WriteLine();

                    }

                    // Output Met 2 values
                    sw.Write(met2.name + "," + Math.Round(met2.UTMX, 0).ToString() + "," + Math.Round(met2.UTMY, 0).ToString() + "," + Math.Round(met2.elev, 1).ToString() + ",");

                    if (WD_Ind == numWD)
                    {
                        sw.Write(Math.Round(met2.gridStats.GetOverallP10(met2Dist.windRose, radiusIndex, "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(met2.gridStats.GetOverallP10(met2Dist.windRose, radiusIndex, "DW"), 3).ToString() + ",");
                        sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2Dist.windRose, "Expo", "UW"), 3) + ",");
                        sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2Dist.windRose, "Expo", "DW"), 3) + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2Dist.windRose, "SR", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2Dist.windRose, "SR", "DW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2Dist.windRose, "DH", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2Dist.windRose, "DH", "DW"), 3).ToString() + ",");
                        }
                        if (is1to2)
                            sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].WS_Ests[0], 3).ToString() + ",");
                        else
                            sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].WS_Ests[1], 3).ToString() + ",");

                        sw.Write(Math.Round(met2Dist.WS, 3).ToString() + ",");
                    }
                    else
                    {
                        sw.Write(Math.Round(met2.gridStats.stats[radiusIndex].P10_UW[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(met2.gridStats.stats[radiusIndex].P10_DW[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(met2.expo[radiusIndex].expo[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(met2.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo"), 3).ToString() + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(met2.expo[radiusIndex].SR[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(met2.expo[radiusIndex].GetDW_Param(WD_Ind, "SR"), 3).ToString() + ",");
                            sw.Write(Math.Round(met2.expo[radiusIndex].dispH[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(met2.expo[radiusIndex].GetDW_Param(WD_Ind, "DH"), 3).ToString() + ",");
                        }
                        sw.Write(Math.Round(met2Dist.WS * met2Dist.sectorWS_Ratio[WD_Ind], 3).ToString() + ",");
                    }

                }
                else
                {
                    // Met to turb      
                    Met thisMet = thisInst.metList.GetMet(startStr);
                    Met.WSWD_Dist thisDist = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, thisTOD, thisSeason);
                    Turbine thisTurb = thisInst.turbineList.GetTurbine(endStr);
                    Turbine.WS_Ests thisWS_Est = thisTurb.GetWS_Est(radius, thisMet.name, thisModel[radiusIndex]);

                    sw.Write(thisMet.name + "," + Math.Round(thisMet.UTMX, 0).ToString() + "," + Math.Round(thisMet.UTMY, 0).ToString() + "," + Math.Round(thisMet.elev, 1).ToString() + ",");

                    // Output Met 1 values
                    if (WD_Ind == numWD)
                    {
                        sw.Write(Math.Round(thisMet.gridStats.GetOverallP10(thisDist.windRose, radiusIndex, "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(thisMet.gridStats.GetOverallP10(thisDist.windRose, radiusIndex, "DW"), 3).ToString() + ",");
                        sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisDist.windRose, "Expo", "UW"), 3) + ",");
                        sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisDist.windRose, "Expo", "DW"), 3) + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisDist.windRose, "SR", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisDist.windRose, "SR", "DW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisDist.windRose, "DH", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisDist.windRose, "DH", "DW"), 3).ToString() + ",");
                        }
                        sw.Write(Math.Round(thisDist.WS, 3).ToString() + ",");
                    }
                    else
                    {
                        sw.Write(Math.Round(thisMet.gridStats.stats[radiusIndex].P10_UW[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(thisMet.gridStats.stats[radiusIndex].P10_DW[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(thisMet.expo[radiusIndex].expo[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(thisMet.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo"), 3).ToString() + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].SR[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].GetDW_Param(WD_Ind, "SR"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].dispH[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].GetDW_Param(WD_Ind, "DH"), 3).ToString() + ",");
                        }
                        sw.Write(Math.Round(thisDist.WS * thisDist.sectorWS_Ratio[WD_Ind], 3).ToString() + ",");
                    }

                    sw.WriteLine();

                    // Output values along nodes
                    int numNodes = thisWS_Est.pathOfNodes.Length;

                    for (int j = 0; j < numNodes; j++)
                    {
                        sw.Write("Node " + (j + 1).ToString() + ",");

                        Nodes this_Node = thisWS_Est.pathOfNodes[j];
                        double[] thisWindRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), this_Node.UTMX, this_Node.UTMY, thisTOD, thisSeason, thisModel[radiusIndex].height);
                        sw.Write(Math.Round(this_Node.UTMX, 0).ToString() + "," + Math.Round(this_Node.UTMY, 0).ToString() + "," + Math.Round(this_Node.elev, 1).ToString() + ",");

                        if (WD_Ind == numWD)
                        {
                            sw.Write(Math.Round(this_Node.gridStats.GetOverallP10(thisWindRose, radiusIndex, "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.gridStats.GetOverallP10(thisWindRose, radiusIndex, "DW"), 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "Expo", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "Expo", "DW"), 3).ToString() + ",");
                            if (gotSR == true)
                            {
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "SR", "UW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "SR", "DW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "DH", "UW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(thisWindRose, "DH", "DW"), 3).ToString() + ",");
                            }

                            sw.Write(Math.Round(thisWS_Est.WS_at_nodes[j], 3).ToString() + ",");
                        }
                        else
                        {
                            sw.Write(Math.Round(this_Node.gridStats.stats[radiusIndex].P10_UW[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.gridStats.stats[radiusIndex].P10_DW[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].expo[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo"), 3).ToString() + ",");
                            if (gotSR == true)
                            {
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].SR[WD_Ind], 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetDW_Param(WD_Ind, "SR"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].dispH[WD_Ind], 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetDW_Param(WD_Ind, "DH"), 3).ToString() + ",");
                            }

                            sw.Write(Math.Round(thisWS_Est.sectorWS_at_nodes[j, WD_Ind], 3).ToString() + ",");
                        }

                        sw.WriteLine();
                    }

                    // Output Turbine estimates / values
                    sw.Write(thisTurb.name + "," + Math.Round(thisTurb.UTMX, 0).ToString() + "," + Math.Round(thisTurb.UTMY, 0).ToString() + "," + Math.Round(thisTurb.elev, 1).ToString() + ",");

                    if (WD_Ind == numWD)
                    {
                        Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(null);
                        sw.Write(Math.Round(thisTurb.gridStats.GetOverallP10(avgEst.freeStream.windRose, radiusIndex, "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(thisTurb.gridStats.GetOverallP10(avgEst.freeStream.windRose, radiusIndex, "DW"), 3).ToString() + ",");
                        sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(avgEst.freeStream.windRose, "Expo", "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(avgEst.freeStream.windRose, "Expo", "DW"), 3).ToString() + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(avgEst.freeStream.windRose, "SR", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(avgEst.freeStream.windRose, "SR", "DW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(avgEst.freeStream.windRose, "DH", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(avgEst.freeStream.windRose, "DH", "DW"), 3).ToString() + ",");
                        }

                        sw.Write(Math.Round(thisWS_Est.WS, 3).ToString() + ",");
                    }
                    else
                    {
                        sw.Write(Math.Round(thisTurb.gridStats.stats[radiusIndex].P10_UW[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(thisTurb.gridStats.stats[radiusIndex].P10_DW[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(thisTurb.expo[radiusIndex].expo[WD_Ind], 3).ToString() + ",");
                        sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetDW_Param(WD_Ind, "Expo"), 3).ToString() + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].SR[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetDW_Param(WD_Ind, "SR"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].dispH[WD_Ind], 3).ToString() + ",");
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetDW_Param(WD_Ind, "DH"), 3).ToString() + ",");
                        }
                        sw.Write(Math.Round(thisWS_Est.sectorWS[WD_Ind], 3).ToString() + ",");
                    }

                }
                sw.Close();
            }
        }

        /// <summary> Exports power curve and thrust curve of specified curve </summary>        
        public void Export_CRV(Continuum thisInst, string powerCurve)
        {           
            int numPowerCurves = thisInst.turbineList.PowerCurveCount;
            TurbineCollection.PowerCurve thisPowerCurve = new TurbineCollection.PowerCurve();

            for (int i = 0; i < numPowerCurves; i++)
            {
                thisPowerCurve = thisInst.turbineList.powerCurves[i];

                if (thisPowerCurve.name == powerCurve)
                    break;
            }

            string headerString = "Continuum 3: Exported Power Curve";

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {

                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                sw.WriteLine(headerString);
                sw.WriteLine(DateTime.Now);

                sw.Write("Power Curve : ");
                sw.Write(thisPowerCurve.name);
                sw.WriteLine();

                sw.Write("Cut-In WS =");
                sw.Write(thisPowerCurve.cutInWS);
                sw.WriteLine();

                sw.Write("Cut-Out WS =");
                sw.Write(thisPowerCurve.cutOutWS);
                sw.WriteLine();

                sw.Write("Rated Power =");
                sw.Write(thisPowerCurve.ratedPower);
                sw.WriteLine();

                sw.WriteLine("WS [m/s], Power [kW], Thrust coeff");

                for (int i = 0; i < thisPowerCurve.power.Length; i++)
                {
                    sw.Write(thisPowerCurve.firstWS + i * thisPowerCurve.wsInt + ",");
                    sw.Write(thisPowerCurve.power[i] + ",");
                    sw.Write(thisPowerCurve.thrustCoeff[i]);
                    sw.WriteLine();
                }

                sw.Close();
            }
        }

        /// <summary> Exports results of all Round Robin analyses </summary> 
        public void Export_RoundRobin_CSV(Continuum thisInst)
        {
            MetPairCollection.RR_WS_Ests[] allRoundRobins = thisInst.metPairList.roundRobinEsts;
                        
            if (allRoundRobins == null)
            {
                MessageBox.Show("No Round Robin analyses have been generated yet.", "Continuum 3");
                return;
            }

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                try
                {
                    sw.WriteLine("Continuum 3: Modeled " + thisInst.modeledHeight + " m Wind Speeds: Round Robin Analysis Results");
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine();

                    for (int i = 0; i < allRoundRobins.Length; i++)
                    {
                        MetPairCollection.RR_WS_Ests thisRR = allRoundRobins[i];
                        sw.WriteLine("Round Robin Analysis: " + (i + 1) + " mets");

                        sw.Write("Using mets: ,");
                        sw.Write(thisInst.metList.CreateMetString(thisRR.metsUsed, true));
                        sw.WriteLine();

                        sw.Write("Num mets in Model,");
                        sw.Write("mets Used to Predict,");
                        sw.Write("mets Predicted,");
                        sw.Write("WS Estimate,");
                        sw.Write("WS Est Error,");
                        sw.WriteLine();

                        for (int j = 0; j <= thisRR.avgWS_Ests.GetUpperBound(0); j++)
                        { // for every predictee met
                            for (int k = 0; k <= thisRR.avgWS_Ests.GetUpperBound(1); k++)
                            {  // for every UW&DW model
                                string[] metsInThisModel = new string[thisRR.metSubSize];

                                for (int n = 0; n < metsInThisModel.Length; n++)
                                    metsInThisModel[n] = thisRR.metsInModel[n, k];

                                string metsInModelStr = metsInThisModel[0];

                                for (int n = 1; n < metsInThisModel.Length; n++)
                                    metsInModelStr = metsInModelStr + " " + metsInThisModel[n];


                                sw.Write(thisRR.metSubSize.ToString() + ",");
                                sw.Write(metsInModelStr + ",");
                                sw.Write(thisRR.avgWS_Ests[j, k].predictee + ",");
                                sw.Write(Math.Round(thisRR.avgWS_Ests[j, k].avgWS, 3).ToString() + ",");
                                sw.Write(thisRR.avgWS_Ests[j, k].estError.ToString("P") + ",");
                                sw.WriteLine();
                            }
                        }
                        sw.WriteLine();
                    }
                }
                catch
                {
                    MessageBox.Show("Error writing to file.", "Continuum 3");
                    return;
                }

                sw.Close();

            }
        }

        /// <summary> Exports selected Continuum model coefficients. </summary> 
        public void Export_Models_CSV(Continuum thisInst)
        {
            int numWD = thisInst.GetNumWD();
            int numRadii = thisInst.radiiList.ThisCount;

            bool useSR = false;
            if (thisInst.topo.useSR == true)
                useSR = true;

            bool useFlowSep = false;
            if (thisInst.topo.useSepMod == true)
                useFlowSep = true;

            bool useValley = false;
            if (thisInst.topo.useValley == true)
                useValley = true;

            bool useElev = false;
            if (thisInst.topo.useElev == true)
                useElev = true;

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                sw.WriteLine("Continuum 3 Model Parameters");
                sw.WriteLine(DateTime.Now);

                Model[] theseModels = thisInst.GetModels("Advanced");
                if (theseModels == null)
                    return;

                int numMetsUsed = theseModels[0].metsUsed.Length;
                string metStr = thisInst.metList.CreateMetString(theseModels[0].metsUsed, true);
                
                if (numMetsUsed == 1)
                    sw.WriteLine("Default Continuum Model");
                else
                    sw.WriteLine("Site-calibrated Model using " + numMetsUsed + " mets (" + metStr + ")");

                for (int i = 0; i < numRadii; i++)
                {
                    Model thisModel = theseModels[i];
                    sw.WriteLine("radius ," + thisInst.radiiList.investItem[i].radius);

                    // Overall wind speed cross-prediction error

                    sw.WriteLine("RMS WS Ests");
                    sw.Write(Math.Round(thisModel.RMS_WS_Est, 4).ToString("P"));
                    sw.WriteLine();

                    sw.Write("WD sect,");
                    sw.Write("RMS Sect. WS Ests,");

                    if (useElev)
                        sw.Write("Elevation coeff.,");

                    sw.Write("downhill_A,");
                    sw.Write("downhill_B,");
                    sw.Write("uphill_A,");
                    sw.Write("uphill_B,");
                    sw.Write("UW_Critical,");
                    sw.Write("SpeedUp_A,");
                    sw.Write("SpeedUp_B,");

                    if (useSR == true)
                    {
                        sw.Write("DH Stability_A,");
                        sw.Write("UH Stability_A,");
                        sw.Write("SU Stability_A,");
                    }

                    if (useFlowSep == true)
                    {
                        sw.Write("sep_A_DW,");
                        sw.Write("sep_B_DW,");
                        sw.Write("turbWS_Fact,");
                        sw.Write("sepCrit,");
                        sw.Write("Sep_crit_WS,");
                    }

                    sw.WriteLine();

                    for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                    {
                        sw.Write(Math.Round((WD_Ind * (double)360 / numWD), 1).ToString() + ",");

                        if (thisModel.RMS_Sect_WS_Est != null)
                            sw.Write(Math.Round(thisModel.RMS_Sect_WS_Est[WD_Ind], 5).ToString("P") + ",");
                        else
                            sw.Write("0.0,");

                        if (useElev)
                            sw.Write(Math.Round(thisModel.elevCoeff[WD_Ind], 5).ToString() + ",");

                        sw.Write(Math.Round(thisModel.downhill_A[WD_Ind], 5).ToString() + ",");
                        sw.Write(Math.Round(thisModel.downhill_B[WD_Ind], 5).ToString() + ",");
                        sw.Write(Math.Round(thisModel.uphill_A[WD_Ind], 5).ToString() + ",");
                        sw.Write(Math.Round(thisModel.uphill_B[WD_Ind], 5).ToString() + ",");
                        sw.Write(Math.Round(thisModel.UW_crit[WD_Ind], 5).ToString() + ",");
                        sw.Write(Math.Round(thisModel.spdUp_A[WD_Ind], 5).ToString() + ",");
                        sw.Write(Math.Round(thisModel.spdUp_B[WD_Ind], 5).ToString() + ",");

                        if (useSR == true)
                        {
                            sw.Write(Math.Round(thisModel.DH_Stab_A[WD_Ind], 5).ToString() + ",");
                            sw.Write(Math.Round(thisModel.UH_Stab_A[WD_Ind], 5).ToString() + ",");
                            sw.Write(Math.Round(thisModel.SU_Stab_A[WD_Ind], 5).ToString() + ",");
                        }

                        if (useFlowSep == true)
                        {
                            sw.Write(Math.Round(thisModel.sep_A_DW[WD_Ind], 5).ToString() + ",");
                            sw.Write(Math.Round(thisModel.sep_B_DW[WD_Ind], 5).ToString() + ",");
                            sw.Write(Math.Round(thisModel.turbWS_Fact[WD_Ind], 5).ToString() + ",");
                            sw.Write(Math.Round(thisModel.sepCrit[WD_Ind], 5).ToString() + ",");
                            sw.Write(Math.Round(thisModel.Sep_crit_WS[WD_Ind], 5).ToString() + ",");
                        }

                        sw.WriteLine();
                    }

                    sw.WriteLine();
                }
                sw.Close();
            }
        }

        /// <summary> Exports estimated WS and gross energy production at turbine sites. </summary> 
        public void Export_WS_AEP(Continuum thisInst, string powerCurve)
        {             
            if (thisInst.turbineList.GotEst("WS", new TurbineCollection.PowerCurve(), null) == true)
            {
                Met[] theseMets = thisInst.GetCheckedMets("Gross");
                Turbine[] theseTurbines = thisInst.GetCheckedTurbs("Gross");
                int numMets = theseMets.Count();
                int numTurbines = theseTurbines.Count();
                bool haveAEP = false;
                
                Export_Degs_or_UTM thisExport = new Export_Degs_or_UTM();
                thisExport.cbo_Lats_UTMs.SelectedIndex = 0;
                thisExport.ShowDialog();
                int latsOrUTMs = thisExport.cbo_Lats_UTMs.SelectedIndex; // 0 = Lats/Longs 1 = UTM coords

                if (thisInst.UTM_conversions.savedDatumIndex == 100)
                {
                    UTM_datum thisDatum = new UTM_datum();
                    thisDatum.cbo_Datums.SelectedIndex = 0;
                    thisDatum.ShowDialog();
                    thisInst.UTM_conversions.savedDatumIndex = thisDatum.cbo_Datums.SelectedIndex;
                }

                StreamWriter sw = null;                
                TurbineCollection.PowerCurve thisPowerCurve = new TurbineCollection.PowerCurve();

                if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                        string headerString = "Continuum 3: Estimated Gross WS and AEP at Met and Turbine Sites";
                        sw.WriteLine(headerString);

                        if (powerCurve == "No Power Curves Imported")
                            haveAEP = false;
                        else
                        {
                            headerString = "Using Power Curve: " + powerCurve;
                            sw.WriteLine(headerString);
                            haveAEP = true;
                            thisPowerCurve = thisInst.turbineList.GetPowerCurve(powerCurve);
                        }

                        if (latsOrUTMs == 1)
                        {
                            sw.WriteLine("UTM Zone: " + thisInst.UTM_conversions.UTMZoneNumber + " " + thisInst.UTM_conversions.hemisphere + " hemisphere");
                            sw.Write("Site,");
                            sw.Write("UTMX [m],");
                            sw.Write("UTMY [m],");
                        }
                        else
                        {
                            sw.Write("Site,");
                            sw.Write("Latitude [degs],");
                            sw.Write("Longitude [degs],");
                        }

                        sw.Write("Elev [m],");
                        sw.Write("WS [m/s],");
                        sw.Write("Weibull k,");
                        sw.Write("Weibull A,");

                        if (haveAEP == true)
                        {
                            sw.Write("AEP [MWh],");
                            sw.Write("CF [%],");
                        }

                        sw.WriteLine();

                        for (int i = 0; i < numMets; i++)
                        {
                            sw.Write(theseMets[i].name + ",");
                            Met.WSWD_Dist thisDist = theseMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                            if (latsOrUTMs == 1)
                            {
                                sw.Write(theseMets[i].UTMX.ToString() + ", ");
                                sw.Write(theseMets[i].UTMY.ToString() + ", ");
                            }
                            else
                            {
                                UTM_conversion.Lat_Long thisLL = new UTM_conversion.Lat_Long();
                                thisLL = thisInst.UTM_conversions.UTMtoLL(theseMets[i].UTMX, theseMets[i].UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ", ");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ", ");
                            }

                            sw.Write(Math.Round(theseMets[i].elev, 2).ToString() + ", ");
                            sw.Write(Math.Round(thisDist.WS, 3).ToString() + ", ");
                            MetCollection.Weibull_params weibull = thisInst.metList.CalcWeibullParams(thisDist.WS_Dist, thisDist.sectorWS_Dist, thisDist.WS);
                            sw.Write(Math.Round(weibull.overall_k, 2).ToString() + ", ");
                            sw.Write(Math.Round(weibull.overall_A, 2).ToString() + ", ");

                            if (haveAEP == true)
                            {
                                double metAEP = thisInst.turbineList.CalcAndReturnGrossAEP(thisDist.WS_Dist, thisInst.metList, powerCurve);
                                double metCF = thisInst.turbineList.CalcCapacityFactor(metAEP, thisPowerCurve.ratedPower);
                                sw.Write(Math.Round(metAEP, 1).ToString() + ", ");
                                sw.Write(Math.Round(metCF, 4).ToString("P") + ", ");
                            }

                            sw.WriteLine();
                        }

                        for (int i = 0; i < numTurbines; i++)
                        {                            
                            Turbine.Avg_Est avgEst = theseTurbines[i].GetAvgWS_Est(null);

                            sw.Write(theseTurbines[i].name + ",");

                            if (latsOrUTMs == 1)
                            {
                                sw.Write(theseTurbines[i].UTMX.ToString() + ", ");
                                sw.Write(theseTurbines[i].UTMY.ToString() + ", ");
                            }
                            else
                            {
                                UTM_conversion.Lat_Long thisLL = new UTM_conversion.Lat_Long();
                                thisLL = thisInst.UTM_conversions.UTMtoLL(theseTurbines[i].UTMX, theseTurbines[i].UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ", ");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ", ");

                            }

                            sw.Write(Math.Round(theseTurbines[i].elev, 2).ToString() + ", ");
                            sw.Write(Math.Round(avgEst.freeStream.WS, 3).ToString() + ", ");
                            sw.Write(Math.Round(avgEst.freeStream.weibullParams.overall_k, 2).ToString() + ", ");
                            sw.Write(Math.Round(avgEst.freeStream.weibullParams.overall_A, 2).ToString() + ", ");

                            if (haveAEP == true)
                            {
                                for (int j = 0; j < theseTurbines[i].GrossAEP_Count; j++)
                                {
                                    if (theseTurbines[i].grossAEP[j].powerCurve.name == powerCurve)
                                    {
                                        sw.Write(Math.Round(theseTurbines[i].grossAEP[j].AEP, 1).ToString() + ", ");
                                        sw.Write(Math.Round(theseTurbines[i].grossAEP[j].CF, 4).ToString("P") + ", ");
                                    }
                                }
                            }

                            sw.WriteLine();
                        }

                        sw.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error writing to file.", "Continuum 3");
                        sw.Close();
                    }
                }
            }
            else
                MessageBox.Show("Need to do Turbine calculations first.", "Continuum 3");

        }

        /// <summary> Exports estimated WS and net energy production and wake loss at turbine sites. </summary> 
        public void Export_Net_WS_AEP(Continuum thisInst)
        { 
            if (thisInst.wakeModelList.NumWakeModels > 0)
            {
                Turbine[] turbsToExport = thisInst.GetCheckedTurbs("Net");

                int numWD = thisInst.GetNumWD();
                int WD_Ind = thisInst.GetWD_ind("Net");
                Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();
                string Wake_model_name = thisInst.wakeModelList.GetWakeModelName(thisWakeModel.wakeModelType);

                string Model_type = "";
                
                if (thisInst.metList.ThisCount == 1)
                    Model_type = "Default Continuum Model";
                else
                    Model_type = "Site-calibrated Continuum Model";

                Export_Degs_or_UTM thisExport = new Export_Degs_or_UTM();
                thisExport.cbo_Lats_UTMs.SelectedIndex = 0;
                thisExport.ShowDialog();
                int latsOrUTMs = thisExport.cbo_Lats_UTMs.SelectedIndex; // 0 = Lats/Longs 1 = UTM coords

                if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                        sw.WriteLine("Continuum 3: Estimated Net WS and AEP at Turbine Sites");
                        sw.WriteLine("Using Power Curve: " + thisWakeModel.powerCurve.name);
                        sw.WriteLine("Using Wake Model: " + Wake_model_name);
                        sw.WriteLine("Using Continuum Model: " + Model_type);
                        if (WD_Ind == numWD)
                            sw.WriteLine("Overall Estimates");
                        else
                            sw.WriteLine("Estimates for WD sector: " + Math.Round(WD_Ind * (double)360 / numWD, 1).ToString() + " degs,");

                        if (latsOrUTMs == 1)
                        {
                            sw.WriteLine("UTM Zone: " + thisInst.UTM_conversions.UTMZoneNumber + " " + thisInst.UTM_conversions.hemisphere + " hemisphere");
                            sw.Write("Site,");
                            sw.Write("String #,");
                            sw.Write("UTMX,");
                            sw.Write("UTMY,");
                        }
                        else
                        {
                            sw.Write("Site,");
                            sw.Write("String #,");
                            sw.Write("Latitude,");
                            sw.Write("Longitude,");
                        }

                        sw.Write("Elev [m],");
                        sw.Write("Free-stream WS [m/s],");
                        sw.Write("Waked WS [m/s],");
                        sw.Write("Net AEP [MWh],");
                        sw.Write("Wake Loss [%],");
                        sw.Write("Weibull k,");
                        sw.Write("Weibull A,");

                        sw.WriteLine();

                        for (int i = 0; i < turbsToExport.Length; i++)
                        {
                            Turbine thisTurb = turbsToExport[i];

                            sw.Write(thisTurb.name + ",");
                            sw.Write(thisTurb.stringNum + ",");

                            if (latsOrUTMs == 1)
                            {
                                sw.Write(thisTurb.UTMX.ToString() + ",");
                                sw.Write(thisTurb.UTMY.ToString() + ",");
                            }
                            else
                            {
                                UTM_conversion.Lat_Long thisLL;
                                if (thisInst.UTM_conversions.savedDatumIndex == 100)
                                {
                                    UTM_datum thisDatum = new UTM_datum();
                                    thisDatum.cbo_Datums.SelectedIndex = 0;
                                    thisDatum.ShowDialog();
                                    thisInst.UTM_conversions.savedDatumIndex = thisDatum.cbo_Datums.SelectedIndex;
                                }

                                thisLL = thisInst.UTM_conversions.UTMtoLL(thisTurb.UTMX, thisTurb.UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");
                            }

                            sw.Write(Math.Round(thisTurb.elev, 2).ToString() + ",");

                            double Avg_WS_est = thisTurb.GetAvgOrSectorWS_Est(thisWakeModel, WD_Ind, "WS", thisWakeModel.powerCurve); // Net average wind speed estimate
                            double FS_Avg_WS_est = thisTurb.GetAvgOrSectorWS_Est(null, WD_Ind, "WS", thisWakeModel.powerCurve); // Gross (i.e. unwaked) average wind speed estimate
                            double Net_Est = thisTurb.GetNetAEP(thisWakeModel, WD_Ind); // Net AEP   
                            double wakeLoss = thisTurb.GetWakeLoss(thisWakeModel, WD_Ind);
                            double Weib_k = thisTurb.GetAvgOrSectorWS_Est(thisWakeModel, WD_Ind, "WeibK", thisWakeModel.powerCurve);
                            double Weib_A = thisTurb.GetAvgOrSectorWS_Est(thisWakeModel, WD_Ind, "WeibA", thisWakeModel.powerCurve);

                            sw.Write(Math.Round(FS_Avg_WS_est, 3) + ",");
                            sw.Write(Math.Round(Avg_WS_est, 3) + ",");
                            sw.Write(Math.Round(Net_Est, 1) + ",");
                            sw.Write(Math.Round(100 * wakeLoss, 1) + ",");
                            sw.Write(Math.Round(Weib_k, 3) + ",");
                            sw.Write(Math.Round(Weib_A, 3) + ",");

                            sw.WriteLine();
                        }

                        // Export other losses
                        sw.WriteLine();
                        sw.WriteLine("Summary of Loss factors,");

                        Exceedance exceed = thisInst.turbineList.exceed;

                        for (int i = 0; i < exceed.Num_Exceed(); i++)
                        {
                            Exceedance.ExceedanceCurve thisCurve = exceed.exceedCurves[i];
                            sw.WriteLine(thisCurve.exceedStr + "," + Math.Round(exceed.Get_PF_Value(0.5, thisCurve), 4).ToString("P"));
                        }

                        sw.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error writing to file.", "Continuum 3");
                    }
                }
            }
            else
                MessageBox.Show("Need to do Turbine calculations first.", "Continuum 3");

        }

        /// <summary> Exports either gross or net sectorwise WS at turbine sites and met sites. </summary> 
        public void Export_Directional_WS(Continuum thisInst, string tabName)
        {             
            if (thisInst.turbineList.GotEst("WS", new TurbineCollection.PowerCurve(), null) == true)
            {
                int numWD = thisInst.GetNumWD();

                Met[] theseMets = new Met[0];
                Turbine[] theseTurbines = new Turbine[0];
                Wake_Model thisWakeModel = null;

                if (tabName == "Gross")
                {
                    theseMets = thisInst.GetCheckedMets("Gross");
                    theseTurbines = thisInst.GetCheckedTurbs("Gross");
                }
                else
                {
                    theseTurbines = thisInst.GetCheckedTurbs("Net");
                    thisWakeModel = thisInst.GetSelectedWakeModel();
                }

                int numTurbines = theseTurbines.Count();
                int numMets = theseMets.Count();                                

                Export_Degs_or_UTM thisExport = new Export_Degs_or_UTM();
                thisExport.cbo_Lats_UTMs.SelectedIndex = 0;
                thisExport.ShowDialog();
                int latsOrUTMs = thisExport.cbo_Lats_UTMs.SelectedIndex; // 0 = Lats/Longs 1 = UTM coords

                if (thisInst.UTM_conversions.savedDatumIndex == 100)
                {
                    UTM_datum thisDatum = new UTM_datum();
                    thisDatum.cbo_Datums.SelectedIndex = 0;
                    thisDatum.ShowDialog();
                    thisInst.UTM_conversions.savedDatumIndex = thisDatum.cbo_Datums.SelectedIndex;
                }

                double dirBin = (double)360 / numWD;

                Turbine.Avg_Est avgEst = new Turbine.Avg_Est();

                if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                        if (tabName == "Gross")
                            sw.WriteLine("Continuum 3: Gross Directional WS [m/s] at Met and Turbine Sites");
                        else
                            sw.WriteLine("Continuum 3: Net Directional WS [m/s] at Turbine Sites");

                        if (latsOrUTMs == 1)
                        {
                            sw.WriteLine("UTM Zone: " + thisInst.UTM_conversions.UTMZoneNumber + " " + thisInst.UTM_conversions.hemisphere + " hemisphere");
                            sw.Write("Site,");
                            sw.Write("UTMX [m],");
                            sw.Write("UTMY [m],");
                        }
                        else
                        {
                            sw.Write("Site,");
                            sw.Write("Latitude [degs],");
                            sw.Write("Longitude [degs],");
                        }

                        sw.Write("Elev [m],");
                        for (int WD = 0; WD < numWD; WD++)
                            sw.Write((WD * dirBin).ToString() + ",");

                        sw.WriteLine();

                        for (int i = 0; i < numMets; i++)
                        {
                            sw.Write(theseMets[i].name + ",");
                            Met.WSWD_Dist thisDist = theseMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                            if (latsOrUTMs == 1)
                            {
                                sw.Write(theseMets[i].UTMX + ",");
                                sw.Write(theseMets[i].UTMY + ",");
                            }
                            else
                            {
                                UTM_conversion.Lat_Long thisLL = new UTM_conversion.Lat_Long();
                                thisLL = thisInst.UTM_conversions.UTMtoLL(theseMets[i].UTMX, theseMets[i].UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");
                            }

                            sw.Write(Math.Round(theseMets[i].elev, 2).ToString() + ",");
                            for (int WD = 0; WD < numWD; WD++)
                                sw.Write(Math.Round(thisDist.WS * thisDist.sectorWS_Ratio[WD], 3).ToString() + ",");

                            sw.WriteLine();
                        }

                        for (int i = 0; i < numTurbines; i++)
                        {
                            if (tabName == "Gross")
                                avgEst = theseTurbines[i].GetAvgWS_Est(null); // just want free-stream WS
                            else
                                avgEst = theseTurbines[i].GetAvgWS_Est(thisWakeModel);

                            sw.Write(theseTurbines[i].name + ",");

                            if (latsOrUTMs == 1)
                            {
                                sw.Write(theseTurbines[i].UTMX + ",");
                                sw.Write(theseTurbines[i].UTMY + ",");
                            }
                            else
                            {
                                UTM_conversion.Lat_Long thisLL = new UTM_conversion.Lat_Long();
                                thisLL = thisInst.UTM_conversions.UTMtoLL(theseTurbines[i].UTMX, theseTurbines[i].UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");
                            }

                            sw.Write(Math.Round(theseTurbines[i].elev, 2).ToString() + ",");
                            for (int WD = 0; WD <= numWD - 1; WD++)
                            {
                                if (tabName == "Gross")
                                    sw.Write(Math.Round(avgEst.freeStream.sectorWS[WD], 3).ToString() + ",");
                                else
                                    sw.Write(Math.Round(avgEst.waked.sectorWS[WD], 3).ToString() + ",");
                            }                                

                            sw.WriteLine();
                        }

                        // Weibull Shape Factors
                        if (tabName == "Gross")
                            sw.WriteLine("Continuum 3: Gross Directional Weibull Shape factor at Met and Turbine Sites");
                        else
                            sw.WriteLine("Continuum 3: Net Directional Weibull Shape factor at Turbine Sites");

                        sw.Write("Site,");
                        if (latsOrUTMs == 1)
                        {
                            sw.Write("UTMX [m],");
                            sw.Write("UTMY [m],");
                        }
                        else
                        {
                            sw.Write("Latitude [degs],");
                            sw.Write("Longitude [degs],");
                        }

                        sw.Write("Elev [m],");
                        for (int WD = 0; WD < numWD; WD++)
                            sw.Write((WD * dirBin).ToString() + ",");

                        sw.WriteLine();

                        for (int i = 0; i < numMets; i++)
                        {
                            sw.Write(theseMets[i].name + ",");
                            Met.WSWD_Dist thisDist = theseMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                            if (latsOrUTMs == 1)
                            {
                                sw.Write(theseMets[i].UTMX + ",");
                                sw.Write(theseMets[i].UTMY + ",");
                            }
                            else
                            {
                                UTM_conversion.Lat_Long thisLL = new UTM_conversion.Lat_Long();
                                thisLL = thisInst.UTM_conversions.UTMtoLL(theseMets[i].UTMX, theseMets[i].UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");
                            }

                            sw.Write(Math.Round(theseMets[i].elev, 2).ToString() + ",");
                            MetCollection.Weibull_params weibull = thisInst.metList.CalcWeibullParams(thisDist.WS_Dist, thisDist.sectorWS_Dist, thisDist.WS);
                            for (int WD = 0; WD < numWD; WD++)
                                sw.Write(Math.Round(weibull.sector_k[WD], 3).ToString() + ",");

                            sw.WriteLine();
                        }

                        for (int i = 0; i < numTurbines; i++)
                        {
                            if (tabName == "Gross")
                                avgEst = theseTurbines[i].GetAvgWS_Est(null);
                            else
                                avgEst = theseTurbines[i].GetAvgWS_Est(thisWakeModel);

                            sw.Write(theseTurbines[i].name + ",");

                            if (latsOrUTMs == 1)
                            {
                                sw.Write(theseTurbines[i].UTMX.ToString() + ",");
                                sw.Write(theseTurbines[i].UTMY.ToString() + ",");
                            }
                            else
                            {
                                UTM_conversion.Lat_Long thisLL = thisInst.UTM_conversions.UTMtoLL(theseTurbines[i].UTMX, theseTurbines[i].UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");
                            }

                            sw.Write(Math.Round(theseTurbines[i].elev, 2).ToString() + ",");
                            for (int WD = 0; WD <= numWD - 1; WD++)
                            {
                                if (tabName == "Gross")
                                    sw.Write(Math.Round(avgEst.freeStream.weibullParams.sector_k[WD], 3).ToString() + ",");
                                else
                                    sw.Write(Math.Round(avgEst.waked.weibullParams.sector_k[WD], 3).ToString() + ",");
                            }                                

                            sw.WriteLine();
                        }

                        // weibull Scale Factors
                        if (tabName == "Gross")
                            sw.WriteLine("Continuum 3: Gross Directional Weibull Scale factor at Met and Turbine Sites");
                        else
                            sw.WriteLine("Continuum 3: Net Directional Weibull Scale factor at Turbine Sites");

                        sw.Write("Site,");
                        if (latsOrUTMs == 1)
                        {
                            sw.Write("UTMX [m],");
                            sw.Write("UTMY [m],");
                        }
                        else
                        {
                            sw.Write("Latitude [degs],");
                            sw.Write("Longitude [degs],");
                        }

                        sw.Write("Elev [m],");
                        for (int WD = 0; WD < numWD; WD++)
                            sw.Write((WD * dirBin).ToString() + ",");

                        sw.WriteLine();

                        for (int i = 0; i < numMets; i++)
                        {
                            sw.Write(theseMets[i].name + ",");
                            Met.WSWD_Dist thisDist = theseMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                            if (latsOrUTMs == 1)
                            {
                                sw.Write(theseMets[i].UTMX + ",");
                                sw.Write(theseMets[i].UTMY + ",");
                            }
                            else
                            {
                                UTM_conversion.Lat_Long thisLL = new UTM_conversion.Lat_Long();
                                thisLL = thisInst.UTM_conversions.UTMtoLL(theseMets[i].UTMX, theseMets[i].UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");
                            }

                            sw.Write(Math.Round(theseMets[i].elev, 2).ToString() + ",");
                            MetCollection.Weibull_params weibull = thisInst.metList.CalcWeibullParams(thisDist.WS_Dist, thisDist.sectorWS_Dist, thisDist.WS);
                            for (int WD = 0; WD <= numWD - 1; WD++)
                                sw.Write(Math.Round(weibull.sector_A[WD], 3).ToString() + ",");

                            sw.WriteLine();
                        }

                        for (int i = 0; i <= numTurbines - 1; i++)
                        {
                            if (tabName == "Gross")
                                avgEst = theseTurbines[i].GetAvgWS_Est(null);
                            else
                                avgEst = theseTurbines[i].GetAvgWS_Est(thisWakeModel);

                            sw.Write(theseTurbines[i].name + ",");
                            if (latsOrUTMs == 1)
                            {
                                sw.Write(theseTurbines[i].UTMX.ToString() + ",");
                                sw.Write(theseTurbines[i].UTMY.ToString() + ",");
                            }
                            else
                            {
                                UTM_conversion.Lat_Long thisLL;
                                thisLL = thisInst.UTM_conversions.UTMtoLL(theseTurbines[i].UTMX, theseTurbines[i].UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");
                            }

                            sw.Write(Math.Round(theseTurbines[i].elev, 2).ToString() + ",");
                            for (int WD = 0; WD <= numWD - 1; WD++)
                            {
                                if (tabName == "Gross")
                                    sw.Write(Math.Round(avgEst.freeStream.weibullParams.sector_A[WD], 3).ToString() + ",");
                                else
                                    sw.Write(Math.Round(avgEst.waked.weibullParams.sector_A[WD], 3).ToString() + ",");
                            }                                

                            sw.WriteLine();
                        }

                        sw.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error writing to file.", "Continuum 3");                        
                    }
                }
            }
            else
                MessageBox.Show("Need to do Turbine calculations first.", "Continuum 3");

        }

        /// <summary> Exports P50/P90/P99 wind speed and energy production estimates at turbine sites. </summary> 
        public void Export_WS_AEP_Uncert(Continuum thisInst, string powerCurve)
        {             
            int numTurbines = thisInst.turbineList.TurbineCount;
            if (numTurbines == 0)
            {
                MessageBox.Show("No Turbines have been loaded yet. Go to Input tab to import turbine sites.", "Continuum 3");
                return;
            }
                        
            string modelStr = "";

            if (thisInst.metList.ThisCount == 1)
                modelStr = "Default Model";
            else
                modelStr = "Site-Calibrated Model";

            Export_Degs_or_UTM thisExport = new Export_Degs_or_UTM();
            thisExport.cbo_Lats_UTMs.SelectedIndex = 0;
            thisExport.ShowDialog();
            int latsOrUTMs = thisExport.cbo_Lats_UTMs.SelectedIndex; // 0 = Lats/Longs 1 = UTM coords

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                    sw.WriteLine("Continuum 3: Estimated P50 P90 & P99 WS and AEP at Turbine Sites");
                    sw.WriteLine(modelStr);
                    bool haveAEP;

                    if (latsOrUTMs == 1)
                        sw.WriteLine("UTM Zone: " + thisInst.UTM_conversions.UTMZoneNumber + " " + thisInst.UTM_conversions.hemisphere + " hemisphere");

                    if (powerCurve == "No Power Curves Imported")
                    {
                        haveAEP = false;
                    }
                    else
                    {
                        sw.WriteLine("Using Power Curve: " + powerCurve);
                        haveAEP = true;
                    }

                    sw.Write("Site,");
                    if (latsOrUTMs == 0)
                        sw.Write("Latitude, Longitude,");
                    else
                    {
                        sw.Write("UTMX, UTMY,");
                    }

                    sw.Write("Elev [m], WS [m/s], WS Uncert [%], WS P90 [m/s], WS P99 [m/s],");

                    if (haveAEP)                    
                        sw.Write("AEP [MWh], AEP P90 [MWh], AEP P99 [MWh]");                    

                    sw.WriteLine();

                    for (int i = 0; i < numTurbines; i++)
                    {
                        Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                        Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(null);

                        sw.Write(thisTurb.name + ",");

                        if (latsOrUTMs == 1)
                        {
                            sw.Write(thisTurb.UTMX.ToString() + ",");
                            sw.Write(thisTurb.UTMY.ToString() + ",");
                        }
                        else
                        {
                            if (thisInst.UTM_conversions.savedDatumIndex == 100)
                            {
                                UTM_datum thisDatum = new UTM_datum();
                                thisDatum.cbo_Datums.SelectedIndex = 0;
                                thisDatum.ShowDialog();
                                thisInst.UTM_conversions.savedDatumIndex = thisDatum.cbo_Datums.SelectedIndex;
                            }

                            UTM_conversion.Lat_Long thisLL = thisInst.UTM_conversions.UTMtoLL(thisTurb.UTMX, thisTurb.UTMY);
                            sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                            sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");

                        }
                        sw.Write(Math.Round(thisTurb.elev, 2).ToString() + ",");
                        sw.Write(Math.Round(avgEst.freeStream.WS, 3).ToString() + ",");

                        double thisP90 = avgEst.freeStream.WS - avgEst.freeStream.WS * avgEst.uncert * 1.28155f;
                        double thisP99 = avgEst.freeStream.WS - avgEst.freeStream.WS * avgEst.uncert * 2.326f;

                        sw.Write(Math.Round(avgEst.uncert, 5).ToString("P") + ",");
                        sw.Write(Math.Round(thisP90, 3).ToString() + ",");
                        sw.Write(Math.Round(thisP99, 3).ToString() + ",");

                        if (haveAEP == true)
                        {
                            for (int j = 0; j <= thisTurb.GrossAEP_Count - 1; j++)
                            {
                                if (thisTurb.grossAEP[j].powerCurve.name == powerCurve) 
                                {
                                    sw.Write(Math.Round(thisTurb.grossAEP[j].AEP, 1).ToString() + ",");
                                    sw.Write(Math.Round(thisTurb.grossAEP[j].P90, 1).ToString() + ",");
                                    sw.Write(Math.Round(thisTurb.grossAEP[j].P99, 1).ToString() + ",");
                                }
                            }
                        }
                        sw.WriteLine();
                    }
                    sw.Close();
                }
                catch
                {
                    MessageBox.Show("Error writing to file.", "Continuum 3");
                }
            }
        }

        /// <summary> Exports overall net (i.e. waked) wind speed distributions estimated at met and turbine sites. </summary> 
        public void ExportNetWS_Dists(Continuum thisInst)
        {                        
            int numTurbinesToExport = thisInst.chkTurbNet.CheckedItems.Count;

            if (thisInst.metList.ThisCount == 0)
                return;

            if (thisInst.metList.metItem[0].WSWD_DistCount == 0)
                return;

            int numWS = thisInst.metList.numWS;
            double WS_first = thisInst.metList.WS_FirstInt;
            double WS_int = thisInst.metList.WS_IntSize;
            int numWD = thisInst.GetNumWD();
            int WD_Ind = thisInst.GetWD_ind("Net");
            Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();

            double[,] turbineWS = new double[numTurbinesToExport, numWS];
            
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {                
                StreamWriter sw = null;

                try
                {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                }
                catch
                {
                    MessageBox.Show("Error writing to file.");
                    sw.Close();
                    return;
                }

                sw.WriteLine("Continuum 3: Waked Wind Speed Distributions at Turbine Sites");
                if (WD_Ind == numWD)
                    sw.WriteLine("Overall WS Distributions");
                else
                    sw.WriteLine("WS Distribution for WD sector: " + Math.Round(WD_Ind * (double)360 / numWD, 1).ToString() + " degs,");

                sw.WriteLine();

                sw.Write("WS [m/s],");

                for (int i = 0; i < thisInst.chkTurbNet.CheckedItems.Count; i++)
                    sw.Write(thisInst.chkTurbNet.CheckedItems[i].ToString() + ",");

                sw.WriteLine();

                for (int i = 0; i < numTurbinesToExport; i++)
                {
                    Turbine thisTurb = thisInst.turbineList.GetTurbine(thisInst.chkTurbNet.CheckedItems[i].ToString());
                    Turbine.Avg_Est avgWSEst = thisTurb.GetAvgWS_Est(thisWakeModel);

                    for (int j = 0; j < numWS; j++)                    
                        turbineWS[i, j] = avgWSEst.waked.WS_Dist[j];                    
                }

                for (int i = 0; i < numWS; i++)
                {
                    double WS_Val = WS_first + i * WS_int - WS_int / 2;
                    sw.Write(Math.Round(WS_Val, 1).ToString() + ",");

                    for (int j = 0; j <= numTurbinesToExport - 1; j++)
                        sw.Write(Math.Round(turbineWS[j, i], 3).ToString() + ",");

                    sw.WriteLine();
                }

                sw.Close();
            }
        }

        /// <summary> Exports overall gross wind speed distributions estimated at met and turbine sites. </summary> 
        public void ExportGrossWS_Dists(Continuum thisInst)
        {                         
            Met[] theseMets = thisInst.GetCheckedMets("Gross");
            Turbine[] theseTurbines = thisInst.GetCheckedTurbs("Gross");
            int numTurbines = theseTurbines.Count();
            int numMets = theseMets.Count();

            if (numMets + numTurbines == 0)
                return;                        

            if (thisInst.metList.ThisCount == 0)
                return;

            if (thisInst.metList.metItem[0].WSWD_DistCount == 0)
                return;

            int numWS = thisInst.metList.numWS;
            double WS_first = thisInst.metList.WS_FirstInt;
            double WS_int = thisInst.metList.WS_IntSize;                                        

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw;

                try
                {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                }
                catch
                {
                    MessageBox.Show("Error writing to file.");
                    return;
                }

                sw.WriteLine("Continuum 3: Gross Wind Speed Distributions at Met and Turbine Sites");
                sw.WriteLine();

                sw.Write("WS [m/s],");

                for (int i = 0; i < numMets; i++)
                    sw.Write(theseMets[i].name + ",");

                for (int i = 0; i < numTurbines; i++)
                    sw.Write(theseTurbines[i].name + ",");

                sw.WriteLine();

                for (int i = 0; i < numWS; i++)
                {
                    sw.Write(WS_first + i * WS_int - WS_int / 2 + ",");

                    for (int j = 0; j <= numMets - 1; j++)
                    {
                        Met.WSWD_Dist thisDist = theseMets[j].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                        sw.Write(thisDist.WS_Dist[i] + ",");
                    }

                    for (int j = 0; j <= numTurbines - 1; j++)
                    {
                        Turbine.Avg_Est avgEst = theseTurbines[j].GetAvgWS_Est(null);
                        sw.Write(avgEst.freeStream.WS_Dist[i] + ",");
                    }

                    sw.WriteLine();
                }

                sw.Close();
            }
        }

        /// <summary> Exports sectorwise gross wind speed distributions estimated at met and turbine sites. </summary> 
        public void ExportDirectionalWS_Dists(Continuum thisInst)
        {            
            Met[] theseMets = thisInst.GetCheckedMets("Gross");
            Turbine[] theseTurbines = thisInst.GetCheckedTurbs("Gross");

            int numTurbines = theseTurbines.Count();
            int numMets = theseMets.Count();

            if (numTurbines + numMets == 0)
                return;

            int numWD = thisInst.GetNumWD();            
            
            if (thisInst.metList.ThisCount == 0)
                return;

            if (thisInst.metList.metItem[0].WSWD_DistCount == 0)
                return;

            int numWS = thisInst.metList.numWS;
            double WS_first = thisInst.metList.WS_FirstInt;
            double WS_int = thisInst.metList.WS_IntSize;                        

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = null;

                try
                {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                }
                catch
                {
                    MessageBox.Show("Error writing to file.");
                    sw.Close();
                    return;
                }

                sw.WriteLine("Continuum 3: Gross Directional Wind Speed Distributions at mets and Estimated at Turbine Sites");
                
                for (int i = 0; i < numMets; i++)
                {
                    sw.WriteLine(theseMets[i].name);
                    Met.WSWD_Dist thisDist = theseMets[i].GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

                    sw.Write("WS [m/s],");
                    for (int WD = 0; WD < numWD; WD++)
                        sw.Write(WD * (double)360 / numWD + ",");

                    sw.WriteLine();

                    for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
                    {
                        sw.Write((Math.Round(WS_first + WS_ind * WS_int - WS_int / 2, 1)).ToString() + ",");
                        for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                            sw.Write(Math.Round(thisDist.sectorWS_Dist[WD_Ind, WS_ind], 2) + ",");

                        sw.WriteLine();
                    }

                    sw.WriteLine();
                }

                for (int i = 0; i <= numTurbines - 1; i++)
                {
                    sw.WriteLine(theseTurbines[i].name);

                    sw.Write("WS [m/s],");
                    for (int WD = 0; WD <= numWD - 1; WD++)
                        sw.Write(WD * (double)360 / numWD + ",");

                    sw.WriteLine();

                    Turbine.Avg_Est avgEst = theseTurbines[i].GetAvgWS_Est(null);

                    for (int WS_ind = 0; WS_ind <= numWS - 1; WS_ind++)
                    {
                        sw.Write(Math.Round(WS_first + WS_ind * WS_int - WS_int / 2, 1) + ",");
                        for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)
                            sw.Write(Math.Round(avgEst.freeStream.sectorWS_Dist[WD_Ind, WS_ind], 2) + ",");

                        sw.WriteLine();
                    }
                    sw.WriteLine();
                }

                sw.Close();

            }
        }

        /// <summary> Exports sectorwise net wind speed distributions estimated at met and turbine sites. </summary> 
        public void ExportNetDirectionalWS_Dists(Continuum thisInst)
        {          
            int numWD = thisInst.GetNumWD();
            if (numWD == 0) return;

            if (thisInst.metList.metItem[0].WSWD_DistCount == 0)
                return;

            int numWS = thisInst.metList.numWS;
            double WS_first = thisInst.metList.WS_FirstInt;
            double WS_int = thisInst.metList.WS_IntSize;

            Turbine[] turbsToExport = thisInst.GetCheckedTurbs("Net");
            int numTurbinesToExport = turbsToExport.Length;
                                    
            Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();

            if (thisWakeModel == null)
                return;

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = null;

                try
                {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                }
                catch
                {
                    MessageBox.Show("Error writing to file.");
                    sw.Close();
                    return;
                }

                sw.WriteLine("Continuum 3: Directional Wind Speed Distributions Estimated at Turbine Sites");

                for (int i = 0; i < numTurbinesToExport; i++)
                {
                    sw.WriteLine(turbsToExport[i].name);

                    sw.Write("WS [m/s],");
                    for (int WD = 0; WD <= numWD - 1; WD++)
                        sw.Write(Math.Round(WD * (double)360 / numWD, 1) + ",");

                    sw.WriteLine();

                    Turbine.Avg_Est avgEst = turbsToExport[i].GetAvgWS_Est(thisWakeModel);

                    for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
                    {
                        sw.Write(Math.Round(WS_first + WS_ind * WS_int - WS_int / 2, 1) + ",");

                        for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                            sw.Write(Math.Round(avgEst.waked.sectorWS_Dist[WD_Ind, WS_ind], 2) + ",");

                        sw.WriteLine();
                    }
                    sw.WriteLine();
                }

                sw.Close();

            }
        }

        /// <summary> Exports exposure, surface roughness, and displacement height (sectorwise and overall) at met and turbine sites. </summary> 
        public void ExportExpos(Continuum thisInst, string[] radii, string[] mets, string[] turbs, bool sectorOut, bool bulkOut, int numSectors, bool gotSR, bool Sector_SRDH_out, bool Bulk_SRDH_out)
        {            
            DateTime Date_String = DateTime.Now;
            int numWD = thisInst.GetNumWD();

            if (numWD == 1)
            {
                MessageBox.Show("You need to import met files first.", "Continuum 3");
                return;
            }

            try
            {
                if (thisInst.sfdExpos.ShowDialog() == DialogResult.OK)
                {

                    StreamWriter sw = new StreamWriter(thisInst.sfdExpos.FileName);
                    sw.WriteLine("Continuum 3: Calculated Exposures and Elevations");
                    sw.WriteLine(Date_String);

                    if (thisInst.savedParams.savedFileName != "")
                        sw.WriteLine(thisInst.savedParams.savedFileName);

                    try
                    {
                        sw.WriteLine();

                        for (int rad_ind = 0; rad_ind < radii.Length; rad_ind++)
                        {
                            int radiusInd = radii[rad_ind].IndexOf(" ");
                            int radius = Convert.ToInt16(radii[rad_ind].Substring(radiusInd + 1, radii[rad_ind].Length - radiusInd - 1));
                            if (numSectors == 1)
                                sw.WriteLine("Calculated using radius of investigation: " + radii[rad_ind]);
                            else
                                sw.WriteLine("Calculated at: " + radii[rad_ind] + ", averaged over " + numSectors + " sectors");

                            // Export exposures at met and turbine sites
                            if (sectorOut == true || bulkOut == true)
                            {
                                sw.WriteLine("Exposure [m]");
                                if (mets != null) WriteExpoSRDH(sw, bulkOut, sectorOut, "Expo", mets, null, numWD, rad_ind, thisInst);
                                if (turbs != null) WriteExpoSRDH(sw, bulkOut, sectorOut, "Expo", null, turbs, numWD, rad_ind, thisInst);
                            }

                            if (gotSR == true && (Sector_SRDH_out == true || Bulk_SRDH_out == true))
                            {
                                sw.WriteLine("Surface Roughness [m]");
                                if (mets != null) WriteExpoSRDH(sw, Bulk_SRDH_out, Sector_SRDH_out, "SR", mets, null, numWD, rad_ind, thisInst);
                                if (turbs != null) WriteExpoSRDH(sw, Bulk_SRDH_out, Sector_SRDH_out, "SR", null, turbs, numWD, rad_ind, thisInst);

                                sw.WriteLine("Displacement height [m]");
                                if (mets != null) WriteExpoSRDH(sw, Bulk_SRDH_out, Sector_SRDH_out, "DH", mets, null, numWD, rad_ind, thisInst);
                                if (turbs != null) WriteExpoSRDH(sw, Bulk_SRDH_out, Sector_SRDH_out, "DH", null, turbs, numWD, rad_ind, thisInst);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        MessageBox.Show("Error writing to file.", "Continuum 3");
                        sw.Close();
                    }

                    sw.Close();
                }

            }
            catch
            {
                MessageBox.Show("Error opening the file. Make sure that it's not open in another program.", "Continuum 3");
                return;
            }

        }

        /// <summary> Writes exposure, surface roughness, and/or displacement height to specified StreamWriter. </summary>        
        public void WriteExpoSRDH(StreamWriter sw, bool bulkOut, bool sectorOut, string Expo_or_SR_or_DH, string[] mets, string[] turbs, int numWD, int rad_ind, Continuum thisInst)
        {
            double dirBin = (double)360 / numWD;
            string header = "";

            if (Expo_or_SR_or_DH == "Expo")
                header = "Site, Elev. [m],";
            else
                header = "Site, ";

            if (sectorOut == true)
                for (int i = 0; i <= numWD - 1; i++)
                    header = header + (i * dirBin).ToString() + ",";

            if (bulkOut == true)
            {
                if (Expo_or_SR_or_DH == "Expo")
                    header = header + "UW Expo[m], DW Expo[m]";
                else if (Expo_or_SR_or_DH == "SR")
                    header = header + "UW SR[m], DW SR[m]";
                else if (Expo_or_SR_or_DH == "DH")
                    header = header + "UW DH[m], DW DH[m]";
            }

            sw.WriteLine(header);
            int numSites = 0;
            string Site_name = "";
            Met thisMet = null;
            Turbine thisTurbine = null;

            if (mets != null)
                numSites = mets.Length;
            else
                numSites = turbs.Length;

            for (int i = 0; i < numSites; i++)
            {
                if (mets != null)
                {
                    Site_name = mets[i];
                    thisMet = thisInst.metList.GetMet(mets[i]);
                }
                else
                {
                    Site_name = turbs[i];
                    thisTurbine = thisInst.turbineList.GetTurbine(turbs[i]);
                }

                sw.Write(Site_name + ", ");

                if (Expo_or_SR_or_DH == "Expo")
                {
                    if (thisMet != null) sw.Write(Math.Round(thisMet.elev, 2) + ", ");
                    if (thisTurbine != null) sw.Write(Math.Round(thisTurbine.elev, 2) + ", ");
                }

                if (sectorOut == true)
                {
                    for (int j = 0; j < numWD; j++)
                    {
                        if (Expo_or_SR_or_DH == "Expo")
                        {
                            if (thisMet != null) sw.Write(Math.Round(thisMet.expo[rad_ind].expo[j], 5).ToString() + ", ");
                            if (thisTurbine != null) sw.Write(Math.Round(thisTurbine.expo[rad_ind].expo[j], 5).ToString() + ", ");
                        }
                        else if (Expo_or_SR_or_DH == "SR")
                        {
                            if (thisMet != null) sw.Write(Math.Round(thisMet.expo[rad_ind].SR[j], 5).ToString() + ", ");
                            if (thisTurbine != null) sw.Write(Math.Round(thisTurbine.expo[rad_ind].SR[j], 5).ToString() + ", ");
                        }
                        else if (Expo_or_SR_or_DH == "DH")
                        {
                            if (thisMet != null) sw.Write(Math.Round(thisMet.expo[rad_ind].dispH[j], 5).ToString() + ", ");
                            if (thisTurbine != null) sw.Write(Math.Round(thisTurbine.expo[rad_ind].dispH[j], 5).ToString() + ", ");
                        }
                    }
                }

                if (bulkOut == true)
                {
                    if (Expo_or_SR_or_DH == "Expo")
                    {
                        if (thisMet != null)
                        {
                            Met.WSWD_Dist thisDist = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisDist.windRose, "Expo", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisDist.windRose, "Expo", "DW"), 4).ToString() + ", ");
                        }
                        else
                        {
                            Turbine.Avg_Est avgEst = new Turbine.Avg_Est();

                            if (thisTurbine.AvgWSEst_Count > 0)
                                avgEst = thisTurbine.avgWS_Est[0];
                            else
                                avgEst.freeStream.windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), thisTurbine.UTMX, thisTurbine.UTMY, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(avgEst.freeStream.windRose, "Expo", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(avgEst.freeStream.windRose, "Expo", "DW"), 4).ToString() + ", ");
                        }
                    }
                    else if (Expo_or_SR_or_DH == "SR")
                    {
                        if (thisMet != null)
                        {
                            Met.WSWD_Dist thisDist = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisDist.windRose, "SR", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisDist.windRose, "SR", "DW"), 4).ToString() + ", ");
                        }
                        else
                        {
                            Turbine.Avg_Est avgEst = new Turbine.Avg_Est();

                            if (thisTurbine.AvgWSEst_Count > 0)
                                avgEst = thisTurbine.avgWS_Est[0];
                            else
                                avgEst.freeStream.windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), thisTurbine.UTMX, thisTurbine.UTMY, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(avgEst.freeStream.windRose, "SR", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(avgEst.freeStream.windRose, "SR", "DW"), 4).ToString() + ", ");
                        }
                    }
                    else if (Expo_or_SR_or_DH == "DH")
                    {
                        if (thisMet != null)
                        {
                            Met.WSWD_Dist thisDist = thisMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisDist.windRose, "DH", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisDist.windRose, "DH", "DW"), 4).ToString() + ", ");
                        }
                        else
                        {
                            Turbine.Avg_Est avgEst = new Turbine.Avg_Est();

                            if (thisTurbine.AvgWSEst_Count > 0)
                                avgEst = thisTurbine.avgWS_Est[0];
                            else
                                avgEst.freeStream.windRose = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), thisTurbine.UTMX, thisTurbine.UTMY, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(avgEst.freeStream.windRose, "DH", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(avgEst.freeStream.windRose, "DH", "DW"), 4).ToString() + ", ");
                        }
                    }
                }
                sw.WriteLine();
            }
            sw.WriteLine();
        }

        /// <summary> Exports generated map as a .CSV. </summary>
        public void ExportMapCSV(Continuum thisInst, string map_export, int WD_Ind, int numWD)
        {
            Map thisMap = new Map();
            for (int i = 0; i <= thisInst.mapList.ThisCount - 1; i++)
            {
                if (map_export == thisInst.mapList.mapItem[i].mapName)
                {
                    thisMap = thisInst.mapList.mapItem[i];
                    break;
                }
            }

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                if (thisMap.modelType == 0)
                    sw.WriteLine("Continuum 3: Modeled Upwind Exposures");
                else if (thisMap.modelType == 1)
                    sw.WriteLine("Continuum 3: Modeled Downwind Exposures");
                else if (thisMap.modelType == 2 || thisMap.modelType == 4)
                {
                    if (WD_Ind == numWD)
                        sw.WriteLine("Continuum 3: Modeled " + thisInst.modeledHeight + " m Wind Speeds");
                    else
                        sw.WriteLine("Continuum 3: Modeled " + thisInst.modeledHeight + " m Wind Speeds for Wind direction =" + ((double)360 / numWD * WD_Ind).ToString() + " degrees");
                }
                else if (thisMap.modelType == 3 || thisMap.modelType == 5)
                {
                    if (thisMap.isWaked == false)
                        sw.WriteLine("Continuum 3: Modeled Gross AEP using Power Curve: " + thisMap.powerCurve);
                    else
                        sw.WriteLine("Continuum 3: Modeled Waked WS using Power Curve: " + thisMap.powerCurve);
                }
                
                sw.WriteLine(DateTime.Now);
                sw.Write(map_export);
                sw.WriteLine();

                sw.Write("UTMX, UTMY,");

                if (thisMap.modelType == 0)
                    sw.Write("UW_Expo,");
                else if (thisMap.modelType == 1)
                    sw.Write("DW_Expo,");
                else if (thisMap.modelType == 2 || thisMap.modelType == 4)
                    sw.Write(thisInst.modeledHeight + " m WS [m/s],");
                else if (thisMap.modelType == 3 || thisMap.modelType == 5)
                {
                    if (thisMap.isWaked == false)
                        sw.Write("Gross AEP [MWh],");
                    else
                        sw.Write(thisInst.modeledHeight + " m WS [m/s],");
                }

                sw.WriteLine();

                for (int i = 0; i < thisMap.numX; i++)
                {
                    for (int j = 0; j < thisMap.numY; j++)
                    {
                        int UTMX = (thisMap.minUTMX + i * thisMap.reso);
                        int UTMY = (thisMap.minUTMY + j * thisMap.reso);
                        sw.Write(UTMX + ",");
                        sw.Write(UTMY + ",");
                        if (numWD == WD_Ind)
                            sw.Write(Math.Round(thisMap.parameterToMap[i, j], 4).ToString() + ", ");
                        else
                            sw.Write(Math.Round(thisMap.sectorParamToMap[i, j, WD_Ind], 4).ToString() + ", ");

                        sw.WriteLine();
                    }
                }
                sw.Close();
            }
        }

        /// <summary> Exports all topography data stored in DB. Used in TopoInfo_tests. </summary>
        public void ExportTopo(Continuum thisInst, string folderName)
        {                        
            string fileName = folderName + "\\Topo_export.csv";
            StreamWriter sw = new StreamWriter(fileName);

            sw.WriteLine("Exported topography data");
            sw.WriteLine("UTMX, UTMY, Elev");

            BinaryFormatter bin = new BinaryFormatter();
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);
            TopoInfo topo = thisInst.topo;

            try
            {
                using (var ctx = new Continuum_EDMContainer(connString))
                {
                    Topo_table topoDB = new Topo_table();
                    var topoDB_Query = from N in ctx.Topo_table where N.Id >= 0 select N;

                    TopoInfo.Min_Max_Num topoX_All = topo.topoNumXY.X.all;
                    TopoInfo.Min_Max_Num topoY_All = topo.topoNumXY.Y.all;

                    foreach (var N in topoDB_Query)
                    {
                        MemoryStream MS1 = new MemoryStream(N.Elevs);
                        float[] theseElevs = (float[])bin.Deserialize(MS1);

                        int X_IndAll = N.Id - 1;
                        int numY = theseElevs.Length;

                        for (int j = 0; j < numY; j++)
                        {
                            int Y_IndAll = j;

                            double UTMX = topo.topoNumXY.X.all.min + X_IndAll * topoX_All.reso;
                            double UTMY = topo.topoNumXY.Y.all.min + Y_IndAll * topoY_All.reso;
                            sw.WriteLine(Math.Round(UTMX, 2) + "," + Math.Round(UTMY, 2) + "," + theseElevs[j]);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                sw.Close();
                return;
            }

            sw.Close();

        }

        /// <summary> Exports all land cover data stored in DB. Used in TopoInfo_tests. </summary>
        public void ExportLC(Continuum thisInst, string folderName)
        {
            string fileName = folderName + "\\LC_export.csv";
            StreamWriter sw = new StreamWriter(fileName);

            BinaryFormatter bin = new BinaryFormatter();
            NodeCollection nodeList = new NodeCollection();
            string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);

            sw.WriteLine("Exported land cover data");
            sw.WriteLine("UTMX, UTMY, Land Cover Code");

            TopoInfo topo = thisInst.topo;
            int[,] allLandCover = new int[topo.LC_NumXY.X.all.num, topo.LC_NumXY.Y.all.num];

            // Grab land cover data from DB

            using (var ctx = new Continuum_EDMContainer(connString))
            {
                var landCoverDB_Query = from N in ctx.LandCover_table where N.Id >= 0 select N;

                foreach (var N in landCoverDB_Query)
                {
                    MemoryStream MS1 = new MemoryStream(N.LandCover);
                    int[] these_LCs = (int[])bin.Deserialize(MS1);

                    int X_IndAll = N.Id - 1;
                    int numY = these_LCs.Length;

                    for (int j = 0; j < numY; j++)
                    {
                        double UTMX = topo.LC_NumXY.X.all.min + X_IndAll * topo.LC_NumXY.X.all.reso;
                        double UTMY = topo.LC_NumXY.Y.all.min + j * topo.LC_NumXY.Y.all.reso;
                        sw.WriteLine(Math.Round(UTMX, 2) + "," + Math.Round(UTMY, 2) + "," + these_LCs[j]);
                    }

                }
            }

            sw.Close();

        }

        
        /// <summary> Exports filtered/unfiltered met data, estimated shear, and extrapolated wind speed time series data. </summary>
        public void Export_Estimated_data(Continuum thisInst, Met_Data_Filter thisMetData, double thisHeight, bool For_MCP, string metName)
        {            
            if (thisInst.sfdEstimateWS.ShowDialog() == DialogResult.OK)
            {
                string filename = thisInst.sfdEstimateWS.FileName;

                StreamWriter file = new StreamWriter(filename);
                if (For_MCP == false)
                {
                    file.WriteLine("Input file: " + thisMetData.rawFilename);
                    if (thisMetData.filteringDone == true)
                        file.WriteLine("Met data filtered using WRA v" + thisMetData.WRA_Methodology);
                    else
                        file.WriteLine("Met data Unfiltered");

                    file.WriteLine("Using Avg of Valid Redundant Sensors");
                    file.WriteLine("Met: " + metName);
                    file.WriteLine("Extrapolated to " + thisHeight + " m");
                    file.WriteLine(DateTime.Today);

                    file.WriteLine();
                    file.WriteLine("Date, " + thisHeight + " m WS [m/s], WS SD [m/s], WD [deg], Shear alpha");

                }
                else
                    file.WriteLine("Date, WS, WD");


                DateTime This_Export_Start = thisInst.Export_Start.Value;
                DateTime This_Export_End = thisInst.Export_End.Value;

                Met_Data_Filter.Sim_TS Sim_to_export = new Met_Data_Filter.Sim_TS();

                for (int i = 0; i < thisMetData.GetNumSimData(); i++)
                    if (thisMetData.simData[i].height == thisHeight)
                        Sim_to_export = thisMetData.simData[i];

                int thisInd = 0;
                while (Sim_to_export.WS_WD_data[thisInd].timeStamp < This_Export_Start)
                    thisInd++;

                while (thisInd < Sim_to_export.WS_WD_data.Length)
                {
                    if (Sim_to_export.WS_WD_data[thisInd].timeStamp > This_Export_End)
                        break;

                    file.Write(Sim_to_export.WS_WD_data[thisInd].timeStamp);
                    file.Write(",");
                    file.Write(Math.Round(Sim_to_export.WS_WD_data[thisInd].WS, 3));
                    file.Write(",");
                    if (For_MCP == false)
                    {
                        file.Write(Math.Round(Sim_to_export.WS_WD_data[thisInd].SD, 4));
                        file.Write(",");
                    }

                    file.Write(Math.Round(Sim_to_export.WS_WD_data[thisInd].WD, 2));
                    if (For_MCP == false)
                    {
                        file.Write(",");
                        file.Write(Math.Round(Sim_to_export.WS_WD_data[thisInd].alpha, 4));
                    }

                    file.WriteLine();
                    thisInd++;
                }

                file.Close();
            }
        }

        
        /// <summary> Exports QC'd flagged met data time series. </summary>        
        public void ExportFlaggedData(Continuum thisInst, Met_Data_Filter thisMetData, string metName)
        {            
            if (thisInst.sfdEstimateWS.ShowDialog() == DialogResult.OK)
            {
                string filename = thisInst.sfdEstimateWS.FileName;
                StreamWriter file = new StreamWriter(filename);
                
                try
                {
                    file.WriteLine("Flagged WS & WD data");
                    file.WriteLine(DateTime.Today);
                    if (thisMetData.filteringDone == true)
                        file.WriteLine("Filtered Met data: " + metName);
                    else
                        file.WriteLine("Met data Unfiltered");

                    file.WriteLine();

                    string[] headerStr = new string[1 + thisMetData.GetNumAnems() * 2 + thisMetData.GetNumAnems() * 2 + thisMetData.GetNumVanes() * 2];
                    headerStr[0] = "Time Stamp";
                    int thisInd = 1;

                    for (int i = 0; i < thisMetData.GetNumAnems(); i++)
                    {
                        headerStr[thisInd] = "Anem_" + thisMetData.anems[i].height + "_" + thisMetData.anems[i].ID + "_WS";
                        headerStr[thisInd + 1] = "Anem_" + thisMetData.anems[i].height + "_" + thisMetData.anems[i].ID + "_WS_Flag";
                        thisInd = thisInd + 2;
                        headerStr[thisInd] = "Anem_" + thisMetData.anems[i].height + "_" + thisMetData.anems[i].ID + "_SD";
                        headerStr[thisInd + 1] = "Anem_" + thisMetData.anems[i].height + "_" + thisMetData.anems[i].ID + "_SD_Flag";
                        thisInd = thisInd + 2;
                    }

                    for (int i = 0; i < thisMetData.GetNumVanes(); i++)
                    {
                        headerStr[thisInd] = "Vane_" + thisMetData.vanes[i].height + "_WD";
                        headerStr[thisInd + 1] = "Vane_" + thisMetData.vanes[i].height + "_Flag";
                        thisInd = thisInd + 2;
                    }

                    for (int i = 0; i < thisInd; i++)
                    {
                        file.Write(headerStr[i]);
                        file.Write(",");
                    }

                    file.WriteLine();

                    DateTime This_Export_Start = thisInst.Export_Start.Value;
                    DateTime This_Export_End = thisInst.Export_End.Value;

                    thisInd = 0;
                    while (thisMetData.anems[0].windData[thisInd].timeStamp < This_Export_Start)
                        thisInd++;

                    while (thisInd < thisMetData.anems[0].windData.Length)
                    {
                        if (thisMetData.anems[0].windData[thisInd].timeStamp > This_Export_End)
                            break;

                        file.Write(thisMetData.anems[0].windData[thisInd].timeStamp);
                        file.Write(",");
                        for (int j = 0; j < thisMetData.GetNumAnems(); j++)
                        {
                            file.Write(Math.Round(thisMetData.anems[j].windData[thisInd].avg, 3));
                            file.Write(",");
                            string filterFlag = thisMetData.GetFlagString(thisMetData.anems[j].windData[thisInd].filterFlag);
                            file.Write(filterFlag);
                            file.Write(",");
                            file.Write(Math.Round(thisMetData.anems[j].windData[thisInd].SD, 3));
                            file.Write(",");
                            if (thisMetData.anems[j].windData[thisInd].SD < (thisMetData.anems[j].windData[thisInd].avg / 3))
                                file.Write("Valid");
                            else
                                file.Write("SD out range");

                            file.Write(",");
                        }

                        for (int j = 0; j < thisMetData.GetNumVanes(); j++)
                        {
                            file.Write(Math.Round(thisMetData.vanes[j].dirData[thisInd].avg, 3));
                            file.Write(",");
                            file.Write(thisMetData.vanes[j].dirData[thisInd].filterFlag);
                            file.Write(",");
                        }

                        file.WriteLine();
                        thisInd++;

                    }

                    file.Close();
                }
                catch
                {
                    MessageBox.Show("Error writing to file.");
                    file.Close();
                    return;
                }

            }
        }
                
        /// <summary> Exports estimated shear alpha time series data. </summary>        
        public void ExportShearData(Continuum thisInst, Met_Data_Filter thisMetData, string metName)
        {            
            if (thisInst.sfdEstimateWS.ShowDialog() == DialogResult.OK)
            {
                string filename = thisInst.sfdEstimateWS.FileName;

                try
                {
                    StreamWriter file = new StreamWriter(filename);
                    file.WriteLine("Shear & WD data at: " + metName);
                    file.WriteLine("Using Average of valid Redundant sensors");
                    file.WriteLine(DateTime.Today);
                    if (thisMetData.filteringDone == true)
                        file.WriteLine("Fitered Met data");
                    else
                        file.WriteLine("Unfiltered Met data");

                    file.WriteLine();

                    file.Write("Time Stamp, Alpha, WD");
                    file.WriteLine();

                    DateTime This_Export_Start = thisInst.Export_Start.Value;
                    DateTime This_Export_End = thisInst.Export_End.Value;

                    int thisInd = 0;
                    while (thisMetData.alpha[thisInd].timeStamp < This_Export_Start)
                        thisInd++;

                    while (thisInd < thisMetData.alpha.Length)
                    {
                        if (thisMetData.alpha[thisInd].timeStamp > This_Export_End)
                            break;

                        file.Write(thisMetData.alpha[thisInd].timeStamp);
                        file.Write(",");
                        file.Write(Math.Round(thisMetData.alpha[thisInd].alpha, 3));
                        file.Write(",");
                        file.Write(Math.Round(thisMetData.alpha[thisInd].WD, 2));
                        file.Write(",");

                        file.WriteLine();
                        thisInd++;

                    }

                    file.Close();
                }
                catch
                {
                    MessageBox.Show("Error writing to file.");
                    return;
                }

            }
        }

        /// <summary> Exports long-term estimated MCP'd time series data. </summary>  
        public void ExportMCP_TimeSeries(Continuum thisInst)
        {            
            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisInst.GetSelectedMCP();
            string MCP_method = thisInst.Get_MCP_Method();
            DateTime exportStart = thisInst.dateMCPExportStart.Value;
            DateTime exportEnd = thisInst.dateMCPExportEnd.Value;            
            DateTime refEnd = thisMCP.GetStartOrEndDate("Reference", "End");

            if (thisMCP.LT_WS_Ests.Length == 0)
                thisMCP.LT_WS_Ests = thisMCP.GenerateLT_WS_TS(thisInst, thisMet, MCP_method);

            // Check that the export start/end are within interval of estimated data
            if (exportStart > refEnd)
            {
                MessageBox.Show("The selected export start date is after the end of the reference data period.");
                return;
            }

            try
            {
                if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
                {
                    string filename = thisInst.sfd60mWS.FileName;
                    StreamWriter file = new StreamWriter(filename);
                    file.WriteLine("MCP WS & WD Estimates");
                    file.WriteLine(DateTime.Today);
                    file.WriteLine(MCP_method);
                    file.WriteLine("Data binned into " + thisMCP.numWD + " WD bins; " + thisMCP.numTODs + " Time of Day bins; " + thisMCP.numSeasons + " Season bins");
                    file.WriteLine();

                    file.WriteLine("Date, WS Est [m/s], WD Est [deg]");
                    //  MCP.Site_data[] LT_WS_Est = thisMCP.GenerateLT_WS_TS(thisInst, thisMet, selectedMethod);

                    foreach (MCP.Site_data LT_WS_WD in thisMCP.LT_WS_Ests)
                    {
                        if (LT_WS_WD.thisDate >= exportStart && LT_WS_WD.thisDate <= exportEnd)
                        {
                            file.Write(LT_WS_WD.thisDate);
                            file.Write(",");
                            file.Write(Math.Round(LT_WS_WD.thisWS, 4));
                            file.Write(",");
                            file.Write(Math.Round(LT_WS_WD.thisWD, 3));
                            file.WriteLine();
                        }
                    }

                    file.Close();

                }
            }
            catch
            {
                MessageBox.Show("Error saving to file. Check that it is not open in another program");
            }
        }

        /// <summary> Exports MCP Method of Bins wind speed ratios. </summary>  
        public void ExportMCP_BinRatios(Continuum thisInst)
        {            
            MCP thisMCP = thisInst.GetSelectedMCP();           

            string filename = "";
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
                filename = thisInst.sfd60mWS.FileName;
            else
                return;

            try
            {
                StreamWriter file = new StreamWriter(filename);
                file.WriteLine("Avg, SD & count of WS Ratios (Target/Reference) from Method of Bins");
                file.WriteLine(DateTime.Today.ToShortDateString());
                file.WriteLine();

                file.WriteLine("Average WS Ratios by WS & WD");
                file.WriteLine();
                file.Write("WS [m/s],");
                for (int i = 0; i <= thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(0); i++)
                {
                    file.Write(i * thisMCP.Get_WS_width_for_MCP());
                    file.Write(",");
                }
                file.WriteLine();

                for (int j = 0; j <= thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(1); j++)
                {
                    if (j != thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(1))
                    {
                        file.Write(j * 360 / thisMCP.numWD);
                        file.Write(",");
                    }
                    else
                        file.Write("All WD,");

                    for (int i = 0; i <= thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(0); i++)
                        if (thisMCP.MCP_Bins.binAvgSD_Cnt[i, j].avgWS_Ratio > 0)
                        {
                            file.Write(Math.Round(thisMCP.MCP_Bins.binAvgSD_Cnt[i, j].avgWS_Ratio, 3));
                            file.Write(",");
                        }
                        else
                            file.Write(" ,");
                    file.WriteLine();
                }

                file.WriteLine();
                file.WriteLine("Standard Deviation of WS Ratios by WS & WD");
                file.WriteLine();
                file.Write("WS [m/s],");
                for (int i = 0; i <= thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(0); i++)
                {
                    file.Write(i * thisMCP.Get_WS_width_for_MCP());
                    file.Write(",");
                }
                file.WriteLine();

                for (int j = 0; j <= thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(1); j++)
                {
                    if (j != thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(1))
                    {
                        file.Write(j * 360 / thisMCP.numWD);
                        file.Write(",");
                    }
                    else
                        file.Write("All WD,");

                    for (int i = 0; i <= thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(0); i++)
                        if (thisMCP.MCP_Bins.binAvgSD_Cnt[i, j].avgWS_Ratio > 0)
                        {
                            file.Write(Math.Round(thisMCP.MCP_Bins.binAvgSD_Cnt[i, j].SD_WS_Ratio, 3));
                            file.Write(",");
                        }
                        else
                            file.Write(" ,");
                    file.WriteLine();
                }

                file.WriteLine();
                file.WriteLine("count of WS Ratios by WS & WD");
                file.WriteLine();
                file.Write("WS [m/s],");
                for (int i = 0; i <= thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(0); i++)
                {
                    file.Write(i * thisMCP.Get_WS_width_for_MCP());
                    file.Write(",");
                }
                file.WriteLine();

                for (int j = 0; j <= thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(1); j++)
                {
                    if (j != thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(1))
                    {
                        file.Write(j * 360 / thisMCP.numWD);
                        file.Write(",");
                    }
                    else
                        file.Write("All WD,");

                    for (int i = 0; i <= thisMCP.MCP_Bins.binAvgSD_Cnt.GetUpperBound(0); i++)
                        if (thisMCP.MCP_Bins.binAvgSD_Cnt[i, j].avgWS_Ratio > 0)
                        {
                            file.Write(thisMCP.MCP_Bins.binAvgSD_Cnt[i, j].count);
                            file.Write(",");
                        }
                        else
                            file.Write(" ,");
                    file.WriteLine();
                }

                file.Close();
            }
            catch
            {
                MessageBox.Show("Error saving to file. Make sure that is not open in another program.");
            }
        }

        /// <summary> Exports long-term MCP'd WS/WD distribution to a TAB file. </summary>  
        public void ExportMCP_TAB(Continuum thisInst)
        {
            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisInst.GetSelectedMCP();
            string MCP_method = thisInst.Get_MCP_Method();
            DateTime exportStart = thisInst.dateMCPExportStart.Value;
            DateTime exportEnd = thisInst.dateMCPExportEnd.Value;
            int numTAB_Bins = Convert.ToInt32(thisInst.cboTAB_bins.SelectedItem.ToString());
            double wsBinWidth = 1;
            double.TryParse(thisInst.txtTAB_WS_bin.Text, out wsBinWidth);

            if (thisMCP.LT_WS_Ests.Length == 0)
                thisMCP.LT_WS_Ests = thisMCP.GenerateLT_WS_TS(thisInst, thisMet, MCP_method);

            string filename = "";
            if (thisInst.sfdSaveTAB.ShowDialog() == DialogResult.OK)
                filename = thisInst.sfdSaveTAB.FileName;

            if (filename != "")
            {
                try
                {
                    // open file to output TAB file
                    StreamWriter file = new StreamWriter(filename);
                    string metName = thisMet.name;
                    file.WriteLine(metName);

                    // read in name, UTMX/Y and height
                    UTM_conversion.Lat_Long theseLL = thisInst.UTM_conversions.UTMtoLL(thisMet.UTMX, thisMet.UTMY);
                    string latitude = Math.Round(theseLL.latitude, 4).ToString();
                    string longitude = Math.Round(theseLL.longitude, 4).ToString();
                    double height = thisInst.modeledHeight;

                    string UTMs_Height = latitude + " " + longitude + " " + height.ToString();

                    // write TAB header details
                    file.WriteLine(UTMs_Height);
                    file.Write(numTAB_Bins);
                    file.Write(" ");
                    file.Write(wsBinWidth);
                    file.WriteLine(" 0");

                    double[] windRose = new double[numTAB_Bins];
                    double[,] WSWD_Dist = new double[thisInst.metList.numWS, numTAB_Bins];

                    DateTime thisTS = DateTime.Today;
                    double thisWS = 0;
                    double thisWD = 0;

                    int Est_data_ind = 0;

                    //   MCP.Site_data[] LT_WS_Est = thisMCP.GenerateLT_WS_TS(thisInst, thisMet, MCP_type);

                    // searches through MCP LT WS Est timeseries to find Est_data_ind corresponding
                    // to first data point to use in TAB file

                    for (int i = 0; i < thisMCP.LT_WS_Ests.Length; i++)
                    {
                        if (thisMCP.LT_WS_Ests[i].thisDate < exportStart)
                            Est_data_ind++;
                        else
                            break;
                    }

                    thisTS = thisMCP.LT_WS_Ests[Est_data_ind].thisDate;
                    thisWS = thisMCP.LT_WS_Ests[Est_data_ind].thisWS;
                    thisWD = thisMCP.LT_WS_Ests[Est_data_ind].thisWD;
                    Est_data_ind++;

                    // starting at This_Start, goes through LT WS Est data, until it reaches This_End,
                    // and finds WD and WS/WD distributions
                    while (thisTS <= exportEnd)
                    {
                        if (thisWS >= 0 && thisWD >= 0)
                        {
                            int WS_Ind = thisMCP.Get_WS_ind(thisWS, wsBinWidth);
                            int WD_Ind = thisMCP.Get_WD_ind(thisWD, numTAB_Bins);

                            if (WS_Ind > 30) WS_Ind = 30;

                            windRose[WD_Ind]++;
                            WSWD_Dist[WS_Ind, WD_Ind]++;

                        }

                        if (thisTS == exportEnd)
                            break;

                        thisTS = thisMCP.LT_WS_Ests[Est_data_ind].thisDate;
                        thisWS = thisMCP.LT_WS_Ests[Est_data_ind].thisWS;
                        thisWD = thisMCP.LT_WS_Ests[Est_data_ind].thisWD;
                        Est_data_ind++;

                    }

                    double Sum_WD = 0;
                    for (int i = 0; i < numTAB_Bins; i++)
                        Sum_WD = Sum_WD + windRose[i];

                    for (int i = 0; i < numTAB_Bins; i++)
                    {
                        windRose[i] = windRose[i] / Sum_WD * 100;
                        file.Write(Math.Round(windRose[i], 4) + "\t");
                    }
                    file.WriteLine();

                    for (int WD_Ind = 0; WD_Ind < numTAB_Bins; WD_Ind++)
                    {
                        double Sum_WS = 0;
                        for (int WS_Ind = 0; WS_Ind < thisInst.metList.numWS; WS_Ind++)
                            Sum_WS = Sum_WS + WSWD_Dist[WS_Ind, WD_Ind];

                        for (int WS_Ind = 0; WS_Ind < thisInst.metList.numWS; WS_Ind++)
                            WSWD_Dist[WS_Ind, WD_Ind] = WSWD_Dist[WS_Ind, WD_Ind] / Sum_WS * 1000;

                    }

                    for (int i = 0; i < thisInst.metList.numWS; i++)
                    {
                        file.Write((i + wsBinWidth / 2) + "\t");
                        for (int j = 0; j < numTAB_Bins; j++)
                            file.Write(Math.Round(WSWD_Dist[i, j], 3) + "\t");
                        file.WriteLine();
                    }

                    file.Close();
                }
                catch
                {
                    MessageBox.Show("Error writing to file. Make sure that it is not open in another program.");
                }
            }
        }

        /// <summary> Exports results of MCP uncertainty analysis. </summary>  
        public void ExportMCP_Uncertainty(Continuum thisInst)
        {             
            string filename = "";
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
                filename = thisInst.sfd60mWS.FileName;

            Met thisMet = thisInst.GetSelectedMet("MCP");
            MCP thisMCP = thisInst.GetSelectedMCP();
            string currentMethod = thisInst.Get_MCP_Method();

            if (filename != "")
            {
                StreamWriter file = new StreamWriter(filename);
                file.WriteLine("MCP Uncertainty at Target Site " + currentMethod + ",");
                file.WriteLine("Reference: MERRA2, Target: " + thisMet.name);
                file.WriteLine("Data binned into " + thisMCP.numWD + " WD bins; " + thisMCP.numTODs + " Time of Day bins; " + thisMCP.numSeasons + " Season bins");
                file.WriteLine("Start Time, End Time, Window Size, LT WS Est, LT Avg, Std Dev");

                if (currentMethod == "Orth. Regression" && thisMCP.uncertOrtho.Length > 0)
                {
                    for (int u = 0; u < thisMCP.uncertOrtho.Length; u++)
                    {
                        // Assign LT Avg series = Avg of Uncert obj
                        if (thisMCP.uncertOrtho[u].avg != 0 && thisMCP.uncertOrtho[u].stDev != 0)
                        {
                            for (int i = 0; i < thisMCP.uncertOrtho[u].LT_Ests.Length; i++)
                            {
                                file.Write(thisMCP.uncertOrtho[u].start[i]);
                                file.Write(",");
                                file.Write(thisMCP.uncertOrtho[u].end[i]);
                                file.Write(",");
                                file.Write(thisMCP.uncertOrtho[u].WSize);
                                file.Write(",");
                                file.Write(thisMCP.uncertOrtho[u].LT_Ests[i]);
                                file.Write(",");
                                file.Write(Math.Round(thisMCP.uncertOrtho[u].avg, 3));
                                file.Write(",");
                                file.Write(Math.Round(thisMCP.uncertOrtho[u].stDev, 4));
                                file.WriteLine();
                            }
                        }
                    }
                }
                else if (currentMethod == "Method of Bins" && thisMCP.uncertBins.Length > 0)
                {
                    for (int u = 0; u < thisMCP.uncertBins.Length; u++)
                    {
                        // Assign LT Avg series = Avg of Uncert obj
                        if (thisMCP.uncertBins[u].avg != 0 && thisMCP.uncertBins[u].stDev != 0)
                        {
                            for (int i = 0; i < thisMCP.uncertBins[u].LT_Ests.Length; i++)
                            {
                                file.Write(thisMCP.uncertBins[u].start[i]);
                                file.Write(",");
                                file.Write(thisMCP.uncertBins[u].end[i]);
                                file.Write(",");
                                file.Write(thisMCP.uncertBins[u].WSize);
                                file.Write(",");
                                file.Write(thisMCP.uncertBins[u].LT_Ests[i]);
                                file.Write(",");
                                file.Write(Math.Round(thisMCP.uncertBins[u].avg, 3));
                                file.Write(",");
                                file.Write(Math.Round(thisMCP.uncertBins[u].stDev, 4));
                                file.WriteLine();
                            }
                        }
                    }
                }
                else if (currentMethod == "Variance Ratio" && thisMCP.uncertVarrat.Length > 0)
                {
                    for (int u = 0; u < thisMCP.uncertVarrat.Length; u++)
                    {
                        // Assign LT Avg series = Avg of Uncert obj
                        if (thisMCP.uncertVarrat[u].avg != 0 && thisMCP.uncertVarrat[u].stDev != 0)
                        {
                            for (int i = 0; i < thisMCP.uncertVarrat[u].LT_Ests.Length; i++)
                            {
                                file.Write(thisMCP.uncertVarrat[u].start[i]);
                                file.Write(",");
                                file.Write(thisMCP.uncertVarrat[u].end[i]);
                                file.Write(",");
                                file.Write(thisMCP.uncertVarrat[u].WSize);
                                file.Write(",");
                                file.Write(thisMCP.uncertVarrat[u].LT_Ests[i]);
                                file.Write(",");
                                file.Write(Math.Round(thisMCP.uncertVarrat[u].avg, 3));
                                file.Write(",");
                                file.Write(Math.Round(thisMCP.uncertVarrat[u].stDev, 4));
                                file.WriteLine();
                            }
                        }
                    }
                }
                else if (currentMethod == "Matrix" && thisMCP.uncertMatrix.Length > 0)
                {
                    for (int u = 0; u < thisMCP.uncertMatrix.Length; u++)
                    {
                        // Assign LT Avg series = Avg of Uncert obj
                        if (thisMCP.uncertMatrix[u].avg != 0 && thisMCP.uncertMatrix[u].stDev != 0)
                        {
                            for (int i = 0; i < thisMCP.uncertMatrix[u].LT_Ests.Length; i++)
                            {
                                file.Write(thisMCP.uncertMatrix[u].start[i]);
                                file.Write(",");
                                file.Write(thisMCP.uncertMatrix[u].end[i]);
                                file.Write(",");
                                file.Write(thisMCP.uncertMatrix[u].WSize);
                                file.Write(",");
                                file.Write(thisMCP.uncertMatrix[u].LT_Ests[i]);
                                file.Write(",");
                                file.Write(Math.Round(thisMCP.uncertMatrix[u].avg, 3));
                                file.Write(",");
                                file.Write(Math.Round(thisMCP.uncertMatrix[u].stDev, 4));
                                file.WriteLine();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Uncertainty Data for Selected MCP Method Exists, Please Try Again");
                }
                file.Close();
            }
        }

        /// <summary> Exports Reference wind rose for selected time interval. </summary> 
        public void ExportReferenceWindRose(Continuum thisInst)
        {
            string Output_folder = "";

            if (thisInst.fbd_Export.ShowDialog() == DialogResult.OK)
                Output_folder = thisInst.fbd_Export.SelectedPath;
            else
                return;

            if (thisInst.cboRefWindOrEnergy.SelectedItem == null)
                thisInst.cboRefWindOrEnergy.SelectedIndex = 0;

            string windOrEnergyRose = thisInst.cboRefWindOrEnergy.SelectedItem.ToString();

            string Month_str;
            if (thisInst.cboReferenceMonth.SelectedItem.ToString() == "All Months")
                Month_str = "All";
            else
                Month_str = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(thisInst.cboReferenceMonth.SelectedIndex);

            string Year_str = thisInst.cboReferenceYear.SelectedItem.ToString();

            string filename = "";
            Reference thisRef = thisInst.GetSelectedReference("LT Ref");
           
            if (thisRef.interpData.TS_Data == null)
                return;

            if (Month_str != "All")
                filename = Output_folder + "\\" + thisRef.refDataDownload.refType + "_" + "Lat_" + Math.Round(thisRef.interpData.Coords.latitude, 3) 
                    + "_" + "Lon_" + Math.Round(thisRef.interpData.Coords.longitude, 3) + "_" + Month_str + "_" + Year_str + "_" + thisInst.metList.numWD + "Sector " + windOrEnergyRose + ".csv";
            else
                filename = Output_folder + "\\" + thisRef.refDataDownload.refType + "_" + "Lat_" + Math.Round(thisRef.interpData.Coords.latitude, 3) + "_" + "Lon_" 
                    + Math.Round(thisRef.interpData.Coords.longitude, 3) + "_" + Year_str + "_" + thisInst.metList.numWD + "Sector " + windOrEnergyRose + ".csv";

            // Header
            StreamWriter file = new StreamWriter(filename);
            try
            {
                file.WriteLine("Wind Rose Estimated from " + thisRef.refDataDownload.refType);
                if (thisRef.numNodes == 1)
                    file.WriteLine("Using closest node: ," + thisRef.nodes[0].XY_ind.Lat + ", " + thisRef.nodes[0].XY_ind.Lon);
                else
                    file.WriteLine("Interpolated between closest" + thisRef.numNodes + " nodes");

            }
            catch
            {
                MessageBox.Show("Error writing to output file. Make sure it isn't open.");
                return;
            }

            file.WriteLine("Lat: ," + thisRef.interpData.Coords.latitude + ", Long: ," + thisRef.interpData.Coords.longitude);
            file.WriteLine();


            file.WriteLine("Month: " + Month_str);
            file.WriteLine("Year: " + Year_str);

            file.WriteLine();
            file.WriteLine("WD sector, Frequency");

            int monthInd = 100;
            int yearInd = 100;
            if (thisInst.cboReferenceMonth.SelectedItem.ToString() != "All Months")
                monthInd = thisInst.cboReferenceMonth.SelectedIndex + 1;

            if (thisInst.cboReferenceYear.SelectedItem.ToString() != "LT Avg")
                yearInd = Convert.ToInt16(thisInst.cboReferenceYear.SelectedItem.ToString());

            double[] thisWR = thisRef.CalcWindOrEnergyRose(monthInd, yearInd, thisInst.UTM_conversions, thisInst.metList.numWD, windOrEnergyRose, thisInst.modelList.airDens, thisInst.modelList.rotorDiam);
            double sectWidth = 360.0 / thisInst.metList.numWD;

            for (int i = 0; i < thisInst.metList.numWD; i++)
                file.WriteLine(i * sectWidth + "," + thisWR[i].ToString());

            file.Close();
        }

        /// <summary> Exports monthly LT reference data for all years. </summary> 
        public void ExportReferenceAllMonthsAllYears(Continuum thisInst)
        {
            string Output_folder = "";

            if (thisInst.fbd_Export.ShowDialog() == DialogResult.OK)
                Output_folder = thisInst.fbd_Export.SelectedPath;
            else
                return;
      
            string filename = "";    

            Reference thisRef = thisInst.GetSelectedReference("LT Ref");
            if (thisRef.interpData.TS_Data == null)
                return;

            int firstYear = thisRef.startDate.Year;
            int lastYear = thisRef.endDate.Year;

            //      string selectedMERRA = thisInst.cboMERRASelectedMet.SelectedItem.ToString();

            Met targetMet = new Met();
            if (thisRef.isUserDefined == false)
                targetMet = thisInst.metList.GetMetAtLatLon(thisRef.interpData.Coords.latitude, thisRef.interpData.Coords.longitude, thisInst.UTM_conversions);

            if (thisRef.isUserDefined)            
                filename = Output_folder + "\\Monthly_Prod_Lat_" + thisRef.interpData.Coords.latitude + "_" + "Lon_" + thisRef.interpData.Coords.longitude +
                "_" + firstYear.ToString() + "_to_" + lastYear.ToString() + ".csv";            
            else            
                filename = Output_folder + "\\Monthly_Prod_" + targetMet.name + "_" + firstYear.ToString() + "_to_" + lastYear.ToString() + ".csv";
            
            StreamWriter file = new StreamWriter(filename);

            file.WriteLine("Long-Term Monthly Production based on MERRA data");
            if (thisRef.isUserDefined)            
                file.WriteLine("Project location (user-defined):, Lat:, " + thisRef.interpData.Coords.latitude + ", Long:, " + thisRef.interpData.Coords.longitude);            
            else
                file.WriteLine("Project:," + targetMet.name);

            file.WriteLine();
            file.WriteLine("Year, Month, Prod (MWh), % Diff from Monthly LT");

            for (int i = firstYear; i <= lastYear; i++)
            {
                {
                    Reference.MonthlyProdByYearAndLTAvg[] thisMonthly = new Reference.MonthlyProdByYearAndLTAvg[0];
                    thisMonthly = thisRef.interpData.monthlyProd;

                    for (int j = 1; j <= 12; j++)
                    {
                        if (thisRef.Have_Full_Month(thisRef.interpData.TS_Data, j, i))
                        {
                            file.Write(i + ",");
                            file.Write(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(j) + ",");
                            int[] monthYearInd = thisRef.Get_Month_Year_Inds(thisMonthly, j, i);
                            file.Write(Math.Round(thisMonthly[monthYearInd[0]].YearProd[monthYearInd[1]].prod / 1000, 1).ToString() + ",");
                            double Diff = thisRef.Calc_Perc_Diff_from_LT_Monthly(thisMonthly[monthYearInd[0]], i);
                            file.Write((Math.Round(Diff, 4)).ToString("P")); // adds % diff from the average
                            file.WriteLine();
                        }
                    }
                }
            }

            file.Close();
        }

        /// <summary> Exports Reference time series data (interpolated, if more than one node selected). </summary> 
        public void ExportReferenceInterpData(Continuum thisInst)
        {
            Reference thisRef = thisInst.GetSelectedReference("LT Ref");

            if (thisRef.interpData.TS_Data == null)
            {
                MessageBox.Show("No data to export! You have to extract data first.");
                return;
            }

            string Output_folder = "";

            if (thisInst.fbd_Export.ShowDialog() == DialogResult.OK)
                Output_folder = thisInst.fbd_Export.SelectedPath;
            else
                return;

            string filename = Output_folder + "\\" + thisRef.refDataDownload.refType + "_Lat_" + thisRef.interpData.Coords.latitude + "_" + "Lon_" + thisRef.interpData.Coords.longitude + "_" + thisRef.Make_MERRA2_Date_String(thisRef.startDate)
                + "_to_" + thisRef.Make_MERRA2_Date_String(thisRef.endDate) + ".csv";

            // Header
            StreamWriter file = new StreamWriter(filename);
            file.WriteLine("Interpolated " + thisRef.refDataDownload.refType + " Hourly Data");
            if (thisRef.numNodes == 1)
                file.WriteLine("Using closest " + thisRef.refDataDownload.refType + " node: Lat = " + thisRef.nodes[0].XY_ind.Lat.ToString() + "; Long = " + thisRef.nodes[0].XY_ind.Lon.ToString());
            else
                file.WriteLine("Interpolated between " + thisRef.numNodes + " " + thisRef.refDataDownload.refType + " nodes");

            file.WriteLine();
            file.WriteLine("Lat: ," + thisRef.interpData.Coords.latitude + ", Long: ," + thisRef.interpData.Coords.longitude);
            file.WriteLine("UTC Offset: " + thisRef.interpData.UTC_offset.ToString());

            file.WriteLine();
            file.Write("Time Stamp ,");
            file.Write(thisRef.wswdH.ToString() + "mWS [m/s],");
            file.Write(thisRef.wswdH.ToString() + "mWD [degs],");            
            file.Write("Surf. Press. [kPa],");
            file.Write("Sea Press. [kPa],");
            file.Write("10mTemp[deg C],");
            
            /*
            if (thisRef.MERRA_Params.Get_50mWSWD) file.Write("50mWS [m/s],");
            if (thisRef.MERRA_Params.Get_50mWSWD) file.Write("50mWD [degs],");
            if (thisRef.MERRA_Params.Get_10mWSWD) file.Write("10mWS [m/s],");
            if (thisRef.MERRA_Params.Get_10mWSWD) file.Write("10mWD [degs],");
            if (thisRef.MERRA_Params.Get_SurfPress) file.Write("Surf. Press. [kPa],");
            if (thisRef.MERRA_Params.Get_SurfPress) file.Write("Sea Press. [kPa],");
            if (thisRef.MERRA_Params.Get_10mTemp) file.Write("10mTemp[deg C],");
            if (thisRef.MERRA_Params.Get_FracMean) file.Write("MODIS Cloud Fraction,");
            if (thisRef.MERRA_Params.Get_OpticalThick) file.Write("MODIS Optical Thickness,");
            if (thisRef.MERRA_Params.Get_TotalFrac) file.Write("ISCCP Cloud Fraction,");
            if (thisRef.MERRA_Params.Get_Precip) file.Write("Total Precipitation,");
            if (thisRef.MERRA_Params.Get_Corr_Precip) file.Write("Corrected Precipitation,");
            */

            file.WriteLine();

            for (int i = 0; i < thisRef.interpData.TS_Data.Length; i++)
            {
                file.Write(thisRef.interpData.TS_Data[i].thisDate + ",");
                file.Write(Math.Round(thisRef.interpData.TS_Data[i].WS, 4) + ",");
                file.Write(Math.Round(thisRef.interpData.TS_Data[i].WD, 3) + ",");
                //    if (thisRef.MERRA_Params.Get_10mWSWD) file.Write(Math.Round(thisRef.interpData.TS_Data[i].WS10m, 3) + ",");
                //    if (thisRef.MERRA_Params.Get_10mWSWD) file.Write(Math.Round(thisRef.interpData.TS_Data[i].WD10m, 2) + ",");
                file.Write(Math.Round(thisRef.interpData.TS_Data[i].surfPress / 1000, 3) + ",");
                file.Write(Math.Round(thisRef.interpData.TS_Data[i].seaPress / 1000, 3) + ",");
                file.Write(Math.Round(thisRef.interpData.TS_Data[i].temperature - 273.15, 2) + ",");
                //   if (thisRef.MERRA_Params.Get_FracMean) file.Write(thisRef.interpData.TS_Data[i].Mean_Cloud_Fraction + ",");
                //   if (thisRef.MERRA_Params.Get_OpticalThick) file.Write(thisRef.interpData.TS_Data[i].Optical_Thick + ",");
                //   if (thisRef.MERRA_Params.Get_TotalFrac) file.Write(thisRef.interpData.TS_Data[i].Total_Cloud_Area_Fraction + ",");
                //   if (thisRef.MERRA_Params.Get_Precip) file.Write(thisRef.interpData.TS_Data[i].Precip + ",");
                //   if (thisRef.MERRA_Params.Get_Corr_Precip) file.Write(thisRef.interpData.TS_Data[i].Precip_Corr + ",");

                file.WriteLine();
            }

            file.Close();
        }

        /// <summary> Exports annual average values shown on Time Series tab. </summary>
        public void ExportYearlyTurbineValues(Continuum thisInst)
        {             
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                bool haveGross = false;
                bool haveNet = false;

                try
                {
                    if (thisInst.lstYearlyTurbine.Items[0].SubItems[1].Text != "")
                        haveGross = true;

                    if (thisInst.lstYearlyTurbine.Items[0].SubItems[3].Text != "")
                        haveNet = true;
                }
                catch
                {
                    return;
                }

                string filePath = thisInst.sfd60mWS.FileName;
                StreamWriter file = new StreamWriter(filePath);

                file.WriteLine("Estimated Annual Parameters at Turbine: " + thisInst.GetSelectedTurbine("Monthly").name);
                file.WriteLine(DateTime.Now.ToShortDateString());
                file.WriteLine();

                // Output Annual table
                file.Write(thisInst.lstYearlyTurbine.Columns[0].Text + "," + thisInst.lstYearlyTurbine.Columns[1].Text + ",");
                if (haveGross == true)
                    file.Write(thisInst.lstYearlyTurbine.Columns[2].Text + ",");

                if (haveNet == true)
                    file.Write(thisInst.lstYearlyTurbine.Columns[3].Text + "," + thisInst.lstYearlyTurbine.Columns[4].Text + ",");

                if (haveGross || haveNet)
                    file.Write(thisInst.lstYearlyTurbine.Columns[5].Text + ",");

                file.WriteLine();

                for (int i = 0; i < thisInst.lstYearlyTurbine.Items.Count; i++)
                {
                    for (int j = 0; j < thisInst.lstYearlyTurbine.Items[i].SubItems.Count; j++)
                        if (thisInst.lstYearlyTurbine.Items[i].SubItems[j].Text != "")
                            file.Write(thisInst.lstYearlyTurbine.Items[i].SubItems[j].Text + ",");

                    file.WriteLine();
                }

                file.Close();
            }
        }

        /// <summary> Exports monthly average values shown on Time Series tab. </summary>
        public void ExportMonthlyTurbineValues(Continuum thisInst)
        {            
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                bool haveGross = false;
                bool haveNet = false;

                try
                {
                    if (thisInst.lstMonthlyTurbine.Items[0].SubItems[2].Text != "")
                        haveGross = true;

                    if (thisInst.lstMonthlyTurbine.Items[0].SubItems[4].Text != "")
                        haveNet = true;
                }
                catch
                {
                    return;
                }

                string filePath = thisInst.sfd60mWS.FileName;
                StreamWriter file = new StreamWriter(filePath);

                file.WriteLine("Estimated Monthly Parameters at Turbine: " + thisInst.GetSelectedTurbine("Monthly").name);
                file.WriteLine(DateTime.Now.ToShortDateString());
                file.WriteLine();

                // Output Monthly table
                file.Write(thisInst.lstMonthlyTurbine.Columns[0].Text + "," + thisInst.lstMonthlyTurbine.Columns[1].Text + "," + thisInst.lstMonthlyTurbine.Columns[2].Text + ",");
                if (haveGross == true)
                    file.Write(thisInst.lstMonthlyTurbine.Columns[3].Text + ",");

                if (haveNet == true)
                    file.Write(thisInst.lstMonthlyTurbine.Columns[4].Text + "," + thisInst.lstMonthlyTurbine.Columns[5].Text + ",");

                if (haveGross || haveNet)
                    file.Write(thisInst.lstMonthlyTurbine.Columns[6].Text + ",");

                file.WriteLine();

                for (int i = 0; i < thisInst.lstMonthlyTurbine.Items.Count; i++)
                {
                    for (int j = 0; j < thisInst.lstMonthlyTurbine.Items[i].SubItems.Count; j++)
                        if (thisInst.lstMonthlyTurbine.Items[i].SubItems[j].Text != "")
                            file.Write(thisInst.lstMonthlyTurbine.Items[i].SubItems[j].Text + ",");

                    file.WriteLine();
                }

                file.Close();
            }
        }

        /// <summary> Exports estimated hourly values at selected turbine on Time Series tab. </summary>
        public void ExportHourlyTurbineValues(Continuum thisInst)
        {
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                Turbine thisTurb = thisInst.GetSelectedTurbine("Monthly");

                Wake_Model thisWakeModel = null;
                if (thisInst.wakeModelList.NumWakeModels > 0)
                {
                    string wakeModelString = thisInst.cboMonthlyWakeModel.SelectedItem.ToString();
                    thisWakeModel = thisInst.wakeModelList.GetWakeModelFromString(wakeModelString);
                }
                                
                TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Monthly");
                WakeCollection.WakeLossCoeffs[] wakeCoeffs = null;
                string MCP_Method = thisInst.Get_MCP_Method();

                if (thisWakeModel != null)
                {
                    // Find wake loss coeffs                 
                    int minDistance = 10000000;
                    int maxDistance = 0;

                    for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
                    {
                        int[] Min_Max_Dist = thisInst.turbineList.CalcMinMaxDistanceToTurbines(thisInst.turbineList.turbineEsts[i].UTMX, thisInst.turbineList.turbineEsts[i].UTMY);
                        if (Min_Max_Dist[0] < minDistance) minDistance = Min_Max_Dist[0]; // this is min distance to turbine but when WD is at a different angle (not in line with turbines) the X dist is less than this value so making this always equal to 2*RD
                        if (Min_Max_Dist[1] > maxDistance) maxDistance = Min_Max_Dist[1];
                    }

                    minDistance = (int)(2 * thisWakeModel.powerCurve.RD);
                    if (maxDistance == 0) maxDistance = 15000; // maxDistance will be zero when there is only one turbine. Might be good to make this value constant
                    wakeCoeffs = thisInst.wakeModelList.GetWakeLossesCoeffs(minDistance, maxDistance, thisWakeModel, thisInst.metList);
                }

                NodeCollection nodeList = new NodeCollection();
                Nodes targetNode = nodeList.GetTurbNode(thisTurb);

                Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(thisWakeModel);
                ModelCollection.TimeSeries[] thisTS = avgEst.timeSeries;

                if (thisTS.Length == 0)
                    avgEst.timeSeries = thisInst.modelList.GenerateTimeSeries(thisInst, thisInst.metList.GetMetsUsed(), targetNode, 
                        powerCurve, thisWakeModel, wakeCoeffs, MCP_Method);

                if (avgEst.timeSeries == null)
                    return;

                if (avgEst.timeSeries.Length == 0)
                    return;

                thisTS = avgEst.timeSeries;

                string filePath = thisInst.sfd60mWS.FileName;
                StreamWriter file = new StreamWriter(filePath);

                file.WriteLine("Estimated Hourly Estimates at Turbine: " + thisInst.GetSelectedTurbine("Monthly").name);
                file.WriteLine(DateTime.Now.ToShortDateString());

                if (powerCurve.power != null)
                    file.WriteLine("Using : " + powerCurve.name);

                if (wakeCoeffs != null)
                    file.WriteLine("Wake Model :" + thisInst.wakeModelList.CreateWakeModelString(thisWakeModel));

                file.WriteLine();

                file.Write("Timestamp, " + thisInst.modeledHeight + " WS [m/s], " + thisInst.modeledHeight + " WD, ");

                bool haveGross = false;
                bool haveNet = false;

                if (powerCurve.power != null)
                {
                    file.Write("Gross Energy [kWh],");
                    haveGross = true;
                }

                if (wakeCoeffs != null)
                {
                    file.Write("Net Energy [kWh], Waked Wind Speed [m/s], Wake Loss [%]");
                    haveNet = true;
                }

                file.WriteLine();

                int TS_length = thisTS.Length;
                double overallP50 = 1;
                
                if (haveNet)
                    overallP50 = thisInst.turbineList.exceed.GetOverallPValue_1yr(50);
                
                if (overallP50 == 0)
                    overallP50 = 1;

                for (int i = 0; i < TS_length; i++)
                {
                    file.Write(thisTS[i].dateTime + "," + Math.Round(thisTS[i].freeStreamWS, 3) + "," + thisTS[i].WD + ",");

                    if (haveGross)
                        file.Write(thisTS[i].grossEnergy + ",");

                    if (haveNet)
                    {
                        double thisWake = 100 * (thisTS[i].grossEnergy - thisTS[i].netEnergy / overallP50) / thisTS[i].grossEnergy;

                        if (thisTS[i].grossEnergy == 0)
                            thisWake = 0;

                        file.Write(thisTS[i].netEnergy + "," + Math.Round(thisTS[i].wakedWS, 3) + "," + Math.Round(thisWake, 3));
                    }

                    file.WriteLine();
                }

                file.Close();

            }
        }

        /// <summary> Exports exceedance tables/performance factor probability distribution. </summary>
        public void Export_P_tables_and_Curves(bool Exp_Curves, bool Exp_Table, bool Exp_All_P_Vals, Continuum thisInst)
        {
            string Header = "";

            if (Exp_Curves && Exp_Table)
            {
                Header = "Performance Variation Modeling: Performance Exceedance table and Performance Factors probability distributions";
            }
            else if (Exp_Curves == true && Exp_Table == false)
            {
                Header = "Performance Variation Modeling: Defined Performance Factors probability distributions";
            }
            else if (Exp_Curves == false && Exp_Table == true)
            {
                Header = "Performance Variation Modeling: Performance Exceedance table";
            }
            else if (Exp_Curves == false && Exp_All_P_Vals == true)
            {
                Header = "Performance Variation Modeling: Performance Exceedance table for all P Values";
            }

            string Header_date = DateTime.Today.ToString();
            Turbine thisTurb = thisInst.GetSelectedTurbine("Exceedance");                   
            
            if (thisInst.turbineList.exceed.compositeLoss.pVals1yr == null && Exp_All_P_Vals == true)
                return;

            string wakeString = "";
            try
            {
                wakeString = thisInst.cboExceedWake.SelectedItem.ToString();
            }
            catch { }

            Wake_Model wakeModel = thisInst.wakeModelList.GetWakeModelFromString(wakeString); 
            Turbine.Net_Energy_Est netEst = thisTurb.GetNetEnergyEst(wakeModel);
            double AEP = netEst.AEP;
            Exceedance.Monte_Carlo thisComp = thisInst.turbineList.exceed.compositeLoss;

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);
                file.WriteLine(Header);
                file.WriteLine(Header_date);
                file.WriteLine(wakeModel);
                file.WriteLine("");

                // 1 year values              

                if (Exp_Table && thisInst.turbineList.exceed.compositeLoss.isComplete)
                {
                    file.WriteLine("1 year P-table");                    
                    file.WriteLine("P_Value, 1-yr Total_PF, 1-yr AEP");
                    file.WriteLine("P1," + Math.Round(thisComp.pVals1yr[0], 6) + "," + Math.Round(thisComp.pVals1yr[0] * AEP, 2));
                    file.WriteLine("P10," + Math.Round(thisComp.pVals1yr[9], 6) + "," + Math.Round(thisComp.pVals1yr[9] * AEP, 2));
                    file.WriteLine("P50," + Math.Round(thisComp.pVals1yr[49], 6) + "," + Math.Round(thisComp.pVals1yr[49] * AEP, 2));
                    file.WriteLine("P90," + Math.Round(thisComp.pVals1yr[89], 6) + "," + Math.Round(thisComp.pVals1yr[89] * AEP, 2));
                    file.WriteLine("P99," + Math.Round(thisComp.pVals1yr[98], 6) + "," + Math.Round(thisComp.pVals1yr[98] * AEP, 2));
                    file.WriteLine("");
                }
                else if (Exp_All_P_Vals & thisComp.isComplete)
                {
                    file.WriteLine("P_Value, 1-yr Total_PF, 1-yr AEP");
                    for (int i = 0; i < 99; i++)
                    {
                        int ind = i + 1;
                        string P_str = "P" + ind;
                        file.WriteLine(P_str + "," + Math.Round(thisComp.pVals1yr[i], 3) + "," + Math.Round(thisComp.pVals1yr[i] * AEP, 2));
                    }
                    file.WriteLine("");
                }

                // 10 year values
                if (Exp_Table && thisComp.isComplete)
                {
                    file.WriteLine("10 year P-table");                    
                    file.WriteLine("P_Value, 10-yr Total_PF, 10-yr AEP");
                    file.WriteLine("P1,"+ Math.Round(thisComp.pVals10yrs[0], 3) + "," + Math.Round(thisComp.pVals10yrs[0] * AEP, 2));
                    file.WriteLine("P10," + Math.Round(thisComp.pVals10yrs[9], 3) + "," + Math.Round(thisComp.pVals10yrs[9] * AEP, 2));
                    file.WriteLine("P50," + Math.Round(thisComp.pVals10yrs[49], 3) + "," + Math.Round(thisComp.pVals10yrs[49] * AEP, 2));
                    file.WriteLine("P90," + Math.Round(thisComp.pVals10yrs[89], 3) + "," + Math.Round(thisComp.pVals10yrs[89] * AEP, 2));
                    file.WriteLine("P99," + Math.Round(thisComp.pVals10yrs[98], 3) + "," + Math.Round(thisComp.pVals10yrs[98] * AEP, 2));
                    file.WriteLine("");
                }
                else if (Exp_All_P_Vals & thisComp.isComplete)
                {
                    file.WriteLine("P_Value, 10-yr Total_PF, 10-yr AEP");
                    for (int i = 0; i < 99; i++)
                    {
                        int ind = i + 1;
                        string P_str = "P" + ind;
                        file.WriteLine(P_str + "," + Math.Round(thisComp.pVals10yrs[i], 3) + "," + Math.Round(thisComp.pVals10yrs[i] * AEP, 2));
                    }
                    file.WriteLine("");
                }

                // 20 year values                
                if (Exp_Table && thisComp.isComplete)
                {
                    file.WriteLine("20 year P-table");
                    
                    file.WriteLine("P_Value, 20-yr Total_PF, 20-yr AEP");
                    file.WriteLine("P1," + Math.Round(thisComp.pVals20yrs[0], 3) + "," + Math.Round(thisComp.pVals20yrs[0] * AEP, 2));
                    file.WriteLine("P10," + Math.Round(thisComp.pVals20yrs[9], 3) + "," + Math.Round(thisComp.pVals20yrs[9] * AEP, 2));
                    file.WriteLine("P50," + Math.Round(thisComp.pVals20yrs[49], 3) + "," + Math.Round(thisComp.pVals20yrs[49] * AEP, 2));
                    file.WriteLine("P90," + Math.Round(thisComp.pVals20yrs[89], 3) + "," + Math.Round(thisComp.pVals20yrs[89] * AEP, 2));
                    file.WriteLine("P99," + Math.Round(thisComp.pVals20yrs[98], 3) + "," + Math.Round(thisComp.pVals20yrs[98] * AEP, 2));
                    file.WriteLine("");
                }
                else if (Exp_All_P_Vals & thisComp.isComplete)
                {
                    file.WriteLine("P_Value, 20-yr Total_PF, 20-yr AEP");
                    for (int i = 0; i < 99; i++)
                    {
                        int ind = i + 1;
                        string P_str = "P" + ind;
                        file.WriteLine(P_str + "," + Math.Round(thisComp.pVals20yrs[i], 3) + "," + Math.Round(thisComp.pVals20yrs[i] * AEP, 2));
                    }
                    file.WriteLine("");
                }

                if (Exp_Curves)
                {
                    foreach (Exceedance.ExceedanceCurve PF_crv in thisInst.turbineList.exceed.exceedCurves)
                    {
                        file.WriteLine(PF_crv.exceedStr);
                        file.WriteLine("Lower bound," + Math.Round(PF_crv.lowerBound * 100, 4));
                        file.WriteLine("Upper bound," + Math.Round(PF_crv.upperBound * 100, 4));

                        if (PF_crv.modes != null)
                        {
                            for (int i = 0; i < PF_crv.modes.Length; i++)
                            {
                                file.WriteLine("Mode " + (i + 1).ToString());
                                file.WriteLine(", Mean: ," + Math.Round(PF_crv.modes[i].mean * 100, 4));
                                file.WriteLine(", SD: ," + Math.Round(PF_crv.modes[i].SD * 100, 4));
                                file.WriteLine(", Weight: ," + Math.Round(PF_crv.modes[i].weight * 100, 3));
                            }
                        }
                        else
                            file.WriteLine("Imported CDF");

                        file.Write("PF,");
                        foreach (double This_PF in PF_crv.xVals)
                            file.Write(Math.Round(This_PF * 100, 4) + ",");

                        file.WriteLine("");

                        file.Write("PDF,");
                        foreach (double This_PDF in PF_crv.probDist)
                            file.Write(Math.Round(This_PDF, 4) + ",");

                        file.WriteLine("");

                        file.Write("CDF,");
                        foreach (double This_CDF in PF_crv.cumulDist)
                            file.Write(Math.Round(This_CDF, 4) + ",");

                        file.WriteLine("");
                        file.WriteLine("");
                    }
                }

                file.Close();
            }

        }

        /// <summary>  Exports ice hit coordinates for all years modeled. </summary>
        public void ExportIceHitCoordinates(Continuum thisInst)
        {            
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);
                file.WriteLine("Ice Hit Coordinates");
                file.WriteLine(thisInst.UTM_conversions.UTMZoneNumber + " Hemisphere: " + thisInst.UTM_conversions.hemisphere);
                file.WriteLine(thisInst.savedParams.savedFileName);
                file.WriteLine();

                for (int i = 0; i < thisInst.siteSuitability.yearlyIceHits.Length; i++)
                {
                    SiteSuitability.YearlyHits theseHits = thisInst.siteSuitability.yearlyIceHits[i];
                    file.WriteLine("Year: " + (i + 1).ToString());
                    file.WriteLine("Hit Num, UTMX, UTMY, Turbine");

                    for (int j = 0; j < theseHits.iceHits.Length; j++)
                        file.WriteLine((j + 1).ToString() + "," + Math.Round(theseHits.iceHits[j].thisX, 2).ToString() + "," + Math.Round(theseHits.iceHits[j].thisZ, 2).ToString()
                            + "," + theseHits.iceHits[j].turbineName);

                    file.WriteLine();

                }

                file.Close();
            }
        }

        /// <summary>  Exports ice hits vs distance for all years modeled. </summary>
        public void ExportIceVsDistance(Continuum thisInst)
        {       
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);
                file.WriteLine("Number of Ice Hits By Distance");
                file.WriteLine(thisInst.UTM_conversions.UTMZoneNumber + " Hemisphere: " + thisInst.UTM_conversions.hemisphere);
                file.WriteLine(thisInst.savedParams.savedFileName);
                file.WriteLine();

                for (int i = 0; i < thisInst.siteSuitability.numYearsToModel; i++)
                {
                    SiteSuitability.FinalPosition[] theseHits = thisInst.siteSuitability.yearlyIceHits[i].iceHits;
                    double[] hitsByDist = thisInst.siteSuitability.CalcIceHitVersusDistance(theseHits, thisInst.metList.numWD, thisInst.turbineList.turbineEsts[0].name, thisInst);

                    file.WriteLine("Year: " + (i + 1).ToString());
                    file.WriteLine("Distance [m], Num. of Hits");

                    for (int j = 0; j < hitsByDist.Length; j++)
                        file.WriteLine(((j + 1) * 50).ToString() + "," + hitsByDist[j].ToString());

                    file.WriteLine();

                }

                file.Close();
            }
        }

        /// <summary>  Exports shadow flicker 12 x 24 hours at every zone. </summary>
        public void ExportShadowFlicker12x24(Continuum thisInst)
        {            
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);
                file.WriteLine("Number of Shadow Flicker Hours by Month and Hour of Day");
                file.WriteLine(thisInst.UTM_conversions.UTMZoneNumber + " Hemisphere: " + thisInst.UTM_conversions.hemisphere);
                file.WriteLine(thisInst.savedParams.savedFileName);
                file.WriteLine();

                for (int i = 0; i < thisInst.siteSuitability.GetNumZones(); i++)
                {
                    SiteSuitability.Zone zone = thisInst.siteSuitability.zones[i];
                    file.WriteLine("Zone: " + zone.name);
                    file.Write(",");

                    for (int m = 0; m < 12; m++)
                        file.Write(thisInst.updateThe.GetMonthString(m + 1) + ",");

                    file.WriteLine();

                    for (int h = 0; h < 24; h++)
                    {
                        for (int m = 0; m < 12; m++) // Outputs total shadow flicker hours and for each month 
                        {
                            if (m == 0) // add "All Months" and Hour name
                            {
                                string hourStr = thisInst.updateThe.GetHourString(h);
                                file.Write(hourStr + ",");
                                file.Write(thisInst.siteSuitability.GetTotalFlickerHours(zone, m, h).ToString() + ",");
                            }
                            else if (m < 11)
                                file.Write(thisInst.siteSuitability.GetTotalFlickerHours(zone, m, h) + ",");
                            else
                                file.WriteLine(thisInst.siteSuitability.GetTotalFlickerHours(zone, m, h) + ",");

                        }

                    }

                    file.WriteLine();
                }

                file.Close();
            }

        }

        /// <summary>  Exports estimated sound levels at each zone. </summary>
        public void ExportSoundZones(Continuum thisInst)
        {             
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);
                file.WriteLine("Noise Level at Zones Surrounding Turbines");
                file.WriteLine(thisInst.UTM_conversions.UTMZoneNumber + " Hemisphere: " + thisInst.UTM_conversions.hemisphere);
                file.WriteLine(thisInst.savedParams.savedFileName);
                file.WriteLine();

                file.WriteLine("Zone, Latitude [degs], Longitude [degs], Sound Level [dBA]");

                for (int i = 0; i < thisInst.siteSuitability.GetNumZones(); i++)
                {
                    SiteSuitability.Zone zone = thisInst.siteSuitability.zones[i];
                    file.Write(zone.name + "," + Math.Round(zone.latitude, 3).ToString() + "," + Math.Round(zone.longitude, 3).ToString() + ",");
                    UTM_conversion.UTM_coords theseUTM = thisInst.UTM_conversions.LLtoUTM(zone.latitude, zone.longitude);
                    double noiseLevel = thisInst.siteSuitability.CalcNoiseLevel((int)theseUTM.UTMEasting, (int)theseUTM.UTMNorthing, thisInst);
                    file.WriteLine(Math.Round(noiseLevel, 2).ToString());
                }

                file.Close();
            }
        }

        /// <summary>  Exports selected turblence intensity from Site Conditions tab. </summary>
        public void ExportTurbulenceIntensity(Continuum thisInst)
        {            
            Met thisMet = thisInst.GetSelectedMet("Site Conditions TI");
            if (thisMet.name == null) return;
            DateTime startTime = thisInst.dateTIStart.Value;
            DateTime endTime = thisInst.dateTIEnd.Value;                        
            thisMet.CalcTurbulenceIntensity(startTime, endTime, thisInst.modeledHeight, thisInst);

            string turbType = thisInst.cboTI_Type.SelectedItem.ToString();
            double[] effectiveTI = new double[thisInst.metList.numWD];

            if (turbType == "Effective")
            {
                Turbine thisTurb = thisInst.GetSelectedTurbine("Turbulence");
                double wohler = Convert.ToDouble(thisInst.cboEffectiveTI_m.SelectedItem.ToString());
                TurbineCollection.PowerCurve powerCurve = thisInst.GetSelectedPowerCurve("Turbulence");
                double terrComplCorr = 1.0;
                if (thisInst.chkApplyTCCtoEffTI.Checked)
                {
                    if (thisInst.cboTI_TerrainComplexCorr.SelectedIndex == 1)
                        terrComplCorr = 1.05;
                    else if (thisInst.cboTI_TerrainComplexCorr.SelectedIndex == 2)
                        terrComplCorr = 1.10;
                    else if (thisInst.cboTI_TerrainComplexCorr.SelectedIndex == 3)
                        terrComplCorr = 1.15;
                }                                

                if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);
                    file.WriteLine(turbType + " Turbulence Intensity at Turbine: " + thisTurb.name + " using TI from Met: " + thisMet.name);

                    if (turbType == "Effective" && thisInst.chkApplyTCCtoEffTI.Checked)
                        file.WriteLine("Adjusted by terrain complexity factor: " + thisInst.cboTI_TerrainComplexCorr.SelectedItem.ToString());

                    file.WriteLine("Measured from " + thisMet.turbulence.startTime.ToString() + " to " + thisMet.turbulence.endTime.ToString());
                    file.WriteLine();
                    file.WriteLine(thisInst.savedParams.savedFileName);
                    file.WriteLine();

                    file.Write("WS [m/s],");

                    for (int i = 0; i < thisInst.metList.numWD; i++)
                        file.Write(Math.Round(i * 360.0 / thisInst.metList.numWD, 1).ToString() + ",");

                    file.Write(", Overall");
                    file.WriteLine();

                    for (int WS_Ind = 0; WS_Ind < thisInst.metList.numWS; WS_Ind++)
                    {
                        file.Write(WS_Ind.ToString() + ",");

                        for (int WD_Ind = 0; WD_Ind < thisInst.metList.numWD; WD_Ind++)
                        {
                            effectiveTI = thisTurb.CalcEffectiveTI(thisMet, wohler, thisInst, powerCurve, WD_Ind, terrComplCorr);

                            if (effectiveTI[WS_Ind] > 0)
                                file.Write(Math.Round(effectiveTI[WS_Ind], 4).ToString() + ",");
                            else
                                file.Write(",");
                        }

                        file.Write(",");
                        effectiveTI = thisTurb.CalcEffectiveTI(thisMet, wohler, thisInst, powerCurve, thisInst.metList.numWD, terrComplCorr); // Calculates overall effective TI
                        if (effectiveTI[WS_Ind] > 0)
                            file.WriteLine(Math.Round(effectiveTI[WS_Ind], 4).ToString());
                        else
                            file.WriteLine(",");
                    }

                    file.Close();
                }

            }
            else
            {
                if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);
                    file.WriteLine(turbType + " Turbulence Intensity at Met: " + thisMet.name);
                    file.WriteLine("Measured from " + thisMet.turbulence.startTime.ToString() + " to " + thisMet.turbulence.endTime.ToString());
                    file.WriteLine();
                    file.WriteLine(thisInst.savedParams.savedFileName);
                    file.WriteLine();

                    file.Write("WS [m/s],");

                    for (int i = 0; i < thisInst.metList.numWD; i++)
                        file.Write(Math.Round(i * 360.0 / thisInst.metList.numWD, 1).ToString() + ",");

                    file.Write(", Overall, Count");
                    file.WriteLine();

                    Met.TIandCount[] overallTI = thisMet.CalcOverallTurbulenceIntensity(turbType, thisInst);

                    for (int WS_Ind = 0; WS_Ind < thisInst.metList.numWS; WS_Ind++)
                    {
                        file.Write(WS_Ind.ToString() + ",");

                        for (int WD_Ind = 0; WD_Ind < thisInst.metList.numWD; WD_Ind++)
                        {
                            if (turbType == "Average")
                            {
                                if (thisMet.turbulence.avgWS[WS_Ind, WD_Ind] > 0)
                                    file.Write(Math.Round(thisMet.turbulence.avgSD[WS_Ind, WD_Ind] / thisMet.turbulence.avgWS[WS_Ind, WD_Ind], 4).ToString() + ",");
                                else
                                    file.Write(",");
                            }
                            else if (turbType == "Representative")
                            {
                                if (thisMet.turbulence.avgWS[WS_Ind, WD_Ind] > 0 && thisMet.turbulence.p90SD[WS_Ind, WD_Ind] > 0)
                                    file.Write(Math.Round(thisMet.turbulence.p90SD[WS_Ind, WD_Ind] / thisMet.turbulence.avgWS[WS_Ind, WD_Ind], 4).ToString() + ",");
                                else
                                    file.Write(",");
                            }
                        }

                        file.WriteLine("," + Math.Round(overallTI[WS_Ind].overallTI, 4).ToString() + "," + Math.Round((double)overallTI[WS_Ind].count, 4).ToString());
                    }

                    file.Close();
                }
            }
        }

        /// <summary>  Exports extreme wind speed estimated at selected met site. </summary>
        public void ExportExtremeWS(Continuum thisInst)
        {
            Met thisMet = thisInst.GetSelectedMet("Site Conditions Extreme WS");
            Met.Extreme_WindSpeed extremeWS = thisMet.CalcExtremeWindSpeeds(thisInst);

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);

                file.WriteLine("Estimated Extreme Wind Speeds at Met: " + thisMet.name);
                file.WriteLine("Met Start date:," + thisInst.dateExtremeWS_Start.Value.ToString("yyyy-MM-dd HH:mm"));
                file.WriteLine("Met End date:," + thisInst.dateExtremeWS_End.Value.ToString("yyyy-MM-dd HH:mm"));
                file.WriteLine("Height [m]," + thisInst.cboExtremeWS_Height.SelectedItem.ToString());
                file.WriteLine();
                file.WriteLine(thisInst.savedParams.savedFileName);
                file.WriteLine();

                if (thisInst.chkUseWMO_TenMin.Checked || thisInst.chkUseWMO_Gust.Checked)
                {
                    file.WriteLine("WMO Gust Factor Used:");
                    file.WriteLine("WMO Class: " + thisInst.cboWMO_Class.SelectedItem.ToString());
                    file.WriteLine("Description: " + thisInst.txtWMO_Desc.Text);
                    if (thisInst.chkUseWMO_TenMin.Checked)
                        file.WriteLine("Hourly to Ten-Minute Gust Factor: " + thisInst.txtWMO_HourTenMin.Text);
                    if (thisInst.chkUseWMO_Gust.Checked)
                        file.WriteLine("Hourly to 3-Sec Gust Factor: " + thisInst.txtWMO_HourGust.Text);
                    file.WriteLine();
                }

                file.WriteLine("Gumbel Coefficients:");
                file.WriteLine("10-min Beta:," + thisInst.txtGumbelTenMinBeta.Text + ", 10-min Mu:," + thisInst.txtGumbelTenMinMu.Text);
                file.WriteLine("Gust Beta:," + thisInst.txtGumbelGustBeta.Text + ", Gust Mu:," + thisInst.txtGumbelGustMu.Text);

                file.WriteLine("Extreme WS Type, WS [m/s]");
                file.WriteLine("1 yr 10-min Max WS, " + Math.Round(extremeWS.tenMin1yr, 2));
                file.WriteLine("1 yr Max Gust, " + Math.Round(extremeWS.gust1yr, 2));
                file.WriteLine("50 yr 10-min Max WS, " + Math.Round(extremeWS.tenMin50yr, 2));
                file.WriteLine("50 yr Max Gust, " + Math.Round(extremeWS.gust50yr, 2));

                file.Close();
            }

        }

        /// <summary>  Exports extreme wind speed estimated at selected met site. </summary>
        public void ExportExtremeWS_Table(Continuum thisInst)
        {
            Met thisMet = thisInst.GetSelectedMet("Site Conditions Extreme WS");
            
            Met.Extreme_WindSpeed extremeWS = thisMet.CalcExtremeWindSpeeds(thisInst);

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);
                double height = Convert.ToDouble(thisInst.cboExtremeWS_Height.SelectedItem.ToString());

                file.WriteLine("Estimated Extreme Wind Speeds at Met: " + thisMet.name);
                file.WriteLine("Met Start date:," + thisInst.dateExtremeWS_Start.Value.ToString("yyyy-MM-dd HH:mm"));
                file.WriteLine("Met End date:," + thisInst.dateExtremeWS_End.Value.ToString("yyyy-MM-dd HH:mm"));
                file.WriteLine("Height [m]," + height.ToString());                
                file.WriteLine("Reference used: " + thisInst.cboExtremeWSRef.SelectedItem.ToString());

                file.WriteLine();
                file.WriteLine(thisInst.savedParams.savedFileName);
                file.WriteLine();

                if (thisInst.chkUseWMO_TenMin.Checked || thisInst.chkUseWMO_Gust.Checked)
                {
                    file.WriteLine("WMO Gust Factor Used:");
                    file.WriteLine("WMO Class: " + thisInst.cboWMO_Class.SelectedItem.ToString());
                    file.WriteLine("Description: " + thisInst.txtWMO_Desc.Text);
                    if (thisInst.chkUseWMO_TenMin.Checked)
                        file.WriteLine("Hourly to Ten-Minute Gust Factor: " + thisInst.txtWMO_HourTenMin.Text);
                    if (thisInst.chkUseWMO_Gust.Checked)
                        file.WriteLine("Hourly to 3-Sec Gust Factor: " + thisInst.txtWMO_HourGust.Text);
                    file.WriteLine();
                }

                // Print headers
                file.WriteLine("Year, Max. Yearly Ref. WS Hourly [m/s], Max. Yearly Ref. WS Hourly Concurrent with Met [m/s], Max. Yearly Actual WS 10-min [m/s]," +
                    "Max. Yearly Est. WS 10-min [m/s], Max. Yearly Actual WS Gust [m/s], Max. Yearly Est. Gust [m/s]");

                for (int y = 0; y < extremeWS.maxEstTenMin.Length; y++)
                {
                    file.Write(extremeWS.maxEstTenMin[y].thisYear + "," + extremeWS.maxHourlyRefWS[y].maxWS + ",");
                                        
                    int actWSInd = -999;

                    for (int a = 0; a < extremeWS.maxMetTenMin.Length; a++)
                        if (extremeWS.maxMetTenMin[a].thisYear == extremeWS.maxEstTenMin[y].thisYear)
                        {                            
                            actWSInd = a;
                            break;
                        }

                    if (actWSInd != -999)
                        file.Write(extremeWS.maxHourlyRefConcWS[actWSInd].maxWS + "," + extremeWS.maxMetTenMin[actWSInd].maxWS + ",");
                    else
                        file.Write(",,");

                    file.Write(extremeWS.maxEstTenMin[y].maxWS + ",");

                    actWSInd = -999;
                    if (extremeWS.maxMetGust != null)
                        for (int a = 0; a < extremeWS.maxMetGust.Length; a++)
                            if (extremeWS.maxMetGust[a].thisYear == extremeWS.maxEstTenMin[y].thisYear)
                            {
                                actWSInd = a;
                                break;
                            }

                    if (actWSInd != -999)
                        file.Write(extremeWS.maxMetGust[actWSInd].maxWS + ",");
                    else
                        file.Write(",");

                    if (extremeWS.maxEstGust != null)
                        file.WriteLine(extremeWS.maxEstGust[y].maxWS);
                    else
                        file.WriteLine();
                }

                file.Close();
            }

        }

         

        /// <summary>  Exports extreme wind shear stats (P1, P10, P50 alpha at WS ranges: 5 - 10, 10 - 15, 15+, All WS > cut-in. </summary>
        public void ExportExtremeShear(Continuum thisInst)
        {             
            Met thisMet = thisInst.GetSelectedMet("Site Conditions Shear");
            DateTime startTime = thisInst.dateTimeExtremeShearStart.Value;
            DateTime endTime = thisInst.dateTimeExtremeShearEnd.Value;

            if (thisMet.metData == null)
                return;

            double[] alphaP1_5_to_10 = thisMet.GetAlphaPValueAndCount(5, 10, 1, thisInst, startTime, endTime);
            double[] alphaP10_5_to_10 = thisMet.GetAlphaPValueAndCount(5, 10, 10, thisInst, startTime, endTime);
            double[] alphaP50_5_to_10 = thisMet.GetAlphaPValueAndCount(5, 10, 50, thisInst, startTime, endTime);

            double[] alphaP1_10_to_15 = thisMet.GetAlphaPValueAndCount(10, 15, 1, thisInst, startTime, endTime);
            double[] alphaP10_10_to_15 = thisMet.GetAlphaPValueAndCount(10, 15, 10, thisInst, startTime, endTime);
            double[] alphaP50_10_to_15 = thisMet.GetAlphaPValueAndCount(10, 15, 50, thisInst, startTime, endTime);

            double[] alphaP1_15plus = thisMet.GetAlphaPValueAndCount(15, 30, 1, thisInst, startTime, endTime);
            double[] alphaP10_15plus = thisMet.GetAlphaPValueAndCount(15, 30, 10, thisInst, startTime, endTime);
            double[] alphaP50_15plus = thisMet.GetAlphaPValueAndCount(15, 30, 50, thisInst, startTime, endTime);

            double[] alphaP1_All = thisMet.GetAlphaPValueAndCount(3, 30, 1, thisInst, startTime, endTime);
            double[] alphaP10_All = thisMet.GetAlphaPValueAndCount(3, 30, 10, thisInst, startTime, endTime);
            double[] alphaP50_All = thisMet.GetAlphaPValueAndCount(3, 30, 50, thisInst, startTime, endTime);

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);

                file.WriteLine("Estimated Extreme Wind Speeds at Met: " + thisMet.name);
                file.WriteLine("From: " + startTime.ToShortDateString() + "; To: " + endTime.ToShortDateString());
                file.WriteLine();
                file.WriteLine(thisInst.savedParams.savedFileName);
                file.WriteLine();
                file.WriteLine("P Value, 5-10 m/s, 10-15 m/s, 15+ m/s, All WS > Cut-In");
                file.WriteLine("P1, " + Math.Round(alphaP1_5_to_10[0], 3).ToString() + "," + Math.Round(alphaP1_10_to_15[0], 3).ToString() + "," +
                    Math.Round(alphaP1_15plus[0], 3).ToString() + "," + Math.Round(alphaP1_All[0], 3).ToString());
                file.WriteLine("P10, " + Math.Round(alphaP10_5_to_10[0], 3).ToString() + "," + Math.Round(alphaP10_10_to_15[0], 3).ToString() + "," +
                    Math.Round(alphaP10_15plus[0], 3).ToString() + "," + Math.Round(alphaP10_All[0], 3).ToString());
                file.WriteLine("P50, " + Math.Round(alphaP50_5_to_10[0], 3).ToString() + "," + Math.Round(alphaP50_10_to_15[0], 3).ToString() + "," +
                    Math.Round(alphaP50_15plus[0], 3).ToString() + "," + Math.Round(alphaP50_All[0], 3).ToString());
                file.WriteLine("Count, " + Math.Round(alphaP50_5_to_10[1], 0).ToString() + "," + Math.Round(alphaP50_10_to_15[1], 3).ToString() + "," +
                    Math.Round(alphaP50_15plus[1], 3).ToString() + "," + Math.Round(alphaP50_All[1], 3).ToString());

                file.Close();
            }
        }

        /// <summary>  Exports average inflow angle for each WD sector. </summary>
        public void ExportInflowAngles(Continuum thisInst)
        {           
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                if (thisInst.cboInflowRadius.SelectedItem == null)
                    thisInst.cboInflowRadius.SelectedIndex = 0;

                int radius = Convert.ToInt16(thisInst.cboInflowRadius.SelectedItem.ToString());

                if (thisInst.cboInflowReso.SelectedItem == null)
                    thisInst.cboInflowReso.SelectedIndex = 0;

                int reso = Convert.ToInt16(thisInst.cboInflowReso.SelectedItem.ToString());

                Turbine thisTurb = thisInst.GetSelectedTurbine("Inflow Angle");
                if (thisTurb.elev == 0)
                    return;

                StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);

                file.WriteLine("Average Inflow Angle by Wind Direction");
                file.WriteLine("Using radius: " + radius.ToString() + "m and resolution: " + reso.ToString() + "m");
                file.WriteLine();
                file.WriteLine(thisInst.savedParams.savedFileName);
                file.WriteLine();
                file.WriteLine("WD [degs], Inflow Angle [degs]");

                for (int WD_Ind = 0; WD_Ind < thisInst.metList.numWD; WD_Ind++)
                {
                    double thisWD = WD_Ind * 360.0 / thisInst.metList.numWD;

                    TopoInfo.TopoGrid[] elevProfile = thisInst.topo.GetElevationProfile(thisTurb.UTMX, thisTurb.UTMY, thisWD, radius, reso);

                    // Calculate slope along inflow (not downwind)
                    int numPtsInflow = radius / reso;
                    double[] xVals = new double[numPtsInflow];
                    double[] yVals = new double[numPtsInflow];

                    for (int i = 0; i < numPtsInflow; i++)
                    {
                        xVals[i] = i * reso;
                        yVals[i] = elevProfile[i].elev;
                    }

                    double[] slopeAndVar = thisInst.topo.CalcSlopeAndVariation(xVals, yVals);
                    double inflowAngle = Math.Atan(slopeAndVar[0]) * 180 / Math.PI;

                    file.WriteLine(Math.Round(thisWD, 1).ToString() + "," + Math.Round(inflowAngle, 2).ToString());
                }

                file.Close();
            }

        }

        /// <summary>  Exports average inflow angle for each WD sector. </summary>
        public void ExportElevationProfile(Continuum thisInst)
        {
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                if (thisInst.cboInflowRadius.SelectedItem == null)
                    thisInst.cboInflowRadius.SelectedIndex = 0;

                int radius = Convert.ToInt16(thisInst.cboInflowRadius.SelectedItem.ToString());

                if (thisInst.cboInflowReso.SelectedItem == null)
                    thisInst.cboInflowReso.SelectedIndex = 0;

                int reso = Convert.ToInt16(thisInst.cboInflowReso.SelectedItem.ToString());

                double selWD = Convert.ToDouble(thisInst.cboInflowWD.SelectedItem.ToString());

                Turbine thisTurb = thisInst.GetSelectedTurbine("Inflow Angle");
                if (thisTurb.elev == 0)
                    return;

                StreamWriter file = new StreamWriter(thisInst.sfd60mWS.FileName);

                file.WriteLine(thisInst.savedParams.savedFileName);
                file.WriteLine("Elevation Profile at Turbine " + thisTurb.name + " along WD = " + selWD.ToString());
                file.WriteLine("Using radius: " + radius.ToString() + "m and resolution: " + reso.ToString() + "m");
                file.WriteLine();

                bool uwOnly = thisInst.chkTerrainSlope_UWOnly.Checked;
                double binSize = 360.0 / thisInst.metList.numWD;
                int numDataPoints = Convert.ToInt32(360 * radius / reso / thisInst.metList.numWD); // Estimates approx number of total points so array resizing is reduced

                if (uwOnly == false)
                    numDataPoints = numDataPoints * 2;

                TopoInfo.TopoGrid[] elevProfile = new TopoInfo.TopoGrid[numDataPoints];
                int valInd = 0;

                double minWD = selWD - binSize / 2;

                if (minWD < 0)
                    minWD = minWD + 360;

                for (double i = 0; i < binSize; i++)
                {
                    double d = minWD + i;
                    TopoInfo.TopoGrid[] elevProfData = thisInst.topo.GetElevationProfile(thisTurb.UTMX, thisTurb.UTMY, d, (int)radius, (int)reso, uwOnly);

                    for (int p = 0; p < elevProfData.Length; p++)
                    {
                        if (valInd >= numDataPoints)
                        {
                            numDataPoints++;
                            Array.Resize(ref elevProfile, numDataPoints);
                        }

                        elevProfile[valInd] = new TopoInfo.TopoGrid();
                        elevProfile[valInd].UTMX = elevProfData[p].UTMX;
                        elevProfile[valInd].UTMY = elevProfData[p].UTMY;
                        elevProfile[valInd].elev = elevProfData[p].elev;
                        valInd++;
                    }
                }
               
                file.WriteLine("UTMX [m], UTMY [m], Elev [m]");

                for (int i = 0; i < elevProfile.Length; i++)
                    file.WriteLine(elevProfile[i].UTMX + "," + elevProfile[i].UTMY + "," + elevProfile[i].elev);
                
                file.Close();
            }

        }

        /// <summary>  Exports maximum 10-minute and maximum gust for every year of met selected on Met Data QC tab. </summary>
        public void ExportAnnualMax(Continuum thisInst)
        {
            DateTime thisExportStart = thisInst.Export_Start.Value;
            DateTime thisExportEnd = thisInst.Export_End.Value;

            int startYear = thisExportStart.Year;
            int endYear = thisExportEnd.Year;
                        
            double thisHeight = 0;

            Met thisMet = thisInst.GetSelectedMet("Met Data QC");

            // if more than one height has been estimated, ask user which one to export
            if (thisMet.metData.GetNumSimData() > 1)
            {
                Pick_a_Height userPick = new Pick_a_Height();

                for (int i = 0; i < thisMet.metData.GetNumSimData(); i++)
                    userPick.lstHeights.Items.Add(thisMet.metData.simData[i].height.ToString());

                userPick.ShowDialog();
                thisHeight = Convert.ToDouble(userPick.lstHeights.SelectedItems[0].Text);
            }
            else if (thisMet.metData.GetNumSimData() == 1)
                thisHeight = thisMet.metData.simData[0].height;
            else
                thisHeight = 80;

            if (thisInst.sfdEstimateWS.ShowDialog() == DialogResult.OK)
            {
                string filename = thisInst.sfdEstimateWS.FileName;

                try
                {
                    StreamWriter file = new StreamWriter(filename);
                    file.WriteLine("Yearly Maximum 10-Minute WS and Gust");
                    file.WriteLine("Using Average of Valid Redundant sensors");
                    file.WriteLine("Met: " + thisMet.name);
                    file.WriteLine();

                    file.Write("Year, Time Stamp max WS, max WS [m/s], Time Stamp max Gust, max Gust [m/s]");
                    file.WriteLine();

                    for (int i = startYear; i <= endYear; i++)
                    {
                        Met_Data_Filter.Yearly_Maxes These_Maxes = thisMet.metData.GetMaxWS_AndGust(i, thisHeight);

                        file.Write(i + ",");
                        file.Write(These_Maxes.timeStampMaxWS + ",");
                        file.Write(These_Maxes.maxWS + ",");
                        file.Write(These_Maxes.timeStampMaxGust + ",");
                        file.Write(These_Maxes.maxGust + ",");
                        file.WriteLine();
                    }

                    file.Close();
                }
                catch
                {
                    MessageBox.Show("Error writing to file.");
                    return;
                }

            }
        }

        /// <summary>  Exports coordinates of specified TopoGrid array. Used in GridInfo_Tests. </summary>
        public void ExportGridArray(TopoInfo.TopoGrid[] thisGrid, string folderName, string filename)
        {
            string fileName = folderName + "\\" + filename;

            try
            {
                if (thisGrid == null)
                    return;
                if (thisGrid.Length == 0)
                    return;

                StreamWriter file = new StreamWriter(fileName);

                for (int i = 0; i < thisGrid.Length; i++)
                    file.WriteLine(Math.Round(thisGrid[i].UTMX, 3) + "," + Math.Round(thisGrid[i].UTMY, 3) + "," + Math.Round(thisGrid[i].elev, 3));

                file.Close();

            }
            catch
            {
                MessageBox.Show("Error writing to file.");
                return;
            }

        }

        /// <summary> Exports hourly met time series data used in MCP. </summary>
        public void ExportMCP_TargetData(Continuum thisInst, MCP.Site_data[] targetData)
        {
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                sw.WriteLine("Time Stamp, WS [m/s], WD [deg]");

                for (int i = 0; i < targetData.Length; i++)
                    sw.WriteLine(targetData[i].thisDate + "," + targetData[i].thisWS + "," + targetData[i].thisWD);

                sw.Close();

            }
        }

        /// <summary> Exports either the terrain slope or standard deviation of terrain variation for specified radius of investigation in each wind direction sector </summary>
        /// <param name="thisInst"></param>
        public void ExportTerrainSlopeOrVariationBySector(Continuum thisInst, string slopeOrVariation, string radiusOfInterest, bool forceThruBase)
        {
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                if (slopeOrVariation == "Slope")
                    sw.WriteLine("Terrain Slope by Turbine Site");
                else
                    sw.WriteLine("Terrain Variation by Turbine Site");

                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                sw.WriteLine();

                if (thisInst.siteSuitability.forceThruTurbBase)
                    sw.WriteLine("Fitted planes forced through turbine base elevation");
                else
                    sw.WriteLine("Fitted planes NOT forced through turbine base elevation");

                sw.WriteLine();

                int numWD = thisInst.metList.numWD;

                double radius = 0;
                if (radiusOfInterest == "5z")
                    radius = thisInst.modeledHeight * 5;
                if (radiusOfInterest == "10z")
                    radius = thisInst.modeledHeight * 10;
                if (radiusOfInterest == "20z")
                    radius = thisInst.modeledHeight * 20;

                sw.Write("Turbine,");

                for (int d = 0; d < numWD; d++)
                    sw.Write("WD sect " + (d + 1).ToString() + ",");
                sw.WriteLine();

                for (int t = 0; t < thisInst.turbineList.TurbineCount; t++)
                {
                    Turbine thisTurb = thisInst.turbineList.turbineEsts[t];
                    sw.Write(thisTurb.name + ",");

                    double[] slopesOrVars = thisInst.siteSuitability.CalcTerrainSlopeOrVariationBySector(radius, slopeOrVariation, thisTurb.UTMX, thisTurb.UTMY, thisTurb.elev, true, false,
                        forceThruBase, numWD, thisInst.topo);

                    for (int d = 0; d < numWD; d++)
                        sw.Write(Math.Round(slopesOrVars[d], 5).ToString() + ",");

                    sw.WriteLine();
                }

                sw.Close();
            }
        }

        /// <summary> Exports terrain complexity values at each turbine site </summary>
        public void ExportTerrainComplexityAtTurbines(Continuum thisInst)
        {
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                double hubH = thisInst.modeledHeight;
                int numWD = thisInst.metList.numWD;
                double[] energyRose = thisInst.metList.GetAvgEnergyRose(hubH, Met.TOD.All, Met.Season.All, thisInst.metList.numWD);
                string energyRoseStr = "Avg Met Energy Rose";

                if (energyRose == null)
                {
                    energyRoseStr = "Reference Site Avg Energy Rose";
                    energyRose = thisInst.refList.CalcAvgEnergyRose(thisInst.UTM_conversions, thisInst.metList.numWD, thisInst.modelList.airDens, thisInst.modelList.rotorDiam);
                }
                
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                sw.WriteLine("Terrain Complexity Values by Turbine Site");
                sw.WriteLine("Energy Rose Used in Weighting: " + energyRoseStr);

                if (thisInst.siteSuitability.forceThruTurbBase)
                    sw.WriteLine("Fitted planes forced through turbine base elevation");
                else
                    sw.WriteLine("Fitted planes NOT forced through turbine base elevation");

                sw.WriteLine("Reference: ");
                sw.WriteLine("Turbine, Elev. [m], Complexity, 5h 360 TSI, 5h 360 TVI, 5h 30 TSI, 5h 30 TVI, 10h 30 TSI, 10h 30 TVI, 20h 30 TSI, 20h 30 TVI, P10 UW, P10 DW");
                 
                for (int t = 0; t < thisInst.turbineList.TurbineCount; t++)
                {
                    Turbine thisTurb = thisInst.turbineList.turbineEsts[t];
                    sw.Write(thisTurb.name + "," + thisTurb.elev + ",");

                    sw.Write(thisInst.siteSuitability.CalcTerrainComplexityPerIEC(thisInst.turbineList, thisInst.topo, thisInst.modeledHeight, energyRose, "WTG", numWD, thisTurb) + ",");

                    double[] terrainComplex = thisInst.siteSuitability.CalcTerrainSlopeAndVariationIndexPerIEC(thisTurb.UTMX, thisTurb.UTMY, thisTurb.elev, 5 * hubH, thisInst.topo, numWD);
                    sw.Write(Math.Round(terrainComplex[0], 2) + "," + Math.Round(terrainComplex[1], 4) + ",");

                    terrainComplex = thisInst.siteSuitability.CalcTerrainSlopeAndVariationIndexPerIEC(thisTurb.UTMX, thisTurb.UTMY, thisTurb.elev, 5 * hubH, thisInst.topo, numWD, energyRose);
                    sw.Write(Math.Round(terrainComplex[0], 2) + "," + Math.Round(terrainComplex[1], 4) + ",");

                    terrainComplex = thisInst.siteSuitability.CalcTerrainSlopeAndVariationIndexPerIEC(thisTurb.UTMX, thisTurb.UTMY, thisTurb.elev, 10 * hubH, thisInst.topo, numWD, energyRose);
                    sw.Write(Math.Round(terrainComplex[0], 2) + "," + Math.Round(terrainComplex[1], 4) + ",");

                    terrainComplex = thisInst.siteSuitability.CalcTerrainSlopeAndVariationIndexPerIEC(thisTurb.UTMX, thisTurb.UTMY, thisTurb.elev, 20 * hubH, thisInst.topo, numWD, energyRose);
                    sw.Write(Math.Round(terrainComplex[0], 2) + "," + Math.Round(terrainComplex[1], 4) + ",");

                    // P10 UW & DW Exposure R = 6000
                    double thisUW_P10 = thisTurb.gridStats.GetOverallP10(energyRose, 2, "UW");
                    double thisDW_P10 = thisTurb.gridStats.GetOverallP10(energyRose, 2, "DW");
                    sw.WriteLine(Math.Round(thisUW_P10, 2) + "," + Math.Round(thisDW_P10, 2) + ",");
                }

                sw.Close();
            }
        }

        /// <summary> Exports histogram of shear exponents for selected met, date range, and wind speed range </summary>
        /// <param name="thisInst"></param>
        public void ExportShearHistogram(Continuum thisInst)
        {
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                sw.WriteLine("Shear Exponent Histogram");
                sw.WriteLine();

                Met thisMet = thisInst.GetSelectedMet("Site Conditions Extreme WS");
                DateTime startTime = thisInst.dateTimeExtremeShearStart.Value;
                DateTime endTime = thisInst.dateTimeExtremeShearEnd.Value;

                string rangeAlpha = thisInst.cboExtremeShearRange.SelectedItem.ToString();
                double[] thisHisto = thisMet.GetAlphaHistogram(rangeAlpha, thisInst, startTime, endTime);

                sw.WriteLine("Using met site:," + thisMet.name);
                sw.WriteLine("From:," + startTime.ToString());
                sw.WriteLine("To:," + startTime.ToString());
                sw.WriteLine("Wind Speed Range:," + rangeAlpha.ToString());
                sw.WriteLine();

                sw.WriteLine("Alpha, Count");

                for (int h = 0; h < thisHisto.Length; h++)
                    sw.WriteLine(-0.5 + h * 0.02 + "," + thisHisto[h]);

                sw.Close();

            }
        }

        /// <summary> Exports summary of all models' RMS errors (overall and sectorwise) </summary>
        /// <param name="thisInst"></param>
        public void ExportModelRMS_Errors(Continuum thisInst)
        {
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                sw.WriteLine("Continuum Model RMS Error Summary");
                sw.WriteLine();

                sw.WriteLine("Mets used: " + thisInst.metList.CreateMetString(thisInst.metList.GetMetsUsed(), true));

                if (thisInst.topo.useSR == true)
                    sw.WriteLine("Surface roughness model used");
                else
                    sw.WriteLine("Surface roughness model NOT used");

                if (thisInst.topo.useSepMod == true)
                    sw.WriteLine("Flow separation model used");

                if (thisInst.topo.useElev == true)
                    sw.WriteLine("Elevation model used");
                else
                    sw.WriteLine("Elevation model NOT used");

                if (thisInst.topo.useValley == true)
                    sw.WriteLine("Valley flow model used");
                else
                    sw.WriteLine("Valley flow model NOT used");

                sw.WriteLine();

                Met.TOD thisTOD = thisInst.GetSelectedTOD("Advanced");
                Met.Season thisSeason = thisInst.GetSelectedSeason("Advanced");
                Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), thisTOD, thisSeason, thisInst.modeledHeight, false);

                for (int r = 0; r < thisInst.radiiList.ThisCount; r++)
                {
                    Model thisModel = theseModels[r];
                    sw.WriteLine("Radius of investigation:," + thisInst.radiiList.investItem[r].radius.ToString());
                    sw.WriteLine("RMS Overall Error [%]," + thisModel.RMS_WS_Est.ToString());
                    sw.WriteLine("WD, RMS Error");

                    for (int d = 0; d < thisModel.RMS_Sect_WS_Est.Length; d++)
                        sw.WriteLine(Math.Round(d * 360.0 / thisInst.metList.numWD, 2).ToString() + "," + thisModel.RMS_Sect_WS_Est[d].ToString());

                    sw.WriteLine();
                }

                sw.Close();

            }
        }

        /// <summary> Exports MERRA2 cloud cover data to CSV </summary>        
        public void ExportCloudData(CloudCover cloudData, string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine("MERRA2 Cloud Cover Data");
            sw.WriteLine("Lat: " + cloudData.coords.Lat.ToString() + ", Long:" + cloudData.coords.Lon.ToString());
            sw.WriteLine();

            // Write headers
            sw.WriteLine("Timestamp, ISCCP Cloud Cover Fraction, Modis Cloud Cover Fraction, Modis Cloud Optical Thickness");

            for (int t = 0; t < cloudData.merraCloud.Length; t++)
            {
                sw.WriteLine(cloudData.merraCloud[t].thisDate.ToString() + "," + cloudData.merraCloud[t].isccpCloudCover.ToString()
                    + "," + cloudData.merraCloud[t].modisCloudCover.ToString() + "," + cloudData.merraCloud[t].modisCloudThickness.ToString());
            }

            sw.Close();

        }

        /// <summary> Exports overall summary results of all Round Robin analyses </summary>
        public void Export_RoundRobinSummary(Continuum thisInst)
        {
            MetPairCollection.RR_WS_Ests[] allRoundRobins = thisInst.metPairList.roundRobinEsts;

            if (allRoundRobins == null)
            {
                MessageBox.Show("No Round Robin analyses have been generated yet.", "Continuum 3");
                return;
            }

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                try
                {
                    sw.WriteLine("Continuum 3: Summary of Modeled " + thisInst.modeledHeight + " m Wind Speeds: Round Robin Analysis Results");
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine();

                    // Write headers
                    sw.WriteLine("Met Subset Size, RMS Error [%], Num. Models, Min Error [%], Max Error [%]");

                    for (int r = 0; r < allRoundRobins.Length; r++)
                    {
                        sw.Write(allRoundRobins[r].metSubSize.ToString() + ",");
                        sw.Write(allRoundRobins[r].RMS_All.ToString() + ",");
                        sw.Write(allRoundRobins[r].RMS_Err.Length.ToString() + ",");
                        sw.Write(ArrayStatistics.Minimum(allRoundRobins[r].RMS_Err).ToString() + ",");
                        sw.WriteLine(ArrayStatistics.Maximum(allRoundRobins[r].RMS_Err).ToString());
                    }

                    sw.Close();
                }
                catch
                {
                    MessageBox.Show("Error writing to file.  Make sure that it is not open in another program");
                    sw.Close();
                    return;
                }

            }
        }
    }

}
