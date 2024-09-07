using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
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
    public class UserRepositorieCommand : IUserRepositorieCommand
    {
        private readonly DbSet<User> _users;
        public UserRepositorieCommand(IUnitOfWork unitOfWork)
        {
            _users = unitOfWork.Set<User>();
        }

        public async ValueTask AddAsync(User user, CancellationToken cancellationToken)
        {
            var resultUsers = await _users
                   .FirstOrDefaultAsync(p => p.UserName == user.UserName, cancellationToken);
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
    }
}
