using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class SenServiceEmailNotification
{
    public Guid SenIdServiceEmailNotification { get; set; }

    public string SenNmEmail { get; set; } = null!;

    public Guid? SenUsrId { get; set; }

    public virtual UsrUser? SenUsr { get; set; }
}
