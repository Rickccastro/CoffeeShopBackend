namespace CoffeeShop.Domain.Enums
{
    public enum PedidoStatus
    {
        /// <summary>
        /// O pedido foi criado, mas o pagamento ainda não foi concluído.
        /// </summary>
        Pendente = 0,

        /// <summary>
        /// O pagamento foi confirmado com sucesso.
        /// </summary>
        Pago = 1,

        /// <summary>
        /// A sessão expirou ou o pagamento falhou.
        /// </summary>
        Cancelado = 2
    }
}
