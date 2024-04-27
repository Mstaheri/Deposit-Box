using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.MoqData
{
    public class BankSafeTransactionsMoqData
    {
        public Task<BankSafeTransaction> Get()
        {
            BankSafeTransaction bankSafeTransactions = new BankSafeTransaction("ali",
                "1234123412341234", 1122,
                0);

            return Task.FromResult(bankSafeTransactions);
        }
        public Task<List<BankSafeTransaction>> GetAll()
        {
            List<BankSafeTransaction> list = new List<BankSafeTransaction>()
            {
                new BankSafeTransaction("ali",
                "1234123412341234", 1122,
                0) ,
                new BankSafeTransaction("ali",
                "1234123412341234", 0,
                45325)
            };
            return Task.FromResult(list);
        }
    }
}
