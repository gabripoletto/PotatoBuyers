using Microsoft.AspNetCore.Mvc;
using PotatoBuyers.Application.UseCases.Login.DoLogin;
using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Communication.Responses;

namespace PotatoBuyers.API.Controllers
{
    public class LoginController : PotatoBuyersBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }
    }
}
