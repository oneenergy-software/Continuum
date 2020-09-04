using System;

namespace ContinuumNS
{
    /// <summary> Class that holds a list of Invest_Params (i.e. radius of investigation and inverse distance exponent). Default is four Invest_Params with 
    /// inverse distance exponent = 1 and radius of investigation = 4000, 6000, 8000, and 10,000 m.</summary>
    [Serializable()]
    public class InvestCollection
    {
        /// <summary> List of radii and exponents </summary>
        public Invest_Params[] investItem = new Invest_Params[4];

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> Returns the number of Invest_Params </summary>
        public int ThisCount
        {
            get { if (investItem == null)
                    return 0;
            else
                return investItem.Length; }
        }
        
        /// <summary> Returns maximum radius of investigation in list. </summary>        
        public int GetMaxRadius()
        {
            // Returns the maximum radius of investigation
            int maxRadius = 0;

            for (int i = 0; i < ThisCount; i++)
            {
                if (investItem[i].radius > maxRadius)
                    maxRadius = investItem[i].radius;                
            }

            return maxRadius;            
        }

        /// <summary> Creates four Invest_Params (4000, 6000, 8000, 10000 m with exponent = 1) when initialized. </summary>   
        public void New()
        {      
            int thisInd = 0;
            double exp;

            for (int i = 4000; i <= 10000; i = i + 2000)
            {
                exp = 1.0f;
                investItem[thisInd] = new Invest_Params();
                investItem[thisInd].radius = i;
                investItem[thisInd].exponent = exp;
                thisInd++;
            }
        }

        /// <summary> Returns index of specified radius of investigation. </summary>  
        public int GetRadiusInd(int thisRadius)
        {
            int radiusIndex = 0;

            for (int i = 0; i < ThisCount; i++)
                if (investItem[i].radius == thisRadius)
                    radiusIndex = i;

            return radiusIndex;
        }
    }
}
