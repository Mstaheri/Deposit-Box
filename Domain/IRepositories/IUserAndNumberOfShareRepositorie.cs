﻿using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUserAndNumberOfShareRepositorie
    {
        ValueTask AddAsync(UserAndNumberOfShare userAndNumberOfShare, CancellationToken cancellationToken);
        Task DeleteAsync(Name nameBankSafe , UserName userName, CancellationToken cancellationToken);
        Task<UserAndNumberOfShare> GetNameBankAsync(Name nameBankSafe, CancellationToken cancellationToken);
        Task<UserAndNumberOfShare> GetUserNameAsync(UserName userName, CancellationToken cancellationToken);
        Task<UserAndNumberOfShare> GetNameBankAndUserNameAsync(Name nameBankSafe, UserName userName, CancellationToken cancellationToken);
        Task<List<UserAndNumberOfShare>> GetAllAsync(CancellationToken cancellationToken);
    }
}
