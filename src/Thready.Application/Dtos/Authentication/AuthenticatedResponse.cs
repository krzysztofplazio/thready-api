namespace Thready.Application.Dtos.Authentication;

public class AuthenticatedResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}