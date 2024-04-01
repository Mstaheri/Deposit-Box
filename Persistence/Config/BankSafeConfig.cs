using Domain.Entity;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Convertors;
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
            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.HasKey(p => p.Name);

            builder.HasMany(p => p.UserAndNumberOfShares)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.BankSafeTransactions)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.BankSafeDocuments)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.loans)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.LoanTransactions)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.LoanDocuments)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.Name)
                .HasConversion<NameConverter>()
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.SharePrice)
                .HasConversion<MoneyConverter>()
                .HasMaxLength(12)
                .IsRequired(true);
        }
    }
}
