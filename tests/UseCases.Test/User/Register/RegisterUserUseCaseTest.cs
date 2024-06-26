using CommomTestUtilities.Requests;
using PotatoBuyers.Application.UseCases.User.Register;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        public async Task Success()
        {
            var request = RegisterUserValidatorBuilder.Build();

            var useCase = new RegisterUserUseCase();

            var result = await useCase.Execute(request);
        }
    }
}
