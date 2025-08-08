using BC = BCrypt.Net.BCrypt;
using CoffeeShop.Application.Services.InternalServices.Security.Cryptography;

namespace CoffeeShop.Infraestructure.Services.InternalServices.Security.Cryptography;
public class BCryptor : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
}