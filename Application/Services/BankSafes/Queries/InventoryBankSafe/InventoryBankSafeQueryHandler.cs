using Application.UnitOfWork;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.IRepositories.IBankSafeRepositorie;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafes.Queries.InventoryBankSafe
{
    public class InventoryBankSafeQueryHandler
        : IRequestHandler<InventoryBankSafeQuery, OperationResult<decimal>>
    {
        private readonly IBankSafeRepositorieQuery _bankSafeRepositorie;
        private readonly ILogger<InventoryBankSafeQueryHandler> _logger;
        public InventoryBankSafeQueryHandler(IBankSafeRepositorieQuery bankSafeRepositorie
            , ILogger<InventoryBankSafeQueryHandler> logger)
        {
            _bankSafeRepositorie = bankSafeRepositorie;
            _logger = logger;
        }
        public async Task<OperationResult<decimal>> Handle(InventoryBankSafeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bankSafeRepositorie.Inventory(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(InventoryBankSafeQueryHandler)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<decimal>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<decimal>(false, ex.Message, -1);
            }
        }
    }
}
