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
        Task DeleteAsync(Name name);
        Task<BankSafe> GetAsync(Name name);
        Task<List<BankSafe>> GetAllAsync();
        Task<decimal> Inventory();
        Task<decimal> InventoryBankAccount(AccountNumber accountNumber , Name nameBankSafe);
    }
}
