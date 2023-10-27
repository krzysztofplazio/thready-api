using MediatR;
using Thready.API.Dtos.Users;

namespace Thready.API.Commands.UpdateUser;

public class UpdateUsersRefreshTokenCommand : IRequest
{
    public DateTime ExpiryTime { get; }
    public string Username { get; }
    public string? RefreshToken { get; }

    public UpdateUsersRefreshTokenCommand(string username, string? refreshToken, DateTime expiryTime)
    {
        Username = username;
        RefreshToken = refreshToken;
        ExpiryTime = expiryTime;
    }
}