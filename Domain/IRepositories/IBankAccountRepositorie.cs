using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IBankAccountRepositorie
    {
        ValueTask AddAsync(BankAccount bankAccount , CancellationToken cancellationToken);
        Task DeleteAsync(AccountNumber accountNumber , CancellationToken cancellationToken);
        Task<BankAccount> GetAsync(AccountNumber accountNumber , CancellationToken cancellationToken);
        Task<List<BankAccount>> GetAllAsync(CancellationToken cancellationToken);
    }
}
