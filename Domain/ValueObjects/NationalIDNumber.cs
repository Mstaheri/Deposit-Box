using Domain.Common;
using Domain.Exceptions;
using Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class NationalIDNumber : ValueObject
    {
        public string Value { get; private set; }
        public NationalIDNumber(string value)
        {
            var result = CheckNationalIDNumber(value);
            if (result.Success == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckNationalIDNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = string.Format(ConstMessages.IsNull, nameof(NationalIDNumber));
                return new OperationResult(false, message);
            }
            else if (value.Length != 10 || !Validation.CheckNumberFormat(value))
            {
                string message = string.Format(ConstMessages.IncorrectFormat, nameof(NationalIDNumber));
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

        public static implicit operator NationalIDNumber(string value)
        => new NationalIDNumber(value);

        public static implicit operator string(NationalIDNumber nationalIDNumber)
            => nationalIDNumber.Value;
    }
}
