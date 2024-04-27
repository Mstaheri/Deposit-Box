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

namespace Application.Services.BankAccounts.Queries.GetAllBankAccount
{
    public class GetAllBankAccountQueryHandler
        : IRequestHandler<GetAllBankAccountQuery, OperationResult<List<BankAccount>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAccountRepositorie _bankAccountRepositorie;
        private readonly ILogger<GetAllBankAccountQueryHandler> _Logger;
        public GetAllBankAccountQueryHandler(IUnitOfWork unitOfWork,
            IBankAccountRepositorie bankAccountRepositorie,
            ILogger<GetAllBankAccountQueryHandler> Logger)

        {
            _unitOfWork = unitOfWork;
            _bankAccountRepositorie = bankAccountRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult<List<BankAccount>>> Handle(GetAllBankAccountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bankAccountRepositorie.GetAllAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllBankAccountQueryHandler)
                        , "");
                _Logger.LogInformation(message, cancellationToken);
                return new OperationResult<List<BankAccount>>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message, cancellationToken);
                return new OperationResult<List<BankAccount>>(false, ex.Message, null);
            }
        }
    }
}
