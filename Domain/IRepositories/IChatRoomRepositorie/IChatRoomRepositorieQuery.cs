using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IChatRoomRepositorie
{
    public interface IChatRoomRepositorieQuery
    {
        Task<ChatRoom> GetChatRoomByConnectionId(string connectionId, CancellationToken cancellationToken);
    }
}
