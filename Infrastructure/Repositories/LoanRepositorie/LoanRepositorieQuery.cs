using Application.UnitOfWork;
using Domain.Entity;
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
    public class LoanRepositorieQuery : ILoanRepositorieQuery
    {
        private readonly DbSet<Loan> _loan;
        public LoanRepositorieQuery(IUnitOfWork unitOfWork)
        {
            _loan = unitOfWork.Set<Loan>();

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
