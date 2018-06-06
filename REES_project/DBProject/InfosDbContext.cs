using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBProject
{
    public class InfosDbContext : DbContext
    {
       public InfosDbContext() : base("name=InfosContext")
        {

        }
        public DbSet<Infos> Infos { get; set; }
    }
}
