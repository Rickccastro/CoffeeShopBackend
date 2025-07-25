using CoffeeShop.Application.UseCase.Login.Login;
using CoffeeShop.Application.UseCase.Login.LoginValidado;
using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Requests.Login;
using Microsoft.AspNetCore.Http;
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

        return Ok("Codigo Enviado");
    }

    [Route("validated-login-user")]
    [HttpPost]
    public async Task<ActionResult> ValidatedLogin([FromServices] ILoginValidadoUseCase useCase, [FromBody] LoginValidatedRequest request)
    {
        var result = await useCase.LoginValidado(request);

        return Ok(result);
    }
}
