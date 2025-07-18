using CoffeeShop.Application.UseCase.Preco.GetPrecoVigente;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.Preco.GetPrecoVIgente
{
    public class GetPrecoVigenteUseCase : IGetPrecoVigenteUseCase
    {
        public PriPrice GetPrecoVigente(ProProduto produto, DateTime dataAtual)
        {
            return produto.PriPrices
                .FirstOrDefault(p => p.PriDataInicio <= dataAtual &&
                                     (p.PriDataFim == null || p.PriDataFim >= dataAtual))
                ?? throw new InvalidOperationException($"Preço vigente não encontrado para o produto {produto.ProIdProduto}.");
        }
    }
}
