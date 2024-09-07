using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IChatRoomRepositorie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ChatRoomRepositorie
{
    public class ChatRoomRepositorieCommand : IChatRoomRepositorieCommand
    {
        public ChatRoomRepositorieCommand(IUnitOfWork unitOfWork)
        {
            _chatRoom = unitOfWork.Set<ChatRoom>();
        }
        private readonly DbSet<ChatRoom> _chatRoom;
        public async ValueTask AddAsync(ChatRoom chatRoom)
        {
            var result = await _chatRoom.FirstOrDefaultAsync(p => p.ConnectionId == chatRoom.ConnectionId);
            if (result == null)
            {
                _chatRoom.Add(chatRoom);
            }
            else
            {
                string message = string.Format(ConstMessages.Duplicate, chatRoom.ConnectionId);
                throw new Exception(message);
            }

        }
    }
}
