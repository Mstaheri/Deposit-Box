using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserAndNumberOfShares.Commands.DeleteUserAndNumberOfShare
{
    public record DeleteUserAndNumberOfShareCommand
        : IRequest<OperationResult>
    {
        public required string NameBankSafe { get; init; }
        public required string UserName { get; init; }
    }
}
