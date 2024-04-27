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

namespace Application.Services.UserAndNumberOfShares.Queries.GetUserAndNumberOfShare
{
    public record GetByNameBankQuery
        :IRequest<OperationResult<UserAndNumberOfShare>>
    {
        public required string NameBankSafe { get; init; }
    }
}
