using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IBankSafeRepositorie
    {
        ValueTask AddAsync(BankSafe bankSafe);
        Task DeleteAsync(Name name);
        Task<BankSafe> GetAsync(Name name);
        Task<List<BankSafe>> GetAllAsync();
    }
}
