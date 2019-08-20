using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class Wake_Model
    {
        public int wakeModelType;   // 0 = Eddy Viscosity, 1 = DAWM Eddy Viscosity, 3 = Jensen
        public double horizWakeExp;   // Horizontal wake expansion angle
                                       //  public Vert_Wake_Exp  double
        public double ambTI;   // Average ambient TI
        public TurbineCollection.PowerCurve powerCurve;   // power and Thrust curves of turbine causing wake
        public double wakeRechargeRate;   // Rate at which wake is recharged (this equation needs to be revisited)
        public double wakeRechargeExp;   // distance weighting exponent in wake recharge rate
        public string comboMethod;   // Linear (Sum of Delta Vs), RSS (Root Sum Square of Delta Vs), Max (Max of Delta Vs), Geometric (Product of Delta Vs ^ 1/N)
                                      // Avg Lin&RSS, Avg Lin&Max, Avg Lin&Geo, Avg RSS&Max, Avg RSS&Geo, Avg Max&Geo

        // Used only in DAWM
        public double DW_Spacing;   // Downwind spacing (RDs) used in DAWM
        public double CW_Spacing;   // Crosswind spacing (RDs) used in DAWM
        public double ambRough;   // Average surface roughness used in DAWM

        // Used only in Jensen model
        public double wakeDecayConst = 0.075; // Wake decay constant used in Jensen model. Default for onshore = 0.075
    }
}
