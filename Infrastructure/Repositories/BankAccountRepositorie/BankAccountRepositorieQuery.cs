using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories;
using Domain.IRepositories.IBankAccountRepositorie;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.BankAccountRepositorie
{
    public class BankAccountRepositorieQuery : IBankAccountRepositorieQuery
    {
        private readonly DbSet<BankAccount> _bankAccounts;
        public BankAccountRepositorieQuery(IUnitOfWork UnitOfWork)
        {
            _bankAccounts = UnitOfWork.Set<BankAccount>();
        }
        public async Task<List<BankAccount>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _bankAccounts.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<BankAccount> GetAsync(AccountNumber accountNumber, CancellationToken cancellationToken)
        {
            var result = await _bankAccounts.FindAsync(accountNumber, cancellationToken);
            return result;
        }
    }
}
