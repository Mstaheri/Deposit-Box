using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class BankSafe
    {
        public string Name { get; private set; }
        public decimal SharePrice { get; private set; }
        public ICollection<UserSharePrice> UserSharePrices { get; private set; }
        public ICollection<BankSafeTransactions> BankSafeTransactions { get; private set; }
        public ICollection<BankSafeDocument> BankSafeDocuments { get; private set; }
        public BankSafe(string name, decimal sharePrice)
        {
            Name = name;
            SharePrice = sharePrice;
        }
        public void Edit(decimal sharePrice)
        {
            SharePrice = sharePrice;
        }
    }
}
