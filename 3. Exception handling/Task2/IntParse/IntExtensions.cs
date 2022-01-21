using System;
using System.Linq;

namespace IntParse
{
    public static class IntExtensions
    {
        /// <summary>
        /// Max number of digits with sign
        /// </summary>
        private const int maxLength = 11;

        /// <summary>
        /// The method converts string to Int32
        /// </summary>
        /// <param name="source">String to convert</param>
        /// <param name="result">Int32 result</param>
        /// <returns>True if converts successful, otherweise false</returns>
        public static bool TryParseInt32(this string source, out int result)
        {
            result = 0;

            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            var negativeFlag = source[0].Equals('-') ? -1 : 1;

            if (negativeFlag == -1 || source[0].Equals('+'))
            {
                source = source[1..];
            }

            if (source.Any((c) => !char.IsDigit(c)) || source.Length > maxLength)
            {
                return false;
            }

            var reverceSource = source.Reverse();
            var factor = 1;
            int tempResult;

            foreach (var c in reverceSource)
            {
                var digit = GetDigit(c);

                if (digit == -1)
                {                    
                    return false;
                }

                try
                {
                    // Check for overflow
                    tempResult = checked(result * negativeFlag + digit * factor * negativeFlag);
                }
                catch (OverflowException)
                {
                    result = 0;
                    return false;
                }

                result += digit * factor;
                factor *= 10;
            }

            // Set sign
            result *= negativeFlag;
            return true;
        }

        /// <summary>
        /// The method converts char to digit from 0 to 9. If char contains no digit, returns -1.
        /// </summary>
        /// <param name="digit">Digit char format.</param>
        /// <returns>Digit int32 format or -1 if error.</returns>
        private static int GetDigit(char digit)
        {
            switch (digit) {
                case '0' :
                    {
                        return 0;
                    }
                case '1':
                    {
                        return 1;
                    }
                case '2':
                    {
                        return 2;
                    }
                case '3':
                    {
                        return 3;
                    }
                case '4':
                    {
                        return 4;
                    }
                case '5':
                    {
                        return 5;
                    }
                case '6':
                    {
                        return 6;
                    }
                case '7':
                    {
                        return 7;
                    }
                case '8':
                    {
                        return 8;
                    }
                case '9':
                    {
                        return 9;
                    }
                default:
                    {
                        return -1;
                    }
            }
        }
    }
}
