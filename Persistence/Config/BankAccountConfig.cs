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
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(p => p.AccountNumber);

            builder.HasMany(p => p.BankSafeTransactions)
                .WithOne(p => p.BankAccount)
                .HasForeignKey(p => p.AccountNumber);

            builder.HasMany(p => p.BankSafeDocuments)
                .WithOne(p => p.BankAccount)
                .HasForeignKey(p => p.AccountNumber);
        }
    }
}
