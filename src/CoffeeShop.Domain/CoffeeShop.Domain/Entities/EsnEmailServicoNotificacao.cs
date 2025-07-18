﻿namespace CoffeeShop.Domain.Entities;

public partial class EsnEmailServicoNotificacao
{
    public Guid EmailId { get; set; }

    public string EmailNm { get; set; } = null!;

    public Guid? EmailUsrId { get; set; }

    public virtual UsrUsuario? EmailUsr { get; set; } 
}
