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

namespace Application.Services.BankSafes.Commands.AddBankSafe
{
    public class AddBankSafeCommandHandler
        : IRequestHandler<AddBankSafeCommand, OperationResult>
    {
        private readonly IBankSafeRepositorieCommand _bankSafeRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddBankSafeCommandHandler> _logger;
        public AddBankSafeCommandHandler(IBankSafeRepositorieCommand bankSafeRepositorie,
            IUnitOfWork unitOfWork
            , ILogger<AddBankSafeCommandHandler> logger)
        {
            _bankSafeRepositorie = bankSafeRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OperationResult> Handle(AddBankSafeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bankSafe = new BankSafe(
                    request.Name,
                    request.SharePrice);
                _bankSafeRepositorie.Add(bankSafe);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                    , bankSafe.Name.Value
                    , nameof(AddBankSafeCommandHandler));
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
