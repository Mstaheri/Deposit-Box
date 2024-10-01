using Domain.Attributes;
using Domain.Common;
using Domain.Enum;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class LoanDocument : IEntity
    {
        public LoanDocument(Guid codeLoan, Name nameBankSafe, string registrationDate,
            string dueDate, decimal amount, SituationTypes situation)
        {
            Code = Guid.NewGuid();
            CodeLoan = codeLoan;
            NameBankSafe = nameBankSafe;
            RegistrationDate = registrationDate;
            DueDate = dueDate;
            Amount = amount;
            Situation = situation;
        }
        public Guid Code { get; private set; }
        public Guid CodeLoan { get; private set; }
        public Name NameBankSafe { get; private set; }
        public string RegistrationDate { get; private set; }
        public string DueDate { get; private set; }
        public decimal Amount { get; private set; }
        public SituationTypes Situation { get; private set; }
        public virtual Loan Loan { get; private set; }
        public virtual BankSafe BankSafe { get; private set; }
        
        public void Update(string registrationDate,string dueDate,
            decimal amount, SituationTypes situation)
        {
            RegistrationDate = registrationDate;
            DueDate = dueDate;
            Amount = amount;
            Situation = situation;
        }
        public void Confirmed()
        {
            Situation = SituationTypes.Confirmed;
        }
        public void UnderReview()
        {
            Situation = SituationTypes.UnderReview;
        }
        public void Returned()
        {
            Situation = SituationTypes.Returned;
        }
    }
}
