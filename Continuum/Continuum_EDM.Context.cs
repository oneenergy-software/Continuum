using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ContinuumNS
{
    public partial class Continuum_EDMContainer : DbContext
    {
        public Continuum_EDMContainer(string connectionString) : base(connectionString)
        {
        }
        public DbSet<Node_table> Node_table { get; set; }
        public DbSet<Expo_table> Expo_table { get; set; }
        public DbSet<GridStat_table> GridStat_table { get; set; }        
        public DbSet<Topo_table> Topo_table { get; set; }
        public DbSet<LandCover_table> LandCover_table { get; set; }
    }
}
