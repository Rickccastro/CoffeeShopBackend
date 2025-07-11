﻿using CoffeeShop.Communication.Requests.Checkout;
using Stripe.Checkout;

namespace CoffeeShop.Application.UseCase.Checkout.Create
{
    public interface ICreateCheckoutUseCase
    {
      Task<Session> CreateCheckout(CheckoutRequest request);
    }
}
