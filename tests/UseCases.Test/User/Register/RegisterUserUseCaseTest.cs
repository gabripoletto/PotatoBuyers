using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using PotatoBuyers.Application.UseCases.User.Register;
using PotatoBuyers.Exceptions.ExceptionsBase;
using PotatoBuyers.Exceptions.ResponsesMessages;
using Xunit;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RegisterUserValidatorBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Response.Should().Be(request.Name);
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RegisterUserValidatorBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        private RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var userWriteOnly = UserWriteOnlyRepositoryBuilder.Build();
            var userReadOnly = new UserReadOnlyRepositoryBuilder();

            if (!string.IsNullOrEmpty(email))
                userReadOnly.ExistActiveUserWithEmail(email);
            
            return new RegisterUserUseCase(userWriteOnly, userReadOnly.Builder(), unitOfWork, mapper, passwordEncripter);
        }
    }
}
