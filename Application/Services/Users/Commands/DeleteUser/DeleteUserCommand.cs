using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Users.Commands.DeleteUser
{
    public record DeleteUserCommand : IRequest<OperationResult>
    {
        public required string UserName { get; init; }
    }
}
