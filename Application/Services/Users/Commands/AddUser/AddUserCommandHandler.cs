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

namespace Application.Services.Users.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, OperationResult>
    {
        private readonly IUserRepositorie _userRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddUserCommandHandler> _logger;
        public AddUserCommandHandler(IUserRepositorie userRepositorie,
            IUnitOfWork unitOfWork
            , ILogger<AddUserCommandHandler> logger)
        {
            _userRepositorie = userRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<OperationResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User(
                    request.FirstName,
                    request.LastName,
                    request.PhoneNumber,
                    request.NationalIDNumber,
                    request.UserName,
                    request.Password);
                await _userRepositorie.AddAsync(user, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                    , user.UserName.Value
                    , nameof(AddUserCommandHandler));
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
