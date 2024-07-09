using Application.Services.BankSafes.Commands.AddBankSafe;
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

namespace Application.Services.Loan.Commands.AddLoan
{
    public class AddLoanCommandHandler
        : IRequestHandler<AddLoanCommand, OperationResult<Guid>>
    {
        private readonly ILoanRepositorie _loanRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddLoanCommandHandler> _logger;
        public AddLoanCommandHandler(ILoanRepositorie loanRepositorie,
            IUnitOfWork unitOfWork
            , ILogger<AddLoanCommandHandler> logger)
        {
            _loanRepositorie = loanRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OperationResult<Guid>> Handle(AddLoanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loan = new Domain.Entity.Loan(
                    request.NameBankSafe,
                    request.FirstName,
                    request.LastName,
                    request.NumberOfInstallments,
                    request.Amount,
                    request.Wage);
                await _loanRepositorie.AddAsync(loan, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                    , loan.FirstName.Value + " " + loan.LastName.Value
                    , nameof(AddLoanCommandHandler));
                _logger.LogInformation(message);
                return new OperationResult<Guid>(true, null , loan.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<Guid>(false, ex.Message , Guid.Empty);
            }
        }
    }
}
