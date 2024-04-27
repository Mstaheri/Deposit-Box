using Domain.Exceptions;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Users.Commands.AddUser
{
    public record AddUserCommand : IRequest<OperationResult>
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string PhoneNumber { get; init; }
        public required string NationalIDNumber { get; init; }
        public required string UserName { get; init; }
        public required string Password { get; init; }
    }
}
