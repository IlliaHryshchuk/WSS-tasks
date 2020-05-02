using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WSS_first_task
{
    class SnailContext : DbContext
    {
        public SnailContext() : base("DbConnection")
        {
        }

        public DbSet<SnailMatrix> Snails { get; set; }
    }
}
