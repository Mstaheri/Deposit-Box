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

namespace Application.Services.BankSafeDocuments.Command.AddBankSafeDocuments
{
    public class AddBankSafeDocumentsCommandHandler
        : IRequestHandler<AddBankSafeDocumentsCommand, OperationResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankSafeDocumentRepositorie _bankSafeDocumentRepositorie;
        private readonly ILogger<AddBankSafeDocumentsCommandHandler> _Logger;
        public AddBankSafeDocumentsCommandHandler(IUnitOfWork unitOfWork,
            IBankSafeDocumentRepositorie bankSafeDocumentRepositorie,
            ILogger<AddBankSafeDocumentsCommandHandler> Logger)

        {
            _unitOfWork = unitOfWork;
            _bankSafeDocumentRepositorie = bankSafeDocumentRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult<Guid>> Handle(AddBankSafeDocumentsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                 var bankSafeDocument = new BankSafeDocument(
                    request.NameBankSafe,
                    request.AccountNumber,
                    request.RegistrationDate,
                    request.DueDate,
                    request.Deposit,
                    request.Withdrawal,
                    request.Situation
                    );

                await _bankSafeDocumentRepositorie.AddAsync(bankSafeDocument, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , bankSafeDocument.Code
                        , nameof(AddBankSafeDocumentsCommandHandler));
                _Logger.LogInformation(message);
                return new OperationResult<Guid>(true, null , bankSafeDocument.Code);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<Guid>(false, ex.Message , new Guid());
            }
        }
    }
}
