using CoffeeShop.Application.UseCase.Customer.Create;
using CoffeeShop.Application.UseCase.Customer.Login;
using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Requests.User;
using CoffeeShop.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Route("create-user")]
        [HttpPost]
        public async Task <ActionResult> Create([FromServices] ICreateUserUseCase useCase, [FromBody] UserRequest request)
        {
            var resultCreateCheckoutUseCase =  await useCase.CreateUser(request);

            return Ok(resultCreateCheckoutUseCase);
        }

        [Route("login-user")]
        [HttpPost]
        public async Task<ActionResult> Login([FromServices] ILoginUserUseCase useCase, [FromBody] LoginRequest request)
        {
            var user = await useCase.LoginUser(request);

            return Ok(userResponse);
        }
    }
}
