using Domain.Entity;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Config
{
    public class BankSafeDocumentConfig : IEntityTypeConfiguration<BankSafeDocument>
    {
        public void Configure(EntityTypeBuilder<BankSafeDocument> builder)
        {
            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.HasKey(p => p.Code);

            builder.Property(p => p.NameBankSafe)
                .HasConversion(nameBankSafe => nameBankSafe.Value, value => new Name(value))
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.AccountNumber)
                .HasConversion(accountNumber => accountNumber.Value, value => new AccountNumber(value))
                .HasMaxLength(16)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.RegistrationDate)
                .HasMaxLength(10)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.DueDate)
                .HasMaxLength(10)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.Deposit)
                .HasMaxLength(12)
                .IsRequired(true);

            builder.Property(p => p.Withdrawal)
                .HasMaxLength(12)
                .IsRequired(true);
        }
    }
}
