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
using System.Xml.Linq;

namespace Application.Services.BankSafes.Queries.GetBankSafe
{
    public class GetBankSafeQueryHandler
        : IRequestHandler<GetBankSafeQuery, OperationResult<BankSafe>>
    {
        private readonly IBankSafeRepositorie _bankSafeRepositorie;
        private readonly ILogger<GetBankSafeQueryHandler> _logger;
        public GetBankSafeQueryHandler(IBankSafeRepositorie bankSafeRepositorie
            , ILogger<GetBankSafeQueryHandler> logger)
        {
            _bankSafeRepositorie = bankSafeRepositorie;
            _logger = logger;
        }

        public async Task<OperationResult<BankSafe>> Handle(GetBankSafeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bankSafeRepositorie.GetAsync(request.Name, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetBankSafeQueryHandler)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<BankSafe>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<BankSafe>(false, ex.Message, null);
            }
        }
    }
}
