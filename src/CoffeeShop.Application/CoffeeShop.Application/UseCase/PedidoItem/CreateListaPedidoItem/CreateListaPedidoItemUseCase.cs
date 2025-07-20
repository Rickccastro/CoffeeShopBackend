using AutoMapper;
using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Application.UseCase.Checkout.CreateCheckoutLineItems;
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

    public async Task<List<PeiPedidoIten>> CreateListaPedidoItem(List<CheckoutListItemRequest> itens)
    {
        var lineItems = new List<PeiPedidoIten>();
        var dataAtual = DateTime.UtcNow;

        var produtos = await _getListaProdutoByIdsUseCase.GetListaProdutosAsync(itens);

        foreach (var item in itens)
        {
            var produto = produtos.FirstOrDefault(p => p.ProIdProduto == item.ProdutoId)
                          ?? throw new InvalidOperationException($"Produto {item.ProdutoId} não encontrado.");

            var preco = _getPrecoVigenteUseCase.GetPrecoVigente(produto, dataAtual);

            lineItems.Add(new PeiPedidoIten
            {
                PeiIdPedidoItens = Guid.NewGuid(),
                PeiIdProduto = produto.ProIdProduto,
                PeiIdProdutoNavigation = produto,

                PeiIdPreco = preco.PriId,
                PeiIdPrecoNavigation = preco,

                PeiIntQuantidade = item.Quantity,
                PeiIntValorUnit = preco.PriPrecoUnitario,
                PeiIntValorTotal = preco.PriPrecoUnitario * item.Quantity
            });
        }

        return lineItems;
    }
}
