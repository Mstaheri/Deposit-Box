using Domain.Message;
using Domain.OperationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class LastName : ValueObject
    {
        public string Value { get; init; }
        public LastName(string value)
        {
            var result = CheckLastName(value);
            if (result.Success == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckLastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = string.Format(ConstMessages.IsNull, nameof(LastName));
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

        public static implicit operator LastName(string value)
        => new LastName(value);

        public static implicit operator string(LastName senderFirstName)
            => senderFirstName.Value;
    }

}
