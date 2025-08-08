using CoffeeShop.Domain.Enums;

namespace CoffeeShop.Domain.Enums
{
    public enum OrderStatus
    {
        AWAITING_PAYMENT = 0,
        PREPARING = 1,
        DELIVERING = 2,
        DELIVERED = 3,
        CANCELED = 4,
        REFUNDED = 5,
    }
}

public static class OrderStatusExtensions
{
    public static string ToValue(this OrderStatus status) =>
        status switch
        {
            OrderStatus.AWAITING_PAYMENT => "AWAITING_PAYMENT",
            OrderStatus.PREPARING => "PREPARING",
            OrderStatus.DELIVERING => "DELIVERING",
            OrderStatus.DELIVERED => "DELIVERED",
            OrderStatus.CANCELED => "CANCELED",
            OrderStatus.REFUNDED => "REFUNDED",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}
