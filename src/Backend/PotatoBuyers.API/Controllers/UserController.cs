using Microsoft.AspNetCore.Mvc;
using PotatoBuyers.Application.UseCases.User.Register;
using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Communication.Responses;

namespace PotatoBuyers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("Registrar")]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(RequestRegisterUserJson request)
        {
            RegisterUserUseCase useCase = new RegisterUserUseCase();

            ResponseRegisterUserJson result = useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
