using Bogus;
using Bogus.Extensions.Brazil;
using CommomTestUtilities.Cryptography;
using CommomTestUtilities.UtilitiesTest;
using PotatoBuyers.Domain.Entities.Users;

namespace CommomTestUtilities.Entities
{
    public class UserBaseBuilder
    {
        public static (UserBase user, string password) Build()
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();

            var password = PasswordGenerator.GeneratePassword();

            var user = new Faker<UserBase>()
                .RuleFor(user => user.Id, () => 1)
                .RuleFor(user => user.Name, (f) => f.Person.FullName)
                .RuleFor(user => user.Password, (f) => passwordEncripter.Encrypt(password))
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Cpf, (f) => f.Person.Cpf(true))
                .RuleFor(user => user.Telephone, (f) => f.Phone.PhoneNumberFormat(55));

            return (user, password);
        }
    }
}
