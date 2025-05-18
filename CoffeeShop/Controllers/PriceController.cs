using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using static CoffeeShop.Controllers.ProductsController;

namespace CoffeeShop.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PricesController : ControllerBase
{
    public class Product()
    {
        public string Name { get; set; } = string.Empty;
        public string id { get; set; } = string.Empty;

    }

    // POST: api/prices
    [HttpPost]
    public async Task<IActionResult> CreatePrice([FromBody] Product request)
    {
        var options = new PriceCreateOptions
        {
            Currency = "brl",
            UnitAmount = 1000,
            Product = request.id
        };

        var service = new PriceService();

        try
        {
            Price price = await service.CreateAsync(options);
            return Ok(price);
        }
        catch (StripeException ex)
        {
            return StatusCode(500, $"Stripe error: {ex.Message}");
        }
    }

    // PUT: api/prices/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePrice(string id)
    {
        var options = new PriceUpdateOptions
        {
            Metadata = new Dictionary<string, string> { { "order_id", "6735" } },
        };

        var service = new PriceService();

        try
        {
            Price price = await service.UpdateAsync(id, options);
            return Ok(price);
        }
        catch (StripeException ex)
        {
            return StatusCode(500, $"Stripe error: {ex.Message}");
        }
    }

    // GET: api/prices/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPrice(string id)
    {
        var service = new PriceService();

        try
        {
            Price price = await service.GetAsync(id);
            return Ok(price);
        }
        catch (StripeException ex)
        {
            return StatusCode(500, $"Stripe error: {ex.Message}");
        }
    }

    // GET: api/prices
    [HttpGet]
    public async Task<IActionResult> ListPrices([FromQuery] int limit = 3)
    {
        var options = new PriceListOptions { Limit = limit };
        var service = new PriceService();

        try
        {
            StripeList<Price> prices = await service.ListAsync(options);
            return Ok(prices.Data);
        }
        catch (StripeException ex)
        {
            return StatusCode(500, $"Stripe error: {ex.Message}");
        }
    }

    // GET: api/prices/search?orderId=6735
    [HttpGet("search")]
    public async Task<IActionResult> SearchPrices([FromQuery] string orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId))
            return BadRequest("Order ID is required.");

        var options = new PriceSearchOptions
        {
            Query = $"active:'true' AND metadata['order_id']:'{orderId}'",
        };

        var service = new PriceService();

        try
        {
            StripeSearchResult<Price> result = await service.SearchAsync(options);
            return Ok(result.Data);
        }
        catch (StripeException ex)
        {
            return StatusCode(500, $"Stripe error: {ex.Message}");
        }
    }
}
