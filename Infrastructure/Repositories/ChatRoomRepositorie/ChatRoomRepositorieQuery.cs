using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories.IChatRoomRepositorie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ChatRoomRepositorie
{
    public class ChatRoomRepositorieQuery : IChatRoomRepositorieQuery
    {
        public ChatRoomRepositorieQuery(IUnitOfWork unitOfWork)
        {
            _chatRoom = unitOfWork.Set<ChatRoom>();
        }
        private readonly DbSet<ChatRoom> _chatRoom;
        public async Task<ChatRoom> GetChatRoomByConnectionId(string connectionId,
            CancellationToken cancellationToken)
        {
            var result = await _chatRoom.FindAsync(connectionId, cancellationToken);
            return result;
        }
    }
}
