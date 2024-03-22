using Application.UnitOfWork;
using Domain.IRepositories;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    throw new Exception("The account number is duplicate");
                }
            }
            else
            {
                throw new Exception("Username not found");
            }
            
        }

        public async Task DeleteAsync(string accountNumber)
        {

            var result = await _bankAccounts.FirstOrDefaultAsync(p => p.AccountName == accountNumber);
            if (result != null)
            {
                _bankAccounts.Remove(result);
            }
            else
            {
                throw new Exception("The desired bank account was not found");
            }
            
        }

        public async Task<List<BankAccount>> GetAllAsync()
        {
            var result = await _bankAccounts.ToListAsync();
            return result;
        }

        public async Task<BankAccount> GetAsync(string accountNumber)
        {
            var result = await _bankAccounts.FirstOrDefaultAsync(p => p.AccountNumber == accountNumber);
            return result;
        }
    }
}
