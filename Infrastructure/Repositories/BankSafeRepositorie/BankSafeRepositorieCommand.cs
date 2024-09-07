using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IBankSafeRepositorie;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.BankSafeRepositorie
{
    public class BankSafeRepositorieCommand : IBankSafeRepositorieCommand
    {
        private readonly DbSet<BankSafe> _bankSafe;
        public BankSafeRepositorieCommand(IUnitOfWork unitOfWork)
        {
            _bankSafe = unitOfWork.Set<BankSafe>();
        }
        public void Add(BankSafe bankSafe)
        {
            _bankSafe.Add(bankSafe);
        }

        public async Task DeleteAsync(Name name, CancellationToken cancellationToken)
        {
            var result = await _bankSafe.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
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
    }
}
