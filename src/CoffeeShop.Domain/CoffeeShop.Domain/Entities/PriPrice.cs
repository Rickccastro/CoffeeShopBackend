namespace CoffeeShop.Domain.Entities;

public partial class PriPrice
{
    public string PriId { get; set; } = null!;

    public string PriIdProduto { get; set; } = null!;

    public decimal PriPrecoUnitario { get; set; }

    public DateTime PriDataInicio { get; set; }

    public DateTime? PriDataFim { get; set; }

    public virtual ICollection<PeiPedidoIten> PeiPedidoItens { get; set; } = new List<PeiPedidoIten>();

    public virtual ProProduto PriIdProdutoNavigation { get; set; } = null!;
}
