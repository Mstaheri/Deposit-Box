using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations
{
    public class Validation
    {
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
