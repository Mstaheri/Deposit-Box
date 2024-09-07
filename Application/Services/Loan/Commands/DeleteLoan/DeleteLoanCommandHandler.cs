using Application.Services.BankSafes.Commands.DeleteBankSafe;
using Application.UnitOfWork;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.IRepositories.ILoanRepositorie;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Loan.Commands.DeleteLoan
{
    public class DeleteLoanCommandHandler :
        IRequestHandler<DeleteLoanCommand, OperationResult>
    {
        private readonly ILoanRepositorieCommand _loanRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteLoanCommandHandler> _logger;
        public DeleteLoanCommandHandler(ILoanRepositorieCommand loanRepositorie,
            IUnitOfWork unitOfWork
            , ILogger<DeleteLoanCommandHandler> logger)
        {
            _loanRepositorie = loanRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OperationResult> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _loanRepositorie.DeleteAsync(request.Code, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , request.Code
                        , nameof(DeleteLoanCommandHandler));
                _logger.LogInformation(message);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
    }
}
