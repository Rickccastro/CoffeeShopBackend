using CoffeeShop.Application.UseCase.Email.EmailServiceNotification;
using CoffeeShop.Communication.Requests.Email;

using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [Route("create-email")]
        [HttpPost]
        public async Task<ActionResult> Create([FromServices] IEmailSenderNotificationUseCase useCase, [FromBody] EmailRequest request)
        {
            await useCase.SendEmailNotification(request.Email);

            return Ok("Email Enviado");
        }

    }
}
