using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ContinuumNS
{
    public class Export
    {
        public void ExportMetCrossPreds(Continuum thisInst)
        { 
            // Exports the met cross-prediction error for selected radius and WD sector
                        
            string headerString = "Continuum 2.3: Met Cross-Predictions";
            int numPairs = thisInst.metPairList.PairCount;
            StreamWriter sw;
           
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK) {
                try
                {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);                    
                }                    
                catch {
                    MessageBox.Show("Error writing to file.  Make sure the file is closed.", "Continuum 2.3");
                    return;
                }

                sw.WriteLine(headerString);
                sw.WriteLine(DateTime.Now);
                sw.WriteLine("");
                               
                bool isCalibrated = thisInst.GetSelectedModel("Net");
                Model[] theseModels = thisInst.GetModels(thisInst, "Net");

                if (isCalibrated == true)
                    headerString = "Using Site-Calibrated Model";
                else                    
                    headerString = "Using Default Model";

                sw.WriteLine(headerString);

                if (thisInst.topo.useSR == true)
                    headerString = "Surface roughness model USED";
                else
                    headerString = "Surface roughness model NOT USED";

                sw.WriteLine(headerString);

                if (thisInst.topo.useSepMod == true)
                    headerString = "Flow Separation model USED";
                else
                    headerString = "Flow Separation model NOT USED";

                sw.WriteLine(headerString);

                int radiusIndex = thisInst.GetRadiusInd("Net");
                int radius = thisInst.radiiList.investItem[radiusIndex].radius;
              
                sw.Write("radius = " + radius.ToString());
                sw.WriteLine();

                int WD_Ind = thisInst.GetWD_ind( "Net");
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

                for (int j = 0; j < numPairs; j++) {
                    Pair_Of_Mets thisPair = thisInst.metPairList.metPairs[j];

                    sw.Write(thisPair.met1.name + ",");
                    sw.Write(thisPair.met2.name + ",");
                    int WS_PredInd = thisPair.GetWS_PredInd(theseModels, thisInst.modelList);

                    if (WD_Ind == numWD)
                        sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].percErr[0], 4).ToString("P"));
                    else
                        sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].percErrSector[0, WD_Ind], 4).ToString("P"));

                    sw.WriteLine();

                    sw.Write(thisPair.met2.name + ",");
                    sw.Write(thisPair.met1.name + ",");
                    if (WD_Ind == numWD)
                        sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].percErr[1], 4).ToString("P"));
                    else
                        sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].percErrSector[1, WD_Ind], 4).ToString("P"));

                    sw.WriteLine();

                }
                sw.Close();
            }                       

        }

        public void ExportNodesAndWS(Continuum thisInst)
        {
            // Exports calculations and estimates along path of nodes between selected start and end site

            int numPairs = thisInst.metPairList.PairCount;
            if (numPairs == 0)
            {
                MessageBox.Show("Need to create met pairs and cross-predict first.  Click button on top left.", "Continuum 2.3");
                return;
            }                        
                        
            Pair_Of_Mets thisPair = null;            
            int numRadii = thisInst.radiiList.ThisCount;
            int numWD = thisInst.GetNumWD();
                        
            int radiusIndex = thisInst.GetRadiusInd("Advanced");
            int radius = thisInst.radiiList.investItem[radiusIndex].radius;
            int WD_Ind = thisInst.GetWD_ind("Advanced");
            bool isCalibrated = thisInst.GetSelectedModel("Advanced");
            
            Model[] thisModel = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), radius, radius, isCalibrated);

            string startStr = thisInst.GetStartMetAdvanced();
            string endStr = thisInst.GetEndSiteAdvanced();
            string headerString = "Continuum 2.3: Estimated and Calculated Values from " + startStr + " to " + endStr;
                        
            int numMets = thisInst.metList.ThisCount;            
            int numTurbines = thisInst.turbineList.TurbineCount;
            StreamWriter sw;                       

            bool gotSR = thisInst.topo.gotSR;
            
            // Figure out if end is a met or a turbine
            bool endIsTurb = true;

            for (int i = 0; i < numMets; i++) {
                Met thisMet = thisInst.metList.metItem[i];
                if (thisMet.name == endStr) {
                    endIsTurb = false;
                    break;
                }
            }                  

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK) {
                try {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                }
                catch {
                    MessageBox.Show("Error writing to file.  Make sure the file is closed.", "Continuum 2.3");
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

                if (isCalibrated == true)
                    sw.Write("Default,");
                else
                    sw.Write("Site-Calibrated,");

                sw.WriteLine();
                sw.WriteLine();

                sw.Write("Met/Node, UTMX, UTMY, elev, P10 UW, P10 DW, UW Expo, DW Expo,");

                if (gotSR == true)                
                    sw.Write("UW roughness, DW roughness, UW Disp H, DW Disp H,");

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

                    sw.Write(met1.name + "," + Math.Round(met1.UTMX,0).ToString() + "," + Math.Round(met1.UTMY,0).ToString() + "," + Math.Round(met1.elev,1).ToString() + ",");

                    // Output Met 1 values
                    if (WD_Ind == numWD)
                    {
                        sw.Write(Math.Round(met1.gridStats.GetOverallP10(met1.windRose, radiusIndex, "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(met1.gridStats.GetOverallP10(met1.windRose, radiusIndex, "DW"), 3).ToString() + ",");
                        sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1.windRose, "Expo", "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1.windRose, "Expo", "DW"), 3).ToString() + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1.windRose, "SR", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1.windRose, "SR", "DW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1.windRose, "DH", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met1.expo[radiusIndex].GetOverallValue(met1.windRose, "DH", "DW"), 3).ToString() + ",");
                        }
                        sw.Write(met1.WS.ToString() + ",");
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
                        sw.Write(Math.Round(met1.WS * met1.sectorWS_Ratio[WD_Ind], 3).ToString() + ",");
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
                            sw.Write(Math.Round(this_Node.gridStats.GetOverallP10(this_Node.windRose, radiusIndex, "UW"), 3) + ",");
                            sw.Write(Math.Round(this_Node.gridStats.GetOverallP10(this_Node.windRose, radiusIndex, "DW"), 3) + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "Expo", "UW"), 3) + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "Expo", "DW"), 3) + ",");
                            if (gotSR == true)
                            {
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "SR", "UW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "SR", "DW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "DH", "UW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "DH", "DW"), 3).ToString() + ",");
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
                        sw.Write(Math.Round(met2.gridStats.GetOverallP10(met2.windRose, radiusIndex, "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(met2.gridStats.GetOverallP10(met2.windRose, radiusIndex, "DW"), 3).ToString() + ",");
                        sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2.windRose, "Expo", "UW"), 3) + ",");
                        sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2.windRose, "Expo", "DW"), 3) + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2.windRose, "SR", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2.windRose, "SR", "DW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2.windRose, "DH", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(met2.expo[radiusIndex].GetOverallValue(met2.windRose, "DH", "DW"), 3).ToString() + ",");
                        }
                        if (is1to2)
                            sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].WS_Ests[0], 3).ToString() + ",");
                        else
                            sw.Write(Math.Round(thisPair.WS_Pred[WS_PredInd, radiusIndex].WS_Ests[1], 3).ToString() + ",");

                        sw.Write(Math.Round(met2.WS, 3).ToString() + ",");
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
                        sw.Write(Math.Round(met2.WS * met2.sectorWS_Ratio[WD_Ind], 3).ToString() + ",");
                    }

                }
                else {
                    // Met to turb                   
                                       
                    NodeCollection nodeList = new NodeCollection();

                    Met thisMet = thisInst.metList.GetMet(startStr);
                    Turbine thisTurb = thisInst.turbineList.GetTurbine(endStr);
                    Turbine.WS_Ests thisWS_Est = thisTurb.GetWS_Est(radius, thisMet.name, thisModel[radiusIndex]);

                    sw.Write(thisMet.name + "," + Math.Round(thisMet.UTMX, 0).ToString() + "," + Math.Round(thisMet.UTMY, 0).ToString() + "," + Math.Round(thisMet.elev, 1).ToString() + ",");

                    // Output Met 1 values
                    if (WD_Ind == numWD)
                    {
                        sw.Write(Math.Round(thisMet.gridStats.GetOverallP10(thisMet.windRose, radiusIndex, "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(thisMet.gridStats.GetOverallP10(thisMet.windRose, radiusIndex, "DW"), 3).ToString() + ",");
                        sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisMet.windRose, "Expo", "UW"), 3) + ",");
                        sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisMet.windRose, "Expo", "DW"), 3) + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisMet.windRose, "SR", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisMet.windRose, "SR", "DW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisMet.windRose, "DH", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisMet.expo[radiusIndex].GetOverallValue(thisMet.windRose, "DH", "DW"), 3).ToString() + ",");
                        }
                        sw.Write(Math.Round(thisMet.WS, 3).ToString() + ",");
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
                        sw.Write(Math.Round(thisMet.WS * thisMet.sectorWS_Ratio[WD_Ind], 3).ToString() + ",");
                    }

                    sw.WriteLine();

                    // Output values along nodes
                    int numNodes = thisWS_Est.pathOfNodesUTMs.Length;

                    for (int j = 0; j < numNodes; j++)
                    {
                        sw.Write("Node " + (j + 1).ToString() + ",");
                        Nodes[] dummy = null;
                        Nodes this_Node = nodeList.GetANode(thisWS_Est.pathOfNodesUTMs[j].UTMX, thisWS_Est.pathOfNodesUTMs[j].UTMY, thisInst, ref dummy, null);
                        sw.Write(Math.Round(this_Node.UTMX, 0).ToString() + "," + Math.Round(this_Node.UTMY, 0).ToString() + "," + Math.Round(this_Node.elev, 1).ToString() + ",");

                        if (WD_Ind == numWD)
                        {
                            sw.Write(Math.Round(this_Node.gridStats.GetOverallP10(this_Node.windRose, radiusIndex, "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.gridStats.GetOverallP10(this_Node.windRose, radiusIndex, "DW"), 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "Expo", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "Expo", "DW"), 3).ToString() + ",");
                            if (gotSR == true)
                            {
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "SR", "UW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "SR", "DW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "DH", "UW"), 3).ToString() + ",");
                                sw.Write(Math.Round(this_Node.expo[radiusIndex].GetOverallValue(this_Node.windRose, "DH", "DW"), 3).ToString() + ",");
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
                        sw.Write(Math.Round(thisTurb.gridStats.GetOverallP10(thisTurb.windRose, radiusIndex, "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(thisTurb.gridStats.GetOverallP10(thisTurb.windRose, radiusIndex, "DW"), 3).ToString() + ",");
                        sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(thisTurb.windRose, "Expo", "UW"), 3).ToString() + ",");
                        sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(thisTurb.windRose, "Expo", "DW"), 3).ToString() + ",");
                        if (gotSR == true)
                        {
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(thisTurb.windRose, "SR", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(thisTurb.windRose, "SR", "DW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(thisTurb.windRose, "DH", "UW"), 3).ToString() + ",");
                            sw.Write(Math.Round(thisTurb.expo[radiusIndex].GetOverallValue(thisTurb.windRose, "DH", "DW"), 3).ToString() + ",");
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

        public void Export_CRV(Continuum thisInst, string powerCurve)
        {
            // Exports power curve and thrust curve of selected thisPowerCurve
            int numPowerCurves = thisInst.turbineList.PowerCurveCount;
            TurbineCollection.PowerCurve thisPowerCurve = new TurbineCollection.PowerCurve();
            
            for (int i = 0; i < numPowerCurves; i++) {
                thisPowerCurve = thisInst.turbineList.powerCurves[i];

                if (thisPowerCurve.name == powerCurve)
                    break;
            }

            double WS_FirstInt = 0;
            double WS_IntSize = 0;

            if (thisInst.metList.ThisCount > 0) {
                WS_FirstInt = thisInst.metList.metItem[0].WS_FirstInt;
                WS_IntSize = thisInst.metList.metItem[0].WS_IntSize;
            }
                       
            string headerString = "Continuum 2.3: Exported power Curve";

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK) {

                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                sw.WriteLine(headerString);
                sw.WriteLine(DateTime.Now);

                sw.Write("power Curve : ");
                sw.Write(thisPowerCurve.name);
                sw.WriteLine();

                sw.Write("Cut-In WS =");
                sw.Write(thisPowerCurve.cutInWS);
                sw.WriteLine();

                sw.Write("Cut-Out WS =");
                sw.Write(thisPowerCurve.cutOutWS);
                sw.WriteLine();

                sw.Write("Rated power =");
                sw.Write(thisPowerCurve.ratedPower);
                sw.WriteLine();

                sw.Write("WS [m],/s");
                sw.Write("power, kW");
                sw.WriteLine();
                                
                for (int i = 0; i < thisPowerCurve.power.Length; i++) {
                    sw.Write(WS_FirstInt + WS_IntSize * i);
                    sw.Write(thisPowerCurve.power[i]);
                    sw.WriteLine();
                }

                sw.Close();
            }
        }

        public void Export_RoundRobin_CSV(Continuum thisInst)
        {

            MetPairCollection.RR_WS_Ests[] allRoundRobins = thisInst.metPairList.roundRobinEsts;

            // Exports results of Math.Round Robin analysis
            if (allRoundRobins == null) {
                MessageBox.Show("No Round Robin analyses have been generated yet.", "Continuum 2.3");
                return;
            }

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK) {

                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                try
                {
                    sw.WriteLine("Continuum 2.3: Modeled " + thisInst.metList.metItem[0].height + " m Wind Speeds: Round Robin Analysis Results");
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
                                sw.Write(Math.Round(thisRR.avgWS_Ests[j, k].avgWS,3).ToString() + ",");
                                sw.Write(thisRR.avgWS_Ests[j, k].estError.ToString("P") + ",");
                                sw.WriteLine();
                            }
                        }
                        sw.WriteLine();
                    }
                }
                catch
                {
                    MessageBox.Show("Error writing to file.", "Continuum 2.3");
                    return;
                }

                sw.Close();              
            
            }
        }

        public void Export_Models_CSV(Continuum thisInst)
        {
            // Exports selected model coefficients (i.e. power law A and B, etc)                     
                        
            int numWD = thisInst.GetNumWD();
            int numRadii = thisInst.radiiList.ThisCount;

            bool useSR = false;
            if (thisInst.topo.useSR == true)
                useSR = true;
                    
            bool useFlowSep = false;
            if (thisInst.topo.useSepMod == true)
                useFlowSep = true;                        
            
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                sw.WriteLine("Continuum 2.3 Model Parameters");
                sw.WriteLine(DateTime.Now);
                                                 
                Model[] theseModels = thisInst.GetModels(thisInst, "Advanced");
                if (theseModels == null)
                    return;

                int numMetsUsed = theseModels[0].metsUsed.Length;
                string metStr = thisInst.metList.CreateMetString(theseModels[0].metsUsed, true);
                bool isCalibrated = thisInst.GetSelectedModel("Advanced");

                if (isCalibrated == false)
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
                        sw.Write(Math.Round(thisModel.RMS_Sect_WS_Est[WD_Ind], 5).ToString("P") + ",");
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

        public void Export_WS_AEP(Continuum thisInst, string powerCurve)
        {
            // Exports estimated WS and gross energy production at turbine sites 
            if (thisInst.turbineList.turbineCalcsDone == true)
            {
                Met[] theseMets = thisInst.GetCheckedMets("Gross");
                Turbine[] theseTurbines = thisInst.GetCheckedTurbs("Gross");
                int numMets = theseMets.Count();
                int numTurbines = theseTurbines.Count();
                                
                bool haveAEP = false;               
                bool isCalibrated = thisInst.GetSelectedModel("Gross");               

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

                if (thisInst.UTM_conversions.UTMZoneNumber == -999)
                {
                    Select_UTM_zone thisZone = new Select_UTM_zone();
                    thisZone.cboHemisphere.SelectedIndex = 0;
                    thisZone.ShowDialog();
                    thisInst.UTM_conversions.UTMZoneNumber = Convert.ToInt16(thisZone.cbo_UTMNumber.SelectedItem);
                    thisInst.UTM_conversions.hemisphere = thisZone.cboHemisphere.SelectedItem.ToString();
                }

                StreamWriter sw = null;
                int avgWS_Ind = 0;
                TurbineCollection.PowerCurve thisPowerCurve = new TurbineCollection.PowerCurve();

                if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                        string headerString = "Continuum 2.3: Estimated Gross WS and AEP at Met and Turbine Sites";
                        sw.WriteLine(headerString);

                        if (powerCurve == "No power Curves Imported")                                                    
                            haveAEP = false;                        
                        else
                        {
                            headerString = "Using power Curve: " + powerCurve;
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
                            sw.Write("latitude [degs],");
                            sw.Write("longitude [degs],");
                        }

                        sw.Write("elev [m],");
                        sw.Write("WS [m/s],");
                        sw.Write("weibull k,");
                        sw.Write("weibull A,");

                        if (haveAEP == true) {
                            sw.Write("AEP [MWh],");
                            sw.Write("CF [%],");
                        }                                               

                        sw.WriteLine();

                        for (int i = 0; i < numMets; i++)
                        {
                            sw.Write(theseMets[i].name + ",");

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
                            sw.Write(Math.Round(theseMets[i].WS, 3).ToString() + ", ");
                            MetCollection.Weibull_params weibull = thisInst.metList.CalcWeibullParams(theseMets[i].WS_Dist, theseMets[i].sectorWS_Dist, theseMets[i].WS);
                            sw.Write(Math.Round(weibull.overall_k, 2).ToString() + ", ");
                            sw.Write(Math.Round(weibull.overall_A, 2).ToString() + ", ");

                            if (haveAEP == true)
                            {
                                double metAEP = thisInst.turbineList.CalcAndReturnGrossAEP(theseMets[i].WS_Dist, powerCurve);
                                double metCF = thisInst.turbineList.CalcCapacityFactor(metAEP, thisPowerCurve.ratedPower);
                                sw.Write(Math.Round(metAEP, 1).ToString() + ", ");
                                sw.Write(Math.Round(metCF, 4).ToString("P") + ", ");
                            }

                            sw.WriteLine();
                        }

                        for (int i = 0; i < numTurbines; i++)
                        {
                            for (int j = 0; j < theseTurbines[i].AvgWSEst_Count; j++)
                            {
                                if (theseTurbines[i].avgWS_Est[j].isCalibrated == isCalibrated)
                                {
                                    avgWS_Ind = j;
                                    break;
                                }
                            }

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
                            sw.Write(Math.Round(theseTurbines[i].avgWS_Est[avgWS_Ind].WS, 3).ToString() + ", ");
                            sw.Write(Math.Round(theseTurbines[i].avgWS_Est[avgWS_Ind].weibull_k, 2).ToString() + ", ");
                            sw.Write(Math.Round(theseTurbines[i].avgWS_Est[avgWS_Ind].weibull_A, 2).ToString() + ", ");

                            if (haveAEP == true)
                            {
                                for (int j = 0; j < theseTurbines[i].GrossAEP_Count; j++)
                                {
                                    if (theseTurbines[i].grossAEP[j].powerCurve.name == powerCurve && theseTurbines[i].grossAEP[j].isCalibrated == isCalibrated)
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
                        MessageBox.Show("Error writing to file.", "Continuum 2.3");
                        sw.Close();
                    }
                }
            }
            else
                MessageBox.Show("Need to do Turbine calculations first.", "Continuum 2.3");           

        }

        public void Export_Net_WS_AEP(Continuum thisInst)
            {
            // Exports estimated WS and net energy production and wake loss at turbine sites

            if (thisInst.wakeModelList.NumWakeModels > 0)
            {                                                
                Turbine[] turbsToExport = thisInst.GetCheckedTurbs("Net");
                
                int numWD = thisInst.GetNumWD();
                int WD_Ind = thisInst.GetWD_ind("Net");
                Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();
                string Wake_model_name = thisInst.wakeModelList.GetWakeModelName(thisWakeModel.wakeModelType);

                string Model_type = "";
                bool isCalibrated = thisInst.GetSelectedModel("Net");
                if (isCalibrated == false)                           
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
                        sw.WriteLine("Continuum 2.3: Estimated Net WS and AEP at Turbine Sites");
                        sw.WriteLine("Using power Curve: " + thisWakeModel.powerCurve.name);
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
                            sw.Write("string #,");
                            sw.Write("UTMX,");
                            sw.Write("UTMY,");
                        }
                        else
                        {
                            sw.Write("Site,");
                            sw.Write("string #,");
                            sw.Write("latitude,");
                            sw.Write("longitude,");
                        }

                        sw.Write("elev [m],");
                        sw.Write("Free-stream WS [m/s],");
                        sw.Write("Waked WS [m/s],");
                        sw.Write("Net AEP [MWh],");
                        sw.Write("Wake Loss [%],");
                        sw.Write("weibull k,");
                        sw.Write("weibull A,");
                                           
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

                                if (thisInst.UTM_conversions.UTMZoneNumber == -999)
                                {
                                    Select_UTM_zone This_Zone = new Select_UTM_zone();
                                    This_Zone.cboHemisphere.SelectedIndex = 0;
                                    This_Zone.ShowDialog();
                                    thisInst.UTM_conversions.UTMZoneNumber = Convert.ToInt16(This_Zone.cbo_UTMNumber.SelectedItem);
                                    thisInst.UTM_conversions.hemisphere = This_Zone.cboHemisphere.SelectedItem.ToString();
                                }

                                thisLL = thisInst.UTM_conversions.UTMtoLL(thisTurb.UTMX, thisTurb.UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");
                            }

                            sw.Write(Math.Round(thisTurb.elev, 2).ToString() + ",");

                            double Avg_WS_est = thisTurb.GetAvgOrSectorWS_Est(isCalibrated, thisWakeModel, WD_Ind, "WS"); // Net average wind speed estimate
                            double FS_Avg_WS_est = thisTurb.GetAvgOrSectorWS_Est(isCalibrated, null, WD_Ind, "WS"); // Gross (i.e. unwaked) average wind speed estimate
                            double Net_Est = thisTurb.GetNetAEP(thisWakeModel, isCalibrated, WD_Ind); // Net AEP   
                            double wakeLoss = thisTurb.GetWakeLoss(thisWakeModel, isCalibrated, WD_Ind);
                            double Weib_k = thisTurb.GetAvgOrSectorWS_Est(isCalibrated, thisWakeModel, WD_Ind, "WeibK");
                            double Weib_A = thisTurb.GetAvgOrSectorWS_Est(isCalibrated, thisWakeModel, WD_Ind, "WeibA");

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
                        sw.WriteLine("Availability: ,");

                        sw.WriteLine(", Turbine: ," + Math.Round(thisInst.otherLosses.Turb_Avail_Loss, 4).ToString("P"));
                        sw.WriteLine(", BOP: ," + Math.Round(thisInst.otherLosses.BOP_Avail_Loss, 4).ToString("P"));
                        sw.WriteLine("Electrical: ," + Math.Round(thisInst.otherLosses.Electrical_Loss, 4).ToString("P"));

                        sw.WriteLine("Environmental: ,");
                        sw.WriteLine(", Icing: ," + Math.Round(thisInst.otherLosses.Icing_Loss, 4).ToString("P"));
                        sw.WriteLine(", Blade Soiling: ," + Math.Round(thisInst.otherLosses.Blade_Soil_Loss, 4).ToString("P"));                        
                        sw.WriteLine(", Blade Degradation: ," + Math.Round(thisInst.otherLosses.Blade_Degrade_Loss, 4).ToString("P"));
                        sw.WriteLine(", High/Low Temperature: ," + Math.Round(thisInst.otherLosses.HighLowTemp_Loss, 4).ToString("P"));

                        sw.WriteLine("Turbine Performance: ,");
                        sw.WriteLine(", power Curve: ," + Math.Round(thisInst.otherLosses.Power_Crv_Loss, 4).ToString("P"));
                        sw.WriteLine(", Turbulence: ," + Math.Round(thisInst.otherLosses.Turbulence_Loss, 4).ToString("P"));

                        sw.WriteLine("Curtailment: ,");
                        sw.WriteLine(", Grid Curtail: ," + Math.Round(thisInst.otherLosses.Grid_Curtail_Loss, 4).ToString("P"));
                        sw.WriteLine(", Environmental Curtail: ," + Math.Round(thisInst.otherLosses.Environ_Curtail_Loss, 4).ToString("P"));
                        sw.WriteLine(", Sector Management: ," + Math.Round(thisInst.otherLosses.Wake_Sect_Curtail_Loss, 4).ToString("P"));
                        
                        sw.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error writing to file.", "Continuum 2.3");                        
                    }
                }
            }
            else
                MessageBox.Show("Need to do Turbine calculations first.", "Continuum 2.3");           
            
        }

        public void Export_Directional_WS(Continuum thisInst, string tabName)
        {
            // Exports either gross or net sectorwise WS at turbine sites and met sites
            if (thisInst.turbineList.turbineCalcsDone == true)
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
                
                bool isCalibrated = thisInst.GetSelectedModel(tabName);
                
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

                if (thisInst.UTM_conversions.UTMZoneNumber == -999)
                {
                    Select_UTM_zone thisZone = new Select_UTM_zone();
                    thisZone.cboHemisphere.SelectedIndex = 0;
                    thisZone.ShowDialog();
                    thisInst.UTM_conversions.UTMZoneNumber = Convert.ToInt16(thisZone.cbo_UTMNumber.SelectedItem);
                    thisInst.UTM_conversions.hemisphere = thisZone.cboHemisphere.SelectedItem.ToString();
                }

                double dirBin = (double)360 / numWD;
                
                Turbine.Avg_Est avgEst = new Turbine.Avg_Est();
                
                if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
                {
                    try {
                        StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                        
                        if (tabName == "Gross")
                            sw.WriteLine("Continuum 2.3: Gross Directional WS [m/s] at Met and Turbine Sites");
                        else
                            sw.WriteLine("Continuum 2.3: Net Directional WS [m/s] at Turbine Sites");
                                                
                        if (latsOrUTMs == 1) {
                            sw.WriteLine("UTM Zone: " + thisInst.UTM_conversions.UTMZoneNumber + " " + thisInst.UTM_conversions.hemisphere + " hemisphere");
                            sw.Write("Site,");
                            sw.Write("UTMX [m],");
                            sw.Write("UTMY [m],");
                        }
                        else {
                            sw.Write("Site,");
                            sw.Write("latitude [degs],");
                            sw.Write("longitude [degs],");
                        }

                        sw.Write("elev [m],");
                        for (int WD = 0; WD < numWD; WD++)
                            sw.Write((WD * dirBin).ToString() + ",");
                                                
                        sw.WriteLine();

                        for (int i = 0; i < numMets; i++)
                        {
                            sw.Write(theseMets[i].name + ",");

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
                                sw.Write(Math.Round(theseMets[i].WS * theseMets[i].sectorWS_Ratio[WD], 3).ToString() + ",");

                            sw.WriteLine();
                        }

                        for (int i = 0; i < numTurbines; i++)
                        {                             
                            avgEst = theseTurbines[i].GetAvgWS_Est(isCalibrated, thisWakeModel);                         

                            sw.Write(theseTurbines[i].name + ",");

                            if (latsOrUTMs == 1) {
                                sw.Write(theseTurbines[i].UTMX + ",");
                                sw.Write(theseTurbines[i].UTMY + ",");
                            }
                            else {
                                UTM_conversion.Lat_Long thisLL = new UTM_conversion.Lat_Long();                                
                                thisLL = thisInst.UTM_conversions.UTMtoLL(theseTurbines[i].UTMX, theseTurbines[i].UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");
                            }

                            sw.Write(Math.Round(theseTurbines[i].elev, 2).ToString() + ",");
                            for (int WD = 0; WD <= numWD - 1; WD++)
                                sw.Write(Math.Round(avgEst.sectorWS[WD], 3).ToString() + ",");

                            sw.WriteLine();
                        }

                        // weibull Shape Factors
                        if (tabName == "Gross")
                            sw.WriteLine("Continuum 2.3: Gross Directional weibull Shape factor at Met and Turbine Sites");
                        else
                            sw.WriteLine("Continuum 2.3: Net Directional weibull Shape factor at Turbine Sites");

                        sw.Write("Site,");
                        if (latsOrUTMs == 1) {
                            sw.Write("UTMX [m],");
                            sw.Write("UTMY [m],");
                        }
                        else {
                            sw.Write("latitude [degs],");
                            sw.Write("longitude [degs],");
                        }

                        sw.Write("elev [m],");
                        for (int WD = 0; WD < numWD; WD++)
                            sw.Write((WD * dirBin).ToString() + ",");
                                                
                        sw.WriteLine();

                        for (int i = 0; i < numMets; i++)
                        {
                            sw.Write(theseMets[i].name + ",");

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
                            MetCollection.Weibull_params weibull = thisInst.metList.CalcWeibullParams(theseMets[i].WS_Dist, theseMets[i].sectorWS_Dist, theseMets[i].WS);
                            for (int WD = 0; WD < numWD; WD++)
                                sw.Write(Math.Round(weibull.sector_k[WD], 3).ToString() + ",");

                            sw.WriteLine();
                        }

                        for (int i = 0; i < numTurbines; i++)
                        {
                            avgEst = theseTurbines[i].GetAvgWS_Est(isCalibrated, thisWakeModel);                                                        
                            sw.Write(theseTurbines[i].name + ",");

                            if (latsOrUTMs == 1) {
                                sw.Write(theseTurbines[i].UTMX.ToString() + ",");
                                sw.Write(theseTurbines[i].UTMY.ToString() + ",");
                            }
                            else {
                                UTM_conversion.Lat_Long thisLL = thisInst.UTM_conversions.UTMtoLL(theseTurbines[i].UTMX, theseTurbines[i].UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");
                            }

                            sw.Write(Math.Round(theseTurbines[i].elev, 2).ToString() + ",");
                            for (int WD = 0; WD <= numWD - 1; WD++)
                                sw.Write(Math.Round(avgEst.sectWeibull_k[WD], 3).ToString() + ",");

                            sw.WriteLine();
                        }

                        // weibull Scale Factors
                        if (tabName == "Gross")
                            sw.WriteLine("Continuum 2.3: Gross Directional weibull Scale factor at Met and Turbine Sites");    
                        else
                            sw.WriteLine("Continuum 2.3: Net Directional weibull Scale factor at Turbine Sites");

                        sw.Write("Site,");
                        if (latsOrUTMs == 1) {
                            sw.Write("UTMX [m],");
                            sw.Write("UTMY [m],");
                        }
                        else {
                            sw.Write("latitude [degs],");
                            sw.Write("longitude [degs],");
                        }

                        sw.Write("elev [m],");
                        for (int WD = 0; WD < numWD; WD++)
                            sw.Write((WD * dirBin).ToString() + ",");
                                                
                        sw.WriteLine();

                        for (int i = 0; i < numMets; i++)
                        {
                            sw.Write(theseMets[i].name + ",");

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
                            MetCollection.Weibull_params weibull = thisInst.metList.CalcWeibullParams(theseMets[i].WS_Dist, theseMets[i].sectorWS_Dist, theseMets[i].WS);
                            for (int WD = 0; WD <= numWD - 1; WD++)
                                sw.Write(Math.Round(weibull.sector_A[WD], 3).ToString() + ",");

                            sw.WriteLine();
                        }

                        for (int i = 0; i <= numTurbines - 1; i++)
                        {
                            avgEst = theseTurbines[i].GetAvgWS_Est(isCalibrated, thisWakeModel);

                            sw.Write(theseTurbines[i].name + ",");
                            if (latsOrUTMs == 1) {
                                sw.Write(theseTurbines[i].UTMX.ToString() + ",");
                                sw.Write(theseTurbines[i].UTMY.ToString() + ",");
                            }
                            else {
                                UTM_conversion.Lat_Long thisLL;                                
                                thisLL = thisInst.UTM_conversions.UTMtoLL(theseTurbines[i].UTMX, theseTurbines[i].UTMY);
                                sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                                sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");
                            }

                            sw.Write(Math.Round(theseTurbines[i].elev, 2).ToString() + ",");
                            for (int WD = 0; WD <= numWD - 1; WD++)
                                sw.Write(Math.Round(avgEst.sectWeibull_A[WD], 3).ToString() + ",");

                            sw.WriteLine();
                        }

                        sw.Close();
                    }
                    catch {
                        MessageBox.Show("Error writing to file.", "Continuum 2.3");
                    }
                }
            }
            else 
                MessageBox.Show("Need to do Turbine calculations first.",  "Continuum 2.3");           
            
        }

        public void Export_WS_AEP_Uncert(Continuum thisInst, string powerCurve)
        {
            // Exports P50/P90/P99 wind speed and energy production estimates at turbine sites
            int numTurbines = thisInst.turbineList.TurbineCount;
            if (numTurbines == 0) {
                MessageBox.Show("No Turbines have been loaded yet.  Go to Input tab to import turbine sites.", "Continuum 2.3");
                return;
            }

            bool isCalibrated = thisInst.GetSelectedModel("Uncertainty");

            string modelStr = "";
            
            if (isCalibrated == false)
                modelStr = "Default Model";           
            else 
                modelStr = "Site-Calibrated Model";                
            
            Export_Degs_or_UTM thisExport = new Export_Degs_or_UTM();
            thisExport.cbo_Lats_UTMs.SelectedIndex = 0;
            thisExport.ShowDialog();
            int latsOrUTMs = thisExport.cbo_Lats_UTMs.SelectedIndex; // 0 = Lats/Longs 1 = UTM coords

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                try {
                    StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);                    
                    sw.WriteLine("Continuum 2.3: Estimated P50 P90 & P99 WS and AEP at Turbine Sites");
                    sw.WriteLine(modelStr);                                        
                    bool haveAEP;

                    if (latsOrUTMs == 1)
                        sw.WriteLine("UTM Zone: " + thisInst.UTM_conversions.UTMZoneNumber + " " + thisInst.UTM_conversions.hemisphere + " hemisphere");

                    if (powerCurve == "No power Curves Imported") {
                        
                        haveAEP = false;
                    }
                    else {                        
                        sw.WriteLine("Using power Curve: " + powerCurve);
                        
                        
                        
                        haveAEP = true;
                    }

                    sw.Write("Site,");
                    if (latsOrUTMs == 0) 
                        sw.Write("latitude, longitude,");                    
                    else {
                        sw.Write("UTMX, UTMY,");
                    }

                    sw.Write("Elev [m], WS [m/s], WS Uncert [%], WS P90 [m/s], WS P99 [m/s],");

                    if (haveAEP)
                    {
                        sw.Write("AEP [MWh], AEP P90 [MWh], AEP P99 [MWh]");
                    }                   
                                      
                    sw.WriteLine();

                    for (int i = 0; i < numTurbines; i++)
                    {
                        Turbine thisTurb = thisInst.turbineList.turbineEsts[i];
                        Turbine.Avg_Est avgEst = thisTurb.GetAvgWS_Est(isCalibrated, null);

                        sw.Write(thisTurb.name + ",");

                        if (latsOrUTMs == 1) {
                            sw.Write(thisTurb.UTMX.ToString() + ",");
                            sw.Write(thisTurb.UTMY.ToString() + ",");
                        }
                        else {
                            if (thisInst.UTM_conversions.savedDatumIndex == 100) {
                                UTM_datum thisDatum = new UTM_datum();
                                thisDatum.cbo_Datums.SelectedIndex = 0;
                                thisDatum.ShowDialog();
                                thisInst.UTM_conversions.savedDatumIndex = thisDatum.cbo_Datums.SelectedIndex;
                            }

                            if (thisInst.UTM_conversions.UTMZoneNumber == -999) {
                                Select_UTM_zone This_Zone = new Select_UTM_zone();
                                This_Zone.cboHemisphere.SelectedIndex = 0;
                                This_Zone.ShowDialog();
                                thisInst.UTM_conversions.UTMZoneNumber = Convert.ToInt16(This_Zone.cbo_UTMNumber.SelectedItem.ToString());
                                thisInst.UTM_conversions.hemisphere = This_Zone.cboHemisphere.SelectedItem.ToString();
                            }
                            UTM_conversion.Lat_Long thisLL = thisInst.UTM_conversions.UTMtoLL(thisTurb.UTMX, thisTurb.UTMY);
                            sw.Write(Math.Round(thisLL.latitude, 5).ToString() + ",");
                            sw.Write(Math.Round(thisLL.longitude, 5).ToString() + ",");

                        }
                        sw.Write(Math.Round(thisTurb.elev, 2).ToString() + ",");
                        sw.Write(Math.Round(avgEst.WS, 3).ToString() + ",");

                        double thisP90 = avgEst.WS - avgEst.WS * avgEst.uncert * 1.28155f;
                        double thisP99 = avgEst.WS - avgEst.WS * avgEst.uncert * 2.326f;

                        sw.Write(Math.Round(avgEst.uncert, 5).ToString("P") + ",");
                        sw.Write(Math.Round(thisP90, 3).ToString() + ",");
                        sw.Write(Math.Round(thisP99, 3).ToString() + ",");

                        if (haveAEP == true) {
                            for (int j = 0; j <= thisTurb.GrossAEP_Count - 1; j++) {
                                if (thisTurb.grossAEP[j].powerCurve.name == powerCurve && thisTurb.grossAEP[j].isCalibrated == isCalibrated) {                                    
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
                catch {
                    MessageBox.Show("Error writing to file.", "Continuum 2.3");                    
                }
            }
        }

        

        public void ExportNetWS_Dists(Continuum thisInst)
        {
            // Exports overall net (i.e. waked) wind speed distributions estimated at met and turbine sites
            int numTurbines = thisInst.turbineList.TurbineCount;            
            int numTurbinesToExport = thisInst.chkTurbNet.CheckedItems.Count;

            if (thisInst.metList.ThisCount == 0)
                return;

            int numWS = thisInst.metList.metItem[0].WS_Dist.Length;
            double WS_first = thisInst.metList.metItem[0].WS_FirstInt;
            double WS_int = thisInst.metList.metItem[0].WS_IntSize;
            int numWD = thisInst.GetNumWD();
            int WD_Ind = thisInst.GetWD_ind("Net");
            Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();

            double[,] turbineWS = new double[numTurbinesToExport, numWS];
            bool isCalibrated = thisInst.GetSelectedModel("Net");

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK) {
                                
                Turbine thisTurb = null;
                StreamWriter sw = null;

                try {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                }
                catch {
                    MessageBox.Show("Error writing to file.");
                    sw.Close();
                    return;
                }

                sw.WriteLine("Continuum 2.3: Waked Wind Speed Distributions at Turbine Sites");
                if (WD_Ind == numWD)
                    sw.WriteLine("Overall WS Distributions");
                else
                    sw.WriteLine("WS Distribution for WD sector: " + Math.Round(WD_Ind * (double)360 / numWD, 1).ToString() + " degs,");

                sw.WriteLine();

                sw.Write("WS [m/s],");
             
                for (int i = 0; i < thisInst.chkTurbNet.CheckedItems.Count; i++)
                    sw.Write(thisInst.chkTurbNet.CheckedItems[i].ToString() + ",");                                   

                sw.WriteLine();

                for (int i = 0; i < numTurbinesToExport; i++) { 
                    for (int j = 0; j < numTurbines; j++) { 
                        if (thisInst.turbineList.turbineEsts[j].name == thisInst.chkTurbNet.CheckedItems[i].ToString()) {
                            thisTurb = thisInst.turbineList.turbineEsts[j];
                            break;
                        }
                    }

                    Turbine.Avg_Est avgWSEst = thisTurb.GetAvgWS_Est(isCalibrated, thisWakeModel);

                    for (int j = 0; j < numWS; j++) {
                        turbineWS[i, j] = avgWSEst.WS_Dist[j];
                    }
                }

                for (int i = 0; i < numWS; i++)
                {
                    double WS_Val = WS_first + i * WS_int - WS_int / 2;
                    sw.Write(Math.Round(WS_Val,1).ToString() + ",");
                    
                    for (int j = 0; j <= numTurbinesToExport - 1; j++)                        
                        sw.Write(Math.Round(turbineWS[j, i],3).ToString() + ",");                       
                    
                    sw.WriteLine();
                }

                sw.Close();
            }
        }

        public void ExportGrossWS_Dists(Continuum thisInst)
        {
            // Exports overall gross wind speed distributions estimated at met and turbine sites            
            Met[] theseMets = thisInst.GetCheckedMets("Gross");
            Turbine[] theseTurbines = thisInst.GetCheckedTurbs("Gross");
            int numTurbines = theseTurbines.Count();
            int numMets = theseMets.Count();

            if (numMets + numTurbines == 0)
                return;

            bool isCalibrated = thisInst.GetSelectedModel("Gross");           

            if (thisInst.metList.ThisCount == 0)
                return;
            
            int numWS = thisInst.metList.metItem[0].WS_Dist.Length;
            double WS_first = thisInst.metList.metItem[0].WS_FirstInt;
            double WS_int = thisInst.metList.metItem[0].WS_IntSize;

           // double[,] Met_WS = new double[numMets, numWS];
          //  double[,] turbineWS = new double[numTurbines, numWS];                       

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {                
                StreamWriter sw = null;

                try {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                }
                catch {
                    MessageBox.Show("Error writing to file.");
                    return;
                }

                sw.WriteLine("Continuum 2.3: Gross Wind Speed Distributions at Met and Turbine Sites");
                sw.WriteLine();

                sw.Write("WS [m/s],");                

                for (int i = 0; i < numMets; i++) 
                    sw.Write(theseMets[i].name + ",");                  
                
                for (int i = 0; i < numTurbines; i++)
                    sw.Write(theseTurbines[i].name + ",");
                    
                sw.WriteLine();                               
                                                             

                for (int i = 0; i <= numWS - 1; i++) {                    
                    sw.Write(WS_first + i * WS_int - WS_int / 2 + ",");
                    
                    for (int j = 0; j <= numMets - 1; j++)                         
                        sw.Write(theseMets[j].WS_Dist[i] + ",");                     
                    
                    for (int j = 0; j <= numTurbines - 1; j++)
                    {
                        Turbine.Avg_Est avgEst = theseTurbines[j].GetAvgWS_Est(isCalibrated, null);
                        sw.Write(avgEst.WS_Dist[i] + ",");
                    }                                              
                    
                    sw.WriteLine();
                }

                sw.Close();
            }
        }

        public void ExportDirectionalWS_Dists(Continuum thisInst)
        {
            // Exports sectorwise gross wind speed distributions estimated at met and turbine sites
            
            Met[] theseMets = thisInst.GetCheckedMets("Gross");
            Turbine[] theseTurbines = thisInst.GetCheckedTurbs("Gross");

            int numTurbines = theseTurbines.Count();
            int numMets = theseMets.Count();

            if (numTurbines + numMets == 0) 
                return;

            int numWD = thisInst.GetNumWD();
            double dirBin = (double)360 / numWD;   
            bool isCalibrated = thisInst.GetSelectedModel("Gross");

            if (thisInst.metList.ThisCount == 0)
                return;

            int numWS = thisInst.metList.metItem[0].WS_Dist.Length;
            double WS_first = thisInst.metList.metItem[0].WS_FirstInt;
            double WS_int = thisInst.metList.metItem[0].WS_IntSize;            

            Export_Degs_or_UTM thisExport = new Export_Degs_or_UTM();
            thisExport.cbo_Lats_UTMs.SelectedIndex = 0;
            thisExport.ShowDialog();
            int latsOrUTMs = thisExport.cbo_Lats_UTMs.SelectedIndex; // 0 = Lats/Longs 1 = UTM coords

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {                                
                StreamWriter sw = null;

                try {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                }
                catch {
                    MessageBox.Show("Error writing to file.");
                    sw.Close();
                    return;
                }
                                
                sw.WriteLine("Continuum 2.3: Gross Directional Wind Speed Distributions at mets and Estimated at Turbine Sites");
                

                for (int i = 0; i < numMets; i++)
                {                   
                    sw.WriteLine(theseMets[i].name);
                    sw.Write("WS [m/s],");
                    for (int WD = 0; WD < numWD; WD++)
                        sw.Write(WD * (double)360 / numWD + ",");

                    sw.WriteLine();

                    for (int WS_ind = 0; WS_ind < numWS; WS_ind++) {
                        
                        sw.Write((Math.Round(WS_first + WS_ind * WS_int - WS_int / 2, 1)).ToString() + ",");
                        for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)                             
                            sw.Write(Math.Round(theseMets[i].sectorWS_Dist[WD_Ind, WS_ind], 2) + ",");
                        
                        sw.WriteLine();
                    }

                    sw.WriteLine();
                }

                for (int i = 0; i <= numTurbines - 1; i++) {                    
                    sw.WriteLine(theseTurbines[i].name);

                    sw.Write("WS [m/s],");
                    for (int WD = 0; WD <= numWD - 1; WD++)
                        sw.Write(WD * (double)360 / numWD + ",");
                    
                    sw.WriteLine();

                    Turbine.Avg_Est avgEst = theseTurbines[i].GetAvgWS_Est(isCalibrated, null);

                    for (int WS_ind = 0; WS_ind <= numWS - 1; WS_ind++) {                        
                        sw.Write(Math.Round(WS_first + WS_ind * WS_int - WS_int / 2, 1) + ",");
                        for (int WD_Ind = 0; WD_Ind <= numWD - 1; WD_Ind++)                            
                            sw.Write(Math.Round(avgEst.sectorWS_Dist[WD_Ind, WS_ind], 2) + ",");
                        
                        sw.WriteLine();
                    }
                    sw.WriteLine();
                }

                sw.Close();

            }
        }

        public void ExportNetDirectionalWS_Dists(Continuum thisInst)
        {
            // Exports sectorwise net wind speed distributions estimated at met and turbine sites
            int numTurbines = thisInst.turbineList.TurbineCount;

            int numWD = thisInst.GetNumWD();
            if (numWD == 0) return;
                                    
            int numWS = thisInst.metList.metItem[0].WS_Dist.Length;
            double WS_first = thisInst.metList.metItem[0].WS_FirstInt;
            double WS_int = thisInst.metList.metItem[0].WS_IntSize;
                        
            Turbine[] turbsToExport = thisInst.GetCheckedTurbs("Net");
            int numTurbinesToExport = turbsToExport.Length;
            double[,] turbineWS = new double[numTurbinesToExport, numWS];

            bool isCalibrated = thisInst.GetSelectedModel("Net");
            Wake_Model thisWakeModel = thisInst.GetSelectedWakeModel();

            if (thisWakeModel == null)
                return;

            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK) {
                                
                StreamWriter sw = null;

                try {
                    sw = new StreamWriter(thisInst.sfd60mWS.FileName);
                }
                catch {
                    MessageBox.Show("Error writing to file.");
                    sw.Close();
                    return;
                }
                                
                sw.WriteLine("Continuum 2.3: Directional Wind Speed Distributions Estimated at Turbine Sites");
                

                for (int i = 0; i < numTurbinesToExport; i++) {
                    
                    sw.WriteLine(turbsToExport[i].name);

                    sw.Write("WS [m/s],");
                    for (int WD = 0; WD <= numWD - 1; WD++)
                        sw.Write(Math.Round(WD * (double)360 / numWD, 1) + ",");
                    
                    sw.WriteLine();

                    Turbine.Avg_Est avgEst = turbsToExport[i].GetAvgWS_Est(isCalibrated, thisWakeModel);

                    for (int WS_ind = 0; WS_ind < numWS; WS_ind++)
                    {                        
                        sw.Write(Math.Round(WS_first + WS_ind * WS_int - WS_int / 2, 1) + ",");

                        for (int WD_Ind = 0; WD_Ind < numWD; WD_Ind++)
                            sw.Write(Math.Round(avgEst.sectorWS_Dist[WD_Ind, WS_ind], 2) + ",");
                            
                        sw.WriteLine();
                    }
                    sw.WriteLine();
                }

                sw.Close();

            }
        }
        
        public void ExportExpos(Continuum thisInst, string[] radii, string[] mets, string[] turbs, bool sectorOut, bool bulkOut, int numSectors, bool gotSR, bool Sector_SRDH_out, bool Bulk_SRDH_out)
        {
            // Exports exposure and SRDH (sectorwise and overall) at met and turbine sites
            
            DateTime Date_String = DateTime.Now;
                 
            Met thisMet = new Met();
            Turbine thisTurb = new Turbine();

            int numWD = thisInst.GetNumWD();

            if (numWD  == 1) {
                MessageBox.Show("You need to import met files first.", "Continuum 2.3");
                return;
            }                                  

            try {
                if (thisInst.sfdExpos.ShowDialog() == DialogResult.OK) {

                    StreamWriter sw = new StreamWriter(thisInst.sfdExpos.FileName);                    
                    sw.WriteLine("Continuum 2.3: Calculated Exposures and Elevations");
                    sw.WriteLine(Date_String);

                    if (thisInst.savedParams.savedFileName != "")
                        sw.WriteLine(thisInst.savedParams.savedFileName);

                    try {
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
                                sw.WriteLine("Surface roughness [m]");
                                if (mets != null) WriteExpoSRDH(sw, Bulk_SRDH_out, Sector_SRDH_out, "SR", mets, null, numWD, rad_ind, thisInst);
                                if (turbs != null) WriteExpoSRDH(sw, Bulk_SRDH_out, Sector_SRDH_out, "SR", null, turbs, numWD, rad_ind, thisInst);

                                sw.WriteLine("Displacement height [m]");
                                if (mets != null) WriteExpoSRDH(sw, Bulk_SRDH_out, Sector_SRDH_out, "DH", mets, null, numWD, rad_ind, thisInst);
                                if (turbs != null) WriteExpoSRDH(sw, Bulk_SRDH_out, Sector_SRDH_out, "DH", null, turbs, numWD, rad_ind, thisInst);
                            }
                        }
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.ToString());
                        MessageBox.Show("Error writing to file.", "Continuum 2.3");
                        sw.Close();
                    }

                    sw.Close();
                }
                
            }
            catch {
                MessageBox.Show("Error opening the file. Make sure that it's not open in another program.", "Continuum 2.3");
                return;
            }

        }

        public void WriteExpoSRDH(StreamWriter sw, bool bulkOut, bool sectorOut, string Expo_or_SR_or_DH, string[] mets, string[] turbs, int numWD, int rad_ind, Continuum thisInst)
        {
            double dirBin = (double)360 / numWD;
            string header = "";

            if (Expo_or_SR_or_DH == "Expo")
                header = "Site, elev. [m],";
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
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisMet.windRose, "Expo", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisMet.windRose, "Expo", "DW"), 4).ToString() + ", ");
                        }
                        else
                        {
                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(thisTurbine.windRose, "Expo", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(thisTurbine.windRose, "Expo", "DW"), 4).ToString() + ", ");
                        }
                    }
                    else if (Expo_or_SR_or_DH == "SR")
                    {
                        if (thisMet != null)
                        {
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisMet.windRose, "SR", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisMet.windRose, "SR", "DW"), 4).ToString() + ", ");
                        }
                        else
                        {
                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(thisTurbine.windRose, "SR", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(thisTurbine.windRose, "SR", "DW"), 4).ToString() + ", ");
                        }
                    }
                    else if (Expo_or_SR_or_DH == "DH")
                    {
                        if (thisMet != null)
                        {
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisMet.windRose, "DH", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisMet.expo[rad_ind].GetOverallValue(thisMet.windRose, "DH", "DW"), 4).ToString() + ", ");
                        }
                        else
                        {
                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(thisTurbine.windRose, "DH", "UW"), 4).ToString() + ", ");
                            sw.Write(Math.Round(thisTurbine.expo[rad_ind].GetOverallValue(thisTurbine.windRose, "DH", "DW"), 4).ToString() + ", ");
                        }
                    }
                }
                sw.WriteLine();
            }
            sw.WriteLine();
        }

        public void ExportMapCSV(Continuum thisInst, string map_export, int WD_Ind, int numWD)
        {
            // Exports generated map as a .CSV            
                       
            Map thisMap = new Map();
            for (int i = 0; i <= thisInst.mapList.ThisCount - 1; i++) { 
                if (map_export == thisInst.mapList.mapItem[i].mapName) {
                    thisMap = thisInst.mapList.mapItem[i];                   
                    break;
                }
            }
                        
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                if (thisMap.modelType == 0)
                    sw.WriteLine("Continuum 2.3: Modeled Upwind Exposures");
                else if (thisMap.modelType == 1)
                    sw.WriteLine("Continuum 2.3: Modeled Downwind Exposures");
                else if (thisMap.modelType == 2 || thisMap.modelType == 4)
                {
                    if (WD_Ind == numWD)
                        sw.WriteLine("Continuum 2.3: Modeled " + thisInst.metList.metItem[0].height + " m Wind Speeds");
                    else
                        sw.WriteLine("Continuum 2.3: Modeled " + thisInst.metList.metItem[0].height + " m Wind Speeds for Wind direction =" + ((double)360 / numWD * WD_Ind).ToString() + " degrees");
                }
                else if (thisMap.modelType == 3 || thisMap.modelType == 5)
                {
                    if (thisMap.isWaked == false)
                        sw.WriteLine("Continuum 2.3: Modeled Gross AEP using power Curve: " + thisMap.powerCurve);
                    else
                        sw.WriteLine("Continuum 2.3: Modeled Waked WS using power Curve: " + thisMap.powerCurve);
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
                    sw.Write(thisInst.metList.metItem[0].height + " m WS [m/s],");
                else if (thisMap.modelType == 3 || thisMap.modelType == 5)
                {
                    if (thisMap.isWaked == false)
                        sw.Write("Gross AEP [MWh],");
                    else
                        sw.Write(thisInst.metList.metItem[0].height + " m WS [m/s],");
                }                   
                
                sw.WriteLine();

                for (int i = 0; i < thisMap.numX; i++) { 
                    for (int j = 0; j < thisMap.numY; j++) {
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

        public void ExportTopo(Continuum thisInst)
        {
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                sw.WriteLine("Exported topography data");
                sw.WriteLine("UTMX, UTMY, elev");

                BinaryFormatter bin = new BinaryFormatter();
                NodeCollection nodeList = new NodeCollection();
                string connString = nodeList.GetDB_ConnectionString(thisInst.savedParams.savedFileName);
                // Modified to output every 3rd datapoint

                try
                {
                    using (var ctx = new Continuum_EDMContainer(connString))
                    {
                        Topo_table topoDB = new Topo_table();
                        var topoDB_Query = from N in ctx.Topo_table where N.Id >= 0 select N;
                        int counter = 0;
                        TopoInfo.Min_Max_Num topoX_All = thisInst.topo.topoNumXY.X.all;
                        TopoInfo.Min_Max_Num topoY_All = thisInst.topo.topoNumXY.Y.all;

                        foreach (var N in topoDB_Query)
                        {
                          //  double ThisUTMX = (N.Id - 1
                            double UTMX = Math.Floor((double)N.Id/ topoY_All.num) * topoY_All.reso + topoX_All.min;
                            double UTMY = topoY_All.min + topoX_All.reso * N.Id - (UTMX - topoX_All.min) * topoY_All.num;
                                                        
                            if (counter == 0)
                            {                                
                              //  sw.WriteLine(Math.Round(This_UTMX, 2) + "," + Math.Round(This_UTMY, 2) + "," + N.elev);                                
                            }

                            counter++;
                            if (counter == 3) counter = 0;
                            
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
        }

   /*     public void Export_Topo_Calcs(Continuum thisInst)
        {
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                sw.WriteLine("Exported topography data");
                sw.WriteLine("UTMX, UTMY, elev");
               
                TopoInfo.Min_Max_Num Topo_X_Calc = thisInst.topo.topoNumXY.X.calcs;
                TopoInfo.Min_Max_Num Topo_Y_Calc = thisInst.topo.topoNumXY.Y.calcs;

                for(int i = 0; i < Topo_X_Calc.Num; i++)
                {
                    double This_UTMX = Math.Floor((double)N.Id / topoY_All.Num) * topoY_All.reso + topoX_All.Min;
                    double This_UTMY = topoY_All.Min + topoX_All.reso * N.Id - (This_UTMX - topoX_All.Min) * topoY_All.Num;

                    if (counter == 0)
                    {
                        sw.WriteLine(Math.Round(This_UTMX, 2) + "," + Math.Round(This_UTMY, 2) + "," + N.elev);                                
                    }

                    counter++;
                    if (counter == 3) counter = 0;

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
        }
*/
        public void Export_LC(Continuum thisInst)
        {
            if (thisInst.sfd60mWS.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(thisInst.sfd60mWS.FileName);

                sw.WriteLine("Exported land cover data");
                sw.WriteLine("UTMX, UTMY, Land Cover Code");

                for (int i = 0; i <= thisInst.topo.landCover.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= thisInst.topo.landCover.GetUpperBound(1); j++)
                    {
                        double thisX = thisInst.topo.LC_NumXY.X.all.min + i * thisInst.topo.LC_NumXY.X.all.reso;
                        double thisY = thisInst.topo.LC_NumXY.Y.all.min + j * thisInst.topo.LC_NumXY.Y.all.reso;
                        sw.WriteLine(thisX + "," + thisY + "," + thisInst.topo.landCover[i, j]);
                    }
                }
                
                sw.Close();
            }
        }
    }

}
