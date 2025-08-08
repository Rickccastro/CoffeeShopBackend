namespace CoffeeShop.Communication.Requests.User
{
    public class UserRequest
    {
        public string UsrNmCpf { get; set; } = null!;

        public string UsrNmPassword { get; set; } = null!;

        public string UsrNmName { get; set; } = null!;

        public string UsrNmEndereco { get; set; } = null!;

        public string UsrEmailNm { get; set; } = null!;
    }
}
