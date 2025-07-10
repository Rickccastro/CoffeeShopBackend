using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Domain.Repositories.Especificas
{
    public interface IPrecoRepository : IRepositoryBase<PriPrice>
    {
        Task<PriPrice> ObterPrecoVigenteAsync(string produtoId, DateTime dataReferencia);
    }
}
