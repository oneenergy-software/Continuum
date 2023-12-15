using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Continuum_Tests.GUI_Tests
{
    [TestClass]
    public class WakeModel_Tests
    {
        string testFolder;
        string saveFolder;

        Globals globals = new Globals();
        
        public WakeModel_Tests()
        {
            testFolder = globals.testFolder;
            saveFolder = globals.saveFolder;
        }

        [TestMethod]
        public void WakeModelTypeAndSettings_Test()
        {
            Continuum thisInst = new Continuum("");
            string fileName = saveFolder + "OneMetTABAndGrossNet_1";
            thisInst.Open(fileName + ".cfm");
            thisInst.isTest = true;
            Wake_Model wakeModel = thisInst.wakeModelList.wakeModels[0];
            thisInst.wakeModelList.RemoveWakeModel(thisInst.turbineList, thisInst.mapList, wakeModel);
            thisInst.turbineList.ClearAllTurbines();

            string turbineFile = testFolder + "\\Turbine sites\\Findlay\\Three rows.csv"; // 12 turbine sites, 3 rows
            thisInst.LoadTurbines(turbineFile);

            // Test wake model type and settings

            Gen_WakeModel thisWake = new Gen_WakeModel(thisInst);
            double horizExp = 5;
            double ambTI = 10;
            double DW_Spacing = 0;
            double CW_Spacing = 0;
            double ambRough = 0;
            string crvName = thisInst.turbineList.powerCurves[0].name;
            TurbineCollection.PowerCurve crvObject = thisInst.turbineList.powerCurves[0];            
            double wakeRecharge = 0;

            Turbine.Avg_Est thisEst = new Turbine.Avg_Est();

            string outputFile = testFolder + "\\Calc WS.csv";
                        
            double[] wakedEsts = new double[0];
            int wakeModelInd = 0;

            for (int modelInd = 0; modelInd < 3; modelInd++)
                for (int comboInd = 0; comboInd < 5; comboInd++)
                    for (int horizInd = 0; horizInd < 2; horizInd++)                        
                    {
                        if (horizInd == 0)
                            horizExp = 5;
                        else if (horizInd == 1)
                            horizExp = 6;      

                        thisWake.cboWakeCombo.SelectedIndex = comboInd;
                        thisWake.cboWakeModel.SelectedIndex = modelInd;
                        thisWake.cboPowerCrvs.SelectedIndex = 0;
                        thisWake.txtHorizWakeExp.Text = horizExp.ToString();
                        thisWake.txtAmbTI.Text = ambTI.ToString();

                        if (modelInd == 1) // Deep-array model
                        {
                            DW_Spacing = 5;
                            CW_Spacing = 2;
                            ambRough = 0.005;
                            thisWake.txtDownSpace.Text = DW_Spacing.ToString();
                            thisWake.txtCrossSpace.Text = CW_Spacing.ToString();
                            thisWake.txtAmbRough.Text = ambRough.ToString();
                        }
                        else
                        {
                            DW_Spacing = 0;
                            CW_Spacing = 0;
                            ambRough = 0;
                        }
                        
                        thisWake.GenWakeModel();

                        while (thisInst.BW_worker.DoWorkDone == false && thisInst.BW_worker.WasReturned == false)
                            Thread.Sleep(100);                        

                        string comboName = thisWake.cboWakeCombo.SelectedItem.ToString();
                        wakeModel = thisInst.wakeModelList.GetWakeModel(modelInd, horizExp, ambTI, DW_Spacing, CW_Spacing, ambRough, crvName, comboName);
                        thisEst = thisInst.turbineList.turbineEsts[10].GetAvgWS_Est(wakeModel);
                                                
                        for (int i = 0; i < wakeModelInd; i++)                        
                            Assert.AreNotEqual(thisEst.waked.WS, wakedEsts[i], "Same estimate calculated. ModelInd:" + modelInd + ", ComboInd:" + comboInd + ", HorizInd: " + horizInd);

                        wakeModelInd++;
                        Array.Resize(ref wakedEsts, wakeModelInd);
                        wakedEsts[wakeModelInd - 1] = thisEst.waked.WS;

                        thisInst.updateThe.AllTABs();
                        thisInst.BW_worker.Close();
                        
                    }

            
            thisInst.Close();

        }
    }
}
