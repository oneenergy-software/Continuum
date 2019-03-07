using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public partial class MapWSEst_table
    {
        public int Id { get; set; }
        public string PredMet { get; set; }
        public byte[] PathNodes { get; set; }
        public int radius { get; set; }
        public double WS { get; set; }
        public double WS_weight { get; set; }
        public byte[] sectorWS { get; set; }
        public bool elevDiffTooBig { get; set; }
        public bool expoDiffTooBig { get; set; }

        public int MapNode_tableId { get; set; }

        public virtual MapNode_table MapNode_table { get; set; }
    }
}
