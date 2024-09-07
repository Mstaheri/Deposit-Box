using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IUserRepositorie
{
    public interface IUserRepositorieQuery
    {
        Task<User> GetAsync(UserName userName, CancellationToken cancellationToken);
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<User> GetByUserNameAndPassword(UserName userNam, Password password, CancellationToken cancellationToken);
    }
}
