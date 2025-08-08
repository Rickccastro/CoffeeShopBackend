using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Domain.Repositories.Especificas
{
    public interface IPriceRepository : IRepositoryBase<PriPrice>
    {
        Task<PriPrice> ObterPrecoVigenteAsync(string produtoId, DateTime dataReferencia);
    }
}
