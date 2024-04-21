using Domain.Common;
using Domain.Exceptions;
using Domain.Message;
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
    public sealed class Name : ValueObject
    {
        public string Value { get; private set; }
        public Name(string value)
        {
            var result = CheckValueName(value);
            if (result.Success == true)
            {
                Value = value;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        private OperationResult CheckValueName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = string.Format(ConstMessages.IsNull, nameof(Name));
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

        public static implicit operator Name(string value)
        => new Name(value);

        public static implicit operator string(Name name)
            => name.Value;
    }
    
}
