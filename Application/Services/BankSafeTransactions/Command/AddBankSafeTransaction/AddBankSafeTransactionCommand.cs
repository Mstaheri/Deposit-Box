using Domain.Exceptions;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafeTransactions.Command.AddBankSafeTransaction
{
    public record AddBankSafeTransactionCommand
        :IRequest<OperationResult<Guid>>
    {
        public required string NameBankSafe { get; init; }
        public required string AccountNumber { get; init; }
        public required decimal Deposit { get; init; }
        public required decimal Withdrawal { get; init; }
    }
}
