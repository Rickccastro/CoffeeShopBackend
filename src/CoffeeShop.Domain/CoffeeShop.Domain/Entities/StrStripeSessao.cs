using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class StrStripeSessao
{
    public Guid StrIdStripeSessao { get; set; }

    public Guid StrIdPedido { get; set; }

    public string StrIdSession { get; set; } = null!;

    public string StrNmPaymentIntentId { get; set; } = null!;

    public string StrNmStatus { get; set; } = null!;

    public string StrEnumModo { get; set; } = null!;

    public virtual PedPedido StrIdPedidoNavigation { get; set; } = null!;
}
