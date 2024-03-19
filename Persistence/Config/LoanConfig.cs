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
    public class LoanConfig : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(p => p.Code);

            builder.HasMany(p => p.LoanTransactions)
                .WithOne(p => p.Loan)
                .HasForeignKey(p => p.CodeLoan)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.LoanDocuments)
                .WithOne(p => p.Loan)
                .HasForeignKey(p => p.CodeLoan)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
