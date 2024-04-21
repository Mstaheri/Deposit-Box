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
    public class BankSafeDocument : IEntity
    {
        public Guid Code { get; private set; }
        public Name NameBankSafe { get; private set; }
        public AccountNumber AccountNumber { get; private set; }
        public PersianDate RegistrationDate { get; private set; }
        public PersianDate DueDate { get; private set; }
        public Money Deposit { get; private set; }
        public Money Withdrawal { get; private set; }
        public SituationTypes Situation { get; private set; }
        public BankAccount? BankAccount { get; private set; }
        public BankSafe? BankSafe { get; private set; }
        public BankSafeDocument(Name nameBankSafe, AccountNumber accountNumber, PersianDate registrationDate,
            PersianDate dueDate, Money deposit, Money withdrawal, SituationTypes situation)
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
        public void Update(PersianDate registrationDate, PersianDate dueDate,
            Money deposit, Money withdrawal)
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
