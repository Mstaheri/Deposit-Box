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
        public const string Duplicate = "The {0} is duplicate";
        public const string NotFound = "The desired {0} was not found";
        public const string Successfully = "{0} is {1} Successfully";
        public const string NotNegativeOrZero = "The {0} must not be negative or zero";
        public const string NotNegative = "The {0} must not be negative";
        public const string NotInventory = "The balance of {0} account in Bank {1} is {2}";

    }
}
