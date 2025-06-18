using CoffeeShop.Application.UseCase.Checkout.Create;
using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;


namespace CoffeeShop.Controllers;
[Route("[controller]")]
[ApiController]
public class CheckoutSessionController : ControllerBase
{
    [Route("create-checkout-session")]
    [HttpPost]
    public ActionResult Create([FromServices] ICreateCheckoutUseCase useCase, [FromBody] CheckoutRequest request)
    {
        var resultCreateCheckoutUseCase = useCase.CreateCheckout(request);

        return Ok(new CheckoutSessionResponse(resultCreateCheckoutUseCase.ToString()));
    }

    [HttpGet]
    public ActionResult SessionStatus([FromQuery] string session_id)
    {
        var sessionService = new SessionService();
        Session session = sessionService.Get(session_id);

        return new JsonResult(new { status = session.Status, customer_email = session.CustomerDetails.Email });
    }
}
