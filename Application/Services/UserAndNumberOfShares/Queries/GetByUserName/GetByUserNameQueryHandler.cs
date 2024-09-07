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

namespace Application.Services.UserAndNumberOfShares.Queries.GetByUserName
{
    public class GetByUserNameQueryHandler
        :IRequestHandler<GetByUserNameQuery , OperationResult<UserAndNumberOfShare>>
    {
        private readonly IUserAndNumberOfShareRepositorieQuery _userAndNumberOfShareRepositorie;
        private readonly ILogger<GetByUserNameQueryHandler> _Logger;
        public GetByUserNameQueryHandler(
            IUserAndNumberOfShareRepositorieQuery userAndNumberOfShareRepositorie,
            ILogger<GetByUserNameQueryHandler> Logger)

        {
            _userAndNumberOfShareRepositorie = userAndNumberOfShareRepositorie;
            _Logger = Logger;
        }

        public async Task<OperationResult<UserAndNumberOfShare>> Handle(GetByUserNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userAndNumberOfShareRepositorie.GetUserNameAsync(request.UserName, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetByUserNameQueryHandler)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<UserAndNumberOfShare>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<UserAndNumberOfShare>(false, ex.Message, null);
            }
        }
    }
}
