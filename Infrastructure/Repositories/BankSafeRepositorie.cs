using Application.UnitOfWork;
using Domain.Entity;
using Domain.Enum;
using Domain.IRepositories;
using Domain.Message;
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
        private readonly DbSet<BankSafeTransactions> _bankSafeTransactions;
        private readonly DbSet<BankSafeDocument> _bankSafeDocument;
        public BankSafeRepositorie(IUnitOfWork unitOfWork)
        {
            _bankSafe = unitOfWork.Set<BankSafe>();
            _bankSafeTransactions= unitOfWork.Set<BankSafeTransactions>();
            _bankSafeDocument= unitOfWork.Set<BankSafeDocument>();
        }
        public async ValueTask AddAsync(BankSafe bankSafe)
        {
            await _bankSafe.AddAsync(bankSafe);
        }

        public async Task DeleteAsync(Name name)
        {
            var result = await _bankSafe.FirstOrDefaultAsync(p => p.Name == name);
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

        public async Task<List<BankSafe>> GetAllAsync()
        {
            var result = await _bankSafe.ToListAsync();
            return result;
        }

        public async Task<BankSafe> GetAsync(Name name)
        {
            var result = await _bankSafe.FirstOrDefaultAsync(p => p.Name == name);
            return result;
        }

        public async Task<decimal> Inventory()
        {
            var inventoryTransactions = await _bankSafeTransactions.SumAsync(p => p.Deposit - p.Withdrawal);
            var inventoryDocument = await _bankSafeDocument.Where(p => p.Situation == SituationTypes.Confirmed)
                .SumAsync(p => p.Deposit - p.Withdrawal);
            decimal inventory = inventoryTransactions + inventoryDocument;
            return inventory;
        }

        public async Task<decimal> InventoryBankAccount(AccountNumber accountNumber, Name nameBankSafe)
        {
            var inventoryTransactions = await _bankSafeTransactions
                            .Where(p => p.AccountNumber == accountNumber
                             && p.NameBankSafe == nameBankSafe)
                            .SumAsync(p => p.Deposit - p.Withdrawal);

            var inventoryDocument = await _bankSafeDocument
                            .Where(p => p.AccountNumber == accountNumber
                             && p.NameBankSafe == nameBankSafe
                             && p.Situation == SituationTypes.Confirmed)
                            .SumAsync(p => p.Deposit - p.Withdrawal);

            decimal inventory = inventoryTransactions + inventoryDocument;
            return inventory;
        }
    }
}
