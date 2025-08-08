using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.Produto.GetById
{
    public interface IGetListaProdutoByIdsUseCase
    {
        Task<List<ProProduct>> GetListaProdutosAsync(List<CheckoutListItemRequest> items);
    }
}
    