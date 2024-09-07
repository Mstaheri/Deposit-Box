using Application.UnitOfWork;
using Domain.Entity;
using Domain.Enum;
using Domain.IRepositories.IBankSafeRepositorie;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.BankSafeRepositorie
{
    public class BankSafeRepositorieQuery : IBankSafeRepositorieQuery
    {
        private readonly DbSet<BankSafe> _bankSafe;
        private readonly DbSet<BankSafeTransaction> _bankSafeTransactions;
        private readonly DbSet<BankSafeDocument> _bankSafeDocument;
        public BankSafeRepositorieQuery(IUnitOfWork unitOfWork)
        {
            _bankSafe = unitOfWork.Set<BankSafe>();
            _bankSafeTransactions = unitOfWork.Set<BankSafeTransaction>();
            _bankSafeDocument = unitOfWork.Set<BankSafeDocument>();
        }
        public async Task<List<BankSafe>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _bankSafe.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<BankSafe> GetAsync(Name name, CancellationToken cancellationToken)
        {
            var result = await _bankSafe.FindAsync(name, cancellationToken);
            return result;
        }

        public async Task<decimal> Inventory(CancellationToken cancellationToken)
        {
            var inventoryTransactions = await _bankSafeTransactions
                .SumAsync(p => p.Deposit - p.Withdrawal, cancellationToken);

            var inventoryDocument = await _bankSafeDocument
                .Where(p => p.Situation == SituationTypes.Confirmed)
                .SumAsync(p => p.Deposit - p.Withdrawal, cancellationToken);

            decimal inventory = inventoryTransactions + inventoryDocument;
            return inventory;
        }

        public async Task<decimal> InventoryBankAccount(AccountNumber accountNumber, Name nameBankSafe
            , CancellationToken cancellationToken)
        {
            var inventoryTransactions = await _bankSafeTransactions
                            .Where(p => p.AccountNumber == accountNumber
                             && p.NameBankSafe == nameBankSafe)
                            .SumAsync(p => p.Deposit - p.Withdrawal, cancellationToken);

            var inventoryDocument = await _bankSafeDocument
                            .Where(p => p.AccountNumber == accountNumber
                             && p.NameBankSafe == nameBankSafe
                             && p.Situation == SituationTypes.Confirmed)
                            .SumAsync(p => p.Deposit - p.Withdrawal, cancellationToken);

            decimal inventory = inventoryTransactions + inventoryDocument;
            return inventory;
        }
    }
}
