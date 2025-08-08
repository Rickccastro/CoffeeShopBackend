using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Domain.Repositories.Especificas
{
    public interface IOrderRepository : IRepositoryBase<OrdOrder>
    {
        void AttachProdutoAndPrice(IEnumerable<OriOrderItem> pedidoItens);
    }
}
