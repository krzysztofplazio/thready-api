using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thready.Application.Dtos.Users;
using Thready.Application.Exceptions.Users;
using Thready.Application.Queries.GetUserByUsername;

namespace Thready.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("me")]
    [Produces(typeof(UserDto))]
    public async Task<IActionResult> GetCurrentUser()
    {
        var username = HttpContext.User?.Claims
                        .FirstOrDefault(x => string.Equals(x.Type,
                                                           ClaimTypes.NameIdentifier,
                                                           StringComparison.OrdinalIgnoreCase))?.Value ?? throw new UserHasNoIdentityException();
        var query = new GetUserByUsernameQuery(username);
        return Ok(await _mediator.Send(query));
    }
}