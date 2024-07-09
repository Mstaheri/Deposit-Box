using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class PersianDate : ValueObject
    {
        public string Value { get; private set; }
        public PersianDate(string value)
        {
            var result = CheckPersianDate(value);
            if (result.IsSuccess == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckPersianDate(string value)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string message = string.Format(ConstMessages.IncorrectFormatCharacters, nameof(PersianDate));
            ReadOnlySpan<char> dataSpan = value;
            bool y = int.TryParse(dataSpan.Slice(0, 4), out int year);
            bool m = int.TryParse(dataSpan.Slice(5, 2), out int month);
            bool d = int.TryParse(dataSpan.Slice(8, 2), out int day);
            if (y == false || m == false || d == false)
            {
                return new OperationResult(false, message);
            }
            else
            {
                var result = OperationResult.CreateValidator(month)
                    .Validate(x => year < 1 || year > 1600, message)
                    .Validate(x => month < 1 || month > 12, message)
                    .Validate(x => day < 1 || day > persianCalendar.GetDaysInMonth(year, month), message);
                return result;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator PersianDate(string value)
        => new PersianDate(value);

        public static implicit operator string(PersianDate persianDate)
            => persianDate.Value;
    }
}
