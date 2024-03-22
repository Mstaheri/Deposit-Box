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
            var modifiedEntries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified ||
                p.State == EntityState.Added ||
                p.State == EntityState.Deleted);
            foreach (var item in modifiedEntries)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());

                var insert = entityType.FindProperty("InsertTime");
                var update = entityType.FindProperty("UpdateTime");
                var Remove = entityType.FindProperty("RemoveTime");
                var isRemove = entityType.FindProperty("IsRemoved");
                if (item.State == EntityState.Added && insert != null)
                {
                    item.Property("InsertTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Modified && update != null)
                {
                    item.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Deleted && Remove != null && isRemove != null)
                {
                    item.Property("RemoveTime").CurrentValue = DateTime.Now;
                    item.Property("IsRemoved").CurrentValue = true;
                    item.State= EntityState.Modified;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var entityTipy in modelBuilder.Model.GetEntityTypes())
            {
                if (entityTipy.ClrType.GetCustomAttributes(typeof(AudiTableAttribute), true).Length > 0)
                {
                    modelBuilder.Entity(entityTipy.Name).Property<DateTime>("InsertTime");
                    modelBuilder.Entity(entityTipy.Name).Property<DateTime?>("UpdateTime");
                    modelBuilder.Entity(entityTipy.Name).Property<DateTime?>("RemoveTime");
                    modelBuilder.Entity(entityTipy.Name).Property<bool>("IsRemoved");
                }
            }
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextEF).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
