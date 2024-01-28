using System.Globalization;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Thready.Application.Commands.CreateUser;
using Thready.Application.Commands.VerifyPassword;
using Thready.Application.Constants;
using Thready.Application.Dtos.Authentication;
using Thready.Application.Dtos.Users;
using Thready.Application.Exceptions.Users;
using Thready.Application.Queries.GetUserByUsername;

namespace Thready.API.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

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

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest registerUserRequest, CancellationToken cancellationToken = default)
    {
        var id = await _mediator.Send(new CreateUserCommand(registerUserRequest), cancellationToken: cancellationToken);
        return Created(string.Create(CultureInfo.InvariantCulture, $"/api/users/{id}"), value: null);
    }

    [HttpDelete("logout"), Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return NoContent();
    }
}