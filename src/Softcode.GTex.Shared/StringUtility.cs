using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex
{
    public static class StringUtility
    {
        public static int GetNumberOfLowercase(this string value)
        {
            if(string.IsNullOrWhiteSpace(value) || !value.Any(char.IsLower))
            {
                return 0;
            }

            return value.Count(char.IsLower);
        }

        public static int GetNumberOfUppercase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Any(char.IsUpper))
            {
                return 0;
            }

            return value.Count(char.IsUpper);
        }

        public static int GetNumberOfDigit(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Any(char.IsDigit))
            {
                return 0;
            }

            return value.Count(char.IsDigit);
        }

        public static int GetNumberOfSpecialCharacter(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            int numberOfLetter = value.Count(char.IsLetterOrDigit);
            int numberOfWhiteSpace = value.Count(char.IsWhiteSpace);

            return value.Length - (numberOfLetter + numberOfWhiteSpace);
        }
    }
}
