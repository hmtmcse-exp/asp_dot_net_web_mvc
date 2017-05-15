using isms.Models.system;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace isms.Data
{
    public class ISMSContext : DbContext
    {
        public ISMSContext(DbContextOptions<ISMSContext> options) : base(options)
        {
        }

        public DbSet<Operator> Operator { get; set; }
    }
}
