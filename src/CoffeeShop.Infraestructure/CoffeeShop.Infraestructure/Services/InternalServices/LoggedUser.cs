using CoffeeShop.Application.Services.InternalServices;
using CoffeeShop.Application.Services.InternalServices.Security.Token;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoffeeShop.Infraestructure.Services.InternalServices;
internal class LoggedUser : ILoggedUser
{
    private readonly CoffeeShopDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(CoffeeShopDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<UsrUser> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .UsrUsers
            .AsNoTracking()
            .FirstAsync(user => user.UsrIdUser == Guid.Parse(identifier));
    }
}
