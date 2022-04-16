using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

# nullable disable

namespace Domain
{
    public class KolmeoContext : DbContext
    {
        public KolmeoContext(DbContextOptions<KolmeoContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }
    }
}
