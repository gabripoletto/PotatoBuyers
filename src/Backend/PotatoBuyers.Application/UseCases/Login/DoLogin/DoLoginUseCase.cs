using PotatoBuyers.Application.Services.Cryptography;
using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Communication.Responses;
using PotatoBuyers.Domain.Repositories.User;
using PotatoBuyers.Exceptions.ExceptionsBase;

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
            var encriptedPassword = _passwordEncripter.Encrypt(request.Password);

            var user = await _repository.GetByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();
            
            return new ResponseRegisteredUserJson 
            {
                Name = user.Name 
            };
        }
    }
}
