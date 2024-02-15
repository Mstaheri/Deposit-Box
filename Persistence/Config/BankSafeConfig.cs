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

            builder.HasMany(p => p.UserSharePrices)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe);

            builder.HasMany(p => p.BankSafeTransactions)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe);

            builder.HasMany(p => p.BankSafeDocuments)
                .WithOne(p => p.BankSafe)
                .HasForeignKey(p => p.NameBankSafe);
        }
    }
}
