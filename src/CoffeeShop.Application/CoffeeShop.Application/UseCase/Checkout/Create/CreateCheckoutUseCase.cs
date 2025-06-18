using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Communication.Responses;
using Stripe.Checkout;



namespace CoffeeShop.Application.UseCase.Checkout.Create
{
    public class CreateCheckoutUseCase : ICreateCheckoutUseCase
    {
        public Session CreateCheckout(CheckoutRequest request)
        {
            var domain = "http://localhost:4200";

            var lineItems = request.Items.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "brl",
                    UnitAmount = (long)(item.Amount * 100),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Name,
                        Description = item.Description,
                        Images = new List<string> { item.ImageUrl }
                    },
                },
                Quantity = item.Quantity,
            }).ToList();

            var options = new SessionCreateOptions
            {
                UiMode = "embedded",
                LineItems = lineItems,
                Mode = "payment",
                ReturnUrl = domain + "/return-checkout?session_id={CHECKOUT_SESSION_ID}",
            };

            var service = new SessionService();
            return service.Create(options);
        }
    }
}
