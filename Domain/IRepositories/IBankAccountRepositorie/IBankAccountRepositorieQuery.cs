using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IBankAccountRepositorie
{
    public interface IBankAccountRepositorieQuery
    {
        Task<BankAccount> GetAsync(AccountNumber accountNumber, CancellationToken cancellationToken);
        Task<List<BankAccount>> GetAllAsync(CancellationToken cancellationToken);
    }
}
