using System.Security.Claims;
using MediatR;

namespace Thready.API.Commands.GenerateAccessToken;

public class GenerateAccessTokenCommand : IRequest<string>
{
    public IEnumerable<Claim> Claims { get; }
    public GenerateAccessTokenCommand(IEnumerable<Claim> claims)
    {
        Claims = claims;
    }
}