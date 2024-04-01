using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Convertors
{
    public class NameConverter : ValueConverter<Name,string>
    {
        public NameConverter() : 
            base(ClrToDb => ClrToDb.Value,
                DbToClr => new Name(DbToClr))
        {

        }
    }
}
