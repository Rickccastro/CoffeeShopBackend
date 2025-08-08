using System;
using System.Collections.Generic;

namespace CoffeeShop.Domain.Entities;

public partial class PayPayment
{
    public string PayIdPayment { get; set; } = null!;

    public Guid PayOrderId { get; set; }

    public string? PayIdPaymentIntent { get; set; }

    public DateTime PayDateCreated { get; set; }

    public string PayEnumStatus { get; set; } = null!;

    public string PayNmMethod { get; set; } = null!;

    public string PayNmReceiptUrl { get; set; } = null!;

    public int PayIntAmountTotal { get; set; }

    public DateTime? PayDateStatusUpdated { get; set; }

    public string PayEnumRefundedStatus { get; set; } = null!;

    public string? PayNmFailureReason { get; set; }

    public virtual OrdOrder PayOrder { get; set; } = null!;

    public virtual ICollection<RefRefund> RefRefunds { get; set; } = new List<RefRefund>();
}
