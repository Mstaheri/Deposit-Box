using Application.Services.Loan.Queries.GetAllLoan;
using Domain.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Loan.Queries.GetByCodeLoan
{
    public class GetByCodeLoanQueryHandler :
        IRequestHandler<GetByCodeLoanQuery, OperationResult<Domain.Entity.Loan>>
    {
        private readonly ILoanRepositorie _loanRepositorie;
        private readonly ILogger<GetByCodeLoanQueryHandler> _logger;
        public GetByCodeLoanQueryHandler(ILoanRepositorie loanRepositorie
            , ILogger<GetByCodeLoanQueryHandler> logger)
        {
            _loanRepositorie = loanRepositorie;
            _logger = logger;
        }
        public async Task<OperationResult<Domain.Entity.Loan>> Handle(GetByCodeLoanQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _loanRepositorie.GetByCodeAsync(request.Code, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllLoanQueryHandler)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<Domain.Entity.Loan>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<Domain.Entity.Loan>(false, ex.Message, null);
            }
        }
    }
}
