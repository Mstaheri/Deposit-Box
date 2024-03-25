using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories;
using Domain.Message;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserAndNumberOfShareRepositorie : IUserAndNumberOfShareRepositorie
    {
        private readonly DbSet<UserAndNumberOfShare> _userAndNumberOfShare;
        private readonly IUserRepositorie _UserRepositorie;
        private readonly IBankSafeRepositorie _bankSafeRepositorie;
        public UserAndNumberOfShareRepositorie(IUnitOfWork unitOfWork,
            IUserRepositorie userRepositorie,
            IBankSafeRepositorie bankSafeRepositorie)
        {
            _userAndNumberOfShare = unitOfWork.Set<UserAndNumberOfShare>();
            _UserRepositorie = userRepositorie;
            _bankSafeRepositorie = bankSafeRepositorie;
        }
        public async ValueTask AddAsync(UserAndNumberOfShare userAndNumberOfShare)
        {
            var resultUser = await _UserRepositorie.GetAsync(userAndNumberOfShare.UserName);
            if (resultUser != null)
            {
                var resultBankSafe = await _bankSafeRepositorie.GetAsync(userAndNumberOfShare.NameBankSafe);
                if (resultBankSafe != null)
                {
                    await _userAndNumberOfShare.AddAsync(userAndNumberOfShare);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound, userAndNumberOfShare.NameBankSafe.Value);
                    throw new Exception(message);
                }
            }
            else
            {
                string message = string.Format(ConstMessages.NotFound, userAndNumberOfShare.UserName.Value);
                throw new Exception(message);
            }
        }

        public async Task DeleteAsync(Name nameBankSafe, UserName userName)
        {
            var result = await _userAndNumberOfShare
               .FirstOrDefaultAsync(p => p.NameBankSafe == nameBankSafe && p.UserName == userName);
            if (result != null)
            {
                _userAndNumberOfShare.Remove(result);
            }
            else
            {
                string message = string.Format(ConstMessages.NotFound, nameBankSafe.Value +","+ userName.Value);
                throw new Exception(message);
            }
        }

        public async Task<List<UserAndNumberOfShare>> GetAllAsync()
        {
            var result = await _userAndNumberOfShare.ToListAsync();
            return result;
        }

        public async Task<UserAndNumberOfShare> GetNameBankAsync(Name nameBankSafe)
        {
            var result = await _userAndNumberOfShare
                .FirstOrDefaultAsync(p => p.NameBankSafe == nameBankSafe);
            return result;
        }

        public async Task<UserAndNumberOfShare> GetUserNameAsync(UserName userName)
        {
            var result = await _userAndNumberOfShare
                .FirstOrDefaultAsync(p => p.UserName == userName);
            return result;
        }

        public async Task<UserAndNumberOfShare> GetNameBankAndUserNameAsync(Name nameBankSafe, UserName userName)
        {
            var result = await _userAndNumberOfShare
                .FirstOrDefaultAsync(p => p.UserName == userName && p.NameBankSafe == nameBankSafe);
            return result;
        }
    }
}
