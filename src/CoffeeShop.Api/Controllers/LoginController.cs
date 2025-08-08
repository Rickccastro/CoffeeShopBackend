using CoffeeShop.Application.UseCase.Login.Login;
using CoffeeShop.Application.UseCase.Login.LoginValidado;
using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Requests.Login;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [Route("login-user")]
    [HttpPost]
    public async Task<ActionResult> Login([FromServices] ILoginNotificationUseCase useCase, [FromBody] LoginRequest request)
    {
        await useCase.LoginNotification(request);

        return Ok();
    }

    [Route("validated-login-user")]
    [HttpPost]
    public async Task<IActionResult> ValidatedLogin( [FromServices] ILoginValidadoUseCase useCase, [FromBody] LoginValidatedRequest request)
    {
        var result = await useCase.LoginValidado(request);

        return Ok(result);
    }
}
