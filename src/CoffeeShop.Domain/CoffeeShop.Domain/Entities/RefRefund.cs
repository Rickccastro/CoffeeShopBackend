using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class RefRefund
{
    public string RefIdRefund { get; set; } = null!;

    public string RefPayIdPayment { get; set; } = null!;

    public string RefIdGatewayRefund { get; set; } = null!;

    public decimal RefIntAmountRefund { get; set; }

    public string? RefNmReason { get; set; }

    public DateTime RefDateCreatedAt { get; set; }

    public string RefEnumStatusRefund { get; set; } = null!;

    public DateTime? RefDateStatusUpdatedAt { get; set; }

    public string RefNmReceiptUrl { get; set; } = null!;

    public virtual PayPayment RefPayIdPaymentNavigation { get; set; } = null!;
}
