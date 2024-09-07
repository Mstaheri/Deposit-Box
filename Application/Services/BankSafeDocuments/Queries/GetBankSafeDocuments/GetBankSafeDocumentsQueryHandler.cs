using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.IRepositories.IBankSafeDocumentRepositorie;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafeDocuments.Query.GetBankSafeDocuments
{
    public class GetBankSafeDocumentsQueryHandler
        : IRequestHandler<GetBankSafeDocumentsQuery, OperationResult<BankSafeDocument>>
    {
        private readonly IBankSafeDocumentRepositorieQuery _bankSafeDocumentRepositorie;
        private readonly ILogger<GetBankSafeDocumentsQueryHandler> _Logger;
        public GetBankSafeDocumentsQueryHandler(
            IBankSafeDocumentRepositorieQuery bankSafeDocumentRepositorie,
            ILogger<GetBankSafeDocumentsQueryHandler> Logger)

        {
            _bankSafeDocumentRepositorie = bankSafeDocumentRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult<BankSafeDocument>> Handle(GetBankSafeDocumentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bankSafeDocumentRepositorie.GetAsync(request.Code, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetBankSafeDocumentsQueryHandler)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<BankSafeDocument>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<BankSafeDocument>(false, ex.Message, null);
            }
        }
    }
}
