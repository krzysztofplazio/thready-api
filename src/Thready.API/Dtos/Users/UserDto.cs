namespace Thready.API.Dtos.Users;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public byte[]? Avatar { get; set; }
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
}