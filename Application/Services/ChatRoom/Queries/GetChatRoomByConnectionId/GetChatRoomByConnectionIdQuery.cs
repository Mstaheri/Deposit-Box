using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ChatRoom.Queries.GetChatRoomByConnectionId
{
    public record GetChatRoomByConnectionIdQuery : IRequest<Guid>
    {
        public required string ConnectionId { get; init; }
    }
}
