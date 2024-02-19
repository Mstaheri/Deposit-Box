using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Loan
    {
        public Guid Code { get; private set; }
        public string NameBankSafe { get; private set; }
        public string UserName { get; private set; }
        public int NumberOfInstallments { get; private set; }
        public decimal Amount { get; private set; }
        public int Wage { get; private set; }
        public BankSafe BankSafe { get; private set; }
        public ICollection<LoanTransactions> LoanTransactions { get; private set; }
        public ICollection<LoanDocument> LoanDocuments { get; private set; }
        public Loan(string nameBankSafe, string userName, int numberOfInstallments, decimal amount)
        {
            Code = Guid.NewGuid();
            NameBankSafe = nameBankSafe;
            UserName = userName;
            NumberOfInstallments = numberOfInstallments;
            Amount = amount;
        }
        public void Update(int numberOfInstallments, decimal amount)
        {
            NumberOfInstallments = numberOfInstallments;
            Amount = amount;
        }
    }
}
