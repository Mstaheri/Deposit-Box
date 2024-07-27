
using Domain.Entity;
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
    public class ChatRoomConfig : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(e => e.ConnectionId)
                .IsUnique();
            builder.Property(p => p.ConnectionId)
                .HasMaxLength(23)
                .IsUnicode(false)
                .IsRequired(true);
        }
    }
}
