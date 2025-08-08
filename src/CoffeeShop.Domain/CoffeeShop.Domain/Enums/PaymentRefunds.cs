namespace CoffeeShop.Domain.Enums;
public enum PaymentRefunds
{
    TOTAL,
    PARCIAL,
    NONE,
}


public static class PaymentRefundsExtensions
{
    public static string ToValue(this PaymentRefunds status) =>
        status switch
        {
            PaymentRefunds.TOTAL => "T",
            PaymentRefunds.PARCIAL => "P",
            PaymentRefunds.NONE => "N",

            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}