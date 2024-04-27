using Application.UnitOfWork;
using Domain.Entity;
using Domain.Enum;
using Domain.IRepositories;
using Domain.Exceptions;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BankSafeRepositorie : IBankSafeRepositorie
    {
        private readonly DbSet<BankSafe> _bankSafe;
        private readonly DbSet<BankSafeTransaction> _bankSafeTransactions;
        private readonly DbSet<BankSafeDocument> _bankSafeDocument;
        public BankSafeRepositorie(IUnitOfWork unitOfWork)
        {
            _bankSafe = unitOfWork.Set<BankSafe>();
            _bankSafeTransactions= unitOfWork.Set<BankSafeTransaction>();
            _bankSafeDocument= unitOfWork.Set<BankSafeDocument>();
        }
        public void Add(BankSafe bankSafe)
        {
            _bankSafe.Add(bankSafe);
        }

        public async Task DeleteAsync(Name name , CancellationToken cancellationToken)
        {
            var result = await _bankSafe.FirstOrDefaultAsync(p => p.Name == name , cancellationToken);
            if (result != null)
            {
                _bankSafe.Remove(result);
            }
            else
            {
                string message = string.Format(ConstMessages.NotFound, name.Value);
                throw new Exception(message);
            }
        }

        public async Task<List<BankSafe>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _bankSafe.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<BankSafe> GetAsync(Name name , CancellationToken cancellationToken)
        {
            var result = await _bankSafe.FirstOrDefaultAsync(p => p.Name == name , cancellationToken);
            return result;
        }

        public async Task<decimal> Inventory(CancellationToken cancellationToken)
        {
            var inventoryTransactions = await _bankSafeTransactions
                .SumAsync(p => p.Deposit - p.Withdrawal ,cancellationToken);

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
