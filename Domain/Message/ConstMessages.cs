using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Message
{
    public class ConstMessages
    {
        public const string IsNull = "The {0} is empty";
        public const string IncorrectFormat = "This format is not correct for {0}";
        public const string IncorrectFormatCharacters = "The format of the characters in the {0} is not correct";

    }
}
