using Domain.Exceptions;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserAndNumberOfShares.Commands.AddUserAndNumberOfShare
{
    public record AddUserAndNumberOfShareCommand : IRequest<OperationResult>
    {
        public required string NameBankSafe { get; init; }
        public required string UserName { get; init; }
        public required int NumberOfShares { get; init; }
    }
}
