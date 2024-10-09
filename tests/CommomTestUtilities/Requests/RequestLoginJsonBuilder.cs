using Bogus;
using PotatoBuyers.Communication.Requests;

namespace CommomTestUtilities.Requests
{
    public class RequestLoginJsonBuilder
    {
        public static RequestLoginJson Build(int passwordLength = 10)
        {
            var teste = new Faker<RequestLoginJson>()
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Email))
                .RuleFor(user => user.Password, (f) => GeneratePassword(passwordLength));

            return teste;
        }

        private static string GeneratePassword(int passwordLength)
        {
            var faker = new Faker();
            string password;

            do
            {
                password = faker.Random.String2(passwordLength, @"^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$");
            } while (password.Length < 8);

            return password;
        }
    }
}
