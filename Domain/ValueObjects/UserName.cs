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
    public sealed class UserName : ValueObject
    {
        public string Value { get; private set; }
        public UserName(string value)
        {
            var result = CheckUserName(value);
            if (result.IsSuccess == true)
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
            var result = OperationResult.CreateValidator(value)
                .Validate(x => !Validation.CheckFormatcharacter(x), string.Format(ConstMessages.IncorrectFormatCharacters, nameof(UserName)));
            return result;
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
