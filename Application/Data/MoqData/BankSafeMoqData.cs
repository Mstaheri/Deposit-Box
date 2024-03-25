using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.MoqData
{
    public class BankSafeMoqData
    {
        public Task<BankSafe> Get()
        {
            BankSafe bankAccount = new BankSafe("Omid",123434);

            return Task.FromResult(bankAccount);
        }
        public Task<List<BankSafe>> GetAll()
        {
            List<BankSafe> list = new List<BankSafe>()
            {
                new BankSafe("Omid",123434) ,
                new BankSafe("MST",123434)
            };
            return Task.FromResult(list);
        }
    }
}
