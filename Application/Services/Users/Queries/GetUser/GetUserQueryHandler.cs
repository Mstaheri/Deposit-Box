using Application.UnitOfWork;
using Domain.Entity;
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

namespace Application.Services.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, OperationResult<User>>
    {
        private readonly IUserRepositorie _userRepositorie;
        private readonly ILogger<GetUserQueryHandler> _logger;
        public GetUserQueryHandler(IUserRepositorie userRepositorie
            , ILogger<GetUserQueryHandler> logger)
        {
            _userRepositorie = userRepositorie;
            _logger = logger;
        }
        public async Task<OperationResult<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRepositorie.GetAsync(request.UserName, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetUserQueryHandler)
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
