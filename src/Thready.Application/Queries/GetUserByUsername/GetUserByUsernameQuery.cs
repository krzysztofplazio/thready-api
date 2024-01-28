using MediatR;
using Thready.Application.Dtos.Users;

namespace Thready.Application.Queries.GetUserByUsername;

public class GetUserByUsernameQuery : IRequest<UserDto>
{
    public string Username { get; }

    public GetUserByUsernameQuery(string username)
    {
        Username = username;
    }
}