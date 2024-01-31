using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTemplateCSharp.Entities
{
    public class UserAccountsDbContext : DbContext
    {
        public UserAccountsDbContext() : base() { }
        public UserAccountsDbContext(DbContextOptions<UserAccountsDbContext> options) : base(options) { }
        public DbSet<UserAccounts> UserAccounts { get; set; }
    }
}
