using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.IBankSafeRepositorie;
using Domain.IRepositories.IUserAndNumberOfShareRepositorie;
using Domain.IRepositories.IUserRepositorie;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.UserAndNumberOfShareRepositorie
{
    public class UserAndNumberOfShareRepositorieCommand : IUserAndNumberOfShareRepositorieCommand
    {
        private readonly DbSet<UserAndNumberOfShare> _userAndNumberOfShare;
        private readonly IUserRepositorieQuery _UserRepositorie;
        private readonly IBankSafeRepositorieQuery _bankSafeRepositorie;
        public UserAndNumberOfShareRepositorieCommand(IUnitOfWork unitOfWork,
            IUserRepositorieQuery userRepositorie,
            IBankSafeRepositorieQuery bankSafeRepositorie)
        {
            _userAndNumberOfShare = unitOfWork.Set<UserAndNumberOfShare>();
            _UserRepositorie = userRepositorie;
            _bankSafeRepositorie = bankSafeRepositorie;
        }
        public async ValueTask AddAsync(UserAndNumberOfShare userAndNumberOfShare, CancellationToken cancellationToken)
        {
            var resultUser = await _UserRepositorie
                .GetAsync(userAndNumberOfShare.UserName, cancellationToken);
            if (resultUser != null)
            {
                var resultBankSafe = await _bankSafeRepositorie
                    .GetAsync(userAndNumberOfShare.NameBankSafe, cancellationToken);
                if (resultBankSafe != null)
                {
                    _userAndNumberOfShare.Add(userAndNumberOfShare);
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

        public async Task DeleteAsync(Name nameBankSafe, UserName userName, CancellationToken cancellationToken)
        {
            var result = await _userAndNumberOfShare
               .FirstOrDefaultAsync(p => p.NameBankSafe == nameBankSafe && p.UserName == userName, cancellationToken);
            if (result != null)
            {
                _userAndNumberOfShare.Remove(result);
            }
            else
            {
                string message = string.Format(ConstMessages.NotFound, nameBankSafe.Value + "," + userName.Value);
                throw new Exception(message);
            }
        }

    }
}
