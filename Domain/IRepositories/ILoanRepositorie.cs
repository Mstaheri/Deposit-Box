using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface ILoanRepositorie
    {
        ValueTask AddAsync(Loan loan, CancellationToken cancellationToken);
        Task DeleteAsync(Guid code, CancellationToken cancellationToken);
        Task<Loan> GetByCodeAsync(Guid code , CancellationToken cancellationToken);
        Task<List<Loan>> GetAllAsync(CancellationToken cancellationToken);
    }
}
