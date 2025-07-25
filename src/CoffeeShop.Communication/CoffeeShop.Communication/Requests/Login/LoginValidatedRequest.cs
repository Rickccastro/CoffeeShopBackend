using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Communication.Requests.Login;
public class LoginValidatedRequest
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Code { get; set; }
}
