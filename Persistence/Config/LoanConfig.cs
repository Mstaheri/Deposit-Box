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
    public class LoanConfig : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.HasKey(p => p.Code);

            builder.HasMany(p => p.LoanTransactions)
                .WithOne(p => p.Loan)
                .HasForeignKey(p => p.CodeLoan)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.LoanDocuments)
                .WithOne(p => p.Loan)
                .HasForeignKey(p => p.CodeLoan)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.NameBankSafe)
                .HasConversion(nameBankSafe => nameBankSafe.Value, value => new Name(value))
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.LastName)
               .HasMaxLength(50)
               .IsUnicode(true)
               .IsRequired(true);

            builder.Property(p => p.NumberOfInstallments)
               .HasMaxLength(4)
               .IsRequired(true);

            builder.Property(p => p.Amount)
               .HasMaxLength(12)
               .IsRequired(true);

            builder.Property(p => p.Wage)
               .HasMaxLength(2)
               .IsRequired(true);
        }
    }
}
