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

namespace Infrastructure.Repositories
{
    public class UserRepositorie : IUserRepositorie
    {
        private readonly DbSet<User> _users;
        public UserRepositorie(IUnitOfWork unitOfWork)
        {
            _users = unitOfWork.Set<User>();
        }

        public async ValueTask AddAsync(User user)
        {
            var resultUsers = await _users
                   .FirstOrDefaultAsync(p => p.UserName == user.UserName);
            if (resultUsers == null)
            {
                await _users.AddAsync(user);
            }
            else
            {
                throw new Exception("The user name is duplicate");
            }
            
        }

        public async Task DeleteAsync(string userName)
        {
            var result = await _users.FirstOrDefaultAsync(p => p.UserName == userName);
            if (result != null)
            {
                _users.Remove(result);
            }
            else
            {
                throw new Exception("The desired User was not found");
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            var result = await _users.ToListAsync();
            return result;
        }

        public async Task<User> GetAsync(string userName)
        {
            var result = await _users.FirstOrDefaultAsync(p => p.UserName == userName);
            return result;
        }
    }
}
