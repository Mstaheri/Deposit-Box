using Domain.Entity;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Users.Queries.GetAllUser
{
    public record GetAllUserQuery : IRequest<OperationResult<List<User>>>
    {

    }
}
