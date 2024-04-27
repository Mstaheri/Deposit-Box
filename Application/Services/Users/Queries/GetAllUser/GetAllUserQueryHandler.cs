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

namespace Application.Services.Users.Queries.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, OperationResult<List<User>>>
    {
        private readonly IUserRepositorie _userRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllUserQueryHandler> _logger;
        public GetAllUserQueryHandler(IUserRepositorie userRepositorie,
            IUnitOfWork unitOfWork
            , ILogger<GetAllUserQueryHandler> logger)
        {
            _userRepositorie = userRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OperationResult<List<User>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRepositorie.GetAllAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllUserQueryHandler)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<List<User>>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<List<User>>(false, ex.Message, null);
            }
        }
    }
}
