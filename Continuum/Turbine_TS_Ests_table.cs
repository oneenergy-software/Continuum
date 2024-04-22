using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public class Turbine_TS_Ests_table
    {
        public int Id { get; set; }
        public string turbName { get; set; }

        public string wakeModel { get; set; }

        public string powerCurve { get; set; }

        public byte[] tsData { get; set; }
    }
}
