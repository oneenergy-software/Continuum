using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public partial class MapNode_table
    {
        public int Id { get; set; }
        public string Map_Name { get; set; }
        public double AvgWSEst { get; set; }
        public byte[] sectorWS { get; set; }
        public double grossAEP { get; set; }
        public bool Uses_Best { get; set; }
        public byte[] WSDist_Array { get; set; }
        public byte[] SectDist_Array { get; set; }
        public byte[] metsUsed { get; set; }
        public bool useSR { get; set; }
        public bool useFlowSep { get; set; }
        public int Node_tableId { get; set; }

        public virtual Node_table Node_table { get; set; }
        public virtual ICollection<MapWSEst_table> MapWSEst_table { get; set; } = new HashSet<MapWSEst_table>();
    }
}
