using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public class Node_table
    {
        public int Id { get; set; }
        public double UTMX { get; set; }
        public double UTMY { get; set; }
        public double elev { get; set; }
      
         public virtual ICollection<Expo_table> expo { get; set; } = new HashSet<Expo_table>();
         public virtual ICollection<GridStat_table> GridStats { get; set; } = new HashSet<GridStat_table>();
                         
    }
}
