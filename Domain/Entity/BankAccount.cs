using Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class BankAccount
    {
        public string AccountNumber { get; private set; }
        public string UserName { get; private set; }
        public string AccountName { get; private set; }
        public string BankName { get; private set; }
        public string Description { get; private set; }
        public User User { get; private set; }
        public ICollection<BankSafeTransactions> BankSafeTransactions { get; private set; }
        public ICollection<BankSafeDocument> BankSafeDocuments { get; private set; }
        public BankAccount(string accountNumber, string userName, string accountName,
            string bankName, string description)
        {
            AccountNumber = accountNumber;
            UserName = userName;
            AccountName = accountName;
            BankName = bankName;
            Description = description;
        }
        public void Update(string accountName,
            string bankName, string description)
        {
            AccountName = accountName;
            BankName = bankName;
            Description = description;
        }
    }
}
