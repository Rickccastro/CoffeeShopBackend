using CoffeeShop.Application.Services.ExternalServices.Contracts.Stripe;
using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Enums;
using Stripe.Checkout;

namespace CoffeeShop.Infraestructure.Services.ExternalServices.Stripe.CreateCheckout
{
    public class CreateCheckoutSession : ICreateCheckoutSession
    {
        public CheckoutSessionResult CreateSession(List<OriOrderItem> lineItems)
        {
            try
            {
                var listLineItemsOptions = MappingCheckoutToLineItemOption(lineItems);

                var domain = "http://localhost:4200";
                var sessionOptions = new SessionCreateOptions
                {
                    UiMode = "embedded",
                    LineItems = listLineItemsOptions,
                    Mode = "payment",
                    ReturnUrl = domain + "/return-checkout?session_id={CHECKOUT_SESSION_ID}",
                    CustomerEmail = lineItems.FirstOrDefault()?.OriIdOrderNavigation?.OrdUsr?.SenServiceEmailNotifications!.SenNmEmail,
                };

                var session = new SessionService().Create(sessionOptions);

                return new CheckoutSessionResult
                {
                    SessionId = session.Id,
                    ClientSecret = session.ClientSecret,
                    PaymentIntentId = session.PaymentIntentId,
                    ReturnUrl = session.ReturnUrl,
                    Status = session.Status,  
                    CustomerEmail = session.CustomerEmail,
                    PaymentMethodTypes = new List<string> { PaymentMethods.Card.ToValue(), PaymentMethods.Pix.ToValue()}
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<SessionLineItemOptions> MappingCheckoutToLineItemOption(List<OriOrderItem> lineItems)
        {
            return lineItems.Select(item => new SessionLineItemOptions
            {  
            Quantity = item.OriIntQuantity,
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "brl",
                UnitAmount = Convert.ToInt64(item.OriIdPriceNavigation.PriIntUnitPrice * 100),
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = item.OriIdProductNavigation.ProNmTitle,
                    Description = item.OriIdProductNavigation.ProNmSubtitle,
                    Images = new List<string> { item.OriIdProductNavigation.ProNmImgSrc },

                }
            },
            }).ToList();
        } 
    }
}
