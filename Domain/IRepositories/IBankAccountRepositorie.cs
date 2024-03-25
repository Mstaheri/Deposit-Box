using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IBankAccountRepositorie
    {
        ValueTask AddAsync(BankAccount bankAccount);
        Task DeleteAsync(AccountNumber accountNumber);
        Task<BankAccount> GetAsync(AccountNumber accountNumber);
        Task<List<BankAccount>> GetAllAsync();
    }
}
