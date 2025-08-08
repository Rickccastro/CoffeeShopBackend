using CoffeeShop.Application.UseCase.Preco.GetPrecoVigente;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.Preco.GetPrecoVIgente
{
    public class GetPrecoVigenteUseCase : IGetPrecoVigenteUseCase
    {
        public PriPrice GetPrecoVigente(ProProduct produto, DateTime dataAtual)
        {
            return produto.PriPrices
                .FirstOrDefault(p => p.PriDateStart <= dataAtual &&
                                     (p.PriDateEnd == null || p.PriDateStart >= dataAtual))
                ?? throw new InvalidOperationException($"Preço vigente não encontrado para o produto {produto.ProIdProduct}.");
        }
    }
}
