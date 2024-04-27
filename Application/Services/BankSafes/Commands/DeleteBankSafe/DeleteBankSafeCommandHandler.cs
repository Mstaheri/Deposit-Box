using Application.UnitOfWork;
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

namespace Application.Services.BankSafes.Commands.DeleteBankSafe
{
    public class DeleteBankSafeCommandHandler
        : IRequestHandler<DeleteBankSafeCommand, OperationResult>
    {
        private readonly IBankSafeRepositorie _bankSafeRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteBankSafeCommandHandler> _logger;
        public DeleteBankSafeCommandHandler(IBankSafeRepositorie bankSafeRepositorie,
            IUnitOfWork unitOfWork
            , ILogger<DeleteBankSafeCommandHandler> logger)
        {
            _bankSafeRepositorie = bankSafeRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OperationResult> Handle(DeleteBankSafeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _bankSafeRepositorie.DeleteAsync(request.Name, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , request.Name
                        , nameof(DeleteBankSafeCommandHandler));
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
