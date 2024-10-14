using Bogus;
using Bogus.Extensions.Brazil;
using CommomTestUtilities.UtilitiesTest;
using PotatoBuyers.Communication.Requests;

namespace CommomTestUtilities.Requests
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build(int passwordLength = 10)
        {
            var test = new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Name, (f) => f.Person.FullName)
                .RuleFor(user => user.Password, (f) => PasswordGenerator.GeneratePassword(passwordLength))
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Cpf, (f) => f.Person.Cpf(true))
                .RuleFor(user => user.Telephone, (f) => f.Phone.PhoneNumberFormat(55));

            return test;
        }
    }
}
