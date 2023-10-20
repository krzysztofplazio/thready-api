using MediatR;
using Microsoft.AspNetCore.Identity;
using Thready.API.Dtos.Users;


namespace Thready.API.Commands.VerifyPassword;

public class VerifyPasswordCommand : IRequest<PasswordVerificationResult>
{
    public string Username { get; }
    public string Password { get; }

    public VerifyPasswordCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
}