using CoffeeShop.Domain.ExternalServices.Stripe;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;
using Stripe.Checkout;

namespace CoffeeShop.Infraestructure.ExternalServices.Stripe.CreateSession
{
    public class CreateCheckoutSession : ICreateCheckoutSession
    {
        public CheckoutSessionResult CreateSession(List<CheckoutItemRequest> lineItems)
        {
           var listLineItemsOptions =  new List<SessionLineItemOptions>();


            var domain = "http://localhost:4200";
            var sessionOptions = new SessionCreateOptions
            {
                UiMode = "embedded",
                LineItems = listLineItemsOptions,
                Mode = "payment",
                ReturnUrl = domain + "/return-checkout?session_id={CHECKOUT_SESSION_ID}",
            };

             var session = new SessionService().Create(sessionOptions);

            return new CheckoutSessionResult
            {
                SessionId = session.Id,
                ClientSecret =session.ClientSecret,
                PaymentIntentId = session.PaymentIntentId,
                ReturnUrl = session.ReturnUrl,
                Status = session.Status,            
            };
        }
    }
}
