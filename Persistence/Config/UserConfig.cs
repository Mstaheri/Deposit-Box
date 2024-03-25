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
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

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
                .HasConversion(firstName => firstName.Value, value => new Name(value))
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.LastName)
                .HasConversion(lastName => lastName.Value, value => new Name(value))
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.PhoneNumber)
                .HasConversion(phoneNumber => phoneNumber.Value, value => new PhoneNumber(value))
                .HasMaxLength(11)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.NationalIDNumber)
                .HasConversion(nationalIDNumber => nationalIDNumber.Value, value => new NationalIDNumber(value))
                .HasMaxLength(10)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.UserName)
                .HasConversion(userName => userName.Value, value => new UserName(value))
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(true);

            builder.Property(p => p.Password)
                .HasConversion(password => password.Value, value => new Password(value))
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired(true);

            
        }
    }
}
