using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public decimal Value { get; private set; }
        public Money(decimal value = 0)
        {
            var result = CheckMoney(value);
            if (result.IsSuccess == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckMoney(decimal value)
        {
            var result = OperationResult.CreateValidator(value)
                .Validate(x => x < 0, string.Format(ConstMessages.NotNegative, nameof(Money)));
            return result;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator Money(decimal value)
        => new Money(value);

        public static implicit operator decimal(Money money)
            => money.Value;
    }
}
