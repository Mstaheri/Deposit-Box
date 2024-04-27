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

namespace Application.Services.BankAccounts.Commands.DeleteBankAccount
{
    public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAccountRepositorie _bankAccountRepositorie;
        private readonly ILogger<DeleteBankAccountCommandHandler> _Logger;
        public DeleteBankAccountCommandHandler(IUnitOfWork unitOfWork,
            IBankAccountRepositorie bankAccountRepositorie,
            ILogger<DeleteBankAccountCommandHandler> Logger)

        {
            _unitOfWork = unitOfWork;
            _bankAccountRepositorie = bankAccountRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _bankAccountRepositorie.DeleteAsync(request.AccountNumber, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , request.AccountNumber
                        , nameof(DeleteBankAccountCommandHandler));
                _Logger.LogInformation(message, cancellationToken);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message, cancellationToken);
                return new OperationResult(false, ex.Message);
            }
        }
    }
}
