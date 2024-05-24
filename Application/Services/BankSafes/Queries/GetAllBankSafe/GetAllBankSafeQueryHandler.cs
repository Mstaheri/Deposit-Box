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

namespace Application.Services.BankSafes.Queries.GetAllBankSafe
{
    public class GetAllBankSafeQueryHandler
        : IRequestHandler<GetAllBankSafeQuery, OperationResult<List<BankSafe>>>
    {
        private readonly IBankSafeRepositorie _bankSafeRepositorie;
        private readonly ILogger<GetAllBankSafeQueryHandler> _logger;
        public GetAllBankSafeQueryHandler(IBankSafeRepositorie bankSafeRepositorie
            , ILogger<GetAllBankSafeQueryHandler> logger)
        {
            _bankSafeRepositorie = bankSafeRepositorie;
            _logger = logger;
        }
        public async Task<OperationResult<List<BankSafe>>> Handle(GetAllBankSafeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bankSafeRepositorie.GetAllAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllBankSafeQueryHandler)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<List<BankSafe>>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<List<BankSafe>>(false, ex.Message, null);
            }
        }
    }
}
