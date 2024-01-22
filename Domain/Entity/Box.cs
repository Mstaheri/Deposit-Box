using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Box
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int NumberOfShares { get; set; }
        public decimal SharePrice { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<DepositToBox> DepositToBoxs { get; set; }
    }
}
