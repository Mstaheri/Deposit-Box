using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankAccounts.Commands.DeleteBankAccount
{
    public record DeleteBankAccountCommand :IRequest<OperationResult>
    {
        public required string AccountNumber { get; init; }
    }
}
