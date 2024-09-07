using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories.IBankAccountRepositorie;
using Domain.IRepositories.IBankSafeDocumentRepositorie;
using Domain.IRepositories.IBankSafeRepositorie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.BankSafeDocumentRepositorie
{
    public class BankSafeDocumentRepositorieQuery : IBankSafeDocumentRepositorieQuery
    {
        private readonly DbSet<BankSafeDocument> _bankSafeDocument;
        public BankSafeDocumentRepositorieQuery(IUnitOfWork unitOfWork)
        {
            _bankSafeDocument = unitOfWork.Set<BankSafeDocument>();
        }
        public async Task<List<BankSafeDocument>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _bankSafeDocument.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<BankSafeDocument> GetAsync(System.Guid code, CancellationToken cancellationToken)
        {
            var result = await _bankSafeDocument.FindAsync(code, cancellationToken);
            return result;
        }
    }
}
