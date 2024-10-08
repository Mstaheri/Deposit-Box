﻿using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.IRepositories.IBankSafeTransactionsRepositorie;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafeTransactions.Queries.GetBankSafeTransaction
{
    public class GetBankSafeTransactionCommandHandler
        : IRequestHandler<GetBankSafeTransactionCommand, OperationResult<BankSafeTransaction>>
    {
        private readonly IBankSafeTransactionsRepositorieQuery _bankSafeTransactionsRepositorie;
        private readonly ILogger<GetBankSafeTransactionCommandHandler> _Logger;
        public GetBankSafeTransactionCommandHandler(
            IBankSafeTransactionsRepositorieQuery bankSafeTransactionsRepositorie,
            ILogger<GetBankSafeTransactionCommandHandler> Logger)

        {
            _bankSafeTransactionsRepositorie = bankSafeTransactionsRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult<BankSafeTransaction>> Handle(GetBankSafeTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bankSafeTransactionsRepositorie.GetAsync(request.Code, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetBankSafeTransactionCommandHandler)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<BankSafeTransaction>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<BankSafeTransaction>(false, ex.Message, null);
            }
        }
    }
}
