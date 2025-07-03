using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class EsnEmailServicoNotificacao
{
    public Guid EmailId { get; set; }

    public string EmailNm { get; set; } = null!;

    public Guid EmailUsrId { get; set; }

    public virtual UsrUsuario EmailUsr { get; set; } = null!;

    public virtual ICollection<UsrUsuario> UsrUsuarios { get; set; } = new List<UsrUsuario>();
}
