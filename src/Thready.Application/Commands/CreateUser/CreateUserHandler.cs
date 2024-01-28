using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Thready.Application.Dtos.Users;
using Thready.Core.Models;
using Microsoft.Extensions.Configuration;
using Thready.Core.Repositories.Users;
using System.Globalization;

namespace Thready.Application.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public CreateUserHandler(IUsersRepository usersRepository,
                             IMapper mapper,
                             IPasswordHasher<User> passwordHasher,
                             IConfiguration configuration)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userDto = _mapper.Map<UserDto>(request.RegisterUserRequest);
        var user = _mapper.Map<User>(userDto);
        var userRole = await _usersRepository.GetRoleByRoleName(request.RegisterUserRequest.Role).ConfigureAwait(false);
        user.Password = Encoding.UTF8.GetBytes(_passwordHasher.HashPassword(user, request.RegisterUserRequest.Password));
        user.RoleId = userRole?.Id ?? Convert.ToInt32(_configuration.GetSection("DefaultUsersRole").Value, CultureInfo.InvariantCulture);
        
        await _usersRepository.InsertUser(user).ConfigureAwait(false);
        return user.Id;
    }
}