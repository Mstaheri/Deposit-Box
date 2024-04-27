using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankAccounts.Commands.UpdateBankAccount
{
    public record UpdateBankAccountCommand : IRequest<OperationResult>
    {
        public required string AccountNumber { get; init; }
        public required string UserName { get; init; }
        public required string AccountName { get; init; }
        public required string BankName { get; init; }
        public string Description { get; init; }
    }
}
