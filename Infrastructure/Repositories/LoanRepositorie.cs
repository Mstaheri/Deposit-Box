using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LoanRepositorie : ILoanRepositorie
    {
        private readonly DbSet<Loan> _loan;
        private readonly IBankSafeRepositorie _bankSafeRepositorie;
        public LoanRepositorie(IUnitOfWork unitOfWork 
            , IBankSafeRepositorie bankSafeRepositorie)
        {
            _loan = unitOfWork.Set<Loan>();
            _bankSafeRepositorie= bankSafeRepositorie;

        }
        public async ValueTask AddAsync(Loan loan, CancellationToken cancellationToken)
        {
            var result = await _bankSafeRepositorie
                .GetAsync(loan.NameBankSafe , cancellationToken);
            if (result != null)
            {
                _loan.Add(loan);
            }
            else
            {
                string message = string.Format(ConstMessages.NotFound, loan.NameBankSafe.Value);
                throw new Exception(message);
            }
        }

        public async Task DeleteAsync(System.Guid code, CancellationToken cancellationToken)
        {
            var result = await _loan.FirstOrDefaultAsync(loan => loan.Code == code , cancellationToken);
            if (result != null)
            {
                _loan.Remove(result);
            }
            else
            {
                string message = string.Format(ConstMessages.NotFound, code);
                throw new Exception(message);
            }
        }

        public async Task<List<Loan>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _loan.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<Loan> GetByCodeAsync(Guid code, CancellationToken cancellationToken)
        {
            var result = await _loan.FindAsync(code, cancellationToken);
            return result;
        }
    }
}
