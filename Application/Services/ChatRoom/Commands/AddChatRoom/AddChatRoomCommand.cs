using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ChatRoom.Commands.AddChatRoom
{
    public record AddChatRoomCommand : IRequest<OperationResult<Guid>>
    {
        public required string ConnectionId { get; init; }
    }
}
