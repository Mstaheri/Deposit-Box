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
        ValueTask AddAsync(BankSafeDocument bankSafeDocument);
        Task<BankSafeDocument> GetAsync(Guid code);
        Task<List<BankSafeDocument>> GetAllAsync();
    }
}
