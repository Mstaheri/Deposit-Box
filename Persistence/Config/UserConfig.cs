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
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.UserName);

            builder.HasMany(p => p.BankAccounts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.UserAndNumberOfShares)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.LastName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.NationalIDNumber)
                .HasMaxLength(10)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(true);

            builder.Property(p => p.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(true);
        }
    }
}
