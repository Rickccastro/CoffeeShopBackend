using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class ProProduct
{
    public string ProIdProduct { get; set; } = null!;

    public string ProNmImgAlt { get; set; } = null!;

    public string ProNmImgSrc { get; set; } = null!;

    public string ProNmSubtitle { get; set; } = null!;

    public string ProNmTitle { get; set; } = null!;

    public virtual ICollection<OriOrderItem> OriOrderItems { get; set; } = new List<OriOrderItem>();

    public virtual ICollection<PriPrice> PriPrices { get; set; } = new List<PriPrice>();
}
