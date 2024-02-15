using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class BankSafeDocument
    {
        public Guid CodeDocuments { get; private set; }
        public string NameBankSafe { get; private set; }
        public string AccountNumber { get; private set; }
        public string RegistrationDate { get; private set; }
        public string DueDate { get; private set; }
        public decimal DepositPrice { get; private set; }
        public decimal WithdrawalPrice { get; private set; }
        public SituationTypes Situation { get; private set; }
        public BankAccount BankAccount { get; private set; }
        public BankSafe BankSafe { get; private set; }
        public BankSafeDocument(string nameBankSafe, string accountNumber, string registrationDate,
            string dueDate, decimal depositPrice, decimal withdrawalPrice, SituationTypes situation)
        {
            NameBankSafe = nameBankSafe;
            AccountNumber = accountNumber;
            RegistrationDate = registrationDate;
            DueDate = dueDate;
            if (depositPrice == 0 && withdrawalPrice != 0 || depositPrice != 0 && withdrawalPrice == 0)
            {
                DepositPrice = depositPrice;
                WithdrawalPrice = withdrawalPrice;
            }
            else
            {
                new Exception("");
            }
            Situation = situation;
        }
        public void Edit(string registrationDate,string dueDate,
            decimal depositPrice, decimal withdrawalPrice)
        {
            RegistrationDate = registrationDate;
            DueDate = dueDate;
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
