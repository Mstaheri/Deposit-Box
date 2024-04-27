using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OperationResult>
    {
        private readonly IUserRepositorie _userRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        public UpdateUserCommandHandler(IUserRepositorie userRepositorie,
            IUnitOfWork unitOfWork
            , ILogger<UpdateUserCommandHandler> logger)
        {
            _userRepositorie = userRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OperationResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRepositorie.GetAsync(request.UserName, cancellationToken);
                if (result != null)
                {
                    result.Update(request.FirstName, request.LastName, request.PhoneNumber
                     , request.NationalIDNumber, request.Password);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    string message = string.Format(ConstMessages.Successfully
                        , request.UserName
                        , nameof(UpdateUserCommandHandler));
                    _logger.LogInformation(message);
                    return new OperationResult(true, null);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound, request.UserName);
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
