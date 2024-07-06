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
    public sealed class NationalIDNumber : ValueObject
    {
        public string Value { get; private set; }
        public NationalIDNumber(string value)
        {
            var result = CheckNationalIDNumber(value);
            if (result.IsSuccess == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckNationalIDNumber(string value)
        {
            var result = OperationResult.CreateValidator(value)
                .Validate(x => string.IsNullOrWhiteSpace(x), string.Format(ConstMessages.IsNull, nameof(NationalIDNumber)))
                .Validate(x => x.Length != 10, string.Format(ConstMessages.IncorrectFormat, nameof(NationalIDNumber)))
                .Validate(x => !Validation.CheckNumberFormat(x), string.Format(ConstMessages.IncorrectFormat, nameof(NationalIDNumber)));

            return result;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator NationalIDNumber(string value)
        => new NationalIDNumber(value);

        public static implicit operator string(NationalIDNumber nationalIDNumber)
            => nationalIDNumber.Value;
    }
}
