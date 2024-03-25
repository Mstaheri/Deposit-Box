using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUserAndNumberOfShareRepositorie
    {
        ValueTask AddAsync(UserAndNumberOfShare userAndNumberOfShare);
        Task DeleteAsync(Name nameBankSafe , UserName userName);
        Task<UserAndNumberOfShare> GetNameBankAsync(Name nameBankSafe);
        Task<UserAndNumberOfShare> GetUserNameAsync(UserName userName);
        Task<UserAndNumberOfShare> GetNameBankAndUserNameAsync(Name nameBankSafe, UserName userName);
        Task<List<UserAndNumberOfShare>> GetAllAsync();
    }
}
