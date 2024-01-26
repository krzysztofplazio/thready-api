using AutoMapper;
using MediatR;
using Thready.API.Constants;
using Thready.API.Exceptions.Users;
using Thready.API.Repositories.Users;
using Thready.Models.Models;

namespace Thready.API.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUsersRefreshTokenCommand>
{
    private readonly IUsersRepository _usersRepository;
    public UpdateUserHandler(IUsersRepository usersRepository) => _usersRepository = usersRepository;

    public async Task<Unit> Handle(UpdateUsersRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByUsername(request.Username).ConfigureAwait(false) ?? throw new UserNotExistException(UserExceptionErrorCodes.UserNotExist);
        user.RefreshToken = request.RefreshToken;
        user.RefreshTokenExpiryTime = request.ExpiryTime;
        await _usersRepository.UpdateUser(user).ConfigureAwait(false);
        return Unit.Value;
    }
}