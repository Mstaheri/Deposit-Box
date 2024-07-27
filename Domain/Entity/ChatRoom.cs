using Domain.Attributes;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class ChatRoom : IEntity
    {
        public ChatRoom(string connectionId)
        {
            Id = Guid.NewGuid();
            ConnectionId = connectionId;
        }
        public Guid Id { get; private set; }
        public string ConnectionId { get; private set; }
    }
}
