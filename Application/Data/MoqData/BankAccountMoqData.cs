using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.MoqData
{
    public class BankAccountMoqData
    {
        public Task<BankAccount> Get()
        {
            BankAccount bankAccount = new BankAccount("1234123412341234",
                "Mstaheri", "ss",
                "tejarat", ":D");

            return Task.FromResult(bankAccount);
        }
        public Task<List<BankAccount>> GetAll()
        {
            List<BankAccount> list = new List<BankAccount>()
            {
                new BankAccount("1234123412341234",
                "Mstaheri", "ss",
                "tejarat", ":D") ,
                new BankAccount("1234123412341234",
                "Estaheri", "ss",
                "tejarat", ":D")
            };
            return Task.FromResult(list);
        }
    }
}
