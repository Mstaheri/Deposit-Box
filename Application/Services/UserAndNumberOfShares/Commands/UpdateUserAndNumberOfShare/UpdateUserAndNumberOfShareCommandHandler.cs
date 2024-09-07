using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.IRepositories.IUserAndNumberOfShareRepositorie;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserAndNumberOfShares.Commands.UpdateUserAndNumberOfShare
{
    public class UpdateUserAndNumberOfShareCommandHandler
        : IRequestHandler<UpdateUserAndNumberOfShareCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAndNumberOfShareRepositorieQuery _userAndNumberOfShareRepositorie;
        private readonly ILogger<UpdateUserAndNumberOfShareCommandHandler> _Logger;
        public UpdateUserAndNumberOfShareCommandHandler(IUnitOfWork unitOfWork,
            IUserAndNumberOfShareRepositorieQuery userAndNumberOfShareRepositorie,
            ILogger<UpdateUserAndNumberOfShareCommandHandler> Logger)

        {
            _unitOfWork = unitOfWork;
            _userAndNumberOfShareRepositorie = userAndNumberOfShareRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult> Handle(UpdateUserAndNumberOfShareCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userAndNumberOfShareRepositorie.GetNameBankAndUserNameAsync
                    (request.NameBankSafe, request.UserName, cancellationToken);
                if (result != null)
                {
                    result.Update(request.NumberOfShares);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    string message = string.Format(ConstMessages.Successfully
                    , request.NameBankSafe+ "," + request.UserName
                        , nameof(UpdateUserAndNumberOfShareCommandHandler));
                    _Logger.LogInformation(message);
                    return new OperationResult(true, null);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound
                        , request.NameBankSafe + "," + request.UserName);
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
    }
}
