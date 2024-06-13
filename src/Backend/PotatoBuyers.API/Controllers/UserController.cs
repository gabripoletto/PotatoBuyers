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
        public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUserJson request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
