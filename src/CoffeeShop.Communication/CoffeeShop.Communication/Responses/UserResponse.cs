namespace CoffeeShop.Communication.Responses
{
    public class UserResponse
    {
        public required string Cpf { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string Endereco { get; set; }
    }
}
