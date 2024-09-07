using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IBankSafeRepositorie;
using Domain.IRepositories.ILoanRepositorie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.LoanRepositorie
{
    public class LoanRepositorieCommand : ILoanRepositorieCommand
    {
        private readonly DbSet<Loan> _loan;
        private readonly IBankSafeRepositorieQuery _bankSafeRepositorie;
        public LoanRepositorieCommand(IUnitOfWork unitOfWork
            , IBankSafeRepositorieQuery bankSafeRepositorie)
        {
            _loan = unitOfWork.Set<Loan>();
            _bankSafeRepositorie = bankSafeRepositorie;

        }
        public async ValueTask AddAsync(Loan loan, CancellationToken cancellationToken)
        {
            var result = await _bankSafeRepositorie
                .GetAsync(loan.NameBankSafe, cancellationToken);
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

        public async Task DeleteAsync(Guid code, CancellationToken cancellationToken)
        {
            var result = await _loan.FirstOrDefaultAsync(loan => loan.Code == code, cancellationToken);
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
    }
}
