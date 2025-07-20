using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Domain.Repositories.Especificas
{
    public interface IPedidoRepository : IRepositoryBase<PedPedido>
    {
        void AttachProdutoAndPrice(IEnumerable<PeiPedidoIten> pedidoItens);
    }
}
