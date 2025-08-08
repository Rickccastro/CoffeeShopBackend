using AutoMapper;
using CoffeeShop.Application.UseCase.PedidoItem.CreateListaPedidoItem;
using CoffeeShop.Application.UseCase.PedidoItem.CreatePedidoItem;
using CoffeeShop.Application.UseCase.Preco.GetPrecoVigente;
using CoffeeShop.Application.UseCase.Produto.GetById;
using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.Entities;

public class CreateListaPedidoItemUseCase : ICreateListaPedidoItemUseCase
{
    private readonly IMapper _mapper;
    private readonly IGetListaProdutoByIdsUseCase _getListaProdutoByIdsUseCase;
    private readonly ICreatePedidoItemUseCase _createPedidoItemUseCase;
    private readonly IGetPrecoVigenteUseCase _getPrecoVigenteUseCase;

    public CreateListaPedidoItemUseCase(IMapper mapper,
            IGetListaProdutoByIdsUseCase getListaProdutoByIdsUseCase,
            ICreatePedidoItemUseCase createPedidoItemUseCase,
            IGetPrecoVigenteUseCase getPrecoVigenteUseCase)
    {
        _mapper = mapper;
        _getListaProdutoByIdsUseCase = getListaProdutoByIdsUseCase;
        _createPedidoItemUseCase = createPedidoItemUseCase;
        _getPrecoVigenteUseCase = getPrecoVigenteUseCase;
    }

    public async Task<List<OriOrderItem>> CreateListaPedidoItem(List<CheckoutListItemRequest> itens)
    {
        var lineItems = new List<OriOrderItem>();
        var dataAtual = DateTime.UtcNow;

        var produtos = await _getListaProdutoByIdsUseCase.GetListaProdutosAsync(itens);

        foreach (var item in itens)
        {
            var produto = produtos.FirstOrDefault(p => p.ProIdProduct == item.ProdutoId)
                          ?? throw new InvalidOperationException($"Produto {item.ProdutoId} não encontrado.");

            var preco = _getPrecoVigenteUseCase.GetPrecoVigente(produto, dataAtual);

            lineItems.Add(new OriOrderItem
            {
                OriIdItemsOrder = Guid.NewGuid(),
                OriIdProductNavigation = produto,
                OriIdPriceNavigation = preco,
                OriIntQuantity = item.Quantity,
                OriIntValorUnit = preco.PriIntUnitPrice,
                OriIntTotalValueItem = preco.PriIntUnitPrice * item.Quantity
            });
        }

        return lineItems;
    }
}
