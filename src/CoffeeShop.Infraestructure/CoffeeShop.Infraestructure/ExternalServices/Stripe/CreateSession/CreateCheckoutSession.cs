using CoffeeShop.Application.ExternalServices.Contracts.Stripe;
using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Entities;
using Stripe.Checkout;

namespace CoffeeShop.Infraestructure.ExternalServices.Stripe.CreateSession
{
    public class CreateCheckoutSession : ICreateCheckoutSession
    {
        public CheckoutSessionResult CreateSession(List<PeiPedidoIten> lineItems)
        {
            try
            {
                var listLineItemsOptions = this.MappingCheckoutToLineItemOption(lineItems);

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
                    ClientSecret = session.ClientSecret,
                    PaymentIntentId = session.PaymentIntentId,
                    ReturnUrl = session.ReturnUrl,
                    Status = session.Status,
                };
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<SessionLineItemOptions> MappingCheckoutToLineItemOption(List<PeiPedidoIten> lineItems)
        {
            return lineItems.Select(item => new SessionLineItemOptions
            {  
            Quantity = item.PeiIntQuantidade,
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "brl",
                UnitAmount = Convert.ToInt64(item.PeiIdPrecoNavigation.PriPrecoUnitario * 100),
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = item.PeiIdProdutoNavigation.ProNmTitle,
                    Description = item.PeiIdProdutoNavigation.ProNmSubtitle,
                    Images = new List<string> { item.PeiIdProdutoNavigation.ProNmImgSrc },

                }
            },
            }).ToList();
        } 
    }
}
