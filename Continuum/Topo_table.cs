using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public partial class Topo_table
    {
        public int Id { get; set; } // Each entry represents topo data along a given UTMX. The topo min value is used to find Id for each UTMX      
        public byte[] Elevs { get; set; }   
    }
}
