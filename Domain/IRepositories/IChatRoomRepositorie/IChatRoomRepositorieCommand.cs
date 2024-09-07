using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IChatRoomRepositorie
{
    public interface IChatRoomRepositorieCommand
    {
        ValueTask AddAsync(ChatRoom chatRoom);
    }
}
