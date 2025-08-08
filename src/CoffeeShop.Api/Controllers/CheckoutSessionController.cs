using CoffeeShop.Application.Orchestrator.Checkout.Create;
using CoffeeShop.Application.UseCase.Checkout.Expire;
using CoffeeShop.Application.UseCase.Checkout.GetSessionStatus;
using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace CoffeeShop.Controllers;
[Route("[controller]")]
[ApiController]
public class CheckoutSessionController : ControllerBase
{
    [Authorize]
    [Route("create-checkout-session")]
    [HttpPost]
    public async Task <ActionResult> Create([FromServices] ICreateCheckoutOrchestrator orchestrator, [FromBody] CheckoutRequest request)
    {
        var resultCreateCheckoutUseCase = await orchestrator.CreateCheckout(request);

        return Ok(new CheckoutSessionResponse(resultCreateCheckoutUseCase.ClientSecret, resultCreateCheckoutUseCase.SessionId));
    }

    [Route("expire-checkout-session")]
    [HttpPost]
    public ActionResult Expire([FromServices] IExpireCheckoutUseCase useCase, [FromBody] SessionRequest session)
    {
        useCase.ExpireCheckout(session.SessionId);

       return Ok();
    }

    [HttpGet]
    public ActionResult SessionStatus([FromServices] IGetSessionStatusUseCase useCase,[FromQuery] SessionRequest session)
    {

        var result = useCase.GetStatus(session);

        return  Ok(new SessionResponse { Status = result.Status, CustomerEmail = result.CustomerEmail });
    }
}
