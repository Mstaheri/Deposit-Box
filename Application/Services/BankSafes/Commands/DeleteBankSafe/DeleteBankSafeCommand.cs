using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafes.Commands.DeleteBankSafe
{
    public record DeleteBankSafeCommand : IRequest<OperationResult>
    {
        public required string Name { get; init; }
    }
}
