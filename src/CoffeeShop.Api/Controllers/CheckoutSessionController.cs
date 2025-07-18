﻿using CoffeeShop.Application.UseCase.Checkout.Create;
using CoffeeShop.Application.UseCase.Checkout.GetSessionStatus;
using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Communication.Responses;
using Microsoft.AspNetCore.Mvc;


namespace CoffeeShop.Controllers;
[Route("[controller]")]
[ApiController]
public class CheckoutSessionController : ControllerBase
{
    [Route("create-checkout-session")]
    [HttpPost]
    public async Task <ActionResult> Create([FromServices] ICreateCheckoutUseCase useCase, [FromBody] CheckoutRequest request)
    {
        var resultCreateCheckoutUseCase = await useCase.CreateCheckout(request);

        return Ok(new CheckoutSessionResponse(resultCreateCheckoutUseCase.ClientSecret));
    }

    [HttpGet]
    public ActionResult SessionStatus([FromServices] IGetSessionStatusUseCase useCase,[FromQuery] SessionRequest session)
    {

        var result = useCase.GetStatus(session);

        return  Ok(new SessionResponse { Status = result.Status, CustomerEmail = result.CustomerEmail });
    }
}
