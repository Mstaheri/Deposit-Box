using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories.IUserAndNumberOfShareRepositorie;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.UserAndNumberOfShareRepositorie
{
    public class UserAndNumberOfShareRepositorieQuery : IUserAndNumberOfShareRepositorieQuery
    {
        private readonly DbSet<UserAndNumberOfShare> _userAndNumberOfShare;
        public UserAndNumberOfShareRepositorieQuery(IUnitOfWork unitOfWork)
        {
            _userAndNumberOfShare = unitOfWork.Set<UserAndNumberOfShare>();
        }
        public async Task<List<UserAndNumberOfShare>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _userAndNumberOfShare.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<UserAndNumberOfShare> GetNameBankAsync(Name nameBankSafe, CancellationToken cancellationToken)
        {
            var result = await _userAndNumberOfShare.FindAsync(nameBankSafe, cancellationToken);
            return result;
        }

        public async Task<UserAndNumberOfShare> GetUserNameAsync(UserName userName, CancellationToken cancellationToken)
        {
            var result = await _userAndNumberOfShare.FindAsync(userName, cancellationToken);
            return result;
        }

        public async Task<UserAndNumberOfShare> GetNameBankAndUserNameAsync(Name nameBankSafe, UserName userName
            , CancellationToken cancellationToken)
        {
            var result = await _userAndNumberOfShare
                .FindAsync(userName, nameBankSafe, cancellationToken);
            return result;
        }
    }
}
