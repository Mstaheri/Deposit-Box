using Application.UnitOfWork;
using Domain.Entity;
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
    public class BankSafeTransactionsRepositorie : IBankSafeTransactionsRepositorie
    {
        private readonly DbSet<BankSafeTransactions> _bankSafeTransactions;
        private readonly IBankSafeRepositorie _bankSafeRepositorie;
        private readonly IBankAccountRepositorie _bankAccountRepositorie;
        public BankSafeTransactionsRepositorie(IUnitOfWork unitOfWork,
            IBankSafeRepositorie bankSafeRepositorie,
            IBankAccountRepositorie bankAccountRepositorie)
        {
            _bankSafeTransactions = unitOfWork.Set<BankSafeTransactions>();
            _bankSafeRepositorie = bankSafeRepositorie;
            _bankAccountRepositorie = bankAccountRepositorie;
        }
        public async ValueTask AddAsync(BankSafeTransactions bankSafeTransactions)
        {
            var resultbankAccount = await _bankAccountRepositorie.GetAsync(bankSafeTransactions.AccountNumber);
            if (resultbankAccount != null)
            {
                var resultBankSafe = await _bankSafeRepositorie.GetAsync(bankSafeTransactions.NameBankSafe);
                if (resultBankSafe != null)
                {
                    if (bankSafeTransactions.Withdrawal.Value != 0)
                    {
                        var inventory = await _bankSafeRepositorie.InventoryBankAccount(bankSafeTransactions.AccountNumber,
                            bankSafeTransactions.NameBankSafe);

                        if (inventory < bankSafeTransactions.Withdrawal.Value)
                        {
                            string message = string.Format(ConstMessages.NotInventory
                                , bankSafeTransactions.AccountNumber.Value
                                , bankSafeTransactions.NameBankSafe.Value
                                , inventory);
                            throw new Exception(message);
                        }
                    }
                    _bankSafeTransactions.Add(bankSafeTransactions);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound, bankSafeTransactions.NameBankSafe.Value);
                    throw new Exception(message);
                }
            }
            else
            {
                string message = string.Format(ConstMessages.NotFound, bankSafeTransactions.AccountNumber.Value);
                throw new Exception(message);
            }
        }

        public async Task<List<BankSafeTransactions>> GetAllAsync()
        {
            var result = await _bankSafeTransactions.ToListAsync();
            return result;
        }

        public async Task<BankSafeTransactions> GetAsync(Guid code)
        {
            var result = await _bankSafeTransactions.FirstOrDefaultAsync(p => p.Code == code);
            return result;
        }
    }
}
