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
    public class BankSafeDocumentConfig : IEntityTypeConfiguration<BankSafeDocument>
    {
        public void Configure(EntityTypeBuilder<BankSafeDocument> builder)
        {
            builder.HasKey(p => p.CodeDocuments);
        }
    }
}
