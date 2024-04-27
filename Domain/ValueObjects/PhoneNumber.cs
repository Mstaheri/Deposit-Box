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
    public sealed class PhoneNumber : ValueObject
    {
        public string Value { get; private set; }
        public PhoneNumber(string value)
        {
            var result = CheckPhoneNumber(value);
            if (result.Success == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckPhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new OperationResult(true, null);
            }
            else if (value.Length != 11 || !value.StartsWith("09") || !Validation.CheckNumberFormat(value))
            {
                string message = string.Format(ConstMessages.IncorrectFormat, nameof(PhoneNumber));
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

        public static implicit operator PhoneNumber(string value)
        => new PhoneNumber(value);

        public static implicit operator string(PhoneNumber phoneNumber)
            => phoneNumber.Value;
    }

}
