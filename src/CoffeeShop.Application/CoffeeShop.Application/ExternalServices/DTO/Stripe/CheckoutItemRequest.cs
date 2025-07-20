using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.ExternalServices.DTO.Stripe;

public class CheckoutItemRequest
{
    public ProProduto Produto { get; set; }
    public PriPrice Preco { get; set; }

    public long Quantidade { get; set; }
    public decimal ValorTotalItem { get; set; }
}
