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
    public sealed class UserName : ValueObject
    {
        public string Value { get; private set; }
        public UserName(string value)
        {
            var result = CheckUserName(value);
            if (result.Success == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckUserName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = string.Format(ConstMessages.IsNull, nameof(UserName));
                return new OperationResult(false, message);
            }
            else if (!Validation.CheckFormatcharacter(value))
            {
                string message = string.Format(ConstMessages.IncorrectFormatCharacters, nameof(UserName));
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

        public static implicit operator UserName(string value)
        => new UserName(value);

        public static implicit operator string(UserName userName)
            => userName.Value;
    }
}
