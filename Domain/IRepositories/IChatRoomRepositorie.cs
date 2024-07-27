using Domain.Entity;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IChatRoomRepositorie
    {
        ValueTask AddAsync(ChatRoom chatRoom);
        Task<ChatRoom> GetChatRoomByConnectionId(string connectionId , CancellationToken cancellationToken);
    }
}
