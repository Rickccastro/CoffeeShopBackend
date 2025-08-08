using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class OrdOrder
{
    public Guid OrdIdOrder { get; set; }

    public DateTime OrdDateCreated { get; set; }

    public string OrdEnumStatusOrder { get; set; } = null!;

    public int OrdIntTotalCostOrder { get; set; }

    public Guid OrdUsrId { get; set; }

    public virtual UsrUser OrdUsr { get; set; } = null!;

    public virtual ICollection<OriOrderItem> OriOrderItems { get; set; } = new List<OriOrderItem>();

    public virtual PayPayment PayPayments { get; set; } = new PayPayment();
}
