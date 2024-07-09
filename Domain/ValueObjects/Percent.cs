using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class Percent : ValueObject
    {
        public int Value { get; private set; }
        public Percent(int value)
        {
            var result = CheckPercent(value);
            if (result.IsSuccess == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        private OperationResult CheckPercent(int value)
        {
            var result = OperationResult.CreateValidator(value)
                .Validate(p => !(p >= 1 && p <= 100), string.Format(ConstMessages.NotBetweenNumber, nameof(Percent), "1", "100"));
            return result;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator Percent(int value)
        => new Percent(value);

        public static implicit operator int(Percent percent)
        => percent.Value;
    }
}
