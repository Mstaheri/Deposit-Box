using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbContextEF : DbContext
    {
        public DbContextEF(DbContextOptions<DbContextEF> Connection) : base(Connection)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<BankSafe> BankSafes { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<UserSharePrice> UserSharePrices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new BankSafeConfig());
            modelBuilder.ApplyConfiguration(new BankAccountConfig());
            modelBuilder.ApplyConfiguration(new UserSharePriceConfig());
            modelBuilder.ApplyConfiguration(new BankSafeTransactionsConfig());
            modelBuilder.ApplyConfiguration(new BankSafeDocumentConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
