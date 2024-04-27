using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.BankAccounts.Commands.AddBankAccount
{
    public class AddBankAccountCommandHandler : IRequestHandler<AddBankAccountCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAccountRepositorie _bankAccountRepositorie;
        private readonly ILogger<AddBankAccountCommandHandler> _Logger;
        public AddBankAccountCommandHandler(IUnitOfWork unitOfWork,
            IBankAccountRepositorie bankAccountRepositorie,
            ILogger<AddBankAccountCommandHandler> Logger)

        {
            _unitOfWork = unitOfWork;
            _bankAccountRepositorie = bankAccountRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult> Handle(AddBankAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bankAccount = new BankAccount(
                    request.AccountNumber,
                    request.UserName,
                    request.AccountName,
                    request.BankName,
                    request.Description);
                await _bankAccountRepositorie.AddAsync(bankAccount, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , bankAccount.AccountNumber.Value
                        , nameof(AddBankAccountCommandHandler));
                _Logger.LogInformation(message);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
    }
}
