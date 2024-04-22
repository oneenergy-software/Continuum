using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ContinuumNS
{
    public partial class Continuum_EDMContainer_v1_1 : DbContext
    {
        public Continuum_EDMContainer_v1_1(string connectionString) : base(connectionString)
        {
        }
        public DbSet<Node_table> Node_table { get; set; }
        public DbSet<Expo_table> Expo_table { get; set; }
        public DbSet<GridStat_table> GridStat_table { get; set; }        
        public DbSet<Topo_table> Topo_table { get; set; }
        public DbSet<LandCover_table> LandCover_table { get; set; }
        public DbSet<Anem_table> Anem_table { get; set; }
        public DbSet<Vane_table> Vane_table { get; set; }
        public DbSet<Temp_table> Temp_table { get; set; }
        public DbSet<MERRA_Node_table> MERRA_Node_table { get; set; }        
        
    }
}
