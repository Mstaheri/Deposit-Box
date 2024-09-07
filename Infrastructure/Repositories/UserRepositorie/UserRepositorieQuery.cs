using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories.IUserRepositorie;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.UserRepositorie
{
    public class UserRepositorieQuery : IUserRepositorieQuery
    {
        private readonly DbSet<User> _users;
        public UserRepositorieQuery(IUnitOfWork unitOfWork)
        {
            _users = unitOfWork.Set<User>();
        }
        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _users.ToListAsync(cancellationToken);
            return result;
        }

        public async Task<User> GetAsync(UserName userName, CancellationToken cancellationToken)
        {
            var result = await _users.FirstOrDefaultAsync(p => p.UserName == userName, cancellationToken);
            return result;
        }

        public async Task<User> GetByUserNameAndPassword(UserName userNam, Password password, CancellationToken cancellationToken)
        {
            var result = await _users
                .FirstOrDefaultAsync(p => p.UserName == userNam && p.Password == password, cancellationToken);
            return result;
        }
    }
}
