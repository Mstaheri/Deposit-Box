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

namespace Application.Services.BankAccounts.Commands.UpdateBankAccount
{
    public class UpdateBankAccountCommandHandler
        : IRequestHandler<UpdateBankAccountCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAccountRepositorie _bankAccountRepositorie;
        private readonly ILogger<UpdateBankAccountCommandHandler> _Logger;
        public UpdateBankAccountCommandHandler(IUnitOfWork unitOfWork,
            IBankAccountRepositorie bankAccountRepositorie,
            ILogger<UpdateBankAccountCommandHandler> Logger)

        {
            _unitOfWork = unitOfWork;
            _bankAccountRepositorie = bankAccountRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult> Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bankAccountRepositorie.GetAsync(request.AccountNumber, cancellationToken);
                if (result != null)
                {
                    result.Update(request.AccountName, request.BankName, request.Description);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    string message = string.Format(ConstMessages.Successfully
                        , request.AccountNumber
                        , nameof(UpdateBankAccountCommandHandler));
                    _Logger.LogInformation(message, cancellationToken);
                    return new OperationResult(true, null);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound, request.AccountNumber);
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message, cancellationToken);
                return new OperationResult(false, ex.Message);
            }
        }
    }
}
