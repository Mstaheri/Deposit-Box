using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Convertors
{
    public class PasswordConverter : ValueConverter<Password,string>
    {
        public PasswordConverter() :
            base(ClrToDb => ClrToDb.Value,
                DbToClr => new Password(DbToClr))
        {

        }
    }
}
