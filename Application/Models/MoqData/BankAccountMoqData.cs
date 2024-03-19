using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.MoqData
{
    public class BankAccountMoqData
    {
        public Task<BankAccount> Get()
        {
            BankAccount bankAccount = new BankAccount("58589831109231322",
                "Mstaheri", "ss",
                "tejarat", ":D");

            return Task.FromResult(bankAccount);
        }
        public Task<List<BankAccount>> GetAll()
        {
            List<BankAccount> list = new List<BankAccount>()
            {
                new BankAccount("58589831109231322",
                "Mstaheri", "ss",
                "tejarat", ":D") ,
                new BankAccount("58589331509531432",
                "Estaheri", "ss",
                "tejarat", ":D")
            };
            return Task.FromResult(list);
        }
    }
}
