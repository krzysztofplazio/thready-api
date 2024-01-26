using MediatR;
using Thready.API.Dtos.Users;

namespace Thready.API.Queries.GetUserByUsername;

public class GetUserByUsernameQuery : IRequest<UserDto>
{
    public string Username { get; }

    public GetUserByUsernameQuery(string username)
    {
        Username = username;
    }
}