using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public partial class All_Expos
    {
        public int Id { get; set; }
        public double UTMX { get; set; }
        public double UTMY { get; set; }
        public byte[] Expo_Array { get; set; }
        public byte[] ExpoDist_Array { get; set; }
        public int Radius { get; set; }
        public double Exponent { get; set; }
    }
}
