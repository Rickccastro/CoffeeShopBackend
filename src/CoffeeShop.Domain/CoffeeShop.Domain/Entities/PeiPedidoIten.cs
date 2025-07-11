namespace CoffeeShop.Domain.Entities;

public partial class PeiPedidoIten
{
    public Guid PeiIdPedidoItens { get; set; }

    public Guid PeiIdPedido { get; set; }

    public string PeiIdPreco { get; set; } = null!;

    public string PeiIdProduto { get; set; } = null!;

    public long PeiIntQuantidade { get; set; }

    public long PeiIntValorUnit { get; set; }

    public long PeiIntValorTotal { get; set; }

    public virtual PedPedido PeiIdPedidoNavigation { get; set; } = null!;

    public virtual PriPrice PeiIdPrecoNavigation { get; set; } = null!;

    public virtual ProProduto PeiIdProdutoNavigation { get; set; } = null!;
}
