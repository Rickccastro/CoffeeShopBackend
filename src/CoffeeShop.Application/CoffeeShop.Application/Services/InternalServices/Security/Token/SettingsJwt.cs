using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Application.Services.InternalServices.Security.Token;
public class SettingsJwt
{
    public string SigningKey { get; set; } = string.Empty;
    public uint ExpiresMinutes { get; set; }
}
