using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.Preco.GetPrecoVigente
{
    public interface IGetPrecoVigenteUseCase
    {
        PriPrice GetPrecoVigente(ProProduct produto, DateTime dataAtual);
    }
}
