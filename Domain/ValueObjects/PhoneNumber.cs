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
            if (result.IsSuccess == true)
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
            var result = OperationResult.CreateValidator(value)
                .Validate(x => string.IsNullOrWhiteSpace(x), string.Format(ConstMessages.IsNull, nameof(PhoneNumber)))
                .Validate(x => x.Length != 11, string.Format(ConstMessages.IncorrectFormat, nameof(PhoneNumber)))
                .Validate(x => !x.StartsWith("09"), string.Format(ConstMessages.IncorrectFormat, nameof(PhoneNumber)))
                .Validate(x => !Validation.CheckNumberFormat(value), string.Format(ConstMessages.IncorrectFormat, nameof(PhoneNumber)));

            return result;
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
