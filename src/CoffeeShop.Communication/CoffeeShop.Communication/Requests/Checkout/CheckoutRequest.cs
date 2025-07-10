namespace CoffeeShop.Communication.Requests.Checkout
{
    public class CheckoutListItemRequest
    {
        public string ProdutoId { get; set; }
        public int Quantity { get; set; }
    }

    public class CheckoutRequest
    {
        public string UserId { get; set; }
        public List<CheckoutListItemRequest> Items { get; set; }
    }
}
