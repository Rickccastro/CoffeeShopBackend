using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class OriOrderItem
{
    public Guid OriIdItemsOrder { get; set; }

    public Guid OriIdOrder { get; set; }

    public string OriIdPrice { get; set; } = null!;

    public string OriIdProduct { get; set; } = null!;

    public long OriIntQuantity { get; set; }

    public decimal OriIntTotalValueItem { get; set; }

    public decimal OriIntValorUnit { get; set; }

    public virtual OrdOrder OriIdOrderNavigation { get; set; } = null!;

    public virtual PriPrice OriIdPriceNavigation { get; set; } = null!;

    public virtual ProProduct OriIdProductNavigation { get; set; } = null!;
}
