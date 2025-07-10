using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class PriPrice
{
    public Guid PriId { get; set; }

    public string PriIdProduto { get; set; } = null!;

    public long PriPrecoUnitario { get; set; }

    public DateTime PriDataInicio { get; set; }

    public DateTime? PriDataFim { get; set; }

    public virtual ICollection<PeiPedidoIten> PeiPedidoItens { get; set; } = new List<PeiPedidoIten>();

    public virtual ProProduto PriIdProdutoNavigation { get; set; } = null!;
}
