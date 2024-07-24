using Bogus;
using Bogus.Extensions.Brazil;
using PotatoBuyers.Communication.Requests;

namespace CommomTestUtilities.Requests
{
    public class RegisterUserValidatorBuilder
    {
        public static RequestRegisterUserJson Build(int passwordLength = 10)
        {
            var teste = new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Name, (f) => f.Person.FullName)
                .RuleFor(user => user.Password, (f) => GeneratePassword(passwordLength))
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Cpf, (f) => f.Person.Cpf(true))
                .RuleFor(user => user.Telefone, (f) => f.Phone.PhoneNumberFormat(55));


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
