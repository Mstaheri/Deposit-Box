using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class DepositToBox
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public Box Box { get; set; }
        public BankAccount BankAccount { get; set; }
    }
   
}
