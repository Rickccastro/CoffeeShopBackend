namespace CoffeeShop.Domain.Enums;
public enum PaymentMethods
{
    Card,
    Boleto,
    Pix
}

public static class PaymentMethodsExtensions
{
    public static string ToValue(this PaymentMethods method) =>
        method switch
        {
            PaymentMethods.Card => "card",
            PaymentMethods.Boleto => "boleto",
            PaymentMethods.Pix => "pix",
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
}