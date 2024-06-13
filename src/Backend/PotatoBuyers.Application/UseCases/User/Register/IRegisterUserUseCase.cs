using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Communication.Responses;

namespace PotatoBuyers.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
    }
}
