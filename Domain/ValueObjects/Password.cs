using Domain.Message;
using Domain.OperationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class Password : ValueObject
    {
        public string Value { get; init; }
        public Password(string value)
        {
            var result = CheckPassword(value);
            if (result.Success == true)
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
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = string.Format(ConstMessages.IsNull, nameof(Password));
                return new OperationResult(false, message);
            }
            else if (!Validation.CheckFormatcharacter(value))
            {
                string message = string.Format(ConstMessages.IncorrectFormatCharacters, nameof(Password));
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

        public static implicit operator Password(string value)
        => new Password(value);

        public static implicit operator Password(UserName senderFirstName)
            => senderFirstName.Value;
    }
}
