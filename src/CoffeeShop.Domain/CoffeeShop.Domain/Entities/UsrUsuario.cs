using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class UsrUsuario
{
    public Guid UsrId { get; set; }

    public string UsrNm { get; set; } = null!;

    public long UsrIntPassword { get; set; }

    public string UsrNmEndereco { get; set; } = null!;

    public string UsrIntCpf { get; set; } = null!;
    public virtual EsnEmailServicoNotificacao UsrEmail { get; set; } = null!;
    public virtual ICollection<PedPedido> PedPedidos { get; set; } = new List<PedPedido>();

}
