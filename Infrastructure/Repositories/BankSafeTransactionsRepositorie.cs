using Application.UnitOfWork;
using Domain.Entity;
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
    public class BankSafeTransactionsRepositorie : IBankSafeTransactionsRepositorie
    {
        private readonly DbSet<BankSafeTransaction> _bankSafeTransactions;
        private readonly IBankSafeRepositorie _bankSafeRepositorie;
        private readonly IBankAccountRepositorie _bankAccountRepositorie;
        public BankSafeTransactionsRepositorie(IUnitOfWork unitOfWork,
            IBankSafeRepositorie bankSafeRepositorie,
            IBankAccountRepositorie bankAccountRepositorie)
        {
            _bankSafeTransactions = unitOfWork.Set<BankSafeTransaction>();
            _bankSafeRepositorie = bankSafeRepositorie;
            _bankAccountRepositorie = bankAccountRepositorie;
        }
        public async ValueTask AddAsync(BankSafeTransaction bankSafeTransactions , CancellationToken cancellationToken)
        {
            var resultbankAccount = await _bankAccountRepositorie
                .GetAsync(bankSafeTransactions.AccountNumber, cancellationToken);
            if (resultbankAccount != null)
            {
                var resultBankSafe = await _bankSafeRepositorie
                    .GetAsync(bankSafeTransactions.NameBankSafe, cancellationToken);
                if (resultBankSafe != null)
                {
                    if (bankSafeTransactions.Withdrawal.Value != 0)
                    {
                        var inventory = await _bankSafeRepositorie
                            .InventoryBankAccount(bankSafeTransactions.AccountNumber,
                            bankSafeTransactions.NameBankSafe , cancellationToken);

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

        public async Task<List<BankSafeTransaction>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _bankSafeTransactions.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<BankSafeTransaction> GetAsync(Guid code , CancellationToken cancellationToken)
        {
            var result = await _bankSafeTransactions.FindAsync(code, cancellationToken);
            return result;
        }
    }
}
