using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Loan.Queries.GetAllLoan
{
    public record GetAllLoanQuery 
        : IRequest<OperationResult<List<Domain.Entity.Loan>>>
    {

    }
}
