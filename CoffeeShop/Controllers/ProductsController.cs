using Microsoft.AspNetCore.Mvc;
using Stripe;


namespace CoffeeShop.Controllers;
[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> imagem { get; set; } = [];

    }

    public class UpdateProductRequest
    {
        public Dictionary<string, string> Metadata { get; set; } = new();
    }


    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Product name is required.");
        }

        var options = new ProductCreateOptions
        {
            Name = request.Name,
            Description = request.Description,
            Images = request.imagem
        };

        var service = new ProductService();
        Product product = await service.CreateAsync(options);

        return Ok(new
        {
            product.Id,
            product.Name,
            product.Created,
            product.Active
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, [FromBody] UpdateProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Product ID is required.");

        var options = new ProductUpdateOptions
        {
            Metadata = request.Metadata,
        };

        var service = new ProductService();

        try
        {
            Product updatedProduct = await service.UpdateAsync(id, options);
            return Ok(new
            {
                updatedProduct.Id,
                updatedProduct.Name,
                updatedProduct.Metadata
            });
        }
        catch (StripeException ex)
        {
            return StatusCode(500, $"Stripe error: {ex.Message}");
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Product ID is required.");

        var service = new ProductService();

        try
        {
            Product product = await service.GetAsync(id);
            return Ok(new
            {
                product.Id,
                product.Name,
                product.Images,
                product.Metadata,
                product.Created,
                product.Active
            });
        }
        catch (StripeException ex)
        {
            return StatusCode(500, $"Stripe error: {ex.Message}");
        }
    }


    [HttpGet]
    public async Task<IActionResult> ListProducts([FromQuery] int limit = 10)
    {
        if (limit <= 0 || limit > 100)
            return BadRequest("Limit must be between 1 and 100.");

        var options = new ProductListOptions
        {
            Limit = limit
        };

        var service = new ProductService();

        try
        {
            StripeList<Product> products = await service.ListAsync(options);
            var result = products.Data.Select(p => new
            {
                p.Id,
                p.Name,
                p.Images,
                p.Metadata,
                p.Created,
                p.Active
            });

            return Ok(result);
        }
        catch (StripeException ex)
        {
            return StatusCode(500, $"Stripe error: {ex.Message}");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Product ID is required.");

        var service = new ProductService();

        try
        {
            Product deletedProduct = await service.DeleteAsync(id);
            return Ok(new
            {
                deletedProduct.Id,
                deletedProduct.Deleted
            });
        }
        catch (StripeException ex)
        {
            return StatusCode(500, $"Stripe error: {ex.Message}");
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts([FromQuery] string orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId))
            return BadRequest("Order ID is required.");

        var options = new ProductSearchOptions
        {
            Query = $"active:'true' AND metadata['order_id']:'{orderId}'"
        };

        var service = new ProductService();

        try
        {
            StripeSearchResult<Product> result = await service.SearchAsync(options);

            var products = result.Data.Select(p => new
            {
                p.Id,
                p.Name,
                p.Metadata,
                p.Created,
                p.Active
            });

            return Ok(products);
        }
        catch (StripeException ex)
        {
            return StatusCode(500, $"Stripe error: {ex.Message}");
        }
    }
}



