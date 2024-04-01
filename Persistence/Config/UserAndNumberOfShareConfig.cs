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
    public class UserAndNumberOfShareConfig : IEntityTypeConfiguration<UserAndNumberOfShare>
    {
        public void Configure(EntityTypeBuilder<UserAndNumberOfShare> builder)
        {
            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.HasKey(p => new { p.UserName, p.NameBankSafe });

            builder.Property(p => p.NameBankSafe)
               .HasConversion<NameConverter>()
               .HasMaxLength(50)
               .IsUnicode(true)
               .IsRequired(true);

            builder.Property(p => p.UserName)
               .HasConversion<UserNameConverter>()
               .HasMaxLength(50)
               .IsUnicode(false)
               .IsRequired(true);

            builder.Property(p => p.NumberOfShares)
               .HasConversion<NumberConverter>()
               .HasMaxLength(4)
               .IsRequired(true);
        }
    }
}
