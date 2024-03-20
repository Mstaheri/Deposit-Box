using Domain.Attributes;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class BankSafeDocument
    {
        public Guid Code { get; private set; }
        public string NameBankSafe { get; private set; }
        public string AccountNumber { get; private set; }
        public string RegistrationDate { get; private set; }
        public string DueDate { get; private set; }
        public decimal Deposit { get; private set; }
        public decimal Withdrawal { get; private set; }
        public SituationTypes Situation { get; private set; }
        public BankAccount BankAccount { get; private set; }
        public BankSafe BankSafe { get; private set; }
        public BankSafeDocument(string nameBankSafe, string accountNumber, string registrationDate,
            string dueDate, decimal deposit, decimal withdrawal, SituationTypes situation)
        {
            Code = Guid.NewGuid();
            NameBankSafe = nameBankSafe;
            AccountNumber = accountNumber;
            RegistrationDate = registrationDate;
            DueDate = dueDate;
            if (deposit == 0 && withdrawal != 0 || deposit != 0 && withdrawal == 0)
            {
                Deposit = deposit;
                Withdrawal = withdrawal;
            }
            else
            {
                new Exception("");
            }
            Situation = situation;
        }
        public void Update(string registrationDate,string dueDate,
            decimal deposit, decimal withdrawal)
        {
            RegistrationDate = registrationDate;
            DueDate = dueDate;
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
