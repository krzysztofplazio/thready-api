using MediatR;
using Thready.API.Dtos.Users;

namespace Thready.API.Commands.CreateUser;

public class CreateUserCommand : IRequest<int>
{
    public RegisterUserRequest RegisterUserRequest { get; }
    public CreateUserCommand(RegisterUserRequest registerUserRequest)
    {
        RegisterUserRequest = registerUserRequest;
    }
}