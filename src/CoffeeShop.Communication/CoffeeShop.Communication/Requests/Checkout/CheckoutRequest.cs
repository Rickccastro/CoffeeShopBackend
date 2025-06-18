using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Communication.Requests.Checkout
{
    public class CheckoutItem
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CheckoutRequest
    {
        public List<CheckoutItem> Items { get; set; }
    }
}
