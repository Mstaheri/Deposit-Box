using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUserRepositorie
    {
        ValueTask AddAsync(User user, CancellationToken cancellationToken);
        Task DeleteAsync(UserName userName, CancellationToken cancellationToken);
        Task<User> GetAsync(UserName userName, CancellationToken cancellationToken);
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
    }
}
