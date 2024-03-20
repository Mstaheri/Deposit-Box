using Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class BankSafeTransactions
    {
        public Guid Code { get; private set; }
        public string NameBankSafe { get; private set; }
        public string AccountNumber { get; private set; }
        public decimal Deposit { get; private set; }
        public decimal Withdrawal { get; private set; }
        public BankAccount BankAccount { get; private set; }
        public BankSafe BankSafe { get; private set; }
        public BankSafeTransactions(string nameBankSafe, string accountNumber,
            decimal deposit = 0, decimal withdrawal = 0)
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
                new Exception("");
            }
            
        }
        public void Update(decimal deposit = 0, decimal withdrawal = 0)
        {
            if (deposit == 0 && withdrawal != 0 || deposit != 0 && withdrawal == 0)
            {
                Deposit = deposit;
                Withdrawal = withdrawal;
            }
            else
            {
                new Exception("");
            }
        }

    }
}
