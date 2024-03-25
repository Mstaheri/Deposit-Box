using Domain.Attributes;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class Loan
    {
        public Guid Code { get; private set; }
        public Name NameBankSafe { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int NumberOfInstallments { get; private set; }
        public decimal Amount { get; private set; }
        public int Wage { get; private set; }
        public BankSafe BankSafe { get; private set; }
        public ICollection<LoanTransactions> LoanTransactions { get; private set; }
        public ICollection<LoanDocument> LoanDocuments { get; private set; }
        public Loan(Name nameBankSafe, string firstName , string lastName, int numberOfInstallments, decimal amount)
        {
            Code = Guid.NewGuid();
            NameBankSafe = nameBankSafe;
            FirstName = firstName;
            LastName = lastName;
            NumberOfInstallments = numberOfInstallments;
            Amount = amount;
        }
        public void Update(string firstName , string lastName ,
            int numberOfInstallments, decimal amount)
        {
            FirstName = firstName;
            LastName = lastName;
            NumberOfInstallments = numberOfInstallments;
            Amount = amount;
        }
    }
}
