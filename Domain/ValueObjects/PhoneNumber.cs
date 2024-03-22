using Domain.Message;
using Domain.OperationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public string Value { get; init; }
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
                string message = string.Format(ConstMessages.IsNull, nameof(PhoneNumber));
                return new OperationResult(false, message);
            }
            else if (!Validation.CheckPhoneNumberFormat(value))
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

        public static implicit operator string(PhoneNumber senderFirstName)
            => senderFirstName.Value;
    }

}
