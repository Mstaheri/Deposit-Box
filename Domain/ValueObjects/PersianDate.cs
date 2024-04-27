using Domain.Common;
using Domain.Exceptions;
using Domain.Exceptions;
using System;
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
            if (result.Success == true)
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
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = string.Format(ConstMessages.IsNull, nameof(CheckPersianDate));
                return new OperationResult(false, message);
            }
            else
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                string message = string.Format(ConstMessages.IncorrectFormatCharacters, nameof(CheckPersianDate));
                string[] dateParts = value.Split('/');
                bool y = int.TryParse(dateParts[0] , out int year);
                bool m = int.TryParse(dateParts[1] , out int month);
                bool d = int.TryParse(dateParts[2] ,  out int day);
                if (y == false || m == false || d == false)
                {
                    return new OperationResult(false, message);
                }
                else  if (year < 1 || year > 1600)
                {
                    return new OperationResult(false, message);
                }
                else if (month < 1 || month > 12)
                {
                    return new OperationResult(false, message);
                }
                else if (day < 1 || day > persianCalendar.GetDaysInMonth(year, month))
                {
                    return new OperationResult(false, message);
                }
                else
                {
                    return new OperationResult(true, null);
                }   
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
