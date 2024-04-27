using Domain.Entity;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafeDocuments.Queries.GetAllBankSafeDocuments
{
    public record GetAllBankSafeDocumentsQuery
        : IRequest<OperationResult<List<BankSafeDocument>>>
    {
    }
}
