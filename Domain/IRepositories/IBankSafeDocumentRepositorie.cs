using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IBankSafeDocumentRepositorie
    {
        ValueTask AddAsync(BankSafeDocument bankSafeDocument ,CancellationToken cancellationToken);
        Task<BankSafeDocument> GetAsync(Guid code ,CancellationToken cancellationToken);
        Task<List<BankSafeDocument>> GetAllAsync(CancellationToken cancellationToken);
    }
}
