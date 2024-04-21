using Application.UnitOfWork;
using Domain.IRepositories;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Message;
using Domain.ValueObjects;

namespace Infrastructure.Repositories
{
    public class UserRepositorie : IUserRepositorie
    {
        private readonly DbSet<User> _users;
        public UserRepositorie(IUnitOfWork unitOfWork)
        {
            _users = unitOfWork.Set<User>();
        }

        public async ValueTask AddAsync(User user, CancellationToken cancellationToken)
        {
            var resultUsers = await _users
                   .FirstOrDefaultAsync(p => p.UserName == user.UserName , cancellationToken);
            if (resultUsers == null)
            {
                _users.Add(user);
            }
            else
            {
                string message = string.Format(ConstMessages.Duplicate, user.UserName.Value);
                throw new Exception(message);
            }
            
        }

        public async Task DeleteAsync(UserName userName, CancellationToken cancellationToken)
        {
            var result = await _users.FirstOrDefaultAsync(p => p.UserName == userName, cancellationToken);
            if (result != null)
            {
                _users.Remove(result);
            }
            else
            {
                string message = string.Format(ConstMessages.NotFound, userName.Value);
                throw new Exception(message);
            }
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
    }
}
