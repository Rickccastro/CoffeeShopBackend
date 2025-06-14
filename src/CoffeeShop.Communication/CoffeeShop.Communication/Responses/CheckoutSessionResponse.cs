using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Communication.Responses
{
    public class CheckoutSessionResponse
    {
        public string ClientSecret { get; set; }

        public CheckoutSessionResponse(string clientSecret)
        {
            ClientSecret = clientSecret;
        }
    }
}
