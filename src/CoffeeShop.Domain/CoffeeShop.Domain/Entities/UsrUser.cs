using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class UsrUser
{
    public Guid UsrIdUser { get; set; }

    public string UsrNmCpf { get; set; } = null!;

    public string UsrNmPassword { get; set; } = null!;

    public string UsrNmName { get; set; } = null!;

    public string UsrNmEndereco { get; set; } = null!;

    public virtual ICollection<OrdOrder> OrdOrders { get; set; } = new List<OrdOrder>();

    public virtual SenServiceEmailNotification? SenServiceEmailNotifications { get; set; }
}
