using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Convertors
{
    public class NumberConverter : ValueConverter<Number,int>
    {
        public NumberConverter():
            base(ClrToDb => ClrToDb.Value,
                DbToClr => new Number(DbToClr))
        {

        }
    }
}
