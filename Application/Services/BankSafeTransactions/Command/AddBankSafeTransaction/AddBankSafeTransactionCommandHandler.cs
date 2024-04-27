using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafeTransactions.Command.AddBankSafeTransaction
{
    public class AddBankSafeTransactionCommandHandler
        : IRequestHandler<AddBankSafeTransactionCommand, OperationResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankSafeTransactionsRepositorie _bankSafeTransactionsRepositorie;
        private readonly ILogger<AddBankSafeTransactionCommandHandler> _Logger;
        public AddBankSafeTransactionCommandHandler(IUnitOfWork unitOfWork,
            IBankSafeTransactionsRepositorie bankSafeTransactionsRepositorie,
            ILogger<AddBankSafeTransactionCommandHandler> Logger)

        {
            _unitOfWork = unitOfWork;
            _bankSafeTransactionsRepositorie = bankSafeTransactionsRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult<Guid>> Handle(AddBankSafeTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bankSafeTransaction = new BankSafeTransaction(
                    request.NameBankSafe,
                    request.AccountNumber,
                    request.Deposit,
                    request.Withdrawal
                    );
                await _bankSafeTransactionsRepositorie.AddAsync(bankSafeTransaction, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , bankSafeTransaction.Code
                        , nameof(AddBankSafeTransactionCommandHandler));
                _Logger.LogInformation(message);
                return new OperationResult<Guid>(true, null , bankSafeTransaction.Code);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<Guid>(false, ex.Message , new Guid());
            }
        }
    }
}
