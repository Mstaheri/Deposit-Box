using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IBankAccountRepositorie;
using Domain.IRepositories.IBankSafeRepositorie;
using Domain.IRepositories.IBankSafeTransactionsRepositorie;
using Infrastructure.Repositories.BankSafeRepositorie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.BankSafeTransactionsRepositorie
{
    public class BankSafeTransactionsRepositorieCommand : IBankSafeTransactionsRepositorieCommand
    {
        private readonly DbSet<BankSafeTransaction> _bankSafeTransactions;
        private readonly IBankSafeRepositorieQuery _bankSafeRepositorie;
        private readonly IBankAccountRepositorieQuery _bankAccountRepositorie;
        public BankSafeTransactionsRepositorieCommand(IUnitOfWork unitOfWork,
            IBankSafeRepositorieQuery bankSafeRepositorie,
            IBankAccountRepositorieQuery bankAccountRepositorie)
        {
            _bankSafeTransactions = unitOfWork.Set<BankSafeTransaction>();
            _bankSafeRepositorie = bankSafeRepositorie;
            _bankAccountRepositorie = bankAccountRepositorie;
        }
        public async ValueTask AddAsync(BankSafeTransaction bankSafeTransactions, CancellationToken cancellationToken)
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
                            bankSafeTransactions.NameBankSafe, cancellationToken);

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
    }
}
