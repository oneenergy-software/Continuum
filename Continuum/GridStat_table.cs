using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public partial class GridStat_table
    {
        public int Id { get; set; }
        public int radius { get; set; }
        public byte[] P10_UW { get; set; }
        public byte[] P10_DW { get; set; }
        public int Node_tableId { get; set; }

        public virtual Node_table Node_table { get; set; }
    }
}
