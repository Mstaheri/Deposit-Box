﻿using Domain.Attributes;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class BankSafe : IEntity
    {
        public Name Name { get; private set; }
        public Money SharePrice { get; private set; }
        public ICollection<UserAndNumberOfShare>? UserAndNumberOfShares { get; private set; }
        public ICollection<BankSafeTransactions>? BankSafeTransactions { get; private set; }
        public ICollection<BankSafeDocument>? BankSafeDocuments { get; private set; }
        public ICollection<Loan>? loans { get; private set; }
        public ICollection<LoanTransactions>? LoanTransactions { get; private set; }
        public ICollection<LoanDocument>? LoanDocuments { get; private set; }
        public BankSafe(Name name, Money sharePrice)
        {
            Name = name;
            SharePrice = sharePrice;
        }
        public void Update(Money sharePrice)
        {
            SharePrice = sharePrice;
        }
    }
}
