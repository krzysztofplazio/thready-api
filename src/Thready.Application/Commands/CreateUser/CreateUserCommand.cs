using MediatR;
using Thready.Application.Dtos.Users;

namespace Thready.Application.Commands.CreateUser;

public class CreateUserCommand : IRequest<int>
{
    public RegisterUserRequest RegisterUserRequest { get; }
    public CreateUserCommand(RegisterUserRequest registerUserRequest)
    {
        RegisterUserRequest = registerUserRequest;
    }
}