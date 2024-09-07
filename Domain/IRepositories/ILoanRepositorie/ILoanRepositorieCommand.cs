using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.ILoanRepositorie
{
    public interface ILoanRepositorieCommand
    {
        ValueTask AddAsync(Loan loan, CancellationToken cancellationToken);
        Task DeleteAsync(Guid code, CancellationToken cancellationToken);
    }
}
