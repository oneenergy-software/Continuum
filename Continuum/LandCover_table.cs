using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public partial class LandCover_table
    {
        public int Id { get; set; } // Each entry represents land cover data along a given UTMX. The land cover min value is used to find Id for each UTMX      
        public byte[] LandCover { get; set; }        
    }
}
