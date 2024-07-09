using Application.Services.BankSafes.Queries.GetAllBankSafe;
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

namespace Application.Services.Loan.Queries.GetAllLoan
{
    public class GetAllLoanQueryHandler :
        IRequestHandler<GetAllLoanQuery , OperationResult<List<Domain.Entity.Loan>>>
    {
        private readonly ILoanRepositorie _loanRepositorie;
        private readonly ILogger<GetAllLoanQueryHandler> _logger;
        public GetAllLoanQueryHandler(ILoanRepositorie loanRepositorie
            , ILogger<GetAllLoanQueryHandler> logger)
        {
            _loanRepositorie = loanRepositorie;
            _logger = logger;
        }

        public async Task<OperationResult<List<Domain.Entity.Loan>>> Handle(GetAllLoanQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _loanRepositorie.GetAllAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllLoanQueryHandler)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<List<Domain.Entity.Loan>>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<List<Domain.Entity.Loan>>(false, ex.Message, null);
            }
        }
    }
}
