using Application.UnitOfWork;
using Domain.IRepositories;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Message;
using Domain.ValueObjects;

namespace Infrastructure.Repositories
{
    public class BankAccountRepositorie : IBankAccountRepositorie
    {
        private readonly DbSet<BankAccount> _bankAccounts;
        private readonly IUserRepositorie _userRepositorie;
        public BankAccountRepositorie(IUnitOfWork UnitOfWork, IUserRepositorie userRepositorie)
        {
            _bankAccounts = UnitOfWork.Set<BankAccount>();
            _userRepositorie = userRepositorie;
        }
        public async ValueTask AddAsync(BankAccount bankAccount)
        {
            var resultUser = await _userRepositorie.GetAsync(bankAccount.UserName);
            if (resultUser != null)
            {
                var resultBankAccounts = await _bankAccounts
                    .FirstOrDefaultAsync(p => p.AccountNumber == bankAccount.AccountNumber);
                if (resultBankAccounts == null)
                {
                    await _bankAccounts.AddAsync(bankAccount);
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

        public async Task DeleteAsync(AccountNumber accountNumber)
        {

            var result = await _bankAccounts.FirstOrDefaultAsync(p => p.AccountNumber == accountNumber);
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

        public async Task<List<BankAccount>> GetAllAsync()
        {
            var result = await _bankAccounts.ToListAsync();
            return result;
        }

        public async Task<BankAccount> GetAsync(AccountNumber accountNumber)
        {
            var result = await _bankAccounts.FirstOrDefaultAsync(p => p.AccountNumber == accountNumber);
            return result;
        }
    }
}
