using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IBankSafeTransactionsRepositorie
{
    public interface IBankSafeTransactionsRepositorieCommand
    {
        ValueTask AddAsync(BankSafeTransaction bankSafeTransactions, CancellationToken cancellationToken);
    }
}
