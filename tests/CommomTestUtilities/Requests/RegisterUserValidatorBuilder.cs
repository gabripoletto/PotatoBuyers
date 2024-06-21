using Bogus;
using PotatoBuyers.Communication.Requests;
using Bogus.Extensions.Brazil;

namespace CommomTestUtilities.Requests
{
    public class RegisterUserValidatorBuilder
    {
        public static RequestRegisterUserJson Build()
        {

            return new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Name, (f) => f.Person.FullName)
                .RuleFor(user => user.Password, (f) => f.Internet.Password(6))
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Cpf, (f) => f.Person.Cpf(true))
                .RuleFor(user => user.Telefone, (f) => f.Phone.PhoneNumberFormat(55));

        }
    }
}
