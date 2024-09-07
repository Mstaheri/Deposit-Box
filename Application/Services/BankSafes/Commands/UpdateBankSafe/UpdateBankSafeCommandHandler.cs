using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.IRepositories.IBankSafeRepositorie;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafes.Commands.UpdateBankSafe
{
    public class UpdateBankSafeCommandHandler
        : IRequestHandler<UpdateBankSafeCommand, OperationResult>
    {
        private readonly IBankSafeRepositorieQuery _bankSafeRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateBankSafeCommandHandler> _logger;
        public UpdateBankSafeCommandHandler(IBankSafeRepositorieQuery bankSafeRepositorie,
            IUnitOfWork unitOfWork
            , ILogger<UpdateBankSafeCommandHandler> logger)
        {
            _bankSafeRepositorie = bankSafeRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OperationResult> Handle(UpdateBankSafeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bankSafeRepositorie.GetAsync(request.Name, cancellationToken);
                if (result != null)
                {
                    result.Update(request.SharePrice);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    string message = string.Format(ConstMessages.Successfully
                        , request.Name
                        , nameof(UpdateBankSafeCommandHandler));
                    _logger.LogInformation(message);
                    return new OperationResult(true, null);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound, request.Name);
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
    }
}
