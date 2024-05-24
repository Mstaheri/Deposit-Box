using Domain.Entity;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Users.Queries.GetByUserNameAndPassword
{
    public record GetByUserNameAndPasswordQuery : IRequest<OperationResult<User>>
    {
        public required string UserName { get; init; }
        public required string Password { get; init; }
    }
}
