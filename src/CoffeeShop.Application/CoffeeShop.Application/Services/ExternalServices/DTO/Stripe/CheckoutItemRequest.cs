using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;

public class CheckoutItemRequest
{
    public ProProduct Produto { get; set; }
    public PriPrice Preco { get; set; }

    public long Quantidade { get; set; }
    public decimal ValorTotalItem { get; set; }
}
