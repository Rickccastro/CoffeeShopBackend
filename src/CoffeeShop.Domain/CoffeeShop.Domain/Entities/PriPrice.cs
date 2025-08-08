using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class PriPrice
{
    public string PriIdPrice { get; set; } = null!;

    public string PriProductId { get; set; } = null!;

    public DateTime PriDateStart { get; set; }

    public DateTime? PriDateEnd { get; set; }

    public decimal PriIntUnitPrice { get; set; }

    public virtual ICollection<OriOrderItem> OriOrderItems { get; set; } = new List<OriOrderItem>();

    public virtual ProProduct PriProduct { get; set; } = null!;
}
