using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafes.Commands.UpdateBankSafe
{
    public record UpdateBankSafeCommand : IRequest<OperationResult>
    {
        public required string Name { get; init; }
        public required decimal SharePrice { get; init; }
    }
}
