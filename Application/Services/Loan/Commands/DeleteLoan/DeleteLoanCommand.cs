using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Loan.Commands.DeleteLoan
{
    public record DeleteLoanCommand : IRequest<OperationResult>
    {
        public required Guid Code { get; init; }
    }
}
