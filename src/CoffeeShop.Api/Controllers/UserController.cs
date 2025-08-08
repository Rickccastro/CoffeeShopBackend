using CoffeeShop.Application.UseCase.Customer.Create;
using CoffeeShop.Application.UseCase.User.GetUser;
using CoffeeShop.Communication.Requests.User;
using CoffeeShop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        [Route("get-user")]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser([FromServices] IGetUserUseCase userService)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim);
            var user = await userService.GetUser(userId);
            if (user == null)
                return NotFound();

            // Retorne só os dados que frontend precisa
            return Ok(user);
        }
    }
}
