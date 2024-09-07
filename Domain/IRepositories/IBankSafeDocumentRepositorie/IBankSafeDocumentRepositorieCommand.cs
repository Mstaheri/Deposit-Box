using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IBankSafeDocumentRepositorie
{
    public interface IBankSafeDocumentRepositorieCommand
    {
        ValueTask AddAsync(BankSafeDocument bankSafeDocument, CancellationToken cancellationToken);
    }
}
