using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Convertors
{
    public class PercentConverter : ValueConverter<Percent, int>
    {
        public PercentConverter() :
            base(ClrToDb => ClrToDb.Value,
                DbToClr => new Percent(DbToClr))
        {

        }
    }
}
