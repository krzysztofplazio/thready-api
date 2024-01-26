using AutoMapper;
using MediatR;
using Thready.API.Dtos.Users;
using Thready.API.Repositories.Users;

namespace Thready.API.Queries.GetUserByUsername;

public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameQuery, UserDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public GetUserByUsernameHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<UserDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<UserDto>(await _usersRepository.GetUserByUsername(request.Username).ConfigureAwait(false));
}