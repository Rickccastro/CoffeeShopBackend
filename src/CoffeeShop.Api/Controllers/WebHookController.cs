using Microsoft.AspNetCore.Mvc;
using Stripe;


namespace CoffeeShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WebHookController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Webhook()
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();
        var stripeSignature = Request.Headers["Stripe-Signature"];

        //await _stripeWebhookHandler.HandleEventAsync(json, stripeSignature);

        return Ok();
    }
}
