using System;

namespace ContinuumNS
{
    /// <summary> Class that holds all information and settings of a wake model. </summary>
    [Serializable()]
    public class Wake_Model
    {
        /// <summary> Wake model type. 0 = Eddy Viscosity, 1 = DAWM Eddy Viscosity, 2 = Jensen </summary>
        public int wakeModelType;
        /// <summary> Horizontal wake expansion angle </summary>
        public double horizWakeExp;
        /// <summary> Average ambient turbulence intensity </summary>
        public double ambTI;
        /// <summary> Power and Thrust curves of turbine causing wake. </summary>
        public TurbineCollection.PowerCurve powerCurve;
        /// <summary> Rate at which wake is recharged (this equation needs to be revisited). </summary>
        public double wakeRechargeRate;
        /// <summary> Distance weighting exponent in wake recharge rate. </summary>
        public double wakeRechargeExp;
        /// <summary> Wake combination method: Linear (Sum of Delta Vs), RSS (Root Sum Square of Delta Vs), Max (Max of Delta Vs), Geometric (Product of Delta Vs ^ 1/N), Avg Lin and RSS, Avg Lin and Max, Avg Lin and Geo, Avg RSS and Max, Avg RSS and Geo, Avg Max and Geo </summary>
        public string comboMethod;

        // Used only in DAWM
        /// <summary> Downwind spacing (RDs) used in DAWM. Used only in DAWM. </summary>
        public double DW_Spacing;
        /// <summary> Crosswind spacing (RDs) used in DAWM. Used only in DAWM. </summary>
        public double CW_Spacing;
        /// <summary> Average surface roughness used in DAWM. Used only in DAWM. </summary>
        public double ambRough;
               
        /// <summary> Wake decay constant used in Jensen model. Default for onshore = 0.075. Used only in Jensen model </summary>
        public double wakeDecayConst = 0.075; // 
    }
}
