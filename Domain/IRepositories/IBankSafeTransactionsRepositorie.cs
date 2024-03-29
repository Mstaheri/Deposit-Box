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
        ValueTask AddAsync(BankSafeTransactions bankSafeTransactions);
        Task<BankSafeTransactions> GetAsync(Guid code);
        Task<List<BankSafeTransactions>> GetAllAsync();
    }
}
