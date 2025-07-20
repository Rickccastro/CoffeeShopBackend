namespace CoffeeShop.Domain.Entities;

public partial class PedPedido
{
    public Guid PedIdPedido { get; set; }

    public Guid PedUsrId { get; set; }

    public string PedEnumStatusPedido { get; set; } = null!;

    public decimal PedIntValorTotal { get; set; }

    public DateOnly PedDateCriacao { get; set; }

    public string? PedStripeSessionId { get; set; }

    public string? PedStripePaymentIntentId { get; set; }

    public virtual UsrUsuario PedUsr { get; set; } = null!;

    public virtual ICollection<PeiPedidoIten> PeiPedidoItens { get; set; } = new List<PeiPedidoIten>();

    public virtual ICollection<StrStripeSessao> StrStripeSessaos { get; set; } = new List<StrStripeSessao>();
}
