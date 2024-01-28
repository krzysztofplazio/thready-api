using MediatR;
using Microsoft.AspNetCore.Identity;
using Thready.Application.Dtos.Users;


namespace Thready.Application.Commands.VerifyPassword;

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