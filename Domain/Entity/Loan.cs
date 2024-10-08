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
    public class Loan : IEntity
    {
        public Loan(Name nameBankSafe, Name firstName, Name lastName, Number numberOfInstallments, Money amount , Percent wage)
        {
            Code = Guid.NewGuid();
            NameBankSafe = nameBankSafe;
            FirstName = firstName;
            LastName = lastName;
            NumberOfInstallments = numberOfInstallments;
            Amount = amount;
            Wage = wage;
        }
        public Guid Code { get; private set; }
        public Name NameBankSafe { get; private set; }
        public Name FirstName { get; private set; }
        public Name LastName { get; private set; }
        public Number NumberOfInstallments { get; private set; }
        public Money Amount { get; private set; }
        public Percent Wage { get; private set; }
        public virtual BankSafe BankSafe { get; private set; }
        public virtual ICollection<LoanTransactions> LoanTransactions { get; private set; }
        public virtual ICollection<LoanDocument> LoanDocuments { get; private set; }
        
        public void Update(Name firstName , Name lastName ,
            Number numberOfInstallments, Money amount)
        {
            FirstName = firstName;
            LastName = lastName;
            NumberOfInstallments = numberOfInstallments;
            Amount = amount;
        }
    }
}
