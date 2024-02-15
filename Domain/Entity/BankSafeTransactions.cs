using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class BankSafeTransactions
    {
        public Guid CodeTransactions { get; private set; }
        public string NameBankSafe { get; private set; }
        public string AccountNumber { get; private set; }
        public decimal DepositPrice { get; private set; }
        public decimal WithdrawalPrice { get; private set; }
        public BankAccount BankAccount { get; private set; }
        public BankSafe BankSafe { get; private set; }
        public BankSafeTransactions(string nameBankSafe, string accountNumber,
            decimal depositPrice = 0, decimal withdrawalPrice = 0)
        {
            CodeTransactions = Guid.NewGuid();
            NameBankSafe = nameBankSafe;
            AccountNumber = accountNumber;
            if (depositPrice == 0 && withdrawalPrice != 0 || depositPrice != 0 && withdrawalPrice == 0)
            {
                DepositPrice = depositPrice;
                WithdrawalPrice = withdrawalPrice;
            }
            else
            {
                new Exception("");
            }
            
        }
        public void Edit(decimal depositPrice = 0, decimal withdrawalPrice = 0)
        {
            if (depositPrice == 0 && withdrawalPrice != 0 || depositPrice != 0 && withdrawalPrice == 0)
            {
                DepositPrice = depositPrice;
                WithdrawalPrice = withdrawalPrice;
            }
            else
            {
                new Exception("");
            }
        }

    }
}
