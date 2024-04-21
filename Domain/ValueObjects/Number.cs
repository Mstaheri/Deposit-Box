using Domain.Common;
using Domain.Exceptions;
using Domain.Message;
using Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class Number : ValueObject
    {
        public int Value { get; private set; }
        public Number(int value)
        {
            var result = CheckNumber(value);
            if (result.Success == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckNumber(int value)
        {
            if (value <= 0)
            {
                string message = string.Format(ConstMessages.NotNegativeOrZero, nameof(Number));
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

        public static implicit operator Number(int value)
        => new Number(value);

        public static implicit operator int(Number number)
            => number.Value;
    }
}
