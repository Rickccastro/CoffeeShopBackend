using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class PedidoRepository : RepositoryBase<PedPedido>, IPedidoRepository
    {
        public PedidoRepository(CoffeeShopDbContext context) : base(context) { }

        public void AttachProdutoAndPrice(IEnumerable<PeiPedidoIten> pedidoItens)
        {
            AttachEntities(pedidoItens.Select(pi => pi.PeiIdPrecoNavigation));
            AttachEntities(pedidoItens.Select(pi => pi.PeiIdProdutoNavigation));
        }
    }
}
