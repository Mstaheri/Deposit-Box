using Domain.Entity;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafeDocuments.Query.GetBankSafeDocuments
{
    public record GetBankSafeDocumentsQuery
        :IRequest<OperationResult<BankSafeDocument>>
    {
        public required Guid Code { get; init; }
    }
}
