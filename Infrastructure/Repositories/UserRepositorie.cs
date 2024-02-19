using Application.IRepositories;
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
            await _users.AddAsync(user);
        }

        public async Task DeleteAsync(string userName)
        {
            var result = await _users.Where(p => p.UserName == userName).FirstOrDefaultAsync();
            if (result != null)
            {
                _users.Remove(result);
            }
            else
            {
                new Exception("");
            }
        }
        public async Task<User> GetAsync(string userName)
        {
            var result = await _users.Where(p => p.UserName == userName).FirstOrDefaultAsync();
            return result;
        }
    }
}
