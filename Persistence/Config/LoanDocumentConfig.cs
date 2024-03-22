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
    public class LoanDocumentConfig : IEntityTypeConfiguration<LoanDocument>
    {
        public void Configure(EntityTypeBuilder<LoanDocument> builder)
        {
            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.HasKey(p => p.Code);

            builder.Property(p => p.NameBankSafe)
               .HasMaxLength(50)
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

            builder.Property(p => p.Amount)
                .HasMaxLength(12)
                .IsRequired(true);
        }
    }
}
