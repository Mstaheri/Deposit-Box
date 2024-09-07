using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.ILoanRepositorie
{
    public interface ILoanRepositorieQuery
    {
        Task<Loan> GetByCodeAsync(Guid code, CancellationToken cancellationToken);
        Task<List<Loan>> GetAllAsync(CancellationToken cancellationToken);
    }
}
