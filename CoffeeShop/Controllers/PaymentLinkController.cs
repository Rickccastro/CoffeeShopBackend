using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace CoffeeShop.Controllers;
[Route("[controller]")]
[ApiController]
public class PaymentLinkController : ControllerBase
{
    public class PaymentItem
    {
        public string PriceId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreatePaymentLinkRequest
    {
        public List<PaymentItem> Items { get; set; }
    }


        [HttpPost]
        public async Task<IActionResult> CreatePaymentLink([FromBody] CreatePaymentLinkRequest request)
        {
            if (request.Items == null || !request.Items.Any())
                return BadRequest("You must provide at least one item.");

            var lineItems = request.Items.Select(item => new PaymentLinkLineItemOptions
            {
                Price = item.PriceId,
                Quantity = item.Quantity
            }).ToList();

            var options = new PaymentLinkCreateOptions
            {
                LineItems = lineItems
            };

            var service = new PaymentLinkService();

            try
            {
                var paymentLink = await service.CreateAsync(options);
                return Ok(new
                {
                    Url = paymentLink.Url,
                    Id = paymentLink.Id
                });
            }
            catch (StripeException ex)
            {
                return StatusCode(500, $"Stripe error: {ex.Message}");
            }
        }
}
