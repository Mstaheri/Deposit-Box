using Application.IRepositories;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
