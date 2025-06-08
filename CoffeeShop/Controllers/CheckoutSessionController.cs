using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using static System.Net.WebRequestMethods;

namespace CoffeeShop.Controllers;
[Route("[controller]")]
[ApiController]
public class CheckoutSessionController : ControllerBase
{

    public class CheckoutItem
    {
        //public string PriceId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CheckoutRequest
    {
        public List<CheckoutItem> Items { get; set; }
    }

    [Route("create-checkout-session")]
    [HttpPost]
    public ActionResult Create([FromBody] CheckoutRequest request)
    {
        var domain = "http://localhost:4200";

        var lineItems = request.Items.Select(item => new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "brl",
                UnitAmount = (long)(item.Amount * 100),
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = item.Name,
                    Description = item.Description,
                    Images = new List<string> { item.ImageUrl }
                },
            },
            Quantity = item.Quantity,
        }).ToList();

        var options = new SessionCreateOptions
        {
            UiMode = "embedded",
            LineItems = lineItems,
            Mode = "payment",
            ReturnUrl = domain + "/return-checkout?session_id={CHECKOUT_SESSION_ID}",
        };

        var service = new SessionService();
        var session = service.Create(options);

        return new JsonResult(new { clientSecret = session.ClientSecret });
    }

    [HttpGet]
    public ActionResult SessionStatus([FromQuery] string session_id)
    {
        var sessionService = new SessionService();
        Session session = sessionService.Get(session_id);

        return new JsonResult(new { status = session.Status, customer_email = session.CustomerDetails.Email });
    }
}
