using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;
using System.IO;

namespace Continuum_Tests
{
    /// <summary>
    /// Summary description for ModelCollection_Tests
    /// </summary>
    [TestClass]
    public class ModelCollection_Tests
    {        
        Globals globals = new Globals();
        string testingFolder;

        public ModelCollection_Tests()
        {
            testingFolder = globals.testingFolder + "ModelCollection";
        }

        [TestMethod]
        public void GetDeltaWS_DW_Expo_Test()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.SetDefaultModelCoeffs(16);
            thisModel.radius = 4000;

            ModelCollection modelList = new ModelCollection();
            ModelCollection.Coeff_Delta_WS[] This_Delta_WS = new ModelCollection.Coeff_Delta_WS[0];

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(6, 10, 10, 20, 30, 20, 20, thisModel, 0, false, false); // Test 1
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.20205, 0.001, "Wrong delta WS in Test 1");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(6, 10, 10, 15, -10, 30, 30, thisModel, 0, false, false); // Test 2
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.23250, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.09801, 0.001, "Wrong delta WS in Test 2");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(6, 10, 10, -8, -20, 5, 5, thisModel, 0, false, false); // Test 3
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.35988, 0.001, "Wrong delta WS in Test 3");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(6, 10, 10, -4, 15, 8, 8, thisModel, 0, false, false); // Test 4
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.089461, 0.001, "Wrong delta WS in Test 4");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.551676, 0.001, "Wrong delta WS in Test 4");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(10, 50, 200, 50, 200, 220, 220, thisModel, 0, true, false); // Test 5
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.316054, 0.001, "Wrong delta WS in Test 5");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.21193, 0.001, "Wrong delta WS in Test 5");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(8, 180, 150, 130, 75, 100, 100, thisModel, 0, true, false); // Test 6
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.208015, 0.001, "Wrong delta WS in Test 6");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.07055, 0.001, "Wrong delta WS in Test 6");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(8, 75, 125, -10, 160, 110, 110, thisModel, 0, true, false); // Test 7
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.043555, 0.001, "Wrong delta WS in Test 7");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.994433, 0.001, "Wrong delta WS in Test 7");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.04355, 0.001, "Wrong delta WS in Test 7");

            This_Delta_WS = modelList.Get_DeltaWS_DW_Expo(8, 160, 140, 200, -5, 160, 160, thisModel, 0, true, false); // Test 8
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.344724, 0.001, "Wrong delta WS in Test 8");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.518931, 0.001, "Wrong delta WS in Test 8");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.01723, 0.001, "Wrong delta WS in Test 8");

        }

        [TestMethod]
        public void GetDeltaWS_UW_Expo()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.SetDefaultModelCoeffs(16);
            thisModel.radius = 4000;

            ModelCollection modelList = new ModelCollection();
            ModelCollection.Coeff_Delta_WS[] This_Delta_WS = new ModelCollection.Coeff_Delta_WS[0];

            NodeCollection.Sep_Nodes[] Sep_Node_1 = new NodeCollection.Sep_Nodes[1];
            NodeCollection.Sep_Nodes[] Sep_Node_2 = new NodeCollection.Sep_Nodes[1];

            NodeCollection.Node_UTMs Node_1 = new NodeCollection.Node_UTMs();
            Node_1.UTMX = 280560;
            Node_1.UTMY = 4558410;
            NodeCollection.Node_UTMs Node_2 = new NodeCollection.Node_UTMs();
            Node_2.UTMX = 280860;
            Node_2.UTMY = 4552410;

            // Test 1: Scenario 1, Site 1 and Site 2 do not have turb_end_nodes (i.e. Both sites inside turbulent zone)
            Sep_Node_1[0].highNode = new Nodes();
            Sep_Node_1[0].highNode.UTMX = 280610;
            Sep_Node_1[0].highNode.UTMY = 4558900;
            Sep_Node_1[0].highNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            Sep_Node_1[0].highNode.expo[0].expo[8] = 200;
            Sep_Node_2[0].highNode = new Nodes();
            Sep_Node_2[0].highNode.UTMX = 279600;
            Sep_Node_2[0].highNode.UTMY = 4550266;
            Sep_Node_2[0].highNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_2[0].highNode.expo[0].expo[0] = 150;
            Sep_Node_2[0].highNode.expo[0].expo[8] = 150;

            Sep_Node_1[0].turbEndNode = null;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-10, 0, 20, 30, 80, 80, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725, 0.001, "Wrong delta WS in Test 1");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.265671, 0.001, "Wrong delta WS in Test 1");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.31577, 0.001, "Wrong delta WS in Test 1");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -2.39337, 0.001, "Wrong delta WS in Test 1");

            // Test 2: Scenario 1, Site 1 and Site 2 both have turb_end_nodes (i.e. Both sites outside turbulent zone)
            Sep_Node_1[0].turbEndNode = new Nodes();
            Sep_Node_2[0].turbEndNode = new Nodes();
            Sep_Node_1[0].turbEndNode.UTMX = 280000;
            Sep_Node_1[0].turbEndNode.UTMY = 4558000;
            Sep_Node_1[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_1[0].turbEndNode.expo[0].expo[0] = -2;
            Sep_Node_1[0].turbEndNode.expo[0].expo[8] = 25;
            Sep_Node_2[0].turbEndNode.UTMX = 279800;
            Sep_Node_2[0].turbEndNode.UTMY = 4551500;
            Sep_Node_2[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_2[0].turbEndNode.expo[0].expo[0] = -14;
            Sep_Node_2[0].turbEndNode.expo[0].expo[8] = 30;

            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-15, -2, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.091724715, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.231128716, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -0.084668968, 0.001, "Wrong delta WS in Test 2");
            Assert.AreEqual(This_Delta_WS[5].deltaWS_Expo, -0.162409197, 0.001, "Wrong delta WS in Test 2");

            // Test 3: Scenario 1, Site 1 has turb_end_node and Site 2 does not (i.e. Site 1 outside turbulent zone, Sie 2 inside turbulent zone)
            Sep_Node_2[0].turbEndNode = null;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-9, -4, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.049390231, 0.001, "Wrong delta WS in Test 3");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 3");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.231128716, 0.001, "Wrong delta WS in Test 3");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 3");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -1.68437925, 0.001, "Wrong delta WS in Test 3");

            // Test 4: Scenario 1, Site 1 does not has turb_end_node and Site 2 does (i.e. Site 1 inside turbulent zone, Sie 2 outside turbulent zone)
            Sep_Node_1[0].turbEndNode = null;
            Sep_Node_2[0].turbEndNode = new Nodes();
            Sep_Node_2[0].turbEndNode.UTMX = 279800;
            Sep_Node_2[0].turbEndNode.UTMY = 4551500;
            Sep_Node_2[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_2[0].turbEndNode.expo[0].expo[0] = -14;
            Sep_Node_2[0].turbEndNode.expo[0].expo[8] = 30;

            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-15, -2, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 4");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.231128716, 0.001, "Wrong delta WS in Test 4");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 4");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.084668968, 0.001, "Wrong delta WS in Test 4");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -0.863617561, 0.001, "Wrong delta WS in Test 4");

            // Test 5: Scenario 2, flow = Downhill
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-30, -46, 40, 60, 10, 10, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.50858436, 0.001, "Wrong delta WS in Test 5");

            // Test 6: Scenario 2, flow = Uphill
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(80, 82, 40, 60, 60, 60, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.012717211, 0.001, "Wrong delta WS in Test 6");

            // Test 7: Scenario 2, flow = Speed-Up
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(2, 10, 40, 60, 60, 60, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.011474371, 0.001, "Wrong delta WS in Test 7");

            // Test 8: Scenario 3, Flow 1 = Downhill; Flow 2 = Uphill
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-30, 82, 40, 60, 55, 55, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.312888198, 0.001, "Wrong delta WS in Test 8");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.03172509, 0.001, "Wrong delta WS in Test 8");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.409523988, 0.001, "Wrong delta WS in Test 8");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.690687097, 0.001, "Wrong delta WS in Test 8");

            // Test 9: Scenario 4, Flow 1 = Downhill; Flow 2 = Speed-Up
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-22, 18, 40, 60, 15, 15, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.536481614, 0.001, "Wrong delta WS in Test 9");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.059033754, 0.001, "Wrong delta WS in Test 9");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.47744786, 0.001, "Wrong delta WS in Test 9");

            // Test 10: Scenario 6, Flow 1 = Downhill, Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is outside turbulent zone)
            Sep_Node_1 = null;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 120;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-15, -2, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.105836209, 0.001, "Wrong delta WS in Test 10");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.022207682, 0.001, "Wrong delta WS in Test 10");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.457634857, 0.001, "Wrong delta WS in Test 10");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 10");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -0.084668968, 0.001, "Wrong delta WS in Test 10");
            Assert.AreEqual(This_Delta_WS[5].deltaWS_Expo, -2.292735546, 0.001, "Wrong delta WS in Test 10");

            // Test 11: Scenario 6, Flow 1 = Downhill, Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is outside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 20;
            Sep_Node_2[0].highNode.expo[0].expo[8] = 250;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-9, -11, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.063501726, 0.001, "Wrong delta WS in Test 11");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.021150174, 0.001, "Wrong delta WS in Test 11");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 11");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.021167242, 0.001, "Wrong delta WS in Test 11");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -1.730321988, 0.001, "Wrong delta WS in Test 11");

            // Test 12: Scenario 6, Flow 1 = Downhill, Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is inside turbulent zone)
            Sep_Node_2[0].turbEndNode = null;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 120;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-9, -11, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.063501726, 0.001, "Wrong delta WS in Test 12");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.022207682, 0.001, "Wrong delta WS in Test 12");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.457634857, 0.001, "Wrong delta WS in Test 12");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 12");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -3.814705598, 0.001, "Wrong delta WS in Test 12");

            // Test 13: Scenario 6, Flow 1 = Downhill, Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is inside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 20;
            Sep_Node_2[0].highNode.expo[0].expo[8] = 250;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(-9, -11, 40, 60, 100, 100, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.063501726, 0.001, "Wrong delta WS in Test 13");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.021150174, 0.001, "Wrong delta WS in Test 13");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 13");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -3.35812825, 0.001, "Wrong delta WS in Test 13");

            // Test 14: Scenario 7, Flow 1 = Uphill, Flow 2 = Downhill 
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(40, -11, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.18621661, 0.001, "Wrong delta WS in Test 14");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.045546258, 0.001, "Wrong delta WS in Test 14");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.170506735, 0.001, "Wrong delta WS in Test 14");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 0.311177087, 0.001, "Wrong delta WS in Test 14");

            // Test 15: Scenario 8, Flow 1 = Uphill, Flow 2 = SpdUp
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(40, 15, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.18621661, 0.001, "Wrong delta WS in Test 15");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.013013216, 0.001, "Wrong delta WS in Test 15");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.173203393, 0.001, "Wrong delta WS in Test 15");

            // Test 16: Scenario 8, Flow 1 = Uphill, Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is outside turbulent zone)
            Sep_Node_2[0].turbEndNode = new Nodes();
            Sep_Node_2[0].turbEndNode.UTMX = 279800;
            Sep_Node_2[0].turbEndNode.UTMY = 4551500;
            Sep_Node_2[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_2[0].turbEndNode.expo[0].expo[0] = -14;
            Sep_Node_2[0].turbEndNode.expo[0].expo[8] = 30;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 120;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(30, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.882078678, 0.001, "Wrong delta WS in Test 16");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 16");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.186007348, 0.001, "Wrong delta WS in Test 16");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -2.734889219, 0.001, "Wrong delta WS in Test 16");

            // Test 17: Scenario 8, Flow 1 = Uphill, Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is outside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 19;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(30, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.088207868, 0.001, "Wrong delta WS in Test 17");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.004337739, 0.001, "Wrong delta WS in Test 17");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 17");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.186007348, 0.001, "Wrong delta WS in Test 17");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -1.768940413, 0.001, "Wrong delta WS in Test 17");

            // Test 18: Scenario 8, Flow 1 = Uphill, Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is inside turbulent zone)
            Sep_Node_2[0].turbEndNode = null;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 50;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(30, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.196017484, 0.001, "Wrong delta WS in Test 18");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 18");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.511794182, 0.001, "Wrong delta WS in Test 18");

            // Test 19: Scenario 8, Flow 1 = Uphill, Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is inside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 15;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(30, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.088207868, 0.001, "Wrong delta WS in Test 19");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.013013216, 0.001, "Wrong delta WS in Test 19");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 19");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -3.240582047, 0.001, "Wrong delta WS in Test 19");

            // Test 20: Flow 1 = Speed-Up; Flow 2 = Uphill
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, 30, 40, 60, 25, 25, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.026599019, 0.001, "Wrong delta WS in Test 20");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.098839891, 0.001, "Wrong delta WS in Test 20");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.072240872, 0.001, "Wrong delta WS in Test 20");

            // Test 21: Flow 1 = Speed-Up; Flow 2 = Downhill
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, -10, 40, 60, 25, 25, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, false, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, -0.024180926, 0.001, "Wrong delta WS in Test 20");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.174626293, 0.001, "Wrong delta WS in Test 20");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.150445366, 0.001, "Wrong delta WS in Test 20");

            // Test 22: Flow 1 = Speed-Up; Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is outside turbulent zone)
            Sep_Node_2[0].turbEndNode = new Nodes();
            Sep_Node_2[0].turbEndNode.UTMX = 279800;
            Sep_Node_2[0].turbEndNode.UTMY = 4551500;
            Sep_Node_2[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_2[0].turbEndNode.expo[0].expo[0] = -14;
            Sep_Node_2[0].turbEndNode.expo[0].expo[8] = 30;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 120;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.023857564, 0.001, "Wrong delta WS in Test 22");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.970286545, 0.001, "Wrong delta WS in Test 22");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 22");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.186007348, 0.001, "Wrong delta WS in Test 22");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, -2.799239524, 0.001, "Wrong delta WS in Test 22");

            // Test 23: Flow 1 = Speed-Up; Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is outside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 18;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.017350955, 0.001, "Wrong delta WS in Test 23");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -1.666803194, 0.001, "Wrong delta WS in Test 23");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.186007348, 0.001, "Wrong delta WS in Test 23");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -1.835459587, 0.001, "Wrong delta WS in Test 23");

            // Test 24: Flow 1 = Speed-Up; Flow 2 = Turbulent (Sep Pt 2 is Uphill, Site 2 is inside turbulent zone)
            Sep_Node_2[0].turbEndNode = null;
            Sep_Node_2[0].highNode.expo[0].expo[0] = 120;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.023857564, 0.001, "Wrong delta WS in Test 24");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.970286545, 0.001, "Wrong delta WS in Test 24");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 24");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -4.26220568, 0.001, "Wrong delta WS in Test 24");

            // Test 25: Flow 1 = Speed-Up; Flow 2 = Turbulent (Sep Pt 2 is SpdUp, Site 2 is inside turbulent zone)
            Sep_Node_2[0].highNode.expo[0].expo[0] = 18;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(10, -2, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.017350955, 0.001, "Wrong delta WS in Test 25");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -3.315776698, 0.001, "Wrong delta WS in Test 25");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -3.298425743, 0.001, "Wrong delta WS in Test 25");

            // Test 26: Flow 1 = Turbulent; Flow 2 = Uphill (Sep Pt 1 is Uphill, Site 1 is outside turbulent zone)
            Sep_Node_1 = Sep_Node_2;
            Sep_Node_2 = null;
            Sep_Node_1[0].turbEndNode = new Nodes();
            Sep_Node_1[0].turbEndNode.UTMX = 280000;
            Sep_Node_1[0].turbEndNode.UTMY = 4558000;
            Sep_Node_1[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_1[0].turbEndNode.expo[0].expo[0] = -2;
            Sep_Node_1[0].highNode.UTMX = 280610;
            Sep_Node_1[0].highNode.UTMY = 4558900;
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 110, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 26");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 26");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.882078678, 0.001, "Wrong delta WS in Test 26");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 2.362738866, 0.001, "Wrong delta WS in Test 26");

            // Test 27: Flow 1 = Turbulent; Flow 2 = Uphill (Sep Pt 1 is SpdUp, Site 1 is outside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 110, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 27");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 27");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.002168869, 0.001, "Wrong delta WS in Test 27");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.872277803, 0.001, "Wrong delta WS in Test 27");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, 0.610551254, 0.001, "Wrong delta WS in Test 27");

            // Test 28: Flow 1 = Turbulent; Flow 2 = Uphill (Sep Pt 1 is Uphill, Site 1 is inside turbulent zone)
            Sep_Node_1[0].turbEndNode = null;
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 110, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 28");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.882078678, 0.001, "Wrong delta WS in Test 28");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 1.538804563, 0.001, "Wrong delta WS in Test 28");

            // Test 29: Flow 1 = Turbulent; Flow 2 = Uphill (Sep Pt 1 is SpdUp, Site 1 is inside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 110, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 29");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 0.002168869, 0.001, "Wrong delta WS in Test 29");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.872277803, 0.001, "Wrong delta WS in Test 29");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.213383048, 0.001, "Wrong delta WS in Test 29");

            // Test 30: Flow 1 = Turbulent; Flow 2 = Downhill (Sep Pt 1 is Uphill, Site 1 is outside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            Sep_Node_1[0].turbEndNode = new Nodes();
            Sep_Node_1[0].turbEndNode.UTMX = 280000;
            Sep_Node_1[0].turbEndNode.UTMY = 4558000;
            Sep_Node_1[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_1[0].turbEndNode.expo[0].expo[0] = -2;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, -20, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 30");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 30");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 1.754356481, 0.001, "Wrong delta WS in Test 30");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.045546258, 0.001, "Wrong delta WS in Test 30");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, 0.310012246, 0.001, "Wrong delta WS in Test 30");
            Assert.AreEqual(This_Delta_WS[5].deltaWS_Expo, 3.499482657, 0.001, "Wrong delta WS in Test 30");

            // Test 31: Flow 1 = Turbulent; Flow 2 = Downhill (Sep Pt 1 is SpdUp, Site 1 is outside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, -20, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 31");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 31");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.043377388, 0.001, "Wrong delta WS in Test 31");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 0.310012246, 0.001, "Wrong delta WS in Test 31");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, 1.747295046, 0.001, "Wrong delta WS in Test 31");

            // Test 32: Flow 1 = Turbulent; Flow 2 = Downhill (Sep Pt 1 is Uphill, Site 1 is inside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            Sep_Node_1[0].turbEndNode = null;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, -20, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 32");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.754356481, 0.001, "Wrong delta WS in Test 32");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.045546258, 0.001, "Wrong delta WS in Test 32");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 0.310012246, 0.001, "Wrong delta WS in Test 32");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, 2.675548355, 0.001, "Wrong delta WS in Test 32");

            // Test 33: Flow 1 = Turbulent; Flow 2 = Downhill (Sep Pt 1 is SpdUp, Site 1 is inside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, -20, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 33");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.043377388, 0.001, "Wrong delta WS in Test 33");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.310012246, 0.001, "Wrong delta WS in Test 33");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 0.923360743, 0.001, "Wrong delta WS in Test 33");

            // Test 34: Flow 1 = Turbulent; Flow 2 = Speed-Up (Sep Pt 1 is Uphill, Site 1 is outside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            Sep_Node_1[0].turbEndNode = new Nodes();
            Sep_Node_1[0].turbEndNode.UTMX = 280000;
            Sep_Node_1[0].turbEndNode.UTMY = 4558000;
            Sep_Node_1[0].turbEndNode.AddExposure(4000, 1, 1, 16);
            Sep_Node_1[0].turbEndNode.expo[0].expo[0] = -2;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 10, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 34");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 34");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 1.754356481, 0.001, "Wrong delta WS in Test 34");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, -0.023857564, 0.001, "Wrong delta WS in Test 34");
            Assert.AreEqual(This_Delta_WS[4].deltaWS_Expo, 3.211159106, 0.001, "Wrong delta WS in Test 34");

            // Test 34: Flow 1 = Turbulent; Flow 2 = Speed-Up (Sep Pt 1 is SpdUp, Site 1 is outside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 10, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.031001225, 0.001, "Wrong delta WS in Test 35");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.449658964, 0.001, "Wrong delta WS in Test 35");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.021688694, 0.001, "Wrong delta WS in Test 35");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 1.458971494, 0.001, "Wrong delta WS in Test 35");

            // Test 35: Flow 1 = Turbulent; Flow 2 = Speed-Up (Sep Pt 1 is Uphill, Site 1 is inside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 200;
            Sep_Node_1[0].turbEndNode = null;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 10, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 36");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, 1.754356481, 0.001, "Wrong delta WS in Test 36");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, -0.023857564, 0.001, "Wrong delta WS in Test 36");
            Assert.AreEqual(This_Delta_WS[3].deltaWS_Expo, 2.387224803, 0.001, "Wrong delta WS in Test 36");

            // Test 35: Flow 1 = Turbulent; Flow 2 = Speed-Up (Sep Pt 1 is SpdUp, Site 1 is inside turbulent zone)
            Sep_Node_1[0].highNode.expo[0].expo[0] = 20;
            This_Delta_WS = modelList.Get_DeltaWS_UW_Expo(0, 10, 40, 60, 30, 30, thisModel, 0, 0, Sep_Node_1, Sep_Node_2, 8, true, Node_1, Node_2, false);
            Assert.AreEqual(This_Delta_WS[0].deltaWS_Expo, 0.656725886, 0.001, "Wrong delta WS in Test 37");
            Assert.AreEqual(This_Delta_WS[1].deltaWS_Expo, -0.021688694, 0.001, "Wrong delta WS in Test 37");
            Assert.AreEqual(This_Delta_WS[2].deltaWS_Expo, 0.635037192, 0.001, "Wrong delta WS in Test 37");

        }

        [TestMethod]
        public void GetDeltaWS_SRDH_Test()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.SetDefaultModelCoeffs(16);
            thisModel.radius = 4000;

            ModelCollection modelList = new ModelCollection();

            double This_Delta_WS = modelList.GetDeltaWS_SRDH(6, 80, 0.2f, 0.3f, 5, 7, 5, 4);
            Assert.AreEqual(-0.78659, This_Delta_WS, 0.001, "Wrong delta WS in Test 1");

            This_Delta_WS = modelList.GetDeltaWS_SRDH(10, 80, 0.05f, 0.1f, 2, 1, 3, 5);
            Assert.AreEqual(1.27466, This_Delta_WS, 0.001, "Wrong delta WS in Test 2");

        }

        [TestMethod]
        public void GetWS_Equiv_Test()
        {
            ModelCollection modelList = new ModelCollection();
            string Filepath = testingFolder + "\\Get_WS_Equiv";
            string WR_Pred_file = Filepath + "\\WR_Pred.txt";
            StreamReader sr = new StreamReader(WR_Pred_file);

            double[] WR_Pred = new double[16];
            for (int i = 0; i < 16; i++)
                WR_Pred[i] = Convert.ToSingle(sr.ReadLine()) / 100;

            sr.Close();

            string WR_Target_file = Filepath + "\\WR_Target.txt";
            sr = new StreamReader(WR_Target_file);

            double[] WR_Target = new double[16];
            for (int i = 0; i < 16; i++)
                WR_Target[i] = Convert.ToSingle(sr.ReadLine()) / 100;

            sr.Close();

            string WS_Pred_file = Filepath + "\\WS_Pred.txt";
            sr = new StreamReader(WS_Pred_file);

            double[] WS_Pred = new double[16];
            for (int i = 0; i < 16; i++)
                WS_Pred[i] = Convert.ToSingle(sr.ReadLine());

            sr.Close();

            double[] WS_Equiv = modelList.GetWS_Equiv(WR_Pred, WR_Target, WS_Pred);

            string WS_Equiv_Excel = Filepath + "\\WS_Equiv.txt";
            sr = new StreamReader(WS_Equiv_Excel);

            for (int i = 0; i < 16; i++)
            {
                double thisWSEquiv = Convert.ToDouble(sr.ReadLine());
                Assert.AreEqual(WS_Equiv[i], thisWSEquiv, 0.001, "Wrong equivalent WS in Sector " + i);
            }

        }

        [TestMethod]
        public void ExportAllMetCrossPreds()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\ModelCollection Great Western.cfm";
            thisInst.Open(Filename);

            string overallOutput = testingFolder + "\\Calc_RMS_Overall_Sector\\Overall CrossPredictions.csv";
            string sectorOutput = testingFolder + "\\Calc_RMS_Overall_Sector\\Sectorwise CrossPredictions.csv";

            StreamWriter overall = new StreamWriter(overallOutput);
            StreamWriter sector = new StreamWriter(sectorOutput);

            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);

            // Do overall first
            for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
            {
                Model thisModel = models[i];
                overall.WriteLine("Radius = " + thisModel.radius.ToString());

                for (int j = 0; j < thisInst.metPairList.PairCount; j++)
                {
                    Pair_Of_Mets thisPair = thisInst.metPairList.metPairs[j];
                    Pair_Of_Mets.WS_CrossPreds thisCross = thisPair.GetWS_CrossPred(thisModel);
                    overall.WriteLine(thisPair.met1.name + "," + thisPair.met2.name + "," + Math.Round(thisCross.percErr[0], 4));
                    overall.WriteLine(thisPair.met2.name + "," + thisPair.met1.name + "," + Math.Round(thisCross.percErr[1], 4));                                        
                }

                overall.WriteLine();
            }

            // Now do sectorwise
            for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
            {
                Model thisModel = models[i];
                sector.WriteLine("Radius = " + thisModel.radius.ToString());

                for (int WDind = 0; WDind < thisInst.metList.numWD; WDind++)
                {
                    sector.Write(WDind + ",");

                    for (int j = 0; j < thisInst.metPairList.PairCount; j++)
                    {
                        Pair_Of_Mets thisPair = thisInst.metPairList.metPairs[j];
                        Pair_Of_Mets.WS_CrossPreds thisCross = thisPair.GetWS_CrossPred(thisModel);

                        sector.Write(Math.Round(thisCross.percErrSector[0, WDind], 4) + ",");
                        sector.Write(Math.Round(thisCross.percErrSector[1, WDind], 4) + ",");
                    }
                    sector.WriteLine();
                }

                sector.WriteLine();

            }

            thisInst.Close();
            overall.Close();
            sector.Close();
        }

        [TestMethod]
        public void Calc_RMS_Overall_and_Sectorwise_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\ModelCollection Great Western.cfm";
            thisInst.Open(Filename);

            // When this model was created, the function 'CalcRMS_Overall_and_Sectorwise' was called so RMS errors exist in models. 

            string overallRMS = testingFolder + "\\Calc_RMS_Overall_Sector\\Overall RMS.csv";
            string sector4000 = testingFolder + "\\Calc_RMS_Overall_Sector\\Sectorwise RMS 4000.csv";
            string sector6000 = testingFolder + "\\Calc_RMS_Overall_Sector\\Sectorwise RMS 6000.csv";
            string sector8000 = testingFolder + "\\Calc_RMS_Overall_Sector\\Sectorwise RMS 8000.csv";
            string sector10000 = testingFolder + "\\Calc_RMS_Overall_Sector\\Sectorwise RMS 10000.csv";

            StreamReader srOverall = new StreamReader(overallRMS);
            StreamReader srSect4000 = new StreamReader(sector4000);
            StreamReader srSect6000 = new StreamReader(sector6000);
            StreamReader srSect8000 = new StreamReader(sector8000);
            StreamReader srSect10000 = new StreamReader(sector10000);
            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
                        
            for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
            {                
                Model thisModel = models[i];
                double thisRMS = Convert.ToDouble(srOverall.ReadLine());
                // Test overall RMS error
                Assert.AreEqual(thisRMS, thisModel.RMS_WS_Est, 0.001, "Wrong overall RMS error radius = " + thisModel.radius.ToString());
                                
                // Test sectorwise RMS error
                for (int j = 0; j < thisInst.metList.numWD; j++)
                {
                    if (i == 0)
                        thisRMS = Convert.ToDouble(srSect4000.ReadLine());
                    else if (i == 1)
                        thisRMS = Convert.ToDouble(srSect6000.ReadLine());
                    else if (i == 2)
                        thisRMS = Convert.ToDouble(srSect8000.ReadLine());
                    else if (i == 3)
                        thisRMS = Convert.ToDouble(srSect10000.ReadLine());

                    Assert.AreEqual(thisRMS, thisModel.RMS_Sect_WS_Est[j], 0.001, "Wrong sectorwise RMS error radius = " + thisModel.radius.ToString());
                }
            }

            srOverall.Close();
            srSect4000.Close();
            srSect6000.Close();
            srSect8000.Close();
            srSect10000.Close();

            thisInst.Close();
        }

        [TestMethod]
        public void ExportP10Values()
        {
            Continuum thisInst = new Continuum("");
            thisInst.isTest = true;

            string Filename = testingFolder + "\\DoWS_Estimate\\ModelCollection DoWS_Estimate.cfm";
            thisInst.Open(Filename);                      

            Met met1 = thisInst.metList.metItem[0];
            Met met2 = thisInst.metList.metItem[1];
            Met met3 = thisInst.metList.metItem[2];

            for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
            {
                string metFile = testingFolder + "\\GetWS_EstWeights\\Great Western Met P10 Expos " + thisInst.radiiList.investItem[i].radius.ToString() + ".csv";
                StreamWriter swMet = new StreamWriter(metFile);

                swMet.WriteLine(met1.name + " P10 UW," + met1.name + " P10 DW," + met2.name + " P10 UW," + met2.name + " P10 DW," + met3.name + " P10 UW," + met3.name + " P10 DW");

                for (int j = 0; j < thisInst.metList.numWD; j++)
                    swMet.WriteLine(Math.Round(met1.gridStats.stats[i].P10_UW[j], 3).ToString() + "," + Math.Round(met1.gridStats.stats[i].P10_DW[j], 3).ToString() + ","
                        + Math.Round(met2.gridStats.stats[i].P10_UW[j], 3).ToString() + "," + Math.Round(met2.gridStats.stats[i].P10_DW[j], 3).ToString() + ","
                        + Math.Round(met3.gridStats.stats[i].P10_UW[j], 3).ToString() + "," + Math.Round(met3.gridStats.stats[i].P10_DW[j], 3).ToString());

                swMet.Close();
            }
            
            Turbine turb1 = thisInst.turbineList.turbineEsts[0];
            double[] windRose1 = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), turb1.UTMX, turb1.UTMY, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
            Turbine turb2 = thisInst.turbineList.turbineEsts[1];
            double[] windRose2 = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), turb2.UTMX, turb2.UTMY, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
            Turbine turb3 = thisInst.turbineList.turbineEsts[2];
            double[] windRose3 = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), turb3.UTMX, turb3.UTMY, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
            
            for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
            {
                string turbFile = testingFolder + "\\GetWS_EstWeights\\Great Western Turb P10 Expos " + thisInst.radiiList.investItem[i].radius.ToString() + ".csv";
                StreamWriter swTurb = new StreamWriter(turbFile);

                swTurb.WriteLine(turb1.name + " P10 UW," + turb1.name + " P10 DW," + turb1.name + " Wind Rose," 
                    + turb2.name + " P10 UW," + turb2.name + " P10 DW," + turb2.name + " Wind Rose,"
                    + turb3.name + " P10 UW," + turb3.name + " P10 DW," + turb3.name + " Wind Rose");

                for (int j = 0; j < thisInst.metList.numWD; j++)
                {                    
                    swTurb.WriteLine(Math.Round(turb1.gridStats.stats[i].P10_UW[j], 3).ToString() + "," + Math.Round(turb1.gridStats.stats[i].P10_DW[j], 3).ToString() + "," + Math.Round(windRose1[j],3).ToString() + ","
                        + Math.Round(turb2.gridStats.stats[i].P10_UW[j], 3).ToString() + "," + Math.Round(turb2.gridStats.stats[i].P10_DW[j], 3).ToString() + "," + Math.Round(windRose2[j], 3).ToString() + ","
                        + Math.Round(turb3.gridStats.stats[i].P10_UW[j], 3).ToString() + "," + Math.Round(turb3.gridStats.stats[i].P10_DW[j], 3).ToString() + "," + Math.Round(windRose3[j], 3).ToString());
                }

                swTurb.Close();
            }                       
            
            thisInst.Close();
        }

        [TestMethod]
        public void GetWS_EstWeights_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\DoWS_Estimate\\ModelCollection DoWS_Estimate.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            Nodes targetNode = new Nodes();
            NodeCollection nodeList = new NodeCollection();
            targetNode = nodeList.GetTurbNode(thisInst.turbineList.turbineEsts[0]);
            
            targetNode.elev = thisInst.topo.CalcElevs(targetNode.UTMX, targetNode.UTMY);
            targetNode.gridStats = new Grid_Info();

            targetNode.CalcGridStatsAndExposures(thisInst);

            Met[] metsUsed = thisInst.metList.GetMets(thisInst.metList.GetMetsUsed(), null);
            Model[] Models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            
            ModelCollection.ModelWeights[] theseWeights = thisInst.modelList.GetWS_EstWeights(metsUsed, targetNode, Models, 
                thisInst.metList.GetAvgWindRose(thisInst.modeledHeight, Met.TOD.All, Met.Season.All), thisInst.radiiList);

            string weightFile = testingFolder + "\\GetWS_EstWeights\\Great Western Turb 464 Weights.csv";
            StreamReader sr = new StreamReader(weightFile);
                        
            for (int i = 0; i < thisInst.radiiList.ThisCount; i++)
            {
                int radius = thisInst.radiiList.investItem[i].radius;
                string thisLine = sr.ReadLine();
                string[] wgtStrings = thisLine.Split(',');
                ModelCollection.ModelWeights thisWgt = new ModelCollection.ModelWeights();

                // Met 474
                for (int j = 0; j < theseWeights.Length; j++)
                {
                    if (theseWeights[j].model.radius == radius && theseWeights[j].met.name == "Met_474")
                    {
                        thisWgt = theseWeights[j];
                        break;
                    }
                }

                Assert.AreEqual(thisWgt.weight, Convert.ToDouble(wgtStrings[0]), 0.01, "Wrong WS Weight Met 474 " + radius.ToString());

                // Met 475
                for (int j = 0; j < theseWeights.Length; j++)
                {
                    if (theseWeights[j].model.radius == radius && theseWeights[j].met.name == "Met_475")
                    {
                        thisWgt = theseWeights[j];
                        break;
                    }
                }

                Assert.AreEqual(thisWgt.weight, Convert.ToDouble(wgtStrings[1]), 0.01, "Wrong WS Weight Met 475 " + radius.ToString());

                // Met 3350
                for (int j = 0; j < theseWeights.Length; j++)
                {
                    if (theseWeights[j].model.radius == radius && theseWeights[j].met.name == "Met_3350")
                    {
                        thisWgt = theseWeights[j];
                        break;
                    }
                }

                Assert.AreEqual(thisWgt.weight, Convert.ToDouble(wgtStrings[2]), 0.01, "Wrong WS Weight Met 3350 " + radius.ToString());
            }


            thisInst.Close();

        }

        [TestMethod]
        public void ExportParamsAndDeltaWS()
        {
            // Exports values needed for DoWS_Estimate_Test
            Continuum thisInst = new Continuum("");
            thisInst.isTest = true;
            
            string Filename = testingFolder + "\\DoWS_Estimate\\ModelCollection DoWS_Estimate.cfm";
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);
            bool useValley = thisInst.topo.useValley;

            StreamWriter sr = new StreamWriter(testingFolder + "\\DoWS_Estimate\\WS and Expos for DoWS_Estimate.csv");

            // Test 1: Met 474 to Met 475, Radius = 4000, WD = 270 (WD_ind = 18)
            int WD_ind = 18;
            int radInd = 0;
            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            Model model = models[radInd];
            Met startMet = thisInst.metList.GetMet("Met_474");
            Met.WSWD_Dist startWSDist = startMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

            Met endMet = thisInst.metList.GetMet("Met_475");
            Met.WSWD_Dist endWSDist = endMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

            double[] WS_Pred = new double[thisInst.metList.numWD];

            for (int i = 0; i < thisInst.metList.numWD; i++)
                WS_Pred[i] = startWSDist.WS * startWSDist.sectorWS_Ratio[i];

            double[] equivWS = thisInst.modelList.GetWS_Equiv(startWSDist.windRose, endWSDist.windRose, WS_Pred);
            double UW_Expo1 = startMet.expo[radInd].expo[WD_ind];
            double DW_Expo1 = startMet.expo[radInd].GetDW_Param(WD_ind, "Expo");
            double UW_SR1 = startMet.expo[radInd].SR[WD_ind];
            double DW_SR1 = startMet.expo[radInd].GetDW_Param(WD_ind, "SR");
            double UW_DH1 = startMet.expo[radInd].dispH[WD_ind];
            double DW_DH1 = startMet.expo[radInd].GetDW_Param(WD_ind, "DH");
            double P10UW_1 = startMet.gridStats.stats[radInd].P10_UW[WD_ind];
            double P10DW_1 = startMet.gridStats.stats[radInd].P10_DW[WD_ind];

            double UW_Expo2 = endMet.expo[radInd].expo[WD_ind];
            double DW_Expo2 = endMet.expo[radInd].GetDW_Param(WD_ind, "Expo");
            double UW_SR2 = endMet.expo[radInd].SR[WD_ind];
            double DW_SR2 = endMet.expo[radInd].GetDW_Param(WD_ind, "SR");
            double UW_DH2 = endMet.expo[radInd].dispH[WD_ind];
            double DW_DH2 = endMet.expo[radInd].GetDW_Param(WD_ind, "DH");
            double P10UW_2 = endMet.gridStats.stats[radInd].P10_UW[WD_ind];
            double P10DW_2 = endMet.gridStats.stats[radInd].P10_DW[WD_ind];

            double avgP10UW = (P10UW_1 + P10UW_2) / 2;
            double avgP10DW = (P10DW_1 + P10DW_2) / 2;
            double avgUWExpo = (UW_Expo1 + UW_Expo2) / 2;
            double avgDWExpo = (DW_Expo1 + DW_Expo2) / 2;

            double UW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, UW_SR1, false, "UW", useValley);
            double DW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, DW_SR1, false, "DW", useValley);
            double UW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, UW_SR2, false, "UW", useValley);
            double DW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, DW_SR2, false, "DW", useValley);
                        
            ModelCollection.Coeff_Delta_WS[] deltaWS_UW = thisInst.modelList.Get_DeltaWS_UW_Expo(UW_Expo1, UW_Expo2, DW_Expo1, DW_Expo2, avgP10UW, avgP10DW, model, WD_ind, radInd, null, null, 0,
                false, new NodeCollection.Node_UTMs(), new NodeCollection.Node_UTMs(), useValley);
            ModelCollection.Coeff_Delta_WS[] deltaWS_DW = thisInst.modelList.Get_DeltaWS_DW_Expo(equivWS[WD_ind], UW_Expo1, UW_Expo2, DW_Expo1, DW_Expo2, avgP10UW, avgP10DW,
                model, WD_ind, false, useValley);
            double deltaWS_SRDH_UW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD_ind], thisInst.modeledHeight, UW_SR1, UW_SR2, UW_DH1, UW_DH2, UW_Stab1, UW_Stab2);
            double deltaWS_SRDH_DW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD_ind], thisInst.modeledHeight, DW_SR1, DW_SR2, DW_DH1, DW_DH2, DW_Stab1, DW_Stab2);

            sr.WriteLine("Met 474 predicting Met 475 R = 4000 WD = 270");

            sr.WriteLine("Met 474 Equiv WS," + Math.Round(equivWS[WD_ind], 4));

            for (int i = 0; i < deltaWS_UW.Length; i++)
                sr.WriteLine("Delta WS UW Expo," + Math.Round(deltaWS_UW[i].deltaWS_Expo, 4));

            for (int i = 0; i < deltaWS_DW.Length; i++)
                sr.WriteLine("Delta WS DW Expo," + Math.Round(deltaWS_DW[i].deltaWS_Expo, 4));

            sr.WriteLine("Delta WS UW SRDH," + Math.Round(deltaWS_SRDH_UW, 4));
            sr.WriteLine("Delta WS DW SRDH," + Math.Round(deltaWS_SRDH_DW, 4));

            // Test 2: Met 3350 to Met 474, Radius = 10000, WD = 15 (WD_ind = 1)
            WD_ind = 1;
            radInd = 3;           
            model = models[radInd];
            startMet = thisInst.metList.GetMet("Met_3350");
            startWSDist = startMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

            endMet = thisInst.metList.GetMet("Met_474");
            endWSDist = endMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);                

            for (int i = 0; i < thisInst.metList.numWD; i++)
                WS_Pred[i] = startWSDist.WS * startWSDist.sectorWS_Ratio[i];

            equivWS = thisInst.modelList.GetWS_Equiv(startWSDist.windRose, endWSDist.windRose, WS_Pred);
            UW_Expo1 = startMet.expo[radInd].expo[WD_ind];
            DW_Expo1 = startMet.expo[radInd].GetDW_Param(WD_ind, "Expo");
            UW_SR1 = startMet.expo[radInd].SR[WD_ind];
            DW_SR1 = startMet.expo[radInd].GetDW_Param(WD_ind, "SR");
            UW_DH1 = startMet.expo[radInd].dispH[WD_ind];
            DW_DH1 = startMet.expo[radInd].GetDW_Param(WD_ind, "DH");
            P10UW_1 = startMet.gridStats.stats[radInd].P10_UW[WD_ind];
            P10DW_1 = startMet.gridStats.stats[radInd].P10_DW[WD_ind];

            UW_Expo2 = endMet.expo[radInd].expo[WD_ind];
            DW_Expo2 = endMet.expo[radInd].GetDW_Param(WD_ind, "Expo");
            UW_SR2 = endMet.expo[radInd].SR[WD_ind];
            DW_SR2 = endMet.expo[radInd].GetDW_Param(WD_ind, "SR");
            UW_DH2 = endMet.expo[radInd].dispH[WD_ind];
            DW_DH2 = endMet.expo[radInd].GetDW_Param(WD_ind, "DH");
            P10UW_2 = endMet.gridStats.stats[radInd].P10_UW[WD_ind];
            P10DW_2 = endMet.gridStats.stats[radInd].P10_DW[WD_ind];

            avgP10UW = (P10UW_1 + P10UW_2) / 2;
            avgP10DW = (P10DW_1 + P10DW_2) / 2;
            avgUWExpo = (UW_Expo1 + UW_Expo2) / 2;
            avgDWExpo = (DW_Expo1 + DW_Expo2) / 2;

            UW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, UW_SR1, false, "UW", useValley);
            DW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, DW_SR1, false, "DW", useValley);
            UW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, UW_SR2, false, "UW", useValley);
            DW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, DW_SR2, false, "DW", useValley);

            deltaWS_UW = thisInst.modelList.Get_DeltaWS_UW_Expo(UW_Expo1, UW_Expo2, DW_Expo1, DW_Expo2, avgP10UW, avgP10DW, model, WD_ind, radInd, null, null, 0,
                false, new NodeCollection.Node_UTMs(), new NodeCollection.Node_UTMs(), useValley);
            deltaWS_DW = thisInst.modelList.Get_DeltaWS_DW_Expo(equivWS[WD_ind], UW_Expo1, UW_Expo2, DW_Expo1, DW_Expo2, avgP10UW, avgP10DW,
                model, WD_ind, false, useValley);
            deltaWS_SRDH_UW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD_ind], thisInst.modeledHeight, UW_SR1, UW_SR2, UW_DH1, UW_DH2, UW_Stab1, UW_Stab2);
            deltaWS_SRDH_DW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD_ind], thisInst.modeledHeight, DW_SR1, DW_SR2, DW_DH1, DW_DH2, DW_Stab1, DW_Stab2);

            sr.WriteLine("Met 3350 predicting Met 474 R = 10000 WD = 15");

            sr.WriteLine("Met 3350 Equiv WS," + Math.Round(equivWS[WD_ind], 4));

            for (int i = 0; i < deltaWS_UW.Length; i++)
                if (deltaWS_UW[i].flowType != "Total")
                    sr.WriteLine("Delta WS UW Expo," + Math.Round(deltaWS_UW[i].deltaWS_Expo, 4));

            for (int i = 0; i < deltaWS_DW.Length; i++)
                if (deltaWS_DW[i].flowType != "Total")
                    sr.WriteLine("Delta WS DW Expo," + Math.Round(deltaWS_DW[i].deltaWS_Expo, 4));

            sr.WriteLine("Delta WS UW SRDH," + Math.Round(deltaWS_SRDH_UW, 4));
            sr.WriteLine("Delta WS DW SRDH," + Math.Round(deltaWS_SRDH_DW, 4));                       

            // Test 3: Met 474 to Turb 532, Radius = 4000, WD = 285 (WD_ind = 19)
            WD_ind = 19;
            radInd = 0;
            model = models[radInd];
            startMet = thisInst.metList.GetMet("Met_474");
            startWSDist = startMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);
                     
            Turbine endTurbine = thisInst.turbineList.GetTurbine("Turb_532");
            Turbine.Avg_Est avgEst = endTurbine.GetAvgWS_Est(null);
            Turbine.WS_Ests wsEst = endTurbine.GetWS_Est(4000, startMet.name, model);
            Nodes[] pathOfNodes = wsEst.pathOfNodes;

            // Get delta WS values from start met to node
            Nodes node = pathOfNodes[0];
            double[] nodeWR = thisInst.metList.GetInterpolatedWindRose(thisInst.metList.GetMetsUsed(), node.UTMX, node.UTMY, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);

            for (int i = 0; i < thisInst.metList.numWD; i++)
                WS_Pred[i] = startWSDist.WS * startWSDist.sectorWS_Ratio[i];

            double[] nodeWS = new double[thisInst.metList.numWD];
            equivWS = thisInst.modelList.GetWS_Equiv(startWSDist.windRose, nodeWR, WS_Pred);

            // Doing calcs for all WD since we need the estimated WS at all WD for the equiv WS calc at the node
            for (int WD = 0; WD < thisInst.metList.numWD; WD++)
            {                
                UW_Expo1 = startMet.expo[radInd].expo[WD];
                DW_Expo1 = startMet.expo[radInd].GetDW_Param(WD, "Expo");
                UW_SR1 = startMet.expo[radInd].SR[WD];
                DW_SR1 = startMet.expo[radInd].GetDW_Param(WD, "SR");
                UW_DH1 = startMet.expo[radInd].dispH[WD];
                DW_DH1 = startMet.expo[radInd].GetDW_Param(WD, "DH");
                P10UW_1 = startMet.gridStats.stats[radInd].P10_UW[WD];
                P10DW_1 = startMet.gridStats.stats[radInd].P10_DW[WD];

                UW_Expo2 = node.expo[radInd].expo[WD];
                DW_Expo2 = node.expo[radInd].GetDW_Param(WD, "Expo");
                UW_SR2 = node.expo[radInd].SR[WD];
                DW_SR2 = node.expo[radInd].GetDW_Param(WD, "SR");
                UW_DH2 = node.expo[radInd].dispH[WD];
                DW_DH2 = node.expo[radInd].GetDW_Param(WD, "DH");
                P10UW_2 = node.gridStats.stats[radInd].P10_UW[WD];
                P10DW_2 = node.gridStats.stats[radInd].P10_DW[WD];

                avgP10UW = (P10UW_1 + P10UW_2) / 2;
                avgP10DW = (P10DW_1 + P10DW_2) / 2;
                avgUWExpo = (UW_Expo1 + UW_Expo2) / 2;
                avgDWExpo = (DW_Expo1 + DW_Expo2) / 2;

                UW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD, UW_SR1, false, "UW", useValley);
                DW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD, DW_SR1, false, "DW", useValley);
                UW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD, UW_SR2, false, "UW", useValley);
                DW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD, DW_SR2, false, "DW", useValley);

                deltaWS_UW = thisInst.modelList.Get_DeltaWS_UW_Expo(UW_Expo1, UW_Expo2, DW_Expo1, DW_Expo2, avgP10UW, avgP10DW, model, WD, radInd, null, null, 0,
                    false, new NodeCollection.Node_UTMs(), new NodeCollection.Node_UTMs(), useValley);
                deltaWS_DW = thisInst.modelList.Get_DeltaWS_DW_Expo(equivWS[WD], UW_Expo1, UW_Expo2, DW_Expo1, DW_Expo2, avgP10UW, avgP10DW,
                    model, WD, false, useValley);
                deltaWS_SRDH_UW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD], thisInst.modeledHeight, UW_SR1, UW_SR2, UW_DH1, UW_DH2, UW_Stab1, UW_Stab2);
                deltaWS_SRDH_DW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD], thisInst.modeledHeight, DW_SR1, DW_SR2, DW_DH1, DW_DH2, DW_Stab1, DW_Stab2);

                if (WD == 19)
                {
                    sr.WriteLine("Met 474 predicting Turb 532 R = 4000 WD = 285");

                    sr.WriteLine("Met 474 Equiv WS," + Math.Round(equivWS[WD], 4));

                    for (int i = 0; i < deltaWS_UW.Length; i++)
                        if (deltaWS_UW[i].flowType != "Total")
                            sr.WriteLine("Delta WS UW Expo," + Math.Round(deltaWS_UW[i].deltaWS_Expo, 4));

                    for (int i = 0; i < deltaWS_DW.Length; i++)
                        if (deltaWS_DW[i].flowType != "Total")
                            sr.WriteLine("Delta WS DW Expo," + Math.Round(deltaWS_DW[i].deltaWS_Expo, 4));

                    sr.WriteLine("Delta WS UW SRDH," + Math.Round(deltaWS_SRDH_UW, 4));
                    sr.WriteLine("Delta WS DW SRDH," + Math.Round(deltaWS_SRDH_DW, 4));
                }
                                
                nodeWS[WD] = equivWS[WD] + deltaWS_SRDH_UW + deltaWS_SRDH_DW;
                for (int i = 0; i < deltaWS_UW.Length; i++)
                    if (deltaWS_UW[i].flowType != "Total")
                        nodeWS[WD] = nodeWS[WD] + deltaWS_UW[i].deltaWS_Expo;

                for (int i = 0; i < deltaWS_DW.Length; i++)
                    if (deltaWS_DW[i].flowType != "Total")
                        nodeWS[WD] = nodeWS[WD] + deltaWS_DW[i].deltaWS_Expo;
            }

            // Find delta WS from node to turbine
            equivWS = thisInst.modelList.GetWS_Equiv(nodeWR, avgEst.freeStream.windRose, nodeWS);

            UW_Expo1 = node.expo[radInd].expo[WD_ind];
            DW_Expo1 = node.expo[radInd].GetDW_Param(WD_ind, "Expo");
            UW_SR1 = node.expo[radInd].SR[WD_ind];
            DW_SR1 = node.expo[radInd].GetDW_Param(WD_ind, "SR");
            UW_DH1 = node.expo[radInd].dispH[WD_ind];
            DW_DH1 = node.expo[radInd].GetDW_Param(WD_ind, "DH");
            P10UW_1 = node.gridStats.stats[radInd].P10_UW[WD_ind];
            P10DW_1 = node.gridStats.stats[radInd].P10_DW[WD_ind];

            UW_Expo2 = endTurbine.expo[radInd].expo[WD_ind];
            DW_Expo2 = endTurbine.expo[radInd].GetDW_Param(WD_ind, "Expo");
            UW_SR2 = endTurbine.expo[radInd].SR[WD_ind];
            DW_SR2 = endTurbine.expo[radInd].GetDW_Param(WD_ind, "SR");
            UW_DH2 = endTurbine.expo[radInd].dispH[WD_ind];
            DW_DH2 = endTurbine.expo[radInd].GetDW_Param(WD_ind, "DH");
            P10UW_2 = endTurbine.gridStats.stats[radInd].P10_UW[WD_ind];
            P10DW_2 = endTurbine.gridStats.stats[radInd].P10_DW[WD_ind];

            avgP10UW = (P10UW_1 + P10UW_2) / 2;
            avgP10DW = (P10DW_1 + P10DW_2) / 2;
            avgUWExpo = (UW_Expo1 + UW_Expo2) / 2;
            avgDWExpo = (DW_Expo1 + DW_Expo2) / 2;

            UW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, UW_SR1, false, "UW", useValley);
            DW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, DW_SR1, false, "DW", useValley);
            UW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, UW_SR2, false, "UW", useValley);
            DW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, DW_SR2, false, "DW", useValley);

            deltaWS_UW = thisInst.modelList.Get_DeltaWS_UW_Expo(UW_Expo1, UW_Expo2, DW_Expo1, DW_Expo2, avgP10UW, avgP10DW, model, WD_ind, radInd, null, null, 0,
                false, new NodeCollection.Node_UTMs(), new NodeCollection.Node_UTMs(), useValley);
            deltaWS_DW = thisInst.modelList.Get_DeltaWS_DW_Expo(equivWS[WD_ind], UW_Expo1, UW_Expo2, DW_Expo1, DW_Expo2, avgP10UW, avgP10DW,
                model, WD_ind, false, useValley);
            deltaWS_SRDH_UW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD_ind], thisInst.modeledHeight, UW_SR1, UW_SR2, UW_DH1, UW_DH2, UW_Stab1, UW_Stab2);
            deltaWS_SRDH_DW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD_ind], thisInst.modeledHeight, DW_SR1, DW_SR2, DW_DH1, DW_DH2, DW_Stab1, DW_Stab2);
                       
            sr.WriteLine("Node Equiv WS," + Math.Round(equivWS[WD_ind], 4));

            for (int i = 0; i < deltaWS_UW.Length; i++)
                if (deltaWS_UW[i].flowType != "Total")
                    sr.WriteLine("Delta WS UW Expo," + Math.Round(deltaWS_UW[i].deltaWS_Expo, 4));

            for (int i = 0; i < deltaWS_DW.Length; i++)
                if (deltaWS_DW[i].flowType != "Total")
                    sr.WriteLine("Delta WS DW Expo," + Math.Round(deltaWS_DW[i].deltaWS_Expo, 4));

            sr.WriteLine("Delta WS UW SRDH," + Math.Round(deltaWS_SRDH_UW, 4));
            sr.WriteLine("Delta WS DW SRDH," + Math.Round(deltaWS_SRDH_DW, 4));

            sr.Close();
            thisInst.Close();
        }

        [TestMethod]
        public void DoWS_Estimate_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\DoWS_Estimate\\ModelCollection DoWS_Estimate.cfm";
            thisInst.Open(Filename);                    

            // Test 1: Met 474 to Met 475            
            
            int radInd = 0;
            int WD_Ind = 18;
            Met startMet = thisInst.metList.GetMet("Met_474");
            Met endMet = thisInst.metList.GetMet("Met_475");
            NodeCollection nodeList = new NodeCollection();
            Nodes endNode = nodeList.GetMetNode(endMet);
            Pair_Of_Mets metPair = thisInst.metPairList.GetPair_Of_Mets(startMet, endMet);
            Nodes[] pathOfNodes = metPair.WS_Pred[radInd, 0].nodePath;
            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            Model model = models[radInd];

            ModelCollection.WS_Est_Struct thisEst = thisInst.modelList.DoWS_Estimate(startMet, endNode, pathOfNodes, model, thisInst);
            Assert.AreEqual(thisEst.sectorWS[WD_Ind], 7.0258, 0.001, "Wrong WS Test 1");

            // Test 2: Met 3350 to Met 474            
            radInd = 3;
            WD_Ind = 1;
            startMet = thisInst.metList.GetMet("Met_3350");
            endMet = thisInst.metList.GetMet("Met_474");            
            endNode = nodeList.GetMetNode(endMet);
            metPair = thisInst.metPairList.GetPair_Of_Mets(startMet, endMet);
            pathOfNodes = metPair.WS_Pred[radInd, 0].nodePath;
            models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            model = models[radInd];

            thisEst = thisInst.modelList.DoWS_Estimate(startMet, endNode, pathOfNodes, model, thisInst);
            Assert.AreEqual(thisEst.sectorWS[WD_Ind], 7.9439, 0.001, "Wrong WS Test 2");

            // Test 3: Met 475 to Turb 464            
            radInd = 0;
            WD_Ind = 19;
            startMet = thisInst.metList.GetMet("Met_474");
            Turbine endTurbine = thisInst.turbineList.GetTurbine("Turb_532");
            endNode = nodeList.GetTurbNode(endTurbine);
                        
            models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            model = models[radInd];
            Turbine.WS_Ests wsEst = endTurbine.GetWS_Est(4000, startMet.name, model);
            pathOfNodes = wsEst.pathOfNodes;

            thisEst = thisInst.modelList.DoWS_Estimate(startMet, endNode, pathOfNodes, model, thisInst);
            Assert.AreEqual(thisEst.sectorWS[WD_Ind], 6.856, 0.001, "Wrong WS Test 3");

            thisInst.Close();
        }

        [TestMethod]
        public void FindSiteCalibratedModels_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\ModelCollection Great Western.cfm";
            thisInst.Open(Filename);

            thisInst.modelList.ClearAllExceptImported();
            Assert.AreEqual(thisInst.modelList.ModelCount, 0, 0, "Didn't clear models");

            thisInst.modelList.FindSiteCalibratedModels(thisInst, Met.TOD.All, Met.Season.All, thisInst.modeledHeight);
            Assert.AreEqual(thisInst.modelList.ModelCount, 1, 0, "Didn't create site-calibrated model");
            
            thisInst.Close();
        }

   /*     [TestMethod]
        public void GetModelWeights_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string Filename = testingFolder + "\\Testing Error Estimate.cfm";
            thisInst.Open(Filename);

            Model[] theseModels = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), true, Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            double[] theseWeights = thisInst.modelList.GetModelWeights(theseModels);

            Assert.AreEqual(theseWeights[0], 0.25, 0.001, "Wrong Model Weight 4000 m");
            Assert.AreEqual(theseWeights[1], 1.00, 0.001, "Wrong Model Weight 6000 m");
            Assert.AreEqual(theseWeights[2], 0.99255, 0.001, "Wrong Model Weight 8000 m");
            Assert.AreEqual(theseWeights[3], 0.68611, 0.001, "Wrong Model Weight 10000 m");

            thisInst.Close();
        }
*/
        [TestMethod]
        public void CalcGrossAEP_AndMonthlyEnergy_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\ModelCollection testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            NodeCollection nodeList = new NodeCollection();
            Nodes targetNode = nodeList.GetTurbNode(thisTurb);
            string MCP_Method = thisInst.Get_MCP_Method();
            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0]; ;
            Wake_Model wakeModel = new Wake_Model();

            ModelCollection.TimeSeries[] thisTS = thisInst.modelList.GenerateTimeSeries(thisInst, thisInst.metList.GetMetsUsed(), targetNode, powerCurve, wakeModel, null, MCP_Method);

            Turbine.Gross_Energy_Est thisGross = new Turbine.Gross_Energy_Est();
            thisInst.modelList.CalcGrossAEP_AndMonthlyEnergy(ref thisGross, thisTS, thisInst);

            Assert.AreEqual(thisGross.AEP, 3302.594677, 0.1, "Wrong average AEP Test 1");
            Assert.AreEqual(thisGross.sectorEnergy[0], 104.1019456, 0.1, "Wrong sector AEP Test 2");
            Assert.AreEqual(thisGross.sectorEnergy[10], 381.2825653, 0.1, "Wrong sector AEP Test 3");
            Assert.AreEqual(thisGross.sectorEnergy[15], 180.9071585, 0.1, "Wrong sector AEP Test 4");

            Assert.AreEqual(thisGross.monthlyVals[0].energyProd, 328.3761571, 0.1, "Wrong monthly Jan 2005 Test 5");
            Assert.AreEqual(thisGross.monthlyVals[22].energyProd, 213.0688251, 0.1, "Wrong sector AEP Test 6");
            Assert.AreEqual(thisGross.monthlyVals[29].energyProd, 156.0195372, 0.1, "Wrong sector AEP Test 7");
            Assert.AreEqual(thisGross.monthlyVals[71].energyProd, 312.4151406, 0.1, "Wrong sector AEP Test 8");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcNetAEP_AndMonthlyEnergy_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\ModelCollection testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            NodeCollection nodeList = new NodeCollection();
            Nodes targetNode = nodeList.GetTurbNode(thisTurb);
            string MCP_Method = thisInst.Get_MCP_Method();
            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0]; ;
            Wake_Model wakeModel = thisInst.wakeModelList.wakeModels[0];

            // Find wake loss coeffs
            WakeCollection.WakeLossCoeffs[] wakeCoeffs = null;
            int minDistance = 10000000;
            int maxDistance = 0;

            for (int i = 0; i < thisInst.turbineList.TurbineCount; i++)
            {
                int[] Min_Max_Dist = thisInst.turbineList.CalcMinMaxDistanceToTurbines(thisInst.turbineList.turbineEsts[i].UTMX, thisInst.turbineList.turbineEsts[i].UTMY);
                if (Min_Max_Dist[0] < minDistance) minDistance = Min_Max_Dist[0]; // this is min distance to turbine but when WD is at a different angle (not in line with turbines) the X dist is less than this value so making this always equal to 2*RD
                if (Min_Max_Dist[1] > maxDistance) maxDistance = Min_Max_Dist[1];
            }

            minDistance = (int)(2 * wakeModel.powerCurve.RD);
            if (maxDistance == 0) maxDistance = 15000; // maxDistance will be zero when there is only one turbine. Might be good to make this value constant
            wakeCoeffs = thisInst.wakeModelList.GetWakeLossesCoeffs(minDistance, maxDistance, wakeModel, thisInst.metList);

            ModelCollection.TimeSeries[] thisTS = thisInst.modelList.GenerateTimeSeries(thisInst, thisInst.metList.GetMetsUsed(), targetNode, powerCurve, wakeModel, wakeCoeffs, MCP_Method);

            Turbine.Net_Energy_Est thisNet = new Turbine.Net_Energy_Est();
            thisNet.wakeModel = wakeModel;
            thisInst.modelList.CalcNetAEP_AndMonthlyEnergy(ref thisNet, thisTS, thisInst);

            Assert.AreEqual(thisNet.AEP, 3076.561197, 0.1, "Wrong average AEP Test 1");
            Assert.AreEqual(thisNet.sectorEnergy[0], 98.88121615, 0.1, "Wrong sector AEP Test 2");
            Assert.AreEqual(thisNet.sectorEnergy[5], 102.9205041, 0.1, "Wrong sector AEP Test 3");
            Assert.AreEqual(thisNet.sectorEnergy[15], 171.8346352, 0.1, "Wrong sector AEP Test 4");

            Assert.AreEqual(thisNet.monthlyVals[0].energyProd, 305.4312399, 0.1, "Wrong monthly Jan 2005 Test 5");
            Assert.AreEqual(thisNet.monthlyVals[22].energyProd, 200.7621586, 0.1, "Wrong monthly Test 6");
            Assert.AreEqual(thisNet.monthlyVals[29].energyProd, 146.8461043, 0.1, "Wrong monthly Test 7");
            Assert.AreEqual(thisNet.monthlyVals[71].energyProd, 295.9046064, 0.1, "Wrong monthly Test 8");

            thisInst.Close();
        }

        [TestMethod]
        public void DoWS_EstimateOneWDTimeSeries_Test()
        {
            Continuum thisInst = new Continuum("");
            
            string fileName = testingFolder + "\\ModelCollection TS testing.cfm";
            thisInst.Open(fileName);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            NodeCollection nodeList = new NodeCollection();
            Nodes endNode = nodeList.GetTurbNode(thisInst.turbineList.turbineEsts[1]);
            ModelCollection.PathsOfNodes[] pathsOfNodes = new ModelCollection.PathsOfNodes[1];
            pathsOfNodes[0].met = thisInst.metList.metItem[0];
            pathsOfNodes[0].model = thisInst.modelList.models[0, 0];
            Nodes metNode = nodeList.GetMetNode(thisInst.metList.metItem[0]);
            pathsOfNodes[0].path = nodeList.FindPathOfNodes(metNode, endNode, pathsOfNodes[0].model, thisInst);
            Nodes[] path = pathsOfNodes[0].path;

            double thisWSEst = thisInst.modelList.DoWS_EstimateOneWDTimeSeries(6.76, 8, thisInst.metList.metItem[0], endNode, path, pathsOfNodes[0].model, thisInst);
            Assert.AreEqual(thisWSEst, 5.3988, 0.001, "Wrong WS Estimate");

            thisInst.Close();
        }

        [TestMethod]
        public void CalcWSWD_Dist_Test()
        {
            Continuum thisInst = new Continuum("");
            thisInst.isTest = true;
            
            string fileName = testingFolder + "\\ModelCollection testing.cfm";
            thisInst.Open(fileName);

            Turbine thisTurb = thisInst.turbineList.turbineEsts[0];
            Wake_Model thisWakeModel = thisInst.wakeModelList.wakeModels[0];

            TurbineCollection.PowerCurve powerCurve = thisInst.turbineList.powerCurves[0]; ;
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
                avgEst.timeSeries = thisInst.modelList.GenerateTimeSeries(thisInst, thisInst.metList.GetMetsUsed(), targetNode, powerCurve, thisWakeModel, wakeCoeffs, MCP_Method);

            Met.WSWD_Dist thisDist = thisInst.modelList.CalcWSWD_Dist(avgEst.timeSeries, thisInst, "Freestream");
            Assert.AreEqual(thisDist.WS, 5.791210254, 0.001, "Wrong Avg WS Test 1");
            Assert.AreEqual(thisDist.windRose[0], 0.044861555, 0.001, "Wrong Wind Rose[0] Test 2");
            Assert.AreEqual(thisDist.windRose[11], 0.08972311, 0.001, "Wrong Wind Rose[11] Test 3");
            Assert.AreEqual(thisDist.sectorWS_Dist[5, 7], 0.155647383, 0.001, "Wrong Sector WS dist Test 4");

            thisDist = thisInst.modelList.CalcWSWD_Dist(avgEst.timeSeries, thisInst, "Waked");
            Assert.AreEqual(thisDist.WS, 5.622261524, 0.001, "Wrong Avg WS Test 5");
            Assert.AreEqual(thisDist.sectorWS_Ratio[9], 1.059366317, 0.001, "Wrong Sector Ratio Test 6");

            thisInst.Close();
        }

        [TestMethod]
        public void ExportDeltaWS_Ests()
        {
            // Exports values needed for DoWS_Estimate_Test
            Continuum thisInst = new Continuum("");

            string Filename = testingFolder + "\\ModelCollection TS testing.cfm";
            thisInst.isTest = true;
            thisInst.Open(Filename);
            thisInst.topo.GetElevsAndSRDH_ForCalcs(thisInst, null, false);

            StreamWriter sw = new StreamWriter(testingFolder + "\\DoWS_EstimateOneWD_TS\\Delta WS Ests.csv");
            NodeCollection nodeList = new NodeCollection();

            // Test 1: Ashtabula Iten to Marion W2, Radius = 4000, WD = 180 (WD_ind = 8)
            int WD_ind = 8;
            int radInd = 0;
            Model[] models = thisInst.modelList.GetModels(thisInst, thisInst.metList.GetMetsUsed(), Met.TOD.All, Met.Season.All, thisInst.modeledHeight, false);
            Model model = models[radInd];
            Met startMet = thisInst.metList.GetMet("Ashtabula");
            Nodes node1 = nodeList.GetMetNode(startMet);
            Met.WSWD_Dist startWSDist = startMet.GetWS_WD_Dist(thisInst.modeledHeight, Met.TOD.All, Met.Season.All);

            Turbine endTurb = thisInst.turbineList.turbineEsts[1];
            Nodes node2 = new Nodes();
                        
            double[] WS_Pred = new double[thisInst.metList.numWD];

            for (int i = 0; i < thisInst.metList.numWD; i++)
                WS_Pred[i] = startWSDist.WS * startWSDist.sectorWS_Ratio[i];
            
            double avgP10UW = 0;
            double avgP10DW = 0;
            double avgUWExpo = 0;
            double avgDWExpo = 0;

            double UW_Stab1 = 0;
            double DW_Stab1 = 0;
            double UW_Stab2 = 0;
            double DW_Stab2 = 0;

            Turbine.WS_Ests wsEst = endTurb.GetWS_Est(4000, "Ashtabula", model);
            Nodes[] pathOfNodes = wsEst.pathOfNodes;

            sw.WriteLine("Predictor Met: Ashtabula");
            sw.WriteLine();
            sw.WriteLine("Node, Value");

            double[] equivWS;
            ModelCollection.Coeff_Delta_WS[] deltaWS_UW;
            ModelCollection.Coeff_Delta_WS[] deltaWS_DW;
            double deltaWS_SRDH_UW;
            double deltaWS_SRDH_DW;

            bool useValley = thisInst.topo.useValley;

            for (int i = 0; i < pathOfNodes.Length; i++)
            {
                node2 = pathOfNodes[i];   
                equivWS = thisInst.modelList.GetWS_Equiv(startWSDist.windRose, startWSDist.windRose, WS_Pred); // only one met so wind rose is same
                avgP10UW = (node1.gridStats.stats[radInd].P10_UW[WD_ind] + node2.gridStats.stats[radInd].P10_UW[WD_ind]) / 2;
                avgP10DW = (node1.gridStats.stats[radInd].P10_DW[WD_ind] + node2.gridStats.stats[radInd].P10_DW[WD_ind]) / 2;

                avgUWExpo = (node1.expo[radInd].expo[WD_ind] + node2.expo[radInd].expo[WD_ind]) / 2;
                avgDWExpo = (node1.expo[radInd].GetDW_Param(WD_ind, "Expo") + node2.expo[radInd].GetDW_Param(WD_ind, "Expo")) / 2;

                deltaWS_UW = thisInst.modelList.Get_DeltaWS_UW_Expo(node1.expo[radInd].expo[WD_ind], node2.expo[radInd].expo[WD_ind], node1.expo[radInd].GetDW_Param(WD_ind, "Expo"),
                    node2.expo[radInd].GetDW_Param(WD_ind, "Expo"), avgP10UW, avgP10DW, model, WD_ind, radInd, null, null, 0, false, new NodeCollection.Node_UTMs(), new NodeCollection.Node_UTMs(), useValley);
                deltaWS_DW = thisInst.modelList.Get_DeltaWS_DW_Expo(equivWS[WD_ind], node1.expo[radInd].expo[WD_ind], node2.expo[radInd].expo[WD_ind],
                    node1.expo[radInd].GetDW_Param(WD_ind, "Expo"), node2.expo[radInd].GetDW_Param(WD_ind, "Expo"), avgP10UW, avgP10DW,
                    model, WD_ind, false, useValley);

                UW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, node1.expo[radInd].SR[WD_ind], false, "UW", useValley);
                DW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, node1.expo[radInd].GetDW_Param(WD_ind, "SR"), false, "DW", useValley);
                UW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, node2.expo[radInd].SR[WD_ind], false, "UW", useValley);
                DW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, node2.expo[radInd].GetDW_Param(WD_ind, "SR"), false, "DW", useValley);

                deltaWS_SRDH_UW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD_ind], thisInst.modeledHeight, node1.expo[radInd].SR[WD_ind], node2.expo[radInd].SR[WD_ind], node1.expo[radInd].dispH[WD_ind],
                    node2.expo[radInd].dispH[WD_ind], UW_Stab1, UW_Stab2);
                deltaWS_SRDH_DW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD_ind], thisInst.modeledHeight, node1.expo[radInd].GetDW_Param(WD_ind, "SR"), node2.expo[radInd].GetDW_Param(WD_ind, "SR"),
                    node1.expo[radInd].GetDW_Param(WD_ind, "DH"), node2.expo[radInd].GetDW_Param(WD_ind, "DH"), DW_Stab1, DW_Stab2);

                sw.WriteLine("Node" + (i + 1).ToString());
                sw.WriteLine();
                for (int j = 0; j < deltaWS_UW.Length; j++)
                {
                    sw.WriteLine("Delta WS UW Expo," + Math.Round(deltaWS_UW[j].deltaWS_Expo, 4));
                    WS_Pred[WD_ind] = WS_Pred[WD_ind] + deltaWS_UW[j].deltaWS_Expo;
                }              
                
                for (int j = 0; j < deltaWS_DW.Length; j++)
                {
                    sw.WriteLine("Delta WS DW Expo," + Math.Round(deltaWS_DW[j].deltaWS_Expo, 4));
                    WS_Pred[WD_ind] = WS_Pred[WD_ind] + deltaWS_DW[j].deltaWS_Expo;
                }

                WS_Pred[WD_ind] = WS_Pred[WD_ind] + deltaWS_SRDH_UW;
                WS_Pred[WD_ind] = WS_Pred[WD_ind] + deltaWS_SRDH_DW;

                sw.WriteLine("Delta WS UW SRDH," + Math.Round(deltaWS_SRDH_UW, 4));
                sw.WriteLine("Delta WS DW SRDH," + Math.Round(deltaWS_SRDH_DW, 4));

                node1 = node2;

                sw.WriteLine();
            }

            node2 = nodeList.GetTurbNode(endTurb);
            equivWS = thisInst.modelList.GetWS_Equiv(startWSDist.windRose, startWSDist.windRose, WS_Pred); // only one met so wind rose is same
            avgP10UW = (node1.gridStats.stats[radInd].P10_UW[WD_ind] + node2.gridStats.stats[radInd].P10_UW[WD_ind]) / 2;
            avgP10DW = (node1.gridStats.stats[radInd].P10_DW[WD_ind] + node2.gridStats.stats[radInd].P10_DW[WD_ind]) / 2;

            avgUWExpo = (node1.expo[radInd].expo[WD_ind] + node2.expo[radInd].expo[WD_ind]) / 2;
            avgDWExpo = (node1.expo[radInd].GetDW_Param(WD_ind, "Expo") + node2.expo[radInd].GetDW_Param(WD_ind, "Expo")) / 2;

            deltaWS_UW = thisInst.modelList.Get_DeltaWS_UW_Expo(node1.expo[radInd].expo[WD_ind], node2.expo[radInd].expo[WD_ind], node1.expo[radInd].GetDW_Param(WD_ind, "Expo"),
                node2.expo[radInd].GetDW_Param(WD_ind, "Expo"), avgP10UW, avgP10DW, model, WD_ind, radInd, null, null, 0, false, new NodeCollection.Node_UTMs(), new NodeCollection.Node_UTMs(), useValley);
            deltaWS_DW = thisInst.modelList.Get_DeltaWS_DW_Expo(equivWS[WD_ind], node1.expo[radInd].expo[WD_ind], node2.expo[radInd].expo[WD_ind],
                node1.expo[radInd].GetDW_Param(WD_ind, "Expo"), node2.expo[radInd].GetDW_Param(WD_ind, "Expo"), avgP10UW, avgP10DW,
                model, WD_ind, false, useValley);

            UW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, node1.expo[radInd].SR[WD_ind], false, "UW", useValley);
            DW_Stab1 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, node1.expo[radInd].GetDW_Param(WD_ind, "SR"), false, "DW", useValley);
            UW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, node2.expo[radInd].SR[WD_ind], false, "UW", useValley);
            DW_Stab2 = model.GetStabilityCorrection(avgUWExpo, avgDWExpo, WD_ind, node2.expo[radInd].GetDW_Param(WD_ind, "SR"), false, "DW", useValley);

            deltaWS_SRDH_UW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD_ind], thisInst.modeledHeight, node1.expo[radInd].SR[WD_ind], node2.expo[radInd].SR[WD_ind], node1.expo[radInd].dispH[WD_ind],
                node2.expo[radInd].dispH[WD_ind], UW_Stab1, UW_Stab2);
            deltaWS_SRDH_DW = thisInst.modelList.GetDeltaWS_SRDH(equivWS[WD_ind], thisInst.modeledHeight, node1.expo[radInd].GetDW_Param(WD_ind, "SR"), node2.expo[radInd].GetDW_Param(WD_ind, "SR"),
                node1.expo[radInd].GetDW_Param(WD_ind, "DH"), node2.expo[radInd].GetDW_Param(WD_ind, "DH"), DW_Stab1, DW_Stab2);

            sw.WriteLine("Turbine");
            sw.WriteLine();
            for (int j = 0; j < deltaWS_UW.Length; j++)
            {
                sw.WriteLine("Delta WS UW Expo," + Math.Round(deltaWS_UW[j].deltaWS_Expo, 4));
                WS_Pred[WD_ind] = WS_Pred[WD_ind] + deltaWS_UW[j].deltaWS_Expo;
            }                       

            for (int j = 0; j < deltaWS_DW.Length; j++)
            {
                sw.WriteLine("Delta WS DW Expo," + Math.Round(deltaWS_DW[j].deltaWS_Expo, 4));
                WS_Pred[WD_ind] = WS_Pred[WD_ind] + deltaWS_DW[j].deltaWS_Expo;
            }

            WS_Pred[WD_ind] = WS_Pred[WD_ind] + deltaWS_SRDH_UW;
            WS_Pred[WD_ind] = WS_Pred[WD_ind] + deltaWS_SRDH_DW;

            sw.WriteLine("Delta WS UW SRDH," + Math.Round(deltaWS_SRDH_UW, 4));
            sw.WriteLine("Delta WS DW SRDH," + Math.Round(deltaWS_SRDH_DW, 4));

            sw.Close();
            thisInst.Close();
        }
    }
}
