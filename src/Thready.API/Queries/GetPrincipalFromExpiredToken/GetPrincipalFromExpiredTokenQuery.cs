using System.Security.Claims;
using MediatR;

namespace Thready.API.Queries.GetPrincipalFromExpiredToken
{
    public class GetPrincipalFromExpiredTokenQuery : IRequest<ClaimsPrincipal>
    {
        public string Token { get; }
        public GetPrincipalFromExpiredTokenQuery(string token)
        {
            Token = token;
        }
    }
}