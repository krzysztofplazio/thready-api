using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Thready.API.Commands.CreateUser;
using Thready.API.Commands.GenerateAccessToken;
using Thready.API.Commands.GenerateRefreshToken;
using Thready.API.Commands.UpdateUser;
using Thready.API.Commands.VerifyPassword;
using Thready.API.Constants;
using Thready.API.Dtos.Authentication;
using Thready.API.Dtos.Users;
using Thready.API.Exceptions.Users;
using Thready.API.Queries.GetPrincipalFromExpiredToken;
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
    public async ValueTask<IActionResult> LoginAsync([FromBody] LoginModel user, CancellationToken cancellationToken = default)
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

        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, userResult.Role),
            new(ClaimTypes.NameIdentifier, user.Username),
        };
        var token = await _mediator.Send(new GenerateAccessTokenCommand(claims));
        var refreshToken = await _mediator.Send(new GenerateRefreshTokenCommand());

        await _mediator.Send(new UpdateUsersRefreshTokenCommand(userResult.Username,
                                                                refreshToken,
                                                                DateTime.UtcNow.AddDays(_configuration.GetValue<double>("JwtOptions:RefreshToken:ExpiryDays"))),
                                                                cancellationToken: cancellationToken);
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Role, ClaimTypes.NameIdentifier);
        identity.AddClaims(claims);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
            });

        return Ok(new AuthenticatedResponse 
        { 
            AccessToken = token,
            RefreshToken = refreshToken,
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest registerUserRequest, CancellationToken cancellationToken = default)
    {
        var id = await _mediator.Send(new CreateUserCommand(registerUserRequest), cancellationToken: cancellationToken);
        return Created(string.Create(CultureInfo.InvariantCulture, $"/api/users/{id}"), value: null);
    }

    
    [HttpPost]
    [Route("refresh")]
    public async ValueTask<IActionResult> Refresh(TokenApiModel tokenApiModel, CancellationToken cancellationToken = default)
    {
        if (tokenApiModel is null)
        {
            return BadRequest();
        }

        var principal = await _mediator.Send(new GetPrincipalFromExpiredTokenQuery(tokenApiModel.AccessToken));
        var username = principal.Identity?.Name ?? throw new UserHasNoIdentityException(); 
        var user = await _mediator.Send(new GetUserByUsernameQuery(username), cancellationToken);
        if (user is null || !string.Equals(user.RefreshToken, tokenApiModel.RefreshToken, StringComparison.Ordinal) || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return BadRequest();
        }
        
        var newAccessToken = await _mediator.Send(new GenerateAccessTokenCommand(principal.Claims));
        var newRefreshToken = await _mediator.Send(new GenerateRefreshTokenCommand());
        await _mediator.Send(new UpdateUsersRefreshTokenCommand(username,
                                                                newRefreshToken,
                                                                DateTime.UtcNow.AddDays(_configuration.GetValue<double>("JwtOptions:RefreshToken:ExpiryDays"))),
                                                                cancellationToken: cancellationToken);

        return Ok(new AuthenticatedResponse()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
        });
    }

    [HttpPost, Authorize]
    [Route("revoke")]
    public async Task<IActionResult> Revoke(CancellationToken cancellationToken = default)
    {
        var username = User.Identity?.Name ?? throw new UserHasNoIdentityException();
        var user = await _mediator.Send(new GetUserByUsernameQuery(username)) ?? throw new UserNotExistException(UserExceptionErrorCodes.UserNotExist);

        await _mediator.Send(new UpdateUsersRefreshTokenCommand(username,
                                                                refreshToken: null,
                                                                DateTime.MinValue),
                                                                cancellationToken: cancellationToken);
        return NoContent();
    }

    [HttpDelete("logout"), Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return NoContent();
    }
}