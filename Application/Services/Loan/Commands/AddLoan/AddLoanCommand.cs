using Domain.Exceptions;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Loan.Commands.AddLoan
{
    public record AddLoanCommand : IRequest<OperationResult<Guid>>
    {
        public required string NameBankSafe { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required int NumberOfInstallments { get; init; }
        public required decimal Amount { get; init; }
        public required int Wage { get; init; }
    }
    
}
