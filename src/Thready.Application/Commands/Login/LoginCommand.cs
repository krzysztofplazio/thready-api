using MediatR;
using Microsoft.AspNetCore.Identity;
using Thready.Application.Dtos.Users;


namespace Thready.Application.Commands.Login;

public class LoginCommand(string? username, string? password) : IRequest
{
    public string? Username { get; } = username;
    public string? Password { get; } = password;
}