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
    public class UserSharePriceConfig : IEntityTypeConfiguration<UserAndNumberOfShare>
    {
        public void Configure(EntityTypeBuilder<UserAndNumberOfShare> builder)
        {
            builder.HasKey(p => new { p.UserName, p.NameBankSafe });
        }
    }
}
