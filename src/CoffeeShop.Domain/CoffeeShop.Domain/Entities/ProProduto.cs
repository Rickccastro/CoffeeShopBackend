using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class ProProduto
{
    public string ProIdProduto { get; set; } = null!;

    public string ProNmImgSrc { get; set; } = null!;

    public string ProNmImgAlt { get; set; } = null!;

    public string ProNmSubtitle { get; set; } = null!;

    public string ProNmTitle { get; set; } = null!;

    public virtual ICollection<PeiPedidoIten> PeiPedidoItens { get; set; } = new List<PeiPedidoIten>();

    public virtual ICollection<PriPrice> PriPrices { get; set; } = new List<PriPrice>();
}
