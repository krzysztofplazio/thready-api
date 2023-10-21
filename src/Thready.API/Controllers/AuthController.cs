using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Thready.API.Commands.CreateUser;
using Thready.API.Commands.VerifyPassword;
using Thready.API.Constants;
using Thready.API.Dtos.Authentication;
using Thready.API.Dtos.Users;
using Thready.API.Exceptions.Users;
using Thready.API.Queries.GetUserByUsername;
using Thready.Models.Models;

namespace Thready.API.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthController(IConfiguration configuration, IMediator mediator, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _configuration = configuration;
        _mediator = mediator;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel user, CancellationToken cancellationToken = default)
    {
        if (user?.Username is null || user?.Password is null)
        {
            return BadRequest();
        }

        var userResult = await _mediator.Send(new GetUserByUsernameQuery(user.Username), cancellationToken);
        var verifyResult = await _mediator.Send(new VerifyPasswordCommand(user.Username, user.Password), cancellationToken);

        if (!string.Equals(user.Username, userResult.Username, StringComparison.Ordinal)
            || verifyResult != PasswordVerificationResult.Success && verifyResult != PasswordVerificationResult.SuccessRehashNeeded)
        {
            throw new BadUsernameOrPasswordException(UserExceptionErrorCodes.BadUsernameOrPassword);
        }

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:Secret"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: _configuration["JwtOptions:Issuer"],
            audience: _configuration["JwtOptions:Audience"],
            claims: new List<Claim>
            {
                new(ClaimTypes.Role, userResult.Role),
            },
            expires: DateTime.Now.AddSeconds(_configuration.GetValue<int>("JwtOptions:ExpirationSeconds")),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return Ok(new AuthenticatedResponse { Token = tokenString });
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest registerUserRequest, CancellationToken cancellationToken = default)
    {
        var id = await _mediator.Send(new CreateUserCommand(registerUserRequest), cancellationToken: cancellationToken);
        return Created($"/api/users/{id}", null);
    }
}