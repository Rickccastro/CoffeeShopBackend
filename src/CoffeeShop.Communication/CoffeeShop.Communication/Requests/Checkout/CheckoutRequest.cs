using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Communication.Requests.Checkout
{
    public class CheckoutItemRequest
    {
        public Guid ProdutoId { get; set; }
        public int Quantity { get; set; }
    }

    public class CheckoutRequest
    {
        public List<CheckoutItemRequest> Items { get; set; }
    }
}
