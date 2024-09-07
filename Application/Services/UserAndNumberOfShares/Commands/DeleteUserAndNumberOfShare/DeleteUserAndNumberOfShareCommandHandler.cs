using Application.UnitOfWork;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.IRepositories.IUserAndNumberOfShareRepositorie;
using Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserAndNumberOfShares.Commands.DeleteUserAndNumberOfShare
{
    public class DeleteUserAndNumberOfShareCommandHandler
        : IRequestHandler<DeleteUserAndNumberOfShareCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAndNumberOfShareRepositorieCommand _userAndNumberOfShareRepositorie;
        private readonly ILogger<DeleteUserAndNumberOfShareCommandHandler> _Logger;
        public DeleteUserAndNumberOfShareCommandHandler(IUnitOfWork unitOfWork,
            IUserAndNumberOfShareRepositorieCommand userAndNumberOfShareRepositorie,
            ILogger<DeleteUserAndNumberOfShareCommandHandler> Logger)

        {
            _unitOfWork = unitOfWork;
            _userAndNumberOfShareRepositorie = userAndNumberOfShareRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult> Handle(DeleteUserAndNumberOfShareCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userAndNumberOfShareRepositorie.DeleteAsync(request.NameBankSafe, request.UserName, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , request.NameBankSafe + "," + request.UserName
                        , nameof(DeleteUserAndNumberOfShareCommandHandler));
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
