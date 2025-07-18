namespace CoffeeShop.Domain.ExternalServices.Stripe.Entities
{
    public class CheckoutItemRequest
    {
        public string ProdutoId { get; set; }         
        public string ProdutoNome { get; set; }
        public string ProdutoDescricao { get; set; }
        public string ProdutoImagem { get; set; }    

        public string PrecoId { get; set; }
        public long PrecoUnitario { get; set; }        
        public long Quantidade { get; set; }
        public long ValorTotalItem { get; set; }
    }
}
