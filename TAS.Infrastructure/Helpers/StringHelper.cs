using System.Globalization;
using System.Text;

namespace TAS.Infrastructure.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        ///     Convert string to integer
        /// </summary>
        /// <param name="enteredString"></param>
        /// <returns>integer number, deafault return 0</returns>
        public static int ConvertToInt(string enteredString)
        {
            int value;
            return int.TryParse(enteredString, out value) ? value : 0;
        }

        /// <summary>
        ///     Check string is integer or not
        /// </summary>
        /// <param name="enteredString"></param>
        /// <returns>true or false</returns>
        public static bool IsIntNumber(string enteredString)
        {
            try
            {
                Convert.ToInt32(enteredString, CultureInfo.CurrentCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Generate uuid v4 string
        /// </summary>
        /// <returns>uuid string</returns>
        public static string GenerateUUID()
        {
            Guid uuid = Guid.NewGuid();
            return uuid.ToString();
        }

        /// <summary>
        ///     Convert string to base64 format
        /// </summary>
        /// <param name="enteredString"></param>
        /// <returns>Base64 format string</returns>
        public static string ConvertStringToBase64(string enteredString)
        {
            var stringBytes = Encoding.UTF8.GetBytes(enteredString);
            return Convert.ToBase64String(stringBytes);
        }

        /// <summary>
        ///     Convert base64 string to original
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns>Original string</returns>
        public static string ConvertBase64ToString(string base64String)
        {
            var stringBytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(stringBytes);
        }

        /// <summary>
        ///     Generate random string with number
        /// </summary>
        /// <param name="length"></param>
        /// <returns>Random string with number</returns>
        public static string GenerateStringMode1(int length)
        {
            return Generate(length, true);
        }

        /// <summary>
        ///     Generate random string with number and upper case
        /// </summary>
        /// <param name="length"></param>
        /// <returns>Random string with number and upper case</returns>
        public static string GenerateStringMode2(int length)
        {
            return Generate(length, true, true);
        }

        /// <summary>
        ///     Generate random string with number ,upper case and special char
        /// </summary>
        /// <param name="length"></param>
        /// <returns>Random string with number ,upper case and special char</returns>
        public static string GenerateStringMode3(int length)
        {
            return Generate(length, true, true, true);
        }

        /// <summary>
        ///     Generate random string with only lower case
        /// </summary>
        /// <param name="length"></param>
        /// <returns>Return random string with only lower case</returns>
        public static string GenerateStringMode0(int length)
        {
            return Generate(length, false, false, false);
        }

        /// <summary>
        ///     Generate random string
        /// </summary>
        /// <param name="length"></param>
        /// <param name="hasNumber"></param>
        /// <param name="hasUppercaseChar"></param>
        /// <param name="hasSpecialChar"></param>
        /// <returns>Random string</returns>
        private static string Generate(int length, bool hasNumber = false, bool hasUppercaseChar = false, bool hasSpecialChar = false)
        {
            var random = new Random();
            var seed = random.Next(1, int.MaxValue);
            const string allowedChars = "abcdefghijkmnopqrstuvwxyz";
            const string upperChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            const string numberChars = "0123456789";
            const string specialChars = @"!#$%&'()*+,-./:;<=>?@[\]_";
            var chars = new char[length];
            var rd = new Random(seed);
            for (var i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            var randomIndexs = new List<int>();
            var randomIndex = -1;
            if (hasNumber)
            {
                do
                {
                    randomIndex = rd.Next(0, length);
                    if (!randomIndexs.Contains(randomIndex))
                    {
                        randomIndexs.Add(randomIndex);
                        break;
                    }
                } while (randomIndexs.Contains(randomIndex));
                chars[randomIndex] = numberChars[rd.Next(0, numberChars.Length)];
            }
            if (hasUppercaseChar)
            {
                do
                {
                    randomIndex = rd.Next(0, length);
                    if (!randomIndexs.Contains(randomIndex))
                    {
                        randomIndexs.Add(randomIndex);
                        break;
                    }
                } while (randomIndexs.Contains(randomIndex));
                chars[randomIndex] = upperChars[rd.Next(0, upperChars.Length)];
            }
            if (hasSpecialChar)
            {
                do
                {
                    randomIndex = rd.Next(0, length);
                    if (!randomIndexs.Contains(randomIndex))
                    {
                        randomIndexs.Add(randomIndex);
                        break;
                    }
                } while (randomIndexs.Contains(randomIndex));
                chars[randomIndex] = specialChars[rd.Next(0, specialChars.Length)];
            }
            return new string(chars);
        }
    }
}
