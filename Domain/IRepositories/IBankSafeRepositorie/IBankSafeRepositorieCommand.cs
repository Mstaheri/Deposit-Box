using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IBankSafeRepositorie
{
    public interface IBankSafeRepositorieCommand
    {
        void Add(BankSafe bankSafe);
        Task DeleteAsync(Name name, CancellationToken cancellationToken);
    }
}
