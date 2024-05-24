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

namespace Application.Services.BankSafeDocuments.Queries.GetAllBankSafeDocuments
{
    public class GetAllBankSafeDocumentsQueryHandler
        : IRequestHandler<GetAllBankSafeDocumentsQuery, OperationResult<List<BankSafeDocument>>>
    {
        private readonly IBankSafeDocumentRepositorie _bankSafeDocumentRepositorie;
        private readonly ILogger<GetAllBankSafeDocumentsQueryHandler> _Logger;
        public GetAllBankSafeDocumentsQueryHandler(
            IBankSafeDocumentRepositorie bankSafeDocumentRepositorie,
            ILogger<GetAllBankSafeDocumentsQueryHandler> Logger)

        {
            _bankSafeDocumentRepositorie = bankSafeDocumentRepositorie;
            _Logger = Logger;
        }

        public async Task<OperationResult<List<BankSafeDocument>>> Handle(GetAllBankSafeDocumentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bankSafeDocumentRepositorie.GetAllAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllBankSafeDocumentsQueryHandler)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<List<BankSafeDocument>>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<List<BankSafeDocument>>(false, ex.Message, null);
            }
        }
    }
}
