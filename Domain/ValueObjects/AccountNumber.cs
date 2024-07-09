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
    public sealed class AccountNumber : ValueObject
    {
        public string Value { get; private set; }
        public AccountNumber(string value)
        {
            var result = CheckAccountNumber(value);
            if (result.IsSuccess == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckAccountNumber(string value)
        {
            var result = OperationResult.CreateValidator(value)
                .Validate(x => x.Length != 16 , string.Format(ConstMessages.IncorrectFormat, nameof(AccountNumber)))
                .Validate(x => !Validation.CheckNumberFormat(x) , string.Format(ConstMessages.IncorrectFormat, nameof(AccountNumber)));
            return result;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator AccountNumber(string value)
        => new AccountNumber(value);

        public static implicit operator string(AccountNumber accountNumber)
            => accountNumber.Value;
    }
}
