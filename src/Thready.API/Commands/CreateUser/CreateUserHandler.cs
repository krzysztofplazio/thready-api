using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Thready.API.Dtos.Users;
using Thready.API.Exceptions.Roles;
using Thready.API.Repositories.Users;
using Thready.Models.Models;

namespace Thready.API.Commands.CreateUser;

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
        user.RoleId = userRole?.Id ?? _configuration.GetValue<int>("DefautUsersRole");
        
        await _usersRepository.InsertUser(user).ConfigureAwait(false);
        return user.Id;
    }
}