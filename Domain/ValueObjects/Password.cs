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
    public sealed class Password : ValueObject
    {
        public string Value { get; private set; }
        public Password(string value)
        {
            var result = CheckPassword(value);
            if (result.IsSuccess == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckPassword(string value)
        {
            var result = OperationResult.CreateValidator(value)
                .Validate(x => string.IsNullOrWhiteSpace(x), string.Format(ConstMessages.IsNull, nameof(Password)))
                .Validate(x => !Validation.CheckFormatcharacter(value), string.Format(ConstMessages.IncorrectFormatCharacters, nameof(Password)));
            return result;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator Password(string value)
        => new Password(value);

        public static implicit operator string(Password password)
            => password.Value;
    }
}
