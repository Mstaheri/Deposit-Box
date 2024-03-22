using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Validation
    {
        public static bool CheckPhoneNumberFormat(string Str)
        {
            if (Str.Length == 11 && Str.StartsWith("09") && CheckNumberFormat(Str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckNationalIDNumber(string Str)
        {
            if (Str.Length == 10 && CheckNumberFormat(Str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckNumberFormat(string Str)
        {
            bool result = long.TryParse(Str, out long a);
            return result;
        }
        public static bool CheckFormatcharacter(string input)
        {
            foreach (char c in input)
            {
                if (!(c >= ' ' && c <= '~'))
                    return false;
            }
            return true;
        }
    }
}
