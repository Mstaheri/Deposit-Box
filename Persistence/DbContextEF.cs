using Application.UnitOfWork;
using Domain.Attributes;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Persistence
{
    public class DbContextEF : DbContext , IUnitOfWork
    {
        public DbContextEF(DbContextOptions<DbContextEF> Connection) : base(Connection)
        {
           
        }
        public DbSet<User> Users { get; set; }
        public DbSet<BankSafe> BankSafes { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankSafeTransactions> BankSafeTransactions { get; set; }
        public DbSet<BankSafeDocument> BankSafeDocuments { get; set; }
        public DbSet<UserAndNumberOfShare> UserAndNumberOfShares { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanTransactions> LoanTransactions { get; set; }
        public DbSet<LoanDocument> LoanDocuments { get; set; }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //foreach (var entityTipy in modelBuilder.Model.GetEntityTypes())
            //{
            //    if (entityTipy.ClrType.GetCustomAttributes(typeof(AudiTableAttribute), true).Length > 0)
            //    {
            //        modelBuilder.Entity(entityTipy.Name).Property<DateTime>("InsertTime");
            //        modelBuilder.Entity(entityTipy.Name).Property<DateTime>("UpdateTime");
            //        modelBuilder.Entity(entityTipy.Name).Property<DateTime>("RemoveTime");
            //        modelBuilder.Entity(entityTipy.Name).Property<bool>("IsRemoved");
            //    }
            //}
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new BankSafeConfig());
            modelBuilder.ApplyConfiguration(new BankAccountConfig());
            modelBuilder.ApplyConfiguration(new UserSharePriceConfig());
            modelBuilder.ApplyConfiguration(new BankSafeTransactionsConfig());
            modelBuilder.ApplyConfiguration(new BankSafeDocumentConfig());
            modelBuilder.ApplyConfiguration(new LoanConfig());
            modelBuilder.ApplyConfiguration(new LoanTransactionsConfig());
            modelBuilder.ApplyConfiguration(new LoanDocumentConfig());
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
