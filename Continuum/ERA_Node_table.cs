using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public partial class ERA_Node_table
    {
        public int Id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public byte[] eraData { get; set; }
    }
}
