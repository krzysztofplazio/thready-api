using System.Security.Claims;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Thready.Application.Constants;
using Thready.Application.Exceptions.Users;
using Thready.Application.Queries.GetUserByUsername;
using Thready.Core.Models;
using Thready.Core.Repositories.Users;
using System.Globalization;

namespace Thready.Application.Commands.Login;

public class LoginHandler(IPasswordHasher<User> passwordHasher,
                          IUsersRepository usersRepository,
                          IHttpContextAccessor httpContextAccessor) : IRequestHandler<LoginCommand>
{
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(paramName: nameof(httpContextAccessor));
    private const int ExpiresDays = 1;

    async Task IRequestHandler<LoginCommand>.Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (request.Username is null || request.Password is null)
        {
            throw new BadHttpRequestException("");
        }

        var user = await _usersRepository.GetUserByUsername(request.Username, cancellationToken).ConfigureAwait(false) ?? throw new UserNotExistException(UserExceptionErrorCodes.UserNotExist);
        var verifyResult = _passwordHasher.VerifyHashedPassword(user, Encoding.UTF8.GetString(user.Password), request.Password);
        if (!string.Equals(user.Username, user.Username, StringComparison.Ordinal)
            || verifyResult != PasswordVerificationResult.Success && verifyResult != PasswordVerificationResult.SuccessRehashNeeded)
        {
            throw new BadUsernameOrPasswordException(UserExceptionErrorCodes.BadUsernameOrPassword);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, user.Role.Name.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture)),
        };

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Role, ClaimTypes.NameIdentifier);
        identity.AddClaims(claims);
        var principal = new ClaimsPrincipal(identity);
        await _httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(ExpiresDays),
            }).ConfigureAwait(false);
    }
}