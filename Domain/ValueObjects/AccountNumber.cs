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
    public sealed class AccountNumber : ValueObject
    {
        public string Value { get; private set; }
        public AccountNumber(string value)
        {
            var result = CheckAccountNumber(value);
            if (result.Success == true)
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
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = string.Format(ConstMessages.IsNull, nameof(AccountNumber));
                return new OperationResult(false, message);
            }
            else if (value.Length != 16 || !Validation.CheckNumberFormat(value))
            {
                string message = string.Format(ConstMessages.IncorrectFormat, nameof(AccountNumber));
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

        public static implicit operator AccountNumber(string value)
        => new AccountNumber(value);

        public static implicit operator string(AccountNumber accountNumber)
            => accountNumber.Value;
    }
}
