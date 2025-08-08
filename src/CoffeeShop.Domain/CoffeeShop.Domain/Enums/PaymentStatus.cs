namespace CoffeeShop.Domain.Enums;
public enum PaymentStatus
{
    REFUNDED,
    CANCELLED,
    FAILED,
    SUCCEEDED,
    PENDING
}

public static class PaymentStatusExtensions
{
    public static string ToValue(this PaymentStatus status) =>
        status switch
        {
            PaymentStatus.REFUNDED => "REFUNDED",
            PaymentStatus.CANCELLED => "CANCELLED",
            PaymentStatus.FAILED => "FAILED",
            PaymentStatus.SUCCEEDED => "SUCCEEDED",
            PaymentStatus.PENDING => "PENDING",
     
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}