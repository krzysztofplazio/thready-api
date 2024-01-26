using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Thready.API.Commands.GenerateAccessToken;

public class GenerateAccessTokenHandler : IRequestHandler<GenerateAccessTokenCommand, string>
{
    private readonly IConfiguration _configuration;
    public GenerateAccessTokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:Secret"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: _configuration["JwtOptions:Issuer"],
            audience: _configuration["JwtOptions:Audience"],
            claims: request.Claims,
            expires: DateTime.Now.AddSeconds(_configuration.GetValue<int>("JwtOptions:ExpirationSeconds")),
            signingCredentials: signinCredentials
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(tokeOptions));
    }
}