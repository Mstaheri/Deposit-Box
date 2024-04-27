using Domain.Exceptions;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafes.Commands.AddBankSafe
{
    public record AddBankSafeCommand : IRequest<OperationResult>
    {
        public required string Name { get; init; }
        public required decimal SharePrice { get; init; }
    }
}
