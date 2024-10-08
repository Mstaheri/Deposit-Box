﻿using Domain.Attributes;
using Domain.Common;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class BankAccount : IEntity
    {
        public BankAccount(AccountNumber accountNumber, UserName userName, Name accountName,
            Name bankName, string description)
        {
            AccountNumber = accountNumber;
            UserName = userName;
            AccountName = accountName;
            BankName = bankName;
            Description = description;
        }
        public AccountNumber AccountNumber { get; private set; }
        public UserName UserName { get; private set; }
        public Name AccountName { get; private set; }
        public Name BankName { get; private set; }
        public string Description { get; private set; }
        public virtual User User { get; private set; }
        public virtual ICollection<BankSafeTransaction> BankSafeTransactions { get; private set; }
        public virtual ICollection<BankSafeDocument> BankSafeDocuments { get; private set; }
        
        public void Update(Name accountName,
            Name bankName, string description)
        {
            AccountName = accountName;
            BankName = bankName;
            Description = description;
        }
    }
}
