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
    public class BankSafeRepositorie : IBankSafeRepositorie
    {
        private readonly DbSet<BankSafe> _bankSafe;
        public BankSafeRepositorie(IUnitOfWork unitOfWork)
        {
            _bankSafe = unitOfWork.Set<BankSafe>();
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
    }
}
