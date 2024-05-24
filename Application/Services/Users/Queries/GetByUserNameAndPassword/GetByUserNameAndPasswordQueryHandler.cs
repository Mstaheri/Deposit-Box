using Application.Services.Users.Queries.GetAllUser;
using Application.Services.Users.Queries.GetUser;
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

namespace Application.Services.Users.Queries.GetByUserNameAndPassword
{
    public class GetByUserNameAndPasswordQueryHandler
        : IRequestHandler<GetByUserNameAndPasswordQuery, OperationResult<User>>
    {
        public readonly IUserRepositorie _userRepositorie;
        private readonly ILogger<GetByUserNameAndPasswordQueryHandler> _logger;
        public GetByUserNameAndPasswordQueryHandler(
            IUserRepositorie userRepositorie ,
            ILogger<GetByUserNameAndPasswordQueryHandler> logger)
        {
            _userRepositorie = userRepositorie;
            _logger = logger;
        }
        public async Task<OperationResult<User>> Handle(GetByUserNameAndPasswordQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRepositorie.GetByUserNameAndPassword(
                    request.UserName,
                    request.Password,
                    cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetByUserNameAndPasswordQueryHandler)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<User>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<User>(false, ex.Message, null);
            }
        }
    }
}
