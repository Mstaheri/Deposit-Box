using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Config
{
    internal class BankSafeConfig : IEntityTypeConfiguration<BankSafe>
    {
        public void Configure(EntityTypeBuilder<BankSafe> builder)
        {
            builder.HasKey(p => p.Name);

            builder.HasMany(p => p.UserAndNumberOfShares)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.BankSafeTransactions)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.BankSafeDocuments)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.loans)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.LoanTransactions)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.LoanDocuments)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
