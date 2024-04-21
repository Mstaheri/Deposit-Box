using Domain.Attributes;
using Domain.Common;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class BankSafeTransactions : IEntity
    {
        public Guid Code { get; private set; }
        public Name NameBankSafe { get; private set; }
        public AccountNumber AccountNumber { get; private set; }
        public Money Deposit { get; private set; }
        public Money Withdrawal { get; private set; }
        public BankAccount? BankAccount { get; private set; }
        public BankSafe? BankSafe { get; private set; }
        public BankSafeTransactions(Name nameBankSafe, AccountNumber accountNumber,
            Money deposit, Money withdrawal)
        {
            Code = Guid.NewGuid();
            NameBankSafe = nameBankSafe;
            AccountNumber = accountNumber;
            if (deposit == 0 && withdrawal != 0 || deposit != 0 && withdrawal == 0)
            {
                Deposit = deposit;
                Withdrawal = withdrawal;
            }
            else
            {
                throw new Exception("");
            }
            
        }
        public void Update(Money deposit, Money withdrawal)
        {
            if (deposit == 0 && withdrawal != 0 || deposit != 0 && withdrawal == 0)
            {
                Deposit = deposit;
                Withdrawal = withdrawal;
            }
            else
            {
                throw new Exception("");
            }
        }

    }
}
