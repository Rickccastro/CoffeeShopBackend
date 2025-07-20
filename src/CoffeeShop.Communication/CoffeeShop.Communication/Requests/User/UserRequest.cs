namespace CoffeeShop.Communication.Requests.User
{
    public class UserRequest
    {
        public string UsrIntCpf { get; set; } = null!;
        public string UsrNm { get; set; } = null!;
        public string UsrIntPassword { get; set; } = null!;
        public string UsrNmEndereco { get; set; } = null!;
        public string EmailNm { get; set; } = null!;
    }
}
