using Domain.Message;
using Domain.OperationResults;
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
        public Money(decimal value)
        {
            var result = CheckMoney(value);
            if (result.Success == true)
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
            if (value < 0)
            {
                string message = string.Format(ConstMessages.NotNegative, nameof(Money));
                return new OperationResult(false, message);
            }
            else
            {
                return new OperationResult(true, null);
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator Money(int value)
        => new Money(value);

        public static implicit operator decimal(Money number)
            => number.Value;
    }
}
