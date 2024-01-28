using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Thready.Application.Constants;
using Thready.Application.Exceptions.Users;
using Thready.Core.Models;
using Thready.Core.Repositories.Users;

namespace Thready.Application.Commands.VerifyPassword;

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