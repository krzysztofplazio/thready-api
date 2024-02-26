using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Thready.Application.Constants;
using Thready.Application.Dtos.Projects;
using Thready.Application.Exceptions.Users;
using Thready.Core.Dtos.Paginations;
using Thready.Core.Repositories.Users;

namespace Thready.Application.Queries.GetAssignedProjects;

public class GetAssignedProjectsHandler(IProjectsRepository projectsRepository, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetAssignedProjectsQuery, PagedItems<ProjectResult>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(paramName: nameof(httpContextAccessor));

    async Task<PagedItems<ProjectResult>> IRequestHandler<GetAssignedProjectsQuery, PagedItems<ProjectResult>>.Handle(GetAssignedProjectsQuery request, CancellationToken cancellationToken)
    {
        if (int.TryParse(_httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value 
                            ?? throw new UserHasNoIdentityException(UserExceptionErrorCodes.UserHasNoIdentity), CultureInfo.InvariantCulture, out int userId))
        {
            throw new UserNotExistException(UserExceptionErrorCodes.UserNotExist);
        }

       // TODO: Implement elasticsearch

        return await _projectsRepository.GetAssignedProjects(request.Order, request.Search, userId, request.PageSize, request.PageNumber, cancellationToken).ConfigureAwait(false);
    }
}
