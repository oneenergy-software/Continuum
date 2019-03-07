using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    [Serializable()]
    public class InvestCollection
    {
        public Invest_Params[] investItem = new Invest_Params[4];

        public int ThisCount
        {
            get { if (investItem == null)
                    return 0;
            else
                return investItem.Length; }
        }

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

        public void New()
        {
            // Creates four Invest_Params (4000, 6000, 8000, 10000 m with exponent = 1) when initialized
            //Default values
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

        public void RestoreDefaults()
        {
            investItem = new Invest_Params[4];
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


        public void ClearAllInvest()
        {
            investItem = null;
        }               

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
