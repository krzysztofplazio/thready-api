using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Thready.Application.Dtos.Projects;
using Thready.Core.Dtos.Paginations;
using Thready.Core.Models;
using Thready.Core.Repositories.Users;
using Thready.Infrastructure.Contexts;
using Thready.Infrastructure.Extentions;

namespace Thready.Infrastructure.Repositories.Projects;

public class ProjectsRepository(ThreadyDatabaseContext context) : IProjectsRepository
{
    private readonly ThreadyDatabaseContext _context = context;

    public async Task<PagedItems<ProjectResult>> GetAssignedProjects(string? order,
                                                                     string? search,
                                                                     int userId,
                                                                     int pageNumber,
                                                                     int pageSize,
                                                                     CancellationToken cancellationToken = default)
    {
        var baseQuery = _context.Projects
            .Include(x => x.ProjectPermissions.Where(x => x.UserId == userId))
            .Include(x => x.Creator)
            .Select(x => new Project
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DueDate = x.DueDate,
                Creator = x.Creator,
            });

        if (order is not null)
        {
            baseQuery = baseQuery.OrderBy(order);
        }

        return await baseQuery
            .Select(x => new ProjectResult
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DueDate = x.DueDate,
                Creator = string.Concat(x.Creator.FirstName, " ", x.Creator.LastName),
            })
            .ToPaginatedListAsync(pageNumber, pageSize, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ProjectResult?> GetProjectById(int id, CancellationToken cancellationToken = default) 
        => await _context.Projects
                    .Include(x => x.Creator)
                    .Select(x => new ProjectResult
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Creator = string.Concat(x.Creator.FirstName, " ", x.Creator.LastName),
                        Description = x.Description,
                        DueDate = x.DueDate,
                        
                    }).FirstOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
}
