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
        public Task<BankSafeTransactions> Get()
        {
            BankSafeTransactions bankAccount = new BankSafeTransactions("ali",
                "1234123412341234", 1122,
                0);

            return Task.FromResult(bankAccount);
        }
        public Task<List<BankSafeTransactions>> GetAll()
        {
            List<BankSafeTransactions> list = new List<BankSafeTransactions>()
            {
                new BankSafeTransactions("ali",
                "1234123412341234", 1122,
                0) ,
                new BankSafeTransactions("ali",
                "1234123412341234", 0,
                45325)
            };
            return Task.FromResult(list);
        }
    }
}
