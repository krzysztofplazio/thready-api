using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Thready.API.Constants;
using Thready.API.Exceptions.Users;
using Thready.API.Repositories.Users;
using Thready.Models.Models;

namespace Thready.API.Commands.VerifyPassword;

public class VerifyPasswordHandler : IRequestHandler<VerifyPasswordCommand, PasswordVerificationResult>
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUsersRepository _usersRepository;

    public VerifyPasswordHandler(IPasswordHasher<User> passwordHasher, IUsersRepository usersRepository)
    {
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
    }
    public async Task<PasswordVerificationResult> Handle(VerifyPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByUsername(request.Username).ConfigureAwait(false) ?? throw new UserNotExistException(UserExceptionErrorCodes.UserNotExist);
        return await Task.FromResult(_passwordHasher.VerifyHashedPassword(user, Encoding.UTF8.GetString(user.Password), request.Password)).ConfigureAwait(false);
    }
}