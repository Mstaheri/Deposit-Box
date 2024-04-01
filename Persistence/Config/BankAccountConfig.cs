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
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasQueryFilter(m => EF.Property<bool>(m , "IsRemoved") == false);

            builder.HasKey(p => p.AccountNumber);

            builder.HasMany(p => p.BankSafeTransactions)
                .WithOne(p => p.BankAccount)
                .HasForeignKey(p => p.AccountNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.BankSafeDocuments)
                .WithOne(p => p.BankAccount)
                .HasForeignKey(p => p.AccountNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.AccountNumber)
                .HasConversion<AccountNumberConverter>()
                .HasMaxLength(16)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.UserName)
                .HasConversion<UserNameConverter>()
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(true);

            builder.Property(p => p.AccountName)
                .HasConversion<NameConverter>()
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.BankName)
                .HasConversion<NameConverter>()
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.Description)
                .HasMaxLength(500)
                .IsUnicode(true)
                .IsRequired(false);


        }
    }
}
