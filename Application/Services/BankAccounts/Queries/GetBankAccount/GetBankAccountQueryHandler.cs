using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankAccounts.Queries.GetBankAccount
{
    public class GetBankAccountQueryHandler
        : IRequestHandler<GetBankAccountQuery, OperationResult<BankAccount>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAccountRepositorie _bankAccountRepositorie;
        private readonly ILogger<GetBankAccountQueryHandler> _Logger;
        public GetBankAccountQueryHandler(IUnitOfWork unitOfWork,
            IBankAccountRepositorie bankAccountRepositorie,
            ILogger<GetBankAccountQueryHandler> Logger)

        {
            _unitOfWork = unitOfWork;
            _bankAccountRepositorie = bankAccountRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult<BankAccount>> Handle(GetBankAccountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bankAccountRepositorie.GetAsync(request.AccountNumber, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetBankAccountQueryHandler)
                        , "");
                _Logger.LogInformation(message, cancellationToken);
                return new OperationResult<BankAccount>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message, cancellationToken);
                return new OperationResult<BankAccount>(false, ex.Message, null);
            }
        }
    }
}
