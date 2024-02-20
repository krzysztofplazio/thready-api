using Thready.Core.Enums;

namespace Thready.Application.Dtos.Users;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public RoleEnum Role { get; set; } = RoleEnum.User;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}