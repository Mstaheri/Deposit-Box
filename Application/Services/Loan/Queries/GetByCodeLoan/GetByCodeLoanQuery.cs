using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Loan.Queries.GetByCodeLoan
{
    public record GetByCodeLoanQuery
        : IRequest<OperationResult<Domain.Entity.Loan>>
    {
        public required Guid Code { get; init; }
    }
}
