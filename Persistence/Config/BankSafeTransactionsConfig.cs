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
    internal class BankSafeTransactionsConfig : IEntityTypeConfiguration<BankSafeTransactions>
    {
        public void Configure(EntityTypeBuilder<BankSafeTransactions> builder)
        {
            builder.HasKey(p => p.CodeTransactions);
        }
    }
}
