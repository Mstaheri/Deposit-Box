using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IBankAccountRepositorie
    {
        ValueTask AddAsync(BankAccount bankAccount);
        Task DeleteAsync(string accountNumber);
        Task<BankAccount> GetAsync(string accountNumber);
        Task<List<BankAccount>> GetAllAsync();
    }
}
