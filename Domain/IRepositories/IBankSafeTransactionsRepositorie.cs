using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IBankSafeTransactionsRepositorie
    {
        ValueTask AddAsync(BankSafeTransaction bankSafeTransactions, CancellationToken cancellationToken);
        Task<BankSafeTransaction> GetAsync(Guid code, CancellationToken cancellationToken);
        Task<List<BankSafeTransaction>> GetAllAsync(CancellationToken cancellationToken);
    }
}
