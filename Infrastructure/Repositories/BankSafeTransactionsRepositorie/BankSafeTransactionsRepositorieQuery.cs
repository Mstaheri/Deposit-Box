using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories.IBankAccountRepositorie;
using Domain.IRepositories.IBankSafeTransactionsRepositorie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.BankSafeTransactionsRepositorie
{
    public class BankSafeTransactionsRepositorieQuery : IBankSafeTransactionsRepositorieQuery
    {
        private readonly DbSet<BankSafeTransaction> _bankSafeTransactions;
        public BankSafeTransactionsRepositorieQuery(IUnitOfWork unitOfWork)
        {
            _bankSafeTransactions = unitOfWork.Set<BankSafeTransaction>();
        }
        public async Task<List<BankSafeTransaction>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _bankSafeTransactions.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<BankSafeTransaction> GetAsync(Guid code, CancellationToken cancellationToken)
        {
            var result = await _bankSafeTransactions.FindAsync(code, cancellationToken);
            return result;
        }
    }
}
