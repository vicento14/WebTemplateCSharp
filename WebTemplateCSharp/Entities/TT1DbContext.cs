using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTemplateCSharp.Entities
{
    public class TT1DbContext : DbContext
    {
        public TT1DbContext() : base() { }
        public TT1DbContext(DbContextOptions<TT1DbContext> options) : base(options) { }
        public DbSet<TT1> TT1 { get; set; }
    }
}
