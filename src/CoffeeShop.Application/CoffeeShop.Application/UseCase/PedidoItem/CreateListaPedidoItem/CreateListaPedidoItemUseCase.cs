using AutoMapper;
using CoffeeShop.Application.UseCase.PedidoItem.CreateListaPedidoItem;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;

public class CreateListaPedidoItemUseCase : ICreateListaPedidoItemUseCase
{
    private readonly IMapper _mapper;

    public CreateListaPedidoItemUseCase(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<List<PeiPedidoIten>> CreateListaPedidoItem(List<CheckoutItemRequest> lineItems, DateTime dataAtual)
    {
        return _mapper.Map<List<PeiPedidoIten>>(lineItems);
    }
}
