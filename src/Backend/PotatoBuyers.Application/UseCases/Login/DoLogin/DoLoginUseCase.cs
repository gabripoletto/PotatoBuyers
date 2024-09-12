using PotatoBuyers.Application.Services.Cryptography;
using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Communication.Responses;
using PotatoBuyers.Domain.Repositories.User;

namespace PotatoBuyers.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly PasswordEncripter _passwordEncripter;

        public DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncripter passwordEncripter)
        {
            _repository = repository;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            return new ResponseRegisteredUserJson 
            {
                Name = ""
            };
        }
    }
}
