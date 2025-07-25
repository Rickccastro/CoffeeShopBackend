using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using CoffeeShop.Application.ExternalServices.Contracts.AWS;

namespace CoffeeShop.Infraestructure.ExternalServices.AWS.JWT;
public class CreateUserAuth : ICreateUserAuth
{
    private readonly string _userPoolId;
    private readonly AmazonCognitoIdentityProviderClient _cognitoClient;

    public CreateUserAuth(AmazonCognitoIdentityProviderClient client, string userPoolId)
    {
        _cognitoClient = client;
        _userPoolId = userPoolId;
    }
    public async Task CreateAdminUser(string email, string password)
    {
        var request = new AdminCreateUserRequest
        {
            UserPoolId = _userPoolId,
            Username = email,
            UserAttributes = new List<AttributeType>
        {
            new AttributeType { Name = "email", Value = email },
            new AttributeType { Name = "email_verified", Value = "true" }
        },
            MessageAction = "SUPPRESS"
        };


        await _cognitoClient.AdminCreateUserAsync(request);

        var passwordRequest = new AdminSetUserPasswordRequest
        {
            UserPoolId = _userPoolId,
            Username = email,
            Password = password,
            Permanent = true
        };

        await _cognitoClient.AdminSetUserPasswordAsync(passwordRequest);
    }
}
