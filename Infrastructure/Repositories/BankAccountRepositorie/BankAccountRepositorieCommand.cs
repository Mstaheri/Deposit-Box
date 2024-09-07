using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.IRepositories.IBankAccountRepositorie;
using Domain.IRepositories.IUserRepositorie;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.BankAccountRepositorie
{
    public class BankAccountRepositorieCommand : IBankAccountRepositorieCommand
    {
        private readonly DbSet<BankAccount> _bankAccounts;
        private readonly IUserRepositorieQuery _userRepositorie;
        public BankAccountRepositorieCommand(IUnitOfWork UnitOfWork, IUserRepositorieQuery userRepositorie)
        {
            _bankAccounts = UnitOfWork.Set<BankAccount>();
            _userRepositorie = userRepositorie;
        }
        public async ValueTask AddAsync(BankAccount bankAccount, CancellationToken cancellationToken)
        {
            var resultUser = await _userRepositorie.GetAsync(bankAccount.UserName, cancellationToken);
            if (resultUser != null)
            {
                var resultBankAccounts = await _bankAccounts
                    .FirstOrDefaultAsync(p => p.AccountNumber == bankAccount.AccountNumber, cancellationToken);
                if (resultBankAccounts == null)
                {
                    _bankAccounts.Add(bankAccount);
                }
                else
                {
                    string message = string.Format(ConstMessages.Duplicate, bankAccount.AccountNumber.Value);
                    throw new Exception(message);
                }
            }
            else
            {
                string message = string.Format(ConstMessages.NotFound, bankAccount.UserName.Value);
                throw new Exception(message);
            }

        }

        public async Task DeleteAsync(AccountNumber accountNumber, CancellationToken cancellationToken)
        {

            var result = await _bankAccounts.FirstOrDefaultAsync(p => p.AccountNumber == accountNumber, cancellationToken);
            if (result != null)
            {
                _bankAccounts.Remove(result);
            }
            else
            {
                string message = string.Format(ConstMessages.NotFound, accountNumber.Value);
                throw new Exception(message);
            }

        }
    }
}
