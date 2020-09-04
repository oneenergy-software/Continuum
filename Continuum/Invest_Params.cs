using System;

namespace ContinuumNS
{
    /// <summary> Class that holds radius of investigation and inverse distance weighting exponent used in exposure and SRDH calculations. </summary>
    [Serializable()]
    public class Invest_Params
    {
        /// <summary> Radius of investigation  </summary>
        public int radius;
        /// <summary> Inverse distance weighting exponent </summary>
        public double exponent; 
                
    }
}
