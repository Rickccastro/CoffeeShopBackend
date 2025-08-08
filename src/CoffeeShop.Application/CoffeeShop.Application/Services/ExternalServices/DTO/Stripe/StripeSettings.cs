using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
public class StripeSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public string WebHookSecret { get; set; } = string.Empty;
}