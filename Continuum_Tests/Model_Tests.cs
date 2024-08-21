using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinuumNS;

namespace Continuum_Tests
{
    /// <summary>
    /// Summary description for Model_Tests
    /// </summary>
    [TestClass]
    public class Model_Tests
    {
        [TestMethod]
        public void CalcDWCoeff_Test()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.SetDefaultModelCoeffs(16);

            double This_Coeff = thisModel.CalcDW_Coeff(3, 2.5f, 0, "Downhill");
            Assert.AreEqual(0.069831, This_Coeff, 0.001, "Wrong downhill coeff");

            This_Coeff = thisModel.CalcDW_Coeff(48, 58, 0, "Uphill");
            Assert.AreEqual(0.0064945, This_Coeff, 0.001, "Wrong uphill coeff");

            This_Coeff = thisModel.CalcDW_Coeff(200, 180, 0, "Turbulent");
            Assert.AreEqual(-0.00300, This_Coeff, 0.001, "Wrong Turbulent coeff");

        }

        [TestMethod]
        public void CalcUWCoeff_Test()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.SetDefaultModelCoeffs(16);

            double This_Coeff = thisModel.CalcUW_Coeff(2, 1, 0, "Downhill");
            Assert.AreEqual(-0.091025, This_Coeff, 0.0001, "Wrong downhill coeff");

            This_Coeff = thisModel.CalcUW_Coeff(44, 52, 0, "Uphill");
            Assert.AreEqual(-0.006953, This_Coeff, 0.0001, "Wrong uphill coeff");

            This_Coeff = thisModel.CalcUW_Coeff(13, 11, 0, "SpdUp");
            Assert.AreEqual(0.00357195, This_Coeff, 0.0001, "Wrong Speed-Up coeff");

        }

        [TestMethod]
        public void GetFlowType_Test()
        {
            Model thisModel = new Model();
            thisModel.SizeArrays(16);
            thisModel.SetDefaultModelCoeffs(16);
            bool useValley = false;
            
            string flowType = thisModel.GetFlowType(-10, -10, 0, "UW", null, 0, false, 0, useValley); // Test 1
            Assert.AreSame("Downhill", flowType, "Wrong flow type: Test 1");

            flowType = thisModel.GetFlowType(-10, -10, 0, "DW", null, 0, false, 0, useValley); // Test 2
            Assert.AreSame("Uphill", flowType, "Wrong flow type: Test 2");

            flowType = thisModel.GetFlowType(-10, 10, 0, "UW", null, 0, false, 0, useValley); // Test 3
            Assert.AreSame("Downhill", flowType, "Wrong flow type: Test 3");

            flowType = thisModel.GetFlowType(-10, 10, 0, "DW", null, 0, false, 0, useValley); // Test 4
            Assert.AreSame("Downhill", flowType, "Wrong flow type: Test 4");

            flowType = thisModel.GetFlowType(10, -10, 0, "UW", null, 0, false, 0, useValley); // Test 5
            Assert.AreSame("SpdUp", flowType, "Wrong flow type: Test 5");

            flowType = thisModel.GetFlowType(10, -10, 0, "DW", null, 0, false, 0, useValley); // Test 6
            Assert.AreSame("Uphill", flowType, "Wrong flow type: Test 6");

            flowType = thisModel.GetFlowType(10, 10, 0, "UW", null, 0, false, 0, useValley); // Test 7
            Assert.AreSame("SpdUp", flowType, "Wrong flow type: Test 7");

            flowType = thisModel.GetFlowType(10, 10, 0, "DW", null, 0, false, 0, useValley); // Test 8
            Assert.AreSame("Downhill", flowType, "Wrong flow type: Test 8");

            flowType = thisModel.GetFlowType(30, 10, 0, "UW", null, 0, false, 0, useValley); // Test 9
            Assert.AreSame("Uphill", flowType, "Wrong flow type: Test 9");

            flowType = thisModel.GetFlowType(10, 10, 0, "DW", null, 0, false, 0, useValley); // Test 10
            Assert.AreSame("Downhill", flowType, "Wrong flow type: Test 10");

            flowType = thisModel.GetFlowType(30, -10, 0, "UW", null, 0, false, 0, useValley); // Test 11
            Assert.AreSame("Uphill", flowType, "Wrong flow type: Test 11");

            flowType = thisModel.GetFlowType(30, -10, 0, "DW", null, 0, false, 0, useValley); // Test 12
            Assert.AreSame("Uphill", flowType, "Wrong flow type: Test 12");

            // Turbulent tests
            NodeCollection.Sep_Nodes[] This_Sep_Node = new NodeCollection.Sep_Nodes[1];
            This_Sep_Node[0].highNode = new Nodes();
            This_Sep_Node[0].highNode.AddExposure(4000, 1, 1, 16);
            This_Sep_Node[0].highNode.expo[0].expo[0] = 200; // UW expo
            This_Sep_Node[0].highNode.expo[0].expo[8] = 200; // DW expo

            flowType = thisModel.GetFlowType(-10, 10, 0, "UW", This_Sep_Node, 8, true, 0, useValley); // Test 13
            Assert.AreSame("Turbulent", flowType, "Wrong flow type: Test 13");

            flowType = thisModel.GetFlowType(200, 200, 0, "DW", null, 8, true, 0, useValley); // Test 14
            Assert.AreSame("Turbulent", flowType, "Wrong flow type: Test 14");

        }

        
    }
}
