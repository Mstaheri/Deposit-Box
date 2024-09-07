using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IBankSafeTransactionsRepositorie
{
    public interface IBankSafeTransactionsRepositorieQuery
    {
        Task<BankSafeTransaction> GetAsync(Guid code, CancellationToken cancellationToken);
        Task<List<BankSafeTransaction>> GetAllAsync(CancellationToken cancellationToken);
    }
}
