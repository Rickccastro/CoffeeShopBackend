using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class ProProduto
{
    public Guid ProIdProduto { get; set; }

    public string ProNmImgSrc { get; set; } = null!;

    public string ProNmImgAlt { get; set; } = null!;

    public string CafNmSubtitle { get; set; } = null!;

    public string CafNmTitle { get; set; } = null!;

    public virtual ICollection<PeiPedidoIten> PeiPedidoItens { get; set; } = new List<PeiPedidoIten>();

    public virtual ICollection<PriPrice> PriPrices { get; set; } = new List<PriPrice>();
}
