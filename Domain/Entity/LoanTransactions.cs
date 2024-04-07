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
    public class LoanTransactions : IEntity
    {
        public Guid Code { get; private set; }
        public Guid CodeLoan { get; private set; }
        public Name NameBankSafe { get; private set; }
        public int NumberOfInstallments { get; private set; }
        public decimal Amount { get; private set; }
        public Loan Loan { get; private set; }
        public BankSafe BankSafe { get; private set; }
        public LoanTransactions(Guid codeLoan , Name nameBankSafe , int numberOfInstallments
            , decimal amount)
        {
            Code = Guid.NewGuid();
            CodeLoan= codeLoan;
            NameBankSafe= nameBankSafe;
            NumberOfInstallments= numberOfInstallments;
            Amount= amount;
        }
        public void Update(int numberOfInstallments, decimal amount)
        {
            NumberOfInstallments = numberOfInstallments;
            Amount = amount;
        }
    }
}
