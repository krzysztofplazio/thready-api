namespace Thready.API.Dtos.Authentication;

public class TokenApiModel
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}