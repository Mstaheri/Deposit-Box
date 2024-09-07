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

namespace Application.Services.UserAndNumberOfShares.Queries.GetAllUserAndNumberOfShare
{
    public class GetAllUserAndNumberOfShareQueryHandler
        : IRequestHandler<GetAllUserAndNumberOfShareQuery, OperationResult<List<UserAndNumberOfShare>>>
    {
        private readonly IUserAndNumberOfShareRepositorieQuery _userAndNumberOfShareRepositorie;
        private readonly ILogger<GetAllUserAndNumberOfShareQueryHandler> _Logger;
        public GetAllUserAndNumberOfShareQueryHandler(
            IUserAndNumberOfShareRepositorieQuery userAndNumberOfShareRepositorie,
            ILogger<GetAllUserAndNumberOfShareQueryHandler> Logger)

        {
            _userAndNumberOfShareRepositorie = userAndNumberOfShareRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult<List<UserAndNumberOfShare>>> Handle(GetAllUserAndNumberOfShareQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userAndNumberOfShareRepositorie.GetAllAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllUserAndNumberOfShareQueryHandler)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<List<UserAndNumberOfShare>>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<List<UserAndNumberOfShare>>(false, ex.Message, null);
            }
        }
    }
}
