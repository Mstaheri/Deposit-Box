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

namespace Application.Services.UserAndNumberOfShares.Commands.AddUserAndNumberOfShare
{
    public class AddUserAndNumberOfShareCommandHandler
        : IRequestHandler<AddUserAndNumberOfShareCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAndNumberOfShareRepositorieCommand _userAndNumberOfShareRepositorie;
        private readonly ILogger<AddUserAndNumberOfShareCommandHandler> _Logger;
        public AddUserAndNumberOfShareCommandHandler(IUnitOfWork unitOfWork,
            IUserAndNumberOfShareRepositorieCommand userAndNumberOfShareRepositorie,
            ILogger<AddUserAndNumberOfShareCommandHandler> Logger)

        {
            _unitOfWork = unitOfWork;
            _userAndNumberOfShareRepositorie = userAndNumberOfShareRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult> Handle(AddUserAndNumberOfShareCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userAndNumberOfShare = new UserAndNumberOfShare(
                    request.NameBankSafe,
                    request.UserName,
                    request.NumberOfShares);
                await _userAndNumberOfShareRepositorie.AddAsync(userAndNumberOfShare, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                , userAndNumberOfShare.NameBankSafe.Value + "," + userAndNumberOfShare.UserName.Value
                        , nameof(AddUserAndNumberOfShareCommandHandler));
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
