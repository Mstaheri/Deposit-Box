using Application.UnitOfWork;
using Domain.Entity;
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

namespace Application.Services.UserAndNumberOfShares.Queries.GetByNameBankAndUserName
{
    public class GetByNameBankAndUserNameQueryHandler
        : IRequestHandler<GetByNameBankAndUserNameQuery, OperationResult<UserAndNumberOfShare>>
    {
        private readonly IUserAndNumberOfShareRepositorieQuery _userAndNumberOfShareRepositorie;
        private readonly ILogger<GetByNameBankAndUserNameQueryHandler> _Logger;
        public GetByNameBankAndUserNameQueryHandler(
            IUserAndNumberOfShareRepositorieQuery userAndNumberOfShareRepositorie,
            ILogger<GetByNameBankAndUserNameQueryHandler> Logger)

        {
            _userAndNumberOfShareRepositorie = userAndNumberOfShareRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult<UserAndNumberOfShare>> Handle(GetByNameBankAndUserNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userAndNumberOfShareRepositorie.GetNameBankAndUserNameAsync
                    (request.NameBankSafe, request.UserName, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetByNameBankAndUserNameQueryHandler)
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
