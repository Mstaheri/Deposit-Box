using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUserRepositorie
    {
        ValueTask AddAsync(User user);
        Task DeleteAsync(string userName);
        Task<User> GetAsync(string userName);
    }
}
