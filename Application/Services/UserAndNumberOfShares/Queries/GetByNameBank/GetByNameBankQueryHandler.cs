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

namespace Application.Services.UserAndNumberOfShares.Queries.GetUserAndNumberOfShare
{
    public class GetByNameBankQueryHandler
        : IRequestHandler<GetByNameBankQuery, OperationResult<UserAndNumberOfShare>>
    {
        private readonly IUserAndNumberOfShareRepositorieQuery _userAndNumberOfShareRepositorie;
        private readonly ILogger<GetByNameBankQueryHandler> _Logger;
        public GetByNameBankQueryHandler(
            IUserAndNumberOfShareRepositorieQuery userAndNumberOfShareRepositorie,
            ILogger<GetByNameBankQueryHandler> Logger)

        {
            _userAndNumberOfShareRepositorie = userAndNumberOfShareRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult<UserAndNumberOfShare>> Handle(GetByNameBankQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userAndNumberOfShareRepositorie.GetNameBankAsync(request.NameBankSafe, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetByNameBankQueryHandler)
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
