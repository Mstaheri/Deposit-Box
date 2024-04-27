using Domain.Entity;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafes.Queries.GetBankSafe
{
    public record GetBankSafeQuery : IRequest<OperationResult<BankSafe>>
    {
        public required string Name { get; init; }
    }
}
