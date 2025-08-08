using CoffeeShop.Application.Services.ExternalServices.Contracts.Stripe;
using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Enums;
using CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos;
using Stripe;
using Stripe.Checkout;

namespace CoffeeShop.Infraestructure.Services.ExternalServices.Stripe.WeebHook;
public class WebHookHandler : IWebHookHandler
{
    private readonly string _webhookSecret;
    private readonly PaymentRepository _paymentRepository;


    public WebHookHandler(StripeSettings stripeSettings, PaymentRepository paymentRepository)
    {
        _webhookSecret = stripeSettings.WebHookSecret;
        _paymentRepository = paymentRepository;
    }

    //public async Task HandleEventAsync(string json, string stripeSignature)
    //{
    //    var stripeEvent = EventUtility.ConstructEvent(
    //        json,
    //        stripeSignature,
    //        _webhookSecret
    //    );

    //    switch (stripeEvent.Type)
    //    {
    //        case "payment_intent.succeeded":
    //            var pi = stripeEvent.Data.Object as PaymentIntent;
    //            // lógica para pagamento aprovado
    //            break;

    //        case "payment_intent.payment_failed":
    //            var failed = stripeEvent.Data.Object as PaymentIntent;
    //            // lógica para pagamento falhou
    //            break;

    //        case "charge.refunded":
    //            var refundedCharge = stripeEvent.Data.Object as Charge;
    //            // lógica para reembolso
    //            break;

    //        case "checkout.session.completed":
    //            var session = stripeEvent.Data.Object as Session;
    //            // lógica para checkout concluído
    //            break;

    //        default:
    //            break;
    //    }
    //}

    public async Task HandleEventAsync(string json, string stripeSignature)
    {
        var stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, _webhookSecret);

        switch (stripeEvent.Type)
        {
            case "payment_intent.succeeded":
                {
                    var session = stripeEvent.Data.Object as Session;

                    // Buscar a Session completa no Stripe
                    var sessionService = new SessionService();
                    var fullSession = await sessionService.GetAsync(session.Id);

                    // Buscar o PaymentIntent
                    var paymentIntentService = new PaymentIntentService();
                    var paymentIntent = await paymentIntentService.GetAsync(fullSession.PaymentIntentId);


                    var paymentMethodService = new PaymentMethodService();
                    var paymentMethod = await paymentMethodService.GetAsync(paymentIntent.PaymentMethod.ToString());

                    // Buscar o Charge para obter o ReceiptUrl
                    string receiptUrl = null;
                    if (!string.IsNullOrEmpty(paymentIntent.LatestChargeId))
                    {
                        var chargeService = new ChargeService();
                        var charge = await chargeService.GetAsync(paymentIntent.LatestChargeId);
                        receiptUrl = charge.ReceiptUrl;
                    }

                    // 1️⃣ Buscar o pagamento no banco usando EF
                    var payment = await _paymentRepository.ObterPorPropriedadeAsync(x=> x.PayIdPayment == fullSession.Id);
                    if (payment != null)
                    {
                        // 2️⃣ Atualizar propriedades
                        payment.PayIdPaymentIntent = paymentIntent.Id;
                        payment.PayEnumStatus = PaymentStatus.SUCCEEDED.ToValue();
                        payment.PayNmMethod = paymentMethod?.Type.ToUpper();
                        payment.PayNmReceiptUrl = receiptUrl;

                        // 3️⃣ Salvar alterações
                        await _paymentRepository.AtualizarAsync(payment);
                    }

                    break;
                }

            case "payment_intent.payment_failed":
            case "charge.refunded":
                {
                    var paymentIntentEvent = stripeEvent.Data.Object as PaymentIntent;
                    if (paymentIntentEvent == null)
                    {
                        var charge = stripeEvent.Data.Object as Charge;
                        if (charge?.PaymentIntentId != null)
                        {
                            paymentIntentEvent = await new PaymentIntentService().GetAsync(charge.PaymentIntentId);
                        }
                    }

                    var pi = paymentIntentEvent;


                    if (pi != null)
                    {
                        // Buscar Session ligada a esse PaymentIntent
                        var sessionService = new SessionService();
                        var sessionList = await sessionService.ListAsync(new SessionListOptions
                        {
                            PaymentIntent = pi.Id,
                            Limit = 1
                        });
                        var sessionId = sessionList.Data.FirstOrDefault()?.Id;

                        if (sessionId != null)
                        {
                            // Buscar o Charge para obter o ReceiptUrl (mesma lógica do primeiro case)
                            string receiptUrl = null;
                            if (!string.IsNullOrEmpty(pi.LatestChargeId))
                            {
                                var chargeService = new ChargeService();
                                var charge = await chargeService.GetAsync(pi.LatestChargeId);
                                receiptUrl = charge.ReceiptUrl;
                            }

                            //await _paymentRepository.AtualizarAsync(
                            //    sessionId,
                            //    pi.Id,
                            //    stripeEvent.Type switch
                            //    {
                            //        "payment_intent.succeeded" => "SUCCEEDED",
                            //        "payment_intent.payment_failed" => "FAILED",
                            //        "charge.refunded" => "REFUNDED",
                            //        _ => "UNKNOWN"
                            //    },
                            //    pi.PaymentMethodTypes?.FirstOrDefault(),
                            //    receiptUrl
                            //);
                        }
                    }

                    break;
                }
        }
    }
}
