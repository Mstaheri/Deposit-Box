﻿using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IBankSafeRepositorie
    {
        void Add(BankSafe bankSafe);
        Task DeleteAsync(Name name, CancellationToken cancellationToken);
        Task<BankSafe> GetAsync(Name name , CancellationToken cancellationToken);
        Task<List<BankSafe>> GetAllAsync(CancellationToken cancellationToken);
        Task<decimal> Inventory(CancellationToken cancellationToken);
        Task<decimal> InventoryBankAccount(AccountNumber accountNumber , Name nameBankSafe, CancellationToken cancellationToken);
    }
}
