using Bogus;
using System.Text.RegularExpressions;

namespace CommomTestUtilities.UtilitiesTest
{
    public class PasswordGenerator
    {
        public static string GeneratePassword(int passwordLength = 10)
        {
            var faker = new Faker();
            string password;

            do
            {
                password = faker.Random.String2(passwordLength, @"^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$");
            } while (password.Length < 8);

            return password;
        }

        public static string GeneratePasswordWithNumber()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            return GenerateInvalidPassword(chars, numbers, @"\d");
        }

        public static string GeneratePasswordWithUppercase()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return GenerateInvalidPassword(chars, uppercase, @"[A-Z]");
        }

        public static string GeneratePasswordWithSpecialCharacter()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            const string specialChars = "!@#$%^&*()-_=+[{]}\\|;:'\",<.>/?";
            return GenerateInvalidPassword(chars, specialChars, @"[\W_]");
        }

        private static string GenerateInvalidPassword(string chars, string extraChars, string regexPattern)
        {
            Random random = new Random();

            string password;
            do
            {
                char[] buffer = new char[9];
                for (int i = 0; i < 8; i++)
                {
                    buffer[i] = chars[random.Next(chars.Length)];
                }
                buffer[8] = extraChars[random.Next(extraChars.Length)];
                password = new string(buffer);
            } while (!Regex.IsMatch(password, regexPattern));

            return password;
        }
    }
}
