using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class BankAccount
    {
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string Description { get; set; }
        public User Users { get; set; }
        public ICollection<DepositToBox> DepositToBoxs { get; set; }
    }
}
