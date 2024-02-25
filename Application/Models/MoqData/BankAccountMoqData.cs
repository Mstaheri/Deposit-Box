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
    }
}
