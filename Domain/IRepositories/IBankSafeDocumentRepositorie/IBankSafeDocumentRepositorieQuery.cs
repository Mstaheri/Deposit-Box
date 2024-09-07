using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IBankSafeDocumentRepositorie
{
    public interface IBankSafeDocumentRepositorieQuery
    {
        Task<BankSafeDocument> GetAsync(Guid code, CancellationToken cancellationToken);
        Task<List<BankSafeDocument>> GetAllAsync(CancellationToken cancellationToken);
    }
}
