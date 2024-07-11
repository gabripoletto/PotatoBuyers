using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using PotatoBuyers.Application.UseCases.User.Register;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        public async Task Success()
        {
            var request = RegisterUserValidatorBuilder.Build();

            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var userWriteOnly = UserWriteOnlyRepositoryBuilder.Build();

            var useCase = new RegisterUserUseCase(userWriteOnly, unitOfWork, passwordEncripter, mapper);

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Response.Should().Be(request.Name);
        }
    }
}
