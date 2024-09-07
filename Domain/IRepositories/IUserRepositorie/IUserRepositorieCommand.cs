using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IUserRepositorie
{
    public interface IUserRepositorieCommand
    {
        ValueTask AddAsync(User user, CancellationToken cancellationToken);
        Task DeleteAsync(UserName userName, CancellationToken cancellationToken);
    }
}
