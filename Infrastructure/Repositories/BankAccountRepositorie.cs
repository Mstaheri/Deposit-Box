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
        public BankAccountRepositorie(IUnitOfWork UnitOfWork)
        {
            _bankAccounts = UnitOfWork.Set<BankAccount>();
        }
        public async ValueTask AddAsync(BankAccount bankAccount)
        {
            await _bankAccounts.AddAsync(bankAccount);
        }

        public async Task DeleteAsync(string accountNumber)
        {
            var result = await _bankAccounts.Where(p => p.AccountName == accountNumber)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                _bankAccounts.Remove(result);
            }
            else
            {
                new Exception("BankAccount deletion was not successful");
            }
            
        }

        public async Task<List<BankAccount>> GetAllAsync()
        {
            var result = await _bankAccounts.ToListAsync();
            return result;
        }

        public async Task<BankAccount> GetAsync(string accountNumber)
        {
            var result = await _bankAccounts.Where(p => p.AccountNumber == accountNumber)
                .FirstOrDefaultAsync();
            return result;
        }
    }
}
