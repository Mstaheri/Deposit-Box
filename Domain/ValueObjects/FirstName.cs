using Domain.Message;
using Domain.OperationResults;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.ValueObjects
{
    public sealed class FirstName : ValueObject
    {
        public string Value { get; init; }
        public FirstName(string value)
        {
            var result = CheckFirstName(value);
            if (result.Success == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckFirstName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = string.Format(ConstMessages.IsNull, nameof(FirstName));
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

        public static implicit operator FirstName(string value)
        => new FirstName(value);

        public static implicit operator string(FirstName senderFirstName)
            => senderFirstName.Value;
    }
    
}
