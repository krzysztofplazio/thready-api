using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Thready.Application.Dtos.Users;
using Thready.Core.Models;
using Microsoft.Extensions.Configuration;
using Thready.Core.Repositories.Users;
using System.Globalization;
using Thready.Application.Utils;
using Thready.Core.Enums;
using Thready.Application.Exceptions.Users;
using Thready.Application.Constants;

namespace Thready.Application.Commands.CreateUser;

public class CreateUserHandler(IUsersRepository usersRepository,
                               IMapper mapper,
                               IPasswordHasher<User> passwordHasher,
                               IConfiguration configuration) : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IConfiguration _configuration = configuration;

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _usersRepository.GetUserByUsername(request.RegisterUserRequest.Username, cancellationToken).ConfigureAwait(false) is not null)
        {
            throw new UsernameTakenException(UserExceptionErrorCodes.UsernameTaken);
        }

        var user = _mapper.Map<User>(request.RegisterUserRequest);
        var userRole = await _usersRepository.GetRoleById((int)request.RegisterUserRequest.Role.ToEnum<RoleEnum>(), cancellationToken).ConfigureAwait(false);
        user.Password = Encoding.UTF8.GetBytes(_passwordHasher.HashPassword(user, request.RegisterUserRequest.Password));
        user.RoleId = userRole?.Id ?? Convert.ToInt32(_configuration.GetSection("DefaultUsersRole").Value, CultureInfo.InvariantCulture);
        
        await _usersRepository.InsertUser(user, cancellationToken).ConfigureAwait(false);
        return user.Id;
    }
}