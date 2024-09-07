using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IBankAccountRepositorie
{
    public interface IBankAccountRepositorieCommand
    {
        ValueTask AddAsync(BankAccount bankAccount, CancellationToken cancellationToken);
        Task DeleteAsync(AccountNumber accountNumber, CancellationToken cancellationToken);
    }
}
