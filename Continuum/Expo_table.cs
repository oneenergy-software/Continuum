using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public partial class Expo_table
    {
        public int Id { get; set; }
        public byte[] Expo_Array { get; set; }
        public byte[] ExpoDist_Array { get; set; }
        public int radius { get; set; }
        public double exponent { get; set; }        
        public byte[] UW_Cross_Grade { get; set; }
        public byte[] UW_ParallelGrade { get; set; }
        public byte[] SR_Array { get; set; }
        public byte[] DH_Array { get; set; }

        public int Node_tableId { get; set; }

        public virtual Node_table Node_table { get; set; }
    }
}
