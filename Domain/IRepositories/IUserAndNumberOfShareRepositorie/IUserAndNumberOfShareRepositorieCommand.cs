using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IUserAndNumberOfShareRepositorie
{
    public interface IUserAndNumberOfShareRepositorieCommand
    {
        ValueTask AddAsync(UserAndNumberOfShare userAndNumberOfShare, CancellationToken cancellationToken);
        Task DeleteAsync(Name nameBankSafe, UserName userName, CancellationToken cancellationToken);
    }
}
