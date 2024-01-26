using System.Security.Cryptography;
using MediatR;

namespace Thready.API.Commands.GenerateRefreshToken;

public class GenerateRefreshTokenHandler : IRequestHandler<GenerateRefreshTokenCommand, string>
{
    public Task<string> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Task.FromResult(Convert.ToBase64String(randomNumber));
    }
}