using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using CoffeeShop.Application.ExternalServices.Contracts.AWS;
using CoffeeShop.Communication.Requests.Login;
using System.Security.Cryptography;
using System.Text;

public class AuthService : IJwtTokenService
{
    private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _userPoolId;

    public AuthService(
        AmazonCognitoIdentityProviderClient client,
        string clientId,
        string clientSecret,
        string userPoolId)
    {
        _cognitoClient = client;
        _clientId = clientId;
        _clientSecret = clientSecret;
        _userPoolId = userPoolId;
    }

    public async Task<string> AuthenticateUser(LoginValidatedRequest loginValidatedRequest)
    {
        var secretHash = GenerateSecretHash(loginValidatedRequest.Email);

        var authRequest = new AdminInitiateAuthRequest
        {
            UserPoolId = _userPoolId,
            ClientId = _clientId,
            AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
            AuthParameters = new Dictionary<string, string>
        {
            { "USERNAME", loginValidatedRequest.Email },
            { "PASSWORD", loginValidatedRequest.Senha },
            { "SECRET_HASH", secretHash }

        }
        };

        var authResponse = await _cognitoClient.AdminInitiateAuthAsync(authRequest);

        return authResponse.AuthenticationResult.IdToken;
    }

    private string GenerateSecretHash(string username)
    {
        var key = Encoding.UTF8.GetBytes(_clientSecret);
        var message = Encoding.UTF8.GetBytes(username + _clientId);

        using (var hmac = new HMACSHA256(key))
        {
            var hash = hmac.ComputeHash(message);
            return Convert.ToBase64String(hash);
        }
    }
}
