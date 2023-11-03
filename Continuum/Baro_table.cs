using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public partial class Baro_table
    {
        public int Id { get; set; }
        public string metName { get; set; }
        public double height { get; set; }
        public byte[] baro { get; set; }
    }
}
