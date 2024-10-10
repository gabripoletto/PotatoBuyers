using Bogus;
using CommomTestUtilities.UtilitiesTest;
using PotatoBuyers.Domain.Entities.Users;

namespace CommomTestUtilities.Entities
{
    public class UserBaseBuilder
    {
        public static UserBase Build()
        {
            return new Faker<UserBase>()
                .RuleFor(user => user.Id, () => 1)
                .RuleFor(user => user.Name, (f) => f.Person.FirstName)
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Password, (f) => PasswordGenerator.GeneratePassword());
        }
    }
}
