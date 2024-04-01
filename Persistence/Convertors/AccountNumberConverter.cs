using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Convertors
{
    public class AccountNumberConverter : ValueConverter<AccountNumber,string>
    {
        public AccountNumberConverter() :
            base(ClrToDb => ClrToDb.Value,
                DbToClr => new AccountNumber(DbToClr))
        {

        }
    }
}
