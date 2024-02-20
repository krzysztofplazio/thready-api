using MediatR;
using Thready.Application.Dtos.Users;

namespace Thready.Application.Commands.CreateUser;

public class CreateUserCommand(RegisterUserRequest registerUserRequest) : IRequest<int>
{
    public RegisterUserRequest RegisterUserRequest { get; } = registerUserRequest;
}