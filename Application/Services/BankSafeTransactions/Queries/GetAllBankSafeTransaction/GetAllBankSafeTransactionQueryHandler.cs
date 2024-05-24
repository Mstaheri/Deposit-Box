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

namespace Application.Services.BankSafeTransactions.Queries.GetAllBankSafeTransaction
{
    public class GetAllBankSafeTransactionQueryHandler
        : IRequestHandler<GetAllBankSafeTransactionQuery, OperationResult<List<BankSafeTransaction>>>
    {
        private readonly IBankSafeTransactionsRepositorie _bankSafeTransactionsRepositorie;
        private readonly ILogger<GetAllBankSafeTransactionQueryHandler> _Logger;
        public GetAllBankSafeTransactionQueryHandler(
            IBankSafeTransactionsRepositorie bankSafeTransactionsRepositorie,
            ILogger<GetAllBankSafeTransactionQueryHandler> Logger)

        {
            _bankSafeTransactionsRepositorie = bankSafeTransactionsRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult<List<BankSafeTransaction>>> Handle(GetAllBankSafeTransactionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bankSafeTransactionsRepositorie.GetAllAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllBankSafeTransactionQueryHandler)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<List<BankSafeTransaction>>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<List<BankSafeTransaction>>(false, ex.Message, null);
            }
        }
    }
}
