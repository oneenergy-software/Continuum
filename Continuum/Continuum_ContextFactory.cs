using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinuumNS
{
    public class Continuum_ContextFactory : IDbContextFactory<Continuum_EDMContainer>
    {
        NodeCollection nodeList;
        string savedFilename = "";

        public Continuum_ContextFactory(NodeCollection nodeList, string savedFile)
        {
            this.nodeList = nodeList;
            this.savedFilename = savedFile;
        }

        public Continuum_EDMContainer Create()
        {
            string connectionString = nodeList.GetDB_ConnectionString(savedFilename);
            return new Continuum_EDMContainer(connectionString);
        }
    }
}
