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
            builder.HasKey(p => p.Code);
        }
    }
}
