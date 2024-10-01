using Domain.Attributes;
using Domain.Common;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class BankSafe : IEntity
    {
        public BankSafe(Name name, Money sharePrice)
        {
            Name = name;
            SharePrice = sharePrice;
        }
        public Name Name { get; private set; }
        public Money SharePrice { get; private set; }
        public virtual ICollection<UserAndNumberOfShare> UserAndNumberOfShares { get; private set; }
        public virtual ICollection<BankSafeTransaction> BankSafeTransactions { get; private set; }
        public virtual ICollection<BankSafeDocument> BankSafeDocuments { get; private set; }
        public virtual ICollection<Loan> loans { get; private set; }
        public virtual ICollection<LoanTransactions> LoanTransactions { get; private set; }
        public virtual ICollection<LoanDocument> LoanDocuments { get; private set; }
       
        public void Update(Money sharePrice)
        {
            SharePrice = sharePrice;
        }
    }
}
