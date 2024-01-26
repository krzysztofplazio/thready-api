using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Thready.API.Queries.GetPrincipalFromExpiredToken;

public class GetPrincipalFromExpiredTokenHandler : IRequestHandler<GetPrincipalFromExpiredTokenQuery, ClaimsPrincipal>
{
    private readonly IConfiguration _configuration;
    public GetPrincipalFromExpiredTokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<ClaimsPrincipal> Handle(GetPrincipalFromExpiredTokenQuery request, CancellationToken cancellationToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:Secret"])),
            ValidateLifetime = false,
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(request.Token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return Task.FromResult(principal);
    }
}