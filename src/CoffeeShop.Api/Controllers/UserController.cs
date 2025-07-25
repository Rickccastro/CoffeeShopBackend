using CoffeeShop.Application.UseCase.Customer.Create;
using CoffeeShop.Communication.Requests.User;
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
    }
}
