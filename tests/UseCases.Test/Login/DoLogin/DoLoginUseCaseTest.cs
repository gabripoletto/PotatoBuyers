using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using PotatoBuyers.Application.UseCases.Login.DoLogin;
using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Domain.Entities.Users;
using PotatoBuyers.Exceptions.ExceptionsBase;
using PotatoBuyers.Exceptions.ResponsesMessages;
using Xunit;

namespace UseCases.Test.Login.DoLogin
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var useCase = CreateUseCase();

            var result = await useCase.Execute(new RequestLoginJson
            {
                Email = "",
                Password = "",
            });

            result.Should().NotBeNull();
            result.Name.Should().NotBeNullOrWhiteSpace().And.Be("");
        }

        [Fact]
        public async Task Error_Invalid_User()
        {
            var request = RequestLoginJsonBuilder.Build();

            var useCase = CreateUseCase();

            Func<Task> act = async () => { await useCase.Execute(request); };

            await act.Should().ThrowAsync<InvalidLoginException>()
                .Where(e => e.Message.Equals(ErrorMessages.EMAIL_OR_PASSWORD_INVALID));
        }

        private static DoLoginUseCase CreateUseCase(UserBase? user = null)
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            if (user is not null)
                userReadOnlyRepositoryBuilder.GetByEmailAndPassword(user);

            return new DoLoginUseCase(userReadOnlyRepositoryBuilder.Builder(), passwordEncripter);
        }
    }
}
